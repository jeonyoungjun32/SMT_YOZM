using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.BM
{
    class BM_SalesM
    {
        #region Property

        public string CorpId { get; set; }
        public string SalesNo { get; set; }
        public string SalesDate { get; set; }
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
        public string ExchangeRate { get; set; }
        public string OutType { get; set; }
        public string Remark { get; set; }
        public string BillToCode { get; set; }
        public string BillToName { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public string CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public string ModDt { get; set; }
        public string ShipDate { get; set; }
        public string SalesMan { get; set; }
        public string HouseCode { get; set; }


        #endregion Property End

        Database db = new Database();

        string query;


        public DataRow SelectBM_SalesM_BySalesNo()
        {
            query = $@"
                SELECT
                      SalesNo
                    , SalesDate
                    , ShipToCode
                    , ShipToName
                    , WriteDate
                    , WriteID
                    , WriteName
                    , TotalPrice
                    , Type
                    , Remark
                    , BillToCode
                    , BillToName
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
	                        , cm.MaterialCode
	                        , cm.MaterialName
	                        , sod.Qty AS OrderQty
	                        , sd.Qty AS OutQty
	                        , sd.UnitCode
	                        , sd.UnitPrice
	                        , sd.Price
	                        , sd.VatPrice
	                        , sd.TotalPrice
                        FROM BM_SalesM sm
                        INNER JOIN BM_SalesD sd ON sm.SalesNo = sd.SalesNo
                        LEFT JOIN CM_Item cm ON sd.ProductCode = cm.MaterialCode
                        LEFT JOIN SdOrderMaster som ON sd.SdOrderNo = som.SdOrderNo
                        LEFT JOIN BM_SdOrderD sod ON sd.SdOrderNo = sod.SdOrderNo AND sd.SdOrderSeq = sod.SdOrderSeq
                        WHERE sm.CorpId = '{WrGlobal.CorpID}'
                        AND sd.SdOrderNo = '{sdOrderNo}'
                        ORDER BY SalesDate ASC, MaterialCode ASC
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
	                        , cm.MaterialCode
	                        , cm.MaterialName
	                        , sod.Qty AS OrderQty
	                        , sd.Qty AS OutQty
	                        , sd.UnitCode
	                        , sd.UnitPrice
	                        , sd.Price
	                        , sd.VatPrice
	                        , sd.TotalPrice
                        FROM BM_SalesM sm
                        INNER JOIN BM_SalesD sd ON sm.SalesNo = sd.SalesNo
                        LEFT JOIN CM_Item cm ON sd.ProductCode = cm.MaterialCode
                        LEFT JOIN SdOrderMaster som ON sd.SdOrderNo = som.SdOrderNo
                        LEFT JOIN BM_SdOrderD sod ON sd.SdOrderNo = sod.SdOrderNo AND sd.SdOrderSeq = sod.SdOrderSeq
                        WHERE sm.CorpId = '{WrGlobal.CorpID}' ";
            if (customer != "All")
            {
                query += $@"AND som.SoldToCode = '{customer}' ";
            }

            query += $@"AND som.ObtainDate BETWEEN '{startDate}' AND '{endDate}'
                        ORDER BY SalesDate ASC, MaterialCode ASC
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
            , {ExchangeRate.ConvertStr_Empty_To_Null()}
            , {OutType.ConvertStr_Empty_To_Null()}
            , {Remark.ConvertStr_Empty_To_Null()}
            , {BillToCode.ConvertStr_Empty_To_Null()}
            , {BillToName.ConvertStr_Empty_To_Null()}
            , '{WrGlobal.LoginID}'
            , '{WrGlobal.IpAddress}'
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
                , ExchangeRate  = {ExchangeRate.ConvertStr_Empty_To_Null()}
                , OutType       = {OutType.ConvertStr_Empty_To_Null()}
                , Remark        = {Remark.ConvertStr_Empty_To_Null()}
                , BillToCode    = {BillToCode.ConvertStr_Empty_To_Null()}
                , BillToName    = {BillToName.ConvertStr_Empty_To_Null()}
                , ModId         = '{WrGlobal.LoginID}'
                , ModIP         = '{WrGlobal.IpAddress}'
                , ModDt         = GETDATE()
                , ShipDate      = {ShipDate.ConvertStr_Empty_To_Null()}
                , SalesMan      = {SalesMan.ConvertStr_Empty_To_Null()}
                , HouseCode      = {HouseCode.ConvertStr_Empty_To_Null()}
                 WHERE CorpId = '{CorpId}'
                AND SalesNo = '{SalesNo}'

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
    }
}
