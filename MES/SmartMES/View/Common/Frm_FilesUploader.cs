using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SmartCommon.Common;

namespace SmartMES.Common
{
	public partial class FilesUploader : DevExpress.XtraEditors.XtraForm
    {
        string fromFullFilename;    // 경로포함한 파일명
        string[] keyArr = null;

        string uploadTable = "";

        Database db = new Database();
        string query;

        public FilesUploader()
        {
            InitializeComponent();
        }

        public FilesUploader(string _uploadTable, string _keyNo, string _fromFullFilename)
        {
            InitializeComponent();

            uploadTable = _uploadTable;
            fromFullFilename = _fromFullFilename;
            keyArr = _keyNo.Split(new char[] { '|' });
        }

        public void SetData(string fileName, long pos, long total)
        {
            Application.DoEvents();
            pBar1.EditValue = (int)(pos * 1.0 / total * 100);
            lblFName.Text = fileName;

            lblFileLeng.Text = string.Format("{0} / {1}", clsFunction.GetFileSize(pos), clsFunction.GetFileSize(total));
        }

        private void FilesUploader_Shown(object sender, EventArgs e)
        {
            switch (uploadTable) 
            {
                case "CmDocument":
                    CmDocumentUpload();
                    break;

                case "MachineDocument":
                    MachineDocumentUpload();
                    break;

            }

            FormClose();
        }

        private void CmDocumentUpload()
        {
            string ItemCode = keyArr[0];
            string docNo = keyArr[1];

            string targetPath = "\\" + uploadTable + "\\" + ItemCode + "\\" + docNo;

            byte[] buf = new byte[1024 * 512];  // 0.5M

            FileInfo file = new FileInfo(fromFullFilename);

            if (!Directory.Exists(WrGlobal.m_NetworkDrivePath + targetPath))
                Directory.CreateDirectory(WrGlobal.m_NetworkDrivePath + targetPath);

            string toFileName = DateTime.Now.ToString("yyMMddHHmmss") + "_" + file.Name;

            FileStream strIn = new FileStream(fromFullFilename, FileMode.Open);
            FileStream strOut = new FileStream(Path.Combine(WrGlobal.m_NetworkDrivePath + targetPath, toFileName), FileMode.Create);

            try
            {
                while (strIn.Position < strIn.Length)
                {
                    int len = strIn.Read(buf, 0, buf.Length);
                    strOut.Write(buf, 0, len);

                    SetData(file.Name, strIn.Position, strIn.Length);
                }

                query = string.Format("SELECT ISNULL(MAX(SEQ), 0) + 1 SEQ FROM CmDocument "
                                    + "WHERE CorpId = '{0}' AND ItemCode = '{1}' AND DocNo = '{2}'",
                                    WrGlobal.CorpID, ItemCode, docNo);
                DataRowView drv = db.GetDataRecord(query);

                string seq = drv[0].ToString();

                query = string.Format("INSERT INTO CmDocument(CorpId, ItemCode, DocNo, Seq, FileName, Path, Capacity, FileExt, CreId, CreDt) VALUES("
                                        + "'{0}', '{1}', '{2}', {3}, '{4}', '{5}', {6}, '{7}', '{8}', GETDATE())",
                                        WrGlobal.CorpID, ItemCode, docNo, seq, toFileName, targetPath, file.Length.ToString(), file.Extension, WrGlobal.LoginID);
                db.ExecuteQuery(query);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "업로드", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                strOut.Flush();

                strIn.Close();
                strOut.Close();
            }
        }


        private void MachineDocumentUpload()
        {
            string machineCode = keyArr[0];
            int seq = Convert.ToInt32(keyArr[1]);

            string targetPath = "\\" + WrGlobal.DBName + "\\" + WrGlobal.CorpID +  "\\" + uploadTable + "\\" + machineCode;
            string path = WrGlobal.m_NetworkDrivePath + targetPath;

            byte[] buf = new byte[1024 * 512];  // 0.5M

            FileInfo file = new FileInfo(fromFullFilename);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string toFileName = DateTime.Now.ToString("yyMMddHHmmss") + "_" + file.Name;
            
            FileStream strIn = new FileStream(fromFullFilename, FileMode.Open);
            FileStream strOut = new FileStream(Path.Combine(path, toFileName), FileMode.Create);

            try
            {
                while (strIn.Position < strIn.Length)
                {
                    int len = strIn.Read(buf, 0, buf.Length);
                    strOut.Write(buf, 0, len);

                    SetData(file.Name, strIn.Position, strIn.Length);
                }

                query = string.Format("INSERT INTO MachineDocument(CorpId, MachineCode, Seq, Title, FileName, Path, Capacity, FileExt, CreId, CreDt) VALUES("
                                        + "'{0}', '{1}', {2}, '{3}', '{4}', '{5}', {6}, '{7}', '{8}', GETDATE())",
                                        WrGlobal.CorpID, machineCode, seq, file.Name, toFileName, targetPath, file.Length.ToString(), file.Extension, WrGlobal.LoginID);
                db.ExecuteQuery(query);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "업로드", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                strOut.Flush();

                strIn.Close();
                strOut.Close();
            }
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