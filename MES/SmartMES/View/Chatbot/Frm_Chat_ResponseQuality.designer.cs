namespace SmartMES.View.Chatbot
{
    partial class Frm_Chat_ResponseQuality
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Chat_ResponseQuality));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.LupMatch = new baseSMT.smtSearchLookUp();
            this.LupFeedback = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tlpKpiCards = new System.Windows.Forms.TableLayoutPanel();
            this.panelResponseTime = new DevExpress.XtraEditors.PanelControl();
            this.lblValueResTime = new DevExpress.XtraEditors.LabelControl();
            this.lblTitleResTime = new DevExpress.XtraEditors.LabelControl();
            this.panelSatis = new DevExpress.XtraEditors.PanelControl();
            this.lblValueSatis = new DevExpress.XtraEditors.LabelControl();
            this.lblTitleSatis = new DevExpress.XtraEditors.LabelControl();
            this.panelDislike = new DevExpress.XtraEditors.PanelControl();
            this.lblValueDislike = new DevExpress.XtraEditors.LabelControl();
            this.lblTitleDislike = new DevExpress.XtraEditors.LabelControl();
            this.panelLike = new DevExpress.XtraEditors.PanelControl();
            this.lblValueLike = new DevExpress.XtraEditors.LabelControl();
            this.lblTitleLike = new DevExpress.XtraEditors.LabelControl();
            this.Gc_List = new DevExpress.XtraGrid.GridControl();
            this.Gv_List = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tlpKpiCards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelResponseTime)).BeginInit();
            this.panelResponseTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSatis)).BeginInit();
            this.panelSatis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelDislike)).BeginInit();
            this.panelDislike.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelLike)).BeginInit();
            this.panelLike.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.LupMatch);
            this.smtCommonBar1.Controls.Add(this.LupFeedback);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1313, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "응답품질평가";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 7;
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
            this.cmdExcel.Location = new System.Drawing.Point(1135, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(101, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 40;
            this.cmdExcel.Text = "엑셀변환";
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
            this.cmdDisplay.Location = new System.Drawing.Point(861, 0);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 39;
            this.cmdDisplay.Text = "조회";
            // 
            // LupMatch
            // 
            this.LupMatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupMatch.Location = new System.Drawing.Point(662, 0);
            this.LupMatch.LookAndFeel.SkinName = "WXI";
            this.LupMatch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupMatch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LupMatch.Name = "LupMatch";
            this.LupMatch.Size = new System.Drawing.Size(199, 40);
            this.LupMatch.smtLabelForeColor = System.Drawing.Color.White;
            this.LupMatch.smtLabelTitleValue = "매칭여부";
            this.LupMatch.smtLabelTitleVisible = true;
            this.LupMatch.smtLabelTitleWidth = 100;
            this.LupMatch.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupMatch.smtLookUpDataDisplayMember = "";
            this.LupMatch.smtLookUpDataSource = null;
            this.LupMatch.smtLookUpDataValueMember = "";
            this.LupMatch.smtLookUpSelectdIndex = -1;
            this.LupMatch.smtLookUpText = "";
            this.LupMatch.smtLookUpValue = "";
            this.LupMatch.TabIndex = 38;
            this.LupMatch.Visible = false;
            // 
            // LupFeedback
            // 
            this.LupFeedback.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupFeedback.Location = new System.Drawing.Point(461, 0);
            this.LupFeedback.LookAndFeel.SkinName = "WXI";
            this.LupFeedback.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupFeedback.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LupFeedback.Name = "LupFeedback";
            this.LupFeedback.Size = new System.Drawing.Size(201, 40);
            this.LupFeedback.smtLabelForeColor = System.Drawing.Color.White;
            this.LupFeedback.smtLabelTitleValue = "피드백 구분";
            this.LupFeedback.smtLabelTitleVisible = true;
            this.LupFeedback.smtLabelTitleWidth = 100;
            this.LupFeedback.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupFeedback.smtLookUpDataDisplayMember = "";
            this.LupFeedback.smtLookUpDataSource = null;
            this.LupFeedback.smtLookUpDataValueMember = "";
            this.LupFeedback.smtLookUpSelectdIndex = -1;
            this.LupFeedback.smtLookUpText = "";
            this.LupFeedback.smtLookUpValue = "";
            this.LupFeedback.TabIndex = 36;
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(116, 0);
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
            this.Dt_Date.TabIndex = 35;
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
            this.cmdClose.Location = new System.Drawing.Point(1236, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.Gc_List);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1313, 483);
            this.layoutControl1.TabIndex = 8;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.tlpKpiCards);
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1307, 92);
            this.panelControl1.TabIndex = 102;
            // 
            // tlpKpiCards
            // 
            this.tlpKpiCards.ColumnCount = 4;
            this.tlpKpiCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKpiCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKpiCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKpiCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpKpiCards.Controls.Add(this.panelResponseTime, 3, 0);
            this.tlpKpiCards.Controls.Add(this.panelSatis, 2, 0);
            this.tlpKpiCards.Controls.Add(this.panelDislike, 1, 0);
            this.tlpKpiCards.Controls.Add(this.panelLike, 0, 0);
            this.tlpKpiCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpKpiCards.Location = new System.Drawing.Point(0, 0);
            this.tlpKpiCards.Name = "tlpKpiCards";
            this.tlpKpiCards.RowCount = 1;
            this.tlpKpiCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpKpiCards.Size = new System.Drawing.Size(1307, 92);
            this.tlpKpiCards.TabIndex = 0;
            // 
            // panelResponseTime
            // 
            this.panelResponseTime.Appearance.BackColor = System.Drawing.Color.AliceBlue;
            this.panelResponseTime.Appearance.Options.UseBackColor = true;
            this.panelResponseTime.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelResponseTime.Controls.Add(this.lblValueResTime);
            this.panelResponseTime.Controls.Add(this.lblTitleResTime);
            this.panelResponseTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResponseTime.Location = new System.Drawing.Point(981, 3);
            this.panelResponseTime.Name = "panelResponseTime";
            this.panelResponseTime.Size = new System.Drawing.Size(323, 86);
            this.panelResponseTime.TabIndex = 3;
            // 
            // lblValueResTime
            // 
            this.lblValueResTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValueResTime.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F);
            this.lblValueResTime.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblValueResTime.Appearance.Options.UseFont = true;
            this.lblValueResTime.Appearance.Options.UseForeColor = true;
            this.lblValueResTime.Location = new System.Drawing.Point(234, 35);
            this.lblValueResTime.Name = "lblValueResTime";
            this.lblValueResTime.Size = new System.Drawing.Size(86, 48);
            this.lblValueResTime.TabIndex = 4;
            this.lblValueResTime.Text = "-";
            // 
            // lblTitleResTime
            // 
            this.lblTitleResTime.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 24F);
            this.lblTitleResTime.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleResTime.Appearance.Options.UseFont = true;
            this.lblTitleResTime.Appearance.Options.UseForeColor = true;
            this.lblTitleResTime.Location = new System.Drawing.Point(3, 3);
            this.lblTitleResTime.Name = "lblTitleResTime";
            this.lblTitleResTime.Size = new System.Drawing.Size(214, 38);
            this.lblTitleResTime.TabIndex = 4;
            this.lblTitleResTime.Text = "평균 응답시간⏰";
            // 
            // panelSatis
            // 
            this.panelSatis.Appearance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelSatis.Appearance.Options.UseBackColor = true;
            this.panelSatis.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelSatis.Controls.Add(this.lblValueSatis);
            this.panelSatis.Controls.Add(this.lblTitleSatis);
            this.panelSatis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSatis.Location = new System.Drawing.Point(655, 3);
            this.panelSatis.Name = "panelSatis";
            this.panelSatis.Size = new System.Drawing.Size(320, 86);
            this.panelSatis.TabIndex = 2;
            // 
            // lblValueSatis
            // 
            this.lblValueSatis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValueSatis.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F);
            this.lblValueSatis.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblValueSatis.Appearance.Options.UseFont = true;
            this.lblValueSatis.Appearance.Options.UseForeColor = true;
            this.lblValueSatis.Location = new System.Drawing.Point(222, 35);
            this.lblValueSatis.Name = "lblValueSatis";
            this.lblValueSatis.Size = new System.Drawing.Size(94, 48);
            this.lblValueSatis.TabIndex = 3;
            this.lblValueSatis.Text = "-";
            // 
            // lblTitleSatis
            // 
            this.lblTitleSatis.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 24F);
            this.lblTitleSatis.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblTitleSatis.Appearance.Options.UseFont = true;
            this.lblTitleSatis.Appearance.Options.UseForeColor = true;
            this.lblTitleSatis.Location = new System.Drawing.Point(4, 3);
            this.lblTitleSatis.Name = "lblTitleSatis";
            this.lblTitleSatis.Size = new System.Drawing.Size(158, 38);
            this.lblTitleSatis.TabIndex = 3;
            this.lblTitleSatis.Text = "만족도📊";
            // 
            // panelDislike
            // 
            this.panelDislike.Appearance.BackColor = System.Drawing.Color.LavenderBlush;
            this.panelDislike.Appearance.Options.UseBackColor = true;
            this.panelDislike.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelDislike.Controls.Add(this.lblValueDislike);
            this.panelDislike.Controls.Add(this.lblTitleDislike);
            this.panelDislike.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDislike.Location = new System.Drawing.Point(329, 3);
            this.panelDislike.Name = "panelDislike";
            this.panelDislike.Size = new System.Drawing.Size(320, 86);
            this.panelDislike.TabIndex = 1;
            // 
            // lblValueDislike
            // 
            this.lblValueDislike.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValueDislike.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F);
            this.lblValueDislike.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblValueDislike.Appearance.Options.UseFont = true;
            this.lblValueDislike.Appearance.Options.UseForeColor = true;
            this.lblValueDislike.Location = new System.Drawing.Point(265, 35);
            this.lblValueDislike.Name = "lblValueDislike";
            this.lblValueDislike.Size = new System.Drawing.Size(51, 48);
            this.lblValueDislike.TabIndex = 2;
            this.lblValueDislike.Text = "-";
            // 
            // lblTitleDislike
            // 
            this.lblTitleDislike.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 24F);
            this.lblTitleDislike.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitleDislike.Appearance.Options.UseFont = true;
            this.lblTitleDislike.Appearance.Options.UseForeColor = true;
            this.lblTitleDislike.Location = new System.Drawing.Point(3, 3);
            this.lblTitleDislike.Name = "lblTitleDislike";
            this.lblTitleDislike.Size = new System.Drawing.Size(122, 38);
            this.lblTitleDislike.TabIndex = 2;
            this.lblTitleDislike.Text = "싫어요👎";
            // 
            // panelLike
            // 
            this.panelLike.Appearance.BackColor = System.Drawing.Color.Honeydew;
            this.panelLike.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panelLike.Appearance.Options.UseBackColor = true;
            this.panelLike.Appearance.Options.UseBorderColor = true;
            this.panelLike.Appearance.Options.UseFont = true;
            this.panelLike.Appearance.Options.UseForeColor = true;
            this.panelLike.Appearance.Options.UseTextOptions = true;
            this.panelLike.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelLike.Controls.Add(this.lblValueLike);
            this.panelLike.Controls.Add(this.lblTitleLike);
            this.panelLike.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLike.Location = new System.Drawing.Point(3, 3);
            this.panelLike.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelLike.Name = "panelLike";
            this.panelLike.Size = new System.Drawing.Size(320, 86);
            this.panelLike.TabIndex = 0;
            // 
            // lblValueLike
            // 
            this.lblValueLike.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValueLike.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F);
            this.lblValueLike.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblValueLike.Appearance.Options.UseFont = true;
            this.lblValueLike.Appearance.Options.UseForeColor = true;
            this.lblValueLike.Location = new System.Drawing.Point(250, 35);
            this.lblValueLike.Name = "lblValueLike";
            this.lblValueLike.Size = new System.Drawing.Size(67, 48);
            this.lblValueLike.TabIndex = 1;
            this.lblValueLike.Text = "-";
            // 
            // lblTitleLike
            // 
            this.lblTitleLike.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblTitleLike.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 24F);
            this.lblTitleLike.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblTitleLike.Appearance.Options.UseBorderColor = true;
            this.lblTitleLike.Appearance.Options.UseFont = true;
            this.lblTitleLike.Appearance.Options.UseForeColor = true;
            this.lblTitleLike.Location = new System.Drawing.Point(1, 3);
            this.lblTitleLike.Name = "lblTitleLike";
            this.lblTitleLike.Size = new System.Drawing.Size(122, 38);
            this.lblTitleLike.TabIndex = 0;
            this.lblTitleLike.Text = "좋아요👍";
            // 
            // Gc_List
            // 
            this.Gc_List.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.Gc_List.Location = new System.Drawing.Point(3, 101);
            this.Gc_List.MainView = this.Gv_List;
            this.Gc_List.Name = "Gc_List";
            this.Gc_List.Size = new System.Drawing.Size(1307, 378);
            this.Gc_List.TabIndex = 101;
            this.Gc_List.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_List});
            // 
            // Gv_List
            // 
            this.Gv_List.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.Gv_List.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Gv_List.ColumnPanelRowHeight = 35;
            this.Gv_List.DetailHeight = 497;
            this.Gv_List.GridControl = this.Gc_List;
            this.Gv_List.Name = "Gv_List";
            this.Gv_List.OptionsView.ShowGroupPanel = false;
            this.Gv_List.RowHeight = 35;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.simpleSeparator1,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1313, 483);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.Gc_List;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 98);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1313, 384);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 482);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(1313, 1);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 98);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(7, 98);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1313, 98);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Frm_Chat_ResponseQuality
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 523);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Chat_ResponseQuality";
            this.Text = "응답품질평가";
            this.Load += new System.EventHandler(this.Frm_CM_DefectCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.tlpKpiCards.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelResponseTime)).EndInit();
            this.panelResponseTime.ResumeLayout(false);
            this.panelResponseTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSatis)).EndInit();
            this.panelSatis.ResumeLayout(false);
            this.panelSatis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelDislike)).EndInit();
            this.panelDislike.ResumeLayout(false);
            this.panelDislike.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelLike)).EndInit();
            this.panelLike.ResumeLayout(false);
            this.panelLike.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl Gc_List;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_List;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchLookUp LupMatch;
        private baseSMT.smtSearchLookUp LupFeedback;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton cmdExcel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.TableLayoutPanel tlpKpiCards;
        private DevExpress.XtraEditors.PanelControl panelResponseTime;
        private DevExpress.XtraEditors.PanelControl panelSatis;
        private DevExpress.XtraEditors.PanelControl panelDislike;
        private DevExpress.XtraEditors.PanelControl panelLike;
        private DevExpress.XtraEditors.LabelControl lblTitleLike;
        private DevExpress.XtraEditors.LabelControl lblValueLike;
        private DevExpress.XtraEditors.LabelControl lblValueSatis;
        private DevExpress.XtraEditors.LabelControl lblTitleSatis;
        private DevExpress.XtraEditors.LabelControl lblValueDislike;
        private DevExpress.XtraEditors.LabelControl lblTitleDislike;
        private DevExpress.XtraEditors.LabelControl lblValueResTime;
        private DevExpress.XtraEditors.LabelControl lblTitleResTime;
    }
}