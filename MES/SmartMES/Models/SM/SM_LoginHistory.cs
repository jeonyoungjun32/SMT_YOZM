using SmartCommon.Common;
using System;
using System.Data;

namespace SmartMES.Model.SM
{
    public class SM_LoginHistory
    {
        #region Property

        public string CorpID { get; set; }
        public string ID { get; set; }
        public DateTime Login_Time { get; set; }
        public DateTime Logout_Time { get; set; }
        public string Login_IP { get; set; }
        public string Login_Project { get; set; }
        #endregion

        Database db = new Database();
        string query;

        public DataTable Select_SM_LoginHistory(DateTime startDate, DateTime endDate)
        {
            query = $@"
                        SELECT al.* , cu.*
                        FROM SM_LoginHistory al
                        LEFT JOIN SM_User cu on al.CorpId = cu.CorpId AND cu.Employeeid = al.id
                        WHERE al.CorpId = '{WrGlobal.CorpID}' 
                        AND Login_Time >= '{startDate : yyyy-MM-dd 00:00:00}'
                        AND Login_Time <= '{endDate : yyyy-MM-dd 23:59:59}'"
                        ;

            if (ID != "" && ID != "ALL")
            {
                query += $"\n AND al.ID = '{ID}' ";
            }

            query += "\n ORDER BY Login_Time DESC";

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public DataTable Select_LoginOut_History(string startDate, string endDate, string dept_Cd = "", string emp_Nm = "")
        {
            query = $@"
                        SELECT 
                              lo.ID
                            , lo.Login_Time
                            , lo.Logout_Time
                            , lo.Login_IP

                            , ep.EmployeeName
                            , ep.DeptId

                            , dp.DeptName
                        
                        FROM SM_LoginHistory lo 
                        LEFT JOIN SM_User ep ON lo.CorpID = ep.CorpID AND lo.ID = ep.EmployeeId
                        LEFT JOIN CM_Dept dp ON lo.CorpID = dp.CorpID AND ep.DeptId = dp.DeptCode
                        WHERE lo.CorpID = '{WrGlobal.CorpID}'
                          AND lo.Login_Time BETWEEN '{startDate}' AND '{endDate} 23:59:59' ";

            if (ID != "")
            {
                query += $" AND lo.ID LIKE '%{ID}%' ";
            }

            if (dept_Cd != "" && dept_Cd != "ALL")
            {
                query += $" AND ep.DeptCode = '{dept_Cd}' ";
            }

            if (emp_Nm != "")
            {
                query += $" AND ep.EmployeeName LIKE '%{emp_Nm}%' ";
            }

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public void Ins_LoginOut_History()
        {
			query = $@"
                        INSERT SM_LoginHistory
                                (   HostName
                                    , CorpID
                                    , ID
                                    , Login_Time
                                    , Login_IP
                                    , Login_Project
                                )
                                VALUES
                                (
                             host_name()  
                            ,  '{CorpID}'
                            , '{ID}'
                            , '{Login_Time:yyyy-MM-dd HH:mm:ss}'
                            , '{Login_IP}'
                            , '{Login_Project}'
                        )
                    ";
			db.ExecuteQuery(query);
		}

        //Main에 추가되어 있음
        public void Upd_LoginOut_History()
        {
           // query = $@"
           //             UPDATE LoginOutHistory
           //             SET Logout_Time = '{Logout_Time:yyyy-MM-dd HH:mm:ss}'
           //             WHERE CorpID = '{WrGlobal.CorpID}'
           //             AND Login_Time = '{WrGlobal.LoginTime:yyyy-MM-dd HH:mm:ss}'
           //         ";
           //
           // db.ExecuteQuery(query);
        }
    }
}
