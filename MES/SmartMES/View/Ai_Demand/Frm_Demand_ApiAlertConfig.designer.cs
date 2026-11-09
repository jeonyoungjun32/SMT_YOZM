namespace SmartMES.View.Ai_Demand
{
    partial class Frm_Demand_ApiAlertConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Demand_ApiAlertConfig));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcAlert = new DevExpress.XtraGrid.GridControl();
            this.gvAlert = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colCorpId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustCode1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipToCode1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipToName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCorpId2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustCode2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipToCode2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipToName2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTradeType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVatType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBillToNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeliLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colArea = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipToCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManagerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUseYn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btnSearch = new baseSMT.smtSearchButton();
            this.lupUseYn = new baseSMT.smtSearchLookUp();
            this.lupAlertType = new baseSMT.smtSearchLookUp();
            this.btnAdd = new baseSMT.smtSearchButton();
            this.btnSave = new baseSMT.smtSearchButton();
            this.btnDelete = new baseSMT.smtSearchButton();
            this.btnClose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcAlert);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlGroup2});
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(3148, 404, 650, 400);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1339, 458);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcAlert
            // 
            this.gcAlert.Location = new System.Drawing.Point(7, 34);
            this.gcAlert.MainView = this.gvAlert;
            this.gcAlert.Name = "gcAlert";
            this.gcAlert.Size = new System.Drawing.Size(1325, 417);
            this.gcAlert.TabIndex = 50;
            this.gcAlert.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAlert});
            // 
            // gvAlert
            // 
            this.gvAlert.GridControl = this.gcAlert;
            this.gvAlert.Name = "gvAlert";
            this.gvAlert.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(292, 599);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 245);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(316, 644);
            this.layoutControlGroup2.Text = "납품처 리스트";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1339, 458);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1339, 458);
            this.layoutControlGroup3.Text = "이상변동 알림리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAlert;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1331, 423);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // colCorpId1
            // 
            this.colCorpId1.FieldName = "CorpId";
            this.colCorpId1.Name = "colCorpId1";
            // 
            // colCustCode1
            // 
            this.colCustCode1.FieldName = "CustCode";
            this.colCustCode1.Name = "colCustCode1";
            // 
            // colShipToCode1
            // 
            this.colShipToCode1.FieldName = "ShipToCode";
            this.colShipToCode1.Name = "colShipToCode1";
            // 
            // colShipToName1
            // 
            this.colShipToName1.Caption = "납품처";
            this.colShipToName1.FieldName = "ShipToName";
            this.colShipToName1.Name = "colShipToName1";
            this.colShipToName1.Visible = true;
            this.colShipToName1.VisibleIndex = 0;
            // 
            // colCorpId2
            // 
            this.colCorpId2.FieldName = "CorpId";
            this.colCorpId2.Name = "colCorpId2";
            this.colCorpId2.Visible = true;
            this.colCorpId2.VisibleIndex = 0;
            // 
            // colCustCode2
            // 
            this.colCustCode2.FieldName = "CustCode";
            this.colCustCode2.Name = "colCustCode2";
            this.colCustCode2.Visible = true;
            this.colCustCode2.VisibleIndex = 1;
            // 
            // colShipToCode2
            // 
            this.colShipToCode2.FieldName = "ShipToCode";
            this.colShipToCode2.Name = "colShipToCode2";
            this.colShipToCode2.Visible = true;
            this.colShipToCode2.VisibleIndex = 2;
            // 
            // colShipToName2
            // 
            this.colShipToName2.FieldName = "ShipToName";
            this.colShipToName2.Name = "colShipToName2";
            this.colShipToName2.Visible = true;
            this.colShipToName2.VisibleIndex = 3;
            // 
            // colTradeType
            // 
            this.colTradeType.FieldName = "TradeType";
            this.colTradeType.Name = "colTradeType";
            this.colTradeType.Visible = true;
            this.colTradeType.VisibleIndex = 4;
            // 
            // colVatType
            // 
            this.colVatType.FieldName = "VatType";
            this.colVatType.Name = "colVatType";
            this.colVatType.Visible = true;
            this.colVatType.VisibleIndex = 5;
            // 
            // colBillToNo
            // 
            this.colBillToNo.FieldName = "BillToNo";
            this.colBillToNo.Name = "colBillToNo";
            this.colBillToNo.Visible = true;
            this.colBillToNo.VisibleIndex = 6;
            // 
            // colFax
            // 
            this.colFax.FieldName = "Fax";
            this.colFax.Name = "colFax";
            this.colFax.Visible = true;
            this.colFax.VisibleIndex = 7;
            // 
            // colDeliLocation
            // 
            this.colDeliLocation.FieldName = "DeliLocation";
            this.colDeliLocation.Name = "colDeliLocation";
            this.colDeliLocation.Visible = true;
            this.colDeliLocation.VisibleIndex = 8;
            // 
            // colTel
            // 
            this.colTel.FieldName = "Tel";
            this.colTel.Name = "colTel";
            this.colTel.Visible = true;
            this.colTel.VisibleIndex = 9;
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 10;
            // 
            // colTransType
            // 
            this.colTransType.FieldName = "TransType";
            this.colTransType.Name = "colTransType";
            this.colTransType.Visible = true;
            this.colTransType.VisibleIndex = 11;
            // 
            // colLocation
            // 
            this.colLocation.FieldName = "Location";
            this.colLocation.Name = "colLocation";
            this.colLocation.Visible = true;
            this.colLocation.VisibleIndex = 12;
            // 
            // colLocationTitle
            // 
            this.colLocationTitle.FieldName = "LocationTitle";
            this.colLocationTitle.Name = "colLocationTitle";
            this.colLocationTitle.Visible = true;
            this.colLocationTitle.VisibleIndex = 13;
            // 
            // colArea
            // 
            this.colArea.FieldName = "Area";
            this.colArea.Name = "colArea";
            this.colArea.Visible = true;
            this.colArea.VisibleIndex = 14;
            // 
            // colShipToCost
            // 
            this.colShipToCost.FieldName = "ShipToCost";
            this.colShipToCost.Name = "colShipToCost";
            this.colShipToCost.Visible = true;
            this.colShipToCost.VisibleIndex = 15;
            // 
            // colManagerName
            // 
            this.colManagerName.FieldName = "ManagerName";
            this.colManagerName.Name = "colManagerName";
            this.colManagerName.Visible = true;
            this.colManagerName.VisibleIndex = 16;
            // 
            // colUseYn
            // 
            this.colUseYn.FieldName = "UseYn";
            this.colUseYn.Name = "colUseYn";
            this.colUseYn.Visible = true;
            this.colUseYn.VisibleIndex = 17;
            // 
            // colCreId
            // 
            this.colCreId.FieldName = "CreId";
            this.colCreId.Name = "colCreId";
            this.colCreId.Visible = true;
            this.colCreId.VisibleIndex = 18;
            // 
            // colCreIP
            // 
            this.colCreIP.FieldName = "CreIP";
            this.colCreIP.Name = "colCreIP";
            this.colCreIP.Visible = true;
            this.colCreIP.VisibleIndex = 19;
            // 
            // colCreDt
            // 
            this.colCreDt.FieldName = "CreDt";
            this.colCreDt.Name = "colCreDt";
            this.colCreDt.Visible = true;
            this.colCreDt.VisibleIndex = 20;
            // 
            // colModId
            // 
            this.colModId.FieldName = "ModId";
            this.colModId.Name = "colModId";
            this.colModId.Visible = true;
            this.colModId.VisibleIndex = 21;
            // 
            // colModIP
            // 
            this.colModIP.FieldName = "ModIP";
            this.colModIP.Name = "colModIP";
            this.colModIP.Visible = true;
            this.colModIP.VisibleIndex = 22;
            // 
            // colModDt
            // 
            this.colModDt.FieldName = "ModDt";
            this.colModDt.Name = "colModDt";
            this.colModDt.Visible = true;
            this.colModDt.VisibleIndex = 23;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btnSearch);
            this.smtCommonBar1.Controls.Add(this.lupUseYn);
            this.smtCommonBar1.Controls.Add(this.lupAlertType);
            this.smtCommonBar1.Controls.Add(this.btnAdd);
            this.smtCommonBar1.Controls.Add(this.btnSave);
            this.smtCommonBar1.Controls.Add(this.btnDelete);
            this.smtCommonBar1.Controls.Add(this.btnClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1339, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "이상변동알림관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.AllowFocus = false;
            this.btnSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btnSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Appearance.Options.UseBackColor = true;
            this.btnSearch.Appearance.Options.UseForeColor = true;
            this.btnSearch.AutoSize = true;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnSearch.ImageOptions.ImageIndex = 0;
            this.btnSearch.ImageOptions.ImageToTextIndent = 15;
            this.btnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btnSearch.Location = new System.Drawing.Point(716, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSearch.Size = new System.Drawing.Size(77, 40);
            this.btnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btnSearch.TabIndex = 33;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.ButtonClick);
            // 
            // lupUseYn
            // 
            this.lupUseYn.Dock = System.Windows.Forms.DockStyle.Left;
            this.lupUseYn.Location = new System.Drawing.Point(428, 0);
            this.lupUseYn.LookAndFeel.SkinName = "WXI";
            this.lupUseYn.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lupUseYn.Name = "lupUseYn";
            this.lupUseYn.Size = new System.Drawing.Size(288, 40);
            this.lupUseYn.smtLabelForeColor = System.Drawing.Color.White;
            this.lupUseYn.smtLabelTitleValue = "사용여부";
            this.lupUseYn.smtLabelTitleVisible = true;
            this.lupUseYn.smtLabelTitleWidth = 100;
            this.lupUseYn.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.lupUseYn.smtLookUpDataDisplayMember = "코드명";
            this.lupUseYn.smtLookUpDataSource = null;
            this.lupUseYn.smtLookUpDataValueMember = "코드";
            this.lupUseYn.smtLookUpSelectdIndex = -1;
            this.lupUseYn.smtLookUpText = "";
            this.lupUseYn.smtLookUpValue = "";
            this.lupUseYn.TabIndex = 35;
            // 
            // lupAlertType
            // 
            this.lupAlertType.Dock = System.Windows.Forms.DockStyle.Left;
            this.lupAlertType.Location = new System.Drawing.Point(140, 0);
            this.lupAlertType.LookAndFeel.SkinName = "WXI";
            this.lupAlertType.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lupAlertType.Name = "lupAlertType";
            this.lupAlertType.Size = new System.Drawing.Size(288, 40);
            this.lupAlertType.smtLabelForeColor = System.Drawing.Color.White;
            this.lupAlertType.smtLabelTitleValue = "알림구분";
            this.lupAlertType.smtLabelTitleVisible = true;
            this.lupAlertType.smtLabelTitleWidth = 100;
            this.lupAlertType.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.lupAlertType.smtLookUpDataDisplayMember = "코드명";
            this.lupAlertType.smtLookUpDataSource = null;
            this.lupAlertType.smtLookUpDataValueMember = "코드";
            this.lupAlertType.smtLookUpSelectdIndex = -1;
            this.lupAlertType.smtLookUpText = "";
            this.lupAlertType.smtLookUpValue = "";
            this.lupAlertType.TabIndex = 36;
            // 
            // btnAdd
            // 
            this.btnAdd.AllowFocus = false;
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btnAdd.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.Appearance.Options.UseForeColor = true;
            this.btnAdd.AutoSize = true;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnAdd.ImageOptions.ImageIndex = 1;
            this.btnAdd.ImageOptions.ImageToTextIndent = 15;
            this.btnAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btnAdd.Location = new System.Drawing.Point(1031, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnAdd.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnAdd.Size = new System.Drawing.Size(77, 40);
            this.btnAdd.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.btnAdd.TabIndex = 34;
            this.btnAdd.Text = "등록";
            this.btnAdd.Click += new System.EventHandler(this.ButtonClick);
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
            this.btnSave.Location = new System.Drawing.Point(1108, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSave.Size = new System.Drawing.Size(77, 40);
            this.btnSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "저장";
            this.btnSave.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnDelete
            // 
            this.btnDelete.AllowFocus = false;
            this.btnDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btnDelete.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Appearance.Options.UseBackColor = true;
            this.btnDelete.Appearance.Options.UseForeColor = true;
            this.btnDelete.AutoSize = true;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnDelete.ImageOptions.ImageIndex = 3;
            this.btnDelete.ImageOptions.ImageToTextIndent = 15;
            this.btnDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btnDelete.Location = new System.Drawing.Point(1185, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnDelete.Size = new System.Drawing.Size(77, 40);
            this.btnDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "삭제";
            this.btnDelete.Click += new System.EventHandler(this.ButtonClick);
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
            this.btnClose.Location = new System.Drawing.Point(1262, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClose.Size = new System.Drawing.Size(77, 40);
            this.btnClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "닫기";
            this.btnClose.Click += new System.EventHandler(this.ButtonClick);
            // 
            // Frm_Demand_ApiAlertConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 498);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Demand_ApiAlertConfig";
            this.Text = "이상변동알림관리";
            this.Load += new System.EventHandler(this.frmPM_ProductPlanView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId1;
        private DevExpress.XtraGrid.Columns.GridColumn colCustCode1;
        private DevExpress.XtraGrid.Columns.GridColumn colShipToCode1;
        private DevExpress.XtraGrid.Columns.GridColumn colShipToName1;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId2;
        private DevExpress.XtraGrid.Columns.GridColumn colCustCode2;
        private DevExpress.XtraGrid.Columns.GridColumn colShipToCode2;
        private DevExpress.XtraGrid.Columns.GridColumn colShipToName2;
        private DevExpress.XtraGrid.Columns.GridColumn colTradeType;
        private DevExpress.XtraGrid.Columns.GridColumn colVatType;
        private DevExpress.XtraGrid.Columns.GridColumn colBillToNo;
        private DevExpress.XtraGrid.Columns.GridColumn colFax;
        private DevExpress.XtraGrid.Columns.GridColumn colDeliLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colTel;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colTransType;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colArea;
        private DevExpress.XtraGrid.Columns.GridColumn colShipToCost;
        private DevExpress.XtraGrid.Columns.GridColumn colManagerName;
        private DevExpress.XtraGrid.Columns.GridColumn colUseYn;
        private DevExpress.XtraGrid.Columns.GridColumn colCreId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreIP;
        private DevExpress.XtraGrid.Columns.GridColumn colCreDt;
        private DevExpress.XtraGrid.Columns.GridColumn colModId;
        private DevExpress.XtraGrid.Columns.GridColumn colModIP;
        private DevExpress.XtraGrid.Columns.GridColumn colModDt;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btnClose;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraGrid.GridControl gcAlert;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAlert;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton btnDelete;
        private baseSMT.smtSearchButton btnSearch;
        private baseSMT.smtSearchButton btnSave;
        private baseSMT.smtSearchButton btnAdd;
        private baseSMT.smtSearchLookUp lupAlertType;
        private baseSMT.smtSearchLookUp lupUseYn;
    }
}