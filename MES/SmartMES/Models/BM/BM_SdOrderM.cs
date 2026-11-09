using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;

namespace SmartMES.Models.BM
{
	class BM_SdOrderM
	{
        #region Property

        public string CorpId { get; set; }

        public string SdOrderNo { get; set; }

        public string SoldToCode { get; set; }

        public string ShipToCode { get; set; }

        public string BillToCode { get; set; }

        public string ObtainDate { get; set; }

        public string DeliveryDate { get; set; }

        public decimal Price { get; set; }

        public decimal VatPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string OrderType { get; set; }

        public string ObtainType { get; set; }

        public string VatType { get; set; }

        public int VatRate { get; set; }

        public string SalesMan { get; set; }

        public string OutState { get; set; }

        public string Remark { get; set; }

        public string HouseID { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string State { get; set; }

        public string EstimateNo { get; set; }

        public string PONo { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_BM_SdOrderM(string type, string startDate, string endDate, string customer)
        {
            query = $@"
                        SELECT 
                            som.CorpId
                            , som.SdOrderNo

                            , som.SoldToCode
                            , cc.CustName

                            , som.ShipToCode
                            , som.BillToCode

                            , som.ObtainDate
                            , som.DeliveryDate
                            , som.Price
                            , som.VatPrice
                            , som.TotalPrice

                            , som.OrderType
                            , som.ObtainType
                            , som.VatType
                            , som.VatRate
                            , som.SalesMan
                            , som.OutState
                            , som.Remark

                            , som.HouseID
                            , ch.Title HouseName

                            , som.State
                            , som.CreId
                            , som.CreIp
                            , som.CreDt
                            , som.ModId
                            , som.ModIP
                            , som.ModDt

                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','S010', som.OrderType)) AS OrderTypeName  --수주구분 OrderType
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','C010', som.VatType))   AS VatTypeName --과세구분

                        FROM BM_SdOrderM som
                        LEFT JOIN CM_Cust cc ON som.SoldToCode = cc.CustCode
	                    Left Join CM_Warehouse ch on som.CorpId = ch.CorpId and som.HouseID = ch.HouseID
                        WHERE som.CorpId = '{WrGlobal.CorpID}'
                        ";
            if (customer != "ALL")
            {
                query += $@"AND som.SoldToCode = '{customer}'";
            }
            if (startDate != null && endDate != null)
            {
                if (type == "납기일자")
                {
                    query += $@"AND som.DeliveryDate BETWEEN '{startDate}' AND '{endDate}'";
                }
                else
                {
                    query += $@"AND som.ObtainDate BETWEEN '{startDate}' AND '{endDate}'";
                }
            }

            return db.GetDataTable(query);
        }

        public DataRowView Select_BM_SdOrderMByNo(string CorpId, string sdOrderNo)
        {
            query = $@"
                        SELECT 
                            som.CorpId
                            , som.SdOrderNo
                            , som.SoldToCode
                            , cc.CustName
                            , som.ShipToCode
                            , som.BillToCode
                            , som.ObtainDate
                            , som.DeliveryDate
                            , som.Price
                            , som.VatPrice
                            , som.TotalPrice
                            , som.OrderType
                            , som.VatType
                            , som.VatRate
                            , som.SalesMan
                            , som.OutState
                            , som.Remark
                            , som.HouseId
                            , som.State
                            , som.CreId
                            , som.CreIp
                            , som.CreDt
                            , som.ModId
                            , som.ModIP
                            , som.ModDt

                        FROM BM_SdOrderM som
                        LEFT JOIN CM_Cust cc ON som.SoldToCode = cc.CustCode
                        WHERE som.CorpId = '{CorpId}'
                        AND som.SdOrderNo = '{sdOrderNo}'
                        ";


            return db.GetDataRecord(query);
        }

      
        public string Get_EstimateNo()
        {
            query = $"SELECT EstimateNo FROM BM_SdOrderM WHERE CorpID = '{WrGlobal.CorpID}' AND SdOrderNo = '{SdOrderNo}'";
            DataTable dt = db.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["EstimateNo"]);
            }

            return "";
        }

        public bool Ins_BM_SdOrderM()
        {
            query = $@"
                            INSERT INTO BM_SdOrderM
                            (
                                  CorpId
                                , SdOrderNo
                                , SoldToCode
                                , ShipToCode
                                , ObtainDate
                                , DeliveryDate
                                , Price
                                , VatPrice
                                , TotalPrice
                                , OrderType
                                , VatType
                                , VatRate
                                , SalesMan
                                , OutState
                                , Remark
                                , HouseID
                                , State
                                , EstimateNo
                                , PONo
                                , CreId
                                , CreIP
                                , CreDt
                            )
                            VALUES
                            (
                             '{CorpId}'
                                , '{SdOrderNo}'
                                , '{SoldToCode}'
                                , '{ShipToCode}'
                                , '{ObtainDate}'
                                , '{DeliveryDate}'
                                , '{Price}'
                                , '{VatPrice}'
                                , '{TotalPrice}'
                                , '{OrderType}'
                                , '{VatType}'
                                , '{VatRate}'
                                , '{SalesMan}'
                                , '{OutState}'
                                , '{Remark}'
                                , '{HouseID}'
                                , '{State}'
                                , '{EstimateNo}'
                                , '{PONo}'
                                , '{WrGlobal.LoginID}'
                                , '{WrGlobal.IpAddress}'
                                , GETDATE()
                            )

            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_BM_SdOrderM()
        {
            query = $@"
                    UPDATE BM_SdOrderM
                        SET SoldToCode = '{SoldToCode}'
                        , ShipToCode     = '{ShipToCode}'
                        , ObtainDate     = {ObtainDate.ConvertStr_Empty_To_Null()}
                        , DeliveryDate   = {DeliveryDate.ConvertStr_Empty_To_Null()}
                        , Price          = {Price.ConvertString_Null_To_Zero()}
                        , VatPrice       = {VatPrice.ConvertString_Null_To_Zero()}
                        , TotalPrice     = {TotalPrice.ConvertString_Null_To_Zero()}
                        , OrderType      = '{OrderType}'
                        , VatType        = '{VatType}'
                        , VatRate        = '{VatRate}'
                        , SalesMan       = '{SalesMan}'
                        , OutState       = '{OutState}'
                        , Remark         = '{Remark}'
                        , HouseID        = '{HouseID}'
                        , State          = '{State}'
                        , PONo           = {PONo.ConvertStr_Empty_To_Null()}
                        , ModId          = '{WrGlobal.LoginID}'
                        , ModIP          = '{WrGlobal.IpAddress}'
                        , ModDt          = GETDATE()

                    WHERE CorpId = '{CorpId}'
                    AND SdOrderNo = '{SdOrderNo}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_BM_SdOrderM_State()
        {
            query = $@"
                    UPDATE BM_SdOrderM
                    SET State = '{State}'
                    WHERE CorpId = '{CorpId}'
                    AND SdOrderNo = '{SdOrderNo}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_BM_SdOrderM()
        {
            query = $@"
                    DELETE BM_SdOrderM
                    WHERE CORPID = '{CorpId}'
                    AND SDORDERNO = '{SdOrderNo}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
