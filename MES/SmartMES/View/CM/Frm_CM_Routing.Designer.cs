namespace SmartMES.View.CM
{
    partial class Frm_CM_Routing
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_Routing));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.gcCM_RoutingRev = new DevExpress.XtraGrid.GridControl();
			this.gvCM_RoutingRev = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colRoutingCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colRoutingName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colRoutingType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.routingTypeColLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUseYn = new DevExpress.XtraGrid.Columns.GridColumn();
			this.useYnColCheckEdit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.colRoutingMasterDelete = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnRoutingMasterDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gcCM_Process = new DevExpress.XtraGrid.GridControl();
			this.CM_ProcessBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.gvCM_Process = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colProcessCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colProcessName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colWorkType = new DevExpress.XtraGrid.Columns.GridColumn();
			this.workTypeColLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.useYnColCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.RoutingTreeList = new DevExpress.XtraTreeList.TreeList();
			this.colCorpId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colRoutingCode2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colChildID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colParentId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colRoutingSeq = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colTreeLevel = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colRowNum = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colProcessCode2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colProcessName2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colWorkType2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.workTypeColLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.colDelete = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.btnNodeDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.cmRoutingBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
			this.gcCM_Item = new DevExpress.XtraGrid.GridControl();
			this.gvCM_Item = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colCorpId1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colRoutingCode3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUseYn1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCreId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCreIP = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCreDt = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colModId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colModIP = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colModDt = new DevExpress.XtraGrid.Columns.GridColumn();
			this.cmdMaterial = new DevExpress.XtraEditors.SimpleButton();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
			this.dragDropEvents2 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
			this.dragDropEvents11 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdInsert = new baseSMT.smtSearchButton();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdDelete = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gcCM_RoutingRev)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvCM_RoutingRev)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.routingTypeColLookUpEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.useYnColCheckEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnRoutingMasterDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gcCM_Process)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CM_ProcessBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvCM_Process)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.workTypeColLookUpEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.useYnColCheckEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.RoutingTreeList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.workTypeColLookUpEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNodeDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmRoutingBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gcCM_Item)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvCM_Item)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.gcCM_RoutingRev);
			this.layoutControl1.Controls.Add(this.gcCM_Process);
			this.layoutControl1.Controls.Add(this.RoutingTreeList);
			this.layoutControl1.Controls.Add(this.gcCM_Item);
			this.layoutControl1.Controls.Add(this.cmdMaterial);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1240, 609);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// gcCM_RoutingRev
			// 
			this.gcCM_RoutingRev.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
			this.gcCM_RoutingRev.Location = new System.Drawing.Point(13, 41);
			this.gcCM_RoutingRev.MainView = this.gvCM_RoutingRev;
			this.gcCM_RoutingRev.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.gcCM_RoutingRev.Name = "gcCM_RoutingRev";
			this.gcCM_RoutingRev.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.useYnColCheckEdit,
            this.routingTypeColLookUpEdit,
            this.btnRoutingMasterDelete});
			this.gcCM_RoutingRev.Size = new System.Drawing.Size(456, 554);
			this.gcCM_RoutingRev.TabIndex = 13;
			this.gcCM_RoutingRev.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCM_RoutingRev});
			// 
			// gvCM_RoutingRev
			// 
			this.gvCM_RoutingRev.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gvCM_RoutingRev.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvCM_RoutingRev.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gvCM_RoutingRev.Appearance.Row.Options.UseFont = true;
			this.gvCM_RoutingRev.ColumnPanelRowHeight = 34;
			this.gvCM_RoutingRev.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRoutingCode,
            this.colRoutingName,
            this.colRoutingType,
            this.colRemark,
            this.colUseYn,
            this.colRoutingMasterDelete});
			this.gvCM_RoutingRev.DetailHeight = 397;
			this.gvCM_RoutingRev.FixedLineWidth = 4;
			this.gvCM_RoutingRev.GridControl = this.gcCM_RoutingRev;
			this.gvCM_RoutingRev.Name = "gvCM_RoutingRev";
			this.gvCM_RoutingRev.NewItemRowText = "클릭하면 신규 라우팅을 생성할 수 있습니다.";
			this.gvCM_RoutingRev.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
			this.gvCM_RoutingRev.OptionsDetail.EnableMasterViewMode = false;
			this.gvCM_RoutingRev.OptionsEditForm.EditFormColumnCount = 2;
			this.gvCM_RoutingRev.OptionsView.ColumnAutoWidth = false;
			this.gvCM_RoutingRev.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
			this.gvCM_RoutingRev.OptionsView.ShowGroupPanel = false;
			this.gvCM_RoutingRev.RowHeight = 34;
			this.gvCM_RoutingRev.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvCM_RoutingRev_FocusedRowChanged);
			this.gvCM_RoutingRev.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gvCM_RoutingRev_RowUpdated);
			// 
			// colRoutingCode
			// 
			this.colRoutingCode.Caption = "라우팅코드";
			this.colRoutingCode.FieldName = "RoutingCode";
			this.colRoutingCode.Name = "colRoutingCode";
			this.colRoutingCode.OptionsColumn.AllowEdit = false;
			this.colRoutingCode.OptionsColumn.ReadOnly = true;
			this.colRoutingCode.OptionsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
			this.colRoutingCode.Visible = true;
			this.colRoutingCode.VisibleIndex = 0;
			this.colRoutingCode.Width = 80;
			// 
			// colRoutingName
			// 
			this.colRoutingName.Caption = "라우팅명";
			this.colRoutingName.FieldName = "RoutingName";
			this.colRoutingName.Name = "colRoutingName";
			this.colRoutingName.OptionsEditForm.ColumnSpan = 2;
			this.colRoutingName.OptionsEditForm.UseEditorColRowSpan = false;
			this.colRoutingName.Visible = true;
			this.colRoutingName.VisibleIndex = 1;
			this.colRoutingName.Width = 160;
			// 
			// colRoutingType
			// 
			this.colRoutingType.Caption = "라우팅구분";
			this.colRoutingType.ColumnEdit = this.routingTypeColLookUpEdit;
			this.colRoutingType.FieldName = "RoutingType";
			this.colRoutingType.Name = "colRoutingType";
			this.colRoutingType.OptionsEditForm.VisibleIndex = 1;
			this.colRoutingType.Width = 72;
			// 
			// routingTypeColLookUpEdit
			// 
			this.routingTypeColLookUpEdit.AutoHeight = false;
			this.routingTypeColLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.routingTypeColLookUpEdit.Name = "routingTypeColLookUpEdit";
			this.routingTypeColLookUpEdit.NullText = "";
			// 
			// colRemark
			// 
			this.colRemark.Caption = "비 고";
			this.colRemark.FieldName = "Remark";
			this.colRemark.Name = "colRemark";
			this.colRemark.OptionsEditForm.ColumnSpan = 2;
			this.colRemark.OptionsEditForm.UseEditorColRowSpan = false;
			this.colRemark.OptionsEditForm.VisibleIndex = 3;
			this.colRemark.Visible = true;
			this.colRemark.VisibleIndex = 2;
			this.colRemark.Width = 240;
			// 
			// colUseYn
			// 
			this.colUseYn.Caption = "사용여부";
			this.colUseYn.ColumnEdit = this.useYnColCheckEdit;
			this.colUseYn.FieldName = "UseYn";
			this.colUseYn.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.colUseYn.Name = "colUseYn";
			this.colUseYn.OptionsEditForm.VisibleIndex = 2;
			this.colUseYn.Visible = true;
			this.colUseYn.VisibleIndex = 4;
			this.colUseYn.Width = 64;
			// 
			// useYnColCheckEdit
			// 
			this.useYnColCheckEdit.AutoHeight = false;
			this.useYnColCheckEdit.Name = "useYnColCheckEdit";
			this.useYnColCheckEdit.ValueChecked = "Y";
			this.useYnColCheckEdit.ValueUnchecked = "N";
			// 
			// colRoutingMasterDelete
			// 
			this.colRoutingMasterDelete.Caption = "삭 제";
			this.colRoutingMasterDelete.ColumnEdit = this.btnRoutingMasterDelete;
			this.colRoutingMasterDelete.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
			this.colRoutingMasterDelete.Name = "colRoutingMasterDelete";
			this.colRoutingMasterDelete.Visible = true;
			this.colRoutingMasterDelete.VisibleIndex = 3;
			this.colRoutingMasterDelete.Width = 50;
			// 
			// btnRoutingMasterDelete
			// 
			this.btnRoutingMasterDelete.AutoHeight = false;
			this.btnRoutingMasterDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
			this.btnRoutingMasterDelete.Name = "btnRoutingMasterDelete";
			this.btnRoutingMasterDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.btnRoutingMasterDelete.Click += new System.EventHandler(this.btnRoutingMasterDelete_Click);
			// 
			// gcCM_Process
			// 
			this.gcCM_Process.DataSource = this.CM_ProcessBindingSource;
			this.gcCM_Process.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
			this.gcCM_Process.Location = new System.Drawing.Point(847, 41);
			this.gcCM_Process.MainView = this.gvCM_Process;
			this.gcCM_Process.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.gcCM_Process.Name = "gcCM_Process";
			this.gcCM_Process.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.useYnColCheckEdit1,
            this.workTypeColLookUpEdit});
			this.gcCM_Process.Size = new System.Drawing.Size(380, 306);
			this.gcCM_Process.TabIndex = 13;
			this.gcCM_Process.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCM_Process});
			// 
			// gvCM_Process
			// 
			this.gvCM_Process.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gvCM_Process.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvCM_Process.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.behaviorManager1.SetBehaviors(this.gvCM_Process, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.DragDrop.DragDropBehavior.Create(typeof(DevExpress.XtraGrid.Extensions.ColumnViewDragDropSource), true, true, false, true, this.dragDropEvents2)))});
			this.gvCM_Process.ColumnPanelRowHeight = 34;
			this.gvCM_Process.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProcessCode,
            this.colProcessName,
            this.colWorkType});
			this.gvCM_Process.DetailHeight = 397;
			this.gvCM_Process.FixedLineWidth = 4;
			this.gvCM_Process.GridControl = this.gcCM_Process;
			this.gvCM_Process.Name = "gvCM_Process";
			this.gvCM_Process.NewItemRowText = "클릭하여 권한그룹을 등록하세요.";
			this.gvCM_Process.OptionsBehavior.Editable = false;
			this.gvCM_Process.OptionsDetail.EnableMasterViewMode = false;
			this.gvCM_Process.OptionsView.ColumnAutoWidth = false;
			this.gvCM_Process.OptionsView.ShowGroupPanel = false;
			this.gvCM_Process.RowHeight = 34;
			this.gvCM_Process.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvCM_Process_RowCellStyle);
			// 
			// colProcessCode
			// 
			this.colProcessCode.Caption = "공정코드";
			this.colProcessCode.FieldName = "ProcessCode";
			this.colProcessCode.Name = "colProcessCode";
			this.colProcessCode.Visible = true;
			this.colProcessCode.VisibleIndex = 0;
			this.colProcessCode.Width = 80;
			// 
			// colProcessName
			// 
			this.colProcessName.Caption = "공정명";
			this.colProcessName.FieldName = "ProcessName";
			this.colProcessName.Name = "colProcessName";
			this.colProcessName.Visible = true;
			this.colProcessName.VisibleIndex = 1;
			this.colProcessName.Width = 160;
			// 
			// colWorkType
			// 
			this.colWorkType.Caption = "작업구분";
			this.colWorkType.ColumnEdit = this.workTypeColLookUpEdit;
			this.colWorkType.FieldName = "WorkType";
			this.colWorkType.Name = "colWorkType";
			this.colWorkType.Visible = true;
			this.colWorkType.VisibleIndex = 2;
			this.colWorkType.Width = 96;
			// 
			// workTypeColLookUpEdit
			// 
			this.workTypeColLookUpEdit.AutoHeight = false;
			this.workTypeColLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.workTypeColLookUpEdit.Name = "workTypeColLookUpEdit";
			this.workTypeColLookUpEdit.NullText = "";
			// 
			// useYnColCheckEdit1
			// 
			this.useYnColCheckEdit1.AutoHeight = false;
			this.useYnColCheckEdit1.Name = "useYnColCheckEdit1";
			this.useYnColCheckEdit1.ValueChecked = "Y";
			this.useYnColCheckEdit1.ValueUnchecked = "N";
			// 
			// RoutingTreeList
			// 
			this.RoutingTreeList.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.RoutingTreeList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.RoutingTreeList.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.behaviorManager1.SetBehaviors(this.RoutingTreeList, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.DragDrop.DragDropBehavior.Create(typeof(DevExpress.XtraTreeList.TreeListDragDropSource), true, true, true, true, this.dragDropEvents11)))});
			this.RoutingTreeList.ColumnPanelRowHeight = 31;
			this.RoutingTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCorpId,
            this.colRoutingCode2,
            this.colChildID,
            this.colParentId,
            this.colRoutingSeq,
            this.colTreeLevel,
            this.colRowNum,
            this.colProcessCode2,
            this.colProcessName2,
            this.colWorkType2,
            this.colDelete});
			this.RoutingTreeList.Cursor = System.Windows.Forms.Cursors.Default;
			this.RoutingTreeList.DataSource = this.cmRoutingBindingSource;
			this.RoutingTreeList.KeyFieldName = "ChildID";
			this.RoutingTreeList.Location = new System.Drawing.Point(496, 42);
			this.RoutingTreeList.Name = "RoutingTreeList";
			this.RoutingTreeList.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.MouseDown;
			this.RoutingTreeList.OptionsBehavior.PopulateServiceColumns = true;
			this.RoutingTreeList.OptionsView.AutoWidth = false;
			this.RoutingTreeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnNodeDelete,
            this.workTypeColLookUpEdit1});
			this.RoutingTreeList.RowHeight = 31;
			this.RoutingTreeList.ShowButtonMode = DevExpress.XtraTreeList.ShowButtonModeEnum.Default;
			this.RoutingTreeList.Size = new System.Drawing.Size(324, 304);
			this.RoutingTreeList.StateImageList = this.imageCollection1;
			this.RoutingTreeList.TabIndex = 179;
			// 
			// colCorpId
			// 
			this.colCorpId.FieldName = "CorpId";
			this.colCorpId.Name = "colCorpId";
			this.colCorpId.OptionsColumn.AllowEdit = false;
			// 
			// colRoutingCode2
			// 
			this.colRoutingCode2.FieldName = "RoutingCode";
			this.colRoutingCode2.Name = "colRoutingCode2";
			this.colRoutingCode2.OptionsColumn.AllowEdit = false;
			// 
			// colChildID
			// 
			this.colChildID.FieldName = "ChildID";
			this.colChildID.Name = "colChildID";
			this.colChildID.OptionsColumn.AllowEdit = false;
			this.colChildID.Width = 90;
			// 
			// colParentId
			// 
			this.colParentId.FieldName = "ParentId";
			this.colParentId.Name = "colParentId";
			this.colParentId.OptionsColumn.AllowEdit = false;
			this.colParentId.Width = 90;
			// 
			// colRoutingSeq
			// 
			this.colRoutingSeq.FieldName = "RoutingSeq";
			this.colRoutingSeq.Name = "colRoutingSeq";
			this.colRoutingSeq.OptionsColumn.AllowEdit = false;
			// 
			// colTreeLevel
			// 
			this.colTreeLevel.FieldName = "TreeLevel";
			this.colTreeLevel.Name = "colTreeLevel";
			this.colTreeLevel.OptionsColumn.AllowEdit = false;
			// 
			// colRowNum
			// 
			this.colRowNum.Caption = "순번";
			this.colRowNum.FieldName = "RowNum";
			this.colRowNum.Name = "colRowNum";
			this.colRowNum.OptionsColumn.AllowEdit = false;
			this.colRowNum.Width = 50;
			// 
			// colProcessCode2
			// 
			this.colProcessCode2.Caption = "공정코드";
			this.colProcessCode2.FieldName = "ProcessCode";
			this.colProcessCode2.Name = "colProcessCode2";
			this.colProcessCode2.OptionsColumn.AllowEdit = false;
			this.colProcessCode2.Visible = true;
			this.colProcessCode2.VisibleIndex = 0;
			this.colProcessCode2.Width = 100;
			// 
			// colProcessName2
			// 
			this.colProcessName2.AppearanceHeader.Options.UseTextOptions = true;
			this.colProcessName2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colProcessName2.Caption = "공정명";
			this.colProcessName2.FieldName = "ProcessName";
			this.colProcessName2.Name = "colProcessName2";
			this.colProcessName2.OptionsColumn.AllowEdit = false;
			this.colProcessName2.Visible = true;
			this.colProcessName2.VisibleIndex = 1;
			this.colProcessName2.Width = 200;
			// 
			// colWorkType2
			// 
			this.colWorkType2.Caption = "작업구분";
			this.colWorkType2.ColumnEdit = this.workTypeColLookUpEdit1;
			this.colWorkType2.FieldName = "WorkType";
			this.colWorkType2.Name = "colWorkType2";
			this.colWorkType2.Visible = true;
			this.colWorkType2.VisibleIndex = 2;
			// 
			// workTypeColLookUpEdit1
			// 
			this.workTypeColLookUpEdit1.AutoHeight = false;
			this.workTypeColLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.workTypeColLookUpEdit1.Name = "workTypeColLookUpEdit1";
			// 
			// colDelete
			// 
			this.colDelete.Caption = "삭제";
			this.colDelete.ColumnEdit = this.btnNodeDelete;
			this.colDelete.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Right;
			this.colDelete.MinWidth = 50;
			this.colDelete.Name = "colDelete";
			this.colDelete.OptionsColumn.AllowMove = false;
			this.colDelete.Visible = true;
			this.colDelete.VisibleIndex = 3;
			this.colDelete.Width = 50;
			// 
			// btnNodeDelete
			// 
			this.btnNodeDelete.AutoHeight = false;
			this.btnNodeDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
			this.btnNodeDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.btnNodeDelete.Name = "btnNodeDelete";
			this.btnNodeDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.btnNodeDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnNodeDelete_ButtonClick);
			// 
			// imageCollection1
			// 
			this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
			this.imageCollection1.Images.SetKeyName(0, "copy.png");
			this.imageCollection1.Images.SetKeyName(1, "folder_close.png");
			this.imageCollection1.Images.SetKeyName(2, "folder_open.png");
			this.imageCollection1.Images.SetKeyName(3, "folder.png");
			this.imageCollection1.Images.SetKeyName(4, "writing.png");
			// 
			// gcCM_Item
			// 
			this.gcCM_Item.Location = new System.Drawing.Point(501, 441);
			this.gcCM_Item.MainView = this.gvCM_Item;
			this.gcCM_Item.Name = "gcCM_Item";
			this.gcCM_Item.Size = new System.Drawing.Size(720, 149);
			this.gcCM_Item.TabIndex = 13;
			this.gcCM_Item.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCM_Item});
			// 
			// gvCM_Item
			// 
			this.gvCM_Item.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gvCM_Item.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvCM_Item.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gvCM_Item.ColumnPanelRowHeight = 34;
			this.gvCM_Item.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCorpId1,
            this.colItemCode,
            this.gridColumn2,
            this.gridColumn11,
            this.colRoutingCode3,
            this.colUseYn1,
            this.colCreId,
            this.colCreIP,
            this.colCreDt,
            this.colModId,
            this.colModIP,
            this.colModDt});
			this.gvCM_Item.GridControl = this.gcCM_Item;
			this.gvCM_Item.Name = "gvCM_Item";
			this.gvCM_Item.OptionsDetail.EnableMasterViewMode = false;
			this.gvCM_Item.OptionsView.ColumnAutoWidth = false;
			this.gvCM_Item.OptionsView.ShowGroupPanel = false;
			this.gvCM_Item.RowHeight = 34;
			// 
			// colCorpId1
			// 
			this.colCorpId1.AppearanceHeader.Options.UseTextOptions = true;
			this.colCorpId1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCorpId1.FieldName = "CorpId";
			this.colCorpId1.Name = "colCorpId1";
			this.colCorpId1.OptionsColumn.AllowEdit = false;
			// 
			// colItemCode
			// 
			this.colItemCode.AppearanceHeader.Options.UseTextOptions = true;
			this.colItemCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colItemCode.Caption = "품목코드";
			this.colItemCode.FieldName = "ItemCode";
			this.colItemCode.Name = "colItemCode";
			this.colItemCode.OptionsColumn.AllowEdit = false;
			this.colItemCode.Visible = true;
			this.colItemCode.VisibleIndex = 0;
			this.colItemCode.Width = 133;
			// 
			// gridColumn2
			// 
			this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn2.Caption = "품명";
			this.gridColumn2.FieldName = "ItemName";
			this.gridColumn2.Name = "gridColumn2";
			this.gridColumn2.OptionsColumn.AllowEdit = false;
			this.gridColumn2.Visible = true;
			this.gridColumn2.VisibleIndex = 1;
			this.gridColumn2.Width = 250;
			// 
			// gridColumn11
			// 
			this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
			this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumn11.Caption = "규격";
			this.gridColumn11.FieldName = "Size";
			this.gridColumn11.Name = "gridColumn11";
			this.gridColumn11.OptionsColumn.AllowEdit = false;
			this.gridColumn11.Visible = true;
			this.gridColumn11.VisibleIndex = 2;
			this.gridColumn11.Width = 200;
			// 
			// colRoutingCode3
			// 
			this.colRoutingCode3.AppearanceHeader.Options.UseTextOptions = true;
			this.colRoutingCode3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colRoutingCode3.FieldName = "RoutingCode";
			this.colRoutingCode3.Name = "colRoutingCode3";
			this.colRoutingCode3.OptionsColumn.AllowEdit = false;
			// 
			// colUseYn1
			// 
			this.colUseYn1.AppearanceHeader.Options.UseTextOptions = true;
			this.colUseYn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colUseYn1.Caption = "사용";
			this.colUseYn1.FieldName = "UseYn";
			this.colUseYn1.Name = "colUseYn1";
			this.colUseYn1.OptionsColumn.AllowEdit = false;
			this.colUseYn1.Visible = true;
			this.colUseYn1.VisibleIndex = 3;
			this.colUseYn1.Width = 50;
			// 
			// colCreId
			// 
			this.colCreId.AppearanceHeader.Options.UseTextOptions = true;
			this.colCreId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreId.FieldName = "CreId";
			this.colCreId.Name = "colCreId";
			this.colCreId.OptionsColumn.AllowEdit = false;
			// 
			// colCreIP
			// 
			this.colCreIP.AppearanceHeader.Options.UseTextOptions = true;
			this.colCreIP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreIP.FieldName = "CreIP";
			this.colCreIP.Name = "colCreIP";
			this.colCreIP.OptionsColumn.AllowEdit = false;
			// 
			// colCreDt
			// 
			this.colCreDt.AppearanceHeader.Options.UseTextOptions = true;
			this.colCreDt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreDt.FieldName = "CreDt";
			this.colCreDt.Name = "colCreDt";
			this.colCreDt.OptionsColumn.AllowEdit = false;
			// 
			// colModId
			// 
			this.colModId.AppearanceHeader.Options.UseTextOptions = true;
			this.colModId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colModId.FieldName = "ModId";
			this.colModId.Name = "colModId";
			this.colModId.OptionsColumn.AllowEdit = false;
			// 
			// colModIP
			// 
			this.colModIP.AppearanceHeader.Options.UseTextOptions = true;
			this.colModIP.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colModIP.FieldName = "ModIP";
			this.colModIP.Name = "colModIP";
			this.colModIP.OptionsColumn.AllowEdit = false;
			// 
			// colModDt
			// 
			this.colModDt.AppearanceHeader.Options.UseTextOptions = true;
			this.colModDt.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colModDt.FieldName = "ModDt";
			this.colModDt.Name = "colModDt";
			this.colModDt.OptionsColumn.AllowEdit = false;
			// 
			// cmdMaterial
			// 
			this.cmdMaterial.Location = new System.Drawing.Point(501, 407);
			this.cmdMaterial.Name = "cmdMaterial";
			this.cmdMaterial.Size = new System.Drawing.Size(720, 28);
			this.cmdMaterial.StyleController = this.layoutControl1;
			this.cmdMaterial.TabIndex = 14;
			this.cmdMaterial.Text = "제품코드 등록";
			this.cmdMaterial.Visible = false;
			this.cmdMaterial.Click += new System.EventHandler(this.cmdMaterial_Click);
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlGroup5});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1240, 609);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.CustomizationFormText = "권한 그룹";
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 3;
			this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 7, 7);
			this.layoutControlGroup2.Size = new System.Drawing.Size(482, 609);
			this.layoutControlGroup2.Text = "라우팅 리스트";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.gcCM_RoutingRev;
			this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.MaxSize = new System.Drawing.Size(462, 0);
			this.layoutControlItem2.MinSize = new System.Drawing.Size(462, 24);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlItem2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlItem2.Size = new System.Drawing.Size(462, 560);
			this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.CustomizationFormText = "권한 그룹";
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup3.Location = new System.Drawing.Point(834, 0);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 3;
			this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlGroup3.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 7, 7);
			this.layoutControlGroup3.Size = new System.Drawing.Size(406, 361);
			this.layoutControlGroup3.Text = "공정 리스트";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.gcCM_Process;
			this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem1.CustomizationFormText = "layoutControlItem2";
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.MaxSize = new System.Drawing.Size(386, 0);
			this.layoutControlItem1.MinSize = new System.Drawing.Size(386, 24);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem1.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlItem1.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem1.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlItem1.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem1.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlItem1.Size = new System.Drawing.Size(386, 312);
			this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem1.Text = "layoutControlItem2";
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroup4
			// 
			this.layoutControlGroup4.CustomizationFormText = "부서 트리";
			this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
			this.layoutControlGroup4.Location = new System.Drawing.Point(482, 0);
			this.layoutControlGroup4.Name = "layoutControlGroup4";
			this.layoutControlGroup4.OptionsItemText.TextToControlDistance = 3;
			this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 8, 8);
			this.layoutControlGroup4.Size = new System.Drawing.Size(352, 361);
			this.layoutControlGroup4.Text = "라우팅 트리";
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.RoutingTreeList;
			this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem3.CustomizationFormText = "layoutControlItem1";
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(330, 310);
			this.layoutControlItem3.Text = "layoutControlItem1";
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlGroup5
			// 
			this.layoutControlGroup5.CustomizationFormText = "품번 리스트";
			this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem4});
			this.layoutControlGroup5.Location = new System.Drawing.Point(482, 361);
			this.layoutControlGroup5.Name = "layoutControlGroup5";
			this.layoutControlGroup5.OptionsItemText.TextToControlDistance = 3;
			this.layoutControlGroup5.Size = new System.Drawing.Size(758, 248);
			this.layoutControlGroup5.Text = "사용 리스트";
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.gcCM_Item;
			this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 34);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(726, 155);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.cmdMaterial;
			this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem4.CustomizationFormText = "layoutControlItem1";
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(726, 34);
			this.layoutControlItem4.Text = "layoutControlItem1";
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdInsert);
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdDelete);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1240, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "공정라우팅 관리";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 6;
			// 
			// cmdInsert
			// 
			this.cmdInsert.AllowFocus = false;
			this.cmdInsert.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdInsert.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdInsert.Appearance.Options.UseBackColor = true;
			this.cmdInsert.Appearance.Options.UseForeColor = true;
			this.cmdInsert.AutoSize = true;
			this.cmdInsert.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdInsert.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdInsert.ImageOptions.ImageIndex = 1;
			this.cmdInsert.ImageOptions.ImageToTextIndent = 15;
			this.cmdInsert.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdInsert.Location = new System.Drawing.Point(932, 0);
			this.cmdInsert.Name = "cmdInsert";
			this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdInsert.Size = new System.Drawing.Size(77, 40);
			this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdInsert.TabIndex = 7;
			this.cmdInsert.Text = "등록";
			this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
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
			this.cmdDisplay.Location = new System.Drawing.Point(131, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Visible = false;
			// 
			// cmdSave
			// 
			this.cmdSave.AllowFocus = false;
			this.cmdSave.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdSave.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdSave.Appearance.Options.UseBackColor = true;
			this.cmdSave.Appearance.Options.UseForeColor = true;
			this.cmdSave.AutoSize = true;
			this.cmdSave.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdSave.ImageOptions.ImageIndex = 4;
			this.cmdSave.ImageOptions.ImageToTextIndent = 15;
			this.cmdSave.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdSave.Location = new System.Drawing.Point(1009, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// cmdDelete
			// 
			this.cmdDelete.AllowFocus = false;
			this.cmdDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDelete.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDelete.Appearance.Options.UseBackColor = true;
			this.cmdDelete.Appearance.Options.UseForeColor = true;
			this.cmdDelete.AutoSize = true;
			this.cmdDelete.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDelete.ImageOptions.ImageIndex = 3;
			this.cmdDelete.ImageOptions.ImageToTextIndent = 15;
			this.cmdDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDelete.Location = new System.Drawing.Point(1086, 0);
			this.cmdDelete.Name = "cmdDelete";
			this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDelete.Size = new System.Drawing.Size(77, 40);
			this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDelete.TabIndex = 2;
			this.cmdDelete.Text = "삭제";
			this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(1163, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// Frm_CM_Routing
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1240, 649);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_CM_Routing";
			this.Text = "공정라우팅 관리";
			this.Load += new System.EventHandler(this.Frm_CM_Routing_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gcCM_RoutingRev)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvCM_RoutingRev)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.routingTypeColLookUpEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.useYnColCheckEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnRoutingMasterDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gcCM_Process)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CM_ProcessBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvCM_Process)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.workTypeColLookUpEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.useYnColCheckEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.RoutingTreeList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.workTypeColLookUpEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNodeDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmRoutingBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gcCM_Item)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvCM_Item)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcCM_RoutingRev;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCM_RoutingRev;
        private DevExpress.XtraGrid.Columns.GridColumn colRoutingCode;
        private DevExpress.XtraGrid.Columns.GridColumn colRoutingName;
        private DevExpress.XtraGrid.Columns.GridColumn colRoutingType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit routingTypeColLookUpEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colUseYn;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit useYnColCheckEdit;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl gcCM_Process;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCM_Process;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessName;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit workTypeColLookUpEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit useYnColCheckEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraTreeList.TreeList RoutingTreeList;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents11;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCorpId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRowNum;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colChildID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnNodeDelete;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource CM_ProcessBindingSource;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRoutingCode2;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colTreeLevel;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProcessCode2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProcessName2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRoutingSeq;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colWorkType2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit workTypeColLookUpEdit1;
        private System.Windows.Forms.BindingSource cmRoutingBindingSource;
        private DevExpress.XtraGrid.GridControl gcCM_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCM_Item;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId1;
        private DevExpress.XtraGrid.Columns.GridColumn colItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn colRoutingCode3;
        private DevExpress.XtraGrid.Columns.GridColumn colUseYn1;
        private DevExpress.XtraGrid.Columns.GridColumn colCreId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreIP;
        private DevExpress.XtraGrid.Columns.GridColumn colCreDt;
        private DevExpress.XtraGrid.Columns.GridColumn colModId;
        private DevExpress.XtraGrid.Columns.GridColumn colModIP;
        private DevExpress.XtraGrid.Columns.GridColumn colModDt;
        private DevExpress.XtraEditors.SimpleButton cmdMaterial;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colRoutingMasterDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnRoutingMasterDelete;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdInsert;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDelete;
		private baseSMT.smtSearchButton cmdClose;
	}
}