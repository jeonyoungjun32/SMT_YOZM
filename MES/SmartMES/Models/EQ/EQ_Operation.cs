using System;
using System.Data;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.EQ
{
    public class EQ_Operation
    {

        #region Property

        public string MachineCode { get; set; }
        public string WorkDay { get; set; }
        public int Seq { get; set; }
        public string ProcessCode { get; set; }
        public string LineCode { get; set; }
        public string OperationYn { get; set; }
        public string CLASS { get; set; }
        public string ItemCode { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int StopTime { get; set; }
        public string OrderNo { get; set; }
        public int OrderSeq { get; set; }
        public string Remark { get; set; }
        public string OperationMeasure { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_EQ_Operation(DateTime startDt, DateTime endDt)
        {
            query = $@"
                        SELECT '' Status
                            , mo.CorpId
                            , CONVERT(DATE, mo.WorkDay) WorkDay

                            , mo.MachineCode
                            , cm.MachineName

                            , mo.Seq
                            , mo.ProcessCode
                            , mo.LineCode
                            , mo.OperationYn
                            , mo.CLASS
                            , mo.ClassItemCode

                            , mo.StartTime
                            , mo.EndTime
                            , mo.StopTime

                            , mo.OrderNo
                            , mo.OrderSeq
                            , mo.Remark
                            , mo.OperationMeasure

                            , mo.CreId
                            , mo.CreIP
                            , mo.CreDt
                            , mo.ModId
                            , mo.ModIP
                            , mo.ModDt

                        FROM EQ_Operation mo
                        LEFT JOIN EQ_Machine cm ON mo.CorpId = cm.CorpId AND mo.MachineCode = cm.MachineCode
                        WHERE mo.CorpId = '{WrGlobal.CorpID}'
                          AND MO.WorkDay BETWEEN '{startDt:yyyyMMdd}' AND '{endDt:yyyyMMdd}'    
                    ";

            if(MachineCode.ConvertString_Null_To_Empty() != "ALL" && MachineCode.ConvertString_Null_To_Empty() != "")
			{
                query += $"\nAND mo.MachineCode = '{MachineCode}'";
			}

            query += "ORDER BY mo.WorkDay DESC, mo.Seq DESC";

            return db.GetDataTable(query);
        }

        public DataTable Select_EQ_OperationView(string startDate, string endDate)
        {
            query = $@"
                        SELECT
                            CorpId
                            , CONVERT(CHAR(10), CONVERT(DATE, WorkDay, 112), 23) WorkDay
                            , MachineCode
                            , MachineCode MachineName
                            , Seq
                            , ProcessCode
                            , LineCode
                            , OperationYn
                            , CLASS
                            , ItemCode
                            , CONVERT(CHAR(16), StartTime, 120) StartTime
                            , CONVERT(CHAR(16), EndTime, 120) EndTime
                            , OrderNo
                            , OrderSeq
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM EQ_Operation
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND WorkDay BETWEEN '{startDate}' AND '{endDate}'
                    ";

            if (MachineCode != "ALL")
            {
                query += $"AND MachineCode = '{MachineCode}'";
            }

            return db.GetDataTable(query);
        }

        public DataRow Select_EQ_Operation_ByCode()
        {
            query = $@"
                        SELECT
                            CorpId
                            , MachineCode
                            , Seq
                            , CONVERT(CHAR(10), CONVERT(DATE, WorkDay, 112), 23) WorkDay
                            , ProcessCode
                            , LineCode
                            , OperationYn
                            , CLASS
                            , ItemCode
                            , StartTime
                            , EndTime
                            , OrderNo
                            , OrderSeq
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM EQ_Operation
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MachineCode = '{MachineCode}'
                        AND Seq = {Seq}
                    ";

            return db.GetDataRow(query);
        }

        public bool Ins_EQ_Operation()
        {
            query = $@"
                        INSERT INTO EQ_Operation
                        (
                            CorpId
                            , WorkDay
                            , ProcessCode
                            , LineCode
                            , MachineCode
                            , Seq
                            , OperationYn
                            , CLASS
                            , ClassItemCode
                            , StartTime
                            , EndTime
                            , StopTime
                            , OrderNo
                            , OrderSeq
                            , Remark
                            , OperationMeasure
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{WorkDay}'
                            , '{ProcessCode}'
                            , '{LineCode}'
                            , '{MachineCode}'
                            , {Seq}
                            , '{OperationYn}'
                            , '{CLASS}'
                            , '{ItemCode}'
                            , '{StartTime}'
                            , '{EndTime}'
                            , {StopTime.ConvertInt_Empty_To_Null()}
                            , {OrderNo.ConvertStr_Empty_To_Null()}
                            , {OrderSeq.ConvertInt_Empty_To_Null()}
                            , {Remark.ConvertStr_Empty_To_Null()}
                            , {OperationMeasure.ConvertStr_Empty_To_Null()}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_EQ_Operation()
        {
            query = $@"
                        UPDATE EQ_Operation
                        SET
                            WorkDay = '{WorkDay}'
                            , ProcessCode = '{ProcessCode}'
                            , LineCode = '{LineCode}'
                            , OperationYn = '{OperationYn}'
                            , CLASS = '{CLASS}'
                            , ClassItemCode = '{ItemCode}'
                            , StartTime = '{StartTime}'
                            , EndTime = '{EndTime}'
                            , StopTime = {StopTime.ConvertInt_Empty_To_Null()}
                            , Remark = {Remark.ConvertStr_Empty_To_Null()}
                            , OperationMeasure = {OperationMeasure.ConvertStr_Empty_To_Null()}
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MachineCode = '{MachineCode}'
                        AND WorkDay = '{WorkDay}'
                        AND Seq = {Seq}
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_EQ_Operation()
        {
            query = $@"
                        DELETE EQ_Operation 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND MachineCode = '{MachineCode}' 
                        AND Seq = {Seq}
                    ";

            return db.ExecuteQuery(query);
        }

        public DataRow Get_LastEQ_OperationSeq()
        {
            query = $@"
                        SELECT ISNULL(MAX(Seq), 0) + 1 Seq 
                        FROM EQ_Operation 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND WorkDay = '{WorkDay}'
                    ";

            return db.GetDataRow(query);
        }

        public DataTable Get_MachineRunRate(string startDate, string endDate, string type)
        {
            query = $@"'
                        DECLARE @SQL NVARCHAR(MAX)
                        DECLARE @COL NVARCHAR(MAX)
                        DECLARE @COL_Value NVARCHAR(MAX)
                        DECLARE @PivotColumns NVARCHAR(MAX)
                        DECLARE @NonWorkTimeColumn NVARCHAR(MAX)

                        SELECT 
	                        @PIVOTCOLUMNS = STRING_AGG(QUOTENAME('Title' + CAST(Seq AS NVARCHAR)), ', ')
	                        , @NonWorkTimeColumn = STRING_AGG('ISNULL(Title' + CAST(Seq AS NVARCHAR) + ', 0)', ' + ')
	                        , @COL_Value = STRING_AGG('ISNULL(Title' + CAST(Seq AS NVARCHAR) + ', 0) AS Title' + CAST(Seq AS NVARCHAR), ', ')
	                        , @COL = STRING_AGG('CAST(ISNULL(Title' + CAST(Seq AS NVARCHAR) + ', 0) / 60 AS VARCHAR) + ''시간 '' + CAST(ISNULL(Title' + CAST(Seq AS NVARCHAR) + ', 0) % 60 AS VARCHAR) + ''분'' AS Title' + CAST(Seq AS NVARCHAR), ', ')
                        FROM
                        (
	                        SELECT
		                        ROW_NUMBER() OVER(ORDER BY ItemCode) AS Seq
	                        FROM CM_CateItem
	                        WHERE CLASS = '2030M'
                        ) AS T

                        --SELECT @PivotColumns, @NonWorkTimeColumn, @COL


                        SET @SQL = '
                        SELECT
	                        CM.MachineCode
                            , CM.MachineName
	                        , CAST(ISNULL(CCI.WorkTime, 0) / 60 AS VARCHAR) + ''시간 '' + CAST(ISNULL(CCI.WorkTime, 0) % 60 AS VARCHAR) + ''분'' AS WorkTime
	                        , CAST((ISNULL(CCi.WorkTime, 0) - ISNULL(T2.NonWorkTime, 0)) / 60 AS VARCHAR) + ''시간'' + CAST((ISNULL(CCi.WorkTime, 0) - ISNULL(T2.NonWorkTime, 0)) % 60 AS VARCHAR) + ''분'' AS RealWorkTime
	                        , CAST(ISNULL(T2.NonWorkTime, 0) / 60 AS VARCHAR) + ''시간'' + CAST(ISNULL(T2.NonWorkTime, 0) % 60 AS VARCHAR) + ''분'' AS NonWorkTime
	                        , ' + @COL + '
	                        , ((CAST(ISNULL(CCi.WorkTime, 0) AS FLOAT) - CAST(ISNULL(T2.NonWorkTime, 0) AS FLOAT)) / CAST(ISNULL(CCI.WorkTime, 0) AS FLOAT) * 100) AS Rate
	                        , ISNULL(CCI.WorkTime, 0) AS WorkTime_Value
	                        , ISNULL(CCi.WorkTime, 0) - ISNULL(T2.NonWorkTime, 0) AS RealWorkTime_Value
	                        , ISNULL(T2.NonWorkTime, 0) AS NonWorkTime_Value
	                        , ' + @COL_Value +'
                        FROM EQ_Machine CM
                        LEFT JOIN
                        (
	                        SELECT Title * 60 * (DATEDIFF(DAY, ''{startDate}'', ''{endDate}'') + 1) AS WorkTime FROM CM_CateItem WHERE CLASS = ''2080'' AND ItemCode = ''{type}''	-- 분으로 연산
                        ) CCI ON 1 = 1
                        LEFT JOIN
                        (
	                        SELECT
		                        *
		                        , ' + @NonWorkTimeColumn + ' NonWorkTime
	                        FROM
	                        (
		                        SELECT
			                        MachineCode
			                        --, MO.ItemCode
			                        , ''Title'' + CAST(CCI.Seq AS NVARCHAR) Title
			                        , DATEDIFF(MINUTE, StartTime, EndTime) AS Minute
		                        FROM EQ_Operation MO
		                        LEFT JOIN
		                        (
			                        SELECT
				                        ItemCode
				                        , ROW_NUMBER() OVER(ORDER BY ItemCode) AS Seq
			                        FROM CM_CateItem
			                        WHERE CLASS = ''2030M''
		                        ) CCI ON MO.ItemCode = Cci.ClassItemCode
		                        WHERE WorkDay BETWEEN ''{startDate}'' AND ''{endDate}''
		                        AND OperationYn = ''N''
	                        ) AS T1
	                        PIVOT
	                        (
		                        SUM(T1.Minute) FOR T1.Title IN (' + @PivotColumns + ')
	                        ) AS PIVOT_T1
                        ) T2 ON CM.MachineCode = T2.MachineCode
                        '

                        EXEC sp_executesql @SQL;
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Get_EQ_OperationRate(string startDate, string endDate)
        {
            query = $@"
                        SELECT
	                        MO.ItemCode
	                        , CCI.Title
	                        , SUM(DATEDIFF(MINUTE, MO.StartTime, MO.EndTime)) Total
                        FROM EQ_Operation MO
                        LEFT JOIN CM_CateItem CCI ON MO.ItemCode = Cci.ClassItemCode AND CCI.CLASS = '2030M'
                        WHERE MO.WorkDay BETWEEN '{startDate}' AND '{endDate}'
                        GROUP BY MO.ItemCode, CCI.Title
                        ORDER BY MO.ItemCode
                    ";

            return db.GetDataTable(query);
        }
    }
}
