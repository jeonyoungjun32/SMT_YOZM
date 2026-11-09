
namespace SmartMES.View.DTL
{
    partial class Frm_DT_DataQuality
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DT_DataQuality));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.Btn_Reset = new baseSMT.smtSearchButton();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.DtOrder = new baseSMT.smtSearchDatetime();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.colCorpId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCTCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCTName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCT_IP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCT_Port = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCT_UserYn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModDt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.TPMain = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.LblDataLoss = new DevExpress.XtraEditors.LabelControl();
            this.labelControl111 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.LblCollectErr = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.LblOutRange = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.gcDataQuality = new DevExpress.XtraGrid.GridControl();
            this.gvDataQuality = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).BeginInit();
            this.TPMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).BeginInit();
            this.panelControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.Btn_Reset);
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.DtOrder);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1195, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "데이터 품질관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 2;
            // 
            // Btn_Reset
            // 
            this.Btn_Reset.AllowFocus = false;
            this.Btn_Reset.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.Btn_Reset.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_Reset.Appearance.Options.UseBackColor = true;
            this.Btn_Reset.Appearance.Options.UseForeColor = true;
            this.Btn_Reset.AutoSize = true;
            this.Btn_Reset.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Reset.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.Btn_Reset.ImageOptions.ImageIndex = 1;
            this.Btn_Reset.ImageOptions.ImageToTextIndent = 15;
            this.Btn_Reset.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Btn_Reset.ImageOptions.SvgImage")));
            this.Btn_Reset.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Btn_Reset.Location = new System.Drawing.Point(1029, 0);
            this.Btn_Reset.Name = "Btn_Reset";
            this.Btn_Reset.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Btn_Reset.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.Btn_Reset.Size = new System.Drawing.Size(89, 40);
            this.Btn_Reset.smtButtonAutoText = false;
            this.Btn_Reset.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.Btn_Reset.TabIndex = 28;
            this.Btn_Reset.Text = "초기화";
            this.Btn_Reset.Click += new System.EventHandler(this.Btn_Click);
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
            this.cmdDisplay.Location = new System.Drawing.Point(476, 0);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 27;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Click += new System.EventHandler(this.Btn_Click);
            // 
            // DtOrder
            // 
            this.DtOrder.Dock = System.Windows.Forms.DockStyle.Left;
            this.DtOrder.Location = new System.Drawing.Point(131, 0);
            this.DtOrder.LookAndFeel.SkinName = "WXI";
            this.DtOrder.LookAndFeel.UseDefaultLookAndFeel = false;
            this.DtOrder.Name = "DtOrder";
            this.DtOrder.Size = new System.Drawing.Size(345, 40);
            this.DtOrder.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DtOrder.smtComboTitleItems = new string[0];
            this.DtOrder.smtComboTitleValue = "";
            this.DtOrder.smtComboTitleVisible = false;
            this.DtOrder.smtComboTitleWidth = 100;
            this.DtOrder.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.DtOrder.smtEndDateTimeValue = new System.DateTime(2025, 2, 25, 15, 50, 58, 413);
            this.DtOrder.smtEndDateTimeVisible = true;
            this.DtOrder.smtLabelForeColor = System.Drawing.Color.White;
            this.DtOrder.smtLabelTitleValue = "조회기간";
            this.DtOrder.smtLabelTitleVisible = true;
            this.DtOrder.smtLabelTitleWidth = 100;
            this.DtOrder.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.DtOrder.smtStartDateTimeValue = new System.DateTime(2025, 2, 25, 15, 50, 58, 413);
            this.DtOrder.smtStartDateTimeVisible = true;
            this.DtOrder.TabIndex = 26;
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
            this.cmdClose.Location = new System.Drawing.Point(1118, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // colCorpId1
            // 
            this.colCorpId1.Caption = "사업장번호";
            this.colCorpId1.FieldName = "CorpId";
            this.colCorpId1.Name = "colCorpId1";
            // 
            // colCTCode
            // 
            this.colCTCode.Caption = "컨트롤코드";
            this.colCTCode.FieldName = "CTCode";
            this.colCTCode.Name = "colCTCode";
            this.colCTCode.Visible = true;
            this.colCTCode.VisibleIndex = 0;
            this.colCTCode.Width = 98;
            // 
            // colCTName
            // 
            this.colCTName.Caption = "컨트롤명";
            this.colCTName.FieldName = "CTName";
            this.colCTName.Name = "colCTName";
            this.colCTName.Visible = true;
            this.colCTName.VisibleIndex = 1;
            this.colCTName.Width = 148;
            // 
            // colCT_IP
            // 
            this.colCT_IP.Caption = "IP";
            this.colCT_IP.FieldName = "CT_IP";
            this.colCT_IP.Name = "colCT_IP";
            this.colCT_IP.Visible = true;
            this.colCT_IP.VisibleIndex = 2;
            this.colCT_IP.Width = 144;
            // 
            // colCT_Port
            // 
            this.colCT_Port.Caption = "Port";
            this.colCT_Port.FieldName = "CT_Port";
            this.colCT_Port.Name = "colCT_Port";
            this.colCT_Port.Visible = true;
            this.colCT_Port.VisibleIndex = 3;
            // 
            // colCT_UserYn
            // 
            this.colCT_UserYn.Caption = "사용유무";
            this.colCT_UserYn.FieldName = "CT_UserYn";
            this.colCT_UserYn.Name = "colCT_UserYn";
            // 
            // colCreId
            // 
            this.colCreId.FieldName = "CreId";
            this.colCreId.Name = "colCreId";
            // 
            // colCreIP
            // 
            this.colCreIP.FieldName = "CreIP";
            this.colCreIP.Name = "colCreIP";
            // 
            // colCreDt
            // 
            this.colCreDt.FieldName = "CreDt";
            this.colCreDt.Name = "colCreDt";
            // 
            // colModId
            // 
            this.colModId.FieldName = "ModId";
            this.colModId.Name = "colModId";
            // 
            // colModIP
            // 
            this.colModIP.FieldName = "ModIP";
            this.colModIP.Name = "colModIP";
            // 
            // colModDt
            // 
            this.colModDt.FieldName = "ModDt";
            this.colModDt.Name = "colModDt";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1195, 543);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.splitContainerControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1195, 543);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(3, 3);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.TPMain);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.gcDataQuality);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1189, 537);
            this.splitContainerControl1.TabIndex = 4;
            // 
            // TPMain
            // 
            this.TPMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.TPMain.Appearance.Options.UseBackColor = true;
            this.TPMain.AutoScroll = true;
            this.TPMain.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F)});
            this.TPMain.Controls.Add(this.panelControl12);
            this.TPMain.Controls.Add(this.panelControl3);
            this.TPMain.Controls.Add(this.panelControl5);
            this.TPMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TPMain.Location = new System.Drawing.Point(0, 0);
            this.TPMain.Name = "TPMain";
            this.TPMain.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.TPMain.Size = new System.Drawing.Size(1189, 100);
            this.TPMain.TabIndex = 2;
            // 
            // panelControl12
            // 
            this.panelControl12.Appearance.BackColor = System.Drawing.Color.Honeydew;
            this.panelControl12.Appearance.Options.UseBackColor = true;
            this.panelControl12.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TPMain.SetColumn(this.panelControl12, 0);
            this.panelControl12.Controls.Add(this.LblDataLoss);
            this.panelControl12.Controls.Add(this.labelControl111);
            this.panelControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl12.Location = new System.Drawing.Point(3, 3);
            this.panelControl12.Name = "panelControl12";
            this.TPMain.SetRow(this.panelControl12, 0);
            this.panelControl12.Size = new System.Drawing.Size(390, 94);
            this.panelControl12.TabIndex = 0;
            // 
            // LblDataLoss
            // 
            this.LblDataLoss.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblDataLoss.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LblDataLoss.Appearance.Options.UseFont = true;
            this.LblDataLoss.Appearance.Options.UseForeColor = true;
            this.LblDataLoss.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblDataLoss.Location = new System.Drawing.Point(16, 38);
            this.LblDataLoss.Name = "LblDataLoss";
            this.LblDataLoss.Size = new System.Drawing.Size(135, 46);
            this.LblDataLoss.TabIndex = 1;
            this.LblDataLoss.Text = "-";
            // 
            // labelControl111
            // 
            this.labelControl111.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.labelControl111.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControl111.Appearance.Options.UseFont = true;
            this.labelControl111.Appearance.Options.UseForeColor = true;
            this.labelControl111.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl111.Location = new System.Drawing.Point(18, 9);
            this.labelControl111.Name = "labelControl111";
            this.labelControl111.Size = new System.Drawing.Size(107, 27);
            this.labelControl111.TabIndex = 0;
            this.labelControl111.Text = "데이터누락";
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.MistyRose;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TPMain.SetColumn(this.panelControl3, 1);
            this.panelControl3.Controls.Add(this.LblCollectErr);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(399, 3);
            this.panelControl3.Name = "panelControl3";
            this.TPMain.SetRow(this.panelControl3, 0);
            this.panelControl3.Size = new System.Drawing.Size(390, 94);
            this.panelControl3.TabIndex = 1;
            // 
            // LblCollectErr
            // 
            this.LblCollectErr.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblCollectErr.Appearance.ForeColor = System.Drawing.Color.DarkRed;
            this.LblCollectErr.Appearance.Options.UseFont = true;
            this.LblCollectErr.Appearance.Options.UseForeColor = true;
            this.LblCollectErr.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblCollectErr.Location = new System.Drawing.Point(24, 38);
            this.LblCollectErr.Name = "LblCollectErr";
            this.LblCollectErr.Size = new System.Drawing.Size(135, 46);
            this.LblCollectErr.TabIndex = 1;
            this.LblCollectErr.Text = "-";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.DarkRed;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(24, 9);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(107, 27);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "수집 오류";
            // 
            // panelControl5
            // 
            this.panelControl5.Appearance.BackColor = System.Drawing.Color.OldLace;
            this.panelControl5.Appearance.Options.UseBackColor = true;
            this.panelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.TPMain.SetColumn(this.panelControl5, 2);
            this.panelControl5.Controls.Add(this.LblOutRange);
            this.panelControl5.Controls.Add(this.labelControl9);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(796, 3);
            this.panelControl5.Name = "panelControl5";
            this.TPMain.SetRow(this.panelControl5, 0);
            this.panelControl5.Size = new System.Drawing.Size(390, 94);
            this.panelControl5.TabIndex = 2;
            // 
            // LblOutRange
            // 
            this.LblOutRange.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblOutRange.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.LblOutRange.Appearance.Options.UseFont = true;
            this.LblOutRange.Appearance.Options.UseForeColor = true;
            this.LblOutRange.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblOutRange.Location = new System.Drawing.Point(24, 38);
            this.LblOutRange.Name = "LblOutRange";
            this.LblOutRange.Size = new System.Drawing.Size(144, 46);
            this.LblOutRange.TabIndex = 1;
            this.LblOutRange.Text = "-";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.Location = new System.Drawing.Point(24, 9);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(107, 27);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "범위초과";
            // 
            // gcDataQuality
            // 
            this.gcDataQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataQuality.Location = new System.Drawing.Point(0, 0);
            this.gcDataQuality.MainView = this.gvDataQuality;
            this.gcDataQuality.Name = "gcDataQuality";
            this.gcDataQuality.Size = new System.Drawing.Size(1189, 421);
            this.gcDataQuality.TabIndex = 8;
            this.gcDataQuality.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDataQuality});
            // 
            // gvDataQuality
            // 
            this.gvDataQuality.GridControl = this.gcDataQuality;
            this.gvDataQuality.Name = "gvDataQuality";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.splitContainerControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1243, 348, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1195, 543);
            this.layoutControl1.TabIndex = 6;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Frm_DT_DataQuality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 583);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_DT_DataQuality";
            this.Text = "데이터 품질관리";
            this.Load += new System.EventHandler(this.frmPLCSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).EndInit();
            this.TPMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).EndInit();
            this.panelControl12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDataQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId1;
        private DevExpress.XtraGrid.Columns.GridColumn colCTCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCTName;
        private DevExpress.XtraGrid.Columns.GridColumn colCT_IP;
        private DevExpress.XtraGrid.Columns.GridColumn colCT_Port;
        private DevExpress.XtraGrid.Columns.GridColumn colCT_UserYn;
        private DevExpress.XtraGrid.Columns.GridColumn colCreId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreIP;
        private DevExpress.XtraGrid.Columns.GridColumn colCreDt;
        private DevExpress.XtraGrid.Columns.GridColumn colModId;
        private DevExpress.XtraGrid.Columns.GridColumn colModIP;
        private DevExpress.XtraGrid.Columns.GridColumn colModDt;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchDatetime DtOrder;
        private baseSMT.smtSearchButton Btn_Reset;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcDataQuality;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDataQuality;
        private DevExpress.Utils.Layout.TablePanel TPMain;
        private DevExpress.XtraEditors.PanelControl panelControl12;
        private DevExpress.XtraEditors.LabelControl LblDataLoss;
        private DevExpress.XtraEditors.LabelControl labelControl111;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl LblCollectErr;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.PanelControl panelControl5;
        private DevExpress.XtraEditors.LabelControl LblOutRange;
        private DevExpress.XtraEditors.LabelControl labelControl9;
    }
}