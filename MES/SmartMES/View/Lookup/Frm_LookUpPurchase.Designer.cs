
namespace SmartMES.View.Lookup
{
    partial class Frm_LookUpPurchase
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LookUpPurchase));
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.btn_Search = new baseSMT.smtSearchButton();
			this.Lup_Cust = new baseSMT.smtSearchLookUp();
			this.Dt_Date = new baseSMT.smtSearchDatetime();
			this.cmdInsert = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.gcDetail = new DevExpress.XtraGrid.GridControl();
			this.gvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			this.SuspendLayout();
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.btn_Search);
			this.smtCommonBar1.Controls.Add(this.Lup_Cust);
			this.smtCommonBar1.Controls.Add(this.Dt_Date);
			this.smtCommonBar1.Controls.Add(this.cmdInsert);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1067, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "입고품목선택";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 8;
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
			this.btn_Search.Location = new System.Drawing.Point(749, 0);
			this.btn_Search.Name = "btn_Search";
			this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btn_Search.Size = new System.Drawing.Size(77, 40);
			this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.btn_Search.TabIndex = 12;
			this.btn_Search.Text = "조회";
			this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
			// 
			// Lup_Cust
			// 
			this.Lup_Cust.Dock = System.Windows.Forms.DockStyle.Left;
			this.Lup_Cust.Location = new System.Drawing.Point(461, 0);
			this.Lup_Cust.LookAndFeel.SkinName = "WXI";
			this.Lup_Cust.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Lup_Cust.Name = "Lup_Cust";
			this.Lup_Cust.Size = new System.Drawing.Size(288, 40);
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
			this.Lup_Cust.TabIndex = 11;
			// 
			// Dt_Date
			// 
			this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
			this.Dt_Date.Location = new System.Drawing.Point(116, 0);
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
			this.Dt_Date.smtLabelTitleValue = "조회기간";
			this.Dt_Date.smtLabelTitleVisible = true;
			this.Dt_Date.smtLabelTitleWidth = 100;
			this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
			this.Dt_Date.smtStartDateTimeVisible = true;
			this.Dt_Date.TabIndex = 10;
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
			this.cmdInsert.Location = new System.Drawing.Point(913, 0);
			this.cmdInsert.Name = "cmdInsert";
			this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInsert.Size = new System.Drawing.Size(77, 40);
			this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdInsert.TabIndex = 7;
			this.cmdInsert.Tag = "Ins";
			this.cmdInsert.Text = "선택";
			this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(990, 0);
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
			this.layoutControl1.Controls.Add(this.gcDetail);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1067, 562);
			this.layoutControl1.TabIndex = 9;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// gcDetail
			// 
			this.gcDetail.Cursor = System.Windows.Forms.Cursors.Default;
			this.gcDetail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
			this.gcDetail.Location = new System.Drawing.Point(7, 7);
			this.gcDetail.MainView = this.gvDetail;
			this.gcDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.gcDetail.Name = "gcDetail";
			this.gcDetail.Size = new System.Drawing.Size(1053, 548);
			this.gcDetail.TabIndex = 5;
			this.gcDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetail});
			// 
			// gvDetail
			// 
			this.gvDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gvDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvDetail.ColumnPanelRowHeight = 36;
			this.gvDetail.DetailHeight = 496;
			this.gvDetail.FixedLineWidth = 4;
			this.gvDetail.GridControl = this.gcDetail;
			this.gvDetail.Name = "gvDetail";
			this.gvDetail.OptionsSelection.CheckBoxSelectorColumnWidth = 46;
			this.gvDetail.OptionsSelection.MultiSelect = true;
			this.gvDetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.gvDetail.OptionsView.ColumnAutoWidth = false;
			this.gvDetail.OptionsView.ShowGroupPanel = false;
			this.gvDetail.RowHeight = 36;
			this.gvDetail.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvDetail_SelectionChanged_1);
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1067, 562);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.CustomizationFormText = "품목 리스트";
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 3;
			this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(1067, 562);
			this.layoutControlGroup3.Text = "입고품목 리스트";
			this.layoutControlGroup3.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.gcDetail;
			this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlItem2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlItem2.Size = new System.Drawing.Size(1059, 554);
			this.layoutControlItem2.Text = "layoutControlItem1";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// frmLookUpPurchase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1067, 602);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "Frm_LookUpPurchase";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "입고품목선택";
			this.Load += new System.EventHandler(this.Frm_LookUpPurchase_Load);
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchLookUp Lup_Cust;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton cmdInsert;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetail;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}