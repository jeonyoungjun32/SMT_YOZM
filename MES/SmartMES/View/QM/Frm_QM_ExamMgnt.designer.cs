
namespace SmartMES.View.QM
{
    partial class Frm_QM_ExamMgnt
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_ExamMgnt));
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.Gc_Exam = new DevExpress.XtraGrid.GridControl();
			this.Gv_Exam = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
			this.label2 = new System.Windows.Forms.Label();
			this.Rdo_Exam_Div = new DevExpress.XtraEditors.RadioGroup();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdIns = new baseSMT.smtSearchButton();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdDel = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Exam)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Exam)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
			this.panelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Rdo_Exam_Div.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupControl2
			// 
			this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
			this.groupControl2.AppearanceCaption.Options.UseFont = true;
			this.groupControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.groupControl2.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl2.CaptionImageOptions.Image")));
			this.groupControl2.Controls.Add(this.Gc_Exam);
			this.groupControl2.Controls.Add(this.panelControl3);
			this.groupControl2.Location = new System.Drawing.Point(7, 34);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(1014, 454);
			this.groupControl2.TabIndex = 271;
			this.groupControl2.Text = "검사유형정보";
			// 
			// Gc_Exam
			// 
			this.Gc_Exam.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_Exam.Location = new System.Drawing.Point(2, 75);
			this.Gc_Exam.MainView = this.Gv_Exam;
			this.Gc_Exam.Name = "Gc_Exam";
			this.Gc_Exam.Size = new System.Drawing.Size(1010, 377);
			this.Gc_Exam.TabIndex = 1;
			this.Gc_Exam.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Exam});
			// 
			// Gv_Exam
			// 
			this.Gv_Exam.DetailHeight = 397;
			this.Gv_Exam.GridControl = this.Gc_Exam;
			this.Gv_Exam.Name = "Gv_Exam";
			this.Gv_Exam.OptionsView.ShowGroupPanel = false;
			// 
			// panelControl3
			// 
			this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl3.Controls.Add(this.label2);
			this.panelControl3.Controls.Add(this.Rdo_Exam_Div);
			this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl3.Location = new System.Drawing.Point(2, 29);
			this.panelControl3.Name = "panelControl3";
			this.panelControl3.Padding = new System.Windows.Forms.Padding(3);
			this.panelControl3.Size = new System.Drawing.Size(1010, 46);
			this.panelControl3.TabIndex = 279;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(719, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 17);
			this.label2.TabIndex = 286;
			this.label2.Text = "<- radio있음";
			this.label2.Visible = false;
			// 
			// Rdo_Exam_Div
			// 
			this.Rdo_Exam_Div.Location = new System.Drawing.Point(7, 9);
			this.Rdo_Exam_Div.Name = "Rdo_Exam_Div";
			this.Rdo_Exam_Div.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.Rdo_Exam_Div.Properties.Appearance.Options.UseBackColor = true;
			this.Rdo_Exam_Div.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.Rdo_Exam_Div.Size = new System.Drawing.Size(776, 28);
			this.Rdo_Exam_Div.TabIndex = 280;
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.groupControl2);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1028, 495);
			this.layoutControl1.TabIndex = 272;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1028, 495);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.groupControl2;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(1020, 460);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
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
			this.smtCommonBar1.Size = new System.Drawing.Size(1028, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "검사유형정보";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 273;
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
			this.cmdIns.Location = new System.Drawing.Point(720, 0);
			this.cmdIns.Name = "cmdIns";
			this.cmdIns.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdIns.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdIns.Size = new System.Drawing.Size(77, 40);
			this.cmdIns.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
			this.cmdIns.TabIndex = 7;
			this.cmdIns.Text = "등록";
			this.cmdIns.Click += new System.EventHandler(this.cmdRowEdit_Click);
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
			this.cmdDisplay.Location = new System.Drawing.Point(122, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
			this.cmdDisplay.Visible = false;
			this.cmdDisplay.Click += new System.EventHandler(this.Btn_Search_Click);
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
			this.cmdSave.Location = new System.Drawing.Point(797, 0);
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
			this.cmdDel.Location = new System.Drawing.Point(874, 0);
			this.cmdDel.Name = "cmdDel";
			this.cmdDel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDel.Size = new System.Drawing.Size(77, 40);
			this.cmdDel.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
			this.cmdDel.TabIndex = 2;
			this.cmdDel.Text = "삭제";
			this.cmdDel.Click += new System.EventHandler(this.cmdRowEdit_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(951, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(1028, 495);
			this.layoutControlGroup1.Text = "검사유형정보";
			// 
			// Frm_QM_ExamMgnt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1028, 535);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_QM_ExamMgnt";
			this.Text = "검사유형정보";
			this.Load += new System.EventHandler(this.Frm_QM_Exam_Mgnt_Load);
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Exam)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Exam)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
			this.panelControl3.ResumeLayout(false);
			this.panelControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Rdo_Exam_Div.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl Gc_Exam;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Exam;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.RadioGroup Rdo_Exam_Div;
        private System.Windows.Forms.Label label2;
		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdIns;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdDel;
		private baseSMT.smtSearchButton cmdClose;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
	}
}