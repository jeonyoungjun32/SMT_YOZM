namespace SmartMES.View.MM
{
    partial class Frm_MM_PlacingMaterial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_PlacingMaterial));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.smtSearchButton2 = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.LupCustomer = new baseSMT.smtSearchLookUp();
            this.GcPlacing = new DevExpress.XtraGrid.GridControl();
            this.GvPlacing = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcPlacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPlacing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.smtCommonBar1);
            this.layoutControl1.Controls.Add(this.GcPlacing);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1748, 297, 812, 500);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1106, 647);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton2);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.LupCustomer);
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1106, 45);
            this.smtCommonBar1.smtImageTitleAutoText = false;
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "layoutControl1";
            this.smtCommonBar1.smtImageTitleVisible = false;
            this.smtCommonBar1.TabIndex = 178;
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
            this.smtSearchButton1.ImageOptions.ImageIndex = 1;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(952, 0);
            this.smtSearchButton1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 45);
            this.smtSearchButton1.TabIndex = 18;
            this.smtSearchButton1.Text = "등록";
            this.smtSearchButton1.Click += new System.EventHandler(this.btn_Check_Click);
            // 
            // smtSearchButton2
            // 
            this.smtSearchButton2.AllowFocus = false;
            this.smtSearchButton2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton2.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton2.Appearance.Options.UseBackColor = true;
            this.smtSearchButton2.Appearance.Options.UseForeColor = true;
            this.smtSearchButton2.AutoSize = true;
            this.smtSearchButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton2.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton2.ImageOptions.ImageIndex = 5;
            this.smtSearchButton2.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton2.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton2.Location = new System.Drawing.Point(1029, 0);
            this.smtSearchButton2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.smtSearchButton2.Name = "smtSearchButton2";
            this.smtSearchButton2.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.smtSearchButton2.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton2.Size = new System.Drawing.Size(77, 45);
            this.smtSearchButton2.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.smtSearchButton2.TabIndex = 17;
            this.smtSearchButton2.Text = "닫기";
            this.smtSearchButton2.Click += new System.EventHandler(this.btn_Close_Click);
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
            this.btn_Search.Location = new System.Drawing.Point(287, 0);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 45);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 16;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // LupCustomer
            // 
            this.LupCustomer.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupCustomer.Location = new System.Drawing.Point(0, 0);
            this.LupCustomer.LookAndFeel.SkinName = "WXI";
            this.LupCustomer.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupCustomer.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LupCustomer.Name = "LupCustomer";
            this.LupCustomer.PopMode = false;
            this.LupCustomer.Size = new System.Drawing.Size(287, 45);
            this.LupCustomer.smtLabelTitleValue = "거래처";
            this.LupCustomer.smtLabelTitleVisible = true;
            this.LupCustomer.smtLabelTitleWidth = 70;
            this.LupCustomer.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupCustomer.smtLookUpDataDisplayMember = "코드명";
            this.LupCustomer.smtLookUpDataSource = null;
            this.LupCustomer.smtLookUpDataValueMember = "코드";
            this.LupCustomer.smtLookUpSelectdIndex = -1;
            this.LupCustomer.smtLookUpText = "";
            this.LupCustomer.smtLookUpValue = "";
            this.LupCustomer.TabIndex = 14;
            // 
            // GcPlacing
            // 
            this.GcPlacing.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.GcPlacing.Location = new System.Drawing.Point(7, 46);
            this.GcPlacing.MainView = this.GvPlacing;
            this.GcPlacing.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.GcPlacing.Name = "GcPlacing";
            this.GcPlacing.Size = new System.Drawing.Size(1092, 594);
            this.GcPlacing.TabIndex = 53;
            this.GcPlacing.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvPlacing});
            // 
            // GvPlacing
            // 
            this.GvPlacing.DetailHeight = 371;
            this.GvPlacing.GridControl = this.GcPlacing;
            this.GvPlacing.Name = "GvPlacing";
            this.GvPlacing.OptionsSelection.MultiSelect = true;
            this.GvPlacing.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.GvPlacing.OptionsView.ShowAutoFilterRow = true;
            this.GvPlacing.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1106, 647);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 39);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1106, 608);
            this.layoutControlGroup1.Text = "품목선택";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.GcPlacing;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1098, 600);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.smtCommonBar1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Size = new System.Drawing.Size(1106, 39);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // Frm_MM_PlacingMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 647);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.Name = "Frm_MM_PlacingMaterial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "발주품목선택";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcPlacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPlacing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl GcPlacing;
        private DevExpress.XtraGrid.Views.Grid.GridView GvPlacing;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private baseSMT.smtCommonBar smtCommonBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private baseSMT.smtSearchLookUp LupCustomer;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtSearchButton smtSearchButton2;
	}
}