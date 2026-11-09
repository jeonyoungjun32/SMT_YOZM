using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SmartPOP.Common;
using SmartERP.Models;
using SmartPOP.Models;
using SmartPOP.CM;
using SmartPOP.View.Lookup;

namespace SmartPOP.View.ST
{
    public partial class Frm_ST_StockMoveIn : DevExpress.XtraEditors.XtraForm
    {
        cls_Grid grid = new cls_Grid();
        cls_Lookup clsLup = new cls_Lookup();
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";
        string type = "";
        string StockDivCd = ""; //창고구분  (자재,제품,공정,외주)

        string[] mtType = { "40", "50", "60" }; // 40 : 원단, 50 : 마니라, 60 : 부자재

        public Frm_ST_StockMoveIn()
        {
            this.StockDivCd = "S0001,S0002,S0003,S0008";

            InitializeComponent();
            
            SetDesign();
        }
        public Frm_ST_StockMoveIn(string _StockDivCd)
        {

            if (_StockDivCd == "자재")
            {
                this.StockDivCd = "S0001";
            }
            else if (_StockDivCd == "제품")
            {
                this.StockDivCd = "S0002";
            }
            else if (_StockDivCd == "공정")
            {
                this.StockDivCd = "S0003";
            }
            else if (_StockDivCd == "외주")
            {
                this.StockDivCd = "S0008";
            }
            else 
            {
                this.StockDivCd = "S0001,S0002,S0003,S0008";
            }

            InitializeComponent();

            if (_StockDivCd == "외주")
            {
                smtSearchButton3.Visible = false;
            }


            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 설정
            Set_GridView(gvStock);
            Set_SearchLup();
        }

        private void Set_SearchLup()
        {
            LupWareType.smtLookUpDataSource = code.smtLookUp("창고정보", new string[] { "", StockDivCd }, "전체");
            //LupMaterialType.smtLookUpDataSource = code.smtLookUp("품목구분", "", "전체");
            LupMaterialType.smtLookUpDataSource = code.Get_MaterialType(mtType, true);
            LupMaterialType.smtLookUpDataDisplayMember = "분류명";
            LupMaterialType.smtLookUpDataValueMember = "분류코드";
            LupMaterialType.smtLookUpValue = "ALL";

            repositoryItemLookUpEdit1.DataSource = code.smtLookUp("품목구분", "", "");
            repositoryItemLookUpEdit1.ValueMember = "코드";
            repositoryItemLookUpEdit1.DisplayMember = "코드명";
        }

        private void Set_GridView(GridView gv)
        {
            cls_Repository rpo = new cls_Repository();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            clsCode _code = new clsCode();
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(_code.GetMaterialList(), "품목코드", "품명");

            //gv.OptionsView.AllowCellMerge = true;   //같은값인 셀 합치기 허용
            CM_Warehouse wareHouse = new CM_Warehouse();
            DataTable dt_Stock = wareHouse.Select_WareHouse_Lookup(type);
            RepositoryItemSearchLookUpEdit lup_Stock = rpo.Create_RepositoryItemSearchLookUp(dt_Stock, "분류코드", "분류명");

            //CmCateItem item = new CmCateItem();
            //DataTable dt_materialType = item.SelectClassItemLoopUp(WrGlobal.CorpID, "A010");
            //RepositoryItemSearchLookUpEdit lup_Type = rpo.Create_RepositoryItemSearchLookUp(dt_materialType, "분류코드", "분류명");
            //CodeOption에 따른 Column 생성부
            grid.Create_GvColumn(gv, "창고코드", "WarehouseCode");
            grid.Create_GvColumn(gv, "창고", "Warehouse");
            grid.Create_GvColumn(gv, "품목구분", "MaterialType", repositoryItemLookUpEdit1);
            grid.Create_GvColumn(gv, "품목코드", "ProductCode");
            grid.Create_GvColumn(gv, "품명", "ProductCode", lup_Item);
            grid.Create_GvColumn(gv, "장", "Lenght");
            grid.Create_GvColumn(gv, "폭", "Width");
            grid.Create_GvColumn(gv, "절수", "QtyNum");
            grid.Create_GvColumn(gv, "재단규격", "CutSize");
            grid.Create_GvColumn(gv, "현재고량", "StockQty");

            grid.Create_GvColumn(gv, "소분순번", "DetailSeq");

            //Column 수정 여부
            //grid.Column_AllowEdit(gv.Columns["Stock_Cd"], true);
            //grid.Column_AllowEdit(gv.Columns["Qty"], true);
            //grid.Column_AllowEdit(gv.Columns["Remark"], true);

            gv.Columns["WarehouseCode"].Visible = false;
            gv.Columns["DetailSeq"].Visible = false;
            gv.Columns["MaterialType"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //Column Type, 포맷/정렬
            grid.SetCol_Type(gv.Columns["StockQty"], cls_Grid.TypeList.TFloat);

            //
            grid.SetPOPGrid(gv);
        }

        #endregion 디자인 아이템 생성 End

        #region 데이터 조회

        private void Search()
        {
            string materialType = LupMaterialType.smtLookUpValue;
            string WareType = LupWareType.smtLookUpValue;

            if (materialType == "ALL") materialType = "";
            if (WareType == "ALL") WareType = StockDivCd;

            query = $@"
                       SELECT 
                        st.WarehouseCode
                        , st.Warehouse
                        , st.MaterialType
                        , st.ProductCode
                        , st.DetailSeq
                        , cd.Lenght
                        , cd.Width
                        , cd.QtyNum
                        , cd.CutSize
                        , st.StockQty
                        FROM FN_StockListView ('{materialType}', '{WareType}', '', '') st
                        LEFT JOIN CmMaterialDetail cd ON st.ProductCode = cd.MaterialCode AND st.DetailSeq = cd.seq
                        LEFT JOIN CmMaterial CM ON ST.ProductCode = CM.MaterialCode
                        WHERE CM.MaterialType IN ('{string.Join("','", mtType)}')
                        ORDER BY st.Warehouse,st.MaterialType,st.ProductCode
                    ";
      
            gcStock.DataSource = db.GetDataTable(query);
            gvStock.BestFitColumns();
        }

        #endregion 데이터 조회 End

        #region Event

        private void Frm_MT_Stock_Load(object sender, EventArgs e)
        {
            Search();
        }
        private void Event_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 신규/수정 저장하지 않고 창 닫을때 확인 메세지
            //e.Cancel - true : 종료방지, false : 정상종료
            if (grid.Check_Gv_FormClosing(gvStock, "Status"))
            {
                e.Cancel = true;
            }
        }


        #endregion Event End

        private void smtSearchButton2_Click(object sender, EventArgs e)
        {
            //조정
            if (gvStock.FocusedRowHandle < 0) return;

            string WarehouseCode = (gvStock.GetFocusedRowCellValue("WarehouseCode") ?? "").ToString();
            string ProductCode = (gvStock.GetFocusedRowCellValue("ProductCode") ?? "").ToString();
            string ProductName = (gvStock.GetFocusedRowCellDisplayText("ProductCode") ?? "").ToString();
            int Length = gvStock.GetFocusedRowCellValue("Lenght").ConvertInt_Null_To_Zero();
            int Width = gvStock.GetFocusedRowCellValue("Width").ConvertInt_Null_To_Zero();
            int QtyNum = gvStock.GetFocusedRowCellValue("QtyNum").ConvertInt_Null_To_Zero();
            string CutSize = gvStock.GetFocusedRowCellValue("CutSize").ConvertString_Null_To_Empty();
            int StockQty = int.Parse((gvStock.GetFocusedRowCellValue("StockQty") ?? "0").ToString());

            int DetailSeq = gvStock.GetFocusedRowCellValue("DetailSeq").ConvertInt_Null_To_Zero();

            //frmLookUpStockAdjustment frm = new frmLookUpStockAdjustment(WarehouseCode, ProductCode, ProductName, Length, Width, QtyNum, CutSize, StockQty, DetailSeq);
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    Search();
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void smtSearchButton3_Click(object sender, EventArgs e)
        {
            //이동
            if (gvStock.FocusedRowHandle < 0) return;

            string WarehouseCode = (gvStock.GetFocusedRowCellValue("WarehouseCode") ?? "").ToString();
            string ProductCode = (gvStock.GetFocusedRowCellValue("ProductCode") ?? "").ToString();
            string ProductName = (gvStock.GetFocusedRowCellDisplayText("ProductCode") ?? "").ToString();
            int Length = gvStock.GetFocusedRowCellValue("Lenght").ConvertInt_Null_To_Zero();
            int Width = gvStock.GetFocusedRowCellValue("Width").ConvertInt_Null_To_Zero();
            int QtyNum = gvStock.GetFocusedRowCellValue("QtyNum").ConvertInt_Null_To_Zero();
            string CutSize = gvStock.GetFocusedRowCellValue("CutSize").ConvertString_Null_To_Empty();
            int StockQty = int.Parse((gvStock.GetFocusedRowCellValue("StockQty") ?? "0").ToString());

            int DetailSeq = gvStock.GetFocusedRowCellValue("DetailSeq").ConvertInt_Null_To_Zero();

            Frm_LookUpStockMove frm = new Frm_LookUpStockMove(WarehouseCode, ProductCode, ProductName, Length, Width, QtyNum, CutSize, StockQty, DetailSeq, "In");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }

        private void smtSearchButton1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gvStock.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }
        }
    }
}