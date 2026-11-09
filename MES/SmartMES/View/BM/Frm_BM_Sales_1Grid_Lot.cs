using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.ST;
using SmartMES.View.Lookup;
using SMT_MES.View.Business.Rpt;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
    public partial class Frm_BM_Sales_1Grid_Lot : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        Database db = new Database();
        string query = string.Empty;
        string ItemDiv = "20";
        int lastRowHandle = -1;
        private string[] CustDiv = { "20", "40" }; //거래처구분 리스트(10 : 매입, 20 : 매출, 40 :매입/매출)

        public Frm_BM_Sales_1Grid_Lot()
        {
            InitializeComponent();
            SetGridView();
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정
            tabbedControlGroup1.SelectedTabPageIndex = 1;

            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            smtUpdate.Visible = false;//LOT별 출고 시 수정하면 LOT번호가 사라져서 안보이게 처리

            SetLup();

            DisplayData();
        }

        private void SetLup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            //그리드 납품처(매출 거래처)
            //DataTable dt = code.smtLookUp("거래처", "매출", "", "");

            //repositoryItemSearchLookUpEdit1.DataSource = dt;
            //repositoryItemSearchLookUpEdit1.DisplayMember = "거래처명";
            //repositoryItemSearchLookUpEdit1.ValueMember = "거래처코드";

            //repositoryItemSearchLookUpEdit2.DataSource = dt;
            //repositoryItemSearchLookUpEdit2.DisplayMember = "거래처명";
            //repositoryItemSearchLookUpEdit2.ValueMember = "거래처코드";

            code.LookupCustomer(WrGlobal.CorpID, "매출", true, "", ref Lup_Cust);
            Lup_Cust.smtLookUpValue = "ALL";
        }

        private void SetGridView()
        {
            SetGridView_SdOrder(gvBM_SdOrderD);
            SetGridView_Stock(gvStock);
            SetGridView_Sales(gvSales);
            SetGridView_BM_SalesD(gvBM_SalesD);
        }

        private void SetGridView_SdOrder(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            //
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A011"), "코드", "명칭");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");
            //품목
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(), "품목코드", "품명");
            //거래처
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(CustDiv), "거래처코드", "거래처명");
            //창고
            RepositoryItemSearchLookUpEdit lup_WareHouse = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            //Column 생성
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");
            grid.Create_GvColumn(gv, "수주일자", "ObtainDate");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "수주량", "Qty", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "미납량", "RemainQty", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "단가", "UnitPrice", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "금액", "Price", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "부가세", "VatPrice", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "총금액", "TotalPrice", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "출고일자", "OutDate");
            grid.Create_GvColumn(gv, "거래처", "BillToCode");
            grid.Create_GvColumn(gv, "납품처", "ShipToCode");
            grid.Create_GvColumn(gv, "출고수량", "OutQty", rpo.Create_rpoTxt_Numeric());

            //grid.Create_GvColumn(gv, "출고창고", "HouseCode");
            //gv.Columns["HouseCode"].ColumnEdit = lup_WareHouse;

            //Repository
            gv.Columns["BillToCode"].ColumnEdit = lup_SoldToCode;
            gv.Columns["ShipToCode"].ColumnEdit = lup_SoldToCode;

            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //
            grid.Column_AllowEdit(gv.Columns["OutDate"]);
            grid.Column_AllowEdit(gv.Columns["BillToCode"]);
            grid.Column_AllowEdit(gv.Columns["ShipToCode"]);
            //grid.Column_AllowEdit(gv.Columns["OutQty"]);
            grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            //grid.Column_AllowEdit(gv.Columns["HouseCode"]);

            //Column 단일 설정
            if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] != "Y")
            {
                gv.Columns["ShipToCode"].Visible = false;
            }

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["RemainQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //컬럼 위치 설정
            gv.Columns["OutQty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["UnitPrice"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            //gv.Columns["HouseCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["ShipToCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["BillToCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["OutDate"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "TotalPrice");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellValueChanged += Gv_CellValueChanged;
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
            gv.BeforeLeaveRow += Gv_BeforeLeaveRow;
            grid.Set_Navigator(gvBM_SdOrderD);
        }

        private void Gv_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            gvBM_SdOrderD.SetRowCellValue(e.RowHandle, "OutQty", "0");
        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DisplayStock();
        }

        private void SetGridView_Stock(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "창고", "Stock_Cd");
            grid.Create_GvColumn(gv, "생산일자", "WorkDate");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "SerialNo", "SerialNo");
            grid.Create_GvColumn(gv, "LOTNo", "LotNo");
            grid.Create_GvColumn(gv, "재고수량", "StockQty", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "출고수량", "OutQty", rpo.Create_rpoTxt_Numeric());

            //
            grid.Column_AllowEdit(gv.Columns["OutQty"]);

            //Column 단일 설정

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["WorkDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TFloat);

            //컬럼 위치 설정
            gv.Columns["OutQty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            grid.SetCol_Summary(gv, "StockQty");
            grid.SetCol_Summary(gv, "OutQty");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellValueChanged += GvStock_CellValueChanged;
            grid.Set_Navigator(gvBM_SdOrderD);
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drChange = gv.GetFocusedDataRow();
            if (drChange == null) return;

            string value = e.Value.ConvertString_Null_To_Empty();
            string fieldName = e.Column.FieldName;

            //단가 변경 시 금액 계산
            if (fieldName == "UnitPrice")
            {
                float unitPrice = value.ConvertFloat_Null_To_Zero();
                float qty = drChange["Qty"].ConvertFloat_Null_To_Zero();

                drChange["Price"] = qty * unitPrice;
                drChange["VatPrice"] = (qty * unitPrice) * 0.1;
                drChange["TotalPrice"] = drChange["Price"].ConvertFloat_Null_To_Zero() + drChange["VatPrice"].ConvertFloat_Null_To_Zero();
            }

            gv.BestFitColumns();
        }

        private void GvStock_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;

            //상태값 수정
            grid.Change_Status(gv, e.RowHandle);

            //재고리스트의 출고수량 입력시 출고수량 합계 수주리스트의 출고수량에 반영
            if (fieldName == "OutQty")
            {
                decimal sum = 0;

                for (int i = 0; i < gvStock.RowCount; i++)
                {
                    object val = gvStock.GetRowCellValue(i, "OutQty");

                    if (!val.IsNullOrEmpty())
                        sum += Convert.ToDecimal(val);
                }

                gvBM_SdOrderD.SetRowCellValue(gvBM_SdOrderD.FocusedRowHandle, "OutQty", sum);
            }
        }

        private void SetGridView_Sales(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;

            //
            gv.OptionsBehavior.ReadOnly = true;

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", ItemDiv, "", ""), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");
            RepositoryItemSearchLookUpEdit lup_WareHouse = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "출고번호", "SalesNo");
            grid.Create_GvColumn(gv, "출고순번", "SalesSeq");
            grid.Create_GvColumn(gv, "출고일자", "SalesDate");
            //grid.Create_GvColumn(gv, "거래처", "BillToCode", lup_SoldToCode);
            grid.Create_GvColumn(gv, "거래처코드", "BillToCode");
            grid.Create_GvColumn(gv, "거래처명", "BillToName");
            grid.Create_GvColumn(gv, "납품처", "ShipToCode");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "수량", "Qty", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "단가", "UnitPrice", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "금액", "Price", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "부가세", "VatPrice", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "총금액", "TotalPrice", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "출고창고", "HouseCode", lup_WareHouse);
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            gv.Columns["ShipToCode"].ColumnEdit = lup_SoldToCode;

            //Repository 생성 or 이벤트 입력
            //gv.Columns["SalesNo"].Visible = false;
            //gv.Columns["SalesSeq"].Visible = false;
            gv.Columns["Status"].Visible = false;

            //Column 단일 설정

            //셀값 변경 허용

            if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] != "Y")
            {
                gv.Columns["ShipToCode"].Visible = false;
            }

            gv.Columns["BillToCode"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;
            gv.Columns["SdOrderSeq"].Visible = false;
            gv.Columns["CreIP"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModIP"].Visible = false;
            gv.Columns["ModDt"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;     //상태
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "TotalPrice");

            //칼럼 너비 설정
            gv.Columns["Status"].MaxWidth = 60;

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowClick += GvSales_RowClick;
            grid.Set_Navigator(gv);
        }

        private void SetGridView_BM_SalesD(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsBehavior.ReadOnly = true;

            RepositoryItemSearchLookUpEdit lup_WareHouse = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            //Column 생성
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "출고창고", "HouseCode", lup_WareHouse);
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "LOTNo", "LotNo");
            grid.Create_GvColumn(gv, "수량", "Qty", rpo.Create_rpoTxt_Numeric());

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);

            grid.SetCol_Summary(gv, "Qty");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        private void GvSales_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (lastRowHandle == e.RowHandle && layoutControlGroup9.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lastRowHandle = -1;
            }
            else
            {
                layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                DisplayBM_SalesD();
                lastRowHandle = e.RowHandle;
            }
        }

        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayData();
            layoutControlGroup9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void DisplayData()
        {
            this.Validate();

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            string custCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();

            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                string outProgress = RdoOutType.EditValue.ConvertString_Null_To_Empty();

                //수주조회
                BM_SdOrderD sd = new BM_SdOrderD();
                gcSdOrder.DataSource = sd.SelectBM_SdOrderD(custCode, startDate, endDate, custCode, outProgress);

                gvBM_SdOrderD.BestFitColumns();
            }
            else
            {
                BM_SalesD sd = new BM_SalesD();
                sd.CustCode = custCode;
                gcSales.DataSource = sd.Select_BM_SalesD(startDate, endDate, "10"); //출고구분 10: 제품, 20:반품, 30:자재,40:외주

                gvSales.BestFitColumns();
            }
        }

        private void DisplayStock()
        {
            //재고조회
            GridView gv = gcSdOrder.MainView as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            ST_StockD std = new ST_StockD();
            std.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            //std.HouseCode = Lup_House.smtLookUpValue.ConvertString_Null_To_Empty();
            //std.LotNo = TxtLot.smtTextBoxValue.ConvertString_Null_To_Empty();

            gcStock.DataSource = std.SelectStockByProdLot();

            gvStock.BestFitColumns();
        }

        private void DisplayBM_SalesD()
        {
            GridView gv = gcSales.MainView as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            ST_StockD std = new ST_StockD();
            std.JobNo = dr["SalesNo"].ConvertString_Null_To_Empty();
            std.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();

            gcBM_SalesD.DataSource = std.SelectBM_SalesD();

            gvBM_SalesD.BestFitColumns();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            tabbedControlGroup1.SelectedTabPageIndex = 1;

            Frm_LookUpSales frm = new Frm_LookUpSales("");

            if (frm.ShowDialog() == DialogResult.OK)
            {
                XtraMessageBox.Show(cls_MessageBox.Save_Comp);
            }

            tabbedControlGroup1.SelectedTabPageIndex = 1;
            DisplayData();
        }

        private void smtUpdate_Click(object sender, EventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex == 0) return;

            DataRow dr = gvSales.GetFocusedDataRow();
            if (dr == null) return;

            string salesNo = dr["SalesNo"].ConvertString_Null_To_Empty();

            Frm_LookUpSales frm = new Frm_LookUpSales(salesNo);
            frm.ShowDialog();

            DisplayData();
        }

        // 출고요청 버튼 클릭
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex != 0) return;
            if (gvBM_SdOrderD.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("선택한 내역이 없습니다.\n수주 건 선택 후 저장 하세요.");
                return;
            }

            gvBM_SdOrderD.PostEditor();
            gvBM_SdOrderD.UpdateCurrentRow();
            //임시테이블 데이터 지우기
            query = "Delete TMP_RequestSales Where HostName = Host_name()";
            db.ExecuteQuery(query);

            int row, outQty, sdOrderSeq;
            string shipToCode, custCode, ItemCode, sdOrderNo, houseCode, lotNo;
            decimal unitprice = 0;
            DateTime outDate;

            //납품처 및 출고수량 체크
            for (int rowCnt = 0; rowCnt < gvBM_SdOrderD.SelectedRowsCount; rowCnt++)
            {
                if (gvBM_SdOrderD.GetSelectedRows()[rowCnt] >= 0)
                {
                    row = gvBM_SdOrderD.GetSelectedRows()[rowCnt];
                    shipToCode = gvBM_SdOrderD.GetRowCellValue(row, "ShipToCode").ToString();
                    custCode = gvBM_SdOrderD.GetRowCellValue(row, "BillToCode").ToString();
                    outQty = gvBM_SdOrderD.GetRowCellValue(row, "OutQty").ConvertInt_Null_To_Zero();
                    houseCode = gvBM_SdOrderD.GetRowCellValue(row, "HouseCode").ToString();

                    if (outQty == 0)
                    {
                        MessageBox.Show("출고수량을 입력해주세요.");
                        return;
                    }

                    if (custCode == "")
                    {
                        MessageBox.Show("거래처가 없는 건이 있습니다.");
                        return;
                    }
                    //if (houseCode == "")
                    //{
                    //    MessageBox.Show("출고창고가 없는 건이 있습니다.");
                    //    return;
                    //}

                    //납품처 사용 옵션 -> 납품처 사용 -> 납품처 미입력 메시지
                    if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] == "Y")
                    {
                        if (shipToCode == "")
                        {
                            MessageBox.Show("납품처가 없는 건이 있습니다.");
                            return;
                        }
                    }
                    else
                    {
                        gvBM_SdOrderD.SetRowCellValue(gvBM_SdOrderD.GetSelectedRows()[rowCnt], "ShipToCode", custCode); //납품처 재고출고에서 사용하여 없을경우 거래처로 넣음
                    }
                }
            }

            this.Validate();

            DataTable dt = null;

            gvStock.PostEditor();
            gvStock.UpdateCurrentRow();
            dt = gcStock.DataSource as DataTable;

            int sdRow = gvBM_SdOrderD.FocusedRowHandle;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i]["Status"]) == "수정")
                {
                    outQty = Convert.ToInt32(dt.Rows[i]["OutQty"] ?? 0);
                    if (outQty > 0)
                    {
                        custCode = gvBM_SdOrderD.GetRowCellValue(sdRow, "BillToCode").ToString();
                        shipToCode = gvBM_SdOrderD.GetRowCellValue(sdRow, "ShipToCode").ToString();
                        outDate = (DateTime)gvBM_SdOrderD.GetRowCellValue(sdRow, "OutDate");
                        ItemCode = gvBM_SdOrderD.GetRowCellValue(sdRow, "ItemCode").ToString();
                        sdOrderNo = gvBM_SdOrderD.GetRowCellValue(sdRow, "SdOrderNo").ToString();
                        sdOrderSeq = Convert.ToInt32(gvBM_SdOrderD.GetRowCellValue(sdRow, "SdOrderSeq"));
                        unitprice = gvBM_SdOrderD.GetRowCellValue(sdRow, "UnitPrice").ConvertDecimal_Null_To_Zero();

                        houseCode = dt.Rows[i]["Stock_Cd"].ConvertString_Null_To_Empty();
                        lotNo = dt.Rows[i]["SerialNo"].ConvertString_Null_To_Empty();
                        //outQty = Convert.ToInt32(gvStock.GetRowCellValue(i, "OutQty"));

                        query = $"INSERT INTO TMP_RequestSales (HostName,CustCode,ShipToCode,OutDate,ItemCode,OutQty, SdOrderNo, SdOrderSeq, HouseCode, UnitPrice, LotNo)";
                        query = query + $"values (Host_name(),'{custCode}','{shipToCode}','{outDate.ToString("yyyy-MM-dd HH:mm:ss")}','{ItemCode}',{outQty},'{sdOrderNo}', {sdOrderSeq}, '{houseCode}', '{unitprice}', '{lotNo}')";
                        db.ExecuteQuery(query);
                    }
                }
            }

            query = $"exec Sales_MakeSalesorder_SdOrder '{WrGlobal.CorpID}', '{WrGlobal.LoginID}'";
            db.ExecuteQuery(query);

            DisplayData();
            MessageBox.Show(cls_MessageBox.Save_Comp);
        }

        // 출고 취소
        private void CmdCancel_Click(object sender, EventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                XtraMessageBox.Show("수주 리스트 조회 상태에서 삭제 할 수 없습니다.\n출고 리스트 조회 후 삭제를 진행하세요.");
                return;
            }

            if (XtraMessageBox.Show(cls_MessageBox.DelMsg_Select, cls_MessageBox.DelCaption, MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            int row;
            string salesNo, salesSeq;

            for (int rowCnt = 0; rowCnt < gvSales.SelectedRowsCount; rowCnt++)
            {
                if (gvSales.GetSelectedRows()[rowCnt] >= 0)
                {
                    row = gvSales.GetSelectedRows()[rowCnt];
                    //orderNo = gvSales.GetRowCellDisplayText(row, "SdOrderNo");
                    //orderSeq = gvSales.GetRowCellDisplayText(row, "SdOrderSeq");
                    salesNo = gvSales.GetRowCellDisplayText(row, "SalesNo");
                    salesSeq = gvSales.GetRowCellDisplayText(row, "SalesSeq").ConvertString_Null_To_Zero();

                    query = $"exec Sales_CancelSalesorder_SalesNo '{WrGlobal.CorpID}'";
                    query = query + $",'{salesNo}'";
                    query = query + $",{salesSeq}";
                    db.ExecuteQuery(query);
                }
            }
            DisplayData();
            MessageBox.Show(cls_MessageBox.Del_Comp);
        }

        public class GvPreRequest
        {
            public string ShipToCode { get; internal set; }
        }

        // 포커스된 행 색상 변경
        private void gvPreRequest_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.Assign(view.PaintAppearance.SelectedRow);
                e.Appearance.Options.UseForeColor = true;
            }
        }

        //엑셀변환 하기
        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (tabbedControlGroup1.SelectedTabPageIndex == 0)
                    gvBM_SdOrderD.ExportToXls(sfd.FileName);
                else
                    gvSales.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        private void tabbedControlGroup1_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            DisplayData();
        }

        private void gvMachine_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayData();
        }

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void Print()
        {
            if (tabbedControlGroup1.SelectedTabPageIndex != 1)
            {
                XtraMessageBox.Show("수주조회 상태에서 출력 할 수 없습니다.", "");
                return;
            }

            GridControl gc = gcSales;

            DataTable dt = gc.DataSource as DataTable;
            if (dt == null) return;
            if (dt.Rows.Count < 1) return;

            GridView gv = gc.MainView as GridView;
            if (gv == null) return;

            int[] selrow = gv.GetSelectedRows();
            if (selrow.Length < 1)
            {
                XtraMessageBox.Show("선택한 내역이 없습니다", "");
                return;
            }

            DataTable selDataTable = dt.Copy().Clone();

            for (int cnt = 0; cnt < gv.SelectedRowsCount; cnt++)
            {
                DataRow dr = selDataTable.NewRow();
                dr.ItemArray = gv.GetDataRow(selrow[cnt]).ItemArray;
                //dr["Item_Nm"] = gv.GetRowCellDisplayText(selrow[cnt], "Item_Nm");

                selDataTable.Rows.Add(dr);
            }

            DataView dv = new DataView(selDataTable);
            dv.Sort = "SalesDate ASC, BillToCode ASC";


            //업체별 거래명세서 옵션
            string rptNm = "";

            if (WrGlobal.ProcOption["BN"].ContainsKey("InVoice"))
            {
                rptNm = WrGlobal.ProcOption["BN"]["InVoice"];
            }

            XtraReport report = null;
            string printDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            switch (rptNm)
            {
                case "Rpt_InVoice":
                    report = new Rpt_InVoice(dv.ToTable(), printDate);
                    break;

                case "Rpt_InVoice_TPG":
                    report = new Rpt_InVoice_TPG(dv.ToTable(), printDate);
                    break;

                default:
                    report = new Rpt_InVoice(dv.ToTable(), printDate);
                    break;
            }

            ReportPrintTool reportTool = new ReportPrintTool(report);
            reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            reportTool.ShowPreviewDialog();
        }

        private void cmdAccPrint_Click(object sender, EventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex != 1)
            {
                XtraMessageBox.Show("수주조회 상태에서 출력 할 수 없습니다.", "");
                return;
            }

            GridControl gc = gcSales;

            DataTable dt = gc.DataSource as DataTable;
            if (dt == null) return;
            if (dt.Rows.Count < 1) return;

            GridView gv = gc.MainView as GridView;
            if (gv == null) return;

            int[] selrow = gv.GetSelectedRows();
            if (selrow.Length < 1)
            {
                XtraMessageBox.Show("선택한 내역이 없습니다", "");
                return;
            }

            DataTable selDataTable = dt.Copy().Clone();

            for (int cnt = 0; cnt < gv.SelectedRowsCount; cnt++)
            {
                DataRow dr = selDataTable.NewRow();
                dr.ItemArray = gv.GetDataRow(selrow[cnt]).ItemArray;
                //dr["Item_Nm"] = gv.GetRowCellDisplayText(selrow[cnt], "Item_Nm");

                selDataTable.Rows.Add(dr);
            }

            DataView dv = new DataView(selDataTable);
            dv.Sort = "SalesDate ASC, BillToCode ASC";

            string printDate = DateTime.Now.ToString("d");

            XtraReport report = new RptAcceptanceReceipt(dv.ToTable(), printDate);

            ReportPrintTool reportTool = new ReportPrintTool(report);
            reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            reportTool.ShowPreviewDialog();
        }

        private void cmdSelectOut_Click(object sender, EventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex != 0) return;

            if (gvBM_SdOrderD.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("선택한 내역이 없습니다.\n수주 건 선택 후 진행하세요.");
                return;
            }

            if (XtraMessageBox.Show("선택한 수주 건의 미납량만큼 LOT별로 자동 출고 처리하시겠습니까?",
                "", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            gvBM_SdOrderD.PostEditor();
            gvBM_SdOrderD.UpdateCurrentRow();

            //임시테이블 데이터 지우기
            query = "Delete TMP_RequestSales Where HostName = Host_name()";
            db.ExecuteQuery(query);

            int idx = gvBM_SdOrderD.FocusedRowHandle;
            int[] selRows = gvBM_SdOrderD.GetSelectedRows();

            var shortageList = new System.Collections.Generic.List<string>();
            var noStockList = new System.Collections.Generic.List<string>();
            bool processedAny = false;

            //품목코드별 재고 LOT 목록 캐시 (이번 실행 동안 공유되며 배분할 때마다 잔량이 차감됨)
            var stockPool = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<LotStock>>();

            for (int rowCnt = 0; rowCnt < selRows.Length; rowCnt++)
            {
                int row = selRows[rowCnt];
                if (row < 0) continue;

                string shipToCode = gvBM_SdOrderD.GetRowCellValue(row, "ShipToCode").ConvertString_Null_To_Empty();
                string custCode = gvBM_SdOrderD.GetRowCellValue(row, "BillToCode").ConvertString_Null_To_Empty();
                string itemCode = gvBM_SdOrderD.GetRowCellValue(row, "ItemCode").ConvertString_Null_To_Empty();
                string sdOrderNo = gvBM_SdOrderD.GetRowCellValue(row, "SdOrderNo").ConvertString_Null_To_Empty();
                int sdOrderSeq = gvBM_SdOrderD.GetRowCellValue(row, "SdOrderSeq").ConvertInt_Null_To_Zero();
                decimal unitPrice = gvBM_SdOrderD.GetRowCellValue(row, "UnitPrice").ConvertDecimal_Null_To_Zero();
                decimal remainQty = gvBM_SdOrderD.GetRowCellValue(row, "RemainQty").ConvertDecimal_Null_To_Zero();

                object outDateObj = gvBM_SdOrderD.GetRowCellValue(row, "OutDate");
                DateTime outDate = (outDateObj == null || outDateObj == DBNull.Value)
                    ? DateTime.Now
                    : Convert.ToDateTime(outDateObj);

                if (remainQty <= 0) continue;

                //거래처/납품처 체크
                if (custCode == "")
                {
                    MessageBox.Show($"[{sdOrderNo}-{sdOrderSeq}] 거래처가 없어 처리할 수 없습니다.");
                    return;
                }

                if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] == "Y")
                {
                    if (shipToCode == "")
                    {
                        MessageBox.Show($"[{sdOrderNo}-{sdOrderSeq}] 납품처가 없어 처리할 수 없습니다.");
                        return;
                    }
                }
                else
                {
                    shipToCode = custCode;
                    gvBM_SdOrderD.SetRowCellValue(row, "ShipToCode", custCode);
                }

                //해당 품목의 LOT 재고를 아직 로드하지 않았으면 이번 1회만 DB 조회 후 캐시
                if (!stockPool.ContainsKey(itemCode))
                {
                    ST_StockD std = new ST_StockD();
                    std.ItemCode = itemCode;
                    DataTable dtStock = std.SelectStockByProdLot();

                    var lotList = new System.Collections.Generic.List<LotStock>();
                    if (dtStock != null)
                    {
                        foreach (DataRow r in dtStock.Rows)
                        {
                            lotList.Add(new LotStock
                            {
                                HouseCode = r["Stock_Cd"].ConvertString_Null_To_Empty(),
                                LotNo = r["SerialNo"].ConvertString_Null_To_Empty(),
                                RemainQty = r["StockQty"].ConvertDecimal_Null_To_Zero()
                            });
                        }
                    }
                    stockPool[itemCode] = lotList;
                }

                var lots = stockPool[itemCode];

                if (lots.Count == 0)
                {
                    noStockList.Add($"{sdOrderNo}-{sdOrderSeq} ({itemCode}) : 재고 없음");
                    continue;
                }

                decimal remainToAllocate = remainQty;
                decimal allocatedQty = 0;

                //재고가 있는 만큼 순서대로 배분 (부족하면 있는 만큼만)
                foreach (var lot in lots)
                {
                    if (remainToAllocate <= 0) break;
                    if (lot.RemainQty <= 0) continue;

                    decimal targetOutQty = (remainToAllocate >= lot.RemainQty) ? lot.RemainQty : remainToAllocate;

                    //메모리상의 잔량을 차감 -> 이후 같은 품목의 다른 수주 건이 이 줄어든 잔량을 보게 됨
                    lot.RemainQty -= targetOutQty;
                    remainToAllocate -= targetOutQty;
                    allocatedQty += targetOutQty;

                    query = "INSERT INTO TMP_RequestSales (HostName,CustCode,ShipToCode,OutDate,ItemCode,OutQty, SdOrderNo, SdOrderSeq, HouseCode, UnitPrice, LotNo)";
                    query += "values (Host_name()" +
                             $",'{custCode}'" +
                             $",'{shipToCode}'" +
                             $",'{outDate:yyyy-MM-dd HH:mm:ss}'" +
                             $",'{itemCode}'" +
                             $",{targetOutQty.ToString(System.Globalization.CultureInfo.InvariantCulture)}" +
                             $",'{sdOrderNo}'" +
                             $",{sdOrderSeq}" +
                             $",'{lot.HouseCode}'" +
                             $",'{unitPrice.ToString(System.Globalization.CultureInfo.InvariantCulture)}'" +
                             $",'{lot.LotNo}')";
                    db.ExecuteQuery(query);
                }

                //화면 표시용 출고수량 반영
                gvBM_SdOrderD.SetRowCellValue(row, "OutQty", allocatedQty);

                if (allocatedQty > 0)
                    processedAny = true;

                //부족분 있으면 안내 목록에 기록
                if (remainToAllocate > 0)
                {
                    shortageList.Add($"{sdOrderNo}-{sdOrderSeq} ({itemCode}) : {allocatedQty:N0}만 출고 / 부족 {remainToAllocate:N0}");
                }
            }

            if (!processedAny)
            {
                string msg = "처리 가능한 건이 없습니다.";
                if (noStockList.Count > 0)
                    msg += "\n\n[재고 없음]\n" + string.Join("\n", noStockList);

                MessageBox.Show(msg);
                return;
            }

            query = $"exec Sales_MakeSalesorder_SdOrder '{WrGlobal.CorpID}', '{WrGlobal.LoginID}'";
            db.ExecuteQuery(query);

            DisplayData();

            gvBM_SdOrderD.FocusedRowHandle = idx;
            gvBM_SdOrderD.TopRowIndex = idx;

            string resultMsg = cls_MessageBox.Save_Comp;
            if (shortageList.Count > 0)
                resultMsg += "\n\n[재고 부족 - 부분 출고됨]\n" + string.Join("\n", shortageList);
            if (noStockList.Count > 0)
                resultMsg += "\n\n[재고 없음 - 미처리]\n" + string.Join("\n", noStockList);

            MessageBox.Show(resultMsg);
        }
    }
}

public class LotStock
{
    public string HouseCode;
    public string LotNo;
    public decimal RemainQty;
}