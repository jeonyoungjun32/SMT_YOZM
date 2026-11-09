using System.Data;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.MD
{
    public class MD_MoldChk_Rmk
    {
        #region Property

        public string MoldCode { get; set; }
        public string InspectDate { get; set; }
        public string InspectDivCd { get; set; }
        public string ChkTimeDivCd { get; set; }
        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Sel_MoldChk_Rmk(string from, string to)
        {
            query = $@"
                        SELECT Remark
                          FROM MD_MoldChk_Rmk
                        WHERE CorpId = '{WrGlobal.CorpID}'
                         AND MoldCode = '{MoldCode}'
                         AND InspectDate = '{from}'
                         AND ChkTimeDivCd = '{ChkTimeDivCd}'
                      ";

            return db.GetDataTable(query);
        }

        public DataRow Select_MoldChk_Rmk(string from, string to)
        {
            query = $@"
                        SELECT Remark
                          FROM MD_MoldChk_Rmk
                        WHERE CorpId = '{WrGlobal.CorpID}'
                         AND MoldCode = '{MoldCode}'
                         AND InspectDivCd = '{InspectDivCd}'
                         AND InspectDate = '{from}'
                         AND ChkTimeDivCd = '{ChkTimeDivCd}'
                      ";

            return db.GetDataRow(query);
        }

        public bool Ins_MoldChk_Rmk()
        {
            query = $@"
                        INSERT INTO MD_MoldChk_Rmk
                        (
                            CorpId
                            , MoldCode
                            , InspectDate
                            , InspectDivCd
                            , ChkTimeDivCd
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{MoldCode}'
                            , '{InspectDate}'
                            , {InspectDivCd.ConvertStr_Empty_To_Null()}
                            , {ChkTimeDivCd.ConvertStr_Empty_To_Null()}
                            , {Remark.ConvertStr_Empty_To_Null()}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                       ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_MoldChk_Rmk(string from, string to)
        {
            query = $@"
                        UPDATE MD_MoldChk_Rmk
                        SET
                            Remark = {Remark.ConvertStr_Empty_To_Null()}
                           , ModId = '{WrGlobal.LoginID}'
                           , ModIP = '{WrGlobal.ClientHostName}'
                           , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND MoldCode = '{MoldCode}'
                          AND InspectDate = '{from}'
                          AND ChkTimeDivCd = '{ChkTimeDivCd}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
