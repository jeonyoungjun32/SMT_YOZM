namespace SmartMES.View.SM
{
    partial class Frm_SM_MenuGroup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SM_MenuGroup));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.MenuTreeList = new DevExpress.XtraTreeList.TreeList();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.LupUseYn = new baseSMT.smtSearchLookUp();
			this.cmdInsert = new baseSMT.smtSearchButton();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdDashboardDesign = new baseSMT.smtSearchButton();
			this.cmdDashboardView = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MenuTreeList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.MenuTreeList);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1335, 365);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// MenuTreeList
			// 
			this.MenuTreeList.ColumnPanelRowHeight = 31;
			this.MenuTreeList.Cursor = System.Windows.Forms.Cursors.Default;
			this.MenuTreeList.KeyFieldName = "Id";
			this.MenuTreeList.Location = new System.Drawing.Point(19, 46);
			this.MenuTreeList.Name = "MenuTreeList";
			this.MenuTreeList.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.MouseDown;
			this.MenuTreeList.OptionsBehavior.PopulateServiceColumns = true;
			this.MenuTreeList.OptionsView.AutoWidth = false;
			this.MenuTreeList.ParentFieldName = "ParentId";
			this.MenuTreeList.RowHeight = 31;
			this.MenuTreeList.Size = new System.Drawing.Size(1297, 300);
			this.MenuTreeList.TabIndex = 179;
			this.MenuTreeList.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.MenuTreeList_GetStateImage);
			this.MenuTreeList.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.MenuTreeList_CustomNodeCellEdit);
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1335, 365);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(1335, 365);
			this.layoutControlGroup1.Text = "메뉴 트리";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.MenuTreeList;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1303, 306);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.LupUseYn);
			this.smtCommonBar1.Controls.Add(this.cmdInsert);
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdDashboardDesign);
			this.smtCommonBar1.Controls.Add(this.cmdDashboardView);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1335, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "메뉴 관리";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 1;
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
			this.cmdDisplay.Location = new System.Drawing.Point(383, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 13;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Visible = false;
			this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
			// 
			// LupUseYn
			// 
			this.LupUseYn.Dock = System.Windows.Forms.DockStyle.Left;
			this.LupUseYn.Location = new System.Drawing.Point(95, 0);
			this.LupUseYn.LookAndFeel.SkinName = "WXI";
			this.LupUseYn.LookAndFeel.UseDefaultLookAndFeel = false;
			this.LupUseYn.Name = "LupUseYn";
			this.LupUseYn.Size = new System.Drawing.Size(288, 40);
			this.LupUseYn.smtLabelTitleValue = "사용여부";
			this.LupUseYn.smtLabelTitleVisible = true;
			this.LupUseYn.smtLabelTitleWidth = 100;
			this.LupUseYn.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.LupUseYn.smtLookUpDataDisplayMember = "";
			this.LupUseYn.smtLookUpDataSource = null;
			this.LupUseYn.smtLookUpDataValueMember = "";
			this.LupUseYn.smtLookUpSelectdIndex = -1;
			this.LupUseYn.smtLookUpText = "";
			this.LupUseYn.smtLookUpValue = "";
			this.LupUseYn.TabIndex = 30;
			this.LupUseYn.Visible = false;
			// 
			// cmdInsert
			// 
			this.cmdInsert.AllowFocus = false;
			this.cmdInsert.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdInsert.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdInsert.Appearance.Options.UseBackColor = true;
			this.cmdInsert.Appearance.Options.UseForeColor = true;
			this.cmdInsert.AutoSize = true;
			this.cmdInsert.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdInsert.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdInsert.ImageOptions.ImageIndex = 1;
			this.cmdInsert.ImageOptions.ImageToTextIndent = 15;
			this.cmdInsert.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdInsert.Location = new System.Drawing.Point(838, 0);
			this.cmdInsert.Name = "cmdInsert";
			this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInsert.Size = new System.Drawing.Size(77, 40);
			this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdInsert.TabIndex = 12;
			this.cmdInsert.Text = "등록";
			this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
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
			this.cmdSave.Location = new System.Drawing.Point(915, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 11;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// cmdDashboardDesign
			// 
			this.cmdDashboardDesign.AllowFocus = false;
			this.cmdDashboardDesign.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDashboardDesign.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDashboardDesign.Appearance.Options.UseBackColor = true;
			this.cmdDashboardDesign.Appearance.Options.UseForeColor = true;
			this.cmdDashboardDesign.AutoSize = true;
			this.cmdDashboardDesign.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdDashboardDesign.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDashboardDesign.ImageOptions.ImageIndex = 1;
			this.cmdDashboardDesign.ImageOptions.ImageToTextIndent = 15;
			this.cmdDashboardDesign.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDashboardDesign.Location = new System.Drawing.Point(992, 0);
			this.cmdDashboardDesign.Name = "cmdDashboardDesign";
			this.cmdDashboardDesign.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDashboardDesign.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDashboardDesign.Size = new System.Drawing.Size(128, 40);
			this.cmdDashboardDesign.smtButtonAutoText = false;
			this.cmdDashboardDesign.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdDashboardDesign.TabIndex = 10;
			this.cmdDashboardDesign.Text = "대시보드 작성";
			this.cmdDashboardDesign.Visible = false;
			this.cmdDashboardDesign.Click += new System.EventHandler(this.cmdDashboardDesign_Click);
			// 
			// cmdDashboardView
			// 
			this.cmdDashboardView.AllowFocus = false;
			this.cmdDashboardView.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDashboardView.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDashboardView.Appearance.Options.UseBackColor = true;
			this.cmdDashboardView.Appearance.Options.UseForeColor = true;
			this.cmdDashboardView.AutoSize = true;
			this.cmdDashboardView.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdDashboardView.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDashboardView.ImageOptions.ImageIndex = 0;
			this.cmdDashboardView.ImageOptions.ImageToTextIndent = 15;
			this.cmdDashboardView.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDashboardView.Location = new System.Drawing.Point(1120, 0);
			this.cmdDashboardView.Name = "cmdDashboardView";
			this.cmdDashboardView.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDashboardView.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDashboardView.Size = new System.Drawing.Size(128, 40);
			this.cmdDashboardView.smtButtonAutoText = false;
			this.cmdDashboardView.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDashboardView.TabIndex = 9;
			this.cmdDashboardView.Text = "대시보드 보기";
			this.cmdDashboardView.Visible = false;
			this.cmdDashboardView.Click += new System.EventHandler(this.cmdDashboardView_Click);
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
			this.cmdClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdClose.Location = new System.Drawing.Point(1248, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(87, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 8;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// Frm_SM_MenuGroup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1335, 405);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.LookAndFeel.SkinName = "WXI";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Name = "Frm_SM_MenuGroup";
			this.Text = "메뉴 관리";
			this.Load += new System.EventHandler(this.Frm_SM_MenuGroup_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MenuTreeList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList MenuTreeList;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdDashboardView;
        private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchButton cmdInsert;
        private baseSMT.smtSearchButton cmdSave;
        private baseSMT.smtSearchButton cmdDashboardDesign;
        private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchLookUp LupUseYn;
	}
}