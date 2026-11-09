using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_MTTRView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();

        Database db = new Database();
        string query;

        string FormType = ""; //MTTR, MTBF 일단 한 화면에서 보여주기 사업계획서상

        public Frm_EQ_MTTRView()
        {
            InitializeComponent();
        }

        public Frm_EQ_MTTRView(string type)
        {
            InitializeComponent();

            FormType = type;
        }

        private void FrmMachineStatus_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"));
            DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));;

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Set_Grid(gridView1);
            Search();
        }

        private void Set_Grid(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.ShowGroupPanel = false;

			gv.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
			gv.OptionsView.AllowHtmlDrawHeaders = true;

			grid.Create_GvColumn(gv, "설비명", "MachineCode");
            grid.Create_GvColumn(gv, "가동시간<br>(HR)", "OPER_HH");
            grid.Create_GvColumn(gv, "비가동시간<br>(HR)", "NONWORK_HH");
            grid.Create_GvColumn(gv, "비가동<br>횟수", "NONWORK_CNT");
            grid.Create_GvColumn(gv, "MTTR", "MTTR");
            grid.Create_GvColumn(gv, "MTBF", "MTBF");
            grid.Create_GvColumn(gv, "주요<br>비가동원인1", "NONWORK_NM1");
            grid.Create_GvColumn(gv, "주요<br>비가동원인2", "NONWORK_NM2");

            ////
            //gv.Columns["DgpName"].MinWidth = 200;
            //gv.Columns["DgpTime"].MinWidth = 200;

            grid.SetCol_Type(gv.Columns["OPER_HH"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NONWORK_HH"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NONWORK_CNT"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["MTTR"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["MTBF"], clsGrid.TypeList.TFloat);

            if(this.FormType == "MTTR")
			{
                gv.Columns["MTBF"].Visible = false;
			}
			else if(FormType == "MTBF")
			{
                gv.Columns["MTTR"].Visible = false;
			}

			////
			gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
			gv.Columns["NONWORK_NM1"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
			gv.Columns["NONWORK_NM2"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;


            //
            grid.RowNumber_Indicator(gv);
			//gv.Columns["Gauge_Type"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
		}

        private void Search()
        {
            query = $@"
                        SET ANSI_WARNINGS OFF
                        SET ARITHIGNORE ON
                        SET ARITHABORT OFF


                        DECLARE @CorpId VARCHAR(4) = '{WrGlobal.CorpID}'
                        , @STDT VARCHAR(8) = '{Dt_Date.smtStartDateTimeValue.ToString("yyyyMMdd")}'
                        , @ENDDT VARCHAR(8)= '{Dt_Date.smtEndDateTimeValue.ToString("yyyyMMdd")}'

                        DECLARE @TABLE TABLE
                        (
   	                        MachineCode VARCHAR(20)
   	                        , WORKDATE VARCHAR(8)

   	                        , WorkHH Float
   	                        , NONWORK_CNT NUMERIC(13,0)	DEFAULT 0
   	                        , NONWORK_HH  NUMERIC(13,0)	DEFAULT 0
   	                        , REPAIR_HH   NUMERIC(13,0)	DEFAULT 0
   	                        , REPAIR_CNT   NUMERIC(13,0)	DEFAULT 0

   	                        , OPER_HH NUMERIC(13,0)		DEFAULT 0
   	                        , MTTF NUMERIC(13,0)
                            , MTTR NUMERIC(13,0)
                            , MTBF NUMERIC(13,0)

   	                        , ItemCode1 VARCHAR(20)
   	                        , ItemCode2 VARCHAR(20)

                        )

                        --작업시작시간, 종료시간, 가동시간
                        INSERT INTO @TABLE
                        (
                        MachineCode
                        --, WORKDATE
                        , WorkHH
                        )
                        SELECT
	                        OP.MachineCode
	                        , DATEDIFF(HH,  MIN(OP.StartDate), MAX(OP.EndDate)) AS WorkHH
                        FROM PM_OrderProcess OP
                        INNER JOIN PM_OrderResult ORR ON OP.OrderNo = ORR.OrderNo AND OP.OrderSeq = ORR.OrderSeq
                        WHERE OP.CorpId = @CorpId
                        AND CONVERT(CHAR(10), ORR.WorkDate, 23) BETWEEN @STDT AND @ENDDT
                        AND OP.MachineCode <> ''
                        GROUP BY OP.MachineCode

                        --비가동횟수, 비가동 시간 구하기
                        UPDATE @TABLE
                        SET NONWORK_CNT = CNT, NONWORK_HH = Stop_Time_HH
                        FROM @TABLE A,
                        (
	                        SELECT
		                        MachineCode
		                        , COUNT(*) AS CNT, CAST(SUM(DATEDIFF(MI, StartTime, EndTime)) / 60.0 AS FLOAT) AS Stop_Time_HH
		                        , SUM(DATEDIFF(MI, StartTime, EndTime))/ COUNT(*) AS MTTF
	                        FROM EQ_Operation
	                        WHERE CorpId = @CorpId
	                        AND WorkDay BETWEEN @STDT AND @ENDDT
	                        GROUP BY MachineCode
	                        HAVING SUM(DATEDIFF(MI, StartTime, EndTime)) > 0
                        ) B
                        WHERE A.MachineCode = B.MachineCode

                        --수리횟수, 수리 시간 구하기
                        UPDATE @TABLE
                        SET REPAIR_HH = B.REPAIR_HH
                        , REPAIR_CNT = CNT
                        FROM @TABLE A
                        ,
                        (
   	                        --수리
   	                        SELECT MachineCode
   	                        --, Repair_Date
   	                        , CAST(SUM(DATEDIFF(MINUTE, Repair_Start_Time, Repair_End_Time)) / 60.0 AS FLOAT) AS REPAIR_HH
                            , COUNT(*) CNT
   	                        FROM EQ_Repair
   	                        WHERE CorpId = @CorpId
	                        AND Repair_Date BETWEEN @STDT AND @ENDDT
   	                        AND MachineCode <> ''
   	                        GROUP BY MachineCode--, Repair_Date
                        ) B
                        WHERE A.MachineCode = B.MachineCode --AND A.WORKDATE = B.Repair_Date

                        --가동시간
                        UPDATE @TABLE
                        SET OPER_HH = WorkHH - NONWORK_HH

                        --MTTF, MTTR
                        UPDATE @TABLE
                        SET MTTF = OPER_HH / REPAIR_CNT
                          , MTTR = REPAIR_HH/REPAIR_CNT

                        --MTBF
                        UPDATE @TABLE
                        SET MTBF = MTTF + MTTR

                        --주요 비가동 원인 TOP 2
                        ;WITH CTE_NONWORK(MachineCode, ItemCode, CNT, ROWNUM)
                        AS
                        (
                        SELECT MachineCode, ItemCode, COUNT(*) AS CNT, ROW_NUMBER() OVER(ORDER BY MachineCode, COUNT(*) DESC) AS ROWNUM
                        FROM EQ_Operation
                        WHERE CorpId = @CorpId
                        AND WorkDay BETWEEN @STDT AND @ENDDT
                        GROUP BY MachineCode, ItemCode
                        )

                        UPDATE @TABLE
                        SET ItemCode1 = B.ItemCode
                        FROM @TABLE A, CTE_NONWORK B
                        WHERE A.MachineCode = B.MachineCode
                        AND B.ROWNUM = 1

						;WITH CTE_NONWORK(MachineCode, ItemCode, CNT, ROWNUM)
                        AS
                        (
                        SELECT MachineCode, ItemCode, COUNT(*) AS CNT, ROW_NUMBER() OVER(ORDER BY MachineCode, COUNT(*) DESC) AS ROWNUM
                        FROM EQ_Operation
                        WHERE CorpId = @CorpId
                        AND WorkDay BETWEEN @STDT AND @ENDDT
                        GROUP BY MachineCode, ItemCode
                        )

						UPDATE @TABLE
                        SET ItemCode2 = C.ItemCode
                        FROM @TABLE A, CTE_NONWORK C
                        WHERE A.MachineCode = C.MachineCode
                        AND C.ROWNUM = 2

                        --최종 조회
                        SELECT A.*
                        , B.MachineName, C.Title AS NONWORK_NM1, D.Title AS NONWORK_NM2
                        FROM @TABLE A
                        LEFT JOIN EQ_Machine B ON B.CorpId = @CorpId AND A.MachineCode = B.MachineCode
                        LEFT JOIN CM_CateItem C ON C.CorpId = @CorpId AND C.CLASS LIKE '2030%' AND A.ItemCode1 = C.ItemCode
                        LEFT JOIN CM_CateItem D ON D.CorpId = @CorpId AND D.CLASS LIKE '2030%' AND A.ItemCode2 = D.ItemCode
                        WHERE A.MachineCode IS NOT NULL AND A.MachineCode <> 'NULL'
                        ";
            DataTable dt = db.GetDataTable(query);

            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();


            //비가동유형별 점유율
            query = $@"
            DECLARE   @CorpId VARCHAR(4) = '{WrGlobal.CorpID}'
                    , @STDT VARCHAR(8) = '{Dt_Date.smtStartDateTimeValue.ToString("yyyyMMdd")}'
                    , @ENDDT VARCHAR(8)= '{Dt_Date.smtEndDateTimeValue.ToString("yyyyMMdd")}'


            SELECT A.ItemCode, SUM(DATEDIFF(MI, StartTime, EndTime)) AS Stop_Time, B.Title, CAST(SUM(DATEDIFF(MI, StartTime, EndTime)) / 60.0 AS FLOAT) AS Stop_Time_HH
            FROM EQ_Operation A
            LEFT JOIN CM_CateItem B ON A.CorpId = B.CorpId AND B.CLASS LIKE '2030%' AND A.ItemCode = B.ItemCode
            WHERE A.CorpId = @CorpId
            AND A.WorkDay BETWEEN @STDT AND @ENDDT
            GROUP BY A.ItemCode, B.Title

            ";
            DataTable dtChart = db.GetDataTable(query);

            chartControl1.AnimationStartMode = ChartAnimationMode.OnDataChanged;
            chartControl1.Legend.Visibility = DefaultBoolean.True;
            chartControl1.SelectionMode = ElementSelectionMode.Single;
            chartControl1.SeriesSelectionMode = SeriesSelectionMode.Point;
            chartControl1.DataSource = dtChart;

            chartControl1.Series.Clear();

            Series series = new Series("NonWorkType", ViewType.Doughnut);
            series.ArgumentDataMember = "Title";
            series.ValueDataMembersSerializable = "Stop_Time_HH";

            //TOP N개 옵션
            series.TopNOptions.Enabled = true;
            series.TopNOptions.Mode = TopNMode.Count;
            series.TopNOptions.Count = 5;
            series.TopNOptions.ShowOthers = true;
            series.TopNOptions.OthersArgument = "기타";
            series.Label.TextPattern = "{A} ({VP:P2})";
            series.LabelsVisibility = DefaultBoolean.Default;
			series.LegendTextPattern = "{A}";
            series.DataSource = dtChart;

            chartControl1.Series.Add(series);

//            //MTTR 추이
//            query = $@"

//SET ANSI_WARNINGS OFF
//SET ARITHIGNORE ON
//SET ARITHABORT OFF


//DECLARE @CorpId VARCHAR(2) = '01'
//, @STDT VARCHAR(8) = '{barEditEndDate.DateTime.AddMonths(-11).ToString("yyyyMMdd")}'	--당월 - 11개월
//, @ENDDT VARCHAR(8)= '{barEditEndDate.DateTime.ToString("yyyyMMdd")}'	--당월
//, @MachineCode VARCHAR(10) ='ALL'

//DECLARE @TABLE TABLE
//(
//	MachineCode VARCHAR(20)
//	, WORKDATE VARCHAR(8)

//	, WorkHH NUMERIC(13,0)
//	, NONWORK_CNT NUMERIC(13,0)	DEFAULT 0
//	, NONWORK_HH  NUMERIC(13,0)	DEFAULT 0
//	, REPAIR_HH   NUMERIC(13,0)	DEFAULT 0
//	, REPAIR_HH   NUMERIC(13,0)	DEFAULT 0

//	, OPER_HH NUMERIC(13,0)		DEFAULT 0
//	, MTTF NUMERIC(13,0)
//	, MTTR NUMERIC(13,0)
//	, MTBF NUMERIC(13,0)

//	, NONWORK_CD1 VARCHAR(20)
//	, NONWORK_CD2 VARCHAR(20)

//)

///*

//MTTF = 총가동시간/고장건수
//MTTR = 총고장시간/고장건수
//MTBF = MTTF + MTTR

//*/

//--작업시작시간, 종료시간, 가동시간
//INSERT INTO @TABLE
//(
//MachineCode
//, WORKDATE
//, WorkHH
//)
//SELECT MachineCode
//, LEFT(Rst_Date, 6)
//,  DATEDIFF(HH,  MIN(Start_Time), MAX(End_Time)) AS WorkHH
//FROM PD_ProcessResult
//WHERE CorpId = @CorpId
//AND Rst_Date BETWEEN @STDT AND @ENDDT
//AND MachineCode <> ''
//GROUP BY MachineCode, LEFT(Rst_Date, 6)
//HAVING MachineCode = IIF(@MachineCode ='ALL', MachineCode, @MachineCode)


//--비가동횟수, 비가동 시간 구하기
//UPDATE @TABLE
//SET NONWORK_CNT = CNT, NONWORK_HH = Stop_Time_HH
//FROM @TABLE A,
//(
//SELECT MachineCode
//, LEFT(NonWork_Date, 6) AS NonWork_Date
//, COUNT(*) AS CNT, CAST(SUM(Stop_Time) / 60.0 AS FLOAT) AS Stop_Time_HH
// , SUM(Stop_Time)/ COUNT(*) AS MTTF
// FROM PD_NonWork_Class
//WHERE CorpId = @CorpId
//AND NonWork_Date BETWEEN @STDT AND @ENDDT
//GROUP BY MachineCode, LEFT(NonWork_Date, 6)
//HAVING SUM(Stop_Time) > 0
//) B
//WHERE A.MachineCode = B.MachineCode AND A.WORKDATE = B.NonWork_Date


//--수리 소요시간 구하기
//UPDATE @TABLE
//SET REPAIR_HH = B.REPAIR_HH
//FROM @TABLE A
//,
//(
//	--수리
//	SELECT MachineCode
//	, LEFT(Repair_Date, 6) AS Repair_Date
//	, CAST(SUM(DATEDIFF(MM, Repair_Start_Time, Repair_End_Time)) / 60.0 AS FLOAT) AS REPAIR_HH
//	FROM EQ_Machine_Repair
//	WHERE CorpId = @CorpId
//	AND Repair_Date BETWEEN @STDT AND @ENDDT
//	AND MachineCode <> ''
//	GROUP BY MachineCode, Repair_Date
//) B
//WHERE A.MachineCode = B.MachineCode AND A.WORKDATE = B.Repair_Date


//--가동시간
//UPDATE @TABLE
//SET OPER_HH = WorkHH - NONWORK_HH

//--MTTF, MTTR
//UPDATE @TABLE
//SET MTTF =OPER_HH / NONWORK_CNT-- IIF(NONWORK_CNT = 0, OPER_HH, OPER_HH / NONWORK_CNT)
//, MTTR =  NONWORK_HH / NONWORK_CNT--IIF(NONWORK_CNT = 0, NONWORK_HH, NONWORK_HH / NONWORK_CNT)

//--MTBF
//UPDATE @TABLE
//SET MTBF = MTTF + MTTR


//SELECT WORKDATE, SUM(MTTR) MTTR, SUM(MTBF) MTBF
//FROM @TABLE A
//GROUP BY WORKDATE



//";
//            DataTable dtMTTR = db.GetDataTable(query);
//            chartControl2.DataSource = dtMTTR;

//            chartControl2.Series.Clear();

//            Series series1 = new Series("Line", ViewType.Line);
//            series1.ArgumentDataMember = "WORKDATE";
//            series1.ValueDataMembersSerializable = "MTTR";
//            series1.DataSource = dtMTTR;

//            chartControl2.Series.Add(series1);
        }

        private void ChartWeekNgType_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            e.LegendText = string.Format("{0}  :  {1}", e.SeriesPoint.Argument, e.SeriesPoint.Values[0]);
        }


        private void ChartNgTypeWork_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            e.LegendText = string.Format("{0}  :  {1}", e.SeriesPoint.Argument, e.SeriesPoint.Values[0]);
        }

        private void ChartNgTypeMachine_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            e.LegendText = string.Format("{0}  :  {1}", e.SeriesPoint.Argument, e.SeriesPoint.Values[0]);
        }

        private void BarEditStartDate_EditValueChanged(object sender, EventArgs e)
        {
            //if (barEditStartDate.EditValue is null)
            //    return;

            //Display();
        }

        private void BarEditEndDate_EditValueChanged(object sender, EventArgs e)
        {
            //if (barEditEndDate.EditValue is null)
            //    return;

            //Display();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}