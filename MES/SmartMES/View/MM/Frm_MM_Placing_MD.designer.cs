namespace SmartMES.View.MM
{
    partial class Frm_MM_Placing_MD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_Placing_MD));
            this.colItemName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.smtSearchButton4 = new baseSMT.smtSearchButton();
            this.smtSearchButton3 = new baseSMT.smtSearchButton();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.customerBox = new baseSMT.smtSearchLookUp();
            this.smtSearchButton2 = new baseSMT.smtSearchButton();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.cmdPO = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.gcDetail = new DevExpress.XtraGrid.GridControl();
            this.gvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnCancel = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gcPlacingMaster = new DevExpress.XtraGrid.GridControl();
            this.gvPlacingMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlacingMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlacingMaster)).BeginInit();
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
            // colItemName
            // 
            this.colItemName.Caption = "품명";
            this.colItemName.FieldName = "ItemName";
            this.colItemName.MinWidth = 29;
            this.colItemName.Name = "colItemName";
            this.colItemName.Width = 214;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.smtSearchButton4);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton3);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.customerBox);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton2);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdPO);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1415, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "발주 관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 9;
            // 
            // smtSearchButton4
            // 
            this.smtSearchButton4.AllowFocus = false;
            this.smtSearchButton4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton4.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton4.Appearance.Options.UseBackColor = true;
            this.smtSearchButton4.Appearance.Options.UseForeColor = true;
            this.smtSearchButton4.AutoSize = true;
            this.smtSearchButton4.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton4.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton4.ImageOptions.ImageIndex = 1;
            this.smtSearchButton4.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton4.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton4.Location = new System.Drawing.Point(821, 0);
            this.smtSearchButton4.Name = "smtSearchButton4";
            this.smtSearchButton4.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton4.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton4.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton4.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.smtSearchButton4.TabIndex = 18;
            this.smtSearchButton4.Text = "등록";
            this.smtSearchButton4.Click += new System.EventHandler(this.button6_Click);
            // 
            // smtSearchButton3
            // 
            this.smtSearchButton3.AllowFocus = false;
            this.smtSearchButton3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton3.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton3.Appearance.Options.UseBackColor = true;
            this.smtSearchButton3.Appearance.Options.UseForeColor = true;
            this.smtSearchButton3.AutoSize = true;
            this.smtSearchButton3.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton3.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton3.ImageOptions.ImageIndex = 2;
            this.smtSearchButton3.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton3.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton3.Location = new System.Drawing.Point(898, 0);
            this.smtSearchButton3.Name = "smtSearchButton3";
            this.smtSearchButton3.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton3.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton3.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton3.smtButtonStyle = baseSMT.EnumButtonStyles.수정;
            this.smtSearchButton3.TabIndex = 17;
            this.smtSearchButton3.Text = "수정";
            this.smtSearchButton3.Click += new System.EventHandler(this.button5_Click);
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
            this.smtSearchButton1.ImageOptions.ImageIndex = 3;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(975, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.smtSearchButton1.TabIndex = 16;
            this.smtSearchButton1.Text = "삭제";
            this.smtSearchButton1.Click += new System.EventHandler(this.button4_Click);
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
            this.btn_Search.Location = new System.Drawing.Point(728, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 15;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // customerBox
            // 
            this.customerBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.customerBox.Location = new System.Drawing.Point(440, 0);
            this.customerBox.LookAndFeel.SkinName = "WXI";
            this.customerBox.LookAndFeel.UseDefaultLookAndFeel = false;
            this.customerBox.Name = "customerBox";
            this.customerBox.Size = new System.Drawing.Size(288, 40);
            this.customerBox.smtLabelForeColor = System.Drawing.Color.White;
            this.customerBox.smtLabelTitleValue = "거래처";
            this.customerBox.smtLabelTitleVisible = true;
            this.customerBox.smtLabelTitleWidth = 70;
            this.customerBox.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.customerBox.smtLookUpDataDisplayMember = "코드명";
            this.customerBox.smtLookUpDataSource = null;
            this.customerBox.smtLookUpDataValueMember = "코드";
            this.customerBox.smtLookUpSelectdIndex = -1;
            this.customerBox.smtLookUpText = "";
            this.customerBox.smtLookUpValue = "";
            this.customerBox.TabIndex = 13;
            // 
            // smtSearchButton2
            // 
            this.smtSearchButton2.AllowFocus = false;
            this.smtSearchButton2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton2.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton2.Appearance.Options.UseBackColor = true;
            this.smtSearchButton2.Appearance.Options.UseForeColor = true;
            this.smtSearchButton2.AutoSize = true;
            this.smtSearchButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton2.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton2.ImageOptions.Image = global::SmartMES.Properties.Resources.completed;
            this.smtSearchButton2.ImageOptions.ImageIndex = 4;
            this.smtSearchButton2.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton2.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton2.Location = new System.Drawing.Point(1052, 0);
            this.smtSearchButton2.Name = "smtSearchButton2";
            this.smtSearchButton2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton2.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton2.Size = new System.Drawing.Size(96, 40);
            this.smtSearchButton2.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.smtSearchButton2.TabIndex = 12;
            this.smtSearchButton2.Text = "입고등록";
            this.smtSearchButton2.Click += new System.EventHandler(this.Btn_Row_Ins_Click);
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(95, 0);
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
            this.Dt_Date.TabIndex = 10;
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
            this.cmdExcel.Location = new System.Drawing.Point(1148, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(101, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 6;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.button2_Click);
            this.cmdExcel.DoubleClick += new System.EventHandler(this.button2_Click);
            // 
            // cmdPO
            // 
            this.cmdPO.AllowFocus = false;
            this.cmdPO.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdPO.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdPO.Appearance.Options.UseBackColor = true;
            this.cmdPO.Appearance.Options.UseForeColor = true;
            this.cmdPO.AutoSize = true;
            this.cmdPO.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdPO.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdPO.ImageOptions.ImageIndex = 10;
            this.cmdPO.ImageOptions.ImageToTextIndent = 15;
            this.cmdPO.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdPO.Location = new System.Drawing.Point(1249, 0);
            this.cmdPO.Name = "cmdPO";
            this.cmdPO.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdPO.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdPO.Size = new System.Drawing.Size(89, 40);
            this.cmdPO.smtButtonStyle = baseSMT.EnumButtonStyles.프린터;
            this.cmdPO.TabIndex = 19;
            this.cmdPO.Text = "발주서";
            this.cmdPO.Click += new System.EventHandler(this.cmdPO_Click);
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
            this.cmdClose.Location = new System.Drawing.Point(1338, 0);
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
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1415, 765);
            this.layoutControl1.TabIndex = 10;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.gcDetail);
            this.layoutControl2.Controls.Add(this.gcPlacingMaster);
            this.layoutControl2.Location = new System.Drawing.Point(3, 3);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1623, 870, 812, 500);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1409, 759);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // gcDetail
            // 
            this.gcDetail.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcDetail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gcDetail.Location = new System.Drawing.Point(7, 463);
            this.gcDetail.MainView = this.gvDetail;
            this.gcDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcDetail.Name = "gcDetail";
            this.gcDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDelete,
            this.btnCancel});
            this.gcDetail.Size = new System.Drawing.Size(1395, 289);
            this.gcDetail.TabIndex = 88;
            this.gcDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetail});
            // 
            // gvDetail
            // 
            this.gvDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvDetail.ColumnPanelRowHeight = 29;
            this.gvDetail.DetailHeight = 495;
            this.gvDetail.FixedLineWidth = 3;
            this.gvDetail.GridControl = this.gcDetail;
            this.gvDetail.Name = "gvDetail";
            this.gvDetail.OptionsView.ColumnAutoWidth = false;
            this.gvDetail.OptionsView.ShowFooter = true;
            this.gvDetail.OptionsView.ShowGroupPanel = false;
            this.gvDetail.RowHeight = 29;
            // 
            // btnDelete
            // 
            this.btnDelete.AutoHeight = false;
            this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoHeight = false;
            this.btnCancel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Undo)});
            this.btnCancel.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // gcPlacingMaster
            // 
            this.gcPlacingMaster.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcPlacingMaster.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gcPlacingMaster.Location = new System.Drawing.Point(7, 34);
            this.gcPlacingMaster.MainView = this.gvPlacingMaster;
            this.gcPlacingMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcPlacingMaster.Name = "gcPlacingMaster";
            this.gcPlacingMaster.Size = new System.Drawing.Size(1395, 372);
            this.gcPlacingMaster.TabIndex = 4;
            this.gcPlacingMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPlacingMaster});
            // 
            // gvPlacingMaster
            // 
            this.gvPlacingMaster.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvPlacingMaster.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvPlacingMaster.ColumnPanelRowHeight = 29;
            this.gvPlacingMaster.DetailHeight = 425;
            this.gvPlacingMaster.GridControl = this.gcPlacingMaster;
            this.gvPlacingMaster.Name = "gvPlacingMaster";
            this.gvPlacingMaster.OptionsBehavior.Editable = false;
            this.gvPlacingMaster.OptionsDetail.EnableMasterViewMode = false;
            this.gvPlacingMaster.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            this.gvPlacingMaster.OptionsView.ColumnAutoWidth = false;
            this.gvPlacingMaster.OptionsView.ShowAutoFilterRow = true;
            this.gvPlacingMaster.OptionsView.ShowGroupPanel = false;
            this.gvPlacingMaster.OptionsView.ShowIndicator = false;
            this.gvPlacingMaster.RowHeight = 29;
            this.gvPlacingMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvPlacingMaster_FocusedRowChanged);
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(1409, 759);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "발주등록 마스터";
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
            this.layoutControlGroup2.Size = new System.Drawing.Size(1409, 413);
            this.layoutControlGroup2.Text = "발주등록 마스터";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcPlacingMaster;
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
            this.layoutControlItem1.Size = new System.Drawing.Size(1401, 378);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "발주 품목";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 429);
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
            this.layoutControlGroup3.Size = new System.Drawing.Size(1409, 330);
            this.layoutControlGroup3.Text = "발주등록 품목";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcDetail;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1401, 295);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 413);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1409, 16);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1415, 765);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.layoutControl2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1415, 765);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Frm_MM_Placing_MD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1415, 805);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_MM_Placing_MD";
            this.Text = "발주 관리";
            this.Load += new System.EventHandler(this.Frm_MM_Placing_MD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlacingMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlacingMaster)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colItemName;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchLookUp customerBox;
        private baseSMT.smtSearchButton smtSearchButton2;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton cmdExcel;
        private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchButton smtSearchButton4;
        private baseSMT.smtSearchButton smtSearchButton3;
        private baseSMT.smtSearchButton smtSearchButton1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraGrid.GridControl gcDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetail;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnCancel;
        private DevExpress.XtraGrid.GridControl gcPlacingMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPlacingMaster;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private baseSMT.smtSearchButton cmdPO;
	}
}