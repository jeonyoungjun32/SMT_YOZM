
namespace SmartMES.View.QM
{
    partial class Frm_QM_HACCPStd
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_HACCPStd));
			this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
			this.BtnItemImg = new DevExpress.XtraEditors.SimpleButton();
			this.BtnExam_Q = new DevExpress.XtraEditors.SimpleButton();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.Gc_Std = new DevExpress.XtraGrid.GridControl();
			this.Gv_Std = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.LupItemClass1 = new DevExpress.XtraEditors.SearchLookUpEdit();
			this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.Rdo_Target_Div = new DevExpress.XtraEditors.RadioGroup();
			this.Rdo_CCP_Div = new DevExpress.XtraEditors.RadioGroup();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.BtnDelete_All = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Down = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Up = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Row_Del = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Row_Ins = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton13 = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.BtnCopyItem = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_All_Add = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Add = new DevExpress.XtraEditors.SimpleButton();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Std)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Std)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LupItemClass1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Rdo_Target_Div.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Rdo_CCP_Div.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtnSave
			// 
			this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.ImageOptions.Image")));
			this.BtnSave.Location = new System.Drawing.Point(927, 39);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new System.Drawing.Size(80, 26);
			this.BtnSave.TabIndex = 266;
			this.BtnSave.Text = "저장";
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// BtnItemImg
			// 
			this.BtnItemImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnItemImg.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnItemImg.ImageOptions.Image")));
			this.BtnItemImg.Location = new System.Drawing.Point(565, 7);
			this.BtnItemImg.Name = "BtnItemImg";
			this.BtnItemImg.Size = new System.Drawing.Size(128, 25);
			this.BtnItemImg.TabIndex = 287;
			this.BtnItemImg.Text = "검사기준서 등록";
			this.BtnItemImg.Visible = false;
			this.BtnItemImg.Click += new System.EventHandler(this.BtnItemImg_Click);
			// 
			// BtnExam_Q
			// 
			this.BtnExam_Q.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnExam_Q.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnExam_Q.ImageOptions.Image")));
			this.BtnExam_Q.Location = new System.Drawing.Point(699, 7);
			this.BtnExam_Q.Name = "BtnExam_Q";
			this.BtnExam_Q.Size = new System.Drawing.Size(141, 25);
			this.BtnExam_Q.TabIndex = 283;
			this.BtnExam_Q.Text = "정량검사 일괄수정";
			this.BtnExam_Q.Visible = false;
			this.BtnExam_Q.Click += new System.EventHandler(this.BtnExam_Q_Click);
			// 
			// groupControl1
			// 
			this.groupControl1.Controls.Add(this.Gc_Std);
			this.groupControl1.Controls.Add(this.panelControl1);
			this.groupControl1.Location = new System.Drawing.Point(3, 3);
			this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(1277, 563);
			this.groupControl1.TabIndex = 271;
			this.groupControl1.Text = "항목 정보";
			// 
			// Gc_Std
			// 
			this.Gc_Std.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_Std.Location = new System.Drawing.Point(2, 132);
			this.Gc_Std.MainView = this.Gv_Std;
			this.Gc_Std.Name = "Gc_Std";
			this.Gc_Std.Size = new System.Drawing.Size(1273, 429);
			this.Gc_Std.TabIndex = 1;
			this.Gc_Std.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Std,
            this.gridView1});
			// 
			// Gv_Std
			// 
			this.Gv_Std.DetailHeight = 425;
			this.Gv_Std.GridControl = this.Gc_Std;
			this.Gv_Std.Name = "Gv_Std";
			this.Gv_Std.OptionsView.ShowGroupPanel = false;
			// 
			// gridView1
			// 
			this.gridView1.DetailHeight = 425;
			this.gridView1.GridControl = this.Gc_Std;
			this.gridView1.Name = "gridView1";
			// 
			// panelControl1
			// 
			this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl1.Controls.Add(this.LupItemClass1);
			this.panelControl1.Controls.Add(this.labelControl2);
			this.panelControl1.Controls.Add(this.Rdo_Target_Div);
			this.panelControl1.Controls.Add(this.Rdo_CCP_Div);
			this.panelControl1.Controls.Add(this.labelControl3);
			this.panelControl1.Controls.Add(this.BtnDelete_All);
			this.panelControl1.Controls.Add(this.Btn_Down);
			this.panelControl1.Controls.Add(this.Btn_Up);
			this.panelControl1.Controls.Add(this.BtnSave);
			this.panelControl1.Controls.Add(this.Btn_Row_Del);
			this.panelControl1.Controls.Add(this.Btn_Row_Ins);
			this.panelControl1.Controls.Add(this.simpleButton13);
			this.panelControl1.Controls.Add(this.labelControl1);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl1.Location = new System.Drawing.Point(2, 29);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
			this.panelControl1.Size = new System.Drawing.Size(1273, 103);
			this.panelControl1.TabIndex = 280;
			// 
			// LupItemClass1
			// 
			this.LupItemClass1.Location = new System.Drawing.Point(94, 71);
			this.LupItemClass1.Name = "LupItemClass1";
			this.LupItemClass1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.LupItemClass1.Properties.Appearance.Options.UseBackColor = true;
			this.LupItemClass1.Properties.AutoHeight = false;
			this.LupItemClass1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.LupItemClass1.Properties.NullText = "";
			this.LupItemClass1.Properties.PopupView = this.searchLookUpEdit1View;
			this.LupItemClass1.Size = new System.Drawing.Size(190, 26);
			this.LupItemClass1.TabIndex = 289;
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
			this.labelControl2.Location = new System.Drawing.Point(11, 71);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(114, 26);
			this.labelControl2.TabIndex = 290;
			this.labelControl2.Text = "품목분류1";
			// 
			// Rdo_Target_Div
			// 
			this.Rdo_Target_Div.Location = new System.Drawing.Point(94, 39);
			this.Rdo_Target_Div.Name = "Rdo_Target_Div";
			this.Rdo_Target_Div.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Rdo_Target_Div.Properties.Appearance.Options.UseBackColor = true;
			this.Rdo_Target_Div.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.Rdo_Target_Div.Size = new System.Drawing.Size(606, 26);
			this.Rdo_Target_Div.TabIndex = 280;
			// 
			// Rdo_CCP_Div
			// 
			this.Rdo_CCP_Div.Location = new System.Drawing.Point(94, 7);
			this.Rdo_CCP_Div.Name = "Rdo_CCP_Div";
			this.Rdo_CCP_Div.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Rdo_CCP_Div.Properties.Appearance.Options.UseBackColor = true;
			this.Rdo_CCP_Div.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.Rdo_CCP_Div.Size = new System.Drawing.Size(606, 26);
			this.Rdo_CCP_Div.TabIndex = 288;
			// 
			// labelControl3
			// 
			this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl3.Location = new System.Drawing.Point(11, 39);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(114, 26);
			this.labelControl3.TabIndex = 287;
			this.labelControl3.Text = "관리대상구분";
			// 
			// BtnDelete_All
			// 
			this.BtnDelete_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnDelete_All.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelete_All.ImageOptions.Image")));
			this.BtnDelete_All.Location = new System.Drawing.Point(1186, 39);
			this.BtnDelete_All.Name = "BtnDelete_All";
			this.BtnDelete_All.Size = new System.Drawing.Size(80, 26);
			this.BtnDelete_All.TabIndex = 286;
			this.BtnDelete_All.Tag = "Del";
			this.BtnDelete_All.Text = "전체 삭제";
			this.BtnDelete_All.Click += new System.EventHandler(this.BtnDelete_All_Click);
			// 
			// Btn_Down
			// 
			this.Btn_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Down.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Down.ImageOptions.Image")));
			this.Btn_Down.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
			this.Btn_Down.Location = new System.Drawing.Point(1187, 7);
			this.Btn_Down.Name = "Btn_Down";
			this.Btn_Down.Size = new System.Drawing.Size(80, 26);
			this.Btn_Down.TabIndex = 283;
			this.Btn_Down.Click += new System.EventHandler(this.Btn_RowMove);
			// 
			// Btn_Up
			// 
			this.Btn_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Up.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Up.ImageOptions.Image")));
			this.Btn_Up.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
			this.Btn_Up.Location = new System.Drawing.Point(1100, 7);
			this.Btn_Up.Name = "Btn_Up";
			this.Btn_Up.Size = new System.Drawing.Size(80, 26);
			this.Btn_Up.TabIndex = 282;
			this.Btn_Up.Click += new System.EventHandler(this.Btn_RowMove);
			// 
			// Btn_Row_Del
			// 
			this.Btn_Row_Del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Row_Del.ImageOptions.Image = global::SmartMES.Properties.Resources.cancel_16x16;
			this.Btn_Row_Del.Location = new System.Drawing.Point(1100, 39);
			this.Btn_Row_Del.Name = "Btn_Row_Del";
			this.Btn_Row_Del.Size = new System.Drawing.Size(80, 26);
			this.Btn_Row_Del.TabIndex = 280;
			this.Btn_Row_Del.Tag = "Del";
			this.Btn_Row_Del.Text = "삭제";
			this.Btn_Row_Del.Click += new System.EventHandler(this.Btn_RowEdit);
			// 
			// Btn_Row_Ins
			// 
			this.Btn_Row_Ins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Row_Ins.ImageOptions.Image = global::SmartMES.Properties.Resources.add_16x16;
			this.Btn_Row_Ins.Location = new System.Drawing.Point(1013, 39);
			this.Btn_Row_Ins.Name = "Btn_Row_Ins";
			this.Btn_Row_Ins.Size = new System.Drawing.Size(80, 26);
			this.Btn_Row_Ins.TabIndex = 278;
			this.Btn_Row_Ins.Tag = "Ins";
			this.Btn_Row_Ins.Text = " 신규 ";
			this.Btn_Row_Ins.Click += new System.EventHandler(this.Btn_RowEdit);
			// 
			// simpleButton13
			// 
			this.simpleButton13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.simpleButton13.Appearance.Options.UseTextOptions = true;
			this.simpleButton13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.simpleButton13.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton13.ImageOptions.Image")));
			this.simpleButton13.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
			this.simpleButton13.Location = new System.Drawing.Point(2436, 7);
			this.simpleButton13.Name = "simpleButton13";
			this.simpleButton13.Size = new System.Drawing.Size(114, 32);
			this.simpleButton13.TabIndex = 59;
			this.simpleButton13.Text = "바코드 출력";
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl1.Location = new System.Drawing.Point(11, 7);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(114, 26);
			this.labelControl1.TabIndex = 60;
			this.labelControl1.Text = "CCP 구분";
			// 
			// BtnCopyItem
			// 
			this.BtnCopyItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnCopyItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnCopyItem.ImageOptions.Image")));
			this.BtnCopyItem.Location = new System.Drawing.Point(855, 7);
			this.BtnCopyItem.Name = "BtnCopyItem";
			this.BtnCopyItem.Size = new System.Drawing.Size(118, 26);
			this.BtnCopyItem.TabIndex = 285;
			this.BtnCopyItem.Text = "품목참조등록";
			this.BtnCopyItem.Visible = false;
			this.BtnCopyItem.Click += new System.EventHandler(this.BtnCopyItem_Click);
			// 
			// Btn_All_Add
			// 
			this.Btn_All_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_All_Add.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_All_Add.ImageOptions.Image")));
			this.Btn_All_Add.Location = new System.Drawing.Point(979, 7);
			this.Btn_All_Add.Name = "Btn_All_Add";
			this.Btn_All_Add.Size = new System.Drawing.Size(118, 26);
			this.Btn_All_Add.TabIndex = 284;
			this.Btn_All_Add.Text = "검사일괄등록";
			this.Btn_All_Add.Visible = false;
			this.Btn_All_Add.Click += new System.EventHandler(this.Btn_All_Add_Click);
			// 
			// Btn_Add
			// 
			this.Btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Btn_Add.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Add.ImageOptions.Image")));
			this.Btn_Add.Location = new System.Drawing.Point(1102, 7);
			this.Btn_Add.Name = "Btn_Add";
			this.Btn_Add.Size = new System.Drawing.Size(88, 26);
			this.Btn_Add.TabIndex = 281;
			this.Btn_Add.Text = "일괄추가";
			this.Btn_Add.Visible = false;
			this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.groupControl1);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1283, 569);
			this.layoutControl1.TabIndex = 273;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1283, 569);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.groupControl1;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1283, 569);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.BtnItemImg);
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.BtnExam_Q);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Controls.Add(this.BtnCopyItem);
			this.smtCommonBar1.Controls.Add(this.Btn_Add);
			this.smtCommonBar1.Controls.Add(this.Btn_All_Add);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1283, 40);
			this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "CCP관리기준";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 274;
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
			this.cmdDisplay.Location = new System.Drawing.Point(121, 0);
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
			this.cmdClose.Location = new System.Drawing.Point(1206, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// Frm_QM_HACCPStd
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1283, 609);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_QM_HACCPStd";
			this.Text = "CCP관리기준";
			this.Load += new System.EventHandler(this.Frm_QM_ItemExamMgnt_Load);
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Std)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Std)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LupItemClass1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Rdo_Target_Div.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Rdo_CCP_Div.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnSave;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraGrid.GridControl Gc_Std;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Std;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.SimpleButton Btn_Row_Del;
		private DevExpress.XtraEditors.SimpleButton Btn_Row_Ins;
		private DevExpress.XtraEditors.RadioGroup Rdo_Target_Div;
		private DevExpress.XtraEditors.SimpleButton simpleButton13;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.SimpleButton Btn_Add;
		private DevExpress.XtraEditors.SimpleButton Btn_Down;
		private DevExpress.XtraEditors.SimpleButton Btn_Up;
        private DevExpress.XtraEditors.SimpleButton BtnExam_Q;
        private DevExpress.XtraEditors.SimpleButton Btn_All_Add;
		private DevExpress.XtraEditors.SimpleButton BtnCopyItem;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton BtnItemImg;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraEditors.SimpleButton BtnDelete_All;
		private DevExpress.XtraEditors.RadioGroup Rdo_CCP_Div;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.SearchLookUpEdit LupItemClass1;
		private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
	}
}