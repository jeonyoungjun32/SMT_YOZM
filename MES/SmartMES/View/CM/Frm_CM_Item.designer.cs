
namespace SmartMES.View.CM

{
    partial class Frm_CM_Item
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_Item));
            this.Gc_Item = new DevExpress.XtraGrid.GridControl();
            this.Gv_Product = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Asy = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Goods = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Material = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Subsidiary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Consume = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_Jig = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.LupCustomer = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LblSearchCustomer = new DevExpress.XtraEditors.LabelControl();
            this.Lup_ItemClass = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Rdo_Item_Div = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdCopy = new baseSMT.smtSearchButton();
            this.cmdInsert = new baseSMT.smtSearchButton();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDelete = new baseSMT.smtSearchButton();
            this.cmdExcelUp = new baseSMT.smtSearchButton();
            this.cmdExcelDown = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Pnl_History = new DevExpress.XtraEditors.GroupControl();
            this.Gc_History = new DevExpress.XtraGrid.GridControl();
            this.Gv_History = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.Btn_History_Save = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_HistoryRow_Del = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_History_Close = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_HistoryRow_Ins = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Product)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Asy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Goods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Material)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Subsidiary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Consume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Jig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LupCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lup_ItemClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rdo_Item_Div.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_History)).BeginInit();
            this.Pnl_History.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_History)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_History)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // Gc_Item
            // 
            this.Gc_Item.Location = new System.Drawing.Point(7, 119);
            this.Gc_Item.MainView = this.Gv_Product;
            this.Gc_Item.Name = "Gc_Item";
            this.Gc_Item.Size = new System.Drawing.Size(1337, 281);
            this.Gc_Item.TabIndex = 1;
            this.Gc_Item.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Product,
            this.Gv_Asy,
            this.Gv_Goods,
            this.Gv_Material,
            this.Gv_Subsidiary,
            this.Gv_Consume,
            this.Gv_Jig});
            // 
            // Gv_Product
            // 
            this.Gv_Product.DetailHeight = 397;
            this.Gv_Product.GridControl = this.Gc_Item;
            this.Gv_Product.Name = "Gv_Product";
            this.Gv_Product.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_Product.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Asy
            // 
            this.Gv_Asy.DetailHeight = 397;
            this.Gv_Asy.GridControl = this.Gc_Item;
            this.Gv_Asy.Name = "Gv_Asy";
            this.Gv_Asy.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_Asy.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Goods
            // 
            this.Gv_Goods.DetailHeight = 397;
            this.Gv_Goods.GridControl = this.Gc_Item;
            this.Gv_Goods.Name = "Gv_Goods";
            this.Gv_Goods.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_Goods.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Material
            // 
            this.Gv_Material.DetailHeight = 397;
            this.Gv_Material.GridControl = this.Gc_Item;
            this.Gv_Material.Name = "Gv_Material";
            this.Gv_Material.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_Material.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Subsidiary
            // 
            this.Gv_Subsidiary.DetailHeight = 397;
            this.Gv_Subsidiary.GridControl = this.Gc_Item;
            this.Gv_Subsidiary.Name = "Gv_Subsidiary";
            this.Gv_Subsidiary.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_Subsidiary.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Consume
            // 
            this.Gv_Consume.DetailHeight = 397;
            this.Gv_Consume.GridControl = this.Gc_Item;
            this.Gv_Consume.Name = "Gv_Consume";
            this.Gv_Consume.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_Consume.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_Jig
            // 
            this.Gv_Jig.DetailHeight = 397;
            this.Gv_Jig.GridControl = this.Gc_Item;
            this.Gv_Jig.Name = "Gv_Jig";
            this.Gv_Jig.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_Jig.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.label2);
            this.panelControl3.Controls.Add(this.LupCustomer);
            this.panelControl3.Controls.Add(this.LblSearchCustomer);
            this.panelControl3.Controls.Add(this.Lup_ItemClass);
            this.panelControl3.Controls.Add(this.Rdo_Item_Div);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Location = new System.Drawing.Point(3, 3);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl3.Size = new System.Drawing.Size(1345, 79);
            this.panelControl3.TabIndex = 279;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(645, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 314;
            this.label2.Text = "<- radio있음";
            this.label2.Visible = false;
            // 
            // LupCustomer
            // 
            this.LupCustomer.EditValue = "";
            this.LupCustomer.Location = new System.Drawing.Point(297, 40);
            this.LupCustomer.Name = "LupCustomer";
            this.LupCustomer.Properties.AutoHeight = false;
            this.LupCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupCustomer.Properties.NullText = "";
            this.LupCustomer.Properties.PopupView = this.gridView1;
            this.LupCustomer.Size = new System.Drawing.Size(138, 26);
            this.LupCustomer.TabIndex = 287;
            this.LupCustomer.EditValueChanged += new System.EventHandler(this.LupCustomer_EditValueChanged);
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
            this.LblSearchCustomer.Location = new System.Drawing.Point(234, 40);
            this.LblSearchCustomer.Name = "LblSearchCustomer";
            this.LblSearchCustomer.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.LblSearchCustomer.Size = new System.Drawing.Size(83, 26);
            this.LblSearchCustomer.TabIndex = 286;
            this.LblSearchCustomer.Text = "거래처";
            // 
            // Lup_ItemClass
            // 
            this.Lup_ItemClass.Location = new System.Drawing.Point(78, 40);
            this.Lup_ItemClass.Name = "Lup_ItemClass";
            this.Lup_ItemClass.Properties.AutoHeight = false;
            this.Lup_ItemClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Lup_ItemClass.Properties.PopupView = this.searchLookUpEdit1View;
            this.Lup_ItemClass.Size = new System.Drawing.Size(138, 26);
            this.Lup_ItemClass.TabIndex = 280;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 397;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsEditForm.PopupEditFormWidth = 914;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // Rdo_Item_Div
            // 
            this.Rdo_Item_Div.Location = new System.Drawing.Point(78, 7);
            this.Rdo_Item_Div.Name = "Rdo_Item_Div";
            this.Rdo_Item_Div.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Rdo_Item_Div.Properties.Appearance.Options.UseBackColor = true;
            this.Rdo_Item_Div.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Rdo_Item_Div.Size = new System.Drawing.Size(587, 26);
            this.Rdo_Item_Div.TabIndex = 280;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(7, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(83, 26);
            this.labelControl2.TabIndex = 281;
            this.labelControl2.Text = "분류";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(7, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 26);
            this.labelControl1.TabIndex = 60;
            this.labelControl1.Text = "품목구분";
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdCopy);
            this.smtCommonBar1.Controls.Add(this.cmdInsert);
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.cmdDelete);
            this.smtCommonBar1.Controls.Add(this.cmdExcelUp);
            this.smtCommonBar1.Controls.Add(this.cmdExcelDown);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1351, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "BS_Item";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 282;
            // 
            // cmdCopy
            // 
            this.cmdCopy.AllowFocus = false;
            this.cmdCopy.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdCopy.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdCopy.Appearance.Options.UseBackColor = true;
            this.cmdCopy.Appearance.Options.UseForeColor = true;
            this.cmdCopy.AutoSize = true;
            this.cmdCopy.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdCopy.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdCopy.ImageOptions.ImageIndex = 1;
            this.cmdCopy.ImageOptions.ImageToTextIndent = 15;
            this.cmdCopy.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdCopy.Location = new System.Drawing.Point(737, 0);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdCopy.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdCopy.Size = new System.Drawing.Size(92, 40);
            this.cmdCopy.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdCopy.TabIndex = 30;
            this.cmdCopy.Tag = "Ins";
            this.cmdCopy.Text = "행 복사";
            this.cmdCopy.Click += new System.EventHandler(this.cmd_Click);
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
            this.cmdInsert.Location = new System.Drawing.Point(829, 0);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdInsert.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdInsert.Size = new System.Drawing.Size(77, 40);
            this.cmdInsert.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdInsert.TabIndex = 7;
            this.cmdInsert.Tag = "Ins";
            this.cmdInsert.Text = "등록";
            this.cmdInsert.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdDisplay.Location = new System.Drawing.Point(97, 0);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 5;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Visible = false;
            this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
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
            this.cmdSave.Location = new System.Drawing.Point(906, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "저장";
            this.cmdSave.Click += new System.EventHandler(this.cmd_Click);
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
            this.cmdDelete.Location = new System.Drawing.Point(983, 0);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDelete.Size = new System.Drawing.Size(77, 40);
            this.cmdDelete.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Tag = "Del";
            this.cmdDelete.Text = "삭제";
            this.cmdDelete.Click += new System.EventHandler(this.Btn_RowEdit);
            // 
            // cmdExcelUp
            // 
            this.cmdExcelUp.AllowFocus = false;
            this.cmdExcelUp.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdExcelUp.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdExcelUp.Appearance.Options.UseBackColor = true;
            this.cmdExcelUp.Appearance.Options.UseForeColor = true;
            this.cmdExcelUp.AutoSize = true;
            this.cmdExcelUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdExcelUp.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdExcelUp.ImageOptions.ImageIndex = 8;
            this.cmdExcelUp.ImageOptions.ImageToTextIndent = 15;
            this.cmdExcelUp.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdExcelUp.Location = new System.Drawing.Point(1060, 0);
            this.cmdExcelUp.Name = "cmdExcelUp";
            this.cmdExcelUp.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcelUp.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcelUp.Size = new System.Drawing.Size(113, 40);
            this.cmdExcelUp.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀업로드;
            this.cmdExcelUp.TabIndex = 6;
            this.cmdExcelUp.Text = "엑셀업로드";
            this.cmdExcelUp.Click += new System.EventHandler(this.cmd_Click);
            // 
            // cmdExcelDown
            // 
            this.cmdExcelDown.AllowFocus = false;
            this.cmdExcelDown.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdExcelDown.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdExcelDown.Appearance.Options.UseBackColor = true;
            this.cmdExcelDown.Appearance.Options.UseForeColor = true;
            this.cmdExcelDown.AutoSize = true;
            this.cmdExcelDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdExcelDown.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdExcelDown.ImageOptions.ImageIndex = 9;
            this.cmdExcelDown.ImageOptions.ImageToTextIndent = 15;
            this.cmdExcelDown.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdExcelDown.Location = new System.Drawing.Point(1173, 0);
            this.cmdExcelDown.Name = "cmdExcelDown";
            this.cmdExcelDown.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcelDown.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcelDown.Size = new System.Drawing.Size(101, 40);
            this.cmdExcelDown.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcelDown.TabIndex = 29;
            this.cmdExcelDown.Text = "엑셀변환";
            this.cmdExcelDown.Click += new System.EventHandler(this.cmd_Click);
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
            this.cmdClose.Location = new System.Drawing.Point(1274, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmd_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl3);
            this.layoutControl1.Controls.Add(this.Gc_Item);
            this.layoutControl1.Controls.Add(this.Pnl_History);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1351, 595);
            this.layoutControl1.TabIndex = 283;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Pnl_History
            // 
            this.Pnl_History.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.Pnl_History.AppearanceCaption.Options.UseFont = true;
            this.Pnl_History.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Pnl_History.CaptionImageOptions.Image")));
            this.Pnl_History.Controls.Add(this.Gc_History);
            this.Pnl_History.Controls.Add(this.panelControl2);
            this.Pnl_History.Location = new System.Drawing.Point(4, 411);
            this.Pnl_History.Name = "Pnl_History";
            this.Pnl_History.ShowCaption = false;
            this.Pnl_History.Size = new System.Drawing.Size(1343, 180);
            this.Pnl_History.TabIndex = 284;
            this.Pnl_History.Text = "품목별 단가내역";
            // 
            // Gc_History
            // 
            this.Gc_History.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_History.Location = new System.Drawing.Point(2, 37);
            this.Gc_History.MainView = this.Gv_History;
            this.Gc_History.Name = "Gc_History";
            this.Gc_History.Size = new System.Drawing.Size(1339, 141);
            this.Gc_History.TabIndex = 1;
            this.Gc_History.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_History});
            // 
            // Gv_History
            // 
            this.Gv_History.GridControl = this.Gc_History;
            this.Gv_History.Name = "Gv_History";
            this.Gv_History.OptionsView.ShowGroupPanel = false;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.Btn_History_Save);
            this.panelControl2.Controls.Add(this.Btn_HistoryRow_Del);
            this.panelControl2.Controls.Add(this.Btn_History_Close);
            this.panelControl2.Controls.Add(this.Btn_HistoryRow_Ins);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl2.Size = new System.Drawing.Size(1339, 35);
            this.panelControl2.TabIndex = 282;
            // 
            // Btn_History_Save
            // 
            this.Btn_History_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_History_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_History_Save.ImageOptions.Image")));
            this.Btn_History_Save.Location = new System.Drawing.Point(1113, 6);
            this.Btn_History_Save.Name = "Btn_History_Save";
            this.Btn_History_Save.Size = new System.Drawing.Size(70, 23);
            this.Btn_History_Save.TabIndex = 293;
            this.Btn_History_Save.Text = "저장";
            this.Btn_History_Save.Click += new System.EventHandler(this.Btn_History_Save_Click);
            // 
            // Btn_HistoryRow_Del
            // 
            this.Btn_HistoryRow_Del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_HistoryRow_Del.ImageOptions.Image = global::SmartMES.Properties.Resources.cut_16x16;
            this.Btn_HistoryRow_Del.Location = new System.Drawing.Point(1188, 6);
            this.Btn_HistoryRow_Del.Name = "Btn_HistoryRow_Del";
            this.Btn_HistoryRow_Del.Size = new System.Drawing.Size(70, 23);
            this.Btn_HistoryRow_Del.TabIndex = 292;
            this.Btn_HistoryRow_Del.Text = "삭제";
            this.Btn_HistoryRow_Del.Click += new System.EventHandler(this.Btn_RowEdit_History);
            // 
            // Btn_History_Close
            // 
            this.Btn_History_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_History_Close.ImageOptions.Image = global::SmartMES.Properties.Resources.cancel_16x16;
            this.Btn_History_Close.Location = new System.Drawing.Point(1265, 6);
            this.Btn_History_Close.Name = "Btn_History_Close";
            this.Btn_History_Close.Size = new System.Drawing.Size(70, 23);
            this.Btn_History_Close.TabIndex = 290;
            this.Btn_History_Close.Text = "닫기";
            this.Btn_History_Close.Click += new System.EventHandler(this.Btn_History_Close_Click);
            // 
            // Btn_HistoryRow_Ins
            // 
            this.Btn_HistoryRow_Ins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_HistoryRow_Ins.ImageOptions.Image = global::SmartMES.Properties.Resources.add_16x16;
            this.Btn_HistoryRow_Ins.Location = new System.Drawing.Point(1037, 6);
            this.Btn_HistoryRow_Ins.Name = "Btn_HistoryRow_Ins";
            this.Btn_HistoryRow_Ins.Size = new System.Drawing.Size(70, 23);
            this.Btn_HistoryRow_Ins.TabIndex = 290;
            this.Btn_HistoryRow_Ins.Tag = "Ins";
            this.Btn_HistoryRow_Ins.Text = "등록";
            this.Btn_HistoryRow_Ins.Click += new System.EventHandler(this.Btn_RowEdit_History);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlGroup1,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1351, 595);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl3;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 85);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(7, 85);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1351, 85);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 85);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1351, 322);
            this.layoutControlGroup1.Text = "리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Gc_Item;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1343, 287);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.Pnl_History;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 407);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem3.Size = new System.Drawing.Size(1351, 188);
            this.layoutControlItem3.Text = "layoutControlItem2";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Frm_CM_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 635);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_CM_Item";
            this.Text = "BS_Item";
            this.Load += new System.EventHandler(this.Frm_CM_Item_Type_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Product)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Asy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Goods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Material)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Subsidiary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Consume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Jig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LupCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lup_ItemClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rdo_Item_Div.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_History)).EndInit();
            this.Pnl_History.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_History)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_History)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl Gc_Item;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Product;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.RadioGroup Rdo_Item_Div;
        private DevExpress.XtraEditors.SearchLookUpEdit Lup_ItemClass;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Asy;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Goods;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Material;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Subsidiary;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Consume;
        private DevExpress.XtraEditors.SearchLookUpEdit LupCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl LblSearchCustomer;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Jig;
        private System.Windows.Forms.Label label2;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdCopy;
		private baseSMT.smtSearchButton cmdInsert;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDelete;
		private baseSMT.smtSearchButton cmdExcelUp;
		private baseSMT.smtSearchButton cmdExcelDown;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.GroupControl Pnl_History;
        private DevExpress.XtraGrid.GridControl Gc_History;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_History;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton Btn_History_Save;
        private DevExpress.XtraEditors.SimpleButton Btn_HistoryRow_Del;
        private DevExpress.XtraEditors.SimpleButton Btn_History_Close;
        private DevExpress.XtraEditors.SimpleButton Btn_HistoryRow_Ins;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}