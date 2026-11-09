using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using baseSMT;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_Dept : DevExpress.XtraEditors.XtraForm
    {
        clsRepository rpo = new clsRepository();
        clsSystem code = new clsSystem();
        clsGrid grid = new clsGrid();

        public Frm_CM_Dept()
        {
            InitializeComponent();
        }

        private void Frm_CM_Dept_Load(object sender, EventArgs e)
        {
            SetDesign();
            Func_Search();
        }

        private void SetDesign()
        {
            Set_GridView(Gv_List);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_List, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        #region GridView Setting

        private void Set_GridView(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "부서코드", "DeptCode");
            grid.Create_GvColumn(gv, "부서명", "DeptName");
            grid.Create_GvColumn(gv, "메모", "Bigo");
            grid.Create_GvColumn(gv, "사용유무", "UseYn", rpo.Create_rpoChk());

            grid.Column_AllowEdit(gv.Columns["DeptName"]);
            grid.Column_AllowEdit(gv.Columns["Bigo"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            gv.Columns["DeptCode"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            grid.Check_OverLap_CellValue(sender as GridView, e, "DeptCode");
        }

        #endregion GridView Setting End

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

        #endregion 행 삽입, 추가, 삭제 End

        #region 조회 기능

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Func_Search()
        {
            // 데이터 조회
            CM_Dept cd = new CM_Dept();

            Gc_List.DataSource = cd.Select_CM_Dept();

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
                    if (dr["DeptName"].IsNullOrEmpty())
                    {
                        MessageBox.Show("부서명이 비어있습니다.");
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
            //데이터베이스에 반영
            //1. 기본 셋팅
            CM_Dept cd = new CM_Dept();
            cd.DeptCode = dr["DeptCode"].ConvertString_Null_To_Empty();
            cd.DeptName = dr["DeptName"].ConvertString_Null_To_Empty();
            cd.Bigo = dr["Bigo"].ConvertString_Null_To_Empty();
            cd.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    cd.DeptCode = code.GetNextSequence("DeptId_Seq").ToString();

                    //Insert
                    if (!cd.Ins_CM_Dept())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    //Update
                    if (!cd.Upd_CM_Dept())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!cd.Del_CM_Dept())
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

        #endregion 저장 기능 End

        private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}