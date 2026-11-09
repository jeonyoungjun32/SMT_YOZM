using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.ST;
using SmartMES.View.MM;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.MM
{
	public partial class Frm_MM_MaterialOut : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

		string[] CustType = new string[] { "10", "40" };


		public Frm_MM_MaterialOut()
        {
            InitializeComponent();
        }

        private void Frm_MM_MaterialOut_Load(object sender, EventArgs e)
        {
            // 디자인
            Set_DtEdit();
            Set_Lup();

            // 그리드
            Set_GvMaster(GvBM_SalesM);
            Set_GvDetail(GvBM_SalesD);

            // 조회
            DisplayMaster();

            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);
        }

        #region 디자인
        private void Set_DtEdit()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;
        }

        private void Set_Lup()
        {
            DataTable dtCust = code.Get_CustomerDt(true, CustType);

            LupCustomer.smtLookUpDataSource = dtCust;
            LupCustomer.smtLookUpDataDisplayMember = "내용";
            LupCustomer.smtLookUpDataValueMember = "코드";
            LupCustomer.smtLookUpValue = "ALL";
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

            //
            if(Database.DbName.ToUpper().Contains("KNP"))
			{
                gv.Columns["ShipToCode"].Visible = false;
			}

            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TInt);
        }

        private void Set_GvDetail(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            RepositoryItemSearchLookUpEdit lupVat = rpo.Create_RepositoryItemSearchLookUp(code.GetClassList("C010"), "분류코드", "분류명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "순번", "SalesSeq");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "출고량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "합계", "TotalPrice");
            grid.Create_GvColumn(gv, "부가세유형", "VatType", lupVat);
            grid.Create_GvColumn(gv, "부가세율(%)", "VatRate");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //포맷
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TInt);

            //Summary
            grid.SetCol_Summary(gv, "Price");
            grid.SetCol_Summary(gv, "VatPrice");
            grid.SetCol_Summary(gv, "TotalPrice");
        }

        private void GvBM_SalesM_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayDetail();
        }

		#endregion

		#region 이벤트
		private void BtnIns_Click(object sender, EventArgs e)
		{
			Frm_MM_MaterialOutD_TypeA form = new Frm_MM_MaterialOutD_TypeA("I", "");
			form.FormClosed += Form_FormClosed;
			form.ShowDialog();
		}

		private void BtnUpd_Click(object sender, EventArgs e)
		{
			string salesNo = GvBM_SalesM.GetFocusedRowCellValue("SalesNo")?.ToString();
			if (salesNo == null) return;

			Frm_MM_MaterialOutD_TypeA form = new Frm_MM_MaterialOutD_TypeA("U", salesNo);
			form.FormClosed += Form_FormClosed;
			form.ShowDialog();
		}

		private void Form_FormClosed(object sender, FormClosedEventArgs e)
		{
            DisplayMaster();
		}

		private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayMaster();
        }
        #endregion

        #region 조회
        private void DisplayMaster()
        {
            string custCode = LupCustomer.smtLookUpValue.ConvertString_Null_To_Empty();
            custCode = custCode == "ALL" ? "" : custCode;

            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            BM_SalesM sm = new BM_SalesM();
            sm.OutType = "30"; //출고구분 10:제품, 30:자재
            sm.ShipToCode = custCode;

            DataTable dt = sm.Select_BM_SalesM(startDate, endDate);

            GcBM_SalesM.DataSource = dt;
            GvBM_SalesM.BestFitColumns();

            if (dt == null) GcBM_SalesD.DataSource = null;
            if (dt.Rows.Count < 1) GcBM_SalesD.DataSource = null;
        }

        private void DisplayDetail()
        {
            string salesNo = GvBM_SalesM.GetFocusedRowCellValue("SalesNo")?.ToString();

            BM_SalesD sd = new BM_SalesD();
            sd.SalesNo = salesNo;

            GcBM_SalesD.DataSource = sd.Select_BM_SalesD_SalesNo();
            GvBM_SalesD.BestFitColumns();
        }

        #endregion

		private void cmdDelete_Click(object sender, EventArgs e)
        {
            // 선택한 항목이 없으면 리턴
            if (GvBM_SalesM.GetSelectedRows().Length == 0) return;

            //자료를 삭제하는 부분
            if (MessageBox.Show("선택한 자료를 삭제 하시겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            DataRow dr = GvBM_SalesM.GetFocusedDataRow();
            string salesNo = dr["SalesNo"].ConvertString_Null_To_Empty();

            bool result = true;

            //재고 삭제
            ST_StockD std = new ST_StockD();
            std.JobNo = salesNo;
            result = std.DeleteStockMD_JobNo();

			if (result)
			{
                //출고 디테일 삭제
                BM_SalesD sd = new BM_SalesD();
                sd.CorpId = WrGlobal.CorpID;
                sd.SalesNo = salesNo;

                result = sd.DeletebySalesNo();
            }
    
			if (result)
			{
                //출고 마스터 삭제
                BM_SalesM sm = new BM_SalesM();
                sm.CorpId = WrGlobal.CorpID;
                sm.SalesNo = salesNo;

                result = sm.DeleteBM_SalesM();
            }

			if (result)
			{
                DisplayMaster();
                XtraMessageBox.Show(cls_MessageBox.Del_Comp);
			}
			else
			{
                XtraMessageBox.Show(cls_MessageBox.Del_Fail);
			}
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}