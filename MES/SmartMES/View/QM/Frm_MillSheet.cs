using DevExpress.XtraEditors;
using System;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
    public partial class Frm_MillSheet : DevExpress.XtraEditors.XtraForm
    {
        string g_RefNo = "";
        int g_RefSeq = 0;
        string g_ExamDivCd = "";

        public Frm_MillSheet(string RefNo, int ref_Row_Seq, string ExamDivCd)
        {
            InitializeComponent();

            g_RefNo = RefNo;
            g_RefSeq = ref_Row_Seq;
            g_ExamDivCd = ExamDivCd;
        }

        private void Frm_MillSheet_Load(object sender, EventArgs e)
        {

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            switch(btn.Name)
            {
                case "BtnSearch": Search(); break;
                case "BtnUpload": Upload(); break;
                case "BtnDelete": Delete(); break;
            }
        }

        private void Search()
        {
            //QM_ExamResult qer = new QM_ExamResult();
            //qer.RefNo = g_RefNo;
            //qer.RefSeq = g_RefSeq;
            //qer.ExamDivCd = g_ExamDivCd;

            //DataRow dataRow = qer.Select_ExamResult_Exsits();

            //if (dataRow == null)
            //{
            //    MessageBox.Show("등록된 밀시트가 없습니다.");
            //    return;
            //}

            //string InFilePath = dataRow["InFilePath"].ConvertString_Null_To_Empty();

            //if (InFilePath == "")
            //{
            //    MessageBox.Show("등록된 밀시트가 없습니다.");
            //    return;
            //}

            ////FTP 이미지 다운로드
            //cls_FTP clsFtp = new cls_FTP();
            //Stream stream = clsFtp.DownloadFile(InFilePath);
            //if (stream == null) return;

            //pictureEdit1.Image = Image.FromStream(stream);
        }

        private void Upload()
        {
            //FTP 이미지 업로드
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG,*.PNG";

            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string localFilePath = Path.GetDirectoryName(ofd.FileName);
            string fileName = Path.GetFileName(ofd.FileName);

            //string serverFolder = WrGlobal.m_ftpServerip + "/" + WrGlobal.m_ftpFolderPath;
            //string serverFileFullPath = serverFolder + "/" + fileName;

            //cls_FTP clsFtp = new cls_FTP();

            //FileInfo file = clsFtp.UploadFile(serverFolder, localFilePath, fileName);
            //if (file == null) return;

            ////FTP FilePath UPDATE
            //QM_ExamResult qer = new QM_ExamResult();
            //qer.RefNo = g_RefNo;
            //qer.RefSeq = g_RefSeq;
            //qer.ExamDivCd = g_ExamDivCd;
            //qer.InFilePath = serverFileFullPath;

            //bool result = qer.Upd_ExamResult_FilePath();

            //if (result)
            //{
            //    MessageBox.Show("업로드가 완료되었습니다.");
            //}
        }

        private void Delete()
        {
            ////FTP FilePath UPDATE
            //QM_ExamResult qer = new QM_ExamResult();
            //qer.RefNo = g_RefNo;
            //qer.RefSeq = g_RefSeq;
            //qer.ExamDivCd = g_ExamDivCd;
            //qer.InFilePath = "";

            //bool result = qer.Upd_ExamResult_FilePath();

            //if (result)
            //{
            //    MessageBox.Show("삭제가 완료되었습니다.");
            //    this.pictureEdit1.Image = null;
            //}
        }
    }
}