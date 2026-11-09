namespace SmartDesign.View.Project
{
    partial class frmDesignProject
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dateEndDate = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateStartDate = new DevExpress.XtraEditors.DateEdit();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.gcSchedule = new DevExpress.XtraGrid.GridControl();
            this.projectMasterBindingSource = new System.Windows.Forms.BindingSource();
            this.designDataSet = new SmartDesign.DesignDataSet();
            this.gvSchedule = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCorpID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectID1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCharge = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClass1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.projectMasterTableAdapter = new SmartDesign.DesignDataSetTableAdapters.ProjectMasterTableAdapter();
            this.tableAdapterManager = new SmartDesign.DesignDataSetTableAdapters.TableAdapterManager();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectMasterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.designDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panel1);
            this.layoutControl1.Controls.Add(this.gcSchedule);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1023, 707);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cmdInsert);
            this.panel1.Controls.Add(this.cmdSearch);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdDelete);
            this.panel1.Controls.Add(this.cmdClose);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.pictureEdit1);
            this.panel1.Location = new System.Drawing.Point(11, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 34);
            this.panel1.TabIndex = 53;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("돋움", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::SmartDesign.Properties.Resources.add1_16X16;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(637, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 32);
            this.button1.TabIndex = 34;
            this.button1.Text = "     작업지시";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdInsert.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.cmdInsert.FlatAppearance.BorderSize = 0;
            this.cmdInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInsert.Font = new System.Drawing.Font("돋움", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert.ForeColor = System.Drawing.Color.White;
            this.cmdInsert.Image = global::SmartDesign.Properties.Resources.add1_16X16;
            this.cmdInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInsert.Location = new System.Drawing.Point(719, 0);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(70, 32);
            this.cmdInsert.TabIndex = 33;
            this.cmdInsert.Text = "     추가";
            this.cmdInsert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.cmdSearch.FlatAppearance.BorderSize = 0;
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("돋움", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.ForeColor = System.Drawing.Color.White;
            this.cmdSearch.Image = global::SmartDesign.Properties.Resources.Search_16px;
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(505, 0);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(70, 32);
            this.cmdSearch.TabIndex = 31;
            this.cmdSearch.Text = "     조회";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dateEndDate);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.dateStartDate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(156, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(349, 32);
            this.panel3.TabIndex = 30;
            // 
            // dateEndDate
            // 
            this.dateEndDate.EditValue = new System.DateTime(2089, 12, 31, 23, 59, 59, 0);
            this.dateEndDate.Location = new System.Drawing.Point(223, 6);
            this.dateEndDate.Name = "dateEndDate";
            this.dateEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEndDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEndDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEndDate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.dateEndDate.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.dateEndDate.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.dateEndDate.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)(((((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView) 
            | DevExpress.XtraEditors.VistaCalendarViewStyle.QuarterView) 
            | DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView) 
            | DevExpress.XtraEditors.VistaCalendarViewStyle.CenturyView)));
            this.dateEndDate.Size = new System.Drawing.Size(100, 20);
            this.dateEndDate.StyleController = this.layoutControl1;
            this.dateEndDate.TabIndex = 49;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(176, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 48;
            this.label1.Text = "종료일";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("돋움", 9F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 47;
            this.label3.Text = "시작일";
            // 
            // dateStartDate
            // 
            this.dateStartDate.EditValue = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.dateStartDate.Location = new System.Drawing.Point(53, 6);
            this.dateStartDate.Name = "dateStartDate";
            this.dateStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStartDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateStartDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateStartDate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.dateStartDate.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.dateStartDate.Properties.MaskSettings.Set("mask", "yyyy-MM-dd");
            this.dateStartDate.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)(((((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView) 
            | DevExpress.XtraEditors.VistaCalendarViewStyle.QuarterView) 
            | DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView) 
            | DevExpress.XtraEditors.VistaCalendarViewStyle.CenturyView)));
            this.dateStartDate.Size = new System.Drawing.Size(100, 20);
            this.dateStartDate.StyleController = this.layoutControl1;
            this.dateStartDate.TabIndex = 45;
            // 
            // cmdSave
            // 
            this.cmdSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdSave.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.cmdSave.FlatAppearance.BorderSize = 0;
            this.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSave.Font = new System.Drawing.Font("돋움", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.ForeColor = System.Drawing.Color.White;
            this.cmdSave.Image = global::SmartDesign.Properties.Resources.Save_as_16px;
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(789, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(70, 32);
            this.cmdSave.TabIndex = 27;
            this.cmdSave.Text = "     저장";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdDelete.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.cmdDelete.FlatAppearance.BorderSize = 0;
            this.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDelete.Font = new System.Drawing.Font("돋움", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.ForeColor = System.Drawing.Color.White;
            this.cmdDelete.Image = global::SmartDesign.Properties.Resources.cut1_16X16;
            this.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDelete.Location = new System.Drawing.Point(859, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(70, 32);
            this.cmdDelete.TabIndex = 25;
            this.cmdDelete.Text = "     삭제";
            this.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdClose.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.cmdClose.FlatAppearance.BorderSize = 0;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Font = new System.Drawing.Font("돋움", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.ForeColor = System.Drawing.Color.White;
            this.cmdClose.Image = global::SmartDesign.Properties.Resources.close;
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(929, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(70, 32);
            this.cmdClose.TabIndex = 16;
            this.cmdClose.Text = "     닫기";
            this.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.Location = new System.Drawing.Point(32, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(124, 32);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = " 프로젝트 관리";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureEdit1.EditValue = global::SmartDesign.Properties.Resources.folder_24X24;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Size = new System.Drawing.Size(32, 32);
            this.pictureEdit1.TabIndex = 7;
            // 
            // gcSchedule
            // 
            this.gcSchedule.DataSource = this.projectMasterBindingSource;
            this.gcSchedule.Location = new System.Drawing.Point(11, 48);
            this.gcSchedule.MainView = this.gvSchedule;
            this.gcSchedule.Name = "gcSchedule";
            this.gcSchedule.Size = new System.Drawing.Size(1001, 649);
            this.gcSchedule.TabIndex = 14;
            this.gcSchedule.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSchedule});
            // 
            // projectMasterBindingSource
            // 
            this.projectMasterBindingSource.DataMember = "ProjectMaster";
            this.projectMasterBindingSource.DataSource = this.designDataSet;
            // 
            // designDataSet
            // 
            this.designDataSet.DataSetName = "DesignDataSet";
            this.designDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gvSchedule
            // 
            this.gvSchedule.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCorpID1,
            this.colProjectID1,
            this.colType1,
            this.colProjectName1,
            this.colStartDate1,
            this.colEndDate1,
            this.colCharge,
            this.colClass1});
            this.gvSchedule.GridControl = this.gcSchedule;
            this.gvSchedule.Name = "gvSchedule";
            this.gvSchedule.OptionsView.RowAutoHeight = true;
            this.gvSchedule.OptionsView.ShowAutoFilterRow = true;
            this.gvSchedule.OptionsView.ShowGroupPanel = false;
            this.gvSchedule.RowHeight = 25;
            // 
            // colCorpID1
            // 
            this.colCorpID1.Caption = "사업장번호";
            this.colCorpID1.FieldName = "CorpID";
            this.colCorpID1.Name = "colCorpID1";
            this.colCorpID1.OptionsColumn.AllowEdit = false;
            this.colCorpID1.Visible = true;
            this.colCorpID1.VisibleIndex = 0;
            this.colCorpID1.Width = 66;
            // 
            // colProjectID1
            // 
            this.colProjectID1.Caption = "프로젝트ID";
            this.colProjectID1.FieldName = "ProjectID";
            this.colProjectID1.Name = "colProjectID1";
            this.colProjectID1.Visible = true;
            this.colProjectID1.VisibleIndex = 1;
            this.colProjectID1.Width = 66;
            // 
            // colType1
            // 
            this.colType1.Caption = "항목";
            this.colType1.FieldName = "Type";
            this.colType1.Name = "colType1";
            this.colType1.Visible = true;
            this.colType1.VisibleIndex = 3;
            this.colType1.Width = 66;
            // 
            // colProjectName1
            // 
            this.colProjectName1.Caption = "프로젝트명";
            this.colProjectName1.FieldName = "ProjectName";
            this.colProjectName1.Name = "colProjectName1";
            this.colProjectName1.Visible = true;
            this.colProjectName1.VisibleIndex = 4;
            this.colProjectName1.Width = 66;
            // 
            // colStartDate1
            // 
            this.colStartDate1.Caption = "시작일";
            this.colStartDate1.FieldName = "StartDate";
            this.colStartDate1.Name = "colStartDate1";
            this.colStartDate1.Visible = true;
            this.colStartDate1.VisibleIndex = 5;
            this.colStartDate1.Width = 66;
            // 
            // colEndDate1
            // 
            this.colEndDate1.Caption = "종료일";
            this.colEndDate1.FieldName = "EndDate";
            this.colEndDate1.Name = "colEndDate1";
            this.colEndDate1.Visible = true;
            this.colEndDate1.VisibleIndex = 6;
            this.colEndDate1.Width = 66;
            // 
            // colCharge
            // 
            this.colCharge.Caption = "담당자";
            this.colCharge.FieldName = "Charge";
            this.colCharge.Name = "colCharge";
            this.colCharge.Visible = true;
            this.colCharge.VisibleIndex = 7;
            this.colCharge.Width = 66;
            // 
            // colClass1
            // 
            this.colClass1.Caption = "CLASS";
            this.colClass1.FieldName = "CLASS";
            this.colClass1.Name = "colClass1";
            this.colClass1.Visible = true;
            this.colClass1.VisibleIndex = 2;
            this.colClass1.Width = 66;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1023, 707);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcSchedule;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1005, 653);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panel1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 38);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 38);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1005, 38);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // projectMasterTableAdapter
            // 
            this.projectMasterTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ProjectDetailTableAdapter = null;
            this.tableAdapterManager.ProjectMasterTableAdapter = this.projectMasterTableAdapter;
            this.tableAdapterManager.ProjectMemoTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = SmartDesign.DesignDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // frmDesignProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 707);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmDesignProject";
            this.Text = "frmDesignProject";
            this.Load += new System.EventHandler(this.frmDesignProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectMasterBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.designDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdClose;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DateEdit dateStartDate;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit dateEndDate;
        private DevExpress.XtraGrid.GridControl gcSchedule;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSchedule;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DesignDataSet designDataSet;
        private System.Windows.Forms.BindingSource projectMasterBindingSource;
        private DesignDataSetTableAdapters.ProjectMasterTableAdapter projectMasterTableAdapter;
        private DesignDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpID1;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectID1;
        private DevExpress.XtraGrid.Columns.GridColumn colType1;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectName1;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate1;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate1;
        private DevExpress.XtraGrid.Columns.GridColumn colCharge;
        private DevExpress.XtraGrid.Columns.GridColumn colClass1;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Button button1;
    }
}