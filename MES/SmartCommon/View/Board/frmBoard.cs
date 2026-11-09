using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartCommon.View.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.View.Board
{
    public partial class frmBoard : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";
        public frmBoard()
        {
            InitializeComponent();
        }


        private void frmBoardView_Load(object sender, EventArgs e)
        {
            query = "Select'ALL' ItemCode, '전체' Title union All select ItemCode, Title FROM CM_CateItem where CorpId = '{WrGlobal.CorpID}' and class = 'A70010'";
            gcClass.DataSource = db.GetDataView("구분", query);

            DisplayData();
        }


        private void DisplayData()
        {
            boardGridControl.DataSource = null;
            string classType = gvClass.GetFocusedRowCellDisplayText("ItemCode");
            query = $"exec Board_GetBoardList '{WrGlobal.CorpID}','20230101','99991230','{classType}'";
            boardGridControl.DataSource = db.GetDataView("리스트", query);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            GridView view = sender as GridView;
            if (view != null &&
                view.IsValidRowHandle(e.RowHandle))
            {
                if (e.Column.FieldName == "Gubun")
                {
                    string status = view.GetRowCellValue(e.RowHandle, "Gubun").ToString();
                    switch (status)
                    {
                        case "중요":
                            e.Appearance.Options.UseForeColor = true;
                            e.Appearance.BackColor = Color.Orange;
                            e.Appearance.ForeColor = Color.White;
                            break;
                    }//end
                }//end if

            }//end if


        }

        private void gvClass_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayData();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string corpId = gridView1.GetFocusedRowCellValue("CorpId")?.ToString() ?? "";
            string boardNo = gridView1.GetFocusedRowCellValue("BoardNo")?.ToString() ?? "";

            query = $"Select top 1 Body from Board Where CorpID = '{corpId}' and BoardNo = '{boardNo}'";
            DataRowView drv = db.GetDataRecord(query);
            if (drv != null)
                richEditControl1.RtfText = drv["Body"].ToString();
            else
                richEditControl1.RtfText = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDocumentEdit form = new frmDocumentEdit(richEditControl1.RtfText);
            form.ShowDialog();
        }
    }
}