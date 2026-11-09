using DevExpress.XtraEditors;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SmartCommon.View.Board
{
    public partial class frmBoardView : DevExpress.XtraEditors.XtraForm
    {
        public frmBoardView()
        {
            InitializeComponent();
        }


        private void frmBoardView_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frmBoardDetail form = new frmBoardDetail("Insert", "");
            form.ShowDialog();
            DisplayData();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string boardNo = gridView1.GetFocusedRowCellDisplayText("BoardNo");
            frmBoardDetail form = new frmBoardDetail("Update", boardNo);
            form.ShowDialog();
            DisplayData();
        }

        private void DisplayData()
        {
            //this.boardTableAdapter.Fill(this.documentDataSet.Board,WrGlobal.CorpID);
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            //삭제
            string boardNo = gridView1.GetFocusedRowCellDisplayText("BoardNo");
            if (MessageBox.Show($"선택한 자료를 삭제 하시겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }//end if

            boardBindingSource.RemoveCurrent();
            //this.boardTableAdapter.Update(this.documentDataSet.Board);


        }
    }
}