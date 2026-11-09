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
using SmartMES.Common;
using SmartCommon.Common;

namespace SmartMES.View.Monitoring
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";

        public XtraForm1()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            query = "Select top 1 * From MachineStatus Where Status = '가동중'";
            DataView dv = db.GetDataView("설비현황", query);
            foreach (DataRowView item in dv)
            {
                label9.Text = item["MachineName"].ToString();
                label3.Text = $"{item["PlanQty"]:#,0}";
                label2.Text = item["WorkQty"].ToString();

                bunifuCircleProgressbar1.MaxValue = Convert.ToInt32(item["PlanQty"]);
                bunifuCircleProgressbar1.Value = Convert.ToInt32(item["WorkQty"]);


            }

        }
    }
}