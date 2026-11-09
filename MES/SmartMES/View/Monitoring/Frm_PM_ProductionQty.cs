using SmartMES.Models.IF;
using SmartMES.UserControls;
using System;
using System.Data;

namespace SmartMES.View.Monitoring
{
	public partial class Frm_PM_ProductionQty : DevExpress.XtraEditors.XtraForm
    {
        UCDGP[] DataChart = null;

        int MaxCnt = 27;    //MaxUserControl 수
        //int ColCnt = 4;
        //int RowCnt = 4;

        public Frm_PM_ProductionQty()
        {
            InitializeComponent();
        }

        private void Frm_PM_ProductionQty_Load(object sender, EventArgs e)
        {
            DataChart = new UCDGP[] {ucdgp1, ucdgp2, ucdgp3, ucdgp4, ucdgp5, ucdgp6, ucdgp7, ucdgp8, ucdgp9, ucdgp10
                , ucdgp11, ucdgp12, ucdgp13, ucdgp14, ucdgp15, ucdgp16, ucdgp17, ucdgp18, ucdgp19, ucdgp20, ucdgp21, ucdgp22, ucdgp23, ucdgp24, ucdgp25, ucdgp26, ucdgp27 };
            //InitSetting();
            Search();

            TmTime.Enabled = true;

            TmSearch.Interval = 10000;  //10초
            TmSearch.Enabled = true;
        }

        private void Search()
        {
            IF_DGPCode mTDGPCode = new IF_DGPCode();
            DataTable dt = null;

            int cnt = 0;

            dt = mTDGPCode.Select_DGPList_Counter();

            foreach (DataRow dr in dt.Rows)
            {
                if (cnt >= MaxCnt) continue;
                if (DataChart[cnt] == null) continue;

                string dgpCode = dr["DgpCode"].ToString();

                DataChart[cnt].DisplayDGP(dgpCode);

                cnt++;
            }

        }

        private void TmTime_Tick(object sender, EventArgs e)
        {
            LblTime.Text = DateTime.Now.ToString();
        }

        private void TmSearch_Tick(object sender, EventArgs e)
        {
            Search();
        }
    }
}