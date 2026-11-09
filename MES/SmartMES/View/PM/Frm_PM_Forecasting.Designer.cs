namespace SmartMES.View.PM
{
    partial class Frm_PM_Forecasting
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PM_Forecasting));
            this.commonBar = new baseSMT.smtCommonBar();
            this.registerButton = new baseSMT.smtSearchButton();
            this.deleteButton = new baseSMT.smtSearchButton();
            this.saveButton = new baseSMT.smtSearchButton();
            this.confirmButton = new baseSMT.smtSearchButton();
            this.cancelConfirmButton = new baseSMT.smtSearchButton();
            this.templateButton = new baseSMT.smtSearchButton();
            this.uploadButton = new baseSMT.smtSearchButton();
            this.closeButton = new baseSMT.smtSearchButton();
            this.body = new System.Windows.Forms.TableLayoutPanel();
            this.filters = new System.Windows.Forms.FlowLayoutPanel();
            this.monthLabel = new DevExpress.XtraEditors.LabelControl();
            this.baseMonth = new DevExpress.XtraEditors.DateEdit();
            this.periodLabel = new DevExpress.XtraEditors.LabelControl();
            this.periodMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.salesLabel = new DevExpress.XtraEditors.LabelControl();
            this.salesFilter = new DevExpress.XtraEditors.ComboBoxEdit();
            this.itemLabel = new DevExpress.XtraEditors.LabelControl();
            this.itemSearch = new DevExpress.XtraEditors.TextEdit();
            this.searchButton = new baseSMT.smtSearchButton();
            this.cards = new System.Windows.Forms.TableLayoutPanel();
            this.card1 = new DevExpress.XtraEditors.PanelControl();
            this.cardIcon1 = new DevExpress.XtraEditors.SvgImageBox();
            this.cardTitle1 = new DevExpress.XtraEditors.LabelControl();
            this.cardValue1 = new DevExpress.XtraEditors.LabelControl();
            this.card2 = new DevExpress.XtraEditors.PanelControl();
            this.cardIcon2 = new DevExpress.XtraEditors.SvgImageBox();
            this.cardTitle2 = new DevExpress.XtraEditors.LabelControl();
            this.cardValue2 = new DevExpress.XtraEditors.LabelControl();
            this.card3 = new DevExpress.XtraEditors.PanelControl();
            this.cardIcon3 = new DevExpress.XtraEditors.SvgImageBox();
            this.cardTitle3 = new DevExpress.XtraEditors.LabelControl();
            this.cardValue3 = new DevExpress.XtraEditors.LabelControl();
            this.listGroup = new DevExpress.XtraEditors.GroupControl();
            this.forecastGrid = new DevExpress.XtraGrid.GridControl();
            this.forecastView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.commonBar)).BeginInit();
            this.commonBar.SuspendLayout();
            this.body.SuspendLayout();
            this.filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseMonth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSearch.Properties)).BeginInit();
            this.cards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.card1)).BeginInit();
            this.card1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardIcon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card2)).BeginInit();
            this.card2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardIcon2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card3)).BeginInit();
            this.card3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardIcon3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listGroup)).BeginInit();
            this.listGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.forecastGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forecastView)).BeginInit();
            this.SuspendLayout();
            // 
            // commonBar
            // 
            this.commonBar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.commonBar.Appearance.Options.UseBackColor = true;
            this.commonBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.commonBar.Controls.Add(this.searchButton);
            this.commonBar.Controls.Add(this.registerButton);
            this.commonBar.Controls.Add(this.deleteButton);
            this.commonBar.Controls.Add(this.saveButton);
            this.commonBar.Controls.Add(this.confirmButton);
            this.commonBar.Controls.Add(this.cancelConfirmButton);
            this.commonBar.Controls.Add(this.templateButton);
            this.commonBar.Controls.Add(this.uploadButton);
            this.commonBar.Controls.Add(this.closeButton);
            this.commonBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.commonBar.Location = new System.Drawing.Point(0, 0);
            this.commonBar.Name = "commonBar";
            this.commonBar.Size = new System.Drawing.Size(1440, 40);
            this.commonBar.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.commonBar.smtImageTitleAutoText = false;
            this.commonBar.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("commonBar.smtImageTitleSvgImage")));
            this.commonBar.smtImageTitleText = "포케스팅 관리";
            this.commonBar.smtImageTitleVisible = true;
            this.commonBar.TabIndex = 1;
            // 
            // registerButton
            // 
            this.registerButton.AllowFocus = false;
            this.registerButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.registerButton.Appearance.Options.UseForeColor = true;
            this.registerButton.AutoSize = true;
            this.registerButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.registerButton.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.registerButton.ImageOptions.ImageIndex = 1;
            this.registerButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.registerButton.Location = new System.Drawing.Point(718, 0);
            this.registerButton.Name = "registerButton";
            this.registerButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.registerButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.registerButton.Size = new System.Drawing.Size(76, 40);
            this.registerButton.smtButtonAutoText = false;
            this.registerButton.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.registerButton.TabIndex = 1;
            this.registerButton.Text = "등록";
            this.registerButton.ToolTip = "화면 시안: 업무 기능 미연결";
            // 
            // deleteButton
            // 
            this.deleteButton.AllowFocus = false;
            this.deleteButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Appearance.Options.UseForeColor = true;
            this.deleteButton.AutoSize = true;
            this.deleteButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.deleteButton.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.deleteButton.ImageOptions.ImageIndex = 3;
            this.deleteButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.deleteButton.Location = new System.Drawing.Point(794, 0);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.deleteButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.deleteButton.Size = new System.Drawing.Size(76, 40);
            this.deleteButton.smtButtonAutoText = false;
            this.deleteButton.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "삭제";
            this.deleteButton.ToolTip = "화면 시안: 업무 기능 미연결";
            // 
            // saveButton
            // 
            this.saveButton.AllowFocus = false;
            this.saveButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.saveButton.Appearance.Options.UseForeColor = true;
            this.saveButton.AutoSize = true;
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.saveButton.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.saveButton.ImageOptions.ImageIndex = 4;
            this.saveButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.saveButton.Location = new System.Drawing.Point(870, 0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.saveButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.saveButton.Size = new System.Drawing.Size(76, 40);
            this.saveButton.smtButtonAutoText = false;
            this.saveButton.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "저장";
            this.saveButton.ToolTip = "화면 시안: 업무 기능 미연결";
            // 
            // confirmButton
            // 
            this.confirmButton.AllowFocus = false;
            this.confirmButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.confirmButton.Appearance.Options.UseForeColor = true;
            this.confirmButton.AutoSize = true;
            this.confirmButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.confirmButton.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.confirmButton.ImageOptions.ImageIndex = 6;
            this.confirmButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.confirmButton.Location = new System.Drawing.Point(946, 0);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.confirmButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.confirmButton.Size = new System.Drawing.Size(76, 40);
            this.confirmButton.smtButtonAutoText = false;
            this.confirmButton.smtButtonStyle = baseSMT.EnumButtonStyles.확정;
            this.confirmButton.TabIndex = 4;
            this.confirmButton.Text = "확정";
            this.confirmButton.ToolTip = "화면 시안: 업무 기능 미연결";
            // 
            // cancelConfirmButton
            // 
            this.cancelConfirmButton.AllowFocus = false;
            this.cancelConfirmButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.cancelConfirmButton.Appearance.Options.UseForeColor = true;
            this.cancelConfirmButton.AutoSize = true;
            this.cancelConfirmButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.cancelConfirmButton.Enabled = false;
            this.cancelConfirmButton.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cancelConfirmButton.ImageOptions.ImageIndex = 7;
            this.cancelConfirmButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.cancelConfirmButton.Location = new System.Drawing.Point(1022, 0);
            this.cancelConfirmButton.Name = "cancelConfirmButton";
            this.cancelConfirmButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cancelConfirmButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cancelConfirmButton.Size = new System.Drawing.Size(100, 40);
            this.cancelConfirmButton.smtButtonAutoText = false;
            this.cancelConfirmButton.smtButtonStyle = baseSMT.EnumButtonStyles.확정취소;
            this.cancelConfirmButton.TabIndex = 5;
            this.cancelConfirmButton.Text = "확정취소";
            this.cancelConfirmButton.ToolTip = "화면 시안: 업무 기능 미연결";
            // 
            // templateButton
            // 
            this.templateButton.AllowFocus = false;
            this.templateButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.templateButton.Appearance.Options.UseForeColor = true;
            this.templateButton.AutoSize = true;
            this.templateButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.templateButton.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.templateButton.ImageOptions.ImageIndex = 9;
            this.templateButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.templateButton.Location = new System.Drawing.Point(1122, 0);
            this.templateButton.Name = "templateButton";
            this.templateButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.templateButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.templateButton.Size = new System.Drawing.Size(127, 40);
            this.templateButton.smtButtonAutoText = false;
            this.templateButton.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.templateButton.TabIndex = 6;
            this.templateButton.Text = "양식 다운로드";
            this.templateButton.ToolTip = "화면 시안: 업무 기능 미연결";
            // 
            // uploadButton
            // 
            this.uploadButton.AllowFocus = false;
            this.uploadButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.uploadButton.Appearance.Options.UseForeColor = true;
            this.uploadButton.AutoSize = true;
            this.uploadButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.uploadButton.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.uploadButton.ImageOptions.ImageIndex = 8;
            this.uploadButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.uploadButton.Location = new System.Drawing.Point(1249, 0);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.uploadButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.uploadButton.Size = new System.Drawing.Size(115, 40);
            this.uploadButton.smtButtonAutoText = false;
            this.uploadButton.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀업로드;
            this.uploadButton.TabIndex = 7;
            this.uploadButton.Text = "엑셀 업로드";
            this.uploadButton.ToolTip = "화면 시안: 업무 기능 미연결";
            // 
            // closeButton
            // 
            this.closeButton.AllowFocus = false;
            this.closeButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.closeButton.Appearance.Options.UseForeColor = true;
            this.closeButton.AutoSize = true;
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeButton.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.closeButton.ImageOptions.ImageIndex = 5;
            this.closeButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.closeButton.Location = new System.Drawing.Point(1364, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.closeButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.closeButton.Size = new System.Drawing.Size(76, 40);
            this.closeButton.smtButtonAutoText = false;
            this.closeButton.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "닫기";
            this.closeButton.ToolTip = "닫기";
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // body
            // 
            this.body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(248)))));
            this.body.ColumnCount = 1;
            this.body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.body.Controls.Add(this.filters, 0, 0);
            this.body.Controls.Add(this.cards, 0, 1);
            this.body.Controls.Add(this.listGroup, 0, 2);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(0, 40);
            this.body.Name = "body";
            this.body.Padding = new System.Windows.Forms.Padding(12);
            this.body.RowCount = 3;
            this.body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.body.Size = new System.Drawing.Size(1440, 820);
            this.body.TabIndex = 0;
            // 
            // filters
            // 
            this.filters.Controls.Add(this.monthLabel);
            this.filters.Controls.Add(this.baseMonth);
            this.filters.Controls.Add(this.periodLabel);
            this.filters.Controls.Add(this.periodMode);
            this.filters.Controls.Add(this.salesLabel);
            this.filters.Controls.Add(this.salesFilter);
            this.filters.Controls.Add(this.itemLabel);
            this.filters.Controls.Add(this.itemSearch);
            this.filters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filters.Location = new System.Drawing.Point(15, 23);
            this.filters.Name = "filters";
            this.filters.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.filters.Margin = new System.Windows.Forms.Padding(0);
            this.filters.Size = new System.Drawing.Size(1410, 76);
            this.filters.TabIndex = 1;
            this.filters.WrapContents = false;
            // 
            // monthLabel
            // 
            this.monthLabel.Location = new System.Drawing.Point(8, 16);
            this.monthLabel.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.monthLabel.Name = "monthLabel";
            this.monthLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.monthLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.monthLabel.Appearance.Options.UseTextOptions = true;
            this.monthLabel.Size = new System.Drawing.Size(44, 32);
            this.monthLabel.TabIndex = 0;
            this.monthLabel.Text = "기준월";
            // 
            // baseMonth
            // 
            this.baseMonth.EditValue = new System.DateTime(2026, 11, 1, 0, 0, 0, 0);
            this.baseMonth.Location = new System.Drawing.Point(55, 11);
            this.baseMonth.Name = "baseMonth";
            this.baseMonth.Properties.AutoHeight = false;
            this.baseMonth.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.baseMonth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.baseMonth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.baseMonth.Properties.DisplayFormat.FormatString = "yyyy년 MM월";
            this.baseMonth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.baseMonth.Properties.EditFormat.FormatString = "yyyy-MM";
            this.baseMonth.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.baseMonth.Properties.Mask.EditMask = "yyyy년 MM월";
            this.baseMonth.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.baseMonth.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.baseMonth.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.baseMonth.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearView;
            this.baseMonth.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView | DevExpress.XtraEditors.VistaCalendarViewStyle.CenturyView;
            this.baseMonth.Size = new System.Drawing.Size(140, 32);
            this.baseMonth.TabIndex = 1;
            // 
            // periodLabel
            // 
            this.periodLabel.Location = new System.Drawing.Point(206, 16);
            this.periodLabel.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.periodLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.periodLabel.Appearance.Options.UseTextOptions = true;
            this.periodLabel.Size = new System.Drawing.Size(56, 32);
            this.periodLabel.TabIndex = 2;
            this.periodLabel.Text = "관리단위";
            // 
            // periodMode
            // 
            this.periodMode.EditValue = "주별";
            this.periodMode.Location = new System.Drawing.Point(265, 11);
            this.periodMode.Name = "periodMode";
            this.periodMode.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.periodMode.Properties.AutoHeight = false;
            this.periodMode.Properties.Items.AddRange(new object[] { "월별", "주별", "일별" });
            this.periodMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.periodMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.periodMode.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.periodMode.Size = new System.Drawing.Size(100, 32);
            this.periodMode.TabIndex = 3;
            this.periodMode.EditValueChanged += new System.EventHandler(this.PeriodMode_EditValueChanged);
            // 
            // salesLabel
            // 
            this.salesLabel.Location = new System.Drawing.Point(426, 16);
            this.salesLabel.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.salesLabel.Name = "salesLabel";
            this.salesLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.salesLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.salesLabel.Appearance.Options.UseTextOptions = true;
            this.salesLabel.Size = new System.Drawing.Size(56, 32);
            this.salesLabel.TabIndex = 4;
            this.salesLabel.Text = "판매구분";
            // 
            // salesFilter
            // 
            this.salesFilter.EditValue = "전체";
            this.salesFilter.Location = new System.Drawing.Point(485, 11);
            this.salesFilter.Name = "salesFilter";
            this.salesFilter.Properties.AutoHeight = false;
            this.salesFilter.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.salesFilter.Properties.Items.AddRange(new object[] {
            "전체",
            "B2B",
            "B2C"});
            this.salesFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.salesFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.salesFilter.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
            this.salesFilter.Size = new System.Drawing.Size(100, 32);
            this.salesFilter.TabIndex = 5;
            // 
            // itemLabel
            // 
            this.itemLabel.Location = new System.Drawing.Point(596, 16);
            this.itemLabel.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.itemLabel.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.itemLabel.Appearance.Options.UseTextOptions = true;
            this.itemLabel.Size = new System.Drawing.Size(32, 32);
            this.itemLabel.TabIndex = 6;
            this.itemLabel.Text = "품목";
            // 
            // itemSearch
            // 
            this.itemSearch.Location = new System.Drawing.Point(631, 11);
            this.itemSearch.Name = "itemSearch";
            this.itemSearch.Properties.AutoHeight = false;
            this.itemSearch.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.itemSearch.Properties.NullValuePrompt = "품목코드 또는 품목명";
            this.itemSearch.Size = new System.Drawing.Size(200, 32);
            this.itemSearch.TabIndex = 7;
            // 
            // searchButton
            // 
            this.searchButton.AllowFocus = false;
            this.searchButton.Appearance.ForeColor = System.Drawing.Color.White;
            this.searchButton.Appearance.Options.UseForeColor = true;
            this.searchButton.AutoSize = true;
            this.searchButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchButton.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.searchButton.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.searchButton.ImageOptions.ImageIndex = 0;
            this.searchButton.Location = new System.Drawing.Point(837, 11);
            this.searchButton.Name = "searchButton";
            this.searchButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.searchButton.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.searchButton.Size = new System.Drawing.Size(76, 40);
            this.searchButton.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.searchButton.TabIndex = 8;
            this.searchButton.Text = "조회";
            this.searchButton.ToolTip = "판매구분과 품목으로 예시 목록 조회 (Ctrl+Q)";
            // 
            // cards
            // 
            this.cards.ColumnCount = 3;
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.cards.Controls.Add(this.card1, 0, 0);
            this.cards.Controls.Add(this.card2, 1, 0);
            this.cards.Controls.Add(this.card3, 2, 0);
            this.cards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cards.Location = new System.Drawing.Point(12, 102);
            this.cards.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.cards.Name = "cards";
            this.cards.RowCount = 1;
            this.cards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.cards.Size = new System.Drawing.Size(1416, 94);
            this.cards.TabIndex = 2;
            // 
            // card1
            // 
            this.card1.Controls.Add(this.cardIcon1);
            this.card1.Controls.Add(this.cardTitle1);
            this.card1.Controls.Add(this.cardValue1);
            this.card1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card1.Location = new System.Drawing.Point(0, 0);
            this.card1.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(462, 94);
            this.card1.TabIndex = 0;
            // 
            // cardIcon1
            // 
            this.cardIcon1.Location = new System.Drawing.Point(16, 20);
            this.cardIcon1.Name = "cardIcon1";
            this.cardIcon1.Size = new System.Drawing.Size(52, 52);
            this.cardIcon1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cardIcon1.SvgImage")));
            this.cardIcon1.TabIndex = 0;
            // 
            // cardTitle1
            // 
            this.cardTitle1.Location = new System.Drawing.Point(84, 16);
            this.cardTitle1.Name = "cardTitle1";
            this.cardTitle1.Size = new System.Drawing.Size(63, 17);
            this.cardTitle1.TabIndex = 1;
            this.cardTitle1.Text = "월 예상수량";
            // 
            // cardValue1
            // 
            this.cardValue1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 23F, System.Drawing.FontStyle.Bold);
            this.cardValue1.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cardValue1.Appearance.Options.UseFont = true;
            this.cardValue1.Appearance.Options.UseForeColor = true;
            this.cardValue1.Location = new System.Drawing.Point(80, 38);
            this.cardValue1.Name = "cardValue1";
            this.cardValue1.Size = new System.Drawing.Size(143, 37);
            this.cardValue1.TabIndex = 2;
            this.cardValue1.Text = "11,600 EA";
            // 
            // card2
            // 
            this.card2.Controls.Add(this.cardIcon2);
            this.card2.Controls.Add(this.cardTitle2);
            this.card2.Controls.Add(this.cardValue2);
            this.card2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card2.Location = new System.Drawing.Point(472, 0);
            this.card2.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(462, 94);
            this.card2.TabIndex = 1;
            // 
            // cardIcon2
            // 
            this.cardIcon2.Location = new System.Drawing.Point(16, 20);
            this.cardIcon2.Name = "cardIcon2";
            this.cardIcon2.Size = new System.Drawing.Size(52, 52);
            this.cardIcon2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cardIcon2.SvgImage")));
            this.cardIcon2.TabIndex = 0;
            // 
            // cardTitle2
            // 
            this.cardTitle2.Location = new System.Drawing.Point(84, 16);
            this.cardTitle2.Name = "cardTitle2";
            this.cardTitle2.Size = new System.Drawing.Size(77, 17);
            this.cardTitle2.TabIndex = 1;
            this.cardTitle2.Text = "B2B 예상수량";
            // 
            // cardValue2
            // 
            this.cardValue2.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 23F, System.Drawing.FontStyle.Bold);
            this.cardValue2.Appearance.ForeColor = System.Drawing.Color.SeaGreen;
            this.cardValue2.Appearance.Options.UseFont = true;
            this.cardValue2.Appearance.Options.UseForeColor = true;
            this.cardValue2.Location = new System.Drawing.Point(80, 38);
            this.cardValue2.Name = "cardValue2";
            this.cardValue2.Size = new System.Drawing.Size(135, 37);
            this.cardValue2.TabIndex = 2;
            this.cardValue2.Text = "6,000 EA";
            // 
            // card3
            // 
            this.card3.Controls.Add(this.cardIcon3);
            this.card3.Controls.Add(this.cardTitle3);
            this.card3.Controls.Add(this.cardValue3);
            this.card3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card3.Location = new System.Drawing.Point(944, 0);
            this.card3.Margin = new System.Windows.Forms.Padding(0);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(472, 94);
            this.card3.TabIndex = 2;
            // 
            // cardIcon3
            // 
            this.cardIcon3.Location = new System.Drawing.Point(16, 20);
            this.cardIcon3.Name = "cardIcon3";
            this.cardIcon3.Size = new System.Drawing.Size(52, 52);
            this.cardIcon3.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("cardIcon3.SvgImage")));
            this.cardIcon3.TabIndex = 0;
            // 
            // cardTitle3
            // 
            this.cardTitle3.Location = new System.Drawing.Point(84, 16);
            this.cardTitle3.Name = "cardTitle3";
            this.cardTitle3.Size = new System.Drawing.Size(78, 17);
            this.cardTitle3.TabIndex = 1;
            this.cardTitle3.Text = "B2C 예상수량";
            // 
            // cardValue3
            // 
            this.cardValue3.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 23F, System.Drawing.FontStyle.Bold);
            this.cardValue3.Appearance.ForeColor = System.Drawing.Color.DarkOrange;
            this.cardValue3.Appearance.Options.UseFont = true;
            this.cardValue3.Appearance.Options.UseForeColor = true;
            this.cardValue3.Location = new System.Drawing.Point(80, 38);
            this.cardValue3.Name = "cardValue3";
            this.cardValue3.Size = new System.Drawing.Size(134, 37);
            this.cardValue3.TabIndex = 2;
            this.cardValue3.Text = "5,600 EA";
            // 
            // listGroup
            // 
            this.listGroup.Controls.Add(this.forecastGrid);
            this.listGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listGroup.Location = new System.Drawing.Point(15, 211);
            this.listGroup.Name = "listGroup";
            this.listGroup.Padding = new System.Windows.Forms.Padding(4);
            this.listGroup.Size = new System.Drawing.Size(1410, 528);
            this.listGroup.TabIndex = 3;
            this.listGroup.Text = "판매 예상수량  ·  4개 항목";
            // 
            // forecastGrid
            // 
            this.forecastGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.forecastGrid.Location = new System.Drawing.Point(6, 33);
            this.forecastGrid.MainView = this.forecastView;
            this.forecastGrid.Name = "forecastGrid";
            this.forecastGrid.Size = new System.Drawing.Size(1398, 489);
            this.forecastGrid.TabIndex = 0;
            this.forecastGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.forecastView});
            // 
            // forecastView
            // 
            this.forecastView.Appearance.FooterPanel.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
            this.forecastView.Appearance.FooterPanel.Options.UseFont = true;
            this.forecastView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Pretendard SemiBold", 10F);
            this.forecastView.Appearance.HeaderPanel.Options.UseFont = true;
            this.forecastView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.forecastView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.forecastView.Appearance.Row.Font = new System.Drawing.Font("Pretendard SemiBold", 10F);
            this.forecastView.Appearance.Row.Options.UseFont = true;
            this.forecastView.ColumnPanelRowHeight = 36;
            this.forecastView.GridControl = this.forecastGrid;
            this.forecastView.IndicatorWidth = 45;
            this.forecastView.Name = "forecastView";
            this.forecastView.OptionsView.ColumnAutoWidth = true;
            this.forecastView.FooterPanelHeight = 36;
            this.forecastView.OptionsView.ShowFooter = true;
            this.forecastView.OptionsView.ShowGroupPanel = false;
            this.forecastView.RowHeight = 36;
            // 
            // Frm_PM_Forecasting
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 860);
            this.Controls.Add(this.body);
            this.Controls.Add(this.commonBar);
            this.Font = new System.Drawing.Font("Pretendard SemiBold", 10F);
            this.MinimumSize = new System.Drawing.Size(1120, 640);
            this.Name = "Frm_PM_Forecasting";
            this.Text = "포케스팅 관리";
            ((System.ComponentModel.ISupportInitialize)(this.commonBar)).EndInit();
            this.commonBar.ResumeLayout(false);
            this.commonBar.PerformLayout();
            this.body.ResumeLayout(false);
            this.filters.ResumeLayout(false);
            this.filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.baseMonth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salesFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemSearch.Properties)).EndInit();
            this.cards.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.card1)).EndInit();
            this.card1.ResumeLayout(false);
            this.card1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardIcon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card2)).EndInit();
            this.card2.ResumeLayout(false);
            this.card2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardIcon2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card3)).EndInit();
            this.card3.ResumeLayout(false);
            this.card3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardIcon3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listGroup)).EndInit();
            this.listGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.forecastGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forecastView)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private baseSMT.smtCommonBar commonBar;
        private System.Windows.Forms.TableLayoutPanel body;
        private System.Windows.Forms.FlowLayoutPanel filters;
        private DevExpress.XtraEditors.LabelControl monthLabel;
        private DevExpress.XtraEditors.DateEdit baseMonth;
        private DevExpress.XtraEditors.LabelControl periodLabel;
        private DevExpress.XtraEditors.ComboBoxEdit periodMode;
        private DevExpress.XtraEditors.LabelControl salesLabel;
        private DevExpress.XtraEditors.ComboBoxEdit salesFilter;
        private DevExpress.XtraEditors.LabelControl itemLabel;
        private DevExpress.XtraEditors.TextEdit itemSearch;
        private baseSMT.smtSearchButton searchButton;
        private System.Windows.Forms.TableLayoutPanel cards;
        private DevExpress.XtraEditors.PanelControl card1;
        private DevExpress.XtraEditors.SvgImageBox cardIcon1;
        private DevExpress.XtraEditors.LabelControl cardTitle1;
        private DevExpress.XtraEditors.LabelControl cardValue1;
        private DevExpress.XtraEditors.PanelControl card2;
        private DevExpress.XtraEditors.SvgImageBox cardIcon2;
        private DevExpress.XtraEditors.LabelControl cardTitle2;
        private DevExpress.XtraEditors.LabelControl cardValue2;
        private DevExpress.XtraEditors.PanelControl card3;
        private DevExpress.XtraEditors.SvgImageBox cardIcon3;
        private DevExpress.XtraEditors.LabelControl cardTitle3;
        private DevExpress.XtraEditors.LabelControl cardValue3;
        private DevExpress.XtraEditors.GroupControl listGroup;
        private DevExpress.XtraGrid.GridControl forecastGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView forecastView;
        private baseSMT.smtSearchButton registerButton;
        private baseSMT.smtSearchButton deleteButton;
        private baseSMT.smtSearchButton saveButton;
        private baseSMT.smtSearchButton confirmButton;
        private baseSMT.smtSearchButton cancelConfirmButton;
        private baseSMT.smtSearchButton templateButton;
        private baseSMT.smtSearchButton uploadButton;
        private baseSMT.smtSearchButton closeButton;
    }
}
