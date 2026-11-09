using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using SmartCommon.Common;
using SmartMES.Common;
using System.Threading.Tasks;

namespace SmartMES.View.Monitoring
{
    public partial class Frm_TotalMonitoring : Form
    {
        Database db = new Database();

        //

        // 새로고침 중복 실행 방지
        private bool isRefreshing = false;

        // ── 색상 정의
        static readonly Color C_BG = Color.FromArgb(0x0a, 0x0f, 0x1e);
        static readonly Color C_PANEL = Color.FromArgb(0x11, 0x18, 0x27);
        static readonly Color C_BORDER = Color.FromArgb(0x1e, 0x30, 0x55);
        static readonly Color C_TEXT = Color.FromArgb(0xdc, 0xee, 0xff);
        static readonly Color C_MUTED = Color.FromArgb(0x5a, 0x7a, 0xaa);
        static readonly Color C_DIVIDER = Color.FromArgb(0x1a, 0x28, 0x40);
        static readonly Color C_BLUE = Color.FromArgb(0x00, 0x96, 0xff);
        static readonly Color C_GREEN = Color.FromArgb(0x00, 0xd2, 0x8c);
        static readonly Color C_ORANGE = Color.FromArgb(0xff, 0xa0, 0x00);
        static readonly Color C_RED = Color.FromArgb(0xff, 0x46, 0x50);
        static readonly Color C_PURPLE = Color.FromArgb(0xa0, 0x50, 0xdc);

        // ── 데이터
        public double[] gaugeVals = { 0, 0, 0, 0, 0, 0, 0, 0 };
        public Color[] gaugeColors;
        public string[] gaugeLabels = { "", "", "", "", "", "", "", "" };

        public double[] trendActual = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public double[] trendPlan = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public double[] barActual = { 0, 0, 0, 0, 0 };
        public double[] barPlan = { 0, 0, 0, 0, 0 };
        public string[] barLabels = { "", "", "", "", "" };
        public double[] costVals = { 0, 0, 0, 0 };
        public double[] costTargets = { 0, 0, 0, 0 };
        public string[] costNames = { "", "", "", "" };
        public Color[] costColors;

        //public int[] defectVals = { 0, 0, 0, 0, 0 };
        //public string[] defectLabels = { "", "", "", "", "" };
        public Color[] defectColors;

        // 품질 합격률 영역에 표시할 불량유형 데이터
        public int[] qualityDefectVals = { 0 };
        public string[] qualityDefectLabels = { "" };

        // 당월 최다 불량유형 TOP5 데이터
        public int[] AssemblyDefectVals = { 0 };
        public string[] AssemblyDefectLabels = { "" };

        public Color[] alarmDots;
        public string[] alarmTimes = { "00:00", "00:00", "00:00", "00:00", "00:00" };
        public string[] alarmMsgs = { "", "", "", "", "" };

        // 사출/조립 불량유형 선그래프용 데이터
        public double[] injectDefectLineVals = { 0 };
        public double[] assemblyDefectLineVals = { 0 };
        public string[] defectLineLabels = { "" };


        // KPI 데이터
        readonly int kpiSize = 4;
        public string[] vals;
        public string[] subs;

        // ── 애니메이션
        double animT = 0;

        //재조회 시간 관리
        private const int REFRESH_COUNT = 60;
        int refreshCnt = 0;

        public Frm_TotalMonitoring()
        {
            InitializeComponent();

            vals = new string[kpiSize];
            subs = new string[kpiSize];

            gaugeColors = new Color[] { C_GREEN, C_BLUE, C_ORANGE, C_BLUE, C_ORANGE, C_GREEN, C_PURPLE, C_BLUE };
            costColors = new Color[] { C_BLUE, C_GREEN, C_ORANGE, C_RED };
            defectColors = new Color[] { C_RED, C_ORANGE, C_PURPLE, C_BLUE, Color.FromArgb(0x3a, 0x5a, 0x8a) };
            alarmDots = new Color[] { C_RED, C_ORANGE, C_ORANGE, C_GREEN, C_BLUE };

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            Text = "생산·품질·설비 종합 모니터링";
            Size = new Size(1440, 900);
            MinimumSize = new Size(1200, 760);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = C_BG;
            DoubleBuffered = true;

            animT = 1.0;

            Paint += OnPaint;
            Resize += (s, e) => Invalidate();

            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
        }

        private async void Frm_TotalMonitoring_Shown(object sender, EventArgs e)
        {
            // 데이터 조회
            await Task.Run(Search);

            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (isClosing) return;
            if (this.IsDisposed || this.Disposing) return;
            if (isRefreshing) return;

            timer.Stop();

            try
            {
                isRefreshing = true;

                refreshCnt++;
                if (refreshCnt > REFRESH_COUNT)
                {
                    refreshCnt = 0;
                    await Task.Run(Search);
                }

                if (!isClosing && !this.IsDisposed && !this.Disposing)
                {
                    if (refreshCnt == 0)
                    {
                        Invalidate();
                    }

                    else
                    {
                        int W = ClientSize.Width;
                        int H = ClientSize.Height;
                        int P = 12;

                        Rectangle rect = new Rectangle(P, P, W - P * 2, 56);
                        Invalidate(rect);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                isRefreshing = false;

                if (!isClosing && !this.IsDisposed && !this.Disposing && timer != null)
                {
                    timer.Start();
                }
            }
        }

        public void Search()
        {
            LoadGauge();
            LoadTrend();
            LoadBar();
            LoadCost();

            //LoadDefect();
            LoadKPI();
            LoadDefect("PR001", out qualityDefectVals, out qualityDefectLabels);
            LoadDefect("PR002", out AssemblyDefectVals, out AssemblyDefectLabels);

            LoadAlarm();
        }

        //동그라미 게이지 표 데이터
        public void LoadGauge()
        {
            DataTable dtProcess = ProcessList();
            DataRow[] drProcess = dtProcess.Select("ProcessCode <> 'ALL'");

            string processCode1 = drProcess[0]["ProcessCode"].ConvertString_Null_To_Empty();
            string processCode2 = drProcess[1]["ProcessCode"].ConvertString_Null_To_Empty();

            string[] processList = { "수주대비","지시대비", processCode1, processCode2, "주간가동률", "야간가동률", "수입검사","초중종물검사" };

            gaugeVals = new double[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            DataTable dt = null;

            for (int i = 0; i < processList.Length; i++)
            {
                switch (processList[i])
                {
                    case "수주대비":
                    case "지시대비":
                        dt = Month_OrderRate(processList[i]);
                        break;

                    case "PR001":
                    case "PR002":
                        dt = MonthProcessDate(processList[i]);
                        break;

                    case "수입검사":
                        dt = Month_QTRate("10");
                        break;

                    case "초중종물검사":
                        dt = Month_QTRate("40");
                        break;

                    case "주간가동률":
                        dt = Month_MachineRate("10");
                        break;

                    case "야간가동률":
                        dt = Month_MachineRate("20");
                        break;
                    default:
                        break;
                }

                gaugeVals[i] = (dt.Rows.Count > 0)
                    ? ToDouble(dt.Rows[0]["ProgressRate"])
                    : 0;
            }

            string processName1 = drProcess[0]["ProcessName"].ConvertString_Null_To_Empty();
            string processName2 = drProcess[1]["ProcessName"].ConvertString_Null_To_Empty();

            gaugeLabels = new string[] { "수주대비 달성률","지시대비 달성률", processName1, processName2, "주간","야간","수입검사","초중종검사" };
        }

        public void LoadKPI()
        {

            string val = "";
            string sub = "";

            for (int i = 0; i < kpiSize; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            DataTable dt_Prod = MonthProcessNowDate();
                            val = dt_Prod.Rows.Count == 0 ? "0" : $"{dt_Prod.Rows[0]["Work_Qty"].ConvertString_Null_To_Empty()}";
                            sub = dt_Prod.Rows.Count == 0 ? "0" : $"{dt_Prod.Rows[0]["Target_Qty"].ConvertString_Null_To_Empty()}";
                            if (decimal.TryParse(val, out decimal num))
                            {
                                val = num.ToString("#,##0") + " EA";
                            }

                            if (decimal.TryParse(sub, out decimal sub_num))
                            {
                                sub = "목표수량 " + sub_num.ToString("#,##0") + " EA";
                            }
                        }
                        break;
                    case 1:
                        {
                            DataTable dtDefect = Month_DefectRate();
                            val = dtDefect.Rows.Count == 0 ? "-" : $"{Convert.ToDecimal(dtDefect.Rows[0]["Defect_PPM"] == DBNull.Value ? 0 : dtDefect.Rows[0]["Defect_PPM"]): #,##0} PPM";
                            sub = "";
                        }
                        break;
                    case 2:// 당월 품질 합격률
                        {
                            DataTable dtQT = Month_QTRate();
                            val = dtQT.Rows.Count == 0 ? "-" : $"{dtQT.Rows[0]["ProgressRate"].ConvertString_Null_To_Empty()} %";
                            sub = "";
                        }
                        break;

                    case 3:// 당월 설비 가동률
                        {
                            DataTable dtMachine = Month_MachineRate();
                            val = dtMachine.Rows.Count == 0 ? "0 %" : $"{dtMachine.Rows[0]["ProgressRate"].ConvertString_Null_To_Empty()} %";
                            sub = "";
                        }
                        break;
                    default:
                        val = "";
                        sub = "";
                        break;
                }

                vals[i] = val;
                subs[i] = sub;
            }
        }

        // 생산추이 선 그래프
        public void LoadTrend()
        {
            string query = $@"
                    /* 
                        주차 기준을 월요일 시작으로 고정
                    */
                    SET DATEFIRST 1;

                    DECLARE @Today DATE = CONVERT(DATE, GETDATE());

                    /*
                        이번 주 월요일
                    */
                    DECLARE @ThisWeekStart DATE 
                        = DATEADD(DAY, 1 - DATEPART(WEEKDAY, @Today), @Today);

                    WITH Last7Weeks AS
                    (
                        /*
                            최근 7주
                            W1 : 가장 오래된 주
                            W7 : 현재 주
                        */
                        SELECT
                              1 AS WeekSeq
                            , DATEADD(WEEK, -6, @ThisWeekStart) AS WeekStartDate

                        UNION ALL

                        SELECT
                              WeekSeq + 1
                            , DATEADD(WEEK, 1, WeekStartDate)

                        FROM Last7Weeks

                        WHERE WeekSeq < 7
                    ),

                    ResultBase AS
                    (
                        /*
                            생산실적
                        */
                        SELECT
                              CONVERT(DATE, R.StartDate) AS WorkDate

                            , SUM(ISNULL(R.WorkQty, 0)) AS Work_Qty

                        FROM PM_OrderResult R

                        WHERE R.CorpId = '1000'

                        GROUP BY
                            CONVERT(DATE, R.StartDate)
                    ),

                    PlanBase AS
                    (
                        /*
                            생산계획
                        */
                        SELECT
                              CONVERT(DATE, P.OrderDate) AS PlanDate

                            , SUM(ISNULL(P.OrderQty, 0)) AS Plan_Qty

                        FROM PM_OrderProcess P

                        WHERE P.CorpId = '1000'

                        GROUP BY
                            CONVERT(DATE, P.OrderDate)
                    )

                    SELECT
                          'W' + CONVERT(VARCHAR(10), W.WeekSeq) AS DayName

                        , CONVERT(CHAR(10), W.WeekStartDate, 120) AS WeekStartDate

                        , CONVERT(
                            CHAR(10),
                            DATEADD(DAY, 6, W.WeekStartDate),
                            120
                          ) AS WeekEndDate

                        , ISNULL(SUM(R.Work_Qty), 0) AS Work_Qty
                          -- 생산실적

                        , ISNULL(SUM(P.Plan_Qty), 0) AS Plan_Qty
                          -- 생산계획

                    FROM Last7Weeks W

                    LEFT JOIN ResultBase R
                        ON R.WorkDate >= W.WeekStartDate
                        AND R.WorkDate < DATEADD(WEEK, 1, W.WeekStartDate)

                    LEFT JOIN PlanBase P
                        ON P.PlanDate >= W.WeekStartDate
                        AND P.PlanDate < DATEADD(WEEK, 1, W.WeekStartDate)

                    GROUP BY
                          W.WeekSeq
                        , W.WeekStartDate

                    ORDER BY
                        W.WeekSeq

                    OPTION (MAXRECURSION 7);
                ";

            DataTable dt = db.GetDataTable(query);

            MapToDoubleArrays(dt, "Work_Qty", "Plan_Qty", out trendActual, out trendPlan);
        }

        // 공정별 생산량 비교 바 그래프
        public void LoadBar()
        {
            /*
                - 라인 기준 → 설비 기준 변경
                - EQ_Machine 사용
                - MachineName 기준 표시
            */

            string query = $@"
                    DECLARE 
                          @StartDate CHAR(8) = '{DateTime.Now.ConvertDate_To_Char8()}'
                        , @EndDate   CHAR(8) = '{DateTime.Now.ConvertDate_To_Char8()}';

                    SELECT
                          ISNULL(M.MachineName, '-') AS Line_Nm

                        , ISNULL(SUM(R.WorkQty), 0) AS Work_Qty
                          -- 생산실적

                        , ISNULL(SUM(P.OrderQty), 0) AS Plan_Qty
                          -- 생산계획

                    FROM PM_OrderResult R
                    LEFT JOIN PM_OrderProcess P
                        ON P.CorpId = R.CorpId
                        AND P.OrderNo = R.OrderNo
                        AND P.OrderSeq = R.OrderSeq
                        AND P.ProcessCode = R.ProcessCode
                        AND P.MachineCode = R.MachineCode

                    LEFT JOIN EQ_Machine M ON M.CorpId = R.CorpId AND M.MachineCode = R.MachineCode

                    WHERE R.CorpId = '1000'
                        AND ISNULL(R.MachineCode, '') <> ''
                        AND R.WorkDate BETWEEN @StartDate AND @EndDate

                    GROUP BY M.MachineName
                    ORDER BY M.MachineName;
                ";

            DataTable dt = db.GetDataTable(query);

            int n = dt.Rows.Count;

            barActual = new double[n];
            barPlan = new double[n];
            barLabels = new string[n];

            for (int i = 0; i < n; i++)
            {
                barActual[i] = ToDouble(dt.Rows[i]["Work_Qty"]);
                barPlan[i] = ToDouble(dt.Rows[i]["Plan_Qty"]);
                barLabels[i] = dt.Rows[i]["Line_Nm"].ToString();
            }
        }
        //제조 경비현황 중간 맨아래
        // 설비 성능 가동률 쿼리
        public void LoadCost()
        {
            string query = $@"
        DECLARE 
              @CorpId      VARCHAR(10) = '{WrGlobal.CorpID}'
            , @StartDate   CHAR(8)    = '{DateTime.Now.ConvertDate_To_Char8()}'
            , @EndDate     CHAR(8)    = '{DateTime.Now.ConvertDate_To_Char8()}'
            , @ProcessCode VARCHAR(5) = '';

        ;WITH NW AS
        (
            /*
                비가동시간 집계
            */
            SELECT
                  N.CorpId
                , N.OrderNo
                , N.OrderSeq
                , N.MachineCode

                , SUM(
                    ISNULL(N.StopTime, 0)
                  ) AS NonWork_Minute

            FROM EQ_Operation N

            WHERE N.CorpId = @CorpId
                AND N.WorkDay BETWEEN @StartDate AND @EndDate
                AND (@ProcessCode IS NULL OR @ProcessCode = '' OR N.ProcessCode = @ProcessCode)
                AND N.OperationYn = 'N'

            GROUP BY
                  N.CorpId
                , N.OrderNo
                , N.OrderSeq
                , N.MachineCode
        ),

        RT AS
        (
            SELECT
                  A.CorpId
                , A.OrderNo
                , A.OrderSeq
                , A.WorkDate
                , A.MachineCode

                , M.MachineName AS SET_Place

                /*
                    CT 대체값
                    기존 BS_Item.CT 없음
                    임시 1 사용
                */
                , 1 AS CT

                , ISNULL(A.WorkQty, 0) AS Work_Qty
                , ISNULL(A.NgQty, 0)   AS Bad_Qty

                /*
                    작업시간
                */
                , CASE
                    WHEN A.StartDate IS NULL
                      OR A.EndDate IS NULL
                    THEN 0

                    WHEN DATEDIFF(MINUTE, A.StartDate, A.EndDate) < 0
                    THEN 0

                    ELSE DATEDIFF(MINUTE, A.StartDate, A.EndDate)
                  END AS 작업시간

                /*
                    비가동시간
                */
                , ISNULL(NW.NonWork_Minute, 0) AS 비가동시간

                /*
                    가동시간
                */
                , CASE
                    WHEN
                    (
                        CASE
                            WHEN A.StartDate IS NULL
                              OR A.EndDate IS NULL
                            THEN 0

                            WHEN DATEDIFF(MINUTE, A.StartDate, A.EndDate) < 0
                            THEN 0

                            ELSE DATEDIFF(MINUTE, A.StartDate, A.EndDate)
                        END
                        - ISNULL(NW.NonWork_Minute, 0)
                    ) < 0
                    THEN 0

                    ELSE
                    (
                        CASE
                            WHEN A.StartDate IS NULL
                              OR A.EndDate IS NULL
                            THEN 0

                            WHEN DATEDIFF(MINUTE, A.StartDate, A.EndDate) < 0
                            THEN 0

                            ELSE DATEDIFF(MINUTE, A.StartDate, A.EndDate)
                        END
                        - ISNULL(NW.NonWork_Minute, 0)
                    )
                  END AS 가동시간

                /*
                    목표시간
                */
                , 660 AS 가동목표시간

            FROM PM_OrderResult A

            LEFT JOIN NW
                ON NW.CorpId = A.CorpId
                AND NW.OrderNo = A.OrderNo
                AND NW.OrderSeq = A.OrderSeq
                AND NW.MachineCode = A.MachineCode

            LEFT JOIN EQ_Machine M
                ON M.CorpId = A.CorpId
                AND M.MachineCode = A.MachineCode

            WHERE A.CorpId = @CorpId
                AND A.WorkDate BETWEEN @StartDate AND @EndDate
                AND (@ProcessCode IS NULL OR @ProcessCode = '' OR A.ProcessCode = @ProcessCode)
        ),

        TARGET AS
        (
            /*
                목표시간 중복 제거
            */
            SELECT
                  SET_Place
                , MachineCode
                , WorkDate

                , MAX(가동목표시간) AS 가동목표시간

            FROM RT

            GROUP BY
                  SET_Place
                , MachineCode
                , WorkDate
        )

        , ResultAgg AS
        (
            SELECT
                  SET_Place
                , MachineCode
                , WorkDate
                , SUM(Work_Qty) AS Work_Qty
                , MAX(CT) AS CT
                
                , SUM(Work_Qty * CT / 60.0) AS Ideal_Minute
                , SUM(가동시간) AS Run_Minute

            FROM RT
            GROUP BY 
                  SET_Place
                , MachineCode
                , WorkDate
        )

        SELECT
              R.SET_Place

            /*
                성능가동률
            */
           , CASE
                WHEN SUM(R.Run_Minute) = 0 THEN NULL

                ELSE ROUND(
                        SUM(R.Ideal_Minute) 
                        / NULLIF(SUM(R.Run_Minute * 1.0), 0)
                        * 100
                    , 2)
              END AS 성능가동률

            /*
                설비가동률 = (실제 가동시간 / 가동목표시간) * 100
            */
            , CASE
                WHEN SUM(TG.가동목표시간) = 0 THEN NULL

                ELSE ROUND(
                        SUM(R.Run_Minute) * 100.0
                        / NULLIF(SUM(TG.가동목표시간), 0)
                    , 2)
              END AS 설비가동률

        FROM ResultAgg R

       LEFT JOIN TARGET TG
            ON TG.SET_Place = R.SET_Place
            AND TG.MachineCode = R.MachineCode
            AND TG.WorkDate = R.WorkDate

        GROUP BY
            R.SET_Place

        ORDER BY
            R.SET_Place;
    ";

            DataTable dt = db.GetDataTable(query);

            if (dt.Rows.Count == 0) return;

            int count = dt.Rows.Count;

            costVals = new double[count];
            costTargets = new double[count];
            costNames = new string[count];

            for (int i = 0; i < count; i++)
            {
                DataRow row = dt.Rows[i];

                costVals[i] = ToDouble(row["성능가동률"]);
                costTargets[i] = 100;
                costNames[i] = row["SET_Place"].ToString();
            }
        }

        // 불량유형 도넛그래프
        public void LoadDefect(string proc_Cd, out int[] vals, out string[] labels)
        {
            string query = $@"
        DECLARE 
              @Today     DATE        = CONVERT(DATE, GETDATE())
            , @Proc_Cd   VARCHAR(20) = '{proc_Cd}';

        DECLARE
              @TodayChar       CHAR(8)
            , @PrevMonthStart  CHAR(8)
            , @PrevMonthEnd    CHAR(8)
            , @ThisMonthStart  CHAR(8)
            , @ThisMonthEnd    CHAR(8)
            , @SevenDaysStart  CHAR(8);

        SET @TodayChar      = CONVERT(CHAR(8), @Today, 112);

        SET @PrevMonthStart = CONVERT(
                                    CHAR(8),
                                    DATEADD(MONTH, DATEDIFF(MONTH, 0, @Today) - 1, 0),
                                    112
                                 );

        SET @PrevMonthEnd   = CONVERT(
                                    CHAR(8),
                                    EOMONTH(DATEADD(MONTH, -1, @Today)),
                                    112
                                 );

        SET @ThisMonthStart = CONVERT(
                                    CHAR(8),
                                    DATEFROMPARTS(YEAR(@Today), MONTH(@Today), 1),
                                    112
                                 );

        SET @ThisMonthEnd   = @TodayChar;

        /*
            오늘 포함 최근 7일
        */
        SET @SevenDaysStart = CONVERT(
                                    CHAR(8),
                                    DATEADD(DAY, -6, @Today),
                                    112
                                 );

        ;WITH Last7Days AS
        (
            /*
                최근 7일 생성
            */
            SELECT
                  0 AS D_Offset
                , @Today AS WorkDate

            UNION ALL

            SELECT
                  D_Offset + 1
                , DATEADD(DAY, -1, WorkDate)

            FROM Last7Days

            WHERE D_Offset < 6
        ),

        BaseData AS
        (
            /*
                생산실적 기준
            */
            SELECT
                  A.CorpId
                , A.ProcessCode

                , A.WorkDate

                , ISNULL(A.WorkQty, 0) AS Work_Qty
                , ISNULL(A.NgQty, 0)   AS Ng_Qty

            FROM PM_OrderResult A

            WHERE A.CorpId = '{WrGlobal.CorpID}'

                AND (
                    @Proc_Cd = ''
                    OR A.ProcessCode = @Proc_Cd
                )

                /*
                    전월 시작일부터 오늘까지
                */
                AND A.WorkDate BETWEEN @PrevMonthStart AND @TodayChar
        ),

        ResultBase AS
        (
            /*
                1. 전월 불량
            */
            SELECT
                  0 AS SortSeq
                , '전월불량' AS DayName

                , @PrevMonthStart AS StartDate
                , @PrevMonthEnd AS EndDate

                , SUM(ISNULL(Ng_Qty, 0)) AS Work_Qty

            FROM BaseData

            WHERE WorkDate BETWEEN @PrevMonthStart AND @PrevMonthEnd

            UNION ALL

            /*
                2. 최근 7일 일별 불량
            */
            SELECT
                  7 - D.D_Offset AS SortSeq

                , 'D-' + CONVERT(VARCHAR(10), D.D_Offset) AS DayName

                , CONVERT(CHAR(8), D.WorkDate, 112) AS StartDate
                , CONVERT(CHAR(8), D.WorkDate, 112) AS EndDate

                , ISNULL(SUM(B.Ng_Qty), 0) AS Work_Qty

            FROM Last7Days D

            LEFT JOIN BaseData B
                ON B.WorkDate = CONVERT(CHAR(8), D.WorkDate, 112)

            GROUP BY
                  D.D_Offset
                , D.WorkDate

            UNION ALL

            /*
                3. 당월 불량
            */
            SELECT
                  8 AS SortSeq
                , '당월불량' AS DayName

                , @ThisMonthStart AS StartDate
                , @ThisMonthEnd AS EndDate

                , SUM(ISNULL(Ng_Qty, 0)) AS Work_Qty

            FROM BaseData

            WHERE WorkDate BETWEEN @ThisMonthStart AND @ThisMonthEnd
        )

        SELECT
              SortSeq
            , DayName
            , StartDate
            , EndDate

            , ISNULL(Work_Qty, 0) AS Work_Qty

        FROM ResultBase

        ORDER BY
            SortSeq

        OPTION (MAXRECURSION 7);
    ";

            DataTable dt = db.GetDataTable(query);

            int n = dt?.Rows.Count ?? 0;

            vals = new int[n];
            labels = new string[n];

            for (int i = 0; i < n; i++)
            {
                vals[i] = Convert.ToInt32(dt.Rows[i]["Work_Qty"]);
                labels[i] = dt.Rows[i]["DayName"].ToString();
            }
        }

        //알람현황 리스트
        public void LoadAlarm()
        {
            string query = @" SELECT TOP 5 CONVERT(VARCHAR(10), WriteDate, 23)  AS ntDate, Subject  FROM SM_Notice ORDER BY WriteDate DESC";
            DataTable dt = db.GetDataTable(query);

            int n = dt.Rows.Count;

            alarmTimes = new string[n];
            alarmMsgs = new string[n];

            for (int i = 0; i < n; i++)
            {
                alarmTimes[i] = dt.Rows[i]["ntDate"].ToString();
                alarmMsgs[i] = dt.Rows[i]["Subject"].ToString();
            }
        }

        public void MapToDoubleArrays(DataTable dt, string col1, string col2, out double[] arr1, out double[] arr2)
        {
            int count = dt?.Rows.Count ?? 0;

            arr1 = new double[count];
            arr2 = new double[count];

            for (int i = 0; i < count; i++)
            {
                arr1[i] = ToDouble(dt.Rows[i][col1]);
                arr2[i] = ToDouble(dt.Rows[i][col2]);
            }
        }

        double ToDouble(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return 0;

            double result;
            if (double.TryParse(obj.ToString(), out result))
                return result;

            return 0;
        }

        #region 원형그래프
        // 당월 생산 달성률
        // 수주대비 / 지시대비 달성률
        private DataTable Month_OrderRate(string select_Div = "")
        {
            string query = $@"
                            DECLARE 
                                  @CorpId      VARCHAR(10)  = '{WrGlobal.CorpID}'
                                , @StartDate   CHAR(8)      = '{DateTime.Now.ConvertDate_To_Char8()}'
                                , @EndDate     CHAR(8)      = '{DateTime.Now.ConvertDate_To_Char8()}'
                                , @Select_Div  VARCHAR(20)  = '{select_Div}'
                                , @ProcessCode VARCHAR(5)   = ''
                                , @ItemCode    NVARCHAR(50) = '';

                            ;WITH OrderBase AS
                            (
                                /*
                                    수주 기준수량
                                */
                                SELECT
                                      D.CorpId
                                    , D.SdOrderNo
                                    , D.SdOrderSeq
                                    , D.ItemCode

                                    , SUM(ISNULL(D.Qty, 0)) AS Order_Qty

                                FROM BM_SdOrderD D
                                INNER JOIN BM_SdOrderM M ON M.CorpId = D.CorpId AND M.SdOrderNo = D.SdOrderNo
                                WHERE D.CorpId = @CorpId

                                    AND CONVERT(CHAR(8), M.ObtainDate, 112)
                                        BETWEEN @StartDate AND @EndDate

                                    AND (
                                        @ItemCode = ''
                                        OR D.ItemCode = @ItemCode
                                    )

                                GROUP BY
                                      D.CorpId
                                    , D.SdOrderNo
                                    , D.SdOrderSeq
                                    , D.ItemCode
                            ),

                            ResultByOrder AS
                            (
                                /*
                                    수주 기준 생산실적
                                */
                                SELECT
                                      P.CorpId
                                    , P.OrderNo
                                    , P.OrderSeq
                                    , PP.SdOrderNo
									, PP.SdOrderSeq

                                    , SUM(ISNULL(R.WorkQty, 0)) AS Work_Qty

                                FROM PM_OrderProcess P
                                LEFT JOIN PM_OrderResult R ON R.CorpId = P.CorpId AND R.OrderNo = P.OrderNo AND R.OrderSeq = P.OrderSeq AND R.ProcessCode = P.ProcessCode
								LEFT JOIN PM_ProductPlan PP ON R.CorpId = PP.CorpId AND R.OrderNo = PP.OrderNo
                                WHERE P.CorpId = @CorpId

                                    AND CONVERT(CHAR(8), P.OrderDate, 112)
                                        BETWEEN @StartDate AND @EndDate

                                    AND (
                                        @ProcessCode = ''
                                        OR P.ProcessCode = @ProcessCode
                                    )

                                    AND (
                                        @ItemCode = ''
                                        OR P.ItemCode = @ItemCode
                                    )

                                GROUP BY
                                      P.CorpId
                                    , P.OrderNo
                                    , P.OrderSeq
                                    , PP.SdOrderNo
									, PP.SdOrderSeq
                            ),

                            OrderRate AS
                            (
                                /*
                                    수주대비 달성률
                                */
                                SELECT
                                      '수주대비' AS Select_Div

                                    , SUM(ISNULL(O.Order_Qty, 0)) AS Target_Qty
                                    , SUM(ISNULL(R.Work_Qty, 0))  AS Work_Qty

                                FROM OrderBase O
                                LEFT JOIN ResultByOrder R
                                    ON R.CorpId = O.CorpId
                                    AND R.SdOrderNo = O.SdOrderNo
                                    AND R.SdOrderSeq = O.SdOrderSeq
                            ),

                            CmdBase AS
                            (
                                /*
                                    생산지시 기준수량
                                */
                                SELECT
                                      A.CorpId
                                    , A.OrderNo
                                    , A.OrderSeq
                                    , A.ProcessCode
                                    , A.ItemCode

                                    , SUM(ISNULL(A.OrderQty, 0)) AS OrderQty

                                FROM PM_OrderProcess A
                                WHERE A.CorpId = @CorpId

                                    AND CONVERT(CHAR(8), A.OrderDate, 112)
                                        BETWEEN @StartDate AND @EndDate

                                    AND (
                                        @ProcessCode = ''
                                        OR A.ProcessCode = @ProcessCode
                                    )

                                    AND (
                                        @ItemCode = ''
                                        OR A.ItemCode = @ItemCode
                                    )

                                GROUP BY
                                      A.CorpId
                                    , A.OrderNo
                                    , A.OrderSeq
                                    , A.ProcessCode
                                    , A.ItemCode
                            ),

                            ResultByCmd AS
                            (
                                /*
                                    생산실적 기준
                                */
                                SELECT
                                      R.CorpId
                                    , R.OrderNo
                                    , R.OrderSeq
                                    , R.ProcessCode

                                    , SUM(ISNULL(R.WorkQty, 0)) AS Work_Qty

                                FROM PM_OrderResult R
                                WHERE R.CorpId = @CorpId

                                    AND R.WorkDate BETWEEN @StartDate AND @EndDate

                                    AND (
                                        @ProcessCode = ''
                                        OR R.ProcessCode = @ProcessCode
                                    )

                                GROUP BY
                                      R.CorpId
                                    , R.OrderNo
                                    , R.OrderSeq
                                    , R.ProcessCode
                            ),

                            CmdRate AS
                            (
                                /*
                                    지시대비 달성률
                                */
                                SELECT
                                      '지시대비' AS Select_Div

                                    , SUM(ISNULL(C.OrderQty, 0)) AS Target_Qty
                                    , SUM(ISNULL(R.Work_Qty, 0)) AS Work_Qty

                                FROM CmdBase C
                                LEFT JOIN ResultByCmd R
                                    ON R.CorpId = C.CorpId
                                    AND R.OrderNo = C.OrderNo
                                    AND R.OrderSeq = C.OrderSeq
                                    AND R.ProcessCode = C.ProcessCode
                            ),

                            RateBase AS
                            (
                                SELECT * FROM OrderRate

                                UNION ALL

                                SELECT * FROM CmdRate
                            )

                            SELECT
                                  A.Select_Div

                                , A.Target_Qty AS 기준수량
                                , A.Work_Qty   AS 실적수량

                                , CASE
                                    WHEN ISNULL(A.Target_Qty, 0) = 0 THEN 0

                                    ELSE CAST(
                                            ROUND(
                                                A.Work_Qty
                                                / NULLIF(A.Target_Qty * 1.0, 0)
                                                * 100
                                            , 2)
                                        AS DECIMAL(10,2))
                                  END AS ProgressRate

                            FROM RateBase A

                            WHERE A.Select_Div = @Select_Div;
                        ";

            return db.GetDataTable(query);
        }

        // 당월 공정별 달성률
        // 왼쪽 위에서 두번째 원형 차트 (사출/조립)
        private DataTable MonthProcessDate(string ProcessCd = "")
        {
            string query = $@"
                                DECLARE 
                                      @CorpId     VARCHAR(10) = '{WrGlobal.CorpID}'
                                    , @ProcessCd  VARCHAR(5) = '{ProcessCd}'
                                    , @StartDate  CHAR(8)    = '{DateTime.Now.ConvertDate_To_Char8()}'
                                    , @EndDate    CHAR(8)    = '{DateTime.Now.ConvertDate_To_Char8()}';

                                ;WITH CmdBase AS
                                (
                                    /*
                                        생산지시 기준 목표수량
                                    */
                                    SELECT
                                          A.CorpId
                                        , A.ProcessCode
                                        , A.OrderNo
                                        , A.OrderSeq

                                        , SUM(ISNULL(A.OrderQty, 0)) AS OrderQty

                                    FROM PM_OrderProcess A

                                    WHERE A.CorpId = @CorpId

                                        AND CONVERT(CHAR(8), A.OrderDate, 112)
                                            BETWEEN @StartDate AND @EndDate

                                        /*
                                            특정 공정 조회
                                        */
                                        AND (
                                            @ProcessCd = ''
                                            OR A.ProcessCode = @ProcessCd
                                        )

                                    GROUP BY
                                          A.CorpId
                                        , A.ProcessCode
                                        , A.OrderNo
                                        , A.OrderSeq
                                ),

                                ResultBase AS
                                (
                                    /*
                                        생산실적 기준
                                    */
                                    SELECT
                                          A.CorpId
                                        , A.ProcessCode
                                        , A.OrderNo
                                        , A.OrderSeq

                                        , SUM(ISNULL(A.WorkQty, 0)) AS Work_Qty
                                        , SUM(ISNULL(A.GoodQty, 0)) AS Good_Qty
                                        , SUM(ISNULL(A.NgQty, 0))   AS Bad_Qty

                                    FROM PM_OrderResult A

                                    WHERE A.CorpId = @CorpId

                                        AND A.WorkDate BETWEEN @StartDate AND @EndDate

                                        AND (
                                            @ProcessCd = ''
                                            OR A.ProcessCode = @ProcessCd
                                        )

                                    GROUP BY
                                          A.CorpId
                                        , A.ProcessCode
                                        , A.OrderNo
                                        , A.OrderSeq
                                ),

                                RateBase AS
                                (
                                    SELECT
                                          C.ProcessCode

                                        /*
                                            공정별 지시수량
                                        */
                                        , SUM(ISNULL(C.OrderQty, 0)) AS OrderQty

                                        /*
                                            공정별 생산수량
                                        */
                                        , SUM(ISNULL(R.Work_Qty, 0)) AS Work_Qty

                                        /*
                                            참고용 양품 / 불량
                                        */
                                        , SUM(ISNULL(R.Good_Qty, 0)) AS Good_Qty
                                        , SUM(ISNULL(R.Bad_Qty, 0))  AS Bad_Qty

                                    FROM CmdBase C

                                    LEFT JOIN ResultBase R
                                        ON R.CorpId = C.CorpId
                                        AND R.OrderNo = C.OrderNo
                                        AND R.OrderSeq = C.OrderSeq
                                        AND R.ProcessCode = C.ProcessCode

                                    GROUP BY
                                        C.ProcessCode
                                )

                                SELECT
                                      A.ProcessCode

                                    , A.OrderQty AS 지시수량
                                    , A.Work_Qty AS 실적수량
                                    , A.Good_Qty AS 양품수량
                                    , A.Bad_Qty  AS 불량수량

                                    , CASE
                                        WHEN ISNULL(A.OrderQty, 0) = 0 THEN 0

                                        ELSE CAST(
                                                ROUND(
                                                    A.Work_Qty
                                                    / NULLIF(A.OrderQty * 1.0, 0)
                                                    * 100
                                                , 2)
                                            AS DECIMAL(10,2))
                                      END AS ProgressRate

                                FROM RateBase A

                                ORDER BY
                                    A.ProcessCode;
                            ";

            return db.GetDataTable(query);
        }

        private DataTable MonthProcessNowDate(string ProcessCd = "")
        {
            string query = $@"
                        DECLARE 
                              @CorpId     VARCHAR(10) = '{WrGlobal.CorpID}'
                            , @ProcessCd  VARCHAR(5) = '{ProcessCd}'
                            , @StartDate  CHAR(8)    = '{DateTime.Now.ConvertDate_To_Char8()}'
                            , @EndDate    CHAR(8)    = '{DateTime.Now.ConvertDate_To_Char8()}';

                        ;WITH CmdBase AS
                        (
                            /*
                                오늘 생산지시 기준 목표수량
                            */
                            SELECT
                                  A.CorpId
                                , A.OrderNo
                                , A.OrderSeq
                                , A.ProcessCode

                                , SUM(ISNULL(A.OrderQty, 0)) AS Target_Qty

                            FROM PM_OrderProcess A

                            WHERE A.CorpId = @CorpId
                                AND CONVERT(CHAR(8), A.OrderDate, 112)
                                    BETWEEN @StartDate AND @EndDate

                                AND (
                                    @ProcessCd = ''
                                    OR A.ProcessCode = @ProcessCd
                                )

                            GROUP BY
                                  A.CorpId
                                , A.OrderNo
                                , A.OrderSeq
                                , A.ProcessCode
                        ),

                        ResultBase AS
                        (
                            /*
                                오늘 생산실적 집계
                            */
                            SELECT
                                  A.CorpId
                                , A.OrderNo
                                , A.OrderSeq
                                , A.ProcessCode

                                , SUM(ISNULL(A.WorkQty, 0)) AS Work_Qty
                                , SUM(ISNULL(A.GoodQty, 0)) AS Good_Qty
                                , SUM(ISNULL(A.NgQty, 0))   AS Bad_Qty

                            FROM PM_OrderResult A

                            WHERE A.CorpId = @CorpId
                                AND A.WorkDate BETWEEN @StartDate AND @EndDate

                                AND (
                                    @ProcessCd = ''
                                    OR A.ProcessCode = @ProcessCd
                                )

                            GROUP BY
                                  A.CorpId
                                , A.OrderNo
                                , A.OrderSeq
                                , A.ProcessCode
                        ),

                        RateBase AS
                        (
                            SELECT
                                  SUM(ISNULL(C.Target_Qty, 0)) AS Target_Qty
                                , SUM(ISNULL(R.Work_Qty, 0))   AS Work_Qty
                                , SUM(ISNULL(R.Good_Qty, 0))   AS Good_Qty
                                , SUM(ISNULL(R.Bad_Qty, 0))    AS Bad_Qty

                            FROM CmdBase C

                           FULL OUTER JOIN ResultBase R
                                ON R.CorpId = C.CorpId
                                AND R.OrderNo = C.OrderNo
                                AND R.OrderSeq = C.OrderSeq
                                AND R.ProcessCode = C.ProcessCode
                        )

                        SELECT
                              ISNULL(A.Target_Qty, 0) AS Target_Qty   -- 목표량
                            , ISNULL(A.Work_Qty, 0)   AS Work_Qty     -- 생산량
                            , ISNULL(A.Good_Qty, 0)   AS Good_Qty     -- 양품량
                            , ISNULL(A.Bad_Qty, 0)    AS Bad_Qty      -- 불량량

                            , CASE
                                WHEN ISNULL(A.Target_Qty, 0) = 0 THEN 0
                                ELSE CAST(
                                        ROUND(
                                            ISNULL(A.Work_Qty, 0)
                                            / NULLIF(A.Target_Qty * 1.0, 0)
                                            * 100
                                        , 1)
                                    AS DECIMAL(10,1))
                              END AS ProgressRate

                        FROM RateBase A;
                    ";

            return db.GetDataTable(query);
        }

        // 당일불량률(PPM)
        private DataTable Month_DefectRate()
        {
            string query = $@"
                        DECLARE
                              @CorpId    VARCHAR(10) = '{WrGlobal.CorpID}'
                            , @WorkDate  CHAR(8)    = '{DateTime.Now.ConvertDate_To_Char8()}';

                        SELECT 
                              @WorkDate AS WorkDate
                            , SUM(ISNULL(WorkQty, 0)) AS Total_WorkQty
                            , SUM(ISNULL(NgQty, 0))   AS Total_NgQty

                            /*
                                PPM = (불량수량 / 생산수량) * 1,000,000
                            */
                            , CASE
                                WHEN SUM(ISNULL(WorkQty, 0)) = 0 THEN 0
                                ELSE CAST( ROUND((SUM(ISNULL(NgQty, 0)) * 1000000.0) / SUM(ISNULL(WorkQty, 0)), 0) AS INT) 
                              END AS Defect_PPM

                        FROM PM_OrderResult
                        WHERE CorpId = @CorpId
                        AND CONVERT(CHAR(8), WorkDate, 112) = @WorkDate
                        ;
                    ";

            return db.GetDataTable(query);
        }

        // 설비 가동률
        // 오른쪽 위에서 첫번째 원형 차트(주간/야간)
        private DataTable Month_MachineRate(string work_Band_Div = "")
        {
            string query = $@"
                            DECLARE
                              @CorpId       VARCHAR(10) = '{WrGlobal.CorpID}'
                            , @Select_Div   VARCHAR(10) = '{work_Band_Div}'
                            , @StartDate    CHAR(8)     = '{DateTime.Now.ConvertDate_To_Char8()}'
                            , @EndDate      CHAR(8)     = '{DateTime.Now.ConvertDate_To_Char8()}';

                             ;WITH BandTarget AS
                             (
                                 SELECT *
                                 FROM
                                 (
                                     VALUES
                                           ('10', 660) -- 주간
                                         , ('20', 780) -- 야간
                                 ) V(Work_Band_Div_Cd, Target_Minute)
                             ),

                             MasterBase AS
                             (
                                 SELECT
                                       CorpId
                                     , OrderNo
                                     , OrderSeq
                                     , MachineCode
                                     , CONVERT(CHAR(8), StartDate, 112) AS WorkDate
                                     , DisplayNumber
                                 FROM PM_OrderProcess
                                 WHERE CorpId = @CorpId
                                   AND CONVERT(CHAR(8), StartDate, 112) BETWEEN @StartDate AND @EndDate

                                 UNION

                                 SELECT
                                       R.CorpId
                                     , R.OrderNo
                                     , R.OrderSeq
                                     , R.MachineCode
                                     , R.WorkDate
                                     , NULL AS DisplayNumber
                                 FROM PM_OrderResult R
                                 LEFT JOIN PM_OrderProcess OP
                                     ON R.CorpId = OP.CorpId
                                    AND R.OrderNo = OP.OrderNo
                                    AND R.OrderSeq = OP.OrderSeq
                                 WHERE R.CorpId = @CorpId
                                   AND R.WorkDate BETWEEN @StartDate AND @EndDate
                                   AND OP.OrderNo IS NULL
                             ),

                             OrderBase AS
                             (
                                 SELECT
                                       MB.CorpId
                                     , MB.OrderNo
                                     , MB.OrderSeq
                                     , MB.MachineCode
                                     , MB.WorkDate
                                     , CASE
                                         WHEN ISNULL(MB.DisplayNumber, '') LIKE 'N%' THEN '20'
                                         ELSE '10'
                                       END AS Select_Div
                                     , BT.Target_Minute

                                 FROM MasterBase MB
                                 INNER JOIN BandTarget BT
                                     ON BT.Work_Band_Div_Cd = CASE
                                                                 WHEN ISNULL(MB.DisplayNumber, '') LIKE 'N%' THEN '20'
                                                                 ELSE '10'
                                                              END
                                 WHERE (
                                     @Select_Div = ''
                                     OR
                                     CASE
                                         WHEN ISNULL(MB.DisplayNumber, '') LIKE 'N%' THEN '20'
                                         ELSE '10'
                                     END = @Select_Div
                                 )
                             ),

                             NonWorkBase AS
                             (
                                 SELECT
                                       E.CorpId
                                     , E.OrderNo
                                     , E.OrderSeq
                                     , E.MachineCode
                                     , SUM(ISNULL(E.StopTime, 0)) AS NonWork_Minute
                                 FROM EQ_Operation E
                                 WHERE E.CorpId = @CorpId
                                     AND E.WorkDay BETWEEN @StartDate AND @EndDate
                                     AND E.OperationYn = 'N'
                                 GROUP BY E.CorpId, E.OrderNo, E.OrderSeq, E.MachineCode
                             ),

                             ResultBase AS
                             (
                                 SELECT
                                       O.Select_Div
                                     , O.WorkDate
                                     , O.MachineCode
                                     , O.OrderNo
                                     , O.OrderSeq

                                     , SUM(
                                         CASE
                                             WHEN R.StartDate IS NULL OR R.EndDate IS NULL THEN 0
                                             WHEN DATEDIFF(MINUTE, R.StartDate, R.EndDate) < 0 THEN 0
                                             ELSE DATEDIFF(MINUTE, R.StartDate, R.EndDate)
                                         END
                                       ) AS Work_Minute

                                     , SUM(ISNULL(N.NonWork_Minute, 0)) AS NonWork_Minute

                                     , SUM(
                                         CASE
                                             WHEN (
                                                 CASE
                                                     WHEN R.StartDate IS NULL OR R.EndDate IS NULL THEN 0
                                                     WHEN DATEDIFF(MINUTE, R.StartDate, R.EndDate) < 0 THEN 0
                                                     ELSE DATEDIFF(MINUTE, R.StartDate, R.EndDate)
                                                 END - ISNULL(N.NonWork_Minute, 0)
                                             ) < 0 THEN 0
                                             ELSE (
                                                 CASE
                                                     WHEN R.StartDate IS NULL OR R.EndDate IS NULL THEN 0
                                                     WHEN DATEDIFF(MINUTE, R.StartDate, R.EndDate) < 0 THEN 0
                                                     ELSE DATEDIFF(MINUTE, R.StartDate, R.EndDate)
                                                 END - ISNULL(N.NonWork_Minute, 0)
                                             )
                                         END
                                       ) AS Run_Minute

                                 FROM OrderBase O
                                 LEFT JOIN PM_OrderResult R
                                     ON R.CorpId = O.CorpId
                                     AND R.OrderNo = O.OrderNo
                                     AND R.OrderSeq = O.OrderSeq
                                     AND R.MachineCode = O.MachineCode
                                     AND R.WorkDate BETWEEN @StartDate AND @EndDate
                                 LEFT JOIN NonWorkBase N
                                     ON N.CorpId = R.CorpId
                                     AND N.OrderNo = R.OrderNo
                                     AND N.OrderSeq = R.OrderSeq
                                     AND N.MachineCode = R.MachineCode

                                 GROUP BY O.Select_Div, O.WorkDate, O.MachineCode, O.OrderNo, O.OrderSeq
                             ),

                             TargetBase AS
                             (
                                 SELECT
                                       O.Select_Div
                                     , O.WorkDate
                                     , O.MachineCode
                                     , MAX(O.Target_Minute) AS Target_Minute
                                 FROM OrderBase O
                                 GROUP BY O.Select_Div, O.WorkDate, O.MachineCode
                             ),

                             ResultAgg AS
                             (
                                 SELECT
                                       Select_Div
                                     , SUM(Work_Minute) AS Work_Minute
                                     , SUM(NonWork_Minute) AS NonWork_Minute
                                     , SUM(Run_Minute) AS Run_Minute
                                 FROM ResultBase
                                 GROUP BY Select_Div
                             ),

                             TargetAgg AS
                             (
                                 SELECT
                                       Select_Div
                                     , SUM(Target_Minute) AS Target_Minute
                                 FROM TargetBase
                                 GROUP BY Select_Div
                             )

                             SELECT
                                   T.Select_Div
                                 , ISNULL(R.Work_Minute, 0) AS 작업시간
                                 , ISNULL(R.NonWork_Minute, 0) AS 비가동시간
                                 , ISNULL(R.Run_Minute, 0) AS 가동시간
                                 , ISNULL(T.Target_Minute, 0) AS 가동목표시간
                                 , IIF(
                                     ISNULL(T.Target_Minute, 0) = 0,
                                     0,
                                     CAST(
                                         ROUND( ISNULL(R.Run_Minute, 0) / NULLIF(T.Target_Minute * 1.0, 0) * 100, 1) AS DECIMAL(10,1))
                                    ) AS ProgressRate

                             FROM TargetAgg T
                             LEFT JOIN ResultAgg R
                                 ON R.Select_Div = T.Select_Div
                             ORDER BY T.Select_Div;
                    ";

            return db.GetDataTable(query);
        }

        // 품질 합격률
        // 오른쪽 위에서 두번째 원형 차트(수입검사/초중종검사)
        private DataTable Month_QTRate(string exam_Div_Cd = "")
        {
            string query = $@"
                    DECLARE 
                            @CorpId   VARCHAR(10) = '{WrGlobal.CorpID}'
                        , @ExamDivCd  VARCHAR(3) = '{exam_Div_Cd}'   -- 특정 검사구분만 조회 시 입력, 전체는 ''
                        , @StartDate   CHAR(8) = '{DateTime.Now.ConvertDate_To_Char8()}'
                        , @EndDate   CHAR(8) = '{DateTime.Now.ConvertDate_To_Char8()}';

                  SELECT
                              COUNT(1) AS 검사건수

                            , SUM(CASE 
                                    WHEN A.PassDivCd = '10' THEN 1 
                                    ELSE 0 
                                  END) AS 합격건수

                            , SUM(CASE 
                                    WHEN A.PassDivCd = '20' THEN 1 
                                    ELSE 0 
                                  END) AS 불합격건수

                            , CASE 
                                WHEN COUNT(1) = 0 THEN 0
                                ELSE CAST(
                                        ROUND(
                                            SUM(CASE 
                                                    WHEN A.PassDivCd = '10' THEN 1 
                                                    ELSE 0 
                                                END) 
                                            / NULLIF(COUNT(1) * 1.0, 0) * 100
                                        , 1)
                                    AS DECIMAL(10,1))
                              END AS ProgressRate

                        FROM QM_ExamResult A

                        WHERE A.CorpId = @CorpId
                            AND A.ExamDate BETWEEN @StartDate AND @EndDate

                            AND A.PassDivCd IN ('10', '20')

                            AND (@ExamDivCd = '' OR A.ExamDivCd = @ExamDivCd)
            ";

            return db.GetDataTable(query);
        }

        private DataTable ProcessList()
        {
            string query = $@"
                             SELECT
                              ProcessCode
                            , ProcessName
                            , ROW_NUMBER() OVER(ORDER BY ProcessCode) AS Sort
                        FROM CM_Process
                        WHERE UseYn = 'Y'
            ";

            return db.GetDataTable(query);
        }

        #endregion

        #region 디자인

        // ══════════════════════════════════════════════════
        //  메인 Paint
        // ══════════════════════════════════════════════════
        void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int W = ClientSize.Width;
            int H = ClientSize.Height;
            int P = 12;

            g.Clear(C_BG);

            int headerH = 56;
            DrawHeader(g, P, P, W - P * 2, headerH);

            int kpiTop = P + headerH + P;
            int kpiH = 62;
            DrawKpiRow(g, P, kpiTop, W - P * 2, kpiH);

            int mainTop = kpiTop + kpiH + P;
            int mainH = H - mainTop - P;
            int totalW = W - P * 2;
            int col1W = (int)(totalW * 0.30);
            int col2W = (int)(totalW * 0.36);
            int col3W = totalW - col1W - col2W - P * 2;
            int col1X = P;
            int col2X = col1X + col1W + P;
            int col3X = col2X + col2W + P;

            DrawColumn1(g, col1X, mainTop, col1W, mainH);
            DrawColumn2(g, col2X, mainTop, col2W, mainH);
            DrawColumn3(g, col3X, mainTop, col3W, mainH);
        }

        // ══════════════════════════════════════════════════
        //  헤더
        // ══════════════════════════════════════════════════
        void DrawHeader(Graphics g, int x, int y, int w, int h)
        {
            FillPanel(g, new Rectangle(x, y, w, h));

            using (SolidBrush b = new SolidBrush(C_BLUE))
                g.FillRectangle(b, x, y + 6, 4, h - 12);

            using (Font fTitle = new Font("맑은 고딕", 16, FontStyle.Bold))
            using (Font fSub = new Font("맑은 고딕", 11))
            using (SolidBrush bText = new SolidBrush(C_TEXT))
            using (SolidBrush bMuted = new SolidBrush(C_MUTED))
            {
                g.DrawString("MES 생산·품질·설비 종합 대시보드", fTitle, bText, x + 18, y + 8);
                g.DrawString("Manufacturing Execution System  |  실시간 모니터링", fSub, bMuted, x + 22, y + 36);

                string clock = DateTime.Now.ToString("HH:mm:ss");
                using (Font fClock = new Font("맑은 고딕", 13, FontStyle.Bold))
                {
                    SizeF csz = g.MeasureString(clock, fClock);
                    g.DrawString(clock, fClock, bText, x + w - csz.Width - 10, y + (h - csz.Height) / 2);
                    DrawBadge(g, x + w - (int)csz.Width - 100, y + (h - 22) / 2, "관리자", C_BLUE);
                    DrawBadge(g, x + w - (int)csz.Width - 190, y + (h - 22) / 2, "● LIVE", C_GREEN);
                }
            }
        }

        void DrawBadge(Graphics g, int x, int y, string text, Color c)
        {
            using (Font f = new Font("맑은 고딕", 11, FontStyle.Bold))
            {
                SizeF sz = g.MeasureString(text, f);
                RectangleF r = new RectangleF(x, y, sz.Width + 16, 22);
                using (SolidBrush bg = new SolidBrush(Color.FromArgb(40, c)))
                using (Pen pen = new Pen(Color.FromArgb(130, c), 1))
                using (SolidBrush br = new SolidBrush(c))
                {
                    FillRoundRect(g, bg, r, 6);
                    DrawRoundRect(g, pen, r, 6);
                    g.DrawString(text, f, br, x + 8, y + 3);
                }
            }
        }

        // ══════════════════════════════════════════════════
        //  KPI 4개
        // ══════════════════════════════════════════════════
        void DrawKpiRow(Graphics g, int x, int y, int w, int h)
        {
            int gap = 8;
            int cw = (w - gap * 3) / 4;

            string[] labels = { "금일 생산량", "당일 불량률(PPM)", "당일 품질 합격률", "당일 설비 가동률" };
            Color[] cols = { C_GREEN, C_BLUE, C_PURPLE, C_ORANGE };

            for (int i = 0; i < 4; i++)
            {
                int cx = x + i * (cw + gap);
                FillPanel(g, new Rectangle(cx, y, cw, h));

                using (Font fL = new Font("맑은 고딕", 11))
                using (Font fV = new Font("맑은 고딕", 17, FontStyle.Bold))
                using (SolidBrush bM = new SolidBrush(C_MUTED))
                using (SolidBrush bC = new SolidBrush(cols[i]))
                {
                    g.DrawString(labels[i], fL, bM, cx + 10, y + 7);
                    g.DrawString(vals[i], fV, bC, cx + 8, y + 20);
                    g.DrawString(subs[i], fL, bM, cx + 10, y + h - 17);
                }
            }
        }

        // ══════════════════════════════════════════════════
        //  컬럼 1: 생산현황 + 공정달성률 + 알림
        // ══════════════════════════════════════════════════
        void DrawColumn1(Graphics g, int x, int y, int w, int h)
        {
            int p1H = (int)(h * 0.28);
            int p2H = (int)(h * 0.28);
            int p3H = h - p1H - p2H - 16;

            FillPanel(g, new Rectangle(x, y, w, p1H));
            DrawSecTitle(g, x + 10, y + 8, "당일 생산 달성률", C_BLUE);
            DrawGaugePair(g, x, y + 30, w, p1H - 30, 0, 1);

            int y2 = y + p1H + 8;
            FillPanel(g, new Rectangle(x, y2, w, p2H));
            DrawSecTitle(g, x + 10, y2 + 8, "당일 공정별 달성률", C_ORANGE);
            DrawGaugePair(g, x, y2 + 30, w, p2H - 30, 2, 3);

            int y3 = y2 + p2H + 8;
            FillPanel(g, new Rectangle(x, y3, w, p3H));
            DrawSecTitle(g, x + 10, y3 + 8, "최근 공지사항", C_RED);
            DrawAlarms(g, x, y3 + 30, w, p3H - 30);
        }

        void DrawGaugePair(Graphics g, int x, int y, int w, int h, int idx1, int idx2)
        {
            int half = w / 2;
            DrawGauge(g, x, y, half, h, gaugeVals[idx1], gaugeColors[idx1], gaugeLabels[idx1]);
            DrawGauge(g, x + half, y, half, h, gaugeVals[idx2], gaugeColors[idx2], gaugeLabels[idx2]);
        }

        void DrawGauge(Graphics g, int x, int y, int w, int h, double val, Color col, string label)
        {
            int dia = Math.Min(w - 20, h - 24);
            if (dia < 30) dia = 30;
            int cx = x + w / 2;
            int cy = y + (h - 18) / 2;
            float r = dia / 2f;
            float lw = dia * 0.09f;

            RectangleF arc = new RectangleF(cx - r, cy - r, dia, dia);

            using (Pen bgPen = new Pen(Color.FromArgb(0x1a, 0x28, 0x40), lw))
            {
                bgPen.StartCap = LineCap.Round;
                bgPen.EndCap = LineCap.Round;
                g.DrawArc(bgPen, arc, 135f, 270f);
            }

            float sweep = (float)(270.0 * val / 100.0 * animT);
            if (sweep > 0.5f)
            {
                using (Pen vPen = new Pen(col, lw))
                {
                    vPen.StartCap = LineCap.Round;
                    vPen.EndCap = LineCap.Round;
                    g.DrawArc(vPen, arc, 135f, sweep);
                }
            }

            int dispVal = (int)(val * animT);
            using (Font fv = new Font("맑은 고딕", dia > 70 ? 14 : 11, FontStyle.Bold))
            using (SolidBrush bv = new SolidBrush(C_TEXT))
            {
                string txt = dispVal + "%";
                SizeF ts = g.MeasureString(txt, fv);
                g.DrawString(txt, fv, bv, cx - ts.Width / 2, cy - ts.Height / 2);
            }

            using (Font fl = new Font("맑은 고딕", 11))
            using (SolidBrush bl = new SolidBrush(C_MUTED))
            {
                SizeF ls = g.MeasureString(label, fl);
                g.DrawString(label, fl, bl, cx - ls.Width / 2, y + h - 17);
            }
        }

        void DrawAlarms(Graphics g, int x, int y, int w, int h)
        {
            int count = alarmMsgs.Length;
            if (count == 0) return;

            int rowH = Math.Max(22, h / count);

            int iconW = 16;     // 아이콘 영역
            int timeW = 100;    // 시간 영역 (조절 가능)
            int padding = 6;

            using (Font ft = new Font("맑은 고딕", 10))
            using (SolidBrush bMuted = new SolidBrush(C_MUTED))
            using (SolidBrush bText = new SolidBrush(C_TEXT))
            using (Pen pen = new Pen(C_DIVIDER, 1))
            {
                // 문자열 포맷 (핵심)
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center; // ⭐ 수직 중앙
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.NoWrap;

                for (int i = 0; i < count; i++)
                {
                    int ry = y + i * rowH;

                    // 구분선
                    if (i > 0)
                        g.DrawLine(pen, x + 4, ry, x + w - 4, ry);

                    // ● 아이콘 (정중앙)
                    using (SolidBrush db = new SolidBrush(alarmDots[i]))
                        g.FillEllipse(db, x + 4, ry + (rowH - 8) / 2, 8, 8);

                    // 컬럼 위치 계산
                    int timeX = x + iconW + padding;
                    int msgX = timeX + timeW;
                    int msgW = w - msgX - 6;

                    // 영역 정의
                    Rectangle timeRect = new Rectangle(timeX, ry, timeW, rowH);
                    Rectangle msgRect = new Rectangle(msgX, ry, msgW, rowH);

                    // 시간
                    g.DrawString(alarmTimes[i], ft, bMuted, timeRect, sf);

                    // 메시지
                    g.DrawString(alarmMsgs[i], ft, bText, msgRect, sf);
                }
            }
        }

        // ══════════════════════════════════════════════════
        //  컬럼 2: 추이차트 + 바차트 + 경비
        // ══════════════════════════════════════════════════
        void DrawColumn2(Graphics g, int x, int y, int w, int h)
        {
            int p1H = (int)(h * 0.32);
            int p2H = (int)(h * 0.26);
            int p3H = h - p1H - p2H - 16;

            FillPanel(g, new Rectangle(x, y, w, p1H));
            DrawSecTitle(g, x + 10, y + 8, "생산 추이", C_BLUE);
            DrawTrendLegend(g, x + 10, y + 28);
            DrawLineChart(g, x + 8, y + 44, w - 16, p1H - 52);

            int y2 = y + p1H + 8;
            FillPanel(g, new Rectangle(x, y2, w, p2H));
            DrawSecTitle(g, x + 10, y2 + 8, "공정별 생산량", C_BLUE);
            //DrawSecTitle(g, x + 10, y2 + 8, "설비별 생산량", C_BLUE);
            DrawBarChart(g, x + 8, y2 + 30, w - 16, p2H - 38);

            int y3 = y2 + p2H + 8;
            FillPanel(g, new Rectangle(x, y3, w, p3H));
            DrawSecTitle(g, x + 10, y3 + 8, "설비 성능 가동률", C_ORANGE);
            DrawCostBars(g, x + 8, y3 + 30, w - 16, p3H - 38);
        }

        void DrawTrendLegend(Graphics g, int x, int y)
        {
            using (Font f = new Font("맑은 고딕", 9))
            using (SolidBrush bm = new SolidBrush(C_MUTED))
            using (SolidBrush bg = new SolidBrush(C_GREEN))
            using (SolidBrush bo = new SolidBrush(C_ORANGE))
            {
                g.FillRectangle(bg, x, y + 4, 14, 3);
                g.DrawString("실적", f, bm, x + 16, y);
                g.FillRectangle(bo, x + 48, y + 4, 14, 3);
                g.DrawString("계획", f, bm, x + 64, y);
            }
        }

        void DrawLineChart(Graphics g, int x, int y, int w, int h)
        {
            int padL = 32, padB = 18, padT = 4, padR = 8;
            int cx = x + padL;
            int cy = y + padT;
            int cw = w - padL - padR;
            int chh = h - padB - padT;
            double minV = double.MaxValue;
            double maxV = double.MinValue;
            int n = trendActual.Length;

            // 전체 데이터 기준 min/max 계산
            for (int i = 0; i < n; i++)
            {
                if (trendActual[i] < minV) minV = trendActual[i];
                if (trendPlan[i] < minV) minV = trendPlan[i];

                if (trendActual[i] > maxV) maxV = trendActual[i];
                if (trendPlan[i] > maxV) maxV = trendPlan[i];
            }

            // 데이터 없을 때 방어
            if (minV == double.MaxValue) minV = 0;
            if (maxV == double.MinValue) maxV = 1;

            // 여유 공간 추가 (그래프 위/아래 padding)
            double range = maxV - minV;
            if (range == 0) range = 1;

            minV = 0;

            maxV += range * 0.1;

            if (maxV <= 0) maxV = 1;

            //minV -= range * 0.1;
            //maxV += range * 0.1;

            using (Font fl = new Font("맑은 고딕", 11))
            using (SolidBrush bl = new SolidBrush(C_MUTED))
            using (Pen gridPen = new Pen(Color.FromArgb(0x1e, 0x30, 0x55), 1))
            {
                for (int i = 0; i <= 4; i++)
                {
                    float ly = cy + chh - (float)(i / 4.0 * chh);
                    g.DrawLine(gridPen, cx, ly, cx + cw, ly);
                    int yValue = (int)(minV + i / 4.0 * (maxV - minV));

                    g.DrawString(
                        yValue.ToString("#,##0"),
                        fl,
                        bl,
                        x,
                        ly - 6
                    );
                    //g.DrawString(((int)(minV + i / 4.0 * (maxV - minV))).ToString(), fl, bl, x, ly - 6);
                }
            }

            PointF[] actualPts = new PointF[n];
            PointF[] planPts = new PointF[n];
            for (int i = 0; i < n; i++)
            {
                float px = cx + (float)i / (n - 1) * cw;
                double av = trendActual[i] * animT + minV * (1 - animT);
                double pv = trendPlan[i] * animT + minV * (1 - animT);
                float apy = cy + chh - (float)((av - minV) / (maxV - minV) * chh);
                float ppy = cy + chh - (float)((pv - minV) / (maxV - minV) * chh);
                actualPts[i] = new PointF(px, Math.Max(cy, Math.Min(cy + chh, apy)));
                planPts[i] = new PointF(px, Math.Max(cy, Math.Min(cy + chh, ppy)));
            }

            // 실적 채우기
            PointF[] fillPts = new PointF[n + 2];
            fillPts[0] = new PointF(actualPts[0].X, cy + chh);
            for (int i = 0; i < n; i++) fillPts[i + 1] = actualPts[i];
            fillPts[n + 1] = new PointF(actualPts[n - 1].X, cy + chh);
            using (SolidBrush fillBr = new SolidBrush(Color.FromArgb(18, C_GREEN)))
                g.FillPolygon(fillBr, fillPts);

            // 계획 점선
            using (Pen planPen = new Pen(C_ORANGE, 1.5f))
            {
                planPen.DashStyle = DashStyle.Dash;
                if (n > 1) g.DrawLines(planPen, planPts);
            }
            foreach (PointF pt in planPts)
            {
                using (SolidBrush pb = new SolidBrush(C_ORANGE))
                    g.FillEllipse(pb, pt.X - 3, pt.Y - 3, 6, 6);
            }

            // 실적 선
            using (Pen actPen = new Pen(C_GREEN, 2.5f))
            {
                if (n > 1) g.DrawLines(actPen, actualPts);
            }
            foreach (PointF pt in actualPts)
            {
                using (SolidBrush pb = new SolidBrush(C_GREEN))
                    g.FillEllipse(pb, pt.X - 4, pt.Y - 4, 8, 8);
                using (SolidBrush pb2 = new SolidBrush(C_PANEL))
                    g.FillEllipse(pb2, pt.X - 2, pt.Y - 2, 4, 4);
            }

            // X축 레이블
            using (Font fx = new Font("맑은 고딕", 11))
            using (SolidBrush bx = new SolidBrush(C_MUTED))
            {
                for (int i = 0; i < n; i++)
                {
                    float px = cx + (float)i / (n - 1) * cw;
                    g.DrawString("W" + (i + 1), fx, bx, px - 8, y + h - 14);
                }
            }
        }

        void DrawBarChart(Graphics g, int x, int y, int w, int h)
        {
            int padL = 32, padB = 18, padR = 8, padT = 4;
            int cx = x + padL;
            int cy = y + padT;
            int cw = w - padL - padR;
            int chh = h - padB - padT;

            int n = barLabels.Length == 0 ? 1 : barLabels.Length;
            int groupW = cw / n;
            int barW = Math.Max(6, groupW / 3);

            double maxV = 0;

            // 실제 + 계획 중 최대값 찾기
            if (barLabels.Length > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    if (barActual[i] > maxV) maxV = barActual[i];
                    if (barPlan[i] > maxV) maxV = barPlan[i];
                }

                // 최소값 보정 (0 division 방지)
                if (maxV == 0) maxV = 1;

                // 보기 좋게 상단 여유 추가 (10%)
                maxV *= 1.1;

                using (Pen gridPen = new Pen(Color.FromArgb(0x1e, 0x30, 0x55), 1))
                using (Font fl = new Font("맑은 고딕", 11))
                using (SolidBrush bl = new SolidBrush(C_MUTED))
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        float ly = cy + chh - (float)(i / 3.0 * chh);
                        g.DrawLine(gridPen, cx, ly, cx + cw, ly);
                        g.DrawString(((int)(i / 3.0 * maxV)).ToString(), fl, bl, x, ly - 6);
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    int bx2 = cx + i * groupW + groupW / 2 - barW;
                    int bh = (int)(chh * barActual[i] / maxV * animT);
                    int ph = (int)(chh * barPlan[i] / maxV * animT);

                    using (SolidBrush bb = new SolidBrush(Color.FromArgb(180, C_BLUE)))
                        g.FillRectangle(bb, bx2, cy + chh - bh, barW, bh);
                    using (SolidBrush pb = new SolidBrush(Color.FromArgb(50, C_BLUE)))
                        g.FillRectangle(pb, bx2 + barW + 2, cy + chh - ph, barW, ph);

                    using (Font fl2 = new Font("맑은 고딕", 11))
                    using (Font vFont = new Font("맑은 고딕", 9))
                    using (SolidBrush bl2 = new SolidBrush(C_MUTED))
                    {
                        SizeF ls = g.MeasureString(barLabels[i], fl2);
                        g.DrawString(barLabels[i], fl2, bl2,
                            cx + i * groupW + groupW / 2 - ls.Width / 2, y + h - 14);

                        // 실적 값(Work_Qty) 표시
                        string txtWorkQty = Math.Round(barActual[i] * animT).ToString("N0");
                        SizeF asz = g.MeasureString(txtWorkQty, vFont);

                        float ax = bx2 + (barW / 2.0f) - (asz.Width / 2.0f);
                        float ay = (cy + chh - bh) - asz.Height - 2;
                        g.DrawString(txtWorkQty, vFont, bl2, ax, ay);

                        // 계획 값(Plan_Qty) 표시
                        string txtPlanQty = Math.Round(barPlan[i] * animT).ToString("N0");
                        SizeF psz = g.MeasureString(txtPlanQty, vFont);

                        float px = (bx2 + barW + 2) + (barW / 2.0f) - (psz.Width / 2.0f);
                        float py = (cy + chh - ph) - psz.Height - 2;
                        g.DrawString(txtPlanQty, vFont, bl2, px, py);
                    }
                }
            }

            using (Font fl3 = new Font("맑은 고딕", 9))
            using (SolidBrush bm3 = new SolidBrush(C_MUTED))
            using (SolidBrush bb3 = new SolidBrush(Color.FromArgb(180, C_BLUE)))
            using (SolidBrush pb3 = new SolidBrush(Color.FromArgb(50, C_BLUE)))
            {
                g.FillRectangle(bb3, x + w - 80, y + 2, 10, 8);
                g.DrawString("실적", fl3, bm3, x + w - 70, y);
                g.FillRectangle(pb3, x + w - 32, y + 2, 10, 8);
                g.DrawString("계획", fl3, bm3, x + w - 22, y);
            }
        }

        void DrawCostBars(Graphics g, int x, int y, int w, int h)
        {
            int n = costNames.Length;
            int rowH = h / n;

            for (int i = 0; i < n; i++)
            {
                int ry = y + i * rowH;

                // 영역 분리
                int labelH = 16;   // 라벨 영역 높이
                int barH = 10;     // 바 높이

                // ───── 라벨 (위)
                using (Font fn = new Font("맑은 고딕", 11, FontStyle.Bold))
                using (SolidBrush bn = new SolidBrush(C_TEXT))
                {
                    g.DrawString(costNames[i], fn, bn, x, ry);
                }

                // ───── 바 영역 (아래)
                int barX = x;
                int barY = ry + labelH + 4;
                int barW = w - 80; // 오른쪽 값 영역 확보

                // 배경 바
                using (SolidBrush bgBr = new SolidBrush(Color.FromArgb(0x0d, 0x1a, 0x2e)))
                    FillRoundRect(g, bgBr, new RectangleF(barX, barY, barW, barH), 5);

                // 값 채우기
                int fillW = (int)(barW * (costVals[i] / costTargets[i]) * animT);
                fillW = Math.Min(fillW, barW);

                if (fillW > 2)
                {
                    using (SolidBrush fillBr = new SolidBrush(costColors[i % costColors.Length]))
                        FillRoundRect(g, fillBr, new RectangleF(barX, barY, fillW, barH), 5);
                }

                // ───── 값 텍스트 (오른쪽)
                string valTxt = costVals[i].ToString("F1") + "%";

                using (Font fv = new Font("맑은 고딕", 11))
                using (SolidBrush bv = new SolidBrush(C_MUTED))
                {
                    SizeF vs = g.MeasureString(valTxt, fv);
                    g.DrawString(valTxt, fv, bv, x + w - vs.Width, ry);
                }
            }
        }

        // ══════════════════════════════════════════════════
        //  컬럼 3: 설비가동률 + 품질합격률 + 불량분석
        // ══════════════════════════════════════════════════
        void DrawColumn3(Graphics g, int x, int y, int w, int h)
        {
            #region 3단계 (ver.1) 중중대

            /*
            int p1H = (int)(h * 0.28);
            int p2H = (int)(h * 0.28);
            int p3H = h - p1H - p2H - 16;

            FillPanel(g, new Rectangle(x, y, w, p1H));
            DrawSecTitle(g, x + 10, y + 8, "설비 가동률", C_GREEN);
            DrawGaugePair(g, x, y + 30, w, p1H - 30, 4, 5);

            int y2 = y + p1H + 8;
            FillPanel(g, new Rectangle(x, y2, w, p2H));
            DrawSecTitle(g, x + 10, y2 + 8, "품질 합격률", C_GREEN);
            DrawGaugePair(g, x, y2 + 30, w, p2H - 30, 6, 7);

            int y3 = y2 + p2H + 8;
            FillPanel(g, new Rectangle(x, y3, w, p3H));
            DrawSecTitle(g, x + 10, y3 + 8, "당월 최다 불량 유형 TOP5", C_PURPLE);
            DrawDonut(g, x + 8, y3 + 30, w - 16, p3H - 38);
            */
            #endregion

            #region 3단계 (ver.2) 균일
            /*
            int gap = 8;


               // 설비 가동률은 기존 크기 유지.
               // 남은 영역은 품질 불량유형 / 당월 최다 불량유형을 동일 높이로 분할.

            int p1H = (int)(h * 0.28);
            int remainH = h - p1H - gap * 2;

            int p2H = remainH / 2;
            int p3H = remainH - p2H;

            // 1. 설비 가동률
            FillPanel(g, new Rectangle(x, y, w, p1H));
            DrawSecTitle(g, x + 10, y + 8, "설비 가동률", C_GREEN);
            DrawGaugePair(g, x, y + 30, w, p1H - 30, 4, 5);

            // 2. 품질 영역 불량유형 도넛
            int y2 = y + p1H + gap;
            FillPanel(g, new Rectangle(x, y2, w, p2H));
            DrawSecTitle(g, x + 10, y2 + 8, "사출 불량 유형", C_GREEN);

            //DrawDonut(
            //    g,
            //    x + 8,
            //    y2 + 30,
            //    w - 16,
            //    p2H - 38,
            //    qualityDefectVals,
            //    qualityDefectLabels
            //);
            DrawDefectLineChart(
                    g,
                    x + 8,
                    y2 + 34,
                    w - 16,
                    p2H - 42,
                    qualityDefectVals,
                    qualityDefectLabels,
                    C_GREEN
                );

            // 3. 당월 최다 불량 유형 TOP5
            int y3 = y2 + p2H + gap;
            FillPanel(g, new Rectangle(x, y3, w, p3H));
            DrawSecTitle(g, x + 10, y3 + 8, "조립 불량 유형", C_PURPLE);

            //DrawDonut(
            //    g,
            //    x + 8,
            //    y3 + 30,
            //    w - 16,
            //    p3H - 38,
            //    AssemblyDefectVals,
            //    AssemblyDefectLabels
            //);
            DrawDefectLineChart(
                g,
                x + 8,
                y3 + 34,
                w - 16,
                p3H - 42,
                AssemblyDefectVals,
                AssemblyDefectLabels,
                C_PURPLE
            );
            */
            #endregion

            int gap = 8;

            /*
                우측 컬럼 구조 변경

                1. 설비 가동률 : 기존 크기 유지
                2. 사출/조립 불량유형 : 남은 영역 전체 사용
            */
            int p1H = (int)(h * 0.28);
            int p2H = h - p1H - gap;

            // 1. 설비 가동률
            FillPanel(g, new Rectangle(x, y, w, p1H));
            DrawSecTitle(g, x + 10, y + 8, "설비 가동률", C_GREEN);
            DrawGaugePair(g, x, y + 30, w, p1H - 30, 4, 5);

            // 2. 사출/조립 불량유형 통합 그래프
            int y2 = y + p1H + gap;
            FillPanel(g, new Rectangle(x, y2, w, p2H));

            DrawSecTitle(g, x + 10, y2 + 8, "공정별 불량유형", C_PURPLE);

            DrawDefectCompareLegend(g, x + 20, y2 + 30);

            DrawCombinedDefectLineChart(
                g,
                x + 8,
                y2 + 55,
                w - 16,
                p2H - 63,
                qualityDefectVals,
                qualityDefectLabels,
                AssemblyDefectVals,
                AssemblyDefectLabels
            );
        }

        void DrawDefectCompareLegend(Graphics g, int x, int y)
        {
            DataTable dt= ProcessList();
            DataRow[] drProcess = dt.Select("ProcessCode <> 'ALL'");

            string processCode1 = drProcess[0]["ProcessName"].ConvertString_Null_To_Empty();
            string processCode2 = drProcess[1]["ProcessName"].ConvertString_Null_To_Empty();

            using (Font f = new Font("맑은 고딕", 10, FontStyle.Bold))
            using (SolidBrush txtBrush = new SolidBrush(C_MUTED))
            using (SolidBrush injBrush = new SolidBrush(C_GREEN))
            using (SolidBrush asmBrush = new SolidBrush(C_ORANGE))
            {
                // 사출 : 원형 마커
                g.FillEllipse(injBrush, x, y + 5, 9, 9);
                g.DrawString(processCode1, f, txtBrush, x + 14, y);

                // 조립 : 사각 마커
                int x2 = x + 120;
                g.FillRectangle(asmBrush, x2, y + 5, 9, 9);
                g.DrawString(processCode2, f, txtBrush, x2 + 14, y);
            }
        }

        void DrawCombinedDefectLineChart(
                Graphics g,
                int x,
                int y,
                int w,
                int h,
                int[] injectVals,
                string[] injectLabels,
                int[] assemblyVals,
                string[] assemblyLabels
            )
        {
            if (injectVals == null) injectVals = new int[0];
            if (injectLabels == null) injectLabels = new string[0];
            if (assemblyVals == null) assemblyVals = new int[0];
            if (assemblyLabels == null) assemblyLabels = new string[0];

            // 불량유형명 기준으로 사출/조립 데이터를 합치기
            List<string> mergedLabels = new List<string>();
            Dictionary<string, int> injectMap = new Dictionary<string, int>();
            Dictionary<string, int> assemblyMap = new Dictionary<string, int>();

            for (int i = 0; i < injectLabels.Length && i < injectVals.Length; i++)
            {
                string key = injectLabels[i];
                if (string.IsNullOrWhiteSpace(key)) key = "미지정";

                if (!injectMap.ContainsKey(key))
                    injectMap.Add(key, injectVals[i]);
                else
                    injectMap[key] += injectVals[i];

                if (!mergedLabels.Contains(key))
                    mergedLabels.Add(key);
            }

            for (int i = 0; i < assemblyLabels.Length && i < assemblyVals.Length; i++)
            {
                string key = assemblyLabels[i];
                if (string.IsNullOrWhiteSpace(key)) key = "미지정";

                if (!assemblyMap.ContainsKey(key))
                    assemblyMap.Add(key, assemblyVals[i]);
                else
                    assemblyMap[key] += assemblyVals[i];

                if (!mergedLabels.Contains(key))
                    mergedLabels.Add(key);
            }

            if (mergedLabels.Count == 0)
            {
                DrawNoDataText(g, x, y, w, h);
                return;
            }

            int n = mergedLabels.Count;

            double[] inj = new double[n];
            double[] asm = new double[n];

            for (int i = 0; i < n; i++)
            {
                string key = mergedLabels[i];

                inj[i] = injectMap.ContainsKey(key) ? injectMap[key] : 0;
                asm[i] = assemblyMap.ContainsKey(key) ? assemblyMap[key] : 0;
            }

            int padL = 42;
            int padB = 52;
            int padT = 8;
            int padR = 12;

            int cx = x + padL;
            int cy = y + padT;
            int cw = w - padL - padR;
            int chh = h - padB - padT;

            if (cw <= 0 || chh <= 0) return;

            double maxV = 0;

            for (int i = 0; i < n; i++)
            {
                if (inj[i] > maxV) maxV = inj[i];
                if (asm[i] > maxV) maxV = asm[i];
            }

            if (maxV <= 0)
            {
                DrawNoDataText(g, x, y, w, h);
                return;
            }

            // Y축은 0부터 시작
            double minV = 0;
            maxV = maxV + (maxV * 0.15);

            if (maxV <= 0) maxV = 1;

            // Y축 그리드
            using (Font fl = new Font("맑은 고딕", 8))
            using (SolidBrush bl = new SolidBrush(C_MUTED))
            using (Pen gridPen = new Pen(Color.FromArgb(0x1e, 0x30, 0x55), 1))
            {
                for (int i = 0; i <= 4; i++)
                {
                    float ly = cy + chh - (float)(i / 4.0 * chh);

                    g.DrawLine(gridPen, cx, ly, cx + cw, ly);

                    string yText = ((int)(minV + i / 4.0 * (maxV - minV))).ToString("#,##0");
                    g.DrawString(yText, fl, bl, x, ly - 6);
                }
            }

            PointF[] injPts = new PointF[n];

            PointF[] asmPts = new PointF[n];

            for (int i = 0; i < n; i++)
            {
                float px = n == 1
                    ? cx + cw / 2f
                    : cx + (float)i / (n - 1) * cw;

                double injVal = inj[i] * animT;
                double asmVal = asm[i] * animT;

                float injY = cy + chh - (float)((injVal - minV) / (maxV - minV) * chh);
                float asmY = cy + chh - (float)((asmVal - minV) / (maxV - minV) * chh);

                injY = Math.Max(cy, Math.Min(cy + chh, injY));
                asmY = Math.Max(cy, Math.Min(cy + chh, asmY));

                injPts[i] = new PointF(px, injY);
                asmPts[i] = new PointF(px, asmY);
            }

            // 사출 선
            using (Pen injPen = new Pen(C_GREEN, 2.5f))
            {
                injPen.StartCap = LineCap.Round;
                injPen.EndCap = LineCap.Round;

                if (n > 1)
                    g.DrawLines(injPen, injPts);
            }

            // 조립 선
            using (Pen asmPen = new Pen(C_ORANGE, 2.5f))
            {
                asmPen.StartCap = LineCap.Round;
                asmPen.EndCap = LineCap.Round;
                asmPen.DashStyle = DashStyle.Dash;

                if (n > 1)
                    g.DrawLines(asmPen, asmPts);
            }

            // 사출 점: 원형
            using (Font fv = new Font("맑은 고딕", 8, FontStyle.Bold))
            using (SolidBrush textBr = new SolidBrush(C_TEXT))
            {
                for (int i = 0; i < n; i++)
                {
                    PointF pt = injPts[i];

                    using (SolidBrush b = new SolidBrush(C_GREEN))
                        g.FillEllipse(b, pt.X - 4, pt.Y - 4, 8, 8);

                    using (SolidBrush b = new SolidBrush(C_PANEL))
                        g.FillEllipse(b, pt.X - 2, pt.Y - 2, 4, 4);

                    if (inj[i] > 0)
                    {
                        string valText = inj[i].ToString("#,##0");
                        SizeF sz = g.MeasureString(valText, fv);
                        g.DrawString(valText, fv, textBr, pt.X - sz.Width / 2, pt.Y - 20);
                    }
                }
            }

            // 조립 점: 사각형
            using (Font fv = new Font("맑은 고딕", 8, FontStyle.Bold))
            using (SolidBrush textBr = new SolidBrush(C_TEXT))
            {
                for (int i = 0; i < n; i++)
                {
                    PointF pt = asmPts[i];

                    using (SolidBrush b = new SolidBrush(C_ORANGE))
                        g.FillRectangle(b, pt.X - 4, pt.Y - 4, 8, 8);

                    if (asm[i] > 0)
                    {
                        string valText = asm[i].ToString("#,##0");
                        SizeF sz = g.MeasureString(valText, fv);
                        g.DrawString(valText, fv, textBr, pt.X - sz.Width / 2, pt.Y + 6);
                    }
                }
            }

            // X축 라벨
            using (Font fx = new Font("맑은 고딕", 8))
            using (SolidBrush bx = new SolidBrush(C_MUTED))
            {
                for (int i = 0; i < n; i++)
                {
                    float px = n == 1
                        ? cx + cw / 2f
                        : cx + (float)i / (n - 1) * cw;

                    string labelText = ShortText(mergedLabels[i], 5);

                    SizeF sz = g.MeasureString(labelText, fx);
                    g.DrawString(labelText, fx, bx, px - sz.Width / 2, y + h - 20);
                }
            }
        }

        //void DrawNoDataText(Graphics g, int x, int y, int w, int h)
        //{
        //    using (Font f = new Font("맑은 고딕", 12, FontStyle.Bold))
        //    using (SolidBrush br = new SolidBrush(C_MUTED))
        //    {
        //        string txt = "데이터 없음";
        //        SizeF sz = g.MeasureString(txt, f);
        //        g.DrawString(txt, f, br, x + (w - sz.Width) / 2, y + (h - sz.Height) / 2);
        //    }
        //}


        string ShortText(string text, int maxLen)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            text = text.Trim();

            if (text.Length <= maxLen)
                return text;

            return text.Substring(0, maxLen) + "…";
        }

        /*
        void DrawDonut(Graphics g, int x, int y, int w, int h)
        {
            int total = 0;
            foreach (int v in defectVals) total += v;

            int dia = Math.Min(w * 6 / 10, h - 10);
            int cx = x + dia / 2 + 10;
            int cy = y + h / 2;
            float outer = dia / 2f;
            float inner = outer * 0.55f;

            float startAngle = -90f;
            for (int i = 0; i < defectVals.Length; i++)
            {
                float sweep = 360f * defectVals[i] / total * (float)animT;
                using (SolidBrush br = new SolidBrush(defectColors[i]))
                    g.FillPie(br, cx - outer, cy - outer, dia, dia, startAngle, sweep);
                startAngle += sweep;
            }

            using (SolidBrush holeBr = new SolidBrush(C_PANEL))
                g.FillEllipse(holeBr, cx - inner, cy - inner, inner * 2, inner * 2);

            int lx = cx + (int)outer + 12;
            using (Font fl = new Font("맑은 고딕", 8))
            using (SolidBrush bm = new SolidBrush(C_MUTED))
            {
                for (int i = 0; i < defectLabels.Length; i++)
                {
                    int ly = y + i * 22 + (h - defectLabels.Length * 22) / 2;
                    using (SolidBrush db = new SolidBrush(defectColors[i]))
                        g.FillRectangle(db, lx, ly + 3, 10, 10);
                    g.DrawString(defectLabels[i] + " " + defectVals[i] + "EA", fl, bm, lx + 14, ly);
                }
            }
        }
        */

        void DrawDonut(Graphics g, int x, int y, int w, int h, int[] vals, string[] labels)
        {
            if (vals == null || labels == null || vals.Length == 0 || labels.Length == 0)
                return;

            int total = 0;
            foreach (int v in vals)
                total += Math.Max(0, v);

            int dia = Math.Min(w * 6 / 10, h - 10);
            if (dia < 40) dia = Math.Min(w, h) - 20;
            if (dia < 30) dia = 30;

            int cx = x + dia / 2 + 10;
            int cy = y + h / 2;
            float outer = dia / 2f;
            float inner = outer * 0.55f;

            //RectangleF pieRect = new RectangleF(cx - outer, cy - outer, dia, dia);
            Rectangle pieRect = new Rectangle((int)(cx - outer), (int)(cy - outer), dia, dia);

            if (total <= 0)
            {
                using (SolidBrush emptyBr = new SolidBrush(Color.FromArgb(0x1a, 0x28, 0x40)))
                    g.FillEllipse(emptyBr, pieRect);

                using (SolidBrush holeBr = new SolidBrush(C_PANEL))
                    g.FillEllipse(holeBr, cx - inner, cy - inner, inner * 2, inner * 2);

                using (Font f = new Font("맑은 고딕", 12, FontStyle.Bold))
                using (SolidBrush br = new SolidBrush(C_TEXT))
                {
                    string txt = "데이터 없음";
                    SizeF sz = g.MeasureString(txt, f);
                    g.DrawString(txt, f, br, cx - sz.Width / 2, cy - sz.Height / 2);
                }

                return;
            }

            float startAngle = -90f;

            for (int i = 0; i < vals.Length; i++)
            {
                int val = Math.Max(0, vals[i]);
                if (val == 0) continue;

                float sweep = 360f * val / total * (float)animT;

                using (SolidBrush br = new SolidBrush(defectColors[i % defectColors.Length]))
                    g.FillPie(br, pieRect, startAngle, sweep);

                startAngle += sweep;
            }

            using (SolidBrush holeBr = new SolidBrush(C_PANEL))
                g.FillEllipse(holeBr, cx - inner, cy - inner, inner * 2, inner * 2);

            int lx = cx + (int)outer + 12;

            using (Font fl = new Font("맑은 고딕", 11))
            using (SolidBrush bm = new SolidBrush(C_MUTED))
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    int ly = y + i * 22 + (h - labels.Length * 22) / 2;

                    using (SolidBrush db = new SolidBrush(defectColors[i % defectColors.Length]))
                        g.FillRectangle(db, lx, ly + 3, 10, 10);

                    string text = labels[i] + " " + vals[i].ToString("#,##0") + "EA";
                    g.DrawString(text, fl, bm, lx + 14, ly);
                }
            }
        }

        void DrawDefectLineChart(
                                    Graphics g,
                                    int x,
                                    int y,
                                    int w,
                                    int h,
                                    int[] vals,
                                    string[] labels,
                                    Color lineColor
                                )
        {
            if (vals == null || labels == null || vals.Length == 0 || labels.Length == 0)
            {
                DrawNoDataText(g, x, y, w, h);
                return;
            }

            int n = Math.Min(vals.Length, labels.Length);
            if (n <= 0)
            {
                DrawNoDataText(g, x, y, w, h);
                return;
            }

            int padL = 34;
            int padB = 30;
            int padT = 4;
            int padR = 8;

            int cx = x + padL;
            int cy = y + padT;
            int cw = w - padL - padR;
            int chh = h - padB - padT;

            if (cw <= 0 || chh <= 0) return;

            double minV = 0;
            double maxV = 0;

            for (int i = 0; i < n; i++)
            {
                if (vals[i] > maxV)
                    maxV = vals[i];
            }

            if (maxV <= 0)
            {
                DrawNoDataText(g, x, y, w, h);
                return;
            }

            // 위쪽 여백
            maxV += maxV * 0.15;

            if (maxV <= 0) maxV = 1;

            // Y축 그리드
            using (Font fl = new Font("맑은 고딕", 10))
            using (SolidBrush bl = new SolidBrush(C_MUTED))
            using (Pen gridPen = new Pen(Color.FromArgb(0x1e, 0x30, 0x55), 1))
            {
                for (int i = 0; i <= 4; i++)
                {
                    float ly = cy + chh - (float)(i / 4.0 * chh);

                    g.DrawLine(gridPen, cx, ly, cx + cw, ly);

                    string yText = ((int)(minV + i / 4.0 * (maxV - minV))).ToString("#,##0");
                    g.DrawString(yText, fl, bl, x, ly - 6);
                }
            }

            PointF[] pts = new PointF[n];

            for (int i = 0; i < n; i++)
            {
                float px = n == 1
                    ? cx + cw / 2f
                    : cx + (float)i / (n - 1) * cw;

                double v = vals[i] * animT;

                float py = cy + chh - (float)((v - minV) / (maxV - minV) * chh);
                py = Math.Max(cy, Math.Min(cy + chh, py));

                pts[i] = new PointF(px, py);
            }

            // 아래 영역 채우기
            if (n > 1)
            {
                PointF[] fillPts = new PointF[n + 2];
                fillPts[0] = new PointF(pts[0].X, cy + chh);

                for (int i = 0; i < n; i++)
                    fillPts[i + 1] = pts[i];

                fillPts[n + 1] = new PointF(pts[n - 1].X, cy + chh);

                using (SolidBrush fillBr = new SolidBrush(Color.FromArgb(18, lineColor)))
                    g.FillPolygon(fillBr, fillPts);
            }

            // 선
            using (Pen linePen = new Pen(lineColor, 2.5f))
            {
                linePen.StartCap = LineCap.Round;
                linePen.EndCap = LineCap.Round;

                if (n > 1)
                    g.DrawLines(linePen, pts);
            }

            // 점 + 값
            using (Font fv = new Font("맑은 고딕", 8, FontStyle.Bold))
            using (SolidBrush textBr = new SolidBrush(C_TEXT))
            {
                for (int i = 0; i < n; i++)
                {
                    PointF pt = pts[i];

                    using (SolidBrush dotBr = new SolidBrush(lineColor))
                        g.FillEllipse(dotBr, pt.X - 4, pt.Y - 4, 8, 8);

                    using (SolidBrush innerBr = new SolidBrush(C_PANEL))
                        g.FillEllipse(innerBr, pt.X - 2, pt.Y - 2, 4, 4);

                    string valText = vals[i].ToString("#,##0");
                    SizeF vs = g.MeasureString(valText, fv);
                    g.DrawString(valText, fv, textBr, pt.X - vs.Width / 2, pt.Y - 18);
                }
            }

            // X축 라벨
            using (Font fx = new Font("맑은 고딕", 10))
            using (SolidBrush bx = new SolidBrush(C_MUTED))
            {
                for (int i = 0; i < n; i++)
                {
                    float px = n == 1
                        ? cx + cw / 2f
                        : cx + (float)i / (n - 1) * cw;

                    string labelText = ShortText(labels[i], 5);

                    SizeF sz = g.MeasureString(labelText, fx);
                    g.DrawString(labelText, fx, bx, px - sz.Width / 2, y + h - 24);
                }
            }
        }

        void DrawNoDataText(Graphics g, int x, int y, int w, int h)
        {
            using (Font f = new Font("맑은 고딕", 12, FontStyle.Bold))
            using (SolidBrush br = new SolidBrush(C_MUTED))
            {
                string txt = "데이터 없음";
                SizeF sz = g.MeasureString(txt, f);
                g.DrawString(txt, f, br, x + (w - sz.Width) / 2, y + (h - sz.Height) / 2);
            }
        }

        //string ShortText(string text, int maxLen)
        //{
        //    if (string.IsNullOrEmpty(text))
        //        return "";

        //    text = text.Trim();

        //    if (text.Length <= maxLen)
        //        return text;

        //    return text.Substring(0, maxLen) + "…";
        //}

        // ══════════════════════════════════════════════════
        //  공통 헬퍼
        // ══════════════════════════════════════════════════
        void FillPanel(Graphics g, Rectangle r)
        {
            using (SolidBrush br = new SolidBrush(C_PANEL))
            using (Pen pen = new Pen(C_BORDER, 1))
            {
                FillRoundRect(g, br, r, 10);
                DrawRoundRect(g, pen, r, 10);
            }
        }

        void DrawSecTitle(Graphics g, int x, int y, string title, Color col)
        {
            using (Font f = new Font("맑은 고딕", 11, FontStyle.Bold))
            using (SolidBrush br = new SolidBrush(col))
                g.DrawString(title, f, br, x, y);
        }

        static void FillRoundRect(Graphics g, Brush br, RectangleF r, float radius)
        {
            using (GraphicsPath path = RoundedPath(r, radius))
                g.FillPath(br, path);
        }

        static void DrawRoundRect(Graphics g, Pen pen, RectangleF r, float radius)
        {
            using (GraphicsPath path = RoundedPath(r, radius))
                g.DrawPath(pen, path);
        }

        static GraphicsPath RoundedPath(RectangleF r, float radius)
        {
            float d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion
        private bool isClosing = false;



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isClosing = true;

            StopTimer();

            base.OnFormClosing(e);
        }

        private void StopTimer()
        {
            if (timer == null) return;

            try
            {
                timer.Stop();
                timer.Enabled = false;
                timer.Tick -= Timer_Tick;
                timer.Dispose();
            }
            finally
            {
                timer = null;
            }
        }

        private void Frm_TotalMonitoring_Activated(object sender, EventArgs e)
        {
            Invalidate();
            if (timer != null && !timer.Enabled)
            {
                timer.Start();
            }
        }

        private void Frm_TotalMonitoring_Deactivate(object sender, EventArgs e)
        {
            if (timer != null && timer.Enabled)
            {
                timer.Stop();
            }
        }
    }
}
