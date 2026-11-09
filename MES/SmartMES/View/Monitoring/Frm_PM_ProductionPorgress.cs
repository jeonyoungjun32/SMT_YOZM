using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using SmartCommon.Common;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Monitoring
{

    public partial class Frm_PM_ProductionPorgress : DevExpress.XtraEditors.XtraForm
    {
        Image blueCircle = Properties.Resources.BlueCircle;
        Image redCircle = Properties.Resources.RedCircle;
        Image greenCircle = Properties.Resources.GreenCircle;
        Timer timer = new Timer();

        clsGrid grid = new clsGrid();
        Database db = new Database();
        string query = "";

        public Frm_PM_ProductionPorgress()
        {
            InitializeComponent();
        }

        private void Frm_PM_ProductionPorgress_Load(object sender, EventArgs e)
        {
            Set_GridView(Gv_List);
            Search();

            timer.Interval = 5000;      // 5초마다
            timer.Tick += new EventHandler(newMachineStatus);

            TmTime.Start();
            timer.Start();
        }

        private void newMachineStatus(object sender, EventArgs e)
        {
            SetMachineStatus();
        }

        private void SetMachineStatus()
        {
            Search();
        }

        private void Create_GvColumn(GridView gv, string caption, string fieldName, RepositoryItem repository = null)
        {
            GridColumn col = new GridColumn();
            col.Caption = caption;          //표기명
            col.Name = fieldName;           //Column명
            col.FieldName = fieldName;      //연동 데이터 Column명
            col.ColumnEdit = repository;    //Repository Item(ComboBox, Lookup 등)
            col.Visible = true;             //화면에 보이기
            col.OptionsColumn.AllowEdit = false;   //유저가 수정 허용여부

            gv.Columns.Add(col);            //GridView에 추가
        }

        private void Set_GridView(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬

            Create_GvColumn(gv, "상태", "Status");
            Create_GvColumn(gv, "지시번호", "OrderNo");
            Create_GvColumn(gv, "순번", "OrderSeq");

			Create_GvColumn(gv, "품목코드", "ItemCode");
			Create_GvColumn(gv, "품명", "ItemName");
			Create_GvColumn(gv, "설비", "MachineName");

			Create_GvColumn(gv, "지시수량", "OrderQty");
			Create_GvColumn(gv, "생산수량", "WorkQty");
			Create_GvColumn(gv, "불량수량", "NgQty");

			//Create_GvColumn(gv, "생산시작일자", "Start_Date");
			//Create_GvColumn(gv, "생산종료일자", "End_Date");

			//Create_GvColumn(gv, "지시수량", "MachinePlanQty");
			//Create_GvColumn(gv, "생산수량", "MachineWorkQty");
			//Create_GvColumn(gv, "양품수량", "MachineGoodQty");
			//Create_GvColumn(gv, "불량수량", "MachineBadQty");
			//Create_GvColumn(gv, "잔여수량", "BalanceQty");

            //
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //
            gv.Columns["OrderNo"].MaxWidth = 240;
            gv.Columns["MachineName"].MaxWidth = 220;
            gv.Columns["OrderQty"].MaxWidth = 140;
            gv.Columns["WorkQty"].MaxWidth = 140;
            gv.Columns["NgQty"].MaxWidth = 120;
            gv.Columns["OrderSeq"].MaxWidth = 80;
            gv.Columns["Status"].MaxWidth = 80;

            grid.SetPOPGrid(gv);
            gv.CustomDrawCell += Gv_CustomDrawCell;
        }

        private void Gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            string value = e.CellValue == null ? "" : e.CellValue.ToString();

            if (e.Column.FieldName == "Status")
            {
                switch (value)
                {
                    case "대기":
                        //대기
                        e.Appearance.ForeColor = Color.Gold;
                        e.DisplayText = "●";
                        break;

                    case "작업중":
                        //진행
                        e.Appearance.ForeColor = Color.Green;
                        e.DisplayText = "●";
                        break;
                    case "작업완료":
                        //종료
                        e.Appearance.ForeColor = Color.Red;
                        e.DisplayText = "●";
                        break;
                    //default:
                    //    //e.Cache.DrawImage(redCircle, ((e.Bounds.X + e.Bounds.Right) / 2) - 10, e.Bounds.Y + 4, 20, 20);
                    //    //e.Cache.DrawImage(null, ((e.Bounds.X + e.Bounds.Right) / 2) - 10, e.Bounds.Y + 4, 20, 20);
                    //    e.Cache.Clear();
                    //    break;
                }
            }
        }

        private void Search()
        {
            //테이블 없어 주석처리
            // query = $@"SELECT ms.OrderNo
            //                    ,wo.OrderQty	-- 지시수량
            //                    ,wo.WorkQty		-- 생산수량
            //                    ,wo.GoodQty		-- 양품수량
            //                    ,wo.NgQty		-- 불량수량
            //                    -- 설비가동중 수량
            //                    ,ms.PlanQty MachinePlanQty		
            //                    ,ms.WorkQty MachineWorkQty
            //                    ,ms.GoodQty MachineGoodQty
            //                    ,ms.BadQty MachineBadQty
            //                    ,ms.BalanceQty
            //                    ,ms.BalanceQty
            //                    ,ms.Status
            //                    FROM MachineStatus ms
            //                    LEFT JOIN PM_ProductPlan wo ON ms.CorpId = wo.CorpId AND ms.OrderNo = wo.OrderNo
            //                    WHERE ms.CorpId = '{WrGlobal.CorpID}'
            //                ";
            //Gc_List.DataSource = db.GetDataView("모니터링", query);

            string startDate = DateTime.Now.AddDays(-7).Date.ToString("d");
            string endDate = DateTime.Now.Date.ToString("d");

            query = $@"
                        SELECT OP.OrderNo
                                , OP.OrderSeq
		                        , WO.WorkDate
		                        , WO.EndDate
		                        , OP.ProcessCode
                                , OP.MachineCode
                                , mc.MachineName
		                        , op.ItemCode AS ItemCode
		                        , CM.ItemName
		                        , ISNULL(OP.OrderQty, 0) AS OrderQty
		                        , ISNULL(OP.WorkQty, 0) AS WorkQty
		                        , ISNULL(OP.GoodQty, 0) AS GoodQty
		                        , ISNULL(OP.NgQty, 0) AS NgQty
		                        , OP.Status
                                , OP.StartDate
                                , OP.EndDate
                        FROM PM_OrderProcess OP
                        LEFT JOIN PM_ProductPlan WO ON OP.CorpId = WO.CorpId AND OP.OrderNo = WO.OrderNo
                        LEFT JOIN CM_Item  CM ON op.CorpId = CM.CorpId AND op.ItemCode = cm.ItemCode
                        LEFT JOIN EQ_Machine mc ON op.CorpId = mc.CorpId AND op.MachineCode = mc.MachineCode

                        WHERE OP.CorpID = '{WrGlobal.CorpID}'
                        AND WO.WorkDate BETWEEN '{startDate}' AND '{endDate}'
                    ";

            Gc_List.DataSource = db.GetDataTable(query);
        }

        private void frmTmpMachine_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
        }

		private void TmTime_Tick(object sender, EventArgs e)
		{
            LblTime.Text = DateTime.Now.ToString();
        }
	}
}