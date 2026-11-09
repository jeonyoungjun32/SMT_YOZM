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
using SmartMES.View.SM;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SmartMES.View.Management
{
    public partial class Frm_MM_Dashboard_Total1 : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        DateTime selectedSalesMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

        int searchInterval = 60000;

        public Frm_MM_Dashboard_Total1()
        {
            InitializeComponent();

            // 상단 조회조건 색상 설정
            smtCommonBar1.smtBackColor = Color.Transparent;
            btn_Colose.ForeColor = Color.Black;
        }

        private void Frm_AN_DefectType_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            SetGrid_Work(Gv_Work);
            SetGrid_Machine(GvMachine);

            Search();

            Tm_Search.Interval = searchInterval;
            LblRemainSec.Text = (searchInterval / 1000).ToString("N0");

            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)
        private void SetGrid_Work(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;


            grid.Create_GvColumn(gv, "지시번호-순번", "OrderNo");
            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "설비", "MachineName");
            grid.Create_GvColumn(gv, "품명", "MaterialName");
            grid.Create_GvColumn(gv, "지시량", "OrderQty");
            grid.Create_GvColumn(gv, "생산시작일", "FirstStartDate");
            grid.Create_GvColumn(gv, "생산량", "WorkQty");
            grid.Create_GvColumn(gv, "진행률", "ProgressRate");
            grid.Create_GvColumn(gv, "상태", "OrderStatus");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ProgressRate"], clsGrid.TypeList.TFloatN1);

            gv.Columns["OrderNo"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["OrderStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["FirstStartDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
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
            if (e.Column.FieldName == "MachineStatus" || e.Column.FieldName == "OrderStatus") //
			{
				if (value == "가동중" || value == "진행중" )
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
            grid.Create_GvColumn(gv, "총가동(H)", "TodayRunHours");
            grid.Create_GvColumn(gv, "정지(H)", "TodayStopHours");
            grid.Create_GvColumn(gv, "실가동(H)", "TodayRealRunHours");
            //grid.Create_GvColumn(gv, "비고", "Remark");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["OperationRate"], clsGrid.TypeList.TFloatN1);
            grid.SetCol_Type(gv.Columns["TodayRunHours"], clsGrid.TypeList.TFloatN1);
            grid.SetCol_Type(gv.Columns["TodayStopHours"], clsGrid.TypeList.TFloatN1);
            grid.SetCol_Type(gv.Columns["TodayRealRunHours"], clsGrid.TypeList.TFloatN1);

            gv.Columns["MachineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gv.Columns["Remark"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
            gv.CustomDrawCell += gridView1_CustomDrawCell;
        }

		#endregion

		#region 데이터 조회 (Search)
		private void Search()
        {
            SearchCardData();          // 상단 카드
            Search_Chart_Sales_Month();       
            Search_Chart_Sales_Item(selectedSalesMonth);    
            Search_Chart_Sales_Cust(selectedSalesMonth);  
            SearchGrid_Work();
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
DECLARE @CorpId       VARCHAR(10) = '{WrGlobal.CorpID}'; -- 사업장코드



WITH TodayProduction AS (
    -- 1. 금일 생산량 및 불량수량 (PM_OrderResult)
    SELECT 
        ISNULL(SUM(GoodQty), 0) AS TodayGoodQty,
        ISNULL(SUM(NgQty), 0)   AS TodayNgQty,
        ISNULL(SUM(WorkQty), 0) AS TodayTotalQty
    FROM 
        PM_OrderResult
    WHERE 
        WorkDate = @Today
        AND (@CorpId IS NULL OR CorpId = @CorpId)
),
TodayPlan AS (
    -- 2. 오늘 생산계획수량 (PM_ProductPlan)
    SELECT 
        ISNULL(SUM(OrderQty), 0) AS TodayPlanQty
    FROM 
        PM_ProductPlan
    WHERE 
        WorkDate = @Today
        AND CorpId = @CorpId
),
EquipmentOperation AS (
    -- 3. 설비가동률 (실적 작업시간 / (실적 작업시간 + 비가동시간))
    SELECT 
        ISNULL(SUM(DATEDIFF(MINUTE, R.StartDate, R.EndDate)), 0) AS TotalWorkTimeMinute,
        ISNULL(SUM(O.StopTime), 0) AS TotalStopTimeMinute
    FROM 
        PM_OrderResult R
    LEFT JOIN 
        EQ_Operation O ON R.CorpId      = O.CorpId 
                      AND R.WorkDate    = O.WorkDay 
                      AND R.MachineCode = O.MachineCode
    WHERE 
        R.WorkDate = @Today
        AND R.CorpId = @CorpId
),
MonthSales AS (
    -- 4. 누계 매출액 (BM_SalesM, BM_SalesD)
    SELECT 
        ISNULL(SUM(D.TotalPrice), 0) AS TotalSalesAmt
    FROM 
        BM_SalesM M
    INNER JOIN 
        BM_SalesD D ON M.CorpId  = D.CorpId 
                   AND M.SalesNo = D.SalesNo
    WHERE 
        M.SalesDate BETWEEN @StartOfMonth AND @Today
        AND M.OutType = '10' --10:제품출하, 30:자재출고
        AND M.CorpId = @CorpId
)
SELECT 
    -- [금일 생산량]
    P.TodayGoodQty AS TodayProductionQty,
    
    -- [생산계획 대비 달성률 (%)]
    CASE 
        WHEN PLN.TodayPlanQty = 0 THEN 0.0
        ELSE ROUND((CAST(P.TodayGoodQty AS FLOAT) / PLN.TodayPlanQty) * 100, 1)
    END AS PlanAchievementRate,
    
    -- [불량률 (%)]
    CASE 
        WHEN P.TodayTotalQty = 0 THEN 0.0
        ELSE ROUND((CAST(P.TodayNgQty AS FLOAT) / P.TodayTotalQty) * 100, 2)
    END AS DefectRate,
    
    -- [설비가동률 (%)]
    CASE 
        WHEN (EQ.TotalWorkTimeMinute + EQ.TotalStopTimeMinute) = 0 THEN 0.0
        ELSE ROUND((CAST(EQ.TotalWorkTimeMinute AS FLOAT) / (EQ.TotalWorkTimeMinute + EQ.TotalStopTimeMinute)) * 100, 1)
    END AS EquipmentOperationRate,
    
    -- [누계 매출액]
    S.TotalSalesAmt AS CumulativeSalesAmount
FROM 
    TodayProduction P
CROSS JOIN TodayPlan PLN
CROSS JOIN EquipmentOperation EQ
CROSS JOIN MonthSales S;
";
            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                LblWorkQty.Text = dr["TodayProductionQty"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";
                LblAchieveRate.Text = dr["PlanAchievementRate"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblNgRate.Text = dr["DefectRate"].ConvertFloat_Null_To_Zero().ToString("N2") + " %";
                LblOpRate.Text = dr["EquipmentOperationRate"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblSalesAmt.Text = dr["CumulativeSalesAmount"].ConvertInt_Null_To_Zero().ToString("N0") + " 원";
            }
        }
        #endregion

        #region 차트 로드 
        private void Search_Chart_Sales_Month()
        {
            // 월별 매출 최근 6개월
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}';

-- 1. 최근 6개월의 월(YYYY-MM) 목록을 생성하는 재귀 CTE
WITH Months AS (
    SELECT 
        CONVERT(CHAR(7), DATEADD(month, -5, GETDATE()), 120) AS SalesYearMonth, -- 'YYYY-MM' 형태 생성
        DATEADD(month, -5, GETDATE()) AS MonthDate,
        1 AS N
    UNION ALL
    SELECT 
        CONVERT(CHAR(7), DATEADD(month, N, DATEADD(month, -5, GETDATE())), 120),
        DATEADD(month, N, DATEADD(month, -5, GETDATE())),
        N + 1
    FROM Months
    WHERE N < 6
),
-- 2. 해당 기간 동안의 월별 매출 집계
SalesData AS (
    SELECT 
        CONVERT(CHAR(7), M.SalesDate, 120) AS SalesYearMonth, -- 'YYYY-MM' 형태 생성
        SUM(D.TotalPrice) AS TotalSalesAmt
    FROM 
        BM_SalesM M
    INNER JOIN 
        BM_SalesD D ON M.CorpId  = D.CorpId 
                   AND M.SalesNo = D.SalesNo
    WHERE 
        M.CorpId = @CorpId
        AND M.SalesDate >= DATEADD(month, -5, DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0)) -- 5개월 전 1일부터
        AND M.SalesDate <  DATEADD(day, 1, CAST(GETDATE() AS DATE))
        AND M.OutType = '10'
    GROUP BY 
        CONVERT(CHAR(7), M.SalesDate, 120)
)
-- 3. 6개월 목록과 매출 데이터 LEFT JOIN (YYYY-MM 출력 및 실적 없는 달 0 처리)
SELECT 
    M.SalesYearMonth,
    ISNULL(S.TotalSalesAmt, 0) AS TotalSalesAmt
FROM 
    Months M
LEFT JOIN 
    SalesData S ON M.SalesYearMonth = S.SalesYearMonth
ORDER BY 
    M.MonthDate ASC;
";

            DataTable dt = db.GetDataTable(query);

            ChartSales_Month.DataSource = dt;

            ChartSales_Month.Series.Clear();
            ChartSales_Month.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_Month.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_Month.BackColor = Color.White;
            ChartSales_Month.RuntimeHitTesting = true;

            Series s1 = new Series("월별매출", ViewType.Bar);
            s1.ArgumentDataMember = "SalesYearMonth";
            s1.ValueDataMembers.AddRange(new string[] { "TotalSalesAmt" });
            s1.DataSource = dt;
            s1.CrosshairLabelPattern = "{A}월: {V:#,##0} 원";
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            ChartSales_Month.Series.Add(s1);

            XYDiagram diagram = ChartSales_Month.Diagram as XYDiagram;

            if (diagram != null)
            {
                // Y축 수치에 콤마 표기 (예: 10000 -> 10,000)
                diagram.AxisY.Label.TextPattern = "{V:N0}";
            }

            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(55, 91, 150);
            barView.BarWidth = 0.4;

            s1.Label.TextPattern = "{V:#,##0}";
            //BarSeriesLabel barLabel = (BarSeriesLabel)s1.Label;
            //barLabel.Position = BarSeriesLabelPosition.Top;
            //barLabel.BackColor = Color.Transparent;
            //barLabel.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            //barLabel.Font = new Font(this.Font.FontFamily, 8, FontStyle.Bold);
            //barLabel.TextColor = Color.FromArgb(70, 80, 95);
        }

        private void ChartSales_Month_MouseClick(object sender, MouseEventArgs e)
        {
            ChartControl chart = sender as ChartControl;

            if (chart == null)
            {
                return;
            }

            ChartHitInfo hitInfo = chart.CalcHitInfo(e.Location);

            if (hitInfo.SeriesPoint == null)
            {
                return;
            }

            DateTime salesMonth;

            if (!DateTime.TryParseExact(hitInfo.SeriesPoint.Argument.ToString(), "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out salesMonth))
            {
                return;
            }

            selectedSalesMonth = salesMonth;

            Search_Chart_Sales_Item(selectedSalesMonth);
            Search_Chart_Sales_Cust(selectedSalesMonth);
        }

        private void Search_Chart_Sales_Item(DateTime salesMonth)
        {
            Grp_Item.Text = $"{salesMonth.ToString("M월 제품별 매출 TOP 5")}";

            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}';
DECLARE @FirstDay date = CONVERT(date, '{salesMonth:yyyyMMdd}', 112);
DECLARE @NextMonthDay date = DATEADD(month, 1, @FirstDay);

SELECT TOP 5
    D.ItemCode,
    I.ItemName,
    SUM(D.TotalPrice) AS SalesAmt,
    SUM(D.Qty)        AS SalesQty
FROM 
    BM_SalesM M
INNER JOIN 
    BM_SalesD D ON M.CorpId  = D.CorpId 
               AND M.SalesNo = D.SalesNo
LEFT JOIN 
    CM_Item I ON D.CorpId   = I.CorpId 
              AND D.ItemCode = I.ItemCode
WHERE 
    M.CorpId = @CorpId
    AND M.SalesDate >= @FirstDay 
    AND M.SalesDate <  @NextMonthDay
    AND M.OutType = '10'
GROUP BY 
    D.ItemCode, I.ItemName
ORDER BY 
    SalesAmt DESC;
";


            DataTable dt = db.GetDataTable(query);

            // 가동률 차트 컨트롤 (폼 디자인 상의 컨트롤명으로 맞춤)
            ChartControl chart = ChartSales_Item;

            chart.Series.Clear();
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chart.BackColor = Color.White;

            Series s1 = new Series("제품별 매출", ViewType.Bar);
            s1.ArgumentDataMember = "ItemName";
            s1.ValueDataMembers.AddRange(new string[] { "SalesAmt" });
            s1.DataSource = dt;
            s1.CrosshairLabelPattern = "{A} : {V:#,#} 원";
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            chart.Series.Add(s1);

            XYDiagram diagram = ChartSales_Item.Diagram as XYDiagram;

            if (diagram != null)
            {
                diagram.Rotated = true; // X축과 Y축 축 방향 회전 (가로 막대)

                // [핵심 1] 1위 제품이 상단에 오도록 X축(제품명 축) 정렬 순서 반전
                diagram.AxisX.Reverse = true;

                // [핵심 2] 축 라벨 강제 표시 및 겹침 방지 해제
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;

                // [핵심 3] 숫자 축(가로축) 포맷 설정 및 가독성 확보
                diagram.AxisY.Label.TextPattern = "{V:N0}";
            }

            // 막대 디자인 설정
            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(55, 91, 150);// 오른쪽 이미지 스타일의 파란색 (원하는 색상으로 변경 가능)
            barView.BarWidth = 0.4;

            // [핵심 4] 막대 우측 라벨 포맷 및 디자인 (제품명 {A} 제거, 순수 금액만 표시)
            s1.Label.TextPattern = "{V:#,#}";

            BarSeriesLabel barLabel = (BarSeriesLabel)s1.Label;
            barLabel.Position = BarSeriesLabelPosition.Top; // 막대 끝(우측) 바깥쪽에 배치
            barLabel.BackColor = Color.Transparent;          // 배경 투명화 (네모 박스 제거)
            barLabel.Border.Visibility = DevExpress.Utils.DefaultBoolean.False; // 테두리 제거
                                                                                // barLabel.Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold);
            barLabel.TextColor = Color.FromArgb(70, 80, 95);
        }

        private void Search_Chart_Sales_Cust(DateTime salesMonth)
        {
            // 매출 거래처별 점유율
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}';
DECLARE @FirstDay date = CONVERT(date, '{salesMonth:yyyyMMdd}', 112);
DECLARE @NextMonthDay date = DATEADD(month, 1, @FirstDay);

WITH SalesByCust AS (
    SELECT 
        M.BillToCode AS CustCode,
        M.BillToName AS CustName,
        SUM(D.TotalPrice) AS CustSalesAmt
    FROM 
        BM_SalesM M
    INNER JOIN 
        BM_SalesD D ON M.CorpId  = D.CorpId 
                   AND M.SalesNo = D.SalesNo
    WHERE 
        M.CorpId = @CorpId
        AND M.SalesDate >= @FirstDay 
        AND M.SalesDate <  @NextMonthDay
        AND M.OutType = '10'
    GROUP BY 
        M.BillToCode, M.BillToName
),
TotalSales AS (
    SELECT ISNULL(SUM(CustSalesAmt), 0) AS TotalAmt FROM SalesByCust
)
SELECT TOP 5
    S.CustCode,
    S.CustName,
    S.CustSalesAmt,
    CASE 
        WHEN T.TotalAmt = 0 THEN 0.0
        ELSE ROUND((CAST(S.CustSalesAmt AS FLOAT) / T.TotalAmt) * 100, 1)
    END AS SalesRatio -- 매출 구성 비율 (%)
FROM 
    SalesByCust S
CROSS JOIN 
    TotalSales T
ORDER BY 
    S.CustSalesAmt DESC;
";
            DataTable dt = db.GetDataTable(query);

            ChartControl chart = ChartSales_Cust;

            chart.Series.Clear();
            chart.Series.BeginUpdate();

            // 도넛 시리즈 생성
            Series doughnutSeries = new Series("거래처별 매출 점유율", ViewType.Doughnut);

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
                    string title = Convert.ToString(dr["CustName"]);
                    double count = Convert.ToDouble(dr["CustSalesAmt"]); // 건수(OrderCount) 바인딩
                    //double count = dr.IsNull("CustSalesAmt") ? 0d : Convert.ToDouble(dr["CustSalesAmt"]);


                    SeriesPoint point = new SeriesPoint(title, count);

                    //// 각 포인트마다 직접 색상 지정 (view.Palette 대신 사용)
                    //if (i < customColors.Length)
                    //{
                    //    point.Color = customColors[i];
                    //}

                    doughnutSeries.Points.Add(point);
                }

                // 그래프 자체 조각 라벨 숨기기
                //doughnutSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

                // 범례(Legend) 설정
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chart.Legend.MarkerMode = LegendMarkerMode.Marker;

                // {A} = 구간명, {V} = 건수, {VP:P1} = 차트 자동 계산 백분율
                // 예: "100% 이상    2 (16.7%)"
                doughnutSeries.LegendTextPattern = "{A} : {V:#,#} 원 ({VP:P1})";

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
                    //view.TotalLabel.Visible = true;
                    //view.TotalLabel.TextPattern = "전체\n{TV:#,##0} 개"; // {TV} : 전체 건수 합계
                    //view.TotalLabel.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                    //view.TotalLabel.TextColor = Color.FromArgb(50, 60, 75);
                }
            }

            chart.Series.EndUpdate();
            chart.AnimationStartMode = ChartAnimationMode.OnDataChanged;
        }
        #endregion

        #region 조회 쿼리 (그리드)

        private void SearchGrid_Work()
        {
            string startDt = DateTime.Now.AddDays(-3).ToString("yyyyMMdd");
            string endDt = DateTime.Now.ToString("yyyyMMdd");

            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
DECLARE @StartDate CHAR(8)      = '{startDt}'; -- 조회 시작일자 (OrderDate 기준)
DECLARE @EndDate   CHAR(8)      = '{endDt}'; -- 조회 종료일자

WITH OrderBase AS (
    -- 1. 작업지시 기본 정보 및 품명 조인
    SELECT 
        OP.CorpId,
        OP.OrderNo,
        OP.OrderSeq,
        OP.OrderDate,
        OP.ItemCode,
        I.ItemName,
        OP.ProcessCode,
        P.ProcessName,
        OP.OrderQty,
        OP.MachineCode,
        M.MachineName,
        OP.Status

    FROM PM_OrderProcess OP
    LEFT JOIN CM_Item I ON OP.CorpId = I.CorpId AND OP.ItemCode = I.ItemCode
    LEFT JOIN CM_Process P ON OP.CorpId = P.CorpId AND OP.ProcessCode = P.ProcessCode
    LEFT JOIN EQ_Machine M ON OP.CorpId = M.CorpId AND OP.MachineCode = M.MachineCode
    WHERE OP.CorpId = @CorpId
      AND OP.OrderDate BETWEEN @StartDate AND @EndDate
),
ResultSummary AS (
    -- 2. 오늘 기준 작업지시별 최신 작업시작/종료 시간 및 누적 생산량 집계
    SELECT 
        CorpId,
        OrderNo,
        OrderSeq,
        ISNULL(SUM(WorkQty), 0) AS WorkQty,
        MIN(StartDate) AS FirstStartDate, -- 최초 시작시간
        MAX(EndDate)   AS LastEndDate,    -- 최종 종료시간
        
        -- 진행중 판별용: 시작시간은 입력되었으나(<= 오늘) 종료시간이 없거나 미래인 경우의 건수
        COUNT(CASE WHEN StartDate IS NOT NULL AND (EndDate IS NULL) THEN 1 END) AS InProgressCount,
        -- 완료 판별용: 시작시간과 종료시간이 모두 오늘 이전/현재로 완료된 건수
        COUNT(CASE WHEN StartDate IS NOT NULL AND EndDate IS NOT NULL THEN 1 END) AS CompletedCount
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
    GROUP BY CorpId, OrderNo, OrderSeq
)
SELECT 
    ROW_NUMBER() OVER(ORDER BY O.OrderDate DESC, O.OrderNo ASC, O.OrderSeq ASC) AS Seq,
    
    -- 지시번호 (OrderSeq 구분 표기)
    CASE 
        WHEN O.OrderSeq IS NULL OR O.OrderSeq = 0 THEN O.OrderNo
        ELSE O.OrderNo + '-' + CAST(O.OrderSeq AS VARCHAR(10))
    END AS OrderNo,
    
    O.ItemCode,
    O.ItemName,
    O.ProcessCode,
    O.ProcessName,
    O.MachineName,
    FirstStartDate,
    -- 수량 관련
    ISNULL(O.OrderQty, 0) AS OrderQty,       -- 지시수량
    ISNULL(R.WorkQty, 0) AS WorkQty,   -- 생산량
    
    -- 진행률 (%) : (생산량 / 지시수량) * 100
    CASE 
        WHEN ISNULL(O.OrderQty, 0) = 0 THEN 0.0
        WHEN (CAST(ISNULL(R.WorkQty, 0) AS FLOAT) / O.OrderQty) * 100 > 100.0 THEN 100.0
        ELSE ROUND((CAST(ISNULL(R.WorkQty, 0) AS FLOAT) / O.OrderQty) * 100, 1)
    END AS ProgressRate,

  CASE 
		WHEN o.Status = '작업중' THEN '진행중'
        WHEN o.Status = '작업완료' THEN '완료'
        ELSE '대기'
    END AS OrderStatus

FROM OrderBase O
LEFT JOIN ResultSummary R ON O.CorpId = R.CorpId 
                        AND O.OrderNo = R.OrderNo 
                        AND O.OrderSeq = R.OrderSeq
ORDER BY
                CASE
                    WHEN O.Status = '작업완료' THEN 9999
                    ELSE 0
                END,
            FirstStartDate DESC;
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
    ROUND(ISNULL(TR.TodayRunMinutes, 0) / 60.0, 1) AS TodayRunHours
	, TodayRunMinutes
	, IIF(ISNULL(TodayStopMinutes, 0) = 0, 0, ROUND(ISNULL(TodayStopMinutes, 0)/60.0,1)) AS TodayStopHours
	, ROUND((TodayRunMinutes - ISNULL(TodayStopMinutes, 0)) / 60.0 ,1) AS TodayRealRunHours

FROM MachineBase M
LEFT JOIN TodayOrderResult TR ON M.MachineCode = TR.MachineCode
LEFT JOIN TodayStopResult TS  ON M.MachineCode = TS.MachineCode
LEFT JOIN LastOrderStatus R   ON M.MachineCode = R.MachineCode AND R.RN = 1
LEFT JOIN LastStopStatus S    ON M.MachineCode = S.MachineCode AND S.RN = 1
ORDER BY 
  CASE 
        WHEN S.StartTime IS NOT NULL AND (S.EndTime IS NULL OR S.EndTime > @NowTime) THEN 3
        WHEN R.StartDate IS NOT NULL AND (R.EndDate IS NULL OR R.EndDate > @NowTime) THEN 1
        ELSE 4
    END,
M.MachineCode ASC;


";

			DataTable dt = db.GetDataTable(query);
			GcMachine.DataSource = dt;

			GvMachine.BestFitColumns();
		}

        private void SearchGrid_Notice()
		{
            string date = DateTime.Now.ToString("d");

            SM_Notice notice = new SM_Notice();
            
            Gc_Notice.DataSource = notice.Select_SM_Notice_Top5(date);
        }

        #endregion

        #region Event
        private void timer1_Tick(object sender, EventArgs e)
        {
            LblCurTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (toggleSwitch1.IsOn)
            {
                if (LblRemainSec.Text == "-") LblRemainSec.Text = searchInterval.ToString("M0");

                LblRemainSec.Text = (LblRemainSec.Text.ConvertInt_Null_To_Zero() - 1).ToString("N0");
            }
        }

        private void Tm_Search_Tick(object sender, EventArgs e)
        {
            Search();
            LblRemainSec.Text = (searchInterval / 1000).ToString("N0");
        }

		#endregion

		private void toggleSwitch1_Toggled(object sender, EventArgs e)
		{
            //On - 자동 조회
            if (toggleSwitch1.IsOn)
            {
                Search();
                Tm_Search.Enabled = true;
                LblRemainSec.Text = (searchInterval / 1000).ToString("N0");
            }
            else
            {
                Tm_Search.Enabled = false;
                LblRemainSec.Text = "-";
            }
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            SimpleButton button = (SimpleButton)sender;

            Frm_Main Main = (Frm_Main)this.MdiParent;

            Form f = null;

            switch (button.Name)
            {
                case "Btn1":
                    f = new Frm_BM_SdOrder_MD();
                    break;

                case "Btn2":
                    f = new Frm_PM_OrderProcess_MC();
                    break;

                case "Btn3":
                    f = new Frm_PM_OrderProcessResult();
                    break;

                case "Btn4":
                    f = new Frm_MM_Purchase_MD();
                    break;

                case "Btn5":
                    f = new Frm_EQ_MachineChk("10");
                    break;
            }

            f.Text = button.Text;
            Main.doOpenForm(f);
        }

        private void btn_Colose_Click(object sender, EventArgs e)
		{
            this.Close();
		}

        private void Gv_Notice_Click(object sender, EventArgs e)
        {
            string boardNo = Gv_Notice.GetFocusedRowCellValue("BoardNo")?.ToString();

            if (string.IsNullOrEmpty(boardNo))
                return;

            Frm_SM_Notice frm = new Frm_SM_Notice(boardNo, false);
            frm.ShowDialog();
        }
	}
}