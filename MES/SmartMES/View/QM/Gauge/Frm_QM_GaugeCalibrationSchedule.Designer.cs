
namespace SmartMES.View.QM.Gauge
{
	partial class Frm_QM_GaugeCalibrationSchedule
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_GaugeCalibrationSchedule));
			this.Gc_Gauge = new DevExpress.XtraGrid.GridControl();
			this.Gv_Gauge = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdSearch = new baseSMT.smtSearchButton();
			this.cmdExcel = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Gauge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Gauge)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Gc_Gauge
			// 
			this.Gc_Gauge.Location = new System.Drawing.Point(7, 34);
			this.Gc_Gauge.MainView = this.Gv_Gauge;
			this.Gc_Gauge.Name = "Gc_Gauge";
			this.Gc_Gauge.Size = new System.Drawing.Size(1028, 447);
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
			// layoutControl1
			// 
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
            this.layoutControlGroup3});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1042, 488);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
			this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(1042, 488);
			this.layoutControlGroup3.Text = "교정내역";
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.Gc_Gauge;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(1034, 453);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdSearch);
			this.smtCommonBar1.Controls.Add(this.cmdExcel);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1042, 40);
			this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "교정예정현황";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 289;
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
			this.cmdSearch.Location = new System.Drawing.Point(116, 0);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSearch.Size = new System.Drawing.Size(77, 40);
			this.cmdSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdSearch.TabIndex = 5;
			this.cmdSearch.Text = "조회";
			this.cmdSearch.Click += new System.EventHandler(this.cmd_Click);
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
			// Frm_QM_GaugeCalibrationSchedule
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1042, 528);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_QM_GaugeCalibrationSchedule";
			this.Text = "교정예정현황";
			this.Load += new System.EventHandler(this.Frm_QM_GaugeCalibration_Load);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Gauge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Gauge)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraGrid.GridControl Gc_Gauge;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Gauge;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdSearch;
		private baseSMT.smtSearchButton cmdClose;
		private baseSMT.smtSearchButton cmdExcel;
	}
}