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
using System.IO;
using System.Threading;
using SmartCommon.Common;

namespace SmartCommon.Common
{
    public partial class FtpFileDownloader : DevExpress.XtraEditors.XtraForm
    {
        string downFolder = "";
        string[] filePaths = null;
        string[] oriFileNames = null;
        string[] fileNames = null;
        long totalSize = 0;

        public FtpFileDownloader()
        {
            InitializeComponent();
        }

        public FtpFileDownloader(string _downFolder, string[] _filePaths, string[] _oriFileNames, string[] _fileNames, long _totalSize)
        {
            InitializeComponent();

            downFolder = _downFolder;
            filePaths = _filePaths;
            oriFileNames = _oriFileNames;
            fileNames = _fileNames;
            totalSize = _totalSize;
        }

        private void FileDownloader_Shown(object sender, EventArgs e)
        {
            long currentSize = 0;
            int downRate = 0;

            clsFtp ftp = new clsFtp();

            for (int i = 0; i < fileNames.Length; i++)
            {

                oriFileNames[i] = GetNotDuplicateFileName(oriFileNames[i]);

                string ftpPath = WrGlobal.ftpIp + filePaths[i];

                long fileSize = ftp.DownloadFile(ftpPath, downFolder, oriFileNames[i], fileNames[i]);

                currentSize = currentSize + fileSize;

                downRate = (int)(currentSize * 1.0 / totalSize * 100);

                SetData(oriFileNames[i], 0, 0, 0, currentSize, totalSize, downRate);
            }

            SetData("", 0, 0, 100, 0, 0, 100);

            FormClose();
        }

        public void SetData(string fileName, long fileCurrent, long fileSize, int currentLength, long totalCurrent, long totalSize, int TotalRate)
        {
            Application.DoEvents();
            lblFName.Text = fileName;

            lblFolderLen.Text = string.Format("{0} / {1}", clsFunction.GetFileSize(totalCurrent), clsFunction.GetFileSize(totalSize));
            pBar2.EditValue = (int)TotalRate;

        }

        public void FormClose()
        {
            Application.DoEvents();
            lblMsg.Text = "작업이 완료되었습니다...";
            Thread.Sleep(1000);
            this.Close();
        }

        private string GetNotDuplicateFileName(string oriFileName)
        {
            if (File.Exists(downFolder + "/" + oriFileName))
            {
                int seq = 0;

                string name = oriFileName.Substring(0, oriFileName.LastIndexOf("."));
                string ext = oriFileName.Substring(oriFileName.LastIndexOf("."));
                oriFileName = name + "_" + seq.ToString() + ext;

                while (File.Exists(downFolder + "/" + oriFileName))
                {
                    seq = seq + 1;
                    oriFileName = name + "_" + seq.ToString() + ext;
                }
            }

            return oriFileName;
        }
    }
}