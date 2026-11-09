using DevExpress.CodeParser;
using DevExpress.DashboardCommon;
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
    public partial class Frm_AN_Machine_ProductView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        public Frm_AN_Machine_ProductView()
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

            Search();

            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)
        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "구분", "MachineClass");
            grid.Create_GvColumn(gv, "금월생산량", "CurrQty");
            grid.Create_GvColumn(gv, "전월생산량", "PrevQty");
            grid.Create_GvColumn(gv, "전월대비(%)", "PrevCompareRatio");
            grid.Create_GvColumn(gv, "누계생산량", "YearQty");
            grid.Create_GvColumn(gv, "금월가동시간(h)", "CurrRunTimeHours");
            grid.Create_GvColumn(gv, "전월가동시간(h)", "PrevRunTimeHours");
            grid.Create_GvColumn(gv, "가동률(%)", "OperationRate");
            grid.Create_GvColumn(gv, "기준생산량", "TargetQty");
            grid.Create_GvColumn(gv, "달성률(%)", "AchievementRate");
            //grid.Create_GvColumn(gv, "비고", "Remark");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["ActiveCount"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["CurrQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["PrevQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["PrevCompareRatio"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["YearQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["CurrRunTimeHours"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["PrevRunTimeHours"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OperationRate"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TargetQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["AchievementRate"], clsGrid.TypeList.TFloat);

            gv.Columns["MachineClass"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gv.Columns["Remark"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
            gv.CustomDrawCell += gridView1_CustomDrawCell;
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Remark")
            {
                string value = e.CellValue.ConvertString_Null_To_Empty();

                if (value == "지연")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 243, 205); // 연한 노란색
                    e.Appearance.ForeColor = Color.FromArgb(180, 120, 0);   // 진한 주황/갈색
                }
                else if (value == "정상")
                {
                    e.Appearance.BackColor = Color.FromArgb(220, 245, 230); // 연한 초록색
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
                }
            }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "AchievementRate" || e.Column.FieldName == "OperationRate")
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
        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            string startDt = barDate.smtStartDateTimeValue.ToString("yyyy-MM-01");
            string endDt = Convert.ToDateTime(startDt).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            SearchCardData(startDt, endDt);          // 상단 카드
            Search_Chart_Day(startDt, endDt);       // 설비별 생산량 TOP 10
            Search_Chart_AcRate(startDt, endDt);    // 설비별 가동률 현황
            Search_Chart_Line_Qty(startDt, endDt);  // 설비별 생산량 추이
            SearchGrid_Work(startDt, endDt);        // 설비별 상세 현황 그리드
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData(string startDt, string endDt)
        {
            query = $@"
DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
DECLARE @StartDate DATE = '{startDt}';
DECLARE @EndDate DATE = '{endDt}';

DECLARE @PrevStartDate DATE = DATEADD(MONTH, -1, @StartDate);
DECLARE @PrevEndDate DATE = EOMONTH(@PrevStartDate);

WITH CurrentResult AS (
    SELECT 
        ISNULL(SUM(WorkQty), 0) AS TotalQty,
        ISNULL(SUM(GoodQty), 0) AS GoodQty,
        ISNULL(SUM(DATEDIFF(MINUTE, StartDate, EndDate)) / 60.0, 0) AS TotalProdHours,
        COUNT(DISTINCT MachineCode) AS ActiveMachines
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate BETWEEN @StartDate AND @EndDate
),
PrevResult AS (
    SELECT 
        ISNULL(SUM(WorkQty), 0) AS TotalQty,
        ISNULL(SUM(GoodQty), 0) AS GoodQty
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate BETWEEN @PrevStartDate AND @PrevEndDate
),
StopStats AS (
    SELECT 
        ISNULL(SUM(DATEDIFF(MINUTE, StartTime, EndTime)) / 60.0, 0) AS TotalStopTimeHours
    FROM EQ_Operation
    WHERE CorpId = @CorpId
      AND WorkDay BETWEEN @StartDate AND @EndDate
),
MachineStats AS (
    SELECT COUNT(*) AS TotalMachines
    FROM EQ_Machine
    WHERE CorpId = @CorpId
)
SELECT 
    c.TotalQty AS TotalQty,
    c.GoodQty AS GoodQty,
    CASE 
        WHEN p.TotalQty = 0 THEN 0.0
        ELSE ROUND(((c.TotalQty - p.TotalQty) / CAST(p.TotalQty AS FLOAT)) * 100, 1)
    END AS ComparePrevMonthRatio,
    CASE 
        WHEN (c.TotalProdHours + s.TotalStopTimeHours) = 0 THEN 0.0
        ELSE ROUND((c.TotalProdHours / (c.TotalProdHours + s.TotalStopTimeHours)) * 100, 1)
    END AS AvgOperationRate,
    ROUND(c.TotalProdHours, 1) AS TotalRunTimeHours,
    c.ActiveMachines,
    m.TotalMachines
FROM CurrentResult c
CROSS JOIN PrevResult p
CROSS JOIN StopStats s
CROSS JOIN MachineStats m;
";
            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                float workQty = dr["TotalQty"].ConvertFloat_Null_To_Zero();
                float goodQty = dr["GoodQty"].ConvertFloat_Null_To_Zero();

                LblWorkQty.Text = workQty.ToString("N0") + " EA";
                LblGoodQty.Text = goodQty.ToString("N0") + " EA";
                LblAchieveRate.Text = dr["ComparePrevMonthRatio"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblOpTime.Text = dr["TotalRunTimeHours"].ConvertFloat_Null_To_Zero().ToString("N1") + " 시간";
                LblCnt.Text = dr["ActiveMachines"].ConvertInt_Null_To_Zero() + " / " + dr["TotalMachines"].ConvertInt_Null_To_Zero() + " 대";
            }
        }
        #endregion

        #region 차트 로드 
        private void Search_Chart_Day(string startDt, string endDt)
        {
            // 설비별 생산량 TOP 10 차트
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
, @StartDate DATE = '{startDt}'
, @EndDate DATE = '{endDt}';

SELECT TOP 10
    ISNULL(m.MachineCode, r.MachineCode) AS MachineCode,
    ISNULL(m.MachineName, r.MachineCode) AS MachineName,
    ISNULL(SUM(r.GoodQty), 0) AS TotalQty
FROM PM_OrderResult r
LEFT JOIN EQ_Machine m 
       ON r.CorpId = m.CorpId 
      AND r.MachineCode = m.MachineCode
WHERE r.CorpId = @CorpId
  AND r.WorkDate BETWEEN @StartDate AND @EndDate
GROUP BY 
    ISNULL(m.MachineCode, r.MachineCode),
    ISNULL(m.MachineName, r.MachineCode)
ORDER BY TotalQty DESC;
";

            DataTable dt = db.GetDataTable(query);

            ChartDay.Series.Clear();
            ChartDay.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series s1 = new Series("생산량 TOP 10", ViewType.Bar);
            s1.ArgumentDataMember = "MachineName";
            s1.ValueDataMembers.AddRange(new string[] { "TotalQty" });
            s1.DataSource = dt;
            s1.CrosshairLabelPattern = "{S}: {V:#,##0} EA";
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            ChartDay.Series.Add(s1);

            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(51, 102, 255);
            barView.BarWidth = 0.4;

            s1.Label.TextPattern = "{V:#,##0}";
            //BarSeriesLabel barLabel = (BarSeriesLabel)s1.Label;
            //barLabel.Position = BarSeriesLabelPosition.Top;
            //barLabel.BackColor = Color.Transparent;
            //barLabel.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //barLabel.Font = new Font(this.Font.FontFamily, 8, FontStyle.Bold);
            //barLabel.TextColor = Color.FromArgb(70, 80, 95);
        }

        private void Search_Chart_AcRate(string startDt, string endDt)
        {
            // 설비별 가동률 차트
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
, @StartDate DATE = '{startDt}'
, @EndDate DATE = '{endDt}';

WITH ProdTime AS (
    SELECT 
        MachineCode,
        SUM(DATEDIFF(MINUTE, StartDate, EndDate)) AS ProdMinutes
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate BETWEEN @StartDate AND @EndDate
    GROUP BY MachineCode
),
StopTime AS (
    SELECT 
        MachineCode,
        SUM(DATEDIFF(MINUTE, StartTime, EndTime)) AS StopMinutes
    FROM EQ_Operation
    WHERE CorpId = @CorpId
      AND WorkDay BETWEEN @StartDate AND @EndDate
    GROUP BY MachineCode
)
SELECT TOP 10 
    m.MachineCode,
    m.MachineName,
    CASE 
        WHEN (ISNULL(p.ProdMinutes, 0) + ISNULL(s.StopMinutes, 0)) = 0 THEN 0.0
        ELSE ROUND((CAST(ISNULL(p.ProdMinutes, 0) AS FLOAT) / (ISNULL(p.ProdMinutes, 0) + ISNULL(s.StopMinutes, 0))) * 100, 1)
    END AS OperationRate
FROM EQ_Machine m
LEFT JOIN ProdTime p ON m.MachineCode = p.MachineCode
LEFT JOIN StopTime s ON m.MachineCode = s.MachineCode
WHERE m.CorpId = @CorpId
ORDER BY OperationRate DESC;
";

            DataTable dt = db.GetDataTable(query);

            // 가동률 차트 컨트롤 (폼 디자인 상의 컨트롤명으로 맞춤)
            ChartControl chart = chartControl1;

            chart.Series.Clear();
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series s1 = new Series("가동률", ViewType.Bar);
            s1.ArgumentDataMember = "MachineName";
            s1.ValueDataMembers.AddRange(new string[] { "OperationRate" });
            s1.DataSource = dt;
            s1.CrosshairLabelPattern = "{A}: {V:0.0}%";
            //s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            chart.Series.Add(s1);

            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(82, 196, 143);
            barView.BarWidth = 0.4;

            s1.Label.TextPattern = "{A}: {V:0.0}%";
            //BarSeriesLabel barLabel = (BarSeriesLabel)s1.Label;
            //barLabel.Position = BarSeriesLabelPosition.Top;
            //barLabel.BackColor = Color.Transparent;
            //barLabel.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //barLabel.Font = new Font(this.Font.FontFamily, 8, FontStyle.Bold);
            //barLabel.TextColor = Color.FromArgb(70, 80, 95);
        }

        private void Search_Chart_Line_Qty(string startDt, string endDt)
        {
            // 설비별 생산량 추이 (일별 Line 차트)
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
, @StartDate DATE = '{startDt}'
, @EndDate DATE = '{endDt}';

SELECT 
    CONVERT(VARCHAR(10), r.WorkDate, 120) AS WorkDate,
    ISNULL(m.MachineName, r.MachineCode) AS MachineName,
    ISNULL(SUM(r.GoodQty), 0) AS DailyQty
FROM PM_OrderResult r
LEFT JOIN EQ_Machine m 
       ON r.CorpId = m.CorpId 
      AND r.MachineCode = m.MachineCode
WHERE r.CorpId = @CorpId
  AND r.WorkDate BETWEEN @StartDate AND @EndDate
GROUP BY 
    CONVERT(VARCHAR(10), r.WorkDate, 120),
    ISNULL(m.MachineName, r.MachineCode),
    r.MachineCode
ORDER BY WorkDate ASC, r.MachineCode ASC;
";

            DataTable dt = db.GetDataTable(query);

            ChartControl chart = ChartLine; // 추이 차트 컨트롤명 매핑

            chart.Series.Clear();
            chart.SeriesTemplate.ChangeView(ViewType.Line);
            chart.SeriesDataMember = "MachineName";
            chart.SeriesTemplate.ArgumentDataMember = "WorkDate";
            chart.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "DailyQty" });
            chart.DataSource = dt;

            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
        }
        #endregion

        #region 조회 쿼리 (그리드)
        private void SearchGrid_Work(string startDt, string endDt)
        {
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
, @StartDate DATE = '{startDt}'
, @EndDate DATE = '{endDt}';

DECLARE @PrevStartDate DATE = DATEADD(MONTH, -1, @StartDate);
DECLARE @PrevEndDate DATE = EOMONTH(@PrevStartDate);
DECLARE @YearStartDate DATE = DATEFROMPARTS(YEAR(@StartDate), 1, 1);

WITH MachineBase AS (
    SELECT 
        M.CorpId,
        MachineCode,
        MachineName,
        C.CLASSNAME AS MachineClass,
        1 AS ActiveCount
    FROM EQ_Machine M
	LEFT JOIN CM_CLASS C ON M.CorpId = C.CorpId AND C.HIGHCLASS = '2010' AND M.MGroup = C.CLASS
    WHERE M.CorpId = @CorpId
),
CurrentResult AS (
    SELECT 
        MachineCode,
        SUM(GoodQty) AS CurrQty,
        SUM(WorkQty) AS TargetQty,
        SUM(DATEDIFF(MINUTE, StartDate, EndDate)) AS CurrProdMinutes
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate BETWEEN @StartDate AND @EndDate
    GROUP BY MachineCode
),
PrevResult AS (
    SELECT 
        MachineCode,
        SUM(GoodQty) AS PrevQty,
        SUM(DATEDIFF(MINUTE, StartDate, EndDate)) AS PrevProdMinutes
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate BETWEEN @PrevStartDate AND @PrevEndDate
    GROUP BY MachineCode
),
YearResult AS (
    SELECT 
        MachineCode,
        SUM(GoodQty) AS YearQty
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND WorkDate BETWEEN @YearStartDate AND @EndDate
    GROUP BY MachineCode
),
CurrentStop AS (
    SELECT 
        MachineCode,
        SUM(DATEDIFF(MINUTE, StartTime, EndTime)) AS CurrStopMinutes
    FROM EQ_Operation
    WHERE CorpId = @CorpId
      AND WorkDay BETWEEN @StartDate AND @EndDate
    GROUP BY MachineCode
)
SELECT 
    ROW_NUMBER() OVER(ORDER BY m.MachineCode ASC) AS Seq,
    m.MachineCode,
    m.MachineName,
    m.MachineClass,
    m.ActiveCount,
    ISNULL(c.CurrQty, 0) AS CurrQty,
    ISNULL(p.PrevQty, 0) AS PrevQty,
    CASE 
        WHEN ISNULL(p.PrevQty, 0) = 0 THEN 0.0
        ELSE ROUND(((ISNULL(c.CurrQty, 0) - p.PrevQty) / CAST(p.PrevQty AS FLOAT)) * 100, 1)
    END AS PrevCompareRatio,
    ISNULL(y.YearQty, 0) AS YearQty,
    ROUND(ISNULL(c.CurrProdMinutes, 0) / 60.0, 1) AS CurrRunTimeHours,
    ROUND(ISNULL(p.PrevProdMinutes, 0) / 60.0, 1) AS PrevRunTimeHours,
    CASE 
        WHEN (ISNULL(c.CurrProdMinutes, 0) + ISNULL(cs.CurrStopMinutes, 0)) = 0 THEN 0.0
        ELSE ROUND((CAST(ISNULL(c.CurrProdMinutes, 0) AS FLOAT) / (c.CurrProdMinutes + cs.CurrStopMinutes)) * 100, 1)
    END AS OperationRate,
    ISNULL(c.TargetQty, 0) AS TargetQty,
    CASE 
        WHEN ISNULL(c.TargetQty, 0) = 0 THEN 0.0
        ELSE ROUND((CAST(ISNULL(c.CurrQty, 0) AS FLOAT) / c.TargetQty) * 100, 1)
    END AS AchievementRate,
    CASE 
        WHEN c.TargetQty IS NULL OR c.TargetQty = 0 THEN '정상'
        WHEN (CAST(ISNULL(c.CurrQty, 0) AS FLOAT) / c.TargetQty) * 100 >= 90.0 THEN '정상'
        ELSE '지연'
    END AS Remark
FROM MachineBase m
LEFT JOIN CurrentResult c ON m.MachineCode = c.MachineCode
LEFT JOIN PrevResult p ON m.MachineCode = p.MachineCode
LEFT JOIN YearResult y ON m.MachineCode = y.MachineCode
LEFT JOIN CurrentStop cs ON m.MachineCode = cs.MachineCode
ORDER BY m.MachineCode ASC;

";

            Gc_Work.DataSource = db.GetDataTable(query);
            Gv_Work.BestFitColumns();
        }
        #endregion

        #region Event
        private void timer1_Tick(object sender, EventArgs e)
        {
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