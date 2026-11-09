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
using DevExpress.Data.Mask;
using DevExpress.XtraEditors;

namespace baseSMT
{
    public partial class smtBodyCheckBox : DevExpress.XtraEditors.XtraUserControl
    {
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

        //체크박스
        [Category("2.smtCheckEdit")]
        public bool smtCheckEditChecked
        {
            get { return ckEdit.Checked; }
            set { ckEdit.Checked = value; }
        }
        [Category("2.smtCheckEdit")]
        public string smtCheckEditDisplayValueChecked
        {
            get { return ckEdit.Properties.DisplayValueChecked; }
            set { ckEdit.Properties.DisplayValueChecked = value; }
        }
        [Category("2.smtCheckEdit")]
        public string smtCheckEditDisplayValueUnchecked
        {
            get { return ckEdit.Properties.DisplayValueUnchecked; }
            set { ckEdit.Properties.DisplayValueUnchecked = value; }
        }
        [Category("2.smtCheckEdit")]
        [Editor("System.Drawing.Design.UITypeEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ParenthesizePropertyName(true)]
        [RefreshProperties(RefreshProperties.All)]
        public ControlBindingsCollection smtCheckEditDataDataBindings
        {
            get { return ckEdit.DataBindings; }
        }
        [Category("2.smtCheckEdit")]
        public event EventHandler smtCheckEditCheckedChanged;

        public smtBodyCheckBox()
        {
            InitializeComponent();
            ckEdit.CheckedChanged += new EventHandler(fnCheckedChanged);
            this.SizeChanged += new EventHandler(fnSizeChanged);
        }
        private void fnCheckedChanged(object sender, EventArgs eventargs)
        {
            if (smtCheckEditCheckedChanged != null)
            {
                smtCheckEditCheckedChanged.Invoke(sender, eventargs);
            }
        }
        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            this.Height = 32;
        }
    }
}
