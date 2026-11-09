using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SmartCommon.Common;
using SmartDesign.View.Dashboard;
using SmartMES.Common;

namespace SmartMES.View.Monitoring.Dashboard
{
    public partial class Frm_PM_ProductionDashboard : DevExpress.XtraEditors.XtraForm
    {
        Database db   = new Database();

        private Timer _clockTimer;
        private Timer _dataTimer;
        private Label _lblCurrentTime;
        private DataGridView _qualGrid;

        private OeeItemPanel _oeeOee;
        private OeeItemPanel _oeeYield;
        private OeeItemPanel _oeePerf;
        private OeeItemPanel _oeeTime;

        private Dictionary<string, DonutGaugePanel> _gaugeDict = new Dictionary<string, DonutGaugePanel>();
        private Dictionary<string, DonutGaugePanel> _qualGaugeDict = new Dictionary<string, DonutGaugePanel>();
        private Dictionary<string, BarChartPanel> _barChartDict = new Dictionary<string, BarChartPanel>();
        private Dictionary<string, LineChartPanel> _lineChartDict = new Dictionary<string, LineChartPanel>();
        public Dictionary<string, ProcessMonthlyStat> MonthlyStats { get; set; } = new Dictionary<string, ProcessMonthlyStat>();
        public Dictionary<string, ProcessAchievement> Achievements { get; set; } = new Dictionary<string, ProcessAchievement>();

        public class ProcessAchievement
        {
            public int OrderQty { get; set; }
            public int WorkQty { get; set; }
            public int GoodQty { get; set; }
            public int NgQty { get; set; }
            public decimal GoodRate { get; set; }
            public decimal PerfRate { get; set; }    // 성능가동률
            public decimal TimeRate { get; set; }    // 시간가동률
            public decimal OEERate { get; set; }     // 설비종합효율(OEE)
        }

        public class ProcessMonthlyStat
        {
            public int[] WeeklyPlan { get; set; }
            public int[] WeeklyWork { get; set; }

            public int[] DailyPlan { get; set; } = new int[7];
            public int[] DailyWork { get; set; } = new int[7];
        }

        public Frm_PM_ProductionDashboard()
        {
            InitializeComponent();
        }

        private void frmProductionDashboard_Load(object sender, EventArgs e)
        {
            InitForm();
            BuildLayout();
            DisplayData();
            StartTimers();
        }

        // Form 기본 설정
        private void InitForm()
        {
            Text           = "생산종합현황판";
            BackColor      = DT.PageBg;
            StartPosition  = FormStartPosition.CenterScreen;
            Size           = new Size(1440, 900);
            MinimumSize    = new Size(1200, 780);
            Font           = new Font(DT.FN, 9f);
            DoubleBuffered = true;
        }

        // 데이터 조회
        private void DisplayData()
        {
            try
            {
                LoadDashboardData(WrGlobal.CorpID);
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "데이터 조회 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDashboardData(string corpId)
        {
            Achievements.Clear();
            MonthlyStats.Clear();

            // ── 달성율 ─────────────────────────────────────
            DataTable dtAch = SelectAchievement(corpId);
            if (dtAch != null && dtAch.Rows.Count > 0)
            {
                foreach (DataRow r in dtAch.Rows)
                {
                    string processCode = r["ProcessCode"].ConvertString_Null_To_Empty();

                    Achievements[processCode] = new ProcessAchievement
                    {
                        OrderQty = r["OrderQty"].ConvertInt_Null_To_Zero(),
                        WorkQty = r["WorkQty"].ConvertInt_Null_To_Zero()
                    };
                }
            }

            // ── 품질 ──────────────────────────────────────
            DataTable dtQual = SelectQuality(corpId);
            if (dtQual != null && dtQual.Rows.Count > 0)
            {
                foreach (DataRow r in dtQual.Rows)
                {
                    string processCode = r["ProcessCode"].ConvertString_Null_To_Empty();

                    if (!Achievements.ContainsKey(processCode))
                        Achievements[processCode] = new ProcessAchievement();

                    Achievements[processCode].WorkQty = r["WorkQty"].ConvertInt_Null_To_Zero();
                    Achievements[processCode].GoodQty = r["GoodQty"].ConvertInt_Null_To_Zero();
                    Achievements[processCode].NgQty = r["NgQty"].ConvertInt_Null_To_Zero();
                    Achievements[processCode].GoodRate = r["GoodRate"].ConvertDecimal_Null_To_Zero();
                }
            }

            // ── OEE ───────────────────────────────────────
            DataTable dtOee = SelectOee(corpId);
            if (dtOee != null && dtOee.Rows.Count > 0)
            {
                foreach (DataRow r in dtOee.Rows)
                {
                    string processCode = r["ProcessCode"].ConvertString_Null_To_Empty();

                    if (!Achievements.ContainsKey(processCode))
                        Achievements[processCode] = new ProcessAchievement();

                    Achievements[processCode].GoodRate = r["GoodRate"].ConvertDecimal_Null_To_Zero();
                    Achievements[processCode].PerfRate = r["PerfRate"].ConvertDecimal_Null_To_Zero();
                    Achievements[processCode].TimeRate = r["TimeRate"].ConvertDecimal_Null_To_Zero();
                    Achievements[processCode].OEERate = r["OEERate"].ConvertDecimal_Null_To_Zero();
                }
            }

            // ── 전월실적 ─────────────────────────────────────
            DataTable dtMon = SelectMonthlyWeekly(corpId);
            if (dtMon != null && dtMon.Rows.Count > 0)
            {
                foreach (DataRow row in dtMon.Rows)
                {
                    string processCode = row["ProcessCode"].ConvertString_Null_To_Empty();

                    int[] weeklyPlan = new int[5];
                    int[] weeklyWork = new int[5];

                    for (int w = 0; w < 5; w++)
                    {
                        weeklyPlan[w] = row[$"W{w + 1}_Plan"] == DBNull.Value ? 0 : row[$"W{w + 1}_Plan"].ConvertInt_Null_To_Zero();
                        weeklyWork[w] = row[$"W{w + 1}_Work"] == DBNull.Value ? 0 : row[$"W{w + 1}_Work"].ConvertInt_Null_To_Zero();
                    }

                    MonthlyStats[processCode] = new ProcessMonthlyStat
                    {
                        WeeklyPlan = weeklyPlan,
                        WeeklyWork = weeklyWork
                    };
                }
            }

            // ── 주간 ──────────────────────────────────────
            DataTable dtWk = SelectWeeklyDaily(corpId);
            if (dtWk != null && dtWk.Rows.Count > 0)
            {
                foreach (DataRow row in dtWk.Rows)
                {
                    string processCode = row["ProcessCode"].ConvertString_Null_To_Empty();

                    int[] dailyPlan = new int[7];
                    int[] dailyWork = new int[7];

                    for (int d = 0; d < 7; d++)
                    {
                        dailyPlan[d] = row[$"D{d + 1}_Plan"] == DBNull.Value ? 0 : Convert.ToInt32(row[$"D{d + 1}_Plan"]);
                        dailyWork[d] = row[$"D{d + 1}_Work"] == DBNull.Value ? 0 : Convert.ToInt32(row[$"D{d + 1}_Work"]);
                    }

                    if (!MonthlyStats.ContainsKey(processCode))
                        MonthlyStats[processCode] = new ProcessMonthlyStat();

                    MonthlyStats[processCode].DailyPlan = dailyPlan;
                    MonthlyStats[processCode].DailyWork = dailyWork;
                }
            }
        }

        // UI 갱신
        private void UpdateUI()
        {
            if (Achievements == null) return;

            foreach (var item in Achievements)
            {
                string processCode = item.Key;
                var achieveData = item.Value;

                if (_gaugeDict.ContainsKey(processCode))
                {
                    _gaugeDict[processCode].UpdateValue((double)achieveData.WorkQty, (double)achieveData.OrderQty);
                }

                if (_qualGaugeDict.ContainsKey(processCode))
                {
                    double rawRate = (double)achieveData.GoodRate;
                    double qualityRate = Math.Truncate(rawRate * 10) / 10;

                    _qualGaugeDict[processCode].UpdateValue(qualityRate, 100.0);
                }
            }

            RefreshQualGrid();

            if (MonthlyStats != null)
            {
                foreach (var item in MonthlyStats)
                {
                    string pCode = item.Key;
                    var stat = item.Value;

                    if (_barChartDict.ContainsKey(pCode))
                    {
                        _barChartDict[pCode].UpdateData(stat.WeeklyPlan, stat.WeeklyWork);
                    }

                    if (_lineChartDict.ContainsKey(pCode))
                    {
                        _lineChartDict[pCode].UpdateData(stat.DailyPlan, stat.DailyWork);
                    }
                }
            }

            var yieldList = new List<KeyValuePair<string, double>>();
            var perfList = new List<KeyValuePair<string, double>>();
            var timeList = new List<KeyValuePair<string, double>>();
            var oeeList = new List<KeyValuePair<string, double>>();

            DataTable dtProcess = ProcessList();
            foreach (DataRow row in dtProcess.Rows)
            {
                string pCode = row["ProcessCode"].ConvertString_Null_To_Empty();
                string pName = row["ProcessName"].ConvertString_Null_To_Empty();

                if (Achievements.TryGetValue(pCode, out var ach))
                {
                    yieldList.Add(new KeyValuePair<string, double>(pName, Math.Truncate((double)ach.GoodRate * 10) / 10));
                    perfList.Add(new KeyValuePair<string, double>(pName, (double)ach.PerfRate));
                    timeList.Add(new KeyValuePair<string, double>(pName, (double)ach.TimeRate));
                    oeeList.Add(new KeyValuePair<string, double>(pName, (double)ach.OEERate));
                }
            }

            _oeeYield.UpdateValues(yieldList);
            _oeePerf.UpdateValues(perfList);
            _oeeTime.UpdateValues(timeList);
            _oeeOee.UpdateValues(oeeList);
        }

        // 새로고침 버튼
        private void cmdRefresh_ElementClick(object sender, EventArgs e)
        {
            DisplayData();
        }

        // 닫기 버튼
        private void cmdClose_ElementClick(object sender, EventArgs e)
        {
            this.Close();
        }

        // ═════════════════════════════════════════════════════
        // 레이아웃 구성
        // ═════════════════════════════════════════════════════
        private void BuildLayout()
        {
            var root = new TableLayoutPanel
            {
                Dock        = DockStyle.Fill,
                RowCount    = 4,
                ColumnCount = 1,
                BackColor   = Color.Transparent,
                Padding     = new Padding(10)
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));  // 헤더
            root.RowStyles.Add(new RowStyle(SizeType.Percent,  36));  // Row1
            root.RowStyles.Add(new RowStyle(SizeType.Percent,  36));  // Row2
            root.RowStyles.Add(new RowStyle(SizeType.Percent,  28));  // Row3

            root.Controls.Add(BuildHeader(), 0, 0);
            root.Controls.Add(BuildRow1(),   0, 1);
            root.Controls.Add(BuildRow2(),   0, 2);
            root.Controls.Add(BuildRow3(),   0, 3);

            Controls.Add(root);
        }

        // 헤더
        private Panel BuildHeader()
        {
            var pnl = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            var lblTitle = new Label
            {
                Text      = "생  산  종  합  현  황  판",
                Font      = DT.TitleFont,
                ForeColor = DT.TextPrimary,
                AutoSize  = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock      = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            _lblCurrentTime = new Label
            {
                Text      = GetTimeText(),
                Font      = new Font(DT.FN, 9f),
                ForeColor = DT.TextMuted,
                AutoSize  = false,
                TextAlign = ContentAlignment.TopRight,
                Size      = new Size(180, 48),
                BackColor = Color.Transparent,
                Location  = new Point(pnl.Width > 188 ? pnl.Width - 188 : 0, 2),
                Anchor    = AnchorStyles.Top | AnchorStyles.Right
            };

            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(_lblCurrentTime);

            _lblCurrentTime.BringToFront();

            return pnl;
        }

        // Row1: 달성율 | 품질
        private Panel BuildRow1()
        {
            var tbl = MakeGrid(1, 2);
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

            tbl.Controls.Add(BuildAchieveCard(), 0, 0);
            tbl.Controls.Add(BuildQualityCard(), 1, 0);

            return Wrap(tbl);
        }

        // Row2: 전월실적 | 주간실적
        private Panel BuildRow2()
        {
            DataTable dtProcess = ProcessList();
            if (dtProcess == null || dtProcess.Rows.Count == 0) return new Panel();

            int processCount = dtProcess.Rows.Count;
            int colCount = processCount * 2;

            var tbl = MakeGrid(1, colCount);
            float percent = 100f / colCount;
            for (int i = 0; i < colCount; i++)
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percent));

            var weeks = new string[] { "1주", "2주", "3주", "4주", "5주" };
            var days = new string[] { "월", "화", "수", "목", "금", "토", "일" };

            // 색상 배열
            var palettes = new[] {
                new { Base = DT.BlueStrong,  Light = DT.BlueLight,  Fill = Color.FromArgb(18, 24, 95, 165) },
                new { Base = DT.GreenStrong, Light = DT.GreenLight, Fill = Color.FromArgb(18, 59, 109, 17) },
                new { Base = DT.OrangeStrong, Light = DT.OrangeLight, Fill = Color.FromArgb(18, 255, 140, 0) }
            };

            _barChartDict.Clear();
            _lineChartDict.Clear();

            for (int i = 0; i < processCount; i++)
            {
                DataRow row = dtProcess.Rows[i];
                string processCode = row["ProcessCode"].ConvertString_Null_To_Empty();
                string processName = row["ProcessName"].ConvertString_Null_To_Empty();
                var palette = palettes[i % palettes.Length];

                // 빈 데이터 배열
                int[] emptyWeekly = new int[5];
                int[] emptyDaily = new int[7];

                // 전월실적 바 차트 생성
                var cardBar = MakeChartCard($"전월실적 - {processName}", palette.Base);
                var barChart = new BarChartPanel(weeks, emptyWeekly, emptyWeekly, palette.Light, palette.Base);
                AddLegendAndChart(cardBar, new string[] { "목표", "실적" }, new Color[] { palette.Light, palette.Base }, barChart, false);

                tbl.Controls.Add(cardBar, i * 2, 0);
                _barChartDict[processCode] = barChart;

                // 주간 라인 차트 생성
                var cardLine = MakeChartCard($"{processName}  주간", palette.Base);
                var lineChart = new LineChartPanel(days, emptyDaily, emptyDaily, palette.Base, palette.Fill);
                AddLegendAndChart(cardLine, new string[] { "목표", "실적" }, new Color[] { Color.FromArgb(180, 180, 180), palette.Base }, lineChart, true);

                tbl.Controls.Add(cardLine, (i * 2) + 1, 0);
                _lineChartDict[processCode] = lineChart;
            }

            return Wrap(tbl);
        }

        // Row3: OEE 4개
        private Panel BuildRow3()
        {
            var tbl = MakeGrid(1, 4);
            for (int i = 0; i < 4; i++)
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            _oeeOee = new OeeItemPanel("설비종합효율 (OEE)") { Dock = DockStyle.Fill, Margin = new Padding(4) };
            _oeeYield = new OeeItemPanel("양품율") { Dock = DockStyle.Fill, Margin = new Padding(4) };
            _oeePerf = new OeeItemPanel("성능가동율") { Dock = DockStyle.Fill, Margin = new Padding(4) };
            _oeeTime = new OeeItemPanel("시간가동율") { Dock = DockStyle.Fill, Margin = new Padding(4) };

            tbl.Controls.Add(_oeeOee,   0, 0);
            tbl.Controls.Add(_oeeYield, 1, 0);
            tbl.Controls.Add(_oeePerf,  2, 0);
            tbl.Controls.Add(_oeeTime,  3, 0);

            return Wrap(tbl);
        }

        // ═════════════════════════════════════════════════════
        // 달성율 카드
        // ═════════════════════════════════════════════════════
        private Panel BuildAchieveCard()
        {
            var card = MakeCard("실시간 생산달성율", DT.TextMuted);

            // 공정 리스트
            DataTable dtProcess = ProcessList();
            int processCount = dtProcess.Rows.Count;

            if (processCount == 0) processCount = 1;

            var inner = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = processCount,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            inner.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // 컬럼 너비 비율 설정 (100% / 공정 수)
            float columnPercent = 100f / processCount;
            for (int i = 0; i < processCount; i++)
            {
                inner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
            }

            _gaugeDict.Clear();

            // 색상 배열
            var colors = new[]
            {
                new { Strong = DT.BlueStrong,  Bg = DT.BluePillBg,  Fg = DT.BluePillFg },
                new { Strong = DT.GreenStrong, Bg = DT.GreenPillBg, Fg = DT.GreenPillFg },
                new { Strong = DT.OrangeStrong, Bg = DT.OrangePillBg, Fg = DT.OrangePillFg }
            };

            for (int i = 0; i < dtProcess.Rows.Count; i++)
            {
                DataRow row = dtProcess.Rows[i];
                string processCode = row["ProcessCode"].ConvertString_Null_To_Empty();
                string processName = row["ProcessName"].ConvertString_Null_To_Empty();

                var colorSet = colors[i % colors.Length];

                double actualValue = 0;
                double targetValue = 0;

                var gauge = new DonutGaugePanel(
                    processName,
                    actualValue,
                    targetValue,
                    colorSet.Strong, colorSet.Bg, colorSet.Fg)
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(3)
                };

                _gaugeDict[processCode] = gauge;
                inner.Controls.Add(gauge, i, 0);
            }

            GetBody(card).Controls.Add(inner);
            return card;
        }

        // ═════════════════════════════════════════════════════
        // 생산 카드
        // ═════════════════════════════════════════════════════
        private Panel BuildQualityCard()
        {
            var card = MakeCard("생산종합현황", DT.TextMuted);
            var body = GetBody(card);

            var outer = new TableLayoutPanel
            {
                Dock        = DockStyle.Fill,
                ColumnCount = 2,
                RowCount    = 1,
                BackColor   = Color.Transparent
            };
            outer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            outer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));
            outer.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            DataTable dtProcess = ProcessList();
            int processCount = dtProcess.Rows.Count;

            if (processCount == 0) processCount = 1;

            var gaugeSplit = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = processCount,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            gaugeSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            float columnPercent = 100f / processCount;
            for (int i = 0; i < processCount; i++)
            {
                gaugeSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
            }

            _qualGaugeDict.Clear();

            // 색상 배열
            var colors = new[]
            {
                new { Strong = DT.BlueStrong,  Bg = DT.BluePillBg,  Fg = DT.BluePillFg },
                new { Strong = DT.GreenStrong, Bg = DT.GreenPillBg, Fg = DT.GreenPillFg },
                new { Strong = DT.OrangeStrong, Bg = DT.OrangePillBg, Fg = DT.OrangePillFg }
            };

            for (int i = 0; i < dtProcess.Rows.Count; i++)
            {
                DataRow row = dtProcess.Rows[i];
                string processCode = row["ProcessCode"].ConvertString_Null_To_Empty();
                string processName = row["ProcessName"].ConvertString_Null_To_Empty();

                var colorSet = colors[i % colors.Length];

                var gauge = new DonutGaugePanel(
                    processName,
                    0, // 양품 수량 초기값
                    1, // 검사 수량 초기값 (0으로 나누기 방지)
                    colorSet.Strong, colorSet.Bg, colorSet.Fg,
                    "양품률", compact: true)
                    {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(3)
                    };

                _qualGaugeDict[processCode] = gauge;
                gaugeSplit.Controls.Add(gauge, i, 0);
            }

            // 우측 그리드
            _qualGrid = BuildQualGrid();

            outer.Controls.Add(gaugeSplit, 0, 0);
            outer.Controls.Add(_qualGrid, 1, 0);
            body.Controls.Add(outer);

            return card;
        }

        // ═════════════════════════════════════════════════════
        // 생산 그리드
        // ═════════════════════════════════════════════════════
        private DataGridView BuildQualGrid()
        {
            var dgv = new DataGridView
            {
                Dock                      = DockStyle.Fill,
                ReadOnly                  = true,
                AllowUserToAddRows        = false,
                AllowUserToDeleteRows     = false,
                AllowUserToResizeRows     = false,
                RowHeadersVisible         = false,
                BorderStyle               = BorderStyle.None,
                BackgroundColor           = DT.CardBg,
                GridColor                 = DT.BorderLight,
                Font                      = new Font(DT.FN, 9f),
                AutoSizeColumnsMode       = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode             = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersBorderStyle  = DataGridViewHeaderBorderStyle.None,
                CellBorderStyle           = DataGridViewCellBorderStyle.SingleHorizontal,
                EnableHeadersVisualStyles = false,
                Margin                    = new Padding(4)
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor  = DT.SurfaceBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor  = DT.TextMuted;
            dgv.ColumnHeadersDefaultCellStyle.Font       = new Font(DT.FN, 9f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 24;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgv.DefaultCellStyle.Alignment          = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.ForeColor          = DT.TextSecond;
            dgv.DefaultCellStyle.BackColor          = DT.CardBg;
            dgv.DefaultCellStyle.SelectionBackColor = DT.SurfaceBg;
            dgv.DefaultCellStyle.SelectionForeColor = DT.TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = DT.SurfaceBg;

            dgv.Columns.Add("구분", "구분");
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            DataTable dtProcess = ProcessList();
            foreach (DataRow row in dtProcess.Rows)
            {
                string processCode = row["ProcessCode"].ConvertString_Null_To_Empty();
                string processName = row["ProcessName"].ConvertString_Null_To_Empty();

                dgv.Columns.Add(processCode, processName);
            }

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            Color[] processColors = new Color[]
            {
                DT.BlueStrong,
                DT.GreenStrong,
                DT.OrangeStrong
            };

            dgv.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == 0) return;

                if (e.RowIndex == 2)
                {
                    e.CellStyle.ForeColor = DT.RedText;
                    e.CellStyle.Font = new Font(DT.FN, 9f, FontStyle.Bold);
                }

                if (e.RowIndex == 3)
                {
                    int pIndex = e.ColumnIndex - 1;

                    if (pIndex >= 0)
                    {
                        int colorIndex = pIndex % processColors.Length;
                        e.CellStyle.ForeColor = processColors[colorIndex];
                    }
                }
            };

            dgv.SelectionChanged += (s, e) =>
            {
                dgv.ClearSelection();
            };

            return dgv;
        }

        private void RefreshQualGrid()
        {
            if (_qualGrid == null || Achievements == null) return;

            _qualGrid.Rows.Clear();

            int colCount = _qualGrid.Columns.Count;
            if (colCount <= 1) return;

            string[] WorkQty = new string[colCount];
            string[] GoodQty = new string[colCount];
            string[] NgQty = new string[colCount];
            string[] GoodRate = new string[colCount];

            WorkQty[0] = "생산수량";
            GoodQty[0] = "양품수량";
            NgQty[0] = "불량수량";
            GoodRate[0] = "양품률";

            for (int i = 1; i < colCount; i++)
            {
                string processCode = _qualGrid.Columns[i].Name;

                if (Achievements.TryGetValue(processCode, out var achieveData))
                {
                    WorkQty[i] = achieveData.WorkQty.ToString("N0");
                    GoodQty[i] = achieveData.GoodQty.ToString("N0");
                    NgQty[i] = achieveData.NgQty.ToString("N0");
                    GoodRate[i] = achieveData.GoodRate.ToString("0.00") + "%";
                }
            }

            _qualGrid.Rows.Add(WorkQty);
            _qualGrid.Rows.Add(GoodQty);
            _qualGrid.Rows.Add(NgQty);
            _qualGrid.Rows.Add(GoodRate);

            _qualGrid.ClearSelection();
        }

        // ═════════════════════════════════════════════════════
        // 차트 카드 헬퍼
        // ═════════════════════════════════════════════════════
        private Panel MakeChartCard(string title, Color accentColor)
        {
            return MakeCard(title, accentColor);
        }

        private void AddLegendAndChart(Panel card, string[] legendTexts, Color[] legendColors, Panel chartPanel, bool firstDashed)
        {
            var body = GetBody(card);
            var legendPnl = new LegendPanel(legendTexts, legendColors, firstDashed)
            {
                Dock   = DockStyle.Top,
                Height = 16
            };
            chartPanel.Dock = DockStyle.Fill;
            body.Controls.Add(chartPanel);
            body.Controls.Add(legendPnl);
        }

        // ═════════════════════════════════════════════════════
        // 공통 카드 팩토리
        // ═════════════════════════════════════════════════════
        private Panel MakeCard(string title, Color accentColor)
        {
            var outer = new Panel
            {
                Dock      = DockStyle.Fill,
                BackColor = Color.Transparent,
                Margin    = new Padding(4)
            };

            var layout = new TableLayoutPanel
            {
                Dock        = DockStyle.Fill,
                RowCount    = 2,
                ColumnCount = 1,
                BackColor   = Color.Transparent,
                Padding     = new Padding(0),
                Margin      = new Padding(0)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var header = new CardHeaderPanel(title, accentColor)
            {
                Dock   = DockStyle.Fill,
                Margin = new Padding(0)
            };
            var body = new Panel
            {
                Dock      = DockStyle.Fill,
                BackColor = DT.CardBg,
                Padding   = new Padding(8, 6, 8, 8),
                Margin    = new Padding(0)
            };

            layout.Controls.Add(header, 0, 0);
            layout.Controls.Add(body,   0, 1);
            outer.Controls.Add(layout);

            outer.Paint += (s, e) =>
            {
                using (var path = DrawHelper.RoundRect(
                    new Rectangle(0, 0, outer.Width - 1, outer.Height - 1), DT.R))
                using (var pen  = new Pen(DT.Border, 0.8f))
                    e.Graphics.DrawPath(pen, path);
            };

            return outer;
        }

        private Panel GetBody(Panel card)
        {
            var layout = (TableLayoutPanel)card.Controls[0];
            return (Panel)layout.Controls[1];
        }

        private TableLayoutPanel MakeGrid(int rows, int cols)
        {
            var t = new TableLayoutPanel
            {
                Dock        = DockStyle.Fill,
                RowCount    = rows,
                ColumnCount = cols,
                BackColor   = Color.Transparent,
                Padding     = new Padding(0)
            };
            for (int i = 0; i < rows; i++)
                t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            return t;
        }

        private Panel Wrap(Control c)
        {
            var p = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            c.Dock = DockStyle.Fill;
            p.Controls.Add(c);
            return p;
        }

        // ── 실시간 달성율 ─────────────────────────────────────
        private DataTable SelectAchievement(string corpId)
        {
            string query = $"exec Dashboard_GetAchievement '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 전월실적 ──────────────────────────────────────────
        private DataTable SelectMonthlyWeekly(string corpId)
        {
            string query = $"exec Dashboard_GetMonthlyWeekly '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 주간 일별 실적 ────────────────────────────────────
        private DataTable SelectWeeklyDaily(string corpId)
        {
            string query = $"exec Dashboard_GetWeeklyDaily '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 품질 현황 ─────────────────────────────────────────
        private DataTable SelectQuality(string corpId)
        {
            string query = $"exec Dashboard_GetQuality '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── OEE ───────────────────────────────────────────────
        private DataTable SelectOee(string corpId)
        {
            string query = $"exec Dashboard_GetOee '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 공정 리스트 ───────────────────────────────────────
        private DataTable ProcessList()
        {
            string query = $@"
                             SELECT
                              ProcessCode
                            , ProcessName
                            , ROW_NUMBER() OVER(ORDER BY ProcessCode) AS Sort
                        FROM CM_Process
                        WHERE UseYn = 'Y'
            ";
            return db.GetDataTable(query);
        }

        // ═════════════════════════════════════════════════════
        // 타이머
        // ═════════════════════════════════════════════════════
        private void StartTimers()
        {
            // 시계 (1초)
            _clockTimer = new Timer { Interval = 1000 };
            _clockTimer.Tick += (s, e) =>
            {
                _lblCurrentTime.Text = GetTimeText();
            };
            _clockTimer.Start();

            // 데이터 자동갱신 (60초)
            _dataTimer = new Timer { Interval = 60000 };
            _dataTimer.Tick += (s, e) =>
            {
                DisplayData();
            };
            _dataTimer.Start();
        }

        private string GetTimeText()
        {
            return "현재시간\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // ── Dispose ───────────────────────────────────────────
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clockTimer?.Dispose();
                _dataTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
