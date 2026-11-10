using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;

namespace SmartMES.View.PM
{
    /// <summary>
    /// 포케스팅 관리
    /// </summary>
    public partial class Frm_PM_Forecasting : DevExpress.XtraEditors.XtraForm
    {
        public Frm_PM_Forecasting()
        {
            InitializeComponent();
            searchButton.Click += SearchButton_Click;
            uploadButton.Click += UploadButton_Click;
            // 체크한 품목의 일별 재고를 별도 창에서 확인할 수 있게 합니다.
            var stockButton = new baseSMT.smtSearchButton
            {
                Name = "stockPreviewButton",
                smtButtonStyle = baseSMT.EnumButtonStyles.조회,
                smtButtonAutoText = false,
                Text = "재고 보기",
                Dock = DockStyle.Right
            };
            stockButton.Click += StockPreviewButton_Click;
            commonBar.Controls.Add(stockButton);
            commonBar.Controls.SetChildIndex(stockButton, commonBar.Controls.GetChildIndex(searchButton) + 1);

            new SmartCommon.Common.clsGrid().Set_Navigator(forecastView);

            InitializeForecastGrid();
            ShowDocumentPreview();
            ApplyPeriodMode();
            baseMonth.EditValueChanged += BaseMonth_EditValueChanged;
        }

        /// <summary>기존 MES 공통 그리드 방식으로 판매 예상수량 열을 구성합니다.</summary>
        private void InitializeForecastGrid()
        {
            var grid = new clsGrid();
            // 셀 병합과 함께 품목을 체크할 수 있도록 체크 상태를 행 데이터에 기록합니다.
            var checkEditor = new RepositoryItemCheckEdit();
            forecastGrid.RepositoryItems.Add(checkEditor);
            grid.Create_GvColumn(forecastView, "선택", "IsChecked", checkEditor, true);
            checkEditor.EditValueChanged += (sender, e) => forecastView.PostEditor();
            grid.Create_GvColumn(forecastView, "판매구분", "SalesType");
            grid.Create_GvColumn(forecastView, "거래처 / 채널", "CustomerName");
            grid.Create_GvColumn(forecastView, "품목코드", "ItemCode");
            grid.Create_GvColumn(forecastView, "품목명", "ItemName");

            for (int week = 1; week <= 5; week++)
                grid.Create_GvColumn(forecastView, week + "주", "Week" + week + "Qty");

            grid.Create_GvColumn(forecastView, "월 예상수량", "MonthQty");
            grid.Create_GvColumn(forecastView, "합계", "TotalQty");
            grid.Create_GvColumn(forecastView, "비고", "Remark");

            foreach (GridColumn column in forecastView.Columns)
            {
                column.OptionsColumn.AllowMerge = DefaultBoolean.False;
                column.Width = 95;
                column.MinWidth = 75;
                if (!column.FieldName.EndsWith("Qty"))
                    continue;
                column.DisplayFormat.FormatType = FormatType.Numeric;
                column.DisplayFormat.FormatString = "#,##0";
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
            var checkColumn = forecastView.Columns["IsChecked"];
            checkColumn.MinWidth = 48;
            checkColumn.Width = 48;
            checkColumn.Fixed = FixedStyle.Left;
            checkColumn.OptionsColumn.FixedWidth = true;
            checkColumn.OptionsColumn.AllowSort = DefaultBoolean.False;
            forecastView.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDown;
            forecastView.OptionsView.AllowCellMerge = true;
            var customerColumn = forecastView.Columns["CustomerName"];
            customerColumn.OptionsColumn.AllowMerge = DefaultBoolean.True;
            customerColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            customerColumn.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            forecastView.CellMerge += ForecastView_CellMerge;
            forecastView.MouseDown += ForecastView_MouseDown;

            // 날짜 열을 미리 만들고 기준월의 말일에 맞춰 필요한 열만 표시합니다.
            for (int day = 1; day <= 31; day++)
            {
                string field = "Day" + day + "Qty";
                grid.Create_GvColumn(forecastView, day.ToString(), field);
                GridColumn column = forecastView.Columns[field];
                column.Visible = false;
                column.OptionsColumn.AllowMerge = DefaultBoolean.False;
                column.Width = 64;
                column.DisplayFormat.FormatType = FormatType.Numeric;
                column.DisplayFormat.FormatString = "#,##0";
                column.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            }

            forecastView.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            InitializeForecastTotals();
        }

        /// <summary>품목 검색 등 현재 필터를 통과한 행의 수량을 하단에 합산합니다.</summary>
        private void InitializeForecastTotals()
        {
            forecastView.OptionsView.ShowFooter = true;
            forecastView.FooterPanelHeight = 36;
            forecastView.Appearance.FooterPanel.BackColor = Color.FromArgb(233, 244, 238);
            forecastView.Appearance.FooterPanel.ForeColor = Color.FromArgb(35, 70, 55);
            forecastView.Appearance.FooterPanel.Options.UseBackColor = true;
            forecastView.Appearance.FooterPanel.Options.UseForeColor = true;

            // 기본 합계 기능은 필터 변경과 데이터 수정 시 자동으로 다시 계산됩니다.
            foreach (GridColumn column in forecastView.Columns)
            {
                if (!column.FieldName.EndsWith("Qty"))
                    continue;

                column.Summary.Clear();
                column.Summary.Add(SummaryItemType.Sum, column.FieldName, "{0:#,##0}");
            }

            var labelColumn = forecastView.Columns["ItemName"];
            labelColumn.Summary.Clear();
            labelColumn.Summary.Add(SummaryItemType.Count, "ItemName", "총합계");
        }

        /// <summary>판매구분과 거래처명이 같은 인접 행의 거래처 셀만 합칩니다.</summary>
        private void ForecastView_CellMerge(object sender, CellMergeEventArgs e)
        {
            if (e.Column.FieldName != "CustomerName")
                return;

            var first = forecastView.GetDataRow(e.RowHandle1);
            var second = forecastView.GetDataRow(e.RowHandle2);
            e.Merge = first != null && second != null
                && !string.IsNullOrWhiteSpace(Convert.ToString(first["CustomerName"]))
                && Equals(first["CustomerName"], second["CustomerName"])
                && Equals(first["SalesType"], second["SalesType"]);
            e.Handled = true;
        }

        /// <summary>선택 열 머리글을 누르면 표시된 품목의 체크 상태를 한 번에 바꿉니다.</summary>
        private void ForecastView_MouseDown(object sender, MouseEventArgs e)
        {
            var hit = forecastView.CalcHitInfo(e.Location);
            if (e.Button != MouseButtons.Left || !hit.InColumn || hit.Column?.FieldName != "IsChecked")
                return;

            var rows = new List<DataRow>();
            for (int index = 0; index < forecastView.DataRowCount; index++)
            {
                var row = forecastView.GetDataRow(index);
                if (row != null)
                    rows.Add(row);
            }

            bool check = rows.Exists(row => !Equals(row["IsChecked"], true));
            foreach (var row in rows)
                row["IsChecked"] = check;
        }

        /// <summary>워드의 네 행을 화면 확인용으로 표시합니다. 실제 업무 데이터가 아닙니다.</summary>
        private void ShowDocumentPreview()
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

            rows.Columns.Add("MonthQty", typeof(int));
            rows.Columns.Add("TotalQty", typeof(int));
            rows.Columns.Add("Remark", typeof(string));

            for (int day = 1; day <= 31; day++)
                rows.Columns.Add("Day" + day + "Qty", typeof(int));
            rows.Columns.Add("IsChecked", typeof(bool)).DefaultValue = false;

            rows.Rows.Add("B2B", "거래처A", "YG001", "플레인 그릭요거트 100g", 1200, 1200, 1200, 1200, 0, 4800, 4800, "정기납품");
            rows.Rows.Add("B2B", "거래처B", "YG002", "플레인500g", 300, 300, 300, 300, 0, 1200, 1200, "정기납품");
            rows.Rows.Add("B2C", "온라인몰", "YG001", "플레인 그릭요거트 100g", 800, 800, 900, 900, 0, 3400, 3400, "온라인판매");
            rows.Rows.Add("B2C", "온라인몰", "YG003", "딸기100g", 500, 500, 600, 600, 0, 2200, 2200, "행사반영");

            forecastGrid.DataSource = rows;

        }

        /// <summary>관리단위에 맞는 수량 열을 표시합니다.</summary>
        private void ApplyPeriodMode()
        {
            if (forecastView.Columns["Day1Qty"] == null)
                return;

            string mode = Convert.ToString(periodMode.EditValue);
            bool weekly = mode == "주별";
            bool monthly = mode == "월별";
            bool showCalendar = mode == "일별";

            forecastView.BeginUpdate();
            try
            {
                forecastView.OptionsView.ColumnAutoWidth = !showCalendar;
                forecastView.ColumnPanelRowHeight = showCalendar ? 52 : 36;
                int index = 1;
                foreach (string field in new[] { "SalesType", "CustomerName", "ItemCode", "ItemName" })
                {
                    var column = forecastView.Columns[field];
                    column.Fixed = showCalendar || field == "SalesType" ? FixedStyle.Left : FixedStyle.None;
                    column.VisibleIndex = index++;
                }

                // 주별은 1주차부터 순서대로 표시합니다.
                for (int week = 1; week <= 5; week++)
                    forecastView.Columns["Week" + week + "Qty"].VisibleIndex = weekly ? index++ : -1;

                forecastView.Columns["MonthQty"].VisibleIndex = monthly ? index++ : -1;
                forecastView.Columns["TotalQty"].VisibleIndex = weekly ? index++ : -1;

                DateTime month = baseMonth.DateTime;
                int days = showCalendar && month != DateTime.MinValue ? DateTime.DaysInMonth(month.Year, month.Month) : 0;
                var calendar = CultureInfo.GetCultureInfo("ko-KR").DateTimeFormat;
                for (int day = 31; day >= 1; day--)
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
                forecastView.Columns["Remark"].VisibleIndex = index;
                forecastView.Columns["IsChecked"].VisibleIndex = 0;
            }
            finally
            {
                forecastView.EndUpdate();
            }
            forecastView.LeftCoord = 0;
            if (showCalendar)
                ScrollToToday();
        }

        /// <summary>이번 달 일별 화면에서 오늘 날짜 열을 스크롤 영역의 가운데에 맞춥니다.</summary>
        private void ScrollToToday()
        {
            DateTime today = DateTime.Today;
            DateTime month = baseMonth.DateTime;
            if (month.Year != today.Year || month.Month != today.Month)
                return;

            GridColumn todayColumn = forecastView.Columns["Day" + today.Day + "Qty"];
            int fixedWidth = 0;
            if (forecastView.OptionsView.ShowIndicator)
                fixedWidth += forecastView.IndicatorWidth;

            int offset = 0;
            foreach (GridColumn column in forecastView.VisibleColumns)
            {
                if (column.Fixed != FixedStyle.None)
                    fixedWidth += column.Width;
                else if (column.VisibleIndex < todayColumn.VisibleIndex)
                    offset += column.Width;
            }

            int availableWidth = forecastGrid.ClientSize.Width - fixedWidth;
            forecastView.LeftCoord = Math.Max(0, offset - Math.Max(0, availableWidth - todayColumn.Width) / 2);
        }

        private void PeriodMode_EditValueChanged(object sender, EventArgs e)
        {
            ApplyPeriodMode();
        }

        /// <summary>예시 목록을 판매구분과 품목으로 조회하고 표시 중인 수량을 집계합니다.</summary>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            forecastView.PostEditor();
            var filters = new List<CriteriaOperator>();
            string salesType = Convert.ToString(salesFilter.EditValue);
            if (salesType == "B2B" || salesType == "B2C")
                filters.Add(new BinaryOperator("SalesType", salesType));

            string keyword = itemSearch.Text.Trim();
            if (keyword.Length > 0)
            {
                filters.Add(new GroupOperator(GroupOperatorType.Or,
                    new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty("ItemCode"), new OperandValue(keyword)),
                    new FunctionOperator(FunctionOperatorType.Contains, new OperandProperty("ItemName"), new OperandValue(keyword))));
            }

            forecastView.ActiveFilterCriteria = filters.Count == 0
                ? null
                : new GroupOperator(GroupOperatorType.And, filters);

            int b2b = 0;
            int b2c = 0;
            for (int handle = 0; handle < forecastView.DataRowCount; handle++)
            {
                DataRow row = forecastView.GetDataRow(handle);
                int quantity = Convert.ToInt32(row["TotalQty"]);
                if (Equals(row["SalesType"], "B2B"))
                    b2b += quantity;
                else if (Equals(row["SalesType"], "B2C"))
                    b2c += quantity;
            }
            listGroup.Text = $"판매 예상수량  ·  {forecastView.DataRowCount:N0}개 항목";
            cardValue1.Text = $"{b2b + b2c:N0} EA";
            cardValue2.Text = $"{b2b:N0} EA";
            cardValue3.Text = $"{b2c:N0} EA";
        }

        /// <summary>기준월이 바뀌면 달력에 표시할 날짜 수와 요일을 다시 계산합니다.</summary>
        private void BaseMonth_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(periodMode.EditValue) == "일별")
                ApplyPeriodMode();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>선택한 품목의 기초재고·입고·출고·기말재고를 일별로 표시합니다.</summary>
        private void StockPreviewButton_Click(object sender, EventArgs e)
        {
            DateTime month = baseMonth.DateTime;
            if (month == DateTime.MinValue)
            {
                XtraMessageBox.Show(this, "기준월을 선택해 주세요.", "재고 보기");
                return;
            }

            int days = DateTime.DaysInMonth(month.Year, month.Month);
            using (var rows = CreateStockPreviewRows(month, days))
            {
                if (rows.Rows.Count == 0)
                {
                    XtraMessageBox.Show(this, "재고를 볼 품목을 체크해 주세요.", "재고 보기");
                    return;
                }

                using (var preview = new XtraForm
                {
                    Text = $"3PL 재고 관리 ({month:yyyy년 MM월})",
                    StartPosition = FormStartPosition.CenterParent,
                    ClientSize = new Size(1100, 580),
                    MinimumSize = new Size(780, 380),
                    Font = Font,
                    ShowInTaskbar = false
                })
                {
                    var stockGrid = new GridControl { Dock = DockStyle.Fill };
                    var view = new GridView(stockGrid);
                    stockGrid.MainView = view;
                    view.OptionsBehavior.Editable = false;
                    view.OptionsBehavior.AutoPopulateColumns = false;
                    view.OptionsCustomization.AllowSort = false;
                    view.OptionsCustomization.AllowGroup = false;
                    view.OptionsView.ColumnAutoWidth = false;
                    view.OptionsView.ShowGroupPanel = false;
                    view.OptionsView.ShowIndicator = false;
                    view.OptionsView.AllowCellMerge = true;
                    view.RowHeight = 28;
                    view.ColumnPanelRowHeight = 52;
                    view.OptionsSelection.EnableAppearanceFocusedCell = false;
                    view.OptionsSelection.EnableAppearanceFocusedRow = false;
                    view.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

                    var grid = new clsGrid();
                    // 품목명은 네 줄에 걸쳐 표시되므로 긴 이름을 셀 안에서 줄바꿈합니다.
                    var itemNameEditor = new RepositoryItemMemoEdit { WordWrap = true };
                    itemNameEditor.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                    itemNameEditor.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                    stockGrid.RepositoryItems.Add(itemNameEditor);
                    grid.Create_GvColumn(view, "품목명", "ItemName", itemNameEditor);
                    grid.Create_GvColumn(view, "재고 구분", "StockType");
                    var itemNameColumn = view.Columns["ItemName"];
                    itemNameColumn.Width = 135;
                    itemNameColumn.Fixed = FixedStyle.Left;
                    itemNameColumn.OptionsColumn.AllowMerge = DefaultBoolean.True;
                    itemNameColumn.AppearanceCell.BackColor = Color.FromArgb(242, 242, 242);
                    itemNameColumn.AppearanceCell.Options.UseBackColor = true;
                    itemNameColumn.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
                    itemNameColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    itemNameColumn.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
                    view.Columns["StockType"].Width = 95;
                    view.Columns["StockType"].Fixed = FixedStyle.Left;
                    view.Columns["StockType"].OptionsColumn.AllowMerge = DefaultBoolean.False;
                    // 이름이 같은 다른 품목이 섞이지 않도록 품목코드 기준으로 품목명 셀을 묶습니다.
                    view.CellMerge += (s, args) =>
                    {
                        if (args.Column.FieldName != "ItemName")
                            return;
                        var first = view.GetDataRow(args.RowHandle1);
                        var second = view.GetDataRow(args.RowHandle2);
                        args.Merge = first != null && second != null && Equals(first["ItemCode"], second["ItemCode"]);
                        args.Handled = true;
                    };

                    var calendar = CultureInfo.GetCultureInfo("ko-KR").DateTimeFormat;
                    for (int day = days; day >= 1; day--)
                    {
                        var date = new DateTime(month.Year, month.Month, day);
                        string field = "Day" + day + "Qty";
                        grid.Create_GvColumn(view, day + "\n" + calendar.GetAbbreviatedDayName(date.DayOfWeek), field);
                        GridColumn column = view.Columns[field];
                        column.Width = 75;
                        column.OptionsColumn.AllowMerge = DefaultBoolean.False;
                        column.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                        column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        if (date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            column.AppearanceHeader.ForeColor = Color.Firebrick;
                            column.AppearanceHeader.Options.UseForeColor = true;
                        }
                        column.DisplayFormat.FormatType = FormatType.Numeric;
                        column.DisplayFormat.FormatString = "#,##0";
                    }

                    view.RowCellStyle += (s, args) =>
                    {
                        if (args.RowHandle >= 0 && args.Column.FieldName != "ItemName"
                            && Equals(view.GetRowCellValue(args.RowHandle, "StockType"), "입고"))
                            args.Appearance.BackColor = Color.FromArgb(221, 241, 249);
                    };
                    stockGrid.DataSource = rows;
                    var notice = new LabelControl
                    {
                        Dock = DockStyle.Top,
                        AutoSizeMode = LabelAutoSizeMode.None,
                        Height = 28,
                        Padding = new Padding(10, 0, 0, 0),
                        Text = $"예시 수량   |   선택 품목 {rows.Rows.Count / 4}개"
                    };
                    var title = new LabelControl
                    {
                        Dock = DockStyle.Top,
                        AutoSizeMode = LabelAutoSizeMode.None,
                        Height = 40,
                        Padding = new Padding(10, 0, 0, 0),
                        Text = $"3PL 재고 관리 ({month:MM}월)"
                    };
                    title.Appearance.Font = new Font(Font.FontFamily, 14F, FontStyle.Bold);
                    preview.Controls.Add(stockGrid);
                    preview.Controls.Add(notice);
                    preview.Controls.Add(title);
                    preview.ShowDialog(this);
                }
            }
        }

        /// <summary>선택한 품목코드마다 재고 미리보기용 예시 행 네 개를 만듭니다.</summary>
        private DataTable CreateStockPreviewRows(DateTime month, int days)
        {
            var rows = new DataTable();
            rows.Columns.Add("ItemCode", typeof(string));
            rows.Columns.Add("ItemName", typeof(string));
            rows.Columns.Add("StockType", typeof(string));
            for (int day = 1; day <= days; day++)
                rows.Columns.Add("Day" + day + "Qty", typeof(int));

            var codes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            forecastView.PostEditor();
            var forecastRows = (DataTable)forecastGrid.DataSource;
            foreach (DataRow selected in forecastRows.Rows)
            {
                if (!Equals(selected["IsChecked"], true))
                    continue;
                string code = Convert.ToString(selected["ItemCode"]);
                if (string.IsNullOrWhiteSpace(code) || !codes.Add(code))
                    continue;

                string name = Convert.ToString(selected["ItemName"]);
                // 예시 품목명은 엑셀처럼 제품 계열과 상세 이름을 두 줄로 표시합니다.
                if (name == "플레인 그릭요거트 100g")
                    name = "플레인\r\n그릭요거트 100g";
                DataRow opening = rows.Rows.Add(code, name, "기초재고");
                DataRow incoming = rows.Rows.Add(code, name, "입고");
                DataRow outgoing = rows.Rows.Add(code, name, "출고");
                DataRow closing = rows.Rows.Add(code, name, "기말재고");
                // 전날 기말재고를 다음 날 기초재고로 넘기는 흐름을 예시 수량으로 보여줍니다.
                int balance = 1000;
                for (int day = 1; day <= days; day++)
                {
                    string field = "Day" + day + "Qty";
                    int received = new DateTime(month.Year, month.Month, day).DayOfWeek == DayOfWeek.Sunday ? 0 : 120;
                    const int shipped = 80;
                    opening[field] = balance;
                    incoming[field] = received;
                    outgoing[field] = shipped;
                    balance += received - shipped;
                    closing[field] = balance;
                }
            }
            return rows;
        }

        /// <summary>예상 발주 엑셀의 원본 항목과 MES 매핑 결과를 비교하는 예시 화면을 엽니다.</summary>
        private void UploadButton_Click(object sender, EventArgs e)
        {
            using (var preview = new XtraForm())
            using (var rows = CreateUploadPreviewRows())
            {
                preview.Text = "예상 발주 엑셀 업로드 미리보기";
                preview.StartPosition = FormStartPosition.CenterParent;
                preview.ClientSize = new Size(1100, 540);
                preview.MinimumSize = new Size(900, 420);
                preview.Font = Font;
                preview.ShowInTaskbar = false;
                preview.MinimizeBox = false;

                var layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(12),
                    ColumnCount = 1,
                    RowCount = 5
                };
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));

                layout.Controls.Add(new LabelControl
                {
                    Dock = DockStyle.Fill,
                    Text = $"화면 예시   |   기준월 {baseMonth.DateTime:yyyy년 MM월}   |   {periodMode.Text}"
                }, 0, 0);

                var fileBar = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
                fileBar.Controls.Add(new SimpleButton { Text = "파일 선택", Enabled = false, Size = new Size(92, 30) });
                fileBar.Controls.Add(new TextEdit
                {
                    Text = "2026년 3PL Forecast _예상 발주량.xlsx",
                    ReadOnly = true,
                    Width = 360,
                    TabStop = false
                });
                layout.Controls.Add(fileBar, 0, 1);
                layout.Controls.Add(new LabelControl
                {
                    Dock = DockStyle.Fill,
                    Text = "요약 시트 · 채널별 상세 기준   |   전체 4건   |   매핑 완료 2건   |   규격 확인 1건   |   미매핑 1건"
                }, 0, 2);

                var previewGrid = new GridControl { Dock = DockStyle.Fill };
                var view = new GridView(previewGrid);
                previewGrid.MainView = view;
                view.OptionsBehavior.Editable = false;
                view.OptionsView.ShowGroupPanel = false;
                view.OptionsView.ShowAutoFilterRow = true;
                view.RowHeight = 32;
                view.ColumnPanelRowHeight = 32;
                var grid = new clsGrid();
                grid.Create_GvColumn(view, "상태", "Status");
                grid.Create_GvColumn(view, "엑셀 행", "SourceRow");
                grid.Create_GvColumn(view, "원본 채널", "SourceChannel");
                grid.Create_GvColumn(view, "원본 품명", "SourceItem");
                grid.Create_GvColumn(view, "채널코드", "ChannelCode");
                grid.Create_GvColumn(view, "MES 품목코드", "ItemCode");
                grid.Create_GvColumn(view, "MES 품명", "ItemName");
                grid.Create_GvColumn(view, "예상수량", "Quantity");
                grid.Create_GvColumn(view, "확인 사항", "Remark");
                view.Columns["SourceRow"].Width = 60;
                view.Columns["SourceChannel"].Width = 130;
                view.Columns["SourceItem"].Width = 160;
                view.Columns["ItemCode"].Width = 120;
                view.Columns["ItemName"].Width = 160;
                view.Columns["Remark"].Width = 210;
                view.Columns["Quantity"].DisplayFormat.FormatType = FormatType.Numeric;
                view.Columns["Quantity"].DisplayFormat.FormatString = "#,##0";
                previewGrid.DataSource = rows;
                layout.Controls.Add(previewGrid, 0, 3);

                var actions = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    Padding = new Padding(0, 5, 0, 0)
                };
                var close = new SimpleButton { Text = "닫기", DialogResult = DialogResult.Cancel, Size = new Size(90, 30) };
                actions.Controls.Add(close);
                actions.Controls.Add(new SimpleButton { Text = "화면에 반영", Enabled = false, Size = new Size(110, 30) });
                layout.Controls.Add(actions, 0, 4);
                preview.Controls.Add(layout);
                preview.CancelButton = close;
                preview.ShowDialog(this);
            }
        }

        /// <summary>매핑 완료, 규격 확인, 미매핑 상태를 확인할 수 있는 예시 행을 구성합니다.</summary>
        private static DataTable CreateUploadPreviewRows()
        {
            var rows = new DataTable();
            rows.Columns.Add("Status", typeof(string));
            rows.Columns.Add("SourceRow", typeof(int));
            rows.Columns.Add("SourceChannel", typeof(string));
            rows.Columns.Add("SourceItem", typeof(string));
            rows.Columns.Add("ChannelCode", typeof(string));
            rows.Columns.Add("ItemCode", typeof(string));
            rows.Columns.Add("ItemName", typeof(string));
            rows.Columns.Add("Quantity", typeof(int));
            rows.Columns.Add("Remark", typeof(string));
            rows.Rows.Add("매핑 완료", 57, "롯데슈퍼", "블루베리 100g", "예시01", "1FBBA100G0102", "블루베리 100g", 480, "기존 제품 연결 예시");
            rows.Rows.Add("매핑 완료", 58, "롯데슈퍼", "마시는 그릭요거트 750ml", "예시01", "1DRPA750M0101", "마시는그릭 750ml", 240, "품명 별칭 연결 예시");
            rows.Rows.Add("규격 확인", 33, "홈플러스 EX (카톤박스)", "플레인 그릭요거트", "예시02", null, null, 1200, "100g 낱개·묶음 규격 확인");
            rows.Rows.Add("미매핑", 74, "킴스클럽", "플레인 그릭요거트", null, null, null, 360, "채널 코드 확인 예시");
            return rows;
        }
    }
}
