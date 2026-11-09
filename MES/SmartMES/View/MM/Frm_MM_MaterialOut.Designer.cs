namespace SmartMES.View.MM
{
	partial class Frm_MM_MaterialOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_MaterialOut));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdIns = new baseSMT.smtSearchButton();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.LupCustomer = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.smtSearchButton5 = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.GcBM_SalesD = new DevExpress.XtraGrid.GridControl();
            this.GvBM_SalesD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GcBM_SalesM = new DevExpress.XtraGrid.GridControl();
            this.GvBM_SalesM = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdIns);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.cmdDelete);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.LupCustomer);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton5);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1215, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "입고 관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 10;
            // 
            // cmdIns
            // 
            this.cmdIns.AllowFocus = false;
            this.cmdIns.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdIns.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdIns.Appearance.Options.UseBackColor = true;
            this.cmdIns.Appearance.Options.UseForeColor = true;
            this.cmdIns.AutoSize = true;
            this.cmdIns.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdIns.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdIns.ImageOptions.ImageIndex = 1;
            this.cmdIns.ImageOptions.ImageToTextIndent = 15;
            this.cmdIns.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdIns.Location = new System.Drawing.Point(806, 0);
            this.cmdIns.Name = "cmdIns";
            this.cmdIns.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdIns.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdIns.Size = new System.Drawing.Size(77, 40);
            this.cmdIns.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdIns.TabIndex = 17;
            this.cmdIns.Text = "등록";
            this.cmdIns.Click += new System.EventHandler(this.BtnIns_Click);
            // 
            // smtSearchButton1
            // 
            this.smtSearchButton1.AllowFocus = false;
            this.smtSearchButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton1.Appearance.Options.UseBackColor = true;
            this.smtSearchButton1.Appearance.Options.UseForeColor = true;
            this.smtSearchButton1.AutoSize = true;
            this.smtSearchButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton1.ImageOptions.ImageIndex = 2;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(883, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.수정;
            this.smtSearchButton1.TabIndex = 16;
            this.smtSearchButton1.Text = "수정";
            this.smtSearchButton1.Click += new System.EventHandler(this.BtnUpd_Click);
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
            this.cmdDelete.Location = new System.Drawing.Point(960, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(77, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 18;
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.AllowFocus = false;
            this.btn_Search.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Search.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Search.Appearance.Options.UseBackColor = true;
            this.btn_Search.Appearance.Options.UseForeColor = true;
            this.btn_Search.AutoSize = true;
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Search.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Search.ImageOptions.ImageIndex = 0;
            this.btn_Search.ImageOptions.ImageToTextIndent = 15;
            this.btn_Search.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Search.Location = new System.Drawing.Point(728, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 15;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // LupCustomer
            // 
            this.LupCustomer.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupCustomer.Location = new System.Drawing.Point(440, 0);
            this.LupCustomer.LookAndFeel.SkinName = "WXI";
            this.LupCustomer.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupCustomer.Name = "LupCustomer";
            this.LupCustomer.Size = new System.Drawing.Size(288, 40);
            this.LupCustomer.smtLabelTitleValue = "거래처";
            this.LupCustomer.smtLabelTitleVisible = true;
            this.LupCustomer.smtLabelTitleWidth = 70;
            this.LupCustomer.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupCustomer.smtLookUpDataDisplayMember = "코드명";
            this.LupCustomer.smtLookUpDataSource = null;
            this.LupCustomer.smtLookUpDataValueMember = "코드";
            this.LupCustomer.smtLookUpSelectdIndex = -1;
            this.LupCustomer.smtLookUpText = "";
            this.LupCustomer.smtLookUpValue = "";
            this.LupCustomer.TabIndex = 13;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(95, 0);
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
            this.Dt_Date.smtLabelTitleValue = "출고일자";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 10;
            // 
            // smtSearchButton5
            // 
            this.smtSearchButton5.AllowFocus = false;
            this.smtSearchButton5.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton5.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton5.Appearance.Options.UseBackColor = true;
            this.smtSearchButton5.Appearance.Options.UseForeColor = true;
            this.smtSearchButton5.AutoSize = true;
            this.smtSearchButton5.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton5.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton5.ImageOptions.ImageIndex = 9;
            this.smtSearchButton5.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton5.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton5.Location = new System.Drawing.Point(1037, 0);
            this.smtSearchButton5.Name = "smtSearchButton5";
            this.smtSearchButton5.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton5.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton5.Size = new System.Drawing.Size(101, 40);
            this.smtSearchButton5.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.smtSearchButton5.TabIndex = 6;
            this.smtSearchButton5.Text = "엑셀변환";
            this.smtSearchButton5.Visible = false;
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
            this.cmdClose.Location = new System.Drawing.Point(1138, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1215, 714);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.GcBM_SalesD);
            this.layoutControl2.Controls.Add(this.GcBM_SalesM);
            this.layoutControl2.Location = new System.Drawing.Point(3, 3);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1748, 297, 812, 500);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1209, 708);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // GcBM_SalesD
            // 
            this.GcBM_SalesD.Cursor = System.Windows.Forms.Cursors.Default;
            this.GcBM_SalesD.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.GcBM_SalesD.Location = new System.Drawing.Point(7, 473);
            this.GcBM_SalesD.MainView = this.GvBM_SalesD;
            this.GcBM_SalesD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GcBM_SalesD.Name = "GcBM_SalesD";
            this.GcBM_SalesD.Size = new System.Drawing.Size(1195, 228);
            this.GcBM_SalesD.TabIndex = 52;
            this.GcBM_SalesD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvBM_SalesD});
            // 
            // GvBM_SalesD
            // 
            this.GvBM_SalesD.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GvBM_SalesD.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GvBM_SalesD.ColumnPanelRowHeight = 29;
            this.GvBM_SalesD.DetailHeight = 425;
            this.GvBM_SalesD.GridControl = this.GcBM_SalesD;
            this.GvBM_SalesD.Name = "GvBM_SalesD";
            this.GvBM_SalesD.OptionsBehavior.Editable = false;
            this.GvBM_SalesD.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            this.GvBM_SalesD.OptionsView.ColumnAutoWidth = false;
            this.GvBM_SalesD.OptionsView.ShowGroupPanel = false;
            this.GvBM_SalesD.RowHeight = 29;
            // 
            // GcBM_SalesM
            // 
            this.GcBM_SalesM.Cursor = System.Windows.Forms.Cursors.Default;
            this.GcBM_SalesM.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.GcBM_SalesM.Location = new System.Drawing.Point(7, 34);
            this.GcBM_SalesM.MainView = this.GvBM_SalesM;
            this.GcBM_SalesM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GcBM_SalesM.Name = "GcBM_SalesM";
            this.GcBM_SalesM.Size = new System.Drawing.Size(1195, 382);
            this.GcBM_SalesM.TabIndex = 51;
            this.GcBM_SalesM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvBM_SalesM});
            // 
            // GvBM_SalesM
            // 
            this.GvBM_SalesM.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GvBM_SalesM.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GvBM_SalesM.ColumnPanelRowHeight = 29;
            this.GvBM_SalesM.DetailHeight = 425;
            this.GvBM_SalesM.GridControl = this.GcBM_SalesM;
            this.GvBM_SalesM.Name = "GvBM_SalesM";
            this.GvBM_SalesM.OptionsBehavior.Editable = false;
            this.GvBM_SalesM.OptionsDetail.EnableMasterViewMode = false;
            this.GvBM_SalesM.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.GvBM_SalesM.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.GvBM_SalesM.OptionsView.ColumnAutoWidth = false;
            this.GvBM_SalesM.OptionsView.ShowAutoFilterRow = true;
            this.GvBM_SalesM.OptionsView.ShowGroupPanel = false;
            this.GvBM_SalesM.RowHeight = 29;
            this.GvBM_SalesM.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GvBM_SalesM_FocusedRowChanged);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1209, 708);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "발주등록 마스터";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControlGroup2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup2.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1209, 423);
            this.layoutControlGroup2.Text = "출고등록 마스터";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GcBM_SalesM;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1201, 388);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "발주등록 세부내용";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 439);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1209, 269);
            this.layoutControlGroup3.Text = "출고 품목";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.GcBM_SalesD;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1201, 234);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 423);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1209, 16);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1215, 714);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.layoutControl2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1215, 714);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Frm_MM_MaterialOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 754);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_MM_MaterialOut";
            this.Text = "입고 관리";
            this.Load += new System.EventHandler(this.Frm_MM_MaterialOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton btn_Search;
		private baseSMT.smtSearchLookUp LupCustomer;
		private baseSMT.smtSearchDatetime Dt_Date;
		private baseSMT.smtSearchButton smtSearchButton5;
		private baseSMT.smtSearchButton cmdClose;
		private baseSMT.smtSearchButton cmdIns;
		private baseSMT.smtSearchButton smtSearchButton1;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControl layoutControl2;
		private DevExpress.XtraGrid.GridControl GcBM_SalesD;
		private DevExpress.XtraGrid.Views.Grid.GridView GvBM_SalesD;
		private DevExpress.XtraGrid.GridControl GcBM_SalesM;
		private DevExpress.XtraGrid.Views.Grid.GridView GvBM_SalesM;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private baseSMT.smtSearchButton cmdDelete;
	}
}