using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Management
{
    public partial class Frm_MM_Dashboard_Total : DevExpress.XtraEditors.XtraForm
    {
        private readonly clsGrid grid = new clsGrid();
        private readonly clsRepository rpo = new clsRepository();
        private readonly Database db = new Database();
        string query = "";

        public Frm_MM_Dashboard_Total()
        {
            InitializeComponent();
        }

        private void Frm_MM_Dashboard_Total_Load(object sender, EventArgs e)
        {
            SetGrid(GvST);
            Search();
        }

        #region 데이터 조회 (Search)
        private void Search()
        {
            string stMM = DateTime.Now.ToString("yyyy-MM-01");
            string edMM = DateTime.Now.ToString("yyyy-MM-dd");
            string past10Days = DateTime.Now.AddDays(-10).ToString("d");
            string today = DateTime.Now.ToString("d");

            Search_Chart_NgPerAndCnt();     //실시간 생산현황
            Search_MaintainRate();          //보전지수

            Search_QM_Rate();               //실시간 품질현황
            Search_QM_NgTop5();             //실시간 품질현황

            Search_ST_List();
            Search_EQ_OEE();
            Search_MM_Rate();
            Search_Chart_Sales();
        }
        #endregion

        #region 실시간 생산현황
        private void Search_Chart_NgPerAndCnt()
        {
            // 지시 없는 실적은 집계 대상 아님
            query = $@"
                        SELECT TOP 5
                            C.Title,
                            SUM(P.OrderQty) AS OrderQty,
                            SUM(A.WorkQty) AS WorkQty,
                            CASE 
                                WHEN SUM(P.OrderQty) = 0 THEN 0
                                ELSE ROUND(SUM(A.WorkQty) * 100.0 / SUM(P.OrderQty), 1)
                            END AS AchieveRate
                        FROM (
                            SELECT CorpId, ItemCode, SUM(WorkQty) AS WorkQty
                            FROM PM_OrderResult
                            WHERE CONVERT(date, WorkDate) = CONVERT(date, GETDATE())
                            GROUP BY CorpId, ItemCode
                        ) A
                        INNER JOIN (
                            SELECT CorpId, ItemCode, SUM(OrderQty) AS OrderQty
                            FROM PM_OrderProcess
                            GROUP BY CorpId, ItemCode
                        ) P
                            ON A.CorpId = P.CorpId AND A.ItemCode = P.ItemCode
                        LEFT JOIN CM_Item B ON A.CorpId = B.CorpId AND A.ItemCode = B.ItemCode
                        LEFT JOIN CM_CateItem C ON B.CorpId = C.CorpId AND B.ItemClass1 = C.ClassItemCode AND C.Class = 'A01210'
                        WHERE A.CorpId = '{WrGlobal.CorpID}'
                        GROUP BY C.Title
                        ORDER BY AchieveRate ASC
                    ";

            DataTable dtNgType = db.GetDataTable(query);

            // 라벨 값
            double totalQty = 0;
            double totalOrderQty = 0;
            double achieveRate = 0;

            if (dtNgType.Rows.Count > 0)
            {
                totalQty = Convert.ToDouble(dtNgType.Compute("SUM(WorkQty)", ""));
                totalOrderQty = Convert.ToDouble(dtNgType.Compute("SUM(OrderQty)", ""));

                if (totalOrderQty > 0)
                    achieveRate = Math.Round((totalQty / totalOrderQty) * 100, 1);
            }

            lblTotalQty.Text = totalQty.ToString("#,##0");
            lblAchieveRate.Text = achieveRate.ToString("0.0") + "%";

            ChartPM.DataSource = dtNgType;
            ChartPM.Series.Clear();
            ChartPM.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series seriesNgType = new Series("달성률", ViewType.Bar);

            // 왼쪽 품목분류명
            seriesNgType.ArgumentDataMember = "Title";

            // 퍼센트 값
            seriesNgType.ValueDataMembers.AddRange("AchieveRate");

            // 막대 끝 라벨 표시
            seriesNgType.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            ChartPM.Series.Add(seriesNgType);

            // 바 설정
            BarSeriesView barView = (BarSeriesView)seriesNgType.View;
            barView.ColorEach = true;
            barView.BarWidth = 0.45;
            barView.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // 퍼센트 라벨 (막대 안)
            BarSeriesLabel label = (BarSeriesLabel)seriesNgType.Label;
            label.TextPattern = "{V}%";
            label.Position = BarSeriesLabelPosition.Center;
            label.TextOrientation = TextOrientation.Horizontal;
            label.TextColor = Color.White;
            label.BackColor = Color.Transparent;
            label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // ToolTip 데이터용
            seriesNgType.ToolTipPointPattern = "{A}\n달성률 : {V}%\n생산수량 : {TV:#,##0}";

            // 차트
            XYDiagram diagramNgTye = (XYDiagram)ChartPM.Diagram;

            // 가로 막대
            diagramNgTye.Rotated = true;

            // 배경
            diagramNgTye.DefaultPane.BackColor = Color.Transparent;
            diagramNgTye.DefaultPane.BorderVisible = false;

            // 줄눈 제거
            diagramNgTye.AxisX.GridLines.Visible = false;
            diagramNgTye.AxisY.GridLines.Visible = false;

            // 눈금 제거
            diagramNgTye.AxisX.Tickmarks.Visible = false;
            diagramNgTye.AxisX.Tickmarks.MinorVisible = false;

            diagramNgTye.AxisY.Tickmarks.Visible = false;
            diagramNgTye.AxisY.Tickmarks.MinorVisible = false;

            // 축선 제거
            diagramNgTye.AxisX.VisibleInPanesSerializable = "-1";
            diagramNgTye.AxisY.VisibleInPanesSerializable = "-1";

            // 범위
            diagramNgTye.AxisX.VisualRange.SetMinMaxValues(0, 100);
            diagramNgTye.AxisX.WholeRange.SetMinMaxValues(0, 100);

            // 퍼센트 높은 순 위쪽
            diagramNgTye.AxisY.Reverse = false;
        }
        #endregion

        #region 보전지수
        private void Search_MaintainRate()
        {
            string query = $@"
                            -- =========================================
                            -- 보전지수 / 누적가동시간 조회 (금일 기준)
                            -- =========================================
                            SELECT
                                -- 보전지수(%) = 순수가동시간 / 전체작업시간 * 100
                                ISNULL(
                                    ROUND(
                                        SUM(T.RunTime) * 100.0 / NULLIF(SUM(T.OperTime), 0)
                                    , 2)
                                , 0) AS MaintainRate,

                                -- 누적 가동시간 (시간)
                                ISNULL(
                                    ROUND(SUM(T.RunTime) / 60.0, 1)
                                , 0) AS RunHour

                            FROM
                            (
                                -- =========================================
                                -- 생산실적 기준 전체 작업시간 / 가동시간 계산
                                -- =========================================
                                SELECT
                                    A.OrderNo,
                                    A.OrderSeq,
                                    A.SubSeq,

                                    -- 작업 전체 시간 (분)
                                    DATEDIFF(MINUTE, A.StartDate, A.EndDate) AS OperTime,

                                    -- 순수 가동시간 = 작업시간 - 비가동시간
                                    DATEDIFF(MINUTE, A.StartDate, A.EndDate)
                                    -
                                    ISNULL(
                                        CASE
                                            -- 비가동 시작이 작업 구간 내부
                                            WHEN B.StartTime BETWEEN A.StartDate AND A.EndDate THEN
                                                DATEDIFF(
                                                    MINUTE,
                                                    B.StartTime,
                                                    CASE
                                                        WHEN B.EndTime < A.EndDate THEN B.EndTime
                                                        ELSE A.EndDate
                                                    END
                                                )

                                            -- 비가동 종료가 작업 구간 내부
                                            WHEN B.EndTime BETWEEN A.StartDate AND A.EndDate THEN
                                                DATEDIFF(
                                                    MINUTE,
                                                    CASE
                                                        WHEN B.StartTime > A.StartDate THEN B.StartTime
                                                        ELSE A.StartDate
                                                    END,
                                                    B.EndTime
                                                )

                                            -- 작업 시작이 비가동 구간 내부
                                            WHEN A.StartDate BETWEEN B.StartTime AND B.EndTime THEN
                                                DATEDIFF(
                                                    MINUTE,
                                                    A.StartDate,
                                                    CASE
                                                        WHEN A.EndDate < B.EndTime THEN A.EndDate
                                                        ELSE B.EndTime
                                                    END
                                                )

                                            -- 작업 종료가 비가동 구간 내부
                                            WHEN A.EndDate BETWEEN B.StartTime AND B.EndTime THEN
                                                DATEDIFF(
                                                    MINUTE,
                                                    CASE
                                                        WHEN A.StartDate > B.StartTime THEN B.StartTime
                                                        ELSE B.StartTime
                                                    END,
                                                    A.EndDate
                                                )

                                            ELSE 0
                                        END
                                    ,0) AS RunTime

                                FROM PM_OrderResult A
                                LEFT JOIN EQ_Operation B ON A.OrderNo = B.OrderNo AND A.OrderSeq = B.OrderSeq
                                WHERE A.CorpId = '{WrGlobal.CorpID}'
                                  AND CONVERT(date, A.WorkDate) = CONVERT(date, GETDATE())
                            ) T
                            ";

            DataTable dtMainData = db.GetDataTable(query);

            if (dtMainData.Rows.Count > 0)
            {
                double maintainRate = Convert.ToDouble(dtMainData.Rows[0]["MaintainRate"]);
                double runHour = Convert.ToDouble(dtMainData.Rows[0]["RunHour"]);

                // 소수 반올림 통일
                maintainRate = Math.Round(maintainRate, 1);

                // 게이지
                circularGauge2.BeginUpdate();

                // Scale 설정
                arcScaleComponent1.MinValue = 0;
                arcScaleComponent1.MaxValue = 100;

                // 값 적용
                arcScaleComponent4.Value = (float)maintainRate;
                circularGauge2.EndUpdate();

                // 라벨
                lblMachineRate.Text = maintainRate.ToString("0.0") + "%";
                lblRunHour.Text = runHour.ToString("0.0") + " H";
            }
        }
        #endregion

        #region 설비 종합효율 타일

        #region 조회
        private void Search_EQ_OEE()
        {
            query = $@"
                        SELECT
                            ROUND(SUM(T.RunTime) * 100.0 / NULLIF(SUM(T.OperTime), 0), 1) AS RunRate,
                            ROUND(SUM(T.GoodQty) * 100.0 / NULLIF(SUM(T.WorkQty), 0), 1) AS GoodRate,
                            ROUND(
                                (SUM(T.RunTime) * 100.0 / NULLIF(SUM(T.OperTime), 0))
                                *
                                (SUM(T.GoodQty) * 100.0 / NULLIF(SUM(T.WorkQty), 0))
                                / 100
                            , 1) AS OEE
                        FROM
                        (
                            SELECT
                                DATEDIFF(MINUTE, A.StartDate, A.EndDate) AS OperTime,
                                DATEDIFF(MINUTE, A.StartDate, A.EndDate) - ISNULL(B.StopMinute, 0) AS RunTime,
                                A.WorkQty,
                                A.WorkQty - ISNULL(A.NgQty, 0) AS GoodQty
                            FROM PM_OrderResult A
                            LEFT JOIN
                            (
                                SELECT OrderNo, OrderSeq,
                                       SUM(DATEDIFF(MINUTE, StartTime, EndTime)) AS StopMinute
                                FROM EQ_Operation
                                GROUP BY OrderNo, OrderSeq
                            ) B
                            ON A.OrderNo = B.OrderNo AND A.OrderSeq = B.OrderSeq
                            WHERE A.CorpId = '{WrGlobal.CorpID}'
                              AND CONVERT(date, A.WorkDate) = CONVERT(date, GETDATE())
                        ) T";

            DataRow dr = db.GetDataRow(query);
            if (dr == null) return;

            double runRate = dr["RunRate"].ConvertDouble_Null_To_Zero();
            double goodRate = dr["GoodRate"].ConvertDouble_Null_To_Zero();
            double ngRate = 100 - goodRate;
            double oee = dr["OEE"].ConvertDouble_Null_To_Zero();

            tableLayoutPanel4.Controls.Clear();

            AddKpiToGrid("시간가동률", runRate, Color.FromArgb(52, 152, 219), 0);
            AddKpiToGrid("양품률", goodRate, Color.FromArgb(46, 204, 113), 1);
            AddKpiToGrid("불량률", ngRate, Color.FromArgb(231, 76, 60), 2);
            AddKpiToGrid("설비종합효율", oee, Color.FromArgb(155, 89, 182), 3);

            lblEquipOEE.Text = oee.ToString("0.0") + "%";
        }
        #endregion

        #region 패널 생성
        private void AddKpiToGrid(string title, double value, Color color, int cellIndex)
        {
            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(6)
            };

            Panel leftBar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 5,
                BackColor = color
            };

            Label lblTitle = new Label
            {
                Text = title,
                Dock = DockStyle.Left,
                Width = 160,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("맑은 고딕", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 70, 70),
                Padding = new Padding(10, 0, 0, 0)
            };

            Label lblValue = new Label
            {
                Text = $"{value:0.0}%",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("맑은 고딕", 11, FontStyle.Bold),
                ForeColor = color,
                Padding = new Padding(0, 0, 12, 0)
            };

            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            card.Controls.Add(leftBar);

            card.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(
                    e.Graphics,
                    card.ClientRectangle,
                    Color.FromArgb(210, 210, 210),
                    ButtonBorderStyle.Solid
                );
            };

            int col = cellIndex;   // 0~3
            int row = 0;

            tableLayoutPanel4.Controls.Add(card, col, row);
        }
        #endregion

        #endregion

        #region 실시간 품질현황

        #region 양품율/불량율
        private void Search_QM_Rate()
        {
            query = $@"
                        SELECT
                                CASE
                                    WHEN COUNT(*) = 0 THEN 0
                                    ELSE ROUND(
                                        SUM(CASE WHEN IsPass = 1 THEN 1 ELSE 0 END) * 100.0
                                        / COUNT(*)
                                    , 1)
                                END AS PassRate,

                                CASE
                                    WHEN COUNT(*) = 0 THEN 0
                                    ELSE ROUND(
                                        SUM(CASE WHEN IsPass = 0 THEN 1 ELSE 0 END) * 100.0
                                        / COUNT(*)
                                    , 1)
                                END AS NgRate

                            FROM
                            (
                                SELECT
                                    RefNo,
                                    RefSeq,

                                    CASE
                                        WHEN SUM(CASE WHEN PassDivCd = '10' THEN 0 ELSE 1 END) = 0
                                        THEN 1
                                        ELSE 0
                                    END AS IsPass

                                FROM QM_ExamResult
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                  --AND ExamDivCd = '30'
                                  AND CONVERT(date, ExamDate) >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)

                                GROUP BY
                                    RefNo,
                                    RefSeq
                            ) A
            ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                double goodRate = Convert.ToDouble(dr["PassRate"]);
                double ngRate = Convert.ToDouble(dr["NgRate"]);

                lblGoodRate.Text = goodRate.ToString("0.0") + "%";
                lblNgRate.Text = ngRate.ToString("0.0") + "%";
            }
        }
        #endregion

        #region 품목별 불량 WORST TOP 5 
        private void Search_QM_NgTop5()
        {
            // 수량 애매함
            query = $@"
                        SELECT TOP 5
                                B.ItemName,
                                COUNT(*) AS NgCnt
                        FROM
                        (
                            SELECT
                                ItemCode,
                                RefNo,
                                RefSeq,

                                CASE
                                    WHEN SUM(CASE WHEN PassDivCd = '10' THEN 0 ELSE 1 END) = 0
                                    THEN 1
                                    ELSE 0
                                END AS IsPass

                            FROM QM_ExamResult
                            WHERE CorpId = '{WrGlobal.CorpID}'
                                --AND ExamDivCd = '30'
                                AND CONVERT(date, ExamDate) >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)

                            GROUP BY
                                ItemCode,
                                RefNo,
                                RefSeq
                        ) A
                        LEFT JOIN CM_Item B
                            ON A.ItemCode = B.ItemCode
                        WHERE A.IsPass = 0
                        GROUP BY B.ItemName
                        ORDER BY NgCnt DESC
            ";

            DataTable dt = db.GetDataTable(query);

            Chart_QM.DataSource = dt;
            Chart_QM.Series.Clear();
            Chart_QM.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series series = new Series("불량", ViewType.Bar);

            // 품목명
            series.ArgumentDataMember = "ItemName";

            // 불량건수
            series.ValueDataMembers.AddRange("NgCnt");

            // 라벨 표시
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            Chart_QM.Series.Add(series);

            // 바 설정
            BarSeriesView barView = (BarSeriesView)series.View;
            barView.ColorEach = true;
            barView.BarWidth = 0.45;
            barView.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // 라벨
            BarSeriesLabel label = (BarSeriesLabel)series.Label;
            label.TextPattern = "{V}";
            label.Position = BarSeriesLabelPosition.Center;
            label.TextOrientation = TextOrientation.Horizontal;
            label.TextColor = Color.White;
            label.BackColor = Color.Transparent;
            label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // 툴팁
            series.ToolTipPointPattern = "{A}\n불량건수 : {V}";

            // 차트
            XYDiagram diagram = (XYDiagram)Chart_QM.Diagram;

            // 가로 막대
            diagram.Rotated = true;

            // 배경
            diagram.DefaultPane.BackColor = Color.Transparent;
            diagram.DefaultPane.BorderVisible = false;

            // 줄눈 제거
            diagram.AxisX.GridLines.Visible = false;
            diagram.AxisY.GridLines.Visible = false;

            // 눈금 제거
            diagram.AxisX.Tickmarks.Visible = false;
            diagram.AxisX.Tickmarks.MinorVisible = false;

            diagram.AxisY.Tickmarks.Visible = false;
            diagram.AxisY.Tickmarks.MinorVisible = false;

            // 축 제거
            diagram.AxisX.VisibleInPanesSerializable = "-1";
            diagram.AxisY.VisibleInPanesSerializable = "-1";

            // 높은 순 위쪽
            diagram.AxisY.Reverse = true;
        }

        #endregion

        #endregion

        #region 납품 및 재고현황

        #region 입고율 프로그레스바 및 라벨
        private void Search_MM_Rate()
        {
            query = $@"
                        SELECT
                            -- 발주 건수
                            ISNULL((
                                SELECT COUNT(*)
                                FROM MM_PlacingM
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                    AND CONVERT(date, PlacingDate) = CONVERT(date, GETDATE())
                            ), 0) AS OrderCount,

                            -- 입고 건수
                            ISNULL((
                                SELECT COUNT(*)
                                FROM MM_PurchaseM
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                    AND CONVERT(date, StoDate) = CONVERT(date, GETDATE())
                            ), 0) AS InCount,

                            -- 출고 건수
                            ISNULL((
                                SELECT COUNT(*)
                                FROM BM_SalesM
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                    AND CONVERT(date, SalesDate) = CONVERT(date, GETDATE())
                            ), 0) AS OutCount,

                            -- 발주 수량
                            ISNULL((
                                SELECT SUM(D.Qty)
                                FROM MM_PlacingD D
                                INNER JOIN MM_PlacingM M
                                    ON D.CorpId = M.CorpId
                                    AND D.MmOrderNo = M.MmOrderNo
                                WHERE D.CorpId = '{WrGlobal.CorpID}'
                                    AND CONVERT(date, M.PlacingDate) = CONVERT(date, GETDATE())
                            ), 0) AS OrderQty,

                            -- 입고 수량
                            ISNULL((
                                SELECT SUM(D.Qty)
                                FROM MM_PurchaseD D
                                INNER JOIN MM_PurchaseM M
                                    ON D.CorpId = M.CorpId
                                    AND D.StoNo = M.StoNo
                                WHERE D.CorpId = '{WrGlobal.CorpID}'
                                    AND CONVERT(date, M.StoDate) = CONVERT(date, GETDATE())
                            ), 0) AS InQty
                    ";

            DataTable dt = db.GetDataTable(query);

            if (dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];

            // 건수
            int orderCount = Convert.ToInt32(row["OrderCount"]);
            int inCount = Convert.ToInt32(row["InCount"]);
            int outCount = Convert.ToInt32(row["OutCount"]);

            // 수량
            double orderQty = Convert.ToDouble(row["OrderQty"]);
            double inQty = Convert.ToDouble(row["InQty"]);

            // 건수 라벨
            lblOrderCount.Text = $"{orderCount:#,0} 건";
            lblInCount.Text = $"{inCount:#,0} 건";
            lblOutCount.Text = $"{outCount:#,0} 건";

            // 입고율 (수량 기준)
            double rate = 0;

            if (orderQty > 0)
                rate = (inQty / orderQty) * 100;

            progressInRate.Properties.Minimum = 0;
            progressInRate.Properties.Maximum = 100;
            progressInRate.EditValue = (int)Math.Min(rate, 100);
        }
        #endregion

        #region 입고/납품 차트
        private void Search_Chart_Sales()
        {
            query = $@"
                        SELECT
                            SUBSTRING(CONVERT(varchar(8), D.DT, 112), 5, 2) + '월'
                            + SUBSTRING(CONVERT(varchar(8), D.DT, 112), 7, 2) + '일' AS WorkDate,
                            ISNULL(S.ShipQty, 0) AS ShipQty,
                            ISNULL(P.InQty, 0) AS InQty
                        FROM
                        (
                            SELECT CONVERT(date, DATEADD(DAY, -6, GETDATE())) AS DT
                            UNION ALL SELECT CONVERT(date, DATEADD(DAY, -5, GETDATE()))
                            UNION ALL SELECT CONVERT(date, DATEADD(DAY, -4, GETDATE()))
                            UNION ALL SELECT CONVERT(date, DATEADD(DAY, -3, GETDATE()))
                            UNION ALL SELECT CONVERT(date, DATEADD(DAY, -2, GETDATE()))
                            UNION ALL SELECT CONVERT(date, DATEADD(DAY, -1, GETDATE()))
                            UNION ALL SELECT CONVERT(date, GETDATE())
                        ) D
                        LEFT JOIN
                        (
                            SELECT
                                CONVERT(date, M.SalesDate) AS DT,
                                SUM(SD.Qty) AS ShipQty
                            FROM BM_SalesM M
                            LEFT JOIN BM_SalesD SD ON M.CorpId = SD.CorpId AND M.SalesNo = SD.SalesNo
                            WHERE M.CorpId = '{WrGlobal.CorpID}'
                            GROUP BY CONVERT(date, M.SalesDate)
                        ) S
                            ON D.DT = S.DT
                        LEFT JOIN
                        (
                            SELECT
                                CONVERT(date, M.StoDate) AS DT,
                                SUM(PD.Qty) AS InQty
                            FROM MM_PurchaseM M
                            LEFT JOIN MM_PurchaseD PD ON M.CorpId = PD.CorpId AND M.StoNo = PD.StoNo
                            WHERE M.CorpId = '{WrGlobal.CorpID}'
                            GROUP BY CONVERT(date, M.StoDate)
                        ) P
                            ON D.DT = P.DT
                        ORDER BY D.DT
                        ";
            DataTable dt = db.GetDataTable(query);
            chartSales.Series.Clear();

            DataTable chartDt = new DataTable();
            chartDt.Columns.Add("WorkDate", typeof(string));
            chartDt.Columns.Add("ShipQty", typeof(double));
            chartDt.Columns.Add("InQty", typeof(double));

            foreach (DataRow dr in dt.Rows)
            {
                chartDt.Rows.Add(
                    dr["WorkDate"].ToString(),
                    Convert.ToDouble(dr["ShipQty"]),
                    Convert.ToDouble(dr["InQty"])
                );
            }

            // 출하 (Bar)
            Series seriesShip = new Series("출하", ViewType.Bar);
            seriesShip.ArgumentScaleType = ScaleType.Qualitative;
            seriesShip.DataSource = chartDt;
            seriesShip.ArgumentDataMember = "WorkDate";
            seriesShip.ValueDataMembers.AddRange("ShipQty");
            SideBySideBarSeriesView shipView = (SideBySideBarSeriesView)seriesShip.View;
            shipView.Color = Color.FromArgb(31, 97, 141);   // 진한 파랑

            seriesShip.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            BarSeriesLabel shipLabel = (BarSeriesLabel)seriesShip.Label;
            shipLabel.TextPattern = "{V:#,0}";
            shipLabel.Position = BarSeriesLabelPosition.Top;

            // 입고 (Line)
            Series seriesIn = new Series("입고", ViewType.Line);
            seriesIn.ArgumentScaleType = ScaleType.Qualitative;
            seriesIn.DataSource = chartDt;
            seriesIn.ArgumentDataMember = "WorkDate";
            seriesIn.ValueDataMembers.AddRange("InQty");
            LineSeriesView lineView = (LineSeriesView)seriesIn.View;
            lineView.Color = Color.FromArgb(93, 173, 226);  // 하늘색
            lineView.LineStyle.Thickness = 2;
            lineView.LineMarkerOptions.Visible = true;

            // Series 추가
            chartSales.Series.Add(seriesShip);
            chartSales.Series.Add(seriesIn);

            XYDiagram diagram = (XYDiagram)chartSales.Diagram;
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
            diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;

            // 스타일
            chartSales.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chartSales.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            chartSales.BackColor = Color.White;
        }
        #endregion

        #region Grid 생성
        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "창고", "Warehouse");
            grid.Create_GvColumn(gv, "품목분류", "Title");
            grid.Create_GvColumn(gv, "재고수량", "StockQty");
            grid.Create_GvColumn(gv, "포장수량", "StockBoxQty");

            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["StockBoxQty"], clsGrid.TypeList.TFloat);

            //gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            gv.OptionsSelection.EnableAppearanceFocusedRow = false;
        }
        #endregion

        #region 재고현황
        private void Search_ST_List()
        {
            query = $@"
                       SELECT
                               A.Warehouse
							   , C.Title
							   , SUM(A.StockQty) AS StockQty
							   , SUM(
									CASE
										WHEN A.PackQty IS NULL OR A.PackQty = 0 THEN 0
										ELSE CEILING(A.StockQty / A.PackQty)
									END
							   ) AS StockBoxQty
						FROM StockListView A
						LEFT JOIN CM_Item B ON A.CorpId = B.CorpId AND A.ItemCode = B.ItemCode
						LEFT JOIN CM_CateItem C ON B.CorpId = C.CorpId AND B.ItemClass1 = C.ClassItemCode AND C.Class = 'A01210'
						WHERE A.ItemType IN ('10', '20', '30')
						GROUP BY C.Title, A.Warehouse
						ORDER BY SUM(A.StockQty) DESC
                    ";

            GcST.DataSource = db.GetDataView("품목분류별 현재고현황", query);
            GvST.BestFitColumns();
        }
        #endregion

        #endregion
    }
}