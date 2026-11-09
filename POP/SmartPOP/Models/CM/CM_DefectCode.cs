using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.CM
{
	class CM_DefectCode
	{
		#region Property
		public string CorpId { get; set; }

		public string FaultCode { get; set; }

		public string ProcessID { get; set; }

		public string Title { get; set; }

		public string ClassID { get; set; }

		public string ExamType { get; set; }

		public string Remark { get; set; }

		public string UseYn { get; set; }

		public string CreId { get; set; }

		public string CreIP { get; set; }

		public DateTime? CreDt { get; set; }

		public string ModId { get; set; }

		public string ModIP { get; set; }

		public DateTime? ModDt { get; set; }
		#endregion

		Database db = new Database();
		string query = "";

		public DataTable Select_BadByProcess_OrderResult(string serialNo)
		{
			query = $@"
						DECLARE @COLUMNS   VARCHAR(MAX)
						,@SQL			   VARCHAR(MAX)
						,@ProcessCode		  VARCHAR(MAX)
						,@SerialNo		   VARCHAR(MAX)
						, @CorpId			VARCHAR(MAX) = '{WrGlobal.CorpID}'

						SET @COLUMNS = ''
						SET @ProcessCode = '{ProcessID}'
						SET @SerialNo = '{serialNo}'

						SELECT @COLUMNS = @COLUMNS + '[' + Title + '],'
						  FROM (
								 SELECT 	   
										 FaultCode
									 , Title
									 , ProcessID
								   FROM CM_DefectCode
								   WHERE CorpId = @CorpId 
								   AND ExamType = '10' --10:생산불량, 20:검사불량
								   AND ProcessID = @ProcessCode 
								   AND POPDisplayYn = 'Y'
							   ) AS bad
						 ORDER BY FaultCode

						--select @COLUMNS
						IF @COLUMNS = '' --라인/공정 불량유형 미등록시 리턴
							RETURN

						SET @COLUMNS = LEFT(@COLUMNS, LEN(@COLUMNS) - 1)
						

						SET @SQL = ' 
    
								select  *
								from 
								(
										select  Title, CONVERT(varchar, sum(Qty)) as Qty
										from
										(
											select a.Title, a.FaultCode, 0 as Qty
											from CM_DefectCode a
											where CorpId = ''' + @CorpId + ''' AND ProcessID = ''' + @ProcessCode + '''

											union all

											select a.Title, a.FaultCode, CONVERT(float, Qty) AS Qty
											from CM_DefectCode a
											right join QM_WorkDefect b on (b.DefectCode = a.FaultCode)
											where a.CorpId = ''' + @CorpId + ''' 
											and a.ProcessID = ''' + @ProcessCode + '''
											and b.SerialNo = '''+@SerialNo+'''
										)c
										group by Title, FaultCode
								)  as Result
								PIVOT ( 
												max(Qty) FOR Title IN ( ' + @COLUMNS + ' )
										 ) AS pivot_result

								union all

								select  *
								from 
								(
										select  Title, '''+'▲'+''' as UP
										from
										(
											select a.Title, a.FaultCode, 0 as Qty
											from CM_DefectCode a
											where CorpId = ''' + @CorpId + '''  AND ProcessID = ''' + @ProcessCode + '''

											union all

											select a.Title, a.FaultCode, Qty
											from CM_DefectCode a
											right join QM_WorkDefect b on (b.DefectCode = a.FaultCode)
											where a.CorpId = ''' + @CorpId + ''' 
											and a.ProcessID = ''' + @ProcessCode + '''
											and b.SerialNo = '''+@SerialNo+'''
										)c
										group by Title, FaultCode
								)  as Result
								PIVOT ( 
												max(UP) FOR Title IN ( ' + @COLUMNS + ' )
										 ) AS pivot_result

								union all

								select  *
								from 
								(
										select  Title, '''+'▼'+''' as DOWN
										from
										(
											select a.Title, a.FaultCode, 0 as Qty
											from CM_DefectCode a
											where CorpId = ''' + @CorpId + '''  AND ProcessID = ''' + @ProcessCode + '''

											union all

											select a.Title, a.FaultCode, Qty
											from CM_DefectCode a
											right join QM_WorkDefect b on (b.DefectCode = a.FaultCode)
											where a.CorpId = ''' + @CorpId + ''' 
											and a.ProcessID = ''' + @ProcessCode + '''
											and b.SerialNo = '''+@SerialNo+'''
										)c
										group by Title, FaultCode
								)  as Result
								PIVOT ( 
												max(DOWN) FOR Title IN ( ' + @COLUMNS + ' )
										 ) AS pivot_result

								union all

								select  *
								from 
								(
										select  Title, FaultCode
										from
										(
											select a.Title, a.FaultCode, 0 as Qty
											from CM_DefectCode a
											where CorpId =''' + @CorpId + '''  AND ProcessID = ''' + @ProcessCode + '''

											union all

											select a.Title, a.FaultCode, Qty
											from CM_DefectCode a
											right join QM_WorkDefect b on (b.DefectCode = a.FaultCode)
											where a.CorpId = ''' + @CorpId + ''' 
											and a.ProcessID = ''' + @ProcessCode + '''
											and b.SerialNo = '''+@SerialNo+'''
										)c
										group by Title, FaultCode
								)  as Result
								PIVOT ( 
												max(FaultCode) FOR Title IN ( ' + @COLUMNS + ' )
										 ) AS pivot_result
						'

						--SELECT @SQL
						EXEC(@SQL)  
					";

			return db.GetDataTable(query);
		}

	}
}
