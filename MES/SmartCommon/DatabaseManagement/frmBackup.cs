using DevExpress.XtraEditors;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.DatabaseManagement
{
    public partial class frmBackup : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";
        public frmBackup()
        {
            InitializeComponent();
        }

        private void cmdBackup_Click(object sender, EventArgs e)
        {
            try
            {
                marqueeProgressBarControl1.Enabled = true;
                query = @"BACKUP DATABASE DongjinDB TO DISK = 'D:\백업\Dongjin-" + DateTime.Now.Ticks.ToString() + ".bak'";
                db.ExecuteQuery(query);
                MessageBox.Show("데이터베이스 백업 성공");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                marqueeProgressBarControl1.Enabled = false;
            }


        }
    }
}