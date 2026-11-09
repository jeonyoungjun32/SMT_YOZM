namespace SmartMES.View.ST
{
    partial class Frm_ST_StockLotView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ST_StockLotView));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.BtnSearch = new baseSMT.smtSearchButton();
            this.TxtLot = new baseSMT.smtSearchTextBox();
            this.BtnExcel = new baseSMT.smtSearchButton();
            this.BtnClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.GcLot = new DevExpress.XtraGrid.GridControl();
            this.GvLot = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Lup_Item = new baseSMT.smtSearchLookUp();
            this.Lup_House = new baseSMT.smtSearchLookUp();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcLot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvLot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.BtnSearch);
            this.smtCommonBar1.Controls.Add(this.TxtLot);
            this.smtCommonBar1.Controls.Add(this.Lup_Item);
            this.smtCommonBar1.Controls.Add(this.Lup_House);
            this.smtCommonBar1.Controls.Add(this.BtnExcel);
            this.smtCommonBar1.Controls.Add(this.BtnClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1275, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "LOT별재고현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 0;
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
            this.BtnSearch.Location = new System.Drawing.Point(864, 0);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnSearch.Size = new System.Drawing.Size(77, 40);
            this.BtnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.BtnSearch.TabIndex = 7;
            this.BtnSearch.Text = "조회";
            this.BtnSearch.Click += new System.EventHandler(this.Btn_Click);
            // 
            // TxtLot
            // 
            this.TxtLot.Dock = System.Windows.Forms.DockStyle.Left;
            this.TxtLot.Location = new System.Drawing.Point(635, 0);
            this.TxtLot.LookAndFeel.SkinName = "WXI";
            this.TxtLot.LookAndFeel.UseDefaultLookAndFeel = false;
            this.TxtLot.Name = "TxtLot";
            this.TxtLot.Size = new System.Drawing.Size(229, 40);
            this.TxtLot.smtLabelTitleValue = "LotNo.";
            this.TxtLot.smtLabelTitleVisible = true;
            this.TxtLot.smtLabelTitleWidth = 70;
            this.TxtLot.smtTextBoxDecimal = 0;
            this.TxtLot.smtTextBoxMust = false;
            this.TxtLot.smtTextBoxNumber = false;
            this.TxtLot.smtTextBoxReadOnly = false;
            this.TxtLot.smtTextBoxValue = "";
            this.TxtLot.TabIndex = 8;
            // 
            // BtnExcel
            // 
            this.BtnExcel.AllowFocus = false;
            this.BtnExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.BtnExcel.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnExcel.Appearance.Options.UseBackColor = true;
            this.BtnExcel.Appearance.Options.UseForeColor = true;
            this.BtnExcel.AutoSize = true;
            this.BtnExcel.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnExcel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnExcel.ImageOptions.ImageIndex = 9;
            this.BtnExcel.ImageOptions.ImageToTextIndent = 15;
            this.BtnExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.BtnExcel.Location = new System.Drawing.Point(1097, 0);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnExcel.Size = new System.Drawing.Size(101, 40);
            this.BtnExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.BtnExcel.TabIndex = 4;
            this.BtnExcel.Text = "엑셀변환";
            this.BtnExcel.Click += new System.EventHandler(this.Btn_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.AllowFocus = false;
            this.BtnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.BtnClose.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnClose.Appearance.Options.UseBackColor = true;
            this.BtnClose.Appearance.Options.UseForeColor = true;
            this.BtnClose.AutoSize = true;
            this.BtnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnClose.ImageOptions.ImageIndex = 5;
            this.BtnClose.ImageOptions.ImageToTextIndent = 15;
            this.BtnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.BtnClose.Location = new System.Drawing.Point(1198, 0);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnClose.Size = new System.Drawing.Size(77, 40);
            this.BtnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.BtnClose.TabIndex = 3;
            this.BtnClose.Text = "닫기";
            this.BtnClose.Click += new System.EventHandler(this.Btn_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.GcLot);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1275, 519);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // GcLot
            // 
            this.GcLot.Location = new System.Drawing.Point(3, 3);
            this.GcLot.MainView = this.GvLot;
            this.GcLot.Name = "GcLot";
            this.GcLot.Size = new System.Drawing.Size(1269, 513);
            this.GcLot.TabIndex = 4;
            this.GcLot.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvLot});
            // 
            // GvLot
            // 
            this.GvLot.GridControl = this.GcLot;
            this.GvLot.Name = "GvLot";
            this.GvLot.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1275, 519);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GcLot;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1275, 519);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Lup_Item
            // 
            this.Lup_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Item.Location = new System.Drawing.Point(347, 0);
            this.Lup_Item.LookAndFeel.SkinName = "WXI";
            this.Lup_Item.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Item.Name = "Lup_Item";
            this.Lup_Item.Size = new System.Drawing.Size(288, 40);
            this.Lup_Item.smtLabelTitleValue = "품목";
            this.Lup_Item.smtLabelTitleVisible = true;
            this.Lup_Item.smtLabelTitleWidth = 100;
            this.Lup_Item.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Item.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Item.smtLookUpDataSource = null;
            this.Lup_Item.smtLookUpDataValueMember = "코드";
            this.Lup_Item.smtLookUpSelectdIndex = -1;
            this.Lup_Item.smtLookUpText = "";
            this.Lup_Item.smtLookUpValue = "";
            this.Lup_Item.TabIndex = 15;
            // 
            // Lup_House
            // 
            this.Lup_House.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_House.Location = new System.Drawing.Point(131, 0);
            this.Lup_House.LookAndFeel.SkinName = "WXI";
            this.Lup_House.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_House.Name = "Lup_House";
            this.Lup_House.Size = new System.Drawing.Size(216, 40);
            this.Lup_House.smtLabelTitleValue = "창고";
            this.Lup_House.smtLabelTitleVisible = true;
            this.Lup_House.smtLabelTitleWidth = 80;
            this.Lup_House.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_House.smtLookUpDataDisplayMember = "코드명";
            this.Lup_House.smtLookUpDataSource = null;
            this.Lup_House.smtLookUpDataValueMember = "코드";
            this.Lup_House.smtLookUpSelectdIndex = -1;
            this.Lup_House.smtLookUpText = "";
            this.Lup_House.smtLookUpValue = "";
            this.Lup_House.TabIndex = 14;
            // 
            // frmStockLotView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 559);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmStockLotView";
            this.Text = "LOT별재고현황";
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcLot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvLot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl GcLot;
        private DevExpress.XtraGrid.Views.Grid.GridView GvLot;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton BtnExcel;
        private baseSMT.smtSearchButton BtnClose;
        private baseSMT.smtSearchButton BtnSearch;
        private baseSMT.smtSearchTextBox TxtLot;
        private baseSMT.smtSearchLookUp Lup_Item;
        private baseSMT.smtSearchLookUp Lup_House;
    }
}