using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_SdOrderView_Shortage : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_BM_SdOrderView_Shortage()
        {
            InitializeComponent();
        }

        private void Frm_BM_SdOrderView_Shortage_Load(object sender, EventArgs e)
        {
            Set_Lup();
            Set_GvOrder(GvOrder);
            Set_GridView_List(GvList);

            // 조회
            Display_Order();
        }

        #region 디자인

        private void Set_Lup()
        { 
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Cust.smtLookUpDataSource = code.Get_CustomerDt(true);
            Lup_Cust.smtLookUpDataDisplayMember = "내용";
            Lup_Cust.smtLookUpDataValueMember = "코드";
            Lup_Cust.smtLookUpValue = "ALL";

            CM_Item cm = new CM_Item();
            Lup_Item.smtLookUpDataSource = cm.Select_MaterialList_All(true);
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";
        }

        private void Set_GvOrder(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.IndicatorWidth = 50;                     //순번 너비
            gv.OptionsView.ColumnAutoWidth = false;

            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            clsRepository rpo = new clsRepository();
            RepositoryItemSearchLookUpEdit lup_SalesUser = rpo.Create_RepositoryItemSearchLookUp(code.Get_UserList(), "코드", "코드명");
            RepositoryItemSearchLookUpEdit lup_Cust = rpo.Create_RepositoryItemSearchLookUp(code.Select_Customer_ByKey(), "거래처코드", "거래처명");

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A011"), "코드", "명칭");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "수주상태", "OrderState");
            grid.Create_GvColumn(gv, "수주일", "ObtainDate");
            grid.Create_GvColumn(gv, "납기일", "DeliveryDate");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
			grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");
			grid.Create_GvColumn(gv, "거래처", "SoldToCode", lup_Cust);
			grid.Create_GvColumn(gv, "납품처", "ShipToCode", lup_Cust);

			grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");


			grid.Create_GvColumn(gv, "포장입수량", "PackQty");
			grid.Create_GvColumn(gv, "수주수량", "Qty");
			grid.Create_GvColumn(gv, "재고수량", "StockQty");
			grid.Create_GvColumn(gv, "과부족수량", "OverQty");

			grid.Create_GvColumn(gv, "반품량", "ReturnQty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총액", "AdjustedTotalPrice");   // 총액 : TotalPrice, 반품금액 포함 : AdjustedTotalPrice
            grid.Create_GvColumn(gv, "견적일", "EstimateDate");
            grid.Create_GvColumn(gv, "담당자", "SalesMan", lup_SalesUser);
            grid.Create_GvColumn(gv, "진행상황", "State");

            //Repository
            if(gv.Columns.ColumnByFieldName("UnitCode") != null)        gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if(gv.Columns.ColumnByFieldName("ItemClass1") != null)  gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if(gv.Columns.ColumnByFieldName("ItemClass2") != null)  gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            // 포맷
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["AdjustedTotalPrice"], clsGrid.TypeList.TFloat);

            grid.SetCol_Type(gv.Columns["PackQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OrderBoxQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["StockBoxQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OverQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OverBoxQty"], clsGrid.TypeList.TFloat);

            gv.OptionsView.ShowFooter = true;   //하단 합계창 보이기

            gv.Columns["Qty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv.Columns["Qty"].SummaryItem.DisplayFormat = "{0:#,0}";

            gv.Columns["Price"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv.Columns["Price"].SummaryItem.DisplayFormat = "{0:#,0}";

            gv.Columns["VatPrice"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv.Columns["VatPrice"].SummaryItem.DisplayFormat = "{0:#,0}";

            gv.Columns["AdjustedTotalPrice"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gv.Columns["AdjustedTotalPrice"].SummaryItem.DisplayFormat = "{0:#,0}";

            gv.Columns["EstimateDate"].Visible = false;
            gv.Columns["Size"].Visible = false;

            gv.Columns["OrderState"].Visible = false;
            gv.Columns["State"].Visible = false;

            gv.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.False;   //그룹 컬럼 표시 여부
            gv.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;   //그룹 Summary 요약 여부

            //
            grid.RowNumber_Indicator(gv);
            gv.SelectionChanged += Gv_SelectionChanged;
        }

        private void Gv_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            DisplayList();
        }

        private void Set_GridView_List(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "품번", "Alias");
            grid.Create_GvColumn(gv, "규격", "Size");
            grid.Create_GvColumn(gv, "단위", "UnitCode", lup_UnitCode);

            grid.Create_GvColumn(gv, "소요량", "TotalRequiredQty");
            grid.Create_GvColumn(gv, "현재고", "CurStockQty");
            grid.Create_GvColumn(gv, "예상재고", "ExpectStockQty");
            grid.Create_GvColumn(gv, "안전재고", "SaftyQty");
            grid.Create_GvColumn(gv, "과부족량", "DiffQty");

            grid.SetCol_Type(gv.Columns["TotalRequiredQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["CurStockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ExpectStockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["SaftyQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["DiffQty"], clsGrid.TypeList.TFloat);


            gv.Columns["ExpectStockQty"].Visible = false;

            //이벤트 설정
            grid.RowNumber_Indicator(gv); //행 번호 표기
            grid.Set_Navigator(gv);
            gv.CustomDrawCell += Gv_CustomDrawCell;
            gv.BestFitColumns();
        }

        private void Gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "DiffQty")
            {
                float saftyQty = GvList.GetRowCellValue(e.RowHandle, "SaftyQty").ConvertFloat_Null_To_Zero();

                if (e.CellValue.ConvertFloat_Null_To_Zero() < saftyQty)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        #endregion

        #region 조회

        private void Display_Order()
        {
            this.Validate();
            BM_SdOrderD sod = new BM_SdOrderD();

            sod.StartDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            sod.EndDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            sod.CustCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();
            sod.ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            
            GcOrder.DataSource = sod.SelectBM_SdOrderD_All();

            GvOrder.BestFitColumns();
        }

        private void DisplayList()
        {
            this.Validate();

            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            int[] selectedRows = GvOrder.GetSelectedRows();

            if (selectedRows.Length == 0)
            {
                MessageBox.Show("조회할 수주건을 먼저 선택하세요.", "알림");
                return;
            }

            //선택한 내역의 수주번호, 순번 가져오기
            List<string> orderList = new List<string>();

            foreach (int rowHandle in selectedRows)
            {
                string orderNo = GvOrder.GetRowCellValue(rowHandle, "SdOrderNo")?.ToString();
                string orderSeq = GvOrder.GetRowCellValue(rowHandle, "SdOrderSeq")?.ToString();

                if (!string.IsNullOrEmpty(orderNo) && !string.IsNullOrEmpty(orderSeq))
                {
                    orderList.Add($"('{orderNo}', {orderSeq})");
                }
            }

            string orderCondition = string.Join(", ", orderList);

            BM_SdOrderD sd = new BM_SdOrderD();
            GcList.DataSource = sd.SelectOrder_ShortageView(orderCondition);
            GvList.BestFitColumns();
        }
        #endregion

        #region 이벤트

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Display_Order();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(GvOrder);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		#endregion

	}
}