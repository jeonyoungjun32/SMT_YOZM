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
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Columns;

namespace SmartMES.View.Ai_Demand
{
    public partial class Frm_Demand_Condition : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        bool isUpdating = false;

        public Frm_Demand_Condition()
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
            lupImpactType.smtLookUpDataSource = code.GetCategory("D11050", true).ToTable();
            lupImpactType.smtLookUpDataDisplayMember = "분류명";
            lupImpactType.smtLookUpDataValueMember = "분류코드";
            lupImpactType.smtLookUpValue = "ALL";

            lupUseYn.smtLookUpDataSource = code.GetCategory("G05002", true).ToTable();
            lupUseYn.smtLookUpDataDisplayMember = "분류명";
            lupUseYn.smtLookUpDataValueMember = "분류코드";
            lupUseYn.smtLookUpValue = "ALL";
        }

        private void SetGridView()
		{
            SetGv_Impact(gvImpact);
		}

        private void SetGv_Impact(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            DM_Code dc = new DM_Code();
            RepositoryItemSearchLookUpEdit lupImpactType = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("D11050", false).ToTable(), "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lupUseYn = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("G05002", false).ToTable(), "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lupItem = rpo.Create_RepositoryItemSearchLookUp(dc.Select_Item_Lup(true, true), "품목코드", "품목명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "조건구분", "ImpactType", lupImpactType);
            grid.Create_GvColumn(gv, "조건명", "ImpactName");
            grid.Create_GvColumn(gv, "적용품목", "ItemCode", lupItem);
            grid.Create_GvColumn(gv, "영향증감값(%)", "Rate");
            grid.Create_GvColumn(gv, "사용여부", "UseYn", lupUseYn);

            grid.SetCol_Type(gv.Columns["Rate"], clsGrid.TypeList.TFloat);

            foreach (GridColumn gc in gv.Columns)
            {
                grid.Column_AllowEdit(gc);
            }

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CellValueChanged += Gv_CellValueChanged;
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (isUpdating) return;

            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            isUpdating = true;
            grid.Change_Status(gv, e.RowHandle);

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
                //조회
                case nameof(btnSearch):
                    DisplayData();
                    break;

                //엑셀다운
                case nameof(btnAdd):
                    Row_State("Add");
                    break;

                case nameof(btnDelete):
                    Row_State("Del");
                    break;

                case nameof(btnSave):
                    Save();
                    break;
                case nameof(btnClose):
                    this.Close();
                    break;
            }
        }

        #region Event
        private void Row_State(string key)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (key == "Add")
            {
                dict.Add("Status", "신규");
                dict.Add("UseYn", "Y");
            }

            else if (key == "Del")
            {
                dict.Add("Status", "삭제");
            }

            grid.Set_Row(gcImpact, dict, key);
        }

        private void Save()
        {
            gvImpact.CloseEditor();
            gvImpact.PostEditor();

            if (!IsValid()) return;

            DataTable dt = gcImpact.DataSource as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();
                if (status == "") continue;

                DM_Impact di = new DM_Impact();
                di.ImpactCode = dr["ImpactCode"].ConvertString_Null_To_Empty();
                di.ImpactType = dr["ImpactType"].ConvertString_Null_To_Empty();
                di.ImpactName = dr["ImpactName"].ConvertString_Null_To_Empty();
                di.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
                di.Rate = dr["Rate"].ConvertDecimal_Null_To_Zero();
                di.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    if (!di.Insert_Impact())
                    {
                        MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                        return;
                    }
                }

                else if (status == "수정")
                {
                    if (!di.Update_Impact())
                    {
                        MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                        return;
                    }
                }

                else if (status == "삭제")
                {
                    if (!di.Delete_Impact())
                    {
                        MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                        return;
                    }
                }
            }

            MessageBox.Show(cls_MessageBox.Save_Comp, cls_MessageBox.SaveCaption);
            DisplayData();
        }

        private bool IsValid()
        {
            DataTable dt = gcImpact.DataSource as DataTable;
            if (dt.Select("ISNULL(ImpactType, '') = ''").Length > 0)
            {
                MessageBox.Show("조건구분은 필수항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(ImpactName, '') = ''").Length > 0)
            {
                MessageBox.Show("조건명은 필수항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(ItemCode, '') = ''").Length > 0)
            {
                MessageBox.Show("적용품목명은 필수항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(Rate, 0) = 0").Length > 0)
            {
                MessageBox.Show("영향증감값은 0보다 크거나 작아야합니다.");
                return false;
            }

            return true;
        }

        #endregion Event

        #region 조회
        private void DisplayData()
        {
            string impactType = lupImpactType.smtLookUpValue == "ALL" ? "" : lupImpactType.smtLookUpValue;
            string useYn = lupUseYn.smtLookUpValue == "ALL" ? "" : lupUseYn.smtLookUpValue;

            DM_Impact di = new DM_Impact();
            gcImpact.DataSource = di.Select_Impact(impactType, useYn);
            gvImpact.BestFitColumns();
        }
        #endregion 조회
    }
}