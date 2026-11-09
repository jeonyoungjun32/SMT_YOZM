using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_WorkDefectViewType : DevExpress.XtraEditors.XtraForm
    {
        WrComFunc func = new WrComFunc();
        Database db = new Database();
        clsCode code = new clsCode();
        string query = "";
        clsGrid grid = new clsGrid();
        cls_Lookup clsLup = new cls_Lookup();

        // 품목구분 리스트
        // 10 : 상품, 20 : 제품, 30 : 반제품, 40 : 구매자재, 50 : 부자재, 60 : 샘플/개발
        private string[] ItemDiv = { "20", "30" };

        private readonly Color mintColor = Color.FromArgb(54, 143, 123);
        private readonly Color orangeColor = Color.FromArgb(205, 137, 52);
        private readonly Color dangerColor = Color.FromArgb(201, 82, 82);

        private bool isBinding = false;

        decimal GoalNgPer = 0.80M; /* 목표 불량률 : CM_Class 'A005010' ExtraValue 기준 */

        public Frm_QM_WorkDefectViewType()
        {
            InitializeComponent();
        }

        private void frmQM_WorkDefectView_Load(object sender, EventArgs e)
        {
            Init();

            SetLup();
            Set_GvRework(GvQM_WorkDefect);
            SetChart();

            Search();
        }

        private void Init()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            DataRow drClass = code.GetClassInfo("A005010");

            if (drClass != null)
            {
                decimal goalNgPer = drClass["ExtraValue"].ConvertDecimal_Null_To_Zero();
                if (goalNgPer != 0) GoalNgPer = goalNgPer;
            }
        }

        private void SetLup()
        {
            // 공정
            DataTable dtProcess = code.GetProcess_List_ProcessType(true, null);

            clsLup.Create_RepositoryItemSearchLookUp(Lup_Process, dtProcess, "공정코드", "공정명");
            Lup_Process.smtLookUpValue = "ALL";

            // 설비
            DataTable dtMachine = code.GetMachineList(true);

            clsLup.Create_RepositoryItemSearchLookUp(LupMachine, dtMachine, "공정코드", "공정명");
            LupMachine.smtLookUpValue = "ALL";

            // 품목
            DataTable dtItem = code.GetMaterialList(ItemDiv, true).Table;

            clsLup.Create_RepositoryItemSearchLookUp(Lup_Item, dtItem, "품목코드", "품명");
            Lup_Item.smtLookUpValue = "ALL";
        }

        private void Set_GvRework(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ShowIndicator = false;
            gv.OptionsView.ShowAutoFilterRow = true;

            gv.OptionsBehavior.Editable = false;
            gv.OptionsBehavior.ReadOnly = true;

            gv.OptionsFind.AlwaysVisible = true;
            gv.OptionsFind.FindNullPrompt = "불량유형 검색";

            gv.FocusRectStyle = DrawFocusRectStyle.RowFocus;

            gv.RowHeight = 30;
            gv.ColumnPanelRowHeight = 34;

            grid.Create_GvColumn(gv, "순위", "Rank");
            grid.Create_GvColumn(gv, "불량코드", "DefectCode");
            grid.Create_GvColumn(gv, "불량명", "DefectName");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "불량수량", "Qty");
            grid.Create_GvColumn(gv, "불량률", "DefectRate");
            grid.Create_GvColumn(gv, "점유율", "ShareRate");
            grid.Create_GvColumn(gv, "누적비율", "CumulativeRate");

            gv.Columns["Rank"].Width = 60;
            gv.Columns["DefectCode"].Width = 100;
            gv.Columns["DefectName"].Width = 180;
            gv.Columns["WorkQty"].Width = 120;
            gv.Columns["Qty"].Width = 120;
            gv.Columns["DefectRate"].Width = 100;
            gv.Columns["ShareRate"].Width = 100;
            gv.Columns["CumulativeRate"].Width = 100;

            gv.Columns["Rank"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["DefectCode"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["DefectRate"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ShareRate"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["CumulativeRate"], clsGrid.TypeList.TFloat);

            //gv.Columns["WorkQty"].DisplayFormat.FormatString = "N0";
            //gv.Columns["Qty"].DisplayFormat.FormatString = "N0";
            //gv.Columns["DefectRate"].DisplayFormat.FormatString = "N2' %'";
            //gv.Columns["ShareRate"].DisplayFormat.FormatString = "N2' %'";
            //gv.Columns["CumulativeRate"].DisplayFormat.FormatString = "N2' %'";

            grid.SetCol_Summary(gv, "Qty");

            gv.RowCellStyle += GvQM_WorkDefect_RowCellStyle;
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

        private void Gv_FocusedRowObjectChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (isBinding || e.FocusedRowHandle < 0)
            {
                return;
            }

            string defectCode = GvQM_WorkDefect
                .GetRowCellValue(e.FocusedRowHandle, "DefectCode")
                .ConvertString_Null_To_Empty();

            string defectName = GvQM_WorkDefect
                .GetRowCellValue(e.FocusedRowHandle, "DefectName")
                .ConvertString_Null_To_Empty();

            SearchTrend(defectCode, defectName);
        }

        private void GvQM_WorkDefect_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Column.FieldName == "DefectRate")
            {
                decimal defectRate = GvQM_WorkDefect
                    .GetRowCellValue(e.RowHandle, "DefectRate")
                    .ConvertDecimal_Null_To_Zero();

                if (defectRate >= GoalNgPer)
                {
                    e.Appearance.ForeColor = dangerColor;
                }
            }
        }

        private void SetChart()
        {
            SetParetoChart();
            SetTrendChart();
        }

        private void SetParetoChart()
        {
            ChtPareto.Series.Clear();
            ChtPareto.Titles.Clear();

            ChtPareto.BorderOptions.Visibility = DefaultBoolean.False;
            ChtPareto.Legend.Visibility = DefaultBoolean.True;
            ChtPareto.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            ChtPareto.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;

            ChartTitle chartTitle = new ChartTitle();

            chartTitle.Text = "불량유형별 파레토";
            chartTitle.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);

            ChtPareto.Titles.Add(chartTitle);

            // 불량수량 막대
            Series qtySeries = new Series("불량수량", ViewType.Bar);

            qtySeries.ArgumentDataMember = "DefectName";
            qtySeries.ValueDataMembers.AddRange(new string[] { "Qty" });
            qtySeries.ArgumentScaleType = ScaleType.Qualitative;
            qtySeries.LabelsVisibility = DefaultBoolean.True;
            qtySeries.Label.TextPattern = "{V:#,##0}";

            BarSeriesView barView = qtySeries.View as BarSeriesView;

            if (barView != null)
            {
                barView.Color = mintColor;
                barView.BarWidth = 0.65D;
            }

            // 누적비율 꺾은선
            Series cumulativeSeries = new Series("누적비율", ViewType.Line);

            cumulativeSeries.ArgumentDataMember = "DefectName";
            cumulativeSeries.ValueDataMembers.AddRange(new string[] { "CumulativeRate" });
            cumulativeSeries.ArgumentScaleType = ScaleType.Qualitative;
            cumulativeSeries.LabelsVisibility = DefaultBoolean.False;

            LineSeriesView lineView = cumulativeSeries.View as LineSeriesView;

            if (lineView != null)
            {
                lineView.Color = orangeColor;
                lineView.MarkerVisibility = DefaultBoolean.True;
                lineView.LineMarkerOptions.Size = 8;
            }

            ChtPareto.Series.AddRange(new Series[] { qtySeries, cumulativeSeries });

            XYDiagram diagram = ChtPareto.Diagram as XYDiagram;

            if (diagram != null)
            {
                diagram.AxisX.Label.Angle = -25;
                diagram.AxisY.Title.Visibility = DefaultBoolean.True;
                diagram.AxisY.Title.Text = "불량수량";
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                SecondaryAxisY secondaryAxis = new SecondaryAxisY("누적비율축");

                secondaryAxis.Title.Visibility = DefaultBoolean.True;
                secondaryAxis.Title.Text = "누적비율(%)";
                secondaryAxis.WholeRange.SetMinMaxValues(0, 100);

                diagram.SecondaryAxesY.Add(secondaryAxis);

                if (lineView != null)
                {
                    lineView.AxisY = secondaryAxis;
                }
            }

            ChtPareto.CrosshairEnabled = DefaultBoolean.True;
            ChtPareto.MouseClick += ChtPareto_MouseClick;
        }

        private void SetTrendChart()
        {
            ChtTrend.Series.Clear();
            ChtTrend.Titles.Clear();

            ChtTrend.BorderOptions.Visibility = DefaultBoolean.False;
            ChtTrend.Legend.Visibility = DefaultBoolean.False;

            ChartTitle chartTitle = new ChartTitle();

            chartTitle.Text = "선택 불량유형 일별 추이";
            chartTitle.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);

            ChtTrend.Titles.Add(chartTitle);

            Series rateSeries = new Series("불량률", ViewType.Line);

            rateSeries.ArgumentDataMember = "Period";
            rateSeries.ValueDataMembers.AddRange(new string[] { "DefectRate" });
            rateSeries.ArgumentScaleType = ScaleType.Qualitative;
            rateSeries.LabelsVisibility = DefaultBoolean.True;
            rateSeries.Label.TextPattern = "{V:N2}%";

            LineSeriesView lineView = rateSeries.View as LineSeriesView;

            if (lineView != null)
            {
                lineView.Color = mintColor;
                lineView.MarkerVisibility = DefaultBoolean.True;
                lineView.LineMarkerOptions.Size = 8;
            }

            ChtTrend.Series.Add(rateSeries);

            XYDiagram diagram = ChtTrend.Diagram as XYDiagram;

            if (diagram != null)
            {
                diagram.AxisY.Title.Visibility = DefaultBoolean.True;
                diagram.AxisY.Title.Text = "불량률(%)";
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                ConstantLine targetLine = new ConstantLine("목표 불량률", GoalNgPer);

                targetLine.Color = dangerColor;
                targetLine.ShowInLegend = true;

                diagram.AxisY.ConstantLines.Add(targetLine);
            }

            ChtTrend.CrosshairEnabled = DefaultBoolean.True;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        #region 조회

        private void Search()
        {
            DataRow resultData = SelectResultSummary();
            DataTable summaryData = SelectDefectSummary();

            isBinding = true;

            try
            {
                GvQM_WorkDefect.GridControl.DataSource = summaryData;
                ChtPareto.DataSource = summaryData;

                SetCardData(resultData, summaryData);

                if (summaryData.Rows.Count > 0)
                {
                    GvQM_WorkDefect.FocusedRowHandle = 0;

                    string defectCode = summaryData.Rows[0]["DefectCode"].ConvertString_Null_To_Empty();
                    string defectName = summaryData.Rows[0]["DefectName"].ConvertString_Null_To_Empty();

                    SearchTrend(defectCode, defectName);
                }
                else
                {
                    ChtTrend.DataSource = null;
                    SetTrendTitle("선택 불량유형 일별 추이");
                }
            }
            finally
            {
                isBinding = false;
            }
        }

        /// <summary>
        /// 전체 생산수량과 전체 불량수량 조회
        ///
        /// 전체 불량수량은 생산실적에 입력된 PM_ORDERRESULT.NGQTY를 기준으로 한다.
        /// QM_WorkDefect.Qty는 불량유형별 상세수량이므로 전체 불량수량으로 사용하지 않는다.
        /// </summary>
        private DataRow SelectResultSummary()
        {
            string resultWhere = GetResultWhereCondition();

            query = $@"
                SELECT
                      ISNULL(SUM(ISNULL(res.WORKQTY, 0)), 0) AS WorkQty
                    , ISNULL(SUM(ISNULL(res.NGQTY, 0)), 0) AS ResultNgQty
                    , CASE
                          WHEN SUM(ISNULL(res.WORKQTY, 0)) = 0 THEN 0
                          ELSE ROUND(
                              SUM(ISNULL(res.NGQTY, 0)) * 100.0
                              / SUM(ISNULL(res.WORKQTY, 0)),
                              2
                          )
                      END AS ResultDefectRate
                FROM PM_ORDERRESULT res
                WHERE {resultWhere};
            ";

            return db.GetDataRow(query);
        }

        /// <summary>
        /// 불량유형별 요약 조회
        /// </summary>
        private DataTable SelectDefectSummary()
        {
            string resultWhere = GetResultWhereCondition();
            string defectWhere = GetDefectWhereCondition();

            query = $@"
                WITH ResultBase AS
                (
                    /*
                     * 생산실적은 별도로 합산한다.
                     *
                     * QM_WorkDefect와 직접 JOIN하면
                     * 불량유형 개수만큼 WORKQTY가 중복된다.
                     */
                    SELECT
                        ISNULL(SUM(ISNULL(res.WORKQTY, 0)), 0) AS WorkQty
                    FROM PM_ORDERRESULT res
                    WHERE {resultWhere}
                ),
                DefectSummary AS
                (
                    SELECT
                          qc.DefectCode
                        , ISNULL(cd.Title, qc.DefectCode) AS DefectName
                        , SUM(ISNULL(qc.Qty, 0)) AS Qty
                    FROM QM_WorkDefect qc

                    LEFT JOIN CM_DefectCode cd
                      ON qc.CorpId = cd.CorpId
                     AND qc.DefectCode = cd.FaultCode

                    WHERE {defectWhere}

                    GROUP BY
                          qc.DefectCode
                        , ISNULL(cd.Title, qc.DefectCode)
                ),
                DefectCalc AS
                (
                    SELECT
                          ds.DefectCode
                        , ds.DefectName
                        , ds.Qty
                        , ROW_NUMBER() OVER
                          (
                              ORDER BY
                                    ds.Qty DESC
                                  , ds.DefectCode
                          ) AS Rank
                        , SUM(ds.Qty) OVER () AS TotalDefectQty
                        , SUM(ds.Qty) OVER
                          (
                              ORDER BY
                                    ds.Qty DESC
                                  , ds.DefectCode
                              ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
                          ) AS CumulativeQty
                    FROM DefectSummary ds
                )
                SELECT
                      dc.Rank
                    , dc.DefectCode
                    , dc.DefectName
                    , rb.WorkQty
                    , dc.Qty

                    -- 전체 생산량 대비 해당 불량유형 불량률
                    , CASE
                          WHEN rb.WorkQty = 0 THEN 0
                          ELSE ROUND(dc.Qty * 100.0 / rb.WorkQty, 2)
                      END AS DefectRate

                    -- 전체 불량수량 중 해당 불량유형 점유율
                    , CASE
                          WHEN dc.TotalDefectQty = 0 THEN 0
                          ELSE ROUND(dc.Qty * 100.0 / dc.TotalDefectQty, 2)
                      END AS ShareRate

                    -- 파레토 누적비율
                    , CASE
                          WHEN dc.TotalDefectQty = 0 THEN 0
                          ELSE ROUND(dc.CumulativeQty * 100.0 / dc.TotalDefectQty, 2)
                      END AS CumulativeRate

                FROM DefectCalc dc
                CROSS JOIN ResultBase rb

                ORDER BY
                      dc.Qty DESC
                    , dc.DefectCode;
            ";

            return db.GetDataTable(query);
        }

        /// <summary>
        /// 선택 불량유형 일별 추이
        /// </summary>
        private DataTable SelectDefectTrend(string defectCode)
        {
            string resultWhere = GetResultWhereCondition();
            string defectWhere = GetDefectWhereCondition();

            query = $@"
                WITH ResultDaily AS
                (
                    SELECT
                          CONVERT(date, res.WORKDATE) AS WorkDay
                        , SUM(ISNULL(res.WORKQTY, 0)) AS WorkQty
                    FROM PM_ORDERRESULT res
                    WHERE {resultWhere}
                    GROUP BY CONVERT(date, res.WORKDATE)
                ),
                DefectDaily AS
                (
                    SELECT
                          CONVERT(date, qc.WorkDate) AS WorkDay
                        , SUM(ISNULL(qc.Qty, 0)) AS Qty
                    FROM QM_WorkDefect qc
                    WHERE {defectWhere}
                      AND qc.DefectCode = '{defectCode}'
                    GROUP BY CONVERT(date, qc.WorkDate)
                )
                SELECT
                      CONVERT(varchar(10), rd.WorkDay, 120) AS Period
                    , rd.WorkQty
                    , ISNULL(dd.Qty, 0) AS Qty
                    , CASE
                          WHEN rd.WorkQty = 0 THEN 0
                          ELSE ROUND(ISNULL(dd.Qty, 0) * 100.0 / rd.WorkQty, 2)
                      END AS DefectRate
                FROM ResultDaily rd

                LEFT JOIN DefectDaily dd
                  ON rd.WorkDay = dd.WorkDay

                ORDER BY rd.WorkDay;
            ";

            return db.GetDataTable(query);
        }

        #endregion

        #region 조회조건

        private string GetResultWhereCondition()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.Date.ToString("yyyy-MM-dd");
            string endDate = Dt_Date.smtEndDateTimeValue.Date.AddDays(1).ToString("yyyy-MM-dd");
            string corpId = WrGlobal.CorpID;
            string itemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            string processCode = Lup_Process.smtLookUpValue.ConvertString_Null_To_Empty();
            string machineCode = LupMachine.smtLookUpValue.ConvertString_Null_To_Empty();

            string result = $@"
                    res.CorpId = '{corpId}'
                AND res.WORKDATE >= '{startDate}'
                AND res.WORKDATE <  '{endDate}'
            ";

            if (IsAll(itemCode) == false)
            {
                result += $@"
                    AND res.ITEMCODE = '{itemCode}'
                ";
            }

            if (IsAll(processCode) == false)
            {
                result += $@"
                    AND res.PROCESSCODE = '{processCode}'
                ";
            }

            if (IsAll(machineCode) == false)
            {
                result += $@"
                    AND res.MACHINECODE = '{machineCode}'
                ";
            }

            return result;
        }

        private string GetDefectWhereCondition()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.Date.ToString("yyyy-MM-dd");
            string endDate = Dt_Date.smtEndDateTimeValue.Date.AddDays(1).ToString("yyyy-MM-dd");
            string corpId = WrGlobal.CorpID;
            string itemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            string processCode = Lup_Process.smtLookUpValue.ConvertString_Null_To_Empty();
            string machineCode = LupMachine.smtLookUpValue.ConvertString_Null_To_Empty();

            string result = $@"
                    qc.CorpId = '{corpId}'
                AND qc.WorkDate >= '{startDate}'
                AND qc.WorkDate <  '{endDate}'
            ";

            if (IsAll(itemCode) == false)
            {
                result += $@"
                    AND qc.ItemCode = '{itemCode}'
                ";
            }

            if (IsAll(processCode) == false)
            {
                result += $@"
                    AND qc.FromProcessCode = '{processCode}'
                ";
            }

            if (IsAll(machineCode) == false)
            {
                result += $@"
                    AND EXISTS
                    (
                        SELECT 1
                        FROM PM_ORDERRESULT machineResult
                        WHERE machineResult.CorpId = qc.CorpId
                          AND machineResult.ORDERNO = qc.ORDERNO
                          AND machineResult.SERIALNO = qc.SERIALNO
                          AND machineResult.PROCESSCODE = qc.FromProcessCode
                          AND machineResult.MACHINECODE = '{machineCode}'
                    )
                ";
            }

            return result;
        }

        private bool IsAll(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                || value.Equals("ALL", StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region 카드

        private void SetCardData(DataRow resultData, DataTable table)
        {
            decimal resultNgQty = 0;
            decimal resultDefectRate = 0;

            if (resultData != null)
            {
                resultNgQty = resultData["ResultNgQty"].ConvertDecimal_Null_To_Zero();
                resultDefectRate = resultData["ResultDefectRate"].ConvertDecimal_Null_To_Zero();
            }

            LblTotalQty.Text = resultNgQty.ToString("N0") + " EA";
            LblRate.Text = resultDefectRate.ToString("N2") + " %";

            if (table == null || table.Rows.Count == 0)
            {
                LblTopType.Text = "없음";
                LblTopQty.Text = "(0 EA)";
                LblCnt.Text = "0 종";

                return;
            }

            DataRow topRow = table.Rows[0];

            LblTopType.Text = topRow["DefectName"].ConvertString_Null_To_Empty();
            LblTopQty.Text = "(" + topRow["Qty"].ConvertDecimal_Null_To_Zero().ToString("N0") + " EA)";
            LblCnt.Text = table.Rows.Count.ToString("N0") + " 종";
        }

        #endregion

        #region 추이 조회

        private void SearchTrend(string defectCode, string defectName)
        {
            if (string.IsNullOrWhiteSpace(defectCode))
            {
                ChtTrend.DataSource = null;
                return;
            }

            DataTable trendData = SelectDefectTrend(defectCode);

            ChtTrend.DataSource = trendData;

            SetTrendTitle("[" + defectName + "] 일별 불량률 추이");
        }

        private void SetTrendTitle(string title)
        {
            if (ChtTrend.Titles.Count > 0)
            {
                ChtTrend.Titles[0].Text = title;
            }
        }

        #endregion

        private void ChtPareto_MouseClick(object sender, MouseEventArgs e)
        {
            ChartHitInfo hitInfo = ChtPareto.CalcHitInfo(e.Location);

            if (hitInfo.InSeriesPoint == false)
            {
                return;
            }

            string defectName = hitInfo.SeriesPoint.Argument;
            int rowHandle = GvQM_WorkDefect.LocateByValue("DefectName", defectName);

            if (rowHandle < 0)
            {
                return;
            }

            GvQM_WorkDefect.FocusedRowHandle = rowHandle;
            GvQM_WorkDefect.MakeRowVisible(rowHandle);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
