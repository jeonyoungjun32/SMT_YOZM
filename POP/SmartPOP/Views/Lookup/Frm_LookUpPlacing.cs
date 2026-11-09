using DevExpress.XtraEditors;
using SmartPOP.Common;
using SmartPOP.Models;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.View.Lookup
{
    public partial class Frm_LookUpPlacing : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        cls_Repository rpo = new cls_Repository();

        public delegate void SetData(DataRowView drv);
        public event SetData onSetData;

        public Frm_LookUpPlacing()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = System.Windows.Forms.Screen.GetBounds(MousePosition).Location;

            InitializeComponent();
        }

        public Frm_LookUpPlacing(string _table, string _custCode)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = System.Windows.Forms.Screen.GetBounds(MousePosition).Location;

            InitializeComponent();

            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(DateTime.Now.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            barEditStartDate.EditValue = startDate;
            barEditEndDate.EditValue = endDate;

            rpo.Set_POP_Dtp(barEditStartDate);
            rpo.Set_POP_Dtp(barEditEndDate);

            custCodeSearchLookUpEdit.Properties.DataSource = code.GetCustomer(WrGlobal.CorpID, "10");
            custCodeSearchLookUpEdit.Properties.DisplayMember = "내용";
            custCodeSearchLookUpEdit.Properties.ValueMember = "코드";

            custCodeSearchLookUpEdit.EditValue = _custCode;
            custCodeSearchLookUpEdit.ReadOnly = true;
        }

        private void frmLookUpPlacing_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            //this.placingDetailTableAdapter.FillBySelect(purchaseDataSet.PlacingDetail, WrGlobal.CorpID, custCodeSearchLookUpEdit.EditValue.ToString(), (DateTime)barEditStartDate.EditValue, (DateTime)barEditEndDate.EditValue);
        }

        private void cmdInsert_ElementClick(object sender, EventArgs e)
        {
            if (gvDetail.SelectedRowsCount < 1)
            {
                cls_MessageBox.Show("등록할 항목을 선택해 주세요.");
                return;
            }

            int rowIdx = 0;

            for (int rowCnt = 0; rowCnt < gvDetail.SelectedRowsCount; rowCnt++)
            {
                rowIdx = gvDetail.GetSelectedRows()[rowCnt];

                onSetData((DataRowView)gvDetail.GetRow(rowIdx));
            }
            this.Close();
        }

        private void cmdSelect_ElementClick(object sender,EventArgs e)
        {
            DisplayData();
        }

        private void gvDetail_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Add)
            {
                if (gvDetail.GetRowCellValue(e.ControllerRow, "StoQty") == DBNull.Value)
                {
                    gvDetail.SetRowCellValue(e.ControllerRow, "StoQty", gvDetail.GetRowCellValue(e.ControllerRow, "BalanceQty"));
                }
            }
        }
    }
}