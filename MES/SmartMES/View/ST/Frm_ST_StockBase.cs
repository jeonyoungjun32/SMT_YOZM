using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

using SmartMES.Models.CM;

using SmartMES.Models;
using SmartMES.Models.CM;
using SmartCommon.Common;
using SmartMES.Models.ST;

using System;
using System.Data;
using System.Windows.Forms;
using baseSMT;
using DevExpress.XtraEditors;
using SmartMES.Common;

namespace SmartMES.View.ST
{
	public partial class Frm_ST_StockBase : DevExpress.XtraEditors.XtraForm
    {

        /*
         * 기초재고 ~> 재고조정 사용, 기초재고 구분 할 내용이 없어 임시로 비고에 '기초' 기재
         */

        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();
        cls_Lookup clsLup = new cls_Lookup();
        Database db = new Database();
        string query = "";
        string type = "";
        //string StockDivCd = ""; //창고구분

        public Frm_ST_StockBase()
        {
            InitializeComponent();
            SetDesign();
        }

        public Frm_ST_StockBase(string FormType)
        {
            InitializeComponent();
            type = FormType;
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 설정
            Set_GridView(Gv_Stock);
            Set_SearchLup();

            //엑셀다운(GV에 보이는 형식 그대로 저장)
            //btn_ExcelDown.DownGridControl = this.Gc_StockAdj;
        }

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

            //
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            //gv.OptionsView.AllowCellMerge = true;   //같은값인 셀 합치기 허용
            CM_Warehouse wareHouse = new CM_Warehouse();
            DataTable dt_Stock = wareHouse.Select_WareHouse_Lookup(type);
            RepositoryItemSearchLookUpEdit lup_Stock = rpo.Create_RepositoryItemSearchLookUp(dt_Stock, "분류코드", "분류명");

            //CM_CateItem item = new CM_CateItem();
            //DataTable dt_ItemType = item.SelectClassItemLookUp(WrGlobal.CorpID, "A010");
            //RepositoryItemSearchLookUpEdit lup_Type = rpo.Create_RepositoryItemSearchLookUp(dt_ItemType, "분류코드", "분류명");
            //CodeOption에 따른 Column 생성부
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "품목구분", "ItemTypeName");
            grid.Create_GvColumn(gv, "입고일자", "InOutDate");
            grid.Create_GvColumn(gv, "창고", "ToHouseCode", lup_Stock);
            grid.Create_GvColumn(gv, "기초재고", "Qty");

            //Column 숨김

            //Column 수정 여부

            //Column Type, 포맷/정렬
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
        }

        #endregion 디자인 아이템 생성 End

        #region 데이터 조회

        private void Search()
        {
            //if (!Check_Search()) return;

            Gc_Stock.MainView = Gv_Stock;

            ST_StockD std = new ST_StockD();
            std.ToHouseCode = Lup_House.smtLookUpValue.ConvertString_Null_To_Empty();
            std.ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_Stock.DataSource = std.SelectStockBase();

            Gv_Stock.BestFitColumns();
        }

        #endregion 데이터 조회 End

        #region 데이터 업데이트
  //      private void Save()
		//{
  //          Save_Gv_Adj();
  //      }

  //      private void Save_Gv_Adj()
  //      {
  //          Gv_Stock.FocusInvalidRow();

  //          GridView gv = Gv_Stock;
  //          if (ChkSave(gv)) return;
  //          DataTable dt = gv.GridControl.DataSource as DataTable;

  //          if (!Check_SaveClick(dt)) return;
  //          if (dt.Rows.Count == 0) return; //데이터 있는지 검사

  //          for (int cur_Idx = 0; cur_Idx < dt.Rows.Count; cur_Idx++)
  //          {
  //              //0. 작업한 Row 아닌 Row 스킵
  //              if (Convert.ToString(dt.Rows[cur_Idx]["Status"]) == "") continue;

  //              //신규
  //              if (Convert.ToString(dt.Rows[cur_Idx]["Status"]) == "신규")
  //              {
  //                  ST_StockM ST_StockM = Create_ST_StockM(dt.Rows[cur_Idx]);
  //                  ST_StockD ST_StockD = Create_ST_StockD(dt.Rows[cur_Idx], ST_StockM);
  //                  if (!ST_StockM.Ins_ST_StockM())
  //                  {
  //                      MessageBox.Show("저장 중 에러가 발생하였습니다.");
  //                      return;
  //                  }

  //                  if (!ST_StockD.Ins_ST_StockD())
  //                  {
  //                      MessageBox.Show("저장 중 에러가 발생하였습니다.");
  //                      return;
  //                  }


  //                  gv.SetRowCellValue(cur_Idx, "Status", "");
  //              }
  //          }

  //          MessageBox.Show("조정이 정상적으로 완료되었습니다.");

  //          //정상 작동진행
  //          Search();
  //      }

  //      private bool Check_SaveClick(DataTable dt)
  //      {
  //          //데이터 불러온 상태인지 검사
  //          if (dt == null) return false;

  //          string status = "";

  //          foreach (DataRow dr in dt.Rows)
  //          {
  //              status = dr["Status"].ConvertString_Null_To_Empty();

  //              if (status == "신규" || status == "수정")
  //              {
  //                  if (Convert.ToString(dr["Stock_Cd"]) == "") { MessageBox.Show("창고를 입력해주세요"); return false; }
  //              }
  //          }

  //          return true;
  //      }

  //      private ST_StockM Create_ST_StockM(DataRow dr)
  //      {
  //          ST_StockM ST_StockM = new ST_StockM();

  //          ST_StockM.InOutNo = "ST" + code.GetAutoNumber("재고", DateTime.Now.Date);
  //          ST_StockM.OutDate = DateTime.Now.Date.ConvertDate_To_Char10();
  //          ST_StockM.FromHouseCode = "";
  //          ST_StockM.ToHouseCode = dr["Stock_Cd"].ConvertString_Null_To_Empty();
  //          ST_StockM.WriteID = WrGlobal.LoginID;

  //          return ST_StockM;
  //      }

  //      private ST_StockD Create_ST_StockD(DataRow dr, ST_StockM ST_StockM)
  //      {
  //          ST_StockD ST_StockD = new ST_StockD();

  //          ST_StockD.InOutNo = ST_StockM.InOutNo;
  //          ST_StockD.Seq = 1;
  //          //ST_StockD.CustCode = "";
  //          ST_StockD.InOutDate = ST_StockM.OutDate;
  //          ST_StockD.Type = "조정";
  //          ST_StockD.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
  //          //ST_StockD.LotNo = "";
  //          ST_StockD.FromHouseCode = ST_StockM.FromHouseCode;
  //          ST_StockD.ToHouseCode = ST_StockM.ToHouseCode;
  //          //ST_StockD.JobNo = "";
  //          //ST_StockD.JobSeq = "";
  //          //ST_StockD.JobSeq2 = "";
  //          //ST_StockD.FaultCode = "";
  //          ST_StockD.Qty = dr["Qty"].ConvertDecimal_Null_To_Zero();
  //          //ST_StockD.OutQty = "";
  //          ST_StockD.CloseYn = "N";
  //          ST_StockD.Remark = dr["Remark"].ConvertString_Null_To_Empty();

  //          return ST_StockD;
  //      }


  //      private bool ChkSave(GridView gv)
  //      {
  //          for (int idx = 0; idx < gv.RowCount; idx++)
  //          {
  //              DataRow dr = gv.GetDataRow(idx);
  //              string status = Convert.ToString(dr["Status"]);

  //              if (status != "")
  //              {
  //                  return false;
  //              }
  //          }
  //          return true;
  //      }

        #endregion 데이터 업데이트 End

        #region Event

        private void Frm_MT_Stock_Load(object sender, EventArgs e)
        {
            Search();
        }

        #endregion Event End

        private void Cmd_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;

			switch (btn.Name)
			{
                case "btn_Search": Search(); break;
                case "cmdDelete": Delete(); break;
                case "cmdClose": this.Close(); break;
                case "cmdExcelUp": ExcelUp(); break;
                case "cmdExcelDown": ExcelDown(); break;
			}
        }

        private void Delete()
		{
            DialogResult dialogResult = XtraMessageBox.Show("기초재고 등록 내역을 삭제하시겠습니까?", "기초재고 삭제", MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            int[] idxs = Gv_Stock.GetSelectedRows();

			foreach (int idx in idxs)
			{
                DataRow dr = Gv_Stock.GetDataRow(idx);

                ST_StockD std = new ST_StockD();
                std.InOutNo = dr["InOutNo"].ConvertString_Null_To_Empty();
                std.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
                std.Del_StockMD();
			}

            XtraMessageBox.Show(cls_MessageBox.Del_Comp);
        }

        private void ExcelUp()
		{
            Frm_ST_ExcelUpLoad_Base f = new Frm_ST_ExcelUpLoad_Base();
			f.FormClosed += F_FormClosed;
            f.Show();
		}

		private void F_FormClosed(object sender, FormClosedEventArgs e)
		{
            Search();
		}

		private void ExcelDown()
        {
            GridView gv = Gc_Stock.MainView as GridView;
            if (gv == null) return;

            clsFunction func = new clsFunction();
            func.ExportExcel(gv);
        }
	}
}