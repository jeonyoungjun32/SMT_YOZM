namespace SmartMES.View.Chatbot
{
    partial class Frm_Chat_ScenarioManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Chat_ScenarioManagement));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.SearchTxtTitle = new baseSMT.smtSearchTextBox();
            this.LupProcess = new baseSMT.smtSearchLookUp();
            this.cmdIns = new baseSMT.smtSearchButton();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.cmdDel = new baseSMT.smtSearchButton();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.Dc = new DevExpress.XtraDiagram.DiagramControl();
            this.Gc_List = new DevExpress.XtraGrid.GridControl();
            this.Gv_List = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleSeparator1 = new DevExpress.XtraLayout.SimpleSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.SearchTxtTitle);
            this.smtCommonBar1.Controls.Add(this.LupProcess);
            this.smtCommonBar1.Controls.Add(this.cmdIns);
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.cmdDel);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1313, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "챗봇시나리오관리";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 7;
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
            this.cmdDisplay.Location = new System.Drawing.Point(644, 0);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 34;
            this.cmdDisplay.Text = "조회";
            // 
            // SearchTxtTitle
            // 
            this.SearchTxtTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.SearchTxtTitle.Location = new System.Drawing.Point(389, 0);
            this.SearchTxtTitle.LookAndFeel.SkinName = "WXI";
            this.SearchTxtTitle.LookAndFeel.UseDefaultLookAndFeel = false;
            this.SearchTxtTitle.Name = "SearchTxtTitle";
            this.SearchTxtTitle.Size = new System.Drawing.Size(255, 40);
            this.SearchTxtTitle.smtLabelTitleValue = "시나리오명";
            this.SearchTxtTitle.smtLabelTitleVisible = true;
            this.SearchTxtTitle.smtLabelTitleWidth = 100;
            this.SearchTxtTitle.smtTextBoxDecimal = 0;
            this.SearchTxtTitle.smtTextBoxMust = false;
            this.SearchTxtTitle.smtTextBoxNumber = false;
            this.SearchTxtTitle.smtTextBoxReadOnly = false;
            this.SearchTxtTitle.smtTextBoxValue = "";
            this.SearchTxtTitle.TabIndex = 33;
            // 
            // LupProcess
            // 
            this.LupProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupProcess.Location = new System.Drawing.Point(140, 0);
            this.LupProcess.LookAndFeel.SkinName = "WXI";
            this.LupProcess.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupProcess.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LupProcess.Name = "LupProcess";
            this.LupProcess.Size = new System.Drawing.Size(249, 40);
            this.LupProcess.smtLabelForeColor = System.Drawing.Color.White;
            this.LupProcess.smtLabelTitleValue = "시나리오 구분";
            this.LupProcess.smtLabelTitleVisible = true;
            this.LupProcess.smtLabelTitleWidth = 100;
            this.LupProcess.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupProcess.smtLookUpDataDisplayMember = "";
            this.LupProcess.smtLookUpDataSource = null;
            this.LupProcess.smtLookUpDataValueMember = "";
            this.LupProcess.smtLookUpSelectdIndex = -1;
            this.LupProcess.smtLookUpText = "";
            this.LupProcess.smtLookUpValue = "";
            this.LupProcess.TabIndex = 31;
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
            this.cmdIns.Location = new System.Drawing.Point(868, 0);
            this.cmdIns.Name = "cmdIns";
            this.cmdIns.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdIns.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdIns.Size = new System.Drawing.Size(101, 40);
            this.cmdIns.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.cmdIns.TabIndex = 7;
            this.cmdIns.Tag = "Ins";
            this.cmdIns.Text = "신규등록";
            this.cmdIns.Click += new System.EventHandler(this.Btn_RowEdit);
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
            this.cmdSave.Location = new System.Drawing.Point(969, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 3;
            this.cmdSave.Text = "저장";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
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
            this.cmdDel.Location = new System.Drawing.Point(1046, 0);
            this.cmdDel.Name = "cmdDel";
            this.cmdDel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDel.Size = new System.Drawing.Size(77, 40);
            this.cmdDel.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.cmdDel.TabIndex = 2;
            this.cmdDel.Tag = "Del";
            this.cmdDel.Text = "삭제";
            this.cmdDel.Click += new System.EventHandler(this.Btn_RowEdit);
            // 
            // cmdExcel
            // 
            this.cmdExcel.AllowFocus = false;
            this.cmdExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.cmdExcel.Appearance.ForeColor = System.Drawing.Color.White;
            this.cmdExcel.Appearance.Options.UseBackColor = true;
            this.cmdExcel.Appearance.Options.UseForeColor = true;
            this.cmdExcel.AutoSize = true;
            this.cmdExcel.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdExcel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.cmdExcel.ImageOptions.ImageIndex = 9;
            this.cmdExcel.ImageOptions.ImageToTextIndent = 15;
            this.cmdExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.cmdExcel.Location = new System.Drawing.Point(1123, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(113, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 6;
            this.cmdExcel.Text = "흐름테스트";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
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
            this.cmdClose.Location = new System.Drawing.Point(1236, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.label1);
            this.layoutControl1.Controls.Add(this.Dc);
            this.layoutControl1.Controls.Add(this.Gc_List);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1313, 483);
            this.layoutControl1.TabIndex = 8;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // label1
            // 
            this.label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label1.Location = new System.Drawing.Point(3, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1307, 32);
            this.label1.StyleController = this.layoutControl1;
            this.label1.TabIndex = 103;
            this.label1.Text = "대화 흐름 (MES 접속 안내)";
            // 
            // Dc
            // 
            this.Dc.Location = new System.Drawing.Point(3, 264);
            this.Dc.Name = "Dc";
            this.Dc.OptionsBehavior.SelectedStencils = new DevExpress.Diagram.Core.StencilCollection(new string[] {
            "BasicShapes",
            "BasicFlowchartShapes"});
            this.Dc.OptionsView.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Dc.Size = new System.Drawing.Size(1307, 216);
            this.Dc.StyleController = this.layoutControl1;
            this.Dc.TabIndex = 102;
            this.Dc.Text = "diagramControl1";
            // 
            // Gc_List
            // 
            this.Gc_List.Location = new System.Drawing.Point(3, 3);
            this.Gc_List.MainView = this.Gv_List;
            this.Gc_List.Name = "Gc_List";
            this.Gc_List.Size = new System.Drawing.Size(1307, 216);
            this.Gc_List.TabIndex = 101;
            this.Gc_List.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_List});
            // 
            // Gv_List
            // 
            this.Gv_List.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.Gv_List.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Gv_List.ColumnPanelRowHeight = 35;
            this.Gv_List.DetailHeight = 497;
            this.Gv_List.GridControl = this.Gc_List;
            this.Gv_List.Name = "Gv_List";
            this.Gv_List.OptionsView.ShowGroupPanel = false;
            this.Gv_List.RowHeight = 35;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.simpleSeparator1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1313, 483);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.Gc_List;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1313, 222);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Dc;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 261);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1313, 222);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.label1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 222);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 38);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(153, 38);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1313, 38);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleSeparator1
            // 
            this.simpleSeparator1.AllowHotTrack = false;
            this.simpleSeparator1.Location = new System.Drawing.Point(0, 260);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new System.Drawing.Size(1313, 1);
            // 
            // Frm_Chat_ScenarioManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 523);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Chat_ScenarioManagement";
            this.Text = "챗봇시나리오관리";
            this.Load += new System.EventHandler(this.Frm_CM_DefectCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Dc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleSeparator1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchLookUp LupProcess;
        private baseSMT.smtSearchButton cmdIns;
        private baseSMT.smtSearchButton cmdSave;
        private baseSMT.smtSearchButton cmdDel;
        private baseSMT.smtSearchButton cmdExcel;
        private baseSMT.smtSearchButton cmdClose;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl Gc_List;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_List;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private baseSMT.smtSearchButton cmdDisplay;
        private baseSMT.smtSearchTextBox SearchTxtTitle;
        private DevExpress.XtraDiagram.DiagramControl Dc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleSeparator simpleSeparator1;
    }
}