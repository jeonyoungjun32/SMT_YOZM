using DevExpress.CodeParser;
using DevExpress.DashboardCommon;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using SmartCommon.Common;
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
    public partial class Frm_DT_MachineView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        public Frm_DT_MachineView()
        {
            InitializeComponent();

            // 상단 조회조건 색상 설정
            smtCommonBar1.smtBackColor = Color.Transparent;
            btn_Colose.ForeColor = Color.Black;
        }

        private void Frm_DT_MachineView_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            SetGrid_Work(Gv_Work);

            Search();

            Tm_Search.Interval = 360000;
            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)
        private void SetGrid_Work(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "가동상태", "WorkStatus");
            grid.Create_GvColumn(gv, "금일생산량", "GoodQty");
            grid.Create_GvColumn(gv, "가동률", "Utilization");
            grid.Create_GvColumn(gv, "양품률", "YieldRate");
            grid.Create_GvColumn(gv, "최근수집시각", "LastCollectTime");

            // 숫자 및 포맷 설정
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Utilization"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["YieldRate"], clsGrid.TypeList.TFloat);

            gv.Columns["MachineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["WorkStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
            //gv.CustomDrawCell += gridView1_CustomDrawCell;
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string value = e.CellValue.ConvertString_Null_To_Empty();

            if (e.Column.FieldName == "WorkStatus") //
            {
				if (value == "가동중" || value == "진행중" || value == "작업중" )
                {
                    e.Appearance.BackColor = Color.FromArgb(220, 245, 230); // 연한 초록색
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
                }
                else if (value == "정지")
                {
                    e.Appearance.BackColor = Color.FromArgb(253, 232, 232); // 연한 분홍/파스텔 레드
                    e.Appearance.ForeColor = Color.FromArgb(200, 50, 50);    // 진한 빨간색
                }
				else
				{
					e.Appearance.ForeColor = Color.FromArgb(100, 110, 120); // 진한 회색
				}
            }
        }
        #endregion

        #region 데이터 조회 (Search)
        private void Search()
        {
            SearchCardData();

            SearchGrid_Work();
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData()
        {
            query = $@"
                DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                DECLARE @Today DATE = CAST(GETDATE() AS DATE);

                WITH MachineData AS
                (
                    SELECT
                          cm.CorpId
                        , cm.MachineCode

                        -- 현재 생산 중
                        , CASE
                              WHEN EXISTS (
                                  SELECT 1
                                  FROM PM_OrderProcess op
                                  INNER JOIN PM_OrderResult ors ON op.CorpId = ors.CorpId AND op.OrderNo = ors.OrderNo AND op.OrderSeq = ors.OrderSeq
                                  WHERE op.CorpId = cm.CorpId
                                    AND op.MachineCode = cm.MachineCode
                                    AND ors.WorkDate >= @Today
                                    AND ors.WorkDate < DATEADD(DAY, 1, @Today)
                                    AND ors.StartDate IS NOT NULL
                                    AND ors.EndDate IS NULL
                              )
                              THEN 1
                              ELSE 0
                          END AS RunningMachine

                        -- 고장
                        , CASE
                              WHEN EXISTS (
                                  SELECT 1
                                  FROM EQ_Repair er
                                  WHERE er.CorpId = cm.CorpId
                                    AND er.MachineCode = cm.MachineCode
                                    AND er.RepairDate = CONVERT(VARCHAR(8), @Today, 112)
                                    AND er.RepairStartTime IS NOT NULL
                                    AND er.RepairEndTime IS NOT NULL
                                    AND GETDATE() >= er.RepairStartTime
                                    AND GETDATE() < er.RepairEndTime
                              )
                              THEN 1
                              ELSE 0
                          END AS FailureMachine

                        -- 금일 작업시간
                        , ISNULL((
                              SELECT SUM(
                                  CASE
                                      WHEN ors.StartDate IS NOT NULL
                                       AND ors.EndDate IS NOT NULL
                                      THEN DATEDIFF(MINUTE, ors.StartDate, ors.EndDate)
                                      ELSE 0
                                  END
                              )
                              FROM PM_OrderProcess op
                              INNER JOIN PM_OrderResult ors
                                ON op.CorpId = ors.CorpId
                               AND op.OrderNo = ors.OrderNo
                               AND op.OrderSeq = ors.OrderSeq
                              WHERE op.CorpId = cm.CorpId
                                AND op.MachineCode = cm.MachineCode
                                AND ors.WorkDate >= @Today
                                AND ors.WorkDate < DATEADD(DAY, 1, @Today)
                          ), 0) AS RunTime

                    FROM EQ_Machine cm
                    WHERE cm.CorpId = @CorpId AND cm.UseYn = 'Y'
                )

                SELECT
                      SUM(RunningMachine) AS RunningMachineCount

                    -- 고장 대수
                    , SUM(FailureMachine) AS FailureMachineCount

                    -- 대기 대수
                    , SUM(
                          CASE
                              WHEN RunningMachine = 0
                               AND FailureMachine = 0
                              THEN 1
                              ELSE 0
                          END
                      ) AS WaitingMachineCount

                    -- 평균 가동률
                    , CAST(
                          AVG(
                              CASE
                                  WHEN RunTime > 0
                                  THEN RunTime * 100.0 / 480
                                  ELSE 0
                              END
                          )
                      AS DECIMAL(5,1)) AS OperatingRate
                FROM MachineData;
            ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                lblRunningMachineCount.Text = dr["RunningMachineCount"].ToString() + "대";
                lblWaitingMachineCount.Text = dr["WaitingMachineCount"].ToString() + "대";
                lblFailureMachineCount.Text = dr["FailureMachineCount"].ToString() + "대";
                lblOperatRate.Text = dr["OperatingRate"].ToString() + "%";
            }
        }
        #endregion

        #region 조회 쿼리 (그리드)

        private void SearchGrid_Work()
        {
            // 설비별 생산 현황
            query = $@"
                        SELECT
                              cm.MachineCode
                            , cm.MachineName

                            -- 설비 상태
                            , CASE
                                  -- 현재 정지 중
                                  WHEN EXISTS (
                                      SELECT 1
                                      FROM EQ_Repair er
                                      WHERE er.CorpId = cm.CorpId
                                        AND er.MachineCode = cm.MachineCode
                                        AND er.RepairDate = CONVERT(VARCHAR(8), GETDATE(), 112)
                                        AND er.RepairStartTime IS NOT NULL
                                        AND er.RepairEndTime IS NOT NULL
                                        AND GETDATE() >= er.RepairStartTime
                                        AND GETDATE() < er.RepairEndTime
                                  )
                                  THEN '정지'

                                  -- 금일 생산 중
                                  WHEN EXISTS (
                                      SELECT 1
                                      FROM PM_OrderProcess op
                                      INNER JOIN PM_OrderResult ors ON op.CorpId = ors.CorpId AND op.OrderNo = ors.OrderNo AND op.OrderSeq = ors.OrderSeq
                                      WHERE op.CorpId = cm.CorpId
                                        AND op.MachineCode = cm.MachineCode
                                        AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                        AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                                        AND ors.StartDate IS NOT NULL
                                        AND ors.EndDate IS NULL
                                  )
                                  THEN '가동중'

                                  -- 금일 생산 완료
                                  WHEN EXISTS (
                                      SELECT 1
                                      FROM PM_OrderProcess op
                                      INNER JOIN PM_OrderResult ors ON op.CorpId = ors.CorpId AND op.OrderNo = ors.OrderNo AND op.OrderSeq = ors.OrderSeq
                                      WHERE op.CorpId = cm.CorpId
                                        AND op.MachineCode = cm.MachineCode
                                        AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                        AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                                        AND ors.EndDate IS NOT NULL
                                  )
                                  THEN '완료'

                                  ELSE '대기'
                              END AS WorkStatus

                            -- 금일 생산량
                            , ISNULL((
                                  SELECT SUM(ISNULL(ors.GoodQty, 0))
                                  FROM PM_OrderProcess op
                                  INNER JOIN PM_OrderResult ors  ON op.CorpId = ors.CorpId AND op.OrderNo = ors.OrderNo AND op.OrderSeq = ors.OrderSeq
                                  WHERE op.CorpId = cm.CorpId
                                    AND op.MachineCode = cm.MachineCode
                                    AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                    AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                              ), 0) AS GoodQty

                            -- 가동률
                            , CASE
                                  WHEN ISNULL((
                                      SELECT SUM(
                                          CASE
                                              WHEN ors.StartDate IS NOT NULL
                                               AND ors.EndDate IS NOT NULL
                                              THEN DATEDIFF(MINUTE, ors.StartDate, ors.EndDate)
                                              ELSE 0
                                          END
                                      )
                                      FROM PM_OrderProcess op
                                      INNER JOIN PM_OrderResult ors
                                        ON op.CorpId = ors.CorpId
                                       AND op.OrderNo = ors.OrderNo
                                       AND op.OrderSeq = ors.OrderSeq
                                      WHERE op.CorpId = cm.CorpId
                                        AND op.MachineCode = cm.MachineCode
                                        AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                        AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                                  ), 0) > 0
                                  THEN ROUND(
                                      ISNULL((
                                          SELECT SUM(
                                              CASE
                                                  WHEN ors.StartDate IS NOT NULL
                                                   AND ors.EndDate IS NOT NULL
                                                  THEN DATEDIFF(MINUTE, ors.StartDate, ors.EndDate)
                                                  ELSE 0
                                              END
                                          )
                                          FROM PM_OrderProcess op
                                          INNER JOIN PM_OrderResult ors
                                            ON op.CorpId = ors.CorpId
                                           AND op.OrderNo = ors.OrderNo
                                           AND op.OrderSeq = ors.OrderSeq
                                          WHERE op.CorpId = cm.CorpId
                                            AND op.MachineCode = cm.MachineCode
                                            AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                            AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                                      ), 0) * 100.0 / 480
                                  , 2)
                                  ELSE 0
                              END AS Utilization

                            -- 양품률
                            , CASE
                                  WHEN ISNULL((
                                      SELECT SUM(
                                            ISNULL(ors.GoodQty, 0)
                                          + ISNULL(ors.NgQty, 0)
                                      )
                                      FROM PM_OrderProcess op
                                      INNER JOIN PM_OrderResult ors
                                        ON op.CorpId = ors.CorpId
                                       AND op.OrderNo = ors.OrderNo
                                       AND op.OrderSeq = ors.OrderSeq
                                      WHERE op.CorpId = cm.CorpId
                                        AND op.MachineCode = cm.MachineCode
                                        AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                        AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                                  ), 0) > 0
                                  THEN ROUND(
                                      CAST(ISNULL((
                                          SELECT SUM(ISNULL(ors.GoodQty, 0))
                                          FROM PM_OrderProcess op
                                          INNER JOIN PM_OrderResult ors
                                            ON op.CorpId = ors.CorpId
                                           AND op.OrderNo = ors.OrderNo
                                           AND op.OrderSeq = ors.OrderSeq
                                          WHERE op.CorpId = cm.CorpId
                                            AND op.MachineCode = cm.MachineCode
                                            AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                            AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                                      ), 0) AS DECIMAL(18, 2))
                                      /
                                      NULLIF((
                                          SELECT SUM(
                                                ISNULL(ors.GoodQty, 0)
                                              + ISNULL(ors.NgQty, 0)
                                          )
                                          FROM PM_OrderProcess op
                                          INNER JOIN PM_OrderResult ors ON op.CorpId = ors.CorpId
                                           AND op.MachineCode = cm.MachineCode
                                           AND op.OrderNo = ors.OrderNo
                                           AND op.OrderSeq = ors.OrderSeq
                                          WHERE op.CorpId = cm.CorpId
                                            AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                            AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                                      ), 0) * 100
                                  , 2)
                                  ELSE 0
                              END AS YieldRate

                            -- 최근 수집시각
                            , (
                                  SELECT MAX(ors.WorkDate)
                                  FROM PM_OrderProcess op
                                  INNER JOIN PM_OrderResult ors ON op.CorpId = ors.CorpId AND op.OrderNo = ors.OrderNo AND op.OrderSeq = ors.OrderSeq
                                  WHERE op.CorpId = cm.CorpId
                                    AND op.MachineCode = cm.MachineCode
                                    AND ors.WorkDate >= CAST(GETDATE() AS DATE)
                                    AND ors.WorkDate < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))
                              ) AS LastCollectTime

                        FROM EQ_Machine cm
                        WHERE cm.CorpId = '{WrGlobal.CorpID}'
                        ORDER BY cm.MachineName;
                    ";

            Gc_Work.DataSource = db.GetDataTable(query);
            Gv_Work.BestFitColumns();
        }
    
        #endregion

        #region Event
        private void timer1_Tick(object sender, EventArgs e)
        {
            LblCurTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void Tm_Search_Tick(object sender, EventArgs e)
        {
            Search();
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            string value = toggleSwitch1.EditValue.ConvertString_Null_To_Empty();

            //On - 자동 조회
            if (toggleSwitch1.IsOn)
            {
                Search();
                Tm_Search.Enabled = true;
            }
            else
            {
                Tm_Search.Enabled = false;
            }
        }

        private void btn_Colose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}