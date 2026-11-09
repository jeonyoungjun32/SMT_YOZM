
namespace SmartMES.View.PM
{
    partial class Frm_PM_OrderResult_MatInput
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
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.BtnSearch = new baseSMT.smtSearchButton();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_ProcessResult = new DevExpress.XtraGrid.GridControl();
            this.Gv_ProcessResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_ProcessResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProcessResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.BtnSearch);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1240, 40);
            this.smtCommonBar1.smtImageTitleText = "공정실적 자재투입현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 282;
            // 
            // BtnSearch
            // 
            this.BtnSearch.AllowFocus = false;
            this.BtnSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.BtnSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnSearch.Appearance.Options.UseBackColor = true;
            this.BtnSearch.Appearance.Options.UseForeColor = true;
            this.BtnSearch.AutoSize = true;
            this.BtnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnSearch.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.BtnSearch.ImageOptions.ImageIndex = 0;
            this.BtnSearch.ImageOptions.ImageToTextIndent = 15;
            this.BtnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.BtnSearch.Location = new System.Drawing.Point(512, 0);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.BtnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.BtnSearch.Size = new System.Drawing.Size(77, 40);
            this.BtnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.BtnSearch.TabIndex = 5;
            this.BtnSearch.Text = "조회";
            this.BtnSearch.Click += new System.EventHandler(this.Btn_Click);
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(167, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
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
            this.Dt_Date.TabIndex = 41;
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
            this.cmdClose.Location = new System.Drawing.Point(1163, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Gc_ProcessResult);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1240, 565);
            this.layoutControl1.TabIndex = 283;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Gc_ProcessResult
            // 
            this.Gc_ProcessResult.Location = new System.Drawing.Point(3, 3);
            this.Gc_ProcessResult.MainView = this.Gv_ProcessResult;
            this.Gc_ProcessResult.Name = "Gc_ProcessResult";
            this.Gc_ProcessResult.Size = new System.Drawing.Size(1234, 559);
            this.Gc_ProcessResult.TabIndex = 270;
            this.Gc_ProcessResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_ProcessResult});
            // 
            // Gv_ProcessResult
            // 
            this.Gv_ProcessResult.DetailHeight = 397;
            this.Gv_ProcessResult.GridControl = this.Gc_ProcessResult;
            this.Gv_ProcessResult.Name = "Gv_ProcessResult";
            this.Gv_ProcessResult.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_ProcessResult.OptionsSelection.MultiSelect = true;
            this.Gv_ProcessResult.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1240, 565);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Gc_ProcessResult;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1240, 565);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Frm_PM_OrderResult_MatInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 605);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_PM_OrderResult_MatInput";
            this.Text = "공정실적 자재투입현황";
            this.Load += new System.EventHandler(this.Frm_PM_OrderResult_MatInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_ProcessResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProcessResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton BtnSearch;
        private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchDatetime Dt_Date;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl Gc_ProcessResult;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_ProcessResult;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}