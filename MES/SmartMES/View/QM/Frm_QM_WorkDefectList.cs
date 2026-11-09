using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.PM;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_WorkDefectList : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        Database db = new Database();
        string query = "";
        string customer = "";

        public Frm_QM_WorkDefectList()
        {
            InitializeComponent();
        }

        private void Frm_QM_WorkDefectList_Load(object sender, EventArgs e)
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            barEdit.smtStartDateTimeValue = firstDayOfMonth;
            barEdit.smtEndDateTimeValue = lastDayOfMonth;

            ssCustomer.smtLookUpDataSource = code.smtLookUp("거래처", "매출", "전체");
            ssCustomer.smtLookUpEditValueChanged += SsCustomer_smtLookUpEditValueChanged;
            LupItemType.smtLookUpDataSource = code.smtLookUp("품목", "", "전체");
            LupItemType.smtLookUpDataValueMember = "품번코드";
            LupItemType.smtLookUpDataDisplayMember = "품명";
            LupItemType.smtLookUpSelectdIndex = 0;

            Set_Grid();
            Search();
        }

        private void SsCustomer_smtLookUpEditValueChanged(object sender, EventArgs e)
        {
            //Update 거래처코드
            this.customer = ssCustomer.smtLookUpValue?.ConvertString_Null_To_Empty() ?? "ALL";

            //Update LupItemType
            LupItemType.smtLookUpDataSource = code.smtLookUp("품목", this.customer, "전체", "");

        }

        #region 디자인

        private void Set_Grid()
        {
            GridView gv = gvSales;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            RepositoryItemSearchLookUpEdit lupUnitCode = rpo.Create_RepositoryItemSearchLookUp(code.GetUnitCode(), "코드", "코드명");

            grid.Create_GvColumn(gv, "실적번호", "OrderNo");
            grid.Create_GvColumn(gv, "실적순번", "OrderSeq");
            grid.Create_GvColumn(gv, "거래처", "CustName");
            grid.Create_GvColumn(gv, "품번", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "실적일자", "WorkDate");
            grid.Create_GvColumn(gv, "불량유형", "FaultCodeName");
            grid.Create_GvColumn(gv, "작업수량", "WorkQty");
            grid.Create_GvColumn(gv, "불량수량", "BadQty");

            gv.Columns["WorkQty"].Visible = false;
        }
        #endregion

        #region 조회
        private void Search()
        {
            string custCode = ssCustomer.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : ssCustomer.smtLookUpValue.ConvertString_Null_To_Empty();
            string ItemCode = LupItemType.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItemType.smtLookUpValue.ConvertString_Null_To_Empty();
            string startDate = barEdit.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = barEdit.smtEndDateTimeValue.ConvertDate_To_Char8();

            PM_OrderResult pm = new PM_OrderResult();
            pm.ItemCode = ItemCode;
            DataTable dt = pm.Select_PM_OrderResult_ByQM_WorkDefect(startDate, endDate, custCode);
            gcSales.DataSource = dt;
            gvSales.BestFitColumns();
        }

        #endregion 조회 END


        #region 버튼
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvSales.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }//end fnction
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 버튼 END

    }
}