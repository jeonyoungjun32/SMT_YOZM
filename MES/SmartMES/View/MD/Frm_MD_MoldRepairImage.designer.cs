
namespace SmartMES.View.MD
{
	partial class Frm_MD_MoldRepairImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MD_MoldRepairImage));
            this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
            this.PicImage = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_ItemImage = new DevExpress.XtraGrid.GridControl();
            this.Gv_ItemImage = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.ItemPdf = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemPic = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.Btn_Row_Ins = new baseSMT.smtSearchButton();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.BtnSave = new baseSMT.smtSearchButton();
            this.Btn_Row_Del = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.PicImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_ItemImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ItemImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Location = new System.Drawing.Point(913, 378);
            this.pdfViewer1.Margin = new System.Windows.Forms.Padding(4);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(779, 321);
            this.pdfViewer1.TabIndex = 1;
            // 
            // PicImage
            // 
            this.PicImage.Location = new System.Drawing.Point(913, 53);
            this.PicImage.Margin = new System.Windows.Forms.Padding(4);
            this.PicImage.Name = "PicImage";
            this.PicImage.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.PicImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.PicImage.Size = new System.Drawing.Size(779, 319);
            this.PicImage.StyleController = this.layoutControl1;
            this.PicImage.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.PicImage);
            this.layoutControl1.Controls.Add(this.pdfViewer1);
            this.layoutControl1.Controls.Add(this.Gc_ItemImage);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(18, 246, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1711, 718);
            this.layoutControl1.TabIndex = 283;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Gc_ItemImage
            // 
            this.Gc_ItemImage.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.Gc_ItemImage.Location = new System.Drawing.Point(19, 53);
            this.Gc_ItemImage.MainView = this.Gv_ItemImage;
            this.Gc_ItemImage.Margin = new System.Windows.Forms.Padding(4);
            this.Gc_ItemImage.Name = "Gc_ItemImage";
            this.Gc_ItemImage.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemMemoEdit1});
            this.Gc_ItemImage.Size = new System.Drawing.Size(836, 646);
            this.Gc_ItemImage.TabIndex = 282;
            this.Gc_ItemImage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_ItemImage});
            // 
            // Gv_ItemImage
            // 
            this.Gv_ItemImage.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.Gv_ItemImage.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Gv_ItemImage.DetailHeight = 467;
            this.Gv_ItemImage.GridControl = this.Gc_ItemImage;
            this.Gv_ItemImage.Name = "Gv_ItemImage";
            this.Gv_ItemImage.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_ItemImage.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1711, 718);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.splitterItem1,
            this.ItemPdf,
            this.ItemPic});
            this.layoutControlGroup1.Location = new System.Drawing.Point(874, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(837, 718);
            this.layoutControlGroup1.Text = "이미지";
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(20, 652);
            // 
            // ItemPdf
            // 
            this.ItemPdf.Control = this.pdfViewer1;
            this.ItemPdf.Location = new System.Drawing.Point(20, 325);
            this.ItemPdf.Name = "ItemPdf";
            this.ItemPdf.Size = new System.Drawing.Size(785, 327);
            this.ItemPdf.TextSize = new System.Drawing.Size(0, 0);
            this.ItemPdf.TextVisible = false;
            // 
            // ItemPic
            // 
            this.ItemPic.Control = this.PicImage;
            this.ItemPic.Location = new System.Drawing.Point(20, 0);
            this.ItemPic.Name = "ItemPic";
            this.ItemPic.Size = new System.Drawing.Size(785, 325);
            this.ItemPic.TextSize = new System.Drawing.Size(0, 0);
            this.ItemPic.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(874, 718);
            this.layoutControlGroup2.Text = "금형세척/수리";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.Gc_ItemImage;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(842, 652);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.Btn_Row_Ins);
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.BtnSave);
            this.smtCommonBar1.Controls.Add(this.Btn_Row_Del);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1711, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "금형 이미지";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 282;
            // 
            // Btn_Row_Ins
            // 
            this.Btn_Row_Ins.AllowFocus = false;
            this.Btn_Row_Ins.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.Btn_Row_Ins.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_Row_Ins.Appearance.Options.UseBackColor = true;
            this.Btn_Row_Ins.Appearance.Options.UseForeColor = true;
            this.Btn_Row_Ins.AutoSize = true;
            this.Btn_Row_Ins.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Row_Ins.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.Btn_Row_Ins.ImageOptions.ImageIndex = 1;
            this.Btn_Row_Ins.ImageOptions.ImageToTextIndent = 15;
            this.Btn_Row_Ins.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Btn_Row_Ins.Location = new System.Drawing.Point(1343, 0);
            this.Btn_Row_Ins.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Row_Ins.Name = "Btn_Row_Ins";
            this.Btn_Row_Ins.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.Btn_Row_Ins.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.Btn_Row_Ins.Size = new System.Drawing.Size(92, 40);
            this.Btn_Row_Ins.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.Btn_Row_Ins.TabIndex = 7;
            this.Btn_Row_Ins.Tag = "Ins";
            this.Btn_Row_Ins.Text = "추가";
            this.Btn_Row_Ins.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdDisplay.Location = new System.Drawing.Point(131, 0);
            this.cmdDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(92, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 5;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Visible = false;
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
            this.BtnSave.Location = new System.Drawing.Point(1435, 0);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.BtnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnSave.Size = new System.Drawing.Size(92, 40);
            this.BtnSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "저장";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // Btn_Row_Del
            // 
            this.Btn_Row_Del.AllowFocus = false;
            this.Btn_Row_Del.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.Btn_Row_Del.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_Row_Del.Appearance.Options.UseBackColor = true;
            this.Btn_Row_Del.Appearance.Options.UseForeColor = true;
            this.Btn_Row_Del.AutoSize = true;
            this.Btn_Row_Del.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Row_Del.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.Btn_Row_Del.ImageOptions.ImageIndex = 3;
            this.Btn_Row_Del.ImageOptions.ImageToTextIndent = 15;
            this.Btn_Row_Del.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Btn_Row_Del.Location = new System.Drawing.Point(1527, 0);
            this.Btn_Row_Del.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Row_Del.Name = "Btn_Row_Del";
            this.Btn_Row_Del.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.Btn_Row_Del.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.Btn_Row_Del.Size = new System.Drawing.Size(92, 40);
            this.Btn_Row_Del.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.Btn_Row_Del.TabIndex = 2;
            this.Btn_Row_Del.Tag = "Del";
            this.Btn_Row_Del.Text = "삭제";
            this.Btn_Row_Del.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdClose.Location = new System.Drawing.Point(1619, 0);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(92, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Frm_MD_MoldRepairImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1711, 758);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_MD_MoldRepairImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "금형 이미지";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_MD_MoldRepairImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_ItemImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ItemImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraEditors.PictureEdit PicImage;
		private DevExpress.XtraPdfViewer.PdfViewer pdfViewer1;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlItem ItemPdf;
		private DevExpress.XtraLayout.LayoutControlItem ItemPic;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton Btn_Row_Ins;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton BtnSave;
		private baseSMT.smtSearchButton Btn_Row_Del;
		private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraGrid.GridControl Gc_ItemImage;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_ItemImage;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}