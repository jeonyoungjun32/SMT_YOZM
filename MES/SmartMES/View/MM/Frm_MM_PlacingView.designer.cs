namespace SmartMES.View.MM
{
    partial class Frm_MM_PlacingView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_PlacingView));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Cust = new baseSMT.smtSearchLookUp();
            this.Lup_PlacingType = new baseSMT.smtSearchLookUp();
            this.Lup_State = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.smtSearchButton2 = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcCustomer = new DevExpress.XtraGrid.GridControl();
            this.gvCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcPlacingList = new DevExpress.XtraGrid.GridControl();
            this.gvPlacingList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlacingList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlacingList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Cust);
            this.smtCommonBar1.Controls.Add(this.Lup_PlacingType);
            this.smtCommonBar1.Controls.Add(this.Lup_State);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdDelete);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton2);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1474, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "발주 현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 4;
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
            this.btn_Search.Location = new System.Drawing.Point(1160, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 14;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.CmdSearch_Click_1);
            // 
            // Lup_Cust
            // 
            this.Lup_Cust.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Cust.Location = new System.Drawing.Point(872, 0);
            this.Lup_Cust.LookAndFeel.SkinName = "WXI";
            this.Lup_Cust.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Cust.Name = "Lup_Cust";
            this.Lup_Cust.Size = new System.Drawing.Size(288, 40);
            this.Lup_Cust.smtLabelTitleValue = "거래처";
            this.Lup_Cust.smtLabelTitleVisible = true;
            this.Lup_Cust.smtLabelTitleWidth = 100;
            this.Lup_Cust.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Cust.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Cust.smtLookUpDataSource = null;
            this.Lup_Cust.smtLookUpDataValueMember = "코드";
            this.Lup_Cust.smtLookUpSelectdIndex = -1;
            this.Lup_Cust.smtLookUpText = "";
            this.Lup_Cust.smtLookUpValue = "";
            this.Lup_Cust.TabIndex = 13;
            // 
            // Lup_PlacingType
            // 
            this.Lup_PlacingType.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_PlacingType.Location = new System.Drawing.Point(656, 0);
            this.Lup_PlacingType.LookAndFeel.SkinName = "WXI";
            this.Lup_PlacingType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_PlacingType.Name = "Lup_PlacingType";
            this.Lup_PlacingType.Size = new System.Drawing.Size(216, 40);
            this.Lup_PlacingType.smtLabelTitleValue = "발주구분";
            this.Lup_PlacingType.smtLabelTitleVisible = true;
            this.Lup_PlacingType.smtLabelTitleWidth = 80;
            this.Lup_PlacingType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_PlacingType.smtLookUpDataDisplayMember = "코드명";
            this.Lup_PlacingType.smtLookUpDataSource = null;
            this.Lup_PlacingType.smtLookUpDataValueMember = "코드";
            this.Lup_PlacingType.smtLookUpSelectdIndex = -1;
            this.Lup_PlacingType.smtLookUpText = "";
            this.Lup_PlacingType.smtLookUpValue = "";
            this.Lup_PlacingType.TabIndex = 12;
            // 
            // Lup_State
            // 
            this.Lup_State.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_State.Location = new System.Drawing.Point(440, 0);
            this.Lup_State.LookAndFeel.SkinName = "WXI";
            this.Lup_State.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_State.Name = "Lup_State";
            this.Lup_State.Size = new System.Drawing.Size(216, 40);
            this.Lup_State.smtLabelTitleValue = "상태";
            this.Lup_State.smtLabelTitleVisible = true;
            this.Lup_State.smtLabelTitleWidth = 70;
            this.Lup_State.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_State.smtLookUpDataDisplayMember = "코드명";
            this.Lup_State.smtLookUpDataSource = null;
            this.Lup_State.smtLookUpDataValueMember = "코드";
            this.Lup_State.smtLookUpSelectdIndex = -1;
            this.Lup_State.smtLookUpText = "";
            this.Lup_State.smtLookUpValue = "";
            this.Lup_State.TabIndex = 11;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(95, 0);
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
            this.Dt_Date.TabIndex = 10;
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
            this.cmdDelete.ImageOptions.ImageIndex = 9;
            this.cmdDelete.ImageOptions.ImageToTextIndent = 15;
            this.cmdDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdDelete.Location = new System.Drawing.Point(1296, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(101, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdDelete.TabIndex = 6;
            this.cmdDelete.Text = "엑셀변환";
            this.cmdDelete.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // smtSearchButton2
            // 
            this.smtSearchButton2.AllowFocus = false;
            this.smtSearchButton2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton2.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton2.Appearance.Options.UseBackColor = true;
            this.smtSearchButton2.Appearance.Options.UseForeColor = true;
            this.smtSearchButton2.AutoSize = true;
            this.smtSearchButton2.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton2.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton2.ImageOptions.ImageIndex = 5;
            this.smtSearchButton2.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton2.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton2.Location = new System.Drawing.Point(1397, 0);
            this.smtSearchButton2.Name = "smtSearchButton2";
            this.smtSearchButton2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton2.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton2.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton2.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.smtSearchButton2.TabIndex = 5;
            this.smtSearchButton2.Text = "닫기";
            this.smtSearchButton2.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcCustomer);
            this.layoutControl1.Controls.Add(this.gcPlacingList);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1474, 812);
            this.layoutControl1.TabIndex = 5;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcCustomer
            // 
            this.gcCustomer.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gcCustomer.Location = new System.Drawing.Point(19, 46);
            this.gcCustomer.MainView = this.gvCustomer;
            this.gcCustomer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcCustomer.Name = "gcCustomer";
            this.gcCustomer.Size = new System.Drawing.Size(100, 747);
            this.gcCustomer.TabIndex = 38;
            this.gcCustomer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCustomer});
            // 
            // gvCustomer
            // 
            this.gvCustomer.ColumnPanelRowHeight = 25;
            this.gvCustomer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn32,
            this.gridColumn3});
            this.gvCustomer.DetailHeight = 425;
            this.gvCustomer.GridControl = this.gcCustomer;
            this.gvCustomer.Name = "gvCustomer";
            this.gvCustomer.OptionsView.ShowGroupPanel = false;
            this.gvCustomer.OptionsView.ShowIndicator = false;
            this.gvCustomer.RowHeight = 25;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "거래처";
            this.gridColumn32.FieldName = "CustName";
            this.gridColumn32.MinWidth = 23;
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.OptionsColumn.AllowFocus = false;
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 0;
            this.gridColumn32.Width = 1438;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "거래처코드";
            this.gridColumn3.FieldName = "CustCode";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowFocus = false;
            this.gridColumn3.Width = 367;
            // 
            // gcPlacingList
            // 
            this.gcPlacingList.Location = new System.Drawing.Point(145, 7);
            this.gcPlacingList.MainView = this.gvPlacingList;
            this.gcPlacingList.Name = "gcPlacingList";
            this.gcPlacingList.Size = new System.Drawing.Size(1322, 798);
            this.gcPlacingList.TabIndex = 39;
            this.gcPlacingList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPlacingList});
            // 
            // gvPlacingList
            // 
            this.gvPlacingList.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvPlacingList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvPlacingList.GridControl = this.gcPlacingList;
            this.gvPlacingList.Name = "gvPlacingList";
            this.gvPlacingList.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup7,
            this.layoutControlGroup2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1474, 812);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.CustomizationFormText = "거래처 리스트";
            this.layoutControlGroup7.ExpandButtonVisible = true;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup7.Size = new System.Drawing.Size(138, 812);
            this.layoutControlGroup7.Text = "거래처 리스트";
            this.layoutControlGroup7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.gcCustomer;
            this.layoutControlItem11.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(106, 753);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "발주현황";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(138, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1336, 812);
            this.layoutControlGroup2.Text = "발주현황";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcPlacingList;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1328, 804);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Frm_MM_Placing_MD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 852);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_MM_Placing_MD";
            this.Text = "발주 현황";
            this.Load += new System.EventHandler(this.frmBomProductList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPlacingList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPlacingList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdDelete;
        private baseSMT.smtSearchButton smtSearchButton2;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchLookUp Lup_State;
        private baseSMT.smtSearchLookUp Lup_PlacingType;
        private baseSMT.smtSearchLookUp Lup_Cust;
        private baseSMT.smtSearchButton btn_Search;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.GridControl gcPlacingList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPlacingList;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}