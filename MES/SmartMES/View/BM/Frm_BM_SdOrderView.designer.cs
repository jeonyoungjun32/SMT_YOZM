namespace SmartMES.View.BM
{
    partial class Frm_BM_SdOrderView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BM_SdOrderView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ChkMaterial = new DevExpress.XtraEditors.CheckEdit();
            this.ChkSalesMan = new DevExpress.XtraEditors.CheckEdit();
            this.ChkSoldToCode = new DevExpress.XtraEditors.CheckEdit();
            this.GcOrder = new DevExpress.XtraGrid.GridControl();
            this.GvOrder = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Item = new baseSMT.smtSearchLookUp();
            this.Lup_Cust = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.btn_Excel = new baseSMT.smtSearchButton();
            this.btn_Close = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkSalesMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkSoldToCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.GcOrder);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(3190, 555, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1848, 625);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ChkMaterial);
            this.panelControl1.Controls.Add(this.ChkSalesMan);
            this.panelControl1.Controls.Add(this.ChkSoldToCode);
            this.panelControl1.Location = new System.Drawing.Point(7, 7);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1834, 27);
            this.panelControl1.TabIndex = 32;
            // 
            // ChkMaterial
            // 
            this.ChkMaterial.Location = new System.Drawing.Point(160, 2);
            this.ChkMaterial.Name = "ChkMaterial";
            this.ChkMaterial.Properties.Caption = "품목";
            this.ChkMaterial.Size = new System.Drawing.Size(69, 22);
            this.ChkMaterial.TabIndex = 2;
            this.ChkMaterial.Tag = "ItemCode";
            // 
            // ChkSalesMan
            // 
            this.ChkSalesMan.Location = new System.Drawing.Point(85, 2);
            this.ChkSalesMan.Name = "ChkSalesMan";
            this.ChkSalesMan.Properties.Caption = "담당자";
            this.ChkSalesMan.Size = new System.Drawing.Size(69, 22);
            this.ChkSalesMan.TabIndex = 1;
            this.ChkSalesMan.Tag = "SalesMan";
            // 
            // ChkSoldToCode
            // 
            this.ChkSoldToCode.Location = new System.Drawing.Point(10, 2);
            this.ChkSoldToCode.Name = "ChkSoldToCode";
            this.ChkSoldToCode.Properties.Caption = "거래처";
            this.ChkSoldToCode.Size = new System.Drawing.Size(69, 22);
            this.ChkSoldToCode.TabIndex = 0;
            this.ChkSoldToCode.Tag = "SoldToCode";
            // 
            // GcOrder
            // 
            this.GcOrder.Cursor = System.Windows.Forms.Cursors.Default;
            this.GcOrder.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.GcOrder.Location = new System.Drawing.Point(7, 40);
            this.GcOrder.MainView = this.GvOrder;
            this.GcOrder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GcOrder.Name = "GcOrder";
            this.GcOrder.Size = new System.Drawing.Size(1834, 578);
            this.GcOrder.TabIndex = 4;
            this.GcOrder.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvOrder});
            // 
            // GvOrder
            // 
            this.GvOrder.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GvOrder.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GvOrder.ColumnPanelRowHeight = 29;
            this.GvOrder.DetailHeight = 425;
            this.GvOrder.GridControl = this.GcOrder;
            this.GvOrder.Name = "GvOrder";
            this.GvOrder.OptionsBehavior.Editable = false;
            this.GvOrder.OptionsDetail.EnableMasterViewMode = false;
            this.GvOrder.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.GvOrder.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.GvOrder.OptionsView.ColumnAutoWidth = false;
            this.GvOrder.OptionsView.ShowGroupPanel = false;
            this.GvOrder.RowHeight = 29;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1848, 625);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "주문 등록 마스터";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControlGroup2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup2.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1848, 625);
            this.layoutControlGroup2.Text = "주문 등록 마스터";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GcOrder;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 33);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem1.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlItem1.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem1.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlItem1.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem1.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlItem1.Size = new System.Drawing.Size(1840, 584);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 33);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(5, 33);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1840, 33);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Item);
            this.smtCommonBar1.Controls.Add(this.Lup_Cust);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.btn_Excel);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1848, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "수주현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 2;
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
            this.btn_Search.Location = new System.Drawing.Point(1013, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 13;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // Lup_Item
            // 
            this.Lup_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Item.Location = new System.Drawing.Point(725, 0);
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
            this.Lup_Item.TabIndex = 8;
            // 
            // Lup_Cust
            // 
            this.Lup_Cust.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Cust.Location = new System.Drawing.Point(437, 0);
            this.Lup_Cust.LookAndFeel.SkinName = "WXI";
            this.Lup_Cust.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Cust.Name = "Lup_Cust";
            this.Lup_Cust.Size = new System.Drawing.Size(288, 40);
            this.Lup_Cust.smtLabelForeColor = System.Drawing.Color.White;
            this.Lup_Cust.smtLabelTitleValue = "거래처";
            this.Lup_Cust.smtLabelTitleVisible = true;
            this.Lup_Cust.smtLabelTitleWidth = 100;
            this.Lup_Cust.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Cust.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Cust.smtLookUpDataSource = null;
            this.Lup_Cust.smtLookUpDataValueMember = "코드";
            this.Lup_Cust.smtLookUpSelectdIndex = -1;
            this.Lup_Cust.smtLookUpText = "";
            this.Lup_Cust.smtLookUpValue = "";
            this.Lup_Cust.TabIndex = 9;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(92, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Name = "Dt_Date";
            this.Dt_Date.Size = new System.Drawing.Size(345, 40);
            this.Dt_Date.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Dt_Date.smtComboTitleItems = new string[0];
            this.Dt_Date.smtComboTitleValue = "";
            this.Dt_Date.smtComboTitleVisible = false;
            this.Dt_Date.smtComboTitleWidth = 100;
            this.Dt_Date.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 729);
            this.Dt_Date.smtEndDateTimeVisible = true;
            this.Dt_Date.smtLabelForeColor = System.Drawing.Color.White;
            this.Dt_Date.smtLabelTitleValue = "조회기간";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 7;
            // 
            // btn_Excel
            // 
            this.btn_Excel.AllowFocus = false;
            this.btn_Excel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Excel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Excel.Appearance.Options.UseBackColor = true;
            this.btn_Excel.Appearance.Options.UseForeColor = true;
            this.btn_Excel.AutoSize = true;
            this.btn_Excel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Excel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Excel.ImageOptions.ImageIndex = 9;
            this.btn_Excel.ImageOptions.ImageToTextIndent = 15;
            this.btn_Excel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Excel.Location = new System.Drawing.Point(1670, 0);
            this.btn_Excel.Name = "btn_Excel";
            this.btn_Excel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Excel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Excel.Size = new System.Drawing.Size(101, 40);
            this.btn_Excel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.btn_Excel.TabIndex = 2;
            this.btn_Excel.Text = "엑셀변환";
            this.btn_Excel.Click += new System.EventHandler(this.btn_Excel_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.AllowFocus = false;
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Close.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseForeColor = true;
            this.btn_Close.AutoSize = true;
            this.btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Close.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Close.ImageOptions.ImageIndex = 5;
            this.btn_Close.ImageOptions.ImageToTextIndent = 15;
            this.btn_Close.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Close.Location = new System.Drawing.Point(1771, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(77, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Frm_BM_SdOrderView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1848, 665);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_BM_SdOrderView";
            this.Text = "수주현황";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChkMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkSalesMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkSoldToCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl GcOrder;
        private DevExpress.XtraGrid.Views.Grid.GridView GvOrder;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit ChkSoldToCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit ChkMaterial;
        private DevExpress.XtraEditors.CheckEdit ChkSalesMan;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchLookUp Lup_Item;
        private baseSMT.smtSearchLookUp Lup_Cust;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton btn_Excel;
        private baseSMT.smtSearchButton btn_Close;
        private baseSMT.smtSearchButton btn_Search;
    }
}