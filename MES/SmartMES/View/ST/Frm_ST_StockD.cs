using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models.CM;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartCommon.Common;
using SmartMES.Models.ST;

using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace SmartMES.View.ST
{
    public partial class frmST_StockD : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        clsGrid grid = new clsGrid();
        string type = "";
        string query = "";
        
        public frmST_StockD()
        {
            InitializeComponent();
        }

        public frmST_StockD(string FormType)
        {
            InitializeComponent();
            type = FormType;
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            Set_GridView(gvList);
            Set_SearchLup();

            DisplayData();
        }

        #region 디자인 아이템 생성

        private void Set_SearchLup()
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
            query = $@"SELECT'ALL' 품목코드, '전체' 품명 
                    UNION ALL 
                    select distinct ItemCode 품목코드, ItemName 품명 from CM_Item Where CorpId = '1000' ";

            if (type == "제품") query += $@"AND ItemType IN ('10', '20', '30')";
            else if (type == "자재") query += $@"AND ItemType IN ('40', '50', '60', '70')";

            Lup_Item.smtLookUpDataSource = db.GetDataView("구분", query).ToTable();
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";

        }

        private void Set_GridView(GridView gv)
        {
            clsRepository rpo = new clsRepository();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //gv.OptionsView.AllowCellMerge = true;   //같은값인 셀 합치기 허용
            CM_Warehouse wareHouse = new CM_Warehouse();
            DataTable dt_Stock = wareHouse.Select_WareHouse_Lookup("공정");
            RepositoryItemSearchLookUpEdit lup_House = rpo.Create_RepositoryItemSearchLookUp(dt_Stock, "분류코드", "분류명");

            CM_CateItem item = new CM_CateItem();
            DataTable dt_ItemType = item.SelectClassItemLookUp(WrGlobal.CorpID, "A010");
            RepositoryItemSearchLookUpEdit lup_Type = rpo.Create_RepositoryItemSearchLookUp(dt_ItemType, "분류코드", "분류명");

            //CodeOption에 따른 Column 생성부
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "조정일자", "InOutDate");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemName");
            grid.Create_GvColumn(gv, "품목구분", "ItemType", lup_Type);
            grid.Create_GvColumn(gv, "창고", "ToHouseCode", lup_House);
            grid.Create_GvColumn(gv, "LotNo", "LotNo");
            grid.Create_GvColumn(gv, "전산재고량", "StockQty");
            grid.Create_GvColumn(gv, "조정량", "Qty");
            grid.Create_GvColumn(gv, "조정사유", "Remark");

            //Column 숨김
            gv.Columns["StockQty"].Visible = false;

            //Column 수정 여부
            grid.Column_AllowEdit(gv.Columns["Stock_Cd"], true);
            grid.Column_AllowEdit(gv.Columns["Qty"], true);
            grid.Column_AllowEdit(gv.Columns["Remark"], true);

            //Column Type, 포맷/정렬
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InOutDate"], clsGrid.TypeList.TDate);

            gvList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.Gv_ComputeStock_CellValueChanged);
        }

        private void Gv_ComputeStock_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;

            if (fieldName == "Qty")
            {
                float Adj_Qty;

                Adj_Qty = gv.GetFocusedRowCellValue("StockQty").ConvertFloat_Null_To_Zero() - gv.GetFocusedRowCellValue("Qty").ConvertFloat_Null_To_Zero();
                gv.SetFocusedRowCellValue("Adj_Qty", -Adj_Qty);
            }

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
        }

        #endregion

        private void DisplayData()
        {
            ST_StockD sd = new ST_StockD();
            sd.ItemCode = Lup_Item.smtLookUpValue.ToString();
            sd.HouseCode = Lup_House.smtLookUpValue.ConvertString_Null_To_Empty();
            gcList.DataSource = sd.Select_StockAdjList(type);

            gvList.BestFitColumns();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Del(object sender, EventArgs e)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("Status", Convert.ToString(gvList.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(gcList, dic, "Del");
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            gvList.PostEditor();
            gvList.UpdateCurrentRow();
            dt = gcList.DataSource as DataTable;

            //저장 시작
            //1. 데이터 불러온 상태인지 검사
            //if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            int maxWorkSeq = 1;

            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (dt.Rows[curHandle]["OrderQty"].ConvertInt_Null_To_Zero() == 0) continue;

                    if (!Save_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }
            DisplayData();

            XtraMessageBox.Show(cls_MessageBox.Save_Comp, cls_MessageBox.SaveCaption);
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            ST_StockD sd = new ST_StockD();

            try
            {
                sd.CorpId = WrGlobal.CorpID;
                sd.InOutNo = Convert.ToString(dr["InOutNo"]);      
                sd.Seq = dr["Seq"].ConvertInt_Null_To_Zero();      

                if (div != 'D')
                {
                    //Delete제외 공통
                    sd.Qty = dr["Qty"].ConvertInt_Null_To_Zero();
                    sd.Remark = dr["Remark"].ConvertString_Null_To_Empty();
                }
            }
            catch
            {
                MessageBox.Show("값 형태변환 에러");
                return false;
            }

            //2. DB 적용
            if (div == 'I')
            {
                //wo.OrderNo = "WO" + code.GetAutoNumber("작업지시", Convert.ToDateTime(wo.WorkDate));
                ////Insert
                //if (!wo.Insert_PM_ProductPlan())
                //{
                //    //저장 중 에러 발생시
                //    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                //    return false;
                //}
                //if (!wo.Insert_PM_SdRoute())
                //{
                //    cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                //    return false;
                //}
            }
            else if (div == 'U')
            {
                //Update
                if (!sd.Upd_StockAdj())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'D')
            {
                //Delete
                if (!sd.Del_StockMD())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }

            //상태값 초기화
            dr["Status"] = "";

            return true;
        }
    }
}