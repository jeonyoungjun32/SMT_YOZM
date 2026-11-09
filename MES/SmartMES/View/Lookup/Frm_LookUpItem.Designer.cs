
namespace SmartMES.View.Lookup
{
    partial class Frm_LookUpItem
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LookUpItem));
			this.smtCommonBar2 = new baseSMT.smtCommonBar();
			this.BtnClose = new baseSMT.smtSearchButton();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.GcItem = new DevExpress.XtraGrid.GridControl();
			this.GvItem = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.BtnSelect = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).BeginInit();
			this.smtCommonBar2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GcItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvItem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// smtCommonBar2
			// 
			this.smtCommonBar2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar2.Appearance.Options.UseBackColor = true;
			this.smtCommonBar2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar2.Controls.Add(this.BtnSelect);
			this.smtCommonBar2.Controls.Add(this.BtnClose);
			this.smtCommonBar2.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar2.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar2.Name = "smtCommonBar2";
			this.smtCommonBar2.Size = new System.Drawing.Size(964, 40);
			this.smtCommonBar2.smtImageTitleAutoText = false;
			this.smtCommonBar2.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar2.smtImageTitleSvgImage")));
			this.smtCommonBar2.smtImageTitleText = "품목선택";
			this.smtCommonBar2.smtImageTitleVisible = true;
			this.smtCommonBar2.TabIndex = 94;
			// 
			// BtnClose
			// 
			this.BtnClose.AllowFocus = false;
			this.BtnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.BtnClose.Appearance.ForeColor = System.Drawing.Color.White;
			this.BtnClose.Appearance.Options.UseBackColor = true;
			this.BtnClose.Appearance.Options.UseForeColor = true;
			this.BtnClose.AutoSize = true;
			this.BtnClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtnClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.BtnClose.ImageOptions.ImageIndex = 5;
			this.BtnClose.ImageOptions.ImageToTextIndent = 15;
			this.BtnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.BtnClose.Location = new System.Drawing.Point(887, 0);
			this.BtnClose.Name = "BtnClose";
			this.BtnClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.BtnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnClose.Size = new System.Drawing.Size(77, 40);
			this.BtnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.BtnClose.TabIndex = 1;
			this.BtnClose.Text = "닫기";
			this.BtnClose.Click += new System.EventHandler(this.smtSearchButton1_Click);
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.GcItem);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(964, 562);
			this.layoutControl1.TabIndex = 95;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// GcItem
			// 
			this.GcItem.Location = new System.Drawing.Point(16, 16);
			this.GcItem.MainView = this.GvItem;
			this.GcItem.Name = "GcItem";
			this.GcItem.Size = new System.Drawing.Size(932, 530);
			this.GcItem.TabIndex = 4;
			this.GcItem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvItem});
			// 
			// GvItem
			// 
			this.GvItem.GridControl = this.GcItem;
			this.GvItem.Name = "GvItem";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.Root.Name = "Root";
			this.Root.Size = new System.Drawing.Size(964, 562);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.GcItem;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(938, 536);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// BtnSelect
			// 
			this.BtnSelect.AllowFocus = false;
			this.BtnSelect.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.BtnSelect.Appearance.ForeColor = System.Drawing.Color.White;
			this.BtnSelect.Appearance.Options.UseBackColor = true;
			this.BtnSelect.Appearance.Options.UseForeColor = true;
			this.BtnSelect.AutoSize = true;
			this.BtnSelect.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtnSelect.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.BtnSelect.ImageOptions.ImageIndex = 6;
			this.BtnSelect.ImageOptions.ImageToTextIndent = 15;
			this.BtnSelect.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.BtnSelect.Location = new System.Drawing.Point(810, 0);
			this.BtnSelect.Name = "BtnSelect";
			this.BtnSelect.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.BtnSelect.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnSelect.Size = new System.Drawing.Size(77, 40);
			this.BtnSelect.smtButtonStyle = baseSMT.EnumButtonStyles.확정;
			this.BtnSelect.TabIndex = 3;
			this.BtnSelect.Text = "선택";
			this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
			// 
			// Frm_LookUpItem
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(964, 602);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.LookAndFeel.SkinName = "WXI";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Name = "Frm_LookUpItem";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.Frm_LookUpItem_Load);
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).EndInit();
			this.smtCommonBar2.ResumeLayout(false);
			this.smtCommonBar2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GcItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvItem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar2;
        private baseSMT.smtSearchButton BtnClose;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraGrid.GridControl GcItem;
		private DevExpress.XtraGrid.Views.Grid.GridView GvItem;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private baseSMT.smtSearchButton BtnSelect;
	}
}