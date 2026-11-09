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
using SmartPOP.Models;
using SmartPOP.Common;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using SMT_MES.Controls;
using SmartPOP.Models.ST;
using SmartPOP.Models.MM;
using SmartMES.View.MM;

namespace SmartPOP.Views.MM
{
    public partial class Frm_MM_PurchaseView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        MM_PurchaseD purchase = new MM_PurchaseD();
        cls_Repository rpo = new cls_Repository();
        cls_Grid grid = new cls_Grid();

        string[] CustType = new string[] { "10", "40" };

        public Frm_MM_PurchaseView()
        {
            InitializeComponent();
        }

        private void frmPurchaseView_POP_Load(object sender, EventArgs e)
        {
            barEditStartDate.EditValue = DateTime.Now.AddDays(-7);
            barEditEndDate.EditValue = DateTime.Now;

            //달력 크기 키우기
            rpo.Set_POP_Dtp(barEditStartDate);
            rpo.Set_POP_Dtp(barEditEndDate);

            LupCust.Properties.DataSource = code.GetCustomerList(CustType, true);
            LupCust.Properties.DisplayMember = "거래처명";
            LupCust.Properties.ValueMember = "거래처코드";
            LupCust.EditValue = "ALL";
            rpo.Set_POP_SearchLup(LupCust);

            //Grid 설정
            SetGvDetail(gvDetail);
            grid.SetPOPGrid(gvPurchaseMaster);

            DisplayData();
        }

        private void SetGvDetail(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsView.ShowGroupPanel = false;

            gv.IndicatorWidth = 50;                     //순번 너비

            grid.SetPOPGrid(gvDetail);

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

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
            if (gv.Columns.ColumnByFieldName("MaterialClass1") != null) gv.Columns["MaterialClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("MaterialClass2") != null) gv.Columns["MaterialClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("MaterialClass3") != null) gv.Columns["MaterialClass3"].ColumnEdit = lup_Class_Cd3;

            //
            gv.Columns["StoNo"].Visible = false;

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Qty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["BadQty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TFloat);

            //grid.SetCol_Summary(gv, "Qty");
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }

        private void DisplayData()
        {
            this.Validate();

            string startDate = barEditStartDate.EditValue.ConvertDate_To_Char10();
            string endDate = barEditEndDate.EditValue.ConvertDate_To_Char10();

            MM_PurchaseM purchaseMaster = new MM_PurchaseM();
            purchaseMaster.CustCode = LupCust.EditValue.ConvertString_Null_To_Empty();

            gcPurchaseMaster.DataSource = purchaseMaster.Select_MM_PurchaseM(startDate, endDate);
            gvPurchaseMaster.BestFitColumns();
        }

        private void gvPurchaseMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayDetailList();
        }

        private void DisplayDetailList()
        {
            MM_PurchaseD pd = new MM_PurchaseD();
            pd.StoNo = (gvPurchaseMaster.GetFocusedRowCellValue("StoNo") ?? "").ToString();

            gcDetail.DataSource = pd.Select_MM_PurchaseD_StoNo();
            gvDetail.BestFitColumns();
        }

        //조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        //등록
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            Frm_MM_Purchase_TypeA form = new Frm_MM_Purchase_TypeA("Insert", "");
            form.ShowDialog();
            DisplayData();
        }

        //수정
        private void button5_Click(object sender, EventArgs e)
        {
            if (gvPurchaseMaster.FocusedRowHandle < 0) return;

            Frm_MM_Purchase_TypeA form = new Frm_MM_Purchase_TypeA("Update", gvPurchaseMaster.GetFocusedRowCellValue("StoNo").ToString());
            form.ShowDialog();
            DisplayData();
            DisplayDetailList();
        }

        //삭제
        private void button4_Click(object sender, EventArgs e)
        {
            //자료를 삭제하는 부분
            if (cls_MessageBox.Show("선택한 자료를 삭제 하시겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            try
            {
                string stoNo = gvPurchaseMaster.GetRowCellValue(gvPurchaseMaster.FocusedRowHandle, "StoNo").ToString();

                if (stoNo.Length > 0)
                {
                    purchase.DeletePurchaseMaster(gvPurchaseMaster.GetFocusedRowCellValue("CorpId").ToString(), stoNo);

                    //재고 삭제(입고내역 기준으로 출고내역 있는 경우 삭제 예외처리 추가 필요, 현재는 입고내역으로 출고처리하는 것이 없음)
                    ST_StockD std = new ST_StockD();
                    std.JobNo = stoNo;

                    std.DeleteStockMD();

                    DisplayData();
                }//end if
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
	}
}