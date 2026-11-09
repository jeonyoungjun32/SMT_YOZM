using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Calendar;
using SmartPOP.Common;
using System;
using System.Windows.Forms;

namespace SmartPOP.Views.POPUP
{
    public partial class Frm_DateTime_POP_AMPM : DevExpress.XtraEditors.XtraForm
    {
        string TimeTxt = "";
        string DateTxt = "";
        string _date = "";
        string _Hour = "";
        string _Min = "";
        string _startTime = "";
        string _endTime = "";
        string _filedName = "";

        public Frm_DateTime_POP_AMPM(string date, string startTime, string endTime, string filedName)
        {
            InitializeComponent();
            
            //달력 최상단 헤더(2023년 x월 xx일) 안보이도록
            calendarControl1.ShowMonthHeaders = true;
            calendarControl1.ShowMonthNavigationButtons = DevExpress.Utils.DefaultBoolean.True;
            calendarControl1.ShowTodayButton = false;

            // 팝업 시 키패드에 뜨는 날짜,시간
            if (date == null || date == " " || date == "")
            {
                _date = null;
                _Hour = null;
                _Min = null;
            }
            else
            {
                _date = date.Substring(0, 10);
                _Hour = date.Substring(11, 2);
                _Min = date.Substring(14, 2);
            }

            _startTime = startTime;
            _endTime = endTime;
            _filedName = filedName;
        }

        private void Frm_DateTime_POP_Load(object sender, EventArgs e)
        {
            //입력된 시간입력 , nul이면 현재시간 입력
            if (_date == null || _Hour == null)
            {
                Txt_Date.Text = DateTime.Now.ToString("yyyy-MM-dd");    
                TxtHour.Text = DateTime.Now.ToString("HH");
                TxtMin.Text = DateTime.Now.ToString("mm");
            }
            else
            {
                Txt_Date.Text = _date;
                TxtHour.Text = _Hour;
                TxtMin.Text = _Min;
            }

            //초기 AM/PM
            if (Convert.ToInt32(TxtHour.Text) < 12)
            {
                AMPM.Text = "AM";
                this.BtnHourAM.Appearance.BackColor = System.Drawing.Color.Silver;
                this.BtnHourPM.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            else if (Convert.ToInt32(TxtHour.Text) >= 12)
            {
                AMPM.Text = "PM";
                this.BtnHourPM.Appearance.BackColor = System.Drawing.Color.Silver;
                this.BtnHourAM.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            }

            //키보드 입력 시 AM/PM이 바뀌는 이벤트
            TxtHour.KeyPress += TxtHour_KeyPress;
            TxtMin.KeyPress += TxtMin_KeyPress;
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
                    TxtHour.Text = DateTime.Now.ToString("HH");
                    TxtMin.Text = DateTime.Now.ToString("mm");
                    if (Convert.ToInt32(TxtHour.Text) < 12) 
                    { 
                        AMPM.Text = "AM";
                        this.BtnHourAM.Appearance.BackColor = System.Drawing.Color.Silver;
                        this.BtnHourPM.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
                    }
                    else if (Convert.ToInt32(TxtHour.Text) >= 12) 
                    {   
                        AMPM.Text = "PM"; 
                        this.BtnHourPM.Appearance.BackColor = System.Drawing.Color.Silver;
                        this.BtnHourAM.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
                    }
                    calendarControl1.SelectedRanges.Clear();
                    break;

                case "btn_OK":
                    sendData();
                    break;

                case "btn_Close":
                    Close_Pad();
                    this.Close();
                    break;

                case "BtnHourAM":
                    AMPM.Text = "AM";
                    this.BtnHourAM.Appearance.BackColor = System.Drawing.Color.Silver;
                    this.BtnHourPM.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;

                    string txtam = string.IsNullOrEmpty(TxtHour.Text) ? "0" : TxtHour.Text;
                    if (Convert.ToInt32(txtam) >= 12)
                    {
                        if (txtam == "0") return;
                        int changeAm = Convert.ToInt32(TxtHour.Text) - 12;
                        if(changeAm <= 9)
                        {
                            TxtHour.Text = "0" + (Convert.ToInt32(TxtHour.Text) - 12).ToString();
                        }
                        else
                        {
                            TxtHour.Text = (Convert.ToInt32(TxtHour.Text) - 12).ToString();
                        }
                    }
                    break;

                case "BtnHourPM":
                    AMPM.Text = "PM";
                    this.BtnHourPM.Appearance.BackColor = System.Drawing.Color.Silver;
                    this.BtnHourAM.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;

                    string txtpm = string.IsNullOrEmpty(TxtHour.Text) ? "0" : TxtHour.Text;
                    if (Convert.ToInt32(txtpm) < 12) { if (txtpm == "0") return;  TxtHour.Text = (Convert.ToInt32(TxtHour.Text) + 12).ToString(); }
                    break;

            }
        }

        private void sendData()
        {   
            //시간입력확인
            //받은 시간을 : 기준으로 시간/분으로 구분
            
            //네자리 입력 필수
            if(TxtHour.Text.Length < 2 || TxtMin.Text.Length < 2)
            {
                //cls_MessageBox.Show("시간 입력 범위 : 00:00 ~ 23:59");
                return;
            }

            int hour = Convert.ToInt32(TxtHour.Text); //시간
            int min = Convert.ToInt32(TxtMin.Text);  //분


            if (hour < 0 || hour >= 24 || min < 0 || min >= 60)
            {
                //cls_MessageBox.Show(" 시간 입력 범위 : 00:00 ~ 23:59 ");
                TxtHour.Text = "";
                return;
            }

            //보내줄 결과값
            string resultTime = TxtHour.Text + ":" + TxtMin.Text;
            TimeTxt = resultTime.ConvertString_Null_To_Empty();
            DateTxt = Txt_Date.Text.ConvertString_Null_To_Empty();

            //시작시간,종료시간 비교 빠르거나 느리지않게
            //a. 시작시간 최초입력 (종료시간 입력 전)
            if (_startTime == "" && _endTime == "")
            {
                //바로입력
                resultDateTime();
            }
            //b. 시작시간 수정 (종료시간 입력 후)
            else if (_filedName == "lbl_StartDate" && _endTime != "") 
            {
                //종료시간과 비교
                DateTime endTime = Convert.ToDateTime(_endTime);
                DateTime starTime = Convert.ToDateTime(DateTxt + " " + resultTime);
                TimeSpan timeDiff = endTime - starTime;

                if (timeDiff.TotalMinutes < 0)
                {
                   // cls_MessageBox.Show(" 입력한 시작시간은 종료시간보다 늦습니다. ");
                    TxtHour.Text = "";
                    TxtMin.Text = "";
                    return;
                }

                resultDateTime();
            }   
            //c. 종료시간 최초입력/수정
            else
            {
                DateTime starTime = Convert.ToDateTime(_startTime);
                DateTime endTime = Convert.ToDateTime(DateTxt + " " + resultTime);
                TimeSpan timeDiff = starTime - endTime;

                if (timeDiff.TotalMinutes > 0)
                {
                    //cls_MessageBox.Show(" 입력한 종료시간은 시작시간보다 빠릅니다.");
                    TxtHour.Text = "";
                    TxtMin.Text = "";
                    return;
                }

                //조건에 충족했다면 결과값 보내기 
                resultDateTime();
            }

            this.Close();
        }
        private void Close_Pad()
        {
            //보내줄 결과값
            DateTxt = _date;
            TimeTxt = _Hour + ":" + _Min;
            resultDateTime();
        }

        public Tuple<string,string> resultDateTime()
        {
           
            if (TimeTxt == ":") { TimeTxt = null; }

            string strTimeTxt = TimeTxt.ConvertString_Null_To_Empty();
            string dateTime = DateTxt + " " + strTimeTxt;

            return Tuple.Create(dateTime, strTimeTxt);
        }

         private void NumPad_Click(object sender, EventArgs e)
         {
            SimpleButton Btn = (SimpleButton)sender;

            string tag = Btn.Tag == null ? "" : Btn.Tag.ToString();

            switch (tag)
            {
                case "Hour":
                    if (AMPM.Text == "AM")
                    {
                        ActiveControl = TxtHour;
                        ActiveControl.Text = "";
                        
                        if (Btn.Text.Length == 1)
                        {
                            ActiveControl.Text += '0';
                            ActiveControl.Text += Btn.Text;
                        }
                        else
                        {
                            if (Btn.Text == "12") { ActiveControl.Text = "00";  }
                            else { ActiveControl.Text += Btn.Text; };
                        }
                    }
                    else if (AMPM.Text == "PM")
                    {
                        ActiveControl = TxtHour;
                        ActiveControl.Text = "";

                        int intTxt1 = Convert.ToInt32(Btn.Text);
                        int intTxt2 = intTxt1 + 12;
                        if (Btn.Text == "12") { ActiveControl.Text += Btn.Text; }
                        else { ActiveControl.Text += intTxt2.ToString(); };
                        
                    }
                 break;

                case "Min":
                    ActiveControl = TxtMin;
                    ActiveControl.Text = "";

                    ActiveControl.Text += Btn.Text;

                    break;

                case "Up":
                    ActiveControl = TxtMin;
                    string txtup = string.IsNullOrEmpty(TxtMin.Text) ? "0" : TxtHour.Text;
                    if (txtup == "0") return;
                    if (Convert.ToInt32(ActiveControl.Text) >= 59) ActiveControl.Text = "-1";
                    try
                    {
                        string strTxt = ActiveControl.Text;
                        int intTxt = Convert.ToInt32(strTxt);
                        int intTxt_plus = intTxt + 1;
                        if(intTxt_plus < 10) { ActiveControl.Text = "0" + Convert.ToString(intTxt_plus); }
                        if(intTxt_plus >= 10) { ActiveControl.Text = Convert.ToString(intTxt_plus); }
                    }
                    catch
                    { }
                    break;

                case "Down":
                    ActiveControl = TxtMin;
                    string txtdown = string.IsNullOrEmpty(TxtMin.Text) ? "0" : TxtHour.Text;
                    if (txtdown == "0") return;
                    if (Convert.ToInt32(ActiveControl.Text) == 0) ActiveControl.Text = "60";
                    try
                    {
                        string strTxt = ActiveControl.Text;
                        int intTxt = Convert.ToInt32(strTxt);
                        int intTxt_plus = intTxt - 1;
                        if (intTxt_plus < 10) { ActiveControl.Text = "0" + Convert.ToString(intTxt_plus); }
                        if (intTxt_plus >= 10) { ActiveControl.Text = Convert.ToString(intTxt_plus); }
                    }
                    catch
                    { }
                    break;
            }
        }

        private void TxtHour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TxtHour.Text == "") return;
            if (Convert.ToInt32(TxtHour.Text) < 12 || Convert.ToInt32(TxtHour.Text) == 0)
            {
                AMPM.Text = "AM";
                this.BtnHourAM.Appearance.BackColor = System.Drawing.Color.Silver;
                this.BtnHourPM.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            else if (Convert.ToInt32(TxtHour.Text) >= 12 && 23 >= Convert.ToInt32(TxtHour.Text))
            {
                AMPM.Text = "PM";
                this.BtnHourPM.Appearance.BackColor = System.Drawing.Color.Silver;
                this.BtnHourAM.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            }

            // 숫자 또는 백스페이스를 누를 때만 입력 허용
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 입력을 무시합니다.
            }
        }

        private void TxtMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TxtHour.Text == "") return;
            // 숫자 또는 백스페이스를 누를 때만 입력 허용
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 입력을 무시합니다.
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