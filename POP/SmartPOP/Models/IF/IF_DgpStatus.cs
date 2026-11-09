using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.IF
{
    class IF_DgpStatus
    {
        #region Property

        public string Corpid { get; set; }

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

        public DataTable Select_IF_DGPStatus(string ctCode = null)
        {
            query = $@"
                        SELECT
                            ds.Corpid
                            , ds.DgpCode
                            , ds.DgpName
                            , ds.PlcModel
                            , ds.[Use]
                            , ds.MesSendState
                            , ds.CounterReset
                            , ds.MachineState
                            , ds.StartDate
                            , ds.EndDate
                            , dc.Write_Address
                        FROM IF_DGPStatus ds
                        LEFT JOIN IF_DGPCode dc ON ds.Corpid = dc.Corpid AND ds.DgpCode = dc.DgpCode
                        WHERE ds.Corpid = '{WrGlobal.CorpID}'
                    ";

            if (ctCode != null)
            {
                query += $"\nAND dc.CTCode = '{ctCode}'";
            }

            return db.GetDataTable(query);
        }

        public void UpdateCounterReset()
        {
            string query = " UPDATE IF_DgpStatus " +
                          $"    SET CounterReset = '{CounterReset}' " +
                          $"  WHERE DgpCode = '{DgpCode} '";

            db.ExecuteQuery(query);
        }

    }
}
