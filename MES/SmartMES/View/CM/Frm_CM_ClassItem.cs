using System;
using System.Data;
using SmartMES.Models;
using DevExpress.XtraTreeList;
using SmartCommon.Common;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using System.Windows.Forms;
using SmartMES.Models.CM;
using System.Collections.Generic;
using baseSMT;
using DevExpress.XtraEditors.Controls;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_ClassItem : DevExpress.XtraEditors.XtraForm
    {
        clsCode codes = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_CM_ClassItem()
        {
            InitializeComponent();
        }

        private void Frm_CM_ClassItem_Load(object sender, EventArgs e)
        {
            if (WrGlobal.AuthorList.Exists(s => s.MenuID == this.Tag.ToString() && s.AuthorCode == "02"))
            {
                cmdSave.Visible = true;
                cmdDel.Visible = true;
            }

            if (WrGlobal.LoginID.ToUpper() != "DEV") gvItem.Columns.Remove(gvItem.Columns["UseYn"]);

            SetGvItem(gvItem);

            // 트리조회
            DisplayCategory();
        }

        #region GridView Setting

        private void SetGvItem(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            gvItem.OptionsView.ShowFooter = true;

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "코드", "ClassItemCode");
            grid.Create_GvColumn(gv, "타이틀", "Title");
            grid.Create_GvColumn(gv, "참고사항1", "ExtraValue");
            grid.Create_GvColumn(gv, "참고사항2", "ExtraValue1");
            grid.Create_GvColumn(gv, "정렬순번", "OrderSeq");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "사용유무", "UseYn", rpo.Create_rpoChk());

            //Format
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ClassItemCode"]);
            grid.Column_AllowEdit(gv.Columns["Title"]);
            grid.Column_AllowEdit(gv.Columns["ExtraValue"]);
            grid.Column_AllowEdit(gv.Columns["ExtraValue1"]);
            grid.Column_AllowEdit(gv.Columns["OrderSeq"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            //이벤트 설정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
        }

        #endregion

        #region 조회 기능

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            // 트리조회
            DisplayCategory();
        }

        private void DisplayCategory()
        {
            treeListCategoryGroup.DataSource = codes.GetCategory();
            treeListCategoryGroup.ExpandAll();
        }

        private void treeListCategoryGroup_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeListMultiSelection selectedNodes = treeListCategoryGroup.Selection;

            //251028 : 조회 리스트가 없는 경우 조회 내용의 첫번째 node 자동 선택 이벤트 오류 예외처리
            if (selectedNodes[0] != null)
            {
                string class1 = selectedNodes[0].GetValue(treeListCategoryGroup.Columns["CLASS"]).ToString().Trim();
                DisplayData(class1);
            }
        }

        private void DisplayData(string class1)
        {
            CM_CateItem ci = new CM_CateItem();
            gcItem.DataSource = ci.GetCategory(class1);
            gvItem.BestFitColumns();
        }
        #endregion

        #region 저장 기능

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            gvItem.PostEditor();
            gvItem.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = gcItem.DataSource as DataTable;

            //현재 선택된 CLASS 저장
            string class1 = treeListCategoryGroup.FocusedNode["CLASS"].ConvertString_Null_To_Empty();

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
            //3. 정상절차 진행
            DisplayData(class1);
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
                    if (dr["ClassItemCode"].IsNullOrEmpty())
                    {
                        MessageBox.Show("코드를 입력해주세요.");
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

            if (checkCnt == 0) return false;

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
            CM_CateItem ci = new CM_CateItem();
            ci.CorpId = WrGlobal.CorpID;
            ci.Class = treeListCategoryGroup.FocusedNode["CLASS"].ConvertString_Null_To_Empty();
            ci.ClassItemCode = dr["ClassItemCode"].ConvertString_Null_To_Empty();
            ci.Title = dr["Title"].ConvertString_Null_To_Empty();
            ci.ExtraValue = dr["ExtraValue"].ConvertString_Null_To_Empty();
            ci.ExtraValue1 = dr["ExtraValue1"].ConvertString_Null_To_Empty();
            ci.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
            ci.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();
            ci.Remark = dr["Remark"].ConvertString_Null_To_Empty();

            string item = dr["ClassItemCode"].ConvertString_Null_To_Empty();

            try
            {
                if (div == 'I')
                {
                    if (!ci.InsertCateItem())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    if (!ci.UpdateCateItem(item))
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    if (!ci.DeleteCateItem())
                    {
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
        #endregion

        #region Event

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
            string ColName = e.Column.FieldName;
            string value = e.Value.ConvertString_Null_To_Empty();

            //수정사항 있을시 상태값 수정 표기
            if (ColName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeListCategoryGroup_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
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

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            grid.Check_OverLap_CellValue(sender as GridView, e, "ClassItemCode");
        }

        #endregion

        #region 행 삽입, 추가, 삭제

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            try
            {
                //행삽입, 추가, 삭제 버튼 클릭 이벤트
                //1. 이름의 마지막 3글자 가져오기
                smtSearchButton btn = sender as smtSearchButton;
                string txt_Act = btn.Tag.ToString();

                //2. 입력 데이터 설정
                Dictionary<string, object> dic = new Dictionary<string, object>();

                if (txt_Act != "Del")
                {
                    dic.Add("Status", "신규");
                    dic.Add("UseYn", "Y");
                }
                else
                {
                    //삭제 진행인경우
                    dic.Add("Status", Convert.ToString(gvItem.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
                }

                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(gcItem, dic, txt_Act);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}