using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;
using DevExpress.XtraEditors;

namespace baseSMT
{
    [Browsable(false)]
    public partial class smtBodyDatetime : DevExpress.XtraEditors.XtraUserControl
    {
        private int _DefaultSelectIndex = -1;

        private bool _Must = false; //필수입력

        private const string SMT_DateFormat = "yyyy-MM-dd";
        private const string SMT_DateTimeFormath = "yyyy-MM-dd HH:mm:ss";
        private const string SMT_TimeFormath = "HH:mm:ss";

        public DatetimeType SMT_DatetimeType = DatetimeType.Date;

        //라벨 타이틀
        [Category("1.smtLabel")]
        public bool smtLabelTitleVisible
        {
            get { return lbl_Title.Visible; }
            set { lbl_Title.Visible = value; }
        }
        [Category("1.smtLabel")]
        public string smtLabelTitleValue
        {
            get { return lbl_Title.Text; }
            set { lbl_Title.Text = value; }
        }
        [Category("1.smtLabel")]
        public int smtLabelTitleWidth
        {
            get { return lbl_Title.Width; }
            set { lbl_Title.Width = value; }
        }

        //타임박스
        [Category("2.smtDateTime")]
        public DateTime smtDateTimeValue
        {
            get { return barEditDate.DateTime; }
            set { barEditDate.DateTime = value; }
        }
        [Category("2.smtDateTime")]
        public DatetimeType smtDateTimeEditingTime
        {
            get
            {
                return SMT_DatetimeType;
            }
            set
            {
                SMT_DatetimeType = value;

                if (SMT_DatetimeType == DatetimeType.Date)
                {
                    barEditDate.Properties.CalendarDateEditing = true;
                    barEditDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
                    barEditDate.Properties.MaskSettings.MaskExpression = SMT_DateFormat;
                }
                else if (SMT_DatetimeType == DatetimeType.DateTime)
                {
                    barEditDate.Properties.CalendarDateEditing = true;
                    barEditDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
                    barEditDate.Properties.MaskSettings.MaskExpression = SMT_DateTimeFormath;
                }
                else if (SMT_DatetimeType == DatetimeType.Time)
                {
                    barEditDate.Properties.CalendarDateEditing = false;
                    barEditDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
                    barEditDate.Properties.MaskSettings.MaskExpression = SMT_TimeFormath;
                }
            }
        }
        [Category("2.smtDateTime")]
        public bool smtDateTimeReadOnly
        {
            get { return barEditDate.Properties.ReadOnly; }
            set 
            { 
                barEditDate.Properties.ReadOnly = value;
                barEditDate.Properties.Buttons[0].Visible = !value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtDateTime"), Description("필수입력")]
        public bool smtDateTimeMust
        {
            get { return _Must; }
            set
            {
                _Must = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtDateTime")]
        [Editor("System.Drawing.Design.UITypeEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ParenthesizePropertyName(true)]
        [RefreshProperties(RefreshProperties.All)]
        public ControlBindingsCollection smtDateTimeDataDataBindings
        {
            get { return barEditDate.DataBindings; }
        }
        [Category("2.smtDateTime")]
        public event EventHandler smtDateTimeTextChanged;

        public smtBodyDatetime()
        {
            InitializeComponent();
            barEditDate.TextChanged += new EventHandler(fnTextChanged);
            this.SizeChanged += new EventHandler(fnSizeChanged);
        }
        private void fnMustReadChanged()
        {
            if (smtDateTimeMust)
            {
                barEditDate.Properties.Appearance.BackColor = smtDesignSetting.MustColor;
            }
            else
            {
                if (smtDateTimeReadOnly) barEditDate.Properties.Appearance.BackColor = smtDesignSetting.ReadOnlyColor;
                else barEditDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            }
        }
        private void fnTextChanged(object sender, EventArgs eventargs)
        {
            if (smtDateTimeTextChanged != null)
            {
                smtDateTimeTextChanged.Invoke(sender, eventargs);
            }
        }
        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            this.Height = 32;
        }
    }
}
