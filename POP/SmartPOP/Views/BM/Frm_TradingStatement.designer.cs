namespace SmartPOP.Views.BM
{
	partial class Frm_TradingStatement
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_TradingStatement));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.gcBM_SalesD = new DevExpress.XtraGrid.GridControl();
			this.gvBM_SalesD = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.repositoryItemLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.ssCustomer = new baseSMT.smtSearchLookUp();
			this.barDate = new baseSMT.smtSearchDatetime();
			this.btnPrint = new baseSMT.smtSearchButton();
			this.btnClose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gcBM_SalesD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvBM_SalesD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.gcBM_SalesD);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 45);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(981, 335, 650, 400);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1227, 417);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// gcBM_SalesD
			// 
			this.gcBM_SalesD.Cursor = System.Windows.Forms.Cursors.Default;
			this.gcBM_SalesD.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
			this.gcBM_SalesD.Location = new System.Drawing.Point(7, 34);
			this.gcBM_SalesD.MainView = this.gvBM_SalesD;
			this.gcBM_SalesD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.gcBM_SalesD.Name = "gcBM_SalesD";
			this.gcBM_SalesD.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemCheckEdit2,
            this.repositoryItemCheckEdit3,
            this.repositoryItemLookUpEdit3,
            this.repositoryItemTextEdit1});
			this.gcBM_SalesD.Size = new System.Drawing.Size(1213, 376);
			this.gcBM_SalesD.TabIndex = 4;
			this.gcBM_SalesD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBM_SalesD});
			// 
			// gvBM_SalesD
			// 
			this.gvBM_SalesD.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gvBM_SalesD.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvBM_SalesD.ColumnPanelRowHeight = 31;
			this.gvBM_SalesD.DetailHeight = 450;
			this.gvBM_SalesD.GridControl = this.gcBM_SalesD;
			this.gvBM_SalesD.Name = "gvBM_SalesD";
			this.gvBM_SalesD.OptionsEditForm.PopupEditFormWidth = 900;
			this.gvBM_SalesD.OptionsSelection.CheckBoxSelectorColumnWidth = 45;
			this.gvBM_SalesD.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.gvBM_SalesD.OptionsView.ColumnAutoWidth = false;
			this.gvBM_SalesD.OptionsView.ShowFooter = true;
			this.gvBM_SalesD.OptionsView.ShowGroupPanel = false;
			this.gvBM_SalesD.RowHeight = 31;
			// 
			// repositoryItemLookUpEdit1
			// 
			this.repositoryItemLookUpEdit1.AutoHeight = false;
			this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
			// 
			// repositoryItemCheckEdit2
			// 
			this.repositoryItemCheckEdit2.AutoHeight = false;
			this.repositoryItemCheckEdit2.ContentAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
			this.repositoryItemCheckEdit2.ValueGrayed = true;
			// 
			// repositoryItemCheckEdit3
			// 
			this.repositoryItemCheckEdit3.AutoHeight = false;
			this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
			this.repositoryItemCheckEdit3.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
			this.repositoryItemCheckEdit3.ValueChecked = "Y";
			this.repositoryItemCheckEdit3.ValueGrayed = "";
			this.repositoryItemCheckEdit3.ValueUnchecked = "N";
			// 
			// repositoryItemLookUpEdit3
			// 
			this.repositoryItemLookUpEdit3.AutoHeight = false;
			this.repositoryItemLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemLookUpEdit3.EditFormat.FormatString = "N1";
			this.repositoryItemLookUpEdit3.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemLookUpEdit3.Name = "repositoryItemLookUpEdit3";
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.DisplayFormat.FormatString = "N1";
			this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemTextEdit1.EditFormat.FormatString = "N1";
			this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1227, 417);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.CustomizationFormText = "매출 리스트";
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
			this.layoutControlGroup2.Size = new System.Drawing.Size(1227, 417);
			this.layoutControlGroup2.Text = "출하내역";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.gcBM_SalesD;
			this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem1.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlItem1.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem1.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlItem1.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem1.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlItem1.Size = new System.Drawing.Size(1219, 382);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.ssCustomer);
			this.smtCommonBar1.Controls.Add(this.barDate);
			this.smtCommonBar1.Controls.Add(this.btnPrint);
			this.smtCommonBar1.Controls.Add(this.btnClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1227, 45);
			this.smtCommonBar1.smtImageTitleAutoText = false;
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "거래명세서";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 1;
			// 
			// cmdDisplay
			// 
			this.cmdDisplay.AllowFocus = false;
			this.cmdDisplay.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDisplay.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDisplay.Appearance.Options.UseBackColor = true;
			this.cmdDisplay.Appearance.Options.UseForeColor = true;
			this.cmdDisplay.AutoSize = true;
			this.cmdDisplay.Dock = System.Windows.Forms.DockStyle.Left;
			this.cmdDisplay.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDisplay.ImageOptions.ImageIndex = 0;
			this.cmdDisplay.ImageOptions.ImageToTextIndent = 15;
			this.cmdDisplay.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDisplay.Location = new System.Drawing.Point(767, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(79, 45);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 28;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
			// 
			// ssCustomer
			// 
			this.ssCustomer.Dock = System.Windows.Forms.DockStyle.Left;
			this.ssCustomer.Location = new System.Drawing.Point(479, 0);
			this.ssCustomer.LookAndFeel.SkinName = "WXI";
			this.ssCustomer.LookAndFeel.UseDefaultLookAndFeel = false;
			this.ssCustomer.Name = "ssCustomer";
			this.ssCustomer.Size = new System.Drawing.Size(288, 45);
			this.ssCustomer.smtLabelTitleValue = "거래처";
			this.ssCustomer.smtLabelTitleVisible = true;
			this.ssCustomer.smtLabelTitleWidth = 100;
			this.ssCustomer.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.ssCustomer.smtLookUpDataDisplayMember = "";
			this.ssCustomer.smtLookUpDataSource = null;
			this.ssCustomer.smtLookUpDataValueMember = "";
			this.ssCustomer.smtLookUpSelectdIndex = -1;
			this.ssCustomer.smtLookUpText = "";
			this.ssCustomer.smtLookUpValue = "";
			this.ssCustomer.TabIndex = 30;
			// 
			// barDate
			// 
			this.barDate.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDate.Location = new System.Drawing.Point(114, 0);
			this.barDate.LookAndFeel.SkinName = "WXI";
			this.barDate.LookAndFeel.UseDefaultLookAndFeel = false;
			this.barDate.Name = "barDate";
			this.barDate.Size = new System.Drawing.Size(365, 45);
			this.barDate.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.barDate.smtComboTitleItems = new string[0];
			this.barDate.smtComboTitleValue = "";
			this.barDate.smtComboTitleVisible = false;
			this.barDate.smtComboTitleWidth = 100;
			this.barDate.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.barDate.smtEndDateTimeValue = new System.DateTime(2025, 1, 13, 10, 39, 30, 699);
			this.barDate.smtEndDateTimeVisible = true;
			this.barDate.smtLabelTitleValue = "출하일자";
			this.barDate.smtLabelTitleVisible = true;
			this.barDate.smtLabelTitleWidth = 100;
			this.barDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.barDate.smtStartDateTimeValue = new System.DateTime(2025, 1, 13, 10, 39, 30, 695);
			this.barDate.smtStartDateTimeVisible = true;
			this.barDate.TabIndex = 29;
			// 
			// btnPrint
			// 
			this.btnPrint.AllowFocus = false;
			this.btnPrint.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.btnPrint.Appearance.ForeColor = System.Drawing.Color.White;
			this.btnPrint.Appearance.Options.UseBackColor = true;
			this.btnPrint.Appearance.Options.UseForeColor = true;
			this.btnPrint.AutoSize = true;
			this.btnPrint.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnPrint.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.btnPrint.ImageOptions.ImageIndex = 10;
			this.btnPrint.ImageOptions.ImageToTextIndent = 15;
			this.btnPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.btnPrint.Location = new System.Drawing.Point(1030, 0);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btnPrint.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btnPrint.Size = new System.Drawing.Size(118, 45);
			this.btnPrint.smtButtonStyle = baseSMT.EnumButtonStyles.프린터;
			this.btnPrint.TabIndex = 5;
			this.btnPrint.Text = "거래명세서";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnClose
			// 
			this.btnClose.AllowFocus = false;
			this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.btnClose.Appearance.ForeColor = System.Drawing.Color.White;
			this.btnClose.Appearance.Options.UseBackColor = true;
			this.btnClose.Appearance.Options.UseForeColor = true;
			this.btnClose.AutoSize = true;
			this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.btnClose.ImageOptions.ImageIndex = 5;
			this.btnClose.ImageOptions.ImageToTextIndent = 15;
			this.btnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.btnClose.Location = new System.Drawing.Point(1148, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btnClose.Size = new System.Drawing.Size(79, 45);
			this.btnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "닫기";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// frmTradingStatement_UAG
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1227, 462);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmTradingStatement_UAG";
			this.Text = "거래명세서";
			this.Load += new System.EventHandler(this.frmTradingStatement_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gcBM_SalesD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvBM_SalesD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraGrid.GridControl gcBM_SalesD;
		private DevExpress.XtraGrid.Views.Grid.GridView gvBM_SalesD;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchLookUp ssCustomer;
		private baseSMT.smtSearchDatetime barDate;
		private baseSMT.smtSearchButton btnPrint;
		private baseSMT.smtSearchButton btnClose;
	}
}