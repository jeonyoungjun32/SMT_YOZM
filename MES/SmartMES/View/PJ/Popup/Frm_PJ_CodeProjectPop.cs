using DevExpress.XtraTreeList;
using SmartCommon.Common;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.PJ.Popup
{
	public partial class Frm_PJ_CodeProjectPop : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query;
        string corp = "Z0";

        public delegate void SetData(string selClass);  //델리게이트 선언
        public event SetData onSetData;                 //계약

        public Frm_PJ_CodeProjectPop()
        {
            InitializeComponent();
        }

        private void frmCodeProjectPop_Load(object sender, EventArgs e)
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
                    }//end if
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
                    }//end if
                }//end if

            }//end if
        }

        private void cmdDisplay_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            // 트리조회
            DisplayCategory();
        }

        private void DisplayCategory()
        {
            query = string.Format("Select CLASS, CLASSNAME 분류명, HIGHCLASS From CLASS Where  class like '10%' and CorpId = '{WrGlobal.CorpID}'", WrGlobal.PublicCorpID);
            DataView dvCategory = db.GetDataView("항목", query);
            treeListCategoryGroup.DataSource = dvCategory;
            treeListCategoryGroup.ExpandAll();

            //this.codeProjectMaterialTableAdapter.Fill(this.codeDataSet.CodeProjectMaterial);
        }

        private void DisplayProject()
        {
            string CLASS = gvProject.GetFocusedRowCellValue("CLASS")?.ToString() ?? "";
            string PjID = gvProject.GetFocusedRowCellValue("PjID")?.ToString() ?? "";
        }

        private void DisplayMaterial()
        {
            string CLASS = gvProject.GetFocusedRowCellValue("CLASS")?.ToString() ?? "";
            string PjID = gvProject.GetFocusedRowCellValue("PjID")?.ToString() ?? "";
        }

        private void treeListCategoryGroup_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            string CLASS = treeListCategoryGroup.GetFocusedRowCellValue("CLASS").ToString() ?? "";
            gvProject.FocusInvalidRow();
            //gcProject.DataSource = null;
            //gcProjectMaterial.DataSource = null;
        }

        private void gvProject_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (e.FocusedRowHandle < 0) return;

            string CLASS = gvProject.GetFocusedRowCellValue("CLASS")?.ToString() ?? "";
            string PjID = gvProject.GetFocusedRowCellValue("PjID")?.ToString() ?? "";
            //gcProjectMaterial.DataSource = codeProjectMaterialBindingSource;
        }

        private void cmdClose_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            this.Close();
        }

        private void cmdSelect_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            string CLASS = treeListCategoryGroup.GetFocusedRowCellValue("CLASS").ToString() ?? "";

            if (CLASS == "")
            {
                MessageBox.Show("등록할 항목을 선택해 주세요.");
                return;
            }

            onSetData(CLASS);
            this.Close();
        }
    }
}