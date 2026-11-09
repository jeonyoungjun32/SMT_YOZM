
namespace SmartMES.View.ST
{
    partial class Frm_ST_StockBase
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ST_StockBase));
			this.Panel1 = new DevExpress.XtraEditors.PanelControl();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdExcelUp = new baseSMT.smtSearchButton();
			this.cmdExcelDown = new baseSMT.smtSearchButton();
			this.btn_Search = new baseSMT.smtSearchButton();
			this.Lup_Item = new baseSMT.smtSearchLookUp();
			this.Lup_House = new baseSMT.smtSearchLookUp();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.Gc_Stock = new DevExpress.XtraGrid.GridControl();
			this.Gv_Stock = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.cmdDelete = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.Panel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Stock)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Stock)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// Panel1
			// 
			this.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.Panel1.Location = new System.Drawing.Point(2, 23);
			this.Panel1.Name = "Panel1";
			this.Panel1.Padding = new System.Windows.Forms.Padding(3);
			this.Panel1.Size = new System.Drawing.Size(1465, 45);
			this.Panel1.TabIndex = 271;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDelete);
			this.smtCommonBar1.Controls.Add(this.cmdExcelUp);
			this.smtCommonBar1.Controls.Add(this.cmdExcelDown);
			this.smtCommonBar1.Controls.Add(this.btn_Search);
			this.smtCommonBar1.Controls.Add(this.Lup_Item);
			this.smtCommonBar1.Controls.Add(this.Lup_House);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1493, 40);
			this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "기초재고";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 5;
			// 
			// cmdExcelUp
			// 
			this.cmdExcelUp.AllowFocus = false;
			this.cmdExcelUp.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdExcelUp.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdExcelUp.Appearance.Options.UseBackColor = true;
			this.cmdExcelUp.Appearance.Options.UseForeColor = true;
			this.cmdExcelUp.AutoSize = true;
			this.cmdExcelUp.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdExcelUp.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdExcelUp.ImageOptions.ImageIndex = 8;
			this.cmdExcelUp.ImageOptions.ImageToTextIndent = 15;
			this.cmdExcelUp.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdExcelUp.Location = new System.Drawing.Point(1202, 0);
			this.cmdExcelUp.Name = "cmdExcelUp";
			this.cmdExcelUp.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdExcelUp.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdExcelUp.Size = new System.Drawing.Size(113, 40);
			this.cmdExcelUp.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀업로드;
			this.cmdExcelUp.TabIndex = 288;
			this.cmdExcelUp.Text = "엑셀업로드";
			this.cmdExcelUp.Click += new System.EventHandler(this.Cmd_Click);
			// 
			// cmdExcelDown
			// 
			this.cmdExcelDown.AllowFocus = false;
			this.cmdExcelDown.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdExcelDown.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdExcelDown.Appearance.Options.UseBackColor = true;
			this.cmdExcelDown.Appearance.Options.UseForeColor = true;
			this.cmdExcelDown.AutoSize = true;
			this.cmdExcelDown.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdExcelDown.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdExcelDown.ImageOptions.ImageIndex = 9;
			this.cmdExcelDown.ImageOptions.ImageToTextIndent = 15;
			this.cmdExcelDown.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdExcelDown.Location = new System.Drawing.Point(1315, 0);
			this.cmdExcelDown.Name = "cmdExcelDown";
			this.cmdExcelDown.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdExcelDown.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdExcelDown.Size = new System.Drawing.Size(101, 40);
			this.cmdExcelDown.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
			this.cmdExcelDown.TabIndex = 287;
			this.cmdExcelDown.Text = "엑셀변환";
			this.cmdExcelDown.Click += new System.EventHandler(this.Cmd_Click);
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
			this.btn_Search.Location = new System.Drawing.Point(596, 0);
			this.btn_Search.Name = "btn_Search";
			this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btn_Search.Size = new System.Drawing.Size(77, 40);
			this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.btn_Search.TabIndex = 14;
			this.btn_Search.Text = "조회";
			this.btn_Search.Click += new System.EventHandler(this.Cmd_Click);
			// 
			// Lup_Item
			// 
			this.Lup_Item.Dock = System.Windows.Forms.DockStyle.Left;
			this.Lup_Item.Location = new System.Drawing.Point(308, 0);
			this.Lup_Item.LookAndFeel.SkinName = "WXI";
			this.Lup_Item.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Lup_Item.Name = "Lup_Item";
			this.Lup_Item.Size = new System.Drawing.Size(288, 40);
			this.Lup_Item.smtLabelForeColor = System.Drawing.Color.White;
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
			this.Lup_Item.TabIndex = 13;
			// 
			// Lup_House
			// 
			this.Lup_House.Dock = System.Windows.Forms.DockStyle.Left;
			this.Lup_House.Location = new System.Drawing.Point(92, 0);
			this.Lup_House.LookAndFeel.SkinName = "WXI";
			this.Lup_House.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Lup_House.Name = "Lup_House";
			this.Lup_House.Size = new System.Drawing.Size(216, 40);
			this.Lup_House.smtLabelForeColor = System.Drawing.Color.White;
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
			this.Lup_House.TabIndex = 12;
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
			this.cmdClose.Location = new System.Drawing.Point(1416, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 5;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.Cmd_Click);
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.Gc_Stock);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1493, 601);
			this.layoutControl1.TabIndex = 6;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// Gc_Stock
			// 
			this.Gc_Stock.Location = new System.Drawing.Point(3, 3);
			this.Gc_Stock.MainView = this.Gv_Stock;
			this.Gc_Stock.Name = "Gc_Stock";
			this.Gc_Stock.Size = new System.Drawing.Size(1487, 595);
			this.Gc_Stock.TabIndex = 274;
			this.Gc_Stock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Stock});
			// 
			// Gv_Stock
			// 
			this.Gv_Stock.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.Gv_Stock.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.Gv_Stock.GridControl = this.Gc_Stock;
			this.Gv_Stock.Name = "Gv_Stock";
			this.Gv_Stock.OptionsView.ShowGroupPanel = false;
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1493, 601);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.Gc_Stock;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1493, 601);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
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
			this.cmdDelete.Location = new System.Drawing.Point(1125, 0);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDelete.Size = new System.Drawing.Size(77, 40);
			this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDelete.TabIndex = 289;
			this.cmdDelete.Text = "삭제";
			this.cmdDelete.Click += new System.EventHandler(this.Cmd_Click);
			// 
			// Frm_ST_StockBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1493, 641);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_ST_StockBase";
			this.Text = "기초재고";
			this.Load += new System.EventHandler(this.Frm_MT_Stock_Load);
			((System.ComponentModel.ISupportInitialize)(this.Panel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Stock)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Stock)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl Panel1;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchLookUp Lup_Item;
        private baseSMT.smtSearchLookUp Lup_House;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl Gc_Stock;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Stock;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton cmdExcelDown;
		private baseSMT.smtSearchButton cmdExcelUp;
		private baseSMT.smtSearchButton cmdDelete;
	}
}