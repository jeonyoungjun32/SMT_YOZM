namespace SmartMES.View.CM
{
    partial class Frm_CM_Warehouse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_Warehouse));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcCM_Warehouse = new DevExpress.XtraGrid.GridControl();
            this.gvCM_Warehouse = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.custTypeColLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.creditCodeColLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.wareTypeColLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.wareKindColLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.corpIdColLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.useYnColCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdInsert = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCM_Warehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCM_Warehouse)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.custTypeColLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.creditCodeColLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wareTypeColLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wareKindColLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corpIdColLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.useYnColCheckEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcCM_Warehouse);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1067, 580);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcCM_Warehouse
            // 
            this.gcCM_Warehouse.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.gcCM_Warehouse.Location = new System.Drawing.Point(7, 7);
            this.gcCM_Warehouse.MainView = this.gvCM_Warehouse;
            this.gcCM_Warehouse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcCM_Warehouse.Name = "gcCM_Warehouse";
            this.gcCM_Warehouse.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.custTypeColLookUpEdit,
            this.creditCodeColLookUpEdit,
            this.wareTypeColLookUpEdit1,
            this.wareKindColLookUpEdit1,
            this.corpIdColLookUpEdit1,
            this.useYnColCheckEdit});
            this.gcCM_Warehouse.Size = new System.Drawing.Size(1053, 566);
            this.gcCM_Warehouse.TabIndex = 173;
            this.gcCM_Warehouse.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCM_Warehouse});
            // 
            // gvCM_Warehouse
            // 
            this.gvCM_Warehouse.ColumnPanelRowHeight = 36;
            this.gvCM_Warehouse.DetailHeight = 495;
            this.gvCM_Warehouse.FixedLineWidth = 4;
            this.gvCM_Warehouse.GridControl = this.gcCM_Warehouse;
            this.gvCM_Warehouse.Name = "gvCM_Warehouse";
            this.gvCM_Warehouse.OptionsDetail.EnableMasterViewMode = false;
            this.gvCM_Warehouse.OptionsView.ColumnAutoWidth = false;
            this.gvCM_Warehouse.OptionsView.ShowAutoFilterRow = true;
            this.gvCM_Warehouse.OptionsView.ShowGroupPanel = false;
            this.gvCM_Warehouse.RowHeight = 36;
            this.gvCM_Warehouse.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvCM_Warehouse_FocusedRowChanged);
            // 
            // custTypeColLookUpEdit
            // 
            this.custTypeColLookUpEdit.AutoHeight = false;
            this.custTypeColLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.custTypeColLookUpEdit.Name = "custTypeColLookUpEdit";
            this.custTypeColLookUpEdit.NullText = "";
            // 
            // creditCodeColLookUpEdit
            // 
            this.creditCodeColLookUpEdit.AutoHeight = false;
            this.creditCodeColLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.creditCodeColLookUpEdit.Name = "creditCodeColLookUpEdit";
            this.creditCodeColLookUpEdit.NullText = "";
            // 
            // wareTypeColLookUpEdit1
            // 
            this.wareTypeColLookUpEdit1.AutoHeight = false;
            this.wareTypeColLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wareTypeColLookUpEdit1.Name = "wareTypeColLookUpEdit1";
            this.wareTypeColLookUpEdit1.NullText = "";
            // 
            // wareKindColLookUpEdit1
            // 
            this.wareKindColLookUpEdit1.AutoHeight = false;
            this.wareKindColLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wareKindColLookUpEdit1.Name = "wareKindColLookUpEdit1";
            this.wareKindColLookUpEdit1.NullText = "";
            // 
            // corpIdColLookUpEdit1
            // 
            this.corpIdColLookUpEdit1.AutoHeight = false;
            this.corpIdColLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.corpIdColLookUpEdit1.Name = "corpIdColLookUpEdit1";
            this.corpIdColLookUpEdit1.NullText = "";
            // 
            // useYnColCheckEdit
            // 
            this.useYnColCheckEdit.AutoHeight = false;
            this.useYnColCheckEdit.Name = "useYnColCheckEdit";
            this.useYnColCheckEdit.ValueChecked = "Y";
            this.useYnColCheckEdit.ValueUnchecked = "N";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1067, 580);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1067, 580);
            this.layoutControlGroup1.Text = "창고 리스트";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcCM_Warehouse;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1059, 572);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdInsert);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.cmdDelete);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1067, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "창고관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 1;
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
            this.cmdInsert.Location = new System.Drawing.Point(658, 0);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdInsert.Size = new System.Drawing.Size(77, 40);
            this.cmdInsert.smtButtonAutoText = false;
            this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Tag = "Ins";
            this.cmdInsert.Text = "등록";
            this.cmdInsert.Visible = false;
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
            this.cmdSave.Location = new System.Drawing.Point(735, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "저장";
            this.cmdSave.Visible = false;
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
            this.cmdDelete.Location = new System.Drawing.Point(812, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(77, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Tag = "Del";
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Visible = false;
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
            this.cmdExcel.Location = new System.Drawing.Point(889, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(101, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 2;
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
            this.cmdClose.Location = new System.Drawing.Point(990, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmWarehouse
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1067, 620);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_CM_Warehouse";
            this.Text = "창고관리";
            this.Load += new System.EventHandler(this.Frm_CM_Warehouse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCM_Warehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCM_Warehouse)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.custTypeColLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.creditCodeColLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wareTypeColLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wareKindColLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corpIdColLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.useYnColCheckEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcCM_Warehouse;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCM_Warehouse;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit custTypeColLookUpEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit creditCodeColLookUpEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit wareTypeColLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit wareKindColLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit corpIdColLookUpEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit useYnColCheckEdit;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdInsert;
        private baseSMT.smtSearchButton cmdSave;
        private baseSMT.smtSearchButton cmdDelete;
        private baseSMT.smtSearchButton cmdExcel;
        private baseSMT.smtSearchButton cmdClose;
    }
}