using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.BM
{
	class BM_SalesM
    {
        #region Property

        public string CorpId { get; set; }

        public string SalesNo { get; set; }

        public string SalesDate { get; set; }

        public string BillToCode { get; set; }

        public string BillToName { get; set; }

        public string ShipToCode { get; set; }

        public string ShipToName { get; set; }

        public string WriteDate { get; set; }

        public string WriteID { get; set; }

        public string WriteName { get; set; }

        public int Cnt { get; set; }

        public decimal TotalPrice { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public string CurrencyType { get; set; }

        public double ExchangeRate { get; set; }

        public string OutType { get; set; } // 출고구분 10:제품, 30:자재

        public string ShipDate { get; set; }

        public string SalesMan { get; set; }

        public string HouseCode { get; set; }

        public string Remark { get; set; }


        #endregion Property End

        Database db = new Database();

        string query;

        public DataTable Select_BM_SalesM(string startDate, string endDate)
        {
            ShipToCode = ShipToCode.ConvertString_Null_To_Empty();

            query = $@"
                SELECT 
                       sm.CorpId
                     , sm.SalesNo
                     , sm.SalesDate
                     , sm.ShipToCode
                     , sm.ShipToName
                     , sm.WriteDate
                     , sm.WriteID
                     , sm.WriteName
                     , sm.Cnt
                     , sm.TotalPrice
                     , sm.Type
                     , sm.Status
                     , sm.CurrencyType
                     , sm.ExchangeRate
                     , sm.OutType
                     , sm.Remark
                     , sm.BillToCode
                     , sm.BillToName
                     , sm.CreId
                     , sm.CreIP
                     , sm.CreDt
                     , sm.ModId
                     , sm.ModIP
                     , sm.ModDt
                     , sm.ShipDate
                     , sm.SalesMan
                     , sm.HouseCode
                     , tax.VatType
                     , tax.VatRate

                FROM BM_SalesM sm
                OUTER APPLY (
                    SELECT TOP (1) sd.VatType, sd.VatRate
                    FROM BM_SalesD sd
                    WHERE sd.CorpId = sm.CorpId AND sd.SalesNo = sm.SalesNo
                    ORDER BY sd.SalesSeq
                ) tax
                WHERE sm.CorpId = '{WrGlobal.CorpID}'
                AND sm.SalesDate BETWEEN '{startDate}' AND '{endDate}'
              ";

            if(OutType != "")
			{
                query += $"\nAND sm.OutType = '{OutType}'";
			}

            if (ShipToCode != "" && ShipToCode != "ALL")
            {
                query += $"\nAND sm.ShipToCode = '{ShipToCode}'";
            }

            query += $"\nORDER BY SalesNo DESC";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }


        public DataRow SelectBM_SalesM_BySalesNo()
		{
            query = $@"
                SELECT
                     CorpId
                     , SalesNo
                     , SalesDate
                     , ShipToCode
                     , ShipToName
                     , WriteDate
                     , WriteID
                     , WriteName
                     , Cnt
                     , TotalPrice
                     , Type
                     , Status
                     , CurrencyType
                     , ExchangeRate
                     , OutType
                     , Remark
                     , BillToCode
                     , BillToName
                     , CreId
                     , CreIP
                     , CreDt
                     , ModId
                     , ModIP
                     , ModDt
                     , ShipDate
                     , SalesMan
                     , HouseCode

                FROM BM_SalesM sm
                WHERE sm.CorpId = '{WrGlobal.CorpID}'
                AND sm.SalesNo = '{SalesNo}'
                ";
                
            return db.GetDataRow(query);
		}

        public DataTable Select_Outsourcing()
        {
            query = $@"
                        SELECT SalesNo	
		                        , ShipToCode AS CustCode
		                        , SalesDate
		                        , Remark
                        FROM BM_SalesM
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CONVERT(nvarchar(10), SalesDate, 23) = '{SalesDate}'
                        AND OutType = '{OutType}'
                        ORDER BY SalesNo DESC
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Select_TradingStatementDetail(string sdOrderNo)
        {
            query = $@"
                        SELECT
	                        sm.CorpId
	                        , sd.SalesNo
	                        , sd.SalesSeq
	                        , sd.SdOrderNo
	                        , sd.SdOrderSeq
	                        , CONVERT(CHAR(10), sm.SalesDate, 23) AS SalesDate

                            , som.SoldToCode
                            , som.BillToCode
                            , som.PONo

	                        , cm.ItemCode
	                        , cm.ItemName
	                        , cm.Alias
	                        , cm.Size
                            , cm.SearchName

	                        , sod.Qty AS OrderQty
	                        , sd.Qty AS OutQty
	                        , sd.Qty

	                        , sd.UnitCode
	                        , sd.UnitPrice
	                        , sd.Price
	                        , sd.VatPrice
	                        , sd.TotalPrice
	                        , sd.Remark

                        FROM BM_SalesM sm
                        INNER JOIN BM_SalesD sd ON sm.SalesNo = sd.SalesNo
                        LEFT JOIN CM_Item cm ON sd.ItemCode = cm.ItemCode
                        LEFT JOIN BM_SdOrderM som ON sd.SdOrderNo = som.SdOrderNo
                        LEFT JOIN BM_SdOrderD sod ON sd.SdOrderNo = sod.SdOrderNo AND sd.SdOrderSeq = sod.SdOrderSeq
                        WHERE sm.CorpId = '{WrGlobal.CorpID}'
                        AND sd.SdOrderNo = '{sdOrderNo}'
                        ORDER BY SalesDate ASC, ItemCode ASC
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_SalesTaxInvoice(string startDate, string endDate, string customer)
        {
            query = $@"
                        SELECT
	                        sm.CorpId
	                        , sd.SalesNo
	                        , sd.SalesSeq
	                        , sd.SdOrderNo
	                        , sd.SdOrderSeq
	                        , CONVERT(CHAR(10), sm.SalesDate, 23) AS SalesDate
                            , som.SoldToCode
	                        , cm.ItemCode
	                        , cm.ItemName
	                        , sod.Qty AS OrderQty
	                        , sd.Qty AS OutQty
	                        , sd.UnitCode
	                        , sd.UnitPrice
	                        , sd.Price
	                        , sd.VatPrice
	                        , sd.TotalPrice
                        FROM BM_SalesM sm
                        INNER JOIN BM_SalesD sd ON sm.SalesNo = sd.SalesNo
                        LEFT JOIN CM_Item cm ON sd.ItemCode = cm.ItemCode
                        LEFT JOIN BM_SdOrderM som ON sd.SdOrderNo = som.SdOrderNo
                        LEFT JOIN BM_SdOrderD sod ON sd.SdOrderNo = sod.SdOrderNo AND sd.SdOrderSeq = sod.SdOrderSeq
                        WHERE sm.CorpId = '{WrGlobal.CorpID}' ";
            if (customer != "ALL")
            {
                query += $@"AND som.SoldToCode = '{customer}' ";
            }

            query += $@"AND som.ObtainDate BETWEEN '{startDate}' AND '{endDate}'
                        ORDER BY SalesDate ASC, ItemCode ASC
                    ";

            return db.GetDataTable(query);
        }

        public bool InsertBM_SalesM()
		{
            query = $@"
            INSERT INTO BM_SalesM
            (
            CorpId
            , SalesNo
            , SalesDate
            , ShipToCode
            , ShipToName
            , WriteDate
            , WriteID
            , WriteName
            , Cnt
            , TotalPrice
            , Type
            , Status
            , CurrencyType
            , ExchangeRate
            , OutType
            , Remark
            , BillToCode
            , BillToName
            , CreId
            , CreIP
            , CreDt
            , ShipDate
            , SalesMan
            , HouseCode
            )
            VALUES
            (
              '{CorpId}'
            ,'{SalesNo}'
            , '{SalesDate}'
            , '{ShipToCode}'
            , '{ShipToName}'
            , {WriteDate.ConvertStr_Empty_To_Null()}
            , {WriteID.ConvertStr_Empty_To_Null()}
            , {WriteName.ConvertStr_Empty_To_Null()}
            , {Cnt.ConvertInt_Empty_To_Null()}
            , {TotalPrice.ConvertDecimal_Empty_To_Null()}
            , {Type.ConvertStr_Empty_To_Null()}
            , {Status.ConvertStr_Empty_To_Null()}
            , {CurrencyType.ConvertStr_Empty_To_Null()}
            , {ExchangeRate.ConvertDouble_Empty_To_Null()}
            , {OutType.ConvertStr_Empty_To_Null()}
            , {Remark.ConvertStr_Empty_To_Null()}
            , {BillToCode.ConvertStr_Empty_To_Null()}
            , {BillToName.ConvertStr_Empty_To_Null()}
            , '{WrGlobal.LoginID}'
            , '{WrGlobal.ClientHostName}'
            , getdate()
            , {ShipDate.ConvertStr_Empty_To_Null()}
            , {SalesMan.ConvertStr_Empty_To_Null()}
            , {HouseCode.ConvertStr_Empty_To_Null()}
            )
            ";

            return db.ExecuteQuery(query);
		}

        public bool UpdateBM_SalesM()
		{
            query = $@"

                UPDATE BM_SalesM
                SET
                 SalesDate     = '{SalesDate}'
                , ShipToCode    = '{ShipToCode}'
                , ShipToName    = {ShipToName.ConvertStr_Empty_To_Null()}
                , WriteDate     = {WriteDate.ConvertStr_Empty_To_Null()}
                , WriteID       = {WriteID.ConvertStr_Empty_To_Null()}
                , WriteName     = {WriteName.ConvertStr_Empty_To_Null()}
                , Cnt           = '{Cnt}'
                , TotalPrice    = '{TotalPrice}'
                , Type          = {Type.ConvertStr_Empty_To_Null()}
                , Status        = {Status.ConvertStr_Empty_To_Null()}
                , CurrencyType  = {CurrencyType.ConvertStr_Empty_To_Null()}
                , ExchangeRate  = {ExchangeRate.ConvertDouble_Empty_To_Null()}
                , OutType       = {OutType.ConvertStr_Empty_To_Null()}
                , Remark        = {Remark.ConvertStr_Empty_To_Null()}
                , BillToCode    = {BillToCode.ConvertStr_Empty_To_Null()}
                , BillToName    = {BillToName.ConvertStr_Empty_To_Null()}
                , ModId         = '{WrGlobal.LoginID}'
                , ModIP         = '{WrGlobal.ClientHostName}'
                , ModDt         = GETDATE()
                , ShipDate      = {ShipDate.ConvertStr_Empty_To_Null()}
                , SalesMan      = {SalesMan.ConvertStr_Empty_To_Null()}
                , HouseCode      = {HouseCode.ConvertStr_Empty_To_Null()}
                 WHERE CorpId = '{CorpId}'
                AND SalesNo = '{SalesNo}'

            ";
            return db.ExecuteQuery(query);

		}

        public bool UpdateBM_SalesM_Remark()
        {
            query = $@"

                UPDATE BM_SalesM
                SET Remark        = {Remark.ConvertStr_Empty_To_Null()}
                 WHERE CorpId = '{CorpId}'
                AND SalesNo = '{SalesNo}'

            ";
            return db.ExecuteQuery(query);
        }

        public bool UpdateBM_SalesM_TotalPrice()
        {
            query = $@"
             UPDATE A
            SET A.Cnt =
                    (SELECT COUNT(*)
                     FROM BM_SalesD
                     WHERE CorpId = A.CorpId
                       AND SalesNo = A.SalesNo)
                , A.TotalPrice =
                    (SELECT ISNULL(SUM(TotalPrice), 0)
                     FROM BM_SalesD
                     WHERE CorpId = A.CorpId
                       AND SalesNo = A.SalesNo)
            FROM BM_SalesM A
            WHERE A.CorpId = '{CorpId}'
              AND A.SalesNo = '{SalesNo}';
            ";
            return db.ExecuteQuery(query);

        }

        public bool DeleteBM_SalesM()
        {
            query = $@"
                DELETE BM_SalesM
                WHERE CorpId = '{CorpId}'
                AND SalesNo = '{SalesNo}'
            ";
            return db.ExecuteQuery(query);

        }

        public bool DeleteSalesMD_Stock()
        {
            query = $@"
                    DELETE BM_SalesD WHERE CorpId = '{CorpId}' AND SalesNo = '{SalesNo}'
                    DELETE BM_SalesM WHERE CorpId = '{CorpId}' AND SalesNo = '{SalesNo}'

		            DELETE from ST_StockD Where CorpId = '{CorpId}' and InOutNo = '{SalesNo}'
		            DELETE from ST_StockM Where CorpId = '{CorpId}' and InOutNo = '{SalesNo}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
