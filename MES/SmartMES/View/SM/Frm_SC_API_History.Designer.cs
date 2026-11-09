
namespace SmartMES.View.Systems
{
    partial class Frm_SC_API_History
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SC_API_History));
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.Gc_API_History = new DevExpress.XtraGrid.GridControl();
			this.Gv_API_History = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
			this.Lup_Dept_Cd = new DevExpress.XtraEditors.LookUpEdit();
			this.Txt_Emp_No = new DevExpress.XtraEditors.TextEdit();
			this.Lbl_Emp_No = new DevExpress.XtraEditors.LabelControl();
			this.Lbl_Dept_Nm = new DevExpress.XtraEditors.LabelControl();
			this.ToDate = new DevExpress.XtraEditors.DateEdit();
			this.FromDate = new DevExpress.XtraEditors.DateEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.LblSearchDateMiddle = new DevExpress.XtraEditors.LabelControl();
			this.Lbl_Emp_Nm = new DevExpress.XtraEditors.LabelControl();
			this.Lup_Emp_Nm = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.panel3 = new System.Windows.Forms.Panel();
			this.cmdClose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_API_History)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_API_History)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
			this.panelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Lup_Dept_Cd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Txt_Emp_No.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ToDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ToDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FromDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FromDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Lup_Emp_Nm.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.panel3.SuspendLayout();
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
			this.groupControl2.Size = new System.Drawing.Size(1206, 488);
			this.groupControl2.TabIndex = 273;
			this.groupControl2.Text = "사용이력 리스트";
			// 
			// Gc_API_History
			// 
			this.Gc_API_History.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_API_History.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Gc_API_History.Location = new System.Drawing.Point(2, 58);
			this.Gc_API_History.MainView = this.Gv_API_History;
			this.Gc_API_History.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Gc_API_History.Name = "Gc_API_History";
			this.Gc_API_History.Size = new System.Drawing.Size(1202, 428);
			this.Gc_API_History.TabIndex = 1;
			this.Gc_API_History.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_API_History});
			// 
			// Gv_API_History
			// 
			this.Gv_API_History.DetailHeight = 327;
			this.Gv_API_History.GridControl = this.Gc_API_History;
			this.Gv_API_History.Name = "Gv_API_History";
			this.Gv_API_History.OptionsEditForm.PopupEditFormWidth = 700;
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
			this.panelControl3.Location = new System.Drawing.Point(2, 23);
			this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.panelControl3.Name = "panelControl3";
			this.panelControl3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.panelControl3.Size = new System.Drawing.Size(1202, 35);
			this.panelControl3.TabIndex = 278;
			this.panelControl3.Visible = false;
			// 
			// Lup_Dept_Cd
			// 
			this.Lup_Dept_Cd.Location = new System.Drawing.Point(793, 5);
			this.Lup_Dept_Cd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Lup_Dept_Cd.Name = "Lup_Dept_Cd";
			this.Lup_Dept_Cd.Properties.AutoHeight = false;
			this.Lup_Dept_Cd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.Lup_Dept_Cd.Properties.NullText = "";
			this.Lup_Dept_Cd.Size = new System.Drawing.Size(121, 21);
			this.Lup_Dept_Cd.TabIndex = 0;
			this.Lup_Dept_Cd.Visible = false;
			// 
			// Txt_Emp_No
			// 
			this.Txt_Emp_No.Location = new System.Drawing.Point(1058, 10);
			this.Txt_Emp_No.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Txt_Emp_No.Name = "Txt_Emp_No";
			this.Txt_Emp_No.Properties.AutoHeight = false;
			this.Txt_Emp_No.Size = new System.Drawing.Size(38, 21);
			this.Txt_Emp_No.TabIndex = 286;
			this.Txt_Emp_No.Visible = false;
			// 
			// Lbl_Emp_No
			// 
			this.Lbl_Emp_No.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.Lbl_Emp_No.Location = new System.Drawing.Point(1004, 9);
			this.Lbl_Emp_No.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Lbl_Emp_No.Name = "Lbl_Emp_No";
			this.Lbl_Emp_No.Size = new System.Drawing.Size(73, 21);
			this.Lbl_Emp_No.TabIndex = 287;
			this.Lbl_Emp_No.Text = "사원번호";
			this.Lbl_Emp_No.Visible = false;
			// 
			// Lbl_Dept_Nm
			// 
			this.Lbl_Dept_Nm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.Lbl_Dept_Nm.Location = new System.Drawing.Point(740, 5);
			this.Lbl_Dept_Nm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Lbl_Dept_Nm.Name = "Lbl_Dept_Nm";
			this.Lbl_Dept_Nm.Size = new System.Drawing.Size(61, 21);
			this.Lbl_Dept_Nm.TabIndex = 60;
			this.Lbl_Dept_Nm.Text = "사업부";
			this.Lbl_Dept_Nm.Visible = false;
			// 
			// ToDate
			// 
			this.ToDate.EditValue = null;
			this.ToDate.Location = new System.Drawing.Point(183, 4);
			this.ToDate.Margin = new System.Windows.Forms.Padding(0);
			this.ToDate.Name = "ToDate";
			this.ToDate.Properties.AutoHeight = false;
			this.ToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.ToDate.Size = new System.Drawing.Size(121, 21);
			this.ToDate.TabIndex = 284;
			// 
			// FromDate
			// 
			this.FromDate.EditValue = null;
			this.FromDate.Location = new System.Drawing.Point(46, 4);
			this.FromDate.Margin = new System.Windows.Forms.Padding(0);
			this.FromDate.Name = "FromDate";
			this.FromDate.Properties.AutoHeight = false;
			this.FromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.FromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.FromDate.Size = new System.Drawing.Size(121, 21);
			this.FromDate.TabIndex = 282;
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControl3.Appearance.Options.UseForeColor = true;
			this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl3.Location = new System.Drawing.Point(4, 4);
			this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.labelControl3.Size = new System.Drawing.Size(61, 21);
			this.labelControl3.TabIndex = 285;
			this.labelControl3.Text = "기간";
			// 
			// LblSearchDateMiddle
			// 
			this.LblSearchDateMiddle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblSearchDateMiddle.Location = new System.Drawing.Point(170, 4);
			this.LblSearchDateMiddle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.LblSearchDateMiddle.MaximumSize = new System.Drawing.Size(7, 21);
			this.LblSearchDateMiddle.MinimumSize = new System.Drawing.Size(7, 21);
			this.LblSearchDateMiddle.Name = "LblSearchDateMiddle";
			this.LblSearchDateMiddle.Size = new System.Drawing.Size(7, 21);
			this.LblSearchDateMiddle.TabIndex = 283;
			this.LblSearchDateMiddle.Text = "~";
			// 
			// Lbl_Emp_Nm
			// 
			this.Lbl_Emp_Nm.Appearance.ForeColor = System.Drawing.Color.White;
			this.Lbl_Emp_Nm.Appearance.Options.UseForeColor = true;
			this.Lbl_Emp_Nm.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.Lbl_Emp_Nm.Location = new System.Drawing.Point(320, 4);
			this.Lbl_Emp_Nm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Lbl_Emp_Nm.Name = "Lbl_Emp_Nm";
			this.Lbl_Emp_Nm.Size = new System.Drawing.Size(61, 21);
			this.Lbl_Emp_Nm.TabIndex = 281;
			this.Lbl_Emp_Nm.Text = "사용자명";
			// 
			// Lup_Emp_Nm
			// 
			this.Lup_Emp_Nm.Location = new System.Drawing.Point(382, 4);
			this.Lup_Emp_Nm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Lup_Emp_Nm.Name = "Lup_Emp_Nm";
			this.Lup_Emp_Nm.Properties.AutoHeight = false;
			this.Lup_Emp_Nm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.Lup_Emp_Nm.Properties.NullText = "";
			this.Lup_Emp_Nm.Properties.PopupView = this.searchLookUpEdit1View;
			this.Lup_Emp_Nm.Size = new System.Drawing.Size(121, 21);
			this.Lup_Emp_Nm.TabIndex = 1;
			// 
			// searchLookUpEdit1View
			// 
			this.searchLookUpEdit1View.DetailHeight = 327;
			this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
			this.searchLookUpEdit1View.OptionsEditForm.PopupEditFormWidth = 700;
			this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.panel3);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1206, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "Frm_SC_API_History";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 274;
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
			this.cmdDisplay.Location = new System.Drawing.Point(664, 0);
			this.cmdDisplay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(69, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Click += new System.EventHandler(this.BtnSearch_Click);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.ToDate);
			this.panel3.Controls.Add(this.FromDate);
			this.panel3.Controls.Add(this.Lup_Emp_Nm);
			this.panel3.Controls.Add(this.Lbl_Emp_Nm);
			this.panel3.Controls.Add(this.LblSearchDateMiddle);
			this.panel3.Controls.Add(this.labelControl3);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.Location = new System.Drawing.Point(153, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(511, 40);
			this.panel3.TabIndex = 28;
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
			this.cmdClose.Location = new System.Drawing.Point(1137, 0);
			this.cmdClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(9, 0, 9, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(69, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// Frm_SC_API_History
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1206, 528);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "Frm_SC_API_History";
			this.Text = "Frm_SC_API_History";
			this.Load += new System.EventHandler(this.Frm_SC_LoginOut_History_Load);
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_API_History)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_API_History)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
			this.panelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Lup_Dept_Cd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Txt_Emp_No.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ToDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ToDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FromDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FromDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Lup_Emp_Nm.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl Gc_API_History;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_API_History;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl Lbl_Emp_Nm;
        private DevExpress.XtraEditors.LookUpEdit Lup_Dept_Cd;
        private DevExpress.XtraEditors.LabelControl Lbl_Dept_Nm;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit ToDate;
        private DevExpress.XtraEditors.LabelControl LblSearchDateMiddle;
        private DevExpress.XtraEditors.DateEdit FromDate;
        private DevExpress.XtraEditors.TextEdit Txt_Emp_No;
        private DevExpress.XtraEditors.LabelControl Lbl_Emp_No;
        private DevExpress.XtraEditors.SearchLookUpEdit Lup_Emp_Nm;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private System.Windows.Forms.Panel panel3;
		private baseSMT.smtSearchButton cmdClose;
	}
}