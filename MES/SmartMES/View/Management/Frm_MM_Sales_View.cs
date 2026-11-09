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
using SmartMES.Models.PM;

namespace SmartMES.View.Management
{
    public partial class Frm_MM_Sales_View : DevExpress.XtraEditors.XtraForm
    {

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";


        public Frm_MM_Sales_View()
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

        private void Frm_MM_Sales_View_Load(object sender, EventArgs e)
        {
            barDate.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            barDate.smtEndDateTimeValue = DateTime.Now;

            Tm_Time.Start();

            SetGrid(Gv_Sales);

            Search();

            Tm_Search.Start();
        }

        #region 디자인 설정
        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");
            grid.Create_GvColumn(gv, "총수주금액", "OrderPrice");
            grid.Create_GvColumn(gv, "총출하금액", "SalesPrice");
            grid.Create_GvColumn(gv, "비중(%)", "SalesRate");
            grid.Create_GvColumn(gv, "거래건수", "Cnt");
            grid.Create_GvColumn(gv, "평균납기일", "AvgDeliveryDay");

            //
            grid.SetCol_Type(gv.Columns["SalesRate"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OrderPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["SalesPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Cnt"], clsGrid.TypeList.TFloat);

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator; //행번호 표기
        }
        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            // 실적 일자 기준 조회
            SearchCardData();           // 상단 카드

            Search_Chart_InOutDay();
            Search_Chart_Cust();
            Search_Chart_Doughnut();

            SearchGrid_MM();        // 고객사별 매출 현황
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            DateTime startDate = barDate.smtStartDateTimeValue;
            DateTime endDate = barDate.smtEndDateTimeValue;

            string startDt = startDate.ToString("yyyy-MM-dd");
            string endDt = endDate.ToString("yyyy-MM-dd");

            query = $@"
                        SELECT
                              ISNULL(SO.OrderCount, 0) AS OrderCount
                            , ISNULL(SO.OrderAmount, 0) AS OrderAmount
                            , ISNULL(SA.SalesCount, 0) AS SalesCount
                            , ISNULL(SA.SalesAmount, 0) AS SalesAmount
                            , ISNULL(SA.CustCount, 0) AS CustCount

                        FROM
                        (
                            SELECT
                                  COUNT(DISTINCT OM.SdOrderNo) AS OrderCount
                                , SUM(OM.TotalPrice) AS OrderAmount
                            FROM BM_SdOrderM OM
                            WHERE OM.ObtainDate >= '{startDt}'
                              AND OM.ObtainDate < DATEADD(DAY, 1, '{endDt}')
                        ) SO

                        CROSS JOIN
                        (
                            SELECT
                                  COUNT(DISTINCT SM.SalesNo) AS SalesCount
                                , SUM(SD.TotalPrice) AS SalesAmount
                                , COUNT(DISTINCT SM.BillToCode) AS CustCount
                            FROM BM_SalesM SM
                            LEFT JOIN BM_SalesD SD ON SM.CorpId = SD.CorpId AND SM.SalesNo = SD.SalesNo
                            WHERE SM.SalesDate >= '{startDt}'
                              AND SM.SalesDate < DATEADD(DAY, 1, '{endDt}')
                        ) SA;
            ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                // 수주건수
                LblTotalPlanQty.Text = dr["OrderCount"].ConvertFloat_Null_To_Zero().ToString("N0") + " 건";

                // 출하건수
                LblTotalWorkQty.Text = dr["SalesCount"].ConvertFloat_Null_To_Zero().ToString("N0") + " 건";

                // 총수주금액
                LblAchieveRate.Text = dr["OrderAmount"].ConvertFloat_Null_To_Zero().ToString("N0") + " 원";

                // 총출하금액
                LblGoodQty.Text = dr["SalesAmount"].ConvertFloat_Null_To_Zero().ToString("N0") + " 원";

                // 거래처수
                LblNgQty.Text = dr["CustCount"].ConvertFloat_Null_To_Zero().ToString("N0") + " ";
            }
        }
        #endregion

        #region 매출금액 추이 차트 로드

        private void Search_Chart_InOutDay()
        {
            DateTime startDate = barDate.smtStartDateTimeValue;
            DateTime endDate = barDate.smtEndDateTimeValue;
            string startDt = startDate.ToString("yyyy-MM-dd");
            string endDt = endDate.ToString("yyyy-MM-dd");

            query = $@"
                DECLARE @StartDate DATE = '{startDt}';
                DECLARE @EndDate   DATE = '{endDt}';

                ;WITH DateList AS
                (
                    SELECT @StartDate AS SalesDate
                    UNION ALL
                    SELECT DATEADD(DAY, 1, SalesDate)
                    FROM DateList
                    WHERE SalesDate < @EndDate
                )
                SELECT
                      D.SalesDate
                    , ISNULL(SUM(SD.TotalPrice), 0) AS TotalSalesPrice
                    , COUNT(DISTINCT SM.SalesNo) AS SalesCount
                FROM DateList D
                LEFT JOIN BM_SalesM SM ON D.SalesDate = SM.SalesDate
                LEFT JOIN BM_SalesD SD ON SM.CorpId = SD.CorpId AND SM.SalesNo = SD.SalesNo
                GROUP BY D.SalesDate
                ORDER BY D.SalesDate
                OPTION (MAXRECURSION 0);
            ";

            DataTable dt = db.GetDataTable(query);

            //==================== 차트 초기화 ====================
            MMChart.Series.Clear();
            MMChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            MMChart.BackColor = Color.White;

            if (dt == null || dt.Rows.Count == 0)
                return;

            //==================== 범례 ====================
            MMChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            MMChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            MMChart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            MMChart.Legend.Direction = LegendDirection.LeftToRight;
            MMChart.Legend.Font = new Font("맑은 고딕", 9, FontStyle.Bold);

            //==================== 매출금액 ====================
            Series seriesSales = new Series("매출금액", ViewType.Bar);

            seriesSales.DataSource = dt;
            seriesSales.ArgumentDataMember = "SalesDate";
            seriesSales.ValueDataMembers.AddRange(new string[] { "TotalSalesPrice" });
            seriesSales.CrosshairLabelPattern = "{S} : {V:#,##0} 원";
            seriesSales.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            SideBySideBarSeriesLabel salesLabel = (SideBySideBarSeriesLabel)seriesSales.Label;
            salesLabel.TextPattern = "{V:#,##0}";
            salesLabel.Font = new Font("맑은 고딕", 8, FontStyle.Bold);

            SideBySideBarSeriesView salesView = (SideBySideBarSeriesView)seriesSales.View;
            salesView.Color = Color.FromArgb(100, 170, 230);
            salesView.BarWidth = 0.6;

            MMChart.Series.Add(seriesSales);

            //==================== 출하건수 ====================
            Series seriesCount = new Series("출하건수", ViewType.Line);

            seriesCount.DataSource = dt;
            seriesCount.ArgumentDataMember = "SalesDate";
            seriesCount.ValueDataMembers.AddRange(new string[] { "SalesCount" });
            seriesCount.CrosshairLabelPattern = "{S} : {V:#,##0} 건";
            seriesCount.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            PointSeriesLabel countLabel = (PointSeriesLabel)seriesCount.Label;
            countLabel.TextPattern = "{V:#,##0}";
            countLabel.Font = new Font("맑은 고딕", 8, FontStyle.Bold);

            LineSeriesView countView = (LineSeriesView)seriesCount.View;
            countView.Color = Color.FromArgb(245, 170, 100);
            countView.LineStyle.Thickness = 3;
            countView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            MMChart.Series.Add(seriesCount);

            //==================== 차트 디자인 ====================
            XYDiagram diagram = (XYDiagram)MMChart.Diagram;

            diagram.DefaultPane.BackColor = Color.White;
            diagram.DefaultPane.BorderVisible = false;

            //==================== 기존 보조 Y축 초기화 ====================
            diagram.SecondaryAxesY.Clear();

            //==================== 보조 Y축 ====================
            SecondaryAxisY secondaryAxisY = new SecondaryAxisY("출하건수");

            secondaryAxisY.Title.Text = "출하건수";
            secondaryAxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

            secondaryAxisY.Label.TextPattern = "{V:#,##0}";
            secondaryAxisY.GridLines.Visible = false;

            // 보조축 최소값 0
            secondaryAxisY.WholeRange.Auto = false;
            secondaryAxisY.WholeRange.SetMinMaxValues(0, 10);

            diagram.SecondaryAxesY.Add(secondaryAxisY);

            // 출하건수 Line을 보조축에 연결
            countView.AxisY = secondaryAxisY;

            //==================== X축 ====================
            diagram.AxisX.GridLines.Visible = false;
            diagram.AxisX.Label.Font = new Font("맑은 고딕", 9);
            diagram.AxisX.Label.TextPattern = "{A:MM-dd}";

            //==================== 기본 Y축 ====================
            diagram.AxisY.GridLines.Visible = true;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            diagram.AxisY.Label.TextPattern = "{V:#,##0}";

            //==================== Crosshair ====================
            MMChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            MMChart.CrosshairOptions.ShowArgumentLine = true;
            MMChart.CrosshairOptions.ShowValueLine = true;
        }

        #endregion

        #region 제품별 매출금액 TOP 5 차트 로드

        private void Search_Chart_Cust()
        {
            DateTime startDate = barDate.smtStartDateTimeValue;
            DateTime endDate = barDate.smtEndDateTimeValue;

            string startDt = startDate.ToString("yyyy-MM-dd");
            string endDt = endDate.ToString("yyyy-MM-dd");

            query = $@"
                    SELECT TOP 5
                          SD.ItemCode
                        , CM.ItemName
                        , SUM(SD.TotalPrice) AS TotalSalesPrice
                    FROM BM_SalesM SM
                    LEFT JOIN BM_SalesD SD ON SM.CorpId = SD.CorpId AND SM.SalesNo = SD.SalesNo
                    LEFT JOIN CM_Item CM ON SD.CorpId = CM.CorpId AND SD.ItemCode = CM.ItemCode
                    WHERE SM.SalesDate >= '{startDt}'
                      AND SM.SalesDate < DATEADD(DAY, 1, '{endDt}')
                    GROUP BY SD.ItemCode, CM.ItemName
                    ORDER BY TotalSalesPrice DESC;
            ";

            DataTable dt = db.GetDataTable(query);

            //==================== 차트 초기화 ====================
            CustChart.Series.Clear();
            CustChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            CustChart.BackColor = Color.White;

            if (dt == null || dt.Rows.Count == 0)
                return;

            //==================== 범례 ====================
            CustChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            CustChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            CustChart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            CustChart.Legend.Direction = LegendDirection.LeftToRight;
            CustChart.Legend.Font = new Font("맑은 고딕", 9, FontStyle.Bold);

            //==================== 제품별 매출금액 ====================
            Series series = new Series("매출금액", ViewType.Bar);

            series.DataSource = dt;
            series.ArgumentDataMember = "ItemCode";
            series.ValueDataMembers.AddRange(new string[] { "TotalSalesPrice" });
            series.CrosshairLabelPattern = "{A} : {V:#,##0} 원";
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            //==================== 막대 라벨 ====================
            SideBySideBarSeriesLabel label = (SideBySideBarSeriesLabel)series.Label;
            label.TextPattern = "{V:#,##0}";
            label.Font = new Font("맑은 고딕", 8, FontStyle.Bold);

            //==================== 막대 디자인 ====================
            SideBySideBarSeriesView view = (SideBySideBarSeriesView)series.View;
            view.Color = Color.FromArgb(80, 190, 175);
            view.BarWidth = 0.6;

            CustChart.Series.Add(series);

            //==================== 차트 디자인 ====================
            XYDiagram diagram = (XYDiagram)CustChart.Diagram;
            diagram.Rotated = true;

            diagram.DefaultPane.BackColor = Color.White;
            diagram.DefaultPane.BorderVisible = false;

            //==================== 제품코드 ==================== //품목 길어서 우선 코드로
            diagram.AxisX.GridLines.Visible = false;
            diagram.AxisX.Label.Font = new Font("맑은 고딕", 9);
            diagram.AxisX.Label.TextPattern = "{A}";
            diagram.AxisX.Label.Visible = true;
            diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;

            //==================== 매출금액 ====================
            diagram.AxisY.GridLines.Visible = true;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            diagram.AxisY.Label.TextPattern = "{V:#,##0}";

            //==================== Crosshair ====================
            CustChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            CustChart.CrosshairOptions.ShowArgumentLine = true;
            CustChart.CrosshairOptions.ShowValueLine = true;
        }

        #endregion

        #region 도넛 차트 로드

        private void Search_Chart_Doughnut()
        {
            DateTime startDate = barDate.smtStartDateTimeValue;
            DateTime endDate = barDate.smtEndDateTimeValue;
            string startDt = startDate.ToString("yyyy-MM-dd");
            string endDt = endDate.ToString("yyyy-MM-dd");

            query = $@"
                        SELECT
                              SD.ItemCode
                            , CM.ItemName
                            , SUM(SD.TotalPrice) AS TotalSalesPrice
                            , CAST(
                                SUM(SD.TotalPrice) * 100.0 /
                                NULLIF(SUM(SUM(SD.TotalPrice)) OVER (), 0)
                                AS DECIMAL(5, 1)
                              ) AS SalesRate
                        FROM BM_SalesM SM
                        LEFT JOIN BM_SalesD SD ON SM.CorpId = SD.CorpId AND SM.SalesNo = SD.SalesNo
                        LEFT JOIN CM_Item CM ON SD.CorpId = CM.CorpId AND SD.ItemCode = CM.ItemCode
                        WHERE SM.SalesDate BETWEEN '{startDt}' AND '{endDt}'
                        GROUP BY SD.ItemCode, CM.ItemName
                        ORDER BY TotalSalesPrice DESC;
            ";

            DataTable dt = db.GetDataTable(query);

            //==================== 차트 초기화 ====================
            ChartDoughnut.Series.Clear();
            ChartDoughnut.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartDoughnut.BackColor = Color.White;

            if (dt == null || dt.Rows.Count == 0)
                return;

            //==================== 도넛 차트 ====================
            Series series = new Series("품목별 매출금액", ViewType.Doughnut);

            series.DataSource = dt;
            series.ArgumentDataMember = "ItemName";
            series.ValueDataMembers.AddRange(new string[] { "TotalSalesPrice" });

            //==================== 라벨 ====================
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            DoughnutSeriesLabel label = (DoughnutSeriesLabel)series.Label;
            label.TextPattern = "{A}\n{V:#,##0}원 ({SalesRate:0.0}%)";
            label.Font = new Font("맑은 고딕", 8, FontStyle.Bold);
            label.Position = PieSeriesLabelPosition.TwoColumns;

            //==================== 도넛 디자인 ====================
            DoughnutSeriesView view = (DoughnutSeriesView)series.View;
            view.HoleRadiusPercent = 60;

            ChartDoughnut.Series.Add(series);

            //==================== 범례 ====================
            ChartDoughnut.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            //==================== Crosshair ====================
            ChartDoughnut.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
        }

        #endregion

        #region 조회 쿼리
        private void SearchGrid_MM()
        {
            DateTime startDate = barDate.smtStartDateTimeValue;
            DateTime endDate = barDate.smtEndDateTimeValue;
            string startDt = startDate.ToString("yyyy-MM-dd");
            string endDt = endDate.ToString("yyyy-MM-dd");

            query = $@"
                       WITH OrderData AS
                        (
                            SELECT
                                  OM.SoldToCode AS CustCode
                                , MAX(CC.CustName) AS CustName
                                , SUM(OM.TotalPrice) AS OrderPrice
                            FROM BM_SdOrderM OM
                            LEFT JOIN CM_Cust CC ON OM.CorpId = CC.CorpId AND OM.SoldToCode = CC.CustCode
                            WHERE OM.ObtainDate BETWEEN '{startDt}' AND '{endDt}'
                            GROUP BY OM.SoldToCode
                        ),
                        SalesData AS
                        (
                            SELECT
                                  SM.BillToCode AS CustCode
                                , MAX(SM.BillToName) AS CustName
                                , COUNT(DISTINCT SM.SalesNo) AS Cnt
                                , SUM(SM.TotalPrice) AS SalesPrice
                            FROM BM_SalesM SM
                            WHERE SM.SalesDate BETWEEN '{startDt}' AND '{endDt}'
                            GROUP BY SM.BillToCode
                        ),
                        DeliveryData AS
                        (
                            SELECT
                                  OM.BillToCode AS CustCode
                                , AVG(DATEDIFF(DAY, CAST(OM.ObtainDate AS DATE), CAST(OD.DeliveryDate AS DATE))) AS AvgDeliveryDay
                            FROM BM_SdOrderM OM
                            LEFT JOIN BM_SdOrderD OD ON OM.CorpId = OD.CorpId AND OM.SdOrderNo = OD.SdOrderNo
                            WHERE OM.ObtainDate BETWEEN '{startDt}' AND '{endDt}'
                              AND OD.DeliveryDate IS NOT NULL
                            GROUP BY OM.BillToCode
                        )

                        SELECT
                              ISNULL(OD.CustCode, SD.CustCode) AS CustCode
                            , ISNULL(OD.CustName, SD.CustName) AS CustName

                            -- 수주액
                            , ISNULL(OD.OrderPrice, 0) AS OrderPrice

                            -- 출하액
                            , ISNULL(SD.SalesPrice, 0) AS SalesPrice

                            -- 출하 비중
                            , CAST(
                                ISNULL(SD.SalesPrice,0) * 100.0 /
                                NULLIF(SUM(ISNULL(SD.SalesPrice,0)) OVER (),0)
                                AS DECIMAL(5,1)
                              ) AS SalesRate

                            -- 거래건수
                            , ISNULL(SD.Cnt,0) AS Cnt

                            -- 평균납기일
                            , ISNULL(DD.AvgDeliveryDay,0) AS AvgDeliveryDay

                        FROM OrderData OD
                        FULL OUTER JOIN SalesData SD ON OD.CustCode = SD.CustCode
                        LEFT JOIN DeliveryData DD ON ISNULL(OD.CustCode, SD.CustCode) = DD.CustCode
                        ORDER BY ISNULL(SalesPrice,0) DESC;
                    ";

            Gc_Sales.DataSource = db.GetDataTable(query);
            Gv_Sales.BestFitColumns();
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