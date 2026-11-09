using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SmartCommon.Common;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.QM
{
    public class QM_4MChangeHistory
    {
        #region Property

        public string CorpId { get; set; }

        public string ChangeNo { get; set; }

        public int HistoryID { get; set; }

        public int RevisionNo { get; set; }

        public string BeforeContent { get; set; }

        public string AfterContent { get; set; }

        public string ApprovalNo { get; set; }

        public string UseYn { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable Select_QM_4MChangeHistory()
        {
            query = $@"
                        SELECT
                            '' AS Status
                            , CorpId
                            , HistoryID
                            , ChangeNo
                            , RevisionNo
                            , BeforeContent
                            , AfterContent
                            , ApprovalNo
                            , UseYn
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM QM_4MChangeHistory
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            if (ChangeNo != "ALL" && ChangeNo != null)
            {
                query += $"\nAND ChangeNo = '{ChangeNo}'";
            }

            return db.GetDataTable(query);
        }

        public bool Ins_QM_4MChangeHistory()
        {
            query = $@"
                        INSERT INTO QM_4MChangeHistory
                        (
                            CorpId
                            , ChangeNo
                            , RevisionNo
                            , BeforeContent
                            , AfterContent
                            , ApprovalNo
                            , UseYn
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{ChangeNo}'
                            , (SELECT ISNULL(MAX(RevisionNo), 0) + 1
                                FROM QM_4MChangeHistory
                               WHERE CorpId = '{WrGlobal.CorpID}' AND ChangeNo = '{ChangeNo}')
                            , '{BeforeContent}'
                            , '{AfterContent}'
                            , '{ApprovalNo}'
                            , '{UseYn}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_4MChangeHistory()
        {
            query = $@"
                        UPDATE QM_4MChangeHistory
                        SET
                            ApprovalNo = '{ApprovalNo}'
                            , UseYn = '{UseYn}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ChangeNo = '{ChangeNo}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_QM_4MChangeHistory()
        {
            query = $@"
                        DELETE QM_4MChangeHistory
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        ND HistoryID = '{HistoryID}'
                        AND ChangeNo = '{ChangeNo}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
