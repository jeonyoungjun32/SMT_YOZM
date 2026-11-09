using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.QM
{
    public class QM_WorkDefect
    {
        #region Property
        public string SerialNo { get; set; }        //실적번호
        public string ProcessCode { get; set; }     //공정코드
        public string LineCode { get; set; }     //라인코드
        public string MaterialCode { get; set; }     //공정코드
        public string OrderNo { get; set; }     //공정코드
        public string ExamType { get; set; }
        public string ReWorkID { get; set; }
        public string BadCode { get; set; }
        public string CodeString { get; set; }
        public string DefectCode { get; set; }
        public string ReworkID { get; set; }
        public int Seq { get; set; }
        public int SubSeq { get; set; }
        public int CodeInt { get; set; }
        public float CodeFloat { get; set; }
        public float BadQty { get; set; }
        public float Qty { get; set; }
        #endregion

        Database db = new Database();
        string query = "";


        public DataTable Select_QtBadChk()
        {
            query = $@"
                        SELECT 
                            cd.ProcessID, 
                            cp.ProcessName, 
                            cd.FaultCode, 
                            cd.Title,
                            SUM(ISNULL(wd.Qty, 0)) AS FaultQty,  
                            MAX(wd.ReworkID) AS ReworkID,        
                            MAX(wd.SerialNo) AS SerialNo,        
                            MAX(wd.orderNo) AS orderNo,          
                            MAX(wd.MaterialCode) AS MaterialCode 
                        FROM CM_DefectCode cd
                        LEFT JOIN CM_Process cp ON cd.CorpId = cp.CorpId AND (cd.ProcessID = cp.ProcessCode OR cd.ProcessID = '')
                        LEFT JOIN QM_WorkDefect wd ON cd.CorpId = wd.CorpId AND cd.FaultCode = wd.DefectCode AND wd.SerialNo = '{SerialNo}' AND wd.SubSeq = '{SubSeq}'
                        WHERE cp.CorpId = '{WrGlobal.CorpID}'  
                              AND cp.ProcessCode = '{ProcessCode}' 
                              AND cd.ExamType = '{ExamType}'
                              --AND wd.SerialNo = '{SerialNo}'
                              --AND wd.SubSeq = '{SubSeq}'
                        GROUP BY cd.ProcessID, cp.ProcessName, cd.FaultCode,cd.Title

            ";
            return db.GetDataTable(query);
        }

        public string Get_ReWorkID()
        {
            string query = $@"
                    	-- 1. 부적합 순번 구하기
                    	DECLARE @MaxSeq int
                    , @pCorpId varchar(20) = '{WrGlobal.CorpID}'
                    , @pSerialNo varchar(20) = '{SerialNo}'
                    
                    SET @MaxSeq = (
                        SELECT TOP 1 CONVERT(INT, RIGHT(ReworkID, 4)) + 1
                        FROM QM_WorkDefect
                        WHERE CorpId = @pCorpId
                          AND SerialNo = @pSerialNo
                        ORDER BY CONVERT(INT, RIGHT(ReworkID, 4)) DESC
                    )

                    IF @MaxSeq IS NULL
                        SET @MaxSeq = 1

                    SELECT CONCAT(@pSerialNo, '-') + RIGHT('000' + CONVERT(nvarchar(4), @MaxSeq), 4) AS ReworkID

                    ";

            string rID = db.ExecuteQueryAndReturnResult(query).ConvertString_Null_To_Empty();

            return rID;
        }


		public bool ChkQM_WorkDefect()
		{
			query = $@"SELECT 1
                       FROM QM_WorkDefect
                       WHERE ReworkID = '{ReworkID}'";

			DataRow dr = db.GetDataRow(query);
			bool result = false;

			if(dr!= null)
			{
				result = dr[0].ConvertString_Null_To_Empty() == "1";
			}

			return result;
		}

		public bool Ins_QM_WorkDefect()
		{
			string reworkID = "";
			query = $@"
                    	-- 1. 부적합 순번 구하기
                    	DECLARE @MaxSeq int
                    , @pCorpId varchar(20) = '{WrGlobal.CorpID}'
                    , @pSerialNo varchar(20) = '{SerialNo}'
                    
                    	SELECT @MaxSeq = COUNT(*) + 1
                    	FROM QM_WorkDefect
                    	WHERE CorpId = @pCorpId
                    	AND SerialNo = @pSerialNo
                    
                    SELECT CONCAT(@pSerialNo, '-') + FORMAT(@MaxSeq, 'D4') AS ReworkID
                    ";

			DataRowView drv = db.GetDataRecord(query);

			if (drv != null)
			{
				reworkID = drv["ReworkID"].ConvertString_Null_To_Empty();
			}

			query = $@"
                            INSERT INTO QM_WorkDefect
                            (
                              CorpId, SerialNo, Seq, SubSeq, ReworkID
							, DefectCode, Qty, ExamType
							, OrderNo, FromProcessCode, MaterialCode
                            , WorkDate, CreDate, CreID, CreIP
                            )
                            VALUES
                            (
                              '{WrGlobal.CorpID}', '{SerialNo}', '{Seq}', '{SubSeq}', '{reworkID}'
							, '{DefectCode}', {Qty}, '{ExamType}'
							, '{OrderNo}', '{ProcessCode}', '{MaterialCode}'
							,  GETDATE(), GETDATE(), '{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}'
                            )
                            ";

			return db.ExecuteQuery(query);
		}

		public bool Upd_QM_WorkDefect()
		{
			query = $@"
                            UPDATE QM_WorkDefect
                            SET Qty = {Qty}
							, DefectCode = '{DefectCode}'
							, WorkDate = GETDATE()
                            , ModDate = GETDATE()
                            , ModID   = '{WrGlobal.LoginID}'
                            , ModIP   = '{WrGlobal.ClientHostName}'
                            WHERE CorpId = '{WrGlobal.CorpID}'
							AND ReworkID = '{ReworkID}'
                            ";

			return db.ExecuteQuery(query);
		}

		public DataTable Upd_ResultQty()
        {
            return db.GetDataTable(query);
        }

        public void Del_QtBacChk()
        {
            query = $@" 
                            DELETE FROM QM_WorkDefect 
                                    WHERE CorpID = '{WrGlobal.CorpID}' AND ExamType = '{ExamType}' AND OrderNo = '{OrderNo}' AND SerialNo = '{SerialNo}' AND DefectCode = '{BadCode}' --AND ReworkID = '{ReWorkID}'";
            db.ExecuteQuery(query);
        }
        public void Upd_QtBadChk()
        {
            try
            {
                query = $@"                

                            INSERT INTO QM_WorkDefect
			                (
				                    CorpID, SerialNo, Seq, SubSeq, ReworkID
				                , FromProcessCode, ToProcessCode, ExamType
				                , OrderNo, WorkDate, MaterialCode, DefectCode, Qty
				                , StartDate, EndDate
				                , ReworkProcessCode, RepairID, ReasonID, Remark
				                , CreDate, CreID, CreIP
			                )
			                VALUES
			                (
				                    '{WrGlobal.CorpID}', '{SerialNo}', 1, '{SubSeq}', '{ReWorkID}'
                                , '{ProcessCode}', '', '{ExamType}'
				                , '{OrderNo}', GETDATE(), '{MaterialCode}', '{BadCode}', '{BadQty}'
				                , GETDATE(), GETDATE()
                                , '', '', '', ''
                                , GETDATE(), '', ''			
			                )
                                                    ";

                db.ExecuteQuery(query);

            }
            catch (Exception ex)
            {
            
            }
        }

		public DataTable Select_BadByLine()
		{
			query = $@"

						DECLARE @COLUMNS   VARCHAR(MAX)
						,@SQL			   VARCHAR(MAX)
						,@LineCd		   VARCHAR(MAX)
						,@RstNo			   VARCHAR(MAX)
						,@RstRowSeq		   VARCHAR(MAX)

						SET @COLUMNS = ''
						SET @LineCd = '{LineCode}'
						SET @RstNo = '{SerialNo}'
						--SET @RstRowSeq = ''

						SELECT @COLUMNS = @COLUMNS + '[' + Bad_Nm + '],'
						  FROM (
								 SELECT 	   
										 Bad_Cd
									 , Bad_Nm
									 , Process_Cd
									 , Line_Cd
								   FROM BS_Bad
								   WHERE Corp_Cd = '{WrGlobal.CorpID}' AND Line_Cd = @LineCd 
							   ) AS bad
						 ORDER BY Bad_Cd

						--select @COLUMNS
						IF @COLUMNS = '' --라인/공정 불량유형 미등록시 리턴
							RETURN

						SET @COLUMNS = LEFT(@COLUMNS, LEN(@COLUMNS) - 1)
						

						SET @SQL = ' 
    
								select  *
								from 
								(
										select  Bad_Nm, CONVERT(varchar, sum(Bad_Qty)) as Bad_QTy
										from
										(
											select a.Bad_Nm, a.Bad_Cd, 0 as Bad_Qty
											from BS_Bad a
											where Corp_Cd = ''{WrGlobal.CorpID}'' AND Line_Cd = ''' + @LineCd + '''

											union all

											select a.Bad_Nm, a.Bad_Cd, CONVERT(int, Bad_Qty) AS Bad_Qty
											from BS_Bad a
											right join QT_Bad_Chk b on (b.Bad_Cd = a.Bad_Cd)
											where a.Corp_Cd = ''{WrGlobal.CorpID}''
											and a.line_cd = ''' + @LineCd + '''
											and b.Rst_No = '''+@RstNo+'''
											and b.Rst_Row_Seq = ''' + @RstRowSeq + '''
										)c
										group by Bad_Nm, Bad_Cd
								)  as Result
								PIVOT ( 
												max(Bad_Qty) FOR Bad_Nm IN ( ' + @COLUMNS + ' )
										 ) AS pivot_result

								union all

								select  *
								from 
								(
										select  Bad_Nm, '''+'▲'+''' as UP
										from
										(
											select a.Bad_Nm, a.Bad_Cd, 0 as Bad_Qty
											from BS_Bad a
											where Corp_Cd =''{WrGlobal.CorpID}'' AND Line_Cd = ''' + @LineCd + '''

											union all

											select a.Bad_Nm, a.Bad_Cd, Bad_Qty
											from BS_Bad a
											right join QT_Bad_Chk b on (b.Bad_Cd = a.Bad_Cd)
											where a.Corp_Cd = ''{WrGlobal.CorpID}''
											and a.line_cd = ''' + @LineCd + '''
											and b.Rst_No = '''+@RstNo+'''
											and b.Rst_Row_Seq = ''' + @RstRowSeq + '''
										)c
										group by Bad_Nm, Bad_Cd
								)  as Result
								PIVOT ( 
												max(UP) FOR Bad_Nm IN ( ' + @COLUMNS + ' )
										 ) AS pivot_result

								union all

								select  *
								from 
								(
										select  Bad_Nm, '''+'▼'+''' as DOWN
										from
										(
											select a.Bad_Nm, a.Bad_Cd, 0 as Bad_Qty
											from BS_Bad a
											where Corp_Cd = ''{WrGlobal.CorpID}'' AND Line_Cd = ''' + @LineCd + '''

											union all

											select a.Bad_Nm, a.Bad_Cd, Bad_Qty
											from BS_Bad a
											right join QT_Bad_Chk b on (b.Bad_Cd = a.Bad_Cd)
											where a.Corp_Cd = ''{WrGlobal.CorpID}''
											and a.line_cd = ''' + @LineCd + '''
											and b.Rst_No = '''+@RstNo+'''
											and b.Rst_Row_Seq = ''' + @RstRowSeq + '''
										)c
										group by Bad_Nm, Bad_Cd
								)  as Result
								PIVOT ( 
												max(DOWN) FOR Bad_Nm IN ( ' + @COLUMNS + ' )
										 ) AS pivot_result

								union all

								select  *
								from 
								(
										select  Bad_Nm, Bad_Cd
										from
										(
											select a.Bad_Nm, a.Bad_Cd, 0 as Bad_Qty
											from BS_Bad a
											where Corp_Cd = ''{WrGlobal.CorpID}'' AND Line_Cd = ''' + @LineCd + '''

											union all

											select a.Bad_Nm, a.Bad_Cd, Bad_Qty
											from BS_Bad a
											right join QT_Bad_Chk b on (b.Bad_Cd = a.Bad_Cd)
											where a.Corp_Cd =''{WrGlobal.CorpID}''
											and a.line_cd = ''' + @LineCd + '''
											and b.Rst_No = '''+@RstNo+'''
											and b.Rst_Row_Seq = ''' + @RstRowSeq + '''
										)c
										group by Bad_Nm, Bad_Cd
								)  as Result
								PIVOT ( 
												max(Bad_Cd) FOR Bad_Nm IN ( ' + @COLUMNS + ' )
										 ) AS pivot_result
						'

						--select @SQL

						EXEC(@SQL)  ";

			return db.GetDataTable(query);
		}

		public DataTable Select_NgQTY()
		{
			SerialNo = SerialNo.ConvertString_Null_To_Empty();

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
							, MAX(MaterialCode) as MaterialCode
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
								, null as MaterialCode
								, null as WorkDate
							from CM_DefectCode cd
							WHERE cd.CorpId = '{WrGlobal.CorpID}' ";

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
								, odr.MaterialCode
								, odr.WorkDate
							FROM QM_WorkDefect wd
							RIGHT JOIN PM_OrderResult odr on wd.CorpId = odr.CorpID and wd.SerialNo = odr.SerialNo
							WHERE wd.CorpId = '{WrGlobal.CorpID}' ";

			if (SerialNo != "")
			{ query += $@"AND wd.SerialNo = '{SerialNo}'
                        AND wd.SubSeq = {SubSeq}"; }

			query += $@"
						) as a
						GROUP BY CorpId, FaultCode
                        ORDER BY FaultCode";

			return db.GetDataTable(query);
		}

		public DataTable Upd_ProdIn_Qty_ByRst()
        {
            return db.GetDataTable(query);
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

		public void Del_QM_WorkDefect_BySerialNo()
		{
			query = $@" 
				DELETE QM_WorkDefect
				WHERE CorpId = '{WrGlobal.CorpID}'
				AND SerialNo = '{SerialNo}'
				AND ExamType = '{ExamType}'
				";
			db.ExecuteQuery(query);
		}

		public bool DelQM_WorkDefect()
		{
			query = $@"
                DELETE QM_WorkDefect
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND SerialNo = '{SerialNo}'
				AND Seq = '{Seq}'
				AND SubSeq = '{SubSeq}'
				AND ReworkID = '{ReworkID}'
                
                ";

			return db.ExecuteQuery(query);
		}
	}
}
