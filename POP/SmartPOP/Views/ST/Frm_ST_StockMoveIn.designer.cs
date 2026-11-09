
namespace SmartPOP.View.ST
{
    partial class Frm_ST_StockMoveIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_ST_StockMoveIn));
            this.Panel1 = new DevExpress.XtraEditors.PanelControl();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.smtSearchButton3 = new baseSMT.smtSearchButton();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.btnClose = new baseSMT.smtSearchButton();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.LupMaterialType = new baseSMT.smtSearchLookUp();
            this.LupWareType = new baseSMT.smtSearchLookUp();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcStock = new DevExpress.XtraGrid.GridControl();
            this.gvStock = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(2, 23);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(1465, 45);
            this.Panel1.TabIndex = 271;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.smtSearchButton3);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.btnClose);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.LupMaterialType);
            this.smtCommonBar1.Controls.Add(this.LupWareType);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1112, 45);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "자재재고 이동/조정";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 0;
            // 
            // smtSearchButton3
            // 
            this.smtSearchButton3.AllowFocus = false;
            this.smtSearchButton3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton3.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton3.Appearance.Options.UseBackColor = true;
            this.smtSearchButton3.Appearance.Options.UseForeColor = true;
            this.smtSearchButton3.AutoSize = true;
            this.smtSearchButton3.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton3.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton3.ImageOptions.Image = global::SmartPOP.Properties.Resources.arrowR_16X16;
            this.smtSearchButton3.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton3.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton3.Location = new System.Drawing.Point(854, 0);
            this.smtSearchButton3.Name = "smtSearchButton3";
            this.smtSearchButton3.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton3.Size = new System.Drawing.Size(74, 45);
            this.smtSearchButton3.TabIndex = 16;
            this.smtSearchButton3.Text = "이동";
            this.smtSearchButton3.Click += new System.EventHandler(this.smtSearchButton3_Click);
            // 
            // cmdExcel
            // 
            this.cmdExcel.AllowFocus = false;
            this.cmdExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdExcel.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdExcel.Appearance.Options.UseBackColor = true;
            this.cmdExcel.Appearance.Options.UseForeColor = true;
            this.cmdExcel.AutoSize = true;
            this.cmdExcel.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdExcel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdExcel.ImageOptions.ImageIndex = 9;
            this.cmdExcel.ImageOptions.ImageToTextIndent = 15;
            this.cmdExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdExcel.Location = new System.Drawing.Point(928, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.Size = new System.Drawing.Size(105, 45);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 14;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowFocus = false;
            this.btnClose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.AutoSize = true;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.ImageOptions.ImageIndex = 5;
            this.btnClose.ImageOptions.ImageToTextIndent = 15;
            this.btnClose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btnClose.Location = new System.Drawing.Point(1033, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnClose.Size = new System.Drawing.Size(79, 45);
            this.btnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "닫기";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // smtSearchButton1
            // 
            this.smtSearchButton1.AllowFocus = false;
            this.smtSearchButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton1.Appearance.Options.UseBackColor = true;
            this.smtSearchButton1.Appearance.Options.UseForeColor = true;
            this.smtSearchButton1.AutoSize = true;
            this.smtSearchButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.smtSearchButton1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton1.ImageOptions.ImageIndex = 0;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(696, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.Size = new System.Drawing.Size(79, 45);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.smtSearchButton1.TabIndex = 11;
            this.smtSearchButton1.Text = "조회";
            this.smtSearchButton1.Click += new System.EventHandler(this.smtSearchButton1_Click);
            // 
            // LupMaterialType
            // 
            this.LupMaterialType.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupMaterialType.Location = new System.Drawing.Point(431, 0);
            this.LupMaterialType.LookAndFeel.SkinName = "WXI";
            this.LupMaterialType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupMaterialType.Name = "LupMaterialType";
            this.LupMaterialType.Size = new System.Drawing.Size(265, 45);
            this.LupMaterialType.smtLabelTitleValue = "품목구분";
            this.LupMaterialType.smtLabelTitleVisible = true;
            this.LupMaterialType.smtLabelTitleWidth = 70;
            this.LupMaterialType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupMaterialType.smtLookUpDataDisplayMember = "코드명";
            this.LupMaterialType.smtLookUpDataSource = null;
            this.LupMaterialType.smtLookUpDataValueMember = "코드";
            this.LupMaterialType.smtLookUpSelectdIndex = -1;
            this.LupMaterialType.smtLookUpText = "";
            this.LupMaterialType.smtLookUpValue = "";
            this.LupMaterialType.TabIndex = 12;
            // 
            // LupWareType
            // 
            this.LupWareType.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupWareType.Location = new System.Drawing.Point(166, 0);
            this.LupWareType.LookAndFeel.SkinName = "WXI";
            this.LupWareType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupWareType.Name = "LupWareType";
            this.LupWareType.Size = new System.Drawing.Size(265, 45);
            this.LupWareType.smtLabelTitleValue = "창고";
            this.LupWareType.smtLabelTitleVisible = true;
            this.LupWareType.smtLabelTitleWidth = 50;
            this.LupWareType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupWareType.smtLookUpDataDisplayMember = "코드명";
            this.LupWareType.smtLookUpDataSource = null;
            this.LupWareType.smtLookUpDataValueMember = "코드";
            this.LupWareType.smtLookUpSelectdIndex = -1;
            this.LupWareType.smtLookUpText = "";
            this.LupWareType.smtLookUpValue = "";
            this.LupWareType.TabIndex = 10;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcStock);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1112, 542);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcStock
            // 
            this.gcStock.Location = new System.Drawing.Point(3, 3);
            this.gcStock.MainView = this.gvStock;
            this.gcStock.Name = "gcStock";
            this.gcStock.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.gcStock.Size = new System.Drawing.Size(1106, 536);
            this.gcStock.TabIndex = 4;
            this.gcStock.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStock});
            // 
            // gvStock
            // 
            this.gvStock.GridControl = this.gcStock;
            this.gvStock.Name = "gvStock";
            this.gvStock.OptionsView.ShowAutoFilterRow = true;
            this.gvStock.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1112, 542);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcStock;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1112, 542);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmStockMoveIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1112, 587);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "FrmStockMoveIn";
            this.Text = "자재재고 이동/조정";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Event_FormClosing);
            this.Load += new System.EventHandler(this.Frm_MT_Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl Panel1;
        private baseSMT.smtCommonBar smtCommonBar1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcStock;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStock;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtSearchLookUp LupMaterialType;
        private baseSMT.smtSearchLookUp LupWareType;
        private baseSMT.smtSearchButton cmdExcel;
        private baseSMT.smtSearchButton btnClose;
        private baseSMT.smtSearchButton smtSearchButton3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
    }
}