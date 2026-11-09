using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Calendar;
using SmartPOP.Common;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.POPUP
{
    public partial class Frm_DateTime_POP : DevExpress.XtraEditors.XtraForm
    {
        string TimeTxt = "";
        string DateTxt = "";
        string _date = "";
        string _time = "";

        public Frm_DateTime_POP(string date, string title = "날짜 및 시간")
        {
            InitializeComponent();

            this.Text = title;

            //달력 최상단 헤더(2023년 x월 xx일) 안보이도록
            calendarControl1.ShowMonthHeaders = true;
            calendarControl1.ShowMonthNavigationButtons = DevExpress.Utils.DefaultBoolean.True;
            calendarControl1.ShowTodayButton = false;

            // 팝업 시 키패드에 뜨는 날짜,시간
            if (date == null || date == " " || date == "")
            {
                _date = null;
                _time = null;
            }
            else
            {
                _date = date.Substring(0, 10);
                _time = date.Substring(11, 5);
            }
        }

        private void Frm_DateTime_POP_Load(object sender, EventArgs e)
        {
            if (_date == null || _time == null)
            {
                Txt_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");    
                TxtTotalQty.Text = DateTime.Now.ToString("HH:mm");
                calendarControl1.DateTime = DateTime.Now;
            }
            else
            {
                Txt_Date.Text = _date;
                TxtTotalQty.Text = _time;
                calendarControl1.DateTime = Convert.ToDateTime(_date);
            }

            calendarControl1.EditValue = Txt_Date.Text + " " + TxtTotalQty.Text;
		}

		private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;

            string Name = Btn.Name;

            switch (Name)
            {
                case "btn_CurrentDate":
                    //현재일자: 
                    Txt_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    calendarControl1.SelectedRanges.Clear();
                    break;
                case "btn_CurrentTime":
                    Txt_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TxtTotalQty.Text = DateTime.Now.ToString("HH:mm");
                    calendarControl1.SelectedRanges.Clear();
                    break;
                case "btn_AM0820":
                    TxtTotalQty.Text = DateTime.Now.ToString("08:20");
                    calendarControl1.SelectedRanges.Clear();
                    break;
                case "btn_PM20":
                    TxtTotalQty.Text = DateTime.Now.ToString("20:00");
                    calendarControl1.SelectedRanges.Clear();
                    break;
                case "btn_OK":
                    this.DialogResult = DialogResult.OK;
                    sendData();
                    break;
                case "btn_Close":
                    //Close_Pad();
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    break;
            }
        }

        private void sendData()
        {   
            //시간입력확인
            //받은 시간을 : 기준으로 시간/분으로 구분
            string[] _time = TxtTotalQty.Text.Split(':'); //받은시간
            
            if(_time.Length < 2)
            {
                cls_MessageBox.Show("시간 입력 범위 : 00:00 ~ 23:59");
                return;
            }

            int hour = Convert.ToInt32(_time[0]); //시간
            int min = Convert.ToInt32(_time[1]);  //분

            int minLength = _time[1].Length; //4자리 필수 입력

            if (hour < 0 || hour >= 24 || min < 0 || min >= 60 || minLength <= 1)
            {
                cls_MessageBox.Show(" 시간 입력 범위 : 00:00 ~ 23:59 ");
                TxtTotalQty.Text = "";
                return;
            }
            
            //보내줄 결과값
            TimeTxt = TxtTotalQty.Text.ConvertString_Null_To_Empty();
            DateTxt = Txt_Date.Text.ConvertString_Null_To_Empty();
            resultDateTime();

            this.Close();
        }
        private void Close_Pad()
        {
            //보내줄 결과값
            DateTxt = _date;
            TimeTxt = _time;
            resultDateTime();
        }

        public Tuple<string,string> resultDateTime()
        {
            string strTimeTxt = TimeTxt.ConvertString_Null_To_Empty();
            string dateTime = DateTxt + " " + strTimeTxt;

            return Tuple.Create(dateTime, strTimeTxt);
        }

         private void TotalQtyNumPad_Click(object sender, EventArgs e)
         {
            SimpleButton Btn = (SimpleButton)sender;
            ActiveControl = TxtTotalQty;

            string tag = Btn.Tag == null ? "" : Btn.Tag.ToString();

            switch (tag)
            {
                case "num":
                    if (ActiveControl.Text.Length > 4)
                    {
                        ActiveControl.Text = "";
                        ActiveControl.Text += Btn.Text;
                    }
                    else if (ActiveControl.Text.Length == 2 &&  ActiveControl.Text.Length < 5)
                    {
                        ActiveControl.Text += ":";
                        ActiveControl.Text += Btn.Text;
                    }
                    else if (ActiveControl.Text.Length < 5)
                    {
                        ActiveControl.Text += Btn.Text;
                    }
                    break;

                case "back":
                    if (ActiveControl.Text.Length == 1)
                        ActiveControl.Text = "0";
                    if (ActiveControl.Text.Length > 1)
                        ActiveControl.Text = ActiveControl.Text.Substring(0, ActiveControl.Text.Length - 1);
                    break;

                case "C":
                    ActiveControl.Text = "";
                    break;
            }
        }

        private void calendarControl1_Click(object sender, MouseEventArgs e)
        {
            CalendarHitInfo hitInfo = calendarControl1.GetHitInfo(e.Location);
            if (hitInfo.HitTest == CalendarHitInfoType.MonthNumber)
            {
                string test = hitInfo.HitDate.ToString("yyyy-MM-dd");
                Txt_Date.Text = test;
            }
        }
    }
}
