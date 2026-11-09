using SmartCommon.Common;
using SmartMES.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.Lookup
{
	public partial class frmLookupGoods : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SetData(DataRowView drv);  //델리게이트 선언
        public event SetData onSetData;                 //계약

        string table = "", custCode = "", materialType = "";

        clsCode code = new clsCode();

        public frmLookupGoods()
        {

            InitializeComponent();

            DisplayInit();
        }
        public frmLookupGoods(string _table)
        {
            InitializeComponent();
            table = _table;
            DisplayInit();
        }

        public frmLookupGoods(string _table, string _custCode)
        {
            InitializeComponent();


            table = _table;
            custCode = _custCode;
            DisplayInit();

            //if (table != "SdOrderDetail")
            //{
            //    colInputQty.Visible = false;
            //}


            //DataView yearCodeView = code.GetCategory("A052");
            //yearCodeConCheckedComboBoxEdit.Properties.DataSource = yearCodeView;
            //yearCodeConCheckedComboBoxEdit.Properties.DisplayMember = "분류명";
            //yearCodeConCheckedComboBoxEdit.Properties.ValueMember = "분류코드";

            //code.LookupCategory(ref brandCodeConSearchLookUpEdit, "A051", true);         // 브랜드
            //brandCodeConSearchLookUpEdit.EditValue = "All";

            //code.LookupCategory(ref seasonCodeConLookUpEdit, "A053", true);              // 시즌
            //seasonCodeConLookUpEdit.EditValue = "All";

            //code.LookupCategory(ref materialTypeConLookUpEdit, "A010", false);       // 품목구분
            //materialTypeConLookUpEdit.EditValue = "20";

        }

        public frmLookupGoods(string _table, string _materialType, string _search)
        {
            InitializeComponent();
            table = _table;
            materialType = _materialType;
            DisplayInit();
        }
        private void DisplayInit()
        {
            code.LookupCategory(ref brandCodeConSearchLookUpEdit, "A051", true);         // 브랜드
            brandCodeConSearchLookUpEdit.EditValue = "All";

            DataView yearCodeView = code.GetCategory("A052");
            yearCodeConCheckedComboBoxEdit.Properties.DataSource = yearCodeView;
            yearCodeConCheckedComboBoxEdit.Properties.DisplayMember = "분류명";
            yearCodeConCheckedComboBoxEdit.Properties.ValueMember = "분류코드";

            code.LookupCategory(ref seasonCodeConLookUpEdit, "A053", true);              // 시즌
            seasonCodeConLookUpEdit.EditValue = "All";

            code.LookupCategory(ref materialTypeConLookUpEdit, "A010", false);       // 품목구분

            if (materialType == "부자재")
                materialTypeConLookUpEdit.EditValue = "50";
            else
                materialTypeConLookUpEdit.EditValue = "20";


            if (table == "SdOrderDetail")
            {
                colInputQty.Caption = "주문수량";
                colInputQty.Visible = false;
            }
            else if (table == "PlacingDetail")
            {
                colInputQty.Caption = "발주수량";
            }
            else if (table == "RequestPoDetail")
            {
                colInputQty.Caption = "요청수량";
            }
            else if (table == "AuditDetail")
            {
                colInputQty.Caption = "실사수량";
            }
            else
            {
                colInputQty.Visible = false;
            }

        }

        private void frmLookupGoods_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = System.Windows.Forms.Screen.GetBounds(MousePosition).Location;
        }

        // 선택 버튼
        private void cmdInsert_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            if (gvCmMaterial.SelectedRowsCount < 1)
            {
                MessageBox.Show("등록할 항목을 선택해 주세요.");
                return;
            }

            int rowIdx = 0;

            for (int rowCnt = 0; rowCnt < gvCmMaterial.SelectedRowsCount; rowCnt++)
            {
                rowIdx = gvCmMaterial.GetSelectedRows()[rowCnt];

                onSetData((DataRowView)gvCmMaterial.GetRow(rowIdx));
            }

            gvCmMaterial.ClearSelection();

            this.Close();
        }

        private void cmdClose_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {

            this.Close();
        }

        private void DisplayData()
        {
            try
            {
                this.cmMaterialTableAdapter.Fill(this.codeDataSet.CmMaterial, WrGlobal.PublicCorpID);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayMaterial()
        {
            if ((yearCodeConCheckedComboBoxEdit.EditValue ?? "").ToString() == ""
                && (brandCodeConSearchLookUpEdit.EditValue ?? "").ToString() == "All"
                && (seasonCodeConLookUpEdit.EditValue ?? "").ToString() == "All"
                && (materialTypeConLookUpEdit.EditValue ?? "").ToString() == "All"
                && txtMaterialCode.Text == "")
            {
                MessageBox.Show("검색 조건을 하나 이상 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                //250403 ㅁ시 주석
                //this.cmMaterialTableAdapter.FillBySearch(this.codeDataSet.CmMaterial, WrGlobal.CorpID
                //                                    , (yearCodeConCheckedComboBoxEdit.EditValue ?? "").ToString()
                //                                    , (brandCodeConSearchLookUpEdit.EditValue ?? "").ToString()
                //                                    , (seasonCodeConLookUpEdit.EditValue ?? "").ToString()
                //                                    , (materialTypeConLookUpEdit.EditValue ?? "").ToString()
                //                                    , txtMaterialCode.Text, "");

                gvCmMaterial.BestFitColumns();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdDisplay_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            DisplayMaterial();

            //if (table == "SdOrderDetail")
            //{
            //    DisplayProduct();
            //}
            //else if (table == "CmBomMaster")
            //{
            //    DisplayProduct();
            //}  
            //else
            //{
            //    DisplayData();
            //}
        }

        private void materialTypeConLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            switch (materialTypeConLookUpEdit.EditValue.ToString())
            {
                case "10":
                case "20":
                case "30":
                case "T20":
                    code.LookupCategory(ref brandCodeConSearchLookUpEdit, "A051", true);         // 브랜드
                    brandCodeConSearchLookUpEdit.EditValue = "All";

                    break;

                case "40":
                case "T40":
                    code.LookupCategory(ref brandCodeConSearchLookUpEdit, "A080", true);         // 원단브랜드
                    brandCodeConSearchLookUpEdit.EditValue = "All";
                    break;

                case "50":
                case "T50":
                    code.LookupCategory(ref brandCodeConSearchLookUpEdit, "A090", true);         // 부자재브랜드
                    brandCodeConSearchLookUpEdit.EditValue = "All";
                    break;

                default:
                    code.LookupCategory(ref brandCodeConSearchLookUpEdit, "A051", true);         // 브랜드
                    brandCodeConSearchLookUpEdit.EditValue = "All";
                    break;
            }
        }
    }
}