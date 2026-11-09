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
    public enum EnumButtonStyles
    {
        None,
        조회,
        추가,
        수정,
        삭제,
        저장,
        닫기,
        확정,
        확정취소,
        엑셀업로드,
        엑셀변환,
        프린터
    }
    public class smtSearchButton : SimpleButton
    {
        private DevExpress.Utils.SvgImageCollection svgImageList;
        private EnumButtonStyles myButtonStyles;
        private Dictionary<EnumButtonStyles, int> myButtonStylesDec;
        private bool myButtonAutoText = true;

        //라벨 타이틀
        [Category("1.smtButton")]
        [DefaultValue(EnumButtonStyles.None)]
        public EnumButtonStyles smtButtonStyle
        {
            get
            {
                return myButtonStyles;
            }
            set
            {
                myButtonStyles = value;
                if (value != EnumButtonStyles.None)
                {
                    this.ImageOptions.ImageIndex = myButtonStylesDec[value];
                }
                else
                {
                    this.ImageOptions.ImageIndex = -1;
                    this.Text = this.Name;
                }

                if (smtButtonStyle != EnumButtonStyles.None && myButtonAutoText)
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

                if (smtButtonStyle != EnumButtonStyles.None && myButtonAutoText)
                {
                    this.Text = smtButtonStyle.ToString();
                }
            }
        }

        public smtSearchButton()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(baseSMT));
            svgImageList = new DevExpress.Utils.SvgImageCollection();
            myButtonStylesDec = new Dictionary<EnumButtonStyles, int>();
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonSearch")));
            myButtonStylesDec[EnumButtonStyles.조회] = 0;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonAdd")));
            myButtonStylesDec[EnumButtonStyles.추가] = 1;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonUpdate")));
            myButtonStylesDec[EnumButtonStyles.수정] = 2;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonDelete")));
            myButtonStylesDec[EnumButtonStyles.삭제] = 3;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonSave")));
            myButtonStylesDec[EnumButtonStyles.저장] = 4;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonClose")));
            myButtonStylesDec[EnumButtonStyles.닫기] = 5;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonConfirm")));
            myButtonStylesDec[EnumButtonStyles.확정] = 6;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonConfirmCencel")));
            myButtonStylesDec[EnumButtonStyles.확정취소] = 7;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonExcelUpload")));
            myButtonStylesDec[EnumButtonStyles.엑셀업로드] = 8;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonExcelExport")));
            myButtonStylesDec[EnumButtonStyles.엑셀변환] = 9;
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonPrint")));
            myButtonStylesDec[EnumButtonStyles.프린터] = 10;

            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseForeColor = true;
            this.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.ImageOptions.ImageList = svgImageList;
            this.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ImageOptions.ImageToTextIndent = 15;
            this.AutoSize = true;
            this.AllowFocus = false;
            this.BringToFront();

            this.BringToFront();
            this.Dock = DockStyle.Right;

            if (smtButtonStyle != EnumButtonStyles.None && myButtonAutoText)
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
