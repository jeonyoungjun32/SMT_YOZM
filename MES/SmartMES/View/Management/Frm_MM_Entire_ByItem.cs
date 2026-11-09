using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Model.Management;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.Management;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMES.Models.ST;

namespace SmartMES.View.Management
{
	public partial class Frm_MM_Entire_ByItem : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
		Database db = new Database();
        clsRepository rpo = new clsRepository();

        private string CustTop1 = "";
        private string CustTop2 = "";
        private string CustTop3 = "";
        private string TotalOrderQty = "";
        private string TotalResultQty = "";
        private string[] CustDiv = { "05", "20", "40", "50" }; //거래처구분 리스트(05 : 매입/매출, 10 : 매입, 20 : 매출, 30 : 외주, 40 : 납품)

        public Frm_MM_Entire_ByItem()
        {
            InitializeComponent();
        }

        private void Frm_MM_ProductAnalysis_Machine_View_load(object sender, EventArgs e)
        {
            //Set_SearchLup();
            //SetGridAmt(Gv_Purchase);
            //SetGridAmt(Gv_Sales);
            //SetStock(Gv_MTStock);
            //SetStock(Gv_BSStock);
            //Search();
        }

        private void Frm_MM_Entire_ByItem_Shown(object sender, EventArgs e)
        {
            try
            {
                Set_SearchLup();
                SetGridAmt(Gv_Purchase);
                SetGridAmt(Gv_Sales);
                SetStock(Gv_MTStock);
                SetStock(Gv_BSStock);

                Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"초기화 중 오류: {ex.Message}");
            }
        }

        #region SearchLup
        private void Set_SearchLup()
        {
            barDate.smtStartDateTimeValue = DateTime.Now;

            DataTable dtItem = new CM_Item().Select_Material_List(new string[] { "40", "50", "60" }, true);
            //"40" : 원자재 | "50" : 부자재 | "60" : 소모자재  --> 필요 시 반제품("30") 추가필요

            DataTable dtCust = new CM_Cust().Select_Cust_List(null, true);
            cls_Lookup clsLup = new cls_Lookup();
            clsLup.Create_RepositoryItemSearchLookUp(LupCustomer, dtCust, "코드", "명칭");
            LupCustomer.smtLookUpValue = "ALL";

            //Clear 안보이게
            //LupCustomer.Properties.ShowClearButton = false;
            //DtpDate.Properties.ShowClear = false;
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

            CM_Cust BsCust = new CM_Cust();
            RepositoryItemSearchLookUpEdit lup_CustCode = rpo.Create_RepositoryItemSearchLookUp(BsCust.Select_Cust_List(CustDiv), "코드", "명칭");

            grid.Create_GvColumn(gv, "지시일자", "ProcCmd_Date");
            grid.Create_GvColumn(gv, "실적일자", "Rst_Date");
            grid.Create_GvColumn(gv, "실적일자", "Order_Date");
            grid.Create_GvColumn(gv, "라인", "Line_Nm");
            grid.Create_GvColumn(gv, "공정", "Process_Nm");
            grid.Create_GvColumn(gv, "거래처", "CustCode", lup_CustCode);
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName", rpo.Create_rpoMemo());
            grid.Create_GvColumn(gv, "수주량", "ROrderQty");
            grid.Create_GvColumn(gv, "지시량", "ProcCmd_Qty");
            grid.Create_GvColumn(gv, "실적량", "WorkQty");
            grid.Create_GvColumn(gv, "진척율 (%)", "ProgressRate");
            grid.Create_GvColumn(gv, "지시대비 실적 달성률 (%)", "ProgressRatebar");

            //포맷
            grid.SetCol_Type(gv.Columns["ROrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ProcCmd_Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ProgressRate"], clsGrid.TypeList.TFloat);

            if(Database.DbName.Contains("IWAUTO"))
			{
                gv.Columns["ProgressRate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gv.Columns["ProgressRate"].DisplayFormat.FormatString = "#,##0.#";
            }

            //칼럼 색상
            //gv.Columns["ProgressRate"].AppearanceCell.ForeColor = Color.Red;

            //칼럼숨김
            gv.Columns["Order_Date"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;
            gv.Columns["Line_Nm"].Visible = false;
            gv.Columns["Process_Nm"].Visible = false;
            gv.Columns["ProcCmd_Date"].Visible = false;
            gv.Columns["ProgressRate"].Visible = false;

            //칼럼너비
            gv.Columns["ProgressRatebar"].MinWidth = 170;
            gv.Columns["Rst_Date"].MinWidth   = 100;
            gv.Columns["ItemName"].MinWidth   = 120;
            gv.Columns["CustCode"].MinWidth   = 120;
            gv.Columns["ROrderQty"].MaxWidth   = 70;
            gv.Columns["ROrderQty"].MinWidth   = 70;
            gv.Columns["ProcCmd_Qty"].MaxWidth = 70;
            gv.Columns["ProcCmd_Qty"].MinWidth = 70;
            gv.Columns["WorkQty"].MaxWidth  = 70;
            gv.Columns["WorkQty"].MinWidth = 70;

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            // ProgressBar를 RepositoryItemProgressBar 생성
            RepositoryItemProgressBar repoProgressBar = new RepositoryItemProgressBar();

            // 색상 설정
            repoProgressBar.LookAndFeel.UseDefaultLookAndFeel = false; //해줘야 적용됨
            repoProgressBar.ShowTitle = true;

            //repoProgressBar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            //repoProgressBar.Properties.StartColor = Color.LightSalmon;
            //repoProgressBar.Properties.EndColor = Color.RoyalBlue;

            // 칼럼에 할당
            gv.Columns["ProgressRatebar"].ColumnEdit = repoProgressBar;

            //폰트굵게
            Font font_Bold = new Font(gv.Columns["ProgressRate"].AppearanceCell.Font, FontStyle.Bold);
            gv.Columns["ProgressRate"].AppearanceCell.Font = font_Bold;

            //이벤트
            gv.CustomDrawCell += RowCellDisplayValue;
            gv.CustomDrawCell += DrawProgressBar; //진행 상황표기
        }

        private void SetGridAmt(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.OptionsView.ColumnAutoWidth = true;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ShowColumnHeaders = false;
            gv.OptionsView.ShowIndicator = false;
            gv.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gv.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;

            //초기 포커스 색상
            gv.Appearance.HideSelectionRow.BackColor = Color.LightCyan;
            gv.Appearance.HideSelectionRow.BackColor2 = Color.White;

            gv.Appearance.FocusedRow.BackColor = Color.LightCyan;
            gv.Appearance.FocusedRow.BackColor2 = Color.White;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;

            grid.Create_GvColumn(gv, "거래처명", "CustName");
            grid.Create_GvColumn(gv, "금액", "AMT");

            //칼럼명
            if (gv.Name == "Gv_Purchase") { gv.Columns["AMT"].Caption = "매입금액"; } else { gv.Columns["AMT"].Caption = "매출금액"; }

            //포맷
            grid.SetCol_Type(gv.Columns["AMT"], clsGrid.TypeList.TInt);

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }
        }

        private void SetStock(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.OptionsView.ColumnAutoWidth = true;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ShowColumnHeaders = true;
            gv.OptionsView.ShowIndicator = false;
            gv.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;

            //초기 포커스 색상
            gv.Appearance.HideSelectionRow.BackColor = Color.LightCyan;
            gv.Appearance.HideSelectionRow.BackColor2 = Color.White;

            gv.Appearance.FocusedRow.BackColor = Color.LightCyan;
            gv.Appearance.FocusedRow.BackColor2 = Color.White;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "수량", "StockQty");

            gv.Columns["ItemCode"].Visible = false;

            //포맷
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TInt);

            //칼럼너비
            gv.Columns["ItemCode"].MinWidth = 130;

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }
        }

        #endregion gridview

        #region 데이터조회 / label값 설정
        private void Search()
        {
            string CustCode = LupCustomer.smtLookUpValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : LupCustomer.smtLookUpValue.ConvertString_Null_To_Empty();
            string startDate = barDate.smtStartDateTimeValue.ToString("yyyy-MM-01");
            string endDate = Convert.ToDateTime(startDate).AddMonths(1).AddDays(-1).ToString("d");  //이번달 말일

            string aa = Convert.ToDateTime(startDate).AddMonths(-2).ToString("d"); //

            Label[,] LabelsCustItem = new Label[3, 3];
            Label[,] LabelsItemStockQty = new Label[3, 3];

            MM_ProductAnalysis bad_Chk = new MM_ProductAnalysis();
            MM_PurchaseAnalysis purAnalysis = new MM_PurchaseAnalysis();
            ST_StockM stock = new ST_StockM();
            bad_Chk.CustCode = CustCode;
            purAnalysis.CustCode = CustCode;

            ////1. 데이터 테이블 조회
            DataTable dtSlaes = purAnalysis.SalesView_AmtByCust(startDate, endDate);         //매출,매입금액
            DataTable dtPurchase = purAnalysis.PurchaseView_AmtByCust(startDate, endDate);   //매출,매입금액
            DataTable dtProdcut = bad_Chk.ProductEntireView_ByCmd(startDate, endDate);       //종합
            DataTable dtChart1 = bad_Chk.ProductEntireView_ItemWorkQty(startDate, endDate);  //품목별 생산수량
            DataTable dtChart3 = bad_Chk.ProductEntireView_CustOrderQty(startDate, endDate); //거래처별 수주수량
            DataTable dtChart4 = bad_Chk.ProductEntireView_Total(aa, endDate);
            //DataTable dtChart2 = bad_Chk.ProductEntireView_Month(date);      //월별 품목별 생산량

            Gc_BSStock.DataSource = stock.Select_StockList("제품");  //제품현재고현황
            Gc_MatStock.DataSource = stock.Select_StockList("자재");  //자재현재고현황

            // 거래처별 품목 TOP3
            LabelsCustItem = new Label[,] {
                { Lbl_Cust1_ItemTop1, Lbl_Cust1_ItemTop2, Lbl_Cust1_ItemTop3 },
                { Lbl_Cust2_ItemTop1, Lbl_Cust2_ItemTop2, Lbl_Cust2_ItemTop3 },
                { Lbl_Cust3_ItemTop1, Lbl_Cust3_ItemTop2, Lbl_Cust3_ItemTop3 }
            };

            if (dtChart3.Rows.Count > 0)
            {
                int maxCount = Math.Min(3, dtChart3.Rows.Count); // 최대 반복 횟수 제한
                for (int i = 0; i < maxCount; i++)
                {
                    string CustTopi = dtChart3.Rows[i]["CustCode"].ConvertString_Null_To_Empty();
                    string CustTopi_Nm = dtChart3.Rows[i]["CustName"].ConvertString_Null_To_Empty();

                    DataTable dtTopi = bad_Chk.ProductEntireView_ItemTOP3_ByCust(startDate, endDate, CustTopi);

                    // 거래처명 설정
                    Label[] LabelsCust = { Lbl_CustTop1, Lbl_CustTop2, Lbl_CustTop3 };
                    LabelsCust[i].Text = CustTopi_Nm;

                    // 수주량, 생산량 설정
                    Label[] LabelsCustOrderQty = { Lbl_Cust1_OrderQty, Lbl_Cust2_OrderQty, Lbl_Cust3_OrderQty };
                    Label[] LabelsCustResultQty = { Lbl_Cust1_ResultQty, Lbl_Cust2_ResultQty, Lbl_Cust3_ResultQty };

                    LabelsCustOrderQty[i].Text = Convert.ToInt32(dtChart3.Rows[i]["ROrderQty"]).ToString("#,#");
                    LabelsCustResultQty[i].Text = Convert.ToInt32(dtChart3.Rows[i]["WorkQty"]).ToString("#,#");

                    // 품목 TOP3 설정
                    int maxItemCount = Math.Min(3, dtTopi.Rows.Count); // 품목 개수 제한
                    for (int j = 0; j < maxItemCount; j++)
                    {
                        LabelsCustItem[i, j].Text = dtTopi.Rows[j]["ItemName"].ConvertString_Null_To_Empty();
                        //LabelsItemStockQty[i, j].Text = Convert.ToInt32(dtTopi.Rows[j]["StockQty"]).ToString("#,#").ConvertString_Null_To_Zero();
                    }
                }
            }


            ////2.데이터소스 바인딩
            Gc_Sales.DataSource = dtSlaes;          //매출,매입금액
            Gc_Purchase.DataSource = dtPurchase;    //매출,매입금액
            Gc_Product.DataSource = dtProdcut;      //종합
            SetChart1(barChart1, dtChart1);         //품목별 생산수량
            SetChart3(barChart3, dtChart3);         // 거래처별 수주수량
            SetChart4(barChart4, dtChart4);         //전체 수주량, 실적량
            //SetChart2(barChart2, dtChart2);       //월별 품목별 생산량

            ////3. Label값 바인딩
            //생산진행현황
            TotalOrderQty = SummarySum(Gv_Product, "OrderQty");              //당월 총 수주 수량
            TotalResultQty = SummarySum(Gv_Product, "GoodQty");            //당월 총 실적 수량

            lbl_TotalOrder.Text = dtProdcut.Rows.Count.ConvertString_Null_To_Zero();   //당월 총 수주 건수
            lbl_TotalOrderQty.Text = TotalOrderQty;                                    //당월 총 수주 수량
            lbl_TotalResultQty.Text = TotalResultQty;                                  //당월 총 실적 수량
            lbl_Total.Text = SummarySum(Gv_Product, "NgQty");                        //당월 총 불량 수량
            label10.Text = SummaryAvg(Gv_Product, "OrderQty");                        //건당 평균 수주 수량
            label12.Text = SummaryAvg(Gv_Product, "GoodQty");                       //건당 평균 실적 수량
            //string rate = ((lbl_TotalResultQty.Text.ConvertFloat_Null_To_Zero() / lbl_TotalOrderQty.Text.ConvertFloat_Null_To_Zero() * 100)).ToString("F2"); //당월 평균 진척률
            //lbl_Total.Text = rate == "NaN" ? "0" : rate;                               //당월 평균 진척률

            //디자인조정
            Gv_Product.BestFitColumns();
            Gv_Product.Columns["ProgressRatebar"].Width = 250; //칼럼너비
        }
        #endregion 데이터조회

        #region event
        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            switch (btn.Name)
            {
                case "BtnSearch":
                    try
                    {
                        Search();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"조회 중 오류: {ex.Message}");
                    }
                    break;

                case "BtnClose":
                    this.Close();
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


        void DrawProgressBar(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (!fieldName.Contains("ProgressRatebar")) return;

            int percent = Convert.ToInt32(e.CellValue);
            int progressWidth = percent * e.Bounds.Width / 100;

            // 막대 높이 조정 (여백 적용)
            int padding = 4; // 위아래 여백 설정
            int barHeight = e.Bounds.Height - (padding * 2); // 막대 높이 조정
            int barY = e.Bounds.Y + padding; // 막대 시작 위치 조정

            // 색상 설정
            Brush progressBrush = Brushes.Green;
            Color textColor = Color.Black;

            if (percent < 25)
            {
                progressBrush = Brushes.LightSalmon;
                textColor = Color.Black;
            }
            else if (percent < 50)
            {
                progressBrush = Brushes.Orange;
                textColor = Color.Black;
            }
            else if (percent > 75)
            {
                progressBrush = Brushes.MidnightBlue;
                textColor = Color.White;
            }

            // 진행 막대 그리기 (위아래 여백 적용)
            e.Graphics.FillRectangle(progressBrush, new Rectangle(e.Bounds.X, barY, progressWidth, barHeight));

            // 텍스트 출력 (색상 변경 적용)
            string progressText = $"{percent} %";
            using (Brush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(progressText, e.Appearance.Font, textBrush, e.Bounds, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
            }

            e.Handled = true; // 기본 렌더링 방지
        }
        #endregion event

        #region ChartControl
        private void SetChart1(ChartControl chart, DataTable dt)
        {
            //품명 길어서 안 보임..
            try
            {
                if (dt == null) { chart.Series.Clear(); return; }
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                // 각 불량 유형별로 시리즈 생성
                Series series1 = new Series("생산량", ViewType.Bar);

                // 해당 시리즈에 데이터 포인트 추가
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i]["ItemName"].ToString() != "")
                    {
                        //설비명이 비어있으면 안나오도록
                        string strAxis = dt.Rows[i]["ItemName"].ToString() == "" ? "" : dt.Rows[i]["ItemName"].ToString();
                        float value1 = dt.Rows[i]["WorkQty"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["WorkQty"].ToString());
                        series1.Points.Add(new SeriesPoint(strAxis, value1));

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series1);
                        //series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                    }
                }

                // 포인트 라벨 설정
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series1.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정

                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가

                chart.Series.Add(series1);
                // X축 및 Y축 설정
                XYDiagram diagram = chart.Diagram as XYDiagram;
                //diagram.AxisX.Label.TextPattern = "{A:dd}";
                //diagram.AxisX.WholeRange.MinValue = 1;
                //diagram.AxisX.VisualRange.MinValue = 15;
                //diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;

                //시리즈 색깔
                chart.Series["생산량"].View.Color = Color.FromArgb(13,37,107);

                // 제목 설정
                //chart.Titles.Add(new ChartTitle { Text = "생산현황", Font = new Font(Font.FontFamily.Name, 11, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        private void SetChart3(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt == null) { chart.Series.Clear(); return; }
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                // 각 불량 유형별로 시리즈 생성
                Series series1 = new Series("수주량", ViewType.Bar);

                // 해당 시리즈에 데이터 포인트 추가
                for (int i = dt.Rows.Count-1; i >= 0; i--)
                {
                    if (dt.Rows[i]["CustName"].ToString() != "")
                    {
                        //설비명이 비어있으면 안나오도록
                        string strAxis = dt.Rows[i]["CustName"].ToString() == "" ? "" : dt.Rows[i]["CustName"].ToString();
                        float value1 = dt.Rows[i]["ROrderQty"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["ROrderQty"].ToString());
                        series1.Points.Add(new SeriesPoint(strAxis, value1));

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series1);
                        //series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                    }
                }

                // 포인트 라벨 설정
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series1.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정

                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가

                chart.Series.Add(series1);
                // X축 및 Y축 설정
                XYDiagram diagram = chart.Diagram as XYDiagram;
                //diagram.AxisX.Label.TextPattern = "{A:dd}";
                //diagram.AxisX.WholeRange.MinValue = 1;
                //diagram.AxisX.VisualRange.MinValue = 15;
                //diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;

                //시리즈 색깔
                chart.Series["수주량"].View.Color = Color.FromArgb(109,190,183);

                // 제목 설정
                //chart.Titles.Add(new ChartTitle { Text = "수주현황", Font = new Font(Font.FontFamily.Name, 11, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        private void SetChart4(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt == null) { chart.Series.Clear(); return; }
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                // 각 불량 유형별로 시리즈 생성
                Series series1 = new Series("수주량", ViewType.Area);
                Series series2 = new Series("생산량", ViewType.Area);
                Series series3 = new Series("불량량", ViewType.Area);

                // 해당 시리즈에 데이터 포인트 추가
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MONTH"].ToString() != "")
                    {
                        //설비명이 비어있으면 안나오도록
                        string strAxis = dt.Rows[i]["MONTH"].ToString() == "" ? "" : dt.Rows[i]["MONTH"].ToString();
                        float value1 = dt.Rows[i]["ROrderQty"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["ROrderQty"].ToString());
                        float value2 = dt.Rows[i]["WorkQty"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["WorkQty"].ToString());
                        float value3 = dt.Rows[i]["NgQty"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["NgQty"].ToString());
                        series1.Points.Add(new SeriesPoint(strAxis+"월", value1));
                        series2.Points.Add(new SeriesPoint(strAxis + "월", value2));
                        series3.Points.Add(new SeriesPoint(strAxis + "월", value3));

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series1);
                        seriesList.Add(series2);
                        seriesList.Add(series3);
                        series1.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                        series2.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                        series3.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                    }
                }


                // 포인트 라벨 설정
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series1.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정
                series2.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정
                series3.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정

                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가

                chart.Series.Add(series1);
                chart.Series.Add(series2);
                chart.Series.Add(series3);

                //시리즈 색깔
                chart.Series["생산량"].View.Color = Color.FromArgb(148,222,222);
                chart.Series["수주량"].View.Color = Color.FromArgb(213,201,235);
                chart.Series["불량량"].View.Color = Color.FromArgb(168,208,245);

                // 제목 설정
                //chart.Titles.Add(new ChartTitle { Text = "생산현황", Font = new Font(Font.FontFamily.Name, 11, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        #endregion ChartControl END

        /*
        private void SetChart2(ChartControl chart, DataTable dt)
        {
        try
        {
        if (dt == null) { chart.Series.Clear(); return; }
        if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

        // 시리즈들을 담을 리스트 생성
        List<Series> seriesList = new List<Series>();

        for (int idx = 0; idx < dt.Rows.Count; idx++)
        {
          string itemNm = dt.Rows[idx]["ItemName"].ToString();
          //해당 칼럼은 그래프에 표기X

          // 각 불량 유형별로 시리즈 생성
          Series series = new Series(itemNm, ViewType.Bar);

          // 해당 시리즈에 데이터 포인트 추가
          for (int j = 1; j < 12; j++)
          {
              string strJ = j.ToString("D2");
              string strAxis = strJ + "월";
              int value = dt.Rows[idx][strAxis].ToString() == "" ? 0 : dt.Rows[idx][strAxis].ConvertInt_Null_To_Zero();
              series.Points.Add(new SeriesPoint(strAxis, value));
          }

          // 생성된 시리즈를 리스트에 추가
          seriesList.Add(series);
          series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
          // 포인트 라벨 설정
          series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
          series.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정
        }
        // ChartControl 초기화
        chart.Series.Clear();
        chart.Titles.Clear();

        // 리스트에 있는 모든 시리즈를 ChartControl에 추가
        foreach (var series in seriesList)
        {
          chart.Series.Add(series);
        }

        // X축 및 Y축 설정cuu
        XYDiagram diagram = chart.Diagram as XYDiagram;
        if (diagram == null) return; //총생산,양품수량은 있는데 불량이 하나도 없을 경우 차트를 만들지 않는다.

        diagram.AxisX.VisualRange.SetMinMaxValues("01월", "03월"); // 스크롤 범위
        diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;


        // 제목 설정
        chart.Titles.Add(new ChartTitle { Text = "일별 불량유형 합계", Font = new Font(Font.FontFamily.Name, 12, FontStyle.Bold) });
        }
        catch (Exception ex)
        {
        if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
        {
          MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
        }
        }
        }
        */
    }
}