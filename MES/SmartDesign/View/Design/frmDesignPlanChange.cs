using SmartMES.Common;
using SmartMES.Models;
using SmartDesign.Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartDesign.View.Design
{
	public partial class frmDesignPlanChange : DevExpress.XtraEditors.XtraForm
    {
        private string _planEndDate = string.Empty;

        public string PlandEndDate { get => _planEndDate; set => _planEndDate = value; }

        public frmDesignPlanChange()
        {
            InitializeComponent();
        }

        #region 이벤트


        #endregion 이벤트

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDesignPlanChange_Load(object sender, EventArgs e)
        {
            smtBodyDatetime1.smtDateTimeValue = Convert.ToDateTime(_planEndDate);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            PlandEndDate = smtBodyDatetime1.smtDateTimeValue.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}