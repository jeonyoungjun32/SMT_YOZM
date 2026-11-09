using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SmartCommon.Common;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Models;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using SmartCommon.View.Common;

namespace SmartDesign.View.Project
{
    public partial class frmDesignProjectDetail : DevExpress.XtraEditors.XtraForm
    {
        string query = "";
        string corp = WrGlobal.CorpID;
        Database db = new Database();
        clsDocument doc = new clsDocument();

        public frmDesignProjectDetail()
        {
            InitializeComponent();

            itemCodeColLookUpEdit.DataSource = doc.GetFileItemCode("102020");
            itemCodeColLookUpEdit.ValueMember = "ItemCode";
            itemCodeColLookUpEdit.DisplayMember = "Title";
        }
        private void frmDesignProject_Load(object sender, EventArgs e)
        {

            DisplayData();
        }
        //ProjectMater 테이블 가져오기
        private void DisplayData()
        {
            try
            {
                this.projectMasterTableAdapter.Fill(this.designDataSet.ProjectMaster, corp);
            }
            catch (Exception)
            {

                throw;
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


        //ProjectDetail 테이블 가져오기 
        private void DisPlayDataDetail()
        {
            try
            {
                //ProjectDetail 테이블 생성
                string projectid = gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString();
                query = $"exec Project_MakeProjectSchedule '{corp}', '{projectid}'";
                //db.ExecuteQuery(query);
                //ProjectDetail 날짜 cell 추가
                query = $"exec Project_GetProjectSchedule '{corp}', '{projectid}'";
                gcProjectDetail.DataSource = db.GetDataView("프로젝트", query);
                gvProjectDetail.BestFitColumns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void gvProjectMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DisplayFile();
                DisPlayDataDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void gvProjectDetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            GridView view = sender as GridView;
            if (e.CellValue == null) return;


            string groupYn = gvProjectDetail.GetRowCellValue(e.RowHandle, "GroupYn").ToString();


            if (e.RowHandle >= 0)
            {
                if (groupYn == "Y" && e.CellValue.ToString() == "P")
                {
                    e.Appearance.BackColor = Color.FromArgb(150, 194, 145);
                }
                else if(e.CellValue.ToString() == "P")
                {
                    e.Appearance.BackColor = Color.FromArgb(244, 238, 238);
                }
                else if (e.CellValue.ToString() == "O")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 219, 170);
                }
                else if (e.CellValue.ToString() == "C")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 183, 183);
                }
                else
                {
                    return;
                }
            }

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                    
                    string projectid = gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString() ?? "";
                    string PjID = gvProjectDetail.GetFocusedRowCellValue("제품유형")?.ToString() ?? "";
                    frmProjectModify from = new frmProjectModify(corp, projectid, PjID);
                    from.ShowDialog();
                    DisplayFile();
                    DisPlayDataDetail();     
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
                int row = 0;
                for (int rowCnt = 0; rowCnt < gvProjectDetail.SelectedRowsCount; rowCnt++)
                {
                    if (gvProjectDetail.GetSelectedRows()[rowCnt] >= 0)
                    {
                        row = gvProjectDetail.GetSelectedRows()[rowCnt];

                        string projectid = gvProjectDetail.GetRowCellValue(row, "프로젝트ID")?.ToString() ?? "";
                        string pjid = gvProjectDetail.GetRowCellValue(row, "제품유형")?.ToString() ?? "";
                        query = String.Format($"exec Project_DeleteProjectSchedule '{corp}', '{projectid}',  '{pjid}' ");
                        db.ExecuteQuery(query);
                       
                    }
                }
                MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisPlayDataDetail();
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
            DisplayData();
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        { 
            string projectid = gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString() ?? "";        
            frmProjectModify  from = new frmProjectModify(corp, projectid, "" );
            from.ShowDialog();
            DisPlayDataDetail();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //string projectid = gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString() ?? "";
            //SmartMES.View.Placing.frmPlacingDetail form = new SmartMES.View.Placing.frmPlacingDetail("Insert", "", projectid);
            //form.ShowDialog();
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
            if (gvCodeDocumentFile.SelectedRowsCount < 0) return;

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
            }
        }

        private void btnFileSave_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            frmWorkOrderExcel form = new frmWorkOrderExcel();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisPlayDataDetail();

            
          
                //ProjectDetail 테이블 생성
                string projectid = gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString();
                query = $"exec Project_MakeProjectSchedule '{corp}', '{projectid}'";
                db.ExecuteQuery(query);

            
        }
    }
}