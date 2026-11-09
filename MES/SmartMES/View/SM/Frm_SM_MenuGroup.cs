using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.SM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_MenuGroup : DevExpress.XtraEditors.XtraForm
    {
        clsSystem sys = new clsSystem();
        clsCode code = new clsCode();

        SM_MenuGroup menu = new SM_MenuGroup();

        clsTree tree = new clsTree();
        clsRepository rpo = new clsRepository();


        public Frm_SM_MenuGroup()
        {
            InitializeComponent();
        }

        private void Frm_SM_MenuGroup_Load(object sender, EventArgs e)
{
            InitLup();

            SetTree(MenuTreeList);

            DisplayMenu();
        }

        private void InitLup()
		{
            DataTable dt = code.GetCM_CateItem(WrGlobal.CorpID, "A050", true);

            // 사용여부 룩업 - N으로 조회 후 저장시 오류날 수 있으므로 N 제거
            DataRow[] rows = dt.Select("코드 <> 'N'");

            DataTable filterDt = dt.Clone();
            foreach (DataRow row in rows)
            {
                filterDt.ImportRow(row);
            }
            LupUseYn.smtLookUpDataSource = filterDt;

            //LupUseYn.smtLookUpDataSource = code.GetCM_CateItem(WrGlobal.CorpID, "A050", true);
            LupUseYn.smtLookUpDataValueMember = "코드";
            LupUseYn.smtLookUpDataDisplayMember = "명칭";
            LupUseYn.smtLookUpValue = "Y";
            LupUseYn.smtLookUpEditValueChanged += LupUseYn_smtLookUpEditValueChanged;
        }

        private void SetTree(TreeList treeList)
		{
            treeList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // 드래그 모드를 설정
            treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Single;
            treeList.OptionsDragAndDrop.AcceptOuterNodes = true;

            //키 설정
            treeList.KeyFieldName = "Id";            // 고유 키 필드
            treeList.ParentFieldName = "ParentId";  // 부모 키 필드 (이 값이 자동 변경됨)

            //Repository
            RepositoryItemButtonEdit btnDelete = new RepositoryItemButtonEdit();
            btnDelete.AutoHeight = false;
            btnDelete.Buttons.Clear();

            btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            btnDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnNodeDelete_ButtonClick);

            //
            RepositoryItemRadioGroup rdo = new RepositoryItemRadioGroup();
            rdo.Items.Clear();
            rdo.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "웹"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "윈폼"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("3", "POP")});

            tree.Create_TreeColummn(treeList, "상태", "Status");
            tree.Create_TreeColummn(treeList, "순번", "RowNum");
            tree.Create_TreeColummn(treeList, "Id", "Id");
            tree.Create_TreeColummn(treeList, "상위Id", "ParentId");
            tree.Create_TreeColummn(treeList, "메뉴항목명", "Title_ko");
            tree.Create_TreeColummn(treeList, "그룹레벨", "GroupLevel");
            tree.Create_TreeColummn(treeList, "그룹여부", "GroupYn", rpo.Create_rpoChk("1", "0"));

            tree.Create_TreeColummn(treeList, "프로젝트명", "ProjectName");
            tree.Create_TreeColummn(treeList, "접근폴더", "FolderName");
            tree.Create_TreeColummn(treeList, "폼/페이지명", "FormName");
            tree.Create_TreeColummn(treeList, "웹/윈폼", "ProgramType", rdo);
            tree.Create_TreeColummn(treeList, "파라미터", "ParamText");
            tree.Create_TreeColummn(treeList, "아이콘(CS)", "ImageKey");
            tree.Create_TreeColummn(treeList, "아이콘(웹)", "WebImageKey");
            tree.Create_TreeColummn(treeList, "폴더여부", "ExpandYn", rpo.Create_rpoChk("1", "0"));
            tree.Create_TreeColummn(treeList, "메모", "Bigo");

            tree.Create_TreeColummn(treeList, "삭제", "NodeDel", btnDelete);

            //VISIBLE
            treeList.Columns["RowNum"].Visible = false;
            treeList.Columns["Id"].Visible = false;
            treeList.Columns["ParentId"].Visible = false;
            treeList.Columns["GroupLevel"].Visible = false;
            treeList.Columns["GroupYn"].Visible = false;

            //
            tree.TreeColumn_AllowTrue(treeList.Columns["Title_ko"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["GroupYn"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["ProjectName"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["FolderName"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["FormName"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["ProgramType"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["ParamText"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["ImageKey"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["WebImageKey"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["ExpandYn"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["Bigo"]);
            tree.TreeColumn_AllowTrue(treeList.Columns["NodeDel"]);

            //
            treeList.Columns["NodeDel"].Fixed = FixedStyle.Right;

            //개발자 계정일 경우 사용여부 활성화
            if (WrGlobal.LoginID.ToUpper() == "DEV")
            {
                tree.Create_TreeColummn(treeList, "사용여부", "UseYn", rpo.Create_rpoChk());
                tree.TreeColumn_AllowTrue(treeList.Columns["UseYn"]);

                //
                LupUseYn.Visible = true;
            }
            treeList.CellValueChanging += TreeList_CellValueChanging;
        }

        private void TreeList_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "UseYn")
                return;

            TreeListNode node = e.Node;
            if (node == null)
                return;

            string useYn = e.Value?.ToString();

            // 폴더인 경우
            if (node.GetValue("GroupYn")?.ToString() == "1")
            {
                node.SetValue("UseYn", useYn);

                // 하위 전체 변경
                SetChildUseYn(node.Nodes, useYn);
            }
        }

        private void SetChildUseYn(TreeListNodes nodes, string useYn)
        {
            foreach (TreeListNode node in nodes)
            {
                node.SetValue("UseYn", useYn);

                if (node.HasChildren)
                {
                    SetChildUseYn(node.Nodes, useYn);
                }
            }
        }

        private void LupUseYn_smtLookUpEditValueChanged(object sender, EventArgs e)
		{
            DisplayMenu();
        }

		private void DisplayMenu()
        {
            menu.CorpId = WrGlobal.PublicCorpID;
            menu.UseYn = LupUseYn.smtLookUpValue.ConvertString_Null_To_Empty();
            MenuTreeList.DataSource = menu.Select_SM_MenuGroup();

            MenuTreeList.BestFitColumns();
            MenuTreeList.ExpandAll();
        }
        private void SetNodeLevel(TreeListNodes nodes)
        {
            if (nodes.Count == 0) return;

            foreach(TreeListNode node in nodes)
            {
                node.SetValue("GroupLevel", node.Level);

                if (node.Nodes.Count > 0)
                {
                    node.SetValue("GroupYn", "1");
                    SetNodeLevel(node.Nodes);
                }
                else
                {
                    node.SetValue("GroupYn", "0");
                }
            }
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

        private void MenuTreeList_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            //if (e.Column.FieldName == "NodeDel")
            //{
            //    if (e.Node.ParentNode == null)
            //    {
            //        e.RepositoryItem = btnNoDelete;
            //    }
            //    else
            //    {
            //        e.RepositoryItem = btnNodeDelete;
            //    }
            //}
        }

        private void btnNodeDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("선택한 항목을 삭제 하시겠습니까?\r\n하위항목이 존재하는 경우 하위 항목도 같이 삭제됩니다.", "항목삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                TreeListNode node = MenuTreeList.FocusedNode;
                if (node == null) return;

                //DB에서 삭제
                menu.CorpId = WrGlobal.PublicCorpID;
                menu.Id = node["Id"].ConvertString_Null_To_Empty();
                menu.Del_SM_MenuGroup();

                //트리리스트에서 삭제
                List<TreeListNode> nodes = new List<TreeListNode>();
                nodes.Add(node);
                DropNode(nodes);
            }
        }

        // 노드 삭제
        void DropNode(IEnumerable<TreeListNode> nodes)
        {
            List<TreeListNode> _nodes = new List<TreeListNode>(nodes);
            foreach (TreeListNode node in _nodes)
            {
                if (node.HasChildren)
                    DropNode(node.Nodes);

                DataRowView rowView = MenuTreeList.GetRow(node.Id) as DataRowView;
                if (rowView == null)
                    return;

                menu.CorpId = WrGlobal.PublicCorpID;
                menu.Id = node["Id"].ConvertString_Null_To_Empty();
                menu.Del_SM_MenuGroup();

                MenuTreeList.Nodes.Remove(node);
            }
        }
        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            DisplayMenu();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = MenuTreeList.DataSource as DataTable;
                if (dt == null) return;

                DataRow selectItem = MenuTreeList.GetFocusedDataRow();

                string ID = selectItem["Id"].ToString();
                int GroupLevel = selectItem["GroupLevel"].ConvertInt_Null_To_Zero();

                DataRow newItem = dt.NewRow();

                newItem["Status"] = "신규";
                newItem["CorpId"] = WrGlobal.PublicCorpID;
                newItem["Id"] = sys.GetNextSequence("MenuId_Seq").ToString();
                newItem["ProjectName"] = WrGlobal.ProJectName;
                newItem["GroupYn"] = "0";
                newItem["ExpandYn"] = "0";
                newItem["PopupYn"] = "N";
                newItem["UseYn"] = "Y";
                newItem["ProgramType"] = 2;

                if (selectItem != null)
                {
                    newItem["ParentId"] = ID;
                    newItem["GroupLevel"] = GroupLevel + 1;
                }
                else
                {
                    newItem["ParentId"] = "10000";
                    newItem["GroupLevel"] = 1;
                }

                dt.Rows.Add(newItem);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
				MenuTreeList.FindFilterText = "";
				MenuTreeList.ClearFindFilter();

				this.Validate();

                //
                MenuTreeList.ExpandAll();

                MenuTreeList.BeginUpdate();

                // 메뉴 순번 매기기 + 상태변경
                for (int i = 0; i < MenuTreeList.VisibleNodesCount; i++)
                {
                    TreeListNode node = MenuTreeList.GetNodeByVisibleIndex(i);

                    int sortNo = i + 1;

                    node.SetValue("RowNum", sortNo);
                    string status = node["Status"].ConvertString_Null_To_Empty();
                    if (status != "신규") node["Status"] = "수정";

                    DataRow row = MenuTreeList.GetDataRecordByNode(node) as DataRow;

                    if (row != null)
                    {
                        row["RowNum"] = sortNo;
                    }
                }

                //
                SetNodeLevel(MenuTreeList.Nodes);

                //순번
                DataTable dt = MenuTreeList.DataSource as DataTable;
                dt.AcceptChanges();

                MenuTreeList.EndUpdate();

                //저장
                SM_AuthorGroup at = new SM_AuthorGroup();
                at.CorpId = WrGlobal.PublicCorpID;

                string groupCode = at.GetGroupCode_Dev();

                SM_UserAuthor userAuthor = new SM_UserAuthor();

                foreach (DataRow dr in dt.Rows)
                {
                    string status = dr["Status"].ConvertString_Null_To_Empty();

                    menu = new SM_MenuGroup();

                    menu.CorpId = WrGlobal.PublicCorpID;
                    menu.Id = dr["Id"].ConvertString_Null_To_Empty();
                    menu.ParentId = dr["ParentId"].ConvertString_Null_To_Empty();
                    menu.Title_ko = dr["Title_ko"].ConvertString_Null_To_Empty();
                    menu.GroupLevel = dr["GroupLevel"].ConvertInt_Null_To_Zero();
                    menu.GroupYn = dr["GroupYn"].ConvertString_Null_To_Empty();
                    menu.ExpandYn = dr["ExpandYn"].ConvertString_Null_To_Empty();
                    menu.RowNum = dr["RowNum"].ConvertInt_Null_To_Zero();
                    menu.ProjectName = dr["ProjectName"].ConvertString_Null_To_Empty();
                    menu.FolderName = dr["FolderName"].ConvertString_Null_To_Empty();
                    menu.FormName = dr["FormName"].ConvertString_Null_To_Empty();
                    menu.ParamText = dr["ParamText"].ConvertString_Null_To_Empty();
                    menu.ImageKey = dr["ImageKey"].ConvertString_Null_To_Empty();
                    menu.WebImageKey = dr["WebImageKey"].ConvertString_Null_To_Empty();
                    menu.ProgramType = dr["ProgramType"].ConvertString_Null_To_Empty();
                    menu.PopupYn = dr["PopupYn"].ConvertString_Null_To_Empty();
                    menu.Bigo = dr["Bigo"].ConvertString_Null_To_Empty();
                    menu.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

                    if (status == "신규")
					{
                        if(!menu.Ins_SM_MenuGroup())
						{
                            XtraMessageBox.Show("저장 중 오류");
                            return;
						}

						//개발자ID 권한 부여
						userAuthor.CorpId = WrGlobal.PublicCorpID;
						userAuthor.GroupCode = groupCode;
						userAuthor.MenuID = menu.Id;
                        userAuthor.Del_AuthorByMenuID();

						userAuthor.UseYn = "Y";
						userAuthor.AuthorCode = "01";
                        userAuthor.Ins_SM_UserAuthor();

						userAuthor.AuthorCode = "02";
                        userAuthor.Ins_SM_UserAuthor();
					}
					else if (status == "수정")
					{
                        if (!menu.Upd_SM_MenuGroup())
                        {
                            XtraMessageBox.Show("저장 중 오류");
                            return;
                        }
                    }

                    dr["Status"] = "";
                }

                sys.SetDashboadMenuItem();
                sys.SetCodeAuthorByMenu();

                DisplayMenu();

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdDashboardDesign_Click(object sender, EventArgs e)
        {
            TreeListNode fNode = MenuTreeList.FocusedNode;

            DataRowView drv = sys.IsExistDashboardItem((fNode.GetValue("Id") ?? "").ToString());

            if (drv == null)
            {
                MessageBox.Show("신규 등록된 메뉴는 저장 후 작성하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //frmDashBoardDesign form = new frmDashBoardDesign(drv);
            //form.ShowDialog();
        }

        private void cmdDashboardView_Click(object sender, EventArgs e)
        {
            TreeListNode fNode = MenuTreeList.FocusedNode;

            DataRowView drv = sys.IsExistDashboardItem((fNode.GetValue("Id") ?? "").ToString());

            if (drv == null || drv["XML"].ToString() == "")
            {
                MessageBox.Show("대시보드가 작성되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //frmDashBoardView form = new frmDashBoardView(drv);
            //form.ShowDialog();
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}