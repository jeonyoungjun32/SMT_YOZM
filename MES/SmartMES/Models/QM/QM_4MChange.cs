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
    public class QM_4MChange
    {
        #region Property
        public string CorpId { get; set; }

        public string ChangeNo { get; set; }

        public string ChangeDate { get; set; }

        public string ChangeType { get; set; }

        public string ItemCode { get; set; }

        public string ProcessCode { get; set; }

        public string MachineCode { get; set; }

        public string MItemCode { get; set; }

        public string EmpId { get; set; }

        public string Remark { get; set; }

        public string ConfirmYn { get; set; }
        #endregion

        Database db = new Database();
        string query;

        public DataTable Select_QM_4MChange()
        {
            query = $@"
                        SELECT
                            '' AS Status
                            , CorpId
                            , ChangeNo
                            , ChangeDate
                            , ChangeType
                            , ItemCode
                            , ProcessCode
                            , MachineCode
                            , MItemCode
                            , EmpId
                            , Remark
                            , ConfirmYn
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM QM_4MChange
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            if (ConfirmYn != "ALL" && ConfirmYn != null)
            {
                query += $"\nAND ConfirmYn = '{ConfirmYn}'";
            }

            if (ItemCode != "ALL")
            {
                query += $"\nAND ItemCode = '{ItemCode}'";
            }

            return db.GetDataTable(query);
        }

        public bool Ins_QM_4MChange()
        {
            query = $@"
                        INSERT INTO QM_4MChange
                        (
                            CorpId
                            , ChangeNo
                            , ChangeDate
                            , ChangeType
                            , ItemCode
                            , ProcessCode
                            , MachineCode
                            , MItemCode
                            , EmpId
                            , Remark
                            , ConfirmYn
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , dbo.FN_Create_4M_No('{ChangeDate}')
                            , '{ChangeDate}'
                            , '{ChangeType}'
                            , '{ItemCode}'
                            , '{ProcessCode}'
                            , '{MachineCode}'
                            , '{MItemCode}'
                            , '{EmpId}'
                            , '{Remark}'
                            , '{ConfirmYn}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_4MChange()
        {
            query = $@"
                        UPDATE QM_4MChange
                        SET
                            ChangeDate = '{ChangeDate}'
                            , ChangeType = '{ChangeType}'
                            , ItemCode = '{ItemCode}'
                            , ProcessCode = '{ProcessCode}'
                            , MachineCode = '{MachineCode}'
                            , MItemCode = '{MItemCode}'
                            , EmpId = '{EmpId}'
                            , Remark = '{Remark}'
                            , ConfirmYn = '{ConfirmYn}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND [ChangeNo] = '{ChangeNo}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_QM_4MChange()
        {
            query = $@"
                        DELETE QM_4MChange
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND [ChangeNo] = '{ChangeNo}' 
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
