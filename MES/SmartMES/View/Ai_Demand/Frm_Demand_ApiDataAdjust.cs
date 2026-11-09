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
    public partial class Frm_Demand_ApiDataAdjust : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        bool isUpdating = false;

        public Frm_Demand_ApiDataAdjust()
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
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lupType = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("D11020", false).ToTable(), "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lupUser = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("사용자", "", ""), "사원ID", "사원명");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemName");
            grid.Create_GvColumn(gv, "예측기간", "RegDate");
            grid.Create_GvColumn(gv, "예상수요량", "Qty");
            grid.Create_GvColumn(gv, "보정값", "AdjustedQty");
            grid.Create_GvColumn(gv, "보정사유", "Reason");
            grid.Create_GvColumn(gv, "보정자", "AdjustedId", lupUser);
            grid.Create_GvColumn(gv, "보정날짜", "AdjustedDate");

            grid.SetCol_Type(gv.Columns["AdjustedDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["AdjustedQty"], clsGrid.TypeList.TInt);

            grid.Column_AllowEdit(gv.Columns["AdjustedQty"]);
            grid.Column_AllowEdit(gv.Columns["Reason"]);
            grid.Column_AllowEdit(gv.Columns["AdjustedId"]);
            grid.Column_AllowEdit(gv.Columns["AdjustedDate"]);

            gv.CellValueChanged += Gv_CellValueChanged;
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (isUpdating) return;

            GridView gv = sender as GridView;
            DataRow dr =gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            string fieldName = e.Column.FieldName;
            isUpdating = true;

            if (fieldName == "AdjustedQty" || fieldName == "Reason")
            {
                dr["AdjustedId"] = WrGlobal.LoginID;
                dr["AdjustedDate"] = DateTime.Today;
            }
            isUpdating = false;
            gv.BestFitColumns();
        }

        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            baseSMT.smtSearchButton Btn = (baseSMT.smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case nameof(btnSearch):
                    DisplayData();
                    break;

                case nameof(btnSave):
                    Save();
                    break;

                case nameof(btnDelete):
                    Delete();
                    break;

                case nameof(btnClose):
                    this.Close();
                    break;
            }
        }

        #region Event
        private void Save()
        {
            gvResult.CloseEditor();
            gvResult.PostEditor();

            DataTable dt = gcResult.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row in dt.Rows)
            {
                if (row.RowState != DataRowState.Modified) continue;

                DM_Result dr = new DM_Result();
                dr.Seq = row["Seq"].ConvertInt_Null_To_Zero();
                if (row["AdjustedQty"] == null || row["AdjustedQty"] == DBNull.Value)
                {
                    dr.AdjustedQty = null;
                }
                else
                {
                    dr.AdjustedQty = row["AdjustedQty"].ConvertDecimal_Null_To_Zero();
                }
                dr.Reason = row["Reason"].ConvertString_Null_To_Empty();
                dr.AdjustedId = row["AdjustedId"].ConvertString_Null_To_Empty();
                if (row["AdjustedDate"] == null || row["AdjustedDate"] == DBNull.Value)
                {
                    dr.AdjustedDate = null;
                }
                else
                {
                    dr.AdjustedDate = row["AdjustedDate"].ConvertDate_To_Char10();
                }

                if (!dr.Update_Adjust())
                {
                    MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                    return;
                }
            }

            MessageBox.Show(cls_MessageBox.Save_Comp, cls_MessageBox.SaveCaption);
            DisplayData();
        }

        private void Delete()
        {
            DataRow dr = gvResult.GetFocusedDataRow();
            if (dr == null) return;

            isUpdating = true;
            dr["AdjustedQty"] = DBNull.Value;
            dr["Reason"] = null;
            dr["AdjustedId"] = null;
            dr["AdjustedDate"] = DBNull.Value;
            isUpdating = false;
        }

        #endregion Event

        #region 조회
        private void DisplayData()
        {
            string searchDate = dtSearch.smtStartDateTimeValue.ToString("yyyy-MM");
            string itemName = txtItemName.smtTextBoxValue;

            DM_Result dr = new DM_Result();
            gcResult.DataSource = dr.Select_AdjustedList(searchDate, itemName);
            gvResult.BestFitColumns();
        }
        #endregion 조회
    }
}