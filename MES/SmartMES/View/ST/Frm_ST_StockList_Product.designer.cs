namespace SmartMES.View.ST
{
    partial class Frm_ST_StockList_Product
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ST_StockList_Product));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Item = new baseSMT.smtSearchLookUp();
            this.Lup_House = new baseSMT.smtSearchLookUp();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmdExcel = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Item);
            this.smtCommonBar1.Controls.Add(this.Lup_House);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1601, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "재고 관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 5;
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
            this.btn_Search.Location = new System.Drawing.Point(836, 0);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(92, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 13;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.CmdSearch_Click_1);
            // 
            // Lup_Item
            // 
            this.Lup_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Item.Location = new System.Drawing.Point(476, 0);
            this.Lup_Item.LookAndFeel.SkinName = "WXI";
            this.Lup_Item.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Item.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lup_Item.Name = "Lup_Item";
            this.Lup_Item.Size = new System.Drawing.Size(360, 40);
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
            this.Lup_Item.TabIndex = 11;
            // 
            // Lup_House
            // 
            this.Lup_House.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_House.Location = new System.Drawing.Point(116, 0);
            this.Lup_House.LookAndFeel.SkinName = "WXI";
            this.Lup_House.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_House.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lup_House.Name = "Lup_House";
            this.Lup_House.Size = new System.Drawing.Size(360, 40);
            this.Lup_House.smtLabelTitleValue = "창고";
            this.Lup_House.smtLabelTitleVisible = true;
            this.Lup_House.smtLabelTitleWidth = 100;
            this.Lup_House.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_House.smtLookUpDataDisplayMember = "코드명";
            this.Lup_House.smtLookUpDataSource = null;
            this.Lup_House.smtLookUpDataValueMember = "코드";
            this.Lup_House.smtLookUpSelectdIndex = -1;
            this.Lup_House.smtLookUpText = "";
            this.Lup_House.smtLookUpValue = "";
            this.Lup_House.TabIndex = 12;
            // 
            // smtSearchButton1
            // 
            this.smtSearchButton1.AllowFocus = false;
            this.smtSearchButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton1.Appearance.Options.UseBackColor = true;
            this.smtSearchButton1.Appearance.Options.UseForeColor = true;
            this.smtSearchButton1.AutoSize = true;
            this.smtSearchButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton1.ImageOptions.ImageIndex = 5;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(1509, 0);
            this.smtSearchButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(92, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.smtSearchButton1.TabIndex = 5;
            this.smtSearchButton1.Text = "닫기";
            this.smtSearchButton1.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1601, 614);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcList
            // 
            this.gcList.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.gcList.Location = new System.Drawing.Point(9, 43);
            this.gcList.MainView = this.gvList;
            this.gcList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(1583, 562);
            this.gcList.TabIndex = 39;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvList});
            // 
            // gvList
            // 
            this.gvList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvList.ColumnPanelRowHeight = 29;
            this.gvList.DetailHeight = 500;
            this.gvList.GridControl = this.gcList;
            this.gvList.Name = "gvList";
            this.gvList.OptionsEditForm.PopupEditFormWidth = 1000;
            this.gvList.OptionsSelection.CheckBoxSelectorColumnWidth = 57;
            this.gvList.OptionsSelection.MultiSelect = true;
            this.gvList.OptionsView.BestFitUseErrorInfo = DevExpress.Utils.DefaultBoolean.True;
            this.gvList.OptionsView.ColumnAutoWidth = false;
            this.gvList.OptionsView.ShowAutoFilterRow = true;
            this.gvList.OptionsView.ShowGroupPanel = false;
            this.gvList.OptionsView.ShowIndicator = false;
            this.gvList.RowHeight = 29;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1601, 614);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "재고 리스트";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(1601, 614);
            this.layoutControlGroup4.Text = "재고 리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcList;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1591, 570);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
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
            this.cmdExcel.Location = new System.Drawing.Point(1387, 0);
            this.cmdExcel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(122, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 41;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // Frm_ST_StockList_Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 654);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Frm_ST_StockList_Product";
            this.Text = "재고 관리";
            this.Load += new System.EventHandler(this.frmBomProductList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtSearchLookUp Lup_Item;
        private baseSMT.smtSearchLookUp Lup_House;
        private baseSMT.smtSearchButton btn_Search;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvList;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton cmdExcel;
    }
}