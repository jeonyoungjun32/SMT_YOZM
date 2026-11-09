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
using SmartMES.Models;
using SmartMES.Common;
using SmartMES.View.Lookup;
using SmartCommon.Common;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using SmartMES.Models.PM;
using SmartMES.Models.MM;

namespace SmartMES.View.MM
{
    public partial class frmMM_PurchaseD_BarCode : DevExpress.XtraEditors.XtraForm
    {
        string crud = "";
        string StoNo = "";
        string MmOrderNo = "";

        clsCode code = new clsCode();
        clsPurchase purchase = new clsPurchase();
        DataRowView master;
        string FirstYn = "Y";   //CellValue Change 때문에
        bool isNew = false;

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        string[] ItemDiv = new string[] { "40", "50" };

        public frmMM_PurchaseD_BarCode()
        {
            InitializeComponent();
        }

        public frmMM_PurchaseD_BarCode(string _crud, string _MmOrderNo)
        {
            InitializeComponent();

            this.crud = _crud;
            this.MmOrderNo = _MmOrderNo;
        }

        private void frmMM_PurchaseD_Load(object sender, EventArgs e)
        {
            searchCustomer.Properties.DataSource = code.smtLookUp("거래처", "매입", "");
            searchCustomer.Properties.DisplayMember = "거래처명";
            searchCustomer.Properties.ValueMember = "거래처코드";

            writeIdLookUpEdit.Properties.DataSource = code.GetUser();
            writeIdLookUpEdit.Properties.DisplayMember = "사용자명";
            writeIdLookUpEdit.Properties.ValueMember = "ID";

            code.LookupCategory(ref vatTypeLookUpEdit, "C010");         // 과세구분

            DataView warehouseList = code.GetWarehouseList();

            if(Database.DbName == "GJY")
            {
                warehouseList.RowFilter = "창고타입 NOT IN ('100')";
            }

            houseIDLookUpEdit.Properties.DataSource = warehouseList;
            houseIDLookUpEdit.Properties.DisplayMember = "창고명";
            houseIDLookUpEdit.Properties.ValueMember = "창고코드";

            //창고타입은 UI에서 제거
            var view = houseIDLookUpEdit.Properties.View;
            if (view.Columns["창고타입"] != null)
            {
                view.Columns["창고타입"].Visible = false;
            }

            houseIDLookUpEdit.EditValue = "W01";

            string[] ItemType = { "40", "50" };   //원자재
            code.Get_Material(ref ItemCodeSearchLookUpEdit, ItemType);

            SetGrid(gvDetail);

            try
            {
                if(crud == "BarCode")
                {
                    // 1) 마스터 조회
                    MM_PlacingM pm = new MM_PlacingM();
                    pm.MmOrderNo = MmOrderNo;
                    DataTable dtMaster = pm.Select_MM_PlacingM_MmOrderNo();

                    if (dtMaster.Rows.Count == 0)
                    {
                        XtraMessageBox.Show("해당 발주번호가 존재하지 않습니다.");
                        this.Close();
                        return;
                    }

                    DataRow row = dtMaster.Rows[0];

                    stoNoTextEdit.EditValue = "";
                    stoDateEdit.DateTime = DateTime.Now.Date;
                    houseIDLookUpEdit.EditValue = "S0001";
                    searchCustomer.EditValue = row["CustCode"];
                    writeIdLookUpEdit.EditValue = WrGlobal.LoginID;
                    vatTypeLookUpEdit.EditValue = row["VatType"];
                    vatRateSpinEdit.EditValue = row["VatRate"];
                    master = dtMaster.DefaultView[0];

                    // 2) 디테일 불러오기
                    MM_PlacingD pd = new MM_PlacingD();
                    pd.MmOrderNo = MmOrderNo;
                    DataTable dtDetail = pd.Select_MM_PlacingD_MmOrderNo();

                    DataTable dtGrid = new DataTable();
                    dtGrid.Columns.Add("StoNo");
                    dtGrid.Columns.Add("ItemCode");
                    dtGrid.Columns.Add("ItemName");
                    dtGrid.Columns.Add("Size");
                    dtGrid.Columns.Add("Alias");
                    dtGrid.Columns.Add("UnitCode");
                    dtGrid.Columns.Add("Qty", typeof(decimal));
                    dtGrid.Columns.Add("BadQty", typeof(decimal));
                    dtGrid.Columns.Add("UnitPrice", typeof(decimal));
                    dtGrid.Columns.Add("Price", typeof(decimal));
                    dtGrid.Columns.Add("VatPrice", typeof(decimal));
                    dtGrid.Columns.Add("TotalPrice", typeof(decimal));
                    dtGrid.Columns.Add("VatType");
                    dtGrid.Columns.Add("VatRate", typeof(decimal));
                    dtGrid.Columns.Add("MmOrderNo");
                    dtGrid.Columns.Add("MmOrderSeq");
                    dtGrid.Columns.Add("LotNo");
                    dtGrid.Columns.Add("State");

                    // 3) 조회 결과를 DataTable에 바로 추가
                    foreach (DataRow detail in dtDetail.Rows)
                    {
                        DataRow newRow = dtGrid.NewRow();
                        newRow["StoNo"] = "";
                        newRow["ItemCode"] = detail["ItemCode"];
                        newRow["ItemName"] = detail["ItemName"];
                        newRow["Size"] = detail["Size"];
                        newRow["Alias"] = detail["Alias"];
                        newRow["UnitCode"] = detail["UnitCode"];
                        newRow["Qty"] = detail["Qty"];
                        newRow["BadQty"] = 0;
                        newRow["UnitPrice"] = detail["UnitPrice"];
                        newRow["Price"] = detail["Price"];
                        newRow["VatPrice"] = detail["VatPrice"];
                        newRow["TotalPrice"] = detail["TotalPrice"];
                        newRow["VatType"] = "10";
                        newRow["VatRate"] = 10;
                        newRow["MmOrderNo"] = detail["MmOrderNo"];
                        newRow["MmOrderSeq"] = detail["MmOrderSeq"];
                        newRow["LotNo"] = "";
                        newRow["State"] = "대기";
                        dtGrid.Rows.Add(newRow);
                    }

                    // 4) 그리드에 바로 DataSource 지정
                    gcDetail.DataSource = dtGrid;
                    gvDetail.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BadQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

        }

        private void gvDetail_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (isNew) return;

            if (e.Column.FieldName == "Qty" || e.Column.FieldName == "UnitPrice")
            {
                DataRow row = gvDetail.GetDataRow(e.RowHandle);
                if (row == null) return;

                if (row["Qty"] == DBNull.Value || string.IsNullOrEmpty(row["Qty"].ToString()))
                    row["Qty"] = 0m;

                if (row["UnitPrice"] == DBNull.Value || string.IsNullOrEmpty(row["UnitPrice"].ToString()))
                    row["UnitPrice"] = 0m;

                decimal qty = Convert.ToDecimal(row["Qty"]);
                decimal badQty = Convert.ToDecimal(row["BadQty"]);
                decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);
                decimal vatRate = Convert.ToDecimal(vatRateSpinEdit.EditValue);

                row["Price"] = (qty - badQty) * unitPrice;
                row["VatPrice"] = Math.Round((decimal)row["Price"] * vatRate / 100m);
                row["TotalPrice"] = (decimal)row["Price"] + (decimal)row["VatPrice"];
            }

        }

        private void CalculatePriceByVat()
        {
            DataTable dt = gvDetail.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row in dt.Rows)
            {
                CalculatePriceByPrice(row);
            }
        }

        private void CalculatePriceByUnitPriceOrQty(DataRow row)
        {
            if (row["Qty"] == DBNull.Value || string.IsNullOrEmpty(row["Qty"].ToString()))
                row["Qty"] = 0m;

            if (row["UnitPrice"] == DBNull.Value || string.IsNullOrEmpty(row["UnitPrice"].ToString()))
                row["UnitPrice"] = 0m;

            decimal qty = Convert.ToDecimal(row["Qty"]);
            decimal badQty = Convert.ToDecimal(row["BadQty"]);
            decimal unitPrice = Convert.ToDecimal(row["UnitPrice"]);
            decimal vatRate = Convert.ToDecimal(vatRateSpinEdit.EditValue);

            row["Price"] = (qty - badQty) * unitPrice;
            row["VatPrice"] = Math.Round((decimal)row["Price"] * vatRate / 100m);
            row["TotalPrice"] = (decimal)row["Price"] + (decimal)row["VatPrice"];
        }

        private void CalculatePriceByPrice(DataRow row)
        {
            if (row == null) return;

            decimal price = Convert.ToDecimal(row["Price"]);
            decimal vatRate = Convert.ToDecimal(vatRateSpinEdit.EditValue);

            row["VatPrice"] = Math.Round(price * vatRate / 100m);
            row["TotalPrice"] = price + Convert.ToDecimal(row["VatPrice"]);
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

        private void gvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = gvDetail.GetDataRow(e.RowHandle);
            if (row == null) return;

            if (e.Column.FieldName == "UnitPrice" || e.Column.FieldName == "Qty")
            {
                CalculatePriceByUnitPriceOrQty(row);
            }
            else if (e.Column.FieldName == "Price")
            {
                CalculatePriceByPrice(row);
            }
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int rowHandle = gvDetail.FocusedRowHandle;
            if (rowHandle < 0) return;

            DataRow row = gvDetail.GetDataRow(rowHandle);
            if (row == null) return;

            if (row["State"]?.ToString() != "대기")
            {
                MessageBox.Show("대기 상태의 항목만 삭제할 수 있습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gvDetail.DeleteRow(rowHandle);
        }

        // 입고 품목 등록
        private void btnInsert_Click(object sender, EventArgs e)
        {
            //if ((ItemCodeSearchLookUpEdit.EditValue ?? "").ToString() == "")
            //{
            //    MessageBox.Show("입고 품목을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //string selectedCode = ItemCodeSearchLookUpEdit.EditValue.ToString();

            //// DataTable 확보
            //DataTable dt = gvDetail.DataSource as DataTable;
            //if (dt == null)
            //{
            //    // 그리드에 연결된 테이블이 없으면 새로 생성
            //    dt = new DataTable();
            //    dt.Columns.Add("StoNo", typeof(string));
            //    dt.Columns.Add("State", typeof(string));
            //    dt.Columns.Add("StoSeq", typeof(int));
            //    dt.Columns.Add("ItemCode", typeof(string));
            //    dt.Columns.Add("ItemName", typeof(string));
            //    dt.Columns.Add("Alias", typeof(string));
            //    dt.Columns.Add("ItemClass1", typeof(string));
            //    dt.Columns.Add("ItemClass2", typeof(string));
            //    dt.Columns.Add("ItemClass3", typeof(string));
            //    dt.Columns.Add("Size", typeof(string));
            //    dt.Columns.Add("UnitWeight", typeof(string));
            //    dt.Columns.Add("Texture", typeof(string));
            //    dt.Columns.Add("Type", typeof(string));
            //    dt.Columns.Add("ColorCode", typeof(string));
            //    dt.Columns.Add("UnitCode", typeof(string));
            //    dt.Columns.Add("Qty", typeof(decimal));
            //    dt.Columns.Add("BadQty", typeof(decimal));
            //    dt.Columns.Add("UnitPrice", typeof(decimal));
            //    dt.Columns.Add("Price", typeof(decimal));
            //    dt.Columns.Add("VatPrice", typeof(decimal));
            //    dt.Columns.Add("TotalPrice", typeof(decimal));
            //    dt.Columns.Add("CreId", typeof(string));
            //    dt.Columns.Add("CreIP", typeof(string));
            //    dt.Columns.Add("CreDt", typeof(DateTime));

            //    gvDetail.GridControl.DataSource = dt; // 그리드에 연결
            //}

            //// 이미 등록된 품목 체크
            //foreach (DataRow row in dt.Rows)
            //{
            //    if (row["ItemCode"].ToString() == selectedCode)
            //    {
            //        MessageBox.Show("이미 등록된 품목입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}

            //try
            //{
            //    DataRow materialDrv = code.GetMaterialInfo(selectedCode).Row;

            //    // 새 행 생성
            //    DataRow newRow = dt.NewRow();
            //    newRow["StoNo"] = "";
            //    newRow["State"] = "대기";

            //    // 최대 시퀀스 계산
            //    int maxSeq = 0;
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        int seq = row["StoSeq"] == DBNull.Value ? 0 : Convert.ToInt32(row["StoSeq"]);
            //        if (seq > maxSeq) maxSeq = seq;
            //    }
            //    newRow["StoSeq"] = maxSeq + 1;

            //    // 품목 정보
            //    newRow["ItemCode"] = materialDrv["ItemCode"];
            //    newRow["ItemName"] = materialDrv["ItemName"];
            //    if (gvDetail.Columns.ColumnByFieldName("Alias") != null) newRow["Alias"] = materialDrv["Alias"];
            //    if (gvDetail.Columns.ColumnByFieldName("ItemClass1") != null) newRow["ItemClass1"] = materialDrv["ItemClass1"];
            //    if (gvDetail.Columns.ColumnByFieldName("ItemClass2") != null) newRow["ItemClass2"] = materialDrv["ItemClass2"];
            //    if (gvDetail.Columns.ColumnByFieldName("ItemClass3") != null) newRow["ItemClass3"] = materialDrv["ItemClass3"];
            //    newRow["Size"] = materialDrv["Size"];
            //    if (gvDetail.Columns.ColumnByFieldName("UnitWeight") != null) newRow["UnitWeight"] = materialDrv["UnitWeight"];
            //    if (gvDetail.Columns.ColumnByFieldName("Texture") != null) newRow["Texture"] = materialDrv["Texture"];
            //    if (gvDetail.Columns.ColumnByFieldName("Type") != null) newRow["Type"] = materialDrv["Type"];
            //    if (gvDetail.Columns.ColumnByFieldName("ColorCode") != null) newRow["ColorCode"] = materialDrv["ColorCode"];
            //    newRow["UnitCode"] = materialDrv["UnitCode"];

            //    // 수량/단가
            //    decimal qty = Convert.ToDecimal(qtySpinEdit.EditValue ?? 1);
            //    newRow["Qty"] = qty;
            //    newRow["BadQty"] = 0;

            //    // 금액 계산
            //    decimal price = qty * Convert.ToDecimal(newRow["UnitPrice"]);
            //    decimal vatRate = Convert.ToDecimal(vatRateSpinEdit.EditValue);
            //    newRow["Price"] = price;
            //    newRow["VatPrice"] = Math.Round(price * vatRate / 100m);
            //    newRow["TotalPrice"] = price + Convert.ToDecimal(newRow["VatPrice"]);

            //    // 생성 정보
            //    newRow["CreId"] = WrGlobal.LoginID;
            //    newRow["CreIP"] = WrGlobal.ClientHostName;
            //    newRow["CreDt"] = DateTime.Now;

            //    // DataTable에 추가
            //    dt.Rows.Add(newRow);

            //    gvDetail.RefreshData();
            //    gvDetail.BestFitColumns();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //finally
            //{
            //    ItemCodeSearchLookUpEdit.EditValue = null;
            //    qtySpinEdit.EditValue = 1;
            //}
        }



        private void cmdSave_Click(object sender, EventArgs e)
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
            if (crud == "BarCode")
            {
                stoNoTextEdit.EditValue = "PO" + code.GetAutoNumber("입고", (DateTime)stoDateEdit.EditValue);
            }

            decimal price = 0;
            decimal vatPrice = 0;
            decimal totalPrice = 0;

            if (master != null)
            {
                string state = master["State"] == DBNull.Value ? "" : master["State"].ToString();
                if (state == "대기")
                    master["State"] = "완료";
            }

            //DataTable dtDetail = gvDetail.DataSource as DataTable;

            if (gvDetail.DataRowCount == 0)
            {
                MessageBox.Show("입고 품목이 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < gvDetail.DataRowCount; i++)
            {
                DataRow row = gvDetail.GetDataRow(i);

                row["StoNo"] = stoNoTextEdit.EditValue.ToString();
                row["VatType"] = master["VatType"];
                row["VatRate"] = master["VatRate"];

                price += Convert.ToDecimal(row["Price"] ?? 0);
                vatPrice += Convert.ToDecimal(row["VatPrice"] ?? 0);
                totalPrice += Convert.ToDecimal(row["TotalPrice"] ?? 0);

                if (row.RowState == DataRowState.Detached || row.RowState == DataRowState.Added)
                {
                    row["State"] = "완료";
                    row["LotNo"] = "M" + code.GetAutoNumber("자재LOT", stoDateEdit.DateTime);
                }
            }

            master["Price"] = price;
            master["VatPrice"] = vatPrice;
            master["TotalPrice"] = totalPrice;
            master["State"] = "완료";

            try
            {
                // 입고 마스터 등록
                MM_PurchaseM pm = new MM_PurchaseM();
                pm.StoNo = stoNoTextEdit.EditValue.ToString();
                pm.StoDate = stoDateEdit.DateTime.ToString();
                pm.CustCode = searchCustomer.EditValue?.ToString() ?? "";
                pm.WriteID = writeIdLookUpEdit.EditValue?.ToString() ?? WrGlobal.LoginID;
                pm.Price = Convert.ToDecimal(master["Price"]);
                pm.VatPrice = Convert.ToDecimal(master["VatPrice"]);
                pm.TotalPrice = Convert.ToDecimal(master["TotalPrice"]);
                pm.VatType = master["VatType"].ToString();
                pm.VatRate = master["VatRate"].ConvertInt_Null_To_Zero();
                pm.HouseID = houseIDLookUpEdit.EditValue?.ToString() ?? "";
                pm.Type = "1"; // 1.매입 2.반품 3.외주
                pm.CustomerUser = customerUserTextEdit.EditValue?.ToString() ?? "";
                pm.Remark = remarkTextEdit1.EditValue?.ToString() ?? "";
                pm.State = master["State"].ToString();

                if (!pm.Insert_MM_PurchaseM())
                {
                    XtraMessageBox.Show("Master 저장 중 에러가 발생하였습니다.");
                    return;
                }

                // 입고 디테일 등록
                for (int i = 0; i < gvDetail.RowCount; i++)
                {
                    DataRow row = gvDetail.GetDataRow(i);
                    if (row == null) continue;

                    MM_PurchaseD pd = new MM_PurchaseD();
                    pd.StoNo = stoNoTextEdit.EditValue.ToString();
                    pd.StoSeq = row["MmOrderSeq"].ConvertInt_Null_To_One(); // 발주 순번 그대로
                    pd.ItemCode = row["ItemCode"].ConvertString_Null_To_Empty();
                    pd.UnitCode = row["UnitCode"].ConvertString_Null_To_Empty();
                    pd.Qty = row["Qty"].ConvertFloat_Null_To_Zero();
                    pd.BadQty = row["BadQty"].ConvertFloat_Null_To_Zero();
                    pd.UnitPrice = row["UnitPrice"].ConvertDecimal_Null_To_Zero();
                    pd.Price = row["Price"].ConvertDecimal_Null_To_Zero();
                    pd.VatPrice = row["VatPrice"].ConvertDecimal_Null_To_Zero();
                    pd.TotalPrice = row["TotalPrice"].ConvertDecimal_Null_To_Zero();
                    pd.VatType = master["VatType"].ToString();
                    pd.VatRate =master["VatRate"].ConvertInt_Null_To_Zero();
                    pd.MmOrderNo = row["MmOrderNo"].ConvertString_Null_To_Empty();
                    pd.MmOrderSeq = row["MmOrderSeq"].ConvertInt_Null_To_One();
                    pd.State = row["State"].ConvertString_Null_To_Empty();
                    pd.LotNo = row["LotNo"].ConvertString_Null_To_Empty();

                    if (!pd.Insert_MM_PurchaseD())
                    {
                        XtraMessageBox.Show("Detail 저장 중 에러가 발생하였습니다.");
                        return;
                    }
                }

                // 발주정보 업데이트
                purchase.UpdatePlacingInQtyByPurchase(stoNoTextEdit.EditValue.ToString());

                // 재고 등록
                purchase.MakeStockByPurchase(stoNoTextEdit.EditValue.ToString());

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		private void btn_Close_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}