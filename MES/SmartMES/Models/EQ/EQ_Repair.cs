using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;

namespace SmartMES.Models.EQ
{
	class EQ_Repair
	{
        #region Property

        public string Repair_No { get; set; }
        public string Repair_Date { get; set; }
        public int Seq { get; set; }
        public int DispSeq { get; set; }
        public string MachineCode { get; set; }
        public string Repair_Cause { get; set; }
        public string Repair_State { get; set; }
        public string Repair_Emp_No { get; set; }
        public string Repair_Start_Time { get; set; }
        public string Repair_End_Time { get; set; }
        public string Repair_Time { get; set; }
        public string Repair_Cust_Cd { get; set; }
        public decimal Repair_Amt { get; set; }
        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_EQ_Repair()
        {
            query = $@"
                        SELECT
                            '' AS Status
                            , CorpId
                            , RepairNo
                            , CONVERT(DATETIME, RepairDate, 112) RepairDate
                            , MachineCode
                            , MachineCode AS MachineName
                            , RePairCause
                            , RepairState
                            , RepairEmpNo
                            , RepairStartTime
                            , RepairEndTime
                            , RepairTime
                            , RepairCustCd
                            , RepairAmt
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM EQ_Repair
                        WHERE CorpId = '{WrGlobal.CorpID}'
                    ";

            if (MachineCode != "ALL")
            {
                query += $"\nAND MachineCode = '{MachineCode}'";
            }

            return db.GetDataTable(query);
        }

        public bool Ins_EQ_Repair()
        {
            query = $@"
                        DECLARE @RepairDate CHAR(8) = '{Repair_Date}'
                            , @OutNo nvarchar(20)
                            , @pCorpID nvarchar(20) = '{WrGlobal.CorpID}'

                        Exec Code_AutoNumber @pCorpID, '수리', @RepairDate, @OutNo output;
				        Set @OutNo = 'MR' + @OutNo;

                        INSERT INTO EQ_Repair
					    (
						    CorpId
                            , RepairNo
						    , RepairDate
						    , MachineCode
						    , RePairCause
						    , RepairState
						    , RepairEmpNo
						    , RepairStartTime
						    , RepairEndTime
						    , CreId
                            , CreIP
                            , CreDt
					    )
					    VALUES
					    (
						    '{WrGlobal.CorpID}'
                            , @OutNo
                            , @RepairDate
                            , '{MachineCode}'
                            , '{Repair_Cause}'
                            , '{Repair_State}'
                            , '{Repair_Emp_No}'
                            , '{Repair_Start_Time}'
                            , '{Repair_End_Time}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.IpAddress}'
                            , getdate()
					    )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_EQ_Repair()
        {
            query = $@"
                        UPDATE EQ_Repair
                            SET RepairDate = '{Repair_Date}'
                            , MachineCode = '{MachineCode}'
                            , RepairStartTime = '{Repair_Start_Time}'
                            , RepairEndTime = '{Repair_End_Time}'
                            , RePairCause =  '{Repair_Cause}'
                            , RepairState = '{Repair_State}'
                            , RepairEmpNo = '{Repair_Emp_No}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.IpAddress}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND RepairNo = '{Repair_No}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_EQ_Repair()
        {
            query = $@"
                        DELETE EQ_Repair
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND RepairNo = '{Repair_No}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
