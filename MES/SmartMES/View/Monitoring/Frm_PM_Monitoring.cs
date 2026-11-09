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

namespace SmartMES.View.Monitoring
{
    public partial class Frm_PM_Monitoring : DevExpress.XtraEditors.XtraForm
    {

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";


        public Frm_PM_Monitoring()
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
            barDate.smtStartDateTimeValue = DateTime.Now;
            barDate.smtEndDateTimeValue = DateTime.Now;

            Tm_Time.Start();

            SetGrid(Gv_Work);

            Search();

            Tm_Search.Start();
        }

        #region 디자인 설정
        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "실적수량", "TotalWorkQty");
            grid.Create_GvColumn(gv, "진행률(%)", "WorkRate");
            grid.Create_GvColumn(gv, "상태", "WorkStatus");

            //
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalWorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkRate"], clsGrid.TypeList.TFloat);

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
        }
        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            // 실적 일자 기준 조회
            SearchCardData();           // 상단 카드

            Search_Chart_Doughnut();

            SearchGrid_Work();          // 하단 생산진행현황
            Search_Chart_InOutDay();    // 하단 시간별 생산 추이 차트
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            DateTime baseDate = barDate.smtStartDateTimeValue;
            string startDt = baseDate.ToString("yyyy-MM-dd 00:00:00");

            query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                        DECLARE @Today DATE = '{startDt}';

                        SELECT
                              SUM(OP.OrderQty) AS 전체생산지시
                            , SUM(ISNULL(R.TotalQty,0)) AS 전체생산실적
                            , SUM(ISNULL(R.GoodQty,0)) AS 양품수량
                            , SUM(ISNULL(R.NgQty,0)) AS 불량수량
                            , CASE
                                WHEN SUM(OP.OrderQty)=0 THEN 0
                                ELSE ROUND(SUM(ISNULL(R.TotalQty,0))*100.0/SUM(OP.OrderQty),1)
                              END AS 생산달성률
                            , CASE
                                WHEN SUM(OP.OrderQty)=0 THEN 0
                                ELSE ROUND(SUM(ISNULL(R.TotalQty,0))*100.0/SUM(OP.OrderQty),1)
                              END AS 진행률
                        FROM PM_OrderProcess OP
                        INNER JOIN
                        (
                            SELECT
                                  CorpId
                                , OrderNo
                                , OrderSeq
                                , SUM(GoodQty) AS GoodQty
                                , SUM(NgQty) AS NgQty
                                , SUM(GoodQty+NgQty) AS TotalQty
                            FROM PM_OrderResult
                            WHERE CorpId=@CorpId
                              AND WorkDate>=@Today
                              AND WorkDate<DATEADD(DAY,1,@Today)
                            GROUP BY CorpId,OrderNo,OrderSeq
                        ) R
                        ON OP.CorpId = R.CorpId
                        AND OP.OrderNo = R.OrderNo
                        AND OP.OrderSeq = R.OrderSeq
                        WHERE OP.CorpId=@CorpId;
            ";
            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                LblTotalPlanQty.Text = dr["전체생산지시"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";
                LblTotalWorkQty.Text = dr["전체생산실적"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";
                LblAchieveRate.Text = dr["생산달성률"].ConvertFloat_Null_To_Zero().ToString("N1") + " %";
                LblGoodQty.Text = dr["양품수량"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";
                LblNgQty.Text = dr["불량수량"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";

            }
        }
        #endregion

        #region 차트 로드 
        private void Search_Chart_InOutDay()
        {
            DateTime baseDate = barDate.smtStartDateTimeValue;
            string startDt = baseDate.ToString("yyyy-MM-dd 00:00:00");
            string endDt = baseDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00");

            // 실적 종료시간 기준 조회
            query = $@"
                    -- [1단계] 7시부터 20시(오후 8시)까지만 시간 축(마스터) 생성
                    WITH WorkTimeSlots AS (
                        SELECT 7 AS [Hour]
                        UNION ALL SELECT 8  UNION ALL SELECT 9  UNION ALL SELECT 10 
                        UNION ALL SELECT 11 UNION ALL SELECT 12 UNION ALL SELECT 13 
                        UNION ALL SELECT 14 UNION ALL SELECT 15 UNION ALL SELECT 16 
                        UNION ALL SELECT 17 UNION ALL SELECT 18 UNION ALL SELECT 19 
                        UNION ALL SELECT 20
                    )
                    SELECT 
                        RIGHT('0' + CAST(ts.[Hour] AS VARCHAR(2)), 2) + '시' AS [WorkHour]

                        -- 실적이 없는 시간대는 0으로 채우기
                        , ISNULL(SUM(r.WorkQty), 0) AS [TotalWorkQty]
                        , ISNULL(SUM(r.GoodQty), 0) AS [TotalGoodQty]
                        , ISNULL(SUM(r.NgQty), 0)   AS [TotalNgQty]
                    FROM WorkTimeSlots ts
                    LEFT JOIN [YOZM].[dbo].[PM_OrderResult] r 
                        ON ts.[Hour] = DATEPART(HOUR, r.EndDate)
                       AND r.CorpId = '{WrGlobal.CorpID}'
                       AND r.EndDate >= '{startDt}'
                       AND r.EndDate < '{endDt}'
                    GROUP BY ts.[Hour]
                    ORDER BY ts.[Hour] ASC;
            ";

            DataTable dt = db.GetDataTable(query);

            //==================== 차트 초기화 ====================
            lineChart.Series.Clear();
            lineChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            lineChart.BackColor = Color.White;

            //==================== 범례 ====================
            lineChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            lineChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            lineChart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            lineChart.Legend.Direction = LegendDirection.LeftToRight;
            lineChart.Legend.Font = new Font("맑은 고딕", 9, FontStyle.Bold);

            //==================== 생산량 ====================
            Series seriesWork = new Series("생산량", ViewType.Line);
            seriesWork.ArgumentDataMember = "WorkHour";
            seriesWork.ValueDataMembers.AddRange(new string[] { "TotalWorkQty" });
            seriesWork.DataSource = dt;
            seriesWork.CrosshairLabelPattern = "{S} : {V:#,0} EA";

            seriesWork.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            PointSeriesLabel workLabel = (PointSeriesLabel)seriesWork.Label;
            workLabel.TextPattern = "{V:#,0}";
            workLabel.Font = new Font("맑은 고딕", 8, FontStyle.Bold);
            workLabel.TextColor = Color.DodgerBlue;

            LineSeriesView workView = (LineSeriesView)seriesWork.View;
            workView.Color = Color.DodgerBlue;
            workView.LineStyle.Thickness = 4;
            workView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            lineChart.Series.Add(seriesWork);

            //==================== 불량량 ====================
            Series seriesNg = new Series("불량량", ViewType.Line);
            seriesNg.ArgumentDataMember = "WorkHour";
            seriesNg.ValueDataMembers.AddRange(new string[] { "TotalNgQty" });
            seriesNg.DataSource = dt;
            seriesNg.CrosshairLabelPattern = "{S} : {V:#,0} EA";

            LineSeriesView ngView = (LineSeriesView)seriesNg.View;
            ngView.Color = Color.FromArgb(255, 102, 102);
            ngView.LineStyle.Thickness = 2;
            ngView.LineStyle.DashStyle = DashStyle.Dash;
            ngView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            lineChart.Series.Add(seriesNg);

            //==================== 차트 디자인 ====================
            XYDiagram diagram = (XYDiagram)lineChart.Diagram;

            diagram.DefaultPane.BackColor = Color.White;
            diagram.DefaultPane.BorderVisible = false;

            // X축
            diagram.AxisX.GridLines.Visible = false;
            diagram.AxisX.Color = Color.FromArgb(90, 90, 90);
            diagram.AxisX.Label.Font = new Font("맑은 고딕", 9, FontStyle.Regular);
            diagram.AxisX.Label.TextColor = Color.FromArgb(120, 200, 200, 200);

            // Y축
            diagram.AxisY.GridLines.Visible = true;
            diagram.AxisY.GridLines.Color = Color.FromArgb(120, 200, 200, 200);
            diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dash;

            diagram.AxisY.Label.TextPattern = "{V:#,##0}";
            diagram.AxisY.Label.Font = new Font("맑은 고딕", 9, FontStyle.Regular);
            diagram.AxisY.GridLines.Color = Color.FromArgb(120, 200, 200, 200);

            // Crosshair
            lineChart.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            lineChart.CrosshairOptions.ShowArgumentLine = true;
            lineChart.CrosshairOptions.ShowValueLine = true;
        }

        #endregion

        #region 도넛 차트 로드

        private void Search_Chart_Doughnut()
        {
            DateTime baseDate = barDate.smtStartDateTimeValue;
            string startDt = baseDate.ToString("yyyy-MM-dd 00:00:00");

            query = $@"
                DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                DECLARE @Today DATE = '{startDt}';

                -- 품목 TOP5
                SELECT TOP 5
                      'ITEM' AS ChartType
                    , I.ItemName AS ChartName
                    , SUM(R.GoodQty + R.NgQty) AS Qty
                FROM PM_OrderResult R
                INNER JOIN CM_Item I ON R.CorpId = I.CorpId AND R.ItemCode = I.ItemCode
                WHERE R.CorpId = @CorpId
                AND R.WorkDate >= @Today
                AND R.WorkDate < DATEADD(DAY,1,@Today)
                GROUP BY I.ItemName

                UNION ALL

                -- 공정 TOP5
                SELECT TOP 5
                      'PROCESS' AS ChartType
                    , P.ProcessName AS ChartName
                    , SUM(R.GoodQty + R.NgQty) AS Qty
                FROM PM_OrderResult R
                INNER JOIN CM_Process P ON R.CorpId = P.CorpId AND R.ProcessCode = P.ProcessCode
                WHERE R.CorpId = @CorpId
                AND R.WorkDate >= @Today
                AND R.WorkDate < DATEADD(DAY,1,@Today)
                GROUP BY P.ProcessName

                ORDER BY ChartType, Qty DESC;
            ";

            DataTable dt = db.GetDataTable(query);

            ChartStock.Series.Clear();

            if (dt == null || dt.Rows.Count == 0)
                return;


            // =============================================================
            // 왼쪽 도넛 : 품목별 생산수량
            // =============================================================

            DataTable dtItem = dt.Select("ChartType = 'ITEM'")
                                 .CopyToDataTable();


            Series seriesItem = new Series("품목별 생산", ViewType.Doughnut);

            seriesItem.DataSource = dtItem;
            seriesItem.ArgumentDataMember = "ChartName";
            seriesItem.ValueDataMembers.AddRange(new string[] { "Qty" });

            seriesItem.LegendTextPattern = "{A} : {V:#,##0}";


            seriesItem.Label.BackColor = Color.Transparent;
            seriesItem.Label.Border.Visibility = DefaultBoolean.False;


            DoughnutSeriesLabel itemLabel = seriesItem.Label as DoughnutSeriesLabel;

            if (itemLabel != null)
            {
                itemLabel.TextPattern = "{A}\n{VP:0.0%}";
                itemLabel.Position = PieSeriesLabelPosition.TwoColumns;
            }


            DoughnutSeriesView itemView = seriesItem.View as DoughnutSeriesView;

            if (itemView != null)
            {
                itemView.HoleRadiusPercent = 65;
                itemView.Border.Visibility = DefaultBoolean.False;
                itemView.RuntimeExploding = true;

                itemView.Titles.Clear();

                SeriesTitle title = new SeriesTitle();
                title.Text = "품목별 생산수량";

                itemView.Titles.Add(title);
            }


            // =============================================================
            // 오른쪽 도넛 : 공정별 생산수량
            // =============================================================

            DataTable dtProcess = dt.Select("ChartType = 'PROCESS'")
                                    .CopyToDataTable();

            Series seriesProcess = new Series("공정별 생산", ViewType.Doughnut);

            seriesProcess.DataSource = dtProcess;
            seriesProcess.ArgumentDataMember = "ChartName";
            seriesProcess.ValueDataMembers.AddRange(new string[] { "Qty" });

            seriesProcess.LegendTextPattern = "{A} : {V:#,##0}";

            seriesProcess.Label.BackColor = Color.Transparent;
            seriesProcess.Label.Border.Visibility = DefaultBoolean.False;

            DoughnutSeriesLabel processLabel = seriesProcess.Label as DoughnutSeriesLabel;

            if (processLabel != null)
            {
                processLabel.TextPattern = "{A}\n{VP:0.0%}";
                processLabel.Position = PieSeriesLabelPosition.TwoColumns;
            }

            DoughnutSeriesView processView = seriesProcess.View as DoughnutSeriesView;

            if (processView != null)
            {
                processView.HoleRadiusPercent = 65;
                processView.Border.Visibility = DefaultBoolean.False;
                processView.RuntimeExploding = true;

                processView.Titles.Clear();

                SeriesTitle title = new SeriesTitle();
                title.Text = "공정별 생산수량";

                processView.Titles.Add(title);
            }

            // =============================================================
            // 차트 추가
            // =============================================================

            ChartStock.Series.AddRange(new Series[]
            {
                seriesItem,
                seriesProcess
            });

            // =============================================================
            // 좌우 동일 영역 배치
            // =============================================================

            SimpleDiagram diagram = ChartStock.Diagram as SimpleDiagram;

            if (diagram != null)
            {
                diagram.LayoutDirection = LayoutDirection.Horizontal;
            }
        }

        #endregion

        #region 조회 쿼리
        private void SearchGrid_Work()
        {
            DateTime baseDate = barDate.smtStartDateTimeValue;
            string startDt = baseDate.ToString("yyyy-MM-dd 00:00:00");
            string endDt = baseDate.AddDays(1).ToString("yyyy-MM-dd 00:00:00");

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
						WHERE CorpId = '{WrGlobal.CorpID}' AND WOrkDate BETWEEN '{startDt}' AND '{endDt}'  
						 GROUP BY OrderNo, OrderSeq
                    ) ORS ON OP.OrderNo = ORS.OrderNo AND OP.OrderSeq = ORS.OrderSeq 
                    ";

            Gc_Work.DataSource = db.GetDataTable(query);
            Gv_Work.BestFitColumns();

            Gc_Tile.DataSource = db.GetDataTable(query);
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