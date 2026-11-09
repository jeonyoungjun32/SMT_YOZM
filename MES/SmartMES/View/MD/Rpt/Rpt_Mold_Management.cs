using SmartCommon.Common;
using System.Data;

namespace SmartMES.View.MD.Rpt
{
    public partial class Rpt_Mold_Management : DevExpress.XtraReports.UI.XtraReport
    {

        public Rpt_Mold_Management(DataTable MachineHistory)
        {
            InitializeComponent();

            this.DataSource = MachineHistory;
            LblCorp_Nm.Text = WrGlobal.CorpName;
        }

    }
}
