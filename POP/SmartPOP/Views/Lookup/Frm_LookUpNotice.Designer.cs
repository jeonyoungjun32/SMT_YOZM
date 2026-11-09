namespace SmartPOP.Views.Lookup
{
    partial class Frm_LookUpNotice
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LookUpNotice));
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.cmdSave = new DevExpress.XtraBars.Navigation.NavButton();
            this.navButton1 = new DevExpress.XtraBars.Navigation.NavButton();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.noticeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.fontRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.FontRibbonPageGroup();
            this.fontRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.FontRibbonPageGroup();
            this.changeFontNameItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontNameItem();
            this.changeFontSizeItem2 = new DevExpress.XtraRichEdit.UI.ChangeFontSizeItem();
            this.illustrationsBar1 = new DevExpress.XtraRichEdit.UI.IllustrationsBar();
            this.smtBodyRichEdit1 = new baseSMT.smtBodyRichEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcNotice = new DevExpress.XtraGrid.GridControl();
            this.gvNotice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCorpId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBoardNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBody = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDisplayType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colCreIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ChkTodayView = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.noticeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcNotice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNotice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChkTodayView.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // cmdSave
            // 
            this.cmdSave.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.cmdSave.Caption = "저 장";
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Visible = false;
            // 
            // navButton1
            // 
            this.navButton1.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
            this.navButton1.Caption = "저 장";
            this.navButton1.Name = "navButton1";
            this.navButton1.Visible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(842, 45);
            this.smtCommonBar1.smtImageTitleAutoText = false;
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "공지사항정보";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 187;
            // 
            // btn_Close
            // 
            this.btn_Close.AllowFocus = false;
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Close.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseForeColor = true;
            this.btn_Close.AutoSize = true;
            this.btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Close.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Close.ImageOptions.ImageIndex = 5;
            this.btn_Close.ImageOptions.ImageToTextIndent = 15;
            this.btn_Close.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Close.Location = new System.Drawing.Point(763, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.Size = new System.Drawing.Size(79, 45);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Visible = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // noticeBindingSource
            // 
            this.noticeBindingSource.DataMember = "Notice";
            // 
            // codeDataSet
            // 
            //this.codeDataSet.DataSetName = "CodeDataSet";
            //this.codeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // fontRibbonPageGroup1
            // 
            this.fontRibbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.True;
            this.fontRibbonPageGroup1.Name = "fontRibbonPageGroup1";
            this.fontRibbonPageGroup1.Text = "";
            // 
            // noticeTableAdapter
            // 
            // fontRibbonPageGroup2
            // 
            this.fontRibbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.True;
            this.fontRibbonPageGroup2.Name = "fontRibbonPageGroup2";
            this.fontRibbonPageGroup2.Text = "";
            // 
            // changeFontNameItem1
            // 
            this.changeFontNameItem1.Edit = this.repositoryItemFontEdit1;
            this.changeFontNameItem1.Id = 23;
            this.changeFontNameItem1.Name = "changeFontNameItem1";
            // 
            // changeFontSizeItem2
            // 
            this.changeFontSizeItem2.Edit = null;
            this.changeFontSizeItem2.Id = 24;
            this.changeFontSizeItem2.Name = "changeFontSizeItem2";
            // 
            // illustrationsBar1
            // 
            this.illustrationsBar1.BarName = "";
            this.illustrationsBar1.Control = null;
            this.illustrationsBar1.DockCol = 0;
            this.illustrationsBar1.DockRow = 1;
            this.illustrationsBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.illustrationsBar1.Text = "";
            // 
            // smtBodyRichEdit1
            // 
            this.smtBodyRichEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smtBodyRichEdit1.Location = new System.Drawing.Point(0, 211);
            this.smtBodyRichEdit1.LookAndFeel.SkinName = "WXI";
            this.smtBodyRichEdit1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtBodyRichEdit1.Name = "smtBodyRichEdit1";
            this.smtBodyRichEdit1.Size = new System.Drawing.Size(842, 236);
            this.smtBodyRichEdit1.smtRichEditDataDataBindings.Add(new System.Windows.Forms.Binding("RtfText", this.noticeBindingSource, "Body", true));
            this.smtBodyRichEdit1.smtRichEditFontVisible = false;
            this.smtBodyRichEdit1.smtRichEditReadOnly = true;
            this.smtBodyRichEdit1.smtRichEditRtfText = resources.GetString("smtBodyRichEdit1.smtRichEditRtfText");
            this.smtBodyRichEdit1.smtRichEditText = "";
            this.smtBodyRichEdit1.TabIndex = 196;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcNotice);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(544, 263, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(842, 166);
            this.layoutControl1.TabIndex = 197;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcNotice
            // 
            this.gcNotice.DataSource = this.noticeBindingSource;
            this.gcNotice.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(70)))), ((int)(((byte)(68)))));
            this.gcNotice.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
            this.gcNotice.Location = new System.Drawing.Point(4, 4);
            this.gcNotice.MainView = this.gvNotice;
            this.gcNotice.Name = "gcNotice";
            this.gcNotice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit3});
            this.gcNotice.Size = new System.Drawing.Size(834, 158);
            this.gcNotice.TabIndex = 4;
            this.gcNotice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNotice});
            // 
            // gvNotice
            // 
            this.gvNotice.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvNotice.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvNotice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCorpId,
            this.colBoardNo,
            this.colClass,
            this.colWriteDate,
            this.colSubject,
            this.colWriteName,
            this.colBody,
            this.colDisplayType,
            this.colStartDate,
            this.colEndDate,
            this.colCreId,
            this.colCreIP,
            this.colCreDt,
            this.colModId,
            this.colModIP,
            this.colModDt});
            this.gvNotice.GridControl = this.gcNotice;
            this.gvNotice.Name = "gvNotice";
            this.gvNotice.OptionsBehavior.Editable = false;
            this.gvNotice.OptionsDetail.EnableMasterViewMode = false;
            this.gvNotice.OptionsView.ColumnAutoWidth = false;
            this.gvNotice.OptionsView.ShowAutoFilterRow = true;
            this.gvNotice.OptionsView.ShowGroupPanel = false;
            // 
            // colCorpId
            // 
            this.colCorpId.Caption = "사업장코드";
            this.colCorpId.FieldName = "CorpId";
            this.colCorpId.Name = "colCorpId";
            // 
            // colBoardNo
            // 
            this.colBoardNo.Caption = "공지번호";
            this.colBoardNo.FieldName = "BoardNo";
            this.colBoardNo.Name = "colBoardNo";
            this.colBoardNo.Visible = true;
            this.colBoardNo.VisibleIndex = 0;
            this.colBoardNo.Width = 120;
            // 
            // colClass
            // 
            this.colClass.Caption = "구분";
            this.colClass.FieldName = "Class";
            this.colClass.Name = "colClass";
            // 
            // colWriteDate
            // 
            this.colWriteDate.AppearanceCell.Options.UseTextOptions = true;
            this.colWriteDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWriteDate.Caption = "작성일자";
            this.colWriteDate.FieldName = "WriteDate";
            this.colWriteDate.Name = "colWriteDate";
            this.colWriteDate.Visible = true;
            this.colWriteDate.VisibleIndex = 1;
            this.colWriteDate.Width = 110;
            // 
            // colSubject
            // 
            this.colSubject.Caption = "제목";
            this.colSubject.FieldName = "Subject";
            this.colSubject.Name = "colSubject";
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 2;
            this.colSubject.Width = 427;
            // 
            // colWriteName
            // 
            this.colWriteName.Caption = "작성자";
            this.colWriteName.FieldName = "WriteName";
            this.colWriteName.Name = "colWriteName";
            // 
            // colBody
            // 
            this.colBody.Caption = "내용";
            this.colBody.FieldName = "Body";
            this.colBody.Name = "colBody";
            this.colBody.Width = 492;
            // 
            // colDisplayType
            // 
            this.colDisplayType.Caption = "표시방식";
            this.colDisplayType.FieldName = "DisplayType";
            this.colDisplayType.Name = "colDisplayType";
            // 
            // colStartDate
            // 
            this.colStartDate.AppearanceCell.Options.UseTextOptions = true;
            this.colStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartDate.Caption = "시작일자";
            this.colStartDate.FieldName = "StartDate";
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.Width = 110;
            // 
            // colEndDate
            // 
            this.colEndDate.AppearanceCell.Options.UseTextOptions = true;
            this.colEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndDate.Caption = "종료일자";
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.Width = 110;
            // 
            // colCreId
            // 
            this.colCreId.AppearanceCell.Options.UseTextOptions = true;
            this.colCreId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreId.Caption = "작성자";
            this.colCreId.ColumnEdit = this.repositoryItemLookUpEdit3;
            this.colCreId.FieldName = "CreId";
            this.colCreId.Name = "colCreId";
            this.colCreId.Visible = true;
            this.colCreId.VisibleIndex = 3;
            this.colCreId.Width = 112;
            // 
            // repositoryItemLookUpEdit3
            // 
            this.repositoryItemLookUpEdit3.AutoHeight = false;
            this.repositoryItemLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit3.Name = "repositoryItemLookUpEdit3";
            this.repositoryItemLookUpEdit3.NullText = "";
            // 
            // colCreIP
            // 
            this.colCreIP.Caption = "생성IP";
            this.colCreIP.FieldName = "CreIP";
            this.colCreIP.Name = "colCreIP";
            // 
            // colCreDt
            // 
            this.colCreDt.Caption = "생성일자";
            this.colCreDt.FieldName = "CreDt";
            this.colCreDt.Name = "colCreDt";
            // 
            // colModId
            // 
            this.colModId.AppearanceCell.Options.UseTextOptions = true;
            this.colModId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colModId.Caption = "최종수정자";
            this.colModId.ColumnEdit = this.repositoryItemLookUpEdit3;
            this.colModId.FieldName = "ModId";
            this.colModId.Name = "colModId";
            this.colModId.Width = 112;
            // 
            // colModIP
            // 
            this.colModIP.Caption = "수정IP";
            this.colModIP.FieldName = "ModIP";
            this.colModIP.Name = "colModIP";
            // 
            // colModDt
            // 
            this.colModDt.Caption = "수정일자";
            this.colModDt.FieldName = "ModDt";
            this.colModDt.Name = "colModDt";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(842, 166);
            this.Root.Text = "공지사항 리스트";
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcNotice;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(106, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(840, 164);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.ChkTodayView);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 447);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(842, 30);
            this.panelControl1.TabIndex = 198;
            // 
            // ChkTodayView
            // 
            this.ChkTodayView.Dock = System.Windows.Forms.DockStyle.Right;
            this.ChkTodayView.Location = new System.Drawing.Point(691, 2);
            this.ChkTodayView.Margin = new System.Windows.Forms.Padding(0);
            this.ChkTodayView.Name = "ChkTodayView";
            this.ChkTodayView.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.ChkTodayView.Properties.Appearance.Options.UseFont = true;
            this.ChkTodayView.Properties.Caption = " 오늘하루 표시안함";
            this.ChkTodayView.Size = new System.Drawing.Size(149, 26);
            this.ChkTodayView.TabIndex = 1;
            this.ChkTodayView.CheckedChanged += new System.EventHandler(this.ChkTodayView_CheckedChanged);
            // 
            // frmLookUpNotice
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(842, 477);
            this.Controls.Add(this.smtBodyRichEdit1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmLookUpNotice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLookUpNotice_FormClosed);
            this.Load += new System.EventHandler(this.frmLookUpNotice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.noticeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcNotice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNotice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChkTodayView.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Navigation.NavButton cmdSave;
        private DevExpress.XtraBars.Navigation.NavButton navButton1;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Close;
        private DevExpress.XtraRichEdit.UI.FontRibbonPageGroup fontRibbonPageGroup1;
        private System.Windows.Forms.BindingSource noticeBindingSource;
        private DevExpress.XtraRichEdit.UI.FontRibbonPageGroup fontRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.ChangeFontNameItem changeFontNameItem1;
        private DevExpress.XtraRichEdit.UI.ChangeFontSizeItem changeFontSizeItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private DevExpress.XtraRichEdit.UI.IllustrationsBar illustrationsBar1;
        private baseSMT.smtBodyRichEdit smtBodyRichEdit1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcNotice;
        private DevExpress.XtraGrid.Views.Grid.GridView gvNotice;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId;
        private DevExpress.XtraGrid.Columns.GridColumn colBoardNo;
        private DevExpress.XtraGrid.Columns.GridColumn colClass;
        private DevExpress.XtraGrid.Columns.GridColumn colWriteDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colWriteName;
        private DevExpress.XtraGrid.Columns.GridColumn colBody;
        private DevExpress.XtraGrid.Columns.GridColumn colDisplayType;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreId;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
        private DevExpress.XtraGrid.Columns.GridColumn colCreIP;
        private DevExpress.XtraGrid.Columns.GridColumn colCreDt;
        private DevExpress.XtraGrid.Columns.GridColumn colModId;
        private DevExpress.XtraGrid.Columns.GridColumn colModIP;
        private DevExpress.XtraGrid.Columns.GridColumn colModDt;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit ChkTodayView;
    }
}