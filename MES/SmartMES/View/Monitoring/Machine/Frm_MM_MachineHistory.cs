using DevExpress.CodeParser;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.PM;
using System;
using System.Data;
using System.Drawing;

namespace SmartMES.View.Monitoring.Machine
{
	public partial class Frm_MM_MachineHistory : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        public Frm_MM_MachineHistory()
        {
            InitializeComponent();

            // 상단 조회조건 색상 설정
            smtCommonBar1.smtBackColor = Color.Transparent;

            barDate.smtLabelForeColor = Color.Black;
            LupProcess.smtLabelForeColor = Color.Black;
            LupItem.smtLabelForeColor = Color.Black;

            smtSearchButton1.ForeColor = Color.Black;
            btn_Colose.ForeColor = Color.Black;
        }

        private void Frm_AN_DefectType_Load(object sender, EventArgs e)
        {
            barDate.smtEndDateTimeValue = DateTime.Now;

            Tm_Time.Start();

            SetGrid(Gv_List);
            SetGrid_Detail(Gv_Detail);

            Search();

            Tm_Search.Start();
        }

        #region 기본 설정 (Grid)

        private void SetGrid(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            RepositoryItemSearchLookUpEdit lup_User = rpo.Create_RepositoryItemSearchLookUp(code.Get_UserList(), "코드", "코드명");

            grid.Create_GvColumn(gv, "일자", "WorkDate");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "이력구분", "HistoryType");
            grid.Create_GvColumn(gv, "작업구분", "WorkType");
            grid.Create_GvColumn(gv, "내용", "Content");
            grid.Create_GvColumn(gv, "작업자", "WorkerEmpNo", lup_User);
            grid.Create_GvColumn(gv, "작업시간", "WorkTime");
            grid.Create_GvColumn(gv, "다음점검일", "Nextdate");
            grid.Create_GvColumn(gv, "구분", "DivCd");

            //
            grid.SetCol_Type(gv.Columns["WorkDate"], clsGrid.TypeList.TDateTime);
            grid.SetCol_Type(gv.Columns["WorkTime"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Nextdate"], clsGrid.TypeList.TDateTime);

            gv.Columns["Content"].MaxWidth = 400;

            gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["DivCd"].Visible = false;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
			gv.RowCellStyle += Gv_RowCellStyle;
            gv.FocusedRowChanged += Gv_FocusedRowChanged;
        }

        private void Gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null || e.FocusedRowHandle < 0) return;

            GetDetailData(gv, e.FocusedRowHandle);
        }

        private void GetDetailData(GridView gv, int rowHandle)
        {
            if (gv == null || rowHandle < 0) return;

            string historyType = gv.GetRowCellValue(rowHandle, "HistoryType")?.ToString();
            string machineCode = gv.GetRowCellValue(rowHandle, "MachineCode")?.ToString();
            string workDate = gv.GetRowCellValue(rowHandle, "WorkDate")?.ToString();
            string divCd = gv.GetRowCellValue(rowHandle, "DivCd")?.ToString();

            DataTable dtDetail = new DataTable();

            if (historyType == "수리" || historyType == "고장")
            {
                dtDetail = SearchDetail_Repair(divCd);
            }
            else if (historyType == "점검")
            {
                dtDetail = SearchDetail_Check(machineCode, workDate, divCd);
            }

            Gc_Detail.DataSource = dtDetail;
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
            if(e.Column.FieldName == "MachineCode")
			{
                e.Appearance.ForeColor = Color.DodgerBlue;
			}

            if (e.Column.FieldName == "ItemValue")
            {
                string value = e.CellValue.ConvertString_Null_To_Empty();

                if(value == "고장")
                {
                    //e.Appearance.BackColor = Color.FromArgb(255, 102, 102);
                    e.Appearance.ForeColor = Color.FromArgb(245, 12, 0);
                }
            }
        }

		private void SetGrid_Detail(GridView gv)
        {
            gv.OptionsView.ShowGroupPanel = false;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.IndicatorWidth = 50;

            grid.Create_GvColumn(gv, "항목", "ItemName");
            grid.Create_GvColumn(gv, "상세내용", "ItemValue");

            gv.OptionsView.RowAutoHeight = true;

            if (gv.Columns["ItemValue"] != null)
            {
                // 텍스트 줄바꿈
                gv.Columns["ItemValue"].AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                // MemoEdit로 지정하여 멀티라인(여러 줄) 표현
                DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit memoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                memoEdit.WordWrap = true;
                gv.Columns["ItemValue"].ColumnEdit = memoEdit;
            }

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
        }

        #endregion

        #region 데이터 조회

        private void Search()
        {
            string startDt = barDate.smtStartDateTimeValue.ToString("yyyy-MM-01");
            string endDt = Convert.ToDateTime(startDt).AddMonths(1).AddDays(-1).ToString("d");

            SearchCardData(startDt, endDt);
            SearchGrid(startDt, endDt);
        }

        #endregion

        #region 대시보드 카드 데이터 로드

        private void SearchCardData(string startDt, string endDt)
        {
            query = $@"
                        DECLARE @CorpId NVARCHAR(20) = '{WrGlobal.CorpID}';
                        DECLARE @StartDate DATE = '{startDt}'
                        DECLARE @EndDate DATE = '{endDt}'
                        DECLARE @Today VARCHAR(8) = CONVERT(VARCHAR(8), GETDATE(), 112); 

                        WITH
                        -- 조회 기간 내 수리이력이 있는 설비 목록
                        RepairMachine AS (
                            SELECT DISTINCT MachineCode
                            FROM EQ_Repair
                            WHERE CorpId = @CorpId
                              AND CONVERT(DATE, RepairDate) BETWEEN @StartDate AND @EndDate
                        ),

                        -- 조회 기간 내 점검이력이 있는 설비 목록
                        ChkMachine AS (
                            SELECT DISTINCT MachineCode
                            FROM EQ_MachineChk
                            WHERE CorpId = @CorpId
                              AND CONVERT(DATE, InspectDate) BETWEEN @StartDate AND @EndDate
                        ),

                        -- 설비별 가장 최근 점검일 기준 금일 점검 예정 여부
                        LastChk AS (
                            SELECT
                                MachineCode,
                                CONVERT(VARCHAR(8), DATEADD(DAY, 30, CONVERT(DATE, MAX(InspectDate))), 112) AS NextInspectDate
                            FROM EQ_MachineChk
                            WHERE CorpId = @CorpId
                            GROUP BY MachineCode
                        )

                        SELECT
                            -- 전체 설비 대수
                            COUNT(M.MachineCode) AS TotalMachineCnt,

                            -- 정상 가동 대수 (전체 설비 대수 - 고장 설비 대수)
                            COUNT(M.MachineCode) - COUNT(PR.MachineCode) AS NormalMachineCnt,

                            -- 점검 대수
                            COUNT(PC.MachineCode) AS MaintenanceMachineCnt,

                            -- 고장 대수
                            COUNT(PR.MachineCode) AS FaultMachineCnt,

                            -- 금일 점검 예정 대수
                            SUM(CASE WHEN LC.NextInspectDate = @Today THEN 1 ELSE 0 END) AS TodayInspectPlanCnt

                        FROM EQ_Machine M
                        LEFT JOIN RepairMachine PR ON M.MachineCode = PR.MachineCode
                        LEFT JOIN ChkMachine PC    ON M.MachineCode = PC.MachineCode
                        LEFT JOIN LastChk LC       ON M.MachineCode = LC.MachineCode
            ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                int totalCnt = dr["TotalMachineCnt"].ConvertInt_Null_To_Zero();
                int normalCnt = dr["NormalMachineCnt"].ConvertInt_Null_To_Zero();
                int maintCnt = dr["MaintenanceMachineCnt"].ConvertInt_Null_To_Zero();
                int todayPlan = dr["TodayInspectPlanCnt"].ConvertInt_Null_To_Zero();
                int faultCnt = dr["FaultMachineCnt"].ConvertInt_Null_To_Zero();

                LblTotal.Text = totalCnt.ToString("N0") + " 대";
                LblNomal.Text = normalCnt.ToString("N0") + " 대";
                LblRepair.Text = maintCnt.ToString("N0") + " 대";
                LblInspect.Text = todayPlan.ToString("N0") + " 건";
                LblNg.Text = faultCnt.ToString("N0") + " 대";
            }
        }

        #endregion

        #region 조회 쿼리

        private void SearchGrid(string startDt, string endDt)
        {
            query = $@"
            DECLARE @CorpId nvarchar(20) = '{WrGlobal.CorpID}'
            , @StartDate DATE = '{startDt}'
            , @EndDate DATE = '{endDt}';

            SELECT
                 CorpId
                , WorkDate
	            , DivCd
                , MachineCode
                , MachineName
                , HistoryType

                , CASE
                    WHEN DivCd = '10' THEN '일상점검'
                    WHEN DivCd = '20' THEN '정기점검'
                    ELSE WorkType
	              END AS WorkType

                , Content
                , WorkerEmpNo
                , WorkTime

                , CASE
                    WHEN HistoryType = '점검' THEN CONVERT(VARCHAR(8), DATEADD(DAY, 30, CONVERT(DATE, WorkDate)), 112)
                    ELSE '-'
                  END AS Nextdate
            FROM (
                SELECT
                    R.CorpId
		            , R.RepairNo as DivCd
                    , R.RepairDate  AS WorkDate
                    , R.MachineCode
                    , M.MachineName
                    , '고장' AS HistoryType
                    , '수리' AS WorkType
                    , R.RepairCause AS Content
                    , R.RepairEmpNo AS WorkerEmpNo
                    --, CAST(ROUND(CAST(R.RepairTime AS FLOAT) / 60.0, 1) AS DECIMAL(10, 1)) AS WorkTime
                    , CASE
                        WHEN R.RepairStartTime IS NOT NULL AND R.RepairEndTime IS NOT NULL
                        THEN CAST(ROUND(DATEDIFF(MINUTE, R.RepairStartTime, R.RepairEndTime) / 60.0, 1) AS DECIMAL(10, 1))
                        ELSE CAST(ROUND(CAST(R.RepairTime AS FLOAT) / 60.0, 1) AS DECIMAL(10, 1))
                     END AS WorkTime
                FROM EQ_Repair R
                LEFT JOIN EQ_Machine M ON R.CorpId = M.CorpId AND R.MachineCode = M.MachineCode

                UNION ALL

                SELECT
                    C.CorpId
					, C.InspectDivCd as DivCd
                    , C.InspectDate AS WorkDate
                    , C.MachineCode
                    , M.MachineName
                    , '점검' AS HistoryType
                    , C.InspectDivCd AS WorkType
                    , CASE
                        WHEN SUM(CASE WHEN C.InspectCheck = 'N' THEN 1 ELSE 0 END) > 0
                        THEN '점검 실시 (확인 필요)'
                        ELSE '점검 실시 (이상 없음)'
                    END AS Content
                    , MAX(C.ChargeEmpNo) AS WorkerEmpNo
                    , CAST(NULL AS DECIMAL(10, 1)) AS WorkTime
                FROM EQ_MachineChk C
                LEFT JOIN EQ_Machine M ON C.CorpId = M.CorpId AND C.MachineCode = M.MachineCode
                GROUP BY C.CorpId, C.InspectDate, C.MachineCode, M.MachineName, C.InspectDivCd
            ) A
            WHERE CorpId = @CorpId
            AND CONVERT(DATE, WorkDate) BETWEEN @StartDate AND @EndDate
            ORDER BY WorkDate DESC, MachineCode
            ";

            DataTable dt = db.GetDataTable(query);
            Gc_List.DataSource = dt;
            Gv_List.BestFitColumns();

            if (dt != null && dt.Rows.Count > 0)
            {
                Gv_List.FocusedRowHandle = 0;
                GetDetailData(Gv_List, 0);
            }
            else
            {
                Gc_Detail.DataSource = null;
            }
        }

		private DataTable SearchDetail_Repair(string repairNo)
		{
            string query = $@"
            SELECT
                 ItemName
                , ItemValue
            FROM (
                SELECT
                    R.MachineCode
                    , M.MachineName
                    , '고장' AS HistoryType
		            , '수리' AS WorkType
                    , R.RepairCause
                    , ISNULL(CONVERT(VARCHAR(16), R.RepairStartTime, 120), R.RepairDate) AS RepairDate
                    , U.EmployeeName
                    --, CAST(CAST(R.RepairTime AS FLOAT) / 60.0 AS VARCHAR(10)) + ' 시간' AS RepairTime
                    , CASE
                       WHEN R.RepairStartTime IS NOT NULL AND R.RepairEndTime IS NOT NULL
                       THEN CAST(CAST(ROUND(DATEDIFF(MINUTE, R.RepairStartTime, R.RepairEndTime) / 60.0, 1) AS DECIMAL(10, 1)) AS VARCHAR(20)) + ' 시간'
                       WHEN R.RepairTime IS NOT NULL
                       THEN CAST(CAST(ROUND(CAST(R.RepairTime AS FLOAT) / 60.0, 1) AS DECIMAL(10, 1)) AS VARCHAR(20)) + ' 시간'
                       ELSE ''
                    END AS WorkTime
                    , R.RepairState
                    , R.Remark
                    , NULL AS UsedPart
                    , '-' AS NextDay
                FROM EQ_Repair R
                LEFT JOIN EQ_Machine M ON R.CorpId = M.CorpId AND R.MachineCode = M.MachineCode
            	LEFT JOIN SM_User U ON R.CorpId = U.CorpId AND R.RepairEmpNo = U.EmployeeId
                WHERE R.CorpId = '{WrGlobal.CorpID}'
                  AND R.RepairNo = '{repairNo}'
            ) A
            CROSS APPLY (
                VALUES
                    (1,  '설비코드',   A.MachineCode)
                    , (2,  '설비명',     A.MachineName)
                    , (3,  '이력구분',   A.HistoryType)
                    , (4,  '작업구분',   A.WorkType)
                    , (5,  '작업일시',   A.RepairDate)
                    , (6,  '작업자',     A.EmployeeName)
                    , (7,  '작업시간',   A.WorkTime)
                    , (8,  '내용',       A.RepairCause)
                    , (9,  '조치내용',   A.RepairState)
                    --, (10, '사용부품',   A.UsedPart)
                    , (11, '다음점검일', A.NextDay)
            ) AS C(SortOrder, ItemName, ItemValue)
            ORDER BY SortOrder
            ";

            return db.GetDataTable(query);
        }

        private DataTable SearchDetail_Check(string machineCode, string inspectDate, string inspectDivCd)
        {
            string query = $@"
                SELECT
                    ItemName
                    , ItemValue
                FROM (
                    SELECT
                        C.MachineCode
                        , M.MachineName
                        , '점검' AS HistoryType
                        , CASE
                            WHEN C.InspectDivCd = '10' THEN '일상점검'
                            WHEN C.InspectDivCd = '20' THEN '정기점검'
                          END AS WorkType
                        , ISNULL(MAX(C.InspectDate), '-') AS InspectDate
                        , MAX(U.EmployeeName)  AS EmployeeName
                        , '' AS WorkTime

                        , CASE
                            WHEN SUM(CASE WHEN C.InspectCheck = 'N' THEN 1 ELSE 0 END) > 0
                            THEN '점검 실시 (확인 필요)'
                            ELSE '점검 실시 (이상 없음)'
                          END AS Content

                        -- 조치 메모가 있는 경우 쉼표로 연결
                        , ISNULL( STRING_AGG(NULLIF(TRIM(C.ActionMemo), ''), ', '), '') AS ActionMemo
                        , '' AS UsedPart

                        -- 다음 점검일 (30일 뒤 계산)
                        , ISNULL(CONVERT(VARCHAR(8), DATEADD(DAY, 30, CONVERT(DATE, MAX(C.InspectDate))), 112), '-') AS NextDay
                    FROM EQ_MachineChk C
                    LEFT JOIN EQ_Machine M ON C.CorpId = M.CorpId AND C.MachineCode = M.MachineCode
                    LEFT JOIN SM_User U ON C.CorpId = U.CorpId AND C.ChargeEmpNo = U.EmployeeId
                    WHERE C.CorpId = '{WrGlobal.CorpID}'
                      AND C.MachineCode = '{machineCode}'
                      AND C.InspectDate = '{inspectDate}'
                      AND C.InspectDivCd = '{inspectDivCd}'
                    GROUP BY C.CorpId, C.MachineCode, M.MachineName, C.InspectDivCd
                ) A
                CROSS APPLY (
                    VALUES
                        (1,  '설비코드',   CAST(A.MachineCode AS NVARCHAR(MAX)))
                        , (2,  '설비명',     CAST(A.MachineName AS NVARCHAR(MAX)))
                        , (3,  '이력구분',   CAST(A.HistoryType AS NVARCHAR(MAX)))
                        , (4,  '작업구분',   CAST(A.WorkType AS NVARCHAR(MAX)))
                        , (5,  '작업일시',   CAST(A.InspectDate AS NVARCHAR(MAX)))
                        , (6,  '작업자',     CAST(A.EmployeeName AS NVARCHAR(MAX)))
                        , (7,  '작업시간',   CAST(A.WorkTime AS NVARCHAR(MAX)))
                        , (8,  '내용',       CAST(A.Content AS NVARCHAR(MAX)))
                        , (9,  '조치내용',   CAST(A.ActionMemo AS NVARCHAR(MAX)))
                        , (10, '다음점검일', CAST(A.NextDay AS NVARCHAR(MAX)))
                ) AS C(SortOrder, ItemName, ItemValue)
                ORDER BY SortOrder
            ";

            return db.GetDataTable(query);
        }

        #endregion

        #region event

        private void timer1_Tick(object sender, EventArgs e)
		{
            //LblCurTime.Text = DateTime.Now.ToString("yyyy.MM.dd HH:MM:ss");
        }

		private void Tm_Search_Tick(object sender, EventArgs e)
		{
            Search();
		}

        private void smtSearchButton1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_Colose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}