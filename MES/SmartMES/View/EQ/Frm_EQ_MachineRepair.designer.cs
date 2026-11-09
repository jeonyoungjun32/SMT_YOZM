namespace SmartMES.View.EQ
{
    partial class Frm_EQ_MachineRepair
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EQ_MachineRepair));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcRepair = new DevExpress.XtraGrid.GridControl();
            this.gvRepair = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.DateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemDateEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Ins = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Machine = new baseSMT.smtSearchLookUp();
            this.btn_Save = new baseSMT.smtSearchButton();
            this.btn_Del = new baseSMT.smtSearchButton();
            this.btn_Excel = new baseSMT.smtSearchButton();
            this.btn_Close = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcRepair);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1091, 485);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcRepair
            // 
            this.gcRepair.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcRepair.Location = new System.Drawing.Point(7, 7);
            this.gcRepair.MainView = this.gvRepair;
            this.gcRepair.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcRepair.Name = "gcRepair";
            this.gcRepair.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.DateEdit1,
            this.DateEdit2,
            this.repositoryItemDateEdit5});
            this.gcRepair.Size = new System.Drawing.Size(1077, 471);
            this.gcRepair.TabIndex = 215;
            this.gcRepair.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRepair});
            // 
            // gvRepair
            // 
            this.gvRepair.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvRepair.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvRepair.ColumnPanelRowHeight = 29;
            this.gvRepair.GridControl = this.gcRepair;
            this.gvRepair.Name = "gvRepair";
            this.gvRepair.OptionsView.ColumnAutoWidth = false;
            this.gvRepair.OptionsView.ShowGroupPanel = false;
            this.gvRepair.RowHeight = 29;
            // 
            // DateEdit1
            // 
            this.DateEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.DateEdit1.AutoHeight = false;
            this.DateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit1.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.DateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit1.DisplayFormat.FormatString = "G";
            this.DateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit1.EditFormat.FormatString = "G";
            this.DateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit1.MaskSettings.Set("mask", "");
            this.DateEdit1.Name = "DateEdit1";
            this.DateEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // DateEdit2
            // 
            this.DateEdit2.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.DateEdit2.AutoHeight = false;
            this.DateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit2.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.DateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit2.DisplayFormat.FormatString = "G";
            this.DateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit2.EditFormat.FormatString = "G";
            this.DateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit2.MaskSettings.Set("mask", "");
            this.DateEdit2.Name = "DateEdit2";
            this.DateEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemDateEdit5
            // 
            this.repositoryItemDateEdit5.AutoHeight = false;
            this.repositoryItemDateEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit5.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit5.Name = "repositoryItemDateEdit5";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1091, 485);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1091, 485);
            this.layoutControlGroup3.Text = "설비수리이력";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcRepair;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1083, 477);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Ins);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Machine);
            this.smtCommonBar1.Controls.Add(this.btn_Save);
            this.smtCommonBar1.Controls.Add(this.btn_Del);
            this.smtCommonBar1.Controls.Add(this.btn_Excel);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1091, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "설비수리이력";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 2;
            // 
            // btn_Ins
            // 
            this.btn_Ins.AllowFocus = false;
            this.btn_Ins.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Ins.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Ins.Appearance.Options.UseBackColor = true;
            this.btn_Ins.Appearance.Options.UseForeColor = true;
            this.btn_Ins.AutoSize = true;
            this.btn_Ins.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Ins.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Ins.ImageOptions.ImageIndex = 1;
            this.btn_Ins.ImageOptions.ImageToTextIndent = 15;
            this.btn_Ins.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Ins.Location = new System.Drawing.Point(682, 0);
            this.btn_Ins.Name = "btn_Ins";
            this.btn_Ins.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Ins.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Ins.Size = new System.Drawing.Size(77, 40);
            this.btn_Ins.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.btn_Ins.TabIndex = 9;
            this.btn_Ins.Text = "등록";
            this.btn_Ins.Click += new System.EventHandler(this.cmd_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.AllowFocus = false;
            this.btn_Search.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Search.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Search.Appearance.Options.UseBackColor = true;
            this.btn_Search.Appearance.Options.UseForeColor = true;
            this.btn_Search.AutoSize = true;
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Search.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Search.ImageOptions.ImageIndex = 0;
            this.btn_Search.ImageOptions.ImageToTextIndent = 15;
            this.btn_Search.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Search.Location = new System.Drawing.Point(404, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 6;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // Lup_Machine
            // 
            this.Lup_Machine.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Machine.Location = new System.Drawing.Point(116, 0);
            this.Lup_Machine.LookAndFeel.SkinName = "WXI";
            this.Lup_Machine.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Machine.Name = "Lup_Machine";
            this.Lup_Machine.Size = new System.Drawing.Size(288, 40);
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
            this.Lup_Machine.TabIndex = 8;
            // 
            // btn_Save
            // 
            this.btn_Save.AllowFocus = false;
            this.btn_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Save.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseForeColor = true;
            this.btn_Save.AutoSize = true;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Save.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Save.ImageOptions.ImageIndex = 4;
            this.btn_Save.ImageOptions.ImageToTextIndent = 15;
            this.btn_Save.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Save.Location = new System.Drawing.Point(759, 0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Save.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Save.Size = new System.Drawing.Size(77, 40);
            this.btn_Save.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "저장";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.AllowFocus = false;
            this.btn_Del.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Del.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Del.Appearance.Options.UseBackColor = true;
            this.btn_Del.Appearance.Options.UseForeColor = true;
            this.btn_Del.AutoSize = true;
            this.btn_Del.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Del.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Del.ImageOptions.ImageIndex = 3;
            this.btn_Del.ImageOptions.ImageToTextIndent = 15;
            this.btn_Del.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Del.Location = new System.Drawing.Point(836, 0);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Del.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Del.Size = new System.Drawing.Size(77, 40);
            this.btn_Del.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.btn_Del.TabIndex = 3;
            this.btn_Del.Text = "삭제";
            this.btn_Del.Click += new System.EventHandler(this.cmd_Click);
            // 
            // btn_Excel
            // 
            this.btn_Excel.AllowFocus = false;
            this.btn_Excel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Excel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Excel.Appearance.Options.UseBackColor = true;
            this.btn_Excel.Appearance.Options.UseForeColor = true;
            this.btn_Excel.AutoSize = true;
            this.btn_Excel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Excel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Excel.ImageOptions.ImageIndex = 9;
            this.btn_Excel.ImageOptions.ImageToTextIndent = 15;
            this.btn_Excel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Excel.Location = new System.Drawing.Point(913, 0);
            this.btn_Excel.Name = "btn_Excel";
            this.btn_Excel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Excel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Excel.Size = new System.Drawing.Size(101, 40);
            this.btn_Excel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.btn_Excel.TabIndex = 2;
            this.btn_Excel.Text = "엑셀변환";
            this.btn_Excel.Click += new System.EventHandler(this.btn_Excel_Click);
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
            this.btn_Close.Location = new System.Drawing.Point(1014, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(77, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmMachineRepair
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 525);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_EQ_MachineRepair";
            this.Text = "설비수리등록";
            this.Load += new System.EventHandler(this.Frm_EQ_MachineRepair_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcRepair;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRepair;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DateEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit5;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Ins;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchLookUp Lup_Machine;
        private baseSMT.smtSearchButton btn_Save;
        private baseSMT.smtSearchButton btn_Del;
        private baseSMT.smtSearchButton btn_Excel;
        private baseSMT.smtSearchButton btn_Close;
    }
}