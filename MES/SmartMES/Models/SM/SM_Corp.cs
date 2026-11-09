using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartMES.Models.SM
{
    class SM_Corp
    {
        #region Property

        public string CorpId { get; set; }
        public string CorpName { get; set; }
        public string CorpNo { get; set; }
        public string CorpRegNo { get; set; }
        public string PresiName { get; set; }
        public string BussinessCategory { get; set; }
        public string BussinessField { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string PostNo { get; set; }
        public string Address { get; set; }
        public string AddressSub { get; set; }
        public string TaxCode { get; set; }
        public string ContactPoint { get; set; }
        public string ContactEmail { get; set; }
        public string MasterYn { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public byte[] Pic { get; set; }
        public byte[] LogoImg { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public DateTime? CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public DateTime? ModDt { get; set; }

        #endregion

        string query = "";
        Database db = new Database();

        public DataTable Select_SM_Corp()
        {
            query = $@"
                    SELECT  '' Status
                        , CorpId
                        , CorpName
                        , CorpNo
                        , CorpRegNo
                        , PresiName
                        , BussinessCategory
                        , BussinessField
                        , Tel
                        , Fax
                        , PostNo
                        , Address
                        , AddressSub
                        , TaxCode
                        , ContactPoint
                        , ContactEmail
                        , MasterYn
                        , OpenDate
                        , CloseDate
                        , Pic
                        , LogoImg
                        , CreId
                        , CreIP
                        , CreDt
                        , ModId
                        , ModIP
                        , ModDt
                    FROM SM_Corp
                    ";

            return db.GetDataTable(query);
        }

        public bool Ins_SM_Corp()
        {
            query = $@"
                        INSERT INTO SM_Corp
                        (
                            CorpId
                            , CorpName
                            , CorpNo
                            , CorpRegNo
                            , PresiName
                            , BussinessCategory
                            , BussinessField
                            , Tel
                            , Fax
                            , PostNo
                            , Address
                            , AddressSub
                            , TaxCode
                            , ContactPoint
                            , ContactEmail
                            , MasterYn
                            , OpenDate
                            , Pic
                            , LogoImg
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                             '{CorpId}'
                            , {CorpName.ConvertStr_Empty_To_Null()}
                            , {CorpNo.ConvertStr_Empty_To_Null()}
                            , {CorpRegNo.ConvertStr_Empty_To_Null()}
                            , {PresiName.ConvertStr_Empty_To_Null()}
                            , {BussinessCategory.ConvertStr_Empty_To_Null()}
                            , {BussinessField.ConvertStr_Empty_To_Null()}
                            , {Tel.ConvertStr_Empty_To_Null()}
                            , {Fax.ConvertStr_Empty_To_Null()}
                            , {PostNo.ConvertStr_Empty_To_Null()}
                            , {Address.ConvertStr_Empty_To_Null()}
                            , {AddressSub.ConvertStr_Empty_To_Null()}
                            , {TaxCode.ConvertStr_Empty_To_Null()}
                            , {ContactPoint.ConvertStr_Empty_To_Null()}
                            , {ContactEmail.ConvertStr_Empty_To_Null()}
                            , {MasterYn.ConvertStr_Empty_To_Null()}
                            , {(OpenDate == null ? "NULL" : $"'{OpenDate:yyyy-MM-dd}'")}
                            , @Pic
                            , @LogoImg
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                        ";

            var images = new Dictionary<string, byte[]>
            {
                {"@Pic", Pic},
                {"@LogoImg", LogoImg}
            };

            return db.ExecuteQueryImage(query, images);
        }

        public bool Upd_SM_Corp()
        {
            query = $@"
                UPDATE SM_Corp
                SET
                   CorpName = {CorpName.ConvertStr_Empty_To_Null()}
                 , CorpNo = {CorpNo.ConvertStr_Empty_To_Null()}
                 , CorpRegNo = {CorpRegNo.ConvertStr_Empty_To_Null()}
                 , PresiName = {PresiName.ConvertStr_Empty_To_Null()}
                 , BussinessCategory = {BussinessCategory.ConvertStr_Empty_To_Null()}
                 , BussinessField = {BussinessField.ConvertStr_Empty_To_Null()}
                 , Tel = {Tel.ConvertStr_Empty_To_Null()}
                 , Fax = {Fax.ConvertStr_Empty_To_Null()}
                 , PostNo = {PostNo.ConvertStr_Empty_To_Null()}
                 , Address = {Address.ConvertStr_Empty_To_Null()}
                 , AddressSub = {AddressSub.ConvertStr_Empty_To_Null()}
                 , TaxCode = {TaxCode.ConvertStr_Empty_To_Null()}
                 , ContactPoint = {ContactPoint.ConvertStr_Empty_To_Null()}
                 , ContactEmail = {ContactEmail.ConvertStr_Empty_To_Null()}
                 , MasterYn = {MasterYn.ConvertStr_Empty_To_Null()}
                 , OpenDate = {(OpenDate == null ? "NULL" : $"'{OpenDate:yyyy-MM-dd}'")}
                 , Pic = @Pic
                 , LogoImg = @LogoImg
                 , ModId = '{WrGlobal.LoginID}'
                 , ModIP = '{WrGlobal.ClientHostName}'
                 , ModDt = GETDATE()
                WHERE CorpId = '{CorpId}'
                ";

            var images = new Dictionary<string, byte[]>
            {
                {"@Pic", Pic},
                {"@LogoImg", LogoImg}
            };

            return db.ExecuteQueryImage(query, images);
        }

        public bool Del_SM_Corp()
        {
            query = $@"DELETE FROM SM_Corp
                            WHERE CorpId = '{CorpId}'
                            ";

            return db.ExecuteQuery(query);
        }
    }
}
