namespace SmartMES.View.BM
{
    partial class Frm_BM_SdOrder_1Grid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BM_SdOrder_1Grid));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.GcBM_SdOrderD = new DevExpress.XtraGrid.GridControl();
            this.GvBM_SdOrderD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GvPrepPlan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Ins = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.btn_Del = new baseSMT.smtSearchButton();
            this.btn_Confirm = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Cust = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.btn_Confirm_Cancel = new baseSMT.smtSearchButton();
            this.btn_Excel = new baseSMT.smtSearchButton();
            this.btn_Close = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcBM_SdOrderD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvBM_SdOrderD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPrepPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.GcBM_SdOrderD);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(3190, 555, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1399, 353);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // GcBM_SdOrderD
            // 
            this.GcBM_SdOrderD.Cursor = System.Windows.Forms.Cursors.Default;
            this.GcBM_SdOrderD.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.GcBM_SdOrderD.Location = new System.Drawing.Point(7, 7);
            this.GcBM_SdOrderD.MainView = this.GvBM_SdOrderD;
            this.GcBM_SdOrderD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GcBM_SdOrderD.Name = "GcBM_SdOrderD";
            this.GcBM_SdOrderD.Size = new System.Drawing.Size(1385, 339);
            this.GcBM_SdOrderD.TabIndex = 50;
            this.GcBM_SdOrderD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvBM_SdOrderD,
            this.GvPrepPlan});
            // 
            // GvBM_SdOrderD
            // 
            this.GvBM_SdOrderD.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GvBM_SdOrderD.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GvBM_SdOrderD.ColumnPanelRowHeight = 29;
            this.GvBM_SdOrderD.DetailHeight = 425;
            this.GvBM_SdOrderD.GridControl = this.GcBM_SdOrderD;
            this.GvBM_SdOrderD.Name = "GvBM_SdOrderD";
            this.GvBM_SdOrderD.OptionsBehavior.Editable = false;
            this.GvBM_SdOrderD.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            this.GvBM_SdOrderD.OptionsView.ColumnAutoWidth = false;
            this.GvBM_SdOrderD.OptionsView.ShowGroupPanel = false;
            this.GvBM_SdOrderD.RowHeight = 29;
            this.GvBM_SdOrderD.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.GvBM_SdOrderD_CustomDrawCell);
            // 
            // GvPrepPlan
            // 
            this.GvPrepPlan.ColumnPanelRowHeight = 35;
            this.GvPrepPlan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19});
            this.GvPrepPlan.DetailHeight = 425;
            this.GvPrepPlan.GridControl = this.GcBM_SdOrderD;
            this.GvPrepPlan.Name = "GvPrepPlan";
            this.GvPrepPlan.OptionsBehavior.Editable = false;
            this.GvPrepPlan.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            this.GvPrepPlan.OptionsView.ColumnAutoWidth = false;
            this.GvPrepPlan.OptionsView.ShowGroupPanel = false;
            this.GvPrepPlan.RowHeight = 35;
            this.GvPrepPlan.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn14, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "주문번호";
            this.gridColumn3.FieldName = "SdOrderNo";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 86;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "순번";
            this.gridColumn4.FieldName = "SdOrderSeq";
            this.gridColumn4.MinWidth = 23;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.FixedWidth = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 48;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "품번";
            this.gridColumn5.FieldName = "ItemCode";
            this.gridColumn5.MinWidth = 23;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 128;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "품명";
            this.gridColumn6.FieldName = "ItemName";
            this.gridColumn6.MinWidth = 23;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            this.gridColumn6.Width = 192;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "규격";
            this.gridColumn7.FieldName = "Size";
            this.gridColumn7.MinWidth = 23;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            this.gridColumn7.Width = 171;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "수량";
            this.gridColumn8.DisplayFormat.FormatString = "#,0.####";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "Qty";
            this.gridColumn8.MinWidth = 23;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 93;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "단위";
            this.gridColumn9.FieldName = "UnitName";
            this.gridColumn9.MinWidth = 23;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 57;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "납기일";
            this.gridColumn10.FieldName = "DeliveryDate";
            this.gridColumn10.MinWidth = 23;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Width = 97;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "파레트";
            this.gridColumn11.FieldName = "PaletteYn";
            this.gridColumn11.MinWidth = 23;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Width = 69;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "상태";
            this.gridColumn12.FieldName = "State";
            this.gridColumn12.MinWidth = 23;
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 6;
            this.gridColumn12.Width = 57;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "단가";
            this.gridColumn13.DisplayFormat.FormatString = "#,0.####";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "UnitPrice";
            this.gridColumn13.MinWidth = 23;
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Width = 103;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "공급가";
            this.gridColumn14.DisplayFormat.FormatString = "#,0.####";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn14.FieldName = "Price";
            this.gridColumn14.MinWidth = 23;
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.Width = 131;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "부가세";
            this.gridColumn15.DisplayFormat.FormatString = "#,0.####";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "VatPrice";
            this.gridColumn15.MinWidth = 23;
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowFocus = false;
            this.gridColumn15.Width = 127;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "합계";
            this.gridColumn16.DisplayFormat.FormatString = "#,0.####";
            this.gridColumn16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn16.FieldName = "TotalPrice";
            this.gridColumn16.MinWidth = 23;
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowFocus = false;
            this.gridColumn16.Width = 139;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "비고";
            this.gridColumn17.FieldName = "Remark";
            this.gridColumn17.MinWidth = 23;
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 7;
            this.gridColumn17.Width = 229;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "작업계획";
            this.gridColumn18.FieldName = "OrderStatus";
            this.gridColumn18.MinWidth = 19;
            this.gridColumn18.Name = "gridColumn18";
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "발주상태";
            this.gridColumn19.FieldName = "PlacingStatus";
            this.gridColumn19.MinWidth = 19;
            this.gridColumn19.Name = "gridColumn19";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1399, 353);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "주문등록 세부내용";
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
            this.layoutControlGroup3.Size = new System.Drawing.Size(1399, 353);
            this.layoutControlGroup3.Text = "주문 등록 세부내용";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.GcBM_SdOrderD;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem2.OptionsPrint.AppearanceItem.Options.UseFont = true;
            this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
            this.layoutControlItem2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
            this.layoutControlItem2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
            this.layoutControlItem2.Size = new System.Drawing.Size(1391, 345);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Ins);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.btn_Del);
            this.smtCommonBar1.Controls.Add(this.btn_Confirm);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Cust);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.btn_Confirm_Cancel);
            this.smtCommonBar1.Controls.Add(this.btn_Excel);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1399, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "주문등록";
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
            this.btn_Ins.Location = new System.Drawing.Point(812, 0);
            this.btn_Ins.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Ins.Name = "btn_Ins";
            this.btn_Ins.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Ins.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Ins.Size = new System.Drawing.Size(77, 40);
            this.btn_Ins.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.btn_Ins.TabIndex = 11;
            this.btn_Ins.Tag = "Ins";
            this.btn_Ins.Text = "등록";
            this.btn_Ins.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdSave.Location = new System.Drawing.Point(889, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 12;
            this.cmdSave.Text = "저장";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
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
            this.btn_Del.Location = new System.Drawing.Point(966, 0);
            this.btn_Del.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Del.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Del.Size = new System.Drawing.Size(77, 40);
            this.btn_Del.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.btn_Del.TabIndex = 3;
            this.btn_Del.Tag = "Del";
            this.btn_Del.Text = "삭제";
            this.btn_Del.Click += new System.EventHandler(this.Btn_RowEdit);
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.AllowFocus = false;
            this.btn_Confirm.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Confirm.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Confirm.Appearance.Options.UseBackColor = true;
            this.btn_Confirm.Appearance.Options.UseForeColor = true;
            this.btn_Confirm.AutoSize = true;
            this.btn_Confirm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Confirm.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Confirm.ImageOptions.ImageIndex = 6;
            this.btn_Confirm.ImageOptions.ImageToTextIndent = 15;
            this.btn_Confirm.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Confirm.Location = new System.Drawing.Point(1043, 0);
            this.btn_Confirm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Confirm.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Confirm.Size = new System.Drawing.Size(77, 40);
            this.btn_Confirm.smtButtonStyle = baseSMT.EnumButtonStyles.확정;
            this.btn_Confirm.TabIndex = 10;
            this.btn_Confirm.Text = "확정";
            this.btn_Confirm.Visible = false;
            this.btn_Confirm.Click += new System.EventHandler(this.btn_Confirm_Click);
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
            this.btn_Search.Location = new System.Drawing.Point(725, 0);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 6;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // Lup_Cust
            // 
            this.Lup_Cust.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Cust.Location = new System.Drawing.Point(437, 0);
            this.Lup_Cust.LookAndFeel.SkinName = "WXI";
            this.Lup_Cust.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Cust.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lup_Cust.Name = "Lup_Cust";
            this.Lup_Cust.Size = new System.Drawing.Size(288, 40);
            this.Lup_Cust.smtLabelForeColor = System.Drawing.Color.White;
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
            this.Lup_Cust.TabIndex = 8;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(92, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.Dt_Date.smtLabelForeColor = System.Drawing.Color.White;
            this.Dt_Date.smtLabelTitleValue = "조회기간";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 7;
            // 
            // btn_Confirm_Cancel
            // 
            this.btn_Confirm_Cancel.AllowFocus = false;
            this.btn_Confirm_Cancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Confirm_Cancel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Confirm_Cancel.Appearance.Options.UseBackColor = true;
            this.btn_Confirm_Cancel.Appearance.Options.UseForeColor = true;
            this.btn_Confirm_Cancel.AutoSize = true;
            this.btn_Confirm_Cancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Confirm_Cancel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Confirm_Cancel.ImageOptions.ImageIndex = 7;
            this.btn_Confirm_Cancel.ImageOptions.ImageToTextIndent = 15;
            this.btn_Confirm_Cancel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Confirm_Cancel.Location = new System.Drawing.Point(1120, 0);
            this.btn_Confirm_Cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Confirm_Cancel.Name = "btn_Confirm_Cancel";
            this.btn_Confirm_Cancel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Confirm_Cancel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Confirm_Cancel.Size = new System.Drawing.Size(101, 40);
            this.btn_Confirm_Cancel.smtButtonStyle = baseSMT.EnumButtonStyles.확정취소;
            this.btn_Confirm_Cancel.TabIndex = 4;
            this.btn_Confirm_Cancel.Text = "확정취소";
            this.btn_Confirm_Cancel.Visible = false;
            this.btn_Confirm_Cancel.Click += new System.EventHandler(this.btn_Confirm_Cancel_Click);
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
            this.btn_Excel.Location = new System.Drawing.Point(1221, 0);
            this.btn_Excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.btn_Close.Location = new System.Drawing.Point(1322, 0);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(77, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Frm_BM_SdOrder_1Grid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1399, 393);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_BM_SdOrder_1Grid";
            this.Text = "주문등록";
            this.Load += new System.EventHandler(this.frmObtain_HS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcBM_SdOrderD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvBM_SdOrderD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPrepPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl GcBM_SdOrderD;
        private DevExpress.XtraGrid.Views.Grid.GridView GvBM_SdOrderD;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Views.Grid.GridView GvPrepPlan;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchLookUp Lup_Cust;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton btn_Confirm_Cancel;
        private baseSMT.smtSearchButton btn_Del;
        private baseSMT.smtSearchButton btn_Excel;
        private baseSMT.smtSearchButton btn_Close;
        private baseSMT.smtSearchButton btn_Ins;
        private baseSMT.smtSearchButton btn_Confirm;
        private baseSMT.smtSearchButton cmdSave;
    }
}