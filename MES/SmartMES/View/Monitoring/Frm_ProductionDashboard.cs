using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using SMT_MES.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using SMT_MES.View.Business;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.View.BM;

namespace SmartMES.View.Monitoring
{
    public partial class Frm_ProductionDashboard : Form
    {
        private readonly Database db = new Database();

        private readonly Color colorBackground = Color.FromArgb(246, 248, 251);
        private readonly Color colorCard = Color.White;
        private readonly Color colorBorder = Color.FromArgb(223, 229, 238);
        private readonly Color colorHeader = Color.FromArgb(248, 250, 253);
        private readonly Color colorText = Color.FromArgb(28, 32, 40);
        private readonly Color colorMuted = Color.FromArgb(102, 112, 133);
        private readonly Color colorPrimary = Color.FromArgb(0, 52, 102);
        private readonly Color colorTarget = Color.FromArgb(30, 112, 235);
        private readonly Color colorActual = Color.FromArgb(20, 174, 161);
        private readonly Color colorRed = Color.FromArgb(229, 57, 53);
        private readonly Color colorGreen = Color.FromArgb(90, 190, 70);
        private readonly Color colorGray = Color.FromArgb(150, 154, 162);

        private DateEdit dtpFrom;
        private DateEdit dtpTo;
        private SearchLookUpEdit cmbProcess;
        private SimpleButton btnSearch;
        private GridControl gcProduction;
        private BandedGridView gvProduction;
        private TableLayoutPanel chartLayout;
        private ChartControl chartDayInjection;
        private ChartControl chartDayAssembly;
        private ChartControl chartNightInjection;
        private ChartControl chartNightAssembly;
        private Label lblCurrentTime;
        private Label lblProductionDate;
        private Timer dashboardTimer;
        private DateTime lastHourlySearchHour;

        public Frm_ProductionDashboard()
        {
            InitializeComponent();
            //LoadProcessList();
            BuildView();
            SetDefaultCondition();
            SearchData();
            StartDashboardTimer();
        }

        private void BuildView()
        {
            SuspendLayout();

            Text = "생산계획 · 실적 대시보드";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1320, 820);
            MinimumSize = new Size(1160, 700);
            BackColor = colorBackground;
            Font = new Font("맑은 고딕", 9F);

            Controls.Clear();

            Panel titlePanel = BuildTitlePanel();
            Control conditionPanel = BuildConditionPanel();
            SplitContainer split = BuildContentPanel();

            Controls.Add(split);
            Controls.Add(conditionPanel);
            Controls.Add(titlePanel);

            ResumeLayout(false);
        }

        private Panel BuildTitlePanel()
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 62,
                BackColor = colorBackground,
                Padding = new Padding(18, 14, 18, 6)
            };

            Label title = new Label
            {
                AutoSize = true,
                Text = "생산계획 · 실적 대시보드",
                Font = new Font("맑은 고딕", 17F, FontStyle.Bold),
                ForeColor = colorText,
                Location = new Point(18, 15)
            };

            Panel timePanel = new Panel
            {
                Size = new Size(340, 44),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            lblCurrentTime = new Label
            {
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("맑은 고딕", 9.5F, FontStyle.Bold),
                ForeColor = colorText,
                TextAlign = ContentAlignment.MiddleRight
            };

            lblProductionDate = new Label
            {
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("맑은 고딕", 9.5F, FontStyle.Bold),
                ForeColor = colorText,
                TextAlign = ContentAlignment.MiddleRight
            };

            timePanel.Controls.Add(lblProductionDate);
            timePanel.Controls.Add(lblCurrentTime);
            timePanel.Location = new Point(panel.ClientSize.Width - timePanel.Width - 18, 11);
            panel.Resize += (s, e) => timePanel.Location = new Point(panel.ClientSize.Width - timePanel.Width - 18, 11);

            panel.Controls.Add(title);
            panel.Controls.Add(timePanel);
            return panel;
        }

        private Control BuildConditionPanel()
        {
            GroupControl group = new GroupControl
            {
                Dock = DockStyle.Top,
                Height = 80,
                Text = " ",
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default,
                Padding = new Padding(3)
            };
            group.AppearanceCaption.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            group.AppearanceCaption.Options.UseFont = true;

            Panel inner = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            group.Controls.Add(inner);

            LabelControl lblPeriod = new LabelControl
            {
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(8, 8),
                Padding = new Padding(4, 0, 0, 0),
                Size = new Size(63, 23),
                Text = "조회일자"
            };
            inner.Controls.Add(lblPeriod);

            dtpFrom = CreateDateEdit();
            dtpFrom.Location = new Point(78, 9);
            inner.Controls.Add(dtpFrom);

            LabelControl tilde = new LabelControl
            {
                AutoSizeMode = LabelAutoSizeMode.None,
                Location = new Point(203, 9),
                MaximumSize = new Size(7, 21),
                MinimumSize = new Size(7, 21),
                Size = new Size(7, 21),
                Text = "~"
            };
            inner.Controls.Add(tilde);

            dtpTo = CreateDateEdit();
            dtpTo.Location = new Point(216, 9);
            inner.Controls.Add(dtpTo);

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BM_SdOrder_MD));
            btnSearch = new SimpleButton
            {
                Location = new Point(346, 8),
                Name = "BtnSearch",
                Size = new Size(70, 23),
                Text = "조회",
                Image = ((System.Drawing.Image)(resources.GetObject("BtnSearch.ImageOptions.Image")))
            };
            btnSearch.Click += (s, e) => SearchData();
            inner.Controls.Add(btnSearch);

            cmbProcess = new SearchLookUpEdit
            {
                Location = new Point(416, 8),
                Size = new Size(121, 23),
                Visible = false
            };
            cmbProcess.Properties.AutoHeight = false;
            cmbProcess.Properties.NullText = "";
            cmbProcess.Properties.DisplayMember = "ProcessName";
            cmbProcess.Properties.ValueMember = "ProcessCode";
            cmbProcess.Properties.View.OptionsView.ShowGroupPanel = false;
            cmbProcess.Properties.View.OptionsView.ShowIndicator = false;
            inner.Controls.Add(cmbProcess);

            LoadProcessList();

            return group;
        }

        private SplitContainer BuildContentPanel()
        {
            SplitContainer split = new SplitContainer
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterWidth = 8,
                BackColor = colorBackground,
                Padding = new Padding(16, 12, 16, 16)
            };

            split.Panel1.Padding = new Padding(0, 0, 8, 0);
            split.Panel2.Padding = new Padding(8, 0, 0, 0);
            split.Resize += (s, e) =>
            {
                int distance = Math.Max(300, (split.Width - split.SplitterWidth) / 2);
                if (distance > 0 && distance < split.Width)
                {
                    split.SplitterDistance = distance;
                }
            };

            split.Panel1.Controls.Add(BuildGridCard());
            split.Panel2.Controls.Add(BuildChartCard());

            return split;
        }

        private Control BuildGridCard()
        {
            RoundedPanel card = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                FillColor = colorCard,
                BorderColor = colorBorder,
                Radius = 10,
                Padding = new Padding(1)
            };

            gcProduction = new GridControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("맑은 고딕", 9F)
            };

            gvProduction = new BandedGridView(gcProduction);
            gcProduction.MainView = gvProduction;
            gcProduction.ViewCollection.Add(gvProduction);
            gcProduction.DataSourceChanged += (s, e) => gvProduction.BestFitColumns();

            ConfigureGridView();
            card.Controls.Add(gcProduction);

            return card;
        }

        private Control BuildChartCard()
        {
            RoundedPanel card = new RoundedPanel
            {
                Dock = DockStyle.Fill,
                FillColor = colorCard,
                BorderColor = colorBorder,
                Radius = 10,
                Padding = new Padding(18, 16, 18, 16)
            };

            Label title = new Label
            {
                Dock = DockStyle.Top,
                Height = 34,
                Text = "종합목표달성률",
                Font = new Font("맑은 고딕", 13F, FontStyle.Bold),
                ForeColor = colorText,
                TextAlign = ContentAlignment.MiddleLeft
            };

            chartLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 10,
                BackColor = colorCard,
                Padding = new Padding(0, 4, 0, 0)
            };
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));

            chartDayInjection = CreateChart();
            chartDayAssembly = CreateChart();
            chartNightInjection = CreateChart();
            chartNightAssembly = CreateChart();

            DataTable dt = (DataTable)cmbProcess.Properties.DataSource;
            DataRow[] drProcess = dt.Select("ProcessCode <> 'ALL'");

            string processCode1 = drProcess[0]["ProcessName"].ConvertString_Null_To_Empty();
            string processCode2 = drProcess[1]["ProcessName"].ConvertString_Null_To_Empty();

            chartLayout.Controls.Add(CreateBandTitle("주간"), 0, 0);
            chartLayout.Controls.Add(CreateProcessTitle(processCode1), 0, 1);
            chartLayout.Controls.Add(chartDayInjection, 0, 2);
            chartLayout.Controls.Add(CreateProcessTitle(processCode2), 0, 3);
            chartLayout.Controls.Add(chartDayAssembly, 0, 4);
            chartLayout.Controls.Add(CreateBandTitle("야간"), 0, 5);
            chartLayout.Controls.Add(CreateProcessTitle(processCode1), 0, 6);
            chartLayout.Controls.Add(chartNightInjection, 0, 7);
            chartLayout.Controls.Add(CreateProcessTitle(processCode2), 0, 8);
            chartLayout.Controls.Add(chartNightAssembly, 0, 9);

            card.Controls.Add(chartLayout);
            card.Controls.Add(title);

            return card;
        }

        private void ConfigureGridView()
        {
            gvProduction.BeginUpdate();

            gvProduction.Bands.Clear();
            gvProduction.Columns.Clear();

            gvProduction.OptionsBehavior.Editable = false;
            gvProduction.OptionsBehavior.ReadOnly = true;
            gvProduction.OptionsView.ShowGroupPanel = false;
            gvProduction.OptionsView.ShowIndicator = false;
            gvProduction.OptionsView.ColumnAutoWidth = true;

            // 핵심: 컬럼 헤더 숨김
            gvProduction.OptionsView.ShowColumnHeaders = false;

            gvProduction.OptionsView.ShowBands = true;
            gvProduction.OptionsView.AllowCellMerge = true;
            gvProduction.OptionsSelection.EnableAppearanceFocusedCell = false;

            gvProduction.RowHeight = 44;
            gvProduction.BandPanelRowHeight = 38;
            gvProduction.ColumnPanelRowHeight = 38;

            gvProduction.Appearance.Row.Font = new Font("맑은 고딕", 10F);
            gvProduction.Appearance.Row.ForeColor = colorText;

            gvProduction.Appearance.HeaderPanel.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            gvProduction.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gvProduction.Appearance.HeaderPanel.BackColor = colorHeader;

            gvProduction.Appearance.BandPanel.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            gvProduction.Appearance.BandPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gvProduction.Appearance.BandPanel.BackColor = colorHeader;

            gvProduction.Appearance.FocusedRow.BackColor = Color.FromArgb(235, 243, 255);
            gvProduction.Appearance.FocusedRow.ForeColor = colorText;
            gvProduction.Appearance.SelectedRow.BackColor = Color.FromArgb(235, 243, 255);
            gvProduction.Appearance.SelectedRow.ForeColor = colorText;

            gvProduction.CustomDrawCell -= GvProduction_CustomDrawCell;
            gvProduction.RowCellStyle -= GvProduction_RowCellStyle;
            gvProduction.CellMerge -= GvProduction_CellMerge;

            gvProduction.CustomDrawCell += GvProduction_CustomDrawCell;
            gvProduction.RowCellStyle += GvProduction_RowCellStyle;
            gvProduction.CellMerge += GvProduction_CellMerge;

            // 1단 Band
            GridBand bandGroup = AddBand("구분", 72);
            GridBand bandMachine = AddBand("설비", 96);
            GridBand bandDay = AddBand("주간", 320);
            GridBand bandNight = AddBand("야간", 320);

            // 구분/설비는 하위 Band 없이 자기 제목만 크게 보이게
            bandGroup.AutoFillDown = true;
            bandMachine.AutoFillDown = true;

            // 구분, 설비 데이터 컬럼
            AddColumn(bandGroup, "GroupName", 72, false);
            AddColumn(bandMachine, "MachineName", 96, false);

            // 주간 하위 Band
            GridBand bandDayTarget = AddChildBand(bandDay, "목표", 76);
            GridBand bandDayInstruction = AddChildBand(bandDay, "지시", 76);
            GridBand bandDayActual = AddChildBand(bandDay, "실적", 76);
            GridBand bandDayRate = AddChildBand(bandDay, "달성률", 92);

            AddColumn(bandDayTarget, "DayTarget", 76, true);
            AddColumn(bandDayInstruction, "DayInstruction", 76, true);
            AddColumn(bandDayActual, "DayActual", 76, true);
            AddColumn(bandDayRate, "DayRateText", 92, false);

            // 야간 하위 Band
            GridBand bandNightTarget = AddChildBand(bandNight, "목표", 76);
            GridBand bandNightInstruction = AddChildBand(bandNight, "지시", 76);
            GridBand bandNightActual = AddChildBand(bandNight, "실적", 76);
            GridBand bandNightRate = AddChildBand(bandNight, "달성률", 92);

            AddColumn(bandNightTarget, "NightTarget", 76, true);
            AddColumn(bandNightInstruction, "NightInstruction", 76, true);
            AddColumn(bandNightActual, "NightActual", 76, true);
            AddColumn(bandNightRate, "NightRateText", 92, false);

            gvProduction.EndUpdate();
        }

        private GridBand AddBand(string caption, int width)
        {
            GridBand band = gvProduction.Bands.AddBand(caption);

            band.AppearanceHeader.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            band.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

            band.Width = width;
            band.MinWidth = width;

            //band.OptionsBand.AllowMove = false;
            //band.OptionsBand.AllowSize = false;

            return band;
        }

        private GridBand AddChildBand(GridBand parentBand, string caption, int width)
        {
            GridBand band = parentBand.Children.AddBand(caption);

            band.AppearanceHeader.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
            band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            band.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;

            band.Width = width;
            band.MinWidth = width;

            //band.OptionsBand.AllowMove = false;
            //band.OptionsBand.AllowSize = false;

            return band;
        }

        private void AddColumn(GridBand band, string fieldName, int width, bool numeric)
        {
            BandedGridColumn column = gvProduction.Columns.AddField(fieldName);

            column.Caption = "";
            column.FieldName = fieldName;

            band.Columns.Add(column);

            column.Visible = true;
            column.Width = width;
            column.MinWidth = width;

            // 핵심: BestFit이나 자동 조정으로 폭 줄어드는 것 방지
            //column.OptionsColumn.FixedWidth = true;

            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = DefaultBoolean.False;
            //column.OptionsColumn.AllowMove = false;
            //column.OptionsColumn.AllowSize = false;

            column.AppearanceCell.Font = new Font("맑은 고딕", 10F);
            column.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            if (numeric)
            {
                column.DisplayFormat.FormatType = FormatType.Numeric;
                column.DisplayFormat.FormatString = "N0";
            }
        }

        private void SetDefaultCondition()
        {
            DateTime today = DateTime.Now.Date;
            dtpFrom.DateTime = today;
            dtpTo.DateTime = today;
            cmbProcess.EditValue = "ALL";
        }

        private void StartDashboardTimer()
        {
            UpdateDashboardTime();
            lastHourlySearchHour = DateTime.Now.Date.AddHours(DateTime.Now.Hour);

            dashboardTimer = new Timer
            {
                Interval = 1000
            };
            dashboardTimer.Tick += DashboardTimer_Tick;
            dashboardTimer.Start();
        }

        private void DashboardTimer_Tick(object sender, EventArgs e)
        {
            UpdateDashboardTime();

            DateTime now = DateTime.Now;
            DateTime currentHour = now.Date.AddHours(now.Hour);
            if (now.Minute == 0 && now.Second <= 3 && lastHourlySearchHour != currentHour)
            {
                lastHourlySearchHour = currentHour;
                SearchData();
            }
        }

        private void UpdateDashboardTime()
        {
            DateTime now = DateTime.Now;
            if (lblCurrentTime != null)
            {
                lblCurrentTime.Text = "현재 시간 : " + now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            if (lblProductionDate != null)
            {
                lblProductionDate.Text = "생산일자 : " + now.ToString("yyyy-MM-dd");
            }
        }

        private void LoadProcessList()
        {
            string query = $@"
                                SELECT 
                                      'ALL' AS ProcessCode
                                    , '전체' AS ProcessName
                                    , 0 AS SortSeq

                                UNION ALL
                                SELECT
                                      P.ProcessCode
                                    , P.ProcessName
                                    , CASE 
                                        WHEN P.ProcessCode = 'PR001' THEN 1
                                        WHEN P.ProcessCode = 'PR002' THEN 2
                                        ELSE 9
                                      END AS SortSeq
                                FROM CM_Process P
                                WHERE P.CorpId = '{WrGlobal.CorpID}'
                                    AND ISNULL(P.UseYn, 'Y') = 'Y'
                                ORDER BY SortSeq, ProcessCode;
                            ";

            DataTable processTable = db.GetDataTable(query);

            cmbProcess.Properties.DataSource = processTable;

            cmbProcess.Properties.View.Columns.Clear();

            cmbProcess.Properties.View.Columns.AddVisible("ProcessCode", "코드");
            cmbProcess.Properties.View.Columns.AddVisible("ProcessName", "공정");

            cmbProcess.Properties.ValueMember = "ProcessCode";
            cmbProcess.Properties.DisplayMember = "ProcessName";
        }

        private void SearchData()
        {
            if (dtpFrom.DateTime.Date > dtpTo.DateTime.Date)
            {
                XtraMessageBox.Show("조회 시작일은 종료일보다 클 수 없습니다.", "조회", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable gridData = BuildGridQuery();
                PrepareGridData(gridData);
                gcProduction.DataSource = gridData;
                gvProduction.BestFitColumns();

                DataTable chartData = BuildChartQuery();
                BindCharts(chartData);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("생산계획 · 실적 대시보드 조회 중 오류가 발생했습니다.\r\n" + ex.Message, "조회 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrepareGridData(DataTable table)
        {
            if (!table.Columns.Contains("DayRateText")) table.Columns.Add("DayRateText", typeof(string));
            if (!table.Columns.Contains("NightRateText")) table.Columns.Add("NightRateText", typeof(string));
            if (!table.Columns.Contains("DayRate")) table.Columns.Add("DayRate", typeof(decimal));
            if (!table.Columns.Contains("NightRate")) table.Columns.Add("NightRate", typeof(decimal));

            foreach (DataRow row in table.Rows)
            {
                decimal dayInstruction = row["DayInstruction"].ConvertDecimal_Null_To_Zero();
                decimal dayActual = row["DayActual"].ConvertDecimal_Null_To_Zero();
                decimal nightInstruction = row["NightInstruction"].ConvertDecimal_Null_To_Zero();
                decimal nightActual = row["NightActual"].ConvertDecimal_Null_To_Zero();

                decimal? dayRate = CalcGridRate(dayInstruction, dayActual);
                decimal? nightRate = CalcGridRate(nightInstruction, nightActual);

                row["DayRateText"] = FormatRate(dayRate);
                row["NightRateText"] = FormatRate(nightRate);
                row["DayRate"] = dayRate.HasValue ? dayRate.Value : -1M;
                row["NightRate"] = nightRate.HasValue ? nightRate.Value : -1M;
            }
        }

        private DataTable BuildGridQuery()
        {
            string fromDate = dtpFrom.DateTime.ConvertDate_To_Char8();
            string toDate = dtpTo.DateTime.ConvertDate_To_Char8();
            string processCd = cmbProcess.EditValue.ConvertString_Null_To_Empty();

            string query = $@"
        DECLARE @CorpId VARCHAR(10) = '{WrGlobal.CorpID}';
        DECLARE @FromDate CHAR(8) = '{fromDate}';
        DECLARE @ToDate CHAR(8) = '{toDate}';
        DECLARE @ProcessCode VARCHAR(15) = '{processCd}';

        DECLARE @Today CHAR(8) = CONVERT(CHAR(8), GETDATE(), 112);
        DECLARE @TodayDate DATE = CONVERT(DATE, @Today, 112);
        DECLARE @Now DATETIME = GETDATE();

        ;WITH MachineBase AS
        (
            SELECT
                  M.CorpId
                , M.MachineCode
                , M.MachineName
                , M.MachineType AS ProcessCode
                , M.Remark AS SetPlace

            FROM EQ_Machine M

            WHERE M.CorpId = @CorpId
                AND ISNULL(M.UseYn, 'Y') = 'Y'

                AND (
                    @ProcessCode = 'ALL'
                    OR M.MachineType = @ProcessCode
                )
        ),

        CmdBase AS
        (
            SELECT
                  P.CorpId

                , P.OrderDate

                , P.ProcessCode
                , P.MachineCode

                /*
                    주간 / 야간
                */
                , CASE
                    WHEN DATEPART(HOUR, P.StartDate) >= 17
                    THEN '20'
                    ELSE '10'
                  END AS WorkBand

                , ISNULL(P.OrderQty, 0) AS CmdQty

                /*
                    공정 CT
                */
                , ISNULL(C.CycleTime, 0) AS CT

                /*
                    경과시간
                */
                , CASE
                    WHEN P.OrderDate < @Today
                    THEN
                        CASE
                            WHEN DATEPART(HOUR, P.StartDate) >= 17
                            THEN 54000
                            ELSE 32400
                        END

                    WHEN P.OrderDate > @Today
                    THEN 0

                    WHEN DATEPART(HOUR, P.StartDate) >= 17
                    THEN
                        CASE
                            WHEN DATEDIFF(
                                    SECOND,
                                    DATEADD(HOUR, 17, CAST(@TodayDate AS DATETIME)),
                                    @Now
                                 ) < 0
                            THEN 0

                            WHEN DATEDIFF(
                                    SECOND,
                                    DATEADD(HOUR, 17, CAST(@TodayDate AS DATETIME)),
                                    @Now
                                 ) > 54000
                            THEN 54000

                            ELSE DATEDIFF(
                                    SECOND,
                                    DATEADD(HOUR, 17, CAST(@TodayDate AS DATETIME)),
                                    @Now
                                 )
                        END

                    ELSE
                        CASE
                            WHEN DATEDIFF(
                                    SECOND,
                                    DATEADD(HOUR, 8, CAST(@TodayDate AS DATETIME)),
                                    @Now
                                 ) < 0
                            THEN 0

                            WHEN DATEDIFF(
                                    SECOND,
                                    DATEADD(HOUR, 8, CAST(@TodayDate AS DATETIME)),
                                    @Now
                                 ) > 32400
                            THEN 32400

                            ELSE DATEDIFF(
                                    SECOND,
                                    DATEADD(HOUR, 8, CAST(@TodayDate AS DATETIME)),
                                    @Now
                                 )
                        END
                  END AS ElapsedSeconds

            FROM PM_OrderProcess P

            LEFT JOIN CM_Process C
                ON C.CorpId = P.CorpId
                AND C.ProcessCode = P.ProcessCode

            WHERE P.CorpId = @CorpId
                AND P.OrderDate BETWEEN @FromDate AND @ToDate

                AND (
                    @ProcessCode = 'ALL'
                    OR P.ProcessCode = @ProcessCode
                )
        ),

        CmdCalc AS
        (
            SELECT
                  C.CorpId
                , C.ProcessCode
                , C.MachineCode
                , C.WorkBand
                , C.CmdQty

                , CASE
                    WHEN C.CmdQty <= 0
                    THEN 0

                    WHEN C.CT <= 0
                    THEN C.CmdQty

                    WHEN FLOOR(
                            C.ElapsedSeconds / NULLIF(C.CT, 0)
                         ) > C.CmdQty
                    THEN C.CmdQty

                    ELSE FLOOR(
                            C.ElapsedSeconds / NULLIF(C.CT, 0)
                         )
                  END AS InstructionQty

            FROM CmdBase C
        ),

        CmdByMachine AS
        (
            SELECT
                  C.CorpId
                , C.MachineCode
                , C.ProcessCode

                , SUM(
                    CASE
                        WHEN C.WorkBand = '10'
                        THEN C.CmdQty
                        ELSE 0
                    END
                  ) AS DayTarget

                , SUM(
                    CASE
                        WHEN C.WorkBand = '10'
                        THEN C.InstructionQty
                        ELSE 0
                    END
                  ) AS DayInstruction

                , SUM(
                    CASE
                        WHEN C.WorkBand = '20'
                        THEN C.CmdQty
                        ELSE 0
                    END
                  ) AS NightTarget

                , SUM(
                    CASE
                        WHEN C.WorkBand = '20'
                        THEN C.InstructionQty
                        ELSE 0
                    END
                  ) AS NightInstruction

            FROM CmdCalc C

            GROUP BY
                  C.CorpId
                , C.MachineCode
                , C.ProcessCode
        ),

        ResultByMachine AS
        (
            SELECT
                  R.CorpId
                , R.MachineCode
                , R.ProcessCode

                , SUM(
                    CASE
                        WHEN R.StartDate IS NOT NULL
                         AND CONVERT(CHAR(5), R.StartDate, 108) < '17:00'
                        THEN ISNULL(R.WorkQty, 0)
                        ELSE 0
                    END
                  ) AS DayActual

                , SUM(
                    CASE
                        WHEN R.StartDate IS NULL
                          OR CONVERT(CHAR(5), R.StartDate, 108) >= '17:00'
                        THEN ISNULL(R.WorkQty, 0)
                        ELSE 0
                    END
                  ) AS NightActual

            FROM PM_OrderResult R

            WHERE R.CorpId = @CorpId
                AND R.WorkDate BETWEEN @FromDate AND @ToDate

                AND (
                    @ProcessCode = 'ALL'
                    OR R.ProcessCode = @ProcessCode
                )

            GROUP BY
                  R.CorpId
                , R.MachineCode
                , R.ProcessCode
        )

        SELECT
              X.GroupName
            , X.MachineName

            , X.DayTarget
            , X.DayInstruction
            , X.DayActual

            , X.NightTarget
            , X.NightInstruction
            , X.NightActual

        FROM
        (
            SELECT
                  CASE
                    WHEN MB.SetPlace LIKE '%A동%' THEN 1
                    WHEN MB.SetPlace LIKE '%B동%' THEN 2
                    WHEN MB.SetPlace LIKE '%D동%' THEN 3
                    ELSE 8
                  END AS Sort1

                , MB.MachineCode AS Sort2

                , CASE
                    WHEN MB.SetPlace LIKE '%A동%' THEN 'A동'
                    WHEN MB.SetPlace LIKE '%B동%' THEN 'B동'
                    WHEN MB.SetPlace LIKE '%D동%' THEN 'D동'
                    ELSE ISNULL(MB.SetPlace, '-')
                  END AS GroupName

                , MB.MachineName

                , ISNULL(CM.DayTarget, 0) AS DayTarget
                , ISNULL(CM.DayInstruction, 0) AS DayInstruction
                , ISNULL(RM.DayActual, 0) AS DayActual

                , ISNULL(CM.NightTarget, 0) AS NightTarget
                , ISNULL(CM.NightInstruction, 0) AS NightInstruction
                , ISNULL(RM.NightActual, 0) AS NightActual

            FROM MachineBase MB

            LEFT JOIN CmdByMachine CM
                ON CM.CorpId = MB.CorpId
                AND CM.MachineCode = MB.MachineCode
               -- AND CM.ProcessCode = MB.ProcessCode

            LEFT JOIN ResultByMachine RM
                ON RM.CorpId = MB.CorpId
                AND RM.MachineCode = MB.MachineCode
               -- AND RM.ProcessCode = MB.ProcessCode
        ) X

        ORDER BY
              X.Sort1
            , X.Sort2;
    ";

            return db.GetDataTable(query);
        }

        private DataTable BuildChartQuery()
        {
            string processCd = cmbProcess.EditValue.ConvertString_Null_To_Empty();
            string toDate = dtpTo.DateTime.ConvertDate_To_Char8();

            string query = $@"
        DECLARE @Corp_Cd VARCHAR(10) = '{WrGlobal.CorpID}';
        DECLARE @Process_Cd VARCHAR(5) = '{processCd}';
        DECLARE @BaseDate DATE = CONVERT(DATE, '{toDate}', 112);

        DECLARE @Today CHAR(8) = CONVERT(CHAR(8), @BaseDate, 112);
        DECLARE @TodayDate DATE = @BaseDate;

        DECLARE @Now DATETIME =
            CASE
                WHEN CONVERT(CHAR(8), GETDATE(), 112) = @Today
                    THEN GETDATE()
                ELSE DATEADD(SECOND, 86399, CAST(@TodayDate AS DATETIME))
            END;

        ;WITH DateBase AS
        (
            SELECT 0 AS DaySeq
                 , CONVERT(CHAR(8), DATEADD(DAY, -2, @BaseDate), 112) AS WorkDate
                 , 'D-2' AS WorkDateName

            UNION ALL

            SELECT 1
                 , CONVERT(CHAR(8), DATEADD(DAY, -1, @BaseDate), 112)
                 , 'D-1'

            UNION ALL

            SELECT 2
                 , CONVERT(CHAR(8), @BaseDate, 112)
                 , 'D-0'
        ),
        ProcessBase AS
        (
            SELECT
                  ProcessCode
                , ProcessName
                , ROW_NUMBER() OVER(ORDER BY ProcessCode) AS Sort2
            FROM CM_Process
            WHERE UseYn = 'Y'
        ),
        CmdBase AS
        (
            SELECT
                  op.CorpId
                , CONVERT(CHAR(8), op.OrderDate, 112) AS WorkDate
                , op.ProcessCode

                , CASE
                    WHEN DATEPART(HOUR, op.StartDate) >= 17 THEN '20'
                    ELSE '10'
                  END AS WorkBand

                , ISNULL(op.OrderQty, 0) AS CmdQty
                , ISNULL(cp.CycleTime, 0) AS CT

                , CASE
                    WHEN CONVERT(CHAR(8), op.OrderDate, 112) < @Today
                        THEN CASE
                                WHEN DATEPART(HOUR, op.StartDate) >= 17 THEN 54000
                                ELSE 32400
                             END

                    WHEN CONVERT(CHAR(8), op.OrderDate, 112) > @Today
                        THEN 0

                    WHEN DATEPART(HOUR, op.StartDate) >= 17
                        THEN
                            CASE
                                WHEN DATEDIFF(SECOND, DATEADD(HOUR, 17, CAST(@TodayDate AS DATETIME)), @Now) < 0 THEN 0
                                WHEN DATEDIFF(SECOND, DATEADD(HOUR, 17, CAST(@TodayDate AS DATETIME)), @Now) > 54000 THEN 54000
                                ELSE DATEDIFF(SECOND, DATEADD(HOUR, 17, CAST(@TodayDate AS DATETIME)), @Now)
                            END

                    ELSE
                        CASE
                            WHEN DATEDIFF(SECOND, DATEADD(HOUR, 8, CAST(@TodayDate AS DATETIME)), @Now) < 0 THEN 0
                            WHEN DATEDIFF(SECOND, DATEADD(HOUR, 8, CAST(@TodayDate AS DATETIME)), @Now) > 32400 THEN 32400
                            ELSE DATEDIFF(SECOND, DATEADD(HOUR, 8, CAST(@TodayDate AS DATETIME)), @Now)
                        END
                  END AS ElapsedSeconds

            FROM PM_OrderProcess op

            INNER JOIN DateBase db
                ON db.WorkDate = CONVERT(CHAR(8), op.OrderDate, 112)

            LEFT JOIN CM_Process cp
                ON cp.CorpId = op.CorpId
               AND cp.ProcessCode = op.ProcessCode

            WHERE op.CorpId = @Corp_Cd
              AND op.ProcessCode IN ('PR001', 'PR002')
              AND (@Process_Cd = 'ALL' OR op.ProcessCode = @Process_Cd)
        ),
        CmdAgg AS
        (
            SELECT
                  cb.WorkDate
                , cb.ProcessCode
                , cb.WorkBand

                , SUM(cb.CmdQty) AS TargetQty

                , SUM(
                    CASE
                        WHEN cb.CmdQty <= 0 THEN 0
                        WHEN cb.CT <= 0 THEN cb.CmdQty

                        WHEN FLOOR(cb.ElapsedSeconds / NULLIF(cb.CT, 0)) > cb.CmdQty
                            THEN cb.CmdQty

                        ELSE FLOOR(cb.ElapsedSeconds / NULLIF(cb.CT, 0))
                    END
                  ) AS InstructionQty

            FROM CmdBase cb

            GROUP BY
                  cb.WorkDate
                , cb.ProcessCode
                , cb.WorkBand
        ),
        ResultAgg AS
        (
            SELECT
                  CONVERT(CHAR(8), pr.WorkDate, 112) AS WorkDate
                , pr.ProcessCode

                , CASE
                    WHEN pr.StartDate IS NOT NULL
                     AND CONVERT(CHAR(5), pr.StartDate, 108) < '17:00'
                        THEN '10'
                    ELSE '20'
                  END AS WorkBand

                , SUM(ISNULL(pr.WorkQty, 0)) AS ActualQty

            FROM PM_OrderResult pr

            INNER JOIN DateBase db
                ON db.WorkDate = CONVERT(CHAR(8), pr.WorkDate, 112)

            WHERE pr.CorpId = @Corp_Cd
              AND pr.ProcessCode IN ('PR001', 'PR002')
              AND (@Process_Cd = 'ALL' OR pr.ProcessCode = @Process_Cd)

            GROUP BY
                  CONVERT(CHAR(8), pr.WorkDate, 112)
                , pr.ProcessCode
                , CASE
                    WHEN pr.StartDate IS NOT NULL
                     AND CONVERT(CHAR(5), pr.StartDate, 108) < '17:00'
                        THEN '10'
                    ELSE '20'
                  END
        )

        SELECT
              db.DaySeq
            , db.WorkDate
            , db.WorkDateName

            , SUBSTRING(db.WorkDate, 5, 2)
              + '-'
              + SUBSTRING(db.WorkDate, 7, 2) AS WorkDateLabel

            , CASE wb.WorkBand
                WHEN '10' THEN '주간'
                ELSE '야간'
              END AS WorkBandName

            , pb.ProcessName

            , ISNULL(ca.TargetQty, 0) AS TargetQty
            , ISNULL(ca.InstructionQty, 0) AS InstructionQty
            , ISNULL(ra.ActualQty, 0) AS ActualQty

        FROM DateBase db

        CROSS JOIN ProcessBase pb

        CROSS JOIN
        (
            SELECT '10' AS WorkBand
            UNION ALL
            SELECT '20'
        ) wb

        LEFT JOIN CmdAgg ca
               ON ca.WorkDate = db.WorkDate
              AND ca.ProcessCode = pb.ProcessCode
              AND ca.WorkBand = wb.WorkBand

        LEFT JOIN ResultAgg ra
               ON ra.WorkDate = db.WorkDate
              AND ra.ProcessCode = pb.ProcessCode
              AND ra.WorkBand = wb.WorkBand

        WHERE @Process_Cd = 'ALL'
           OR pb.ProcessCode = @Process_Cd

        ORDER BY
              CASE wb.WorkBand WHEN '10' THEN 1 ELSE 2 END
            , pb.Sort2
            , db.DaySeq;";

            return db.GetDataTable(query);
        }

        private void BindCharts(DataTable table)
        {
            DataTable dt = (DataTable)cmbProcess.Properties.DataSource;
            if (dt == null) return;

            DataRow[] drProcess = dt.Select("ProcessCode <> 'ALL'");

            string processCode1 = drProcess[0]["ProcessName"].ConvertString_Null_To_Empty();
            string processCode2 = drProcess[1]["ProcessName"].ConvertString_Null_To_Empty();

            BindChart(chartDayInjection, table, "주간", processCode1);
            BindChart(chartDayAssembly, table, "주간", processCode2);
            BindChart(chartNightInjection, table, "야간", processCode1);
            BindChart(chartNightAssembly, table, "야간", processCode2);
        }

        private void BindChart(ChartControl chart, DataTable table, string band, string process)
        {
            chart.Series.Clear();
            chart.Titles.Clear();

            // 범위 밖 데이터 툴팁 표시 허용
            chart.CrosshairOptions.ShowOutOfRangePoints = true;

            Series targetSeries = new Series("목표", ViewType.Bar);
            Series actualSeries = new Series("실적", ViewType.Bar);
            Series rateSeries = new Series("달성률(%)", ViewType.Line);

            // X축을 문자 항목 축으로 강제
            targetSeries.ArgumentScaleType = ScaleType.Qualitative;
            actualSeries.ArgumentScaleType = ScaleType.Qualitative;
            rateSeries.ArgumentScaleType = ScaleType.Qualitative;

            targetSeries.ValueScaleType = ScaleType.Numerical;
            actualSeries.ValueScaleType = ScaleType.Numerical;
            rateSeries.ValueScaleType = ScaleType.Numerical;

            DataRow[] rows = table.Select(
                $"WorkBandName = '{band}' AND ProcessName = '{process}'",
                "DaySeq ASC"
            );

            foreach (DataRow row in rows)
            {
                string argument = "";

                if (row.Table.Columns.Contains("WorkDateLabel"))
                {
                    argument = Convert.ToString(row["WorkDateLabel"]).Trim();
                }

                // WorkDateLabel 값이 비어 있으면 DaySeq나 ProcessName으로 대체
                if (string.IsNullOrWhiteSpace(argument))
                {
                    if (row.Table.Columns.Contains("DaySeq"))
                        argument = Convert.ToString(row["DaySeq"]).Trim();
                    else
                        argument = process;
                }

                decimal target = row["TargetQty"].ConvertDecimal_Null_To_Zero();

                decimal instruction = row.Table.Columns.Contains("InstructionQty")
                    ? row["InstructionQty"].ConvertDecimal_Null_To_Zero()
                    : target;

                decimal actual = row["ActualQty"].ConvertDecimal_Null_To_Zero();

                decimal rate = instruction > 0
                    ? Math.Round(actual / instruction * 100M, 1)
                    : 0M;

                targetSeries.Points.Add(new SeriesPoint(argument, target));
                actualSeries.Points.Add(new SeriesPoint(argument, actual));
                rateSeries.Points.Add(new SeriesPoint(argument, rate));
            }

            targetSeries.View.Color = colorTarget;
            actualSeries.View.Color = colorActual;
            rateSeries.View.Color = colorMuted;

            targetSeries.LabelsVisibility = DefaultBoolean.True;
            actualSeries.LabelsVisibility = DefaultBoolean.True;
            rateSeries.LabelsVisibility = DefaultBoolean.True;

            targetSeries.Label.TextPattern = "{V:N0}";
            actualSeries.Label.TextPattern = "{V:N0}";
            rateSeries.Label.TextPattern = "{V:0.#}%";

            LineSeriesView rateView = rateSeries.View as LineSeriesView;
            if (rateView != null)
            {
                rateView.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dash;
                rateView.MarkerVisibility = DefaultBoolean.True;
                rateView.LineMarkerOptions.Size = 8;
            }

            chart.Series.Add(targetSeries);
            chart.Series.Add(actualSeries);
            chart.Series.Add(rateSeries);

            XYDiagram diagram = chart.Diagram as XYDiagram;
            if (diagram != null)
            {
                diagram.SecondaryAxesY.Clear();

                SecondaryAxisY rateAxis = new SecondaryAxisY("달성률(%)");
                rateAxis.Alignment = AxisAlignment.Far;
                rateAxis.Label.TextPattern = "{V:0}%";

                // 자동 범위 계산 끄기 (명시한 값으로만 고정)
                rateAxis.WholeRange.Auto = false;
                rateAxis.VisualRange.Auto = false;

                // 자동 여백 끄기
                rateAxis.WholeRange.AutoSideMargins = false;
                rateAxis.VisualRange.AutoSideMargins = false;

                rateAxis.WholeRange.SetMinMaxValues(0, 100);
                rateAxis.VisualRange.SetMinMaxValues(0, 100);

                // 눈금 간격 설정
                rateAxis.NumericScaleOptions.AutoGrid = false;
                rateAxis.NumericScaleOptions.GridSpacing = 25;
                diagram.SecondaryAxesY.Add(rateAxis);

                XYDiagramSeriesViewBase rateDiagramView = rateSeries.View as XYDiagramSeriesViewBase;
                if (rateDiagramView != null)
                {
                    rateDiagramView.AxisY = rateAxis;
                }

                // X축 라벨 강제 표시
                diagram.AxisX.Visibility = DefaultBoolean.True;
                diagram.AxisX.Label.Visible = true;
                diagram.AxisX.Label.TextPattern = "{A}";

                // 항목명 많을 때 자동 숨김 방지
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;

                // 날짜/항목이 많으면 0도보다 -45도가 덜 잘림
                diagram.AxisX.Label.Angle = 0;
                diagram.AxisX.Label.Staggered = false;

                // 모든 항목 라벨 표시 유도
                diagram.AxisX.QualitativeScaleOptions.AutoGrid = false;
                diagram.AxisX.QualitativeScaleOptions.GridSpacing = 1;

                diagram.AxisX.Tickmarks.MinorVisible = false;

                diagram.AxisY.Label.TextPattern = "{V:N0}";
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;
                diagram.AxisY.GridLines.Color = Color.FromArgb(235, 238, 244);
            }
        }

        private ChartControl CreateChart()
        {
            ChartControl chart = new ChartControl
            {
                Dock = DockStyle.Fill,
                BorderOptions = { Visibility = DefaultBoolean.False },
                BackColor = colorCard
            };

            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            chart.Legend.Font = new Font("맑은 고딕", 8F);
            chart.Legend.Visibility = DefaultBoolean.True;

            return chart;
        }

        private Label CreateBandTitle(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Text = text,
                Font = new Font("맑은 고딕", 10F, FontStyle.Bold),
                ForeColor = colorText,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private Label CreateProcessTitle(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Text = text,
                Font = new Font("맑은 고딕", 9F, FontStyle.Bold),
                ForeColor = colorText,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(0, 2, 0, 0)
            };
        }

        private DateEdit CreateDateEdit()
        {
            DateEdit edit = new DateEdit();

            edit.Margin = new Padding(0);
            edit.EditValue = null;

            // 자동 높이 조절 끄기
            edit.Properties.AutoHeight = false;

            // 날짜 포맷
            edit.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            edit.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";

            edit.Properties.EditFormat.FormatType = FormatType.DateTime;
            edit.Properties.EditFormat.FormatString = "yyyy-MM-dd";

            edit.Properties.Mask.EditMask = "yyyy-MM-dd";
            edit.Properties.Mask.UseMaskAsDisplayFormat = true;

            // 버튼은 기본 Combo 버튼 1개만 사용
            edit.Properties.Buttons.Clear();
            edit.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Combo));

            // 높이/크기는 마지막에 지정
            edit.Size = new Size(120, 23);
            edit.MinimumSize = new Size(120, 23);
            edit.MaximumSize = new Size(120, 23);

            // 부모 컨트롤이 늘리지 못하게
            edit.Dock = DockStyle.None;
            edit.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            return edit;

            //DateEdit edit = new DateEdit
            //{
            //    EditValue = null,
            //    Margin = new Padding(0),
            //    Size = new Size(120, 23)
            //};

            //edit.Properties.AutoHeight = false;

            //edit.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            //edit.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";

            //edit.Properties.EditFormat.FormatType = FormatType.DateTime;
            //edit.Properties.EditFormat.FormatString = "yyyy-MM-dd";

            //edit.Properties.Mask.EditMask = "yyyy-MM-dd";
            //edit.Properties.Mask.UseMaskAsDisplayFormat = true;

            //return edit;
        }

        private Control CreateEditorBox(BaseEdit editor, int x, int y, int width, int height)
        {
            editor.Height = height - 8;

            RoundedPanel box = new RoundedPanel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = colorCard,
                FillColor = Color.White,
                BorderColor = Color.FromArgb(198, 207, 219),
                Radius = 8,
                Padding = new Padding(10, 4, 8, 4)
            };
            box.Controls.Add(editor);
            return box;
        }

        private Label CreateConditionLabel(string text)
        {
            return new Label
            {
                AutoSize = true,
                Text = text,
                Font = new Font("맑은 고딕", 12F, FontStyle.Bold),
                ForeColor = colorText
            };
        }

        private void ArrangeConditionControls(Control parent, params Control[] controls)
        {
            int[] gaps = { 14, 10, 10, 10 };
            int totalWidth = 0;
            for (int i = 0; i < controls.Length; i++)
            {
                totalWidth += controls[i].Width;
                if (i < gaps.Length)
                {
                    totalWidth += gaps[i];
                }
            }

            int x = 18;
            for (int i = 0; i < controls.Length; i++)
            {
                int y = Math.Max(0, (parent.ClientSize.Height - controls[i].Height) / 2);
                controls[i].Location = new Point(x, y);
                x += controls[i].Width + (i < gaps.Length ? gaps[i] : 0);
            }
        }

        private void GvProduction_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "GroupName" || e.Column.FieldName == "MachineName")
            {
                e.Appearance.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            }
        }

        private void GvProduction_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            if (e.Column.FieldName == "GroupName")
            {
                string group1 = Convert.ToString(gvProduction.GetRowCellValue(e.RowHandle1, "GroupName"));
                string group2 = Convert.ToString(gvProduction.GetRowCellValue(e.RowHandle2, "GroupName"));
                e.Merge = group1 == group2;
                e.Handled = true;
                return;
            }

            e.Merge = false;
            e.Handled = true;
        }

        private void GvProduction_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName != "DayRateText" && e.Column.FieldName != "NightRateText")
            {
                return;
            }

            string rateField = e.Column.FieldName == "DayRateText" ? "DayRate" : "NightRate";
            object value = gvProduction.GetRowCellValue(e.RowHandle, rateField);
            decimal rate = value.ConvertDecimal_Null_To_Zero(); ;

            if (rate < 0)
            {
                e.DefaultDraw();
                return;
            }

            e.Appearance.DrawBackground(e.Cache, e.Bounds);

            Color dotColor = rate >= 100M ? colorRed : (rate >= 50M ? colorGreen : colorGray);
            int dotSize = 12;
            int dotX = e.Bounds.X + 12;
            int dotY = e.Bounds.Y + (e.Bounds.Height - dotSize) / 2;

            using (SolidBrush brush = new SolidBrush(dotColor))
            {
                e.Cache.Graphics.FillEllipse(brush, dotX, dotY, dotSize, dotSize);
            }

            Rectangle textRect = new Rectangle(dotX + dotSize + 8, e.Bounds.Y, e.Bounds.Width - dotSize - 18, e.Bounds.Height);
            using (SolidBrush textBrush = new SolidBrush(colorText))
            {
                e.Cache.DrawString(Convert.ToString(e.CellValue), e.Appearance.Font, textBrush, textRect, e.Appearance.GetStringFormat());
            }
            e.Handled = true;
        }

        private static decimal? CalcGridRate(decimal target, decimal actual)
        {
            if (target <= 0) return null;

            return Math.Round(actual / target * 100M, 1);
        }

        private static string FormatRate(decimal? value)
        {
            return value.HasValue ? value.Value.ToString("0.#") + "%" : "-";
        }
    }

    public class RoundedPanel : Panel
    {
        public int Radius { get; set; } = 10;
        public Color FillColor { get; set; } = Color.White;
        public Color BorderColor { get; set; } = Color.FromArgb(223, 229, 238);

        public RoundedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.Clear(BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = CreateRoundPath(rect, Radius))
            using (SolidBrush fill = new SolidBrush(FillColor))
            using (Pen border = new Pen(BorderColor))
            {
                border.Alignment = PenAlignment.Inset;
                e.Graphics.FillPath(fill, path);
                e.Graphics.DrawPath(border, path);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Region = null;
        }

        private static GraphicsPath CreateRoundPath(Rectangle rect, int radius)
        {
            radius = Math.Max(1, Math.Min(radius, Math.Min(rect.Width, rect.Height) / 2));
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
