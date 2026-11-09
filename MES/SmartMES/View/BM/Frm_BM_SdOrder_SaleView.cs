using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_SdOrder_SaleView : DevExpress.XtraEditors.XtraForm
    {
        /*수주대비 납품현황*/
        clsCode code = new clsCode();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_BM_SdOrder_SaleView()
        {
            InitializeComponent();

            Set_Lup();
            Set_GvOrder(GvOrder);
            Set_State(radioGroup1);

            // 조회
            Display();
        }

        private void Set_State(RadioGroup rdo)
        {
            clsRadioGroup clsRadio = new clsRadioGroup();

            rdo.Properties.Items.Add(new RadioGroupItem("ALL", "전체"));
            rdo.Properties.Items.Add(new RadioGroupItem("대기", "대기"));
            rdo.Properties.Items.Add(new RadioGroupItem("출고완료", "출고완료"));
            rdo.EditValue = "ALL";

            rdo.SelectedIndex = 0;

            rdo.SelectedIndexChanged += radioGroup1_SelectedIndexChanged;
        }

        private void Set_Lup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            //Lup_Cust.smtLookUpDataSource = code.Get_CustomerDt(true);
            //Lup_Cust.smtLookUpDataDisplayMember = "내용";
            //Lup_Cust.smtLookUpDataValueMember = "코드";

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

            gv.OptionsView.ShowFooter = true;   //하단 합계창 보이기

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "출고상태", "OutState");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "순번", "SdOrderSeq");
            grid.Create_GvColumn(gv, "수주처", "SoldToCode");
            grid.Create_GvColumn(gv, "수주일자", "ObtainDate");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "수주량", "Qty");
            grid.Create_GvColumn(gv, "출하량", "OutQty");
            grid.Create_GvColumn(gv, "미출하수량", "NOutQty");

            // 포맷
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NOutQty"], clsGrid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            //grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            //grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            //grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);


            if(WrGlobal.LoginID != "Dev")
            {
                gv.Columns["ItemCode"].Visible = false;
            }

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            GridGroupSummaryItem itemQty = new GridGroupSummaryItem();
            itemQty.FieldName = "Qty";
            itemQty.ShowInGroupColumnFooter = gv.Columns["Qty"];
            itemQty.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            itemQty.DisplayFormat = "총 수주량 = {0:#,0}";
            gv.GroupSummary.Add(itemQty);

            GridGroupSummaryItem itemOutQty = new GridGroupSummaryItem();
            itemOutQty.FieldName = "OutQty";
            itemOutQty.ShowInGroupColumnFooter = gv.Columns["OutQty"];
            itemOutQty.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            itemOutQty.DisplayFormat = "총 출하량 = {0:#,0}";
            gv.GroupSummary.Add(itemOutQty);

            gv.ExpandAllGroups();
            gv.BestFitColumns();

            gv.OptionsBehavior.AllowPartialGroups = DevExpress.Utils.DefaultBoolean.False;   //그룹 컬럼 표시 여부
            gv.OptionsBehavior.AlignGroupSummaryInGroupRow = DevExpress.Utils.DefaultBoolean.False;   //그룹 Summary 요약 여부
        }

        #region 조회

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void Display()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            string outState = radioGroup1.EditValue?.ToString() ?? "ALL";

            BM_SdOrderD sd = new BM_SdOrderD();
            sd.ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();

            GcOrder.DataSource = sd.SelectSdOrder_Summary(startDate, endDate, outState);
            GvOrder.BestFitColumns();
        }

        #endregion

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Display();
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
    }
}



