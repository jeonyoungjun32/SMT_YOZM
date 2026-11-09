
namespace SmartMES.View.Management
{
    partial class Frm_MM_PurchaseAnalysis_View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_PurchaseAnalysis_View));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_Purchase = new DevExpress.XtraGrid.GridControl();
            this.Gv_Purchase = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.lbl_SummarySum = new System.Windows.Forms.Label();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.LupItem = new baseSMT.smtSearchLookUp();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.LupCustomer = new baseSMT.smtSearchLookUp();
            this.barDate = new baseSMT.smtSearchDatetime();
            this.btn_Colose = new baseSMT.smtSearchButton();
            this.LupItemClass1 = new baseSMT.smtSearchLookUp();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Purchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Purchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.Gc_Purchase);
            this.groupControl2.Controls.Add(this.panelControl3);
            this.groupControl2.Location = new System.Drawing.Point(14, 56);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1300, 591);
            this.groupControl2.TabIndex = 275;
            this.groupControl2.Text = "분석현황";
            // 
            // Gc_Purchase
            // 
            this.Gc_Purchase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_Purchase.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.Gc_Purchase.Location = new System.Drawing.Point(2, 66);
            this.Gc_Purchase.MainView = this.Gv_Purchase;
            this.Gc_Purchase.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Gc_Purchase.Name = "Gc_Purchase";
            this.Gc_Purchase.Size = new System.Drawing.Size(1296, 523);
            this.Gc_Purchase.TabIndex = 102;
            this.Gc_Purchase.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Purchase});
            // 
            // Gv_Purchase
            // 
            this.Gv_Purchase.BandPanelRowHeight = 19;
            this.Gv_Purchase.ColumnPanelRowHeight = 19;
            this.Gv_Purchase.DetailHeight = 495;
            this.Gv_Purchase.GridControl = this.Gc_Purchase;
            this.Gv_Purchase.Name = "Gv_Purchase";
            this.Gv_Purchase.OptionsBehavior.Editable = false;
            this.Gv_Purchase.OptionsView.ColumnAutoWidth = false;
            this.Gv_Purchase.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.Gv_Purchase.OptionsView.ShowColumnHeaders = false;
            this.Gv_Purchase.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.Gv_Purchase.OptionsView.ShowGroupPanel = false;
            this.Gv_Purchase.OptionsView.ShowIndicator = false;
            this.Gv_Purchase.RowHeight = 38;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.lbl_SummarySum);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(2, 29);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1296, 37);
            this.panelControl3.TabIndex = 0;
            // 
            // lbl_SummarySum
            // 
            this.lbl_SummarySum.BackColor = System.Drawing.Color.Yellow;
            this.lbl_SummarySum.Location = new System.Drawing.Point(79, 7);
            this.lbl_SummarySum.Name = "lbl_SummarySum";
            this.lbl_SummarySum.Size = new System.Drawing.Size(229, 23);
            this.lbl_SummarySum.TabIndex = 339;
            this.lbl_SummarySum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(3, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelControl1.Size = new System.Drawing.Size(70, 26);
            this.labelControl1.TabIndex = 340;
            this.labelControl1.Text = "매입 총액";
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.LupItem);
            this.smtCommonBar1.Controls.Add(this.LupItemClass1);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.LupCustomer);
            this.smtCommonBar1.Controls.Add(this.barDate);
            this.smtCommonBar1.Controls.Add(this.btn_Colose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1327, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "매입분석현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 291;
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
            this.smtSearchButton1.Location = new System.Drawing.Point(1102, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.smtSearchButton1.TabIndex = 8;
            this.smtSearchButton1.Text = "조회";
            this.smtSearchButton1.Click += new System.EventHandler(this.Btn_Click);
            // 
            // LupItem
            // 
            this.LupItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupItem.Location = new System.Drawing.Point(834, 0);
            this.LupItem.LookAndFeel.SkinName = "WXI";
            this.LupItem.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupItem.Name = "LupItem";
            this.LupItem.Size = new System.Drawing.Size(268, 40);
            this.LupItem.smtLabelTitleValue = "품목";
            this.LupItem.smtLabelTitleVisible = true;
            this.LupItem.smtLabelTitleWidth = 80;
            this.LupItem.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupItem.smtLookUpDataDisplayMember = "";
            this.LupItem.smtLookUpDataSource = null;
            this.LupItem.smtLookUpDataValueMember = "";
            this.LupItem.smtLookUpSelectdIndex = -1;
            this.LupItem.smtLookUpText = "";
            this.LupItem.smtLookUpValue = "";
            this.LupItem.TabIndex = 16;
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
            this.cmdExcel.Location = new System.Drawing.Point(1149, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(101, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 18;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // LupCustomer
            // 
            this.LupCustomer.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupCustomer.Location = new System.Drawing.Point(316, 0);
            this.LupCustomer.LookAndFeel.SkinName = "WXI";
            this.LupCustomer.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupCustomer.Name = "LupCustomer";
            this.LupCustomer.Size = new System.Drawing.Size(265, 40);
            this.LupCustomer.smtLabelTitleValue = "거래처";
            this.LupCustomer.smtLabelTitleVisible = true;
            this.LupCustomer.smtLabelTitleWidth = 80;
            this.LupCustomer.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupCustomer.smtLookUpDataDisplayMember = "";
            this.LupCustomer.smtLookUpDataSource = null;
            this.LupCustomer.smtLookUpDataValueMember = "";
            this.LupCustomer.smtLookUpSelectdIndex = -1;
            this.LupCustomer.smtLookUpText = "";
            this.LupCustomer.smtLookUpValue = "";
            this.LupCustomer.TabIndex = 17;
            // 
            // barDate
            // 
            this.barDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDate.Location = new System.Drawing.Point(116, 0);
            this.barDate.LookAndFeel.SkinName = "WXI";
            this.barDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barDate.Name = "barDate";
            this.barDate.Size = new System.Drawing.Size(200, 40);
            this.barDate.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.barDate.smtComboTitleItems = new string[0];
            this.barDate.smtComboTitleValue = "";
            this.barDate.smtComboTitleVisible = false;
            this.barDate.smtComboTitleWidth = 100;
            this.barDate.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.barDate.smtEndDateTimeValue = new System.DateTime(2025, 1, 13, 10, 39, 30, 699);
            this.barDate.smtEndDateTimeVisible = false;
            this.barDate.smtLabelTitleValue = "기준년월";
            this.barDate.smtLabelTitleVisible = true;
            this.barDate.smtLabelTitleWidth = 80;
            this.barDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.barDate.smtStartDateTimeValue = new System.DateTime(2025, 1, 13, 10, 39, 30, 695);
            this.barDate.smtStartDateTimeVisible = true;
            this.barDate.TabIndex = 9;
            // 
            // btn_Colose
            // 
            this.btn_Colose.AllowFocus = false;
            this.btn_Colose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Colose.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Colose.Appearance.Options.UseBackColor = true;
            this.btn_Colose.Appearance.Options.UseForeColor = true;
            this.btn_Colose.AutoSize = true;
            this.btn_Colose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Colose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Colose.ImageOptions.ImageIndex = 5;
            this.btn_Colose.ImageOptions.ImageToTextIndent = 15;
            this.btn_Colose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Colose.Location = new System.Drawing.Point(1250, 0);
            this.btn_Colose.Name = "btn_Colose";
            this.btn_Colose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Colose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Colose.Size = new System.Drawing.Size(77, 40);
            this.btn_Colose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Colose.TabIndex = 1;
            this.btn_Colose.Text = "닫기";
            this.btn_Colose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // LupItemClass1
            // 
            this.LupItemClass1.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupItemClass1.Location = new System.Drawing.Point(581, 0);
            this.LupItemClass1.LookAndFeel.SkinName = "WXI";
            this.LupItemClass1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupItemClass1.Name = "LupItemClass1";
            this.LupItemClass1.Size = new System.Drawing.Size(253, 40);
            this.LupItemClass1.smtLabelTitleValue = "품목분류 1";
            this.LupItemClass1.smtLabelTitleVisible = true;
            this.LupItemClass1.smtLabelTitleWidth = 80;
            this.LupItemClass1.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupItemClass1.smtLookUpDataDisplayMember = "";
            this.LupItemClass1.smtLookUpDataSource = null;
            this.LupItemClass1.smtLookUpDataValueMember = "";
            this.LupItemClass1.smtLookUpSelectdIndex = -1;
            this.LupItemClass1.smtLookUpText = "";
            this.LupItemClass1.smtLookUpValue = "";
            this.LupItemClass1.TabIndex = 19;
            this.LupItemClass1.smtLookUpEditValueChanged += new System.EventHandler(this.LupItemClass1_smtLookUpEditValueChanged);
            // 
            // Frm_MM_PurchaseAnalysis_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 660);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_MM_PurchaseAnalysis_View";
            this.Text = "매입분석현황";
            this.Load += new System.EventHandler(this.Frm_MM_PurchaseAnalysis_View_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Purchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Purchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.Label lbl_SummarySum;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl Gc_Purchase;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView Gv_Purchase;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton smtSearchButton1;
		private baseSMT.smtSearchLookUp LupItem;
		private baseSMT.smtSearchButton cmdExcel;
		private baseSMT.smtSearchLookUp LupCustomer;
		private baseSMT.smtSearchDatetime barDate;
		private baseSMT.smtSearchButton btn_Colose;
        private baseSMT.smtSearchLookUp LupItemClass1;
        //private Controls.Btn_ExcelDown_ALL Btn_ExcelDown_ALL;
    }
}