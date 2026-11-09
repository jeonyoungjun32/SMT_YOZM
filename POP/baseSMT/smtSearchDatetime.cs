using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Model;
using static System.Net.WebRequestMethods;

namespace baseSMT
{
    public enum DatetimeType
    {
        Date, DateTime, Time, Month, Year
    }
    public partial class smtSearchDatetime : DevExpress.XtraEditors.XtraUserControl
    {
        private const int SMT_DateWidth = 130;
        private const int SMT_DateMonthWidth = 130;
        private const int SMT_DateYearWidth = 130;
        private const int SMT_DateTimeWidth = 180;
        private const int SMT_TimeWidth = 130;
        private const string SMT_DateFormat = "yyyy-MM-dd";
        private const string SMT_DateMonthFormat = "yyyy-MM";
        private const string SMT_DateYeraFormat = "yyyy";
        private const string SMT_DateTimeFormath = "yyyy-MM-dd HH:mm:ss";
        private const string SMT_TimeFormath = "HH:mm:ss";

        public DatetimeType SMT_DatetimeTypeStart = DatetimeType.Date;
        public DatetimeType SMT_DatetimeTypeEnd = DatetimeType.Date;

        public event EventHandler EditValueChanged;

        private bool popMode = false;

        //콤보타이틀
        [Category("1.smtCombo")]
        public bool smtComboTitleVisible
        {
            get { return barEditType.Visible; }
            set { barEditType.Visible = value; }
        }
        [Category("1.smtCombo")]
        public string smtComboTitleValue
        {
            get { return barEditType.Text; }
            set { barEditType.Text = value; }
        }
        [Category("1.smtCombo")]
        public int smtComboTitleWidth
        {
            get { return barEditType.Width; }
            set { barEditType.Width = value; }
        }
        [Category("1.smtCombo")]
        public string[] smtComboTitleItems
        {
            get
            {
                string[] data = new string[barEditType.Properties.Items.Count];
                for (int i = 0; i < barEditType.Properties.Items.Count; i++)
                {
                    data[i] = barEditType.Properties.Items[i].ToString();
                }
                return data;
            }
            set
            {
                string[] data = value;
                barEditType.Properties.Items.Clear();
                foreach (string item in data)
                {
                    barEditType.Properties.Items.Add(item);
                }
            }
        }
        [Category("1.smtCombo")]
        public DevExpress.Utils.HorzAlignment smtComboTitleAlignment
        {
            get { return barEditType.Properties.Appearance.TextOptions.HAlignment; }
            set { barEditType.Properties.Appearance.TextOptions.HAlignment = value; }
        }

        //라벨 타이틀
        [Category("2.smtLabel")]
        public bool smtLabelTitleVisible
        {
            get { return lbl_Title.Visible; }
            set { lbl_Title.Visible = value; }
        }
        [Category("2.smtLabel")]
        public string smtLabelTitleValue
        {
            get { return lbl_Title.Text; }
            set { lbl_Title.Text = value; }
        }
        [Category("2.smtLabel")]
        public int smtLabelTitleWidth
        {
            get { return lbl_Title.Width; }
            set { lbl_Title.Width = value; }
        }

        //시작시간
        [Category("3.smtStartDateTime")]
        public bool smtStartDateTimeVisible
        {
            get { return barEditStartDate.Visible; }
            set { barEditStartDate.Visible = value; }
        }
        [Category("3.smtStartDateTime")]
        public DateTime smtStartDateTimeValue
        {
            get { return barEditStartDate.DateTime; }
            set { barEditStartDate.DateTime = value; }
        }
        [Category("3.smtStartDateTime")]
        public DatetimeType smtStartDateTimeEditingTime
        {
            get
            {
                return SMT_DatetimeTypeStart;
            }
            set
            {
                SMT_DatetimeTypeStart = value;

                if (SMT_DatetimeTypeStart == DatetimeType.Date)
                {
                    barEditStartDate.Properties.CalendarDateEditing = true;
                    barEditStartDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
                    barEditStartDate.Properties.MaskSettings.MaskExpression = SMT_DateFormat;
                    barEditStartDate.Width = SMT_DateWidth;
                }
                else if (SMT_DatetimeTypeStart == DatetimeType.DateTime)
                {
                    barEditStartDate.Properties.CalendarDateEditing = true;
                    barEditStartDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
                    barEditStartDate.Properties.MaskSettings.MaskExpression = SMT_DateTimeFormath;
                    barEditStartDate.Width = SMT_DateTimeWidth;
                }
                else if (SMT_DatetimeTypeStart == DatetimeType.Time)
                {
                    barEditStartDate.Properties.CalendarDateEditing = false;
                    barEditStartDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
                    barEditStartDate.Properties.MaskSettings.MaskExpression = SMT_TimeFormath;
                    barEditStartDate.Width = SMT_TimeWidth;
                }
                else if (SMT_DatetimeTypeStart == DatetimeType.Month)
                {
                    barEditStartDate.Properties.CalendarDateEditing = true;
                    barEditStartDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
                    barEditStartDate.Properties.MaskSettings.MaskExpression = SMT_DateMonthFormat;

                    barEditStartDate.Width = SMT_DateMonthWidth;
                    barEditStartDate.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
                }
                else if (SMT_DatetimeTypeStart == DatetimeType.Year)
                {
                    barEditStartDate.Properties.CalendarDateEditing = true;
                    barEditStartDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
                    barEditStartDate.Properties.MaskSettings.MaskExpression = SMT_DateYeraFormat;

                    barEditStartDate.Width = SMT_DateYearWidth;
                    barEditStartDate.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
                }
            }
        }

        //종료시간
        [Category("4.smtEndDateTime")]
        public bool smtEndDateTimeVisible
        {
            get
            { 
                return barEditEndDate.Visible;
            }
            set 
            {
                barEditEndDate.Visible = value;
            }
        }
        [Category("4.smtEndDateTime")]
        public DateTime smtEndDateTimeValue
        {
            get { return barEditEndDate.DateTime; }
            set { barEditEndDate.DateTime = value; }
        }
        [Category("4.smtEndDateTime")]
        public DatetimeType smtEndDateTimeEditingTime
        {
            get
            {
                return SMT_DatetimeTypeEnd;
            }
            set
            {
                SMT_DatetimeTypeEnd = value;

                if (SMT_DatetimeTypeEnd == DatetimeType.Date)
                {
                    barEditEndDate.Properties.CalendarDateEditing = true;
                    barEditEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
                    barEditEndDate.Properties.MaskSettings.MaskExpression = SMT_DateFormat;
                    barEditEndDate.Width = SMT_DateWidth;
                }
                else if (SMT_DatetimeTypeEnd == DatetimeType.DateTime)
                {
                    barEditEndDate.Properties.CalendarDateEditing = true;
                    barEditEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
                    barEditEndDate.Properties.MaskSettings.MaskExpression = SMT_DateTimeFormath;
                    barEditEndDate.Width = SMT_DateTimeWidth;
                }
                else if (SMT_DatetimeTypeEnd == DatetimeType.Time)
                {
                    barEditEndDate.Properties.CalendarDateEditing = false;
                    barEditEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
                    barEditEndDate.Properties.MaskSettings.MaskExpression = SMT_TimeFormath;
                    barEditEndDate.Width = SMT_TimeWidth;
                }
                else if (SMT_DatetimeTypeStart == DatetimeType.Month)
                {
                    barEditEndDate.Properties.CalendarDateEditing = true;
                    barEditEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
                    barEditEndDate.Properties.MaskSettings.MaskExpression = SMT_DateMonthFormat;

                    barEditEndDate.Width = SMT_DateMonthWidth;
                    barEditEndDate.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
                }
                else if (SMT_DatetimeTypeStart == DatetimeType.Year)
                {
                    barEditEndDate.Properties.CalendarDateEditing = true;
                    barEditEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
                    barEditEndDate.Properties.MaskSettings.MaskExpression = SMT_DateYeraFormat;

                    barEditEndDate.Width = SMT_DateYearWidth;
                    barEditEndDate.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
                }
            }
        }
        [Category("4.smtEndDateTime")]
        public DateEdit smtStartDateEdit
        {
            get { return barEditStartDate; }
            set { barEditStartDate = value; }
        }
        [Category("4.smtEndDateTime")]
        public DateEdit smtStartEndEdit
        {
            get { return barEditEndDate; }
            set { barEditEndDate = value; }
        }

        [Category("Custom"), Description("POP 모드 여부 (폰트/사이즈 확대)")]
        public bool PopMode
        {
            get => popMode;
            set
            {
                popMode = value;
                ApplyStyle();
            }
        }

        public smtSearchDatetime()
        {
            InitializeComponent();

            barEditStartDate.EditValueChanged += (s, e) => OnEditValueChanged(s, e);
            barEditEndDate.EditValueChanged += (s, e) => OnEditValueChanged(s, e);

            barEditStartDate.DateTime = DateTime.Now;
            barEditEndDate.DateTime = DateTime.Now;

            this.SizeChanged += new EventHandler(fnSizeChanged);
            barEditType.SizeChanged += new EventHandler(fnSizeChanged);
            lbl_Title.SizeChanged += new EventHandler(fnSizeChanged);
            barEditStartDate.SizeChanged += new EventHandler(fnSizeChanged);
            barEditEndDate.SizeChanged += new EventHandler(fnSizeChanged);

            barEditType.VisibleChanged += new EventHandler(fnSizeChanged);
            lbl_Title.VisibleChanged += new EventHandler(fnSizeChanged);
            barEditStartDate.VisibleChanged += new EventHandler(fnSizeChanged);
            barEditEndDate.VisibleChanged += new EventHandler(fnSizeChanged);

            this.BringToFront();
            this.Dock = DockStyle.Left;
        }
        private void OnEditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged?.Invoke(sender, e);
        }

        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            int _height = barEditType.Height;
            int _top = (this.Height - _height) / 2;

            int width1 = barEditType.Visible ? barEditType.Width : 0;
            int width2 = lbl_Title.Visible ? lbl_Title.Width : 0;
            int width3 = barEditStartDate.Visible ? barEditStartDate.Width : 0;
            int width4 = barEditEndDate.Visible ? barEditEndDate.Width + 5 : 0;

            barEditType.Location = new Point(0, _top);
            lbl_Title.Location = new Point(width1, 0);
            lbl_Title.Height = this.Height;
            barEditStartDate.Location = new Point(width1 + width2, _top);
            barEditEndDate.Location = new Point(width1 + width2 + width3 + 5, _top);

            this.Width = width1 + width2 + width3 + width4;
        }

        private void ApplyStyle()
        {
            if (!this.IsHandleCreated) return;

            if (popMode)
            {
				SetPOPDtpFont(barEditStartDate);
                SetPOPDtpFont(barEditEndDate);
            }
		}

        private void SetPOPDtpFont(DateEdit dtp)
		{
            //달력 디자인 변경
            //클릭이벤트 추가(화살표가 아닌 다른영역을 클릭해도 팝업활성화)
            dtp.MouseDown += (sender, e) => DateEdit_MouseDown(sender, e);
            dtp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //달력 팝업 크기 키우기
            dtp.Properties.CellSize = new Size(70, 60);
            //달력 날짜 크기 키우기
            int size = 8;
            dtp.Properties.AppearanceCalendar.DayCellHoliday.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCell.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellHighlighted.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellSelected.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellPressed.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellToday.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellToday.BackColor = Color.LightGray;
            dtp.Properties.AppearanceCalendar.Header.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.Header.ForeColor = Color.Black;
            dtp.Properties.AppearanceCalendar.HeaderPressed.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.HeaderHighlighted.FontSizeDelta = size;

            //날짜데이터 입력 편의성
            dtp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
        }

        private void DateEdit_MouseDown(object sender, MouseEventArgs e)
        {
            DateEdit dtp = sender as DateEdit;
            if (dtp == null) return;

            // MouseDown 이벤트가 발생한 위치가 화살표 버튼이 아닌 다른 UI인 경우
            if (e.Button == MouseButtons.Left && e.X > dtp.Width - dtp.Properties.Buttons[0].Width)
            {
                // 팝업 창 띄우기
                dtp.ShowPopup();
            }
        }
    }
}
