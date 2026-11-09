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
	public class MM_PlacingM
    {
        #region Property
        public string CorpId { get; set; }
        public string MmOrderNo { get; set; }
        public string CustCode { get; set; }
        public string PlacingDate { get; set; }
        public int? Price { get; set; }
        public int? VatPrice { get; set; }
        public int? TotalPrice { get; set; }
        public string DeliveryDate { get; set; }
        public string WriteID { get; set; }
        public string VatType { get; set; }
        public int? VatRate { get; set; }
        public string CustomerUser { get; set; }
        public string CustomerTel { get; set; }
        public string State { get; set; }
        public string PlacingType { get; set; }
        public string WorkType { get; set; }
        public string Remark { get; set; }
        public DateTime? CloseDate { get; set; }
        public string CloseUserID { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_MM_PlacingM(string startDate, string endDate)
        {
            CustCode = CustCode.ConvertString_Null_To_Empty();
            State = State.ConvertString_Null_To_Empty();
            PlacingType = PlacingType.ConvertString_Null_To_Empty();

            query = $@"
                       SELECT 
                               pm.CorpId
                              , pm.MmOrderNo

                              , pm.CustCode
                              , cc.CustName

                              , pm.PlacingDate
                              , pm.Price
                              , pm.VatPrice
                              , pm.TotalPrice
                              , pm.DeliveryDate
                              , pm.WriteID
                              , pm.VatType
                              , pm.VatRate
                              , pm.CustomerUser
                              , pm.CustomerTel
                              , pm.State
                              , pm.PlacingType
                              , pm.WorkType
                              , pm.Remark
                              , pm.CloseDate
                              , pm.CloseUserID
                              , pm.CreId
                              , pm.CreIP
                              , pm.CreDt
                              , pm.ModId
                              , pm.ModIP
                              , pm.ModDt

                        FROM MM_PlacingM pm
	                    LEFT Join CM_Cust cc on cc.CustCode = pm.CustCode
			            Left Join Cm_CateItem c1 on c1.Class = 'C010' and c1.CorpId = pm.CorpId and c1.ClassItemCode = pm.VatType
			            Left Join SM_User cu on cu.CorpId = pm.CorpId and cu.EmployeeId = pm.WriteID

                        WHERE pm.CorpId = '{WrGlobal.CorpID}'
                        AND pm.PlacingDate between '{startDate}' and '{endDate}'

                        ";

            if(PlacingType != "")
			{
                query += $"\nAND pm.PlacingType = '{PlacingType}'";
			}

            if (State != "")
            {
                query += $"\nAND pm.State = '{State}'";
            }

            if (CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $"\nAND pm.CustCode = '{CustCode}'";
            }

            query += $"\nOrder by pm.MmOrderNo , pm.PlacingDate";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public DataTable Select_MM_PlacingM_MmOrderNo()
        {
            query = $@"
                       SELECT 
                               CorpId
                              , MmOrderNo
                              , CustCode
                              , PlacingDate
                              , Price
                              , VatPrice
                              , TotalPrice
                              , DeliveryDate
                              , WriteID
                              , VatType
                              , VatRate
                              , CustomerUser
                              , CustomerTel
                              , State
                              , PlacingType
                              , WorkType
                              , Remark
                              , CloseDate
                              , CloseUserID
                              , CreId
                              , CreIP
                              , CreDt
                              , ModId
                              , ModIP
                              , ModDt
                        FROM MM_PlacingM
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MmOrderNo = '{MmOrderNo}'
                        ";

            //if (ItemCode != null && ItemCode != "" && ItemCode.ToUpper() != "ALL")
            //{
            //    query += $"\nAND ItemCode = '{ItemCode}'";
            //}

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public bool ExistsMM_PlacingM(string corpId, string mmOrderNo)
		{
            bool result = false;

            query = $@"SELECT COUNT(CorpId) AS CNT FROM MM_PlacingM WHERE CorpId = '{corpId}' AND MMOrderNo = '{mmOrderNo}' ";

            DataRow dr = db.GetDataRow(query);

            if(dr != null)
			{
                result = dr["CNT"].ConvertInt_Null_To_Zero() > 0;
			}

            return result;
		}

        public bool Insert_MM_PlacingM()
        {
            query = $@"
                        INSERT INTO MM_PlacingM(
                                 CorpId
                               , MmOrderNo
                               , CustCode
                               , PlacingDate
                               , Price
                               , VatPrice
                               , TotalPrice
                               , DeliveryDate
                               , WriteID
                               , VatType
                               , VatRate
                               , CustomerUser
                               , CustomerTel
                               , State
                               , PlacingType
                               , WorkType
                               , Remark
                               , CloseDate
                               , CloseUserID
                               , CreId
                               , CreIP
                               , CreDt 
                        )
                        VALUES
                        (
                                '{CorpId}'
                                , '{MmOrderNo}'
                                , {CustCode.ConvertStr_Empty_To_Null()}
                                , '{PlacingDate.ConvertDate_To_Char10()}'
                                , {Price.ConvertInt_Null_To_Zero()}
                                , {VatPrice.ConvertInt_Null_To_Zero()}
                                , {TotalPrice.ConvertInt_Null_To_Zero()}
                                , '{DeliveryDate.ConvertDate_To_Char10()}'
                                , {WriteID.ConvertStr_Empty_To_Null()}
                                , {VatType.ConvertStr_Empty_To_Null()}
                                , {VatRate.ConvertInt_Null_To_Zero()}
                                , {CustomerUser.ConvertStr_Empty_To_Null()}
                                , {CustomerTel.ConvertStr_Empty_To_Null()}
                                , {State.ConvertStr_Empty_To_Null()}
                                , {PlacingType.ConvertStr_Empty_To_Null()}
                                , {WorkType.ConvertStr_Empty_To_Null()}
                                , {Remark.ConvertStr_Empty_To_Null()}
                                , '{CloseDate.ConvertDate_To_Char10()}'
                                , {CloseUserID.ConvertStr_Empty_To_Null()}
                                , '{WrGlobal.LoginID}'
                                , '{WrGlobal.ClientHostName}'
                                , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Update_MM_PlacingM_Price()
        {
            query = $@"
                        UPDATE MM_PlacingM
                        SET Price = '{Price}'
	                        , VatPrice = '{VatPrice}'
	                        , TotalPrice = '{TotalPrice}'
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND MmOrderNo = '{MmOrderNo}';
                        ";

            return db.ExecuteQuery(query);
        }


        public bool Update_MM_PlacingM()
        {
            query = $@"
                        UPDATE MM_PlacingM
                        SET CustCode = '{CustCode}',
                            PlacingDate = '{PlacingDate.ConvertDate_To_Char10()}',
                            Price = {Price.ConvertInt_Null_To_Zero()},
                            VatPrice = {VatPrice.ConvertInt_Null_To_Zero()},
                            TotalPrice = {TotalPrice.ConvertInt_Null_To_Zero()},
                            DeliveryDate = '{DeliveryDate.ConvertDate_To_Char10()}',
                            WriteID = {WriteID.ConvertStr_Empty_To_Null()},
                            VatType = {VatType.ConvertStr_Empty_To_Null()},
                            VatRate = {VatRate.ConvertInt_Null_To_Zero()},
                            CustomerUser = {CustomerUser.ConvertStr_Empty_To_Null()},
                            CustomerTel = {CustomerTel.ConvertStr_Empty_To_Null()},
                            State = {State.ConvertStr_Empty_To_Null()},
                            PlacingType = {PlacingType.ConvertStr_Empty_To_Null()},
                            Remark = {Remark.ConvertStr_Empty_To_Null()},
                            ModId = '{WrGlobal.LoginID}', 
                            ModIP =  '{WrGlobal.ClientHostName}',
                            ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND MmOrderNo = '{MmOrderNo}';
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
