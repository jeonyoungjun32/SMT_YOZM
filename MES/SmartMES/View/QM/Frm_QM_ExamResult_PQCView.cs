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

namespace SmartMES.View.QM
{
	public partial class Frm_QM_ExamResult_PQCView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();

        private string[] ItemDiv = { "40", "50", "60" }; //품목구분 리스트(10 : 상품, 20 : 제품, 30 : 반제품, 40 : 원자재, 50 : 부자재, 60 : 소모자재)

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

        public Frm_QM_ExamResult_PQCView()
        {
            InitializeComponent();

            Exam_Div = "30";
            SetDesign();
        }

        public Frm_QM_ExamResult_PQCView(string exam_Div)
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

            }

            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            //업체별 옵션
            Set_CompanyOption();

            //검사구분에따른 설정
            Set_TypeOption();

            //GridView 설정
            Set_GridView();

            Set_Lup();
        }

        private void Set_CompanyOption()
        {
            //if (Database.DbName.Contains("RNDZIPPER"))
            //{ 
                //cmdMillSheet.Visible = false;   // 밀시트
            //}
        }

        private void Set_TypeOption()
        {
            //수입검사
            if (Exam_Div == "10")
            {
                ItemDiv = new string[] { "40", "50" };


                //검사별 검사 횟수 
                if (WrGlobal.ProcOption["QT"].ContainsKey("IQC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["IQC_ExamCnt"].ConvertInt_Null_To_Zero();
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
            else if (Exam_Div == "20")
            {
                ItemDiv = new string[] { "20" };

                //검사별 검사 횟수 
                if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["OQC_ExamCnt"].ConvertInt_Null_To_Zero();
                }
            }
            //출하검사 : 출하기반
            else if (Exam_Div == "25")
            {
                ItemDiv = new string[] { "20" };

                if (Database.DbName.Contains("GJY")) ItemDiv = new string[] { "40", "50" };

                //검사별 검사 횟수 
                if (WrGlobal.ProcOption["QT"].ContainsKey("OQC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["OQC_ExamCnt"].ConvertInt_Null_To_Zero();
                }
            }
            //공정검사
            else if (Exam_Div == "30")
            {
                ItemDiv = new string[] {"30" };

                //검사별 검사 횟수 
                if (WrGlobal.ProcOption["QT"].ContainsKey("PQC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["PQC_ExamCnt"].ConvertInt_Null_To_Zero();
                }
            }
            //초중종검사
            else if (Exam_Div == "40")
            {
                ItemDiv = new string[] { "20", "30" };

                //검사별 검사 횟수 
                if (WrGlobal.ProcOption["QT"].ContainsKey("FML_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["FML_ExamCnt"].ConvertInt_Null_To_Zero();
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
                ItemDiv = new string[] { "20", "30" };

                //검사별 검사 횟수 
                if (WrGlobal.ProcOption["QT"].ContainsKey("DSC_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["DSC_ExamCnt"].ConvertInt_Null_To_Zero();
                }
            }
            //공정조건검사
            else if (Exam_Div == "60")
            {
                ItemDiv = new string[] { "20", "30" };

                //검사별 검사 횟수 
                if (WrGlobal.ProcOption["QT"].ContainsKey("PCQ_ExamCnt"))
                {
                    ExamCnt = WrGlobal.ProcOption["QT"]["PCQ_ExamCnt"].ConvertInt_Null_To_Zero();
                }
            }
        }

        #region GridView 셋팅

        private void Set_GridView()
        {
            switch (Exam_Div)
            {
				//case "10": SetGrid_MatIn(Gv_MatIn); break;   //수입검사
				//case "20": SetGrid_ProdRst(Gv_ProdRst); break;  //출하검사:생산실적기반
				case "25": SetGrid_ProdOut(Gv_ProdOut); break;  //출하검사:출하내역기반
				case "30": SetGrid_PrQC(Gv_PrQC); break;  //공정검사
				case "40": SetGrid_ProcCmd_FML(Gv_Cmd_FML); break;  //초중종검사
				case "50": SetGrid_ProcCmd_DSC(Gv_Cmd_DSC); break;  //일상자주검사
				case "60": SetGrid_PCQ(Gv_PCQ); break;  //공정조건검사
			}
        }

        private void SetGrid_ProdOut(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "출하번호", "RefNo");
            grid.Create_GvColumn(gv, "출하순번", "RefSeq");

            grid.Create_GvColumn(gv, "검사번호", "ExamNo");
            grid.Create_GvColumn(gv, "검사순번", "ExamSeq");
            grid.Create_GvColumn(gv, "검사일자", "ExamDate");

            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

			//grid.Create_GvColumn(gv, "측정구분", "MeasureDivCd", lup_Item_Div_Cd);

            grid.Create_GvColumn(gv, "검사명", "Title");
			grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

            grid.Create_GvColumn(gv, "조치내역", "ActionMemo");
            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");


            //VISIBLE
            //gv.Columns["MeasureDivCd"].Visible = false;

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["Cust_Nm"], false);


            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);
        }

        private void SetGrid_PrQC(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "검사번호", "ExamNo");
            grid.Create_GvColumn(gv, "검사순번", "ExamSeq");
            grid.Create_GvColumn(gv, "검사일자", "ExamDate");

            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "작업순번", "SubSeq");
            grid.Create_GvColumn(gv, "시리얼번호", "RefNo");
            //grid.Create_GvColumn(gv, "거래처코드", "Cust_Cd");
            //grid.Create_GvColumn(gv, "거래처명", "Cust_Nm");

            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
			grid.Create_GvColumn(gv, "라인코드", "LineCode");
			grid.Create_GvColumn(gv, "라인명", "LineName");


            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

			//grid.Create_GvColumn(gv, "측정구분", "MeasureDivCd", lup_Item_Div_Cd);

			grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사명", "Title");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

            grid.Create_GvColumn(gv, "조치내역", "ActionMemo");
            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");


            //VISIBLE
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["LineCode"].Visible = false;
            gv.Columns["LineName"].Visible = false;

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["Cust_Nm"], false);


            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);
        }

        private void SetGrid_ProcCmd_FML(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //측정구분 초중종 Q01110,일상자주 Q01120,공정조건 Q01130
            RepositoryItemSearchLookUpEdit lupMeasureDiv = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "Q01110"), "코드", "명칭");	


            grid.Create_GvColumn(gv, "검사번호", "ExamNo");
            grid.Create_GvColumn(gv, "검사순번", "ExamSeq");

            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "작업순번", "SubSeq");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            //grid.Create_GvColumn(gv, "거래처코드", "Cust_Cd");
            //grid.Create_GvColumn(gv, "거래처명", "Cust_Nm");

            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "라인명", "LineName");


            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");

			grid.Create_GvColumn(gv, "측정구분", "MeasureDivCd", lupMeasureDiv);

			grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사명", "Title");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

            grid.Create_GvColumn(gv, "조치내역", "ActionMemo");
            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");


            //VISIBLE
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["LineCode"].Visible = false;
            gv.Columns["LineName"].Visible = false;

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["Cust_Nm"], false);


            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);
        }

        private void SetGrid_ProcCmd_DSC(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //측정구분 초중종 Q01110,일상자주 Q01120,공정조건 Q01130
            RepositoryItemSearchLookUpEdit lupMeasureDiv = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "Q01110"), "코드", "명칭");

            grid.Create_GvColumn(gv, "검사번호", "ExamNo");
            grid.Create_GvColumn(gv, "검사순번", "ExamSeq");

            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "작업순번", "SubSeq");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            //grid.Create_GvColumn(gv, "거래처코드", "Cust_Cd");
            //grid.Create_GvColumn(gv, "거래처명", "Cust_Nm");

            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "라인명", "LineName");


            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");

			grid.Create_GvColumn(gv, "측정구분", "MeasureDivCd", lupMeasureDiv);

			grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사명", "Title");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

            grid.Create_GvColumn(gv, "조치내역", "ActionMemo");
            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");


            //VISIBLE
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["LineCode"].Visible = false;
            gv.Columns["LineName"].Visible = false;

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["Cust_Nm"], false);


            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);
        }

        private void SetGrid_PCQ(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //측정구분 초중종 Q01110,일상자주 Q01120,공정조건 Q01130
            RepositoryItemSearchLookUpEdit lupMeasureDiv = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "Q01110"), "코드", "명칭");


            grid.Create_GvColumn(gv, "검사번호", "ExamNo");
            grid.Create_GvColumn(gv, "검사순번", "ExamSeq");

            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "작업순번", "SubSeq");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            //grid.Create_GvColumn(gv, "거래처코드", "Cust_Cd");
            //grid.Create_GvColumn(gv, "거래처명", "Cust_Nm");

            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "라인명", "LineName");


            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");

			grid.Create_GvColumn(gv, "측정구분", "MeasureDivCd", lupMeasureDiv);

			grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사명", "Title");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

            grid.Create_GvColumn(gv, "조치내역", "ActionMemo");
            grid.Create_GvColumn(gv, "검사수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");


            //VISIBLE
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["LineCode"].Visible = false;
            gv.Columns["LineName"].Visible = false;

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["Cust_Nm"], false);


            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);
        }

        private void Set_GridView_Defect(GridView gv)
		{
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "작업순번", "SubSeq");
            grid.Create_GvColumn(gv, "ReworkID", "ReworkID");

            grid.Create_GvColumn(gv, "불량코드", "DefectCode");
            grid.Create_GvColumn(gv, "불량명", "Title");
            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "비고", "Remark");


			//VISIBLE
			gv.Columns["OrderNo"].Visible = false;
			gv.Columns["SerialNo"].Visible = false;
			gv.Columns["Seq"].Visible = false;
			gv.Columns["SubSeq"].Visible = false;
			gv.Columns["DefectCode"].Visible = false;
			gv.Columns["ReworkID"].Visible = false;


			////셀값 변경 허용
			grid.Column_AllowEdit(gv.Columns["Qty"], true);
			grid.Column_AllowEdit(gv.Columns["Remark"], true);

			//컬럼 타입 설정
			grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
		}

        #endregion GridView 셋팅 End

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

            //clsLup.Create_RepositoryItemSearchLookUp(LupLine, dt_line, "라인코드", "라인명"); ;
			//LupLine.EditValue = "ALL";

			//공정
			DataTable dt_process = null;

            dt_process = code.GetProcess_List_ProcessType(true, new string[] { "10" }); //가공공정만


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
            //DataTable dt_Cust = code.GetCustomerList(CustDiv, true);
			//clsLup.Create_RepositoryItemSearchLookUp(LupCust, dt_Cust, "거래처코드", "거래처명");
			//LupCust.EditValue = "ALL";
		}

        #endregion

        private void Frm_BN_Order_Load(object sender, EventArgs e)
        {
            Search_BfProc();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            g_Rst_No = "";
            G_Rst_Row_Seq = 0;
            Search_BfProc();
        }

        #region 데이터 조회

        public void Search_BfProc()
        {
            this.Validate();

            //이전 프로세스 작업내역 조회
            switch (Exam_Div)
            {
				//case "10": Search_IQC(); break;   //수입검사:입고내역 조회
				//case "20": Search_OQC(); break;   //출하검사:생산실적 조회
				case "25": Search_OQC_Out(); break;   //출하검사:출하내역 조회
				case "30": Search_ProcQCInsp(); break;   //공정검사
				case "40": Search_ProcQCInsp(); break;   //초중종검사
                case "50": Search_ProcQCInsp(); break;   //공정자주검사
                case "60": Search_ProcQCInsp(); break;   //공정자주검사
            }
        }

        private void Search_OQC_Out()
        {
            //출하검사:출하내역 근거 출하검사 조회
            Gc_BfProc.MainView = Gv_ProdOut; 

            string startDate = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue).ToString("yyyyMMdd");
            string endDate = Convert.ToDateTime(Dt_Date.smtEndDateTimeValue).ToString("yyyyMMdd");

            QM_ExamResult er = new QM_ExamResult();
            er.ExamDivCd = "20";
            er.ItemCode = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();

            Gc_BfProc.DataSource = er.Select_ExamList_OQCOut(startDate, endDate, null);

            (Gc_BfProc.MainView as GridView).BestFitColumns();
        }

        private void Search_ProcQCInsp()
		{
            switch (Exam_Div)
            {
                case "30": Gc_BfProc.MainView = Gv_PrQC; break;  //공정검사
                case "40": Gc_BfProc.MainView = Gv_Cmd_FML; break; //초중종검사
                case "50": Gc_BfProc.MainView = Gv_Cmd_DSC; break;
                case "60": Gc_BfProc.MainView = Gv_PCQ; break;
            }

			string startDate = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue).ToString("yyyyMMdd");
			string endDate = Convert.ToDateTime(Dt_Date.smtEndDateTimeValue).ToString("yyyyMMdd");

            QM_ExamResult er = new QM_ExamResult();
            er.ExamDivCd = Exam_Div;
            er.ItemCode = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();
			er.LineCode = LupLine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupLine.EditValue.ConvertString_Null_To_Empty();
			er.ProcessCode = LupProcess.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupProcess.EditValue.ConvertString_Null_To_Empty();
            er.MachineCode = LupMachine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupMachine.EditValue.ConvertString_Null_To_Empty();

            Gc_BfProc.DataSource = er.Select_ExamListView_PQC(startDate, endDate, null, "");

            (Gc_BfProc.MainView as GridView).BestFitColumns();
        }


        #endregion 데이터 조회 End

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
            //qer.RefSeq = drBf["RefSeq"].ConvertInt_Null_To_Zero();
            qer.ExamDivCd = Exam_Div;

            DataRow dataRow = qer.Select_ExamResult_Exsits_No();

            if (dataRow == null)
            {
                return;
            }

            string RefNo = drBf["RefNo"].ConvertString_Null_To_Empty();
            //int RefSeq = drBf["RefSeq"].ConvertInt_Null_To_Zero();

            Frm_MillSheet_pdf frm = new Frm_MillSheet_pdf(RefNo, Exam_Div, dataRow);
            frm.ShowDialog();

            //밀시트 저장 후 재조회 
            //1. 선택한 row 저장
            int SaveFocusedRowInx = (Gc_BfProc.MainView as GridView).FocusedRowHandle;

            //2재조회
            Search_BfProc();

            //3. 저장 후 선택한 row를 불러옴
            (Gc_BfProc.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        #endregion

        private void BtnMillSheet_ElementClick(object sender, EventArgs e)
        {
            Upload_MillSheet();
        }

		private void cmdExcel_Click(object sender, EventArgs e)
		{
            GridView gv = Gc_BfProc.MainView as GridView;

            clsFunction func = new clsFunction();
            func.ExportExcel(gv);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}