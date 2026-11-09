
namespace SmartMES.View.Lookup
{
    partial class Frm_SelectPM_ProductPlan
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SelectPM_ProductPlan));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.GcPM_OrderProcess = new DevExpress.XtraGrid.GridControl();
			this.GvPM_OrderProcess = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.BtnSelect = new baseSMT.smtSearchButton();
			this.smtSearchButton1 = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GcPM_OrderProcess)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvPM_OrderProcess)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.GcPM_OrderProcess);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1249, 694);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// GcPM_OrderProcess
			// 
			this.GcPM_OrderProcess.Location = new System.Drawing.Point(3, 3);
			this.GcPM_OrderProcess.MainView = this.GvPM_OrderProcess;
			this.GcPM_OrderProcess.Name = "GcPM_OrderProcess";
			this.GcPM_OrderProcess.Size = new System.Drawing.Size(1243, 688);
			this.GcPM_OrderProcess.TabIndex = 6;
			this.GcPM_OrderProcess.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvPM_OrderProcess});
			// 
			// GvPM_OrderProcess
			// 
			this.GvPM_OrderProcess.DetailHeight = 331;
			this.GvPM_OrderProcess.GridControl = this.GcPM_OrderProcess;
			this.GvPM_OrderProcess.Name = "GvPM_OrderProcess";
			this.GvPM_OrderProcess.OptionsView.ShowGroupPanel = false;
			this.GvPM_OrderProcess.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GvPM_OrderProcess_RowCellStyle);
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1249, 694);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.GcPM_OrderProcess;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1249, 694);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.BtnSelect);
			this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1249, 40);
			this.smtCommonBar1.smtImageTitleAutoText = false;
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "작업지시 리스트";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 1;
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
			this.BtnSelect.Location = new System.Drawing.Point(1095, 0);
			this.BtnSelect.Name = "BtnSelect";
			this.BtnSelect.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.BtnSelect.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnSelect.Size = new System.Drawing.Size(77, 40);
			this.BtnSelect.smtButtonAutoText = false;
			this.BtnSelect.smtButtonStyle = baseSMT.EnumButtonStyles.확정;
			this.BtnSelect.TabIndex = 2;
			this.BtnSelect.Text = "선택";
			this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
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
			this.smtSearchButton1.Location = new System.Drawing.Point(1172, 0);
			this.smtSearchButton1.Name = "smtSearchButton1";
			this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
			this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.smtSearchButton1.TabIndex = 1;
			this.smtSearchButton1.Text = "닫기";
			this.smtSearchButton1.Click += new System.EventHandler(this.smtSearchButton1_Click);
			// 
			// frmSelectPM_ProductPlan
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1249, 734);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.LookAndFeel.SkinName = "WXI";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Name = "Frm_SelectPM_ProductPlan";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "작업지시리스트";
			this.Load += new System.EventHandler(this.Frm_SelectPM_ProductPlan_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GcPM_OrderProcess)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvPM_OrderProcess)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl GcPM_OrderProcess;
        private DevExpress.XtraGrid.Views.Grid.GridView GvPM_OrderProcess;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton BtnSelect;
        private baseSMT.smtSearchButton smtSearchButton1;
    }
}