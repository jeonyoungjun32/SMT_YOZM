using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using System;
using SmartCommon.Common;
using System.Windows.Forms;

namespace SmartMES.View.Monitoring.Machine
{
    public partial class Frm_PM_MachineOperatingRate : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsGrid grid = new clsGrid();
        Timer tm = new Timer();
        string query = "";

        public Frm_PM_MachineOperatingRate()
        {
            InitializeComponent();
            Set_GridView(gvMachine);
        }

        private void Frm_PM_MachineOperatingRate_Load(object sender, EventArgs e)
        {
            Search();

            tm.Start();
            Tm_Search.Start();
            TmTime.Enabled = true;

            if (Database.DbName.ToUpper().Contains("UAGHITEC"))
            {
                LblMainTitle.Text = "설비가동모니터링";
            }
        }

        private void TmTime_Tick(object sender, EventArgs e)
        {
            LblTime.Text = DateTime.Now.ToString();
        }

        private void Tm_Search_Tick(object sender, EventArgs e)
        {
            Search();
        }

        private void Set_GridView(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
                                                                                                        //gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsView.RowAutoHeight = true;    //MemoEdit MultiLine

            gv.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 20.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            gv.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 18.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            //grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "가동시간(분)", "WorkTime");
            grid.Create_GvColumn(gv, "비가동시간(분)", "DownTime");
            grid.Create_GvColumn(gv, "생산시간(분)", "ProductionTime");
            grid.Create_GvColumn(gv, "가동율(%)", "OperatingRate");

            //grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["Rate"], clsGrid.TypeList.TFloatN2);

            //칼럼Visible
            gv.Columns["ProductionTime"].Visible = false;
            //gv.Columns["OrderDate"].Visible = false;
            //gv.Columns["WorkQty"].Visible = false;
        }

        private void Search()
        {
            string FromDate = DateTime.Today.AddDays(-3).ConvertDate_To_Char8();
            string ToDate = DateTime.Today.ConvertDate_To_Char8();

            query = $@"
                        SELECT
                            cm.CorpId ,
	                        CM.MachineCode ,
	                        CM.MachineName ,
                            ISNULL(TOTAL.WorkQty, 0) WorkQty,	-- 생산수량
                            ISNULL(TOTAL.WorkTime, 0) WorkTime,	-- 가동시간_분
                            ISNULL(mo.DownTime, 0) DownTime,	-- 비가동시간_분
                            ISNULL(TOTAL.WorkTime, 0) - ISNULL(mo.DownTime, 0) ProductionTime, -- 생산시간_분(가동 - 비가동)

                            -- 가동율 퍼센트 (소수점 2자리, 불필요한 0 제거, 0이면 '0')
                            -- 가동율 (생산시간 ÷ 가동시간 * 100)
	                        CASE
		                        WHEN ISNULL(TOTAL.WorkTime, 0) = 0 THEN '0'
		                        WHEN ISNULL(TOTAL.WorkTime, 0) - ISNULL(mo.DownTime, 0) < 0 THEN
			                        FORMAT(
				                        ROUND(
					                        ISNULL(TOTAL.WorkTime, 0) * 100.0 /
					                        NULLIF(ISNULL(mo.DownTime, 0), 0), 2
				                        ), '0.##'
			                        )
		                        ELSE
			                        FORMAT(
				                        ROUND(
					                        (ISNULL(TOTAL.WorkTime, 0) - ISNULL(mo.DownTime, 0)) * 100.0 /
					                        NULLIF(ISNULL(TOTAL.WorkTime, 0), 0), 2
				                        ), '0.##'
			                        )
	                        END AS OperatingRate -- 가동율(%)

                        FROM EQ_Machine CM

                        -- 생산수량 + 가동시간
                        LEFT JOIN (
					                        SELECT
						                        OP.CorpId,
						                        OP.MachineCode,
						                        SUM(ISNULL(ORQ.GoodQty, 0) + ISNULL(ORQ.NgQty, 0)) AS WorkQty,
						                        SUM(DATEDIFF(MINUTE, ORQ.StartDate, ORQ.EndDate)) AS WorkTime
					                        FROM PM_OrderResult ORQ
					                        JOIN PM_OrderProcess OP
						                        ON ORQ.CorpId = OP.CorpId
					                           AND ORQ.OrderNo = OP.OrderNo
					                           AND ORQ.OrderSeq = OP.OrderSeq
					                        GROUP BY OP.CorpId, OP.MachineCode
                        ) TOTAL
                            ON  cm.CorpId = TOTAL.CorpId
                           AND cm.MachineCode = TOTAL.MachineCode

                        -- 비가동시간
                        LEFT JOIN (
					                        SELECT
						                        CorpId,
						                        MachineCode,
						                        SUM(DATEDIFF(MINUTE, StartTime, EndTime)) AS DownTime
					                        FROM EQ_Operation
					                        GROUP BY CorpId, MachineCode
                        ) mo
                            ON  cm.CorpId = mo.CorpId
                           AND cm.MachineCode = mo.MachineCode
                    WHERE CM.UseYn = 'Y'

            ";

            if (Database.DbName.ToUpper().Contains("DOOIL"))
            {
                query += $"\n AND CM.MachineCode NOT IN ('MC034')";
            }

            gcMachine.DataSource = db.GetDataTable(query);
            gvMachine.BestFitColumns();
        }

        private void gvMachine_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "OperatingRate")
            {
                // 값 그대로 표시하고 %를 붙임
                e.DisplayText = string.Format("{0:0.#}%", e.Value);
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}