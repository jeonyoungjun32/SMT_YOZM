using DevExpress.CodeParser;
using DevExpress.DashboardCommon;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using SmartCommon.Common;
using SmartMES.Models.DTL;
using SmartMES.Models.PM;
using SmartMES.Models.SM;
using SmartMES.View.BM;
using SmartMES.View.EQ;
using SmartMES.View.MM;
using SmartMES.View.PM;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.DTL
{
    public partial class Frm_DT_LogView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_DT_LogView()
        {
            InitializeComponent();
        }

        private void Frm_DT_DTLLogView_Load(object sender, EventArgs e)
        {
            Init();
            SetGrid_Work(Gv_DTLLog);

            Search();
        }

        private void Init()
		{
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-1);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            LupLogType.Visible = false;
        }

        #region 기본 설정 (Grid)
        private void SetGrid_Work(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "발생시각", "OccurDate");
            grid.Create_GvColumn(gv, "로그유형", "LogType");
            grid.Create_GvColumn(gv, "작업명", "JobName");
            grid.Create_GvColumn(gv, "메시지", "LogMsg");
            grid.Create_GvColumn(gv, "처리건수", "ProcessCnt");
            grid.Create_GvColumn(gv, "소요시간(초)", "TotalSec");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["ProcessCnt"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalSec"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OccurDate"], clsGrid.TypeList.TDateTime);

            gv.Columns["LogType"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
            //gv.CustomDrawCell += gridView1_CustomDrawCell;
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string value = e.CellValue.ConvertString_Null_To_Empty();

            if (e.Column.FieldName == "LogType") //
            {
				if (value == "종료")
                {
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
                }
                else if (value == "오류")
                {
                    e.Appearance.ForeColor = Color.FromArgb(200, 50, 50);    // 진한 빨간색
                }
				else
				{
					e.Appearance.ForeColor = Color.FromArgb(100, 110, 120); // 진한 회색
				}
            }
        }

        #endregion

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        #region 데이터 조회 (Search)
        private void Search()
        {
            // 설비별 생산 현황
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            DTL_Log dl = new DTL_Log();
            dl.LogType = LupLogType.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_DTLLog.DataSource = dl.Select_DTL_Log(startDate, endDate);

            Gv_DTLLog.BestFitColumns();
        }
        #endregion

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(Gv_DTLLog);
        }

        private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
        }
	}
}