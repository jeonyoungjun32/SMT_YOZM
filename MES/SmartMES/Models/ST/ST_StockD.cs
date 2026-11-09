using DevExpress.CodeParser;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.Models.ST
{
	class ST_StockD
    {
		#region Property

		public string CorpId { get; set; }

        public string InOutNo { get; set; }

        public int Seq { get; set; }
        public int DetailSeq { get; set; }

        public string CustCode { get; set; }

        public string InOutDate { get; set; }

        public string Type { get; set; }

        public string ItemCode { get; set; }

        public string LotNo { get; set; }

        public string FromHouseCode { get; set; }

        public string ToHouseCode { get; set; }

        public string JobNo { get; set; }

        public int? JobSeq { get; set; }

        public int? JobSeq2 { get; set; }

        public string FaultCode { get; set; }

        public decimal? Qty { get; set; }

        public decimal? OutQty { get; set; }

        public string WriteID { get; set; }

        public string CloseYn { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        public string Status { get; set; }

        public string HouseCode { get; set; }

        public string MmOrderNo { get; set; }
        public int  MmOrderSeq { get; set; }
        public string PackagingCode { get; set; }

        public string[] ItemType { get; set; }
        public string WareHouseCode { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_Stock_MonthlyMaterial(string stockDiv, string Date, string[] ItemType, string houseID)
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();

            query = $@"
                    SELECT 
                      A.WareHouseCode
                    , CH.Title AS Warehouse
                    , A.ItemCode
                    , CM.ItemName
                    , CM.ItemType
                    , CM.UnitCode
                    , ROUND(SUM(A.InQty), 2) AS InQty
                    , ROUND(SUM(A.OutQty), 2) AS OutQty
                    , ROUND(SUM(A.AuditQty), 2) AS AuditQty
                    FROM     
                    (    
		                    --입고
		                    SELECT  SM.ToHouseCode AS WareHouseCode, SD.ItemCode AS ItemCode, ISNULL(SUM(SD.Qty), 0) AS InQty, 0 AS OutQty, 0 AS AuditQty
		                    FROM dbo.ST_StockM AS SM 
		                    INNER JOIN dbo.ST_StockD AS SD ON SM.CorpId = SD.CorpId AND SM.InOutNo = SD.InOutNo
		                    WHERE  (SM.ToHouseCode IS NOT NULL) AND (SM.ToHouseCode <> '') AND InOutDate <= '{Date}'
		                    GROUP BY SM.ToHouseCode, SD.ItemCode, SD.PackagingCode

		                    UNION ALL

		                    --출고
		                    SELECT   SM.FromHouseCode AS WareHouseCode, SD.ItemCode AS ItemCode, 0 AS InQty, ISNULL(SUM(SD.Qty), 0) AS OutQty, 0 AS AuditQty
		                    FROM     dbo.ST_StockM AS SM 
		                    INNER JOIN dbo.ST_StockD AS SD ON SM.CorpId = SD.CorpId AND SM.InOutNo = SD.InOutNo
		                    WHERE  (SM.FromHouseCode IS NOT NULL) AND (SM.FromHouseCode <> '') AND InOutDate <= '{Date}'
		                    GROUP BY SM.FromHouseCode, SD.ItemCode, SD.PackagingCode

		                    UNION ALL

		                    --확인재고
		                    SELECT   AM.HouseCode AS WareHouseCode, AD.ItemCode AS ItemCode, 0 AS InQty,  0 AS OutQty, ISNULL(SUM(AD.Qty), 0) AS AuditQty
		                    FROM dbo.AuditMaster AS AM 
		                    INNER JOIN dbo.AuditDetail AS AD ON AM.AuditNo = AD.AuditNo 
                            WHERE CONVERT(DATE, LEFT(AD.AuditNo,6), 112) <= '{Date}'
		                    GROUP BY AM.HouseCode, AD.ItemCode, AD.PackagingCode
                    ) AS A 
                    LEFT OUTER JOIN dbo.CM_Item AS CM ON CM.CorpId = '{WrGlobal.CorpID}' AND cm.ItemCode = A.ItemCode 
                    LEFT OUTER JOIN dbo.CM_Warehouse AS CH ON CH.CorpId = '{WrGlobal.CorpID}' AND CH.HouseID = A.WareHouseCode 
                    LEFT OUTER JOIN dbo.CM_CateItem AS CI ON CI.CorpId = 'Z000' AND CI.CLASS = 'A010' AND ci.ClassItemCode = CM.ItemType
                    WHERE CM.ItemType IN (";
            int idx = 0;

            foreach (string item in ItemType)
            {
                if (idx != 0)
                {
                    query += ", ";
                }

                query += $@"'{item}'";
                idx++;
            }
            query += ")";

            if (ItemCode != "NULL")
            {
                query += $"\nAND A.ItemCode = {ItemCode}";
            }
            if (houseID != "NULL")
            {
                query += $"\nAND A.WareHouseCode IN ( {houseID})";
            }

            query += "\n GROUP BY A.WareHouseCode, CH.Title, CM.ItemType, A.ItemCode, CM.ItemName, CM.UnitCode";

            return db.GetDataTable(query);
        }

        public DataTable SelectStock_Lot(string type)
        {
            query = $@"SELECT 
                        	'' AS Status, 
                        	cm.ItemCode, 
                        	cm.ItemName, 
                        	cm.ItemType, 
                        	--CAST(ISNULL(SUM(IIF(st.StockQty > 0, st.StockQty, st.StockQty * -1)), 0) AS INT) AS StockQty, 
                            ISNULL(st.StockQty, 0) AS StockQty, 
                            LotNo,
                            st.WareHouseCode AS Stock_Cd
                        FROM StockListView_Lot st 
						LEFT JOIN CM_Item cm ON cm.CorpId = st.CorpId AND cm.ItemCode = st.ItemCode 
                        LEFT JOIN CM_Warehouse CW ON st.WareHouseCode = CW.HouseID
						WHERE st.LotNo <> ''
            ";

            if (type == "제품") query += $@"AND cm.ItemType IN ('10', '20', '30')";
            else if (type == "자재") query += $@"AND cm.ItemType IN ('40', '50', '60', '70')";

            if (ItemCode != "ALL") query += $@" AND cm.ItemCode = '{ItemCode}'";
            if (HouseCode != "ALL") query += $"\n AND st.WareHouseCode = '{HouseCode}'";
            if (LotNo != "") query += $"\n AND LotNo LIKE '{LotNo}%'";

            query += " ORDER BY LotNo, cm.ItemType, cm.ItemCode";

            return db.GetDataTable(query);
        }

        public DataTable SelectStockInOutList(string startDate, string endDate, string wareHouseCode, string[] itemDiv)
		{
            ItemCode = ItemCode.ConvertString_Null_To_Empty();

            query = $@"
                        select 
                            A.*, 
                            B.ItemName, 
                            B.ItemType, 
                            C.Title, -- 창고명
							D.CalcQty,   -- 기준수
                            A.Qty / ISNULL(D.CalcQty, 1) AS QtyKg -- 실적수량 기반 Kg 환산
                        from  StockInOutView A
                            INNER JOIN CM_Item B ON A.ItemCode = B.ItemCode
                            LEFT JOIN CM_Warehouse C ON A.WarehouseCode = C.HouseID
                            LEFT JOIN PM_SdRoute D ON A.JobNo = D.OrderNo AND A.ItemCode = D.MItemCode 
                        where A.CorpId = '{WrGlobal.CorpID}'
                        AND A.WareHouseCode != ''
                        AND A.InOutDate between '{startDate}' and '{endDate}'
                    ";

			if (ItemCode != "" && ItemCode != "ALL") query += $"\n AND A.ItemCode = '{ItemCode}'";
			if (wareHouseCode != "" && wareHouseCode != "ALL") query += $"\n AND A.WarehouseCode = '{wareHouseCode}'";

			//특정 구분만 보고 싶은 경우
			if (itemDiv != null && itemDiv.Length > 0)
			{
				query += " AND (";
				for (int idx = 0; idx < itemDiv.Length; idx++)
				{
					if (idx != 0)
					{
						query += " OR";
					}

					query += $@" B.ItemType = '{itemDiv[idx]}'";
				}

				query += ")";
			}

            query += $"\nORDER BY InOutNo, Seq";
            DataTable result = db.GetDataTable(query);

            // 재고 처리용 내부 표식은 유지하고 입출고현황의 비고는 공란으로 표시
            foreach (DataRow row in result.Rows)
                row["Remark"] = "";

            return result;
		}

        public DataTable Select_StockAdjList(string type)
        {
            query = $@"
                        SELECT '' AS Status
                        , st.CorpId
                        , st.InOutNo
                        , st.Seq
                        , st.CustCode
                        , st.InOutDate
                        , st.Type

                        , st.ItemCode
                        , cm.ItemName
                        , cm.ItemType
                        , cm.Size
                        , cm.ItemClass1
                        , cm.ItemClass2
                        , cm.ItemClass3

                        , st.DetailSeq
                        , st.LotNo
                        , st.FromHouseCode
                        , st.ToHouseCode
                        , st.JobNo
                        , st.JobSeq
                        , st.JobSeq2
                        , st.PackagingCode
                        , st.FaultCode
                        , st.Qty
                        , st.OutQty
                        , st.WriteID
                        , st.CloseYn
                        , st.Remark
                        , st.CreId
                        , st.CreIP
                        , st.CreDt
                        , st.ModId
                        , st.ModIP
                        , st.ModDt

                        FROM ST_StockD st
                        INNER JOIN ST_StockM sm ON st.CorpId = sm.CorpId AND st.InOutNo = sm.InOutNo
                        LEFT JOIN CM_Item cm ON st.CorpId = cm.CorpId AND st.ItemCode = cm.ItemCode
                        WHERE st.Type = '조정'
            ";

            if (type == "제품") query += $@"AND cm.ItemType IN ('10', '20', '30')";
            else if (type == "자재") query += $@"AND cm.ItemType IN ('40', '50', '60', '70')";

            if (ItemCode != "ALL") query += $@" AND st.ItemCode = '{ItemCode}'";
            if (HouseCode != "ALL") query += $"\n AND st.ToHouseCode = '{HouseCode}'";

            query += $@"Order by ItemType, ToHouseCode";

            return db.GetDataTable(query);
        }

        public DataTable SelectStockByProdLot()
        {
            query = $@"SELECT
                        	'' AS Status
							, st.WareHouseCode AS Stock_Cd
							, st.WareHouse
                        	, st.ItemCode
							, st.LotNo AS SerialNo
							, ISNULL(st.StockQty, 0) AS StockQty
                            , CAST(0 AS DECIMAL(18,3)) AS OutQty
							, odr.LotNo
							, odr.WorkDate

                        	--CAST(ISNULL(SUM(IIF(st.StockQty > 0, st.StockQty, st.StockQty * -1)), 0) AS INT) AS StockQty,
                        FROM StockListView_Lot st
						LEFT JOIN PM_OrderResult odr ON odr.CorpId = st.CorpId AND odr.ItemCode = st.ItemCode AND odr.SerialNo = st.LotNo
                        WHERE st.CorpId = '{WrGlobal.CorpID}'
						 AND st.LotNo <> ''
                         AND st.ItemCode = '{ItemCode}'
            ";

            //if (HouseCode != "ALL") query += $"\n AND st.WareHouseCode = '{HouseCode}'";
            // if (LotNo != "") query += $"\n AND LotNo LIKE '{LotNo}%'";

            query += " ORDER BY st.LotNo";

            return db.GetDataTable(query);
        }

        public DataTable SelectBM_SalesD()
        {
            query = $@"	SELECT  ST.Seq
		                		, SD.HouseCode
		                		, SD.ItemCode
		                		, ST.LotNo
		                		, ST.Qty
		                		, SD.UnitPrice

		                FROM ST_StockD ST
		                LEFT JOIN BM_SalesD SD ON ST.CorpId = SD.CorpId AND ST.JobNo = SD.SalesNo AND ST.JobSeq = SD.SalesSeq 
                        WHERE ST.CorpId = '{WrGlobal.CorpID}'
                         AND ST.JobNo = '{JobNo}'
                         AND ST.ItemCode = '{ItemCode}'
            ";

            query += " ORDER BY ST.Seq";

            return db.GetDataTable(query);
        }

        public DataTable SelectBM_SalesD_Return()
        {
            query = $@"	SELECT  ST.Seq
		                		, SD.HouseCode
		                		, SD.ItemCode
		                		, ST.LotNo
		                		, ST.Qty - ISNULL(RT.ReturnQty, 0) AS Qty
		                		, SD.UnitPrice
                                , '' Status
                                , 0 ReturnQty

		                FROM ST_StockD ST
		                LEFT JOIN BM_SalesD SD ON ST.CorpId = SD.CorpId AND ST.JobNo = SD.SalesNo AND ST.JobSeq = SD.SalesSeq 
                        LEFT JOIN (
                            SELECT CorpId, LotNo, ItemCode, SUM(Qty) AS ReturnQty
                            FROM ST_StockD
                            WHERE Type = '반품'
                            GROUP BY CorpId, LotNo, ItemCode
                        ) RT ON ST.CorpId = RT.CorpId AND ST.LotNo = RT.LotNo AND ST.ItemCode = RT.ItemCode
                        WHERE ST.CorpId = '{WrGlobal.CorpID}'
                         AND ST.JobNo = '{JobNo}'
                         AND ST.ItemCode = '{ItemCode}'
                         AND (ST.Qty - ISNULL(RT.ReturnQty, 0)) > 0
            ";

            query += " ORDER BY ST.Seq";

            return db.GetDataTable(query);
        }

        public DataTable Select_ST_MonthlyMaterial(string stockDiv, string Date)
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            WareHouseCode = WareHouseCode.ConvertStr_Empty_To_Null();

            query = $@"
                    SELECT 
                          A.WareHouseCode
                        , CH.Title AS Warehouse
                        , A.ProductCode
                        , CM.ItemName
                        , CM.ItemType
                        , CM.UnitCode
                        , ROUND(SUM(A.InQty), 2) AS InQty
                        , ROUND(SUM(A.OutQty), 2) AS OutQty
                    FROM     
                    (    
		                    --입고
		                    SELECT  SM.ToHouseCode AS WareHouseCode, SD.ItemCode AS ProductCode, ISNULL(SUM(SD.Qty), 0) AS InQty, 0 AS OutQty, 0 AS AuditQty
		                    FROM dbo.ST_StockM AS SM 
		                    INNER JOIN dbo.ST_StockD AS SD ON SM.CorpId = SD.CorpId AND SM.InOutNo = SD.InOutNo
		                    WHERE  (SM.ToHouseCode IS NOT NULL) AND (SM.ToHouseCode <> '') AND InOutDate <= '{Date}'
		                    GROUP BY SM.ToHouseCode, SD.ItemCode, SD.PackagingCode

		                    UNION ALL

		                    --출고
		                    SELECT   SM.FromHouseCode AS WareHouseCode, SD.ItemCode AS ProductCode, 0 AS InQty, ISNULL(SUM(SD.Qty), 0) AS OutQty, 0 AS AuditQty
		                    FROM     dbo.ST_StockM AS SM 
		                    INNER JOIN dbo.ST_StockD AS SD ON SM.CorpId = SD.CorpId AND SM.InOutNo = SD.InOutNo
		                    WHERE  (SM.FromHouseCode IS NOT NULL) AND (SM.FromHouseCode <> '') AND InOutDate <= '{Date}'
		                    GROUP BY SM.FromHouseCode, SD.ItemCode, SD.PackagingCode

                    ) AS A 
                    LEFT OUTER JOIN dbo.CM_Item AS CM ON CM.CorpId = '{WrGlobal.CorpID}' AND CM.ItemCode = A.ProductCode 
                    LEFT OUTER JOIN dbo.CM_Warehouse AS CH ON CH.CorpId = '{WrGlobal.CorpID}' AND CH.HouseID = A.WareHouseCode 
                    LEFT OUTER JOIN dbo.CM_CateItem AS CI ON CI.CorpId = 'Z000' AND CI.CLASS = 'A010' AND CI.ClassItemCode = CM.ItemType
                    WHERE CM.ItemType IN (";
            int idx = 0;

            foreach (string item in ItemType)
            {
                if (idx != 0)
                {
                    query += ", ";
                }

                query += $@"'{item}'";
                idx++;
            }
            query += ")";

            if (ItemCode != "NULL")
            {
                query += $"\nAND A.ProductCode = {ItemCode}";
            }
            if (WareHouseCode != "NULL")
            {
                query += $"\nAND A.WareHouseCode IN ( {WareHouseCode})";
            }

            query += "\n GROUP BY A.WareHouseCode, CH.Title, CM.ItemType, A.ProductCode, CM.ItemName, CM.UnitCode";

            return db.GetDataTable(query);
        }

        public DataTable SelectStockBase()
		{
            ToHouseCode = ToHouseCode.ConvertString_Null_To_Empty();
            ItemCode = ItemCode.ConvertString_Null_To_Empty();

            query = $@"
                SELECT 
                    stm.OutDate
                    , std.CorpId
                    , std.InOutNo
                    , std.Seq
                    , std.CustCode
                    , std.InOutDate
                    , std.Type

                    , std.ItemCode
                    , IT.ItemName
                    , IT.Size
                    , cci.Title AS ItemTypeName

                    , std.DetailSeq
                    , std.LotNo
                    , std.FromHouseCode
                    , std.ToHouseCode
                    , std.JobNo
                    , std.JobSeq
                    , std.JobSeq2
                    , std.PackagingCode
                    , std.FaultCode
                    , std.Qty
                    , std.OutQty
                    , std.WriteID
                    , std.CloseYn
                    , std.Remark
                    , std.CreId
                    , std.CreIP
                    , std.CreDt

                FROM ST_StockD STD
                INNER JOIN ST_StockM STM ON STD.CorpId = STM.CorpId AND STD.InOUtNo = STM.InOutNo
                LEFT JOIN CM_ITEM IT ON STD.CorpId = IT.CorpId AND STD.ItemCode = IT.ItemCode
                LEFT JOIN CM_CateItem cci ON STD.CorpId = cci.CorpId AND cci.CLASS = 'A010' AND it.ItemType = cci.ClassItemCode

                WHERE STD.CorpId = '{WrGlobal.CorpID}'
                AND std.Type = '조정'
                AND ISNULL(std.Remark, '') = '기초'
                ";

            if(ToHouseCode != "" && ToHouseCode.ToUpper() != "ALL")
			{
                query += $"\nAND std.ToHouseCode = '{ToHouseCode}'";
			}

            if (ItemCode != "" && ItemCode.ToUpper() != "ALL")
            {
                query += $"\nAND std.ItemCode = '{ItemCode}'";
            }

            return db.GetDataTable(query);
		}

        public bool Ins_ST_StockD()
        {
            query = $@"INSERT INTO ST_StockD
                        (
                            CorpId
                            , InOutNo
                            , Seq
                            , CustCode
                            , InOutDate
                            , Type
                            , ItemCode
                            , DetailSeq
                            , LotNo
                            , FromHouseCode
                            , ToHouseCode
                            , JobNo
                            , JobSeq
                            , JobSeq2
                            , PackagingCode
                            , FaultCode
                            , Qty
                            , OutQty
                            , WriteID
                            , CloseYn
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , Status
                        )
                        VALUES(
                                   '{WrGlobal.CorpID}',
                                   '{InOutNo}',
                                   '{Seq}',
                                   '{CustCode}',
                                   '{InOutDate}',

                                   '{Type}',
                                   '{ItemCode}',
                                   {DetailSeq.ConvertInt_Empty_To_Null()},
                                   '{LotNo}',
                                   '{FromHouseCode}',
                                   '{ToHouseCode}',

                                   '{JobNo}',
                                   '{JobSeq}',
                                   '{JobSeq2}',
                                    {PackagingCode.ConvertStr_Empty_To_Null()},
                                   '{FaultCode}',

                                   '{Qty}',
                                   '{OutQty.ConvertInt_Null_To_Zero()}',
                                   '{WriteID}',
                                   '{CloseYn}',
                                   '{Remark}',
                                   '{WrGlobal.LoginID}',
                                   '{WrGlobal.ClientHostName}',
                                    GETDATE(),
                                   '{Status}')";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Qty_ByDefect()
        {
            query = $@"
                UPDATE ST_StockD
                SET Qty = '{Qty}'
                WHERE CorpId = '{CorpId}'
                AND JobNo = '{JobNo}'
                AND JobSeq = '{JobSeq}'
                AND JobSeq2 = '{JobSeq2}'
                AND Type = '{Type}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Qty_ByDefect_JobNo()
        {
            query = $@"
                UPDATE ST_StockD
                SET Qty = '{Qty}'
                WHERE CorpId = '{CorpId}'
                AND JobNo = '{JobNo}'
                ";

            return db.ExecuteQuery(query);

        }

        public bool Upd_StockAdj()
        {
            query = $@"Update ST_StockD
                        SET Qty = '{Qty}'
                            , Remark = '{Remark}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt =  GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND InOutNo = '{InOutNo}'
                        AND Seq = '{Seq}'   
                    ";

            return db.ExecuteQuery(query);
        }

        public bool DeleteStockMD_JobNo()
        {
            query = $@"

                    DECLARE @TB TABLE
                    (
                    	InOutNo nvarchar(20)
                    )
                    
                    INSERT @TB
                    SELECT InOutNo
                    FROM ST_StockD 
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND JobNo = '{JobNo}'
                    
                    DELETE ST_StockD WHERE InOutNo IN (SELECT InOutNo FROM @TB)
                    DELETE ST_StockM WHERE InOutNo IN (SELECT InOutNo FROM @TB)

                    ";

            return db.ExecuteQuery(query);
        }

        public bool DeleteStockMD_Work()
        {
            query = $@"

                    DECLARE @TB TABLE
                    (
                    	InOutNo nvarchar(20)
                    )
                    
                    INSERT @TB
                    SELECT InOutNo
                    FROM ST_StockD 
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND JobNo = '{JobNo}'
                    AND JobSeq = {JobSeq}
                    AND JobSeq2 = {JobSeq2}
                    
                    DELETE ST_StockD WHERE InOutNo IN (SELECT InOutNo FROM @TB)
                    DELETE ST_StockM WHERE InOutNo IN (SELECT InOutNo FROM @TB)
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_StockMD()
        {
            query = $@"
                DELETE FROM ST_StockD 
                WHERE CorpID = '{WrGlobal.CorpID}'
                AND InOutNo = '{InOutNo}' 
                AND Seq = {Seq};

                DELETE FROM ST_StockM
                WHERE CorpID = '{WrGlobal.CorpID}'
                AND InOutNo = '{InOutNo}'
                AND NOT EXISTS (
                    SELECT 1 
                    FROM ST_StockD 
                    WHERE CorpID = '{WrGlobal.CorpID}' 
                    AND InOutNo = '{InOutNo}'
                );
            ";

            return db.ExecuteQuery(query);
        }
    }
}
