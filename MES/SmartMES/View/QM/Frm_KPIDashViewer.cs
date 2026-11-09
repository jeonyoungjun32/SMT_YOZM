using SmartMES.View.Management;
using SmartMES.View.QM.Dash;
//using SMT_MES.View.Quality.Dash;
//using SMT_MES.Model;
//using SMT_MES.View.Quality.Dash;

namespace SmartMES.View.QM
{
    public partial class Frm_KPIDashViewer : DevExpress.XtraEditors.XtraForm
    {
        public Frm_KPIDashViewer()
        {
            InitializeComponent();
        }

        public Frm_KPIDashViewer(string frmName)
        {
            InitializeComponent();

            if(frmName == "Frm_KPIDashViewer")
            {
                this.dashboardViewer1.Dashboard = new DashKPI();
            }

            if (frmName == "Frm_Dashboard1")
            {
                this.dashboardViewer1.Dashboard = new BM_Dashboard1();
            }
        }
    }
}