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
using SmartMES.Common;
using SmartPOP.Common;
using SMT_MES.Controls;

namespace SmartPOP.Views.POPUP
{
    public partial class Frm_PDFViewer : DevExpress.XtraEditors.XtraForm
    {
        public delegate void FormSendDataHandler(byte[] pdf, string title);
        public event FormSendDataHandler FormSendEvent;

        string processCode = "";
        string materialCode = "";
        string formType = "";
        string code = "";
        string seq = "";
        string title = "";
        byte[] FileBuff = new byte[] { };
        bool PnlVisible = true;

        Database db = new Database();
        string query = "";

        byte[] Work_Standard_Sheet;

        public Frm_PDFViewer()
        {
            InitializeComponent();
        }

        public Frm_PDFViewer(string materialCode)
        {
            InitializeComponent();

            this.materialCode = materialCode;

            Search();
        }

        public Frm_PDFViewer(string materialCode, string processCode)
        {
            InitializeComponent();

            this.materialCode = materialCode;
            this.processCode = processCode;

            Search();
        }

        public Frm_PDFViewer(string formType, string code, string seq = null)
        {
            InitializeComponent();

            this.formType = formType;
            this.code = code;
            this.seq = seq;

            Search();
        }

        private void Search()
        {
            if (formType == "MaterialDesign")
            {
                query = $@"SELECT PicPdf FROM CmMaterialDesign WHERE CorpId = '{WrGlobal.CorpID}' AND MaterialCode = '{code}'";
                if (seq != null)
                {
                    query += $@"AND Seq = '{seq}'";
                }
            }
            else //작업표준서
            {
                query = $@"SELECT PicPdf FROM CmOrderWorkStandards WHERE CorpId = '{WrGlobal.CorpID}' AND MaterialCode = '{materialCode}'";
            }

            DataRow dr = db.GetDataRow(query);
            if (dr == null) return;

            try
            {
                byte[] buff;
                if (formType == "MaterialDesign")
                {
                    buff = dr["PicPdf"] as byte[];
                }
                else
                {
                    buff = dr["PicPdf"] as byte[];
                }
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
                this.title = fileName;

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
                if (FileBuff == null || FileBuff.Length == 0)
                {
                    cls_MessageBox.Show("불러온 PDF가 없습니다. 저장할 수 없습니다.");
                    return;
                }

                Work_Standard_Sheet = FileBuff;

                Send_Data(Work_Standard_Sheet, title);
                
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

                if (formType == "MaterialDesign")
                {
                    if (Upd_PicPdf())
                    {
                        cls_MessageBox.Show("삭제가 완료되었습니다.");
                    }
                }
                else
                {
                    if (Delete_CmWorkStandards())
                    {
                        cls_MessageBox.Show("삭제가 완료되었습니다.");
                    }
                }
            }
        }

        //작업표준서 등록
        private bool Ins_CmWorkStandards()
        {
            query = $@"INSERT INTO CmOrderWorkStandards (
                                                  CorpId
                                                , MaterialCode
                                                , ItemCode
                                                , Pic
                                                , CreId
                                                , CreIP
                                                , CreDt) 
						                        VALUES (
                                                '{WrGlobal.CorpID}'
                                                ,'{processCode}'
                                                ,'{materialCode}'
                                                , '10'  --안정해짐 일단 10으로 고정
                                                , @pic
                                                ,'{WrGlobal.LoginID}'
                                                ,'{WrGlobal.IpAddress}'
                                                ,GETDATE())";

            return db.ExecuteQueryImage(query, Work_Standard_Sheet);
        }

        private bool Upd_PicPdf()
        {
            query = $@"UPDATE CmMaterialDesign
                            SET PicPdf = @Pic
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND MaterialCode = '{code}'
                        ";

            return db.ExecuteQueryImage(query, Work_Standard_Sheet);
        }

        //작업표준서 삭제
        private bool Delete_CmWorkStandards()
        {
            query = $@"Delete From CmOrderWorkStandards
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND MaterialCode = '{materialCode}'
                        ";

            return db.ExecuteQueryImage(query, Work_Standard_Sheet);
        }

        private void Send_Data(byte[] pdf, string title)
        {
            try
            {
                if (title == "")
                {
                    cls_MessageBox.Show("변경된 내용이 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                FormSendEvent(pdf, title);
                this.Close();
            }
            catch
            {

            }
        }
    }
}