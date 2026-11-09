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
    public partial class Frm_QM_WorkDefectViewProcess : DevExpress.XtraEditors.XtraForm
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
        private readonly Color dangerColor = Color.FromArgb(201, 82, 82);

        private DataTable dtProcess = new DataTable();
        private bool isBinding = false;

        decimal GoalNgPer = 0.80M; /* 목표 불량률 : CM_Class 'A005010' ExtraValue 기준 */

        public Frm_QM_WorkDefectViewProcess()
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
            dtProcess = code.GetProcess_List_ProcessType(true, null);

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
            gv.OptionsFind.FindNullPrompt = "공정 검색";

            gv.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            gv.RowHeight = 30;
            gv.ColumnPanelRowHeight = 34;

            grid.Create_GvColumn(gv, "순위", "Rank");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "불량수량", "Qty");
            grid.Create_GvColumn(gv, "불량률", "DefectRate");
            grid.Create_GvColumn(gv, "불량점유율", "ShareRate");

            gv.Columns["Rank"].Width = 60;
            gv.Columns["ProcessCode"].Width = 110;
            gv.Columns["ProcessName"].Width = 180;
            gv.Columns["WorkQty"].Width = 120;
            gv.Columns["Qty"].Width = 120;
            gv.Columns["DefectRate"].Width = 100;
            gv.Columns["ShareRate"].Width = 110;

            gv.Columns["Rank"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["ProcessCode"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["DefectRate"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ShareRate"], clsGrid.TypeList.TFloat);

            gv.Columns["WorkQty"].DisplayFormat.FormatString = "N0";
            gv.Columns["Qty"].DisplayFormat.FormatString = "N0";

            grid.SetCol_Summary(gv, "WorkQty");
            grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "DefectRate", DevExpress.Data.SummaryItemType.Average, "{0:N2}");

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

            string processCode = GvQM_WorkDefect
                .GetRowCellValue(e.FocusedRowHandle, "ProcessCode")
                .ConvertString_Null_To_Empty();

            string processName = GvQM_WorkDefect
                .GetRowCellValue(e.FocusedRowHandle, "ProcessName")
                .ConvertString_Null_To_Empty();

            SearchTrend(processCode, processName);
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
                    e.Appearance.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
                }
            }
        }

        private void SetChart()
        {
            SetProcessChart();
            SetTrendChart();
        }

        private void SetProcessChart()
        {
            ChtPareto.Series.Clear();
            ChtPareto.Titles.Clear();

            ChtPareto.BorderOptions.Visibility = DefaultBoolean.False;
            ChtPareto.Legend.Visibility = DefaultBoolean.False;

            ChartTitle chartTitle = new ChartTitle();

            chartTitle.Text = "공정별 불량률 비교";
            chartTitle.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);

            ChtPareto.Titles.Add(chartTitle);

            Series rateSeries = new Series("불량률", ViewType.Bar);

            rateSeries.ArgumentDataMember = "ProcessName";
            rateSeries.ValueDataMembers.AddRange(new string[] { "DefectRate" });
            rateSeries.ArgumentScaleType = ScaleType.Qualitative;
            rateSeries.LabelsVisibility = DefaultBoolean.True;
            rateSeries.Label.TextPattern = "{V:N2}%";

            BarSeriesView barView = rateSeries.View as BarSeriesView;

            if (barView != null)
            {
                barView.Color = mintColor;
                barView.BarWidth = 0.65D;
            }

            ChtPareto.Series.Add(rateSeries);

            XYDiagram diagram = ChtPareto.Diagram as XYDiagram;

            if (diagram != null)
            {
                diagram.AxisX.Label.Angle = -25;
                diagram.AxisY.Title.Visibility = DefaultBoolean.True;
                diagram.AxisY.Title.Text = "불량률(%)";
                diagram.AxisY.WholeRange.AlwaysShowZeroLevel = true;

                ConstantLine targetLine = new ConstantLine("목표 불량률", GoalNgPer);

                targetLine.Color = dangerColor;
                targetLine.ShowInLegend = true;

                diagram.AxisY.ConstantLines.Add(targetLine);
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

            chartTitle.Text = "선택 공정 일별 불량률 추이";
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
            DataTable summaryData = SelectProcessSummary();

            SetProcessName(summaryData);

            isBinding = true;

            try
            {
                GvQM_WorkDefect.GridControl.DataSource = summaryData;
                ChtPareto.DataSource = summaryData;

                SetCardData(summaryData);

                if (summaryData.Rows.Count > 0)
                {
                    GvQM_WorkDefect.FocusedRowHandle = 0;

                    string processCode = summaryData.Rows[0]["ProcessCode"].ConvertString_Null_To_Empty();
                    string processName = summaryData.Rows[0]["ProcessName"].ConvertString_Null_To_Empty();

                    SearchTrend(processCode, processName);
                }
                else
                {
                    ChtTrend.DataSource = null;
                    SetTrendTitle("선택 공정 일별 불량률 추이");
                }
            }
            finally
            {
                isBinding = false;
            }
        }

        /// <summary>
        /// 공정별 생산수량, 불량수량 및 불량률 조회
        ///
        /// 공정별 불량수량은 생산실적에 입력된 PM_ORDERRESULT.NGQTY를 기준으로 한다.
        /// QM_WorkDefect.Qty는 불량유형별 상세수량이므로 공정 불량수량 산정에 사용하지 않는다.
        /// </summary>
        private DataTable SelectProcessSummary()
        {
            string resultWhere = GetResultWhereCondition();

            query = $@"
                WITH ResultSummary AS
                (
                    SELECT
                          res.PROCESSCODE AS ProcessCode
                        , SUM(ISNULL(res.WORKQTY, 0)) AS WorkQty
                        , SUM(ISNULL(res.NGQTY, 0)) AS Qty
                    FROM PM_ORDERRESULT res
                    WHERE {resultWhere}
                    GROUP BY res.PROCESSCODE
                ),
                ProcessCalc AS
                (
                    SELECT
                          rs.ProcessCode
                        , rs.WorkQty
                        , rs.Qty
                        , CASE
                              WHEN rs.WorkQty = 0 THEN 0
                              ELSE rs.Qty * 100.0 / rs.WorkQty
                          END AS DefectRate
                        , SUM(rs.Qty) OVER () AS TotalDefectQty
                    FROM ResultSummary rs
                    WHERE ISNULL(rs.ProcessCode, '') <> ''
                )
                SELECT
                      ROW_NUMBER() OVER
                      (
                          ORDER BY
                                pc.DefectRate DESC
                              , pc.Qty DESC
                              , pc.ProcessCode
                      ) AS Rank
                    , pc.ProcessCode
                    , pc.WorkQty
                    , pc.Qty
                    , ROUND(pc.DefectRate, 2) AS DefectRate
                    , CASE
                          WHEN pc.TotalDefectQty = 0 THEN 0
                          ELSE ROUND(pc.Qty * 100.0 / pc.TotalDefectQty, 2)
                      END AS ShareRate
                FROM ProcessCalc pc
                ORDER BY
                      pc.DefectRate DESC
                    , pc.Qty DESC
                    , pc.ProcessCode;
            ";

            return db.GetDataTable(query);
        }

        /// <summary>
        /// 선택 공정 일별 불량률 추이
        /// </summary>
        private DataTable SelectProcessTrend(string processCode)
        {
            string resultWhere = GetResultWhereCondition(false);

            query = $@"
                WITH ResultDaily AS
                (
                    SELECT
                          CONVERT(date, res.WORKDATE) AS WorkDay
                        , SUM(ISNULL(res.WORKQTY, 0)) AS WorkQty
                        , SUM(ISNULL(res.NGQTY, 0)) AS Qty
                    FROM PM_ORDERRESULT res
                    WHERE {resultWhere}
                      AND res.PROCESSCODE = '{processCode}'
                    GROUP BY CONVERT(date, res.WORKDATE)
                )
                SELECT
                      CONVERT(varchar(10), rd.WorkDay, 120) AS Period
                    , rd.WorkQty
                    , rd.Qty
                    , CASE
                          WHEN rd.WorkQty = 0 THEN 0
                          ELSE ROUND(rd.Qty * 100.0 / rd.WorkQty, 2)
                      END AS DefectRate
                FROM ResultDaily rd
                ORDER BY rd.WorkDay;
            ";

            return db.GetDataTable(query);
        }

        #endregion

        #region 조회조건

        private string GetResultWhereCondition(bool includeProcess = true)
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

            if (includeProcess && IsAll(processCode) == false)
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

        private bool IsAll(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                || value.Equals("ALL", StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region 공정명 설정

        private void SetProcessName(DataTable table)
        {
            if (table == null)
            {
                return;
            }

            if (table.Columns.Contains("ProcessName") == false)
            {
                table.Columns.Add("ProcessName", typeof(string));
            }

            foreach (DataRow row in table.Rows)
            {
                string processCode = row["ProcessCode"].ConvertString_Null_To_Empty();
                string processName = processCode;

                if (dtProcess != null && dtProcess.Columns.Contains("공정코드"))
                {
                    string findProcessCode = processCode.Replace("'", "''");
                    DataRow[] processRows = dtProcess.Select("[공정코드] = '" + findProcessCode + "'");

                    if (processRows.Length > 0 && dtProcess.Columns.Contains("공정명"))
                    {
                        processName = processRows[0]["공정명"].ConvertString_Null_To_Empty();
                    }
                }

                row["ProcessName"] = processName;
            }
        }

        #endregion

        #region 카드

        private void SetCardData(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                LblTotalQty.Text = "0 EA";
                LblCnt.Text = "0.00 %";
                labelControl6.Text = "-";
                LblRate.Text = "0 개";
                LblTopType.Text = "없음";
                LblTopQty.Text = "(0 EA)";

                return;
            }

            decimal totalBadQty = 0;
            decimal maxDefectRate = -1;
            decimal maxDefectQty = -1;
            string maxRateProcessName = "";
            string maxQtyProcessName = "";

            foreach (DataRow row in table.Rows)
            {
                decimal qty = row["Qty"].ConvertDecimal_Null_To_Zero();
                decimal defectRate = row["DefectRate"].ConvertDecimal_Null_To_Zero();
                string processName = row["ProcessName"].ConvertString_Null_To_Empty();

                totalBadQty += qty;

                if (defectRate > maxDefectRate)
                {
                    maxDefectRate = defectRate;
                    maxRateProcessName = processName;
                }

                if (qty > maxDefectQty)
                {
                    maxDefectQty = qty;
                    maxQtyProcessName = processName;
                }
            }

            LblTotalQty.Text = totalBadQty.ToString("N0") + " EA";
            LblCnt.Text = maxDefectRate.ToString("N2") + " %";
            labelControl6.Text = "[" + maxRateProcessName + "]";
            LblRate.Text = table.Rows.Count.ToString("N0") + " 개";
            LblTopType.Text = maxQtyProcessName;
            LblTopQty.Text = "(" + maxDefectQty.ToString("N0") + " EA)";
        }

        #endregion

        #region 추이 조회

        private void SearchTrend(string processCode, string processName)
        {
            if (string.IsNullOrWhiteSpace(processCode))
            {
                ChtTrend.DataSource = null;
                return;
            }

            DataTable trendData = SelectProcessTrend(processCode);

            ChtTrend.DataSource = trendData;

            SetTrendTitle("[" + processName + "] 일별 불량률 추이");
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

            string processName = hitInfo.SeriesPoint.Argument;
            int rowHandle = GvQM_WorkDefect.LocateByValue("ProcessName", processName);

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
