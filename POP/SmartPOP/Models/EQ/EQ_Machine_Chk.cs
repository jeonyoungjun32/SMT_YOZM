using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.EQ
{
	class EQ_Machine_Chk
	{
        #region Property

        public string Machine_Cd { get; set; }
        public int Seq { get; set; }
        public string Inspect_Date { get; set; }
        public string Chk_Time_Div_Cd { get; set; }
        public string Inspect_Div_Cd { get; set; }
        public string Inspect_Check { get; set; }
        public string ChargeEmp_No { get; set; }
        public string Action_Result_Div_Cd { get; set; }
        public string Action_Div_Cd { get; set; }
        public string Action_Memo { get; set; }
        public string Action_Emp_No { get; set; }
        public string Chk_Image { get; set; }
        public string Rmk { get; set; }
        public string Week_Div_Cd { get; set; }


        #endregion Property End

        Database db = new Database();
        string query;

		public DataTable Select_InspectResult()
		{
			//설비점검(Frm_EQ_Machine_Chk)에서 사용하는 Query
			query = $@" SELECT mc.Machine_Cd
							 , m.Machine_Nm
							 , mc.Seq
							 , mcs.Disp_Seq
							 , mc.Inspect_Date
							 , mc.Chk_Time_Div_Cd
							 , mc.Inspect_Div_Cd
							 , Management_Item
							 , Check_Method
							 , Standard_Spec
							 , ValueType_Div_Cd
							 --, mc.Inspect_Check
							 , CASE WHEN mc.Inspect_Check = 'Y' THEN '10'
							          WHEN mc.Inspect_Check = 'N' THEN '20'
									  ELSE mc.Inspect_Check
							   END Inspect_Check
                             , Cycle_Div_Cd
                             , Week_Div_Cd

							 , mc.ChargeEmp_No
							 , mc.Action_Result_Div_Cd
							 , mc.Action_Div_Cd
							 , mc.Action_Memo
							 , mc.Action_Emp_No

							 , mcs.Inspect_Min
							 , mcs.Inspect_Max
							 , mc.Rmk

						FROM EQ_Machine_Chk mc
						LEFT JOIN EQ_MachineChk mcs ON (mcs.Machine_Cd = mc.Machine_Cd OR mcs.Machine_Cd = '') AND mcs.seq = mc.seq
						LEFT JOIN BS_Machine m on mcs.Machine_Cd = m.Machine_Cd

						WHERE mc.Corp_Cd = '{WrGlobal.CorpID}' 
						  AND mc.Inspect_Div_Cd = '{Inspect_Div_Cd}'
					      AND mc.Inspect_Date = '{Inspect_Date}'
						  AND mc.Machine_Cd = '{Machine_Cd}' ";

			if (Inspect_Div_Cd == "10")
			{
				query += $"\n AND mc.Chk_Time_Div_Cd = '{Chk_Time_Div_Cd}'";
			}

			if (Inspect_Div_Cd == "20")
			{
				query += $"\n AND mcs.Week_Div_Cd = '{Week_Div_Cd}'";
			}

			query += "\n ORDER BY mcs.Disp_Seq";

			return db.GetDataTable(query);
		}

		public DataTable EQ_Machine_ChkView(string date)
		{
			//설비일상점검, 정기점검현황 조회 
			query = $@" EXEC [EQ_Machine_ChkView] '{WrGlobal.CorpID}', '{date}', '{Inspect_Div_Cd}', '{Machine_Cd}'	";

			return db.GetDataTable(query);
		}

		public bool Ins_InspectResult()
		{
			db.Edit_Property(this);

			query = $@"INSERT INTO EQ_Machine_Chk
						(
							  Corp_Cd, Machine_Cd, Seq
							, Inspect_Date, Chk_Time_Div_Cd, Inspect_Div_Cd
							, Inspect_Check, ChargeEmp_No    
							, Action_Result_Div_Cd, Action_Div_Cd, Action_Memo, Action_Emp_No
							--, Chk_Image
							, Rmk
							, Ins_Emp_No,Ins_Date_Time
						)
						VALUES
						(
							  '{WrGlobal.CorpID}', '{Machine_Cd}', {Seq}
							, '{Inspect_Date}', '{Chk_Time_Div_Cd}', '{Inspect_Div_Cd}'
							, '{Inspect_Check}', '{ChargeEmp_No}'
							, '{Action_Result_Div_Cd}', '{Action_Div_Cd}', '{Action_Memo}', '{Action_Emp_No}'
							--, '{Chk_Image}'
							, '{Rmk}'
							, '{WrGlobal.LoginID}', GETDATE()
						)";

			return db.ExecuteQuery(query);
		}

		public bool Upd_InspectResult()
		{
			// Exam_Div_Cd
			db.Edit_Property(this);

			query = $@"UPDATE EQ_Machine_Chk
						  SET Inspect_Check = '{Inspect_Check}'
						    , ChargeEmp_No = '{ChargeEmp_No}'
						    , Action_Result_Div_Cd = '{Action_Result_Div_Cd}'
						    , Action_Div_Cd = '{Action_Div_Cd}'
						    , Action_Memo = '{Action_Memo}'
						    , Action_Emp_No = '{Action_Emp_No}'
						    --, Chk_Image = '{Chk_Image}'
						    , Rmk = '{Rmk}'
							, Upd_Emp_No = '{WrGlobal.LoginID}'
                            , Upd_Date_Time = GETDATE()

						WHERE Corp_Cd = '{WrGlobal.CorpID}' 
						  AND Inspect_Div_Cd = '{Inspect_Div_Cd}'
						  AND Inspect_Date = '{Inspect_Date}'
						  AND Machine_Cd = '{Machine_Cd}'
						  AND Seq = {Seq}
						  AND Chk_Time_Div_Cd = '{Chk_Time_Div_Cd}' ";

			return db.ExecuteQuery(query);
		}

		public bool Del_InspectResult()
		{
			query = $@"DELETE EQ_Machine_Chk
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
						  AND Inspect_Div_Cd = '{Inspect_Div_Cd}'
						  AND Inspect_Date = '{Inspect_Date}'
						  AND Machine_Cd = '{Machine_Cd}'
						  AND Chk_Time_Div_Cd = '{Chk_Time_Div_Cd}' ";

			return db.ExecuteQuery(query);
		}
	}
}
