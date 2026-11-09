using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.QM;
using SMT_MES.View.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_ItemExamMgnt : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력

        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();

        public Frm_QM_ItemExamMgnt()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //검사구분 데이터 입력

            Set_Exam_Div_Rdo(Rdo_Exam_Div);

            //품목구분 데이터 입력
            Set_Item_Div_Rdo(Rdo_Item_Div);

            //GridView 데이터 입력
            Set_ItemGridView(Gv_Item);
            Set_ItemExamGridView(Gv_ItemExam);

            ////검사방법 데이터 입력
            //Set_ExamMethod_Lup(Lup_ExamMethod);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_ItemExam, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_Exam_Div_Rdo(RadioGroup rdo)
        {
            //Item 생성
            clsRadioGroup clsRadio = new clsRadioGroup();

            if (Database.DbName.ToUpper().Contains("TPG") || Database.DbName.ToUpper().Contains("ALLTHEN"))
			{
                clsRadio.Create_RadioGroupItem(rdo, cm.SelectCateItem("Q005", new string[] {"10", "20", "30", "50"}), "명칭", "코드");
			}
			else
			{
                clsRadio.Create_RadioGroupItem(rdo, cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q005"), "분류명", "분류코드");
			}

            rdo.SelectedIndex = 0;

            rdo.SelectedIndexChanged += Rdo_Exam_Div_SelectedIndexChanged;
        }

        private void Set_Item_Div_Rdo(RadioGroup rdo)
        {
            //품목구분 RadioGroup 셋팅
            //Item 생성

            clsRadioGroup clsRadio = new clsRadioGroup();
            DataTable dtP = code.GetItemType(new string[] { "20", "30", "40", "50", "60" });

            if (Database.DbName.Contains("RNDZIPPER"))
            {
                dtP = code.GetItemType(new string[] { "20" });
            }

            clsRadio.Create_RadioGroupItem(rdo, dtP, "코드명", "코드");

            rdo.SelectedIndex = 0;
            rdo.SelectedIndexChanged += Rdo_Item_Div_SelectedIndexChanged;
        }

        #region GridView Setting

        private void Set_ItemGridView(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false; //DataTable 입력시 Column 생성 방지
            gv.OptionsFind.AlwaysVisible = true;
            gv.OptionsView.ShowGroupPanel = false;


            //Column 생성
            grid.Create_GvColumn(gv, "품목구분", "ItemTypeName");
            grid.Create_GvColumn(gv, "품목분류1", "ClassName1");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "규격", "Size");

            //Repository 설정
            //grid.Column_AllowEdit(gv.Columns["Unit"], false);

            grid.RowNumber_Indicator(gv);       //행순번 표기
            gv.BeforeLeaveRow += Gv_BeforeLeaveRow;
            gv.RowStyle += Event_ChangeBG_ByHasExamMgnt;    //검사데이터 여부에따른 색상 지정
			gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

		private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
		{
            Func_ExamSearch();
        }

		private void Gv_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (!Check_Search())
            {
                e.Allow = false; // 선택된 행 변경을 막습니다.
            }
        }

        private void Event_ChangeBG_ByHasExamMgnt(object sender, RowStyleEventArgs e)
        {
            //삭제시 배경색 지정
            GridView gv = sender as GridView;
            if (e.RowHandle == -1) return;

            if (Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ExistsExam")) == "삭제")
            {
                e.Appearance.BackColor = Color.Gray;
            }

            //검사 등록 내역 있는지 색 표기
            DataTable dt = gv.GridControl.DataSource as DataTable;
            if (dt == null) return;

            if (e.RowHandle > -1 && Convert.ToString(dt.Rows[e.RowHandle]["ExistsExam"]) == "N")
            {
                e.Appearance.BackColor = Color.Gray;
            }
            else
            {
                e.Appearance.BackColor = Color.Transparent;
            }
        }

        private void Set_ItemExamGridView(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false;


            QM_ExamMgnt Exam = new QM_ExamMgnt();

            RepositoryItem lup_Exam_Div = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q005"), "분류코드", "분류명");  //검사구분
            RepositoryItem lup_ValueType = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q006"), "분류코드", "분류명"); //검사값형태
            RepositoryItem lup_ExamCd = rpo.Create_RepositoryItemSearchLookUp(Exam.Select_Exam_Mgnt_Lup(Rdo_Exam_Div.EditValue.ConvertString_Null_To_Empty()), "코드", "명칭");
            RepositoryItem lup_Proc_Cd = rpo.Create_RepositoryItem(code.GetProcess_List(), "공정코드", "공정명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "표시순번", "DispSeq");
            grid.Create_GvColumn(gv, "사용여부", "UseYn");
            grid.Create_GvColumn(gv, "검사구분", "ExamDivCd");
            grid.Create_GvColumn(gv, "공정구분", "ProcessCode", lup_Proc_Cd);
            grid.Create_GvColumn(gv, "검사코드", "ExamCd");
            grid.Create_GvColumn(gv, "검사명", "ExamNm");
            grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");
            grid.Create_GvColumn(gv, "검사주기", "ExamCycle");
            grid.Create_GvColumn(gv, "값 형태", "ValueTypeDivCd");
            grid.Create_GvColumn(gv, "소수점 자리수", "DecimalCnt");
            grid.Create_GvColumn(gv, "기본값", "DefaultValue");
            grid.Create_GvColumn(gv, "관리범위 하한", "MgntMin");
            grid.Create_GvColumn(gv, "관리범위 상한", "MgntMax");
            grid.Create_GvColumn(gv, "위험범위 하한", "DangerMin");
            grid.Create_GvColumn(gv, "위험범위 상한", "DangerMax");


            //Column 단일 설정
            //Repository 설정
            gv.Columns["ExamNm"].ColumnEdit = lup_ExamCd;
            //검사항목 - X1 ~ X5 CheckEdit 설정
            rpoChk = rpo.Create_rpoChk();
            rpoTxt = rpo.Create_rpoTxt_Numeric();
            //사용여부
            RepositoryItemCheckEdit chk_Use = rpo.Create_rpoChk();
            gv.Columns["ExamDivCd"].ColumnEdit = lup_Exam_Div;
            gv.Columns["ValueTypeDivCd"].ColumnEdit = lup_ValueType;
            gv.Columns["UseYn"].ColumnEdit = chk_Use;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ExamNm"]);          //검사명
            grid.Column_AllowEdit(gv.Columns["ExamDivCd"]);      //검사구분
            grid.Column_AllowEdit(gv.Columns["ExamSpec"]);        //검사규격
            grid.Column_AllowEdit(gv.Columns["ExamMethod"]);      //검사방법
            grid.Column_AllowEdit(gv.Columns["ExamUnit"]);        //검사단위
            grid.Column_AllowEdit(gv.Columns["ExamCycle"]);       //검사주기
            grid.Column_AllowEdit(gv.Columns["ValueTypeDivCd"]); //값 형태
            grid.Column_AllowEdit(gv.Columns["DecimalCnt"]);      //소수점 자리수
            grid.Column_AllowEdit(gv.Columns["DefaultValue"]);    //기본값
            grid.Column_AllowEdit(gv.Columns["MgntMin"]);         //관리범위 하한
            grid.Column_AllowEdit(gv.Columns["MgntMax"]);         //관리범위 상한
            grid.Column_AllowEdit(gv.Columns["DangerMin"]);       //위험범위 하한
            grid.Column_AllowEdit(gv.Columns["DangerMax"]);       //위험범위 상한
            grid.Column_AllowEdit(gv.Columns["UseYn"]);           //사용여부

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            grid.SetCol_Type(gv.Columns["DecimalCnt"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["MgntMin"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["MgntMax"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["DangerMin"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["DangerMax"], clsGrid.TypeList.TFloat);

            gv.Columns["UseYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //사용여부

            //이벤트 설정
            //gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;        //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;                 //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;                  //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;          //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;                  //코드 입력시 중복 확인
            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //Row별 Repository 다르게하기

            if(WrGlobal.LoginID != "Dev")
            {
                gv.Columns["ExamCd"].Visible = false;
                gv.Columns["DangerMin"].Visible = false;
                gv.Columns["DangerMax"].Visible = false;
            }
            gv.Columns["ProcessCode"].Visible = false;
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            string value = e.Value.ConvertString_Null_To_Empty();

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            if(fieldName == "ExamNm")
            {
                QM_ExamMgnt Exam = new QM_ExamMgnt();
                Exam.ExamCd = value;

                DataRow drExam = Exam.Select_Exam_Info();
                if (drExam == null) return;

                drChanged["ExamCd"] = drChanged["ExamNm"];

                if (drChanged.Table.Columns.Contains("ExamSpec"))  { drChanged["ExamSpec"] = drExam["ExamSpec"]; }
                if (drChanged.Table.Columns.Contains("ExamMethod")) { drChanged["ExamMethod"] = drExam["ExamMethod"]; }
                if (drChanged.Table.Columns.Contains("ExamUnit")) { drChanged["ExamUnit"] = drExam["ExamUnit"]; }
                if (drChanged.Table.Columns.Contains("ExamCycle")) { drChanged["ExamCycle"] = drExam["ExamCycle"]; }
                if (drChanged.Table.Columns.Contains("ValueTypeDivCd")) { drChanged["ValueTypeDivCd"] = drExam["ValueTypeDivCd"]; }
                if (drChanged.Table.Columns.Contains("DecimalCnt")) { drChanged["DecimalCnt"] = drExam["DecimalCnt"]; }
                if (drChanged.Table.Columns.Contains("ProcessCode")) { drChanged["ProcessCode"] = drExam["ProcessCode"]; }
            }

            gv.BestFitColumns();
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_Row_Ins.PerformClick(); break;  //행삽입
                case (Keys.Alt | Keys.D): Btn_Row_Del.PerformClick(); ; break; //행삭제
                case (Keys.Alt | Keys.S): Func_Save(); ; break; //저장
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;

            string txt_Act = btn.Tag.ToString();

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
                dic.Add("Status", Convert.ToString(Gv_ItemExam.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_ItemExam, dic, txt_Act);
        }

        private void Btn_All_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string Item_Div_Cd = Convert.ToString(Rdo_Item_Div.EditValue);
                string ExamDivCd = Convert.ToString(Rdo_Exam_Div.EditValue);

                if (ExamDivCd == "ALL")
                {
                    MessageBox.Show("검사구분이 '전체'일시 일괄추가 불가");
                    return;
                }

                string ItemCode = Gv_Item.GetFocusedRowCellDisplayText("ItemCode").ConvertString_Null_To_Empty();
                string Item_Nm = Gv_Item.GetFocusedRowCellDisplayText("ItemName").ConvertString_Null_To_Empty();
                string Alias = Gv_Item.GetFocusedRowCellDisplayText("Size").ConvertString_Null_To_Empty();
                string Exam_Div_Nm = Rdo_Exam_Div.Properties.Items[Rdo_Exam_Div.SelectedIndex].Description.ToString();

                string MSG = $"검사 항목 미등록 품목들은 '검사유형정보'에 등록한 '{Exam_Div_Nm}'의 항목들을 일괄 등록 합니다. \n";
                      MSG += $"진행 하시겠습니까? \n";

                if (MessageBox.Show(MSG, "검사 항목 일괄추가", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                QM_ItemExamMgnt ItemExam = new QM_ItemExamMgnt();
                if (ItemExam.Insert_ItemExam_Batch(Item_Div_Cd, ExamDivCd))
                {
                    MessageBox.Show("저장 되었습니다.");
                    Func_ItemSearch();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //등록된 수입, 초중종, 출하 검사항목 일괄추가
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            DataTable dt = Gv_Item.GridControl.DataSource as DataTable;
            if (dt.Rows.Count <= 0) return;

            string ItemCode = Gv_Item.GetFocusedRowCellValue("ItemCode").ConvertString_Null_To_Empty();
            string ExamDiv = Rdo_Exam_Div.EditValue.ToString();

            if (ExamDiv == "ALL")
            {
                MessageBox.Show("검사구분이 '전체'일시 일괄추가 불가");
                return;
            }

            QM_ExamMgnt Exam = new QM_ExamMgnt();

            Exam.ExamDivCd = ExamDiv;
            DataTable dtExam = Exam.Add_Exam();

            int rowPos = 0;
            DataTable dtItemExam = Gc_ItemExam.DataSource as DataTable;


            foreach (DataRow dr in dtExam.Rows)
            {
                bool result = false;

                //이미 등록되어있는 코드는 제외하고 추가 처리
                for (int i = 0; i < Gv_ItemExam.RowCount; i++)
                {
                    if(Convert.ToString(Gv_ItemExam.GetRowCellValue(i, "ExamCd")) == Convert.ToString(dr["ExamCd"]))
                    {
                        result = true;
                        break;
                    }
                }

                if (result) continue;

                DataRow drInsert = dtItemExam.NewRow();
                drInsert.ItemArray = dr.ItemArray;
                drInsert["ItemCode"] = ItemCode;

                dtItemExam.Rows.InsertAt(drInsert, rowPos++);
            }

            Gv_ItemExam.BestFitColumns();
        }

        //선택한 로우 위, 아래로 이동
        private void Btn_RowMove(object sender, EventArgs e)
        {
            SimpleButton Btn = sender as SimpleButton;

            DataTable dt = Gv_ItemExam.GridControl.DataSource as DataTable;

            int idx = Gv_ItemExam.FocusedRowHandle;

            string BtnName = Btn.Name;

            if (BtnName.Contains("Up"))
            {
                if (idx == 0) return; // Top일 경우 종료
            }
            else
            {
                if (idx == dt.Rows.Count - 1) return; // Bottom일 경우 종료
            }

            DataRow curRow = dt.Rows[idx];
            curRow["Status"] = Convert.ToString(curRow["Status"]) == "신규" ? "신규" : "수정";

            DataRow newRow = dt.NewRow();
            newRow.ItemArray = curRow.ItemArray; // 내용 복사

            dt.Rows.RemoveAt(idx);

            if (BtnName.Contains("Up"))
            {
                dt.Rows.InsertAt(newRow, idx - 1);

                if (idx == dt.Rows.Count - 1)
                    Gv_ItemExam.FocusedRowHandle = Gv_ItemExam.FocusedRowHandle - 1;
                else
                    Gv_ItemExam.FocusedRowHandle = Gv_ItemExam.FocusedRowHandle - 2;
            }
            else
            {
                dt.Rows.InsertAt(newRow, idx + 1);

                Gv_ItemExam.FocusedRowHandle++;
            }

            Gv_ItemExam.SelectRow(Gv_ItemExam.FocusedRowHandle);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            GridView gv = sender as GridView;
            grid.Check_OverLap_CellValue(gv, e, "ExamCd");
            grid.Check_OverLap_CellValue(gv, e, "ExamNm");
            Check_SameExamNm(gv, e);
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

                    if (Convert.ToString(dr["ExamDivCd"]) == ExamDivCd
                        && Convert.ToString(dr["ExamNm"]) == Convert.ToString(e.Value))
                    {
                        e.Valid = false;    //입력 불가 처리
                        e.ErrorText = $"중복된 {gv.FocusedColumn.Caption}이(가) 있습니다.";  //경고 알림
                        break;
                    }
                }
            }
        }

        private void Event_GvCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            //검사항목 일정Row의 Repository다르게하기
            GridView gv = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string fieldName = e.Column.FieldName;
                if (fieldName == "DefaultValue")
                {
                    string type = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd"));

                    if (type == "10")
                    {
                        //양/불 선택일경우
                        e.RepositoryItem = rpoChk;
                    }
                    else if (type == "20")
                    {
                        //측정치 입력일경우
                        e.RepositoryItem = rpoTxt;
                    }
                }
            }
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_QM_ItemExamMgnt_Load(object sender, EventArgs e)
        {
            Func_ItemSearch();
        }

        #endregion Event End

        #region 조회 기능

        #region 품목 조회

        private void Rdo_Item_Div_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func_ItemSearch();
        }

        private void Func_ItemSearch()
        {
            //1. 조회 검사
            if (!Check_Search()) return;

            //2.품목 조회
            CM_Item item = new CM_Item();
            item.ItemType = Convert.ToString(Rdo_Item_Div.EditValue);
            string examType = Rdo_Exam_Div.EditValue.ConvertString_Null_To_Empty();

            Gc_Item.DataSource = item.Select_Item_HasExamMgnt(examType);
            Gv_Item.BestFitColumns();   //Column 너비 조정

            if (Gv_Item.DataRowCount > 0)
                Func_ExamSearch();
        }

        #endregion 품목 조회 End

        #region 품목 검사항목 조회

        private void Rdo_Exam_Div_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func_ExamSearch();

            //Exam Grid Repository 설정
            QM_ExamMgnt Exam = new QM_ExamMgnt();
            RepositoryItem lup_ExamCd = rpo.Create_RepositoryItemSearchLookUp(Exam.Select_Exam_Lup(Rdo_Exam_Div.EditValue.ToString()), "ExamCd", "ExamNm");
            Gv_ItemExam.Columns["ExamNm"].ColumnEdit = lup_ExamCd;
        }

        private void Func_ExamSearch()
        {
            //1.조회 검사
            if (!Check_Search()) return;
            if (Gv_Item.RowCount < 1) return;

            string ItemCode = Gv_Item.GetFocusedRowCellValue("ItemCode").ConvertString_Null_To_Empty();
            string ExamDiv = Rdo_Exam_Div.EditValue.ConvertString_Null_To_Empty();


            //2.품목별검사항목 조회
            QM_ItemExamMgnt ItemExam = new QM_ItemExamMgnt();

            Gc_ItemExam.DataSource = ItemExam.Select_ItemExam(ItemCode, ExamDiv, "");
            Gv_ItemExam.BestFitColumns();   //Column 너비 조정
        }

        private bool Check_Search()
        {
            if((Gv_ItemExam.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            //작성내역 저장
            DataTable dtItem = Gc_ItemExam.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtItem)) return;

            int DispSeq = 0;
            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dtItem.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "신규")
                {
                    ++DispSeq;
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'I', DispSeq)) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "수정")
                {
                    ++DispSeq;
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U', DispSeq)) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'D', DispSeq)) return;
                }
                else //수정, 미작업 로우
				{
                    ++DispSeq;
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U', DispSeq)) return;
                }
            }

            //3. 정상절차 진행
            Func_ExamSearch();

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

        private bool Save_ToDatabase(DataRow dr, char div, int DispSeq)
        {

            //데이터베이스에 반영
            //1. 기본 셋팅
            QM_ItemExamMgnt ItemExam = new QM_ItemExamMgnt();

            ItemExam.ItemCode          = Gv_Item.GetFocusedDataRow()["ItemCode"].ConvertString_Null_To_Empty();
            ItemExam.ExamDivCd      = dr["ExamDivCd"].ConvertString_Null_To_Empty();
            ItemExam.ExamCd          = dr["ExamCd"].ConvertString_Null_To_Empty();
            ItemExam.DispSeq         = DispSeq;
            ItemExam.ExamSpec        = dr["ExamSpec"].ConvertString_Null_To_Empty();
            ItemExam.ExamMethod      = dr["ExamMethod"].ConvertString_Null_To_Empty();
            ItemExam.ExamUnit        = dr["ExamUnit"].ConvertString_Null_To_Empty();
            ItemExam.ExamCycle       = dr["ExamCycle"].ConvertString_Null_To_Empty();
            ItemExam.ValueTypeDivCd = dr["ValueTypeDivCd"].ConvertString_Null_To_Empty();
            ItemExam.DecimalCnt      = dr["DecimalCnt"].ConvertInt_Null_To_Zero();
            ItemExam.DefaultValue    = dr["DefaultValue"].ConvertString_Null_To_Empty();
            ItemExam.MgntMin         = dr["MgntMin"].ConvertFloat_Null_To_Zero();
            ItemExam.MgntMax         = dr["MgntMax"].ConvertFloat_Null_To_Zero();
            ItemExam.DangerMin       = dr["DangerMin"].ConvertFloat_Null_To_Zero();
            ItemExam.DangerMax       = dr["DangerMax"].ConvertFloat_Null_To_Zero();
            ItemExam.UseYn           = dr["UseYn"].ConvertString_Null_To_Empty();

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!ItemExam.Ins_ItemExam())
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
                if (!ItemExam.Upd_ItemExam())
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
                if (!ItemExam.Del_ItemExam())
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

        private void BtnExam_Q_Click(object sender, EventArgs e)
        {
            Frm_QM_ItemExamMgnt_Quantity frm = new Frm_QM_ItemExamMgnt_Quantity();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.WindowState = FormWindowState.Maximized;
            frm.FormClosed += FrmPopup_FormClosed;
            frm.ShowDialog();
        }

        private void FrmPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            //1. 선택한 row 저장
            int FocusedRowInxItem = (Gc_Item.MainView as GridView).FocusedRowHandle;
            int FocusedRowInxItemExam = (Gc_ItemExam.MainView as GridView).FocusedRowHandle;

            //2. 재조회
            Func_ExamSearch();

            //3. 저장 후 선택한 row를 불러옴
            (Gc_Item.MainView as GridView).FocusedRowHandle = FocusedRowInxItem;
            (Gc_ItemExam.MainView as GridView).FocusedRowHandle = FocusedRowInxItemExam;
        }

        private void BtnCopyItem_Click(object sender, EventArgs e)
		{
			if ((Gv_ItemExam.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
                                                                    OR Status = '수정'
                                                                    OR Status = '삭제'").Length > 0)
			{
				XtraMessageBox.Show("변경된 내역이 있습니다. 저장 후 진행할 수 있습니다.");
				return;
			}

			DataRow dr = Gv_Item.GetFocusedDataRow();
			if (dr == null) return;

			string item_div_Cd = Convert.ToString(Rdo_Item_Div.EditValue);
			string ItemCode = Convert.ToString(dr["ItemCode"]);
			string ExamDivCd = Convert.ToString(Rdo_Exam_Div.EditValue);
			DataTable dt = Gc_ItemExam.DataSource as DataTable;

			Frm_QM_ItemExamMgnt_ItemCopy frm = new Frm_QM_ItemExamMgnt_ItemCopy(item_div_Cd, ExamDivCd, ItemCode);
			frm.StartPosition = FormStartPosition.CenterParent;
			frm.WindowState = FormWindowState.Maximized;
			frm.ShowDialog();
		}


		private void cmdClose_Click(object sender,EventArgs e)
		{
            this.Close();
		}

        private void BtnItemImg_Click(object sender, EventArgs e)
        {
            //검사기준서 등록
            Frm_CM_ItemImage frm = new Frm_CM_ItemImage();
            frm.ShowDialog();
        }

		private void BtnDelete_All_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < Gv_ItemExam.RowCount; i++)
			{
                Gv_ItemExam.SetRowCellValue(i, "Status",  "삭제");
			}
		}

		private void cmdDisplay_Click(object sender, EventArgs e)
		{
            Func_ItemSearch();

        }
	}
}