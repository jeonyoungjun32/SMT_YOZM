namespace SmartMES.View.BM
{
    partial class Frm_BM_Estimate
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BM_Estimate));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
			this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
			this.Gc_EstimateMater = new DevExpress.XtraGrid.GridControl();
			this.Gv_EstimateMater = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
			this.Gc_BM_EstimateD = new DevExpress.XtraGrid.GridControl();
			this.Gv_BM_EstimateD = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdOrder = new baseSMT.smtSearchButton();
			this.cmdInsert = new baseSMT.smtSearchButton();
			this.cmdUpdate = new baseSMT.smtSearchButton();
			this.cmdDelete = new baseSMT.smtSearchButton();
			this.cmdExcel = new baseSMT.smtSearchButton();
			this.cmdInspectionRpt = new baseSMT.smtSearchButton();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.LupCustomer = new baseSMT.smtSearchLookUp();
			this.Dt_Date = new baseSMT.smtSearchDatetime();
			this.cmdClose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
			this.splitContainerControl1.Panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
			this.splitContainerControl1.Panel2.SuspendLayout();
			this.splitContainerControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
			this.layoutControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_EstimateMater)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_EstimateMater)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
			this.layoutControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_BM_EstimateD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_BM_EstimateD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.splitContainerControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(601, 439, 437, 485);
			this.layoutControl1.Root = this.layoutControlGroup1;
			this.layoutControl1.Size = new System.Drawing.Size(1598, 605);
			this.layoutControl1.TabIndex = 2;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// splitContainerControl1
			// 
			this.splitContainerControl1.Horizontal = false;
			this.splitContainerControl1.Location = new System.Drawing.Point(3, 3);
			this.splitContainerControl1.Name = "splitContainerControl1";
			// 
			// splitContainerControl1.Panel1
			// 
			this.splitContainerControl1.Panel1.Controls.Add(this.layoutControl2);
			this.splitContainerControl1.Panel1.Text = "Panel1";
			// 
			// splitContainerControl1.Panel2
			// 
			this.splitContainerControl1.Panel2.Controls.Add(this.layoutControl3);
			this.splitContainerControl1.Panel2.Text = "Panel2";
			this.splitContainerControl1.Size = new System.Drawing.Size(1592, 599);
			this.splitContainerControl1.SplitterPosition = 346;
			this.splitContainerControl1.TabIndex = 4;
			// 
			// layoutControl2
			// 
			this.layoutControl2.Controls.Add(this.Gc_EstimateMater);
			this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl2.Location = new System.Drawing.Point(0, 0);
			this.layoutControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.layoutControl2.Name = "layoutControl2";
			this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(601, 439, 437, 485);
			this.layoutControl2.Root = this.layoutControlGroup2;
			this.layoutControl2.Size = new System.Drawing.Size(1592, 346);
			this.layoutControl2.TabIndex = 3;
			this.layoutControl2.Text = "layoutControl2";
			// 
			// Gc_EstimateMater
			// 
			this.Gc_EstimateMater.Location = new System.Drawing.Point(7, 34);
			this.Gc_EstimateMater.MainView = this.Gv_EstimateMater;
			this.Gc_EstimateMater.Name = "Gc_EstimateMater";
			this.Gc_EstimateMater.Size = new System.Drawing.Size(1578, 305);
			this.Gc_EstimateMater.TabIndex = 169;
			this.Gc_EstimateMater.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_EstimateMater});
			// 
			// Gv_EstimateMater
			// 
			this.Gv_EstimateMater.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.Gv_EstimateMater.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.Gv_EstimateMater.DetailHeight = 397;
			this.Gv_EstimateMater.GridControl = this.Gc_EstimateMater;
			this.Gv_EstimateMater.Name = "Gv_EstimateMater";
			this.Gv_EstimateMater.OptionsView.ShowGroupPanel = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.CustomizationFormText = "Root";
			this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup2.GroupBordersVisible = false;
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup6});
			this.layoutControlGroup2.Name = "Root";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(1592, 346);
			this.layoutControlGroup2.TextVisible = false;
			// 
			// layoutControlGroup6
			// 
			this.layoutControlGroup6.CustomizationFormText = "견적리스트";
			this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
			this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup6.Name = "layoutControlGroup3";
			this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup6.Size = new System.Drawing.Size(1592, 346);
			this.layoutControlGroup6.Text = "견적리스트";
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.Gc_EstimateMater;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem1";
			this.layoutControlItem3.Size = new System.Drawing.Size(1584, 311);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControl3
			// 
			this.layoutControl3.Controls.Add(this.Gc_BM_EstimateD);
			this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl3.Location = new System.Drawing.Point(0, 0);
			this.layoutControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.layoutControl3.Name = "layoutControl3";
			this.layoutControl3.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(601, 439, 437, 485);
			this.layoutControl3.Root = this.layoutControlGroup3;
			this.layoutControl3.Size = new System.Drawing.Size(1592, 237);
			this.layoutControl3.TabIndex = 3;
			this.layoutControl3.Text = "layoutControl3";
			// 
			// Gc_BM_EstimateD
			// 
			this.Gc_BM_EstimateD.Location = new System.Drawing.Point(7, 34);
			this.Gc_BM_EstimateD.MainView = this.Gv_BM_EstimateD;
			this.Gc_BM_EstimateD.Name = "Gc_BM_EstimateD";
			this.Gc_BM_EstimateD.Size = new System.Drawing.Size(1578, 196);
			this.Gc_BM_EstimateD.TabIndex = 169;
			this.Gc_BM_EstimateD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_BM_EstimateD});
			// 
			// Gv_BM_EstimateD
			// 
			this.Gv_BM_EstimateD.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.Gv_BM_EstimateD.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.Gv_BM_EstimateD.DetailHeight = 397;
			this.Gv_BM_EstimateD.GridControl = this.Gc_BM_EstimateD;
			this.Gv_BM_EstimateD.Name = "Gv_BM_EstimateD";
			this.Gv_BM_EstimateD.OptionsView.ShowGroupPanel = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.CustomizationFormText = "Root";
			this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup3.GroupBordersVisible = false;
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup5});
			this.layoutControlGroup3.Name = "Root";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(1592, 237);
			this.layoutControlGroup3.TextVisible = false;
			// 
			// layoutControlGroup5
			// 
			this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
			this.layoutControlGroup5.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup5.Name = "layoutControlGroup3";
			this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup5.Size = new System.Drawing.Size(1592, 237);
			this.layoutControlGroup5.Text = "견적상세";
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.Gc_BM_EstimateD;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem1";
			this.layoutControlItem4.Size = new System.Drawing.Size(1584, 202);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.CustomizationFormText = "Root";
			this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.layoutControlGroup1.GroupBordersVisible = false;
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Name = "Root";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1598, 605);
			this.layoutControlGroup1.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.splitContainerControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1598, 605);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdOrder);
			this.smtCommonBar1.Controls.Add(this.cmdInsert);
			this.smtCommonBar1.Controls.Add(this.cmdUpdate);
			this.smtCommonBar1.Controls.Add(this.cmdDelete);
			this.smtCommonBar1.Controls.Add(this.cmdExcel);
			this.smtCommonBar1.Controls.Add(this.cmdInspectionRpt);
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.LupCustomer);
			this.smtCommonBar1.Controls.Add(this.Dt_Date);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1598, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "견적관리";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 11;
			// 
			// cmdOrder
			// 
			this.cmdOrder.AllowFocus = false;
			this.cmdOrder.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdOrder.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdOrder.Appearance.Options.UseBackColor = true;
			this.cmdOrder.Appearance.Options.UseForeColor = true;
			this.cmdOrder.AutoSize = true;
			this.cmdOrder.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdOrder.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdOrder.ImageOptions.Image = global::SmartMES.Properties.Resources.convert_16x16;
			this.cmdOrder.ImageOptions.ImageIndex = 1;
			this.cmdOrder.ImageOptions.ImageToTextIndent = 15;
			this.cmdOrder.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdOrder.Location = new System.Drawing.Point(1016, 0);
			this.cmdOrder.Name = "cmdOrder";
			this.cmdOrder.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdOrder.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdOrder.Size = new System.Drawing.Size(96, 40);
			this.cmdOrder.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdOrder.TabIndex = 51;
			this.cmdOrder.Tag = "Ins";
			this.cmdOrder.Text = "수주전환";
			this.cmdOrder.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdInsert.Location = new System.Drawing.Point(1112, 0);
			this.cmdInsert.Name = "cmdInsert";
			this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInsert.Size = new System.Drawing.Size(77, 40);
			this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdInsert.TabIndex = 50;
			this.cmdInsert.Tag = "Ins";
			this.cmdInsert.Text = "등록";
			this.cmdInsert.Click += new System.EventHandler(this.Btn_Click);
			// 
			// cmdUpdate
			// 
			this.cmdUpdate.AllowFocus = false;
			this.cmdUpdate.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdUpdate.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdUpdate.Appearance.Options.UseBackColor = true;
			this.cmdUpdate.Appearance.Options.UseForeColor = true;
			this.cmdUpdate.AutoSize = true;
			this.cmdUpdate.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdUpdate.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdUpdate.ImageOptions.ImageIndex = 2;
			this.cmdUpdate.ImageOptions.ImageToTextIndent = 15;
			this.cmdUpdate.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdUpdate.Location = new System.Drawing.Point(1189, 0);
			this.cmdUpdate.Name = "cmdUpdate";
			this.cmdUpdate.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdUpdate.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdUpdate.Size = new System.Drawing.Size(77, 40);
			this.cmdUpdate.smtButtonStyle = baseSMT.EnumButtonStyles.수정;
			this.cmdUpdate.TabIndex = 49;
			this.cmdUpdate.Tag = "Ins";
			this.cmdUpdate.Text = "수정";
			this.cmdUpdate.Click += new System.EventHandler(this.Btn_Click);
			// 
			// cmdDelete
			// 
			this.cmdDelete.AllowFocus = false;
			this.cmdDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDelete.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDelete.Appearance.Options.UseBackColor = true;
			this.cmdDelete.Appearance.Options.UseForeColor = true;
			this.cmdDelete.AutoSize = true;
			this.cmdDelete.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDelete.ImageOptions.ImageIndex = 3;
			this.cmdDelete.ImageOptions.ImageToTextIndent = 15;
			this.cmdDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDelete.Location = new System.Drawing.Point(1266, 0);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDelete.Size = new System.Drawing.Size(77, 40);
			this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDelete.TabIndex = 48;
			this.cmdDelete.Tag = "Del";
			this.cmdDelete.Text = "삭제";
			this.cmdDelete.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdExcel.Location = new System.Drawing.Point(1343, 0);
			this.cmdExcel.Name = "cmdExcel";
			this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdExcel.Size = new System.Drawing.Size(101, 40);
			this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
			this.cmdExcel.TabIndex = 44;
			this.cmdExcel.Text = "엑셀변환";
			this.cmdExcel.Click += new System.EventHandler(this.Btn_Click);
			// 
			// cmdInspectionRpt
			// 
			this.cmdInspectionRpt.AllowFocus = false;
			this.cmdInspectionRpt.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdInspectionRpt.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdInspectionRpt.Appearance.Options.UseBackColor = true;
			this.cmdInspectionRpt.Appearance.Options.UseForeColor = true;
			this.cmdInspectionRpt.AutoSize = true;
			this.cmdInspectionRpt.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdInspectionRpt.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdInspectionRpt.ImageOptions.ImageIndex = 10;
			this.cmdInspectionRpt.ImageOptions.ImageToTextIndent = 15;
			this.cmdInspectionRpt.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdInspectionRpt.Location = new System.Drawing.Point(1444, 0);
			this.cmdInspectionRpt.Name = "cmdInspectionRpt";
			this.cmdInspectionRpt.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInspectionRpt.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInspectionRpt.Size = new System.Drawing.Size(77, 40);
			this.cmdInspectionRpt.smtButtonStyle = baseSMT.EnumButtonStyles.프린터;
			this.cmdInspectionRpt.TabIndex = 43;
			this.cmdInspectionRpt.Text = "인쇄";
			this.cmdInspectionRpt.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdDisplay.Location = new System.Drawing.Point(725, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// LupCustomer
			// 
			this.LupCustomer.Dock = System.Windows.Forms.DockStyle.Left;
			this.LupCustomer.Location = new System.Drawing.Point(437, 0);
			this.LupCustomer.LookAndFeel.SkinName = "WXI";
			this.LupCustomer.LookAndFeel.UseDefaultLookAndFeel = false;
			this.LupCustomer.Name = "LupCustomer";
			this.LupCustomer.Size = new System.Drawing.Size(288, 40);
			this.LupCustomer.smtLabelTitleValue = "거래처";
			this.LupCustomer.smtLabelTitleVisible = true;
			this.LupCustomer.smtLabelTitleWidth = 100;
			this.LupCustomer.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.LupCustomer.smtLookUpDataDisplayMember = "";
			this.LupCustomer.smtLookUpDataSource = null;
			this.LupCustomer.smtLookUpDataValueMember = "";
			this.LupCustomer.smtLookUpSelectdIndex = -1;
			this.LupCustomer.smtLookUpText = "";
			this.LupCustomer.smtLookUpValue = "";
			this.LupCustomer.TabIndex = 17;
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
			this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 5, 8, 12, 41, 0, 172);
			this.Dt_Date.smtEndDateTimeVisible = true;
			this.Dt_Date.smtLabelTitleValue = "조회일자";
			this.Dt_Date.smtLabelTitleVisible = true;
			this.Dt_Date.smtLabelTitleWidth = 100;
			this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 5, 8, 12, 41, 0, 168);
			this.Dt_Date.smtStartDateTimeVisible = true;
			this.Dt_Date.TabIndex = 38;
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
			this.cmdClose.Location = new System.Drawing.Point(1521, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.Btn_Click);
			// 
			// frmEstimate
			// 
			this.Appearance.Options.UseFont = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1598, 645);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmEstimate";
			this.Text = "견적관리";
			this.Load += new System.EventHandler(this.frmComplain_HS_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
			this.splitContainerControl1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
			this.splitContainerControl1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
			this.splitContainerControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
			this.layoutControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_EstimateMater)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_EstimateMater)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
			this.layoutControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_BM_EstimateD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_BM_EstimateD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControl layoutControl2;
		private DevExpress.XtraGrid.GridControl Gc_EstimateMater;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_EstimateMater;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.LayoutControl layoutControl3;
		private DevExpress.XtraGrid.GridControl Gc_BM_EstimateD;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_BM_EstimateD;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private baseSMT.smtSearchLookUp LupCustomer;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton cmdExcel;
        private baseSMT.smtSearchButton cmdInspectionRpt;
        private baseSMT.smtSearchButton cmdOrder;
        private baseSMT.smtSearchButton cmdInsert;
        private baseSMT.smtSearchButton cmdUpdate;
        private baseSMT.smtSearchButton cmdDelete;
    }
}