
namespace SmartMES.View.EQ
{
    partial class Frm_EQ_MachineChk
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EQ_MachineChk));
			this.LupLine = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.LblLine = new DevExpress.XtraEditors.LabelControl();
			this.Grp_Bool = new DevExpress.XtraEditors.GroupControl();
			this.Gc_Insp = new DevExpress.XtraGrid.GridControl();
			this.Gv_Insp = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.LayoutItem_Bool = new DevExpress.XtraLayout.LayoutControlItem();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.Lup_InspTime = new baseSMT.smtSearchLookUp();
			this.Lup_Machine = new baseSMT.smtSearchLookUp();
			this.Dt_Date = new baseSMT.smtSearchDatetime();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.cmdDelete = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.LupLine.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Grp_Bool)).BeginInit();
			this.Grp_Bool.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Insp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Insp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LayoutItem_Bool)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// LupLine
			// 
			this.LupLine.EditValue = "";
			this.LupLine.Location = new System.Drawing.Point(901, 5);
			this.LupLine.Name = "LupLine";
			this.LupLine.Properties.AutoHeight = false;
			this.LupLine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.LupLine.Properties.NullText = "";
			this.LupLine.Properties.PopupView = this.gridView1;
			this.LupLine.Size = new System.Drawing.Size(49, 26);
			this.LupLine.TabIndex = 288;
			this.LupLine.Visible = false;
			// 
			// gridView1
			// 
			this.gridView1.DetailHeight = 340;
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// LblLine
			// 
			this.LblLine.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblLine.Location = new System.Drawing.Point(836, 8);
			this.LblLine.Name = "LblLine";
			this.LblLine.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.LblLine.Size = new System.Drawing.Size(35, 26);
			this.LblLine.TabIndex = 287;
			this.LblLine.Text = "라인";
			this.LblLine.Visible = false;
			// 
			// Grp_Bool
			// 
			this.Grp_Bool.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
			this.Grp_Bool.AppearanceCaption.Options.UseFont = true;
			this.Grp_Bool.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Grp_Bool.CaptionImageOptions.Image")));
			this.Grp_Bool.Controls.Add(this.Gc_Insp);
			this.Grp_Bool.Location = new System.Drawing.Point(0, 0);
			this.Grp_Bool.Margin = new System.Windows.Forms.Padding(0);
			this.Grp_Bool.Name = "Grp_Bool";
			this.Grp_Bool.Size = new System.Drawing.Size(1237, 388);
			this.Grp_Bool.TabIndex = 280;
			this.Grp_Bool.Text = "점검항목";
			// 
			// Gc_Insp
			// 
			this.Gc_Insp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_Insp.Location = new System.Drawing.Point(2, 29);
			this.Gc_Insp.MainView = this.Gv_Insp;
			this.Gc_Insp.Name = "Gc_Insp";
			this.Gc_Insp.Size = new System.Drawing.Size(1233, 357);
			this.Gc_Insp.TabIndex = 1;
			this.Gc_Insp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Insp});
			// 
			// Gv_Insp
			// 
			this.Gv_Insp.DetailHeight = 397;
			this.Gv_Insp.GridControl = this.Gc_Insp;
			this.Gv_Insp.Name = "Gv_Insp";
			this.Gv_Insp.OptionsView.ShowGroupPanel = false;
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.Grp_Bool);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(631, 365, 808, 450);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1237, 388);
			this.layoutControl1.TabIndex = 282;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.LayoutItem_Bool});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1237, 388);
			this.Root.TextVisible = false;
			// 
			// LayoutItem_Bool
			// 
			this.LayoutItem_Bool.Control = this.Grp_Bool;
			this.LayoutItem_Bool.Location = new System.Drawing.Point(0, 0);
			this.LayoutItem_Bool.Name = "LayoutItem_Bool";
			this.LayoutItem_Bool.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.LayoutItem_Bool.Size = new System.Drawing.Size(1237, 388);
			this.LayoutItem_Bool.TextSize = new System.Drawing.Size(0, 0);
			this.LayoutItem_Bool.TextVisible = false;
			// 
			// groupControl2
			// 
			this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.groupControl2.Controls.Add(this.layoutControl1);
			this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl2.Location = new System.Drawing.Point(0, 40);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(1237, 388);
			this.groupControl2.TabIndex = 60;
			this.groupControl2.Text = "groupControl2";
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDelete);
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.Lup_InspTime);
			this.smtCommonBar1.Controls.Add(this.Lup_Machine);
			this.smtCommonBar1.Controls.Add(this.Dt_Date);
			this.smtCommonBar1.Controls.Add(this.LblLine);
			this.smtCommonBar1.Controls.Add(this.LupLine);
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1237, 40);
			this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "설비점검";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 271;
			// 
			// cmdDisplay
			// 
			this.cmdDisplay.AllowFocus = false;
			this.cmdDisplay.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDisplay.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDisplay.Appearance.Options.UseBackColor = true;
			this.cmdDisplay.Appearance.Options.UseForeColor = true;
			this.cmdDisplay.AutoSize = true;
			this.cmdDisplay.Dock = System.Windows.Forms.DockStyle.Left;
			this.cmdDisplay.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDisplay.ImageOptions.ImageIndex = 0;
			this.cmdDisplay.ImageOptions.ImageToTextIndent = 15;
			this.cmdDisplay.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDisplay.Location = new System.Drawing.Point(820, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Click += new System.EventHandler(this.Btn_Click);
			// 
			// Lup_InspTime
			// 
			this.Lup_InspTime.Dock = System.Windows.Forms.DockStyle.Left;
			this.Lup_InspTime.Location = new System.Drawing.Point(600, 0);
			this.Lup_InspTime.LookAndFeel.SkinName = "WXI";
			this.Lup_InspTime.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Lup_InspTime.Name = "Lup_InspTime";
			this.Lup_InspTime.Size = new System.Drawing.Size(220, 40);
			this.Lup_InspTime.smtLabelForeColor = System.Drawing.Color.White;
			this.Lup_InspTime.smtLabelTitleValue = "점검시간";
			this.Lup_InspTime.smtLabelTitleVisible = true;
			this.Lup_InspTime.smtLabelTitleWidth = 100;
			this.Lup_InspTime.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.Lup_InspTime.smtLookUpDataDisplayMember = "코드명";
			this.Lup_InspTime.smtLookUpDataSource = null;
			this.Lup_InspTime.smtLookUpDataValueMember = "코드";
			this.Lup_InspTime.smtLookUpSelectdIndex = -1;
			this.Lup_InspTime.smtLookUpText = "";
			this.Lup_InspTime.smtLookUpValue = "";
			this.Lup_InspTime.TabIndex = 291;
			// 
			// Lup_Machine
			// 
			this.Lup_Machine.Dock = System.Windows.Forms.DockStyle.Left;
			this.Lup_Machine.Location = new System.Drawing.Point(312, 0);
			this.Lup_Machine.LookAndFeel.SkinName = "WXI";
			this.Lup_Machine.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Lup_Machine.Name = "Lup_Machine";
			this.Lup_Machine.Size = new System.Drawing.Size(288, 40);
			this.Lup_Machine.smtLabelForeColor = System.Drawing.Color.White;
			this.Lup_Machine.smtLabelTitleValue = "설비";
			this.Lup_Machine.smtLabelTitleVisible = true;
			this.Lup_Machine.smtLabelTitleWidth = 100;
			this.Lup_Machine.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.Lup_Machine.smtLookUpDataDisplayMember = "코드명";
			this.Lup_Machine.smtLookUpDataSource = null;
			this.Lup_Machine.smtLookUpDataValueMember = "코드";
			this.Lup_Machine.smtLookUpSelectdIndex = -1;
			this.Lup_Machine.smtLookUpText = "";
			this.Lup_Machine.smtLookUpValue = "";
			this.Lup_Machine.TabIndex = 290;
			// 
			// Dt_Date
			// 
			this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
			this.Dt_Date.Location = new System.Drawing.Point(92, 0);
			this.Dt_Date.LookAndFeel.SkinName = "WXI";
			this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Dt_Date.Name = "Dt_Date";
			this.Dt_Date.Size = new System.Drawing.Size(220, 40);
			this.Dt_Date.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.Dt_Date.smtComboTitleItems = new string[0];
			this.Dt_Date.smtComboTitleValue = "";
			this.Dt_Date.smtComboTitleVisible = false;
			this.Dt_Date.smtComboTitleWidth = 100;
			this.Dt_Date.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 729);
			this.Dt_Date.smtEndDateTimeVisible = false;
			this.Dt_Date.smtLabelForeColor = System.Drawing.Color.White;
			this.Dt_Date.smtLabelTitleValue = "조회기간";
			this.Dt_Date.smtLabelTitleVisible = true;
			this.Dt_Date.smtLabelTitleWidth = 100;
			this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
			this.Dt_Date.smtStartDateTimeVisible = true;
			this.Dt_Date.TabIndex = 289;
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
			this.cmdSave.Location = new System.Drawing.Point(1083, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(1160, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdDelete.Location = new System.Drawing.Point(1006, 0);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDelete.Size = new System.Drawing.Size(77, 40);
			this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDelete.TabIndex = 292;
			this.cmdDelete.Text = "삭제";
			this.cmdDelete.Click += new System.EventHandler(this.Btn_Click);
			// 
			// Frm_EQ_MachineChk
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1237, 428);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_EQ_MachineChk";
			this.Text = "설비점검";
			this.Load += new System.EventHandler(this.Frm_Load);
			((System.ComponentModel.ISupportInitialize)(this.LupLine.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Grp_Bool)).EndInit();
			this.Grp_Bool.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Insp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Insp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LayoutItem_Bool)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl Grp_Bool;
        private DevExpress.XtraGrid.GridControl Gc_Insp;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Insp;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem LayoutItem_Bool;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SearchLookUpEdit LupLine;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl LblLine;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchLookUp Lup_InspTime;
        private baseSMT.smtSearchLookUp Lup_Machine;
        private baseSMT.smtSearchDatetime Dt_Date;
		private baseSMT.smtSearchButton cmdDelete;
	}
}