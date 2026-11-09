using DevExpress.PivotGrid.OLAP;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraCharts.Native;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.CM
{
	public class CM_LineProcessWorker
	{
		#region Property
		public string CorpId { get; set; }

		public string ProcessCode { get; set; }

		public string LineCode { get; set; }

		public string EmpNo { get; set; }

		public int Seq { get; set; }

		public string WorkBandDivCd { get; set; }
		#endregion

		Database db = new Database();
		string query = "";

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
	}
}
