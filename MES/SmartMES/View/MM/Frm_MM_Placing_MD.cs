using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SmartCommon.Common;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Reflection;
using System.Linq;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTab;
using SmartMES.Models.MM;

namespace SmartMES.View.MM
{
    public partial class Frm_MM_Placing_MD : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsCode code = new clsCode();
        clsPurchase purchase = new clsPurchase();
        clsRepository rpo = new clsRepository();
        clsGrid grid = new clsGrid();
        string query = "";
        string placingType = "";
        private GridControl planGrid;
        private GridView planView;
        private LabelControl planNotice;
        private XtraTabControl purchaseTabs;
        private XtraTabPage planPage;

        public Frm_MM_Placing_MD()
        {
            InitializeComponent();
        }

        public Frm_MM_Placing_MD(string title)
        {
            InitializeComponent();
            this.Text = title;
            this.placingType = title;
        }

        private void Frm_MM_Placing_MD_Load(object sender, EventArgs e)
        {
            //기초정보 세팅
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            customerBox.smtLookUpDataSource = code.GetCustomerI_Lup(WrGlobal.CorpID, true);
            customerBox.smtLookUpDataDisplayMember = "거래처명";
            customerBox.smtLookUpDataValueMember = "거래처코드";
            customerBox.smtLookUpValue = "ALL";

            //프로세스 옵션 : 발주서 출력, 옵션명 없을 경우 버튼 숨김처리
            if(WrGlobal.ProcOption["MT"]["PlacingOrder_Report"].Trim() == "")
			{
                cmdPO.Visible = false;
			}

            Set_GvMaster(gvPlacingMaster);
            Set_GvDetail(gvDetail);

            if (MM_PlacingD.UsePlanningFields)
                InitializePlanningTab();

            DisplayData();
        }

        /// <summary>발주계획을 기존 발주등록 목록과 같은 화면에서 확인할 수 있게 합니다.</summary>
        private void InitializePlanningTab()
        {
            purchaseTabs = new XtraTabControl { Dock = DockStyle.Fill, Name = "purchaseTabs" };
            var orderPage = new XtraTabPage { Text = "발주등록", Name = "purchaseOrderPage" };
            planPage = new XtraTabPage { Text = "발주계획", Name = "purchasePlanPage" };

            planGrid = new GridControl { Dock = DockStyle.Fill, Name = "purchasePlanGrid" };
            planView = new GridView(planGrid) { Name = "purchasePlanView" };
            planGrid.MainView = planView;
            planView.OptionsBehavior.Editable = false;
            planView.OptionsBehavior.AutoPopulateColumns = false;
            planView.OptionsView.ShowGroupPanel = false;
            planView.OptionsView.ShowAutoFilterRow = true;
            planView.OptionsView.ShowFooter = true;
            planView.OptionsView.ColumnAutoWidth = false;
            planView.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            planView.RowHeight = 28;
            grid.Set_Navigator(planView);

            planNotice = new LabelControl
            {
                Dock = DockStyle.Top,
                AutoSizeMode = LabelAutoSizeMode.None,
                Height = 38,
                Padding = new Padding(10, 0, 0, 0)
            };
            planPage.Controls.Add(planGrid);
            planPage.Controls.Add(planNotice);

            int layoutIndex = Controls.GetChildIndex(layoutControl1);
            orderPage.Controls.Add(layoutControl1);
            purchaseTabs.TabPages.AddRange(new[] { orderPage, planPage });
            purchaseTabs.SelectedTabPage = orderPage;
            Controls.Add(purchaseTabs);
            Controls.SetChildIndex(purchaseTabs, layoutIndex);

            var orderButtons = new[] { smtSearchButton4, smtSearchButton3, smtSearchButton1,
                smtSearchButton2, cmdExcel, cmdPO };
            var originalStates = orderButtons.ToDictionary(button => button, button => button.Enabled);
            purchaseTabs.SelectedPageChanged += (s, e) =>
            {
                bool showingPlan = purchaseTabs.SelectedTabPage == planPage;
                foreach (var button in orderButtons)
                    button.Enabled = !showingPlan && originalStates[button];
                if (showingPlan && planGrid.DataSource != null)
                    planView.BestFitColumns();
            };
        }

        /// <summary>기간에 맞는 원본 발주계획을 조회하고 실제 발주가 없을 때 계획 탭을 표시합니다.</summary>
        private void DisplayImportedPlan(DateTime startDate, DateTime endDate, string customerCode)
        {
            if (planGrid == null)
                return;
            try
            {
                DataTable rows = new MM_PlacingD().SelectImportedPurchasePlan(startDate, endDate, customerCode);
                if (planView.Columns.Count == 0)
                    SetPlanningColumns(rows);
                var previous = planGrid.DataSource as DataTable;
                planGrid.DataSource = rows;
                previous?.Dispose();

                if (rows.Rows.Count > 0 && gvPlacingMaster.DataRowCount == 0)
                    purchaseTabs.SelectedTabPage = planPage;
                if (planGrid.Visible)
                    planView.BestFitColumns();
            }
            catch (Exception ex)
            {
                var previous = planGrid.DataSource as DataTable;
                planGrid.DataSource = null;
                previous?.Dispose();
                XtraMessageBox.Show(this, ex.Message, "자재발주 계획 조회");
            }
        }

        private void SetPlanningColumns(DataTable rows)
        {
            string[,] columns =
            {
                { "재고 기준일", "BasisDate" }, { "품목코드", "ItemCode" }, { "품명", "SourceItemName" },
                { "거래처 코드", "SupplierSourceCode" }, { "구매처", "SupplierSourceName" }, { "단위", "SourceUnit" },
                { "단가", "UnitPrice" }, { "현재고량", "CurrentStockQty" }, { "재고회전 (개월)", "StockCoverageMonths" },
                { "구매량", "PurchaseQty" }, { "구매금액", "PurchaseAmount" }, { "월사용량", "MonthlyUsageQty" },
                { "일평균 (월 22일)", "DailyUsageQty" }, { "납품 예정일", "DeliveryDate" },
                { "납품일 기준 잔여재고량", "DeliveryRemainingQty" }, { "리드타임 (일)", "LeadTimeDays" },
                { "납품일 기준 재고회전 (개월)", "DeliveryCoverageMonths" },
                { "용도", "UsagePurpose" }, { "사용부서", "UsageDepartment" }, { "특이사항", "Remark" },
                { "MOQ", "MinimumOrderQty" }, { "1P 기준", "PalletBasis" }, { "구매처 상태", "SupplierStatus" },
                { "원본 추가 수량", "SourceExtraQty" }, { "원본 행", "SourceRow" }, { "원본 수식 오류", "SourceIssues" }
            };
            for (int index = 0; index < columns.GetLength(0); index++)
            {
                string field = columns[index, 1];
                grid.Create_GvColumn(planView, columns[index, 0], field);
                var column = planView.Columns[field];
                if (rows.Columns[field].DataType == typeof(decimal))
                {
                    column.DisplayFormat.FormatType = FormatType.Numeric;
                    column.DisplayFormat.FormatString = "#,##0.####";
                }
                else if (rows.Columns[field].DataType == typeof(DateTime))
                {
                    column.DisplayFormat.FormatType = FormatType.DateTime;
                    column.DisplayFormat.FormatString = "yyyy-MM-dd";
                }
            }
            planView.Columns["BasisDate"].Fixed = FixedStyle.Left;
            planView.Columns["ItemCode"].Fixed = FixedStyle.Left;
            planView.Columns["SourceItemName"].Fixed = FixedStyle.Left;
            planView.Columns["PurchaseAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PurchaseAmount", "{0:#,##0.####}");
        }

        private void Set_GvMaster(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "발주번호", "MmOrderNo");
            grid.Create_GvColumn(gv, "거래처", "CustName");
            grid.Create_GvColumn(gv, "발주일자", "PlacingDate");
            grid.Create_GvColumn(gv, "납기일자", "DeliveryDate");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //Column 단일 설정

            //셀값 변경 허용

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        private void Set_GvDetail(GridView gv)
        {
            gv.Columns.Clear();

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
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", "", "", ""), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");
            //Column 생성

            grid.Create_GvColumn(gv, "순번", "MmOrderSeq");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "납기일자", "DeliveryDate");
            grid.Create_GvColumn(gv, "발주량", "Qty");
            grid.Create_GvColumn(gv, "입고량", "InQty");
            grid.Create_GvColumn(gv, "미입고수량", "BalanceQty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "비고", "Remark");

            Frm_MM_PlacingD.AddPlanningColumns(gv, grid);

            //grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            //grid.Create_GvColumn(gv, "등록IP", "CreIP");
            //grid.Create_GvColumn(gv, "등록일시", "CreDt");
            //grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            //grid.Create_GvColumn(gv, "수정IP", "ModIP");
            //grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //Column 단일 설정

            //셀값 변경 허용

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["DeliveryDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BalanceQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        private void DisplayData()
        {
            gvPlacingMaster.FocusInvalidRow();
			//this.Validate();

			string typeCode = "";
			switch (placingType)
			{
				case "원자재발주":
				case "자재발주":
					typeCode = "10";
					break;
				case "부자재발주":
					typeCode = "20";
					break;
				case "소모품발주":
					typeCode = "30";
					break;
				case "상품발주":
					typeCode = "50";
					break;
			}

			string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
			string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");
			string custCode = customerBox.smtLookUpValue;

			MM_PlacingM pm = new MM_PlacingM();
			pm.CustCode = custCode;
			pm.PlacingType = typeCode;

			gcPlacingMaster.DataSource = pm.Select_MM_PlacingM(startDate, endDate);
			gvPlacingMaster.BestFitColumns();
            DisplayImportedPlan(Dt_Date.smtStartDateTimeValue, Dt_Date.smtEndDateTimeValue, custCode);
		}

        private void gvPlacingMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayDetailList();
        }

        private void DisplayDetailList()
        {
            string mmOrderNo = (gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo") ?? "").ToString();

            MM_PlacingD pd = new MM_PlacingD();
            pd.MmOrderNo = mmOrderNo;

            gcDetail.DataSource = pd.Select_MM_PlacingD_MmOrderNo();
            gvDetail.BestFitColumns();
        }

        //조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        //등록
        private void button6_Click(object sender, EventArgs e)
        {
            //if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            //{
                Frm_MM_PlacingD form = new Frm_MM_PlacingD("Insert", "", placingType);
                form.ShowDialog();
            //}
            //else
            //{ 
                //frmMM_PlacingD form = new frmMM_PlacingD("Insert", "", placingType);
                //form.ShowDialog();
            //}
            
            DisplayData();
        }

        //수정
        private void button5_Click(object sender, EventArgs e)
        {
            if (gvPlacingMaster.FocusedRowHandle < 0) return;

            string state = purchase.GetPlacingState(gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());

            try
            {
                if (state != "완료")
                {
                    //if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
                    //{
                    Frm_MM_PlacingD form = new Frm_MM_PlacingD("Update", gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());
                        form.ShowDialog();
                    //}
                    //else
                    //{ 
                        //frmMM_PlacingD form = new frmMM_PlacingD("Update", gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());
                        //form.ShowDialog();
                    //}
                    DisplayData();
                }
                else
                {
                    MessageBox.Show("완료 상태의 건은 수정이 불가능합니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //삭제
        private void button4_Click(object sender, EventArgs e)
        {
            if (gvPlacingMaster.FocusedRowHandle < 0) return;

            string state = purchase.GetPlacingState(gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());
            if (state == "완료")
            {
                MessageBox.Show("완료 처리된 건은 삭제할 수 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //자료를 삭제하는 부분
            if (MessageBox.Show("선택한 항목를 삭제 하겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                MessageBox.Show("삭제되었습니다.");
                return;
            }//end if

            try
            {
                string sdOrderNo = gvPlacingMaster.GetRowCellValue(gvPlacingMaster.FocusedRowHandle, "MmOrderNo").ToString();

                if (sdOrderNo.Length > 0)
                {
                    purchase.DeleteMM_PlacingM(gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());

                    DisplayData();
                }//end if
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //완료처리
        private void Btn_Row_Ins_Click(object sender, EventArgs e)
        {
            if (gvPlacingMaster.FocusedRowHandle < 0) return;

            string state = gvPlacingMaster.GetFocusedRowCellValue("State").ToString();

            if (state == "완료")
            {
                MessageBox.Show("완료 처리된 항목입니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("선택한 항목을 완료처리 하겠습나까?", "완료처리", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }//end if

            try
            {
                string sdOrderNo = gvPlacingMaster.GetRowCellValue(gvPlacingMaster.FocusedRowHandle, "MmOrderNo").ToString();

                if (sdOrderNo.Length > 0)
                {
                    purchase.EndMM_PlacingM(gvPlacingMaster.GetFocusedRowCellValue("MmOrderNo").ToString());
                    DisplayData();
                }//end if
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //엑셀변환
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvPlacingMaster.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }
        }

		private void cmdPO_Click(object sender, EventArgs e)
		{
            DataRow drMaster = gvPlacingMaster.GetFocusedDataRow();
            if (drMaster == null) return;

            string rptNm = WrGlobal.ProcOption["MT"]["PlacingOrder_Report"];
            try
            {
                if(rptNm == "") throw new Exception($"레포트를 찾을 수 없습니다.");

                // 현재 실행중인 Assembly 가져오기
                Assembly assembly = Assembly.GetExecutingAssembly();

                // reportName 과 일치하는 타입 찾기
                Type type = assembly.GetTypes()
                    .FirstOrDefault(t => t.Name.Equals(rptNm, StringComparison.OrdinalIgnoreCase)
                                      && typeof(XtraReport).IsAssignableFrom(t));

                if (type == null)
                    throw new Exception($"레포트를 찾을 수 없습니다.");

                // 인스턴스 생성
                XtraReport report = (XtraReport)Activator.CreateInstance(type, drMaster); //파라미터가 다를 경우 분기 필요

                using (ReportPrintTool reportTool = new ReportPrintTool(report))
                {
                    reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
                    reportTool.ShowPreviewDialog();
                }
            }
            catch
            {
                XtraMessageBox.Show("레포트를 찾을 수 없습니다.");
            }
        }
        
        //닫기
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
