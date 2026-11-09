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
using DevExpress.XtraGrid.Columns;
using SmartMES.Models.DM;
using System.Collections.Generic;

namespace SmartMES.View.Ai_Demand
{
    public partial class Frm_Demand_ApiAlertConfig : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        bool isUpdating = false;

        public Frm_Demand_ApiAlertConfig()
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
            lupAlertType.smtLookUpDataSource = code.GetCategory("D11030", true).ToTable();
            lupAlertType.smtLookUpDataDisplayMember = "분류명";
            lupAlertType.smtLookUpDataValueMember = "분류코드";
            lupAlertType.smtLookUpValue = "ALL";

            lupUseYn.smtLookUpDataSource = code.GetCategory("G05002", true).ToTable();
            lupUseYn.smtLookUpDataDisplayMember = "분류명";
            lupUseYn.smtLookUpDataValueMember = "분류코드";
            lupUseYn.smtLookUpValue = "ALL";
        }

        private void SetGridView()
		{
            SetGv_Alert(gvAlert);
		}

        private void SetGv_Alert(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lupAlert = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("D11030", false).ToTable(), "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lupBase = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("D11040", false).ToTable(), "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lupYn = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("G05002", false).ToTable(), "분류코드", "분류명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "알림명", "AlertName");
            grid.Create_GvColumn(gv, "알림구분", "AlertType", lupAlert);
            grid.Create_GvColumn(gv, "비교기준", "CompareBase", lupBase);
            grid.Create_GvColumn(gv, "임계치(%)", "Rate");
            grid.Create_GvColumn(gv, "사용여부", "UseYn", lupYn);

            grid.SetCol_Type(gv.Columns["Rate"], clsGrid.TypeList.TFloat);

            foreach (GridColumn gc in gv.Columns)
            {
                grid.Column_AllowEdit(gc);
            }

            grid.Column_AllowEdit(gv.Columns["Status"], false);

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
            string fieldName = e.Column.FieldName;

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

                case nameof(btnAdd):
                    Set_RowState("Add");
                    break;

                case nameof(btnDelete):
                    Set_RowState("Del");
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
        private void Set_RowState(string key)
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

            grid.Set_Row(gcAlert, dict, key);
        }

        private void Save()
        {
            gvAlert.CloseEditor();
            if (!IsValidate()) return;

            DataTable dt = gcAlert.DataSource as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();
                if (status == "") continue;

                DM_Config dc = new DM_Config();
                dc.AlertCode = dr["AlertCode"].ConvertString_Null_To_Empty();
                dc.AlertName = dr["AlertName"].ConvertString_Null_To_Empty();
                dc.AlertType = dr["AlertType"].ConvertString_Null_To_Empty();
                dc.CompareBase = dr["CompareBase"].ConvertString_Null_To_Empty();
                dc.Rate = dr["Rate"].ConvertDecimal_Null_To_Zero();
                dc.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    if (!dc.Insert_Config())
                    {
                        MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                        return;
                    }
                }

                else if (status == "수정")
                {
                    if (!dc.Update_Config())
                    {
                        MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                        return;
                    }
                }

                else if (status == "삭제")
                {
                    if (!dc.Delete_Config())
                    {
                        MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                        return;
                    }
                }
            }

            MessageBox.Show(cls_MessageBox.Save_Comp, cls_MessageBox.SaveCaption);
            DisplayData();
        }

        private bool IsValidate()
        {
            DataTable dt = gcAlert.DataSource as DataTable;
            
            if (dt.Select("ISNULL(AlertName, '') = ''").Length > 0)
            {
                MessageBox.Show("알림명은 필수 항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(AlertType, '') = ''").Length > 0)
            {
                MessageBox.Show("알림구분은 필수 항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(CompareBase, '') = ''").Length > 0)
            {
                MessageBox.Show("비교기준은 필수 항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(Rate, 0) = 0").Length > 0)
            {
                MessageBox.Show("임계치는 0보다 크거나 작아야합니다.");
                return false;
            }

            return true;
        }
        #endregion Event

        #region 조회
        private void DisplayData()
        {
            string alertType = lupAlertType.smtLookUpValue == "ALL" ? "" : lupAlertType.smtLookUpValue;
            string useYn = lupUseYn.smtLookUpValue == "ALL" ? "" : lupUseYn.smtLookUpValue;

            DM_Config dc = new DM_Config();
            gcAlert.DataSource = dc.Select_Config(alertType, useYn);
            gvAlert.BestFitColumns();
        }
        #endregion 조회
    }
}