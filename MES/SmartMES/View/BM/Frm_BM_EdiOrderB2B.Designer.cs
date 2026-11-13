using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using baseSMT;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace SmartMES.View.BM
{
    partial class Frm_BM_EdiOrderB2B
    {
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BM_EdiOrderB2B));
            this.bar = new baseSMT.smtCommonBar();
            this.registrationFields = new System.Windows.Forms.TableLayoutPanel();
            this.lblOrderDate = new DevExpress.XtraEditors.LabelControl();
            this.orderDate = new DevExpress.XtraEditors.DateEdit();
            this.lblCustomerSearch = new DevExpress.XtraEditors.LabelControl();
            this.customerSearch = new DevExpress.XtraEditors.TextEdit();
            this.customerType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblCustomerType = new DevExpress.XtraEditors.LabelControl();
            this.cmdSearch = new baseSMT.smtSearchButton();
            this.cmdApply = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.cmdAdd = new baseSMT.smtSearchButton();
            this.cmdUpload = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.body = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupCustomers = new DevExpress.XtraEditors.GroupControl();
            this.gcCustomers = new DevExpress.XtraGrid.GridControl();
            this.gvCustomers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.details = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupOrders = new DevExpress.XtraEditors.GroupControl();
            this.gcOrders = new DevExpress.XtraGrid.GridControl();
            this.gvOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupEdiDetails = new DevExpress.XtraEditors.GroupControl();
            this.gcEdiDetails = new DevExpress.XtraGrid.GridControl();
            this.gvEdiDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).BeginInit();
            this.bar.SuspendLayout();
            this.registrationFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.body)).BeginInit();
            this.body.Panel1.SuspendLayout();
            this.body.Panel2.SuspendLayout();
            this.body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomers)).BeginInit();
            this.groupCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.details)).BeginInit();
            this.details.Panel1.SuspendLayout();
            this.details.Panel2.SuspendLayout();
            this.details.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupOrders)).BeginInit();
            this.groupOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupEdiDetails)).BeginInit();
            this.groupEdiDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcEdiDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEdiDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // bar
            // 
            this.bar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.bar.Appearance.Options.UseBackColor = true;
            this.bar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.bar.Controls.Add(this.registrationFields);
            this.bar.Controls.Add(this.cmdApply);
            this.bar.Controls.Add(this.cmdDelete);
            this.bar.Controls.Add(this.cmdAdd);
            this.bar.Controls.Add(this.cmdUpload);
            this.bar.Controls.Add(this.cmdClose);
            this.bar.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar.Location = new System.Drawing.Point(0, 0);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(1440, 40);
            this.bar.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.bar.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bar.smtImageTitleSvgImage")));
            this.bar.smtImageTitleText = "EDI 수주 등록(B2B)";
            this.bar.smtImageTitleVisible = true;
            this.bar.TabIndex = 1;
            // 
            // registrationFields
            // 
            this.registrationFields.ColumnCount = 8;
            this.registrationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.registrationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.registrationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.registrationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.registrationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.registrationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
            this.registrationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.registrationFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.registrationFields.Controls.Add(this.lblOrderDate, 0, 0);
            this.registrationFields.Controls.Add(this.orderDate, 1, 0);
            this.registrationFields.Controls.Add(this.lblCustomerType, 2, 0);
            this.registrationFields.Controls.Add(this.customerType, 3, 0);
            // 거래처 분류 조회조건
            this.lblCustomerType.Name = "lblCustomerType";
            this.lblCustomerType.Text = "분류";
            this.lblCustomerType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCustomerType.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblCustomerType.Appearance.Options.UseForeColor = true;
            this.customerType.Name = "customerType";
            this.customerType.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.customerType.Properties.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo));
            this.customerType.Properties.Items.AddRange(new object[] { "전체", "B2B", "B2C" });
            this.customerType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.customerType.EditValue = "전체";
            this.customerType.TabIndex = 2;
            this.customerSearch.TabIndex = 3;
            this.registrationFields.Controls.Add(this.lblCustomerSearch, 4, 0);
            this.registrationFields.Controls.Add(this.customerSearch, 5, 0);
            this.registrationFields.Controls.Add(this.cmdSearch, 6, 0);
            this.registrationFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registrationFields.Location = new System.Drawing.Point(156, 0);
            this.registrationFields.Name = "registrationFields";
            this.registrationFields.Padding = new System.Windows.Forms.Padding(8, 0, 6, 0);
            this.registrationFields.RowCount = 1;
            this.registrationFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.registrationFields.Size = new System.Drawing.Size(857, 40);
            this.registrationFields.TabIndex = 1;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOrderDate.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblOrderDate.Appearance.Options.UseForeColor = true;
            this.lblOrderDate.Location = new System.Drawing.Point(11, 11);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(61, 17);
            this.lblOrderDate.TabIndex = 0;
            this.lblOrderDate.Text = "EDI 등록일";
            // 
            // orderDate
            // 
            this.orderDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.orderDate.EditValue = new System.DateTime(2026, 11, 11, 0, 0, 0, 0);
            this.orderDate.Location = new System.Drawing.Point(83, 10);
            this.orderDate.Name = "orderDate";
            this.orderDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.orderDate.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            this.orderDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.orderDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.orderDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.orderDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.orderDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.orderDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.orderDate.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView) 
            | DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView) 
            | DevExpress.XtraEditors.VistaCalendarViewStyle.CenturyView)));
            this.orderDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.orderDate.Size = new System.Drawing.Size(119, 32);
            this.orderDate.TabIndex = 1;
            // 
            // lblCustomerSearch
            // 
            this.lblCustomerSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCustomerSearch.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblCustomerSearch.Appearance.Options.UseForeColor = true;
            this.lblCustomerSearch.Location = new System.Drawing.Point(208, 11);
            this.lblCustomerSearch.Name = "lblCustomerSearch";
            this.lblCustomerSearch.Size = new System.Drawing.Size(36, 17);
            this.lblCustomerSearch.TabIndex = 2;
            this.lblCustomerSearch.Text = "거래처";
            // 
            // customerSearch
            // 
            this.customerSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.customerSearch.Location = new System.Drawing.Point(260, 10);
            this.customerSearch.Name = "customerSearch";
            this.customerSearch.Properties.NullValuePrompt = "거래처코드 또는 거래처명";
            this.customerSearch.Size = new System.Drawing.Size(199, 32);
            this.customerSearch.TabIndex = 3;
            // 
            // cmdSearch
            // 
            this.cmdSearch.AllowFocus = false;
            this.cmdSearch.AutoSize = true;
            this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdSearch.ImageOptions.ImageIndex = 0;
            this.cmdSearch.Location = new System.Drawing.Point(462, 0);
            this.cmdSearch.Margin = new System.Windows.Forms.Padding(0);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSearch.Size = new System.Drawing.Size(82, 40);
            this.cmdSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdSearch.TabIndex = 4;
            this.cmdSearch.Text = "조회";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // cmdApply
            // 
            this.cmdApply.AllowFocus = false;
            this.cmdApply.AutoSize = true;
            this.cmdApply.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdApply.ImageOptions.ImageIndex = 4;
            this.cmdApply.Location = new System.Drawing.Point(1013, 0);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdApply.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdApply.Size = new System.Drawing.Size(127, 40);
            this.cmdApply.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdApply.TabIndex = 2;
            this.cmdApply.Text = "연결 저장·반영";
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.AllowFocus = false;
            this.cmdDelete.AutoSize = true;
            this.cmdDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdDelete.ImageOptions.ImageIndex = 3;
            this.cmdDelete.Location = new System.Drawing.Point(1140, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(72, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 3;
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.AllowFocus = false;
            this.cmdAdd.AutoSize = true;
            this.cmdAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdAdd.ImageOptions.ImageIndex = 1;
            this.cmdAdd.Location = new System.Drawing.Point(1212, 0);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdAdd.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdAdd.Size = new System.Drawing.Size(72, 40);
            this.cmdAdd.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdAdd.TabIndex = 4;
            this.cmdAdd.Text = "등록";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdUpload
            // 
            this.cmdUpload.AllowFocus = false;
            this.cmdUpload.AutoSize = true;
            this.cmdUpload.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdUpload.ImageOptions.ImageIndex = 8;
            this.cmdUpload.Location = new System.Drawing.Point(1284, 0);
            this.cmdUpload.Name = "cmdUpload";
            this.cmdUpload.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdUpload.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdUpload.Size = new System.Drawing.Size(84, 40);
            this.cmdUpload.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀업로드;
            this.cmdUpload.TabIndex = 5;
            this.cmdUpload.Text = "업로드";
            this.cmdUpload.Click += new System.EventHandler(this.cmdUpload_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.AllowFocus = false;
            this.cmdClose.AutoSize = true;
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdClose.ImageOptions.ImageIndex = 5;
            this.cmdClose.Location = new System.Drawing.Point(1368, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(72, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // body
            // 
            this.body.Panel1.Controls.Add(this.groupCustomers);
            this.body.Panel2.Controls.Add(this.details);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(0, 40);
            this.body.Name = "body";
            this.body.Padding = new System.Windows.Forms.Padding(6);
            this.body.Size = new System.Drawing.Size(1440, 780);
            // 거래처 목록과 상세 영역 사이의 너비 조절
            this.body.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.body.IsSplitterFixed = false;
            this.body.Panel1.MinSize = 180;
            this.body.Panel2.MinSize = 320;
            this.body.SplitterPosition = 351;
            this.body.TabIndex = 0;
            // 
            // groupCustomers
            // 
            this.groupCustomers.Controls.Add(this.gcCustomers);
            this.groupCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCustomers.Location = new System.Drawing.Point(9, 9);
            this.groupCustomers.Name = "groupCustomers";
            this.groupCustomers.Size = new System.Drawing.Size(351, 762);
            this.groupCustomers.TabIndex = 0;
            this.groupCustomers.Text = "B2B · B2C 거래처";
            // 
            // gcCustomers
            // 
            this.gcCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcCustomers.Location = new System.Drawing.Point(2, 29);
            this.gcCustomers.MainView = this.gvCustomers;
            this.gcCustomers.Name = "gcCustomers";
            this.gcCustomers.Size = new System.Drawing.Size(347, 731);
            this.gcCustomers.TabIndex = 0;
            this.gcCustomers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCustomers});
            // 
            // gvCustomers
            // 
            this.gvCustomers.GridControl = this.gcCustomers;
            this.gvCustomers.Name = "gvCustomers";
            this.gvCustomers.OptionsBehavior.Editable = false;
            this.gvCustomers.OptionsView.ShowGroupPanel = false;
            this.gvCustomers.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvCustomers_FocusedRowChanged);
            // 
            // details
            // 
            this.details.Panel1.Controls.Add(this.groupOrders);
            this.details.Panel2.Controls.Add(this.groupEdiDetails);
            this.details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.details.Location = new System.Drawing.Point(366, 9);
            this.details.Name = "details";
            this.details.Size = new System.Drawing.Size(1065, 762);
            // 등록 목록과 하단 상세 그리드 사이의 높이 조절
            this.details.Horizontal = false;
            this.details.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.details.IsSplitterFixed = false;
            this.details.Panel1.MinSize = 120;
            this.details.Panel2.MinSize = 150;
            this.details.SplitterPosition = 281;
            this.details.TabIndex = 1;
            // 
            // groupOrders
            // 
            this.groupOrders.Controls.Add(this.gcOrders);
            this.groupOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupOrders.Location = new System.Drawing.Point(3, 3);
            this.groupOrders.Name = "groupOrders";
            this.groupOrders.Size = new System.Drawing.Size(1059, 275);
            this.groupOrders.TabIndex = 0;
            // 
            // gcOrders
            // 
            this.gcOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOrders.Location = new System.Drawing.Point(2, 29);
            this.gcOrders.MainView = this.gvOrders;
            this.gcOrders.Name = "gcOrders";
            this.gcOrders.Size = new System.Drawing.Size(1055, 244);
            this.gcOrders.TabIndex = 0;
            this.gcOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrders});
            // 
            // gvOrders
            // 
            this.gvOrders.GridControl = this.gcOrders;
            this.gvOrders.Name = "gvOrders";
            this.gvOrders.OptionsBehavior.Editable = false;
            this.gvOrders.OptionsView.ColumnAutoWidth = false;
            this.gvOrders.OptionsView.ShowGroupPanel = false;
            // 
            // groupEdiDetails
            // 
            this.groupEdiDetails.Controls.Add(this.gcEdiDetails);
            this.groupEdiDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupEdiDetails.Location = new System.Drawing.Point(3, 284);
            this.groupEdiDetails.Name = "groupEdiDetails";
            this.groupEdiDetails.Size = new System.Drawing.Size(1059, 475);
            this.groupEdiDetails.TabIndex = 1;
            this.groupEdiDetails.Text = "EDI 등록 자료 상세";
            // 
            // gcEdiDetails
            // 
            this.gcEdiDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcEdiDetails.Location = new System.Drawing.Point(2, 29);
            this.gcEdiDetails.MainView = this.gvEdiDetails;
            this.gcEdiDetails.Name = "gcEdiDetails";
            this.gcEdiDetails.Size = new System.Drawing.Size(1055, 444);
            this.gcEdiDetails.TabIndex = 0;
            this.gcEdiDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEdiDetails});
            // 
            // gvEdiDetails
            // 
            this.gvEdiDetails.GridControl = this.gcEdiDetails;
            this.gvEdiDetails.Name = "gvEdiDetails";
            this.gvEdiDetails.OptionsBehavior.Editable = false;
            this.gvEdiDetails.OptionsView.ColumnAutoWidth = false;
            this.gvEdiDetails.OptionsView.ShowGroupPanel = false;
            // 
            // Frm_BM_EdiOrderB2B
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 820);
            this.Controls.Add(this.body);
            this.Controls.Add(this.bar);
            this.Name = "Frm_BM_EdiOrderB2B";
            this.Text = "EDI 수주 등록(B2B)";
            this.Load += new System.EventHandler(this.Frm_BM_EdiOrderB2B_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar)).EndInit();
            this.bar.ResumeLayout(false);
            this.bar.PerformLayout();
            this.registrationFields.ResumeLayout(false);
            this.registrationFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerSearch.Properties)).EndInit();
            this.body.Panel1.ResumeLayout(false);
            this.body.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.body)).EndInit();
            this.body.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupCustomers)).EndInit();
            this.groupCustomers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomers)).EndInit();
            this.details.Panel1.ResumeLayout(false);
            this.details.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.details)).EndInit();
            this.details.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupOrders)).EndInit();
            this.groupOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupEdiDetails)).EndInit();
            this.groupEdiDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcEdiDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEdiDetails)).EndInit();
            this.ResumeLayout(false);

        }

        private smtCommonBar bar;
        private smtSearchButton cmdSearch;
        private smtSearchButton cmdClose;
        private smtSearchButton cmdUpload;
        private smtSearchButton cmdApply;
        private LabelControl lblOrderDate;
        private DateEdit orderDate;
        private smtSearchButton cmdAdd;
        private smtSearchButton cmdDelete;
        private LabelControl lblCustomerSearch;
        private TextEdit customerSearch;
        private ComboBoxEdit customerType;
        private LabelControl lblCustomerType;
        private SplitContainerControl body;
        private TableLayoutPanel registrationFields;
        private SplitContainerControl details;
        private GroupControl groupCustomers;
        private GridControl gcCustomers;
        private GridView gvCustomers;
        private GroupControl groupOrders;
        private GridControl gcOrders;
        private GridView gvOrders;
        private GroupControl groupEdiDetails;
        private GridControl gcEdiDetails;
        private GridView gvEdiDetails;
        private IContainer components;
    }
}
