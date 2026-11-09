using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SmartCommon.Common;

namespace SmartMES.View.Dashboard
{
    // ═════════════════════════════════════════════════════════
    // 카드 헤더 패널 (재사용)
    // ═════════════════════════════════════════════════════════
    internal sealed class CompCardHeaderPanel : Panel
    {
        private readonly string _title;
        private readonly Color  _accent;

        public CompCardHeaderPanel(string title, Color accent)
        {
            _title = title; _accent = accent;
            BackColor = CT.SurfaceBg;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using (var pen = new Pen(CT.Border, 0.8f))
                g.DrawLine(pen, 0, Height - 1, Width, Height - 1);

            using (var br = new SolidBrush(_accent))
                g.FillRectangle(br, 0, 4, 3, Height - 8);

            using (var f  = new Font(CT.FN, 8.5f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.TextMuted))
            using (var sf = new StringFormat { LineAlignment = StringAlignment.Center })
                g.DrawString(_title, f, br,
                    new Rectangle(10, 0, Width - 12, Height), sf);
        }
    }

    // ═════════════════════════════════════════════════════════
    // frmComprehensiveDashboard - 메인 폼
    //
    // 레이아웃:
    //   Row1: [생산현황] [보전지수] [OEE]
    //   Row2: [품질현황] [납품/재고현황]
    //
    // frmCodeMember 와 동일한 패턴:
    //   Database db / ComprehensiveRepository repo
    //   _Load → DisplayData()
    //   cmdRefresh_ElementClick → DisplayData()
    //   Timer 시계(1초) + 자동갱신(60초)
    // ═════════════════════════════════════════════════════════
    public partial class frmComprehensiveDashboard : DevExpress.XtraEditors.XtraForm
    {
        // ── 공통 멤버 ─────────────────────────────────────────
        Database                  db   = new Database();
        ComprehensiveRepository   repo = new ComprehensiveRepository();
        ComprehensiveData         data;

        string corp = WrGlobal.CorpID;

        // ── 타이머 ────────────────────────────────────────────
        private Timer _clockTimer;
        private Timer _dataTimer;

        // ── 컨트롤 참조 ───────────────────────────────────────
        private Label                  _lblCurrentTime;

        private ProductionPanel        _pnlProduction;
        private MaintenancePanel       _pnlMaintenance;
        private OeePanel               _pnlOee;
        private QualityPanel           _pnlQuality;
        private DeliveryInventoryPanel _pnlDelivery;

        // ═════════════════════════════════════════════════════
        public frmComprehensiveDashboard()
        {
            InitializeComponent();
        }

        // ── Load ──────────────────────────────────────────────
        private void frmComprehensiveDashboard_Load(object sender, EventArgs e)
        {
            InitForm();
            BuildLayout();
            DisplayData();
            StartTimers();
        }

        // ── Form 기본 설정 ───────────────────────────────────
        private void InitForm()
        {
            Text           = "실시간 종합현황";
            BackColor      = CT.PageBg;
            StartPosition  = FormStartPosition.CenterScreen;
            Size           = new Size(1440, 900);
            MinimumSize    = new Size(1200, 720);
            Font           = new Font(CT.FN, 9f);
            DoubleBuffered = true;
        }

        // ── 데이터 조회 (DisplayCategory 패턴) ───────────────
        private void DisplayData()
        {
            try
            {
                // SP 준비 완료 후: data = repo.LoadAll(corp);
                data = ComprehensiveData.CreateSample();
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "데이터 조회 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── UI 갱신 ───────────────────────────────────────────
        private void UpdateUI()
        {
            if (data == null) return;

            _pnlProduction.UpdateData(
                data.ProductionGroups, data.ProductionTotal, data.WeeklyAchieveRate);

            _pnlMaintenance.UpdateData(data.MaintenanceIndex, data.AccumHours);

            _pnlOee.UpdateData(
                data.OeeTotal, data.OeeWarning,
                data.OeeSachulTime,  data.OeeSachulPerf,  data.OeeSachulYield,
                data.OeeJoripTime,   data.OeeJoripPerf,   data.OeeJoripYield);

            _pnlQuality.UpdateData(
                data.QualityRate, data.EorRate, data.WorstEquipList);

            _pnlDelivery.UpdateData(
                data.GrRate, data.TgRate, data.LastRetrieveTime,
                data.DoCnt, data.GrCnt, data.TgCnt, data.TgOk,
                data.InventoryList, data.StockRate);
        }

        // ── 새로고침 (cmdDisplay_ElementClick 패턴) ──────────
        private void cmdRefresh_ElementClick(object sender, EventArgs e)
        {
            DisplayData();
        }

        // ── 닫기 ─────────────────────────────────────────────
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
                RowCount    = 3,
                ColumnCount = 1,
                BackColor   = Color.Transparent,
                Padding     = new Padding(10)
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));  // 헤더
            root.RowStyles.Add(new RowStyle(SizeType.Percent,  52));  // Row1
            root.RowStyles.Add(new RowStyle(SizeType.Percent,  48));  // Row2

            root.Controls.Add(BuildHeader(), 0, 0);
            root.Controls.Add(BuildRow1(),   0, 1);
            root.Controls.Add(BuildRow2(),   0, 2);

            Controls.Add(root);
        }

        // ── 헤더 ─────────────────────────────────────────────
        private Panel BuildHeader()
        {
            var pnl = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            var lblTitle = new Label
            {
                Text      = "실  시  간  종  합  현  황",
                Font      = CT.TitleFont,
                ForeColor = CT.TextPrimary,
                AutoSize  = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock      = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            _lblCurrentTime = new Label
            {
                Text      = GetTimeText(),
                Font      = new Font(CT.FN, 9f),
                ForeColor = CT.TextMuted,
                AutoSize  = false,
                TextAlign = ContentAlignment.TopRight,
                Size      = new Size(180, 48),
                BackColor = Color.Transparent,
                Anchor    = AnchorStyles.Top | AnchorStyles.Right
            };

            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(_lblCurrentTime);
            pnl.Resize += (s, ev) =>
            {
                _lblCurrentTime.Location = new Point(pnl.Width - 188, 2);
            };

            return pnl;
        }

        // ── Row1: 생산현황 | 보전지수 | OEE ──────────────────
        private Panel BuildRow1()
        {
            var tbl = MakeGrid(1, 3);
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39));

            // 생산현황
            var cardProd = MakeCard("실시간 생산현황", CT.BlueStrong);
            _pnlProduction = new ProductionPanel(
                data?.ProductionGroups, data?.ProductionTotal ?? 0,
                data?.WeeklyAchieveRate ?? 0)
            { Dock = DockStyle.Fill };
            GetBody(cardProd).Controls.Add(_pnlProduction);
            tbl.Controls.Add(cardProd, 0, 0);

            // 보전지수
            var cardMi = MakeCard("보전지수 (Maintenance Index)", CT.TextMuted);
            _pnlMaintenance = new MaintenancePanel(
                data?.MaintenanceIndex ?? 0, data?.AccumHours ?? 0)
            { Dock = DockStyle.Fill };
            GetBody(cardMi).Controls.Add(_pnlMaintenance);
            tbl.Controls.Add(cardMi, 1, 0);

            // OEE
            var cardOee = MakeCard("설비종합효율 (OEE)", CT.TextMuted);
            _pnlOee = new OeePanel(
                data?.OeeTotal ?? 0, data?.OeeWarning ?? false,
                data?.OeeSachulTime  ?? 0, data?.OeeSachulPerf  ?? 0, data?.OeeSachulYield ?? 0,
                data?.OeeJoripTime   ?? 0, data?.OeeJoripPerf   ?? 0, data?.OeeJoripYield  ?? 0)
            { Dock = DockStyle.Fill };
            GetBody(cardOee).Controls.Add(_pnlOee);
            tbl.Controls.Add(cardOee, 2, 0);

            return Wrap(tbl);
        }

        // ── Row2: 품질현황 | 납품/재고현황 ───────────────────
        private Panel BuildRow2()
        {
            var tbl = MakeGrid(1, 2);
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52));

            // 품질현황
            var cardQual = MakeCard("실시간 품질현황", CT.RedText);
            _pnlQuality = new QualityPanel(
                data?.QualityRate ?? 0, data?.EorRate ?? 0, data?.WorstEquipList)
            { Dock = DockStyle.Fill };
            GetBody(cardQual).Controls.Add(_pnlQuality);
            tbl.Controls.Add(cardQual, 0, 0);

            // 납품/재고현황
            var cardDel = MakeCard("납품 및 재고현황", CT.GreenStrong);
            _pnlDelivery = new DeliveryInventoryPanel(
                data?.GrRate ?? 0, data?.TgRate ?? 0,
                data?.LastRetrieveTime ?? "",
                data?.DoCnt ?? 0, data?.GrCnt ?? 0,
                data?.TgCnt ?? 0, data?.TgOk  ?? 0,
                data?.InventoryList, data?.StockRate ?? 0)
            { Dock = DockStyle.Fill };
            GetBody(cardDel).Controls.Add(_pnlDelivery);
            tbl.Controls.Add(cardDel, 1, 0);

            return Wrap(tbl);
        }

        // ═════════════════════════════════════════════════════
        // 공통 팩토리
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

            var header = new CompCardHeaderPanel(title, accentColor)
            {
                Dock   = DockStyle.Fill,
                Margin = new Padding(0)
            };
            var body = new Panel
            {
                Dock      = DockStyle.Fill,
                BackColor = CT.CardBg,
                Padding   = new Padding(10, 8, 10, 10),
                Margin    = new Padding(0)
            };

            layout.Controls.Add(header, 0, 0);
            layout.Controls.Add(body,   0, 1);
            outer.Controls.Add(layout);

            outer.Paint += (s, e) =>
            {
                using (var path = CDraw.RoundRect(
                    new Rectangle(0, 0, outer.Width - 1, outer.Height - 1), CT.R))
                using (var pen = new Pen(CT.Border, 0.8f))
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

        // ═════════════════════════════════════════════════════
        // 타이머
        // ═════════════════════════════════════════════════════
        private void StartTimers()
        {
            _clockTimer = new Timer { Interval = 1000 };
            _clockTimer.Tick += (s, e) =>
            {
                _lblCurrentTime.Text = GetTimeText();
            };
            _clockTimer.Start();

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
