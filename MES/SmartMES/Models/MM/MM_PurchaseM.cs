using DevExpress.Utils.About;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;

namespace SmartMES.Models.MM
{
	public class MM_PurchaseM
    {
        #region Property
        public string CorpId { get; set; }

        public string StoNo { get; set; }
        public string StoDate { get; set; }

        public string CustCode { get; set; }

        public string WriteID { get; set; }

        public decimal? Price { get; set; }

        public decimal? VatPrice { get; set; }

        public decimal? TotalPrice { get; set; }

        public string VatType { get; set; }

        public int VatRate { get; set; }

        public string HouseID { get; set; }

        public string Type { get; set; }

        public string CustomerUser { get; set; }

        public string CloseYn { get; set; }

        public string CloseDate { get; set; }

        public string CloseUserID { get; set; }


        public string Remark { get; set; }

        public string State { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_MM_PurchaseM(string startDate, string endDate)
        {
            Type = Type.ConvertString_Null_To_Empty();
            CustCode = CustCode.ConvertString_Null_To_Empty();
           
            query = $@"
                        SELECT '' AS Status
                             , pm.CorpId
                             , pm.StoNo
                             , pm.StoDate

                             , pm.CustCode
                            , cc1.CustName 

                             , pm.WriteID
                             , pm.Price
                             , pm.VatPrice
                             , pm.TotalPrice
                             , pm.VatType
                             , pm.VatRate
                             , pm.HouseID
                             , pm.Type
                             , pm.CustomerUser
                             , pm.CloseYn
                             , pm.CloseDate
                             , pm.CloseUserID
                             , pm.Remark
                             , pm.State
                             , pm.CreId
                             , pm.CreIP
                             , pm.CreDt
                             , pm.ModId
                             , pm.ModIP
                             , pm.ModDt
                             , pm.PurchaseType
			                 , c1.Title VatTypeTitle
			                 , wh.Title HouseName
			                 , (   SELECT TOP 1 pd.MmOrderNo
			                 	FROM MM_PurchaseD pd
			                 	WHERE pd.CorpId = pm.CorpId AND pd.StoNo  = pm.StoNo
			                 ) AS MmOrderNo

                        FROM MM_PurchaseM pm
	                    Left Join CM_Cust cc1 on cc1.CorpId = pm.CorpId and cc1.CustCode = pm.CustCode
			            Left Join CM_CateItem c1 on c1.Class = 'C010' and c1.CorpId = pm.CorpId and c1.ClassItemCode = pm.VatType
			            Left Join CM_Warehouse wh on pm.CorpId = wh.CorpId and wh.HouseID = pm.HouseID
                        WHERE pm.CorpId = '{WrGlobal.CorpID}'
                          AND pm.StoDate BETWEEN '{startDate}' AND '{endDate}'
                          
                      ";

            if(Type != "")
			{
                query += $"\nAND pm.Type = '{Type}'";
			}

            if (CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $"\nAND pm.CustCode = '{CustCode}'";
            }

            query += $"\nORDER BY pm.StoNo";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public DataRow Select_MM_PurchaseM_ByStoNo()
        {
            query = $@"
                        SELECT 
                               CorpId
                             , StoNo
                             , StoDate
                             , CustCode
                             , WriteID
                             , Price
                             , VatPrice
                             , TotalPrice
                             , VatType
                             , VatRate
                             , HouseID
                             , Type
                             , CustomerUser
                             , CloseYn
                             , CloseDate
                             , CloseUserID

                             , Remark
                             , State
                             , CreId
                             , CreIP
                             , CreDt
                             , ModId
                             , ModIP
                             , ModDt
                             , PurchaseType
                        FROM MM_PurchaseM
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND StoNo  = '{StoNo}'
                      ";

            DataRow dr = db.GetDataRow(query);
            return dr;
        }

        public bool Insert_MM_PurchaseM()
        {
            query = $@"
                     INSERT INTO MM_PurchaseM
                            (
                                 CorpId
                                  , StoNo
                                  , StoDate
                                  , CustCode
                                  , WriteID
                                  , Price
                                  , VatPrice
                                  , TotalPrice
                                  , VatType
                                  , VatRate
                                  , HouseID
                                  , Type
                                  , CustomerUser
                                  , Remark
                                  , State
                                  , CreId
                                  , CreIP
                                  , CreDt
                            )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                          , {StoNo.ConvertStr_Empty_To_Null()}
                          , '{StoDate.ConvertDate_To_Char10()}'
                          , {CustCode.ConvertStr_Empty_To_Null()}
                          , {WriteID.ConvertStr_Empty_To_Null()}
                          , '{Price.ConvertDecimal_Null_To_Zero()}'
                          , '{VatPrice.ConvertDecimal_Null_To_Zero()}'
                          , '{TotalPrice.ConvertDecimal_Null_To_Zero()}'
                          , {VatType.ConvertStr_Empty_To_Null()}
                          , '{VatRate.ConvertInt_Null_To_Zero()}'
                          , {HouseID.ConvertStr_Empty_To_Null()}
                          , {Type.ConvertStr_Empty_To_Null()}
                          , {CustomerUser.ConvertStr_Empty_To_Null()}
                          , {Remark.ConvertStr_Empty_To_Null()}
                          , {State.ConvertStr_Empty_To_Null()}
                          , '{WrGlobal.LoginID}'
                          , '{WrGlobal.ClientHostName}'
                          , GETDATE()
                        )

                    ";

            return db.ExecuteQuery(query);
        }

        public bool Update_MM_PurchaseM()
        {
            query = $@"
                        UPDATE MM_PurchaseM
                           SET 
                                 StoDate      = '{StoDate.ConvertDate_To_Char10()}'
                               , CustCode     = {CustCode.ConvertStr_Empty_To_Null()}
                               , WriteID      = {WriteID.ConvertStr_Empty_To_Null()}
                               , Price        = '{Price.ConvertDecimal_Null_To_Zero()}'
                               , VatPrice     = '{VatPrice.ConvertDecimal_Null_To_Zero()}'
                               , TotalPrice   = '{TotalPrice.ConvertDecimal_Null_To_Zero()}'
                               , VatType      = {VatType.ConvertStr_Empty_To_Null()}
                               , VatRate      = '{VatRate.ConvertInt_Null_To_Zero()}'
                               , HouseID      = {HouseID.ConvertStr_Empty_To_Null()}
                               , Type         = {Type.ConvertStr_Empty_To_Null()}
                               , CustomerUser = {CustomerUser.ConvertStr_Empty_To_Null()}
                               , Remark       = {Remark.ConvertStr_Empty_To_Null()}
                               , State        = {State.ConvertStr_Empty_To_Null()}
                               , ModId        = '{WrGlobal.LoginID}'
                               , ModIP        = '{WrGlobal.ClientHostName}'
                               , ModDt        = GETDATE()
                         WHERE CorpId = '{WrGlobal.CorpID}'
                           AND StoNo  = {StoNo.ConvertStr_Empty_To_Null()}
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Update_MM_PurchaseM_Price()
        {
            query = $@"
                        UPDATE MM_PurchaseM
                        SET Price = '{Price}'
	                        , VatPrice = '{VatPrice}'
	                        , TotalPrice = '{TotalPrice}'
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND StoNo = '{StoNo}';
                        ";

            return db.ExecuteQuery(query);
        }

        public bool Update_MM_PurchaseM_Price_DetailSum(string stoNo)
        {
            query = $@"
                      UPDATE MM_PurchaseM
                        SET Price			= D.Price
                            , VatPrice		= D.VatPrice
                            , TotalPrice	= D.TotalPrice
                        FROM MM_PurchaseM M, 
                        ( 
                        	SELECT CorpId, StoNo, SUM(Price) Price, SUM(VatPrice) VatPrice, SUM(TotalPrice) TotalPrice
                        	FROM MM_PurchaseD D
                        	WHERE D.CorpId = '{WrGlobal.CorpID}'
                        	and D.StoNo = '{stoNo}'
                        	GROUP BY CorpId, StoNo
                        ) AS D
                        WHERE M.CorpID = '{WrGlobal.CorpID}'
                        AND M.StoNo = '{stoNo}'
                        AND M.CorpId = D.CorpId
                        AND M.StoNo = D.StoNo
                        ";

            return db.ExecuteQuery(query);
        }
    }
}
