using System;
using System.Data;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models.PM;

namespace SmartMES.View.PM
{
    /// <summary>
    /// 포케스팅 관리
    /// </summary>
    public partial class Frm_PM_Forecasting : DevExpress.XtraEditors.XtraForm
    {
        private readonly clsGrid grid = new clsGrid();
        private int refreshVersion;

        public Frm_PM_Forecasting()
        {
            InitializeComponent();
            SetGridView();
            ApplyPeriodMode();
            baseMonth.EditValueChanged += BaseMonth_EditValueChanged;
        }

        private void Frm_PM_Forecasting_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        /// <summary>실 발주량 조회 그리드 설정</summary>
        private void SetGridView()
        {
            grid.Set_Navigator(forecastView);
            grid.Create_GvColumn(forecastView, "판매구분", "SalesType");
            grid.Create_GvColumn(forecastView, "거래처 / 채널", "CustomerName");
            grid.Create_GvColumn(forecastView, "품목코드", "ItemCode");
            grid.Create_GvColumn(forecastView, "품목명", "ItemName");

            for (int week = 1; week <= 5; week++)
            {
                grid.Create_GvColumn(forecastView, week + "주", "Week" + week + "Qty");
                grid.SetCol_Summary(forecastView, "Week" + week + "Qty", format: "{0:N0}");
            }

            grid.Create_GvColumn(forecastView, "합계", "TotalQty");
            grid.Create_GvColumn(forecastView, "비고", "Remark");
            grid.SetCol_Summary(forecastView, "TotalQty", format: "{0:N0}");

            foreach (GridColumn column in forecastView.Columns)
            {
                column.OptionsColumn.AllowMerge = DefaultBoolean.False;
                column.Width = 95;
                column.MinWidth = 75;
                if (!column.FieldName.EndsWith("Qty"))
                    continue;
                grid.SetCol_Type(column, clsGrid.TypeList.TInt);
                column.AppearanceCell.BackColor = Color.FromArgb(242, 248, 245);
            }

            forecastView.Columns["SalesType"].Width = 85;
            forecastView.Columns["CustomerName"].Width = 150;
            forecastView.Columns["ItemCode"].Width = 110;
            forecastView.Columns["ItemName"].Width = 190;
            forecastView.Columns["Remark"].Width = 160;
            forecastView.Columns["CustomerName"].MinWidth = 110;
            forecastView.Columns["ItemCode"].MinWidth = 95;
            forecastView.Columns["ItemName"].MinWidth = 140;
            forecastView.Columns["Remark"].MinWidth = 100;
            forecastView.Columns["SalesType"].Fixed = FixedStyle.Left;
            forecastView.OptionsBehavior.Editable = false;
            // 드래그로 셀 범위 선택 후 Ctrl+C 복사, 엑셀에 열 구조 유지
            forecastView.OptionsSelection.MultiSelect = true;
            forecastView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            forecastView.OptionsSelection.EnableAppearanceFocusedCell = true;
            forecastView.OptionsClipboard.AllowCopy = DefaultBoolean.True;
            forecastView.OptionsClipboard.CopyColumnHeaders = DefaultBoolean.False;
            forecastView.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
            forecastView.OptionsView.AllowCellMerge = true;
            forecastView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            // 연속된 판매구분 병합 및 가운데 정렬
            var salesColumn = forecastView.Columns["SalesType"];
            salesColumn.OptionsColumn.AllowMerge = DefaultBoolean.True;
            salesColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            salesColumn.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            var customerColumn = forecastView.Columns["CustomerName"];
            customerColumn.OptionsColumn.AllowMerge = DefaultBoolean.True;
            customerColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            customerColumn.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            forecastView.CellMerge += ForecastView_CellMerge;
            forecastView.DoubleClick += ForecastView_DoubleClick;

            // 기준월 말일에 맞춰 표시할 날짜 열을 미리 생성
            for (int day = 1; day <= 31; day++)
            {
                string field = "Day" + day + "Qty";
                grid.Create_GvColumn(forecastView, day.ToString(), field);
                // 현재 조회된 품목의 날짜별 수량 합계
                grid.SetCol_Summary(forecastView, field, format: "{0:N0}");
                GridColumn column = forecastView.Columns[field];
                column.Visible = false;
                column.OptionsColumn.AllowMerge = DefaultBoolean.False;
                column.Width = 64;
                grid.SetCol_Type(column, clsGrid.TypeList.TInt);
                column.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            }

            forecastView.CustomDrawRowIndicator += grid.RowNumber_Indicator;
        }

        /// <summary>품목 행 더블클릭으로 일별 실 발주량 수정창 열기</summary>
        private void ForecastView_DoubleClick(object sender, EventArgs e)
        {
            var hit = forecastView.CalcHitInfo(forecastGrid.PointToClient(Control.MousePosition));

            if (hit.InRowCell)
                OpenQuantityEditor(forecastView.GetDataRow(hit.RowHandle), hit.Column.FieldName);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var item = forecastView.GetFocusedDataRow();

            if (item == null || item.IsNull("ItemName"))
            {
                cls_MessageBox.Show("수정할 품목 행을 선택해 주세요.", "실 발주량 수정");
                return;
            }

            OpenQuantityEditor(item, forecastView.FocusedColumn?.FieldName);
        }

        private void OpenQuantityEditor(DataRow item, string field)
        {
            DateTime month = baseMonth.DateTime;

            if (item == null || item.IsNull("ItemName") || month == DateTime.MinValue)
                return;

            string custCode = item.Field<string>("CustCode");
            string itemCode = item.Field<string>("ItemCode");
            string channel = item.Field<string>("CustomerName");
            string itemName = item.Field<string>("ItemName");
            int topRowIndex = forecastView.TopRowIndex;
            int leftCoord = forecastView.LeftCoord;

            using (var quantities = new DataTable())
            using (var dialog = new XtraForm())
            {
                quantities.Columns.Add("OrderDate", typeof(DateTime));
                quantities.Columns.Add("OrderQty", typeof(int));

                for (int day = 1; day <= DateTime.DaysInMonth(month.Year, month.Month); day++)
                    quantities.Rows.Add(new DateTime(month.Year, month.Month, day), item["Day" + day + "Qty"]);

                quantities.AcceptChanges();

                dialog.Text = $"실 발주량 수정 - {channel} / {itemName}";
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.ClientSize = new Size(540, 540);
                dialog.Font = Font;
                dialog.ShowInTaskbar = false;

                var quantityGrid = new GridControl { Dock = DockStyle.Fill };
                var view = new GridView(quantityGrid);
                quantityGrid.MainView = view;
                view.OptionsView.ShowGroupPanel = false;

                grid.Create_GvColumn(view, "발주일자", "OrderDate");
                var dateColumn = view.Columns["OrderDate"];
                grid.SetCol_Type(dateColumn, clsGrid.TypeList.TDate);
                dateColumn.DisplayFormat.FormatString = "yyyy-MM-dd (ddd)";

                var quantityEditor = new RepositoryItemSpinEdit
                {
                    IsFloatValue = false,
                    MinValue = 0,
                    MaxValue = int.MaxValue,
                    AllowNullInput = DefaultBoolean.True
                };

                quantityGrid.RepositoryItems.Add(quantityEditor);
                grid.Create_GvColumn(view, "실 발주량", "OrderQty", quantityEditor, true);
                grid.SetCol_Type(view.Columns["OrderQty"], clsGrid.TypeList.TInt);
                quantityGrid.DataSource = quantities;

                // 기존 MES 공통 버튼의 색상·아이콘·여백 적용
                var buttons = new PanelControl
                {
                    Dock = DockStyle.Top,
                    Height = 40,
                    BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                };
                buttons.Appearance.BackColor = Color.FromArgb(49, 63, 92);
                buttons.Appearance.Options.UseBackColor = true;
                var save = new baseSMT.smtSearchButton
                {
                    Name = "cmdSave",
                    smtButtonStyle = baseSMT.EnumButtonStyles.저장,
                    AllowFocus = false,
                    AutoSize = true,
                    Dock = DockStyle.Right,
                    Padding = new Padding(12, 0, 12, 0)
                };
                var close = new baseSMT.smtSearchButton
                {
                    Name = "cmdClose",
                    smtButtonStyle = baseSMT.EnumButtonStyles.닫기,
                    AllowFocus = false,
                    AutoSize = true,
                    Dock = DockStyle.Right,
                    Padding = new Padding(12, 0, 12, 0),
                    DialogResult = DialogResult.Cancel
                };
                foreach (var button in new[] { save, close })
                {
                    button.ImageOptions.AllowGlyphSkinning = DefaultBoolean.True;
                    button.ImageOptions.ImageToTextIndent = 15;
                    button.ImageOptions.SvgImageSize = new Size(21, 21);
                }
                buttons.Controls.Add(save);
                buttons.Controls.Add(close);
                dialog.Controls.Add(quantityGrid);
                dialog.Controls.Add(buttons);
                dialog.CancelButton = close;

                if (field != null && field.StartsWith("Day") && int.TryParse(field.Substring(3, field.Length - 6), out int selectedDay))
                {
                    dialog.Shown += (shownSender, args) =>
                    {
                        view.FocusedRowHandle = selectedDay - 1;
                        view.FocusedColumn = view.Columns["OrderQty"];
                    };
                }

                save.Click += (saveSender, args) =>
                {
                    if (!view.PostEditor() || !view.UpdateCurrentRow())
                        return;

                    try
                    {
                        PM_Forecasting.SaveActualQuantities(custCode, itemCode, channel, itemName, quantities);
                        dialog.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        cls_MessageBox.Show(ex.Message, "실 발주량 저장", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
            }

            DisplayData();
            RestoreEditedCell(custCode, itemCode, field, topRowIndex, leftCoord);
        }

        /// <summary>EDI 반영 후 현재 조회 조건과 스크롤 위치를 유지한 자동 갱신</summary>
        internal async Task RefreshEdiQuantitiesAsync()
        {
            int version = ++refreshVersion;
            DateTime month = baseMonth.DateTime;
            string salesType = salesFilter.EditValue as string ?? "전체";
            string keyword = itemSearch.Text.Trim();

            // DB 조회·행 구성만 작업 스레드에서 실행, 컨트롤 접근은 UI 스레드에서 처리
            DataTable rows = await Task.Run(() => GetForecastRows(salesType, keyword, month));
            if (IsDisposed || Disposing || version != refreshVersion
                || month != baseMonth.DateTime || salesType != (salesFilter.EditValue as string ?? "전체")
                || keyword != itemSearch.Text.Trim())
            {
                rows.Dispose();
                return;
            }

            DataRow row = forecastView.GetFocusedDataRow();
            string custCode = row == null ? null : Convert.ToString(row["CustCode"]);
            string itemCode = row == null ? null : Convert.ToString(row["ItemCode"]);
            string field = forecastView.FocusedColumn?.FieldName;
            int topRowIndex = forecastView.TopRowIndex;
            int leftCoord = forecastView.LeftCoord;
            BindForecastRows(rows);
            RestoreEditedCell(custCode, itemCode, field, topRowIndex, leftCoord, false);
        }

        /// <summary>저장 후 수정한 품목과 열의 포커스 복원</summary>
        private void RestoreEditedCell(string custCode, string itemCode, string field, int topRowIndex, int leftCoord, bool focus = true)
        {
            for (int handle = 0; handle < forecastView.DataRowCount; handle++)
            {
                DataRow row = forecastView.GetDataRow(handle);

                if (row == null || row.Field<string>("CustCode") != custCode
                    || row.Field<string>("ItemCode") != itemCode)
                    continue;

                forecastView.FocusedRowHandle = handle;

                var column = field == null ? null : forecastView.Columns[field];

                if (column?.Visible == true)
                    forecastView.FocusedColumn = column;

                if (focus)
                    forecastGrid.Focus();
                forecastView.TopRowIndex = topRowIndex;
                forecastView.LeftCoord = leftCoord;
                forecastView.MakeRowVisible(handle);
                return;
            }
        }

        /// <summary>판매구분과 거래처명이 같은 인접 행의 거래처 셀만 합칩니다.</summary>
        private void ForecastView_CellMerge(object sender, CellMergeEventArgs e)
        {
            if (e.Column.FieldName != "CustomerName")
                return;

            var first = forecastView.GetDataRow(e.RowHandle1);
            var second = forecastView.GetDataRow(e.RowHandle2);
            e.Merge = first != null && second != null && !string.IsNullOrWhiteSpace(Convert.ToString(first["CustomerName"])) && Equals(first["CustCode"], second["CustCode"]) && Equals(first["SalesType"], second["SalesType"]);
            e.Handled = true;
        }

        /// <summary>실 발주량을 품목별 그리드 행으로 구성</summary>
        private DataTable GetForecastRows(string salesType, string keyword, DateTime selectedMonth)
        {
            var rows = new DataTable();

            foreach (string field in new[] { "SalesType", "CustomerName", "ItemCode", "ItemName" })
            {
                rows.Columns.Add(field, typeof(string));
            }

            for (int week = 1; week <= 5; week++)
            {
                rows.Columns.Add("Week" + week + "Qty", typeof(int));
            }

            rows.Columns.Add("CustCode", typeof(string));
            rows.Columns.Add("TotalQty", typeof(int));
            rows.Columns.Add("Remark", typeof(string));

            for (int day = 1; day <= 31; day++)
                rows.Columns.Add("Day" + day + "Qty", typeof(int));


            if (selectedMonth == DateTime.MinValue)
                selectedMonth = DateTime.Today;

            using (var customers = PM_Forecasting.SelectActualQuantities(selectedMonth))
            {
                if (customers != null)
                {
                    var items = customers.AsEnumerable().Where(row => salesType == "전체" || row.Field<string>("CustType") == salesType)
                        .Where(row => keyword.Length == 0 || (row.Field<string>("ItemCode") ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 
                        || (row.Field<string>("ItemName") ?? "").IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        .GroupBy(row => new
                        {
                            CustomerCode = row.Field<string>("CustCode"),
                            SalesType = row.Field<string>("CustType"),
                            Channel = row.Field<string>("ChannelName"),
                            ItemCode = row.Field<string>("ItemCode"),
                            ItemName = row.Field<string>("ItemName")
                        });

                    foreach (var item in items)
                    {
                        DataRow row = rows.Rows.Add(item.Key.SalesType, item.Key.Channel, item.Key.ItemCode, item.Key.ItemName);

                        row["CustCode"] = item.Key.CustomerCode;

                        int? monthQty = null;

                        foreach (DataRow actual in item.Where(record => !record.IsNull("OrderDay")))
                        {
                            int day = actual.Field<int>("OrderDay");
                            int qty = actual.Field<int>("OrderQty");
                            row["Day" + day + "Qty"] = qty;

                            // 매월 1~7일을 1주차로 집계
                            string weekField = "Week" + ((day - 1) / 7 + 1) + "Qty";
                            row[weekField] = (row.Field<int?>(weekField) ?? 0) + qty;
                            monthQty = (monthQty ?? 0) + qty;
                        }

                        if (monthQty.HasValue)
                            row["TotalQty"] = monthQty.Value;
                    }
                }
            }

            return rows;
        }

        /// <summary>관리단위별 수량 열 표시</summary>
        private void ApplyPeriodMode()
        {
            if (forecastView.Columns["Day1Qty"] == null)
                return;

            bool showCalendar = Convert.ToString(periodMode.EditValue) == "일별";

            forecastView.BeginUpdate();
            try
            {
                forecastView.OptionsView.ColumnAutoWidth = !showCalendar;
                forecastView.ColumnPanelRowHeight = showCalendar ? 52 : 36;
                int index = 0;
                foreach (string field in new[] { "SalesType", "CustomerName", "ItemCode", "ItemName" })
                {
                    var column = forecastView.Columns[field];
                    column.Fixed = showCalendar || field == "SalesType" ? FixedStyle.Left : FixedStyle.None;
                    column.VisibleIndex = index++;
                }

                // 1주차부터 순서대로 표시
                for (int week = 1; week <= 5; week++)
                    forecastView.Columns["Week" + week + "Qty"].VisibleIndex = showCalendar ? -1 : index++;

                var totalColumn = forecastView.Columns["TotalQty"];
                totalColumn.Fixed = showCalendar ? FixedStyle.Right : FixedStyle.None;
                totalColumn.VisibleIndex = showCalendar ? -1 : index++;

                DateTime month = baseMonth.DateTime;
                int days = showCalendar && month != DateTime.MinValue ? DateTime.DaysInMonth(month.Year, month.Month) : 0;
                var calendar = CultureInfo.GetCultureInfo("ko-KR").DateTimeFormat;
                for (int day = 1; day <= 31; day++)
                {
                    var column = forecastView.Columns["Day" + day + "Qty"];
                    if (day > days)
                    {
                        column.VisibleIndex = -1;
                        continue;
                    }

                    var date = new DateTime(month.Year, month.Month, day);
                    column.Caption = day + "\n" + calendar.GetAbbreviatedDayName(date.DayOfWeek);
                    column.AppearanceHeader.ForeColor = date.DayOfWeek == DayOfWeek.Sunday ? Color.Firebrick : Color.Empty;
                    column.AppearanceHeader.Options.UseForeColor = date.DayOfWeek == DayOfWeek.Sunday;
                    column.VisibleIndex = index++;
                }
                forecastView.Columns["Remark"].VisibleIndex = index++;
                if (showCalendar)
                    totalColumn.VisibleIndex = index;
            }
            finally
            {
                forecastView.EndUpdate();
            }
            forecastView.LeftCoord = 0;
        }

        private void PeriodMode_EditValueChanged(object sender, EventArgs e)
        {
            ApplyPeriodMode();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        /// <summary>조회 결과 표시 및 실 발주량 집계</summary>
        private void DisplayData()
        {
            string salesType = salesFilter.EditValue as string ?? "전체";
            string keyword = itemSearch.Text.Trim();
            ++refreshVersion;
            DataTable rows = GetForecastRows(salesType, keyword, baseMonth.DateTime);
            BindForecastRows(rows);
        }

        private void BindForecastRows(DataTable rows)
        {
            forecastView.ActiveFilterCriteria = null;
            forecastGrid.DataSource = rows;
            forecastView.OptionsView.ShowFooter = true;

            int b2b = 0;
            int b2c = 0;
            int total = 0;
            foreach (DataRow row in rows.Rows)
            {
                int quantity = row.Field<int?>("TotalQty") ?? 0;
                total += quantity;
                if (Equals(row["SalesType"], "B2B"))
                    b2b += quantity;
                else if (Equals(row["SalesType"], "B2C"))
                    b2c += quantity;
            }
            // 하단 합계 문구와 숫자가 잘리지 않도록 최소 너비 계산
            var totalColumn = forecastView.Columns["TotalQty"];
            string footerText = string.Format(totalColumn.SummaryItem.DisplayFormat, total);
            int footerWidth = TextRenderer.MeasureText(footerText, forecastView.Appearance.FooterPanel.Font).Width;
            totalColumn.MinWidth = Math.Max(95, footerWidth + 28 * forecastGrid.DeviceDpi / 96);
            totalColumn.Width = Math.Max(totalColumn.Width, totalColumn.MinWidth);

            // 일별·주별 합계 자릿수와 화면 배율에 맞춰 최소 너비 조정
            foreach (GridColumn dayColumn in forecastView.Columns)
            {
                if (!dayColumn.FieldName.StartsWith("Day") && !dayColumn.FieldName.StartsWith("Week"))
                    continue;

                long dayTotal = rows.AsEnumerable().Sum(row => (long)(row.Field<int?>(dayColumn.FieldName) ?? 0));
                string dayFooterText = string.Format(dayColumn.SummaryItem.DisplayFormat, dayTotal);
                int dayFooterWidth = TextRenderer.MeasureText(dayFooterText, forecastView.Appearance.FooterPanel.Font).Width;
                dayColumn.MinWidth = Math.Max(64, dayFooterWidth + 36 * forecastGrid.DeviceDpi / 96);
                dayColumn.Width = Math.Max(dayColumn.Width, dayColumn.MinWidth);
            }

            listGroup.Text = $"실 발주량  ·  {rows.Rows.Count:N0}개 항목";
            cardValue1.Text = $"{total:N0} EA";
            cardValue2.Text = $"{b2b:N0} EA";
            cardValue3.Text = $"{b2c:N0} EA";
        }

        /// <summary>기준월 변경 시 날짜 열과 발주량 갱신</summary>
        private void BaseMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(periodMode.EditValue) == "일별")
                ApplyPeriodMode();
            DisplayData();
        }

        private void ExcelDownloadButton_Click(object sender, EventArgs e)
        {
            if (forecastView.DataRowCount == 0)
            {
                cls_MessageBox.Show("조회된 자료가 없습니다.", "엑셀 다운로드");
                return;
            }

            var excel = new clsExcelDown
            {
                DownGridControl = forecastGrid,
                FormName = $"포케스팅_실발주량_{baseMonth.DateTime:yyyyMM}"
            };
            excel.Btn_ExcelDown();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
