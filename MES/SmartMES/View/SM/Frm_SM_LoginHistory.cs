using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Model.SM;
using SmartMES.Models;
using System;
using System.Data;

namespace SmartMES.View.SM
{
	public partial class Frm_SM_LoginHistory : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        public Frm_SM_LoginHistory()
        {
            InitializeComponent();
        }

        private void Frm_SM_LoginHistory_Load(object sender, EventArgs e)
        {
            Set_SearchLup();
            Set_GridView(Gv_Login);
            Display();
        }

        private void Set_SearchLup()
		{
            DateTime startDate = Convert.ToDateTime(DateTime.Now.AddDays(-7));
            DateTime endDate = Convert.ToDateTime(DateTime.Now);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            DataTable dtUser = code.GetDataTableUser(true, null, "N");
            Lup_Emp.smtLookUpDataSource = dtUser;
            Lup_Emp.smtLookUpDataDisplayMember = "명칭";
            Lup_Emp.smtLookUpDataValueMember = "코드";
            Lup_Emp.smtLookUpValue = "ALL";
        }

        private void Set_GridView(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Column 생성
            grid.Create_GvColumn(gv, "사용자ID", "ID");
            grid.Create_GvColumn(gv, "사용자명", "EmployeeName");
            grid.Create_GvColumn(gv, "로그인 일시", "Login_Time");
            grid.Create_GvColumn(gv, "로그아웃 일시", "Logout_Time");
            grid.Create_GvColumn(gv, "로그인 IP", "Login_IP");

            //Format 설정
            gv.Columns["Login_Time"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gv.Columns["Login_Time"].DisplayFormat.FormatString = "yyyy-MM-dd tt h:mm:ss";

            gv.Columns["Logout_Time"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gv.Columns["Logout_Time"].DisplayFormat.FormatString = "yyyy-MM-dd tt h:mm:ss";

            grid.RowNumber_Indicator(gv); //행번호 표기
        }


        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Display();
        }

        private void Display()
        {
            DateTime startDate = Dt_Date.smtStartDateTimeValue;
            DateTime endDate = Dt_Date.smtEndDateTimeValue;

            SM_LoginHistory sm = new SM_LoginHistory();
            sm.ID = Lup_Emp.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_Login.DataSource = sm.Select_SM_LoginHistory(startDate, endDate);

            Gv_Login.BestFitColumns();
        }

		private void btn_Close_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}