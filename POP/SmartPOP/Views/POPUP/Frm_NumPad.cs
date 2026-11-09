using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SMT_MES.Controls;
using SmartPOP.Common;

namespace SmartPOP.Views.POPUP
{
    public partial class Frm_NumPad : DevExpress.XtraEditors.XtraForm
    {
        public delegate void FormSendDataHandler(string totalQty);
        public event FormSendDataHandler FormSendEvent;
        DataRow dr;
        string SdOrderNo;
        string OrderNo;
        string btnTag;
        float ProcCmdQty = 0;

        public Frm_NumPad(string BtnTag)
        {
            InitializeComponent();
            btnTag = BtnTag;
        }

        public Frm_NumPad(string BtnTag, float _ProcCmdQty)
        {
            InitializeComponent();
            btnTag = BtnTag;
            ProcCmdQty = _ProcCmdQty;
        }

        private void FrmNumPad_Load(object sender, EventArgs e)
        {
            TxtTotalQty.Text = ProcCmdQty.ToString(); 
        }


       
        private void TotalQtyNumPad_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;
            ActiveControl = TxtTotalQty;

            string tag = Btn.Tag == null ? "" : Btn.Tag.ToString();

            switch (tag)
            {
                case "num":
                    if (ActiveControl.Text != "0")
                        ActiveControl.Text += Btn.Text;
                    else if (ActiveControl.Text == "0")
                        ActiveControl.Text = Btn.Text;
                    break;

                case "up":
                    try
                    {
                        ActiveControl.Text = (Convert.ToInt32(ActiveControl.Text) + 1).ToString();
                    }
                    catch
                    { }
                    break;

                case "down":
                    try
                    {
                        if (Convert.ToInt32(ActiveControl.Text) > 0)
                            ActiveControl.Text = (Convert.ToInt32(ActiveControl.Text) - 1).ToString();
                    }
                    catch
                    { }
                    break;

                case "back":
                    if (ActiveControl.Text.Length == 1)
                        ActiveControl.Text = "0";
                    if (ActiveControl.Text.Length > 1)
                        ActiveControl.Text = ActiveControl.Text.Substring(0, ActiveControl.Text.Length - 1);
                    break;

                case "C":
                    ActiveControl.Text = "0";
                    break;
                case ".":
                    if (ActiveControl.Text.IndexOf(".") > 0) return;
                    ActiveControl.Text += Btn.Text;
                    break;
            }
        }

        private void TxtTotalQty_EditValueChanged(object sender, EventArgs e)
        {
            //TxtTotalQty.Text = Regex.Replace(TxtTotalQty.Text, @"\D", "");
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            double ConvertTxt = TxtTotalQty.EditValue.ConvertDouble_Null_To_Zero();

            if(btnTag != "Qty")
            {
                ConvertTxt = Math.Floor(ConvertTxt.ConvertDouble_Null_To_Zero());
            }

            switch (btnTag)
            {
                case "HHMM":
                    int lastTwoDigits = ConvertTxt.ConvertInt_Null_To_Zero() % 100;

                    if (ConvertTxt > 2359 || lastTwoDigits > 59)
                    {
                        cls_MessageBox.Show(" 시간 입력 범위 : 00:00 ~ 23:59 ");
                        return;
                    }
                    break;

                case "HH":
                    if (ConvertTxt > 12)
                    {
                        cls_MessageBox.Show(" 시간 입력 범위 : 1 ~ 12 ");
                        return;
                    }
                    break;

                case "MM":
                    if (ConvertTxt > 59)
                    {
                        cls_MessageBox.Show(" 분 입력 범위 : 0 ~ 59 ");
                        return;
                    }
                    break;

                case "Qty":
                    break;
            }

            FormSendEvent(ConvertTxt.ConvertString_Null_To_Zero());
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}