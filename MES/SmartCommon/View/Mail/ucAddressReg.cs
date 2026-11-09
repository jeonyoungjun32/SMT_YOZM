using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.View.Mail
{
    public partial class ucAddressReg : UserControl
    {
        // 외부로 Listbox 내용을 전달하기 위해 추가
        public List<string> LAddress
        {
            get => lboxAddress.Items.Cast<String>().ToList();
            // set => _lAddress = value; 
        }
        
        public ucAddressReg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add Button 선택 시 List Box에 Mial 주소 등록 (주소 등록 시 정규식 체크 해서 메일 주소 인지 확인)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(EmailFormatCheck(tboxAddress.Text))
            {

                // 어라? 체크 필요
                if (LAddress != null && LAddress.Contains(tboxAddress.Text))
                {
                    MessageBox.Show("이미 등록 되어 있는 주소 입니다.");
                    return;  // 기존에 있으면 안되지~
                }

                lboxAddress.Items.Add(tboxAddress.Text);
                tboxAddress.Text = "";
            }
            else
            {
                MessageBox.Show("입력 값이 메일 형식이 아닙니다.");
            }
        }

        /// <summary>
        /// Mouse Double Click 시 Mail 주소 삭제
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lboxAddress_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int iRow = lboxAddress.SelectedIndex;
            if (iRow != -1)
            {
                lboxAddress.Items.RemoveAt(iRow);
            }
        }


        /// <summary>
        /// Mail 주소 형식인지 정규식 체크
        /// </summary>
        /// <param name="strcheckMailAddress"></param>
        /// <returns></returns>
        private bool EmailFormatCheck(string strcheckMailAddress)
        {

            string regExp = @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";  // 이메일 정규식 체크 
            Regex regex = new Regex(regExp);

            if (regex.IsMatch(strcheckMailAddress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		private void btnDelete_Click(object sender, EventArgs e)
		{
            if (lboxAddress.SelectedItem == null) return;

            string selected = lboxAddress.SelectedItem.ToString();
            if (XtraMessageBox.Show($"'{selected}' 삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lboxAddress.Items.Remove(selected);
            }
        }
	}
}
