using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using SmartPOP.Models;
using SmartPOP.Common;
using SMT_MES.Controls;
using SmartPOP.View.Lookup;
using System.Drawing;
using DevExpress.XtraEditors;
using SmartPOP.Models.Sales;

namespace SmartPOP.Views.Sales
{
    public partial class frmOutRequest_SdOrder : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        Database db = new Database();
        string query = string.Empty;
        string ItemDiv = "20";

        public frmOutRequest_SdOrder()
        {
            InitializeComponent();
        }

        
        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();

            DisplayData();
        }

        private void SetLup()
		{
            DateTime start = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime end = Convert.ToDateTime(start.AddDays(40).ToString("yyyy-MM-01"));

            DtOrder.smtStartDateTimeValue = start;
            DtOrder.smtEndDateTimeValue = end;

            //조회조건 거래처
            LupCust.smtLookUpDataSource = code.GetCustomer(WrGlobal.CorpID, true).ToTable();
            LupCust.smtLookUpDataDisplayMember = "내용";
            LupCust.smtLookUpDataValueMember = "코드";
            LupCust.smtLookUpValue = "All";

            //SearchLookUp font size
            //LupCust.Properties.View.Appearance.Row.Font = new Font("맑은 고딕", 18, FontStyle.Regular);         //본문row
            //LupCust.Properties.View.Appearance.HeaderPanel.Font = new Font("맑은 고딕", 18, FontStyle.Regular); //헤더
        }

        private void SetGridView()
		{
            SetGridView_SdOrder(gvSdOrderDetail);
            SetGridView_Sales(gvSales);
        }
        private void SetGridView_SdOrder(GridView gv)
        {
            //그리드 납품처(매출 거래처)
            DataTable dt = code.GetCustomerList(new string[] { "20" }, false);

            repositoryItemSearchLookUpEdit1.DataSource = dt;
            repositoryItemSearchLookUpEdit1.DisplayMember = "거래처명";
            repositoryItemSearchLookUpEdit1.ValueMember = "거래처코드";

            repositoryItemSearchLookUpEdit2.DataSource = dt;
            repositoryItemSearchLookUpEdit2.DisplayMember = "거래처명";
            repositoryItemSearchLookUpEdit2.ValueMember = "거래처코드";

            grid.Set_Navigator(gv);

            //셀 내부 서치룩업 크기 조정
            grid.SetPOPGrid(gv);
			grid.SetPOPCellLup(repositoryItemSearchLookUpEdit1);
			grid.SetPOPCellLup(repositoryItemSearchLookUpEdit2);
		}

        private void SetGridView_Sales(GridView gv)
		{
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            //gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;

            //
            gv.OptionsBehavior.ReadOnly = true;

			RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
			RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", ItemDiv, "", ""), "품목코드", "품명");
			RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "출하번호", "SalesNo");
            grid.Create_GvColumn(gv, "출하순번", "SalesSeq");
            grid.Create_GvColumn(gv, "출하일자", "SalesDate");
            //grid.Create_GvColumn(gv, "거래처", "BillToCode", lup_SoldToCode);
            grid.Create_GvColumn(gv, "거래처코드", "BillToCode");
            grid.Create_GvColumn(gv, "거래처명", "BillToName");
            grid.Create_GvColumn(gv, "납품처코드", "ShipToCode");
            grid.Create_GvColumn(gv, "납품처명", "ShipToName");
            grid.Create_GvColumn(gv, "품목코드", "ProductCode");
            grid.Create_GvColumn(gv, "품명"    , "ProductName");
            //grid.Create_GvColumn(gv, "품목", "ProductCode", lup_Item);
            grid.Create_GvColumn(gv, "단위", "UnitCode", lup_UnitCode);
            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "창고", "HouseCode");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀 내부 서치룩업 크기 조정
            grid.SetPOPGrid(gv);
            grid.SetPOPCellLup(lup_UnitCode);
            grid.SetPOPCellLup(lup_SoldToCode);
            grid.SetPOPCellLup(lup_Emp_No);
            grid.SetPOPCellLup(lup_Item);

            //Repository 생성 or 이벤트 입력
            //gv.Columns["SalesNo"].Visible = false;
            //gv.Columns["SalesSeq"].Visible = false;
            gv.Columns["Status"].Visible = false;
            gv.Columns["HouseCode"].Visible = false;
            //Column 단일 설정

            //셀값 변경 허용

            if(WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] != "Y")
			{
                gv.Columns["ShipToCode"].Visible = false;
                gv.Columns["ShipToName"].Visible = false;

            }

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;     //상태
            grid.SetCol_Type(gv.Columns["Qty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "TotalPrice");

            //칼럼 너비 설정
            gv.Columns["Status"].MaxWidth = 60;

            //이벤트 설정
            grid.SetPOPGrid(gv);
           // gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			grid.Set_Navigator(gv);
		}

        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            string startDate = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char10();
            //string custCode = gvCustomer.FocusedRowHandle < 0 ? string.Empty : gvCustomer.GetFocusedRowCellValue("CustCode").ToString();
            string custCode = LupCust.smtLookUpValue;

            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                //수주조회
                SdOrderDetail sd = new SdOrderDetail();
                gcSdOrder.DataSource = sd.SelectSdOrderDetail(custCode, startDate, endDate);

				foreach (DevExpress.XtraGrid.Columns.GridColumn column in gvSdOrderDetail.Columns)
				{
					column.OptionsColumn.AllowEdit = false;
				}

				//// 납품처, 출고수량, 출고일 칼럼만 편집 가능하도록 설정
				gvSdOrderDetail.Columns["ShipToCode"].OptionsColumn.AllowEdit = true;
				gvSdOrderDetail.Columns["BillToCode"].OptionsColumn.AllowEdit = true;
				gvSdOrderDetail.Columns["OutQty"].OptionsColumn.AllowEdit = true;
				gvSdOrderDetail.Columns["UnitPrice"].OptionsColumn.AllowEdit = true;
				gvSdOrderDetail.Columns["OutDate"].OptionsColumn.AllowEdit = true;

                gvSdOrderDetail.BestFitColumns();
			}
            else
            {
                SalesDetail sd = new SalesDetail();
                sd.CustCode = custCode;
                gcSales.DataSource = sd.Select_SalesDetail(startDate, endDate);

                gvSales.BestFitColumns();
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            tabbedControlGroup1.SelectedTabPageIndex = 1;

            frmLookUpSales frm = new frmLookUpSales("");
            frm.ShowDialog();

            DisplayData();
        }

        private void smtUpdate_Click(object sender, EventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex == 0) return;

            DataRow dr = gvSales.GetFocusedDataRow();
            if (dr == null) return;

            string salesNo = dr["SalesNo"].ConvertString_Null_To_Empty();

            frmLookUpSales frm = new frmLookUpSales(salesNo);
            frm.ShowDialog();

            DisplayData();
        }

        // 출고요청 버튼 클릭
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex != 0) return;
            if (gvSdOrderDetail.SelectedRowsCount <= 0) return;

            //임시테이블 데이터 지우기
            query = "Delete TempRequestSales Where HostName = Host_name()";
            db.ExecuteQuery(query);

            int row, outQty, sdOrderSeq;
            string shipToCode, custCode, materialCode, sdOrderNo;
            DateTime outDate;

            //납품처 및 출고수량 체크
            for (int rowCnt = 0; rowCnt < gvSdOrderDetail.SelectedRowsCount; rowCnt++)
            {
                if (gvSdOrderDetail.GetSelectedRows()[rowCnt] >= 0)
                {
                    row = gvSdOrderDetail.GetSelectedRows()[rowCnt];
                    shipToCode = gvSdOrderDetail.GetRowCellValue(row, "ShipToCode").ToString();
                    custCode = gvSdOrderDetail.GetRowCellValue(row, "BillToCode").ToString();
                    outQty = Convert.ToInt32(gvSdOrderDetail.GetRowCellValue(row, "OutQty"));

                    if (custCode == "")
                    {
                        MessageBox.Show("거래처가 없는 건이 있습니다.");
                        return;
                    }
                    
                    //납품처 사용 옵션 -> 납품처 사용 -> 납품처 미입력 메시지
                    if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] == "Y" && shipToCode == "")
                    {
                        MessageBox.Show("납품처가 없는 건이 있습니다.");
                        return;
                    }
					else
					{
                        gvSdOrderDetail.SetRowCellValue(gvSdOrderDetail.GetSelectedRows()[rowCnt], "ShipToCode", custCode); //납품처 재고출고에서 사용하여 없을경우 거래처로 넣음
					}

                    if (outQty == 0)
                    {
                        MessageBox.Show("출고수량을 입력해주세요.");
                        return;
                    }
                }
            }

            this.Validate();

            for (int rowCnt = 0; rowCnt < gvSdOrderDetail.SelectedRowsCount; rowCnt++)
            {
                if (gvSdOrderDetail.GetSelectedRows()[rowCnt] >= 0)
                {
                    row = gvSdOrderDetail.GetSelectedRows()[rowCnt];
                    custCode = gvSdOrderDetail.GetRowCellValue(row, "BillToCode").ToString();
                    shipToCode = gvSdOrderDetail.GetRowCellValue(row, "ShipToCode").ToString();
                    outDate = (DateTime)gvSdOrderDetail.GetRowCellValue(row, "OutDate");
                    materialCode = gvSdOrderDetail.GetRowCellValue(row, "MaterialCode").ToString();
                    outQty = Convert.ToInt32(gvSdOrderDetail.GetRowCellValue(row, "OutQty"));
                    sdOrderNo = gvSdOrderDetail.GetRowCellValue(row, "SdOrderNo").ToString();
                    sdOrderSeq = Convert.ToInt32(gvSdOrderDetail.GetRowCellValue(row, "SdOrderSeq"));

                    query = $"INSERT INTO TempRequestSales (HostName,CustCode,ShipToCode,OutDate,MaterialCode,OutQty, SdOrderNo, SdOrderSeq)";
                    query = query + $"values (Host_name(),'{custCode}','{shipToCode}','{outDate.ToString("yyyy-MM-dd HH:mm:ss")}','{materialCode}',{outQty},'{sdOrderNo}', {sdOrderSeq})";
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
                    gvSdOrderDetail.ExportToXls(sfd.FileName);
                else
                    gvSales.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        private void tabbedControlGroup1_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            DisplayData();
        }

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}