using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Model;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.BM;
using SmartPOP.Report;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace SmartPOP.Views.BM
{
    public partial class Frm_TradingStatement : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        string ItemDiv = "20";
        string[] CustDiv = new string[] { "20" };

        public Frm_TradingStatement()
        {
            InitializeComponent();
            SetDesign();
        }

        private void SetDesign()
        {
            SetSearchLookUp();

            SetGridView_Sales(gvBM_SalesD);
        }
        private void frmTradingStatement_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void SetSearchLookUp()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            barDate.smtStartDateTimeValue = startDate;
            barDate.smtEndDateTimeValue = endDate;

            //달력 키우기
            rpo.Set_POP_Dtp(barDate.smtStartDateEdit);
            rpo.Set_POP_Dtp(barDate.smtStartEndEdit);

            DataTable dtCust = code.smtLookUp("거래처", "매출", "전체", "");
            ssCustomer.smtLookUpDataSource = dtCust;
            ssCustomer.smtLookUpDataDisplayMember = "거래처명";
            ssCustomer.smtLookUpDataValueMember = "거래처코드";
            ssCustomer.smtLookUpValue = "ALL";

            //SearchLookUp font size
            ssCustomer.smtLookUpView.Appearance.Row.Font = new Font("맑은 고딕", 18, FontStyle.Regular);         //본문row
            ssCustomer.smtLookUpView.Appearance.HeaderPanel.Font = new Font("맑은 고딕", 18, FontStyle.Regular); //헤더
        }

        private void SetGridView_Sales(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;

            //
            gv.OptionsBehavior.ReadOnly = true;

            //
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", ItemDiv, "", ""), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "출하번호", "SalesNo");
            grid.Create_GvColumn(gv, "출하순번", "SalesSeq");
            grid.Create_GvColumn(gv, "출하일자", "SalesDate");
            //grid.Create_GvColumn(gv, "거래처", "BillToCode", lup_SoldToCode);
            grid.Create_GvColumn(gv, "거래처코드", "BillToCode");
            grid.Create_GvColumn(gv, "거래처명", "BillToName");
            grid.Create_GvColumn(gv, "납품처코드", "ShipToCode");
            grid.Create_GvColumn(gv, "납품처명", "ShipToName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "창고", "HouseCode");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository 생성 or 이벤트 입력
            //gv.Columns["SalesNo"].Visible = false;
            //gv.Columns["SalesSeq"].Visible = false;
            gv.Columns["Status"].Visible = false;
            gv.Columns["HouseCode"].Visible = false;

            //Column 단일 설정

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("MaterialClass1") != null) gv.Columns["MaterialClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("MaterialClass2") != null) gv.Columns["MaterialClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("MaterialClass3") != null) gv.Columns["MaterialClass3"].ColumnEdit = lup_Class_Cd3;
            //셀값 변경 허용

            if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] != "Y")
            {
                gv.Columns["ShipToCode"].Visible = false;
                gv.Columns["ShipToName"].Visible = false;

            }

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;     //상태
            grid.SetCol_Type(gv.Columns["Qty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "TotalPrice");

            //셀 내부 서치룩업 크기 조정
            grid.SetPOPGrid(gv);
            grid.SetPOPCellLup(lup_UnitCode);
            grid.SetPOPCellLup(lup_SoldToCode);
            grid.SetPOPCellLup(lup_Emp_No);
            grid.SetPOPCellLup(lup_Item);

            //칼럼 너비 설정
            gv.Columns["Status"].MaxWidth = 60;

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

    
		private void cmdDisplay_Click(object sender, EventArgs e)
		{
            Display();
        }

        private void Display()
        {
            this.Validate();

            string startDate = barDate.smtStartDateTimeValue.ToString("d");
            string endDate = barDate.smtEndDateTimeValue.ToString("d");
            string custCode = ssCustomer.smtLookUpValue.ConvertString_Null_To_Empty();

            BM_SalesD sd = new BM_SalesD();
            sd.CustCode = custCode;
            gcBM_SalesD.DataSource = sd.Select_BM_SalesD(startDate, endDate);

            gvBM_SalesD.BestFitColumns();
        }

        private void btnPrint_Click(object sender, EventArgs e)
		{
            GridControl gc = gcBM_SalesD;

            DataTable dt = gc.DataSource as DataTable;
            if (dt == null) return;
            if (dt.Rows.Count < 1) return;

            GridView gv = gc.MainView as GridView;
            if (gv == null) return;

            this.Validate();

            //if (gv == Gv_ROder)
            //{
            //    cls_MessageBox.Show("수주조회 상태에서 출력 할 수 없습니다.", "");
            //    return;
            //}

            int[] selrow = gv.GetSelectedRows();
            if (selrow.Length < 1)
            {
                cls_MessageBox.Show("선택한 내역이 없습니다", "");
                return;
            }

            DataTable selDataTable = dt.Copy().Clone();

			for (int cnt = 0; cnt < gv.SelectedRowsCount; cnt++)
			{
				DataRow dr = selDataTable.NewRow();
				dr.ItemArray = gv.GetDataRow(selrow[cnt]).ItemArray;
				//dr["Item_Nm"] = gv.GetRowCellDisplayText(selrow[cnt], "Item_Nm");

				selDataTable.Rows.Add(dr);
			}

			DataView dv = new DataView(selDataTable);
            dv.Sort = "SalesDate ASC, BillToCode ASC";


            //업체별 거래명세서 옵션
            string rptNm = "";

            if (WrGlobal.ProcOption["BN"].ContainsKey("InVoice"))
            {
                rptNm = WrGlobal.ProcOption["BN"]["InVoice"];
            }

            XtraReport report = null;
            string printDate = DateTime.Now.ToString("d");

            switch (rptNm)
            {
                case "Rpt_InVoice":
                    report = new Rpt_InVoice(dv.ToTable(), printDate);
                    break;

                //case "Rpt_InVoice_UAG":S
                //    report = new Rpt_InVoice_UAG(dv.ToTable(), printDate);
                //    break;

                default:
                    report = new Rpt_InVoice(dv.ToTable(), printDate);
                    break;
            }

            ReportPrintTool reportTool = new ReportPrintTool(report);
            reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            reportTool.ShowPreviewDialog();
        }

		private void btnClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}