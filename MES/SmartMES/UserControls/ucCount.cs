using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraEditors;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartCommon.Common;

namespace SmartMES.UserControls
{
    public partial class ucCount : DevExpress.XtraEditors.XtraUserControl
    {
        Database db = new Database();
        string query = "";
        string machineCode;

        public ucCount()
        {
            InitializeComponent();
        }

        public ucCount(string machineCode,int planQty)
        {
            InitializeComponent();
            this.machineCode = machineCode;
            bunifuCircleProgressbar1.MaxValue = planQty;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            query = $"Select * From MachineStatus Where machineCode = '{machineCode}'";
            DataView dv = db.GetDataView("설비현황", query);
            foreach (DataRowView item in dv)
            {
                label9.Text = item["MachineName"].ToString();
                label3.Text = $"{item["PlanQty"]:#,0}";
                label2.Text = item["WorkQty"].ToString();

                try
                {
                    int planQty = Convert.ToInt32(item["PlanQty"]);
                    int workQty = Convert.ToInt32(item["WorkQty"]);

                    if (workQty == 0 && planQty == 0) break;

                    if (workQty > planQty) workQty = planQty;
                    bunifuCircleProgressbar1.MaxValue = planQty;
                    bunifuCircleProgressbar1.Value = workQty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }

        }
    }
}
