namespace SmartMES.View.SM
{
    partial class Frm_SM_Quick
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SM_Quick));
            DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.imageCollection11 = new DevExpress.Utils.ImageCollection(this.components);
            this.tileNavCategory1 = new DevExpress.XtraBars.Navigation.TileNavCategory();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn11 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCheck = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNewTitle_ko = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colParentId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colGroupLevel = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRowNum = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colTitle_ko = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colCorpId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.btn_Save = new baseSMT.smtSearchButton();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.MenuTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn6 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn7 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn8 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn9 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.quickCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.treeListColumn10 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn12 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.emptyEditor = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MenuTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quickCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection11
            // 
            this.imageCollection11.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection11.ImageStream")));
            this.imageCollection11.Images.SetKeyName(0, "copy.png");
            this.imageCollection11.Images.SetKeyName(1, "folder_close.png");
            this.imageCollection11.Images.SetKeyName(2, "folder_open.png");
            this.imageCollection11.Images.SetKeyName(3, "folder.png");
            this.imageCollection11.Images.SetKeyName(4, "writing.png");
            // 
            // tileNavCategory1
            // 
            this.tileNavCategory1.Name = "tileNavCategory1";
            // 
            // 
            // 
            this.tileNavCategory1.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "정렬순번";
            this.treeListColumn2.FieldName = "SortNum";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // treeListColumn11
            // 
            this.treeListColumn11.Caption = "그룹여부";
            this.treeListColumn11.FieldName = "GroupYn";
            this.treeListColumn11.Name = "treeListColumn11";
            this.treeListColumn11.OptionsColumn.ReadOnly = true;
            // 
            // colCheck
            // 
            this.colCheck.AppearanceHeader.Options.UseTextOptions = true;
            this.colCheck.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCheck.Caption = "적 용";
            repositoryItemCheckEdit2.AutoHeight = false;
            repositoryItemCheckEdit2.Name = "quickCheckEdit";
            repositoryItemCheckEdit2.ValueChecked = "Y";
            repositoryItemCheckEdit2.ValueUnchecked = "N";
            this.colCheck.ColumnEdit = repositoryItemCheckEdit2;
            this.colCheck.FieldName = "QuickYn";
            this.colCheck.MinWidth = 50;
            this.colCheck.Name = "colCheck";
            this.colCheck.OptionsColumn.AllowSort = true;
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 3;
            this.colCheck.Width = 67;
            // 
            // colNewTitle_ko
            // 
            this.colNewTitle_ko.Caption = "메뉴 약칭";
            this.colNewTitle_ko.FieldName = "NewTitle_ko";
            this.colNewTitle_ko.Name = "colNewTitle_ko";
            this.colNewTitle_ko.OptionsColumn.AllowSort = true;
            this.colNewTitle_ko.Visible = true;
            this.colNewTitle_ko.VisibleIndex = 2;
            this.colNewTitle_ko.Width = 200;
            // 
            // colParentId
            // 
            this.colParentId.FieldName = "ParentId";
            this.colParentId.Name = "colParentId";
            this.colParentId.OptionsColumn.AllowSort = true;
            this.colParentId.OptionsColumn.ReadOnly = true;
            this.colParentId.Width = 90;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowSort = true;
            this.colId.OptionsColumn.ReadOnly = true;
            this.colId.Width = 90;
            // 
            // colGroupLevel
            // 
            this.colGroupLevel.Caption = "레벨";
            this.colGroupLevel.FieldName = "GroupLevel";
            this.colGroupLevel.Name = "colGroupLevel";
            this.colGroupLevel.OptionsColumn.AllowSort = true;
            this.colGroupLevel.OptionsColumn.ReadOnly = true;
            this.colGroupLevel.Width = 50;
            // 
            // colRowNum
            // 
            this.colRowNum.Caption = "순번";
            this.colRowNum.FieldName = "RowNum";
            this.colRowNum.Name = "colRowNum";
            this.colRowNum.OptionsColumn.AllowSort = true;
            this.colRowNum.OptionsColumn.ReadOnly = true;
            this.colRowNum.Width = 50;
            // 
            // colTitle_ko
            // 
            this.colTitle_ko.Caption = "메뉴항목명";
            this.colTitle_ko.FieldName = "Title_ko";
            this.colTitle_ko.Name = "colTitle_ko";
            this.colTitle_ko.OptionsColumn.AllowSort = true;
            this.colTitle_ko.OptionsColumn.ReadOnly = true;
            this.colTitle_ko.Visible = true;
            this.colTitle_ko.VisibleIndex = 0;
            this.colTitle_ko.Width = 300;
            // 
            // colCorpId
            // 
            this.colCorpId.FieldName = "CorpId";
            this.colCorpId.Name = "colCorpId";
            this.colCorpId.OptionsColumn.AllowSort = true;
            this.colCorpId.OptionsColumn.ReadOnly = true;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.btn_Save);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(946, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "즐겨찾기 설정";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 280;
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
            this.btn_Search.Location = new System.Drawing.Point(119, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 50;
            this.btn_Search.Text = "조회";
            this.btn_Search.Visible = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.AllowFocus = false;
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Save.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseForeColor = true;
            this.btn_Save.AutoSize = true;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Save.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Save.ImageOptions.ImageIndex = 2;
            this.btn_Save.ImageOptions.ImageToTextIndent = 15;
            this.btn_Save.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Save.Location = new System.Drawing.Point(800, 0);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btn_Save.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Save.Size = new System.Drawing.Size(73, 40);
            this.btn_Save.smtButtonAutoText = false;
            this.btn_Save.smtButtonStyle = baseSMT.EnumButtonStyles.수정;
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "저장";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
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
            this.btn_Close.Location = new System.Drawing.Point(873, 0);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(73, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.splitContainerControl2);
            this.layoutControl2.Controls.Add(this.MenuTreeList);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 40);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(419, 19, 650, 400);
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(946, 576);
            this.layoutControl2.TabIndex = 281;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(10, 28);
            this.splitContainerControl2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(718, 1);
            this.splitContainerControl2.SplitterPosition = 0;
            this.splitContainerControl2.TabIndex = 0;
            // 
            // MenuTreeList
            // 
            this.MenuTreeList.ColumnPanelRowHeight = 31;
            this.MenuTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5,
            this.treeListColumn6,
            this.treeListColumn7,
            this.treeListColumn8,
            this.treeListColumn9,
            this.treeListColumn10,
            this.treeListColumn12});
            this.MenuTreeList.Cursor = System.Windows.Forms.Cursors.Default;
            this.MenuTreeList.KeyFieldName = "Id";
            this.MenuTreeList.Location = new System.Drawing.Point(4, 31);
            this.MenuTreeList.Name = "MenuTreeList";
            this.MenuTreeList.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.MouseDown;
            this.MenuTreeList.OptionsBehavior.PopulateServiceColumns = true;
            this.MenuTreeList.OptionsView.AutoWidth = false;
            this.MenuTreeList.OptionsView.ShowAutoFilterRow = true;
            this.MenuTreeList.ParentFieldName = "ParentId";
            this.MenuTreeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.quickCheckEdit,
            this.emptyEditor});
            this.MenuTreeList.RowHeight = 31;
            this.MenuTreeList.Size = new System.Drawing.Size(938, 541);
            this.MenuTreeList.TabIndex = 179;
            this.MenuTreeList.TreeLevelWidth = 24;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.FieldName = "CorpId";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowSort = true;
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "메뉴항목명";
            this.treeListColumn3.FieldName = "Title_ko";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.AllowSort = true;
            this.treeListColumn3.OptionsColumn.ReadOnly = true;
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 0;
            this.treeListColumn3.Width = 300;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "순번";
            this.treeListColumn4.FieldName = "RowNum";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.AllowSort = true;
            this.treeListColumn4.OptionsColumn.ReadOnly = true;
            this.treeListColumn4.Width = 50;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "레벨";
            this.treeListColumn5.FieldName = "GroupLevel";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.OptionsColumn.AllowSort = true;
            this.treeListColumn5.OptionsColumn.ReadOnly = true;
            this.treeListColumn5.Width = 50;
            // 
            // treeListColumn6
            // 
            this.treeListColumn6.FieldName = "Id";
            this.treeListColumn6.Name = "treeListColumn6";
            this.treeListColumn6.OptionsColumn.AllowSort = true;
            this.treeListColumn6.OptionsColumn.ReadOnly = true;
            this.treeListColumn6.Width = 90;
            // 
            // treeListColumn7
            // 
            this.treeListColumn7.FieldName = "ParentId";
            this.treeListColumn7.Name = "treeListColumn7";
            this.treeListColumn7.OptionsColumn.AllowSort = true;
            this.treeListColumn7.OptionsColumn.ReadOnly = true;
            this.treeListColumn7.Width = 90;
            // 
            // treeListColumn8
            // 
            this.treeListColumn8.Caption = "메뉴 약칭";
            this.treeListColumn8.FieldName = "NewTitle_ko";
            this.treeListColumn8.Name = "treeListColumn8";
            this.treeListColumn8.OptionsColumn.AllowSort = true;
            this.treeListColumn8.Visible = true;
            this.treeListColumn8.VisibleIndex = 2;
            this.treeListColumn8.Width = 200;
            // 
            // treeListColumn9
            // 
            this.treeListColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.treeListColumn9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeListColumn9.Caption = "적 용";
            this.treeListColumn9.ColumnEdit = this.quickCheckEdit;
            this.treeListColumn9.FieldName = "QuickYn";
            this.treeListColumn9.MinWidth = 50;
            this.treeListColumn9.Name = "treeListColumn9";
            this.treeListColumn9.OptionsColumn.AllowSort = true;
            this.treeListColumn9.Visible = true;
            this.treeListColumn9.VisibleIndex = 3;
            this.treeListColumn9.Width = 67;
            // 
            // quickCheckEdit
            // 
            this.quickCheckEdit.AutoHeight = false;
            this.quickCheckEdit.Name = "quickCheckEdit";
            this.quickCheckEdit.ValueChecked = "Y";
            this.quickCheckEdit.ValueUnchecked = "N";
            // 
            // treeListColumn10
            // 
            this.treeListColumn10.Caption = "그룹여부";
            this.treeListColumn10.FieldName = "GroupYn";
            this.treeListColumn10.Name = "treeListColumn10";
            this.treeListColumn10.OptionsColumn.ReadOnly = true;
            // 
            // treeListColumn12
            // 
            this.treeListColumn12.Caption = "정렬순번";
            this.treeListColumn12.FieldName = "SortNum";
            this.treeListColumn12.Name = "treeListColumn12";
            this.treeListColumn12.Visible = true;
            this.treeListColumn12.VisibleIndex = 1;
            // 
            // emptyEditor
            // 
            this.emptyEditor.AutoHeight = false;
            this.emptyEditor.Name = "emptyEditor";
            this.emptyEditor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(946, 576);
            this.layoutControlGroup2.Text = "메뉴 트리";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.MenuTreeList;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem2";
            this.layoutControlItem3.Size = new System.Drawing.Size(944, 547);
            this.layoutControlItem3.Text = "layoutControlItem1";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Frm_SM_Quick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 616);
            this.Controls.Add(this.layoutControl2);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_SM_Quick";
            this.Text = "즐겨찾기 설정";
            this.Load += new System.EventHandler(this.Frm_SM_Quick_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MenuTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quickCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.Utils.ImageCollection imageCollection11;
        private DevExpress.XtraBars.Navigation.TileNavCategory tileNavCategory1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCorpId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTitle_ko;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRowNum;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colGroupLevel;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNewTitle_ko;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCheck;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn11;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchButton btn_Save;
        private baseSMT.smtSearchButton btn_Close;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraTreeList.TreeList MenuTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn6;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn7;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn8;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit quickCheckEdit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn10;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn12;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit emptyEditor;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}