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
    public partial class Frm_DT_Monitoring : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        Database db = new Database();
        string query = "";

        public Frm_DT_Monitoring()
        {
            InitializeComponent();

            // 상단 조회조건 색상 설정
            smtCommonBar1.smtBackColor = Color.Transparent;
            btn_Colose.ForeColor = Color.Black;
        }

        private void Frm_DT_MachineView_Load(object sender, EventArgs e)
        {
            Tm_Time.Start();

            SetGrid_Work(Gv_Machine);

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

            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "최근수집시각", "LastCollectTime");
            grid.Create_GvColumn(gv, "경과시간", "ElapsedTime");
            grid.Create_GvColumn(gv, "수신상태", "ReceiveStatus");
            grid.Create_GvColumn(gv, "금일누락횟수", "TodayMissCount");

            gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["LastCollectTime"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ElapsedTime"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ReceiveStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["TodayMissCount"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;
            gv.RowCellStyle += Gv_RowCellStyle;
        }

        private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            string value = e.CellValue.ConvertString_Null_To_Empty();

            if (e.Column.FieldName == "ReceiveStatus")
            {
                if (value == "정상")
                {
                    e.Appearance.BackColor = Color.FromArgb(220, 245, 230); // 연한 초록색
                    e.Appearance.ForeColor = Color.FromArgb(40, 140, 70);   // 진한 초록색
                }
                else if (value == "지연")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 249, 235); // 연한 노란색
                    e.Appearance.ForeColor = Color.FromArgb(230, 126, 0);   // 진한 주황색
                }
                else if (value == "누락")
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
            DataTable dt = SearchGrid_Work();

            SearchCardData(dt);
        }
        #endregion

        #region 대시보드 카드 데이터 로드
        private void SearchCardData(DataTable dt)
        {
            Control[] machineLabels = new Control[]
            {
                Lbl_Machine1,
                Lbl_Machine2,
                Lbl_Machine3,
                Lbl_Machine4,
                Lbl_Machine5,
                Lbl_Machine6
            };
            Control[] statusLabels = new Control[]
            {
                Lbl_Status1,
                Lbl_Status2,
                Lbl_Status3,
                Lbl_Status4,
                Lbl_Status5,
                Lbl_Status6
            };
            Control[] panels = new Control[]
            {
                Pnl1,
                Pnl2,
                Pnl3,
                Pnl4,
                Pnl5,
                Pnl6
            };

            for (int i = 0; i < machineLabels.Length; i++)
            {
                machineLabels[i].Text = "";
                statusLabels[i].Text = "";
                SetCardColor(panels[i], machineLabels[i], statusLabels[i], "");
            }

            if (dt == null)
            {
                return;
            }

            int cardCount = Math.Min(dt.Rows.Count, machineLabels.Length);

            for (int i = 0; i < cardCount; i++)
            {
                string machineName = dt.Rows[i]["MachineName"].ConvertString_Null_To_Empty();
                string receiveStatus = dt.Rows[i]["ReceiveStatus"].ConvertString_Null_To_Empty();

                machineLabels[i].Text = machineName;
                statusLabels[i].Text = receiveStatus;
                SetCardColor(panels[i], machineLabels[i], statusLabels[i], receiveStatus);
            }
        }

        private void SetCardColor(Control panel, Control machineLabel, Control statusLabel, string receiveStatus)
        {
            Color backColor = Color.White;
            Color foreColor = Color.FromArgb(100, 110, 120);

            if (receiveStatus == "정상")
            {
                backColor = Color.FromArgb(239, 250, 244);
                foreColor = Color.FromArgb(0, 155, 88);
            }
            else if (receiveStatus == "지연")
            {
                backColor = Color.FromArgb(255, 249, 235);
                foreColor = Color.FromArgb(230, 126, 0);
            }
            else if (receiveStatus == "누락")
            {
                backColor = Color.FromArgb(255, 242, 242);
                foreColor = Color.FromArgb(235, 40, 40);
            }

            panel.BackColor = backColor;
            machineLabel.ForeColor = foreColor;
            statusLabel.ForeColor = foreColor;

            PanelControl panelControl = panel as PanelControl;
            LabelControl machineLabelControl = machineLabel as LabelControl;
            LabelControl statusLabelControl = statusLabel as LabelControl;

            if (panelControl != null)
            {
                panelControl.Appearance.BackColor = backColor;
                panelControl.Appearance.Options.UseBackColor = true;
            }

            if (machineLabelControl != null)
            {
                machineLabelControl.Appearance.ForeColor = foreColor;
                machineLabelControl.Appearance.Options.UseForeColor = true;
            }

            if (statusLabelControl != null)
            {
                statusLabelControl.Appearance.ForeColor = foreColor;
                statusLabelControl.Appearance.Options.UseForeColor = true;
            }
        }
        #endregion

        #region 조회 쿼리 (그리드)

        private DataTable SearchGrid_Work()
        {
            query = $@"
                        DECLARE @CorpId NVARCHAR(10) = '{WrGlobal.CorpID}';
                        DECLARE @ThresholdSec INT = 60;
                        DECLARE @TodayStart CHAR(19) = CONVERT(CHAR(10), GETDATE(), 120) + ' 00:00:00';
                        DECLARE @TomorrowStart CHAR(19) = CONVERT(CHAR(10), DATEADD(DAY, 1, GETDATE()), 120) + ' 00:00:00';

                        WITH Equipment AS
                        (
                            SELECT
                                  C.Corpid
                                , C.Equip_Name AS MachineCode
                                , ISNULL(M.MachineName, C.Equip_Name) AS MachineName
                                , MAX(T.LastTime) AS LastCollectTime
                            FROM IF_DGPCode C
                            INNER JOIN EQ_Machine M
                                ON M.CorpId = C.Corpid
                               AND M.MachineCode = C.Equip_Name
                            LEFT JOIN IF_DgpCounter T
                                ON T.Corpid = C.Corpid
                               AND T.DgpCode = C.DgpCode
                            WHERE C.Corpid = @CorpId
                              AND ISNULL(C.Equip_Name, '') <> ''
                            GROUP BY
                                  C.Corpid
                                , C.Equip_Name
                                , M.MachineName
                        ),
                        TodayReceive AS
                        (
                            SELECT DISTINCT
                                  C.Corpid
                                , C.Equip_Name AS MachineCode
                                , TRY_CONVERT(DATETIME, H.DgpTime) AS ReceiveTime
                            FROM IF_DgpHistory H
                            INNER JOIN IF_DGPCode C
                                ON C.Corpid = H.Corpid
                               AND C.DgpCode = H.DgpCode
                            INNER JOIN Equipment E
                                ON E.Corpid = C.Corpid
                               AND E.MachineCode = C.Equip_Name
                            WHERE H.DgpTime >= @TodayStart
                              AND H.DgpTime < @TomorrowStart
                              AND TRY_CONVERT(DATETIME, H.DgpTime) IS NOT NULL
                        ),
                        ReceiveGap AS
                        (
                            SELECT
                                  R.Corpid
                                , R.MachineCode
                                , R.ReceiveTime
                                , LAG(R.ReceiveTime) OVER
                                  (
                                      PARTITION BY R.Corpid, R.MachineCode
                                      ORDER BY R.ReceiveTime
                                  ) AS PreviousReceiveTime
                            FROM TodayReceive R
                        ),
                        MissSummary AS
                        (
                            SELECT
                                  G.Corpid
                                , G.MachineCode
                                , COUNT(*) AS MissCount
                            FROM ReceiveGap G
                            WHERE G.PreviousReceiveTime IS NOT NULL
                              AND DATEDIFF(SECOND, G.PreviousReceiveTime, G.ReceiveTime) > @ThresholdSec
                            GROUP BY
                                  G.Corpid
                                , G.MachineCode
                        )
                        SELECT
                              E.MachineCode
                            , E.MachineName
                            , E.LastCollectTime
                            , CASE
                                  WHEN E.LastCollectTime IS NULL THEN ''
                                  WHEN A.ElapsedSec < 60 THEN CONVERT(VARCHAR(10), A.ElapsedSec) + '초'
                                  WHEN A.ElapsedSec < 3600 THEN CONVERT(VARCHAR(10), A.ElapsedSec / 60) + '분'
                                  ELSE CONVERT(VARCHAR(10), A.ElapsedSec / 3600) + '시간'
                              END AS ElapsedTime
                            , CASE
                                  WHEN E.LastCollectTime IS NULL THEN '누락'
                                  WHEN A.ElapsedSec > @ThresholdSec THEN '누락'
                                  WHEN A.ElapsedSec * 2 >= @ThresholdSec THEN '지연'
                                  ELSE '정상'
                              END AS ReceiveStatus
                            , ISNULL(S.MissCount, 0)
                              + CASE
                                    WHEN E.LastCollectTime IS NULL
                                      OR A.ElapsedSec > @ThresholdSec
                                    THEN 1
                                    ELSE 0
                                END AS TodayMissCount
                        FROM Equipment E
                        OUTER APPLY
                        (
                            SELECT
                                CASE
                                    WHEN E.LastCollectTime IS NULL THEN NULL
                                    WHEN E.LastCollectTime > GETDATE() THEN 0
                                    ELSE DATEDIFF(SECOND, E.LastCollectTime, GETDATE())
                                END AS ElapsedSec
                        ) A
                        LEFT JOIN MissSummary S
                            ON S.Corpid = E.Corpid
                           AND S.MachineCode = E.MachineCode
                        ORDER BY
                            CASE
                                WHEN E.LastCollectTime IS NULL THEN 3
                                WHEN A.ElapsedSec > @ThresholdSec THEN 3
                                WHEN A.ElapsedSec * 2 >= @ThresholdSec THEN 2
                                ELSE 1
                            END DESC,
                            E.MachineName;
                    ";

            DataTable dt = db.GetDataTable(query);

            Gc_Machine.DataSource = dt;
            Gv_Machine.BestFitColumns();

            return dt;
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
