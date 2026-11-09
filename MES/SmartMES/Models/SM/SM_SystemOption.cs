using SmartCommon.Common;
using System.Data;

namespace SmartMES.Model.SM
{
    public class SM_SystemOption
    {
        #region Property

        public string TaskDiv { get; set; }
        public string OptionNm { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_SystemOption(string task = null)
        {
            //사업부관리(Frm_SC_Corp) 호출 Query
            query = $@"SELECT '' AS Status
                            , TaskDiv
                            , OptionNm
                            , Value
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt

                         FROM SM_SystemOption
                        WHERE CorpID = '{WrGlobal.CorpID}'";

            if(task != null)
            {
                query += $"\nAND TaskDiv = '{task}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_TaskDivList()
        {
            query = $@"SELECT DISTINCT
                              TaskDiv

                         FROM SM_SystemOption
                        WHERE CorpID = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }

        public bool Ins_SystemOption()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO SM_SystemOption(CorpID, TaskDiv, OptionNm, Value, Remark
                                           , CreId, CreIP, CreDt)
                            VALUES ('{WrGlobal.CorpID}'
                                  , '{TaskDiv}'
                                  , '{OptionNm}'
                                  , '{Value}'
                                  , '{Remark}'
                                  , '{WrGlobal.LoginID}'
                                  , '{WrGlobal.ClientHostName}'
                                  , GETDATE())";

            return db.ExecuteQuery(query);
        }

        public bool Upd_SystemOption()
        {
            db.Edit_Property(this);

            query = $@"UPDATE SM_SystemOption
                          SET OptionNm = '{OptionNm}'
                            , Value = '{Value}'
                            , Remark = '{Remark}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()

                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND TaskDiv = '{TaskDiv}'
                          AND OptionNm = '{OptionNm}'";

            return db.ExecuteQuery(query);
        }
    }
}
