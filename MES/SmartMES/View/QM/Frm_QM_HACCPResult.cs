using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.PM;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
	public partial class Frm_QM_HACCPResult : DevExpress.XtraEditors.XtraForm
    {
        /*검사수량, LOTNo, 판정일자 추가된 화면*/
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();

        clsRadioGroup clsRadio = new clsRadioGroup();
        cls_Lookup clsLup = new cls_Lookup();

        CM_CateItem cm = new CM_CateItem();

        private string[] CustDiv = { "05", "10", "20", "30", "40", "50" }; //거래처구분 리스트(05 : 매입/매출, 10 : 매입, 20 : 매출, 30 : 외주, 40 : 납품)
        private string[] ItemDiv = { "40", "50", "60" }; //품목구분 리스트(10 : 상품, 20 : 제품, 30 : 반제품, 40 : 원자재, 50 : 부자재, 60 : 소모자재)
        private List<string> ProcessCds = new List<string>();   //특정 공정만 조회할 수 있도록

        public string g_Rst_No = "";
        public int G_Rst_Row_Seq = 0;

        string Default_Set_Yn = "Y"; //검사 디폴트 값 셋팅 여부

        RepositoryItemLookUpEdit rpoPass = new RepositoryItemLookUpEdit();
        RepositoryItemTextEdit rpoNum = new RepositoryItemTextEdit();
        RepositoryItemTextEdit rpoTextFree = new RepositoryItemTextEdit();
        RepositoryItemTextEdit rpoDateEdit = new RepositoryItemTextEdit();
        RepositoryItemTextEdit rpoReadOnly = new RepositoryItemTextEdit();

        public Frm_QM_HACCPResult(string ccpDiv, string targetDiv)
        {
            InitializeComponent();

            SetDesign();

            Lup_CCP_Div.EditValue = ccpDiv;
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;
            DtpExamDate.EditValue = DateTime.Now;

            Set_Rpo();

            //검사구분에따른 설정
            Set_TypeOption();

            //업체별 옵션
            Set_CompanyOption();

            //GridView 설정
            Set_GridView();

            Set_Lup();
        }

        private void Set_Rpo()
		{
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("코드"), new DataColumn("명칭") });
            dt.Rows.Add("Y", "OK");
            dt.Rows.Add("N", "NG");

            rpoPass = rpo.Create_RepositoryItem(dt, "코드", "명칭");
            rpoNum = new RepositoryItemTextEdit()
            {
                Mask = { MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric }
            };

            rpoTextFree = new RepositoryItemTextEdit
            {
                //Mask = { MaskType = DevExpress.XtraEditors.Mask.MaskType.None }
            };

            rpoDateEdit = new RepositoryItemTextEdit()
            {
                Mask = {
                    MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret,
                    EditMask = "yyyy-MM-dd HH:mm",
                    UseMaskAsDisplayFormat = true
                }
            };

			rpoDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			rpoDateEdit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
			//rpoDateEdit.EditValueChanged += RpoDateEdit_EditValueChanged;

			rpoReadOnly = new RepositoryItemTextEdit
            {
                ReadOnly = true,
                Enabled = false,
                AllowFocused = false,
                Appearance = { BackColor = smtDesignSetting.ReadOnlyColor }
            };
        }

		private void Set_CompanyOption()
        {
            cmdInspectStd.Visible = false; //검사기준서
        }

		private void Set_TypeOption()
		{
			Pnl_Proc.Text = "생산내역";
			ItemDiv = new string[] { "20", "30" };

			Default_Set_Yn = "N";

			//조회조건
			LblLine.Visible = true;
			LupLine.Visible = true;

			LblProcess.Visible = true;
			LupProcess.Visible = true;

			LblMachine.Visible = true;
			LupMachine.Visible = true;

			LblItem.Visible = true;
			LupItem.Visible = true;

		}

		#region GridView 셋팅

		private void Set_GridView()
		{
			SetGrid_PrQC(Gv_PrQC);
			SetGrid_ExRst(Gv_ExRst);
		}

		private void SetGrid_PrQC(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //품목분류1
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", null), "코드", "명칭");

            grid.Create_GvColumn(gv, "검사여부", "ExamYn");

            grid.Create_GvColumn(gv, "실적번호", "RefNo");
            grid.Create_GvColumn(gv, "실적순번", "RefSeq");

            grid.Create_GvColumn(gv, "생산일자", "WorkDate");

            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "라인명", "LineName");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "생산량", "WorkQty");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ItemName"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.Columns["ExamYn"].MaxWidth = 80;

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;
            if (gv.Columns["ItemClass1"] != null) { gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1; }
            if (gv.Columns["ItemClass2"] != null) { gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2; }
            if (gv.Columns["ItemClass3"] != null) { gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3; }

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            //if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["RefNo"].Visible = false;
                gv.Columns["RefSeq"].Visible = false;
                gv.Columns["LineCode"].Visible = false;
                gv.Columns["ProcessCode"].Visible = false;
                gv.Columns["MachineCode"].Visible = false;
                gv.Columns["ItemCode"].Visible = false;

                gv.Columns["CreId"].Visible = false;
                gv.Columns["CreIP"].Visible = false;
                gv.Columns["CreDt"].Visible = false;
                gv.Columns["ModId"].Visible = false;
                gv.Columns["ModIP"].Visible = false;
                gv.Columns["ModDt"].Visible = false;
            }
        }

        private void GvBfProc_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            Search_QCList();
        }


        private void SetGrid_ExRst(GridView gv)
        {
            //값 측정검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //헤더 멀티라인 설정
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            //검사수량
            grid.Create_GvColumn(gv, "검사코드", "ExamCd");
            grid.Create_GvColumn(gv, "검사명", "ExamNm");
            grid.Create_GvColumn(gv, "검사규격", "Spec");
            grid.Create_GvColumn(gv, "검사방법", "Method");
            grid.Create_GvColumn(gv, "검사단위", "Unit");
            grid.Create_GvColumn(gv, "관리범위<br>하한", "MgntMin");
            grid.Create_GvColumn(gv, "관리범위<br>상한", "MgntMax");

            grid.Create_GvColumn(gv, "결과값", "ResultValue", rpoTextFree);

            grid.Create_GvColumn(gv, "판정", "PassDivCd");
            grid.Create_GvColumn(gv, "조치내역", "ActionMemo");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.Create_GvColumn(gv, "검사번호", "ExamNo");
            grid.Create_GvColumn(gv, "검사순번", "ExamSeq");

            //등록자, 수정자 column 숨김
            gv.Columns["ExamCd"].Visible = false;
            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModDt"].Visible = false;

            //Repository 셋팅
            gv.Columns["PassDivCd"].ColumnEdit = rpo.Create_RepositoryItem(code.GetCM_CateItem(WrGlobal.CorpID, "Q020"), "코드", "명칭");

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ResultValue"]);

            grid.Column_AllowEdit(gv.Columns["PassDivCd"]);
            grid.Column_AllowEdit(gv.Columns["ActionMemo"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            grid.SetCol_Type(gv.Columns["MgntMin"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["MgntMax"], clsGrid.TypeList.TFloat);

            //이벤트
            gv.CellValueChanged += Event_CellValueChagned_Num; ;    //셀값 수정 Event
            //gv.RowCellClick += Event_CellClick;
            gv.CustomDrawCell += Event_Cell_ForeColor;   //불합격이면 글자색 빨강
            gv.CustomRowCellEditForEditing += Gv_CustomRowCellEditForEditing; //추후 테스트 후 RowCellEdit 만 이벤트 처리하고 이 이벤트는 삭제 예정
            //gv.CustomRowCellEdit += Gv_CustomRowCellEdit;
            gv.RowCellStyle += Gv_RowCellStyle;


            if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["ExamNo"].Visible = false;
                gv.Columns["ExamSeq"].Visible = false;
            }
        }


        private void Event_CellClick(object sender, RowCellClickEventArgs e)
        {
   //         if (e.Column.FieldName == "ResultValue")
			//{
   //             GridView gv = sender as GridView;
   //             DataRow dr = gv.GetDataRow(e.RowHandle);
   //             string valueType = dr["ValueTypeDivCd"].ConvertString_Null_To_Empty();
   //             if (valueType != "10") return;


   //             string currentValue = Convert.ToString(dr[e.Column.FieldName]);

   //             string newValue = (currentValue == "Y") ? "N" : "Y";

   //             gv.SetFocusedRowCellValue(e.Column.FieldName, newValue);

   //             gv.RefreshData();
			//}
        }

        private void Event_Cell_ForeColor(object sender, RowCellCustomDrawEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle < 0) return;

            string fieldName = e.Column.FieldName;

            if (fieldName == "ResultValue")
            {
                string value = gv.GetRowCellValue(e.RowHandle, fieldName)?.ToString();
                string valueType = gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd")?.ToString();

                if(valueType == "10") //합불일때
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
        private void Gv_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
			GridView gv = sender as GridView;

			string fieldName = e.Column.FieldName;

			if (fieldName == "ResultValue")
			{
                string valueType = gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd").ConvertString_Null_To_Empty();
                //string itemCode = gv.GetRowCellValue(e.RowHandle, "ItemCode")?.ToString();


                // 정성 값이면 Lookup으로 보여줌
                if (valueType == "10")
                {
                    e.RepositoryItem = rpoPass;
                }
                else if (valueType == "20")
                {
                    e.RepositoryItem = rpoNum;

                }
                else if (valueType == "30")
                {
                    e.RepositoryItem = rpoTextFree;
                }
                else if (valueType == "40")
                {
                    e.RepositoryItem = rpoDateEdit;
                }
                else
                {
                    e.RepositoryItem = rpoTextFree;
                }
            }
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //GridView gv = sender as GridView;
            //string fieldNm = e.Column.FieldName;

            //int examCnt = gv.GetRowCellValue(e.RowHandle, "Exam_Cnt").ConvertInt_Null_To_Zero();
            //string itemCode = gv.GetRowCellValue(e.RowHandle, "ItemCode").ConvertString_Null_To_Empty();

            //// "ResultValue"~"X6" 컬럼만 처리
            //if (e.Column.FieldName.StartsWith("X"))
            //{

            //    if (int.TryParse(e.Column.FieldName.Substring(1), out int xIndex))
            //    {
            //        // X컬럼 번호가 exam_cnt보다 크면 → 편집불가 셀 → 배경색 회색
            //        if (xIndex > examCnt)
            //        {
            //            e.Appearance.BackColor = smtDesignSetting.ReadOnlyColor;
            //            e.Appearance.ForeColor = Color.DarkGray;
            //        }
            //        else
            //        {
            //            // 편집 가능한 셀 색상 (선택사항)
            //            e.Appearance.BackColor = smtDesignSetting.MustColor;
            //            e.Appearance.ForeColor = Color.Black;
            //        }
            //    }
            //}
        }

        private void Gv_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            GridView gv = sender as GridView;

            string fieldName = e.Column.FieldName;

            string itemCode = gv.GetRowCellValue(e.RowHandle, "ItemCode")?.ToString();
            string valueType = gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd").ConvertString_Null_To_Empty();
            //string examCntStr = gv.GetRowCellValue(e.RowHandle, "Exam_Cnt").ConvertString_Null_To_Empty();

            // 기본 설정: 정성/정량에 따라 편집기 지정
            // 정성 값이면 Lookup으로 보여줌
            if(fieldName == "ResultValue")
			{
				if (valueType == "10")
				{
					e.RepositoryItem = rpoPass;
				}
				else if (valueType == "20")
				{
					e.RepositoryItem = rpoNum;
				}
				else if (valueType == "30")
				{
					e.RepositoryItem = rpoTextFree;
				}
				else if (valueType == "40")
				{
					e.RepositoryItem = rpoDateEdit;
				}
			}

            //// 시험 횟수 이상 컬럼은 편집 불가
            //if (int.TryParse(examCntStr, out int examCnt) && int.TryParse(fieldName.Substring(1), out int xIndex))
            //{
            //    if (xIndex > examCnt)
            //        e.RepositoryItem = rpoReadOnly;
            //}
            //else
            //{
            //    e.RepositoryItem = rpoReadOnly; // examCnt가 비정상일 경우 모두 비활성
            //}
        }

        private void Event_CellValueChagned_Num(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            string PassDivCd = "10";

            //검사값이 관리범위를 벗어났는지 검사
            if(fieldNm == "ResultValue")
            {
                string value = dr["ResultValue"].ConvertString_Null_To_Empty();
                string valueType = dr["ValueTypeDivCd"].ConvertString_Null_To_Empty();

                if(valueType == "20")//숫자일때
                { 
                  if(!Chk_Num(dr, value)) PassDivCd = "20"; 
                    gv.SetFocusedRowCellValue("PassDivCd", PassDivCd);
                }
                else if(valueType == "40")//날짜
				{
                    DateTime date = new DateTime();

                    if(DateTime.TryParse(value, out date))
					{
                        dr[fieldNm] = date.ToString("yyyy-MM-dd HH:mm");
					}
				}
            }

            gv.BestFitColumns();
        }

        private bool Chk_Num(DataRow dr, string value)
        {
            bool result = true;

            decimal xValue = 0;

            if (!decimal.TryParse(value, out xValue)) return true; //값이 숫자가 아닐경우 true 처리

            decimal MgntMin = dr["MgntMin"].ConvertDecimal_Null_To_Zero();
            decimal MgntMax = dr["MgntMax"].ConvertDecimal_Null_To_Zero();
            //decimal DangerMin = dr["DangerMin"].ConvertDecimal_Null_To_Zero();
            //decimal DangerMax = dr["DangerMax"].ConvertDecimal_Null_To_Zero();
            xValue = value.ConvertDecimal_Null_To_Zero();

            //관리범위 오버 검사
            //상한만 있을때
            if (MgntMin == 0 && MgntMax > 0)
            {
                if (xValue > MgntMax)
                {
                    //값이 상한보다 높으면 불합 처리
                    result = false;
                }
            }
            //하한만 있을때
            else if (MgntMin > 0 && MgntMax == 0)
            {
                if (xValue < MgntMin)
                {
                    //값이 하한보다 낮으면 불합 처리
                    result = false;
                }
            }
            //상,하한 둘다 있을때
            else if (MgntMin > 0 && MgntMin < MgntMax)
            {
                if (xValue < MgntMin || xValue > MgntMax)
                {
                    //값이 하한보다 낮거나 상한보다 높으면 불합 처리
                    result = false;
                }
            }

            ////위험범위 오버 검사
            ////상한만 있을때
            //if (DangerMin == 0 && DangerMax > 0)
            //{
            //    if (xValue > DangerMax)
            //    {
            //        //값이 상한보다 높으면 불합 처리
            //        result = false;
            //    }
            //}
            ////하한만 있을때
            //else if (DangerMin > 0 && DangerMax == 0)
            //{
            //    if (xValue < DangerMin)
            //    {
            //        //값이 하한보다 낮으면 불합 처리
            //        result = false;
            //    }
            //}
            ////상,하한 둘다 있을때
            //else if (DangerMin > 0 && DangerMin < DangerMax)
            //{
            //    if (xValue < DangerMin || xValue > DangerMax)
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

            //1. 조회조건 : 라인, 공정, 설비, 품목

            //라인
            CM_Line line = new CM_Line();
            DataTable dt_line = code.GetLine_List("", true);

            clsLup.Create_RepositoryItemSearchLookUp(LupLine, dt_line, "라인코드", "라인명"); ;
            LupLine.EditValue = "ALL";

            //공정
            CM_Process process = new CM_Process();
            DataTable dt_process = null;

            if (ProcessCds.Count > 0)
            {
                dt_process = code.SelectCM_Process_ByCode(true, ProcessCds.ToArray());
			}
			else
			{
                dt_process = code.GetProcess_List_ProcessType(true, null);
            }

            clsLup.Create_RepositoryItemSearchLookUp(LupProcess, dt_process, "공정코드", "공정명");
            LupProcess.EditValue = "ALL";

            //설비
            DataTable dt_machine = code.GetMachineList(true);
            clsLup.Create_RepositoryItemSearchLookUp(LupMachine, dt_machine, "설비코드", "설비명");
            LupMachine.EditValue = "ALL";

            //품목
            DataTable dt_item = code.GetMaterialList(ItemDiv, true).Table;
            clsLup.Create_RepositoryItemSearchLookUp(LupItem, dt_item, "품목코드", "품명");
            //grid.SetLupView_Item(LupItem, "제품코드 초기화");
            LupItem.EditValue = "ALL";
            LupItem.Properties.PopupFormSize = new System.Drawing.Size(1000, 0);

            //거래처
            DataTable dt_Cust = code.GetCustomerList(CustDiv, true);
            clsLup.Create_RepositoryItemSearchLookUp(LupCust, dt_Cust, "거래처코드", "거래처명");
            LupCust.EditValue = "ALL";

            //검사자
            string[] deptId = null;// code.GetCM_CateItemCode(WrGlobal.CorpID, "Q003");
            //if (deptId.Length < 1) deptId = null; //공통코드에 검사부서 등록하여 불러오기, 없으면 전체 사용자 조회하도록

            DataTable dtUser = code.GetDataTableUser(false, deptId, "N");
            clsLup.Create_RepositoryItemSearchLookUp(LupExamEmpId, dtUser, "코드", "명칭");

            //
            //CCP 구분
            DataTable dt_CCP = code.GetCategory_HighClass("F010");
            clsLup.Create_RepositoryItemSearchLookUp(Lup_CCP_Div, dt_CCP, "코드", "명칭");

			Lup_CCP_Div.EditValueChanged += Lup_CCP_Div_EditValueChanged;

            if(dt_CCP != null & dt_CCP.Rows.Count > 0)
			{
                Lup_CCP_Div.EditValue = dt_CCP.Rows[0]["코드"];
            }

            ////관리대상
            //DataTable dt_Target = code.GetCategory_HighClass(Lup_CCP_Div.EditValue.ConvertString_Null_To_Empty());
            //clsLup.Create_RepositoryItemSearchLookUp(Lup_Target_Div, dt_Target, "코드", "명칭");
            
            //if (dt_Target != null & dt_Target.Rows.Count > 0)
            //{
            //    Lup_Target_Div.EditValue = dt_Target.Rows[0]["코드"];
            //}
        }

		private void Lup_CCP_Div_EditValueChanged(object sender, EventArgs e)
		{
            //관리대상 재설정
            DataTable dt_Target = code.GetCategory_HighClass(Lup_CCP_Div.EditValue.ConvertString_Null_To_Empty());
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Target_Div, dt_Target, "코드", "명칭");

            if (dt_Target != null & dt_Target.Rows.Count > 0)
            {
                Lup_Target_Div.EditValue = dt_Target.Rows[0]["코드"];
            }
        }

        private void Lup_CCP_Div_EditValueChanged_1(object sender, EventArgs e)
        {
            Search_QCList();
        }

        #endregion

        private void Frm_BN_Order_Load(object sender, EventArgs e)
        {
            Search_BfProc();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton Btn = (smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdSave": Save_Data(); break;
                case "cmdDelete": Delete_Data(); break;
                case "cmdInspectionRpt": Print(); break;
            }
        }

        #region 데이터 조회

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            g_Rst_No = "";
            G_Rst_Row_Seq = 0;
            Search_BfProc();
        }

        public void Search_BfProc()
        {
            this.Validate();

            Search_OrderResult();

            DataTable dt = Gc_BfProc.DataSource as DataTable;

            if(dt == null)
            {
                Gc_ExRst.DataSource = null;
			}
			else
			{
                if(dt.Rows.Count < 1)
                {
                    Gc_ExRst.DataSource = null;
                }
			}
        }

        private void Search_OrderResult ()
        {
            GridView gv = Gc_BfProc.MainView as GridView;
            int idx = gv.FocusedRowHandle;

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char8();

            string line_Cd = LupLine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupLine.EditValue.ConvertString_Null_To_Empty();
            string ProcessCode = LupProcess.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupProcess.EditValue.ConvertString_Null_To_Empty();
            string MachineCode = LupMachine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupMachine.EditValue.ConvertString_Null_To_Empty() ;
            string ItemCode = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();

            PM_OrderResult or = new PM_OrderResult();
            or.LineCode = line_Cd;
            or.ProcessCode = ProcessCode;
            or.MachineCode = MachineCode;
            or.ItemCode = ItemCode;

            DataTable dt_ProcResult = null;

            if (g_Rst_No == "" && G_Rst_Row_Seq == 0)
            {
                dt_ProcResult = or.Select_PM_OrderResult_Exam("", startDate, endDate,  null, ProcessCds);
            }
            else
            {
                ////현장시스템에서 로드 시 해당 실적 건만 조회
                or.SerialNo = g_Rst_No;

				dt_ProcResult = or.Select_PM_OrderResult_Exam("");
			}

            Gc_BfProc.DataSource = dt_ProcResult;

            gv.FocusedRowHandle = idx;
            gv.BestFitColumns();

            //데이터 없을시 정성, 정량 검사항목 DataSource도 Clear
            if(dt_ProcResult == null)
            {
                Gc_ExRst.DataSource = null;
            }
            else
            {
                if(dt_ProcResult.Rows.Count < 1)
                {
                    Gc_ExRst.DataSource = null;
                }
            }
        }

        //검사내역 조회
        private void Search_QCList()
        {
            Search_QC_List();
            Set_ExamDate();
        }

        private void Search_QC_List()
        {
            //검사목록 불러오기
            GridView gv = Gc_BfProc.MainView as GridView;
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

			//측정구분
			string ccpDivCd = Lup_CCP_Div.EditValue.ConvertString_Null_To_Empty();
			string tarGetDivCd = Lup_Target_Div.EditValue.ConvertString_Null_To_Empty();

			QM_HaccpExamResult qer = new QM_HaccpExamResult();

			qer.RefNo = Convert.ToString(dr["RefNo"]);
			qer.RefSeq = dr["RefSeq"].ConvertInt_Null_To_Zero();
			qer.CcpDivCd = ccpDivCd;           //검사 구분
			qer.TargetDivCd = tarGetDivCd;           //검사 구분

			DataTable dtExam = qer.Select_QM_HaccpExamResult();

            //검사한 항목일경우
            if (dtExam != null && dtExam.Rows.Count > 0)
			{
				Gc_ExRst.DataSource = dtExam;
			}
			else
			{
				//미검사 항목일 경우
				QM_HaccpStd iem = new QM_HaccpStd();
				iem.CcpDivCd = ccpDivCd;
				iem.TargetDivCd = tarGetDivCd;
				iem.ItemClassCd = dr["ItemClass1"].ConvertString_Null_To_Empty(); //***
				Gc_ExRst.DataSource = iem.Select_QM_HaccpStd();

				if (Default_Set_Yn == "Y")
				{
					SetDefaultValue();
				}
			}

		Gv_ExRst.BestFitColumns();
        }

        private void SetDefaultValue()
        {
            DataTable dtNum = Gc_ExRst.DataSource as DataTable;

            //정량일 경우 해당 검사 규격의 랜덤값으로 설정
            if (dtNum != null)
            {
                if (dtNum.Rows.Count > 0)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());

                    foreach (DataRow dr in dtNum.Rows)
                    {
                        string valueType = dr["ValueTypeDivCd"].ConvertString_Null_To_Empty();

                        if(valueType == "20") //숫자
						{
                            double minValue = dr["MgntMin"].ConvertDouble_Null_To_Zero();
                            double maxValue = dr["MgntMax"].ConvertDouble_Null_To_Zero();
                            double defaultValue = dr["DefaultValue"].ConvertDouble_Null_To_Zero();
                            int DecimalCnt = dr["DecimalCnt"].ConvertInt_Null_To_Zero();


                            if (minValue == 0 && maxValue == 0 && defaultValue == 0) continue;

                            if (minValue == 0 && maxValue == 0)
                            {
                                if (defaultValue != 0)
                                {
                                    minValue = defaultValue;
                                    maxValue = defaultValue;
                                }
                            }
                            else if (minValue == 0)
                            {
                                if (maxValue != 0)
                                {
                                    minValue = maxValue - 3;
                                }
                                else if (defaultValue != 0)
                                {
                                    minValue = defaultValue - 3;
                                }
                            }
                            else if (maxValue == 0)
                            {
                                if (minValue != 0)
                                {
                                    maxValue = minValue + 3;
                                }
                                else if (defaultValue != 0)
                                {
                                    maxValue = defaultValue + 3;
                                }
                            }

                            double randValue = 0;

                            randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), DecimalCnt);
                            dr["ResultValue"] = randValue;
                        }
                        else
						{
                            dr["ResultValue"] = dr["DefaultValue"];
                        }
                    }
                }
            }
        }

        private void Set_ExamDate()
		{
             GridView gv = Gc_BfProc.MainView as GridView;
            DataRow drBf = gv.GetFocusedDataRow();

            //검사일자 바인딩
            DataTable dtSave = Gc_ExRst.DataSource as DataTable;

            if (dtSave == null)
            {
                DtpExamDate.EditValue = DateTime.Now.Date;
                LupExamEmpId.EditValue = null;
                return;
            }

            if (dtSave.Rows.Count > 0)
            {
                DataRow dr = dtSave.Rows[0];

                if (dr["ExamNo"].ConvertString_Null_To_Empty() != "") //측정 O
                {
                    DtpExamDate.EditValue = dr["ExamDate"];        //검사일자
                    LupExamEmpId.EditValue = dr["ExamEmpId"];       //검사자
                }
                else
                {
                    DtpExamDate.EditValue = DateTime.Now.Date;  //검사일자
                    LupExamEmpId.EditValue = null;              //검사자
                }
			}
			else
			{
                DtpExamDate.EditValue = DateTime.Now.Date;
                LupExamEmpId.EditValue = null;
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

				DataRow drBf = (Gc_BfProc.MainView as GridView).GetFocusedDataRow();

                QM_HaccpExamResult qer = new QM_HaccpExamResult();
				qer.ExamDate = DtpExamDate.Text.ConvertDate_To_Char10();                   //검사일자
				//qer.ItemCode = drBf["ItemCode"].ConvertString_Null_To_Empty();    //품목
				qer.CcpDivCd = Lup_CCP_Div.EditValue.ConvertString_Null_To_Empty();                                              //검사구분
				qer.TargetDivCd = Lup_Target_Div.EditValue.ConvertString_Null_To_Empty();                                              //검사구분
				qer.RefNo = drBf["RefNo"].ConvertString_Null_To_Empty();    //참조번호
				qer.RefSeq = drBf["RefSeq"].ConvertInt_Null_To_Zero();        //참조순번
				qer.ExamEmpId = LupExamEmpId.EditValue.ConvertString_Null_To_Empty();        //검사자
				//qer.LotNo = TxtLotNo.EditValue.ConvertString_Null_To_Empty();        //LotNo

				//측정구분


				bool Success = true;
				if (ExamNo == "")
				{
					//신규등록
					//측정 구분에따른 추가 신규입력 구분
					DataRow drExists = qer.Select_ExamResult_Exsits();

					int idx = 0;
					if (drExists == null)
					{
						//측정구분이 다른항목이 입력된적 없는경우
						qer.ExamNo = Create_No();
					}
					else
					{
						//측정구분이 다른항목이 입력된적 있는경우
						qer.ExamNo = drExists["ExamNo"].ConvertString_Null_To_Empty();
						idx = drExists["ExamSeq"].ConvertInt_Null_To_Zero();
					}

					//데이터 저장
					foreach (DataRow drInsert in dtSave.Rows)
					{
						qer.ExamSeq = idx + 1;                                                   //검사순번
						qer.DispSeq = qer.ExamSeq;                                           //표시순번
						qer.ExamCd = drInsert["ExamCd"].ConvertString_Null_To_Empty();         //검사코드
						qer.ResultValue = drInsert["ResultValue"].ConvertString_Null_To_Empty();              //검사값1
						qer.PassDivCd = drInsert["PassDivCd"].ConvertString_Null_To_Empty();     //판정여부
						qer.ActionMemo = drInsert["ActionMemo"].ConvertString_Null_To_Empty();     //조치내역
						qer.Remark = drInsert["Remark"].ConvertString_Null_To_Empty();             //비고

						//Insert
						if (!qer.Ins_QM_HaccpExamResult())
						{
							//저장 중 에러 발생시
							qer.Del_QM_HaccpExamResult();

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
						qer.ExamNo = drInsert["ExamNo"].ConvertString_Null_To_Empty();         //검사번호
						qer.ExamSeq = drInsert["ExamSeq"].ConvertInt_Null_To_Zero();         //검사순번
						qer.DispSeq = qer.ExamSeq;                                           //표시순번
						qer.ExamCd = drInsert["ExamCd"].ConvertString_Null_To_Empty();         //검사코드
						qer.ResultValue = drInsert["ResultValue"].ConvertString_Null_To_Empty();              //검사값1
						qer.PassDivCd = drInsert["PassDivCd"].ConvertString_Null_To_Empty();     //판정여부
						qer.ActionMemo = drInsert["ActionMemo"].ConvertString_Null_To_Empty();     //조치내역
						qer.Remark = drInsert["Remark"].ConvertString_Null_To_Empty();             //비고

						//Update
						if (!qer.Upd_QM_HaccpExamResult())
						{
							//저장 중 에러 발생시

							Success = false;
							break;
						}
					}
				}

				if (Success)
				{
					//3. 선택한 row 저장
					int SaveFocusedRowInx = (Gc_BfProc.MainView as GridView).FocusedRowHandle;

					MessageBox.Show("저장이 정상적으로 완료되었습니다.");

					Search_BfProc();
					//5. 저장 후 선택한 row를 불러옴
					(Gc_BfProc.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
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
		}

        private void Delete_Data()
		{
            try
            {
                DataTable dt = Gc_ExRst.DataSource as DataTable;


                for (int i = 0; i < dt.Rows.Count; i++)
				{
                    string examNo = dt.Rows[i]["ExamNo"].ConvertString_Null_To_Empty();
                    int examSeq = dt.Rows[i]["ExamSeq"].ConvertInt_Null_To_Zero();

                    if(examNo != "")
					{
                        QM_HaccpExamResult ex = new QM_HaccpExamResult();
                        ex.ExamNo = examNo;
                        ex.ExamSeq = examSeq;
                        ex.Del_QM_HaccpExamResult();
					}
				}

                MessageBox.Show("데이터 삭제 완료");

                Search_BfProc();
            }
            catch
			{

			}

        }

        private string Create_No()
        {
            cls_Database cDB = new cls_Database();
            int No_Seq = cDB.Get_Last_NoSeq("QM_HaccpExamResult", "ExamNo", DtpExamDate.Text.ConvertDate_To_Char8());

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
   //         if (dr == null || dr["ExamYn"].ConvertString_Null_To_Empty() == "N") return;

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
   //                 if (dr["ItemClass1"].ConvertString_Null_To_Empty() != "")
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

   //             if (Database.DbName.ToUpper().Contains("UAGHITEC"))
   //             {
   //                 //거래처별로 검사성적서 차별화 (삼천리기계 : v1, FLOWBUS : v2, 에스피지 : v3)
   //                 if (dr["BillToCode"].ConvertString_Null_To_Empty() == "C000008")
   //                 {
   //                     rptNm = "RptOQC_v1";
   //                 }
   //                 else if (dr["BillToCode"].ConvertString_Null_To_Empty() == "C000043")
   //                 {
   //                     rptNm = "RptOQC_v2";
   //                 }
   //                 else if (dr["BillToCode"].ConvertString_Null_To_Empty() == "C000005")
   //                 {
   //                     rptNm = "RptOQC_v3";
   //                 }
   //                 else
   //                 {
   //                     rptNm = "RptOQC_UAG";
   //                 }
   //             }
   //         }

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

   //             if (Database.DbName.Contains("TPG") && this.Exam_Div == "25")   //티피지 출하검사에서 레포트 디자이너로 오픔
   //             {
   //                 Frm_RptDesingner f = new Frm_RptDesingner(report);
   //                 f.ShowDialog();
   //             }
   //             else
   //             {
   //                 using (ReportPrintTool reportTool = new ReportPrintTool(report))
   //                 {
   //                     reportTool.PreviewForm.PrintControl.Zoom = 0.9f;

   //                     reportTool.ShowPreviewDialog();
   //                 }
   //             }
   //         }
			//catch
			//{
   //             XtraMessageBox.Show("레포트를 찾을 수 없습니다.");
   //         }
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}