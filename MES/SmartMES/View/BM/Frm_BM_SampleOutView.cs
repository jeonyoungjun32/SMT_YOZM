using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Report;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_SampleOutView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        
        public delegate void CloseForm(object sender, EventArgs e);
        public CloseForm OnCloseForm;

        public Frm_BM_SampleOutView()
        {
            InitializeComponent();
        }

        private void frmSalesView_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            code.LookupCustomer(WrGlobal.CorpID, "매출", true, "", ref Lup_Cust);
            Lup_Cust.smtLookUpValue = "ALL";

            //DisplayOption();
            SetGridView_SampleOut(gvSales);
            DisplayData();
        }

        private void SetGridView_SampleOut(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;                                    //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsBehavior.ReadOnly = true;      // 조회전용 화면이니 편집 막기

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", "", "", ""), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");
            RepositoryItemSearchLookUpEdit lup_WareHouse = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");
            
            //Column 생성
            grid.Create_GvColumn(gv, "출고번호", "OutNo");
            grid.Create_GvColumn(gv, "출고순번", "OutSeq");
            grid.Create_GvColumn(gv, "출고일자", "OutDate");
            grid.Create_GvColumn(gv, "납품처", "ShipToCode", lup_SoldToCode);
            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");
            grid.Create_GvColumn(gv, "수량", "Qty", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "제조번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "유효기간", "ExpirationDate");
            grid.Create_GvColumn(gv, "바코드", "BarCodeNo");
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository
            if (gv.Columns.ColumnByFieldName("ItemCode") != null) gv.Columns["ItemCode"].ColumnEdit = lup_Item;
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //VISIBLE
            if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] != "Y")
            {
                gv.Columns["ShipToCode"].Visible = false;
            }

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "TotalPrice");

            //칼럼 너비 설정

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            this.Validate();

            string startDate = string.Format("{0:yyyy-MM-dd}", Dt_Date.smtStartDateTimeValue);
            string endDate = string.Format("{0:yyyy-MM-dd}", Dt_Date.smtEndDateTimeValue);
            string shipToCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();


            BM_SampleOutM sd = new BM_SampleOutM();
            sd.ShipToCode = shipToCode;
            gcSales.DataSource = sd.Select_BM_SampleOutM(startDate, endDate);

            gvSales.BestFitColumns();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            rptInvoice report = new rptInvoice();

            ReportPrintTool reportTool = new ReportPrintTool(report);
            reportTool.PreviewForm.PrintControl.Zoom = 0.9f;

            reportTool.ShowPreviewDialog();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvSales.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}