using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.MM;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.Lookup
{
    public partial class Frm_LookUpPurchase : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public delegate void SetData(DataRowView drv);
        public event SetData onSetData;

        public Frm_LookUpPurchase()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = System.Windows.Forms.Screen.GetBounds(MousePosition).Location;

            InitializeComponent();
        }

        public Frm_LookUpPurchase(string _table, string _custCode)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = System.Windows.Forms.Screen.GetBounds(MousePosition).Location;

            InitializeComponent();
        }

        private void Frm_LookUpPurchase_Load(object sender, EventArgs e)
        {
            Init();
            SetGrid(gvDetail);

            DisplayData();
        }

        private void Init()
		{
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Cust.smtLookUpDataSource = code.GetCustomerI_Lup(WrGlobal.CorpID, true);
            Lup_Cust.smtLookUpDataDisplayMember = "거래처명";
            Lup_Cust.smtLookUpDataValueMember = "거래처코드";
            Lup_Cust.smtLookUpValue = "ALL";
            //Lup_Cust.smtLookUpValue = _custCode;
        }

        private void SetGrid(GridView gv)
		{
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비


            grid.Create_GvColumn(gv, "출고량", "OutQty");
            grid.Create_GvColumn(gv, "입고량", "Qty");

            grid.Create_GvColumn(gv, "입고번호", "StoNo");
            grid.Create_GvColumn(gv, "순번", "StoSeq");
            grid.Create_GvColumn(gv, "LotNo", "LotNo");
            grid.Create_GvColumn(gv, "거래처", "CustName");

            grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");

            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");

            grid.Create_GvColumn(gv, "비고", "Remark");

            //
            grid.Column_AllowEdit(gv.Columns["OutQty"]);

            //
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TFloat);

            //
            gv.Columns["OutQty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

            //
            gv.Columns["UnitPrice"].Visible = false;
            gv.Columns["Price"].Visible = false;
            gv.Columns["VatPrice"].Visible = false;
            gv.Columns["TotalPrice"].Visible = false;

            //
            grid.RowNumber_Indicator(gv);
			gv.CellValueChanged += Gv_CellValueChanged;
        }

		private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            if(e.Column.FieldName == "OutQty")
			{
                float value = e.Value.ConvertFloat_Null_To_Zero();

                if(value == 0)
				{
                    gvDetail.UnselectRow(e.RowHandle);
				}
				else
				{
                    gvDetail.SelectRow(e.RowHandle);
				}
			}
		}

		private void DisplayData()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char8();

            MM_PurchaseD pd = new MM_PurchaseD();
            pd.CorpId = WrGlobal.CorpID;
            pd.CustCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();

            gcDetail.DataSource = pd.SelectPurchaseDList(startDate, endDate);
            gvDetail.BestFitColumns();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            gvDetail.PostEditor();
            gvDetail.UpdateCurrentRow();

            if (gvDetail.SelectedRowsCount < 1)
            {
                MessageBox.Show("등록할 항목을 선택해 주세요.");
                return;
            }

            int rowIdx = 0;

            for (int rowCnt = 0; rowCnt < gvDetail.SelectedRowsCount; rowCnt++)
            {
                rowIdx = gvDetail.GetSelectedRows()[rowCnt];

                onSetData((DataRowView)gvDetail.GetRow(rowIdx));
            }
            this.Close();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void gvDetail_SelectionChanged_1(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Add)
            {
                if (gvDetail.GetRowCellValue(e.ControllerRow, "OutQty") == DBNull.Value)
                {
                    gvDetail.SetRowCellValue(e.ControllerRow, "OutQty", gvDetail.GetRowCellValue(e.ControllerRow, "Qty"));
                }
            }
        }
    }
}