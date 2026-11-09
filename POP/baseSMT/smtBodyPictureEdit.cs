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
    public partial class smtBodyPictureEdit : DevExpress.XtraEditors.XtraUserControl
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

        //픽쳐박스
        [Category("2.smtPictureEdit")]
        [Editor("DevExpress.Utils.Design.DXImageEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        public Image smtPictureEditImage
        {
            get { return picEdit.Image; }
            set { picEdit.Image = value; }
        }
        [Category("2.smtPictureEdit")]
        [DefaultValue(DevExpress.XtraEditors.Controls.PictureStoreMode.Default)]
        public DevExpress.XtraEditors.Controls.PictureStoreMode smtPictureEditPictureStoreMode
        {
            get { return picEdit.Properties.PictureStoreMode; }
            set { picEdit.Properties.PictureStoreMode = value; }
        }

        [Category("2.smtPictureEdit")]
        public DevExpress.XtraEditors.Controls.PictureSizeMode smtPictureEditSizeMode
        {
            get { return picEdit.Properties.SizeMode; }
            set { picEdit.Properties.SizeMode = value; }
        }
        [Category("2.smtPictureEdit")]
        public bool smtPictureEditReadOnly
        {
            get { return picEdit.Properties.ReadOnly; }
            set 
            { 
                picEdit.Properties.ReadOnly = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtPictureEdit"), Description("필수입력")]
        public bool smtPictureEditMust
        {
            get { return _Must; }
            set 
            { 
                _Must = value;
                fnMustReadChanged();
            }
        }
        [Category("2.smtPictureEdit")]
        [Editor("System.Drawing.Design.UITypeEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [ParenthesizePropertyName(true)]
        [RefreshProperties(RefreshProperties.All)]
        public ControlBindingsCollection smtPictureEditDataDataBindings
        {
            get { return picEdit.DataBindings; }
        }

        public smtBodyPictureEdit()
        {
            InitializeComponent();
        }
        private void fnMustReadChanged()
        {
            if (smtPictureEditMust)
            {
                picEdit.Properties.Appearance.BackColor = smtDesignSetting.MustColor;
            }
            else
            {
                if (smtPictureEditReadOnly) picEdit.Properties.Appearance.BackColor = smtDesignSetting.ReadOnlyColor;
                else picEdit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            }
        }
    }
}
