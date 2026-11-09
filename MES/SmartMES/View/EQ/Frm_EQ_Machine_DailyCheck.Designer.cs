
namespace SmartMES.View.EQ
{ 
    partial class Frm_EQ_Machine_DailyCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EQ_Machine_DailyCheck));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_Daily_Chk = new DevExpress.XtraGrid.GridControl();
            this.Gv_Daily_Chk = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_ProductCmd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.Lup_InspTime = new baseSMT.smtSearchLookUp();
            this.Lup_Machine = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdClose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Daily_Chk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Daily_Chk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProductCmd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.Gc_Daily_Chk);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 40);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(1460, 660);
            this.groupControl2.TabIndex = 275;
            this.groupControl2.Text = "정보";
            // 
            // Gc_Daily_Chk
            // 
            this.Gc_Daily_Chk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_Daily_Chk.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.Gc_Daily_Chk.Location = new System.Drawing.Point(2, 2);
            this.Gc_Daily_Chk.MainView = this.Gv_Daily_Chk;
            this.Gc_Daily_Chk.Margin = new System.Windows.Forms.Padding(4);
            this.Gc_Daily_Chk.Name = "Gc_Daily_Chk";
            this.Gc_Daily_Chk.Size = new System.Drawing.Size(1456, 656);
            this.Gc_Daily_Chk.TabIndex = 269;
            this.Gc_Daily_Chk.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Daily_Chk,
            this.Gv_ProductCmd});
            // 
            // Gv_Daily_Chk
            // 
            this.Gv_Daily_Chk.DetailHeight = 467;
            this.Gv_Daily_Chk.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.Gv_Daily_Chk.GridControl = this.Gc_Daily_Chk;
            this.Gv_Daily_Chk.Name = "Gv_Daily_Chk";
            this.Gv_Daily_Chk.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.Gv_Daily_Chk.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_Daily_Chk.OptionsNavigation.AutoMoveRowFocus = false;
            this.Gv_Daily_Chk.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Gv_Daily_Chk.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.Gv_Daily_Chk.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_ProductCmd
            // 
            this.Gv_ProductCmd.DetailHeight = 467;
            this.Gv_ProductCmd.GridControl = this.Gc_Daily_Chk;
            this.Gv_ProductCmd.Name = "Gv_ProductCmd";
            this.Gv_ProductCmd.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_ProductCmd.OptionsView.ShowGroupPanel = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.Lup_InspTime);
            this.smtCommonBar1.Controls.Add(this.Lup_Machine);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1460, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "설비일상점검현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 276;
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
            this.cmdDisplay.Location = new System.Drawing.Point(1027, 0);
            this.cmdDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(92, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 5;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Click += new System.EventHandler(this.Btn_Click);
            // 
            // Lup_InspTime
            // 
            this.Lup_InspTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_InspTime.Location = new System.Drawing.Point(752, 0);
            this.Lup_InspTime.LookAndFeel.SkinName = "WXI";
            this.Lup_InspTime.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_InspTime.Margin = new System.Windows.Forms.Padding(4);
            this.Lup_InspTime.Name = "Lup_InspTime";
            this.Lup_InspTime.Size = new System.Drawing.Size(275, 40);
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
            this.Lup_InspTime.TabIndex = 31;
            // 
            // Lup_Machine
            // 
            this.Lup_Machine.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Machine.Location = new System.Drawing.Point(392, 0);
            this.Lup_Machine.LookAndFeel.SkinName = "WXI";
            this.Lup_Machine.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Machine.Margin = new System.Windows.Forms.Padding(4);
            this.Lup_Machine.Name = "Lup_Machine";
            this.Lup_Machine.Size = new System.Drawing.Size(360, 40);
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
            this.Lup_Machine.TabIndex = 30;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(172, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Margin = new System.Windows.Forms.Padding(4);
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
            this.Dt_Date.smtLabelTitleValue = "조회기간";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 29;
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
            this.cmdClose.Location = new System.Drawing.Point(1368, 0);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(92, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.Btn_Click);
            // 
            // Frm_EQ_Machine_DailyCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1460, 700);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_EQ_Machine_DailyCheck";
            this.Text = "설비일상점검현황";
            this.Load += new System.EventHandler(this.Frm_QT_Bad_ChkView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Daily_Chk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Daily_Chk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProductCmd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl Gc_Daily_Chk;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Daily_Chk;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_ProductCmd;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchLookUp Lup_Machine;
        private baseSMT.smtSearchLookUp Lup_InspTime;
    }
}