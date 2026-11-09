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
using DevExpress.XtraGrid.Views.BandedGrid;
using SmartMES.Models;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_StockInOutTotal : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        Database db = new Database();
        clsCode code = new clsCode();
        string query = "";
        string wareType = "10,20,30";  //10 : 원자재, 20 : 제품, 30 : 공정, 40 : 반품
        string[] ItemType = { };
       string status;

        public Frm_ST_StockInOutTotal()
        {
            InitializeComponent();
        }

        public Frm_ST_StockInOutTotal(string status)
        {
            InitializeComponent();
            this.status = status;
        }

        private void frmStockDaily2_Load(object sender, EventArgs e)
        {
            Set_Lup();
            Set_BandGrid(gvInOut);

            DisplayData();
        }

        #region 디자인

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
                ItemType = WrGlobal.Material_ItemType;
            }
            else if (status == "제품")
            {
                // 창고설정
                wareType = "20,40";
                //품목설정
                ItemType = WrGlobal.Business_ItemType;
            }
            else if (status == "공정")
            {
                // 창고설정
                wareType = "30";
                //품목설정
                //ItemType = WrGlobal.Product_ItemType;
                ItemType = new string[] { "20", "30"};
            }
            else
            {
                // 창고설정
                wareType = "10,20,30,40";
                //품목설정
                ItemType = WrGlobal.Business_ItemType;
            }

            //창고
            smtHouseCode.smtLookUpDataSource = code.smtLookUp("창고정보", wareType, "ALL");

            //품목
            ssCM_Item.smtLookUpDataSource = code.GetMaterialList(ItemType, true).ToTable();
            ssCM_Item.smtLookUpDataDisplayMember = "품명";
            ssCM_Item.smtLookUpDataValueMember = "품목코드";
            ssCM_Item.smtLookUpValue = "ALL";
        }

        private void Set_BandGrid(BandedGridView gv)
        {
            // Band 생성
            grid.Create_Band(gv, "Common", "");
            grid.Create_Band(gv, "In", "입고");
            grid.Create_Band(gv, "Out", "출고");
            //grid.Create_Band(gv, "Total", "");

            // 컬럼 생성
            // 공통부분
            grid.Create_BandedColumn(gv, "Warehouse", "창고구분", "WarehouseName", "Common");
            grid.Create_BandedColumn(gv, "ItemCode", "품목코드", "ItemCode", "Common");
            grid.Create_BandedColumn(gv, "ItemName", "품명", "ItemName", "Common");
            if (status == "자재")
            {
                grid.Create_BandedColumn(gv, "Size", "규격", "Size", "Common");
                grid.Create_BandedColumn(gv, "ItemTypeName", "품목구분", "ItemTypeName", "Common");
                grid.Create_BandedColumn(gv, "ItemClass1Name", "대분류", "ItemClass1Name", "Common");
                grid.Create_BandedColumn(gv, "ItemClass2Name", "중분류", "ItemClass2Name", "Common");
                grid.Create_BandedColumn(gv, "UnitName", "단위", "UnitName", "Common");
                grid.Create_BandedColumn(gv, "MovementKind", "구분", "MovementKind", "Common");
                grid.Create_BandedColumn(gv, "StockQty", "현재고", "StockQty", "Common");
                grid.Create_BandedColumn(gv, "NetQty", "계 (입고-출고)", "NetQty", "Common");
                grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
                grid.SetCol_Type(gv.Columns["NetQty"], clsGrid.TypeList.TFloat);
            }
            //grid.Create_BandedColumn(gv, "Size", "규격", "Size", "Common");
            //grid.Create_BandedColumn(gv, "UnitCode", "단위", "UnitCode", "Common");
            //grid.Create_BandedColumn(gv, "PreQty", "전월재고", "PreQty", "Common");

            // 입고부분
            grid.Create_BandedColumn(gv, "InQty", "매입", "InQty", "In");
            grid.Create_BandedColumn(gv, "MakeInQty", "생산", "MakeInQty", "In");
            grid.Create_BandedColumn(gv, "MoveInQty", "이동", "MoveInQty", "In");
            grid.Create_BandedColumn(gv, "ReturnQty", "반품", "ReturnQty", "In");
            //grid.Create_BandedColumn(gv, "StockInQty", "실사", "StockInQty", "In");
            grid.Create_BandedColumn(gv, "TotalInQty", "입고소계", "TotalInQty", "In");

            // 출고부분
            grid.Create_BandedColumn(gv, "OutQty", "매출", "OutQty", "Out");
            grid.Create_BandedColumn(gv, "MakeOutQty", "생산", "MakeOutQty", "Out");
            grid.Create_BandedColumn(gv, "MoveOutQty", "이동", "MoveOutQty", "Out");
            //grid.Create_BandedColumn(gv, "StockOutQty", "실사", "StockOutQty", "Out");
            grid.Create_BandedColumn(gv, "TotalOutQty", "출고소계", "TotalOutQty", "Out");

            //현재고부분
            //grid.Create_BandedColumn(gv, "StockQty", "현재고", "StockQty", "Total");

            //column Header 가운데 정렬
            foreach (BandedGridColumn col in gv.Columns)
            {
                col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            //Column Type, 포맷/정렬
            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["MakeInQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["MoveInQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["ReturnQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalInQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["MakeOutQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["MoveOutQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalOutQty"], clsGrid.TypeList.TInt);

            gv.OptionsView.ColumnAutoWidth = false;
            gv.OptionsBehavior.Editable = false; // 데이터 수정 불가 처리

            //컬럼 visible
            gv.Columns["ReturnQty"].Visible = false;
            if (Database.DbName.ToUpper().Contains("ALLTHEN"))
            {
                if(status == "제품") { gv.Columns["ReturnQty"].Visible = true; }
            }
            if (Database.DbName.ToUpper().Contains("TPG"))
            {
                if (status == "제품") { gv.Columns["ReturnQty"].Visible = true; }
            }
        }

        #endregion

        #region 조회

        // 제품/자재 입출고집계 조회
        private void DisplayData()
        {
            this.Validate();

            string ItemCode = ssCM_Item.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : ssCM_Item.smtLookUpValue.ConvertString_Null_To_Empty();
            string warehouseCode = smtHouseCode.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : smtHouseCode.smtLookUpValue.ConvertString_Null_To_Empty();

            if (ItemType != null && ItemType.Length > 0)
            {
                // 자재는 조회 기간 전체를 품목과 창고 단위로 집계합니다.
                string movementDate = status == "자재" ? "MAX(A.InOutDate)" : "A.InOutDate";
                string groupByDate = status == "자재" ? "" : ", A.InOutDate";
                query = $@"
                         SELECT
                                A.ItemCode,
                                B.ItemName,
                                B.Size,
                                dbo.FN_Get_CateItem_Title(B.CorpId, 'A010', B.ItemType) AS ItemTypeName,
                                dbo.FN_Get_CateItem_Title(B.CorpId, 'A01210', B.ItemClass1) AS ItemClass1Name,
                                dbo.FN_Get_CateItem_Title(B.CorpId, 'A01220', B.ItemClass2) AS ItemClass2Name,
                                COALESCE(NULLIF(dbo.FN_Get_CateItem_Title(B.CorpId, 'A011', B.UnitCode), N''), B.UnitCode) AS UnitName,
                                CASE WHEN A.HasIn = 1 AND A.HasOut = 1 THEN N'입고·출고'
                                     WHEN A.HasIn = 1 THEN N'입고' WHEN A.HasOut = 1 THEN N'출고' ELSE N'' END AS MovementKind,
                                COALESCE(S.StockQty, 0) AS StockQty,
                                COALESCE(A.NetQty, 0) AS NetQty,
                                A.WarehouseCode,
	                            C.Title as WarehouseName,
	                            A.InOutDate,
                                COALESCE(A.창고입고재고_입고, 0) AS InQty,
                                COALESCE(A.창고입고재고_생산, 0) AS MakeInQty,
                                COALESCE(A.창고입고재고_이동, 0) AS MoveInQty,
	                            COALESCE(A.창고입고재고_반품, 0) AS ReturnQty,
                                COALESCE(A.창고출고재고_입고, 0) AS OutQty,
                                COALESCE(A.창고출고재고_생산, 0) AS MakeOutQty,
                                COALESCE(A.창고출고재고_이동, 0) AS MoveOutQty,
                                COALESCE(A.창고입고재고_입고, 0) + COALESCE(A.창고입고재고_생산, 0) + COALESCE(A.창고입고재고_이동, 0) + COALESCE(A.창고입고재고_반품, 0) AS TotalInQty,
                                COALESCE(A.창고출고재고_입고, 0) + COALESCE(A.창고출고재고_생산, 0) + COALESCE(A.창고출고재고_이동, 0) AS TotalOutQty
                            FROM (
                                SELECT 
                                    A.CorpId,
                                    A.ItemCode,
                                    A.WarehouseCode,
		                            {movementDate} AS InOutDate,
                                    SUM(CASE WHEN A.StockType = '창고입고재고' AND A.InoutType = '입고' THEN A.qty ELSE 0 END) AS 창고입고재고_입고,
                                    SUM(CASE WHEN A.StockType = '창고입고재고' AND A.InoutType = '반품' THEN A.qty ELSE 0 END) AS 창고입고재고_반품, 
                                    SUM(CASE WHEN A.StockType = '창고입고재고' AND A.InoutType = '생산' THEN A.qty ELSE 0 END) AS 창고입고재고_생산,
                                    SUM(CASE WHEN A.StockType = '창고입고재고' AND A.InoutType = '이동' THEN A.qty ELSE 0 END) AS 창고입고재고_이동,
                                    SUM(CASE WHEN A.StockType = '창고출고재고' AND A.InoutType = '출고' THEN A.qty ELSE 0 END) AS 창고출고재고_입고,
                                    SUM(CASE WHEN A.StockType = '창고출고재고' AND A.InoutType = '생산' THEN A.qty ELSE 0 END) AS 창고출고재고_생산,
                                    SUM(CASE WHEN A.StockType = '창고출고재고' AND A.InoutType = '이동' THEN A.qty ELSE 0 END) AS 창고출고재고_이동,
                                    MAX(CASE WHEN A.StockType = N'창고입고재고' THEN 1 ELSE 0 END) AS HasIn,
                                    MAX(CASE WHEN A.StockType = N'창고출고재고' THEN 1 ELSE 0 END) AS HasOut,
                                    SUM(CASE WHEN A.StockType = N'창고입고재고' THEN A.Qty
                                             WHEN A.StockType = N'창고출고재고' THEN -A.Qty END) AS NetQty
                                FROM 
                                    StockInOutView A
                                WHERE 
                                    A.CorpId = '{WrGlobal.CorpID}'
                                    AND A.InOutDate >= CONVERT(date, '{Dt_Date.smtStartDateTimeValue:yyyy-MM-dd}', 23)
                                    AND A.InOutDate < DATEADD(day, 1, CONVERT(date, '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', 23))
                                    AND A.InoutType IN ('입고', '생산', '이동', '출고','반품')
                                GROUP BY 
                                    A.CorpId,
                                    A.ItemCode,
                                    A.WarehouseCode{groupByDate}
                            ) AS A
                            LEFT JOIN CM_Item B ON A.CorpId = B.CorpId AND A.ItemCode = B.ItemCode
                            INNER JOIN CM_Warehouse C ON A.CorpId = C.CorpId AND A.WarehouseCode = C.HouseID
                            OUTER APPLY (
                                SELECT SUM(CASE WHEN T.StockType = N'창고입고재고' THEN T.Qty
                                                WHEN T.StockType = N'창고출고재고' THEN -T.Qty END) AS StockQty
                                FROM dbo.StockInOutView T
                                WHERE T.CorpId = A.CorpId AND T.ItemCode = A.ItemCode
                                  AND T.WarehouseCode = A.WarehouseCode
                                  AND T.InOutDate < DATEADD(day, 1, CONVERT(date, '{Dt_Date.smtEndDateTimeValue:yyyy-MM-dd}', 23))
                            ) S
                            WHERE B.CorpId = '{WrGlobal.CorpID}' 
                                  AND B.ItemType IN ('{string.Join("','", ItemType)}') 
                                ";

                if (ItemCode != "")
                {
                    query += $"AND A.ItemCode = '{ItemCode}' ";
                }
                if (warehouseCode != "")
                {
                    query += $"AND A.WarehouseCode = '{warehouseCode}' ";
                }

                gcInOut.DataSource = db.GetDataView("입출고현황", query);
                gvInOut.BestFitColumns();
                gvInOut.OptionsBehavior.ReadOnly = true;
            }
        }
        #endregion

        #region Event

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

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
