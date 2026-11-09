using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SmartMES.Common;
using SmartMES.Models;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using SmartCommon.Common;
using baseSMT;
using SmartMES.Models.SM;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_AuthorGroup : DevExpress.XtraEditors.XtraForm
    {
        clsSystem sys = new clsSystem();
        clsRepository rpo = new clsRepository();
        clsTree clsTree = new clsTree();
        Database db = new Database();

        clsSystem code = new clsSystem();
        clsGrid grid = new clsGrid();

        bool isTreeChanged = false;

        public Frm_SM_AuthorGroup()
        {
            InitializeComponent();
        }

        private void Frm_SM_AuthorGroup_Load(object sender, EventArgs e)
        {
            if (WrGlobal.AuthorList.Exists(s => s.MenuID == this.Tag.ToString() && s.AuthorCode == "02"))
            {
                cmdInsert.Visible = true;
                cmdSave.Visible = true;
                cmdDelete.Visible = true;
            }

            SetDesign();
            Func_Search();
        }

        #region Setting

        private void SetDesign()
        {
            Set_GridView(Gv_List);
            Set_Tree(treeList);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_List, "Status"))
                {
                    e.Cancel = true;
                }
            });

        }
        private void Set_GridView(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            //gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "그룹코드", "GroupCode");
            grid.Create_GvColumn(gv, "권한그룹명", "GroupName");

            grid.Column_AllowEdit(gv.Columns["GroupName"]);

            gv.Columns["GroupCode"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void Gv_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = Gv_List.GetFocusedDataRow();
            if (dr == null) return;

            string groupCode = dr["GroupCode"].ConvertString_Null_To_Empty();

            LoadMenuTree(groupCode);

            if (WrGlobal.LoginID.ToUpper() != "DEV")
            {
                treeList.Columns["FormName"].Visible = false;
            }
        }

        private void Set_Tree(TreeList tree)
        {
            tree.Columns.Clear();

            tree.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            tree.OptionsView.AutoWidth = false;
            tree.OptionsCustomization.AllowSort = false;

            clsTree.Create_TreeColummn(tree, "Id", "Id");
            clsTree.Create_TreeColummn(tree, "ParentId", "ParentId");
            clsTree.Create_TreeColummn(tree, "메뉴항목명", "Title_ko");
            clsTree.Create_TreeColummn(tree, "그룹여부", "GroupYn");
            clsTree.Create_TreeColummn(tree, "조회", "Author1", rpo.Create_rpoChk());
            clsTree.Create_TreeColummn(tree, "편집", "Author2", rpo.Create_rpoChk());
            clsTree.Create_TreeColummn(tree, "폼명", "FormName");

            clsTree.TreeColumn_AllowTrue(tree.Columns["Author1"]);
            clsTree.TreeColumn_AllowTrue(tree.Columns["Author2"]);

            tree.Columns["Id"].Visible = false;
            tree.Columns["GroupYn"].Visible = false;

            treeList.OptionsBehavior.ImmediateEditor = true;
            treeList.CellValueChanging += treeList_CellValueChanging;
            treeList.CellValueChanged += treeList_CellValueChanged;
        }

        private void treeList_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Author1")
            {
                UpdateNodeCheckImmediate("Author1", e.Value);
            }

            if (e.Column.FieldName == "Author2")
            {
                UpdateNodeCheckImmediate("Author2", e.Value);
            }
        }

        private void UpdateNodeCheckImmediate(string colName, object value)
        {
            TreeListNode node = treeList.FocusedNode;
            if (node == null) return;

            string groupYn = (node.GetValue("GroupYn") ?? "").ToString();
            if (groupYn != "1") return;

            bool isChecked = value?.ToString() == "Y";

            SetNodeCheck(node.Nodes, colName, isChecked);

            node.SetValue(colName, isChecked ? "Y" : "N");
        }

        private void SetNodeCheck(TreeListNodes nodes, string colName, bool isChecked)
        {
            if (nodes.Count == 0) return;

            foreach (TreeListNode node in nodes)
            {
                if (node.GetValue("GroupYn").ToString() == "1")
                {
                    node.SetValue(colName, isChecked ? "Y" : "N");
                    SetNodeCheck(node.Nodes, colName, isChecked);
                }
                else
                {
                    node.SetValue(colName, isChecked ? "Y" : "N");
                }
            }
        }

        private void treeList_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Author1" || e.Column.FieldName == "Author2")
            {
                isTreeChanged = true;
            }
        }

        private void LoadMenuTree(string GroupCode)
        {
            string query = "";
            query = $@"exec Code_GetCodeAuthorByGroupCode '{WrGlobal.PublicCorpID}', '{GroupCode}'";

            DataTable dt = db.GetDataTable(query);
            treeList.DataSource = dt;

            treeList.KeyFieldName = "Id";
            treeList.ParentFieldName = "ParentId";

            treeList.ExpandAll();
            treeList.BestFitColumns();
        }

        #endregion Setting End

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): cmdInsert.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): cmdDelete.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            try
            {
                DataRow drItemClass = Gv_List.GetFocusedDataRow();
                if (drItemClass == null) return;

                //행삽입, 추가, 삭제 버튼 클릭 이벤트
                //1. 이름의 마지막 3글자 가져오기
                smtSearchButton btn = sender as smtSearchButton;
                string txt_Act = btn.Tag.ToString();

                //2. 입력 데이터 설정
                Dictionary<string, object> dic = new Dictionary<string, object>();

                if (txt_Act != "Del")
                {
                    dic.Add("Status", "신규");
                }
                else
                {
                    //삭제 진행인경우
                    dic.Add("Status", Convert.ToString(Gv_List.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
                }

                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(Gc_List, dic, txt_Act);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 자료를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            try
            {
                DataRow dr = Gv_List.GetFocusedDataRow();
                if (dr == null) return;

                string groupCode = dr["GroupCode"].ConvertString_Null_To_Empty();

                SM_UserAuthor ua = new SM_UserAuthor();
                ua.CorpId = WrGlobal.PublicCorpID;
                ua.GroupCode = groupCode;

                if (!ua.Del_SM_UserAuthor())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return;
                }

                SM_AuthorGroup ag = new SM_AuthorGroup();
                ag.GroupCode = groupCode;

                if (!ag.Del_SM_AuthorGroup())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return;
                }

                MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Func_Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion 행 삽입, 추가, 삭제 End

        #region 조회 기능

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Func_Search()
        {
            // 데이터 조회
            SM_AuthorGroup ag = new SM_AuthorGroup();
            Gc_List.DataSource = ag.Select_SM_AuthorGroup();

            Gv_List.BestFitColumns();   //Column 너비 조정
        }

        #endregion 조회 기능 End

        #region 저장 기능

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            Gv_List.PostEditor();           //입력한 값을 확정
            Gv_List.UpdateCurrentRow();     //현재 행(Row)의 변경 내용을 데이터 소스에 적용

            //작성내역 저장
            DataTable dt = Gc_List.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }

            DataRow dr = Gv_List.GetFocusedDataRow();
            string groupCode = dr["GroupCode"].ConvertString_Null_To_Empty();

            Save_TreeAuthor(groupCode);

            //3. 정상절차 진행
            Func_Search();

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private bool Check_SaveClick(DataTable dt)
        {
            int checkCnt = 0;
            bool isDelete = false;

            //Data를 불러온 상태인지 검사
            if (dt == null)
            {
                return false;
            }

            //변경사항이 없을 경우 RETURN
            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    //미입력된 칸 있는지 확인
                    if (dr["GroupName"].IsNullOrEmpty())
                    {
                        MessageBox.Show("권한그룹명이 비어있습니다.");
                        return false;
                    }

                    checkCnt++;
                }
                else if (status == "수정")
                {
                    checkCnt++;
                }
                else if (status == "삭제")
                {
                    checkCnt++;
                    isDelete = true;
                }
            }

            if (checkCnt == 0 && !isTreeChanged) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    return true;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            SM_AuthorGroup ag = new SM_AuthorGroup();
            ag.GroupCode = dr["GroupCode"].ConvertString_Null_To_Empty();
            ag.GroupName = dr["GroupName"].ConvertString_Null_To_Empty();

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    ag.GroupCode = sys.GetNewAuthorGroupCode();

                    //Insert
                    if (!ag.Ins_SM_AuthorGroup())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }

                    dr["GroupCode"] = ag.GroupCode;
                }
                else if (div == 'U')
                {
                    //Update
                    if (!ag.Upd_SM_AuthorGroup())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!ag.Del_SM_AuthorGroup())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }

                //상태값 초기화
                dr[0] = "";

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void Save_TreeAuthor(string groupCode)
        {
            if (string.IsNullOrEmpty(groupCode))
            {
                MessageBox.Show("권한그룹을 선택해주세요.");
                return;
            }

            treeList.PostEditor();
            treeList.CloseEditor();

            // 기존 권한 삭제
            Delete_UserAuthor(groupCode);

            foreach (TreeListNode node in treeList.Nodes)
            {
                Save_NodeAuthor(node, groupCode);
            }
        }

        private void Delete_UserAuthor(string groupCode)
        {
            SM_UserAuthor ua = new SM_UserAuthor();
            ua.CorpId = WrGlobal.PublicCorpID;
            ua.GroupCode = groupCode;

            ua.Del_SM_UserAuthor();
        }

        private void Save_NodeAuthor(TreeListNode node, string groupCode)
        {
            if (node == null) return;

            string menuId = node.GetValue("Id").ConvertString_Null_To_Empty();
            string author1 = node.GetValue("Author1").ConvertString_Null_To_Empty();
            string author2 = node.GetValue("Author2").ConvertString_Null_To_Empty();

            if (author1 != "Y" && author2 != "Y")
            {
                foreach (TreeListNode child in node.Nodes)
                {
                    Save_NodeAuthor(child, groupCode);
                }
                return;
            }

            if (author1 == "Y")
                Insert_UserAuthor(groupCode, menuId, "01");

            if (author2 == "Y")
                Insert_UserAuthor(groupCode, menuId, "02");

            foreach (TreeListNode child in node.Nodes)
            {
                Save_NodeAuthor(child, groupCode);
            }
        }

        private void Insert_UserAuthor(string groupCode, string menuId, string authorCode)
        {
            SM_UserAuthor ua = new SM_UserAuthor();
            ua.CorpId = WrGlobal.PublicCorpID;
            ua.GroupCode = groupCode;
            ua.MenuID = menuId;
            ua.AuthorCode = authorCode;
            ua.UseYn = "Y";

            if (!ua.Ins_SM_UserAuthor())
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
                return;
            }
        }

        #endregion 저장 기능 End


        private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}