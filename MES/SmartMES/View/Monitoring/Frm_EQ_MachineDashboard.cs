using DevExpress.CodeParser;
using DevExpress.DashboardCommon;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
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
    public partial class Frm_EQ_MachineDashboard : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        public Frm_EQ_MachineDashboard()
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
            SetGrid_OrderProcess(Gv_Repair);

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

            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "양품수량(EA)", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량(EA)", "NgQty");
            grid.Create_GvColumn(gv, "작업시간(분)", "WorkTime");
            grid.Create_GvColumn(gv, "작업상태", "WorkStatus");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkTime"], clsGrid.TypeList.TFloat);

            gv.Columns["WorkStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
            //gv.CustomDrawCell += gridView1_CustomDrawCell;
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string value = e.CellValue.ConvertString_Null_To_Empty();

            if (e.Column.FieldName == "WorkStatus") //
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

            grid.Create_GvColumn(gv, "발생시간", "RepairStartTime");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "고장내용", "RePairCause");
            grid.Create_GvColumn(gv, "수리내용", "RepairState");
            grid.Create_GvColumn(gv, "수리시간(분)", "RepairTime");

            //gv.Columns["WorkStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
        }

        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            SearchCardData();

            Search_Chart_EquipmentRate();
            Search_Chart_EquipmentStatus();
            Search_Chart_EquipmentRate_Top5();
            Search_Chart_MachineRepair();
            Search_Chart_RepairCause();
            Search_Chart_StopType();
            Search_TileView_EquipmentRate();

            SearchGrid_Work();
            SearchGrid_Machine();
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                        DECLARE @Today DATE = CAST(GETDATE() AS DATE);

                        DECLARE @TotalMachineCount INT = 0;
                        DECLARE @RunningMachineCount INT = 0;
                        DECLARE @StopMachineCount INT = 0;
                        DECLARE @TotalRunTime BIGINT = 0;
                        DECLARE @TotalStopTime BIGINT = 0;
                        DECLARE @TotalFailureTime BIGINT = 0;


                        -- 전체 설비 수
                        SELECT
                            @TotalMachineCount = COUNT(*)
                        FROM EQ_Machine
                        WHERE CorpId = @CorpId
                          AND UseYn = 'Y'


                        -- 전체 가동시간
                        SELECT
                            @TotalRunTime = ISNULL(
                                SUM(DATEDIFF(MINUTE, StartDate, EndDate))
                            , 0)
                        FROM PM_OrderResult
                        WHERE CorpId = @CorpId
                          AND WorkDate = @Today
                          AND StartDate IS NOT NULL
                          AND EndDate IS NOT NULL


                        -- 전체 비가동시간
                        SELECT
                            @TotalStopTime = ISNULL(
                                SUM(DATEDIFF(MINUTE, StartTime, EndTime))
                            , 0)
                        FROM EQ_Operation
                        WHERE CorpId = @CorpId
                          AND WorkDay = @Today
                          AND StartTime IS NOT NULL
                          AND EndTime IS NOT NULL


                        -- 금일 총 고장시간
                        SELECT
                            @TotalFailureTime = ISNULL(
                                SUM(
                                    CASE
                                        WHEN RepairStartTime IS NOT NULL
                                         AND RepairEndTime IS NOT NULL
                                        THEN DATEDIFF(MINUTE, RepairStartTime, RepairEndTime)
                                        ELSE 0
                                    END
                                )
                            , 0)
                        FROM EQ_Repair
                        WHERE CorpId = @CorpId
                          AND RepairDate >= CONVERT(VARCHAR(8), @Today, 112)
                          AND RepairDate < CONVERT(VARCHAR(8), DATEADD(DAY, 1, @Today), 112)


                        -- 금일 가동한 설비 수
                        SELECT
                            @RunningMachineCount = COUNT(DISTINCT MachineCode)
                        FROM PM_OrderResult
                        WHERE CorpId = @CorpId
                          AND WorkDate = @Today
                          AND StartDate IS NOT NULL
                          AND EndDate IS NOT NULL
                          AND DATEDIFF(MINUTE, StartDate, EndDate) > 0


                        -- 금일 비가동 설비 수
                        SELECT
                            @StopMachineCount = COUNT(DISTINCT MachineCode)
                        FROM EQ_Operation
                        WHERE CorpId = @CorpId
                          AND WorkDay = @Today
                          AND StartTime IS NOT NULL
                          AND EndTime IS NOT NULL
                          AND DATEDIFF(MINUTE, StartTime, EndTime) > 0


                        SELECT
                              @TotalMachineCount AS TotalMachineCount
                            , @RunningMachineCount AS RunningMachineCount
                            , @StopMachineCount AS StopMachineCount

                            -- 전체 가동률
                            , CAST(
                                CASE
                                    WHEN @TotalRunTime + @TotalStopTime = 0 THEN 0
                                    ELSE @TotalRunTime * 100.0 /
                                         (@TotalRunTime + @TotalStopTime)
                                END
                              AS DECIMAL(5,1)) AS OperatingRate

                            -- 총 고장시간
                            , CAST(@TotalFailureTime / 60 AS VARCHAR(10))
                              + '시간 '
                              + CAST(@TotalFailureTime % 60 AS VARCHAR(10))
                              + '분' AS TotalFailureTime
                    ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                lblTotalMachineCount.Text = dr["TotalMachineCount"].ToString() + "대";
                lblRunningMachineCount.Text = dr["RunningMachineCount"].ToString() + "대";
                lblStopMachineCount.Text = dr["StopMachineCount"].ToString() + "대";
                lblOperatRate.Text = dr["OperatingRate"].ToString() + "%";
                lblTotalRepairTime.Text = dr["TotalFailureTime"].ToString();
            }
        }
        #endregion

        #region 차트 로드
        private void Search_Chart_EquipmentRate()
        {
            // 당일 설비 종합 지표 추이
            query = $@"
        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
        DECLARE @Today DATE = CAST(GETDATE() AS DATE);

        ;WITH HourList AS
        (
            SELECT 0 AS HourNo
            UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3
            UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6
            UNION ALL SELECT 7 UNION ALL SELECT 8 UNION ALL SELECT 9
            UNION ALL SELECT 10 UNION ALL SELECT 11 UNION ALL SELECT 12
            UNION ALL SELECT 13 UNION ALL SELECT 14 UNION ALL SELECT 15
            UNION ALL SELECT 16 UNION ALL SELECT 17 UNION ALL SELECT 18
            UNION ALL SELECT 19 UNION ALL SELECT 20 UNION ALL SELECT 21
            UNION ALL SELECT 22 UNION ALL SELECT 23
        ),
        RunStats AS
        (
            SELECT
                DATEPART(HOUR, StartDate) AS HourNo,
                SUM(ISNULL(WorkQty, 0)) AS WorkQty,
                SUM(ISNULL(GoodQty, 0)) AS GoodQty,
                SUM(ISNULL(NgQty, 0)) AS NgQty,
                SUM(DATEDIFF(MINUTE, StartDate, EndDate)) AS RunTime
            FROM PM_OrderResult
            WHERE CorpId = @CorpId
              AND WorkDate = @Today
            GROUP BY DATEPART(HOUR, StartDate)
        ),
        StopStats AS
        (
            SELECT
                DATEPART(HOUR, StartTime) AS HourNo,
                SUM(DATEDIFF(MINUTE, StartTime, EndTime)) AS StopTime
            FROM EQ_Operation
            WHERE CorpId = @CorpId
              AND WorkDay = @Today
            GROUP BY DATEPART(HOUR, StartTime)
        )
        SELECT
            H.HourNo,
            CAST(H.HourNo AS VARCHAR(2)) + '시' AS WorkHour,
            CASE
                WHEN ISNULL(R.RunTime, 0) + ISNULL(S.StopTime, 0) = 0 THEN 0
                ELSE ROUND(
                    ISNULL(R.RunTime, 0) * 100.0 /
                    (ISNULL(R.RunTime, 0) + ISNULL(S.StopTime, 0)), 1
                )
            END AS OperatingRate,
            CASE
                WHEN ISNULL(R.WorkQty, 0) = 0 THEN 0
                ELSE ROUND(
                    ISNULL(R.GoodQty, 0) * 100.0 /
                    NULLIF(ISNULL(R.WorkQty, 0), 0), 1
                )
            END AS QualityRate,
            CASE
                WHEN ISNULL(R.RunTime, 0) + ISNULL(S.StopTime, 0) = 0
                  OR ISNULL(R.WorkQty, 0) = 0
                THEN 0
                ELSE ROUND(
                    (
                        ISNULL(R.RunTime, 0) * 100.0 /
                        (ISNULL(R.RunTime, 0) + ISNULL(S.StopTime, 0))
                    )
                    *
                    (
                        ISNULL(R.WorkQty, 0) * 100.0 /
                        NULLIF(ISNULL(R.WorkQty, 0), 0)
                    ) / 100.0, 1
                )
            END AS OEE
        FROM HourList H
        LEFT JOIN RunStats R ON H.HourNo = R.HourNo
        LEFT JOIN StopStats S ON H.HourNo = S.HourNo
        WHERE H.HourNo <= DATEPART(HOUR, GETDATE())
        ORDER BY H.HourNo;
    ";

            DataTable dt = db.GetDataTable(query);

            ChartEquipmentRate.DataSource = dt;
            ChartEquipmentRate.Series.Clear();
            ChartEquipmentRate.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ChartEquipmentRate.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            ChartEquipmentRate.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            ChartEquipmentRate.Legend.Direction = LegendDirection.LeftToRight;
            ChartEquipmentRate.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartEquipmentRate.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartEquipmentRate.BackColor = Color.White;

            // =============================================================
            // 가동률 : 막대
            // =============================================================

            Series s1 = new Series("가동률", ViewType.Bar);
            s1.ArgumentDataMember = "WorkHour";
            s1.ValueDataMembers.AddRange(new string[] { "OperatingRate" });
            s1.DataSource = dt;
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            s1.CrosshairLabelPattern = "{A}: {V:0.0}%";

            ChartEquipmentRate.Series.Add(s1);


            // =============================================================
            // OEE : 라인
            // =============================================================

            Series s2 = new Series("OEE", ViewType.Line);
            s2.ArgumentDataMember = "WorkHour";
            s2.ValueDataMembers.AddRange(new string[] { "OEE" });
            s2.DataSource = dt;
            s2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            s2.CrosshairLabelPattern = "{A}: {V:0.0}%";

            ChartEquipmentRate.Series.Add(s2);


            // =============================================================
            // 품질률 : 라인
            // =============================================================

            Series s3 = new Series("품질률", ViewType.Line);
            s3.ArgumentDataMember = "WorkHour";
            s3.ValueDataMembers.AddRange(new string[] { "QualityRate" });
            s3.DataSource = dt;
            s3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            s3.CrosshairLabelPattern = "{A}: {V:0.0}%";

            ChartEquipmentRate.Series.Add(s3);


            // =============================================================
            // 차트 설정
            // =============================================================

            XYDiagram diagram = ChartEquipmentRate.Diagram as XYDiagram;

            if (diagram != null)
            {
                diagram.AxisX.Label.TextPattern = "{A}";
                diagram.AxisY.Label.TextPattern = "{V:0}%";

                diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);
                diagram.AxisY.VisualRange.SetMinMaxValues(0, 100);
            }


            // =============================================================
            // 가동률 막대 디자인
            // =============================================================

            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(50, 140, 240);
            barView.BarWidth = 0.5;


            // =============================================================
            // OEE 라인 디자인
            // =============================================================

            LineSeriesView oeeView = (LineSeriesView)s2.View;
            oeeView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            oeeView.LineStyle.Thickness = 2;


            // =============================================================
            // 품질률 라인 디자인
            // =============================================================

            LineSeriesView qualityView = (LineSeriesView)s3.View;
            qualityView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            qualityView.LineStyle.Thickness = 2;


            // =============================================================
            // 범례 : 차트 우측 상단
            // =============================================================
            ChartEquipmentRate.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ChartEquipmentRate.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            ChartEquipmentRate.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            ChartEquipmentRate.Legend.Direction = LegendDirection.LeftToRight;
            ChartEquipmentRate.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        private void Search_Chart_EquipmentStatus()
        {
            // 설비 상태 현황 : 가동 / 정지
            query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                        DECLARE @Today DATE = CAST(GETDATE() AS DATE);

                        ;WITH MachineStatus AS
                        (
                            SELECT
                                M.MachineCode,
                                M.MachineName,
                                -- 당일 가동시간
                                ISNULL(R.TotalRunTime, 0) AS TotalRunTime,
                                -- 당일 정지시간
                                ISNULL(S.TotalStopTime, 0) AS TotalStopTime
                            FROM EQ_Machine M

                            -- =====================================================
                            -- 당일 설비별 가동시간
                            -- =====================================================
                            LEFT JOIN
                            (
                                SELECT
                                    CorpId,
                                    MachineCode,
                                    SUM(DATEDIFF(MINUTE, StartDate, EndDate)) AS TotalRunTime
                                FROM PM_OrderResult
                                WHERE CorpId = @CorpId
                                  AND WorkDate = @Today
                                GROUP BY CorpId, MachineCode
                            ) R ON M.CorpId = R.CorpId AND M.MachineCode = R.MachineCode

                            -- =====================================================
                            -- 당일 설비별 정지시간
                            -- =====================================================
                            LEFT JOIN
                            (
                                SELECT
                                    CorpId,
                                    MachineCode,
                                    SUM(DATEDIFF(MINUTE, StartTime, EndTime)) AS TotalStopTime
                                FROM EQ_Operation
                                WHERE CorpId = @CorpId AND WorkDay = @Today
                                GROUP BY CorpId, MachineCode
                            ) S ON M.CorpId = S.CorpId AND M.MachineCode = S.MachineCode
                            WHERE M.CorpId = @CorpId AND M.UseYn = 'Y'
                        )

                        -- =========================================================
                        -- 가동 / 정지 설비 대수
                        -- =========================================================
                        SELECT
                            CASE
                                WHEN TotalRunTime > 0 THEN '가동'
                                ELSE '정지'
                            END AS StatusName,

                            COUNT(*) AS StatusCount
                        FROM MachineStatus
                        GROUP BY
                            CASE
                                WHEN TotalRunTime > 0 THEN '가동'
                                ELSE '정지'
                            END
                        ORDER BY StatusName
                    ";

            DataTable dt = db.GetDataTable(query);

            ChartEquipmentStatus.DataSource = dt;

            ChartEquipmentStatus.Series.Clear();
            ChartEquipmentStatus.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ChartEquipmentStatus.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartEquipmentStatus.BackColor = Color.White;

            // =============================================================
            // 도넛 차트
            // =============================================================

            Series s1 = new Series("설비 상태", ViewType.Doughnut);

            s1.ArgumentDataMember = "StatusName";
            s1.ValueDataMembers.AddRange(
                new string[] { "StatusCount" }
            );

            s1.DataSource = dt;
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s1.Label.TextPattern = "{A}: {V}대";
            ChartEquipmentStatus.Series.Add(s1);


            // =============================================================
            // 도넛 디자인
            // =============================================================

            DoughnutSeriesView doughnutView = (DoughnutSeriesView)s1.View;
            doughnutView.HoleRadiusPercent = 65;
        }

        private void Search_Chart_EquipmentRate_Top5()
        {
            // 당일 설비별 가동률 TOP 5
            query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                        DECLARE @Today DATE = CAST(GETDATE() AS DATE);

                        ;WITH RunStats AS
                        (
                            SELECT
                                CorpId,
                                MachineCode,
                                SUM(DATEDIFF(MINUTE, StartDate, EndDate)) AS TotalRunTime
                            FROM PM_OrderResult
                            WHERE CorpId = @CorpId AND WorkDate = @Today
                            GROUP BY CorpId, MachineCode
                        ),
                        StopStats AS
                        (
                            SELECT
                                CorpId,
                                MachineCode,
                                SUM(DATEDIFF(MINUTE, StartTime, EndTime)) AS TotalStopTime
                            FROM EQ_Operation
                            WHERE CorpId = @CorpId AND WorkDay = @Today
                            GROUP BY CorpId, MachineCode
                        )
                        SELECT TOP 5
                            M.MachineCode,
                            M.MachineName,
                            CASE
                                WHEN ISNULL(R.TotalRunTime, 0) + ISNULL(S.TotalStopTime, 0) = 0 THEN 0
                                ELSE ROUND(
                                    ISNULL(R.TotalRunTime, 0) * 100.0 /
                                    (ISNULL(R.TotalRunTime, 0) + ISNULL(S.TotalStopTime, 0)), 1
                                )
                            END AS OperatingRate
                        FROM EQ_Machine M
                        LEFT JOIN RunStats R ON M.CorpId = R.CorpId AND M.MachineCode = R.MachineCode
                        LEFT JOIN StopStats S ON M.CorpId = S.CorpId AND M.MachineCode = S.MachineCode
                        WHERE M.CorpId = @CorpId AND M.UseYn = 'Y'
                        ORDER BY OperatingRate DESC;
                    ";

            DataTable dt = db.GetDataTable(query);

            ChartEquipmentRate_Top5.DataSource = dt;
            ChartEquipmentRate_Top5.Series.Clear();
            ChartEquipmentRate_Top5.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartEquipmentRate_Top5.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartEquipmentRate_Top5.BackColor = Color.White;

            Series s1 = new Series("설비별 가동률", ViewType.Bar);
            s1.ArgumentDataMember = "MachineName";
            s1.ValueDataMembers.AddRange(new string[] { "OperatingRate" });
            s1.DataSource = dt;
            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s1.Label.TextPattern = "{V:0.0}%";
            s1.CrosshairLabelPattern = "{A}: {V:0.0}%";

            ChartEquipmentRate_Top5.Series.Add(s1);

            XYDiagram diagram = ChartEquipmentRate_Top5.Diagram as XYDiagram;

            if (diagram != null)
            {
                //diagram.Rotated = true;
                //diagram.AxisX.Reverse = true;
                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;
                diagram.AxisY.Label.TextPattern = "{V:0}%";
                diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);
                diagram.AxisY.VisualRange.SetMinMaxValues(0, 100);
            }

            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;
            barView.Color = Color.FromArgb(50, 140, 240);
            barView.BarWidth = 0.4;
        }

        private void Search_Chart_MachineRepair()
        {
            // 최근 1개월 설비별 고장 건수
            query = $@"
                SELECT
                      er.MachineCode
                    , cm.MachineName
                    , COUNT(*) AS FailureCount
                FROM EQ_Repair er
                LEFT JOIN EQ_Machine cm ON er.CorpId = cm.CorpId AND er.MachineCode = cm.MachineCode
                WHERE er.CorpId = '{WrGlobal.CorpID}'
                  AND er.RepairDate >= CONVERT(VARCHAR(8), DATEADD(MONTH, -1, GETDATE()), 112)
                  AND er.RepairDate < CONVERT(VARCHAR(8), DATEADD(DAY, 1, GETDATE()), 112)
                GROUP BY er.MachineCode, cm.MachineName
                ORDER BY FailureCount DESC
            ";

            DataTable dt = db.GetDataTable(query);

            ChartMachineRepair.DataSource = dt;
            ChartMachineRepair.Series.Clear();
            ChartMachineRepair.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartMachineRepair.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartMachineRepair.BackColor = Color.White;

            Series s1 = new Series("설비별 고장 건수", ViewType.Bar);

            s1.ArgumentDataMember = "MachineName";
            s1.ValueDataMembers.AddRange(new string[] { "FailureCount" });
            s1.DataSource = dt;

            s1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s1.Label.TextPattern = "{V:0}건";
            s1.CrosshairLabelPattern = "{A}: {V:0}건";

            ChartMachineRepair.Series.Add(s1);

            XYDiagram diagram = ChartMachineRepair.Diagram as XYDiagram;

            if (diagram != null)
            {
                // 가로 막대 차트
                diagram.Rotated = true;

                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;

                // 고장 건수
                diagram.AxisY.Label.TextPattern = "{V:0}건";
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;
                diagram.AxisY.VisualRange.Auto = true;
            }

            SideBySideBarSeriesView barView = (SideBySideBarSeriesView)s1.View;

            barView.Color = Color.FromArgb(80, 150, 220);
            barView.BarWidth = 0.4;
        }

        private void Search_Chart_RepairCause()
        {
            // 당월 고장유형별 현황
            query = $@"
                SELECT
                      ISNULL(RepairCause, '기타') AS RepairCause
                    , COUNT(*) AS FailureCount
                FROM EQ_Repair
                WHERE CorpId = '{WrGlobal.CorpID}'
                  AND RepairDate >= CONVERT(VARCHAR(6), GETDATE(), 112) + '01'
                  AND RepairDate < CONVERT(VARCHAR(8), DATEADD(DAY, 1, GETDATE()), 112)
                GROUP BY
                    ISNULL(RepairCause, '기타')
                ORDER BY FailureCount DESC;
            ";

            DataTable dt = db.GetDataTable(query);

            ChartRepairCause.DataSource = dt;
            ChartRepairCause.Series.Clear();
            ChartRepairCause.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ChartRepairCause.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            ChartRepairCause.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
            ChartRepairCause.Legend.Direction = LegendDirection.TopToBottom;
            ChartRepairCause.Legend.Font = new Font("맑은 고딕", 9, FontStyle.Regular);

            ChartRepairCause.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartRepairCause.BackColor = Color.White;

            Series series = new Series("고장유형", ViewType.Doughnut);

            series.DataSource = dt;
            series.ArgumentDataMember = "RepairCause";
            series.ValueDataMembers.AddRange(new string[] { "FailureCount" });

            // 도넛 차트 라벨
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series.Label.TextPattern = "{A}: {V:0}건";

            // 마우스 오버
            series.CrosshairLabelPattern = "{A}: {V:0}건";

            ChartRepairCause.Series.Add(series);

            // 도넛 차트 설정
            DoughnutSeriesView doughnutView = (DoughnutSeriesView)series.View;

            doughnutView.HoleRadiusPercent = 60;

            // 중앙에 총 고장 건수 표시
            doughnutView.TotalLabel.Visible = true;
            doughnutView.TotalLabel.TextPattern = "총 {TV:0}건";
        }

        private void Search_Chart_StopType()
        {
            query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                        DECLARE @Today DATE = CAST(GETDATE() AS DATE);

                        SELECT
                              mo.ClassItemCode AS StopType
                            , CCI.Title AS StopTypeName
                            , SUM(ISNULL(mo.StopTime, 0)) AS StopTime
                        FROM EQ_Operation mo
                        LEFT JOIN CM_CateItem CCI  ON CCI.CorpID = mo.CorpId AND CCI.ClassItemCode = mo.ClassItemCode
                        INNER JOIN CM_CLASS CL ON CL.CorpID = CCI.CorpID AND CL.CLASS = CCI.CLASS AND CL.HIGHCLASS = '2030'
                        WHERE mo.CorpId = @CorpId AND mo.WorkDay = @Today
                        GROUP BY mo.ClassItemCode, CCI.Title
                        ORDER BY StopTime DESC
                    ";

            DataTable dt = db.GetDataTable(query);

            ChartStopType.DataSource = dt;
            ChartStopType.Series.Clear();
            ChartStopType.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ChartStopType.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            ChartStopType.BackColor = Color.White;

            //==========================================================
            // 도넛차트
            //==========================================================
            Series seriesStop = new Series("비가동 현황", ViewType.Doughnut);

            seriesStop.DataSource = dt;
            seriesStop.ArgumentDataMember = "StopTypeName";
            seriesStop.ValueDataMembers.AddRange(new string[] { "StopTime" });

            //==========================================================
            // 라벨
            //==========================================================
            seriesStop.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            seriesStop.Label.TextPattern = "{A}: {V}분";

            //==========================================================
            // 툴팁
            //==========================================================
            seriesStop.CrosshairLabelPattern = "{A}: {V}분";

            ChartStopType.Series.Add(seriesStop);

            //==========================================================
            // 도넛 설정
            //==========================================================
            DoughnutSeriesView doughnutView =
                (DoughnutSeriesView)seriesStop.View;

            doughnutView.HoleRadiusPercent = 60;

            //==========================================================
            // 범례
            //==========================================================
            ChartStopType.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            ChartStopType.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
            ChartStopType.Legend.Direction = LegendDirection.TopToBottom;
            ChartStopType.Legend.Font = new Font("맑은 고딕", 9, FontStyle.Regular);
        }

        private void Search_TileView_EquipmentRate()
        {
            query = $@"
                DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                DECLARE @Today DATE = CAST(GETDATE() AS DATE);

                DECLARE @RunTime BIGINT = 0;
                DECLARE @StopTime BIGINT = 0;

                -- 가동시간
                SELECT
                    @RunTime = ISNULL(SUM(DATEDIFF(MINUTE, StartDate, EndDate)), 0)
                FROM PM_OrderResult
                WHERE CorpId = @CorpId
                  AND WorkDate = @Today;

                -- 비가동시간
                SELECT
                    @StopTime = ISNULL(SUM(DATEDIFF(MINUTE, StartTime, EndTime)), 0)
                FROM EQ_Operation
                WHERE CorpId = @CorpId
                  AND WorkDay = @Today;

                SELECT
                      CAST(@RunTime / 60 AS VARCHAR(20)) + '시간' AS RunTime
                    , CAST(@StopTime / 60 AS VARCHAR(20)) + '시간' AS StopTime
                    , FORMAT(
                        CASE
                            WHEN @RunTime + @StopTime = 0 THEN 0
                            ELSE @RunTime * 100.0 / (@RunTime + @StopTime)
                        END
                      , '0.0') + '%' AS OperatingRate;
            ";

            DataTable dt = db.GetDataTable(query);

            if (dt.Rows.Count > 0)
            {
                lblRunTime.Text = dt.Rows[0]["RunTime"].ToString();
                lblStopTime.Text = dt.Rows[0]["StopTime"].ToString();
                lblOperatingRate.Text = dt.Rows[0]["OperatingRate"].ToString();
            }
        }

        #endregion

        #region 조회 쿼리 (그리드)

        private void SearchGrid_Work()
        {
            // 설비별 생산 현황
            query = $@"
                        SELECT
                              op.Status AS WorkStatus
                            , op.MachineCode
                            , cm.MachineName
                            , op.OrderNo
                            , op.OrderSeq
                            , op.ItemCode
                            , cmt.ItemName
                            , ISNULL(ors.GoodQty, 0) AS GoodQty
                            , ISNULL(ors.NgQty, 0) AS NgQty
                            , ors.WorkerID
                            , cu.EmployeeName
                            , ors.SerialNo
                            , ors.WorkDate
                            , ors.StartDate
                            , ors.EndDate
                            , CASE
                                  WHEN ors.StartDate IS NOT NULL
                                   AND ors.EndDate IS NOT NULL
                                  THEN DATEDIFF(MINUTE, ors.StartDate, ors.EndDate)
                                  ELSE 0
                              END AS WorkTime
                            , ors.CreId
                            , ors.CreIP
                            , ors.CreDt
                        FROM PM_OrderProcess op
                        LEFT JOIN PM_OrderResult ors ON op.OrderNo = ors.OrderNo AND op.OrderSeq = ors.OrderSeq
                        LEFT JOIN EQ_Machine cm ON op.CorpId = cm.CorpId AND op.MachineCode = cm.MachineCode
                        LEFT JOIN CM_Item cmt ON op.CorpId = cmt.CorpId AND op.ItemCode = cmt.ItemCode
                        LEFT JOIN SM_User cu ON ors.CorpId = cu.CorpId AND ors.WorkerID = cu.EmployeeId
                        WHERE op.CorpId = '1000'
                          AND op.MachineCode IS NOT NULL
                          AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                          AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                        ORDER BY ors.WorkDate DESC
                    ";

            Gc_Work.DataSource = db.GetDataTable(query);
            Gv_Work.BestFitColumns();
        }

        private void SearchGrid_Machine()
		{
			// 설비 고장 현황
			query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                        DECLARE @Today DATE = CAST(GETDATE() AS DATE);

                        SELECT
                               er.RepairNo
                            , er.MachineCode
                            , cm.MachineName
                            , CONVERT(VARCHAR(10), er.RepairDate, 23) AS RepairDate
                            , er.RePairCause
                            , er.RepairState
                            , er.RepairEmpNo
                            , CONVERT(VARCHAR(8), er.RepairStartTime, 108) AS RepairStartTime
                            , CONVERT(VARCHAR(8), er.RepairEndTime, 108) AS RepairEndTime
                            , CASE
                                WHEN er.RepairStartTime IS NULL
                                  OR er.RepairEndTime IS NULL
                                    THEN 0
                                ELSE DATEDIFF(MINUTE, er.RepairStartTime, er.RepairEndTime)
                              END AS RepairTime
                            , er.RepairCustCd
                            , er.RepairAmt
                            , er.Remark
                        FROM EQ_Repair er
                        LEFT JOIN EQ_Machine cm ON er.CorpId = cm.CorpId AND er.MachineCode = cm.MachineCode
                        WHERE er.CorpId = @CorpId
                          AND er.RepairDate >= CONVERT(VARCHAR(8), @Today, 112)
                          AND er.RepairDate < CONVERT(VARCHAR(8), DATEADD(DAY, 1, @Today), 112)
                        ORDER BY er.RepairDate DESC

                        ";

            DataTable dt = db.GetDataTable(query);
            Gc_Repair.DataSource = dt;
            Gv_Repair.BestFitColumns();
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