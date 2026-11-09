using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Model.Management;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Data;
using System.Drawing;
using System.Linq;

namespace SmartMES.View.Management
{
	public partial class Frm_MM_Entire_View : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        Database db = new Database();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        string formType = "";  //지시기반 Cmd ,  수주기반 Order
        string[] ItemDiv = new string[] { "40", "50", "60" };
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

            DataView dvItem = code.GetMaterialList(ItemDiv, true);
            //"40" : 원자재 | "50" : 부자재 | "60" : 소모자재  --> 필요 시 반제품("30") 추가필요

            DataTable dtCust = new CM_Cust().Select_LupCust(true, null);
            cls_Lookup clsLup = new cls_Lookup();
            clsLup.Create_RepositoryItemSearchLookUp(LupItem, dvItem.Table, "품목코드", "품명");

            clsLup.Create_RepositoryItemSearchLookUp(LupCustomer, dtCust, "거래처코드", "거래처명");
            LupCustomer.EditValue = "ALL";
            LupItem.EditValue = "ALL";
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

            grid.Create_GvColumn(gv, "수주일자", "ObtainDate");
            grid.Create_GvColumn(gv, "실적일자", "WorkDate");
            grid.Create_GvColumn(gv, "라인", "LineName");
            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "규격", "Spec");
            grid.Create_GvColumn(gv, "수주량", "Order_Qty");
            grid.Create_GvColumn(gv, "실적수량", "Result_Qty"); //입고량
            grid.Create_GvColumn(gv, "진척율 (%)", "ProgressRate");
            grid.Create_GvColumn(gv, "진척율 그래프", "ProgressRatebar");

            //포맷
            grid.SetCol_Type(gv.Columns["Order_Qty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Result_Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ProgressRate"], clsGrid.TypeList.TFloat);

            //칼럼 숨김
            gv.Columns["CustCode"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;
            gv.Columns["WorkDate"].Visible = false;      
            gv.Columns["Spec"].Visible = false;      


            //칼럼 색상   
            //gv.Columns["ProgressRate"].AppearanceCell.ForeColor = Color.Red;

            //칼럼너비
            gv.Columns["ProgressRatebar"].MinWidth = 250;

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            // ProgressBar를 RepositoryItemProgressBar 생성
            RepositoryItemProgressBar repoProgressBar = new RepositoryItemProgressBar();

            // 색상 설정
            repoProgressBar.LookAndFeel.UseDefaultLookAndFeel = false; //해줘야 적용됨
            repoProgressBar.ShowTitle = false;
            repoProgressBar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            repoProgressBar.Properties.StartColor = Color.LightSalmon;
            repoProgressBar.Properties.EndColor = Color.RoyalBlue;

            // 칼럼에 할당
            gv.Columns["ProgressRatebar"].ColumnEdit = repoProgressBar;

            //폰트굵게
            Font font_Bold = new Font(gv.Columns["ProgressRate"].AppearanceCell.Font, FontStyle.Bold);
            gv.Columns["ProgressRate"].AppearanceCell.Font = font_Bold;

            //타입별 그리드 설정
            if (formType == "Cmd")
            {
                gv.Columns["ObtainDate"].Caption = "지시일자";
                gv.Columns["Order_Qty"].Caption = "지시량";
                gv.Columns["CustName"].Visible = false;
                lblType.Text = "당월 총 지시 건수";
                lblOredrQty.Text = "당월 총 지시 수량";
            }
            else if ((formType == "Order"))
            {
                gv.Columns["ObtainDate"].Caption = "수주일자";
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
        #endregion gridview

        #region 데이터조회 / label값 설정
        private void Search()
        {
            string Cust_Cd = LupCustomer.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupCustomer.EditValue.ConvertString_Null_To_Empty();
            string ItemCode = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();
            string date = DtpDate.DateTime.ToString("yyyyMM");
            string datePre = DtpDate.DateTime.AddMonths(-1).ToString("yyyyMM");

            string startdate = DtpDate.DateTime.ToString("yyyy-MM-01");
            string enddate = Convert.ToDateTime(startdate).AddMonths(+1).AddDays(-1).ToString("yyyy-MM-dd");

            MM_ProductAnalysis bad_Chk = new MM_ProductAnalysis();
            bad_Chk.CustCode = Cust_Cd;
            bad_Chk.ItemCode = ItemCode;

            ////1. 데이터 테이블 조회
            //생산진행현황
            DataTable dtProdcut = null;
            if (formType == "Order")
            {
                //수주기반 생산진행현황  조회
                dtProdcut = bad_Chk.ProductEntireView_ByROrder(startdate, enddate);
            }
            else if (formType == "Cmd")
            {
                //지시기반 생산진행현황  조회
                dtProdcut = bad_Chk.ProductEntireView_ByCmd(startdate, enddate);
            }
            Gc_Product.DataSource = dtProdcut;

            //매출,매입금액
            string query = "";

            query = $@"
                        
                        SELECT
                            sm.ShipToCode ,
                            bc.CustName ,
                            SUM(CAST(sm.TotalPrice AS INT)) AS AMT
                            
                        FROM BM_SalesD sd
                        LEFT JOIN BM_SalesM sm on sd.CorpId = sm.CorpId and sd.SalesNo = sm.SalesNo
                        LEFT JOIN CM_Cust bc ON bc.CustCode = sm.ShipToCode AND sd.CorpID = bc.CorpID
                        
                        WHERE sd.CorpID = '{WrGlobal.CorpID}'
                        and sm.SalesDate between '{startdate}' and '{enddate}'

                        ";
            if (Cust_Cd != "NULL" && Cust_Cd != "")
            {
                query += $@"AND sm.ShipToCode = '{Cust_Cd}' ";
            }

            query += $@"GROUP BY  sm.ShipToCode,  bc.CustName
                        HAVING SUM(CAST(sd.TotalPrice AS INT)) <> 0 AND SUM(CAST(sd.TotalPrice AS INT)) IS NOT NULL
                        ORDER BY CustName";

            DataTable dtSlaes = db.GetDataTable(query);

            query = $@"
                          SELECT                          
                        pm.CustCode,
                        BC.CustName,
                        SUM(CAST(PD.TotalPrice AS INT)) AS AMT

                        FROM MM_PurchaseD PD

                        LEFT JOIN MM_PurchaseM pm ON  PD.CorpID = pm.CorpID and PD.StoNo = pm.StoNo
                        LEFT JOIN CM_Cust BC ON BC.CustCode = pm.CustCode AND PD.CorpID = BC.CorpID

                        WHERE PD.CorpID ='{WrGlobal.CorpID}'
                        AND pm.StoDate between  '{startdate}' and '{enddate}'

                        ";
            if (Cust_Cd != "NULL" && Cust_Cd != "")
{
                query += $"AND BC.CustCode = '{Cust_Cd}'";
}

            query += $@"GROUP BY  pm.CustCode,  BC.CustName
                        HAVING SUM(CAST(PD.TotalPrice AS INT)) <> 0 AND SUM(CAST(PD.TotalPrice AS INT)) IS NOT NULL
                        ORDER BY CustName";


            DataTable dtPurchase = db.GetDataTable(query);

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
            DataTable dtLblText = Entire_AMTLabel(startdate, enddate, Cust_Cd);
            
            if (dtLblText != null)
			{
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
            }


            //디자인조정
            Gv_Product.BestFitColumns();            
            Gv_Product.Columns["ProgressRatebar"].Width = 250; //칼럼너비
        }

        public DataTable Entire_AMTLabel(string startDate, string endDate, string cust_Cd)
        {
            string query = "";
            query += $@"
                	DECLARE @CorpId nvarchar(50) = '{WrGlobal.CorpID}'
					DECLARE @CustCd nvarchar(50) = '{cust_Cd}'
					DECLARE @startdate date = '{startDate}'
					DECLARE @enddate date = '{endDate}'

					DECLARE @prestartdate date = '{Convert.ToDateTime(startDate).AddMonths(-1).ToString("d")}'
					DECLARE @preenddate date = '{Convert.ToDateTime(startDate).AddDays(-1).ToString("d")}'

                    DECLARE @CurrentMonthAMT_P DECIMAL(18, 2);
                    DECLARE @LastMonthAMT_P DECIMAL(18, 2);
                    DECLARE @CurrentMonthAMT_S DECIMAL(18, 2);
                    DECLARE @LastMonthAMT_S DECIMAL(18, 2);

                       -- 현재 월의 실적
                    SELECT @CurrentMonthAMT_S = SUM(sd.TotalPrice)
                    FROM BM_SalesD sd
					left join BM_SalesM sm on sd.CorpId = sm.CorpId and sd.SalesNo = sm.SalesNo
                    WHERE sd.CorpID = @CorpId
                    AND sm.SalesDate between @startdate and @enddate
                        ";
            if (cust_Cd != "" && cust_Cd != "ALL")
            {
                query += $"AND sm.BillToCode = @CustCd ";
            }
            query += $@"
            
                     -- 전월의 실적0
                    SELECT @LastMonthAMT_S = SUM(sd.TotalPrice)
                       FROM BM_SalesD sd
					left join BM_SalesM sm on sd.CorpId = sm.CorpId and sd.SalesNo = sm.SalesNo
                    WHERE sd.CorpID = @CorpId
                    AND sm.SalesDate between @prestartdate and @preenddate
                        ";
            if (cust_Cd != "" && cust_Cd != "ALL")
                {
                query += $"AND sm.BillToCode = @CustCd ";
            }
            query += $@"

                      -- 현재 월의 실적
                    SELECT @CurrentMonthAMT_P = SUM(pd.TotalPrice)
                    FROM MM_PurchaseD pd 
					left join MM_PurchaseM pm on pd.CorpId = pm.CorpId and pd.StoNo = pm.StoNo
                    WHERE pd.CorpID = @CorpId
                    AND pm.StoDate between @startdate and @enddate
                        ";
            if (cust_Cd != "" && cust_Cd != "ALL")
                {
                query += $"AND pm.CustCode = @CustCd";
            }
            query += $@"
            
                        -- 전월의 실적
                    SELECT @LastMonthAMT_P = SUM(pd.TotalPrice)
                    FROM MM_PurchaseD pd 
					left join MM_PurchaseM pm on pd.CorpId = pm.CorpId and pd.StoNo = pm.StoNo
                    WHERE pd.CorpID = @CorpId
                    AND pm.StoDate between @prestartdate and @preenddate
                        ";
            if (cust_Cd != "" && cust_Cd != "ALL")
            {
                query += $"AND pm.CustCode = @CustCd";
            }
            query += $@"
                    SELECT
                    CONVERT(INT, @CurrentMonthAMT_P) AS 당월매입합계,
                    CONVERT(INT,@LastMonthAMT_P) AS 전월매입합계,
                    CONVERT(INT, @CurrentMonthAMT_S) AS 당월매출합계,
                    CONVERT(INT,@LastMonthAMT_S) AS 전월매출합계 ,
                    CONVERT(float, CASE WHEN @LastMonthAMT_S = 0 THEN 0 ELSE ROUND((@CurrentMonthAMT_S - @LastMonthAMT_S) / @LastMonthAMT_S * 100 , 2) END) AS 전월대비매출상승률,
                    CONVERT(float, CASE WHEN @LastMonthAMT_P = 0 THEN 0 ELSE ROUND((@CurrentMonthAMT_P - @LastMonthAMT_P) / @LastMonthAMT_P * 100 , 2) END) AS 전월대비매입상승률


                    ";


            return db.GetDataTable(query);
        }
        #endregion 데이터조회

        #region event
        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            switch (btn.Name)
            {
                case "cmdDisplay":
                    Search();
                    break;

                case "cmdClose":
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
        #endregion event
    }
}