using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartMES.Common;
using SmartCommon.Common;
using System.Diagnostics;
using SmartMES.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Models;
using SmartMES.Models.ST;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_StockInOutLIst : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();

        string status;

        string[] ItemDiv = null;
        string[] WareDiv = null;

        public Frm_ST_StockInOutLIst()
        {
            InitializeComponent();
        }

        public Frm_ST_StockInOutLIst(string status)
        {
            InitializeComponent();
            this.status = status;
        }

        private void frmStockDaily2_Load(object sender, EventArgs e)
        {
            Init();
            SetGridStock(gvInOut);

            DisplayData();
        }

        private void Init()
		{
            DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));
            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            //창고
            if (status == "공정") WareDiv = new string[] { "30" };

            Lup_House.smtLookUpDataSource = code.GetWarehouseList_Dt(true, WareDiv);
            Lup_House.smtLookUpDataDisplayMember = "창고명";
            Lup_House.smtLookUpDataValueMember = "창고코드";
            Lup_House.smtLookUpValue = "ALL";

            //품목
            if (status == "제품") ItemDiv = WrGlobal.Product_ItemType;
            else if (status == "자재") ItemDiv = WrGlobal.Material_ItemType;
            else if (status == "공정") ItemDiv = new string[] { "20", "30" };

            Lup_Item.smtLookUpDataSource = code.GetMaterialList(true, ItemDiv);
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";
        }

        private void SetGridStock(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            grid.Create_GvColumn(gv, "작업번호", "JobNo");
            grid.Create_GvColumn(gv, "작업순서", "JobSeq");
            grid.Create_GvColumn(gv, "재고유형", "StockType");
            grid.Create_GvColumn(gv, "입출고유형", "InoutType");
            grid.Create_GvColumn(gv, "입출고일자", "InOutDate");
            grid.Create_GvColumn(gv, "창고코드", "WarehouseCode");
            grid.Create_GvColumn(gv, "창고명", "Title");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "수량", "Qty");

            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.SetCol_Type(gv.Columns["InOutDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);

            gv.OptionsBehavior.ReadOnly = true;
            gv.Columns["WarehouseCode"].Visible = false;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            string itemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            string warehouseCode = Lup_House.smtLookUpValue.ConvertString_Null_To_Empty();

            ST_StockD std = new ST_StockD();
            std.ItemCode = itemCode;

            gcInOut.DataSource = std.SelectStockInOutList(startDate, endDate, warehouseCode, ItemDiv);
            gvInOut.BestFitColumns();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            //엑셀변환 하기
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                    gvInOut.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}