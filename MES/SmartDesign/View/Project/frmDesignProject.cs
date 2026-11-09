using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartCommon.Common;




namespace SmartDesign.View.Project
{
    public partial class frmDesignProject : DevExpress.XtraEditors.XtraForm
    {
        string query = "";
        string corp = WrGlobal.CorpID;
        Database db = new Database();

        public frmDesignProject()
        {
            InitializeComponent();
        }

        private void frmDesignProject_Load(object sender, EventArgs e)
        {
            DateTime startdate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01")); ;
            DateTime enddate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));

            dateStartDate.EditValue = startdate;
            dateEndDate.EditValue = enddate;

            DisplayData();

        }
        private void DisplayData()
        {
            try
            {
                this.projectMasterTableAdapter.FillByList(this.designDataSet.ProjectMaster, corp, "All", (DateTime)dateStartDate.EditValue, (DateTime)dateEndDate.EditValue, WrGlobal.LoginID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {

            try
            {
                projectMasterBindingSource.AddNew();
                DataRowView newItem = (DataRowView)projectMasterBindingSource.Current;

                newItem["CorpID"] = WrGlobal.CorpID;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("자료를 저장 하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
                {
                    if (gvSchedule.GetFocusedRowCellValue("Type").ToString() == "")
                    {
                        MessageBox.Show("항목을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (gvSchedule.GetFocusedRowCellValue("ProjectName").ToString() == "")
                    {
                        MessageBox.Show("프로젝트명을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (gvSchedule.GetFocusedRowCellValue("StartDate").ToString() == "")
                    {
                        MessageBox.Show("시작일을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (gvSchedule.GetFocusedRowCellValue("EndDate").ToString() == "")
                    {
                        MessageBox.Show("종료일을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (gvSchedule.GetFocusedRowCellValue("Charge").ToString() == "")
                    {
                        MessageBox.Show("담당자를 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.Validate();
                    projectMasterBindingSource.EndEdit();
                    this.projectMasterTableAdapter.Update(this.designDataSet.ProjectMaster);
                    MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }//end if

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 자료를 삭제 하시겠습나까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }//end if
            try
            {

                projectMasterBindingSource.RemoveCurrent();
                projectMasterTableAdapter.Update(designDataSet.ProjectMaster);
                MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmWorkOrderExcel form = new frmWorkOrderExcel();
            form.ShowDialog();
        }
    }
}