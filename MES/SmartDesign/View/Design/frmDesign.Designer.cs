namespace SmartDesign.View.Design
{
    partial class frmDesign
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDesign));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.projectMemberBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.designDataSet = new SmartDesign.DesignDataSet();
            this.codeDocumentFileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.codeDataSet = new SmartDesign.CodeDataSet();
            this.projectMasterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colCorpID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.projectMasterTableAdapter = new SmartDesign.DesignDataSetTableAdapters.ProjectMasterTableAdapter();
            this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.foDialog = new System.Windows.Forms.OpenFileDialog();
            this.codeDocumentFileTableAdapter = new SmartDesign.CodeDataSetTableAdapters.CodeDocumentFileTableAdapter();
            this.projectMemberTableAdapter = new SmartDesign.DesignDataSetTableAdapters.ProjectMemberTableAdapter();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.smtSearchButton4 = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMemberAdd = new System.Windows.Forms.Button();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit3 = new DevExpress.XtraEditors.PictureEdit();
            this.gcProjectMember = new DevExpress.XtraGrid.GridControl();
            this.gvProjectMember = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.searchEmployee = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView8 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.searchRole = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.searchPermission = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit3View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.gcCodeDocumentFile = new DevExpress.XtraGrid.GridControl();
            this.gvCodeDocumentFile = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGubun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKeyCode1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileSeq = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.itemCodeColLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colTitle3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCapacity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileExt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnFileDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colOriFileName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.productImagePictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.corpIDTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.projectIDTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.projectNameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.startDateDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.endDateDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.gcProjectMaster = new DevExpress.XtraGrid.GridControl();
            this.gvProjectMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.remarkTextEdit = new DevExpress.XtraEditors.MemoEdit();
            this.classTextEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.brandTextEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.productYearTextEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.seasonTextEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupNameTextEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chargeSearchLookUpEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pMNameSearchLookUpEidit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.typeTextEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.productYearDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.materialSearchLookUpEidit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView51 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCtlGrpFile = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.projectMemberBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.designDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeDocumentFileBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectMasterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectMember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectMember)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchRole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchPermission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCodeDocumentFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCodeDocumentFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemCodeColLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFileDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productImagePictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corpIDTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectIDTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectNameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startDateDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startDateDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDateDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDateDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.remarkTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.classTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brandTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productYearTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seasonTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupNameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargeSearchLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMNameSearchLookUpEidit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productYearDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productYearDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialSearchLookUpEidit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView51)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCtlGrpFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // projectMemberBindingSource
            // 
            this.projectMemberBindingSource.DataMember = "ProjectMember";
            this.projectMemberBindingSource.DataSource = this.designDataSet;
            // 
            // designDataSet
            // 
            this.designDataSet.DataSetName = "DesignDataSet";
            this.designDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // codeDocumentFileBindingSource
            // 
            this.codeDocumentFileBindingSource.DataMember = "CodeDocumentFile";
            this.codeDocumentFileBindingSource.DataSource = this.codeDataSet;
            // 
            // codeDataSet
            // 
            this.codeDataSet.DataSetName = "CodeDataSet";
            this.codeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // projectMasterBindingSource
            // 
            this.projectMasterBindingSource.DataMember = "ProjectMaster";
            this.projectMasterBindingSource.DataSource = this.designDataSet;
            // 
            // colCorpID
            // 
            this.colCorpID.Caption = "관리자번호";
            this.colCorpID.FieldName = "CorpID";
            this.colCorpID.Name = "colCorpID";
            this.colCorpID.Visible = true;
            this.colCorpID.VisibleIndex = 0;
            // 
            // colProjectID
            // 
            this.colProjectID.Caption = "프로젝트번호";
            this.colProjectID.FieldName = "ProjectID";
            this.colProjectID.Name = "colProjectID";
            this.colProjectID.Visible = true;
            this.colProjectID.VisibleIndex = 1;
            // 
            // colType
            // 
            this.colType.AccessibleName = "";
            this.colType.Caption = "제품타입";
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.Caption = "담당자";
            this.colEmployeeName.FieldName = "EmployeeName";
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.Visible = true;
            this.colEmployeeName.VisibleIndex = 3;
            // 
            // colProjectName
            // 
            this.colProjectName.Caption = "프로젝트명";
            this.colProjectName.FieldName = "ProjectName";
            this.colProjectName.Name = "colProjectName";
            this.colProjectName.Visible = true;
            this.colProjectName.VisibleIndex = 4;
            // 
            // colStartDate
            // 
            this.colStartDate.Caption = "시작일";
            this.colStartDate.FieldName = "StartDate";
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.Visible = true;
            this.colStartDate.VisibleIndex = 5;
            // 
            // colEndDate
            // 
            this.colEndDate.Caption = "종료일";
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.Visible = true;
            this.colEndDate.VisibleIndex = 6;
            // 
            // projectMasterTableAdapter
            // 
            this.projectMasterTableAdapter.ClearBeforeFill = true;
            // 
            // foDialog
            // 
            this.foDialog.FileName = "openFileDialog1";
            this.foDialog.Multiselect = true;
            // 
            // codeDocumentFileTableAdapter
            // 
            this.codeDocumentFileTableAdapter.ClearBeforeFill = true;
            // 
            // projectMemberTableAdapter
            // 
            this.projectMemberTableAdapter.ClearBeforeFill = true;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit2.Margin = new System.Windows.Forms.Padding(10);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Size = new System.Drawing.Size(32, 18);
            this.pictureEdit2.TabIndex = 7;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton4);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.cmdDelete);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1488, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "디자인 프로젝트 관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 1;
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
            this.cmdDisplay.Location = new System.Drawing.Point(378, 0);
            this.cmdDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 12;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(158, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Dt_Date.Name = "Dt_Date";
            this.Dt_Date.Size = new System.Drawing.Size(220, 40);
            this.Dt_Date.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Dt_Date.smtComboTitleItems = new string[0];
            this.Dt_Date.smtComboTitleValue = "";
            this.Dt_Date.smtComboTitleVisible = false;
            this.Dt_Date.smtComboTitleWidth = 100;
            this.Dt_Date.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 729);
            this.Dt_Date.smtEndDateTimeVisible = false;
            this.Dt_Date.smtLabelTitleValue = "조회년도";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Year;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 11;
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
            this.smtSearchButton4.Location = new System.Drawing.Point(1180, 0);
            this.smtSearchButton4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.smtSearchButton4.Name = "smtSearchButton4";
            this.smtSearchButton4.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton4.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton4.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton4.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.smtSearchButton4.TabIndex = 10;
            this.smtSearchButton4.Text = "등록";
            this.smtSearchButton4.Click += new System.EventHandler(this.cmdInsert_Click);
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
            this.cmdSave.Location = new System.Drawing.Point(1257, 0);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 9;
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
            this.cmdDelete.Location = new System.Drawing.Point(1334, 0);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(77, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 8;
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
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
            this.smtSearchButton1.ImageOptions.ImageIndex = 5;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(1411, 0);
            this.smtSearchButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.smtSearchButton1.TabIndex = 7;
            this.smtSearchButton1.Text = "닫기";
            this.smtSearchButton1.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1488, 1026);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.panel2);
            this.layoutControl2.Controls.Add(this.gcProjectMember);
            this.layoutControl2.Controls.Add(this.btnDownload);
            this.layoutControl2.Controls.Add(this.btnUpload);
            this.layoutControl2.Controls.Add(this.gcCodeDocumentFile);
            this.layoutControl2.Controls.Add(this.productImagePictureEdit);
            this.layoutControl2.Controls.Add(this.corpIDTextEdit);
            this.layoutControl2.Controls.Add(this.projectIDTextEdit);
            this.layoutControl2.Controls.Add(this.projectNameTextEdit);
            this.layoutControl2.Controls.Add(this.startDateDateEdit);
            this.layoutControl2.Controls.Add(this.endDateDateEdit);
            this.layoutControl2.Controls.Add(this.gcProjectMaster);
            this.layoutControl2.Controls.Add(this.remarkTextEdit);
            this.layoutControl2.Controls.Add(this.classTextEdit);
            this.layoutControl2.Controls.Add(this.brandTextEdit);
            this.layoutControl2.Controls.Add(this.productYearTextEdit);
            this.layoutControl2.Controls.Add(this.seasonTextEdit);
            this.layoutControl2.Controls.Add(this.groupNameTextEdit);
            this.layoutControl2.Controls.Add(this.chargeSearchLookUpEdit);
            this.layoutControl2.Controls.Add(this.pMNameSearchLookUpEidit);
            this.layoutControl2.Controls.Add(this.typeTextEdit);
            this.layoutControl2.Controls.Add(this.productYearDateEdit);
            this.layoutControl2.Controls.Add(this.materialSearchLookUpEidit);
            this.layoutControl2.Location = new System.Drawing.Point(3, 4);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1482, 1018);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnMemberAdd);
            this.panel2.Controls.Add(this.labelControl2);
            this.panel2.Controls.Add(this.pictureEdit3);
            this.panel2.Location = new System.Drawing.Point(979, 643);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(481, 32);
            this.panel2.TabIndex = 233;
            // 
            // btnMemberAdd
            // 
            this.btnMemberAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMemberAdd.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnMemberAdd.FlatAppearance.BorderSize = 0;
            this.btnMemberAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMemberAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnMemberAdd.ForeColor = System.Drawing.Color.White;
            this.btnMemberAdd.Image = global::SmartDesign.Properties.Resources.add1_16X16;
            this.btnMemberAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMemberAdd.Location = new System.Drawing.Point(404, 0);
            this.btnMemberAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMemberAdd.Name = "btnMemberAdd";
            this.btnMemberAdd.Size = new System.Drawing.Size(75, 30);
            this.btnMemberAdd.TabIndex = 33;
            this.btnMemberAdd.Text = "      추가";
            this.btnMemberAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMemberAdd.UseVisualStyleBackColor = true;
            this.btnMemberAdd.Click += new System.EventHandler(this.btnMemberAdd_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(37, 0);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(192, 30);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "프로젝트 멤버 관리";
            // 
            // pictureEdit3
            // 
            this.pictureEdit3.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit3.EditValue = ((object)(resources.GetObject("pictureEdit3.EditValue")));
            this.pictureEdit3.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit3.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.pictureEdit3.Name = "pictureEdit3";
            this.pictureEdit3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit3.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit3.Size = new System.Drawing.Size(37, 30);
            this.pictureEdit3.TabIndex = 7;
            // 
            // gcProjectMember
            // 
            this.gcProjectMember.DataSource = this.projectMemberBindingSource;
            this.gcProjectMember.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gcProjectMember.Location = new System.Drawing.Point(979, 683);
            this.gcProjectMember.MainView = this.gvProjectMember;
            this.gcProjectMember.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcProjectMember.Name = "gcProjectMember";
            this.gcProjectMember.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.searchEmployee,
            this.searchRole,
            this.searchPermission});
            this.gcProjectMember.Size = new System.Drawing.Size(481, 310);
            this.gcProjectMember.TabIndex = 232;
            this.gcProjectMember.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProjectMember});
            // 
            // gvProjectMember
            // 
            this.gvProjectMember.ColumnPanelRowHeight = 30;
            this.gvProjectMember.DetailHeight = 425;
            this.gvProjectMember.GridControl = this.gcProjectMember;
            this.gvProjectMember.Name = "gvProjectMember";
            this.gvProjectMember.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvProjectMember.OptionsView.ShowGroupPanel = false;
            this.gvProjectMember.RowHeight = 30;
            // 
            // searchEmployee
            // 
            this.searchEmployee.AutoHeight = false;
            this.searchEmployee.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchEmployee.Name = "searchEmployee";
            this.searchEmployee.NullText = "";
            this.searchEmployee.PopupView = this.gridView8;
            // 
            // gridView8
            // 
            this.gridView8.DetailHeight = 425;
            this.gridView8.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView8.Name = "gridView8";
            this.gridView8.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView8.OptionsView.ShowGroupPanel = false;
            // 
            // searchRole
            // 
            this.searchRole.AutoHeight = false;
            this.searchRole.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchRole.Name = "searchRole";
            this.searchRole.NullText = "";
            this.searchRole.PopupView = this.repositoryItemSearchLookUpEdit2View;
            // 
            // repositoryItemSearchLookUpEdit2View
            // 
            this.repositoryItemSearchLookUpEdit2View.DetailHeight = 425;
            this.repositoryItemSearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit2View.Name = "repositoryItemSearchLookUpEdit2View";
            this.repositoryItemSearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // searchPermission
            // 
            this.searchPermission.AutoHeight = false;
            this.searchPermission.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchPermission.Name = "searchPermission";
            this.searchPermission.NullText = "";
            this.searchPermission.PopupView = this.repositoryItemSearchLookUpEdit3View;
            // 
            // repositoryItemSearchLookUpEdit3View
            // 
            this.repositoryItemSearchLookUpEdit3View.DetailHeight = 425;
            this.repositoryItemSearchLookUpEdit3View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit3View.Name = "repositoryItemSearchLookUpEdit3View";
            this.repositoryItemSearchLookUpEdit3View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit3View.OptionsView.ShowGroupPanel = false;
            // 
            // btnDownload
            // 
            this.btnDownload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.ImageOptions.Image")));
            this.btnDownload.Location = new System.Drawing.Point(1368, 643);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Padding = new System.Windows.Forms.Padding(2);
            this.btnDownload.Size = new System.Drawing.Size(92, 32);
            this.btnDownload.StyleController = this.layoutControl2;
            this.btnDownload.TabIndex = 226;
            this.btnDownload.Text = "다운로드";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpload.ImageOptions.Image")));
            this.btnUpload.Location = new System.Drawing.Point(979, 643);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Padding = new System.Windows.Forms.Padding(2);
            this.btnUpload.Size = new System.Drawing.Size(383, 32);
            this.btnUpload.StyleController = this.layoutControl2;
            this.btnUpload.TabIndex = 225;
            this.btnUpload.Text = "업로드";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // gcCodeDocumentFile
            // 
            this.gcCodeDocumentFile.DataSource = this.codeDocumentFileBindingSource;
            this.gcCodeDocumentFile.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcCodeDocumentFile.Location = new System.Drawing.Point(979, 683);
            this.gcCodeDocumentFile.MainView = this.gvCodeDocumentFile;
            this.gcCodeDocumentFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcCodeDocumentFile.Name = "gcCodeDocumentFile";
            this.gcCodeDocumentFile.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnFileDelete,
            this.itemCodeColLookUpEdit});
            this.gcCodeDocumentFile.Size = new System.Drawing.Size(481, 310);
            this.gcCodeDocumentFile.TabIndex = 224;
            this.gcCodeDocumentFile.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCodeDocumentFile});
            // 
            // gvCodeDocumentFile
            // 
            this.gvCodeDocumentFile.ColumnPanelRowHeight = 25;
            this.gvCodeDocumentFile.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colGubun,
            this.colKeyCode1,
            this.colFileSeq,
            this.colItemCode,
            this.colTitle3,
            this.colFileName,
            this.colPath,
            this.colCapacity,
            this.colFileExt,
            this.gridColumn9,
            this.colOriFileName});
            this.gvCodeDocumentFile.FixedLineWidth = 3;
            this.gvCodeDocumentFile.GridControl = this.gcCodeDocumentFile;
            this.gvCodeDocumentFile.Name = "gvCodeDocumentFile";
            this.gvCodeDocumentFile.OptionsSelection.CheckBoxSelectorColumnWidth = 23;
            this.gvCodeDocumentFile.OptionsSelection.MultiSelect = true;
            this.gvCodeDocumentFile.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvCodeDocumentFile.OptionsView.ColumnAutoWidth = false;
            this.gvCodeDocumentFile.OptionsView.ShowGroupPanel = false;
            this.gvCodeDocumentFile.RowHeight = 30;
            // 
            // gridColumn1
            // 
            this.gridColumn1.FieldName = "CorpId";
            this.gridColumn1.MinWidth = 19;
            this.gridColumn1.Name = "gridColumn1";
            // 
            // colGubun
            // 
            this.colGubun.FieldName = "Gubun";
            this.colGubun.MinWidth = 19;
            this.colGubun.Name = "colGubun";
            // 
            // colKeyCode1
            // 
            this.colKeyCode1.Caption = "gridColumn3";
            this.colKeyCode1.FieldName = "KeyCode1";
            this.colKeyCode1.MinWidth = 19;
            this.colKeyCode1.Name = "colKeyCode1";
            // 
            // colFileSeq
            // 
            this.colFileSeq.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileSeq.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileSeq.Caption = "순번";
            this.colFileSeq.FieldName = "FileSeq";
            this.colFileSeq.MinWidth = 19;
            this.colFileSeq.Name = "colFileSeq";
            this.colFileSeq.OptionsColumn.AllowFocus = false;
            this.colFileSeq.OptionsColumn.FixedWidth = true;
            this.colFileSeq.Visible = true;
            this.colFileSeq.VisibleIndex = 1;
            this.colFileSeq.Width = 40;
            // 
            // colItemCode
            // 
            this.colItemCode.Caption = "파일분류";
            this.colItemCode.ColumnEdit = this.itemCodeColLookUpEdit;
            this.colItemCode.FieldName = "ItemCode";
            this.colItemCode.MinWidth = 19;
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.Width = 80;
            // 
            // itemCodeColLookUpEdit
            // 
            this.itemCodeColLookUpEdit.AutoHeight = false;
            this.itemCodeColLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.itemCodeColLookUpEdit.Name = "itemCodeColLookUpEdit";
            this.itemCodeColLookUpEdit.NullText = "";
            // 
            // colTitle3
            // 
            this.colTitle3.Caption = "문서명";
            this.colTitle3.FieldName = "Title";
            this.colTitle3.MinWidth = 19;
            this.colTitle3.Name = "colTitle3";
            this.colTitle3.Visible = true;
            this.colTitle3.VisibleIndex = 2;
            this.colTitle3.Width = 350;
            // 
            // colFileName
            // 
            this.colFileName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileName.Caption = "파일명";
            this.colFileName.FieldName = "FileName";
            this.colFileName.MinWidth = 19;
            this.colFileName.Name = "colFileName";
            this.colFileName.OptionsColumn.AllowFocus = false;
            this.colFileName.Width = 99;
            // 
            // colPath
            // 
            this.colPath.AppearanceHeader.Options.UseTextOptions = true;
            this.colPath.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPath.Caption = "파일경로";
            this.colPath.FieldName = "Path";
            this.colPath.MinWidth = 19;
            this.colPath.Name = "colPath";
            this.colPath.OptionsColumn.AllowFocus = false;
            this.colPath.Width = 120;
            // 
            // colCapacity
            // 
            this.colCapacity.FieldName = "Capacity";
            this.colCapacity.MinWidth = 19;
            this.colCapacity.Name = "colCapacity";
            // 
            // colFileExt
            // 
            this.colFileExt.FieldName = "FileExt";
            this.colFileExt.MinWidth = 19;
            this.colFileExt.Name = "colFileExt";
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn9.Caption = "삭제";
            this.gridColumn9.ColumnEdit = this.btnFileDelete;
            this.gridColumn9.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.gridColumn9.MinWidth = 19;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.FixedWidth = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 3;
            this.gridColumn9.Width = 45;
            // 
            // btnFileDelete
            // 
            this.btnFileDelete.AutoHeight = false;
            this.btnFileDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.btnFileDelete.Name = "btnFileDelete";
            this.btnFileDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btnFileDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnFileDelete_ButtonClick);
            // 
            // colOriFileName
            // 
            this.colOriFileName.FieldName = "OriFileName";
            this.colOriFileName.MinWidth = 19;
            this.colOriFileName.Name = "colOriFileName";
            this.colOriFileName.OptionsColumn.AllowFocus = false;
            // 
            // productImagePictureEdit
            // 
            this.productImagePictureEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "ProductImage", true));
            this.productImagePictureEdit.Location = new System.Drawing.Point(966, 384);
            this.productImagePictureEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productImagePictureEdit.Name = "productImagePictureEdit";
            this.productImagePictureEdit.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.productImagePictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.productImagePictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.productImagePictureEdit.Size = new System.Drawing.Size(507, 20);
            this.productImagePictureEdit.StyleController = this.layoutControl2;
            this.productImagePictureEdit.TabIndex = 73;
            this.productImagePictureEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // corpIDTextEdit
            // 
            this.corpIDTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "CorpID", true));
            this.corpIDTextEdit.Location = new System.Drawing.Point(1051, 38);
            this.corpIDTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.corpIDTextEdit.Name = "corpIDTextEdit";
            this.corpIDTextEdit.Size = new System.Drawing.Size(267, 32);
            this.corpIDTextEdit.StyleController = this.layoutControl2;
            this.corpIDTextEdit.TabIndex = 54;
            this.corpIDTextEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // projectIDTextEdit
            // 
            this.projectIDTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "ProjectID", true));
            this.projectIDTextEdit.Location = new System.Drawing.Point(1409, 38);
            this.projectIDTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.projectIDTextEdit.Name = "projectIDTextEdit";
            this.projectIDTextEdit.Size = new System.Drawing.Size(64, 32);
            this.projectIDTextEdit.StyleController = this.layoutControl2;
            this.projectIDTextEdit.TabIndex = 56;
            // 
            // projectNameTextEdit
            // 
            this.projectNameTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "ProjectName", true));
            this.projectNameTextEdit.Location = new System.Drawing.Point(1051, 118);
            this.projectNameTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.projectNameTextEdit.Name = "projectNameTextEdit";
            this.projectNameTextEdit.Size = new System.Drawing.Size(422, 32);
            this.projectNameTextEdit.StyleController = this.layoutControl2;
            this.projectNameTextEdit.TabIndex = 66;
            this.projectNameTextEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // startDateDateEdit
            // 
            this.startDateDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "StartDate", true));
            this.startDateDateEdit.EditValue = null;
            this.startDateDateEdit.Location = new System.Drawing.Point(1051, 280);
            this.startDateDateEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startDateDateEdit.Name = "startDateDateEdit";
            this.startDateDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.startDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.startDateDateEdit.Size = new System.Drawing.Size(167, 32);
            this.startDateDateEdit.StyleController = this.layoutControl2;
            this.startDateDateEdit.TabIndex = 68;
            this.startDateDateEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // endDateDateEdit
            // 
            this.endDateDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "EndDate", true));
            this.endDateDateEdit.EditValue = null;
            this.endDateDateEdit.Location = new System.Drawing.Point(1309, 280);
            this.endDateDateEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.endDateDateEdit.Name = "endDateDateEdit";
            this.endDateDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endDateDateEdit.Size = new System.Drawing.Size(164, 32);
            this.endDateDateEdit.StyleController = this.layoutControl2;
            this.endDateDateEdit.TabIndex = 70;
            this.endDateDateEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // gcProjectMaster
            // 
            this.gcProjectMaster.DataSource = this.projectMasterBindingSource;
            this.gcProjectMaster.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gcProjectMaster.Location = new System.Drawing.Point(9, 38);
            this.gcProjectMaster.MainView = this.gvProjectMaster;
            this.gcProjectMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcProjectMaster.Name = "gcProjectMaster";
            this.gcProjectMaster.Size = new System.Drawing.Size(925, 969);
            this.gcProjectMaster.TabIndex = 14;
            this.gcProjectMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProjectMaster});
            // 
            // gvProjectMaster
            // 
            this.gvProjectMaster.DetailHeight = 425;
            this.gvProjectMaster.GridControl = this.gcProjectMaster;
            this.gvProjectMaster.Name = "gvProjectMaster";
            this.gvProjectMaster.OptionsDetail.EnableMasterViewMode = false;
            this.gvProjectMaster.OptionsView.RowAutoHeight = true;
            this.gvProjectMaster.OptionsView.ShowAutoFilterRow = true;
            this.gvProjectMaster.OptionsView.ShowGroupPanel = false;
            this.gvProjectMaster.RowHeight = 30;
            // 
            // remarkTextEdit
            // 
            this.remarkTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "Remark", true));
            this.remarkTextEdit.Location = new System.Drawing.Point(1051, 320);
            this.remarkTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.remarkTextEdit.Name = "remarkTextEdit";
            this.remarkTextEdit.Size = new System.Drawing.Size(422, 56);
            this.remarkTextEdit.StyleController = this.layoutControl2;
            this.remarkTextEdit.TabIndex = 72;
            this.remarkTextEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // classTextEdit
            // 
            this.classTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "CLASS", true));
            this.classTextEdit.Location = new System.Drawing.Point(1051, 78);
            this.classTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classTextEdit.Name = "classTextEdit";
            this.classTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.classTextEdit.Properties.NullText = "";
            this.classTextEdit.Properties.PopupView = this.searchLookUpEdit1View;
            this.classTextEdit.Properties.UseReadOnlyAppearance = false;
            this.classTextEdit.Size = new System.Drawing.Size(422, 32);
            this.classTextEdit.StyleController = this.layoutControl2;
            this.classTextEdit.TabIndex = 58;
            this.classTextEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 425;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // brandTextEdit
            // 
            this.brandTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "Brand", true));
            this.brandTextEdit.Location = new System.Drawing.Point(1051, 532);
            this.brandTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.brandTextEdit.Name = "brandTextEdit";
            this.brandTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.brandTextEdit.Properties.NullText = "";
            this.brandTextEdit.Properties.PopupView = this.gridView1;
            this.brandTextEdit.Size = new System.Drawing.Size(422, 32);
            this.brandTextEdit.StyleController = this.layoutControl2;
            this.brandTextEdit.TabIndex = 227;
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 425;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // productYearTextEdit
            // 
            this.productYearTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "ProductYear", true));
            this.productYearTextEdit.Location = new System.Drawing.Point(1051, 452);
            this.productYearTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productYearTextEdit.Name = "productYearTextEdit";
            this.productYearTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.productYearTextEdit.Properties.NullText = "";
            this.productYearTextEdit.Properties.PopupView = this.gridView2;
            this.productYearTextEdit.Size = new System.Drawing.Size(422, 32);
            this.productYearTextEdit.StyleController = this.layoutControl2;
            this.productYearTextEdit.TabIndex = 228;
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 425;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // seasonTextEdit
            // 
            this.seasonTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "Season", true));
            this.seasonTextEdit.Location = new System.Drawing.Point(1051, 240);
            this.seasonTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.seasonTextEdit.Name = "seasonTextEdit";
            this.seasonTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.seasonTextEdit.Properties.NullText = "";
            this.seasonTextEdit.Properties.PopupView = this.gridView3;
            this.seasonTextEdit.Size = new System.Drawing.Size(167, 32);
            this.seasonTextEdit.StyleController = this.layoutControl2;
            this.seasonTextEdit.TabIndex = 229;
            this.seasonTextEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.DetailHeight = 425;
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // groupNameTextEdit
            // 
            this.groupNameTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "GroupName", true));
            this.groupNameTextEdit.Location = new System.Drawing.Point(1051, 492);
            this.groupNameTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupNameTextEdit.Name = "groupNameTextEdit";
            this.groupNameTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.groupNameTextEdit.Properties.NullText = "";
            this.groupNameTextEdit.Properties.PopupView = this.gridView4;
            this.groupNameTextEdit.Size = new System.Drawing.Size(422, 32);
            this.groupNameTextEdit.StyleController = this.layoutControl2;
            this.groupNameTextEdit.TabIndex = 230;
            // 
            // gridView4
            // 
            this.gridView4.DetailHeight = 425;
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // chargeSearchLookUpEdit
            // 
            this.chargeSearchLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "Charge", true));
            this.chargeSearchLookUpEdit.Location = new System.Drawing.Point(1051, 200);
            this.chargeSearchLookUpEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chargeSearchLookUpEdit.Name = "chargeSearchLookUpEdit";
            this.chargeSearchLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chargeSearchLookUpEdit.Properties.NullText = "";
            this.chargeSearchLookUpEdit.Properties.PopupView = this.gridView5;
            this.chargeSearchLookUpEdit.Size = new System.Drawing.Size(167, 32);
            this.chargeSearchLookUpEdit.StyleController = this.layoutControl2;
            this.chargeSearchLookUpEdit.TabIndex = 62;
            this.chargeSearchLookUpEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // gridView5
            // 
            this.gridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView5.OptionsView.ShowGroupPanel = false;
            // 
            // pMNameSearchLookUpEidit
            // 
            this.pMNameSearchLookUpEidit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "PMName", true));
            this.pMNameSearchLookUpEidit.Location = new System.Drawing.Point(1309, 200);
            this.pMNameSearchLookUpEidit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pMNameSearchLookUpEidit.Name = "pMNameSearchLookUpEidit";
            this.pMNameSearchLookUpEidit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.pMNameSearchLookUpEidit.Properties.NullText = "";
            this.pMNameSearchLookUpEidit.Properties.PopupView = this.gridView6;
            this.pMNameSearchLookUpEidit.Size = new System.Drawing.Size(164, 32);
            this.pMNameSearchLookUpEidit.StyleController = this.layoutControl2;
            this.pMNameSearchLookUpEidit.TabIndex = 64;
            this.pMNameSearchLookUpEidit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // gridView6
            // 
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // typeTextEdit
            // 
            this.typeTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.projectMasterBindingSource, "Type", true));
            this.typeTextEdit.Location = new System.Drawing.Point(1051, 412);
            this.typeTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.typeTextEdit.Name = "typeTextEdit";
            this.typeTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.typeTextEdit.Properties.Items.AddRange(new object[] {
            "Solid",
            "Print"});
            this.typeTextEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.typeTextEdit.Size = new System.Drawing.Size(422, 32);
            this.typeTextEdit.StyleController = this.layoutControl2;
            this.typeTextEdit.TabIndex = 60;
            // 
            // productYearDateEdit
            // 
            this.productYearDateEdit.EditValue = null;
            this.productYearDateEdit.Location = new System.Drawing.Point(1309, 240);
            this.productYearDateEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productYearDateEdit.Name = "productYearDateEdit";
            this.productYearDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.productYearDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.productYearDateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.productYearDateEdit.Properties.DisplayFormat.FormatString = "yyyy";
            this.productYearDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.productYearDateEdit.Properties.EditFormat.FormatString = "yyyy";
            this.productYearDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.productYearDateEdit.Properties.MaskSettings.Set("mask", "yyyy");
            this.productYearDateEdit.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;
            this.productYearDateEdit.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.productYearDateEdit.Size = new System.Drawing.Size(164, 32);
            this.productYearDateEdit.StyleController = this.layoutControl2;
            this.productYearDateEdit.TabIndex = 68;
            this.productYearDateEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // materialSearchLookUpEidit
            // 
            this.materialSearchLookUpEidit.Location = new System.Drawing.Point(1051, 159);
            this.materialSearchLookUpEidit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialSearchLookUpEidit.Name = "materialSearchLookUpEidit";
            this.materialSearchLookUpEidit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.materialSearchLookUpEidit.Properties.NullText = "";
            this.materialSearchLookUpEidit.Properties.PopupView = this.gridView51;
            this.materialSearchLookUpEidit.Size = new System.Drawing.Size(422, 32);
            this.materialSearchLookUpEidit.StyleController = this.layoutControl2;
            this.materialSearchLookUpEidit.TabIndex = 62;
            this.materialSearchLookUpEidit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // gridView51
            // 
            this.gridView51.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView51.Name = "gridView51";
            this.gridView51.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView51.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.splitterItem1,
            this.splitterItem2,
            this.tabbedControlGroup1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1482, 1018);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.layoutControlItem12,
            this.layoutControlItem18,
            this.layoutControlItem22,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem14,
            this.layoutControlItem20,
            this.layoutControlItem17,
            this.layoutControlItem16,
            this.layoutControlItem13,
            this.layoutControlItem10,
            this.layoutControlItem19,
            this.layoutControlItem15,
            this.layoutControlItem25,
            this.layoutControlItem26});
            this.layoutControlGroup2.Location = new System.Drawing.Point(957, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup1";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup2.Size = new System.Drawing.Size(523, 573);
            this.layoutControlGroup2.Text = "프로젝트 상세내역";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.corpIDTextEdit;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(358, 40);
            this.layoutControlItem4.Text = "Corp ID:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.classTextEdit;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(513, 40);
            this.layoutControlItem8.Text = "분류";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.chargeSearchLookUpEdit;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 162);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(258, 40);
            this.layoutControlItem12.Text = "담당자";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.startDateDateEdit;
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 242);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(258, 40);
            this.layoutControlItem18.Text = "시작일";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.remarkTextEdit;
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 282);
            this.layoutControlItem22.MaxSize = new System.Drawing.Size(0, 64);
            this.layoutControlItem22.MinSize = new System.Drawing.Size(97, 64);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(513, 64);
            this.layoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem22.Text = "비고사항";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.productImagePictureEdit;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 346);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(513, 28);
            this.layoutControlItem5.Text = "Product Image:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.projectIDTextEdit;
            this.layoutControlItem6.Location = new System.Drawing.Point(358, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(155, 40);
            this.layoutControlItem6.Text = "Project ID:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.pMNameSearchLookUpEidit;
            this.layoutControlItem14.Location = new System.Drawing.Point(258, 162);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(255, 40);
            this.layoutControlItem14.Text = "총책임자";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.endDateDateEdit;
            this.layoutControlItem20.Location = new System.Drawing.Point(258, 242);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(255, 40);
            this.layoutControlItem20.Text = "종료일";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.seasonTextEdit;
            this.layoutControlItem17.Location = new System.Drawing.Point(0, 202);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(258, 40);
            this.layoutControlItem17.Text = "시즌";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.projectNameTextEdit;
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 80);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(513, 40);
            this.layoutControlItem16.Text = "프로젝트명";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.brandTextEdit;
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 494);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(513, 40);
            this.layoutControlItem13.Text = "브랜드";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem13.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.typeTextEdit;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 374);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(513, 40);
            this.layoutControlItem10.Text = "항목";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.groupNameTextEdit;
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 454);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(513, 40);
            this.layoutControlItem19.Text = "중분류";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.productYearTextEdit;
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 414);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(513, 40);
            this.layoutControlItem15.Text = "연도";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem15.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.productYearDateEdit;
            this.layoutControlItem25.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem25.CustomizationFormText = "시작일";
            this.layoutControlItem25.Location = new System.Drawing.Point(258, 202);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(255, 40);
            this.layoutControlItem25.Text = "연도";
            this.layoutControlItem25.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.materialSearchLookUpEidit;
            this.layoutControlItem26.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem26.CustomizationFormText = "품명";
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 120);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 5, 5);
            this.layoutControlItem26.Size = new System.Drawing.Size(513, 42);
            this.layoutControlItem26.Text = "품명";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup2";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup3.Size = new System.Drawing.Size(941, 1016);
            this.layoutControlGroup3.Text = "프로젝트 내역조회";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcProjectMaster;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(931, 977);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(941, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(16, 1016);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(957, 573);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(523, 16);
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.Location = new System.Drawing.Point(957, 589);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.layoutControlGroup4;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(523, 427);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup4,
            this.layoutCtlGrpFile});
            this.tabbedControlGroup1.Text = "항목 리스트";
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11,
            this.layoutControlItem23});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup4.Name = "layoutControlGroup3";
            this.layoutControlGroup4.Size = new System.Drawing.Size(487, 358);
            this.layoutControlGroup4.Text = "프로젝트 멤버";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcProjectMember;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(487, 318);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.panel2;
            this.layoutControlItem23.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem23.MaxSize = new System.Drawing.Size(0, 40);
            this.layoutControlItem23.MinSize = new System.Drawing.Size(258, 40);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(487, 40);
            this.layoutControlItem23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem23.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem23.TextVisible = false;
            // 
            // layoutCtlGrpFile
            // 
            buttonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("buttonImageOptions1.Image")));
            this.layoutCtlGrpFile.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("저장", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1)});
            this.layoutCtlGrpFile.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.layoutCtlGrpFile.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem7,
            this.layoutControlItem9});
            this.layoutCtlGrpFile.Location = new System.Drawing.Point(0, 0);
            this.layoutCtlGrpFile.Name = "layoutCtlGrpFile";
            this.layoutCtlGrpFile.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutCtlGrpFile.Size = new System.Drawing.Size(487, 358);
            this.layoutCtlGrpFile.Text = "해당 디자인 파일 업로드";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcCodeDocumentFile;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(487, 318);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnUpload;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(389, 40);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnDownload;
            this.layoutControlItem9.Location = new System.Drawing.Point(389, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(98, 40);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1488, 1026);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.layoutControl2;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1488, 1026);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 1066);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmDesign";
            this.Load += new System.EventHandler(this.frmDesignProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projectMemberBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.designDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeDocumentFileBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.codeDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectMasterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectMember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectMember)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchRole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchPermission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit3View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCodeDocumentFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCodeDocumentFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemCodeColLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFileDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productImagePictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corpIDTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectIDTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectNameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startDateDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startDateDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDateDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDateDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.remarkTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.classTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brandTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productYearTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seasonTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupNameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chargeSearchLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMNameSearchLookUpEidit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.typeTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productYearDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productYearDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialSearchLookUpEidit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView51)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCtlGrpFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn colCorpID;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectID;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DesignDataSet designDataSet;
        private System.Windows.Forms.BindingSource projectMasterBindingSource;
        private DesignDataSetTableAdapters.ProjectMasterTableAdapter projectMasterTableAdapter;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;
        private System.Windows.Forms.OpenFileDialog foDialog;
        private CodeDataSet codeDataSet;
        private System.Windows.Forms.BindingSource codeDocumentFileBindingSource;
        private CodeDataSetTableAdapters.CodeDocumentFileTableAdapter codeDocumentFileTableAdapter;
        private System.Windows.Forms.BindingSource projectMemberBindingSource;
        private DesignDataSetTableAdapters.ProjectMemberTableAdapter projectMemberTableAdapter;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton smtSearchButton4;
        private baseSMT.smtSearchButton cmdSave;
        private baseSMT.smtSearchButton cmdDelete;
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchDatetime Dt_Date;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMemberAdd;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit3;
        private DevExpress.XtraGrid.GridControl gcProjectMember;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProjectMember;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit searchEmployee;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView8;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit searchRole;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit2View;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit searchPermission;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit3View;
        private DevExpress.XtraEditors.SimpleButton btnDownload;
        private DevExpress.XtraEditors.SimpleButton btnUpload;
        private DevExpress.XtraGrid.GridControl gcCodeDocumentFile;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCodeDocumentFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colGubun;
        private DevExpress.XtraGrid.Columns.GridColumn colKeyCode1;
        private DevExpress.XtraGrid.Columns.GridColumn colFileSeq;
        private DevExpress.XtraGrid.Columns.GridColumn colItemCode;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit itemCodeColLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle3;
        private DevExpress.XtraGrid.Columns.GridColumn colFileName;
        private DevExpress.XtraGrid.Columns.GridColumn colPath;
        private DevExpress.XtraGrid.Columns.GridColumn colCapacity;
        private DevExpress.XtraGrid.Columns.GridColumn colFileExt;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnFileDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colOriFileName;
        private DevExpress.XtraEditors.PictureEdit productImagePictureEdit;
        private DevExpress.XtraEditors.TextEdit corpIDTextEdit;
        private DevExpress.XtraEditors.TextEdit projectIDTextEdit;
        private DevExpress.XtraEditors.TextEdit projectNameTextEdit;
        private DevExpress.XtraEditors.DateEdit startDateDateEdit;
        private DevExpress.XtraEditors.DateEdit endDateDateEdit;
        private DevExpress.XtraGrid.GridControl gcProjectMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProjectMaster;
        private DevExpress.XtraEditors.MemoEdit remarkTextEdit;
        private DevExpress.XtraEditors.SearchLookUpEdit classTextEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.SearchLookUpEdit brandTextEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SearchLookUpEdit productYearTextEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SearchLookUpEdit seasonTextEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraEditors.SearchLookUpEdit groupNameTextEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraEditors.SearchLookUpEdit chargeSearchLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.SearchLookUpEdit pMNameSearchLookUpEidit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraEditors.ComboBoxEdit typeTextEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlGroup layoutCtlGrpFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DateEdit productYearDateEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraEditors.SearchLookUpEdit materialSearchLookUpEidit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView51;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
    }
}