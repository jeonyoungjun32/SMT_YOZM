using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.Lookup
{
    public partial class Frm_SelectPM_ProductPlan : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SetData(DataRowView dr);
        public event SetData onSetData;

        private string procCodes;
        private string orderNo;
        private bool AutoSelectd = false;

        public string type { get; set; }
        private string userID;

        private string query = "";

        clsGrid grid = new clsGrid();
        Database db = new Database();

        public Frm_SelectPM_ProductPlan()
        {
            InitializeComponent();
        }


        //public Frm_SelectPM_ProductPlan(string userID)
        //{
        //    InitializeComponent();

        //    this.userID = userID;
        //}

        public Frm_SelectPM_ProductPlan(string procCodes, string orderNo = "")
        {
            InitializeComponent();
            this.procCodes = procCodes;
            this.orderNo = orderNo;

            if (this.orderNo != "")
            {
                AutoSelectd = true;
            }
        }

        private void Frm_SelectPM_ProductPlan_Load(object sender, EventArgs e)
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
            grid.Create_GvColumn(gv, "전공정구분", "PreProcessTypeName");
            grid.Create_GvColumn(gv, "공정", "ProcessCode");
            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "촤종공정", "FinalYn");
            grid.Create_GvColumn(gv, "공정순서", "RoutingSeq");
            grid.Create_GvColumn(gv, "라인", "LineCode");
            grid.Create_GvColumn(gv, "라인", "LineName");

            grid.Create_GvColumn(gv, "제품코드", "ItemCode");
            grid.Create_GvColumn(gv, "제품명", "ProductName");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "작업지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "작업지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "지시일자", "WorkDate");
            grid.Create_GvColumn(gv, "완료요청일", "EndDate");

            grid.Create_GvColumn(gv, "작업지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "부적합수량", "NgQty");

            grid.Create_GvColumn(gv, "지시비고", "RemarkOrder");


            gv.Columns["PreProcessTypeName"].Visible = false;
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["RoutingSeq"].Visible = false;
            gv.Columns["LineCode"].Visible = false;
            gv.Columns["LineName"].Visible = false;
            gv.Columns["OrderSeq"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;
            gv.Columns["ProductName"].Visible = false;

            // 수량 가운데 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["PreProcessTypeName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ItemCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["FinalYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["OrderQty"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["WorkQty"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["GoodQty"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["NgQty"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomColumnDisplayText += Gv_CustomColumnDisplayText;

        }

        private void DisplayData()
        {
			string query = $"exec POP_SelectPM_ProductPlan '{WrGlobal.CorpID}','{procCodes}','{WrGlobal.LoginID}','{orderNo}'";
            GcPM_OrderProcess.DataSource = db.GetDataTable(query);
            GvPM_OrderProcess.BestFitColumns();

            if (AutoSelectd)
            {
                if (GvPM_OrderProcess.RowCount == 0)
                {
                    this.Close();
                }
                else if (GvPM_OrderProcess.RowCount == 1)
                {
                    onSetData((DataRowView)GvPM_OrderProcess.GetRow(0));

                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (GvPM_OrderProcess.RowCount == 0) return;

            onSetData((DataRowView)GvPM_OrderProcess.GetFocusedRow());

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void GvPM_OrderProcess_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if(e.RowHandle < 0) return;
            if (e.Column.Name != "Status") return;

            GridView gv = sender as GridView;

            if (e.CellValue.ToString() == "작업대기")
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
            else if (e.CellValue.ToString() == "작업중")
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void Gv_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "OrderQty" || e.Column.FieldName == "WorkQty" || e.Column.FieldName == "GoodQty" || e.Column.FieldName == "NgQty")
            {
                if (e.Value == null || e.Value == DBNull.Value)
                {
                    e.DisplayText = "0";
                    return;
                }

                if (e.Value.ToString().Length >= 3)
                {
                    e.DisplayText = string.Format("{0:N2}", e.Value).TrimEnd('0').TrimEnd('.');
                }
            }
        }

        private void smtSearchButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}