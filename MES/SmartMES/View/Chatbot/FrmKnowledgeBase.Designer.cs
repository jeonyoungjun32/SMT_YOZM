using DevExpress.XtraEditors;

namespace SmartMES.View.Chatbot
{
    partial class FrmKnowledgeBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKnowledgeBase));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.BtnSearch = new baseSMT.smtSearchButton();
            this.SearchLupUseYn = new baseSMT.smtSearchLookUp();
            this.SearchTxtTitle = new baseSMT.smtSearchTextBox();
            this.SearchLupType = new baseSMT.smtSearchLookUp();
            this.cmdAdd = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.TxtEmbedding = new baseSMT.smtBodyTextBox();
            this.RdoUseYn = new baseSMT.smtBodyRadioGroup();
            this.MemoContent = new baseSMT.smtBodyMemoEdit();
            this.TxtTitle = new baseSMT.smtBodyTextBox();
            this.LupType = new baseSMT.smtBodyLookUp();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.BtnSave = new baseSMT.smtBodyButton();
            this.BtnDel = new baseSMT.smtBodyButton();
            this.BtnEmbedding = new baseSMT.smtBodyButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_Know = new DevExpress.XtraGrid.GridControl();
            this.Gv_Know = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Know)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Know)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.BtnSearch);
            this.smtCommonBar1.Controls.Add(this.SearchLupUseYn);
            this.smtCommonBar1.Controls.Add(this.SearchTxtTitle);
            this.smtCommonBar1.Controls.Add(this.SearchLupType);
            this.smtCommonBar1.Controls.Add(this.cmdAdd);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1079, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "지식베이스 관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 284;
            // 
            // BtnSearch
            // 
            this.BtnSearch.AllowFocus = false;
            this.BtnSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Appearance.Options.UseBackColor = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.AutoSize = true;
            this.BtnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnSearch.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnSearch.ImageOptions.ImageIndex = 0;
            this.BtnSearch.ImageOptions.ImageToTextIndent = 15;
            this.BtnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.BtnSearch.Location = new System.Drawing.Point(779, 0);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnSearch.Size = new System.Drawing.Size(77, 40);
            this.BtnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.BtnSearch.TabIndex = 5;
            this.BtnSearch.Text = "조회";
            // 
            // SearchLupUseYn
            // 
            this.SearchLupUseYn.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchLupUseYn.Location = new System.Drawing.Point(585, 0);
            this.SearchLupUseYn.LookAndFeel.SkinName = "WXI";
            this.SearchLupUseYn.LookAndFeel.UseDefaultLookAndFeel = false;
            this.SearchLupUseYn.Name = "SearchLupUseYn";
            this.SearchLupUseYn.Size = new System.Drawing.Size(194, 40);
            this.SearchLupUseYn.smtLabelForeColor = System.Drawing.Color.White;
            this.SearchLupUseYn.smtLabelTitleValue = "사용유무";
            this.SearchLupUseYn.smtLabelTitleVisible = true;
            this.SearchLupUseYn.smtLabelTitleWidth = 70;
            this.SearchLupUseYn.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.SearchLupUseYn.smtLookUpDataDisplayMember = "코드명";
            this.SearchLupUseYn.smtLookUpDataSource = null;
            this.SearchLupUseYn.smtLookUpDataValueMember = "코드";
            this.SearchLupUseYn.smtLookUpSelectdIndex = -1;
            this.SearchLupUseYn.smtLookUpText = "";
            this.SearchLupUseYn.smtLookUpValue = "";
            this.SearchLupUseYn.TabIndex = 45;
            // 
            // SearchTxtTitle
            // 
            this.SearchTxtTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchTxtTitle.Location = new System.Drawing.Point(344, 0);
            this.SearchTxtTitle.LookAndFeel.SkinName = "WXI";
            this.SearchTxtTitle.LookAndFeel.UseDefaultLookAndFeel = false;
            this.SearchTxtTitle.Name = "SearchTxtTitle";
            this.SearchTxtTitle.Size = new System.Drawing.Size(241, 40);
            this.SearchTxtTitle.smtLabelTitleValue = "제목";
            this.SearchTxtTitle.smtLabelTitleVisible = true;
            this.SearchTxtTitle.smtLabelTitleWidth = 70;
            this.SearchTxtTitle.smtTextBoxDecimal = 0;
            this.SearchTxtTitle.smtTextBoxMust = false;
            this.SearchTxtTitle.smtTextBoxNumber = false;
            this.SearchTxtTitle.smtTextBoxReadOnly = false;
            this.SearchTxtTitle.smtTextBoxValue = "";
            this.SearchTxtTitle.TabIndex = 44;
            // 
            // SearchLupType
            // 
            this.SearchLupType.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchLupType.Location = new System.Drawing.Point(131, 0);
            this.SearchLupType.LookAndFeel.SkinName = "WXI";
            this.SearchLupType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.SearchLupType.Name = "SearchLupType";
            this.SearchLupType.Size = new System.Drawing.Size(213, 40);
            this.SearchLupType.smtLabelForeColor = System.Drawing.Color.White;
            this.SearchLupType.smtLabelTitleValue = "분류";
            this.SearchLupType.smtLabelTitleVisible = true;
            this.SearchLupType.smtLabelTitleWidth = 70;
            this.SearchLupType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.SearchLupType.smtLookUpDataDisplayMember = "코드명";
            this.SearchLupType.smtLookUpDataSource = null;
            this.SearchLupType.smtLookUpDataValueMember = "코드";
            this.SearchLupType.smtLookUpSelectdIndex = -1;
            this.SearchLupType.smtLookUpText = "";
            this.SearchLupType.smtLookUpValue = "";
            this.SearchLupType.TabIndex = 43;
            // 
            // cmdAdd
            // 
            this.cmdAdd.AllowFocus = false;
            this.cmdAdd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdAdd.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdAdd.Appearance.Options.UseBackColor = true;
            this.cmdAdd.Appearance.Options.UseForeColor = true;
            this.cmdAdd.AutoSize = true;
            this.cmdAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdAdd.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdAdd.ImageOptions.ImageIndex = 1;
            this.cmdAdd.ImageOptions.ImageToTextIndent = 15;
            this.cmdAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdAdd.Location = new System.Drawing.Point(925, 0);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdAdd.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdAdd.Size = new System.Drawing.Size(77, 40);
            this.cmdAdd.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdAdd.TabIndex = 42;
            this.cmdAdd.Text = "등록";
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
            this.cmdClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdClose.Location = new System.Drawing.Point(1002, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.groupControl2);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1079, 660);
            this.layoutControl1.TabIndex = 285;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.TxtEmbedding);
            this.groupControl2.Controls.Add(this.RdoUseYn);
            this.groupControl2.Controls.Add(this.MemoContent);
            this.groupControl2.Controls.Add(this.TxtTitle);
            this.groupControl2.Controls.Add(this.LupType);
            this.groupControl2.Controls.Add(this.panelControl1);
            this.groupControl2.Location = new System.Drawing.Point(635, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(441, 654);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "지식상세";
            // 
            // TxtEmbedding
            // 
            this.TxtEmbedding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtEmbedding.Location = new System.Drawing.Point(5, 579);
            this.TxtEmbedding.LookAndFeel.SkinName = "WXI";
            this.TxtEmbedding.LookAndFeel.UseDefaultLookAndFeel = false;
            this.TxtEmbedding.Name = "TxtEmbedding";
            this.TxtEmbedding.Size = new System.Drawing.Size(431, 32);
            this.TxtEmbedding.smtLabelTitleValue = "임베딩";
            this.TxtEmbedding.smtLabelTitleVisible = true;
            this.TxtEmbedding.smtLabelTitleWidth = 100;
            this.TxtEmbedding.smtTextBoxDecimal = 0;
            this.TxtEmbedding.smtTextBoxMust = false;
            this.TxtEmbedding.smtTextBoxNumber = false;
            this.TxtEmbedding.smtTextBoxReadOnly = false;
            this.TxtEmbedding.smtTextBoxValue = "";
            this.TxtEmbedding.TabIndex = 5;
            // 
            // RdoUseYn
            // 
            this.RdoUseYn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RdoUseYn.Location = new System.Drawing.Point(5, 617);
            this.RdoUseYn.LookAndFeel.SkinName = "WXI";
            this.RdoUseYn.LookAndFeel.UseDefaultLookAndFeel = false;
            this.RdoUseYn.Name = "RdoUseYn";
            this.RdoUseYn.Size = new System.Drawing.Size(431, 32);
            this.RdoUseYn.smtLabelTitleValue = "사용여부";
            this.RdoUseYn.smtLabelTitleVisible = true;
            this.RdoUseYn.smtLabelTitleWidth = 100;
            this.RdoUseYn.smtLookUpReadOnly = false;
            this.RdoUseYn.smtRadioGroupColor = System.Drawing.Color.Transparent;
            this.RdoUseYn.smtRadioGroupDataDisplayMember = "";
            this.RdoUseYn.smtRadioGroupDataSource = null;
            this.RdoUseYn.smtRadioGroupDataValueMember = "";
            this.RdoUseYn.smtRadioGroupMust = false;
            this.RdoUseYn.smtRadioGroupValue = "";
            this.RdoUseYn.TabIndex = 4;
            // 
            // MemoContent
            // 
            this.MemoContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MemoContent.Location = new System.Drawing.Point(5, 155);
            this.MemoContent.LookAndFeel.SkinName = "WXI";
            this.MemoContent.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MemoContent.Name = "MemoContent";
            this.MemoContent.Size = new System.Drawing.Size(431, 418);
            this.MemoContent.smtLabelTitleValue = "내용";
            this.MemoContent.smtLabelTitleVisible = true;
            this.MemoContent.smtLabelTitleWidth = 100;
            this.MemoContent.smtMemoMust = false;
            this.MemoContent.smtMemoReadOnly = false;
            this.MemoContent.smtMemoValue = "";
            this.MemoContent.TabIndex = 3;
            // 
            // TxtTitle
            // 
            this.TxtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtTitle.Location = new System.Drawing.Point(5, 117);
            this.TxtTitle.LookAndFeel.SkinName = "WXI";
            this.TxtTitle.LookAndFeel.UseDefaultLookAndFeel = false;
            this.TxtTitle.Name = "TxtTitle";
            this.TxtTitle.Size = new System.Drawing.Size(431, 32);
            this.TxtTitle.smtLabelTitleValue = "제목";
            this.TxtTitle.smtLabelTitleVisible = true;
            this.TxtTitle.smtLabelTitleWidth = 100;
            this.TxtTitle.smtTextBoxDecimal = 0;
            this.TxtTitle.smtTextBoxMust = false;
            this.TxtTitle.smtTextBoxNumber = false;
            this.TxtTitle.smtTextBoxReadOnly = false;
            this.TxtTitle.smtTextBoxValue = "";
            this.TxtTitle.TabIndex = 2;
            // 
            // LupType
            // 
            this.LupType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LupType.Location = new System.Drawing.Point(5, 79);
            this.LupType.LookAndFeel.SkinName = "WXI";
            this.LupType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupType.Name = "LupType";
            this.LupType.Size = new System.Drawing.Size(431, 32);
            this.LupType.smtLabelTitleValue = "분류";
            this.LupType.smtLabelTitleVisible = true;
            this.LupType.smtLabelTitleWidth = 100;
            this.LupType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupType.smtLookUpDataDisplayMember = "코드명";
            this.LupType.smtLookUpDataSource = null;
            this.LupType.smtLookUpDataValueMember = "코드";
            this.LupType.smtLookUpMust = false;
            this.LupType.smtLookUpReadOnly = false;
            this.LupType.smtLookUpSelectdIndex = -1;
            this.LupType.smtLookUpText = "";
            this.LupType.smtLookUpValue = "";
            this.LupType.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.BtnSave);
            this.panelControl1.Controls.Add(this.BtnDel);
            this.panelControl1.Controls.Add(this.BtnEmbedding);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 29);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(437, 44);
            this.panelControl1.TabIndex = 0;
            // 
            // BtnSave
            // 
            this.BtnSave.AllowFocus = false;
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Appearance.BackColor = System.Drawing.Color.White;
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.BtnSave.Appearance.Options.UseBackColor = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnSave.ImageOptions.ImageToTextIndent = 15;
            this.BtnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSave.ImageOptions.SvgImage")));
            this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            this.BtnSave.Location = new System.Drawing.Point(114, 6);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnSave.Size = new System.Drawing.Size(93, 32);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "저장";
            // 
            // BtnDel
            // 
            this.BtnDel.AllowFocus = false;
            this.BtnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDel.Appearance.BackColor = System.Drawing.Color.White;
            this.BtnDel.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.BtnDel.Appearance.Options.UseBackColor = true;
            this.BtnDel.Appearance.Options.UseForeColor = true;
            this.BtnDel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnDel.ImageOptions.ImageToTextIndent = 15;
            this.BtnDel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDel.ImageOptions.SvgImage")));
            this.BtnDel.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            this.BtnDel.Location = new System.Drawing.Point(213, 6);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnDel.Size = new System.Drawing.Size(93, 32);
            this.BtnDel.TabIndex = 1;
            this.BtnDel.Text = "삭제";
            this.BtnDel.Click += new System.EventHandler(this.Btn_Click);
            // 
            // BtnEmbedding
            // 
            this.BtnEmbedding.AllowFocus = false;
            this.BtnEmbedding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnEmbedding.Appearance.BackColor = System.Drawing.Color.White;
            this.BtnEmbedding.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.BtnEmbedding.Appearance.Options.UseBackColor = true;
            this.BtnEmbedding.Appearance.Options.UseForeColor = true;
            this.BtnEmbedding.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnEmbedding.ImageOptions.ImageToTextIndent = 15;
            this.BtnEmbedding.ImageOptions.SvgImageSize = new System.Drawing.Size(26, 26);
            this.BtnEmbedding.Location = new System.Drawing.Point(312, 6);
            this.BtnEmbedding.Name = "BtnEmbedding";
            this.BtnEmbedding.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnEmbedding.Size = new System.Drawing.Size(122, 32);
            this.BtnEmbedding.TabIndex = 0;
            this.BtnEmbedding.Text = "임베딩재생성";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.Gc_Know);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(610, 654);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "지식베이스";
            // 
            // Gc_Know
            // 
            this.Gc_Know.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_Know.Location = new System.Drawing.Point(2, 29);
            this.Gc_Know.MainView = this.Gv_Know;
            this.Gc_Know.Name = "Gc_Know";
            this.Gc_Know.Size = new System.Drawing.Size(606, 623);
            this.Gc_Know.TabIndex = 0;
            this.Gc_Know.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Know});
            // 
            // Gv_Know
            // 
            this.Gv_Know.GridControl = this.Gc_Know;
            this.Gv_Know.Name = "Gv_Know";
            this.Gv_Know.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.splitterItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1079, 660);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(616, 660);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(632, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(447, 660);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(616, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(16, 660);
            // 
            // FrmKnowledgeBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 700);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("FrmKnowledgeBase.IconOptions.SvgImage")));
            this.MinimumSize = new System.Drawing.Size(482, 734);
            this.Name = "FrmKnowledgeBase";
            this.Text = "지식베이스 관리";
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Know)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Know)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdAdd;
        private baseSMT.smtSearchButton BtnSearch;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private baseSMT.smtSearchTextBox SearchTxtTitle;
        private baseSMT.smtSearchLookUp SearchLupType;
        private GroupControl groupControl2;
        private GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraGrid.GridControl Gc_Know;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Know;
        private PanelControl panelControl1;
        private baseSMT.smtBodyButton BtnSave;
        private baseSMT.smtBodyButton BtnDel;
        private baseSMT.smtBodyButton BtnEmbedding;
        private baseSMT.smtBodyLookUp LupType;
        private baseSMT.smtBodyTextBox TxtTitle;
        private baseSMT.smtBodyMemoEdit MemoContent;
        private baseSMT.smtBodyRadioGroup RdoUseYn;
        private baseSMT.smtBodyTextBox TxtEmbedding;
        private baseSMT.smtSearchLookUp SearchLupUseYn;
    }
}
