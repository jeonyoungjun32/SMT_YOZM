using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using SmartCommon.Common;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Reflection;
using System.Linq;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Models.MM;

namespace SmartMES.View.MM
{
    /// <summary>
    /// 자재 발주
    /// </summary>
    public partial class Frm_MM_Placing_MD : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsCode code = new clsCode();
        clsPurchase purchase = new clsPurchase();
        clsRepository rpo = new clsRepository();
        clsGrid grid = new clsGrid();
        string query = "";
        string placingType = "";

        public Frm_MM_Placing_MD()
        {
            InitializeComponent();
        }

        public Frm_MM_Placing_MD(string title)
        {
            InitializeComponent();
            this.Text = title;
            this.placingType = title;
        }

        private void Frm_MM_Placing_MD_Load(object sender, EventArgs e)
        {
            //기초정보 세팅
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            customerBox.smtLookUpDataSource = code.GetCustomerI_Lup(WrGlobal.CorpID, true);
            customerBox.smtLookUpDataDisplayMember = "거래처명";
            customerBox.smtLookUpDataValueMember = "거래처코드";
            customerBox.smtLookUpValue = "ALL";

            //프로세스 옵션 : 발주서 출력, 옵션명 없을 경우 버튼 숨김처리
            if(WrGlobal.ProcOption["MT"]["PlacingOrder_Report"].Trim() == "")
			{
                cmdPO.Visible = false;
			}

            Set_GvMaster(gvPlacingMaster);
            Set_GvDetail(gvDetail);

            DisplayData();
        }

        private void Set_GvMaster(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = true;

            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;

            //Column 생성
            string stateField = MM_PlacingD.UseYozmOrderColumns ? "ReceiptProgress" : "State";
            grid.Create_GvColumn(gv, "발주번호", "MmOrderNo");
            grid.Create_GvColumn(gv, "거래처", "CustName");
            grid.Create_GvColumn(gv, "발주일자", "PlacingDate");
            grid.Create_GvColumn(gv, "납기일자", "DeliveryDate");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "상태", stateField);

            if (MM_PlacingD.UseYozmOrderColumns)
            {
                foreach (var column in new[]
                {
                    (Field: stateField, Width: 100), (Field: "MmOrderNo", Width: 140),
                    (Field: "CustName", Width: 160), (Field: "PlacingDate", Width: 110),
                    (Field: "DeliveryDate", Width: 110), (Field: "Price", Width: 110),
                    (Field: "VatPrice", Width: 105), (Field: "TotalPrice", Width: 110),
                    (Field: "Remark", Width: 200)
                })
                {
                    gv.Columns[column.Field].MinWidth = column.Width * DeviceDpi / 96;
                    gv.Columns[column.Field].Width = gv.Columns[column.Field].MinWidth;
                }
                grid.SetCol_Type(gv.Columns["PlacingDate"], clsGrid.TypeList.TDate);
                grid.SetCol_Type(gv.Columns["DeliveryDate"], clsGrid.TypeList.TDate);
                SetOrderStatusEditor(gv, stateField);
            }
            //Column 단일 설정

            //셀값 변경 허용

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            gv.Columns["DeliveryDate"].Visible = MM_PlacingD.UseYozmOrderColumns;

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        private void SetOrderStatusEditor(GridView view, string field)
        {
            var editor = new RepositoryItemButtonEdit
            {
                TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            };
            editor.Buttons.Clear();
            editor.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(
                DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph) { Caption = "발주로 변경" });
            editor.ButtonClick += (sender, args) =>
            {
                DataRow row = view.GetFocusedDataRow();

                if (row == null)
                    return;

                try
                {
                    if (!MM_PlacingM.ReopenYozmOrder(row["MmOrderNo"].ToString()))
                    {
                        MessageBox.Show("발주로 변경할 수 없는 상태입니다. 다시 조회하세요.", "상태 변경");
                        return;
                    }
                    view.HideEditor();
                    DisplayData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "상태 변경");
                }
            };
            view.GridControl.RepositoryItems.Add(editor);
            view.OptionsBehavior.Editable = true;
            view.Columns[field].OptionsColumn.AllowEdit = true;
            view.CustomRowCellEditForEditing += (sender, args) =>
            {
                if (args.Column.FieldName == field)
                    args.RepositoryItem = editor;
            };
            view.ShowingEditor += (sender, args) =>
            {
                DataRow row = view.GetFocusedDataRow();
                args.Cancel = view.FocusedColumn?.FieldName != field || row == null ||
                    !HasSelectedOrderBeenClosed() || row["State"].ToString() != MM_PlacingD.NewOrderState;
            };
        }

        private void Set_GvDetail(GridView gv)
        {
            gv.Columns.Clear();

            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;

            //
            gv.OptionsBehavior.ReadOnly = true;

            if (MM_PlacingD.UseYozmOrderColumns)
            {
                {
                    bool editable = false;
                    RepositoryItemButtonEdit delete = null;
                    var types = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A010"), "코드", "명칭");
                    var large = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
                    var middle = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
                    var units = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
                    var houses = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");
        
                    grid.Create_GvColumn(gv, "순번", "MmOrderSeq");
                    grid.Create_GvColumn(gv, "품목코드", "ItemCode");
                    grid.Create_GvColumn(gv, "품목명", "ItemName");
                    grid.Create_GvColumn(gv, "규격", "Size");
                    grid.Create_GvColumn(gv, "구분", "ItemType", types);
                    grid.Create_GvColumn(gv, "대분류", "ItemClass1", large);
                    grid.Create_GvColumn(gv, "중분류", "ItemClass2", middle);
                    grid.Create_GvColumn(gv, "발주단위", "UnitCode", units);
                    grid.Create_GvColumn(gv, "발주수량", "Qty", null, editable);
                    grid.Create_GvColumn(gv, "납기일자", "DeliveryDate", editable ? rpo.Create_rpoDate() : null, editable);
                    grid.Create_GvColumn(gv, "입고창고", "ReceiveHouseId", houses, editable);
                    grid.Create_GvColumn(gv, "비고", "Remark", new RepositoryItemTextEdit { MaxLength = 500 }, editable);
                    grid.Create_GvColumn(gv, "납품초과 허용율(%)", "DeliveryOverTolerancePct");
                    grid.Create_GvColumn(gv, "환산수량", "PurchaseConversionQty");
                    grid.Create_GvColumn(gv, "납품누계", "DeliveredQty");
                    grid.Create_GvColumn(gv, "입고누계", "InQty");
                    grid.Create_GvColumn(gv, "진행상태", "State");
                    grid.Create_GvColumn(gv, "BOM", "BomChecked", new RepositoryItemCheckEdit(), editable);
                    grid.Create_GvColumn(gv, "단가", "UnitPrice", null, editable);
                    grid.Create_GvColumn(gv, "금액", "Price");
                    grid.Create_GvUnboundColumn(gv, "수량", "DisplayQty", typeof(double), "[Qty]",
                        DevExpress.Utils.FormatType.Numeric, "#,##0.####");
                    grid.Create_GvColumn(gv, "MRP번호", "MrpNo", new RepositoryItemTextEdit { MaxLength = 20 }, editable);
                    grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
                    grid.Create_GvColumn(gv, "등록자", "CreId");
                    grid.Create_GvColumn(gv, "등록일", "CreDt");
                    grid.Create_GvColumn(gv, "수정자", "ModId");
                    grid.Create_GvColumn(gv, "수정일", "ModDt");
        
                    grid.SetCol_Type(gv.Columns["DeliveryDate"], clsGrid.TypeList.TDate);
                    foreach (string field in new[] { "CreDt", "ModDt" })
                        grid.SetCol_Type(gv.Columns[field], clsGrid.TypeList.TDateTime);
                    foreach (string field in new[] { "Qty", "DeliveryOverTolerancePct", "PurchaseConversionQty", "DeliveredQty", "InQty", "UnitPrice", "Price" })
                    {
                        gv.Columns[field].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gv.Columns[field].DisplayFormat.FormatString = "#,##0.######";
                    }
                    if (editable)
                    {
                        foreach (string field in new[] { "Qty", "DeliveryDate", "ReceiveHouseId", "Remark", "BomChecked", "UnitPrice", "MrpNo" })
                        {
                            var appearance = gv.Columns[field].AppearanceCell;
                            appearance.BackColor = field == "Qty" ? System.Drawing.Color.SeaShell : baseSMT.smtDesignSetting.MustColor;
                            appearance.Options.UseBackColor = true;
                        }
                        grid.Create_GvColumn(gv, "삭제", "colDelete", delete, true);
                        gv.Columns["colDelete"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
                    }
                }
                grid.SetCol_Summary(gv, "Price");
                gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
                grid.Set_Navigator(gv);
                return;
            }

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", "", "", ""), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");
            //Column 생성

            grid.Create_GvColumn(gv, "순번", "MmOrderSeq");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "납기예정일자", "DeliveryDate");
            grid.Create_GvColumn(gv, "발주량", "Qty");
            grid.Create_GvColumn(gv, "입고량", "InQty");
            grid.Create_GvColumn(gv, "미입고수량", "BalanceQty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            //grid.Create_GvColumn(gv, "등록IP", "CreIP");
            //grid.Create_GvColumn(gv, "등록일시", "CreDt");
            //grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            //grid.Create_GvColumn(gv, "수정IP", "ModIP");
            //grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //Column 단일 설정

            //셀값 변경 허용

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["DeliveryDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BalanceQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        private void DisplayData()
        {
            gvPlacingMaster.FocusInvalidRow();
			//this.Validate();

			string typeCode = "";
			switch (placingType)
			{
				case "원자재발주":
				case "자재발주":
					typeCode = "10";
					break;
				case "부자재발주":
					typeCode = "20";
					break;
				case "소모품발주":
					typeCode = "30";
					break;
				case "상품발주":
					typeCode = "50";
					break;
			}

			string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
			string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");
			string custCode = customerBox.smtLookUpValue;

			MM_PlacingM pm = new MM_PlacingM();
			pm.CustCode = custCode;
			pm.PlacingType = typeCode;

            DataTable orders = pm.Select_MM_PlacingM(startDate, endDate);
            if (MM_PlacingD.UseYozmOrderColumns && orders != null && orders.Columns.Contains("ReceiptProgress"))
                orders.Columns["ReceiptProgress"].ReadOnly = false;
            gcPlacingMaster.DataSource = orders;
			gvPlacingMaster.BestFitColumns();
		}

        private void gvPlacingMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayDetailList();
        }

        private void DisplayDetailList()
        {
            string mmOrderNo = (gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo") ?? "").ToString();

            MM_PlacingD pd = new MM_PlacingD();
            pd.MmOrderNo = mmOrderNo;

            gcDetail.DataSource = pd.Select_MM_PlacingD_MmOrderNo();
            gvDetail.BestFitColumns();
        }

        //조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        //등록
        private void button6_Click(object sender, EventArgs e)
        {
            //if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            //{
                Frm_MM_PlacingD form = new Frm_MM_PlacingD("Insert", "", placingType);
                form.ShowDialog();
            //}
            //else
            //{ 
                //frmMM_PlacingD form = new frmMM_PlacingD("Insert", "", placingType);
                //form.ShowDialog();
            //}
            
            DisplayData();
        }

        //수정
        private void button5_Click(object sender, EventArgs e)
        {
            if (gvPlacingMaster.FocusedRowHandle < 0) return;

            string orderNo = gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString();
            string state = purchase.GetPlacingState(orderNo);
            GridColumn masterColumn = gvPlacingMaster.FocusedColumn;
            int masterTopRow = gvPlacingMaster.TopRowIndex;
            int masterLeft = gvPlacingMaster.LeftCoord;
            object detailSequence = gvDetail.GetFocusedRowCellValue("MmOrderSeq");
            GridColumn detailColumn = gvDetail.FocusedColumn;
            int detailTopRow = gvDetail.TopRowIndex;
            int detailLeft = gvDetail.LeftCoord;

            try
            {
                if (!IsSelectedOrderClosed(state))
                {
                    using (var form = new Frm_MM_PlacingD("Update", orderNo))
                        form.ShowDialog(this);

                    gvPlacingMaster.BeginUpdate();
                    gvDetail.BeginUpdate();
                    try
                    {
                        DisplayData();
                        // 재조회 후 행 번호가 달라져도 수정한 발주와 상세 항목으로 돌아갑니다.
                        if (RestoreGridPosition(gvPlacingMaster, "MmOrderNo", orderNo,
                            masterColumn, masterTopRow, masterLeft))
                        {
                            // 같은 행 번호로 복원되면 FocusedRowChanged가 발생하지 않을 수 있습니다.
                            DisplayDetailList();
                            RestoreGridPosition(gvDetail, "MmOrderSeq", detailSequence,
                                detailColumn, detailTopRow, detailLeft);
                        }
                    }
                    finally
                    {
                        gvDetail.EndUpdate();
                        gvPlacingMaster.EndUpdate();
                    }
                    gvPlacingMaster.Focus();
                }
                else
                {
                    MessageBox.Show("완료 상태의 건은 수정이 불가능합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static bool RestoreGridPosition(GridView view, string keyField, object key,
            GridColumn column, int topRow, int leftCoord)
        {
            if (key == null || key == DBNull.Value || view.DataRowCount == 0)
                return false;

            int rowHandle = view.LocateByValue(keyField, key);
            if (rowHandle < 0)
                return false;

            view.FocusedRowHandle = rowHandle;
            if (column != null)
                view.FocusedColumn = column;
            view.TopRowIndex = Math.Max(0, Math.Min(topRow, view.DataRowCount - 1));
            view.LeftCoord = leftCoord;
            view.MakeRowVisible(rowHandle);
            return true;
        }

        //삭제
        private void button4_Click(object sender, EventArgs e)
        {
            if (gvPlacingMaster.FocusedRowHandle < 0) return;

            string state = purchase.GetPlacingState(gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());
            if (IsSelectedOrderClosed(state))
            {
                MessageBox.Show("완료 처리된 건은 삭제할 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //자료를 삭제하는 부분
            if (MessageBox.Show("선택한 항목를 삭제 하겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                MessageBox.Show("삭제되었습니다.");
                return;
            }//end if

            try
            {
                string sdOrderNo = gvPlacingMaster.GetRowCellValue(gvPlacingMaster.FocusedRowHandle, "MmOrderNo").ToString();

                if (sdOrderNo.Length > 0)
                {
                    purchase.DeleteMM_PlacingM(gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());

                    DisplayData();
                }//end if
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //완료처리
        private void Btn_Row_Ins_Click(object sender, EventArgs e)
        {
            if (MM_PlacingD.UseYozmOrderColumns)
            {
                OpenSelectedOrderReceipt();
                return;
            }

            if (gvPlacingMaster.FocusedRowHandle < 0) return;

            DataRow order = gvPlacingMaster.GetFocusedDataRow();

            if (order == null)
                return;

            string state = order["State"].ToString();

            if (IsSelectedOrderClosed(state))
            {
                MessageBox.Show("완료 처리된 항목입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("선택한 항목을 완료처리 하시겠습니까?", "완료처리",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }//end if

            try
            {
                string sdOrderNo = gvPlacingMaster.GetRowCellValue(gvPlacingMaster.FocusedRowHandle, "MmOrderNo").ToString();

                if (sdOrderNo.Length > 0)
                {
                    purchase.EndMM_PlacingM(gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());
                    DisplayData();
                }//end if
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsSelectedOrderClosed(string state)
        {
            return state == MM_PlacingD.ReceivedState ||
                (MM_PlacingD.UseYozmOrderColumns && HasSelectedOrderBeenClosed());
        }

        private bool HasSelectedOrderBeenClosed()
        {
            return gvPlacingMaster.GetFocusedDataRow()?.Field<DateTime?>("CloseDate") > new DateTime(1900, 1, 1);
        }

        private void OpenSelectedOrderReceipt()
        {
            DataRow order = gvPlacingMaster.GetFocusedDataRow();
            if (order == null || IsSelectedOrderClosed(order["State"].ToString()))
            {
                MessageBox.Show("입고할 잔량이 남은 발주를 선택하세요.", "입고등록");
                return;
            }

            using (var form = new frmMM_PurchaseD_TypeA("Insert", "", order["MmOrderNo"].ToString()))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                    DisplayData();
            }
        }

        //엑셀변환
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvPlacingMaster.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }
        }

		private void cmdPO_Click(object sender, EventArgs e)
		{
            DataRow drMaster = gvPlacingMaster.GetFocusedDataRow();
            if (drMaster == null) return;

            string rptNm = WrGlobal.ProcOption["MT"]["PlacingOrder_Report"];
            try
            {
                if(rptNm == "") throw new Exception($"레포트를 찾을 수 없습니다.");

                // 현재 실행중인 Assembly 가져오기
                Assembly assembly = Assembly.GetExecutingAssembly();

                // reportName 과 일치하는 타입 찾기
                Type type = assembly.GetTypes()
                    .FirstOrDefault(t => t.Name.Equals(rptNm, StringComparison.OrdinalIgnoreCase)
                                      && typeof(XtraReport).IsAssignableFrom(t));

                if (type == null)
                    throw new Exception($"레포트를 찾을 수 없습니다.");

                // 인스턴스 생성
                XtraReport report = (XtraReport)Activator.CreateInstance(type, drMaster); //파라미터가 다를 경우 분기 필요

                using (ReportPrintTool reportTool = new ReportPrintTool(report))
                {
                    reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
                    reportTool.ShowPreviewDialog();
                }
            }
            catch
            {
                XtraMessageBox.Show("레포트를 찾을 수 없습니다.");
            }
        }
        
        //닫기
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
