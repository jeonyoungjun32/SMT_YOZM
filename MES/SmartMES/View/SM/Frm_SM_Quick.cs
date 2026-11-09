using DevExpress.XtraTreeList.Nodes;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.SM;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_Quick : DevExpress.XtraEditors.XtraForm
    {
        clsSystem sys = new clsSystem();
        SM_Quick _Quick = new SM_Quick();

        public Frm_SM_Quick()
        {
            InitializeComponent();
        }

        private void Frm_SM_Quick_Load(object sender, EventArgs e)
        {
            DisplayMenu();
        }

        private void DisplayMenu()
        {
            MenuTreeList.DataSource = sys.GetEmployeeQuickMenuList("ALL");
            MenuTreeList.ExpandAll();
        }

        private void MenuTreeList_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
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

        private void btn_Search_Click(object sender, EventArgs e)
        {
            DisplayMenu();
        }

        private void MenuTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            MenuTreeList.OptionsBehavior.Editable = false;

            if (!e.Node.HasChildren) {
                MenuTreeList.OptionsBehavior.Editable = true;
            }
        }

        private void MenuTreeList_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            if (e.Column.FieldName == "QuickYn")
            {
                if (e.Node.HasChildren)
                {
                    e.RepositoryItem = emptyEditor;
                }
                else
                {
                    e.RepositoryItem = quickCheckEdit;
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //기존 항목삭제
                _Quick.CorpId = WrGlobal.CorpID;
                _Quick.EmployeeId = WrGlobal.LoginID;
                _Quick.Del_SM_Quick_EmployeeId();

                List<TreeListNode> Listnodes = MenuTreeList.GetNodeList();

                foreach (TreeListNode node in Listnodes)
                {
                    if (MenuTreeList.GetRowCellValue(node, "GroupYn").ToString() == "Y") continue;
                    if (MenuTreeList.GetRowCellValue(node, "QuickYn").ToString() == "N") continue;

                    if (MenuTreeList.GetRowCellValue(node, "SortNum").ToString() == "")
                    {
                        MenuTreeList.SetRowCellValue(node, "SortNum", 99999);
                    }

                    _Quick.CorpId = WrGlobal.CorpID;
                    _Quick.EmployeeId = WrGlobal.LoginID;
                    _Quick.Id = MenuTreeList.GetRowCellValue(node, "Id").ConvertString_Null_To_Empty();
                    _Quick.Title_ko = MenuTreeList.GetRowCellValue(node, "NewTitle_ko").ConvertString_Null_To_Empty();
                    _Quick.SortNum = MenuTreeList.GetRowCellValue(node, "SortNum").ConvertInt_Null_To_Zero();
                    _Quick.Ins_SM_Quick();
                }

                DisplayMenu();

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //새로고침
                MethodInfo quickMenuRefreshCall = this.MdiParent.GetType().GetMethod("SetQuickMenuRefresh", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                quickMenuRefreshCall.Invoke(this.MdiParent, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}