
namespace SmartMES.View.CM
{
    partial class Frm_CM_CommonFile_Item
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_CommonFile_Item));
			this.GcDocumentFile = new DevExpress.XtraGrid.GridControl();
			this.GvDocumentFile = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.BtnDownload = new baseSMT.smtSearchButton();
			this.BtnUpload = new baseSMT.smtSearchButton();
			this.BtnSave = new baseSMT.smtSearchButton();
			this.BtnDelete = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.TreeListClass = new DevExpress.XtraTreeList.TreeList();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.GcItem = new DevExpress.XtraGrid.GridControl();
			this.GvItem = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.GcDocumentFile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvDocumentFile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TreeListClass)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GcItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			this.SuspendLayout();
			// 
			// GcDocumentFile
			// 
			this.GcDocumentFile.Location = new System.Drawing.Point(545, 65);
			this.GcDocumentFile.MainView = this.GvDocumentFile;
			this.GcDocumentFile.Name = "GcDocumentFile";
			this.GcDocumentFile.Size = new System.Drawing.Size(669, 463);
			this.GcDocumentFile.TabIndex = 278;
			this.GcDocumentFile.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvDocumentFile});
			// 
			// GvDocumentFile
			// 
			this.GvDocumentFile.DetailHeight = 397;
			this.GvDocumentFile.GridControl = this.GcDocumentFile;
			this.GvDocumentFile.Name = "GvDocumentFile";
			this.GvDocumentFile.OptionsEditForm.PopupEditFormWidth = 914;
			// 
			// panelControl1
			// 
			this.panelControl1.Location = new System.Drawing.Point(545, 31);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(669, 28);
			this.panelControl1.TabIndex = 0;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.BtnDownload);
			this.smtCommonBar1.Controls.Add(this.BtnUpload);
			this.smtCommonBar1.Controls.Add(this.BtnSave);
			this.smtCommonBar1.Controls.Add(this.BtnDelete);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1218, 40);
			this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "공통문서관리";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 278;
			// 
			// BtnDownload
			// 
			this.BtnDownload.AllowFocus = false;
			this.BtnDownload.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.BtnDownload.Appearance.ForeColor = System.Drawing.Color.White;
			this.BtnDownload.Appearance.Options.UseBackColor = true;
			this.BtnDownload.Appearance.Options.UseForeColor = true;
			this.BtnDownload.AutoSize = true;
			this.BtnDownload.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtnDownload.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.BtnDownload.ImageOptions.ImageIndex = 9;
			this.BtnDownload.ImageOptions.ImageToTextIndent = 15;
			this.BtnDownload.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDownload.ImageOptions.SvgImage")));
			this.BtnDownload.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.BtnDownload.Location = new System.Drawing.Point(797, 0);
			this.BtnDownload.Name = "BtnDownload";
			this.BtnDownload.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.BtnDownload.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnDownload.Size = new System.Drawing.Size(101, 40);
			this.BtnDownload.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
			this.BtnDownload.TabIndex = 5;
			this.BtnDownload.Text = "다운로드";
			this.BtnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
			// 
			// BtnUpload
			// 
			this.BtnUpload.AllowFocus = false;
			this.BtnUpload.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.BtnUpload.Appearance.ForeColor = System.Drawing.Color.White;
			this.BtnUpload.Appearance.Options.UseBackColor = true;
			this.BtnUpload.Appearance.Options.UseForeColor = true;
			this.BtnUpload.AutoSize = true;
			this.BtnUpload.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtnUpload.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.BtnUpload.ImageOptions.ImageIndex = 8;
			this.BtnUpload.ImageOptions.ImageToTextIndent = 15;
			this.BtnUpload.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnUpload.ImageOptions.SvgImage")));
			this.BtnUpload.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.BtnUpload.Location = new System.Drawing.Point(898, 0);
			this.BtnUpload.Name = "BtnUpload";
			this.BtnUpload.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.BtnUpload.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnUpload.Size = new System.Drawing.Size(89, 40);
			this.BtnUpload.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀업로드;
			this.BtnUpload.TabIndex = 4;
			this.BtnUpload.Text = "업로드";
			this.BtnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
			// 
			// BtnSave
			// 
			this.BtnSave.AllowFocus = false;
			this.BtnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.BtnSave.Appearance.ForeColor = System.Drawing.Color.White;
			this.BtnSave.Appearance.Options.UseBackColor = true;
			this.BtnSave.Appearance.Options.UseForeColor = true;
			this.BtnSave.AutoSize = true;
			this.BtnSave.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtnSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.BtnSave.ImageOptions.ImageIndex = 4;
			this.BtnSave.ImageOptions.ImageToTextIndent = 15;
			this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.BtnSave.Location = new System.Drawing.Point(987, 0);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.BtnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnSave.Size = new System.Drawing.Size(77, 40);
			this.BtnSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.BtnSave.TabIndex = 6;
			this.BtnSave.Text = "저장";
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// BtnDelete
			// 
			this.BtnDelete.AllowFocus = false;
			this.BtnDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.BtnDelete.Appearance.ForeColor = System.Drawing.Color.White;
			this.BtnDelete.Appearance.Options.UseBackColor = true;
			this.BtnDelete.Appearance.Options.UseForeColor = true;
			this.BtnDelete.AutoSize = true;
			this.BtnDelete.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtnDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.BtnDelete.ImageOptions.ImageIndex = 3;
			this.BtnDelete.ImageOptions.ImageToTextIndent = 15;
			this.BtnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.BtnDelete.Location = new System.Drawing.Point(1064, 0);
			this.BtnDelete.Name = "BtnDelete";
			this.BtnDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.BtnDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnDelete.Size = new System.Drawing.Size(77, 40);
			this.BtnDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.BtnDelete.TabIndex = 2;
			this.BtnDelete.Text = "삭제";
			this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(1141, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.GcItem);
			this.layoutControl1.Controls.Add(this.TreeListClass);
			this.layoutControl1.Controls.Add(this.panelControl1);
			this.layoutControl1.Controls.Add(this.GcDocumentFile);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1270, 426, 650, 400);
			this.layoutControl1.Root = this.layoutControlGroup3;
			this.layoutControl1.Size = new System.Drawing.Size(1218, 532);
			this.layoutControl1.TabIndex = 280;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// TreeListClass
			// 
			this.TreeListClass.ColumnPanelRowHeight = 31;
			this.TreeListClass.Cursor = System.Windows.Forms.Cursors.Default;
			this.TreeListClass.KeyFieldName = "CLASS";
			this.TreeListClass.Location = new System.Drawing.Point(4, 31);
			this.TreeListClass.Name = "TreeListClass";
			this.TreeListClass.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.MouseDown;
			this.TreeListClass.OptionsBehavior.PopulateServiceColumns = true;
			this.TreeListClass.OptionsView.AutoWidth = false;
			this.TreeListClass.ParentFieldName = "ParentId";
			this.TreeListClass.RowHeight = 31;
			this.TreeListClass.Size = new System.Drawing.Size(272, 497);
			this.TreeListClass.TabIndex = 281;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup3.GroupBordersVisible = false;
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.splitterItem1,
            this.layoutControlGroup1});
			this.layoutControlGroup3.Name = "Root";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(1218, 532);
			this.layoutControlGroup3.TextVisible = false;
			// 
			// layoutControlGroup4
			// 
			this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup4.Name = "layoutControlGroup4";
			this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup4.Size = new System.Drawing.Size(280, 532);
			this.layoutControlGroup4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup4.Text = "문서분류";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.TreeListClass;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(278, 503);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroup5
			// 
			this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3});
			this.layoutControlGroup5.Location = new System.Drawing.Point(541, 0);
			this.layoutControlGroup5.Name = "layoutControlGroup5";
			this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup5.Size = new System.Drawing.Size(677, 532);
			this.layoutControlGroup5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup5.Text = "문서리스트";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.GcDocumentFile;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(675, 469);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.panelControl1;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(675, 34);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// splitterItem1
			// 
			this.splitterItem1.AllowHotTrack = true;
			this.splitterItem1.Location = new System.Drawing.Point(525, 0);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(16, 532);
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
			this.layoutControlGroup1.Location = new System.Drawing.Point(280, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(245, 532);
			this.layoutControlGroup1.Text = "품목";
			// 
			// GcItem
			// 
			this.GcItem.Location = new System.Drawing.Point(287, 34);
			this.GcItem.MainView = this.GvItem;
			this.GcItem.Name = "GcItem";
			this.GcItem.Size = new System.Drawing.Size(231, 491);
			this.GcItem.TabIndex = 282;
			this.GcItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvItem});
			// 
			// GvItem
			// 
			this.GvItem.DetailHeight = 397;
			this.GvItem.GridControl = this.GcItem;
			this.GvItem.Name = "GvItem";
			this.GvItem.OptionsEditForm.PopupEditFormWidth = 914;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.GcItem;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(237, 497);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// Frm_CM_CommonFile_Item
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1218, 572);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_CM_CommonFile_Item";
			this.Text = "공통문서관리";
			this.Load += new System.EventHandler(this.Frm_CM_CommonFile_Item_Load);
			((System.ComponentModel.ISupportInitialize)(this.GcDocumentFile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvDocumentFile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TreeListClass)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GcItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraGrid.GridControl GcDocumentFile;
		private DevExpress.XtraGrid.Views.Grid.GridView GvDocumentFile;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton BtnDownload;
		private baseSMT.smtSearchButton BtnUpload;
		private baseSMT.smtSearchButton BtnDelete;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private DevExpress.XtraTreeList.TreeList TreeListClass;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private baseSMT.smtSearchButton BtnSave;
		private DevExpress.XtraGrid.GridControl GcItem;
		private DevExpress.XtraGrid.Views.Grid.GridView GvItem;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
	}
}