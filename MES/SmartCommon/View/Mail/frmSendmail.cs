using DevExpress.XtraEditors;
using SmartCommon.Common;
using SmartCommon.View.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.View.Mail
{
    public partial class frmSendmail : XtraForm
    {
        
        public frmSendmail()
        {
            InitializeComponent();
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            //예외처리
            if (ucARsend.LAddress.Count < 1)
            {
                MessageBox.Show("받는 사람을 추가하세요.");
                return;
            }

            string sendMail = "joy.dvp@gmail.com";
            string nicname = "";
            clsEMail eMail = new clsEMail(sendMail, "onff kdnz wcnp olqy", sendMail, nicname);//앱비밀번호

            List<string> LSend = ucARsend.LAddress;
            List<string> LRed = ucARreg.LAddress;
            List<string> LHide = ucARhide.LAddress;
            List<string> lstFilePath = new List<string>();

            foreach (string item in lboxFile.Items)
			{
                lstFilePath.Add(item);
            }

            string sResult = eMail.SendEMail(tboxSubject.Text, txtBody.HtmlText, LSend, LRed, LHide, lstFilePath);
            XtraMessageBox.Show(sResult);
        }

        private void cmdDocEdit_Click(object sender, EventArgs e)
        {
            frmDocumentEdit form = new frmDocumentEdit(txtBody.RtfText);
            form.onSetData += new frmDocumentEdit.SetData(DocumentSetData);
            form.ShowDialog();

        }
        private void DocumentSetData(string rtfData)
        {
            txtBody.RtfText = rtfData;
        }

		private void BtnAdd_Click(object sender, EventArgs e)
		{
            //체크할거
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                Title = "첨부할 파일 선택"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in openFileDialog.FileNames)
                {
                    if (!lboxFile.Items.Contains(path))
                    {
                        lboxFile.Items.Add(path);
                    }
                }
            }
        }

		private void btnDelete_Click(object sender, EventArgs e)
		{
            if (lboxFile.SelectedItem == null) return;

            string selected = lboxFile.SelectedItem.ToString();

            if (XtraMessageBox.Show($"'{selected}' 첨부파일을 삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lboxFile.Items.Remove(selected);
            }
        }

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}
