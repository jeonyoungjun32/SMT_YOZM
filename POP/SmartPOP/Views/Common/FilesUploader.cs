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

namespace SmartPOP.Common
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

            }

            FormClose();
        }

        private void CmDocumentUpload()
        {
            string itemCode = keyArr[0];
            string title = keyArr[1];

            string targetPath = "\\" + uploadTable + "\\" + itemCode;

            byte[] buf = new byte[1024 * 512];  // 0.5M

            FileInfo file = new FileInfo(fromFullFilename);

            if (!Directory.Exists(WrGlobal.m_NetworkDrivePath + targetPath))
                Directory.CreateDirectory(WrGlobal.m_NetworkDrivePath + targetPath);

            string docNo = itemCode + db.ExecAutoNumber("문서" + itemCode, Convert.ToDateTime("2020-01-01")).Split(new char[] { '-' })[1];
            string toFileName = docNo + file.Extension.ToLower();

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

                query = string.Format("INSERT INTO CmDocument(CorpId, DocNo, ItemCode, Title, FileName, Path, Capacity, FileExt, UseYn, CreId, CreIP, CreDt) VALUES("
                                        + "'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, '{7}', '{8}', '{9}', '{10}', GETDATE())",
                                        WrGlobal.CorpID, docNo, itemCode, title, toFileName, targetPath, file.Length.ToString(), file.Extension.ToLower(), "Y", WrGlobal.LoginID, WrGlobal.ClientHostName);
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