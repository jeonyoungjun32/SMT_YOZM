using DevExpress.DataProcessing.InMemoryDataProcessor;
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

namespace SmartDesign.View.Project
{
    public partial class frmProjectModify : DevExpress.XtraEditors.XtraForm
    {
        string ProjectID;
        string Corp;
        string PjID;
        DataRowView master;
        public frmProjectModify()
        {
            InitializeComponent();
        }
        public frmProjectModify(string Corp, string ProjectID, string PjID)
        {
            this.Corp = Corp;
            this.PjID = PjID;
            this.ProjectID = ProjectID;

            InitializeComponent();
            if (PjID == "")
            {
                DataRowView drv = (DataRowView)projectDetailBindingSource.AddNew();
                drv["CorpID"] = Corp;
                drv["ProjectID"] = ProjectID;
                drv["PjID"] = PjID;
                drv["GroupLevel"] = groupLevelSpinEdit.Value;
                drv["GroupYn"] = groupYnTextEdit.Text;
                drv["Status"] = statusTextEdit.Text;
                projectDetailBindingSource.EndEdit();
            }
            else
            {
                projectDetailTableAdapter.FillByList(designDataSet.ProjectDetail, Corp, ProjectID, PjID);
            }

        }
        private void frmProjectModify_Load(object sender, EventArgs e)
        {

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("자료를 저장 하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
            {
                if (pjIDTextEdit.Text == "")
                {
                    MessageBox.Show("제품유형을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (titleTextEdit.Text == "")
                {
                    MessageBox.Show("내용을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (startDateDateEdit.Text == "")
                {
                    MessageBox.Show("시작일을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (endDateDateEdit.Text == "")
                {
                    MessageBox.Show("종료일을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                projectDetailBindingSource.EndEdit();
                projectDetailTableAdapter.Update(designDataSet.ProjectDetail);
                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();


            }//end if

        }


    }
}