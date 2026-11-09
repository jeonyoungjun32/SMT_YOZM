using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SMT_MES.View.Business.Rpt;
using System;
using System.Data;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_Sales_TradingStatement : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsObtainOrder sales = new clsObtainOrder();
        Database db = new Database();
        string query = "";
        clsGrid grid = new clsGrid();

        public Frm_BM_Sales_TradingStatement()
        {
            InitializeComponent();
            SetDesign();
        }

        private void SetDesign()
        {
            SetSearchLookUp();
            Set_GridMaster(gvMaster);
            Set_GridDetail(gvDetail);
        }

        private void frmTradingStatement_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void SetSearchLookUp()
        {
            DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));
            barEdit.smtStartDateTimeValue = startDate;
            barEdit.smtEndDateTimeValue = endDate;

            ssCustomer.smtLookUpDataSource = code.GetCustomer(WrGlobal.CorpID, true).ToTable();
            ssCustomer.smtLookUpValue = "ALL";
        }

        private void Set_GridMaster(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            grid.Create_GvColumn(gv, "수주번호",        "SdOrderNo");
            grid.Create_GvColumn(gv, "수주처",          "CustName");
            grid.Create_GvColumn(gv, "수주일자",        "ObtainDate");
            grid.Create_GvColumn(gv, "납기일",          "DeliveryDate");
        }

        private void Set_GridDetail(GridView gv)
        {
            gv.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            grid.Create_GvColumn(gv, "수주번호",              "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번",              "SdOrderSeq");
            grid.Create_GvColumn(gv, "출고번호",              "SalesNo");
            grid.Create_GvColumn(gv, "출고순번",              "SalesSeq");
            grid.Create_GvColumn(gv, "출고일자",              "SalesDate");
            grid.Create_GvColumn(gv, "제품코드",              "ItemCode");
            grid.Create_GvColumn(gv, "제품명",                "ItemName");
            if (Database.DbName.ToUpper().Contains("DOOIL"))
            {
                grid.Create_GvColumn(gv, "통합품명", "SearchName");
            }
            grid.Create_GvColumn(gv, "수주수량",              "OrderQty");
            grid.Create_GvColumn(gv, "출고수량",              "OutQty");
            grid.Create_GvColumn(gv, "단위",                  "UnitCode");
            grid.Create_GvColumn(gv, "단가",                  "UnitPrice");
            grid.Create_GvColumn(gv, "공급가",                "Price");
            grid.Create_GvColumn(gv, "부가세",                "VatPrice");
            grid.Create_GvColumn(gv, "총금액",                "TotalPrice");

            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);
        }

        private void DisplayData()
        {
            string type = Convert.ToString(barEdit.smtComboTitleValue);
            string startDate = barEdit.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = barEdit.smtEndDateTimeValue.ConvertDate_To_Char10();
            string customer = Convert.ToString(ssCustomer.smtLookUpValue);

            BM_SdOrderM master = new BM_SdOrderM();
            gcMaster.DataSource = master.Select_BM_SdOrderM(type, startDate, endDate, customer);
            gvMaster.BestFitColumns();
        }

        private void DisplayDetailList()
        {
            DataTable dt = gcMaster.DataSource as DataTable;
            if (dt == null) return;

            string sdOrderNo = (gvMaster.GetFocusedRowCellValue("SdOrderNo") ?? "").ToString();

            BM_SalesM sm = new BM_SalesM();
            gcDetail.DataSource = sm.Select_TradingStatementDetail(sdOrderNo);
            gvDetail.BestFitColumns();

            for (int i = 0; i < gvDetail.RowCount; i++)
            {
                gvDetail.SelectRow(i);
            }
        }

        private void gvMaster_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DisplayDetailList();
        }


        // 포커스된 행 색상
        private void gvMaster_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                //Apply the appearance of the SelectedRow
                e.Appearance.Assign(view.PaintAppearance.SelectedRow);
                //Just to illustrate how the code works. Remove the following lines to see the desired appearance.
                e.Appearance.Options.UseForeColor = true;
            }//end if
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
            DisplayDetailList();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            print();
        }

        private void print()
        {
            GridView gv = gvDetail;
            int[] selRow = gv.GetSelectedRows();

            if (selRow.Length < 1)
            {
                XtraMessageBox.Show("선택된 내역이 없습니다.");
                return;
            }

            DataTable dt = gcDetail.DataSource as DataTable;
            DataTable selDataTable = dt.Copy().Clone();

            for (int i = 0; i < gv.SelectedRowsCount; i++)
            {
                DataRow dr = selDataTable.NewRow();
                dr.ItemArray = gv.GetDataRow(selRow[i]).ItemArray;

                selDataTable.Rows.Add(dr);
            }

            //업체별 거래명세서 옵션
            string rptNm = "";

            // SC_Switch 테이블에서 "InVoice" 키의 값 조회
            if (WrGlobal.ProcOption["BN"].ContainsKey("InVoice"))
            {
                rptNm = WrGlobal.ProcOption["BN"]["InVoice"];
            }

            XtraReport report = null;

            switch (rptNm)
            {
                case "Rpt_InVoice":
                    report = new Rpt_InVoice(selDataTable, barEdit.smtEndDateTimeValue.ConvertDate_To_Char10());
                    break;

                default:
                    report = new Rpt_InVoice(selDataTable, barEdit.smtEndDateTimeValue.ConvertDate_To_Char10());
                    break;
            }

            ReportPrintTool reportTool = new ReportPrintTool(report);
            reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            reportTool.ShowPreviewDialog();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}