namespace SmartMES.View.PM
{
    partial class Frm_PM_OrderProcess_MC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PM_OrderProcess_MC));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.BtnSearch = new baseSMT.smtSearchButton();
            this.BtnClose = new baseSMT.smtSearchButton();
            this.DtOrder = new baseSMT.smtSearchDatetime();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmdInsert = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDel = new baseSMT.smtSearchButton();
            this.GcPM_ProductPlan = new DevExpress.XtraGrid.GridControl();
            this.GvPM_ProductPlan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GcList = new DevExpress.XtraGrid.GridControl();
            this.GvList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GcMCList = new DevExpress.XtraGrid.GridControl();
            this.GvMCList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Rdo_ItemType = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcPM_ProductPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPM_ProductPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcMCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvMCList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rdo_ItemType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.BtnSearch);
            this.smtCommonBar1.Controls.Add(this.BtnClose);
            this.smtCommonBar1.Controls.Add(this.DtOrder);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1211, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "작업지시등록";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 2;
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
            this.BtnSearch.Location = new System.Drawing.Point(486, 0);
            this.BtnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.BtnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnSearch.Size = new System.Drawing.Size(81, 40);
            this.BtnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.BtnSearch.TabIndex = 6;
            this.BtnSearch.Text = "조회";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
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
            this.BtnClose.Location = new System.Drawing.Point(1130, 0);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.BtnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnClose.Size = new System.Drawing.Size(81, 40);
            this.BtnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.BtnClose.TabIndex = 2;
            this.BtnClose.Text = "닫기";
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // DtOrder
            // 
            this.DtOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.DtOrder.Location = new System.Drawing.Point(116, 0);
            this.DtOrder.LookAndFeel.SkinName = "WXI";
            this.DtOrder.LookAndFeel.UseDefaultLookAndFeel = false;
            this.DtOrder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DtOrder.Name = "DtOrder";
            this.DtOrder.Size = new System.Drawing.Size(370, 40);
            this.DtOrder.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DtOrder.smtComboTitleItems = new string[] {
        "지시일자",
        "완료요청일"};
            this.DtOrder.smtComboTitleValue = "지시일자";
            this.DtOrder.smtComboTitleVisible = true;
            this.DtOrder.smtComboTitleWidth = 125;
            this.DtOrder.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.DtOrder.smtEndDateTimeValue = new System.DateTime(2025, 2, 7, 17, 22, 11, 735);
            this.DtOrder.smtEndDateTimeVisible = true;
            this.DtOrder.smtLabelForeColor = System.Drawing.Color.White;
            this.DtOrder.smtLabelTitleValue = "지시일자";
            this.DtOrder.smtLabelTitleVisible = false;
            this.DtOrder.smtLabelTitleWidth = 125;
            this.DtOrder.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.DtOrder.smtStartDateTimeValue = new System.DateTime(2025, 2, 7, 17, 22, 11, 735);
            this.DtOrder.smtStartDateTimeVisible = true;
            this.DtOrder.TabIndex = 1;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.panel2);
            this.layoutControl1.Controls.Add(this.GcPM_ProductPlan);
            this.layoutControl1.Controls.Add(this.GcList);
            this.layoutControl1.Controls.Add(this.GcMCList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2456, 142, 979, 538);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1211, 589);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.panel2.Controls.Add(this.cmdInsert);
            this.panel2.Controls.Add(this.cmdSave);
            this.panel2.Controls.Add(this.cmdDel);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(627, 337);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(579, 30);
            this.panel2.TabIndex = 32;
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
            this.cmdInsert.Location = new System.Drawing.Point(348, 0);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdInsert.Size = new System.Drawing.Size(77, 30);
            this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdInsert.TabIndex = 32;
            this.cmdInsert.Tag = "Ins";
            this.cmdInsert.Text = "등록";
            this.cmdInsert.Click += new System.EventHandler(this.cmdRowEdit_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.AllowFocus = false;
            this.cmdSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdSave.Appearance.Options.UseBackColor = true;
            this.cmdSave.Appearance.Options.UseForeColor = true;
            this.cmdSave.AutoSize = true;
            this.cmdSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdSave.ImageOptions.ImageIndex = 4;
            this.cmdSave.ImageOptions.ImageToTextIndent = 15;
            this.cmdSave.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdSave.Location = new System.Drawing.Point(425, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 30);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 31;
            this.cmdSave.Tag = "Ins";
            this.cmdSave.Text = "저장";
            this.cmdSave.Click += new System.EventHandler(this.Btn_Click);
            // 
            // cmdDel
            // 
            this.cmdDel.AllowFocus = false;
            this.cmdDel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdDel.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdDel.Appearance.Options.UseBackColor = true;
            this.cmdDel.Appearance.Options.UseForeColor = true;
            this.cmdDel.AutoSize = true;
            this.cmdDel.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdDel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdDel.ImageOptions.ImageIndex = 3;
            this.cmdDel.ImageOptions.ImageToTextIndent = 15;
            this.cmdDel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdDel.Location = new System.Drawing.Point(502, 0);
            this.cmdDel.Name = "cmdDel";
            this.cmdDel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDel.Size = new System.Drawing.Size(77, 30);
            this.cmdDel.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDel.TabIndex = 30;
            this.cmdDel.Tag = "Del";
            this.cmdDel.Text = "삭제";
            this.cmdDel.Click += new System.EventHandler(this.cmdRowEdit_Click);
            // 
            // GcPM_ProductPlan
            // 
            this.GcPM_ProductPlan.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GcPM_ProductPlan.Location = new System.Drawing.Point(5, 72);
            this.GcPM_ProductPlan.MainView = this.GvPM_ProductPlan;
            this.GcPM_ProductPlan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GcPM_ProductPlan.Name = "GcPM_ProductPlan";
            this.GcPM_ProductPlan.Size = new System.Drawing.Size(1201, 207);
            this.GcPM_ProductPlan.TabIndex = 6;
            this.GcPM_ProductPlan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvPM_ProductPlan});
            // 
            // GvPM_ProductPlan
            // 
            this.GvPM_ProductPlan.ColumnPanelRowHeight = 25;
            this.GvPM_ProductPlan.GridControl = this.GcPM_ProductPlan;
            this.GvPM_ProductPlan.Name = "GvPM_ProductPlan";
            this.GvPM_ProductPlan.OptionsView.ShowGroupPanel = false;
            this.GvPM_ProductPlan.RowHeight = 25;
            // 
            // GcList
            // 
            this.GcList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GcList.Location = new System.Drawing.Point(5, 337);
            this.GcList.MainView = this.GvList;
            this.GcList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GcList.Name = "GcList";
            this.GcList.Size = new System.Drawing.Size(596, 245);
            this.GcList.TabIndex = 20;
            this.GcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvList});
            // 
            // GvList
            // 
            this.GvList.GridControl = this.GcList;
            this.GvList.Name = "GvList";
            this.GvList.OptionsView.ShowGroupPanel = false;
            // 
            // GcMCList
            // 
            this.GcMCList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GcMCList.Location = new System.Drawing.Point(627, 373);
            this.GcMCList.MainView = this.GvMCList;
            this.GcMCList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GcMCList.Name = "GcMCList";
            this.GcMCList.Size = new System.Drawing.Size(579, 209);
            this.GcMCList.TabIndex = 20;
            this.GcMCList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvMCList});
            // 
            // GvMCList
            // 
            this.GvMCList.GridControl = this.GcMCList;
            this.GvMCList.Name = "GvMCList";
            this.GvMCList.OptionsView.ShowGroupPanel = false;
            this.GvMCList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GvMCList_CellValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup5,
            this.layoutControlGroup2,
            this.splitterItem1,
            this.splitterItem2,
            this.layoutControlItem5,
            this.layoutControlGroup3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1211, 589);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "작업지시 리스트";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.simpleSeparator1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 38);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1211, 249);
            this.layoutControlGroup3.Text = "생산계획 리스트";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.GcPM_ProductPlan;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1205, 213);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 213);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(1205, 1);
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "공정 리스트";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 303);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(606, 286);
            this.layoutControlGroup5.Text = "공정 리스트";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.GcList;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(600, 251);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "공정지시 리스트";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(622, 303);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(589, 286);
            this.layoutControlGroup2.Text = "설비 리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GcMCList;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 36);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(583, 215);
            this.layoutControlItem1.Text = "layoutControlItem7";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panel2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(583, 36);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 287);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1211, 16);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(606, 303);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(16, 286);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.Rdo_ItemType);
            this.panelControl1.Location = new System.Drawing.Point(2, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1207, 32);
            this.panelControl1.TabIndex = 34;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.panelControl1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 38);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(5, 38);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(1211, 38);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // Rdo_ItemType
            // 
            this.Rdo_ItemType.Dock = System.Windows.Forms.DockStyle.Left;
            this.Rdo_ItemType.Location = new System.Drawing.Point(2, 2);
            this.Rdo_ItemType.Name = "Rdo_ItemType";
            this.Rdo_ItemType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Rdo_ItemType.Properties.Appearance.Options.UseBackColor = true;
            this.Rdo_ItemType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Rdo_ItemType.Size = new System.Drawing.Size(184, 28);
            this.Rdo_ItemType.TabIndex = 0;
            // 
            // Frm_PM_OrderProcess_MC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 629);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_PM_OrderProcess_MC";
            this.Text = "작업지시등록";
            this.Load += new System.EventHandler(this.frmPM_OrderProcess_MC_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcPM_ProductPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPM_ProductPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcMCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvMCList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rdo_ItemType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton BtnSearch;
        private baseSMT.smtSearchButton BtnClose;
        private baseSMT.smtSearchDatetime DtOrder;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl GcPM_ProductPlan;
        private DevExpress.XtraGrid.Views.Grid.GridView GvPM_ProductPlan;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.GridControl GcList;
        private DevExpress.XtraGrid.Views.Grid.GridView GvList;
        private DevExpress.XtraGrid.GridControl GcMCList;
        private DevExpress.XtraGrid.Views.Grid.GridView GvMCList;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private baseSMT.smtSearchButton cmdSave;
        private baseSMT.smtSearchButton cmdDel;
        private baseSMT.smtSearchButton cmdInsert;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.RadioGroup Rdo_ItemType;
    }
}