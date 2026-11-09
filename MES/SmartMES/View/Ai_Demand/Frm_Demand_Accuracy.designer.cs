namespace SmartMES.View.Ai_Demand
{
    partial class Frm_Demand_Accuracy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Demand_ApiDataAdjust));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcAccuracy = new DevExpress.XtraGrid.GridControl();
            this.gvAccuracy = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            this.txtSearchItemName = new baseSMT.smtSearchTextBox();
            this.dtSearchDate = new baseSMT.smtSearchDatetime();
            this.cmdClose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAccuracy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAccuracy)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.gcAccuracy);
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
            // gcAccuracy
            // 
            this.gcAccuracy.Location = new System.Drawing.Point(7, 34);
            this.gcAccuracy.MainView = this.gvAccuracy;
            this.gcAccuracy.Name = "gcAccuracy";
            this.gcAccuracy.Size = new System.Drawing.Size(1325, 417);
            this.gcAccuracy.TabIndex = 50;
            this.gcAccuracy.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAccuracy});
            // 
            // gvAccuracy
            // 
            this.gvAccuracy.GridControl = this.gcAccuracy;
            this.gvAccuracy.Name = "gvAccuracy";
            this.gvAccuracy.OptionsView.ShowGroupPanel = false;
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
            this.layoutControlGroup3.Text = "예측/실적비교리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAccuracy;
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
            this.smtCommonBar1.Controls.Add(this.txtSearchItemName);
            this.smtCommonBar1.Controls.Add(this.dtSearchDate);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1339, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "AI예측 정확도확인";
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
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnSearch.ImageOptions.ImageIndex = 0;
            this.btnSearch.ImageOptions.ImageToTextIndent = 15;
            this.btnSearch.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btnSearch.Location = new System.Drawing.Point(1185, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSearch.Size = new System.Drawing.Size(77, 40);
            this.btnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btnSearch.TabIndex = 33;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.ButtonClick);
            // 
            // txtSearchItemName
            // 
            this.txtSearchItemName.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtSearchItemName.Location = new System.Drawing.Point(365, 0);
            this.txtSearchItemName.LookAndFeel.SkinName = "WXI";
            this.txtSearchItemName.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtSearchItemName.Name = "txtSearchItemName";
            this.txtSearchItemName.Size = new System.Drawing.Size(288, 40);
            this.txtSearchItemName.smtLabelTitleValue = "품명";
            this.txtSearchItemName.smtLabelTitleVisible = true;
            this.txtSearchItemName.smtLabelTitleWidth = 100;
            this.txtSearchItemName.smtTextBoxDecimal = 0;
            this.txtSearchItemName.smtTextBoxMust = false;
            this.txtSearchItemName.smtTextBoxNumber = false;
            this.txtSearchItemName.smtTextBoxReadOnly = false;
            this.txtSearchItemName.smtTextBoxValue = "";
            this.txtSearchItemName.TabIndex = 1;
            // 
            // dtSearchDate
            // 
            this.dtSearchDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtSearchDate.Location = new System.Drawing.Point(145, 0);
            this.dtSearchDate.LookAndFeel.SkinName = "WXI";
            this.dtSearchDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtSearchDate.Name = "dtSearchDate";
            this.dtSearchDate.Size = new System.Drawing.Size(220, 40);
            this.dtSearchDate.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.dtSearchDate.smtComboTitleItems = new string[0];
            this.dtSearchDate.smtComboTitleValue = "";
            this.dtSearchDate.smtComboTitleVisible = false;
            this.dtSearchDate.smtComboTitleWidth = 100;
            this.dtSearchDate.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.dtSearchDate.smtEndDateTimeValue = new System.DateTime(2026, 8, 26, 11, 17, 4, 744);
            this.dtSearchDate.smtEndDateTimeVisible = false;
            this.dtSearchDate.smtLabelForeColor = System.Drawing.Color.White;
            this.dtSearchDate.smtLabelTitleValue = "조회기간";
            this.dtSearchDate.smtLabelTitleVisible = true;
            this.dtSearchDate.smtLabelTitleWidth = 100;
            this.dtSearchDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.dtSearchDate.smtStartDateTimeValue = new System.DateTime(2026, 8, 26, 11, 17, 4, 744);
            this.dtSearchDate.smtStartDateTimeVisible = true;
            this.dtSearchDate.TabIndex = 0;
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
            this.cmdClose.Location = new System.Drawing.Point(1262, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.CmdClose_Click);
            // 
            // Frm_Demand_ApiDataAdjust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 498);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Demand_ApiDataAdjust";
            this.Text = "AI예측 정확도확인";
            this.Load += new System.EventHandler(this.Frm_Demand_ApiDataAdjust_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAccuracy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAccuracy)).EndInit();
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
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraGrid.GridControl gcAccuracy;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAccuracy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchDatetime dtSearchDate;
        private baseSMT.smtSearchTextBox txtSearchItemName;
        private baseSMT.smtSearchButton btnSearch;
    }
}