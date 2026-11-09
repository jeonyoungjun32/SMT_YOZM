using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SmartMES.Models.CM;
using SmartPOP.Models.PM;
using SmartMES.Models.QM;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.BM;
using SmartPOP.Models.CM;
using SmartPOP.Models.MM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace SmartPOP.View.QM
{
	public partial class Frm_QM_ExamResult_Print : DevExpress.XtraEditors.XtraForm
    {
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        clsCode code = new clsCode();
        //CmCateItem cm = new CmCateItem();

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


        public Frm_QM_ExamResult_Print(string exam_Div)
        {
            InitializeComponent();
            switch (exam_Div)
            {
                case "InQC":      Exam_Div = "10"; break;  //수입검사
                case "OutQC":     Exam_Div = "20"; break;  //출하검사 : 생산실적기반
                case "OutQC_Out": Exam_Div = "25"; break;  //출하검사 : 출하내역기반
                case "PrQC":      Exam_Div = "30"; break;  //공정검사
                case "FML":       Exam_Div = "40"; break;  //초중종검사
                case "DSC":       Exam_Div = "50"; break;  //일상자주검사
                case "PCQ":       Exam_Div = "60"; break;  //공정조건검사
                case "Circuit":   Exam_Div = "40"; break;  //순회검사, 초중종과 같음
                case "Daily":     Exam_Div = "80"; break;    //일일검사
            }

            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            //캘린더 크기 설정
            rpo.Set_POP_Dtp(Dt_Date.smtStartDateEdit);
            rpo.Set_POP_Dtp(Dt_Date.smtStartEndEdit);

            //검사구분에따른 설정
            Set_TypeOption();

            //GridView 설정
            Set_GridView();

            Set_Lup();
        }

        private void Set_TypeOption()
        {
            //수입검사
            if(Exam_Div == "10")
            {
                Pnl_Proc.Text = "입고내역";
                //Lbl_ProcDate.Text = "입고일자";

                ItemDiv = new string[] { "40", "50" };

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
            }
            //출하검사 : 생산실적기반
            else if(Exam_Div == "20")
            {
                Pnl_Proc.Text = "생산내역";

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
            }
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
                case "40": SetGrid_ProcCmd_FML(Gv_Cmd_FML); break;  //초중종검사
				case "50": SetGrid_ProcCmd_DSC(Gv_Cmd_DSC); break;  //일상자주검사
                case "60": SetGrid_PCQ(Gv_PCQ); break;  //공정조건검사
                case "80": SetGrid_Daily(Gv_Daily); break;  //일일검사
            }

            SetGrid_ExRst(Gv_ExRst);
        }

        private void SetGrid_MatIn(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //품목분류1
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210", new string[] { "40,50" }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220", new string[] { "40,50"}), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230", new string[] { "40,50" }), "코드", "명칭");

            grid.Create_GvColumn(gv, "검사여부", "ExamYn");
            grid.Create_GvColumn(gv, "밀시트", "In_FilePath");
            grid.Create_GvColumn(gv, "입고번호", "Ref_No");
            grid.Create_GvColumn(gv, "입고번호", "Ref_No_Seq");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "자재Lot No", "LotNo");
            grid.Create_GvColumn(gv, "업체Lot No", "CustLotNo");
            grid.Create_GvColumn(gv, "검사수량", "Exam_Qty");
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
            grid.SetCol_Type(gv.Columns["Exam_Qty"], cls_Grid.TypeList.TFloat);

            //정렬
            gv.Columns["In_FilePath"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;
            if(gv.Columns["MaterialClass1"] != null) { gv.Columns["MaterialClass1"].ColumnEdit = lup_Class_Cd1; }
            if(gv.Columns["MaterialClass2"] != null) { gv.Columns["MaterialClass2"].ColumnEdit = lup_Class_Cd1; }
            if(gv.Columns["MaterialClass3"] != null) { gv.Columns["MaterialClass3"].ColumnEdit = lup_Class_Cd1; }

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

            gv.Columns["Ref_No"].Visible = false;
            gv.Columns["Ref_No_Seq"].Visible = false;
            gv.Columns["CustCode"].Visible = false;
            gv.Columns["MaterialCode"].Visible = false;
            gv.Columns["In_FilePath"].Visible = false;

            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreIP"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModIP"].Visible = false;
            gv.Columns["ModDt"].Visible = false;

            grid.SetPOPGrid(gv);
        }

        private void SetGrid_ProdRst(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            grid.Create_GvColumn(gv, "실적번호", "Ref_No");
            grid.Create_GvColumn(gv, "실적번호", "Ref_No_Seq");

            grid.Create_GvColumn(gv, "라인코드", "Line_Cd");
            grid.Create_GvColumn(gv, "라인명", "Line_Nm");
            grid.Create_GvColumn(gv, "공정코드", "Process_Cd");
            grid.Create_GvColumn(gv, "공정명", "Process_Nm");
            grid.Create_GvColumn(gv, "설비코드", "Machine_Cd");
            grid.Create_GvColumn(gv, "설비명", "Machine_Nm");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "생산수량", "Exam_Qty");
            grid.Create_GvColumn(gv, "비고", "Rmk");
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
            grid.Column_AllowEdit(gv.Columns["Process_Cd"], false);
            grid.Column_AllowEdit(gv.Columns["Machine_Cd"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Exam_Qty"], cls_Grid.TypeList.TFloat);

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            gv.Columns["Line_Cd"].Visible = false;
            gv.Columns["Process_Cd"].Visible = false;
            gv.Columns["Machine_Cd"].Visible = false;
            gv.Columns["CustCode"].Visible = false;
            gv.Columns["CustName"].Visible = false;

            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreIP"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModIP"].Visible = false;
            gv.Columns["ModDt"].Visible = false;

            //if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["Ref_No"].Visible = false;
                gv.Columns["Ref_No_Seq"].Visible = false;
                gv.Columns["CustCode"].Visible = false;
                gv.Columns["Item_Cd"].Visible = false;
            }
        }

        private void SetGrid_ProdOut(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            grid.Create_GvColumn(gv, "검사여부", "ExamYn");
            grid.Create_GvColumn(gv, "출하번호", "RefNo");
            grid.Create_GvColumn(gv, "출하순번", "RefSeq");

            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

            //grid.Create_GvColumn(gv, "LotNo", "PdLot_No");

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
            grid.SetCol_Type(gv.Columns["ExamQty"], cls_Grid.TypeList.TFloat);

			//등록,수정자
			RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류123
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210", new string[] { "40,50" }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220", new string[] { "40,50" }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230", new string[] { "40,50" }), "코드", "명칭");

            if (gv.Columns["MaterialClass1"] != null) { gv.Columns["MaterialClass1"].ColumnEdit = lup_Class_Cd1; }
            if (gv.Columns["MaterialClass2"] != null) { gv.Columns["MaterialClass2"].ColumnEdit = lup_Class_Cd1; }
            if (gv.Columns["MaterialClass3"] != null) { gv.Columns["MaterialClass3"].ColumnEdit = lup_Class_Cd1; }

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
			gv.Columns["ModId"].ColumnEdit = lup_Emp_No;
            gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            gv.Columns["RefNo"].Visible = false;
            gv.Columns["RefSeq"].Visible = false;
            gv.Columns["BillToCode"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;
            gv.Columns["SdOrderNo"].Visible = false;
            gv.Columns["SdOrderSeq"].Visible = false;
            gv.Columns["ItemClass1"].Visible = false;

            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreIP"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModIP"].Visible = false;
            gv.Columns["ModDt"].Visible = false;

            grid.SetPOPGrid(gv);
        }

        private void SetGrid_PrQC(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "검사여부", "ExamYn");

            grid.Create_GvColumn(gv, "실적번호", "Ref_No");
            grid.Create_GvColumn(gv, "실적순번", "Ref_No_Seq");

            grid.Create_GvColumn(gv, "생산일자", "WorkDate");

            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "라인명", "LineName");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "검사수량", "Exam_Qty");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["MaterialName"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Exam_Qty"], cls_Grid.TypeList.TFloat);
            gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.Columns["ExamYn"].MaxWidth = 80;

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            //if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["Ref_No"].Visible = false;
                gv.Columns["Ref_No_Seq"].Visible = false;
                gv.Columns["LineCode"].Visible = false;
                gv.Columns["ProcessCode"].Visible = false;
                gv.Columns["MachineCode"].Visible = false;
                gv.Columns["MaterialCode"].Visible = false;

                gv.Columns["CreId"].Visible = false;
                gv.Columns["CreIP"].Visible = false;
                gv.Columns["CreDt"].Visible = false;
                gv.Columns["ModId"].Visible = false;
                gv.Columns["ModIP"].Visible = false;
                gv.Columns["ModDt"].Visible = false;
            }

            grid.SetPOPGrid(gv);
        }

        private void SetGrid_ProcCmd_FML(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "검사여부", "ExamYn");

            grid.Create_GvColumn(gv, "실적번호", "Ref_No");
            grid.Create_GvColumn(gv, "실적순번", "Ref_No_Seq");

            grid.Create_GvColumn(gv, "생산일자", "WorkDate");

            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "라인명", "LineName");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "검사수량", "Exam_Qty");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["MaterialName"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Exam_Qty"], cls_Grid.TypeList.TFloat);
            gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.Columns["ExamYn"].MaxWidth = 80;

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            //if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["Ref_No"].Visible = false;
                gv.Columns["Ref_No_Seq"].Visible = false;
                gv.Columns["LineCode"].Visible = false;
                gv.Columns["ProcessCode"].Visible = false;
                gv.Columns["MachineCode"].Visible = false;
                gv.Columns["MaterialCode"].Visible = false;

                gv.Columns["CreId"].Visible = false;
                gv.Columns["CreIP"].Visible = false;
                gv.Columns["CreDt"].Visible = false;
                gv.Columns["ModId"].Visible = false;
                gv.Columns["ModIP"].Visible = false;
                gv.Columns["ModDt"].Visible = false;
            }

            grid.SetPOPGrid(gv);
        }

        private void SetGrid_ProcCmd_DSC(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            grid.Create_GvColumn(gv, "실적번호", "Ref_No");
            grid.Create_GvColumn(gv, "실적순번", "Ref_No_Seq");

            grid.Create_GvColumn(gv, "라인코드", "Line_Cd");
            grid.Create_GvColumn(gv, "라인명", "Line_Nm");
            grid.Create_GvColumn(gv, "공정코드", "Process_Cd");
            grid.Create_GvColumn(gv, "공정명", "Process_Nm");
            grid.Create_GvColumn(gv, "설비코드", "Machine_Cd");
            grid.Create_GvColumn(gv, "설비명", "Machine_Nm");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "검사수량", "Exam_Qty");
            grid.Create_GvColumn(gv, "비고", "Rmk");
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
            grid.Column_AllowEdit(gv.Columns["Process_Cd"], false);
            grid.Column_AllowEdit(gv.Columns["Machine_Cd"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Exam_Qty"], cls_Grid.TypeList.TFloat);

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            //if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["Ref_No"].Visible = false;
                gv.Columns["Ref_No_Seq"].Visible = false;
                gv.Columns["CustCode"].Visible = false;
                gv.Columns["MaterialCode"].Visible = false;

                gv.Columns["CreId"].Visible = false;
                gv.Columns["CreIP"].Visible = false;
                gv.Columns["CreDt"].Visible = false;
                gv.Columns["ModId"].Visible = false;
                gv.Columns["ModIP"].Visible = false;
                gv.Columns["ModDt"].Visible = false;
            }

            grid.SetPOPGrid(gv);
        }

        private void SetGrid_PCQ(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            grid.Create_GvColumn(gv, "실적번호", "Ref_No");
            grid.Create_GvColumn(gv, "실적순번", "Ref_No_Seq");

            grid.Create_GvColumn(gv, "라인코드", "Line_Cd");
            grid.Create_GvColumn(gv, "라인명", "Line_Nm");
            grid.Create_GvColumn(gv, "공정코드", "Process_Cd");
            grid.Create_GvColumn(gv, "공정명", "Process_Nm");
            grid.Create_GvColumn(gv, "설비코드", "Machine_Cd");
            grid.Create_GvColumn(gv, "설비명", "Machine_Nm");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "검사수량", "Exam_Qty");
            grid.Create_GvColumn(gv, "비고", "Rmk");
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
            grid.Column_AllowEdit(gv.Columns["Process_Cd"], false);
            grid.Column_AllowEdit(gv.Columns["Machine_Cd"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Exam_Qty"], cls_Grid.TypeList.TFloat);

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            //if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["Ref_No"].Visible = false;
                gv.Columns["Ref_No_Seq"].Visible = false;
                gv.Columns["CustCode"].Visible = false;
                gv.Columns["MaterialCode"].Visible = false;

                gv.Columns["CreId"].Visible = false;
                gv.Columns["CreIP"].Visible = false;
                gv.Columns["CreDt"].Visible = false;
                gv.Columns["ModId"].Visible = false;
                gv.Columns["ModIP"].Visible = false;
                gv.Columns["ModDt"].Visible = false;
            }

            grid.SetPOPGrid(gv);
        }

        private void SetGrid_Daily(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "검사여부", "ExamYn");

            grid.Create_GvColumn(gv, "실적번호", "Ref_No");
            grid.Create_GvColumn(gv, "실적순번", "Ref_No_Seq");

            grid.Create_GvColumn(gv, "생산일자", "WorkDate");

            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "라인명", "LineName");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "검사수량", "Exam_Qty");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["MaterialName"], false);

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Exam_Qty"], cls_Grid.TypeList.TFloat);
            gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.Columns["ExamYn"].MaxWidth = 80;

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;

            //if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["Ref_No"].Visible = false;
                gv.Columns["Ref_No_Seq"].Visible = false;
                gv.Columns["LineCode"].Visible = false;
                gv.Columns["ProcessCode"].Visible = false;
                gv.Columns["MachineCode"].Visible = false;
                gv.Columns["MaterialCode"].Visible = false;

                gv.Columns["CreId"].Visible = false;
                gv.Columns["CreIP"].Visible = false;
                gv.Columns["CreDt"].Visible = false;
                gv.Columns["ModId"].Visible = false;
                gv.Columns["ModIP"].Visible = false;
                gv.Columns["ModDt"].Visible = false;
            }

            grid.SetPOPGrid(gv);
        }

        private void GvBfProc_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            Search_QC_List();
        }

        private void SetGrid_ExRst(GridView gv)
        {
            //합/불검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "검사코드", "Exam_Cd");
            grid.Create_GvColumn(gv, "검사명", "Exam_Nm");
            grid.Create_GvColumn(gv, "검사규격", "Exam_Spec");
            grid.Create_GvColumn(gv, "검사방법", "Exam_Method");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

            grid.Create_GvColumn(gv, "판정", "Pass_Div_Cd");
            grid.Create_GvColumn(gv, "조치내역", "Action_Memo");
            grid.Create_GvColumn(gv, "비고", "Rmk");
            grid.Create_GvColumn(gv, "등록자", "Ins_Emp_No");
            grid.Create_GvColumn(gv, "등록일시", "Ins_Date_Time");
            grid.Create_GvColumn(gv, "수정자", "Upd_Emp_No");
            grid.Create_GvColumn(gv, "수정일시", "Upd_Date_Time");

            grid.Create_GvColumn(gv, "검사번호", "Exam_No");
            grid.Create_GvColumn(gv, "검사순번", "Exam_No_Seq");


            //등록자, 수정자 column 숨김
            gv.Columns["Exam_Cd"].Visible = false;
            gv.Columns["Ins_Emp_No"].Visible = false;
            gv.Columns["Ins_Date_Time"].Visible = false;
            gv.Columns["Upd_Emp_No"].Visible = false;
            gv.Columns["Upd_Date_Time"].Visible = false;

            //Repository 셋팅
            gv.Columns["Pass_Div_Cd"].ColumnEdit = rpo.Create_RepositoryItem(code.GetCateItem(WrGlobal.CorpID, "Q020"), "코드", "명칭");

            //시료 판정
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("코드"), new DataColumn("명칭") });
            dt.Rows.Add("Y", "OK");
            dt.Rows.Add("N", "NG");

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["Exam_Nm"], false);

            //이벤트
            gv.CustomDrawCell += Event_Cell_ForeColor;   //불합격이면 글자색 빨강

            if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["Exam_No"].Visible = false;
                gv.Columns["Exam_No_Seq"].Visible = false;
            }

            grid.SetPOPGrid(gv);
        }

        private void Event_CellValueChagned_Bool(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            string pass_Div_Cd = "10"; //10:합격 20:불합격

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
                    case 1: if (x1 == "N") pass_Div_Cd = "20"; break;
                    case 2: if (x1 == "N") pass_Div_Cd = "20"; if (x2 == "N") pass_Div_Cd = "20"; break;
                    case 3: if (x1 == "N") pass_Div_Cd = "20"; if (x2 == "N") pass_Div_Cd = "20"; if (x3 == "N") pass_Div_Cd = "20"; break;
                    case 4: if (x1 == "N") pass_Div_Cd = "20"; if (x2 == "N") pass_Div_Cd = "20"; if (x3 == "N") pass_Div_Cd = "20"; if (x4 == "N") pass_Div_Cd = "20"; break;
                    case 5: if (x1 == "N") pass_Div_Cd = "20"; if (x2 == "N") pass_Div_Cd = "20"; if (x3 == "N") pass_Div_Cd = "20"; if (x4 == "N") pass_Div_Cd = "20"; if (x5 == "N") pass_Div_Cd = "20"; break;
                    case 6: if (x1 == "N") pass_Div_Cd = "20"; if (x2 == "N") pass_Div_Cd = "20"; if (x3 == "N") pass_Div_Cd = "20"; if (x4 == "N") pass_Div_Cd = "20"; if (x5 == "N") pass_Div_Cd = "20"; if (x6 == "N") pass_Div_Cd = "20"; break;
                    default: break;
                }

                gv.SetFocusedRowCellValue("Pass_Div_Cd", pass_Div_Cd);
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

            else if (fieldName == "Pass_Div_Cd")
            {
                string passDivCd = gv.GetRowCellValue(e.RowHandle, "Pass_Div_Cd")?.ToString();

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
            grid.Create_GvColumn(gv, "검사순번", "ExamNoSeq");

            //등록자, 수정자 column 숨김
            gv.Columns["ExamCd"].Visible = false;
            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModDt"].Visible = false;

            //Repository 셋팅
            gv.Columns["PassDivCd"].ColumnEdit = rpo.Create_RepositoryItem(code.GetCateItem(WrGlobal.CorpID, "Q020"), "코드", "명칭");

            //셀값 변경 허용
            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Column_AllowEdit(gv.Columns["X" + i]);
            }

            grid.Column_AllowEdit(gv.Columns["PassDivCd"]);
            grid.Column_AllowEdit(gv.Columns["Action_Memo"]);
            grid.Column_AllowEdit(gv.Columns["Rmk"]);

            grid.SetCol_Type(gv.Columns["MgntMin"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["MgntMax"], cls_Grid.TypeList.TFloat);

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

            string pass_Div_Cd = "10";

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
                    case 1: if (!Chk_Num(dr, x1)) pass_Div_Cd = "20"; break;
                    case 2: if (!Chk_Num(dr, x1)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x2)) pass_Div_Cd = "20";break;
                    case 3: if (!Chk_Num(dr, x1)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x2)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x3)) pass_Div_Cd = "20"; break;
                    case 4: if (!Chk_Num(dr, x1)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x2)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x3)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x4)) pass_Div_Cd = "20"; break;
                    case 5: if (!Chk_Num(dr, x1)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x2)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x3)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x4)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x5)) pass_Div_Cd = "20";break;
                    case 6: if (!Chk_Num(dr, x1)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x2)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x3)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x4)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x5)) pass_Div_Cd = "20"; if (!Chk_Num(dr, x6)) pass_Div_Cd = "20"; break;
                    default: break;
                }

                gv.SetFocusedRowCellValue("PassDivCd", pass_Div_Cd);
            }
        }

        private bool Chk_Num(DataRow dr, string value)
        {
            bool result = true;

            if (value == "")
            {
                return result;
            }

            float mgnt_Min = dr["MgntMin"].ConvertFloat_Null_To_Zero();
            float mgnt_Max = dr["MgntMax"].ConvertFloat_Null_To_Zero();
            float danger_Min = dr["DangerMin"].ConvertFloat_Null_To_Zero();
            float danger_Max = dr["DangerMax"].ConvertFloat_Null_To_Zero();
            float xValue = value.ConvertFloat_Null_To_Zero();

            //관리범위 오버 검사
            //상한만 있을때
            if (mgnt_Min == 0 && mgnt_Max > 0)
            {
                if (xValue > mgnt_Max)
                {
                    //값이 상한보다 높으면 불합 처리
                    result = false;
                }
            }
            //하한만 있을때
            else if (mgnt_Min > 0 && mgnt_Max == 0)
            {
                if (xValue < mgnt_Min)
                {
                    //값이 하한보다 낮으면 불합 처리
                    result = false;
                }
            }
            //상,하한 둘다 있을때
            else if (mgnt_Min > 0 && mgnt_Min < mgnt_Max)
            {
                if (xValue < mgnt_Min || xValue > mgnt_Max)
                {
                    //값이 하한보다 낮거나 상한보다 높으면 불합 처리
                    result = false;
                }
            }

            //위험범위 오버 검사
            //상한만 있을때
            if (danger_Min == 0 && danger_Max > 0)
            {
                if (xValue > danger_Max)
                {
                    //값이 상한보다 높으면 불합 처리
                    result = false;
                }
            }
            //하한만 있을때
            else if (danger_Min > 0 && danger_Max == 0)
            {
                if (xValue < danger_Min)
                {
                    //값이 하한보다 낮으면 불합 처리
                    result = false;
                }
            }
            //상,하한 둘다 있을때
            else if (danger_Min > 0 && danger_Min < danger_Max)
            {
                if (xValue < danger_Min || xValue > danger_Max)
                {
                    //값이 하한보다 낮거나 상한보다 높으면 불합 처리
                    result = false;
                }
            }

            return result;
        }

        #endregion GridView 셋팅 End

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

   //         if (ProcessCds.Count > 0)
   //         {
   //             dt_process = code.SelectCM_Process_ByCode(true, ProcessCds.ToArray());
			//}
			//else
			//{
                dt_process = code.GetProcess_List_ProcessType(true, null);
            //}

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
            LupItem.EditValue = "All";
            LupItem.Properties.PopupFormSize = new System.Drawing.Size(1000, 0);

            //거래처
            DataTable dt_Cust = code.GetCustomerList(CustDiv, true);
            clsLup.Create_RepositoryItemSearchLookUp(LupCust, dt_Cust, "거래처코드", "거래처명");
            LupCust.EditValue = "ALL";
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
            //BS_MachineProc machineProc = new BS_MachineProc();
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
                    //machineProc.Process_Cd = LupValue;
                    //DataTable dt_machine = machineProc.SearchLookUp_Select_MachineProc(true);
                    //clsLup.Create_RepositoryItemSearchLookUp(LupMachine, dt_machine, "코드", "명칭");
                    //LupMachine.EditValue = "ALL";

                    //품목 리스트 변경
                    //item.Process_Cd = LupValue;
                    //DataTable dt_ProcItem = item.Select_Item_ByProcess(new string[] { Rdo_Div.EditValue.ConvertString_Null_To_Empty() }, true);
                    //clsLup.Create_RepositoryItemSearchLookUp(LupItem, dt_ProcItem, "Item_Cd", "Item_Nm");
                    //grid.SetLupView_Item(LupItem, "제품코드 초기화");
                    //LupItem.EditValue = "ALL";
                    break;
            }
        }

        private void LupMeasure_Div_EditValueChanged(object sender, EventArgs e)
        {
            Search_QC_List();
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

        private void Search_IQC()
        {
            Gc_BfProc.MainView = Gv_MatIn;
            GridView gv = Gc_BfProc.MainView as GridView;

            string startDate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-dd");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("yyyy-MM-dd 23:59");
            string lupItem = LupItem.EditValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();

            MM_PurchaseD pd = new MM_PurchaseD();
            pd.CorpId = WrGlobal.CorpID;
            pd.MaterialCode = lupItem;

            Gc_BfProc.DataSource = pd.SelectMM_PurchaseD_Exam(startDate, endDate, "");

            //gv.Columns["In_FilePath"].Visible = BtnMillSheet.Visible;

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

            BM_SalesD sd = new BM_SalesD();

            if (g_Rst_No != "" || G_Rst_Row_Seq != 0)
            {
                sd.SalesNo = g_Rst_No;
                sd.SalesSeq = G_Rst_Row_Seq;
            }

            Gc_BfProc.DataSource = sd.Select_Sales_ExamRst(startDate, endDate, null, "20");

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

            string line_Cd = LupLine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupLine.EditValue.ConvertString_Null_To_Empty();
            string process_Cd = LupProcess.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupProcess.EditValue.ConvertString_Null_To_Empty();
            string machine_Cd = LupMachine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupMachine.EditValue.ConvertString_Null_To_Empty() ;
            string item_Cd = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();

            PM_OrderResult or = new PM_OrderResult();
            or.LineCode = line_Cd;
            or.ProcessCode = process_Cd;
            or.MachineCode = machine_Cd;
            or.MaterialCode = item_Cd;

            DataTable dt_ProcResult = null;

            if (g_Rst_No == "" && G_Rst_Row_Seq == 0)
            {
                dt_ProcResult = or.Select_PM_OrderResult_Exam(Exam_Div, startDate, endDate, null, ProcessCds);
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

        private void Search_Daily()
        {
            //일일검사
            Gc_BfProc.MainView = Gv_Daily;
            GridView gv = Gc_BfProc.MainView as GridView;

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char8();

            string line_Cd = LupLine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupLine.EditValue.ConvertString_Null_To_Empty();
            string process_Cd = LupProcess.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupProcess.EditValue.ConvertString_Null_To_Empty();
            string machine_Cd = LupMachine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupMachine.EditValue.ConvertString_Null_To_Empty();
            string item_Cd = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();

            PM_OrderResult or = new PM_OrderResult();
            or.LineCode = line_Cd;
            or.ProcessCode = process_Cd;
            or.MachineCode = machine_Cd;
            or.MaterialCode = item_Cd;

            Gc_BfProc.DataSource = or.Select_PM_OrderResult_Exam(Exam_Div, startDate, endDate, null, ProcessCds);
            gv.BestFitColumns();
        }

        private void Search_QC_List()
        {
            //검사목록 불러오기
            GridView gv = Gc_BfProc.MainView as GridView;
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

             string Item_Cd = Convert.ToString(dr["ItemCode"]);
            string div = Exam_Div;
            if (Exam_Div == "25") { div = "20"; }

            string Process_Cd = null;

            //컬럼 존재 유무 확인
            if (dr.Table.Columns.Contains("ProcessCode"))
			{
                Process_Cd = Convert.ToString(dr["ProcessCode"]);
            }

            QM_ExamResult qer = new QM_ExamResult();
            qer.Ref_No = Convert.ToString(dr["RefNo"]);
            qer.Ref_No_Seq = dr["RefSeq"].ConvertInt_Null_To_Zero();
            qer.Exam_Div_Cd = div;           //검사 구분
            qer.Measure_Div_Cd = ""; //측정 구분

            Gc_ExRst.DataSource = qer.Select_ExamResult();

            Gv_ExRst.BestFitColumns();
        }

        #endregion 데이터 조회 End


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

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

			string rptNm = "";

			if (Exam_Div == "10")   // 수입검사
			{
				if (WrGlobal.ProcOption["QT"].ContainsKey("IQC_Report"))
				{
					rptNm = WrGlobal.ProcOption["QT"]["IQC_Report"];
				}
                //올덴 - 개발하는동안 임시로
                if(Database.DbName == "ALLTHEN")
                {
                    //자재기준정보에서 품목분류를 뒀으면 A양식, 안했으면 B양식 나오도록
                    if (dr["MaterialClass1"].ConvertString_Null_To_Empty() != "")
                    {
                        rptNm = "RptIQC_ALLTHEN_A";
                    }
                    else
                    {
                        rptNm = "RptIQC_ALLTHEN_B";
                    }
                }
			}
			if (Exam_Div == "20" || Exam_Div == "25") // 출하검사
            {
				if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_Report"))
				{
					rptNm = WrGlobal.ProcOption["QT"]["OQC_Report"];
				}
			}

			try
			{
                // 현재 실행중인 Assembly 가져오기
                Assembly assembly = Assembly.GetExecutingAssembly();

                // reportName 과 일치하는 타입 찾기
                Type type = assembly.GetTypes()
                    .FirstOrDefault(t => t.Name.Equals(rptNm, StringComparison.OrdinalIgnoreCase)
                                      && typeof(XtraReport).IsAssignableFrom(t));
                if (type == null)
                    throw new Exception($"레포트를 찾을 수 없습니다.");

                // 인스턴스 생성
                XtraReport report = (XtraReport)Activator.CreateInstance(type, dr);

                using (ReportPrintTool reportTool = new ReportPrintTool(report))
                {
                    reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
                    reportTool.ShowPreviewDialog();
                }
            }
			catch
			{
                XtraMessageBox.Show("레포트를 찾을 수 없습니다.");
            }
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
           //
           //string MaterialCode = dr["MaterialCode"].ConvertString_Null_To_Empty();
           //string processCd = gv.Columns.ColumnByFieldName("Process_Cd") == null ? "" : dr["Process_Cd"].ConvertString_Null_To_Empty();
           //string exam_Div = Exam_Div;
           //if (exam_Div == "11") { exam_Div = "10"; }
           //if (exam_Div == "25" || exam_Div == "26") { exam_Div = "20"; }
           //
           //FrmPictureEdit frm = new FrmPictureEdit(MaterialCode, "30", processCd, exam_Div, false, "N");  //30검사기준서
           //frm.ShowDialog();
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}