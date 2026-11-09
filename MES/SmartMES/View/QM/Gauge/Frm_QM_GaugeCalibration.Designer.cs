
namespace SmartMES.View.QM.Gauge
{
    partial class Frm_QM_GaugeCalibration
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_GaugeCalibration));
			this.Gc_Gauge = new DevExpress.XtraGrid.GridControl();
			this.Gv_Gauge = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Gc_GaugeCal = new DevExpress.XtraGrid.GridControl();
			this.Gv_GaugeCal = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdInsert = new baseSMT.smtSearchButton();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdDelete = new baseSMT.smtSearchButton();
			this.cmdSearch = new baseSMT.smtSearchButton();
			this.Dt_Date = new baseSMT.smtSearchDatetime();
			this.cmdExcel = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.cmdUpload = new baseSMT.smtSearchButton();
			this.cmdDownload = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Gauge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Gauge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gc_GaugeCal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_GaugeCal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Gc_Gauge
			// 
			this.Gc_Gauge.Location = new System.Drawing.Point(7, 34);
			this.Gc_Gauge.MainView = this.Gv_Gauge;
			this.Gc_Gauge.Name = "Gc_Gauge";
			this.Gc_Gauge.Size = new System.Drawing.Size(267, 447);
			this.Gc_Gauge.TabIndex = 1;
			this.Gc_Gauge.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Gauge});
			// 
			// Gv_Gauge
			// 
			this.Gv_Gauge.DetailHeight = 397;
			this.Gv_Gauge.GridControl = this.Gc_Gauge;
			this.Gv_Gauge.Name = "Gv_Gauge";
			this.Gv_Gauge.OptionsEditForm.PopupEditFormWidth = 914;
			this.Gv_Gauge.OptionsView.ShowGroupPanel = false;
			// 
			// Gc_GaugeCal
			// 
			this.Gc_GaugeCal.Location = new System.Drawing.Point(304, 34);
			this.Gc_GaugeCal.MainView = this.Gv_GaugeCal;
			this.Gc_GaugeCal.Name = "Gc_GaugeCal";
			this.Gc_GaugeCal.Size = new System.Drawing.Size(731, 218);
			this.Gc_GaugeCal.TabIndex = 1;
			this.Gc_GaugeCal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_GaugeCal});
			// 
			// Gv_GaugeCal
			// 
			this.Gv_GaugeCal.DetailHeight = 397;
			this.Gv_GaugeCal.GridControl = this.Gc_GaugeCal;
			this.Gv_GaugeCal.Name = "Gv_GaugeCal";
			this.Gv_GaugeCal.OptionsEditForm.PopupEditFormWidth = 914;
			this.Gv_GaugeCal.OptionsView.ShowGroupPanel = false;
			// 
			// pictureEdit1
			// 
			this.pictureEdit1.Location = new System.Drawing.Point(304, 309);
			this.pictureEdit1.Name = "pictureEdit1";
			this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit1.Size = new System.Drawing.Size(731, 172);
			this.pictureEdit1.StyleController = this.layoutControl1;
			this.pictureEdit1.TabIndex = 0;
			this.pictureEdit1.ToolTip = "상단에서 이미지를 등록하세요.\r\n이미지 셀 클릭 → 마우스 우측 클릭 → Load → 저장";
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.Gc_GaugeCal);
			this.layoutControl1.Controls.Add(this.pictureEdit1);
			this.layoutControl1.Controls.Add(this.Gc_Gauge);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1042, 488);
			this.layoutControl1.TabIndex = 290;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.splitterItem1,
            this.splitterItem2});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1042, 488);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(281, 488);
			this.layoutControlGroup1.Text = "계측기리스트";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.Gc_Gauge;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(273, 453);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup2.Location = new System.Drawing.Point(297, 275);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(745, 213);
			this.layoutControlGroup2.Text = "계측기이미지";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.pictureEdit1;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(737, 178);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
			this.layoutControlGroup3.Location = new System.Drawing.Point(297, 0);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(745, 259);
			this.layoutControlGroup3.Text = "검교정내역";
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.Gc_GaugeCal;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(737, 224);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// splitterItem1
			// 
			this.splitterItem1.AllowHotTrack = true;
			this.splitterItem1.Location = new System.Drawing.Point(297, 259);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(745, 16);
			// 
			// splitterItem2
			// 
			this.splitterItem2.AllowHotTrack = true;
			this.splitterItem2.Location = new System.Drawing.Point(281, 0);
			this.splitterItem2.Name = "splitterItem2";
			this.splitterItem2.Size = new System.Drawing.Size(16, 488);
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDownload);
			this.smtCommonBar1.Controls.Add(this.cmdUpload);
			this.smtCommonBar1.Controls.Add(this.cmdInsert);
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdDelete);
			this.smtCommonBar1.Controls.Add(this.cmdSearch);
			this.smtCommonBar1.Controls.Add(this.Dt_Date);
			this.smtCommonBar1.Controls.Add(this.cmdExcel);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1042, 40);
			this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "계측기교정";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 289;
			// 
			// cmdInsert
			// 
			this.cmdInsert.AllowFocus = false;
			this.cmdInsert.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdInsert.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdInsert.Appearance.Options.UseBackColor = true;
			this.cmdInsert.Appearance.Options.UseForeColor = true;
			this.cmdInsert.AutoSize = true;
			this.cmdInsert.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdInsert.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdInsert.ImageOptions.ImageIndex = 1;
			this.cmdInsert.ImageOptions.ImageToTextIndent = 15;
			this.cmdInsert.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdInsert.Location = new System.Drawing.Point(633, 0);
			this.cmdInsert.Name = "cmdInsert";
			this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInsert.Size = new System.Drawing.Size(77, 40);
			this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdInsert.TabIndex = 13;
			this.cmdInsert.Tag = "Ins";
			this.cmdInsert.Text = "등록";
			this.cmdInsert.Click += new System.EventHandler(this.Btn_RowEdit);
			// 
			// cmdSave
			// 
			this.cmdSave.AllowFocus = false;
			this.cmdSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdSave.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdSave.Appearance.Options.UseBackColor = true;
			this.cmdSave.Appearance.Options.UseForeColor = true;
			this.cmdSave.AutoSize = true;
			this.cmdSave.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdSave.ImageOptions.ImageIndex = 4;
			this.cmdSave.ImageOptions.ImageToTextIndent = 15;
			this.cmdSave.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdSave.Location = new System.Drawing.Point(710, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 12;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.cmd_Click);
			// 
			// cmdDelete
			// 
			this.cmdDelete.AllowFocus = false;
			this.cmdDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDelete.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDelete.Appearance.Options.UseBackColor = true;
			this.cmdDelete.Appearance.Options.UseForeColor = true;
			this.cmdDelete.AutoSize = true;
			this.cmdDelete.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDelete.ImageOptions.ImageIndex = 3;
			this.cmdDelete.ImageOptions.ImageToTextIndent = 15;
			this.cmdDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDelete.Location = new System.Drawing.Point(787, 0);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDelete.Size = new System.Drawing.Size(77, 40);
			this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDelete.TabIndex = 11;
			this.cmdDelete.Tag = "Del";
			this.cmdDelete.Text = "삭제";
			this.cmdDelete.Click += new System.EventHandler(this.Btn_RowEdit);
			// 
			// cmdSearch
			// 
			this.cmdSearch.AllowFocus = false;
			this.cmdSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdSearch.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdSearch.Appearance.Options.UseBackColor = true;
			this.cmdSearch.Appearance.Options.UseForeColor = true;
			this.cmdSearch.AutoSize = true;
			this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Left;
			this.cmdSearch.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdSearch.ImageOptions.ImageIndex = 0;
			this.cmdSearch.ImageOptions.ImageToTextIndent = 15;
			this.cmdSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdSearch.Location = new System.Drawing.Point(449, 0);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSearch.Size = new System.Drawing.Size(77, 40);
			this.cmdSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdSearch.TabIndex = 5;
			this.cmdSearch.Text = "조회";
			this.cmdSearch.Click += new System.EventHandler(this.cmd_Click);
			// 
			// Dt_Date
			// 
			this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
			this.Dt_Date.Location = new System.Drawing.Point(104, 0);
			this.Dt_Date.LookAndFeel.SkinName = "WXI";
			this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Dt_Date.Name = "Dt_Date";
			this.Dt_Date.Size = new System.Drawing.Size(345, 40);
			this.Dt_Date.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.Dt_Date.smtComboTitleItems = new string[0];
			this.Dt_Date.smtComboTitleValue = "";
			this.Dt_Date.smtComboTitleVisible = false;
			this.Dt_Date.smtComboTitleWidth = 100;
			this.Dt_Date.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 729);
			this.Dt_Date.smtEndDateTimeVisible = true;
			this.Dt_Date.smtLabelForeColor = System.Drawing.Color.White;
			this.Dt_Date.smtLabelTitleValue = "조회기간";
			this.Dt_Date.smtLabelTitleVisible = true;
			this.Dt_Date.smtLabelTitleWidth = 100;
			this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
			this.Dt_Date.smtStartDateTimeVisible = true;
			this.Dt_Date.TabIndex = 10;
			// 
			// cmdExcel
			// 
			this.cmdExcel.AllowFocus = false;
			this.cmdExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdExcel.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdExcel.Appearance.Options.UseBackColor = true;
			this.cmdExcel.Appearance.Options.UseForeColor = true;
			this.cmdExcel.AutoSize = true;
			this.cmdExcel.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdExcel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdExcel.ImageOptions.ImageIndex = 9;
			this.cmdExcel.ImageOptions.ImageToTextIndent = 15;
			this.cmdExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdExcel.Location = new System.Drawing.Point(864, 0);
			this.cmdExcel.Name = "cmdExcel";
			this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdExcel.Size = new System.Drawing.Size(101, 40);
			this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
			this.cmdExcel.TabIndex = 14;
			this.cmdExcel.Text = "엑셀변환";
			this.cmdExcel.Click += new System.EventHandler(this.cmd_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(965, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmd_Click);
			// 
			// cmdUpload
			// 
			this.cmdUpload.AllowFocus = false;
			this.cmdUpload.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdUpload.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdUpload.Appearance.Options.UseBackColor = true;
			this.cmdUpload.Appearance.Options.UseForeColor = true;
			this.cmdUpload.AutoSize = true;
			this.cmdUpload.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdUpload.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdUpload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("smtSearchButton1.ImageOptions.Image1")));
			this.cmdUpload.ImageOptions.ImageIndex = 8;
			this.cmdUpload.ImageOptions.ImageToTextIndent = 15;
			this.cmdUpload.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdUpload.Location = new System.Drawing.Point(510, 0);
			this.cmdUpload.Name = "cmdUpload";
			this.cmdUpload.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdUpload.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdUpload.Size = new System.Drawing.Size(123, 40);
			this.cmdUpload.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀업로드;
			this.cmdUpload.TabIndex = 15;
			this.cmdUpload.Tag = "Ins";
			this.cmdUpload.Text = "성적서 업로드";
			this.cmdUpload.Click += new System.EventHandler(this.cmd_Click);
			// 
			// cmdDownload
			// 
			this.cmdDownload.AllowFocus = false;
			this.cmdDownload.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDownload.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDownload.Appearance.Options.UseBackColor = true;
			this.cmdDownload.Appearance.Options.UseForeColor = true;
			this.cmdDownload.AutoSize = true;
			this.cmdDownload.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdDownload.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDownload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("smtSearchButton1.ImageOptions.Image")));
			this.cmdDownload.ImageOptions.ImageIndex = 8;
			this.cmdDownload.ImageOptions.ImageToTextIndent = 15;
			this.cmdDownload.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDownload.Location = new System.Drawing.Point(399, 0);
			this.cmdDownload.Name = "cmdDownload";
			this.cmdDownload.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDownload.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDownload.Size = new System.Drawing.Size(111, 40);
			this.cmdDownload.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀업로드;
			this.cmdDownload.TabIndex = 16;
			this.cmdDownload.Tag = "Ins";
			this.cmdDownload.Text = "성적서 다운";
			this.cmdDownload.Click += new System.EventHandler(this.cmd_Click);
			// 
			// Frm_QM_GaugeCalibration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1042, 528);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_QM_GaugeCalibration";
			this.Text = "계측기교정";
			this.Load += new System.EventHandler(this.Frm_QM_GaugeCalibration_Load);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Gauge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Gauge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gc_GaugeCal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_GaugeCal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl Gc_Gauge;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Gauge;
		private DevExpress.XtraGrid.GridControl Gc_GaugeCal;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_GaugeCal;
		private DevExpress.XtraEditors.PictureEdit pictureEdit1;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdSearch;
		private baseSMT.smtSearchDatetime Dt_Date;
		private baseSMT.smtSearchButton cmdClose;
		private baseSMT.smtSearchButton cmdInsert;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDelete;
		private baseSMT.smtSearchButton cmdExcel;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private DevExpress.XtraLayout.SplitterItem splitterItem2;
		private baseSMT.smtSearchButton cmdUpload;
		private baseSMT.smtSearchButton cmdDownload;
	}
}