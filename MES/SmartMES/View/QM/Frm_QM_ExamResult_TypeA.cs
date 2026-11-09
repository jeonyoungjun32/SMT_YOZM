using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SmartMES.Models.MM;
using SmartMES.Models.PM;
using SmartMES.Models.QM;
using SmartMES.View.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
	public partial class Frm_QM_ExamResult_TypeA : DevExpress.XtraEditors.XtraForm
    {
        /*검사수량, LOTNo, 판정일자 추가된 화면*/
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();

        private string[] CustDiv = { "05", "10", "20", "30", "40", "50" }; //거래처구분 리스트(05 : 매입/매출, 10 : 매입, 20 : 매출, 30 : 외주, 40 : 납품)
        private string[] ItemDiv = { "40", "50", "60" }; //품목구분 리스트(10 : 상품, 20 : 제품, 30 : 반제품, 40 : 원자재, 50 : 부자재, 60 : 소모자재)
        private List<string> ProcessCds = new List<string>();   //특정 공정만 조회할 수 있도록

        /* 10 : 수입검사
         * 20 : 출하검사
         * 30 : 공정검사
         * 40 : 초중종검사
         * 50 : 일상자주검사
         * 60 : 공정조건검사
         */

        private string Exam_Div = string.Empty; //검사구분

        public string g_Rst_No = "";
        public int G_Rst_Row_Seq = 0;

        int ExamCnt = Convert.ToInt16(WrGlobal.ProcOption["QT"]["ExamCnt"]);   //검사수량
        string Default_Set_Yn = "Y"; //검사 디폴트 값 셋팅 여부

        public Frm_QM_ExamResult_TypeA(string exam_Div)
        {
            InitializeComponent();
            switch (exam_Div)
            {
                case "InQC":      Exam_Div = "10"; break;  //수입검사
                //case "OutQC":     Exam_Div = "20"; break;  //출하검사 : 생산실적기반
                case "OutQC_Out": Exam_Div = "25"; break;  //출하검사 : 출하내역기반
                case "PrQC":      Exam_Div = "30"; break;  //공정검사
                //case "FML":       Exam_Div = "40"; break;  //초중종검사
                case "DSC":       Exam_Div = "50"; break;  //일상자주검사
                case "PCQ":       Exam_Div = "60"; break;  //공정조건검사
                //case "Circuit":   Exam_Div = "40"; break;  //순회검사, 초중종과 같음
                case "Daily":     Exam_Div = "80"; break;    //일일검사
            }

            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;
            DtpExamDate.EditValue = DateTime.Now;

            //검사구분에따른 설정
            Set_TypeOption();

            //업체별 옵션
            Set_CompanyOption();

            //GridView 설정
            Set_GridView();

            Set_RadioGroup();

            Set_Lup();

            //기타 표기 명 수정
            //switch (Exam_Div)
            //{
            //    case "10": Pnl_Proc.Text = "입고내역"; Lbl_ProcDate.Text = "입고일자"; break;  //수입검사
            //    case "20": Pnl_Proc.Text = "생산내역"; Lbl_ProcDate.Text = "생산일자"; break;  //출하검사
            //    case "25": Pnl_Proc.Text = "출하내역"; Lbl_ProcDate.Text = "출하일자"; break;  //출하검사
            //    case "30": Pnl_Proc.Text = "지시내역"; Lbl_ProcDate.Text = "지시일자"; break;  //공정검사
            //    case "40": Pnl_Proc.Text = "생산내역"; Lbl_ProcDate.Text = "생산일자"; break;  //초중종검사
            //    case "50": Pnl_Proc.Text = "생산내역"; Lbl_ProcDate.Text = "생산일자"; break;  //일상자주검사
            //    case "60": Pnl_Proc.Text = "생산내역"; Lbl_ProcDate.Text = "생산일자"; break;  //공정조건검사
            //}
        }

        private void CmdInspectionRpt_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Set_CompanyOption()
        {
            cmdInspectStd.Visible = false; //검사기준서
            cmdLabelPrint.Visible = false; //공정이동표
        }

        private void Set_TypeOption()
        {
            //수입검사
            if(Exam_Div == "10")
            {
                Pnl_Proc.Text = "입고내역";
                //Lbl_ProcDate.Text = "입고일자";

                ItemDiv = new string[] { "40", "50" };

                cmdMillSheet.Visible = true;

                //검사별 검사 횟수
                if (WrGlobal.ProcOption["QT"].ContainsKey("IQC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["IQC_ExamCnt"].ConvertInt_Null_To_Zero();
                }

                //검사 디폴트값 셋팅 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("IQC_Default_Set_Yn"))
                {
                    Default_Set_Yn = WrGlobal.ProcOption["QT"]["IQC_Default_Set_Yn"].ConvertString_Null_To_Empty();
                }

                //성적서 버튼 Visible 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("IQC_Rpt_PrintYN"))
                {
                    cmdInspectionRpt.Visible = WrGlobal.ProcOption["QT"]["IQC_Rpt_PrintYN"].ConvertString_Null_To_Empty() == "Y";
                }

                LblLine.Visible = false;
                LupLine.Visible = false;
                //LupLine.Properties.ReadOnly = true;

                LblProcess.Visible = false;
                LupProcess.Visible = false;
                //LupProcess.Properties.ReadOnly = true;

                LblMachine.Visible = false;
                LupMachine.Visible = false;
                //LupMachine.Properties.ReadOnly = true;

                AlwaysVisibleTrueLups.Location = new System.Drawing.Point(244, 34);
                LblItem.Visible = true;
                LupItem.Visible = true;

                LblCust.Visible = true;
                LupCust.Visible = true;

                //pnlPassDate.Visible = true;
                //pnlLotNo.Visible = true;
            }
            ////출하검사 : 생산실적기반
            //else if(Exam_Div == "20")
            //{
            //    Pnl_Proc.Text = "생산내역";

            //    ItemDiv = new string[] { "20" };

            //    //검사별 검사 횟수
            //    if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_ExamCnt"))
            //    {
            //        ExamCnt = WrGlobal.ProcOption["QT"]["OQC_ExamCnt"].ConvertInt_Null_To_Zero();
            //    }

            //    //검사 디폴트값 셋팅 여부
            //    if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_Default_Set_Yn"))
            //    {
            //        Default_Set_Yn = WrGlobal.ProcOption["QT"]["OQC_Default_Set_Yn"].ConvertString_Null_To_Empty();
            //    }

            //    //성적서 버튼 Visible 여부
            //    if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_Rpt_PrintYN"))
            //    {
            //        cmdInspectionRpt.Visible = WrGlobal.ProcOption["QT"]["OQC_Rpt_PrintYN"].ConvertString_Null_To_Empty() == "Y";
            //    }

            //    pnlLotNo.Visible = true;
            //}
            //출하검사 : 출하기반
            else if (Exam_Div == "25")
            {
                Pnl_Proc.Text = "출하내역";

                ItemDiv = new string[] { "20" };

                //검사별 검사 횟수
                if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["OQC_ExamCnt"].ConvertInt_Null_To_Zero();
                }

                //검사 디폴트값 셋팅 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_Default_Set_Yn"))
                {
                    Default_Set_Yn = WrGlobal.ProcOption["QT"]["OQC_Default_Set_Yn"].ConvertString_Null_To_Empty();
                }

                //성적서 버튼 Visible 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_Rpt_PrintYN"))
                {
                    cmdInspectionRpt.Visible = WrGlobal.ProcOption["QT"]["OQC_Rpt_PrintYN"].ConvertString_Null_To_Empty() == "Y";
                }

				if (Database.DbName.Contains("TPG"))
				{
                    pnlLotNo.Visible = true;
				}
            }
            //공정검사
            else if(Exam_Div == "30")
            {
                Pnl_Proc.Text = "지시내역";

                ItemDiv = new string[] { "20", "30" };

                //검사별 검사 횟수
                if (WrGlobal.ProcOption["QT"].ContainsKey("PQC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["PQC_ExamCnt"].ConvertInt_Null_To_Zero();
                }

                //검사 디폴트값 셋팅 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("PQC_Default_Set_Yn"))
                {
                    Default_Set_Yn = WrGlobal.ProcOption["QT"]["PQC_Default_Set_Yn"].ConvertString_Null_To_Empty();
                }

                //성적서 버튼 Visible 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("PQC_Rpt_PrintYN"))
                {
                    cmdInspectionRpt.Visible = WrGlobal.ProcOption["QT"]["PQC_Rpt_PrintYN"].ConvertString_Null_To_Empty() == "Y";
                }
            }
            //초중종검사
            else if(Exam_Div == "40")
            {
                Pnl_Proc.Text = "생산내역";
                ItemDiv = new string[] { "20", "30" };

                //검사별 검사 횟수
                if (WrGlobal.ProcOption["QT"].ContainsKey("FML_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["FML_ExamCnt"].ConvertInt_Null_To_Zero();
                }

                //검사 디폴트값 셋팅 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("FML_Default_Set_Yn"))
                {
                    Default_Set_Yn = WrGlobal.ProcOption["QT"]["FML_Default_Set_Yn"].ConvertString_Null_To_Empty();
                }

                this.pnlMeasureDiv.Visible = true;

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
            //일상자주검사
            else if (Exam_Div == "50")
            {
                Pnl_Proc.Text = "생산내역";

                ItemDiv = new string[] { "20", "30" };

                //검사별 검사 횟수
                if (WrGlobal.ProcOption["QT"].ContainsKey("DSC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["DSC_ExamCnt"].ConvertInt_Null_To_Zero();
                }

                //검사 디폴트값 셋팅 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("DSC_Default_Set_Yn"))
                {
                    Default_Set_Yn = WrGlobal.ProcOption["QT"]["DSC_Default_Set_Yn"].ConvertString_Null_To_Empty();
                }

                pnlMeasureDiv.Visible = true;
                PnlMeasureTime.Visible = true;
            }
            //공정조건검사
            else if (Exam_Div == "60")
            {
                Pnl_Proc.Text = "생산내역";

                ItemDiv = new string[] { "20", "30" };

                //검사별 검사 횟수
                if (WrGlobal.ProcOption["QT"].ContainsKey("PCQ_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["PCQ_ExamCnt"].ConvertInt_Null_To_Zero();
                }

                //검사 디폴트값 셋팅 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("PCQ_Default_Set_Yn"))
                {
                    Default_Set_Yn = WrGlobal.ProcOption["QT"]["PCQ_Default_Set_Yn"].ConvertString_Null_To_Empty();
                }

                pnlMeasureDiv.Visible = true;
                PnlMeasureTime.Visible = true;

                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                //LayoutItem_Bool.Visibility = LayoutVisibility.Never;
            }
            //일일검사
            else if (Exam_Div == "80")
            {
                Pnl_Proc.Text = "생산내역";

                ItemDiv = new string[] { "20", "30" };

                //검사별 검사 횟수
                if (WrGlobal.ProcOption["QT"].ContainsKey("Daily_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["Daily_ExamCnt"].ConvertInt_Null_To_Zero();
                }

                //검사 디폴트값 셋팅 여부
                if (WrGlobal.ProcOption["QT"].ContainsKey("Daily_Default_Set_Yn"))
                {
                    Default_Set_Yn = WrGlobal.ProcOption["QT"]["Daily_Default_Set_Yn"].ConvertString_Null_To_Empty();
                }

                pnlMeasureDiv.Visible = false;
                PnlMeasureTime.Visible = false;
                cmdMillSheet.Visible = false;
                cmdInspectStd.Visible = false;
                cmdLabelPrint.Visible = false;
                cmdInspectionRpt.Visible = false;

                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            }
        }

        #region GridView 셋팅

        private void Set_GridView()
        {
            switch (Exam_Div)
            {
                case "10": SetGrid_MatIn(Gv_MatIn); break;   //수입검사
                //case "20": SetGrid_ProdRst(Gv_ProdRst); break;  //출하검사:생산실적기반 - 250514 미개발
                case "25": SetGrid_ProdOut(Gv_ProdOut); break;  //출하검사:출하내역기반
                case "30": SetGrid_PrQC(Gv_PrQC); break;  //공정검사
                //case "40": SetGrid_ProcCmd_FML(Gv_Cmd_FML); break;  //초중종검사
				case "50": SetGrid_ProcCmd_DSC(Gv_Cmd_DSC); break;  //일상자주검사
                //case "60": SetGrid_PCQ(Gv_PCQ); break;  //공정조건검사
                //case "80": SetGrid_Daily(Gv_Daily); break;  //일일검사
            }

            SetGrid_ExRst_Bool(Gv_ExRst_Bool);
            SetGrid_ExRst_Num(Gv_ExRst_Num);
        }

        private void SetGrid_MatIn(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            //품목분류1
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", null), "코드", "명칭");

            grid.Create_GvColumn(gv, "검사여부", "ExamYn");
            grid.Create_GvColumn(gv, "판정", "AllPass");
            grid.Create_GvColumn(gv, "밀시트", "InFilePath");
            grid.Create_GvColumn(gv, "입고번호", "RefNo");
            grid.Create_GvColumn(gv, "입고순번", "RefSeq");
            grid.Create_GvColumn(gv, "입고일자", "StoDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "Lot No", "QCNo");
            grid.Create_GvColumn(gv, "자재Lot No", "LotNo");
            grid.Create_GvColumn(gv, "업체Lot No", "CustLotNo");
            grid.Create_GvColumn(gv, "입고수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["CustName"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);

            //정렬
            gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["AllPass"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["InFilePath"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;
            if(gv.Columns["ItemClass1"] != null) { gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1; }
            if(gv.Columns["ItemClass2"] != null) { gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2; }
            if(gv.Columns["ItemClass3"] != null) { gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3; }

            //프로세스옵션 : 업체LotNo 입력 활성화
            if (WrGlobal.ProcOption["MT"].ContainsKey("CustLotNo_YN"))
            {
                if (WrGlobal.ProcOption["MT"]["CustLotNo_YN"] != "Y")
                {
                    gv.Columns["LotNo"].Visible = false;
                    gv.Columns["CustLotNo"].Visible = false;
                }
            }

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            gv.Columns["LotNo"].Visible = false;
            gv.Columns["CustLotNo"].Visible = false;

            gv.Columns["RefNo"].Visible = false;
            gv.Columns["RefSeq"].Visible = false;
            gv.Columns["CustCode"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;
            gv.Columns["InFilePath"].Visible = false;

            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreIP"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModIP"].Visible = false;
            gv.Columns["ModDt"].Visible = false;
        }

        private void SetGrid_ProdOut(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            //품목분류1
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", null), "코드", "명칭");

            grid.Create_GvColumn(gv, "검사여부", "ExamYn");
            grid.Create_GvColumn(gv, "판정", "AllPass");
            grid.Create_GvColumn(gv, "출하번호", "RefNo");
            grid.Create_GvColumn(gv, "출하순번", "RefSeq");

            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

			grid.Create_GvColumn(gv, "LotNo", "LotNo");

			grid.Create_GvColumn(gv, "출하일자", "SalesDate");
            grid.Create_GvColumn(gv, "거래처코드", "BillToCode");
            grid.Create_GvColumn(gv, "거래처명", "BillToName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "출하수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "등록자"  , "CreId");
            grid.Create_GvColumn(gv, "등록IP"  , "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자"  , "ModId");
            grid.Create_GvColumn(gv, "수정IP"  , "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["Item_Nm"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);
            gv.Columns["AllPass"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

			//등록,수정자
			RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
			gv.Columns["ModId"].ColumnEdit = lup_Emp_No;
            gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;

            if (gv.Columns["ItemClass1"] != null) { gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1; }
            if (gv.Columns["ItemClass2"] != null) { gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2; }
            if (gv.Columns["ItemClass3"] != null) { gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3; }


            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;


            gv.Columns["RefNo"].Visible = false;
            gv.Columns["RefSeq"].Visible = false;
            gv.Columns["BillToCode"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;
            gv.Columns["SdOrderNo"].Visible = false;
            gv.Columns["SdOrderSeq"].Visible = false;
            //gv.Columns["ItemClass1"].Visible = false;

            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreIP"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModIP"].Visible = false;
            gv.Columns["ModDt"].Visible = false;
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

            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
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

 /*       private void SetGrid_ProcCmd_FML(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

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

            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
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
            gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.Columns["ExamYn"].MaxWidth = 80;

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

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
*/
        private void SetGrid_ProcCmd_DSC(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //품목분류1
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", null), "코드", "명칭");
            grid.Create_GvColumn(gv, "검사여부", "ExamYn");
            grid.Create_GvColumn(gv, "밀시트", "InFilePath");
            grid.Create_GvColumn(gv, "입고번호", "RefNo");
            grid.Create_GvColumn(gv, "입고번호", "RefSeq");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "생산수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);

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
                gv.Columns["InFilePath"].Visible = false;
                gv.Columns["CustCode"].Visible = false;
                gv.Columns["CustName"].Visible = false;

                gv.Columns["CreId"].Visible = false;
                gv.Columns["CreIP"].Visible = false;
                gv.Columns["CreDt"].Visible = false;
                gv.Columns["ModId"].Visible = false;
                gv.Columns["ModIP"].Visible = false;
                gv.Columns["ModDt"].Visible = false;
            }
        }

  /*      private void SetGrid_PCQ(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            grid.Create_GvColumn(gv, "실적번호", "RefNo");
            grid.Create_GvColumn(gv, "실적순번", "RefSeq");

            grid.Create_GvColumn(gv, "라인코드", "Line_Cd");
            grid.Create_GvColumn(gv, "라인명", "Line_Nm");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "Process_Nm");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "Machine_Nm");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");
            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["CustName"], false);
            grid.Column_AllowEdit(gv.Columns["ItemClass_Nm"], false);
            grid.Column_AllowEdit(gv.Columns["Item_Nm"], false);
            grid.Column_AllowEdit(gv.Columns["Line_Nm"], false);
            grid.Column_AllowEdit(gv.Columns["Process_Nm"], false);
            grid.Column_AllowEdit(gv.Columns["Machine_Nm"], false);
            grid.Column_AllowEdit(gv.Columns["Line_Cd"], false);
            grid.Column_AllowEdit(gv.Columns["ProcessCode"], false);
            grid.Column_AllowEdit(gv.Columns["MachineCode"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            //if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["RefNo"].Visible = false;
                gv.Columns["RefSeq"].Visible = false;
                gv.Columns["CustCode"].Visible = false;
                gv.Columns["ItemCode"].Visible = false;

                gv.Columns["CreId"].Visible = false;
                gv.Columns["CreIP"].Visible = false;
                gv.Columns["CreDt"].Visible = false;
                gv.Columns["ModId"].Visible = false;
                gv.Columns["ModIP"].Visible = false;
                gv.Columns["ModDt"].Visible = false;
            }
        }

        private void SetGrid_Daily(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

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

            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
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
            gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.Columns["ExamYn"].MaxWidth = 80;

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

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
        }*/

        private void GvBfProc_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            Search_QCList();
        }

        private void SetGrid_ExRst_Bool(GridView gv)
        {
            //합/불검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "검사코드", "ExamCd");
            grid.Create_GvColumn(gv, "검사명", "ExamNm");
            grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

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
            gv.Columns["X1"].ColumnEdit = rpo.Create_rpoChk();

            gv.Columns["PassDivCd"].ColumnEdit = rpo.Create_RepositoryItem(code.GetCM_CateItem(WrGlobal.CorpID, "Q020"), "코드", "명칭");

            //시료 판정
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("코드"), new DataColumn("명칭") });
            dt.Rows.Add("Y", "OK");
            dt.Rows.Add("N", "NG");

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ExamNm"], false);

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                GridColumn columns = gv.Columns["X" + i];
                columns.ColumnEdit = rpo.Create_RepositoryItem(dt, "코드", "명칭");
                columns.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; //정렬

                //grid.Column_AllowEdit(columns, false);
                columns.AppearanceCell.BackColor = smtDesignSetting.MustColor;  //Column 배경색
            }

            if (Database.DbName.Contains("ALLTHEN")) grid.Column_AllowEdit(gv.Columns["PassDivCd"], false);   //260119 올덴은 합격여부 편집 X
            else grid.Column_AllowEdit(gv.Columns["PassDivCd"]);

            grid.Column_AllowEdit(gv.Columns["ActionMemo"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

			//이벤트
			//gv.ValidatingEditor += Gv_ValidatingEditor;
			gv.CellValueChanged += Event_CellValueChagned_Bool; ;    //셀값 수정 Event
            gv.RowCellClick += Event_CellClick;
            gv.CustomDrawCell += Event_Cell_ForeColor;   //불합격이면 글자색 빨강

            if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["ExamNo"].Visible = false;
                gv.Columns["ExamSeq"].Visible = false;
            }
        }

		private void Event_CellValueChagned_Bool(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            string PassDivCd = "10"; //10:합격 20:불합격

            //검사값에따른 Pass_Div(최종검사여부) 변경
            if (fieldNm.StartsWith("X"))
            {
                string x1 = Convert.ToString(dr["X1"]);
                string x2 = Convert.ToString(dr["X2"]);
                string x3 = Convert.ToString(dr["X3"]);
                string x4 = Convert.ToString(dr["X4"]);
                string x5 = Convert.ToString(dr["X5"]);
                string x6 = Convert.ToString(dr["X6"]);

                switch (ExamCnt)
                {
                    case 1: if (x1 == "N") PassDivCd = "20"; break;
                    case 2: if (x1 == "N") PassDivCd = "20"; if (x2 == "N") PassDivCd = "20"; break;
                    case 3: if (x1 == "N") PassDivCd = "20"; if (x2 == "N") PassDivCd = "20"; if (x3 == "N") PassDivCd = "20"; break;
                    case 4: if (x1 == "N") PassDivCd = "20"; if (x2 == "N") PassDivCd = "20"; if (x3 == "N") PassDivCd = "20"; if (x4 == "N") PassDivCd = "20"; break;
                    case 5: if (x1 == "N") PassDivCd = "20"; if (x2 == "N") PassDivCd = "20"; if (x3 == "N") PassDivCd = "20"; if (x4 == "N") PassDivCd = "20"; if (x5 == "N") PassDivCd = "20"; break;
                    case 6: if (x1 == "N") PassDivCd = "20"; if (x2 == "N") PassDivCd = "20"; if (x3 == "N") PassDivCd = "20"; if (x4 == "N") PassDivCd = "20"; if (x5 == "N") PassDivCd = "20"; if (x6 == "N") PassDivCd = "20"; break;
                    default: break;
                }

                gv.SetFocusedRowCellValue("PassDivCd", PassDivCd);
            }
        }

        private void Event_CellClick(object sender, RowCellClickEventArgs e)
        {
            if (!e.Column.FieldName.StartsWith("X")) return;

            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            string currentValue = Convert.ToString(dr[e.Column.FieldName]);

            string newValue = (currentValue == "Y") ? "N" : "Y";

            gv.SetFocusedRowCellValue(e.Column.FieldName, newValue);

            gv.RefreshData();
        }

        private void Event_Cell_ForeColor(object sender, RowCellCustomDrawEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle < 0) return;

            string fieldName = e.Column.FieldName;

            if (fieldName == "X1" || fieldName == "X2" || fieldName == "X3" || fieldName == "X4" || fieldName == "X5" || fieldName == "X6")
            {
                string value = gv.GetRowCellValue(e.RowHandle, fieldName)?.ToString();

                if (value == "Y")
                {
                    e.Appearance.ForeColor = Color.Black; // 10이면 검은색 글자로 변경
                }
                else if (value == "N")
                {
                    e.Appearance.ForeColor = Color.Red; // 20이면 빨간색 글자로 변경
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

        private void SetGrid_ExRst_Num(GridView gv)
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
            grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");
            grid.Create_GvColumn(gv, "관리범위<br>하한", "MgntMin");
            grid.Create_GvColumn(gv, "관리범위<br>상한", "MgntMax");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

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
            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Column_AllowEdit(gv.Columns["X" + i]);
            }

            grid.Column_AllowEdit(gv.Columns["PassDivCd"]);
            grid.Column_AllowEdit(gv.Columns["ActionMemo"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            grid.SetCol_Type(gv.Columns["MgntMin"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["MgntMax"], clsGrid.TypeList.TFloat);

            //이벤트
            gv.CellValueChanged += Event_CellValueChagned_Num; ;    //셀값 수정 Event
            gv.CustomDrawCell += Event_Cell_ForeColor;   //불합격이면 글자색 빨강

            if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["ExamNo"].Visible = false;
                gv.Columns["ExamSeq"].Visible = false;
            }
        }

        private void Event_CellValueChagned_Num(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            string PassDivCd = "10";

            //검사값이 관리범위를 벗어났는지 검사
            if(fieldNm.StartsWith("X"))
            {
                string x1 = dr["X1"].ConvertString_Null_To_Empty();
                string x2 = dr["X2"].ConvertString_Null_To_Empty();
                string x3 = dr["X3"].ConvertString_Null_To_Empty();
                string x4 = dr["X4"].ConvertString_Null_To_Empty();
                string x5 = dr["X5"].ConvertString_Null_To_Empty();
                string x6 = dr["X6"].ConvertString_Null_To_Empty();

                switch (ExamCnt)
                {
                    case 1: if (!Chk_Num(dr, x1)) PassDivCd = "20"; break;
                    case 2: if (!Chk_Num(dr, x1)) PassDivCd = "20"; if (!Chk_Num(dr, x2)) PassDivCd = "20";break;
                    case 3: if (!Chk_Num(dr, x1)) PassDivCd = "20"; if (!Chk_Num(dr, x2)) PassDivCd = "20"; if (!Chk_Num(dr, x3)) PassDivCd = "20"; break;
                    case 4: if (!Chk_Num(dr, x1)) PassDivCd = "20"; if (!Chk_Num(dr, x2)) PassDivCd = "20"; if (!Chk_Num(dr, x3)) PassDivCd = "20"; if (!Chk_Num(dr, x4)) PassDivCd = "20"; break;
                    case 5: if (!Chk_Num(dr, x1)) PassDivCd = "20"; if (!Chk_Num(dr, x2)) PassDivCd = "20"; if (!Chk_Num(dr, x3)) PassDivCd = "20"; if (!Chk_Num(dr, x4)) PassDivCd = "20"; if (!Chk_Num(dr, x5)) PassDivCd = "20";break;
                    case 6: if (!Chk_Num(dr, x1)) PassDivCd = "20"; if (!Chk_Num(dr, x2)) PassDivCd = "20"; if (!Chk_Num(dr, x3)) PassDivCd = "20"; if (!Chk_Num(dr, x4)) PassDivCd = "20"; if (!Chk_Num(dr, x5)) PassDivCd = "20"; if (!Chk_Num(dr, x6)) PassDivCd = "20"; break;
                    default: break;
                }

                gv.SetFocusedRowCellValue("PassDivCd", PassDivCd);
            }
        }

        private bool Chk_Num(DataRow dr, string value)
        {
            bool result = true;

            if (value == "")
            {
                return result;
            }

            float MgntMin = dr["MgntMin"].ConvertFloat_Null_To_Zero();
            float MgntMax = dr["MgntMax"].ConvertFloat_Null_To_Zero();
            float DangerMin = dr["DangerMin"].ConvertFloat_Null_To_Zero();
            float DangerMax = dr["DangerMax"].ConvertFloat_Null_To_Zero();
            float xValue = value.ConvertFloat_Null_To_Zero();

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

            //위험범위 오버 검사
            //상한만 있을때
            if (DangerMin == 0 && DangerMax > 0)
            {
                if (xValue > DangerMax)
                {
                    //값이 상한보다 높으면 불합 처리
                    result = false;
                }
            }
            //하한만 있을때
            else if (DangerMin > 0 && DangerMax == 0)
            {
                if (xValue < DangerMin)
                {
                    //값이 하한보다 낮으면 불합 처리
                    result = false;
                }
            }
            //상,하한 둘다 있을때
            else if (DangerMin > 0 && DangerMin < DangerMax)
            {
                if (xValue < DangerMin || xValue > DangerMax)
                {
                    //값이 하한보다 낮거나 상한보다 높으면 불합 처리
                    result = false;
                }
            }

            return result;
        }

        #endregion GridView 셋팅 End

        private void Set_RadioGroup()
        {
            //RadioGroup 셋팅
            Lbl_Rdo.Text = "품목구분";

            CM_CateItem cm = new CM_CateItem();
            clsRadioGroup rdo = new clsRadioGroup();

            rdo.Create_RadioGroupItem(Rdo_Div, cm.SelectClassItemLoopUp_WithALL(WrGlobal.CorpID, "A010", ItemDiv, true), "분류명", "분류코드");

            Rdo_Div.SelectedIndex = 0;

            Rdo_Div.EditValueChanged += Rdo_Div_EditValueChanged;
        }

        private void Rdo_Div_EditValueChanged(object sender, EventArgs e)
        {
            Search_BfProc();
        }

        private void Set_Lup()
        {
            cls_Lookup clsLup = new cls_Lookup();

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

            //2. 측정구분
            string cm_Class = "";

            switch (Exam_Div)
            {
                case "40": cm_Class = "Q01110"; break; //초중종
                case "50": cm_Class = "Q01120"; break; //일상자주
                case "60": cm_Class = "Q01130"; break; //공정조건
            }

            DataTable dt = cm.SelectClassItemLoopUp_WithALL(WrGlobal.CorpID, cm_Class, null, false);

            clsLup.SetLup_Combo(LupMeasure_Div, dt, "분류코드", "분류명");

            LupMeasure_Div.EditValueChanged += LupMeasure_Div_EditValueChanged;

            //초기값 설정
            if (dt.Rows.Count > 0)
            {
                LupMeasure_Div.EditValue = dt.Rows[0]["분류코드"];
            }

            //검사자
            string[] deptId = code.GetCM_CateItemCode(WrGlobal.CorpID, "Q003");
            if (deptId.Length < 1) deptId = null; //공통코드에 검사부서 등록하여 불러오기, 없으면 전체 사용자 조회하도록

            DataTable dtUser = code.GetDataTableUser(false, deptId, "N");
            clsLup.Create_RepositoryItemSearchLookUp(LupExamEmpId, dtUser, "코드", "명칭");
        }

        private void Lup_EditValueChanged(object sender, EventArgs e)
        {
            //조회조건 서치룩업 변경시 연계 데이터 재조회
            SearchLookUpEdit Lup = sender as SearchLookUpEdit;

            string LupName = Lup.Name.ToString();
            string LupValue = Lup.EditValue.ConvertString_Null_To_Empty();

            if (Lup.EditValue == null)
            {
                Set_Lup();
                return;
            }

            cls_Lookup clsLup = new cls_Lookup();

            //BS_ProcLine procLine = new BS_ProcLine();
            //EQ_MachineProc machineProc = new EQ_MachineProc();
            //BS_Item item = new BS_Item();

            switch (LupName)
            {
                case "LupLine":
                    //공정리스트 변경
                    //procLine.Line_Cd = LupValue;
                    //DataTable dt_process = procLine.SearchLookUp_Select_ProcLine_NEW(true);
                    //clsLup.Create_RepositoryItemSearchLookUp(LupProcess, dt_process, "코드", "명칭");
                    //LupProcess.EditValue = "ALL";
                    break;

                case "LupProcess":
                    //설비 리스트 변경
                    //machineProc.ProcessCode = LupValue;
                    //DataTable dt_machine = machineProc.SearchLookUp_Select_MachineProc(true);
                    //clsLup.Create_RepositoryItemSearchLookUp(LupMachine, dt_machine, "코드", "명칭");
                    //LupMachine.EditValue = "ALL";

                    //품목 리스트 변경
                    //item.ProcessCode = LupValue;
                    //DataTable dt_ProcItem = item.Select_Item_ByProcess(new string[] { Rdo_Div.EditValue.ConvertString_Null_To_Empty() }, true);
                    //clsLup.Create_RepositoryItemSearchLookUp(LupItem, dt_ProcItem, "ItemCode", "Item_Nm");
                    //grid.SetLupView_Item(LupItem, "제품코드 초기화");
                    //LupItem.EditValue = "ALL";
                    break;
            }
        }

        private void LupMeasure_Div_EditValueChanged(object sender, EventArgs e)
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
                case "cmdMillSheet": Upload_MillSheet(); break;
                case "cmdDelete": Delete_Data(); break;
                case "cmdInspectionRpt": Print(); break;
                case "cmdLabelPrint": Print2(); break;
                case "cmdInspectStd": Print_InspectionStd(); break;
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

            //이전 프로세스 작업내역 조회
            switch (Exam_Div)
            {
                case "10": Search_IQC(); break;   //수입검사:입고내역 조회
                case "20": Search_OQC(); break;   //출하검사:생산실적 조회
                case "25": Search_OQC_Out(); break;   //출하검사:출하내역 조회
                case "30": Search_ProcessResult(); break;   //공정검사
                case "40": Search_ProcessResult(); break;   //초중종검사
                case "50": Search_ProcessResult(); break;   //공정자주검사
                case "60": Search_ProcessResult(); break;   //공정자주검사
                case "80": Search_Daily(); break;           //일일검사
            }

            DataTable dt = Gc_BfProc.DataSource as DataTable;

            if(dt == null)
            {
                Gc_ExRst_Bool.DataSource = null;
                Gc_ExRst_Num.DataSource = null;
			}
			else
			{
                if(dt.Rows.Count < 1)
                {
                    Gc_ExRst_Bool.DataSource = null;
                    Gc_ExRst_Num.DataSource = null;
                }
			}
        }

        private void Search_IQC()
        {
            Gc_BfProc.MainView = Gv_MatIn;
            GridView gv = Gc_BfProc.MainView as GridView;

            string startDate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-dd");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("yyyy-MM-dd 23:59");
            string item_Div = Rdo_Div.EditValue.ConvertString_Null_To_Empty();
            string lupItem = LupItem.EditValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();

            MM_PurchaseD pd = new MM_PurchaseD();
            pd.CorpId = WrGlobal.CorpID;
            pd.ItemCode = lupItem;

            Gc_BfProc.DataSource = pd.SelectMM_PurchaseD_Exam(startDate, endDate, item_Div);

            //gv.Columns["InFilePath"].Visible = BtnMillSheet.Visible;

            gv.BestFitColumns();
        }

        private void Search_OQC()
        {
            //Gc_BfProc.MainView = Gv_ProdRst;
            //GridView gv = Gc_BfProc.MainView as GridView;

            //string startDate = DtpStartDate.EditValue.ConvertDate_To_Char8();
            //string endDate = DtpEndDate.EditValue.ConvertDate_To_Char8();
            //string item_Div = Rdo_Div.EditValue.ConvertString_Null_To_Empty();

            //PD_ProductIn ppi = new PD_ProductIn();
            //Gc_BfProc.DataSource = ppi.Select_ProdIn_ExamRst(startDate, endDate, new string[] { item_Div });

            //gv.BestFitColumns();
        }

        private void Search_OQC_Out()
        {
            Gc_BfProc.MainView = Gv_ProdOut;
            GridView gv = Gc_BfProc.MainView as GridView;

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char8();
            string item_Div = Rdo_Div.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Rdo_Div.EditValue.ConvertString_Null_To_Empty();

            BM_SalesD sd = new BM_SalesD();

            if (g_Rst_No != "" || G_Rst_Row_Seq != 0)
            {
                sd.SalesNo = g_Rst_No;
                sd.SalesSeq = G_Rst_Row_Seq;
            }

            Gc_BfProc.DataSource = sd.Select_Sales_ExamRst(startDate, endDate, item_Div == "" ? null : new string[] { item_Div }, "20");

            gv.BestFitColumns();
        }

        private void Search_ProcessResult()
        {
            switch (Exam_Div)
            {
                case "30": Gc_BfProc.MainView = Gv_PrQC; break;
                case "40": Gc_BfProc.MainView = Gv_Cmd_FML; break;
                case "50": Gc_BfProc.MainView = Gv_Cmd_DSC; break;
                case "60": Gc_BfProc.MainView = Gv_PCQ; break;
            }

            GridView gv = Gc_BfProc.MainView as GridView;
            int idx = gv.FocusedRowHandle;

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char8();
            string item_Div = Rdo_Div.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Rdo_Div.EditValue.ConvertString_Null_To_Empty();

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
                dt_ProcResult = or.Select_PM_OrderResult_Exam(Exam_Div, startDate, endDate, item_Div == "" ? null : new string[] { item_Div }, ProcessCds);
            }
            else
            {
                ////현장시스템에서 로드 시 해당 실적 건만 조회
                or.SerialNo = g_Rst_No;

				dt_ProcResult = or.Select_PM_OrderResult_Exam(Exam_Div);
			}

            Gc_BfProc.DataSource = dt_ProcResult;

            gv.FocusedRowHandle = idx;
            gv.BestFitColumns();

            //데이터 없을시 정성, 정량 검사항목 DataSource도 Clear
            if(dt_ProcResult == null)
            {
                Gc_ExRst_Bool.DataSource = null;
                Gc_ExRst_Num.DataSource = null;
            }
            else
            {
                if(dt_ProcResult.Rows.Count < 1)
                {
                    Gc_ExRst_Bool.DataSource = null;
                    Gc_ExRst_Num.DataSource = null;
                }
            }
        }

        private void Search_Daily()
        {
            //일일검사
            Gc_BfProc.MainView = Gv_Daily;
            GridView gv = Gc_BfProc.MainView as GridView;

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char8();
            string item_Div = Rdo_Div.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Rdo_Div.EditValue.ConvertString_Null_To_Empty();

            string line_Cd = LupLine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupLine.EditValue.ConvertString_Null_To_Empty();
            string ProcessCode = LupProcess.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupProcess.EditValue.ConvertString_Null_To_Empty();
            string MachineCode = LupMachine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupMachine.EditValue.ConvertString_Null_To_Empty();
            string ItemCode = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();

            PM_OrderResult or = new PM_OrderResult();
            or.LineCode = line_Cd;
            or.ProcessCode = ProcessCode;
            or.MachineCode = MachineCode;
            or.ItemCode = ItemCode;

            Gc_BfProc.DataSource = or.Select_PM_OrderResult_Exam(Exam_Div, startDate, endDate, item_Div == "" ? null : new string[] { item_Div }, ProcessCds);
            gv.BestFitColumns();
        }

        //검사내역 조회
        private void Search_QCList()
        {
            //데이터 조회
            switch (Exam_Div)
            {
                case "10":  //입고검사
                case "20":  //출하검사:생산실적기반
                case "25":  //출하검사:출하내역기반
                case "30": Search_QC_List(); Set_ExamDate(); break;   //공정검사

                case "40": //초중종검사
                case "50": //일상자주검사
                case "60": //공정조건검사
                case "80": //일일검사
                    Search_QC_List();
                    Set_ExamDate();
                    //Set_MeasureTime();
                    break;   //초중종검사
            }
        }

        private void Search_QC_List()
        {
            //검사목록 불러오기
            GridView gv = Gc_BfProc.MainView as GridView;
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            //측정구분
            string measure_Div = "";

             switch(Exam_Div)
            {
                case "40":
                case "50":
                case "60":
                case "80":
                    measure_Div = LupMeasure_Div.EditValue.ConvertString_Null_To_Empty();
                    break;
            }

             string ItemCode = Convert.ToString(dr["ItemCode"]);
            string div = Exam_Div;
            if (Exam_Div == "25") { div = "20"; }

            string ProcessCode = null;

            //컬럼 존재 유무 확인
            if (dr.Table.Columns.Contains("ProcessCode"))
			{
                ProcessCode = Convert.ToString(dr["ProcessCode"]);
            }

            QM_ExamResult qer = new QM_ExamResult();
            qer.RefNo = Convert.ToString(dr["RefNo"]);
            qer.RefSeq = dr["RefSeq"].ConvertInt_Null_To_Zero();
            qer.ExamDivCd = div;           //검사 구분
            qer.MeasureDivCd = measure_Div; //측정 구분

            DataTable dtExam = qer.Select_ExamResult();

            //검사한 항목일경우
            if (dtExam.Rows.Count > 0)
            {
                Gc_ExRst_Bool.DataSource = qer.Select_ExamResult_TPG("10");
                Gc_ExRst_Num.DataSource = qer.Select_ExamResult_TPG("20");
            }
            else
            {
                //미검사 항목일 경우
                QM_ItemExamMgnt iem = new QM_ItemExamMgnt();
                iem.ItemCode = ItemCode;
                iem.ExamDivCd = div;
                iem.ProcessCode = ProcessCode;

                iem.ValueTypeDivCd = "10";
                Gc_ExRst_Bool.DataSource = iem.Select_ItemExam_List();

                iem.ValueTypeDivCd = "20";
                Gc_ExRst_Num.DataSource = iem.Select_ItemExam_List();

                if (Default_Set_Yn == "Y")
                {
                    SetDefaultValue();
                }
            }

            Gv_ExRst_Bool.BestFitColumns();
            Gv_ExRst_Num.BestFitColumns();
        }

        private void SetDefaultValue()
        {
            DataTable dtBool = Gc_ExRst_Bool.DataSource as DataTable;
            DataTable dtNum = Gc_ExRst_Num.DataSource as DataTable;

            if (dtBool != null)
            {
                if (dtBool.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtBool.Rows)
                    {
                        dr["X1"] = dr["DefaultValue"];
                        if (ExamCnt == 1) continue;

                        dr["X2"] = dr["DefaultValue"];
                        if (ExamCnt == 2) continue;

                        dr["X3"] = dr["DefaultValue"];
                        if (ExamCnt == 3) continue;

                        dr["X4"] = dr["DefaultValue"];
                        if (ExamCnt == 4) continue;

                        dr["X5"] = dr["DefaultValue"];
                        if (ExamCnt == 5) continue;

                        dr["X6"] = dr["DefaultValue"];
                    }
                }
            }

            //정량일 경우 해당 검사 규격의 랜덤값으로 설정
            if (dtNum != null)
            {
                if (dtNum.Rows.Count > 0)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());

                    foreach (DataRow dr in dtNum.Rows)
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
                        dr["X1"] = randValue;
                        if (ExamCnt == 1) continue;

                        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), DecimalCnt);
                        dr["X2"] = randValue;
                        if (ExamCnt == 2) continue;

                        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), DecimalCnt);
                        dr["X3"] = randValue;
                        if (ExamCnt == 3) continue;

                        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), DecimalCnt);
                        dr["X4"] = randValue;
                        if (ExamCnt == 4) continue;

                        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), DecimalCnt);
                        dr["X5"] = randValue;
                        if (ExamCnt == 5) continue;

                        randValue = Math.Round(minValue + (rand.NextDouble() * (maxValue - minValue)), DecimalCnt);
                        dr["X6"] = randValue;
                    }
                }
            }
        }

        private void Set_ExamDate()
		{
             GridView gv = Gc_BfProc.MainView as GridView;
            DataRow drBf = gv.GetFocusedDataRow();

            //검사일자 바인딩
            DataTable dt_Bool = Gc_ExRst_Bool.DataSource as DataTable;
            DataTable dt_Num = Gc_ExRst_Num.DataSource as DataTable;

            if (dt_Bool == null || dt_Num == null)
            {
                return;
            }

            DataTable dtSave = new DataTable();
            dtSave = dt_Bool.Copy();
            dtSave.Merge(dt_Num);

            if (dtSave.Rows.Count > 0)
            {
                DataRow dr = dtSave.Rows[0];

                if (dr["ExamNo"].ConvertString_Null_To_Empty() != "") //측정 O
                {
                    DtpExamDate.EditValue = dr["ExamDate"];        //검사일자
                    DtpPassDate.EditValue = dr["PassDate"];        //판정일자
                    TxtExamQty.EditValue = dr["ExamQty"];          //검사수량
                    TxtMeasureTime.EditValue = dr["MeasureTime"]; //검사시간
                    if(Exam_Div == "10") TxtLotNo.EditValue = drBf["QCNo"]; //LotNo
                    else TxtLotNo.EditValue = dr["LotNo"];
                    LupExamEmpId.EditValue = dr["ExamEmpId"];       //검사자
                }
                else
                {
                    DtpExamDate.EditValue = DateTime.Now.Date;  //검사일자
                    DtpPassDate.EditValue = null;
                    LupExamEmpId.EditValue = null;              //검사자
                    if(drBf != null) TxtExamQty.EditValue = drBf["ExamQty"];      //검사수량
                    if(drBf != null && drBf.Table.Columns.Contains("LotNo")) TxtLotNo.EditValue = drBf["LotNo"];     //LotNo
                    if(drBf != null && drBf.Table.Columns.Contains("QCNo")) TxtLotNo.EditValue = drBf["QCNo"];      //LotNo

                    //미검사항목 측정시간 Default 값 바인딩
                    if (LupMeasure_Div.EditValue.ConvertString_Null_To_Empty() != "")
					{
                        string cm_Class = "";

                        switch (Exam_Div)
                        {
                            case "40": cm_Class = "Q01110"; break; //초중종
                            case "50": cm_Class = "Q01120"; break; //일상자주
                            case "60": cm_Class = "Q01130"; break; //공정조건
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
			else
			{
                DtpExamDate.EditValue = DateTime.Now.Date;
                DtpPassDate.EditValue = null;
                LupExamEmpId.EditValue = null;
                TxtExamQty.EditValue = null;
                TxtMeasureTime.EditValue = null;
                TxtLotNo.EditValue = null;
            }
        }

  /*      private void Set_MeasureTime()
        {
            //측정시간 바인딩
            DataTable dt_Bool = Gc_ExRst_Bool.DataSource as DataTable;
            DataTable dt_Num = Gc_ExRst_Num.DataSource as DataTable;

            if (dt_Bool == null || dt_Num == null)
            {
                return;
            }

            DataTable dtSave = new DataTable();
            dtSave = dt_Bool.Copy();
            dtSave.Merge(dt_Num);

            if (dtSave.Rows.Count > 0)
            {
                DataRow dr = dtSave.Rows[0];

                if (dr["ExamNo"].ConvertString_Null_To_Empty() != "") //측정 한 경우
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
                        case "40": cm_Class = "Q01110"; break; //초중종
                        case "50": cm_Class = "Q01120"; break; //일상자주
                        case "60": cm_Class = "Q01130"; break; //공정조건
                    }

                    CM_CateItem cci = new CM_CateItem();
                    cci.Class = cm_Class;
                    cci.ClassItemCode = LupMeasure_Div.EditValue.ConvertString_Null_To_Empty();

                    DataRow drCmCd = cci.Select_CM_CateItem_ByCode();

                    if (drCmCd != null)
                    {
                        TxtMeasureTime.EditValue = drCmCd["ExtraValue"];
					}
                }
            }
        }*/

        #endregion 데이터 조회 End

        #region 데이터 저장

        private void Save_Data()
        {
            this.Validate();

            //데이터 저장
            try
            {
                DataTable dtSave = null;
                DataTable dtBool = Gc_ExRst_Bool.DataSource as DataTable;
                DataTable dtNum = Gc_ExRst_Num.DataSource as DataTable;

                if(dtBool == null || dtNum == null)
                {
                    return;
                }

                dtSave = dtBool.Copy();
                dtSave.Merge(dtNum);

                if (dtSave.Rows.Count < 1) return;

                //검사번호 만들기
                string ExamNo = Convert.ToString(dtSave.Rows[0]["ExamNo"]);    //검사번호
                string div = Exam_Div;
                if (Exam_Div == "25") { div = "20"; }

                DataRow drBf = (Gc_BfProc.MainView as GridView).GetFocusedDataRow();
                QM_ExamResult qer = new QM_ExamResult();
                qer.ExamDate = DtpExamDate.Text.ConvertDate_To_Char8();                   //검사일자
                qer.PassDate = DtpPassDate.Text.ConvertDate_To_Char8();                   //검사일자
                qer.ItemCode = drBf["ItemCode"].ConvertString_Null_To_Empty();    //품목
                qer.ExamDivCd = div;                                              //검사구분
                qer.RefNo = drBf["RefNo"].ConvertString_Null_To_Empty();    //참조번호
                qer.RefSeq = drBf["RefSeq"].ConvertInt_Null_To_Zero();        //참조순번
                qer.ExamQty = TxtExamQty.EditValue.ConvertFloat_Null_To_Zero();        //검사수량
                qer.ExamEmpId = LupExamEmpId.EditValue.ConvertString_Null_To_Empty();        //검사자
                qer.LotNo = TxtLotNo.EditValue.ConvertString_Null_To_Empty();        //LotNo

                //측정구분
                string measure_Div = "";

                switch (Exam_Div)
                {
                    case "40":
                    case "50":
                    case "60":
                        measure_Div = LupMeasure_Div.EditValue.ConvertString_Null_To_Empty();
                        break;
                }

                bool Success = true;
                if (ExamNo == "")
                {
                    //신규등록
                    //측정 구분에따른 추가 신규입력 구분
                    DataRow drExists = qer.Select_ExamResult_Exsits();
                    int idx = 0;
                    if(drExists == null)
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
                        qer.ExamSeq    = idx + 1;                                                   //검사순번
                        qer.DispSeq       = qer.ExamSeq;                                           //표시순번
                        qer.ExamCd        = drInsert["ExamCd"].ConvertString_Null_To_Empty();         //검사코드
                        qer.MeasureDivCd = measure_Div; //측정구분
                        qer.MeasureTime   = measure_Div != "" ? TxtMeasureTime.EditValue.ConvertString_Null_To_Empty() : "";    //측정시간
                        qer.X1             = drInsert["X1"].ConvertString_Null_To_Empty();              //검사값1
                        qer.X2             = drInsert["X2"].ConvertString_Null_To_Empty();              //검사값2
                        qer.X3             = drInsert["X3"].ConvertString_Null_To_Empty();              //검사값3
                        qer.X4             = drInsert["X4"].ConvertString_Null_To_Empty();              //검사값4
                        qer.X5             = drInsert["X5"].ConvertString_Null_To_Empty();              //검사값5
                        qer.X6             = drInsert["X6"].ConvertString_Null_To_Empty();              //검사값6
                        qer.PassDivCd    = drInsert["PassDivCd"].ConvertString_Null_To_Empty();     //판정여부
                        qer.ActionMemo    = drInsert["ActionMemo"].ConvertString_Null_To_Empty();     //조치내역
                        qer.Remark            = drInsert["Remark"].ConvertString_Null_To_Empty();             //비고

                        //Insert
                        if (!qer.Ins_ExamResult_TPG())
                        {
                            //저장 중 에러 발생시
                            qer.Del_ExamResult_ByRefNo_ExamDiv();

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
                        qer.ExamNo        = drInsert["ExamNo"].ConvertString_Null_To_Empty();         //검사번호
                        qer.ExamSeq    = drInsert["ExamSeq"].ConvertInt_Null_To_Zero();         //검사순번
                        qer.DispSeq       = qer.ExamSeq;                                           //표시순번
                        qer.ExamCd        = drInsert["ExamCd"].ConvertString_Null_To_Empty();         //검사코드
                        qer.MeasureDivCd = drInsert["MeasureDivCd"].ConvertString_Null_To_Empty();  //측정구분
                        qer.MeasureTime = measure_Div != ""? TxtMeasureTime.EditValue.ConvertString_Null_To_Empty() : "";    //측정시간
                        qer.X1             = drInsert["X1"].ConvertString_Null_To_Empty();              //검사값1
                        qer.X2             = drInsert["X2"].ConvertString_Null_To_Empty();              //검사값2
                        qer.X3             = drInsert["X3"].ConvertString_Null_To_Empty();              //검사값3
                        qer.X4             = drInsert["X4"].ConvertString_Null_To_Empty();              //검사값4
                        qer.X5             = drInsert["X5"].ConvertString_Null_To_Empty();              //검사값5
                        qer.X6             = drInsert["X6"].ConvertString_Null_To_Empty();              //검사값6
                        qer.PassDivCd    = drInsert["PassDivCd"].ConvertString_Null_To_Empty();     //판정여부
                        qer.ActionMemo    = drInsert["ActionMemo"].ConvertString_Null_To_Empty();     //조치내역
                        qer.Remark            = drInsert["Remark"].ConvertString_Null_To_Empty();             //비고

                        //Update
                        if (!qer.Upd_ExamResult_TPG())
                        {
                            //저장 중 에러 발생시

                            Success = false;
                            break;
                        }
                    }
                }

                // 수입검사 LotNo 입력시, 자재입고 QCNo 업데이트
                if (Exam_Div == "10")
                {
                    MM_PurchaseD pd = new MM_PurchaseD();
                    pd.StoNo = drBf["RefNo"].ConvertString_Null_To_Empty();
                    pd.StoSeq = drBf["RefSeq"].ConvertInt_Null_To_Zero();
                    pd.ItemCode = drBf["ItemCode"].ConvertString_Null_To_Empty();
                    pd.QCNo = TxtLotNo.EditValue.ConvertString_Null_To_Empty();

                    if (!pd.Update_MM_PurchaseDLotNo())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return;
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
                DataTable dtBool = Gc_ExRst_Bool.DataSource as DataTable;
                DataTable dtNum = Gc_ExRst_Num.DataSource as DataTable;

                DataTable dt = dtBool.Copy();
                dt.Merge(dtNum);

                for (int i = 0; i < dt.Rows.Count; i++)
				{
                    string ExamNo = dt.Rows[i]["ExamNo"].ConvertString_Null_To_Empty();
                    if(ExamNo != "")
					{
                        QM_ExamResult ex = new QM_ExamResult();
                        ex.ExamNo = ExamNo;
                        ex.ExamSeq = 0;
                        ex.Del_ExamResult();
					}
				}

                if (Exam_Div == "10")
                {
                    DataRow drBf = (Gc_BfProc.MainView as GridView).GetFocusedDataRow();

                    MM_PurchaseD pd = new MM_PurchaseD();
                    pd.StoNo = drBf["RefNo"].ConvertString_Null_To_Empty();
                    pd.StoSeq = drBf["RefSeq"].ConvertInt_Null_To_Zero();
                    pd.ItemCode = drBf["ItemCode"].ConvertString_Null_To_Empty();
                    pd.QCNo = null;

                    if (!pd.Update_MM_PurchaseDLotNo())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return;
                    }
                }

                MessageBox.Show("측정 데이터 삭제 완료");

                //Search_QCList();
                Search_BfProc();

                //return;
            }
            catch
			{

			}

        }

        private string Create_No()
        {
            cls_Database cDB = new cls_Database();
            int No_Seq = cDB.Get_Last_NoSeq("QM_ExamResult", "ExamNo", DtpExamDate.Text.ConvertDate_To_Char8());

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
            DataRow drBf = (Gc_BfProc.MainView as GridView).GetFocusedDataRow();

            if (drBf == null)
            {
                return;
            }



            //검사실적 등록// 확인
            QM_ExamResult qer = new QM_ExamResult();
            qer.RefNo = drBf["RefNo"].ConvertString_Null_To_Empty();
            qer.RefSeq = drBf["RefSeq"].ConvertInt_Null_To_Zero();
            qer.ExamDivCd = Exam_Div;

            DataRow dataRow = qer.Select_ExamResult_Exsits();

            if (dataRow == null)
            {
                if (Database.DbName.Contains("KUMHO_ANSAN"))
                {
                    cmdSave.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("검사 실적이 없습니다. 실적 저장 후 등록 해주세요.");
                    return;
                }
            }

            string RefNo = drBf["RefNo"].ConvertString_Null_To_Empty();
            int RefSeq = drBf["RefSeq"].ConvertInt_Null_To_Zero();

            Frm_MillSheet_pdf frm = new Frm_MillSheet_pdf(RefNo, RefSeq, Exam_Div, dataRow);
            frm.ShowDialog();

            //밀시트 저장 후 재조회
            //1. 선택한 row 저장
            int SaveFocusedRowInx = (Gc_BfProc.MainView as GridView).FocusedRowHandle;

            //2재조회
            Search_IQC();

            //3. 저장 후 선택한 row를 불러옴
            (Gc_BfProc.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        #endregion

        private void Print()
        {
            /* 10 : 수입검사
             * 20 : 출하검사
             * 30 : 공정검사
             * 40 : 초중종검사
             * 50 : 일상자주검사
             * 60 : 공정조건검사
             */

            GridControl gc = Gc_BfProc as GridControl;
            GridView gv = Gc_BfProc.MainView as GridView;

            int[] selectedRowHandles = gv.GetSelectedRows();
            List<DataRow> targetRows = new List<DataRow>(); // DB 업데이트 대상 DataRow 리스트

            if (selectedRowHandles == null || selectedRowHandles.Length == 0) return;

            XtraReport mainReport = null;

            try
            {
                // 선택된 행들을 순회하면서 레포트 생성 및 병합
                foreach (int rowHandle in selectedRowHandles)
                {
                    // Group Header 등의 행은 제외하고 실제 데이터 행만 처리
                    if (!gv.IsDataRow(rowHandle)) continue;

                    DataRow dr = gv.GetDataRow(rowHandle);
                    if (dr == null || dr["ExamYn"].ConvertString_Null_To_Empty() == "N") continue;

                    string rptNm = "";

                    if (Exam_Div == "10")   // 수입검사
                    {
                        if (WrGlobal.ProcOption["QT"].ContainsKey("IQC_Report"))
                        {
                            rptNm = WrGlobal.ProcOption["QT"]["IQC_Report"];
                        }
                    }
                    if (Exam_Div == "20" || Exam_Div == "25") // 출하검사
                    {
                        if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_Report"))
                        {
                            rptNm = WrGlobal.ProcOption["QT"]["OQC_Report"];
                        }
                    }

                    if (string.IsNullOrEmpty(rptNm)) continue;

                    Assembly assembly = Assembly.GetExecutingAssembly();

                    Type type = assembly.GetTypes()
                        .FirstOrDefault(t => t.Name.Equals(rptNm, StringComparison.OrdinalIgnoreCase)
                                          && typeof(XtraReport).IsAssignableFrom(t));
                    if (type == null)
                        throw new Exception($"레포트({rptNm})를 찾을 수 없습니다.");

                    // 레포트 인스턴스 생성
                    XtraReport subReport = (XtraReport)Activator.CreateInstance(type, dr);

                    if (mainReport == null)
                    {
                        mainReport = subReport;
                        mainReport.CreateDocument();
                    }
                    else
                    {
                        subReport.CreateDocument();
                        mainReport.Pages.AddRange(subReport.Pages);
                    }

                    targetRows.Add(dr);
                }

                if (mainReport != null)
                {
                    mainReport.PrintingSystem.ContinuousPageNumbering = true;

                    using (ReportPrintTool reportTool = new ReportPrintTool(mainReport))
                    {
                        reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
                        reportTool.ShowPreviewDialog();
                    }
                }
                else
                {
                    XtraMessageBox.Show("출력할 레포트 대상이 없습니다.");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"레포트 출력 중 오류가 발생했습니다.\n{ex.Message}");
            }
        }

        private void Print2()
        {
            //GridView gv = Gc_BfProc.MainView as GridView;

            //DataRow dr = gv.GetFocusedDataRow();
            //if (dr == null) return;

            //string _Rst_No = dr["RefNo"].ConvertString_Null_To_Empty();
            //int _Rst_Row_Seq = dr["RefSeq"].ConvertInt_Null_To_Zero();

            //if (Database.DbName.ToUpper().Contains("MYUNGJIN"))
            //{
            //    //공정이동표
            //    PD_ProcessResult Result = new PD_ProcessResult();
            //    DataTable dt = Result.Select_ProcessResult(WrGlobal.CorpId, _Rst_No, _Rst_Row_Seq);

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
           //
           //string ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
           //string processCd = gv.Columns.ColumnByFieldName("ProcessCode") == null ? "" : dr["ProcessCode"].ConvertString_Null_To_Empty();
           //string exam_Div = Exam_Div;
           //if (exam_Div == "11") { exam_Div = "10"; }
           //if (exam_Div == "25" || exam_Div == "26") { exam_Div = "20"; }
           //
           //FrmPictureEdit frm = new FrmPictureEdit(ItemCode, "30", processCd, exam_Div, false, "N");  //30검사기준서
           //frm.ShowDialog();
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}