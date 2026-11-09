using DevExpress.CodeParser;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models.PM;
using System;
using System.Data;
using System.Drawing;

namespace SmartMES.View.Analysis
{
	public partial class Frm_AN_Plan_ProductView : DevExpress.XtraEditors.XtraForm
    {

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";


        public Frm_AN_Plan_ProductView()
        {
            InitializeComponent();

            // 상단 조회조건 색상 설정
            smtCommonBar1.smtBackColor = Color.Transparent;

            barDate.smtLabelForeColor = Color.Black;
            LupProcess.smtLabelForeColor = Color.Black;
            LupItem.smtLabelForeColor = Color.Black;

            smtSearchButton1.ForeColor = Color.Black;
            btn_Colose.ForeColor = Color.Black;
        }

        private void Frm_AN_DefectType_Load(object sender, EventArgs e)
        {
            barDate.smtEndDateTimeValue = DateTime.Now;

            Tm_Time.Start();

            SetGrid(Gv_Work);
            SetGrid_Process(gridView1);

            Search();

            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)
        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "계획수량", "PlanQty");
            grid.Create_GvColumn(gv, "양품수량", "ResultQty");
            grid.Create_GvColumn(gv, "진행률(%)", "AchievementRate");
            grid.Create_GvColumn(gv, "상태", "Status");

            //
            grid.SetCol_Type(gv.Columns["PlanQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ResultQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["AchievementRate"], clsGrid.TypeList.TFloat);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
			gv.RowCellStyle += Gv_RowCellStyle;
        }

		private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
            if(e.Column.FieldName == "Status")
			{
                string value = e.CellValue.ConvertString_Null_To_Empty();

                if(value == "지연")
				{
                    e.Appearance.BackColor = Color.FromArgb(255, 243, 205); // 연한 노란색(파스텔톤)
                    e.Appearance.ForeColor = Color.FromArgb(180, 120, 0);   // 진한 주황/갈색 (글자)
                }
                else if(value == "정상")
				{
                    e.Appearance.BackColor = Color.FromArgb(220, 245, 230); // 연한 초록색
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색 (글자)
                }
			}
		}

		private void SetGrid_Process(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "지시수량", "PlanQty");
            grid.Create_GvColumn(gv, "양품수량", "ResultQty");
            grid.Create_GvColumn(gv, "달성률(%)", "AchievementRate");

            //
            grid.SetCol_Type(gv.Columns["PlanQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ResultQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["AchievementRate"], clsGrid.TypeList.TFloat);

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.CustomDrawCell += gridView1_CustomDrawCell;
        }
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "AchievementRate")
            {
                if (e.CellValue == null || e.CellValue == DBNull.Value) return;

                double rate = Convert.ToDouble(e.CellValue);

                // 1. 기존 e.DefaultDraw()를 제거하고, 셀 배경만 직접 지워줍니다. (텍스트 중복 방지)
                e.Cache.FillRectangle(e.Appearance.GetBackBrush(e.Cache), e.Bounds);

                // 2. 프로그레스 바 채울 여백 및 크기 계산
                Rectangle rect = e.Bounds;
                rect.Inflate(-4, -4); // 셀 안쪽 여백

                int barHeight = 8; // 얇은 프로그레스 바 두께
                int barY = rect.Y + (rect.Height - barHeight) / 2;

                int textWidth = 55; // 우측 텍스트 영역 너비
                int barMaxWidth = rect.Width - textWidth;

                if (barMaxWidth > 0)
                {
                    Rectangle bgRect = new Rectangle(rect.X, barY, barMaxWidth, barHeight);

                    // 비율 계산 (0 ~ 100%)
                    double percent = Math.Min(Math.Max(rate, 0), 100) / 100.0;
                    int fillWidth = (int)(barMaxWidth * percent);
                    Rectangle fillRect = new Rectangle(rect.X, barY, fillWidth, barHeight);

                    // 3. 바 그리기
                    using (Brush bgBrush = new SolidBrush(Color.FromArgb(235, 240, 245))) // 바 바탕색(연회색)
                    using (Brush fillBrush = new SolidBrush(Color.FromArgb(51, 102, 255))) // 채움색(파란색)
                    {
                        e.Graphics.FillRectangle(bgBrush, bgRect);
                        if (fillWidth > 0)
                        {
                            e.Graphics.FillRectangle(fillBrush, fillRect);
                        }
                    }

                    // 4. 우측 퍼센트 텍스트(예: 82.0%) 그리기
                    Rectangle textRect = new Rectangle(rect.X + barMaxWidth, rect.Y, textWidth, rect.Height);
                    string text = string.Format("{0:F1}%", rate);

                    using (Brush textBrush = new SolidBrush(Color.FromArgb(50, 60, 75)))
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Far; // 우측 정렬
                        sf.LineAlignment = StringAlignment.Center;
                        e.Cache.DrawString(text, e.Appearance.Font, textBrush, textRect, sf);

                    }
                }

                // 5. 기본 그리기 작업 완료 처리
                e.Handled = true;
            }
        }
        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            // 실적 일자 기준 조회
            string startDt = barDate.smtStartDateTimeValue.ToString("yyyy-MM-01");
            string endDt = Convert.ToDateTime(startDt).AddMonths(1).AddDays(-1).ToString("d");

            SearchCardData(startDt, endDt);           // 상단 카드
            Search_Chart_Day(startDt, endDt);    // 일별 계획대비 생산 
            Search_Chart_Rate(startDt, endDt);    // 달성률분포 
            SearchGrid_Work(startDt, endDt);          // 하단 생산진행현황
            SearchGrid_Process(startDt, endDt);          // 하단 생산진행현황
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData(string startDt, string endDt)
        {
            query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                        DECLARE @StartDate DATE = '{startDt}'
                        DECLARE @EndDate DATE = '{endDt}'

                 SELECT 
    ISNULL(SUM(p.OrderQty), 0) AS TotalPlanQty,                       -- 계획수량
    ISNULL(SUM(r.GoodQty), 0) AS TotalGoodQty,                       -- 실적수량(양품)
    CASE 
        WHEN ISNULL(SUM(p.OrderQty), 0) = 0 THEN 0
        ELSE ROUND(CAST(ISNULL(SUM(r.GoodQty), 0) AS FLOAT) / SUM(p.OrderQty) * 100, 1)
    END AS TotalAchievementRate,                                     -- 달성률(%)
    CASE 
        WHEN ISNULL(SUM(p.OrderQty), 0) - ISNULL(SUM(r.GoodQty), 0) < 0 THEN 0
        ELSE ISNULL(SUM(p.OrderQty), 0) - ISNULL(SUM(r.GoodQty), 0)
    END AS TotalRemainQty,                                           -- 남은수량
    ISNULL(SUM(
        CASE 
            WHEN p.DeliveryDate < CONVERT(VARCHAR(10), GETDATE(), 120) 
                 AND ISNULL(r.GoodQty, 0) < p.OrderQty THEN 1
            ELSE 0
        END
    ), 0) AS DelayCount                                                  -- 지연건수
FROM PM_ProductPlan p
LEFT JOIN (
    SELECT 
        CorpId,
        OrderNo,
        SUM(GoodQty) AS GoodQty
    FROM PM_OrderResult
    GROUP BY CorpId, OrderNo
) r ON p.CorpId = r.CorpId AND p.OrderNo = r.OrderNo
WHERE p.CorpId = @CorpId
AND p.WorkDate BETWEEN @StartDate AND @EndDate;


            ";
            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                float planQty = dr["TotalPlanQty"].ConvertFloat_Null_To_Zero();
                float workQty = dr["TotalGoodQty"].ConvertFloat_Null_To_Zero();

                LblTotalPlanQty.Text = planQty.ToString("N0") + " EA";
                LblTotalWorkQty.Text = workQty.ToString("N0") + " EA";
                LblAchieveRate.Text = dr["TotalAchievementRate"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblGoodQty.Text = dr["TotalRemainQty"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";
                LblNgQty.Text = dr["DelayCount"].ConvertInt_Null_To_Zero() + " 건";

            }
        }
        #endregion

        #region 차트 로드 
        private void Search_Chart_Day(string startDt, string endDt)
        {
            // 1. SQL 쿼리 작성 (CorpId 조건 포함)
            query = $@"
        DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
        , @StartDate DATE = '{startDt}'
        , @EndDate DATE = '{endDt}'

        SELECT 
            d.WorkDate,
            ISNULL(p.PlanQty, 0) AS PlanQty,
            ISNULL(r.ResultQty, 0) AS ResultQty,
            CASE 
                WHEN ISNULL(p.PlanQty, 0) = 0 THEN 0
                ELSE ROUND(CAST(ISNULL(r.ResultQty, 0) AS FLOAT) / p.PlanQty * 100, 0)
            END AS AchievementRate
        FROM (
            SELECT WorkDate FROM PM_ProductPlan WHERE CorpId = @CorpId AND WorkDate BETWEEN @StartDate AND @EndDate
            UNION
            SELECT WorkDate FROM PM_OrderResult WHERE CorpId = @CorpId AND WorkDate BETWEEN @StartDate AND @EndDate
        ) d
        LEFT JOIN (
            SELECT 
                WorkDate,
                SUM(OrderQty) AS PlanQty
            FROM PM_ProductPlan
            WHERE CorpId = @CorpId AND WorkDate BETWEEN @StartDate AND @EndDate
            GROUP BY WorkDate
        ) p ON d.WorkDate = p.WorkDate
        LEFT JOIN (
            SELECT 
                WorkDate,
                SUM(GoodQty) AS ResultQty
            FROM PM_OrderResult
            WHERE CorpId = @CorpId AND WorkDate BETWEEN @StartDate AND @EndDate
            GROUP BY WorkDate
        ) r ON d.WorkDate = r.WorkDate
        ORDER BY d.WorkDate ASC;
    ";

            DataTable dt = db.GetDataTable(query);

            // 2. 차트 및 범례 초기화
            ChartDay.Series.Clear();
            ChartDay.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

            // ---------------------------------------------------------
            // 시리즈 생성 & 데이터 바인딩
            // ---------------------------------------------------------

            // A. 계획수량 (꺾은선 / 점선)
            Series seriesPlan = new Series("계획수량", ViewType.Line);
            seriesPlan.ArgumentDataMember = "WorkDate";
            seriesPlan.ValueDataMembers.AddRange(new string[] { "PlanQty" });
            seriesPlan.DataSource = dt;
            seriesPlan.CrosshairLabelPattern = "{S}: {V:#,0} EA";

            // B. 실적수량 (꺾은선 / 실선)
            Series seriesResult = new Series("양품수량", ViewType.Line);
            seriesResult.ArgumentDataMember = "WorkDate";
            seriesResult.ValueDataMembers.AddRange(new string[] { "ResultQty" });
            seriesResult.DataSource = dt;
            seriesResult.CrosshairLabelPattern = "{S}: {V:#,0} EA";

            // C. 달성률 (막대)
            Series seriesRate = new Series("달성률(%)", ViewType.Bar);
            seriesRate.ArgumentDataMember = "WorkDate";
            seriesRate.ValueDataMembers.AddRange(new string[] { "AchievementRate" });
            seriesRate.DataSource = dt;
            seriesRate.CrosshairLabelPattern = "{S}: {V:0}%";
            seriesRate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True; // 막대 상단 라벨 표시

            // 시리즈 차트에 추가
            ChartDay.Series.Add(seriesRate);   // 막대를 가장 먼저 추가하여 선에 가려지지 않게 처리
            ChartDay.Series.Add(seriesPlan);
            ChartDay.Series.Add(seriesResult);

            // ---------------------------------------------------------
            // 다이어그램 및 축(Y-Axis / Secondary Y-Axis) 설정
            // ---------------------------------------------------------
            XYDiagram diagramCnt = (XYDiagram)ChartDay.Diagram;
            diagramCnt.DefaultPane.BackColor = Color.Transparent;

            // X축 설정
            diagramCnt.AxisX.Label.TextPattern = "{A:MM-dd}";
            diagramCnt.AxisX.GridLines.Visible = false;
            diagramCnt.AxisX.Color = Color.FromArgb(200, 205, 215);
            diagramCnt.AxisX.Label.TextColor = Color.FromArgb(100, 110, 125);
            diagramCnt.AxisX.Label.Font = new Font(this.Font.FontFamily, 8, FontStyle.Regular);

            // 주 Y축 설정 (수량 - EA)
            diagramCnt.AxisY.Label.TextPattern = "{V:#,##0}";
            diagramCnt.AxisY.Label.TextColor = Color.FromArgb(100, 110, 125);
            diagramCnt.AxisY.Label.Font = new Font(this.Font.FontFamily, 8, FontStyle.Regular);
            diagramCnt.AxisY.GridLines.Color = Color.FromArgb(240, 240, 240);

            // 보조 Y축 설정 (달성률 - %)
            SecondaryAxisY secondaryAxisY = new SecondaryAxisY("RateAxis");
            diagramCnt.SecondaryAxesY.Clear();
            diagramCnt.SecondaryAxesY.Add(secondaryAxisY);

            secondaryAxisY.Label.TextPattern = "{V}%";
            secondaryAxisY.Label.TextColor = Color.FromArgb(100, 110, 125);
            secondaryAxisY.Label.Font = new Font(this.Font.FontFamily, 8, FontStyle.Regular);
            secondaryAxisY.GridLines.Visible = false;
            secondaryAxisY.WholeRange.SetMinMaxValues(0, 120); // 이미지처럼 0~120% 스케일 고정

            // ---------------------------------------------------------
            // 시리즈 스타일 & 뷰 세부 디자인
            // ---------------------------------------------------------

            // 1) 계획수량 스타일 (회색 점선)
            LineSeriesView lineViewPlan = (LineSeriesView)seriesPlan.View;
            lineViewPlan.Color = Color.FromArgb(160, 170, 185);
            lineViewPlan.LineStyle.Thickness = 2;
            lineViewPlan.LineStyle.DashStyle = DashStyle.Dash; // 점선 설정
            lineViewPlan.MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;

            // 2) 실적수량 스타일 (파란색 실선 + 마커)
            LineSeriesView lineViewResult = (LineSeriesView)seriesResult.View;
            lineViewResult.Color = Color.FromArgb(82, 196, 143);
            lineViewResult.LineStyle.Thickness = 3;
            lineViewResult.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            //lineViewResult.SeriesMarkerOptions.Size = 6;

            // 3) 달성률 스타일 (민트색 막대 + 보조Y축 매핑 + 상단 라벨)
            SideBySideBarSeriesView barViewRate = (SideBySideBarSeriesView)seriesRate.View;
            barViewRate.Color = Color.FromArgb(55, 91, 150);
            barViewRate.AxisY = secondaryAxisY; // 보조 Y축으로 연결
            barViewRate.BarWidth = 0.35;         // 슬림한 막대 너비

            // 막대 상단 텍스트 라벨 (예: 92%)
            seriesRate.Label.TextPattern = "{V}%";
            BarSeriesLabel barLabel = (BarSeriesLabel)seriesRate.Label;
            barLabel.Position = BarSeriesLabelPosition.Top;
            barLabel.BackColor = Color.Transparent;
            barLabel.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            barLabel.Font = new Font(this.Font.FontFamily, 8, FontStyle.Bold);
            barLabel.TextColor = Color.FromArgb(70, 80, 95);
        }
        private void Search_Chart_Rate(string startDt, string endDt)
        {
            query = $@"
        DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
        , @StartDate date = '{startDt}'
        , @EndDate DATE = '{endDt}';

        WITH PlanVsResult AS (
            SELECT 
                p.OrderNo,
                p.ItemCode,
                SUM(p.OrderQty) AS PlanQty,
                ISNULL(SUM(r.GoodQty), 0) AS ResultQty,
                CASE 
                    WHEN SUM(p.OrderQty) = 0 THEN 0
                    ELSE (CAST(ISNULL(SUM(r.GoodQty), 0) AS FLOAT) / SUM(p.OrderQty)) * 100
                END AS AchievementRate
            FROM PM_ProductPlan p
            LEFT JOIN PM_OrderResult r 
                   ON p.CorpId = r.CorpId 
                  AND p.OrderNo = r.OrderNo
            WHERE p.CorpId = @CorpId
              AND p.WorkDate BETWEEN @StartDate AND @EndDate
            GROUP BY p.OrderNo, p.ItemCode
        ),
        CategoryData AS (
            SELECT 
                CASE 
                    WHEN AchievementRate >= 100 THEN '100% 이상'
                    WHEN AchievementRate >= 90 THEN '90% ~ 100% 미만'
                    WHEN AchievementRate >= 70 THEN '70% ~ 90% 미만'
                    WHEN AchievementRate >= 50 THEN '50% ~ 70% 미만'
                    ELSE '50% 미만'
                END AS CategoryGroup,
                CASE 
                    WHEN AchievementRate >= 100 THEN 1
                    WHEN AchievementRate >= 90 THEN 2
                    WHEN AchievementRate >= 70 THEN 3
                    WHEN AchievementRate >= 50 THEN 4
                    ELSE 5
                END AS SortOrder
            FROM PlanVsResult
        )
        SELECT 
            CategoryGroup,
            COUNT(*) AS OrderCount,
            ROUND(CAST(COUNT(*) AS FLOAT) / SUM(COUNT(*)) OVER() * 100, 1) AS Ratio
        FROM CategoryData
        GROUP BY CategoryGroup, SortOrder
        ORDER BY SortOrder ASC;
    ";

            DataTable dt = db.GetDataTable(query);

            ChartControl chart = ChartRate;

            chart.Series.Clear();
            chart.Series.BeginUpdate();

            // 도넛 시리즈 생성
            Series doughnutSeries = new Series("달성률 분포", ViewType.Doughnut);

            if (dt == null || dt.Rows.Count == 0)
            {
                doughnutSeries.Points.Add(new SeriesPoint("데이터 없음", 0));
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.Series.Add(doughnutSeries);
            }
            else
            {
            //    // 이미지와 동일한 5개 구간 색상 배열 정의
            //    Color[] customColors = new Color[]
            //    {
            //Color.FromArgb(51, 122, 255),  // 100% 이상 (파랑)
            //Color.FromArgb(82, 196, 143),  // 90%~100% 미만 (민트)
            //Color.FromArgb(255, 178, 63),  // 70%~90% 미만 (주황)
            //Color.FromArgb(242, 88, 88),   // 50%~70% 미만 (빨강)
            //Color.FromArgb(155, 89, 218)   // 50% 미만 (보라)
            //    };

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string title = Convert.ToString(dr["CategoryGroup"]);
                    double count = Convert.ToDouble(dr["OrderCount"]); // 건수(OrderCount) 바인딩

                    SeriesPoint point = new SeriesPoint(title, count);

                    //// 각 포인트마다 직접 색상 지정 (view.Palette 대신 사용)
                    //if (i < customColors.Length)
                    //{
                    //    point.Color = customColors[i];
                    //}

                    doughnutSeries.Points.Add(point);
                }

                // 그래프 자체 조각 라벨 숨기기
                doughnutSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

                // 범례(Legend) 설정
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chart.Legend.MarkerMode = LegendMarkerMode.Marker;

                // {A} = 구간명, {V} = 건수, {VP:P1} = 차트 자동 계산 백분율
                // 예: "100% 이상    2 (16.7%)"
                doughnutSeries.LegendTextPattern = "{A}\t\t{V} ({VP:P1})";

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
                    view.TotalLabel.TextPattern = "전체\n{TV:#,##0} 개"; // {TV} : 전체 건수 합계
                    view.TotalLabel.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                    view.TotalLabel.TextColor = Color.FromArgb(50, 60, 75);
                }
            }

            chart.Series.EndUpdate();
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        #endregion

        #region 조회 쿼리

        private void SearchGrid_Work(string startDt, string endDt)
        {
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
, @StartDate date = '{startDt}'
, @EndDate DATE = '{endDt}';

SELECT 
    p.ItemCode AS ItemCode,
    it.ItemName,
    SUM(p.OrderQty) AS PlanQty,
    ISNULL(SUM(r.GoodQty), 0) AS ResultQty,
    CASE 
        WHEN SUM(p.OrderQty) = 0 THEN 0.0
        ELSE ROUND(CAST(ISNULL(SUM(r.GoodQty), 0) AS FLOAT) / SUM(p.OrderQty) * 100, 1)
    END AS AchievementRate,
    CASE 
        WHEN SUM(p.OrderQty) - ISNULL(SUM(r.GoodQty), 0) < 0 THEN 0
        ELSE SUM(p.OrderQty) - ISNULL(SUM(r.GoodQty), 0)
    END AS RemainQty,
    CASE 
        WHEN MAX(p.DeliveryDate) < CONVERT(VARCHAR(10), GETDATE(), 120) 
             AND ISNULL(SUM(r.GoodQty), 0) < SUM(p.OrderQty) THEN '지연'
        ELSE '정상'
    END AS Status
FROM PM_ProductPlan p
LEFT JOIN CM_Item it ON p.CorpId = it.COrpId AND p.ItemCode = it.ItemCode
LEFT JOIN (
    SELECT 
        CorpId,
        OrderNo,
        ItemCode,
        SUM(GoodQty) AS GoodQty
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
    GROUP BY CorpId, OrderNo, ItemCode
) r ON p.CorpId = r.CorpId AND p.OrderNo = r.OrderNo AND p.ItemCode = r.ItemCode
WHERE p.CorpId = @CorpId
  AND p.WorkDate BETWEEN @StartDate AND @EndDate
GROUP BY p.ItemCode, it.ItemName
ORDER BY p.ItemCode ASC;
";


            Gc_Work.DataSource = db.GetDataTable(query);
            Gv_Work.BestFitColumns();
        }

		private void SearchGrid_Process(string startDt, string endDt)
		{
			query = $@"
        DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
        , @StartDate date = '{startDt}'
        , @EndDate DATE = '{endDt}';


-- 1. 계획 및 라인/공정 기본 데이터 준비 (임시 테이블)
WITH PlanBase AS (
    SELECT 
        p.CorpId,
        p.OrderNo,
        p.OrderQty,
        ISNULL(p.ProcessCode, '') AS ProcessCode,
        ISNULL(CP.ProcessName, '') AS ProcessName
    FROM PM_OrderProcess p
    LEFT JOIN CM_Process CP 
           ON p.CorpId = CP.CorpId 
          AND p.ProcessCode = CP.ProcessCode
    WHERE p.CorpId = @CorpId
      AND p.OrderDate BETWEEN @StartDate AND @EndDate
),
-- 2. 실적 데이터 사전 집계 (임시 테이블)
ResultBase AS (
    SELECT 
        CorpId,
        OrderNo,
        ISNULL(ProcessCode, '') AS ProcessCode,
        SUM(GoodQty)            AS GoodQty
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate BETWEEN @StartDate AND @EndDate
    GROUP BY CorpId, OrderNo, ISNULL(ProcessCode, '')
)
-- 3. 최종 집계 및 달성률 계산
SELECT 
    p.ProcessCode AS ProcessCodeM,
    p.ProcessName,
    SUM(p.OrderQty) AS PlanQty,
    ISNULL(SUM(r.GoodQty), 0) AS ResultQty,
    CASE 
        WHEN SUM(p.OrderQty) = 0 THEN 0.0
        ELSE ROUND(CAST(ISNULL(SUM(r.GoodQty), 0) AS FLOAT) / SUM(p.OrderQty) * 100, 1)
    END AS AchievementRate
FROM PlanBase p
LEFT JOIN ResultBase r
       ON p.CorpId = r.CorpId
      AND p.OrderNo = r.OrderNo
GROUP BY 
    p.ProcessCode, 
    p.ProcessName
ORDER BY 
    p.ProcessCode ASC;
        ";


			gridControl1.DataSource = db.GetDataTable(query);
			gridView1.BestFitColumns();
		}

		#endregion

		#region event

		private void timer1_Tick(object sender, EventArgs e)
		{
            //LblCurTime.Text = DateTime.Now.ToString("yyyy.MM.dd HH:MM:ss");
        }

		private void Tm_Search_Tick(object sender, EventArgs e)
		{
            Search();
		}

        private void smtSearchButton1_Click(object sender, EventArgs e)
        {
            Search();
        }

        #endregion

    }
}