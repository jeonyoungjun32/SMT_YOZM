using DevExpress.Utils.Layout;
using System;
using System.Data;
using System.Windows.Forms;
using SmartMES.Common;
using SmartMES.UserControls;
using SmartCommon.Common;
using SmartMES.Models;

namespace SmartMES.View.Monitoring.Machine
{
    public partial class Frm_PM_MachineProgress : DevExpress.XtraEditors.XtraForm
	{
		Database db = new Database();
		string query = "";

		clsCode code = new clsCode();

		ucMachineManufacture[] DataChart = null;

		string Type = "Machine"; //설비기준, DGP기준

		int MachineCnt = 20;    //설비수
		int ColCnt = 3;
		int RowCnt = 5;


		public Frm_PM_MachineProgress()
		{
			InitializeComponent();
		}

		private void Frm_PM_MachineProgress_Load(object sender, EventArgs e)
		{
			InitSetting();
			Search();

			TmTime.Enabled = true;

			TmSearch.Interval = 10000;  //10초
			TmSearch.Enabled = true;
		}

		private void InitSetting()
		{

			DataTable dt = SearchMachine();

			if (dt != null)
			{
				MachineCnt = dt.Rows.Count;
				RowCnt = (MachineCnt / 5).ConvertInt_Null_To_Zero();
			}

			int nCount = 0;

			//TablePanel 초기화
			TPMain.Rows.Clear();
			TPMain.Columns.Clear();
			this.TPMain.AutoScroll = true;

			//TablePanel 컬럼 생성
			for (int i = 0; i < ColCnt; i++)
			{
				TPMain.Columns.Add(new TablePanelColumn(TablePanelEntityStyle.Relative, 50));
			}

			//TablePanel Row 생성
			for (int i = 0; i < RowCnt; i++)
			{
				TPMain.Rows.Add(new TablePanelRow(TablePanelEntityStyle.Relative, 260));
			}

			DataChart = new ucMachineManufacture[MachineCnt];

			for (int i = 0; i < TPMain.Rows.Count; i++)
			{
				for (int j = 0; j < TPMain.Columns.Count; j++)
				{
					if (nCount < MachineCnt)
					{
						DataChart[nCount] = new ucMachineManufacture();
						DataChart[nCount].Dock = DockStyle.Fill;
						DataChart[nCount].Visible = true;

						TPMain.Controls.Add(DataChart[nCount]);
						TPMain.SetCell(DataChart[nCount], i, j);

						nCount++;
					}
				}
			}

			//// 폼 갱신 시 기존 카드 제거
			//flowLayoutPanel1.AutoSize = false;  // 반드시 False!
			//flowLayoutPanel1.AutoScroll = true; // 반드시 True!
			//flowLayoutPanel1.Dock = DockStyle.Fill; // 부모 폼 크기에 고정
			//flowLayoutPanel1.Controls.Clear();

			//// [참고] DB에서 설비 목록 가져오기 (작성하신 Database 클래스 활용)
			//// string sql = "SELECT MachineCode FROM EQ_Machine WHERE UseYn = 'Y'";
			//// DataTable dt = db.GetDataTable(sql);

			//// --- 테스트용 데이터 (실제 연동 시 삭제) ---


			//// 레이아웃 업데이트 일시 중지 (속도 향상 및 깜빡임 방지)
			//flowLayoutPanel1.SuspendLayout();

			//foreach (DataRow dr in dt.Rows)
			//{
			//	string mCode = dr["MachineCode"].ToString();

			//	// 유저 컨트롤 생성
			//	DataChart[nCount] = new ucMachineManufacture();
			//	//ucMachineManufacture ucCard = new ucMachineManufacture();

			//	// [중요] FlowLayout에서는 Dock을 쓰지 않습니다.
			//	// 대신 Margin을 주어 카드 사이의 간격을 조절합니다.
			//	DataChart[nCount].Margin = new Padding(15);

			//	// 유저컨트롤의 Size는 uc 내부에서 설정한 (420, 240)이 유지됩니다.
			//	// 만약 MinimumSize를 설정해두었다면 절대 겹치지 않습니다.

			//	// 실적 데이터 표시 실행
			//	DataChart[nCount].Display(mCode);

			//	// 패널에 추가 (순서대로 착착 붙습니다)
			//	flowLayoutPanel1.Controls.Add(DataChart[nCount]);

			//	nCount++;
			//}

			//flowLayoutPanel1.ResumeLayout();
		}

		private DataTable SearchMachine()
		{
			query = $@"

SELECT TOP 20 MachineCode
FROM EQ_Machine
WHERE CorpId = '{WrGlobal.CorpID}'
AND UseYn = 'Y'
";

			return db.GetDataTable(query);
		}

		private void Search()
		{
			DataTable dt = SearchMachine();

			int cnt = 0;

			foreach (DataRow dr in dt.Rows)
			{
				if (cnt >= MachineCnt) continue;
				if (DataChart[cnt] == null) continue;

				string MachineCode = dr["MachineCode"].ToString();

				DataChart[cnt].Display(MachineCode);
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