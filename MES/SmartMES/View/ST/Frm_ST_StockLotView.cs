using baseSMT;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Models.CM;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.ST;
using SmartMES.Report;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_StockLotView : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        string query = "";
        string type = "";
        string[] ItemDiv = null;

        public Frm_ST_StockLotView(string FormType)
        {
            InitializeComponent();
            type = FormType;

            // 디자인
            Set_Design();

            // 조회
            Display_Lot();
        }

        #region 디자인

        private void Set_Design()
        {
            // Lup 셋팅
            Set_Lup();

            // 그리드
            Set_GvLot(GvLot);
        }

        private void Set_Lup()
        {
            //창고
            query = $@"SELECT'ALL' 창고코드, '전체' 창고명 
                    UNION ALL 
                    SELECT HouseID 창고코드, Title 창고명 from CM_Warehouse where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y' ";

            if (type == "제품") query += $@"AND WareType = '20'";
            else if (type == "자재") query += $@"AND WareType = '10'";
            else if (type == "공정") query += $@"AND WareType = '30'";

            Lup_House.smtLookUpDataSource = db.GetDataView("구분", query).ToTable();
            Lup_House.smtLookUpDataDisplayMember = "창고명";
            Lup_House.smtLookUpDataValueMember = "창고코드";
            Lup_House.smtLookUpValue = "ALL";

            //품목
            if (type == "제품") ItemDiv = WrGlobal.Business_ItemType;
            else if (type == "자재") ItemDiv = WrGlobal.Material_ItemType;

            Lup_Item.smtLookUpDataSource = code.GetMaterialList(ItemDiv, true).ToTable();
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";
        }

        #region 그리드 디자인
        private void Set_GvLot(GridView gv)
        {
            clsRepository rpo = new clsRepository();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //gv.OptionsView.AllowCellMerge = true;   //같은값인 셀 합치기 허용
            CM_Warehouse wareHouse = new CM_Warehouse();
            DataTable dt_Stock = wareHouse.Select_WareHouse_Lookup(type);
            RepositoryItemSearchLookUpEdit lup_Stock = rpo.Create_RepositoryItemSearchLookUp(dt_Stock, "분류코드", "분류명");

            CM_CateItem item = new CM_CateItem();
            DataTable dt_ItemType = item.SelectClassItemLookUp(WrGlobal.CorpID, "A010");
            RepositoryItemSearchLookUpEdit lup_Type = rpo.Create_RepositoryItemSearchLookUp(dt_ItemType, "분류코드", "분류명");
            //CodeOption에 따른 Column 생성부
            //grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "Lot번호", "LotNo");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemName");
            grid.Create_GvColumn(gv, "품목구분", "ItemType", lup_Type);
            grid.Create_GvColumn(gv, "창고", "Stock_Cd", lup_Stock);
            grid.Create_GvColumn(gv, "재고수량", "StockQty");
            //grid.Create_GvColumn(gv, "실재고량", "Qty");

            //Column 숨김
            //gv.Columns["Adj_Qty"].Visible = false;

            //Column 수정 여부
            //grid.Column_AllowEdit(gv.Columns["Stock_Cd"], true);
            //grid.Column_AllowEdit(gv.Columns["Qty"], true);
            //grid.Column_AllowEdit(gv.Columns["Remark"], true);

            //Column Type, 포맷/정렬
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);

        }
        #endregion 그리드 디자인
        #endregion

        #region 조회
        private void Display_Lot()
        {
            ST_StockD sd = new ST_StockD();
            sd.HouseCode = Lup_House.smtLookUpValue.ConvertString_Null_To_Empty();
            sd.ItemCode = Lup_Item.smtLookUpValue.ToString();
            sd.LotNo = TxtLot.smtTextBoxValue.ConvertString_Null_To_Empty();

            GcLot.DataSource = sd.SelectStock_Lot(type);
            GvLot.BestFitColumns();
        }
        #endregion

        #region 버튼 클릭
        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;

            string name = btn.smtButtonStyle.ConvertString_Null_To_Empty();

            switch (name)
            {
                case "조회":
                    Display_Lot();
                    break;

                case "엑셀변환":
                    Export_Excel();
                    break;

                case "닫기":
                    this.Close();
                    break;

                default:
                    break;
            }
        }

        // 엑셀
        private void Export_Excel()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GvLot.ExportToXls(sfd.FileName);
                Process.Start(sfd.FileName);
            }
        }
        #endregion
    }
}