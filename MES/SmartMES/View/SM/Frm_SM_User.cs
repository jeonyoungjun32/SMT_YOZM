using baseSMT;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.SM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static DevExpress.Skins.SolidColorHelper;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_User : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsGrid grid = new clsGrid();
        clsSystem sys = new clsSystem();
        clsCode code = new clsCode();
        clsRepository rpo = new clsRepository();
        SM_User user = new SM_User();

        private string empId = "";

        public Frm_SM_User()
        {
            InitializeComponent();
            InitializePictureEdit();
        }

        // PictureEdit 기본 설정
        private void InitializePictureEdit()
        {
            pictureEdit.Properties.ContextMenuStrip = new ContextMenuStrip();  // 기본 컨텍스트 메뉴 제거
            pictureEdit.Properties.ContextMenuStrip.Items.Add("이미지 등록", null, OnLoadImage);
            pictureEdit.Properties.ContextMenuStrip.Items.Add("이미지 삭제", null, OnDeleteImage);
            pictureEdit.Properties.ShowMenu = false; // 기본 메뉴 숨기기
            pictureEdit.Dock = DockStyle.Fill;
            pictureEdit.Properties.NullText = "이미지가 없습니다. 우클릭을 눌러 이미지를 등록하세요. ";  // 기본 'No image' 텍스트 변경
        }

        private void Frm_SM_User_Load(object sender, EventArgs e)
        {
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);

            SetDesign();
            Func_Search();
        }

        #region Image Methods

        private void OnLoadImage(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureEdit.Image = Image.FromFile(openFileDialog.FileName);

                byte[] imageBytes = File.ReadAllBytes(openFileDialog.FileName);

                user.EmployeeId = empId;
                user.Upd_SM_User_Pic(imageBytes);

                MessageBox.Show("이미지 등록이 완료되었습니다.");
            }
        }

        private void OnDeleteImage(object sender, EventArgs e)
        {
            if (MessageBox.Show("이미지를 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                user.EmployeeId = empId;
                user.Del_SM_User_Pic();

                pictureEdit.Image = null;
            }
        }

        #endregion END Image Methods

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

            RepositoryItemSearchLookUpEdit Lup_Corp = rpo.Create_RepositoryItemSearchLookUp(code.GetCorpList(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit Lup_Dept = rpo.Create_RepositoryItemSearchLookUp(code.GetDeptList(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit Lup_AuthorGroup = rpo.Create_RepositoryItemSearchLookUp(code.GetAuthorGroupList(false), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "사업장", "CorpId", Lup_Corp);
            grid.Create_GvColumn(gv, "사용자ID", "EmployeeId");
            grid.Create_GvColumn(gv, "사용자명", "EmployeeName");
            grid.Create_GvColumn(gv, "비밀번호", "Password");
            grid.Create_GvColumn(gv, "부서", "DeptId", Lup_Dept);
            grid.Create_GvColumn(gv, "직급", "JobTitle");
            grid.Create_GvColumn(gv, "전화번호", "Phone");
            grid.Create_GvColumn(gv, "휴대폰", "CellPhone");
            grid.Create_GvColumn(gv, "권한그룹", "AuthorGroup", Lup_AuthorGroup);
            grid.Create_GvColumn(gv, "이메일", "Email");
            grid.Create_GvColumn(gv, "작업자유무", "WorkerYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "사용유무", "UseYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "자동로그인", "AutoLoginYn", rpo.Create_rpoChk());

            grid.Column_AllowEdit(gv.Columns["CorpId"]);
            grid.Column_AllowEdit(gv.Columns["EmployeeId"]);
            grid.Column_AllowEdit(gv.Columns["EmployeeName"]);
            grid.Column_AllowEdit(gv.Columns["Password"]);
            grid.Column_AllowEdit(gv.Columns["DeptId"]);
            grid.Column_AllowEdit(gv.Columns["JobTitle"]);
            grid.Column_AllowEdit(gv.Columns["Phone"]);
            grid.Column_AllowEdit(gv.Columns["CellPhone"]);
            grid.Column_AllowEdit(gv.Columns["AuthorGroup"]);
            grid.Column_AllowEdit(gv.Columns["Email"]);
            grid.Column_AllowEdit(gv.Columns["WorkerYn"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);
            grid.Column_AllowEdit(gv.Columns["AutoLoginYn"]);

            gv.Columns["CorpId"].Visible = false;
            gv.Columns["WorkerYn"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            gv.FocusedRowChanged += Gv_FocusedRowChanged;
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
            grid.Check_OverLap_CellValue(sender as GridView, e, "EmployeeId");
        }

        private void Gv_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            if (gv != null && gv.IsValidRowHandle(e.FocusedRowHandle))
            {
                // 선택된 행의 CorpId 및 EmployeeId를 가져옵니다.
                string corpId = gv.GetRowCellValue(e.FocusedRowHandle, "CorpId")?.ToString();
                empId = gv.GetRowCellValue(e.FocusedRowHandle, "EmployeeId")?.ToString();

                if (!string.IsNullOrEmpty(corpId) && !string.IsNullOrEmpty(empId))
                {
                    user.CorpId = corpId;
                    user.EmployeeId = empId;

                    byte[] imageBytes = user.SelectUserPic();

                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pictureEdit.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureEdit.Image = null;  // 이미지를 지웁니다.
                    }
                }
            }
        }

        #endregion GridView Setting End

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_RowEdit(cmdInsert, null); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): Btn_RowEdit(cmdDelete, null); break; //행삭제
            }
        }

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
                    dic.Add("CorpId", WrGlobal.CorpID);
                    dic.Add("UseYn", "Y");
                    dic.Add("WorkerYn", "N");
                    dic.Add("AutoLoginYn", "N");
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
            SM_User su = new SM_User();
            su.WorkerYn = "N";
            Gc_List.DataSource = su.Select_SM_User();

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
                    // 사용자 ID에 입력값이 없으면
                    if (dr["EmployeeId"].IsNullOrEmpty())
                    {
                        //프로세스 옵션 사용자 자동채번 Y
                        if (WrGlobal.ProcOption["SC"]["Emp_AutoNo"] == "Y")
                        {
                            string menuType = dr["WorkerYn"].ConvertString_Null_To_Empty() == "Y" ? "W" : "E";

                            SM_User cu = new SM_User();
                            dr["EmployeeId"] = cu.GetNewEmpId(WrGlobal.CorpID, menuType);
                        }
                        else
                        {
                            MessageBox.Show("ID를 입력해주세요.");
                            return false;
                        }
                    }

                    if (dr["WorkerYn"].ConvertString_Null_To_Empty() != "Y")//작업자는 비밀번호 필요없음
                    {
                        if (dr["Password"].IsNullOrEmpty())
                        {
                            MessageBox.Show("비밀번호를 입력해주세요.");
                            return false;
                        }
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
            SM_User su = new SM_User();
            su.CorpId = dr["CorpId"].ConvertString_Null_To_Empty();
            su.EmployeeId = dr["EmployeeId"].ConvertString_Null_To_Empty();
            su.EmployeeName = dr["EmployeeName"].ConvertString_Null_To_Empty();
            su.Password = dr["Password"].ConvertString_Null_To_Empty();
            su.DeptId = dr["DeptId"].ConvertString_Null_To_Empty();
            su.JobTitle = dr["JobTitle"].ConvertString_Null_To_Empty();
            su.Phone = dr["Phone"].ConvertString_Null_To_Empty();
            su.CellPhone = dr["CellPhone"].ConvertString_Null_To_Empty();
            su.AuthorGroup = dr["AuthorGroup"].ConvertString_Null_To_Empty();
            su.Email = dr["Email"].ConvertString_Null_To_Empty();
            su.WorkerYn = dr["WorkerYn"].ConvertString_Null_To_Empty();
            su.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();
            su.AutoLoginYn = dr["AutoLoginYn"].ConvertString_Null_To_Empty();

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    su.Password = clsCommon.SHA256Hash(su.Password); //비밀번호 암호화

                    //Insert
                    if (!su.Ins_SM_User())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    // 비밀번호 입력 안했으면 기존 유지
                    if (string.IsNullOrEmpty(su.Password))
                    {
                        su.Password = dr["Password2"].ToString();
                    }
                    else
                    {
                        // 새 비밀번호 입력 시만 암호화
                        su.Password = clsCommon.SHA256Hash(su.Password);
                    }

                    //Update
                    if (!su.Upd_SM_User())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!su.Del_SM_User())
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