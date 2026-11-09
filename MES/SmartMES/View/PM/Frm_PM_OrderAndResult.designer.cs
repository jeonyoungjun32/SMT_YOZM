namespace SmartMES.View.PM
{
    partial class Frm_PM_OrderAndResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PM_OrderAndResult));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmdInsert = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.GcProcessResult = new DevExpress.XtraGrid.GridControl();
            this.GvProcessResult = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GcPM_ProductPlan = new DevExpress.XtraGrid.GridControl();
            this.GvPM_ProductPlan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.button4 = new System.Windows.Forms.Button();
            this.dragDropEvents2 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.dragDropEvents1 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.dragDropEvents3 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.dragDropEvents4 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.colCorpId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSdOrderNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSdOrderSeq = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkSeq = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGoodQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRevision = new DevExpress.XtraGrid.Columns.GridColumn();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdPrint = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Material = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.Lup_Process = new baseSMT.smtSearchLookUp();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcProcessResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvProcessResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcPM_ProductPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPM_ProductPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panel4);
            this.layoutControl1.Controls.Add(this.GcProcessResult);
            this.layoutControl1.Controls.Add(this.GcPM_ProductPlan);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1124, 369, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1180, 550);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.panel4.Controls.Add(this.cmdInsert);
            this.panel4.Controls.Add(this.cmdDelete);
            this.panel4.Controls.Add(this.cmdSave);
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(4, 315);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1172, 35);
            this.panel4.TabIndex = 32;
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
            this.cmdInsert.Location = new System.Drawing.Point(941, 0);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdInsert.Size = new System.Drawing.Size(77, 35);
            this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdInsert.TabIndex = 30;
            this.cmdInsert.Tag = "Ins";
            this.cmdInsert.Text = "등록";
            this.cmdInsert.Click += new System.EventHandler(this.cmdRowEdit_Click);
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
            this.cmdDelete.Location = new System.Drawing.Point(1018, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(77, 35);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 32;
            this.cmdDelete.Tag = "Del";
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Click += new System.EventHandler(this.cmdRowEdit_Click);
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
            this.cmdSave.Location = new System.Drawing.Point(1095, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 35);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 31;
            this.cmdSave.Tag = "Ins";
            this.cmdSave.Text = "저장";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // GcProcessResult
            // 
            this.GcProcessResult.Cursor = System.Windows.Forms.Cursors.Default;
            this.GcProcessResult.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.GcProcessResult.Location = new System.Drawing.Point(7, 353);
            this.GcProcessResult.MainView = this.GvProcessResult;
            this.GcProcessResult.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GcProcessResult.Name = "GcProcessResult";
            this.GcProcessResult.Size = new System.Drawing.Size(1166, 190);
            this.GcProcessResult.TabIndex = 2;
            this.GcProcessResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvProcessResult});
            // 
            // GvProcessResult
            // 
            this.GvProcessResult.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GvProcessResult.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GvProcessResult.ColumnPanelRowHeight = 29;
            this.GvProcessResult.DetailHeight = 425;
            this.GvProcessResult.GridControl = this.GcProcessResult;
            this.GvProcessResult.Name = "GvProcessResult";
            this.GvProcessResult.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            this.GvProcessResult.OptionsView.ColumnAutoWidth = false;
            this.GvProcessResult.OptionsView.ShowGroupPanel = false;
            this.GvProcessResult.RowHeight = 29;
            // 
            // GcPM_ProductPlan
            // 
            this.GcPM_ProductPlan.Cursor = System.Windows.Forms.Cursors.Default;
            this.GcPM_ProductPlan.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.GcPM_ProductPlan.Location = new System.Drawing.Point(7, 34);
            this.GcPM_ProductPlan.MainView = this.GvPM_ProductPlan;
            this.GcPM_ProductPlan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GcPM_ProductPlan.Name = "GcPM_ProductPlan";
            this.GcPM_ProductPlan.Size = new System.Drawing.Size(1166, 227);
            this.GcPM_ProductPlan.TabIndex = 0;
            this.GcPM_ProductPlan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvPM_ProductPlan});
            // 
            // GvPM_ProductPlan
            // 
            this.GvPM_ProductPlan.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GvPM_ProductPlan.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GvPM_ProductPlan.ColumnPanelRowHeight = 29;
            this.GvPM_ProductPlan.DetailHeight = 425;
            this.GvPM_ProductPlan.GridControl = this.GcPM_ProductPlan;
            this.GvPM_ProductPlan.Name = "GvPM_ProductPlan";
            this.GvPM_ProductPlan.OptionsBehavior.Editable = false;
            this.GvPM_ProductPlan.OptionsSelection.CheckBoxSelectorColumnWidth = 34;
            this.GvPM_ProductPlan.OptionsView.ColumnAutoWidth = false;
            this.GvPM_ProductPlan.OptionsView.ShowGroupPanel = false;
            this.GvPM_ProductPlan.RowHeight = 29;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup2,
            this.splitterItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1180, 550);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(1180, 268);
            this.layoutControlGroup3.Text = "지시리스트";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.GcPM_ProductPlan;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1172, 233);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem5});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 284);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1180, 266);
            this.layoutControlGroup2.Text = "실적리스트";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.GcProcessResult;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1172, 196);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.panel4;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 35);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(99, 35);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Size = new System.Drawing.Size(1172, 35);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new System.Drawing.Point(0, 268);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(1180, 16);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Right;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("돋움", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = global::SmartMES.Properties.Resources.close;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(718, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(70, 187);
            this.button4.TabIndex = 16;
            this.button4.Text = "     닫기";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // colCorpId
            // 
            this.colCorpId.Caption = "사업장번호";
            this.colCorpId.FieldName = "CorpId";
            this.colCorpId.Name = "colCorpId";
            // 
            // colOrderNo
            // 
            this.colOrderNo.Caption = "지시번호";
            this.colOrderNo.FieldName = "OrderNo";
            this.colOrderNo.Name = "colOrderNo";
            this.colOrderNo.Visible = true;
            this.colOrderNo.VisibleIndex = 0;
            // 
            // colSdOrderNo
            // 
            this.colSdOrderNo.Caption = "수주번호";
            this.colSdOrderNo.FieldName = "SdOrderNo";
            this.colSdOrderNo.Name = "colSdOrderNo";
            this.colSdOrderNo.Visible = true;
            this.colSdOrderNo.VisibleIndex = 1;
            // 
            // colSdOrderSeq
            // 
            this.colSdOrderSeq.AppearanceCell.Options.UseTextOptions = true;
            this.colSdOrderSeq.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSdOrderSeq.Caption = "수주순번";
            this.colSdOrderSeq.FieldName = "SdOrderSeq";
            this.colSdOrderSeq.Name = "colSdOrderSeq";
            this.colSdOrderSeq.Visible = true;
            this.colSdOrderSeq.VisibleIndex = 2;
            // 
            // colItemCode
            // 
            this.colItemCode.Caption = "품번";
            this.colItemCode.FieldName = "ItemCode";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.Visible = true;
            this.colItemCode.VisibleIndex = 3;
            // 
            // colWorkDate
            // 
            this.colWorkDate.AppearanceCell.Options.UseTextOptions = true;
            this.colWorkDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWorkDate.Caption = "지시일자";
            this.colWorkDate.FieldName = "WorkDate";
            this.colWorkDate.Name = "colWorkDate";
            this.colWorkDate.Visible = true;
            this.colWorkDate.VisibleIndex = 4;
            // 
            // colWorkSeq
            // 
            this.colWorkSeq.Caption = "지시순번";
            this.colWorkSeq.FieldName = "WorkSeq";
            this.colWorkSeq.Name = "colWorkSeq";
            this.colWorkSeq.Visible = true;
            this.colWorkSeq.VisibleIndex = 5;
            // 
            // colOrderQty
            // 
            this.colOrderQty.AppearanceCell.Options.UseTextOptions = true;
            this.colOrderQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colOrderQty.Caption = "지시수량";
            this.colOrderQty.FieldName = "OrderQty";
            this.colOrderQty.Name = "colOrderQty";
            this.colOrderQty.Visible = true;
            this.colOrderQty.VisibleIndex = 6;
            // 
            // colWorkQty
            // 
            this.colWorkQty.AppearanceCell.Options.UseTextOptions = true;
            this.colWorkQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colWorkQty.Caption = "생산수량";
            this.colWorkQty.FieldName = "WorkQty";
            this.colWorkQty.Name = "colWorkQty";
            // 
            // colGoodQty
            // 
            this.colGoodQty.AppearanceCell.Options.UseTextOptions = true;
            this.colGoodQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colGoodQty.Caption = "양품수량";
            this.colGoodQty.FieldName = "GoodQty";
            this.colGoodQty.Name = "colGoodQty";
            // 
            // colNgQty
            // 
            this.colNgQty.AppearanceCell.Options.UseTextOptions = true;
            this.colNgQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNgQty.Caption = "불량수량";
            this.colNgQty.FieldName = "NgQty";
            this.colNgQty.Name = "colNgQty";
            // 
            // colStatus
            // 
            this.colStatus.AppearanceCell.Options.UseTextOptions = true;
            this.colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatus.Caption = "상태";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 7;
            // 
            // colEndDate
            // 
            this.colEndDate.Caption = "종료일자";
            this.colEndDate.FieldName = "EndDate";
            this.colEndDate.Name = "colEndDate";
            // 
            // colRevision
            // 
            this.colRevision.FieldName = "Revision";
            this.colRevision.Name = "colRevision";
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Material);
            this.smtCommonBar1.Controls.Add(this.Lup_Process);
            this.smtCommonBar1.Controls.Add(this.cmdPrint);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1180, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "생산실적";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 8;
            // 
            // cmdPrint
            // 
            this.cmdPrint.AllowFocus = false;
            this.cmdPrint.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdPrint.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdPrint.Appearance.Options.UseBackColor = true;
            this.cmdPrint.Appearance.Options.UseForeColor = true;
            this.cmdPrint.AutoSize = true;
            this.cmdPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdPrint.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdPrint.ImageOptions.ImageIndex = 10;
            this.cmdPrint.ImageOptions.ImageToTextIndent = 15;
            this.cmdPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdPrint.Location = new System.Drawing.Point(1014, 0);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdPrint.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdPrint.Size = new System.Drawing.Size(89, 40);
            this.cmdPrint.smtButtonAutoText = false;
            this.cmdPrint.smtButtonStyle = baseSMT.EnumButtonStyles.프린터;
            this.cmdPrint.TabIndex = 33;
            this.cmdPrint.Text = "레시피";
            this.cmdPrint.Visible = false;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.AllowFocus = false;
            this.btn_Search.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Search.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Search.Appearance.Options.UseBackColor = true;
            this.btn_Search.Appearance.Options.UseForeColor = true;
            this.btn_Search.AutoSize = true;
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Search.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Search.ImageOptions.ImageIndex = 0;
            this.btn_Search.ImageOptions.ImageToTextIndent = 15;
            this.btn_Search.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Search.Location = new System.Drawing.Point(917, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 32;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // Lup_Material
            // 
            this.Lup_Material.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Material.Location = new System.Drawing.Point(677, 0);
            this.Lup_Material.LookAndFeel.SkinName = "WXI";
            this.Lup_Material.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Material.Name = "Lup_Material";
            this.Lup_Material.Size = new System.Drawing.Size(240, 40);
            this.Lup_Material.smtLabelTitleValue = "품목";
            this.Lup_Material.smtLabelTitleVisible = true;
            this.Lup_Material.smtLabelTitleWidth = 90;
            this.Lup_Material.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Material.smtLookUpDataDisplayMember = "품명";
            this.Lup_Material.smtLookUpDataSource = null;
            this.Lup_Material.smtLookUpDataValueMember = "품목코드";
            this.Lup_Material.smtLookUpSelectdIndex = -1;
            this.Lup_Material.smtLookUpText = "";
            this.Lup_Material.smtLookUpValue = "";
            this.Lup_Material.TabIndex = 31;
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
            this.Dt_Date.smtLabelTitleValue = "조회기간";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 30;
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
            this.cmdClose.Location = new System.Drawing.Point(1103, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Lup_Process
            // 
            this.Lup_Process.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Process.Location = new System.Drawing.Point(437, 0);
            this.Lup_Process.LookAndFeel.SkinName = "WXI";
            this.Lup_Process.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Process.Name = "Lup_Process";
            this.Lup_Process.Size = new System.Drawing.Size(240, 40);
            this.Lup_Process.smtLabelTitleValue = "공정";
            this.Lup_Process.smtLabelTitleVisible = true;
            this.Lup_Process.smtLabelTitleWidth = 90;
            this.Lup_Process.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Process.smtLookUpDataDisplayMember = "공정명";
            this.Lup_Process.smtLookUpDataSource = null;
            this.Lup_Process.smtLookUpDataValueMember = "공정코드";
            this.Lup_Process.smtLookUpSelectdIndex = -1;
            this.Lup_Process.smtLookUpText = "";
            this.Lup_Process.smtLookUpValue = "";
            this.Lup_Process.TabIndex = 34;
            // 
            // frmOrderAndResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 590);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_PM_OrderAndResult";
            this.Text = "생산실적";
            this.Load += new System.EventHandler(this.Frm_PM_OrderAndResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcProcessResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvProcessResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcPM_ProductPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvPM_ProductPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private System.Windows.Forms.Button button4;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents1;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents3;
        private DevExpress.Utils.DragDrop.DragDropEvents dragDropEvents4;
        private DevExpress.XtraGrid.GridControl GcPM_ProductPlan;
        private DevExpress.XtraGrid.Views.Grid.GridView GvPM_ProductPlan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraGrid.GridControl GcProcessResult;
        private DevExpress.XtraGrid.Views.Grid.GridView GvProcessResult;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colCorpId;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderNo;
        private DevExpress.XtraGrid.Columns.GridColumn colSdOrderNo;
        private DevExpress.XtraGrid.Columns.GridColumn colSdOrderSeq;
        private DevExpress.XtraGrid.Columns.GridColumn colItemCode;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkDate;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkSeq;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderQty;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkQty;
        private DevExpress.XtraGrid.Columns.GridColumn colGoodQty;
        private DevExpress.XtraGrid.Columns.GridColumn colNgQty;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRevision;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private System.Windows.Forms.Panel panel4;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton btn_Search;
		private baseSMT.smtSearchLookUp Lup_Material;
		private baseSMT.smtSearchDatetime Dt_Date;
		private baseSMT.smtSearchButton cmdClose;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdInsert;
		private baseSMT.smtSearchButton cmdDelete;
        private baseSMT.smtSearchButton cmdPrint;
        private baseSMT.smtSearchLookUp Lup_Process;
    }
}