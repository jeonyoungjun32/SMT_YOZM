using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.PM
{
    public partial class Frm_PM_OrderProcess_List : DevExpress.XtraEditors.XtraForm
    {
        public string type { get; set; }

        public delegate void SetData(DataRowView dr);
        public event SetData onSetData;

        private string userID;

        private string query = "";
        
        cls_Grid grid = new cls_Grid();
        Database db = new Database();
        

        public Frm_PM_OrderProcess_List()
        {
            InitializeComponent();
        }

        public Frm_PM_OrderProcess_List(string userID)
        {
            InitializeComponent();

            this.userID = userID;
        }

        private void frmSelectPM_ProductPlan_Load(object sender, EventArgs e)
        {
            SetGrid();

            DisplayData();
        }

        private void SetGrid()
        {
            GridView gv = GvPM_OrderProcess;

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "공정", "ProcessCode");
            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "라인", "LineCode");
            grid.Create_GvColumn(gv, "라인", "LineName");
            grid.Create_GvColumn(gv, "작업지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "작업지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "제품코드", "ProductCode");
            grid.Create_GvColumn(gv, "제품명", "ProductName");
            grid.Create_GvColumn(gv, "품번", "MaterialCode");
            grid.Create_GvColumn(gv, "품명", "MaterialName");
            grid.Create_GvColumn(gv, "작업지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "부적합수량", "NgQty");

            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["LineCode"].Visible = false;
        }

        private void DisplayData()
        {
            query = $@"SELECT  DISTINCT -- CM_LineProcessWorker 주석 해제 시 DISTINCT 삭제
                                OP.ProcessCode
                                , OP.LineCode
                                , CP.ProcessName
		                        , CL.LineName
		                        , OP.OrderNo
		                        , OP.OrderSeq
		                        , WO.MaterialCode ProductCode
		                        , CM2.MaterialName ProductName
		                        , OP.HalfMaterialCode MaterialCode
		                        , CM.MaterialName MaterialName
		                        , OP.OrderQty
		                        , OP.WorkQty
		                        , OP.GoodQty
		                        , OP.NgQty
		                        , OP.[Status]
                        FROM PM_OrderProcess OP
                        INNER JOIN PM_ProductPlan WO ON WO.OrderNo = OP.OrderNo
                        --INNER JOIN CM_LineProcessWorker CAL ON OP.ProcessCode = CAL.ProcessCode AND OP.LineCode = CAL.LineCode
                        INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode AND CP.WorkType = '{this.type}' -- 10:사내, 20:외주
                        INNER JOIN CM_Line CL ON OP.LineCode = CL.LineCode
                        INNER JOIN CM_Item CM ON OP.HalfMaterialCode = CM.MaterialCode 
                        INNER JOIN CM_Item CM2 ON WO.MaterialCode = CM2.MaterialCode
                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
                        --AND CAL.EmpNo = '{WrGlobal.LoginID}'
                            -- 10일간 내역 보여줌 
                        AND WO.WorkDate BETWEEN DATEADD(DAY, -10, GETDATE()) AND GETDATE()
                        AND OP.Status <> '작업완료'
                        ORDER BY OP.OrderNO , OP.OrderSeq
                        ";

            GcPM_OrderProcess.DataSource = db.GetDataTable(query);
            GvPM_OrderProcess.BestFitColumns();
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (GvPM_OrderProcess.RowCount == 0) return;

            onSetData((DataRowView)GvPM_OrderProcess.GetFocusedRow());

            this.Close();
        }

        private void GvPM_OrderProcess_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.Name != "Status") return;

            GridView gv = sender as GridView;

            if (e.CellValue.ToString() == "작업대기")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Black;
            }
            else if (e.CellValue.ToString() == "작업중")
            {
                e.Appearance.BackColor = Color.Blue;
                e.Appearance.ForeColor = Color.White;
            }
            else
            {
                e.Appearance.BackColor = Color.Lime;
                e.Appearance.ForeColor = Color.White;
            }
        }
    }
}