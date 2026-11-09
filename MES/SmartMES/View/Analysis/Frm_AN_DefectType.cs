using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using System;
using System.Data;
using System.Drawing;
using DevExpress.Utils;
using SmartMES.Models.CM;
using SmartMES.Models;
using System.Linq;

namespace SmartMES.View.Analysis
{
    public partial class Frm_AN_DefectType : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";
        string strItemDiv = WrGlobal.ProcOption["SC"]["Product_ItemType"];

        public Frm_AN_DefectType()
        {
            InitializeComponent();

        }

        private void Frm_AN_DefectType_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            Set_SearchLup();
            SetGrid(GvDefect);

            Search();

            Tm_Search.Start();
        }

        private void Set_SearchLup()
        {
            barDate.smtStartDateTimeValue = DateTime.Now;

            //품목
            LupItem.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", strItemDiv }, "전체");
            LupItem.smtLookUpDataDisplayMember = "품명";
            LupItem.smtLookUpDataValueMember = "품목코드";
            LupItem.smtLookUpValue = "ALL";

            //공정
            CM_Process process = new CM_Process();
            LupProcess.smtLookUpDataSource = process.SelectCM_ProcessLookup();
            LupProcess.smtLookUpDataDisplayMember = "공정명";
            LupProcess.smtLookUpDataValueMember = "코드";
            LupProcess.smtLookUpValue = "ALL";
        }

        #region Grid 설정

        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "불량코드", "DefectCode");
            grid.Create_GvColumn(gv, "불량유형", "Title");
            grid.Create_GvColumn(gv, "불량수량(EA)", "BadQty");
            grid.Create_GvColumn(gv, "불량률(%)", "BadRate");
            grid.Create_GvColumn(gv, "누적수량", "CumulativeQty");
            grid.Create_GvColumn(gv, "누적률(%)", "CumulativeRate");
            grid.Create_GvColumn(gv, "추이", "TrendFlow_Unbound");

            gv.Columns["DefectCode"].Visible = false;
            gv.Columns["TrendFlow_Unbound"].UnboundType = DevExpress.Data.UnboundColumnType.Object;

            DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit sparklineEditor = new DevExpress.XtraEditors.Repository.RepositoryItemSparklineEdit();

            DevExpress.Sparkline.LineSparklineView lineView = new DevExpress.Sparkline.LineSparklineView();
            lineView.Color = Color.DodgerBlue;
            sparklineEditor.View = lineView;

            gv.GridControl.RepositoryItems.Add(sparklineEditor);
            gv.Columns["TrendFlow_Unbound"].ColumnEdit = sparklineEditor;

            grid.SetCol_Type(gv.Columns["BadQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BadRate"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["CumulativeQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["CumulativeRate"], clsGrid.TypeList.TFloat);

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;

            GvDefect.CustomUnboundColumnData -= GvDefect_CustomUnboundColumnData;
            GvDefect.CustomUnboundColumnData += GvDefect_CustomUnboundColumnData;
        }

        private void GvDefect_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "TrendFlow_Unbound" && e.IsGetData)
            {
                DataRowView rowView = e.Row as DataRowView;

                if (rowView != null)
                {
                    string rawVal = Convert.ToString(rowView["TrendFlow"]);

                    if (!string.IsNullOrEmpty(rawVal) && rawVal != "-")
                    {
                        try
                        {
                            // 쉼표로 분리해서 숫자 리스트로 쪼갠 뒤 e.Value로 스파크라인 작동
                            e.Value = rawVal.Split(',').Select(double.Parse).ToList();
                        }
                        catch
                        {
                            e.Value = null;
                        }
                    }
                }
            }
        }

        #endregion

        #region 데이터 조회

        private void Search()
        {
            SearchCardData();
            DisplayChart();
            DisplayLineChart();
            DisplayGrid();
        }

        #endregion

        #region 대시보드 카드 데이터 로드

        private void SearchCardData()
        {
            string yyyyMM = barDate.smtStartDateTimeValue.ToString("yyyyMM");
            string itemCode = LupItem.smtLookUpValue.ConvertString_Null_To_Empty();
            string processCode = LupProcess.smtLookUpValue.ConvertString_Null_To_Empty();

            query = $@"WITH BaseData AS (
                       SELECT
                           qc.Qty AS Bad_Qty,
                           res.WORKQTY AS Work_Qty,
                           qc.DefectCode,
                           cd.Title
                       FROM QM_WorkDefect qc
                       LEFT JOIN PM_ORDERRESULT res ON qc.SERIALNO = res.SERIALNO AND qc.ORDERNO = res.ORDERNO
                       LEFT JOIN CM_DefectCode cd ON qc.DefectCode = cd.FaultCode
                       WHERE qc.CorpId = '{WrGlobal.CorpID}'
                         AND LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6) = '{yyyyMM}' ";

            if (itemCode != "" && itemCode.ToUpper() != "ALL")
            {
                query += $"\nAND qc.ItemCode = '{itemCode}' ";
            }

            if (processCode != "" && processCode.ToUpper() != "ALL")
            {
                query += $"\nAND FromProcessCode = '{processCode}' ";
            }

            query += $@" ), RankedDefects AS (
                             -- 가장 많이 발생한 불량 유형 순위 계산
                             SELECT
                                 Title,
                                 SUM(Bad_Qty) AS DefectQty,
                                 ROW_NUMBER() OVER (ORDER BY SUM(Bad_Qty) DESC) AS RowNum
                             FROM BaseData
                             GROUP BY Title
                         )
                         SELECT
                             -- 총 불량수량
                             ISNULL(SUM(b.Bad_Qty), 0) AS Total_Bad_Qty,

                             -- 전체 생산량 대비 불량률
                             ROUND(ISNULL(SUM(b.Bad_Qty), 0) * 100.0 / NULLIF(ISNULL(SUM(b.Work_Qty), 0), 0), 2) AS Bad_Rate,

                             -- 최다 불량 유형
                             ISNULL((SELECT TOP 1 Title FROM RankedDefects WHERE RowNum = 1), '없음') AS Top_Defect_Title,
                             ISNULL((SELECT TOP 1 DefectQty FROM RankedDefects WHERE RowNum = 1), '0') AS Top_Defect_Qty,

                             -- 불량유형 수
                             ISNULL(COUNT(DISTINCT b.DefectCode), 0) AS Defect_Type_Count
                         FROM BaseData b";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                LblTotalQty.Text = dr["Total_Bad_Qty"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA";
                LblRate.Text = dr["Bad_Rate"].ConvertFloat_Null_To_Zero().ToString("N2") + " %";
                LblTopType.Text = dr["Top_Defect_Title"].ConvertString_Null_To_Empty() + "";
                LblTopQty.Text = "(" + dr["Top_Defect_Qty"].ConvertFloat_Null_To_Zero().ToString("N0") + " EA)";
                LblCnt.Text = dr["Defect_Type_Count"].ConvertFloat_Null_To_Zero().ToString("N0") + " 건";
            }
        }

        #endregion

        #region 차트 로드

        private void DisplayChart()
        {
            this.Validate();

            string yyyyMM = barDate.smtStartDateTimeValue.ToString("yyyyMM");
            string itemCode = LupItem.smtLookUpValue.ConvertString_Null_To_Empty();
            string processCode = LupProcess.smtLookUpValue.ConvertString_Null_To_Empty();

            //유형별 불량현황
            query = $@"WITH RawData AS (
                         SELECT
                             LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6) AS Date,
                             SUM(qc.Qty) AS Bad_Qty,
                             qc.DefectCode,
                             cd.Title
                         FROM QM_WorkDefect qc
                         LEFT JOIN CM_DefectCode cd ON qc.DefectCode = cd.FaultCode
                         WHERE qc.CorpId = '{WrGlobal.CorpID}'
                           AND LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6) = '{yyyyMM}' ";

            if (itemCode != "" && itemCode.ToUpper() != "ALL")
            {
                query += $"\nAND qc.ItemCode = '{itemCode}' ";
            }

            if (processCode != "" && processCode.ToUpper() != "ALL")
            {
                query += $"\nAND FromProcessCode = '{processCode}' ";
            }

            query += $@"GROUP BY qc.DefectCode, LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6), cd.Title)
                           SELECT
                               Date,
                               DefectCode,
                               Title,
                               Bad_Qty,
                               ROUND(CAST(Bad_Qty AS FLOAT) / SUM(Bad_Qty) OVER() * 100, 1) AS [Percent]
                           FROM RawData
                           ORDER BY Bad_Qty DESC";

            DataTable dt = db.GetDataTable(query);

            SetChartDataQty(ChartQty, dt);
            SetChartDataRatio(ChartRatio, dt);
        }

        private void SetChartDataQty(ChartControl chart, DataTable dt)
        {
            chart.DataSource = dt;

            // 생성한 Series 리스트 추출
            DataTable dtTypes = dt.DefaultView.ToTable(true, "Title");
            chart.Series.Clear();
            chart.Series.BeginUpdate();

            if (dt.Rows.Count == 0)
            {
                string seriesName = Convert.ToString(dtTypes.Columns["Title"]);

                Series series = new Series(seriesName, ViewType.Bar);

                string argu = "불량유형";
                string value = "0";

                series.Points.Add(new SeriesPoint(argu, value));
                chart.Series.Add(series);
            }

            // Series 생성
            foreach (DataRow dataRow in dtTypes.Rows)
            {
                string seriesName = Convert.ToString(dataRow["Title"]);

                Series series = new Series(seriesName, ViewType.Bar);

                BarSeriesView barView = series.View as BarSeriesView;
                if (barView != null)
                {
                    barView.BarWidth = 3;
                }

                // Point 생성
                foreach (DataRow drValue in dt.Select($"Title = '{seriesName}'"))
                {
                    string argu = Convert.ToString(drValue["Title"]) == "" ? "1" : Convert.ToString(drValue["Title"]);    //X축 값
                    string value = Convert.ToString(drValue["Bad_Qty"]);    //Y축 값

                    series.Points.Add(new SeriesPoint(argu, value));
                }

                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                SideBySideBarSeriesLabel label = series.Label as SideBySideBarSeriesLabel;
                if (label != null)
                {
                    label.Position = BarSeriesLabelPosition.Top;
                    label.TextPattern = "{V:#,0}";
                    label.Font = new Font("맑은 고딕", 9F, FontStyle.Regular);
                    label.TextColor = Color.Black;
                    label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
                }

                chart.Series.Add(series);
            }

            chart.Series.EndUpdate();

            XYDiagram diagram = chart.Diagram as XYDiagram;
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        private void SetChartDataRatio(ChartControl chart, DataTable dt)
        {
            chart.Series.Clear();
            chart.Series.BeginUpdate();

            // 도넛 시리즈 생성
            Series doughnutSeries = new Series("불량비율", ViewType.Doughnut);

            if (dt.Rows.Count == 0)
            {
                doughnutSeries.Points.Add(new SeriesPoint("데이터 없음", 0));
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.Series.Add(doughnutSeries);
            }
            else
            {
                // 전체 수량 합계
                double totalQty = Convert.ToDouble(dt.Compute("SUM(Bad_Qty)", ""));

                foreach (DataRow dr in dt.Rows)
                {
                    string title = Convert.ToString(dr["Title"]) == "" ? "기타" : Convert.ToString(dr["Title"]);
                    double qty = Convert.ToDouble(dr["Bad_Qty"]);

                    doughnutSeries.Points.Add(new SeriesPoint(title, qty));
                }

                // 그래프 자체 라벨 숨기기
                doughnutSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                chart.Legend.MarkerMode = LegendMarkerMode.Marker;

                // {A} = 불량유형명, {VP:P1} = 차트가 자동 계산한 백분율(소수점 1자리)
                doughnutSeries.LegendTextPattern = "{A}   {VP:P1}";
                chart.Legend.Font = new Font("맑은 고딕", 9F, FontStyle.Regular);

                chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                chart.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
                chart.Legend.Direction = LegendDirection.TopToBottom;
                chart.Series.Add(doughnutSeries);

                // 도넛 뷰 설정 (가운데 총 수량 표시)
                DoughnutSeriesView view = doughnutSeries.View as DoughnutSeriesView;
                if (view != null)
                {
                    view.Rotation = 270;
                    view.HoleRadiusPercent = 60; // 가운데 구멍 크기 기본 조절

                    // 가운데 총합 라벨 활성화
                    view.TotalLabel.Visible = true;

                    // 라벨 안의 텍스트 패턴 설정
                    view.TotalLabel.TextPattern = "총 불량수량\n{TV:#,0}\nEA";

                    // 가운데 글자 스타일 설정
                    view.TotalLabel.Font = new Font("맑은 고딕", 11F, FontStyle.Bold);
                }
            }

            chart.Series.EndUpdate();
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        private void DisplayLineChart()
        {
            string yyyyMM = barDate.smtStartDateTimeValue.ToString("yyyyMM");
            string itemCode = LupItem.smtLookUpValue.ConvertString_Null_To_Empty();
            string processCode = LupProcess.smtLookUpValue.ConvertString_Null_To_Empty();

            string targetDate = yyyyMM + "01";

            query = $@"SELECT
                        LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6) AS Date,
                        qc.DefectCode,
                        cd.Title,
                        SUM(qc.Qty) AS Bad_Qty
                    FROM QM_WorkDefect qc
                    LEFT JOIN CM_DefectCode cd ON qc.DefectCode = cd.FaultCode
                    WHERE qc.CorpId = '{WrGlobal.CorpID}'
                      AND qc.WorkDate >= DATEADD(MONTH, -6, CAST('{targetDate}' AS DATE))
                      AND qc.WorkDate < DATEADD(MONTH, 1, CAST('{targetDate}' AS DATE)) ";

            if (itemCode != "" && itemCode.ToUpper() != "ALL")
            {
                query += $"\nAND qc.ItemCode = '{itemCode}' ";
            }

            if (processCode != "" && processCode.ToUpper() != "ALL")
            {
                query += $"\nAND FromProcessCode = '{processCode}' ";
            }

            query += $@"GROUP BY LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6), qc.DefectCode, cd.Title
            ORDER BY Date ASC, SUM(qc.Qty) DESC";

            DataTable dtTrend = db.GetDataTable(query);

            SetChartDataTrendLine(lineChart, dtTrend);
        }

        private void SetChartDataTrendLine(ChartControl chart, DataTable dt)
        {
            chart.Series.Clear();

            DataTable dtTypes = dt.DefaultView.ToTable(true, "Title");

            foreach (DataRow typeRow in dtTypes.Rows)
            {
                string seriesName = Convert.ToString(typeRow["Title"]);
                if (string.IsNullOrEmpty(seriesName)) seriesName = "기타";

                Series lineSeries = new Series(seriesName, ViewType.Line);
                lineSeries.ArgumentScaleType = ScaleType.Qualitative;

                DataRow[] trendRows = dt.Select($"Title = '{seriesName}'", "Date ASC");
                foreach (DataRow dr in trendRows)
                {
                    // 문자열로 변환하여 입력
                    string xDate = Convert.ToString(dr["Date"]);
                    double yQty = Convert.ToDouble(dr["Bad_Qty"]);

                    lineSeries.Points.Add(new SeriesPoint(xDate, yQty));
                }

                // 라인 차트 스타일
                lineSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

                LineSeriesView view = lineSeries.View as LineSeriesView;
                if (view != null)
                {
                    view.LineStyle.Thickness = 3; // 선 두께
                    view.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True; // 꺾임점 마커 표시
                    view.LineMarkerOptions.Size = 6; // 마커 크기
                }

                chart.Series.Add(lineSeries);
            }

            // 범례 기본 설정 (우측 정렬)
            chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
            chart.Legend.Direction = LegendDirection.TopToBottom;
            chart.Legend.Font = new Font("맑은 고딕", 9F, FontStyle.Regular);

            chart.Series.EndUpdate();
            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        #endregion

        #region 그리드 데이터 로드

        private void DisplayGrid()
        {
            string yyyyMM = barDate.smtStartDateTimeValue.ToString("yyyyMM");
            string itemCode = LupItem.smtLookUpValue.ConvertString_Null_To_Empty();
            string processCode = LupProcess.smtLookUpValue.ConvertString_Null_To_Empty();

            query = $@"WITH PeriodBadData AS (
                       SELECT
                           qc.DefectCode,
                           cd.Title,
                           SUM(qc.Qty) AS BadQty
                       FROM QM_WorkDefect qc
                       LEFT JOIN CM_DefectCode cd ON qc.DefectCode = cd.FaultCode
                       WHERE qc.CorpId = '{WrGlobal.CorpID}'
                         AND LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6) = '{yyyyMM}' ";

            if (itemCode != "" && itemCode.ToUpper() != "ALL")
            {
                query += $"\nAND qc.ItemCode = '{itemCode}' ";
            }

            if (processCode != "" && processCode.ToUpper() != "ALL")
            {
                query += $"\nAND FromProcessCode = '{processCode}' ";
            }

            query += $@" GROUP BY qc.DefectCode, cd.Title
                        ),
                        TotalBadData AS (
                            SELECT
                                qc.DefectCode,
                                SUM(qc.Qty) AS CumulativeTotalQty
                            FROM QM_WorkDefect qc
                            WHERE qc.CorpId = '{WrGlobal.CorpID}' ";

            if (itemCode != "" && itemCode.ToUpper() != "ALL")
            {
                query += $"\nAND qc.ItemCode = '{itemCode}' ";
            }
            if (processCode != "" && processCode.ToUpper() != "ALL")
            {
                query += $"\nAND FromProcessCode = '{processCode}' ";
            }

            query += $@" GROUP BY qc.DefectCode),
                         TrendData AS (
                             SELECT
                                 qc.DefectCode,
                                 LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6) AS Date,
                                 SUM(qc.Qty) AS Monthly_Qty
                             FROM QM_WorkDefect qc
                             WHERE qc.CorpId = '{WrGlobal.CorpID}'
                               AND qc.WorkDate >= DATEADD(MONTH, -5, CAST('{yyyyMM}01' AS DATE)) -- 6개월간
                               AND qc.WorkDate < DATEADD(MONTH, 1, CAST('{yyyyMM}01' AS DATE)) ";

            if (itemCode != "" && itemCode.ToUpper() != "ALL")
            {
                query += $"\nAND qc.ItemCode = '{itemCode}' ";
            }
            if (processCode != "" && processCode.ToUpper() != "ALL")
            {
                query += $"\nAND FromProcessCode = '{processCode}' ";
            }

            query += $@" GROUP BY qc.DefectCode, LEFT(CONVERT(VARCHAR, qc.WorkDate, 112), 6)),
                        TrendPivot AS (
                           SELECT 
                               DefectCode,
                               STUFF((
                                   SELECT ',' + CAST(t2.Monthly_Qty AS VARCHAR)
                                   FROM TrendData t2
                                   WHERE t2.DefectCode = t1.DefectCode
                                   ORDER BY t2.Date ASC
                                   FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') AS TrendFlow
                           FROM TrendData t1
                           GROUP BY DefectCode
                        )
                        SELECT
                            p.DefectCode,
                            p.Title,
                            p.BadQty,

                            -- 불량률 (%)
                            ROUND(p.BadQty * 100.0 / NULLIF(SUM(p.BadQty) OVER(), 0), 2) AS BadRate,

                            -- 누적수량
                            ISNULL(t.CumulativeTotalQty, 0) AS CumulativeQty,

                            -- 누적비율
                            ROUND(ISNULL(t.CumulativeTotalQty, 0) * 100.0 / NULLIF(SUM(t.CumulativeTotalQty) OVER(), 0), 2) AS CumulativeRate,

                            -- 추이
                            ISNULL(tp.TrendFlow, '-') AS TrendFlow
                        FROM PeriodBadData p
                        LEFT JOIN TotalBadData t ON p.DefectCode = t.DefectCode
                        LEFT JOIN TrendPivot tp ON p.DefectCode = tp.DefectCode
                        ORDER BY p.BadQty DESC;";

            DataTable dt = db.GetDataTable(query);

            GcDefect.DataSource = dt;
            GvDefect.BestFitColumns();
        }

		#endregion

		private void timer1_Tick(object sender, EventArgs e)
		{
            //LblCurTime.Text = DateTime.Now.ToString("yyyy.MM.dd HH:MM:ss");
        }

		private void Tm_Search_Tick(object sender, EventArgs e)
		{
            Search();
		}

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}