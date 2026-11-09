using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SmartMES.Common;
using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using SmartCommon.Common;

namespace SmartMES.View
{
    public partial class Frm_MainBoard : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = string.Empty;

        string comparsion = string.Empty;
        string workRate = string.Empty;
        string preWorkRate = string.Empty;

        string typeValue = string.Empty;
        SecondaryAxisY stockAxisY = null;
        public Frm_MainBoard()
        {
            InitializeComponent();
        }

        private void Frm_MainBoard_Load(object sender, EventArgs e)
        {
            Set_RevenueChart();             //월별 매출 추이
            Set_LblsInRevenueChart();       //월별 매출 추이 라벨

            Set_PerformanceChart();         //계획대비 생산실적
            Set_LblsInPerformanceChart();   //계획대비 생산실적 라벨

            Set_WorkChart();        //월별 생산 추이
            Set_SalesChart();       //월별 매입 추이

            Set_Radio();            //재고 추이 라디오
            Set_StockChart();       //재고 추이

            Set_StandardDate();     //기준일자
            Set_MachineChart();     //생산가동률

            Set_Notice();
        }

        private void Set_StandardDate()
        {
            LblStandardDate.Text = "기준일자 : " + DateTime.Now.ToString("yyyy-MM-dd");
            LblStandardDate2.Text = "기준일자 : " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        //월별 매출 추이

        private void Set_RevenueChart()
        {
            //초기화
            chartControl7.Series.Clear();
            this.chartControl7.SeriesTemplate.ArgumentDataMember = "";
            this.chartControl7.SeriesTemplate.SeriesDataMember = "";
            this.chartControl7.SeriesTemplate.ValueDataMembersSerializable = "";

            /*
            query = $@"IF OBJECT_ID('tempdb..#Months') IS NOT NULL DROP TABLE #Months;
                        CREATE TABLE #Months (Month INT, Year INT);
                        DECLARE @Year INT = YEAR(GETDATE());

                        DECLARE @Month INT = 1;
                        WHILE @Month <= 12
                        BEGIN
                            INSERT INTO #Months (Month, Year) VALUES (@Month, @Year);
                            SET @Month = @Month + 1;
                        END
                        SELECT 
                            CAST(m.Month AS VARCHAR(2)) + '월' AS Month,
                            ISNULL(SUM(ISNULL(sd.TotalPrice, 0)), 0) AS MonthlyPerformance
                        FROM 
                            #Months m
                        LEFT JOIN 
                            BM_SalesD sd ON m.Month = MONTH(sd.DeliveryDate) AND m.Year = YEAR(sd.DeliveryDate) AND sd.CorpID = '1000'
                        GROUP BY 
                            m.Month
                        ORDER BY 
                            m.Month;
                    ";
            */

            query = $@" IF OBJECT_ID('tempdb..#Months') IS NOT NULL DROP TABLE #Months;
                        CREATE TABLE #Months (Month INT, Year INT);
                        DECLARE @Year INT = YEAR(GETDATE());

                        DECLARE @Month INT = 1;
                        WHILE @Month <= 12
                        BEGIN
                        INSERT INTO #Months (Month, Year) VALUES (@Month, @Year);
                        SET @Month = @Month + 1;
                        END;
                        WITH #TEMP AS
                        (
	                        SELECT 
	                        m.Month as iMonth,
	                        CAST(m.Month AS VARCHAR(2)) + '월' AS Month,
	                        CONVERT(INT, ISNULL(SUM(ISNULL(sd.TotalPrice, 0)), 0)/10000) AS MonthlyPerformance
	                        FROM 
	                        #Months m
	                        LEFT JOIN 
	                        (
								SELECT SM.SalesDate
										, SD.TotalPrice
										, SD.ReqType
										, SD.CorpId
								FROM BM_SalesD SD
								LEFT JOIN BM_SalesM SM ON SD.SalesNo = SM.SalesNo
							) sd  on sd.ReqType = '출하출고' and m.Month = MONTH(sd.SalesDate) and m.Year = YEAR(sd.SalesDate) AND sd.CorpID = '1000'
	                        GROUP BY 
	                        m.Month
                        )
                        SELECT *
                        ,(SELECT CONVERT(INT,AVG(MonthlyPerformance)) FROM #TEMP)/10000 AS vAVG FROM #TEMP
                        ORDER BY iMonth;
                    ";

            chartControl7.DataSource = db.GetDataView("월별 매출 ", query);                         //차트에 데이터 바인딩
            //SeriesTempalte : 자동으로 시리즈 생성
            this.chartControl7.SeriesTemplate.SeriesDataMember = "Month";                           //여러 차트
            this.chartControl7.SeriesTemplate.ArgumentDataMember = "Month";                         //X축
            this.chartControl7.SeriesTemplate.ValueDataMembersSerializable = "MonthlyPerformance";  //Y축
            this.chartControl7.BackColor = Color.White;


            // Series Setting and Add to Series
            Series series = new Series("월별 매출 추이", ViewType.Line);

            // X Y Setting
            series.ArgumentDataMember = "Month";                                    //X축
            series.ValueDataMembers.AddRange(new string[] { "MonthlyPerformance" });//Y축
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;         // 라벨 값표시

            // Line Chart Setting
            LineSeriesView lineView = new LineSeriesView();
            lineView.Color = Color.Navy;
            lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;       //dot Marker
            series.View = lineView;                     //series.View = new LineSeriesView();


            chartControl7.Series.Add(series);               //차트 컨트롤에 시리즈 추가
            // 차트 스타일 조정
            chartControl7.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;   // 크로스헤어 활성화
            chartControl7.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False; // 범례 숨기기
            chartControl7.BackColor = Color.White;                                  //  차트 배경색 흰색

            // XYDiagram 형식으로 캐스팅 다이어그램
            ChartControl chart = chartControl7;
            XYDiagram diagram = chart.Diagram as XYDiagram;
            //XYDiagram diagram = (XYDiagram)chartControl7.Diagram;
            if (diagram != null)
            {

                ((XYDiagram)chartControl7.Diagram).DefaultPane.BackColor = Color.White;

                //Y축 Setting
                diagram.AxisY.NumericScaleOptions.AutoGrid = true;  //0축 안보이게
                diagram.AxisY.GridLines.Visible = true;            //그리드의 가로선 안보이게
                diagram.AxisY.CustomLabels.Clear();
                diagram.AxisY.Strips.Clear();                       // Y축 모든 스트립 삭제

                // X축  체크 표시 비활성화
                diagram.AxisX.Tickmarks.MinorVisible = false;


                // X축 설정: 1월부터 12월까지 나열되도록 설정
                //diagram.AxisX.NumericScaleOptions.AutoGrid = false;
                //diagram.AxisX.WholeRange.SetMinMaxValues(1, 12);
                //diagram.AxisX.VisualRange.SetMinMaxValues(1, 12);
                //diagram.AxisX.GridSpacing = 1; // 매 달마다 눈금 표시
                //diagram.AxisX.DateTimeScaleOptions.GridSpacing = 1; // 홀수 월만 표시하려면 GridSpacing을 조정

                // 라벨 설정: 홀수 달만 라벨을 표시
                //diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = false;
                //diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;

                // X축 커스텀 라벨을 모두 삭제
                //diagram.AxisX.CustomLabels.Clear();
                //diagram.AxisX.Strips.Clear();       // X축 모든 스트립 삭제
                //diagram.AxisY.ConstantLines.Clear(); // 모든 Constant Lines를 삭제
                AxisLabel axisXLabel = ((XYDiagram)chartControl7.Diagram).AxisX.Label;

                // 축 레이블 커스텀 드로잉 이벤트 핸들러
                chartControl8.CustomDrawAxisLabel += (sender, e) =>
                {
                    if (e.Item.Axis == diagram.AxisX && (e.Item.Text == "0" || e.Item.Text == "0월"))
                    {
                        e.Item.Text = "";
                    }
                    if (e.Item.Axis == diagram.AxisY && (e.Item.Text == "0" || e.Item.Text == "0.00"))
                    {
                        e.Item.Text = "";
                    }
                };

                //연평균 선(Constant Line) Setting
                ConstantLine averageLine = new ConstantLine("연평균");
                {
                    DataTable dt = GetAveragePerformance();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int averagePerformance = dt.Rows[0]["AveragePerformance"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["AveragePerformance"]) : 0;
                        averageLine.AxisValue = averagePerformance;

                        averageLine.Visible = true;
                        averageLine.RuntimeDeletion = true;
                        averageLine.RuntimeMoving = true;
                        averageLine.Color = Color.Orange; // 선 색상 설정
                        averageLine.LineStyle.DashStyle = DashStyle.Solid; // 선 스타일 설정
                        averageLine.LineStyle.Thickness = 2; // 선 두께 설정
                    }
                }
                //chartControl7.Invalidate();
                diagram.AxisY.ConstantLines.Add(averageLine);    // Y축에 Constant Line 추가
                //chartControl7.RefreshData();                     // 차트 갱신
            }
            else
            {
                MessageBox.Show("차트 다이어그램을 초기화하는 데 실패했습니다.");
            }
        }

        private void Set_LblsInRevenueChart()
        {
            query = $@"Exec Chart_GetMonthlySalesPerformance '{WrGlobal.CorpID}'";
            DataView dv = db.GetDataView("실적 달성률 전월대비", query);

            if (dv != null && dv.Table.Rows.Count > 0)
            {
                DataRowView row = dv[0];

                lblProPerformanceData.Text = (row["Performance"] != DBNull.Value && row["Performance"].ToString() != "") ? String.Format("{0:N0}", row["Performance"]) + "만" : "0"; //월실적
                lblAchievementRateData.Text = (row["AchievementRate"] != DBNull.Value && row["AchievementRate"].ToString() != "") ? String.Format("{0:N1}%", row["AchievementRate"]) : "0%"; //달성률
                lblMonthoverMonthData.Text = (row["MonthOverMonthChange"] != DBNull.Value && row["MonthOverMonthChange"].ToString() != "") ? String.Format("{0:N1}%", row["MonthOverMonthChange"]) : "0%"; //전월대비 -퍼센트 나오면 조건?
            }
            else
            {
                lblProPerformanceData.Text = "0";
                lblAchievementRateData.Text = "0%";
                lblMonthoverMonthData.Text = "0%";
            }
        }

        //연평균
        private DataTable GetAveragePerformance()
        {
            query = $@"DECLARE @AveragePerformance INT;
                        SELECT @AveragePerformance = AVG(MonthlySales)
                        FROM (
                            SELECT SUM(TotalPrice) AS MonthlySales
                            FROM BM_SalesD
                            WHERE CorpID = '{WrGlobal.CorpID}'
                              AND YEAR(DeliveryDate) = YEAR(GETDATE())
                            GROUP BY MONTH(DeliveryDate)
                        ) AS MonthlyPerformance;
                        SELECT @AveragePerformance AS AveragePerformance;";
            return db.GetDataTable(query);
        }


        //목표대비 실적
        private void Set_PerformanceChart()
        {
            // 차트 컨트롤 초기화
            chartControl8.Series.Clear();
            //chartControl8.DataSource = null;
            this.chartControl8.SeriesTemplate.ArgumentDataMember = "";
            this.chartControl8.SeriesTemplate.SeriesDataMember = "";
            this.chartControl8.SeriesTemplate.ValueDataMembersSerializable = "";

            // 데이터 바인딩용 시리즈 생성
            query = $@"Exec Chart_GetProductPerformanceMonthly '{WrGlobal.CorpID}'";
            chartControl8.DataSource = db.GetDataView("목표대비실적", query);        //차트에 데이터 바인딩
            chartControl8.Dock = DockStyle.Fill;

            //시리즈 설정
            Series seriesActual = new Series("실적", ViewType.FullStackedBar);    //StackedBar
            Series seriesPlan = new Series("계획", ViewType.FullStackedBar);
            Series seriesExcess = new Series("초과분", ViewType.FullStackedBar);  // 초과분을 나타낼 시리즈

            // 데이터 바인딩
            //seriesPlan.Points.Add(new SeriesPoint("목표", "PlanQty"));
            //seriesActual.Points.Add(new SeriesPoint("실적", "ActualQty"));

            // ArgumentDataMember 설정
            seriesPlan.ArgumentDataMember = "Month";
            seriesActual.ArgumentDataMember = "Month";
            seriesExcess.ArgumentDataMember = "Month";

            // ValueDataMembers 설정
            seriesPlan.ValueDataMembers.AddRange("PlanQty");
            seriesActual.ValueDataMembers.AddRange("ActualQty");
            seriesExcess.ValueDataMembers.AddRange("ExcessQty");    //ExcessQty-TestExcessQty로 수정

            // 시리즈 뷰 설정 (Bar 너비 등)
            FullStackedBarSeriesView viewPlan = new FullStackedBarSeriesView();       //SideBySideBarSeriesView > StackedBarSeriesView > FullStackedBarSeriesView
            viewPlan.BarWidth = 0.8; // 바 너비 설정
            viewPlan.Color = Color.FromArgb(49, 63, 92);
            seriesPlan.View = viewPlan;

            FullStackedBarSeriesView viewActual = new FullStackedBarSeriesView();
            viewActual.BarWidth = 0.8; // 바 너비 설정
            viewActual.Color = Color.Orange;
            seriesActual.View = viewActual;

            // 초과분 시리즈 뷰 설정
            FullStackedBarSeriesView viewExcess = new FullStackedBarSeriesView();
            viewActual.BarWidth = 0.8; // 바 너비 설정
            viewExcess.Color = Color.Red; // 초과분 색상 설정
            seriesExcess.View = viewExcess;


            // 차트컨트롤에 시리즈 추가(Before diagram)
            chartControl8.Series.AddRange(new Series[] { seriesActual, seriesPlan, seriesExcess }); //Acutual > Plan > Excess
            //chartControl8.Series.Add(seriesPlan);
            //chartControl8.Series.Add(seriesActual);

            // 차트 회전 설정
            XYDiagram diagram = (XYDiagram)chartControl8.Diagram;
            diagram.Rotated = true; // 바 차트를 수평으로 회전

            // X축  체크 표시 비활성화
            diagram.AxisX.Tickmarks.MinorVisible = false;
            //diagram.AxisX.NumericScaleOptions.AutoGrid = true;  //0축 안보이게

            // Y축 레이블 포맷 설정
            diagram.AxisY.Label.TextPattern = "{V:P0}"; //p2 /포맷 스트링: 퍼센트 값 소수점
            //diagram.AxisY.WholeRange.SetMinMaxValues(0, 1); // Y축 전체 범위를 0%에서 100%로 설정
            diagram.AxisX.VisualRange.SideMarginsValue = 15;
            diagram.AxisY.VisualRange.SideMarginsValue = 50;

            // Y축의 보이는 범위를 조정
            diagram.AxisY.VisualRange.MinValue = 1;  // 시작 지점을 1로 설정하여 첫 번째 라벨에 여백을 추가
            diagram.AxisY.VisualRange.MaxValue = 1; // 끝 지점을 13으로 설정하여 마지막 라벨이 완전히 보이게

            // 축 레이블 커스텀 드로잉 이벤트 핸들러
            chartControl8.CustomDrawAxisLabel += (sender, e) =>
            {
                if (e.Item.Axis == diagram.AxisY && e.Item.Text == "0%")
                {
                    e.Item.Text = "";
                }
            };

            // X축 레이블 폰트 설정
            diagram.AxisX.Label.Font = new Font("Pretendard SemiBold", 10, FontStyle.Bold);
            diagram.AxisY.Label.Font = new Font("Pretendard SemiBold", 10, FontStyle.Bold);

            // 스타일 설정
            seriesPlan.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True; // 목표 시리즈 라벨 표시
            seriesActual.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True; // 실적 시리즈 라벨 표시
            seriesExcess.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True; // 초과 시리즈 라벨 표시

            // 라벨 포맷 설정
            seriesPlan.Label.TextPattern = "{V:N0}";
            seriesActual.Label.TextPattern = "{V:N0}";
            seriesExcess.Label.TextPattern = "{V:N0}";

            // 범례 설정
            chartControl7.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            chartControl7.Invalidate();   // 차트 갱신
        }

        private void Set_LblsInPerformanceChart()
        {
            query = $@"Exec Chart_GetProductPerformanceMonthly '{WrGlobal.CorpID}'";
            DataView dv = db.GetDataView("월계획대비 생산실적", query);

            if (dv != null && dv.Table.Rows.Count > 0)
            {
                DataRowView row = dv[0];

                lblPlanQty.Text = (row["PlanQty"] != DBNull.Value && row["PlanQty"].ToString() != "") ? String.Format("{0:N0}", row["PlanQty"]) : "0";               //계획수량
                lblWorkQty.Text = (row["ActualQty"] != DBNull.Value && row["ActualQty"].ToString() != "") ? String.Format("{0:N0}", row["ActualQty"]) : "0";         //생산수량
                lblProductRate.Text = (row["AchievementRate"] != DBNull.Value && row["AchievementRate"].ToString() != "") ? String.Format("{0:N1}%", row["AchievementRate"]) : "0%"; //달성률
            }
            else
            {
                lblPlanQty.Text = "0";
                lblWorkQty.Text = "0";
                lblProductRate.Text = "0%";
            }
        }


        //월별 생산 추이
        private void Set_WorkChart()
        {
            //월별 생산 추이 그리드
            query = $@"WITH AllMonths AS (
                                SELECT '1월' AS MonthOnly
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
                                'Total' AS MonthOnly,
                                [1월], [2월], [3월], [4월], [5월], [6월], [7월], [8월], [9월], [10월], [11월], [12월]
                            FROM (
                                SELECT m.MonthOnly,
                                       ISNULL(SUM(op.goodQty), 0) AS OrderQty
                                FROM AllMonths m
                                LEFT JOIN PM_OrderProcess op ON m.MonthOnly = FORMAT(op.StartDate, 'M월', 'en-US')
                                                         AND YEAR(op.StartDate) = YEAR(GETDATE()) -- 현재 날짜의 년도
                                GROUP BY m.MonthOnly
                            ) AS SourceData
                            PIVOT (
                                SUM(OrderQty) FOR MonthOnly IN ([1월], [2월], [3월], [4월], [5월], [6월], [7월], [8월], [9월], [10월], [11월], [12월])
                            ) AS PivotTable";


            //월별 생산 추이 차트
            //기존 Series 제거
            chartWork.Series.Clear();

            this.chartWork.SeriesTemplate.ArgumentDataMember = "";
            this.chartWork.SeriesTemplate.SeriesDataMember = "";
            this.chartWork.SeriesTemplate.ValueDataMembersSerializable = "";

            //차트 초기화
            chartWork.Series.Clear();

            //Series 생성
            Series qtySeries = new Series("생산량", ViewType.Bar);

            query = $@"WITH AllMonths AS (
                                            SELECT '1월' AS MonthOnly
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

                                        SELECT m.MonthOnly,
                                               ISNULL(SUM(op.goodQty), 0) AS OrderQty
                                        FROM AllMonths m
                                        LEFT JOIN PM_OrderProcess op ON m.MonthOnly = FORMAT(op.StartDate, 'M월', 'en-US')
                                                                 AND YEAR(op.StartDate) = YEAR(GETDATE()) -- 현재 날짜의 년도
                                        GROUP BY m.MonthOnly
                                        ORDER BY CAST(LEFT(m.MonthOnly, CHARINDEX('월', m.MonthOnly) - 1) AS INT)";

            //차트 데이터 바인딩
            chartWork.DataSource = db.GetDataView("월별생산추이", query);

            //X축 및 Y축 설정
            qtySeries.ArgumentDataMember = "MonthOnly";
            qtySeries.ValueDataMembers.AddRange(new string[] { "OrderQty" });
            qtySeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;         // 라벨 값표시

            //라인 차트 설정
            //LineSeriesView lineView = new LineSeriesView();
            //lineView.Color = Color.Navy;
            //qtySeries.View = lineView;

            ////라벨 표시 여부 설정
            //lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            //차트에 시리즈 추가
            chartWork.Series.Add(qtySeries);

            //차트 갱신
            chartWork.RefreshData();
        }

        //월별 매입 추이
        private void Set_SalesChart()
        {
            query = $@"	SELECT
                                YEAR(PlacingDate) AS PlacingYear,
                                MONTH(PlacingDate) AS PlacingMonth,
                                SUM(TotalPrice) AS MonthlyTotalPrice,
                                CAST(SUM(TotalPrice) * 100.0 / NULLIF(AVG(SUM(TotalPrice)) 
                                    OVER (PARTITION BY YEAR(PlacingDate)), 0) AS DECIMAL(18, 0)) AS AchievementRate
                            FROM
                                MM_PlacingM
                            WHERE
                                YEAR(PlacingDate) = YEAR(GETDATE()) AND MONTH(PlacingDate) = MONTH(GETDATE())
                            GROUP BY
                                YEAR(PlacingDate), MONTH(PlacingDate)
                        ";

            DataView dv = db.GetDataView("달성률", query);
            if (dv.Count > 0)
            {
                lblSalesRate.Text = dv[0]["AchievementRate"].ToString() + "%";
            }
            else
            {
                lblSalesRate.Text = "0%";
            }

            query = $@"
                    WITH MonthlySales AS(
                        SELECT
                            YEAR(PlacingDate) AS PlacingYear,
                            MONTH(PlacingDate) AS PlacingMonth,
                            SUM(TotalPrice) AS MonthlyTotalPrice
                        FROM
                            MM_PlacingM
                        GROUP BY
                            YEAR(PlacingDate), MONTH(PlacingDate)
                    )

                    SELECT
                        A.PlacingYear,
                        A.PlacingMonth,
                        --A.MonthlyTotalPrice,
                        CONVERT(INT, ROUND(A.MonthlyTotalPrice / 10000, 0)) MonthlyTotalPrice,
                        ROUND(B.MonthlyTotalPrice / 10000, 0) AS PreviousMonthTotalPrice,
                        CAST(CASE
                                WHEN B.MonthlyTotalPrice IS NULL THEN NULL
                                ELSE((A.MonthlyTotalPrice - B.MonthlyTotalPrice) * 100.0 / NULLIF(B.MonthlyTotalPrice, 0))
                            END AS DECIMAL(18, 1)) AS SalesChangeRate
                    FROM
                        MonthlySales A
                    LEFT JOIN
                        MonthlySales B ON A.PlacingYear = B.PlacingYear AND A.PlacingMonth = B.PlacingMonth + 1
                    WHERE
                        A.PlacingYear = YEAR(GETDATE()) AND A.PlacingMonth = MONTH(GETDATE())";

            dv = db.GetDataView("실적", query);
            if (dv.Count > 0)
            {
                lblResult.Text = dv[0]["MonthlyTotalPrice"].ToString() + "만";     //실적
                lblMoM.Text = dv[0]["SalesChangeRate"].ToString() + "%";    //전월대비
            }
            else
            {
                lblResult.Text = "0";
                lblMoM.Text = "0%";
            }

            //차트
            query = $@"
                        WITH AllMonths AS (
                            SELECT '1월' AS MonthOnly
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
                            COALESCE(YEAR(PlacingDate), YEAR(GETDATE())) AS PlacingYear,
                            m.MonthOnly AS MonthOnly,
                            COALESCE(SUM(TotalPrice), 0) AS MonthlyTotalPrice
                        FROM
                            AllMonths m
                        LEFT JOIN
                            MM_PlacingM p ON YEAR(p.PlacingDate) = YEAR(GETDATE()) AND FORMAT(p.PlacingDate, 'M월', 'en-US') = m.MonthOnly
                        GROUP BY
                            COALESCE(YEAR(PlacingDate), YEAR(GETDATE())), m.MonthOnly
                        ORDER BY
                            COALESCE(YEAR(PlacingDate), YEAR(GETDATE())), CAST(LEFT(m.MonthOnly, CHARINDEX('월', m.MonthOnly) - 1) AS INT);";


            dv = db.GetDataView("월별 매입 추이", query);

            // 월별 매입 추이를 나타내는 시리즈
            DevExpress.XtraCharts.Series monthlySeries = new DevExpress.XtraCharts.Series("월별 매입액", ViewType.Line);

            // 시리즈에 데이터 포인트 추가
            foreach (DataRowView rowView in dv)
            {
                string placingMonth = rowView["MonthOnly"].ToString();
                double monthlyTotalPrice = Convert.ToDouble(rowView["MonthlyTotalPrice"]);

                // 월별 매입 추이 시리즈에 데이터 포인트 추가
                monthlySeries.Points.Add(new SeriesPoint($"{placingMonth}", monthlyTotalPrice));
            }

            // LineSeriesView를 사용하여 시리즈의 색상 및 라벨 설정
            LineSeriesView lineView = new LineSeriesView();
            lineView.Color = Color.Navy;
            lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True; // 마커 표시 설정

            monthlySeries.View = lineView;

            // 차트에 월별 매입 추이 시리즈 추가
            chartSales.Series.Add(monthlySeries);

            // 추가적인 설정
            chartSales.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.True;
            chartSales.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;           // 범례 감추기
            monthlySeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;         // 라벨 값표시

        }

        //재고 추이 라디오
        private void Set_Radio()
        {
            Create_RadioGroupItem(RdoType, "전체", "ALL");
            Create_RadioGroupItem(RdoType, "제품", "20");
            //Create_RadioGroupItem(RdoType, "반제품", "30");
            Create_RadioGroupItem(RdoType, "원자재", "40");
            //Create_RadioGroupItem(RdoType, "부자재", "50");

            RdoType.EditValueChanged += RdoType_SelectedIndexChanged;

            RdoType.EditValue = "ALL";
        }

        private void RdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            typeValue = RdoType.EditValue.ToString();

            #region 호성용
            //if (typeValue == "20") typeList = new string[] { "10", "20", "30" };
            //if (typeValue == "40") typeList = new string[] { "40", "50", "60", "70" };
            #endregion

            SeriesVisibe_YN();
        }

        private void Create_RadioGroupItem(RadioGroup radioGroup, string description, string value)
        {
            RadioGroupItem rdoItem = new RadioGroupItem
            {
                Description = description, //표기할 명칭
                Value = value              //데이터 값
            };

            radioGroup.Properties.Items.Add(rdoItem);
        }

        //재고추이
        private void Set_StockChart()
        {
            panelControl11.Parent = chartStock; // ChartControl 위에 배치
            panelControl11.BackColor = Color.Transparent; // 투명 설정
            panelControl11.BringToFront(); // 차트 위로 올림

            chartStock.Series.Clear();

            //query = $@"EXEC Chart_MainStock '{WrGlobal.CorpID}'";
            query = $@"EXEC Chart_MainStock_2 '{DateTime.Now.ToString("yyyy-MM")}'";

            DataView dv = db.GetDataView("재고추이", query);
            chartStock.DataSource = dv;

            chartStock.SeriesTemplate.ArgumentDataMember = "MM";
            chartStock.SeriesTemplate.SeriesDataMember = "TypeName";
            chartStock.SeriesTemplate.ValueDataMembersSerializable = "StockQty";
            chartStock.SeriesTemplate.LegendTextPattern = "{S}\t\t";
            //chartStock.SeriesTemplate.Label.TextPattern = "{V}";
            //chartStock.SeriesTemplate.View = new PieSeriesView();

            foreach (Series series in chartStock.Series)    // 라벨(값) 보이게
            {
                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            }

            // 스케일 자동조절
            //XYDiagram diagram = chartStock.Diagram as XYDiagram;
            //diagram.AxisY.AutoScaleBreaks.Enabled = true;
            //diagram.AxisY.AutoScaleBreaks.MaxCount = 7; // 분할의 최대 개수 설정

            SeriesVisibe_YN();

            //query = $@"EXEC Chart_MainStock '{WrGlobal.CorpID}', 'Y'";
            query = $@"EXEC Chart_MainStock_2 '{DateTime.Now.ToString("yyyy-MM")}', 'Y'";

            dv = db.GetDataView("TEXT", query);

            //LblStock.Text = (Convert.ToInt32(dv[0].Row["Percent"]) > 0 ? "+" : "") + dv[0].Row["Percent"].ToString() + "%";

            object percentValue = dv[0].Row["Percent"];
            Lbl_Stock.Text = DBNull.Value.Equals(percentValue) ? "-" : (Convert.ToInt32(percentValue) > 0 ? "" : "") + percentValue.ToString() + "%";

            // Argument 오름차순으로 변경 (내림차순으로 되어있음)
            //var axisX = ((XYDiagram)chartStock.Diagram).AxisX;
            //axisX.Reverse = true;
        }

        private void Set_Notice()
        {
            query = $@"
                        SELECT BoardNo
                             , Subject
	                        , WriteDate
                        FROM SM_Notice
                    ";

            Gc_Notice.DataSource = db.GetDataTable(query);
            Gv_Notice.BestFitColumns();
        }

        private void SeriesVisibe_YN()
        {
            // 모든 시리즈 안보이게
            foreach (Series series in chartStock.Series)
            {
                series.Visible = false;
            }


            switch (typeValue)
            {
                case "ALL":
                    // 모든 시리즈 보이게
                    foreach (Series series in chartStock.Series)
                    {
                        series.Visible = true;
                    }

                    break;

                case "20":

                    foreach (Series series in chartStock.Series)
                    {
                        if (series.Name == "제품")
                        {
                            series.Visible = true;
                        }
                    }

                    break;

                case "30":

                    foreach (Series series in chartStock.Series)
                    {
                        if (series.Name == "반제품")
                        {
                            series.Visible = true;
                        }
                    }

                    break;

                case "40":

                    foreach (Series series in chartStock.Series)
                    {
                        if (series.Name == "원자재")
                        {
                            series.Visible = true;
                        }
                    }

                    break;

                case "50":

                    foreach (Series series in chartStock.Series)
                    {
                        if (series.Name == "부자재")
                        {
                            series.Visible = true;
                        }
                    }

                    break;

                default: break;
            }
        }

        private void Set_MachineChart()
        {
            query = $@"
                        WITH NonOper AS (
                            SELECT
                                B.LineCode,		-- 라인
                                A.OrderNo,
                                A.OrderSeq,
                                A.SubSeq,
                                DATEDIFF(MINUTE, A.StartDate, A.EndDate) OperTime,	-- 가동시간
                                CASE
                                    WHEN B.StartTime BETWEEN A.StartDate AND A.EndDate THEN
                                        DATEDIFF(MINUTE, B.StartTime, CASE WHEN B.EndTime < A.EndDate THEN B.EndTime ELSE A.EndDate END)
                                    WHEN B.EndTime BETWEEN A.StartDate AND A.EndDate THEN
                                        DATEDIFF(MINUTE, CASE WHEN B.StartTime > A.StartDate THEN B.StartTime ELSE A.StartDate END, B.EndTime)
                                    WHEN A.StartDate BETWEEN B.StartTime AND B.EndTime THEN
                                        DATEDIFF(MINUTE, A.StartDate, CASE WHEN A.EndDate < B.EndTime THEN A.EndDate ELSE B.EndTime END)
                                    WHEN A.EndDate BETWEEN B.StartTime AND B.EndTime THEN
                                        DATEDIFF(MINUTE, CASE WHEN A.StartDate > B.StartTime THEN A.StartDate ELSE B.StartTime END, A.EndDate)
                                    ELSE 0
                                END AS NonOper	-- 비가동시간
                            FROM PM_OrderResult A	
                            LEFT JOIN EQ_Operation B ON A.OrderNo = B.OrderNo AND A.OrderSeq = B.OrderSeq
                            WHERE 
                                (
                                    (B.StartTime BETWEEN A.StartDate AND A.EndDate)
                                    OR (B.EndTime BETWEEN A.StartDate AND A.EndDate)
                                    OR (A.StartDate BETWEEN B.StartTime AND B.EndTime)
                                    OR (A.EndDate BETWEEN B.StartTime AND B.EndTime)
                                )
                                --AND CONVERT(CHAR(6), A.WorkDate, 112) IN ('202307', '202407')	-- 날짜값 (yyyyMM 형식)
                        )
                        SELECT 
                        ISNULL(AL.PreOperRate,0) AS PreOperRate,
                        ISNULL(AL.CurOperRate,0) AS CurOperRate,
                        CASE WHEN ISNULL(AL.PreOperRate,0) = 0 THEN 0 ELSE  ROUND((AL.CurOperRate - AL.PreOperRate)/AL.PreOperRate * 100,2) END AS OverOperRate
                        FROM 
                        (
	                        SELECT 
		                        ROUND(AVG(ISNULL(CONVERT(float, Cur.RunTime) / Cur.ResultTime, 0) * 100),2) as CurOperRate,	-- 당월 가동률 
		                        ROUND(AVG(ISNULL(CONVERT(float, Pre.RunTime) / Pre.ResultTime, 0) * 100),2) as PreOperRate	-- 전월 가동률 
	                        FROM (
			                        -- 당월 Start
		                        SELECT LineCode
			                        ,  SUM(OperTime) as ResultTime	-- 전체 작업시간
			                        , SUM(OperTime - NonOper) as RunTime	-- 순수 가동시간
			                        , SUM(NonOper) as NonTime	-- 비가동시간
		                        FROM (
			                        SELECT N.LineCode
					                        , N.OrderNo
					                        , N.OrderSeq
					                        , OperTime		-- 전체 작업 시간
					                        , SUM(NonOper) AS NonOper	-- 비가동시간
			                        FROM NonOper N
			                        INNER JOIN PM_OrderResult ORL ON N.OrderNo = ORL.OrderNo AND N.OrderSeq = ORL.OrderSeq AND N.SubSeq = ORL.SubSeq
			                        WHERE CONVERT(CHAR(6), WorkDate, 112) = '{DateTime.Now.ToString("yyyyMM")}'
			                        GROUP BY N.LineCode, N.OrderNo, N.OrderSeq, N.SubSeq, OperTime
			                        ) as R
			                        GROUP BY LineCode
		                        ) as Cur
		                        -- 당월 END

	                        FULL OUTER JOIN (

			                        -- 전월 Start
		                        SELECT LineCode
				                        ,  SUM(OperTime) as ResultTime	-- 전체 작업시간
				                        , SUM(OperTime - NonOper) as RunTime	-- 순수 가동시간
				                        , SUM(NonOper) as NonTime	-- 비가동시간
		                        FROM (
			                        SELECT N.LineCode
					                        , N.OrderNo
					                        , N.OrderSeq
					                        , OperTime		-- 전체 작업 시간
					                        , SUM(NonOper) AS NonOper	-- 비가동시간
			                        FROM NonOper N
			                        INNER JOIN PM_OrderResult ORL ON N.OrderNo = ORL.OrderNo AND N.OrderSeq = ORL.OrderSeq AND N.SubSeq = ORL.SubSeq
			                        WHERE CONVERT(CHAR(6), WorkDate, 112) = '{DateTime.Now.AddMonths(-1).ToString("yyyyMM")}'
			                        GROUP BY N.LineCode, N.OrderNo, N.OrderSeq, N.SubSeq, OperTime
			                        ) as R
			                        GROUP BY LineCode
		                        ) as Pre ON Cur.LineCode = Pre.LineCode
		                        -- 전월 END
                        ) AL;
                        ";

            DataView dv = db.GetDataView("전월대비가동률", query);

            comparsion = (Convert.ToInt32(dv[0]["OverOperRate"]) > 0 ? dv[0]["OverOperRate"].ToString() : "-");
            LblMachineRate.Text = comparsion == "-" ? comparsion : comparsion + "%";

            //workRate = dv[0]["CurOperRate"].ToString() + "%";
            workRate = (Convert.ToInt32(dv[0]["CurOperRate"]) > 0 ? dv[0]["CurOperRate"].ToString() + "%" : "-");
            //preWorkRate = dv[0]["PreOperRate"].ToString() + "%";
            preWorkRate = (Convert.ToInt32(dv[0]["PreOperRate"]) > 0 ? dv[0]["PreOperRate"].ToString() + "%" : "-");

            LblWorkRate.Text = workRate;
            LblPreWorkRate.Text = preWorkRate;

            MachneComponent.Value = Convert.ToInt32(dv[0]["CurOperRate"]);
        }

        private void RdoType_EditValueChanged(object sender, EventArgs e)
        {
            typeValue = RdoType.EditValue.ToString();

            SeriesVisibe_YN();
        }
    }
}