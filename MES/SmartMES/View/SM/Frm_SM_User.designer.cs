namespace SmartMES.View.SM
{
    partial class Frm_SM_User
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SM_User));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
			this.Gc_List = new DevExpress.XtraGrid.GridControl();
			this.Gv_List = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			this.cboYesNo2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.cboYesNo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.cboYesNo1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.cmdInsert = new baseSMT.smtSearchButton();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdDelete = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.colCorpId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colEmpNo = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colPassword = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDeptCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colPhone = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCellPhone = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colAuthorGroup = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUseYn = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCustYn = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCustCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colWorkerYn = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colPhoto = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gc_List)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_List)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboYesNo2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboYesNo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboYesNo1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.pictureEdit);
			this.layoutControl1.Controls.Add(this.Gc_List);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1265, 304, 650, 400);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1281, 594);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// pictureEdit
			// 
			this.pictureEdit.Location = new System.Drawing.Point(910, 34);
			this.pictureEdit.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.pictureEdit.Name = "pictureEdit";
			this.pictureEdit.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
			this.pictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			this.pictureEdit.Properties.ShowScrollBars = true;
			this.pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
			this.pictureEdit.Size = new System.Drawing.Size(366, 553);
			this.pictureEdit.StyleController = this.layoutControl1;
			this.pictureEdit.TabIndex = 1;
			// 
			// Gc_List
			// 
			this.Gc_List.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
			this.Gc_List.Location = new System.Drawing.Point(5, 34);
			this.Gc_List.MainView = this.Gv_List;
			this.Gc_List.Name = "Gc_List";
			this.Gc_List.Size = new System.Drawing.Size(879, 553);
			this.Gc_List.TabIndex = 101;
			this.Gc_List.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_List});
			// 
			// Gv_List
			// 
			this.Gv_List.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.Gv_List.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.Gv_List.ColumnPanelRowHeight = 35;
			this.Gv_List.DetailHeight = 497;
			this.Gv_List.GridControl = this.Gc_List;
			this.Gv_List.Name = "Gv_List";
			this.Gv_List.OptionsView.ShowGroupPanel = false;
			this.Gv_List.RowHeight = 35;
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup2,
            this.splitterItem1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1281, 594);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.AppearanceGroup.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
			this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
			this.layoutControlGroup3.AppearanceItemCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
			this.layoutControlGroup3.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlGroup3.CustomizationFormText = "세부항목";
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13});
			this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
			this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
			this.layoutControlGroup3.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(889, 594);
			this.layoutControlGroup3.Text = "사용자 리스트";
			// 
			// layoutControlItem13
			// 
			this.layoutControlItem13.Control = this.Gc_List;
			this.layoutControlItem13.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem13.CustomizationFormText = "layoutControlItem2";
			this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem13.Name = "layoutControlItem13";
			this.layoutControlItem13.Size = new System.Drawing.Size(883, 559);
			this.layoutControlItem13.Text = "layoutControlItem2";
			this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem13.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.CustomizationFormText = "제품이미지";
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup2.Location = new System.Drawing.Point(905, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 3;
			this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(376, 594);
			this.layoutControlGroup2.Text = "사인이미지";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.pictureEdit;
			this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(370, 559);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// splitterItem1
			// 
			this.splitterItem1.AllowHotTrack = true;
			this.splitterItem1.Location = new System.Drawing.Point(889, 0);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(16, 594);
			// 
			// cboYesNo2
			// 
			this.cboYesNo2.AutoHeight = false;
			this.cboYesNo2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboYesNo2.Items.AddRange(new object[] {
            "Y",
            "N"});
			this.cboYesNo2.Name = "cboYesNo2";
			this.cboYesNo2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// cboYesNo
			// 
			this.cboYesNo.AutoHeight = false;
			this.cboYesNo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboYesNo.Items.AddRange(new object[] {
            "Y",
            "N"});
			this.cboYesNo.Name = "cboYesNo";
			this.cboYesNo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// cboYesNo1
			// 
			this.cboYesNo1.AutoHeight = false;
			this.cboYesNo1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.cboYesNo1.Items.AddRange(new object[] {
            "Y",
            "N"});
			this.cboYesNo1.Name = "cboYesNo1";
			this.cboYesNo1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.cmdInsert);
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdDelete);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1281, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "사용자 관리";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 2;
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
			this.cmdDisplay.Location = new System.Drawing.Point(107, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Visible = false;
			this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
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
			this.cmdInsert.Location = new System.Drawing.Point(973, 0);
			this.cmdInsert.Name = "cmdInsert";
			this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInsert.Size = new System.Drawing.Size(77, 40);
			this.cmdInsert.smtButtonAutoText = false;
			this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdInsert.TabIndex = 4;
			this.cmdInsert.Tag = "Ins";
			this.cmdInsert.Text = "등록";
			this.cmdInsert.Click += new System.EventHandler(this.Btn_RowEdit);
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
			this.cmdSave.Location = new System.Drawing.Point(1050, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
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
			this.cmdDelete.Location = new System.Drawing.Point(1127, 0);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDelete.Size = new System.Drawing.Size(77, 40);
			this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDelete.TabIndex = 2;
			this.cmdDelete.Tag = "Del";
			this.cmdDelete.Text = "삭제";
			this.cmdDelete.Click += new System.EventHandler(this.Btn_RowEdit);
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
			this.cmdClose.Location = new System.Drawing.Point(1204, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// colCorpId
			// 
			this.colCorpId.AppearanceHeader.Options.UseTextOptions = true;
			this.colCorpId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCorpId.Caption = "사업부";
			this.colCorpId.FieldName = "CorpId";
			this.colCorpId.MinWidth = 24;
			this.colCorpId.Name = "colCorpId";
			this.colCorpId.Visible = true;
			this.colCorpId.VisibleIndex = 0;
			this.colCorpId.Width = 137;
			// 
			// colEmpNo
			// 
			this.colEmpNo.AppearanceHeader.Options.UseTextOptions = true;
			this.colEmpNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colEmpNo.Caption = "사용자ID";
			this.colEmpNo.FieldName = "EmployeeId";
			this.colEmpNo.MinWidth = 24;
			this.colEmpNo.Name = "colEmpNo";
			this.colEmpNo.Visible = true;
			this.colEmpNo.VisibleIndex = 1;
			this.colEmpNo.Width = 100;
			// 
			// colName
			// 
			this.colName.AppearanceHeader.Options.UseTextOptions = true;
			this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colName.Caption = "사용자명";
			this.colName.FieldName = "EmployeeName";
			this.colName.MinWidth = 24;
			this.colName.Name = "colName";
			this.colName.Visible = true;
			this.colName.VisibleIndex = 2;
			this.colName.Width = 120;
			// 
			// colPassword
			// 
			this.colPassword.AppearanceHeader.Options.UseTextOptions = true;
			this.colPassword.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colPassword.Caption = "비밀번호변경";
			this.colPassword.FieldName = "Password2";
			this.colPassword.MinWidth = 24;
			this.colPassword.Name = "colPassword";
			this.colPassword.Visible = true;
			this.colPassword.VisibleIndex = 3;
			this.colPassword.Width = 124;
			// 
			// colDeptCode
			// 
			this.colDeptCode.AppearanceHeader.Options.UseTextOptions = true;
			this.colDeptCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colDeptCode.Caption = "부 서";
			this.colDeptCode.FieldName = "DeptId";
			this.colDeptCode.MinWidth = 24;
			this.colDeptCode.Name = "colDeptCode";
			this.colDeptCode.Visible = true;
			this.colDeptCode.VisibleIndex = 4;
			this.colDeptCode.Width = 72;
			// 
			// colPhone
			// 
			this.colPhone.AppearanceHeader.Options.UseTextOptions = true;
			this.colPhone.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colPhone.Caption = "전화번호";
			this.colPhone.FieldName = "Phone";
			this.colPhone.MinWidth = 24;
			this.colPhone.Name = "colPhone";
			this.colPhone.Visible = true;
			this.colPhone.VisibleIndex = 5;
			this.colPhone.Width = 130;
			// 
			// colCellPhone
			// 
			this.colCellPhone.AppearanceHeader.Options.UseTextOptions = true;
			this.colCellPhone.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCellPhone.Caption = "휴대폰";
			this.colCellPhone.FieldName = "CellPhone";
			this.colCellPhone.MinWidth = 24;
			this.colCellPhone.Name = "colCellPhone";
			this.colCellPhone.Visible = true;
			this.colCellPhone.VisibleIndex = 6;
			this.colCellPhone.Width = 130;
			// 
			// colAuthorGroup
			// 
			this.colAuthorGroup.AppearanceHeader.Options.UseTextOptions = true;
			this.colAuthorGroup.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colAuthorGroup.Caption = "권한그룹";
			this.colAuthorGroup.FieldName = "AuthorGroup";
			this.colAuthorGroup.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
			this.colAuthorGroup.MinWidth = 24;
			this.colAuthorGroup.Name = "colAuthorGroup";
			this.colAuthorGroup.Visible = true;
			this.colAuthorGroup.VisibleIndex = 7;
			this.colAuthorGroup.Width = 97;
			// 
			// colUseYn
			// 
			this.colUseYn.AppearanceHeader.Options.UseTextOptions = true;
			this.colUseYn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colUseYn.Caption = "사용여부";
			this.colUseYn.FieldName = "UseYn";
			this.colUseYn.MinWidth = 24;
			this.colUseYn.Name = "colUseYn";
			this.colUseYn.Width = 121;
			// 
			// colEmail
			// 
			this.colEmail.AppearanceCell.Options.UseTextOptions = true;
			this.colEmail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colEmail.AppearanceHeader.Options.UseTextOptions = true;
			this.colEmail.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colEmail.Caption = "이메일";
			this.colEmail.FieldName = "Email";
			this.colEmail.MinWidth = 24;
			this.colEmail.Name = "colEmail";
			this.colEmail.Visible = true;
			this.colEmail.VisibleIndex = 8;
			this.colEmail.Width = 145;
			// 
			// colCustYn
			// 
			this.colCustYn.AppearanceCell.Options.UseTextOptions = true;
			this.colCustYn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCustYn.AppearanceHeader.Options.UseTextOptions = true;
			this.colCustYn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCustYn.Caption = "협력업체유무";
			this.colCustYn.FieldName = "CustYn";
			this.colCustYn.MinWidth = 24;
			this.colCustYn.Name = "colCustYn";
			this.colCustYn.Width = 104;
			// 
			// colCustCode
			// 
			this.colCustCode.AppearanceCell.Options.UseTextOptions = true;
			this.colCustCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCustCode.AppearanceHeader.Options.UseTextOptions = true;
			this.colCustCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCustCode.Caption = "거래처";
			this.colCustCode.FieldName = "CustCode";
			this.colCustCode.MinWidth = 24;
			this.colCustCode.Name = "colCustCode";
			this.colCustCode.Width = 94;
			// 
			// colWorkerYn
			// 
			this.colWorkerYn.Caption = "작업자여부";
			this.colWorkerYn.FieldName = "WorkerYn";
			this.colWorkerYn.MinWidth = 25;
			this.colWorkerYn.Name = "colWorkerYn";
			this.colWorkerYn.Visible = true;
			this.colWorkerYn.VisibleIndex = 9;
			this.colWorkerYn.Width = 94;
			// 
			// colPhoto
			// 
			this.colPhoto.AppearanceHeader.Options.UseTextOptions = true;
			this.colPhoto.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colPhoto.FieldName = "Photo";
			this.colPhoto.MinWidth = 24;
			this.colPhoto.Name = "colPhoto";
			this.colPhoto.Width = 112;
			// 
			// Frm_SM_User
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1281, 634);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.Name = "Frm_SM_User";
			this.Text = "사용자 관리";
			this.Load += new System.EventHandler(this.Frm_SM_User_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gc_List)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_List)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboYesNo2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboYesNo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboYesNo1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboYesNo2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboYesNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboYesNo1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchButton cmdInsert;
        private baseSMT.smtSearchButton cmdSave;
        private baseSMT.smtSearchButton cmdDelete;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraGrid.GridControl Gc_List;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_List;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId;
        private DevExpress.XtraGrid.Columns.GridColumn colEmpNo;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colPassword;
        private DevExpress.XtraGrid.Columns.GridColumn colDeptCode;
        private DevExpress.XtraGrid.Columns.GridColumn colPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colCellPhone;
        private DevExpress.XtraGrid.Columns.GridColumn colAuthorGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colUseYn;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colCustYn;
        private DevExpress.XtraGrid.Columns.GridColumn colCustCode;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkerYn;
        private DevExpress.XtraGrid.Columns.GridColumn colPhoto;
    }
}