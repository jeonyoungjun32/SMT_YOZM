using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using SmartCommon.Common;
using SmartMES.Models.MM;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;

namespace SmartMES.View.MM
{
    public partial class Frm_MM_PurchaseView : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsCode code = new clsCode();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        string query = "";

        string[] ItemType = { "40", "50", "60", "70"};  // 자재품목코드 

        public Frm_MM_PurchaseView()
        {
            InitializeComponent();
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));

            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Cust.smtLookUpDataSource = code.GetCustomerI_Lup(WrGlobal.CorpID, true);
            Lup_Cust.smtLookUpDataDisplayMember = "거래처명";
            Lup_Cust.smtLookUpDataValueMember = "거래처코드";
            Lup_Cust.smtLookUpValue = "ALL";

            Lup_Item.smtLookUpDataSource = code.GetMaterialList(null, true).ToTable();
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";

            Set_GridView(gvList);
            DisplayData();
        }

        private void Set_GridView(GridView gv)
		{
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsView.ShowGroupPanel = false;

            gv.IndicatorWidth = 50;                     //순번 너비

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            //
            grid.Create_GvColumn(gv, "입고번호", "StoNo");
            grid.Create_GvColumn(gv, "입고순번", "StoSeq");
            grid.Create_GvColumn(gv, "거래처", "CustName");
            grid.Create_GvColumn(gv, "입고일자", "StoDate");

            grid.Create_GvColumn(gv, "원자재구분", "ItemTypeName");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "입고수량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "합계", "TotalPrice");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //SUMMARY
            grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            //EVENT
            grid.Set_Navigator(gv);
            grid.RowNumber_Indicator(gv);
		}

        private void CmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

		private void DisplayData()
		{
			string custCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();
            string ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            string startDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();

            MM_PurchaseD pd = new MM_PurchaseD();
            pd.CorpId = WrGlobal.CorpID;
			pd.CustCode = custCode;
			pd.ItemCode = ItemCode;
			gcList.DataSource = pd.SelectMM_PurchaseD(startDt, endDt);

			gvList.BestFitColumns();
		}

		private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvList.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }//end fnction
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
