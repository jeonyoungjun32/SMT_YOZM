
namespace SmartPOP.Views.PM
{
    partial class Frm_MachineDowntime
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MachineDowntime));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
			this.BtnIns = new DevExpress.XtraEditors.SimpleButton();
			this.BtnDel = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Send = new DevExpress.XtraEditors.SimpleButton();
			this.Btn_Back = new DevExpress.XtraEditors.SimpleButton();
			this.BtnOPEnd = new DevExpress.XtraEditors.SimpleButton();
			this.BtnOPStart = new DevExpress.XtraEditors.SimpleButton();
			this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.TxtOrderSeq = new DevExpress.XtraEditors.TextEdit();
			this.TxtLine = new DevExpress.XtraEditors.TextEdit();
			this.TxtProcess = new DevExpress.XtraEditors.TextEdit();
			this.TxtOrderNo = new DevExpress.XtraEditors.TextEdit();
			this.GcOperation = new DevExpress.XtraGrid.GridControl();
			this.GvOperation = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.TxtSubSeq = new DevExpress.XtraEditors.TextEdit();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layout_SubSeq = new DevExpress.XtraLayout.LayoutControlItem();
			this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
			this.BtnSelect = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
			this.tablePanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TxtOrderSeq.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtLine.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtProcess.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtOrderNo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GcOperation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvOperation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtSubSeq.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layout_SubSeq)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
			this.panelControl4.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.tablePanel2);
			this.layoutControl1.Controls.Add(this.TxtOrderSeq);
			this.layoutControl1.Controls.Add(this.TxtLine);
			this.layoutControl1.Controls.Add(this.TxtProcess);
			this.layoutControl1.Controls.Add(this.TxtOrderNo);
			this.layoutControl1.Controls.Add(this.GcOperation);
			this.layoutControl1.Controls.Add(this.TxtSubSeq);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 77);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1350, 592);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// tablePanel2
			// 
			this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 80F)});
			this.tablePanel2.Controls.Add(this.Btn_Back);
			this.tablePanel2.Controls.Add(this.BtnSearch);
			this.tablePanel2.Controls.Add(this.BtnOPStart);
			this.tablePanel2.Controls.Add(this.BtnIns);
			this.tablePanel2.Controls.Add(this.Btn_Send);
			this.tablePanel2.Controls.Add(this.BtnOPEnd);
			this.tablePanel2.Controls.Add(this.BtnDel);
			this.tablePanel2.Location = new System.Drawing.Point(1173, 82);
			this.tablePanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tablePanel2.Name = "tablePanel2";
			this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F)});
			this.tablePanel2.Size = new System.Drawing.Size(174, 507);
			this.tablePanel2.TabIndex = 379;
			// 
			// BtnIns
			// 
			this.BtnIns.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.BtnIns.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.BtnIns.Appearance.Options.UseBackColor = true;
			this.BtnIns.Appearance.Options.UseFont = true;
			this.tablePanel2.SetColumn(this.BtnIns, 0);
			this.BtnIns.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnIns.ImageOptions.SvgImage")));
			this.BtnIns.Location = new System.Drawing.Point(3, 144);
			this.BtnIns.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.BtnIns.Name = "BtnIns";
			this.tablePanel2.SetRow(this.BtnIns, 2);
			this.BtnIns.Size = new System.Drawing.Size(168, 69);
			this.BtnIns.TabIndex = 377;
			this.BtnIns.Text = "추가";
			this.BtnIns.Click += new System.EventHandler(this.BtnRowEdit_Click);
			// 
			// BtnDel
			// 
			this.BtnDel.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.BtnDel.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.BtnDel.Appearance.Options.UseBackColor = true;
			this.BtnDel.Appearance.Options.UseFont = true;
			this.tablePanel2.SetColumn(this.BtnDel, 0);
			this.BtnDel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnDel.ImageOptions.SvgImage")));
			this.BtnDel.Location = new System.Drawing.Point(3, 216);
			this.BtnDel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.BtnDel.Name = "BtnDel";
			this.tablePanel2.SetRow(this.BtnDel, 3);
			this.BtnDel.Size = new System.Drawing.Size(168, 69);
			this.BtnDel.TabIndex = 374;
			this.BtnDel.Text = "삭제";
			this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
			// 
			// Btn_Send
			// 
			this.Btn_Send.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.Btn_Send.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.Btn_Send.Appearance.Options.UseBackColor = true;
			this.Btn_Send.Appearance.Options.UseFont = true;
			this.tablePanel2.SetColumn(this.Btn_Send, 0);
			this.Btn_Send.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Btn_Send.ImageOptions.SvgImage")));
			this.Btn_Send.Location = new System.Drawing.Point(3, 288);
			this.Btn_Send.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.Btn_Send.Name = "Btn_Send";
			this.tablePanel2.SetRow(this.Btn_Send, 4);
			this.Btn_Send.Size = new System.Drawing.Size(168, 69);
			this.Btn_Send.TabIndex = 374;
			this.Btn_Send.Text = "저장";
			this.Btn_Send.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// Btn_Back
			// 
			this.Btn_Back.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.Btn_Back.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.Btn_Back.Appearance.Options.UseBackColor = true;
			this.Btn_Back.Appearance.Options.UseFont = true;
			this.tablePanel2.SetColumn(this.Btn_Back, 0);
			this.Btn_Back.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Btn_Back.ImageOptions.SvgImage")));
			this.Btn_Back.Location = new System.Drawing.Point(3, 432);
			this.Btn_Back.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.Btn_Back.Name = "Btn_Back";
			this.tablePanel2.SetRow(this.Btn_Back, 6);
			this.Btn_Back.Size = new System.Drawing.Size(168, 72);
			this.Btn_Back.TabIndex = 372;
			this.Btn_Back.Text = "메인화면";
			this.Btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
			// 
			// BtnOPEnd
			// 
			this.BtnOPEnd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.BtnOPEnd.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.BtnOPEnd.Appearance.Options.UseBackColor = true;
			this.BtnOPEnd.Appearance.Options.UseFont = true;
			this.tablePanel2.SetColumn(this.BtnOPEnd, 0);
			this.BtnOPEnd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnOPEnd.ImageOptions.SvgImage")));
			this.BtnOPEnd.Location = new System.Drawing.Point(3, 72);
			this.BtnOPEnd.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.BtnOPEnd.Name = "BtnOPEnd";
			this.tablePanel2.SetRow(this.BtnOPEnd, 1);
			this.BtnOPEnd.Size = new System.Drawing.Size(168, 69);
			this.BtnOPEnd.TabIndex = 380;
			this.BtnOPEnd.Text = "비가동 종료";
			this.BtnOPEnd.Click += new System.EventHandler(this.BtnOPEnd_Click);
			// 
			// BtnOPStart
			// 
			this.BtnOPStart.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
			this.BtnOPStart.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.BtnOPStart.Appearance.Options.UseBackColor = true;
			this.BtnOPStart.Appearance.Options.UseFont = true;
			this.tablePanel2.SetColumn(this.BtnOPStart, 0);
			this.BtnOPStart.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnOPStart.ImageOptions.SvgImage")));
			this.BtnOPStart.Location = new System.Drawing.Point(3, 0);
			this.BtnOPStart.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.BtnOPStart.Name = "BtnOPStart";
			this.tablePanel2.SetRow(this.BtnOPStart, 0);
			this.BtnOPStart.Size = new System.Drawing.Size(168, 69);
			this.BtnOPStart.TabIndex = 379;
			this.BtnOPStart.Text = "비가동 시작";
			this.BtnOPStart.Click += new System.EventHandler(this.BtnOPStart_Click);
			// 
			// BtnSearch
			// 
			this.BtnSearch.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.BtnSearch.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.BtnSearch.Appearance.Options.UseBackColor = true;
			this.BtnSearch.Appearance.Options.UseFont = true;
			this.tablePanel2.SetColumn(this.BtnSearch, 0);
			this.BtnSearch.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSearch.ImageOptions.SvgImage")));
			this.BtnSearch.Location = new System.Drawing.Point(3, 360);
			this.BtnSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.BtnSearch.Name = "BtnSearch";
			this.tablePanel2.SetRow(this.BtnSearch, 5);
			this.BtnSearch.Size = new System.Drawing.Size(168, 69);
			this.BtnSearch.TabIndex = 381;
			this.BtnSearch.Text = "조회";
			this.BtnSearch.Click += new System.EventHandler(this.simpleButton1_Click);
			// 
			// TxtOrderSeq
			// 
			this.TxtOrderSeq.Location = new System.Drawing.Point(370, 34);
			this.TxtOrderSeq.Name = "TxtOrderSeq";
			this.TxtOrderSeq.Properties.AllowFocused = false;
			this.TxtOrderSeq.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.TxtOrderSeq.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.TxtOrderSeq.Properties.Appearance.Options.UseBackColor = true;
			this.TxtOrderSeq.Properties.Appearance.Options.UseFont = true;
			this.TxtOrderSeq.Properties.ReadOnly = true;
			this.TxtOrderSeq.Size = new System.Drawing.Size(187, 38);
			this.TxtOrderSeq.StyleController = this.layoutControl1;
			this.TxtOrderSeq.TabIndex = 10;
			// 
			// TxtLine
			// 
			this.TxtLine.Location = new System.Drawing.Point(1205, 34);
			this.TxtLine.Name = "TxtLine";
			this.TxtLine.Properties.AllowFocused = false;
			this.TxtLine.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.TxtLine.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.TxtLine.Properties.Appearance.Options.UseBackColor = true;
			this.TxtLine.Properties.Appearance.Options.UseFont = true;
			this.TxtLine.Properties.ReadOnly = true;
			this.TxtLine.Size = new System.Drawing.Size(138, 38);
			this.TxtLine.StyleController = this.layoutControl1;
			this.TxtLine.TabIndex = 9;
			// 
			// TxtProcess
			// 
			this.TxtProcess.Location = new System.Drawing.Point(927, 34);
			this.TxtProcess.Name = "TxtProcess";
			this.TxtProcess.Properties.AllowFocused = false;
			this.TxtProcess.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.TxtProcess.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.TxtProcess.Properties.Appearance.Options.UseBackColor = true;
			this.TxtProcess.Properties.Appearance.Options.UseFont = true;
			this.TxtProcess.Properties.ReadOnly = true;
			this.TxtProcess.Size = new System.Drawing.Size(187, 38);
			this.TxtProcess.StyleController = this.layoutControl1;
			this.TxtProcess.TabIndex = 8;
			// 
			// TxtOrderNo
			// 
			this.TxtOrderNo.Location = new System.Drawing.Point(125, 34);
			this.TxtOrderNo.Name = "TxtOrderNo";
			this.TxtOrderNo.Properties.AllowFocused = false;
			this.TxtOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.TxtOrderNo.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.TxtOrderNo.Properties.Appearance.Options.UseBackColor = true;
			this.TxtOrderNo.Properties.Appearance.Options.UseFont = true;
			this.TxtOrderNo.Properties.ReadOnly = true;
			this.TxtOrderNo.Size = new System.Drawing.Size(154, 38);
			this.TxtOrderNo.StyleController = this.layoutControl1;
			this.TxtOrderNo.TabIndex = 7;
			// 
			// GcOperation
			// 
			this.GcOperation.Location = new System.Drawing.Point(19, 125);
			this.GcOperation.MainView = this.GvOperation;
			this.GcOperation.Name = "GcOperation";
			this.GcOperation.Size = new System.Drawing.Size(1132, 448);
			this.GcOperation.TabIndex = 6;
			this.GcOperation.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvOperation});
			// 
			// GvOperation
			// 
			this.GvOperation.GridControl = this.GcOperation;
			this.GvOperation.Name = "GvOperation";
			this.GvOperation.OptionsView.ShowGroupPanel = false;
			// 
			// TxtSubSeq
			// 
			this.TxtSubSeq.Location = new System.Drawing.Point(659, 34);
			this.TxtSubSeq.Name = "TxtSubSeq";
			this.TxtSubSeq.Properties.AllowFocused = false;
			this.TxtSubSeq.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.TxtSubSeq.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.TxtSubSeq.Properties.Appearance.Options.UseBackColor = true;
			this.TxtSubSeq.Properties.Appearance.Options.UseFont = true;
			this.TxtSubSeq.Properties.ReadOnly = true;
			this.TxtSubSeq.Size = new System.Drawing.Size(177, 38);
			this.TxtSubSeq.StyleController = this.layoutControl1;
			this.TxtSubSeq.TabIndex = 10;
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlItem1,
            this.layoutControlGroup2});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1350, 592);
			this.Root.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 79);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Size = new System.Drawing.Size(1170, 513);
			this.layoutControlGroup1.Text = "비가동 이력";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.GcOperation;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1138, 454);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.tablePanel2;
			this.layoutControlItem1.Location = new System.Drawing.Point(1170, 79);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(180, 513);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layout_SubSeq});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.OptionsPrint.AppearanceItem.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.layoutControlGroup2.OptionsPrint.AppearanceItem.Options.UseFont = true;
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(1350, 79);
			this.layoutControlGroup2.Text = "작업지시 정보";
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.layoutControlItem3.Control = this.TxtOrderNo;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(278, 44);
			this.layoutControlItem3.Text = "작업지시번호";
			this.layoutControlItem3.TextSize = new System.Drawing.Size(102, 24);
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.AppearanceItemCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.layoutControlItem6.Control = this.TxtOrderSeq;
			this.layoutControlItem6.Location = new System.Drawing.Point(278, 0);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Size = new System.Drawing.Size(278, 44);
			this.layoutControlItem6.Text = "지시순번";
			this.layoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem6.TextSize = new System.Drawing.Size(80, 10);
			this.layoutControlItem6.TextToControlDistance = 5;
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.AppearanceItemCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.layoutControlItem4.Control = this.TxtProcess;
			this.layoutControlItem4.Location = new System.Drawing.Point(835, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(278, 44);
			this.layoutControlItem4.Text = "공정";
			this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem4.TextSize = new System.Drawing.Size(80, 10);
			this.layoutControlItem4.TextToControlDistance = 5;
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.AppearanceItemCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
			this.layoutControlItem5.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layoutControlItem5.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.layoutControlItem5.Control = this.TxtLine;
			this.layoutControlItem5.Location = new System.Drawing.Point(1113, 0);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(229, 44);
			this.layoutControlItem5.Text = "라인";
			this.layoutControlItem5.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layoutControlItem5.TextSize = new System.Drawing.Size(80, 10);
			this.layoutControlItem5.TextToControlDistance = 5;
			// 
			// layout_SubSeq
			// 
			this.layout_SubSeq.AppearanceItemCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.layout_SubSeq.AppearanceItemCaption.Options.UseFont = true;
			this.layout_SubSeq.AppearanceItemCaption.Options.UseTextOptions = true;
			this.layout_SubSeq.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.layout_SubSeq.Control = this.TxtSubSeq;
			this.layout_SubSeq.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
			this.layout_SubSeq.CustomizationFormText = "작업지시순번";
			this.layout_SubSeq.Location = new System.Drawing.Point(556, 0);
			this.layout_SubSeq.Name = "layout_SubSeq";
			this.layout_SubSeq.Size = new System.Drawing.Size(279, 44);
			this.layout_SubSeq.Text = "작업순번";
			this.layout_SubSeq.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
			this.layout_SubSeq.TextSize = new System.Drawing.Size(80, 10);
			this.layout_SubSeq.TextToControlDistance = 16;
			// 
			// panelControl4
			// 
			this.panelControl4.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.panelControl4.Appearance.Options.UseBackColor = true;
			this.panelControl4.Controls.Add(this.BtnSelect);
			this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl4.Location = new System.Drawing.Point(0, 0);
			this.panelControl4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.panelControl4.Name = "panelControl4";
			this.panelControl4.Size = new System.Drawing.Size(1350, 77);
			this.panelControl4.TabIndex = 394;
			this.panelControl4.Visible = false;
			// 
			// BtnSelect
			// 
			this.BtnSelect.Appearance.Font = new System.Drawing.Font("맑은 고딕", 17F, System.Drawing.FontStyle.Bold);
			this.BtnSelect.Appearance.Options.UseFont = true;
			this.BtnSelect.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnSelect.ImageOptions.SvgImage")));
			this.BtnSelect.Location = new System.Drawing.Point(703, 2);
			this.BtnSelect.Margin = new System.Windows.Forms.Padding(0);
			this.BtnSelect.Name = "BtnSelect";
			this.BtnSelect.Size = new System.Drawing.Size(129, 73);
			this.BtnSelect.TabIndex = 378;
			this.BtnSelect.Text = "지시\r\n선택";
			this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
			// 
			// frmMachineDowntime
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1350, 669);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.panelControl4);
			this.Name = "frmMachineDowntime";
			this.Text = "비가동등록";
			this.Load += new System.EventHandler(this.frmSelectPM_ProductPlan_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
			this.tablePanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TxtOrderSeq.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtLine.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtProcess.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtOrderNo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GcOperation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvOperation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtSubSeq.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layout_SubSeq)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
			this.panelControl4.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl GcOperation;
        private DevExpress.XtraGrid.Views.Grid.GridView GvOperation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit TxtOrderSeq;
        private DevExpress.XtraEditors.TextEdit TxtLine;
        private DevExpress.XtraEditors.TextEdit TxtProcess;
        private DevExpress.XtraEditors.TextEdit TxtOrderNo;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
		private DevExpress.XtraEditors.PanelControl panelControl4;
		private DevExpress.XtraEditors.SimpleButton BtnIns;
		private DevExpress.XtraEditors.SimpleButton BtnDel;
		private DevExpress.XtraEditors.SimpleButton Btn_Send;
		private DevExpress.XtraEditors.SimpleButton Btn_Back;
		private DevExpress.XtraEditors.SimpleButton BtnSelect;
		private DevExpress.XtraEditors.TextEdit TxtSubSeq;
		private DevExpress.XtraLayout.LayoutControlItem layout_SubSeq;
		private DevExpress.XtraEditors.SimpleButton BtnOPEnd;
		private DevExpress.XtraEditors.SimpleButton BtnOPStart;
		private DevExpress.XtraEditors.SimpleButton BtnSearch;
		private DevExpress.Utils.Layout.TablePanel tablePanel2;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
	}
}