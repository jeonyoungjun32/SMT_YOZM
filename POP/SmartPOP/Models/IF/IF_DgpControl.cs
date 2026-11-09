using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.IF
{
    class IF_DgpControl
    {
        #region Property

        public string Corpid { get; set; }

        public string CTCode { get; set; }

        public string CTName { get; set; }

        public string CT_Company { get; set; }

        public string CT_IP { get; set; }

        public int CT_Port { get; set; }

        public string CT_UserYn { get; set; }

        public string EmployeeId { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_DgpControl_by_EmployeeId()
        {
            query = $@"
                        SELECT
                            CorpId
                            , CTCode
                            , CTName
                            , CT_Company
                            , CT_IP
                            , CT_Port
                            , CT_UserYn
                            , EmployeeId
                        FROM IF_DgpControl
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND EmployeeId = '{EmployeeId}'
                    ";
            return db.GetDataTable(query);
        }
    }
}
