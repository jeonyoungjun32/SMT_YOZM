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
    public partial class smtBodyMemoEdit : DevExpress.XtraEditors.XtraUserControl
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
        [Category("2.smtMemoEdit")]
        public string smtMemoValue
        {
            get { return txt_Memo.Text; }
            set { txt_Memo.Text = value; }
        }
        [Category("2.smtMemoEdit")]
        public bool smtMemoReadOnly
        {
            get { return txt_Memo.Properties.ReadOnly; }
            set 
            { 
                txt_Memo.Properties.ReadOnly = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtMemoEdit"), Description("필수입력")]
        public bool smtMemoMust
        {
            get { return _Must; }
            set 
            { 
                _Must = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtMemoEdit")]
        [Editor("System.Drawing.Design.UITypeEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ParenthesizePropertyName(true)]
        [RefreshProperties(RefreshProperties.All)]
        public ControlBindingsCollection smtMemoDataDataBindings
        {
            get { return txt_Memo.DataBindings; }
        }

        public smtBodyMemoEdit()
        {
            InitializeComponent();
        }
        private void fnMustReadChanged()
        {
            if (smtMemoMust)
            {
                txt_Memo.Properties.Appearance.BackColor = smtDesignSetting.MustColor;
            }
            else
            {
                if (smtMemoReadOnly) txt_Memo.Properties.Appearance.BackColor = smtDesignSetting.ReadOnlyColor;
                else txt_Memo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            }
        }
    }
}
