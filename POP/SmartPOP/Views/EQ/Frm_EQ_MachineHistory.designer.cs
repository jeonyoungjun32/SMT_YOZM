
namespace SmartPOP.Views.EQ
{
    partial class Frm_EQ_MachineHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EQ_MachineHistory));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcRepair = new DevExpress.XtraGrid.GridControl();
            this.gvRepair = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCorpId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMachineCode1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.slupMachine = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSeq1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDisp_Seq1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRepair_Date1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colRepair_Cause1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRepair_State1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRepair_Emp_No1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRepair_Start_Time1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colRepair_End_Time1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colRepair_Time1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRepair_Cust_Cd1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRepair_Amt1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIns_Emp_No1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIns_Date_Time1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpd_Emp_No1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpd_Date_Time1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnIns = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.Lup_Machine = new baseSMT.smtSearchLookUp();
            this.Lup_Process = new baseSMT.smtSearchLookUp();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slupMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcRepair);
            this.layoutControl1.Controls.Add(this.tablePanel2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1350, 624);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcRepair
            // 
            this.gcRepair.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcRepair.Location = new System.Drawing.Point(7, 34);
            this.gcRepair.MainView = this.gvRepair;
            this.gcRepair.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcRepair.Name = "gcRepair";
            this.gcRepair.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.DateEdit1,
            this.DateEdit2,
            this.repositoryItemDateEdit5,
            this.slupMachine});
            this.gcRepair.Size = new System.Drawing.Size(1200, 583);
            this.gcRepair.TabIndex = 3;
            this.gcRepair.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRepair});
            // 
            // gvRepair
            // 
            this.gvRepair.Appearance.HeaderPanel.Font = new System.Drawing.Font("Pretendard SemiBold", 10F);
            this.gvRepair.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvRepair.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvRepair.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvRepair.ColumnPanelRowHeight = 24;
            this.gvRepair.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colCorpId1,
            this.colMachineCode1,
            this.colSeq1,
            this.colDisp_Seq1,
            this.colRepair_Date1,
            this.colRepair_Cause1,
            this.colRepair_State1,
            this.colRepair_Emp_No1,
            this.colRepair_Start_Time1,
            this.colRepair_End_Time1,
            this.colRepair_Time1,
            this.colRepair_Cust_Cd1,
            this.colRepair_Amt1,
            this.colRemark1,
            this.colIns_Emp_No1,
            this.colIns_Date_Time1,
            this.colUpd_Emp_No1,
            this.colUpd_Date_Time1});
            this.gvRepair.DetailHeight = 288;
            this.gvRepair.GridControl = this.gcRepair;
            this.gvRepair.Name = "gvRepair";
            this.gvRepair.OptionsSelection.MultiSelect = true;
            this.gvRepair.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvRepair.OptionsView.ColumnAutoWidth = false;
            this.gvRepair.OptionsView.ShowGroupPanel = false;
            this.gvRepair.RowHeight = 24;
            this.gvRepair.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colRepair_End_Time1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvRepair.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvRepair_CellValueChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "상태";
            this.gridColumn1.FieldName = "Status";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // colCorpId1
            // 
            this.colCorpId1.AppearanceHeader.Options.UseTextOptions = true;
            this.colCorpId1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCorpId1.FieldName = "CorpId";
            this.colCorpId1.MinWidth = 17;
            this.colCorpId1.Name = "colCorpId1";
            this.colCorpId1.Width = 66;
            // 
            // colMachineCode1
            // 
            this.colMachineCode1.AppearanceHeader.Options.UseTextOptions = true;
            this.colMachineCode1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMachineCode1.Caption = "장비코드";
            this.colMachineCode1.ColumnEdit = this.slupMachine;
            this.colMachineCode1.FieldName = "MachineCode";
            this.colMachineCode1.MinWidth = 17;
            this.colMachineCode1.Name = "colMachineCode1";
            this.colMachineCode1.Visible = true;
            this.colMachineCode1.VisibleIndex = 3;
            this.colMachineCode1.Width = 68;
            // 
            // slupMachine
            // 
            this.slupMachine.AutoHeight = false;
            this.slupMachine.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slupMachine.Name = "slupMachine";
            this.slupMachine.NullText = "";
            this.slupMachine.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colSeq1
            // 
            this.colSeq1.AppearanceHeader.Options.UseTextOptions = true;
            this.colSeq1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSeq1.Caption = "순번";
            this.colSeq1.FieldName = "Seq";
            this.colSeq1.MinWidth = 17;
            this.colSeq1.Name = "colSeq1";
            this.colSeq1.Visible = true;
            this.colSeq1.VisibleIndex = 2;
            this.colSeq1.Width = 72;
            // 
            // colDisp_Seq1
            // 
            this.colDisp_Seq1.AppearanceHeader.Options.UseTextOptions = true;
            this.colDisp_Seq1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDisp_Seq1.Caption = "표시순번";
            this.colDisp_Seq1.FieldName = "Disp_Seq";
            this.colDisp_Seq1.MinWidth = 17;
            this.colDisp_Seq1.Name = "colDisp_Seq1";
            this.colDisp_Seq1.Width = 66;
            // 
            // colRepair_Date1
            // 
            this.colRepair_Date1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_Date1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_Date1.Caption = "수리일자";
            this.colRepair_Date1.ColumnEdit = this.repositoryItemDateEdit5;
            this.colRepair_Date1.DisplayFormat.FormatString = "d";
            this.colRepair_Date1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colRepair_Date1.FieldName = "Repair_Date";
            this.colRepair_Date1.MinWidth = 17;
            this.colRepair_Date1.Name = "colRepair_Date1";
            this.colRepair_Date1.Visible = true;
            this.colRepair_Date1.VisibleIndex = 4;
            this.colRepair_Date1.Width = 87;
            // 
            // repositoryItemDateEdit5
            // 
            this.repositoryItemDateEdit5.AutoHeight = false;
            this.repositoryItemDateEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit5.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit5.Name = "repositoryItemDateEdit5";
            // 
            // colRepair_Cause1
            // 
            this.colRepair_Cause1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_Cause1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_Cause1.Caption = "고장내용";
            this.colRepair_Cause1.FieldName = "Repair_Cause";
            this.colRepair_Cause1.MinWidth = 17;
            this.colRepair_Cause1.Name = "colRepair_Cause1";
            this.colRepair_Cause1.Visible = true;
            this.colRepair_Cause1.VisibleIndex = 5;
            this.colRepair_Cause1.Width = 121;
            // 
            // colRepair_State1
            // 
            this.colRepair_State1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_State1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_State1.Caption = "수리내용";
            this.colRepair_State1.FieldName = "Repair_State";
            this.colRepair_State1.MinWidth = 17;
            this.colRepair_State1.Name = "colRepair_State1";
            this.colRepair_State1.Visible = true;
            this.colRepair_State1.VisibleIndex = 6;
            this.colRepair_State1.Width = 114;
            // 
            // colRepair_Emp_No1
            // 
            this.colRepair_Emp_No1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_Emp_No1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_Emp_No1.Caption = "수리담당자";
            this.colRepair_Emp_No1.FieldName = "Repair_Emp_No";
            this.colRepair_Emp_No1.MinWidth = 17;
            this.colRepair_Emp_No1.Name = "colRepair_Emp_No1";
            this.colRepair_Emp_No1.Visible = true;
            this.colRepair_Emp_No1.VisibleIndex = 7;
            this.colRepair_Emp_No1.Width = 59;
            // 
            // colRepair_Start_Time1
            // 
            this.colRepair_Start_Time1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_Start_Time1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_Start_Time1.Caption = "수리시작시간";
            this.colRepair_Start_Time1.ColumnEdit = this.DateEdit1;
            this.colRepair_Start_Time1.DisplayFormat.FormatString = "G";
            this.colRepair_Start_Time1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colRepair_Start_Time1.FieldName = "Repair_Start_Time";
            this.colRepair_Start_Time1.MinWidth = 17;
            this.colRepair_Start_Time1.Name = "colRepair_Start_Time1";
            this.colRepair_Start_Time1.Visible = true;
            this.colRepair_Start_Time1.VisibleIndex = 8;
            this.colRepair_Start_Time1.Width = 119;
            // 
            // DateEdit1
            // 
            this.DateEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.DateEdit1.AutoHeight = false;
            this.DateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit1.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.DateEdit1.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit1.DisplayFormat.FormatString = "G";
            this.DateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit1.EditFormat.FormatString = "G";
            this.DateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit1.MaskSettings.Set("mask", "");
            this.DateEdit1.Name = "DateEdit1";
            this.DateEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // colRepair_End_Time1
            // 
            this.colRepair_End_Time1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_End_Time1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_End_Time1.Caption = "수리종료시간";
            this.colRepair_End_Time1.ColumnEdit = this.DateEdit2;
            this.colRepair_End_Time1.DisplayFormat.FormatString = "G";
            this.colRepair_End_Time1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colRepair_End_Time1.FieldName = "Repair_End_Time";
            this.colRepair_End_Time1.MinWidth = 17;
            this.colRepair_End_Time1.Name = "colRepair_End_Time1";
            this.colRepair_End_Time1.Visible = true;
            this.colRepair_End_Time1.VisibleIndex = 9;
            this.colRepair_End_Time1.Width = 115;
            // 
            // DateEdit2
            // 
            this.DateEdit2.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.DateEdit2.AutoHeight = false;
            this.DateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit2.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.DateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit2.DisplayFormat.FormatString = "G";
            this.DateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit2.EditFormat.FormatString = "G";
            this.DateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit2.MaskSettings.Set("mask", "");
            this.DateEdit2.Name = "DateEdit2";
            this.DateEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // colRepair_Time1
            // 
            this.colRepair_Time1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_Time1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_Time1.Caption = "수리시간";
            this.colRepair_Time1.FieldName = "Repair_Time";
            this.colRepair_Time1.MinWidth = 17;
            this.colRepair_Time1.Name = "colRepair_Time1";
            this.colRepair_Time1.Visible = true;
            this.colRepair_Time1.VisibleIndex = 10;
            this.colRepair_Time1.Width = 73;
            // 
            // colRepair_Cust_Cd1
            // 
            this.colRepair_Cust_Cd1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_Cust_Cd1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_Cust_Cd1.Caption = "거래처";
            this.colRepair_Cust_Cd1.FieldName = "Repair_Cust_Cd";
            this.colRepair_Cust_Cd1.MinWidth = 17;
            this.colRepair_Cust_Cd1.Name = "colRepair_Cust_Cd1";
            this.colRepair_Cust_Cd1.Visible = true;
            this.colRepair_Cust_Cd1.VisibleIndex = 11;
            this.colRepair_Cust_Cd1.Width = 61;
            // 
            // colRepair_Amt1
            // 
            this.colRepair_Amt1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRepair_Amt1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRepair_Amt1.Caption = "수리금액";
            this.colRepair_Amt1.FieldName = "Repair_Amt";
            this.colRepair_Amt1.MinWidth = 17;
            this.colRepair_Amt1.Name = "colRepair_Amt1";
            this.colRepair_Amt1.Visible = true;
            this.colRepair_Amt1.VisibleIndex = 12;
            this.colRepair_Amt1.Width = 59;
            // 
            // colRemark1
            // 
            this.colRemark1.AppearanceHeader.Options.UseTextOptions = true;
            this.colRemark1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRemark1.Caption = "비고";
            this.colRemark1.FieldName = "Remark";
            this.colRemark1.MinWidth = 17;
            this.colRemark1.Name = "colRemark1";
            this.colRemark1.Visible = true;
            this.colRemark1.VisibleIndex = 13;
            this.colRemark1.Width = 116;
            // 
            // colIns_Emp_No1
            // 
            this.colIns_Emp_No1.AppearanceHeader.Options.UseTextOptions = true;
            this.colIns_Emp_No1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIns_Emp_No1.Caption = "등록자";
            this.colIns_Emp_No1.FieldName = "Ins_Emp_No";
            this.colIns_Emp_No1.MinWidth = 17;
            this.colIns_Emp_No1.Name = "colIns_Emp_No1";
            this.colIns_Emp_No1.Width = 48;
            // 
            // colIns_Date_Time1
            // 
            this.colIns_Date_Time1.AppearanceHeader.Options.UseTextOptions = true;
            this.colIns_Date_Time1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIns_Date_Time1.FieldName = "Ins_Date_Time";
            this.colIns_Date_Time1.MinWidth = 17;
            this.colIns_Date_Time1.Name = "colIns_Date_Time1";
            this.colIns_Date_Time1.Width = 48;
            // 
            // colUpd_Emp_No1
            // 
            this.colUpd_Emp_No1.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpd_Emp_No1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpd_Emp_No1.FieldName = "Upd_Emp_No";
            this.colUpd_Emp_No1.MinWidth = 17;
            this.colUpd_Emp_No1.Name = "colUpd_Emp_No1";
            this.colUpd_Emp_No1.Width = 48;
            // 
            // colUpd_Date_Time1
            // 
            this.colUpd_Date_Time1.AppearanceHeader.Options.UseTextOptions = true;
            this.colUpd_Date_Time1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUpd_Date_Time1.FieldName = "Upd_Date_Time";
            this.colUpd_Date_Time1.MinWidth = 17;
            this.colUpd_Date_Time1.Name = "colUpd_Date_Time1";
            this.colUpd_Date_Time1.Width = 67;
            // 
            // tablePanel2
            // 
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F)});
            this.tablePanel2.Controls.Add(this.BtnSave);
            this.tablePanel2.Controls.Add(this.BtnIns);
            this.tablePanel2.Controls.Add(this.BtnDel);
            this.tablePanel2.Controls.Add(this.cmdSearch);
            this.tablePanel2.Location = new System.Drawing.Point(1213, 34);
            this.tablePanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel2.MaximumSize = new System.Drawing.Size(130, 0);
            this.tablePanel2.MinimumSize = new System.Drawing.Size(100, 0);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
            this.tablePanel2.Size = new System.Drawing.Size(130, 583);
            this.tablePanel2.TabIndex = 2;
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.NavajoWhite;
            this.tablePanel2.SetColumn(this.BtnSave, 0);
            this.BtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.BtnSave.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Location = new System.Drawing.Point(3, 76);
            this.BtnSave.Name = "BtnSave";
            this.tablePanel2.SetRow(this.BtnSave, 1);
            this.BtnSave.Size = new System.Drawing.Size(124, 67);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "저장";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.cmdSave_Click);
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
            this.BtnIns.Location = new System.Drawing.Point(3, 149);
            this.BtnIns.Name = "BtnIns";
            this.tablePanel2.SetRow(this.BtnIns, 2);
            this.BtnIns.Size = new System.Drawing.Size(124, 67);
            this.BtnIns.TabIndex = 3;
            this.BtnIns.Text = "추가";
            this.BtnIns.UseVisualStyleBackColor = false;
            this.BtnIns.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // BtnDel
            // 
            this.BtnDel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tablePanel2.SetColumn(this.BtnDel, 0);
            this.BtnDel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnDel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(54)))), ((int)(((byte)(69)))));
            this.BtnDel.FlatAppearance.BorderSize = 0;
            this.BtnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDel.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.BtnDel.ForeColor = System.Drawing.Color.Black;
            this.BtnDel.Location = new System.Drawing.Point(3, 222);
            this.BtnDel.Name = "BtnDel";
            this.tablePanel2.SetRow(this.BtnDel, 3);
            this.BtnDel.Size = new System.Drawing.Size(124, 67);
            this.BtnDel.TabIndex = 2;
            this.BtnDel.Text = "삭제";
            this.BtnDel.UseVisualStyleBackColor = false;
            this.BtnDel.Click += new System.EventHandler(this.cmdDelete_Click);
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
            this.cmdSearch.Size = new System.Drawing.Size(124, 67);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "조회";
            this.cmdSearch.UseVisualStyleBackColor = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1350, 624);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Pretendard SemiBold", 15F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.CustomizationFormText = "설비수리 이력";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1350, 624);
            this.layoutControlGroup1.Text = "설비수리 이력";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tablePanel2;
            this.layoutControlItem3.Location = new System.Drawing.Point(1206, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(136, 589);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gcRepair;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1206, 589);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.Lup_Machine);
            this.smtCommonBar1.Controls.Add(this.Lup_Process);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.LookAndFeel.SkinName = "WXI";
            this.smtCommonBar1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1350, 45);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "설비수리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 4;
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
            this.btn_Close.Location = new System.Drawing.Point(1271, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.Size = new System.Drawing.Size(79, 45);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "닫기";
            // 
            // Lup_Machine
            // 
            this.Lup_Machine.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Machine.Location = new System.Drawing.Point(388, 0);
            this.Lup_Machine.LookAndFeel.SkinName = "WXI";
            this.Lup_Machine.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Machine.Name = "Lup_Machine";
            this.Lup_Machine.Size = new System.Drawing.Size(288, 45);
            this.Lup_Machine.smtLabelTitleValue = "설비";
            this.Lup_Machine.smtLabelTitleVisible = true;
            this.Lup_Machine.smtLabelTitleWidth = 100;
            this.Lup_Machine.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Machine.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Machine.smtLookUpDataSource = null;
            this.Lup_Machine.smtLookUpDataValueMember = "코드";
            this.Lup_Machine.smtLookUpSelectdIndex = -1;
            this.Lup_Machine.smtLookUpText = "";
            this.Lup_Machine.smtLookUpValue = "";
            this.Lup_Machine.TabIndex = 2;
            // 
            // Lup_Process
            // 
            this.Lup_Process.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Process.Location = new System.Drawing.Point(100, 0);
            this.Lup_Process.LookAndFeel.SkinName = "WXI";
            this.Lup_Process.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Process.Name = "Lup_Process";
            this.Lup_Process.Size = new System.Drawing.Size(288, 45);
            this.Lup_Process.smtLabelTitleValue = "공정";
            this.Lup_Process.smtLabelTitleVisible = true;
            this.Lup_Process.smtLabelTitleWidth = 100;
            this.Lup_Process.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Process.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Process.smtLookUpDataSource = null;
            this.Lup_Process.smtLookUpDataValueMember = "코드";
            this.Lup_Process.smtLookUpSelectdIndex = -1;
            this.Lup_Process.smtLookUpText = "";
            this.Lup_Process.smtLookUpValue = "";
            this.Lup_Process.TabIndex = 4;
            // 
            // frmMachineHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 669);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "frmMachineHistory";
            this.Text = "설비수리이력";
            this.Load += new System.EventHandler(this.frmMachineHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slupMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private System.Windows.Forms.Button BtnIns;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button cmdSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.Button BtnSave;
        private DevExpress.XtraGrid.GridControl gcRepair;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRepair;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId1;
        private DevExpress.XtraGrid.Columns.GridColumn colMachineCode1;
        private DevExpress.XtraGrid.Columns.GridColumn colSeq1;
        private DevExpress.XtraGrid.Columns.GridColumn colDisp_Seq1;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_Date1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit5;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_Cause1;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_State1;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_Emp_No1;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_Start_Time1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DateEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_End_Time1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DateEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_Time1;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_Cust_Cd1;
        private DevExpress.XtraGrid.Columns.GridColumn colRepair_Amt1;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark1;
        private DevExpress.XtraGrid.Columns.GridColumn colIns_Emp_No1;
        private DevExpress.XtraGrid.Columns.GridColumn colIns_Date_Time1;
        private DevExpress.XtraGrid.Columns.GridColumn colUpd_Emp_No1;
        private DevExpress.XtraGrid.Columns.GridColumn colUpd_Date_Time1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit slupMachine;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchLookUp Lup_Machine;
        private baseSMT.smtSearchLookUp Lup_Process;
        private baseSMT.smtSearchButton btn_Close;
    }
}