
namespace SmartMES.View.QM
{
	partial class Frm_QM_ItemExamMgnt_ItemCopy
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_ItemExamMgnt_ItemCopy));
			this.Gc_ItemExam = new DevExpress.XtraGrid.GridControl();
			this.Gv_ItemExam = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.Btn_Down = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Up = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton13 = new DevExpress.XtraEditors.SimpleButton();
			this.Gc_Item = new DevExpress.XtraGrid.GridControl();
			this.Gv_Item = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdIns = new baseSMT.smtSearchButton();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdDel = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
			this.BtnAllCheck = new DevExpress.XtraEditors.SimpleButton();
			this.LupItemClass = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.Lup_Item_Div = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
			((System.ComponentModel.ISupportInitialize)(this.Gc_ItemExam)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_ItemExam)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Item)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Item)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
			this.panelControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LupItemClass.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Lup_Item_Div.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
			this.SuspendLayout();
			// 
			// Gc_ItemExam
			// 
			this.Gc_ItemExam.Location = new System.Drawing.Point(7, 80);
			this.Gc_ItemExam.MainView = this.Gv_ItemExam;
			this.Gc_ItemExam.Name = "Gc_ItemExam";
			this.Gc_ItemExam.Size = new System.Drawing.Size(1208, 215);
			this.Gc_ItemExam.TabIndex = 1;
			this.Gc_ItemExam.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_ItemExam,
            this.gridView1});
			// 
			// Gv_ItemExam
			// 
			this.Gv_ItemExam.DetailHeight = 425;
			this.Gv_ItemExam.GridControl = this.Gc_ItemExam;
			this.Gv_ItemExam.Name = "Gv_ItemExam";
			this.Gv_ItemExam.OptionsEditForm.PopupEditFormWidth = 914;
			this.Gv_ItemExam.OptionsView.ShowGroupPanel = false;
			// 
			// gridView1
			// 
			this.gridView1.DetailHeight = 425;
			this.gridView1.GridControl = this.Gc_ItemExam;
			this.gridView1.Name = "gridView1";
			this.gridView1.OptionsEditForm.PopupEditFormWidth = 914;
			// 
			// panelControl1
			// 
			this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl1.Controls.Add(this.Btn_Down);
			this.panelControl1.Controls.Add(this.Btn_Up);
			this.panelControl1.Controls.Add(this.simpleButton13);
			this.panelControl1.Location = new System.Drawing.Point(7, 34);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
			this.panelControl1.Size = new System.Drawing.Size(1208, 40);
			this.panelControl1.TabIndex = 281;
			// 
			// Btn_Down
			// 
			this.Btn_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Down.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Down.ImageOptions.Image")));
			this.Btn_Down.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
			this.Btn_Down.Location = new System.Drawing.Point(1147, 9);
			this.Btn_Down.Name = "Btn_Down";
			this.Btn_Down.Size = new System.Drawing.Size(52, 26);
			this.Btn_Down.TabIndex = 286;
			this.Btn_Down.Click += new System.EventHandler(this.Btn_RowMove);
			// 
			// Btn_Up
			// 
			this.Btn_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Up.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Up.ImageOptions.Image")));
			this.Btn_Up.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
			this.Btn_Up.Location = new System.Drawing.Point(1089, 10);
			this.Btn_Up.Name = "Btn_Up";
			this.Btn_Up.Size = new System.Drawing.Size(52, 26);
			this.Btn_Up.TabIndex = 285;
			this.Btn_Up.Click += new System.EventHandler(this.Btn_RowMove);
			// 
			// simpleButton13
			// 
			this.simpleButton13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButton13.Appearance.Options.UseTextOptions = true;
			this.simpleButton13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.simpleButton13.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton13.ImageOptions.Image")));
			this.simpleButton13.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.simpleButton13.Location = new System.Drawing.Point(2372, 7);
			this.simpleButton13.Name = "simpleButton13";
			this.simpleButton13.Size = new System.Drawing.Size(114, 32);
			this.simpleButton13.TabIndex = 59;
			this.simpleButton13.Text = "바코드 출력";
			// 
			// Gc_Item
			// 
			this.Gc_Item.Location = new System.Drawing.Point(7, 399);
			this.Gc_Item.MainView = this.Gv_Item;
			this.Gc_Item.Name = "Gc_Item";
			this.Gc_Item.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
			this.Gc_Item.Size = new System.Drawing.Size(1208, 251);
			this.Gc_Item.TabIndex = 280;
			this.Gc_Item.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Item,
            this.gridView2});
			// 
			// Gv_Item
			// 
			this.Gv_Item.DetailHeight = 425;
			this.Gv_Item.GridControl = this.Gc_Item;
			this.Gv_Item.Name = "Gv_Item";
			this.Gv_Item.OptionsEditForm.PopupEditFormWidth = 914;
			this.Gv_Item.OptionsView.ShowGroupPanel = false;
			// 
			// repositoryItemComboBox1
			// 
			this.repositoryItemComboBox1.AutoHeight = false;
			this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
			// 
			// gridView2
			// 
			this.gridView2.DetailHeight = 425;
			this.gridView2.GridControl = this.Gc_Item;
			this.gridView2.Name = "gridView2";
			this.gridView2.OptionsEditForm.PopupEditFormWidth = 914;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdIns);
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdDel);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1222, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "검사항목 품목복사";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 285;
			// 
			// cmdIns
			// 
			this.cmdIns.AllowFocus = false;
			this.cmdIns.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdIns.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdIns.Appearance.Options.UseBackColor = true;
			this.cmdIns.Appearance.Options.UseForeColor = true;
			this.cmdIns.AutoSize = true;
			this.cmdIns.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdIns.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdIns.ImageOptions.ImageIndex = 1;
			this.cmdIns.ImageOptions.ImageToTextIndent = 15;
			this.cmdIns.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdIns.Location = new System.Drawing.Point(914, 0);
			this.cmdIns.Name = "cmdIns";
			this.cmdIns.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdIns.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdIns.Size = new System.Drawing.Size(77, 40);
			this.cmdIns.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdIns.TabIndex = 7;
			this.cmdIns.Tag = "Ins";
			this.cmdIns.Text = "등록";
			this.cmdIns.Click += new System.EventHandler(this.Btn_Row_Edit_Click);
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
			this.cmdDisplay.Location = new System.Drawing.Point(143, 0);
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
			this.cmdSave.Location = new System.Drawing.Point(991, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// cmdDel
			// 
			this.cmdDel.AllowFocus = false;
			this.cmdDel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdDel.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdDel.Appearance.Options.UseBackColor = true;
			this.cmdDel.Appearance.Options.UseForeColor = true;
			this.cmdDel.AutoSize = true;
			this.cmdDel.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdDel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdDel.ImageOptions.ImageIndex = 3;
			this.cmdDel.ImageOptions.ImageToTextIndent = 15;
			this.cmdDel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdDel.Location = new System.Drawing.Point(1068, 0);
			this.cmdDel.Name = "cmdDel";
			this.cmdDel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDel.Size = new System.Drawing.Size(77, 40);
			this.cmdDel.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDel.TabIndex = 2;
			this.cmdDel.Tag = "Del";
			this.cmdDel.Text = "삭제";
			this.cmdDel.Click += new System.EventHandler(this.Btn_Row_Edit_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(1145, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.panelControl2);
			this.layoutControl1.Controls.Add(this.panelControl1);
			this.layoutControl1.Controls.Add(this.Gc_Item);
			this.layoutControl1.Controls.Add(this.Gc_ItemExam);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1062, 448, 650, 400);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1222, 657);
			this.layoutControl1.TabIndex = 286;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// panelControl2
			// 
			this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl2.Controls.Add(this.BtnAllCheck);
			this.panelControl2.Controls.Add(this.LupItemClass);
			this.panelControl2.Controls.Add(this.labelControl1);
			this.panelControl2.Controls.Add(this.Lup_Item_Div);
			this.panelControl2.Controls.Add(this.labelControl2);
			this.panelControl2.Controls.Add(this.simpleButton3);
			this.panelControl2.Location = new System.Drawing.Point(7, 352);
			this.panelControl2.Name = "panelControl2";
			this.panelControl2.Padding = new System.Windows.Forms.Padding(3);
			this.panelControl2.Size = new System.Drawing.Size(1208, 41);
			this.panelControl2.TabIndex = 282;
			// 
			// BtnAllCheck
			// 
			this.BtnAllCheck.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnAllCheck.ImageOptions.Image")));
			this.BtnAllCheck.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
			this.BtnAllCheck.Location = new System.Drawing.Point(578, 7);
			this.BtnAllCheck.Name = "BtnAllCheck";
			this.BtnAllCheck.Size = new System.Drawing.Size(80, 26);
			this.BtnAllCheck.TabIndex = 287;
			this.BtnAllCheck.Text = "전체선택";
			this.BtnAllCheck.Click += new System.EventHandler(this.BtnAllCheck_Click);
			// 
			// LupItemClass
			// 
			this.LupItemClass.Location = new System.Drawing.Point(353, 6);
			this.LupItemClass.Name = "LupItemClass";
			this.LupItemClass.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.LupItemClass.Properties.Appearance.Options.UseBackColor = true;
			this.LupItemClass.Properties.AutoHeight = false;
			this.LupItemClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.LupItemClass.Properties.NullText = "";
			this.LupItemClass.Properties.PopupView = this.gridView3;
			this.LupItemClass.Size = new System.Drawing.Size(217, 26);
			this.LupItemClass.TabIndex = 286;
			this.LupItemClass.EditValueChanged += new System.EventHandler(this.LupItemClass_EditValueChanged);
			// 
			// gridView3
			// 
			this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridView3.Name = "gridView3";
			this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridView3.OptionsView.ShowGroupPanel = false;
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl1.Location = new System.Drawing.Point(293, 6);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(114, 26);
			this.labelControl1.TabIndex = 285;
			this.labelControl1.Text = "품목분류1";
			// 
			// Lup_Item_Div
			// 
			this.Lup_Item_Div.Location = new System.Drawing.Point(66, 6);
			this.Lup_Item_Div.Name = "Lup_Item_Div";
			this.Lup_Item_Div.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Lup_Item_Div.Properties.Appearance.Options.UseBackColor = true;
			this.Lup_Item_Div.Properties.AutoHeight = false;
			this.Lup_Item_Div.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.Lup_Item_Div.Properties.NullText = "";
			this.Lup_Item_Div.Properties.PopupView = this.searchLookUpEdit1View;
			this.Lup_Item_Div.Size = new System.Drawing.Size(217, 26);
			this.Lup_Item_Div.TabIndex = 284;
			this.Lup_Item_Div.EditValueChanged += new System.EventHandler(this.Rdo_Item_Div_EditValueChanged);
			// 
			// searchLookUpEdit1View
			// 
			this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
			this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl2.Location = new System.Drawing.Point(6, 6);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(114, 26);
			this.labelControl2.TabIndex = 283;
			this.labelControl2.Text = "품목구분";
			// 
			// simpleButton3
			// 
			this.simpleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButton3.Appearance.Options.UseTextOptions = true;
			this.simpleButton3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.simpleButton3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.ImageOptions.Image")));
			this.simpleButton3.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.simpleButton3.Location = new System.Drawing.Point(2372, 7);
			this.simpleButton3.Name = "simpleButton3";
			this.simpleButton3.Size = new System.Drawing.Size(114, 32);
			this.simpleButton3.TabIndex = 59;
			this.simpleButton3.Text = "바코드 출력";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.splitterItem1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1222, 657);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1222, 302);
			this.layoutControlGroup1.Text = "검사항목정보";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.Gc_ItemExam;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 46);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1214, 221);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.panelControl1;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 46);
			this.layoutControlItem3.MinSize = new System.Drawing.Size(7, 46);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(1214, 46);
			this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem4});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 318);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(1222, 339);
			this.layoutControlGroup2.Text = "품목";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.Gc_Item;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 47);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1214, 257);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.panelControl2;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(1214, 47);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// splitterItem1
			// 
			this.splitterItem1.AllowHotTrack = true;
			this.splitterItem1.Location = new System.Drawing.Point(0, 302);
			this.splitterItem1.Name = "splitterItem1";
			this.splitterItem1.Size = new System.Drawing.Size(1222, 16);
			// 
			// Frm_QM_ItemExamMgnt_ItemCopy
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1222, 697);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_QM_ItemExamMgnt_ItemCopy";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "검사항목 품목복사";
			this.Load += new System.EventHandler(this.Frm_QM_ItemExamMgnt_ItemCopy_Load);
			((System.ComponentModel.ISupportInitialize)(this.Gc_ItemExam)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_ItemExam)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Item)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Item)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
			this.panelControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LupItemClass.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Lup_Item_Div.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraGrid.GridControl Gc_Item;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Item;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
		private DevExpress.XtraGrid.GridControl Gc_ItemExam;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_ItemExam;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.SimpleButton simpleButton13;
		private DevExpress.XtraEditors.SimpleButton Btn_Down;
		private DevExpress.XtraEditors.SimpleButton Btn_Up;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdIns;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDel;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.SplitterItem splitterItem1;
		private DevExpress.XtraEditors.PanelControl panelControl2;
		private DevExpress.XtraEditors.SimpleButton simpleButton3;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
		private DevExpress.XtraEditors.SearchLookUpEdit LupItemClass;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.SearchLookUpEdit Lup_Item_Div;
		private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.SimpleButton BtnAllCheck;
	}
}