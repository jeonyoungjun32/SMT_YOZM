using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_ItemPrice_Cust : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        Database db = new Database();
        string query = "";
        private string Selected_Cust_Cd = "";
        private string Selected_ItemCode = "";
        private int? currentRowHandle = null;

        public Frm_CM_ItemPrice_Cust()
        {
            InitializeComponent();
            gvMaterialPrice.ShowingEditor += gvMaterialPrice_ShowingEditor;
        }

        private void Frm_CM_ItemPrice_Cust_Load(object sender, EventArgs e)
        {
            SetDesign();
            DisplayCust();
        }

        private void SetDesign()
        {
            SetLup();

            //GridView 데이터 입력
            Set_GvCust(GvCustomer);
            Set_GvPrice(gvMaterialPrice);
            Set_Gv_History(Gv_History);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(gvMaterialPrice, "Status"))
                {
                    e.Cancel = true;
                }
            });

            //초기에 화면에서 안보이도록 설정
            layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void SetLup()
		{
            DataTable dt_priceGroup = code.GetCM_CateItem(WrGlobal.CorpID, "S210", true);

            LupPriceGroup.smtLookUpDataSource = dt_priceGroup;
            LupPriceGroup.smtLookUpDataDisplayMember = "명칭";
            LupPriceGroup.smtLookUpDataValueMember = "코드";
            LupPriceGroup.smtLookUpValue = "ALL";
        }

        #region Main Grid

        private void Set_GvCust(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = true;     //Column 많으면 스크롤 생기도록 바꾸기

            //검색창
            gv.OptionsFind.AlwaysVisible = true;
            gv.OptionsView.ShowGroupPanel = false;

            //Column 생성
            grid.Create_GvColumn(gv, "거래처구분", "CustTypeName");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");
            grid.Create_GvColumn(gv, "등록여부", "PriceYn");

			//
			gv.Columns["PriceYn"].Visible = false;

			//이벤트 설정
			gv.RowCellStyle += Gv_RowCellStyle; ;              //등록여부에따른 배경색 지정
			gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;   //Row선택 변경시 그행 조회
        }

		private void Gv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //단가 등록 내역 있는지 색 표기
            if (e.Column.FieldName == "PriceYn")   //
            {
                GridView gv = sender as GridView;

                string val = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "A"));

                if (val == "N")
                {
                    e.Appearance.BackColor = Color.Gray;
				}
				else
				{
                    e.Appearance.BackColor = Color.Transparent;
                }
            }
        }

        private void Gv_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.FocusedRowHandle);
            if (dr == null) return;

            DisplayPrice();
        }
        #endregion Main Grid End

        #region Sub Grid
        private void Set_GvPrice(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsBehavior.AutoPopulateColumns = false; //DataTable 입력시 Column 생성 방지
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            //
            RepositoryItem lupPriceType = rpo.Create_RepositoryItem(code.Select_CM_CateItem_ByKey(), "그룹코드", "그룹명");
            RepositoryItem lupCust = rpo.Create_RepositoryItem(code.Select_Customer_ByKey(), "거래처코드", "거래처명");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(null), "품목코드", "품명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "단가구분", "PriceType", lupPriceType);
            grid.Create_GvColumn(gv, "거래처", "CustCode", lupCust);
            grid.Create_GvColumn(gv, "품목", "ItemCode", lup_Item);
            grid.Create_GvColumn(gv, "단가", "Price", rpo.Create_rpoTxt_Numeric());
            grid.Create_GvColumn(gv, "적용일자", "ApplyDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "종료일자", "EndDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "거래처품번", "CustAlias");
            grid.Create_GvColumn(gv, "이력", "History");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

			//Repository 설정
			grid.Column_AllowEdit(gv.Columns["CustCode"]);
			grid.Column_AllowEdit(gv.Columns["PriceType"]);
			grid.Column_AllowEdit(gv.Columns["ItemCode"]);
			grid.Column_AllowEdit(gv.Columns["Price"]);
			grid.Column_AllowEdit(gv.Columns["ApplyDate"]);
			grid.Column_AllowEdit(gv.Columns["EndDate"]);
			grid.Column_AllowEdit(gv.Columns["CustAlias"]);
			grid.Column_AllowEdit(gv.Columns["Remark"]);
			grid.Column_AllowEdit(gv.Columns["History"]);

            //
            gv.Columns["History"].Visible = false; //이력테이블 없어져서 숨김처리

            //
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["ApplyDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["EndDate"], clsGrid.TypeList.TDate);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["PriceType"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //단가 히스토리
            RepositoryItemButtonEdit btn_History = new RepositoryItemButtonEdit();
            btn_History.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            btn_History.Click += Event_BtnHistory_Click;                //이벤트 지정
            gv.Columns["History"].ColumnEdit = btn_History;

            grid.RowNumber_Indicator(gv);       //행순번 표기
        }

        private void Event_BtnHistory_Click(object sender, EventArgs e)
        {
            // 현재 포커스 된 행 가져오기
            var view = gvMaterialPrice;
            int clickedRowHandle = view.FocusedRowHandle;

            // 같은 행의 버튼을 한 번 더 클릭한 경우 → 하단 그리드 숨김
            if (currentRowHandle == clickedRowHandle)
            {
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                currentRowHandle = null;

                return;
            }

            // 다른 행의 버튼 클릭 → 하단 그리드 갱신
            currentRowHandle = clickedRowHandle;

            // 선택된 행의 데이터 가져오기 (예시)
            var selectedRow = view.GetRow(clickedRowHandle);
            if (selectedRow != null)
            {
                Search_ItemPrice_History();
            }
            else
            {
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        #endregion Sub Grid End

        #region History
        private void Set_Gv_History(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            //Repository 생성 or 이벤트 입력
            //단가
            RepositoryItemTextEdit txt_Price = new RepositoryItemTextEdit();
            txt_Price.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            RepositoryItem lupPriceType = rpo.Create_RepositoryItem(code.Select_CM_CateItem_ByKey(), "그룹코드", "그룹명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "순번", "Idx");
            grid.Create_GvColumn(gv, "단가구분", "PriceType", lupPriceType);
            grid.Create_GvColumn(gv, "단가", "Price", txt_Price);
            grid.Create_GvColumn(gv, "단가 적용일", "ApplyDate");
            grid.Create_GvColumn(gv, "변경사유", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["Price"]);         //단가
            grid.Column_AllowEdit(gv.Columns["Remark"]);           //변경사유

            gv.Columns["Idx"].Visible = false;

           //셀값 정렬
           grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
            gv.Columns["ApplyDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; //단가 적용일


            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;                 //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;                  //셀값 수정시 상태값 수정
            //gv.GridControl.ProcessGridKey += Event_ProcessGridKey_History;  //행추가, 삽입, 삭제 단축키기능 부여
            //gv.ValidatingEditor += Event_ValidatingEdito_History;           //단가적용일 입력시 중복 확인
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            string value = e.Value.ConvertString_Null_To_Empty();
            DataRow curDr = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;  //Status값 수정 Roop 방지
            grid.Change_Status(gv, e.RowHandle);

            DataRow dr = gv.GetFocusedDataRow();

            if (fieldName == "Price")
            {
                dr["ApplyDate"] = DateTime.Now;
            }
            //if (fieldName == "Item_Nm")
            //{
            //    grid.SetInfo_Item(dr, value);
            //}

            //if (fieldName == "Cust_Nm")
            //{
            //    gv.SetFocusedRowCellValue("Cust_Cd", value);
            //}

            gv.BestFitColumns();
        }

        #endregion History End

        #region 조회

        private void DisplayCust()
		{
            CM_Cust cc = new CM_Cust();
            cc.CustType = LupPriceGroup.smtLookUpValue.ConvertString_Null_To_Empty();
            GcCustomer.DataSource = cc.SelectCustomer_Price();

            GvCustomer.BestFitColumns();
        }

        private void DisplayPrice()
        {
            DataRow dr = GvCustomer.GetFocusedDataRow();
            if (dr == null) return;

            string custCode = dr["CustCode"].ConvertString_Null_To_Empty();
            string priceType = LupPriceGroup.smtLookUpValue.ConvertString_Null_To_Empty();

            if (priceType == "" || custCode == "")
            {
                return;
            }

            CM_ItemPrice price = new CM_ItemPrice();
            price.CustCode = custCode;
            price.PriceType = priceType;

            gcMaterialPrice.DataSource = price.SelectMaterialPrice();

            gvMaterialPrice.BestFitColumns();
        }

        private void Search_ItemPrice_History()
        {
            ////해당품목 거래처별 단가 조회
            //Check_Search_History();

            //GridView gv = gcMaterialPrice.MainView as GridView;
            //DataRow dr = gv.GetFocusedDataRow();

            ////신규Row면 단가이력 안보여주기
            //if (Convert.ToString(dr["Status"]) != "신규")
            //{
            //    //Pnl_History.Visible = true;
            //    layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //    Selected_ItemCode = Convert.ToString(dr["ItemCode"]);
            //    Selected_Cust_Cd = Convert.ToString(dr["CustCode"]);

            //    CM_ItemPriceHistory history = new CM_ItemPriceHistory();
            //    history.ItemCode = Selected_ItemCode;
            //    history.CustCode = Selected_Cust_Cd;

            //    Gc_History.DataSource = history.Select_MaterialPriceHistory();
            //    Gv_History.BestFitColumns();
            //}
        }

        private bool Check_Search_History()
        {
            GridView gv = Gv_History;

            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status == "신규"
                    || status == "수정"
                    || status == "삭제")
                {
                    return MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes;
                }
            }
            return true;
        }
        #endregion 조회 End

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                GridView gv = gvMaterialPrice;

                gv.PostEditor();
                gv.UpdateCurrentRow();

                //필수입력 확인
                if (!Check_SaveClick(gv)) return;

                //작성내역 저장
                DataTable dt = gcMaterialPrice.DataSource as DataTable;

                //2. 데이터 저장
                for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
                {
                    if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                    {
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
                //3. 조회
                DisplayPrice();

                MessageBox.Show("저장이 정상적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Check_SaveClick(GridView gv)
        {
            //Item을 불러온 상태인지 검사
            if (gv.RowCount == 0) return false;

            int checkCnt = 0;

            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status == "신규")
                {
                    //미입력된 칸 있는지 확인
                    if (Convert.ToString(dr["PriceType"]) == "" || Convert.ToString(dr["PriceType"]) == "ALL")
                    {
                        MessageBox.Show("단가구분이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["CustCode"]) == "" || Convert.ToString(dr["ItemCode"]) == "")
                    {
                        MessageBox.Show("품목 또는 거래처가 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["Price"]) == "")
                    {
                        MessageBox.Show("단가가 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["ApplyDate"]) == "")
                    {
                        MessageBox.Show("단가적용일이 비어있습니다.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (status == "수정")
                {
                    if (Convert.ToString(dr["PriceType"]) == "" || Convert.ToString(dr["PriceType"]) == "ALL")
                    {
                        MessageBox.Show("단가그룹이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["ApplyDate"]) == "" || dr["ApplyDate"].ConvertDate_To_Char10() == "0001-01-01")
                    {
                        MessageBox.Show("단가적용일이 비어있습니다.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (status == "삭제")
                {
                    return MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel) == DialogResult.OK;
                    checkCnt++;
                }

            }
            if (checkCnt == 0) return false;

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            CM_ItemPrice mp = new CM_ItemPrice();
            mp.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
            mp.ItemCode = Convert.ToString(dr["ItemCode"]);
            mp.PriceType = Convert.ToString(dr["PriceType"]);
            mp.CustCode = Convert.ToString(dr["CustCode"]);
            mp.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
            mp.ApplyDate = Convert.ToDateTime(dr["ApplyDate"]).ToString("yyyy-MM-dd");
            mp.EndDate = dr["EndDate"].ConvertString_Null_To_Empty() == "" ? "" : dr["EndDate"].ConvertDate_To_Char10();
            mp.CustAlias = dr["CustAlias"].ConvertString_Null_To_Empty();
            mp.Remark = dr["Remark"].ConvertString_Null_To_Empty();

            //CM_ItemPriceHistory history = new CM_ItemPriceHistory();
            //history.PriceType = Convert.ToString(dr["PriceType"]);
            //history.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
            //history.ApplyDate = Convert.ToDateTime(dr["ApplyDate"]).ToString("yyyy-MM-dd");
            //history.Remark = dr["Remark"].ConvertString_Null_To_Empty();
            //history.ItemCode = Convert.ToString(dr["ItemCode"]);
            //history.CustCode = Convert.ToString(dr["CustCode"]);

            //2. DB 적용
            if (div == 'I')
            {
                if (mp.PriceType == "")
                {
                    MessageBox.Show("단가그룹을 선택해주세요.");
                    return false;
                }
                if (mp.CustCode == "")
                {
                    MessageBox.Show("거래처를 선택해주세요.");
                    return false;
                }
                if (mp.ItemCode == "")
                {
                    MessageBox.Show("제품을 선택해주세요.");
                    return false;
                }
                if (!mp.Ins_MaterialPrice())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'U')
            {
                //Update
                if (!mp.Upd_MaterialPrice())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                //if (!history.Ins_MaterialPriceHistory())     // 단가이력 등록
                //{
                //    //저장 중 에러 발생시
                //    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                //    return false;
                //}
            }
            else if (div == 'D')
            {
                //Update
                if (!mp.Del_MaterialPrice())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }

            //상태값 초기화
            dr[0] = "";
            DisplayPrice();
            return true;
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drNew = gv.GetFocusedDataRow();
            string fieldName = gv.FocusedColumn.FieldName;

            if (fieldName == "ItemCode")
            {
                for (int i = 0; i < gv.RowCount; i++)
                {
                    DataRow drComp = gv.GetDataRow(i);

                    if (drNew == drComp) continue;

                    //// 거래처, 구분, 품목 모두 같은지 확인
                    //if ((drComp["ItemCode"].ConvertString_Null_To_Empty() == e.Value.ConvertString_Null_To_Empty())
                    //    && (drComp["CustCode"].ConvertString_Null_To_Empty() == drNew["CustCode"].ConvertString_Null_To_Empty())
                    //    && (drComp["PriceType"].ConvertString_Null_To_Empty() == drNew["PriceType"].ConvertString_Null_To_Empty())
                    //    )
                    //{
                    //    e.Valid = false;
                    //    e.ErrorText = "중복된 제품코드가 있습니다.";

                    //    break;
                    //}
                }
            }
        }

        private void gvMaterialPrice_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void gvMaterialPrice_ShowingEditor(object sender, CancelEventArgs e)
        {
            // 현재 행의 상태 확인 (예: 신규, 수정)
            string status = Convert.ToString(gvMaterialPrice.GetRowCellValue(gvMaterialPrice.FocusedRowHandle, "Status"));

            // 상태가 "신규"가 아닐 때, ItemCode, PriceType, CustCode 칼럼의 편집을 막음
            if (status != "신규")
            {
                if (gvMaterialPrice.FocusedColumn.FieldName == "ItemCode" ||
                    gvMaterialPrice.FocusedColumn.FieldName == "PriceType" ||
                    gvMaterialPrice.FocusedColumn.FieldName == "CustCode")
                {
                    e.Cancel = true;  // 편집을 취소
                }
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayCust();
            DisplayPrice();
        }

        #region History 이벤트

        #region History 행 삽입, 추가, 삭제
        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            //S-MES 에서 btn.Name => btn.Tag로 수정.
            //SimpleButton btn = sender as SimpleButton;
            //string txt_Act = btn.Name.Substring(btn.Name.Length - 3);
            //Button btn = sender as Button;

            // 1. 태그의 마지막 3글자 가져오기(Ins,Del)
            if (sender is smtSearchButton btn)
            {
                string txt_Act = Convert.ToString(btn.Tag).Substring(Convert.ToString(btn.Tag).Length - 3);

                DataRow drCust = GvCustomer.GetFocusedDataRow();
                if (drCust == null) return;

                //2. 입력 데이터 설정
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (txt_Act != "Del")
                {
                    //삽입, 추가 진행인 경우
                    dic.Add("Status", "신규");
                    dic.Add("PriceType", drCust["CustType"]);
                    dic.Add("CustCode", drCust["CustCode"]);
                }
                else
                {
                    //삭제 진행인경우
                    dic.Add("Status", Convert.ToString(gvMaterialPrice.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
                }
                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(gcMaterialPrice, dic, txt_Act);
            }
            else
            {
                // sender가 System.Windows.Forms.Button이 아닌 경우 처리
                throw new ArgumentException("유효하지 않은 버튼입니다.");
            }
        }

        private void Btn_RowEdit_History(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);


            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("Price", 0);
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_History.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_History, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제End

        #region History 저장
        private void Btn_History_Save_Click(object sender, EventArgs e)
        {
            //Func_Save_History();
        }

        //private void Func_Save_History()
        //{
        //    //작성내역 저장
        //    GridView gv = Gv_History;

        //    //1. 데이터 불러온 상태인지 검사
        //    try
        //    {
        //        if (!Check_History_SaveClick(gv)) return;

        //        bool Success = true;
        //        //2. 데이터 저장
        //        for (int idx = 0; idx < gv.RowCount; idx++)
        //        {
        //            DataRow dr = gv.GetDataRow(idx);
        //            string status = Convert.ToString(dr["Status"]);

        //            switch (status)
        //            {
        //                case "신규": if (!History_Save_ToDatabase(dr, 'I')) { Success = false; } break;
        //                case "수정": if (!History_Save_ToDatabase(dr, 'U')) { Success = false; } break;
        //                case "삭제": if (!History_Save_ToDatabase(dr, 'D')) { Success = false; } break;
        //            }

        //            //Row별 정상처리 절차
        //            if (Success)
        //            {
        //                //상태값 초기화
        //                dr[0] = "";
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        //3. 정상절차 진행
        //        if (Success)
        //        {
        //            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        //            DisplayPrice();
        //            Search_ItemPrice_History();
        //        }
        //        else
        //        {
        //            MessageBox.Show("저장 중 오류가 발생하였습니다.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private bool Check_History_SaveClick(GridView gv)
        {
            //Item을 불러온 상태인지 검사
            if (gv.RowCount == 0) return false;


            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status == "신규")
                {
                    //미입력된 칸 있는지 확인
                    if (Convert.ToString(dr["Price"]) == "")
                    {
                        MessageBox.Show("단가가 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["ApplyDate"]) == "")
                    {
                        MessageBox.Show("단가적용일이 비어있습니다.");
                        return false;
                    }
                }
                else if (status == "수정")
                {
                    //등록이력 수정이 안돼서 주석처리 함

                    //BS_ItemPrice_History his = new BS_ItemPrice_History();
                    //his.Price_Div_Cd = Convert.ToString(dr["Price_Div_Cd"]);
                    //if (MainObject == "Item")
                    //{
                    //his.ItemCode = Selected_Main_Cd;
                    //his.Cust_Cd = Selected_Sub_Cd;
                    //}
                    //else if (MainObject == "Cust")
                    //{
                    //his.Cust_Cd = Selected_Main_Cd;
                    //his.ItemCode = Selected_Sub_Cd;
                    //}
                    //his.Apply_Date = dr["Apply_Date"].ConvertDate_To_Char8();

                    //if (his.Select_ItemPrice_History_ApplyDate() != null)
                    //{
                    //MessageBox.Show("해당날짜에 이미 등록된 내역이 있습니다.");
                    //return false;
                    //}
                }
                else if (status == "삭제")
                {
                    return MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel) == DialogResult.OK;
                }
            }
            return true;
        }

        //private bool History_Save_ToDatabase(DataRow dr, char div)
        //{
        //    //데이터베이스에 반영
        //    //1. 기본 셋팅
        //    CM_ItemPriceHistory history = new CM_ItemPriceHistory();
        //    history.Idx = Convert.ToInt32(dr["Idx"]);
        //    history.CustCode = Selected_Cust_Cd;
        //    history.ItemCode = Selected_ItemCode;
        //    history.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
        //    history.ApplyDate = Convert.ToDateTime(dr["ApplyDate"]).ToString("yyyy-MM-dd");
        //    history.Remark = Convert.ToString(dr["Remark"]);

        //    //BS_ItemPrice price = new BS_ItemPrice();
        //    //price.Price_Div_Cd = history.Price_Div_Cd;
        //    //price.ItemCode = history.ItemCode;
        //    //price.Cust_Cd = history.Cust_Cd;
        //    //price.Price = history.Price;
        //    //price.Apply_Date = history.Apply_Date;

        //    //2. DB 적용
        //    if (div == 'I')
        //    {
        //        if (!history.Ins_MaterialPriceHistory())
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            //추가 변경사항 private 따로 만들기
        //            //if (!price.Upd_ItemPrice()) return false;
        //        }
        //    }
        //    else if (div == 'U')
        //    {
        //        if (!history.Upd_MaterialPriceHistory())
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            //추가 변경사항 private 따로 만들기
        //            //최근단가변경이력으로 Update
        //            //if (!price.Upd_ItemPrice()) return false;
        //        }
        //    }
        //    else if (div == 'D')
        //    {
        //        if (!history.Del_MaterialPriceHistory())
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            //추가 변경사항 private 따로 만들기
        //            //최상위 단가로 최근단가 변경
        //            //if (!price.Upd_ItemPrice_ByHD()) return false;
        //        }
        //    }

        //    return true;
        //}

        #endregion History 저장 End

        private void Btn_History_Close_Click(object sender, EventArgs e)
        {
            layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

		#endregion History 이벤트 End

		private void cmdExcelUpload_Click(object sender, EventArgs e)
		{
            Frm_CM_ExcelUpLoad_ItemlPrice f = new Frm_CM_ExcelUpLoad_ItemlPrice();
            //f.Gc = gcMaterialPrice;
			f.FormClosed += F_FormClosed;
            f.ShowDialog();
        }

		private void F_FormClosed(object sender, FormClosedEventArgs e)
		{
            DisplayPrice();
		}
	}
}