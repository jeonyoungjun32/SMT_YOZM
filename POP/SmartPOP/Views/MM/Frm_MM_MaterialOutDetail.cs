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
//using SmartMES.View.Lookup;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using SmartPOP.Models;
using SmartPOP.Common;
using SMT_MES.Controls;
using SmartPOP.Views.POPUP;

namespace SmartMES.View.MM
{
    public partial class Frm_MM_MaterialOutDetail : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();

        string query;
        Database db = new Database();

        string state = "I";     // 신규 : I, 수정 : U
        string SalesNo = "";

        private int _numPadRowHandle = -1;
        private string _numPadFieldName = "";

        public Frm_MM_MaterialOutDetail()
        {
            InitializeComponent();
        }

        public Frm_MM_MaterialOutDetail(string state, string salesNo)
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

            DisplaySalesInfo();
        }

        #region 디자인
        private void Set_Lup()
        {
            string[] custType = { "20", "40" };
            string[] materialType = { "40" };

            //거래처
            LupCustomer.Properties.DataSource = code.GetCustomer(custType);
            LupCustomer.Properties.DisplayMember = "거래처명";
            LupCustomer.Properties.ValueMember = "거래처코드";

            LupCustomer.Properties.ShowClearButton = false;
            LupCustomer.Properties.View.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            LupCustomer.Properties.View.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정

            //VAT구분
            LupVatType.Properties.DataSource = code.GetClassList("C010");
            LupVatType.Properties.DisplayMember = "분류명";
            LupVatType.Properties.ValueMember = "분류코드";

            //품목
            LupMaterial.Properties.DataSource = code.GetMaterialList(materialType);
            LupMaterial.Properties.DisplayMember = "품명";
            LupMaterial.Properties.ValueMember = "품목코드";

            LupMaterial.Properties.ShowClearButton = false;
            LupMaterial.Properties.View.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            LupMaterial.Properties.View.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정

            //창고
            LupStock.Properties.DataSource = code.GetWarehouseList_Dt();
            LupStock.Properties.DisplayMember = "창고명";
            LupStock.Properties.ValueMember = "창고코드";

            LupStock.Properties.ShowClearButton = false;
            LupStock.Properties.View.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            LupStock.Properties.View.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정

            //신규일경우 자재창고 디폴트창고
            if (state == "I")
            {
                LupStock.EditValue = "S0001";
            }
        }

        private void Set_Gv(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lupVat = rpo.Create_RepositoryItemSearchLookUp(code.GetClassList("C010"), "분류코드", "분류명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            EditorButton btn_Del = new EditorButton();
            btn_Del.Kind = ButtonPredefines.Delete;
            RepositoryItemButtonEdit btnDel = new RepositoryItemButtonEdit();
            btnDel.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            btnDel.Buttons.RemoveAt(0);
            btnDel.Buttons.Add(btn_Del);

            btnDel.ButtonClick += BtnDel_ButtonClick;

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
            grid.Create_GvColumn(gv, "삭제", "Del", btnDel);

            //Repository
            //gv.Columns["MaterialName"].ColumnEdit = lup_Item;
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            // 수정여부 
            //grid.Column_AllowEdit(gv.Columns["Qty"]);
            //grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            grid.Column_AllowEdit(gv.Columns["Del"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            // 삭제버튼 고정
            gv.Columns["Del"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //
            grid.SetCol_Type(gv.Columns["Qty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TFloat);

            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            gv.RowCellClick += Gv_RowCellClick;

            gv.BestFitColumns();
        }

        private void Set_Master()
        {
            DtOutDate.EditValue = DateTime.Now.Date;
            rpo.Set_POP_Dtp(DtOutDate);

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

        #region 조회
        private void DisplaySalesInfo()
        {
            Set_MasterInfo();

            Set_DetailInfo();
        }

        // 마스터 정보 셋팅
        private void Set_MasterInfo()
        {
            query = $@"SELECT DISTINCT SM.SalesNo
                                , SM.SalesDate
                                , SM.ShipToCode
                                , SD.VatType
                                , SD.VatRate
                                , SD.HouseCode
                                , SM.Remark
                        FROM BM_SalesM SM
                        INNER JOIN BM_SalesD SD ON SM.SalesNo = SD.SalesNo
                        WHERE SM.CorpId = '{WrGlobal.CorpID}'
                        AND SM.SalesNo = '{SalesNo}'
                    ";

            DataRow dr = db.GetDataRow(query);

            if (dr == null) return;

            stoNoTextEdit.EditValue = dr["SalesNo"].ToString();
            DtOutDate.EditValue = Convert.ToDateTime(dr["SalesDate"]);
            LupStock.EditValue = dr["HouseCode"].ToString();
            LupCustomer.EditValue = dr["ShipToCode"].ToString();
            LupVatType.EditValue = dr["VatType"].ToString();
            SpVatRate.EditValue = Convert.ToInt32(dr["VatRate"]);
            TxtRemark.EditValue = dr["Remark"].ToString();
        }

        // 디테일 정보 셋팅
        private void Set_DetailInfo()
        {
            query = $@"SELECT '' Status
                                  , SalesSeq
                                , cm.ItemCode
                                , cm.ItemName
                                , cm.Alias
                                , cm.ItemClass1
                                , cm.ItemClass2
                                , cm.ItemClass3
                                , cm.Size
                                , cm.UnitWeight
                                , cm.Texture
                                , cm.Type
                                , cm.UnitCode
                                , cm.ColorCode

                                , Qty
                                , sd.UnitCode
                                , sd.UnitPrice
                                , Price
                                , VatPrice
                                , TotalPrice
                                , VatType
                                , VatRate
                                , sd.Remark
                        FROM BM_SalesD sd
				        LEFT JOIN CM_Item cm ON sd.CorpId = cm.CorpId AND sd.ItemCode = cm.ItemCode
                        WHERE sd.CorpId = '{WrGlobal.CorpID}' 
                        AND SalesNo = '{SalesNo}'
                    ";

            gcDetail.DataSource = db.GetDataTable(query);
            gvDetail.BestFitColumns();
        }

        #endregion

        #region CRUD
        // 추가
        private void NEW_SaveSales()
        {
            if (gvDetail.RowCount == 0) return;

            string salesNo = "";
            int salesSeq = 0;
            string productCode = "";
            string unitCode = "";
            double qty = 0;
            double unitPrice = 0;
            string vatType = "";
            double vatRate = 0;
            string houseCode = "";
            string remark = "";

            salesNo = "OG" + code.GetAutoNumber("출고", (DateTime)DtOutDate.EditValue);
            houseCode = LupStock.EditValue?.ToString();
            remark = TxtRemark.EditValue?.ToString();

            try
            {
                DataTable dt = gcDetail.DataSource as DataTable;

                //if (LupCustomer.EditValue?.ToString() == null || LupCustomer.EditValue?.ToString() == "")
                //{
                //    cls_MessageBox.Show("거래처를 선택해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if (houseCode == null || houseCode == "")
                {
                    cls_MessageBox.Show("출고창고를 선택해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dt.Select("Qty <= 0").Length > 0)
                {
                    cls_MessageBox.Show("수량을 작성해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //if (dt.Select("UnitPrice <= 0").Length > 0)
                //{
                //    cls_MessageBox.Show("단가를 작성해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if (remark == null) remark = "";

                #region Master 추가
                query = $@"INSERT INTO BM_SalesM (
                                CorpId, SalesNo, SalesDate
                                , ShipToCode, ShipToName
                                , OutType 
                                , WriteDate, Remark
                                , CreId, CreIP, CreDt
                            ) VALUES (
                                    '{WrGlobal.CorpID}', '{salesNo}', '{DtOutDate.EditValue:yyyy-MM-dd}'
                                    , '{Convert.ToString(LupCustomer.EditValue)}', '{LupCustomer.Properties.GetDisplayText(LupCustomer.EditValue)}'
                                    , '30'  -- 자재출고
                                    , GETDATE(), '{remark}'
                                    , '{WrGlobal.LoginID}', HOST_NAME(), GETDATE()
                            )
                            ";
                db.ExecuteQuery(query);
                #endregion

                #region Detail 추가
                foreach (DataRow dr in dt.Rows)
                {
                    salesSeq = Convert.ToInt32(dr["SalesSeq"]);
                    productCode = dr["ItemCode"].ToString();
                    unitCode = dr["UnitCode"].ToString();
                    qty = dr["Qty"].ConvertFloat_Null_To_Zero();
                    unitPrice = dr["UnitPrice"].ConvertFloat_Null_To_Zero();
                    vatType = LupVatType.EditValue.ToString();
                    vatRate = vatType == "10" ? 10 : 0;

                    query = $@"INSERT INTO BM_SalesD (
                                CorpId, SalesNo, SalesSeq, ItemCode
                                , UnitCode, Qty, UnitPrice, Price, VatPrice, TotalPrice
                                , VatType, VatRate, ReqType, Status
                                , HouseCode
                                )
                                VALUES (
                                '{WrGlobal.CorpID}', '{salesNo}', {salesSeq}, '{productCode}'
                                , '{unitCode}', {qty}, {unitPrice}, {qty * unitPrice}, {qty * unitPrice * (vatRate / 100)}, {qty * unitPrice * (1 + (vatRate / 100))}
                                , '{vatType}', '{vatRate}', '자재출고', '출고완료'
                                , '{houseCode}'
                                )
                            ";

                    db.ExecuteQuery(query);
                }
                #endregion

                //Master 총액 수정
                query = $@"UPDATE BM_SalesM
                            SET TotalPrice = (select ISNULL(SUM(TotalPrice),0) from BM_SalesD where SalesNo = '{salesNo}' and CorpID = '{WrGlobal.CorpID}')
                            WHERE SalesNo = '{salesNo}'
                            AND CorpID = '{WrGlobal.CorpID}'
                        ";

                db.ExecuteQuery(query);

                //StockMaster/Detail 추가
                query = $@"EXEC Make_MaterialOutStockList '{WrGlobal.CorpID}', '{salesNo}', '{houseCode}', '{WrGlobal.LoginID}'";
                db.ExecuteQuery(query);

                cls_MessageBox.Show("출고등록이 완료되었습니다.");
                this.Close();
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.ToString());

                Reset_SalesNo(salesNo);
            }
        }

        private void Reset_SalesNo(string _salesNo)
        {
            query = $@"DELETE FROM BM_SalesD WHERE CorpId = '{WrGlobal.CorpID}' AND SalesNo = '{_salesNo}'";
            db.ExecuteQuery(query);

            query = $@"DELETE FROM BM_SalesM WHERE CorpId = '{WrGlobal.CorpID}' AND SalesNo = '{_salesNo}'";
            db.ExecuteQuery(query);
        }

        private void Upd_SaveSales()
        {
            DataTable dt = gcDetail.DataSource as DataTable;

            string salesNo = stoNoTextEdit.Text;
            string remark = "";
            string houseCode = "";

            houseCode = LupStock.EditValue?.ToString();
            remark = TxtRemark.EditValue?.ToString();

            try
            {
                if (houseCode == null || houseCode == "")
                {
                    cls_MessageBox.Show("출고창고를 선택해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (dt.Select("Qty <= 0").Length > 0)
                {
                    cls_MessageBox.Show("수량을 작성해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //if (dt.Select("UnitPrice <= 0").Length > 0)
                //{
                //    cls_MessageBox.Show("단가를 작성해주십시오", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if (remark == null) remark = "";

                #region Master 추가
                /*
                query = $@"INSERT INTO BM_SalesM (
                                CorpId, SalesNo, SalesDate
                                , ShipToCode, ShipToName
                                , WriteDate, Remark
                                , CreId, CreIP, CreDt
                            ) VALUES (
                                    '{WrGlobal.CorpID}', '{salesNo}', '{DtOutDate.EditValue:yyyy-MM-dd}'
                                    , '{LupCustomer.EditValue.ToString()}', '{LupCustomer.Properties.GetDisplayText(LupCustomer.EditValue)}'
                                    , GETDATE(), '{remark}'
                                    , '{WrGlobal.LoginID}', HOST_NAME(), GETDATE()
                            )
                            ";
                db.ExecuteQuery(query);
                */
                #endregion

                #region Detail 추가
                foreach (DataRow dr in dt.Rows)
                {
                    string status = dr["Status"].ToString();

                    switch (status)
                    {
                        case "신규":
                            if (!Ins_DB(dr))
                            {
                                cls_MessageBox.Show("저장 중 오류가 발생했습니다");
                                return;
                            }
                            break;

                        case "수정":
                            if (!Upd_DB(dr))
                            {
                                cls_MessageBox.Show("저장 중 오류가 발생했습니다");
                                return;
                            }
                            break;

                        case "삭제":
                            if (!Del_DB(dr))
                            {
                                cls_MessageBox.Show("저장 중 오류가 발생했습니다");
                                return;
                            }
                            break;
                    }    

                }
                #endregion

                //Master 총액 수정
                query = $@"UPDATE BM_SalesM
                            SET TotalPrice = (select ISNULL(SUM(TotalPrice),0) from BM_SalesD where SalesNo = '{salesNo}' and CorpID = '{WrGlobal.CorpID}')
                            WHERE SalesNo = '{salesNo}'
                            AND CorpID = '{WrGlobal.CorpID}'
                        ";

                db.ExecuteQuery(query);

                //StockMaster/Detail 추가
                query = $@"EXEC Make_MaterialOutStockList '{WrGlobal.CorpID}', '{salesNo}', '{houseCode}', '{WrGlobal.LoginID}'";
                db.ExecuteQuery(query);

                cls_MessageBox.Show("저장이 완료되었습니다.");
                this.Close();
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.ToString());

                Reset_SalesNo(salesNo);
            }
        }

        private bool Ins_DB(DataRow dr)
        {
            string salesNo = stoNoTextEdit.Text;
            int salesSeq = 0;
            string productCode = "";
            string unitCode = "";
            double qty = 0;
            double unitPrice = 0;
            string vatType = "";
            double vatRate = 0;
            string houseCode = "";
            
            salesSeq = Convert.ToInt32(dr["SalesSeq"]);
            productCode = dr["ItemCode"].ToString();
            unitCode = dr["UnitCode"].ToString();
            qty = dr["Qty"].ConvertFloat_Null_To_Zero();
            unitPrice = dr["UnitPrice"].ConvertFloat_Null_To_Zero();
            vatType = LupVatType.EditValue.ToString();
            vatRate = vatType == "10" ? 10 : 0;
            houseCode = LupStock.EditValue?.ToString();

            query = $@"INSERT INTO BM_SalesD (
                                CorpId, SalesNo, SalesSeq, ProductCode
                                , UnitCode, Qty, UnitPrice, Price, VatPrice, TotalPrice
                                , VatType, VatRate, ReqType, Status
                                , HouseCode
                                )
                                VALUES (
                                '{WrGlobal.CorpID}', '{salesNo}', {salesSeq}, '{productCode}'
                                , '{unitCode}', {qty}, {unitPrice}, {qty * unitPrice}, {qty * unitPrice * (vatRate / 100)}, {qty * unitPrice * (1 + (vatRate / 100))}
                                , '{vatType}', '{vatRate}', '자재출고', '출고완료'
                                , '{houseCode}'
                                )
                            ";
            return db.ExecuteQuery(query);
        }

        private bool Upd_DB(DataRow dr)
        {
            string salesNo = stoNoTextEdit.Text;
            int salesSeq = 0;
            double qty = 0;
            double unitPrice = 0;
            string vatType = "";
            double vatRate = 0;

            salesSeq = Convert.ToInt32(dr["SalesSeq"]);
            qty = dr["Qty"].ConvertFloat_Null_To_Zero();
            unitPrice = dr["UnitPrice"].ConvertFloat_Null_To_Zero();
            vatType = LupVatType.EditValue.ToString();
            vatRate = vatType == "10" ? 10 : 0;

            query = $@"UPDATE BM_SalesD
                        SET Qty = {qty}
                            , UnitPrice = {unitPrice}
                            , Price = {qty * unitPrice}
                            , VatPrice = {qty * unitPrice * (vatRate / 100)}
                            , TotalPrice = {qty * unitPrice * (1 + (vatRate / 100))}
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND SalesNo = '{salesNo}'
                        AND SalesSeq = {salesSeq}
                            ";

            return db.ExecuteQuery(query);
        }


        private bool Del_DB(DataRow dr)
        {
            string salesNo = stoNoTextEdit.Text;
            int salesSeq = 0;

            salesSeq = Convert.ToInt32(dr["SalesSeq"]);

            query = $@"DELETE BM_SalesD
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND SalesNo = '{salesNo}'
                        AND SalesSeq = {salesSeq}
                        ";

            db.ExecuteQuery(query);

            query = $@"IF NOT EXISTS (SELECT 1 
                                        FROM BM_SalesD
                                        WHERE CorpId = '{WrGlobal.CorpID}'
                                        AND SalesNo = '{salesNo}' 
                                    )
                        BEGIN 
                            DELETE BM_SalesM WHERE CorpId = '{WrGlobal.CorpID}'
                                                AND SalesNo = '{salesNo}' 
                        END
                    ";

            return db.ExecuteQuery(query);
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if ((LupMaterial.EditValue ?? "").ToString() == "")
            {
                cls_MessageBox.Show("출고 품목을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = gcDetail.DataSource as DataTable;

            if (dt != null)
            { 
                foreach (DataRow dr in dt.Rows)
                {
                    if ((LupMaterial.EditValue ?? "").ToString() == dr["ItemCode"].ToString())
                    {
                        cls_MessageBox.Show("이미 등록된 품목 입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            try
            {
                DataRow dr = dt.NewRow();

                int maxSeq = 0;


                string materialCode = LupMaterial.EditValue.ConvertString_Null_To_Empty();
                DataRowView drvMaterial = code.GetMaterialInfo(materialCode);
                if (drvMaterial == null) throw null;

                foreach (DataRow item in dt.Rows)
                {
                    int seq = item["SalesSeq"] == DBNull.Value ? 0 : (int)item["SalesSeq"];
                    if (maxSeq < seq) maxSeq = seq;
                }

                dr["Status"] = "신규";
                dr["SalesSeq"] = ++maxSeq;

                dr["ItemCode"] = materialCode;

                if (dt.Columns.Contains("ItemName")) dr["ItemName"] = materialCode;
                if (dt.Columns.Contains("Alias")) dr["Alias"] = drvMaterial["Alias"];
                if (dt.Columns.Contains("ItemClass1")) dr["ItemClass1"] = drvMaterial["ItemClass1"];
                if (dt.Columns.Contains("ItemClass2")) dr["ItemClass2"] = drvMaterial["ItemClass2"];
                if (dt.Columns.Contains("ItemClass3")) dr["ItemClass3"] = drvMaterial["ItemClass3"];
                if (dt.Columns.Contains("Size")) dr["Size"] = drvMaterial["Size"];
                if (dt.Columns.Contains("UnitWeight")) dr["UnitWeight"] = drvMaterial["UnitWeight"];
                if (dt.Columns.Contains("Texture")) dr["Texture"] = drvMaterial["Texture"];
                if (dt.Columns.Contains("Type")) dr["Type"] = drvMaterial["Type"];
                if (dt.Columns.Contains("UnitCode")) dr["UnitCode"] = drvMaterial["UnitCode"];
                if (dt.Columns.Contains("ColorCode")) dr["ColorCode"] = drvMaterial["ColorCode"];

                dr["Qty"] = SpQty.EditValue;
                dr["UnitPrice"] = 0;
                dr["Price"] = 0;
                dr["VatPrice"] = 0;
                dr["TotalPrice"] = 0;
                dr["VatType"] = LupVatType.EditValue;
                dr["VatRate"] = SpVatRate.EditValue;

                dt.Rows.Add(dr);

                gvDetail.BestFitColumns();
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void cmdSave_ElementClick(object sender,EventArgs e)
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

        private void Set_Calcul()
        {
           DataRow dr = gvDetail.GetFocusedDataRow();

            if (dr == null) return;

            decimal unitPrice = dr["UnitPrice"].ConvertDecimal_Null_To_Zero();
            decimal qty = dr["Qty"].ConvertDecimal_Null_To_Zero();
            decimal price = unitPrice * qty;

            dr["Price"] = price;
            dr["VatPrice"] = Math.Round(price * Convert.ToDecimal(SpVatRate.EditValue) / 100);
            dr["TotalPrice"] = price + Convert.ToDecimal(dr["VatPrice"]);
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

        private void Gv_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;

            if (e.RowHandle < 0)
                return;

            if (e.Column.FieldName != "Qty" && e.Column.FieldName != "UnitPrice")
                return;

            try
            {
                _numPadRowHandle = e.RowHandle;
                _numPadFieldName = e.Column.FieldName;

                float value = gv.GetRowCellValue(
                    e.RowHandle,
                    e.Column.FieldName
                ).ConvertFloat_Null_To_Zero();

                Frm_NumPad NumPad = new Frm_NumPad(e.Column.FieldName, 0);

                NumPad.FormSendEvent += new Frm_NumPad.FormSendDataHandler(AddNum_Grid);

                int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
                int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

                int x = screenWidth - NumPad.Width;
                int y = (screenHeight - NumPad.Height) / 2;

                NumPad.StartPosition = FormStartPosition.Manual;
                NumPad.Location = new Point(x, y);

                NumPad.ShowDialog();

                NumPad.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddNum_Grid(string value)
        {
            if (_numPadRowHandle < 0)
                return;

            if (string.IsNullOrEmpty(_numPadFieldName))
                return;

            GridView gv = gcDetail.MainView as GridView;

            if (gv == null)
                return;

            float qty = value.ConvertFloat_Null_To_Zero();

            gv.SetRowCellValue(
                _numPadRowHandle,
                _numPadFieldName,
                qty
            );

            _numPadRowHandle = -1;
            _numPadFieldName = "";
        }

        private void BtnDel_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            Btn_Del();
        }

        private void Btn_Del()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
                
            dic.Add("Status", Convert.ToString(gvDetail.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(gcDetail, dic, "Del");
        }
        #endregion
    }
}