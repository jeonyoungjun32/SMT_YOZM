using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SmartMES.View.BM.Popup;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_SdOrderD : DevExpress.XtraEditors.XtraForm
    {
        string crud = "";
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        string[] CustDiv = new string[] { "20", "30", "40" };

        Database db = new Database();
        string query = "";

        string FirstYn = "Y";   //CellValue Change 때문에
        bool isNew = false;

        public Frm_BM_SdOrderD()
        {
            InitializeComponent();
        }

        public Frm_BM_SdOrderD(string _crud, string _sdOrderNo)
        {
            InitializeComponent();

            this.crud = _crud;
            TxtSdOrderNo.Text = _sdOrderNo;

            SetLup();
            SetMaster();
            SetDetail(gvDetail);

            SearchDetail();
        }

        private void SetLup()
        {
            code.LookupCustomer(WrGlobal.CorpID, "매출", false, "", ref searchCustomer);

            code.LookupWareHouse(WrGlobal.CorpID, ref houseIDLookUpEdit);   //창고
            houseIDLookUpEdit.EditValue = crud == "Insert" ? "S0002" : null;  //완제품창고 디폴트 값 설정
            
            searchSalesMan.Properties.DataSource = code.GetDataTableUser(false, null, "N");
            searchSalesMan.Properties.DisplayMember = "명칭";
            searchSalesMan.Properties.ValueMember = "코드";

            code.LookupCategory(ref searchVAT, "C010");                 // 과세구분

            CM_CateItem cci = new CM_CateItem();

            LupOrderType.Properties.DataSource = cci.SelectClassItemLookUp(WrGlobal.CorpID, "S020");
            LupOrderType.Properties.DisplayMember = "분류명";
            LupOrderType.Properties.ValueMember = "분류코드";

        }

        private void SetMaster()
        {
            string sdorderNo = TxtSdOrderNo.Text;

            DataRowView master = null;

            if (crud == "Update")
            {
                BM_SdOrderM sm = new BM_SdOrderM();
                master = sm.Select_BM_SdOrderMByNo(WrGlobal.CorpID, sdorderNo);

                TxtSdOrderNo.EditValue = master["SdOrderNo"];
                stateTextEdit.EditValue = master["State"];
                startDateDateEdit.EditValue = master["ObtainDate"];
                deliveryDateDateEdit.EditValue = master["DeliveryDate"];
                searchCustomer.EditValue = master["SoldToCode"];
                searchSalesMan.EditValue = master["SalesMan"];
                houseIDLookUpEdit.EditValue = master["HouseID"];
                searchVAT.EditValue = master["VatType"];    //VAT구분
                vATSpinEdit.EditValue = master["VatRate"];    //부과세율
                LupOrderType.EditValue = master["OrderType"];
                remarkTextEdit.EditValue = master["Remark"];
                TxtOutState.EditValue = master["OutState"];
                SpinPrice.EditValue = master["Price"];
                SpinVatPrice.EditValue = master["VatPrice"];
                SpinTotalPrice.EditValue = master["TotalPrice"];
            }
            else
            {
                ////신규데이터 입력

                TxtSdOrderNo.EditValue = "";
                stateTextEdit.EditValue = "대기";
                TxtOutState.EditValue = "대기";
                startDateDateEdit.EditValue = DateTime.Now.Date;
                deliveryDateDateEdit.EditValue = DateTime.Now.Date.AddDays(1);
                searchVAT.EditValue = "10";    //VAT구분
                vATSpinEdit.EditValue = 10;    //부과세율
                LupOrderType.EditValue = "10";
            }
        }

		private void SetDetail(GridView gv)
		{
			gvDetail.Columns.Clear();


			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = false;

            //삭제 버튼
            RepositoryItemButtonEdit btnDelete = new RepositoryItemButtonEdit();
            btnDelete.Name = "btnDelete";
            btnDelete.AutoHeight = false;
            btnDelete.Buttons.Clear();
            btnDelete.Buttons.AddRange(new EditorButton[] {new EditorButton(ButtonPredefines.Delete)});
            btnDelete.ButtonsStyle = BorderStyles.HotFlat;
            btnDelete.TextEditStyle = TextEditStyles.HideTextEditor;
            btnDelete.ButtonClick += new ButtonPressedEventHandler(this.btnDelete_ButtonClick);

            //거래처
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(CustDiv), "거래처코드", "거래처명");
			//단위
			RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
			//품목분류1,2,3
			RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

			grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
			grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

			grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

			grid.Create_GvColumn(gv, "수량", "Qty");
			grid.Create_GvColumn(gv, "단가", "UnitPrice"  );
			grid.Create_GvColumn(gv, "공급가", "Price"    );
			grid.Create_GvColumn(gv, "부가세", "VatPrice" );
            grid.Create_GvColumn(gv, "합계", "TotalPrice");
			grid.Create_GvColumn(gv, "비고", "Remark");
			grid.Create_GvColumn(gv, "작업계획", "OrderStatus");
			grid.Create_GvColumn(gv, "상태", "State");
			grid.Create_GvColumn(gv, "발주상태", "PlacingStatus");
			grid.Create_GvColumn(gv, "출고처", "ShipToCode", lupCust);
            grid.Create_GvColumn(gv, "삭제", "colDelete", btnDelete);


			//Repository
			gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
			gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
			gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
			gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

			//ALLOW EDIT
			grid.Column_AllowEdit(gv.Columns["Size"]);
			grid.Column_AllowEdit(gv.Columns["UnitCode"]);
			grid.Column_AllowEdit(gv.Columns["Qty"]);
			grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
			grid.Column_AllowEdit(gv.Columns["Price"]);
			grid.Column_AllowEdit(gv.Columns["VatPrice"]);
			grid.Column_AllowEdit(gv.Columns["TotalPrice"]);
			grid.Column_AllowEdit(gv.Columns["ShipToCode"]);
			grid.Column_AllowEdit(gv.Columns["colDelete"]);

			//Format
			grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //Fixed
            gv.Columns["colDelete"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //Summary
            grid.SetCol_Summary(gv, "TotalPrice");

            //visible
            gv.Columns["SdOrderNo"].Visible = false;

			grid.Set_Navigator(gvDetail);
			gv.ValidatingEditor += Gv_ValidatingEditor;
		}

		private void Gv_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            string col = gv.FocusedColumn.FieldName;
            string value = e.Value.ToString() ?? "";

            if (col == "Qty" || col == "UnitPrice")
            {
                if (value.Length > 8)
                {
                    e.Valid = false;
                    e.ErrorText = "최대 8자리까지 입력할 수 있습니다.";
                }
            }
        }

        private void SearchDetail()
        {
            string sdorderNo = TxtSdOrderNo.Text;

            BM_SdOrderD sd = new BM_SdOrderD();
            gcDetail.DataSource = sd.SelectBM_SdOrderD_ByNo(WrGlobal.CorpID, sdorderNo);
            gvDetail.BestFitColumns();
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                DataRow delDrv = gvDetail.GetFocusedDataRow();

                if ((delDrv["State"] ?? "").ToString() != "대기" && (delDrv["State"] ?? "").ToString() != "진행")
                {
                    MessageBox.Show(string.Format("{0} 상태에서는 삭제할 수 없습니다.", (delDrv["State"] ?? "").ToString()), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((delDrv["OrderStatus"] ?? "").ToString() == "지시")
                {
                    if (MessageBox.Show("작업지시가 내려진 항목입니다.\n 삭제하겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return;
                    }
                }

                if(Convert.ToString(delDrv["SdOrderNo"]) == "")
                {
                    gvDetail.DeleteRow(gvDetail.FocusedRowHandle);
                }
                else
                {
                    BM_SdOrderD sd = new BM_SdOrderD();

                    sd.CorpId = WrGlobal.CorpID;
                    sd.SdOrderNo = delDrv["SdOrderNo"].ConvertString_Null_To_Empty();
                    sd.SdOrderSeq = delDrv["SdOrderSeq"].ConvertInt_Null_To_One();
                    sd.Del_BM_SdOrderD();

                    MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SearchDetail();
                }

               /* //if(master != null)
                {
                    DataTable dt = gcDetail.DataSource as DataTable;


                    SpinTotalPrice.EditValue = 0;

                    foreach (DataRow item in dt.Rows)
                    {
                        SpinTotalPrice.EditValue = Convert.ToDecimal(SpinTotalPrice.EditValue) + Convert.ToDecimal(item["TotalPrice"]);
                    }
                }*/
            }
        }

        private void deliveryDateDateEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (FirstYn == "Y") return;

            //납기일 변경
            for (int rowCnt = 0; rowCnt < gvDetail.RowCount; rowCnt++)
            {
                gvDetail.SetRowCellValue(rowCnt, gvDetail.Columns["DeliveryDate"], deliveryDateDateEdit.EditValue);
            }
        }

        private void frmRegisterObtainOrderDetail_Load(object sender, EventArgs e)
        {
            FirstYn = "N";
        }

        private void gvDetail_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //if (e.RowHandle < 0) return;
            //if (isNew) return;

            //if (e.Column.FieldName == "Qty" || e.Column.FieldName == "UnitPrice")
            //{
            //    DataRowView drv = (DataRowView)BM_SdOrderDBindingSource.Current;
            //    if ((drv["Qty"] ?? "").ToString() == "") drv["Qty"] = 0;
            //    if ((drv["UnitPrice"] ?? "").ToString() == "") drv["UnitPrice"] = 0;

            //    drv["Price"] = Convert.ToDecimal(drv["Qty"]) * Convert.ToDecimal(drv["UnitPrice"]);
            //    drv["VatPrice"] = Math.Round(Convert.ToDecimal(drv["Price"]) * Convert.ToDecimal(vATSpinEdit.EditValue) / 100);

            //    drv["TotalPrice"] = Convert.ToDecimal(drv["Price"]) + Convert.ToDecimal(drv["VatPrice"]);
            //}
                 }

        private void CalculatePriceByUnitPriceOrQty()
        {
            DataTable dt = gcDetail.DataSource as DataTable;

            DataRow dr = gvDetail.GetFocusedDataRow();
            if (dr == null) return;

            if ((dr["Qty"] ?? "").ToString() == "") dr["Qty"] = 0;
            if ((dr["UnitPrice"] ?? "").ToString() == "") dr["UnitPrice"] = 0;

            dr["Price"] = Convert.ToDecimal(dr["Qty"]) * Convert.ToDecimal(dr["UnitPrice"]);
            dr["VatPrice"] = Convert.ToDecimal(dr["Price"]) * Convert.ToDecimal(vATSpinEdit.EditValue) / 100;
            dr["TotalPrice"] = Convert.ToDecimal(dr["Price"]) + Convert.ToDecimal(dr["VatPrice"]);

            //if(master != null)
           /* {
                SpinTotalPrice.EditValue = 0;

                foreach (DataRow item in dt.Rows)
                {
                    SpinTotalPrice.EditValue = Convert.ToDecimal(SpinTotalPrice.EditValue) + Convert.ToDecimal(item["TotalPrice"]);
                }
            }*/
        }

        private void CalculatePriceByPrice()
        {
            DataTable dt = gcDetail.DataSource as DataTable;

            DataRow dr = gvDetail.GetFocusedDataRow();
            if (dr == null) return;

            dr["VatPrice"] = Math.Round(Convert.ToDecimal(dr["Price"]) * Convert.ToDecimal(vATSpinEdit.EditValue) / 100);
            dr["TotalPrice"] = Convert.ToDecimal(dr["Price"]) + Convert.ToDecimal(dr["VatPrice"]);

            //if (master != null)
          /*  {
                SpinTotalPrice.EditValue = 0;

                foreach (DataRow item in dt.Rows)
                {
                    SpinTotalPrice.EditValue = Convert.ToDecimal(SpinTotalPrice.EditValue) + Convert.ToDecimal(item["TotalPrice"]);
                }
            }*/
        }
        private void CalculatePriceByVat()
        {
            DataRow dr = gvDetail.GetFocusedDataRow();
            if (dr == null) return;

            dr["TotalPrice"] = Convert.ToDecimal(dr["Price"]) + Convert.ToDecimal(dr["VatPrice"]);
        }

        private void CalculatePriceByVatRate()
        {
            for (int i = 0; i < gvDetail.RowCount; i++)
            {
                DataRow dr = gvDetail.GetDataRow(i);

                dr["VatPrice"] = Math.Round(Convert.ToDecimal(dr["Price"]) * Convert.ToDecimal(vATSpinEdit.EditValue) / 100);
                dr["TotalPrice"] = Convert.ToDecimal(dr["Price"]) + Convert.ToDecimal(dr["VatPrice"]);
            }
        }

        //세부내역
        private void ProductSetData(DataRow drv)
        {
            isNew = true;

            DataRow newItem;

            try
            {
                DataTable dt = gvDetail.GridControl.DataSource as DataTable;
                if (dt == null) return;

                newItem = dt.NewRow();

                newItem["CorpId"] = WrGlobal.CorpID;
                newItem["SdOrderNo"] = TxtSdOrderNo.Text;
                newItem["OrderStatus"] = "대기";
                newItem["PlacingStatus"] = "발주대기";
                newItem["State"] = "대기";

                int maxSeq = 0;

                foreach (DataRow item in dt.Rows)
                {
                    int seq = item["SdOrderSeq"] == DBNull.Value ? 0 : (int)item["SdOrderSeq"];
                    if (maxSeq < seq) maxSeq = seq;
                }
                newItem["SdOrderSeq"] = ++maxSeq;

                CM_Item cm = new CM_Item();
                cm.ItemCode = clsCommon.getString(drv["ItemCode"]);

                DataRow drItem = cm.Select_Item_RowInfo();

                if(dt.Columns.Contains("ItemCode")) newItem["ItemCode"] = clsCommon.getString(drv["ItemCode"]);
                if(dt.Columns.Contains("ItemName")) newItem["ItemName"] = clsCommon.getString(drItem["ItemName"]);
                if(dt.Columns.Contains("Alias")) newItem["Alias"] = clsCommon.getString(drItem["Alias"]);
                if(dt.Columns.Contains("ItemClass1")) newItem["ItemClass1"] = clsCommon.getString(drItem["ItemClass1"]);
                if(dt.Columns.Contains("ItemClass2")) newItem["ItemClass2"] = clsCommon.getString(drItem["ItemClass2"]);
                if(dt.Columns.Contains("ItemClass3")) newItem["ItemClass3"] = clsCommon.getString(drItem["ItemClass3"]);
                if(dt.Columns.Contains("Size")) newItem["Size"] = clsCommon.getString(drItem["Size"]);
                if(dt.Columns.Contains("UnitWeight")) newItem["UnitWeight"] = drItem["UnitWeight"].ConvertInt_Null_To_Zero();
                if(dt.Columns.Contains("Texture")) newItem["Texture"] = clsCommon.getString(drItem["Texture"]);
                if(dt.Columns.Contains("Type")) newItem["Type"] = clsCommon.getString(drItem["Type"]);
                if(dt.Columns.Contains("UnitCode")) newItem["UnitCode"] = clsCommon.getString(drItem["UnitCode"]);
                if(dt.Columns.Contains("ColorCode")) newItem["ColorCode"] = clsCommon.getString(drItem["ColorCode"]);
                if(dt.Columns.Contains("ShipToCode")) newItem["ShipToCode"] = clsCommon.getString(drv["ShipToCode"]);
                if(dt.Columns.Contains("PackQty")) newItem["PackQty"] = drItem["PackQty"].ConvertInt_Null_To_Zero();

                if (Convert.ToString(deliveryDateDateEdit.EditValue) != "")
                {
                    newItem["DeliveryDate"] = deliveryDateDateEdit.EditValue;
                }

                //newItem["Qty"] = drv["SdOrderQty"];

                newItem["Qty"] = drv["OrderQty"].ConvertDecimal_Null_To_Zero();

                if(dt.Columns.Contains("StockQty")) newItem["StockQty"] = 0;
                if(dt.Columns.Contains("ReqQty")) newItem["ReqQty"] = 0;
                if(dt.Columns.Contains("ProQty")) newItem["ProQty"] = 0;
                if(dt.Columns.Contains("OutQty")) newItem["OutQty"] = 0;

                /*
                //if (drv["SalesPrice"] == DBNull.Value || drv["SalesPrice"] == null)
                if (drv["UnitPrice"] == DBNull.Value || drv["UnitPrice"] == null)
                {
                    newItem["UnitPrice"] = 0;
                }
                */
                //거래처별 단가에 데이터가 있는지 체크
                /*
                else if ((searchCustomer.EditValue ?? "").ToString() != "")
                {
                */
                    string custCode = searchCustomer.EditValue.ConvertString_Null_To_Empty();
                    string ItemCode = drv["ItemCode"].ToString();

                CM_ItemPrice cmPrice = new CM_ItemPrice();
                cmPrice.PriceType = "20";
                cmPrice.ApplyDate = startDateDateEdit.Text;

                decimal price = cmPrice.Get_MaterialPrice(custCode, ItemCode);
                newItem["UnitPrice"] = price;
                    /*
                }
                else
                {
                    newItem["UnitPrice"] = drv["UnitPrice"];
                }
                    */

                newItem["Price"] = 0;
                newItem["VatPrice"] = 0;
                newItem["TotalPrice"] = 0;


                //newItem["PaletteYn"] = "Y";
                //newItem["ErpYn"] = "N";

                newItem["CreId"] = WrGlobal.LoginID;
                newItem["CreIP"] = WrGlobal.ClientHostName;
                newItem["CreDt"] = DateTime.Now;

                dt.Rows.Add(newItem);

                gvDetail.FocusedRowHandle = gvDetail.RowCount - 1;

                CalculatePriceByUnitPriceOrQty();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isNew = false;
            }

            gvDetail.BestFitColumns();
        }

        private void vATSpinEdit_EditValueChanged(object sender, EventArgs e)
        {
            CalculatePriceByVatRate();
        }

        private void gvDetail_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            if ((gvDetail.GetFocusedRowCellValue("State") ?? "").ToString() == "취소")
            {
                gvDetail.OptionsBehavior.Editable = false;
            }
            else
            {
                gvDetail.OptionsBehavior.Editable = true;
            }
        }

        private void btnCancel_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDetail.FocusedRowHandle < 0) return;

            if ((gvDetail.GetFocusedRowCellValue("State") ?? "").ToString() != "대기" && (gvDetail.GetFocusedRowCellValue("State") ?? "").ToString() != "진행")
            {
                MessageBox.Show(string.Format("이미 {0}된 항목 입니다.", (gvDetail.GetFocusedRowCellValue("State") ?? "").ToString()), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow dr = gvDetail.GetFocusedDataRow();

            dr["State"] = "취소";
            dr["Qty"] = 0;
            dr["Price"] = 0;
            dr["VatPrice"] = 0;
            dr["TotalPrice"] = 0;

          /*  SpinTotalPrice.EditValue = 0;

            DataTable dt = gcDetail.DataSource as DataTable;

            foreach (DataRow item in dt.Rows)
            {
                SpinTotalPrice.EditValue = Convert.ToDecimal(SpinTotalPrice.EditValue) + Convert.ToDecimal(item["TotalPrice"]);
            }*/
        }

        private void searchVAT_EditValueChanged(object sender, EventArgs e)
        {
            if ((searchVAT.EditValue ?? "").ToString() == "10")
            {
                vATSpinEdit.EditValue = 10;
            }
            else
            {
                vATSpinEdit.EditValue = 0;
            }
        }

        private void gvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "UnitPrice" || e.Column.FieldName == "Qty")
            {
                CalculatePriceByUnitPriceOrQty();
            }
            else if (e.Column.FieldName == "Price")
            {
                CalculatePriceByPrice();
            }

            else if (e.Column.FieldName == "VatPrice")
            {
                CalculatePriceByVat();
            }
        }

        private void LupOrderType_EditValueChanged(object sender, EventArgs e)
        {
            /*
            LookUpEdit lup = sender as LookUpEdit;

            string value = Convert.ToString(lup.EditValue);

            //수주 아닐 경우 비활성화
            if(value == "10" || value == "30")
            {
                searchCustomer.Enabled = true;
                searchVAT.Enabled = true;
                vATSpinEdit.Enabled = true;
                searchSalesMan.Enabled = true;
                houseIDLookUpEdit.Enabled = true;
                deliveryDateDateEdit.Enabled = true;
            }
            else
            {
                searchCustomer.Enabled = false;
                searchVAT.Enabled = false;
                vATSpinEdit.Enabled = false;
                searchSalesMan.Enabled = false;
                houseIDLookUpEdit.Enabled = false;
                deliveryDateDateEdit.Enabled = false;
            }
            */
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //예외처리
            if (deliveryDateDateEdit.EditValue == DBNull.Value)
            {
                MessageBox.Show("납기일자를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //프로세스 옵션 : 담당자 필수 여부
            if(WrGlobal.ProcOption["BN"]["ChargeEmp_YN"] == "Y")
			{
                string salesMan = searchSalesMan.EditValue.ConvertString_Null_To_Empty();
                if (string.IsNullOrEmpty(salesMan))
                {
                    MessageBox.Show("담당자를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (gvDetail.RowCount == 0)
            {
                MessageBox.Show("품목을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = gvDetail.GridControl.DataSource as DataTable;

            foreach (DataRow item in dt.Rows)
            {
                if (item["Qty"].ToString() == "0")
                {
                    MessageBox.Show("품목중 수량 미입력 항목이 있습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string orderType = Convert.ToString(LupOrderType.EditValue);

            //저장처리
            if (orderType == "10" || orderType == "20")  //수주
            {
                if ((searchCustomer.EditValue ?? "").ToString() == "")
                {
                    MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if ((houseIDLookUpEdit.EditValue ?? "").ToString() == "")
                {
                    MessageBox.Show("창고를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //문서번호 생성
                if (crud == "Insert" || crud == "Copy")
                {
                    //master["SdOrderNo"] = "SO" + code.GetAutoNumber("수주번호", (DateTime)master["ObtainDate"]);
                    //master["State"] = "진행";

                    TxtSdOrderNo.Text = "SO" + code.GetAutoNumber("수주번호", startDateDateEdit.DateTime);
                    stateTextEdit.Text = "진행";
                }

                decimal price = 0;
                decimal vatPrice = 0;
                decimal totalPrice = 0;

                //디테일번호 설정 및 토탈 금액 계산
                foreach (DataRow item in dt.Rows)
                {
                    item["SdOrderNo"] = TxtSdOrderNo.Text;

                    if (dt.Columns.Contains("VatType")) item["VatType"] = searchVAT.EditValue;
                    if (dt.Columns.Contains("VatRate")) item["VatRate"] = vATSpinEdit.EditValue;

                    if ((item["State"] ?? "").ToString() != "취소")
                    {
                        price += Convert.ToDecimal((item["Price"] ?? 0));
                        vatPrice += Convert.ToDecimal((item["VatPrice"] ?? 0));
                        totalPrice += Convert.ToDecimal((item["TotalPrice"] ?? 0));
                    }

                    if ((item["State"] ?? "").ToString() == "대기")
                    {
                        item["State"] = "진행";
                    }
                }

                //수주마스터 저장
                BM_SdOrderM sm = new BM_SdOrderM();
                sm.CorpId = WrGlobal.CorpID;
                sm.SdOrderNo = TxtSdOrderNo.Text;
                sm.SoldToCode = Convert.ToString(searchCustomer.EditValue);
                sm.ObtainDate = Convert.ToString(startDateDateEdit.DateTime.ToString("d"));
                sm.DeliveryDate = Convert.ToString(deliveryDateDateEdit.DateTime.ToString("d"));
                sm.Price = Convert.ToString(price).ConvertDecimal_Null_To_Zero();
                sm.VatPrice = Convert.ToString(vatPrice).ConvertDecimal_Null_To_Zero();
                sm.TotalPrice = Convert.ToString(totalPrice).ConvertDecimal_Null_To_Zero();

                sm.OrderType = Convert.ToString(LupOrderType.EditValue);
                sm.VatType = Convert.ToString(searchVAT.EditValue);
                sm.VatRate = Convert.ToInt32(vATSpinEdit.EditValue);
                sm.SalesMan = Convert.ToString(searchSalesMan.EditValue);
                sm.OutState = Convert.ToString(TxtOutState.EditValue);
                sm.Remark = Convert.ToString(remarkTextEdit.Text);
                sm.HouseID = Convert.ToString(houseIDLookUpEdit.EditValue);
                sm.State = stateTextEdit.Text;

                if (crud == "Insert")
                {
                    sm.Ins_BM_SdOrderM();
                }
                else
                {
                    sm.Upd_BM_SdOrderM();
                }

                //수주디테일 저장
                foreach (DataRow item in dt.Rows)
                {
                    BM_SdOrderD sd = new BM_SdOrderD();

                    sd.CorpId = WrGlobal.CorpID;
                    sd.SdOrderNo = Convert.ToString(item["SdOrderNo"]);
                    sd.SdOrderSeq = Convert.ToInt32(item["SdOrderSeq"]);


                    if (item["Status"].ConvertString_Null_To_Empty() == "삭제")
                    {
                        sd.Del_BM_SdOrderD();
                    }
                    else
                    {
                        sd.ItemCode = Convert.ToString(item["ItemCode"]);
                        sd.Qty = Convert.ToDecimal(item["Qty"]);
                        sd.ShipToCode = Convert.ToString(item["ShipToCode"]);
                        sd.UnitPrice = Convert.ToDecimal(item["UnitPrice"]);
                        sd.Price = Convert.ToDecimal(item["Price"]);
                        sd.VatPrice = Convert.ToDecimal(item["VatPrice"]);
                        sd.TotalPrice = Convert.ToDecimal(item["TotalPrice"]);
                        sd.DeliveryDate = deliveryDateDateEdit.DateTime;
                        sd.OrderStatus = Convert.ToString(item["OrderStatus"]);
                        sd.PlacingStatus = Convert.ToString(item["PlacingStatus"]);
                        sd.Remark = Convert.ToString(item["Remark"]);
                        sd.VatType = Convert.ToString(item["VatType"]);
                        sd.VatRate = Convert.ToInt32(item["VatRate"]);
                        sd.State = Convert.ToString(item["State"]);
                        sd.InsUpd_BM_SdOrderD();
                    }

                }
            }

            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
			if (searchCustomer.EditValue.ConvertString_Null_To_Empty() == "")
			{
				MessageBox.Show("거래처를 먼저 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Frm_BM_SdOrder_Item form = new Frm_BM_SdOrder_Item(searchCustomer.EditValue.ConvertString_Null_To_Empty());
			form.onSetData += new Frm_BM_SdOrder_Item.SetData(ProductSetData);
			form.ShowDialog();
		}
	}
}