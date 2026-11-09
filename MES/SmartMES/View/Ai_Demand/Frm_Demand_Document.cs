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
using DevExpress.XtraGrid.Columns;
using System.Collections.Generic;

namespace SmartMES.View.Ai_Demand
{
    public partial class Frm_Demand_Document : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        bool isUpdating = false;


        public Frm_Demand_Document()
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
            lupUseYn.smtLookUpDataSource = code.GetCategory("G05002", true).ToTable();
            lupUseYn.smtLookUpDataDisplayMember = "분류명";
            lupUseYn.smtLookUpDataValueMember = "분류코드";
            lupUseYn.smtLookUpValue = "ALL";
        }

        private void SetGridView()
		{
            SetGvDocument(gvDocument);
		}

        private void SetGvDocument(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lupItem = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", new string[] { "", "20,30" }, ""), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lupCycle = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("D11010", false).ToTable(), "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lupType = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("D11020", false).ToTable(), "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lupYn = rpo.Create_RepositoryItemSearchLookUp(code.GetCategory("G05002", false).ToTable(), "분류코드", "분류명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemName", lupItem);
            grid.Create_GvColumn(gv, "예측주기", "ForecastCycle", lupCycle);
            grid.Create_GvColumn(gv, "기준기간(개월)", "BaseMonth");
            grid.Create_GvColumn(gv, "기준실적", "BaseType", lupType);
            grid.Create_GvColumn(gv, "사용여부", "UseYn", lupYn);
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.SetCol_Type(gv.Columns["BaseMonth"], clsGrid.TypeList.TInt);

            foreach (GridColumn gc in gv.Columns)
            {
                grid.Column_AllowEdit(gc);
            }
            grid.Column_AllowEdit(gv.Columns["Status"], false);
            grid.Column_AllowEdit(gv.Columns["ItemCode"], false);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CellValueChanged += Gv_CellValueChanged;
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (isUpdating) return;

            GridView gv = sender as GridView;
            if (gv == null) return;

            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            isUpdating = true;

            string fieldName = e.Column.FieldName;

            grid.Change_Status(gv, e.RowHandle);

            if (fieldName == "ItemName")
            {
                dr["ItemCode"] = dr["ItemName"];
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
                dict["Status"] = "신규";
                dict["UseYn"] = "Y";
            }

            else if (key == "Del")
            {
                dict["Status"] = "삭제";
            }

            grid.Set_Row(gcDocument, dict, key);
        }

        private void Save()
        {
            gvDocument.CloseEditor();
            gvDocument.PostEditor();

            if (!IsValid()) return;

            DM_Code dc = new DM_Code();
            DataTable dt = gcDocument.DataSource as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                dc.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
                dc.ForecastCycle = dr["ForecastCycle"].ConvertString_Null_To_Empty();
                dc.BaseMonth = dr["BaseMonth"].ConvertInt_Null_To_Zero();
                dc.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();
                dc.BaseType = dr["BaseType"].ConvertString_Null_To_Empty();
                dc.Remark = dr["Remark"].ConvertString_Null_To_Empty();

                string status = dr["Status"].ConvertString_Null_To_Empty();
                if (status == "신규")
                {
                    if (!dc.Insert_Code())
                    {
                        MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                        return;
                    }
                }

                else if (status == "수정")
                {
                    if (!dc.Update_Code())
                    {
                        MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                        return;
                    }
                }

                else if (status == "삭제")
                {
                    if (!dc.Delete_Code())
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
            DataTable dt = gcDocument.DataSource as DataTable;
            
            if (dt.Select("ISNULL(ItemCode, '') = ''").Length > 0)
            {
                MessageBox.Show("품목은 필수 항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(ForecastCycle, '') = ''").Length > 0)
            {
                MessageBox.Show("예측주기는 필수 항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(BaseMonth, 0) = 0").Length > 0)
            {
                MessageBox.Show("기준기간은 필수 항목입니다.");
                return false;
            }

            if (dt.Select("ISNULL(BaseType, '') = ''").Length > 0)
            {
                MessageBox.Show("기준실적은 필수 항목입니다.");
                return false;
            }

            return true;
        }
        #endregion Event

        #region 조회
        private void DisplayData()
        {
            string itemName = txtSearchItemName.smtTextBoxValue;
            string useYn = lupUseYn.smtLookUpValue == "ALL" ? "" : lupUseYn.smtLookUpValue;

            DM_Code dc = new DM_Code();
            gcDocument.DataSource = dc.Select_Code(itemName, useYn);
            gvDocument.BestFitColumns();
        }
        #endregion 조회
    }
}