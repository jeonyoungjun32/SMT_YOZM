using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models.CM;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MM;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.BM.Popup
{
	public partial class Frm_PlacingItem : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SetData(DataRow dr);       //델리게이트 선언
        public event SetData onSetData;                 //계약


        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        string ItemType = "10,20,30,40,50,60,70";

        public string SdOrderNo;
        public string CustCode;
        public string PriceType;

        public Frm_PlacingItem()
        {
            InitializeComponent();

            Set_GvMaterial(GvPlacing);

            // 조회
            Display_PlacingMaterial();
        }

        public Frm_PlacingItem(string _CustCode, string _PriceType)
        {
            InitializeComponent();

            this.CustCode = _CustCode;
            this.PriceType = _PriceType;

            LupCustomer.smtLookUpDataSource = code.GetCustomerI_Lup(WrGlobal.CorpID, true);
            LupCustomer.smtLookUpDataDisplayMember = "거래처명";
            LupCustomer.smtLookUpDataValueMember = "거래처코드";
            LupCustomer.smtLookUpValue = _CustCode;

            Set_GvMaterial(GvPlacing);

            // 조회
            Display_PlacingMaterial();
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
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", new string[] { ItemType }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", new string[] { ItemType }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", new string[] { ItemType }), "코드", "명칭");

            //거래처
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");

            grid.Create_GvColumn(gv, "입고량", "InQty");
            grid.Create_GvColumn(gv, "발주잔량", "BalanceQty");
            grid.Create_GvColumn(gv, "발주번호", "MmOrderNo");
            grid.Create_GvColumn(gv, "순번", "MmOrderSeq");
            grid.Create_GvColumn(gv, "거래처", "CustCode", lup_SoldToCode);

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총액", "TotalPrice");


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

            // 수정가능
            grid.Column_AllowEdit(gv.Columns["InQty"]);

            // 고정
            gv.Columns["InQty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            // 포멧
            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["BalanceQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TInt);

            gv.ValidatingEditor += Gv_ValidatingEditor;
			gv.CellValueChanged += Gv_CellValueChanged;
        }
		private void Gv_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            string col = gv.FocusedColumn.FieldName;

            if (col == "InQty")
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
            if (e.Column.FieldName == "InQty")
            {
                float value = e.Value.ConvertFloat_Null_To_Zero();

                if (value == 0)
                {
                    GvPlacing.UnselectRow(e.RowHandle);
                }
                else
                {
                    GvPlacing.SelectRow(e.RowHandle);
                }
            }
        }

        //private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    GridView gv = sender as GridView;

        //    // 수량 변경 시 공급가 자동 계산
        //    if (e.Column.Name == "OrderQty")
        //    {
        //        Calcul(GvProduct);
        //    }
        //    gv.BestFitColumns();
        //}
        #endregion

        #region 조회
        private void Display_PlacingMaterial()
        {
            MM_PlacingD pd = new MM_PlacingD();
            pd.CustCode = LupCustomer.smtLookUpValue.ToString();

            GcPlacing.DataSource = pd.Select_Placing_Cust();
            GvPlacing.BestFitColumns();
        }

        #endregion


        #region 계산

        //private void Calcul(GridView gv)
        //{
        //    DataRow dr = gv.GetFocusedDataRow();

        //    // 공급가
        //    double qty = dr["OrderQty"].ConvertDouble_Null_To_Zero();
        //    double salesPrice = dr["UnitPrice"].ConvertDouble_Null_To_Zero();

        //    dr["Price"] = qty * salesPrice;
        //    dr["TotalPrice"] = (qty * salesPrice) * 1.1;
        //}
        #endregion

        #region  버튼이벤트

        private void btn_Check_Click(object sender, EventArgs e)
        {
            this.Validate();

            if (GvPlacing.SelectedRowsCount == 0) return;
            int[] selectedRows = GvPlacing.GetSelectedRows();

            DataTable dt = GvPlacing.GridControl.DataSource as DataTable;

            if (dt != null)
            {
                foreach (int row in selectedRows)
                {
                    DataRow dr = GvPlacing.GetDataRow(row);

                    int inQty = dr["InQty"].ConvertInt_Null_To_Zero();

                    if (inQty <= 0)
                    {
                        MessageBox.Show("입고량을 입력하세요.");
                        return;
                    }
                    onSetData(dr);
                }
            }
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Display_PlacingMaterial();
        }
        #endregion


    }
}