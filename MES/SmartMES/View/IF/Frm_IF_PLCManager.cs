using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.IF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.IF
{
    public partial class Frm_IF_PLCManager : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력
        clsCode code = new clsCode();

        public Frm_IF_PLCManager()
        {
            InitializeComponent();
            SetDesign();
        }

        private void SetDesign()
        {
            Set_DGPControl(gvDGPControl);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(gvDGPControl, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_DGPControl(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false; //DataTable 입력시 Column 생성 방지
            gv.OptionsView.ShowGroupPanel = false;

            RepositoryItemSearchLookUpEdit lup_UserId = rpo.Create_RepositoryItemSearchLookUp(code.Get_UserList(), "코드", "코드명");

            RepositoryItemComboBox cbCompany = new RepositoryItemComboBox();
            cbCompany.Items.Add("LS");
            cbCompany.Items.Add("Mitsubishi");
            cbCompany.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "컨트롤코드", "CTCode");
            grid.Create_GvColumn(gv, "컨트롤명", "CTName");
            grid.Create_GvColumn(gv, "PLC제조사", "CT_Company", cbCompany);
            grid.Create_GvColumn(gv, "IP", "CT_IP");
            grid.Create_GvColumn(gv, "Port", "CT_Port");
            grid.Create_GvColumn(gv, "사용자", "EmployeeId", lup_UserId);
            grid.Create_GvColumn(gv, "사용여부", "CT_UserYn");
            grid.Create_GvColumn(gv, "자동로그인", "AutoLoginYn");
            //grid.Create_GvColumn(gv, "등록자", "CreId");
            //grid.Create_GvColumn(gv, "등록일시", "CreDt");
            //grid.Create_GvColumn(gv, "수정자", "ModId");
            //grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.Column_AllowEdit(gv.Columns["CTName"]);
            grid.Column_AllowEdit(gv.Columns["CT_Company"]);
            grid.Column_AllowEdit(gv.Columns["CT_IP"]);
            grid.Column_AllowEdit(gv.Columns["CT_Port"]);
            grid.Column_AllowEdit(gv.Columns["CT_UserYn"]);
            grid.Column_AllowEdit(gv.Columns["EmployeeId"]);

            RepositoryItemCheckEdit chk_Use = rpo.Create_rpoChk();
            gv.Columns["CT_UserYn"].ColumnEdit = chk_Use;
            gv.Columns["AutoLoginYn"].ColumnEdit = chk_Use;

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
            gv.Columns["CT_UserYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //사용여부
            gv.Columns["AutoLoginYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //사용여부

            //grid.RowNumber_Indicator(gv);       //행순번 표기
            gv.CellValueChanged += Event_CellValueChanged;                  //셀값 수정시 상태값 수정
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                //case (Keys.Alt | Keys.I): Btn_Row_Ins.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                //case (Keys.Alt | Keys.D): Btn_Row_Del.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                string ctCode = (gvDGPControl.GetFocusedRowCellValue("CTCode") ?? "").ToString();

                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("CT_UserYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(gvDGPControl.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(gcDGPControl, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            GridView gv = sender as GridView;
            grid.Check_OverLap_CellValue(gv, e, "DgpCode");
        }

        #region Event

        private void frmPLCSetting_Load(object sender, EventArgs e)
        {
            SearchDGPControl();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "BtnSave": Func_Save(); break;
            }
        }

        #endregion Event End

        #region 조회 기능

        #region 품목 검사항목 조회

        private void SearchDGPControl()
        {
            if (!Check_Search()) return;

            IF_DGPControl mc = new IF_DGPControl();
            DataTable dt = mc.Select_IF_DGPControl();

            gcDGPControl.DataSource = dt;
            gvDGPControl.BestFitColumns();
        }

        private bool Check_Search()
        {
            if ((gvDGPControl.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
                                                                    OR Status = '수정'
                                                                    OR Status = '삭제'").Length > 0)
            {
                if (MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //다른항목 진행할 경우
                    return true;
                }
                else
                {
                    //다른항목 진행하지 않을경우
                    return false;
                }
            }

            return true;
        }

        #endregion 품목 검사항목 조회 End

        #endregion 조회버튼 클릭 End

        #region 저장 기능


        private void Func_Save()
        {
            //작성내역 저장
            DataTable dtItem = gcDGPControl.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtItem)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dtItem.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'D')) return;
                }
            }

            SearchDGPControl();

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            //미입력된 칸 있는지 확인
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Status"]) == "신규")
                {
                    if (Convert.ToString(dr["CTName"]) == "")
                    {
                        MessageBox.Show("컨트롤명 항목이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["CT_Company"]) == "")
                    {
                        MessageBox.Show("PLC제조사 항목이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["EmployeeId"]) == "")
                    {
                        MessageBox.Show("사용자ID 항목이 비어있습니다.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정")
                {
                    if (Convert.ToString(dr["CTName"]) == "")
                    {
                        MessageBox.Show("컨트롤명 항목이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["CT_Company"]) == "")
                    {
                        MessageBox.Show("PLC제조사 항목이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["EmployeeId"]) == "")
                    {
                        MessageBox.Show("사용자ID 항목이 비어있습니다.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {

                    isDelete = true;
                    checkCnt++;
                }
            }
            if (checkCnt == 0) return false;

            //삭제내역 있으면 물어보기
            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {

            //데이터베이스에 반영
            //1. 기본 셋팅
            IF_DGPControl mc = new IF_DGPControl();
            mc.CTCode = dr["CTCode"].ConvertString_Null_To_Empty();   // 컨트롤코드
            if (div != 'D')
            {
                //Delete제외 공통
                mc.CTName = dr["CTName"].ConvertString_Null_To_Empty();   // 컨트롤명
                mc.CT_Company = dr["CT_Company"].ConvertString_Null_To_Empty();   // PLC제조사
                mc.CT_IP = dr["CT_IP"].ConvertString_Null_To_Empty();   // 컨트롤IP
                mc.CT_Port = dr["CT_Port"].ConvertString_Null_To_Empty();   // 컨트롤Port
                mc.CT_UserYn = dr["CT_UserYn"].ConvertString_Null_To_Empty();   // 컨트롤 사용여부
                mc.EmployeeId = dr["EmployeeId"].ConvertString_Null_To_Empty();   // 컨트롤 사용자
            }

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!mc.Insert_IF_DGPControl())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'U')
            {
                //Update
                if (!mc.Update_IF_DGPControl())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'D')
            {
                //Delete
                if (!mc.Delete_IF_DGPControl())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }

            //상태값 초기화
            dr[0] = "";


            return true;

        }

        #endregion 저장 기능 End

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}