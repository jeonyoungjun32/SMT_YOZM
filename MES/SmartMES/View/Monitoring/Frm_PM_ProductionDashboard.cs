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
    public partial class Frm_PM_ProductionDashboard : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        public Frm_PM_ProductionDashboard()
        {
            InitializeComponent();

            // 상단 조회조건 색상 설정
            smtCommonBar1.smtBackColor = Color.Transparent;
            btn_Colose.ForeColor = Color.Black;
        }

        private void Frm_PM_ProductionDashboard_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            SetGrid_Work(Gv_Work);
            SetGrid_Machine(GvMachine);
            SetGrid_OrderProcess(Gv_OrderProcess);

            Search();

            Tm_Search.Interval = 360000;
            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)
        private void SetGrid_Work(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "계획수량(EA)", "OrderQty");
            grid.Create_GvColumn(gv, "실적수량(EA)", "WorkQty");
            grid.Create_GvColumn(gv, "진행률(%)", "ProgressRate");
            grid.Create_GvColumn(gv, "상태", "ProcessStatus");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ProgressRate"], clsGrid.TypeList.TFloatN1);

            gv.Columns["ProcessName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
            gv.CustomDrawCell += gridView1_CustomDrawCell;
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
            if (e.Column.FieldName == "MachineStatus" || e.Column.FieldName == "ProcessStatus" || e.Column.FieldName == "WorkStatus") //
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

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "ProgressRate" || e.Column.FieldName == "OperationRate")
            {
                if (e.CellValue == null || e.CellValue == DBNull.Value) return;

                double rate = Convert.ToDouble(e.CellValue);

                // 1. 셀 배경만 직접 지우기 (기존 DefaultDraw로 인한 텍스트 중복 방지)
                e.Cache.FillRectangle(e.Appearance.GetBackBrush(e.Cache), e.Bounds);

                // 2. 프로그레스 바 영역 계산
                Rectangle rect = e.Bounds;
                rect.Inflate(-4, -4);

                int barHeight = 8;
                int barY = rect.Y + (rect.Height - barHeight) / 2;

                int textWidth = 55;
                int barMaxWidth = rect.Width - textWidth;

                if (barMaxWidth > 0)
                {
                    Rectangle bgRect = new Rectangle(rect.X, barY, barMaxWidth, barHeight);

                    double percent = Math.Min(Math.Max(rate, 0), 100) / 100.0;
                    int fillWidth = (int)(barMaxWidth * percent);
                    Rectangle fillRect = new Rectangle(rect.X, barY, fillWidth, barHeight);

                    // 바 색상 결정 (달성률: 파란색, 가동률: 민트색)
                    Color barColor = (e.Column.FieldName == "AchievementRate")
                        ? Color.FromArgb(51, 102, 255)
                        : Color.FromArgb(82, 196, 143);

                    using (Brush bgBrush = new SolidBrush(Color.FromArgb(235, 240, 245)))
                    using (Brush fillBrush = new SolidBrush(barColor))
                    {
                        e.Graphics.FillRectangle(bgBrush, bgRect);
                        if (fillWidth > 0)
                        {
                            e.Graphics.FillRectangle(fillBrush, fillRect);
                        }
                    }

                    // 텍스트 출력
                    Rectangle textRect = new Rectangle(rect.X + barMaxWidth, rect.Y, textWidth, rect.Height);
                    string text = string.Format("{0:F1}%", rate);

                    using (Brush textBrush = new SolidBrush(Color.FromArgb(50, 60, 75)))
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Center;
                        e.Cache.DrawString(text, e.Appearance.Font, textBrush, textRect, sf);
                    }
                }

                e.Handled = true;
            }
        }

        private void SetGrid_Machine(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "상태", "MachineStatus");
            grid.Create_GvColumn(gv, "가동률(%)", "OperationRate");
            grid.Create_GvColumn(gv, "가동시간", "TodayRunHours");
            grid.Create_GvColumn(gv, "정지시간", "TodayStopHours");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["OperationRate"], clsGrid.TypeList.TFloatN1);
            grid.SetCol_Type(gv.Columns["TodayRunHours"], clsGrid.TypeList.TFloatN1);
            grid.SetCol_Type(gv.Columns["TodayStopHours"], clsGrid.TypeList.TFloatN1);

            gv.Columns["MachineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
            gv.CustomDrawCell += gridView1_CustomDrawCell;
        }

        private void SetGrid_OrderProcess(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "제품명", "ItemName");
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "지시일", "OrderDate");
            grid.Create_GvColumn(gv, "진행상태", "WorkStatus");

            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);

            gv.Columns["WorkStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
        }

        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            SearchCardData();

            Search_Chart_Production_Time();
            Search_Chart_Production_Day();
            Search_Chart_Production_Prod();
            Search_Chart_DefectRate();
            Search_Chart_Production_Goal();

            SearchGrid_Work();
            SearchGrid_OrderProcess();
            SearchGrid_Machine();
            SearchGrid_Notice();
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            query = $@"
            DECLARE @Today        CHAR(8)  = CONVERT(CHAR(8), GETDATE(), 112); -- 오늘 날짜 (YYYYMMDD 기준, 형식에 따라 'YYYY-MM-DD' 변경)
            DECLARE @StartOfMonth CHAR(8)  = CONVERT(CHAR(8), DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0), 112); -- 당월 1일
            DECLARE @CorpId       VARCHAR(10) = '{WrGlobal.CorpID}';

            WITH TodayProduction AS (
                -- 금일 생산 실적
                SELECT ISNULL(SUM(GoodQty), 0) AS TodayGoodQty
                      , ISNULL(SUM(NgQty), 0)   AS TodayNgQty
                      , ISNULL(SUM(WorkQty), 0) AS TodayTotalQty
                FROM PM_OrderResult
                WHERE WorkDate = @Today
                  AND (@CorpId IS NULL OR CorpId = @CorpId)
            ),
            TodayPlan AS (
                -- 금일 생산 계획 및 지연 건수
                SELECT ISNULL(SUM(OrderQty), 0) AS TodayPlanQty
                      , ISNULL(SUM(CASE WHEN DeliveryDate < GETDATE() THEN 1 ELSE 0 END), 0) AS DelayCount
                FROM PM_ProductPlan
                WHERE WorkDate = @Today
                  AND CorpId = @CorpId
            ),
            EquipmentOperation AS (
                -- 설비가동률 (작업시간 / (작업시간 + 비가동시간))
                SELECT ISNULL(SUM(DATEDIFF(MINUTE, R.StartDate, R.EndDate)), 0) AS TotalWorkTimeMinute
                    , ISNULL(SUM(O.StopTime), 0) AS TotalStopTimeMinute
                FROM PM_OrderResult R
                LEFT JOIN EQ_Operation O ON R.CorpId      = O.CorpId AND R.WorkDate    = O.WorkDay AND R.MachineCode = O.MachineCode
                WHERE R.WorkDate = @Today
                  AND R.CorpId = @CorpId
            ),
            CycleTimeData AS (
                -- 평균 사이클 타임 (작업 소요 초 / 생산 수량)
                SELECT
                    CASE
                        WHEN SUM(WorkQty) = 0 THEN 0.0
                        ELSE ROUND(CAST(SUM(DATEDIFF(SECOND, StartDate, EndDate)) AS FLOAT) / SUM(WorkQty), 1)
                    END AS AvgCycleTime
                FROM PM_OrderResult
                WHERE WorkDate = @Today
                  AND CorpId = @CorpId
                  AND WorkQty > 0
                  AND StartDate IS NOT NULL 
                  AND EndDate IS NOT NULL
            )
            SELECT
                -- 계획 대비 생산률 (%)
                CASE
                    WHEN PLN.TodayPlanQty = 0 THEN 0.0
                    ELSE ROUND((CAST(P.TodayTotalQty AS FLOAT) / PLN.TodayPlanQty) * 100, 1)
                END AS PlanAchievementRate,

                -- 양품률 (%)
                CASE
                    WHEN P.TodayTotalQty = 0 THEN 0.0
                    ELSE ROUND((CAST(P.TodayGoodQty AS FLOAT) / P.TodayTotalQty) * 100, 2)
                END AS GoodRate,

                -- 평균 사이클 타임 (초)
                ISNULL(CT.AvgCycleTime, 0.0) AS AvgCycleTime,

                -- 설비 가동률 (%)
                CASE
                    WHEN (EQ.TotalWorkTimeMinute + EQ.TotalStopTimeMinute) = 0 THEN 0.0
                    ELSE ROUND((CAST(EQ.TotalWorkTimeMinute AS FLOAT) / (EQ.TotalWorkTimeMinute + EQ.TotalStopTimeMinute)) * 100, 1)
                END AS EquipmentOperationRate,

                -- 생산 지연 건수
                PLN.DelayCount AS ProductionDelayCount
            FROM TodayProduction P
            CROSS JOIN TodayPlan PLN
            CROSS JOIN EquipmentOperation EQ
            CROSS JOIN CycleTimeData CT
            ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                LblWorkQty.Text = dr["PlanAchievementRate"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblAchieveRate.Text = dr["GoodRate"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblNgRate.Text = dr["AvgCycleTime"].ConvertFloat_Null_To_Zero().ToString("N1") + " 초";
                LblOpRate.Text = dr["EquipmentOperationRate"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblSalesAmt.Text = dr["ProductionDelayCount"].ConvertInt_Null_To_Zero().ToString("N0") + " 건";
            }
        }
        #endregion

        #region 차트 로드
        private void Search_Chart_Production_Time()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 시간대별 생산량 추이
            query = $@"
            DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}';
            DECLARE @BaseDate DATETIME   = '{endDt}';

            -- 00시부터 23시까지 24시간 시간 축 마스터 생성
            WITH WorkTimeSlots AS (
                SELECT 0 AS Hour UNION ALL SELECT 1  UNION ALL SELECT 2  UNION ALL SELECT 3
                UNION ALL SELECT 4  UNION ALL SELECT 5  UNION ALL SELECT 6  UNION ALL SELECT 7
                UNION ALL SELECT 8  UNION ALL SELECT 9  UNION ALL SELECT 10 UNION ALL SELECT 11
                UNION ALL SELECT 12 UNION ALL SELECT 13 UNION ALL SELECT 14 UNION ALL SELECT 15
                UNION ALL SELECT 16 UNION ALL SELECT 17 UNION ALL SELECT 18 UNION ALL SELECT 19
                UNION ALL SELECT 20 UNION ALL SELECT 21 UNION ALL SELECT 22 UNION ALL SELECT 23
            ),

            -- 실적수량을 시간대별로 집계
            ResultSummary AS (
                SELECT
                    DATEPART(HOUR, ISNULL(EndDate, WorkDate)) AS Hour
                    , SUM(ISNULL(WorkQty, 0)) AS TotalWorkQty
                FROM PM_OrderResult
                WHERE CorpId = @CorpId
                  AND WorkDate >= CAST(@BaseDate AS DATETIME)
                  AND WorkDate <  DATEADD(DAY, 1, CAST(@BaseDate AS DATETIME))
                GROUP BY DATEPART(HOUR, ISNULL(EndDate, WorkDate))
            ),

            -- 총 계획수량 집계 및 시간대별 배치
            OrderSummary AS (
                SELECT SUM(ISNULL(OrderQty, 0)) / 24.0 AS HourlyPlanQty
                FROM PM_OrderProcess
                WHERE CorpId = @CorpId
                  AND OrderDate = CONVERT(CHAR(8), @BaseDate, 112)
            )

            -- 시간 축 기준 최종 출력 (00시 ~ 23시)
            SELECT
                RIGHT('0' + CAST(ts.Hour AS VARCHAR(2)), 2) + '시' AS WorkHour

                -- 시간대별 계획 수량
                , ROUND(ISNULL(p.HourlyPlanQty, 0), 0) AS PlanQty

                -- 시간대별 실적 수량
                , ISNULL(r.TotalWorkQty, 0) AS WorkQty

            FROM WorkTimeSlots ts
            LEFT JOIN ResultSummary r ON ts.Hour = r.Hour
            CROSS JOIN OrderSummary p
            ORDER BY ts.Hour
            ";

            DataTable dt = db.GetDataTable(query);

            ChartProduction_Time.DataSource = dt;

            ChartControl chart = ChartProduction_Time;
            chart.Series.Clear();
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BackColor = Color.White;

            // 범례 설정
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            chart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // 막대 그래프
            //Series sPlan = new Series("계획수량(EA)", ViewType.Bar);
            //sPlan.ArgumentDataMember = "WorkHour";
            //sPlan.ValueDataMembers.AddRange(new string[] { "PlanQty" });
            //sPlan.DataSource = dt;
            //sPlan.CrosshairLabelPattern = "{A} 계획 : {V:#,#} EA";
            //sPlan.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            //
            //SideBySideBarSeriesView barView = (SideBySideBarSeriesView)sPlan.View;
            //barView.Color = Color.FromArgb(200, 205, 215);
            //barView.BarWidth = 0.45;

            // 라인 그래프
            Series sWork = new Series("실적수량(EA)", ViewType.Line);
            sWork.ArgumentDataMember = "WorkHour";
            sWork.ValueDataMembers.AddRange(new string[] { "WorkQty" });
            sWork.DataSource = dt;
            sWork.CrosshairLabelPattern = "{A} : {V:#,0} EA";
            sWork.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            LineSeriesView lineView = (LineSeriesView)sWork.View;
            lineView.Color = Color.FromArgb(24, 100, 230);
            lineView.LineStyle.Thickness = 2;

            // 원형 마커 표시 설정
            lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
            lineView.LineMarkerOptions.Size = 7;
            lineView.LineMarkerOptions.Color = Color.FromArgb(24, 100, 230);
            lineView.LineMarkerOptions.FillStyle.FillMode = FillMode.Solid;

            //chart.Series.AddRange(new Series[] { sPlan, sWork });
            chart.Series.AddRange(new Series[] { sWork });

            XYDiagram diagram = chart.Diagram as XYDiagram;
            if (diagram != null)
            {
                // X축 설정 - 시간
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;

                // Y축 설정 - 수량
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V:#,#}";
            }
        }

        private void Search_Chart_Production_Day()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 일자별 생산실적 추이
            query = $@"
            DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}';
            DECLARE @BaseDate DATETIME   = '{endDt}';

            WITH DateSlots AS (
                SELECT CAST(@BaseDate AS DATE) AS WorkDate
                UNION ALL SELECT DATEADD(DAY, -1, CAST(@BaseDate AS DATE))
                UNION ALL SELECT DATEADD(DAY, -2, CAST(@BaseDate AS DATE))
                UNION ALL SELECT DATEADD(DAY, -3, CAST(@BaseDate AS DATE))
                UNION ALL SELECT DATEADD(DAY, -4, CAST(@BaseDate AS DATE))
                UNION ALL SELECT DATEADD(DAY, -5, CAST(@BaseDate AS DATE))
                UNION ALL SELECT DATEADD(DAY, -6, CAST(@BaseDate AS DATE))
            ),

            -- 일자별 지시 수량 집계
            OrderSummary AS (
                SELECT
                    CAST(OrderDate AS DATE) AS OrderDate
                    , SUM(ISNULL(OrderQty, 0)) AS TotalOrderQty
                FROM PM_OrderProcess
                WHERE CorpId = @CorpId
                  AND OrderDate >= CONVERT(CHAR(8), DATEADD(DAY, -6, @BaseDate), 112)
                  AND OrderDate <= CONVERT(CHAR(8), @BaseDate, 112)
                GROUP BY CAST(OrderDate AS DATE)
            ),

            -- 일자별 실적 수량 집계
            ResultSummary AS (
                SELECT
                    CAST(WorkDate AS DATE) AS TargetWorkDate
                    , SUM(ISNULL(WorkQty, 0)) AS TotalWorkQty
                FROM PM_OrderResult
                WHERE CorpId = @CorpId
                  -- WorkDate 기준으로 7일간 집계 (EndDate가 NULL인 건도 수량 포함)
                  AND WorkDate >= DATEADD(DAY, -6, CAST(@BaseDate AS DATE))
                  AND WorkDate <  DATEADD(DAY, 1, CAST(@BaseDate AS DATE))
                GROUP BY CAST(WorkDate AS DATE)
            )

            SELECT
                CONVERT(VARCHAR(5), ds.WorkDate, 110) AS WorkDate

                -- 실적 수량
                , ISNULL(r.TotalWorkQty, 0) AS WorkQty

                -- 지시 수량
                , ISNULL(o.TotalOrderQty, 0) AS OrderQty

                -- 달성률 (%)
                , CASE
                    WHEN ISNULL(o.TotalOrderQty, 0) = 0 THEN 0.0
                    WHEN (CAST(ISNULL(r.TotalWorkQty, 0) AS FLOAT) / o.TotalOrderQty) * 100 > 100.0 THEN 100.0
                    ELSE ROUND((CAST(ISNULL(r.TotalWorkQty, 0) AS FLOAT) / o.TotalOrderQty) * 100, 1)
                  END AS AchievementRate

            FROM DateSlots ds
            LEFT JOIN ResultSummary r ON ds.WorkDate = r.TargetWorkDate
            LEFT JOIN OrderSummary o  ON ds.WorkDate = o.OrderDate
            ORDER BY ds.WorkDate
            ";

            DataTable dt = db.GetDataTable(query);

            ChartControl chart = ChartProduction_Day;
            chart.Series.Clear();
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BackColor = Color.White;

            // 범례 설정 (상단 우측 배치)
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            chart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series s1 = new Series("실적수량(EA)", ViewType.Bar);
            s1.ArgumentDataMember = "WorkDate";
            s1.ValueDataMembers.AddRange(new string[] { "WorkQty" });
            s1.DataSource = dt;
            s1.CrosshairLabelPattern = "{A} : {V:#,0} EA";
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            // 막대 디자인
            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(24, 100, 171);
            barView.BarWidth = 0.4;

            Series s2 = new Series("달성률(%)", ViewType.Line);
            s2.ArgumentDataMember = "WorkDate";
            s2.ValueDataMembers.AddRange(new string[] { "AchievementRate" });
            s2.DataSource = dt;
            s2.CrosshairLabelPattern = "{A} : {V:0.0}%";
            s2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            // 꺾은선 디자인
            LineSeriesView lineView = (LineSeriesView)s2.View;
            lineView.Color = Color.FromArgb(43, 138, 62);
            lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
            lineView.LineMarkerOptions.Size = 8;
            lineView.LineMarkerOptions.Color = Color.FromArgb(43, 138, 62);
            lineView.LineStyle.Thickness = 2;

            // 차트에 시리즈 추가
            chart.Series.AddRange(new Series[] { s1, s2 });

            XYDiagram diagram = chart.Diagram as XYDiagram;

            if (diagram != null)
            {
                // X축 설정
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;

                // Y축 설정 - 실적수량 (EA)
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V:#,#}";

                // 오른쪽 축 - 달성률 (%)
                diagram.SecondaryAxesY.Clear();
                SecondaryAxisY secondaryY = new SecondaryAxisY("AxisY_Rate");
                secondaryY.Label.TextPattern = "{V:0}";
                secondaryY.WholeRange.SetMinMaxValues(0, 120);

                diagram.SecondaryAxesY.Add(secondaryY);
                lineView.AxisY = secondaryY;
            }
        }

        private void Search_Chart_DefectRate()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 불량현황
            query = $@"
            DECLARE @CorpId   NVARCHAR(20) = '{WrGlobal.CorpID}';
            DECLARE @BaseDate DATE         = '{endDt}';

            WITH DateSlots AS (
                SELECT @BaseDate AS [WorkDate]
                UNION ALL SELECT DATEADD(DAY, -1, @BaseDate)
                UNION ALL SELECT DATEADD(DAY, -2, @BaseDate)
                UNION ALL SELECT DATEADD(DAY, -3, @BaseDate)
                UNION ALL SELECT DATEADD(DAY, -4, @BaseDate)
                UNION ALL SELECT DATEADD(DAY, -5, @BaseDate)
                UNION ALL SELECT DATEADD(DAY, -6, @BaseDate)
            ),

            -- 일자별 생산량 및 불량수량
            ResultSummary AS (
                SELECT
                    CAST(WorkDate AS DATE) AS TargetWorkDate
                    , SUM(ISNULL(WorkQty, 0)) AS TotalWorkQty
                    , SUM(ISNULL(NgQty, 0))   AS TotalNgQty
                FROM [YOZM].[dbo].[PM_OrderResult]
                WHERE CorpId = @CorpId
                  AND WorkDate >= DATEADD(DAY, -6, @BaseDate)
                  AND WorkDate <  DATEADD(DAY, 1, @BaseDate)
                GROUP BY CAST(WorkDate AS DATE)
            )

            -- 일자별 불량률
            SELECT
                CONVERT(VARCHAR(5), ds.WorkDate, 110) AS WorkDate

                -- 일자별 생산/불량/불량률
                , ISNULL(r.TotalWorkQty, 0) AS WorkQty
                , ISNULL(r.TotalNgQty, 0)   AS NgQty
                , CASE
                    WHEN ISNULL(r.TotalWorkQty, 0) = 0 THEN 0.0
                    ELSE ROUND((CAST(ISNULL(r.TotalNgQty, 0) AS FLOAT) / r.TotalWorkQty) * 100, 2)
                END AS NgRate

                -- 7일간 총 불량수량
                , SUM(ISNULL(r.TotalNgQty, 0)) OVER() AS SummaryTotalNgQty

                -- 7일간 전체 불량률 (%)
                , CASE
                    WHEN SUM(ISNULL(r.TotalWorkQty, 0)) OVER() = 0 THEN 0.0
                    ELSE ROUND((CAST(SUM(ISNULL(r.TotalNgQty, 0)) OVER() AS FLOAT) / SUM(ISNULL(r.TotalWorkQty, 0)) OVER()) * 100, 2)
                END AS SummaryTotalNgRate

            FROM DateSlots ds
            LEFT JOIN ResultSummary r ON ds.WorkDate = r.TargetWorkDate
            ORDER BY ds.WorkDate
            ";

            DataTable dt = db.GetDataTable(query);

            double totalNgQty = dt.Rows[0]["SummaryTotalNgQty"].ConvertDouble_Null_To_Zero();
            double totalNgRate = dt.Rows[0]["SummaryTotalNgRate"].ConvertDouble_Null_To_Zero();

            ChartControl chart = lineChart;
            chart.Series.Clear();
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BackColor = Color.White;

            // 범례 설정
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            chart.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series sNg = new Series("불량률(%)", ViewType.Line);
            sNg.ArgumentDataMember = "WorkDate";
            sNg.ValueDataMembers.AddRange(new string[] { "NgRate" });
            sNg.DataSource = dt;
            sNg.CrosshairLabelPattern = "{A} : {V:0.00}%";
            sNg.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            LineSeriesView lineView = (LineSeriesView)sNg.View;
            lineView.Color = Color.FromArgb(235, 60, 50);
            lineView.LineStyle.Thickness = 2;

            // 원형 포인트 마커
            lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            lineView.LineMarkerOptions.Kind = MarkerKind.Circle;
            lineView.LineMarkerOptions.Size = 7;
            lineView.LineMarkerOptions.Color = Color.FromArgb(235, 60, 50);
            lineView.LineMarkerOptions.FillStyle.FillMode = FillMode.Solid;

            chart.Series.Add(sNg);

            XYDiagram diagram = chart.Diagram as XYDiagram;
            if (diagram != null)
            {
                // X축 설정 - 일자
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;

                // Y축 설정 - 불량률
                diagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V:0.0}";
            }
        }

        private void Search_Chart_Production_Prod()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 제품별 생산 비중
            query = $@"
            DECLARE @CorpId   NVARCHAR(20) = '{WrGlobal.CorpID}';
            DECLARE @FirstDay    DATETIME = DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0);      -- 당월 1일 00:00:00
            DECLARE @NextMonthDay DATETIME = DATEADD(month, DATEDIFF(month, 0, GETDATE()) + 1, 0); -- 내달 1일 00:00:00

            WITH ItemProductionSummary AS (
                SELECT
                    R.ItemCode
                    , ISNULL(I.ItemName, R.ItemCode) AS ItemName
                    , SUM(ISNULL(R.WorkQty, 0)) AS TotalWorkQty
                FROM PM_OrderResult R
                LEFT JOIN CM_Item I ON R.CorpId = I.CorpId AND R.ItemCode = I.ItemCode
                WHERE R.CorpId = @CorpId
                  AND R.WorkDate >= @FirstDay
                  AND R.WorkDate <  @NextMonthDay
                GROUP BY R.ItemCode, I.ItemName
            )

            SELECT
                ItemCode
                , ItemName
                , TotalWorkQty AS WorkQty
            FROM ItemProductionSummary
            WHERE TotalWorkQty > 0
            ORDER BY TotalWorkQty DESC
            ";

            DataTable dt = db.GetDataTable(query);

            ChartControl chart = ChartSales_Cust;
            chart.Series.Clear();
            chart.Series.BeginUpdate();

            // 도넛 시리즈 생성
            Series doughnutSeries = new Series("제품별 생산 비중", ViewType.Doughnut);

            if (dt == null || dt.Rows.Count == 0)
            {
                doughnutSeries.Points.Add(new SeriesPoint("데이터 없음", 0));
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.Series.Add(doughnutSeries);
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string title = Convert.ToString(dr["ItemCode"]);
                    double count = Convert.ToDouble(dr["WorkQty"]);

                    SeriesPoint point = new SeriesPoint(title, count);

                    //// 각 포인트마다 직접 색상 지정 (view.Palette 대신 사용)
                    //if (i < customColors.Length)
                    //{
                    //    point.Color = customColors[i];
                    //}

                    doughnutSeries.Points.Add(point);
                }

                // 범례 설정
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chart.Legend.MarkerMode = LegendMarkerMode.Marker;

                // 도넛 그래프 라벨 활성화
                doughnutSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                doughnutSeries.Label.TextPattern = "{VP:P1}";
                doughnutSeries.LegendTextPattern = "{A}: {V:N0} EA";

                chart.Legend.Font = new Font("맑은 고딕", 9F, FontStyle.Regular);
                chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                chart.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
                chart.Legend.Direction = LegendDirection.TopToBottom;
                chart.Series.Add(doughnutSeries);

                // 도넛 뷰 및 옵션 설정
                DoughnutSeriesView view = doughnutSeries.View as DoughnutSeriesView;
                if (view != null)
                {
                    view.Rotation = 270;
                    view.HoleRadiusPercent = 60; // 가운데 구멍 크기

                    // 가운데 총합 라벨 활성화
                    view.TotalLabel.Visible = true;
                    view.TotalLabel.TextPattern = "총 생산량\n{TV:#,##0} EA";
                    view.TotalLabel.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                    view.TotalLabel.TextColor = Color.FromArgb(50, 60, 75);
                }
            }

            chart.Series.EndUpdate();
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        private void Search_Chart_Production_Goal()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 생산 목표 달성 현황
            query = $@"
            DECLARE @CorpId   NVARCHAR(20) = '{WrGlobal.CorpID}';
            DECLARE @FirstDay    DATETIME = DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0);      -- 당월 1일 00:00:00
            DECLARE @NextMonthDay DATETIME = DATEADD(month, DATEDIFF(month, 0, GETDATE()) + 1, 0); -- 내달 1일 00:00:00

            WITH GoalSummary AS (
                -- 생산 계획(목표 수량)
                SELECT ISNULL(SUM(OrderQty), 0) AS GoalQty
                FROM PM_ProductPlan
                WHERE CorpId = @CorpId
                  AND WorkDate >= @FirstDay
                  AND WorkDate <  @NextMonthDay
            ),
            ActualSummary AS (
                -- 생산 실적(작업 수량)
                SELECT ISNULL(SUM(WorkQty), 0) AS ActualQty
                FROM PM_OrderResult
                WHERE CorpId = @CorpId
                  AND WorkDate >= @FirstDay
                  AND WorkDate <  @NextMonthDay
            )
            SELECT
                G.GoalQty
                , A.ActualQty
            FROM GoalSummary G
            CROSS JOIN ActualSummary A
            ";

            DataTable dt = db.GetDataTable(query);

            double goalQty = 0;
            double actualQty = 0;

            if (dt != null && dt.Rows.Count > 0)
            {
                goalQty = Convert.ToDouble(dt.Rows[0]["GoalQty"]);
                actualQty = Convert.ToDouble(dt.Rows[0]["ActualQty"]);
            }

            // 잔여 수량 계산 (목표치 초과 시 0으로 처리)
            double remainQty = Math.Max(0, goalQty - actualQty);
            double rate = goalQty > 0 ? (actualQty / goalQty) * 100 : 0;

            ChartControl chart = chartControl1;
            chart.Series.Clear();
            chart.Annotations.Clear();
            chart.Series.BeginUpdate();

            // 도넛 시리즈 생성
            Series doughnutSeries = new Series("생산 목표 달성 현황", ViewType.Doughnut);

            double dummyValue = (actualQty + remainQty) > 0 ? (actualQty + remainQty) : 1;
            SeriesPoint dummyPoint = new SeriesPoint("Dummy", dummyValue);
            dummyPoint.Color = Color.Transparent;
            doughnutSeries.Points.Add(dummyPoint);

            // 누적 실적 (달성량)
            SeriesPoint actualPoint = new SeriesPoint("누적 실적", actualQty);
            actualPoint.Color = Color.FromArgb(40, 167, 69);
            doughnutSeries.Points.Add(actualPoint);

            // 잔여 수량 (미달성량)
            SeriesPoint remainPoint = new SeriesPoint("잔여 수량", remainQty);
            remainPoint.Color = Color.FromArgb(226, 229, 236);
            doughnutSeries.Points.Add(remainPoint);

            // 기본 범례 및 라벨 숨기기
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            doughnutSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            // 도넛 뷰(View) 반원
            DoughnutSeriesView view = doughnutSeries.View as DoughnutSeriesView;
            if (view != null)
            {
                view.HoleRadiusPercent = 55; // 구멍 크기
                view.Rotation = 180;

                view.TotalLabel.Visible = true;
                view.TotalLabel.TextPattern = $"{rate:F1}%\n\n{actualQty:#,##0} / {goalQty:#,##0} EA";
                view.TotalLabel.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
            }

            chart.Series.Add(doughnutSeries);

            chart.Series.EndUpdate();
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        #endregion

        #region 조회 쿼리 (그리드)

        private void SearchGrid_Work()
        {
            string startDt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            // 공정별 생산 현황
            query = $@"
            DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
            DECLARE @FirstDay    DATETIME = DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0);      -- 당월 1일 00:00:00
            DECLARE @NextMonthDay DATETIME = DATEADD(month, DATEDIFF(month, 0, GETDATE()) + 1, 0); -- 내달 1일 00:00:00

            WITH ProcessOrderBase AS (
                SELECT
                    OP.CorpId,
                    OP.ProcessCode,
                    P.ProcessName,
                    SUM(ISNULL(OP.OrderQty, 0)) AS OrderQty
                FROM PM_OrderProcess OP
                LEFT JOIN CM_Process P ON OP.CorpId = P.CorpId AND OP.ProcessCode = P.ProcessCode
                WHERE OP.CorpId = @CorpId
                  AND OP.OrderDate >= @FirstDay
                  AND OP.OrderDate <  @NextMonthDay
                GROUP BY OP.CorpId, OP.ProcessCode, P.ProcessName
            ),
            ResultSummary AS (
                -- 2. 해당 지시건들에 대한 공정별 작업시작/종료 상태 및 누적 생산량(WorkQty) 집계
                SELECT
                    R.CorpId,
                    OP.ProcessCode,
                    ISNULL(SUM(R.WorkQty), 0) AS WorkQty,
                    MIN(R.StartDate) AS FirstStartDate, -- 최초 시작시간
                    MAX(R.EndDate)   AS LastEndDate,    -- 최종 종료시간

                    -- 진행중 판별용: 시작시간은 입력되었으나 종료시간이 없는 경우의 건수
                    COUNT(CASE WHEN R.StartDate IS NOT NULL AND R.EndDate IS NULL THEN 1 END) AS InProgressCount,
                    -- 완료 판별용: 시작시간과 종료시간이 모두 등록 완료된 건수
                    COUNT(CASE WHEN R.StartDate IS NOT NULL AND R.EndDate IS NOT NULL THEN 1 END) AS CompletedCount
                FROM PM_OrderResult R
                INNER JOIN PM_OrderProcess OP ON R.CorpId = OP.CorpId AND R.OrderNo = OP.OrderNo AND R.OrderSeq = OP.OrderSeq
                WHERE R.CorpId = @CorpId
                  AND OP.OrderDate >= @FirstDay
                  AND OP.OrderDate <  @NextMonthDay
                GROUP BY R.CorpId, OP.ProcessCode
            )
            SELECT
                ROW_NUMBER() OVER(ORDER BY O.ProcessCode ASC) AS Seq,
                O.ProcessCode,
                O.ProcessName,

                ISNULL(O.OrderQty, 0) AS OrderQty,    -- 총 지시수량
                ISNULL(R.WorkQty, 0)  AS WorkQty,     -- 총 생산량(WorkQty)

                -- 진행률 (%) : (생산량 / 지시수량) * 100
                CASE
                    WHEN ISNULL(O.OrderQty, 0) = 0 THEN 0.0
                    WHEN (CAST(ISNULL(R.WorkQty, 0) AS FLOAT) / O.OrderQty) * 100 > 100.0 THEN 100.0
                    ELSE ROUND((CAST(ISNULL(R.WorkQty, 0) AS FLOAT) / O.OrderQty) * 100, 1)
                END AS ProgressRate,

                CASE
                    WHEN ISNULL(R.InProgressCount, 0) > 0 THEN '진행중'
                    WHEN ISNULL(R.CompletedCount, 0) > 0  THEN '완료'
                    ELSE '대기'
                END AS ProcessStatus

            FROM ProcessOrderBase O
            LEFT JOIN ResultSummary R ON O.CorpId = R.CorpId AND O.ProcessCode = R.ProcessCode
            ORDER BY O.ProcessCode ASC;
            ";

            Gc_Work.DataSource = db.GetDataTable(query);
            Gv_Work.BestFitColumns();
        }

        private void SearchGrid_Machine()
		{
			//설비가동현황
			query = $@"

DECLARE @CorpId   nvarchar(20) = '{WrGlobal.CorpID}'
DECLARE @Today    CHAR(8)      = CONVERT(CHAR(8), GETDATE(), 112); -- YYYYMMDD (테이블 형식에 따라 'YYYY-MM-DD'면 120으로 변경)
DECLARE @NowTime  DATETIME     = GETDATE();

WITH MachineBase AS (
    -- 1. 설비 기본 정보
    SELECT 
        CorpId,
        MachineCode,
        MachineName
    FROM EQ_Machine
    WHERE CorpId = @CorpId
),
TodayOrderResult AS (
    -- 2. 오늘 생산실적 및 가동시간(분) 집계
    SELECT 
        MachineCode,
        ISNULL(SUM(DATEDIFF(MINUTE, StartDate, EndDate)), 0) AS TodayRunMinutes
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate = @Today
    GROUP BY MachineCode
),
TodayStopResult AS (
    -- 3. 오늘 비가동/정지시간(분) 집계
    SELECT 
        MachineCode,
        ISNULL(SUM(DATEDIFF(MINUTE, StartTime, EndTime)), 0) AS TodayStopMinutes
    FROM EQ_Operation
    WHERE CorpId = @CorpId
      AND WorkDay = @Today
    GROUP BY MachineCode
),
LastOrderStatus AS (
    -- 4. 설비별 가장 최근 생산실적 (현재 가동 여부 확인용)
    SELECT 
        MachineCode,
        StartDate,
        EndDate,
        ROW_NUMBER() OVER(PARTITION BY MachineCode ORDER BY StartDate DESC) AS RN
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate = @Today
),
LastStopStatus AS (
    -- 5. 설비별 가장 최근 비가동 기록 (현재 정지 여부 확인용)
    SELECT 
        MachineCode,
        StartTime,
        EndTime,
        ROW_NUMBER() OVER(PARTITION BY MachineCode ORDER BY StartTime DESC) AS RN
    FROM EQ_Operation
    WHERE CorpId = @CorpId
      AND WorkDay = @Today
)
SELECT 
    ROW_NUMBER() OVER(ORDER BY M.MachineCode ASC) AS Seq,
    M.MachineCode,
    M.MachineName,
    
    -- [상태 판별 로직]
    -- 1. 최근 비가동 시작시간은 있으나 종료시간이 없거나 현재시간보다 뒤면 '정지'
    -- 2. 최근 작업 시작시간은 있으나 종료시간이 없거나 현재시간보다 뒤면 '가동중'
    -- 3. 그 외의 경우는 '대기'
    CASE 
        WHEN S.StartTime IS NOT NULL AND (S.EndTime IS NULL OR S.EndTime > @NowTime) THEN '정지'
        WHEN R.StartDate IS NOT NULL AND (R.EndDate IS NULL OR R.EndDate > @NowTime) THEN '가동중'
        ELSE '대기'
    END AS MachineStatus,
    
    -- [가동률 (%)] : 가동시간 / (가동시간 + 정지시간) * 100
    CASE 
        WHEN (ISNULL(TR.TodayRunMinutes, 0) + ISNULL(TS.TodayStopMinutes, 0)) = 0 THEN 0.0
        ELSE ROUND((CAST(ISNULL(TR.TodayRunMinutes, 0) AS FLOAT) / (TR.TodayRunMinutes + TS.TodayStopMinutes)) * 100, 1)
    END AS OperationRate,
    
    -- [금일 가동시간 (시간)] : 소수점 첫째 자리
    ROUND(ISNULL(TR.TodayRunMinutes, 0) / 60.0, 1) AS TodayRunHours,

	-- [금일 정지시간 (시간)] : 소수점 첫째 자리 (추가됨)
    ROUND(ISNULL(TS.TodayStopMinutes, 0) / 60.0, 1) AS TodayStopHours
FROM MachineBase M
LEFT JOIN TodayOrderResult TR ON M.MachineCode = TR.MachineCode
LEFT JOIN TodayStopResult TS  ON M.MachineCode = TS.MachineCode
LEFT JOIN LastOrderStatus R   ON M.MachineCode = R.MachineCode AND R.RN = 1
LEFT JOIN LastStopStatus S    ON M.MachineCode = S.MachineCode AND S.RN = 1
ORDER BY M.MachineCode ASC;


";

			DataTable dt = db.GetDataTable(query);
			GcMachine.DataSource = dt;

			GvMachine.BestFitColumns();
		}

        private void SearchGrid_Notice()
		{
            string date = DateTime.Now.ToString("d");

            SM_Notice notice = new SM_Notice();

            //Gc_Notice.DataSource = notice.Select_SM_Notice_Top5(date);
        }

        private void SearchGrid_OrderProcess()
        {
            // 최근 생산 지시 현황
            query = $@"
                     SELECT '' AS Status
							, '' AS Icon
			                , OP.OrderNo                            -- 지시번호
			                , OP.OrderSeq                           -- 지시순번
			                , OP.OrderDate                          -- 지시일자
			                , OP.ProcessCode						-- 공정코드
			                , OP.MachineCode						-- 설비코드
			                , OP.RoutingSeq							-- 라우팅순번
			                , OP.LineCode							-- 라인코드
			                , CP.ProcessName						-- 공정명
			                , WO.SdOrderNo							-- 수주번호
			                , WO.SdOrderSeq							-- 수주순번
                            , WO.CustCode							-- 거래처
                            , OP.Status AS WorkStatus               -- 작업상태

			                , OP.ItemCode ItemCode
			                , cm.ItemName
                            , cm.Alias
                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.Size
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            , cm.UnitCode
                            , cm.ColorCode
                            , cm.HouseID AS OutHouseID

			                , OP.OrderQty
                            , ISNULL(ORS.TotalQty, 0) AS TotalWorkQty
			                , 0 WorkQty
			                , 0 GoodQty
			                , 0 NgQty
							, CASE
                                WHEN ISNULL(OP.OrderQty, 0) = 0 THEN 0
                                ELSE ROUND(
                                    ISNULL(ORS.TotalQty, 0) * 100.0 / OP.OrderQty
                                , 1)
                              END AS WorkRate

                            , CONVERT(varchar(10), GETDATE(), 120) AS WorkDate

                            , '' AS WorkerID
					        , GETDATE() AS StartDate
					        , NULL AS EndDate
							, 0 AS SubSeq
                            , '' AS SerialNo
                            , '' AS LotNo
			                , OP.[Status]

	                FROM PM_OrderProcess OP
	                INNER JOIN PM_ProductPlan WO ON WO.OrderNo = OP.OrderNo
	                INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode
	                INNER JOIN CM_Item CM ON OP.ItemCode = cm.ItemCode 
	                INNER JOIN PM_SdRoute SD ON OP.OrderNo = SD.OrderNo AND OP.RoutingSeq = SD.RoutingSeq
                    INNER JOIN (
                        SELECT
                            OrderNo,
                            OrderSeq,
                            SUM(ISNULL(GoodQty, 0) + ISNULL(NgQty, 0)) AS TotalQty
                        FROM PM_OrderResult
						WHERE CorpId = '{WrGlobal.CorpID}'
						 GROUP BY OrderNo, OrderSeq
                    ) ORS ON OP.OrderNo = ORS.OrderNo AND OP.OrderSeq = ORS.OrderSeq
                     ORDER BY OrderDate Desc
                    ";

            Gc_OrderProcess.DataSource = db.GetDataTable(query);
            Gv_OrderProcess.BestFitColumns();
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

		private void btn_Colose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}