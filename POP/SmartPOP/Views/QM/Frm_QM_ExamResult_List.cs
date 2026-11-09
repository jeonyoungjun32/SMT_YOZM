using DevExpress.XtraEditors;
using SmartPOP.Common;
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

namespace SmartPOP.Views.QM
{
    public partial class Frm_QM_ExamResult_List : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        cls_Repository rpo = new cls_Repository();
        string query = "";

        public Frm_QM_ExamResult_List()
        {
            InitializeComponent();

            btnInsert.Visible = WrGlobal.GAM_SW;
            btnUpdate.Visible = WrGlobal.GAM_SW;
            btnDelete.Visible = WrGlobal.GAM_SW;
        }

        private void frmRequestMaterial_Load(object sender, EventArgs e)
        {
            dateStart.EditValue = DateTime.Now.Date.ToString("yyyy-MM-01");
            dateEnd.EditValue = DateTime.Now.Date.ToString("yyyy-MM-dd");

            rpo.Set_POP_Dtp(dateStart);
            rpo.Set_POP_Dtp(dateEnd);

            DisplayPM_ProductPlan();
            DisplayBOMMaterial();
        }

        private void DisplayPM_ProductPlan()     // 작업지시 불러오기
        {       
            query = $@"Exec POP_GetPM_ProductPlanList '{WrGlobal.CorpID}', '{dateStart.EditValue:yyyyMMdd}', '{dateEnd.EditValue:yyyyMMdd}'";

            gcPM_ProductPlan.DataSource = db.GetDataView("작업지시내역", query);
            gvPM_ProductPlan.BestFitColumns();
        }

        private void DisplayBOMMaterial()   // Route에 있는 자재리스트 가져오기 
        {
            if (gvPM_ProductPlan.DataRowCount == 0) return;

            DataRow dr = gvPM_ProductPlan.GetFocusedDataRow();

            string orderNo = dr["OrderNo"].ToString();
            string productCode = dr["MaterialCode"].ToString();
            string revision = dr["Revision"].ToString();

            query = $@"Exec POP_MakeRequestStockMaterial '{WrGlobal.CorpID}', '{orderNo}', '{productCode}', '{revision}'";
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvPM_ProductPlan_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayBOMMaterial();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayPM_ProductPlan();
            DisplayBOMMaterial();
        }

        private void cmdChoice_Click(object sender, EventArgs e)
        {
           
            cls_MessageBox.Show("출고 요청이 등록되었습니다.");
            DisplayBOMMaterial();
        }

        private void gvMaterial_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
          
        }

        private void gvMaterial_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
           
        }
    }
}