using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using SmartCommon.Common;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using SmartMES.Models.PM;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_ProductPlanView: DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        string ItemType = WrGlobal.ProcOption["SC"]["Product_ItemType"];

        public Frm_PM_ProductPlanView()
        {
            InitializeComponent();
        }

        private void frmPM_ProductPlanView_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();

            DisplayData();
        }

        #region setDesign
        private void SetLup()
        {
            DtOrder.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            DtOrder.smtEndDateTimeValue = DateTime.Now;

            Lup_Material.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");
            Lup_Material.smtLookUpDataDisplayMember = "품명";
            Lup_Material.smtLookUpDataValueMember = "품목코드";
            Lup_Material.smtLookUpValue = "ALL";

            //Lup_Cust.smtLookUpDataSource = code.GetCustomer_Lup(WrGlobal.CorpID, true);
            //Lup_Cust.smtLookUpDataDisplayMember = "내용";
            //Lup_Cust.smtLookUpDataValueMember = "코드";
            //Lup_Cust.smtLookUpValue = "ALL";
        }

        private void SetGridView()
		{
            SetGridPM_ProductPlan(GvPM_ProductPlan);
		}

        private void SetGridPM_ProductPlan(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

			clsRepository rpo = new clsRepository();
			//라인
			RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");
            //품목
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] {"10" ,"20", "30" }), "품목코드", "품명");
            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
			//설비
			RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //거래처
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(), "거래처코드", "거래처명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "구분", "UrgentYn");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
			grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");
			grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "작업일자", "WorkDate", rpo.Create_rpoDate());

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            //grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "거래처", "SoldToCode", lupCust);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);

            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "생산률(%)", "WorkRate");
            grid.Create_GvColumn(gv, "창고수량", "StockQty");

            grid.Create_GvColumn(gv, "비고", "Remark");

            //
            gv.Columns["ItemName"].ColumnEdit = lup_material;
            if (WrGlobal.CdOption["제품코드 초기화"]["ItemClass1"].DispYn == true)
            {
                gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            }
            if (WrGlobal.CdOption["제품코드 초기화"]["ItemClass2"].DispYn == true)
            {
                gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            }
            if (WrGlobal.CdOption["제품코드 초기화"]["ItemClass3"].DispYn == true)
            {
                gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;
            }
            if (WrGlobal.CdOption["제품코드 초기화"]["UnitCode"].DispYn == true)
            {
                gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            }


            // 포맷
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkRate"], clsGrid.TypeList.TInt);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["UrgentYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //visible
            gv.Columns["Status"].Visible = false;
            gv.Columns["SdOrderNo"].Visible = false;
            gv.Columns["SdOrderSeq"].Visible = false;
            gv.Columns["StockQty"].Visible = false;
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["MachineCode"].Visible = false;

            if (Database.DbName.ToUpper().Contains("DOOIL"))
            {
                gv.Columns["SoldToCode"].Visible = false;
            }

            grid.Set_Navigator(GvPM_ProductPlan);
            gv.CustomDrawCell += gv_CustomDrawCell;
        }

        private void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("UrgentYn") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "긴급")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.Transparent;
            }
        }

        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            baseSMT.smtSearchButton Btn = (baseSMT.smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                //조회
                case "cmdDisplay":
                    DisplayData();
                    break;

                //엑셀다운
                case "cmdExcel":
                    cmdExcel_Click();
                    break;
            }
        }

        #region Event
        //엑셀변환 하기
        private void cmdExcel_Click()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GvPM_ProductPlan.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Event

        #region 조회
        private void DisplayData()
        {
            PM_ProductPlan wo = new PM_ProductPlan();

            string startDate = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char8();
            wo.ItemCode = Lup_Material.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Material.smtLookUpValue;

            GcPM_ProductPlan.DataSource = wo.Select_PrepPM_ProductPlan_SdOrder(startDate, endDate);

            GvPM_ProductPlan.BestFitColumns();
        }
        #endregion 조회
	}
}