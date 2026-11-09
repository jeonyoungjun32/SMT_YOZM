using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SmartPOP.Models;
using SmartPOP.Common;
using SmartPOP.View.Lookup;
using SMT_MES.Controls;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Models.MM;

namespace SmartPOP.Views.MM
{
    public partial class Frm_MM_PurchaseDetail : DevExpress.XtraEditors.XtraForm
    {
        string crud = "";
        clsCode code = new clsCode();
        MM_PurchaseD purchase = new MM_PurchaseD();
        DataRowView master;

        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();


        string[] CustType = new string[] { "10", "40" };
        string[] ItemDiv = new string[] { "40", "50", "60" };

        string FirstYn = "Y";   //CellValue Change 때문에
        bool isNew = false;

        public Frm_MM_PurchaseDetail()
        {
            InitializeComponent();
        }

        public Frm_MM_PurchaseDetail(string _crud, string _stoNo)
        {
            InitializeComponent();

            this.crud = _crud;

            //거래처
            LupCust.Properties.DataSource = code.GetCustomerList(CustType, true);
            LupCust.Properties.DisplayMember = "거래처명";
            LupCust.Properties.ValueMember = "거래처코드";
            LupCust.EditValue = "ALL";
            rpo.Set_POP_SearchLup(LupCust);

            //담당자
            writeIdLookUpEdit.Properties.DataSource = code.GetUser();
            writeIdLookUpEdit.Properties.DisplayMember = "사용자명";
            writeIdLookUpEdit.Properties.ValueMember = "ID";
            rpo.Set_POP_SearchLup(writeIdLookUpEdit);

            //부가세율
            code.LookupCategory(ref vatTypeLookUpEdit, "C010");         // 과세구분
            vatTypeLookUpEdit.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);

            //창고
            houseIDLookUpEdit.Properties.DataSource = code.GetWarehouseList();
            houseIDLookUpEdit.Properties.DisplayMember = "창고명";
            houseIDLookUpEdit.Properties.ValueMember = "창고코드";
            houseIDLookUpEdit.EditValue = "S0001";
            rpo.Set_POP_SearchLup(houseIDLookUpEdit);

            //원자재
            code.Get_Material(ref materialCodeSearchLookUpEdit, ItemDiv);
            rpo.Set_POP_SearchLup(materialCodeSearchLookUpEdit);

            //입고일자
            rpo.Set_POP_Dtp(stoDateEdit);

            // 그리드 생성
            SetGrid(gvDetail);
        }

        private void frmMM_PurchaseD_Load(object sender, EventArgs e)
        {
            FirstYn = "N";
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
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            RepositoryItemButtonEdit btnDel = new RepositoryItemButtonEdit();
            btnDel.Name = "btnDelete";
            btnDel.Buttons.Clear();
            btnDel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            btnDel.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            btnDel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnDel.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDelete_ButtonClick);

            //Column 생성
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

            //
            gv.Columns["colDelete"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["Qty"]);
            grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            grid.Column_AllowEdit(gv.Columns["Price"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["colDelete"]);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Qty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BadQty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TFloat);
        }

        private void gvDetail_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (isNew) return;

            if (e.Column.FieldName == "Qty" || e.Column.FieldName == "UnitPrice")
            {
                DataRowView drv = (DataRowView)MM_PurchaseDBindingSource.Current;
                if ((drv["Qty"] ?? "").ToString() == "") drv["Qty"] = 0;
                if ((drv["UnitPrice"] ?? "").ToString() == "") drv["UnitPrice"] = 0;

                drv["Price"] = (Convert.ToDecimal(drv["Qty"]) - Convert.ToDecimal(drv["BadQty"])) * Convert.ToDecimal(drv["UnitPrice"]);
                drv["VatPrice"] = Math.Round(Convert.ToDecimal(drv["Price"]) * Convert.ToDecimal(vatRateSpinEdit.EditValue) / 100);

                drv["TotalPrice"] = Convert.ToDecimal(drv["Price"]) + Convert.ToDecimal(drv["VatPrice"]);
            }
        }

        private void CalculatePriceByVat()
        {
            foreach (DataRowView drv in MM_PurchaseDBindingSource.List)
            {
                CalculatePriceByPrice();
            }
        }

        private void CalculatePriceByUnitPriceOrQty()
        {
            DataRowView drv = (DataRowView)MM_PurchaseDBindingSource.Current;

            if ((drv["Qty"] ?? "").ToString() == "") drv["Qty"] = 0;
            if ((drv["UnitPrice"] ?? "").ToString() == "") drv["UnitPrice"] = 0;

            drv["Price"] = (Convert.ToDecimal(drv["Qty"]) - Convert.ToDecimal(drv["BadQty"])) * Convert.ToDecimal(drv["UnitPrice"]);
            drv["VatPrice"] = Math.Round(Convert.ToDecimal(drv["Price"]) * Convert.ToDecimal(vatRateSpinEdit.EditValue) / 100);
            drv["TotalPrice"] = Convert.ToDecimal(drv["Price"]) + Convert.ToDecimal(drv["VatPrice"]);
        }

        private void CalculatePriceByPrice()
        {
            DataRowView drv = (DataRowView)MM_PurchaseDBindingSource.Current;

            drv["VatPrice"] = Math.Round(Convert.ToDecimal(drv["Price"]) * Convert.ToDecimal(vatRateSpinEdit.EditValue) / 100);
            drv["TotalPrice"] = Convert.ToDecimal(drv["Price"]) + Convert.ToDecimal(drv["VatPrice"]);
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

        // 저장
        private void cmdSave_ElementClick(object sender, EventArgs e)
        {
            if ((LupCust.EditValue ?? "").ToString() == "")
            {
                cls_MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((houseIDLookUpEdit.EditValue ?? "").ToString() == "")
            {
                cls_MessageBox.Show("창고를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //문서번호 생성 
            if (crud == "Insert")
            {
                master["StoNo"] = "PO" + code.GetAutoNumber("입고", (DateTime)master["StoDate"]);
            }

            decimal price = 0;
            decimal vatPrice = 0;
            decimal totalPrice = 0;

            if ((master["State"] ?? "").ToString() == "대기")
            {
                master["State"] = "완료";
            }

            foreach (DataRowView item in MM_PurchaseDBindingSource)
            {
                item["StoNo"] = master["StoNo"];
                item["VatType"] = master["VatType"];
                item["VatRate"] = master["VatRate"];

                price += Convert.ToDecimal((item["Price"] ?? 0));
                vatPrice += Convert.ToDecimal((item["VatPrice"] ?? 0));
                totalPrice += Convert.ToDecimal((item["TotalPrice"] ?? 0));

                if (item.Row.RowState == DataRowState.Added)
                {
                    item["State"] = "완료";
                    item["LotNo"] = "M" + code.GetAutoNumber("자재LOT", stoDateEdit.DateTime);
                }
            }

            master["Price"] = price;
            master["VatPrice"] = vatPrice;
            master["TotalPrice"] = totalPrice;

            master["State"] = "완료";
            master["ModId"] = WrGlobal.LoginID;
            master["ModIP"] = WrGlobal.ClientHostName;
            master["ModDt"] = DateTime.Now;

            try
            {
                //purchaseMasterBindingSource.EndEdit();
                //purchaseMasterTableAdapter.Update(purchaseDataSet.PurchaseMaster);

                //MM_PurchaseDBindingSource.EndEdit();
                //MM_PurchaseDTableAdapter.Update(purchaseDataSet.MM_PurchaseD);

            // 발주정보 업데이트
            purchase.UpdatePlacingInQtyByPurchase(master["StoNo"].ToString());
            // 재고 등록
            purchase.MakeStockByPurchase(master["StoNo"].ToString());

            cls_MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message);
            }
        }

        private void cmdClose_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            this.Close();
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
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                DataRowView delDrv = (DataRowView)MM_PurchaseDBindingSource.Current;

                if ((delDrv["State"] ?? "").ToString() != "대기")
                {
                    cls_MessageBox.Show("대기 상태의 항목만 삭제할 수 있습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //MM_PurchaseDBindingSource.RemoveCurrent();

            }
        }

        private void cmdOrderSelect_ElementClick(object sender, EventArgs e)
        {
            if ((LupCust.EditValue ?? "").ToString() == "")
            {
                cls_MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Frm_LookUpPlacing form = new Frm_LookUpPlacing("PurchaseMaster", (LupCust.EditValue ?? "").ToString());
            form.onSetData += new Frm_LookUpPlacing.SetData(PlacingSetData);
            form.ShowDialog();
        }

        private void PlacingSetData(DataRowView drv)
        {
            isNew = true;

            DataRowView newItem;

            try
            {
                int maxSeq = 0;

                foreach (DataRowView item in MM_PurchaseDBindingSource)
                {
                    int seq = item["StoSeq"] == DBNull.Value ? 0 : (int)item["StoSeq"];
                    if (maxSeq < seq) maxSeq = seq;

                    if (drv["MmOrderNo"] != DBNull.Value)
                    {
                        if (drv["MmOrderNo"].ToString() == item["MmOrderNo"].ToString() && (int)drv["MmOrderSeq"] == (int)item["MmOrderSeq"])
                        {
                            cls_MessageBox.Show(string.Format("동일한 발주 품목이 존재합니다.({0}-{1})", item["MmOrderNo"].ToString(), item["MmOrderSeq"]), "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                MM_PurchaseDBindingSource.AddNew();
                newItem = (DataRowView)MM_PurchaseDBindingSource.Current;

                newItem["CorpId"] = WrGlobal.CorpID;
                newItem["StoNo"] = master["StoNo"];
                newItem["State"] = "대기";

                newItem["StoSeq"] = ++maxSeq;

                newItem["MaterialCode"] = drv["MaterialCode"];
                newItem["MaterialName"] = drv["MaterialName"];
                newItem["Size"] = drv["Size"];
                newItem["UnitCode"] = drv["UnitCode"];

                master["CustCode"] = drv["CustCode"];

                newItem["Qty"] = drv["StoQty"];
                newItem["BadQty"] = 0;
                newItem["UnitPrice"] = drv["UnitPrice"];

                newItem["Price"] = Convert.ToDecimal(newItem["Qty"]) * Convert.ToDecimal(newItem["UnitPrice"]);
                newItem["VatPrice"] = Convert.ToDecimal(newItem["Price"]) * Convert.ToDecimal(drv["VatRate"]) / 100;
                newItem["TotalPrice"] = Convert.ToDecimal(newItem["Price"]) + Convert.ToDecimal(newItem["VatPrice"]);

                newItem["VatType"] = drv["VatType"];
                newItem["VatRate"] = drv["VatRate"];

                newItem["MmOrderNo"] = drv["MmOrderNo"];
                newItem["MmOrderSeq"] = drv["MmOrderSeq"];

                newItem["CreId"] = WrGlobal.LoginID;
                newItem["CreIP"] = WrGlobal.ClientHostName;
                newItem["CreDt"] = DateTime.Now;

                MM_PurchaseDBindingSource.EndEdit();

                LupCust.ReadOnly = true;
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isNew = false;
            }
        }
        // 입고 품목 등록
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if ((materialCodeSearchLookUpEdit.EditValue ?? "").ToString() == "")
            {
                cls_MessageBox.Show("입고 품목을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataRowView drv in MM_PurchaseDBindingSource)
            {
                if ((materialCodeSearchLookUpEdit.EditValue ?? "").ToString() == drv["MaterialCode"].ToString())
                {
                    cls_MessageBox.Show("이미 등록된 품목 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            isNew = true;
            DataRowView newItem;
            DataRowView materialDrv = code.GetMaterialInfo(materialCodeSearchLookUpEdit.EditValue.ToString());

            try
            {
                MM_PurchaseDBindingSource.AddNew();
                newItem = (DataRowView)MM_PurchaseDBindingSource.Current;

                newItem["CorpId"] = WrGlobal.CorpID;
                newItem["StoNo"] = master["StoNo"];
                newItem["State"] = "대기";

                int maxSeq = 0;

                foreach (DataRowView item in MM_PurchaseDBindingSource)
                {
                    int seq = item["StoSeq"] == DBNull.Value ? 0 : (int)item["StoSeq"];
                    if (maxSeq < seq) maxSeq = seq;
                }
                newItem["StoSeq"] = ++maxSeq;

                newItem["MaterialCode"] = materialDrv["MaterialCode"];
                newItem["MaterialName"] = materialDrv["MaterialName"];
                if (gvDetail.Columns.ColumnByFieldName("Alias") != null) newItem["Alias"] = materialDrv["Alias"];
                if (gvDetail.Columns.ColumnByFieldName("MaterialClass1") != null) newItem["MaterialClass1"] = materialDrv["MaterialClass1"];
                if (gvDetail.Columns.ColumnByFieldName("MaterialClass2") != null) newItem["MaterialClass2"] = materialDrv["MaterialClass2"];
                if (gvDetail.Columns.ColumnByFieldName("MaterialClass3") != null) newItem["MaterialClass3"] = materialDrv["MaterialClass3"];
                newItem["Size"] = materialDrv["Size"];
                if (gvDetail.Columns.ColumnByFieldName("UnitWeight") != null) newItem["UnitWeight"] = materialDrv["UnitWeight"];
                if (gvDetail.Columns.ColumnByFieldName("Texture") != null) newItem["Texture"] = materialDrv["Texture"];
                if (gvDetail.Columns.ColumnByFieldName("Type") != null) newItem["Type"] = materialDrv["Type"];
                if (gvDetail.Columns.ColumnByFieldName("ColorCode") != null) newItem["ColorCode"] = materialDrv["ColorCode"];
                newItem["UnitCode"] = materialDrv["UnitCode"];

                newItem["Qty"] = qtySpinEdit.EditValue;
                newItem["BadQty"] = 0;

                if (materialDrv["PurchasePrice"] == DBNull.Value || materialDrv["PurchasePrice"] == null)
                {
                    newItem["UnitPrice"] = 0;
                }
                else
                {
                    newItem["UnitPrice"] = materialDrv["PurchasePrice"];
                }

                newItem["Price"] = Convert.ToDecimal(newItem["Qty"]) * Convert.ToDecimal(newItem["UnitPrice"]);
                newItem["VatPrice"] = Math.Round(Convert.ToDecimal(newItem["Price"]) * Convert.ToDecimal(vatRateSpinEdit.EditValue) / 100);
                newItem["TotalPrice"] = Convert.ToDecimal(newItem["Price"]) + Convert.ToDecimal(newItem["VatPrice"]);

                newItem["CreId"] = WrGlobal.LoginID;
                newItem["CreIP"] = WrGlobal.ClientHostName;
                newItem["CreDt"] = DateTime.Now;

                MM_PurchaseDBindingSource.EndEdit();
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                materialCodeSearchLookUpEdit.EditValue = null;
                qtySpinEdit.EditValue = 1;
                isNew = false;
            }
        }

        private void purchaseMasterBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            //this.Validate();
            //this.purchaseMasterBindingSource.EndEdit();
            //this.tableAdapterManager.UpdateAll(this.purchaseDataSet);

        }
    }
}