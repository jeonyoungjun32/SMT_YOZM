using DevExpress.XtraEditors;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.BM
{
    public partial class Frm_BM_SalesList : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";

        public Frm_BM_SalesList()
        {
            InitializeComponent();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayMaterial();
        }

        private void DisplayMaterial()
        {
            query = $"exec POP_GetSalesMaterial '{WrGlobal.CorpID}','{dateStart.EditValue:yyyy-MM-dd}','{dateEnd.EditValue:yyyy-MM-dd}'";
            gcProduct.DataSource = db.GetDataView("제품", query);
        }

        private void frmSalesList_Load(object sender, EventArgs e)
        {
            dateStart.EditValue = DateTime.Now.Date;
            dateEnd.EditValue = DateTime.Now.Date;
            DisplayMaterial();

        }

        private void gvProduct_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string productCode = gvProduct.GetFocusedRowCellValue("MaterialCode")?.ToString();

            query = $"exec POP_GetBM_SalesD '{WrGlobal.CorpID}','{dateStart.EditValue:yyyy-MM-dd}','{dateEnd.EditValue:yyyy-MM-dd}','{productCode}'";
            gcDetail.DataSource = db.GetDataView("출고", query);

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}