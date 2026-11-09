using SmartCommon.Common;
using System;
using System.Data;
using SmartMES.Common;

namespace SmartMES.Models.QM
{
    public class QM_NCAction
    {
        #region Property

        public string CorpId { get; set; }

        public string NCRNo { get; set; }

        public int ActionSeq { get; set; }

        public string ActionType { get; set; }

        public string ActionContent { get; set; }

        public string ActionUserID { get; set; }

        public string ActionDate { get; set; }

        public string UseYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        // 조회용
        public string StartDt { get; set; }

        public string EndDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_QM_NCAction()
        {
            query = $@"
                SELECT '' Status
                    , CorpId
                    , NCRNo
                    , ActionSeq
                    , ActionType
                    , ActionContent
                    , ActionUserID
                    , ActionDate
                    , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                    , ModId
                    , ModIP
                    , ModDt
                FROM QM_NCAction
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND ActionDate BETWEEN '{StartDt} 00:00:00' AND '{EndDt} 23:59:59'
            ";

            if (NCRNo != "")
            {
                query += $"\nAND NCRNo = '{NCRNo}'";
            }

            query += $"\nORDER BY ActionDate, ActionSeq";

            return db.GetDataTable(query);
        }

        public int GetNextActionSeq(string ncrNo)
        {
            query = $@"
                        SELECT ISNULL(MAX(ActionSeq), 0) + 1
                        FROM QM_NCAction
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND NCRNo = '{ncrNo}'
                    ";

            DataTable dt = db.GetDataTable(query);

            return Convert.ToInt32(dt.Rows[0][0]);
        }

        public bool Ins_QM_NCAction()
        {
            query = $@"
                INSERT INTO QM_NCAction
                (
                      CorpId
                    , NCRNo
                    , ActionSeq
                    , ActionType
                    , ActionContent
                    , ActionUserID
                    , ActionDate
                    , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                )
                VALUES
                (
                      '{WrGlobal.CorpID}'
                    , '{NCRNo}'
                    , '{ActionSeq}'
                    , {ActionType.ConvertStr_Empty_To_Null()}
                    , {ActionContent.ConvertStr_Empty_To_Null()}
                    , {ActionUserID.ConvertStr_Empty_To_Null()}
                    , '{ActionDate.ConvertDate_To_Char10()}'
                    , {UseYn.ConvertStr_Empty_To_Null()}
                    , '{WrGlobal.LoginID}'
                    , '{WrGlobal.ClientHostName}'
                    , GETDATE()
                )
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_NCAction()
        {
            query = $@"
                UPDATE QM_NCAction
                SET
                      ActionType = {ActionType.ConvertStr_Empty_To_Null()}
                    , ActionContent = {ActionContent.ConvertStr_Empty_To_Null()}
                    , ActionUserID = {ActionUserID.ConvertStr_Empty_To_Null()}
                    , ActionDate = '{ActionDate.ConvertDate_To_Char10()}'
                    , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                    , ModId = '{WrGlobal.LoginID}'
                    , ModIP = '{WrGlobal.ClientHostName}'
                    , ModDt = GETDATE()
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND NCRNo = '{NCRNo}'
                AND ActionSeq = '{ActionSeq}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_QM_NCAction()
        {
            query = $@"
                DELETE QM_NCAction
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND NCRNo = '{NCRNo}'
                AND ActionSeq = '{ActionSeq}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}