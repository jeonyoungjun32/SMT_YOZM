namespace SmartMES.View.ST
{
    partial class Frm_ST_StockInOutLIst
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ST_StockInOutLIst));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Item = new baseSMT.smtSearchLookUp();
            this.Lup_House = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcInOut = new DevExpress.XtraGrid.GridControl();
            this.gvInOut = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.authorCheckedComboBoxEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.photoColPictureEdit = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.useYnColCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.deptColTreeListLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTreeListLookUpEdit();
            this.repositoryItemTreeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.corpIdColLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmdExcel = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcInOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorCheckedComboBoxEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoColPictureEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useYnColCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deptColTreeListLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTreeListLookUpEdit1TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corpIdColLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Item);
            this.smtCommonBar1.Controls.Add(this.Lup_House);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1628, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "입출고현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 6;
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
            this.btn_Search.Location = new System.Drawing.Point(1192, 0);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(92, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 13;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // Lup_Item
            // 
            this.Lup_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Item.Location = new System.Drawing.Point(832, 0);
            this.Lup_Item.LookAndFeel.SkinName = "WXI";
            this.Lup_Item.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Item.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lup_Item.Name = "Lup_Item";
            this.Lup_Item.Size = new System.Drawing.Size(360, 40);
            this.Lup_Item.smtLabelTitleValue = "품목";
            this.Lup_Item.smtLabelTitleVisible = true;
            this.Lup_Item.smtLabelTitleWidth = 100;
            this.Lup_Item.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Item.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Item.smtLookUpDataSource = null;
            this.Lup_Item.smtLookUpDataValueMember = "코드";
            this.Lup_Item.smtLookUpSelectdIndex = -1;
            this.Lup_Item.smtLookUpText = "";
            this.Lup_Item.smtLookUpValue = "";
            this.Lup_Item.TabIndex = 11;
            // 
            // Lup_House
            // 
            this.Lup_House.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_House.Location = new System.Drawing.Point(472, 0);
            this.Lup_House.LookAndFeel.SkinName = "WXI";
            this.Lup_House.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_House.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lup_House.Name = "Lup_House";
            this.Lup_House.Size = new System.Drawing.Size(360, 40);
            this.Lup_House.smtLabelTitleValue = "창고";
            this.Lup_House.smtLabelTitleVisible = true;
            this.Lup_House.smtLabelTitleWidth = 100;
            this.Lup_House.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_House.smtLookUpDataDisplayMember = "코드명";
            this.Lup_House.smtLookUpDataSource = null;
            this.Lup_House.smtLookUpDataValueMember = "코드";
            this.Lup_House.smtLookUpSelectdIndex = -1;
            this.Lup_House.smtLookUpText = "";
            this.Lup_House.smtLookUpValue = "";
            this.Lup_House.TabIndex = 12;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(127, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.Dt_Date.TabIndex = 39;
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
            this.smtSearchButton1.Location = new System.Drawing.Point(1536, 0);
            this.smtSearchButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(92, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.smtSearchButton1.TabIndex = 5;
            this.smtSearchButton1.Text = "닫기";
            this.smtSearchButton1.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcInOut);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1628, 627);
            this.layoutControl1.TabIndex = 7;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcInOut
            // 
            this.gcInOut.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.gcInOut.Location = new System.Drawing.Point(4, 4);
            this.gcInOut.MainView = this.gvInOut;
            this.gcInOut.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.gcInOut.Name = "gcInOut";
            this.gcInOut.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.authorCheckedComboBoxEdit,
            this.photoColPictureEdit,
            this.useYnColCheckEdit,
            this.deptColTreeListLookUpEdit,
            this.corpIdColLookUpEdit});
            this.gcInOut.Size = new System.Drawing.Size(1620, 619);
            this.gcInOut.TabIndex = 48;
            this.gcInOut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInOut});
            // 
            // gvInOut
            // 
            this.gvInOut.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvInOut.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvInOut.ColumnPanelRowHeight = 34;
            this.gvInOut.DetailHeight = 434;
            this.gvInOut.GridControl = this.gcInOut;
            this.gvInOut.Name = "gvInOut";
            this.gvInOut.OptionsBehavior.ReadOnly = true;
            this.gvInOut.OptionsDetail.EnableMasterViewMode = false;
            this.gvInOut.OptionsEditForm.PopupEditFormWidth = 1000;
            this.gvInOut.OptionsView.ColumnAutoWidth = false;
            this.gvInOut.OptionsView.ShowAutoFilterRow = true;
            this.gvInOut.OptionsView.ShowGroupPanel = false;
            this.gvInOut.RowHeight = 34;
            // 
            // authorCheckedComboBoxEdit
            // 
            this.authorCheckedComboBoxEdit.AutoHeight = false;
            this.authorCheckedComboBoxEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.authorCheckedComboBoxEdit.DisplayMember = "GroupName";
            this.authorCheckedComboBoxEdit.Name = "authorCheckedComboBoxEdit";
            this.authorCheckedComboBoxEdit.ValueMember = "GroupCode";
            // 
            // photoColPictureEdit
            // 
            this.photoColPictureEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.photoColPictureEdit.Name = "photoColPictureEdit";
            this.photoColPictureEdit.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.photoColPictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // useYnColCheckEdit
            // 
            this.useYnColCheckEdit.AutoHeight = false;
            this.useYnColCheckEdit.Name = "useYnColCheckEdit";
            this.useYnColCheckEdit.ValueChecked = "Y";
            this.useYnColCheckEdit.ValueUnchecked = "N";
            // 
            // deptColTreeListLookUpEdit
            // 
            this.deptColTreeListLookUpEdit.AutoHeight = false;
            this.deptColTreeListLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deptColTreeListLookUpEdit.Name = "deptColTreeListLookUpEdit";
            this.deptColTreeListLookUpEdit.NullText = "";
            this.deptColTreeListLookUpEdit.TreeList = this.repositoryItemTreeListLookUpEdit1TreeList;
            // 
            // repositoryItemTreeListLookUpEdit1TreeList
            // 
            this.repositoryItemTreeListLookUpEdit1TreeList.HorzScrollStep = 2;
            this.repositoryItemTreeListLookUpEdit1TreeList.KeyFieldName = "Id";
            this.repositoryItemTreeListLookUpEdit1TreeList.Location = new System.Drawing.Point(9, 8);
            this.repositoryItemTreeListLookUpEdit1TreeList.MinWidth = 22;
            this.repositoryItemTreeListLookUpEdit1TreeList.Name = "repositoryItemTreeListLookUpEdit1TreeList";
            this.repositoryItemTreeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            this.repositoryItemTreeListLookUpEdit1TreeList.ParentFieldName = "ParentId";
            this.repositoryItemTreeListLookUpEdit1TreeList.Size = new System.Drawing.Size(400, 200);
            this.repositoryItemTreeListLookUpEdit1TreeList.TabIndex = 0;
            this.repositoryItemTreeListLookUpEdit1TreeList.TreeLevelWidth = 17;
            // 
            // corpIdColLookUpEdit
            // 
            this.corpIdColLookUpEdit.AutoHeight = false;
            this.corpIdColLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.corpIdColLookUpEdit.Name = "corpIdColLookUpEdit";
            this.corpIdColLookUpEdit.NullText = "";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1628, 627);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcInOut;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1628, 627);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
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
            this.cmdExcel.Location = new System.Drawing.Point(1414, 0);
            this.cmdExcel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(122, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 40;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // Frm_ST_StockInOutLIst
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1628, 667);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "Frm_ST_StockInOutLIst";
            this.Text = "입출고현황";
            this.Load += new System.EventHandler(this.frmStockDaily2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcInOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.authorCheckedComboBoxEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoColPictureEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useYnColCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deptColTreeListLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTreeListLookUpEdit1TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corpIdColLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton smtSearchButton1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcInOut;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInOut;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit authorCheckedComboBoxEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit photoColPictureEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit useYnColCheckEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTreeListLookUpEdit deptColTreeListLookUpEdit;
        private DevExpress.XtraTreeList.TreeList repositoryItemTreeListLookUpEdit1TreeList;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit corpIdColLookUpEdit;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchLookUp Lup_Item;
        private baseSMT.smtSearchLookUp Lup_House;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton cmdExcel;
    }
}