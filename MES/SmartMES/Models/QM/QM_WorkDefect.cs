using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.QM
{
    class QM_WorkDefect
    {
        #region Properties
        public string CorpId { get; set; }

        public string SerialNo { get; set; }

        public int Seq { get; set; }

        public int SubSeq { get; set; }

        public string ReworkID { get; set; }

        public string FromProcessCode { get; set; }

        public string FromLineCode { get; set; }

        public string ToProcessCode { get; set; }

        public string ToLineCode { get; set; }

        public string ExamType { get; set; }

        public string OrderNo { get; set; }

        public string WorkDate { get; set; }

        public string ItemCode { get; set; }

        public string DefectCode { get; set; }

        public float Qty { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ReworkProcessCode { get; set; }

        public string RepairID { get; set; }

        public string ReasonID { get; set; }

        public string Remark { get; set; }
        #endregion

        Database db = new Database();
        string query;


		public DataTable Select_NgQTY()
		{
			FromProcessCode = FromProcessCode.ConvertString_Null_To_Empty();

			query = $@"
						SELECT
							CorpId
							, MAX(OrderNo) as OrderNo
							, MAX(OrderSeq) as Seq
							, MAX(SerialNo) as SerialNo
							, MAX(ReworkID) as ReworkID
							, FaultCode
							, MAX(Title) as Bad_Nm
							, MAX(ExamType) as ExamType
							, sum(Qty) as NgQty
							, MAX(ProcessCode) as ProcessCode
							, MAX(ItemCode) as ItemCode
							, MAX(WorkDate) as WorkDate
						FROM
						(
							SELECT
								cd.CorpId
								, null as OrderNo
								, null as OrderSeq
								, null as SerialNo
								, null as ReworkID
								, cd.FaultCode
								, cd.Title
								, cd.ExamType
								, 0 as Qty
								, null as ProcessCode
								, null as ItemCode
								, null as WorkDate
							from CM_DefectCode cd
							WHERE cd.CorpId = '{WrGlobal.CorpID}' ";

			if (FromProcessCode != "")
			{
				query += $@" 
							AND cd.ProcessID = '{FromProcessCode}'";
			}

			query += $@"
							union all

							SELECT
								wd.CorpId
								, odr.OrderNo
								, odr.OrderSeq 
								, wd.SerialNo
								, wd.ReworkID
								, wd.DefectCode as FaultCode
								, null as Title
								, wd.ExamType
								, wd.Qty
								, odr.ProcessCode
								, odr.ItemCode
								, odr.WorkDate
							FROM QM_WorkDefect wd
							RIGHT JOIN PM_OrderResult odr on wd.CorpId = odr.CorpID and wd.SerialNo = odr.SerialNo
							WHERE wd.CorpId = '{WrGlobal.CorpID}' ";

			if (SerialNo != "NULL")
			{ query += $@"AND wd.SerialNo = '{SerialNo}'
                        AND wd.SubSeq = {SubSeq}"; }


			if(FromProcessCode != "")
			{
				query += $@" 
							AND wd.FromProcessCode = '{FromProcessCode}'";
			}

			query += $@"
						) as a
						GROUP BY CorpId, FaultCode
                        ORDER BY FaultCode";

			return db.GetDataTable(query);
		}

        public DataTable SelectQM_WorkDefect(string processCode)
		{
			//불량등록 - 공정기준
			// 기존 QM_WorkDefect 데이터가 있는 경우
			query = $@"
						SELECT wd.OrderNo, wd.Seq, wd.SubSeq, wd.SerialNo, wd.ReworkID
							 , cd.FaultCode, cd.Title AS FaultName
							 , wd.Qty
							 , ISNULL(sd.ToHouseCode, '') AS HouseCode
							 , wd.Remark
						FROM QM_WorkDefect wd
						INNER JOIN CM_DefectCode cd ON wd.CorpId = cd.CorpId 
													AND wd.DefectCode = cd.FaultCode
						LEFT JOIN ST_StockD sd ON sd.CorpId = wd.CorpId 
												 AND sd.JobNo = wd.ReworkID
												 AND sd.Type = '부적합' 
						WHERE wd.CorpId = '{WrGlobal.CorpID}'
						AND wd.SerialNo = '{SerialNo}'
						AND wd.Seq = '{Seq}'
						AND wd.SubSeq = '{SubSeq}'
        
						UNION ALL
        
						-- 신규 입력용: 아직 등록되지 않은 불량코드만 표시
						SELECT '' AS OrderNo, '' AS Seq, '' AS SubSeq, '' AS SerialNo, '' AS ReworkID
							 , cd.FaultCode, cd.Title AS FaultName
							 , 0 AS Qty
							 , '' AS HouseCode
							 , '' AS Remark
						FROM CM_DefectCode cd
						WHERE cd.CorpId = '{WrGlobal.CorpID}'
						AND cd.ExamType = '10' -- 10:생산, 20:검사
						AND (cd.ProcessID = '{processCode}'
								OR
							cd.ProcessID = ''	-- 모든공정도 나오도록
							    OR
							'{processCode}' = ''
							)
						AND NOT EXISTS (
							SELECT 1 
							FROM QM_WorkDefect wd2
							WHERE wd2.CorpId = cd.CorpId
							AND wd2.DefectCode = cd.FaultCode
							AND wd2.SerialNo = '{SerialNo}'
							AND wd2.Seq = '{Seq}'
							AND wd2.SubSeq = '{SubSeq}'
						)
        
						ORDER BY FaultCode
					";

			return db.GetDataTable(query);
		}

		public DataTable SelectQM_WorkDefect_BySerialNo()
		{
			query = $@"
						SELECT 
							  wd.OrderNo
							 , wd.Seq
							 , wd.SubSeq
							 , wd.SerialNo
							 , wd.ReworkID
							 , wd.DefectCode
							 , cd.Title AS DefectName
							 , wd.Qty
							 , wd.Remark

						FROM QM_WorkDefect wd
						LEFT JOIN CM_DefectCode cd ON wd.CorpId = cd.CorpId AND wd.DefectCode = cd.FaultCode
	
						WHERE wd.CorpId = '{WrGlobal.CorpID}'
						AND wd.SerialNo = '{SerialNo}'
					";

			return db.GetDataTable(query);
		}

		public DataTable SetChartBadType(string yyyyMM, string processCode, string lineCode)
        {
            //유형별 불량현황
            query = $@"SELECT LEFT(CONVERT(VARCHAR,qc.WorkDate, 112), 6) AS Date, sum(qc.Qty) as Bad_Qty, qc.DefectCode, cd.Title
                                FROM QM_WorkDefect qc
                                LEFT JOIN CM_DefectCode cd ON qc.DefectCode= cd.FaultCode
                                WHERE qc.CorpId = '{WrGlobal.CorpID}' and LEFT(CONVERT(VARCHAR,qc.WorkDate, 112), 6) =  '{yyyyMM}' ";

			if (processCode != "" && processCode.ToUpper() != "ALL")
			{
				query += $"\nAND FromProcessCode = '{processCode}' ";
			}

			query += $@"Group by qc.DefectCode, LEFT(CONVERT(VARCHAR,qc.WorkDate, 112), 6)  , cd.Title
                                ORDER BY sum(qc.Qty) DESC";

            return db.GetDataTable(query);
        }

		public DataTable chkQM_WorkDefect()
        {
            query = $@"SELECT 1
                       FROM QM_WorkDefect
                       WHERE SerialNo = '{SerialNo}'
						AND DefectCode = '{DefectCode}'";

            return db.GetDataTable(query);
        }

		public string GetNextReworkID()
		{
			query = $@"
                    	-- 1. 부적합 순번 구하기
                    	DECLARE @MaxSeq int = 0
							  , @pCorpId varchar(20) = '{WrGlobal.CorpID}'
							  , @pSerialNo varchar(20) = '{SerialNo}'
                    
                    	SELECT  @MaxSeq = ISNULL(MAX(CONVERT(INT, RIGHT(ReWorkId, 4))),0) + 1
                    	FROM QM_WorkDefect
                    	WHERE CorpId = @pCorpId
                    	AND SerialNo = @pSerialNo
                    
						SELECT CONCAT(@pSerialNo, '-') + FORMAT(@MaxSeq, 'D4') AS ReworkID
                    ";

			DataRow dr= db.GetDataRow(query);

			string reworkID = "";

			if(dr != null)
			{
				reworkID = dr["ReworkID"].ConvertString_Null_To_Empty();
			}

			return reworkID;
		}


        public bool Ins_QM_WorkDefect()
        {
            query = $@"
                            INSERT INTO QM_WorkDefect
                            (
                              CorpId, SerialNo, Seq, SubSeq, ReworkID
							, DefectCode, Qty, ExamType
							, OrderNo, FromProcessCode, ItemCode
                            , WorkDate, CreDt, CreID, CreIP
                            )
                            VALUES
                            (
                              '{WrGlobal.CorpID}', '{SerialNo}', '{Seq}', '{SubSeq}', '{ReworkID}'
							, '{DefectCode}', {Qty}, '{ExamType}'
							, '{OrderNo}', '{FromProcessCode}', '{ItemCode}'
							,  '{WorkDate}', GETDATE(), '{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}'
                            )
                            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_WorkDefect()
        {
            query = $@"
UPDATE dbo.QM_WorkDefect
   SET 
       DefectCode = {DefectCode.ConvertStr_Empty_To_Null()}
     , Qty = {Qty}
     , ReasonID = {ReasonID.ConvertStr_Empty_To_Null()}
     , Remark = {Remark.ConvertStr_Empty_To_Null()}
     , ModId = '{WrGlobal.LoginID}'
     , ModIP = '{WrGlobal.ClientHostName}'
     , ModDt = GETDATE()

 WHERE CorpId = '{WrGlobal.CorpID}'
   AND SerialNo = '{SerialNo}'
   AND Seq = '{Seq}'
   AND SubSeq = '{SubSeq}'
   AND ReworkID = '{ReworkID}'
";

            return db.ExecuteQuery(query);
        }


		public bool Upd_QM_WorkDefect_Qty()
		{
			query = $@"
UPDATE dbo.QM_WorkDefect
   SET 
     , Qty = {Qty.ConvertNumeric_Empty_To_Null()}
     , Remark = {Remark.ConvertStr_Empty_To_Null()}
     , ModId = '{WrGlobal.LoginID}'
     , ModIP = '{WrGlobal.ClientHostName}'
     , ModDt = GETDATE()

 WHERE CorpId = '{WrGlobal.CorpID}'
   AND SerialNo = '{SerialNo}'
   AND Seq = '{Seq}'
   AND SubSeq = '{SubSeq}'
   AND ReworkID = '{ReworkID}'
";

			return db.ExecuteQuery(query);
		}


		public bool DelQM_WorkDefect_BySerialNo()
		{
            query = $@"
                DELETE QM_WorkDefect
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND SerialNo = '{SerialNo}'
                
                ";

            return db.ExecuteQuery(query);
		}
    }
}
