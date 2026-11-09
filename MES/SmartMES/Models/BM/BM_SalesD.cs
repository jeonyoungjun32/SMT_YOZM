using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using DevExpress.CodeParser;
using DevExpress.DataAccess.Sql;
using DevExpress.Diagram.Core.Shapes;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.BM
{
    class BM_SalesD
    {

        #region Property

        public string CorpId { get; set; }
        public string SalesNo { get; set; }

        public int SalesSeq { get; set; }

        public string ItemCode { get; set; }

        public float UnitWeight { get; set; }

        public string UnitCode { get; set; }

        public float Qty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Price { get; set; }

        public decimal VatPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal WonAmount { get; set; }

        public string VatType { get; set; }

        public int VatRate { get; set; }

        public string ReqType { get; set; }

        public string SdOrderNo { get; set; }

        public int SdOrderSeq { get; set; }

        public string HouseCode { get; set; }

        public string LotNo { get; set; }

        public string BarCodeNo { get; set; }

        public string Status { get; set; }
        public string ShipToCode { get; set; }

        public string Remark { get; set; }

        public string DeliveryDate { get; set; }

        //조회용
        public string CustCode { get; set; }
        public string stDate { get; set; }
        public string endDate { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_BM_SalesD(string OutFrom, string OutTo, string outType = "")
        {
            //출고등록(Frm_BN_ProductOut에서 출고조회시 사용하는 Query
            query = $@"
                    SELECT '' AS Status
                          , sd.SalesNo
                          , sd.SalesSeq
                          , sm.SalesDate	
                          , sm.ShipDate	
                    
                          , sm.BillToCode
                          , c.CustName AS BillToName 
                          , c.ExchangeType
                          
                          , sm.ShipToCode
                          , cd.CustName AS ShipToName

                          , sm.CurrencyType
                          
                          , sd.ItemCode 
                          , Item.ItemName

                          , sd.ItemCode ItemCode
                          , Item.ItemName 
                          , Item.Alias
                          , Item.ItemClass1
                          , Item.ItemClass2
                          , Item.ItemClass3
                          , Item.Size
                          , Item.UnitWeight
                          , Item.Texture
                          , Item.Type
                          , Item.UnitCode
                          , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A011', Item.UnitCode)) AS UnitName
                          , Item.ColorCode
                    
                          --, ItemPrice.Price
                          --, ItemPrice.Cust_Alias
                    
                          , sd.Qty
                          , sd.UnitCode
                          , sd.UnitPrice
                          , sd.Price
                          , sd.VatPrice
                          , sd.TotalPrice
                          , sd.WonAmount
                          , sd.VatType
                          , sd.VatRate

                          , sm.SalesMan
                          , sm.WriteDate
                          , sm.WriteID
                          , sm.WriteName
                          , sd.Remark
                          , sd.HouseCode
                          , ch.Title as HouseName

                          , sd.SdOrderNo 
                          , sd.SdOrderSeq 
                    
                          , sd.CreId
                          , sd.CreIP
                          , sd.CreDt   
                    	  , sd.ModId
                          , sd.ModIP
                          , sd.ModDt
                    
                          --수주
                          , som.PONo
                          , som.ObtainDate
                          , sod.Remark AS SdOrder_Remark
                    
                    FROM BM_SalesD sd WITH(NOLOCK) 
                    LEFT JOIN BM_SalesM sm WITH(NOLOCK) ON sd.CorpId = sm.CorpId AND sd.SalesNo = sm.SalesNo
                    LEFT JOIN CM_Item Item WITH(NOLOCK) ON sd.CorpId = Item.CorpId AND sd.ItemCode = Item.ItemCode
                    LEFT JOIN CM_Cust             c ON sd.CorpId = c.CorpId AND c.CustCode = sm.BillToCode
                    LEFT JOIN CM_Cust            cd ON sd.CorpId = cd.CorpId AND cd.CustCode = sm.ShipToCode
                    LEFT JOIN CM_WareHouse       ch ON sd.CorpId = ch.CorpId AND ch.HouseID = sd.HouseCode
                    LEFT JOIN BM_SdOrderM som WITH(NOLOCK) ON sd.CorpId = som.CorpId AND sd.SdOrderNo = som.SdOrderNo 
                    LEFT JOIN BM_SdOrderD sod WITH(NOLOCK) ON sd.CorpId = sod.CorpId AND sd.SdOrderNo = sod.SdOrderNo AND sd.SdOrderSeq = sod.SdOrderSeq 
                        WHERE sd.CorpId = '{WrGlobal.CorpID}'
						  AND sm.SalesDate BETWEEN '{OutFrom}' AND '{OutTo}' 
                          ";


            if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] == "Y")
            {
                if (ShipToCode != "" && ShipToCode != null && ShipToCode.ToUpper() != "ALL")
                {
                    query += $"\nAND sm.ShipToCode = {ShipToCode}";
                }
            }

            if (CustCode != "" && CustCode != null && CustCode.ToUpper() != "ALL")
            {
                query += $"\nAND sm.BillToCode = '{CustCode}'";
            }

            if (ItemCode != "" && ItemCode != null && ItemCode.ToUpper() != "ALL")
            {
                query += $"\nAND sd.ItemCode = {ItemCode}";
            }

            if (outType != "")
            {
                query += $"\nAND sm.OutType = '{outType}' ";
            }

            query += $"\nORDER BY sd.SalesNo, sd.SalesSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_BM_SalesD_SalesNo()
        {
            //제품출고, 자재출고 둘 다 씀
            query = $@"
                        SELECT 
                               '' AS Status
                            , sd.SalesSeq
                            , sd.ItemCode ItemCode
                            , cm.ItemName
                            , cm.Alias
                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.Size
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            , cm.UnitCode
                            , cm.ColorCode

                            , sd.Qty
                            , sd.UnitCode AS SdUnitCode
                            , sd.UnitPrice
                            , sd.Price
                            , sd.VatPrice
                            , sd.TotalPrice
                            , sd.VatType
                            , sd.VatRate
                            , sd.Remark

                            , sd.CorpId
                            , sd.SalesNo
                            , sd.LotNo
                            , sd.BarCodeNo
                            , sd.SdOrderNo
                            , sd.SdOrderSeq
                            , sd.ReqType
                            , sd.HouseCode
                            , sd.WonAmount
                            , sd.DeliveryDate
                            , sd.CreId
                            , sd.CreIP
                            , sd.CreDt
                            , sd.ModId
                            , sd.ModIP
                            , sd.ModDt
                            , '' StoNo
                            , 0 StoSeq

                        FROM BM_SalesD sd
                        LEFT JOIN CM_Item cm 
                               ON sd.CorpId = cm.CorpId 
                              AND sd.ItemCode = cm.ItemCode
                        WHERE sd.CorpId = '{WrGlobal.CorpID}'
                          AND sd.SalesNo = '{SalesNo}'
                    ";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public DataTable Select_Sales_ExamRst(string startDate, string EndDate, string[] Item_Div = null, string exam_Div = "")
        {
            query = $@"SELECT 
                              sd.SalesNo AS RefNo
                            , sd.SalesSeq AS RefSeq
                            , CONVERT(DATE, sm.SalesDate) AS SalesDate

                            , sm.BillToCode
                            , bc.CustName BillToName
                            , sm.ShipToCode
                            , bcd.CustName AS ShipToName

                            , bi.ItemClass1
                            , bi.ItemClass2
                            , sd.ItemCode ItemCode
                            , bi.Alias
                            , bi.ItemName 
                            , bi.Size
                            , bi.Texture
                            , bi.UnitCode
                            
                            , sd.Qty AS ExamQty
                            , sd.Qty
                            , sd.Remark
                            , sd.CreId
                            , sd.CreIP
                            , sd.CreDt
                            , sd.ModId
                            , sd.ModIP
                            , sd.ModDt

                            , sd.SdOrderNo
                            , sd.SdOrderSeq
                            
                            --검사여부
                            , IIF(ap.ExamCnt > 0, 'Y', 'N') AS ExamYn

                            -- 전체 합격 여부 (모두 10이면 10, 아니면 20)
                            , CASE 
                                WHEN ap.ExamCnt = 0 THEN NULL          -- 검사내역 없으면(ExamYn='N') 전체합격여부는 NULL 처리
                                WHEN ap.NonPassCnt = 0 THEN '합격'
                                ELSE '불합격'
                              END AS AllPass

                         FROM BM_SalesD sd
                    LEFT JOIN BM_SalesM   sm  ON sd.CorpId = sm.CorpId AND sm.SalesNo       = sd.SalesNo
                    LEFT JOIN CM_Cust   bc  ON sd.CorpId = bc.CorpId AND bc.CustCode       = sm.BillToCode
                    LEFT JOIN CM_Cust   bcd ON sd.CorpId = bcd.CorpId AND bcd.CustCode      = sm.ShipToCode
                    LEFT JOIN CM_Item    bi  ON sd.CorpId = bi.CorpId AND bi.ItemCode       = sd.ItemCode

                    OUTER APPLY (
                        SELECT
                              COUNT(*) AS ExamCnt
                            , SUM(CASE WHEN er.PassDivCd <> '10' THEN 1 ELSE 0 END) AS NonPassCnt
                        FROM QM_ExamResult er
                        WHERE er.CorpId     = sd.CorpId
                          AND er.RefNo      = sd.SalesNo
                          AND er.RefSeq  = sd.SalesSeq
                          AND er.ExamDivCd = '{exam_Div}'
                    ) ap

                         WHERE sd.CorpId = '{WrGlobal.CorpID}'
                    AND sd.ReqType = '출하출고'
                    ";


            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND bi.ItemType IN(";
                for (int idx = 0; idx < Item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $"'{Item_Div[idx]}'";
                }
                query += ")";
            }

            if (SalesNo != "" && SalesSeq != 0)
            {
                query += $"\nAND sd.SalesNo = {SalesNo.ConvertStr_Empty_To_Null()}";
                query += $"\nAND sd.SalesSeq = {SalesSeq.ConvertInt_Empty_To_Null()}";
            }
            else
            {
                query += $"AND sm.SalesDate BETWEEN '{startDate}' AND '{EndDate}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_BM_SalesD_BySalesNo_Lup()
        {
            query = $@"
                        SELECT 
                                  sd.SalesNo
		                        , sd.SalesSeq

		                        , sd.ItemCode
                                , sd.ItemCode ItemName --
                                , cm.Alias
                                , cm.ItemClass1
                                , cm.ItemClass2
                                , cm.ItemClass3
                                , cm.Size
                                , cm.UnitWeight
                                , cm.Texture
                                , cm.Type
                                , cm.UnitCode
                                , cm.ColorCode

		                        , sd.Qty
		                        , sd.UnitPrice
		                        , sd.Price
		                        , sd.VatPrice
		                        , sd.TotalPrice
		                        , sd.WonAmount
		                        , sd.VatType
		                        , sd.VatRate

		                        , sd.HouseCode
		                        , sd.UnitCode
		                        , sd.Remark
		                        , sd.SdOrderNo
		                        , sd.SdOrderSeq
		                        , sd.HouseCode
		                        , sd.LotNo

		                        , sd.CreID
		                        , sd.CreIP
		                        , sd.CreDt
		                        , sd.ModID
		                        , sd.ModIP
		                        , sd.ModDt

		                        --Master
		                        , sm.SalesDate
		                        , sm.ShipToCode
		                        , sm.BillToCode

		                        , '' AS Del

                        FROM BM_SalesD sd
                        LEFT JOIN BM_SalesM sm On sm.CorpId = sd.CorpId AND sd.SalesNo = sm.SalesNo
                        LEFT JOIN CM_Item cm ON sd.CorpId = cm.CorpId AND sd.ItemCode = cm.ItemCode
                        WHERE sd.CorpID = '{WrGlobal.CorpID}'
                        AND sd.SalesNo = '{SalesNo}'
                        ORDER BY sd.SalesSeq DESC
            ";

            return db.GetDataTable(query);
        }

        public DataTable Select_MaterialOutView()
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();

            query = $@"SELECT SD.SalesSeq
                            , SalesDate OutDate
		                    , SD.ItemCode
                            , cm.ItemName
                            , cm.Alias
                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.Size
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            , cm.UnitCode
                            , cm.ColorCode


		                    , SD.Qty
                            , SD.UnitPrice
		                    , SD.Price
		                    , SD.VatPrice
		                    , SD.TotalPrice
		                    , SD.VatType
		                    , SD.VatRate
		                    , SD.HouseCode
                            , FORMAT(CONVERT(date, SM.SalesDate), 'yyyy-MM') AS OutMonth

                    FROM BM_SalesD SD
                    LEFT JOIN BM_SalesM SM ON SD.CorpId = SM.CorpId AND Sd.SalesNo = SM.SalesNo
                    LEFT JOIN CM_Item cm ON sd.CorpId = cm.CorpId and sd.ItemCode = cm.ItemCode
                    WHERE SD.CorpId = '{WrGlobal.CorpID}' 
                    AND SD.ReqType = '자재출고'
                    AND SM.SalesDate  BETWEEN '{stDate}' AND '{endDate}'
                    ";

            if (ItemCode != "NULL")
            {
                query += $@"AND SD.ItemCode = {ItemCode}";
            }

            query += "ORDER BY SalesSeq";

            return db.GetDataTable(query);
        }

        public DataTable SelectSalesList_Return(string startDate, string endDate, string shipToCode)
		{

            query = $@"
                    SELECT 
							  sm.ShipToCode		-- 출고처코드
							, sm.ShipToName		-- 출고처
							, sd.SalesNo		-- 출고번호
                            , Sd.SalesSeq		-- 출고순번

							, sd.ItemCode ItemCode	-- 품목코드
							, cm.ItemName	-- 품명
                            , cm.Alias
                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.Size
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            , cm.UnitCode
                            , cm.ColorCode

							, sd.Qty            -- 출고량
                            , 0 ReturnQty       -- 반품량
                            , ISNULL( (SELECT SUM(re.Qty) 
										  FROM BM_Return re 
										  WHERE re.CorpId = sd.CorpId 
										  AND re.SalesNo = sd.SalesNo 
										  AND re.SalesSeq = sd.SalesSeq), 0) AS TotalQty
							, sd.SdOrderNo	    -- 주문번호
							, sd.SdOrderSeq		-- 주문순번
							, cc.Address		-- 주소
                            , '' Reason
                            , GETDATE() AS ReturnDate
                            , 0 Price
                            , 'Y' StockYn
						FROM BM_SalesD sd

						LEFT JOIN BM_SalesM sm on sm.CorpId = sd.CorpId AND sm.SalesNo = sd.SalesNo
                        LEFT JOIN CM_Item cm on cm.CorpId = sd.CorpId AND cm.ItemCode = sd.ItemCode
						LEFT JOIN CM_Cust cc on cc.CorpId = sm.CorpId and cc.CustCode = sm.ShipToCode 
                        WHERE Sd.CorpId = '{WrGlobal.CorpID}' AND sd.status = '출고완료' AND sd.ReqType != '자재출고' --AND sd.ReqType = '출하출고'
                        AND sm.SalesDate BETWEEN '{startDate}' AND '{endDate}' 
                ";

            if (shipToCode != "ALL") query += $@" AND sm.ShipToCode = '{shipToCode}'";
            if (ItemCode != "ALL") query += $@" AND sd.ItemCode = '{ItemCode}'";

            return db.GetDataTable(query);
        }

        public bool InsertBM_SalesD()
		{
            query = $@"

            INSERT INTO BM_SalesD
            (
            CorpId
            , SalesNo
            , SalesSeq
            , ItemCode
            , UnitWeight
            , UnitCode
            , Qty
            , UnitPrice
            , Price
            , VatPrice
            , TotalPrice
            , WonAmount
            , VatType
            , VatRate
            , ReqType
            , SdOrderNo
            , SdOrderSeq
            , LotNo
            , BarCodeNo
            , Status
            , Remark
            , HouseCode
            , CreId
            , CreIP
            , CreDt
            )
            VALUES
            (
            '{CorpId}'
            , '{SalesNo}'
            , {SalesSeq}
            , {ItemCode.ConvertStr_Empty_To_Null()}
            , {UnitWeight.ConvertNumeric_Empty_To_Null()}
            , {UnitCode.ConvertStr_Empty_To_Null()}
            , {Qty.ConvertNumeric_Empty_To_Null()}
            , {UnitPrice.ConvertNumeric_Empty_To_Null()}
            , {Price.ConvertNumeric_Empty_To_Null()}
            , {VatPrice.ConvertNumeric_Empty_To_Null()}
            , {TotalPrice.ConvertNumeric_Empty_To_Null()}
            , {WonAmount.ConvertNumeric_Empty_To_Null()}
            , {VatType.ConvertStr_Empty_To_Null()}
            , {VatRate.ConvertNumeric_Empty_To_Null()}
            , {ReqType.ConvertStr_Empty_To_Null()}
            , {SdOrderNo.ConvertStr_Empty_To_Null()}
            , {SdOrderSeq.ConvertInt_Empty_To_Null()}
            , {LotNo.ConvertStr_Empty_To_Null()}
            , {BarCodeNo.ConvertStr_Empty_To_Null()}
            , {Status.ConvertStr_Empty_To_Null()}
            , {Remark.ConvertStr_Empty_To_Null()}
            , {HouseCode.ConvertStr_Empty_To_Null()}
            , '{WrGlobal.LoginID}'
            , '{WrGlobal.ClientHostName}'
            , getdate()
            )

            ";

            return db.ExecuteQuery(query);
		}

        public bool UpdateBM_SalesD()
        {
            query = $@"

            UPDATE BM_SalesD
            SET  
            UnitWeight   = {UnitWeight.ConvertNumeric_Empty_To_Null()}
            , UnitCode   = {UnitCode.ConvertStr_Empty_To_Null()}
            , Qty        = {Qty.ConvertNumeric_Empty_To_Null()}
            , UnitPrice  = {UnitPrice.ConvertNumeric_Empty_To_Null()}
            , Price      = {Price.ConvertNumeric_Empty_To_Null()}
            , VatPrice   = {VatPrice.ConvertNumeric_Empty_To_Null()}
            , TotalPrice = {TotalPrice.ConvertNumeric_Empty_To_Null()}
            , WonAmount  = {WonAmount.ConvertDecimal_Empty_To_Null()}
            , VatType    = {VatType.ConvertStr_Empty_To_Null()}
            , VatRate    = {VatRate.ConvertNumeric_Empty_To_Null()}
            , ReqType    = {ReqType.ConvertStr_Empty_To_Null()}
            , SdOrderNo  = {SdOrderNo.ConvertStr_Empty_To_Null()}
            , SdOrderSeq = {SdOrderSeq.ConvertNumeric_Empty_To_Null()}
            , HouseCode     = {HouseCode.ConvertStr_Empty_To_Null()}
            , Status     = {Status.ConvertStr_Empty_To_Null()}
            , Remark     = {Remark.ConvertStr_Empty_To_Null()}
            , ModId      = '{WrGlobal.LoginID}'
            , ModIP      = '{WrGlobal.ClientHostName}'
            , ModDt      = GETdATe()
            WHERE CorpId  = '{CorpId}'
            AND SalesNo   = '{SalesNo}'
            AND SalesSeq  = '{SalesSeq}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool DeleteBM_SalesD()
        {
            query = $@"

            DELETE BM_SalesD
            WHERE CorpId  = '{CorpId}'
            AND SalesNo   = '{SalesNo}'
            AND SalesSeq   = '{SalesSeq}';

            IF NOT EXISTS (SELECT 1 
                            FROM BM_SalesD
                            WHERE CorpId = '{CorpId}'
                            AND SalesNo = '{SalesNo}' 
                        )
            BEGIN 
                DELETE BM_SalesM WHERE CorpId = '{CorpId}'
                                    AND SalesNo = '{SalesNo}' 
            END
            ";

            return db.ExecuteQuery(query);
        }

        public bool DeletebySalesNo()
        {
            query = $@"

            DELETE BM_SalesD
            WHERE CorpId  = '{CorpId}'
            AND SalesNo   = '{SalesNo}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool DeleteSalesMD_Work(string barcodeNo)
        {
            query = $@"

                    DECLARE @TB TABLE
                    (
                    	SalesNo nvarchar(20)
                    )
                    
                    INSERT @TB
                    SELECT SalesNo
                    FROM BM_SalesD 
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND ReqType  = '자재출고'
                    AND BarCodeNo  = '{barcodeNo}'
                    
                    DELETE BM_SalesD WHERE CorpId = '{WrGlobal.CorpID}' AND SalesNo IN (SELECT SalesNo FROM @TB)
                    DELETE BM_SalesM WHERE CorpId = '{WrGlobal.CorpID}' AND SalesNo IN (SELECT SalesNo FROM @TB)
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
