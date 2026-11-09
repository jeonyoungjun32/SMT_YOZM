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
    public partial class Frm_Demand_DateView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();


        public Frm_Demand_DateView()
        {
            InitializeComponent();
        }

        private void frmPM_ProductPlanView_Load(object sender, EventArgs e)
        {
            dtSearch.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            dtSearch.smtEndDateTimeValue = DateTime.Now;

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
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lupType = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("D11020", false).ToTable(), "분류코드", "분류명");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemName");
            grid.Create_GvColumn(gv, "기준실적", "BaseType", lupType);
            grid.Create_GvColumn(gv, "수량", "Qty");

            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TInt);
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
                case nameof(btnClose):
                    this.Close();
                    break;
            }
        }

        #region Event

        #endregion Event

        #region 조회
        private void DisplayData()
        {
            DateTime startDate = dtSearch.smtStartDateTimeValue;
            DateTime endDate = dtSearch.smtEndDateTimeValue;

            DM_Code dc = new DM_Code();
            gcResult.DataSource = dc.Select_BaseData(startDate, endDate);
            gvResult.BestFitColumns();
        }
        #endregion 조회

    }
}