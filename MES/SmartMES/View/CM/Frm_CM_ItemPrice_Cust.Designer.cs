namespace SmartMES.View.CM
{
    partial class Frm_CM_ItemPrice_Cust
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_ItemPrice_Cust));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Pnl_History = new DevExpress.XtraEditors.GroupControl();
            this.Gc_History = new DevExpress.XtraGrid.GridControl();
            this.Gv_History = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.Btn_History_Save = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_HistoryRow_Del = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_History_Close = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_HistoryRow_Ins = new DevExpress.XtraEditors.SimpleButton();
            this.GcCustomer = new DevExpress.XtraGrid.GridControl();
            this.GvCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcMaterialPrice = new DevExpress.XtraGrid.GridControl();
            this.gvMaterialPrice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdExcelUpload = new baseSMT.smtSearchButton();
            this.cmdIns = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDel = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.LupPriceGroup = new baseSMT.smtSearchLookUp();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_History)).BeginInit();
            this.Pnl_History.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_History)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_History)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMaterialPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMaterialPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Pnl_History);
            this.layoutControl1.Controls.Add(this.GcCustomer);
            this.layoutControl1.Controls.Add(this.gcMaterialPrice);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1077, 338, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1249, 588);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Pnl_History
            // 
            this.Pnl_History.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.Pnl_History.AppearanceCaption.Options.UseFont = true;
            this.Pnl_History.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Pnl_History.CaptionImageOptions.Image")));
            this.Pnl_History.Controls.Add(this.Gc_History);
            this.Pnl_History.Controls.Add(this.panelControl2);
            this.Pnl_History.Location = new System.Drawing.Point(389, 406);
            this.Pnl_History.Name = "Pnl_History";
            this.Pnl_History.ShowCaption = false;
            this.Pnl_History.Size = new System.Drawing.Size(857, 179);
            this.Pnl_History.TabIndex = 284;
            this.Pnl_History.Text = "품목별 단가내역";
            // 
            // Gc_History
            // 
            this.Gc_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_History.Location = new System.Drawing.Point(2, 37);
            this.Gc_History.MainView = this.Gv_History;
            this.Gc_History.Name = "Gc_History";
            this.Gc_History.Size = new System.Drawing.Size(853, 140);
            this.Gc_History.TabIndex = 1;
            this.Gc_History.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_History});
            // 
            // Gv_History
            // 
            this.Gv_History.GridControl = this.Gc_History;
            this.Gv_History.Name = "Gv_History";
            this.Gv_History.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.Btn_History_Save);
            this.panelControl2.Controls.Add(this.Btn_HistoryRow_Del);
            this.panelControl2.Controls.Add(this.Btn_History_Close);
            this.panelControl2.Controls.Add(this.Btn_HistoryRow_Ins);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl2.Size = new System.Drawing.Size(853, 35);
            this.panelControl2.TabIndex = 282;
            // 
            // Btn_History_Save
            // 
            this.Btn_History_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_History_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_History_Save.ImageOptions.Image")));
            this.Btn_History_Save.Location = new System.Drawing.Point(627, 6);
            this.Btn_History_Save.Name = "Btn_History_Save";
            this.Btn_History_Save.Size = new System.Drawing.Size(70, 23);
            this.Btn_History_Save.TabIndex = 293;
            this.Btn_History_Save.Text = "저장";
            this.Btn_History_Save.Click += new System.EventHandler(this.Btn_History_Save_Click);
            // 
            // Btn_HistoryRow_Del
            // 
            this.Btn_HistoryRow_Del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_HistoryRow_Del.ImageOptions.Image = global::SmartMES.Properties.Resources.cut_16x16;
            this.Btn_HistoryRow_Del.Location = new System.Drawing.Point(702, 6);
            this.Btn_HistoryRow_Del.Name = "Btn_HistoryRow_Del";
            this.Btn_HistoryRow_Del.Size = new System.Drawing.Size(70, 23);
            this.Btn_HistoryRow_Del.TabIndex = 292;
            this.Btn_HistoryRow_Del.Text = "삭제";
            this.Btn_HistoryRow_Del.Click += new System.EventHandler(this.Btn_RowEdit_History);
            // 
            // Btn_History_Close
            // 
            this.Btn_History_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_History_Close.ImageOptions.Image = global::SmartMES.Properties.Resources.cancel_16x16;
            this.Btn_History_Close.Location = new System.Drawing.Point(779, 6);
            this.Btn_History_Close.Name = "Btn_History_Close";
            this.Btn_History_Close.Size = new System.Drawing.Size(70, 23);
            this.Btn_History_Close.TabIndex = 290;
            this.Btn_History_Close.Text = "닫기";
            this.Btn_History_Close.Click += new System.EventHandler(this.Btn_History_Close_Click);
            // 
            // Btn_HistoryRow_Ins
            // 
            this.Btn_HistoryRow_Ins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_HistoryRow_Ins.Location = new System.Drawing.Point(551, 6);
            this.Btn_HistoryRow_Ins.Name = "Btn_HistoryRow_Ins";
            this.Btn_HistoryRow_Ins.Size = new System.Drawing.Size(70, 23);
            this.Btn_HistoryRow_Ins.TabIndex = 290;
            this.Btn_HistoryRow_Ins.Text = " 신규 ";
            this.Btn_HistoryRow_Ins.Visible = false;
            // 
            // GcCustomer
            // 
            this.GcCustomer.Location = new System.Drawing.Point(19, 46);
            this.GcCustomer.MainView = this.GvCustomer;
            this.GcCustomer.Name = "GcCustomer";
            this.GcCustomer.Size = new System.Drawing.Size(332, 523);
            this.GcCustomer.TabIndex = 40;
            this.GcCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvCustomer});
            // 
            // GvCustomer
            // 
            this.GvCustomer.GridControl = this.GcCustomer;
            this.GvCustomer.Name = "GvCustomer";
            // 
            // gcMaterialPrice
            // 
            this.gcMaterialPrice.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcMaterialPrice.Location = new System.Drawing.Point(405, 46);
            this.gcMaterialPrice.MainView = this.gvMaterialPrice;
            this.gcMaterialPrice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcMaterialPrice.Name = "gcMaterialPrice";
            this.gcMaterialPrice.Size = new System.Drawing.Size(825, 338);
            this.gcMaterialPrice.TabIndex = 39;
            this.gcMaterialPrice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMaterialPrice});
            // 
            // gvMaterialPrice
            // 
            this.gvMaterialPrice.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvMaterialPrice.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvMaterialPrice.Appearance.Row.Options.UseFont = true;
            this.gvMaterialPrice.ColumnPanelRowHeight = 25;
            this.gvMaterialPrice.GridControl = this.gcMaterialPrice;
            this.gvMaterialPrice.Name = "gvMaterialPrice";
            this.gvMaterialPrice.OptionsView.ShowGroupPanel = false;
            this.gvMaterialPrice.RowHeight = 25;
            this.gvMaterialPrice.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMaterialPrice_CellValueChanged);
            this.gvMaterialPrice.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridView1_ValidatingEditor);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1249, 588);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.splitterItem2,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1249, 588);
            this.layoutControlGroup1.Text = "단가리스트";
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(370, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(16, 588);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(370, 588);
            this.layoutControlGroup2.Text = "거래처 리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GcCustomer;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(338, 529);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(386, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(863, 403);
            this.layoutControlGroup3.Text = "단가 리스트";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcMaterialPrice;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(831, 344);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.Pnl_History;
            this.layoutControlItem2.Location = new System.Drawing.Point(386, 403);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(863, 185);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.LupPriceGroup);
            this.smtCommonBar1.Controls.Add(this.cmdExcelUpload);
            this.smtCommonBar1.Controls.Add(this.cmdIns);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.cmdDel);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1249, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "거래처별 단가";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 6;
            // 
            // cmdExcelUpload
            // 
            this.cmdExcelUpload.AllowFocus = false;
            this.cmdExcelUpload.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdExcelUpload.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdExcelUpload.Appearance.Options.UseBackColor = true;
            this.cmdExcelUpload.Appearance.Options.UseForeColor = true;
            this.cmdExcelUpload.AutoSize = true;
            this.cmdExcelUpload.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdExcelUpload.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdExcelUpload.ImageOptions.ImageIndex = 8;
            this.cmdExcelUpload.ImageOptions.ImageToTextIndent = 15;
            this.cmdExcelUpload.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdExcelUpload.Location = new System.Drawing.Point(828, 0);
            this.cmdExcelUpload.Name = "cmdExcelUpload";
            this.cmdExcelUpload.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcelUpload.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcelUpload.Size = new System.Drawing.Size(113, 40);
            this.cmdExcelUpload.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀업로드;
            this.cmdExcelUpload.TabIndex = 30;
            this.cmdExcelUpload.Tag = "Ins";
            this.cmdExcelUpload.Text = "엑셀업로드";
            this.cmdExcelUpload.Click += new System.EventHandler(this.cmdExcelUpload_Click);
            // 
            // cmdIns
            // 
            this.cmdIns.AllowFocus = false;
            this.cmdIns.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdIns.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdIns.Appearance.Options.UseBackColor = true;
            this.cmdIns.Appearance.Options.UseForeColor = true;
            this.cmdIns.AutoSize = true;
            this.cmdIns.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdIns.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdIns.ImageOptions.ImageIndex = 1;
            this.cmdIns.ImageOptions.ImageToTextIndent = 15;
            this.cmdIns.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdIns.Location = new System.Drawing.Point(941, 0);
            this.cmdIns.Name = "cmdIns";
            this.cmdIns.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdIns.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdIns.Size = new System.Drawing.Size(77, 40);
            this.cmdIns.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdIns.TabIndex = 7;
            this.cmdIns.Tag = "Ins";
            this.cmdIns.Text = "등록";
            this.cmdIns.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdSave.Location = new System.Drawing.Point(1018, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "저장";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
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
            this.cmdDel.Location = new System.Drawing.Point(1095, 0);
            this.cmdDel.Name = "cmdDel";
            this.cmdDel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDel.Size = new System.Drawing.Size(77, 40);
            this.cmdDel.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDel.TabIndex = 2;
            this.cmdDel.Tag = "Del";
            this.cmdDel.Text = "삭제";
            this.cmdDel.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdClose.Location = new System.Drawing.Point(1172, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // LupPriceGroup
            // 
            this.LupPriceGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupPriceGroup.Location = new System.Drawing.Point(119, 0);
            this.LupPriceGroup.LookAndFeel.SkinName = "WXI";
            this.LupPriceGroup.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupPriceGroup.Name = "LupPriceGroup";
            this.LupPriceGroup.Size = new System.Drawing.Size(233, 40);
            this.LupPriceGroup.smtLabelTitleValue = "단가그룹";
            this.LupPriceGroup.smtLabelTitleVisible = true;
            this.LupPriceGroup.smtLabelTitleWidth = 100;
            this.LupPriceGroup.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupPriceGroup.smtLookUpDataDisplayMember = "코드명";
            this.LupPriceGroup.smtLookUpDataSource = null;
            this.LupPriceGroup.smtLookUpDataValueMember = "코드";
            this.LupPriceGroup.smtLookUpSelectdIndex = -1;
            this.LupPriceGroup.smtLookUpText = "";
            this.LupPriceGroup.smtLookUpValue = "품목분류";
            this.LupPriceGroup.TabIndex = 34;
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
            this.cmdDisplay.Location = new System.Drawing.Point(352, 0);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 35;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // Frm_CM_ItemPrice_Cust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 628);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_CM_ItemPrice_Cust";
            this.Text = "거래처별 단가";
            this.Load += new System.EventHandler(this.Frm_CM_ItemPrice_Cust_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_History)).EndInit();
            this.Pnl_History.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_History)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_History)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMaterialPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMaterialPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcMaterialPrice;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMaterialPrice;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdIns;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDel;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraGrid.GridControl GcCustomer;
		private DevExpress.XtraGrid.Views.Grid.GridView GvCustomer;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.SplitterItem splitterItem2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.GroupControl Pnl_History;
        private DevExpress.XtraGrid.GridControl Gc_History;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_History;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton Btn_History_Save;
        private DevExpress.XtraEditors.SimpleButton Btn_HistoryRow_Del;
        private DevExpress.XtraEditors.SimpleButton Btn_History_Close;
        private DevExpress.XtraEditors.SimpleButton Btn_HistoryRow_Ins;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private baseSMT.smtSearchButton cmdExcelUpload;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchLookUp LupPriceGroup;
    }
}