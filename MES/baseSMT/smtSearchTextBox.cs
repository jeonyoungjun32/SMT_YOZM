using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace baseSMT
{
    public partial class smtSearchTextBox : DevExpress.XtraEditors.XtraUserControl
    {
        private bool _Must = false; //필수입력
        private bool _smtTextBoxNumber = false; //숫자
        private int _smtTextBoxDecimal = 0;//소수점 자리수

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

        //텍스트박스
        [Category("2.smtTextBox")]
        public string smtTextBoxValue
        {
            get { return txt_Value.EditValue==null?"": txt_Value.EditValue.ToString(); }
            set { txt_Value.Text = value; }
        }
        [Category("2.smtTextBox")]
        public bool smtTextBoxReadOnly
        {
            get { return txt_Value.Properties.ReadOnly; }
            set
            {
                txt_Value.Properties.ReadOnly = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtTextBox"), Description("필수입력")]
        public bool smtTextBoxMust
        {
            get { return _Must; }
            set
            {
                _Must = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtTextBox"), Description("숫자")]
        public bool smtTextBoxNumber
        {
            get { return _smtTextBoxNumber; }
            set
            {
                _smtTextBoxNumber = value;
                NumerChanged();
            }
        }
        [Category("2.smtTextBox"), Description("숫자일때 소수점 자리수")]
        public int smtTextBoxDecimal
        {
            get { return _smtTextBoxDecimal; }
            set
            {
                _smtTextBoxDecimal = value;
                NumerChanged();
            }
        }
        [Category("2.smtTextBox")]
        public event EventHandler smtTextBoxValueChanged;

        public smtSearchTextBox()
        {
            InitializeComponent();

            this.SizeChanged += new EventHandler(fnSizeChanged);
            lbl_Title.SizeChanged += new EventHandler(fnSizeChanged);
            txt_Value.SizeChanged += new EventHandler(fnSizeChanged);

            lbl_Title.VisibleChanged += new EventHandler(fnSizeChanged);
            txt_Value.VisibleChanged += new EventHandler(fnSizeChanged);

            txt_Value.TextChanged += new EventHandler(fnTextChanged);

            this.BringToFront();
            this.Dock = DockStyle.Left;
        }
        private void NumerChanged()
        {
            if (_smtTextBoxNumber)
            {
                txt_Value.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
                txt_Value.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
                txt_Value.Properties.MaskSettings.Set("useAdvancingCaret", true);
                txt_Value.Properties.MaskSettings.Set("mask", "N" + _smtTextBoxDecimal.ToString());
                txt_Value.Properties.UseMaskAsDisplayFormat = true;
                txt_Value.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
            else
            {
                txt_Value.Properties.ResetMaskSettings();
                txt_Value.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }
        }

        private void fnMustReadChanged()
        {
            if (smtTextBoxMust)
            {
                txt_Value.Properties.Appearance.BackColor = smtDesignSetting.MustColor;
            }
            else
            {
                if (smtTextBoxReadOnly) txt_Value.Properties.Appearance.BackColor = smtDesignSetting.ReadOnlyColor;
                else txt_Value.Properties.Appearance.BackColor = System.Drawing.Color.White;
            }
        }

        private void fnTextChanged(object sender, EventArgs eventargs)
        {
            if (smtTextBoxValueChanged != null)
            {
                smtTextBoxValueChanged.Invoke(sender, eventargs);
            }
        }
        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            int _height = txt_Value.Height;
            int _top = (this.Height - _height) / 2;

            int width1 = lbl_Title.Visible ? lbl_Title.Width : 0;
            int width2 = txt_Value.Visible ? txt_Value.Width : 0;

            txt_Value.Location = new Point(width1, _top);
            txt_Value.Width = this.Width - width1;
        }
    }
}
