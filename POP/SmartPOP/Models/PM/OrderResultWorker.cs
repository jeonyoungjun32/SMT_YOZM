using DevExpress.XtraCharts.Native;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.PM
{
	public class PM_OrderResultWorker
	{
        #region Property
        public long Idx { get; set; }

        public string CorpId { get; set; }

        public string OrderNo { get; set; }

        public int OrderSeq { get; set; }

        public int SubSeq { get; set; }

        public string WorkerID { get; set; }

        public string WorkerType { get; set; }

        //
        public string WorkerName { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable SelectWorker()
		{
            query = $@"
                SELECT
                    ow.Idx
                    , ow.CorpId
                    , ow.OrderNo
                    , ow.OrderSeq
                    , ow.SubSeq
                    , ow.WorkerID
                    , ow.WorkerType
                    , ow.CreId
                    , ow.CreIP
                    , ow.CreDt
                    , ow.ModId
                    , ow.ModIP
                    , ow.ModDt
                FROM PM_OrderResultWorker ow
            ";

            return db.GetDataTable(query);
		}

        public DataTable Select_Worker()
        {
            //Y : 라인별 작업자, N : 공정별 작업자
            string LineWorker_YN = WrGlobal.ProcOption["BS"]["LineWorker_YN"];

            query = $@"

                SELECT 
                      A.OrderNo
                    , A.OrderSeq
                    , A.SubSeq
                    , A.WorkerID
                    , B.EmployeeName WorkerName
                    , A.WorkerType
                    , A.CreId
                    , A.CreIP
                    , A.CreDt
                    , A.ModId
                    , A.ModIP
                    , A.ModDt
                
                FROM PM_OrderResultWorker A
                LEFT JOIN SM_User B ON A.CorpId = B.CorpId AND B.UseYn = 'Y' AND A.WorkerId = B.EmployeeId
                LEFT JOIN CM_LineProcessWorker C ON A.CorpId = C.CorpId AND A.WorkerId = C.EmpNo

                WHERE A.CorpId = '{WrGlobal.CorpID}'
                AND A.OrderNo = '{OrderNo}'
                AND A.OrderSeq = '{OrderSeq}'
                AND A.SubSeq = '{SubSeq}'

                ";

      
            query += $"ORDER BY A.WorkerID";

            return db.GetDataTable(query);
        }

        public DataRow Select_Workers_Nm()
        {
            query = $@"

            WITH CTE_WORKER
            AS
            (
            	SELECT B.EmployeeName 
            	FROM  PM_OrderResultWorker A
            	LEFT JOIN SM_User B ON A.CorpId = B.CorpId AND A.WorkerID = B.EmployeeID
            	WHERE A.CorpId = '{WrGlobal.CorpID}'
                AND A.OrderNo = '{OrderNo}' 
                AND A.OrderSeq = '{OrderSeq}' 
                AND A.SubSeq = '{SubSeq}' 
            )
            
            ";

            //작업자명 다 보여주기
                query += $@"
                
                SELECT STRING_AGG(EmployeeName,', ') AS WorkerName
                FROM CTE_WORKER 
                
                ";

            //대표자 외 몇명
    //            query += $@"

                
    //            SELECT TOP 1
				//IIF( 
				//	(
				//		SELECT COUNT(*) - 1 
				//		FROM CTE_WORKER
				//	 ) = 0
				//, EMP_NM
				//, EMP_NM + '외 ' + CONVERT(VARCHAR, (SELECT COUNT(*) - 1 FROM CTE_WORKER)) + '명' 
				//) AS Emp_Nm
    //            FROM CTE_WORKER

    //            ";

            return db.GetDataRow(query);
        }

        public bool InsWorker()
		{
            query = $@"
                INSERT INTO PM_OrderResultWorker
                (
                     CorpId
                    , OrderNo
                    , OrderSeq
                    , SubSeq
                    , WorkerID
                    , WorkerType
                    , CreId
                    , CreIP
                    , CreDt
                )
                VALUES
                (
                     '{WrGlobal.CorpID}'
                    , {OrderNo.ConvertStr_Empty_To_Null()}
                    , {OrderSeq.ConvertInt_Empty_To_Null()}
                    , {SubSeq.ConvertInt_Empty_To_Null()}
                    , {WorkerID.ConvertStr_Empty_To_Null()}
                    , {WorkerType.ConvertStr_Empty_To_Null()}
                    , '{WrGlobal.LoginID}'
                    , '{WrGlobal.ClientHostName}'
                    , GETDATE()
                );


            ";

            return db.ExecuteQuery(query);
		}

        public bool UpdWorker()
        {
            query = $@"
                UPDATE PM_OrderResultWorker SET
                    CorpId             = '{WrGlobal.CorpID}'
                    , OrderNo            = {OrderNo.ConvertStr_Empty_To_Null()}
                    , OrderSeq           = {OrderSeq.ConvertInt_Empty_To_Null()}
                    , SubSeq             = {SubSeq.ConvertInt_Empty_To_Null()}
                    , WorkerID           = {WorkerID.ConvertStr_Empty_To_Null()}
                    , WorkerType         = {WorkerType.ConvertStr_Empty_To_Null()}
                    , ModId              = '{WrGlobal.LoginID}'
                    , ModIP              = '{WrGlobal.ClientIpAddress}'
                    , ModDt              = GETDATE()
                WHERE Idx = '{Idx}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool DelWorker()
        {
            query = $@"
                DELETE FROM PM_OrderResultWorker
                WHERE CorpId             = '{WrGlobal.CorpID}'
                AND OrderNo            = {OrderNo.ConvertStr_Empty_To_Null()}
                AND OrderSeq           = {OrderSeq.ConvertInt_Empty_To_Null()}
                AND SubSeq             = {SubSeq.ConvertInt_Empty_To_Null()}
                ";

            return db.ExecuteQuery(query);
        }
    }
}
