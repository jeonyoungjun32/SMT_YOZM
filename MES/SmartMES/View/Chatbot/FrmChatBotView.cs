using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using SmartCommon.Common;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using SmartMES.Models.PM;
using System.Data;
using SmartMES.Models.ChatBot;

namespace SmartMES.View.Chatbot
{
    public partial class FrmChatBotView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();


        public FrmChatBotView()
        {
            InitializeComponent();
        }

        private void frmPM_ProductPlanView_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();

            //셀 수정 가능 여부
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in GvPM_ProductPlan.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }
            DisplayData();
        }

        #region setDesign
        private void SetLup()
        {
            DtOrder.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            DtOrder.smtEndDateTimeValue = DateTime.Now;

            Lup_Emp.smtLookUpDataSource = code.smtLookUp("사용자", "", "");
            Lup_Emp.smtLookUpDataDisplayMember = "사원명";
            Lup_Emp.smtLookUpDataValueMember = "사원ID";
            Lup_Emp.smtLookUpValue = "ALL";
        }

        private void SetGridView()
		{
            SetGridPM_ProductPlan(GvPM_ProductPlan);
		}

        private void SetGridPM_ProductPlan(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            RepositoryItemSearchLookUpEdit lup_UserId = rpo.Create_RepositoryItemSearchLookUp(code.Get_UserList(), "코드", "코드명");


            grid.Create_GvColumn(gv, "No", "RowNo");
            grid.Create_GvColumn(gv, "사용자", "EmployeeId", lup_UserId);
			grid.Create_GvColumn(gv, "대화시작일시", "FirstMsgDt");
			grid.Create_GvColumn(gv, "대화제목", "");
            grid.Create_GvColumn(gv, "메시지수", "MessageCnt");
            grid.Create_GvColumn(gv, "최근메시지일시", "LastMsgDt");
            grid.Create_GvColumn(gv, "상태", "");
            grid.Create_GvColumn(gv, "상세", "");

            gv.Columns["LastMsgDt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gv.Columns["LastMsgDt"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            gv.Columns["FirstMsgDt"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gv.Columns["FirstMsgDt"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            // 포맷
            //grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            //grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);


            grid.Set_Navigator(GvPM_ProductPlan);
            gv.CustomDrawCell += gv_CustomDrawCell;
        }

        private void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("UrgentYn") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "긴급")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.Transparent;
            }
        }

        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            baseSMT.smtSearchButton Btn = (baseSMT.smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                //조회
                case "cmdDisplay":
                    DisplayData();
                    break;

                //엑셀다운
                case "cmdExcel":
                    cmdExcel_Click();
                    break;
            }
        }

        #region Event
        //엑셀변환 하기
        private void cmdExcel_Click()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GvPM_ProductPlan.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Event

        #region 조회
        private void DisplayData()
        {
            ChatBot_History chat = new ChatBot_History();
            string startDate = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char8();
            string endDate = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char8();
            chat.EmployeeId = Lup_Emp.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Emp.smtLookUpValue;
            DataTable dt = chat.SearchChatbotHistory(startDate, endDate);
            GcPM_ProductPlan.DataSource = dt;

            GvPM_ProductPlan.BestFitColumns();
        }
        #endregion 조회
	}
}