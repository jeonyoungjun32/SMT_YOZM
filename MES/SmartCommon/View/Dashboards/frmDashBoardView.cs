using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.DataAccess.ConnectionParameters;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using SmartCommon.Common;
using DevExpress.Xpo.DB.Helpers;

namespace SmartCommon.View.Dashboards
{
    public partial class frmDashBoardView : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        DataRowView master = null;
        string query = "";

        string menuId = "";

        public frmDashBoardView()
        {
            InitializeComponent();

            menuId = WrGlobal.OpeningMenuId;

            master = IsExistDashboardItem(menuId);

            if (master["XML"].ToString() == "")
            {
                MessageBox.Show("선택된 메뉴는 대시보드가 작성되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            master["XML"] = master["XML"].ToString().Replace("?<?", "<?");
            MemoryStream ms = new MemoryStream();
            byte[] m_Buffer;

            m_Buffer = System.Text.Encoding.UTF8.GetBytes(master["XML"].ToString());
            ms.Write(m_Buffer, 0, m_Buffer.Length);
            ms.Seek(0, SeekOrigin.Begin);
            dashboardViewer.LoadDashboard(ms);
            ms.Flush();
            ms.Close();
        }
        
        public frmDashBoardView(DataRowView _drv)
        {
            InitializeComponent();

            menuId = _drv["MenuId"].ToString();

            master = _drv;

            if (master["XML"].ToString() == "")
            {
                MessageBox.Show("선택된 메뉴는 대시보드가 작성되지 않았습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            master["XML"] = master["XML"].ToString().Replace("?<?", "<?");
            MemoryStream ms = new MemoryStream();
            byte[] m_Buffer;

            m_Buffer = System.Text.Encoding.UTF8.GetBytes(master["XML"].ToString());
            ms.Write(m_Buffer, 0, m_Buffer.Length);
            ms.Seek(0, SeekOrigin.Begin);
            dashboardViewer.LoadDashboard(ms);
            ms.Flush();
            ms.Close();
        }

        private void frmDashBoardView_Load(object sender, EventArgs e)
        {

        }

        private void dashboardViewer_ConfigureDataConnection(object sender, DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs e)
        {
            if (e.ConnectionParameters.GetType().Name != "MsSqlConnectionParameters") return;
            MsSqlConnectionParameters parameters = (MsSqlConnectionParameters)e.ConnectionParameters;

            parameters.UserName = WrGlobal.DBUserName;
            parameters.Password = WrGlobal.DBUserPassword;
        }

        private void dashboardViewer_DashboardLoaded(object sender, DashboardLoadedEventArgs e)
        {
            int cnt = e.Dashboard.Parameters.Count;

            for (int i = 0; i < (cnt); i++)
            {
                if (e.Dashboard.Parameters[i].Type.Name == "DateTime")
                {
                    if (e.Dashboard.Parameters[i].Name.ToLower().IndexOf("start") >= 0 || e.Dashboard.Parameters[i].Name.ToLower().IndexOf("from") >= 0)
                    {
                        e.Dashboard.Parameters[i].Value = DateTime.Today.AddDays(-1);
                    }
                    else if (e.Dashboard.Parameters[i].Name.ToLower().IndexOf("end") >= 0 || e.Dashboard.Parameters[i].Name.ToLower().IndexOf("to") >= 0)
                    {
                        e.Dashboard.Parameters[i].Value = DateTime.Today;
                    }
                    else
                    {
                        e.Dashboard.Parameters[i].Value = DateTime.Today;
                    }
                }

            }
        }

        private void frmDashBoardView_Shown(object sender, EventArgs e)
        {
            dashboardViewer.ShowDashboardParametersForm();
        }

        private DataRowView IsExistDashboardItem(string menuId)
        {
            query = string.Format("Select bi.*, mg.Title_ko MenuName, pmg.Title_ko ParentMenuName "
                                + "From DashBoardItem bi "
                                    + "Inner Join MenuGroup mg on bi.CorpId = mg.CorpId and bi.MenuId = mg.Id "
                                    + "Left Join MenuGroup pmg on mg.CorpId = pmg.CorpId and mg.ParentId = pmg.Id "
                                + "Where bi.CorpId = '{0}' and bi.MenuId = '{1}'",
            WrGlobal.PublicCorpID, menuId);

            return db.GetDataRecord(query);
        }
    }
}