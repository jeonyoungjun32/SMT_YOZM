using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SmartPOP.Models;
using SmartPOP.Common;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using SMT_MES.View.Material.Rpt;

namespace SmartPOP.Views.MM
{
    public partial class Frm_MM_Purchase_Print : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        cls_Repository rpo = new cls_Repository();
        cls_Grid grid = new cls_Grid();

        Database db = new Database();
        string qurey = "";

        public Frm_MM_Purchase_Print()
        {
            InitializeComponent();
        }

        private void frmPurchasePrintLabel_Load(object sender, EventArgs e)
        {
            Set_GvIn(gvDetail);

            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(DateTime.Now.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            barEditStartDate.EditValue = startDate;
            barEditEndDate.EditValue = endDate;

            //달력 크기 키우기
            rpo.Set_POP_Dtp(barEditStartDate);
            rpo.Set_POP_Dtp(barEditEndDate);

            customerBox.Properties.DataSource = code.GetCustomer(WrGlobal.CorpID, "10", true);
            customerBox.Properties.DisplayMember = "내용";
            customerBox.Properties.ValueMember = "코드";
            customerBox.EditValue = "All";

            customerBox.Properties.ShowClearButton = false;
            customerBox.Properties.View.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            customerBox.Properties.View.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정

            DisplayData();
        }

        private void Set_GvIn(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            //Repository

            //Column 셋팅
            grid.Create_GvColumn(gv, "입고번호", "StoNo");
            grid.Create_GvColumn(gv, "입고순번", "StoSeq");
            grid.Create_GvColumn(gv, "입고일자", "StoDate");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "입고량", "Qty");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "LotNo", "LotNo");

            //grid.Create_GvColumn(gv, "등록자", "Ins_Emp_No");
            //grid.Create_GvColumn(gv, "등록일시", "Ins_Date_Time");
            //grid.Create_GvColumn(gv, "수정자", "Upd_Emp_No");
            //grid.Create_GvColumn(gv, "수정일시", "Upd_Date_Time");

            //셀값 변경 허용
            gv.Columns["Remark"].MinWidth = 200;

            grid.SetCol_Type(gv.Columns["StoDate"], cls_Grid.TypeList.TDate);     //일고일자
            grid.SetCol_Type(gv.Columns["Qty"], cls_Grid.TypeList.TFloat);    //발주수량

            //grid.SetInfo_ByObject(gv);
            grid.SetPOPGrid(gv);
            grid.RowNumber_Indicator(gv);
        }

        private void DisplayData()
        {
            gvDetail.FocusInvalidRow();

			string startDate = barEditStartDate.DateTime.ToString("d");
			string endDate = barEditEndDate.DateTime.ToString("d");
            string custCode = Convert.ToString(customerBox.EditValue);

            qurey = $@"
                        SELECT 
                         pd.StoNo
                        , pd.StoSeq
                        , pd.Qty
                        , pd.LotNo
                        , pm.StoDate
                        , pm.CustCode
                        , cc.CustName
                        , pd.ItemCode
                        , cm.ItemName
                        , pd.Remark
                        FROM MM_PurchaseD pd
                        LEFT JOIN MM_PurchaseM pm ON pd.CorpId = pm.CorpId AND pd.StoNo = pm.StoNo
                        LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode
                        LEFT JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.ItemCode = cm.ItemCode
                        WHERE pd.CorpId = '{WrGlobal.CorpID}'
                        AND pm.StoDate BETWEEN '{startDate}' AND '{endDate}'
            ";

            if(custCode != "" && custCode.ToUpper() !="ALL")
			{
                qurey += $"\nAND pm.CustCode = '{custCode}'";
			}

            gcDetail.DataSource = db.GetDataTable(qurey);
            gvDetail.BestFitColumns();
        }

        //조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void cmdPrint_Click(object sender, EventArgs e)
		{
            GridView gv = gvDetail;

            DataTable dt = gv.GridControl.DataSource as DataTable;
            if (dt.Rows.Count <= 0) return;

            int[] selrow = gv.GetSelectedRows();

            if (selrow.Length == 0)
            {
                XtraMessageBox.Show("선택한 출고내역이 없습니다", "");
                return;
            }

            DataTable selDataTable = dt.Clone();
            for (int cnt = 0; cnt < gv.SelectedRowsCount; cnt++)
            {
                selDataTable.ImportRow(gv.GetDataRow(selrow[cnt]));
            }

            Rpt_Injection report = new Rpt_Injection(selDataTable);
            ReportPrintTool reportTool = new ReportPrintTool(report);
            reportTool.PrinterSettings.Collate = false;
            reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            reportTool.ShowPreviewDialog();
        }
	}
}