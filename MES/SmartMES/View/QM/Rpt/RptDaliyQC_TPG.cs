using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;


namespace SMT_MES.View.Quality.Rpt
{
	public partial class RptDaliyQC_TPG : DevExpress.XtraReports.UI.XtraReport
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";

        public string g_Rst_No = "";
        public string G_Rst_Row_Seq = "";
        public string ExamDivCd = "10"; //016 - 10 : 수입검사, 20 : 출하검사, 30 : 공정검사

        public RptDaliyQC_TPG()
        {
            InitializeComponent();
        }

        public RptDaliyQC_TPG(string examDt)
        {
            InitializeComponent();

            query = $@"DECLARE @BaseDate DATE = '{examDt}';  -- 기준일자
                        SET DATEFIRST 1; -- 월요일 시작

                        DECLARE @Mon DATE = DATEADD(DAY, 1 - DATEPART(WEEKDAY, @BaseDate), @BaseDate);
                        DECLARE @Fri DATE = DATEADD(DAY, 4, @Mon);

                        -- 기간 문자열
                        DECLARE @PeriodText NVARCHAR(100) =
                            FORMAT(@Mon, 'yyyy년 MM월 dd일') + ' ~ ' +
                            FORMAT(@Fri, 'yyyy년 MM월 dd일');

                        DECLARE @cols NVARCHAR(MAX)='';
                        DECLARE @sql  NVARCHAR(MAX)='';

                        -- 작업장 목록
                        WITH W AS (
                            SELECT ItemCode AS Workplace
                            FROM CM_CateItem
                            WHERE CLASS = 'A124'
                        ),

                        -- 요일
                        D AS (
                            SELECT '월' AS WN UNION ALL
                            SELECT '화' UNION ALL
                            SELECT '수' UNION ALL
                            SELECT '목' UNION ALL
                            SELECT '금'
                        ),

                        -- 검사명
                        T AS (
                            SELECT '연질' AS TN UNION ALL
                            SELECT '경질' UNION ALL
                            SELECT '금속' UNION ALL
                            SELECT '기타'
                        ),

                        COLS AS (
                            SELECT Workplace + '_' + WN + '_' + TN AS ColName
                            FROM W CROSS JOIN D CROSS JOIN T
                        )

                        SELECT @cols = STRING_AGG(QUOTENAME(ColName), ',')
                        FROM COLS;

                        SET @sql = '
                        ;WITH PVT AS (
                            SELECT ' + @cols + '
                            FROM (
                                SELECT
                                    Workplace + ''_'' +
                                    CASE DATEPART(WEEKDAY, CONVERT(date, ExamDate,112))
                                        WHEN 1 THEN ''월''
                                        WHEN 2 THEN ''화''
                                        WHEN 3 THEN ''수''
                                        WHEN 4 THEN ''목''
                                        WHEN 5 THEN ''금''
                                    END + ''_'' +
                                    CASE ExamCd
                                        WHEN ''ECT001'' THEN ''연질''
                                        WHEN ''ECT064'' THEN ''경질''
                                        WHEN ''ECT065'' THEN ''금속''
                                        WHEN ''ECT066'' THEN ''기타''
                                    END AS ColName,
                                    TRY_CAST(X1 AS INT) AS X1
                                FROM QM_ExamResult_Daily
                                WHERE ExamDivCd = ''53''
                                  AND CONVERT(date, ExamDate,112) BETWEEN ''' + CONVERT(VARCHAR,@Mon,23) + ''' 
                                                                       AND ''' + CONVERT(VARCHAR,@Fri,23) + '''
                            ) SRC
                            PIVOT (
                                MAX(X1) FOR ColName IN (' + @cols + ')
                            ) P
                        )

                        SELECT ''' + @PeriodText + ''' AS PeriodText, *
                        FROM PVT

                        UNION ALL

                        SELECT ''' + @PeriodText + ''', ' + REPLACE(@cols, '[', 'CAST(0 AS INT) AS [') + '
                        WHERE NOT EXISTS (SELECT 1 FROM PVT);
                        ';

                        EXEC(@sql);
            ";

            DataTable dt = db.GetDataTable(query);
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            this.DataSource = dt;
        }
    }
}
