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
using System.Threading;
using System.IO;
using SmartPOP.Common;

namespace SmartPOP.Views.Common
{
    public partial class Frm_FtpFilesUploader : DevExpress.XtraEditors.XtraForm, IFileInterface
    {
        string[] fromFullFilenames;    // 경로포함한 파일명
        string[] keyArr = null;
        string corpId = "";

        string uploadPath = "";
        string uploadTable = "";
        int fileSeq = 0;

        Database db = new Database();
        string query;

    
        public Frm_FtpFilesUploader()
        {
            InitializeComponent();
        }

        public bool UploadSuccesss { get; private set; } = false;

        public Frm_FtpFilesUploader(string _uploadTable, int _fileSeq, string _keyNo, string[] _fromFullFilenames)
        {
            InitializeComponent();

            corpId = WrGlobal.CorpID;
            uploadTable = _uploadTable;
            fileSeq = _fileSeq;
            fromFullFilenames = _fromFullFilenames;
            keyArr = _keyNo.Split(new char[] { '|' });

        }

        public void SetData(string fileName, long fileCurrent, long fileSize, int currentLength, long totalCurrent, long totalSize, int TotalRate)
        {
            Application.DoEvents();
            pBar1.EditValue = currentLength;
            lblFName.Text = fileName;

            lblFileLeng.Text = string.Format("{0} / {1}", clsFunction.GetFileSize(fileCurrent), clsFunction.GetFileSize(fileSize));
            lblFolderLen.Text = string.Format("{0} / {1}", clsFunction.GetFileSize(totalCurrent), clsFunction.GetFileSize(totalSize));
            pBar2.EditValue = (int)TotalRate;
        }

        public void InsertDB(int fileSeq, string title, string oriFileName, string fileName, long capacity, string fileExt)
        {
            if (uploadTable == "CodeDocumentFile")
            {
                query = "Insert Into CodeDocumentFile(CorpId, Gubun, KeyCode1, KeyCode2, KeyCode3, FileSeq"
                                        + ", ItemCode, Title, OriFileName, FileName, Path"
                                        + ", Capacity, FileExt, CreId, CreIP, CreDt) Values("
                                + $"'{WrGlobal.CorpID}', '{keyArr[0]}', '{keyArr[1]}', '{(keyArr.Length > 2 ? keyArr[2] : "")}', '{(keyArr.Length > 3 ? keyArr[3] : "")}', {fileSeq}"
                                + $", '', '{title}', '{oriFileName}', '{fileName}', '{uploadPath}'"
                                + $", {capacity.ToString()}, '{fileExt}', '{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}', getdate())";
            }
            else if (uploadTable == "DocumentCommonFile")
            {
                query = "Insert Into DocumentCommonFile(CorpId, ItemCode, FileSeq, Title, OriFileName, FileName, Path"
                                        + ", Capacity, FileExt, CreId, CreIP, CreDt) Values("
                                + $"'{WrGlobal.CorpID}', '{keyArr[0]}', {fileSeq}"
                                + $", '{title}', '{oriFileName}', '{fileName}', '{uploadPath}'"
                                + $", {capacity.ToString()}, '{fileExt}', '{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}', getdate())";
            }
            if (query != "")
            {
                db.ExecuteQuery(query);
            }

            //switch (uploadTable) 
            //{
            //    case "CmDocument":
            //        query = string.Format("Insert Into {0} (PARTID, FileSeq, ItemCode, Title, OriFileName, FileName, Path, Capacity, FileExt, CREATEUSR, CREATEDT) "
            //                        + "Values('{1}', {2}, '{3}', N'{4}', N'{5}', N'{6}', '{7}', {8}, '{9}', '{10}', getdate())", 
            //                uploadTable, keyArr[0], fileSeq, keyArr[1], title, oriFileName, fileName, uploadPath, capacity.ToString(), fileExt, WrGlobal.LoginID);
            //        db.ExecuteQuery(query);
            //        break;
            //}
        }

        private void FilesUploader_Shown(object sender, EventArgs e)
        {
            clsFtp ftp = new clsFtp(this as IFileInterface);

            uploadPath = "/" + corpId + "/" + uploadTable;

            foreach (string keyStr in keyArr)
            {
                uploadPath = uploadPath + "/" + keyStr;
            }

            ftp.MakeDirectory(uploadPath);

            uploadPath = uploadPath + "/";

            if (uploadTable == "")
            {
                ftp.UploadOriginalFiles(uploadPath, fileSeq, fromFullFilenames);
            }
            else
            {
                ftp.UploadSelectedFiles(uploadPath, fileSeq, fromFullFilenames);
            }
            UploadSuccesss = true;

            FormClose();
        }

        /* FilesUploader_Shown 원본 
         * uploadPath = "/" + uploadTable;
            
            if (uploadTable == "CmDocument")
            {
                uploadPath = uploadPath + "/" + keyArr[0];
            }
            else
            {
                foreach (string keyStr in keyArr)
                {
                    uploadPath = uploadPath + "/" + keyStr;
                }
            }

            ftp.MakeDirectory(uploadPath);

            uploadPath = uploadPath + "/";

            ftp.UploadSelectedFiles(uploadPath, fileSeq, fromFullFilenames);

            FormClose();
         */

        private int GetNextFileSeq(string tableName, string filter)
        {
            query = string.Format("Select isnull(max(FileSeq), 0) + 1 seq From {0} Where {1}", tableName, filter);
            DataRowView drv = db.GetDataRecord(query);

            return (int)drv[0];
        }

        public void FormClose()
        {
            Application.DoEvents();
            lblMsg.Text = "작업이 완료되었습니다...";
            Thread.Sleep(1000);
            this.Close();
        }
    }
}