using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.IF
{
    class IF_DgpCounter
    {
        #region Property

        public string Corpid { get; set; }

        public string DgpCode { get; set; }

        public string DgpName { get; set; }

        public string GroupID { get; set; }
        public decimal Counter { get; set; }

        #endregion

        Database db = new Database();

        public void Update_DGPCounter()
        {
            string query = $@"
                                IF (SELECT Counter FROM IF_DgpCounter WHERE DgpCode = '{DgpCode}') != '{Counter}'
                                BEGIN
                                    UPDATE IF_DgpCounter SET Counter = '{Counter}', LastTime = GETDATE() WHERE DgpCode = '{DgpCode}'
                                END
                            ";

            db.ExecuteQuery(query);
        }

    }
}
