using DevExpress.CodeParser;
using DevExpress.DashboardCommon;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models.PM;
using SmartMES.Models.SM;
using SmartMES.View.BM;
using SmartMES.View.EQ;
using SmartMES.View.MM;
using SmartMES.View.PM;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Monitoring
{
    public partial class Frm_MM_QualityDashboard : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        Database db = new Database();
        string query = "";

        public Frm_MM_QualityDashboard()
        {
            InitializeComponent();

            // 상단 조회조건 색상 설정
            smtCommonBar1.smtBackColor = Color.Transparent;
            btn_Close.ForeColor = Color.Black;
        }

        private void Frm_MM_QualityDashboard_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            SetGrid_DefectTop5(Gv_DefectTop5);
            SetGrid_WorkDefect(Gv_WorkDefect);

            Search();

            Tm_Search.Interval = 360000;
            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)
        private void SetGrid_DefectTop5(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "순위", "RankNo");
            grid.Create_GvColumn(gv, "부적합 내용", "Title");
            grid.Create_GvColumn(gv, "발생 건수", "DefectCount");
            grid.Create_GvColumn(gv, "비율(%)", "DefectRate");
            //grid.Create_GvColumn(gv, "조치상태", "Status");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["DefectCount"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["DefectRate"], clsGrid.TypeList.TFloatN1);

            gv.Columns["RankNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["Title"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.RowCellStyle += Gv_RowCellStyle;
        }

        private void SetGrid_WorkDefect(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "발생일자", "WorkDate");
            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "부적합 내용", "Title");
            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "작업 상태", "Status");

            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkDate"], clsGrid.TypeList.TDate);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string value = e.CellValue.ConvertString_Null_To_Empty();

   //         if (e.Column.FieldName == "Remark")
   //         {
   //             if (value == "지연")
   //             {
   //                 e.Appearance.BackColor = Color.FromArgb(255, 243, 205); // 연한 노란색
   //                 e.Appearance.ForeColor = Color.FromArgb(180, 120, 0);   // 진한 주황/갈색
   //             }
   //             else if (value == "정상")
   //             {
   //                 e.Appearance.BackColor = Color.FromArgb(220, 245, 230); // 연한 초록색
   //                 e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
			//	}
			//}
			//else
            if (e.Column.FieldName == "Status" || e.Column.FieldName == "ProcessStatus" || e.Column.FieldName == "WorkStatus") //
            {
				if (value == "가동중" || value == "진행중" || value == "작업중" )
                {
                    e.Appearance.BackColor = Color.FromArgb(220, 245, 230); // 연한 초록색
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
                }
                else if (value == "정지")
                {
                    e.Appearance.BackColor = Color.FromArgb(253, 232, 232); // 연한 분홍/파스텔 레드
                    e.Appearance.ForeColor = Color.FromArgb(200, 50, 50);    // 진한 빨간색
                }
				else
				{
					e.Appearance.ForeColor = Color.FromArgb(100, 110, 120); // 진한 회색
				}
            }
        }

        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            SearchCardData();

            Search_Chart_GoodRate();
            Search_Chart_NgRate();
            Search_Chart_NgRateByProcess();
            Search_Chart_NgByExam();
            Search_Chart_DefectType();
            Search_Chart_DefectStatus();
            Search_Chart_ExamResult();

            SearchGrid_DefectTop5();
            SearchGrid_WorkDefect();
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            query = $@"
            DECLARE @Today        CHAR(8)  = CONVERT(CHAR(8), GETDATE(), 112); -- 오늘 날짜 (YYYYMMDD 기준, 형식에 따라 'YYYY-MM-DD' 변경)
            DECLARE @StartOfMonth CHAR(8)  = CONVERT(CHAR(8), DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0), 112); -- 당월 1일
            DECLARE @CorpId       VARCHAR(10) = '{WrGlobal.CorpID}';

            WITH ProdData AS (
                SELECT
                    ISNULL(SUM(WorkQty), 0) AS TotalWorkQty
                    , ISNULL(SUM(GoodQty), 0) AS TotalGoodQty
                    , ISNULL(SUM(NgQty), 0)   AS TotalNgQty
                FROM PM_OrderResult
                WHERE CorpId = @CorpId
                  AND WorkDate BETWEEN @StartOfMonth AND @Today
            ),
            ExamData AS (
                --  검사건수 및 부적합건수 집계
                SELECT
                    COUNT(DISTINCT ExamNo) AS TotalExamCount,
                    COUNT(DISTINCT CASE WHEN PassDivCd <> '10' THEN ExamNo END) AS NgCount
                FROM QM_ExamResult
                WHERE CorpId = @CorpId
                  AND ExamDate BETWEEN @StartOfMonth AND @Today
            ),
            ReworkData AS (
                SELECT
                    ISNULL(SUM(CASE WHEN OP.Status = '재작업' THEN 1 ELSE 0 END), 0) AS ReworkCount
                FROM QM_WorkDefect WD
                LEFT JOIN PM_OrderResult ORS ON WD.CorpId = ORS.CorpId AND WD.SerialNo = ORS.SerialNo
                LEFT JOIN PM_OrderProcess OP ON ORS.CorpId = OP.CorpId AND ORS.OrderNo = OP.OrderNo AND ORS.OrderSeq = OP.OrderSeq
                WHERE WD.CorpId = @CorpId
                  AND WD.WorkDate BETWEEN @StartOfMonth AND @Today
            )
            SELECT
                -- 전체 양품률
                CASE
                    WHEN P.TotalWorkQty = 0 THEN 0.0
                    ELSE ROUND((CAST(P.TotalGoodQty AS FLOAT) / P.TotalWorkQty) * 100, 2)
                END AS GoodRate

                -- 전체 불량률 (PPM)
                , CASE
                    WHEN P.TotalWorkQty = 0 THEN 0.0
                    ELSE ROUND((CAST(P.TotalNgQty AS FLOAT) / P.TotalWorkQty) * 1000000, 0)
                END AS NgPPM

                -- 검사건수
                , E.TotalExamCount

                -- 부적합건수
                , E.NgCount

                -- 재작업건수
                , R.ReworkCount

            FROM ProdData P
            CROSS JOIN ExamData E
            CROSS JOIN ReworkData R;
            ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                LblGoodRate.Text = dr["GoodRate"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblNgRate.Text = dr["NgPPM"].ConvertFloat_Null_To_Zero().ToString("N0") + " PPM";
                LblInspectionQty.Text = dr["TotalExamCount"].ConvertFloat_Null_To_Zero().ToString("N0") + " 건";
                LblRework.Text = dr["ReworkCount"].ConvertFloat_Null_To_Zero().ToString("N0") + " 건";
                LblNgQty.Text = dr["NgCount"].ConvertInt_Null_To_Zero().ToString("N0") + " 건";
            }
        }
        #endregion

        #region 차트 로드
        private void Search_Chart_GoodRate()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 양품률 추이
            query = $@"
            DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}';

            WITH Calendar AS (
                SELECT CONVERT(CHAR(8), DATEADD(DAY, -6, GETDATE()), 112) AS WorkDate
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -5, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -4, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -3, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -2, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -1, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), GETDATE(), 112)
            ),
            Prod AS (
                SELECT
                    WorkDate
                    , ISNULL(SUM(WorkQty), 0) AS TotalWorkQty
                    , ISNULL(SUM(GoodQty), 0) AS TotalGoodQty
                FROM PM_OrderResult
                WHERE CorpId = @CorpId
                  AND WorkDate >= CONVERT(CHAR(8), DATEADD(DAY, -6, GETDATE()), 112)
                  AND WorkDate <= CONVERT(CHAR(8), GETDATE(), 112)
                GROUP BY WorkDate
            )
            SELECT
                C.WorkDate
                , SUBSTRING(C.WorkDate, 5, 2) + '-' + SUBSTRING(C.WorkDate, 7, 2) AS DisplayDate
                , ISNULL(P.TotalWorkQty, 0) AS TotalWorkQty
                , ISNULL(P.TotalGoodQty, 0) AS TotalGoodQty
                , CASE
                    WHEN ISNULL(P.TotalWorkQty, 0) = 0 THEN 0.0
                    ELSE ROUND((CAST(P.TotalGoodQty AS FLOAT) / P.TotalWorkQty) * 100, 1)
                END AS GoodRate

            FROM Calendar C
            LEFT JOIN Prod P ON C.WorkDate = P.WorkDate
            ORDER BY C.WorkDate;
            ";

            DataTable dt = db.GetDataTable(query);

            chartGoodRate.DataSource = dt;

            ChartControl chart = chartGoodRate;
            chart.Series.Clear();
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BackColor = Color.White;

            // 범례 설정
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series sGoodRate = new Series("양품률(%)", ViewType.Line);
            sGoodRate.ArgumentDataMember = "DisplayDate";
            sGoodRate.ValueDataMembers.AddRange(new string[] { "GoodRate" });
            sGoodRate.DataSource = dt;

            sGoodRate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            sGoodRate.Label.TextPattern = "{V:F1}%";
            sGoodRate.CrosshairLabelPattern = "{A} : {V:F1}%";

            // PointSeriesLabel 설정
            PointSeriesLabel label = sGoodRate.Label as PointSeriesLabel;
            if (label != null)
            {
                label.BackColor = Color.Transparent;
                label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            // 라인 및 마커 디자인 설정
            LineSeriesView lineView = (LineSeriesView)sGoodRate.View;
            lineView.Color = Color.FromArgb(24, 100, 230);
            lineView.LineStyle.Thickness = 2;

            // 원형 마커 설정
            lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
            lineView.LineMarkerOptions.Size = 8;
            lineView.LineMarkerOptions.Color = Color.FromArgb(24, 100, 230);
            lineView.LineMarkerOptions.FillStyle.FillMode = FillMode.Solid;

            chart.Series.Add(sGoodRate);

            // 축(Diagram) 상세 설정
            XYDiagram diagram = chart.Diagram as XYDiagram;
            if (diagram != null)
            {
                diagram.AxisX.GridLines.Visible = false;
                diagram.AxisY.GridLines.Visible = true;
                diagram.AxisY.GridLines.Color = Color.FromArgb(245, 245, 245);

                // X축 설정
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;

                // Y축 설정
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V}%";
                diagram.AxisY.WholeRange.Auto = true;
            }
        }

        private void Search_Chart_NgRate()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 불량률 (PPM) 추이
            query = $@"
            DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}';

            WITH Calendar AS (
                SELECT CONVERT(CHAR(8), DATEADD(DAY, -6, GETDATE()), 112) AS WorkDate
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -5, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -4, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -3, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -2, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), DATEADD(DAY, -1, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(8), GETDATE(), 112)
            ),
            Prod AS (
                SELECT
                    WorkDate
                    , ISNULL(SUM(WorkQty), 0) AS TotalWorkQty
                    , ISNULL(SUM(NgQty), 0)   AS TotalNgQty
                FROM PM_OrderResult
                WHERE CorpId = @CorpId
                  AND WorkDate >= CONVERT(CHAR(8), DATEADD(DAY, -6, GETDATE()), 112)
                  AND WorkDate <= CONVERT(CHAR(8), GETDATE(), 112)
                GROUP BY WorkDate
            )
            SELECT
                C.WorkDate
                , SUBSTRING(C.WorkDate, 5, 2) + '-' + SUBSTRING(C.WorkDate, 7, 2) AS DisplayDate
                , ISNULL(P.TotalWorkQty, 0) AS TotalWorkQty
                , ISNULL(P.TotalNgQty, 0)   AS TotalNgQty

                -- 불량률 PPM (NgQty / WorkQty) * 1,000,000)
                , CASE
                    WHEN ISNULL(P.TotalWorkQty, 0) = 0 THEN 0
                    ELSE ROUND((CAST(P.TotalNgQty AS FLOAT) / P.TotalWorkQty) * 1000000, 0)
                END AS DefectPPM

            FROM Calendar C
            LEFT JOIN Prod P ON C.WorkDate = P.WorkDate
            ORDER BY C.WorkDate
            ";

            DataTable dt = db.GetDataTable(query);

            chartNgRate.DataSource = dt;

            ChartControl chart = chartNgRate;
            chart.Series.Clear();
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BackColor = Color.White;

            // 범례 설정
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // 막대 그래프
            Series sDefectPPM = new Series("불량률(PPM)", ViewType.Bar);
            sDefectPPM.ArgumentDataMember = "DisplayDate";
            sDefectPPM.ValueDataMembers.AddRange(new string[] { "DefectPPM" });
            sDefectPPM.DataSource = dt;

            // 수치 레이블 표시 설정
            sDefectPPM.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            sDefectPPM.Label.TextPattern = "{V:#,#}";
            sDefectPPM.CrosshairLabelPattern = "{A} : {V:#,0} PPM";

            BarSeriesLabel label = sDefectPPM.Label as BarSeriesLabel;
            if (label != null)
            {
                label.Position = BarSeriesLabelPosition.Top;
                label.BackColor = Color.Transparent;
                label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            // 막대 색상 및 너비 설정
            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)sDefectPPM.View;
            barView.BarWidth = 0.45;

            chart.Series.Add(sDefectPPM);

            // 축(Diagram) 설정
            XYDiagram diagram = chart.Diagram as XYDiagram;
            if (diagram != null)
            {
                diagram.AxisX.GridLines.Visible = false;
                diagram.AxisY.GridLines.Visible = true;
                diagram.AxisY.GridLines.Color = Color.FromArgb(240, 240, 240);

                // X축 설정
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;

                // Y축 설정
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V:#,#}";

                // 여백 자동 확보 (상단 수치가 자르지 않도록 Margin 확보)
                diagram.AxisY.WholeRange.Auto = true;
            }
        }

        private void Search_Chart_NgByExam()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 검사 단계별 불량 현황
            query = $@"
            DECLARE @CorpId   NVARCHAR(20) = '{WrGlobal.CorpID}';
            DECLARE @Today        CHAR(8)     = CONVERT(CHAR(8), GETDATE(), 112);
            DECLARE @StartOfMonth CHAR(8)     = CONVERT(CHAR(8), DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0), 112);

            WITH ExamResult AS (
                SELECT
                    A.ExamNo
                    , A.ExamDivCd
                    , MAX(A.PassDivCd) AS PassDivCd
                FROM QM_ExamResult A
                WHERE A.CorpId = @CorpId
                  AND A.ExamDate BETWEEN @StartOfMonth AND @Today
                GROUP BY A.ExamNo, A.ExamDivCd
            )
            SELECT
                B.TITLE
                , COUNT(DISTINCT CASE WHEN E.PassDivCd = '10' THEN E.ExamNo END) AS PassQty
                , COUNT(DISTINCT CASE WHEN E.PassDivCd <> '10' THEN E.ExamNo END) AS DefectQty
            FROM ExamResult E
            LEFT JOIN CM_CateItem B ON B.CorpId = @CorpId AND B.CLASS = 'Q005' AND E.ExamDivCd = B.ClassItemCode
            GROUP BY B.TITLE, E.ExamDivCd, B.ClassItemCode
            ORDER BY ISNULL(B.ClassItemCode, E.ExamDivCd)
            ";

            DataTable dt = db.GetDataTable(query);

            chartNgByExam.DataSource = dt;

            ChartControl chart = chartNgByExam;
            chart.Series.Clear();
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BackColor = Color.White;

            if (dt == null || dt.Rows.Count == 0)
            {
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                return;
            }

            // 범례 설정
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            chart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.Legend.Font = new Font("맑은 고딕", 9F);

            Series sPass = new Series("합격", ViewType.StackedBar);
            sPass.ArgumentDataMember = "Title";
            sPass.ValueDataMembers.AddRange(new string[] { "PassQty" });
            sPass.DataSource = dt;

            sPass.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            sPass.Label.TextPattern = "{V:#,#}";

            StackedBarSeriesLabel passLabel = sPass.Label as StackedBarSeriesLabel;
            if (passLabel != null)
            {
                passLabel.BackColor = Color.Transparent;
                passLabel.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
                passLabel.Font = new Font("맑은 고딕", 8F, FontStyle.Bold);
                passLabel.TextColor = Color.White;
            }

            StackedBarSeriesView passView = (StackedBarSeriesView)sPass.View;
            passView.Color = Color.FromArgb(24, 100, 230);
            passView.BarWidth = 0.4;

            Series sDefect = new Series("불합격", ViewType.StackedBar);
            sDefect.ArgumentDataMember = "Title";
            sDefect.ValueDataMembers.AddRange(new string[] { "DefectQty" });
            sDefect.DataSource = dt;

            sDefect.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            sDefect.Label.TextPattern = "{V:#,#}";

            StackedBarSeriesLabel defectLabel = sDefect.Label as StackedBarSeriesLabel;
            if (defectLabel != null)
            {
                defectLabel.BackColor = Color.Transparent;
                defectLabel.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
                defectLabel.Font = new Font("맑은 고딕", 8F, FontStyle.Bold);
                defectLabel.TextColor = Color.White;
            }

            StackedBarSeriesView defectView = (StackedBarSeriesView)sDefect.View;
            defectView.Color = Color.FromArgb(235, 60, 50);
            defectView.BarWidth = 0.4;

            // 시리즈 추가
            chart.Series.AddRange(new Series[] { sPass, sDefect });

            XYDiagram diagram = chart.Diagram as XYDiagram;
            if (diagram != null)
            {
                // 세로 막대를 가로 막대로 회전
                diagram.Rotated = true;

                // Y축 격자선 설정
                diagram.AxisX.GridLines.Visible = false;
                diagram.AxisY.GridLines.Visible = true;
                diagram.AxisY.GridLines.Color = Color.FromArgb(245, 245, 245);

                // X축 설정
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Reverse = true;

                // Y축 설정
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V:#,#}";
                diagram.AxisY.Label.Font = new Font("맑은 고딕", 8.5F);

                diagram.AxisY.NumericScaleOptions.AutoGrid = false;
                diagram.AxisY.NumericScaleOptions.GridSpacing = 1;
                diagram.AxisY.WholeRange.SideMarginsValue = 0;

                // 축 범위 고정 (0부터 5까지)
                diagram.AxisY.WholeRange.Auto = false;
                diagram.AxisY.WholeRange.SetMinMaxValues(0, 5);
            }

            if (passView != null)
            {
                passView.BarWidth = 0.25;
            }

            if (defectView != null)
            {
                defectView.BarWidth = 0.25;
            }
        }

        private void Search_Chart_ExamResult()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 검사 현황
            query = $@"
            DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';

            WITH MonthList AS (
                SELECT CONVERT(CHAR(6), DATEADD(MONTH, -5, GETDATE()), 112) AS YyyyMm
                UNION ALL SELECT CONVERT(CHAR(6), DATEADD(MONTH, -4, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(6), DATEADD(MONTH, -3, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(6), DATEADD(MONTH, -2, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(6), DATEADD(MONTH, -1, GETDATE()), 112)
                UNION ALL SELECT CONVERT(CHAR(6), GETDATE(), 112)
            ),

            ActiveExamDivList AS (
                SELECT DISTINCT
                    A.ExamDivCd,
                    ISNULL(B.TITLE, '기타검사') AS ExamDivNm
                FROM QM_ExamResult A
                LEFT JOIN CM_CateItem B ON B.CorpId = @CorpId AND B.CLASS = 'Q005' AND A.ExamDivCd = B.ClassItemCode
                WHERE A.CorpId = @CorpId
                  AND A.ExamDate >= CONVERT(CHAR(8), DATEADD(MONTH, -5, DATEADD(DAY, 1 - DAY(GETDATE()), GETDATE())), 112)
            ),

            ExamData AS (
                SELECT 
                    LEFT(A.ExamDate, 6) AS YyyyMm,
                    A.ExamDivCd,
                    COUNT(DISTINCT A.ExamNo) AS ExamCount
                FROM QM_ExamResult A
                WHERE A.CorpId = @CorpId
                  AND A.ExamDate >= CONVERT(CHAR(8), DATEADD(MONTH, -5, DATEADD(DAY, 1 - DAY(GETDATE()), GETDATE())), 112)
                GROUP BY LEFT(A.ExamDate, 6), A.ExamDivCd
            )

            SELECT 
                SUBSTRING(M.YyyyMm, 1, 4) + '-' + SUBSTRING(M.YyyyMm, 5, 2) AS DisplayMonth, -- X축
                D.ExamDivNm,                                                                 -- 범례 (실적 있는 것만 나옴)
                ISNULL(E.ExamCount, 0) AS ExamCount                                          -- Y축
            FROM MonthList M
            CROSS JOIN ActiveExamDivList D                                                   -- 실제 존재하는 검사종류만 매핑
            LEFT JOIN ExamData E ON M.YyyyMm = E.YyyyMm AND D.ExamDivCd = E.ExamDivCd
            ORDER BY M.YyyyMm ASC, D.ExamDivNm
            ";

            DataTable dt = db.GetDataTable(query);

            chartExamResult.DataSource = dt;

            ChartControl chart = chartExamResult;
            chart.Series.Clear();
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BackColor = Color.White;

            if (dt == null || dt.Rows.Count == 0)
            {
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                return;
            }

            // 범례 설정
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            chart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.Legend.Font = new Font("맑은 고딕", 9F);

            chart.SeriesDataMember = "ExamDivNm";
            chart.SeriesTemplate.ArgumentDataMember = "DisplayMonth";
            chart.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "ExamCount" });
            chart.SeriesTemplate.ChangeView(ViewType.Line);

            // 라인 및 수치 설정
            chart.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            chart.SeriesTemplate.Label.TextPattern = "{V:#,#}";
            chart.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:#,##0} 건";

            // 포인트 상단 수치 레이블 디자인
            PointSeriesLabel label = chart.SeriesTemplate.Label as PointSeriesLabel;
            if (label != null)
            {
                label.BackColor = Color.Transparent;
                label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }

            // 라인 두께 및 원형 마커 설정
            LineSeriesView lineView = (LineSeriesView)chart.SeriesTemplate.View;
            lineView.LineStyle.Thickness = 2;
            lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
            lineView.LineMarkerOptions.Size = 6;

            // 축 설정
            XYDiagram diagram = chart.Diagram as XYDiagram;
            if (diagram != null)
            {
                // X축 (월)
                diagram.AxisX.GridLines.Visible = false;
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                //diagram.AxisX.Label.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;

                // Y축 (건수)
                diagram.AxisY.GridLines.Visible = true;
                diagram.AxisY.GridLines.Color = Color.FromArgb(245, 245, 245);
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V:#,#}";
                diagram.AxisY.Label.Font = new Font("맑은 고딕", 8.5F);

                // Y축 여백 자동 지정
                diagram.AxisY.WholeRange.Auto = true;
            }
        }

        private void Search_Chart_NgRateByProcess()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 공정별 불량률
            query = $@"
            DECLARE @CorpId   NVARCHAR(20) = '{WrGlobal.CorpID}';
            DECLARE @FirstDay    DATETIME = DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0);      -- 당월 1일 00:00:00
            DECLARE @NextMonthDay DATETIME = DATEADD(month, DATEDIFF(month, 0, GETDATE()) + 1, 0); -- 내달 1일 00:00:00

            WITH ProcessBase AS (
                    SELECT CorpId, ProcessCode, ProcessName
                    FROM CM_Process
                    WHERE CorpId = @CorpId
                ),
                ResultSummary AS (
                    SELECT
                        R.CorpId
                        , OP.ProcessCode
                        , ISNULL(SUM(R.WorkQty), 0) AS TotalWorkQty
                        , ISNULL(SUM(R.NgQty), 0)   AS TotalNgQty
                    FROM PM_OrderResult R
                    INNER JOIN PM_OrderProcess OP ON R.CorpId = OP.CorpId AND R.OrderNo = OP.OrderNo AND R.OrderSeq = OP.OrderSeq
                    WHERE R.CorpId = @CorpId
                      AND OP.OrderDate >= @FirstDay
                      AND OP.OrderDate <  @NextMonthDay
                    GROUP BY R.CorpId, OP.ProcessCode
                )
                SELECT
                    P.ProcessCode
                    , P.ProcessName
                    , ISNULL(R.TotalWorkQty, 0) AS TotalWorkQty
                    , ISNULL(R.TotalNgQty, 0)   AS TotalNgQty
                    , CASE 
                        WHEN ISNULL(R.TotalWorkQty, 0) = 0 THEN 0
                        ELSE ROUND((CAST(R.TotalNgQty AS FLOAT) / R.TotalWorkQty) * 1000000, 0)
                    END AS PpmRate
                FROM ProcessBase P
                INNER JOIN ResultSummary R ON P.CorpId = R.CorpId AND P.ProcessCode = R.ProcessCode
                WHERE R.TotalWorkQty > 0
                ORDER BY P.ProcessCode
            ";

            DataTable dt = db.GetDataTable(query);

            ChartControl chart = chartNgRateByProcess;
            chart.Series.Clear();
            chart.Series.BeginUpdate();

            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False; // 범례 숨김
            chart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;

            Series barSeries = new Series("공정별 불량률", ViewType.Bar);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string processName = Convert.ToString(dr["ProcessName"]);
                    double ppm = Convert.ToDouble(dr["PpmRate"]);

                    SeriesPoint point = new SeriesPoint(processName, ppm);
                    barSeries.Points.Add(point);
                }
            }

            barSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True; // 막대 상단 숫자 라벨 표시
            barSeries.Label.TextPattern = "{V:#,##0}";

            // 바 스타일 설정
            SideBySideBarSeriesView view = barSeries.View as SideBySideBarSeriesView;
            if (view != null)
            {
                view.BarWidth = 0.35;

                // 라벨 위치 설정
                ((BarSeriesLabel)barSeries.Label).Position = BarSeriesLabelPosition.Top;
            }

            chart.Series.Add(barSeries);

            // 축 디스플레이 설정
            XYDiagram diagram = chart.Diagram as XYDiagram;
            if (diagram != null)
            {
                // X축 (공정명)
                diagram.AxisX.Tickmarks.Visible = false;
                diagram.AxisX.Label.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);

                // Y축 (PPM 수치)
                diagram.AxisY.Label.TextPattern = "{V:#,##0}";
                diagram.AxisY.GridLines.Color = Color.FromArgb(240, 242, 245);
            }

            chart.Series.EndUpdate();
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        private void Search_Chart_DefectStatus()
        {
            string yyyyMM = DateTime.Now.ToString("yyyyMM");

            // 부적합 현황
            query = $@"
            DECLARE @CorpId  NVARCHAR(20) = '{WrGlobal.CorpID}';
            DECLARE @YyyyMm  CHAR(6)      = '{yyyyMM}';

            WITH DefectSummary AS (
                SELECT
                    ISNULL(DC.Title, '기타') AS Title
                    , COUNT(WD.SerialNo)       AS DefectCount
                    , ISNULL(SUM(WD.Qty), 0)   AS TotalQty
                FROM QM_WorkDefect WD
                LEFT JOIN CM_DefectCode DC ON WD.CorpId = DC.CorpId AND WD.DefectCode = DC.FaultCode
                LEFT JOIN PM_OrderResult ORS ON WD.CorpId = ORS.CorpId AND WD.SerialNo = ORS.SerialNo
                LEFT JOIN PM_OrderProcess OP ON ORS.CorpId = OP.CorpId AND ORS.OrderNo = OP.OrderNo AND ORS.OrderSeq = OP.OrderSeq
                WHERE WD.CorpId = @CorpId
                  AND LEFT(CONVERT(VARCHAR(8), WD.WorkDate, 112), 6) = @YyyyMm
                GROUP BY ISNULL(DC.Title, '기타')
            ),
            CalculatedDefects AS (
                SELECT
                    ROW_NUMBER() OVER (ORDER BY TotalQty DESC, DefectCount DESC) AS RankNo
                    , Title
                    , DefectCount
                    , TotalQty
                    , SUM(TotalQty)    OVER() AS GrandTotalQty
                    , SUM(DefectCount) OVER() AS GrandTotalCount
                FROM DefectSummary
            )
            SELECT
                RankNo
                , Title
                , DefectCount
                , TotalQty

                -- 수량에 대한 비율 (%)
                , CASE
                    WHEN GrandTotalQty = 0 THEN 0.0
                    ELSE ROUND((CAST(TotalQty AS FLOAT) / GrandTotalQty) * 100, 1)
                END AS RateQty

                -- 건수에 대한 비율 (%)
                , CASE
                    WHEN GrandTotalCount = 0 THEN 0.0
                    ELSE ROUND((CAST(DefectCount AS FLOAT) / GrandTotalCount) * 100, 1)
                END AS RateCount

            FROM CalculatedDefects
            ORDER BY RankNo
            ";

            DataTable dt = db.GetDataTable(query);

            ChartControl chart = chartDefectStatus;
            chart.Series.Clear();
            chart.Series.BeginUpdate();

            // 도넛 시리즈 생성
            Series doughnutSeries = new Series("부적합 수량 현황", ViewType.Doughnut);

            doughnutSeries.DataSource = dt;
            doughnutSeries.ArgumentDataMember = "Title";
            doughnutSeries.ValueDataMembers.AddRange(new string[] { "TotalQty" });

            // 도넛 뷰 및 라벨 스타일 설정
            DoughnutSeriesView view = (DoughnutSeriesView)doughnutSeries.View;
            view.HoleRadiusPercent = 60;
            view.TotalLabel.Visible = true;
            view.TotalLabel.TextPattern = "총 부적합 수량\n{TV:N0} EA";
            view.TotalLabel.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);

            // 라벨 표시 형식 설정
            doughnutSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            doughnutSeries.Label.TextPattern = "{A}\n{V:N0} EA";

            // 범례 표시 형식 설정
            doughnutSeries.LegendTextPattern = "{A}  ({VP:P1})";

            // 라벨 위치 및 겹침 방지 처리
            PieSeriesLabel label = (PieSeriesLabel)doughnutSeries.Label;
            label.Position = PieSeriesLabelPosition.TwoColumns;
            label.ResolveOverlappingMode = ResolveOverlappingMode.Default; // 라벨 겹침 자동 해결

            chart.Series.Add(doughnutSeries);

            // 범례 스타일 및 위치 설정
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
            chart.Legend.Direction = LegendDirection.TopToBottom;

            chart.Series.EndUpdate();
        }

        private void Search_Chart_DefectType()
        {
            this.Validate();

            string yyyyMM = DateTime.Now.ToString("yyyyMM");

            //유형별 불량현황
            query = $@"
            WITH RawData AS (
                SELECT
                    LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6) AS WorkDate
                    , qc.DefectCode
                    , ISNULL(cd.Title, '기타불량') AS Title
                    , COUNT(*) AS Bad_Count
                FROM QM_WorkDefect qc
                LEFT JOIN CM_DefectCode cd ON qc.CorpId = cd.CorpId AND qc.DefectCode = cd.FaultCode
                WHERE qc.CorpId = '{WrGlobal.CorpID}'
                  AND LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6) = '{yyyyMM}'
                GROUP BY qc.DefectCode, LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6), cd.Title
            )
            SELECT
                WorkDate
                , DefectCode
                , Title
                , Bad_Count
            FROM RawData
            ORDER BY Bad_Count DESC
            ";

            DataTable dt = db.GetDataTable(query);

            ChartControl chart = chartNgType;
            chart.Series.Clear();
            chart.Series.BeginUpdate();

            // 도넛 시리즈 생성
            Series doughnutSeries = new Series("불량비율", ViewType.Doughnut);

            if (dt.Rows.Count == 0)
            {
                doughnutSeries.Points.Add(new SeriesPoint("데이터 없음", 0));
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.Series.Add(doughnutSeries);
            }
            else
            {
                // 전체 수량 합계
                double totalQty = Convert.ToDouble(dt.Compute("SUM(Bad_Count)", ""));

                foreach (DataRow dr in dt.Rows)
                {
                    string title = Convert.ToString(dr["Title"]) == "" ? "기타" : Convert.ToString(dr["Title"]);
                    double qty = Convert.ToDouble(dr["Bad_Count"]);

                    doughnutSeries.Points.Add(new SeriesPoint(title, qty));
                }

                // 그래프 자체 라벨 숨기기
                doughnutSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chart.Legend.MarkerMode = LegendMarkerMode.Marker;
                doughnutSeries.LegendTextPattern = "{A}   {V:N0}건 ({VP:P1})";
                //doughnutSeries.LegendTextPattern = "{A}   {VP:P1}";
                chart.Legend.Font = new Font("맑은 고딕", 9F, FontStyle.Regular);

                chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                chart.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
                chart.Legend.Direction = LegendDirection.TopToBottom;
                chart.Series.Add(doughnutSeries);

                // 가운데 총 수량 표시
                DoughnutSeriesView view = doughnutSeries.View as DoughnutSeriesView;
                if (view != null)
                {
                    view.Rotation = 270;
                    view.HoleRadiusPercent = 60;

                    view.TotalLabel.Visible = true;
                    view.TotalLabel.TextPattern = "총 불량 건수\n{TV:#,0} 건";
                    view.TotalLabel.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                }
            }

            chart.Series.EndUpdate();
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        #endregion

        #region 조회 쿼리 (그리드)

        private void SearchGrid_DefectTop5()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 주요 부적합 TOP5
            query = $@"
            DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
            DECLARE @Today        CHAR(8)     = CONVERT(CHAR(8), GETDATE(), 112);
            DECLARE @StartOfMonth CHAR(8)     = CONVERT(CHAR(8), DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0), 112);

            WITH DefectSummary AS (
                SELECT
                    ISNULL(DC.Title, '기타') AS Title
                    , COUNT(WD.SerialNo) AS DefectCount
                FROM QM_WorkDefect WD
                LEFT JOIN CM_DefectCode DC ON WD.CorpId = DC.CorpId AND WD.DefectCode = DC.FaultCode
                LEFT JOIN PM_OrderResult ORS ON WD.CorpId = ORS.CorpId AND WD.SerialNo = ORS.SerialNo
                LEFT JOIN PM_OrderProcess OP ON ORS.CorpId = OP.CorpId AND ORS.OrderNo = OP.OrderNo AND ORS.OrderSeq = OP.OrderSeq
                WHERE WD.CorpId = @CorpId
                  AND WD.WorkDate BETWEEN @StartOfMonth AND @Today
                GROUP BY ISNULL(DC.Title, '기타')
            ),
            RankedDefects AS (
                SELECT
                    ROW_NUMBER() OVER (ORDER BY DefectCount DESC) AS RankNo
                    , Title
                    , DefectCount
                    , SUM(DefectCount) OVER() AS TotalDefectCount
                FROM DefectSummary
            )
            SELECT
                RankNo
                , Title
                , DefectCount
                , CASE
                    WHEN TotalDefectCount = 0 THEN 0.0
                    ELSE ROUND((CAST(DefectCount AS FLOAT) / TotalDefectCount) * 100, 1)
                END AS DefectRate

            FROM RankedDefects
            WHERE RankNo <= 5
            ORDER BY RankNo
            ";

            Gc_DefectTop5.DataSource = db.GetDataTable(query);
            Gv_DefectTop5.BestFitColumns();
        }
        private void SearchGrid_WorkDefect()
        {
            string yyyyMM = DateTime.Now.ToString("yyyyMM");

            // 최근 부적합 발생 현황
            query = $@"
            SELECT TOP 30
                WD.WorkDate
            	, WD.ItemCode
                , WD.FromProcessCode
                , P.ProcessName
                , DC.Title
            	, WD.Qty
            	, OP.Status
            FROM QM_WorkDefect WD
            LEFT JOIN CM_DefectCode DC ON WD.CorpId = DC.CorpId AND WD.DefectCode = DC.FaultCode
            LEFT JOIN CM_Process P ON WD.CorpId = P.CorpId AND WD.FromProcessCode = P.ProcessCode
            LEFT JOIN PM_OrderResult ORS ON WD.CorpId = ORS.CorpId AND WD.SerialNo = ORS.SerialNo
            LEFT JOIN PM_OrderProcess OP ON ORS.CorpId = OP.CorpId AND ORS.OrderNo = OP.OrderNo AND ORS.OrderSeq = OP.OrderSeq
            WHERE WD.CorpId = '{WrGlobal.CorpID}'
             AND LEFT(CONVERT(VARCHAR, WD.WorkDate, 112), 6) = '{yyyyMM}'
            ORDER BY WD.WorkDate DESC
            ";

            Gc_WorkDefect.DataSource = db.GetDataTable(query);
            Gv_WorkDefect.BestFitColumns();
        }

        #endregion

        #region Event
        private void timer1_Tick(object sender, EventArgs e)
        {
            LblCurTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void Tm_Search_Tick(object sender, EventArgs e)
        {
            Search();
        }

		#endregion

		private void toggleSwitch1_Toggled(object sender, EventArgs e)
		{
            string value = toggleSwitch1.EditValue.ConvertString_Null_To_Empty();

			//On - 자동 조회
			if (toggleSwitch1.IsOn)
			{
                Search();
                Tm_Search.Enabled = true;
			}
			else
			{
                Tm_Search.Enabled = false;
			}
		}

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}