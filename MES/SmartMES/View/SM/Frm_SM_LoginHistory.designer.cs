namespace SmartMES.View.SM
{
    partial class Frm_SM_LoginHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SM_LoginHistory));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_Login = new DevExpress.XtraGrid.GridControl();
            this.Gv_Login = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cboYesNo2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.cboYesNo = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.cboYesNo1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.Btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Emp = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.btn_Close = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Login)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Login)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYesNo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYesNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYesNo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Gc_Login);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1265, 304, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1268, 878);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Gc_Login
            // 
            this.Gc_Login.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.Gc_Login.Location = new System.Drawing.Point(7, 41);
            this.Gc_Login.MainView = this.Gv_Login;
            this.Gc_Login.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Gc_Login.Name = "Gc_Login";
            this.Gc_Login.Size = new System.Drawing.Size(1254, 830);
            this.Gc_Login.TabIndex = 6;
            this.Gc_Login.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Login});
            // 
            // Gv_Login
            // 
            this.Gv_Login.ColumnPanelRowHeight = 29;
            this.Gv_Login.DetailHeight = 412;
            this.Gv_Login.GridControl = this.Gc_Login;
            this.Gv_Login.Name = "Gv_Login";
            this.Gv_Login.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_Login.OptionsView.ShowGroupPanel = false;
            this.Gv_Login.RowHeight = 29;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1268, 878);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "세부항목";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1268, 878);
            this.layoutControlGroup3.Text = "사용이력 리스트";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.Gc_Login;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1260, 836);
            this.layoutControlItem2.Text = "layoutControlItem3";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // cboYesNo2
            // 
            this.cboYesNo2.AutoHeight = false;
            this.cboYesNo2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboYesNo2.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.cboYesNo2.Name = "cboYesNo2";
            this.cboYesNo2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // cboYesNo
            // 
            this.cboYesNo.AutoHeight = false;
            this.cboYesNo.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboYesNo.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.cboYesNo.Name = "cboYesNo";
            this.cboYesNo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // cboYesNo1
            // 
            this.cboYesNo1.AutoHeight = false;
            this.cboYesNo1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboYesNo1.Items.AddRange(new object[] {
            "Y",
            "N"});
            this.cboYesNo1.Name = "cboYesNo1";
            this.cboYesNo1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.Btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Emp);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1268, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "사용자 이력";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 2;
            // 
            // Btn_Search
            // 
            this.Btn_Search.AllowFocus = false;
            this.Btn_Search.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.Btn_Search.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_Search.Appearance.Options.UseBackColor = true;
            this.Btn_Search.Appearance.Options.UseForeColor = true;
            this.Btn_Search.AutoSize = true;
            this.Btn_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_Search.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.Btn_Search.ImageOptions.ImageIndex = 0;
            this.Btn_Search.ImageOptions.ImageToTextIndent = 15;
            this.Btn_Search.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Btn_Search.Location = new System.Drawing.Point(764, 0);
            this.Btn_Search.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.Btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.Btn_Search.Size = new System.Drawing.Size(92, 40);
            this.Btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.Btn_Search.TabIndex = 50;
            this.Btn_Search.Text = "조회";
            this.Btn_Search.Click += new System.EventHandler(this.cmdDisplay_Click);
            // 
            // Lup_Emp
            // 
            this.Lup_Emp.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Emp.Location = new System.Drawing.Point(476, 0);
            this.Lup_Emp.LookAndFeel.SkinName = "WXI";
            this.Lup_Emp.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Emp.Name = "Lup_Emp";
            this.Lup_Emp.Size = new System.Drawing.Size(288, 40);
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
            this.Lup_Emp.TabIndex = 46;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(131, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Margin = new System.Windows.Forms.Padding(4);
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
            this.Dt_Date.TabIndex = 45;
            // 
            // btn_Close
            // 
            this.btn_Close.AllowFocus = false;
            this.btn_Close.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Close.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Appearance.Options.UseBackColor = true;
            this.btn_Close.Appearance.Options.UseForeColor = true;
            this.btn_Close.AutoSize = true;
            this.btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Close.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Close.ImageOptions.ImageIndex = 5;
            this.btn_Close.ImageOptions.ImageToTextIndent = 15;
            this.btn_Close.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Close.Location = new System.Drawing.Point(1176, 0);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(92, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Frm_SM_LoginHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 918);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Frm_SM_LoginHistory";
            this.Text = "사용자 이력";
            this.Load += new System.EventHandler(this.Frm_SM_LoginHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Login)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Login)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYesNo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYesNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYesNo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboYesNo2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboYesNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox cboYesNo1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton btn_Close;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchLookUp Lup_Emp;
        private baseSMT.smtSearchButton Btn_Search;
        private DevExpress.XtraGrid.GridControl Gc_Login;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Login;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}