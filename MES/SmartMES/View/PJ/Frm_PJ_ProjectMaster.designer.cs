using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.PJ
{
    partial class Frm_PJ_ProjectMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PJ_ProjectMaster));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.colCorpID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.foDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.PlanStartDateDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.PlanEnddateEdit = new DevExpress.XtraEditors.DateEdit();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
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
            this.chargeSearchLookUpEdit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pMNameSearchLookUpEidit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.materialSearchLookUpEidit = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView51 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.tabbedControlGroup1 = new DevExpress.XtraLayout.TabbedControlGroup();
            this.layoutCtlGrpFile = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PlanStartDateDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanStartDateDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanEnddateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanEnddateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.chargeSearchLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMNameSearchLookUpEidit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialSearchLookUpEidit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView51)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCtlGrpFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
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
            // foDialog
            // 
            this.foDialog.FileName = "openFileDialog1";
            this.foDialog.Multiselect = true;
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
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
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
            this.Dt_Date.smtLabelForeColor = System.Drawing.Color.White;
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
            this.layoutControl2.Controls.Add(this.PlanStartDateDateEdit);
            this.layoutControl2.Controls.Add(this.PlanEnddateEdit);
            this.layoutControl2.Controls.Add(this.dataLayoutControl1);
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
            this.layoutControl2.Controls.Add(this.chargeSearchLookUpEdit);
            this.layoutControl2.Controls.Add(this.pMNameSearchLookUpEidit);
            this.layoutControl2.Controls.Add(this.materialSearchLookUpEidit);
            this.layoutControl2.Location = new System.Drawing.Point(3, 3);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1482, 1020);
            this.layoutControl2.TabIndex = 4;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // PlanStartDateDateEdit
            // 
            this.PlanStartDateDateEdit.EditValue = null;
            this.PlanStartDateDateEdit.Location = new System.Drawing.Point(1049, 268);
            this.PlanStartDateDateEdit.Name = "PlanStartDateDateEdit";
            this.PlanStartDateDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PlanStartDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PlanStartDateDateEdit.Size = new System.Drawing.Size(169, 32);
            this.PlanStartDateDateEdit.StyleController = this.layoutControl2;
            this.PlanStartDateDateEdit.TabIndex = 229;
            // 
            // PlanEnddateEdit
            // 
            this.PlanEnddateEdit.EditValue = null;
            this.PlanEnddateEdit.Location = new System.Drawing.Point(1307, 268);
            this.PlanEnddateEdit.Name = "PlanEnddateEdit";
            this.PlanEnddateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PlanEnddateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PlanEnddateEdit.Size = new System.Drawing.Size(166, 32);
            this.PlanEnddateEdit.StyleController = this.layoutControl2;
            this.PlanEnddateEdit.TabIndex = 228;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Location = new System.Drawing.Point(4, 990);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup4;
            this.dataLayoutControl1.Size = new System.Drawing.Size(1474, 26);
            this.dataLayoutControl1.TabIndex = 227;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(1474, 26);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // btnDownload
            // 
            this.btnDownload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.ImageOptions.Image")));
            this.btnDownload.Location = new System.Drawing.Point(1369, 621);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Padding = new System.Windows.Forms.Padding(2);
            this.btnDownload.Size = new System.Drawing.Size(93, 32);
            this.btnDownload.StyleController = this.layoutControl2;
            this.btnDownload.TabIndex = 226;
            this.btnDownload.Text = "다운로드";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnUpload.ImageOptions.Image")));
            this.btnUpload.Location = new System.Drawing.Point(977, 621);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Padding = new System.Windows.Forms.Padding(2);
            this.btnUpload.Size = new System.Drawing.Size(386, 32);
            this.btnUpload.StyleController = this.layoutControl2;
            this.btnUpload.TabIndex = 225;
            this.btnUpload.Text = "업로드";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // gcCodeDocumentFile
            // 
            this.gcCodeDocumentFile.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcCodeDocumentFile.Location = new System.Drawing.Point(977, 659);
            this.gcCodeDocumentFile.MainView = this.gvCodeDocumentFile;
            this.gcCodeDocumentFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcCodeDocumentFile.Name = "gcCodeDocumentFile";
            this.gcCodeDocumentFile.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnFileDelete,
            this.itemCodeColLookUpEdit});
            this.gcCodeDocumentFile.Size = new System.Drawing.Size(485, 309);
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
            this.productImagePictureEdit.Location = new System.Drawing.Point(966, 370);
            this.productImagePictureEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productImagePictureEdit.Name = "productImagePictureEdit";
            this.productImagePictureEdit.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.productImagePictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.productImagePictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.productImagePictureEdit.Size = new System.Drawing.Size(507, 179);
            this.productImagePictureEdit.StyleController = this.layoutControl2;
            this.productImagePictureEdit.TabIndex = 73;
            this.productImagePictureEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // corpIDTextEdit
            // 
            this.corpIDTextEdit.Location = new System.Drawing.Point(1049, 36);
            this.corpIDTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.corpIDTextEdit.Name = "corpIDTextEdit";
            this.corpIDTextEdit.Size = new System.Drawing.Size(221, 32);
            this.corpIDTextEdit.StyleController = this.layoutControl2;
            this.corpIDTextEdit.TabIndex = 54;
            this.corpIDTextEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // projectIDTextEdit
            // 
            this.projectIDTextEdit.Location = new System.Drawing.Point(1359, 36);
            this.projectIDTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.projectIDTextEdit.Name = "projectIDTextEdit";
            this.projectIDTextEdit.Size = new System.Drawing.Size(114, 32);
            this.projectIDTextEdit.StyleController = this.layoutControl2;
            this.projectIDTextEdit.TabIndex = 56;
            // 
            // projectNameTextEdit
            // 
            this.projectNameTextEdit.Location = new System.Drawing.Point(1049, 112);
            this.projectNameTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.projectNameTextEdit.Name = "projectNameTextEdit";
            this.projectNameTextEdit.Size = new System.Drawing.Size(424, 32);
            this.projectNameTextEdit.StyleController = this.layoutControl2;
            this.projectNameTextEdit.TabIndex = 66;
            this.projectNameTextEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // startDateDateEdit
            // 
            this.startDateDateEdit.EditValue = null;
            this.startDateDateEdit.Location = new System.Drawing.Point(1049, 230);
            this.startDateDateEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startDateDateEdit.Name = "startDateDateEdit";
            this.startDateDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.startDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.startDateDateEdit.Size = new System.Drawing.Size(169, 32);
            this.startDateDateEdit.StyleController = this.layoutControl2;
            this.startDateDateEdit.TabIndex = 68;
            this.startDateDateEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // endDateDateEdit
            // 
            this.endDateDateEdit.EditValue = null;
            this.endDateDateEdit.Location = new System.Drawing.Point(1307, 230);
            this.endDateDateEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.endDateDateEdit.Name = "endDateDateEdit";
            this.endDateDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endDateDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endDateDateEdit.Size = new System.Drawing.Size(166, 32);
            this.endDateDateEdit.StyleController = this.layoutControl2;
            this.endDateDateEdit.TabIndex = 70;
            this.endDateDateEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // gcProjectMaster
            // 
            this.gcProjectMaster.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gcProjectMaster.Location = new System.Drawing.Point(9, 36);
            this.gcProjectMaster.MainView = this.gvProjectMaster;
            this.gcProjectMaster.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcProjectMaster.Name = "gcProjectMaster";
            this.gcProjectMaster.Size = new System.Drawing.Size(925, 943);
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
            this.remarkTextEdit.Location = new System.Drawing.Point(1049, 306);
            this.remarkTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.remarkTextEdit.Name = "remarkTextEdit";
            this.remarkTextEdit.Size = new System.Drawing.Size(424, 58);
            this.remarkTextEdit.StyleController = this.layoutControl2;
            this.remarkTextEdit.TabIndex = 72;
            this.remarkTextEdit.EditValueChanged += new System.EventHandler(this.EditControl_ValueChanged);
            // 
            // classTextEdit
            // 
            this.classTextEdit.Location = new System.Drawing.Point(1049, 74);
            this.classTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.classTextEdit.Name = "classTextEdit";
            this.classTextEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.classTextEdit.Properties.NullText = "";
            this.classTextEdit.Properties.PopupView = this.searchLookUpEdit1View;
            this.classTextEdit.Properties.UseReadOnlyAppearance = false;
            this.classTextEdit.Size = new System.Drawing.Size(424, 32);
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
            // chargeSearchLookUpEdit
            // 
            this.chargeSearchLookUpEdit.Location = new System.Drawing.Point(1049, 192);
            this.chargeSearchLookUpEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chargeSearchLookUpEdit.Name = "chargeSearchLookUpEdit";
            this.chargeSearchLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chargeSearchLookUpEdit.Properties.NullText = "";
            this.chargeSearchLookUpEdit.Properties.PopupView = this.gridView5;
            this.chargeSearchLookUpEdit.Size = new System.Drawing.Size(169, 32);
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
            this.pMNameSearchLookUpEidit.Location = new System.Drawing.Point(1307, 192);
            this.pMNameSearchLookUpEidit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pMNameSearchLookUpEidit.Name = "pMNameSearchLookUpEidit";
            this.pMNameSearchLookUpEidit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.pMNameSearchLookUpEidit.Properties.NullText = "";
            this.pMNameSearchLookUpEidit.Properties.PopupView = this.gridView6;
            this.pMNameSearchLookUpEidit.Size = new System.Drawing.Size(166, 32);
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
            // materialSearchLookUpEidit
            // 
            this.materialSearchLookUpEidit.Location = new System.Drawing.Point(1049, 152);
            this.materialSearchLookUpEidit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.materialSearchLookUpEidit.Name = "materialSearchLookUpEidit";
            this.materialSearchLookUpEidit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.materialSearchLookUpEidit.Properties.NullText = "";
            this.materialSearchLookUpEidit.Properties.PopupView = this.gridView51;
            this.materialSearchLookUpEidit.Size = new System.Drawing.Size(424, 32);
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
            this.tabbedControlGroup1,
            this.layoutControlItem10});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1482, 1020);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.layoutControlItem12,
            this.layoutControlItem22,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem14,
            this.layoutControlItem20,
            this.layoutControlItem16,
            this.layoutControlItem26,
            this.layoutControlItem18,
            this.layoutControlItem11,
            this.layoutControlItem13});
            this.layoutControlGroup2.Location = new System.Drawing.Point(957, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup1";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup2.Size = new System.Drawing.Size(523, 556);
            this.layoutControlGroup2.Text = "프로젝트 상세내역";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.corpIDTextEdit;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(310, 38);
            this.layoutControlItem4.Text = "Corp ID:";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.classTextEdit;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(513, 38);
            this.layoutControlItem8.Text = "분류";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.chargeSearchLookUpEdit;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 156);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(258, 38);
            this.layoutControlItem12.Text = "담당자";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.remarkTextEdit;
            this.layoutControlItem22.Location = new System.Drawing.Point(0, 270);
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
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 334);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(513, 185);
            this.layoutControlItem5.Text = "Product Image:";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.projectIDTextEdit;
            this.layoutControlItem6.Location = new System.Drawing.Point(310, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(203, 38);
            this.layoutControlItem6.Text = "Project ID:";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.pMNameSearchLookUpEidit;
            this.layoutControlItem14.Location = new System.Drawing.Point(258, 156);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(255, 38);
            this.layoutControlItem14.Text = "총책임자";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.endDateDateEdit;
            this.layoutControlItem20.Location = new System.Drawing.Point(258, 194);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(255, 38);
            this.layoutControlItem20.Text = "종료일";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.projectNameTextEdit;
            this.layoutControlItem16.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(513, 38);
            this.layoutControlItem16.Text = "프로젝트명";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.materialSearchLookUpEidit;
            this.layoutControlItem26.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem26.CustomizationFormText = "품명";
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 114);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 5, 5);
            this.layoutControlItem26.Size = new System.Drawing.Size(513, 42);
            this.layoutControlItem26.Text = "품명";
            this.layoutControlItem26.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.startDateDateEdit;
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 194);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(258, 38);
            this.layoutControlItem18.Text = "시작일";
            this.layoutControlItem18.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.PlanEnddateEdit;
            this.layoutControlItem11.Location = new System.Drawing.Point(258, 232);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(255, 38);
            this.layoutControlItem11.Text = "목표종료일";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.PlanStartDateDateEdit;
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 232);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(258, 38);
            this.layoutControlItem13.Text = "시작계획일";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(67, 17);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup2";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup3.Size = new System.Drawing.Size(941, 986);
            this.layoutControlGroup3.Text = "프로젝트 내역조회";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcProjectMaster;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(931, 949);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(941, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(16, 986);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(957, 556);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(523, 16);
            // 
            // tabbedControlGroup1
            // 
            this.tabbedControlGroup1.Location = new System.Drawing.Point(957, 572);
            this.tabbedControlGroup1.Name = "tabbedControlGroup1";
            this.tabbedControlGroup1.SelectedTabPage = this.layoutCtlGrpFile;
            this.tabbedControlGroup1.Size = new System.Drawing.Size(523, 414);
            this.tabbedControlGroup1.TabPages.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutCtlGrpFile});
            this.tabbedControlGroup1.Text = "항목 리스트";
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
            this.layoutCtlGrpFile.Size = new System.Drawing.Size(491, 353);
            this.layoutCtlGrpFile.Text = "해당 디자인 파일 업로드";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcCodeDocumentFile;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 38);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(491, 315);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnUpload;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(392, 38);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnDownload;
            this.layoutControlItem9.Location = new System.Drawing.Point(392, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(99, 38);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.dataLayoutControl1;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 986);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(1480, 32);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
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
            // Frm_PJ_ProjectMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 1066);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_PJ_ProjectMaster";
            this.Load += new System.EventHandler(this.frmDesignProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PlanStartDateDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanStartDateDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanEnddateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanEnddateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.chargeSearchLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMNameSearchLookUpEidit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialSearchLookUpEidit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView51)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCtlGrpFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
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
        //private DesignDataSetTableAdapters.ProjectMasterTableAdapter projectMasterTableAdapter;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;
        private System.Windows.Forms.OpenFileDialog foDialog;
        //private DesignDataSetTableAdapters.ProjectMemberTableAdapter projectMemberTableAdapter;
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
        private DevExpress.XtraEditors.SearchLookUpEdit chargeSearchLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.SearchLookUpEdit pMNameSearchLookUpEidit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
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
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private DevExpress.XtraLayout.TabbedControlGroup tabbedControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutCtlGrpFile;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SearchLookUpEdit materialSearchLookUpEidit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView51;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DateEdit PlanStartDateDateEdit;
        private DateEdit PlanEnddateEdit;
        private DataLayoutControl dataLayoutControl1;
        private LayoutControlGroup layoutControlGroup4;
        private LayoutControlItem layoutControlItem11;
        private LayoutControlItem layoutControlItem13;
        private LayoutControlItem layoutControlItem10;
    }
}