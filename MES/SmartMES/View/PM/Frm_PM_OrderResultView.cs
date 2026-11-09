using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using SmartCommon.Common;
using System.Data;
using DevExpress.XtraEditors.Repository;
using System.Windows.Forms;
using System.Diagnostics;
using SmartMES.Models.PM;
using SmartMES.Models.CM;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_OrderResultView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        Database db = new Database();
        CM_Process process = new CM_Process();

        string ItemType = WrGlobal.ProcOption["SC"]["Product_ItemType"];

        public Frm_PM_OrderResultView()
        {
            InitializeComponent();
        }

        private void frmPM_OrderResultView_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();
            DisplayData();
        }

        #region setDesign
        private void SetLup()
		{
            DtResult.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            DtResult.smtEndDateTimeValue = DateTime.Now;

            Lup_Material.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");
            Lup_Material.smtLookUpDataDisplayMember = "품명";
            Lup_Material.smtLookUpDataValueMember = "품목코드";
            Lup_Material.smtLookUpValue = "ALL";

            Lup_Process.smtLookUpDataSource = process.SelectCM_ProcessLookup();
            Lup_Process.smtLookUpDataDisplayMember = "공정명";
            Lup_Process.smtLookUpDataValueMember = "코드";
            Lup_Process.smtLookUpValue = "ALL";
        }

        private void SetGridView()
		{
            SetGridPM_ProductPlan(GvPM_OrderResult);
		}

        private void SetGridPM_ProductPlan(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            //품목
            //RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] { "10", "20","30","60"}), "품목코드", "품목코드");
            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");

            grid.Create_GvColumn(gv, "작업지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "작업지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "시리얼넘버", "SerialNo");
            grid.Create_GvColumn(gv, "작업순번", "SubSeq");

            grid.Create_GvColumn(gv, "실적일자", "WorkDate");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName", lup_ProcessCode);

            grid.Create_GvColumn(gv, "작업시작시간", "StartDate");
            grid.Create_GvColumn(gv, "작업종료시간", "EndDate");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");
            grid.Create_GvColumn(gv, "공정검사여부", "ChkProc");
            //grid.Create_GvColumn(gv, "검사표준서", "ChkQC", chk_Use);

            // 포맷 설졍
            grid.SetCol_Type(gv.Columns["ResultDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["StartDate"], clsGrid.TypeList.TDateTime);
            grid.SetCol_Type(gv.Columns["EndDate"], clsGrid.TypeList.TDateTime);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TInt);

            gv.Columns["WorkDate"].AppearanceCell.ForeColor = System.Drawing.Color.Red;
            gv.Columns["WorkDate"].AppearanceCell.Options.HighPriority = true;
            gv.Columns["ChkProc"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //visible
            gv.Columns["SubSeq"].Visible = false;
            gv.Columns["OrderSeq"].Visible = false;
            //gv.Columns["ItemCode"].Visible = false;
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["ChkProc"].Visible = false;

            grid.SetCol_Summary(gv, "GoodQty");
            grid.SetCol_Summary(gv, "NgQty");

            gv.BestFitColumns();
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
                GvPM_OrderResult.ExportToXls(sfd.FileName);

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
            PM_OrderResult result = new PM_OrderResult();

            result.StartDt = DtResult.smtStartDateTimeValue.ConvertDate_To_Char10();
            result.EndDt = DtResult.smtEndDateTimeValue.ConvertDate_To_Char10();
            result.ItemCode = Lup_Material.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Material.smtLookUpValue;
            result.ProcessCode = Lup_Process.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;

            DataTable dt = result.Select_PM_OrderResultView();

            GcPM_OrderResult.DataSource = dt;
            GvPM_OrderResult.BestFitColumns();
        }
        #endregion 조회
	}
}
