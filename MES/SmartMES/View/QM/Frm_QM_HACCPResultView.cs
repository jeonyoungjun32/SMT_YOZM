using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
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
	public partial class Frm_QM_HACCPResultView : DevExpress.XtraEditors.XtraForm
    {
        /*검사수량, LOTNo, 판정일자 추가된 화면*/
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();

        clsRadioGroup clsRadio = new clsRadioGroup();
        cls_Lookup clsLup = new cls_Lookup();

        CM_CateItem cm = new CM_CateItem();

        private List<string> ProcessCds = new List<string>();   //특정 공정만 조회할 수 있도록

        RepositoryItemLookUpEdit rpoPass = new RepositoryItemLookUpEdit();
        RepositoryItemTextEdit rpoNum = new RepositoryItemTextEdit();
        RepositoryItemTextEdit rpoTextFree = new RepositoryItemTextEdit();
        RepositoryItemTextEdit rpoDateEdit = new RepositoryItemTextEdit();
        RepositoryItemTextEdit rpoReadOnly = new RepositoryItemTextEdit();

        public Frm_QM_HACCPResultView()
        {
            InitializeComponent();

            SetDesign();
        }

        public Frm_QM_HACCPResultView(string ccpDiv, string targetDiv)
        {
            InitializeComponent();

            SetDesign();

            Lup_CCP_Div.EditValue = ccpDiv;
            PnlCCPDiv.Visible = false;
            PnlTargetDiv.Visible = false;
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            Set_Rpo();

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

		#region GridView 셋팅

		private void Set_GridView()
		{
			SetGrid_ExRst(Gv_ExRst);
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
            grid.Create_GvColumn(gv, "검사일자", "ExamDate");
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
            grid.Create_GvColumn(gv, "검사자", "ExamEmpName");
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

            grid.SetCol_Type(gv.Columns["MgntMin"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["MgntMax"], clsGrid.TypeList.TFloat);
            gv.Columns["ResultValue"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["PassDivCd"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ExamEmpName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

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
            Search_QCList();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton Btn = (smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdInspectionRpt": Print(); break;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search_QCList();
        }

        private void Search_QCList()
        {
            string ccpDivCd = Lup_CCP_Div.EditValue.ConvertString_Null_To_Empty();
            string tarGetDivCd = Lup_Target_Div.EditValue.ConvertString_Null_To_Empty();

            QM_HaccpExamResult qer = new QM_HaccpExamResult();

            qer.CcpDivCd = ccpDivCd;           //검사 구분
            qer.TargetDivCd = tarGetDivCd;           //검사 구분

            Gc_ExRst.DataSource = qer.Select_QM_HaccpExamResult();

            Gv_ExRst.BestFitColumns();
        }

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