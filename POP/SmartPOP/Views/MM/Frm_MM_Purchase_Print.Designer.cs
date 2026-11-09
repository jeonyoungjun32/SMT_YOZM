namespace SmartPOP.Views.MM
{
    partial class Frm_MM_Purchase_Print
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
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
			this.cmdPrint = new System.Windows.Forms.Button();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.barEditEndDate = new DevExpress.XtraEditors.DateEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.barEditStartDate = new DevExpress.XtraEditors.DateEdit();
			this.customerBox = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gcDetail = new DevExpress.XtraGrid.GridControl();
			this.gvDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
			this.tablePanel2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barEditEndDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barEditEndDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barEditStartDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barEditStartDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.customerBox.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gcDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.tablePanel2);
			this.layoutControl1.Controls.Add(this.panel2);
			this.layoutControl1.Controls.Add(this.gcDetail);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1748, 297, 812, 500);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1367, 798);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// tablePanel2
			// 
			this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F)});
			this.tablePanel2.Controls.Add(this.cmdPrint);
			this.tablePanel2.Controls.Add(this.cmdSearch);
			this.tablePanel2.Location = new System.Drawing.Point(1230, 77);
			this.tablePanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tablePanel2.MaximumSize = new System.Drawing.Size(130, 0);
			this.tablePanel2.MinimumSize = new System.Drawing.Size(100, 0);
			this.tablePanel2.Name = "tablePanel2";
			this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
			this.tablePanel2.Size = new System.Drawing.Size(130, 714);
			this.tablePanel2.TabIndex = 185;
			// 
			// cmdPrint
			// 
			this.cmdPrint.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.tablePanel2.SetColumn(this.cmdPrint, 0);
			this.cmdPrint.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmdPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.cmdPrint.FlatAppearance.BorderSize = 0;
			this.cmdPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdPrint.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.cmdPrint.ForeColor = System.Drawing.Color.Black;
			this.cmdPrint.Location = new System.Drawing.Point(3, 182);
			this.cmdPrint.Name = "cmdPrint";
			this.tablePanel2.SetRow(this.cmdPrint, 1);
			this.cmdPrint.Size = new System.Drawing.Size(124, 173);
			this.cmdPrint.TabIndex = 5;
			this.cmdPrint.Text = "출력";
			this.cmdPrint.UseVisualStyleBackColor = false;
			this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
			// 
			// cmdSearch
			// 
			this.cmdSearch.BackColor = System.Drawing.Color.NavajoWhite;
			this.tablePanel2.SetColumn(this.cmdSearch, 0);
			this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.cmdSearch.FlatAppearance.BorderSize = 0;
			this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdSearch.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.cmdSearch.ForeColor = System.Drawing.Color.Black;
			this.cmdSearch.Location = new System.Drawing.Point(3, 3);
			this.cmdSearch.Name = "cmdSearch";
			this.tablePanel2.SetRow(this.cmdSearch, 0);
			this.cmdSearch.Size = new System.Drawing.Size(124, 173);
			this.cmdSearch.TabIndex = 1;
			this.cmdSearch.Text = "조회";
			this.cmdSearch.UseVisualStyleBackColor = false;
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1367, 43);
			this.panel2.TabIndex = 33;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.barEditEndDate);
			this.panel3.Controls.Add(this.labelControl1);
			this.panel3.Controls.Add(this.labelControl3);
			this.panel3.Controls.Add(this.barEditStartDate);
			this.panel3.Controls.Add(this.customerBox);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(756, 43);
			this.panel3.TabIndex = 27;
			// 
			// barEditEndDate
			// 
			this.barEditEndDate.EditValue = null;
			this.barEditEndDate.Location = new System.Drawing.Point(264, 6);
			this.barEditEndDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.barEditEndDate.Name = "barEditEndDate";
			this.barEditEndDate.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.barEditEndDate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.barEditEndDate.Properties.Appearance.Options.UseFont = true;
			this.barEditEndDate.Properties.Appearance.Options.UseForeColor = true;
			this.barEditEndDate.Properties.AutoHeight = false;
			this.barEditEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.barEditEndDate.Properties.CalendarTimeProperties.AutoHeight = false;
			this.barEditEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.barEditEndDate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
			this.barEditEndDate.Properties.MaskSettings.Set("useAdvancingCaret", true);
			this.barEditEndDate.Size = new System.Drawing.Size(160, 32);
			this.barEditEndDate.TabIndex = 24;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControl1.Appearance.Options.UseFont = true;
			this.labelControl1.Appearance.Options.UseForeColor = true;
			this.labelControl1.Location = new System.Drawing.Point(427, 10);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.labelControl1.Size = new System.Drawing.Size(71, 24);
			this.labelControl1.TabIndex = 29;
			this.labelControl1.Text = "거래처";
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControl3.Appearance.Options.UseFont = true;
			this.labelControl3.Appearance.Options.UseForeColor = true;
			this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl3.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelControl3.Location = new System.Drawing.Point(0, 0);
			this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.labelControl3.Size = new System.Drawing.Size(98, 43);
			this.labelControl3.TabIndex = 28;
			this.labelControl3.Text = "조회기간";
			// 
			// barEditStartDate
			// 
			this.barEditStartDate.EditValue = null;
			this.barEditStartDate.Location = new System.Drawing.Point(100, 6);
			this.barEditStartDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.barEditStartDate.Name = "barEditStartDate";
			this.barEditStartDate.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.barEditStartDate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.barEditStartDate.Properties.Appearance.Options.UseFont = true;
			this.barEditStartDate.Properties.Appearance.Options.UseForeColor = true;
			this.barEditStartDate.Properties.AutoHeight = false;
			this.barEditStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.barEditStartDate.Properties.CalendarTimeProperties.AutoHeight = false;
			this.barEditStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.barEditStartDate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
			this.barEditStartDate.Properties.MaskSettings.Set("useAdvancingCaret", true);
			this.barEditStartDate.Size = new System.Drawing.Size(160, 32);
			this.barEditStartDate.TabIndex = 23;
			// 
			// customerBox
			// 
			this.customerBox.EditValue = "";
			this.customerBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.customerBox.Location = new System.Drawing.Point(503, 6);
			this.customerBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.customerBox.Name = "customerBox";
			this.customerBox.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.customerBox.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.customerBox.Properties.Appearance.Options.UseFont = true;
			this.customerBox.Properties.Appearance.Options.UseForeColor = true;
			this.customerBox.Properties.AutoHeight = false;
			this.customerBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.customerBox.Properties.NullText = "";
			this.customerBox.Properties.PopupView = this.gridView1;
			this.customerBox.Size = new System.Drawing.Size(250, 32);
			this.customerBox.TabIndex = 22;
			// 
			// gridView1
			// 
			this.gridView1.DetailHeight = 371;
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// gcDetail
			// 
			this.gcDetail.Cursor = System.Windows.Forms.Cursors.Default;
			this.gcDetail.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
			this.gcDetail.Location = new System.Drawing.Point(7, 77);
			this.gcDetail.MainView = this.gvDetail;
			this.gcDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.gcDetail.Name = "gcDetail";
			this.gcDetail.Size = new System.Drawing.Size(1217, 714);
			this.gcDetail.TabIndex = 52;
			this.gcDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetail});
			// 
			// gvDetail
			// 
			this.gvDetail.ColumnPanelRowHeight = 38;
			this.gvDetail.DetailHeight = 450;
			this.gvDetail.GridControl = this.gcDetail;
			this.gvDetail.Name = "gvDetail";
			this.gvDetail.OptionsBehavior.Editable = false;
			this.gvDetail.OptionsSelection.CheckBoxSelectorColumnWidth = 38;
			this.gvDetail.OptionsView.ColumnAutoWidth = false;
			this.gvDetail.OptionsView.ShowGroupPanel = false;
			this.gvDetail.OptionsView.ShowIndicator = false;
			this.gvDetail.RowHeight = 38;
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlItem6});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1367, 798);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.CustomizationFormText = "발주등록 세부내용";
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3});
			this.layoutControlGroup3.Location = new System.Drawing.Point(0, 43);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(1367, 755);
			this.layoutControlGroup3.Text = "입고 품목";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.gcDetail;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1223, 720);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.tablePanel2;
			this.layoutControlItem3.Location = new System.Drawing.Point(1223, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(136, 720);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.Control = this.panel2;
			this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem6.MaxSize = new System.Drawing.Size(0, 43);
			this.layoutControlItem6.MinSize = new System.Drawing.Size(100, 43);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlItem6.Size = new System.Drawing.Size(1367, 43);
			this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			// 
			// frmPurchasePrintLabel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1367, 798);
			this.Controls.Add(this.layoutControl1);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmPurchasePrintLabel";
			this.Text = "입고 관리";
			this.Load += new System.EventHandler(this.frmPurchasePrintLabel_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
			this.tablePanel2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.barEditEndDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barEditEndDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barEditStartDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barEditStartDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.customerBox.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gcDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraGrid.GridControl gcDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.DateEdit barEditEndDate;
        private DevExpress.XtraEditors.DateEdit barEditStartDate;
        private DevExpress.XtraEditors.SearchLookUpEdit customerBox;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private System.Windows.Forms.Button cmdSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.Button cmdPrint;
	}
}