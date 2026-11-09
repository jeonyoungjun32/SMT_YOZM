using DevExpress.XtraEditors;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models.QM;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
	public partial class Frm_MillSheet_pdf : DevExpress.XtraEditors.XtraForm
    {
        string g_RefNo = "";
        int? g_RefSeq = 0;
        string g_ExamDivCd = "";

        Database db = new Database();
        DataRow dr = null;

        public Frm_MillSheet_pdf(string RefNo, int ref_Row_Seq, string ExamDivCd)
        {
            InitializeComponent();

            g_RefNo = RefNo;
            g_RefSeq = ref_Row_Seq;
            g_ExamDivCd = ExamDivCd;
        }

        public Frm_MillSheet_pdf(string RefNo, int ref_Row_Seq, string ExamDivCd, DataRow drRow)
        {
            InitializeComponent();

            g_RefNo = RefNo;
            g_RefSeq = ref_Row_Seq;
            g_ExamDivCd = ExamDivCd;
            dr = drRow;
        }

        public Frm_MillSheet_pdf(string RefNo, string ExamDivCd, DataRow drRow)
        {
            InitializeComponent();

            g_RefNo = RefNo;
            g_RefSeq = null;
            g_ExamDivCd = ExamDivCd;
            dr = drRow;
        }

        private void Frm_MillSheet_pdf_Load(object sender, EventArgs e)
        {
            Search();
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
            DataRow dataRow = null;

            QM_ExamResult qer = new QM_ExamResult();
            qer.RefNo = g_RefNo;
            qer.ExamDivCd = g_ExamDivCd;

            if(g_RefSeq != null)
			{
                qer.RefSeq = g_RefSeq.ConvertInt_Null_To_Zero();
                dataRow = qer.Select_ExamResult_Exsits();
			}
			else
			{
                dataRow = qer.Select_ExamResult_Exsits_No();
            }

            if (dataRow["InFilePath"].ConvertString_Null_To_Empty() == "")
            {
                return;
            }

            string InFilePath = dataRow["InFilePath"].ConvertString_Null_To_Empty();
            string ext = dataRow["FileExt"].ConvertString_Null_To_Empty();

            clsFtp clsFtp = new clsFtp();
            Stream stream = clsFtp.DownloadFile(InFilePath);

            if (ext.ToUpper() == ".PDF")
            {
                PicImage.Visible = false;
                pdfViewer1.Visible = true;

                pdfViewer1.LoadDocument(stream);
                pdfViewer1.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.PageLevel;
            }
            else
            {
                PicImage.Visible = true;
                pdfViewer1.Visible = false;

                PicImage.Image = Image.FromStream(stream);
            }
        }

        private void Upload()
        {
            //FTP 이미지 업로드
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "파일 선택";
            ofd.Filter = "pdf 파일 (*.pdf) | *.PDF; ";
            //ofd.Filter = "모든파일 ( *.bmp; *.jpg; *.png; *.jpeg; *.pdf) | *.BMP; *.JPG; *.PNG; *.JPEG; *.PDF; | pdf 파일 (*.pdf) | *.PDF; |이미지 파일( *.bmp; *.jpg; *.png; *.jpeg) | *.BMP; *.JPG; *.PNG; *.JPEG;";

            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string localFilePath = Path.GetDirectoryName(ofd.FileName);
            string fileName = Path.GetFileName(ofd.FileName);

            string serverFolder = WrGlobal.m_ftpServerip + "/" + WrGlobal.m_ftpFolderPath;
            string serverFileFullPath = serverFolder + "/" + fileName;

            string ext = Path.GetExtension(ofd.FileName);

            clsFtp clsFtp = new clsFtp();

            FileInfo file = clsFtp.UploadFile(serverFolder, localFilePath, fileName);
            if (file == null) return;


            bool result = true;

            //FTP FilePath UPDATE
            QM_ExamResult qer = new QM_ExamResult();
            qer.RefNo = g_RefNo;
            qer.ExamDivCd = g_ExamDivCd;
            qer.InFilePath = serverFileFullPath;
            qer.FileExt = ext;

            if(g_RefSeq != null)
			{
                qer.RefSeq = g_RefSeq.ConvertInt_Null_To_Zero();
                result = qer.Upd_ExamResult_FilePath();
			}
			else
			{
                result = qer.Upd_ExamResult_FilePath_No();
            }

            if (result)
            {
                MessageBox.Show("업로드가 완료되었습니다.");
                Search();
            }
        }

        private void Delete()
        {
            bool result = true;

            //FTP FilePath UPDATE
            QM_ExamResult qer = new QM_ExamResult();
            qer.RefNo = g_RefNo;
            qer.ExamDivCd = g_ExamDivCd;
            qer.InFilePath = "";
            qer.FileExt = "";

            if (g_RefSeq != null)
            {
                qer.RefSeq = g_RefSeq.ConvertInt_Null_To_Zero();
                result = qer.Upd_ExamResult_FilePath();
            }
            else
            {
                result = qer.Upd_ExamResult_FilePath_No();
            }

            if (result)
            {
                MessageBox.Show("삭제가 완료되었습니다.");
                this.PicImage.Image = null;
                this.pdfViewer1.Visible = false;
            }
        }
    }
}