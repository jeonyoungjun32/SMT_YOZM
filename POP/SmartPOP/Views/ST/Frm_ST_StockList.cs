using SmartPOP.Common;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartPOP.View.ST
{
    public partial class Frm_ST_StockList : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";
        
        public Frm_ST_StockList()
        {
            InitializeComponent();
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));

            endDate = endDate.AddDays(-1);

            barEditStartDate.EditValue = startDate;

            query = $"select HouseID 코드, Title 창고명 from CM_Warehouse where CorpId = '{WrGlobal.CorpID}'";
            DataView dvHouse = db.GetDataView("창고", query);

            searchWarehouse.Properties.DataSource = dvHouse;
            searchWarehouse.Properties.DisplayMember = "창고명";
            searchWarehouse.Properties.ValueMember = "코드";

            searchWarehouse.EditValue = WrGlobal.WarehouseID;
            DisplayData();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvList.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }//end fnction
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvMachine_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayData();
        }

        private void gvMeterialName_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayData();
        }


        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }


        private void DisplayOption()
        {


        }
        private void DisplayData()
        {
            //2020-05-06 신세은  frmStockMove (재고이동) 화면에 재고리스트랑 프로시저 다른걸로함
            //query = string.Format("exec Stock_MaterialList '{0}','{1}','{2}','{3}','{4:yyyy-MM-dd}'"
            query = $"exec Stock_GetStockList '{WrGlobal.CorpID}','{DateTime.Now:yyyy-MM-dd}','{DateTime.Now:yyyy-MM-dd}','현재고'";
            gcList.DataSource = db.GetDataView("거래처", query);
        }

    }
}