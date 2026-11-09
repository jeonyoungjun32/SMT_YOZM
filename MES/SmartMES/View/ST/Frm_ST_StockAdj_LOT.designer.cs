
namespace SmartMES.View.ST
{
    partial class Frm_ST_StockAdj_LOT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ST_StockAdj_LOT));
            this.Panel1 = new DevExpress.XtraEditors.PanelControl();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.cmdDisplayStock = new baseSMT.smtSearchButton();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Item = new baseSMT.smtSearchLookUp();
            this.Lup_House = new baseSMT.smtSearchLookUp();
            this.Lup_StockDiv = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_Stock = new DevExpress.XtraGrid.GridControl();
            this.Gv_Stock = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
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
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.checkEdit1);
            this.smtCommonBar1.Controls.Add(this.cmdDisplayStock);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Item);
            this.smtCommonBar1.Controls.Add(this.Lup_House);
            this.smtCommonBar1.Controls.Add(this.Lup_StockDiv);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1493, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "재고조정";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 5;
            // 
            // cmdSave
            // 
            this.cmdSave.AllowFocus = false;
            this.cmdSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdSave.Appearance.Options.UseBackColor = true;
            this.cmdSave.Appearance.Options.UseForeColor = true;
            this.cmdSave.AutoSize = true;
            this.cmdSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdSave.ImageOptions.Image = global::SmartMES.Properties.Resources.승인_20;
            this.cmdSave.ImageOptions.ImageIndex = 1;
            this.cmdSave.ImageOptions.ImageToTextIndent = 15;
            this.cmdSave.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdSave.Location = new System.Drawing.Point(1184, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(76, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdSave.TabIndex = 15;
            this.cmdSave.Text = "조정";
            this.cmdSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkEdit1.Location = new System.Drawing.Point(1109, 0);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.checkEdit1.Properties.Appearance.Options.UseForeColor = true;
            this.checkEdit1.Properties.Caption = "0보이게";
            this.checkEdit1.Size = new System.Drawing.Size(75, 40);
            this.checkEdit1.TabIndex = 289;
            // 
            // cmdDisplayStock
            // 
            this.cmdDisplayStock.AllowFocus = false;
            this.cmdDisplayStock.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdDisplayStock.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdDisplayStock.Appearance.Options.UseBackColor = true;
            this.cmdDisplayStock.Appearance.Options.UseForeColor = true;
            this.cmdDisplayStock.AutoSize = true;
            this.cmdDisplayStock.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdDisplayStock.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdDisplayStock.ImageOptions.ImageIndex = 0;
            this.cmdDisplayStock.ImageOptions.ImageToTextIndent = 15;
            this.cmdDisplayStock.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdDisplayStock.Location = new System.Drawing.Point(1238, 0);
            this.cmdDisplayStock.Name = "cmdDisplayStock";
            this.cmdDisplayStock.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplayStock.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplayStock.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplayStock.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplayStock.TabIndex = 288;
            this.cmdDisplayStock.Text = "이력";
            this.cmdDisplayStock.Click += new System.EventHandler(this.cmdDisplayStock_Click);
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
            this.cmdExcel.Location = new System.Drawing.Point(1315, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(101, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 287;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
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
            this.btn_Search.Location = new System.Drawing.Point(1032, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 14;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // Lup_Item
            // 
            this.Lup_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Item.Location = new System.Drawing.Point(744, 0);
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
            this.Lup_Item.TabIndex = 13;
            this.Lup_Item.smtLookUpEditValueChanged += new System.EventHandler(this.Lup_Item_smtLookUpEditValueChanged);
            // 
            // Lup_House
            // 
            this.Lup_House.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_House.Location = new System.Drawing.Point(528, 0);
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
            this.Lup_House.TabIndex = 12;
            // 
            // Lup_StockDiv
            // 
            this.Lup_StockDiv.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_StockDiv.Location = new System.Drawing.Point(312, 0);
            this.Lup_StockDiv.LookAndFeel.SkinName = "WXI";
            this.Lup_StockDiv.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_StockDiv.Name = "Lup_StockDiv";
            this.Lup_StockDiv.Size = new System.Drawing.Size(216, 40);
            this.Lup_StockDiv.smtLabelTitleValue = "창고구분";
            this.Lup_StockDiv.smtLabelTitleVisible = true;
            this.Lup_StockDiv.smtLabelTitleWidth = 90;
            this.Lup_StockDiv.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_StockDiv.smtLookUpDataDisplayMember = "코드명";
            this.Lup_StockDiv.smtLookUpDataSource = null;
            this.Lup_StockDiv.smtLookUpDataValueMember = "코드";
            this.Lup_StockDiv.smtLookUpSelectdIndex = -1;
            this.Lup_StockDiv.smtLookUpText = "";
            this.Lup_StockDiv.smtLookUpValue = "";
            this.Lup_StockDiv.TabIndex = 11;
            this.Lup_StockDiv.Visible = false;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(92, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Name = "Dt_Date";
            this.Dt_Date.Size = new System.Drawing.Size(220, 40);
            this.Dt_Date.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Dt_Date.smtComboTitleItems = new string[0];
            this.Dt_Date.smtComboTitleValue = "";
            this.Dt_Date.smtComboTitleVisible = false;
            this.Dt_Date.smtComboTitleWidth = 100;
            this.Dt_Date.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 729);
            this.Dt_Date.smtEndDateTimeVisible = false;
            this.Dt_Date.smtLabelTitleValue = "조정일자";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 10;
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
            this.cmdClose.Click += new System.EventHandler(this.CmdClose_Click);
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
            // Frm_ST_StockAdj_LOT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1493, 641);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_ST_StockAdj_LOT";
            this.Text = "재고조정";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Event_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MT_Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
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
        private baseSMT.smtSearchLookUp Lup_StockDiv;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl Gc_Stock;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Stock;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton cmdExcel;
		private baseSMT.smtSearchButton cmdDisplayStock;
		private DevExpress.XtraEditors.CheckEdit checkEdit1;
		private baseSMT.smtSearchButton cmdSave;
	}
}