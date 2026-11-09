using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.QM;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using SmartCommon.Common;

namespace SmartMES.View.Analysis
{
	public partial class Frm_AN_BadType : DevExpress.XtraEditors.XtraForm
    {
        //불량 유형별 차트
        string OpenType = ""; //Form조회 타입(Day : 일별, Month : 월별)

        DataTable dtChart = null;
        clsCode code = new clsCode();

        public Frm_AN_BadType()
        {
            InitializeComponent();
        }

        private void Frm_AN_Progress_Day_Load(object sender, EventArgs e)
        {
            SetDesign();
            DisplayChart();
        }

        private void SetDesign()
        {
            barDate.smtStartDateTimeValue = DateTime.Now;

            Set_SearchLup();
        }

        //SerchLookUp 설정
        private void Set_SearchLup()
        {
            CM_Process process = new CM_Process();

            //라인
            LupLine.Properties.DataSource = code.GetLine_List("");
            LupLine.Properties.DisplayMember = "명칭";
            LupLine.Properties.ValueMember = "코드";

            //공정
            LupProcess.smtLookUpDataSource= process.SelectCM_ProcessLookup();
            LupProcess.smtLookUpDataDisplayMember= "공정명";
            LupProcess.smtLookUpDataValueMember= "코드";

            LupLine.EditValue = "ALL";
            LupProcess.smtLookUpValue = "ALL";
        }

        private void BtnSearch_Rst_Click(object sender, EventArgs e)
        {
            DisplayChart();
        }

        private void DisplayChart()
        {
            this.Validate();

            QM_WorkDefect fl = new QM_WorkDefect();

            //조회조건이 없으면 조회가 안되도록
            string yyyyMM = barDate.smtStartDateTimeValue.ToString("yyyyMM");
            string lineCode = LupLine.EditValue.ConvertString_Null_To_Empty();
            string processCode = LupProcess.smtLookUpValue.ConvertString_Null_To_Empty();

            if (yyyyMM == "" || lineCode == "" || processCode == "")
            {
                MessageBox.Show("조회조건을 모두 선택해주세요.");
                return;
            }

            //일자 설정

            //chartWeekNG.DataSource = null;

            //일별불량수량
            dtChart = fl.SetChartBadType(yyyyMM, processCode, lineCode);
            SetChartDataQtyDay(chartWeekNG, dtChart);
        }

        private void SetChartDataQtyDay(ChartControl chart, DataTable dt)
        {
            chart.DataSource = dt;

            //생성한 Series 리스트 추출
            DataTable dtTypes = dt.DefaultView.ToTable(true, "Title");
            chart.Series.Clear();
            chart.Series.BeginUpdate();

            if (dt.Rows.Count == 0)
            {
                string seriesName = Convert.ToString(dtTypes.Columns["Title"]);

                Series series = new Series(seriesName, ViewType.Bar);

                string argu = "불량유형";
                string value = "0";

                series.Points.Add(new SeriesPoint(argu, value));
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                chart.Series.Add(series);
            }

            //Series 생성
            foreach (DataRow dataRow in dtTypes.Rows)
            {
                string seriesName = Convert.ToString(dataRow["Title"]);

                Series series = new Series(seriesName, ViewType.Bar);

                //값이 보일 Point 생성
                foreach (DataRow drValue in dt.Select($"Title = '{seriesName}'"))
                {
                    string argu = Convert.ToString(drValue["Title"]) == "" ? "1" : Convert.ToString(drValue["Title"]);    //X축 값
                    string value = Convert.ToString(drValue["Bad_Qty"]);    //Y축 값

                    series.Points.Add(new SeriesPoint(argu, value));
                }

                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True; // 반드시 True로 설정
                SideBySideBarSeriesLabel label = series.Label as SideBySideBarSeriesLabel;
                if (label != null)
                {
                    label.Position = BarSeriesLabelPosition.Top;
                    label.TextPattern = "{V:#,0}";    // 막대 위 값 표시
                    label.Font = new Font("맑은 고딕", 9F, FontStyle.Bold);
                    label.TextColor = Color.Black;
                }

                chart.Series.Add(series);
                chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            }

            chart.Series.EndUpdate();
            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Top;

            XYDiagram diagram = chart.Diagram as XYDiagram;

            chart.AnimationStartMode = ChartAnimationMode.OnLoad;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}