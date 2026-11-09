using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

using SmartCommon.Common;

using SmartDesign.View.Dashboard;

using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.ST;
using SmartMES.View.Lookup;

using SMT_MES.View.Business.Rpt;

namespace SmartMES.View.BM
{
	public partial class Frm_BM_SampleOut : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        Database db = new Database();
        string query = string.Empty;
        string ItemDiv = "20";
        private string[] CustDiv = { "20", "40" }; //거래처구분 리스트(10 : 매입, 20 : 매출, 40 :매입/매출)

        public Frm_BM_SampleOut()
        {
            InitializeComponent();
            SetGridView();
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정
            tabbedControlGroup1.SelectedTabPageIndex = 1;

            smtUpdate.Visible = false;//LOT별 출고 시 수정하면 LOT번호가 사라져서 안보이게 처리
            cmdPrint.Visible = false;
            cmdAccPrint.Visible = false;
            cmdSave.Visible = false;

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

            code.LookupCustomer(WrGlobal.CorpID, "매출", true, "", ref Lup_Cust);
            Lup_Cust.smtLookUpValue = "ALL";
        }

        #region 디자인 
        private void SetGridView()
		{
            SetGridView_SampleOutM(gvSampleOutM);
            SetGridView_SampleOutD(gvSampleOutD);
        }

        private void SetGridView_SampleOutM(GridView gv)
		{
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

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

            //Column 생성
            grid.Create_GvColumn(gv, "출고번호", "OutNo");
            grid.Create_GvColumn(gv, "출고순번", "OutSeq");
            grid.Create_GvColumn(gv, "출고일자", "OutDate");
            grid.Create_GvColumn(gv, "납품처", "ShipToCode");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "포장단위", "UnitCode", lup_UnitCode);
            grid.Create_GvColumn(gv, "수량", "Qty", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "제조번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "유효기간", "ExpirationDate");
            grid.Create_GvColumn(gv, "바코드", "BarCodeNo");
            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository
            gv.Columns["ShipToCode"].ColumnEdit = lup_SoldToCode;
            gv.Columns["ItemName"].ColumnEdit = lup_Item;


            grid.Column_AllowEdit(gv.Columns["OutDate"]);
            grid.Column_AllowEdit(gv.Columns["ShipToCode"]);

			//컬럼 위치 설정
	        gv.Columns["Qty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
	        gv.Columns["ShipToCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
	        gv.Columns["OutDate"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["ExpirationDate"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;


            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			gv.CellValueChanged += GvSampleOutM_CellValueChanged;
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;

            grid.Set_Navigator(gvSampleOutM);
        }

        private void SetGridView_SampleOutD(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "개별바코드", "BarCodeNo");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellValueChanged += GvSampleOutD_CellValueChanged;
            grid.Set_Navigator(gvSampleOutM);
        }
        #endregion 디자인 END

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DisplaySampleOutD();
        }

        private void GvSampleOutM_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            DataRow drChange = gv.GetFocusedDataRow();
            if (drChange == null) return;

            string value = e.Value.ConvertString_Null_To_Empty();
            string fieldName = e.Column.FieldName;

      
            gv.BestFitColumns();
		}

        private void GvSampleOutD_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;

            //상태값 수정
            grid.Change_Status(gv, e.RowHandle);
        }


        #region 조회
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
                //샘플링 출고 조회
                BM_SampleOutM sd = new BM_SampleOutM();
                gcSampleOutM.DataSource = sd.Select_BM_SampleOutM(startDate, endDate);

                gvSampleOutM.BestFitColumns();
            }
        }

        private void DisplaySampleOutD()
        {
            //재고조회
            GridView gv = gcSampleOutM.MainView as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            BM_SampleOutM sm = new BM_SampleOutM();
            sm.OutNo = dr["OutNo"].ConvertString_Null_To_Empty();

            gcSampleOutD.DataSource = sm.Select_BM_SampleOutD(sm.OutNo);
            gvSampleOutD.BestFitColumns();
        }

        #endregion 조회 END

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            tabbedControlGroup1.SelectedTabPageIndex = 0;

            Frm_LookUpSampleOut frm = new Frm_LookUpSampleOut("");

            if(frm.ShowDialog() == DialogResult.OK)
			{
                XtraMessageBox.Show(cls_MessageBox.Save_Comp);
            }

            tabbedControlGroup1.SelectedTabPageIndex = 0;
            DisplayData();
        }

        private void smtUpdate_Click(object sender, EventArgs e)
        {
            if (tabbedControlGroup1.SelectedTabPageIndex == 0) return;

            DataRow dr = gvSampleOutM.GetFocusedDataRow();
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
            if (gvSampleOutM.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("선택한 내역이 없습니다.\n수주 건 선택 후 저장 하세요.");
                return;
            }

            gvSampleOutM.PostEditor();
            gvSampleOutM.UpdateCurrentRow();
            //임시테이블 데이터 지우기
            query = "Delete TMP_RequestSales Where HostName = Host_name()";
            db.ExecuteQuery(query);

            int row, outQty, sdOrderSeq;
            string shipToCode, custCode, ItemCode, sdOrderNo, houseCode, lotNo;
            decimal unitprice = 0;
            DateTime outDate;

            //납품처 및 출고수량 체크
            for (int rowCnt = 0; rowCnt < gvSampleOutM.SelectedRowsCount; rowCnt++)
            {
                if (gvSampleOutM.GetSelectedRows()[rowCnt] >= 0)
                {
                    row = gvSampleOutM.GetSelectedRows()[rowCnt];
                    shipToCode = gvSampleOutM.GetRowCellValue(row, "ShipToCode").ToString();
                    custCode = gvSampleOutM.GetRowCellValue(row, "BillToCode").ToString();
                    outQty = gvSampleOutM.GetRowCellValue(row, "OutQty").ConvertInt_Null_To_Zero();
                    houseCode = gvSampleOutM.GetRowCellValue(row, "HouseCode").ToString();

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
                        gvSampleOutM.SetRowCellValue(gvSampleOutM.GetSelectedRows()[rowCnt], "ShipToCode", custCode); //납품처 재고출고에서 사용하여 없을경우 거래처로 넣음
					}
                }
            }

            this.Validate();

            DataTable dt = null;

            gvSampleOutD.PostEditor();
            gvSampleOutD.UpdateCurrentRow();
            dt = gcSampleOutD.DataSource as DataTable;

            int sdRow = gvSampleOutM.FocusedRowHandle;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i]["Status"]) == "수정")
                {
                    outQty = Convert.ToInt32(dt.Rows[i]["OutQty"] ?? 0);
                    if (outQty > 0)
                    {
                        custCode = gvSampleOutM.GetRowCellValue(sdRow, "BillToCode").ToString();
                        shipToCode = gvSampleOutM.GetRowCellValue(sdRow, "ShipToCode").ToString();
                        outDate = (DateTime)gvSampleOutM.GetRowCellValue(sdRow, "OutDate");
                        ItemCode = gvSampleOutM.GetRowCellValue(sdRow, "ItemCode").ToString();
                        sdOrderNo = gvSampleOutM.GetRowCellValue(sdRow, "SdOrderNo").ToString();
                        sdOrderSeq = Convert.ToInt32(gvSampleOutM.GetRowCellValue(sdRow, "SdOrderSeq"));
                        unitprice = gvSampleOutM.GetRowCellValue(sdRow, "UnitPrice").ConvertDecimal_Null_To_Zero();

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

        // 출고 삭제
        private void CmdCancel_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(cls_MessageBox.DelMsg_Select, cls_MessageBox.DelCaption, MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            int row = gvSampleOutM.FocusedRowHandle;

            BM_SampleOutM sm = new BM_SampleOutM();
            sm.OutNo = gvSampleOutM.GetRowCellValue(row, "OutNo").ToString();

            sm.Delete_BM_SampleOutD();
            sm.Delete_BM_SampleOutM();

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
                    gvSampleOutM.ExportToXls(sfd.FileName);
                else
                    gcSampleOutM.ExportToXls(sfd.FileName);

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

            GridControl gc = gcSampleOutM;

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

            GridControl gc = gcSampleOutM;

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