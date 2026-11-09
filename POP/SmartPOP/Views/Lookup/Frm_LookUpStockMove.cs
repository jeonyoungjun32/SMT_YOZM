using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartPOP.Common;
using SmartPOP.Models;
using SMT_MES.Controls;

namespace SmartPOP.View.Lookup
{
    public partial class Frm_LookUpStockMove : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();
        string query = "";

        private string warehouse;
        private string productCode;
        private string productName;
        private int length;
        private int width;
        private int qtyNum;
        private string cutSize;
        private int stockQty;
        private int DetailSeq;
        private string Status;

        public Frm_LookUpStockMove()
        {
            InitializeComponent();
        }

        public Frm_LookUpStockMove(string WarehouseCode, string ProductCode, string ProductName, int Lenght, int Width, int QtyNum, string CutSize, int StockQty,int DetailSeq, string Status)
        {
            InitializeComponent();

            this.warehouse = WarehouseCode;
            this.productCode = ProductCode;
            this.productName = ProductName;
            this.length = Lenght;
            this.width = Width;
            this.qtyNum = QtyNum;
            this.cutSize = CutSize;
            this.stockQty = StockQty;

            this.DetailSeq = DetailSeq;

            this.Status = Status;
        }

        private void frmLookUpStockBase_Load(object sender, EventArgs e)
        {
            Set_Lup();
        }

        private void Set_Lup()
        {
            LupWareType.smtLookUpDataSource = code.smtLookUp("창고정보", "", "");
            LupWareHouse.smtLookUpDataSource = code.smtLookUp("창고정보", "", "");

            LupWareType.smtLookUpValue = warehouse;
            txt_ProductCode.smtTextBoxValue = productCode;
            txt_ProductName.smtTextBoxValue = productName;
            txt_length.smtTextBoxValue = length.ToString();
            txt_Width.smtTextBoxValue = width.ToString();
            txt_QtyNum.smtTextBoxValue = qtyNum.ToString();
            txt_CutSize.smtTextBoxValue = cutSize;
            txt_Qty.smtTextBoxValue = stockQty.ToString();
            LupWareHouse.smtLookUpReadOnly = true;

            txt_StockQty.smtTextBoxValue = stockQty.ToString();
            memo_Reason.smtMemoValue = "";

            if (Status == "In")
            {
                LupWareHouse.smtLookUpValue = "S0003";
            }
            else
            {
                LupWareHouse.smtLookUpValue = "S0001";
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string pMemo_Reason = memo_Reason.smtMemoValue;
            string fromHouse = LupWareType.smtLookUpValue;
            string toHouse = LupWareHouse.smtLookUpValue;
            int qty = Convert.ToInt32(txt_StockQty.smtTextBoxValue);

            if (toHouse == "")
            {
                cls_MessageBox.Show("이동창고를 입력해 주십시오", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (fromHouse == toHouse)
            {
                cls_MessageBox.Show("현재창고와 다른 이동창고를 선택해주십시오.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (qty < 0)
            {
                cls_MessageBox.Show("이동 재고량은 0보다 커야 합니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            query = $@"exec Stock_MakeMoveAdjustment '{WrGlobal.CorpID}','이동','{fromHouse}','{toHouse}','{productCode}',{DetailSeq},{stockQty},{qty},'{pMemo_Reason}',{WrGlobal.LoginID}";

            if (!db.ExecuteQuery(query))
            {
                cls_MessageBox.Show("저장 중 오류가 발생했습니다");
                return;
            }

            cls_MessageBox.Show("창고이동이 정상적으로 완료되었습니다.");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
