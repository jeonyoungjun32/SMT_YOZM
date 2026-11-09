namespace SmartMES.View.QM
{
    partial class Frm_QM_WorkDefectList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_WorkDefectList));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdSearch = new baseSMT.smtSearchButton();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.LupItemType = new baseSMT.smtSearchLookUp();
            this.ssCustomer = new baseSMT.smtSearchLookUp();
            this.barEdit = new baseSMT.smtSearchDatetime();
            this.CmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcSales = new DevExpress.XtraGrid.GridControl();
            this.gvSales = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ItemTypeLookUp = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTypeLookUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdSearch);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.LupItemType);
            this.smtCommonBar1.Controls.Add(this.ssCustomer);
            this.smtCommonBar1.Controls.Add(this.barEdit);
            this.smtCommonBar1.Controls.Add(this.CmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1420, 45);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "불량현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 1;
            // 
            // cmdSearch
            // 
            this.cmdSearch.AllowFocus = false;
            this.cmdSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdSearch.Appearance.Options.UseBackColor = true;
            this.cmdSearch.Appearance.Options.UseForeColor = true;
            this.cmdSearch.AutoSize = true;
            this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdSearch.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdSearch.ImageOptions.ImageIndex = 0;
            this.cmdSearch.ImageOptions.ImageToTextIndent = 15;
            this.cmdSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdSearch.Location = new System.Drawing.Point(917, 0);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSearch.Size = new System.Drawing.Size(77, 45);
            this.cmdSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdSearch.TabIndex = 26;
            this.cmdSearch.Text = "조회";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
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
            this.cmdExcel.Location = new System.Drawing.Point(1242, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.Size = new System.Drawing.Size(101, 45);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 25;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // LupItemType
            // 
            this.LupItemType.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupItemType.Location = new System.Drawing.Point(681, 0);
            this.LupItemType.LookAndFeel.SkinName = "WXI";
            this.LupItemType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupItemType.Name = "LupItemType";
            this.LupItemType.Size = new System.Drawing.Size(236, 45);
            this.LupItemType.smtLabelTitleValue = "품목구분";
            this.LupItemType.smtLabelTitleVisible = true;
            this.LupItemType.smtLabelTitleWidth = 80;
            this.LupItemType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupItemType.smtLookUpDataDisplayMember = "코드명";
            this.LupItemType.smtLookUpDataSource = null;
            this.LupItemType.smtLookUpDataValueMember = "코드";
            this.LupItemType.smtLookUpSelectdIndex = -1;
            this.LupItemType.smtLookUpText = "";
            this.LupItemType.smtLookUpValue = "";
            this.LupItemType.TabIndex = 22;
            // 
            // ssCustomer
            // 
            this.ssCustomer.Dock = System.Windows.Forms.DockStyle.Left;
            this.ssCustomer.Location = new System.Drawing.Point(445, 0);
            this.ssCustomer.LookAndFeel.SkinName = "WXI";
            this.ssCustomer.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ssCustomer.Name = "ssCustomer";
            this.ssCustomer.Size = new System.Drawing.Size(236, 45);
            this.ssCustomer.smtLabelTitleValue = "거래처";
            this.ssCustomer.smtLabelTitleVisible = true;
            this.ssCustomer.smtLabelTitleWidth = 80;
            this.ssCustomer.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ssCustomer.smtLookUpDataDisplayMember = "";
            this.ssCustomer.smtLookUpDataSource = null;
            this.ssCustomer.smtLookUpDataValueMember = "";
            this.ssCustomer.smtLookUpSelectdIndex = -1;
            this.ssCustomer.smtLookUpText = "";
            this.ssCustomer.smtLookUpValue = "";
            this.ssCustomer.TabIndex = 17;
            // 
            // barEdit
            // 
            this.barEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.barEdit.Location = new System.Drawing.Point(100, 0);
            this.barEdit.LookAndFeel.SkinName = "WXI";
            this.barEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barEdit.Name = "barEdit";
            this.barEdit.Size = new System.Drawing.Size(345, 45);
            this.barEdit.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.barEdit.smtComboTitleItems = new string[] {
        "납기일자",
        "발주일자"};
            this.barEdit.smtComboTitleValue = "실적일자";
            this.barEdit.smtComboTitleVisible = true;
            this.barEdit.smtComboTitleWidth = 100;
            this.barEdit.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.barEdit.smtEndDateTimeValue = new System.DateTime(2025, 1, 15, 16, 46, 7, 142);
            this.barEdit.smtEndDateTimeVisible = true;
            this.barEdit.smtLabelTitleValue = "납기일자";
            this.barEdit.smtLabelTitleVisible = false;
            this.barEdit.smtLabelTitleWidth = 100;
            this.barEdit.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.barEdit.smtStartDateTimeValue = new System.DateTime(2025, 1, 15, 16, 46, 7, 142);
            this.barEdit.smtStartDateTimeVisible = true;
            this.barEdit.TabIndex = 6;
            // 
            // CmdClose
            // 
            this.CmdClose.AllowFocus = false;
            this.CmdClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.CmdClose.Appearance.ForeColor = System.Drawing.Color.White;
            this.CmdClose.Appearance.Options.UseBackColor = true;
            this.CmdClose.Appearance.Options.UseForeColor = true;
            this.CmdClose.AutoSize = true;
            this.CmdClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.CmdClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.CmdClose.ImageOptions.ImageIndex = 5;
            this.CmdClose.ImageOptions.ImageToTextIndent = 15;
            this.CmdClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.CmdClose.Location = new System.Drawing.Point(1343, 0);
            this.CmdClose.Name = "CmdClose";
            this.CmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.CmdClose.Size = new System.Drawing.Size(77, 45);
            this.CmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.CmdClose.TabIndex = 1;
            this.CmdClose.Text = "닫기";
            this.CmdClose.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcSales);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(907, 256, 822, 400);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1420, 604);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcSales
            // 
            this.gcSales.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gcSales.Location = new System.Drawing.Point(3, 3);
            this.gcSales.MainView = this.gvSales;
            this.gcSales.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcSales.Name = "gcSales";
            this.gcSales.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTypeLookUp});
            this.gcSales.Size = new System.Drawing.Size(1414, 598);
            this.gcSales.TabIndex = 39;
            this.gcSales.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSales});
            // 
            // gvSales
            // 
            this.gvSales.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvSales.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSales.ColumnPanelRowHeight = 25;
            this.gvSales.DetailHeight = 425;
            this.gvSales.GridControl = this.gcSales;
            this.gvSales.Name = "gvSales";
            this.gvSales.OptionsBehavior.Editable = false;
            this.gvSales.OptionsView.ColumnAutoWidth = false;
            this.gvSales.OptionsView.ShowAutoFilterRow = true;
            this.gvSales.OptionsView.ShowGroupPanel = false;
            this.gvSales.OptionsView.ShowIndicator = false;
            this.gvSales.RowHeight = 25;
            // 
            // ItemTypeLookUp
            // 
            this.ItemTypeLookUp.AutoHeight = false;
            this.ItemTypeLookUp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ItemTypeLookUp.Name = "ItemTypeLookUp";
            this.ItemTypeLookUp.NullText = "";
            this.ItemTypeLookUp.PopupView = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1420, 604);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcSales;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1420, 604);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmDefectList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1420, 649);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_QM_WorkDefectList";
            this.Text = "불량현황";
            this.Load += new System.EventHandler(this.Frm_QM_WorkDefectList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTypeLookUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchDatetime barEdit;
        private baseSMT.smtSearchButton CmdClose;
        private baseSMT.smtSearchLookUp ssCustomer;
        private baseSMT.smtSearchLookUp LupItemType;
        private baseSMT.smtSearchButton cmdExcel;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private baseSMT.smtSearchButton cmdSearch;
        private DevExpress.XtraGrid.GridControl gcSales;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSales;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit ItemTypeLookUp;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}