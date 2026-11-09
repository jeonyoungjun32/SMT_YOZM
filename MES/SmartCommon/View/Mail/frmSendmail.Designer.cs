
namespace SmartCommon.View.Mail
{
    partial class frmSendmail
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSendmail));
			this.layoutConverter1 = new DevExpress.XtraLayout.Converter.LayoutConverter(this.components);
			this.frmSendmaillayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
			this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
			this.BtnAdd = new DevExpress.XtraEditors.SimpleButton();
			this.lboxFile = new System.Windows.Forms.ListBox();
			this.tboxSubject = new DevExpress.XtraEditors.TextEdit();
			this.txtBody = new DevExpress.XtraRichEdit.RichEditControl();
			this.ucARhide = new SmartCommon.View.Mail.ucAddressReg();
			this.ucARreg = new SmartCommon.View.Mail.ucAddressReg();
			this.ucARsend = new SmartCommon.View.Mail.ucAddressReg();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.ucAddressRegitem = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
			this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			this.smtCommonBar1 = new DevExpress.XtraEditors.PanelControl();
			this.cmdDocEdit = new DevExpress.XtraEditors.SimpleButton();
			this.cmdSend = new DevExpress.XtraEditors.SimpleButton();
			this.cmdClose = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.frmSendmaillayoutControl1ConvertedLayout)).BeginInit();
			this.frmSendmaillayoutControl1ConvertedLayout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tboxSubject.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ucAddressRegitem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// frmSendmaillayoutControl1ConvertedLayout
			// 
			this.frmSendmaillayoutControl1ConvertedLayout.Controls.Add(this.btnDelete);
			this.frmSendmaillayoutControl1ConvertedLayout.Controls.Add(this.BtnAdd);
			this.frmSendmaillayoutControl1ConvertedLayout.Controls.Add(this.lboxFile);
			this.frmSendmaillayoutControl1ConvertedLayout.Controls.Add(this.tboxSubject);
			this.frmSendmaillayoutControl1ConvertedLayout.Controls.Add(this.txtBody);
			this.frmSendmaillayoutControl1ConvertedLayout.Controls.Add(this.ucARhide);
			this.frmSendmaillayoutControl1ConvertedLayout.Controls.Add(this.ucARreg);
			this.frmSendmaillayoutControl1ConvertedLayout.Controls.Add(this.ucARsend);
			this.frmSendmaillayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.frmSendmaillayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 40);
			this.frmSendmaillayoutControl1ConvertedLayout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.frmSendmaillayoutControl1ConvertedLayout.Name = "frmSendmaillayoutControl1ConvertedLayout";
			this.frmSendmaillayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1160, 556, 650, 400);
			this.frmSendmaillayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
			this.frmSendmaillayoutControl1ConvertedLayout.Size = new System.Drawing.Size(1208, 556);
			this.frmSendmaillayoutControl1ConvertedLayout.TabIndex = 5;
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(994, 442);
			this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(199, 22);
			this.btnDelete.StyleController = this.frmSendmaillayoutControl1ConvertedLayout;
			this.btnDelete.TabIndex = 11;
			this.btnDelete.Text = "삭제";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// BtnAdd
			// 
			this.BtnAdd.Location = new System.Drawing.Point(791, 442);
			this.BtnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.BtnAdd.Name = "BtnAdd";
			this.BtnAdd.Size = new System.Drawing.Size(199, 22);
			this.BtnAdd.StyleController = this.frmSendmaillayoutControl1ConvertedLayout;
			this.BtnAdd.TabIndex = 9;
			this.BtnAdd.Text = "추가";
			this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
			// 
			// lboxFile
			// 
			this.lboxFile.FormattingEnabled = true;
			this.lboxFile.ItemHeight = 14;
			this.lboxFile.Location = new System.Drawing.Point(791, 468);
			this.lboxFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lboxFile.Name = "lboxFile";
			this.lboxFile.Size = new System.Drawing.Size(402, 73);
			this.lboxFile.TabIndex = 6;
			// 
			// tboxSubject
			// 
			this.tboxSubject.Location = new System.Drawing.Point(44, 12);
			this.tboxSubject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tboxSubject.Name = "tboxSubject";
			this.tboxSubject.Properties.AutoHeight = false;
			this.tboxSubject.Size = new System.Drawing.Size(1152, 24);
			this.tboxSubject.StyleController = this.frmSendmaillayoutControl1ConvertedLayout;
			this.tboxSubject.TabIndex = 6;
			// 
			// txtBody
			// 
			this.txtBody.ActiveViewType = DevExpress.XtraRichEdit.RichEditViewType.Simple;
			this.txtBody.LayoutUnit = DevExpress.XtraRichEdit.DocumentLayoutUnit.Pixel;
			this.txtBody.Location = new System.Drawing.Point(15, 64);
			this.txtBody.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtBody.Name = "txtBody";
			this.txtBody.Size = new System.Drawing.Size(766, 477);
			this.txtBody.TabIndex = 5;
			// 
			// ucARhide
			// 
			this.ucARhide.BackColor = System.Drawing.Color.Transparent;
			this.ucARhide.Location = new System.Drawing.Point(791, 323);
			this.ucARhide.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.ucARhide.Name = "ucARhide";
			this.ucARhide.Size = new System.Drawing.Size(402, 78);
			this.ucARhide.TabIndex = 3;
			// 
			// ucARreg
			// 
			this.ucARreg.BackColor = System.Drawing.Color.Transparent;
			this.ucARreg.Location = new System.Drawing.Point(791, 203);
			this.ucARreg.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.ucARreg.Name = "ucARreg";
			this.ucARreg.Size = new System.Drawing.Size(402, 79);
			this.ucARreg.TabIndex = 2;
			// 
			// ucARsend
			// 
			this.ucARsend.BackColor = System.Drawing.Color.Transparent;
			this.ucARsend.Location = new System.Drawing.Point(791, 64);
			this.ucARsend.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.ucARsend.Name = "ucARsend";
			this.ucARsend.Size = new System.Drawing.Size(402, 98);
			this.ucARsend.TabIndex = 1;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup4,
            this.layoutControlGroup3,
            this.layoutControlItem4,
            this.layoutControlGroup5,
            this.layoutControlGroup6,
            this.splitterItem1});
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Size = new System.Drawing.Size(1208, 556);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 28);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(776, 508);
			this.layoutControlGroup2.Text = "본문";
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.txtBody;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(770, 481);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlGroup4
			// 
			this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.splitterItem3});
			this.layoutControlGroup4.Location = new System.Drawing.Point(776, 167);
			this.layoutControlGroup4.Name = "layoutControlGroup4";
			this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup4.Size = new System.Drawing.Size(412, 120);
			this.layoutControlGroup4.Text = "참조";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.ucARreg;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "ucAddressRegitem";
			this.layoutControlItem1.Size = new System.Drawing.Size(406, 83);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// splitterItem3
			// 
			this.splitterItem3.AllowHotTrack = true;
			this.splitterItem3.Location = new System.Drawing.Point(0, 83);
			this.splitterItem3.Name = "splitterItem3";
			this.splitterItem3.Size = new System.Drawing.Size(406, 10);
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup3.Location = new System.Drawing.Point(776, 28);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(412, 129);
			this.layoutControlGroup3.Text = "받는 사람";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.ucARsend;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "item1";
			this.layoutControlItem2.Size = new System.Drawing.Size(406, 102);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.tboxSubject;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.MinSize = new System.Drawing.Size(86, 24);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(1188, 28);
			this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem4.Text = "제목";
			this.layoutControlItem4.TextSize = new System.Drawing.Size(20, 14);
			// 
			// layoutControlGroup5
			// 
			this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.ucAddressRegitem,
            this.splitterItem2});
			this.layoutControlGroup5.Location = new System.Drawing.Point(776, 287);
			this.layoutControlGroup5.Name = "layoutControlGroup5";
			this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup5.Size = new System.Drawing.Size(412, 119);
			this.layoutControlGroup5.Text = "숨은참조";
			// 
			// ucAddressRegitem
			// 
			this.ucAddressRegitem.Control = this.ucARhide;
			this.ucAddressRegitem.Location = new System.Drawing.Point(0, 0);
			this.ucAddressRegitem.Name = "item0";
			this.ucAddressRegitem.Size = new System.Drawing.Size(406, 82);
			this.ucAddressRegitem.TextSize = new System.Drawing.Size(0, 0);
			this.ucAddressRegitem.TextVisible = false;
			// 
			// splitterItem2
			// 
			this.splitterItem2.AllowHotTrack = true;
			this.splitterItem2.Location = new System.Drawing.Point(0, 82);
			this.splitterItem2.Name = "splitterItem2";
			this.splitterItem2.Size = new System.Drawing.Size(406, 10);
			// 
			// layoutControlGroup6
			// 
			this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem6,
            this.layoutControlItem9});
			this.layoutControlGroup6.Location = new System.Drawing.Point(776, 406);
			this.layoutControlGroup6.Name = "layoutControlGroup6";
			this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup6.Size = new System.Drawing.Size(412, 130);
			this.layoutControlGroup6.Text = "첨부파일";
			// 
			// layoutControlItem8
			// 
			this.layoutControlItem8.Control = this.BtnAdd;
			this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem8.Name = "layoutControlItem8";
			this.layoutControlItem8.Size = new System.Drawing.Size(203, 26);
			this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem8.TextVisible = false;
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.Control = this.lboxFile;
			this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Size = new System.Drawing.Size(406, 77);
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			// 
			// layoutControlItem9
			// 
			this.layoutControlItem9.Control = this.btnDelete;
			this.layoutControlItem9.Location = new System.Drawing.Point(203, 0);
			this.layoutControlItem9.Name = "layoutControlItem9";
			this.layoutControlItem9.Size = new System.Drawing.Size(203, 26);
			this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem9.TextVisible = false;
			// 
			// splitterItem1
			// 
			this.splitterItem1.AllowHotTrack = true;
			this.splitterItem1.Location = new System.Drawing.Point(776, 157);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(412, 10);
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDocEdit);
			this.smtCommonBar1.Controls.Add(this.cmdSend);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1208, 40);
			this.smtCommonBar1.TabIndex = 12;
			// 
			// cmdDocEdit
			// 
			this.cmdDocEdit.AllowFocus = false;
			this.cmdDocEdit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDocEdit.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDocEdit.Appearance.Options.UseBackColor = true;
			this.cmdDocEdit.Appearance.Options.UseForeColor = true;
			this.cmdDocEdit.AutoSize = true;
			this.cmdDocEdit.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdDocEdit.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDocEdit.ImageOptions.ImageIndex = 2;
			this.cmdDocEdit.ImageOptions.ImageToTextIndent = 15;
			this.cmdDocEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cmdDocEdit.ImageOptions.SvgImage")));
			this.cmdDocEdit.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDocEdit.Location = new System.Drawing.Point(981, 0);
			this.cmdDocEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdDocEdit.Name = "cmdDocEdit";
			this.cmdDocEdit.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
			this.cmdDocEdit.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDocEdit.Size = new System.Drawing.Size(89, 40);
			this.cmdDocEdit.TabIndex = 10;
			this.cmdDocEdit.Text = "문서편집";
			this.cmdDocEdit.Click += new System.EventHandler(this.cmdDocEdit_Click);
			// 
			// cmdSend
			// 
			this.cmdSend.AllowFocus = false;
			this.cmdSend.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdSend.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdSend.Appearance.Options.UseBackColor = true;
			this.cmdSend.Appearance.Options.UseForeColor = true;
			this.cmdSend.AutoSize = true;
			this.cmdSend.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdSend.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdSend.ImageOptions.ImageIndex = 11;
			this.cmdSend.ImageOptions.ImageToTextIndent = 15;
			this.cmdSend.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cmdSend.ImageOptions.SvgImage")));
			this.cmdSend.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdSend.Location = new System.Drawing.Point(1070, 0);
			this.cmdSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdSend.Name = "cmdSend";
			this.cmdSend.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
			this.cmdSend.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSend.Size = new System.Drawing.Size(69, 40);
			this.cmdSend.TabIndex = 9;
			this.cmdSend.Text = "전송";
			this.cmdSend.Click += new System.EventHandler(this.btnSendMail_Click);
			// 
			// cmdClose
			// 
			this.cmdClose.AllowFocus = false;
			this.cmdClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdClose.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdClose.Appearance.Options.UseBackColor = true;
			this.cmdClose.Appearance.Options.UseForeColor = true;
			this.cmdClose.AutoSize = true;
			this.cmdClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdClose.ImageOptions.ImageIndex = 5;
			this.cmdClose.ImageOptions.ImageToTextIndent = 15;
			this.cmdClose.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cmdClose.ImageOptions.SvgImage")));
			this.cmdClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdClose.Location = new System.Drawing.Point(1139, 0);
			this.cmdClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(69, 40);
			this.cmdClose.TabIndex = 8;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// frmSendmail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1208, 596);
			this.Controls.Add(this.frmSendmaillayoutControl1ConvertedLayout);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "frmSendmail";
			this.Text = "메일전송";
			((System.ComponentModel.ISupportInitialize)(this.frmSendmaillayoutControl1ConvertedLayout)).EndInit();
			this.frmSendmaillayoutControl1ConvertedLayout.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tboxSubject.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ucAddressRegitem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private ucAddressReg ucARsend;
        private ucAddressReg ucARreg;
        private ucAddressReg ucARhide;
        private DevExpress.XtraLayout.Converter.LayoutConverter layoutConverter1;
        private DevExpress.XtraLayout.LayoutControl frmSendmaillayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem ucAddressRegitem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit tboxSubject;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
		private DevExpress.XtraEditors.SimpleButton BtnAdd;
		private System.Windows.Forms.ListBox lboxFile;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraRichEdit.RichEditControl txtBody;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraEditors.SimpleButton btnDelete;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
		private DevExpress.XtraEditors.PanelControl smtCommonBar1;
		private DevExpress.XtraEditors.SimpleButton cmdSend;
		private DevExpress.XtraEditors.SimpleButton  cmdClose;
		private DevExpress.XtraEditors.SimpleButton cmdDocEdit;
		private DevExpress.XtraLayout.SplitterItem splitterItem3;
		private DevExpress.XtraLayout.SplitterItem splitterItem2;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
	}
}

