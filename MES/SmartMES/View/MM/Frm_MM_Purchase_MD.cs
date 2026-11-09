using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SmartMES.Models;
using SmartMES.Common;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models.ST;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using SmartMES.Models.PM;
using SmartMES.View.Placing;
using SmartMES.Models.MM;

namespace SmartMES.View.MM
{
    public partial class Frm_MM_Purchase_MD : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();
        clsPurchase purchase = new clsPurchase();

        public Frm_MM_Purchase_MD()
        {
            InitializeComponent();

            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Cust.smtLookUpDataSource = code.GetCustomerI_Lup(WrGlobal.CorpID, true);
            Lup_Cust.smtLookUpDataDisplayMember = "거래처명";
            Lup_Cust.smtLookUpDataValueMember = "거래처코드";
            Lup_Cust.smtLookUpValue = "ALL";

            SetGvMaster(gvMM_PurchaseM);
            SetGvDetail(gvDetail);
            DisplayData();

            //바코드스캔 이벤트
            Text_Barcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_Barcode_KeyPress);
        }

        #region GridView Setting

        private void SetGvMaster(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsView.ShowGroupPanel = false;
            gv.IndicatorWidth = 50;                     //순번 너비

            //창고
            RepositoryItemSearchLookUpEdit lup_House = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            grid.Create_GvColumn(gv, "입고번호", "StoNo");
            grid.Create_GvColumn(gv, "거래처", "CustName");
            grid.Create_GvColumn(gv, "입고일", "StoDate");
            grid.Create_GvColumn(gv, "창고", "HouseID");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "비고", "Remark");
            
            //Repository
            if (gv.Columns.ColumnByFieldName("HouseID") != null) gv.Columns["HouseID"].ColumnEdit = lup_House;

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["StoDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }


        private void SetGvDetail(GridView gv)
		{
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsView.ShowGroupPanel = false;

            gv.IndicatorWidth = 50;                     //순번 너비

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "입고번호", "StoNo");
            grid.Create_GvColumn(gv, "순번", "StoSeq");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "입고량", "Qty");
            grid.Create_GvColumn(gv, "불량", "BadQty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "공급가", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "합계", "TotalPrice");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "상태", "State");
            grid.Create_GvColumn(gv, "발주번호", "MmOrderNo");
            grid.Create_GvColumn(gv, "발주순번", "MmOrderSeq");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //
            gv.Columns["StoNo"].Visible = false;

            if(!Database.DbName.ToUpper().Contains("TPG"))
			{
                gv.Columns["MmOrderNo"].Visible = false;
                gv.Columns["MmOrderSeq"].Visible = false;
			}

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BadQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        #endregion

        #region 조회

        private void DisplayData()
        {
            this.Validate();
            gvMM_PurchaseM.FocusInvalidRow();

            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            MM_PurchaseM MM_PurchaseM = new MM_PurchaseM();
            MM_PurchaseM.CustCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();
            MM_PurchaseM.Type = "1";

            gcMM_PurchaseM.DataSource = MM_PurchaseM.Select_MM_PurchaseM(startDate, endDate);
            gvMM_PurchaseM.BestFitColumns();
        }

        private void gvMM_PurchaseM_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayDetailList();
            gvDetail.BestFitColumns();
        }

        private void DisplayDetailList()
        {
            MM_PurchaseD pd = new MM_PurchaseD();
            pd.StoNo = (gvMM_PurchaseM.GetFocusedRowCellValue("StoNo") ?? "").ToString();

            gcDetail.DataSource = pd.Select_MM_PurchaseD_StoNo();
            gvDetail.BestFitColumns();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
            gvMM_PurchaseM.BestFitColumns();
        }

        #endregion

        //등록
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            //if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            //{
                frmMM_PurchaseD_TypeA form = new frmMM_PurchaseD_TypeA("Insert", "");
                form.ShowDialog();
            //}
            //else
            //{
            //    frmMM_PurchaseD form = new frmMM_PurchaseD("Insert", "");
            //    form.ShowDialog();
            //}

            DisplayData();
        }

        //수정
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (gvMM_PurchaseM.FocusedRowHandle < 0) return;

            //if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            //{
                frmMM_PurchaseD_TypeA form = new frmMM_PurchaseD_TypeA("Update", gvMM_PurchaseM.GetFocusedRowCellValue("StoNo").ToString());
                form.ShowDialog();
            //}
            //else
            //{
            //품목 룩업 있는 창
                //frmMM_PurchaseD form = new frmMM_PurchaseD("Update", gvMM_PurchaseM.GetFocusedRowCellValue("StoNo").ToString());
                //form.ShowDialog();
            //}
            DisplayData();
        }

        //삭제
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            // 선택한 항목이 없으면 리턴
            if (gvMM_PurchaseM.GetSelectedRows().Length == 0) return;

            //자료를 삭제하는 부분
            if (MessageBox.Show("선택한 자료를 삭제 하시겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            try
            {
                string stoNo = "";

                foreach (int idx in gvMM_PurchaseM.GetSelectedRows())
                {
                    stoNo = gvMM_PurchaseM.GetRowCellValue(idx, "StoNo").ToString();

                    if (stoNo.Length > 0)
                    {
                        purchase.UpdatePlacingInQtyByPurchase(stoNo);   //발주정보 업데이트
                        purchase.DeleteMM_PurchaseM(gvMM_PurchaseM.GetFocusedRowCellValue("CorpId").ToString(), stoNo);

                        //재고 삭제(입고내역 기준으로 출고내역 있는 경우 삭제 예외처리 추가 필요, 현재는 입고내역으로 출고처리하는 것이 없음)
                        ST_StockD std = new ST_StockD();
                        std.JobNo = stoNo;

                        std.DeleteStockMD_JobNo();

                    }//end if
                }

                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Event

        private void Text_Barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 바코드 조회
            if (e.KeyChar != (char)Keys.Enter) return;

            GridView gv = gcMM_PurchaseM.DefaultView as GridView;
            if (gv == null) return;

            string MmOrderNo = Text_Barcode.Text.Trim();

            try
            {
                //존재유무 확인
                MM_PlacingM pm = new MM_PlacingM();

                if (!pm.ExistsMM_PlacingM(WrGlobal.CorpID, MmOrderNo))
                {
                    //존재하지 않으면 리턴
                    MessageBox.Show(" '" + MmOrderNo + "'는 발주내역이 없는 바코드번호입니다");
                    Text_Barcode.Text = "";
                    return;
                }

                DataTable masterData = gcMM_PurchaseM.DataSource as DataTable;
                if (masterData?.Select($"MmOrderNo = '{MmOrderNo}'").Length > 0)
                {
                    MessageBox.Show("이미 스캔한 발주입니다");
                    Text_Barcode.Text = "";
                    return;
                }

                frmMM_PurchaseD_BarCode form = new frmMM_PurchaseD_BarCode("BarCode", MmOrderNo);
                form.ShowDialog();

                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //엑셀
        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvMM_PurchaseM.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}