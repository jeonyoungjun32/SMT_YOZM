using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models.CM;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_ExamMgnt : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();
        string[] productType = { "10", "20", "30", "40", "50", "60", "70", "80", "90" }; // 제품구분 LUP

        public Frm_QM_ExamMgnt()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 데이터 입력
            Set_GridView(Gv_Exam);

            //검사구분 데이터 입력
            Set_Exam_Rdo(Rdo_Exam_Div);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Exam, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_Exam_Rdo(RadioGroup rdo)
        {
            //검사구분 RadioGroup 셋팅s
            clsRadioGroup clsRadio = new clsRadioGroup();
            clsRadio.Create_RadioGroupItem(rdo, cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q005"), "분류명", "분류코드");

            rdo.SelectedIndex = 0;  //초기값
        }

        #region GridView Setting

        private void Set_GridView(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItem lup_Exam_Div = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q005"), "분류코드", "분류명");  //검사구분
            RepositoryItem lup_ValueType = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q006"), "분류코드", "분류명"); //검사값형태
            RepositoryItem lup_Proc_Div = rpo.Create_RepositoryItem(code.GetProcess_List(), "공정코드", "공정명");
            RepositoryItemSearchLookUpEdit lup_ItemType = rpo.Create_RepositoryItemSearchLookUp(code.GetItemType(productType), "코드", "코드명");

            //사용여부
            RepositoryItemCheckEdit chk_Use = new RepositoryItemCheckEdit();
            chk_Use.ValueChecked = "Y";
            chk_Use.ValueUnchecked = "N";
            chk_Use.NullStyle = StyleIndeterminate.Unchecked;
            chk_Use.AllowGrayed = false;

            //Column 생성
            grid.Create_GvColumn(gv, "상태",          "Status");
            grid.Create_GvColumn(gv, "공정구분",      "ProcessCode", lup_Proc_Div);
            grid.Create_GvColumn(gv, "검사구분",      "ExamDivCd", lup_Exam_Div);
            grid.Create_GvColumn(gv, "검사코드",      "ExamCd");
            grid.Create_GvColumn(gv, "검사명",        "ExamNm");
            grid.Create_GvColumn(gv, "검사규격",      "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법",      "ExamMethod");
            grid.Create_GvColumn(gv, "검사단위",      "ExamUnit");
            grid.Create_GvColumn(gv, "검사주기",      "ExamCycle");
            grid.Create_GvColumn(gv, "값 형태", "ValueTypeDivCd", lup_ValueType);
            grid.Create_GvColumn(gv, "소수점 자리수", "DecimalCnt");

            if (Database.DbName.ToUpper().Contains("TPG"))
            {
                grid.Create_GvColumn(gv, "품목구분", "ItemType", lup_ItemType);
                grid.Column_AllowEdit(gv.Columns["ItemType"]);
            }

            grid.Create_GvColumn(gv, "사용여부",      "UseYn", chk_Use);

            //Column 단일 설정
            //셀값 변경 허용
            //자동채번이 아닐 경우 코드값 편집 허용

            if (WrGlobal.ProcOption["BS"]["Quality_AutoNo"] != "Y")
            {
                grid.Column_AllowEdit(gv.Columns["ExamCd"]);
            }

            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);       //공정구분
            grid.Column_AllowEdit(gv.Columns["ExamNm"]);          //검사명
            grid.Column_AllowEdit(gv.Columns["ExamDivCd"]);      //검사구분
            grid.Column_AllowEdit(gv.Columns["ExamSpec"]);        //검사규격
            grid.Column_AllowEdit(gv.Columns["ExamMethod"]);      //검사방법
            grid.Column_AllowEdit(gv.Columns["ExamUnit"]);        //검사단위
            grid.Column_AllowEdit(gv.Columns["ExamCycle"]);       //검사주기
            grid.Column_AllowEdit(gv.Columns["ValueTypeDivCd"]); //값 형태
            grid.Column_AllowEdit(gv.Columns["DecimalCnt"]);      //소수점 자리수
            grid.Column_AllowEdit(gv.Columns["UseYn"]);           //사용여부

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
            gv.Columns["DecimalCnt"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;   //소수점 자리수
            gv.Columns["UseYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //사용여부

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인

            //검사구분 라디오 버튼 변경시 재조회
            Rdo_Exam_Div.SelectedIndexChanged += new EventHandler(Rdo_Exam_Div_SelectedIndexChanged);

            //코드숨김
            if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["ExamCd"].Visible = false;
            }
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
        }

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            GridView gv = sender as GridView;
            grid.Check_OverLap_CellValue(gv, e, "ExamCd");
            //Check_SameExamNm(gv, e);
        }

        private void Check_SameExamNm(GridView gv, BaseContainerValidateEditorEventArgs e)
        {
            string ColName = gv.FocusedColumn.FieldName;

            if (ColName == "ExamNm")
            {
                string ExamDivCd = Convert.ToString(gv.GetFocusedRowCellValue("ExamDivCd"));

                if (ExamDivCd == "ALL")
                {
                    e.Valid = false;    //입력 불가 처리
                    e.ErrorText = $"검사구분 먼저 선택이 필요합니다.";  //경고 알림

                    return;
                }

                for (int idx = 0; idx < gv.RowCount; idx++)
                {
                    DataRow dr = gv.GetDataRow(idx);

                    if(Convert.ToString(dr["ExamDivCd"]) == ExamDivCd
                        && Convert.ToString(dr["ExamNm"]) == Convert.ToString(e.Value))
                    {
                        e.Valid = false;    //입력 불가 처리
                        e.ErrorText = $"중복된 {gv.FocusedColumn.Caption}이(가) 있습니다.";  //경고 알림
                        break;
                    }
                }
            }
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_QM_Exam_Mgnt_Load(object sender, EventArgs e)
        {
            Func_Search();
        }

        #endregion Event End

        #region 조회 기능

        private void Rdo_Exam_Div_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func_Search();

        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Func_Search()
        {
            //1. 조회 검사
            if (!Check_Search()) return;

            //2. 검사항목 조회
            QM_ExamMgnt Mgnt = new QM_ExamMgnt();

            Gc_Exam.DataSource = Mgnt.Select_Exam(Convert.ToString(Rdo_Exam_Div.EditValue), true) ;

            Gv_Exam.BestFitColumns();   //Column 너비 조정
        }

        private bool Check_Search()
        {
            if((Gv_Exam.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
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

        #endregion 조회버튼 클릭 End

        #region 저장 기능

        private void BtnSave_Click(object sender, EventArgs e)
        {
        }

        private void Func_Save()
        {
            //작성내역 저장
            DataTable dtItem = Gc_Exam.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtItem)) return;

            Gv_Exam.FocusInvalidRow();

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

            //3. 정상절차 진행
            Func_Search();

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
                    if (Convert.ToString(dr["ExamDivCd"]) == "")
                    {
                        MessageBox.Show("검사구분 항목이 비어있습니다.");
                        return false;
                    }
                    if (Convert.ToString(dr["ExamNm"]) == "")
                    {
                        MessageBox.Show("검사명 항목이 비어있습니다.");
                        return false;
                    }
                    if (Convert.ToString(dr["ValueTypeDivCd"]) == "")
                    {
                        MessageBox.Show("값형태 항목이 비어있습니다.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정") { checkCnt++; }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {
                    isDelete = true;
                    checkCnt++;
                }
            }
            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?\n검사유형정보 삭제 시 품목별 검사유형정보의 검사유형정보도 삭제 됩니다.", "Delete OK Cancel", MessageBoxButtons.OKCancel);
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
            QM_ExamMgnt Exam = new QM_ExamMgnt();
            try
            {
                Exam.ExamCd = Convert.ToString(dr["ExamCd"]);      //품목코드

                if (div != 'D')
                {

                    //Delete제외 공통

                    Exam.ExamDivCd = Convert.ToString(dr["ExamDivCd"]);
                    Exam.ExamNm = Convert.ToString(dr["ExamNm"]);
                    Exam.ExamSpec = Convert.ToString(dr["ExamSpec"]);
                    Exam.ExamMethod = Convert.ToString(dr["ExamMethod"]);
                    Exam.ExamUnit = Convert.ToString(dr["ExamUnit"]);
                    Exam.ExamCycle = Convert.ToString(dr["ExamCycle"]);
                    Exam.ValueTypeDivCd = Convert.ToString(dr["ValueTypeDivCd"]);
                    Exam.DecimalCnt = dr["DecimalCnt"].ConvertInt_Null_To_Zero();
                    Exam.UseYn = Convert.ToString(dr["UseYn"]);
                    Exam.ProcessCode = Convert.ToString(dr["ProcessCode"]);
                    Exam.ItemType = Convert.ToString(dr["ItemType"]);
                }
            }
            catch
            {
                MessageBox.Show("값 형태변환 에러");
                return false;
            }

            //2. DB 적용
            if(div == 'I')
            {
                //Insert
                if (!Exam.Ins_Exam())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else if(div == 'U')
            {
                //Update
                if (!Exam.Upd_Exam())
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
                if (!Exam.Del_Exam())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //품목별 검사유형정보에 있는 데이터도 삭제
                QM_ItemExamMgnt bim = new QM_ItemExamMgnt();
                bim.ExamDivCd = Convert.ToString(dr["ExamDivCd"]);
                bim.ExamCd = Exam.ExamCd;
                bim.Del_ItemExam_ExamCd();

                //추가 변경사항 private 따로 만들기
            }

            //상태값 초기화
            dr["Status"] = "";


            return true;

        }


		#endregion 저장 기능 End

		private void cmdSave_Click(object sender, EventArgs e)
		{
            Gv_Exam.PostEditor();
            Gv_Exam.UpdateCurrentRow();
            Func_Save();
        }

        private void cmdRowEdit_Click(object sender, EventArgs e)
		{
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            smtSearchButton btn = sender as smtSearchButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                //dic.Add("Item_Div_Cd", Convert.ToString(Rdo_Exam_Div.EditValue) == "ALL" ? "" : Convert.ToString(Rdo_Exam_Div.EditValue));
                //dic.Add("ItemClass_Nm", Convert.ToString(Lup_ItemClass.EditValue) == "ALL" ? "" : Convert.ToString(Lup_ItemClass.EditValue));
                //dic.Add("BOM_Div_Cd", "40");
                dic.Add("ExamDivCd", Rdo_Exam_Div.EditValue);
                dic.Add("UseYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_Exam.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_Exam, dic, txt_Act);
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}