using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Data;
using System.Diagnostics;
using SmartCommon.Common;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using SmartMES.Models.PM;
using SmartMES.Models.MM;

namespace SmartMES.View.MM
{
    public partial class Frm_MM_PlacingView : DevExpress.XtraEditors.XtraForm
    {
        clsRepository rpo = new clsRepository();
        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();

        Database db = new Database();
        string query = "";

        public Frm_MM_PlacingView()
        {
            InitializeComponent();
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            SetInit();
            Set_Grid(gvPlacingList);
            //DisplayOption();

            DisplayData();
        }

        private void SetInit()
		{
            DateTime endDate = DateTime.Now.Date;
            DateTime startDate = endDate.AddDays(-7);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            //상태 설정
            query = $@"select '전체' 상태
                        Union All
                        select '진행' 상태
                        Union All
                        select '마감' 상태
                        Union All
                        select '완료' 상태";

            Lup_State.smtLookUpDataSource = db.GetDataView("상태", query).ToTable();
            Lup_State.smtLookUpDataDisplayMember = "상태";
            Lup_State.smtLookUpDataValueMember = "상태";
            Lup_State.smtLookUpValue = "전체";
            
            Lup_PlacingType.smtLookUpDataSource = code.GetCM_CateItemALL(WrGlobal.CorpID, "I010", null, true);
            Lup_PlacingType.smtLookUpDataDisplayMember = "명칭";
            Lup_PlacingType.smtLookUpDataValueMember = "코드";
            Lup_PlacingType.smtLookUpValue = "ALL";
            Lup_PlacingType.Visible = false; //발주구분 안써서 숨김

            Lup_Cust.smtLookUpDataSource = code.GetCustomerI_Lup(WrGlobal.CorpID, true);
            Lup_Cust.smtLookUpDataDisplayMember = "거래처명";
            Lup_Cust.smtLookUpDataValueMember = "거래처코드";
            Lup_Cust.smtLookUpValue = "ALL";
        }

        private void Set_Grid(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.IndicatorWidth = 50;                     //순번 너비

            gv.OptionsView.ShowGroupPanel = false;

            //거래처
            RepositoryItemSearchLookUpEdit lup_Cust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomerList(new string[] { "10", "40" }, false), "거래처코드", "거래처명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "발주번호", "MmOrderNo");
            grid.Create_GvColumn(gv, "발주순번", "MmOrderSeq");
            grid.Create_GvColumn(gv, "발주일자", "PlacingDate");
            grid.Create_GvColumn(gv, "발주구분", "PlacingType");
            grid.Create_GvColumn(gv, "거래처", "CustCode", lup_Cust);

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "발주수량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "부가세타입", "VatTitle");
            grid.Create_GvColumn(gv, "부가세율", "VatRate");
            grid.Create_GvColumn(gv, "입고수량", "InQty");
            grid.Create_GvColumn(gv, "미입고수량", "BalanceQty");
            grid.Create_GvColumn(gv, "로스율", "LossRate");
            grid.Create_GvColumn(gv, "납기일자", "DeliveryDate");
            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "마감일자", "CloseDate");
            grid.Create_GvColumn(gv, "마감담당자", "CloseUser");

            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

            //VISIBLE
            //gv.Columns["PlacingType"].Visible = false;

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatRate"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BalanceQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["LossRate"], clsGrid.TypeList.TFloat);

            grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        private void gvMachine_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayData();
        }

        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }


        private void DisplayData()
        {
            try
            {
                string state = Lup_State.smtLookUpValue.ConvertString_Null_To_Empty();
                string custCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();
                string placingType = Lup_PlacingType.smtLookUpValue.ConvertString_Null_To_Empty();
                placingType = placingType == "ALL" ? "" : placingType;

                string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
                string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();

                MM_PlacingD pd = new MM_PlacingD();
                pd.State = state == "전체" ? "" : state;
                pd.CustCode = custCode.ToUpper() == "ALL" ? "" : custCode;
                
                gcPlacingList.DataSource = pd.Select_PlacingList(startDate, endDate, placingType);
                gvPlacingList.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvPlacingList.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }//end fnction
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}