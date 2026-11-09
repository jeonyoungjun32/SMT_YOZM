using baseSMT;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
	/*
     * 일일검사
     */
	public partial class Frm_QM_ExamResultDaily : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();

        private string Exam_Div = string.Empty; //검사구분

        int ExamCnt = 1;// Convert.ToInt16(WrGlobal.ProcOption["QT"]["Exam_Cnt"]);   //검사수량
        string Default_Set_Yn = "N"; //검사 디폴트 값 셋팅 여부

        RepositoryItemLookUpEdit rpoPass = new RepositoryItemLookUpEdit();
        RepositoryItemTextEdit rpoText = new RepositoryItemTextEdit();
        RepositoryItemTextEdit rpoTextFree = new RepositoryItemTextEdit();
        RepositoryItemTextEdit rpoReadOnly = new RepositoryItemTextEdit();
        public Frm_QM_ExamResultDaily(string exam_Div)
        {
            InitializeComponent();

            switch (exam_Div)
            {
                default:     Exam_Div = exam_Div; break;    //
            }

            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            DtpExamDate.EditValue = DateTime.Now;

            //검사구분에따른 설정
            Set_TypeOption();

            //업체별 옵션
            Set_CompanyOption();

            //GridView 설정
            Set_GridView_ExamRst(Gv_ExRst);
            Set_GridView_Improve(GvImprove);

            Set_Lup();
        }

        private void Frm_BN_Order_Load(object sender, EventArgs e)
        {
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정

            //올덴 요청사항 - 측정시간 항목에 마우스를 클릭하면, 항목 전체가 이미 활성화 되어서 숫자만 입력 가능하게 변경 (2026.03.26)
            TxtMeasureTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            TxtMeasureTime.Properties.Mask.EditMask = "00:00";
            TxtMeasureTime.Properties.Mask.UseMaskAsDisplayFormat = true;
            TxtMeasureTime.Properties.Mask.ShowPlaceHolders = true;
            TxtMeasureTime.Properties.Mask.SaveLiteral = false;

            Search_QC_List();
        }

        private void Set_CompanyOption()
        {
            layoutControlGroup1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            splitterItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void Set_TypeOption()
        {
            
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("코드"), new DataColumn("명칭") });
            dt.Rows.Add("Y", "OK");
            dt.Rows.Add("N", "NG");

            rpoPass = rpo.Create_RepositoryItem(dt, "코드", "명칭");
            rpoText = new RepositoryItemTextEdit();
            rpoTextFree = new RepositoryItemTextEdit
            {
                Mask = { MaskType = DevExpress.XtraEditors.Mask.MaskType.None }
            };
            rpoReadOnly = new RepositoryItemTextEdit
            {
                ReadOnly = true,
                Enabled = false,
                AllowFocused = false,
                Appearance = { BackColor = smtDesignSetting.ReadOnlyColor }
            };

            pnlMeasureDiv.Visible = true;
            PnlMeasureTime.Visible = true;
        }

        #region GridView 셋팅

        private void Set_GridView_ExamRst(GridView gv)
        {
            //값 측정검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //헤더 멀티라인 설정
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            gv.OptionsView.AllowCellMerge = true;

            //검사수량
            grid.Create_GvColumn(gv, "검사코드", "ExamCd");
            grid.Create_GvColumn(gv, "검사명", "ExamNm");
            grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            //grid.Create_GvColumn(gv, "관리범위<br>하한", "Mgnt_Min");
            //grid.Create_GvColumn(gv, "관리범위<br>상한", "Mgnt_Max");
            grid.Create_GvColumn(gv, "측정구분", "MeasureDivCd");
            grid.Create_GvColumn(gv, "측정시간", "MeasureTime");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");

			for (int i = 1; i < ExamCnt + 1; i++)
			{
				grid.Create_GvColumn(gv, i + "차", "X" + i);
			}

			grid.Create_GvColumn(gv, "판정", "PassDivCd");
            grid.Create_GvColumn(gv, "조치내역", "ActionMemo");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.Create_GvColumn(gv, "검사번호", "ExamNo");
            grid.Create_GvColumn(gv, "검사순번", "ExamNoSeq");

            // 정렬
            gv.Columns["ExamNm"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;   
            gv.Columns["ExamMethod"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["MeasureDivCd"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["MeasureTime"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["ExamUnit"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["X1"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            if (gv.Columns.ColumnByFieldName("X2") != null) gv.Columns["X2"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            if (gv.Columns.ColumnByFieldName("X3") != null) gv.Columns["X3"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            if (gv.Columns.ColumnByFieldName("X4") != null) gv.Columns["X4"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gv.Columns["PassDivCd"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //등록자, 수정자 column 숨김
            gv.Columns["ExamCd"].Visible = false;
            gv.Columns["MeasureDivCd"].Visible = false;
            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreIP"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModIP"].Visible = false;
            gv.Columns["ModDt"].Visible = false;


            //Repository 셋팅
            gv.Columns["PassDivCd"].ColumnEdit = rpo.Create_RepositoryItem(code.GetCM_CateItem(WrGlobal.CorpID, "Q020"), "코드", "명칭");

            //셀값 변경 허용
            for (int i = 1; i < ExamCnt + 1; i++)
            {
                    grid.Column_AllowEdit(gv.Columns["X" + i]);
            }
            grid.Column_AllowEdit(gv.Columns["PassDivCd"]);

            grid.Column_AllowEdit(gv.Columns["ActionMemo"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            gv.Columns["ExamNm"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            gv.Columns["ExamUnit"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            gv.Columns["MeasureTime"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
            gv.Columns["X1"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            if(gv.Columns.ColumnByFieldName("X2") != null) gv.Columns["X2"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            if(gv.Columns.ColumnByFieldName("X3") != null) gv.Columns["X3"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            if(gv.Columns.ColumnByFieldName("X4") != null) gv.Columns["X4"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            gv.Columns["PassDivCd"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            gv.Columns["ActionMemo"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            gv.Columns["Remark"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            //grid.SetCol_Type(gv.Columns["Mgnt_Min"], cls_Grid.TypeList.TFloat);
            //grid.SetCol_Type(gv.Columns["Mgnt_Max"], cls_Grid.TypeList.TFloat);

            //이벤트
            gv.CellValueChanged += Event_CellValueChagned; ;    //셀값 수정 Event
            gv.RowCellClick += Event_CellClick;
            gv.CustomDrawCell += Event_Cell_ForeColor;   //불합격이면 글자색 빨강
            gv.CustomRowCellEdit += Gv_CustomRowCellEdit;
            //gv.CustomRowCellEditForEditing += Gv_CustomRowCellEditForEditing;

            gv.Columns["ExamNo"].Visible = false;
            gv.Columns["ExamNoSeq"].Visible = false;
        }

        private void Event_Cell_ForeColor(object sender, RowCellCustomDrawEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle < 0) return;

            string fieldName = e.Column.FieldName;
            string valueType = gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd").ConvertString_Null_To_Empty();

            if (fieldName == "X1" || fieldName == "X2" || fieldName == "X3" || fieldName == "X4" || fieldName == "X5" || fieldName == "X6")
            {
                string value = gv.GetRowCellValue(e.RowHandle, fieldName)?.ToString();

                if (valueType == "10")
				{
                    if (value == "Y")
                    {
                        e.Appearance.ForeColor = Color.Black; // 10이면 검은색 글자로 변경
                    }
                    else if (value == "N")
                    {
                        e.Appearance.ForeColor = Color.Red; // 20이면 빨간색 글자로 변경
                    }
                }
                else if(valueType == "20")
				{
                    if(!Chk_Num(gv.GetDataRow(e.RowHandle), fieldName))
					{
                        e.Appearance.ForeColor = Color.Red;
					}
					else
					{
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
			}
            else if (fieldName == "PassDivCd")
            {
                string passDivCd = gv.GetRowCellValue(e.RowHandle, "PassDivCd")?.ToString();

                if (passDivCd == "10")
                {
                    e.Appearance.ForeColor = Color.Black; // 10이면 검은색 글자로 변경
                }
                else if (passDivCd == "20")
                {
                    e.Appearance.ForeColor = Color.Red; // 20이면 빨간색 글자로 변경
                }
            }
        }

        private void Set_GridView_Improve(GridView gv)
        {
            //값 측정검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //헤더 멀티라인 설정
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            //gv.OptionsView.AllowCellMerge = true;

            //검사수량
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "관리기준<br>이탈사항", "Issue");
            grid.Create_GvColumn(gv, "개선조치 및 결과", "ActionResult");
            grid.Create_GvColumn(gv, "비고", "Remark");

			//셀값 변경 허용
			grid.Column_AllowEdit(gv.Columns["Issue"]);
			grid.Column_AllowEdit(gv.Columns["ActionResult"]);
			grid.Column_AllowEdit(gv.Columns["Remark"]);

            // 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            //51 : 온습도, 52:개인위생
            gv.CellValueChanged += Gv_CellValueChanged;
        }

		private void Gv_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            //string fieldName = e.Column.FieldName;
            //DataRow drChanged = gv.GetDataRow(e.RowHandle);
            //string value = e.Value.ConvertString_Null_To_Empty();

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == "Status") return;
            grid.Change_Status(gv, e.RowHandle);

            //gv.BestFitColumns();
        }

		private void Gv_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView gv = sender as GridView;

            string fieldName = e.Column.FieldName;
            if (!fieldName.StartsWith("X")) return;

            string valueType = gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd").ConvertString_Null_To_Empty();

            // 정성 값이면 Lookup으로 보여줌
            if (valueType == "10")
            {
                e.RepositoryItem = rpoPass;
            }
            else if (valueType == "20" || valueType == "30")
            {
                e.RepositoryItem = rpoText;
			}
			else
			{
                e.RepositoryItem = rpoText;
			}
        }

        private void Gv_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
			GridView gv = sender as GridView;

			string fieldName = e.Column.FieldName;
			if (!fieldName.StartsWith("X")) return;

			string valueType = gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd").ConvertString_Null_To_Empty();

			// 기본 설정: 정성/정량에 따라 편집기 지정
			// 정성 값이면 Lookup으로 보여줌
			if (valueType == "10")
			{
				e.RepositoryItem = rpoPass;
			}
			else if (valueType == "20" || valueType == "30")
			{
				e.RepositoryItem = rpoText;
			}
			else
			{
				e.RepositoryItem = rpoText;
			}
		}

        private void Event_CellValueChagned(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            string PassDivCd = "10"; //10:합격 20:불합격
            string valueType = gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd")?.ToString();

            //검사값에따른 Pass_Div(최종검사여부) 변경
            if (fieldNm.StartsWith("X"))
            {
                //값 판정
                string[] xField = new string[] { "X1", "X2", "X3", "X4", "X5", "X6" };

                for (int i = 0; i < ExamCnt; i++)
                {
                    if (valueType == "10")
                    {
                        if (dr[xField[i]].ConvertString_Null_To_Empty() == "N")
                        {
                            PassDivCd = "20";
                            break;
                        }
                    }
                    else if (valueType == "20")
                    {
                        if (!Chk_Num(dr, xField[i]))
                        {
                            PassDivCd = "20";
                            break;
                        }
                    }

                }

                gv.SetFocusedRowCellValue("PassDivCd", PassDivCd);
            }

            gv.BestFitColumns();
        }

        private void Event_CellClick(object sender, RowCellClickEventArgs e)
        {
            if (!e.Column.FieldName.StartsWith("X")) return;

            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            string valueType = gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd")?.ToString();

            if (valueType != "10") return;

            string currentValue = Convert.ToString(dr[e.Column.FieldName]);
            string newValue = "";

            switch (currentValue)
            {
                case "Y":
                    newValue = "N";
                    break;
                case "N":
                    newValue = "N/A";
                    break;
                case "N/A":
                    newValue = "Y";
                    break;
                default:
                    newValue = "Y";
                    break;
            }

            gv.SetFocusedRowCellValue(e.Column.FieldName, newValue);

            gv.RefreshData();
        }

        private bool Chk_Num(DataRow dr, string fieldName)
        {
			bool result = true;

			//if (fieldName == "")
			//{
			//    return result;
			//}

			//float mgnt_Min = dr["Mgnt_Min"].ConvertFloat_Null_To_Zero();
			//float mgnt_Max = dr["Mgnt_Max"].ConvertFloat_Null_To_Zero();
			//float danger_Min = dr["Danger_Min"].ConvertFloat_Null_To_Zero();
			//float danger_Max = dr["Danger_Max"].ConvertFloat_Null_To_Zero();
			//float xValue = dr[fieldName].ConvertFloat_Null_To_Zero();

			////관리범위 오버 검사
			////상한만 있을때
			//if (mgnt_Min == 0 && mgnt_Max > 0)
			//{
			//    if (xValue > mgnt_Max)
			//    {
			//        //값이 상한보다 높으면 불합 처리
			//        result = false;
			//    }
			//}
			////하한만 있을때
			//else if (mgnt_Min > 0 && mgnt_Max == 0)
			//{
			//    if (xValue < mgnt_Min)
			//    {
			//        //값이 하한보다 낮으면 불합 처리
			//        result = false;
			//    }
			//}
			////상,하한 둘다 있을때
			//else if (mgnt_Min > 0 && mgnt_Min < mgnt_Max)
			//{
			//    if (xValue < mgnt_Min || xValue > mgnt_Max)
			//    {
			//        //값이 하한보다 낮거나 상한보다 높으면 불합 처리
			//        result = false;
			//    }
			//}

			////위험범위 오버 검사
			////상한만 있을때
			//if (danger_Min == 0 && danger_Max > 0)
			//{
			//    if (xValue > danger_Max)
			//    {
			//        //값이 상한보다 높으면 불합 처리
			//        result = false;
			//    }
			//}
			////하한만 있을때
			//else if (danger_Min > 0 && danger_Max == 0)
			//{
			//    if (xValue < danger_Min)
			//    {
			//        //값이 하한보다 낮으면 불합 처리
			//        result = false;
			//    }
			//}
			////상,하한 둘다 있을때
			//else if (danger_Min > 0 && danger_Min < danger_Max)
			//{
			//    if (xValue < danger_Min || xValue > danger_Max)
			//    {
			//        //값이 하한보다 낮거나 상한보다 높으면 불합 처리
			//        result = false;
			//    }
			//}

			return result;
        }

        #endregion GridView 셋팅 End

        private void Set_Lup()
        {
            //2. 측정구분
            string cm_Class = "";

            switch (Exam_Div)
            {
                case "51": cm_Class = "Q01151"; break; //초중종
            }

            DataTable dt = cm.SelectClassItemLoopUp_WithALL(WrGlobal.CorpID, cm_Class, null, false);

            cls_Lookup clsLup = new cls_Lookup();

            clsLup.SetLup_Combo(LupMeasure_Div, dt, "분류코드", "분류명");

            LupMeasure_Div.EditValueChanged += LupMeasure_Div_EditValueChanged;

            //초기값 설정
            if (dt.Rows.Count > 0)
            {
                LupMeasure_Div.EditValue = dt.Rows[0]["분류코드"];
            }

            ////검사자
            //string[] deptId = code.GetCateItemCode(WrGlobal.CorpID, "Q003");
            //if (deptId.Length < 1) deptId = null; //공통코드에 검사부서 등록하여 불러오기, 없으면 전체 사용자 조회하도록

            DataTable dtUser = code.GetDataTableUser(false, null, "N");
            clsLup.Create_RepositoryItemSearchLookUp(LupExamEmpId, dtUser, "코드", "명칭");

            //설비
            LupMachine.Properties.DataSource = code.Select_Machine_Lup(true, false);
            LupMachine.Properties.ValueMember = "설비코드";
            LupMachine.Properties.DisplayMember = "설비명";
        }

        private void LupMeasure_Div_EditValueChanged(object sender, EventArgs e)
        {
            Search_QC_List();
        }

        #endregion

        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton Btn = (smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdDisplay": Search_QC_List(); break;
                case "cmdSave": Save_Data(); break;
                case "cmdMillSheet": Upload_MillSheet(); break;
                case "cmdDelete": Delete_Data(); break;
                case "cmdInspectionRpt": Print(); break;
                case "cmdLabelPrint": Print2(); break;
                case "cmdInspectStd": Print_InspectionStd(); break;
            }
        }

        #region 데이터 조회

        private void Search_QC_List()
        {
            //검사목록 불러오기
            //측정구분
            string measure_Div = "";

            switch (Exam_Div)
            {
                case "51":
                    measure_Div = LupMeasure_Div.EditValue.ConvertString_Null_To_Empty();
            break;
            }

            QM_ExamResultDaily qrd = new QM_ExamResultDaily();
            qrd.ExamDate = DtpExamDate.DateTime.ToString("yyyyMMdd");
            qrd.ExamDivCd = Exam_Div;           //검사 구분
            qrd.MeasureDivCd = measure_Div; //측정 구분

            DataTable dtExam = qrd.SelectExamResultByExamDate();

            //검사한 항목일경우
            if (dtExam.Rows.Count > 0)
            {
                Gc_ExRst.DataSource = dtExam;
            }
            else
            {
                //미검사 항목일 경우
                QM_ExamMgnt iem = new QM_ExamMgnt();
                iem.ExamDivCd = Exam_Div;
                Gc_ExRst.DataSource = iem.Select_ItemExam_List_Daily();

                //if (Default_Set_Yn == "Y")
                //{
                //    SetDefaultValue();
                //}
            }

            Gv_ExRst.BestFitColumns();

            Set_ExamDate();
            Set_MeasureTime();

            //Search_Improve();
        }

        private void SetDefaultValue()
        {
            DataTable dt = Gc_ExRst.DataSource as DataTable;

            //정성일 경우 디폴트값, 정량일 경우 해당 검사 규격의 랜덤값으로 설정
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());

                    foreach (DataRow dr in dt.Rows)
                    {
                        string value_Type = dr["ValueTypeDivCd"].ConvertString_Null_To_Empty();
                        //double minValue = dr["Mgnt_Min"].ConvertDouble_Null_To_Zero();
                        //double maxValue = dr["Mgnt_Max"].ConvertDouble_Null_To_Zero();
                        double defaultValue = dr["Default_Value"].ConvertDouble_Null_To_Zero();
                        int decimal_Cnt = dr["Decimal_Cnt"].ConvertInt_Null_To_Zero();

                        if (value_Type == "10")
                        {
                            dr["X1"] = dr["Default_Value"];
                            if (ExamCnt == 1) continue;

                            dr["X2"] = dr["Default_Value"];
                            if (ExamCnt == 2) continue;

                            dr["X3"] = dr["Default_Value"];
                            if (ExamCnt == 3) continue;

                            dr["X4"] = dr["Default_Value"];
                            if (ExamCnt == 4) continue;

                            dr["X5"] = dr["Default_Value"];
                            if (ExamCnt == 5) continue;

                            dr["X6"] = dr["Default_Value"];
                        }
						//    else
						//    {
						//        if (minValue == 0 && maxValue == 0 && defaultValue == 0) continue;

						//        if (minValue == 0 && maxValue == 0)
						//        {
						//            if (defaultValue != 0)
						//            {
						//                minValue = defaultValue;
						//                maxValue = defaultValue;
						//            }
						//        }
						//        else if (minValue == 0)
						//        {
						//            if (maxValue != 0)
						//            {
						//                minValue = maxValue - 3;
						//            }
						//            else if (defaultValue != 0)
						//            {
						//                minValue = defaultValue - 3;
						//            }
						//        }
						//        else if (maxValue == 0)
						//        {
						//            if (minValue != 0)
						//            {
						//                maxValue = minValue + 3;
						//            }
						//            else if (defaultValue != 0)
						//            {
						//                maxValue = defaultValue + 3;
						//            }
						//        }

						//        double randValue = 0;

						//        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), decimal_Cnt);
						//        dr["X1"] = randValue;
						//        if (ExamCnt == 1) continue;

						//        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), decimal_Cnt);
						//        dr["X2"] = randValue;
						//        if (ExamCnt == 2) continue;

						//        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), decimal_Cnt);
						//        dr["X3"] = randValue;
						//        if (ExamCnt == 3) continue;

						//        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), decimal_Cnt);
						//        dr["X4"] = randValue;
						//        if (ExamCnt == 4) continue;

						//        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), decimal_Cnt);
						//        dr["X5"] = randValue;
						//        if (ExamCnt == 5) continue;

						//        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), decimal_Cnt);
						//        dr["X6"] = randValue;
						//    }
					}
				}
			}
		}

        private void Set_ExamDate()
		{
            //검사일자 바인딩
            DataTable dtSave = Gc_ExRst.DataSource as DataTable;

            if (dtSave.Rows.Count > 0)
            {
                DataRow dr = dtSave.Rows[0];

                if (dr["ExamNo"].ConvertString_Null_To_Empty() != "")
                {
                    //검사자
                    LupExamEmpId.EditValue = dr["ExamEmpId"];
                    LupMachine.EditValue = dr["MachineCode"];
                }
                else
                {

                    LupExamEmpId.EditValue = null;
                    LupMachine.EditValue = null;
                }
			}
			else
			{
                LupExamEmpId.EditValue = null;
                LupMachine.EditValue = null;
            }
        }

        private void Set_MeasureTime()
        {
            //측정시간 바인딩
            DataTable dtSave = Gc_ExRst.DataSource as DataTable;

            if (dtSave.Rows.Count > 0)
            {
                DataRow dr = dtSave.Rows[0];

                if (dr["ExamNo"].ConvertString_Null_To_Empty() != "")
                {
					//검사항목
					TxtMeasureTime.EditValue = dr["MeasureTime"];
				}
                else
                {
                    //미검사항목 측정시간 Default 값 바인딩

                    string cm_Class = "";

                    switch (Exam_Div)
                    {
                        case "51": cm_Class = "Q01151"; break; //
                    }

                    CM_CateItem cci = new CM_CateItem();
                    cci.Class = cm_Class;
                    cci.ClassItemCode = LupMeasure_Div.EditValue.ConvertString_Null_To_Empty();

                    DataRow drCmCd = cci.Select_CateItem_ByCode();

                    if (drCmCd != null)
                    {
						TxtMeasureTime.EditValue = drCmCd["ExtraValue"];
					}
                }
            }
        }

        #endregion 데이터 조회 End

        #region 데이터 저장

        private void Save_Data()
        {
            this.Validate();

            //데이터 저장
            try
            {
                DataTable dtSave = Gc_ExRst.DataSource as DataTable;

                if (dtSave.Rows.Count < 1) return;

                //검사번호 만들기
                string ExamNo = Convert.ToString(dtSave.Rows[0]["ExamNo"]);    //검사번호
                string div = Exam_Div;
                //if (Exam_Div == "25") { div = "20"; }

                QM_ExamResultDaily erd = new QM_ExamResultDaily();
                erd.ExamDate = DtpExamDate.Text.ConvertDate_To_Char8();                   //검사일자
                erd.ItemCode = "";//drBf["MaterialCode"].ConvertString_Null_To_Empty();    //품목
                erd.ExamDivCd = div;                                              //검사구분
                erd.RefNo = ""; //drBf["Ref_No"].ConvertString_Null_To_Empty();    //참조번호
                erd.RefNoSeq = 0; //drBf["Ref_No_Seq"].ConvertInt_Null_To_Zero();        //참조순번
                erd.ExamQty = 0;//drBf["Exam_Qty"].ConvertInt_Null_To_Zero();        //검사수량
                erd.ExamEmpId = LupExamEmpId.EditValue.ConvertString_Null_To_Empty();        //검사자
                erd.MachineCode = LupMachine.EditValue.ConvertString_Null_To_Empty();        //검사자

                //측정구분
                string measure_Div = "";


                switch (Exam_Div)
                {
                    case "51":
                        measure_Div = LupMeasure_Div.EditValue.ConvertString_Null_To_Empty();
                        break;
                }

                bool Success = true;
                if (ExamNo == "")
                {
                    //신규등록
                    //측정 구분에따른 추가 신규입력 구분
                    DataRow drExists = erd.SelectExamResultExsitsDaily();
                    int idx = 0;
                    if (drExists == null)
                    {
                        //측정구분이 다른항목이 입력된적 없는경우
                        erd.ExamNo = Create_No();
                    }
                    else
                    {
                        //측정구분이 다른항목이 입력된적 있는경우
                        erd.ExamNo = drExists["ExamNo"].ConvertString_Null_To_Empty();
                        idx = drExists["ExamNoSeq"].ConvertInt_Null_To_Zero();
                    }

                    //데이터 저장
                    foreach (DataRow drInsert in dtSave.Rows)
                    {
                        erd.ExamNoSeq = idx + 1;                                                   //검사순번
                        erd.DispSeq = erd.ExamNoSeq;                                           //표시순번
                        erd.ExamCd = drInsert["ExamCd"].ConvertString_Null_To_Empty();         //검사코드
                        erd.MeasureDivCd = measure_Div; //측정구분
                        erd.MeasureTime = measure_Div != "" ? TxtMeasureTime.EditValue.ConvertString_Null_To_Empty() : "";    //측정시간
                        erd.X1 = drInsert["X1"].ConvertString_Null_To_Empty();              //검사값1
                        erd.X2 = drInsert["X2"].ConvertString_Null_To_Empty();              //검사값2
                        erd.X3 = drInsert["X3"].ConvertString_Null_To_Empty();              //검사값3
                        erd.X4 = drInsert["X4"].ConvertString_Null_To_Empty();              //검사값4
                        erd.X5 = drInsert["X5"].ConvertString_Null_To_Empty();              //검사값5
                        erd.X6 = drInsert["X6"].ConvertString_Null_To_Empty();              //검사값6
                        erd.PassDivCd = drInsert["PassDivCd"].ConvertString_Null_To_Empty();     //판정여부
                        erd.ActionMemo = drInsert["ActionMemo"].ConvertString_Null_To_Empty();     //조치내역
                        erd.Remark = drInsert["Remark"].ConvertString_Null_To_Empty();             //비고

                        //Insert
                        if (!erd.InsExamResultDaily())
                        {
                            //저장 중 에러 발생시
                            erd.DelExamResultByRefNoExamDiv();

                            Success = false;
                            break;
                        }
                        idx++;
                    }
                }
                else
                {
                    //수정
                    foreach (DataRow drInsert in dtSave.Rows)
                    {
                        erd.ExamNo = drInsert["ExamNo"].ConvertString_Null_To_Empty();         //검사번호
                        erd.ExamNoSeq = drInsert["ExamNoSeq"].ConvertInt_Null_To_Zero();         //검사순번
                        erd.DispSeq = erd.ExamNoSeq;                                           //표시순번
                        erd.ExamCd = drInsert["ExamCd"].ConvertString_Null_To_Empty();         //검사코드
                        erd.MeasureDivCd = drInsert["MeasureDivCd"].ConvertString_Null_To_Empty();  //측정구분
                        erd.MeasureTime = measure_Div != "" ? TxtMeasureTime.EditValue.ConvertString_Null_To_Empty() : "";    //측정시간
                        erd.X1 = drInsert["X1"].ConvertString_Null_To_Empty();              //검사값1
                        erd.X2 = drInsert["X2"].ConvertString_Null_To_Empty();              //검사값2
                        erd.X3 = drInsert["X3"].ConvertString_Null_To_Empty();              //검사값3
                        erd.X4 = drInsert["X4"].ConvertString_Null_To_Empty();              //검사값4
                        erd.X5 = drInsert["X5"].ConvertString_Null_To_Empty();              //검사값5
                        erd.X6 = drInsert["X6"].ConvertString_Null_To_Empty();              //검사값6
                        erd.PassDivCd = drInsert["PassDivCd"].ConvertString_Null_To_Empty();     //판정여부
                        erd.ActionMemo = drInsert["ActionMemo"].ConvertString_Null_To_Empty();     //조치내역
                        erd.Remark = drInsert["Remark"].ConvertString_Null_To_Empty();             //비고

                        //Update
                        if (!erd.UpdExamResultDaily())
                        {
                            //저장 중 에러 발생시

                            Success = false;
                            break;
                        }
                    }
                }

                //Save_Improve(erd.ExamNo);

                if (Success)
                {
                    MessageBox.Show("저장이 정상적으로 완료되었습니다.");
                }
                else
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Search_QC_List();
            }
        }

        private void Delete_Data()
		{
            try
            {
                DataTable dt = Gc_ExRst.DataSource as DataTable;

                for (int i = 0; i < dt.Rows.Count; i++)
				{
                    string ExamNo = dt.Rows[i]["ExamNo"].ConvertString_Null_To_Empty();

                    if(ExamNo != "")
					{
                        QM_ExamResultDaily ex = new QM_ExamResultDaily();
                        ex.ExamNo = ExamNo;
                        ex.ExamNoSeq = 0;
                        ex.ExamDivCd = this.Exam_Div;
                        ex.DelExamResultDaily();

                        //QT_ExamResult_Daily_Improve im = new QT_ExamResult_Daily_Improve();
                        //im.ExamNo = ExamNo;
                        //im.Del_QT_ExamResult_Daily_Improve_ExamNo();

                    }
				}

                MessageBox.Show("측정 데이터 삭제 완료");
                Search_QC_List();

            }
            catch
			{

			}

        }

        private string Create_No()
        {
            cls_Database cDB = new cls_Database();
            int No_Seq = cDB.Get_Last_NoSeq("QM_ExamResultDaily", "ExamNo", DtpExamDate.Text.ConvertDate_To_Char8());

            /*
             * InDate가 같은데이터가 있는가?
             *      Yes : Status = "신규"인 항목이 더 있는가?
             *          Yes : 그중 No가 입력된 Row이 있는가?
             *              Yes : 해당 Row 중 거래처가 dr["CustCode"]가 같은항목이 있는가?
             *                  Yes : dr[No] = drComp[No]
             *                  No : No_Seq = LastSeq
             *              No : No_Seq = LastSeq
             *          No : No_Seq = LastSeq
             *      No: No_Seq = LastSeq
            */

            return "QE" + DtpExamDate.Text.ConvertDate_To_Char8() + "-" + No_Seq.ToString("D4");
        }

        #endregion 데이터저장 End

        #region 밀시트 업로드, 다운로드

        private void Upload_MillSheet()
        {
            //DataRow drBf = (Gc_BfProc.MainView as GridView).GetFocusedDataRow();

            //if (drBf == null)
            //{
            //    return;
            //}



            ////검사실적 등록// 확인
            //QT_ExamResult qer = new QT_ExamResult();
            //qer.Ref_No = drBf["Ref_No"].ConvertString_Null_To_Empty();
            //qer.Ref_No_Seq = drBf["Ref_No_Seq"].ConvertInt_Null_To_Zero();
            //qer.Exam_Div_Cd = Exam_Div;

            //DataRow dataRow = qer.Select_ExamResult_Exsits();

            //if (dataRow == null)
            //{
            //    if (Database.DbName.Contains("KUMHO_ANSAN"))
            //    {
            //        cmdSave.PerformClick();
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("검사 실적이 없습니다. 실적 저장 후 등록 해주세요.");
            //        return;
            //    }
            //}

            //string ref_No = drBf["Ref_No"].ConvertString_Null_To_Empty();
            //int ref_No_Seq = drBf["Ref_No_Seq"].ConvertInt_Null_To_Zero();

            //Frm_MillSheet_pdf frm = new Frm_MillSheet_pdf(ref_No, ref_No_Seq, Exam_Div, dataRow);
            //frm.ShowDialog();

            ////밀시트 저장 후 재조회
            ////1. 선택한 row 저장
            //int SaveFocusedRowInx = (Gc_BfProc.MainView as GridView).FocusedRowHandle;

            ////2재조회
            //Search_IQC();

            ////3. 저장 후 선택한 row를 불러옴
            //(Gc_BfProc.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

		#endregion

        private void Print()
        {

   //         /* 10 : 수입검사
   //          * 20 : 출하검사
   //          * 30 : 공정검사
   //          * 40 : 초중종검사
   //          * 50 : 일상자주검사
   //          * 60 : 공정조건검사
   //          */

   //         GridControl gc = Gc_BfProc as GridControl;
   //         GridView gv = Gc_BfProc.MainView as GridView;

   //         DataRow dr = gv.GetFocusedDataRow();
   //         if (dr == null) return;

			//string rptNm = "";

			//if (Exam_Div == "10")   // 수입검사
			//{
			//	if (WrGlobal.ProcOption["QT"].ContainsKey("IQC_Report"))
			//	{
			//		rptNm = WrGlobal.ProcOption["QT"]["IQC_Report"];
			//	}
   //             //올덴 - 개발하는동안 임시로
   //             if(Database.DbName == "ALLTHEN")
   //             {
   //                 //자재기준정보에서 품목분류를 뒀으면 A양식, 안했으면 B양식 나오도록
   //                 if (dr["MaterialClass1"].ConvertString_Null_To_Empty() != "")
   //                 {
   //                     rptNm = "RptIQC_ALLTHEN_A";
   //                 }
   //                 else
   //                 {
   //                     rptNm = "RptIQC_ALLTHEN_B";
   //                 }
   //             }
			//}
			//if (Exam_Div == "20" || Exam_Div == "25") // 출하검사
   //         {
			//	if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_Report"))
			//	{
			//		rptNm = WrGlobal.ProcOption["QT"]["OQC_Report"];
			//	}
			//}

			//try
			//{
   //             // 현재 실행중인 Assembly 가져오기
   //             Assembly assembly = Assembly.GetExecutingAssembly();

   //             // reportName 과 일치하는 타입 찾기
   //             Type type = assembly.GetTypes()
   //                 .FirstOrDefault(t => t.Name.Equals(rptNm, StringComparison.OrdinalIgnoreCase)
   //                                   && typeof(XtraReport).IsAssignableFrom(t));
   //             if (type == null)
   //                 throw new Exception($"레포트를 찾을 수 없습니다.");

   //             // 인스턴스 생성
   //             XtraReport report = (XtraReport)Activator.CreateInstance(type, dr);

   //             using (ReportPrintTool reportTool = new ReportPrintTool(report))
   //             {
   //                 reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
   //                 reportTool.ShowPreviewDialog();
   //             }
   //         }
			//catch
			//{
   //             XtraMessageBox.Show("레포트를 찾을 수 없습니다.");
   //         }
		}

		private void Print2()
        {
            //GridView gv = Gc_BfProc.MainView as GridView;

            //DataRow dr = gv.GetFocusedDataRow();
            //if (dr == null) return;

            //string _Rst_No = dr["Ref_No"].ConvertString_Null_To_Empty();
            //int _Rst_Row_Seq = dr["Ref_No_Seq"].ConvertInt_Null_To_Zero();

            //if (Database.DbName.ToUpper().Contains("MYUNGJIN"))
            //{
            //    //공정이동표
            //    PD_ProcessResult Result = new PD_ProcessResult();
            //    DataTable dt = Result.Select_ProcessResult(WrGlobal.Corp_Cd, _Rst_No, _Rst_Row_Seq);

            //    Rpt_ProcessFlowChart_MyungJin report = new Rpt_ProcessFlowChart_MyungJin(dt);
            //    ReportPrintTool reportTool = new ReportPrintTool(report);

            //    reportTool.PrinterSettings.Collate = false;
            //    reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            //    reportTool.PreviewForm.PrintControl.ResumeLayout();

            //    reportTool.ShowPreviewDialog();
            //}
        }

        private void Print_InspectionStd()
        {
            //GridView gv = Gc_BfProc.MainView as GridView;
            //DataRow dr = gv.GetFocusedDataRow();
            //if (dr == null) return;

            //string itemCd = dr["Item_Cd"].ConvertString_Null_To_Empty();
            //string processCd = gv.Columns.ColumnByFieldName("Process_Cd") == null ? "" : dr["Process_Cd"].ConvertString_Null_To_Empty();
            //string exam_Div = Exam_Div;
            //if (exam_Div == "11") { exam_Div = "10"; }
            //if (exam_Div == "25" || exam_Div == "26") { exam_Div = "20"; }

            //FrmPictureEdit frm = new FrmPictureEdit(itemCd, "30", processCd, exam_Div, false, "N");  //30검사기준서
            //frm.ShowDialog();
        }

        private void TxtMeasureTime_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                TxtMeasureTime.SelectAll();
            }));
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(Gv_ExRst);
        }

        private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}

		private void Btn_HistoryRow_Edit_Click(object sender, EventArgs e)
		{
            //관리기준 이탈사항 추가/삭제
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
            }
            else
            {
            //삭제 진행인경우
                dic.Add("Status", Convert.ToString((GcImprove.MainView as GridView).GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(GcImprove, dic, txt_Act);
        }

  //      private void Search_Improve()
		//{
  //          QT_ExamResult_Daily_Improve im = new QT_ExamResult_Daily_Improve();
  //          im.Exam_Div_Cd = Exam_Div;
  //          im.Exam_Date = DtpExamDate.DateTime.ToString("d");

  //          GcImprove.DataSource =  im.Select_QT_ExRst_Improve();
  //          GvImprove.BestFitColumns();
  //      }

        private void Save_Improve(string examNo)
		{
   //         DataTable dt = GcImprove.DataSource as DataTable;

			//foreach (DataRow dr in dt.Rows)
			//{
   //             string status = dr["Status"].ConvertString_Null_To_Empty();

   //             QT_ExamResult_Daily_Improve im = new QT_ExamResult_Daily_Improve();
   //             im.Idx = dr["Idx"].ConvertDecimal_Null_To_Zero();
   //             im.Exam_Date = DtpExamDate.DateTime.ToString("d");
   //             im.Exam_Div_Cd = this.Exam_Div;
   //             im.Issue = dr["Issue"].ConvertString_Null_To_Empty();
   //             im.IssueEmp = dr["IssueEmp"].ConvertString_Null_To_Empty();
   //             im.ActionResult = dr["ActionResult"].ConvertString_Null_To_Empty();
   //             im.ActionEmpId = dr["ActionEmpId"].ConvertString_Null_To_Empty();
   //             im.ConfirmEmpId = dr["ConfirmEmpId"].ConvertString_Null_To_Empty();
   //             im.Remark = dr["Remark"].ConvertString_Null_To_Empty();
   //             im.ExamNo = examNo;
   //             //im.ExamNoSeq 

   //             if (status == "신규")
			//	{
   //                 im.Ins_QT_ExamResult_Daily_Improve();
			//	}
			//	else if (status == "수정")
			//	{
   //                 im.Upd_QT_ExamResult_Daily_Improve();
			//	}
			//	else if (status == "삭제")
			//	{
   //                 im.Del_QT_ExamResult_Daily_Improve();
			//	}
			//}

        }
	}
}