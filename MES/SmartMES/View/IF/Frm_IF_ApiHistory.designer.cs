
namespace SmartMES.View.IF
{
    partial class Frm_IF_ApiHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_IF_ApiHistory));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_API_History = new DevExpress.XtraGrid.GridControl();
            this.Gv_API_History = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.Lup_Dept_Cd = new DevExpress.XtraEditors.LookUpEdit();
            this.Txt_Emp_No = new DevExpress.XtraEditors.TextEdit();
            this.Lbl_Emp_No = new DevExpress.XtraEditors.LabelControl();
            this.Lbl_Dept_Nm = new DevExpress.XtraEditors.LabelControl();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.Lup_Emp = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_API_History)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_API_History)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lup_Dept_Cd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Emp_No.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.Gc_API_History);
            this.groupControl2.Controls.Add(this.panelControl3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 40);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1378, 601);
            this.groupControl2.TabIndex = 273;
            this.groupControl2.Text = "사용이력 리스트";
            // 
            // Gc_API_History
            // 
            this.Gc_API_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_API_History.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gc_API_History.Location = new System.Drawing.Point(2, 72);
            this.Gc_API_History.MainView = this.Gv_API_History;
            this.Gc_API_History.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gc_API_History.Name = "Gc_API_History";
            this.Gc_API_History.Size = new System.Drawing.Size(1374, 527);
            this.Gc_API_History.TabIndex = 1;
            this.Gc_API_History.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_API_History});
            // 
            // Gv_API_History
            // 
            this.Gv_API_History.DetailHeight = 397;
            this.Gv_API_History.GridControl = this.Gc_API_History;
            this.Gv_API_History.Name = "Gv_API_History";
            this.Gv_API_History.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.Lup_Dept_Cd);
            this.panelControl3.Controls.Add(this.Txt_Emp_No);
            this.panelControl3.Controls.Add(this.Lbl_Emp_No);
            this.panelControl3.Controls.Add(this.Lbl_Dept_Nm);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 29);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl3.Size = new System.Drawing.Size(1374, 43);
            this.panelControl3.TabIndex = 278;
            this.panelControl3.Visible = false;
            // 
            // Lup_Dept_Cd
            // 
            this.Lup_Dept_Cd.Location = new System.Drawing.Point(906, 6);
            this.Lup_Dept_Cd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lup_Dept_Cd.Name = "Lup_Dept_Cd";
            this.Lup_Dept_Cd.Properties.AutoHeight = false;
            this.Lup_Dept_Cd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Lup_Dept_Cd.Properties.NullText = "";
            this.Lup_Dept_Cd.Size = new System.Drawing.Size(138, 26);
            this.Lup_Dept_Cd.TabIndex = 0;
            this.Lup_Dept_Cd.Visible = false;
            // 
            // Txt_Emp_No
            // 
            this.Txt_Emp_No.Location = new System.Drawing.Point(1209, 12);
            this.Txt_Emp_No.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Txt_Emp_No.Name = "Txt_Emp_No";
            this.Txt_Emp_No.Properties.AutoHeight = false;
            this.Txt_Emp_No.Size = new System.Drawing.Size(43, 26);
            this.Txt_Emp_No.TabIndex = 286;
            this.Txt_Emp_No.Visible = false;
            // 
            // Lbl_Emp_No
            // 
            this.Lbl_Emp_No.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Lbl_Emp_No.Location = new System.Drawing.Point(1147, 11);
            this.Lbl_Emp_No.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lbl_Emp_No.Name = "Lbl_Emp_No";
            this.Lbl_Emp_No.Size = new System.Drawing.Size(83, 26);
            this.Lbl_Emp_No.TabIndex = 287;
            this.Lbl_Emp_No.Text = "사원번호";
            this.Lbl_Emp_No.Visible = false;
            // 
            // Lbl_Dept_Nm
            // 
            this.Lbl_Dept_Nm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Lbl_Dept_Nm.Location = new System.Drawing.Point(846, 6);
            this.Lbl_Dept_Nm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lbl_Dept_Nm.Name = "Lbl_Dept_Nm";
            this.Lbl_Dept_Nm.Size = new System.Drawing.Size(70, 26);
            this.Lbl_Dept_Nm.TabIndex = 60;
            this.Lbl_Dept_Nm.Text = "사업부";
            this.Lbl_Dept_Nm.Visible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.Lup_Emp);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1378, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "API이력정보";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 274;
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
            this.cmdClose.Location = new System.Drawing.Point(1301, 0);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Lup_Emp
            // 
            this.Lup_Emp.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Emp.Location = new System.Drawing.Point(460, 0);
            this.Lup_Emp.LookAndFeel.SkinName = "WXI";
            this.Lup_Emp.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Emp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Lup_Emp.Name = "Lup_Emp";
            this.Lup_Emp.Size = new System.Drawing.Size(230, 40);
            this.Lup_Emp.smtLabelTitleValue = "사용자";
            this.Lup_Emp.smtLabelTitleVisible = true;
            this.Lup_Emp.smtLabelTitleWidth = 100;
            this.Lup_Emp.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Emp.smtLookUpDataDisplayMember = "";
            this.Lup_Emp.smtLookUpDataSource = null;
            this.Lup_Emp.smtLookUpDataValueMember = "";
            this.Lup_Emp.smtLookUpSelectdIndex = -1;
            this.Lup_Emp.smtLookUpText = "";
            this.Lup_Emp.smtLookUpValue = "";
            this.Lup_Emp.TabIndex = 48;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(115, 0);
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
            this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 729);
            this.Dt_Date.smtEndDateTimeVisible = true;
            this.Dt_Date.smtLabelTitleValue = "조회기간";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 47;
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
            this.cmdDisplay.Location = new System.Drawing.Point(690, 0);
            this.cmdDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 49;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // Frm_IF_ApiHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 641);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_IF_ApiHistory";
            this.Text = "API이력정보";
            this.Load += new System.EventHandler(this.Frm_IF_ApiHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_API_History)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_API_History)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Lup_Dept_Cd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Emp_No.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl Gc_API_History;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_API_History;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LookUpEdit Lup_Dept_Cd;
        private DevExpress.XtraEditors.LabelControl Lbl_Dept_Nm;
        private DevExpress.XtraEditors.TextEdit Txt_Emp_No;
        private DevExpress.XtraEditors.LabelControl Lbl_Emp_No;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchLookUp Lup_Emp;
        private baseSMT.smtSearchDatetime Dt_Date;
    }
}