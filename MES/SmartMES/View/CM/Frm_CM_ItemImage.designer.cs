
namespace SMT_MES.View.CM
{
	partial class Frm_CM_ItemImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_ItemImage));
            this.Gc_ItemImage = new DevExpress.XtraGrid.GridControl();
            this.Gv_ItemImage = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
            this.PicImage = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Rdo_Item_Div = new DevExpress.XtraEditors.RadioGroup();
            this.Gc_Item = new DevExpress.XtraGrid.GridControl();
            this.Gv_Product = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Asy = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Goods = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Material = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Subsidiary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Consume = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Jig = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.ItemPdf = new DevExpress.XtraLayout.LayoutControlItem();
            this.ItemPic = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdInsert = new baseSMT.smtSearchButton();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_ItemImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ItemImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Rdo_Item_Div.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Product)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Asy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Goods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Material)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Subsidiary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Consume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Jig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPdf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Gc_ItemImage
            // 
            this.Gc_ItemImage.Location = new System.Drawing.Point(546, 46);
            this.Gc_ItemImage.MainView = this.Gv_ItemImage;
            this.Gc_ItemImage.Name = "Gc_ItemImage";
            this.Gc_ItemImage.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemMemoEdit1});
            this.Gc_ItemImage.Size = new System.Drawing.Size(433, 539);
            this.Gc_ItemImage.TabIndex = 282;
            this.Gc_ItemImage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_ItemImage});
            // 
            // Gv_ItemImage
            // 
            this.Gv_ItemImage.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.Gv_ItemImage.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Gv_ItemImage.DetailHeight = 397;
            this.Gv_ItemImage.GridControl = this.Gc_ItemImage;
            this.Gv_ItemImage.Name = "Gv_ItemImage";
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
            // pdfViewer1
            // 
            this.pdfViewer1.Location = new System.Drawing.Point(1001, 318);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(349, 267);
            this.pdfViewer1.TabIndex = 1;
            // 
            // PicImage
            // 
            this.PicImage.Location = new System.Drawing.Point(1001, 46);
            this.PicImage.Name = "PicImage";
            this.PicImage.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.PicImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.PicImage.Size = new System.Drawing.Size(349, 266);
            this.PicImage.StyleController = this.layoutControl1;
            this.PicImage.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Rdo_Item_Div);
            this.layoutControl1.Controls.Add(this.PicImage);
            this.layoutControl1.Controls.Add(this.pdfViewer1);
            this.layoutControl1.Controls.Add(this.Gc_ItemImage);
            this.layoutControl1.Controls.Add(this.Gc_Item);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(18, 246, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1369, 604);
            this.layoutControl1.TabIndex = 283;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Rdo_Item_Div
            // 
            this.Rdo_Item_Div.Location = new System.Drawing.Point(83, 46);
            this.Rdo_Item_Div.Name = "Rdo_Item_Div";
            this.Rdo_Item_Div.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Rdo_Item_Div.Properties.Appearance.Options.UseBackColor = true;
            this.Rdo_Item_Div.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Rdo_Item_Div.Size = new System.Drawing.Size(425, 44);
            this.Rdo_Item_Div.StyleController = this.layoutControl1;
            this.Rdo_Item_Div.TabIndex = 280;
            // 
            // Gc_Item
            // 
            this.Gc_Item.Location = new System.Drawing.Point(19, 96);
            this.Gc_Item.MainView = this.Gv_Product;
            this.Gc_Item.Name = "Gc_Item";
            this.Gc_Item.Size = new System.Drawing.Size(489, 489);
            this.Gc_Item.TabIndex = 2;
            this.Gc_Item.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Product,
            this.Gv_Asy,
            this.Gv_Goods,
            this.Gv_Material,
            this.Gv_Subsidiary,
            this.Gv_Consume,
            this.Gv_Jig});
            // 
            // Gv_Product
            // 
            this.Gv_Product.DetailHeight = 397;
            this.Gv_Product.GridControl = this.Gc_Item;
            this.Gv_Product.Name = "Gv_Product";
            this.Gv_Product.OptionsView.ShowGroupPanel = false;
            this.Gv_Product.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.Gv_Product_FocusedRowChanged);
            // 
            // Gv_Asy
            // 
            this.Gv_Asy.DetailHeight = 397;
            this.Gv_Asy.GridControl = this.Gc_Item;
            this.Gv_Asy.Name = "Gv_Asy";
            this.Gv_Asy.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Goods
            // 
            this.Gv_Goods.DetailHeight = 397;
            this.Gv_Goods.GridControl = this.Gc_Item;
            this.Gv_Goods.Name = "Gv_Goods";
            this.Gv_Goods.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Material
            // 
            this.Gv_Material.DetailHeight = 397;
            this.Gv_Material.GridControl = this.Gc_Item;
            this.Gv_Material.Name = "Gv_Material";
            this.Gv_Material.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Subsidiary
            // 
            this.Gv_Subsidiary.DetailHeight = 397;
            this.Gv_Subsidiary.GridControl = this.Gc_Item;
            this.Gv_Subsidiary.Name = "Gv_Subsidiary";
            this.Gv_Subsidiary.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Consume
            // 
            this.Gv_Consume.DetailHeight = 397;
            this.Gv_Consume.GridControl = this.Gc_Item;
            this.Gv_Consume.Name = "Gv_Consume";
            this.Gv_Consume.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Jig
            // 
            this.Gv_Jig.DetailHeight = 397;
            this.Gv_Jig.GridControl = this.Gc_Item;
            this.Gv_Jig.Name = "Gv_Jig";
            this.Gv_Jig.OptionsView.ShowGroupPanel = false;
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
            this.Root.Size = new System.Drawing.Size(1369, 604);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.splitterItem1,
            this.ItemPdf,
            this.ItemPic});
            this.layoutControlGroup1.Location = new System.Drawing.Point(527, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(842, 604);
            this.layoutControlGroup1.Text = "이미지";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.Gc_ItemImage;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(439, 545);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(439, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(16, 545);
            // 
            // ItemPdf
            // 
            this.ItemPdf.Control = this.pdfViewer1;
            this.ItemPdf.Location = new System.Drawing.Point(455, 272);
            this.ItemPdf.Name = "ItemPdf";
            this.ItemPdf.Size = new System.Drawing.Size(355, 273);
            this.ItemPdf.TextSize = new System.Drawing.Size(0, 0);
            this.ItemPdf.TextVisible = false;
            // 
            // ItemPic
            // 
            this.ItemPic.Control = this.PicImage;
            this.ItemPic.Location = new System.Drawing.Point(455, 0);
            this.ItemPic.Name = "ItemPic";
            this.ItemPic.Size = new System.Drawing.Size(355, 272);
            this.ItemPic.TextSize = new System.Drawing.Size(0, 0);
            this.ItemPic.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(527, 604);
            this.layoutControlGroup2.Text = "품목";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Gc_Item;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(495, 495);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.Rdo_Item_Div;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(0, 50);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(120, 50);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(495, 50);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "품목구분";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(48, 17);
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdInsert);
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.cmdDelete);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1369, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "품목 이미지";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 282;
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
            this.cmdInsert.Location = new System.Drawing.Point(1061, 0);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdInsert.Size = new System.Drawing.Size(77, 40);
            this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdInsert.TabIndex = 7;
            this.cmdInsert.Tag = "Ins";
            this.cmdInsert.Text = "추가";
            this.cmdInsert.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdDisplay.Location = new System.Drawing.Point(107, 0);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 5;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Visible = false;
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
            this.cmdSave.Location = new System.Drawing.Point(1138, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "저장";
            this.cmdSave.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.cmdDelete.Location = new System.Drawing.Point(1215, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(77, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Tag = "Del";
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdClose.Location = new System.Drawing.Point(1292, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Frm_CM_ItemImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1369, 644);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_CM_ItemImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "품목 이미지";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_CM_ItemImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_ItemImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ItemImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Rdo_Item_Div.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Product)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Asy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Goods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Material)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Subsidiary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Consume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Jig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPdf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraGrid.GridControl Gc_ItemImage;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_ItemImage;
		private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
		private DevExpress.XtraEditors.PictureEdit PicImage;
		private DevExpress.XtraPdfViewer.PdfViewer pdfViewer1;
		private DevExpress.XtraGrid.GridControl Gc_Item;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Product;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Asy;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Goods;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Material;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Subsidiary;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Consume;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Jig;
		private DevExpress.XtraEditors.RadioGroup Rdo_Item_Div;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlItem ItemPdf;
		private DevExpress.XtraLayout.LayoutControlItem ItemPic;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdInsert;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDelete;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
	}
}