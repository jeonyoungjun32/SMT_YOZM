namespace SmartMES.View.ST
{
    partial class Frm_ST_StockInOutTotal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ST_StockInOutTotal));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcInOut = new DevExpress.XtraGrid.GridControl();
            this.gvInOut = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.ssCM_Item = new baseSMT.smtSearchLookUp();
            this.smtHouseCode = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.cmdExcel = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcInOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcInOut);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1628, 627);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcInOut
            // 
            this.gcInOut.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.gcInOut.Location = new System.Drawing.Point(3, 3);
            this.gcInOut.MainView = this.gvInOut;
            this.gcInOut.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gcInOut.Name = "gcInOut";
            this.gcInOut.Size = new System.Drawing.Size(1622, 621);
            this.gcInOut.TabIndex = 46;
            this.gcInOut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInOut});
            // 
            // gvInOut
            // 
            this.gvInOut.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gvInOut.Appearance.FooterPanel.Options.UseFont = true;
            this.gvInOut.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.gvInOut.Appearance.GroupFooter.Options.UseFont = true;
            this.gvInOut.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvInOut.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvInOut.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand6,
            this.gridBand1,
            this.gridBand8});
            this.gvInOut.DetailHeight = 412;
            this.gvInOut.GridControl = this.gcInOut;
            this.gvInOut.Name = "gvInOut";
            this.gvInOut.OptionsEditForm.PopupEditFormWidth = 1000;
            this.gvInOut.OptionsView.ColumnAutoWidth = false;
            this.gvInOut.OptionsView.ShowAutoFilterRow = true;
            this.gvInOut.OptionsView.ShowFooter = true;
            this.gvInOut.OptionsView.ShowGroupPanel = false;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.Caption = "재고조정";
            this.gridBand6.MinWidth = 14;
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.Visible = false;
            this.gridBand6.VisibleIndex = -1;
            this.gridBand6.Width = 99;
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "gridBand1";
            this.gridBand1.MinWidth = 14;
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.Visible = false;
            this.gridBand1.VisibleIndex = -1;
            this.gridBand1.Width = 87;
            // 
            // gridBand8
            // 
            this.gridBand8.Caption = "gridBand8";
            this.gridBand8.MinWidth = 14;
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.Visible = false;
            this.gridBand8.VisibleIndex = -1;
            this.gridBand8.Width = 87;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1628, 627);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcInOut;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1628, 627);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.ssCM_Item);
            this.smtCommonBar1.Controls.Add(this.smtHouseCode);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1628, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "입출고집계";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 7;
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
            this.btn_Search.Location = new System.Drawing.Point(1163, 0);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(92, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 42;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // ssCM_Item
            // 
            this.ssCM_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.ssCM_Item.Location = new System.Drawing.Point(803, 0);
            this.ssCM_Item.LookAndFeel.SkinName = "WXI";
            this.ssCM_Item.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ssCM_Item.Margin = new System.Windows.Forms.Padding(4);
            this.ssCM_Item.Name = "ssCM_Item";
            this.ssCM_Item.Size = new System.Drawing.Size(360, 40);
            this.ssCM_Item.smtLabelTitleValue = "품목";
            this.ssCM_Item.smtLabelTitleVisible = true;
            this.ssCM_Item.smtLabelTitleWidth = 100;
            this.ssCM_Item.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ssCM_Item.smtLookUpDataDisplayMember = "";
            this.ssCM_Item.smtLookUpDataSource = null;
            this.ssCM_Item.smtLookUpDataValueMember = "";
            this.ssCM_Item.smtLookUpSelectdIndex = -1;
            this.ssCM_Item.smtLookUpText = "";
            this.ssCM_Item.smtLookUpValue = "";
            this.ssCM_Item.TabIndex = 331;
            // 
            // smtHouseCode
            // 
            this.smtHouseCode.Dock = System.Windows.Forms.DockStyle.Left;
            this.smtHouseCode.Location = new System.Drawing.Point(472, 0);
            this.smtHouseCode.LookAndFeel.SkinName = "WXI";
            this.smtHouseCode.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtHouseCode.Margin = new System.Windows.Forms.Padding(4);
            this.smtHouseCode.Name = "smtHouseCode";
            this.smtHouseCode.Size = new System.Drawing.Size(331, 40);
            this.smtHouseCode.smtLabelTitleValue = "창고";
            this.smtHouseCode.smtLabelTitleVisible = true;
            this.smtHouseCode.smtLabelTitleWidth = 100;
            this.smtHouseCode.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.smtHouseCode.smtLookUpDataDisplayMember = "코드명";
            this.smtHouseCode.smtLookUpDataSource = null;
            this.smtHouseCode.smtLookUpDataValueMember = "코드";
            this.smtHouseCode.smtLookUpSelectdIndex = -1;
            this.smtHouseCode.smtLookUpText = "";
            this.smtHouseCode.smtLookUpValue = "";
            this.smtHouseCode.TabIndex = 330;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(127, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Margin = new System.Windows.Forms.Padding(4);
            this.Dt_Date.Name = "Dt_Date";
            this.Dt_Date.Size = new System.Drawing.Size(345, 40);
            this.Dt_Date.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Dt_Date.smtComboTitleItems = new string[0];
            this.Dt_Date.smtComboTitleValue = "";
            this.Dt_Date.smtComboTitleVisible = false;
            this.Dt_Date.smtComboTitleWidth = 100;
            this.Dt_Date.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 5, 8, 12, 41, 0, 172);
            this.Dt_Date.smtEndDateTimeVisible = true;
            this.Dt_Date.smtLabelTitleValue = "조회일자";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 5, 8, 12, 41, 0, 168);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 43;
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
            this.cmdClose.Location = new System.Drawing.Point(1536, 0);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(92, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.CmdClose_Click);
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
            this.cmdExcel.Location = new System.Drawing.Point(1414, 0);
            this.cmdExcel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(122, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 6;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // Frm_ST_StockInOutTotal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1628, 667);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "Frm_ST_StockInOutTotal";
            this.Text = "입출고집계";
            this.Load += new System.EventHandler(this.frmStockDaily2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcInOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcInOut;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvInOut;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchLookUp smtHouseCode;
        private baseSMT.smtSearchLookUp ssCM_Item;
        private baseSMT.smtSearchButton cmdExcel;
    }
}