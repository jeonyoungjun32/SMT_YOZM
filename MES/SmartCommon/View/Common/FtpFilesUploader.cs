using SmartCommon.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SmartCommon.View.Common
{
    public partial class FtpFilesUploader : DevExpress.XtraEditors.XtraForm, IFileInterface
    {
        string[] fromFullFilenames;    // 경로포함한 파일명
        string corpId = "";
        string[] keyArr = null;

        string uploadPath = "";
        string uploadTable = "";
        int fileSeq = 0;

        Database db = new Database();
        string query;

        public FtpFilesUploader()
        {
            InitializeComponent();
        }
        public bool UploadSuccesss { get; private set; } = false;
        bool shouldUpdate = false;

        public FtpFilesUploader(string _uploadTable, int _fileSeq, string _keyNo, string[] _fromFullFilenames)
        {
            InitializeComponent();

            corpId = WrGlobal.CorpID;
            uploadTable = _uploadTable;
            fileSeq = _fileSeq;
            fromFullFilenames = _fromFullFilenames; //
            keyArr = _keyNo.Split(new char[] { '|' });

            //string keyArrTo = keyArr[1];
            //InsertDB.MethodThatTakesAString(keyArrTo);

        }

        //이미지 파일을 바이트 배열로 변환
        private static byte[] GetImage(string fromFullFilenames)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fromFullFilenames);
                byte[] photo = new byte[fs.Length];
                int bytesRead = fs.Read(photo, 0, (int)fs.Length);

                return photo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("실패하였습니다.", "실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
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
            if (uploadTable == "PJ_ProjectDocumentFile")
            {
                query = "Insert Into PJ_ProjectDocumentFile(CorpId, Gubun, KeyCode1, KeyCode2, KeyCode3, FileSeq"
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
            else if (uploadTable == "CM_ItemImage")
            {
                byte[] photo = GetImage(fromFullFilenames[0]);
                SqlCommand cmd = new SqlCommand();
                //comm.Parameters.Add(new SqlParameter("@Pic", SqlDbType.VarBinary)).Value = photo;  
                //cmd.Parameters.Add("@Pic", SqlDbType.Image, -1).Value = photo;  //SqlDbType.VarBinary,

                cmd.CommandText = @"Insert Into CM_ItemImage(CorpId, ItemCode, Seq, ItemCode, Title, Pic, CreId, CreIP, CreDt) 
                                                         Values(@CorpId, @ItemCode, @Seq, @ItemCode, @Title, @Pic, @CreId, @CreIP, getdate())";

                // 매개변수 추가
                cmd.Parameters.AddWithValue("@CorpId", WrGlobal.CorpID);
                cmd.Parameters.AddWithValue("@ItemCode", keyArr[1]);
                cmd.Parameters.AddWithValue("@Seq", fileSeq);
                cmd.Parameters.AddWithValue("@ItemCode", "10");
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Pic", photo); // 이진 데이터를 매개변수로 추가
                cmd.Parameters.AddWithValue("@CreId", WrGlobal.LoginID);
                cmd.Parameters.AddWithValue("@CreIP", WrGlobal.ClientHostName);

                db.ExecuteCommand(cmd);
                return;
                //Execute Query Version
                //query = $@"
                //           Declare @Pic image
                //           Set @Pic ='{photo}'
                //           Insert Into CM_ItemImage(CorpId, ItemCode, Seq, ItemCode, "
                //        + "Title, Pic, CreId, CreIP, CreDt) "
                //        + "Values("
                //        + $"'{WrGlobal.CorpID}', '{keyArr[1]}', '{fileSeq}', '10', "
                //        + $"'{title}', @Pic, '{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}', getdate()) ";
            }
            else if (uploadTable == "EQ_MachineImage")
            {
                byte[] photo = GetImage(fromFullFilenames[0]);

                bool newYn = true;

                query = $@"SELECT Seq
	FROM EQ_MachineImage
	WHERE CorpId = '{WrGlobal.CorpID}'
	AND MachineCode = '{keyArr[1]}'
	AND Seq = '{fileSeq}'";

                DataRow dr = db.GetDataRow(query);

                if (dr != null)
                {
                    if (Convert.ToString(dr["Seq"]) != "") newYn = false;
                }


                //신규 생성
                if (newYn)
                {
                    query = $@"Insert Into EQ_MachineImage(CorpId, MachineCode, Seq, Title, Image, CreId, CreIP, CreDt) 
                                                         Values('{WrGlobal.CorpID}', '{keyArr[1]}', {fileSeq}, '{keyArr[0]}', @Pic,'{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}', getdate())";
                }
                else
                {
                    query = $@"
                    UPDATE EQ_MachineImage
                    SET Image = @Pic
                      , Title = '{keyArr[0]}'
                      , ModId = '{WrGlobal.LoginID}'
                      , ModIP = '{WrGlobal.ClientHostName}'
                      , ModDt = GETDATE()
                    
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND MachineCode = '{keyArr[1]}'
                    AND Seq = {fileSeq}";
                }

                db.ExecuteQueryImage(query, photo);

                return;
            }

            if (query != "")
            {
                db.ExecuteQuery(query);
            }
        }

        public void UpdateDB(int fileSeq, string title, string oriFileName, string fileName, long capacity, string fileExt)
        {
            if (uploadTable == "CodeDocumentFile")
            {
                query = $@"
                            UPDATE CodeDocumentFile 
                            SET 
                                ItemCode = 'Z030', 
                                Title = '{title}', 
                                OriFileName = '{oriFileName}', 
                                FileName = '{fileName}', 
                                Path = '{uploadPath}', 
                                Capacity = {capacity}, 
                                FileExt = '{fileExt}', 
                                ModId = '{WrGlobal.LoginID}', 
                                ModIP = '{WrGlobal.ClientHostName}', 
                                ModDt = GETDATE() 
                            WHERE 
                                CorpId = '{WrGlobal.CorpID}' AND 
                                Gubun = '{keyArr[0]}' AND 
                                KeyCode1 = '{keyArr[1]}' AND 
                                KeyCode2 = '{(keyArr.Length > 2 ? keyArr[2] : "")}' AND 
                                KeyCode3 = '{(keyArr.Length > 3 ? keyArr[3] : "")}' AND 
                                FileSeq = {fileSeq}";
            }
            if (query != "")
            {
                db.ExecuteQuery(query);
            }
        }


        private void FilesUploader_Shown(object sender, EventArgs e)
        {
            clsFtp ftp = new clsFtp(this as IFileInterface);

            //uploadPath = "/" + WrGlobal.DBName + "/" + uploadTable;
            uploadPath = "/" + Database.DbName + "/" + uploadTable;

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

        private int GetNextFileSeq(string corpId, string tableName, string filter)
        {
            query = string.Format("Select isnull(max(FileSeq), 0) + 1 seq From {1} Where CorpId = '{0}' and {2}", corpId, tableName, filter);
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