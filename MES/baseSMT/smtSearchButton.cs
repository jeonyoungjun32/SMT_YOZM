using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel;
using DevExpress.Utils;

namespace baseSMT
{
    public enum EnumButtonStyles
    {
        None,
        조회,
        등록,
        수정,
        삭제,
        저장,
        닫기,
        확정,
        확정취소,
        엑셀업로드,
        엑셀변환,
        프린터,
        전송
    }
    public class smtSearchButton : SimpleButton
    {
        private DevExpress.Utils.SvgImageCollection svgImageList;
        private EnumButtonStyles myButtonStyles;
        private Dictionary<EnumButtonStyles, int> myButtonStylesDec;
        private bool myButtonAutoText = true;
        private bool myUseStyle = true;

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
        [Category("1.smtButton")]
        [DefaultValue(true)]
        public bool smtUseStyle
        {
            get
            {
                return myUseStyle;
            }
            set
            {
                myUseStyle = value;
                ApplyStyle();
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
            myButtonStylesDec[EnumButtonStyles.등록] = 1;
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
            svgImageList.Add((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtButtonSend1")));
            myButtonStylesDec[EnumButtonStyles.전송] = 11;

            this.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.ImageOptions.ImageList = svgImageList;
            this.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ImageOptions.ImageToTextIndent = 15;
            this.AutoSize = true;
            this.AllowFocus = false;

            ApplyStyle();

            if (smtButtonStyle != EnumButtonStyles.None && myButtonAutoText)
            {
                this.Text = smtButtonStyle.ToString();
            }
        }

        private void ApplyStyle()
        {
            if (myUseStyle)
            {
                this.Appearance.BackColor = System.Drawing.Color.FromArgb(49, 63, 92);
                this.Appearance.ForeColor = System.Drawing.Color.White;
                this.Appearance.Options.UseBackColor = true;
                this.Appearance.Options.UseForeColor = true;

                this.Padding = new Padding(10, 0, 10, 0);
                this.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;

                // 기존처럼 강제하고 싶으면 유지
                this.Dock = DockStyle.Right;
                this.BringToFront();
            }
            else
            {
                // 스타일 강제 적용 안함: 디자이너/컨테이너 설정을 그대로 살림
                // 중요한 건 UseBackColor/UseForeColor를 false로 꺼줘야 스킨/상위 스타일이 먹음
                this.Appearance.Options.UseBackColor = false;
                this.Appearance.Options.UseForeColor = false;

                // Dock/Padding/PaintStyle도 건드리지 않음
                // Dock을 이전에 Right로 강제했던 흔적이 남을 수 있으니,
                // 원하면 아래 한 줄만 풀어서 디폴트로 되돌릴 수 있음:
                // this.Dock = DockStyle.None;
            }
        }

        /// <summary>
        /// 외부에서 호출하여 버튼의 툴팁(제목, 설명)을 설정합니다.
        /// </summary>
        /// <param name="title">툴팁 상단에 표시될 제목</param>
        /// <param name="description">툴팁 본문에 표시될 상세 설명</param>
        public void SetToolTip(string title, string description)
        {
            // 1. SuperToolTip 객체 생성
            SuperToolTip sTip = new SuperToolTip();

            // 2. 제목(Header) 설정
            if (!string.IsNullOrEmpty(title))
            {
                ToolTipTitleItem titleItem = new ToolTipTitleItem();
                titleItem.Text = title;
                sTip.Items.Add(titleItem);
            }

            // 3. 내용(Contents) 설정
            if (!string.IsNullOrEmpty(description))
            {
                ToolTipItem contentItem = new ToolTipItem();
                contentItem.Text = description;
                // HTML 태그 사용을 허용하고 싶다면 아래 주석 해제
                // contentItem.AllowHtmlText = DefaultBoolean.True; 
                sTip.Items.Add(contentItem);
            }

            // 4. 버튼에 할당
            this.SuperTip = sTip;
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
