namespace SmartMES.View.MM
{
    partial class Frm_SelectMaterial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SelectMaterial));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.smtCommonBar2 = new baseSMT.smtCommonBar();
            this.btn_Check = new baseSMT.smtSearchButton();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.checkBoxALL = new System.Windows.Forms.CheckBox();
            this.btn_Ins = new baseSMT.smtSearchButton();
            this.GcMaterial = new DevExpress.XtraGrid.GridControl();
            this.GvMaterial = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GcProduct = new DevExpress.XtraGrid.GridControl();
            this.GvProduct = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).BeginInit();
            this.smtCommonBar2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.smtCommonBar2);
            this.layoutControl1.Controls.Add(this.smtCommonBar1);
            this.layoutControl1.Controls.Add(this.GcMaterial);
            this.layoutControl1.Controls.Add(this.GcProduct);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1748, 297, 812, 500);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1375, 647);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // smtCommonBar2
            // 
            this.smtCommonBar2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar2.Appearance.Options.UseBackColor = true;
            this.smtCommonBar2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar2.Controls.Add(this.btn_Check);
            this.smtCommonBar2.Controls.Add(this.btn_Close);
            this.smtCommonBar2.Location = new System.Drawing.Point(943, 3);
            this.smtCommonBar2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.smtCommonBar2.Name = "smtCommonBar2";
            this.smtCommonBar2.Size = new System.Drawing.Size(429, 45);
            this.smtCommonBar2.smtImageTitleSvgImage = null;
            this.smtCommonBar2.smtImageTitleText = "layoutControl1";
            this.smtCommonBar2.smtImageTitleVisible = false;
            this.smtCommonBar2.TabIndex = 177;
            // 
            // btn_Check
            // 
            this.btn_Check.AllowFocus = false;
            this.btn_Check.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Check.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Check.Appearance.Options.UseBackColor = true;
            this.btn_Check.Appearance.Options.UseForeColor = true;
            this.btn_Check.AutoSize = true;
            this.btn_Check.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Check.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Check.ImageOptions.ImageIndex = 1;
            this.btn_Check.ImageOptions.ImageToTextIndent = 15;
            this.btn_Check.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Check.Location = new System.Drawing.Point(275, 0);
            this.btn_Check.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Check.Name = "btn_Check";
            this.btn_Check.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.btn_Check.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Check.Size = new System.Drawing.Size(77, 45);
            this.btn_Check.TabIndex = 11;
            this.btn_Check.Text = "등록";
            this.btn_Check.Click += new System.EventHandler(this.btn_Check_Click);
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
            this.btn_Close.Location = new System.Drawing.Point(352, 0);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(77, 45);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 10;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.checkBoxALL);
            this.smtCommonBar1.Controls.Add(this.btn_Ins);
            this.smtCommonBar1.Location = new System.Drawing.Point(3, 3);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(918, 45);
            this.smtCommonBar1.smtImageTitleAutoText = false;
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "layoutControl1";
            this.smtCommonBar1.smtImageTitleVisible = false;
            this.smtCommonBar1.TabIndex = 178;
            // 
            // checkBoxALL
            // 
            this.checkBoxALL.AutoSize = true;
            this.checkBoxALL.Checked = true;
            this.checkBoxALL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxALL.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.checkBoxALL.ForeColor = System.Drawing.Color.White;
            this.checkBoxALL.Location = new System.Drawing.Point(13, 10);
            this.checkBoxALL.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkBoxALL.Name = "checkBoxALL";
            this.checkBoxALL.Size = new System.Drawing.Size(56, 23);
            this.checkBoxALL.TabIndex = 3;
            this.checkBoxALL.Text = "전체";
            this.checkBoxALL.UseVisualStyleBackColor = true;
            this.checkBoxALL.CheckedChanged += new System.EventHandler(this.checkBoxALL_CheckedChanged);
            // 
            // btn_Ins
            // 
            this.btn_Ins.AllowFocus = false;
            this.btn_Ins.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Ins.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Ins.Appearance.Options.UseBackColor = true;
            this.btn_Ins.Appearance.Options.UseForeColor = true;
            this.btn_Ins.AutoSize = true;
            this.btn_Ins.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Ins.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Ins.ImageOptions.ImageIndex = 6;
            this.btn_Ins.ImageOptions.ImageToTextIndent = 15;
            this.btn_Ins.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Ins.Location = new System.Drawing.Point(841, 0);
            this.btn_Ins.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Ins.Name = "btn_Ins";
            this.btn_Ins.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.btn_Ins.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Ins.Size = new System.Drawing.Size(77, 45);
            this.btn_Ins.smtButtonStyle = baseSMT.EnumButtonStyles.확정;
            this.btn_Ins.TabIndex = 2;
            this.btn_Ins.Text = "선택";
            this.btn_Ins.Click += new System.EventHandler(this.btn_Ins_Click);
            // 
            // GcMaterial
            // 
            this.GcMaterial.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.GcMaterial.Location = new System.Drawing.Point(7, 78);
            this.GcMaterial.MainView = this.GvMaterial;
            this.GcMaterial.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.GcMaterial.Name = "GcMaterial";
            this.GcMaterial.Size = new System.Drawing.Size(910, 562);
            this.GcMaterial.TabIndex = 53;
            this.GcMaterial.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvMaterial});
            this.GcMaterial.DoubleClick += new System.EventHandler(this.GcMaterial_DoubleClick);
            // 
            // GvMaterial
            // 
            this.GvMaterial.DetailHeight = 371;
            this.GvMaterial.GridControl = this.GcMaterial;
            this.GvMaterial.Name = "GvMaterial";
            this.GvMaterial.OptionsSelection.MultiSelect = true;
            this.GvMaterial.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.GvMaterial.OptionsView.ShowAutoFilterRow = true;
            this.GvMaterial.OptionsView.ShowGroupPanel = false;
            // 
            // GcProduct
            // 
            this.GcProduct.Cursor = System.Windows.Forms.Cursors.Default;
            this.GcProduct.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.GcProduct.Location = new System.Drawing.Point(943, 47);
            this.GcProduct.MainView = this.GvProduct;
            this.GcProduct.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.GcProduct.Name = "GcProduct";
            this.GcProduct.Size = new System.Drawing.Size(429, 597);
            this.GcProduct.TabIndex = 51;
            this.GcProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvProduct});
            // 
            // GvProduct
            // 
            this.GvProduct.ColumnPanelRowHeight = 31;
            this.GvProduct.DetailHeight = 450;
            this.GvProduct.GridControl = this.GcProduct;
            this.GvProduct.Name = "GvProduct";
            this.GvProduct.OptionsDetail.EnableMasterViewMode = false;
            this.GvProduct.OptionsSelection.CheckBoxSelectorColumnWidth = 40;
            this.GvProduct.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.GvProduct.OptionsView.ColumnAutoWidth = false;
            this.GvProduct.OptionsView.ShowGroupPanel = false;
            this.GvProduct.OptionsView.ShowIndicator = false;
            this.GvProduct.RowHeight = 31;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.splitterItem2,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1375, 647);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 44);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(924, 603);
            this.layoutControlGroup1.Text = "품목선택";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.GcMaterial;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(916, 568);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(924, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(16, 647);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.smtCommonBar1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(924, 44);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.smtCommonBar2;
            this.layoutControlItem7.Location = new System.Drawing.Point(940, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(435, 44);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.GcProduct;
            this.layoutControlItem1.Location = new System.Drawing.Point(940, 44);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(435, 603);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Frm_SelectMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 647);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "Frm_SelectMaterial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "품목선택";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).EndInit();
            this.smtCommonBar2.ResumeLayout(false);
            this.smtCommonBar2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl GcProduct;
        private DevExpress.XtraGrid.Views.Grid.GridView GvProduct;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl GcMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView GvMaterial;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private baseSMT.smtCommonBar smtCommonBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private baseSMT.smtCommonBar smtCommonBar2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private baseSMT.smtSearchButton btn_Check;
        private baseSMT.smtSearchButton btn_Close;
        private baseSMT.smtSearchButton btn_Ins;
        private System.Windows.Forms.CheckBox checkBoxALL;
    }
}