using DevExpress.XtraTreeList;
using SmartCommon.Common;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using SmartDesign.Model;
using DevExpress.XtraTreeList.Columns;

namespace SmartDesign.View.Codes
{
	public partial class frmCodeProject : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        CodeProject codeProject = new CodeProject();
        clsGrid grid = new clsGrid();

        string query;
        string corp = "1000";

        public frmCodeProject()
        {
            InitializeComponent();
        }

        private void frmMaterialItem_Load(object sender, EventArgs e)
        {
            DisplayCategory();

            query = $"Select ItemCode 코드, Title 내용 From CmCateItem where CorpId = '{WrGlobal.CorpID}' and Class = 'A0PJ20'";
            lookupWork.DataSource = db.GetDataView("외주/내작", query);
            lookupWork.ValueMember = "코드";
            lookupWork.DisplayMember = "내용";

            query = $"Select ItemCode 코드, Title 내용 From CmCateItem where CorpId = '{WrGlobal.CorpID}' and Class = 'A0PJ30'";
            lookupDay.DataSource = db.GetDataView("외주/내작", query);
            lookupDay.ValueMember = "코드";
            lookupDay.DisplayMember = "내용";

            query = $"Select ItemCode 코드, Title 내용 From CmCateItem where CorpId = '{WrGlobal.CorpID}' and Class = 'A0PJ40'";
            lookupGroup.DataSource = db.GetDataView("외주/내작", query);
            lookupGroup.ValueMember = "코드";
            lookupGroup.DisplayMember = "내용";

            query = $"Select ItemCode 코드, Title 내용 From CmCateItem where CorpId = '{WrGlobal.CorpID}' and Class = 'A0PJ60'";
            lookupLink.DataSource = db.GetDataView("외주/내작", query);
            lookupLink.ValueMember = "코드";
            lookupLink.DisplayMember = "내용";

            

            query = "Select MaterialCode 품번, MaterialName 품명 From CmMaterial where DisplayYn = 'V'";
            //searchMaterial.DataSource = db.GetDataView("결과", query);
            //searchMaterial.DisplayMember = "품명";
            //searchMaterial.ValueMember = "품번";


            //if (WrGlobal.AuthorList.Exists(s => s.MenuID == this.Tag.ToString() && s.AuthorCode == "02"))
            //{
            //    cmdSave.Visible = true;
            //    cmdDelete.Visible = true;
            //}

            cmdSave.Visible = true;
            cmdDelete.Visible = true;

            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //treelist 편집 허용 불가
            foreach (TreeListColumn col in treeListCategoryGroup.Columns)
            {
                col.OptionsColumn.AllowEdit = false;
            }

            //
            grid.SetCol_Summary(gvProject, "WorkDays");

            // 트리조회
            //DisplayCategory();
            //DisplayProject();
        }

        private void treeListCategoryGroup_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Selected)
                {
                    if (e.Node.HasChildren)
                    {
                        e.NodeImageIndex = 3;
                    }
                    else
                    {
                        e.NodeImageIndex = 4;
                    }
                }
                else
                {
                    if (e.Node.HasChildren)
                    {
                        e.NodeImageIndex = 1;
                    }
                    else
                    {
                        e.NodeImageIndex = 0;
                    }
                }

            }
        }

        private void cmdDisplay_ElementClick(object sender, EventArgs e)
        {
            // 트리조회
            DisplayCategory();
        }

        private void DisplayCategory()
        {
            query = $"Select CLASS, CLASSNAME 분류명, HIGHCLASS From CLASS Where  class like 'MD%' and CorpId = '{WrGlobal.CorpID}'";
            DataView dvCategory = db.GetDataView("항목", query);
            treeListCategoryGroup.DataSource = dvCategory;
            treeListCategoryGroup.ExpandAll();

            searchCopy.Properties.DataSource = dvCategory;
            searchCopy.Properties.DisplayMember = "분류명";
            searchCopy.Properties.ValueMember = "CLASS";

            //this.codeProjectMaterialTableAdapter.Fill(this.codeDataSet.CodeProjectMaterial);
        }

        private void treeListCategoryGroup_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            string CLASS = treeListCategoryGroup.GetFocusedRowCellValue("CLASS").ToString() ?? "";
            gvProject.FocusInvalidRow();
            gcProject.DataSource = codeProject.SelectCodeProjectList(CLASS);
            //this.codeProjectTableAdapter.FillByKey(this.codeDataSet.CodeProject, corp, CLASS);
            //gcProject.DataSource = null;
            //gcProjectMaterial.DataSource = null;
        }

        private void gvProject_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string CLASS = gvProject.GetFocusedRowCellValue("CLASS")?.ToString() ?? "";
            string PjID = gvProject.GetFocusedRowCellValue("PjID")?.ToString() ?? "";

            this.codeProjectMaterialTableAdapter.FillByID(this.codeDataSet.CodeProjectMaterial, corp, CLASS, PjID);
        }

        private void cmdAdd_ElementClick(object sender, EventArgs e)
        {
            string CLASS = treeListCategoryGroup.GetFocusedRowCellValue("CLASS").ToString() ?? "";
            DataTable dt = (DataTable)gcProject.DataSource;
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow["CorpId"] = WrGlobal.CorpID;
                newRow["CLASS"] = CLASS;
                newRow["PjID"] = "";
                newRow["Title"] = "";
                newRow["Type"] = "";
                newRow["DateType"] = "순차";
                newRow["WorkDays"] = DBNull.Value;
                newRow["GroupYn"] = "";
                newRow["Remark"] = "";
                dt.Rows.Add(newRow);
            }
        }

        private void cmdSave_ElementClick(object sender, EventArgs e)
        {
            try
            {
                gvProject.PostEditor();
                gvProject.UpdateCurrentRow();

                if (MessageBox.Show("자료를 저장 하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
                {
                    DataTable listDt = (DataTable)gcProject.DataSource;
                    if(listDt != null)
                    {
                        string CLASS = treeListCategoryGroup.GetFocusedRowCellValue("CLASS").ToString() ?? "";
                        string pjID = gvProject.GetFocusedRowCellValue("PjID")?.ToString() ?? "";
                        string groupYn = gvProject.GetFocusedRowCellValue("GroupYn")?.ToString() ?? "";


                        if (string.IsNullOrEmpty(pjID))
                        {
                            MessageBox.Show("프로젝트 ID는 필수 입력 항목입니다.", "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (string.IsNullOrEmpty(groupYn))
                        {
                            MessageBox.Show("그룹유무는 필수 입력 항목입니다.", "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }


                        foreach (DataRow row in listDt.Rows)
                        {
                            if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                            {
                                codeProject.CorpId = corp;
                                codeProject.CLASS = CLASS;
                                codeProject.PjID = row["PjID"]?.ToString() ?? "";
                                codeProject.Title = row["Title"]?.ToString() ?? "";
                                codeProject.Type = row["Type"]?.ToString() ?? "";
                                codeProject.DateType = row["DateType"]?.ToString() ?? "";
                                codeProject.WorkDays = row["WorkDays"]?.ToString() ?? "";
                                codeProject.GroupYn = row["GroupYn"]?.ToString() ?? "";
                                codeProject.Remark = row["Remark"]?.ToString() ?? "";

                                if (!codeProject.InsUpd_CodeProjectList())
                                {
                                    MessageBox.Show("디테일 저장 중 오류가 발생했습니다.", "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }

                    }

                    MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayProject();
                }
            }
            catch (Exception ex)
            {
                StackTrace stackTrace = new StackTrace();
                MessageBox.Show(ex.ToString() + ex.HelpLink,
                                String.Format("{0}/{1} 에러", stackTrace.GetFrame(1), stackTrace.GetFrame(1).GetMethod().Name),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void cmdDelete_ElementClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 자료를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if(gvProject.IsFocusedView)
                    {
                        int rowHandle = gvProject.FocusedRowHandle;
                        if (rowHandle < 0)
                        {
                            MessageBox.Show("선택된 행이 없습니다.");
                            return;
                        }

                        string CLASS = treeListCategoryGroup.GetFocusedRowCellValue("CLASS").ToString() ?? "";
                        string PjID = gvProject.GetRowCellValue(rowHandle, "PjID")?.ToString() ?? "";

                        if (!string.IsNullOrEmpty(PjID))
                        {
                            codeProject.CorpId = corp;
                            codeProject.CLASS = CLASS;
                            codeProject.PjID = PjID;
                            codeProject.Del_CodeProjectList();
                            DisplayProject();
                        }
                    }
                    MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cmdClose_ElementClick(object sender, EventArgs e)
        {
            this.Close();
        }
    
        private void cmdCopy_Click(object sender, EventArgs e)
        {
            string sourceClass = treeListCategoryGroup.GetFocusedRowCellValue("CLASS").ToString();

            query = $"exec Project_CopyProjectCode '{WrGlobal.CorpID}','{sourceClass}','{searchCopy.EditValue}'";
            db.ExecuteQuery(query);

            MessageBox.Show("자료를 저장 하시겠습니까?", "저장");
        }

        private void DisplayProject()
        {
            string CLASS = treeListCategoryGroup.GetFocusedRowCellValue("CLASS").ToString() ?? "";
            if (!string.IsNullOrEmpty(CLASS))
            {
                gcProject.DataSource = codeProject.SelectCodeProjectList(CLASS);
            }
        }

        //private void layoutControlGroupMaterial_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        //{
        //    //250403 폼 추가 전 주석처리
        //
        //    /*    frmLookupProduct form = new frmLookupProduct();
        //        form.onSetData += new frmLookupProduct.SetData(AddMaterial);
        //        form.ShowDialog();*/
        //}
        //
        //private void AddMaterial(DataRowView row)
        //{
        //
        //    codeProjectMaterialBindingSource.AddNew();
        //    DataRowView drv = (DataRowView)codeProjectMaterialBindingSource.Current;
        //    drv["CorpId"] = gvProject.GetRowCellValue(gvProject.FocusedRowHandle, "CorpId").ToString();
        //    drv["CLASS"] = gvProject.GetRowCellValue(gvProject.FocusedRowHandle, "CLASS").ToString();
        //    drv["PjID"] = gvProject.GetRowCellValue(gvProject.FocusedRowHandle, "PjID").ToString();
        //    drv["MaterialCode"] = row["MaterialCode"];
        //    drv["MaterialName"] = row["MaterialName"];
        //    drv["Size"] = row["Size"];
        //    //drv["Unit"] = "";
        //    //drv["Qty"] = "";
        //    drv["UnitPrice"] = row["SalesPrice"];
        //    codeProjectMaterialBindingSource.EndEdit();
        //}

    }
}