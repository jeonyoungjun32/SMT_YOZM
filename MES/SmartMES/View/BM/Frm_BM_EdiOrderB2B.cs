using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using SmartMES.Models.BM;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models.CM;

namespace SmartMES.View.BM
{
    /// <summary>B2B·B2C 등록</summary>
    public partial class Frm_BM_EdiOrderB2B : XtraForm
    {
        private readonly clsGrid grid = new clsGrid();
        private bool uploading;
        private bool calculatingAmount;
        private bool refreshPricesPending;
        private bool deleteRegistration = true;
        private DataTable detailRows;
        private readonly Dictionary<long, DataTable> fileDetails = new Dictionary<long, DataTable>();
        private readonly HashSet<long> excludedRows = new HashSet<long>();

        public Frm_BM_EdiOrderB2B()
        {
            InitializeComponent();
            SetGridView();
            Activated += (sender, args) => { if (refreshPricesPending) RefreshCustomerPrices(); };
            Shown += (sender, args) => BeginInvoke(new Action(ShowAmountColumn));
            // 마지막으로 선택한 그리드에 따라 등록 전체 또는 상세 행 삭제
            gcOrders.Enter += (s, e) => deleteRegistration = true;
            gcOrders.MouseDown += (s, e) => deleteRegistration = true;
            gcEdiDetails.Enter += (s, e) => deleteRegistration = false;
            gcEdiDetails.MouseDown += (s, e) => deleteRegistration = false;
            EnableFileDrop(this);
            FormClosing += (s, e) =>
            {
                if (uploading)
                    e.Cancel = true;
            };
        }

        /// <summary>목록·상세 컬럼과 편집·합계 설정</summary>
        private void SetGridView()
        {
            gvCustomers.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            var customerColumns = new (string Caption, string Field, int Width)[]
            {
                ("분류", "CustType", 70),
                ("거래처코드", "거래처코드", 105),
                ("거래처명", "거래처명", 220),
            };

            foreach (var column in customerColumns)
            {
                grid.Create_GvColumn(gvCustomers, column.Caption, column.Field);
                gvCustomers.Columns[column.Field].Width = column.Width;
            }

            gvOrders.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            var orderColumns = new (string Caption, string Field, int Width)[]
            {
                ("업로드번호", "FileId", 95),
                ("분류", "CustType", 100),
                ("거래처", "CustName", 200),
                ("EDI 등록일", "BasisDate", 105),
                ("파일명", "FileName", 320),
                ("처리상태", "Status", 95),
                ("자료수", "RowCount", 75),
                ("반영수", "AppliedCount", 75),
                ("오류내용", "ErrorMessage", 250),
                ("등록자", "CreId", 90),
                ("등록일시", "CreDt", 150),
            };

            foreach (var column in orderColumns)
            {
                grid.Create_GvColumn(gvOrders, column.Caption, column.Field);
                gvOrders.Columns[column.Field].Width = column.Width;
            }

            gvEdiDetails.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            var detailColumns = new (string Caption, string Field, int Width)[]
            {
                ("원본행", "SourceRow", 75),
                ("반영 선택", "ApplySelected", 80),
                ("거래처코드", "CustCode", 120),
                ("거래처명", "CustName", 140),
                ("품목코드", "ItemCode", 155),
                ("품목명", "ItemName", 210),
                ("발주수량", "OrderQuantity", 85),
                ("원본품명", "SourceName", 230),
                ("옵션", "SourceOption", 240),
                ("주문코드", "SourceCode", 150),
                ("주문번호", "OrderNo", 170),
                ("센터", "CenterName", 130),
                ("납품처", "ShipToName", 170),
                ("출고일", "OrderDate", 105),
                ("원본납기일", "DeliveryDate", 105),
                ("금액", "Amount", 110),
                ("오류내용", "ErrorMessage", 240),
                ("원본시트", "SourceSheet", 120),
            };

            foreach (var column in detailColumns)
            {
                grid.Create_GvColumn(gvEdiDetails, column.Caption, column.Field);
                gvEdiDetails.Columns[column.Field].Width = column.Width;
            }
            gvEdiDetails.Columns["OrderQuantity"].UnboundType = DevExpress.Data.UnboundColumnType.Decimal;

            // 수량·금액의 조회·입력 형식을 정수로 통일
            var numberEditor = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            numberEditor.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            numberEditor.Mask.EditMask = "n0";
            numberEditor.Mask.UseMaskAsDisplayFormat = true;
            numberEditor.AllowNullInput = DefaultBoolean.True;
            gcEdiDetails.RepositoryItems.Add(numberEditor);

            foreach (string field in new[] { "OrderQuantity", "Amount" })
            {
                gvEdiDetails.Columns[field].ColumnEdit = numberEditor;
                gvEdiDetails.Columns[field].DisplayFormat.FormatType = FormatType.Numeric;
                gvEdiDetails.Columns[field].DisplayFormat.FormatString = "n0";
            }

            // 현재 조회된 발주수량·금액의 하단 합계
            gvEdiDetails.OptionsView.ShowFooter = true;
            foreach (string field in new[] { "OrderQuantity", "Amount" })
            {
                gvEdiDetails.Columns[field].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                gvEdiDetails.Columns[field].SummaryItem.DisplayFormat = "{0:n0}";
            }

            gvEdiDetails.OptionsBehavior.Editable = true;
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gvEdiDetails.Columns)
                column.OptionsColumn.AllowEdit = false;
            foreach (string field in new[] { "ApplySelected", "CustCode", "ItemCode", "OrderQuantity", "Amount" })
            {
                gvEdiDetails.Columns[field].OptionsColumn.AllowEdit = true;
                gvEdiDetails.Columns[field].AppearanceCell.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
                gvEdiDetails.Columns[field].AppearanceCell.Options.UseBackColor = true;
            }

            // 선택한 등록 건의 상세 추가, 저장·반영 전까지 DB 집계 제외
            gvEdiDetails.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            gvEdiDetails.InitNewRow += (s, e) =>
            {
                DataRow file = gvOrders.GetFocusedDataRow();
                DataRow row = gvEdiDetails.GetDataRow(e.RowHandle);

                if (file == null || row == null)
                    return;

                string key = "수동:" + Guid.NewGuid().ToString("N");
                row["RowId"] = 0L;
                row["FileId"] = file["FileId"];
                row["CorpId"] = WrGlobal.CorpID;
                row["SourceSheet"] = "직접 등록";
                row["SourceRow"] = 0;
                row["SourceKey"] = key;
                row["RowKey"] = key;
                foreach (string field in new[] { "SourceCode", "SourceName", "SourceOption", "OrderNo", "CenterName", "ShipToName" })
                    row[field] = "";
                row["OrderDate"] = file["BasisDate"];
                row["Qty"] = 0m;
                row["Factor"] = 1m;
                row["Amount"] = DBNull.Value;
                row["AmountFromPrice"] = false;
                row["Applied"] = false;
                row["ApplySelected"] = true;
                DataRow customer = gvCustomers.GetFocusedDataRow();

                if (customer != null && !string.IsNullOrEmpty(Convert.ToString(customer["거래처코드"])))
                    row["CustCode"] = customer["거래처코드"];
            };

            gvEdiDetails.ShowingEditor += (s, e) =>
            {
                var row = gvEdiDetails.GetFocusedDataRow();
                e.Cancel = (row != null && BM_EdiImport.IsDeliveryFee(row["ItemCode"]) && gvEdiDetails.FocusedColumn.FieldName == "OrderQuantity")
                    || uploading || gvOrders.GetFocusedDataRow() == null
                    || (row == null && !gvEdiDetails.IsNewItemRow(gvEdiDetails.FocusedRowHandle));
            };
            // 묶음 주문도 포케스팅과 동일한 실제 발주수량으로 표시
            gvEdiDetails.CustomUnboundColumnData += (s, e) =>
            {
                if (e.Column.FieldName != "OrderQuantity")
                    return;

                var data = e.Row as DataRowView;

                if (data == null)
                    return;

                DataRow row = data.Row;
                if (BM_EdiImport.IsDeliveryFee(row["ItemCode"])) { e.Value = DBNull.Value; return; }

                decimal factor = row.IsNull("Factor") ? 1m : Convert.ToDecimal(row["Factor"]);
                if (e.IsGetData)
                    e.Value = row.IsNull("Qty") ? (object)DBNull.Value : Convert.ToDecimal(row["Qty"]) * factor;

                if (e.IsSetData && factor > 0)
                {
                    decimal quantity = Convert.ToDecimal(e.Value) / factor;
                    if (quantity != Math.Round(quantity, 4))
                    {
                        XtraMessageBox.Show(this, "묶음 수량 단위로 입력해 주세요.", "발주수량");
                        return;
                    }

                    row["Qty"] = quantity;
                    BM_EdiImport.PreviewAmount(row);
                }
            };
            gvOrders.FocusedRowChanged += (s, e) => DisplayFile();
        }

        private void Frm_BM_EdiOrderB2B_Load(object sender, EventArgs e)
        {
            orderDate.EditValue = DateTime.Today;
            var repository = new clsRepository();
            var customers = BM_EdiImport.Query("SELECT CustCode, CustName FROM CM_Cust WHERE CorpId=@Corp AND CustType IN('B2B','B2C') AND UseYn='Y'");
            var items = BM_EdiImport.Query("SELECT ItemCode,ItemName FROM CM_Item WHERE CorpId=@Corp AND UseYn='Y'");
            var customerLookup = repository.Create_RepositoryItemSearchLookUp(customers, "CustCode", "CustCode");
            var itemLookup = repository.Create_RepositoryItemSearchLookUp(items, "ItemCode", "ItemCode");
            gcEdiDetails.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { customerLookup, itemLookup });
            gvEdiDetails.Columns["CustCode"].ColumnEdit = customerLookup;
            gvEdiDetails.Columns["ItemCode"].ColumnEdit = itemLookup;
            itemLookup.PopupFormSize = new System.Drawing.Size(800, 450);
            customerLookup.PopupFormSize = new System.Drawing.Size(600, 400);
            gvEdiDetails.CellValueChanged += (s, args) =>
            {
                if (args.Column.FieldName == "ApplySelected")
                {
                    DataRow selectedRow = gvEdiDetails.GetDataRow(args.RowHandle);

                    if (selectedRow != null && selectedRow.RowState != DataRowState.Added)
                    {
                        long rowId = Convert.ToInt64(selectedRow["RowId"]);

                        if (Convert.ToBoolean(args.Value)) excludedRows.Remove(rowId);
                        else excludedRows.Add(rowId);
                    }
                }
                // 직접 입력한 금액은 단가 자동 계산에서 제외
                if (args.Column.FieldName == "Amount" && !calculatingAmount)
                    gvEdiDetails.SetRowCellValue(args.RowHandle, "AmountFromPrice", false);

                if (args.Column.FieldName == "ItemCode")
                {
                    var matches = items.AsEnumerable().Where(r => Convert.ToString(r["ItemCode"]) == Convert.ToString(args.Value)).ToArray();
                    gvEdiDetails.SetRowCellValue(args.RowHandle, "ItemName", matches.Length == 1 ? matches[0]["ItemName"] : DBNull.Value);
                    if (BM_EdiImport.IsDeliveryFee(args.Value))
                    {
                        DataRow fee = gvEdiDetails.GetDataRow(args.RowHandle);
                        fee["Qty"] = 0m;
                        fee["Factor"] = 1m;
                        if (!fee.IsNull("AmountFromPrice") && Convert.ToBoolean(fee["AmountFromPrice"]))
                            fee["Amount"] = DBNull.Value;
                        fee["AmountFromPrice"] = false;
                    }
                }

                if (args.Column.FieldName == "CustCode")
                {
                    var matches = customers.AsEnumerable().Where(r => Convert.ToString(r["CustCode"]) == Convert.ToString(args.Value)).ToArray();
                    gvEdiDetails.SetRowCellValue(args.RowHandle, "CustName", matches.Length == 1 ? matches[0]["CustName"] : DBNull.Value);
                }

                if (!calculatingAmount && new[] { "CustCode", "ItemCode", "Qty", "Factor" }.Contains(args.Column.FieldName))
                {
                    DataRow row = gvEdiDetails.GetDataRow(args.RowHandle);

                    if (row != null)
                    {
                        try
                        {
                            calculatingAmount = true;
                            BM_EdiImport.PreviewAmount(row);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(this, ex.Message, "단가 조회");
                        }
                        finally
                        {
                            calculatingAmount = false;
                        }
                    }
                }
            };
            itemLookup.EditValueChanged += (s, args) => gvEdiDetails.PostEditor();
            customerLookup.EditValueChanged += (s, args) => gvEdiDetails.PostEditor();
            grid.Set_Navigator(gvCustomers);
            grid.RowNumber_Indicator(gvCustomers);
            grid.Set_Navigator(gvOrders);
            gvOrders.OptionsSelection.MultiSelect = true;
            gvOrders.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            grid.RowNumber_Indicator(gvOrders);
            grid.Set_Navigator(gvEdiDetails);
            // 체크박스와 Ctrl+A로 현재 목록의 행 선택
            gvEdiDetails.OptionsSelection.MultiSelect = true;
            gvEdiDetails.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gvEdiDetails.KeyDown += (control, args) =>
            {
                if (args.Control && args.KeyCode == Keys.A && gvEdiDetails.ActiveEditor == null)
                {
                    gvEdiDetails.SelectAll();
                    args.Handled = true;
                    args.SuppressKeyPress = true;
                }
            };
            grid.RowNumber_Indicator(gvEdiDetails);
            DisplayData();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            gvEdiDetails.PostEditor();

            if (!gvEdiDetails.UpdateCurrentRow())
                return;

            // 미저장 편집은 유지하고 조회용 캐시는 갱신해 최신 단가·연결 정보 표시
            foreach (long id in fileDetails.Where(entry => !entry.Value.AsEnumerable()
                .Any(row => row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified))
                .Select(entry => entry.Key).ToArray())
                fileDetails.Remove(id);

            DisplayData();
            ShowAmountColumn();
        }

        /// <summary>가로 스크롤과 기존 화면 배치에 가려지지 않도록 금액·합계 고정</summary>
        private void ShowAmountColumn()
        {
            // 저장된 화면 배치에서도 완료 표시는 제외, 실제 처리 상태는 DB에서 유지
            var applied = gvEdiDetails.Columns["Applied"];
            if (applied != null)
                gvEdiDetails.Columns.Remove(applied);

            var amount = gvEdiDetails.Columns["Amount"];
            amount.Visible = true;
            amount.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            amount.MinWidth = 130;
            amount.Width = 130;
            gvEdiDetails.OptionsView.ShowFooter = true;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gvCustomers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            DisplayCustomer();
        }

        /// <summary>현재 사업장의 B2B·B2C 거래처와 EDI 등록일별 업로드 조회</summary>
        private void DisplayData()
        {
            string keyword = customerSearch.Text.Trim();
            DataTable customers = BM_EdiImport.Query(@"SELECT CustCode AS 거래처코드, CustName AS 거래처명, CustType FROM CM_Cust WHERE CorpId=@Corp AND CustType IN ('B2B','B2C') AND (@Type='전체' OR CustType=@Type)", "@Type", customerType.Text);

            if (customers != null && keyword.Length > 0)
            {
                for (int index = customers.Rows.Count - 1; index >= 0; index--)
                {
                    DataRow row = customers.Rows[index];
                    string code = row["거래처코드"].ConvertString_Null_To_Empty();
                    string name = row["거래처명"].ConvertString_Null_To_Empty();

                    if (code.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0 && name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) < 0)
                        customers.Rows.RemoveAt(index);
                }
            }

            if (customers != null)
            {
                // 전체 항목 우선, 나머지 거래처는 이름순 정렬
                customers.Columns.Add("전체정렬", typeof(int));
                foreach (DataRow customer in customers.Rows)
                    customer["전체정렬"] = 1;

                DataRow all = customers.NewRow();
                all["전체정렬"] = 0;
                all["거래처코드"] = "";
                all["거래처명"] = "전체";
                customers.Rows.InsertAt(all, 0);
                customers.DefaultView.Sort = "전체정렬, 거래처명, 거래처코드";
            }

            gvCustomers.ClearSorting();
            gcCustomers.DataSource = customers;
            gvCustomers.FocusedRowHandle = gvCustomers.LocateByValue("거래처코드", "");
            DisplayCustomer();
        }

        /// <summary>거래처 금액 저장 후 자동 계산 행만 갱신, 편집 내용과 선택 위치 유지</summary>
        internal void RefreshCustomerPrices()
        {
            refreshPricesPending = true;

            if (uploading || IsDisposed)
                return;

            gvEdiDetails.PostEditor();

            if (!gvEdiDetails.UpdateCurrentRow())
                return;

            try
            {
                calculatingAmount = true;
                foreach (var file in fileDetails)
                {
                    // 파일당 한 번 조회, 원본 금액과 직접 입력 금액은 재계산 대상에서 제외
                    using (DataTable latest = BM_EdiImport.Details(file.Key))
                    {
                        var prices = latest.AsEnumerable().ToDictionary(row => Convert.ToInt64(row["RowId"]));
                        foreach (DataRow row in file.Value.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted)
                                continue;

                            if (!row.IsNull("Amount") && (row.IsNull("AmountFromPrice") || !Convert.ToBoolean(row["AmountFromPrice"])))
                                continue;

                            DataRow saved;
                            if (prices.TryGetValue(Convert.ToInt64(row["RowId"]), out saved)
                                && Equals(row["CustCode"], saved["CustCode"])
                                && Equals(row["ItemCode"], saved["ItemCode"])
                                && Equals(row["OrderDate"], saved["OrderDate"]))
                                BM_EdiImport.PreviewAmount(row, saved["PreviewPrice"]);
                            else
                                BM_EdiImport.PreviewAmount(row);
                        }
                    }
                }

                gvEdiDetails.RefreshData();
                refreshPricesPending = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "B2B 금액 갱신");
            }
            finally
            {
                calculatingAmount = false;
            }
        }
        private void DisplayCustomer()
        {
            DataRow row = gvCustomers.GetFocusedDataRow();

            if (orderDate.EditValue == null)
                return;
            gcOrders.DataSource = BM_EdiImport.Files(orderDate.DateTime, row == null ? "" : Convert.ToString(row["거래처코드"]), customerType.Text);
            // 미반영 자료만 기본 선택, 완료 자료는 사용자가 선택한 경우에만 수정
            gvOrders.ClearSelection();
            for (int handle = 0; handle < gvOrders.RowCount; handle++)
            {
                DataRow file = gvOrders.GetDataRow(handle);
                if (file != null && Convert.ToInt32(file["RowCount"]) > Convert.ToInt32(file["AppliedCount"]))
                    gvOrders.SelectRow(handle);
            }
            DisplayFile();
        }

        /// <summary>선택한 업로드 파일의 상세 조회</summary>
        private void DisplayFile()
        {
            DataRow file = gvOrders.GetFocusedDataRow();
            detailRows = null;

            if (file != null)
            {
                long fileId = Convert.ToInt64(file["FileId"]);

                if (!fileDetails.TryGetValue(fileId, out detailRows))
                {
                    detailRows = BM_EdiImport.Details(fileId);
                    detailRows.Columns.Add("ApplySelected", typeof(bool)).DefaultValue = true;
                    foreach (DataRow row in detailRows.Rows)
                        row["ApplySelected"] = !excludedRows.Contains(Convert.ToInt64(row["RowId"]));
                    detailRows.AcceptChanges();
                    fileDetails[fileId] = detailRows;
                }
            }
            gcEdiDetails.DataSource = detailRows;
            ShowAmountColumn();

        }

        /// <summary>모든 하위 컨트롤에서 엑셀·ZIP 파일 드롭 수신</summary>
        private void EnableFileDrop(Control control)
        {
            control.AllowDrop = true;
            control.DragEnter += (s, e) => e.Effect = !uploading && e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
            control.DragDrop += async (s, e) => await UploadFiles((string[])e.Data.GetData(DataFormats.FileDrop));

            foreach (Control child in control.Controls)
                EnableFileDrop(child);
        }

        /// <summary>선택한 등록일과 거래처로 직접 입력할 초안 생성</summary>
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (uploading)
                return;

            if (orderDate.EditValue == null)
            {
                XtraMessageBox.Show(this, "EDI 등록일을 선택해 주세요.", "직접 등록");
                return;
            }

            gvEdiDetails.PostEditor();
            gvEdiDetails.UpdateCurrentRow();

            if (detailRows != null && detailRows.Rows.Cast<DataRow>().Any(row => (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added)))
            {
                XtraMessageBox.Show(this, "수정 중인 내용을 먼저 저장해 주세요.", "직접 등록");
                return;
            }

            try
            {
                DataRow customer = gvCustomers.GetFocusedDataRow();
                string code = customer == null ? "" : customer["거래처코드"].ConvertString_Null_To_Empty();
                long id = BM_EdiImport.CreateManual(orderDate.DateTime.Date, code);
                DisplayCustomer();
                gvOrders.FocusedRowHandle = gvOrders.LocateByValue("FileId", id);
                DisplayFile();
                gvEdiDetails.FocusedColumn = gvEdiDetails.Columns[code.Length == 0 ? "CustCode" : "ItemCode"];
                gcEdiDetails.Focus();
                gvEdiDetails.ShowEditor();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "직접 등록 오류");
            }
        }

        private async void cmdUpload_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "발주서 (*.xlsx;*.xls;*.zip)|*.xlsx;*.xls;*.zip"
            }

            )
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    await UploadFiles(dialog.FileNames);
        }

        /// <summary>엑셀·ZIP 일괄 읽기와 파일별 반영 결과 안내</summary>
        private async Task UploadFiles(string[] paths)
        {
            if (uploading || paths == null)
                return;
            // 업로드 시작 시 선택한 거래처를 고정, 전체 선택은 원본 이름으로 판별
            string selectedCustomer = Convert.ToString(gvCustomers.GetFocusedDataRow()?["거래처코드"]);
            DateTime? uploadedDate = null;
            uploading = true;
            cmdDelete.Enabled = cmdAdd.Enabled = cmdUpload.Enabled = cmdApply.Enabled = cmdSearch.Enabled = false;
            UseWaitCursor = true;
            try
            {
                string result = await Task.Run(() =>
                {
                    var messages = new List<string>();
                    int count = 0;
                    Action<byte[], string> import = (bytes, name) =>
                    {
                        try
                        {
                            // 업로드는 자료 등록만 진행, 포케스팅 반영은 연결 저장·반영 버튼에서 처리
                            long id = BM_EdiImport.Import(bytes, name, selectedCustomer);
                            using (var saved = BM_EdiImport.Query("SELECT BasisDate FROM BM_EdiImportFile WHERE CorpId=@Corp AND FileId=@File", "@File", id))
                                uploadedDate = Convert.ToDateTime(saved.Rows[0][0]);
                            count++;
                        }
                        catch (Exception ex)
                        {
                            messages.Add(name + ": " + ex.Message);
                        }
                    };

                    long totalBytes = 0;
                    foreach (string path in paths)
                    {
                        if (Path.GetExtension(path).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                        {
                            using (var archive = ZipFile.Open(path, ZipArchiveMode.Read, System.Text.Encoding.GetEncoding(949)))
                            {
                                foreach (var entry in archive.Entries)
                                {
                                    if (!IsExcel(entry.Name) || entry.Name.StartsWith("~$"))
                                        continue;
                                    totalBytes += entry.Length;

                                    if (entry.Length > 50 * 1024 * 1024 || totalBytes > 200 * 1024 * 1024)
                                        throw new InvalidOperationException("업로드 크기 제한 초과 (파일 50MB, 전체 200MB)");

                                    using (var stream = entry.Open())
                                    using (var memory = new MemoryStream())
                                    {
                                        stream.CopyTo(memory);
                                        import(memory.ToArray(), entry.Name);
                                    }
                                }
                            }
                        }
                        else if (IsExcel(path))
                        {
                            long size = new FileInfo(path).Length;
                            totalBytes += size;

                            if (size > 50 * 1024 * 1024 || totalBytes > 200 * 1024 * 1024)
                                throw new InvalidOperationException("업로드 크기 제한 초과");

                            import(File.ReadAllBytes(path), Path.GetFileName(path));
                        }
                    }

                    return count + "개 파일 처리. EDI 등록일은 파일명 날짜 기준"
                        + "\n반영할 파일과 품목을 체크한 뒤 연결 저장·반영을 눌러 주세요."
                        + (messages.Count == 0 ? "" : "\n" + string.Join("\n", messages));
                });

                if (uploadedDate.HasValue)
                    orderDate.DateTime = uploadedDate.Value;

                DisplayData();
                XtraMessageBox.Show(this, result, "EDI 업로드");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "EDI 업로드 오류");
            }
            finally
            {
                uploading = false;
                UseWaitCursor = false;
                cmdDelete.Enabled = cmdAdd.Enabled = cmdUpload.Enabled = cmdApply.Enabled = cmdSearch.Enabled = true;
            }
        }

        /// <summary>열려 있는 포케스팅 화면의 수량·합계 갱신</summary>
        private async Task RefreshForecastFormsAsync()
        {
            foreach (var form in Application.OpenForms.OfType<SmartMES.View.PM.Frm_PM_Forecasting>().ToArray())
            {
                if (form.IsDisposed || form.Disposing)
                    continue;

                try
                {
                    await form.RefreshEdiQuantitiesAsync();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(this, "DB 반영 후 포케스팅 화면 갱신에 실패했습니다. 조회를 다시 눌러 주세요.\n" + ex.Message,
                        "포케스팅 자동 갱신");
                }
            }
        }

        private static bool IsExcel(string path)
        {
            string extension = Path.GetExtension(path);
            return extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase) || extension.Equals(".xls", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>선택 영역의 등록 또는 상세 삭제와 포케스팅 재집계</summary>
        private async void cmdDelete_Click(object sender, EventArgs e)
        {
            if (uploading || gvOrders.GetFocusedDataRow() == null)
                return;

            bool deleteFile = deleteRegistration;
            gvEdiDetails.PostEditor();

            if (!gvEdiDetails.UpdateCurrentRow())
                return;

            var selected = deleteFile
                ? (detailRows == null ? new DataRow[0] : detailRows.Rows.Cast<DataRow>().ToArray())
                : gvEdiDetails.GetSelectedRows().Where(handle => handle >= 0)
                    .Select(handle => gvEdiDetails.GetDataRow(handle)).Where(row => row != null).ToArray();

            if (!deleteFile && selected.Length == 0)
            {
                XtraMessageBox.Show(this, "삭제할 행을 체크해 주세요.", "삭제");
                return;
            }

            if (!deleteFile && detailRows.Rows.Cast<DataRow>().Any(row => !selected.Contains(row)
                && (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)))
            {
                XtraMessageBox.Show(this, "선택하지 않은 행의 수정 내용을 먼저 저장해 주세요.", "삭제");
                return;
            }

            string message = deleteFile
                ? "선택한 등록 자료와 해당 자료의 모든 품목을 삭제하시겠습니까?"
                : $"선택한 {selected.Length}개 행을 삭제하시겠습니까?";

            if (XtraMessageBox.Show(this, message + "\n반영된 포케스팅 수량도 함께 재계산됩니다.",
                "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            long fileId = Convert.ToInt64(gvOrders.GetFocusedDataRow()["FileId"]);
            long[] ids = selected.Where(row => row.RowState != DataRowState.Added)
                .Select(row => Convert.ToInt64(row["RowId"])).ToArray();
            int focusedRow = gvEdiDetails.FocusedRowHandle;
            uploading = true;
            body.Enabled = registrationFields.Enabled = false;
            cmdDelete.Enabled = cmdAdd.Enabled = cmdUpload.Enabled = cmdApply.Enabled = cmdSearch.Enabled = false;
            UseWaitCursor = true;
            try
            {
                // 선택한 저장 행은 한 트랜잭션으로 삭제, 실패 시 전체 롤백
                await Task.Run(() => BM_EdiImport.Delete(fileId, ids, deleteFile));
                foreach (DataRow row in selected.Where(row => row.RowState == DataRowState.Added))
                    detailRows.Rows.Remove(row);

                await RefreshForecastFormsAsync();

                fileDetails.Remove(fileId);
                DisplayCustomer();

                int handle = gvOrders.LocateByValue("FileId", fileId);

                if (handle >= 0)
                    gvOrders.FocusedRowHandle = handle;

                DisplayFile();

                gvEdiDetails.ClearSelection();

                if (gvEdiDetails.DataRowCount > 0)
                    gvEdiDetails.FocusedRowHandle = Math.Max(0, Math.Min(focusedRow, gvEdiDetails.DataRowCount - 1));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "삭제 오류");
            }
            finally
            {
                uploading = false;
                body.Enabled = registrationFields.Enabled = true;
                cmdDelete.Enabled = cmdAdd.Enabled = cmdUpload.Enabled = cmdApply.Enabled = cmdSearch.Enabled = true;
                UseWaitCursor = false;
            }
        }

        /// <summary>상세 저장·포케스팅 반영을 작업 스레드에서 처리</summary>
        private async void cmdApply_Click(object sender, EventArgs e)
        {
            if (uploading)
                return;

            gvEdiDetails.PostEditor();

            if (!gvEdiDetails.UpdateCurrentRow())
                return;

            // 완료 자료에 품목을 추가·수정한 경우 해당 파일도 저장 대상에 포함
            var editedFiles = new HashSet<long>(fileDetails.Where(entry => entry.Value.AsEnumerable().Any(row =>
                row.RowState == DataRowState.Added || (row.RowState == DataRowState.Modified &&
                new[] { "CustCode", "ItemCode", "Qty", "Factor", "Amount", "AmountFromPrice" }
                    .Any(field => !Equals(row[field], row[field, DataRowVersion.Original])))))
                .Select(entry => entry.Key));

            var files = Enumerable.Range(0, gvOrders.RowCount)
                .Where(handle => editedFiles.Count > 0
                    ? editedFiles.Contains(Convert.ToInt64(gvOrders.GetDataRow(handle)["FileId"]))
                    : gvOrders.IsRowSelected(handle))
                .Select(handle => gvOrders.GetDataRow(handle)).Where(row => row != null).ToArray();

            if (files.Length == 0)
            {
                XtraMessageBox.Show(this, "반영할 파일을 체크해 주세요.", "포케스팅 반영");
                return;
            }

            // 작업 스레드에서는 화면 바인딩 테이블 대신 복사본 사용
            var pending = new Dictionary<long, DataTable>();
            foreach (DataRow file in files)
            {
                long id = Convert.ToInt64(file["FileId"]);
                DataTable source;

                if (fileDetails.TryGetValue(id, out source))
                {
                    // 체크 해제한 품목의 미저장 편집 내용은 조회로 유실되지 않도록 확인
                    if (source.AsEnumerable().Any(row => !Convert.ToBoolean(row["ApplySelected"])
                        && (row.RowState == DataRowState.Added || (row.RowState == DataRowState.Modified
                            && source.Columns.Cast<DataColumn>().Any(column => column.ColumnName != "ApplySelected"
                                && !Equals(row[column], row[column, DataRowVersion.Original]))))))
                    {
                        foreach (DataTable copyTable in pending.Values) copyTable?.Dispose();
                        XtraMessageBox.Show(this, "체크 해제한 품목에 수정 내용이 있습니다. 저장할 품목을 체크해 주세요.", "포케스팅 반영");
                        return;
                    }
                    DataTable copy = source.Clone();
                    foreach (DataRow row in source.Rows)
                        if (Convert.ToBoolean(row["ApplySelected"]) &&
                            (!Convert.ToBoolean(row["Applied"]) || row.RowState == DataRowState.Added ||
                             (row.RowState == DataRowState.Modified && new[] { "CustCode", "ItemCode", "Qty", "Factor", "Amount", "AmountFromPrice" }
                                .Any(field => !Equals(row[field], row[field, DataRowVersion.Original])))))
                            copy.ImportRow(row);
                    pending[id] = copy;
                }
                else pending[id] = null;
            }
            long[] excluded = excludedRows.ToArray();
            var savedFiles = new HashSet<long>();
            uploading = true;
            body.Enabled = registrationFields.Enabled = false;
            cmdDelete.Enabled = cmdAdd.Enabled = cmdUpload.Enabled = cmdApply.Enabled = cmdSearch.Enabled = false;
            UseWaitCursor = true;
            try
            {
                var results = await Task.Run(() =>
                {
                    var messages = new List<string>();
                    foreach (var entry in pending)
                    {
                        using (DataTable rows = entry.Value ?? BM_EdiImport.Details(entry.Key))
                        {
                            try
                            {
                                // 완료된 파일을 다시 체크해도 변경 없는 행은 재처리 제외
                                if (entry.Value == null)
                                {
                                    foreach (DataRow row in rows.Select("Applied = true")) rows.Rows.Remove(row);
                                }

                                if (rows.Rows.Count == 0)
                                    continue;

                                BM_EdiImport.SaveMappings(rows);
                                savedFiles.Add(entry.Key);
                                long[] ids = rows.AsEnumerable().Select(row => Convert.ToInt64(row["RowId"]))
                                    .Where(id => !excluded.Contains(id)).ToArray();
                                string result = ids.Length == 0 ? "선택 품목 없음" : BM_EdiImport.Apply(entry.Key, ids);
                                messages.Add(entry.Key + ": " + result);
                            }
                            catch (Exception ex) { messages.Add(entry.Key + ": " + ex.Message); }
                        }
                    }
                    return messages;
                });
                foreach (long id in savedFiles) fileDetails.Remove(id);
                await RefreshForecastFormsAsync();
                DisplayData();
                XtraMessageBox.Show(this, results.Count == 0 ? "새로 반영하거나 수정할 품목이 없습니다." : string.Join("\n", results), "포케스팅 반영");
            }
            catch (Exception ex) { XtraMessageBox.Show(this, ex.Message, "반영 오류"); }
            finally
            {
                uploading = false;
                body.Enabled = registrationFields.Enabled = true;
                cmdDelete.Enabled = cmdAdd.Enabled = cmdUpload.Enabled = cmdApply.Enabled = cmdSearch.Enabled = true;
                UseWaitCursor = false;
            }
        }
    }
}
