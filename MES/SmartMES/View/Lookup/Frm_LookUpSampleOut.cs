using System;
using System.Data;
using System.Windows.Forms;

using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.UI;

using SmartCommon.Common;

using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;

namespace SmartMES.View.Lookup
{
	public partial class Frm_LookUpSampleOut : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        Database db = new Database();
        clsObtainOrder sales = new clsObtainOrder();
        CM_Cust cust = new CM_Cust();

        private string SalesNo = "";
        string cmdType = "";//I:신규, U:수정
        string query = "";

        string[] ItemDiv = new string[] { "20", "60" }; // 10상품 20제품 30 반제품 40원자재 50부자재 60샘플/개발

        public Frm_LookUpSampleOut(string salesNo)
        {
            InitializeComponent();

            this.SalesNo = salesNo;

            if(SalesNo == "")
			{
                cmdType = "I";
			}
			else
			{
                cmdType = "U";
			}
        }


        private void Frm_LookUpSales_Load(object sender, EventArgs e)
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
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(ItemDiv), "품목코드", "품명");

            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");  //품목분류1
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");  //품목분류2
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");  //품목분류3

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

            //

            //Repository 생성 or 이벤트 입력
            gv.Columns["ItemName"].ColumnEdit = lup_Item;

            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            //Column 단일 설정
            gv.Columns["SalesNo"].Visible = false;
            gv.Columns["SalesSeq"].Visible = false;
            gv.Columns["HouseCode"].Visible = false;

            //셀값 변경 허용
            if (this.cmdType == "I")
            {
                grid.Column_AllowEdit(gv.Columns["ItemName"]);
			}

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
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["UnitPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["VatPrice"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["TotalPrice"], clsGrid.TypeList.TFloat);

            //
            grid.SetCol_Summary(gv, "TotalPrice");

            //칼럼 너비 설정
            gv.Columns["Status"].MaxWidth = 60;

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
																	  //gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
        }
		private void Event_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            string fieldName = e.Column.FieldName;
            string value = e.Value.ConvertString_Null_To_Empty();

            if (fieldName == "ItemName" )
			{
                //품목 관련 정보 채우기
                grid.SetInfo_Item(dr, value);
			}
            else if(fieldName == "Qty" || fieldName == "UnitPrice")
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

            }

            gv.BestFitColumns();
        }

     

        private void SetData()
        {
            smtShipToCode.smtLookUpDataSource = code.smtLookUp("거래처", "매출", "");
            // 제품, 샘플/개발 품목만 조회되도록 필터링 
            smtItemCode.smtLookUpDataSource = code.GetMaterialList(ItemDiv);


            switch (cmdType)
			{
                case "I":
                    smtSalesDate.smtDateTimeValue = DateTime.Now;
                    smtExpirationDate.smtDateTimeValue = DateTime.Now;
                    break;

                case "U":
                    BM_SalesM sm = new BM_SalesM();
                    sm.SalesNo = this.SalesNo;

                    DataRow dr = sm.SelectBM_SalesM_BySalesNo();

                    if(dr != null)
					{
					    smtSalesDate.smtDateTimeValue = Convert.ToDateTime(dr["SalesDate"]);
                        smtExpirationDate.smtDateTimeValue = Convert.ToDateTime(dr["ExpirationDate"]);
					    smtShipToCode.smtLookUpValue = dr["ShipToCode"].ConvertString_Null_To_Empty();
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

            // 출고번호 채번 (신규일 때만)
            if (cmdType == "I")
            {
                this.SalesNo = "SP" + code.GetAutoNumber("샘플출고", smtSalesDate.smtDateTimeValue);
            }

            int qty = smtQty.smtTextBoxValue.ConvertInt_Null_To_Zero();

            // --- 마스터 바코드 채번 (SB = Sample Box) ---
            string masterBarCode = "SB" + code.GetAutoNumber("샘플박스바코드", smtSalesDate.smtDateTimeValue);

            // --- 마스터 INSERT ---
            BM_SampleOutM sm = new BM_SampleOutM();
            sm.CorpId = WrGlobal.CorpID;
            sm.OutNo = this.SalesNo;
            sm.OutSeq = 1;
            sm.ItemCode = smtItemCode.smtLookUpValue.ConvertString_Null_To_Empty();
            sm.UnitCode = new CM_Item().GetUnitCode(smtItemCode.smtLookUpValue.ConvertString_Null_To_Empty());
            sm.Qty = qty;
            sm.SdOrderNo = null;
            sm.SdOrderSeq = null;
            sm.BarCodeNo = masterBarCode;
            sm.OutDate = smtSalesDate.smtDateTimeValue.ToString("d");
            sm.ExpirationDate = smtExpirationDate.smtDateTimeValue.ToString("d");
            sm.ShipToCode = smtShipToCode.smtLookUpValue.ConvertString_Null_To_Empty();
            sm.Status = "대기";
            sm.Remark = smtRemark.smtMemoValue.ConvertString_Null_To_Empty();
            sm.CreId = WrGlobal.LoginID;

            if (!sm.Insert_BM_SampleOutM())
            {
                cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                return;
            }

            // --- 디테일 Qty개만큼 자동생성 (SD = Sample Detail) ---
            for (int i = 0; i < qty; i++)
            {
                string detailBarCode = "SD" + code.GetAutoNumber("샘플개별바코드", smtSalesDate.smtDateTimeValue);

                if (!sm.Insert_BM_SampleOutD(detailBarCode))
                {
                    cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                    return;
                }
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool Check_SaveClick()
        {
            //LookUp Validation 필수값 체크
            if (smtSalesDate.smtDateTimeValue == null)
            {
                MessageBox.Show("출하일자를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(smtExpirationDate.smtDateTimeValue == null)
            {
                MessageBox.Show("유효일자를 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (smtShipToCode.smtLookUpValue == "")
            {
                MessageBox.Show("납품처를 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (smtItemCode.smtLookUpValue == "")
            {
                MessageBox.Show("품목을 선택하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            int qty = smtQty.smtTextBoxValue.ConvertInt_Null_To_Zero();
            if (qty <= 0)  
            {
                MessageBox.Show("수량을 입력하세요.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
