using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraCharts.Native;
using SmartCommon.Common;
using SmartMES.Common;


namespace SmartMES.Models.CM
{
    class CM_LineProcessWorker
    {
        #region Property

        public string ProcessCode { get; set; }
        public string LineCode { get; set; }
        public string EmpNo { get; set; }
        public string WorkBandDivCd { get; set; }
        public int Seq { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_ProdtWorkder()
        {
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();
            LineCode = LineCode.ConvertStr_Empty_To_Null();

            query = $@"SELECT '' Status,
                              pw.EmpNo,
                              pw.ProcessCode,
                              pw.LineCode,
                              pw.WorkBandDivCd,
                              pw.CreId,
                              pw.CreIP,
                              pw.CreDt,
                              pw.ModId,
                              pw.ModIP,
                              pw.ModDt
                         FROM CM_LineProcessWorker pw
				    LEFT JOIN SM_User se ON pw.CorpID = se.CorpID AND se.EmployeeId = pw.EmpNo
                        WHERE pw.CorpID = '{WrGlobal.CorpID}'";

            if (ProcessCode != "NULL") { query += $"\nAND pw.ProcessCode = {ProcessCode}"; }
            if (LineCode != "NULL") { query += $"\nAND pw.LineCode    = {LineCode}"; }

            if (WrGlobal.ProcOption["SC"]["Emp_AutoNo"] == "Y")
            {
                //코드 자동채번 하는경우
                query += " ORDER BY se.EmployeeName ASC";
            }
            else
            {
                //코드 수동채번 하는경우
                query += " ORDER BY se.EmployeeId ASC";
            }

            return db.GetDataTable(query);
        }

        public DataTable Init(string orderNo, int orderSeq, int subSeq)
        {
            //Y : 라인별 작업자, N : 공정별 작업자
            string LineWorker_YN = WrGlobal.ProcOption["BS"]["LineWorker_YN"];

            LineCode = LineCode.ConvertString_Null_To_Empty();
            ProcessCode = ProcessCode.ConvertString_Null_To_Empty();

            query = $@"

                    SELECT 
                    	'{orderNo}' as OrderNo
                    	, '{orderSeq}' as OrderSe
                    	, '{subSeq}' as SubSeq
                    	, A.EmpNo WorkerID
                    	, B.EmployeeName WorkerName
                    	, GETDATE() AS WorkDate
                    FROM CM_LineProcessWorker A
                    LEFT JOIN SM_User B ON A.CorpId = B.CorpId AND B.WorkerYn = 'Y' AND A.EmpNo =  B.EmployeeId
                    WHERE A.CorpId = '{WrGlobal.CorpID}'
            
			";

            if (LineWorker_YN == "Y" && LineCode != "")
            {
                query += $"\nAND A.LineCode = '{LineCode}' ";
            }
            else
            {
                query += $"\nAND A.ProcessCode = '{ProcessCode}'";
            }

            query += "\nORDER BY B.EmployeeName";

            return db.GetDataTable(query);
        }

        //등록
        public bool Ins_AuthLine()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO CM_LineProcessWorker (
                               CorpId
                              ,ProcessCode
                              ,LineCode
                              ,EmpNo
                              ,Seq
                              ,WorkBandDivCd
                              ,CreId
                              ,CreIP
                              ,CreDt )
                            VALUES( 
                                '{WrGlobal.CorpID}'
                                ,'{ProcessCode}'                                
                                ,'{LineCode}'
                                ,'{EmpNo}'
                                ,'{Seq}'
                                ,{WorkBandDivCd.ConvertStr_Empty_To_Null()}
                                ,'{WrGlobal.LoginID}'
                                ,'{WrGlobal.IpAddress}'
                                ,GETDATE())";

            return db.ExecuteQuery(query);
        }

        //수정
        public bool Upd_AuthLine()
        {
            db.Edit_Property(this);

            query = $@"UPDATE CM_LineProcessWorker
                            SET  WorkBandDivCd = '{WorkBandDivCd}'
                                ,ModId = '{WrGlobal.LoginID}'
                                ,ModIP = '{WrGlobal.IpAddress}'
                                ,ModDt = GETDATE()
                            WHERE CorpId = '{WrGlobal.CorpID}' AND ProcessCode = '{ProcessCode}' AND LineCode = '{LineCode}' AND EmpNo = '{EmpNo}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_AuthLine()
        {
            db.Edit_Property(this);

            query = $@"DELETE CM_LineProcessWorker
                       WHERE CorpId = '{WrGlobal.CorpID}' AND ProcessCode = '{ProcessCode}' AND LineCode = '{LineCode}' AND EmpNo = '{EmpNo}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
