using DevExpress.CodeParser;
using DevExpress.DashboardCommon;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Ocsp;
using DevExpress.Schedule;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using SmartCommon.Common;
using SmartMES.Models.DTL;
using SmartMES.Models.EQ;
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
    public partial class Frm_DT_DTLOrderResult : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_DT_DTLOrderResult()
        {
            InitializeComponent();
        }

        private void Frm_DT_DTLLogView_Load(object sender, EventArgs e)
        {
            Init();
            SetGrid_Work(Gv_DTLLog);

            SetCard();
            Search();
        }

        private void Init()
		{
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-1);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            //LupLogType.Visible = false;
        }

        #region 기본 설정 (Grid)
        private void SetGrid_Work(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.OptionsView.ColumnAutoWidth = false;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            //
            DataTable dt = new DataTable();
            dt.Columns.Add("코드");
            dt.Columns.Add("명칭");
            dt.Rows.Add(new object[] {"Y","사용" });
            dt.Rows.Add(new object[] {"N","미사용" });

            RepositoryItemLookUpEdit lupUseYn = rpo.Create_RepositoryItem(dt, "코드", "명칭");

            grid.Create_GvColumn(gv, "사업장코드", "CorpName");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "라인명", "LineName");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "생산일자", "WorkDate");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");
            grid.Create_GvColumn(gv, "수집구분", "CollectType");
            grid.Create_GvColumn(gv, "수집상태", "CollectStatus");
            grid.Create_GvColumn(gv, "사용여부", "UseYn", lupUseYn);
            grid.Create_GvColumn(gv, "수집일자", "CreDt");


            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["CreDt"], clsGrid.TypeList.TDateTime);

            gv.Columns["CorpName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["CollectType"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["CollectStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["UseYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
            //gv.CustomDrawCell += gridView1_CustomDrawCell;
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string value = e.CellValue.ConvertString_Null_To_Empty();

            if (e.Column.FieldName == "CollectStatus") //
            {
				if (value == "정상")
                {
                    e.Appearance.BackColor = Color.FromArgb(220, 245, 230); // 연한 초록색
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
                }
                else if (value == "오류")
                {
                    e.Appearance.BackColor = Color.FromArgb(253, 232, 232); // 연한 분홍/파스텔 레드
                    e.Appearance.ForeColor = Color.FromArgb(200, 50, 50);    // 진한 빨간색
                }
				else
				{
					e.Appearance.ForeColor = Color.FromArgb(100, 110, 120); // 진한 회색
				}
            }
            
            if (e.Column.FieldName == "UseYn") //
            {
                if (value == "Y")
                {
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
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
            SetCard();
            Search();
        }

        #region 데이터 조회 (Search)
        private void SetCard()
		{
            EQ_Machine ma = new EQ_Machine();
            DataTable dt = ma.Select_LupMachine();

            LblMcCnt.Text = dt.Rows.Count.ToString() + " 대";
            LblMcTLCnt.Text = dt.Rows.Count.ToString() + " 대"; // ?
            LblNormalCnt.Text = dt.Rows.Count.ToString() + " 대"; // ?
		}

        private void Search()
        {
            // 설비별 생산 현황
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            DTL_OrderResult odr = new DTL_OrderResult();

            Gc_DTLLog.DataSource = odr.Select_DTL_OrderResult(startDate, endDate);

            Gv_DTLLog.BestFitColumns();
        }
        #endregion

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            DTL_OrderResult odr = new DTL_OrderResult();

			if (odr.Ins_Select())
			{
                XtraMessageBox.Show("수동 업데이트 완료");
                Search();
			}
			else
			{
                XtraMessageBox.Show("수동 업데이트 실패");
			}
        }

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