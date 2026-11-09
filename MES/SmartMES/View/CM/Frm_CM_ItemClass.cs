using baseSMT;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
	public partial class Frm_CM_ItemClass : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        Database db = new Database();

        string ClassCode = "A012";
        string ItemType = "20,40,50";

        public Frm_CM_ItemClass()
        {
            InitializeComponent();
        }

        private void Frm_CM_ItemClass_Load(object sender, EventArgs e)
        {
            SetDesign();
            Display();
        }

        #region Setting

        private void SetDesign()
        {
            SetLup();
            SetGridView(gvItemClass);
        }

        private void SetLup()
        {
            string query = $@"Select Class as 코드, ClassName as 명칭 FROM CM_CLASS Where CorpId = '{WrGlobal.CorpID}' and CLASSLEVEL = 2 and HIGHCLASS = '{ClassCode}'";
            DataTable dt = db.GetDataTable(query);

            if (dt.Rows.Count < 0) return;

            lupItemClass.smtLookUpDataSource = dt;
            lupItemClass.smtLookUpDataDisplayMember = "명칭";
            lupItemClass.smtLookUpDataValueMember = "코드";

            if (dt.Rows.Count > 0)
            {
                lupItemClass.smtLookUpValue = Convert.ToString(dt.Rows[0]["코드"]);
            }
        }

        private void SetGridView(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬

            RepositoryItemSearchLookUpEdit lup_ItemType = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목구분", new string[] { ItemType }, ""), "코드", "코드명");

            //사용여부
            RepositoryItemCheckEdit chk_Use = new RepositoryItemCheckEdit();
            chk_Use.ValueChecked = "Y";
            chk_Use.ValueUnchecked = "N";
            chk_Use.NullStyle = StyleIndeterminate.Unchecked;
            chk_Use.AllowGrayed = false;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "품목분류", "CLASS");
            grid.Create_GvColumn(gv, "코드", "ClassItemCode");
            grid.Create_GvColumn(gv, "코드", "Item");
            grid.Create_GvColumn(gv, "내용", "Title");
            grid.Create_GvColumn(gv, "품목구분", "ExtraValue", lup_ItemType);
            grid.Create_GvColumn(gv, "참고사항", "ExtraValue1");
            grid.Create_GvColumn(gv, "사용유무", "UseYn", chk_Use);

            grid.Column_AllowEdit(gv.Columns["ClassItemCode"]);
            grid.Column_AllowEdit(gv.Columns["Title"]);
            grid.Column_AllowEdit(gv.Columns["ExtraValue"]);
            grid.Column_AllowEdit(gv.Columns["ExtraValue1"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            gv.Columns["CLASS"].Visible = false;
            gv.Columns["Item"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
        }

        #endregion Setting End

        #region 버튼

        private void cmd_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;

            switch (btn.Name)
            {
                case "cmdDisplay":
                    Display();
                    break;

                case "cmdSave":
                    Save();
                    break;

                case "cmdClose":
                    this.Close();
                    break;
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            try
            {
                DataRow drItemClass = gvItemClass.GetFocusedDataRow();
                //if (drItemClass == null) return;

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
                    dic.Add("Status", Convert.ToString(gvItemClass.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
                }

                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(gcItemClass, dic, txt_Act);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion 버튼 END

        #region 조회

        private void Display()
        {
            CM_CateItem CM_CateItem = new CM_CateItem();
            CM_CateItem.Class = lupItemClass.smtLookUpValue.ConvertString_Null_To_Empty();

            gcItemClass.DataSource = CM_CateItem.SelectCmCateItem();

            gvItemClass.BestFitColumns();
        }

        #endregion 조회 END

        #region 저장

        private void Save()
        {
            GridView gv = (gcItemClass.MainView as GridView);
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = gcItemClass.DataSource as DataTable;

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

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            //3. 선택한 row 저장
            int SaveFocusedRowInx = gv.FocusedRowHandle;


            //4. 정상절차 진행
            Display();

            //5. 저장 후 선택한 row를 불러옴
            (gcItemClass.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            //변경사항이 없을 경우 RETURN
            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
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
            //데이터베이스에 반영
            //1. 기본 셋팅
            CM_CateItem CM_CateItem = new CM_CateItem();
            CM_CateItem.Class = lupItemClass.smtLookUpValue.ConvertString_Null_To_Empty();
            CM_CateItem.ClassItemCode = dr["ClassItemCode"].ConvertString_Null_To_Empty();
            CM_CateItem.Title = dr["Title"].ConvertString_Null_To_Empty();
            CM_CateItem.ExtraValue = dr["ExtraValue"].ConvertString_Null_To_Empty();
            CM_CateItem.ExtraValue1 = dr["ExtraValue1"].ConvertString_Null_To_Empty();
            CM_CateItem.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

            // 기존 ClassItemCode
            string item = dr["Item"].ConvertString_Null_To_Empty();

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    //Insert
                    if (!CM_CateItem.InsertCateItem())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    //Update
                    if (!CM_CateItem.UpdateCateItem(item))
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!CM_CateItem.DeleteCateItem())
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

        #endregion 저장 End

        private void gvItemClass_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            //코드 중복 확인
            if (e.Column.FieldName == "ClassItemCode")
            {
                CM_CateItem CM_CateItem = new CM_CateItem();
                CM_CateItem.Class = lupItemClass.smtLookUpValue.ConvertString_Null_To_Empty();
                CM_CateItem.ClassItemCode = dr["ClassItemCode"].ConvertString_Null_To_Empty();

                DataRow chkTable = CM_CateItem.Select_CateItem_ByCode();
                if (!chkTable.IsNullOrEmpty())
                {
                    MessageBox.Show("전체 리스트 중 동일한 코드가 존재합니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dr["ClassItemCode"] = DBNull.Value;

                    return;
                }
            }
        }
    }
}