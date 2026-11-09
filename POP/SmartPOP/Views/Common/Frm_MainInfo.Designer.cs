
namespace SmartPOP.Views.Common
{
    partial class Frm_MainInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MainInfo));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btnSearch = new baseSMT.smtSearchButton();
            this.GcNotice = new DevExpress.XtraGrid.GridControl();
            this.GvNotice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBoardNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWriteDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colModId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.smtCommonBar2 = new baseSMT.smtCommonBar();
            this.gcPM_OrderProcess = new DevExpress.XtraGrid.GridControl();
            this.gvPM_OrderProcess = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderNo3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSearchLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcNotice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvNotice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPM_OrderProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPM_OrderProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tablePanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tablePanel2);
            this.splitContainer1.Size = new System.Drawing.Size(1319, 748);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // tablePanel1
            // 
            this.tablePanel1.AutoSize = true;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F)});
            this.tablePanel1.Controls.Add(this.smtCommonBar1);
            this.tablePanel1.Controls.Add(this.GcNotice);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 35F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 1F)});
            this.tablePanel1.Size = new System.Drawing.Size(1319, 333);
            this.tablePanel1.TabIndex = 1;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel1.SetColumn(this.smtCommonBar1, 0);
            this.smtCommonBar1.Controls.Add(this.btnSearch);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smtCommonBar1.Location = new System.Drawing.Point(4, 4);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.tablePanel1.SetRow(this.smtCommonBar1, 0);
            this.smtCommonBar1.Size = new System.Drawing.Size(1311, 45);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "공지사항";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 1;
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
            this.btnSearch.Location = new System.Drawing.Point(120, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btnSearch.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSearch.Size = new System.Drawing.Size(100, 45);
            this.btnSearch.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "조회";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // GcNotice
            // 
            this.tablePanel1.SetColumn(this.GcNotice, 0);
            this.GcNotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GcNotice.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.GcNotice.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.GcNotice.Location = new System.Drawing.Point(4, 57);
            this.GcNotice.MainView = this.GvNotice;
            this.GcNotice.Margin = new System.Windows.Forms.Padding(4);
            this.GcNotice.Name = "GcNotice";
            this.GcNotice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.tablePanel1.SetRow(this.GcNotice, 1);
            this.GcNotice.Size = new System.Drawing.Size(1311, 272);
            this.GcNotice.TabIndex = 0;
            this.GcNotice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvNotice});
            // 
            // GvNotice
            // 
            this.GvNotice.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GvNotice.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GvNotice.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GvNotice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBoardNo,
            this.colWriteDate,
            this.colSubject,
            this.colStartDate,
            this.colEndDate,
            this.colCreId,
            this.colModId});
            this.GvNotice.DetailHeight = 447;
            this.GvNotice.GridControl = this.GcNotice;
            this.GvNotice.Name = "GvNotice";
            this.GvNotice.OptionsEditForm.PopupEditFormWidth = 978;
            this.GvNotice.OptionsView.ShowGroupPanel = false;
            this.GvNotice.OptionsView.ShowIndicator = false;
            this.GvNotice.DoubleClick += new System.EventHandler(this.GvNotice_DoubleClick);
            // 
            // colBoardNo
            // 
            this.colBoardNo.Caption = "공지번호";
            this.colBoardNo.FieldName = "BoardNo";
            this.colBoardNo.MinWidth = 24;
            this.colBoardNo.Name = "colBoardNo";
            this.colBoardNo.Visible = true;
            this.colBoardNo.VisibleIndex = 0;
            this.colBoardNo.Width = 147;
            // 
            // colWriteDate
            // 
            this.colWriteDate.AppearanceCell.Options.UseTextOptions = true;
            this.colWriteDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWriteDate.Caption = "작성일자";
            this.colWriteDate.FieldName = "WriteDate";
            this.colWriteDate.MinWidth = 24;
            this.colWriteDate.Name = "colWriteDate";
            this.colWriteDate.Width = 134;
            // 
            // colSubject
            // 
            this.colSubject.Caption = "제목";
            this.colSubject.FieldName = "Subject";
            this.colSubject.MinWidth = 24;
            this.colSubject.Name = "colSubject";
            this.colSubject.Visible = true;
            this.colSubject.VisibleIndex = 1;
            this.colSubject.Width = 522;
            // 
            // colStartDate
            // 
            this.colStartDate.AppearanceCell.Options.UseTextOptions = true;
            this.colStartDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStartDate.Caption = "시작일자";
            this.colStartDate.FieldName = "StartDate";
            this.colStartDate.MinWidth = 24;
            this.colStartDate.Name = "colStartDate";
            this.colStartDate.Width = 134;
            // 
            // colEndDate
            // 
            this.colEndDate.AppearanceCell.Options.UseTextOptions = true;
            this.colEndDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEndDate.Caption = "종료일자";
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.MinWidth = 24;
            this.colEndDate.Name = "colEndDate";
            this.colEndDate.Width = 134;
            // 
            // colCreId
            // 
            this.colCreId.AppearanceCell.Options.UseTextOptions = true;
            this.colCreId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreId.Caption = "작성자";
            this.colCreId.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colCreId.FieldName = "CreId";
            this.colCreId.MinWidth = 24;
            this.colCreId.Name = "colCreId";
            this.colCreId.Visible = true;
            this.colCreId.VisibleIndex = 2;
            this.colCreId.Width = 137;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // colModId
            // 
            this.colModId.AppearanceCell.Options.UseTextOptions = true;
            this.colModId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colModId.Caption = "최종수정자";
            this.colModId.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colModId.FieldName = "ModId";
            this.colModId.MinWidth = 24;
            this.colModId.Name = "colModId";
            this.colModId.Width = 137;
            // 
            // tablePanel2
            // 
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F)});
            this.tablePanel2.Controls.Add(this.smtCommonBar2);
            this.tablePanel2.Controls.Add(this.gcPM_OrderProcess);
            this.tablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel2.Location = new System.Drawing.Point(0, 0);
            this.tablePanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 35F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 1F)});
            this.tablePanel2.Size = new System.Drawing.Size(1319, 410);
            this.tablePanel2.TabIndex = 2;
            // 
            // smtCommonBar2
            // 
            this.smtCommonBar2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar2.Appearance.Options.UseBackColor = true;
            this.smtCommonBar2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tablePanel2.SetColumn(this.smtCommonBar2, 0);
            this.smtCommonBar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smtCommonBar2.Location = new System.Drawing.Point(4, 4);
            this.smtCommonBar2.LookAndFeel.SkinName = "WXI";
            this.smtCommonBar2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtCommonBar2.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar2.Name = "smtCommonBar2";
            this.tablePanel2.SetRow(this.smtCommonBar2, 0);
            this.smtCommonBar2.Size = new System.Drawing.Size(1311, 45);
            this.smtCommonBar2.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar2.smtImageTitleSvgImage")));
            this.smtCommonBar2.smtImageTitleText = "작업지시현황";
            this.smtCommonBar2.smtImageTitleVisible = true;
            this.smtCommonBar2.TabIndex = 1;
            // 
            // gcPM_OrderProcess
            // 
            this.tablePanel2.SetColumn(this.gcPM_OrderProcess, 0);
            this.gcPM_OrderProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcPM_OrderProcess.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcPM_OrderProcess.Location = new System.Drawing.Point(4, 57);
            this.gcPM_OrderProcess.MainView = this.gvPM_OrderProcess;
            this.gcPM_OrderProcess.Margin = new System.Windows.Forms.Padding(4);
            this.gcPM_OrderProcess.Name = "gcPM_OrderProcess";
            this.gcPM_OrderProcess.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSearchLookUpEdit1,
            this.repositoryItemSearchLookUpEdit2});
            this.tablePanel2.SetRow(this.gcPM_OrderProcess, 1);
            this.gcPM_OrderProcess.Size = new System.Drawing.Size(1311, 349);
            this.gcPM_OrderProcess.TabIndex = 3;
            this.gcPM_OrderProcess.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPM_OrderProcess});
            // 
            // gvPM_OrderProcess
            // 
            this.gvPM_OrderProcess.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvPM_OrderProcess.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvPM_OrderProcess.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvPM_OrderProcess.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn10,
            this.colOrderNo3,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn1});
            this.gvPM_OrderProcess.DetailHeight = 447;
            this.gvPM_OrderProcess.GridControl = this.gcPM_OrderProcess;
            this.gvPM_OrderProcess.Name = "gvPM_OrderProcess";
            this.gvPM_OrderProcess.OptionsEditForm.PopupEditFormWidth = 978;
            this.gvPM_OrderProcess.OptionsView.ShowGroupPanel = false;
            this.gvPM_OrderProcess.OptionsView.ShowIndicator = false;
            this.gvPM_OrderProcess.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.RowCellDisplayValue);
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn10.Caption = "구분";
            this.gridColumn10.FieldName = "URGENT_YN";
            this.gridColumn10.MinWidth = 24;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 92;
            // 
            // colOrderNo3
            // 
            this.colOrderNo3.Caption = "작업지시번호";
            this.colOrderNo3.FieldName = "OrderNo";
            this.colOrderNo3.MinWidth = 24;
            this.colOrderNo3.Name = "colOrderNo3";
            this.colOrderNo3.Visible = true;
            this.colOrderNo3.VisibleIndex = 1;
            this.colOrderNo3.Width = 92;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AccessibleName = "colOrderSeq";
            this.gridColumn2.Caption = "순번";
            this.gridColumn2.FieldName = "OrderSeq";
            this.gridColumn2.MinWidth = 24;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Width = 92;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "지시일자";
            this.gridColumn3.FieldName = "WorkDate";
            this.gridColumn3.MinWidth = 24;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 92;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "품명";
            this.gridColumn8.FieldName = "MaterialName";
            this.gridColumn8.MinWidth = 24;
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            this.gridColumn8.Width = 92;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "제품명";
            this.gridColumn9.FieldName = "ProductMaterialName";
            this.gridColumn9.MinWidth = 24;
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Width = 92;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "공정";
            this.gridColumn4.ColumnEdit = this.repositoryItemSearchLookUpEdit2;
            this.gridColumn4.FieldName = "ProcessCode";
            this.gridColumn4.MinWidth = 24;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 92;
            // 
            // repositoryItemSearchLookUpEdit2
            // 
            this.repositoryItemSearchLookUpEdit2.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit2.Name = "repositoryItemSearchLookUpEdit2";
            this.repositoryItemSearchLookUpEdit2.PopupView = this.repositoryItemSearchLookUpEdit2View;
            // 
            // repositoryItemSearchLookUpEdit2View
            // 
            this.repositoryItemSearchLookUpEdit2View.DetailHeight = 447;
            this.repositoryItemSearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit2View.Name = "repositoryItemSearchLookUpEdit2View";
            this.repositoryItemSearchLookUpEdit2View.OptionsEditForm.PopupEditFormWidth = 978;
            this.repositoryItemSearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "라인명";
            this.gridColumn5.ColumnEdit = this.repositoryItemSearchLookUpEdit1;
            this.gridColumn5.FieldName = "LineCode";
            this.gridColumn5.MinWidth = 24;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Width = 92;
            // 
            // repositoryItemSearchLookUpEdit1
            // 
            this.repositoryItemSearchLookUpEdit1.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit1.Name = "repositoryItemSearchLookUpEdit1";
            this.repositoryItemSearchLookUpEdit1.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.DetailHeight = 447;
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsEditForm.PopupEditFormWidth = 978;
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "지시수량";
            this.gridColumn6.DisplayFormat.FormatString = "#,0";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "OrderQty";
            this.gridColumn6.MinWidth = 24;
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 92;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "상태";
            this.gridColumn7.FieldName = "Status";
            this.gridColumn7.MinWidth = 24;
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 92;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "설비코드";
            this.gridColumn1.FieldName = "MachineCode";
            this.gridColumn1.MinWidth = 24;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 92;
            // 
            // frmMainInfo1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 748);
            this.Controls.Add(this.splitContainer1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMainInfo1";
            this.Text = "공지사항 및 작업지시현황";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcNotice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvNotice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPM_OrderProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPM_OrderProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraGrid.GridControl GcNotice;
        private DevExpress.XtraGrid.Views.Grid.GridView GvNotice;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private DevExpress.XtraGrid.GridControl gcPM_OrderProcess;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPM_OrderProcess;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNo3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit2View;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn colBoardNo;
        private DevExpress.XtraGrid.Columns.GridColumn colWriteDate;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCreId;
        private DevExpress.XtraGrid.Columns.GridColumn colModId;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtCommonBar smtCommonBar2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
		private baseSMT.smtSearchButton btnSearch;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
    }
}