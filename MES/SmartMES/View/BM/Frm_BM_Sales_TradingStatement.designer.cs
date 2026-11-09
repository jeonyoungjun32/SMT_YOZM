namespace SmartMES.View.BM
{
    partial class Frm_BM_Sales_TradingStatement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BM_Sales_TradingStatement));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdSearch = new baseSMT.smtSearchButton();
            this.ssCustomer = new baseSMT.smtSearchLookUp();
            this.barEdit = new baseSMT.smtSearchDatetime();
            this.cmdPrint = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcMaster = new DevExpress.XtraGrid.GridControl();
            this.gvMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDetail = new DevExpress.XtraGrid.GridControl();
            this.gvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdSearch);
            this.smtCommonBar1.Controls.Add(this.ssCustomer);
            this.smtCommonBar1.Controls.Add(this.barEdit);
            this.smtCommonBar1.Controls.Add(this.cmdPrint);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1275, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "거래명세서 출력";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 4;
            // 
            // cmdSearch
            // 
            this.cmdSearch.AllowFocus = false;
            this.cmdSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdSearch.Appearance.Options.UseBackColor = true;
            this.cmdSearch.Appearance.Options.UseForeColor = true;
            this.cmdSearch.AutoSize = true;
            this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdSearch.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdSearch.ImageOptions.ImageIndex = 0;
            this.cmdSearch.ImageOptions.ImageToTextIndent = 15;
            this.cmdSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdSearch.Location = new System.Drawing.Point(689, 0);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSearch.Size = new System.Drawing.Size(77, 40);
            this.cmdSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdSearch.TabIndex = 17;
            this.cmdSearch.Text = "조회";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // ssCustomer
            // 
            this.ssCustomer.Dock = System.Windows.Forms.DockStyle.Left;
            this.ssCustomer.Location = new System.Drawing.Point(456, 0);
            this.ssCustomer.LookAndFeel.SkinName = "WXI";
            this.ssCustomer.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ssCustomer.Name = "ssCustomer";
            this.ssCustomer.Size = new System.Drawing.Size(233, 40);
            this.ssCustomer.smtLabelTitleValue = "거래처";
            this.ssCustomer.smtLabelTitleVisible = true;
            this.ssCustomer.smtLabelTitleWidth = 70;
            this.ssCustomer.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ssCustomer.smtLookUpDataDisplayMember = "";
            this.ssCustomer.smtLookUpDataSource = null;
            this.ssCustomer.smtLookUpDataValueMember = "";
            this.ssCustomer.smtLookUpSelectdIndex = -1;
            this.ssCustomer.smtLookUpText = "";
            this.ssCustomer.smtLookUpValue = "";
            this.ssCustomer.TabIndex = 18;
            // 
            // barEdit
            // 
            this.barEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.barEdit.Location = new System.Drawing.Point(131, 0);
            this.barEdit.LookAndFeel.SkinName = "WXI";
            this.barEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barEdit.Name = "barEdit";
            this.barEdit.Size = new System.Drawing.Size(325, 40);
            this.barEdit.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.barEdit.smtComboTitleItems = new string[] {
        "수주일자",
        "납기일자"};
            this.barEdit.smtComboTitleValue = "";
            this.barEdit.smtComboTitleVisible = false;
            this.barEdit.smtComboTitleWidth = 100;
            this.barEdit.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.barEdit.smtEndDateTimeValue = new System.DateTime(2025, 2, 20, 16, 4, 42, 706);
            this.barEdit.smtEndDateTimeVisible = true;
            this.barEdit.smtLabelTitleValue = "수주일자";
            this.barEdit.smtLabelTitleVisible = true;
            this.barEdit.smtLabelTitleWidth = 80;
            this.barEdit.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.barEdit.smtStartDateTimeValue = new System.DateTime(2025, 2, 20, 16, 4, 42, 706);
            this.barEdit.smtStartDateTimeVisible = true;
            this.barEdit.TabIndex = 19;
            // 
            // cmdPrint
            // 
            this.cmdPrint.AllowFocus = false;
            this.cmdPrint.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdPrint.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdPrint.Appearance.Options.UseBackColor = true;
            this.cmdPrint.Appearance.Options.UseForeColor = true;
            this.cmdPrint.AutoSize = true;
            this.cmdPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdPrint.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdPrint.ImageOptions.Image = global::SmartMES.Properties.Resources.printer;
            this.cmdPrint.ImageOptions.ImageIndex = 4;
            this.cmdPrint.ImageOptions.ImageToTextIndent = 15;
            this.cmdPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdPrint.Location = new System.Drawing.Point(1090, 0);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdPrint.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdPrint.Size = new System.Drawing.Size(108, 40);
            this.cmdPrint.smtButtonAutoText = false;
            this.cmdPrint.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdPrint.TabIndex = 6;
            this.cmdPrint.Text = "거래명세서";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
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
            this.cmdClose.Location = new System.Drawing.Point(1198, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1275, 765);
            this.layoutControl1.TabIndex = 5;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcMaster);
            this.layoutControl2.Controls.Add(this.gcDetail);
            this.layoutControl2.Location = new System.Drawing.Point(3, 3);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(3190, 555, 650, 400);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1269, 759);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcMaster
            // 
            this.gcMaster.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcMaster.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.gcMaster.Location = new System.Drawing.Point(7, 34);
            this.gcMaster.MainView = this.gvMaster;
            this.gcMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcMaster.Name = "gcMaster";
            this.gcMaster.Size = new System.Drawing.Size(1255, 425);
            this.gcMaster.TabIndex = 4;
            this.gcMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMaster});
            // 
            // gvMaster
            // 
            this.gvMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvMaster.ColumnPanelRowHeight = 29;
            this.gvMaster.DetailHeight = 425;
            this.gvMaster.GridControl = this.gcMaster;
            this.gvMaster.Name = "gvMaster";
            this.gvMaster.OptionsBehavior.Editable = false;
            this.gvMaster.OptionsDetail.EnableMasterViewMode = false;
            this.gvMaster.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.gvMaster.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvMaster.OptionsView.ColumnAutoWidth = false;
            this.gvMaster.OptionsView.ShowAutoFilterRow = true;
            this.gvMaster.OptionsView.ShowFooter = true;
            this.gvMaster.OptionsView.ShowGroupPanel = false;
            this.gvMaster.RowHeight = 29;
            this.gvMaster.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gvMaster_FocusedRowObjectChanged);
            // 
            // gcDetail
            // 
            this.gcDetail.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcDetail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.gcDetail.Location = new System.Drawing.Point(7, 516);
            this.gcDetail.MainView = this.gvDetail;
            this.gcDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcDetail.Name = "gcDetail";
            this.gcDetail.Size = new System.Drawing.Size(1255, 236);
            this.gcDetail.TabIndex = 50;
            this.gcDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetail});
            // 
            // gvDetail
            // 
            this.gvDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvDetail.ColumnPanelRowHeight = 29;
            this.gvDetail.DetailHeight = 425;
            this.gvDetail.GridControl = this.gcDetail;
            this.gvDetail.Name = "gvDetail";
            this.gvDetail.OptionsBehavior.Editable = false;
            this.gvDetail.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            this.gvDetail.OptionsView.ColumnAutoWidth = false;
            this.gvDetail.OptionsView.ShowGroupPanel = false;
            this.gvDetail.RowHeight = 29;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.splitterItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1269, 759);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "주문 등록 마스터";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
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
            this.layoutControlGroup2.Size = new System.Drawing.Size(1269, 466);
            this.layoutControlGroup2.Text = "수주 마스터";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcMaster;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem1.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlItem1.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem1.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlItem1.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem1.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlItem1.Size = new System.Drawing.Size(1261, 431);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "주문등록 세부내용";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 482);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1269, 277);
            this.layoutControlGroup3.Text = "수주 세부내용";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcDetail;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem2.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlItem2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlItem2.Size = new System.Drawing.Size(1261, 242);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 466);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1269, 16);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1275, 765);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.layoutControl2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1275, 765);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmTradingStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 805);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTradingStatement";
            this.Text = "거래명세서 출력";
            this.Load += new System.EventHandler(this.frmTradingStatement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdPrint;
        private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchButton cmdSearch;
        private baseSMT.smtSearchLookUp ssCustomer;
        private baseSMT.smtSearchDatetime barEdit;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraGrid.GridControl gcMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMaster;
        private DevExpress.XtraGrid.GridControl gcDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetail;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}