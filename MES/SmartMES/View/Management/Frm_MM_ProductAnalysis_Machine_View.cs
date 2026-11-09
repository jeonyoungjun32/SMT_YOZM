using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Model.Management;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Management
{
	public partial class Frm_MM_ProductAnalysis_Machine_View : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        Database db = new Database();

        public Frm_MM_ProductAnalysis_Machine_View()
        {
            InitializeComponent();
        }

        private void Frm_MM_ProductAnalysis_Machine_View_load(object sender, EventArgs e)
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now;
            //DtpDate.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;

            Set_SearchLup();
            Set_Grid(Gv_Product);

            //엑셀다운(GV에 보이는 형식 그대로 저장
            //Btn_ExcelDown_ALL.DownGridControl = this.Gc_Product;
            //Btn_ExcelDown_withChart.gridControl = this.Gc_Product;
            //Btn_ExcelDown_withChart.chartControl = this.barChart1;
            Search();
        }

        private void Set_SearchLup()//Lup 작업
        {
            //DataTable dtMachine = new EQ_Machine().Select_Machine_List(null,true); //설비
            //DataTable dtLine = new BS_Line().Select_Line_CdNm(true); //라인
            //DataTable dtProc = new BS_ProcLine().SearchLookUp_Select_ProcLine_NEW(true); //공정

            clsCode code = new clsCode();
            DataTable dtMachine = code.GetMachineList(true);
            //CM_Line line = new CM_Line();
            //DataTable dtLine = line.Select_Lup_Line();

            CM_Process process = new CM_Process();
            DataTable dtProc = process.SelectCM_ProcessLookup();

			cls_Lookup clsLup = new cls_Lookup();
			clsLup.Create_RepositoryItemSearchLookUp(Lup_Machine, dtMachine, "설비코드", "설비명");
			//clsLup.Create_RepositoryItemSearchLookUp(LupLine, dtLine, "코드", "명칭");
			clsLup.Create_RepositoryItemSearchLookUp(Lup_Process, dtProc, "코드", "공정명");

			Lup_Machine.smtLookUpValue = "ALL";
            //LupLine.EditValue = "ALL";
            Lup_Process.smtLookUpValue = "ALL";

            //그리드 사이즈 조절
            //LupMachine.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
        }

        private void Set_Grid(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.OptionsView.ColumnAutoWidth = false;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.IndicatorWidth = 50;                     //순번 너비

            grid.Create_GvColumn(gv, "설비", "MachineName");
            //grid.Create_GvColumn(gv, "라인", "LineName");
            //grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "총지시수량", "TotalOrderQty");
            grid.Create_GvColumn(gv, "총생산수량", "TotalWorkQty");
            grid.Create_GvColumn(gv, "달성률 (%)", "달성률");
            grid.Create_GvColumn(gv, "가동시간", "가동시간");
            grid.Create_GvColumn(gv, "비가동시간", "정지시간");
            grid.Create_GvColumn(gv, "가동률 (%)", "가동률");
            grid.Create_GvColumn(gv, "양품수량", "TotalGoodQty");
            grid.Create_GvColumn(gv, "불량수량", "TotalNgQty");
            grid.Create_GvColumn(gv, "불량률 (%)", "불량률");

            //포맷
            grid.SetCol_Type(gv.Columns["TotalOrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalWorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalGoodQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalNgQty"], clsGrid.TypeList.TInt);

            grid.SetCol_Type(gv.Columns["달성률"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["불량률"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["가동률"], clsGrid.TypeList.TFloat);

            grid.SetCol_Type(gv.Columns["가동시간"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["정지시간"], clsGrid.TypeList.TFloat);

            //칼럼 색상
            gv.Columns["달성률"].AppearanceCell.ForeColor = Color.Red;
            gv.Columns["불량률"].AppearanceCell.ForeColor = Color.Red;
            gv.Columns["가동률"].AppearanceCell.ForeColor = Color.Red;

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            //이벤트
            grid.RowNumber_Indicator(gv);     //행번호 표기
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            switch(btn.Name)
            {
                case "cmdDisplay":
                    Search();
                    break;

                case "cmdClose":
                    this.Close();
                    break;
            }
        }

        private void Search()
        {
            //string line_Cd = LupLine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupLine.EditValue.ConvertString_Null_To_Empty();
            string ProcessCode = Lup_Process.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Process.smtLookUpValue.ConvertString_Null_To_Empty();
            string MachineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
            string fromdate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-01");
            string todate = Dt_Date.smtStartDateTimeValue.AddMonths(1).ToString("yyyy-MM-01");

			MM_ProductAnalysis bad_Chk = new MM_ProductAnalysis();
			//bad_Chk.Line_Cd = line_Cd;
			bad_Chk.ProcessCode = ProcessCode;
			bad_Chk.MachineCode = MachineCode;

			//그리드 데이터
			DataTable dt = bad_Chk.Product_Machine_View(fromdate, todate);
			Gc_Product.DataSource = dt;

			//차트

			SetChart1(barChart1, dt);    //왼쪽상단 바차트
			SetChart2(barChart2, dt);    //왼쪽하단 바차트
			SetChart3(pointChart1, dt);     //우측상단 점차트

			SetChart4(pointChart2, dt);  //가동률

			Gv_Product.BestFitColumns();
        }

        private void SetChart1(ChartControl chart, DataTable dt)
        {
            try
            {
                if(dt == null) { chart.Series.Clear(); return; }
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                // 각 불량 유형별로 시리즈 생성
                Series series1 = new Series("총계획수량", ViewType.Bar);
                Series series2 = new Series("총생산수량", ViewType.Bar);

                // 해당 시리즈에 데이터 포인트 추가
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if(dt.Rows[i]["MachineName"].ToString() != "")
                    {
                        //설비명이 비어있으면 안나오도록
                        string strAxis = dt.Rows[i]["MachineName"].ToString() == "" ? "" : dt.Rows[i]["MachineName"].ToString();
                        float value1 = dt.Rows[i]["TotalOrderQty"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["TotalOrderQty"].ToString());
                        float value2 = dt.Rows[i]["TotalWorkQty"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["TotalWorkQty"].ToString());
                        series1.Points.Add(new SeriesPoint(strAxis, value1));
                        series2.Points.Add(new SeriesPoint(strAxis, value2));

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series1);
                        seriesList.Add(series2);
                        //series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                    }
                }

                // 포인트 라벨 설정
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series1.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정
                series2.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정


                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가

                chart.Series.Add(series1);
                chart.Series.Add(series2);
                // X축 및 Y축 설정
                XYDiagram diagram = chart.Diagram as XYDiagram;
                //diagram.AxisX.Label.TextPattern = "{A:dd}";
                //diagram.AxisX.WholeRange.MinValue = 1;
                //diagram.AxisX.VisualRange.MinValue = 15;
                //diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;

                //시리즈 색깔
                chart.Series["총계획수량"].View.Color = Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(192)))), ((int)(((byte)(247)))));
                chart.Series["총생산수량"].View.Color = Color.Red;

                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "설비별 생산현황", Font = new Font(Font.FontFamily.Name, 11, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }
        private void SetChart2(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt == null) { chart.Series.Clear(); return; }
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                // 각 불량 유형별로 시리즈 생성
                Series series1 = new Series("가동시간", ViewType.Bar);
                Series series2 = new Series("비가동시간", ViewType.Bar);

                // 해당 시리즈에 데이터 포인트 추가
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MachineName"].ToString() != "")
                    {
                        //설비명이 비어있으면 안나오도록
                        string strAxis = dt.Rows[i]["MachineName"].ToString() == "" ? "" : dt.Rows[i]["MachineName"].ToString();
                        float value1 = dt.Rows[i]["가동시간"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["가동시간"].ToString());
                        float value2 = dt.Rows[i]["정지시간"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["정지시간"].ToString());
                        series1.Points.Add(new SeriesPoint(strAxis, value1));
                        series2.Points.Add(new SeriesPoint(strAxis, value2));

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series1);
                        seriesList.Add(series2);
                        //series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                    }
                }

                // 포인트 라벨 설정
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series1.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정
                series2.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가
                chart.Series.Add(series1);
                chart.Series.Add(series2);

                //시리즈 색깔
                chart.Series["가동시간"].View.Color = Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(192)))), ((int)(((byte)(247)))));
                chart.Series["비가동시간"].View.Color = Color.Red;

                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "설비별 생산효율", Font = new Font(Font.FontFamily.Name, 11, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }
        private void SetChart3(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt == null) { chart.Series.Clear(); return; }
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                // 각 불량 유형별로 시리즈 생성
                Series series1 = new Series("달성률 (%)", ViewType.Point);

                // 해당 시리즈에 데이터 포인트 추가
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MachineName"].ToString() != "")
                    {
                        //설비명이 비어있으면 안나오도록
                        string strAxis = dt.Rows[i]["MachineName"].ToString() == "" ? "" : dt.Rows[i]["MachineName"].ToString();
                        float value1 = dt.Rows[i]["달성률"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["달성률"].ToString());
                        series1.Points.Add(new SeriesPoint(strAxis, value1));

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series1);
                        //series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                    }
                }

                // 포인트 라벨 설정
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series1.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정


                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가

                chart.Series.Add(series1);
                // X축 및 Y축 설정
                XYDiagram diagram = chart.Diagram as XYDiagram;
                //diagram.AxisX.Label.TextPattern = "{A:dd}";
                //diagram.AxisX.WholeRange.MinValue = 1;
                //diagram.AxisX.VisualRange.MinValue = 15;
                //diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;

                //시리즈 색깔
                chart.Series["달성률 (%)"].View.Color = Color.Red;

                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "계획수량 대비 생산실적 달성률", Font = new Font(Font.FontFamily.Name, 11, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        private void SetChart4(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt == null) { chart.Series.Clear(); return; }
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                // 각 불량 유형별로 시리즈 생성
                Series series1 = new Series("가동률 (%)", ViewType.Point);

                // 해당 시리즈에 데이터 포인트 추가
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MachineName"].ToString() != "")
                    {
                        //설비명이 비어있으면 안나오도록
                        string strAxis = dt.Rows[i]["MachineName"].ToString() == "" ? "" : dt.Rows[i]["MachineName"].ToString();
                        float value1 = dt.Rows[i]["가동률"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["가동률"].ToString());
                        series1.Points.Add(new SeriesPoint(strAxis, value1));

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series1);
                        //series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                    }
                }

                // 포인트 라벨 설정
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series1.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정


                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가

                chart.Series.Add(series1);
                // X축 및 Y축 설정
                XYDiagram diagram = chart.Diagram as XYDiagram;
                //diagram.AxisX.Label.TextPattern = "{A:dd}";
                //diagram.AxisX.WholeRange.MinValue = 1;
                //diagram.AxisX.VisualRange.MinValue = 15;
                //diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;

                //시리즈 색깔
                chart.Series["가동률 (%)"].View.Color = Color.Red;

                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "설비별 가동률", Font = new Font(Font.FontFamily.Name, 11, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        private void SetChart4_Bad(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                // 각 불량 유형별로 시리즈 생성
                Series series1 = new Series("불량률 (%)", ViewType.Point);

                // 해당 시리즈에 데이터 포인트 추가
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["MachineName"].ToString() != "")
                    {
                        //설비명이 비어있으면 안나오도록
                        string strAxis = dt.Rows[i]["MachineName"].ToString() == "" ? "" : dt.Rows[i]["MachineName"].ToString();
                        float value1 = dt.Rows[i]["불량률"].ToString() == "" ? 0 : Convert.ToSingle(dt.Rows[i]["불량률"].ToString());
                        series1.Points.Add(new SeriesPoint(strAxis, value1));

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series1);
                        //series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                    }
                }

                // 포인트 라벨 설정
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                series1.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정


                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가

                chart.Series.Add(series1);
                // X축 및 Y축 설정
                XYDiagram diagram = chart.Diagram as XYDiagram;
                //diagram.AxisX.Label.TextPattern = "{A:dd}";
                //diagram.AxisX.WholeRange.MinValue = 1;
                //diagram.AxisX.VisualRange.MinValue = 15;
                //diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;

                //시리즈 색깔
                chart.Series["불량률 (%)"].View.Color = Color.Red;

                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "설비별 불량률", Font = new Font(Font.FontFamily.Name, 11, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }
    }
}