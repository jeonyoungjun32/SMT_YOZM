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
    public partial class Frm_QM_NonConformance : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        Database db = new Database();
        CM_CateItem cm = new CM_CateItem();

        public Frm_QM_NonConformance()
        {
            InitializeComponent();
        }

        private void Frm_CM_DefectCode_Load(object sender, EventArgs e)
        {
            SetDesign();
            Func_Search();
        }

        private void SetDesign()
        {
            SetLup();
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

        private void Set_GridView(GridView gv)
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
            grid.Create_GvColumn(gv, "상태", "Status");
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
            grid.Create_GvColumn(gv, "불량위치", "DefectPosition");
            grid.Create_GvColumn(gv, "불량현상", "DefectDescription");
            grid.Create_GvColumn(gv, "사진", "ImageFileID");
            //grid.Create_GvColumn(gv, "사용유무", "UseYn", rpo.Create_rpoChk());;

            grid.Column_AllowEdit(gv.Columns["NCRDate"]);
            grid.Column_AllowEdit(gv.Columns["NCRType"]);
            grid.Column_AllowEdit(gv.Columns["InspectionNo"]);
            grid.Column_AllowEdit(gv.Columns["ItemCode"]);
            grid.Column_AllowEdit(gv.Columns["LotNo"]);
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["MachineCode"]);
            grid.Column_AllowEdit(gv.Columns["DefectCode"]);
            grid.Column_AllowEdit(gv.Columns["DefectQty"]);
            grid.Column_AllowEdit(gv.Columns["Severity"]);
            grid.Column_AllowEdit(gv.Columns["DefectPosition"]);
            grid.Column_AllowEdit(gv.Columns["DefectDescription"]);
            grid.Column_AllowEdit(gv.Columns["ImageFileID"]);

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["NCRDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["DefectQty"], clsGrid.TypeList.TFloat);

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
            grid.Check_OverLap_CellValue(sender as GridView, e, "FaultCode");
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
                    dic.Add("NCRDate", DateTime.Now.ToString("yyyy-MM-dd"));
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
            QM_NonConformanceM nc = new QM_NonConformanceM();
            nc.StartDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            nc.EndDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            nc.NCRType = LupType.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_List.DataSource = nc.Select_QM_NonConformanceM();
            Gv_List.BestFitColumns();   
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
                    if (dr["NCRType"].IsNullOrEmpty())
                    {
                        MessageBox.Show("검사구분이 비어있습니다.");
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
            //데이터베이스에 반영
            //1. 기본 셋팅
            QM_NonConformanceM ncr = new QM_NonConformanceM();
            ncr.NCRNo = dr["NCRNo"].ConvertString_Null_To_Empty();
            ncr.Seq = 1;
            ncr.NCRDate = dr["NCRDate"].ConvertDate_To_Char10();
            ncr.NCRType = dr["NCRType"].ConvertString_Null_To_Empty();
            ncr.InspectionNo = dr["InspectionNo"].ConvertString_Null_To_Empty();
            ncr.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            ncr.LotNo = dr["LotNo"].ConvertString_Null_To_Empty();
            ncr.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
            ncr.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();
            ncr.DefectCode = dr["DefectCode"].ConvertString_Null_To_Empty();
            ncr.DefectQty = dr["DefectQty"].ConvertDecimal_Null_To_Zero();
            ncr.Severity = dr["Severity"].ConvertString_Null_To_Empty();
            ncr.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();
            ncr.DefectPosition = dr["DefectPosition"].ConvertString_Null_To_Empty();
            ncr.DefectDescription = dr["DefectDescription"].ConvertString_Null_To_Empty();
            ncr.ImageFileID = dr["ImageFileID"].ConvertString_Null_To_Empty();

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    //부적합번호 생성
                    ncr.NCRNo = "NC" + code.GetAutoNumber("부적합번호", Convert.ToDateTime(ncr.NCRDate));

                    //Insert
                    if (!ncr.Ins_QM_NonConformanceM())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    //Update
                    if (!ncr.Upd_QM_NonConformanceM())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!ncr.Del_QM_NonConformanceM())
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

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_List);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}