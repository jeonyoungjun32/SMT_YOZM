using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.BM
{
    public partial class Frm_BM_SdOrder_1Grid : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsRepository rpo = new clsRepository();
        clsGrid grid = new clsGrid();

        private string[] CustDiv = { "20", "40"}; //거래처구분 리스트(10 : 매입, 20 : 매출, 40 :매입/매출)
        string[] ItemType = WrGlobal.Product_ItemType;

        public Frm_BM_SdOrder_1Grid()
        {
            InitializeComponent();
        }

        private void frmObtain_HS_Load(object sender, EventArgs e)
        {
            InitLup();

            SetGvDetail(GvBM_SdOrderD);

            DisplayData();
        }

        private void InitLup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            code.LookupCustomer(WrGlobal.CorpID, "매출", true, "", ref Lup_Cust);
            Lup_Cust.smtLookUpValue = "ALL";
        }

        #region GridView Setting
        private void SetGvDetail(GridView gv)
		{
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.OptionsSelection.MultiSelect = true; //그리드 멀티셀렉트 설정
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "진행구분", "State");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "순번", "SdOrderSeq");
            grid.Create_GvColumn(gv, "수주일자", "ObtainDate");
            grid.Create_GvColumn(gv, "납기예정일", "DeliveryDate");
            grid.Create_GvColumn(gv, "거래처코드", "SoldToCode");
            grid.Create_GvColumn(gv, "거래처명", "SoldToName");
            grid.Create_GvColumn(gv, "납품처코드", "ShipToCode");
            grid.Create_GvColumn(gv, "납품처명", "ShipToName");

            //CodeOption에 따른 Column 생성부
            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "수주수량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "부과세구분", "VatType");
            grid.Create_GvColumn(gv, "부과세율", "VatRate");
            grid.Create_GvColumn(gv, "담당자", "SalesMan");
            grid.Create_GvColumn(gv, "창고", "HouseID");

            grid.Create_GvColumn(gv, "수주구분", "OrderType");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository 설정
            RepositoryItemSearchLookUpEdit lup_SoldToCode = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(CustDiv), "거래처코드", "거래처명");

            DataTable dtUser = code.GetDataTableUser(false);
            RepositoryItemSearchLookUpEdit lup_SalesMan = rpo.Create_RepositoryItemSearchLookUp(dtUser, "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(dtUser, "코드", "명칭");

            DataTable dt = code.GetMaterialList(ItemType, false).ToTable();
            RepositoryItemSearchLookUpEdit lup_ItemCode = rpo.Create_RepositoryItemSearchLookUp(dt, "품목코드", "품명");
            //grid.SetLupView_Item(lup_ItemCode, "제품코드 초기화");

            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            //수주구분
            CM_CateItem cci = new CM_CateItem();
            RepositoryItemSearchLookUpEdit lup_OrderType = rpo.Create_RepositoryItemSearchLookUp(cci.SelectClassItemLookUp(WrGlobal.CorpID, "S020"), "분류코드", "분류명");

            //창고
            RepositoryItemSearchLookUpEdit lup_House = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            //과세구분
            code.Class = "C010";
            RepositoryItemSearchLookUpEdit lup_VatType = rpo.Create_RepositoryItemSearchLookUp(code.Select_CM_CateItem(), "ItemCode", "Title" );

            gv.Columns["SoldToName"].ColumnEdit = lup_SoldToCode;
            gv.Columns["ShipToName"].ColumnEdit = lup_SoldToCode;
            gv.Columns["ItemName"].ColumnEdit = lup_ItemCode;
            gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;
            gv.Columns["SalesMan"].ColumnEdit = lup_SalesMan;
            gv.Columns["HouseID"].ColumnEdit = lup_House;
            gv.Columns["OrderType"].ColumnEdit = lup_OrderType;
            gv.Columns["VatType"].ColumnEdit = lup_VatType;
            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["SoldToName"]);
            grid.Column_AllowEdit(gv.Columns["ShipToName"]);
            grid.Column_AllowEdit(gv.Columns["ItemName"]);
            grid.Column_AllowEdit(gv.Columns["ObtainDate"]);
            grid.Column_AllowEdit(gv.Columns["Qty"]);
            grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            grid.Column_AllowEdit(gv.Columns["Price"]);
            grid.Column_AllowEdit(gv.Columns["TotalPrice"]);
            grid.Column_AllowEdit(gv.Columns["SalesMan"]);
            grid.Column_AllowEdit(gv.Columns["DeliveryDate"]);
            grid.Column_AllowEdit(gv.Columns["VatType"]);
            grid.Column_AllowEdit(gv.Columns["VatRate"]);
            grid.Column_AllowEdit(gv.Columns["HouseID"]);
            grid.Column_AllowEdit(gv.Columns["OrderType"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            gv.Columns["Remark"].MinWidth = 200;

            //값 정렬
            grid.SetCol_Type(gv.Columns["ObtainDate"], clsGrid.TypeList.TDate);         //수주일자
            grid.SetCol_Type(gv.Columns["DeliveryDate"], clsGrid.TypeList.TDate);       //납기일자
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);               //수량
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);         //단가
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);             //단가
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);          //세액
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);        //금액
            gv.Columns["State"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //이벤트
            gv.OptionsBehavior.Editable = true;
            gv.OptionsBehavior.ReadOnly = false;
            grid.Set_Navigator(gv);
            grid.RowNumber_Indicator(gv);
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;              // 행추가, 삽입, 삭제 단축키기능 부여
            gv.CellValueChanged += GvOrder_CellValueChanged;

            gv.CustomRowCellEditForEditing += Gv_CustomRowCellEditForEditing;   // 거래처 수정시 해당거래처에 해당하는 납품처만 나오게 하기
            gv.ValidatingEditor += Event_ValidatingEditor;                      // 셀값 수정시 변경불가 Event

            //if (WrGlobal.ProcOption["BS"]["Multi_Item_Select_YN"] != "Y") // 멀티셀렉트
            //{
            //    grid.SetRpoItem_ByCust(gv, ItemDiv, PriceDiv, "제품코드 초기화");    //거래처 수정시 해당 거래처에 해당하는 품목만 나오게하기
            //}

            gv.Columns["SdOrderNo"].Visible = false;    //수주번호
            gv.Columns["SdOrderSeq"].Visible = false;   //등록순번
            gv.Columns["SoldToCode"].Visible = false;
            gv.Columns["ShipToCode"].Visible = false;

            //if (WrGlobal.CdOption["제품코드 초기화"]["ItemCode"].DispYn == false) gv.Columns["ItemCode"].Visible = false;

            //프로세스옵션 : 납품처 활성화 여부에 따른 컬럼 숨김처리
            if (WrGlobal.ProcOption["BN"].ContainsKey("Direct_Cust_YN"))
            {
                if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] != "Y")
                {
                    gv.Columns["ShipToCode"].Visible = false;
                    gv.Columns["ShipToName"].Visible = false;
                }
            }
            grid.RowNumber_Indicator(gv);
        }

        #endregion

        private void GvOrder_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                ////수정사항 있을시 상태값 수정 표기
                GridView gv = sender as GridView;
                DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
                string ColName = e.Column.FieldName;
                string value = e.Value.ConvertString_Null_To_Empty();

                //0. 수정사항 있을시 상태값 수정 표기
                if (ColName == gv.Columns[0].FieldName) return;
                grid.Change_Status(gv, e.RowHandle);

                //그리드 거래처 선택시
                if (ColName == "SoldToName")
                {
                    //거래처코드
                    drChanged["SoldToCode"] = value;

                    // 거래처별 단가 셋팅
                    Edit_PriceValue_Row(drChanged);
                }

                if (ColName == "ShipToName")
                {
                    drChanged["ShipToCode"] = value;
                }

                //품목 선택시
                if (ColName == "ItemName")
                {
                    grid.SetInfo_Item(drChanged, Convert.ToString(e.Value));
                    //Update_Cust_Alias(drChanged);

                    // 거래처별 단가 셋팅
                    Edit_PriceValue_Row(drChanged);
                }

                //수량 및 단가, 중량 수정 시
                if (ColName == "Qty" || ColName == "UnitPrice" || ColName == "VatType" || ColName == "VatRate")
                {
                    decimal qty = Convert.ToDecimal(drChanged["Qty"].ConvertFloat_Null_To_Zero());
                    decimal unitPrice = drChanged["UnitPrice"].ConvertDecimal_Null_To_Zero();

                    // 공급가액
                    decimal price = qty * unitPrice;

                    // 부가세율
                    decimal vatRate = Convert.ToString(drChanged["VatType"]) == "10" ? Convert.ToDecimal(drChanged["VatRate"]) : 0;

                    // 부가세
                    decimal vatPrice = price * (vatRate / 100);

                    // 합계금액
                    decimal totalPrice = price + vatPrice;

                    drChanged["VatRate"] = vatRate;
                    drChanged["Price"] = price;
                    drChanged["VatPrice"] = vatPrice;
                    drChanged["TotalPrice"] = totalPrice;
                }
                GvBM_SdOrderD.BestFitColumns();
            }
            catch
            { }
        }

        private void Edit_PriceValue_Row(DataRow dr)
        {
            string custCode = dr["SoldToCode"].ConvertString_Null_To_Empty();
            string itemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            string order_Dt = dr["ObtainDate"].ConvertDate_To_Char8();

            //2. 단가 이력 호출
            CM_ItemPrice itemPrice = new CM_ItemPrice();
            itemPrice.ItemCode = itemCode;
            itemPrice.CustCode = custCode;
            itemPrice.PriceType = "20";     // 매입 : 10, 매출 : 20
            itemPrice.ApplyDate = order_Dt;
            DataRow drPrice = itemPrice.SelectMaterialPrice_Dr();  //단가이력 호출

            //3. 데이터 입력, 입력되면서 이벤트 한번 더 진행되서 금액 입력됨
            if (drPrice != null)
            {
                dr["UnitPrice"] = Convert.ToSingle(drPrice["Price"]);
            }
            else
            {
                dr["UnitPrice"] = 0;
            }

            float Qty = dr["Qty"].ConvertFloat_Null_To_Zero();
            float price = dr["UnitPrice"].ConvertFloat_Null_To_Zero();
            dr["Price"] = Qty * price;
            dr["VatPrice"] = Qty * price * 0.1;
            dr["TotalPrice"] = Qty * price * 1.1;
        }

        private void Gv_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
			//GridView gv = sender as GridView;

			////Grid데이터가 없으면 스킵
			//if (e.RowHandle < 0) return;
			////현재행 != 수정하려는 행일 시 스킵
			//if (e.RowHandle != gv.FocusedRowHandle) return;
			////적용되려는열이 거래처가 아니면 스킵
			//if (e.Column.FieldName != "Direct_" + "SoldToName") return;

			////거래처리스트 호출
			//DataRow dr = gv.GetDataRow(e.RowHandle);

   //         CM_Custdirect direct = new CM_Custdirect();
			//direct.SoldToCode = dr["SoldToCode"].ConvertString_Null_To_Empty();
			//DataTable dt = direct.Select_CustDirectLup();

   //         if(dt != null)
			//{
   //             if(dt.Rows.Count > 0)
			//	{
			//        e.RepositoryItem = rpo.Create_RepositoryItemSearchLookUp(dt, "코드", "명칭");
			//	}
			//}
			//else
			//{

			//}
		}

        private void Event_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow dr = gv.GetFocusedDataRow();

            //프로세스 옵션
            //수주일자 변경 가능여부
            if (WrGlobal.ProcOption["BN"]["ROrder_Date_ChangeYN"] == "Y")
            {
                //1. 입고조회상태 & OrderNo가 빈값이 아니면 -> 거래처, 품명 수정불가
                if (Convert.ToString(dr["SdOrderNo"]) != ""
                    && (gv.FocusedColumn.FieldName == "SoldToName" ||
                        gv.FocusedColumn.FieldName == "ItemName"))
                {
                    e.Valid = false;
                    e.ErrorText = $"수주정보가 있는 행은 {gv.FocusedColumn.Caption}을/를 수정할 수 없습니다.";
                }
            }
            else
            {
                //1. 입고조회상태 & OrderNo가 빈값이 아니면 -> 구분, 입고일자, 거래처, 품명 수정불가
                if (Convert.ToString(dr["SdOrderNo"]) != ""
                    && (//gv.FocusedColumn.FieldName == "ObtainDate" || //2023.12.28 수주일자 변경불가 임시 해제
                        gv.FocusedColumn.FieldName == "SoldToName" ||
                        gv.FocusedColumn.FieldName == "ItemName"))
                {
                    e.Valid = false;
                    e.ErrorText = $"수주정보가 있는 행은 {gv.FocusedColumn.Caption}을/를 수정할 수 없습니다.";
                }
            }
        }

        private void DisplayData()
        {
            string custCode = Lup_Cust.smtLookUpValue;
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

			BM_SdOrderD sd = new BM_SdOrderD();

			GcBM_SdOrderD.DataSource = sd.SelectBM_SdOrderD(WrGlobal.CorpID, startDate, endDate, custCode);
			GvBM_SdOrderD.BestFitColumns();
		}

        #region 행 삽입, 추가, 삭제
        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): btn_Ins.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): btn_Del.PerformClick(); break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            // 1. 태그의 마지막 3글자 가져오기(Ins, Del)
            if (sender is smtSearchButton btn)
            {
                string txt_Act = Convert.ToString(btn.Tag).Substring(Convert.ToString(btn.Tag).Length - 3);

                //2. 입력 데이터 설정
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (txt_Act != "Del")
                {
                    //삽입, 추가 진행인 경우
                    string SoldToCode = Convert.ToString(Lup_Cust.smtLookUpValue).ToUpper() == "ALL" ? "" : Convert.ToString(Lup_Cust.smtLookUpValue);

                    dic.Add("Status", "신규");
                    dic.Add("ObtainDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    dic.Add("DeliveryDate", DateTime.Now.Date.AddDays(7));
                    dic.Add("SoldToCode", SoldToCode);
                    dic.Add("SoldToName", SoldToCode);
                    dic.Add("SalesMan", WrGlobal.LoginID);
                    dic.Add("HouseID", "S0002"); //완제품창고
                    dic.Add("VatType", "10");
                    dic.Add("VatRate", 10);
                    dic.Add("OrderType", "10");
                    dic.Add("State", "진행");
                }
                else
                {
                    //삭제 진행인경우
                    dic.Add("Status", Convert.ToString(GvBM_SdOrderD.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
                }
                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(GcBM_SdOrderD, dic, txt_Act);
            }
            else
            {
                throw new ArgumentException("유효하지 않은 버튼입니다.");
            }
        }
        #endregion

        #region 저장 기능
        private void cmdSave_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            GvBM_SdOrderD.PostEditor();
            GvBM_SdOrderD.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = GcBM_SdOrderD.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

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
            //3. 정상절차 진행
            DisplayData();
            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private bool Check_SaveClick(DataTable dt)
        {
            int checkCnt = 0;
            bool isDelete = false;

            //Data를 불러온 상태인지 검사
            if (dt == null)
            {
                return false;
            }

            //변경사항이 없을 경우 RETURN
            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    checkCnt++;
                }
                else if (status == "수정")
                {
                    checkCnt++;
                }
                else if (status == "삭제")
                {
                    checkCnt++;
                    isDelete = true;
                }
            }

            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    return true;
                }
            }
            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //1. 수주 마스터 저장
            BM_SdOrderM sm = new BM_SdOrderM();
            sm.CorpId = WrGlobal.CorpID;
            sm.SdOrderNo = dr["SdOrderNo"].ConvertString_Null_To_Empty();
            sm.SoldToCode = dr["SoldToCode"].ConvertString_Null_To_Empty();
            sm.ShipToCode = dr["ShipToCode"].ConvertString_Null_To_Empty();
            sm.ObtainDate = dr["ObtainDate"].ConvertDate_To_Char10();
            sm.DeliveryDate = dr["DeliveryDate"].ConvertDate_To_Char10();
            sm.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
            sm.VatPrice = dr["VatPrice"].ConvertDecimal_Null_To_Zero();
            sm.TotalPrice = dr["TotalPrice"].ConvertDecimal_Null_To_Zero();
            sm.OrderType = dr["OrderType"].ConvertString_Null_To_Empty();
            sm.VatType = dr["VatType"].ConvertString_Null_To_Empty();
            sm.VatRate = dr["VatRate"].ConvertInt_Null_To_Zero();
            sm.SalesMan = dr["SalesMan"].ConvertString_Null_To_Empty();
            sm.OutState = dr["OutState"].ConvertString_Null_To_Empty();
            sm.Remark = dr["Remark"].ConvertString_Null_To_Empty();
            sm.HouseID = dr["HouseID"].ConvertString_Null_To_Empty();
            sm.State = dr["State"].ConvertString_Null_To_Empty();

            try
            {
                if (div == 'I')
                {
                    //수주번호 생성
                    sm.SdOrderNo = "SO" + code.GetAutoNumber("수주번호", Convert.ToDateTime(sm.ObtainDate));

                    if (!sm.Ins_BM_SdOrderM())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    if (!sm.Upd_BM_SdOrderM())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            //2.수주 디테일 저장
            BM_SdOrderD sd = new BM_SdOrderD();
            sd.CorpId = WrGlobal.CorpID;
            sd.SdOrderNo = sm.SdOrderNo;
            sd.SdOrderSeq = 1;
            sd.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            sd.Qty = dr["Qty"].ConvertDecimal_Null_To_Zero();
            sd.ShipToCode = dr["ShipToCode"].ConvertString_Null_To_Empty();
            sd.UnitPrice = dr["UnitPrice"].ConvertDecimal_Null_To_Zero();
            sd.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
            sd.VatPrice = dr["VatPrice"].ConvertDecimal_Null_To_Zero();
            sd.TotalPrice = dr["TotalPrice"].ConvertDecimal_Null_To_Zero();
            sd.DeliveryDate = Convert.ToDateTime(dr["DeliveryDate"]).Date;
            sd.OrderStatus = dr["OrderStatus"].ConvertString_Null_To_Empty();
            sd.PlacingStatus = dr["PlacingStatus"].ConvertString_Null_To_Empty();
            sd.Remark = dr["Remark"].ConvertString_Null_To_Empty();
            sd.VatType = dr["VatType"].ConvertString_Null_To_Empty();
            sd.VatRate = dr["VatRate"].ConvertInt_Null_To_Zero();
            sd.State = dr["State"].ConvertString_Null_To_Empty();

            try
            {
                if (div == 'I')
                {
                    if (!sd.Ins_BM_SdOrderD())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    if (!sd.Upd_BM_SdOrderD())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    if (!sd.Del_BM_SdOrderD())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                    if (!sm.Del_BM_SdOrderM())
                    {
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }

                //상태값 초기화
                dr[0] = "";
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DisplayData();
        }
        #endregion

        #region 확정 기능

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            // 선택된 자료 확정
            if (GvBM_SdOrderD.GetSelectedRows().Length == 0)
            {
                MessageBox.Show("확정할 수주를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChangeState("수주확정");
            MessageBox.Show("확정되었습니다.");
            DisplayData();
        }

        private void btn_Confirm_Cancel_Click(object sender, EventArgs e)
        {
            // 선택된 자료 확정 취소
            if (GvBM_SdOrderD.GetSelectedRows().Length == 0)
            {
                MessageBox.Show("확정 취소할 수주를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChangeState("진행");
            MessageBox.Show("확정 취소되었습니다.");
        }

        private void ChangeState(string state)
        {
            //1. 선택된 자료 수주확정/취소

            int row;

            for (int rowCnt = 0; rowCnt < GvBM_SdOrderD.SelectedRowsCount; rowCnt++)
            {
                if (GvBM_SdOrderD.GetSelectedRows()[rowCnt] >= 0)
                {
                    row = GvBM_SdOrderD.GetSelectedRows()[rowCnt];
                    string sdOrderNo = GvBM_SdOrderD.GetRowCellValue(row, "SdOrderNo").ToString();

                    string checkYn = "N";

                    BM_SdOrderD sd = new BM_SdOrderD();
                    sd.CorpId = WrGlobal.CorpID;
                    sd.SdOrderNo = sdOrderNo;

                    //2. 확정취소를 하는 경우 BM_SdOrderD에 OrderStatus가 작업확정이 있는지 검사
                    if (state == "수주확정")
                    {
                        sd.OrderStatus = "작업확정";

                        checkYn = sd.GetSdOrderD_OrderStatus() ? "Y" : "N";
                    }

                    if (checkYn == "N")
                    {
                        //마스터 상태 업데이트
                        BM_SdOrderM sm = new BM_SdOrderM();
                        sm.CorpId = WrGlobal.CorpID;
                        sm.SdOrderNo = sdOrderNo;
                        sm.State = state;
                        sm.Upd_BM_SdOrderM_State();

                        //디테일 상태 업데이트
                        sd.OrderStatus = state;
                        sd.Upd_OrderStatus_BySdOrderNo();
                    }
                    else
                    {
                        MessageBox.Show("작업확정이 진행되어서 취소할 수 없습니다.");
                    }
                }
            }
            DisplayData();
        }
        #endregion

        #region Event
        private void btn_Search_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(GvBM_SdOrderD);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void GvBM_SdOrderD_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("State") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "수주확정")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Green;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.Transparent;
            }
        }
    }
}
