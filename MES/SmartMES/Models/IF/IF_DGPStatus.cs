using System;
using System.Data;
using SmartCommon.Common;

namespace SmartMES.Models.IF
{
    public class IF_DGPStatus
    {
        #region Property

        public string CorpId { get; set; }

        public string DgpCode { get; set; }

        public string DgpName { get; set; }

        public string PlcModel { get; set; }

        public string MesSendState { get; set; }

        public string CounterReset { get; set; }

        public string MachineState { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public bool Update_CounterReset()
        {
            query = $@"
                        UPDATE IF_DgpStatus
                        SET
                            CounterReset = '{CounterReset}'
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND DgpCode = '{DgpCode}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
