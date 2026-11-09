
namespace SmartMES.View.MD
{
    partial class Frm_MD_Mold_ChkView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MD_Mold_ChkView));
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.Btn_Search = new baseSMT.smtSearchButton();
            this.Lup_InspTime = new baseSMT.smtSearchLookUp();
            this.Lup_Mold = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_Insp = new DevExpress.XtraGrid.GridControl();
            this.Gv_Insp = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Insp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Insp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView2
            // 
            this.gridView2.Name = "gridView2";
            // 
            // gridView3
            // 
            this.gridView3.Name = "gridView3";
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
            this.cmdClose.Location = new System.Drawing.Point(1462, 0);
            this.cmdClose.Margin = new System.Windows.Forms.Padding(4);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(92, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.Btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_InspTime);
            this.smtCommonBar1.Controls.Add(this.Lup_Mold);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1554, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "금형점검현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 283;
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
            this.Btn_Search.Location = new System.Drawing.Point(1109, 0);
            this.Btn_Search.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Search.Name = "Btn_Search";
            this.Btn_Search.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.Btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.Btn_Search.Size = new System.Drawing.Size(92, 40);
            this.Btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.Btn_Search.TabIndex = 49;
            this.Btn_Search.Text = "조회";
            this.Btn_Search.Click += new System.EventHandler(this.Btn_Click);
            // 
            // Lup_InspTime
            // 
            this.Lup_InspTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_InspTime.Location = new System.Drawing.Point(798, 0);
            this.Lup_InspTime.LookAndFeel.SkinName = "WXI";
            this.Lup_InspTime.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_InspTime.Name = "Lup_InspTime";
            this.Lup_InspTime.Size = new System.Drawing.Size(311, 40);
            this.Lup_InspTime.smtLabelTitleValue = "점검시간";
            this.Lup_InspTime.smtLabelTitleVisible = true;
            this.Lup_InspTime.smtLabelTitleWidth = 125;
            this.Lup_InspTime.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_InspTime.smtLookUpDataDisplayMember = "";
            this.Lup_InspTime.smtLookUpDataSource = null;
            this.Lup_InspTime.smtLookUpDataValueMember = "";
            this.Lup_InspTime.smtLookUpSelectdIndex = -1;
            this.Lup_InspTime.smtLookUpText = "";
            this.Lup_InspTime.smtLookUpValue = "";
            this.Lup_InspTime.TabIndex = 48;
            this.Lup_InspTime.Visible = false;
            // 
            // Lup_Mold
            // 
            this.Lup_Mold.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Mold.Location = new System.Drawing.Point(487, 0);
            this.Lup_Mold.LookAndFeel.SkinName = "WXI";
            this.Lup_Mold.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Mold.Name = "Lup_Mold";
            this.Lup_Mold.Size = new System.Drawing.Size(311, 40);
            this.Lup_Mold.smtLabelTitleValue = "금형";
            this.Lup_Mold.smtLabelTitleVisible = true;
            this.Lup_Mold.smtLabelTitleWidth = 100;
            this.Lup_Mold.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Mold.smtLookUpDataDisplayMember = "";
            this.Lup_Mold.smtLookUpDataSource = null;
            this.Lup_Mold.smtLookUpDataValueMember = "";
            this.Lup_Mold.smtLookUpSelectdIndex = -1;
            this.Lup_Mold.smtLookUpText = "";
            this.Lup_Mold.smtLookUpValue = "";
            this.Lup_Mold.TabIndex = 45;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(142, 0);
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
            this.Dt_Date.smtLabelTitleValue = "조회일자";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 44;
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
            this.cmdExcel.Location = new System.Drawing.Point(1340, 0);
            this.cmdExcel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(122, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 34;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Gc_Insp);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(2456, 142, 979, 538);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1554, 681);
            this.layoutControl1.TabIndex = 284;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Gc_Insp
            // 
            this.Gc_Insp.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.Gc_Insp.Location = new System.Drawing.Point(7, 41);
            this.Gc_Insp.MainView = this.Gv_Insp;
            this.Gc_Insp.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Gc_Insp.Name = "Gc_Insp";
            this.Gc_Insp.Size = new System.Drawing.Size(1540, 612);
            this.Gc_Insp.TabIndex = 6;
            this.Gc_Insp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Insp});
            // 
            // Gv_Insp
            // 
            this.Gv_Insp.ColumnPanelRowHeight = 29;
            this.Gv_Insp.DetailHeight = 412;
            this.Gv_Insp.GridControl = this.Gc_Insp;
            this.Gv_Insp.Name = "Gv_Insp";
            this.Gv_Insp.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_Insp.OptionsView.ShowGroupPanel = false;
            this.Gv_Insp.RowHeight = 29;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.splitterItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1554, 681);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleSeparator1,
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1554, 661);
            this.layoutControlGroup3.Text = "점검항목";
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 618);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(1546, 1);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.Gc_Insp;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1546, 618);
            this.layoutControlItem2.Text = "layoutControlItem3";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 661);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1554, 20);
            // 
            // Frm_MD_Mold_ChkView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 721);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_MD_Mold_ChkView";
            this.Text = "금형점검현황";
            this.Load += new System.EventHandler(this.Frm_MD_Mold_ChkView_View_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Insp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Insp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtCommonBar smtCommonBar1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private baseSMT.smtSearchButton cmdExcel;
        private baseSMT.smtSearchLookUp Lup_Mold;
        private baseSMT.smtSearchDatetime Dt_Date;
        private DevExpress.XtraGrid.GridControl Gc_Insp;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Insp;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private baseSMT.smtSearchButton Btn_Search;
        private baseSMT.smtSearchLookUp Lup_InspTime;
    }
}