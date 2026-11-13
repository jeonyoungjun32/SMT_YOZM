using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.DragEngine;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MM;
using SmartMES.View.BM.Popup;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SmartMES.View.MM
{
	public partial class frmMM_PurchaseD_TypeA : DevExpress.XtraEditors.XtraForm
    {
        string crud = "";
        string StoNo = "";
        private readonly string sourceOrderNo;

        clsCode code = new clsCode();
        clsPurchase purchase = new clsPurchase();

        MM_PurchaseM purchaseM = new MM_PurchaseM();
        MM_PurchaseD purchaseD = new MM_PurchaseD();

        DataRow drMaster = null;

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        string[] CustType = new string[] { "10", "40" };

        public frmMM_PurchaseD_TypeA()
        {
            InitializeComponent();
        }

        public frmMM_PurchaseD_TypeA(string _crud, string _stoNo, string orderNo = null)
        {
            InitializeComponent();

            this.crud = _crud;
            this.StoNo = _stoNo;
            sourceOrderNo = orderNo;

            Set_Lup();
            SetGrid(gvDetail);

        }

        private void frmMM_PurchaseD_TypeA_Load(object sender, EventArgs e)
        {
            if (crud == "Update")
            {
                purchaseM.StoNo = this.StoNo;
                drMaster = purchaseM.Select_MM_PurchaseM_ByStoNo();

                DisplayMaster();
            }
            else
            {
                purchaseM.StoNo = "";
            }

            DisplayDetail();
            if (!string.IsNullOrEmpty(sourceOrderNo))
            {
                TxtMmOrderNo.Text = sourceOrderNo;
                AddDetail_MmOrder();
            }
        }

		#region 디자인
		private void Set_Lup()
		{
			searchCustomer.Properties.DataSource = code.Get_CustomerDt(true, CustType);
            searchCustomer.Properties.DisplayMember = "내용";
            searchCustomer.Properties.ValueMember = "코드";

            writeIdLookUpEdit.Properties.DataSource = code.GetUser();
            writeIdLookUpEdit.Properties.DisplayMember = "사용자명";
            writeIdLookUpEdit.Properties.ValueMember = "ID";

            code.LookupCategory(ref vatTypeLookUpEdit, "C010");         // 과세구분

            DataView warehouseList = code.GetWarehouseList();

            houseIDLookUpEdit.Properties.DataSource = warehouseList;
            houseIDLookUpEdit.Properties.DisplayMember = "창고명";
            houseIDLookUpEdit.Properties.ValueMember = "창고코드";

            //창고타입은 UI에서 제거
            var view = houseIDLookUpEdit.Properties.View;
            if (view.Columns["창고타입"] != null)
            {
                view.Columns["창고타입"].Visible = false;
            }

            stoDateEdit.EditValue = DateTime.Now.Date;
            houseIDLookUpEdit.EditValue = "S0001";
            vatTypeLookUpEdit.EditValue = "10";
            vatRateSpinEdit.EditValue = 10;
            writeIdLookUpEdit.EditValue = WrGlobal.LoginID;
        }

        private void SetGrid(GridView gv)
		{
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsView.ShowGroupPanel = false;

            //gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            //RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", String.Join(",", ItemDiv), "", ""), "품목코드", "품명");
            //RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명")/*;*/
            RepositoryItemButtonEdit btnDel = new RepositoryItemButtonEdit();
            btnDel.Name = "btnDelete";
            btnDel.Buttons.Clear();
            btnDel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            btnDel.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            btnDel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnDel.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDelete_ButtonClick);

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "입고번호", "StoNo");
            grid.Create_GvColumn(gv, "순번", "StoSeq");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "불량", "BadQty");
            grid.Create_GvColumn(gv, "단위", "UnitCode");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "발주번호", "MmOrderNo");
            grid.Create_GvColumn(gv, "순번", "MmOrderSeq");
            grid.Create_GvColumn(gv, "삭제", "colDelete");

            //Repository
            gv.Columns["colDelete"].ColumnEdit = btnDel;
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //
            gv.Columns["StoNo"].Visible = false;
            gv.Columns["BadQty"].Visible = false;

            //
            gv.Columns["colDelete"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["Qty"]);
            grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            grid.Column_AllowEdit(gv.Columns["Price"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["colDelete"]);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BadQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

			gv.CellValueChanged += Gv_CellValueChanged;
        }

		private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);
            string value = e.Value.ConvertString_Null_To_Empty();

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            if (fieldName == "UnitPrice" || fieldName == "Qty" || fieldName == "Price")
            {
                CalculateRow(drChanged);
            }
        }

		#endregion

		#region 조회

		private void DisplayMaster()
        {
            MM_PurchaseM pm = new MM_PurchaseM();
            pm.StoNo = StoNo;

            DataRow dr = pm.Select_MM_PurchaseM_ByStoNo();

            if (dr != null)
            {
                stoNoTextEdit.EditValue = dr["StoNo"];
                stoDateEdit.EditValue = dr["StoDate"];
                houseIDLookUpEdit.EditValue = dr["HouseID"];
                stateTextEdit.EditValue = dr["State"];
                searchCustomer.EditValue = dr["CustCode"];
                vatTypeLookUpEdit.EditValue = dr["VatType"];
                vatRateSpinEdit.EditValue = dr["VatRate"];
                customerUserTextEdit.EditValue = dr["CustomerUser"];
                writeIdLookUpEdit.EditValue = dr["WriteID"];
                remarkTextEdit.EditValue = dr["Remark"];
            }
        }

        private void DisplayDetail()
        {
            MM_PurchaseD pd = new MM_PurchaseD();
            pd.StoNo = StoNo;

            gcDetail.DataSource = pd.Select_MM_PurchaseD_StoNo();
            gvDetail.BestFitColumns();
        }

        #endregion

        #region 이벤트

        private void CalculatePriceByVat()
        {
            DataTable dtDetail = gcDetail.DataSource as DataTable;
            if (dtDetail == null) return;

            foreach (DataRow dr in dtDetail.Rows)
            {
                CalculateRow(dr);
            }
        }

        private void CalculateRow(DataRow dr)
        {
            decimal qty = dr["Qty"].ConvertDecimal_Null_To_Zero();
            decimal badQty = dr["BadQty"].ConvertDecimal_Null_To_Zero();
            decimal unitPrice = dr["UnitPrice"].ConvertDecimal_Null_To_Zero();
            decimal vatRate = vatRateSpinEdit.EditValue.ConvertDecimal_Null_To_Zero();

            decimal price = (qty - badQty) * unitPrice;
            decimal vat = Math.Round(price * vatRate / 100);

            dr["Price"] = price;
            dr["VatPrice"] = vat;
            dr["TotalPrice"] = price + vat;
        }

        private void vatRateSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePriceByVat();
        }

        private void gvDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;
        }

        private void vatTypeLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (vatTypeLookUpEdit.EditValue.ToString() == "10")
            {
                vatRateSpinEdit.EditValue = 10;
            }
            else
            {
                vatRateSpinEdit.EditValue = 0;
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                DataRow dr = gvDetail.GetFocusedDataRow();

                string status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    gvDetail.DeleteRow(gvDetail.FocusedRowHandle);
                }
                else
                {
                    status = status == "삭제" ? "" : "삭제";
                    gvDetail.SetFocusedRowCellValue("Status", status);
                }
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void cmdOrderSelect_Click(object sender, EventArgs e)
		{
            if ((searchCustomer.EditValue ?? "").ToString() == "")
            {
                MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Frm_PlacingItem form = new Frm_PlacingItem((searchCustomer.EditValue ?? "").ToString(), "10");
            form.onSetData += new Frm_PlacingItem.SetData(ProductSetData_Placing);
            form.ShowDialog();
        }

		private void cmdSave_Click(object sender, EventArgs e)
		{
            gvDetail.PostEditor();
            gvDetail.UpdateCurrentRow();


            if ((searchCustomer.EditValue ?? "").ToString() == "")
            {
                MessageBox.Show("거래처를 선택하세요.", "필수 값 미입력", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((houseIDLookUpEdit.EditValue ?? "").ToString() == "")
            {
                MessageBox.Show("창고를 선택하세요.", "필수 값 미입력", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = gcDetail.DataSource as DataTable;

            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("상세내역이 없습니다.", "필수 값 미입력", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MM_PlacingD.UseYozmOrderColumns && !ValidateYozmReceipt(dt))
                return;

            //마스터 저장
            string stoNo = "";
            string state = "";

            if(this.crud == "Insert")
			{
                stoNo = "PO" + code.GetAutoNumber("입고", stoDateEdit.DateTime);
                state = "완료";
            }
			else
			{
                stoNo = drMaster["StoNo"].ToString();
                state = drMaster["State"].ConvertString_Null_To_Empty();
            }

            purchaseM.StoNo = stoNo;
            purchaseM.StoDate = stoDateEdit.DateTime.ToString("d");
            purchaseM.CustCode = searchCustomer.EditValue?.ToString();
            purchaseM.WriteID = writeIdLookUpEdit.EditValue.ConvertString_Null_To_Empty();
            purchaseM.VatType = vatTypeLookUpEdit.EditValue.ConvertString_Null_To_Empty();
            purchaseM.VatRate = vatRateSpinEdit.EditValue.ConvertInt_Null_To_One();
            purchaseM.HouseID = houseIDLookUpEdit.EditValue?.ToString();
            purchaseM.Type = "1";
            purchaseM.Remark = remarkTextEdit.EditValue.ConvertString_Null_To_Empty();
            purchaseM.State = state;

            if (this.crud == "Insert")
            {
                if (!purchaseM.Insert_MM_PurchaseM())
                {
                    MessageBox.Show("저장 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (!purchaseM.Update_MM_PurchaseM())
                {
                    MessageBox.Show("저장 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //디테일 저장
            foreach (DataRow item in dt.Rows)
            {
                string status = item["Status"].ConvertString_Null_To_Empty();

                purchaseD.StoNo = purchaseM.StoNo;
                purchaseD.StoSeq = item["StoSeq"].ConvertInt_Null_To_Zero();
                purchaseD.ItemCode = item["ItemCode"].ConvertString_Null_To_Empty();
                purchaseD.UnitCode = item["UnitCode"].ConvertString_Null_To_Empty();

                purchaseD.Qty = item["Qty"].ConvertFloat_Null_To_Zero();
                purchaseD.BadQty = item["BadQty"].ConvertFloat_Null_To_Zero();

                purchaseD.UnitPrice = item["UnitPrice"].ConvertDecimal_Null_To_Zero();
                purchaseD.Price = item["Price"].ConvertDecimal_Null_To_Zero();
                purchaseD.VatPrice = item["VatPrice"].ConvertDecimal_Null_To_Zero();
                purchaseD.TotalPrice = item["TotalPrice"].ConvertDecimal_Null_To_Zero();

                purchaseD.VatType = purchaseM.VatType;
                purchaseD.VatRate = purchaseM.VatRate;
                purchaseD.WriteID = writeIdLookUpEdit.EditValue.ConvertString_Null_To_Empty();

                purchaseD.MmOrderNo = item["MmOrderNo"].ConvertString_Null_To_Empty();
                purchaseD.MmOrderSeq = item["MmOrderSeq"].ConvertInt_Null_To_Zero();
                purchaseD.Remark = item["Remark"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    purchaseD.State = "완료";
                    purchaseD.LotNo = "M" + code.GetAutoNumber("자재LOT", stoDateEdit.DateTime);

                    if (!purchaseD.Insert_MM_PurchaseD())
                    {
                        MessageBox.Show("디테일 저장 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (status == "수정")
                {
                    if (!purchaseD.Update_MM_PurchaseD())
                    {
                        MessageBox.Show("디테일 수정 중 오류가 발생했습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (status == "삭제")
                {
                    purchaseD.Delete_MM_PurchaseD();
                }
            }

            //마스터 금액 업데이트
            purchaseM.Update_MM_PurchaseM_Price_DetailSum(stoNo);

            // 발주정보 업데이트
            purchase.UpdatePlacingInQtyByPurchase(stoNo);
            // 재고 등록
            purchase.MakeStockByPurchase(stoNo);

            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (!string.IsNullOrEmpty(sourceOrderNo))
                DialogResult = DialogResult.OK;
            else
                Close();
        }

        private bool ValidateYozmReceipt(DataTable details)
        {
            try
            {
                var rows = details.Select("ISNULL(Status, '') <> '삭제'");
                
                if (rows.Length == 0 || rows.Any(row => row["Qty"].ConvertDecimal_Null_To_Zero() <= 0))
                    throw new InvalidOperationException("입고 품목과 0보다 큰 입고수량을 입력하세요.");

                string customer = searchCustomer.EditValue.ConvertString_Null_To_Empty();
                
                foreach (var order in rows.Where(row => !string.IsNullOrEmpty(row["MmOrderNo"].ToString())).GroupBy(row => row["MmOrderNo"].ToString()))
                {
                    using (DataTable limits = MM_PlacingD.SelectReceiptLimits(order.Key, StoNo))
                    {
                        foreach (var line in order.GroupBy(row => row["MmOrderSeq"].ConvertInt_Null_To_Zero()))
                        {
                            DataRow limit = limits.AsEnumerable().FirstOrDefault(row => row.Field<int>("MmOrderSeq") == line.Key);
                            
                            if (limit == null || limit["CustCode"].ToString() != customer ||
                                line.Any(row => row["ItemCode"].ToString() != limit["ItemCode"].ToString()))
                                throw new InvalidOperationException($"발주 {order.Key}의 거래처 또는 품목이 일치하지 않습니다.");
                                
                            if (limit.Field<DateTime?>("CloseDate") > new DateTime(1900, 1, 1))
                                throw new InvalidOperationException($"종료된 발주 {order.Key}는 입고할 수 없습니다.");
                                
                            if (line.Sum(row => row["Qty"].ConvertDecimal_Null_To_Zero()) >
                                limit["AvailableQty"].ConvertDecimal_Null_To_Zero())
                                throw new InvalidOperationException($"발주 {order.Key}의 {line.Key}번 품목이 입고 가능 수량을 초과했습니다.");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "입고 확인");
                return false;
            }
        }

        #region 품목선택
        private void btn_Material_Click(object sender, EventArgs e)
		{
			if (searchCustomer.EditValue.ConvertString_Null_To_Empty() == "")
			{
				MessageBox.Show("거래처를 먼저 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

            Frm_SelectItem form = new Frm_SelectItem(searchCustomer.EditValue.ConvertString_Null_To_Empty(), "10"); //매입
			form.onSetData += new Frm_SelectItem.SetData(ProductSetData);
			form.ShowDialog();
		}

		private void ProductSetData(DataRow drv)
        {
            DataRow newItem;

            try
            {
                DataTable dt = gvDetail.GridControl.DataSource as DataTable;
                if (dt == null) return;

                newItem = dt.NewRow();

                newItem["CorpId"] = WrGlobal.CorpID;
                newItem["Status"] = "신규";
                newItem["State"] = "대기";

                int maxSeq = 0;

                foreach (DataRow item in dt.Rows)
                {
                    int seq = item["StoSeq"] == DBNull.Value ? 0 : (int)item["StoSeq"];
                    if (maxSeq < seq) maxSeq = seq;
                }
                newItem["StoSeq"] = ++maxSeq;

                CM_Item cm = new CM_Item();
                cm.ItemCode = clsCommon.getString(drv["ItemCode"]);

                DataRow drItem = cm.Select_Item_RowInfo();

                if (dt.Columns.Contains("ItemCode")) newItem["ItemCode"] = clsCommon.getString(drv["ItemCode"]);
                newItem["ItemName"] = drItem["ItemName"];
                if (dt.Columns.Contains("Alias")) newItem["Alias"] = clsCommon.getString(drItem["Alias"]);
                if (dt.Columns.Contains("ItemClass1")) newItem["ItemClass1"] = clsCommon.getString(drItem["ItemClass1"]);
                if (dt.Columns.Contains("ItemClass2")) newItem["ItemClass2"] = clsCommon.getString(drItem["ItemClass2"]);
                if (dt.Columns.Contains("ItemClass3")) newItem["ItemClass3"] = clsCommon.getString(drItem["ItemClass3"]);
                if (dt.Columns.Contains("Size")) newItem["Size"] = clsCommon.getString(drItem["Size"]);
                if (dt.Columns.Contains("UnitWeight")) newItem["UnitWeight"] = drItem["UnitWeight"].ConvertInt_Null_To_Zero();
                if (dt.Columns.Contains("Texture")) newItem["Texture"] = clsCommon.getString(drItem["Texture"]);
                if (dt.Columns.Contains("Type")) newItem["Type"] = clsCommon.getString(drItem["Type"]);
                if (dt.Columns.Contains("UnitCode")) newItem["UnitCode"] = clsCommon.getString(drItem["UnitCode"]);
                if (dt.Columns.Contains("ColorCode")) newItem["ColorCode"] = clsCommon.getString(drItem["ColorCode"]);
                if (dt.Columns.Contains("ShipToCode")) newItem["ShipToCode"] = clsCommon.getString(drv["ShipToCode"]);

                decimal orderQty = drv["OrderQty"].ConvertDecimal_Null_To_Zero();

                newItem["Qty"] = orderQty;

                string custCode = searchCustomer.EditValue.ConvertString_Null_To_Empty();
                string ItemCode = drv["ItemCode"].ToString();

                CM_ItemPrice cmPrice = new CM_ItemPrice();
                cmPrice.PriceType = "10";
                cmPrice.ApplyDate = DateTime.Now.ToString("d");

                decimal price = cmPrice.Get_MaterialPrice(custCode, ItemCode);
                newItem["UnitPrice"] = price;

                newItem["Price"] = price * orderQty;
                newItem["VatPrice"] = price * orderQty * (decimal)0.1;
                newItem["TotalPrice"] = price * orderQty + (price * orderQty * (decimal)0.1);

                newItem["CreId"] = WrGlobal.LoginID;
                newItem["CreIP"] = WrGlobal.ClientHostName;
                newItem["CreDt"] = DateTime.Now;

                dt.Rows.Add(newItem);

                gvDetail.FocusedRowHandle = gvDetail.RowCount - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            gvDetail.BestFitColumns();
        }

        private void ProductSetData_Placing(DataRow drv)
        {
            DataRow newItem;

            try
            {
                DataTable dt = gvDetail.GridControl.DataSource as DataTable;
                if (dt == null) return;

                newItem = dt.NewRow();

                newItem["CorpId"] = WrGlobal.CorpID;
                newItem["Status"] = "신규";
                newItem["State"] = "대기";

                int maxSeq = 0;

                foreach (DataRow item in dt.Rows)
                {
                    int seq = item["StoSeq"] == DBNull.Value ? 0 : (int)item["StoSeq"];
                    if (maxSeq < seq) maxSeq = seq;
                }
                newItem["StoSeq"] = ++maxSeq;

                CM_Item cm = new CM_Item();
                cm.ItemCode = clsCommon.getString(drv["ItemCode"]);

                DataRow drItem = cm.Select_Item_RowInfo();

                if (dt.Columns.Contains("ItemCode")) newItem["ItemCode"] = clsCommon.getString(drv["ItemCode"]);
                newItem["ItemName"] = drItem["ItemName"];
                if (dt.Columns.Contains("Alias")) newItem["Alias"] = clsCommon.getString(drItem["Alias"]);
                if (dt.Columns.Contains("ItemClass1")) newItem["ItemClass1"] = clsCommon.getString(drItem["ItemClass1"]);
                if (dt.Columns.Contains("ItemClass2")) newItem["ItemClass2"] = clsCommon.getString(drItem["ItemClass2"]);
                if (dt.Columns.Contains("ItemClass3")) newItem["ItemClass3"] = clsCommon.getString(drItem["ItemClass3"]);
                if (dt.Columns.Contains("Size")) newItem["Size"] = clsCommon.getString(drItem["Size"]);
                if (dt.Columns.Contains("UnitWeight")) newItem["UnitWeight"] = drItem["UnitWeight"].ConvertInt_Null_To_Zero();
                if (dt.Columns.Contains("Texture")) newItem["Texture"] = clsCommon.getString(drItem["Texture"]);
                if (dt.Columns.Contains("Type")) newItem["Type"] = clsCommon.getString(drItem["Type"]);
                if (dt.Columns.Contains("UnitCode")) newItem["UnitCode"] = clsCommon.getString(drItem["UnitCode"]);
                if (dt.Columns.Contains("ColorCode")) newItem["ColorCode"] = clsCommon.getString(drItem["ColorCode"]);
                if (dt.Columns.Contains("ShipToCode")) newItem["ShipToCode"] = clsCommon.getString(drv["ShipToCode"]);
                if (dt.Columns.Contains("MmOrderNo")) newItem["MmOrderNo"] = clsCommon.getString(drv["MmOrderNo"]);
                if (dt.Columns.Contains("MmOrderSeq"))
                    newItem["MmOrderSeq"] = clsCommon.getString(drv["MmOrderSeq"]);

                decimal qty = drv["InQty"].ConvertDecimal_Null_To_Zero();
                newItem["Qty"] = qty;

                if (MM_PlacingD.UseYozmOrderColumns && dt.Rows.Count == 0)
                {
                    vatTypeLookUpEdit.EditValue = drv["VatType"];
                    vatRateSpinEdit.EditValue = drv["VatRate"];
                }

                string custCode = searchCustomer.EditValue.ConvertString_Null_To_Empty();
                string ItemCode = drv["ItemCode"].ToString();

                CM_ItemPrice cmPrice = new CM_ItemPrice();
                cmPrice.PriceType = "10";
                cmPrice.ApplyDate = DateTime.Now.ToString("d");

                decimal unitPrice = MM_PlacingD.UseYozmOrderColumns ? drv["UnitPrice"].ConvertDecimal_Null_To_Zero()
                    : cmPrice.Get_MaterialPrice(custCode, ItemCode);
                newItem["UnitPrice"] = unitPrice;

                decimal price = unitPrice * qty;
                decimal vatPrice = MM_PlacingD.UseYozmOrderColumns
                    ? Math.Round(price * vatRateSpinEdit.EditValue.ConvertDecimal_Null_To_Zero() / 100)
                    : price * 0.1m;

                newItem["Price"] = price;
                newItem["VatPrice"] = vatPrice;
                newItem["TotalPrice"] = price + vatPrice;

                newItem["CreId"] = WrGlobal.LoginID;
                newItem["CreIP"] = WrGlobal.ClientHostName;
                newItem["CreDt"] = DateTime.Now;

                dt.Rows.Add(newItem);

                gvDetail.FocusedRowHandle = gvDetail.RowCount - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            gvDetail.BestFitColumns();
        }

		#endregion

		private void TxtMmOrderNo_KeyDown(object sender, KeyEventArgs e)
		{
            if (e.KeyCode == Keys.Enter)
            {
                //해당 발주번호 디테일 건중 발주잔량 남은 애들 하단 리스트에 추가
                AddDetail_MmOrder();

                e.Handled = true;          // 키 이벤트 처리 완료
                e.SuppressKeyPress = true; // 엔터 소리(삑) 방지
            }
        }

        private void AddDetail_MmOrder()
		{
            MM_PlacingD pd = new MM_PlacingD();
            pd.MmOrderNo = TxtMmOrderNo.Text;

            DataTable dtPlacing = pd.SelectRemainList();
            if (dtPlacing == null || dtPlacing.Rows.Count == 0)
            {
                if (MM_PlacingD.UseYozmOrderColumns)
                    MessageBox.Show("입고할 잔량이 없거나 종료된 발주입니다.", "입고등록");
                return;
            }

            try
            {
                DataTable dt = gvDetail.GridControl.DataSource as DataTable;
                if (dt == null) return;

                if (MM_PlacingD.UseYozmOrderColumns)
                {
                    DataRow order = dtPlacing.Rows[0];
                    string customer = searchCustomer.EditValue.ConvertString_Null_To_Empty();
                    if (!string.IsNullOrEmpty(customer) && customer != order["CustCode"].ToString())
                    {
                        MessageBox.Show("선택한 거래처와 발주의 거래처가 다릅니다.", "입고등록");
                        return;
                    }
                    if (dt.Rows.Count == 0)
                    {
                        searchCustomer.EditValue = order["CustCode"];
                        vatTypeLookUpEdit.EditValue = order["VatType"];
                        vatRateSpinEdit.EditValue = order["VatRate"];
                        var houses = dtPlacing.AsEnumerable().Select(row => row["ReceiveHouseId"].ToString())
                            .Where(value => value.Length > 0).Distinct().ToArray();
                        if (houses.Length == 1)
                            houseIDLookUpEdit.EditValue = houses[0];
                    }
                }

                DataRow newItem;

                foreach (DataRow dr in dtPlacing.Rows)
				{
                    if (MM_PlacingD.UseYozmOrderColumns && dt.Select("ISNULL(Status, '') <> '삭제'").Any(row =>
                        row["MmOrderNo"].Equals(dr["MmOrderNo"]) && row["MmOrderSeq"].Equals(dr["MmOrderSeq"])))
                        continue;

                    newItem = dt.NewRow();

                    newItem["CorpId"] = WrGlobal.CorpID;
                    newItem["Status"] = "신규";
                    newItem["State"] = "대기";

                    if (searchCustomer.EditValue.ConvertString_Null_To_Empty() == "") searchCustomer.EditValue = dr["CustCode"];

                    int maxSeq = 0;

                    foreach (DataRow item in dt.Rows)
                    {
                        int seq = item["StoSeq"] == DBNull.Value ? 0 : (int)item["StoSeq"];
                        if (maxSeq < seq) maxSeq = seq;
                    }
                    newItem["StoSeq"] = ++maxSeq;

                    CM_Item cm = new CM_Item();
                    cm.ItemCode = clsCommon.getString(dr["ItemCode"]);

                    DataRow drItem = cm.Select_Item_RowInfo();

                    if (dt.Columns.Contains("ItemCode")) newItem["ItemCode"] = clsCommon.getString(dr["ItemCode"]);
                    newItem["ItemName"] = drItem["ItemName"];

                    if (dt.Columns.Contains("Alias")) newItem["Alias"] = clsCommon.getString(drItem["Alias"]);
                    if (dt.Columns.Contains("ItemClass1")) newItem["ItemClass1"] = clsCommon.getString(drItem["ItemClass1"]);
                    if (dt.Columns.Contains("ItemClass2")) newItem["ItemClass2"] = clsCommon.getString(drItem["ItemClass2"]);
                    if (dt.Columns.Contains("ItemClass3")) newItem["ItemClass3"] = clsCommon.getString(drItem["ItemClass3"]);
                    if (dt.Columns.Contains("Size")) newItem["Size"] = clsCommon.getString(drItem["Size"]);
                    if (dt.Columns.Contains("UnitWeight")) newItem["UnitWeight"] = drItem["UnitWeight"].ConvertInt_Null_To_Zero();
                    if (dt.Columns.Contains("Texture")) newItem["Texture"] = clsCommon.getString(drItem["Texture"]);
                    if (dt.Columns.Contains("Type")) newItem["Type"] = clsCommon.getString(drItem["Type"]);
                    if (dt.Columns.Contains("UnitCode")) newItem["UnitCode"] = clsCommon.getString(drItem["UnitCode"]);
                    if (dt.Columns.Contains("ColorCode")) newItem["ColorCode"] = clsCommon.getString(drItem["ColorCode"]);

                    newItem["MmOrderNo"] = clsCommon.getString(dr["MmOrderNo"]);
                    newItem["MmOrderSeq"] = clsCommon.getString(dr["MmOrderSeq"]);

                    decimal remainQty = dr["RemainQty"].ConvertDecimal_Null_To_Zero();

                    newItem["Qty"] = remainQty;

                    string custCode = searchCustomer.EditValue.ConvertString_Null_To_Empty();
                    string ItemCode = dr["ItemCode"].ToString();

                    CM_ItemPrice cmPrice = new CM_ItemPrice();
                    cmPrice.PriceType = "10";
                    cmPrice.ApplyDate = DateTime.Now.ToString("d");

                    decimal unitPrice = MM_PlacingD.UseYozmOrderColumns ? dr["UnitPrice"].ConvertDecimal_Null_To_Zero()
                        : cmPrice.Get_MaterialPrice(custCode, ItemCode);
                    decimal price = unitPrice * remainQty;
                    decimal vatPrice = MM_PlacingD.UseYozmOrderColumns
                        ? Math.Round(price * vatRateSpinEdit.EditValue.ConvertDecimal_Null_To_Zero() / 100)
                        : price * 0.1m;

                    newItem["UnitPrice"] = unitPrice;

                    newItem["Price"] = price;
                    newItem["VatPrice"] = vatPrice;
                    newItem["TotalPrice"] = price + vatPrice;

                    newItem["CreId"] = WrGlobal.LoginID;
                    newItem["CreIP"] = WrGlobal.ClientHostName;
                    newItem["CreDt"] = DateTime.Now;

                    dt.Rows.Add(newItem);

                }

                gvDetail.FocusedRowHandle = gvDetail.RowCount - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            gvDetail.BestFitColumns();
        }
	}
}
