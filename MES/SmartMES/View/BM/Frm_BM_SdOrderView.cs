using DevExpress.XtraEditors;
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

namespace SmartMES.View.BM
{
	public partial class Frm_BM_SdOrderView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        Database db = new Database();
        string query = "";

        List<string> chkList = new List<string>();

        public Frm_BM_SdOrderView()
        {
            InitializeComponent();

            Set_Lup();
            Set_GvOrder(GvOrder);
            Set_ChkEvent();

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
			grid.Create_GvColumn(gv, "주문EA", "Qty");
			grid.Create_GvColumn(gv, "주문BOX", "OrderBoxQty");
			grid.Create_GvColumn(gv, "재고EA", "StockQty");
			grid.Create_GvColumn(gv, "재고BOX", "StockBoxQty");
			grid.Create_GvColumn(gv, "과부족EA", "OverQty");
			grid.Create_GvColumn(gv, "과부족BOX", "OverBoxQty");

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
            gv.Columns["ShipToCode"].Visible = false;

            gv.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.False;   //그룹 컬럼 표시 여부
            gv.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;   //그룹 Summary 요약 여부

            grid.RowNumber_Indicator(gv);

            if(Database.DbName.Contains("ALLTHEN")) gv.CustomDrawCell += gvObtainMaster_CustomDrawCell;
            if (Database.DbName.Contains("TPG"))
            {
                gv.Columns["ItemClass2"].Visible = false;
                gv.Columns["UnitCode"].Visible = false;

                gv.CustomDrawCell += Gv_CustomDrawCell_TPG;
            }
        }

        private void gvObtainMaster_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("OrderState") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "수주확정")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Green;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.Transparent;
            }
        }

        private void Gv_CustomDrawCell_TPG(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName == "OverQty" || fieldName == "OverBoxQty")
			{
                //과부족 - 빨간색 표기
                float value = e.CellValue.ConvertFloat_Null_To_Zero();

                if(value < 0)
				{
                    e.Appearance.ForeColor = Color.Red;
				}
				else
				{
                    e.Appearance.ForeColor = Color.Black;
				}
			}
        }

		private void Set_ChkEvent()
        {
            ChkSoldToCode.CheckedChanged += ChkTest_CheckedChanged;    
            ChkMaterial.CheckedChanged += ChkTest_CheckedChanged;    
            ChkSalesMan.CheckedChanged += ChkTest_CheckedChanged;    
        }

        private void ChkTest_CheckedChanged(object sender, EventArgs e)
        {
            GridView gv = GvOrder;

            CheckEdit chk = sender as CheckEdit;

            string name = chk.Tag.ConvertString_Null_To_Empty();

            if (chk.Checked) chkList.Add(name);
            else chkList.Remove(name);

            gv.ClearGrouping(); // 그룹 초기화

            // 체크한 항목만 그룹화
            foreach (string _name in chkList)
            {
                gv.Columns[_name].Group();
            }

            gv.ExpandAllGroups();
            gv.BestFitColumns();
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