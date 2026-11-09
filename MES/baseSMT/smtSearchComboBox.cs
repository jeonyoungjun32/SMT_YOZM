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
    public partial class smtSearchComboBox : DevExpress.XtraEditors.XtraUserControl
    {
        private int _DefaultSelectIndex = 0;

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
        public event EventHandler smtComboBoxTextChanged;

        public smtSearchComboBox()
        {
            InitializeComponent();
            this.SizeChanged += new EventHandler(fnSizeChanged);
            lbl_Title.SizeChanged += new EventHandler(fnSizeChanged);
            cmb_Value.SizeChanged += new EventHandler(fnSizeChanged);

            lbl_Title.VisibleChanged += new EventHandler(fnSizeChanged);
            cmb_Value.VisibleChanged += new EventHandler(fnSizeChanged);

            cmb_Value.TextChanged += new EventHandler(fnTextChanged);

            this.BringToFront();
            this.Dock = DockStyle.Left;
        }
        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            int _height = cmb_Value.Height;
            int _top = (this.Height - _height) / 2;

            int width1 = lbl_Title.Visible ? lbl_Title.Width : 0;
            int width2 = cmb_Value.Visible ? cmb_Value.Width : 0;

            cmb_Value.Location = new Point(width1, _top);
            cmb_Value.Width = this.Width - width1;
        }
        private void fnTextChanged(object sender, EventArgs eventargs)
        {
            if (smtComboBoxTextChanged != null)
            {
                smtComboBoxTextChanged.Invoke(sender, eventargs);
            }
        }
    }
}
