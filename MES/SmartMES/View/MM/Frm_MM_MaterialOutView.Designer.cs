namespace SmartMES.View.MM
{
    partial class Frm_MM_MaterialOutView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_MaterialOutView));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Material = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.smtSearchButton5 = new baseSMT.smtSearchButton();
            this.smtSearchButton6 = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ChkMonth = new DevExpress.XtraEditors.CheckEdit();
            this.ChkDay = new DevExpress.XtraEditors.CheckEdit();
            this.GcMaterialOut = new DevExpress.XtraGrid.GridControl();
            this.GvMaterialOut = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcMaterialOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvMaterialOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Material);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton5);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton6);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1215, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "자재출고";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 10;
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
            this.btn_Search.Location = new System.Drawing.Point(725, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 15;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // Lup_Material
            // 
            this.Lup_Material.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Material.Location = new System.Drawing.Point(437, 0);
            this.Lup_Material.LookAndFeel.SkinName = "WXI";
            this.Lup_Material.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Material.Name = "Lup_Material";
            this.Lup_Material.Size = new System.Drawing.Size(288, 40);
            this.Lup_Material.smtLabelTitleValue = "품목";
            this.Lup_Material.smtLabelTitleVisible = true;
            this.Lup_Material.smtLabelTitleWidth = 100;
            this.Lup_Material.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Material.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Material.smtLookUpDataSource = null;
            this.Lup_Material.smtLookUpDataValueMember = "코드";
            this.Lup_Material.smtLookUpSelectdIndex = -1;
            this.Lup_Material.smtLookUpText = "";
            this.Lup_Material.smtLookUpValue = "";
            this.Lup_Material.TabIndex = 25;
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
            this.Dt_Date.smtLabelTitleValue = "출고일자";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 10;
            // 
            // smtSearchButton5
            // 
            this.smtSearchButton5.AllowFocus = false;
            this.smtSearchButton5.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton5.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton5.Appearance.Options.UseBackColor = true;
            this.smtSearchButton5.Appearance.Options.UseForeColor = true;
            this.smtSearchButton5.AutoSize = true;
            this.smtSearchButton5.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton5.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton5.ImageOptions.ImageIndex = 9;
            this.smtSearchButton5.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton5.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton5.Location = new System.Drawing.Point(1037, 0);
            this.smtSearchButton5.Name = "smtSearchButton5";
            this.smtSearchButton5.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton5.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton5.Size = new System.Drawing.Size(101, 40);
            this.smtSearchButton5.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.smtSearchButton5.TabIndex = 6;
            this.smtSearchButton5.Text = "엑셀변환";
            this.smtSearchButton5.Visible = false;
            // 
            // smtSearchButton6
            // 
            this.smtSearchButton6.AllowFocus = false;
            this.smtSearchButton6.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton6.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton6.Appearance.Options.UseBackColor = true;
            this.smtSearchButton6.Appearance.Options.UseForeColor = true;
            this.smtSearchButton6.AutoSize = true;
            this.smtSearchButton6.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton6.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton6.ImageOptions.ImageIndex = 5;
            this.smtSearchButton6.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton6.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton6.Location = new System.Drawing.Point(1138, 0);
            this.smtSearchButton6.Name = "smtSearchButton6";
            this.smtSearchButton6.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton6.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton6.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton6.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.smtSearchButton6.TabIndex = 1;
            this.smtSearchButton6.Text = "닫기";
            this.smtSearchButton6.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1215, 524);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.panelControl1);
            this.layoutControl2.Controls.Add(this.GcMaterialOut);
            this.layoutControl2.Location = new System.Drawing.Point(3, 3);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1748, 297, 812, 500);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1209, 518);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ChkMonth);
            this.panelControl1.Controls.Add(this.ChkDay);
            this.panelControl1.Location = new System.Drawing.Point(7, 7);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1195, 25);
            this.panelControl1.TabIndex = 53;
            // 
            // ChkMonth
            // 
            this.ChkMonth.Location = new System.Drawing.Point(85, 2);
            this.ChkMonth.Name = "ChkMonth";
            this.ChkMonth.Properties.Caption = "월별";
            this.ChkMonth.Size = new System.Drawing.Size(69, 22);
            this.ChkMonth.TabIndex = 1;
            this.ChkMonth.Tag = "Month";
            // 
            // ChkDay
            // 
            this.ChkDay.Location = new System.Drawing.Point(10, 2);
            this.ChkDay.Name = "ChkDay";
            this.ChkDay.Properties.Caption = "일자별";
            this.ChkDay.Size = new System.Drawing.Size(69, 22);
            this.ChkDay.TabIndex = 0;
            this.ChkDay.Tag = "Day";
            // 
            // GcMaterialOut
            // 
            this.GcMaterialOut.Cursor = System.Windows.Forms.Cursors.Default;
            this.GcMaterialOut.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.GcMaterialOut.Location = new System.Drawing.Point(7, 38);
            this.GcMaterialOut.MainView = this.GvMaterialOut;
            this.GcMaterialOut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GcMaterialOut.Name = "GcMaterialOut";
            this.GcMaterialOut.Size = new System.Drawing.Size(1195, 473);
            this.GcMaterialOut.TabIndex = 52;
            this.GcMaterialOut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvMaterialOut});
            // 
            // GvMaterialOut
            // 
            this.GvMaterialOut.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GvMaterialOut.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GvMaterialOut.ColumnPanelRowHeight = 29;
            this.GvMaterialOut.DetailHeight = 425;
            this.GvMaterialOut.GridControl = this.GcMaterialOut;
            this.GvMaterialOut.Name = "GvMaterialOut";
            this.GvMaterialOut.OptionsBehavior.Editable = false;
            this.GvMaterialOut.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            this.GvMaterialOut.OptionsView.ColumnAutoWidth = false;
            this.GvMaterialOut.OptionsView.ShowGroupPanel = false;
            this.GvMaterialOut.RowHeight = 29;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1209, 518);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceItemCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlGroup3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceTabPage.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceTabPage.HeaderActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = true;
            this.layoutControlGroup3.AppearanceTabPage.PageClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = true;
            this.layoutControlGroup3.CustomizationFormText = "발주등록 세부내용";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1209, 518);
            this.layoutControlGroup3.Text = "출고 품목";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.AppearanceItemCaptionDisabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlItem2.AppearanceItemCaptionDisabled.Options.UseFont = true;
            this.layoutControlItem2.Control = this.GcMaterialOut;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlItem2.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlItem2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.layoutControlItem2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlItem2.Size = new System.Drawing.Size(1201, 479);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 31);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(7, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1201, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1215, 524);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.layoutControl2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1215, 524);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // frmMaterialOutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 564);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMaterialOutView";
            this.Text = "자재출고";
            this.Load += new System.EventHandler(this.frmMaterialOutView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChkMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcMaterialOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvMaterialOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton smtSearchButton5;
        private baseSMT.smtSearchButton smtSearchButton6;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl GcMaterialOut;
        private DevExpress.XtraGrid.Views.Grid.GridView GvMaterialOut;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private baseSMT.smtSearchLookUp Lup_Material;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit ChkMonth;
        private DevExpress.XtraEditors.CheckEdit ChkDay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}