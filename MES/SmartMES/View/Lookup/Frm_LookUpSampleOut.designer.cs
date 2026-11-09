
namespace SmartMES.View.Lookup
{
    partial class Frm_LookUpSampleOut
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LookUpSampleOut));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.GcBM_SalesD = new DevExpress.XtraGrid.GridControl();
            this.GvBM_SalesD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.smtSalesDate = new baseSMT.smtBodyDatetime();
            this.smtQty = new baseSMT.smtBodyTextBox();
            this.smtRemark = new baseSMT.smtBodyMemoEdit();
            this.smtShipToCode = new baseSMT.smtBodyLookUp();
            this.smtExpirationDate = new baseSMT.smtBodyDatetime();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btnSave = new baseSMT.smtSearchButton();
            this.btnClose = new baseSMT.smtSearchButton();
            this.smtItemCode = new baseSMT.smtBodyLookUp();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.GcBM_SalesD);
            this.layoutControl1.Controls.Add(this.smtSalesDate);
            this.layoutControl1.Controls.Add(this.smtQty);
            this.layoutControl1.Controls.Add(this.smtRemark);
            this.layoutControl1.Controls.Add(this.smtShipToCode);
            this.layoutControl1.Controls.Add(this.smtExpirationDate);
            this.layoutControl1.Controls.Add(this.smtItemCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1108, 94, 812, 500);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1354, 325);
            this.layoutControl1.TabIndex = 102;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // GcBM_SalesD
            // 
            this.GcBM_SalesD.Location = new System.Drawing.Point(32, 372);
            this.GcBM_SalesD.MainView = this.GvBM_SalesD;
            this.GcBM_SalesD.Name = "GcBM_SalesD";
            this.GcBM_SalesD.Size = new System.Drawing.Size(1273, 20);
            this.GcBM_SalesD.TabIndex = 124;
            this.GcBM_SalesD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvBM_SalesD});
            // 
            // GvBM_SalesD
            // 
            this.GvBM_SalesD.GridControl = this.GcBM_SalesD;
            this.GvBM_SalesD.Name = "GvBM_SalesD";
            // 
            // smtSalesDate
            // 
            this.smtSalesDate.Location = new System.Drawing.Point(32, 59);
            this.smtSalesDate.LookAndFeel.SkinName = "WXI";
            this.smtSalesDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtSalesDate.Name = "smtSalesDate";
            this.smtSalesDate.Size = new System.Drawing.Size(612, 32);
            this.smtSalesDate.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.smtSalesDate.smtDateTimeMust = true;
            this.smtSalesDate.smtDateTimeReadOnly = false;
            this.smtSalesDate.smtDateTimeValue = new System.DateTime(((long)(0)));
            this.smtSalesDate.smtLabelTitleValue = "출고일자";
            this.smtSalesDate.smtLabelTitleVisible = true;
            this.smtSalesDate.smtLabelTitleWidth = 100;
            this.smtSalesDate.TabIndex = 113;
            // 
            // smtQty
            // 
            this.smtQty.Location = new System.Drawing.Point(32, 170);
            this.smtQty.Name = "smtQty";
            this.smtQty.Size = new System.Drawing.Size(1273, 32);
            this.smtQty.smtLabelTitleValue = "총수";
            this.smtQty.smtLabelTitleVisible = true;
            this.smtQty.smtLabelTitleWidth = 100;
            this.smtQty.smtTextBoxDecimal = 0;
            this.smtQty.smtTextBoxMust = false;
            this.smtQty.smtTextBoxNumber = true;
            this.smtQty.smtTextBoxReadOnly = false;
            this.smtQty.smtTextBoxValue = "";
            this.smtQty.TabIndex = 122;
            // 
            // smtRemark
            // 
            this.smtRemark.Location = new System.Drawing.Point(32, 207);
            this.smtRemark.LookAndFeel.SkinName = "WXI";
            this.smtRemark.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtRemark.MaximumSize = new System.Drawing.Size(0, 100);
            this.smtRemark.Name = "smtRemark";
            this.smtRemark.Size = new System.Drawing.Size(1273, 100);
            this.smtRemark.smtLabelTitleValue = "비고";
            this.smtRemark.smtLabelTitleVisible = true;
            this.smtRemark.smtLabelTitleWidth = 100;
            this.smtRemark.smtMemoMust = false;
            this.smtRemark.smtMemoReadOnly = false;
            this.smtRemark.smtMemoValue = "";
            this.smtRemark.TabIndex = 119;
            // 
            // smtShipToCode
            // 
            this.smtShipToCode.Location = new System.Drawing.Point(32, 96);
            this.smtShipToCode.Name = "smtShipToCode";
            this.smtShipToCode.Size = new System.Drawing.Size(1273, 32);
            this.smtShipToCode.smtLabelTitleValue = "납품처";
            this.smtShipToCode.smtLabelTitleVisible = true;
            this.smtShipToCode.smtLabelTitleWidth = 100;
            this.smtShipToCode.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.smtShipToCode.smtLookUpDataDisplayMember = "";
            this.smtShipToCode.smtLookUpDataSource = null;
            this.smtShipToCode.smtLookUpDataValueMember = "";
            this.smtShipToCode.smtLookUpMust = false;
            this.smtShipToCode.smtLookUpReadOnly = false;
            this.smtShipToCode.smtLookUpSelectdIndex = -1;
            this.smtShipToCode.smtLookUpText = "";
            this.smtShipToCode.smtLookUpValue = "";
            this.smtShipToCode.TabIndex = 3;
            // 
            // smtExpirationDate
            // 
            this.smtExpirationDate.Location = new System.Drawing.Point(650, 59);
            this.smtExpirationDate.LookAndFeel.SkinName = "WXI";
            this.smtExpirationDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtExpirationDate.Name = "smtExpirationDate";
            this.smtExpirationDate.Size = new System.Drawing.Size(655, 32);
            this.smtExpirationDate.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.smtExpirationDate.smtDateTimeMust = true;
            this.smtExpirationDate.smtDateTimeReadOnly = false;
            this.smtExpirationDate.smtDateTimeValue = new System.DateTime(((long)(0)));
            this.smtExpirationDate.smtLabelTitleValue = "유효일자";
            this.smtExpirationDate.smtLabelTitleVisible = true;
            this.smtExpirationDate.smtLabelTitleWidth = 100;
            this.smtExpirationDate.TabIndex = 113;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1337, 424);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem11,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1311, 313);
            this.layoutControlGroup1.Text = "샘플링 출고";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.smtRemark;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 148);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(0, 106);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(7, 106);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(1279, 106);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.smtSalesDate;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.MaxSize = new System.Drawing.Size(0, 37);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(7, 37);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(618, 37);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.smtShipToCode;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 37);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 37);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(7, 37);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1279, 37);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.smtQty;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 111);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 37);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(7, 37);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1279, 37);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 313);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(1311, 85);
            this.layoutControlGroup2.Text = "샘플링출고 상세";
            this.layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.GcBM_SalesD;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(1279, 26);
            this.layoutControlItem12.Text = "출고상세";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.smtExpirationDate;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem1.Location = new System.Drawing.Point(618, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 37);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(7, 37);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(661, 37);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem11";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btnSave);
            this.smtCommonBar1.Controls.Add(this.btnClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1354, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleAutoText = false;
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "샘플링 출고정보";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.AllowFocus = false;
            this.btnSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.AutoSize = true;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnSave.ImageOptions.ImageIndex = 4;
            this.btnSave.ImageOptions.ImageToTextIndent = 15;
            this.btnSave.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btnSave.Location = new System.Drawing.Point(1200, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSave.Size = new System.Drawing.Size(77, 40);
            this.btnSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.btnClose.Location = new System.Drawing.Point(1277, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClose.Size = new System.Drawing.Size(77, 40);
            this.btnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "닫기";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // smtItemCode
            // 
            this.smtItemCode.Location = new System.Drawing.Point(32, 133);
            this.smtItemCode.LookAndFeel.SkinName = "WXI";
            this.smtItemCode.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtItemCode.Name = "smtItemCode";
            this.smtItemCode.Size = new System.Drawing.Size(1273, 32);
            this.smtItemCode.smtLabelTitleValue = "품목명";
            this.smtItemCode.smtLabelTitleVisible = true;
            this.smtItemCode.smtLabelTitleWidth = 100;
            this.smtItemCode.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.smtItemCode.smtLookUpDataDisplayMember = "";
            this.smtItemCode.smtLookUpDataSource = null;
            this.smtItemCode.smtLookUpDataValueMember = "";
            this.smtItemCode.smtLookUpMust = false;
            this.smtItemCode.smtLookUpReadOnly = false;
            this.smtItemCode.smtLookUpSelectdIndex = -1;
            this.smtItemCode.smtLookUpText = "";
            this.smtItemCode.smtLookUpValue = "";
            this.smtItemCode.TabIndex = 3;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.smtItemCode;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 74);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 37);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(7, 37);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1279, 37);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem2";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Frm_LookUpSampleOut
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1354, 365);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "Frm_LookUpSampleOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Frm_LookUpSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btnSave;
        private baseSMT.smtSearchButton btnClose;
        private baseSMT.smtBodyLookUp smtShipToCode;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private baseSMT.smtBodyMemoEdit smtRemark;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
		private baseSMT.smtBodyDatetime smtSalesDate;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
		private DevExpress.XtraGrid.GridControl GcBM_SalesD;
		private DevExpress.XtraGrid.Views.Grid.GridView GvBM_SalesD;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private baseSMT.smtBodyTextBox smtQty;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private baseSMT.smtBodyDatetime smtExpirationDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtBodyLookUp smtItemCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}