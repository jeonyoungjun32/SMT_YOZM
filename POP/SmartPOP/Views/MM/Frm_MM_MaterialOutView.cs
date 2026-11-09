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
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using SmartPOP.Models;
using SmartPOP.Common;
using SMT_MES.Controls;
using SmartPOP.Models.BM;
using SmartPOP.Models.ST;
using SmartMES.View.MM;

namespace SmartPOP.Views.MM
{
    public partial class Frm_MM_MaterialOutView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();

        private string query = "";
        Database db = new Database();

        public Frm_MM_MaterialOutView()
        {
            InitializeComponent();
        }

        private void frmPurchaseView_Load(object sender, EventArgs e)
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
            DtStart.EditValue = DateTime.Now.Date.AddDays(-7);
            DtEnd.EditValue = DateTime.Now.Date;

            //달력 크기 키우기
            rpo.Set_POP_Dtp(DtStart);
            rpo.Set_POP_Dtp(DtEnd);
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
            query = $@"SELECT 'ALL' 거래처코드, '전체' 거래처명
                        UNION ALL
                        SELECT CustCode 거래처코드, CustName 거래처명
                        FROM CM_Cust
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CustType IN ('10', '40')
                        ";

            LupCustomer.Properties.DataSource = db.GetDataTable(query);
            LupCustomer.Properties.DisplayMember = "거래처명";
            LupCustomer.Properties.ValueMember = "거래처코드";

            LupCustomer.Properties.ShowClearButton = false;
            LupCustomer.Properties.View.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            LupCustomer.Properties.View.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정

            LupCustomer.EditValue = "ALL";
        }

        private void Set_GvMaster(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            cls_Repository rpo = new cls_Repository();
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(), "거래처코드", "거래처명");

            grid.Create_GvColumn(gv, "출고번호", "SalesNo");
            grid.Create_GvColumn(gv, "매출업체", "ShipToCode", lupCust);
            grid.Create_GvColumn(gv, "출고일", "SalesDate");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TFloat);


            //
            grid.SetPOPGrid(gv);
        }

        private void Set_GvDetail(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            cls_Repository rpo = new cls_Repository();
            RepositoryItemSearchLookUpEdit lupVat = rpo.Create_RepositoryItemSearchLookUp(code.GetClassList("C010"), "분류코드", "분류명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

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

            //
            //gv.Columns["MaterialCode"].FieldName = "ProductCode";

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            grid.SetCol_Type(gv.Columns["Qty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TFloat);

            //
            grid.SetPOPGrid(gv);
        }
        #endregion

        #region 이벤트
        private void BtnIns_Click(object sender, EventArgs e)
        {
            Frm_MM_MaterialOutDetail frm = new Frm_MM_MaterialOutDetail();
            frm.ShowDialog();

            DisplayMaster();
            DisplayDetail();
        }

        private void BtnUpd_Click(object sender, EventArgs e)
        {
            string salesNo = GvBM_SalesM.GetFocusedRowCellValue("SalesNo")?.ToString();

            if (salesNo == null) return;

            Frm_MM_MaterialOutDetail frm = new Frm_MM_MaterialOutDetail("U", salesNo);
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

            query = $@"SELECT DISTINCT SM.SalesNo
		                    , SM.ShipToCode
		                    , SM.SalesDate
		                    , SM.TotalPrice
		                    , SM.Remark
                    FROM BM_SalesM SM
                    INNER JOIN BM_SalesD SD ON SM.SalesNo = SD.SalesNo
                    WHERE SM.CorpId  = '{WrGlobal.CorpID}'
                    AND SD.ReqType = '자재출고'	-- 자재출고만 
                    AND SM.SalesDate BETWEEN '{DtStart.EditValue:yyyy-MM-dd}' AND '{DtEnd.EditValue:yyyy-MM-dd}'
                    ";

            if (custCode != "") query += $"\n AND ShipToCode = '{custCode}'";

            query += "\n ORDER BY SalesNo DESC";

            GcBM_SalesM.DataSource = db.GetDataTable(query);
            GvBM_SalesM.BestFitColumns();
        }

        private void DisplayDetail()
        {
            string salesNo = GvBM_SalesM.GetFocusedRowCellValue("SalesNo")?.ToString();

            query = $@"SELECT sd.SalesSeq
		                    , sd.ItemCode

                            , cm.ItemName
                            ,  cm.Alias
                            ,  cm.ItemClass1
                            ,  cm.ItemClass2
                            ,  cm.ItemClass3
                            ,  cm.Size
                            ,  cm.UnitWeight
                            ,  cm.Texture
                            ,  cm.Type
                            ,  cm.UnitCode
                            ,  cm.ColorCode

		                    , sd.Qty
		                    , sd.UnitCode
                            , sd.UnitPrice
		                    , sd.Price
		                    , sd.VatPrice
		                    , sd.TotalPrice
		                    , sd.VatType
		                    , sd.VatRate
		                    , sd.HouseCode
                    FROM BM_SalesD sd
                    LEFT JOIN CM_Item cm ON sd.CorpId = cm.CorpId and sd.ItemCode = cm.ItemCode
                    WHERE sd.CorpId = '{WrGlobal.CorpID}'
                    AND sd.SalesNo = '{salesNo}'
                    ORDER BY sd.SalesSeq 
            ";

            GcBM_SalesD.DataSource = db.GetDataTable(query);
            GvBM_SalesD.BestFitColumns();
        }
        #endregion

		private void cmdDel_Click(object sender, EventArgs e)
		{
            // 선택한 항목이 없으면 리턴
            if (GvBM_SalesM.GetSelectedRows().Length == 0) return;

            //자료를 삭제하는 부분
            if (cls_MessageBox.Show("선택한 자료를 삭제 하시겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            DataRow dr = GvBM_SalesM.GetFocusedDataRow();
            string salesNo = dr["SalesNo"].ConvertString_Null_To_Empty();

            bool result = true;

            //재고 삭제
            ST_StockD std = new ST_StockD();
            std.JobNo = salesNo;
            result = std.DeleteStockMD();

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
                cls_MessageBox.Show(cls_MessageBox.Del_Comp);
            }
            else
            {
                cls_MessageBox.Show(cls_MessageBox.Del_Fail);
            }
        }

		private void btn_Close_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}