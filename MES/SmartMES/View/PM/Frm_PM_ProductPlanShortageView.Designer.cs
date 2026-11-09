
namespace SmartMES.View.PM
{
    partial class Frm_PM_ProductPlanShortageView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PM_ProductPlanShortageView));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LupMaterial = new baseSMT.smtSearchLookUp();
            this.DtResult = new baseSMT.smtSearchDatetime();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.GcPM_ProductPlan = new DevExpress.XtraGrid.GridControl();
            this.GvPM_ProductPlan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.GcList = new DevExpress.XtraGrid.GridControl();
            this.GvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcPM_ProductPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPM_ProductPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.panel1);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1788, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "생산계획대비 재고 과부족현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 274;
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
            this.cmdDisplay.Location = new System.Drawing.Point(1057, 0);
            this.cmdDisplay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(92, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 5;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Click += new System.EventHandler(this.Btn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LupMaterial);
            this.panel1.Controls.Add(this.DtResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(255, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 40);
            this.panel1.TabIndex = 28;
            // 
            // LupMaterial
            // 
            this.LupMaterial.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupMaterial.Location = new System.Drawing.Point(345, 0);
            this.LupMaterial.LookAndFeel.SkinName = "WXI";
            this.LupMaterial.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupMaterial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LupMaterial.Name = "LupMaterial";
            this.LupMaterial.Size = new System.Drawing.Size(360, 40);
            this.LupMaterial.smtLabelTitleValue = "품목";
            this.LupMaterial.smtLabelTitleVisible = true;
            this.LupMaterial.smtLabelTitleWidth = 100;
            this.LupMaterial.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupMaterial.smtLookUpDataDisplayMember = "코드명";
            this.LupMaterial.smtLookUpDataSource = null;
            this.LupMaterial.smtLookUpDataValueMember = "코드";
            this.LupMaterial.smtLookUpSelectdIndex = -1;
            this.LupMaterial.smtLookUpText = "";
            this.LupMaterial.smtLookUpValue = "";
            this.LupMaterial.TabIndex = 25;
            // 
            // DtResult
            // 
            this.DtResult.Dock = System.Windows.Forms.DockStyle.Left;
            this.DtResult.Location = new System.Drawing.Point(0, 0);
            this.DtResult.LookAndFeel.SkinName = "WXI";
            this.DtResult.LookAndFeel.UseDefaultLookAndFeel = false;
            this.DtResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DtResult.Name = "DtResult";
            this.DtResult.Size = new System.Drawing.Size(345, 40);
            this.DtResult.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DtResult.smtComboTitleItems = new string[0];
            this.DtResult.smtComboTitleValue = "";
            this.DtResult.smtComboTitleVisible = false;
            this.DtResult.smtComboTitleWidth = 100;
            this.DtResult.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.DtResult.smtEndDateTimeValue = new System.DateTime(2025, 2, 25, 15, 50, 58, 413);
            this.DtResult.smtEndDateTimeVisible = true;
            this.DtResult.smtLabelTitleValue = "조회기간";
            this.DtResult.smtLabelTitleVisible = true;
            this.DtResult.smtLabelTitleWidth = 100;
            this.DtResult.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.DtResult.smtStartDateTimeValue = new System.DateTime(2025, 2, 25, 15, 50, 58, 413);
            this.DtResult.smtStartDateTimeVisible = true;
            this.DtResult.TabIndex = 24;
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
            this.cmdExcel.Location = new System.Drawing.Point(1574, 0);
            this.cmdExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(122, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 6;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.Btn_Click);
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
            this.cmdClose.Location = new System.Drawing.Point(1696, 0);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(92, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.Btn_Click);
            // 
            // GcPM_ProductPlan
            // 
            this.GcPM_ProductPlan.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.GcPM_ProductPlan.Location = new System.Drawing.Point(9, 43);
            this.GcPM_ProductPlan.MainView = this.GvPM_ProductPlan;
            this.GcPM_ProductPlan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GcPM_ProductPlan.Name = "GcPM_ProductPlan";
            this.GcPM_ProductPlan.Size = new System.Drawing.Size(1770, 268);
            this.GcPM_ProductPlan.TabIndex = 275;
            this.GcPM_ProductPlan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvPM_ProductPlan});
            // 
            // GvPM_ProductPlan
            // 
            this.GvPM_ProductPlan.DetailHeight = 467;
            this.GvPM_ProductPlan.GridControl = this.GcPM_ProductPlan;
            this.GvPM_ProductPlan.Name = "GvPM_ProductPlan";
            this.GvPM_ProductPlan.OptionsBehavior.Editable = false;
            this.GvPM_ProductPlan.OptionsEditForm.PopupEditFormWidth = 1142;
            this.GvPM_ProductPlan.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.GcList);
            this.layoutControl1.Controls.Add(this.GcPM_ProductPlan);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1788, 673);
            this.layoutControl1.TabIndex = 276;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // GcList
            // 
            this.GcList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.GcList.Location = new System.Drawing.Point(9, 383);
            this.GcList.MainView = this.GvList;
            this.GcList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GcList.Name = "GcList";
            this.GcList.Size = new System.Drawing.Size(1770, 281);
            this.GcList.TabIndex = 276;
            this.GcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvList});
            // 
            // GvList
            // 
            this.GvList.DetailHeight = 467;
            this.GvList.GridControl = this.GcList;
            this.GvList.Name = "GvList";
            this.GvList.OptionsBehavior.Editable = false;
            this.GvList.OptionsEditForm.PopupEditFormWidth = 1142;
            this.GvList.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup1,
            this.splitterItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1788, 673);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 340);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1788, 333);
            this.layoutControlGroup2.Text = "재고과부족";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.GcList;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1778, 289);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1788, 320);
            this.layoutControlGroup1.Text = "생산계획리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GcPM_ProductPlan;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1778, 276);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 320);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1788, 20);
            // 
            // frmPM_ProductPlanShortageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1788, 713);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmPM_ProductPlanShortageView";
            this.Text = "생산계획대비 재고 과부족현황";
            this.Load += new System.EventHandler(this.frmPM_ProductPlanShortageView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcPM_ProductPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPM_ProductPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdDisplay;
        private System.Windows.Forms.Panel panel1;
        private baseSMT.smtSearchLookUp LupMaterial;
        private baseSMT.smtSearchDatetime DtResult;
        private baseSMT.smtSearchButton cmdExcel;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraGrid.GridControl GcPM_ProductPlan;
        private DevExpress.XtraGrid.Views.Grid.GridView GvPM_ProductPlan;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl GcList;
        private DevExpress.XtraGrid.Views.Grid.GridView GvList;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}