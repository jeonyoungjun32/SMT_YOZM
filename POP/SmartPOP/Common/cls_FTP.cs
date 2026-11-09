using SmartPOP.Common;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES.Common
{
	public class cls_FTP
	{
        long totalCurrent = 0;

        // EX: /ONE/TWO/THREE/FOUR -> 부모폴더로 가면서 폴더체크하여 생성
        public void MakeDirectory(string folderName)
        {

            if (!CheckDirectory(folderName))
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(WrGlobal.m_ftpServerip + folderName);
                request.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);
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

            FtpWebRequest requestUpload = (FtpWebRequest)WebRequest.Create(WrGlobal.m_ftpServerip + folderName);
            requestUpload.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);

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

        /*  public bool UploadFile(string sourceFolder, string serverFolder, string fileName)
          {
              bool bRet = false;

              try
              {
                  FtpWebRequest request = (FtpWebRequest)WebRequest.Create(WrGlobal.m_ftpServerip + serverFolder + "/" + fileName);
                  request.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);
                  request.Method = WebRequestMethods.Ftp.UploadFile;

                  FileInfo fileInfo = new FileInfo(sourceFolder + @"\" + fileName);
                  FileStream fileStream = fileInfo.OpenRead();

                  int bufferLength = 2048;
                  byte[] buffer = new byte[bufferLength];

                  Stream uploadStream = request.GetRequestStream();
                  int contentLength = fileStream.Read(buffer, 0, bufferLength);

                  long sendLength = 0;
                  int sendRate = 0;

                  while (contentLength != 0)
                  {
                      uploadStream.Write(buffer, 0, contentLength);
                      contentLength = fileStream.Read(buffer, 0, bufferLength);

                      sendLength += contentLength;
                      sendRate = (int)(sendLength * 1.0 / fileStream.Length * 100);

                      totalCurrent += contentLength;
                      frm.SetData(fileInfo.Name, sendLength, fileStream.Length, sendRate, totalCurrent, 0, 0);
                  }

                  frm.SetData(fileInfo.Name, sendLength, fileStream.Length, 100, totalCurrent, 0, 0);

                  uploadStream.Close();
                  uploadStream.Dispose();
                  fileStream.Close();
                  fileStream.Dispose();

                  request = null;

                  bRet = true;
              }
              catch
              {
                  bRet = false;
              }

              return bRet;
          }*/

        public long DownloadFile(string serverFolder, string localPath, string fileName)
        {
            long fileSize = 0;

            using (WebClient request = new WebClient())
            {
                request.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);

                byte[] fileData = request.DownloadData(WrGlobal.m_ftpServerip + serverFolder + "/" + fileName);

                using (FileStream file = File.Create(localPath + @"\" + fileName))
                {
                    file.Write(fileData, 0, fileData.Length);
                    file.Close();
                }

                fileSize = (long)fileData.Length;
            }

            return fileSize;
        }

        public bool DeleteFile(string fullname)
        {
            bool bRet = false;

            try
            {
                Uri sourceFileUri = new Uri(WrGlobal.m_ftpServerip + fullname);
                FtpWebRequest ftpWebRequest = WebRequest.Create(sourceFileUri) as FtpWebRequest;
                ftpWebRequest.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);
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

        // ftp 폴더 전체 삭제
        public void DeleteDirectory(string folder)
        {
            Uri sourceFileUri = new Uri(WrGlobal.m_ftpServerip + folder);

            FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(sourceFileUri);
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);

            List<string> lines = new List<string>();

            using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (StreamReader listReader = new StreamReader(listStream))
            {
                while (!listReader.EndOfStream)
                {
                    lines.Add(listReader.ReadLine());
                }
            }

            foreach (string line in lines)
            {
                string[] tokens =
                  line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[3];
                string permissions = tokens[2];

                string fileUrl = sourceFileUri + "/" + name;

                if (permissions == "<DIR>")
                {
                    DeleteDirectory(folder + "/" + name);
                }
                else
                {
                    FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                    deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                    deleteRequest.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);

                    deleteRequest.GetResponse();
                }
            }

            FtpWebRequest removeRequest = (FtpWebRequest)WebRequest.Create(sourceFileUri);
            removeRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
            removeRequest.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);

            removeRequest.GetResponse();
        }

        public bool FtpFileExists(string serverFilePath, String FileName)
        {
            bool IsExists = true;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverFilePath);

                if (request == null)
                {
                    cls_MessageBox.Show("서버연결에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                request.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);
                request.Method = WebRequestMethods.Ftp.GetFileSize;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;

                if (response.StatusCode == System.Net.FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    IsExists = false;
                }
            }
            catch
            {
                IsExists = false;
            }

            return IsExists;
        }

        public FileInfo UploadFile(string serverFolder, string localPath, string fileName)
        {
            try
            {
                string serverFilePath = serverFolder + "/" + fileName;
                string localFileFullPath = localPath + "\\" + fileName;

                MakeDirectory("/" + WrGlobal.m_ftpFolderPath);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverFilePath);

                if (request == null)
                {
                    cls_MessageBox.Show("서버연결에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                request.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                FileInfo file = new FileInfo(localFileFullPath);
                FileStream fileStream = file.OpenRead();

                request.ContentLength = file.Length;

                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                using (FileStream fs = file.OpenRead())
                {
                    using (Stream strm = request.GetRequestStream())
                    {
                        contentLen = fs.Read(buff, 0, buffLength);

                        while (contentLen != 0)
                        {
                            strm.Write(buff, 0, contentLen);
                            contentLen = fs.Read(buff, 0, buffLength);
                        }
                    }
                    fs.Flush();
                    fs.Close();
                }

                if (buff != null)
                {
                    Array.Clear(buff, 0, buff.Length);
                    buff = null;
                }

                return file;
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "업로드 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public Stream DownloadFile(string path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);

                if (request == null)
                {
                    cls_MessageBox.Show("서버연결에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                request.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Timeout = 5000;

                FtpWebResponse response = request.GetResponse() as FtpWebResponse;

                Stream stream = response.GetResponseStream();

                MemoryStream mStream = new MemoryStream();
                stream.CopyTo(mStream);

                Stream newStream = new MemoryStream(mStream.ToArray());

                return newStream;
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message, "다운로드 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool DeletePDF(string fullname)
        {
            bool bRet = false;

            try
            {
                FtpWebRequest ftpWebRequest = WebRequest.Create(fullname) as FtpWebRequest;

                if (ftpWebRequest == null)
                    return bRet;

                ftpWebRequest.Credentials = new NetworkCredential(WrGlobal.m_ftpServerUser, WrGlobal.m_ftpServerPwd);
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
    }
}
