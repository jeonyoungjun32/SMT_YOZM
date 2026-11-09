using SmartCommon.Common;
using System;
using System.Data;

namespace SmartMES.Models.IF
{
    class IF_ApiHistory
    {
        public string Emp_No { get; set; }

        #region API

        public string crtfcKey { get; set; }
        public DateTime logDt { get; set; }
        public string useSe { get; set; }
        public string sysUser { get; set; }
        public string conectIp { get; set; }
        public string dataUsgqty { get; set; }
        public string SendResulrt { get; set; }

        #endregion API

        Database db = new Database();
        string query = "";

        public DataTable Select_API_History(string startDate, string endDate, string emp_Nm = "")
        {
            query = $@"
                        SELECT 
                           API.crtfcKey
                          ,API.logDt
                          ,API.useSe
                          ,API.sysUser
                          ,API.conectIp
                          ,API.dataUsgqty
                          ,API.SendResulrt
                        
                        FROM IF_ApiHistory API 
                        --LEFT JOIN SC_Emp ep ON API.sysUser = ep.Emp_No
                        WHERE API.logDt BETWEEN '{startDate}' AND '{endDate} 23:59:59' ";

            if (Emp_No != "")
            {
                query += $"\nAND API.sysUser LIKE '%{Emp_No}%' ";
            }

            if (emp_Nm != "")
            {
                query += $"\nAND sysUser LIKE '%{emp_Nm}%' ";
            }

            query += $"\nORDER BY API.logDt, API.sysUser  ";
            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public void Ins_API_History()
        {
            query = $@"
                        INSERT INTO IF_ApiHistory
                        (
                              crtfcKey
                            , logDt
                            , useSe
                            , sysUser
                            , conectIp
                            , dataUsgqty
                            , SendResulrt
                        )
                        VALUES
                        (
                              '{crtfcKey}'
                            ,  GETDATE()
                            , '{useSe}'
                            , '{sysUser}'
                            , '{conectIp}'
                            , '{dataUsgqty}'
                            , '{SendResulrt}'
                        ) ";

            db.ExecuteQuery(query);
        }
    }
}
