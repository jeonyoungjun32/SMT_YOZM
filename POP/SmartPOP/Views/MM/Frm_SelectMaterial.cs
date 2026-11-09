using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.BM;
using SmartPOP.Models.CM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.MM
{
	public partial class Frm_SelectMaterial : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SetData(DataRow dr);       //델리게이트 선언
        public event SetData onSetData;                 //계약


        cls_Repository rpo = new cls_Repository();
        clsCode code = new clsCode();
        CM_ItemPrice cmp = new CM_ItemPrice();
        cls_Grid grid = new cls_Grid();
        string ItemType = "10,20,30,40,50,60,70";
        string[] ItemDiv = new string[] { "40", "50" };  //원자재, 부자재

        public string SdOrderNo;
        public string CustCode;
        public string PriceType;

        HashSet<int> Select_List = new HashSet<int>();      // 클릭 순서 저장

        public Frm_SelectMaterial()
        {
            InitializeComponent();

            Set_GvMaterial(GvMaterial);
            Set_GvProduct(GvProduct);

            // 조회
            Display_MaterialPrice();
            Display_Prod();
        }

        public Frm_SelectMaterial(string _CustCode, string _PriceType)
        {
            InitializeComponent();

            this.CustCode = _CustCode;
            this.PriceType = _PriceType;

            Set_GvMaterial(GvMaterial);
            Set_GvProduct(GvProduct);

            // 조회
            Display_MaterialPrice();
            Display_Prod();
        }

        #region 디자인
        private void Set_GvMaterial(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            // 단위
            CM_CateItem cci = new CM_CateItem();
            RepositoryItemSearchLookUpEdit lup_Unit = rpo.Create_RepositoryItemSearchLookUp(cci.SelectCateItem("A011"), "코드", "명칭");

            //품목분류
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210", new string[] { ItemType }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220", new string[] { ItemType }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230", new string[] { ItemType }), "코드", "명칭");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            //품목분류
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null)
            {
                gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            }

            if (gv.Columns.ColumnByFieldName("ItemClass2") != null)
            {
                gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            }

            if (gv.Columns.ColumnByFieldName("ItemClass3") != null)
            {
                gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;
            }

            gv.SelectionChanged += Gv_SelectionChanged;     // 체크 순서 저장
        }

        private void Btn_Del_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView gv = GvProduct;
            gv.DeleteRow(gv.FocusedRowHandle);
        }

        private void Gv_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            // 전체 리프레시(헤더 체크박스로 전체 선택/해제 등)
            if (e.Action == CollectionChangeAction.Refresh)
            {
                Select_List.Clear();

                int[] selected = gv.GetSelectedRows();
                foreach (int handle in selected)
                {
                    if (handle >= 0)
                        Select_List.Add(handle);
                }
                return;
            }

            // 단일 행 선택/해제
            int rowHandle = e.ControllerRow;
            if (rowHandle < 0)
                return;

            if (e.Action == CollectionChangeAction.Add)
            {
                // ✅ 체크됨 → 리스트에 추가
                Select_List.Add(rowHandle);
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                // ⛔ 체크 해제 → 리스트에서 제거
                Select_List.Remove(rowHandle);
            }
        }

        private void Set_GvProduct(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            cls_Repository rpo = new cls_Repository();
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(), "품목코드", "품명");

            //  10 매입  20	매출  30 외주   40 매입매출
            RepositoryItemSearchLookUpEdit lup_ShipCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(new string[] { "20", "40" }), "거래처코드", "거래처명");
            
            // 단위
            CM_CateItem cci = new CM_CateItem();
            RepositoryItemSearchLookUpEdit lup_Unit = rpo.Create_RepositoryItemSearchLookUp(cci.SelectCateItem("A011"), "코드", "명칭");

            // 삭제 버튼
            EditorButton Btn_Del_edit = new EditorButton();
            Btn_Del_edit.Kind = ButtonPredefines.Delete;

            RepositoryItemButtonEdit Btn_Del = new RepositoryItemButtonEdit();
            Btn_Del.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Del.Buttons.RemoveAt(0);
            Btn_Del.Buttons.Add(Btn_Del_edit);
            Btn_Del.ButtonClick += Btn_Del_ButtonClick;

            grid.Create_GvColumn(gv, "순번", "SdOrderSeq");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "품번", "Alias");
            grid.Create_GvColumn(gv, "규격", "Size");
            grid.Create_GvColumn(gv, "단위", "UnitCode", lup_Unit);
            grid.Create_GvColumn(gv, "품목분류1", "ItemClass1");
            grid.Create_GvColumn(gv, "품목분류2", "ItemClass2");
            grid.Create_GvColumn(gv, "품목분류3", "ItemClass3");
     
            grid.Create_GvColumn(gv, "수량", "OrderQty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "총액", "TotalPrice");
            grid.Create_GvColumn(gv, "출고처", "ShipToCode", lup_ShipCust);
            grid.Create_GvColumn(gv, "비고", "SalesRemark");
            grid.Create_GvColumn(gv, "삭제", "btnDel", Btn_Del);

            // 고정
            gv.Columns["btnDel"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            // 수정가능
            grid.Column_AllowEdit(gv.Columns["OrderQty"]);
            //grid.Column_AllowEdit(gv.Columns["SalesPrice"]);
            grid.Column_AllowEdit(gv.Columns["ShipToCode"]);
            grid.Column_AllowEdit(gv.Columns["SalesRemark"]);
            grid.Column_AllowEdit(gv.Columns["btnDel"]);

            // 포멧
            grid.SetCol_Type(gv.Columns["OrderQty"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["UnitPrice"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Price"], cls_Grid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TInt);

            // 숨김
            gv.Columns["SdOrderSeq"].Visible = false;
            //gv.Columns["Price"].Visible = false;
            //gv.Columns["TotalPrice"].Visible = false;
            gv.Columns["SalesRemark"].Visible = false;
            gv.Columns["Alias"].Visible = false;
            gv.Columns["Size"].Visible = false;
            gv.Columns["UnitCode"].Visible = false;
            gv.Columns["ItemClass1"].Visible = false;
            gv.Columns["ItemClass2"].Visible = false;
            gv.Columns["ItemClass3"].Visible = false;
            gv.Columns["ShipToCode"].Visible = false;
            gv.Columns["UnitPrice"].Visible = false;
            gv.Columns["Price"].Visible = false;
            gv.Columns["TotalPrice"].Visible = false;

            //프로세스 옵션 : 납품처 사용, 미사용 -> 미사용 시 거래처로 자동 입력
            if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] == "N")
            {
                // 출고처 Visble = false처리
                gv.Columns["ShipToCode"].Visible = false;
            }

            // 이벤트
            gv.CellValueChanged += Gv_CellValueChanged;     // 셀값 변경
            gv.ValidatingEditor += Gv_ValidatingEditor;
        }

        private void Gv_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            string col = gv.FocusedColumn.FieldName;

            if (col == "OrderQty")
            {
                string value = e.Value.ToString() ?? "";
                if (value.Length > 8)
                {
                    e.Valid = false;
                    e.ErrorText = "최대 8자리까지 입력할 수 있습니다.";
                }
            }
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            
            // 수량 변경 시 공급가 자동 계산
            if (e.Column.Name == "OrderQty")
            {
                Calcul(GvProduct);
            }

            //if (e.Column.Name == "SalesPrice")
            //{
            //    Calcul(GvProduct);
            //}

            gv.BestFitColumns();
        }
        #endregion

        #region 조회
        private void Display_MaterialPrice()
        {
            if (checkBoxALL.Checked == false)
            {
                //거래처별 품목단가에 있는 품목 리스트 조회
                CM_ItemPrice cmp = new CM_ItemPrice();
                cmp.CustCode = CustCode;
                cmp.PriceType = PriceType;  // 10 매입 20 매출
                cmp.ApplyDate = DateTime.Now.ToString("d");

                GcMaterial.DataSource = cmp.Select_MaterialPrice_Cust(ItemDiv);
                GvMaterial.BestFitColumns();
            }
            else
            {
                // 전체 품목 조회
                CM_Item cm = new CM_Item();
                GcMaterial.DataSource = cm.SelectCM_Item(ItemDiv);
                GvMaterial.BestFitColumns();
            }
        }

        private void Display_Prod()
        {
            BM_SdOrderD sod = new BM_SdOrderD();

            sod.SdOrderNo = this.SdOrderNo;
            sod.CustCode = this.CustCode;

            GcProduct.DataSource = sod.Select_SdOrderByProduct();
            GvProduct.BestFitColumns();
        }

        private void Ins_Product()
        {
            GridView gv = GvProduct;

            string ItemCode;
            string ItemName;
            string alias;

            DataTable dt = gv.GridControl.DataSource as DataTable;

            if (dt != null)
            {
                foreach(int idx in Select_List)
                {
                    ItemCode = GvMaterial.GetRowCellValue(idx, "ItemCode").ConvertString_Null_To_Empty();
                    ItemName = GvMaterial.GetRowCellValue(idx, "ItemName").ConvertString_Null_To_Empty();
                    alias = GvMaterial.GetRowCellValue(idx, "Alias").ConvertString_Null_To_Empty();

                    // 같은 항목 제외
                    //if (dt.Select($"ItemCode = '{ItemCode}'").Length > 0) continue;

                    // 기본값 셋팅
                    DataRow dr = dt.NewRow();
                    dr["ItemCode"] = ItemCode;                                      // ItemCode 컬럼에 값 추가
                    dr["ItemName"] = ItemName;
                    dr["Alias"] = alias;                                                    // 품번
                    dr["Size"] = GvMaterial.GetRowCellValue(idx, "Size").ConvertString_Null_To_Empty();
                    dr["UnitCode"] = GvMaterial.GetRowCellValue(idx, "UnitCode").ConvertString_Null_To_Empty(); // 단위
                    dr["ItemClass1"] = GvMaterial.GetRowCellValue(idx, "ItemClass1").ConvertString_Null_To_Empty();
                    dr["ItemClass2"] = GvMaterial.GetRowCellValue(idx, "ItemClass2").ConvertString_Null_To_Empty();
                    dr["ItemClass3"] = GvMaterial.GetRowCellValue(idx, "ItemClass3").ConvertString_Null_To_Empty();
                    dr["OrderQty"] = 0;                                                     // 수량
                    dr["UnitPrice"] = GvMaterial.GetRowCellValue(idx, "Price").ConvertInt_Null_To_Zero();                                                        
                    dr["Price"] = 0;                                                        // 공급가
                    dr["ShipToCode"] = null;                                                // 출고처
                    dr["Remark"] = null;                                                    // 비고
                    //dr["SalesRemark"] = GvMaterial.GetRowCellValue(idx, "Remark").ConvertString_Null_To_Empty(); // 품목비고                                                   

                    dt.Rows.Add(dr);
                }
                gv.BestFitColumns();
            }
        }
        #endregion

        #region 주문서에 추가

        private bool Chk_Order(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("품목을 등록하세요.");
                return false;
            }

            int orderQty, qty_Kg;

            foreach (DataRow dr in dt.Rows)
            {
                orderQty = dr["OrderQty"].ConvertInt_Null_To_Zero();

                if (orderQty <= 0)
                {
                    MessageBox.Show("수량을 입력하세요.");
                    return false;
                }
            }

            //프로세스 옵션 : 납품처 사용, 미사용 -> 미사용 시 거래처로 자동 입력
            //         if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] == "Y")
            //         {
            //             if (dt.Select($@"ShipToCode IS NULL
            //                         OR 
            //                         ShipToCode = ''
            //             ").Length > 0)
            //             {
            //                 MessageBox.Show("출고처를 등록하세요.");
            //                 return false;
            //             }
            //         }
            //else
            //{
            //	foreach (DataRow item in dt.Rows)
            //	{
            //                 if(item["ShipToCode"].ConvertString_Null_To_Empty() == "")
            //		{
            //                     item["ShipToCode"] = this.CustCode;
            //		}
            //	}
            //}

            return true;
        }
        #endregion

        #region 계산

        private void Calcul(GridView gv)
        {
            DataRow dr = gv.GetFocusedDataRow();

            // 공급가
            double qty = dr["OrderQty"].ConvertDouble_Null_To_Zero();
            double salesPrice = dr["UnitPrice"].ConvertDouble_Null_To_Zero();

            dr["Price"] = qty * salesPrice;
            dr["TotalPrice"] = (qty * salesPrice) * 1.1;
        }
        #endregion

        #region  버튼이벤트

        private void btn_Ins_Click(object sender, EventArgs e)
        {
            //품목 주문서에 추가
            if (GvMaterial.SelectedRowsCount == 0) return;

            Ins_Product();

            // 셀렉트 리스트 초기화
            GvMaterial.ClearSelection();
            Select_List.Clear();
        }

        private void btn_Check_Click(object sender, EventArgs e)
        {
            this.Validate();

            DataTable dt = GvProduct.GridControl.DataSource as DataTable;

            if (!Chk_Order(dt)) return;

            foreach (DataRow dr in dt.Rows)
            {
                onSetData(dr);
            }

            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GcMaterial_DoubleClick(object sender, EventArgs e)
        {
            GridView gv = GvProduct;
            DataTable dt = gv.GridControl.DataSource as DataTable;

            string ItemCode = GvMaterial.GetFocusedRowCellValue("ItemCode").ConvertString_Null_To_Empty();
            string ItemName = GvMaterial.GetFocusedRowCellValue("ItemName").ConvertString_Null_To_Empty();

            // 같은 항목 존재 여부 확인
            bool isExists = dt.Select($"ItemCode = '{ItemCode}'").Length > 0;
            if (isExists)
            {
                // 이미 존재하면 추가하지 않음
                return;
            }

            // 기본값 셋팅
            DataRow dr = dt.NewRow();
            dr["ItemCode"] = ItemCode;
            dr["ItemName"] = ItemName;
            dr["OrderQty"] = 0;                                                     
            dr["Price"] = 0;                                                        
            dr["ShipToCode"] = null;                                                
            dr["Remark"] = null;

            //dr["Qty_Kg"] = 0;
            dr["UnitWeight"] = GvMaterial.GetFocusedRowCellValue("UnitWeight").ConvertDecimal_Null_To_Zero();
            dr["Size"] = GvMaterial.GetFocusedRowCellValue("Size").ConvertString_Null_To_Empty();

            //dr["SalesRemark"] = GvMaterial.GetFocusedRowCellValue("Remark").ConvertString_Null_To_Empty(); // 비고

            dt.Rows.Add(dr);

            gv.BestFitColumns();
        }

		private void checkBoxALL_CheckedChanged(object sender, EventArgs e)
		{
			Display_MaterialPrice();
		}
		#endregion

	}
}