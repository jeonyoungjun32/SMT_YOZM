using SmartMES.Models;
using System;
using System.Windows.Forms;
using SmartCommon.Common;
using SmartMES.Models.SM;
using System.Data;
using SmartMES.Common;
using DevExpress.XtraGrid.Views.Grid;

namespace SmartMES.View.Lookup
{
    public partial class Frm_LookUpNotice : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        Database db = new Database();

        DateTime m_dTime;

        public Frm_LookUpNotice(DateTime dTime)
        {
            InitializeComponent();
            m_dTime = dTime;
        }

        private void Frm_LookUpNotice_Load(object sender, EventArgs e)
        {
            Set_Lup();
            grid.Set_Navigator(gvNotice);

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
            SM_Notice nt = new SM_Notice();

            gcNotice.DataSource = nt.Select_SM_Notice_Popup(m_dTime.ToString("yyyy-MM-dd HH:mm:ss"));
            //gvNotice.BestFitColumns();
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
            gvNotice.FocusedRowObjectChanged -= gvNotice_FocusedRowObjectChanged;
        }

        private void gvNotice_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            if (IsDisposed) return;
            if (!IsHandleCreated) return;

            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            if (dr == null) return;

            smtBodyRichEdit1.smtRichEditRtfText = dr["Body"].ConvertString_Null_To_Empty(); // 내용
        }
    }
}
