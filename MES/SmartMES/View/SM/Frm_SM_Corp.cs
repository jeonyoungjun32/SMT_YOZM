using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using System;
using System.Windows.Forms;
using SmartCommon.Common;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using SmartMES.Models.SM;
using System.Data;
using baseSMT;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_Corp : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        public Frm_SM_Corp()
        {
            InitializeComponent();
        }

        private void Frm_SM_Corp_Load(object sender, EventArgs e)
        {
            SetGrid(Gv_List);
            Func_Search();
        }

        private void SetGrid(GridView gv)
		{
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsView.ShowGroupPanel = false;

            //Repository
            RepositoryItemPictureEdit pic = rpo.Create_rpoPic();
            RepositoryItemPictureEdit picLogo = rpo.Create_rpoPic();

            //Create
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "주사업자여부", "MasterYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "사업장코드", "CorpId");
            grid.Create_GvColumn(gv, "사업장명", "CorpName");
            grid.Create_GvColumn(gv, "사업자등록번호", "CorpNo");
            grid.Create_GvColumn(gv, "법인등록번호", "CorpRegNo");
            grid.Create_GvColumn(gv, "대표자명", "PresiName");
            grid.Create_GvColumn(gv, "우편번호", "PostNo");
            grid.Create_GvColumn(gv, "사업장주소", "Address");
            grid.Create_GvColumn(gv, "사업장번지", "AddressSub");
            grid.Create_GvColumn(gv, "전화번호", "Tel");
            grid.Create_GvColumn(gv, "팩스", "Fax");
            grid.Create_GvColumn(gv, "업태", "BussinessCategory");
            grid.Create_GvColumn(gv, "종목", "BussinessField");
            grid.Create_GvColumn(gv, "개업일자", "OpenDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "도장이미지", "Pic", pic);
            grid.Create_GvColumn(gv, "로고이미지", "LogoImg", picLogo);

            //Visible

            //AllowEdit
            grid.Column_AllowEdit(gv.Columns["MasterYn"]);
            grid.Column_AllowEdit(gv.Columns["CorpId"]);
            grid.Column_AllowEdit(gv.Columns["CorpName"]);
            grid.Column_AllowEdit(gv.Columns["CorpNo"]);
            grid.Column_AllowEdit(gv.Columns["CorpRegNo"]);
            grid.Column_AllowEdit(gv.Columns["PresiName"]);
            grid.Column_AllowEdit(gv.Columns["PostNo"]);
            grid.Column_AllowEdit(gv.Columns["Address"]);
            grid.Column_AllowEdit(gv.Columns["AddressSub"]);
            grid.Column_AllowEdit(gv.Columns["Tel"]);
            grid.Column_AllowEdit(gv.Columns["Fax"]);
            grid.Column_AllowEdit(gv.Columns["Fax"]);
            grid.Column_AllowEdit(gv.Columns["BussinessCategory"]);
            grid.Column_AllowEdit(gv.Columns["BussinessField"]);
            grid.Column_AllowEdit(gv.Columns["OpenDate"]);
            grid.Column_AllowEdit(gv.Columns["Pic"]);
            grid.Column_AllowEdit(gv.Columns["LogoImg"]);

            //Format

            //이벤트 설정
            grid.RowNumber_Indicator(gv); //행번호 표기
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
            grid.Check_OverLap_CellValue(sender as GridView, e, "CorpId");
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
            SM_Corp sc = new SM_Corp();
            Gc_List.DataSource = sc.Select_SM_Corp();

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
                    if (dr["CorpId"].IsNullOrEmpty())
                    {
                        MessageBox.Show("사업장코드가 비어있습니다.");
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
            SM_Corp sc = new SM_Corp();
            sc.CorpId = dr["CorpId"].ConvertString_Null_To_Empty();
            sc.CorpName = dr["CorpName"].ConvertString_Null_To_Empty();
            sc.CorpNo = dr["CorpNo"].ConvertString_Null_To_Empty();
            sc.CorpRegNo = dr["CorpRegNo"].ConvertString_Null_To_Empty();
            sc.PresiName = dr["PresiName"].ConvertString_Null_To_Empty();
            sc.BussinessCategory = dr["BussinessCategory"].ConvertString_Null_To_Empty();
            sc.BussinessField = dr["BussinessField"].ConvertString_Null_To_Empty();
            sc.Tel = dr["Tel"].ConvertString_Null_To_Empty();
            sc.Fax = dr["Fax"].ConvertString_Null_To_Empty();
            sc.PostNo = dr["PostNo"].ConvertString_Null_To_Empty();
            sc.Address = dr["Address"].ConvertString_Null_To_Empty();
            sc.AddressSub = dr["AddressSub"].ConvertString_Null_To_Empty();
            sc.TaxCode = dr["TaxCode"].ConvertString_Null_To_Empty();
            sc.ContactPoint = dr["ContactPoint"].ConvertString_Null_To_Empty();
            sc.ContactEmail = dr["ContactEmail"].ConvertString_Null_To_Empty();
            sc.MasterYn = dr["MasterYn"].ConvertString_Null_To_Empty() == "" ? "N" : dr["MasterYn"].ConvertString_Null_To_Empty();
            sc.OpenDate = dr["OpenDate"] == DBNull.Value? (DateTime?)null: Convert.ToDateTime(dr["OpenDate"]);
            sc.Pic = dr["Pic"] as byte[];
            sc.LogoImg = dr["LogoImg"] as byte[];

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    //Insert
                    if (!sc.Ins_SM_Corp())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    //Update
                    if (!sc.Upd_SM_Corp())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!sc.Del_SM_Corp())
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
