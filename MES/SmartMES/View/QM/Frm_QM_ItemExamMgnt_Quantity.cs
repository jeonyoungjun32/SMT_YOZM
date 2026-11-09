using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
	public partial class Frm_QM_ItemExamMgnt_Quantity : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();

        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력

        private string FormType = "";

        private List<string> FieldNm = new List<string>();

        public Frm_QM_ItemExamMgnt_Quantity()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //검사구분 데이터 입력
            Set_Exam_Div_Rdo(Rdo_Exam_Div);

            //검사 Lup 설정
            SetLupExam();

            //GridView 데이터 입력
            Set_ItemExamGridView(Gv_ItemExam);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_ItemExam, "Status"))
                {
                    e.Cancel = true;
                }
            });

            Set_FieldNm("제품코드 초기화");
        }

        private void Set_Exam_Div_Rdo(RadioGroup rdo)
        {
            //검사구분 RadioGroup 셋팅
            clsRadioGroup clsRadio = new clsRadioGroup();
            clsRadio.Create_RadioGroupItem(rdo, cm.SelectClassItemLookUp(WrGlobal.CorpID, "Q005"), "분류명", "분류코드");

            rdo.SelectedIndex = 0;

            rdo.SelectedIndexChanged += Rdo_Exam_Div_SelectedIndexChanged;
        }

        private void SetLupExam()
        {
            string exam_Div = Convert.ToString(Rdo_Exam_Div.EditValue);
            QM_ExamMgnt exam = new QM_ExamMgnt();

            LupExam.Properties.DataSource = exam.Select_ExamNumeric(exam_Div);
            LupExam.Properties.DisplayMember = "ExamNm";
            LupExam.Properties.ValueMember = "ExamCd";
        }

        #region GridView Setting

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
            RepositoryItem lup_ExamCd = rpo.Create_RepositoryItemSearchLookUp(Exam.Select_Exam_Lup(Rdo_Exam_Div.EditValue.ToString()), "ExamCd", "ExamNm");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");

            grid.Create_GvColumn(gv, "품목구분", "ItemTypeName");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품번", "Alias");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "규격", "Standard");

            grid.Create_GvColumn(gv, "표시순번", "DispSeq");
            grid.Create_GvColumn(gv, "검사구분", "ExamDivCd");
            grid.Create_GvColumn(gv, "검사코드", "ExamCd");
            grid.Create_GvColumn(gv, "검사명", "ExamNm");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");
            grid.Create_GvColumn(gv, "검사주기", "ExamCycle");
            grid.Create_GvColumn(gv, "값 형태", "ValueTypeDivCd");
            grid.Create_GvColumn(gv, "소수점 자리수", "DecimalCnt");
            grid.Create_GvColumn(gv, "검사규격", "ExamSpec");

            //공차 컬럼 하나로 사용 or 하한공자, 상한공자 사용
			grid.Create_GvColumn(gv, "공차", "Tolerance");

			// UAG 하한공차, 상한공차 컬럼
			grid.Create_GvColumn(gv, "하한 공차", "Tolerance_Min");
            grid.Create_GvColumn(gv, "상한 공차", "Tolerance_Max");

            grid.Create_GvColumn(gv, "기본값", "DefaultValue");
            grid.Create_GvColumn(gv, "관리범위 하한", "MgntMin");
            grid.Create_GvColumn(gv, "관리범위 상한", "MgntMax");
            grid.Create_GvColumn(gv, "위험범위 하한", "DangerMin");
            grid.Create_GvColumn(gv, "위험범위 상한", "DangerMax");
            grid.Create_GvColumn(gv, "사용여부", "UseYn");

            gv.Columns["DispSeq"].Visible = false;

            if (Database.DbName.ToUpper() == "UAGHITEC")
			{
                gv.Columns["Tolerance"].Visible = false;

            }
            if (Database.DbName.ToUpper() == "ALLTHEN" || Database.DbName.ToUpper() == "TPG")
			{
                gv.Columns["Alias"].Visible = false;
                gv.Columns["Tolerance_Min"].Visible = false;
                gv.Columns["Tolerance_Max"].Visible = false;
			}

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
            grid.Column_AllowEdit(gv.Columns["Item_Nm"], false);
            //grid.Column_AllowEdit(gv.Columns["ExamNm"]);          //검사명
            //grid.Column_AllowEdit(gv.Columns["ExamDivCd"]);      //검사구분
            grid.Column_AllowEdit(gv.Columns["ExamSpec"]);        //검사규격
            grid.Column_AllowEdit(gv.Columns["ExamMethod"]);      //검사방법
            grid.Column_AllowEdit(gv.Columns["ExamUnit"]);        //검사단위
            grid.Column_AllowEdit(gv.Columns["ExamCycle"]);       //검사주기
            grid.Column_AllowEdit(gv.Columns["ValueTypeDivCd"]); //값 형태
            grid.Column_AllowEdit(gv.Columns["DecimalCnt"]);      //소수점 자리수
            grid.Column_AllowEdit(gv.Columns["DefaultValue"]);    //기본값

            grid.Column_AllowEdit(gv.Columns["Tolerance_Min"]);         //하한 공차
            grid.Column_AllowEdit(gv.Columns["Tolerance_Max"]);         //상한 공차
			grid.Column_AllowEdit(gv.Columns["Tolerance"]);         //공차
			grid.Column_AllowEdit(gv.Columns["MgntMin"]);         //관리범위 하한
            grid.Column_AllowEdit(gv.Columns["MgntMax"]);         //관리범위 상한
            grid.Column_AllowEdit(gv.Columns["DangerMin"]);       //위험범위 하한
            grid.Column_AllowEdit(gv.Columns["DangerMax"]);       //위험범위 상한
            grid.Column_AllowEdit(gv.Columns["UseYn"]);           //사용여부

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            gv.Columns["DecimalCnt"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;   //소수점 자리수
            gv.Columns["MgntMin"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;      //관리항목 하한
            gv.Columns["MgntMax"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;      //관리항목 상한
            gv.Columns["DangerMin"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;    //위험범위 하한
            gv.Columns["DangerMax"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;    //위험범위 상한
            gv.Columns["UseYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //사용여부

            if (Database.DbName.ToUpper().Contains("UAGHITEC"))
            {
                grid.SetCol_Type(gv.Columns["Tolerance_Min"], clsGrid.TypeList.TFloat);
                grid.SetCol_Type(gv.Columns["Tolerance_Max"], clsGrid.TypeList.TFloat);
            }
            else
            {
                grid.SetCol_Type(gv.Columns["Tolerance"], clsGrid.TypeList.TFloat);
            }

            //이벤트 설정
            grid.RowNumber_Indicator(gv);
            gv.CellValueChanged += Event_CellValueChanged;                  //셀값 수정시 상태값 수정

            gv.CellMerge += Gv_CellMerge;                                   // cell Merge
            gv.OptionsView.AllowCellMerge = true;                           // Merge허용

            if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["ExamCd"].Visible = false;
                gv.Columns["DangerMin"].Visible = false;
                gv.Columns["DangerMax"].Visible = false;
            }

            Gv_ItemExam.BestFitColumns();
        }

        private void Set_FieldNm(string div_Cd)
        {
            if (WrGlobal.CdOption.ContainsKey(div_Cd))
            {
                foreach (string fieldNm in WrGlobal.CdOption[div_Cd].Keys)
                {
                    FieldNm.Add(fieldNm);
                }

                FieldNm.Add("Item_Div_Cd");
                FieldNm.Add("ExamDivCd");
            }
        }

        private void Gv_CellMerge(object sender, CellMergeEventArgs e)
         {
            if (FieldNm.Contains(e.Column.FieldName))
            {
                string value1 = Gv_ItemExam.GetRowCellValue(e.RowHandle1, e.Column.FieldName).ConvertString_Null_To_Empty();
                string value2 = Gv_ItemExam.GetRowCellValue(e.RowHandle2, e.Column.FieldName).ConvertString_Null_To_Empty();

                e.Merge = value1 == value2;
            }
            else
            {
                // 나머지행은 병합 x
                e.Merge = false;
            }

            e.Handled = true;
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
            }
            else if (fieldName.Contains("Tolerance") || fieldName == "DefaultValue")
            {
                if(value == "0")
                {
                    drChanged["MgntMin"] = DBNull.Value;
                    drChanged["MgntMax"] = DBNull.Value;
                }
                else
                {
					//float defaultValue = 0;
					//
					//if (float.TryParse(Convert.ToString(drChanged["DefaultValue"]), out defaultValue))
					//{
					//    drChanged["MgntMin"] = defaultValue - value.ConvertFloat_Null_To_Zero();
					//    drChanged["MgntMax"] = defaultValue + value.ConvertFloat_Null_To_Zero();
					//
					//}

					if (fieldName.Contains("Tolerance_"))
					{
						drChanged["MgntMin"] = gv.GetFocusedRowCellValue("DefaultValue").ConvertFloat_Null_To_Zero() - gv.GetFocusedRowCellValue("Tolerance_Min").ConvertFloat_Null_To_Zero();
						drChanged["MgntMax"] = gv.GetFocusedRowCellValue("DefaultValue").ConvertFloat_Null_To_Zero() + gv.GetFocusedRowCellValue("Tolerance_Max").ConvertFloat_Null_To_Zero();
					}
					else
					{
						drChanged["MgntMin"] = gv.GetFocusedRowCellValue("DefaultValue").ConvertFloat_Null_To_Zero() - gv.GetFocusedRowCellValue("Tolerance").ConvertFloat_Null_To_Zero();
						drChanged["MgntMax"] = gv.GetFocusedRowCellValue("DefaultValue").ConvertFloat_Null_To_Zero() + gv.GetFocusedRowCellValue("Tolerance").ConvertFloat_Null_To_Zero();
					}
				}
            }

            //gv.BestFitColumns();
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_QM_ItemExamMgnt_Quantity_Load(object sender, EventArgs e)
        {
            Search();
        }

        #endregion Event End

        #region 조회 기능

        #region 품목 조회

        private void Rdo_Item_Div_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        #endregion 품목 조회 End

        #region 품목 검사항목 조회

        private void Rdo_Exam_Div_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void LupExam_EditValueChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string exam_Div = Convert.ToString(Rdo_Exam_Div.EditValue);
            string ExamCd = Convert.ToString(LupExam.EditValue);

            //1. Exam Grid Repository 및 Lup 설정
            QM_ExamMgnt Exam = new QM_ExamMgnt();
            DataTable dtExam = Exam.Select_Exam_Lup(exam_Div);

            RepositoryItem lup_ExamCd = rpo.Create_RepositoryItemSearchLookUp(dtExam, "ExamCd", "ExamNm");
            Gv_ItemExam.Columns["ExamNm"].ColumnEdit = lup_ExamCd;

            SetLupExam();
            //2.품목별검사항목 조회
            QM_ItemExamMgnt ItemExam = new QM_ItemExamMgnt();
            ItemExam.ExamDivCd = exam_Div;
            ItemExam.ExamCd = ExamCd;
            ItemExam.ValueTypeDivCd = "20";   //정량

            Gc_ItemExam.DataSource = ItemExam.Select_ItemExam_ValueType();

            //Gv_ItemExam.BestFitColumns();   //Column 너비 조정 - 속도 오래걸려서 주석처리 함

            Gv_ItemExam.Columns["ItemName"].BestFit();
            Gv_ItemExam.Columns["Alias"].BestFit();
        }


        #endregion 품목 검사항목 조회 End

        #endregion 조회버튼 클릭 End

        #region 저장 기능

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            switch (btn.Name)
            {
                case "BtnSearch":
                    Search();
                    break;

                case "BtnSave":
                    Func_Save();
                    break;
            }
        }

        private void Func_Save()
        {
            //작성내역 저장
            DataTable dtItem = Gc_ItemExam.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtItem)) return;

            //2. 데이터 저장
            foreach (DataRow dr in dtItem.Rows)
            {
                //상태가 공백인 row를 제외 후 저장 처리
                if (dr.IsNull("Status") || string.IsNullOrWhiteSpace(dr["Status"].ToString()))
                    continue;

                string status = dr["Status"].ToString();

                if (status == "신규")
                {
                    if (!Save_ToDatabase(dr, 'I')) return;
                }
                else if (status == "수정")
                {
                    if (!Save_ToDatabase(dr, 'U')) return;
                }
                else if (status == "삭제")
                {
                    if (!Save_ToDatabase(dr, 'D')) return;
                }
                else
                {
                    if (!Save_ToDatabase(dr, 'U')) return;
                }
            }

            /*for (int curHandle = 0; curHandle < dtItem.Rows.Count; curHandle++)
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
                else //수정, 미작업 로우
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U')) return;
                }
            }*/

            //3. 정상절차 진행
            Search();

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

        private bool Save_ToDatabase(DataRow dr, char div)
        {

            //데이터베이스에 반영
            //1. 기본 셋팅
            QM_ItemExamMgnt ItemExam = new QM_ItemExamMgnt();

            ItemExam.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            ItemExam.ExamDivCd      = dr["ExamDivCd"].ConvertString_Null_To_Empty();
            ItemExam.ExamCd          = dr["ExamCd"].ConvertString_Null_To_Empty();
            ItemExam.DispSeq         = dr["DispSeq"].ConvertInt_Null_To_Zero();
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
    }
}