using System;
using System.Windows.Forms;
using SmartMES.Common;
using SmartCommon.Common;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Models;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_StockInOutTotalQty : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        Database db = new Database();
        clsCode code = new clsCode();

        string query = "";
        string wareType = "10,20,30";  //10 : 원자재, 20 : 제품, 30 : 공정, 40 : 반품
        string ItemType = "10,20,30,40,50";   //10 : 상품,  20 : 제품,  30 : 반제품,  40 : 원자재,  50 : 부자재,  60 : 샘플/개발,  70 : 배출자재,  80 : 배출자재_황동,  90 : 배출자재_알루미늄

        string status;

        public Frm_ST_StockInOutTotalQty()
        {
            InitializeComponent();
        }

        public Frm_ST_StockInOutTotalQty(string status)
        {
            InitializeComponent();
            this.status = status;
        }

        private void frmStockDaily2_Load(object sender, EventArgs e)
        {
            Set_Lup();
            SetGridStock(gvInOut);

            DisplayData();
        }

        private void Set_Lup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = DateTime.Now.Date;
            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            if (status == "자재")
            {
                // 창고설정
                wareType = "10";
                //품목설정
                ItemType = "40,50,70,80,90";

            }
            else if (status == "제품")
            {
                // 창고설정
                wareType = "20,40";
                //품목설정
                ItemType = "10,20";
            }
            else if (status == "공정")
            {
                // 창고설정
                wareType = "30";
                //품목설정
                ItemType = "20,30";
            }
            else
            {
                // 창고설정
                wareType = "10,20,30,40";
                //품목설정
                ItemType = "10,20,30,40,50,60,70,80,90";
            }

            //창고
            Lup_House.smtLookUpDataSource = code.smtLookUp("창고정보", wareType, "");

            //품목
            Lup_Item.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";
        }

        private void SetGridStock(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsBehavior.ReadOnly = true;

            grid.Create_GvColumn(gv, "창고", "WarehouseName");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "이월수량", "PrevQty");
            grid.Create_GvColumn(gv, "입고수량", "InQty");
            grid.Create_GvColumn(gv, "출고수량", "OutQty");
            grid.Create_GvColumn(gv, "재고수량", "StockQty");

            grid.SetCol_Type(gv.Columns["PrevQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
        }

        private void DisplayData()
        {
            string ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            string warehouseCode = Lup_House.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_House.smtLookUpValue.ConvertString_Null_To_Empty();

            if (!string.IsNullOrEmpty(ItemType))
            {
                query = $@"  DECLARE @StartDate DATE = '{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd 00:00:00}';
                             DECLARE @EndDate DATE = '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd 23:59:59}';

                            SELECT
                                  A.WarehouseCode
                                , CW.Title AS WarehouseName
                                , A.ItemCode
                                , CM.ItemName

                                -- 이월수량
                                , ISNULL(SUM(CASE WHEN A.InOutDate < @StartDate
                                        THEN CASE
                                                WHEN A.StockType = '창고입고재고' THEN A.Qty
                                                WHEN A.StockType = '창고출고재고' THEN -A.Qty
                                             END
                                        ELSE 0 END), 0) AS PrevQty

                                -- 입고수량
                                , ISNULL(SUM(CASE WHEN A.InOutDate BETWEEN @StartDate AND @EndDate AND A.StockType = '창고입고재고'
                            				THEN A.Qty
                                        ELSE 0 END), 0) AS InQty

                                -- 출고수량
                                , ISNULL(SUM(CASE WHEN A.InOutDate BETWEEN @StartDate AND @EndDate AND A.StockType = '창고출고재고'
                            				THEN A.Qty
                                        ELSE 0 END), 0) AS OutQty

                                -- 재고수량
                                , ISNULL(SUM(CASE WHEN A.InOutDate < @StartDate
                                        THEN CASE
                                                WHEN A.StockType = '창고입고재고' THEN A.Qty
                                                WHEN A.StockType = '창고출고재고' THEN -A.Qty
                                             END
                                        ELSE 0 END), 0)
                                  +
                                  ISNULL(SUM(CASE WHEN A.InOutDate BETWEEN @StartDate AND @EndDate AND A.StockType = '창고입고재고'
                            				THEN A.Qty
                                        ELSE 0 END), 0)
                                  -
                                  ISNULL(SUM(CASE WHEN A.InOutDate BETWEEN @StartDate AND @EndDate AND A.StockType = '창고출고재고'
                            				THEN A.Qty
                                        ELSE 0 END), 0) AS StockQty

                            FROM StockInOutView A
                                LEFT JOIN CM_Item CM ON A.ItemCode = cm.ItemCode
                                LEFT JOIN CM_Warehouse CW ON A.WarehouseCode = CW.HouseID

                            WHERE CM.CorpId = '{WrGlobal.CorpID}'
                               AND A.InOutDate <= @EndDate";

                if (ItemCode != "")
                {
                    query += $"\n AND A.ItemCode = '{ItemCode}' ";
                }
                if (warehouseCode != "")
                {
                    query += $"\n AND A.WarehouseCode = '{warehouseCode}' ";
                }

                query += $"\n GROUP BY A.WarehouseCode, CW.Title, A.ItemCode, CM.ItemName";
                query += $"\n ORDER BY A.WarehouseCode, A.ItemCode";

                gcInOut.DataSource = db.GetDataView("입출고집계", query);
                gvInOut.BestFitColumns();
            }
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
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
            }//end fnction
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}