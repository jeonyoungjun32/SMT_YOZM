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

namespace SmartMES.View.Management
{
    public partial class Frm_MM_Dashboard_MM : DevExpress.XtraEditors.XtraForm
    {

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        //// 상태별 게이지 바 색상 정의 (첫 번째 이미지 기반 트렌디한 컬러셋)
        //private readonly Color ColorOverStock = Color.FromArgb(0, 168, 204);   // 과잉 (파랑 계열, 예: 248%)
        //private readonly Color ColorNormalS = Color.FromArgb(46, 213, 115);   // 안전 (녹색 계열, 예: 120%)
        //private readonly Color ColorWarning = Color.FromArgb(255, 159, 26);   // 주의 (주황 계열, 예: 71%)
        //private readonly Color ColorLackStock = Color.FromArgb(255, 71, 87);   // 부족 (빨강 계열, 예: 9%)

        ////도넛차트 색
        //private readonly Color[] ModernColors = new Color[] {
        //    Color.FromArgb(0, 168, 204),   // Cyan Blue
        //    Color.FromArgb(46, 213, 115),  // Mint Green
        //    Color.FromArgb(255, 159, 26),  // Amber Orange
        //    Color.FromArgb(255, 71, 87),   // Coral Red
        //    Color.FromArgb(162, 155, 254)  // Soft Purple
        //};

        public Frm_MM_Dashboard_MM()
        {
            //this.LookAndFeel.UseDefaultLookAndFeel = false;

            // 2. 현재 화면에만 WXI Darkness 스킨 및 팔레트 적용
            //this.LookAndFeel.SetSkinStyle(DevExpress.LookAndFeel.SkinStyle.WXI, DevExpress.LookAndFeel.SkinSvgPalette.WXI.Darkness);

            InitializeComponent();
        }

        private void Frm_MM_Dashboard_MM_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            SetGrid_GvItemStock(GvItemStockList);
            Set_Chart_Doughnut(ChartStockQty);
            Set_Chart_Doughnut(ChartStockAmt);

            Search();

            Tm_Search.Interval = 60000;
            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)
        private void SetGrid_GvPreIn(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "예정일자", "DeliveryDate");
            grid.Create_GvColumn(gv, "품목", "ItemName");
            grid.Create_GvColumn(gv, "거래처", "CustName");
            grid.Create_GvColumn(gv, "수량", "Qty");

            //
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            gv.Columns["DeliveryDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
        }

        private void SetGrid_GvItemStock(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            RepositoryItemProgressBar progressBar = new RepositoryItemProgressBar();
            progressBar.Minimum = 0;      // 최소값 (0%)
            progressBar.Maximum = 100;      // 최소값 (0%)
            progressBar.ShowTitle = true;                  // 게이지 위에 텍스트 표시 여부
            progressBar.PercentView = true;                // 수치 뒤에 % 기호 자동 표시 여부
            progressBar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; // 가운데 정렬

            progressBar.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid; // 깔끔한 단색 채우기
            //progressBar.LookAndFeel.UseDefaultLookAndFeel = false;
            progressBar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D; // 플랫 스타일 적용
            //progressBar.Appearance.Options.UseBackColor = true;
            //progressBar.Appearance.BackColor = Color.LightSeaGreen;

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "재고수량", "StockQty");
            grid.Create_GvColumn(gv, "안전재고", "SaftyQty");
            //grid.Create_GvColumn(gv, "재고율", "StockPer", progressBar);
            grid.Create_GvColumn(gv, "재고율", "StockPer");
            grid.Create_GvColumn(gv, "상태", "Status");

            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["SaftyQty"], clsGrid.TypeList.TFloat);
            //grid.SetCol_Type(gv.Columns["StockPer"], clsGrid.TypeList.TFloat);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.Columns["StockQty"].MinWidth = 100;
            gv.Columns["StockQty"].MaxWidth = 100;

            gv.Columns["SaftyQty"].MinWidth = 90;
            gv.Columns["SaftyQty"].MaxWidth = 90;

            gv.Columns["Status"].MinWidth = 70;
            gv.Columns["Status"].MaxWidth = 70;

            gv.Columns["StockPer"].MinWidth = 130;
            gv.Columns["StockPer"].MaxWidth = 130;


            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += GvItemStockList_RowCellStyle;
			gv.CustomDrawCell += Gv_CustomDrawCell;
        }

	
		private void GvItemStockList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // 우리가 제어할 "상태" 컬럼인지 확인합니다. (컬럼명이 "상태"라고 가정)
            if (e.Column.FieldName == "Status")
            {
                // 현재 행의 "상태" 값을 가져옵니다.
                string status = GvItemStockList.GetRowCellValue(e.RowHandle, "Status")?.ToString();

                // 상태 값에 따라 배경색과 글자색을 분기 처리합니다.
                switch (status)
                {
                    case "부족":
                        e.Appearance.BackColor = Color.FromArgb(253, 232, 232); // 연한 분홍/파스텔 레드
                        e.Appearance.ForeColor = Color.FromArgb(200, 50, 50);    // 진한 빨간색
                        break;
                    case "주의":
                        e.Appearance.BackColor = Color.FromArgb(255, 243, 205); // 연한 노란색
						e.Appearance.ForeColor = Color.FromArgb(180, 120, 0);   // 진한 주황/갈색
						break;
                    case "안전":
                        e.Appearance.ForeColor = Color.FromArgb(100, 110, 120); // 진한 회색
                        break;
                    case "과잉":
                        e.Appearance.BackColor = Color.FromArgb(217, 237, 247); // 연한 파랑
                        e.Appearance.ForeColor = Color.FromArgb(49, 112, 143);   // 짙은 파랑
                        break;
                }
            }
        }

        private void Gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 'StockShare' 컬럼을 그릴 때만 개입
            if (e.Column.FieldName == "StockPer" && e.CellValue != null && e.CellValue != DBNull.Value)
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
                    Color barColor = (e.Column.FieldName == "StockPer")
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
                    string text = string.Format("{0:N1}%", rate);

                    using (Brush textBrush = new SolidBrush(Color.FromArgb(50, 60, 75)))
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Center;
                        e.Cache.DrawString(text, new Font(e.Appearance.Font.FontFamily, 7f), textBrush, textRect, sf);
                    }
                }

                e.Handled = true;
            }
        }

        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            string past7Days = DateTime.Now.AddDays(-7).ToString("d");
            string today = DateTime.Now.ToString("d");

            SearchCardData();
            Search_Chart_InOutDay(past7Days, today); // 불량률 & 불량수량 차트는 동일한 쿼리를 사용하므로 묶어서 처리
            SearchGrid_PreIn();
            SearchItemStockList();
            SearchStockWarningList();
            Search_Chart_Doughnut();
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
DECLARE @Today DATE = CONVERT(DATE, GETDATE());
DECLARE @Yesterday DATE = DATEADD(DAY, -1, @Today);
DECLARE @Tomorrow DATE = DATEADD(DAY, 1, @Today);

SELECT 
    -- 1. 입고 건수 컬럼
    ISNULL(InOut.Today_In_CNT, 0)     AS [금일_입고건수],
    ISNULL(InOut.Yesterday_In_CNT, 0) AS [전일_입고건수],
    ISNULL(InOut.Today_In_CNT, 0) -  ISNULL(InOut.Yesterday_In_CNT, 0)     AS [입고비교건수],
    
    -- 2. 출고 건수 컬럼
    ISNULL(InOut.Today_Out_CNT, 0)    AS [금일_출고건수],
    ISNULL(InOut.Yesterday_Out_CNT, 0) AS [전일_출고건수],
    ISNULL(InOut.Today_Out_CNT, 0) -  ISNULL(InOut.Yesterday_Out_CNT, 0)     AS [출고비교건수],
    
    -- 3. 안전재고 미달 건수 컬럼
    ISNULL(Shortage.미달건수, 0)      AS [안전재고_미달건수],

    -- 4. 총 재고금액 컬럼 (품목별 단가 중복 방지 반영)
    ISNULL(StockAmt.Total_Stock_Amount, 0) AS [총_재고금액]
    , ISNULL(ExamPending.PendingQty, 0) AS 수입검사대기

FROM 
    (
        -- [A 서브쿼리] 입출고 건수 한 행으로 집계
        SELECT 
            SUM(CASE WHEN M.StoDate >= CAST(@Today AS DATETIME) AND M.StoDate < CAST(@Tomorrow AS DATETIME) THEN 1 ELSE 0 END) AS Today_In_CNT,
            SUM(CASE WHEN M.StoDate >= CAST(@Yesterday AS DATETIME) AND M.StoDate < CAST(@Today AS DATETIME) THEN 1 ELSE 0 END) AS Yesterday_In_CNT,
            SUM(CASE WHEN S.SalesDate >= CAST(@Today AS DATETIME) AND S.SalesDate < CAST(@Tomorrow AS DATETIME) THEN 1 ELSE 0 END) AS Today_Out_CNT,
            SUM(CASE WHEN S.SalesDate >= CAST(@Yesterday AS DATETIME) AND S.SalesDate < CAST(@Today AS DATETIME) THEN 1 ELSE 0 END) AS Yesterday_Out_CNT
        FROM 
            (
                SELECT M.CorpId, M.StoDate, D.StoNo 
                FROM MM_PurchaseD D
                INNER JOIN MM_PurchaseM M ON M.CorpId = D.CorpId AND D.StoNo = M.StoNo
                WHERE D.CorpId = @CorpId
                  AND M.StoDate >= CAST(@Yesterday AS DATETIME)
                  AND M.StoDate < CAST(@Tomorrow AS DATETIME)
            ) M
        FULL OUTER JOIN 
            (
                SELECT M.CorpId, M.SalesDate, D.SalesNo
                FROM [dbo].[BM_SalesD] D
                INNER JOIN [dbo].[BM_SalesM] M ON M.CorpId = D.CorpId AND D.SalesNo = M.SalesNo
                WHERE D.CorpId = @CorpId
                  AND D.ReqType = '자재출고'
                  AND M.SalesDate >= CAST(@Yesterday AS DATETIME)
                  AND M.SalesDate < CAST(@Tomorrow AS DATETIME)
            ) S ON M.CorpId = S.CorpId AND M.StoNo = S.SalesNo
    ) AS InOut
CROSS JOIN 
    (
        -- [B 서브쿼리] 안전재고 미달 건수 집계
        SELECT COUNT(*) AS 미달건수
        FROM (
            SELECT A.ItemCode
            FROM StockListView A
            INNER JOIN CM_item B ON A.CorpId = B.CorpId AND A.ItemCode = B.ItemCode
            WHERE A.CorpId = @CorpId
            GROUP BY A.CorpId, A.ItemCode, B.SaftyQty
            HAVING SUM(A.StockQty) < B.SaftyQty
        ) AS SubShortage
    ) AS Shortage
CROSS JOIN
    (
        -- [C 서브쿼리] 각 품목별 단가 최신 1건만 매칭하여 금액 계산
        SELECT 
            SUM(ST.Item_Stock_Qty * ISNULL(P.Price, 0)) AS Total_Stock_Amount
        FROM (
            -- 품목별 현재고 합산
            SELECT CorpId, ItemCode, SUM(StockQty) AS Item_Stock_Qty
            FROM StockListView
            WHERE CorpId = @CorpId
            GROUP BY CorpId, ItemCode
            HAVING SUM(StockQty) > 0
        ) AS ST
        INNER JOIN (
            SELECT 
                CorpId, 
                ItemCode, 
                Price,
                ROW_NUMBER() OVER (
                    PARTITION BY CorpId, ItemCode 
                    ORDER BY ApplyDate DESC, Seq DESC
                ) AS RN
            FROM CM_itemprice
            WHERE CorpId = @CorpId
              AND PriceType = '20'
              AND ApplyDate <= @Today
              AND (EndDate IS NULL OR EndDate >= @Today)
        ) P ON ST.CorpId = P.CorpId AND ST.ItemCode = P.ItemCode AND P.RN = 1 -- 가장 최신 단가(1등)만 조인
    ) AS StockAmt
CROSS JOIN
    (
        -- [D 서브쿼리] 수입검사 대기 건수 집계 
        SELECT COUNT(*) AS PendingQty
        FROM MM_PurchaseD A
        INNER JOIN CM_ITEM B ON A.CorpId = B.CorpId AND A.ItemCode = B.ItemCode
        WHERE A.CorpId = '{WrGlobal.CorpID}'
          AND B.ExamYn = 'Y'
          AND NOT EXISTS (
                SELECT 1
                FROM QM_ExamResult
                WHERE CorpId = A.CorpId 
                  AND ExamDivCd = '10' --10:수입검사
                  AND RefNo = A.StoNo 
                  AND RefSeq = A.StoSeq
          )
    ) AS ExamPending;
;
";
            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                LblTodayInCnt.Text = dr["금일_입고건수"].ConvertFloat_Null_To_Zero().ToString("N0") + "건";
                LblInCntDiff.Text = dr["입고비교건수"].ConvertFloat_Null_To_Zero().ToString("▲ #,###;▼ #,###;-") + "건 전일 대비";

                LblTodayOutCnt.Text = dr["금일_출고건수"].ConvertFloat_Null_To_Zero().ToString("N0") + "건";
                LblOutDiff.Text = dr["출고비교건수"].ConvertFloat_Null_To_Zero().ToString("▲ #,###;▼ #,###;-") + "건 전일 대비";

                TodayStockAmt.Text = dr["총_재고금액"].ConvertFloat_Null_To_Zero().ToString("N0") + "원";

                LblStockLackCnt.Text = dr["안전재고_미달건수"].ConvertString_Null_To_Empty() + "건";

                LblPendingIncomingQty.Text = dr["수입검사대기"].ConvertFloat_Null_To_Zero().ToString("N0") + " 건";
            }
        }
        #endregion

        #region 차트 로드 (일별 입출고 추이)
        private void Search_Chart_InOutDay(string stDate, string edDate)
        {
            query = $@"
DECLARE @StartDate DATE = '{stDate}';
DECLARE @EndDate DATE = '{edDate}';

-- [1단계] 재귀 CTE를 사용하여 시작일부터 종료일까지의 모든 날짜 생성 (날짜 마스터)
WITH DateMaster AS (
    SELECT @StartDate AS DateValue
    UNION ALL
    SELECT DATEADD(DAY, 1, DateValue)
    FROM DateMaster
    WHERE DateValue < @EndDate
)

SELECT 
    -- 기준 날짜 마스터의 일자를 출력 (데이터가 없어도 무조건 출력됨)
    CONVERT(VARCHAR(10), DM.DateValue, 120) AS [BaseDate],
    ISNULL(P.Total_In_Qty, 0)               AS [InQty],   -- 데이터 없으면 0
    ISNULL(S.Total_Out_Qty, 0)              AS [OutQty]   -- 데이터 없으면 0
FROM DateMaster DM
LEFT JOIN 
    (
        -- 2. 일별 품목 입고량 집계
        SELECT 
            CONVERT(DATE, M.StoDate) AS StoDate,
            SUM(ISNULL(D.Qty, 0)) AS Total_In_Qty
        FROM MM_PurchaseD D
        INNER JOIN MM_PurchaseM M ON M.CorpId = D.CorpId AND D.StoNo = M.StoNo
        WHERE D.CorpId = '{WrGlobal.CorpID}'
          AND M.StoDate >= CAST(@StartDate AS DATETIME)
          AND M.StoDate < CAST(DATEADD(DAY, 1, @EndDate) AS DATETIME)
        GROUP BY CONVERT(DATE, M.StoDate)
    ) P ON DM.DateValue = P.StoDate
LEFT JOIN 
    (
        -- 3. 일별 품목 출고량 집계
        SELECT 
            CONVERT(DATE, M.SalesDate) AS SalesDate,
            SUM(ISNULL(D.Qty, 0)) AS Total_Out_Qty
        FROM [dbo].[BM_SalesD] D
        INNER JOIN [dbo].[BM_SalesM] M ON M.CorpId = D.CorpId AND D.SalesNo = M.SalesNo
        WHERE D.CorpId = '{WrGlobal.CorpID}'
          AND D.ReqType = '자재출고'
          AND M.SalesDate >= CAST(@StartDate AS DATETIME)
          AND M.SalesDate < CAST(DATEADD(DAY, 1, @EndDate) AS DATETIME)
        GROUP BY CONVERT(DATE, M.SalesDate)
    ) S ON DM.DateValue = S.SalesDate
ORDER BY 
    [BaseDate] ASC
OPTION (MAXRECURSION 0); 
";
            DataTable dt = db.GetDataTable(query);

            // 1. 일별 불량률 차트 셋팅 (ChartNgPer)
            //ChartInOutDay.DataSource = dt;
            ChartInOutDay.Series.Clear();
            ChartInOutDay.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series seriesIn = new Series("입고량", ViewType.Bar);
            seriesIn.ArgumentDataMember = "BaseDate";
            seriesIn.ValueDataMembers.AddRange(new string[] { "InQty"});
            seriesIn.DataSource = dt;
            //seriesIn.Label.TextPattern = "{A}: {V:#,0}";
            seriesIn.CrosshairLabelPattern = "{S}: {V:#,0}";

            ChartInOutDay.Series.Add(seriesIn);

            Series seriesOut = new Series("출고량", ViewType.Bar);
            seriesOut.ArgumentDataMember = "BaseDate";
            seriesOut.ValueDataMembers.AddRange(new string[] {"OutQty" });
            //seriesOut.Label.TextPattern = "{A}: {V:#,0}";
            seriesOut.CrosshairLabelPattern = "{S}: {V:#,0}";
            seriesOut.DataSource = dt;
            
            ChartInOutDay.Series.Add(seriesOut);

			XYDiagram diagramCnt = (XYDiagram)ChartInOutDay.Diagram;
			diagramCnt.DefaultPane.BackColor = Color.Transparent;
			diagramCnt.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;

            diagramCnt.AxisX.Label.TextPattern = "{V:M월/dd}";
            diagramCnt.AxisX.GridLines.Visible = false;

            // 가로 그리드선은 은은한 다크 컬러로 매칭 (너무 밝은 흰색 방지)
            diagramCnt.AxisY.Label.TextPattern = "{V:#,##0}";

            // 축 자체 라인 색상도 대시보드 톤에 맞춤
            diagramCnt.AxisX.Color = Color.FromArgb(50, 65, 90);

            // 축 텍스트(라벨) 폰트 및 색상 연동
            diagramCnt.AxisX.Label.TextColor = Color.FromArgb(130, 140, 160);
            diagramCnt.AxisX.Label.Font = new Font(this.Font.FontFamily, 8, FontStyle.Regular);

            diagramCnt.AxisY.Label.TextColor = Color.FromArgb(130, 140, 160);
            diagramCnt.AxisY.Label.Font = new Font(this.Font.FontFamily, 8, FontStyle.Regular);

            BarSeriesView barView = seriesIn.View as BarSeriesView;
            barView.Color = Color.FromArgb(55, 91, 150); //남색
            barView.BarWidth = 0.4;
            
            BarSeriesView barView2 = seriesOut.View as BarSeriesView;
            barView2.Color = Color.FromArgb(230, 145, 56); //오렌지
            barView2.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            barView2.BarWidth = 0.4;
        }

        private void Set_Chart_Doughnut(ChartControl chart)
        {
            //chart.BackColor = Color.Transparent;
            chart.BorderOptions.Visibility = DefaultBoolean.False;

            // 1. 범례(Legend) 설정 (하단 중앙에 수량/금액 통합 정렬)
            chart.Legend.Visibility = DefaultBoolean.False;
            chart.Legend.BackColor = Color.Transparent;
            chart.Legend.Border.Visibility = DefaultBoolean.False;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            chart.Legend.Direction = LegendDirection.LeftToRight;
            //chart.Legend.Font = new System.Drawing.Font(this.Font.FontFamily, 9, System.Drawing.FontStyle.Regular);
            //chart.Legend.TextColor = Color.FromArgb(130, 140, 160);

            // 2. 전체 메인 타이틀 설정
        /*    chart.Titles.Clear();
            ChartTitle title = new ChartTitle();
            title.Text = "품목구분별 재고 점유율 현황";
            //title.Font = new System.Drawing.Font(this.Font.FontFamily, 12);
            //title.TextColor = Color.FromArgb(220, 225, 235);
            chart.Titles.Add(title);
*/
            // 3. 커스텀 고급 컬러셋 팔레트 등록
            //chart.PaletteRepository.Clear();
            //PaletteEntry[] entries = new PaletteEntry[ModernColors.Length];
            //for (int i = 0; i < ModernColors.Length; i++)
            //{
            //    entries[i] = new PaletteEntry(ModernColors[i], ModernColors[i]);
            //}
            //chart.PaletteRepository.Add("Sophisticated", new Palette("Sophisticated", entries));
            //chart.PaletteName = "Sophisticated";
        }

        private void Search_Chart_Doughnut()
        {
            query = $@"
DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
DECLARE @Today DATE = CONVERT(DATE, GETDATE());

SELECT 
	 ItemType
    , A.ProductGroupName AS ItemTypeName
    , SUM(A.StockQty) AS TotalStockQty
    , SUM(A.StockQty * ISNULL(B.Price, 100)) AS TotalStockAmount 
FROM StockListView A
LEFT OUTER JOIN 
    (
        SELECT 
            CorpId, 
            ItemCode, 
            PriceType,  
            Price,
            ROW_NUMBER() OVER (
                PARTITION BY CorpId, ItemCode, PriceType 
                ORDER BY ApplyDate DESC, Seq DESC
            ) AS RN
        FROM CM_itemprice
        WHERE CorpId = @CorpId
          AND ApplyDate <= @Today
          AND (EndDate IS NULL OR EndDate >= @Today)
    ) B ON A.CorpId = B.CorpId 
       AND A.ItemCode = B.ItemCode 
       AND B.RN = 1 
       AND B.PriceType = CASE WHEN A.ItemType IN ('10', '20') THEN '20' ELSE '10' END
WHERE A.CorpId = @CorpId
GROUP BY 
    A.ItemType, A.ProductGroupName;
";
            DataTable dt = db.GetDataTable(query);

            // 핵심 1: 차트 내부의 모든 시리즈 및 기존에 나눈 영역(Diagram) 정보 리셋
            ChartStockQty.Series.Clear();
            ChartStockAmt.Series.Clear();

            if (dt == null || dt.Rows.Count == 0) return;

            // -------------------------------------------------------------
            // [왼쪽 차트] 수량기준 도넛 시리즈 생성
            // -------------------------------------------------------------

            Series seriesQty = new Series("수량기준", ViewType.Doughnut);
            seriesQty.DataSource = dt;
            seriesQty.ArgumentDataMember = "ItemTypeName";
            seriesQty.ValueDataMembers.AddRange(new string[] { "TotalStockQty" });
            seriesQty.LegendTextPattern = "{A} (수량): {VP:0.0%}";

            // 금액 라벨 설정
            seriesQty.Label.BackColor = Color.Transparent;
            seriesQty.Label.Border.Visibility = DefaultBoolean.False;

            // 수량 라벨 설정
            DoughnutSeriesLabel labelQty = seriesQty.Label as DoughnutSeriesLabel;
            if (labelQty != null)
            {
                labelQty.TextPattern = "{A}\n{VP:0.0%}";
                labelQty.Position = PieSeriesLabelPosition.TwoColumns; // 바깥 선 정렬
                labelQty.ResolveOverlappingMode = ResolveOverlappingMode.Default;
                labelQty.ResolveOverlappingMinIndent = 3;
            }


            // 수량 뷰 설정 (얇고 세련된 도넛 형태)
            DoughnutSeriesView viewQty = seriesQty.View as DoughnutSeriesView;

            if (viewQty != null)
            {
                viewQty.HoleRadiusPercent = 65;
                viewQty.Border.Visibility = DefaultBoolean.False;
                viewQty.RuntimeExploding = true;

                // 각 차트 위의 소제목 (수량기준)
                viewQty.Titles.Clear();
                SeriesTitle sTitleQty = new SeriesTitle();
                sTitleQty.Text = "수량 기준";
                viewQty.Titles.Add(sTitleQty);
            }

            ChartStockQty.Series.AddRange(new Series[] { seriesQty });
            ChartStockQty.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // -------------------------------------------------------------
            // [오른쪽 차트] 금액기준 도넛 시리즈 생성
            // -------------------------------------------------------------
            Series seriesAmount = new Series("금액기준", ViewType.Doughnut);
            seriesAmount.DataSource = dt;
            seriesAmount.ArgumentDataMember = "ItemTypeName";
            seriesAmount.ValueDataMembers.AddRange(new string[] { "TotalStockAmount" });
            seriesAmount.LegendTextPattern = "{A} (금액): {VP:0.0%}";

            // 금액 라벨 설정
            seriesAmount.Label.BackColor = Color.Transparent;
            seriesAmount.Label.Border.Visibility = DefaultBoolean.False;

			DoughnutSeriesLabel labelAmount = seriesAmount.Label as DoughnutSeriesLabel;
			if (labelAmount != null)
			{
				labelAmount.TextPattern = "{A}\n{VP:0.0%}";
				labelAmount.Position = PieSeriesLabelPosition.TwoColumns; // 바깥 선 정렬
                labelAmount.ResolveOverlappingMode = ResolveOverlappingMode.Default;
                labelAmount.ResolveOverlappingMinIndent = 3;
			}

			// 금액 뷰 설정
			DoughnutSeriesView viewAmount = seriesAmount.View as DoughnutSeriesView;
            if (viewAmount != null)
            {
                viewAmount.HoleRadiusPercent = 65;
                viewAmount.Border.Visibility = DefaultBoolean.False;
                viewAmount.RuntimeExploding = true;

				// 각 차트 위의 소제목 (금액기준)
				viewAmount.Titles.Clear();
				SeriesTitle sTitleAmount = new SeriesTitle();
				sTitleAmount.Text = "금액 기준";
				viewAmount.Titles.Add(sTitleAmount);
			}

            ChartStockAmt.Series.AddRange(new Series[] { seriesAmount });
            ChartStockAmt.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
        }

        #endregion


        #region 그리드 데이터 로드 (입고예정)
        private void SearchGrid_PreIn()
        {
            query = $@"
DECLARE @Today DATE = CONVERT(DATE, GETDATE());
DECLARE @WeekLastDay DATE = DATEADD(DAY, 7 - DATEPART(WEEKDAY, @Today), @Today);


SELECT 
D.DeliveryDate

, M.CustCode
, CT.CustName
, D.ItemCode
, IT.ItemName
, IT.Size

, D.Qty

FROM MM_PlacingD D
INNER JOIN MM_PlacingM M ON D.Corpid = M.CorpId AND D.MmOrderNo = M.MmOrderNo
LEFT JOIN CM_Item IT ON D.CorpId = IT.CorpId AND D.ItemCode = IT.ItemCode
LEFT JOIN CM_Cust CT ON D.CorpId = CT.CorpId AND M.CustCode = CT.CustCode
WHERE D.CorpId = '{WrGlobal.CorpID}'
AND D.DeliveryDate BETWEEN @Today AND @WeekLastDay
";
            GcPreIn.DataSource = db.GetDataTable(query);
            GvItemStockList.BestFitColumns();
        }

        private void SearchItemStockList()
		{
            query = $@"

SELECT 
    A.ItemCode
    , B.ItemName
    , A.TotalStockQty AS StockQty
    , ISNULL(B.SaftyQty, 0)  AS SaftyQty
    
    , CALC.StockPer
    
    -- 2. 재고율 구간별 상태(Status) 지정
    , CASE 
        WHEN CALC.StockPer <= 60                  THEN '부족'
        WHEN CALC.StockPer >= 61 AND CALC.StockPer <= 80  THEN '주의'
        WHEN CALC.StockPer >= 100 AND CALC.StockPer <= 150 THEN '안전'
        WHEN CALC.StockPer >= 151                 THEN '과잉'
        ELSE '안전' -- 
    END                                              AS Status
FROM 
    (
        SELECT 
            CorpId,
            ItemCode, 
            ISNULL(SUM(StockQty), 0) AS TotalStockQty
        FROM StockListView
        WHERE CorpId = '{WrGlobal.CorpID}'
        GROUP BY CorpId, ItemCode
    ) A
INNER JOIN CM_Item B ON A.CorpId = B.CorpId AND A.ItemCode = B.ItemCode
CROSS APPLY (
    SELECT 
        CASE 
            WHEN A.TotalStockQty <= 0 OR ISNULL(B.SaftyQty, 0) <= 0 THEN 0 
            ELSE CONVERT(INT, ROUND((A.TotalStockQty / (B.SaftyQty * 1.0)) * 100, 0))
        END AS StockPer
) CALC
WHERE A.TotalStockQty <> 0
;
";

            GcItemStockList.DataSource = db.GetDataTable(query);

		}

        private void SearchStockWarningList()
		{
            query = $@"
--재고경고현황
WITH StockAgg AS (
    SELECT ItemCode
         , ISNULL(SUM(StockQty), 0) AS TotalStockQty
    FROM StockListView
    WHERE CorpId = '{WrGlobal.CorpID}'
    GROUP BY ItemCode
)
SELECT '' AS Icon
     , A.ItemCode
     , B.ItemName
     , A.TotalStockQty AS StockQty
     , B.SaftyQty
     , CONVERT(VARCHAR, CASE 
           WHEN A.TotalStockQty = 0 OR ISNULL(B.SaftyQty, 0) = 0 THEN 0 
           ELSE ROUND((B.SaftyQty * 1.0) / A.TotalStockQty , 1)
       END) + '%' AS Per
     , CONCAT('(', CONVERT(VARCHAR, CONVERT(INT, A.TotalStockQty)), '/', B.SaftyQty, ')') AS Qty
FROM StockAgg A
INNER JOIN CM_item B 
    ON B.CorpId =  '{WrGlobal.CorpID}'
    AND A.ItemCode = B.ItemCode
WHERE A.TotalStockQty < ISNULL(B.SaftyQty, 0)
ORDER BY A.TotalStockQty - ISNULL(B.SaftyQty, 0) 
";

            gridControl2.DataSource = db.GetDataTable(query);

		}
		#endregion

		private void timer1_Tick(object sender, EventArgs e)
		{
            LblCurTime.Text = DateTime.Now.ToString("yyyy.MM.dd HH:MM:ss");
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
	}
}