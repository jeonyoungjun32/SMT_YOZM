using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Data;
using SmartCommon.Common;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Collections.Generic;
using baseSMT;
using DevExpress.XtraEditors.Repository;
using SmartMES.Models.CM;
using SmartMES.Models.QM;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_NCDisposition : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        Database db = new Database();
        CM_CateItem cm = new CM_CateItem();
        string query = "";

        public Frm_QM_NCDisposition()
        {
            InitializeComponent();
        }

        private void Frm_CM_DefectCode_Load(object sender, EventArgs e)
        {
            SetDesign();
            Search_NCR();
            Func_Search();
        }

        private void SetDesign()
        {
            SetLup();
            Set_GvNCR(Gv_NCR);
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

        private void SetLup()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            DataTable dt = cm.SelectClassItemLoopUp_WithALL(WrGlobal.CorpID, "A040", new string[] { }, true);
            LupType.smtLookUpDataSource = dt;
            LupType.smtLookUpDataDisplayMember = "분류명";
            LupType.smtLookUpDataValueMember = "분류코드";
            LupType.smtLookUpValue = "ALL";
        }

        #region GridView Setting

        private void Set_GvNCR(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItem lup_ExamDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "A040"), "분류코드", "분류명");  //검사구분
            RepositoryItemSearchLookUpEdit lupProcess = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(null), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");

            //Column 생성
            grid.Create_GvColumn(gv, "부적합번호", "NCRNo");
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "발생일자", "NCRDate");
            grid.Create_GvColumn(gv, "검사구분", "NCRType", lup_ExamDiv);
            grid.Create_GvColumn(gv, "검사번호", "InspectionNo");
            grid.Create_GvColumn(gv, "품목", "ItemCode", lup_Item);
            grid.Create_GvColumn(gv, "LOT", "LotNo");
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lupProcess);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "불량코드", "DefectCode");
            grid.Create_GvColumn(gv, "불량수량", "DefectQty");
            grid.Create_GvColumn(gv, "중요도", "Severity");
            //grid.Create_GvColumn(gv, "불량위치", "DefectPosition");
            //grid.Create_GvColumn(gv, "불량현상", "DefectDescription");
            //grid.Create_GvColumn(gv, "사진", "ImageFileID");
            //grid.Create_GvColumn(gv, "사용유무", "UseYn", rpo.Create_rpoChk());;

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["NCRDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["DefectQty"], clsGrid.TypeList.TFloat);
        }

        private void Set_GridView(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItem lup_Type = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q060"), "분류코드", "분류명");        //재작업구분
            RepositoryItem lup_Status = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q070"), "분류코드", "분류명");      //승인상태

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "처분번호", "DispositionNo");
            grid.Create_GvColumn(gv, "부적합번호", "NCRNo");
            grid.Create_GvColumn(gv, "구분", "DispositionType", lup_Type);
            grid.Create_GvColumn(gv, "처분수량", "DispositionQty");
            grid.Create_GvColumn(gv, "승인결재 ", "ApprovalNo");
            grid.Create_GvColumn(gv, "승인상태 ", "ApprovalStatus", lup_Status);
            grid.Create_GvColumn(gv, "사용유무", "UseYn", rpo.Create_rpoChk());

            // Column 수정 허용
            grid.Column_AllowEdit(gv.Columns["DispositionType"]);
            grid.Column_AllowEdit(gv.Columns["DispositionQty"]);
            grid.Column_AllowEdit(gv.Columns["ApprovalNo"]);
            grid.Column_AllowEdit(gv.Columns["ApprovalStatus"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            //VISIBLE
            gv.Columns["NCRNo"].Visible = false;

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["DispositionQty"], clsGrid.TypeList.TInt);

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
            gv.Columns["Status"].VisibleIndex = 0;

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
            grid.Check_OverLap_CellValue(sender as GridView, e, "ActionSeq");
        }

        #endregion GridView Setting End

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): cmdIns.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): cmdDel.PerformClick(); ; break; //행삭제
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

                    string ncrNo = Convert.ToString(Gv_NCR.GetFocusedRowCellValue("NCRNo"));
                    dic.Add("NCRNo", ncrNo);
                    dic.Add("UseYn", "Y");
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
            Search_NCR();
            Func_Search();
        }

        private void Search_NCR()
        {
            // 데이터 조회
            QM_NonConformanceM nc = new QM_NonConformanceM();
            nc.StartDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            nc.EndDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            nc.NCRType = LupType.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_NCR.DataSource = nc.Select_QM_NonConformanceM();
            Gv_NCR.BestFitColumns();
        }

        private void Func_Search()
        {
            // 데이터 조회
            QM_NCDisposition nc = new QM_NCDisposition();

            nc.StartDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            nc.EndDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            nc.NCRNo = Gv_NCR.GetFocusedRowCellValue("NCRNo").ConvertString_Null_To_Empty();

            Gc_List.DataSource = nc.Select_QM_NCDisposition();
            Gv_List.Columns["Status"].VisibleIndex = 0;
            Gv_List.BestFitColumns();
        }
        #endregion 조회 기능 End

        #region 저장 기능

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Search_NCR();
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
            Search_NCR();
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
                    if (dr["DispositionType"].IsNullOrEmpty())
                    {
                        MessageBox.Show("재작업구분이 비어있습니다.");
                        return false;
                    }
                    //if (dr["DefectCode"].IsNullOrEmpty())
                    //{
                    //    MessageBox.Show("불량코드가 비어있습니다.");
                    //    return false;
                    //}
                    //if (dr["DefectDescription"].IsNullOrEmpty())
                    //{
                    //    MessageBox.Show("불량현상이 비어있습니다.");
                    //    return false;
                    //}
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
            // 데이터베이스에 반영
            // 1. 기본 셋팅
            QM_NCDisposition nc = new QM_NCDisposition();
            nc.DispositionNo = dr["DispositionNo"].ConvertString_Null_To_Empty();
            nc.NCRNo = dr["NCRNo"].ConvertString_Null_To_Empty();
            nc.DispositionType = dr["DispositionType"].ConvertString_Null_To_Empty();
            nc.DispositionQty = dr["DispositionQty"].ConvertDecimal_Null_To_Zero();
            nc.ApprovalNo = dr["ApprovalNo"].ConvertString_Null_To_Empty();
            nc.ApprovalStatus = dr["ApprovalStatus"].ConvertString_Null_To_Empty();
            nc.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

            // 2. DB 적용
            try
            {
                if (div == 'I')
                {
                    //처분번호 생성
                    nc.DispositionNo = "DP" + code.GetAutoNumber("처분번호", DateTime.Now);

                    // Insert
                    if (!nc.Ins_QM_NCDisposition())
                    {
                        // 저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    // Update
                    if (!nc.Upd_QM_NCDisposition())
                    {
                        // 저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    // Delete
                    if (!nc.Del_QM_NCDisposition())
                    {
                        // 저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }

                // 상태값 초기화
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

        #region Event

        private void Gv_NCR_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            Func_Search();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_List);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}