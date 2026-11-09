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
    public partial class Frm_QM_HACCPStd : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsRadioGroup clsRadio = new clsRadioGroup();

        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxtNum = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력

        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();

        string CCPDiv = "";
        string TargetDiv = "";

        public Frm_QM_HACCPStd()
        {
            InitializeComponent();
        }

        public Frm_QM_HACCPStd(string ccpDiv, string targetDiv)
        {
            //특정 관리 기준일때 사용
            InitializeComponent();

            CCPDiv = ccpDiv;
            TargetDiv = targetDiv;
        }


        private void Frm_QM_ItemExamMgnt_Load(object sender, EventArgs e)
        {
            SetDesign();

            Display_Std();
        }


        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //검사구분 데이터 입력
            Set_Rdo();

            //GridView 데이터 입력
            //Set_ItemGridView(Gv_ItemClass);
            Set_ItemExamGridView(Gv_Std);

            ////검사방법 데이터 입력
            //Set_ExamMethod_Lup(Lup_ExamMethod);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Std, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_Rdo()
        {
            //CCP 구분

            if(CCPDiv == "")
			{
                clsRadio.Create_RadioGroupItem(Rdo_CCP_Div, code.GetCategory_HighClass("F010"), "명칭", "코드");
                Rdo_CCP_Div.SelectedIndex = 0;
			}
			else
			{
                clsRadio.Create_RadioGroupItem(Rdo_CCP_Div, code.GetCategory_HighClass("F010", new string[] { CCPDiv }), "명칭", "코드");
                Rdo_CCP_Div.EditValue = CCPDiv;
			}

            Rdo_CCP_Div.SelectedIndexChanged += Rdo_CCP_SelectedIndexChanged;

            //관리대상
            string ccpDiv = Rdo_CCP_Div.EditValue.ConvertString_Null_To_Empty();

            if(TargetDiv == "")
			{
                clsRadio.Create_RadioGroupItem(Rdo_Target_Div, code.GetCategory_HighClass(ccpDiv), "명칭", "코드");
                Rdo_Target_Div.SelectedIndex = 0;
			}
			else
			{
                clsRadio.Create_RadioGroupItem(Rdo_Target_Div, code.GetCategory_HighClass(ccpDiv, new string[] { TargetDiv }), "명칭", "코드");
                Rdo_Target_Div.EditValue = TargetDiv;
			}

            Rdo_Target_Div.SelectedIndexChanged += Rdo_Target_SelectedIndexChanged;

            //품목분류
            LupItemClass1.Properties.DataSource = cm.SelectClassItemLoopUp_WithALL(WrGlobal.CorpID, "A01210", null, true);
            LupItemClass1.Properties.ValueMember = "분류코드";
            LupItemClass1.Properties.DisplayMember = "분류명";
            LupItemClass1.EditValue = "ALL";
			LupItemClass1.EditValueChanged += LupItemClass1_EditValueChanged;
        }

		private void LupItemClass1_EditValueChanged(object sender, EventArgs e)
		{
            Display_Std();
		}

		private void Rdo_CCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //관리대상 데이터소스 재조회
            string ccpDiv = Rdo_CCP_Div.EditValue.ConvertString_Null_To_Empty();

            Rdo_Target_Div.Properties.Items.Clear();

            clsRadio.Create_RadioGroupItem(Rdo_Target_Div, code.GetCategory_HighClass(ccpDiv), "명칭", "코드");
            Rdo_Target_Div.SelectedIndex = 0;
        }

        private void Rdo_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display_Std();
        }

        #region GridView Setting

		private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
		{
            Display_Std();
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
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false;

            QM_ExamMgnt Exam = new QM_ExamMgnt();

            RepositoryItem lup_CCP = rpo.Create_RepositoryItem(code.GetCategory_HighClass("F010"), "코드", "명칭"); //CCP구분
            RepositoryItem lup_TargetDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "F01003"), "분류코드", "분류명"); //관리대상 구분
            RepositoryItem lup_Exam_Div = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q005"), "분류코드", "분류명");  //
            RepositoryItem lup_ItemClass1 = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "A01210"), "분류코드", "분류명");  //
            //Column 생성 및 셋팅
            //Grid 공통 설정검사구분
            RepositoryItem lup_ValueType = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "F02010"), "분류코드", "분류명"); //검사값형태
            RepositoryItem lup_Proc_Cd = rpo.Create_RepositoryItem(code.GetProcess_List(), "공정코드", "공정명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "표시순번", "DispSeq");
            grid.Create_GvColumn(gv, "CCP구분", "CcpDivCd", lup_CCP);
            grid.Create_GvColumn(gv, "품목분류", "ItemClassCd", lup_ItemClass1);
            grid.Create_GvColumn(gv, "검사코드", "ExamCd");
            grid.Create_GvColumn(gv, "검사명", "ExamNm");
            grid.Create_GvColumn(gv, "관리대상구분", "TargetDivCd");
            grid.Create_GvColumn(gv, "공정구분", "ProcessCode", lup_Proc_Cd);
            grid.Create_GvColumn(gv, "검사규격", "Spec");
            grid.Create_GvColumn(gv, "검사방법", "Method");
            grid.Create_GvColumn(gv, "검사단위", "Unit");
            grid.Create_GvColumn(gv, "검사주기", "Cycle");
            grid.Create_GvColumn(gv, "값 형태", "ValueTypeDivCd");
            grid.Create_GvColumn(gv, "소수점 자리수", "DecimalCnt");
            grid.Create_GvColumn(gv, "기본값", "DefaultValue");
            grid.Create_GvColumn(gv, "관리범위 하한", "MgntMin");
            grid.Create_GvColumn(gv, "관리범위 상한", "MgntMax");
            grid.Create_GvColumn(gv, "위험범위 하한", "DangerMin");
            grid.Create_GvColumn(gv, "위험범위 상한", "DangerMax");
            grid.Create_GvColumn(gv, "사용여부", "UseYn", rpo.Create_rpoChk());


            //Column 단일 설정
            gv.Columns["ExamCd"].Visible = false;
            gv.Columns["TargetDivCd"].Visible = false;
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["DangerMin"].Visible = false;
            gv.Columns["DangerMax"].Visible = false;

            //Repository 설정
            //검사항목 - X1 ~ X5 CheckEdit 설정
            rpoChk = rpo.Create_rpoChk();
            rpoTxtNum = rpo.Create_rpoTxt_Numeric();
            //사용여부
            gv.Columns["ValueTypeDivCd"].ColumnEdit = lup_ValueType;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ExamNm"]);          //검사명
            grid.Column_AllowEdit(gv.Columns["ItemClassCd"]);          //
            grid.Column_AllowEdit(gv.Columns["Spec"]);        //검사규격
            grid.Column_AllowEdit(gv.Columns["Method"]);      //검사방법
            grid.Column_AllowEdit(gv.Columns["Unit"]);        //검사단위
            grid.Column_AllowEdit(gv.Columns["Cycle"]);       //검사주기
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
                //QM_ExamMgnt Exam = new QM_ExamMgnt();
                //Exam.ExamCd = value;

                //DataRow drExam = Exam.Select_Exam_Info();
                //if (drExam == null) return;

                //drChanged["HaccpCd"] = drChanged["ExamNm"];

                //if (drChanged.Table.Columns.Contains("Spec"))  { drChanged["Spec"] = drExam["ExamSpec"]; }
                //if (drChanged.Table.Columns.Contains("Method")) { drChanged["Method"] = drExam["ExamMethod"]; }
                //if (drChanged.Table.Columns.Contains("Unit")) { drChanged["Unit"] = drExam["ExamUnit"]; }
                //if (drChanged.Table.Columns.Contains("Cycle")) { drChanged["Cycle"] = drExam["ExamCycle"]; }
                //if (drChanged.Table.Columns.Contains("ValueTypeDivCd")) { drChanged["ValueTypeDivCd"] = drExam["ValueTypeDivCd"]; }
                //if (drChanged.Table.Columns.Contains("DecimalCnt")) { drChanged["DecimalCnt"] = drExam["DecimalCnt"]; }
                //if (drChanged.Table.Columns.Contains("ProcessCode")) { drChanged["ProcessCode"] = drExam["ProcessCode"]; }
            }

            gv.BestFitColumns();
        }


        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Display_Std();
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
                dic.Add("CcpDivCd", Rdo_CCP_Div.EditValue);
                dic.Add("TargetDivCd", Rdo_Target_Div.EditValue);
                dic.Add("ItemClassCd", LupItemClass1.EditValue);
                dic.Add("UseYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_Std.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_Std, dic, txt_Act);
        }

        private void Btn_All_Add_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string Item_Div_Cd = Convert.ToString(Rdo_Item_Div.EditValue);
            //    string ExamDivCd = Convert.ToString(Rdo_Exam_Div.EditValue);

            //    if (ExamDivCd == "ALL")
            //    {
            //        MessageBox.Show("검사구분이 '전체'일시 일괄추가 불가");
            //        return;
            //    }

            //    string ItemCode = Gv_ItemClass.GetFocusedRowCellDisplayText("ItemCode").ConvertString_Null_To_Empty();
            //    string Item_Nm = Gv_ItemClass.GetFocusedRowCellDisplayText("ItemName").ConvertString_Null_To_Empty();
            //    string Alias = Gv_ItemClass.GetFocusedRowCellDisplayText("Size").ConvertString_Null_To_Empty();
            //    string Exam_Div_Nm = Rdo_Exam_Div.Properties.Items[Rdo_Exam_Div.SelectedIndex].Description.ToString();

            //    string MSG = $"검사 항목 미등록 품목들은 '검사유형정보'에 등록한 '{Exam_Div_Nm}'의 항목들을 일괄 등록 합니다. \n";
            //          MSG += $"진행 하시겠습니까? \n";

            //    if (MessageBox.Show(MSG, "검사 항목 일괄추가", MessageBoxButtons.YesNo) != DialogResult.Yes)
            //        return;

            //    QM_ItemExamMgnt ItemExam = new QM_ItemExamMgnt();
            //    if (ItemExam.Insert_ItemExam_Batch(Item_Div_Cd, ExamDivCd))
            //    {
            //        MessageBox.Show("저장 되었습니다.");
            //        Func_ItemSearch();
            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

        //등록된 수입, 초중종, 출하 검사항목 일괄추가
        private void Btn_Add_Click(object sender, EventArgs e)
        {
            //DataTable dt = Gv_ItemClass.GridControl.DataSource as DataTable;
            //if (dt.Rows.Count <= 0) return;

            //string ItemCode = Gv_ItemClass.GetFocusedRowCellValue("ItemCode").ConvertString_Null_To_Empty();
            //string ExamDiv = Rdo_Exam_Div.EditValue.ToString();

            //if (ExamDiv == "ALL")
            //{
            //    MessageBox.Show("검사구분이 '전체'일시 일괄추가 불가");
            //    return;
            //}

            //QM_ExamMgnt Exam = new QM_ExamMgnt();

            //Exam.ExamDivCd = ExamDiv;
            //DataTable dtExam = Exam.Add_Exam();

            //int rowPos = 0;
            //DataTable dtItemExam = Gc_ItemExam.DataSource as DataTable;


            //foreach (DataRow dr in dtExam.Rows)
            //{
            //    bool result = false;

            //    //이미 등록되어있는 코드는 제외하고 추가 처리
            //    for (int i = 0; i < Gv_ItemExam.RowCount; i++)
            //    {
            //        if(Convert.ToString(Gv_ItemExam.GetRowCellValue(i, "ExamCd")) == Convert.ToString(dr["ExamCd"]))
            //        {
            //            result = true;
            //            break;
            //        }
            //    }

            //    if (result) continue;

            //    DataRow drInsert = dtItemExam.NewRow();
            //    drInsert.ItemArray = dr.ItemArray;
            //    drInsert["ItemCode"] = ItemCode;

            //    dtItemExam.Rows.InsertAt(drInsert, rowPos++);
            //}

            //Gv_ItemExam.BestFitColumns();
        }

        //선택한 로우 위, 아래로 이동
        private void Btn_RowMove(object sender, EventArgs e)
        {
            SimpleButton Btn = sender as SimpleButton;

            DataTable dt = Gv_Std.GridControl.DataSource as DataTable;

            int idx = Gv_Std.FocusedRowHandle;

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
                    Gv_Std.FocusedRowHandle = Gv_Std.FocusedRowHandle - 1;
                else
                    Gv_Std.FocusedRowHandle = Gv_Std.FocusedRowHandle - 2;
            }
            else
            {
                dt.Rows.InsertAt(newRow, idx + 1);

                Gv_Std.FocusedRowHandle++;
            }

            Gv_Std.SelectRow(Gv_Std.FocusedRowHandle);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            //GridView gv = sender as GridView;
            //grid.Check_OverLap_CellValue(gv, e, "ExamCd");
            //grid.Check_OverLap_CellValue(gv, e, "ExamNm");
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
                        e.RepositoryItem = rpoTxtNum;
                    }
                }
            }
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        #endregion Event End

        #region 조회 기능

        #region 품목 검사항목 조회

   

        private void Display_Std()
        {
			//1.조회 검사
			if (!Check_Search()) return;

			//2.검사항목 조회
			QM_HaccpStd std = new QM_HaccpStd();
            std.CcpDivCd = Rdo_CCP_Div.EditValue.ConvertString_Null_To_Empty();
            std.TargetDivCd = Rdo_Target_Div.EditValue.ConvertString_Null_To_Empty();
            std.ItemClassCd = LupItemClass1.EditValue.ConvertString_Null_To_Empty();

			Gc_Std.DataSource = std.Select_QM_HaccpStd();

			Gv_Std.BestFitColumns();   //Column 너비 조정
		}

        private bool Check_Search()
        {
            //if((Gv_ItemExam.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
            //                                                        OR Status = '수정'
            //                                                        OR Status = '삭제'").Length > 0)
            //{
            //    if (MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        //다른항목 진행할 경우
            //        return true;
            //    }
            //    else
            //    {
            //        //다른항목 진행하지 않을경우
            //        return false;
            //    }
            //}
            //
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
            DataTable dtItem = Gc_Std.DataSource as DataTable;

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
            Display_Std();

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
                    //if (Convert.ToString(dr["ExamDivCd"]) == "")
                    //{
                    //    MessageBox.Show("검사구분 항목이 비어있습니다.");
                    //    return false;
                    //}
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
            QM_HaccpStd haccp = new QM_HaccpStd();

            haccp.ItemClassCd          = dr["ItemClassCd"].ConvertString_Null_To_Empty();
            haccp.CcpDivCd          = dr["CcpDivCd"].ConvertString_Null_To_Empty();
            haccp.ExamCd          = dr["ExamCd"].ConvertString_Null_To_Empty();
            haccp.ExamNm = dr["ExamNm"].ConvertString_Null_To_Empty();
            haccp.TargetDivCd = Rdo_Target_Div.EditValue.ConvertString_Null_To_Empty();
            haccp.DispSeq         = DispSeq;
            haccp.Spec        = dr["Spec"].ConvertString_Null_To_Empty();
            haccp.Method      = dr["Method"].ConvertString_Null_To_Empty();
            haccp.Unit        = dr["Unit"].ConvertString_Null_To_Empty();
            haccp.Cycle       = dr["Cycle"].ConvertString_Null_To_Empty();
            haccp.ValueTypeDivCd = dr["ValueTypeDivCd"].ConvertString_Null_To_Empty();
            haccp.DecimalCnt      = dr["DecimalCnt"].ConvertInt_Null_To_Zero();
            haccp.DefaultValue    = dr["DefaultValue"].ConvertString_Null_To_Empty();
            haccp.MgntMin         = dr["MgntMin"].ConvertDecimal_Null_To_Zero();
            haccp.MgntMax         = dr["MgntMax"].ConvertDecimal_Null_To_Zero();
            haccp.DangerMin       = dr["DangerMin"].ConvertDecimal_Null_To_Zero();
            haccp.DangerMax       = dr["DangerMax"].ConvertDecimal_Null_To_Zero();
            haccp.UseYn           = dr["UseYn"].ConvertString_Null_To_Empty();
            haccp.Option1           = dr["Option1"].ConvertString_Null_To_Empty();
            haccp.Option2   = dr["Option2"].ConvertString_Null_To_Empty();
            haccp.Option3   = dr["Option3"].ConvertString_Null_To_Empty();

            //2. DB 적용
            if (div == 'I')
            {
                //검사 코드 채번
                //Insert
                if (!haccp.Ins_QM_HaccpStd())
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
                if (!haccp.Upd_QM_HaccpStd())
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
                if (!haccp.Del_QM_HaccpStd())
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
            //Frm_QM_ItemExamMgnt_Quantity frm = new Frm_QM_ItemExamMgnt_Quantity();
            //frm.StartPosition = FormStartPosition.CenterParent;
            //frm.WindowState = FormWindowState.Maximized;
            //frm.FormClosed += FrmPopup_FormClosed;
            //frm.ShowDialog();
        }

        private void FrmPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            ////1. 선택한 row 저장
            //int FocusedRowInxItem = (Gc_ItemClass.MainView as GridView).FocusedRowHandle;
            //int FocusedRowInxItemExam = (Gc_ItemExam.MainView as GridView).FocusedRowHandle;

            ////2. 재조회
            //Func_ExamSearch();

            ////3. 저장 후 선택한 row를 불러옴
            //(Gc_ItemClass.MainView as GridView).FocusedRowHandle = FocusedRowInxItem;
            //(Gc_ItemExam.MainView as GridView).FocusedRowHandle = FocusedRowInxItemExam;
        }

        private void BtnCopyItem_Click(object sender, EventArgs e)
		{
			//if ((Gv_ItemExam.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
   //                                                                 OR Status = '수정'
   //                                                                 OR Status = '삭제'").Length > 0)
			//{
			//	XtraMessageBox.Show("변경된 내역이 있습니다. 저장 후 진행할 수 있습니다.");
			//	return;
			//}

			//DataRow dr = Gv_ItemClass.GetFocusedDataRow();
			//if (dr == null) return;

			//string item_div_Cd = Convert.ToString(Rdo_Item_Div.EditValue);
			//string ItemCode = Convert.ToString(dr["ItemCode"]);
			//string ExamDivCd = Convert.ToString(Rdo_Exam_Div.EditValue);
			//DataTable dt = Gc_ItemExam.DataSource as DataTable;

			//Frm_QM_ItemExamMgnt_ItemCopy frm = new Frm_QM_ItemExamMgnt_ItemCopy(item_div_Cd, ExamDivCd, ItemCode);
			//frm.StartPosition = FormStartPosition.CenterParent;
			//frm.WindowState = FormWindowState.Maximized;
			//frm.ShowDialog();
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
			for (int i = 0; i < Gv_Std.RowCount; i++)
			{
                Gv_Std.SetRowCellValue(i, "Status",  "삭제");
			}
		}
	}
}