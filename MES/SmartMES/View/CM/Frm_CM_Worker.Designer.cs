namespace SmartMES.View.CM
{
    partial class Frm_CM_Worker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_Worker));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcWorker = new DevExpress.XtraGrid.GridControl();
            this.gvWorker = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.DateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemDateEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.EmployeeLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdInsert = new baseSMT.smtSearchButton();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCorpId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcWorker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcWorker);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1324, 464);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcWorker
            // 
            this.gcWorker.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.gcWorker.Location = new System.Drawing.Point(3, 3);
            this.gcWorker.MainView = this.gvWorker;
            this.gcWorker.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.gcWorker.Name = "gcWorker";
            this.gcWorker.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.DateEdit1,
            this.DateEdit2,
            this.repositoryItemDateEdit5,
            this.EmployeeLookUpEdit,
            this.repositoryItemLookUpEdit2});
            this.gcWorker.Size = new System.Drawing.Size(1318, 458);
            this.gcWorker.TabIndex = 215;
            this.gcWorker.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvWorker});
            // 
            // gvWorker
            // 
            this.gvWorker.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvWorker.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvWorker.ColumnPanelRowHeight = 41;
            this.gvWorker.GridControl = this.gcWorker;
            this.gvWorker.Name = "gvWorker";
            this.gvWorker.OptionsEditForm.PopupEditFormWidth = 1000;
            this.gvWorker.OptionsView.ShowAutoFilterRow = true;
            this.gvWorker.OptionsView.ShowGroupPanel = false;
            this.gvWorker.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvClass_CellValueChanged);
            // 
            // DateEdit1
            // 
            this.DateEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.DateEdit1.AutoHeight = false;
            this.DateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit1.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.DateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit1.DisplayFormat.FormatString = "G";
            this.DateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit1.EditFormat.FormatString = "G";
            this.DateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit1.MaskSettings.Set("mask", "");
            this.DateEdit1.Name = "DateEdit1";
            this.DateEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // DateEdit2
            // 
            this.DateEdit2.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.DateEdit2.AutoHeight = false;
            this.DateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit2.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.DateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit2.DisplayFormat.FormatString = "G";
            this.DateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit2.EditFormat.FormatString = "G";
            this.DateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit2.MaskSettings.Set("mask", "");
            this.DateEdit2.Name = "DateEdit2";
            this.DateEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemDateEdit5
            // 
            this.repositoryItemDateEdit5.AutoHeight = false;
            this.repositoryItemDateEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit5.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit5.Name = "repositoryItemDateEdit5";
            // 
            // EmployeeLookUpEdit
            // 
            this.EmployeeLookUpEdit.AutoHeight = false;
            this.EmployeeLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EmployeeLookUpEdit.Name = "EmployeeLookUpEdit";
            this.EmployeeLookUpEdit.NullText = "작업자를 선택해주세요";
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1324, 464);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcWorker;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1324, 464);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdInsert);
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.cmdDelete);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1324, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "작업자관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 6;
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
            this.cmdInsert.Location = new System.Drawing.Point(834, 0);
            this.cmdInsert.Margin = new System.Windows.Forms.Padding(4);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdInsert.Size = new System.Drawing.Size(92, 40);
            this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdInsert.TabIndex = 7;
            this.cmdInsert.Tag = "Ins";
            this.cmdInsert.Text = "등록";
            this.cmdInsert.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdDisplay.Location = new System.Drawing.Point(199, 0);
            this.cmdDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(92, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 5;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Visible = false;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
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
            this.cmdSave.Location = new System.Drawing.Point(926, 0);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(92, 40);
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
            this.cmdDelete.Location = new System.Drawing.Point(1018, 0);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(92, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Tag = "Del";
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdExcel.Location = new System.Drawing.Point(1110, 0);
            this.cmdExcel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(122, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 6;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
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
            this.cmdClose.Location = new System.Drawing.Point(1232, 0);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(92, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "상태";
            this.gridColumn1.FieldName = "Status";
            this.gridColumn1.MinWidth = 23;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 189;
            // 
            // colCorpId1
            // 
            this.colCorpId1.AppearanceHeader.Options.UseTextOptions = true;
            this.colCorpId1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCorpId1.FieldName = "CorpId";
            this.colCorpId1.MinWidth = 19;
            this.colCorpId1.Name = "colCorpId1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "작업자명";
            this.gridColumn2.FieldName = "EmployeeId";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 308;
            // 
            // Frm_CM_Worker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1324, 504);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Frm_CM_Worker";
            this.Text = "작업자관리";
            this.Load += new System.EventHandler(this.Frm_CM_Worker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcWorker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvWorker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdInsert;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDelete;
		private baseSMT.smtSearchButton cmdExcel;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraGrid.GridControl gcWorker;
		private DevExpress.XtraGrid.Views.Grid.GridView gvWorker;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DateEdit1;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DateEdit2;
		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit5;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit EmployeeLookUpEdit;
		private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private DevExpress.XtraGrid.Columns.GridColumn colCorpId1;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
	}
}