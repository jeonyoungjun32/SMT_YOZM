
namespace SmartPOP.Views.Work.Popup
{
    partial class Frm_BadQty_Detail
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
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_Bad_Detail = new DevExpress.XtraGrid.GridControl();
            this.Gv_Bad_Detail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.Lbl_Good_Qty = new DevExpress.XtraEditors.LabelControl();
            this.Txt_Good_Qty = new DevExpress.XtraEditors.TextEdit();
            this.Lbl_In_Qty = new DevExpress.XtraEditors.LabelControl();
            this.Txt_In_Qty = new DevExpress.XtraEditors.TextEdit();
            this.Lbl_Date = new DevExpress.XtraEditors.LabelControl();
            this.Txt_MaxBadQty = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Bad_Detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Bad_Detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Good_Qty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_In_Qty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_MaxBadQty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            this.groupControl3.AppearanceCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.groupControl3.AppearanceCaption.Options.UseFont = true;
            this.groupControl3.Controls.Add(this.Gc_Bad_Detail);
            this.groupControl3.Controls.Add(this.panelControl3);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.ShowCaption = false;
            this.groupControl3.Size = new System.Drawing.Size(541, 564);
            this.groupControl3.TabIndex = 275;
            this.groupControl3.Text = "유형별 불량수량";
            // 
            // Gc_Bad_Detail
            // 
            this.Gc_Bad_Detail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_Bad_Detail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Gc_Bad_Detail.Location = new System.Drawing.Point(2, 45);
            this.Gc_Bad_Detail.MainView = this.Gv_Bad_Detail;
            this.Gc_Bad_Detail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Gc_Bad_Detail.Name = "Gc_Bad_Detail";
            this.Gc_Bad_Detail.Size = new System.Drawing.Size(537, 517);
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
            this.Gv_Bad_Detail.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.BtnSave);
            this.panelControl3.Controls.Add(this.Lbl_Good_Qty);
            this.panelControl3.Controls.Add(this.Txt_Good_Qty);
            this.panelControl3.Controls.Add(this.Lbl_In_Qty);
            this.panelControl3.Controls.Add(this.Txt_In_Qty);
            this.panelControl3.Controls.Add(this.Lbl_Date);
            this.panelControl3.Controls.Add(this.Txt_MaxBadQty);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(537, 43);
            this.panelControl3.TabIndex = 270;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Location = new System.Drawing.Point(443, 2);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(90, 38);
            this.BtnSave.TabIndex = 295;
            this.BtnSave.Text = "저  장";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // Lbl_Good_Qty
            // 
            this.Lbl_Good_Qty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Lbl_Good_Qty.Location = new System.Drawing.Point(8, 42);
            this.Lbl_Good_Qty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lbl_Good_Qty.Name = "Lbl_Good_Qty";
            this.Lbl_Good_Qty.Size = new System.Drawing.Size(141, 28);
            this.Lbl_Good_Qty.TabIndex = 292;
            this.Lbl_Good_Qty.Text = "양품 수량";
            this.Lbl_Good_Qty.Visible = false;
            // 
            // Txt_Good_Qty
            // 
            this.Txt_Good_Qty.Enabled = false;
            this.Txt_Good_Qty.Location = new System.Drawing.Point(168, 42);
            this.Txt_Good_Qty.Margin = new System.Windows.Forms.Padding(0);
            this.Txt_Good_Qty.Name = "Txt_Good_Qty";
            this.Txt_Good_Qty.Properties.AutoHeight = false;
            this.Txt_Good_Qty.Properties.DisplayFormat.FormatString = "d";
            this.Txt_Good_Qty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Txt_Good_Qty.Properties.EditFormat.FormatString = "d";
            this.Txt_Good_Qty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Txt_Good_Qty.Properties.Mask.EditMask = "d";
            this.Txt_Good_Qty.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.Txt_Good_Qty.Properties.ReadOnly = true;
            this.Txt_Good_Qty.Size = new System.Drawing.Size(116, 28);
            this.Txt_Good_Qty.TabIndex = 293;
            this.Txt_Good_Qty.Visible = false;
            // 
            // Lbl_In_Qty
            // 
            this.Lbl_In_Qty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.Lbl_In_Qty.Appearance.Options.UseFont = true;
            this.Lbl_In_Qty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Lbl_In_Qty.Location = new System.Drawing.Point(8, 7);
            this.Lbl_In_Qty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lbl_In_Qty.Name = "Lbl_In_Qty";
            this.Lbl_In_Qty.Size = new System.Drawing.Size(141, 28);
            this.Lbl_In_Qty.TabIndex = 292;
            this.Lbl_In_Qty.Text = "입고 수량";
            this.Lbl_In_Qty.Visible = false;
            // 
            // Txt_In_Qty
            // 
            this.Txt_In_Qty.Enabled = false;
            this.Txt_In_Qty.Location = new System.Drawing.Point(168, 2);
            this.Txt_In_Qty.Margin = new System.Windows.Forms.Padding(0);
            this.Txt_In_Qty.Name = "Txt_In_Qty";
            this.Txt_In_Qty.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.Txt_In_Qty.Properties.Appearance.Options.UseFont = true;
            this.Txt_In_Qty.Properties.AutoHeight = false;
            this.Txt_In_Qty.Properties.DisplayFormat.FormatString = "d";
            this.Txt_In_Qty.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Txt_In_Qty.Properties.EditFormat.FormatString = "d";
            this.Txt_In_Qty.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Txt_In_Qty.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.Txt_In_Qty.Properties.MaskSettings.Set("mask", "d");
            this.Txt_In_Qty.Properties.ReadOnly = true;
            this.Txt_In_Qty.Size = new System.Drawing.Size(116, 38);
            this.Txt_In_Qty.TabIndex = 293;
            this.Txt_In_Qty.Visible = false;
            // 
            // Lbl_Date
            // 
            this.Lbl_Date.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Lbl_Date.Location = new System.Drawing.Point(8, 73);
            this.Lbl_Date.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lbl_Date.Name = "Lbl_Date";
            this.Lbl_Date.Size = new System.Drawing.Size(141, 28);
            this.Lbl_Date.TabIndex = 292;
            this.Lbl_Date.Text = "등록가능수량";
            this.Lbl_Date.Visible = false;
            // 
            // Txt_MaxBadQty
            // 
            this.Txt_MaxBadQty.Location = new System.Drawing.Point(168, 73);
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
            // Frm_BadQty_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 564);
            this.Controls.Add(this.groupControl3);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_BadQty_Detail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "유형별 불량수량";
            this.Load += new System.EventHandler(this.Frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Bad_Detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Bad_Detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Good_Qty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_In_Qty.Properties)).EndInit();
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
		private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.LabelControl Lbl_Good_Qty;
        private DevExpress.XtraEditors.TextEdit Txt_Good_Qty;
        private DevExpress.XtraEditors.LabelControl Lbl_In_Qty;
        private DevExpress.XtraEditors.TextEdit Txt_In_Qty;
    }
}