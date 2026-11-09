using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.IF
{
    class IF_DgpHistory
    {
        #region Property

        public string Corpid { get; set; }

        public string DgpCode { get; set; }

        public string DgpTime { get; set; }

        public decimal Gathering_Qty { get; set; }

        public decimal Gathering_Count { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public bool Insert_DgpHistory()
        {
            query = $@"
                        INSERT INTO IF_DgpHistory
                        (
                            CorpId
                            , DgpCode
                            , DgpTime
                            , Gathering_Qty
                            , Gathering_Count
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{DgpCode}'
                            , '{DgpTime}'
                            , {Gathering_Qty}
                            , {Gathering_Count}
                        )
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
