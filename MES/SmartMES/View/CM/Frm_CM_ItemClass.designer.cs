
namespace SmartMES.View.CM

{
    partial class Frm_CM_ItemClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_ItemClass));
            this.gcItemClass = new DevExpress.XtraGrid.GridControl();
            this.gvItemClass = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Asy = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Goods = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Material = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Subsidiary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Consume = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Jig = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.lupItemClass = new baseSMT.smtSearchLookUp();
            this.cmdInsert = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.gcItemClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Asy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Goods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Material)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Subsidiary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Consume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Jig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcItemClass
            // 
            this.gcItemClass.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gcItemClass.Location = new System.Drawing.Point(7, 41);
            this.gcItemClass.MainView = this.gvItemClass;
            this.gcItemClass.Margin = new System.Windows.Forms.Padding(4);
            this.gcItemClass.Name = "gcItemClass";
            this.gcItemClass.Size = new System.Drawing.Size(1675, 659);
            this.gcItemClass.TabIndex = 1;
            this.gcItemClass.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvItemClass,
            this.Gv_Asy,
            this.Gv_Goods,
            this.Gv_Material,
            this.Gv_Subsidiary,
            this.Gv_Consume,
            this.Gv_Jig});
            // 
            // gvItemClass
            // 
            this.gvItemClass.DetailHeight = 467;
            this.gvItemClass.GridControl = this.gcItemClass;
            this.gvItemClass.Name = "gvItemClass";
            this.gvItemClass.OptionsEditForm.PopupEditFormWidth = 1142;
            this.gvItemClass.OptionsView.ShowGroupPanel = false;
            this.gvItemClass.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvItemClass_CellValueChanged);
            // 
            // Gv_Asy
            // 
            this.Gv_Asy.DetailHeight = 467;
            this.Gv_Asy.GridControl = this.gcItemClass;
            this.Gv_Asy.Name = "Gv_Asy";
            this.Gv_Asy.OptionsEditForm.PopupEditFormWidth = 1142;
            this.Gv_Asy.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Goods
            // 
            this.Gv_Goods.DetailHeight = 467;
            this.Gv_Goods.GridControl = this.gcItemClass;
            this.Gv_Goods.Name = "Gv_Goods";
            this.Gv_Goods.OptionsEditForm.PopupEditFormWidth = 1142;
            this.Gv_Goods.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Material
            // 
            this.Gv_Material.DetailHeight = 467;
            this.Gv_Material.GridControl = this.gcItemClass;
            this.Gv_Material.Name = "Gv_Material";
            this.Gv_Material.OptionsEditForm.PopupEditFormWidth = 1142;
            this.Gv_Material.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Subsidiary
            // 
            this.Gv_Subsidiary.DetailHeight = 467;
            this.Gv_Subsidiary.GridControl = this.gcItemClass;
            this.Gv_Subsidiary.Name = "Gv_Subsidiary";
            this.Gv_Subsidiary.OptionsEditForm.PopupEditFormWidth = 1142;
            this.Gv_Subsidiary.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Consume
            // 
            this.Gv_Consume.DetailHeight = 467;
            this.Gv_Consume.GridControl = this.gcItemClass;
            this.Gv_Consume.Name = "Gv_Consume";
            this.Gv_Consume.OptionsEditForm.PopupEditFormWidth = 1142;
            this.Gv_Consume.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Jig
            // 
            this.Gv_Jig.DetailHeight = 467;
            this.Gv_Jig.GridControl = this.gcItemClass;
            this.Gv_Jig.Name = "Gv_Jig";
            this.Gv_Jig.OptionsEditForm.PopupEditFormWidth = 1142;
            this.Gv_Jig.OptionsView.ShowGroupPanel = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.lupItemClass);
            this.smtCommonBar1.Controls.Add(this.cmdInsert);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.cmdDelete);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1689, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "품목분류정보";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 282;
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
            this.cmdDisplay.Location = new System.Drawing.Point(502, 0);
            this.cmdDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(92, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 34;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Click += new System.EventHandler(this.cmd_Click);
            // 
            // lupItemClass
            // 
            this.lupItemClass.Dock = System.Windows.Forms.DockStyle.Left;
            this.lupItemClass.Location = new System.Drawing.Point(142, 0);
            this.lupItemClass.LookAndFeel.SkinName = "WXI";
            this.lupItemClass.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lupItemClass.Margin = new System.Windows.Forms.Padding(4);
            this.lupItemClass.Name = "lupItemClass";
            this.lupItemClass.Size = new System.Drawing.Size(360, 40);
            this.lupItemClass.smtLabelTitleValue = "품목분류";
            this.lupItemClass.smtLabelTitleVisible = true;
            this.lupItemClass.smtLabelTitleWidth = 100;
            this.lupItemClass.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.lupItemClass.smtLookUpDataDisplayMember = "코드명";
            this.lupItemClass.smtLookUpDataSource = null;
            this.lupItemClass.smtLookUpDataValueMember = "코드";
            this.lupItemClass.smtLookUpSelectdIndex = -1;
            this.lupItemClass.smtLookUpText = "";
            this.lupItemClass.smtLookUpValue = "품목분류";
            this.lupItemClass.TabIndex = 33;
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
            this.cmdInsert.Location = new System.Drawing.Point(1321, 0);
            this.cmdInsert.Margin = new System.Windows.Forms.Padding(4);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdInsert.Size = new System.Drawing.Size(92, 40);
            this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdInsert.TabIndex = 7;
            this.cmdInsert.Tag = "Ins";
            this.cmdInsert.Text = "등록";
            this.cmdInsert.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdSave.Location = new System.Drawing.Point(1413, 0);
            this.cmdSave.Margin = new System.Windows.Forms.Padding(4);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(92, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "저장";
            this.cmdSave.Click += new System.EventHandler(this.cmd_Click);
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
            this.cmdDelete.Location = new System.Drawing.Point(1505, 0);
            this.cmdDelete.Margin = new System.Windows.Forms.Padding(4);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(92, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Tag = "Del";
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdClose.Location = new System.Drawing.Point(1597, 0);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(92, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmd_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcItemClass);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1689, 707);
            this.layoutControl1.TabIndex = 283;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1689, 707);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "세부항목";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1689, 707);
            this.layoutControlGroup1.Text = "세부항목";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcItemClass;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1681, 665);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmItemClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1689, 747);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_CM_ItemClass";
            this.Text = "품목분류정보";
            this.Load += new System.EventHandler(this.Frm_CM_ItemClass_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcItemClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvItemClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Asy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Goods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Material)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Subsidiary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Consume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Jig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gcItemClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gvItemClass;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Asy;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Goods;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Material;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Subsidiary;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Consume;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Jig;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdInsert;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDelete;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchLookUp lupItemClass;
    }
}