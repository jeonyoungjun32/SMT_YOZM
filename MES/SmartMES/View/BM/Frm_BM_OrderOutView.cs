using System;
using SmartCommon.Common;
using SmartMES.Models;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace SmartMES.View.BM
{
    public partial class Frm_BM_OrderOutView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsPurchase purchase = new clsPurchase();
        clsGrid grid = new clsGrid();

        private string query = "";
        Database db = new Database();

        public Frm_BM_OrderOutView()
        {
            InitializeComponent();
        }

        private void Frm_BM_OrderOutView_Load(object sender, EventArgs e)
        {
            // 디자인
            Set_DtEdit();

            Set_Lup();

            Set_GvMaster(GvBM_SalesM);
            Set_GvDetail(GvBM_SalesD);

            // 조회
            DisplayMaster();
            DisplayDetail();
        }

        #region 디자인
        private void Set_DtEdit()
        {
            DtStart.EditValue = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DtEnd.EditValue = DateTime.Now.Date;
        }

        private void Set_Lup()
        {
            /*
                CustType - CLASS : A030
                10 : 매입
                20 : 매출
                30 : 외주
                40 : 매입/매출
             */
            query = $@"SELECT'ALL' 거래처코드, '전체' 거래처명
                        UNION ALL
                        SELECT CustCode 거래처코드, CustName 거래처명
                        FROM CM_Cust
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CustType IN ('30')
                        ";

            LupCustomer.Properties.DataSource = db.GetDataTable(query);
            LupCustomer.Properties.DisplayMember = "거래처명";
            LupCustomer.Properties.ValueMember = "거래처코드";

            LupCustomer.EditValue = "ALL";
        }

        private void Set_GvMaster(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(), "거래처코드", "거래처명");

            grid.Create_GvColumn(gv, "출고번호", "SalesNo");
            grid.Create_GvColumn(gv, "매출업체", "ShipToCode", lupCust);
            grid.Create_GvColumn(gv, "출고일", "SalesDate");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "비고", "Remark");
        }

        private void Set_GvDetail(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            RepositoryItemSearchLookUpEdit lupUnitCode = rpo.Create_RepositoryItemSearchLookUp(code.GetUnitCode(), "코드", "코드명");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lupVat = rpo.Create_RepositoryItemSearchLookUp(code.GetClassList("C010"), "분류코드", "분류명");

            grid.Create_GvColumn(gv, "순번", "SalesSeq");
            grid.Create_GvColumn(gv, "품번", "ItemCode", lup_Item);
            grid.Create_GvColumn(gv, "출고량", "Qty");
            grid.Create_GvColumn(gv, "단위", "UnitCode", lupUnitCode);
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "합계", "TotalPrice");
            grid.Create_GvColumn(gv, "부가세유형", "VatType", lupVat);
            grid.Create_GvColumn(gv, "부가세율(%)", "VatRate");
            grid.Create_GvColumn(gv, "비고", "Remark");
        }
        #endregion

        #region 이벤트
        private void BtnIns_Click(object sender, EventArgs e)
        {
            Frm_BM_OrderOutDetail frm = new Frm_BM_OrderOutDetail();
            frm.ShowDialog();

            DisplayMaster();
            DisplayDetail();
        }

        private void BtnUpd_Click(object sender, EventArgs e)
        {
            string salesNo = GvBM_SalesM.GetFocusedRowCellValue("SalesNo")?.ToString();

            if (salesNo == null) return;

            Frm_BM_OrderOutDetail frm = new Frm_BM_OrderOutDetail("U", salesNo);
            frm.ShowDialog();

            DisplayMaster();
            DisplayDetail();
        }

        private void GvBM_SalesM_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayDetail();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayMaster();
            DisplayDetail();
        }
        #endregion

        #region 조회
        private void DisplayMaster()
        {
            string custCode = LupCustomer.EditValue?.ToString();

            if (custCode == null || custCode == "ALL") custCode = "";

            query = $@"SELECT SM.SalesNo
		                    , SM.ShipToCode
		                    , SM.SalesDate
		                    , SM.TotalPrice
		                    , SM.Remark
                    FROM BM_SalesM SM
                    WHERE SM.CorpId  = '{WrGlobal.CorpID}'
                    AND SM.SalesDate BETWEEN '{DtStart.EditValue:yyyy-MM-dd}' AND '{DtEnd.EditValue:yyyy-MM-dd}'
                    AND SM.OutType = '40'   -- 외주출고만
                    ";

            if (custCode != "") query += $"\n AND SM.ShipToCode = '{custCode}'";

            query += "\n ORDER BY SalesNo DESC";

            GcBM_SalesM.DataSource = db.GetDataTable(query);
            GvBM_SalesM.BestFitColumns();
        }

        private void DisplayDetail()
        {
            string salesNo = GvBM_SalesM.GetFocusedRowCellValue("SalesNo")?.ToString();

            query = $@"SELECT SalesSeq
		                    , ItemCode
		                    , Qty
		                    , UnitCode
                            , UnitPrice
		                    , Price
		                    , VatPrice
		                    , TotalPrice
		                    , VatType
		                    , VatRate
		                    , HouseCode
                    FROM BM_SalesD
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND SalesNo = '{salesNo}'
                    ORDER BY SalesSeq 
                        ";

            GcBM_SalesD.DataSource = db.GetDataTable(query);
            GvBM_SalesD.BestFitColumns();
        }
        #endregion
    }
}