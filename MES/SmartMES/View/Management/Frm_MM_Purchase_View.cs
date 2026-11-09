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
    public partial class Frm_MM_Purchase_View : DevExpress.XtraEditors.XtraForm
    {

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";


        public Frm_MM_Purchase_View()
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
            barDate.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            barDate.smtEndDateTimeValue = DateTime.Now;

            Tm_Time.Start();

            SetGrid(Gv_MM);
            SetGrid_Cust(Gv_Cust);

            Search();

            Tm_Search.Start();
        }

        #region 디자인 설정
        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "매입수량", "Qty");
            grid.Create_GvColumn(gv, "매입단가", "UnitPrice");
            grid.Create_GvColumn(gv, "매입금액", "TotalPrice");
            grid.Create_GvColumn(gv, "비율(%)", "PurchaseRate");

            //
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["PurchaseRate"], clsGrid.TypeList.TFloat);

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
        }

        private void SetGrid_Cust(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "거래처코드", "ItemCode");
            grid.Create_GvColumn(gv, "거래처명", "ItemName");
            grid.Create_GvColumn(gv, "발주수량", "Qty");
            grid.Create_GvColumn(gv, "입고율(%)", "PurchaseRate");
            grid.Create_GvColumn(gv, "입고수량", "UnitPrice");
            grid.Create_GvColumn(gv, "미입고수량", "TotalPrice");

            //
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["PurchaseRate"], clsGrid.TypeList.TFloat);

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
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

            SearchGrid_MM();        // 품목별 매입금액 현황
            SearchGrid_Cust();      // 공급업체별 상세 현황
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
                  ISNULL(PO.OrderCount, 0) AS OrderCount
                , ISNULL(PO.OrderAmount, 0) AS OrderAmount
                , ISNULL(PU.PurchaseCount, 0) AS PurchaseCount
                , ISNULL(PU.PurchaseAmount, 0) AS PurchaseAmount
                , ISNULL(PU.CustCount, 0) AS CustCount

            FROM
            (
                SELECT
                      COUNT(DISTINCT PM.MmOrderNo) AS OrderCount
                    , SUM(PD.TotalPrice) AS OrderAmount
                FROM MM_PlacingM PM
                LEFT JOIN MM_PlacingD PD
                    ON PM.CorpId = PD.CorpId
                   AND PM.MmOrderNo = PD.MmOrderNo
                WHERE PM.PlacingDate >= '{startDt}'
                  AND PM.PlacingDate < DATEADD(DAY, 1, '{endDt}')
            ) PO

            CROSS JOIN
            (
                SELECT
                      COUNT(DISTINCT PM.StoNo) AS PurchaseCount
                    , SUM(PD.TotalPrice) AS PurchaseAmount
                    , COUNT(DISTINCT PM.CustCode) AS CustCount
                FROM MM_PurchaseM PM
                LEFT JOIN MM_PurchaseD PD
                    ON PM.CorpId = PD.CorpId
                   AND PM.StoNo = PD.StoNo
                WHERE PM.StoDate >= '{startDt}'
                  AND PM.StoDate < DATEADD(DAY, 1, '{endDt}')
            ) PU;
        ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                LblTotalPlanQty.Text = dr["OrderCount"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";
                LblTotalWorkQty.Text = dr["PurchaseCount"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";
                LblAchieveRate.Text = dr["OrderAmount"].ConvertFloat_Null_To_Zero().ToString("N0") + " 원";
                LblGoodQty.Text = dr["PurchaseAmount"].ConvertFloat_Null_To_Zero().ToString("N0") + " 원";
                LblNgQty.Text = dr["CustCount"].ConvertFloat_Null_To_Zero().ToString("N0") + " ";

            }
        }
        #endregion

        #region 월별 매입금액 추이 차트 로드 
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
                            SELECT @StartDate AS StoDate
                            UNION ALL
                            SELECT DATEADD(DAY, 1, StoDate)
                            FROM DateList
                            WHERE StoDate < @EndDate
                        )
                        SELECT
                            D.StoDate,
                            ISNULL(SUM(PD.TotalPrice), 0) AS TotalPurchasePrice,
                            COUNT(DISTINCT PM.StoNo) AS PurchaseCount
                        FROM DateList D
                        LEFT JOIN MM_PurchaseM PM ON D.StoDate = PM.StoDate
                        LEFT JOIN MM_PurchaseD PD ON PM.CorpId = PD.CorpId AND PM.StoNo = PD.StoNo
                        GROUP BY D.StoDate
                        ORDER BY D.StoDate
                        OPTION (MAXRECURSION 0);
            ";

            DataTable dt = db.GetDataTable(query);
            //==================== 차트 초기화 ====================
            MMChart.Series.Clear();
            MMChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            MMChart.BackColor = Color.White;

            //==================== 범례 ====================
            MMChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            MMChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            MMChart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            MMChart.Legend.Direction = LegendDirection.LeftToRight;
            MMChart.Legend.Font = new Font("맑은 고딕", 9, FontStyle.Bold);

            //==================== 매입금액 ====================
            Series seriesPurchase = new Series("매입금액", ViewType.Bar);

            seriesPurchase.DataSource = dt;
            seriesPurchase.ArgumentDataMember = "StoDate";
            seriesPurchase.ValueDataMembers.AddRange(new string[] { "TotalPurchasePrice" });
            seriesPurchase.CrosshairLabelPattern = "{S} : {V:#,##0} 원";
            seriesPurchase.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            SideBySideBarSeriesLabel purchaseLabel = (SideBySideBarSeriesLabel)seriesPurchase.Label;
            purchaseLabel.TextPattern = "{V:#,##0}";
            purchaseLabel.Font = new Font("맑은 고딕", 8, FontStyle.Bold);

            SideBySideBarSeriesView purchaseView = (SideBySideBarSeriesView)seriesPurchase.View;
            purchaseView.Color = Color.FromArgb(50, 100, 180);
            purchaseView.BarWidth = 0.6;

            MMChart.Series.Add(seriesPurchase);

            //==================== 입고건수 ====================
            Series seriesCount = new Series("입고건수", ViewType.Line);

            seriesCount.DataSource = dt;
            seriesCount.ArgumentDataMember = "StoDate";
            seriesCount.ValueDataMembers.AddRange(new string[] { "PurchaseCount" });
            seriesCount.CrosshairLabelPattern = "{S} : {V:#,##0} 건";
            seriesCount.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            PointSeriesLabel countLabel = (PointSeriesLabel)seriesCount.Label;
            countLabel.TextPattern = "{V:#,##0}";
            countLabel.Font = new Font("맑은 고딕", 8, FontStyle.Bold);

            LineSeriesView countView = (LineSeriesView)seriesCount.View;
            countView.Color = Color.FromArgb(80, 175, 125);
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
            SecondaryAxisY secondaryAxisY = new SecondaryAxisY("입고건수");

            secondaryAxisY.Title.Text = "입고건수";
            secondaryAxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

            secondaryAxisY.Label.TextPattern = "{V:#,##0}";
            secondaryAxisY.GridLines.Visible = false;

            // 보조축 최소값 0
            secondaryAxisY.WholeRange.Auto = false;
            secondaryAxisY.WholeRange.SetMinMaxValues(0, 10);

            diagram.SecondaryAxesY.Add(secondaryAxisY);

            // 입고건수 Line을 보조축에 연결
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

        #region 공급업체별 매입금액 TOP 5 차트 로드 
        private void Search_Chart_Cust()
        {
            DateTime startDate = barDate.smtStartDateTimeValue;
            DateTime endDate = barDate.smtEndDateTimeValue;
            string startDt = startDate.ToString("yyyy-MM-dd");
            string endDt = endDate.ToString("yyyy-MM-dd");

            query = $@"
                        SELECT TOP 5
                            CC.CustName,
                            SUM(PD.TotalPrice) AS TotalPurchasePrice
                        FROM MM_PurchaseM PM
                        LEFT JOIN MM_PurchaseD PD ON PM.CorpId = PD.CorpId AND PM.StoNo = PD.StoNo
				        LEFT JOIN CM_Cust CC ON PM.CorpId = CC.CorpId AND PM.CustCode = CC.CustCode
                        WHERE PM.StoDate BETWEEN '{startDt}' AND '{endDt}'
                        GROUP BY CC.CustName
                        ORDER BY TotalPurchasePrice DESC;
            ";

            DataTable dt = db.GetDataTable(query);
            //==================== 차트 초기화 ====================
            CustChart.Series.Clear();
            CustChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            CustChart.BackColor = Color.White;

            //==================== 범례 ====================
            CustChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            CustChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            CustChart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            CustChart.Legend.Direction = LegendDirection.LeftToRight;
            CustChart.Legend.Font = new Font("맑은 고딕", 9, FontStyle.Bold);

            //==================== 공급업체별 매입금액 ====================
            Series series = new Series("매입금액", ViewType.Bar);

            series.DataSource = dt;
            series.ArgumentDataMember = "CustName";
            series.ValueDataMembers.AddRange(new string[] { "TotalPurchasePrice" });
            series.CrosshairLabelPattern = "{A} : {V:#,##0} 원";
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            SideBySideBarSeriesLabel label = (SideBySideBarSeriesLabel)series.Label;
            label.TextPattern = "{V:#,##0}";
            label.Font = new Font("맑은 고딕", 8, FontStyle.Bold);

            SideBySideBarSeriesView view = (SideBySideBarSeriesView)series.View;
            view.Color = Color.FromArgb(55, 91, 150);
            view.BarWidth = 0.6;

            CustChart.Series.Add(series);

            //==================== 차트 디자인 ====================
            XYDiagram diagram = (XYDiagram)CustChart.Diagram;

            // 가로 막대 차트
            diagram.Rotated = true;

            diagram.DefaultPane.BackColor = Color.White;
            diagram.DefaultPane.BorderVisible = false;

            // X축 (공급업체)
            diagram.AxisX.GridLines.Visible = false;
            diagram.AxisX.Label.Font = new Font("맑은 고딕", 9);
            diagram.AxisX.Label.TextPattern = "{A}";

            // Y축 (매입금액)
            diagram.AxisY.GridLines.Visible = true;
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;
            diagram.AxisY.Label.TextPattern = "{V:#,##0}";

            // Crosshair
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
              	    SET ANSI_WARNINGS OFF
	                SET ARITHIGNORE ON
	                SET ARITHABORT OFF

                       SELECT
                            CM.ItemType,
	                        CC.Title, 
                            SUM(PD.TotalPrice) AS TotalPrice,
                            CAST(
                                SUM(PD.TotalPrice) * 100.0 /
                                SUM(SUM(PD.TotalPrice)) OVER ()
                                AS DECIMAL(5, 1)
                            ) AS PurchaseRate
                        FROM MM_PurchaseM PM
                        LEFT JOIN MM_PurchaseD PD ON PM.CorpId = PD.CorpId AND PM.StoNo = PD.StoNo
                        LEFT JOIN CM_Item CM ON PD.CorpId = CM.CorpId AND PD.ItemCode = CM.ItemCode
                        LEFT JOIN CM_CateItem CC ON CM.CorpId = CC.CorpId AND CM.ItemType = CC.ClassItemCode AND CLASS = 'A010'
                        WHERE PM.StoDate BETWEEN '{startDt}' AND '{endDt}'
                        GROUP BY CM.ItemType, CC.Title
                        ORDER BY TotalPrice DESC;
            ";

            DataTable dt = db.GetDataTable(query);

            ChartDoughnut.Series.Clear();

            if (dt == null || dt.Rows.Count == 0)
                return;

            //==================== 차트 초기화 ====================
            ChartDoughnut.Series.Clear();
            ChartDoughnut.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartDoughnut.BackColor = Color.White;

            if (dt == null || dt.Rows.Count == 0)
                return;

            //==================== 도넛 차트 ====================
            Series series = new Series("품목분류별 매입금액", ViewType.Doughnut);

            series.DataSource = dt;
            series.ArgumentDataMember = "Title";
            series.ValueDataMembers.AddRange(new string[] { "TotalPrice" });

            //==================== 라벨 ====================
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            DoughnutSeriesLabel label = (DoughnutSeriesLabel)series.Label;
            label.TextPattern = "{A}\n{V:#,##0}원 ({PurchaseRate:0.0}%)";
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
                        SET ANSI_WARNINGS OFF
	                    SET ARITHIGNORE ON
	                    SET ARITHABORT OFF

                        SELECT TOP 10
                            PD.ItemCode,
                            CM.ItemName,
                            SUM(PD.Qty) AS Qty,
                            PD.UnitPrice,
                            SUM(PD.TotalPrice) AS TotalPrice,
                            CAST(
                                SUM(PD.TotalPrice) * 100.0 /
                                SUM(SUM(PD.TotalPrice)) OVER ()
                                AS DECIMAL(5, 1)
                            ) AS PurchaseRate
                        FROM MM_PurchaseM PM
                        LEFT JOIN MM_PurchaseD PD ON PM.CorpId = PD.CorpId AND PM.StoNo = PD.StoNo
                        LEFT JOIN CM_Item CM ON PD.CorpId = CM.CorpId AND PD.ItemCode = CM.ItemCode
                        WHERE PM.StoDate BETWEEN '{startDt}' AND '{endDt}'
                        GROUP BY PD.ItemCode, CM.ItemName, PD.UnitPrice
                        ORDER BY TotalPrice DESC;
                    ";

            Gc_MM.DataSource = db.GetDataTable(query);
            Gv_MM.BestFitColumns();
        }

        private void SearchGrid_Cust()
        {
            DateTime startDate = barDate.smtStartDateTimeValue;
            DateTime endDate = barDate.smtEndDateTimeValue;

            string startDt = startDate.ToString("yyyy-MM-dd");
            string endDt = endDate.ToString("yyyy-MM-dd");

            query = $@"
                WITH PurchaseData AS
                (
                    SELECT
                          PD.CorpId
                        , PD.MmOrderNo
                        , PD.MmOrderSeq
                        , PD.Qty AS OrderQty
                        , ISNULL(PC.InQty, 0) AS InQty
                        , PD.Qty - ISNULL(PC.InQty, 0) AS BalanceQty
                        , PM.CustCode
                        , CC.CustName
                    FROM MM_PlacingD PD
                    LEFT JOIN MM_PlacingM PM ON PD.CorpId = PM.CorpId AND PD.MmOrderNo = PM.MmOrderNo
                    LEFT JOIN CM_Cust CC ON PM.CorpId = CC.CorpId AND PM.CustCode = CC.CustCode
                    LEFT JOIN
                    (
                        SELECT
                              PC.CorpId
                            , PC.MmOrderNo
                            , PC.MmOrderSeq
                            , SUM(PC.Qty) AS InQty
                        FROM MM_PurchaseD PC
                        GROUP BY PC.CorpId, PC.MmOrderNo, PC.MmOrderSeq
                    ) PC ON PD.CorpId = PC.CorpId AND PD.MmOrderNo = PC.MmOrderNo AND PD.MmOrderSeq = PC.MmOrderSeq
                    WHERE PM.PlacingDate >= '{startDt}' AND PM.PlacingDate < DATEADD(DAY, 1, '{endDt}')
                )

                SELECT
                      CustCode AS ItemCode
                    , MAX(CustName) AS ItemName
                    , SUM(OrderQty) AS Qty
                    , CAST(
                        SUM(InQty) * 100.0 / NULLIF(SUM(OrderQty), 0)
                        AS DECIMAL(5, 1)
                      ) AS PurchaseRate
                    , SUM(InQty) AS UnitPrice
                    , SUM(CASE WHEN BalanceQty > 0 THEN BalanceQty ELSE 0 END) AS TotalPrice
                FROM PurchaseData
                GROUP BY CustCode
                ORDER BY SUM(OrderQty) DESC;
            ";

            Gc_Cust.DataSource = db.GetDataTable(query);
            Gv_Cust.BestFitColumns();
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