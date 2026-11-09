using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.PM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_ProductPlanShortageView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        string ItemType = WrGlobal.ProcOption["SC"]["Product_ItemType"];
        clsCode code = new clsCode();

        Database db = new Database();
        string query = "";

        public Frm_PM_ProductPlanShortageView()
        {
            InitializeComponent();
            SetDesign();
        }

        private void frmPM_ProductPlanShortageView_Load(object sender, EventArgs e)
        {
            DisplayPM_ProductPlan();
            //DisplayList();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //SerchLookUp 설정
            Set_SearchLup();

            //GridView 설정
            Set_GridView_PM_ProductPlan(GvPM_ProductPlan);
            Set_GridView_List(GvList);
        }

        private void Set_SearchLup()
        {
            DtResult.smtStartDateTimeValue = DateTime.Now.AddDays(-1);
            DtResult.smtEndDateTimeValue = DateTime.Now;

            LupMaterial.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");

            LupMaterial.smtLookUpDataDisplayMember = "품명";
            LupMaterial.smtLookUpDataValueMember = "품목코드";
            LupMaterial.smtLookUpValue = "ALL";
        }

        private void Set_GridView_PM_ProductPlan(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            //품목
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");


            grid.Create_GvColumn(gv, "계획번호", "OrderNo");
            grid.Create_GvColumn(gv, "계획일자", "WorkDate");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "계획수량", "OrderQty");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");
            //grid.Create_GvColumn(gv, "재고수량", "StockQty");
            //grid.Create_GvColumn(gv, "과부족량", "Over_Shorts_Qty");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            gv.Columns["ItemName"].FieldName = "col_ItemName";

            //
            gv.Columns["OrderNo"].Visible = false;
            gv.Columns["SdOrderNo"].Visible = false;
            gv.Columns["SdOrderSeq"].Visible = false;

            //
            gv.Columns["OrderQty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //Column Type
            grid.SetCol_Type(gv.Columns["WorkDate"], clsGrid.TypeList.TDate);

            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Over_Shorts_Qty"], clsGrid.TypeList.TFloat);

            //
            //grid.SetCol_Summary(gv, "OrderQty");
            //grid.SetCol_Summary(gv, "Over_Shorts_Qty");

            //이벤트 설정
            grid.RowNumber_Indicator(gv); //행 번호 표기
            grid.Set_Navigator(gv);
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

            if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            {
                gv.Columns["Alias"].Visible = false;
            }

            //이벤트 설정
            grid.RowNumber_Indicator(gv); //행 번호 표기
            grid.Set_Navigator(gv);
			gv.CustomDrawCell += Gv_CustomDrawCell;
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

		#endregion 디자인 아이템 생성 End

        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton Btn = (smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdDisplay":
                    DisplayPM_ProductPlan();
                    //DisplayList();
                    break;

                case "cmdExcel":
                    clsFunction func = new clsFunction();
                    func.ExportExcel(GvList);
                    break;

                case "cmdClose":
                    this.Close();
                    break;
            }
        }

        #region 데이터 조회

        private void DisplayPM_ProductPlan()
        {
            this.Validate();

            string startDate = DtResult.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = DtResult.smtEndDateTimeValue.ConvertDate_To_Char8();
            string ItemCode = LupMaterial.smtLookUpValue;

            PM_ProductPlan wo = new PM_ProductPlan();
            wo.ItemCode = ItemCode;

            GcPM_ProductPlan.DataSource = wo.Select_PM_ProductPlan_Indiv(startDate, endDate);
            GvPM_ProductPlan.BestFitColumns();

            GvPM_ProductPlan.SelectAll();
        }

        private void DisplayList()
		{
            this.Validate();

            string startDate = DtResult.smtStartDateTimeValue.ToString("d");
            string endDate = DtResult.smtEndDateTimeValue.ToString("d");

            int[] selectedRows = GvPM_ProductPlan.GetSelectedRows();

            if (selectedRows.Length == 0)
            {
                GcList.DataSource = null;
                return;
            }

            //선택한 내역의 계획번호 가져오기
            List<string> orderNoList = new List<string>();

            foreach (int rowHandle in selectedRows)
            {
                string orderNo = GvPM_ProductPlan.GetRowCellValue(rowHandle, "OrderNo") as string;

                if (!string.IsNullOrEmpty(orderNo))
                {
                    orderNoList.Add(orderNo);
                }
            }

            string orderNos = string.Join(", ", orderNoList.Select(no => $"'{no}'"));

            PM_ProductPlan wo = new PM_ProductPlan();
            GcList.DataSource = wo.SelectPM_ProductPlanShortageView(orderNos);
            GvList.BestFitColumns();
		}

		#endregion
	}
}
