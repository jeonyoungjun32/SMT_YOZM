using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;

namespace SmartMES.Models.Management
{
    class MM_PurchaseAnalysis
    {
        public string ItemCode { get; set; } //품목코드
        public string CustCode { get; set; } //거래처코드
        public string In_CustCode { get; set; } //창원(CU0194)/구미(CU0195)

        Database db = new Database();
        string query;


        public DataTable SalesPurchase_View(string yyyyMM, string yyyyMMdd)
        {

            string day_Qty_PriceColumn = ""; // 1~31일까지 수량,금액 컬럼 생성
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            CustCode = CustCode.ConvertStr_Empty_To_Null();
            In_CustCode = In_CustCode.ConvertStr_Empty_To_Null();
            query = "";

            for (int i = 1; i <= 31; i++)
            {
                if (i == 31) day_Qty_PriceColumn += $@"
                                FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN IN_QTY  END), '#,0') AS [" + i.ToString("D2") + @"InQTY],
								FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN OUT_QTY END), '#,0') AS [" + i.ToString("D2") + @"OUTQTY]";
                else day_Qty_PriceColumn += $@"
                                FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN IN_QTY  END), '#,0') AS [" + i.ToString("D2") + @"InQTY],
								FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN OUT_QTY END), '#,0') AS [" + i.ToString("D2") + @"OUTQTY],";
            }

            query += $@"SELECT
					    BV.CorpID, BV.ItemCode,BI.Item_Nm, BI.Alias , BI.Spec , BI.Unit , BI.Plate_Div_Cd,IP.Price,
					";

            query += day_Qty_PriceColumn;
            query += $@" FROM(
						 			SELECT CorpID, In_CustCode, 0 AS IN_QTY, sum(Vmi_Qty) AS OUT_QTY, ItemCode, RIGHT(Vmi_Date, 2) AS Day 
                                    FROM BN_Vmi
                                    GROUP BY ItemCode, In_CustCode, RIGHT(Vmi_Date, 2),CorpID
			
			                ) AS BV
                        LEFT JOIN BS_Item BI ON BV.ItemCode = BI.ItemCode AND BI.CorpID = BV.CorpID
						LEFT JOIN CM_Cust CM ON BI.CustCode = CM.CustCode AND CM.CorpID = BV.CorpID
				        LEFT JOIN BS_ItemClass BIC ON BIC.ItemClass_Cd = BI.ItemClass_Cd AND BIC.CorpID = BV.CorpID
                        LEFT JOIN BS_ItemPrice IP ON BI.CustCode = IP.CustCode and BI.ItemCode = IP.ItemCode AND IP.CorpID = BV.CorpID
                        WHERE BV.CorpID = '{WrGlobal.CorpID}' 
                        ";

            if (ItemCode != "NULL")
            {
                query += $"AND BV.ItemCode = {ItemCode}";
            }
            if (CustCode != "NULL")
            {
                query += $"AND BI.CustCode = {CustCode}";
            }
            if (In_CustCode != "NULL")
            {
                query += $"AND  BV.In_CustCode = {In_CustCode}";
            }

            query += $@"  GROUP BY BV.CorpID, BV.ItemCode,BI.Item_Nm
						, BI.Alias , BI.Spec , BI.Unit , BI.Plate_Div_Cd, IP.Price";


            return db.GetDataTable(query);
        }

        public DataTable PurchaseAnalysis_View(string yyyyMM)
        {

            string day_Qty_PriceColumn = ""; // 1~31일까지 수량,금액 컬럼 생성
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            CustCode = CustCode.ConvertStr_Empty_To_Null();
            query = "";

            for (int i = 1; i <= 31; i++)
            {
                if (i == 31) day_Qty_PriceColumn += $"FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN IN_QTY END),'#,0') AS [" + i.ToString("D2") + @"_QTY],
								FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN IN_PRICE * IN_QTY END),'#,0') AS Price_" + i.ToString("D2");
                else day_Qty_PriceColumn += $"FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN IN_QTY END),'#,0') AS [" + i.ToString("D2") + @"_QTY],
								FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN IN_PRICE * IN_QTY END),'#,0') AS Price_" + i.ToString("D2") + ", ";
            }

            query += $@"SELECT
					ItemClass_Nm, CustCode, CustName, ItemClass_Cd, Item_Nm, Alias, Spec, Unit, Grade, Color, ItemCode,
					";

            query += day_Qty_PriceColumn;

            query += $@" FROM(
						 SELECT 
                            BIC.ItemClass_Nm AS ItemClass_Nm,
                            BC.CustCode AS CustCode,
                            BC.CustName AS CustName,
                            BIC.ItemClass_Cd AS ItemClass_Cd,
                            BI.Item_Nm AS Item_Nm,
                            BI.Alias AS Alias,
                            BI.Spec AS Spec,
                            BI.Unit AS Unit,
                            BI.Grade AS Grade,
                            BI.Color AS Color,
                            MM.ItemCode AS ItemCode,
                            ISNULL(MM.IN_QTY  , 0) AS IN_QTY,
                            ISNULL(MM.IN_PRICE, 0) AS IN_PRICE,
                            RIGHT(MM.IN_DATE, 2) AS DAY
                        FROM MT_MatIn MM
                        LEFT JOIN CM_Cust BC ON BC.CustCode = MM.CustCode AND BC.CorpID = MM.CorpID
                        LEFT JOIN BS_ITEM BI ON BI.ItemCode = MM.ItemCode AND BI.CorpID = MM.CorpID
                        LEFT JOIN BS_ItemClass BIC ON BIC.ItemClass_Cd = BI.ItemClass_Cd AND BIC.CorpID = MM.CorpID
                        WHERE MM.CorpID = '{WrGlobal.CorpID}' 
                        -- AND MM.In_Price NOT IN (0)
                        AND LEFT(MM.In_Date,6) = '{yyyyMM}'
                        AND In_Div_Cd = '10'
                        ";

            if (ItemCode != "NULL")
            {
                query += $"AND BI.ItemCode = {ItemCode}";
            }
            if (CustCode != "NULL")
            {
                query += $"AND MM.CustCode = {CustCode}";
            }

            query += $@"
                                GROUP BY BIC.ItemClass_Nm, 
                                    BC.CustCode,
                                    MM.ItemCode,
                                    BC.CustName,
                                    BIC.ItemClass_Cd,
                                    BI.Item_Nm,
                                    BI.Alias,
                                    BI.Spec,
                                    BI.Unit,
                                    BI.Grade,
                                    BI.Color,
                                    MM.IN_QTY,
                                    MM.IN_PRICE,
                                    MM.IN_DATE
						        )AS SourceTable
                                GROUP BY 
                                    ItemClass_Nm, CustCode, CustName, ItemClass_Cd, Item_Nm, Alias, Spec, Unit, Grade, Color, ItemCode
                                ORDER BY
                                    CustCode, ItemClass_Cd, ItemCode;
                                ";

            return db.GetDataTable(query);
        }

        public DataTable SaleAnalysis_View(string yyyyMM)
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            CustCode = CustCode.ConvertStr_Empty_To_Null();
            query = "";
            string day_Qty_AmtColumn = ""; //날짜별 판매량, 금액 컬럼 담는 string

            for (int i = 1; i <= 31; i++)
            {
                if (i == 31)
                {
                    day_Qty_AmtColumn += $"FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN Out_Qty END), '#,0') AS Qty_" + i.ToString("D2") + @",
								            FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN Out_Amt END), '#,0') AS Amt_" + i.ToString("D2");
                }
                else
                {
                    day_Qty_AmtColumn += $"FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN Out_Qty END), '#,0') AS Qty_" + i.ToString("D2") + @",
								            FORMAT(SUM(CASE WHEN DAY = '" + i.ToString("D2") + "' THEN Out_Amt END), '#,0') AS Amt_" + i.ToString("D2") + ", ";
                }
            }

            query += @"SELECT
                        CustCode, CustName, ItemClass_Cd, ItemClass_Nm, ItemCode, Alias, Item_Nm, Spec, Unit, CT, Grade, Base_Weight, Color,";

            query += day_Qty_AmtColumn;

            query += $@"
                    FROM(
	                    SELECT 
		                    bp.CustCode ,
		                    bc.CustName ,
		                    bic.ItemClass_Cd AS ItemClass_Cd,
		                    bic.ItemClass_Nm AS ItemClass_Nm,
		                    bi.ItemCode AS ItemCode,
		                    bi.Alias AS Alias,
		                    bi.Item_Nm AS Item_Nm,
		                    bi.Spec AS Spec,
		                    bi.Unit AS Unit,
		                    bi.CT AS CT,
		                    bi.Grade AS Grade,
		                    bi.Base_Weight AS Base_Weight,
		                    bi.Color AS Color,
		                    CAST(bp.Out_Amt AS INT) AS Out_Amt,
		                    CAST(bp.Out_Qty AS INT) AS Out_Qty,
		                    RIGHT(bp.Out_Date,2) AS Day
	                    FROM BN_ProductOut bp
		                    LEFT JOIN CM_Cust bc ON bc.CustCode = bp.CustCode AND bp.CorpID = bc.CorpID
		                    LEFT JOIN BS_Item bi ON bi.ItemCode = bp.ItemCode AND bi.CorpID = bp.CorpID
		                    LEFT JOIN BS_ItemClass bic ON bic.ItemClass_Cd = bi.ItemClass_Cd AND bic.CorpID = bi.CorpID
	                    WHERE bp.CorpID = '{WrGlobal.CorpID}'
		                    AND LEFT(bp.Out_Date,6) = '{yyyyMM}'
                        ";

            if (ItemCode != "NULL")
            {
                query += $"AND bp.ItemCode = {ItemCode}";
            }
            if (CustCode != "NULL")
            {
                query += $"AND bp.CustCode = {CustCode}";
            }
            query += @"GROUP BY bp.Out_Date,
                            bp.CustCode ,
		                    bc.CustName ,
		                    bic.ItemClass_Cd,
		                    bic.ItemClass_Nm,
		                    bi.ItemCode,
		                    bi.Alias,
		                    bi.Item_Nm,
		                    bi.Spec,
		                    bi.Unit,
		                    bi.CT,
		                    bi.Grade,
		                    bi.Base_Weight,
		                    bp.Out_Amt,
		                    bi.Color,
		                    bp.Out_Qty
                        )AS resultTbl
                GROUP BY
                    CustCode, CustName, ItemClass_Cd, ItemClass_Nm, ItemCode, Alias, Item_Nm, Spec, Unit, CT, Grade, Base_Weight, Color
                ORDER BY 
                    ItemClass_Cd, ItemCode
                        ";

            return db.GetDataTable(query);
        }


        public DataTable SalesView_AmtByCust(string startDate, string endDate)
        {
            CustCode = CustCode.ConvertStr_Empty_To_Null();

            query = "";

            query += $@"
                        SELECT 
                        sm.ShipToCode ,
                        bc.CustName ,
                        SUM(CAST(bp.TotalPrice AS INT)) AS AMT

                        FROM BM_SalesD bp

                        LEFT JOIN BM_SalesM sm ON sm.SalesNo = bp.SalesNo AND bp.CorpID = sm.CorpID
                        LEFT JOIN CM_Cust bc ON bc.CustCode = sm.ShipToCode AND bp.CorpID = bc.CorpID

                        WHERE bp.CorpID = '{WrGlobal.CorpID}'
                        AND sm.SalesDate between '{startDate}' and '{endDate}'

                        ";

            if (CustCode != "NULL")
            {
                query += $@"AND sm.ShipToCode = {CustCode}";
            }

            query += $@"GROUP BY  sm.ShipToCode,  bc.CustName
                        HAVING SUM(CAST(bp.TotalPrice AS INT)) <> 0 AND SUM(CAST(bp.TotalPrice AS INT)) IS NOT NULL
                        ORDER BY CustName";

            return db.GetDataTable(query);
        }

        public DataTable PurchaseView_AmtByCust(string startDate, string endDate)
        {
            query = "";
            query += $@"
                        SELECT                          
                        pm.CustCode,
                        BC.CustName,
                        SUM(CAST(pd.Totalprice AS INT)) AS AMT

                        FROM MM_PurchaseD pd
                        LEFT JOIN MM_PurchaseM pm ON pd.Corpid = pm.Corpid AND pd.StoNo = pm.StoNo
                        LEFT JOIN CM_Cust BC ON BC.CustCode = pm.CustCode AND pd.CorpID = BC.CorpID

                        WHERE pd.CorpID ='{WrGlobal.CorpID}'
                        AND pm.StoDate between '{startDate}' and '{endDate}'

                        ";
            if (CustCode != "NULL" && CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $"AND pm.CustCode = {CustCode}";
            }

            query += $@"GROUP BY  pm.CustCode,  BC.CustName
                        HAVING SUM(CAST(pd.Totalprice AS INT)) <> 0 AND SUM(CAST(pd.Totalprice AS INT)) IS NOT NULL
                        ORDER BY CustName";

            return db.GetDataTable(query);
        }

        public DataTable Entire_AMTLabel(string current, string last)
        {
            query = $@"

    DECLARE @CurrentMonthAMT_P DECIMAL(18, 2);
    DECLARE @LastMonthAMT_P    DECIMAL(18, 2);

    DECLARE @CurrentMonthAMT_S DECIMAL(18, 2);
    DECLARE @LastMonthAMT_S    DECIMAL(18, 2);

    /* =========================================================
       당월 매출
    ========================================================= */
    SELECT
        @CurrentMonthAMT_S = SUM(ISNULL(SD.TotalPrice, 0))

    FROM BM_SalesM SM

    LEFT JOIN BM_SalesD SD
        ON SD.CorpId = SM.CorpId
       AND SD.SalesNo = SM.SalesNo

    WHERE SM.CorpId = '{WrGlobal.CorpID}'
      AND LEFT(SM.SalesDate, 6) = '{current}'
    ";

            //if (CustCode != "NULL" && CustCode != "" && CustCode.ToUpper() != "ALL")
            //{
            //    query += $@" AND SM.BillToCode = {CustCode}";
            //}

            query += $@"

    /* =========================================================
       전월 매출
    ========================================================= */
    SELECT
        @LastMonthAMT_S = SUM(ISNULL(SD.TotalPrice, 0))

    FROM BM_SalesM SM

    LEFT JOIN BM_SalesD SD
        ON SD.CorpId = SM.CorpId
       AND SD.SalesNo = SM.SalesNo

    WHERE SM.CorpId = '{WrGlobal.CorpID}'
      AND LEFT(SM.SalesDate, 6) = '{last}'
    ";

            //if (CustCode != "NULL" && CustCode != "" && CustCode.ToUpper() != "ALL")
            //{
            //    query += $@" AND SM.BillToCode = {CustCode}";
            //}

            query += $@"

    /* =========================================================
       당월 매입
    ========================================================= */
    SELECT
        @CurrentMonthAMT_P = SUM(ISNULL(PD.TotalPrice, 0))

    FROM MM_PurchaseM PM

    LEFT JOIN MM_PurchaseD PD
        ON PD.CorpId = PM.CorpId
       AND PD.StoNo = PM.StoNo

    WHERE PM.CorpId = '{WrGlobal.CorpID}'
      AND PM.PurchaseType = '10'
      AND LEFT(PM.StoDate, 6) = '{current}'
    ";

            //if (CustCode != "NULL" && CustCode != "" && CustCode.ToUpper() != "ALL")
            //{
            //    query += $@" AND PM.CustCode = {CustCode}";
            //}

            query += $@"

    /* =========================================================
       전월 매입
    ========================================================= */
    SELECT
        @LastMonthAMT_P = SUM(ISNULL(PD.TotalPrice, 0))

    FROM MM_PurchaseM PM

    LEFT JOIN MM_PurchaseD PD
        ON PD.CorpId = PM.CorpId
       AND PD.StoNo = PM.StoNo

    WHERE PM.CorpId = '{WrGlobal.CorpID}'
      AND PM.PurchaseType = '10'
      AND LEFT(PM.StoDate, 6) = '{last}'
    ";

            //if (CustCode != "NULL" && CustCode != "" && CustCode.ToUpper() != "ALL")
            //{
            //    query += $@" AND PM.CustCode = {CustCode}";
            //}

            query += $@"

    /* =========================================================
       결과
    ========================================================= */
    SELECT
          CONVERT(INT, ISNULL(@CurrentMonthAMT_P, 0)) AS 당월매입합계
        , CONVERT(INT, ISNULL(@LastMonthAMT_P, 0)) AS 전월매입합계

        , CONVERT(INT, ISNULL(@CurrentMonthAMT_S, 0)) AS 당월매출합계
        , CONVERT(INT, ISNULL(@LastMonthAMT_S, 0)) AS 전월매출합계

        , CONVERT(
            FLOAT,
            CASE
                WHEN ISNULL(@LastMonthAMT_S, 0) = 0
                THEN 0

                ELSE ROUND(
                    (@CurrentMonthAMT_S - @LastMonthAMT_S)
                    / @LastMonthAMT_S * 100,
                    2
                )
            END
          ) AS 전월대비매출상승률

        , CONVERT(
            FLOAT,
            CASE
                WHEN ISNULL(@LastMonthAMT_P, 0) = 0
                THEN 0

                ELSE ROUND(
                    (@CurrentMonthAMT_P - @LastMonthAMT_P)
                    / @LastMonthAMT_P * 100,
                    2
                )
            END
          ) AS 전월대비매입상승률
    ";

             return db.GetDataTable(query);
        }

        public DataTable SalesView_AmtByCust(string yyyyMM)
        {
            query = $@"
                    SELECT
                          SM.BillToCode AS Cust_Cd
                        , ISNULL(CC.CustName, SM.BillToName) AS Cust_Nm
                        , SUM(CAST(ISNULL(SD.TotalPrice, 0) AS BIGINT)) AS AMT

                    FROM BM_SalesM SM
                    LEFT JOIN BM_SalesD SD ON SD.CorpId = SM.CorpId AND SD.SalesNo = SM.SalesNo
                    LEFT JOIN CM_Cust CC ON CC.CorpId = SM.CorpId AND CC.CustCode = SM.BillToCode
                    WHERE SM.CorpId = '{WrGlobal.CorpID}' AND CONVERT(VARCHAR(6), SM.SalesDate, 112) = '{yyyyMM}'
                ";

            if (CustCode != "NULL" && CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $@" AND SM.BillToCode = '{CustCode}'";
            }

            query += $@"
                        GROUP BY SM.BillToCode, ISNULL(CC.CustName, SM.BillToName)
                        HAVING SUM(CAST(ISNULL(SD.TotalPrice, 0) AS BIGINT)) <> 0
                        ORDER BY Cust_Nm
                ";

            return db.GetDataTable(query);
        }

        public DataTable PurchaseView_AmtByCust(string yyyyMM)
        {
            query = $@"
                        SELECT
                              PM.CustCode AS Cust_Cd
                            , ISNULL(CC.CustName, '-') AS Cust_Nm
                            , SUM(CAST(ISNULL(PD.TotalPrice, 0) AS BIGINT)) AS AMT

                        FROM MM_PurchaseM PM
                        LEFT JOIN MM_PurchaseD PD ON PD.CorpId = PM.CorpId AND PD.StoNo = PM.StoNo
                        LEFT JOIN CM_Cust CC ON CC.CorpId = PM.CorpId AND CC.CustCode = PM.CustCode

                        WHERE PM.CorpId = '{WrGlobal.CorpID}' AND CONVERT(VARCHAR(6), PM.StoDate, 112) = '{yyyyMM}'
                    ";

            if (CustCode != "NULL" && CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $@" AND PM.CustCode = '{CustCode}'";
            }

            query += $@"
                    GROUP BY PM.CustCode, ISNULL(CC.CustName, '-')
                    HAVING SUM(CAST(ISNULL(PD.TotalPrice, 0) AS BIGINT)) <> 0
                    ORDER BY Cust_Nm
            ";

            return db.GetDataTable(query);
        }
    }
}
