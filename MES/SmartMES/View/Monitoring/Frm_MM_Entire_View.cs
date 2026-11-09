using DevExpress.Xpf.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Model.Management;
using SmartMES.Models;
using SmartMES.Models.Management;
using System;
using System.Data;
using System.Drawing;

namespace SmartMES.View.Monitoring
{
    public partial class Frm_MM_Entire_View : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();
        clsRepository rpo = new clsRepository();
        clsGrid grid = new clsGrid();
        string formType = "";  //지시기반 Cmd ,  수주기반 Order
        string[] ItemType = WrGlobal.Business_ItemType;
        private string[] CustDiv = { "20", "40" }; //거래처구분 리스트(10 : 매입, 20 : 매출, 40 :매입/매출)

        public Frm_MM_Entire_View(string Type)
        {
            InitializeComponent();
            formType = Type;
        }

        private void Frm_MM_ProductAnalysis_Machine_View_load(object sender, EventArgs e)
        {
            Set_SearchLup();

            SetGridProduct(Gv_Product);
            SetGridAmt(Gv_Purchase);
            SetGridAmt(Gv_Sales);

            Search();
        }

        #region SearchLup
        private void Set_SearchLup()
        {
            DtpDate.EditValue = DateTime.Now;
            DtpDate.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;

            //품목
            LupItem.Properties.DataSource = code.GetMaterialList(ItemType, false).ToTable();
            LupItem.Properties.DisplayMember = "품명";
            LupItem.Properties.ValueMember = "품목코드";
            LupItem.EditValue = "ALL";

            //거래처
            code.LookupCustomer(WrGlobal.CorpID, "매출", true, "", ref LupCustomer);
            LupCustomer.EditValue = "ALL";

            //그리드 사이즈 조절
            LupItem.Properties.BestFitMode = BestFitMode.BestFitResizePopup;

            //Clear 안보이게
            LupCustomer.Properties.ShowClearButton = false;
            LupItem.Properties.ShowClearButton = false;
            DtpDate.Properties.ShowClear = false;
        }
        #endregion SearchLup

        #region gridview
        private void SetGridProduct(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.OptionsView.ColumnAutoWidth = false;
            //Row MultiLine
            gv.OptionsView.RowAutoHeight = true;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬

            grid.Create_GvColumn(gv, "수주일자", "Order_Date");
            grid.Create_GvColumn(gv, "실적일자", "WorkDate");
            grid.Create_GvColumn(gv, "실적라인", "LineName");
            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName", rpo.Create_rpoMemo());
            grid.Create_GvColumn(gv, "규격", "Spec");
            grid.Create_GvColumn(gv, "수주량", "Order_Qty");
            grid.Create_GvColumn(gv, "실적수량", "Result_Qty"); //입고량
            grid.Create_GvColumn(gv, "진척율 (%)", "ProgressRate");
            grid.Create_GvColumn(gv, "진척율 그래프", "ProgressRateBar");

            //포맷
            grid.SetCol_Type(gv.Columns["Order_Qty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Result_Qty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["ProgressRate"], clsGrid.TypeList.TFloat);

            //칼럼 숨김
            gv.Columns["CustCode"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;
            gv.Columns["LineName"].Visible = false;
            gv.Columns["ProcessName"].Visible = false;

            if (Database.DbName.Contains("IWAUTO"))
			{
                gv.Columns["ProgressRate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gv.Columns["ProgressRate"].DisplayFormat.FormatString = "#,##0.#";
            }

            if (Database.DbName.ToUpper().Contains("HANSUNG"))
            {
                gv.Columns["Line_Nm"].Visible = false;
            }
            else
            {
                gv.Columns["Spec"].Visible = false;      
            }

            //칼럼 색상   
            //gv.Columns["ProgressRate"].AppearanceCell.ForeColor = Color.Red;

            //칼럼너비
            gv.Columns["ProgressRateBar"].MinWidth = 250;

            //셀값 변경 허용
            //foreach (GridColumn col in gv.Columns)
            //{
            //    grid.Column_AllowEdit(col, false);
            //}

            // ProgressBar를 RepositoryItemProgressBar 생성
            RepositoryItemProgressBar repoProgressBar = new RepositoryItemProgressBar();

            // 색상 설정
            repoProgressBar.LookAndFeel.UseDefaultLookAndFeel = false; //해줘야 적용됨
            repoProgressBar.ShowTitle = false;
            repoProgressBar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            repoProgressBar.Properties.StartColor = Color.LightSalmon;
            repoProgressBar.Properties.EndColor = Color.RoyalBlue;

            // 칼럼에 할당
            gv.Columns["ProgressRateBar"].ColumnEdit = repoProgressBar;

            //폰트굵게
            Font font_Bold = new Font(gv.Columns["ProgressRate"].AppearanceCell.Font, FontStyle.Bold);
            gv.Columns["ProgressRate"].AppearanceCell.Font = font_Bold;

            //타입별 그리드 설정
            if (formType == "Cmd")
            {
                gv.Columns["Order_Date"].Caption = "지시일자";
                gv.Columns["Order_Qty"].Caption = "지시량";
                gv.Columns["Cust_Nm"].Visible = false;
                lblType.Text = "당월 총 지시 건수";
                lblOredrQty.Text = "당월 총 지시 수량";
            }
            else if ((formType == "Order"))
            {
                gv.Columns["Order_Date"].Caption = "수주일자";
                gv.Columns["Order_Qty"].Caption = "수주량";
                lblType.Text = "당월 총 수주 건수";
                lblOredrQty.Text = "당월 총 수주 수량";
            }

            //이벤트
            gv.CustomDrawCell += RowCellDisplayValue;
        }

        private void SetGridAmt(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.OptionsView.ColumnAutoWidth = true;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬

            grid.Create_GvColumn(gv, "거래처명", "Cust_Nm");
            grid.Create_GvColumn(gv, "금액", "AMT");

            //칼럼명
            if (gv.Name == "Gv_Purchase") { gv.Columns["AMT"].Caption = "매입금액"; } else { gv.Columns["AMT"].Caption = "매출금액"; }

            //포맷
            grid.SetCol_Type(gv.Columns["AMT"], clsGrid.TypeList.TInt);

            //셀값 변경 허용
            //foreach (GridColumn col in gv.Columns)
            //{
            //    grid.Column_AllowEdit(col, false);
            //}
        }
        #endregion gridview

        #region 데이터조회 / label값 설정
        private void Search()
        {
            string Cust_Cd = LupCustomer.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupCustomer.EditValue.ConvertString_Null_To_Empty();
            string Item_Cd = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();
            string date = DtpDate.DateTime.ToString("yyyyMM");
            string datePre = DtpDate.DateTime.AddMonths(-1).ToString("yyyyMM");

            MM_ProductAnalysis bad_Chk = new MM_ProductAnalysis();
            MM_PurchaseAnalysis purAnalysis = new MM_PurchaseAnalysis();
            bad_Chk.CustCode = Cust_Cd;
            bad_Chk.ItemCode = Item_Cd;
            purAnalysis.CustCode = Cust_Cd;
            purAnalysis.ItemCode = Item_Cd;

            ////1. 데이터 테이블 조회
            //생산진행현황
            DataTable dtProdcut = null;
            if (formType == "Order")
            {
                //수주기반 생산진행현황  조회
                dtProdcut = bad_Chk.ProductEntireView_ByROrder(date);
            }
            //else if (formType == "Cmd")
            //{
            //    //지시기반 생산진행현황  조회
            //    dtProdcut = bad_Chk.ProductEntireView_ByCmd(date);
            //}
            Gc_Product.DataSource = dtProdcut;

            //매출,매입금액
            DataTable dtSlaes = purAnalysis.SalesView_AmtByCust(date);
            DataTable dtPurchase = purAnalysis.PurchaseView_AmtByCust(date);
            Gc_Sales.DataSource = dtSlaes;
            Gc_Purchase.DataSource = dtPurchase;

            ////2. Label값 바인딩
            //생산진행현황
            if (dtProdcut != null)
            {
                lbl_TotalOrder.Text = dtProdcut.Rows.Count.ConvertString_Null_To_Zero();   //당월 총 수주 건수
            }

            lbl_TotalOrderQty.Text = SummarySum(Gv_Product, "Order_Qty");              //당월 총 수주 수량
            lbl_TotalResultQty.Text = SummarySum(Gv_Product, "Result_Qty");            //당월 총 실적 수량
            string rate = ((lbl_TotalResultQty.Text.ConvertFloat_Null_To_Zero() / lbl_TotalOrderQty.Text.ConvertFloat_Null_To_Zero() * 100)).ToString("F2"); //당월 평균 진척률
            lbl_Total.Text = rate == "NaN" ? "0" : rate;                               //당월 평균 진척률
            label10.Text = SummaryAvg(Gv_Product, "Order_Qty");                        //건당 평균 수주 수량
            label12.Text = SummaryAvg(Gv_Product, "Result_Qty");                       //건당 평균 실적 수량

            //매출,매입금액
            DataTable dtLblText = purAnalysis.Entire_AMTLabel(date, datePre);
            lbl_Gv_Sales_Sum.Text = SummarySum(Gv_Sales, "AMT");     //매출합계
            lbl_Gv_Purc_Sum.Text = SummarySum(Gv_Purchase, "AMT");   //매입합계
            //전월대비합계
            string lblText_Sales = dtLblText.Rows[0]["전월대비매출상승률"].ConvertString_Null_To_Zero();  //매출
            string lblText_Purc = dtLblText.Rows[0]["전월대비매입상승률"].ConvertString_Null_To_Zero();    //매입
            if (lblText_Sales.ConvertDouble_Null_To_Zero() >= 0)
            {
                //전월대비 매출 금액 상승 시
                lbl_Gv_Sales_Sum_Pre.Text = "▲" + lblText_Sales;
                lbl_Gv_Sales_Sum_Pre.ForeColor = Color.Blue;
            }
            else
            {
                //전월대비 매출 금액 하락 시
                lbl_Gv_Sales_Sum_Pre.Text = "▼" + lblText_Sales;
                lbl_Gv_Sales_Sum_Pre.ForeColor = Color.Red;
            }
            if (lblText_Purc.ConvertDouble_Null_To_Zero() >= 0)
            {
                //전월대비 매입 금액 상승 시
                lbl_Gv_Purc_Sum_Pre.Text = "▲" + lblText_Purc;
                lbl_Gv_Purc_Sum_Pre.ForeColor = Color.Blue;
            }
            else
            {
                //전월대비 매입 금액 하락 시
                lbl_Gv_Purc_Sum_Pre.Text = "▼" + lblText_Purc;
                lbl_Gv_Purc_Sum_Pre.ForeColor = Color.Red;
            }

            //디자인조정
            Gv_Product.BestFitColumns();
            Gv_Product.Columns["ProgressRateBar"].Width = 250; //칼럼너비
        }
        #endregion 데이터조회

        #region event
        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            switch (btn.Name)
            {
                case "Btn_Search":
                    Search();
                    break;
            }
        }

        private void RowCellDisplayValue(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (!fieldName.Contains("ProgressRate")) return;

            float cellValue = gv.GetRowCellValue(e.RowHandle, e.Column).ConvertFloat_Null_To_Zero();

            if (cellValue >= 50)
            {
                e.Appearance.ForeColor = Color.Blue;
            }
            else if (cellValue == 0)
            {
                e.Appearance.ForeColor = Color.Black;
            }
            else
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private string SummarySum(GridView gv, string colName)
        {
            //총 매입매출 구하기 (하단 summary 합계)
            decimal Sum = 0;
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                Sum += gv.GetRowCellValue(i, colName).ConvertDecimal_Null_To_Zero();
            }
            return Sum.ToString("#,##0");
        }

        private string SummaryAvg(GridView gv, string colName)
        {
            //총 매입매출 구하기 (하단 summary 합계)
            decimal Sum = 0;
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                Sum += gv.GetRowCellValue(i, colName).ConvertDecimal_Null_To_Zero();
            }
            if (gv.DataRowCount == 0) return "0";
            else { Sum = Sum / gv.DataRowCount; }
            return Sum.ToString("#,##0");
        }
        #endregion event
    }
}