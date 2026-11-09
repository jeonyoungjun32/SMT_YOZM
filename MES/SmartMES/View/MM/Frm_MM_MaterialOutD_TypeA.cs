using DevExpress.Pdf.Native.BouncyCastle.Asn1.Ocsp;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SmartMES.View.Lookup;
using SmartMES.View.BM.Popup;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmartMES.View.MM
{
	public partial class Frm_MM_MaterialOutD_TypeA : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        string query;
        Database db = new Database();

        string state = "I";     // 신규 : I, 수정 : U
        string SalesNo = "";
        bool isNew = false;
        string[] CustType = { "10", "40" };
        public Frm_MM_MaterialOutD_TypeA()
        {
            InitializeComponent();
        }

        public Frm_MM_MaterialOutD_TypeA(string state, string salesNo)
        {
            this.state = state;
            this.SalesNo = salesNo;

            InitializeComponent();
        }

        private void frmMM_PurchaseD_Load(object sender, EventArgs e)
        {
            Set_Lup();

            Set_Gv(gvDetail);

            Set_Master();

            if (state == "U")
            {
                Set_ReadOnly();
            }

            DisplayMaster();
            DisplayDetail();
        }

        #region 디자인
        private void Set_Lup()
        {
            LupCustomer.Properties.DataSource = code.GetCustomer(CustType);
            LupCustomer.Properties.DisplayMember = "거래처명";
            LupCustomer.Properties.ValueMember = "거래처코드";

            LupVatType.Properties.DataSource = code.GetClassList("C010");
            LupVatType.Properties.DisplayMember = "분류명";
            LupVatType.Properties.ValueMember = "분류코드";

            LupStock.Properties.DataSource = code.GetWarehouseList_Dt();
            LupStock.Properties.DisplayMember = "창고명";
            LupStock.Properties.ValueMember = "창고코드";

            //신규일경우 자재창고 디폴트창고
            if(state == "I")
			{
                LupStock.EditValue = "S0001";
            }

            DtOutDate.EditValue = DateTime.Now.Date;
            LupVatType.EditValue = "10";
            SpVatRate.EditValue = 10;
        }

        private void Set_Gv(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            RepositoryItemSearchLookUpEdit lupUnitCode = rpo.Create_RepositoryItemSearchLookUp(code.GetUnitCode(), "코드", "코드명");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lupVat = rpo.Create_RepositoryItemSearchLookUp(code.GetClassList("C010"), "분류코드", "분류명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            RepositoryItemButtonEdit btnDel = new RepositoryItemButtonEdit();
            btnDel.Name = "btnDelete";
            btnDel.Buttons.Clear();
            btnDel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            btnDel.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            btnDel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnDel.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDelete_ButtonClick);

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "순번", "SalesSeq");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "출고량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "합계", "TotalPrice");
            grid.Create_GvColumn(gv, "부가세유형", "VatType", lupVat);
            grid.Create_GvColumn(gv, "부가세율(%)", "VatRate");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "삭제", "colDelete", btnDel);

            grid.Create_GvColumn(gv, "입고번호", "StoNo");
            grid.Create_GvColumn(gv, "입고순번", "StoSeq");
            grid.Create_GvColumn(gv, "LotNo", "LotNo");

            //
            //gv.Columns["ItemCode"].FieldName = "ItemCode";

            //Repository
            //gv.Columns["ItemName"].ColumnEdit = lup_Item;
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            // 수정여부
            grid.Column_AllowEdit(gv.Columns["Qty"]);
            grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            grid.Column_AllowEdit(gv.Columns["colDelete"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            gv.Columns["VatType"].Visible = false;
            gv.Columns["VatRate"].Visible = false;
            gv.Columns["StoNo"].Visible = false;
            gv.Columns["StoSeq"].Visible = false;
            gv.Columns["LotNo"].Visible = false;

            if (state == "U")
            {
                gv.Columns["colDelete"].Visible = false;
            }

            // 삭제버튼 고정
            gv.Columns["colDelete"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
        }

        private void Set_Master()
        {
            DtOutDate.EditValue = DateTime.Now.Date;

            LupVatType.EditValue = "10";    // 과세
            SpVatRate.EditValue = 10;
        }

        // 업데이트일 때 Master 수정불가
        private void Set_ReadOnly()
        {
            stoNoTextEdit.ReadOnly = true;
            DtOutDate.ReadOnly = true;
            LupStock.ReadOnly = true;
            LupCustomer.ReadOnly = true;
            LupVatType.ReadOnly = true;
            SpVatRate.ReadOnly = true;
        }
        #endregion

        #region 품목선택

        private void btn_Select_Click(object sender, EventArgs e)
        {
            if (LupCustomer.EditValue.ConvertString_Null_To_Empty() == "")
            {
                MessageBox.Show("거래처를 먼저 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Frm_SelectItem form = new Frm_SelectItem(LupCustomer.EditValue.ConvertString_Null_To_Empty(), "20"); //매출
            form.onSetData += new Frm_SelectItem.SetData(ProductSetData);
            form.ShowDialog();
        }

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

                int maxSeq = 0;

                foreach (DataRow item in dt.Rows)
                {
                    int seq = item["SalesSeq"] == DBNull.Value ? 0 : (int)item["SalesSeq"];
                    if (maxSeq < seq) maxSeq = seq;
                }
                newItem["SalesSeq"] = ++maxSeq;

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

                newItem["Qty"] = drv["OrderQty"].ConvertDecimal_Null_To_Zero();
                Decimal qty = newItem["Qty"].ConvertDecimal_Null_To_Zero();

                if (dt.Columns.Contains("StockQty")) newItem["StockQty"] = 0;
                if (dt.Columns.Contains("ReqQty")) newItem["ReqQty"] = 0;
                if (dt.Columns.Contains("ProQty")) newItem["ProQty"] = 0;
                if (dt.Columns.Contains("OutQty")) newItem["OutQty"] = 0;

                string custCode = LupCustomer.EditValue.ConvertString_Null_To_Empty();
                string ItemCode = drv["ItemCode"].ToString();

                CM_ItemPrice cmPrice = new CM_ItemPrice();
                cmPrice.PriceType = "20";
                cmPrice.ApplyDate = DateTime.Now.ToString("d");

                decimal unitPrice = cmPrice.Get_MaterialPrice(custCode, ItemCode);
                newItem["UnitPrice"] = unitPrice;

                decimal price = qty * unitPrice;
                newItem["Price"] = price;

                decimal vatPrice = 0;
                if (LupVatType.EditValue == "10")
                {
                    vatPrice = price * 0.1m;
                }

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
            finally
            {
                isNew = false;
            }

            gvDetail.BestFitColumns();
        }

        #endregion


        #region 입고품목선택
        private void cmdOrderSelect_Click(object sender, EventArgs e)
        {
            //if ((LupCustomer.EditValue ?? "").ToString() == "")
            //{
            //    MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            Frm_LookUpPurchase form = new Frm_LookUpPurchase("MM_PurchaseM", (LupCustomer.EditValue ?? "").ToString());
            form.onSetData += new Frm_LookUpPurchase.SetData(PurchaseSetData);
            form.ShowDialog();
        }

        private void PurchaseSetData(DataRowView drv)
        {
            isNew = true;

            try
            {
                DataTable dtDetail = (DataTable)gcDetail.DataSource;

                int maxSeq = 0;

                foreach (DataRow row in dtDetail.Rows)
                {
                    int seq = row["StoSeq"] == DBNull.Value ? 0 : Convert.ToInt32(row["StoSeq"]);
                    if (maxSeq < seq) maxSeq = seq;

                    // 발주번호 + 발주순번 기준 중복체크
                    if (row["StoNo"] != DBNull.Value && row["StoSeq"] != DBNull.Value)
                    {
                        if (drv["StoNo"].ToString() == row["StoNo"].ToString()
                            && Convert.ToInt32(drv["StoSeq"]) == Convert.ToInt32(row["StoSeq"]))
                        {
                            MessageBox.Show("동일한 입고 품목이 존재합니다.");
                            return;
                        }
                    }
                }

                DataRow newRow = dtDetail.NewRow();

                newRow["CorpId"] = WrGlobal.CorpID;
                //newRow["StoNo"] = master["StoNo"];
                //newRow["State"] = "대기";
                newRow["SalesSeq"] = ++maxSeq;

                newRow["ItemCode"] = drv["ItemCode"];
                newRow["ItemName"] = drv["ItemName"];
                newRow["Size"] = drv["Size"];
                newRow["UnitCode"] = drv["UnitCode"];

                //master["CustCode"] = drv["CustCode"];

                newRow["Qty"] = drv["OutQty"];
                newRow["UnitPrice"] = drv["UnitPrice"];

                //decimal price = Convert.ToDecimal(newRow["Qty"]) * Convert.ToDecimal(newRow["UnitPrice"]);
                //decimal vat = price * Convert.ToDecimal(drv["VatRate"]) / 100;

                //newRow["Price"] = price;
                //newRow["VatPrice"] = vat;
                //newRow["TotalPrice"] = price + vat;

                newRow["Price"] = drv["Price"];
                newRow["VatPrice"] = drv["VatPrice"];
                newRow["TotalPrice"] = drv["TotalPrice"];

                newRow["VatType"] = drv["VatType"];
                newRow["VatRate"] = drv["VatRate"];

                newRow["StoNo"] = drv["StoNo"];
                newRow["StoSeq"] = drv["StoSeq"];
                newRow["LotNo"] = drv["LotNo"];

                newRow["CreId"] = WrGlobal.LoginID;
                newRow["CreIP"] = WrGlobal.ClientHostName;
                newRow["CreDt"] = DateTime.Now;

                dtDetail.Rows.Add(newRow);

                gvDetail.RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isNew = false;
            }
        }

        #endregion

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (state == "I")
            {
                NEW_SaveSales();
            }
            else
            {
                Upd_SaveSales();
            }
        }

        #region 조회
        private void DisplayMaster()
        {
            BM_SalesM sm = new BM_SalesM();
            sm.SalesNo = SalesNo;

            DataRow dr = sm.SelectBM_SalesM_BySalesNo();

            if (dr != null )
            {
                stoNoTextEdit.EditValue = dr["SalesNo"].ToString();
                DtOutDate.EditValue = Convert.ToDateTime(dr["SalesDate"]);
                LupStock.EditValue = dr["HouseCode"].ToString();
                LupCustomer.EditValue = dr["ShipToCode"].ToString();
                TxtRemark.EditValue = dr["Remark"].ToString();
            }
        }

        private void DisplayDetail()
        {
            BM_SalesD sd = new BM_SalesD();
            sd.SalesNo = SalesNo;

            DataTable dt = sd.Select_BM_SalesD_SalesNo();

            gcDetail.DataSource = dt;
            gvDetail.BestFitColumns();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                LupVatType.EditValue = dr["VatType"].ToString();
                SpVatRate.EditValue = dr["VatRate"].ConvertInt_Null_To_Zero();
            }
        }
        #endregion

        #region CRUD
        // 추가
        private void NEW_SaveSales()
        {
            if (gvDetail.RowCount == 0) return;
            gvDetail.FocusInvalidRow();
            gvDetail.UpdateCurrentRow();

            string salesNo = "";

            try
            {
                string houseCode = LupStock.EditValue.ConvertString_Null_To_Empty();

                DataTable dt = gcDetail.DataSource as DataTable;

                if (houseCode == "")
                {
                    MessageBox.Show("출고창고를 선택해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dt.Select("Qty <= 0").Length > 0)
                {
                    MessageBox.Show("수량을 작성해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                salesNo = "OG" + code.GetAutoNumber("출고", (DateTime)DtOutDate.EditValue);

                //마스터 저장
                BM_SalesM sm = new BM_SalesM();
                sm.CorpId = WrGlobal.CorpID;
                sm.SalesNo = salesNo;
                sm.SalesDate = DtOutDate.DateTime.ToString("d");
                sm.ShipToCode = LupCustomer.EditValue.ConvertString_Null_To_Empty();
                sm.ShipToName = LupCustomer.Properties.GetDisplayText(LupCustomer.EditValue);
                sm.OutType = "30"; //10:제품출하, 30:자재출고
                sm.WriteDate = sm.SalesDate;
                sm.Remark = TxtRemark.EditValue.ConvertString_Null_To_Empty();
                sm.HouseCode = houseCode;

                sm.InsertBM_SalesM();

                //디테일 저장
                BM_SalesD sd = new BM_SalesD();
                sd.CorpId = WrGlobal.CorpID;
                sd.SalesNo = salesNo;
                sd.VatType = LupVatType.EditValue.ToString();
                sd.VatRate = LupVatType.EditValue.ConvertString_Null_To_Empty() == "10" ? 10 : 0;
                sd.HouseCode = houseCode;

                foreach (DataRow dr in dt.Rows)
                {
                    sd.SalesSeq = Convert.ToInt32(dr["SalesSeq"]);
                    sd.ItemCode = dr["ItemCode"].ToString();
                    sd.UnitCode = dr["UnitCode"].ToString();
                    sd.Qty = dr["Qty"].ConvertFloat_Null_To_Zero();
                    sd.UnitPrice = dr["UnitPrice"].ConvertDecimal_Null_To_Zero();
                    sd.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
                    sd.VatPrice = dr["VatPrice"].ConvertDecimal_Null_To_Zero();
                    sd.TotalPrice = dr["TotalPrice"].ConvertDecimal_Null_To_Zero();
                    sd.ReqType = "자재출고";
                    sd.Status = "출고완료";
                    sd.LotNo = dr["LotNo"].ToString();

                    sd.InsertBM_SalesD();
                }

                //Master 총액 업데이트
                sm.UpdateBM_SalesM_TotalPrice();

                //ST_StockM/Detail 추가
                query = $@"EXEC Make_MaterialOutStockList '{WrGlobal.CorpID}', '{salesNo}', '{houseCode}', '{WrGlobal.LoginID}'";
                db.ExecuteQuery(query);

                MessageBox.Show("출고등록이 완료되었습니다.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                Delete_Sales(salesNo);
            }
        }

        private void Delete_Sales(string _salesNo)
        {
            BM_SalesM sm = new BM_SalesM();
            sm.CorpId = WrGlobal.CorpID;
            sm.SalesNo = _salesNo;

            sm.DeleteSalesMD_Stock();
        }

        private void Upd_SaveSales()
        {
            gvDetail.FocusInvalidRow();
            gvDetail.UpdateCurrentRow();

            DataTable dt = gcDetail.DataSource as DataTable;

            string salesNo = stoNoTextEdit.Text;
            string houseCode = LupStock.EditValue.ConvertString_Null_To_Empty();
            string remark = TxtRemark.EditValue.ConvertString_Null_To_Empty();

            try
            {
                if (houseCode == null || houseCode == "")
                {
                    MessageBox.Show("출고창고를 선택해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dt.Select("Qty <= 0").Length > 0)
                {
                    MessageBox.Show("수량을 작성해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BM_SalesD sd = new BM_SalesD();
                sd.CorpId = WrGlobal.CorpID;
                sd.SalesNo = salesNo;
                sd.VatType = LupVatType.EditValue.ToString();
                sd.VatRate = LupVatType.EditValue.ConvertString_Null_To_Empty() == "10" ? 10 : 0;
                sd.HouseCode = houseCode;

                foreach (DataRow dr in dt.Rows)
                {
                    string status = dr["Status"].ToString();

                    sd.SalesSeq = Convert.ToInt32(dr["SalesSeq"]);
                    sd.ItemCode = dr["ItemCode"].ToString();
                    sd.UnitCode = dr["UnitCode"].ToString();
                    sd.Qty = dr["Qty"].ConvertFloat_Null_To_Zero();
                    sd.UnitPrice = dr["UnitPrice"].ConvertDecimal_Null_To_Zero();
                    sd.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
                    sd.VatPrice = dr["VatPrice"].ConvertDecimal_Null_To_Zero();
                    sd.TotalPrice = dr["TotalPrice"].ConvertDecimal_Null_To_Zero();
                    sd.ReqType = "자재출고";
                    sd.Status = "출고완료";
                    sd.LotNo = dr["LotNo"].ToString();
                    sd.Status = "출고완료";

                    switch (status)
                    {
                        case "신규":
                            if (!sd.InsertBM_SalesD())
                            {
                                MessageBox.Show("저장 중 오류가 발생했습니다");
                                return;
                            }
                            break;

                        case "수정":
                            if (!sd.UpdateBM_SalesD())
                            {
                                MessageBox.Show("저장 중 오류가 발생했습니다");
                                return;
                            }
                            break;

                        case "삭제":
                            if (!sd.DeleteBM_SalesD())
                            {
                                MessageBox.Show("저장 중 오류가 발생했습니다");
                                return;
                            }
                            break;
                    }
                }

                //Master 총액 업데이트
                BM_SalesM sm = new BM_SalesM();
                sm.CorpId = WrGlobal.CorpID;
                sm.SalesNo = salesNo;

                sm.UpdateBM_SalesM_TotalPrice();

                sm.Remark = remark;
                sm.UpdateBM_SalesM_Remark(); //비고만 수정 가능하여 비고만 업데이트

                //ST_StockM/Detail 추가
                query = $@"EXEC Make_MaterialOutStockList '{WrGlobal.CorpID}', '{salesNo}', '{houseCode}', '{WrGlobal.LoginID}'";
                db.ExecuteQuery(query);

                MessageBox.Show("저장이 완료되었습니다.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                Delete_Sales(salesNo);
            }
        }

        #endregion

        #region 이벤트
        private void LupVatType_EditValueChanged(object sender, EventArgs e)
        {
            string vatType = LupVatType.EditValue.ToString();

            if (vatType != "10") SpVatRate.EditValue = 0;
            else SpVatRate.EditValue = 10;

            if (gvDetail.RowCount != 0)
            {
                DataTable dt = gcDetail.DataSource as DataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    dr["VatType"] = vatType;
                    dr["VatRate"] = SpVatRate.EditValue;
                }

                Set_Calcul();
            }

        }

        private void Set_Calcul()
        {
            DataRow dr = gvDetail.GetFocusedDataRow();

            if (dr == null) return;

            if ((dr["Qty"] ?? "").ToString() == "") dr["Qty"] = 0;
            if ((dr["UnitPrice"] ?? "").ToString() == "") dr["UnitPrice"] = 0;

            dr["Price"] = (Convert.ToDecimal(dr["Qty"]) * Convert.ToDecimal(dr["UnitPrice"]));
            dr["VatPrice"] = Math.Round(Convert.ToDecimal(dr["Price"]) * Convert.ToDecimal(SpVatRate.EditValue) / 100);
            dr["TotalPrice"] = Convert.ToDecimal(dr["Price"]) + Convert.ToDecimal(dr["VatPrice"]);
        }

        private void gvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName == "UnitPrice" || e.Column.FieldName == "Qty") Set_Calcul();
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            grid.Check_OverLap_CellValue(sender as GridView, e, "ItemCode");
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
            {
                int rowHandle = gvDetail.FocusedRowHandle;
                if (rowHandle < 0) return;

                DataRow dr = gvDetail.GetDataRow(rowHandle);
                if (dr == null) return;

                DataTable dt = gvDetail.GridControl.DataSource as DataTable;
                if (dt != null)
                {
                    dt.Rows.Remove(dr);
                    gvDetail.RefreshData();
                }
            }
        }
        #endregion

        private void btn_Close_Click(object sender, EventArgs e)
		{
            this.Close();
		}
    }
}