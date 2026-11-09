namespace SmartPOP.Views.PM
{
    partial class Frm_PM_OrderResult_Process_Print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PM_OrderResult_Process_Print));
            this.button4 = new System.Windows.Forms.Button();
            this.dragDropEvents2 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.dragDropEvents1 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.BtnPrint = new baseSMT.smtSearchButton();
            this.BtnClose = new baseSMT.smtSearchButton();
            this.BtnSearch = new baseSMT.smtSearchButton();
            this.CboState = new baseSMT.smtSearchComboBox();
            this.DtOrder = new baseSMT.smtSearchDatetime();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.GcSerialNo = new DevExpress.XtraGrid.GridControl();
            this.GvSerialNo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GcPlan = new DevExpress.XtraGrid.GridControl();
            this.GvPlan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcSerialNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvSerialNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Right;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("돋움", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = global::SmartPOP.Properties.Resources.close;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(718, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(70, 187);
            this.button4.TabIndex = 16;
            this.button4.Text = "     닫기";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.BtnPrint);
            this.smtCommonBar1.Controls.Add(this.BtnClose);
            this.smtCommonBar1.Controls.Add(this.BtnSearch);
            this.smtCommonBar1.Controls.Add(this.CboState);
            this.smtCommonBar1.Controls.Add(this.DtOrder);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1729, 45);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "공정이동표재발행";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 0;
            // 
            // BtnPrint
            // 
            this.BtnPrint.AllowFocus = false;
            this.BtnPrint.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.BtnPrint.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Appearance.Options.UseBackColor = true;
            this.BtnPrint.Appearance.Options.UseForeColor = true;
            this.BtnPrint.AutoSize = true;
            this.BtnPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnPrint.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnPrint.ImageOptions.ImageIndex = 10;
            this.BtnPrint.ImageOptions.ImageToTextIndent = 15;
            this.BtnPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.BtnPrint.Location = new System.Drawing.Point(1537, 0);
            this.BtnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Padding = new System.Windows.Forms.Padding(13, 0, 13, 0);
            this.BtnPrint.Size = new System.Drawing.Size(96, 45);
            this.BtnPrint.smtButtonAutoText = false;
            this.BtnPrint.smtButtonStyle = baseSMT.EnumButtonStyles.프린터;
            this.BtnPrint.TabIndex = 5;
            this.BtnPrint.Text = "인쇄";
            this.BtnPrint.Click += new System.EventHandler(this.Btn_Click);
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
            this.BtnClose.Location = new System.Drawing.Point(1633, 0);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Padding = new System.Windows.Forms.Padding(13, 0, 13, 0);
            this.BtnClose.Size = new System.Drawing.Size(96, 45);
            this.BtnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "닫기";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
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
            this.BtnSearch.Location = new System.Drawing.Point(768, 0);
            this.BtnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Padding = new System.Windows.Forms.Padding(13, 0, 13, 0);
            this.BtnSearch.Size = new System.Drawing.Size(96, 45);
            this.BtnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.BtnSearch.TabIndex = 3;
            this.BtnSearch.Text = "조회";
            this.BtnSearch.Click += new System.EventHandler(this.Btn_Click);
            // 
            // CboState
            // 
            this.CboState.Dock = System.Windows.Forms.DockStyle.Left;
            this.CboState.Location = new System.Drawing.Point(533, 0);
            this.CboState.LookAndFeel.SkinName = "WXI";
            this.CboState.LookAndFeel.UseDefaultLookAndFeel = false;
            this.CboState.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CboState.Name = "CboState";
            this.CboState.Size = new System.Drawing.Size(235, 45);
            this.CboState.smtComboBoxAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.CboState.smtComboBoxItems = new string[] {
        "전체",
        "대기",
        "진행중",
        "완료"};
            this.CboState.smtComboBoxValue = "전체";
            this.CboState.smtLabelTitleValue = "상태";
            this.CboState.smtLabelTitleVisible = true;
            this.CboState.smtLabelTitleWidth = 60;
            this.CboState.TabIndex = 2;
            // 
            // DtOrder
            // 
            this.DtOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.DtOrder.Location = new System.Drawing.Point(188, 0);
            this.DtOrder.LookAndFeel.SkinName = "WXI";
            this.DtOrder.LookAndFeel.UseDefaultLookAndFeel = false;
            this.DtOrder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DtOrder.Name = "DtOrder";
            this.DtOrder.PopMode = false;
            this.DtOrder.Size = new System.Drawing.Size(345, 45);
            this.DtOrder.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DtOrder.smtComboTitleItems = new string[0];
            this.DtOrder.smtComboTitleValue = "";
            this.DtOrder.smtComboTitleVisible = false;
            this.DtOrder.smtComboTitleWidth = 100;
            this.DtOrder.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.DtOrder.smtEndDateTimeValue = new System.DateTime(2025, 2, 20, 12, 53, 23, 798);
            this.DtOrder.smtEndDateTimeVisible = true;
            this.DtOrder.smtLabelTitleValue = "지시일자";
            this.DtOrder.smtLabelTitleVisible = true;
            this.DtOrder.smtLabelTitleWidth = 80;
            this.DtOrder.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.DtOrder.smtStartDateTimeValue = new System.DateTime(2025, 2, 20, 12, 53, 23, 797);
            this.DtOrder.smtStartDateTimeVisible = true;
            this.DtOrder.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.GcSerialNo);
            this.layoutControl1.Controls.Add(this.GcPlan);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1729, 830);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // GcSerialNo
            // 
            this.GcSerialNo.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.GcSerialNo.Location = new System.Drawing.Point(24, 480);
            this.GcSerialNo.MainView = this.GvSerialNo;
            this.GcSerialNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GcSerialNo.Name = "GcSerialNo";
            this.GcSerialNo.Size = new System.Drawing.Size(1681, 326);
            this.GcSerialNo.TabIndex = 5;
            this.GcSerialNo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvSerialNo});
            // 
            // GvSerialNo
            // 
            this.GvSerialNo.DetailHeight = 474;
            this.GvSerialNo.GridControl = this.GcSerialNo;
            this.GvSerialNo.Name = "GvSerialNo";
            this.GvSerialNo.OptionsEditForm.PopupEditFormWidth = 978;
            this.GvSerialNo.OptionsView.ShowGroupPanel = false;
            // 
            // GcPlan
            // 
            this.GcPlan.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.GcPlan.Location = new System.Drawing.Point(24, 58);
            this.GcPlan.MainView = this.GvPlan;
            this.GcPlan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GcPlan.Name = "GcPlan";
            this.GcPlan.Size = new System.Drawing.Size(1681, 320);
            this.GcPlan.TabIndex = 4;
            this.GcPlan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvPlan});
            // 
            // GvPlan
            // 
            this.GvPlan.DetailHeight = 474;
            this.GvPlan.GridControl = this.GcPlan;
            this.GvPlan.Name = "GvPlan";
            this.GvPlan.OptionsEditForm.PopupEditFormWidth = 978;
            this.GvPlan.OptionsView.ColumnAutoWidth = false;
            this.GvPlan.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.splitterItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1729, 830);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1729, 402);
            this.layoutControlGroup1.Text = "작업지시 리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GcPlan;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1689, 328);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 422);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1729, 408);
            this.layoutControlGroup2.Text = "실적 리스트";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.GcSerialNo;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1689, 334);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 402);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1729, 20);
            // 
            // frmProcMouvementReissued
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1729, 875);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmProcMouvementReissued";
            this.Text = "공정이동표재발행";
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcSerialNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvSerialNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button4;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents1;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents2;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchComboBox CboState;
        private baseSMT.smtSearchDatetime DtOrder;
        private baseSMT.smtSearchButton BtnSearch;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl GcPlan;
        private DevExpress.XtraGrid.Views.Grid.GridView GvPlan;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private baseSMT.smtSearchButton BtnPrint;
        private baseSMT.smtSearchButton BtnClose;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraGrid.GridControl GcSerialNo;
        private DevExpress.XtraGrid.Views.Grid.GridView GvSerialNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}