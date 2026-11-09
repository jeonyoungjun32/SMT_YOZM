using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Analysis
{
	public partial class Frm_AN_Capacity_ByMonth : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        cls_Lookup clsLup = new cls_Lookup();
        clsCode code = new clsCode();

        Database db = new Database();
        string query = "";
        string LP = ""; // "" : 공정, "Line":라인

        public Frm_AN_Capacity_ByMonth()
        {
            InitializeComponent();
        }


        public Frm_AN_Capacity_ByMonth(string type)
        {
            InitializeComponent();
            LP = type;
        }

        private void Frm_AN_Capacity_Load(object sender, EventArgs e)
        {
            Set_Grid(Gv_GrowthRate);
            SetSerachLookUp();
            Search();
        }

        private void SetSerachLookUp()
        {
            RstYear.Properties.MaskSettings.MaskExpression = "yyyy";
            RstYear.Properties.UseMaskAsDisplayFormat = true;
            RstYear.EditValue = DateTime.Now;
            RstYear.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;

            DataTable dtItem = code.GetMaterialList(new string[] { "20", "30" }, true).ToTable(); //품목
            DataTable dtLine = code.GetLine_List("", true);
            DataTable dtProc = code.GetProcess_List();

            clsLup.Create_RepositoryItemSearchLookUp(LupItem, dtItem, "품목코드", "품명");
            //grid.SetLupView_Item(LupItem, "제품코드 초기화");
            clsLup.Create_RepositoryItemSearchLookUp(LupLine, dtLine, "라인코드", "라인명");
            clsLup.Create_RepositoryItemSearchLookUp(LupProcess, dtProc, "공정코드", "공정명");

            LupItem.EditValue = "ALL";
            LupLine.EditValue = "ALL";
            LupProcess.EditValue = "ALL";

            LupItem.Properties.ShowClearButton = false;
            LupLine.Properties.ShowClearButton = false;
            LupProcess.Properties.ShowClearButton = false;
        }

        private void Set_Grid(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.IndicatorWidth = 50;                     //순번 너비

            gv.OptionsView.ColumnAutoWidth = true;

            //focus 설정
            gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            gv.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;

            grid.Create_GvColumn(gv, "년도", "YYYY");
            grid.Create_GvColumn(gv, "월", "MM");
            grid.Create_GvColumn(gv, "생산량", "Work_Qty");
            grid.Create_GvColumn(gv, "증감률(%)", "Growth_Rate");
            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            grid.SetCol_Type(gv.Columns["Work_Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Growth_Rate"], clsGrid.TypeList.TFloat);

            //칼럼헤드색상변경
            foreach (GridColumn col in gv.Columns)
            {
                col.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(30)))), ((int)(((byte)(48)))));
            }

            //이벤트
            //gv.CustomDrawCell += Event_Cell_ForeColor; 

        }

        private void Set_WorkChart(int thisMonth)
        {
            //월별 생산 추이 그리드
            gvWork.OptionsView.ColumnAutoWidth = true;
            AN_Capacity ac = new AN_Capacity();

            ac.YEAR = Convert.ToDateTime(RstYear.EditValue).ToString("yyyy");
            ac.LineCode = LupLine.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupLine.EditValue.ConvertString_Null_To_Empty();
            ac.ProcessCode = LupProcess.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupProcess.EditValue.ConvertString_Null_To_Empty();
            ac.ItemCode = LupItem.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : LupItem.EditValue.ConvertString_Null_To_Empty();

            DataTable dtWork = ac.Select_WorkQty_ByMoth_Grid(); //월별 생산, 양품, 지시수량 조회
            DataTable dtRate = ac.Select_Growth_Rate();
            DataTable dtPie = ac.Select_WorkQty_Process_Chart(LP);//월별 공정별 생산수량 조회
            DataTable dtAvg = ac.Select_MonthAvg();//월별 공정별 생산수량 조회

            gcWork.DataSource = dtWork;
            gvWork.BestFitColumns();
            
            Gc_GrowthRate.DataSource = dtRate;
            Gv_GrowthRate.BestFitColumns();

            Gc_Process.DataSource = dtPie;
            Gv_Process.BestFitColumns();

            if (dtWork.Rows.Count <= 0)
            {
                //MessageBox.Show("조회된 데이터가 없습니다.");
            }
            //차트 그리기
            SetChartDataQty(lineChart1, dtWork);    //월별 생산 추이도 라인 차트
            SetPieChart(pieChart1, dtPie);          //월별 공정별 생산 추이도 파이 차트
            SetLabel(dtAvg);
        }

        public void SetLabel(DataTable dt)
        {
            lblwork.Text = dt.Rows[0]["Avg_Work_Qty"].ConvertDecimal_Null_To_Zero().ToString("#,#");
            lblbad.Text = dt.Rows[0]["Avg_Bad_Qty"].ConvertDecimal_Null_To_Zero().ToString("#,#");
        }
        
        #region 월별 생산 추이도 라인 차트
        private void SetChartDataQty(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                for (int idx = 0; idx < dt.Rows.Count; idx++)
                {
                    string badNm = dt.Rows[idx]["Type"].ToString();
                    //해당 칼럼은 그래프에 표기X
                    if (badNm != "")
                    {
                        // 각 불량 유형별로 시리즈 생성
                        Series series = new Series(badNm, ViewType.Line);

                        // 해당 시리즈에 데이터 포인트 추가
                        for (int j = 1; j < 13; j++)
                        {
                            string strJ = j.ToString() + "월";
                            float value = Convert.ToSingle(dt.Rows[idx][strJ].ConvertFloat_Null_To_Zero());
                            series.Points.Add(new SeriesPoint(strJ, value));
                        }

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series);
                        series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                        // 포인트 라벨 설정
                        series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정
                    }
                    else
                    {
                        //불량유형이 없으면 표가 없음
                    }
                }
                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가
                foreach (var series in seriesList)
                {
                    chart.Series.Add(series);
                }

                // X축 및 Y축 설정cuu
                XYDiagram diagram = chart.Diagram as XYDiagram;
                if (diagram == null) return; //총생산,양품수량은 있는데 불량이 하나도 없을 경우 차트를 만들지 않는다.

                diagram.AxisX.VisualRange.SetMinMaxValues("01일", "10일"); // 스크롤 범위
                diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;


                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "월별 지시 및 생산수량 추이도" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }
        #endregion

        #region 월별 공정별 생산 추이도 파이 차트
        private void SetPieChart(ChartControl chartControl, DataTable dt)
        {
            try
            {
                // ChartControl 초기화
                chartControl.Series.Clear();
                chartControl.Titles.Clear();

                // 각 월별로 파이 차트 생성
                for (int month = 1; month <= 12; month++)
                {
                    // 월별 파이 차트 생성
                    Series series = new Series($"{month}", ViewType.Doughnut);

                    // 해당 월의 데이터를 파이 차트에 추가
                    foreach (DataRow row in dt.Rows)
                    {
                        string argName = row["ProcessName"].ToString();
                        double value = Convert.ToDouble(row[month.ToString() + "월"]);
                        if (value > 0 && !string.IsNullOrEmpty(argName))
                        {
                            SeriesPoint point = new SeriesPoint(argName, value);
                            series.Points.Add(point);
                        }
                        //else
                        //{
                        //    // 데이터가 null인 경우 처리
                        //    SeriesPoint point = new SeriesPoint("", value); 
                        //    series.Points.Add(point);
                        //}
                    }
                    series.Label.TextPattern = "{A}: {V:#,#}"; // 포인트 값 표시 형식 설정
                    series.Label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;

                    // 생성된 시리즈를 ChartControl에 추가
                    chartControl.Series.Add(series);

                    //도넛 가운데 글자 넣기
                    PieTotalLabel totalLabel = ((DoughnutSeriesView)pieChart1.Series[month.ToString()].View).TotalLabel;
                    totalLabel.Visible = true;
                    totalLabel.TextPattern = $"{month}월";
                    totalLabel.DXFont = new DevExpress.Drawing.DXFont("맑은 고딕", 15F);
                    
                    //DoughnutSeriesView view = new DoughnutSeriesView();
                    //view.Titles.Add(new SeriesTitle() { Text = $"{month}월 생산량", Visibility = DevExpress.Utils.DefaultBoolean.True });
                    //view.TotalLabel.Font = new Font("맑은 고딕", 12F); 
                    //series.View = view;                                   

                }

                // 제목 설정
                chartControl.Titles.Add(new ChartTitle { Text = "월별 공정별 생산수량" });
            }
            catch (Exception ex)
            {
                // 오류 처리
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }
        #endregion

        #region 월별 설비별 생산 추이도 바 차트
        private void SetChartDataQty_Machine(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                for (int idx = 0; idx < dt.Rows.Count; idx++)
                {
                    string badNm = dt.Rows[idx]["Machine_Nm"].ToString();
                    //해당 칼럼은 그래프에 표기X
                    if (badNm != "MachineCode")
                    {
                        // 각 불량 유형별로 시리즈 생성
                        Series series = new Series(badNm, ViewType.Bar);

                        // 해당 시리즈에 데이터 포인트 추가
                        for (int j = 1; j < 13; j++)
                        {
                            string strJ = j.ToString() + "월";
                            float value = Convert.ToSingle(dt.Rows[idx][strJ].ToString());
                            series.Points.Add(new SeriesPoint(strJ, value));
                        }

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series);
                        series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                        // 포인트 라벨 설정
                        series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series.Label.TextPattern = "{S}: {V:#,#}";// 포인트 값 표시 형식 설정
                    }
                    else
                    {
                        //불량유형이 없으면 표가 없음
                    }
                }
                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가
                foreach (var series in seriesList)
                {
                    chart.Series.Add(series);
                }

                // X축 및 Y축 설정cuu
                XYDiagram diagram = chart.Diagram as XYDiagram;
                if (diagram == null) return; //총생산,양품수량은 있는데 불량이 하나도 없을 경우 차트를 만들지 않는다.

                diagram.AxisX.VisualRange.SetMinMaxValues("1월", "6월"); // 스크롤 범위
                diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;


                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "월별 설비별 생산실적 추이도", Font = new Font(Font.FontFamily.Name, 12, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }
        #endregion

        #region 월별 설비 가동시간 라인 차트
        private void SetChartDataMachine(ChartControl chart, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0) { chart.Series.Clear(); return; }

                // 시리즈들을 담을 리스트 생성
                List<Series> seriesList = new List<Series>();

                for (int idx = 0; idx < dt.Rows.Count; idx++)
                {
                    string badNm = dt.Rows[idx]["Machine_Nm"].ToString();
                    //해당 칼럼은 그래프에 표기X
                    if (badNm != "계획정지시간" && badNm != "정지시간" && badNm != "시간가동률")
                    {
                        // 각 불량 유형별로 시리즈 생성
                        Series series = new Series(badNm, ViewType.Bar);

                        // 해당 시리즈에 데이터 포인트 추가
                        for (int j = 1; j < 13; j++)
                        {
                            string strJ = j.ToString() + "월";
                            float value = Convert.ToSingle(dt.Rows[idx][strJ].ToString());
                            series.Points.Add(new SeriesPoint(strJ, value));
                        }

                        // 생성된 시리즈를 리스트에 추가
                        seriesList.Add(series);
                        series.ArgumentScaleType = ScaleType.Qualitative; //x축label 자동 조정 막음
                        // 포인트 라벨 설정
                        series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                        series.Label.TextPattern = "{V:#,#}";// 포인트 값 표시 형식 설정
                    }
                    else
                    {
                        //불량유형이 없으면 표가 없음
                    }
                }
                // ChartControl 초기화
                chart.Series.Clear();
                chart.Titles.Clear();

                // 리스트에 있는 모든 시리즈를 ChartControl에 추가
                foreach (var series in seriesList)
                {
                    chart.Series.Add(series);
                }

                // X축 및 Y축 설정cuu
                XYDiagram diagram = chart.Diagram as XYDiagram;
                if (diagram == null) return; //총생산,양품수량은 있는데 불량이 하나도 없을 경우 차트를 만들지 않는다.

                diagram.AxisX.VisualRange.SetMinMaxValues("1월", "6월"); // 스크롤 범위
                diagram.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;


                // 제목 설정
                chart.Titles.Add(new ChartTitle { Text = "월별 지시 및 생산수량 추이도", Font = new Font(Font.FontFamily.Name, 12, FontStyle.Bold) });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("value data member isn't compatible with the numeric scale."))
                {
                    MessageBox.Show("'" + ex.Message.Split('"')[1] + "' 데이터값 설정이 잘못되었습니다.");
                }
            }
        }

        #endregion

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "BtnSearch":
                    Search();
                    break;
            }
        }

        private void Search()
        {
            int thisMonth = Convert.ToInt32(DateTime.Today.Month) + 1;
            Set_WorkChart(thisMonth);
        }

        private void RstYear_EditValueChanged(object sender, EventArgs e)
        {
            Search();
        }


        private void Event_Cell_ForeColor(object sender, RowCellCustomDrawEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.RowHandle < 0) return;

            string fieldName = e.Column.FieldName;

            if (fieldName == "Growth_Rate")
            {
                decimal value = Convert.ToDecimal(gv.GetRowCellValue(e.RowHandle, fieldName)?.ConvertString_Null_To_Zero());

                if (value >= 0)
                {
                    e.Appearance.ForeColor = Color.Blue;
                }
                else if (value < 0)
                {
                    e.Appearance.ForeColor = Color.Red; 
                }
            }
        }

    }

    class AN_Capacity
	{

        Database db = new Database();
        string query = "";
        public string YEAR { get; set; }
        public string LineCode { get; set; } //라인코드
        public string ProcessCode { get; set; }  //공정코드
        public string ItemCode { get; set; }
        public string MachineCode { get; set; }

        public DataTable Select_WorkQty_ByMoth_Grid()
        {
            LineCode = Convert.ToString(LineCode) == "ALL" ? "" : LineCode.ConvertString_Null_To_Empty();
            ProcessCode = ProcessCode.ConvertString_Null_To_Empty();

            //월별 생산, 양품, 지시수량 조회
            query = "";

            query += $@"
                            WITH AllMonths AS (
                            SELECT '1월' AS [Type]
                            UNION SELECT '2월'
                            UNION SELECT '3월'
                            UNION SELECT '4월'
                            UNION SELECT '5월'
                            UNION SELECT '6월'
                            UNION SELECT '7월'
                            UNION SELECT '8월'
                            UNION SELECT '9월'
                            UNION SELECT '10월'
                            UNION SELECT '11월'
                            UNION SELECT '12월'
                            )
                            SELECT 
                                 '1' AS Sort
	                            ,'지시수량' AS [Type]
	                            , [1월], [2월], [3월], [4월], [5월], [6월], [7월], [8월], [9월], [10월], [11월], [12월]
                            FROM (
		                            SELECT m.[Type], ISNULL(SUM(op.OrderQty), 0) AS Work_Qty
		                            FROM AllMonths m
		                            LEFT JOIN PM_OrderProcess op ON  m.[Type] = FORMAT(CONVERT(DATE, op.OrderDate), 'M월', 'en-US')
                                    AND YEAR(CONVERT(DATE, op.OrderDate)) = '{YEAR}'
                                    WHERE CorpID = '{WrGlobal.CorpID}'";

            if (LineCode != "")
            {
                query += $"\nAND op.LineCode = '{LineCode}' ";
            }

            if (ProcessCode != "")
            {
                query += $"\nAND op.ProcessCode = '{ProcessCode}' ";
            }
            if (ItemCode != "")
            {
                query += $"\nAND op.ItemCode = '{ItemCode}' ";
            }

            query += $@"
		                            GROUP BY m.[Type]
                            ) AS SourceData
                            PIVOT ( SUM(Work_Qty) FOR [Type] IN ([1월], [2월], [3월], [4월], [5월], [6월], [7월], [8월], [9월], [10월], [11월], [12월])) AS PivotTable
            
                            UNION 


                            SELECT 
                                 '3' AS Sort
	                            ,'양품수량' AS [Type]
	                            ,[1월], [2월], [3월], [4월], [5월], [6월], [7월], [8월], [9월], [10월], [11월], [12월]
                            FROM (
		                            SELECT m.[Type],
		                            ISNULL(SUM(op.GoodQty), 0) AS Good_Qty
		                            FROM AllMonths m
		                            LEFT JOIN PM_OrderResult op ON m.[Type] = FORMAT(CONVERT(DATE, op.WorkDate), 'M월', 'en-US')
                                    AND YEAR(CONVERT(DATE, op.WorkDate)) = '{YEAR}'
                                    WHERE CorpID = '{WrGlobal.CorpID}'";

            if (LineCode != "")
            {
                query += $"\nAND op.LineCode = '{LineCode}' ";
            }

            if (ProcessCode != "")
            {
                query += $"\nAND op.ProcessCode = '{ProcessCode}' ";
            }
            if (ItemCode != "")
            {
                query += $"\nAND op.ItemCode = '{ItemCode}' ";
            }
            query += $@"
		                            GROUP BY m.[Type]
                            ) AS SourceData
                            PIVOT ( SUM(Good_Qty) FOR [Type] IN ([1월], [2월], [3월], [4월], [5월], [6월], [7월], [8월], [9월], [10월], [11월], [12월]) ) AS PivotTable


			                UNION

                            SELECT 
                                 '4' AS Sort
	                            ,'불량수량' AS [Type]
	                            ,[1월], [2월], [3월], [4월], [5월], [6월], [7월], [8월], [9월], [10월], [11월], [12월]
                            FROM (
		                            SELECT m.[Type],
		                            ISNULL(SUM(op.NgQty), 0) AS Bad_Qty
		                            FROM AllMonths m
		                            LEFT JOIN PM_OrderResult op ON m.[Type] = FORMAT(CONVERT(DATE, op.WorkDate), 'M월', 'en-US')
                                    AND YEAR(CONVERT(DATE, op.WorkDate)) = '{YEAR}'
                                    WHERE CorpID = '{WrGlobal.CorpID}'";

            if (LineCode != "")
            {
                query += $"\nAND op.LineCode = '{LineCode}' ";
            }

            if (ProcessCode != "")
            {
                query += $"\nAND op.ProcessCode = '{ProcessCode}' ";
            }
            if (ItemCode != "")
            {
                query += $"\nAND op.ItemCode = '{ItemCode}' ";
            }
            query += $@"
		                            GROUP BY m.[Type]
                            ) AS SourceData
                            PIVOT ( SUM(Bad_Qty) FOR [Type] IN ([1월], [2월], [3월], [4월], [5월], [6월], [7월], [8월], [9월], [10월], [11월], [12월]) ) AS PivotTable
							Order by Sort

            ";


            return db.GetDataTable(query);
        }

        public DataTable Select_Growth_Rate()
        {
            query = "";
            query += $@" WITH MonthData AS (
                                SELECT 
                                    DATEPART(YEAR, WorkDate) AS YYYY,
                                    DATEPART(MONTH, WorkDate) AS MM,
                                    ISNULL(SUM(WorkQty), 0) AS Work_Qty
                                FROM PM_OrderResult pp
                                WHERE pp.CorpID = '{WrGlobal.CorpID}' 
                               --AND pp.ProcessCode = 'PR001'
                                GROUP BY DATEPART(YEAR, WorkDate), DATEPART(MONTH, WorkDate)
                            ),
                            ExtendedMonthData AS (
                                SELECT
                                    YYYY,
                                    MM,
                                    Work_Qty,
                                    ROW_NUMBER() OVER (ORDER BY YYYY, MM) AS RowNum
                                FROM MonthData
                                UNION ALL
                                SELECT
                                    CAST(YYYY - 1 AS VARCHAR) AS YYYY,
                                    '12' AS MM,
                                    ISNULL(SUM(Work_Qty), 0) AS Work_Qty,
                                    0 AS RowNum
                                FROM MonthData
                                WHERE MM = '01'
                                AND EXISTS (
                                    SELECT 1
                                    FROM MonthData md
                                    WHERE md.YYYY = CAST(YYYY - 1 AS VARCHAR)
                                    AND md.MM = '12'
                                )
                                GROUP BY YYYY
                            )


                            SELECT 
                                EMD.YYYY,
                                EMD.MM,
                                EMD.Work_Qty,
                                LAG(EMD.Work_Qty, 1) OVER (ORDER BY EMD.YYYY, EMD.MM) AS Previous_Work_Qty,
                                CASE 
                                    WHEN LAG(EMD.Work_Qty, 1) OVER (ORDER BY EMD.YYYY, EMD.MM) = 0 THEN 0
                                    ELSE ((EMD.Work_Qty - LAG(EMD.Work_Qty, 1) OVER (ORDER BY EMD.YYYY, EMD.MM)) / CAST(LAG(EMD.Work_Qty, 1) OVER (ORDER BY EMD.YYYY, EMD.MM) AS FLOAT)) * 100
                                END AS Growth_Rate
                            FROM 
                                ExtendedMonthData EMD
	                            WHERE YYYY='{YEAR}' OR (YYYY = CONVERT(INT, '{YEAR}') - 1 AND MM = '12')
                            ORDER BY 
                                EMD.YYYY, EMD.MM

                    ";
            return db.GetDataTable(query);
        }

        public DataTable Select_MonthAvg()
        {
            query = "";
            query += $@"
                        SELECT 
                                CONVERT(Decimal,AVG(Work_Qty),0) AS Avg_Work_Qty
	                        , CONVERT(Decimal,AVG(Good_Qty),0) AS Avg_Good_Qty
	                        , CONVERT(Decimal,AVG(Bad_Qty) ,0) AS Avg_Bad_Qty
                        FROM (
                            SELECT 
                                    DATEPART(MONTH, WorkDate) AS MM
                                , ISNULL(SUM(WorkQty), 0) AS Work_Qty
                                , ISNULL(SUM(GoodQty), 0) AS Good_Qty
	                            , ISNULL(SUM(NgQty), 0) AS Bad_Qty
                            FROM PM_OrderResult pp
                            LEFT JOIN EQ_Machine A ON pp.MachineCode = A.MachineCode AND A.CorpID = pp.CorpID
                            WHERE  DATEPART(YYYY, WorkDate) = '{YEAR}'
                            GROUP BY  DATEPART(MONTH, WorkDate)
                        ) AS Monthly_Work_Qty;

                ";

            return db.GetDataTable(query);
        }

        public DataTable Select_WorkQty_Process_Chart(string LP)
        {
            query = "";

            if (LP.ToUpper() == "LINE")
            {
                query += $@" SELECT LineCode AS ProcessCode, LineName AS ProcessName,";
            }
            else
            {
                query += $@" SELECT ProcessCode, ProcessName,";
            }
            query += $@"
                        ISNULL([01], 0) AS [1월],
                        ISNULL([02], 0) AS [2월],
                        ISNULL([03], 0) AS [3월],
                        ISNULL([04], 0) AS [4월],
                        ISNULL([05], 0) AS [5월],
                        ISNULL([06], 0) AS [6월],
                        ISNULL([07], 0) AS [7월],
                        ISNULL([08], 0) AS [8월],
                        ISNULL([09], 0) AS [9월],
                        ISNULL([10], 0) AS [10월],
                        ISNULL([11], 0) AS [11월],
                        ISNULL([12], 0) AS [12월]
                    FROM (  ";

            if (LP.ToUpper() == "LINE")
            {
                query += $@" SELECT pp.LineCode, bs.LineName,";
            }
            else
            {
                query += $@" SELECT pp.ProcessCode, bs.ProcessName,";
            }
            query += $@"    DATEPART(MONTH, WorkDate) AS MM,  
                            ISNULL(WorkQty,0) AS Work_Qty
                        FROM PM_OrderResult pp  ";

            if (LP.ToUpper() == "LINE")
            {
                query += $@" LEFT JOIN CM_Line bs ON pp.LineCode = bs.LineCode AND pp.CorpID  = bs.CorpID";
            }
            else
            {
                query += $@" LEFT JOIN CM_Process bs ON pp.ProcessCode = bs.ProcessCode AND pp.CorpID  = bs.CorpID";
            }
            query += $@" WHERE YEAR(CONVERT(DATE, WorkDate)) = '{YEAR}'
                          AND pp.CorpID = '{WrGlobal.CorpID}'";

            if (LineCode != "")
            {
                query += $"\nAND pp.LineCode = '{LineCode}' ";
            }

            if (ProcessCode != "")
            {
                query += $"\nAND pp.ProcessCode = '{ProcessCode}' ";
            }
            if (ItemCode != "")
            {
                query += $"\nAND pp.ItemCode = '{ItemCode}' ";
            }
            query += $@"
                    ) AS SourceTable
                    PIVOT (
                        SUM(Work_Qty)
                        FOR MM IN ([01], [02], [03], [04], [05], [06], [07], [08], [09], [10], [11], [12])
                    ) AS PivotTable ORDER BY ProcessName";

            return db.GetDataTable(query);
        }
    }
}