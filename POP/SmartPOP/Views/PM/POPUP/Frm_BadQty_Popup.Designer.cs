
namespace SmartPOP.Views.Work.Popup
{
    partial class Frm_BadQty_Popup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BadQty_Popup));
			this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
			this.Gc_Bad_Detail = new DevExpress.XtraGrid.GridControl();
			this.Gv_Bad_Detail = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
			this.BtnSave = new baseSMT.smtSearchButton();
			this.Lbl_GoodQty = new DevExpress.XtraEditors.LabelControl();
			this.Txt_GoodQty = new DevExpress.XtraEditors.TextEdit();
			this.Lbl_WorkQty = new DevExpress.XtraEditors.LabelControl();
			this.Txt_WorkQty = new DevExpress.XtraEditors.TextEdit();
			this.Lbl_Date = new DevExpress.XtraEditors.LabelControl();
			this.Txt_MaxBadQty = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
			this.groupControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Bad_Detail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Bad_Detail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
			this.panelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Txt_GoodQty.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Txt_WorkQty.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Txt_MaxBadQty.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// groupControl3
			// 
			this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.groupControl3.AppearanceCaption.Options.UseFont = true;
			this.groupControl3.Controls.Add(this.Gc_Bad_Detail);
			this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl3.Location = new System.Drawing.Point(0, 58);
			this.groupControl3.Margin = new System.Windows.Forms.Padding(4);
			this.groupControl3.Name = "groupControl3";
			this.groupControl3.Size = new System.Drawing.Size(895, 549);
			this.groupControl3.TabIndex = 275;
			// 
			// Gc_Bad_Detail
			// 
			this.Gc_Bad_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_Bad_Detail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
			this.Gc_Bad_Detail.Location = new System.Drawing.Point(2, 29);
			this.Gc_Bad_Detail.MainView = this.Gv_Bad_Detail;
			this.Gc_Bad_Detail.Margin = new System.Windows.Forms.Padding(4);
			this.Gc_Bad_Detail.Name = "Gc_Bad_Detail";
			this.Gc_Bad_Detail.Size = new System.Drawing.Size(891, 518);
			this.Gc_Bad_Detail.TabIndex = 269;
			this.Gc_Bad_Detail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Bad_Detail});
			// 
			// Gv_Bad_Detail
			// 
			this.Gv_Bad_Detail.DetailHeight = 420;
			this.Gv_Bad_Detail.GridControl = this.Gc_Bad_Detail;
			this.Gv_Bad_Detail.Name = "Gv_Bad_Detail";
			this.Gv_Bad_Detail.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.True;
			this.Gv_Bad_Detail.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
			this.Gv_Bad_Detail.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;
			this.Gv_Bad_Detail.OptionsEditForm.PopupEditFormWidth = 1029;
			this.Gv_Bad_Detail.OptionsView.ShowGroupPanel = false;
			// 
			// panelControl3
			// 
			this.panelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.panelControl3.Appearance.Options.UseBackColor = true;
			this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl3.Controls.Add(this.BtnSave);
			this.panelControl3.Controls.Add(this.Lbl_GoodQty);
			this.panelControl3.Controls.Add(this.Txt_GoodQty);
			this.panelControl3.Controls.Add(this.Lbl_WorkQty);
			this.panelControl3.Controls.Add(this.Txt_WorkQty);
			this.panelControl3.Controls.Add(this.Lbl_Date);
			this.panelControl3.Controls.Add(this.Txt_MaxBadQty);
			this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl3.Location = new System.Drawing.Point(0, 0);
			this.panelControl3.Margin = new System.Windows.Forms.Padding(4);
			this.panelControl3.Name = "panelControl3";
			this.panelControl3.Size = new System.Drawing.Size(895, 58);
			this.panelControl3.TabIndex = 270;
			// 
			// BtnSave
			// 
			this.BtnSave.AllowFocus = false;
			this.BtnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.BtnSave.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 16F);
			this.BtnSave.Appearance.ForeColor = System.Drawing.Color.White;
			this.BtnSave.Appearance.Options.UseBackColor = true;
			this.BtnSave.Appearance.Options.UseFont = true;
			this.BtnSave.Appearance.Options.UseForeColor = true;
			this.BtnSave.AutoSize = true;
			this.BtnSave.Dock = System.Windows.Forms.DockStyle.Right;
			this.BtnSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.BtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.ImageOptions.Image")));
			this.BtnSave.ImageOptions.ImageIndex = 4;
			this.BtnSave.ImageOptions.ImageToTextIndent = 15;
			this.BtnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.BtnSave.Location = new System.Drawing.Point(791, 0);
			this.BtnSave.Margin = new System.Windows.Forms.Padding(4);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Padding = new System.Windows.Forms.Padding(11, 0, 11, 0);
			this.BtnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.BtnSave.Size = new System.Drawing.Size(104, 58);
			this.BtnSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.BtnSave.TabIndex = 300;
			this.BtnSave.Text = "저장";
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// Lbl_GoodQty
			// 
			this.Lbl_GoodQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 16F);
			this.Lbl_GoodQty.Appearance.ForeColor = System.Drawing.Color.White;
			this.Lbl_GoodQty.Appearance.Options.UseFont = true;
			this.Lbl_GoodQty.Appearance.Options.UseForeColor = true;
			this.Lbl_GoodQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
			this.Lbl_GoodQty.Location = new System.Drawing.Point(251, 13);
			this.Lbl_GoodQty.Margin = new System.Windows.Forms.Padding(4);
			this.Lbl_GoodQty.Name = "Lbl_GoodQty";
			this.Lbl_GoodQty.Size = new System.Drawing.Size(81, 26);
			this.Lbl_GoodQty.TabIndex = 292;
			this.Lbl_GoodQty.Text = "양품 수량";
			// 
			// Txt_GoodQty
			// 
			this.Txt_GoodQty.Enabled = false;
			this.Txt_GoodQty.Location = new System.Drawing.Point(346, 8);
			this.Txt_GoodQty.Margin = new System.Windows.Forms.Padding(0);
			this.Txt_GoodQty.Name = "Txt_GoodQty";
			this.Txt_GoodQty.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 16F);
			this.Txt_GoodQty.Properties.Appearance.Options.UseFont = true;
			this.Txt_GoodQty.Properties.AutoHeight = false;
			this.Txt_GoodQty.Properties.DisplayFormat.FormatString = "{0:#,#.#}";
			this.Txt_GoodQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.Txt_GoodQty.Properties.EditFormat.FormatString = "d";
			this.Txt_GoodQty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.Txt_GoodQty.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
			this.Txt_GoodQty.Properties.MaskSettings.Set("mask", "d");
			this.Txt_GoodQty.Properties.ReadOnly = true;
			this.Txt_GoodQty.Size = new System.Drawing.Size(141, 43);
			this.Txt_GoodQty.TabIndex = 293;
			// 
			// Lbl_WorkQty
			// 
			this.Lbl_WorkQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 16F);
			this.Lbl_WorkQty.Appearance.ForeColor = System.Drawing.Color.White;
			this.Lbl_WorkQty.Appearance.Options.UseFont = true;
			this.Lbl_WorkQty.Appearance.Options.UseForeColor = true;
			this.Lbl_WorkQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
			this.Lbl_WorkQty.Location = new System.Drawing.Point(8, 13);
			this.Lbl_WorkQty.Margin = new System.Windows.Forms.Padding(4);
			this.Lbl_WorkQty.Name = "Lbl_WorkQty";
			this.Lbl_WorkQty.Size = new System.Drawing.Size(81, 26);
			this.Lbl_WorkQty.TabIndex = 292;
			this.Lbl_WorkQty.Text = "생산 수량";
			// 
			// Txt_WorkQty
			// 
			this.Txt_WorkQty.Enabled = false;
			this.Txt_WorkQty.Location = new System.Drawing.Point(93, 8);
			this.Txt_WorkQty.Margin = new System.Windows.Forms.Padding(0);
			this.Txt_WorkQty.Name = "Txt_WorkQty";
			this.Txt_WorkQty.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 16F);
			this.Txt_WorkQty.Properties.Appearance.Options.UseFont = true;
			this.Txt_WorkQty.Properties.AutoHeight = false;
			this.Txt_WorkQty.Properties.DisplayFormat.FormatString = "{0:#,#.#}";
			this.Txt_WorkQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.Txt_WorkQty.Properties.EditFormat.FormatString = "d";
			this.Txt_WorkQty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.Txt_WorkQty.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
			this.Txt_WorkQty.Properties.MaskSettings.Set("mask", "d");
			this.Txt_WorkQty.Properties.ReadOnly = true;
			this.Txt_WorkQty.Size = new System.Drawing.Size(141, 43);
			this.Txt_WorkQty.TabIndex = 293;
			// 
			// Lbl_Date
			// 
			this.Lbl_Date.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.Lbl_Date.Location = new System.Drawing.Point(8, 73);
			this.Lbl_Date.Margin = new System.Windows.Forms.Padding(4);
			this.Lbl_Date.Name = "Lbl_Date";
			this.Lbl_Date.Size = new System.Drawing.Size(141, 28);
			this.Lbl_Date.TabIndex = 292;
			this.Lbl_Date.Text = "등록가능수량";
			this.Lbl_Date.Visible = false;
			// 
			// Txt_MaxBadQty
			// 
			this.Txt_MaxBadQty.Location = new System.Drawing.Point(543, 9);
			this.Txt_MaxBadQty.Margin = new System.Windows.Forms.Padding(0);
			this.Txt_MaxBadQty.Name = "Txt_MaxBadQty";
			this.Txt_MaxBadQty.Properties.AutoHeight = false;
			this.Txt_MaxBadQty.Properties.DisplayFormat.FormatString = "d";
			this.Txt_MaxBadQty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.Txt_MaxBadQty.Properties.EditFormat.FormatString = "d";
			this.Txt_MaxBadQty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.Txt_MaxBadQty.Properties.Mask.EditMask = "d";
			this.Txt_MaxBadQty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
			this.Txt_MaxBadQty.Size = new System.Drawing.Size(116, 28);
			this.Txt_MaxBadQty.TabIndex = 293;
			this.Txt_MaxBadQty.Visible = false;
			// 
			// Frm_BadQty_Popup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(895, 607);
			this.Controls.Add(this.groupControl3);
			this.Controls.Add(this.panelControl3);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Frm_BadQty_Popup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "유형별 불량수량";
			this.Load += new System.EventHandler(this.Frm_Load);
			((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
			this.groupControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Bad_Detail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Bad_Detail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
			this.panelControl3.ResumeLayout(false);
			this.panelControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Txt_GoodQty.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Txt_WorkQty.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Txt_MaxBadQty.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
		private DevExpress.XtraEditors.GroupControl groupControl3;
		private DevExpress.XtraGrid.GridControl Gc_Bad_Detail;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Bad_Detail;
		private DevExpress.XtraEditors.PanelControl panelControl3;
		private DevExpress.XtraEditors.LabelControl Lbl_Date;
		private DevExpress.XtraEditors.TextEdit Txt_MaxBadQty;
        private DevExpress.XtraEditors.LabelControl Lbl_GoodQty;
        private DevExpress.XtraEditors.TextEdit Txt_GoodQty;
        private DevExpress.XtraEditors.LabelControl Lbl_WorkQty;
        private DevExpress.XtraEditors.TextEdit Txt_WorkQty;
        private baseSMT.smtSearchButton BtnSave;
    }
}