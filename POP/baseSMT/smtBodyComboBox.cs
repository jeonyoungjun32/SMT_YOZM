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
    public partial class smtBodyComboBox : DevExpress.XtraEditors.XtraUserControl
    {
        private int _DefaultSelectIndex = -1;

        private bool _Must = false; //필수입력

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

        //콤보박스
        [Category("2.smtComboBox")]
        public string smtComboBoxValue
        {
            get { return cmb_Value.Text; }
            set { cmb_Value.Text = value; }
        }
        [Category("2.smtComboBox")]
        public string[] smtComboBoxItems
        {
            get
            {
                string[] data = new string[cmb_Value.Properties.Items.Count];
                for (int i = 0; i < cmb_Value.Properties.Items.Count; i++)
                {
                    data[i] = cmb_Value.Properties.Items[i].ToString();
                }
                return data;
            }
            set
            {
                string[] data = value;
                cmb_Value.Properties.Items.Clear();
                foreach (string item in data)
                {
                    cmb_Value.Properties.Items.Add(item);
                }
                if (_DefaultSelectIndex >= 0 && cmb_Value.Properties.Items.Count > _DefaultSelectIndex)
                {
                    this.smtComboBoxValue = cmb_Value.Properties.Items[_DefaultSelectIndex].ToString();
                }
            }
        }
        [Category("2.smtComboBox")]
        public DevExpress.Utils.HorzAlignment smtComboBoxAlignment
        {
            get { return cmb_Value.Properties.Appearance.TextOptions.HAlignment; }
            set 
            { 
                cmb_Value.Properties.Appearance.TextOptions.HAlignment = value;
                cmb_Value.Properties.AppearanceDropDown.TextOptions.HAlignment = value;
            }
        }
        [Category("2.smtComboBox")]
        public bool smtComboBoxReadOnly
        {
            get { return cmb_Value.Properties.ReadOnly; }
            set 
            { 
                cmb_Value.Properties.ReadOnly = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtComboBox"), Description("필수입력")]
        public bool smtComboBoxMust
        {
            get { return _Must; }
            set 
            { 
                _Must = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtComboBox")]
        [Editor("System.Drawing.Design.UITypeEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ParenthesizePropertyName(true)]
        [RefreshProperties(RefreshProperties.All)]
        public ControlBindingsCollection smtComboBoxDataDataBindings
        {
            get { return cmb_Value.DataBindings; }
        }
        [Category("2.smtComboBox")]
        public event EventHandler smtComboBoxTextChanged;

        public smtBodyComboBox()
        {
            InitializeComponent();
            cmb_Value.TextChanged += new EventHandler(fnTextChanged);
            this.SizeChanged += new EventHandler(fnSizeChanged);
        }
        private void fnMustReadChanged()
        {
            if (smtComboBoxMust)
            {
                cmb_Value.Properties.Appearance.BackColor = smtDesignSetting.MustColor;
            }
            else
            {
                if (smtComboBoxReadOnly) cmb_Value.Properties.Appearance.BackColor = smtDesignSetting.ReadOnlyColor;
                else cmb_Value.Properties.Appearance.BackColor = System.Drawing.Color.White;
            }
        }
        private void fnTextChanged(object sender, EventArgs eventargs)
        {
            if (smtComboBoxTextChanged != null)
            {
                smtComboBoxTextChanged.Invoke(sender, eventargs);
            }
        }
        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            this.Height = 32;
        }
    }
}
