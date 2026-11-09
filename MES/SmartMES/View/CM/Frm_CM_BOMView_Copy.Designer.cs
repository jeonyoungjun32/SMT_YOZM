namespace SmartMES.View.CM
{
    partial class Frm_CM_BOMView_Copy
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_BOMView_Copy));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.LupRevision = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.LupMaterial = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.LblSearchCustomer = new DevExpress.XtraEditors.LabelControl();
			this.gcMaterial = new DevExpress.XtraGrid.GridControl();
			this.gvMaterial = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.BomTreeList = new DevExpress.XtraTreeList.TreeList();
			this.colCorpId = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colItemCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colRevision2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colMateriaTypeName1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colItemCode1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colItemName1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colProcessCode1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colProcessName1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colRoutingSeq = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colChildID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colParentID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colQty = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repositoryItemSpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.colUnitCode = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colRemark1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colNodeDel = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.btnNoDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.colRowNum = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.colProcessType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.btnNodeDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			this.dragDropEvents51 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LupRevision.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LupMaterial.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gcMaterial)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvMaterial)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BomTreeList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNoDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNodeDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.panelControl1);
			this.layoutControl1.Controls.Add(this.gcMaterial);
			this.layoutControl1.Controls.Add(this.BomTreeList);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1458, 808);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// panelControl1
			// 
			this.panelControl1.Controls.Add(this.LupRevision);
			this.panelControl1.Controls.Add(this.labelControl1);
			this.panelControl1.Controls.Add(this.LupMaterial);
			this.panelControl1.Controls.Add(this.LblSearchCustomer);
			this.panelControl1.Location = new System.Drawing.Point(3, 3);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(1452, 58);
			this.panelControl1.TabIndex = 184;
			// 
			// LupRevision
			// 
			this.LupRevision.EditValue = "";
			this.LupRevision.Location = new System.Drawing.Point(425, 11);
			this.LupRevision.Name = "LupRevision";
			this.LupRevision.Properties.AutoHeight = false;
			this.LupRevision.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.LupRevision.Properties.NullText = "";
			this.LupRevision.Properties.PopupView = this.gridView2;
			this.LupRevision.Size = new System.Drawing.Size(186, 26);
			this.LupRevision.TabIndex = 291;
			this.LupRevision.EditValueChanged += new System.EventHandler(this.LupRevision_EditValueChanged);
			// 
			// gridView2
			// 
			this.gridView2.DetailHeight = 340;
			this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsEditForm.PopupEditFormWidth = 914;
			this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView2.OptionsView.ShowGroupPanel = false;
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl1.Location = new System.Drawing.Point(314, 11);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.labelControl1.Size = new System.Drawing.Size(105, 26);
			this.labelControl1.TabIndex = 290;
			this.labelControl1.Text = "원본 BOM 리비전";
			// 
			// LupMaterial
			// 
			this.LupMaterial.EditValue = "";
			this.LupMaterial.Location = new System.Drawing.Point(118, 11);
			this.LupMaterial.Name = "LupMaterial";
			this.LupMaterial.Properties.AutoHeight = false;
			this.LupMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.LupMaterial.Properties.NullText = "";
			this.LupMaterial.Properties.PopupView = this.gridView1;
			this.LupMaterial.Size = new System.Drawing.Size(186, 26);
			this.LupMaterial.TabIndex = 289;
			this.LupMaterial.EditValueChanged += new System.EventHandler(this.LupMaterial_EditValueChanged);
			// 
			// gridView1
			// 
			this.gridView1.DetailHeight = 340;
			this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsEditForm.PopupEditFormWidth = 914;
			this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView1.OptionsView.ShowGroupPanel = false;
			// 
			// LblSearchCustomer
			// 
			this.LblSearchCustomer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblSearchCustomer.Location = new System.Drawing.Point(7, 11);
			this.LblSearchCustomer.Name = "LblSearchCustomer";
			this.LblSearchCustomer.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.LblSearchCustomer.Size = new System.Drawing.Size(105, 26);
			this.LblSearchCustomer.TabIndex = 288;
			this.LblSearchCustomer.Text = "원본 BOM 품목";
			// 
			// gcMaterial
			// 
			this.gcMaterial.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
			this.gcMaterial.Location = new System.Drawing.Point(779, 110);
			this.gcMaterial.MainView = this.gvMaterial;
			this.gcMaterial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.gcMaterial.Name = "gcMaterial";
			this.gcMaterial.Size = new System.Drawing.Size(660, 679);
			this.gcMaterial.TabIndex = 6;
			this.gcMaterial.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMaterial});
			// 
			// gvMaterial
			// 
			this.gvMaterial.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gvMaterial.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvMaterial.Appearance.Row.Options.UseFont = true;
			this.gvMaterial.Appearance.Row.Options.UseTextOptions = true;
			this.gvMaterial.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gvMaterial.ColumnPanelRowHeight = 29;
			this.gvMaterial.DetailHeight = 425;
			this.gvMaterial.GridControl = this.gcMaterial;
			this.gvMaterial.Name = "gvMaterial";
			this.gvMaterial.OptionsBehavior.Editable = false;
			this.gvMaterial.OptionsBehavior.ReadOnly = true;
			this.gvMaterial.OptionsDetail.EnableMasterViewMode = false;
			this.gvMaterial.OptionsSelection.MultiSelect = true;
			this.gvMaterial.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
			this.gvMaterial.OptionsView.ColumnAutoWidth = false;
			this.gvMaterial.OptionsView.ShowAutoFilterRow = true;
			this.gvMaterial.OptionsView.ShowGroupPanel = false;
			this.gvMaterial.RowHeight = 29;
			// 
			// BomTreeList
			// 
			this.BomTreeList.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.BomTreeList.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.BomTreeList.ColumnPanelRowHeight = 34;
			this.BomTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCorpId,
            this.colItemCode,
            this.colRevision2,
            this.treeListColumn1,
            this.colMateriaTypeName1,
            this.colItemCode1,
            this.colItemName1,
            this.colProcessCode1,
            this.colProcessName1,
            this.colRoutingSeq,
            this.colChildID,
            this.colParentID,
            this.colQty,
            this.colUnitCode,
            this.colRemark1,
            this.colNodeDel,
            this.colRowNum,
            this.colProcessType});
			this.BomTreeList.Cursor = System.Windows.Forms.Cursors.Default;
			this.BomTreeList.KeyFieldName = "ChildID";
			this.BomTreeList.Location = new System.Drawing.Point(19, 110);
			this.BomTreeList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.BomTreeList.MinWidth = 19;
			this.BomTreeList.Name = "BomTreeList";
			this.BomTreeList.OptionsBehavior.PopulateServiceColumns = true;
			this.BomTreeList.OptionsView.AutoWidth = false;
			this.BomTreeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btnNodeDelete,
            this.btnNoDelete,
            this.repositoryItemSpinEdit2});
			this.BomTreeList.RowHeight = 34;
			this.BomTreeList.Size = new System.Drawing.Size(706, 679);
			this.BomTreeList.TabIndex = 178;
			this.BomTreeList.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.BomTreeList_CustomNodeCellEdit);
			// 
			// colCorpId
			// 
			this.colCorpId.FieldName = "CorpId";
			this.colCorpId.MinWidth = 19;
			this.colCorpId.Name = "colCorpId";
			this.colCorpId.OptionsColumn.AllowEdit = false;
			this.colCorpId.OptionsColumn.AllowSort = true;
			// 
			// colItemCode
			// 
			this.colItemCode.FieldName = "ItemCode";
			this.colItemCode.MinWidth = 19;
			this.colItemCode.Name = "colItemCode";
			this.colItemCode.OptionsColumn.AllowEdit = false;
			this.colItemCode.OptionsColumn.AllowSort = true;
			// 
			// colRevision2
			// 
			this.colRevision2.FieldName = "Revision";
			this.colRevision2.MinWidth = 19;
			this.colRevision2.Name = "colRevision2";
			this.colRevision2.OptionsColumn.AllowEdit = false;
			this.colRevision2.OptionsColumn.AllowSort = true;
			// 
			// treeListColumn1
			// 
			this.treeListColumn1.Caption = "(공정) 자재명";
			this.treeListColumn1.FieldName = "DisplayName";
			this.treeListColumn1.MinWidth = 19;
			this.treeListColumn1.Name = "treeListColumn1";
			this.treeListColumn1.OptionsColumn.AllowEdit = false;
			this.treeListColumn1.OptionsColumn.AllowSort = true;
			this.treeListColumn1.Visible = true;
			this.treeListColumn1.VisibleIndex = 0;
			this.treeListColumn1.Width = 391;
			// 
			// colMateriaTypeName1
			// 
			this.colMateriaTypeName1.Caption = "품목구분";
			this.colMateriaTypeName1.FieldName = "ItemTypeName";
			this.colMateriaTypeName1.Name = "colMateriaTypeName1";
			this.colMateriaTypeName1.OptionsColumn.AllowEdit = false;
			this.colMateriaTypeName1.OptionsColumn.AllowSort = true;
			this.colMateriaTypeName1.Visible = true;
			this.colMateriaTypeName1.VisibleIndex = 1;
			this.colMateriaTypeName1.Width = 70;
			// 
			// colItemCode1
			// 
			this.colItemCode1.Caption = "품목코드";
			this.colItemCode1.FieldName = "ItemCode";
			this.colItemCode1.MinWidth = 19;
			this.colItemCode1.Name = "colItemCode1";
			this.colItemCode1.OptionsColumn.AllowEdit = false;
			this.colItemCode1.OptionsColumn.AllowSort = true;
			this.colItemCode1.Visible = true;
			this.colItemCode1.VisibleIndex = 2;
			this.colItemCode1.Width = 70;
			// 
			// colItemName1
			// 
			this.colItemName1.Caption = "품명";
			this.colItemName1.FieldName = "ItemName";
			this.colItemName1.MinWidth = 19;
			this.colItemName1.Name = "colItemName1";
			this.colItemName1.OptionsColumn.AllowEdit = false;
			this.colItemName1.OptionsColumn.AllowSort = true;
			this.colItemName1.Width = 150;
			// 
			// colProcessCode1
			// 
			this.colProcessCode1.Caption = "공정코드";
			this.colProcessCode1.FieldName = "ProcessCode";
			this.colProcessCode1.MinWidth = 19;
			this.colProcessCode1.Name = "colProcessCode1";
			this.colProcessCode1.OptionsColumn.AllowEdit = false;
			this.colProcessCode1.OptionsColumn.AllowSort = true;
			this.colProcessCode1.Width = 70;
			// 
			// colProcessName1
			// 
			this.colProcessName1.Caption = "공정명";
			this.colProcessName1.FieldName = "ProcessName";
			this.colProcessName1.MinWidth = 19;
			this.colProcessName1.Name = "colProcessName1";
			this.colProcessName1.OptionsColumn.AllowEdit = false;
			this.colProcessName1.OptionsColumn.AllowSort = true;
			this.colProcessName1.Width = 99;
			// 
			// colRoutingSeq
			// 
			this.colRoutingSeq.Caption = "라우팅";
			this.colRoutingSeq.FieldName = "RoutingSeq";
			this.colRoutingSeq.MinWidth = 19;
			this.colRoutingSeq.Name = "colRoutingSeq";
			this.colRoutingSeq.OptionsColumn.AllowEdit = false;
			this.colRoutingSeq.OptionsColumn.AllowSort = true;
			this.colRoutingSeq.Width = 59;
			// 
			// colChildID
			// 
			this.colChildID.Caption = "ChildID";
			this.colChildID.FieldName = "ChildID";
			this.colChildID.MinWidth = 19;
			this.colChildID.Name = "colChildID";
			this.colChildID.OptionsColumn.AllowEdit = false;
			this.colChildID.OptionsColumn.AllowSort = true;
			this.colChildID.Width = 59;
			// 
			// colParentID
			// 
			this.colParentID.Caption = "ParentID";
			this.colParentID.FieldName = "ParentID";
			this.colParentID.MinWidth = 19;
			this.colParentID.Name = "colParentID";
			this.colParentID.OptionsColumn.AllowEdit = false;
			this.colParentID.OptionsColumn.AllowSort = true;
			this.colParentID.Width = 59;
			// 
			// colQty
			// 
			this.colQty.AppearanceCell.Options.UseTextOptions = true;
			this.colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colQty.Caption = "수량";
			this.colQty.ColumnEdit = this.repositoryItemSpinEdit2;
			this.colQty.FieldName = "Qty";
			this.colQty.Format.FormatString = "#,0.####";
			this.colQty.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colQty.MinWidth = 19;
			this.colQty.Name = "colQty";
			this.colQty.OptionsColumn.AllowSort = true;
			this.colQty.Visible = true;
			this.colQty.VisibleIndex = 3;
			this.colQty.Width = 46;
			// 
			// repositoryItemSpinEdit2
			// 
			this.repositoryItemSpinEdit2.AutoHeight = false;
			this.repositoryItemSpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemSpinEdit2.DisplayFormat.FormatString = "#,0.####";
			this.repositoryItemSpinEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemSpinEdit2.EditFormat.FormatString = "#,0.####";
			this.repositoryItemSpinEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.repositoryItemSpinEdit2.Name = "repositoryItemSpinEdit2";
			// 
			// colUnitCode
			// 
			this.colUnitCode.AppearanceCell.Options.UseTextOptions = true;
			this.colUnitCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colUnitCode.Caption = "단위";
			this.colUnitCode.FieldName = "UnitCode";
			this.colUnitCode.MinWidth = 19;
			this.colUnitCode.Name = "colUnitCode";
			this.colUnitCode.OptionsColumn.AllowEdit = false;
			this.colUnitCode.OptionsColumn.AllowSort = true;
			this.colUnitCode.OptionsColumn.FixedWidth = true;
			this.colUnitCode.Visible = true;
			this.colUnitCode.VisibleIndex = 4;
			this.colUnitCode.Width = 46;
			// 
			// colRemark1
			// 
			this.colRemark1.Caption = "비고";
			this.colRemark1.FieldName = "Remark";
			this.colRemark1.MinWidth = 19;
			this.colRemark1.Name = "colRemark1";
			this.colRemark1.OptionsColumn.AllowSort = true;
			this.colRemark1.Width = 144;
			// 
			// colNodeDel
			// 
			this.colNodeDel.AppearanceCell.Options.UseTextOptions = true;
			this.colNodeDel.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colNodeDel.Caption = "삭제";
			this.colNodeDel.ColumnEdit = this.btnNoDelete;
			this.colNodeDel.FieldName = "삭제";
			this.colNodeDel.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Right;
			this.colNodeDel.MinWidth = 50;
			this.colNodeDel.Name = "colNodeDel";
			this.colNodeDel.OptionsColumn.AllowSort = true;
			this.colNodeDel.Width = 67;
			// 
			// btnNoDelete
			// 
			this.btnNoDelete.AutoHeight = false;
			this.btnNoDelete.Name = "btnNoDelete";
			this.btnNoDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			// 
			// colRowNum
			// 
			this.colRowNum.FieldName = "RowNum";
			this.colRowNum.MinWidth = 19;
			this.colRowNum.Name = "colRowNum";
			this.colRowNum.OptionsColumn.AllowEdit = false;
			this.colRowNum.OptionsColumn.AllowSort = true;
			// 
			// colProcessType
			// 
			this.colProcessType.FieldName = "ProcessType";
			this.colProcessType.MinWidth = 19;
			this.colProcessType.Name = "colProcessType";
			this.colProcessType.OptionsColumn.AllowEdit = false;
			this.colProcessType.OptionsColumn.AllowSort = true;
			// 
			// btnNodeDelete
			// 
			this.btnNodeDelete.AutoHeight = false;
			this.btnNodeDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
			this.btnNodeDelete.Name = "btnNodeDelete";
			this.btnNodeDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
			this.btnNodeDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnNodeDelete_ButtonClick);
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup3,
            this.layoutControlGroup1,
            this.splitterItem1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1458, 808);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.panelControl1;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1458, 64);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.CustomizationFormText = "원자재 리스트";
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
			this.layoutControlGroup3.Location = new System.Drawing.Point(760, 64);
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
			this.layoutControlGroup3.Size = new System.Drawing.Size(698, 744);
			this.layoutControlGroup3.Text = "복사 품목 리스트";
			// 
			// layoutControlItem8
			// 
			this.layoutControlItem8.Control = this.gcMaterial;
			this.layoutControlItem8.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
			this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem8.MinSize = new System.Drawing.Size(106, 26);
			this.layoutControlItem8.Name = "layoutControlItem8";
			this.layoutControlItem8.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem8.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlItem8.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem8.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlItem8.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem8.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlItem8.Size = new System.Drawing.Size(666, 685);
			this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem8.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 64);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(744, 744);
			this.layoutControlGroup1.Text = "원본 BOM 리스트";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem2.AppearanceItemCaptionDisabled.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.AppearanceItemCaptionDisabled.Options.UseFont = true;
			this.layoutControlItem2.Control = this.BomTreeList;
			this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.MinSize = new System.Drawing.Size(106, 26);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItemControl.Options.UseFont = true;
			this.layoutControlItem2.OptionsPrint.AppearanceItemText.Font = new System.Drawing.Font("굴림", 9F);
			this.layoutControlItem2.OptionsPrint.AppearanceItemText.Options.UseFont = true;
			this.layoutControlItem2.Size = new System.Drawing.Size(712, 685);
			this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem2.Text = "layoutControlItem1";
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// splitterItem1
			// 
			this.splitterItem1.AllowHotTrack = true;
			this.splitterItem1.Location = new System.Drawing.Point(744, 64);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(16, 744);
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1458, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "BOM 등록";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 6;
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
			this.cmdSave.Location = new System.Drawing.Point(1304, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(1381, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 6;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
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
			this.cmdDisplay.Location = new System.Drawing.Point(102, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Click += new System.EventHandler(this.cmdSearch_Click);
			// 
			// frmBOMView_Copy
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1458, 848);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "Frm_CM_BOMView_Copy";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "BOM 등록";
			this.Load += new System.EventHandler(this.Frm_CM_BOMView_Copy_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LupRevision.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LupMaterial.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gcMaterial)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvMaterial)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BomTreeList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNoDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNodeDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMaterial;
        private DevExpress.XtraTreeList.TreeList BomTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCorpId;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colItemCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colItemCode1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colItemName1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProcessCode1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProcessName1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRoutingSeq;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colChildID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colParentID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colQty;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colUnitCode;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRemark1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNodeDel;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnNoDelete;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRowNum;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnNodeDelete;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRevision2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents51;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colProcessType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colMateriaTypeName1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraEditors.SearchLookUpEdit LupRevision;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.SearchLookUpEdit LupMaterial;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.LabelControl LblSearchCustomer;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private baseSMT.smtSearchButton cmdClose;
	}
}