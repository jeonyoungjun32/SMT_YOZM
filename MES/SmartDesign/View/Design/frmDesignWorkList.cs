using SmartCommon.Common;
using SmartCommon.Models;
using SmartCommon.View.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace SmartDesign.View.Design
{
    public partial class frmDesignWorkList : DevExpress.XtraEditors.XtraForm
    {
        string query = "";
        string corp = WrGlobal.CorpID;
        Database db = new Database();
        clsDocument doc = new clsDocument();

        string projectType = "";

        public frmDesignWorkList()
        {
            InitializeComponent();

            itemCodeColLookUpEdit.DataSource = doc.GetFileItemCode("102020");
            itemCodeColLookUpEdit.ValueMember = "ItemCode";
            itemCodeColLookUpEdit.DisplayMember = "Title";
        }

        public frmDesignWorkList(string _projectType)
        {
            InitializeComponent();

            projectType = _projectType;

            itemCodeColLookUpEdit.DataSource = doc.GetFileItemCode("102020");
            itemCodeColLookUpEdit.ValueMember = "ItemCode";
            itemCodeColLookUpEdit.DisplayMember = "Title";
        }

        private void frmDesignProject_Load(object sender, EventArgs e)
        {

            query = $"select EmployeeId 사번, EmployeeName 이름 from CodeUser Where CorpID = '{WrGlobal.CorpID}'";
            DataView dvEmployee = db.GetDataView("직원", query);

            searchEmp1.DataSource = dvEmployee;
            searchEmp1.DisplayMember = "이름";
            searchEmp1.ValueMember = "사번";

            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-01-01"));
            DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-12-31"));
            startDate = startDate.AddYears(-1);


            dateStartDate.EditValue = startDate;
            dateEndDate.EditValue = endDate;
            DisplayData();
        }

        #region 조회

        //ProjectMater 테이블 가져오기
        private void DisplayData()
        {
            try
            {
                //this.projectMasterTableAdapter.FillByList(this.designDataSet.ProjectMaster, WrGlobal.CorpID, projectType, (DateTime)dateStartDate.EditValue, (DateTime)dateEndDate.EditValue, WrGlobal.LoginID);
                this.projectMasterTableAdapter.FillByList(this.designDataSet.ProjectMaster, WrGlobal.CorpID, projectType, (DateTime)dateStartDate.EditValue, (DateTime)dateEndDate.EditValue, "All");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // 파일 목록 가져오기
        private void DisplayFile()
        {
            try
            {
                this.codeDocumentFileTableAdapter.FillByKey(this.codeDataSet.CodeDocumentFile, WrGlobal.CorpID, "Project",
                                                    gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString(), "", "");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        #endregion
        private void gvProjectMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DisplayFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region  상단버튼

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #endregion



        #region ftp파일관리

        // 파일 업로드
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if ((gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString() ?? "") == "") return;

            try
            {
                string projectID = (gvProjectMaster.GetFocusedRowCellValue("ProjectID") ?? "").ToString();

                foDialog.FileName = "";

                if (foDialog.ShowDialog() == DialogResult.OK)
                {
                    if (foDialog.FileNames.Length > 0)
                    {
                        int fileSeq = doc.GetNextFileSeq("CodeDocumentFile", "Gubun = 'Project' and KeyCode1 = '" + projectID + "'");

                        FtpFilesUploader uploader = new FtpFilesUploader("CodeDocumentFile", fileSeq, "Project|" + projectID, foDialog.FileNames);
                        uploader.ShowDialog();

                        DisplayFile();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 파일다운로드 
        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (gvCodeDocumentFile.SelectedRowsCount < 1)
            {
                MessageBox.Show("선택된 자료가 없습니다.");
                return;
            }

            List<string> selPaths = new List<string>();
            List<string> selOriFiles = new List<string>();
            List<string> selFiles = new List<string>();

            int[] idxs = gvCodeDocumentFile.GetSelectedRows();
            long totalSize = 0;

            foreach (int idx in idxs)
            {
                selPaths.Add(gvCodeDocumentFile.GetRowCellValue(idx, "Path").ToString());
                selOriFiles.Add(gvCodeDocumentFile.GetRowCellValue(idx, "OriFileName").ToString());
                selFiles.Add(gvCodeDocumentFile.GetRowCellValue(idx, "FileName").ToString());
                totalSize += Convert.ToInt64(gvCodeDocumentFile.GetRowCellValue(idx, "Capacity").ToString());
            }

            //FtpFileDownloader downloader = new FtpFileDownloader(fbDialog.SelectedPath, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
            FtpFileDownloader downloader = new FtpFileDownloader(@"c:\Temp\", selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
            downloader.ShowDialog();

            spreadsheetControl1.LoadDocument(@"c:\temp\" + selOriFiles.ToArray()[0].ToString());
        }
        // 파일 삭제
        private void btnFileDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("선택한 파일을 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            DataRowView drv = (DataRowView)gvCodeDocumentFile.GetFocusedRow();
            clsFtp ftp = new clsFtp();

            if (ftp.DeleteFile(drv["Path"].ToString() + drv["FileName"].ToString()))
            {
                codeDocumentFileBindingSource.RemoveCurrent();
                this.codeDocumentFileTableAdapter.Update(this.codeDataSet.CodeDocumentFile);
            }
        }


        private void layoutCtlGrpFile_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                this.Validate();

                codeDocumentFileBindingSource.EndEdit();
                this.codeDocumentFileTableAdapter.Update(this.codeDataSet.CodeDocumentFile);

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 프로젝트

        private void layoutCtlGrpProject_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            //LayoutControlGroup layoutGroup = sender as LayoutControlGroup;
            ////if (e.Button.Properties.Caption == "Hide")
            ////    layoutGroup.HideToCustomization();

            //string projectid = gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString() ?? "";
            //frmProjectModify frm;
            //switch (e.Button.Properties.Caption)
            //{
            //    case "프로젝트추가":
            //        frm = new frmProjectModify(corp, projectid, "");
            //        frm.ShowDialog();
            //        DisPlayDataDetail();
            //        break;
            //    case "프로젝트수정":

            //        if (projectid == "")
            //        {
            //            MessageBox.Show("선택한 디자인프로젝트가 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        frm = new frmProjectModify(corp, projectid, projectid);
            //        frm.ShowDialog();
            //        DisplayFile();
            //        DisPlayDataDetail();
            //        break;
            //}

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
        #endregion


        private void btnUpload_Click_1(object sender, EventArgs e)
        {
            if ((gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString() ?? "") == "") return;

            try
            {
                string projectID = (gvProjectMaster.GetFocusedRowCellValue("ProjectID") ?? "").ToString();

                foDialog.FileName = "";

                if (foDialog.ShowDialog() == DialogResult.OK)
                {
                    if (foDialog.FileNames.Length > 0)
                    {
                        int fileSeq = doc.GetNextFileSeq("CodeDocumentFile", "Gubun = 'Project' and KeyCode1 = '" + projectID + "'");

                        FtpFilesUploader uploader = new FtpFilesUploader("CodeDocumentFile", fileSeq, "Project|" + projectID, foDialog.FileNames);
                        uploader.ShowDialog();

                        DisplayFile();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void productImagePictureEdit_DoubleClick(object sender, EventArgs e)
        {
            frmImageView form = new frmImageView(productImagePictureEdit.Image);
            form.ShowDialog();
        }

        private void productImagePictureEdit_EditValueChanged(object sender, EventArgs e)
        {

        }



        private void remarkTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gvCodeDocumentFile.SelectedRowsCount < 1)
            {
                MessageBox.Show("선택된 자료가 없습니다.");
                return;
            }


            if (fbDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> selPaths = new List<string>();
                List<string> selOriFiles = new List<string>();
                List<string> selFiles = new List<string>();

                int[] idxs = gvCodeDocumentFile.GetSelectedRows();
                long totalSize = 0;

                foreach (int idx in idxs)
                {
                    selPaths.Add(gvCodeDocumentFile.GetRowCellValue(idx, "Path").ToString());
                    selOriFiles.Add(gvCodeDocumentFile.GetRowCellValue(idx, "OriFileName").ToString());
                    selFiles.Add(gvCodeDocumentFile.GetRowCellValue(idx, "FileName").ToString());
                    totalSize += Convert.ToInt64(gvCodeDocumentFile.GetRowCellValue(idx, "Capacity").ToString());
                }

                FtpFileDownloader downloader = new FtpFileDownloader(fbDialog.SelectedPath, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
                downloader.ShowDialog();

                if (MessageBox.Show("선택한 자료를 열어 보시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                string fileName = fbDialog.SelectedPath + @"\" + selOriFiles[0].ToString();
                Process.Start(fileName);


            }
        }
    }
}