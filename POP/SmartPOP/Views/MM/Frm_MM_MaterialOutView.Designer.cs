namespace SmartPOP.Views.MM
{ 
    partial class Frm_MM_MaterialOutView
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
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
			this.BtnIns = new System.Windows.Forms.Button();
			this.BtnUpd = new System.Windows.Forms.Button();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.DtStart = new DevExpress.XtraEditors.DateEdit();
			this.DtEnd = new DevExpress.XtraEditors.DateEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.LupCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.GcBM_SalesD = new DevExpress.XtraGrid.GridControl();
			this.GvBM_SalesD = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.GcBM_SalesM = new DevExpress.XtraGrid.GridControl();
			this.GvBM_SalesM = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			this.cmdDel = new System.Windows.Forms.Button();
			this.btn_Close = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
			this.tablePanel2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtStart.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DtStart.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DtEnd.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DtEnd.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LupCustomer.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesD)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesM)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesM)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.tablePanel2);
			this.layoutControl1.Controls.Add(this.panel2);
			this.layoutControl1.Controls.Add(this.GcBM_SalesD);
			this.layoutControl1.Controls.Add(this.GcBM_SalesM);
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
			this.tablePanel2.Controls.Add(this.cmdDel);
			this.tablePanel2.Controls.Add(this.BtnIns);
			this.tablePanel2.Controls.Add(this.BtnUpd);
			this.tablePanel2.Controls.Add(this.cmdSearch);
			this.tablePanel2.Location = new System.Drawing.Point(1260, 77);
			this.tablePanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tablePanel2.MaximumSize = new System.Drawing.Size(130, 0);
			this.tablePanel2.MinimumSize = new System.Drawing.Size(100, 0);
			this.tablePanel2.Name = "tablePanel2";
			this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
			this.tablePanel2.Size = new System.Drawing.Size(100, 315);
			this.tablePanel2.TabIndex = 186;
			// 
			// BtnIns
			// 
			this.BtnIns.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.tablePanel2.SetColumn(this.BtnIns, 0);
			this.BtnIns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BtnIns.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.BtnIns.FlatAppearance.BorderSize = 0;
			this.BtnIns.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnIns.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.BtnIns.ForeColor = System.Drawing.Color.Black;
			this.BtnIns.Location = new System.Drawing.Point(3, 82);
			this.BtnIns.Name = "BtnIns";
			this.tablePanel2.SetRow(this.BtnIns, 1);
			this.BtnIns.Size = new System.Drawing.Size(94, 73);
			this.BtnIns.TabIndex = 5;
			this.BtnIns.Text = "등록";
			this.BtnIns.UseVisualStyleBackColor = false;
			this.BtnIns.Click += new System.EventHandler(this.BtnIns_Click);
			// 
			// BtnUpd
			// 
			this.BtnUpd.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.tablePanel2.SetColumn(this.BtnUpd, 0);
			this.BtnUpd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.BtnUpd.FlatAppearance.BorderSize = 0;
			this.BtnUpd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnUpd.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.BtnUpd.ForeColor = System.Drawing.Color.Black;
			this.BtnUpd.Location = new System.Drawing.Point(3, 161);
			this.BtnUpd.Name = "BtnUpd";
			this.tablePanel2.SetRow(this.BtnUpd, 2);
			this.BtnUpd.Size = new System.Drawing.Size(94, 73);
			this.BtnUpd.TabIndex = 2;
			this.BtnUpd.Text = "수정";
			this.BtnUpd.UseVisualStyleBackColor = false;
			this.BtnUpd.Click += new System.EventHandler(this.BtnUpd_Click);
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
			this.cmdSearch.Size = new System.Drawing.Size(94, 73);
			this.cmdSearch.TabIndex = 1;
			this.cmdSearch.Text = "조회";
			this.cmdSearch.UseVisualStyleBackColor = false;
			this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.panel2.Controls.Add(this.btn_Close);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
			this.panel2.Size = new System.Drawing.Size(1367, 43);
			this.panel2.TabIndex = 31;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.DtStart);
			this.panel3.Controls.Add(this.DtEnd);
			this.panel3.Controls.Add(this.labelControl1);
			this.panel3.Controls.Add(this.labelControl3);
			this.panel3.Controls.Add(this.LupCustomer);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.Location = new System.Drawing.Point(3, 0);
			this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(757, 43);
			this.panel3.TabIndex = 27;
			// 
			// DtStart
			// 
			this.DtStart.EditValue = null;
			this.DtStart.Location = new System.Drawing.Point(101, 6);
			this.DtStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DtStart.Name = "DtStart";
			this.DtStart.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.DtStart.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.DtStart.Properties.Appearance.Options.UseFont = true;
			this.DtStart.Properties.Appearance.Options.UseForeColor = true;
			this.DtStart.Properties.AutoHeight = false;
			this.DtStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.DtStart.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.DtStart.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
			this.DtStart.Properties.MaskSettings.Set("useAdvancingCaret", true);
			this.DtStart.Size = new System.Drawing.Size(160, 32);
			this.DtStart.TabIndex = 16;
			// 
			// DtEnd
			// 
			this.DtEnd.EditValue = null;
			this.DtEnd.Location = new System.Drawing.Point(264, 6);
			this.DtEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DtEnd.Name = "DtEnd";
			this.DtEnd.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.DtEnd.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.DtEnd.Properties.Appearance.Options.UseFont = true;
			this.DtEnd.Properties.Appearance.Options.UseForeColor = true;
			this.DtEnd.Properties.AutoHeight = false;
			this.DtEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.DtEnd.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.DtEnd.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
			this.DtEnd.Properties.MaskSettings.Set("useAdvancingCaret", true);
			this.DtEnd.Size = new System.Drawing.Size(160, 32);
			this.DtEnd.TabIndex = 17;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControl1.Appearance.Options.UseFont = true;
			this.labelControl1.Appearance.Options.UseForeColor = true;
			this.labelControl1.Location = new System.Drawing.Point(1, 10);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
			this.labelControl1.Size = new System.Drawing.Size(92, 24);
			this.labelControl1.TabIndex = 42;
			this.labelControl1.Text = "출고일자";
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControl3.Appearance.Options.UseFont = true;
			this.labelControl3.Appearance.Options.UseForeColor = true;
			this.labelControl3.Location = new System.Drawing.Point(417, 10);
			this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
			this.labelControl3.Size = new System.Drawing.Size(75, 24);
			this.labelControl3.TabIndex = 41;
			this.labelControl3.Text = "거래처";
			// 
			// LupCustomer
			// 
			this.LupCustomer.Location = new System.Drawing.Point(501, 6);
			this.LupCustomer.Margin = new System.Windows.Forms.Padding(0);
			this.LupCustomer.Name = "LupCustomer";
			this.LupCustomer.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
			this.LupCustomer.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.LupCustomer.Properties.Appearance.Options.UseFont = true;
			this.LupCustomer.Properties.Appearance.Options.UseForeColor = true;
			this.LupCustomer.Properties.AutoHeight = false;
			this.LupCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.LupCustomer.Properties.NullText = "";
			this.LupCustomer.Properties.PopupView = this.searchLookUpEdit1View;
			this.LupCustomer.Size = new System.Drawing.Size(250, 32);
			this.LupCustomer.TabIndex = 18;
			// 
			// searchLookUpEdit1View
			// 
			this.searchLookUpEdit1View.DetailHeight = 371;
			this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
			this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// GcBM_SalesD
			// 
			this.GcBM_SalesD.Cursor = System.Windows.Forms.Cursors.Default;
			this.GcBM_SalesD.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
			this.GcBM_SalesD.Location = new System.Drawing.Point(7, 449);
			this.GcBM_SalesD.MainView = this.GvBM_SalesD;
			this.GcBM_SalesD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.GcBM_SalesD.Name = "GcBM_SalesD";
			this.GcBM_SalesD.Size = new System.Drawing.Size(1353, 342);
			this.GcBM_SalesD.TabIndex = 52;
			this.GcBM_SalesD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvBM_SalesD});
			// 
			// GvBM_SalesD
			// 
			this.GvBM_SalesD.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.GvBM_SalesD.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GvBM_SalesD.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.GvBM_SalesD.ColumnPanelRowHeight = 31;
			this.GvBM_SalesD.DetailHeight = 450;
			this.GvBM_SalesD.GridControl = this.GcBM_SalesD;
			this.GvBM_SalesD.Name = "GvBM_SalesD";
			this.GvBM_SalesD.OptionsBehavior.Editable = false;
			this.GvBM_SalesD.OptionsSelection.CheckBoxSelectorColumnWidth = 38;
			this.GvBM_SalesD.OptionsView.ColumnAutoWidth = false;
			this.GvBM_SalesD.OptionsView.ShowGroupPanel = false;
			this.GvBM_SalesD.RowHeight = 31;
			// 
			// GcBM_SalesM
			// 
			this.GcBM_SalesM.Cursor = System.Windows.Forms.Cursors.Default;
			this.GcBM_SalesM.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
			this.GcBM_SalesM.Location = new System.Drawing.Point(7, 77);
			this.GcBM_SalesM.MainView = this.GvBM_SalesM;
			this.GcBM_SalesM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.GcBM_SalesM.Name = "GcBM_SalesM";
			this.GcBM_SalesM.Size = new System.Drawing.Size(1247, 315);
			this.GcBM_SalesM.TabIndex = 51;
			this.GcBM_SalesM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvBM_SalesM});
			// 
			// GvBM_SalesM
			// 
			this.GvBM_SalesM.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.GvBM_SalesM.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.GvBM_SalesM.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.GvBM_SalesM.ColumnPanelRowHeight = 31;
			this.GvBM_SalesM.DetailHeight = 450;
			this.GvBM_SalesM.GridControl = this.GcBM_SalesM;
			this.GvBM_SalesM.Name = "GvBM_SalesM";
			this.GvBM_SalesM.OptionsBehavior.Editable = false;
			this.GvBM_SalesM.OptionsDetail.EnableMasterViewMode = false;
			this.GvBM_SalesM.OptionsSelection.CheckBoxSelectorColumnWidth = 45;
			this.GvBM_SalesM.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.GvBM_SalesM.OptionsView.ColumnAutoWidth = false;
			this.GvBM_SalesM.OptionsView.ShowAutoFilterRow = true;
			this.GvBM_SalesM.OptionsView.ShowGroupPanel = false;
			this.GvBM_SalesM.RowHeight = 31;
			this.GvBM_SalesM.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GvBM_SalesM_FocusedRowChanged);
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlItem6,
            this.splitterItem1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1367, 798);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.CustomizationFormText = "발주등록 마스터";
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 43);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(1367, 356);
			this.layoutControlGroup2.Text = "출고등록 마스터";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.GcBM_SalesM;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1253, 321);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.tablePanel2;
			this.layoutControlItem3.Location = new System.Drawing.Point(1253, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(106, 321);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.CustomizationFormText = "발주등록 세부내용";
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup3.Location = new System.Drawing.Point(0, 415);
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
			this.layoutControlGroup3.Size = new System.Drawing.Size(1367, 383);
			this.layoutControlGroup3.Text = "출고 품목";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.GcBM_SalesD;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1359, 348);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
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
			// splitterItem1
			// 
			this.splitterItem1.AllowHotTrack = true;
			this.splitterItem1.Location = new System.Drawing.Point(0, 399);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(1367, 16);
			// 
			// cmdDel
			// 
			this.cmdDel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.tablePanel2.SetColumn(this.cmdDel, 0);
			this.cmdDel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
			this.cmdDel.FlatAppearance.BorderSize = 0;
			this.cmdDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cmdDel.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.cmdDel.ForeColor = System.Drawing.Color.Black;
			this.cmdDel.Location = new System.Drawing.Point(3, 240);
			this.cmdDel.Name = "cmdDel";
			this.tablePanel2.SetRow(this.cmdDel, 3);
			this.cmdDel.Size = new System.Drawing.Size(94, 72);
			this.cmdDel.TabIndex = 6;
			this.cmdDel.Text = "삭제";
			this.cmdDel.UseVisualStyleBackColor = false;
			this.cmdDel.Click += new System.EventHandler(this.cmdDel_Click);
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
			this.btn_Close.Location = new System.Drawing.Point(1285, 0);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btn_Close.Size = new System.Drawing.Size(79, 43);
			this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.btn_Close.TabIndex = 28;
			this.btn_Close.Text = "닫기";
			this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// frmMaterialOutView_POP
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1367, 798);
			this.Controls.Add(this.layoutControl1);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "frmMaterialOutView_POP";
			this.Text = "자재출고";
			this.Load += new System.EventHandler(this.frmPurchaseView_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
			this.tablePanel2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DtStart.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DtStart.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DtEnd.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DtEnd.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LupCustomer.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesD)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GcBM_SalesM)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvBM_SalesM)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraGrid.GridControl GcBM_SalesM;
        private DevExpress.XtraGrid.Views.Grid.GridView GvBM_SalesM;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl GcBM_SalesD;
        private DevExpress.XtraGrid.Views.Grid.GridView GvBM_SalesD;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.DateEdit DtEnd;
        private DevExpress.XtraEditors.DateEdit DtStart;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SearchLookUpEdit LupCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private System.Windows.Forms.Button BtnIns;
        private System.Windows.Forms.Button BtnUpd;
        private System.Windows.Forms.Button cmdSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private System.Windows.Forms.Button cmdDel;
		private baseSMT.smtSearchButton btn_Close;
	}
}