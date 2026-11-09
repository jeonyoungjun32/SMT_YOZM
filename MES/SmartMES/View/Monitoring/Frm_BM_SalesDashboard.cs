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
    public partial class Frm_BM_SalesDashboard : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        public Frm_BM_SalesDashboard()
        {
            InitializeComponent();

            // 상단 조회조건 색상 설정
            smtCommonBar1.smtBackColor = Color.Transparent;
            btn_Colose.ForeColor = Color.Black;
        }

        private void Frm_BM_SalesDashboard_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            SetGrid_Work(Gv_Cust);

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

            grid.Create_GvColumn(gv, "거래처명", "CustomerName");
            grid.Create_GvColumn(gv, "매출액", "SalesAmt");
            grid.Create_GvColumn(gv, "비중(%)", "SalesRate");
            grid.Create_GvColumn(gv, "건수", "SalesCount");
            grid.Create_GvColumn(gv, "최근거래일", "LastSalesDate");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["SalesAmt"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["SalesCount"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["SalesRate"], clsGrid.TypeList.TFloatN1);

            //gv.Columns["ProcessName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gv.Columns["ProcessStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            //gv.RowCellStyle += Gv_RowCellStyle;
            //gv.CustomDrawCell += gridView1_CustomDrawCell;
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

        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            SearchCardData();          // 상단 카드
            Search_Chart_Sales_Month();
            Search_Chart_Sales_Item();
            Search_Chart_Sales_ItemRate();
            Search_Chart_SdOrder();
            Search_Chart_Sales_Target();
            SearchGrid_Work();
            SearchGrid_Notice();
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            query = $@"
                        DECLARE @Today        CHAR(8) = CONVERT(CHAR(8), GETDATE(), 112);
                        DECLARE @StartOfMonth CHAR(8) = CONVERT(CHAR(8), DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0), 112);
                        DECLARE @CorpId       VARCHAR(10) = '{WrGlobal.CorpID}';

                 SELECT
                -- =============================================
                -- 1. 당월 매출액
                -- =============================================
                ISNULL((
                    SELECT SUM(ISNULL(TotalPrice, 0))
                    FROM BM_SalesM
                    WHERE CorpId = @CorpId
                        AND OutType = '10'
                        AND SalesDate >= @StartOfMonth
                        AND SalesDate <= @Today
                ), 0) AS SalesAmount,


                -- =============================================
                -- 2. 당월 수주액
                -- =============================================
                ISNULL((
                    SELECT SUM(ISNULL(TotalPrice, 0))
                    FROM BM_SdOrderM
                    WHERE CorpId = @CorpId
                        AND ObtainDate >= @StartOfMonth
                        AND ObtainDate <= @Today
                ), 0) AS OrderAmount,


                -- =============================================
                -- 3. 당월 매출 달성률
                -- 매출액 / 수주액 * 100
                -- =============================================
                CASE 
                    WHEN ISNULL((
                        SELECT SUM(ISNULL(TotalPrice, 0))
                        FROM BM_SdOrderM
                        WHERE CorpId = @CorpId
                            AND ObtainDate >= @StartOfMonth
                            AND ObtainDate <= @Today
                    ), 0) = 0
                    THEN 0
                    ELSE
                        ISNULL((
                            SELECT SUM(ISNULL(TotalPrice, 0))
                            FROM BM_SalesM
                            WHERE CorpId = @CorpId
                                AND OutType = '10'
                                AND SalesDate >= @StartOfMonth
                                AND SalesDate <= @Today
                        ), 0) * 100.0
                        /
                        (
                            SELECT SUM(ISNULL(TotalPrice, 0))
                            FROM BM_SdOrderM
                            WHERE CorpId = @CorpId
                                AND ObtainDate >= @StartOfMonth
                                AND ObtainDate <= @Today
                        )
                END AS SalesAchievementRate,


                -- =============================================
                -- 4. 수주 잔고
                -- 누적 수주액 - 누적 매출액
                -- =============================================
                ISNULL((
                    SELECT SUM(ISNULL(TotalPrice, 0))
                    FROM BM_SdOrderM
                    WHERE CorpId = @CorpId
                ), 0)
                -
                ISNULL((
                    SELECT SUM(ISNULL(TotalPrice, 0))
                    FROM BM_SalesM
                    WHERE CorpId = @CorpId  
                    AND OutType = '10'
                ), 0) AS OrderBalance,

                -- =============================================
                -- 5. 당월 거래처 수
                -- 당월 출하(매출)가 발생한 거래처 기준
                -- =============================================
                ISNULL((
                    SELECT COUNT(DISTINCT BillToCode)
                    FROM BM_SalesM
                    WHERE CorpId = @CorpId
                      AND OutType = '10'
                      AND SalesDate >= @StartOfMonth
                      AND SalesDate <= @Today
                      AND ISNULL(BillToCode, '') <> ''
                ), 0) AS CustomerCount
                    ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                // 당월 매출액
                LblSalesAmount.Text = dr["SalesAmount"].ConvertInt_Null_To_Zero().ToString("N0") + " 원";

                // 당월 수주액
                LblOrderAmt.Text = dr["OrderAmount"].ConvertInt_Null_To_Zero().ToString("N0") + " 원";

                // 당월 매출달성률
                LblSalesRate.Text = dr["SalesAchievementRate"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";

                // 수주잔고
                LblOrderBalance.Text = dr["OrderBalance"].ConvertInt_Null_To_Zero().ToString("N0") + " 원";

                // 당월 거래처수
                LblCustomerCnt.Text = dr["CustomerCount"].ConvertInt_Null_To_Zero().ToString("N0") + " 개";
            }
        }
        #endregion

        #region 차트 로드
        private void Search_Chart_Sales_Month()
        {
            // 월별 매출 추이 + 전월 대비 증감률
            query = $@"
                    DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';

                    ;WITH MonthSales AS
                    (
                        SELECT
                            YEAR(SalesDate) AS SalesYear,
                            MONTH(SalesDate) AS SalesMonthNo,
                            SUM(ISNULL(TotalPrice, 0)) AS TotalSalesAmt
                        FROM BM_SalesM
                        WHERE CorpId = @CorpId
                          AND OutType = '10'
                          AND SalesDate >= DATEADD(MONTH, -11, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
                          AND SalesDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                        GROUP BY YEAR(SalesDate),
                            MONTH(SalesDate)
                    ),
                    MonthSalesWithPrev AS
                    (
                        SELECT
                            SalesYear,
                            SalesMonthNo,
                            TotalSalesAmt,

                            LAG(TotalSalesAmt) OVER (
                                ORDER BY SalesYear, SalesMonthNo
                            ) AS PrevSalesAmt

                        FROM MonthSales
                    )
                    SELECT
                        SalesYear,
                        SalesMonthNo,

                        -- X축 표시 : 4월, 5월, 6월, 7월
                        CAST(SalesMonthNo AS VARCHAR(2)) + '월' AS SalesMonth,

                        -- 매출액
                        TotalSalesAmt,

                        -- 전월 대비 증감률
                        CASE
                            WHEN PrevSalesAmt IS NULL OR PrevSalesAmt = 0
                                THEN NULL
                            ELSE
                                (TotalSalesAmt - PrevSalesAmt) * 100.0 / PrevSalesAmt
                        END AS SalesGrowthRate

                    FROM MonthSalesWithPrev
                    ORDER BY SalesYear, SalesMonthNo;
                ";

            DataTable dt = db.GetDataTable(query);

            ChartSales_Month.DataSource = dt;

            ChartSales_Month.Series.Clear();
            // 범례 설정 (상단 우측 배치)
            ChartSales_Month.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ChartSales_Month.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            ChartSales_Month.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            ChartSales_Month.Legend.Direction = LegendDirection.LeftToRight;
            ChartSales_Month.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_Month.BackColor = Color.White;


            // =============================================================
            // 막대 : 매출액
            // =============================================================

            Series s1 = new Series("매출액", ViewType.Bar);

            s1.ArgumentDataMember = "SalesMonth";
            s1.ValueDataMembers.AddRange(new string[] { "TotalSalesAmt" });
            s1.DataSource = dt;

            s1.CrosshairLabelPattern = "{A}: {V:#,##0} 원";
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s1.Label.TextPattern = "{V:#,##0}";

            ChartSales_Month.Series.Add(s1);


            // =============================================================
            // 라인 : 전월 대비
            // =============================================================

            Series s2 = new Series("전월 대비", ViewType.Line);

            s2.ArgumentDataMember = "SalesMonth";
            s2.ValueDataMembers.AddRange(new string[] { "SalesGrowthRate" });
            s2.DataSource = dt;

            s2.CrosshairLabelPattern = "{A}: {V:+0.0;-0.0;0.0}%";
            s2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s2.Label.TextPattern = "{V:+0.0;-0.0;0.0}%";

            ChartSales_Month.Series.Add(s2);

            // =============================================================
            // 차트 설정
            // =============================================================

            XYDiagram diagram = ChartSales_Month.Diagram as XYDiagram;

            if (diagram != null)
            {
                // X축 : 4월, 5월, 6월, 7월
                diagram.AxisX.Label.TextPattern = "{A}";

                // 기본 Y축 : 매출액
                diagram.AxisY.Label.TextPattern = "{V:N0}";

                // =========================================================
                // 보조 Y축 : 전월 대비 %
                // =========================================================

                SecondaryAxisY secondaryAxisY = new SecondaryAxisY("전월 대비");

                secondaryAxisY.Title.Text = "전월 대비 (%)";
                secondaryAxisY.Label.TextPattern = "{V:0.0}%";

                diagram.SecondaryAxesY.Clear();
                diagram.SecondaryAxesY.Add(secondaryAxisY);

                // 라인 → 보조 Y축 연결
                LineSeriesView lineView = (LineSeriesView)s2.View;
                lineView.Color = Color.FromArgb(40, 170, 100);
                lineView.AxisY = secondaryAxisY;
                lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                lineView.LineStyle.Thickness = 2;
            }


            // =============================================================
            // 막대 디자인
            // =============================================================

            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;

            barView.Color = Color.FromArgb(50, 140, 240);
            barView.BarWidth = 0.4;
        }

        private void Search_Chart_Sales_Item()
        {
            // 제품별 매출 TOP 5
            query = $@"
                DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';

                SELECT TOP 5
                    D.ItemCode, 
                    CI.ItemName,
                    SUM(ISNULL(D.TotalPrice, 0)) AS TotalSalesAmt
                FROM BM_SalesD D
                LEFT JOIN BM_SalesM M ON D.CorpId = M.CorpId AND D.SalesNo = M.SalesNo
                LEFT JOIN CM_Item CI ON D.CorpId = CI.CorpId AND D.ItemCode = CI.ItemCode
                WHERE D.CorpId = @CorpId
                  AND M.OutType = '10'
                  AND M.SalesDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
                  AND M.SalesDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                GROUP BY D.ItemCode, CI.ItemName
                ORDER BY TotalSalesAmt DESC
            ";

            DataTable dt = db.GetDataTable(query);

            ChartSales_Item.DataSource = dt;

            ChartSales_Item.Series.Clear();
            ChartSales_Item.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_Item.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_Item.BackColor = Color.White;

            Series s1 = new Series("제품별 매출", ViewType.Bar);
            s1.ArgumentDataMember = "ItemName";
            s1.ValueDataMembers.AddRange(new string[] { "TotalSalesAmt" });
            s1.DataSource = dt;
            s1.CrosshairLabelPattern = "{A} : {V:#,#} 원";
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            ChartSales_Item.Series.Add(s1);

            XYDiagram diagram = ChartSales_Item.Diagram as XYDiagram;

            if (diagram != null)
            {
                diagram.Rotated = true;

                // 1위 제품이 위쪽에 표시
                diagram.AxisX.Reverse = true;

                // 제품명 축 강제 표시
                diagram.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisX.Label.Visible = true;

                // 제품명이 길어도 숨김 방지
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;

                // 금액 축
                diagram.AxisY.Label.TextPattern = "{V:N0}";
            }

            // 막대 디자인
            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(30, 120, 220);
            barView.BarWidth = 0.4;

            // 막대 끝 금액 표시
            s1.Label.TextPattern = "{V:#,#}";

            BarSeriesLabel barLabel = (BarSeriesLabel)s1.Label;
            barLabel.Position = BarSeriesLabelPosition.Top;
            barLabel.BackColor = Color.Transparent;
            barLabel.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            barLabel.TextColor = Color.FromArgb(70, 80, 95);
        }

        private void Search_Chart_Sales_ItemRate()
        {
            // 당월 매출 구성 비율 - 품목별
            query = $@"
                DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';

                ;WITH ItemSales AS
                (
                    SELECT
                        D.ItemCode,
                        CI.ItemName,
                        SUM(ISNULL(D.TotalPrice, 0)) AS SalesAmt
                    FROM BM_SalesD D
                    LEFT JOIN BM_SalesM M ON D.CorpId = M.CorpId AND D.SalesNo = M.SalesNo
                    LEFT JOIN CM_Item CI ON D.CorpId = CI.CorpId AND D.ItemCode = CI.ItemCode
                    WHERE D.CorpId = @CorpId
                      AND M.OutType = '10'
                      AND M.SalesDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
                      AND M.SalesDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                    GROUP BY D.ItemCode, CI.ItemName
                ),
                RankedItem AS
                (
                    SELECT
                        ItemName,
                        SalesAmt,
                        ROW_NUMBER() OVER (ORDER BY SalesAmt DESC) AS RN
                    FROM ItemSales
                )
                SELECT
                    CASE
                        WHEN RN <= 5 THEN ItemName
                        ELSE '기타'
                    END AS ItemName,
                    SUM(SalesAmt) AS SalesAmt
                FROM RankedItem
                GROUP BY
                    CASE
                        WHEN RN <= 5 THEN ItemName
                        ELSE '기타'
                    END
                ORDER BY SalesAmt DESC;
            ";

            DataTable dt = db.GetDataTable(query);

            ChartSales_ItemRate.DataSource = dt;

            ChartSales_ItemRate.Series.Clear();
            ChartSales_ItemRate.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_ItemRate.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            ChartSales_ItemRate.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
            ChartSales_ItemRate.Legend.Direction = LegendDirection.TopToBottom;
            ChartSales_ItemRate.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_ItemRate.BackColor = Color.White;

            Series s1 = new Series("품목별 매출", ViewType.Doughnut);

            s1.ArgumentDataMember = "ItemName";
            s1.ValueDataMembers.AddRange(new string[] { "SalesAmt" });
            s1.DataSource = dt;

            ChartSales_ItemRate.Series.Add(s1);

            DoughnutSeriesLabel label = (DoughnutSeriesLabel)s1.Label;
            label.TextPattern = "{A} {VP:0.0}%";
            label.Position = PieSeriesLabelPosition.TwoColumns;
            label.ResolveOverlappingMode = ResolveOverlappingMode.Default;
            label.BackColor = Color.Transparent;
            label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            DoughnutSeriesView doughnutView = (DoughnutSeriesView)s1.View;
            doughnutView.HoleRadiusPercent = 60;
        }

        private void Search_Chart_SdOrder()
        {
            // 월별 수주 현황 + 전월 대비
            query = $@"
                DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';

                ;WITH MonthOrder AS
                (
                    SELECT
                        YEAR(ObtainDate) AS OrderYear,
                        MONTH(ObtainDate) AS OrderMonthNo,
                        SUM(ISNULL(TotalPrice, 0)) AS TotalOrderAmt
                    FROM BM_SdOrderM
                    WHERE CorpId = @CorpId
                      AND ObtainDate >= DATEADD(MONTH, -11, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
                      AND ObtainDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                    GROUP BY YEAR(ObtainDate), MONTH(ObtainDate)
                ),
                MonthOrderWithPrev AS
                (
                    SELECT
                        OrderYear,
                        OrderMonthNo,
                        TotalOrderAmt,
                        LAG(TotalOrderAmt) OVER (
                            ORDER BY OrderYear, OrderMonthNo
                        ) AS PrevOrderAmt
                    FROM MonthOrder
                )
                SELECT
                    OrderYear,
                    OrderMonthNo,
                    CAST(OrderMonthNo AS VARCHAR(2)) + '월' AS OrderMonth,
                    TotalOrderAmt,
                    CASE
                        WHEN PrevOrderAmt IS NULL OR PrevOrderAmt = 0 THEN NULL
                        ELSE (TotalOrderAmt - PrevOrderAmt) * 100.0 / PrevOrderAmt
                    END AS OrderGrowthRate
                FROM MonthOrderWithPrev
                ORDER BY OrderYear, OrderMonthNo;
            ";

            DataTable dt = db.GetDataTable(query);

            ChartSales_SdOrder.DataSource = dt;

            ChartSales_SdOrder.Series.Clear();
            // 범례 설정 (상단 우측 배치)
            ChartSales_SdOrder.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ChartSales_SdOrder.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            ChartSales_SdOrder.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            ChartSales_SdOrder.Legend.Direction = LegendDirection.LeftToRight;
            ChartSales_SdOrder.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_SdOrder.BackColor = Color.White;

            Series s1 = new Series("수주액", ViewType.Bar);
            s1.ArgumentDataMember = "OrderMonth";
            s1.ValueDataMembers.AddRange(new string[] { "TotalOrderAmt" });
            s1.DataSource = dt;
            s1.CrosshairLabelPattern = "{A}: {V:#,##0} 원";
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s1.Label.TextPattern = "{V:#,##0}";

            ChartSales_SdOrder.Series.Add(s1);

            Series s2 = new Series("전월 대비", ViewType.Line);
            s2.ArgumentDataMember = "OrderMonth";
            s2.ValueDataMembers.AddRange(new string[] { "OrderGrowthRate" });
            s2.DataSource = dt;
            s2.CrosshairLabelPattern = "{A}: {V:+0.0;-0.0;0.0}%";
            s2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s2.Label.TextPattern = "{V:+0.0;-0.0;0.0}%";

            ChartSales_SdOrder.Series.Add(s2);

            XYDiagram diagram = ChartSales_SdOrder.Diagram as XYDiagram;

            if (diagram != null)
            {
                diagram.AxisX.Label.TextPattern = "{A}";
                diagram.AxisY.Label.TextPattern = "{V:N0}";

                SecondaryAxisY secondaryAxisY = new SecondaryAxisY("전월 대비");
                secondaryAxisY.Title.Text = "전월 대비 (%)";
                secondaryAxisY.Label.TextPattern = "{V:0.0}%";

                diagram.SecondaryAxesY.Clear();
                diagram.SecondaryAxesY.Add(secondaryAxisY);

                LineSeriesView lineView = (LineSeriesView)s2.View;
                lineView.AxisY = secondaryAxisY;
                lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                lineView.LineStyle.Thickness = 2;
            }

            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(15, 90, 190);
            barView.BarWidth = 0.4;
        }

        private void Search_Chart_Sales_Target()
        {
            // 매출 목표 대비 달성 현황
            query = $@"
                DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';

                ;WITH MonthSales AS
                (
                    SELECT
                        YEAR(M.SalesDate) AS SalesYear,
                        MONTH(M.SalesDate) AS SalesMonthNo,
                        SUM(ISNULL(M.TotalPrice, 0)) AS SalesAmt
                    FROM BM_SalesM M
                    WHERE M.CorpId = @CorpId
                      AND M.OutType = '10'
                      AND M.SalesDate >= DATEADD(MONTH, -11, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
                      AND M.SalesDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                    GROUP BY YEAR(M.SalesDate), MONTH(M.SalesDate)
                ),
                MonthOrder AS
                (
                    SELECT
                        YEAR(M.ObtainDate) AS OrderYear,
                        MONTH(M.ObtainDate) AS OrderMonthNo,
                        SUM(ISNULL(M.TotalPrice, 0)) AS OrderAmt
                    FROM BM_SdOrderM M
                    WHERE M.CorpId = @CorpId
                      AND M.ObtainDate >= DATEADD(MONTH, -11, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
                      AND M.ObtainDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                    GROUP BY YEAR(M.ObtainDate), MONTH(M.ObtainDate)
                )
                SELECT
                    S.SalesYear,
                    S.SalesMonthNo,
                    CAST(S.SalesMonthNo AS VARCHAR(2)) + '월' AS SalesMonth,
                    ISNULL(O.OrderAmt, 0) AS OrderAmt,
                    S.SalesAmt,
                    CASE
                        WHEN ISNULL(O.OrderAmt, 0) = 0 THEN NULL
                        ELSE S.SalesAmt * 100.0 / O.OrderAmt
                    END AS AchievementRate
                FROM MonthSales S
                LEFT JOIN MonthOrder O ON S.SalesYear = O.OrderYear AND S.SalesMonthNo = O.OrderMonthNo
                ORDER BY S.SalesYear, S.SalesMonthNo;
            ";

            DataTable dt = db.GetDataTable(query);

            ChartSales_Target.DataSource = dt;

            ChartSales_Target.Series.Clear();
            ChartSales_Target.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_Target.BackColor = Color.White;

            // =============================================================
            // 범례 : 차트 상단 우측
            // =============================================================

            ChartSales_Target.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ChartSales_Target.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            ChartSales_Target.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            ChartSales_Target.Legend.Direction = LegendDirection.LeftToRight;
            ChartSales_Target.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartSales_Target.Legend.Font = new Font("맑은 고딕", 9, FontStyle.Regular);


            // =============================================================
            // 막대 1 : 수주액
            // 진한 블루
            // =============================================================

            Series s1 = new Series("수주액", ViewType.Bar);

            s1.ArgumentDataMember = "SalesMonth";
            s1.ValueDataMembers.AddRange(new string[] { "OrderAmt" });
            s1.DataSource = dt;

            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            ChartSales_Target.Series.Add(s1);


            // =============================================================
            // 막대 2 : 매출액
            // 밝은 블루
            // =============================================================

            Series s2 = new Series("매출액", ViewType.Bar);

            s2.ArgumentDataMember = "SalesMonth";
            s2.ValueDataMembers.AddRange(new string[] { "SalesAmt" });
            s2.DataSource = dt;

            s2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s2.Label.TextPattern = "{V:#,##0}";

            ChartSales_Target.Series.Add(s2);


            // =============================================================
            // 라인 : 매출 달성률
            // 그린 계열
            // =============================================================

            Series s3 = new Series("매출 달성률", ViewType.Line);

            s3.ArgumentDataMember = "SalesMonth";
            s3.ValueDataMembers.AddRange(new string[] { "AchievementRate" });
            s3.DataSource = dt;

            s3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s3.Label.TextPattern = "{V:0.0}%";

            ChartSales_Target.Series.Add(s3);


            // =============================================================
            // 차트 설정
            // =============================================================

            XYDiagram diagram = ChartSales_Target.Diagram as XYDiagram;

            if (diagram != null)
            {
                // X축 : 1월, 2월, 3월 ...
                diagram.AxisX.Label.TextPattern = "{A}";

                // 기본 Y축 : 금액
                diagram.AxisY.Label.TextPattern = "{V:N0}";

                // =========================================================
                // 보조 Y축 : 달성률
                // =========================================================

                SecondaryAxisY secondaryAxisY = new SecondaryAxisY("달성률");

                secondaryAxisY.Title.Text = "달성률 (%)";
                secondaryAxisY.Label.TextPattern = "{V:0}%";

                diagram.SecondaryAxesY.Clear();
                diagram.SecondaryAxesY.Add(secondaryAxisY);

                // 라인 → 보조 Y축 연결
                LineSeriesView lineView = (LineSeriesView)s3.View;

                lineView.AxisY = secondaryAxisY;
                lineView.Color = Color.FromArgb(40, 170, 100);
                lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                lineView.LineStyle.Thickness = 3;


                // =========================================================
                // 보조 Y축 범위
                // =========================================================

                secondaryAxisY.WholeRange.SetMinMaxValues(0, 150);
                secondaryAxisY.VisualRange.SetMinMaxValues(0, 150);
            }


            // =============================================================
            // 막대 디자인
            // =============================================================

            SideBySideBarSeriesView orderBarView =
                (SideBySideBarSeriesView)s1.View;

            orderBarView.Color = Color.FromArgb(30, 100, 200);
            orderBarView.BarWidth = 0.35;


            // =============================================================
            // 매출액 막대 디자인
            // =============================================================

            SideBySideBarSeriesView salesBarView =
                (SideBySideBarSeriesView)s2.View;

            salesBarView.Color = Color.FromArgb(50, 140, 240);
            salesBarView.BarWidth = 0.35;
        }

        #endregion

        #region 조회 쿼리 (그리드)

        private void SearchGrid_Work()
        {
            query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';

                        ;WITH CustomerSales AS
                        (
                            SELECT
                                M.BillToCode,
                                M.BillToName AS CustomerName,
                                SUM(ISNULL(M.TotalPrice, 0)) AS SalesAmt,
                                COUNT(DISTINCT M.SalesNo) AS SalesCount,
                                MAX(M.SalesDate) AS LastSalesDate
                            FROM BM_SalesM M
                            WHERE M.CorpId = @CorpId
                                AND M.OutType = '10'
                                AND M.SalesDate >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
                                AND M.SalesDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                            GROUP BY  M.BillToCode, M.BillToName
                        )
                        SELECT
                            CustomerName,
                            SalesAmt,
                            CONCAT(
                                CAST(
                                    CASE
                                        WHEN SUM(SalesAmt) OVER () = 0 THEN 0
                                        ELSE SalesAmt * 100.0 / SUM(SalesAmt) OVER ()
                                    END
                                AS DECIMAL(5, 1)),
                                '%'
                            ) AS SalesRate,
                            SalesCount,
                            LastSalesDate
                        FROM CustomerSales
                        ORDER BY SalesAmt DESC
            ";

            Gc_Cust.DataSource = db.GetDataTable(query);
            Gv_Cust.BestFitColumns();
        }

        private void SearchGrid_Notice()
		{
            string date = DateTime.Now.ToString("d");

            SM_Notice notice = new SM_Notice();
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
        #endregion

    }
}