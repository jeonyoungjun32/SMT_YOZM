using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Win.Gauges.State;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES.UserControls
{
    public partial class ucStock : DevExpress.XtraEditors.XtraUserControl
    {
        int StockQty;
        int SaftyQty;

        public ucStock()
        {
            InitializeComponent();
        }

        public ucStock(int qty, int saftyQty)
        {
            InitializeComponent();

            this.StockQty = qty;
            this.SaftyQty = saftyQty;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelControl1.Text = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            LblSaftyQty.Text = this.SaftyQty.ToString();
            LblSaftyQty.ForeColor = Color.Green;
           
            stateIndicatorComponent1.StateIndex = this.StockQty < this.SaftyQty ? 0 : 1;
            if (stateIndicatorComponent1.StateIndex == 0)
            {
                LblStockQty.ForeColor = Color.Red;
            }
            else
            {
                LblStockQty.ForeColor = Color.Blue;
            }

            LblSaftyQty.Text = this.SaftyQty.ToString();
            LblStockQty.Text = this.StockQty.ToString();
        }
    }
}
