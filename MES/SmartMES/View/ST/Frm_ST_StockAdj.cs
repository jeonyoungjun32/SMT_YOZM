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


namespace SmartMES.View.ST
{
	public partial class Frm_ST_StockAdj : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsCode code = new clsCode();
        cls_Lookup clsLup = new cls_Lookup();
        Database db = new Database();
        string query = "";
        string type = "";
        //string StockDivCd = ""; //창고구분

        public Frm_ST_StockAdj(string FormType)
        {
            InitializeComponent();
            type = FormType;
            SetDesign();

            if (!Database.DbName.ToUpper().Contains("DOOIL"))
            {
                cmdExcel.Visible = false;
            }
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
            //기간
            Dt_Date.smtStartDateTimeValue = DateTime.Now;

            //창고구분
            query = $@"SELECT'ALL' 분류코드, '전체' 분류명 
                    UNION ALL 
                    select distinct ClassItemCode 분류코드, Title 분류명 FROM CM_CateItem Where Class = 'A020' AND useYn = 'Y' AND (CorpId = '1000' or CorpId = 'Z000')";

            Lup_StockDiv.smtLookUpDataSource = db.GetDataView("구분", query).ToTable();
            Lup_StockDiv.smtLookUpDataDisplayMember = "분류명";
            Lup_StockDiv.smtLookUpDataValueMember = "분류코드";
            Lup_StockDiv.smtLookUpValue = "ALL";

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
            DataTable dt_Stock = wareHouse.Select_WareHouse_Lookup(type);
            RepositoryItemSearchLookUpEdit lup_Stock = rpo.Create_RepositoryItemSearchLookUp(dt_Stock, "분류코드", "분류명");

            CM_CateItem item = new CM_CateItem();
            DataTable dt_ItemType = item.SelectClassItemLookUp(WrGlobal.CorpID, "A010");
            RepositoryItemSearchLookUpEdit lup_Type = rpo.Create_RepositoryItemSearchLookUp(dt_ItemType, "분류코드", "분류명");
            //CodeOption에 따른 Column 생성부
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품목명", "ItemName");
            grid.Create_GvColumn(gv, "품목구분", "ItemType", lup_Type);
            grid.Create_GvColumn(gv, "창고", "Stock_Cd", lup_Stock);
            grid.Create_GvColumn(gv, "현재고수량", "StockQty");
            grid.Create_GvColumn(gv, "조정수량", "Qty");
            grid.Create_GvColumn(gv, "조정사유", "Remark");

            //Column 숨김

            //Column 수정 여부
            grid.Column_AllowEdit(gv.Columns["Stock_Cd"], true);
            grid.Column_AllowEdit(gv.Columns["Qty"], true);
            grid.Column_AllowEdit(gv.Columns["Remark"], true);

            //Column Type, 포맷/정렬
            grid.SetCol_Type(gv.Columns["StockQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);

            Gv_Stock.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.Gv_ComputeStock_CellValueChanged);
        }

        #endregion 디자인 아이템 생성 End

        #region 데이터 조회

        private void Search()
        {
            if (!Check_Search()) return;

            Gc_Stock.MainView = Gv_Stock;

            string wareType = Lup_StockDiv.smtLookUpValue.ConvertString_Null_To_Empty();
            string warehouse = Lup_House.smtLookUpValue.ConvertString_Null_To_Empty();

            CM_Item CM_Item = new CM_Item();
            CM_Item.ItemCode = Lup_Item.smtLookUpValue.ToString();

            Gc_Stock.DataSource = CM_Item.SelectCM_ItemAll_RND(type, wareType, warehouse);

            Gv_Stock.BestFitColumns();
        }

        #endregion 데이터 조회 End

        #region 데이터 업데이트
        private void Save()
		{
            Save_Gv_Adj();
        }

        private void Save_Gv_Adj()
        {
            Gv_Stock.FocusInvalidRow();

            GridView gv = Gv_Stock;
            if (ChkSave(gv)) return;
            DataTable dt = gv.GridControl.DataSource as DataTable;

            if (!Check_SaveClick(dt)) return;
            if (dt.Rows.Count == 0) return; //데이터 있는지 검사

            for (int cur_Idx = 0; cur_Idx < dt.Rows.Count; cur_Idx++)
            {
                //0. 작업한 Row 아닌 Row 스킵
                if (Convert.ToString(dt.Rows[cur_Idx]["Status"]) == "") continue;

                //신규
                if (Convert.ToString(dt.Rows[cur_Idx]["Status"]) == "신규")
                {
                    ST_StockM ST_StockM = Create_ST_StockM(dt.Rows[cur_Idx]);
                    ST_StockD ST_StockD = Create_ST_StockD(dt.Rows[cur_Idx], ST_StockM);
                    if (!ST_StockM.Ins_ST_StockM())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return;
                    }

                    if (!ST_StockD.Ins_ST_StockD())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return;
                    }


                    gv.SetRowCellValue(cur_Idx, "Status", "");
                }
            }

            MessageBox.Show("조정이 정상적으로 완료되었습니다.");

            //정상 작동진행
            Search();
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //데이터 불러온 상태인지 검사
            if (dt == null) return false;

            string status = "";

            foreach (DataRow dr in dt.Rows)
            {
                status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규" || status == "수정")
                {
                    if (Convert.ToString(dr["Stock_Cd"]) == "") { MessageBox.Show("창고를 입력해주세요"); return false; }
                }
            }

            return true;
        }

        private ST_StockM Create_ST_StockM(DataRow dr)
        {
            ST_StockM ST_StockM = new ST_StockM();

            ST_StockM.InOutNo = "ST" + code.GetAutoNumber("재고", DateTime.Now.Date);
            ST_StockM.OutDate = DateTime.Now.Date.ConvertDate_To_Char10();
            ST_StockM.FromHouseCode = "";
            ST_StockM.ToHouseCode = dr["Stock_Cd"].ConvertString_Null_To_Empty();
            ST_StockM.WriteID = WrGlobal.LoginID;

            return ST_StockM;
        }

        private ST_StockD Create_ST_StockD(DataRow dr, ST_StockM ST_StockM)
        {
            ST_StockD ST_StockD = new ST_StockD();

            ST_StockD.InOutNo = ST_StockM.InOutNo;
            ST_StockD.Seq = 1;
            //ST_StockD.CustCode = "";
            ST_StockD.InOutDate = ST_StockM.OutDate;
            ST_StockD.Type = "조정";
            ST_StockD.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            //ST_StockD.LotNo = "";
            ST_StockD.FromHouseCode = ST_StockM.FromHouseCode;
            ST_StockD.ToHouseCode = ST_StockM.ToHouseCode;
            //ST_StockD.JobNo = "";
            //ST_StockD.JobSeq = "";
            //ST_StockD.JobSeq2 = "";
            //ST_StockD.FaultCode = "";
            ST_StockD.Qty = dr["Qty"].ConvertDecimal_Null_To_Zero();
            //ST_StockD.OutQty = "";
            ST_StockD.CloseYn = "N";
            ST_StockD.Remark = dr["Remark"].ConvertString_Null_To_Empty();

            return ST_StockD;
        }


        private bool ChkSave(GridView gv)
        {
            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status != "")
                {
                    return false;
                }
            }
            return true;
        }

        #endregion 데이터 업데이트 End

        #region Event

        private void Frm_MT_Stock_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void Gv_ComputeStock_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;

            if (fieldName == "Real_Stock_Qty")
            {
                float Adj_Qty;

                Adj_Qty = gv.GetFocusedRowCellValue("Stock_Qty").ConvertFloat_Null_To_Zero() - gv.GetFocusedRowCellValue("Real_Stock_Qty").ConvertFloat_Null_To_Zero();
                gv.SetFocusedRowCellValue("Adj_Qty", Adj_Qty);
                gv.BestFitColumns();
            }

            //상태값 수정
            gv.SetRowCellValue(e.RowHandle, gv.Columns[0].FieldName, "신규");
        }

        private void Gv_StockAdj_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;

            if (fieldName == "Real_Stock_Qty")
            {
                float Adj_Qty;

                Adj_Qty = gv.GetFocusedRowCellValue("Stock_Qty").ConvertFloat_Null_To_Zero() - gv.GetFocusedRowCellValue("Real_Stock_Qty").ConvertFloat_Null_To_Zero();
                gv.SetFocusedRowCellValue("Adj_Qty", Adj_Qty);
                gv.BestFitColumns();
            }

            //상태값 수정
            grid.Change_Status(gv, e.RowHandle);
        }

        private bool Check_Search()
        {
            try
            {
                //조회시 확인 사항
                //true : 정상진행, false : 진행 Error

                DataTable dt = Gc_Stock.DataSource as DataTable;

                if (dt == null) return true;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["Status"].ConvertString_Null_To_Empty() == "") continue;
                    return MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Gv_CellMerge(object sender, CellMergeEventArgs e)
        {
            //셀 병합시 예외처리
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;

            //거래처
            if (fieldNm == "Cust_Cd" )
            {
                string[] compareList = { fieldNm, "Stock_Cd" };  //비교할 FieldName 목록
                bool mergeYn = true;

                //데이터리스트 비교
                for (int idx = 0; idx < compareList.Length; idx++)
                {
                    string upData = Convert.ToString(gv.GetRowCellValue(e.RowHandle1, compareList[idx]));   //상단데이터
                    string downData = Convert.ToString(gv.GetRowCellValue(e.RowHandle2, compareList[idx])); //하단데이터

                    //데이터 비교
                    if (upData != downData) { mergeYn = false; break; } //다르면 Merge 안함처리
                }

                e.Merge = mergeYn;  //최종 적용 여부
                e.Handled = true;   //뭔지는 모르겠는데 안해주면 적용 안됨
            }
            //품목분류
            else if (fieldNm == "ItemClass_Cd" || fieldNm == "ItemClass_Nm")
            {
                string[] compareList = { fieldNm, "Stock_Cd", "Cust_Cd" };  //비교할 FieldName 목록
                bool mergeYn = true;

                //데이터리스트 비교
                for (int idx = 0; idx < compareList.Length; idx++)
                {
                    string upData = Convert.ToString(gv.GetRowCellValue(e.RowHandle1, compareList[idx]));   //상단데이터
                    string downData = Convert.ToString(gv.GetRowCellValue(e.RowHandle2, compareList[idx])); //하단데이터

                    //데이터 비교
                    if (upData != downData) { mergeYn = false; break; } //다르면 Merge 안함처리
                }

                e.Merge = mergeYn;  //최종 적용 여부
                e.Handled = true;   //뭔지는 모르겠는데 안해주면 적용 안됨
            }
            //품목정보
            else if (fieldNm == "ItemCode" || fieldNm == "Item_Nm" || fieldNm == "Alias" || fieldNm == "Spec" || fieldNm == "Unit"
                || fieldNm == "Grade" || fieldNm == "Color" || fieldNm == "CT")
            {
                string[] compareList = { fieldNm, "Stock_Cd", "Cust_Cd", "ItemCode" };  //비교할 FieldName 목록
                bool mergeYn = true;

                //데이터리스트 비교
                for (int idx = 0; idx < compareList.Length; idx++)
                {
                    string upData = Convert.ToString(gv.GetRowCellValue(e.RowHandle1, compareList[idx]));   //상단데이터
                    string downData = Convert.ToString(gv.GetRowCellValue(e.RowHandle2, compareList[idx])); //하단데이터

                    //데이터비교
                    if (upData != downData) { mergeYn = false; break; } //다르면 Merge 안함처리
                }

                e.Merge = mergeYn;  //최종 적용 여부
                e.Handled = true;   //뭔지는 모르겠는데 안해주면 적용 안됨
            }
        }

        private void Event_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 신규/수정 저장하지 않고 창 닫을때 확인 메세지
            //e.Cancel - true : 종료방지, false : 정상종료
            if (grid.Check_Gv_FormClosing(Gv_Stock, "Status"))
            {
                e.Cancel = true;
            }
        }


        #endregion Event End

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            GridView gv = Gc_Stock.MainView as GridView;
            if (gv == null) return;

            clsFunction func = new clsFunction();
            func.ExportExcel(gv);
        }
    }
}