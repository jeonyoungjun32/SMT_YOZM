using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel;

namespace baseSMT
{
    public enum EnumBodyButtonStyles
    {
        None,
        파일찾기,
        지우기,
        중복확인
    }
    public class smtBodyButton : SimpleButton
    {
        private DevExpress.Utils.SvgImageCollection svgImageList;
        private EnumBodyButtonStyles myButtonStyles;
        private Dictionary<EnumBodyButtonStyles, int> myButtonStylesDec;
        private bool myButtonAutoText = true;

        //라벨 타이틀
        [Category("1.smtButton")]
        [DefaultValue(EnumBodyButtonStyles.None)]
        public EnumBodyButtonStyles smtButtonStyle
        {
            get
            {
                return myButtonStyles;
            }
            set
            {
                myButtonStyles = value;
                if (value != EnumBodyButtonStyles.None)
                {
                    this.ImageOptions.ImageIndex = myButtonStylesDec[value];
                    this.AutoSize = true;
                }
                else
                {
                    this.ImageOptions.ImageIndex = -1;
                    this.Text = this.Name;
                    this.Height = 32;
                    this.Width = 150;
                    this.AutoSize = false;
                }

                if (smtButtonStyle != EnumBodyButtonStyles.None && myButtonAutoText)
                {
                    this.Text = smtButtonStyle.ToString();
                }
            }
        }
        [Category("1.smtButton")]
        [DefaultValue(true)]
        public bool smtButtonAutoText
        {
            get
            {
                return myButtonAutoText;
            }
            set
            {
                myButtonAutoText = value;

                if (smtButtonStyle != EnumBodyButtonStyles.None && myButtonAutoText)
                {
                    this.Text = smtButtonStyle.ToString();
                }
            }
        }

        public smtBodyButton()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(baseSMT));
            svgImageList = new DevExpress.Utils.SvgImageCollection();
            myButtonStylesDec = new Dictionary<EnumBodyButtonStyles, int>();
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonSearch")));
            myButtonStylesDec[EnumBodyButtonStyles.파일찾기] = 0;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonDelete")));
            myButtonStylesDec[EnumBodyButtonStyles.지우기] = 1;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonKeyCheck")));
            myButtonStylesDec[EnumBodyButtonStyles.중복확인] = 2;

            this.Appearance.BackColor = System.Drawing.Color.White; 
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.ImageOptions.ImageList = svgImageList;
            this.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            this.Font = new System.Drawing.Font("Pretendard SemiBold", 10.2F);
            this.Height = 32;
            this.Width = 150;
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ImageOptions.ImageToTextIndent = 15;
            this.AutoSize = false;
            this.AllowFocus = false;
            this.BringToFront();

            if (smtButtonStyle != EnumBodyButtonStyles.None && myButtonAutoText)
            {
                this.Text = smtButtonStyle.ToString();
            }
        }
        protected override void InitLayout()
        {
            base.InitLayout();
            if (this.ImageOptions.ImageList == null)
            {
                this.ImageOptions.ImageList = svgImageList;
            }
        }
    }
}
