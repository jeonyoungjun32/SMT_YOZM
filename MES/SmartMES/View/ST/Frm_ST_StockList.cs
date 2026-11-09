using SmartMES.Common;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using SmartCommon.Common;
using SmartMES.Models;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Models.CM;
using DevExpress.XtraEditors.Repository;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_StockList : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        Database db = new Database();
        clsCode code = new clsCode();

        string query = "";
        string wareType = "10,20,30";  //10 : 원자재, 20 : 제품, 30 : 공정, 40 : 반품
        string ItemType = "10,20,30,40,50";   //10 : 상품  20 : 제품  30 : 반제품  40 : 원자재  50 : 부자재  60 : 샘플/개발  70 : 배출자재  80 : 배출자재_황동  90 : 배출자재_알루미늄

        string status;

        public Frm_ST_StockList()
        {
            InitializeComponent();
        }

        public Frm_ST_StockList(string status)
        {
            InitializeComponent();

            this.status = status;
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            Set_Lup();
            DisplayData();

            bool stockViewInitialized = true;
            Activated += (source, args) =>
            {
                if (stockViewInitialized)
                    DisplayData();
            };
        }

        private void Set_Lup()
        {
            // status에 따른 타이틀 변경
            //LblTitle.Text = status + "현재고현황";

            if (status == "자재")
            {
                wareType = "10";
                ItemType = "40,50,70,80,90";
                SetGridStock(gvList);
            }
            else if (status == "제품")
            {
                wareType = "20,40";
                ItemType = "10,20";
                SetGridStockList(gvList);
            }
            else if (status == "공정")
            {
                wareType = "30";
                ItemType = "20,30";
                SetGridStockList(gvList);
            }
            else
            {
                wareType = "10,20,30,40";
                ItemType = "10,20,30,40,50,60,70,80,90";
                SetGridStockList(gvList);
            }

            //창고
            Lup_House.smtLookUpDataSource = code.smtLookUp("창고정보", wareType, "ALL");

            //품목
            Lup_Item.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";
        }

        private void SetGridStockList(GridView gv)
        {
            clsRepository rpo = new clsRepository();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            CM_CateItem item = new CM_CateItem();
            DataTable dt_ItemType = item.SelectClassItemLookUp(WrGlobal.CorpID, "A010");
            RepositoryItemSearchLookUpEdit lup_Type = rpo.Create_RepositoryItemSearchLookUp(dt_ItemType, "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");

            grid.Create_GvColumn(gv, "창고코드", "WareHouseCode");
            grid.Create_GvColumn(gv, "창고명", "Warehouse");
            grid.Create_GvColumn(gv, "품목구분", "ItemType", lup_Type);
            grid.Create_GvColumn(gv, "제품코드", "ItemCode");
            grid.Create_GvColumn(gv, "제품명", "ItemName");
            grid.Create_GvColumn(gv, "단위", "UnitCode", lup_UnitCode);
            grid.Create_GvColumn(gv, "재고수량", "StockQty");
            grid.Create_GvColumn(gv, "안전재고", "SaftyQty");

            //grid.SetCol_Type(gv.Columns["InOutDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);

            gv.OptionsBehavior.ReadOnly = true;
            gv.Columns["SaftyQty"].Visible = false;
        }

        private void SetGridStock(GridView gv)
        {
            clsRepository rpo = new clsRepository();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            CM_CateItem item = new CM_CateItem();
            DataTable dt_ItemType = item.SelectClassItemLookUp(WrGlobal.CorpID, "A010");
            RepositoryItemSearchLookUpEdit lup_Type = rpo.Create_RepositoryItemSearchLookUp(dt_ItemType, "분류코드", "분류명");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");

            grid.Create_GvColumn(gv, "창고코드", "WareHouseCode");
            grid.Create_GvColumn(gv, "창고명", "Warehouse");
            grid.Create_GvColumn(gv, "자재구분", "ItemType", lup_Type);
            grid.Create_GvColumn(gv, "자재코드", "ItemCode");
            grid.Create_GvColumn(gv, "자재명", "ItemName");
            grid.Create_GvColumn(gv, "단위", "UnitCode", lup_UnitCode);
            grid.Create_GvColumn(gv, "재고수량", "StockQty");
            grid.Create_GvColumn(gv, "안전재고", "SaftyQty");

            //grid.SetCol_Type(gv.Columns["InOutDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);

            gv.OptionsBehavior.ReadOnly = true;
            gv.Columns["SaftyQty"].Visible = false;
        }

        private void DisplayData()
        {
            this.Validate();

            string ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            string warehouseCode = Lup_House.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_House.smtLookUpValue.ConvertString_Null_To_Empty();

            query = $@"SELECT SLT.*
                        FROM StockListView SLT
                        LEFT JOIN CM_WareHouse C ON SLT.WareHouseCode = C.HouseID
                        --WHERE ItemType IN ('{string.Join("','", ItemType.Split(','))}')
                        WHERE 1=1
                        AND C.WareType = '{wareType}'
                        ";
            
            if(warehouseCode != "")
			{
                query += $"AND WarehouseCode = '{warehouseCode}' ";
			}

            if (ItemCode != "")
            {
                query += $"AND ItemCode = '{ItemCode}' ";
            }

            gcList.DataSource = db.GetDataView("현재고현황", query);
            gvList.BestFitColumns();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvList.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }
    }
}
