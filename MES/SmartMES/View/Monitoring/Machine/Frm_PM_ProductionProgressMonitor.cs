using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;
using VScrollBar = System.Windows.Forms.VScrollBar;

namespace SmartMES.View.Monitoring.Machine
{
	public partial class Frm_PM_ProductionProgressMonitor : DevExpress.XtraEditors.XtraForm
    {
        // 색상 정의
        static readonly Color C_BG = Color.FromArgb(240, 244, 248);
        static readonly Color C_PANEL = Color.White;
        static readonly Color C_BORDER = Color.FromArgb(226, 232, 240);
        static readonly Color C_BLUE = Color.FromArgb(37, 99, 168);
        static readonly Color C_GREEN = Color.FromArgb(22, 163, 74);
        static readonly Color C_RED = Color.FromArgb(220, 38, 38);
        static readonly Color C_AMBER = Color.FromArgb(217, 119, 6);
        static readonly Color C_PURPLE = Color.FromArgb(124, 58, 237);
        static readonly Color C_CYAN = Color.FromArgb(8, 145, 178);
        static readonly Color C_TEXT = Color.FromArgb(30, 35, 45);
        static readonly Color C_MUTED = Color.FromArgb(100, 116, 139);
        static readonly Color C_ROWALT = Color.FromArgb(248, 250, 252);
        static readonly Color C_ROWHOV = Color.FromArgb(239, 246, 255);
        static readonly Color C_GRIDLN = Color.FromArgb(241, 245, 249);

        clsGrid grid = new clsGrid();
        Database db = new Database();

        List<EquipCard> cards = new List<EquipCard>();
        List<OrderProcess> orders = new List<OrderProcess>();

        Timer refreshTimer = new Timer();
        Timer clockTimer = new Timer();
        Timer animTimer = new Timer();

        bool isManualSearch = false;
        int totalOrderCnt, runningCnt, waitingCnt, stopCnt;
        decimal AvgWorkRate;
        double animT = 0.0; int hoverRow = -1, selRow = -1;

        string query = "";
        string[] chartEqNames = new string[0];
        double[] chartEqRates = new double[0];

        Rectangle rcFilter, rcCards, rcGrid, rcChartL, rcChartR, rcClock;
        DateTimePicker dpDate;
        ComboBox cmbEquip, cmbStatus;
        Button btnSearch, btnExcel;
        VScrollBar gridScroll;

        class OrderProcess
        {
            public string OrderNo, Equip, ItemCode, ItemName, StartTime, EndTime, Status;
            public int No, OrderQty, WorkQty, NgQty;
            public double WorkRate, NgRate;
        }

        class EquipCard
        {
            public string MachineName, ItemName, Status;
            public decimal OrderQty, GoodQty, NgQty;
            public double WorkRate, NgRate;
            public Color StatusColor;
        }

        public Frm_PM_ProductionProgressMonitor()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.MinimumSize = new Size(1200, 800);

            BuildControls();
        }

        private void Frm_PM_ProductionProgressMonitor_Load(object sender, EventArgs e)
        {
            // 패널 깜빡임 방지 (더블 버퍼링 강제 활성화)
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, panelDashboard, new object[] { true });

            panelDashboard.Paint += panelDashboard_Paint;
            panelDashboard.MouseMove += OnMM;
            panelDashboard.MouseClick += OnMC;

            panelDashboard.MouseEnter += (s, ev) => panelDashboard.Focus();
            panelDashboard.MouseWheel += panelDashboard_MouseWheel;

            this.Resize += (s, ev) =>
            {
                if (panelDashboard != null)
                {
                    CalcLayout();
                    LayoutControls();
                    panelDashboard.Invalidate();
                }
            };

            LoadEquipList();
            LoadData();

            // 새로고침
            refreshTimer.Interval = 10000;
            refreshTimer.Tick += (s, e2) =>
            {
                LoadData();
            };
            refreshTimer.Start();

            // 우측 하단 시계 타이머
            clockTimer.Interval = 1000;
            clockTimer.Tick += (s, ev) =>
            {
                if (panelDashboard != null)
                {
                    rcClock = new Rectangle(0, panelDashboard.Height - 30, panelDashboard.Width, 30);
                    panelDashboard.Invalidate(rcClock);
                }
            };
            clockTimer.Start();

            // 애니메이션 타이머
            animTimer.Interval = 15;
            animTimer.Tick += (s, ev) =>
            {
                animT += 0.05;

                if (animT >= 1.0)
                {
                    animT = 1.0;
                    animTimer.Stop();
                }

                if (panelDashboard != null)
                {
                    panelDashboard.Invalidate(rcChartL);
                    panelDashboard.Invalidate(rcChartR);
                }
            };

            this.FormClosing += Frm_PM_ProductionProgressMonitor_FormClosing;
        }

        private void panelDashboard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(C_BG);

            DrawFilter(e.Graphics);         // 조회필터
            DrawKpi(e.Graphics);            // KPI 상단 박스

            DrawSecLabel(g, rcCards.X, rcCards.Y - 18, "설비별 실시간 진행 현황");
            DrawCards(e.Graphics);          // 설비 카드

            DrawSecLabel(g, rcGrid.X, rcGrid.Y - 25, "작업지시 현황");
            DrawGrid(g);                    // 작업지시 그리드

            DrawChartLeft(g, rcChartL);     // 생산 달성률 차트
            DrawChartRight(g, rcChartR);    // 작업 상태 현황 차트

            string dTime = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
            using (Font f = new Font("맑은 고딕", 9)) using (SolidBrush b = new SolidBrush(C_MUTED))
            { SizeF sz = g.MeasureString(dTime, f);
                g.DrawString(dTime, f, b, panelDashboard.Width - sz.Width - 14, panelDashboard.Height - sz.Height - 6); }
        }

        void BuildControls()
        {
            Font cf = new Font("맑은 고딕", 11);
            dpDate = new DateTimePicker(); dpDate.Format = DateTimePickerFormat.Short; dpDate.Value = DateTime.Today; dpDate.Font = cf;
            //cmbDong = new ComboBox(); cmbDong.DropDownStyle = ComboBoxStyle.DropDownList; cmbDong.Font = cf;
            //cmbDong.Items.AddRange(new object[] { "전체", "D동" }); cmbDong.SelectedIndex = 0;
            cmbEquip = new ComboBox(); cmbEquip.DropDownStyle = ComboBoxStyle.DropDownList; cmbEquip.Font = cf;
            cmbStatus = new ComboBox(); cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList; cmbStatus.Font = cf;
            cmbStatus.Items.AddRange(new object[] { "전체", "작업중", "대기", "작업완료" }); cmbStatus.SelectedIndex = 0;
            btnSearch = new Button(); btnSearch.Text = "조회"; btnSearch.Font = new Font("맑은 고딕", 9, FontStyle.Bold);
            btnSearch.BackColor = C_BLUE; btnSearch.ForeColor = Color.White; btnSearch.FlatStyle = FlatStyle.Flat; btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0; btnSearch.Click += (s, e) => { isManualSearch = true; LoadData(); animT = 0; panelDashboard.Invalidate(); };
            //btnExcel = new Button(); btnExcel.Text = "엑셀다운"; btnExcel.Font = new Font("맑은 고딕", 9, FontStyle.Bold); btnExcel.BackColor = C_PANEL;
            //btnExcel.ForeColor = C_MUTED; btnExcel.FlatStyle = FlatStyle.Flat; btnExcel.Cursor = Cursors.Hand;
            //btnExcel.FlatAppearance.BorderColor = C_BORDER; btnExcel.FlatAppearance.BorderSize = 1;
            panelDashboard.Controls.AddRange(new Control[] { dpDate, cmbEquip, cmbStatus, btnSearch, btnExcel });

            dpDate.BringToFront();
            //cmbDong.BringToFront();
            cmbEquip.BringToFront();
            cmbStatus.BringToFront();
            btnSearch.BringToFront();
            //btnExcel.BringToFront();

            gridScroll = new VScrollBar();
            gridScroll.SmallChange = 1;
            gridScroll.LargeChange = 1;
            gridScroll.Scroll += (s, e) => { panelDashboard.Invalidate(); };
            panelDashboard.Controls.Add(gridScroll);

            CalcLayout();
            LayoutControls();
        }

        void CalcLayout()
        {
            int W = panelDashboard.Width;
            int H = panelDashboard.Height;
            int P = 14, TH = 42, FH = 48, KH = 56;

            // 필터
            rcFilter = new Rectangle(P, P, W - (P * 2), FH);

            // KPI 박스
            int kpiBottom = rcFilter.Bottom + 15 + 80;

            // 설비 카드
            int y1 = kpiBottom + 30;

            int cardW = 260;
            int cardH = 180;
            int cardMargin = 20;
            int availableWidth = W - (P * 2);

            int cols = (availableWidth + cardMargin) / (cardW + cardMargin);
            if (cols < 1) cols = 1;

            int cardCount = (cards != null && cards.Count > 0) ? cards.Count : 1;
            int rows = (int)Math.Ceiling((double)cardCount / cols);
            int totalCardsHeight = (rows * cardH) + ((rows - 1) * cardMargin);

            rcCards = new Rectangle(P, y1, availableWidth, totalCardsHeight);

            // 그리드
            int y2 = rcCards.Bottom + 40;
            rcGrid = new Rectangle(P, y2, availableWidth, 152);

            gridScroll.SetBounds(rcGrid.Right - 20, rcGrid.Y + 30, 20, rcGrid.Height - 30);

            // 차트
            int y3 = rcGrid.Bottom + P;
            int cH = H - y3 - P - 18;
            int hW = (W - (P * 2) - 10) / 2;

            rcChartL = new Rectangle(P, y3, hW, cH);
            rcChartR = new Rectangle(P + hW + 10, y3, hW, cH);
        }

        void LayoutControls()
        {
            int P = 14;
            int fy = rcFilter.Y + 12;

            dpDate.SetBounds(P + 80, fy, 108, 26);
            //cmbDong.SetBounds(P + 220, fy, 96, 26);
            cmbEquip.SetBounds(P + 250, fy, 108, 26);
            cmbStatus.SetBounds(P + 444, fy, 96, 26);
            btnSearch.SetBounds(P + 552, fy, 64, 28);
            //btnExcel.SetBounds(P + 686, fy, 76, 26);
        }

        void DrawFilter(Graphics g)
        {
            DrawPanel(g, rcFilter, 10);
            using (Font f = new Font("맑은 고딕", 9)) using (SolidBrush b = new SolidBrush(C_MUTED))
            {
                int lx = rcFilter.X + 10, ly = rcFilter.Y + 12;
                g.DrawString("기준일자", f, b, lx, ly + 4);
                //g.DrawString("생산동", f, b, lx + 172, ly + 4);
                g.DrawString("설비", f, b, lx + 200, ly + 4);
                g.DrawString("작업상태", f, b, lx + 362, ly + 4);
            }
        }

        private void LoadEquipList()
        {
            string query = $@"
                             SELECT
                                   'ALL' AS MachineCode
                                 , '전체' AS MachineName
                             UNION ALL
                             SELECT
                                   MachineCode
                                 , MachineName
                             FROM EQ_Machine
                             WHERE CorpId = '{WrGlobal.CorpID}'
                               AND ISNULL(UseYN, 'Y') = 'Y'
                             ORDER BY MachineCode;
                            ";

            DataTable equipTable = db.GetDataTable(query);

            cmbEquip.DataSource = equipTable;
            cmbEquip.ValueMember = "MachineCode";
            cmbEquip.DisplayMember = "MachineName";
            cmbEquip.SelectedValue = "ALL";
        }

        private void LoadData()
        {
            try
            {
                string sDate = dpDate.Value.ConvertDate_To_Char8();
                string sEquip = cmbEquip.Text.ConvertString_Null_To_Empty();
                string sStatus = cmbStatus.Text.ConvertString_Null_To_Empty();

                // 금일 작업건수 조회
                string kpiQuery = $@"
                                   DECLARE
                                      @CorpId   VARCHAR(10)  = '{WrGlobal.CorpID}'
                                    , @WorkDate CHAR(8)      = '{sDate}'
                                    , @Equip    NVARCHAR(50) = '{sEquip}';

                                  SELECT
                                    COUNT(OP.OrderNo) AS TotalCnt,
                                    SUM(CASE WHEN OP.Status='작업중' THEN 1 ELSE 0 END) AS RunningCnt,
                                    SUM(CASE WHEN OP.Status='대기' THEN 1 ELSE 0 END) AS WaitingCnt,
                                    SUM(CASE WHEN OP.Status IN ('정지','작업완료') THEN 1 ELSE 0 END) AS StopCnt,

                                    -- 달성률 : (총 양품수량 / 총 지시수량) * 100
                                    ISNULL(
                                        ROUND(
                                            SUM(R.GoodQty) * 100.0 / NULLIF(SUM(OP.OrderQty), 0)
                                        , 1)
                                    , 0) AS AvgWorkRate
                                FROM PM_OrderProcess OP
                                LEFT JOIN (
                                    SELECT
                                          CorpId
                                        , OrderNo
                                        , OrderSeq
                                        , SUM(ISNULL(GoodQty, 0)) AS GoodQty
                                    FROM PM_OrderResult
                                    GROUP BY CorpId, OrderNo, OrderSeq
                                ) R ON OP.CorpId = R.CorpId AND OP.OrderNo = R.OrderNo AND OP.OrderSeq = R.OrderSeq
                                WHERE OP.CorpId = @CorpId
                                  AND CONVERT(VARCHAR(8), OP.OrderDate, 112) = @WorkDate
                                    ";

                DataTable kpiDt = db.GetDataTable(kpiQuery);

                if (kpiDt.Rows.Count > 0)
                {
                    totalOrderCnt = kpiDt.Rows[0]["TotalCnt"].ConvertInt_Null_To_Zero();
                    runningCnt = kpiDt.Rows[0]["RunningCnt"].ConvertInt_Null_To_Zero();
                    waitingCnt = kpiDt.Rows[0]["WaitingCnt"].ConvertInt_Null_To_Zero();
                    stopCnt =  kpiDt.Rows[0]["StopCnt"].ConvertInt_Null_To_Zero();
                    AvgWorkRate = kpiDt.Rows[0]["AvgWorkRate"].ConvertDecimal_Null_To_Zero();
                }


                // 설비별 생산진행 현황 조회
                query = $@"
                        DECLARE
                          @CorpId   VARCHAR(10)  = '{WrGlobal.CorpID}'
                        , @WorkDate CHAR(8)      = '{sDate}'
                        , @Equip    NVARCHAR(50) = '{sEquip}'
                        , @Status   NVARCHAR(20) = '{sStatus}';

                        SELECT
                            OP.OrderNo,
                            EQ.MachineName,
                            OP.ItemCode,
                            CM.ItemName,

                            ISNULL(OP.OrderQty, 0) AS OrderQty,
                            ISNULL(ORE.GoodQty, 0) AS GoodQty,
                            ISNULL(ORE.NgQty, 0) AS NgQty,

                            ISNULL(CAST(ORE.GoodQty AS FLOAT) / NULLIF(CAST(OP.OrderQty AS FLOAT), 0) * 100, 0) AS WorkRate,
                            ISNULL(CAST(ORE.NgQty AS FLOAT)   / NULLIF(CAST(OP.OrderQty AS FLOAT), 0) * 100, 0) AS NgRate,

                            OP.Status

                        FROM PM_OrderProcess OP
                        LEFT JOIN (
                            SELECT
                                  CorpId
                                , OrderNo
                                , OrderSeq
                                , SUM(ISNULL(GoodQty, 0)) AS GoodQty
                                , SUM(ISNULL(NgQty, 0)) AS NgQty
                            FROM PM_OrderResult
                            GROUP BY CorpId, OrderNo, OrderSeq
                        ) ORE ON OP.OrderNo = ORE.OrderNo AND OP.OrderSeq = ORE.OrderSeq AND OP.CorpId = ORE.CorpId

                        LEFT JOIN CM_Item CM ON OP.ItemCode = CM.ItemCode AND OP.CorpId = CM.CorpId
                        LEFT JOIN EQ_Machine EQ ON OP.MachineCode = EQ.MachineCode AND OP.CorpId = EQ.CorpId

                        WHERE OP.CorpId = @CorpId
                        AND CONVERT(VARCHAR(8), OP.OrderDate, 112) = @WorkDate
                        AND (@Equip = '전체' OR @Equip = '' OR EQ.MachineName = @Equip)
                        AND (@Status = '전체' OR @Status = '' OR OP.Status = @Status)
                        ORDER BY OP.OrderNo;
                ";

                DataView dv = db.GetDataView("생산진행현황", query);
                ConvertModels(dv);


                // 작업지시 현황 조회
                query = $@"
                         DECLARE
                               @CorpId   VARCHAR(10)  = '{WrGlobal.CorpID}'
                             , @WorkDate CHAR(8)      = '{sDate}'
                             , @Equip    NVARCHAR(50) = '{sEquip}'
                             , @Status   NVARCHAR(20) = '{sStatus}';

                           SELECT
                             OP.OrderNo,
                             EQ.MachineName,
                             OP.ItemCode,
                             CM.ItemName,
                             SUM(OP.OrderQty) AS OrderQty,
							 SUM(ORE.WorkQty) AS WorkQty,
                             SUM(ORE.GoodQty) AS GoodQty,
                             SUM(ORE.NgQty) AS NgQty,
                             ISNULL(CAST(SUM(ORE.GoodQty) AS FLOAT) / NULLIF(CAST(SUM(OP.OrderQty) AS FLOAT),0) * 100, 0) AS WorkRate,
                             ISNULL(CAST(SUM(ORE.NgQty) AS FLOAT) / NULLIF(CAST(SUM(OP.OrderQty) AS FLOAT),0) * 100 ,0) AS NgRate,
							 ORE.StartDate,
							 ORE.EndDate,
							 OP.Status
                         FROM PM_OrderProcess OP
                         LEFT JOIN PM_OrderResult ORE ON OP.OrderNo = ORE.OrderNo AND OP.OrderSeq = ORE.OrderSeq AND OP.CorpId = ORE.CorpId
                         LEFT JOIN CM_Item CM ON OP.ItemCode = CM.ItemCode AND OP.CorpId = CM.CorpId
                         LEFT JOIN EQ_Machine EQ ON OP.MachineCode = EQ.MachineCode AND OP.CorpId = EQ.CorpId

                         WHERE OP.CorpId = @CorpId
                             AND CONVERT(VARCHAR(8), OP.OrderDate, 112) = @WorkDate
                             AND (@Equip = '전체' OR @Equip = '' OR EQ.MachineName = @Equip)
                             AND (@Status = '전체' OR @Status = '' OR OP.Status = @Status)

                         GROUP BY OP.OrderNo, EQ.MachineName, OP.ItemCode, CM.ItemName, ORE.StartDate, ORE.EndDate, OP.Status
                         ORDER BY OP.OrderNo
                ";

                DataTable gridDt = db.GetDataTable(query);

                if (orders == null) orders = new List<OrderProcess>();
                orders.Clear();

                if (gridDt != null && gridDt.Rows.Count > 0)
                {
                    int rowNum = 1;
                    foreach (DataRow row in gridDt.Rows)
                    {
                        OrderProcess wo = new OrderProcess();

                        wo.No = rowNum++;
                        wo.OrderNo = row["OrderNo"].ConvertString_Null_To_Empty();
                        wo.Equip = row["MachineName"].ConvertString_Null_To_Empty();
                        wo.ItemCode = row["ItemCode"].ConvertString_Null_To_Empty();
                        wo.ItemName = row["ItemName"].ConvertString_Null_To_Empty();

                        wo.OrderQty = row["OrderQty"].ConvertInt_Null_To_Zero();
                        wo.WorkQty = row["WorkQty"].ConvertInt_Null_To_Zero();
                        wo.NgQty = row["NgQty"].ConvertInt_Null_To_Zero();

                        wo.WorkRate = row["WorkRate"] == DBNull.Value ? 0.0 : Convert.ToDouble(row["WorkRate"]);
                        wo.NgRate = row["NgRate"] == DBNull.Value ? 0.0 : Convert.ToDouble(row["NgRate"]);
                        wo.StartTime = row["StartDate"] == DBNull.Value ? "-" : Convert.ToDateTime(row["StartDate"]).ToString("HH:mm");
                        wo.EndTime = row["EndDate"] == DBNull.Value ? "-" : Convert.ToDateTime(row["EndDate"]).ToString("HH:mm");
                        wo.Status = row["Status"].ConvertString_Null_To_Empty();

                        orders.Add(wo);
                    }
                }

                if (orders.Count > 5)
                {
                    gridScroll.Maximum = orders.Count - 5;
                    gridScroll.Visible = true;
                }
                else
                {
                   if (gridScroll != null)
                    {
                        gridScroll.Value = 0;
                    }
                    gridScroll.Visible = false;
                }

                // 설비별 생산 달성률 데이터
                if (orders.Count > 0)
                {
                    var groupedOrders = orders
                        .Where(o => !string.IsNullOrEmpty(o.Equip))
                        .GroupBy(o => o.Equip).ToList();

                    chartEqNames = groupedOrders.Select(g => g.Key).ToArray();
                    chartEqRates = groupedOrders.Select(g => g.Average(o => o.WorkRate)).ToArray();
                }
                else
                {
                    chartEqNames = new string[0];
                    chartEqRates = new double[0];
                }

                if (isManualSearch)
                {
                    animT = 0.0;
                    isManualSearch = false;

                    animTimer.Start();
                }
                else
                {
                    animT = 1.0;
                }

                CalcLayout();
                panelDashboard.Invalidate();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void ConvertModels(DataView dv)
        {
            cards.Clear();

            foreach (DataRowView drv in dv)
            {
                EquipCard card = new EquipCard();

                card.MachineName = drv["MachineName"].ConvertString_Null_To_Empty();
                card.ItemName = drv["ItemName"].ConvertString_Null_To_Empty();
                card.OrderQty = drv["OrderQty"].ConvertDecimal_Null_To_Zero();
                card.GoodQty = drv["GoodQty"].ConvertDecimal_Null_To_Zero();
                card.NgQty = drv["NgQty"].ConvertDecimal_Null_To_Zero();
                card.WorkRate = drv["WorkRate"].ConvertDouble_Null_To_Zero();
                card.NgRate = drv["NgRate"].ConvertDouble_Null_To_Zero();
                card.Status = drv["Status"].ConvertString_Null_To_Empty();
                card.StatusColor = GetStatusColor(card.Status);

                cards.Add(card);
            }
        }

        private Color GetStatusColor(string status)
        {
            switch (status)
            {
                case "작업중":
                    return C_GREEN;

                case "작업완료":
                    return C_BLUE;

                case "대기":
                    return C_AMBER;

                default:
                    return C_CYAN;
            }
        }

        private void DrawKpi(Graphics g)
        {
            int x = rcFilter.X;
            int y = rcFilter.Bottom + 15;

            int height = 80;
            int margin = 20;
            int width = (panelDashboard.Width - (x * 2) - (margin * 4)) / 5;

            DrawKpiBox(g, x, y, width, height, "금일 작업지시", totalOrderCnt.ToString() + "건", C_RED) ;
            x += width + margin;

            DrawKpiBox(g, x, y, width, height, "작업중", runningCnt.ToString() + "건", C_GREEN);
            x += width + margin;

            DrawKpiBox(g, x, y, width, height, "대기", waitingCnt.ToString() + "건", C_AMBER);
            x += width + margin;

            DrawKpiBox(g, x, y, width, height, "작업완료", stopCnt.ToString() + "건", C_BLUE);
            x += width + margin;

            DrawKpiBox(g, x, y, width, height, "평균 달성률", AvgWorkRate.ToString("F1") + "%", C_PURPLE);
        }

        private void DrawKpiBox(Graphics g, int x, int y, int width, int height, string title, string value, Color color)
        {
            Rectangle rect = new Rectangle(x, y, width, height);
            float radius = 5f;

            using (SolidBrush bg = new SolidBrush(Color.White))
                FillRR(g, bg, rect, radius);

            using (Pen border = new Pen(Color.LightGray))
                DrawRR(g, border, rect, radius);

            using (SolidBrush bar = new SolidBrush(color))
                FillRoundRectTop(g, bar, new Rectangle(rect.X, rect.Y, rect.Width, 6), radius);

            Font titleFont = new Font("맑은 고딕", 10);
            Font valueFont = new Font("맑은 고딕", 18, FontStyle.Bold);

            g.DrawString(title, titleFont, Brushes.Gray, rect.X + 15, rect.Y + 10);
            g.DrawString(value, valueFont, Brushes.Black, rect.X + 15, rect.Y + 35);
        }

        void DrawSecLabel(Graphics g, int x, int y, string text)
        {
            using (SolidBrush ab = new SolidBrush(C_BLUE))
                g.FillRectangle(ab, x, y + 2, 3, 13);

            using (Font f = new Font("맑은 고딕", 10, FontStyle.Bold)) using (SolidBrush b = new SolidBrush(Color.FromArgb(51, 65, 85)))
                g.DrawString(text, f, b, x + 7, y);
        }

        private void DrawCards(Graphics g)
        {
            int x = rcCards.X;
            int y = rcCards.Y;

            int width = 260;
            int height = 180;
            int margin = 20;

            foreach (var card in cards)
            {
                Rectangle rect = new Rectangle(x, y+5, width, height);

                DrawSingleCard(g, rect, card);

                x += width + margin;

                if (x + width > panelDashboard.Width - margin)
                {
                    x = rcCards.X;
                    y += height + margin;
                }
            }
        }

        private void DrawSingleCard(Graphics g, Rectangle rect, EquipCard card)
        {
            float radius = 5f;

            using (SolidBrush bg = new SolidBrush(Color.White))
            {
                FillRR(g, bg, rect, radius);
            }

            using (Pen border = new Pen(Color.LightGray))
            {
                DrawRR(g, border, rect, radius);
            }

            using (SolidBrush statusBrush = new SolidBrush(card.StatusColor))
            {
                FillRoundRectTop(g, statusBrush, new Rectangle(rect.X, rect.Y, rect.Width, 10), radius);
            }

            Font titleFont = new Font("맑은 고딕", 10, FontStyle.Bold);
            Font textFont = new Font("맑은 고딕", 9);

            g.DrawString(card.MachineName, titleFont, Brushes.Black, rect.X + 10, rect.Y + 15);


            string sStatus = card.Status != null ? card.Status.Trim() : "";
            Color stBg, stFg;

            if (sStatus == "작업중") { stBg = Color.FromArgb(220, 252, 231); stFg = C_GREEN; }
            else if (sStatus == "작업완료") { stBg = Color.FromArgb(254, 226, 226); stFg = C_RED; }
            else { stBg = Color.FromArgb(254, 249, 195); stFg = C_AMBER; }

            using (Font fs = new Font("맑은 고딕", 8.5f, FontStyle.Bold))
            {
                SizeF bsz = g.MeasureString(sStatus, fs);
                int badgeWidth = (int)bsz.Width + 12;
                int badgeHeight = 20;

                int badgeX = rect.Right - 10 - badgeWidth;
                int badgeY = rect.Y + 13;

                Rectangle br2 = new Rectangle(badgeX, badgeY, badgeWidth, badgeHeight);

                using (SolidBrush sb = new SolidBrush(stBg))
                    FillRR(g, sb, br2, 8);

                using (SolidBrush sfb = new SolidBrush(stFg))
                    g.DrawString(sStatus, fs, sfb, br2.X + 6, br2.Y + 3);
            }

            using (StringFormat sf = new StringFormat())
            {
                sf.Trimming = StringTrimming.EllipsisCharacter; // 영역 넘어가면 ... 처리
                sf.FormatFlags = StringFormatFlags.NoWrap;      // 밑으로 줄바꿈 방지

                RectangleF textRect = new RectangleF(rect.X + 10, rect.Y + 45, rect.Width - 20, 20);

                g.DrawString($"품목 {card.ItemName}", textFont, Brushes.Black, textRect, sf);
            }

            using (StringFormat sfRight = new StringFormat())
            {
                sfRight.Alignment = StringAlignment.Far;

                RectangleF rectPlan = new RectangleF(rect.X + 10, rect.Y + 70, rect.Width - 20, 20);
                g.DrawString("계획", textFont, Brushes.Black, rectPlan);
                g.DrawString($"{card.OrderQty:N0} EA", textFont, Brushes.Black, rectPlan, sfRight);

                RectangleF rectGood = new RectangleF(rect.X + 10, rect.Y + 95, rect.Width - 20, 20);
                g.DrawString("양품", textFont, Brushes.Black, rectGood);
                g.DrawString($"{card.GoodQty:N0} EA", textFont, Brushes.Green, rectGood, sfRight);

                RectangleF rectNg = new RectangleF(rect.X + 10, rect.Y + 120, rect.Width - 20, 20);
                g.DrawString("불량", textFont, Brushes.Black, rectNg);
                g.DrawString($"{card.NgQty:N0} EA", textFont, Brushes.Red, rectNg, sfRight);

                RectangleF rectRate = new RectangleF(rect.X + 10, rect.Y + 145, rect.Width - 20, 20);
                //g.DrawString("달성률", textFont, Brushes.Blue, rectRate);
                g.DrawString($"{card.WorkRate:F1} %", textFont, Brushes.DarkGray, rectRate, sfRight);
            }

            // 진행률 BAR
            double safeRate = Math.Min(card.WorkRate, 100);
            int barWidth = (int)((rect.Width - 20) * (safeRate / 100.0));

            g.FillRectangle(
                Brushes.Green,
                rect.X + 10,
                rect.Y + 165,
                barWidth,
                8
            );

            // BAR 테두리
            g.DrawRectangle(
                Pens.LightGray,
                rect.X + 10,
                rect.Y + 165,
                rect.Width - 20,
                8
            );
        }

        void DrawGrid(Graphics g)
        {
            string[] colNames = { "순번", "작업지시번호", "설비", "품목코드", "품목명", "계획수량", "생산수량", "달성률(%)", "불량수량", "불량률(%)", "시작시간", "종료시간", "작업상태" };
            int[] colRatio = { 30, 100, 70, 70, 150, 55, 55, 50, 50, 50, 50, 50, 50 };

            DrawPanel(g, rcGrid, 10);
            int totalR = 0; foreach (int r in colRatio) totalR += r;
            int avail = rcGrid.Width - 2; int[] cw = new int[colRatio.Length];

            for (int i = 0; i < colRatio.Length; i++) cw[i] = (int)((double)colRatio[i] / totalR * avail);
            int theadH = 30, rowH = 24, gx0 = rcGrid.X + 1, gy0 = rcGrid.Y + 1;

            using (LinearGradientBrush hb = new LinearGradientBrush(new Rectangle(gx0, gy0, avail, theadH), C_ROWHOV, C_ROWALT, LinearGradientMode.Horizontal))
                FillRoundRectTop(g, hb, new Rectangle(gx0, gy0, avail, theadH), 10);

            using (Font fh = new Font("맑은 고딕", 9, FontStyle.Bold)) using (SolidBrush hb = new SolidBrush(C_MUTED))
            {
                StringFormat sf = new StringFormat(); sf.Alignment = StringAlignment.Center; sf.LineAlignment = StringAlignment.Center;
                int hx = gx0;
                for (int i = 0; i < colNames.Length; i++)
                {
                    g.DrawString(colNames[i], fh, hb, new RectangleF(hx + 1, gy0 + 1, cw[i] - 2, theadH - 2), sf);
                    if (i < colNames.Length - 1) { using (Pen dp = new Pen(Color.FromArgb(60, 255, 255, 255), 1))
                            g.DrawLine(dp, hx + cw[i], gy0 + 4, hx + cw[i], gy0 + theadH - 4); }
                    hx += cw[i];
                }
            }
            using (Pen hp = new Pen(C_BORDER, 1)) g.DrawLine(hp, gx0, gy0 + theadH, gx0 + avail, gy0 + theadH);

            using (Font fd = new Font("맑은 고딕", 9))
            {
                StringFormat sfL = new StringFormat(); sfL.Alignment = StringAlignment.Near; sfL.LineAlignment = StringAlignment.Center; sfL.Trimming = StringTrimming.EllipsisCharacter;
                StringFormat sfC = new StringFormat(); sfC.Alignment = StringAlignment.Center; sfC.LineAlignment = StringAlignment.Center;
                bool[] lft = { false, false, true, true, true, false, false, false, false, false, false, false, false };

                int displayRowCount = 5;
                int startRow = gridScroll.Visible ? gridScroll.Value : 0;
                int endRow = Math.Min(orders.Count, startRow + displayRowCount);

                for (int r = startRow; r < endRow; r++)
                {
                    int displayIdx = r - startRow;

                    OrderProcess wo = orders[r];

                    int ry = gy0 + theadH + displayIdx * rowH;

                    // 그리드 배경색 (선택된 행, 마우스 오버, 짝수/홀수 줄 무늬)
                    Color bg = (selRow == r) ? Color.FromArgb(210, 228, 252) : (hoverRow == r) ? C_ROWHOV : (r % 2 == 1) ? C_ROWALT : C_PANEL;
                    using (SolidBrush rb = new SolidBrush(bg)) g.FillRectangle(rb, gx0, ry, avail, rowH);
                    using (Pen lp = new Pen(C_GRIDLN, 1)) g.DrawLine(lp, gx0, ry + rowH - 1, gx0 + avail, ry + rowH - 1);

                    // 그리드 데이터 셀 바인딩
                    string[] cells = { wo.No.ToString(), wo.OrderNo, wo.Equip, wo.ItemCode, wo.ItemName,
                        wo.OrderQty.ToString("N0"), wo.WorkQty.ToString("N0"), wo.WorkRate.ToString("F1"), wo.NgQty.ToString("N0"), wo.NgRate.ToString("F1"), wo.StartTime, wo.EndTime, "" };

                    int cx2 = gx0;
                    for (int c = 0; c < cells.Length; c++)
                    {
                        if (c == 12)
                        {
                            // 상태 뱃지
                            Color stBg, stFg;
                            string safeStatus = wo.Status != null ? wo.Status.Trim() : "";

                            if (wo.Status == "작업중") { stBg = Color.FromArgb(220, 252, 231); stFg = C_GREEN; }
                            else if (wo.Status == "작업완료") { stBg = Color.FromArgb(254, 226, 226); stFg = C_RED; }
                            else { stBg = Color.FromArgb(254, 249, 195); stFg = C_AMBER; }

                            using (Font fs = new Font("맑은 고딕", 8.5f, FontStyle.Bold))
                            {
                                SizeF bsz = g.MeasureString(wo.Status, fs);
                                Rectangle br2 = new Rectangle(cx2 + (cw[c] - (int)bsz.Width - 12) / 2, ry + 3, (int)bsz.Width + 12, rowH - 6);
                                using (SolidBrush sb = new SolidBrush(stBg)) FillRR(g, sb, br2, 8);
                                using (SolidBrush sfb = new SolidBrush(stFg)) g.DrawString(wo.Status, fs, sfb, br2.X + 6, br2.Y + 2);
                            }
                        }
                        else
                        {
                            bool isRed = (c == 9 && wo.NgRate > 2.0);
                            bool isGreen = (c == 7 && wo.WorkRate > 60.0);
                            Color tc = isRed ? C_RED : isGreen ? C_GREEN : C_TEXT;
                            using (SolidBrush tb = new SolidBrush(tc))
                                g.DrawString(cells[c], fd, tb, new RectangleF(cx2 + 3, ry + 1, cw[c] - 6, rowH - 2), lft[c] ? sfL : sfC);
                        }

                        // 세로 구분선
                        if (c < cells.Length - 1) { using (Pen cp = new Pen(C_GRIDLN, 1)) g.DrawLine(cp, cx2 + cw[c], ry, cx2 + cw[c], ry + rowH); }
                        cx2 += cw[c];
                    }
                }
            }
            using (Pen op = new Pen(C_BORDER, 1)) DrawRR(g, op, rcGrid, 10);
        }

        void DrawChartLeft(Graphics g, Rectangle r)
        {
            DrawPanel(g, r, 10);

            using (Font f = new Font("맑은 고딕", 10, FontStyle.Bold)) using (SolidBrush b = new SolidBrush(Color.FromArgb(30, 58, 95)))
            { SizeF sz = g.MeasureString("설비별 생산 달성률 (%)", f); g.DrawString("설비별 생산 달성률 (%)", f, b, r.X + (r.Width - sz.Width) / 2, r.Y + 10); }

            int pL = 42, pB = 22, pT = 34, pR = 12;
            int cx = r.X + pL, cy = r.Y + pT, cw = r.Width - pL - pR, ch = r.Height - pT - pB;

            string[] eq = chartEqNames;
            double[] vs = chartEqRates;

            Color[] bcs = { C_BLUE, C_GREEN, C_RED, C_PURPLE, C_CYAN };
            double maxV = 100;

            using (Pen gp = new Pen(C_GRIDLN, 1)) using (Font f = new Font("맑은 고딕", 7)) using (SolidBrush mb = new SolidBrush(C_MUTED))
            {
                for (int i = 0; i <= 5; i++)
                {
                    float ly = cy + ch - (float)i / 5 * ch;
                    g.DrawLine(gp, cx, ly, cx + cw, ly);
                    string vs2 = ((int)(i / 5.0 * maxV)).ToString() + "%";
                    SizeF sz = g.MeasureString(vs2, f);
                    g.DrawString(vs2, f, mb, cx - sz.Width - 3, ly - sz.Height / 2);
                }
            }

            if (eq != null && eq.Length > 0)
            {
                int gW = cw / eq.Length, bW = Math.Max(18, gW * 2 / 3);
                for (int i = 0; i < eq.Length; i++)
                {
                    int gx2 = cx + i * gW + gW / 2 - bW / 2;

                    //int bh = (int)(ch * vs[i] / maxV);
                    int bh = (int)(ch * vs[i] / maxV * animT);

                    Color c = bcs[i % bcs.Length];

                    if (bh > 0)
                    {
                        using (LinearGradientBrush lb = new LinearGradientBrush(new Rectangle(gx2, cy + ch - bh, bW, bh), LightenColor(c, 40), bcs[i % bcs.Length], LinearGradientMode.Vertical))
                            FillRBar(g, lb, gx2, cy + ch - bh, bW, bh, 4);
                        using (Font f = new Font("맑은 고딕", 8, FontStyle.Bold)) using (SolidBrush wb = new SolidBrush(Color.White))
                        { string lbl = vs[i].ToString("F1") + "%"; SizeF sz = g.MeasureString(lbl, f); if (bh > 16) g.DrawString(lbl, f, wb, gx2 + (bW - sz.Width) / 2, cy + ch - bh + 4); }
                    }
                    using (Font f = new Font("맑은 고딕", 7.5f)) using (SolidBrush mb = new SolidBrush(C_MUTED))
                    { SizeF sz = g.MeasureString(eq[i], f); g.DrawString(eq[i], f, mb, cx + i * gW + gW / 2 - sz.Width / 2, cy + ch + 4); }
                }
            }
        }

        void DrawChartRight(Graphics g, Rectangle r)
        {
            DrawPanel(g, r, 10);

            using (Font f = new Font("맑은 고딕", 10, FontStyle.Bold)) using (SolidBrush b = new SolidBrush(Color.FromArgb(30, 58, 95)))
            { SizeF sz = g.MeasureString("작업 상태 현황", f); g.DrawString("작업 상태 현황", f, b, r.X + (r.Width - sz.Width) / 2, r.Y + 10); }

            int dia = Math.Min(r.Width - 80, r.Height - 50);
            if (dia <= 0) return;

            int cx = r.X + 30 + dia / 2, cy = r.Y + r.Height / 2 + 10;
            float outer = dia / 2f, inner = outer * 0.55f;

            string[] labels = { "작업중", "대기", "작업완료" };
            int[] vals = { runningCnt, waitingCnt, stopCnt };
            int total = runningCnt + waitingCnt + stopCnt;

            Color[] bcs = { C_GREEN, C_AMBER, C_BLUE };
            float startA = -90f;

            if (total > 0)
            {
                for (int i = 0; i < vals.Length; i++)
                {
                    //float sweep = 360f * vals[i] / total;
                    float sweep = 360f * vals[i] / total * (float)animT;

                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(210, bcs[i])))
                        g.FillPie(sb, cx - outer, cy - outer, dia, dia, startA, sweep);
                    startA += 360f * vals[i] / total;
                }
            }
            else
            {
                // 데이터가 없을 때
                using (SolidBrush emptyBrush = new SolidBrush(Color.FromArgb(230, 230, 230)))
                    g.FillEllipse(emptyBrush, cx - outer, cy - outer, dia, dia);
            }

            using (SolidBrush hb = new SolidBrush(C_PANEL)) g.FillEllipse(hb, cx - inner, cy - inner, inner * 2, inner * 2);

            Color textColor = Color.FromArgb(30, 58, 95);

            using (Font fTotalLabel = new Font("맑은 고딕", 9))
            using (Font fTotalNum = new Font("맑은 고딕", 16, FontStyle.Bold))
            using (SolidBrush tb = new SolidBrush(textColor))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                string totalNumText = total.ToString("N0") + "건";

                g.DrawString("전체", fTotalLabel, tb, cx, cy - 12, sf);
                g.DrawString(totalNumText, fTotalNum, tb, cx, cy + 10, sf);
            }

            // 범례
            int lx = cx + (int)outer + 14, ly = cy - 30;
            using (Font f = new Font("맑은 고딕", 9)) using (SolidBrush mb = new SolidBrush(C_MUTED))
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    using (SolidBrush cb = new SolidBrush(bcs[i])) g.FillEllipse(cb, lx, ly + i * 24 + 2, 12, 12);

                    double pct = (total > 0) ? ((double)vals[i] / total * 100.0) : 0.0;
                    string legendText = $"{labels[i]} {vals[i]:N0}건 ({pct:F1}%)";

                    g.DrawString(legendText, f, mb, lx + 16, ly + i * 24);
                }
            }
        }

        static Color LightenColor(Color c, int amt) { return Color.FromArgb(Math.Min(255, c.R + amt), Math.Min(255, c.G + amt), Math.Min(255, c.B + amt)); }
        void DrawPanel(Graphics g, Rectangle r, float rad) { using (SolidBrush b = new SolidBrush(C_PANEL)) FillRR(g, b, r, rad); using (Pen p = new Pen(C_BORDER, 1)) DrawRR(g, p, r, rad); }
        static void FillRR(Graphics g, Brush br, Rectangle r, float rad) { using (GraphicsPath path = MkPath(new RectangleF(r.X, r.Y, r.Width, r.Height), rad)) g.FillPath(br, path); }
        static void DrawRR(Graphics g, Pen pen, Rectangle r, float rad) { using (GraphicsPath path = MkPath(new RectangleF(r.X, r.Y, r.Width, r.Height), rad)) g.DrawPath(pen, path); }
        static void FillRoundRectTop(Graphics g, Brush br, Rectangle r, float rad) { float d = rad * 2; using (GraphicsPath p = new GraphicsPath()) { p.AddArc(r.X, r.Y, d, d, 180, 90); p.AddArc(r.Right - d, r.Y, d, d, 270, 90); p.AddLine(r.Right, r.Y + d, r.Right, r.Bottom); p.AddLine(r.Right, r.Bottom, r.X, r.Bottom); p.AddLine(r.X, r.Bottom, r.X, r.Y + d); p.CloseFigure(); g.FillPath(br, p); } }
        static void FillRBar(Graphics g, Brush br, int x, int y, int w, int h, int rad) { if (w <= 0 || h <= 0) return; if (w < rad * 2) rad = w / 2; if (h < rad * 2) rad = h / 2; float d = rad * 2; using (GraphicsPath p = new GraphicsPath()) { p.AddArc(x, y, d, d, 180, 90); p.AddArc(x + w - d, y, d, d, 270, 90); p.AddArc(x + w - d, y + h - d, d, d, 0, 90); p.AddArc(x, y + h - d, d, d, 90, 90); p.CloseFigure(); g.FillPath(br, p); } }
        static GraphicsPath MkPath(RectangleF r, float rad) { float d = rad * 2; GraphicsPath p = new GraphicsPath(); p.AddArc(r.X, r.Y, d, d, 180, 90); p.AddArc(r.Right - d, r.Y, d, d, 270, 90); p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90); p.AddArc(r.X, r.Bottom - d, d, d, 90, 90); p.CloseFigure(); return p; }
        void OnMM(object sender, MouseEventArgs e) { if (!rcGrid.Contains(e.Location)) { if (hoverRow != -1) { hoverRow = -1; panelDashboard.Invalidate(); } return; } int n = (e.Y - rcGrid.Y - 1 - 30) / 24; if (n != hoverRow) { hoverRow = n; panelDashboard.Invalidate(); } }
        void OnMC(object sender, MouseEventArgs e) { if (!rcGrid.Contains(e.Location)) return; int y = e.Y - rcGrid.Y - 1 - 30; if (y >= 0) { selRow = y / 24; panelDashboard.Invalidate(); } }
        void panelDashboard_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!gridScroll.Visible) return;

            if (rcGrid.Contains(e.Location))
            {
                // 한 번 휠을 굴릴 때 1칸씩 이동
                int step = (e.Delta > 0) ? -1 : 1;
                int newValue = gridScroll.Value + step;

                if (newValue < gridScroll.Minimum) newValue = gridScroll.Minimum;
                if (newValue > gridScroll.Maximum) newValue = gridScroll.Maximum;

                // 값이 변경되었을 때 스크롤바 위치 업데이트
                if (gridScroll.Value != newValue)
                {
                    gridScroll.Value = newValue;
                    hoverRow = -1;
                    panelDashboard.Invalidate(rcGrid);
                }
            }
        }

        private void Frm_PM_ProductionProgressMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (refreshTimer != null) { refreshTimer.Stop(); refreshTimer.Dispose(); }
            if (clockTimer != null) { clockTimer.Stop(); clockTimer.Dispose(); }
            if (animTimer != null) { animTimer.Stop(); animTimer.Dispose(); }
        }
    }
}