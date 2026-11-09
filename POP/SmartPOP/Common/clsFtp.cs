using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Common
{
    public class clsFtp
    {
        private IFileInterface frm = null;

        long totalSize = 0;
        long totalCurrent = 0;

        public clsFtp()
        {

        }

        public clsFtp(IFileInterface frm)
        {
            this.frm = frm;
        }

        // EX: /ONE/TWO/THREE/FOUR -> 부모폴더로 가면서 폴더체크하여 생성
        public void MakeDirectory(string folderName)
        {

            if (!CheckDirectory(folderName))
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(WrGlobal.ftpIp + folderName);
                request.Credentials = new NetworkCredential(WrGlobal.ftpUser, WrGlobal.ftpPwd);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;

                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                try
                {
                    var resp = (FtpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    cls_MessageBox.Show(ex.Message);
                }
            }
        }

        private bool CheckDirectory(string folderName)
        {
            bool directoryExists;

            FtpWebRequest requestUpload = (FtpWebRequest)WebRequest.Create(WrGlobal.ftpIp + folderName + "/");
            requestUpload.Credentials = new NetworkCredential(WrGlobal.ftpUser, WrGlobal.ftpPwd);

            requestUpload.UsePassive = true;
            requestUpload.UseBinary = true;
            requestUpload.KeepAlive = false;

            var request = requestUpload;
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            try
            {
                using (var result = (FtpWebResponse)request.GetResponse())
                {
                    directoryExists = true;
                }
            }
            catch (WebException)
            {
                if (folderName != "")
                {
                    MakeDirectory(folderName.Substring(0, folderName.LastIndexOf("/")));
                    directoryExists = false;
                }
                else
                {
                    directoryExists = true;
                }

            }
            return directoryExists;
        }

        public void UploadSelectedFiles(string uploadPath, int fileSeq, string[] localFullNames)
        {
            try
            {
                totalSize = SelectedUploadSize(localFullNames);

                for (int i = 0; i < localFullNames.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(localFullNames[i]);

                    if (!fileInfo.Exists) return;
                    string uploadFile = string.Format("{0}{1}", fileSeq.ToString(), fileInfo.Extension);

                    FtpWebRequest upldrequest = (FtpWebRequest)WebRequest.Create(WrGlobal.ftpIp + uploadPath + uploadFile);
                    upldrequest.Credentials = new NetworkCredential(WrGlobal.ftpUser, WrGlobal.ftpPwd);
                    upldrequest.Method = WebRequestMethods.Ftp.UploadFile;

                    upldrequest.UsePassive = true;
                    upldrequest.UseBinary = true;
                    upldrequest.KeepAlive = false;


                    FileStream fileStream = fileInfo.OpenRead();

                    int bufferLength = 2048;
                    byte[] buffer = new byte[bufferLength];

                    Stream uploadStream = upldrequest.GetRequestStream();
                    int contentLength = fileStream.Read(buffer, 0, bufferLength);

                    long sendLength = 0;
                    int sendRate = 0;
                    int totalRate = 0;

                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = fileStream.Read(buffer, 0, bufferLength);

                        sendLength += contentLength;
                        sendRate = (int)(sendLength * 1.0 / fileStream.Length * 100);

                        totalCurrent += contentLength;
                        totalRate = (int)(totalCurrent * 100 / totalSize);
                        frm.SetData(fileInfo.Name, sendLength, fileStream.Length, sendRate, totalCurrent, totalSize, totalRate);
                    }
                    totalRate = (int)(totalCurrent * 100 / totalSize);
                    frm.SetData(fileInfo.Name, sendLength, fileStream.Length, 100, totalCurrent, totalSize, totalRate);

                    uploadStream.Close();
                    uploadStream.Flush();
                    fileStream.Close();

                    upldrequest = null;

                    frm.InsertDB(fileSeq, fileInfo.Name.Replace(fileInfo.Extension, ""), fileInfo.Name, uploadFile, fileInfo.Length, fileInfo.Extension);

                    fileSeq++;
                }
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "업로드", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UploadOriginalFiles(string uploadPath, int fileSeq, string[] localFullNames)
        {
            try
            {
                totalSize = SelectedUploadSize(localFullNames);

                for (int i = 0; i < localFullNames.Length; i++)
                {
                    FileInfo fileInfo = new FileInfo(localFullNames[i]);

                    if (!fileInfo.Exists) return;
                    string uploadFile = fileInfo.Name;

                    FtpWebRequest upldrequest = (FtpWebRequest)WebRequest.Create(WrGlobal.ftpIp + uploadPath + uploadFile);
                    upldrequest.Credentials = new NetworkCredential(WrGlobal.ftpUser, WrGlobal.ftpPwd);
                    upldrequest.Method = WebRequestMethods.Ftp.UploadFile;

                    upldrequest.UsePassive = true;
                    upldrequest.UseBinary = true;
                    upldrequest.KeepAlive = false;

                    FileStream fileStream = fileInfo.OpenRead();

                    int bufferLength = 2048;
                    byte[] buffer = new byte[bufferLength];

                    Stream uploadStream = upldrequest.GetRequestStream();
                    int contentLength = fileStream.Read(buffer, 0, bufferLength);

                    long sendLength = 0;
                    int sendRate = 0;
                    int totalRate = 0;

                    while (contentLength != 0)
                    {
                        uploadStream.Write(buffer, 0, contentLength);
                        contentLength = fileStream.Read(buffer, 0, bufferLength);

                        sendLength += contentLength;
                        sendRate = (int)(sendLength * 1.0 / fileStream.Length * 100);

                        totalCurrent += contentLength;
                        totalRate = (int)(totalCurrent * 100 / totalSize);
                        frm.SetData(fileInfo.Name, sendLength, fileStream.Length, sendRate, totalCurrent, totalSize, totalRate);
                    }
                    totalRate = (int)(totalCurrent * 100 / totalSize);
                    frm.SetData(fileInfo.Name, sendLength, fileStream.Length, 100, totalCurrent, totalSize, totalRate);

                    uploadStream.Close();
                    uploadStream.Flush();
                    fileStream.Close();

                    upldrequest = null;

                    frm.InsertDB(fileSeq, fileInfo.Name.Replace(fileInfo.Extension, ""), fileInfo.Name, uploadFile, fileInfo.Length, fileInfo.Extension);

                    fileSeq++;
                }
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "업로드", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public long DownloadFile(string ftpPath, string localPath, string orifilename, string filename)
        {
            long fileSize = 0;

            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential(WrGlobal.ftpUser, WrGlobal.ftpPwd);

                byte[] fileData = request.DownloadData(ftpPath + filename);

                using (FileStream file = File.Create(localPath + @"\" + orifilename))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }

                fileSize = (long)fileData.Length;
            }

            return fileSize;
        }

        public bool DeleteFile(string pathFilename)
        {
            bool bRet = false;

            try
            {
                Uri sourceFileUri = new Uri(WrGlobal.ftpIp + pathFilename);
                FtpWebRequest ftpWebRequest = WebRequest.Create(sourceFileUri) as FtpWebRequest;
                ftpWebRequest.Credentials = new NetworkCredential(WrGlobal.ftpUser, WrGlobal.ftpPwd);
                ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse ftpWebResponse = ftpWebRequest.GetResponse() as FtpWebResponse;
                ftpWebResponse.Close();

                bRet = true;
            }
            catch (Exception ex)
            {
                bRet = false;
                cls_MessageBox.Show(ex.Message, "파일삭제", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bRet;
        }

        // 선택된 파일들의 총 용량
        public long SelectedUploadSize(string[] fullFiles)
        {
            long size = 0;

            foreach (string fullFile in fullFiles)
            {
                size = size + GetFileSize(fullFile);
            }

            return size;
        }
        // 하나의 파일 용량
        public long GetFileSize(string filePath)
        {
            return new FileInfo(filePath).Length;
        }

    }
}
