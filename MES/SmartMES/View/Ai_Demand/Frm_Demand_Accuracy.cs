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
using DevExpress.Utils;
using SmartMES.Models.DM;

namespace SmartMES.View.Ai_Demand
{
    public partial class Frm_Demand_Accuracy : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();


        public Frm_Demand_Accuracy()
        {
            InitializeComponent();
        }

        private void Frm_Demand_ApiDataAdjust_Load(object sender, EventArgs e)
        {
            dtSearchDate.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            dtSearchDate.smtEndDateTimeValue = DateTime.Now;

            SetGridView();
            DisplayData();
        }

        #region setDesign

        private void SetGridView()
		{
            SetGrid(gvAccuracy);

        }

        private void SetGrid(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemName");
            grid.Create_GvColumn(gv, "예측기간", "RegDate");
            grid.Create_GvColumn(gv, "예상수요량", "Qty");
            grid.Create_GvColumn(gv, "실적수량", "BaseQty");
            grid.Create_GvColumn(gv, "오차율", "MapeRate");
            grid.Create_GvColumn(gv, "평가", "EvalGrade");

            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["ObtainQty"], clsGrid.TypeList.TInt);

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
            }
        }

        #region Event

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Event

        #region 조회
        private void DisplayData()
        {
            string searchDate = dtSearchDate.smtStartDateTimeValue.ToString("yyyy-MM");
            string itemName = txtSearchItemName.smtTextBoxValue;

            DM_Result dr = new DM_Result();
            gcAccuracy.DataSource = dr.Select_Demand_Accuracy(searchDate, itemName);
            gvAccuracy.BestFitColumns();
        }
        #endregion 조회
    }
}