using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using SmartCommon.Common;
using SmartDesign.View.Dashboard;
using SmartMES.Common;
using System;
using System.Data;
using System.Drawing;

namespace SmartMES.View.Management
{
    public partial class Frm_MM_Dashboard_QM : DevExpress.XtraEditors.XtraForm
    {
        private readonly clsGrid grid = new clsGrid();
        private readonly clsRepository rpo = new clsRepository();

        private readonly Database db = new Database();
        string query = "";

        // 차트 공통 테마 색상
        private readonly Color _mintColor = Color.FromArgb(20, 200, 150);
        private readonly Color _targetRedColor = Color.FromArgb(230, 80, 80);

        public Frm_MM_Dashboard_QM()
        {
            InitializeComponent();
        }

        private void Frm_MM_Dashboard_QM_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            SetGrid(GvExRst);
            SetGvTile();

            Search();

            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)
        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "등록시간", "CreDt", rpo.Create_rpoDate("yyyy-MM-dd HH:mm"));
            grid.Create_GvColumn(gv, "구분", "ExamDivNm");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
            grid.Create_GvColumn(gv, "판정", "PassDivCd");

            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
        }

        private void SetGvTile()
		{
            // 1. 표시 형식을 사용자 지정(Custom) 타입으로 설정합니다.
            tileView1.Columns["Qty"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            tileView1.Columns["Qty"].DisplayFormat.FormatString = "#,###'개'";

            tileView1.Columns["Per"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            tileView1.Columns["Per"].DisplayFormat.FormatString = "#,###'%'";

            tileView1.Columns["LastMonthPer"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            tileView1.Columns["LastMonthPer"].DisplayFormat.FormatString = "▲ #,###'% 전월 대비';▼ -#,###'% 전월 대비';0'% 전월 대비'";

			tileView1.ItemCustomize += TileView1_ItemCustomize;
        }

		private void TileView1_ItemCustomize(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventArgs e)
		{
            // 현재 그리고 있는 타일 행(Row)에서 "LastMonthPer" 값을 가져옵니다.
            object val = tileView1.GetRowCellValue(e.RowHandle, "LastMonthPer");

            if (val != null && val != DBNull.Value)
            {
                float perValue = Convert.ToSingle(val);

                //// 타일 템플릿 안에서 "LastMonthPer" 컬럼과 연결된 엘리먼트(텍스트 블록)를 찾습니다.
                //// Item["FieldName"] 구조로 접근하거나, 디자이너에서 지정한 엘리먼트 이름(예: e.Item.Elements[0])으로 접근할 수 있습니다.
                TileViewItemElement element = e.Item.Elements[4] as TileViewItemElement;

				if (element != null)
				{
					if (perValue > 0)
					{
						// 0보다 크면 빨간색
						element.Appearance.Normal.ForeColor = Color.Red;
						element.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
					}
					else if (perValue < 0)
					{
						// 0보다 작으면 파란색
						element.Appearance.Normal.ForeColor = Color.Blue;
						element.Appearance.Normal.FontStyleDelta = FontStyle.Bold;
					}
					else
					{
						// 0일 때는 기본 색상 (검은색)
						element.Appearance.Normal.ForeColor = Color.Black;
						element.Appearance.Normal.FontStyleDelta = FontStyle.Regular;
					}
				}
			}
        }

		#endregion

		#region 데이터 조회 (Search)
		private void Search()
        {
            string stMM = DateTime.Now.ToString("yyyy-MM-01");
            string edMM = DateTime.Now.ToString("yyyy-MM-dd");
            string past10Days = DateTime.Now.AddDays(-10).ToString("d");
            string today = DateTime.Now.ToString("d");

            SearchCardData();
            Search_Chart_NgPerAndCnt(past10Days, today); // 불량률 & 불량수량 차트는 동일한 쿼리를 사용하므로 묶어서 처리
            SearchChart_NgType(stMM, edMM);
            SearchGrid_ExRst(stMM, edMM);
            SearchNgType();
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            query = $@"

DECLARE @CorpId nvarchar(10) = '{WrGlobal.CorpID}';

WITH TargetDate AS (
    SELECT CONVERT(VARCHAR(8), GETDATE(), 112) AS Today
         , CONVERT(VARCHAR(8), DATEADD(DAY, -1, GETDATE()), 112) AS Yesterday
         , CONVERT(VARCHAR(6), GETDATE(), 112) AS ThisMonth         -- 이번달 (YYYYMM)
         , CONVERT(VARCHAR(6), DATEADD(MONTH, -1, GETDATE()), 112) AS LastMonth -- 저번달 (YYYYMM)
)
, GroupedExam AS (
    SELECT ExamDate
         , RefNo
         , RefSeq
         , ExamDivCd
         , CASE 
                WHEN SUM(CASE WHEN PassDivCd = '10' THEN 0 ELSE 1 END) = 0 THEN 1 
                ELSE 0 
           END AS IsPass
    FROM QM_ExamResult
    WHERE CorpId = @CorpId
      AND ExamDate IN (SELECT Today FROM TargetDate UNION SELECT Yesterday FROM TargetDate)
    GROUP BY ExamDate
           , RefNo
           , RefSeq
           , ExamDivCd
)
, ExamStats AS (
    SELECT ExamDate
         , COUNT(*) AS TotalExamCnt
         , SUM(CASE WHEN IsPass = 1 THEN 1 ELSE 0 END) AS PassExamCnt
    FROM GroupedExam
    GROUP BY ExamDate
)
, DefectStats AS (
    SELECT WorkDate
         , SUM(Qty) AS TotalDefectQty
    FROM QM_WorkDefect
    WHERE CorpId = @CorpId
      AND WorkDate IN (SELECT Today FROM TargetDate UNION SELECT Yesterday FROM TargetDate)
    GROUP BY WorkDate
)
, DefectPer AS (
    SELECT CONVERT(VARCHAR(6), WorkDate, 112) AS WorkMonth
         -- 나누기 오류 방지 및 백분율(%) 계산을 위해 * 100.0 및 ROUND 처리 추가
         , CASE WHEN ISNULL(SUM(WorkQty), 0) = 0 THEN 0 ELSE ROUND((SUM(NgQty)/ SUM(WorkQty) * 100.0), 2) END AS TotalDefectPer
		 , SUM(WorkQty) WorkQty
		 , SUM(NgQty) NgQty
    FROM PM_OrderResult
    WHERE CorpId = @CorpId
      AND CONVERT(VARCHAR(6), WorkDate, 112) IN (SELECT ThisMonth FROM TargetDate UNION SELECT LastMonth FROM TargetDate)
    GROUP BY CONVERT(VARCHAR(6), WorkDate, 112)
)
, TopDefect AS (
    SELECT TOP 1 A.DefectCode
         , B.Title AS DefectName
         , SUM(A.Qty) AS DefectQty
    FROM QM_WorkDefect A
    LEFT JOIN CM_DefectCode B ON A.CorpId = B.CorpId AND A.DefectCode = B.FaultCode
    WHERE A.CorpId = @CorpId
      AND A.WorkDate = (SELECT Today FROM TargetDate)
    GROUP BY A.DefectCode
           , B.Title
    ORDER BY SUM(A.Qty) DESC
)
, PendingIncoming AS (
    SELECT COUNT(*) AS PendingQty
    FROM MM_PurchaseD A
    LEFT JOIN CM_ITEM B ON A.CorpId = B.CorpId AND A.ItemCode = B.ItemCode
    WHERE A.CorpId =@CorpId
      AND B.ExamYn = 'Y'
      AND NOT EXISTS (
            SELECT 1
            FROM QM_ExamResult
            WHERE CorpId =@CorpId
              AND ExamDivCd = '10'
              AND RefNo = A.StoNo 
              AND RefSeq = A.StoSeq
      )
)
SELECT CASE WHEN ISNULL(E_Today.TotalExamCnt, 0) = 0 THEN 0 ELSE ROUND((ISNULL(E_Today.PassExamCnt, 0) * 100.0) / E_Today.TotalExamCnt, 2) END AS TodayPassRate
     , (CASE WHEN ISNULL(E_Today.TotalExamCnt, 0) = 0 THEN 0 ELSE ROUND((ISNULL(E_Today.PassExamCnt, 0) * 100.0) / E_Today.TotalExamCnt, 2) END) 
       - (CASE WHEN ISNULL(E_Yest.TotalExamCnt, 0) = 0 THEN 0 ELSE ROUND((ISNULL(E_Yest.PassExamCnt, 0) * 100.0) / E_Yest.TotalExamCnt, 2) END) AS PassRateDiff
     , ISNULL(D_Today.TotalDefectQty, 0) AS TodayDefectQty
     , ISNULL(D_Today.TotalDefectQty, 0) - ISNULL(D_Yest.TotalDefectQty, 0) AS DefectQtyDiff
     , ISNULL(E_Today.TotalExamCnt, 0) AS TodayInspectQty
     , ISNULL(TD.DefectName, '없음') AS TopDefectName
     , ISNULL(TD.DefectQty, 0) AS TopDefectQty
     , ISNULL(PI.PendingQty, 0) AS PendingIncomingQty
	 , ISNULL(DP_ThisMonth.TotalDefectPer, 0) AS ThisMonthDefectPer -- 이번달 불량률
     , ISNULL(DP_LastMonth.TotalDefectPer, 0) AS LastMonthDefectPer -- 저번달 불량률
     , ISNULL(DP_LastMonth.TotalDefectPer, 0) - ISNULL(DP_ThisMonth.TotalDefectPer, 0) AS DefectPerDiff

FROM TargetDate AS T
LEFT OUTER JOIN ExamStats AS E_Today ON T.Today = E_Today.ExamDate
LEFT OUTER JOIN ExamStats AS E_Yest ON T.Yesterday = E_Yest.ExamDate
LEFT OUTER JOIN DefectStats AS D_Today ON T.Today = D_Today.WorkDate
LEFT OUTER JOIN DefectStats AS D_Yest ON T.Yesterday = D_Yest.WorkDate
LEFT OUTER JOIN DefectPer AS DP_ThisMonth ON T.ThisMonth = DP_ThisMonth.WorkMonth -- 이번달 JOIN
LEFT OUTER JOIN DefectPer AS DP_LastMonth ON T.LastMonth = DP_LastMonth.WorkMonth -- 저번달 JOIN
OUTER APPLY (SELECT DefectName, DefectQty FROM TopDefect) AS TD
OUTER APPLY (SELECT PendingQty FROM PendingIncoming) AS PI;

";
            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                LblTodayPassRate.Text = dr["TodayPassRate"].ConvertFloat_Null_To_Zero().ToString("N1") + "%";
                LblPassRateDiff.Text = dr["PassRateDiff"].ConvertFloat_Null_To_Zero().ToString("▲ '+'0.##;▼ '-'0.##;-") + "% 전일 대비";

                LblTodayDefectQty.Text = dr["TodayDefectQty"].ConvertFloat_Null_To_Zero().ToString("N0");
                LblDefectQtyDiff.Text = dr["DefectQtyDiff"].ConvertFloat_Null_To_Zero().ToString("▲ '+'0.##;▼ '-'0.##;-") + "건 전일 대비";

                LblTodayDefectPer.Text = dr["ThisMonthDefectPer"].ConvertFloat_Null_To_Zero().ToString("N1") + "%";
                LblDefectPerDiff.Text = dr["DefectPerDiff"].ConvertFloat_Null_To_Zero().ToString("▲ '+'0.##'%';▼ '-'0.##'%';-") + " 전월 대비";

                //TodayInspectQty.Text = dr["TodayInspectQty"].ConvertFloat_Null_To_Zero().ToString("N0");
                LblTopDefectName.Text = dr["TopDefectName"].ConvertString_Null_To_Empty();
                LblTopDefectQty.Text = dr["TopDefectQty"].ConvertFloat_Null_To_Zero().ToString("N0") + " 건";

                LblPendingIncomingQty.Text = dr["PendingIncomingQty"].ConvertFloat_Null_To_Zero().ToString("N0") + " 건";
            }
        }
        #endregion

        #region 차트 로드 (불량률 & 불량수량)
        private void Search_Chart_NgPerAndCnt(string stDate, string edDate)
        {
            string query = $@"
SELECT WorkDate
     , ISNULL(SUM(WorkQty), 0) AS WorkQty
     , ISNULL(SUM(NgQty), 0) AS NgQty
     , CASE 
           WHEN ISNULL(SUM(NgQty), 0) = 0 OR ISNULL(SUM(WorkQty), 0) = 0 THEN 0
           ELSE ROUND(SUM(NgQty) / SUM(WorkQty), 2) 
       END AS NgPer
FROM PM_OrderResult odr
WHERE odr.CorpId = '{WrGlobal.CorpID}'
  AND WorkDate BETWEEN '{stDate}' AND '{edDate}'
GROUP BY WorkDate
";
            DataTable dtNgData = db.GetDataTable(query);

            // 1. 일별 불량률 차트 셋팅 (ChartNgPer)
            ChartNgPer.DataSource = dtNgData;
            ChartNgPer.Series.Clear();
            ChartNgPer.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series seriesNgPer = new Series("불량률", ViewType.SplineArea);
            seriesNgPer.ArgumentDataMember = "WorkDate";
            seriesNgPer.ValueDataMembers.AddRange("NgPer");
            ChartNgPer.Series.Add(seriesNgPer);

            SplineAreaSeriesView viewPer = (SplineAreaSeriesView)seriesNgPer.View;
            viewPer.Color = _mintColor;
            viewPer.Transparency = 180;
            viewPer.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            viewPer.MarkerOptions.Kind = MarkerKind.Circle;
            viewPer.MarkerOptions.Size = 10;
            viewPer.MarkerOptions.Color = _mintColor;

            XYDiagram diagramPer = (XYDiagram)ChartNgPer.Diagram;
            diagramPer.DefaultPane.BackColor = Color.Transparent;
            diagramPer.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
            diagramPer.AxisX.Label.TextPattern = "{V:M/dd}";

            diagramPer.AxisY.ConstantLines.Clear();
            ConstantLine targetLine = new ConstantLine("목표 1.0%")
            {
                AxisValue = 1.0,
                Color = _targetRedColor,
                ShowInLegend = false
            };
            targetLine.LineStyle.DashStyle = DashStyle.Dash;
            targetLine.LineStyle.Thickness = 2;
            targetLine.Title.Visible = false;
            diagramPer.AxisY.ConstantLines.Add(targetLine);

            // 2. 일별 생산/불량수량 차트 셋팅 (ChartNgCnt)
            // 2. 일별 생산/불량수량 차트 셋팅 (ChartNgCnt)
            ChartNgCnt.DataSource = dtNgData;
            ChartNgCnt.Series.Clear();
            ChartNgCnt.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series seriesWorkQty = new Series("생산수량", ViewType.Bar);
            seriesWorkQty.ArgumentDataMember = "WorkDate";
            seriesWorkQty.ValueDataMembers.AddRange("WorkQty");

            Series seriesNgQty = new Series("불량수량", ViewType.Line);
            seriesNgQty.ArgumentDataMember = "WorkDate";
            seriesNgQty.ValueDataMembers.AddRange("NgQty");

            ChartNgCnt.Series.AddRange(new Series[] { seriesWorkQty, seriesNgQty });

            // [1] 바 차트 (생산수량) 색상 지정
            BarSeriesView viewCnt = (BarSeriesView)seriesWorkQty.View;
            viewCnt.Color = Color.FromArgb(0, 120, 215); // 예시: 원하는 다른 색상(Color.Red, Color.Blue 등)이나 변수(_mintColor) 지정
            viewCnt.Transparency = 50;                  // 투명도를 제거하고 싶다면 0으로 설정하세요.

            // [2] 라인 차트 (불량수량) 색상 지정
            LineSeriesView viewNg = (LineSeriesView)seriesNgQty.View;
            viewNg.Color = Color.Red;                   // 예시: 불량수량 라인은 빨간색으로 지정
                                                         viewNg.LineStyle.Thickness = 3;          // (선택사항) 라인 두께를 조금 더 두껍게 강조하고 싶을 때 사용

            XYDiagram diagramCnt = (XYDiagram)ChartNgCnt.Diagram;
            diagramCnt.DefaultPane.BackColor = Color.Transparent;
            diagramCnt.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
            diagramCnt.AxisX.Label.TextPattern = "{V:M/dd}";
        }
        #endregion

        #region 차트 로드 (유형별 불량률)
        private void SearchChart_NgType(string stMM, string edMM)
        {
            string query = $@"
SELECT DefectCode
     , B.Title AS DefectName
     , SUM(Qty) AS Qty
     , ROUND((SUM(Qty) * 100.0) / SUM(SUM(Qty)) OVER(), 1) AS DefectRate
FROM QM_WorkDefect A
LEFT JOIN CM_DefectCode B ON A.CorpId = B.CorpId AND A.DefectCode = B.FaultCode
WHERE A.CorpId = '{WrGlobal.CorpID}'
  AND A.WorkDate BETWEEN '{stMM}' AND '{edMM}'
GROUP BY DefectCode
       , B.Title
ORDER BY SUM(Qty) DESC
";
            DataTable dt = db.GetDataTable(query);
            ChartNgType.DataSource = dt;
            ChartNgType.Series.Clear();
            ChartNgType.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            Series seriesNgType = new Series("유형별 불량%", ViewType.Bar);
            seriesNgType.ArgumentDataMember = "DefectName";
            seriesNgType.ValueDataMembers.AddRange("DefectRate");
            seriesNgType.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            ChartNgType.Series.Add(seriesNgType);

            BarSeriesView barView = (BarSeriesView)seriesNgType.View;
            barView.ColorEach = true;
            barView.BarWidth = 0.5;
            barView.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            BarSeriesLabel label = (BarSeriesLabel)seriesNgType.Label;
            label.TextPattern = "{V}%";
            label.Position = BarSeriesLabelPosition.BottomInside;
            label.TextOrientation = TextOrientation.Horizontal;
            label.TextColor = Color.White;
            label.BackColor = Color.Transparent;
            label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

            XYDiagram diagramNgTye = (XYDiagram)ChartNgType.Diagram;
            diagramNgTye.Rotated = true;
            diagramNgTye.DefaultPane.BackColor = Color.Transparent;
            diagramNgTye.DefaultPane.BorderVisible = false;

            diagramNgTye.AxisX.Tickmarks.Visible = false;
			diagramNgTye.AxisX.Tickmarks.MinorVisible = false;
			diagramNgTye.AxisX.Reverse = true;
			diagramNgTye.AxisX.VisualRange.SetMinMaxValues(0, 100);
			diagramNgTye.AxisX.WholeRange.SetMinMaxValues(0, 100);

			diagramNgTye.AxisY.GridLines.Visible = false;
			diagramNgTye.AxisY.VisualRange.SetMinMaxValues(0, 100);
			diagramNgTye.AxisY.WholeRange.SetMinMaxValues(0, 100);
		}
		#endregion

		#region 그리드 데이터 로드 (검사이력)
		private void SearchGrid_ExRst(string stMM, string edMM)
        {
            string query = $@"
SELECT TOP 30 ExamNo
     , ExamDivCd
     , RefNo
     , RefSeq
     , A.ItemCode
     , D.ItemName
     , CASE MAX(PassDivCd) WHEN '10' THEN '합격' ELSE '불합격' END AS PassDivCd 
     , B.TITLE AS ExamDivNm
     , CONVERT(DATE, A.ExamDate) AS ExamDate
     , MAX(A.ExamQty) AS ExamQty
     , MAX(A.CreDt) AS CreDt
FROM QM_ExamResult A
LEFT JOIN CM_CateItem B ON A.CorpId = B.CorpId AND B.CLASS = 'Q005' AND A.ExamDivCd = B.ClassItemCode
LEFT JOIN CM_CateItem C ON A.CorpId = B.CorpId AND C.CLASS = 'Q020' AND A.PassDivCd = C.ClassItemCode
LEFT JOIN CM_Item D ON A.CorpId = D.CorpId AND A.ItemCode = D.ItemCode
WHERE A.CorpId = '{WrGlobal.CorpID}'
GROUP BY ExamNo
       , ExamDivCd
       , RefNo
       , RefSeq
       , A.ItemCode
       , B.TITLE
       , C.Title
       , CONVERT(DATE, A.ExamDate)
       , D.ItemName
ORDER BY MAX(A.CreDt) DESC
";
            GcExRst.DataSource = db.GetDataTable(query);
            GvExRst.BestFitColumns();
        }

        private void SearchNgType()
		{
            query = $@"


WITH TargetDate AS (
    -- 기준일자(오늘)를 바탕으로 이번달(당월)과 저번달(전월) 기준 문자열 생성 (YYYYMM)
    SELECT CONVERT(VARCHAR(6), GETDATE(), 112) AS ThisMonth
         , CONVERT(VARCHAR(6), DATEADD(MONTH, -1, GETDATE()), 112) AS LastMonth
)
, MonthlyDefect AS (
    -- 불량 데이터를 월별/유형별로 집계
    SELECT CONVERT(VARCHAR(6), A.WorkDate, 112) AS WorkMonth
         , A.DefectCode
         , B.Title AS DefectName
         , SUM(A.Qty) AS DefectQty
    FROM QM_WorkDefect A
    LEFT JOIN CM_DefectCode B ON A.CorpId = B.CorpId AND A.DefectCode = B.FaultCode
    WHERE A.CorpId = '{WrGlobal.CorpID}'
      AND CONVERT(VARCHAR(6), A.WorkDate, 112) IN (SELECT ThisMonth FROM TargetDate UNION SELECT LastMonth FROM TargetDate)
    GROUP BY CONVERT(VARCHAR(6), A.WorkDate, 112)
           , A.DefectCode
           , B.Title
)
, CurrentMonthStats AS (
    -- 이번달 데이터만 필터링 및 이번달 총 불량수량 계산 (비율 계산용)
    SELECT DefectCode
         , DefectName
         , DefectQty AS ThisMonthQty
         , SUM(DefectQty) OVER() AS TotalQty
    FROM MonthlyDefect
    WHERE WorkMonth = (SELECT ThisMonth FROM TargetDate)
)
, PastMonthStats AS (
    -- 저번달 데이터만 필터링
    SELECT DefectCode
         , DefectQty AS LastMonthQty
    FROM MonthlyDefect
    WHERE WorkMonth = (SELECT LastMonth FROM TargetDate)
)
SELECT ROW_NUMBER() OVER(ORDER BY CUR.ThisMonthMonthQty DESC, CUR.DefectCode) AS Seq
     , ISNULL(CUR.DefectName, '미지정') AS DefectName
     , ISNULL(CUR.ThisMonthMonthQty, 0)  AS Qty
     -- 이번달 불량유형 비율 (%)
     , CASE 
            WHEN ISNULL(CUR.TotalQty, 0) = 0 THEN 0 
            ELSE ROUND((ISNULL(CUR.ThisMonthMonthQty, 0) * 100.0) / CUR.TotalQty, 1) 
       END AS Per
     -- 전월 대비 증감률 (%) : ((이번달 - 저번달) / 저번달) * 100
     -- 저번달 기록이 없거나 0이면 이번달 수량이 곧 증가분이므로 분모 체크 처리
     , CASE 
            WHEN ISNULL(PAST.LastMonthQty, 0) = 0 
				THEN 
                CASE WHEN ISNULL(CUR.ThisMonthMonthQty, 0) = 0 THEN 0 ELSE 100.0 END
            ELSE ROUND(((ISNULL(CUR.ThisMonthMonthQty, 0) - PAST.LastMonthQty) * 100.0) / PAST.LastMonthQty, 1)
       END AS LastMonthPer
FROM (
    SELECT DefectCode, DefectName, ThisMonthQty AS ThisMonthMonthQty, TotalQty 
    FROM CurrentMonthStats
) CUR
LEFT OUTER JOIN PastMonthStats PAST ON CUR.DefectCode = PAST.DefectCode
ORDER BY Seq ASC;
";
            GcNgType.DataSource = db.GetDataTable(query);

		}
		#endregion

		private void Tm_Time_Tick(object sender, EventArgs e)
		{
            LblCurTime.Text = DateTime.Now.ToString("yyyy.MM.dd HH:MM:ss");
        }

        private void Tm_Search_Tick(object sender, EventArgs e)
		{
            Search();
        }
	}
}