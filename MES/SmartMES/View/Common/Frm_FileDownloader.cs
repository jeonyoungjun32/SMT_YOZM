using SmartCommon.Common;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SmartMES.Common
{
	public partial class FileDownloader : DevExpress.XtraEditors.XtraForm
    {
        string path = "";
        string fileName = "";
        long fileSize = 0;
        string toPath = "";
        string toFileName = "";

        public FileDownloader()
        {
            InitializeComponent();
        }

        public FileDownloader(string _path, string _fileName, long _fileSize, string _toPath, string _toFileName)
        {
            InitializeComponent();

            path = _path;
            fileName = _fileName;
            fileSize = _fileSize;
            toPath = _toPath;
            toFileName = _toFileName;

            SetData(toFileName, 0, fileSize);
        }

        private void FileDownloader_Shown(object sender, EventArgs e)
        {
            File.Copy(WrGlobal.m_NetworkDrivePath + path + "\\" + fileName, toPath + "\\" + toFileName, true);
            SetData(toFileName, fileSize, fileSize);

            FormClose();
        }

        public void SetData(string fileName, long totalCurrent, long totalSize)
        {
            Application.DoEvents();
            lblFName.Text = fileName;

            lblFolderLen.Text = string.Format("{0} / {1}", clsFunction.GetFileSize(totalCurrent), clsFunction.GetFileSize(totalSize));
            pBar2.EditValue = (int)(totalCurrent * 1.0 / totalSize * 100);

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