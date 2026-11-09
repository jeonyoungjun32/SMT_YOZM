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
using SmartMES.View.Lookup;
using SMT_MES.View.Business.Rpt;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_Sales_1Grid : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        Database db = new Database();
        string query = string.Empty;
        string ItemDiv = "20";

        public Frm_BM_Sales_1Grid()
        {
            InitializeComponent();
            SetGridView();
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정
            tabbedControlGroup1.SelectedTabPageIndex = 1;

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
            DataTable dt = code.smtLookUp("거래처", "매출", "", "");

            repositoryItemSearchLookUpEdit1.DataSource = dt;
            repositoryItemSearchLookUpEdit1.DisplayMember = "거래처명";
            repositoryItemSearchLookUpEdit1.ValueMember = "거래처코드";

            repositoryItemSearchLookUpEdit2.DataSource = dt;
            repositoryItemSearchLookUpEdit2.DisplayMember = "거래처명";
            repositoryItemSearchLookUpEdit2.ValueMember = "거래처코드";

            code.LookupCustomer(WrGlobal.CorpID, "매출", true, "", ref Lup_Cust);
            Lup_Cust.smtLookUpValue = "ALL";
        }

        private void SetGridView()
		{
            SetGridView_SdOrder(gvBM_SdOrderD);
            SetGridView_Sales(gvSales);
        }

        private void SetGridView_SdOrder(GridView gv)
		{
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬

            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            //gv.OptionsView.ShowGroupPanel = false;

            //멀티셀렉트
            gv.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", ItemDiv, "", ""), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "매출", "", ""), "거래처코드", "거래처명");
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

            grid.Create_GvColumn(gv, "출고창고", "HouseCode");
            gv.Columns["HouseCode"].ColumnEdit = lup_WareHouse;

            //grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            //grid.Create_GvColumn(gv, "등록IP", "CreIP");
            //grid.Create_GvColumn(gv, "등록일시", "CreDt");
            //grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            //grid.Create_GvColumn(gv, "수정IP", "ModIP");
            //grid.Create_GvColumn(gv, "수정일시", "ModDt");

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
            grid.Column_AllowEdit(gv.Columns["OutQty"]);
            grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            grid.Column_AllowEdit(gv.Columns["HouseCode"]);

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
			gv.Columns["HouseCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			gv.Columns["ShipToCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			gv.Columns["BillToCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			gv.Columns["OutDate"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

			//
			grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "TotalPrice");

			//칼럼 너비 설정

			//이벤트 설정
			gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			gv.CellValueChanged += Gv_CellValueChanged;
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
            if(fieldName == "UnitPrice")
			{
                float unitPrice = value.ConvertFloat_Null_To_Zero();
                float qty = drChange["Qty"].ConvertFloat_Null_To_Zero();

                drChange["Price"] =  qty * unitPrice;
                drChange["VatPrice"] = (qty * unitPrice) * 0.1;
                drChange["TotalPrice"] = drChange["Price"].ConvertFloat_Null_To_Zero() + drChange["VatPrice"].ConvertFloat_Null_To_Zero();
            }

            gv.BestFitColumns();
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

            if(WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] != "Y")
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
            grid.Set_Navigator(gv);
        }

        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            this.Validate();

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            string custCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();

            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                //수주조회
                BM_SdOrderD sd = new BM_SdOrderD();
                gcSdOrder.DataSource = sd.SelectBM_SdOrderD(custCode, startDate, endDate);

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

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            tabbedControlGroup1.SelectedTabPageIndex = 1;

            Frm_LookUpSales frm = new Frm_LookUpSales("");

            if(frm.ShowDialog() == DialogResult.OK)
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
            string shipToCode, custCode, ItemCode, sdOrderNo, houseCode;
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
                    outQty = Convert.ToInt32(gvBM_SdOrderD.GetRowCellValue(row, "OutQty"));
                    houseCode = gvBM_SdOrderD.GetRowCellValue(row, "HouseCode").ToString();

                    if (custCode == "")
                    {
                        MessageBox.Show("거래처가 없는 건이 있습니다.");
                        return;
                    }
                    if (houseCode == "")
                    {
                        MessageBox.Show("출고창고가 없는 건이 있습니다.");
                        return;
                    }

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

                    if (outQty == 0)
                    {
                        MessageBox.Show("출고수량을 입력해주세요.");
                        return;
                    }
                }
            }

            this.Validate();

            for (int rowCnt = 0; rowCnt < gvBM_SdOrderD.SelectedRowsCount; rowCnt++)
            {
                if (gvBM_SdOrderD.GetSelectedRows()[rowCnt] >= 0)
                {
                    row = gvBM_SdOrderD.GetSelectedRows()[rowCnt];
                    custCode = gvBM_SdOrderD.GetRowCellValue(row, "BillToCode").ToString();
                    shipToCode = gvBM_SdOrderD.GetRowCellValue(row, "ShipToCode").ToString();
                    outDate = (DateTime)gvBM_SdOrderD.GetRowCellValue(row, "OutDate");
                    ItemCode = gvBM_SdOrderD.GetRowCellValue(row, "ItemCode").ToString();
                    outQty = Convert.ToInt32(gvBM_SdOrderD.GetRowCellValue(row, "OutQty"));
                    sdOrderNo = gvBM_SdOrderD.GetRowCellValue(row, "SdOrderNo").ToString();
                    sdOrderSeq = Convert.ToInt32(gvBM_SdOrderD.GetRowCellValue(row, "SdOrderSeq"));
                    houseCode = gvBM_SdOrderD.GetRowCellValue(row, "HouseCode").ConvertString_Null_To_Empty();
                    unitprice = gvBM_SdOrderD.GetRowCellValue(row, "UnitPrice").ConvertDecimal_Null_To_Zero();

                    query = $"INSERT INTO TMP_RequestSales (HostName,CustCode,ShipToCode,OutDate,ItemCode,OutQty, SdOrderNo, SdOrderSeq, HouseCode, UnitPrice)";
                    query = query + $"values (Host_name(),'{custCode}','{shipToCode}','{outDate.ToString("yyyy-MM-dd HH:mm:ss")}','{ItemCode}',{outQty},'{sdOrderNo}', {sdOrderSeq}, '{houseCode}', '{unitprice}')";
                    db.ExecuteQuery(query);
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
            if(tabbedControlGroup1.SelectedTabPageIndex == 0)
			{
                XtraMessageBox.Show("수주 리스트 조회 상태에서 삭제 할 수 없습니다.\n출고 리스트 조회 후 삭제를 진행하세요.");
                return;
			}

            if(XtraMessageBox.Show(cls_MessageBox.DelMsg_Select, cls_MessageBox.DelCaption, MessageBoxButtons.OKCancel) != DialogResult.OK)
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
;

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
	}
}