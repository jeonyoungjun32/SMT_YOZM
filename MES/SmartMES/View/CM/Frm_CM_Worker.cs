using baseSMT;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.SM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_Worker : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        string query = "";

        public Frm_CM_Worker()
        {
            InitializeComponent();
        }

        private void Frm_CM_Worker_Load(object sender, EventArgs e)
        {
            Set_GridView(gvWorker);

            DisplayData();
        }

        private void Set_GridView(GridView gv)
        {
            //GvBOM Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "작업자코드", "EmployeeId");
            grid.Create_GvColumn(gv, "작업자명", "EmployeeName");
            //grid.Create_GvColumn(gv, "비밀번호", "Password");
            grid.Create_GvColumn(gv, "부서코드", "DeptId");
            grid.Create_GvColumn(gv, "사용여부", "UseYn");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");




			//Column 단일 설정
			////비밀번호
			//RepositoryItemTextEdit txt_Password = rpo.Create_rpoTxt_Length(20);
			//txt_Password.PasswordChar = '*';
			////부서
			RepositoryItemSearchLookUpEdit Lup_Dept = rpo.Create_RepositoryItemSearchLookUp(code.GetDeptList(false), "코드", "명칭");


			//gv.Columns["Password"].ColumnEdit = txt_Password;
			gv.Columns["DeptId"].ColumnEdit = Lup_Dept;
            gv.Columns["UseYn"].ColumnEdit = rpo.Create_rpoChk();

			//         //셀값 변경 허용
			//         if (WrGlobal.ProcOption["SC"]["Emp_AutoNo"] != "Y")
			//         {
			//             grid.Column_AllowEdit(gv.Columns["EmployeeId"]);
			//}
			//else
			//{
			gv.Columns["EmployeeId"].Visible = false;
			//         }

			grid.Column_AllowEdit(gv.Columns["EmployeeName"]);
            //grid.Column_AllowEdit(gv.Columns["Password"]);
            grid.Column_AllowEdit(gv.Columns["DeptId"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;     //상태

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            //gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            gv.DoubleClick += Event_DoubleClick;

            grid.Set_Navigator(gvWorker);

        }


        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

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
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            smtSearchButton btn = sender as smtSearchButton;
            string txt_Act = btn.Tag.ToString();

            //작업부
            //string Dept_Cd = Lup_Dept.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Dept.EditValue.ConvertString_Null_To_Empty();

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                //dic.Add("Dept_Cd", Dept_Cd);
                dic.Add("WorkerYn", "Y");
                dic.Add("UseYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(gvWorker.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(gcWorker, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End


        private void Event_DoubleClick(object sender, EventArgs e)
        {
            //DXMouseEventArgs dxMouseEventArgs = e as DXMouseEventArgs;

            //GridView gridView = sender as GridView;
            //GridHitInfo info = gridView.CalcHitInfo(dxMouseEventArgs.Location);

            //DataRow dataRow = gridView.GetDataRow(info.RowHandle);
            //if (dataRow == null || Convert.ToString(dataRow["Status"]) == "신규")
            //{
            //    MessageBox.Show("사용자 정보 미저장");
            //    return;
            //}

            //Frm_SC_Emp_Image frm_SC_Emp_Image = new Frm_SC_Emp_Image();
            //frm_SC_Emp_Image.dataRow = dataRow;
            //frm_SC_Emp_Image.ShowDialog();
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            SM_User cu = new SM_User();
            gcWorker.DataSource = cu.SelectWorkerList();

            gvWorker.BestFitColumns();
        }

        #region 저장 기능
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                gvWorker.PostEditor();
                gvWorker.UpdateCurrentRow();

                //작성내역 저장
                DataTable dt = gcWorker.DataSource as DataTable;

                //1. 데이터 불러온 상태인지 검사
                if (!Check_SaveClick(dt)) return;

                //2.데이터 저장
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
                //3.조회
                DisplayData();
                MessageBox.Show("저장이 정상적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Data를 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    if (WrGlobal.ProcOption["SC"]["Worker_AutoNo"] != "Y")
                    {
                        MessageBox.Show("작업자코드 항목이 비어있습니다.");
                        return false;
                    }

                    if (dr["EmployeeName"].ConvertString_Null_To_Empty() == "")
                    {
                        MessageBox.Show("작업자명 항목이 비어있습니다.");
                        return false;
                    }

                    //if (dr["Password"].ConvertString_Null_To_Empty() == "")
                    //{
                    //    MessageBox.Show("비밀번호 항목이 비어있습니다.");
                    //    return false;
                    //}

                    checkCnt++;
                }
                else if (status == "수정") { checkCnt++; }
                else if (status == "삭제")
                {
                    isDelete = true;

                    checkCnt++;
                }
            }
            if (checkCnt == 0) return false;

			if (isDelete)
			{
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.Cancel) { return false; }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            SM_User cu = new SM_User();
            cu.EmployeeId = dr["EmployeeId"].ConvertString_Null_To_Empty();
            cu.EmployeeName = dr["EmployeeName"].ConvertString_Null_To_Empty();
            //cu.Password = clsCommon.SHA256Hash(dr["Password"].ConvertString_Null_To_Empty());
            cu.DeptId = dr["DeptId"].ConvertString_Null_To_Empty();
            cu.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();
            cu.WorkerYn = "Y";

            //2. DB 적용
            if (div == 'I')
            {
           //Insert
                if (!cu.InsertWorker())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'U')
            {
                //Update
                if (!cu.UpdateWorker())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'D')
            {
                //Update
                if (!cu.Del_SM_User())
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
        #endregion 저장 기능 end

        //엑셀 버튼
        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(gvWorker);
        }

        private void gvClass_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gvWorker_ShowingEditor(object sender, CancelEventArgs e)
        {
            // 현재 행의 상태 확인 (예: 신규, 수정)
            string status = Convert.ToString(gvWorker.GetRowCellValue(gvWorker.FocusedRowHandle, "Status"));

            // 상태가 "신규"가 아닐 때, ItemCode, Class, ItemCode 칼럼의 편집을 막음
            if (status != "신규")
            {
                if (gvWorker.FocusedColumn.FieldName == "EmployeeId")
                {
                    e.Cancel = true;  // 편집을 취소
                }
            }
        }
	}
}