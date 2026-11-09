using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartPOP.Models;
using SmartPOP.Common;

namespace SmartPOP.Views.Lookup
{
    public partial class Frm_LookUpNotice : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();

        DateTime m_dTime;

        public Frm_LookUpNotice(DateTime dTime)
        {
            InitializeComponent();
            m_dTime = dTime;
        }
        private void frmLookUpNotice_Load(object sender, EventArgs e)
        {
            Set_Lup();
            DisplayData();
        }
        private void Set_Lup()
        {
            repositoryItemLookUpEdit3.DataSource = code.smtLookUp("사용자", "", "");
            repositoryItemLookUpEdit3.ValueMember = "사원ID";
            repositoryItemLookUpEdit3.DisplayMember = "사원명";
        }
        private void DisplayData()
        {
            //this.noticeTableAdapter.FillByView(this.codeDataSet.Notice, WrGlobal.CorpID, m_dTime.ToString("yyyy-MM-dd HH:mm:ss"));
            // 데이터셋 제거 필요
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void ChkTodayView_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTodayView.Checked)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void frmLookUpNotice_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
