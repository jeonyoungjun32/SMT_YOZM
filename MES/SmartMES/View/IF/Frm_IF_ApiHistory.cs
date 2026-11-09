using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.IF;
using System;
using System.Data;

namespace SmartMES.View.IF
{
    public partial class Frm_IF_ApiHistory : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();

        public Frm_IF_ApiHistory()
        {
            InitializeComponent();
        }

        private void Frm_IF_ApiHistory_Load(object sender, EventArgs e)
        {
            Set_SearchLup();
            Set_GridView(Gv_API_History);
            Search();
        }

        #region 디자인 아이템 생성

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
            Lup_Emp.smtLookUpValue = "";
        }

        #region GridView Setting

        private void Set_GridView(GridView gv)
        {
            //GvBOM Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            //Column 생성
            grid.Create_GvColumn(gv, "API 인증키", "crtfcKey");
            grid.Create_GvColumn(gv, "사용구분", "useSe");
            grid.Create_GvColumn(gv, "사용자", "sysUser");
            grid.Create_GvColumn(gv, "수신결과", "SendResulrt");
            grid.Create_GvColumn(gv, "데이터사용량", "dataUsgqty");
            grid.Create_GvColumn(gv, "접속IP", "conectIp");
            grid.Create_GvColumn(gv, "로그일시", "logDt");

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["logDt"], clsGrid.TypeList.TDateTime);        //수주일자

            /*
            grid.Column_AllowEdit(gv.Columns["Emp_No"], false);
            grid.Column_AllowEdit(gv.Columns["Emp_Nm"], false);
            grid.Column_AllowEdit(gv.Columns["Dept_Cd"], false);
            grid.Column_AllowEdit(gv.Columns["Dept_Nm"], false);
            */

            //조회조건 라벨 설정
            Lbl_Emp_No.Text = gv.Columns["sysUser"].Caption;

            //Summary
            gv.OptionsView.ShowFooter = true;
            grid.Set_Navigator(gv);
            grid.RowNumber_Indicator(gv);

        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Search

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            ////api 전송 이력 조회
            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char8();

            string emp_No = Txt_Emp_No.Text;
            string emp_Nm = Lup_Emp.smtLookUpText.ConvertString_Null_To_Empty();

            IF_ApiHistory api = new IF_ApiHistory();
            api.Emp_No = emp_No;

            DataTable dt = api.Select_API_History(startDate, endDate, emp_Nm);

            Gc_API_History.DataSource = dt;

            Gv_API_History.BestFitColumns();
        }

		#endregion Search End

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}