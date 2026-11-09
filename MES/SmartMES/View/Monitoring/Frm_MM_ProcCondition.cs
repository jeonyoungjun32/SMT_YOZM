using DevExpress.Utils.Layout;
using SmartCommon.Common;
using SmartMES.Models.IF;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.Monitoring
{
	public partial class Frm_MM_ProcCondition : DevExpress.XtraEditors.XtraForm
    {
        UCDGP3[] DataChart = null;

        string Type = "Machine"; //설비기준, DGP기준
        
        int MaxCnt = 10;    //MaxUserControl 수
        int ColCnt = 5;
        int RowCnt = 2;
        int CodeCnt = 6;    //한 설비? 항목수


        public Frm_MM_ProcCondition()
        {
            InitializeComponent();
        }

        private void Frm_MM_ProcCondition_Load(object sender, EventArgs e)
        {
            InitSetting();
            Search();

            TmTime.Enabled = true;

            TmSearch.Interval = 10000;  //10초
            TmSearch.Enabled = true;
        }

        private void InitSetting()
        {
            int nCount = 0;

            //MaxCnt
            switch (Database.DbName)
            {
                case "IWAUTO":
                    Type = "Machine";
                    MaxCnt = 11;
                    ColCnt = 4;
                    RowCnt = 3;
                    CodeCnt = 6;

                    break;

                default:
                    Type = "Machine";
                    MaxCnt = 6;
                    ColCnt = 4;
                    RowCnt = 3;
                    CodeCnt = 6;
                    break;

            }
             //TablePanel 초기화 
             TPMain.Rows.Clear();
            TPMain.Columns.Clear();

            //TablePanel 컬럼 생성
            for (int i = 0; i < ColCnt; i++)
            {
                TPMain.Columns.Add(new TablePanelColumn(TablePanelEntityStyle.Relative, 50));
            }

            //TablePanel Row 생성
            for (int i = 0; i < RowCnt; i++)
            {
                TPMain.Rows.Add(new TablePanelRow(TablePanelEntityStyle.Relative, 50));
            }

            DataChart = new UCDGP3[MaxCnt];

            for (int i = 0; i < TPMain.Rows.Count; i++)
            {
                for (int j = 0; j < TPMain.Columns.Count; j++)
                {
                    if (nCount < MaxCnt)
                    {
                        DataChart[nCount] = new UCDGP3(CodeCnt);
                        DataChart[nCount].Dock = DockStyle.Fill;
                        DataChart[nCount].Visible = true;

                        TPMain.Controls.Add(DataChart[nCount]);
                        TPMain.SetCell(DataChart[nCount], i, j);

                        nCount++;
                    }
                }
            }
        }

        private void Search()
        {
            IF_DGPCode mTDGPCode = new IF_DGPCode();
            
            DataTable dt = null;

           int cnt = 0;

            if (Type == "Machine")
            {
                dt = mTDGPCode.Select_DGPMachine();

                foreach (DataRow dr in dt.Rows)
                {
                    if (cnt >= MaxCnt) continue;
                    if (DataChart[cnt] == null) continue;

                    string machine_Cd = dr["Equip_Name"].ToString();

                    DataChart[cnt].Display(machine_Cd);
                    cnt++;
                }
            }
            else
            {
                dt = mTDGPCode.Select_DGPList();

                foreach (DataRow dr in dt.Rows)
                {
                    if (cnt >= MaxCnt) continue;
                    if (DataChart[cnt] == null) continue;

                    string dgpCode = dr["DgpCode"].ToString();

                    DataChart[cnt].DisplayDGP(dgpCode);
                    cnt++;
                }

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