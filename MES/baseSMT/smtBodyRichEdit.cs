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
    public partial class smtBodyRichEdit : DevExpress.XtraEditors.XtraUserControl
    {
        //리치박스
        [Category("1.smtRichEdit")]
        public string smtRichEditText
        {
            get { return richEdit.Text; }
            set { richEdit.Text = value; }
        }
        [Category("1.smtRichEdit")]
        public string smtRichEditRtfText
        {
            get { return richEdit.RtfText; }
            set { richEdit.RtfText = value; }
        }
        [Category("1.smtRichEdit")]
        public bool smtRichEditFontVisible
        {
            get { return fontBar1.Visible; }
            set { fontBar1.Visible = value; }
        }
        [Category("1.smtRichEdit")]
        public bool smtRichEditReadOnly
        {
            get { return richEdit.ReadOnly; }
            set { richEdit.ReadOnly = value; }
        }
        [Category("1.smtRichEdit")]
        [Editor("System.Drawing.Design.UITypeEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ParenthesizePropertyName(true)]
        [RefreshProperties(RefreshProperties.All)]
        public ControlBindingsCollection smtRichEditDataDataBindings
        {
            get { return richEdit.DataBindings; }
        }

        public smtBodyRichEdit()
        {
            InitializeComponent();
        }
    }
}
