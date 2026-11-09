using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

using System;
using SmartMES.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using SmartMES.Models;
using SmartMES.Report;
using DateTime = System.DateTime;
using TimeSpan = System.TimeSpan;
using SmartMES.Models.CM;
using SmartMES.Models.PM;
using SmartMES.Common;
using SmartMES.Model.SM;
using SmartCommon.Common;

namespace SmartMES.View.Analysis
{   

    public partial class Frm_AN_Work_LineView : DevExpress.XtraEditors.XtraForm
    {
        DataTable dtChart = null;
        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();

        public Frm_AN_Work_LineView()
        {
            //OpenType = type;
            InitializeComponent();
            SetDesign();
            Set_GridView(Gv_NonWork_Class);
            Search();
        }

        public Frm_AN_Work_LineView(string rLine, string rProcess, string rMachine)
        {
            InitializeComponent();
            //SetDesign();

            try
            {
                //LupLine.EditValue = rLine;
                //LupProcess.EditValue = rProcess;
                //LupMachine.EditValue = rMachine;

            }
            catch (Exception e)
            {
            }

        }

        private void SetDesign()
        {
            CM_Line line = new CM_Line();
            CM_Process process = new CM_Process();

            DataTable dt_line = line.Select_Lup_Line(true);
            DataTable dt_process = process.SelectCM_ProcessLookup();

            cls_Lookup clsLup = new cls_Lookup();

            clsLup.Create_RepositoryItemSearchLookUp(Lup_Line, dt_line, "코드", "명칭");
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Process, dt_process, "코드", "공정명");

            Lup_Line.smtLookUpValue = "ALL";
            Lup_Process.smtLookUpValue = "ALL";
            Dt_Date.smtStartDateTimeValue = DateTime.Now;

            //RstMonth.Properties.DisplayFormat.FormatString = "yyyy년MM월";
            //RstMonth.Properties.Mask.EditMask = "yyyy-MM";
            //RstMonth.Properties.Mask.UseMaskAsDisplayFormat = true;
            //Dt_Date.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
        }

        private void Set_GridView(GridView gv)
        {
            clsRepository rpo = new clsRepository();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.IndicatorWidth = 50;                     //순번 너비

            CM_Line line = new CM_Line();
            CM_Process process = new CM_Process();

            //BS_NonWork_Class nonWork = new BS_NonWork_Class();


            RepositoryItemDateEdit lup_NonWork_Date = rpo.Create_rpoDate();
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(line.Select_Lup_Line(true), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(process.SelectCM_ProcessLookup(), "코드", "공정명");
            RepositoryItemSearchLookUpEdit lup_MachineCode = rpo.Create_RepositoryItemSearchLookUp(code.SelectEQ_MachineLookup(), "코드", "설비명");
            //RepositoryItemSearchLookUpEdit lup_nonWork_Cd = rpo.Create_RepositoryItemSearchLookUp(nonWork.Select_NonWork_Lup(), "코드", "명칭");

            //SC_CmCd cmCd = new SC_CmCd();
            //cmCd.Cm_Class = "029";
            //RepositoryItemLookUpEdit lup_NonWork = rpo.Create_RepositoryItem(cmCd.Select_CmCd(), "Cm_Cd", "Cm_Nm");

            //개발작업리스트 2030803
            //가동율 = (부하시간-정지시간)/부하시간*100
            //조업시간 = 일별 공정실적 시작시간 - 종료시간의 SUM : 분으로 계산
            //계획정지시간 = 비가동유형 비가동구분이 계획정지인 시간의 합
            //부하시간 = (조업시간 - 계획정지시간)
            //정지시간 = 일별 비가동 정지시간의 합

            grid.Create_GvColumn(gv, "일자", "WorkDay");
            grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lup_MachineCode);
            grid.Create_GvColumn(gv, "조업시간", "조업시간");
            grid.Create_GvColumn(gv, "계획정지시간", "계획정지시간");
            grid.Create_GvColumn(gv, "부하시간", "부하시간");
            grid.Create_GvColumn(gv, "정지시간(분)", "정지시간");
            grid.Create_GvColumn(gv, "시간가동률", "시간가동률");

            //gv.Columns["Remark"].MinWidth = 200;

            //Repository
            RepositoryItemTextEdit textEdit = new RepositoryItemTextEdit();
            textEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            textEdit.DisplayFormat.FormatString = "90:00";

            textEdit.Mask.UseMaskAsDisplayFormat = true;
            textEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            textEdit.Mask.EditMask = "90:00";
            textEdit.Mask.SaveLiteral = false;
            textEdit.Mask.ShowPlaceHolders = false;

            //gv.Columns["Start_Time"].ColumnEdit = textEdit;
            //gv.Columns["End_Time"].ColumnEdit = textEdit;

            //셀 수정 가능여부
            foreach (GridColumn col in Gv_NonWork_Class.Columns) { grid.Column_AllowEdit(col, false); }


            //값 형태
            //grid.SetCol_Type(gv.Columns["Stop_Time"], clsGrid.TypeList.TFloat);

            //이벤트
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            //gv.CustomRowCellEditForEditing += Gv_CustomRowCellEditForEditing;
        }

        private void Search()
        {
            DateTime date = Dt_Date.smtStartDateTimeValue;

            string fromDate = date.AddDays(1 - date.Day).Date.ToString("yyyyMMdd");
            string toDate = date.AddDays(1 - date.Day).Date.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");


            string lineCd = Lup_Line.smtLookUpValue.ToString().Replace("ALL", "");
            string processCd = Lup_Process.smtLookUpValue.ToString().Replace("ALL", "");

            MachineInfo info = new MachineInfo(lineCd, processCd);

            PM_OrderResult PM_OrderResult = new PM_OrderResult();
            DataTable dt = PM_OrderResult.Select_NonWork(fromDate, toDate, info);
            Gc_NonWork_Class.DataSource = dt;
            dtChart = dt;

            Gv_NonWork_Class.BestFitColumns();

            SetChartDataQtyDay(BarChart_Nonwork, dtChart);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSearch_Rst_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void SetChartDataQtyDay(ChartControl chart, DataTable dt)
        {
            //완료보고서용으로 만들어서 쿼리 반드시 수정 필요
            chart.DataSource = dt;


            //생성한 Series 리스트 추출
            DataTable dtTypes = dt.DefaultView.ToTable(true, "LineName");
            chart.Series.Clear();
            chart.Series.BeginUpdate();

            if (dt.Rows.Count == 0)
            {
                string seriesName = Convert.ToString(dtTypes.Columns["LineName"]);

                Series series = new Series(seriesName, ViewType.Bar);

                string argu = " ";
                string value = "0";

                series.Points.Add(new SeriesPoint(argu, value));
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.Series.Add(series);
            }

            //Series 생성
            foreach (DataRow dataRow in dtTypes.Rows)
            {
                string seriesName = Convert.ToString(dataRow["LineName"]);

                Series series = new Series(seriesName, ViewType.Bar);

                //값이 보일 Point 생성
                foreach (DataRow drValue in dt.Select($"LineName = '{seriesName}'"))
                {
                    string argu = Convert.ToString(drValue["MachineName"]) == "" ? "1" : Convert.ToString(drValue["MachineName"]);    //X축 값
                    string value = Convert.ToString(drValue["시간가동률"]) == "" ? "1" : Convert.ToString(drValue["시간가동률"]); ;   //Y축 값

                    series.Points.Add(new SeriesPoint(argu, value));
                }

                (series.Label as SideBySideBarSeriesLabel).Position = BarSeriesLabelPosition.Top;

                chart.Series.Add(series);
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }

            chart.Series.EndUpdate();
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Top;

            XYDiagram diagram = chart.Diagram as XYDiagram;

            // X축 설정
            diagram.AxisX.Title.Text = "설비명";
            diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            diagram.AxisX.Title.Font = new Font("맑은 고딕", 12, FontStyle.Regular);
            // Y축 설정
            diagram.AxisY.Title.Text = "가동율";
            diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            diagram.AxisY.Title.Font = new Font("맑은 고딕", 12, FontStyle.Regular);
        }

        public class MachineInfo
        {
            public string Line_Cd { get; set; }
            public string ProcessCode { get; set; }
            public string MachineCode { get; set; }

            public MachineInfo(string lineCd, string processCd)
            {
                Line_Cd = lineCd;
                ProcessCode = processCd;
            }
        }

        //private void Gv_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        //{
        //    GridView gv = sender as GridView;

        //    //Grid데이터가 없으면 스킵
        //    if (e.RowHandle < 0) return;
        //    //현재행 != 수정하려는 행일 시 스킵
        //    if (e.RowHandle != gv.FocusedRowHandle) return;

        //    clsRepository rpo = new clsRepository();

        //    string fieldName = e.Column.FieldName;

        //    DataRow dr = gv.GetDataRow(e.RowHandle);

        //    string line_Cd = dr["Line_Cd"].ConvertString_Null_To_Empty();
        //    string ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();

        //    switch (fieldName)
        //    {
        //        case "ProcessCode":
        //            if (line_Cd != "")
        //            {
        //                procLine.Line_Cd = line_Cd;
        //                DataTable dt_process = procLine.SearchLookUp_Select_ProcLine();

        //                e.RepositoryItem = rpo.Create_RepositoryItemSearchLookUp(dt_process, "코드", "명칭");
        //            }

        //            break;

        //        case "MachineCode":

        //            if (ProcessCode != "")
        //            {
        //                machineProc.ProcessCode = ProcessCode;
        //                DataTable dt_machine = machineProc.SearchLookUp_Select_MachineProc();

        //                e.RepositoryItem = rpo.Create_RepositoryItemSearchLookUp(dt_machine, "코드", "명칭");
        //            }

        //            break;
        //    }
        //}
    }
}