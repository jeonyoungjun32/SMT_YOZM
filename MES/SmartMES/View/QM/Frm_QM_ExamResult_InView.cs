using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models.CM;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace SmartMES.View.QM
{
	public partial class Frm_QM_ExamResult_InView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query;

        clsCode code = new clsCode();
        CM_CateItem cm = new CM_CateItem();

        private string[] CustDiv = { "05", "10", "20", "30", "40", "50" }; //거래처구분 리스트(05 : 매입/매출, 10 : 매입, 20 : 매출, 30 : 외주, 40 : 납품)
        private string[] ItemDiv = { "40", "50" }; //품목구분 리스트(10 : 상품, 20 : 제품, 30 : 반제품, 40 : 원자재, 50 : 부자재, 60 : 소모자재)
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


        public Frm_QM_ExamResult_InView()
        {
            InitializeComponent();

            Exam_Div = "10";


            if (Database.DbName == "KNP" || Database.DbName.ToUpper().Contains("UAGHITEC"))
            {
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            }

            SetDesign();
        }

        //public Frm_QM_ExamResult_InView(string exam_Div)
        //{
        //    InitializeComponent();
        //    switch (exam_Div)
        //    {
        //        case "InQC":      Exam_Div = "10"; break;  //수입검사
        //        case "OutQC":     Exam_Div = "20"; break;  //출하검사 : 생산실적기반
        //        case "OutQC_Out": Exam_Div = "25"; break;  //출하검사 : 출하내역기반
        //        case "PrQC":      Exam_Div = "30"; break;  //공정검사
        //        case "FML":       Exam_Div = "40"; break;  //초중종검사
        //        case "DSC":       Exam_Div = "50"; break;  //일상자주검사
        //        case "PCQ":       Exam_Div = "60"; break;  //공정조건검사
        //        case "Circuit":   Exam_Div = "40"; break;  //순회검사, 초중종과 같음

        //    }

        //    SetDesign();
        //}

        ////감리용 현황화면용
        //public Frm_QM_ExamResult_InView(string exam_Div, string type)
        //{
        //    InitializeComponent();
        //    switch (exam_Div)
        //    {
        //        case "InQC": Exam_Div = "10"; break;   //수입검사
        //        case "OutQC": Exam_Div = "20"; break;  //출하검사
        //        case "PrQC": Exam_Div = "30"; break;  //공정검사
        //    }

        //    SetDesign();

        //    BtnSave.Enabled = false;

        //    foreach (DevExpress.XtraGrid.Columns.GridColumn col in Gv_ExRst_Bool.Columns)
        //    {
        //        grid.Column_AllowEdit(col, false);
        //    }
        //    foreach (DevExpress.XtraGrid.Columns.GridColumn col in Gv_ExRst_Num.Columns)
        //    {
        //        grid.Column_AllowEdit(col, false);
        //    }
        //}

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            if (Database.DbName.ToUpper().Contains("DOOIL"))
            {
                Dt_Date.smtStartDateTimeValue = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            }

            //검사구분에따른 설정
            Set_TypeOption();

            //GridView 설정
            Set_GridView();

            Set_RadioGroup();

            Set_Lup();

            //기타 표기 명 수정
            switch (Exam_Div)
            {
                case "10": Pnl_Proc.Text = "입고내역"; Lbl_ProcDate.Text = "입고일자"; break;  //수입검사
                case "20": Pnl_Proc.Text = "생산내역"; Lbl_ProcDate.Text = "생산일자"; break;  //출하검사
                case "25": Pnl_Proc.Text = "출하내역"; Lbl_ProcDate.Text = "출하일자"; break;  //출하검사
                case "30": Pnl_Proc.Text = "지시내역"; Lbl_ProcDate.Text = "지시일자"; break;  //공정검사
                case "40": Pnl_Proc.Text = "생산내역"; Lbl_ProcDate.Text = "생산일자"; break;  //초중종검사
                case "50": Pnl_Proc.Text = "생산내역"; Lbl_ProcDate.Text = "생산일자"; break;  //일상자주검사
                case "60": Pnl_Proc.Text = "생산내역"; Lbl_ProcDate.Text = "생산일자"; break;  //공정조건검사
            }
        }

        private void Set_TypeOption()
        {
            //수입검사
            if(Exam_Div == "10")
            {
                Pnl_Proc.Text = "입고내역";
                Lbl_ProcDate.Text = "입고일자";

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

                Lup_Item.Visible = true;
                Lup_Item.Visible = true;
            }
        }

        #region GridView 셋팅

        private void Set_GridView()
        {
            switch (Exam_Div)
            {
				case "10": SetGrid_In(Gv_MatIn); break;   //수입검사
															 //case "20": SetGrid_ProdRst(Gv_ProdRst); break;  //출하검사:생산실적기반
															 //case "25": SetGrid_ProdOut(Gv_ProdOut); break;  //출하검사:출하내역기반
				//case "30": SetGrid_PrQC(Gv_PrQC); break;  //공정검사
                //case "40": SetGrid_ProcCmd_FML(Gv_Cmd_FML); break;  //초중종검사
                //case "50": SetGrid_ProcCmd_DSC(Gv_Cmd_DSC); break;  //일상자주검사
                //case "60": SetGrid_PCQ(Gv_PCQ); break;  //공정조건검사
            }
        }

		private void SetGrid_In(GridView gv)
		{
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            grid.Create_GvColumn(gv, "검사일자", "ExamDate");
            grid.Create_GvColumn(gv, "검사번호", "ExamNo");
            grid.Create_GvColumn(gv, "검사순번", "ExamSeq");

            grid.Create_GvColumn(gv, "입고번호", "StoNo");
            grid.Create_GvColumn(gv, "입고순번", "StoSeq");

            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn(gv, "품목구분", "ItemTypeName");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "검사수량", "ExamQty");

            grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
            grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
            grid.Create_GvColumn(gv, "검사명", "Title");
            grid.Create_GvColumn(gv, "검사단위", "ExamUnit");

            for (int i = 1; i < ExamCnt + 1; i++)
            {
                grid.Create_GvColumn(gv, i + "차", "X" + i);
            }

            grid.Create_GvColumn(gv, "조치내역", "ActionMemo");
            //grid.Create_GvColumn(gv, "검사수량", "ExamQty");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["ExamQty"], clsGrid.TypeList.TFloat);

            //정렬
            //gv.Columns["ExamYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Ce

            gv.FocusedRowObjectChanged += GvBfProc_FocusedRowObjectChanged;
        }

        private void GvBfProc_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            //성적서 조회
            GridView gv = sender as GridView;

            DataRow dr = gv.GetDataRow(e.RowHandle);

            if(dr == null)
			{
                PicImage.EditValue = null;
                pdfViewer1.CloseDocument();
                return;
			}

            string InFilePath = dr["InFilePath"].ConvertString_Null_To_Empty();
            string ext = dr["FileExt"].ConvertString_Null_To_Empty();

            if (InFilePath == "") return;

            clsFtp clsFtp = new clsFtp();
            Stream stream = clsFtp.DownloadFile(InFilePath);

            if (ext.ToUpper() == ".PDF")
            {
                PicImage.Visible = false;
                pdfViewer1.Visible = true;

                if (stream != null)
                {
                    pdfViewer1.LoadDocument(stream);
                }

                pdfViewer1.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.PageLevel;
            }
            else
            {
                PicImage.Visible = true;
                pdfViewer1.Visible = false;

                if(stream != null)
				{
                    PicImage.Image = Image.FromStream(stream);
				}
            }

        }

        #endregion GridView 셋팅 End

        private void Set_RadioGroup()
        {
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
            //품목
            DataTable dt_item = code.GetMaterialList(ItemDiv, true).Table;
			clsLup.Create_RepositoryItemSearchLookUp(Lup_Item, dt_item, "품목코드", "품명");
            //grid.SetLupView_Item(LupItem, "제품코드 초기화");
            Lup_Item.smtLookUpValue = "ALL";

            //거래처
            DataTable dt_Cust = code.GetCustomerList(CustDiv, true);
		    clsLup.Create_RepositoryItemSearchLookUp(Lup_Cust, dt_Cust, "거래처코드", "거래처명");
            Lup_Cust.smtLookUpValue = "ALL";
		}

        #endregion

        private void Frm_BN_Order_Load(object sender, EventArgs e)
        {
            Search_BfProc();
        }

        #region 데이터 조회

        public void Search_BfProc()
        {
            //이전 프로세스 작업내역 조회
            switch (Exam_Div)
            {
				case "10": Search_Purchase(); break;   //수입검사:입고내역 조회
												  //case "20": Search_OQC(); break;   //출하검사:생산실적 조회
												  //case "25": Search_OQC_Out(); break;   //출하검사:출하내역 조회
				//case "30": Search_ProcQCInsp(); break;   //공정검사
                //case "40": Search_ProcessResult(); break;   //초중종검사
                //case "50": Search_ProcessResult(); break;   //공정자주검사
                //case "60": Search_ProcessResult(); break;   //공정자주검사
            }
            DataTable dt = Gc_BfProc.DataSource as DataTable;
            if(dt == null)
			{
                PicImage.EditValue = null;
                pdfViewer1.CloseDocument();
			}
			else
			{
                if(dt.Rows.Count < 1)
				{
                    PicImage.EditValue = null;
                    pdfViewer1.CloseDocument();
                }
            }
        }

        private void Search_Purchase()
        {
            Gc_BfProc.MainView = Gv_MatIn;
            GridView gv = Gc_BfProc.MainView as GridView;

            string startDate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-dd");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("yyyy-MM-dd 23:59");
            string item_Div = Rdo_Div.EditValue.ConvertString_Null_To_Empty();
            string lupItem = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();

            string[] ItemDiv = null;

            if(item_Div.ToUpper() != "ALL" && item_Div.ToUpper() != "")
			{
                ItemDiv = new string[] { item_Div };
			}

            QM_ExamResult qt = new QM_ExamResult();
            Gc_BfProc.DataSource = qt.Select_ExamListView_IQC(startDate, endDate, lupItem, ItemDiv);

            gv.BestFitColumns();
        }

        #endregion 데이터 조회 End

        private void btnSearch_Click(object sender, EventArgs e)
        {
            g_Rst_No = "";
            G_Rst_Row_Seq = 0;
            Search_BfProc();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            GridView gv = Gc_BfProc.MainView as GridView;
            if (gv == null) return;

            clsFunction func = new clsFunction();
            func.ExportExcel(gv);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}