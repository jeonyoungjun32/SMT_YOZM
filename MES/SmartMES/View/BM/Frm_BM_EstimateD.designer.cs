namespace SmartMES.View.BM
{
    partial class Frm_BM_EstimateD
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BM_EstimateD));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.smtCommonBar2 = new baseSMT.smtCommonBar();
			this.cmdApplyPrice = new baseSMT.smtSearchButton();
			this.txtAdjustPrice = new baseSMT.smtSearchTextBox();
			this.Lup_UnitPrice = new baseSMT.smtSearchLookUp();
			this.cmdAdjustPrice = new baseSMT.smtSearchButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.VatRate = new baseSMT.smtBodyTextBox();
			this.searchVAT = new baseSMT.smtBodyLookUp();
			this.smtTotalPrice = new baseSMT.smtBodyTextBox();
			this.EstimateName = new baseSMT.smtBodyTextBox();
			this.remarkTextEdit = new baseSMT.smtBodyMemoEdit();
			this.LupSalesMan = new baseSMT.smtBodyLookUp();
			this.LupCustomer = new baseSMT.smtBodyLookUp();
			this.Dt_Date = new baseSMT.smtBodyDatetime();
			this.obtainOrderNoTextEdit = new baseSMT.smtBodyTextBox();
			this.gcDetail = new DevExpress.XtraGrid.GridControl();
			this.gvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.btnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.btnCancel = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.priceSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.paletteYnComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.UnitCodeLookUp = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.searchItemType = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
			this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.searchUnitName = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.searchDeliveryTo2 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
			this.gridView10 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdDelete = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).BeginInit();
			this.smtCommonBar2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.priceSpinEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.paletteYnComboBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UnitCodeLookUp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchItemType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchUnitName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchDeliveryTo2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.smtCommonBar2);
			this.layoutControl1.Controls.Add(this.panel1);
			this.layoutControl1.Controls.Add(this.gcDetail);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1270, 296, 650, 400);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1136, 634);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// smtCommonBar2
			// 
			this.smtCommonBar2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar2.Appearance.Options.UseBackColor = true;
			this.smtCommonBar2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar2.Controls.Add(this.cmdApplyPrice);
			this.smtCommonBar2.Controls.Add(this.txtAdjustPrice);
			this.smtCommonBar2.Controls.Add(this.Lup_UnitPrice);
			this.smtCommonBar2.Controls.Add(this.cmdAdjustPrice);
			this.smtCommonBar2.Location = new System.Drawing.Point(3, 226);
			this.smtCommonBar2.Name = "smtCommonBar2";
			this.smtCommonBar2.Size = new System.Drawing.Size(1130, 40);
			this.smtCommonBar2.smtImageTitleSvgImage = null;
			this.smtCommonBar2.smtImageTitleText = "layoutControl1";
			this.smtCommonBar2.smtImageTitleVisible = false;
			this.smtCommonBar2.TabIndex = 179;
			// 
			// cmdApplyPrice
			// 
			this.cmdApplyPrice.AllowFocus = false;
			this.cmdApplyPrice.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdApplyPrice.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdApplyPrice.Appearance.Options.UseBackColor = true;
			this.cmdApplyPrice.Appearance.Options.UseForeColor = true;
			this.cmdApplyPrice.AutoSize = true;
			this.cmdApplyPrice.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdApplyPrice.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdApplyPrice.ImageOptions.ImageIndex = 6;
			this.cmdApplyPrice.ImageOptions.ImageToTextIndent = 15;
			this.cmdApplyPrice.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdApplyPrice.Location = new System.Drawing.Point(723, 0);
			this.cmdApplyPrice.Name = "cmdApplyPrice";
			this.cmdApplyPrice.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdApplyPrice.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdApplyPrice.Size = new System.Drawing.Size(101, 40);
			this.cmdApplyPrice.smtButtonStyle = baseSMT.EnumButtonStyles.확정;
			this.cmdApplyPrice.TabIndex = 33;
			this.cmdApplyPrice.Text = "단가적용";
			this.cmdApplyPrice.Click += new System.EventHandler(this.Btn_Click);
			// 
			// txtAdjustPrice
			// 
			this.txtAdjustPrice.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtAdjustPrice.Location = new System.Drawing.Point(824, 0);
			this.txtAdjustPrice.LookAndFeel.SkinName = "WXI";
			this.txtAdjustPrice.LookAndFeel.UseDefaultLookAndFeel = false;
			this.txtAdjustPrice.Name = "txtAdjustPrice";
			this.txtAdjustPrice.Size = new System.Drawing.Size(133, 40);
			this.txtAdjustPrice.smtLabelTitleValue = "";
			this.txtAdjustPrice.smtLabelTitleVisible = false;
			this.txtAdjustPrice.smtLabelTitleWidth = 100;
			this.txtAdjustPrice.smtTextBoxDecimal = 0;
			this.txtAdjustPrice.smtTextBoxMust = false;
			this.txtAdjustPrice.smtTextBoxNumber = false;
			this.txtAdjustPrice.smtTextBoxReadOnly = false;
			this.txtAdjustPrice.smtTextBoxValue = "";
			this.txtAdjustPrice.TabIndex = 32;
			// 
			// Lup_UnitPrice
			// 
			this.Lup_UnitPrice.Dock = System.Windows.Forms.DockStyle.Right;
			this.Lup_UnitPrice.Location = new System.Drawing.Point(957, 0);
			this.Lup_UnitPrice.LookAndFeel.SkinName = "WXI";
			this.Lup_UnitPrice.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Lup_UnitPrice.Name = "Lup_UnitPrice";
			this.Lup_UnitPrice.Size = new System.Drawing.Size(72, 40);
			this.Lup_UnitPrice.smtLabelTitleValue = "";
			this.Lup_UnitPrice.smtLabelTitleVisible = true;
			this.Lup_UnitPrice.smtLabelTitleWidth = 10;
			this.Lup_UnitPrice.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.Lup_UnitPrice.smtLookUpDataDisplayMember = "코드명";
			this.Lup_UnitPrice.smtLookUpDataSource = null;
			this.Lup_UnitPrice.smtLookUpDataValueMember = "코드";
			this.Lup_UnitPrice.smtLookUpSelectdIndex = -1;
			this.Lup_UnitPrice.smtLookUpText = "";
			this.Lup_UnitPrice.smtLookUpValue = "";
			this.Lup_UnitPrice.TabIndex = 14;
			// 
			// cmdAdjustPrice
			// 
			this.cmdAdjustPrice.AllowFocus = false;
			this.cmdAdjustPrice.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdAdjustPrice.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdAdjustPrice.Appearance.Options.UseBackColor = true;
			this.cmdAdjustPrice.Appearance.Options.UseForeColor = true;
			this.cmdAdjustPrice.AutoSize = true;
			this.cmdAdjustPrice.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdAdjustPrice.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdAdjustPrice.ImageOptions.ImageIndex = 6;
			this.cmdAdjustPrice.ImageOptions.ImageToTextIndent = 15;
			this.cmdAdjustPrice.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdAdjustPrice.Location = new System.Drawing.Point(1029, 0);
			this.cmdAdjustPrice.Name = "cmdAdjustPrice";
			this.cmdAdjustPrice.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdAdjustPrice.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdAdjustPrice.Size = new System.Drawing.Size(101, 40);
			this.cmdAdjustPrice.smtButtonStyle = baseSMT.EnumButtonStyles.확정;
			this.cmdAdjustPrice.TabIndex = 11;
			this.cmdAdjustPrice.Text = "단가조정";
			this.cmdAdjustPrice.Click += new System.EventHandler(this.Btn_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.VatRate);
			this.panel1.Controls.Add(this.searchVAT);
			this.panel1.Controls.Add(this.smtTotalPrice);
			this.panel1.Controls.Add(this.EstimateName);
			this.panel1.Controls.Add(this.remarkTextEdit);
			this.panel1.Controls.Add(this.LupSalesMan);
			this.panel1.Controls.Add(this.LupCustomer);
			this.panel1.Controls.Add(this.Dt_Date);
			this.panel1.Controls.Add(this.obtainOrderNoTextEdit);
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1130, 217);
			this.panel1.TabIndex = 87;
			// 
			// VatRate
			// 
			this.VatRate.Location = new System.Drawing.Point(9, 92);
			this.VatRate.LookAndFeel.SkinName = "WXI";
			this.VatRate.LookAndFeel.UseDefaultLookAndFeel = false;
			this.VatRate.Name = "VatRate";
			this.VatRate.Size = new System.Drawing.Size(582, 32);
			this.VatRate.smtLabelTitleValue = "부가세율";
			this.VatRate.smtLabelTitleVisible = true;
			this.VatRate.smtLabelTitleWidth = 100;
			this.VatRate.smtTextBoxDecimal = 0;
			this.VatRate.smtTextBoxMust = false;
			this.VatRate.smtTextBoxNumber = false;
			this.VatRate.smtTextBoxReadOnly = true;
			this.VatRate.smtTextBoxValue = "";
			this.VatRate.TabIndex = 126;
			// 
			// searchVAT
			// 
			this.searchVAT.Location = new System.Drawing.Point(597, 91);
			this.searchVAT.LookAndFeel.SkinName = "WXI";
			this.searchVAT.LookAndFeel.UseDefaultLookAndFeel = false;
			this.searchVAT.Name = "searchVAT";
			this.searchVAT.Size = new System.Drawing.Size(524, 32);
			this.searchVAT.smtLabelTitleValue = "부가세 구분";
			this.searchVAT.smtLabelTitleVisible = true;
			this.searchVAT.smtLabelTitleWidth = 100;
			this.searchVAT.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.searchVAT.smtLookUpDataDisplayMember = "";
			this.searchVAT.smtLookUpDataSource = null;
			this.searchVAT.smtLookUpDataValueMember = "";
			this.searchVAT.smtLookUpMust = true;
			this.searchVAT.smtLookUpReadOnly = false;
			this.searchVAT.smtLookUpSelectdIndex = -1;
			this.searchVAT.smtLookUpText = "";
			this.searchVAT.smtLookUpValue = "";
			this.searchVAT.TabIndex = 125;
			this.searchVAT.smtLookUpEditValueChanged += new System.EventHandler(this.searchVAT_smtLookUpEditValueChanged);
			// 
			// smtTotalPrice
			// 
			this.smtTotalPrice.Location = new System.Drawing.Point(9, 127);
			this.smtTotalPrice.Name = "smtTotalPrice";
			this.smtTotalPrice.Size = new System.Drawing.Size(1112, 32);
			this.smtTotalPrice.smtLabelTitleValue = "총금액";
			this.smtTotalPrice.smtLabelTitleVisible = true;
			this.smtTotalPrice.smtLabelTitleWidth = 100;
			this.smtTotalPrice.smtTextBoxDecimal = 0;
			this.smtTotalPrice.smtTextBoxMust = false;
			this.smtTotalPrice.smtTextBoxNumber = true;
			this.smtTotalPrice.smtTextBoxReadOnly = true;
			this.smtTotalPrice.smtTextBoxValue = "";
			this.smtTotalPrice.TabIndex = 123;
			// 
			// EstimateName
			// 
			this.EstimateName.Location = new System.Drawing.Point(597, 15);
			this.EstimateName.LookAndFeel.SkinName = "WXI";
			this.EstimateName.LookAndFeel.UseDefaultLookAndFeel = false;
			this.EstimateName.Name = "EstimateName";
			this.EstimateName.Size = new System.Drawing.Size(524, 32);
			this.EstimateName.smtLabelTitleValue = "견적명";
			this.EstimateName.smtLabelTitleVisible = true;
			this.EstimateName.smtLabelTitleWidth = 100;
			this.EstimateName.smtTextBoxDecimal = 0;
			this.EstimateName.smtTextBoxMust = true;
			this.EstimateName.smtTextBoxNumber = false;
			this.EstimateName.smtTextBoxReadOnly = false;
			this.EstimateName.smtTextBoxValue = "";
			this.EstimateName.TabIndex = 9;
			// 
			// remarkTextEdit
			// 
			this.remarkTextEdit.Location = new System.Drawing.Point(9, 165);
			this.remarkTextEdit.LookAndFeel.SkinName = "WXI";
			this.remarkTextEdit.LookAndFeel.UseDefaultLookAndFeel = false;
			this.remarkTextEdit.Name = "remarkTextEdit";
			this.remarkTextEdit.Size = new System.Drawing.Size(1112, 49);
			this.remarkTextEdit.smtLabelTitleValue = "비고";
			this.remarkTextEdit.smtLabelTitleVisible = true;
			this.remarkTextEdit.smtLabelTitleWidth = 100;
			this.remarkTextEdit.smtMemoMust = false;
			this.remarkTextEdit.smtMemoReadOnly = false;
			this.remarkTextEdit.smtMemoValue = "";
			this.remarkTextEdit.TabIndex = 8;
			// 
			// LupSalesMan
			// 
			this.LupSalesMan.Location = new System.Drawing.Point(597, 53);
			this.LupSalesMan.LookAndFeel.SkinName = "WXI";
			this.LupSalesMan.LookAndFeel.UseDefaultLookAndFeel = false;
			this.LupSalesMan.Name = "LupSalesMan";
			this.LupSalesMan.Size = new System.Drawing.Size(524, 32);
			this.LupSalesMan.smtLabelTitleValue = "담당자";
			this.LupSalesMan.smtLabelTitleVisible = true;
			this.LupSalesMan.smtLabelTitleWidth = 100;
			this.LupSalesMan.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.LupSalesMan.smtLookUpDataDisplayMember = "";
			this.LupSalesMan.smtLookUpDataSource = null;
			this.LupSalesMan.smtLookUpDataValueMember = "";
			this.LupSalesMan.smtLookUpMust = true;
			this.LupSalesMan.smtLookUpReadOnly = false;
			this.LupSalesMan.smtLookUpSelectdIndex = -1;
			this.LupSalesMan.smtLookUpText = "";
			this.LupSalesMan.smtLookUpValue = "";
			this.LupSalesMan.TabIndex = 4;
			// 
			// LupCustomer
			// 
			this.LupCustomer.Location = new System.Drawing.Point(9, 53);
			this.LupCustomer.LookAndFeel.SkinName = "WXI";
			this.LupCustomer.LookAndFeel.UseDefaultLookAndFeel = false;
			this.LupCustomer.Name = "LupCustomer";
			this.LupCustomer.Size = new System.Drawing.Size(582, 32);
			this.LupCustomer.smtLabelTitleValue = "거래처";
			this.LupCustomer.smtLabelTitleVisible = true;
			this.LupCustomer.smtLabelTitleWidth = 100;
			this.LupCustomer.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.LupCustomer.smtLookUpDataDisplayMember = "";
			this.LupCustomer.smtLookUpDataSource = null;
			this.LupCustomer.smtLookUpDataValueMember = "";
			this.LupCustomer.smtLookUpMust = true;
			this.LupCustomer.smtLookUpReadOnly = false;
			this.LupCustomer.smtLookUpSelectdIndex = -1;
			this.LupCustomer.smtLookUpText = "";
			this.LupCustomer.smtLookUpValue = "";
			this.LupCustomer.TabIndex = 3;
			// 
			// Dt_Date
			// 
			this.Dt_Date.Location = new System.Drawing.Point(303, 15);
			this.Dt_Date.LookAndFeel.SkinName = "WXI";
			this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Dt_Date.Name = "Dt_Date";
			this.Dt_Date.Size = new System.Drawing.Size(288, 32);
			this.Dt_Date.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.Dt_Date.smtDateTimeMust = true;
			this.Dt_Date.smtDateTimeReadOnly = false;
			this.Dt_Date.smtDateTimeValue = new System.DateTime(((long)(0)));
			this.Dt_Date.smtLabelTitleValue = "견적일자";
			this.Dt_Date.smtLabelTitleVisible = true;
			this.Dt_Date.smtLabelTitleWidth = 100;
			this.Dt_Date.TabIndex = 1;
			// 
			// obtainOrderNoTextEdit
			// 
			this.obtainOrderNoTextEdit.Location = new System.Drawing.Point(9, 15);
			this.obtainOrderNoTextEdit.LookAndFeel.SkinName = "WXI";
			this.obtainOrderNoTextEdit.LookAndFeel.UseDefaultLookAndFeel = false;
			this.obtainOrderNoTextEdit.Name = "obtainOrderNoTextEdit";
			this.obtainOrderNoTextEdit.Size = new System.Drawing.Size(288, 32);
			this.obtainOrderNoTextEdit.smtLabelTitleValue = "견적번호";
			this.obtainOrderNoTextEdit.smtLabelTitleVisible = true;
			this.obtainOrderNoTextEdit.smtLabelTitleWidth = 100;
			this.obtainOrderNoTextEdit.smtTextBoxDecimal = 0;
			this.obtainOrderNoTextEdit.smtTextBoxMust = false;
			this.obtainOrderNoTextEdit.smtTextBoxNumber = false;
			this.obtainOrderNoTextEdit.smtTextBoxReadOnly = true;
			this.obtainOrderNoTextEdit.smtTextBoxValue = "";
			this.obtainOrderNoTextEdit.TabIndex = 0;
			// 
			// gcDetail
			// 
			this.gcDetail.Cursor = System.Windows.Forms.Cursors.Default;
			this.gcDetail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
			this.gcDetail.Location = new System.Drawing.Point(7, 264);
			this.gcDetail.MainView = this.gvDetail;
			this.gcDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.gcDetail.Name = "gcDetail";
			this.gcDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnDelete,
            this.btnCancel,
            this.priceSpinEdit,
            this.paletteYnComboBox,
            this.UnitCodeLookUp});
			this.gcDetail.Size = new System.Drawing.Size(1122, 363);
			this.gcDetail.TabIndex = 49;
			this.gcDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetail});
			// 
			// gvDetail
			// 
			this.gvDetail.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gvDetail.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvDetail.ColumnPanelRowHeight = 35;
			this.gvDetail.DetailHeight = 496;
			this.gvDetail.FixedLineWidth = 4;
			this.gvDetail.GridControl = this.gcDetail;
			this.gvDetail.Name = "gvDetail";
			this.gvDetail.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
			this.gvDetail.OptionsCustomization.AllowSort = false;
			this.gvDetail.OptionsView.ColumnAutoWidth = false;
			this.gvDetail.OptionsView.ShowFooter = true;
			this.gvDetail.OptionsView.ShowGroupPanel = false;
			this.gvDetail.OptionsView.ShowIndicator = false;
			this.gvDetail.RowHeight = 35;
			// 
			// btnDelete
			// 
			this.btnDelete.AutoHeight = false;
			this.btnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
			this.btnDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// btnCancel
			// 
			this.btnCancel.AutoHeight = false;
			this.btnCancel.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Undo)});
			this.btnCancel.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// priceSpinEdit
			// 
			this.priceSpinEdit.AutoHeight = false;
			this.priceSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.priceSpinEdit.DisplayFormat.FormatString = "#,0";
			this.priceSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.priceSpinEdit.EditFormat.FormatString = "#,0";
			this.priceSpinEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.priceSpinEdit.Name = "priceSpinEdit";
			// 
			// paletteYnComboBox
			// 
			this.paletteYnComboBox.AutoHeight = false;
			this.paletteYnComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.paletteYnComboBox.Items.AddRange(new object[] {
            "Y",
            "N"});
			this.paletteYnComboBox.Name = "paletteYnComboBox";
			// 
			// UnitCodeLookUp
			// 
			this.UnitCodeLookUp.AutoHeight = false;
			this.UnitCodeLookUp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.UnitCodeLookUp.Name = "UnitCodeLookUp";
			this.UnitCodeLookUp.NullText = "";
			this.UnitCodeLookUp.PopupView = this.gridView2;
			// 
			// gridView2
			// 
			this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlItem3,
            this.layoutControlItem1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1136, 634);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.CustomizationFormText = "세부내역";
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup3.Location = new System.Drawing.Point(0, 230);
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
			this.layoutControlGroup3.Size = new System.Drawing.Size(1136, 404);
			this.layoutControlGroup3.Text = "세부내역";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.gcDetail;
			this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlItem2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlItem2.Size = new System.Drawing.Size(1128, 369);
			this.layoutControlItem2.Text = "layoutControlItem1";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.panel1;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(1136, 223);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.smtCommonBar2;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 223);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1136, 7);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// searchItemType
			// 
			this.searchItemType.AutoHeight = false;
			this.searchItemType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.searchItemType.Name = "searchItemType";
			this.searchItemType.NullText = "";
			this.searchItemType.PopupView = this.repositoryItemSearchLookUpEdit1View;
			// 
			// repositoryItemSearchLookUpEdit1View
			// 
			this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
			this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// searchUnitName
			// 
			this.searchUnitName.AutoHeight = false;
			this.searchUnitName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.searchUnitName.Name = "searchUnitName";
			this.searchUnitName.NullText = "";
			this.searchUnitName.PopupView = this.gridView1;
			// 
			// gridView1
			// 
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// searchDeliveryTo2
			// 
			this.searchDeliveryTo2.AutoHeight = false;
			this.searchDeliveryTo2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.searchDeliveryTo2.Name = "searchDeliveryTo2";
			this.searchDeliveryTo2.NullText = "";
			this.searchDeliveryTo2.PopupView = this.gridView10;
			// 
			// gridView10
			// 
			this.gridView10.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView10.Name = "gridView10";
			this.gridView10.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView10.OptionsView.ShowGroupPanel = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdDelete);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1136, 40);
			this.smtCommonBar1.smtImageTitleAutoText = false;
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "견적 정보";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 1;
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
			this.cmdSave.Location = new System.Drawing.Point(905, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 2;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdDelete.Location = new System.Drawing.Point(982, 0);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDelete.Size = new System.Drawing.Size(77, 40);
			this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDelete.TabIndex = 3;
			this.cmdDelete.Text = "삭제";
			this.cmdDelete.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(1059, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.Btn_Click);
			// 
			// Frm_BM_EstimateD
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1136, 674);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.IconOptions.Image = global::SmartMES.Properties.Resources.Person_24px;
			this.LookAndFeel.SkinName = "WXI";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "Frm_BM_EstimateD";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmRegisterObtainOrderDetail_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).EndInit();
			this.smtCommonBar2.ResumeLayout(false);
			this.smtCommonBar2.PerformLayout();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.priceSpinEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.paletteYnComboBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UnitCodeLookUp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchItemType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchUnitName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchDeliveryTo2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetail;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit searchItemType;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit searchUnitName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit searchDeliveryTo2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView10;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnCancel;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit priceSpinEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox paletteYnComboBox;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdSave;
        private baseSMT.smtSearchButton cmdClose;
        private System.Windows.Forms.Panel panel1;
        private baseSMT.smtBodyDatetime Dt_Date;
        private baseSMT.smtBodyTextBox obtainOrderNoTextEdit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private baseSMT.smtBodyLookUp LupSalesMan;
        private baseSMT.smtBodyLookUp LupCustomer;
        private baseSMT.smtBodyMemoEdit remarkTextEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit UnitCodeLookUp;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private baseSMT.smtBodyTextBox EstimateName;
        private baseSMT.smtSearchButton cmdDelete;
        private baseSMT.smtBodyTextBox smtTotalPrice;
        private baseSMT.smtBodyLookUp searchVAT;
        private baseSMT.smtBodyTextBox VatRate;
        private baseSMT.smtCommonBar smtCommonBar2;
        private baseSMT.smtSearchButton cmdAdjustPrice;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchLookUp Lup_UnitPrice;
        private baseSMT.smtSearchTextBox txtAdjustPrice;
        private baseSMT.smtSearchButton cmdApplyPrice;
		private DevExpress.Utils.ToolTipController toolTipController1;
	}
}