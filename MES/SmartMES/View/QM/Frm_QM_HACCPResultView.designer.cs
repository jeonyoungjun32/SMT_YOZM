
namespace SmartMES.View.QM
{
    partial class Frm_QM_HACCPResultView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_HACCPResultView));
			this.Grp_Num = new DevExpress.XtraEditors.GroupControl();
			this.Gc_ExRst = new DevExpress.XtraGrid.GridControl();
			this.Gv_ExRst = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.PnlTargetDiv = new System.Windows.Forms.Panel();
			this.Lup_Target_Div = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
			this.PnlCCPDiv = new System.Windows.Forms.Panel();
			this.Lup_CCP_Div = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.Dt_Date = new baseSMT.smtSearchDatetime();
			this.cmdInspectStd = new baseSMT.smtSearchButton();
			this.cmdInspectionRpt = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.smtSearchButton9 = new baseSMT.smtSearchButton();
			this.smtSearchButton7 = new baseSMT.smtSearchButton();
			this.smtSearchButton6 = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.Grp_Num)).BeginInit();
			this.Grp_Num.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_ExRst)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_ExRst)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			this.PnlTargetDiv.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Lup_Target_Div.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
			this.PnlCCPDiv.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Lup_CCP_Div.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Grp_Num
			// 
			this.Grp_Num.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
			this.Grp_Num.AppearanceCaption.Options.UseFont = true;
			this.Grp_Num.Controls.Add(this.Gc_ExRst);
			this.Grp_Num.Location = new System.Drawing.Point(3, 3);
			this.Grp_Num.Name = "Grp_Num";
			this.Grp_Num.Size = new System.Drawing.Size(1399, 648);
			this.Grp_Num.TabIndex = 281;
			this.Grp_Num.Text = "검사내역";
			// 
			// Gc_ExRst
			// 
			this.Gc_ExRst.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_ExRst.Location = new System.Drawing.Point(2, 29);
			this.Gc_ExRst.MainView = this.Gv_ExRst;
			this.Gc_ExRst.Name = "Gc_ExRst";
			this.Gc_ExRst.Size = new System.Drawing.Size(1395, 617);
			this.Gc_ExRst.TabIndex = 1;
			this.Gc_ExRst.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_ExRst});
			// 
			// Gv_ExRst
			// 
			this.Gv_ExRst.DetailHeight = 397;
			this.Gv_ExRst.GridControl = this.Gc_ExRst;
			this.Gv_ExRst.Name = "Gv_ExRst";
			this.Gv_ExRst.OptionsEditForm.PopupEditFormWidth = 914;
			this.Gv_ExRst.OptionsView.ShowGroupPanel = false;
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.Grp_Num);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(631, 365, 808, 450);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1405, 654);
			this.layoutControl1.TabIndex = 282;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// PnlTargetDiv
			// 
			this.PnlTargetDiv.Controls.Add(this.Lup_Target_Div);
			this.PnlTargetDiv.Controls.Add(this.labelControl7);
			this.PnlTargetDiv.Dock = System.Windows.Forms.DockStyle.Left;
			this.PnlTargetDiv.Location = new System.Drawing.Point(657, 0);
			this.PnlTargetDiv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.PnlTargetDiv.MaximumSize = new System.Drawing.Size(220, 0);
			this.PnlTargetDiv.Name = "PnlTargetDiv";
			this.PnlTargetDiv.Size = new System.Drawing.Size(220, 40);
			this.PnlTargetDiv.TabIndex = 298;
			// 
			// Lup_Target_Div
			// 
			this.Lup_Target_Div.Location = new System.Drawing.Point(80, 5);
			this.Lup_Target_Div.Name = "Lup_Target_Div";
			this.Lup_Target_Div.Properties.AutoHeight = false;
			this.Lup_Target_Div.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.Lup_Target_Div.Properties.DisplayFormat.FormatString = "#,#";
			this.Lup_Target_Div.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.Lup_Target_Div.Properties.EditFormat.FormatString = "#,#";
			this.Lup_Target_Div.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.Lup_Target_Div.Properties.NullText = "";
			this.Lup_Target_Div.Properties.PopupView = this.gridView7;
			this.Lup_Target_Div.Size = new System.Drawing.Size(137, 30);
			this.Lup_Target_Div.TabIndex = 0;
			this.Lup_Target_Div.EditValueChanged += new System.EventHandler(this.Lup_CCP_Div_EditValueChanged_1);
			// 
			// gridView7
			// 
			this.gridView7.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView7.Name = "gridView7";
			this.gridView7.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView7.OptionsView.ShowGroupPanel = false;
			// 
			// labelControl7
			// 
			this.labelControl7.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControl7.Appearance.Options.UseForeColor = true;
			this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl7.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelControl7.Location = new System.Drawing.Point(0, 0);
			this.labelControl7.Name = "labelControl7";
			this.labelControl7.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.labelControl7.Size = new System.Drawing.Size(69, 40);
			this.labelControl7.TabIndex = 292;
			this.labelControl7.Text = "관리대상";
			// 
			// PnlCCPDiv
			// 
			this.PnlCCPDiv.Controls.Add(this.Lup_CCP_Div);
			this.PnlCCPDiv.Controls.Add(this.labelControl6);
			this.PnlCCPDiv.Dock = System.Windows.Forms.DockStyle.Left;
			this.PnlCCPDiv.Location = new System.Drawing.Point(437, 0);
			this.PnlCCPDiv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.PnlCCPDiv.MaximumSize = new System.Drawing.Size(220, 0);
			this.PnlCCPDiv.Name = "PnlCCPDiv";
			this.PnlCCPDiv.Size = new System.Drawing.Size(220, 40);
			this.PnlCCPDiv.TabIndex = 297;
			// 
			// Lup_CCP_Div
			// 
			this.Lup_CCP_Div.Location = new System.Drawing.Point(80, 5);
			this.Lup_CCP_Div.Name = "Lup_CCP_Div";
			this.Lup_CCP_Div.Properties.AutoHeight = false;
			this.Lup_CCP_Div.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.Lup_CCP_Div.Properties.DisplayFormat.FormatString = "#,#";
			this.Lup_CCP_Div.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.Lup_CCP_Div.Properties.EditFormat.FormatString = "#,#";
			this.Lup_CCP_Div.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.Lup_CCP_Div.Properties.NullText = "";
			this.Lup_CCP_Div.Properties.PopupView = this.gridView6;
			this.Lup_CCP_Div.Size = new System.Drawing.Size(137, 30);
			this.Lup_CCP_Div.TabIndex = 0;
			this.Lup_CCP_Div.EditValueChanged += new System.EventHandler(this.Lup_CCP_Div_EditValueChanged_1);
			// 
			// gridView6
			// 
			this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView6.Name = "gridView6";
			this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView6.OptionsView.ShowGroupPanel = false;
			// 
			// labelControl6
			// 
			this.labelControl6.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControl6.Appearance.Options.UseForeColor = true;
			this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl6.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelControl6.Location = new System.Drawing.Point(0, 0);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.labelControl6.Size = new System.Drawing.Size(69, 40);
			this.labelControl6.TabIndex = 292;
			this.labelControl6.Text = "CCP구분";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1405, 654);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.Grp_Num;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1405, 654);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// groupControl2
			// 
			this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.groupControl2.Controls.Add(this.layoutControl1);
			this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl2.Location = new System.Drawing.Point(0, 40);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(1405, 654);
			this.groupControl2.TabIndex = 60;
			this.groupControl2.Text = "groupControl2";
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.PnlTargetDiv);
			this.smtCommonBar1.Controls.Add(this.PnlCCPDiv);
			this.smtCommonBar1.Controls.Add(this.Dt_Date);
			this.smtCommonBar1.Controls.Add(this.cmdInspectStd);
			this.smtCommonBar1.Controls.Add(this.cmdInspectionRpt);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1405, 40);
			this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "공정지시";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 61;
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
			this.cmdDisplay.Location = new System.Drawing.Point(877, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Click += new System.EventHandler(this.BtnSearch_Click);
			// 
			// Dt_Date
			// 
			this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
			this.Dt_Date.Location = new System.Drawing.Point(92, 0);
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
			this.Dt_Date.smtLabelForeColor = System.Drawing.Color.White;
			this.Dt_Date.smtLabelTitleValue = "조회기간";
			this.Dt_Date.smtLabelTitleVisible = true;
			this.Dt_Date.smtLabelTitleWidth = 100;
			this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
			this.Dt_Date.smtStartDateTimeVisible = true;
			this.Dt_Date.TabIndex = 10;
			// 
			// cmdInspectStd
			// 
			this.cmdInspectStd.AllowFocus = false;
			this.cmdInspectStd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdInspectStd.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdInspectStd.Appearance.Options.UseBackColor = true;
			this.cmdInspectStd.Appearance.Options.UseForeColor = true;
			this.cmdInspectStd.AutoSize = true;
			this.cmdInspectStd.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdInspectStd.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdInspectStd.ImageOptions.ImageIndex = 10;
			this.cmdInspectStd.ImageOptions.ImageToTextIndent = 15;
			this.cmdInspectStd.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdInspectStd.Location = new System.Drawing.Point(1126, 0);
			this.cmdInspectStd.Name = "cmdInspectStd";
			this.cmdInspectStd.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInspectStd.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInspectStd.Size = new System.Drawing.Size(113, 40);
			this.cmdInspectStd.smtButtonStyle = baseSMT.EnumButtonStyles.프린터;
			this.cmdInspectStd.TabIndex = 8;
			this.cmdInspectStd.Text = "검사기준서";
			this.cmdInspectStd.Click += new System.EventHandler(this.Btn_Click);
			// 
			// cmdInspectionRpt
			// 
			this.cmdInspectionRpt.AllowFocus = false;
			this.cmdInspectionRpt.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdInspectionRpt.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdInspectionRpt.Appearance.Options.UseBackColor = true;
			this.cmdInspectionRpt.Appearance.Options.UseForeColor = true;
			this.cmdInspectionRpt.AutoSize = true;
			this.cmdInspectionRpt.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdInspectionRpt.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdInspectionRpt.ImageOptions.ImageIndex = 10;
			this.cmdInspectionRpt.ImageOptions.ImageToTextIndent = 15;
			this.cmdInspectionRpt.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdInspectionRpt.Location = new System.Drawing.Point(1239, 0);
			this.cmdInspectionRpt.Name = "cmdInspectionRpt";
			this.cmdInspectionRpt.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInspectionRpt.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInspectionRpt.Size = new System.Drawing.Size(89, 40);
			this.cmdInspectionRpt.smtButtonStyle = baseSMT.EnumButtonStyles.프린터;
			this.cmdInspectionRpt.TabIndex = 6;
			this.cmdInspectionRpt.Text = "성적서";
			this.cmdInspectionRpt.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(1328, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// smtSearchButton9
			// 
			this.smtSearchButton9.AllowFocus = false;
			this.smtSearchButton9.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtSearchButton9.Appearance.ForeColor = System.Drawing.Color.White;
			this.smtSearchButton9.Appearance.Options.UseBackColor = true;
			this.smtSearchButton9.Appearance.Options.UseForeColor = true;
			this.smtSearchButton9.AutoSize = true;
			this.smtSearchButton9.Dock = System.Windows.Forms.DockStyle.Right;
			this.smtSearchButton9.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.smtSearchButton9.ImageOptions.ImageIndex = 5;
			this.smtSearchButton9.ImageOptions.ImageToTextIndent = 15;
			this.smtSearchButton9.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.smtSearchButton9.Location = new System.Drawing.Point(1322, 0);
			this.smtSearchButton9.Name = "smtSearchButton9";
			this.smtSearchButton9.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.smtSearchButton9.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.smtSearchButton9.Size = new System.Drawing.Size(77, 40);
			this.smtSearchButton9.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.smtSearchButton9.TabIndex = 1;
			this.smtSearchButton9.Text = "닫기";
			// 
			// smtSearchButton7
			// 
			this.smtSearchButton7.AllowFocus = false;
			this.smtSearchButton7.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtSearchButton7.Appearance.ForeColor = System.Drawing.Color.White;
			this.smtSearchButton7.Appearance.Options.UseBackColor = true;
			this.smtSearchButton7.Appearance.Options.UseForeColor = true;
			this.smtSearchButton7.AutoSize = true;
			this.smtSearchButton7.Dock = System.Windows.Forms.DockStyle.Right;
			this.smtSearchButton7.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.smtSearchButton7.ImageOptions.ImageIndex = 5;
			this.smtSearchButton7.ImageOptions.ImageToTextIndent = 15;
			this.smtSearchButton7.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.smtSearchButton7.Location = new System.Drawing.Point(1245, 0);
			this.smtSearchButton7.Name = "smtSearchButton7";
			this.smtSearchButton7.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.smtSearchButton7.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.smtSearchButton7.Size = new System.Drawing.Size(77, 40);
			this.smtSearchButton7.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.smtSearchButton7.TabIndex = 6;
			this.smtSearchButton7.Text = "닫기";
			// 
			// smtSearchButton6
			// 
			this.smtSearchButton6.AllowFocus = false;
			this.smtSearchButton6.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtSearchButton6.Appearance.ForeColor = System.Drawing.Color.White;
			this.smtSearchButton6.Appearance.Options.UseBackColor = true;
			this.smtSearchButton6.Appearance.Options.UseForeColor = true;
			this.smtSearchButton6.AutoSize = true;
			this.smtSearchButton6.Dock = System.Windows.Forms.DockStyle.Right;
			this.smtSearchButton6.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.smtSearchButton6.ImageOptions.ImageIndex = 5;
			this.smtSearchButton6.ImageOptions.ImageToTextIndent = 15;
			this.smtSearchButton6.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.smtSearchButton6.Location = new System.Drawing.Point(1168, 0);
			this.smtSearchButton6.Name = "smtSearchButton6";
			this.smtSearchButton6.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.smtSearchButton6.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.smtSearchButton6.Size = new System.Drawing.Size(77, 40);
			this.smtSearchButton6.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.smtSearchButton6.TabIndex = 7;
			this.smtSearchButton6.Text = "닫기";
			// 
			// Frm_QM_HACCPResultView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1405, 694);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_QM_HACCPResultView";
			this.Text = "공정지시";
			this.Load += new System.EventHandler(this.Frm_BN_Order_Load);
			((System.ComponentModel.ISupportInitialize)(this.Grp_Num)).EndInit();
			this.Grp_Num.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_ExRst)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_ExRst)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			this.PnlTargetDiv.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Lup_Target_Div.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
			this.PnlCCPDiv.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Lup_CCP_Div.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl Grp_Num;
        private DevExpress.XtraGrid.GridControl Gc_ExRst;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_ExRst;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.GroupControl groupControl2;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdClose;
		private baseSMT.smtSearchButton cmdInspectionRpt;
		private baseSMT.smtSearchButton cmdInspectStd;
		private baseSMT.smtSearchButton smtSearchButton9;
		private baseSMT.smtSearchButton smtSearchButton7;
		private baseSMT.smtSearchButton smtSearchButton6;
        private baseSMT.smtSearchDatetime Dt_Date;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private System.Windows.Forms.Panel PnlTargetDiv;
		private DevExpress.XtraEditors.SearchLookUpEdit Lup_Target_Div;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
		private DevExpress.XtraEditors.LabelControl labelControl7;
		private System.Windows.Forms.Panel PnlCCPDiv;
		private DevExpress.XtraEditors.SearchLookUpEdit Lup_CCP_Div;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
		private DevExpress.XtraEditors.LabelControl labelControl6;
	}
}