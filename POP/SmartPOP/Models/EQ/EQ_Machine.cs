using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Model.EQ
{
    class EQ_Machine
    {
        #region Property

        public string Seq { get; set; }
        public string Repair_Date { get; set; }
        public string Repair_Cause { get; set; }
        public string Repair_State { get; set; }
        public string Repair_Emp_No { get; set; }
        public string Repair_Start_Time { get; set; }
        public string Repair_End_Time { get; set; }
        public int Repair_Time { get; set; }
        public string Repair_Cust_Cd { get; set; }
        public int Repair_Amt { get; set; }
        public string Remark { get; set; }
        public string MachineCode { get; set; }
        public string ProcessCode { get; set; }
        public string ModelNo { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_Machine_ByID()
        {
            query = $@"SELECT MachineCode
                            , MachineName
                         FROM EQ_Machine
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ModelNo = '{ModelNo}'
                        ";

            return db.GetDataTable(query);
        }

        public DataRow Select_Machine_Info()
        {
            query = $@"SELECT *
                         FROM EQ_Machine
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MachineCode = '{MachineCode}'
                        ";

            return db.GetDataRow(query);
        }


        //등록
        public bool Ins_Repair()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO MachineRepairList (
                                CorpId
                                , MachineCode
                                , Seq
                                , Repair_Date
                                , Repair_Cause
                                , Repair_State
                                , Repair_Emp_No
                                , Repair_Start_Time
                                , Repair_End_Time
                                , Repair_Time
                                , Repair_Cust_Cd
                                , Repair_Amt
                                , Remark
                                , Ins_Emp_No
                                , Ins_Date_Time)
                            VALUES( 
                                '{WrGlobal.CorpID}',
                                '{MachineCode}',
                                '{Seq}',
                                 {Repair_Date.ConvertStr_Empty_To_Null()},
                                '{Repair_Cause}',
                                '{Repair_State}',
                                '{Repair_Emp_No}',
                                 {Repair_Start_Time.ConvertStr_Empty_To_Null()},
                                 {Repair_End_Time.ConvertStr_Empty_To_Null()},
                                 {Repair_Time.ConvertInt_Empty_To_Null()},
                                '{Repair_Cust_Cd}',
                                '{Repair_Amt}',
                                '{Remark}',
                                '{WrGlobal.LoginID}',
                                GETDATE())";

            return db.ExecuteQuery(query);
        }

        //수정
        public bool Upd_Repair()
        {
            db.Edit_Property(this);

            query = $@"UPDATE MachineRepairList
                            SET Repair_Date = '{Repair_Date}', 
		                        Repair_Cause = '{Repair_Cause}',
		                        Repair_State = '{Repair_State}', 
		                        Repair_Emp_No = '{Repair_Emp_No}',
		                        Repair_Start_Time = '{Repair_Start_Time}',
		                        Repair_End_Time = '{Repair_End_Time}',
		                        Repair_Time = '{Repair_Time.ConvertInt_Empty_To_Null()}',
		                        Repair_Cust_Cd = '{Repair_Cust_Cd}',
		                        Repair_Amt = '{Repair_Amt}',
		                        Remark = '{Remark}',
                                Upd_Emp_No = '{WrGlobal.LoginID}',
                                Upd_Date_Time = GETDATE()
                            WHERE CorpId = '{WrGlobal.CorpID}' AND MachineCode = '{MachineCode}' AND Seq = {Seq};";

            return db.ExecuteQuery(query);
        }

        /// <summary>
        /// 설비정보 불러오기, 전체 보기 기능 & 특정 품목구분들 불러오기
        /// </summary>
        /// <param name="mach_List">특정 설비만 보고싶을 경우</param>
        /// <param name="all_Yn">전체 Row 추가 Yn</param>
        /// <returns></returns>
        public DataTable Select_Machine_List(string[] mach_List = null, bool all_Yn = false)
        {
            query = "";

            if (all_Yn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT 'ALL' AS 코드, '전체' AS 명칭
                        UNION ALL";
            }

            query += "\n";
            query += $@"SELECT MachineCode AS 코드, MachineName AS 명칭
                         FROM EQ_Machine
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            //특정 설비만 보고 싶은 경우
            if (mach_List != null &&
                mach_List.Length > 0)
            {
                query += "\nAND MachineCode IN (";

                for (int idx = 0; idx < mach_List.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{mach_List[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }
    }
}
