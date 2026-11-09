
namespace SmartMES.View.PM
{
	partial class Frm_PM_OrderResultWorker
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PM_OrderResultWorker));
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.BtnSearch = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.GcWorker = new DevExpress.XtraGrid.GridControl();
			this.GvWorker = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.GcList = new DevExpress.XtraGrid.GridControl();
			this.GvList = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.Rdo = new DevExpress.XtraEditors.RadioGroup();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GcWorker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvWorker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GcList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Rdo.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.BtnSearch);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1106, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "작업자 등록";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 8;
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
			this.cmdSave.Location = new System.Drawing.Point(952, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 299;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.Btn_Click);
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
			this.BtnSearch.Location = new System.Drawing.Point(107, 0);
			this.BtnSearch.Name = "BtnSearch";
			this.BtnSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.BtnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnSearch.Size = new System.Drawing.Size(77, 40);
			this.BtnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.BtnSearch.TabIndex = 5;
			this.BtnSearch.Text = "조회";
			this.BtnSearch.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(1029, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.Btn_Click);
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.GcWorker);
			this.layoutControl1.Controls.Add(this.GcList);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 82);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1106, 622);
			this.layoutControl1.TabIndex = 9;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// GcWorker
			// 
			this.GcWorker.Location = new System.Drawing.Point(573, 44);
			this.GcWorker.MainView = this.GvWorker;
			this.GcWorker.Name = "GcWorker";
			this.GcWorker.Size = new System.Drawing.Size(516, 561);
			this.GcWorker.TabIndex = 5;
			this.GcWorker.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvWorker});
			// 
			// GvWorker
			// 
			this.GvWorker.GridControl = this.GcWorker;
			this.GvWorker.Name = "GvWorker";
			// 
			// GcList
			// 
			this.GcList.Location = new System.Drawing.Point(17, 44);
			this.GcList.MainView = this.GvList;
			this.GcList.Name = "GcList";
			this.GcList.Size = new System.Drawing.Size(532, 561);
			this.GcList.TabIndex = 4;
			this.GcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvList});
			// 
			// GvList
			// 
			this.GvList.GridControl = this.GcList;
			this.GvList.Name = "GvList";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.splitterItem1,
            this.layoutControlGroup1,
            this.layoutControlGroup2});
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(1106, 622);
			this.Root.TextVisible = false;
			// 
			// splitterItem1
			// 
			this.splitterItem1.AllowHotTrack = true;
			this.splitterItem1.Location = new System.Drawing.Point(540, 0);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(16, 596);
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(540, 596);
			this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Text = "작업자 리스트";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.GcList;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(538, 567);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup2.Location = new System.Drawing.Point(556, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(524, 596);
			this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Text = "선택 리스트";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.GcWorker;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(522, 567);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// Rdo
			// 
			this.Rdo.Dock = System.Windows.Forms.DockStyle.Top;
			this.Rdo.Location = new System.Drawing.Point(0, 40);
			this.Rdo.Name = "Rdo";
			this.Rdo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Rdo.Properties.Appearance.Options.UseBackColor = true;
			this.Rdo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.Rdo.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "1공정")});
			this.Rdo.Size = new System.Drawing.Size(1106, 42);
			this.Rdo.TabIndex = 385;
			this.Rdo.SelectedIndexChanged += new System.EventHandler(this.Rdo_SelectedIndexChanged);
			// 
			// FrmPM_OrderResultWorker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1106, 704);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.Rdo);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_PM_OrderResultWorker";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "작업자 등록";
			this.Load += new System.EventHandler(this.Frm_PM_OrderResultWorker_Load);
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GcWorker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvWorker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GcList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Rdo.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton BtnSearch;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraGrid.GridControl GcWorker;
		private DevExpress.XtraGrid.Views.Grid.GridView GvWorker;
		private DevExpress.XtraGrid.GridControl GcList;
		private DevExpress.XtraGrid.Views.Grid.GridView GvList;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraEditors.RadioGroup Rdo;
	}
}