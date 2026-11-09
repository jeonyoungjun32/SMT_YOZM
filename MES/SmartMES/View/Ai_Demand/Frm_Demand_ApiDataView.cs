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
using System.Data;
using SmartMES.Models.ChatBot;
using SmartMES.Models.DM;

namespace SmartMES.View.Ai_Demand
{
    public partial class Frm_Demand_ApiDataView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();


        public Frm_Demand_ApiDataView()
        {
            InitializeComponent();
        }

        private void frmPM_ProductPlanView_Load(object sender, EventArgs e)
        {
            dtSearch.smtStartDateTimeValue = DateTime.Now;

            SetGridView();

            DisplayData();
        }

        #region setDesign
        private void SetGridView()
		{
            SetGv_Result(gvResult);
		}

        private void SetGv_Result(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            DM_Impact di = new DM_Impact();
            RepositoryItemSearchLookUpEdit lupImpact = rpo.Create_RepositoryItemSearchLookUp(di.Select_Impact_Lup(false), "조건코드", "조건명");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemName");
            grid.Create_GvColumn(gv, "예측기간", "RegDate");
            grid.Create_GvColumn(gv, "예상수요량", "Qty");
            grid.Create_GvColumn(gv, "권장생산량", "ProdQty");
            grid.Create_GvColumn(gv, "영향요인1", "ImpactCode1", lupImpact);
            grid.Create_GvColumn(gv, "영향요인2", "ImpactCode2", lupImpact);
            grid.Create_GvColumn(gv, "영향요인3", "ImpactCode3", lupImpact);

            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["ProdQty"], clsGrid.TypeList.TInt);
        }
        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            baseSMT.smtSearchButton Btn = (baseSMT.smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                //조회
                case nameof(btnSearch):
                    DisplayData();
                    break;

                //엑셀다운
                case nameof(btnExcelDownload):
                    cmdExcel_Click();
                    break;

                case nameof(btnClose):
                    this.Close();
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
                gvResult.ExportToXls(sfd.FileName);

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
            string searchDate = dtSearch.smtStartDateTimeValue.ToString("yyyy-MM");
            string itemName = txtItemName.smtTextBoxValue;

            DM_Result dr = new DM_Result();
            gcResult.DataSource = dr.Select_Result(searchDate, itemName);
            gvResult.BestFitColumns();
        }

        #endregion 조회
    }
}