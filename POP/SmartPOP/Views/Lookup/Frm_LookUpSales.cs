using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.BM;
using SmartPOP.Models.CM;
using SMT_MES.Controls;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartPOP.View.Lookup
{
	public partial class Frm_LookUpSales : DevExpress.XtraEditors.XtraForm
    {
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        clsCode code = new clsCode();
        Database db = new Database();
        string[] ItemDiv = new string[] { "20", "30" };

        private string SalesNo = "";
        string cmdType = "";//I:신규, U:수정
        string query = "";

        public Frm_LookUpSales(string salesNo)
        {
            InitializeComponent();

            this.SalesNo = salesNo;

            if (SalesNo == "")
            {
                cmdType = "I";
            }
            else
            {
                cmdType = "U";
            }
        }

        private void frmLookUpSales_Load(object sender, EventArgs e)
        {
            Set_GridView(GvBM_SalesD);

            SetData();
        }

        private void Set_GridView(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsView.ShowGroupPanel = false;

            //
            gv.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", ItemDiv, "", ""), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(ItemDiv), "품목코드", "품명");

            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");  //품목분류1
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");  //품목분류2
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");  //품목분류3

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "출하번호", "SalesNo");
            grid.Create_GvColumn(gv, "출하순번", "SalesSeq");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "수량", "Qty");
            grid.Create_GvColumn(gv, "단가", "UnitPrice");
            grid.Create_GvColumn(gv, "금액", "Price");
            grid.Create_GvColumn(gv, "부가세", "VatPrice");
            grid.Create_GvColumn(gv, "총금액", "TotalPrice");
            grid.Create_GvColumn(gv, "창고", "HouseCode");
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "등록자", "CreID", lup_Emp_No);
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModID", lup_Emp_No);
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository 생성 or 이벤트 입력
            gv.Columns["SalesNo"].Visible = false;
            gv.Columns["SalesSeq"].Visible = false;
            gv.Columns["HouseCode"].Visible = false;

            //Column 단일 설정
            //
            //gv.Columns["MaterialCode"].FieldName = "ProductCode";   //출하 테이블 필드명이 달라 바꿔줌
            //gv.Columns["MaterialName"].FieldName = "ProductName";   //출하 테이블 필드명이 달라 바꿔줌

            //Repository 생성 or 이벤트 입력
            gv.Columns["ItemName"].ColumnEdit = lup_Item;

            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;


            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ItemName"]);
            grid.Column_AllowEdit(gv.Columns["Qty"]);
            grid.Column_AllowEdit(gv.Columns["UnitCode"]);
            grid.Column_AllowEdit(gv.Columns["UnitPrice"]);
            grid.Column_AllowEdit(gv.Columns["Price"]);
            grid.Column_AllowEdit(gv.Columns["VatPrice"]);
            grid.Column_AllowEdit(gv.Columns["TotalPrice"]);
            grid.Column_AllowEdit(gv.Columns["HouseCode"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;     //상태
            grid.SetCol_Type(gv.Columns["Qty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], cls_Grid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "TotalPrice");

            //칼럼 너비 설정
            gv.Columns["Status"].MaxWidth = 60;

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
			gv.CustomSummaryCalculate += Gv_CustomSummaryCalculate;

            grid.SetPOPGrid(gv);
        }
		private void Event_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            string fieldName = e.Column.FieldName;
            string value = e.Value.ConvertString_Null_To_Empty();

            if (fieldName == "ItemName")
            {
                //품목 관련 정보 채우기
                grid.SetInfo_Item(dr, value);

                //단가 가져오기
                if (smtBillToCode.smtLookUpValue != "" && dr["UnitPrice"].ConvertFloat_Null_To_Zero() == 0)
                {
                    string custCode = smtBillToCode.smtLookUpValue;
                    string materialCode = dr["ItemCode"].ConvertString_Null_To_Empty();

                    if (custCode != "" && materialCode != "")
                    {
                        CM_ItemPrice price = new CM_ItemPrice();
                        price.ApplyDate = smtSalesDate.smtDateTimeValue.ToString("d");
                        price.PriceType = "20";

                        dr["UnitPrice"] = price.Get_MaterialPrice(custCode, materialCode).ConvertFloat_Null_To_Zero();
                    }
                }
            }
            else if (fieldName == "Qty" || fieldName == "UnitPrice")
            {
                //금액계산
                float outQty = dr["Qty"].ConvertFloat_Null_To_Zero();
                float unitPrice = dr["UnitPrice"].ConvertFloat_Null_To_Zero();

                float price = outQty * unitPrice;

                dr["Price"] = Math.Round(price, 0);
                dr["VatPrice"] = Math.Round((price * 0.1), 0);
                dr["TotalPrice"] = Math.Round((price + (price * 0.1)), 0);

                gv.UpdateCurrentRow();

                gv.UpdateSummary();

                smtTotalPrice.smtTextBoxValue = gv.Columns["TotalPrice"].SummaryItem.SummaryValue.ConvertString_Null_To_Zero();
            }

            gv.BestFitColumns();
        }

        private void Gv_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if(e.FieldValue.ToString() == "TotalPrice")
			{
                smtTotalPrice.smtTextBoxValue = e.TotalValue.ConvertString_Null_To_Zero();
			}
        }

        private void SetData()
        {
            sblShipToCode.smtLookUpDataSource = code.smtLookUp("거래처", "매출", "");
            smtBillToCode.smtLookUpDataSource = code.smtLookUp("거래처", "매출", "");
			smtHouseCode.smtLookUpDataSource = code.smtLookUp("창고정보", "20", "");

			switch (cmdType)
			{
                case "I": 
                    smtSalesDate.smtDateTimeValue = DateTime.Now;
                    break;

                case "U":
                    BM_SalesM sm = new BM_SalesM();
                    sm.SalesNo = this.SalesNo;

                    DataRow dr = sm.SelectBM_SalesM_BySalesNo();

                    if(dr != null)
					{
					    smtSalesDate.smtDateTimeValue = Convert.ToDateTime(dr["SalesDate"]);
					    sblShipToCode.smtLookUpValue = dr["ShipToCode"].ConvertString_Null_To_Empty();
					    smtBillToCode.smtLookUpValue = dr["BillToCode"].ConvertString_Null_To_Empty();
					    smtTotalPrice.smtTextBoxValue   = dr["TotalPrice"].ConvertString_Null_To_Empty();
					    smtHouseCode.smtLookUpValue = dr["HouseCode"].ConvertString_Null_To_Empty();
					    smtRemark.smtMemoValue       = dr["Remark"].ConvertString_Null_To_Empty();
					}
					break;
			}

            Display();
        }

        private void Display()
		{
            BM_SalesD sd = new BM_SalesD();
            sd.SalesNo = this.SalesNo;

            GcBM_SalesD.DataSource = sd.Select_BM_SalesD_BySalesNo_Lup();
            GvBM_SalesD.BestFitColumns();
		}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Check_SaveClick()) return;
           
            //마스터 저장
            BM_SalesM sm = new BM_SalesM();
            sm.CorpId = WrGlobal.CorpID;
            sm.SalesNo = this.SalesNo;
            sm.SalesDate = smtSalesDate.smtDateTimeValue.ToString("d");
            sm.ShipToCode = sblShipToCode.smtLookUpValue;
            sm.ShipToName = sblShipToCode.smtLookUpText;
            sm.WriteID = WrGlobal.LoginID;
            sm.WriteName = WrGlobal.LoginUserNM;
            sm.WriteDate = DateTime.Now.ToString("d");

            sm.BillToCode = smtBillToCode.smtLookUpValue;
            sm.BillToName = smtBillToCode.smtLookUpText;

            sm.HouseCode = smtHouseCode.smtLookUpValue;
            sm.TotalPrice = smtTotalPrice.smtTextBoxValue.ConvertDecimal_Null_To_Zero();
            sm.OutType = "10";//10:제품,30:자재
            sm.Cnt = GvBM_SalesD.RowCount;
            sm.Remark = smtRemark.smtMemoValue.ConvertString_Null_To_Empty();

            if (cmdType == "I")
            {
                this.SalesNo = "OG" + code.GetAutoNumber("출고", (DateTime)smtSalesDate.smtDateTimeValue);
                sm.SalesNo = this.SalesNo;

                if (!sm.InsertBM_SalesM())
                {
                    cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                    return;
                }

            }
            else if (cmdType == "U")
            {
                if (!sm.UpdateBM_SalesM())
                {
                    cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                    return;
                }
            }

            //디테일 저장
            DataTable dt = GcBM_SalesD.DataSource as DataTable;

            int seq = 1;
			foreach (DataRow item in dt.Rows)
			{
                BM_SalesD sd = new BM_SalesD();
                sd.CorpId = WrGlobal.CorpID;
                sd.SalesNo = this.SalesNo;
                sd.SalesSeq = this.cmdType == "I" ? seq++: item["SalesSeq"].ConvertInt_Null_To_Zero();
                sd.ProductCode = item["ItemCode"].ConvertString_Null_To_Empty();
                sd.Qty = item["Qty"].ConvertFloat_Null_To_Zero();
                sd.ReqType = "출하출고";
                sd.Status = "";
                sd.UnitCode = item["UnitCode"].ConvertString_Null_To_Empty();
                sd.HouseCode = item["HouseCode"].ConvertString_Null_To_Empty();
                sd.UnitPrice = item["UnitPrice"].ConvertFloat_Null_To_Zero();
                sd.Price = item["Price"].ConvertFloat_Null_To_Zero();
                sd.VatPrice = item["VatPrice"].ConvertFloat_Null_To_Zero();
                sd.TotalPrice = item["TotalPrice"].ConvertFloat_Null_To_Zero();
                sd.WonAmount = item["WonAmount"].ConvertFloat_Null_To_Zero();
                sd.Remark = item["Remark"].ConvertString_Null_To_Empty();
                sd.VatType = "10";
                sd.VatRate = 10;
                sd.Status = "출고완료";

                if (cmdType == "I")
                {
                    sd.InsertBM_SalesD();
                }
                else if (cmdType == "U")
                {
                    sd.UpdateBM_SalesD();
                }
            }

            //재고 처리
            query = $@"EXEC Make_MaterialOutStockList '{WrGlobal.CorpID}', '{this.SalesNo}', '{smtHouseCode.smtLookUpValue}', '{WrGlobal.LoginID}'";
            db.ExecuteQuery(query);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool Check_SaveClick()
        {

            if ((smtBillToCode.smtLookUpValue ?? "").ToString() == "")
            {
                cls_MessageBox.Show("거래처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (smtHouseCode.smtLookUpValue.ConvertString_Null_To_Empty() == "")
            {
                cls_MessageBox.Show("창고를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if ((smtHouseCode.smtLookUpValue ?? "").ToString() == "")
            {
                cls_MessageBox.Show("출고창고를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            DataTable dt = GcBM_SalesD.DataSource as DataTable;

            if(dt == null)
			{
                cls_MessageBox.Show("상세내역을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
			} 
            
            if(dt.Rows.Count < 1)
			{
                cls_MessageBox.Show("상세내역을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
			}

			foreach (DataRow dr in dt.Rows)
			{
                if(dr["ItemName"].ConvertString_Null_To_Empty() == "")
				{
                    cls_MessageBox.Show("품목을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (dr["Qty"].ConvertInt_Null_To_Zero() == 0)
                {
                    cls_MessageBox.Show("수량을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
