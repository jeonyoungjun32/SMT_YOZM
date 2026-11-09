using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartMES.Common;
using SmartMES.Models;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.View.Analysis
{
    public partial class Frm_AN_ProductMachine : DevExpress.XtraEditors.XtraForm
    {
        string query = "";
        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();

        Database db = new Database();

        public Frm_AN_ProductMachine()
        {
            InitializeComponent();
        }

        private void Frm_AN_ProductMachine_Day_Load(object sender, EventArgs e)
        {
            //
            barDate.smtStartDateTimeValue = DateTime.Now;

            Set_LupProc();
            Set_LupLine();

            Set_Grid(Gv_List);
            Set_Chart();

            // 조회
            DisplayGrid();
            DisplayChart();
        }

        #region 디자인

        private void Set_LupProc()
        { 
            LupProcess.smtLookUpDataSource = code.GetProcess_List(true);
            LupProcess.smtLookUpDataDisplayMember = "공정명";
            LupProcess.smtLookUpDataValueMember = "공정코드";

            LupProcess.smtLookUpValue = "ALL";
        }

        private void Set_LupLine()
        {
            string proc = LupProcess.smtLookUpValue?.ToString();

            if (proc == null || proc == "ALL") proc = "";

            LupLine.smtLookUpDataSource = code.GetLine_List(proc, true);
            LupLine.smtLookUpDataDisplayMember = "라인명";
            LupLine.smtLookUpDataValueMember = "라인코드";

            LupLine.smtLookUpValue = "ALL";
        }

        private void Set_Grid(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            grid.Create_GvColumn(gv, "구분", "구분");
            grid.Create_GvColumn(gv, "1월", "1월");
            grid.Create_GvColumn(gv, "2월", "2월");
            grid.Create_GvColumn(gv, "3월", "3월");
            grid.Create_GvColumn(gv, "4월", "4월");
            grid.Create_GvColumn(gv, "5월", "5월");
            grid.Create_GvColumn(gv, "6월", "6월");
            grid.Create_GvColumn(gv, "7월", "7월");
            grid.Create_GvColumn(gv, "8월", "8월");
            grid.Create_GvColumn(gv, "9월", "9월");
            grid.Create_GvColumn(gv, "10월", "10월");
            grid.Create_GvColumn(gv, "11월", "11월");
            grid.Create_GvColumn(gv, "12월", "12월");
            grid.Create_GvColumn(gv, "합계", "합계");

            //
            grid.SetCol_Type(gv.Columns["1월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["2월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["3월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["4월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["5월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["6월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["7월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["8월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["9월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["10월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["11월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["12월"], clsGrid.TypeList.TFloatN2);
            grid.SetCol_Type(gv.Columns["합계"], clsGrid.TypeList.TFloatN2);

            gv.CustomColumnDisplayText += Gv_CustomColumnDisplayText;
        }

        private void Set_Chart()
        {
            Series series1 = new Series("PlanQty", ViewType.Bar);       // 계획량 시리즈
            series1.ArgumentDataMember = "MM";
            series1.ValueDataMembers.AddRange("계획량");
            series1.LegendTextPattern = "계획량";

            Series series2 = new Series("Qty", ViewType.Bar);       // 실적량 시리즈
            series2.ArgumentDataMember = "MM";
            series2.ValueDataMembers.AddRange("생산량");
            series2.LegendTextPattern = "생산량";

            // 시리즈 추가
            chartProduct.Series.AddRange(new Series[] { series1, series2 });
        }
        #endregion

        #region 조회

        private void DisplayGrid()
        {
            this.Validate();

            string yyyy = barDate.smtStartDateTimeValue.ToString("yyyy");
            string proc = LupProcess.smtLookUpValue?.ToString();
            string line = LupLine.smtLookUpValue?.ToString();

            if (proc == null) proc = "";
            if (line == null) line = "";

            query = $@"EXEC Analysis_MonthProduct '{WrGlobal.CorpID}', '{yyyy}', '{proc}', '{line}'";

            Gc_List.DataSource = db.GetDataTable(query);
            Gv_List.BestFitColumns();
        }

        private void DisplayChart()
        {
            this.Validate();

            string yyyy = barDate.smtStartDateTimeValue.ToString("yyyy");
            string proc = LupProcess.smtLookUpValue?.ToString();
            string line = LupLine.smtLookUpValue?.ToString();

            if (proc == null) proc = "";
            if (line == null) line = "";

            query = $@"EXEC Analysis_MonthProduct2 '{WrGlobal.CorpID}', '{yyyy}', '{proc}', '{line}'";

            chartProduct.DataSource = db.GetDataTable(query);
        }
        #endregion

        #region 이벤트
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Gv_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName != "구분")
            {
                // 값이 null이면 0으로 표시
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.DisplayText = "0";
                }

                if (e.Value.ToString().Length >= 3)
                {
                    //e.DisplayText = e.Value.ToString().Substring(0, e.Value.ToString().IndexOf(".") + 3);
                }
            }
        }

        private void BtnSearch_Rst_Click(object sender, EventArgs e)
        {
            this.Validate();
            DisplayGrid();
            DisplayChart();
        }

        private void LupProc_EditValueChanged(object sender, EventArgs e)
        {
            Set_LupLine();
        }
        #endregion

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Gv_List.ExportToXls(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }
        }
    }
}