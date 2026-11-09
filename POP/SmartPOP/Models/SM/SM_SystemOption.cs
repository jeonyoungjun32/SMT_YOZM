using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.SM
{
    public class SM_SystemOption
    {
        #region Property

        public string Task_Div { get; set; }
        public string Option_Nm { get; set; }
        public string Value { get; set; }
        public string Rmk { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        //public DataTable Select_Switch()
        //{
        //    throw new NotImplementedException();
        //}

        public DataTable Select_Switch(string task = null)
        {
            //사업부관리(Frm_SC_Corp) 호출 Query
            query = $@"SELECT '' AS Status
                            , TaskDiv
                            , OptionNm
                            , Value
                            , Remark

                            , CreId
                            , CreIP
                            , CreDt   
                    	    , ModId
                            , ModIP
                            , ModDt

                         FROM SM_SystemOption
                        WHERE CorpID = '{WrGlobal.CorpID}'";
            
            if(task != null)
            {
                query += $"\nAND TaskDiv = '{task}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Switch_TaskDivList()
        {
            query = $@"SELECT DISTINCT
                              TaskDiv

                         FROM SM_SystemOption
                        WHERE CorpID = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }

        public bool Ins_Switch()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO SM_SystemOption(CorpID, Task_Div, Option_Nm, Value, Rmk                              
                                           , Ins_Emp_No, Ins_Date_Time)
                            VALUES ('{WrGlobal.CorpID}'
                                  , '{Task_Div}'
                                  , '{Option_Nm}'
                                  , '{Value}'
                                  , '{Rmk}'

                                  , '{WrGlobal.LoginID}'
                                  , GETDATE())";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Switch()
        {
            db.Edit_Property(this);

            query = $@"UPDATE SM_SystemOption
                          SET Option_Nm = '{Option_Nm}'
                            , Value = '{Value}'
                            , Rmk = '{Rmk}'

                            , Upd_Emp_No = '{WrGlobal.LoginID}'
                            , Upd_Date_Time = GETDATE()


                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND Task_Div = '{Task_Div}'
                          AND Option_Nm = '{Option_Nm}'";

            return db.ExecuteQuery(query);
        }
    }
}
