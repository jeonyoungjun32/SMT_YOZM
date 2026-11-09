using DevExpress.DataAccess.Sql;
using SmartCommon.Common;
using System;
using System.Data;

namespace SmartMES.Models.ST
{
	class ST_StockM
    {
        public string CorpId { get; set; }

        public string InOutNo { get; set; }

        public string OutDate { get; set; }

        public string CustCode { get; set; }

        public string FromHouseCode { get; set; }

        public string ToHouseCode { get; set; }

        public string WriteID { get; set; }

        public DateTime? WriteDate { get; set; }

        public string DeptCode { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        public string Status { get; set; }

        Database db = new Database();
        string query = "";

        public bool Ins_ST_StockM()
        {
            query = $@"INSERT INTO ST_StockM
                        (
                              CorpId
                            , InOutNo
                            , OutDate
                            , CustCode
                            , FromHouseCode
                            , ToHouseCode
                            , WriteID
                            , WriteDate
                            , DeptCode
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                            , Status
                        )
                                    VALUES(
                                   '{WrGlobal.CorpID}',
                                   '{InOutNo}',
                                   '{OutDate}',
                                   '{CustCode}',
                                   '{FromHouseCode}',
                                   '{ToHouseCode}',
                                   '{WriteID}',
                                    GETDATE(),
                                   null,
                                   '{Remark}',
                                   '{WrGlobal.LoginID}',
                                   '{WrGlobal.ClientHostName}',
                                    GETDATE(),
                                   null,
                                   null,
                                   null,
                                   '{Status}')";

            return db.ExecuteQuery(query);
        }

        public DataTable Select_Stock_InOutWaste(string ItemCode, string startDate = null, string endDate = null)
        {
            query = $@"
                        SELECT
	                        SM.CorpId
	                        , SM.InOutNo
	                        , SD.Seq
	                        , SM.OutDate
	                        , SD.FromHouseCode
	                        , SD.ToHouseCode
	                        , SD.InOutDate
	                        , IIF(SD.FromHouseCode IS NULL AND SD.ToHouseCode IS NOT NULL, '', SD.Type) Type
	                        , SD.ItemCode
                            , CMM.ItemName
	                        , OP.MachineCode
                            , CM.MachineName
	                        , SD.Qty / 1000 Qty
	                        , SM.Remark
							, CASE
								WHEN SD.FromHouseCode IS NULL AND SD.ToHouseCode IS NOT NULL THEN '입고'
								WHEN SD.FromHouseCode IS NOT NULL AND SD.ToHouseCode IS NULL THEN '출고'
							END Division
                        FROM ST_StockM SM
                        INNER JOIN ST_StockD SD ON SM.CorpId = SD.CorpId AND SM.InOutNo = SD.InOutNo
                        LEFT JOIN CM_Item CMM ON SD.ItemCode = CMM.ItemCode
                        LEFT JOIN PM_OrderResult ORR ON SD.JobNo = ORR.SerialNo
						LEFT JOIN PM_OrderProcess OP ON ORR.OrderNo = OP.OrderNo AND ORR.OrderSeq = OP.OrderSeq
						LEFT JOIN EQ_Machine CM ON OP.MachineCode = CM.MachineCode
                        WHERE SM.CorpId = '{WrGlobal.CorpID}'
                        AND CMM.ItemType = '70'";

            if (ItemCode != "ALL")
            {
                query += $@"AND SD.ItemCode = '{ItemCode}'";
            }

            if (startDate != null && endDate != null)
            {
                query += $@"AND SD.InOutDate BETWEEN '{startDate}' AND '{endDate}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Stock_OutWaste(string ItemCode)
        {
            query = $@"
                        SELECT TOP 0
                            '' AS Status
	                        , SM.CorpId
	                        , SM.InOutNo
	                        , SD.Seq
	                        , SM.OutDate
	                        , SM.FromHouseCode
	                        , SM.ToHouseCode
	                        , SD.InOutDate
	                        , SD.Type
	                        , SD.ItemCode
                            , CMM.ItemName
	                        , OP.MachineCode
                            , CM.MachineName
	                        , IIF(SD.OutQty is null, SD.Qty, SD.OutQty) / 1000 Qty
                            , CMP.SalesPrice UnitPrice
							, CMP.SalesPrice * (IIF(SD.OutQty is null, SD.Qty, SD.OutQty) / 1000) Price
	                        , SM.Remark
                            , IIF(SD.OutQty is null, '입고', '출고') Division
                        FROM ST_StockM SM
                        INNER JOIN ST_StockD SD ON SM.CorpId = SD.CorpId AND SM.InOutNo = SD.InOutNo
                        LEFT JOIN CM_Item CMM ON SD.ItemCode = CMM.ItemCode
                        LEFT JOIN PM_OrderProcess OP ON SD.JobNo = OP.OrderNo AND SD.JobSeq = OP.OrderSeq
                        LEFT JOIN EQ_Machine CM ON OP.MachineCode = CM.MachineCode
                        LEFT JOIN (SELECT Top 1 * FROM CM_ItemPrice WHERE ItemCode = '{ItemCode}') CMP ON SD.CustCode = CMP.ItemCode
                        WHERE SD.ItemCode = '{ItemCode}'
                        AND SD.OutQty IS NOT NULL
                    ";

            return db.GetDataTable(query);
        }

   
        public DataTable Select_WasteInOutView(string ItemType, string ItemCode, string startDate, string endDate)
        {
            query = $@"
                        SELECT
	                        B.CorpId
	                        , B.InOutDate
	                        , IIF(B.InQty IS NULL, '', B.ItemCode) InItemCode
	                        , IIF(B.InQty IS NULL, '', B.ItemName) InItemName
	                        , B.InQty
	                        , IIF(B.OutQty IS NULL, '', B.ItemCode) OutItemCode
	                        , IIF(B.OutQty IS NULL, '', B.ItemName) OutItemName
	                        , B.OutQty
                        FROM
	                        (
		                        SELECT
			                        A.CorpId
			                        , A.InOutDate
			                        , A.ItemCode
			                        , A.ItemName
			                        , SUM(A.InQty) InQty
			                        , SUM(A.OutQty) OutQty
		                        FROM
			                        (
				                        SELECT
					                        SM.CorpId
					                        , SM.InOutNo
					                        , CONVERT(CHAR(10), SD.InOutDate, 23) InOutDate
					                        , SD.ItemCode
					                        , CM.ItemName
					                        , IIF(SD.FromHouseCode IS NULL AND SD.ToHouseCode IS NOT NULL, SD.Qty / 1000, null) AS InQty
					                        , IIF(SD.FromHouseCode IS NOT NULL AND SD.ToHouseCode IS NULL, SD.Qty / 1000, null) AS OutQty
				                        FROM ST_StockM SM
				                        INNER JOIN ST_StockD SD ON SM.CorpId = SD.CorpId AND SM.InOutNo = SD.InOutNo
				                        LEFT JOIN CM_Item CM ON SD.ItemCode = cm.ItemCode
				                        WHERE SM.CorpId = '{WrGlobal.CorpID}'
				                        AND CM.ItemType = '70'";

            if (ItemCode != "ALL")
            {
                query += $@"AND SD.ItemCode = '{ItemCode}'";
            }

            if (startDate != null && endDate != null)
            {
                query += $@"AND SD.InOutDate BETWEEN '{startDate}' AND '{endDate}'";
            }

            query += $@"
			                        ) A
		                        GROUP BY A.CorpId, A.InOutDate, A.ItemCode, A.ItemName
	                        ) B
                    ";



            return db.GetDataTable(query);
        }

        public DataTable Select_MaterialInOutView(string ItemCode, string startDate, string endDate)
        {
            query = $@"
                        SELECT
	                        B.CorpId
	                        , B.InOutDate
	                        , IIF(B.InQty IS NULL, '', B.ItemCode) InItemCode
	                        , IIF(B.InQty IS NULL, '', B.ItemName) InItemName
	                        , B.InQty
	                        , IIF(B.OutQty IS NULL, '', B.ItemCode) OutItemCode
	                        , IIF(B.OutQty IS NULL, '', B.ItemName) OutItemName
	                        , B.OutQty
                        FROM
	                        (
		                        SELECT
			                        A.CorpId
			                        , A.InOutDate
			                        , A.ItemCode
			                        , A.ItemName
			                        , SUM(A.InQty) InQty
			                        , SUM(A.OutQty) OutQty
		                        FROM
			                        (
				                        SELECT
					                        SM.CorpId
					                        , SM.InOutNo
                                            , SD.Seq
					                        , CONVERT(CHAR(10), SD.InOutDate, 23) InOutDate
					                        , SD.ItemCode
					                        , CM.ItemName
					                        , IIF(SD.OutQty IS NULL, SD.Qty, null) AS InQty
					                        , IIF(SD.OutQty IS NOT NULL, SD.OutQty, null) AS OutQty
				                        FROM ST_StockM SM
				                        INNER JOIN ST_StockD SD ON SM.CorpId = SD.CorpId AND SM.InOutNo = SD.InOutNo
				                        LEFT JOIN CM_Item CM ON SD.ItemCode = cm.ItemCode
				                        WHERE SM.CorpId = '{WrGlobal.CorpID}'
				                        AND CM.ItemType IN ('40', '50')";

            if (ItemCode != "ALL")
            {
                query += $@"AND SD.ItemCode = '{ItemCode}'";
            }

            if (startDate != null && endDate != null)
            {
                query += $@"AND SD.InOutDate BETWEEN '{startDate}' AND '{endDate}'";
            }

            query += $@"
			                        ) A
		                        GROUP BY A.CorpId, A.InOutDate, A.ItemCode, A.ItemName
	                        ) B
                    ";



            return db.GetDataTable(query);
        }

        public bool Stock_MakeStockBySales(string corpId, string type, string salesNo)
		{
            query = $@"EXEC [Stock_MakeStockBySales] '{corpId}', '{type}', '{salesNo}'";

            return db.ExecuteQuery(query);
		}
        public DataTable Select_StockList(string Type)
        {
            query = $@"SELECT * FROM StockListView
                        WHERE 1=1  --항상 참인 조건으로 시작  
                        AND (   ('{Type}' = '제품' AND ItemType IN ('10','20','30'))
                                OR
                                ('{Type}' = '자재' AND ItemType NOT IN('10','20','30'))                      
                                 OR
                                ('{Type}' = '공정'))
                        ";


            return db.GetDataTable(query);
        }
        public bool Ins_ST_StockM_StockOut()
        {
            query = $@"
                        INSERT INTO ST_StockM (
                            CorpId
                            , InOutNo
                            , OutDate
                            , FromHouseCode
                            , WriteID
                            , WriteDate
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{InOutNo}'
                            , '{OutDate}'
                            , '{FromHouseCode}'
                            , '{WrGlobal.LoginID}'
                            , GETDATE()
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_StockM_Defect(string jobNo)
		{
            query = $@"
                        UPDATE ST_StockM
							SET ToHouseCode = '{ToHouseCode}'
							, ModId = '{WrGlobal.LoginID}'
							, ModIP = '{WrGlobal.ClientHostName}'
							, ModDt = GETDATE()
							WHERE CorpId = '{WrGlobal.CorpID}'
							AND InOutNo IN (
								SELECT InOutNo 
								FROM ST_StockD 
								WHERE CorpId = '{WrGlobal.CorpID}' 
								AND JobNo = '{jobNo}'
							);
                    ";

            return db.ExecuteQuery(query);
		}

        public void Del_ST_StockM_ByInOutNo()
        {
            query = $@"DELETE FROM ST_StockM
                        WHERE InOutNo = '{InOutNo}'
                        AND CorpId = '{WrGlobal.CorpID}'
                ";

            db.ExecuteQuery(query);
        }

    }
}
