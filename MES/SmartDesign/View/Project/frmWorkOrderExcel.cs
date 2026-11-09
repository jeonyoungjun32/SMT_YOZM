using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDesign.View.Project
{
    public partial class frmWorkOrderExcel : DevExpress.XtraEditors.XtraForm
    {
        public frmWorkOrderExcel()
        {
            InitializeComponent();

            spreadsheetControl1.LoadDocument(@"g:\작업지시서\샘플.xlsx");
        }
    }
}