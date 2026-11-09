using DevExpress.CodeParser;
using DevExpress.Pdf;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraReports;
using SmartPOP.Models.CM;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.Common
{
    public partial class Frm_PDFViewer_New : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string MaterialCode = "";
        byte[] FileBuff = new byte[] { };
        bool PnlVisible = true;

        public Frm_PDFViewer_New(string materialCode)
        {
            InitializeComponent();
            this.MaterialCode = materialCode;
        }

        public Frm_PDFViewer_New(string materialCode, bool pnlVisible)
        {
            InitializeComponent();
            this.MaterialCode = materialCode;
            this.PnlVisible = pnlVisible;
        }

        private void FrmPDFViewer_Load(object sender, EventArgs e)
        {
            if (!PnlVisible)
            {
                ribbonControl1.Minimized = true;
                ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            }

            Search();
        }

        private void Search()
        {
            CM_Item item = new CM_Item();
            item.MaterialCode = this.MaterialCode;
            
            DataRow dr = item.Select_ItemImage();
            if (dr == null) return;

            try
            {
                byte[] buff = dr["Work_Standard_Sheet"] as byte[];
                if (buff == null) return;

                MemoryStream mStream = new MemoryStream(buff);

                pdfViewer1.LoadDocument(mStream);
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show("올바른 PDF파일이 아닙니다." + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnItem_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "BtnOpen":
                    Open();
                    break;

                case "BtnSave":
                    Save();
                    break;

                case "BtnDelete":
                    Delete();
                    break;
            }
        }

        private void Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "PDF파일 선택";
            ofd.Filter = "pdf 파일 (*.pdf) | *.pdf;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string localFileFullPath = ofd.FileName;
                string fileName = Path.GetFileName(localFileFullPath);

                FileStream fs = new FileStream(localFileFullPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                FileBuff = br.ReadBytes((int)fs.Length);

                pdfViewer1.LoadDocument(localFileFullPath);
            }
        }

        private void Save()
        {
            try
            {
                CM_Item item = new CM_Item();
                item.MaterialCode = this.MaterialCode;
                item.Work_Standard_Sheet = FileBuff;

                if (item.Upd_Item_Work_Standard_Sheet())
                {
                    cls_MessageBox.Show("저장이 완료되었습니다.");
                }

            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message);
            }
        }

        private void Delete()
        {
            DialogResult result = cls_MessageBox.Show("파일을 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                FileBuff = null;
                pdfViewer1.CloseDocument();

                CM_Item item = new CM_Item();
                item.MaterialCode = this.MaterialCode;
                item.Work_Standard_Sheet = FileBuff;

                if (item.Upd_Item_Work_Standard_Sheet())
                {
                    cls_MessageBox.Show("삭제가 완료되었습니다.");
                }
            }
        }
    }
}