using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using DevExpress.XtraEditors;

namespace baseSMT
{
    public class smtCommonBar : DevExpress.XtraEditors.PanelControl
    {
        private DevExpress.XtraEditors.SimpleButton pic_Title;
        private bool myImageTitleAutoText = true;

        //이미지 타이틀
        [Category("1.smtImageTitle")]
        public bool smtImageTitleVisible
        {
            get { return pic_Title.Visible; }
            set { pic_Title.Visible = value; }
        }
        [Category("1.smtImageTitle")]
        [Editor("DevExpress.Utils.Design.SvgImageEditor, " + AssemblyInfo.SRAssemblyDesign, typeof(UITypeEditor))]
        public DevExpress.Utils.Svg.SvgImage smtImageTitleSvgImage
        {
            get { return pic_Title.ImageOptions.SvgImage; }
            set { pic_Title.ImageOptions.SvgImage = value; }
        }
        [Category("1.smtImageTitle")]
        public string smtImageTitleText
        {
            get { return pic_Title.Text; }
            set { pic_Title.Text = value; }
        }
        [Category("1.smtImageTitle")]
        [DefaultValue(true)]
        public bool smtImageTitleAutoText
        {
            get
            {
                return myImageTitleAutoText;
            }
            set
            {
                myImageTitleAutoText = value;
            }
        }
        public smtCommonBar()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(baseSMT));

            this.pic_Title = new DevExpress.XtraEditors.SimpleButton();
            this.pic_Title.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.pic_Title.Appearance.ForeColor = System.Drawing.Color.White;
            this.pic_Title.Appearance.Options.UseBackColor = true;
            this.pic_Title.Appearance.Options.UseForeColor = true;
            this.pic_Title.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.pic_Title.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar")));
            this.pic_Title.Name = "pic_Title";
            this.pic_Title.Text = "";
            this.pic_Title.AutoSize = true;
            this.pic_Title.AllowFocus = false;
            this.Controls.Add(this.pic_Title);
            this.pic_Title.BringToFront();
            this.pic_Title.Dock = System.Windows.Forms.DockStyle.Left;
            this.pic_Title.Font = new System.Drawing.Font("Pretendard SemiBold", 12F, System.Drawing.FontStyle.Bold);

            this.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Dock = DockStyle.Top;

            this.SizeChanged += new EventHandler(fnSizeChanged);
            this.BackColorChanged += new EventHandler(fnBackColorChange);
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (this.pic_Title != null) this.pic_Title.SendToBack();
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
            {
                this.Parent.TextChanged += new EventHandler(ParentTextChanged);
            }
        }
        private void ParentTextChanged(object sender, EventArgs e)
        {
            if (smtImageTitleAutoText && this.Parent != null && smtImageTitleText != this.Parent.Text)
            {
                smtImageTitleText = this.Parent.Text;
            }
        }
        private void fnSizeChanged(object sender, EventArgs eventargs)
        {
            this.Height = 45;
        }
        private void fnBackColorChange(object sender, EventArgs eventargs)
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
        }
    }
}
