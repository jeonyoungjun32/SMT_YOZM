namespace SmartMES.View.SM
{
	partial class Frm_SM_NoticeView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SM_NoticeView));
			this.ribbonControl2 = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.colQty = new DevExpress.XtraGrid.Columns.GridColumn();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdSearch = new baseSMT.smtSearchButton();
			this.smtDatetime = new baseSMT.smtSearchDatetime();
			this.btnInsert = new baseSMT.smtSearchButton();
			this.btnUpdate = new baseSMT.smtSearchButton();
			this.btnDelete = new baseSMT.smtSearchButton();
			this.btnClose = new baseSMT.smtSearchButton();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.gcSM_Notice = new DevExpress.XtraGrid.GridControl();
			this.gvSM_Notice = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.repositoryItemLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.ribbonControl2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gcSM_Notice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvSM_Notice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// ribbonControl2
			// 
			this.ribbonControl2.BackColor = System.Drawing.Color.Gray;
			this.ribbonControl2.ExpandCollapseItem.Id = 0;
			this.ribbonControl2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl2.ExpandCollapseItem});
			this.ribbonControl2.Location = new System.Drawing.Point(0, 0);
			this.ribbonControl2.MaxItemId = 19;
			this.ribbonControl2.Name = "ribbonControl2";
			this.ribbonControl2.Size = new System.Drawing.Size(1263, 0);
			// 
			// colQty
			// 
			this.colQty.AppearanceCell.Options.UseTextOptions = true;
			this.colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colQty.AppearanceHeader.Options.UseTextOptions = true;
			this.colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colQty.Caption = "수량";
			this.colQty.FieldName = "Qty";
			this.colQty.Name = "colQty";
			this.colQty.Visible = true;
			this.colQty.VisibleIndex = 7;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdSearch);
			this.smtCommonBar1.Controls.Add(this.smtDatetime);
			this.smtCommonBar1.Controls.Add(this.btnInsert);
			this.smtCommonBar1.Controls.Add(this.btnUpdate);
			this.smtCommonBar1.Controls.Add(this.btnDelete);
			this.smtCommonBar1.Controls.Add(this.btnClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1163, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "공지사항 등록";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 1;
			this.smtCommonBar1.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdSearch.Location = new System.Drawing.Point(464, 0);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSearch.Size = new System.Drawing.Size(77, 40);
			this.cmdSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdSearch.TabIndex = 6;
			this.cmdSearch.Text = "조회";
			this.cmdSearch.Click += new System.EventHandler(this.Btn_Click);
			// 
			// smtDatetime
			// 
			this.smtDatetime.Dock = System.Windows.Forms.DockStyle.Left;
			this.smtDatetime.Location = new System.Drawing.Point(119, 0);
			this.smtDatetime.LookAndFeel.SkinName = "WXI";
			this.smtDatetime.LookAndFeel.UseDefaultLookAndFeel = false;
			this.smtDatetime.Name = "smtDatetime";
			this.smtDatetime.Size = new System.Drawing.Size(345, 40);
			this.smtDatetime.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.smtDatetime.smtComboTitleItems = new string[0];
			this.smtDatetime.smtComboTitleValue = "";
			this.smtDatetime.smtComboTitleVisible = false;
			this.smtDatetime.smtComboTitleWidth = 100;
			this.smtDatetime.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.smtDatetime.smtEndDateTimeValue = new System.DateTime(2024, 12, 16, 18, 20, 32, 231);
			this.smtDatetime.smtEndDateTimeVisible = true;
			this.smtDatetime.smtLabelTitleValue = "작성일자";
			this.smtDatetime.smtLabelTitleVisible = true;
			this.smtDatetime.smtLabelTitleWidth = 100;
			this.smtDatetime.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.smtDatetime.smtStartDateTimeValue = new System.DateTime(2024, 12, 16, 18, 20, 32, 229);
			this.smtDatetime.smtStartDateTimeVisible = true;
			this.smtDatetime.TabIndex = 5;
			// 
			// btnInsert
			// 
			this.btnInsert.AllowFocus = false;
			this.btnInsert.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.btnInsert.Appearance.ForeColor = System.Drawing.Color.White;
			this.btnInsert.Appearance.Options.UseBackColor = true;
			this.btnInsert.Appearance.Options.UseForeColor = true;
			this.btnInsert.AutoSize = true;
			this.btnInsert.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnInsert.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.btnInsert.ImageOptions.ImageIndex = 1;
			this.btnInsert.ImageOptions.ImageToTextIndent = 15;
			this.btnInsert.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.btnInsert.Location = new System.Drawing.Point(855, 0);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btnInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btnInsert.Size = new System.Drawing.Size(77, 40);
			this.btnInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.btnInsert.TabIndex = 4;
			this.btnInsert.Text = "등록";
			this.btnInsert.Click += new System.EventHandler(this.Btn_Click);
			// 
			// btnUpdate
			// 
			this.btnUpdate.AllowFocus = false;
			this.btnUpdate.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.btnUpdate.Appearance.ForeColor = System.Drawing.Color.White;
			this.btnUpdate.Appearance.Options.UseBackColor = true;
			this.btnUpdate.Appearance.Options.UseForeColor = true;
			this.btnUpdate.AutoSize = true;
			this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnUpdate.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.btnUpdate.ImageOptions.ImageIndex = 2;
			this.btnUpdate.ImageOptions.ImageToTextIndent = 15;
			this.btnUpdate.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.btnUpdate.Location = new System.Drawing.Point(932, 0);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btnUpdate.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btnUpdate.Size = new System.Drawing.Size(77, 40);
			this.btnUpdate.smtButtonStyle = baseSMT.EnumButtonStyles.수정;
			this.btnUpdate.TabIndex = 3;
			this.btnUpdate.Text = "수정";
			this.btnUpdate.Click += new System.EventHandler(this.Btn_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.AllowFocus = false;
			this.btnDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.btnDelete.Appearance.ForeColor = System.Drawing.Color.White;
			this.btnDelete.Appearance.Options.UseBackColor = true;
			this.btnDelete.Appearance.Options.UseForeColor = true;
			this.btnDelete.AutoSize = true;
			this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.btnDelete.ImageOptions.ImageIndex = 3;
			this.btnDelete.ImageOptions.ImageToTextIndent = 15;
			this.btnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.btnDelete.Location = new System.Drawing.Point(1009, 0);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btnDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btnDelete.Size = new System.Drawing.Size(77, 40);
			this.btnDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "삭제";
			this.btnDelete.Click += new System.EventHandler(this.Btn_Click);
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
			this.btnClose.Location = new System.Drawing.Point(1086, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btnClose.Size = new System.Drawing.Size(77, 40);
			this.btnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "닫기";
			this.btnClose.Click += new System.EventHandler(this.Btn_Click);
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.gcSM_Notice);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(419, 19, 650, 400);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1163, 530);
			this.layoutControl1.TabIndex = 2;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// gcSM_Notice
			// 
			this.gcSM_Notice.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(70)))), ((int)(((byte)(68)))));
			this.gcSM_Notice.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gcSM_Notice.Location = new System.Drawing.Point(16, 43);
			this.gcSM_Notice.MainView = this.gvSM_Notice;
			this.gcSM_Notice.Name = "gcSM_Notice";
			this.gcSM_Notice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit3});
			this.gcSM_Notice.Size = new System.Drawing.Size(1131, 471);
			this.gcSM_Notice.TabIndex = 4;
			this.gcSM_Notice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSM_Notice});
			// 
			// gvSM_Notice
			// 
			this.gvSM_Notice.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gvSM_Notice.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvSM_Notice.GridControl = this.gcSM_Notice;
			this.gvSM_Notice.Name = "gvSM_Notice";
			this.gvSM_Notice.OptionsBehavior.Editable = false;
			this.gvSM_Notice.OptionsDetail.EnableMasterViewMode = false;
			this.gvSM_Notice.OptionsView.ColumnAutoWidth = false;
			this.gvSM_Notice.OptionsView.ShowAutoFilterRow = true;
			this.gvSM_Notice.OptionsView.ShowGroupPanel = false;
			// 
			// repositoryItemLookUpEdit3
			// 
			this.repositoryItemLookUpEdit3.AutoHeight = false;
			this.repositoryItemLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemLookUpEdit3.Name = "repositoryItemLookUpEdit3";
			this.repositoryItemLookUpEdit3.NullText = "";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(1163, 530);
			this.Root.Text = "공지사항 리스트";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.gcSM_Notice;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1137, 477);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// FrmSM_Notice
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1163, 570);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.LookAndFeel.SkinName = "WXI";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Name = "FrmSM_Notice";
			this.Text = "공지사항 등록";
			((System.ComponentModel.ISupportInitialize)(this.ribbonControl2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gcSM_Notice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvSM_Notice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl2;
		private DevExpress.XtraGrid.Columns.GridColumn colQty;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdSearch;
		private baseSMT.smtSearchDatetime smtDatetime;
		private baseSMT.smtSearchButton btnInsert;
		private baseSMT.smtSearchButton btnUpdate;
		private baseSMT.smtSearchButton btnDelete;
		private baseSMT.smtSearchButton btnClose;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraGrid.GridControl gcSM_Notice;
		private DevExpress.XtraGrid.Views.Grid.GridView gvSM_Notice;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
	}
}