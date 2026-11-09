using SmartMES.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_PasswordChange : DevExpress.XtraEditors.XtraForm
    {
        clsSystem sys = new clsSystem();

        public Frm_SM_PasswordChange()
        {
            InitializeComponent();
        }

        public Frm_SM_PasswordChange(Rectangle r)
        {
            InitializeComponent();

            this.Left = r.Left + (r.Width - this.Width) / 2;
            this.Top = r.Top + (r.Height - this.Height) / 4;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "")
            {
                MessageBox.Show("현재 비밀번호를 입력하세요.");
                txtFrom.Focus();
                return;
            }

            if (txtTo.Text == "")
            {
                MessageBox.Show("새 비밀번호를 입력하세요.");
                txtFrom.Focus();
                return;
            }

            if (txtFrom.Text == txtTo.Text)
            {
                MessageBox.Show("현재 비밀번호와 새 비밀번호가 같습니다.");
                return;
            }

            if (txtTo.Text != txtConfirm.Text)
            {
                MessageBox.Show("새 비밀번호 확인과 다릅니다.");
                return;
            }

            if (sys.ChangePassword(txtFrom.Text, txtTo.Text))
            {
                MessageBox.Show("비밀번호가 재설정되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("현재 비밀번호가 아닙니다.");
                return;
            }
        }
    }
}