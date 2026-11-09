using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartMES.View.MM
{
	public partial class Frm_MM_MaterialOutView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsPurchase purchase = new clsPurchase();
        clsGrid grid = new clsGrid();

        List<string> chkList = new List<string>();

        private string query = "";
        Database db = new Database();

        string ItemType = "30,40,50,60";

        public Frm_MM_MaterialOutView()
        {
            InitializeComponent();
        }

        private void frmMaterialOutView_Load(object sender, EventArgs e)
        {
            // 디자인
            Set_DtEdit();
            Set_Lup();
            Set_ChkEvent();
            Set_GvMaterialOut(GvMaterialOut);

            // 셀 수정 가능 여부
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in GvMaterialOut.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            // 조회
            DisplayDetail();
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);
        }

        #region 디자인
        private void Set_DtEdit()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;
        }

        private void Set_Lup()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            Lup_Material.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");

            Lup_Material.smtLookUpDataDisplayMember = "품명";
            Lup_Material.smtLookUpDataValueMember = "품목코드";
            Lup_Material.smtLookUpValue = "ALL";
        }

        private void Set_GvMaterialOut(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            RepositoryItemSearchLookUpEdit lupUnitCode = rpo.Create_RepositoryItemSearchLookUp(code.GetUnitCode(), "코드", "코드명");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lupVat = rpo.Create_RepositoryItemSearchLookUp(code.GetClassList("C010"), "분류코드", "분류명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "순번", "SalesSeq");
            grid.Create_GvColumn(gv, "출고일자", "OutDate");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "출고량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "합계", "TotalPrice");
            grid.Create_GvColumn(gv, "부가세유형", "VatType", lupVat);
            grid.Create_GvColumn(gv, "부가세율(%)", "VatRate");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "출고년월", "OutMonth");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //visible
            gv.Columns["SalesSeq"].Visible = false;

            //포맷
            grid.SetCol_Type(gv.Columns["OutDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TInt);

            //SUMMARY
            grid.SetCol_Summary(gv, "Qty", DevExpress.Data.SummaryItemType.Sum, "{0:n0}");
            grid.SetCol_Summary(gv, "Price", DevExpress.Data.SummaryItemType.Sum, "{0:n0}");
            grid.SetCol_Summary(gv, "VatPrice", DevExpress.Data.SummaryItemType.Sum, "{0:n0}");
            grid.SetCol_Summary(gv, "TotalPrice", DevExpress.Data.SummaryItemType.Sum, "{0:n0}");
        }

        private void Set_ChkEvent()
        {
            ChkDay.CheckedChanged += ChkTest_CheckedChanged;
            ChkMonth.CheckedChanged += ChkTest_CheckedChanged;
        }
        #endregion

        #region 이벤트

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayDetail();
        }

        private void ChkTest_CheckedChanged(object sender, EventArgs e)
        {
            GridView gv = GvMaterialOut;
            CheckEdit chk = sender as CheckEdit;
            string name = chk.Tag.ConvertString_Null_To_Empty();

            if (chk == ChkDay && chk.Checked)
            {
                ChkMonth.Checked = false;
            }
            else if (chk == ChkMonth && chk.Checked)
            {
                ChkDay.Checked = false;
            }

            if (chk.Checked)
                chkList.Add(name);
            else
                chkList.Remove(name);

            gv.ClearGrouping();

            gv.Columns["ItemCode"].Group();

            if (chkList.Contains("Day"))
            {
                gv.Columns["OutDate"].Group(); // 일자별 그룹
            }
            else if (chkList.Contains("Month"))
            {
                gv.Columns["OutMonth"].Group(); // 월별 그룹
            }

            gv.ExpandAllGroups();
            gv.BestFitColumns();
        }
        #endregion

        #region 조회

        private void DisplayDetail()
        {
            BM_SalesD SD = new BM_SalesD();
            SD.stDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            SD.endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            SD.ItemCode = Lup_Material.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Material.smtLookUpValue;

            DataTable dt = SD.Select_MaterialOutView();

            GcMaterialOut.DataSource = dt;
            GvMaterialOut.BestFitColumns();
        }
        #endregion

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}