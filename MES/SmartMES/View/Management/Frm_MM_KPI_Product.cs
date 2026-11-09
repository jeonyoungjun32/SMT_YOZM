using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpreadsheet.Model;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Drawing;
using static DevExpress.Utils.MVVM.Internal.ILReader;
using System.Xaml;
using DevExpress.DashboardCommon;
using DevExpress.XtraSpreadsheet.UI;
using System.Windows.Forms;
using DevExpress.Charts.Native;
using DevExpress.Utils;
using Legend = DevExpress.XtraCharts.Legend;

namespace SmartMES.View.Management
{
	public partial class Frm_MM_KPI_Product : DevExpress.XtraEditors.XtraForm
	{
		clsGrid grid = new clsGrid();
		Database db = new Database();
		string query = "";


		public Frm_MM_KPI_Product()
		{
			InitializeComponent();
			InitializeCharts();
			InitializeGrid();
		}

		private void Frm_MM_KPI_Product_Load(object sender, EventArgs e)
		{
			// 기본 조회 기간: 오늘 기준 -7일 ~ 오늘
			Dt_Date.smtStartDateTimeValue = DateTime.Today.AddDays(-7);
			Dt_Date.smtEndDateTimeValue = DateTime.Today;

			btn_Search.Click += (s, ev) => Search();
			btn_Close.Click += (s, ev) => this.Close();
			simpleButton1.Click += (s, ev) => ExcelDownload();

			//Tm_Time.Start();
			Search();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// 필요 시 시간 표시용 레이블 갱신
		}

		private void Tm_Search_Tick(object sender, EventArgs e)
		{
			Search(); // 3분마다 자동 조회
		}


		#region ── 메인 조회 ─────────────────────────────────────────────

		private void Search()
		{
			DateTime dtStart = Dt_Date.smtStartDateTimeValue.Date;
			DateTime dtEnd = Dt_Date.smtEndDateTimeValue.Date;

			try
			{
				LoadKpiCards(dtStart, dtEnd);
				LoadTrendCharts(dtStart, dtEnd);
				LoadDetailGrid(dtStart, dtEnd);
				LoadCustomerBarChart(dtStart, dtEnd);
				LoadMaterialBarChart(dtStart, dtEnd);
			}
			catch (Exception ex)
			{
				DevExpress.XtraEditors.XtraMessageBox.Show(
					$"조회 중 오류가 발생했습니다.\n{ex.Message}", "오류",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region ── SQL 쿼리 상수 ─────────────────────────────────────────

		// ▌ KPI 카드: 당일(조회기간) / 전일 / 전주 집계
		private const string SQL_KPI = @"
DECLARE @Start DATE = '{0}'
DECLARE @End   DATE = '{1}'
DECLARE @PreD  DATE = DATEADD(DAY, -1, @Start)
DECLARE @PreW  DATE = DATEADD(DAY, -7, @Start)

;WITH
-- 생산량 / 양품수 / 불량수
ProdBase AS (
    SELECT
        SUM(WorkQty) AS WorkQty,
        SUM(GoodQty) AS GoodQty,
        SUM(NgQty)   AS NgQty
    FROM PM_OrderResult
    WHERE CorpId = '1000'
      AND CAST(WorkDate AS DATE) BETWEEN @Start AND @End
),
ProdPreD AS (
    SELECT SUM(WorkQty) AS WorkQty, SUM(GoodQty) AS GoodQty, SUM(NgQty) AS NgQty
    FROM PM_OrderResult
    WHERE CorpId = '1000' AND CAST(WorkDate AS DATE) = @PreD
),
ProdPreW AS (
    SELECT SUM(WorkQty) AS WorkQty, SUM(GoodQty) AS GoodQty, SUM(NgQty) AS NgQty
    FROM PM_OrderResult
    WHERE CorpId = '1000' AND CAST(WorkDate AS DATE) = @PreW
),
-- 불량률 (NgQty / WorkQty * 100)
NgPerBase AS (
    SELECT CASE WHEN SUM(WorkQty) > 0
                THEN ROUND(SUM(NgQty) * 100.0 / SUM(WorkQty), 2)
                ELSE 0 END AS NgPer
    FROM PM_OrderResult
    WHERE CorpId = '1000' AND CAST(WorkDate AS DATE) BETWEEN @Start AND @End
),
NgPerPreD AS (
    SELECT CASE WHEN SUM(WorkQty) > 0
                THEN ROUND(SUM(NgQty) * 100.0 / SUM(WorkQty), 2)
                ELSE 0 END AS NgPer
    FROM PM_OrderResult
    WHERE CorpId = '1000' AND CAST(WorkDate AS DATE) = @PreD
),
NgPerPreW AS (
    SELECT CASE WHEN SUM(WorkQty) > 0
                THEN ROUND(SUM(NgQty) * 100.0 / SUM(WorkQty), 2)
                ELSE 0 END AS NgPer
    FROM PM_OrderResult
    WHERE CorpId = '1000' AND CAST(WorkDate AS DATE) = @PreW
),
-- UPH: GoodQty / 실가동시간(시간)
UphBase AS (
    SELECT ROUND(
        SUM(GoodQty) / NULLIF(SUM(DATEDIFF(MINUTE, StartDate, EndDate)) / 60.0, 0)
    , 1) AS UPH
    FROM PM_OrderResult
    WHERE CorpId = '1000'
      AND CAST(WorkDate AS DATE) BETWEEN @Start AND @End
      AND StartDate IS NOT NULL AND EndDate IS NOT NULL
),
UphPreD AS (
    SELECT ROUND(
        SUM(GoodQty) / NULLIF(SUM(DATEDIFF(MINUTE, StartDate, EndDate)) / 60.0, 0)
    , 1) AS UPH
    FROM PM_OrderResult
    WHERE CorpId = '1000' AND CAST(WorkDate AS DATE) = @PreD
      AND StartDate IS NOT NULL AND EndDate IS NOT NULL
),
UphPreW AS (
    SELECT ROUND(
        SUM(GoodQty) / NULLIF(SUM(DATEDIFF(MINUTE, StartDate, EndDate)) / 60.0, 0)
    , 1) AS UPH
    FROM PM_OrderResult
    WHERE CorpId = '1000' AND CAST(WorkDate AS DATE) = @PreW
      AND StartDate IS NOT NULL AND EndDate IS NOT NULL
),
-- 설비정지시간 (분 합계)
StopBase AS (
    SELECT SUM(StopTime) AS StopTime
    FROM EQ_Operation
    WHERE CorpId = '1000'
      AND CAST(WorkDay AS DATE) BETWEEN @Start AND @End
      AND OperationYn = 'N'
),
StopPreD AS (
    SELECT SUM(StopTime) AS StopTime
    FROM EQ_Operation
    WHERE CorpId = '1000' AND CAST(WorkDay AS DATE) = @PreD AND OperationYn = 'N'
),
StopPreW AS (
    SELECT SUM(StopTime) AS StopTime
    FROM EQ_Operation
    WHERE CorpId = '1000' AND CAST(WorkDay AS DATE) = @PreW AND OperationYn = 'N'
)
SELECT
    -- 생산량
    ISNULL((SELECT WorkQty  FROM ProdBase),    0) AS WorkQty_Total,
    ISNULL((SELECT WorkQty  FROM ProdPreD),    0) AS WorkQty_PreD,
    ISNULL((SELECT WorkQty  FROM ProdPreW),    0) AS WorkQty_PreW,
    -- 양품수
    ISNULL((SELECT GoodQty  FROM ProdBase),    0) AS GoodQty_Total,
    ISNULL((SELECT GoodQty  FROM ProdPreD),    0) AS GoodQty_PreD,
    ISNULL((SELECT GoodQty  FROM ProdPreW),    0) AS GoodQty_PreW,
    -- 불량수
    ISNULL((SELECT NgQty    FROM ProdBase),    0) AS NgQty_Total,
    ISNULL((SELECT NgQty    FROM ProdPreD),    0) AS NgQty_PreD,
    ISNULL((SELECT NgQty    FROM ProdPreW),    0) AS NgQty_PreW,
    -- 불량률
    ISNULL((SELECT NgPer    FROM NgPerBase),   0) AS NgPer_Total,
    ISNULL((SELECT NgPer    FROM NgPerPreD),   0) AS NgPer_PreD,
    ISNULL((SELECT NgPer    FROM NgPerPreW),   0) AS NgPer_PreW,
    -- 시간당 생산량 (UPH)
    ISNULL((SELECT UPH      FROM UphBase),     0) AS UPH_Total,
    ISNULL((SELECT UPH      FROM UphPreD),     0) AS UPH_PreD,
    ISNULL((SELECT UPH      FROM UphPreW),     0) AS UPH_PreW,
    -- 설비정지시간
    ISNULL((SELECT StopTime FROM StopBase),    0) AS StopTime_Total,
    ISNULL((SELECT StopTime FROM StopPreD),    0) AS StopTime_PreD,
    ISNULL((SELECT StopTime FROM StopPreW),    0) AS StopTime_PreW
";

		// ▌ 추이 차트: 날짜별 일별 집계
		private const string SQL_TREND = @"
;WITH DateRange AS (
    SELECT CAST(DATEADD(DAY, n, '{0}') AS DATE) AS Dt
    FROM (
        SELECT TOP (DATEDIFF(DAY, '{0}', '{1}') + 1)
               ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) - 1 AS n
        FROM sys.all_objects
    ) x
),
ResultBase AS (
    SELECT
        CAST(WorkDate AS DATE) AS Dt,
        SUM(WorkQty) AS workQty,
        SUM(GoodQty) AS GoodQty,
        SUM(NgQty) AS NgQty,
        ROUND(
            SUM(NgQty) * 100.0
            / NULLIF(SUM(WorkQty), 0)
        , 1) AS NgPer
    FROM PM_OrderResult
    WHERE CorpId = '1000'
      AND CAST(WorkDate AS DATE) BETWEEN '{0}' AND '{1}'
    GROUP BY CAST(WorkDate AS DATE)
),
UPHBase AS (
    SELECT
        CAST(WorkDate AS DATE) AS Dt,

        ROUND(
            SUM(GoodQty)
            /
            NULLIF(
                SUM(DATEDIFF(MINUTE, StartDate, EndDate)) / 60.0
            , 0)
        , 1) AS UPH,

        ROUND(
            AVG(
                DATEDIFF(MINUTE, StartDate, EndDate)
            )
        , 1) AS ProdLT

    FROM PM_OrderResult
    WHERE CorpId = '1000'
      AND StartDate IS NOT NULL
      AND EndDate IS NOT NULL
      AND CAST(WorkDate AS DATE) BETWEEN '{0}' AND '{1}'
    GROUP BY CAST(WorkDate AS DATE)
),
StopBase AS (
    SELECT
        CAST(WorkDay AS DATE) AS Dt,
        SUM(StopTime) AS StopTime
    FROM EQ_Operation
    WHERE CorpId = '1000'
      AND OperationYn = 'N'
      AND CAST(WorkDay AS DATE) BETWEEN '{0}' AND '{1}'
    GROUP BY CAST(WorkDay AS DATE)
)
SELECT
    D.Dt,

    -- 생산량 추이
    ISNULL(R.workQty, 0) AS workQty,
    ISNULL(R.GoodQty, 0) AS GoodQty,

    -- 불량률 추이
    ISNULL(R.NgQty, 0) AS NgQty,
    ISNULL(R.NgPer, 0) AS NgPer,

    -- 시간당 생산량 추이
    ISNULL(U.UPH, 0) AS UPH,
    ISNULL(U.ProdLT, 0) AS ProdLT,

    -- 설비정지시간 추이
    ISNULL(S.StopTime, 0) AS StopTime

FROM DateRange D
LEFT JOIN ResultBase R ON D.Dt = R.Dt
LEFT JOIN UPHBase U ON D.Dt = U.Dt
LEFT JOIN StopBase S ON D.Dt = S.Dt
ORDER BY D.Dt
";

		// ▌ 상세 그리드: 일자별
		private const string SQL_DETAIL = @"
		;WITH PlanBase AS (
			SELECT
				CAST(WorkDate AS DATE) AS Dt,
				SUM(OrderQty) AS 생산계획
			FROM PM_ProductPlan
			WHERE CorpId = '1000'
			  AND CAST(WorkDate AS DATE) BETWEEN '{0}' AND '{1}'
			GROUP BY CAST(WorkDate AS DATE)
		),
		ResultBase AS (
			SELECT
				CAST(WorkDate AS DATE) AS Dt,
				SUM(WorkQty) AS 생산실적,
				SUM(GoodQty) AS 양품수량,
				SUM(NgQty) AS 불량수량,
				SUM(DATEDIFF(MINUTE, StartDate, EndDate)) AS RunTime
			FROM PM_OrderResult
			WHERE CorpId = '1000'
			  AND CAST(WorkDate AS DATE) BETWEEN '{0}' AND '{1}'
			  AND StartDate IS NOT NULL
			  AND EndDate IS NOT NULL
			GROUP BY CAST(WorkDate AS DATE)
		),
		StopBase AS (
			SELECT
				CAST(WorkDay AS DATE) AS Dt,
				SUM(StopTime) AS StopTime
			FROM EQ_Operation
			WHERE CorpId = '1000'
			  AND OperationYn = 'N'
			  AND CAST(WorkDay AS DATE) BETWEEN '{0}' AND '{1}'
			GROUP BY CAST(WorkDay AS DATE)
		)
		SELECT
			P.Dt AS 일자,

			ISNULL(P.생산계획,0) AS 생산계획,

			ISNULL(R.생산실적,0) AS 생산실적,

			ROUND(
				ISNULL(R.생산실적,0) * 100.0
				/ NULLIF(P.생산계획,0)
			,1) AS 달성률,

			ISNULL(R.양품수량,0) AS 양품수량,

			ISNULL(R.불량수량,0) AS 불량수량,

			ROUND(
				ISNULL(R.불량수량,0) * 100.0
				/ NULLIF(R.생산실적,0)
			,1) AS 불량률,

			ROUND(
				ISNULL(R.RunTime,0) * 100.0
				/
				NULLIF(
					ISNULL(R.RunTime,0) + ISNULL(S.StopTime,0)
				,0)
			,1) AS 설비가동률

		FROM PlanBase P
		LEFT JOIN ResultBase R
			   ON P.Dt = R.Dt
		LEFT JOIN StopBase S
			   ON P.Dt = S.Dt

		ORDER BY P.Dt DESC
		";

		#endregion

		#region ── KPI 카드 ──────────────────────────────────────────────

		private void LoadKpiCards(DateTime dtStart, DateTime dtEnd)
		{
			string sql = string.Format(SQL_KPI,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));

			DataTable dt = db.GetDataTable(sql);
			if (dt == null || dt.Rows.Count == 0) return;

			DataRow r = dt.Rows[0];

			// ── 생산량
			decimal workQty = r["WorkQty_Total"].ConvertDecimal_Null_To_Zero();
			decimal workQty_PreD = r["WorkQty_PreD"].ConvertDecimal_Null_To_Zero();
			decimal workQty_PreW = r["WorkQty_PreW"].ConvertDecimal_Null_To_Zero();
			Lbl1_WorkQty.Text = workQty == 0 ? "" : workQty.ToString("N0");
			Lbl1_WorkQty_PreD.Text = workQty_PreD == 0 ? "" : workQty_PreD.ToString("N0");
			Lbl1_WorkQty_PreW.Text = workQty_PreW == 0 ? "" : workQty_PreW.ToString("N0");
			SetDiffLabel(Lbl1_WorkQty_PreD_Per, workQty, workQty_PreD);
			SetDiffLabel(Lbl1_WorkQty_PreW_Per, workQty, workQty_PreW);

			// ── 양품량
			decimal goodQty = r["GoodQty_Total"].ConvertDecimal_Null_To_Zero();
			decimal goodQty_PreD = r["GoodQty_PreD"].ConvertDecimal_Null_To_Zero();
			decimal goodQty_PreW = r["GoodQty_PreW"].ConvertDecimal_Null_To_Zero();

			Lbl2_GoodQty.Text = goodQty == 0 ? "" : goodQty.ToString("N0");
			Lbl2_GoodQty_PreD.Text = goodQty_PreD == 0 ? "" : goodQty_PreD.ToString("N0");
			Lbl2_GoodQty_PreW.Text = goodQty_PreW == 0 ? "" : goodQty_PreW.ToString("N0");
			SetDiffLabel(Lbl2_GoodQty_PreD_Per, goodQty, goodQty_PreD);
			SetDiffLabel(Lbl2_GoodQty_PreW_Per, goodQty, goodQty_PreW);

			// ── 불량량
			decimal ngQty = r["NgQty_Total"].ConvertDecimal_Null_To_Zero();
			decimal ngQty_PreD = r["NgQty_PreD"].ConvertDecimal_Null_To_Zero();
			decimal ngQty_PreW = r["NgQty_PreW"].ConvertDecimal_Null_To_Zero();
			Lbl3_NgQty.Text = ngQty == 0 ? "" : ngQty.ToString("N0");
			Lbl3_NgQty_PreD.Text = ngQty_PreD== 0 ? "" : ngQty_PreD.ToString("N0");
			Lbl3_NgQty_PreW.Text = ngQty_PreW == 0 ? "" : ngQty_PreW.ToString("N0");
			SetDiffLabel(Lbl3_NgQty_PreD_Per, ngQty, ngQty_PreD);
			SetDiffLabel(Lbl3_NgQty_PreW_Per, ngQty, ngQty_PreW);

			// ── 불량률
			decimal ngPer	  = r["NgPer_Total"].ConvertDecimal_Null_To_Zero();
			decimal ngPer_PreD = r["NgPer_PreD"].ConvertDecimal_Null_To_Zero();
			decimal ngPer_PreW = r["NgPer_PreW"].ConvertDecimal_Null_To_Zero();

			Lbl4_NgPer.Text = ngPer == 0 ? "" : ngPer.ToString("N0");
			Lbl4_NgPer_PreD.Text = ngPer_PreD== 0 ? "" : ngPer_PreD.ToString("N0");
			Lbl4_NgPer_PreW.Text = ngPer_PreW == 0 ? "" : ngPer_PreW.ToString("N0");
			SetDiffLabel(Lbl4_NgPer_PreD_Per, ngPer, ngPer_PreD);
			SetDiffLabel(Lbl4_NgPer_PreW_Per, ngPer, ngPer_PreW);

			// ── 시간당 생산량
			decimal uph		 = r["UPH_Total"].ConvertDecimal_Null_To_Zero();
			decimal uph_PreD = r["UPH_PreD"].ConvertDecimal_Null_To_Zero();
			decimal uph_PreW = r["UPH_PreW"].ConvertDecimal_Null_To_Zero();
			Lbl5_UPH.Text = uph == 0 ? "" : uph.ToString("N1");
			Lbl5_UPH_PreD.Text = uph_PreD == 0 ? "" : uph_PreD.ToString("N1");
			Lbl5_UPH_PreW.Text = uph_PreW == 0 ? "" : uph_PreW.ToString("N1");
			// 리드타임은 낮을수록 좋음 → 색 반전
			SetDiffLabel(Lbl5_UPH_PreD_Per, uph, uph_PreD, reverseColor: true);
			SetDiffLabel(Lbl5_UPH_PreW_Per, uph, uph_PreW, reverseColor: true);

			// ── 설비정지시간
			decimal stopTime	  = r["StopTime_Total"].ConvertDecimal_Null_To_Zero();
			decimal stopTime_PreD = r["StopTime_PreD"].ConvertDecimal_Null_To_Zero();
			decimal stopTime_PreW = r["StopTime_PreW"].ConvertDecimal_Null_To_Zero();
			Lbl6_StopTime.Text = stopTime == 0 ? "" : stopTime.ToString("N1");
			Lbl6_StopTime_PreD.Text = stopTime_PreD == 0 ? "" : stopTime_PreD.ToString("N1");
			Lbl6_StopTime_PreW.Text = stopTime_PreW == 0 ? "" :stopTime_PreW.ToString("N1");
			SetDiffLabel(Lbl6_StopTime_PreD_Per, stopTime, stopTime_PreD);
			SetDiffLabel(Lbl6_StopTime_PreW_per, stopTime, stopTime_PreW);
		}

		/// <summary>
		/// 전일/전주 대비 증감률 표시. reverseColor=true 이면 증가가 파랑(나쁨), 감소가 빨강(나쁨→아니, 감소가 좋음→파랑)
		/// </summary>
		private void SetDiffLabel(DevExpress.XtraEditors.LabelControl lbl,
								  decimal current, decimal prev, bool reverseColor = false)
		{
			if (prev == 0)
			{
				lbl.Text = "-";
				lbl.Appearance.ForeColor = Color.Gray;
				return;
			}

			decimal diff = (current - prev) / Math.Abs(prev) * 100;
			string arrow = diff >= 0 ? "▲" : "▼";
			lbl.Text = $"{arrow} {Math.Abs(diff):N2}%";

			bool isUp = diff >= 0;
			// 기본: 증가=빨강, 감소=파랑 / 리드타임처럼 반전: 증가=파랑, 감소=빨강
			Color upColor = reverseColor ? Color.Blue : Color.Red;
			Color downColor = reverseColor ? Color.Red : Color.Blue;
			lbl.Appearance.ForeColor = isUp ? upColor : downColor;
		}

		#endregion

		#region ── 추이 차트 초기화 ─────────────────────────────────────

		private void InitializeCharts()
		{
			SetupLineChart(ChartWorkQty, "생산수량", "양품수량", Color.DodgerBlue, Color.LimeGreen, false , "{V:#,##0}");
			SetupLineChart(chartControl3, "불량수량", "불량률(%)", Color.DodgerBlue, Color.LimeGreen, false , "{V:#,##0}");
			SetupLineChart(chartControl1, "시간당 생산량", "평균 작업시간(분)", Color.Orange, Color.MediumPurple , false , "{V:#,##0.0}");
			SetupLineChart(chartControl2, "정지시간(분)", null, Color.Gold, Color.Transparent, true, "{V:#,##0.0}");
		}

		private void SetupLineChart(DevExpress.XtraCharts.ChartControl chart,
									 string series1Name, string series2Name,
									 Color color1, Color color2,
									 bool singleSeries
									, string format)
		{
		
			chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
			chart.Series.Clear();

			// Series 1
			var s1 = new Series(series1Name, ViewType.Line);
			((LineSeriesView)s1.View).Color = color1;
			((LineSeriesView)s1.View).MarkerVisibility = DefaultBoolean.True;
			((LineSeriesView)s1.View).LineMarkerOptions.Size = 6;
			s1.ArgumentDataMember = "Dt";
			//s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True; // 라벨 보이도록 설정
			s1.Label.TextPattern = format;
			s1.CrosshairLabelPattern = "{A:MM-dd} : " + format;
			chart.Series.Add(s1);

			if (!singleSeries && !string.IsNullOrEmpty(series2Name))
			{
				var s2 = new Series(series2Name, ViewType.Line);
				((LineSeriesView)s2.View).Color = color2;
				((LineSeriesView)s2.View).LineMarkerOptions.Size = 6;
				//((LineSeriesView)s2.View).MarkerVisibility = DefaultBoolean.True;
				s2.ArgumentDataMember = "Dt";
				s2.Label.TextPattern = format; 
				s2.CrosshairLabelPattern = "{A:MM-dd} : " +format;

				chart.Series.Add(s2);
			}

			var diagram = (XYDiagram)chart.Diagram;
			//diagram.AxisX.Label.Angle = -45;
			diagram.AxisX.Label.Font = new Font("Pretendard", 8f);
			diagram.AxisX.Label.TextPattern = "{0:MM-dd}";
			diagram.AxisX.GridLines.Visible = false;

			diagram.AxisY.Label.Font = new Font("Pretendard", 8f);
			diagram.AxisY.Label.TextPattern = format;
			//diagram.AxisY.GridLines.Visible = false;

			diagram.DefaultPane.BackColor = Color.Transparent;
			//diagram.DefaultPane.BorderVisible = false;

			// 범례 (시리즈 이름 직접 표시)
			Legend legend = chart.Legend;

			if (legend != null)
			{
				legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
				legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
				legend.Direction = LegendDirection.LeftToRight;
				legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
				legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
				legend.BackColor = System.Drawing.Color.Transparent;
			}
		}

		private void SetupBarChart(ChartControl chart, string name, Color color, string argument, string value)
		{
			string format = "{V:#,##0}";

			chart.Series.Clear();
			chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

			Series series = new Series(name, ViewType.Bar);
			series.ArgumentDataMember = argument;
			series.ValueDataMembers.AddRange(value);
			series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
			series.Label.TextPattern = format;
			series.CrosshairLabelPattern = "{A} : {V:#,##0}";

			chart.Series.Add(series);

			BarSeriesView barView = (BarSeriesView)series.View;
			//barView.ColorEach = true;
			barView.BarWidth = 0.5;
			barView.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
			barView.Color = color;

			BarSeriesLabel label = (BarSeriesLabel)series.Label;
			label.TextPattern = format;
			label.Position = BarSeriesLabelPosition.Auto;
			label.TextOrientation = TextOrientation.Horizontal;

			//label.TextColor = Color.Black;
			label.BackColor = Color.Transparent;
			label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

			XYDiagram diagram = (XYDiagram)chart.Diagram;
			diagram.Rotated = true;
			diagram.DefaultPane.BackColor = Color.Transparent;
			diagram.DefaultPane.BorderVisible = false;

			diagram.AxisX.Label.Font = new Font("Pretendard", 8f);
			diagram.AxisX.Tickmarks.Visible = false;
			diagram.AxisX.Tickmarks.MinorVisible = false;
			diagram.AxisX.Reverse = true;

			diagram.AxisX.Visible = true;
			diagram.AxisX.Label.Visible = true; // 거래처명 라벨 강제 표시
			diagram.AxisX.Label.TextAlignment = StringAlignment.Near; // 거래처명 라벨 강제 표시

			diagram.AxisY.GridLines.Visible = false;
			diagram.AxisY.Label.Visible = false;
			diagram.AxisY.Label.TextPattern = format;
			diagram.AxisY.Label.Font = new Font("Pretendard", 8f);
		}

		#endregion

		#region ── 추이 차트 데이터 바인딩 ─────────────────────────────

		private void LoadTrendCharts(DateTime dtStart, DateTime dtEnd)
		{
			string sql = string.Format(SQL_TREND,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));

			DataTable dt = db.GetDataTable(sql);
			if (dt == null) return;

			// 생산량 추이
			BindLineChart(ChartWorkQty, dt, "Dt", "workQty", "GoodQty");
			// 불량률 추이
			BindLineChart(chartControl3, dt, "Dt", "NgQty", "NgPer");
			// 시간당 생산량 추이
			BindLineChart(chartControl1, dt, "Dt", "UPH", "ProdLT");
			// 설비정지시간 추이 
			BindLineChartSingle(chartControl2, dt, "Dt", "StopTime");
		}

		private void BindLineChart(DevExpress.XtraCharts.ChartControl chart,
									DataTable dt,
									string argField, string val1Field, string val2Field)
		{
			chart.DataSource = dt;
			chart.Series[0].ArgumentDataMember = argField;
			chart.Series[0].ValueDataMembers.AddRange(new string[] { val1Field });

			if (chart.Series.Count > 1)
			{
				chart.Series[1].ArgumentDataMember = argField;
				chart.Series[1].ValueDataMembers.AddRange(new string[] { val2Field });
			}
		}

		private void BindLineChartSingle(DevExpress.XtraCharts.ChartControl chart,
										  DataTable dt, string argField, string valField)
		{
			chart.DataSource = dt;
			chart.Series[0].ArgumentDataMember = argField;
			chart.Series[0].ValueDataMembers.AddRange(new string[] { valField });
		}

		#endregion

		#region ── 상세 그리드 초기화 / 바인딩 ─────────────────────────

		private void InitializeGrid()
		{
			GridView gv = GvList;

			gv.OptionsBehavior.Editable = false;
			gv.OptionsView.ShowGroupPanel = false;
			gv.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 9f);
			gv.Appearance.Row.Font = new Font("Pretendard", 9f);
			gv.Appearance.FooterPanel.Font = new Font("Pretendard SemiBold", 9f);
			gv.OptionsView.ShowFooter = true;

			//
			grid.Create_GvColumn(gv, "일자", "일자");
			grid.Create_GvColumn(gv, "생산계획", "생산계획");
			grid.Create_GvColumn(gv, "생산실적", "생산실적");
			grid.Create_GvColumn(gv, "달성률", "달성률");
			grid.Create_GvColumn(gv, "양품수량", "양품수량");
			grid.Create_GvColumn(gv, "불량수량", "불량수량");
			grid.Create_GvColumn(gv, "불량률", "불량률");
			grid.Create_GvColumn(gv, "설비가동률", "설비가동률");

			//
			grid.SetCol_Type(gv.Columns["생산계획"], clsGrid.TypeList.TInt);
			grid.SetCol_Type(gv.Columns["생산실적"], clsGrid.TypeList.TInt);
			grid.SetCol_Type(gv.Columns["달성률"], clsGrid.TypeList.TFloatN1);
			grid.SetCol_Type(gv.Columns["양품수량"], clsGrid.TypeList.TInt);
			grid.SetCol_Type(gv.Columns["불량수량"], clsGrid.TypeList.TInt);
			grid.SetCol_Type(gv.Columns["불량률"], clsGrid.TypeList.TFloatN1);
			grid.SetCol_Type(gv.Columns["설비가동률"], clsGrid.TypeList.TFloatN1);

			//
			grid.SetCol_Summary(gv, "생산계획");
			grid.SetCol_Summary(gv, "생산실적");
			grid.SetCol_Summary(gv, "양품수량");
			grid.SetCol_Summary(gv, "불량수량");
			grid.SetCol_Summary(gv, "달성률", DevExpress.Data.SummaryItemType.Average, "{0:N1}");
			grid.SetCol_Summary(gv, "불량률", DevExpress.Data.SummaryItemType.Average, "{0:N1}");
			grid.SetCol_Summary(gv, "설비가동률",DevExpress.Data.SummaryItemType.Average, "{0:N1}");

			//
			gv.RowCellStyle += Gv_RowCellStyle;
		}

		private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			if (e.Column.FieldName != "달성률" && e.Column.FieldName != "설비가동률") return;

			if (e.CellValue == null || e.CellValue == DBNull.Value) return;

			double val = Convert.ToDouble(e.CellValue);

			e.Appearance.ForeColor = val >= 95 ? Color.LimeGreen
								   : val >= 80 ? Color.Orange
											   : Color.Red;
		}

		private void LoadDetailGrid(DateTime dtStart, DateTime dtEnd)
		{
			string sql = string.Format(SQL_DETAIL,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));

			DataTable dt = db.GetDataTable(sql);
			GcList.DataSource = dt;
		}

		#endregion

		#region ── 거래처별 수주 Bar 차트 ───────────────────────────────

		private void LoadCustomerBarChart(DateTime dtStart, DateTime dtEnd)
		{
			// 디자이너 코드에는 없는 차트이므로, 런타임에 동적으로 생성하거나
			// 별도 컨트롤(chartControl_Customer)을 디자이너에 추가 후 연결 필요.
			// 여기서는 chartControl2가 납기준수율이므로 별도 처리.
			// → 실제 화면처럼 하려면 디자이너에 ChartControl 2개 더 추가 권장.
			// 이 메서드는 해당 차트 컨트롤 연결 후 아래 로직 사용.

			/*
			string sql = string.Format(SQL_CUSTOMER,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));
			DataTable dt = db.GetDataTable(sql);
			if (dt == null) return;

			chartCustomer.Series.Clear();
			var s = new Series("수주금액", ViewType.Bar);
			s.ArgumentDataMember = "거래처명";
			s.ValueDataMembersNames = "수주금액";
			((BarSeriesView)s.View).Color = Color.DodgerBlue;
			chartCustomer.Series.Add(s);
			chartCustomer.DataSource = dt;
			var diag = (XYDiagram)chartCustomer.Diagram;
			diag.Rotated = true; // 수평 막대
			*/
		}

		#endregion

		#region ── 품목별 수주/출하 Bar 차트 ────────────────────────────

		private void LoadMaterialBarChart(DateTime dtStart, DateTime dtEnd)
		{
			/*
			string sql = string.Format(SQL_MATERIAL,
				dtStart.ToString("yyyy-MM-dd"),
				dtEnd.ToString("yyyy-MM-dd"));
			DataTable dt = db.GetDataTable(sql);
			if (dt == null) return;

			chartMaterial.Series.Clear();

			var s1 = new Series("수주금액", ViewType.Bar);
			s1.ArgumentDataMember = "품목명";
			s1.ValueDataMembersNames = "수주금액";
			((BarSeriesView)s1.View).Color = Color.DodgerBlue;

			var s2 = new Series("출하금액", ViewType.Bar);
			s2.ArgumentDataMember = "품목명";
			s2.ValueDataMembersNames = "출하금액";
			((BarSeriesView)s2.View).Color = Color.LimeGreen;

			chartMaterial.Series.Add(s1);
			chartMaterial.Series.Add(s2);
			chartMaterial.DataSource = dt;
			var diag = (XYDiagram)chartMaterial.Diagram;
			diag.Rotated = true;
			*/
		}

        #endregion

        #region Event

        private void ExcelDownload()
		{
			using (var dlg = new SaveFileDialog())
			{
				dlg.Filter = "Excel Files (*.xlsx)|*.xlsx";
				dlg.FileName = $"백데이터_생산_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
				if (dlg.ShowDialog() != DialogResult.OK) return;

				try
				{
					GvList.ExportToXlsx(dlg.FileName);
					DevExpress.XtraEditors.XtraMessageBox.Show(
						"엑셀 파일이 저장되었습니다.", "완료",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					DevExpress.XtraEditors.XtraMessageBox.Show(
						$"저장 실패: {ex.Message}", "오류",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btn_Search_Click(object sender, EventArgs e)
		{
			Search();
		}

        private void Dt_Date_EditValueChanged(object sender, EventArgs e)
        {
			Search();
		}

        #endregion
    }
}