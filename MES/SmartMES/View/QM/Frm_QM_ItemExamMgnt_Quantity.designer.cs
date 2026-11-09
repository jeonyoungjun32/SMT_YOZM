
namespace SmartMES.View.QM
{
    partial class Frm_QM_ItemExamMgnt_Quantity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_ItemExamMgnt_Quantity));
            this.PnlMainTitle = new DevExpress.XtraEditors.PanelControl();
            this.Btn_Add = new DevExpress.XtraEditors.SimpleButton();
            this.LblMainTitle = new DevExpress.XtraEditors.LabelControl();
            this.Btn_Down = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Up = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Row_Add = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Row_Ins = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Row_Del = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_ItemExam = new DevExpress.XtraGrid.GridControl();
            this.Gv_ItemExam = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.LupExam = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Rdo_Exam_Div = new DevExpress.XtraEditors.RadioGroup();
            this.simpleButton13 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.PnlMainTitle)).BeginInit();
            this.PnlMainTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_ItemExam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ItemExam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LupExam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rdo_Exam_Div.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlMainTitle
            // 
            this.PnlMainTitle.Controls.Add(this.Btn_Add);
            this.PnlMainTitle.Controls.Add(this.LblMainTitle);
            this.PnlMainTitle.Controls.Add(this.Btn_Down);
            this.PnlMainTitle.Controls.Add(this.Btn_Up);
            this.PnlMainTitle.Controls.Add(this.Btn_Row_Add);
            this.PnlMainTitle.Controls.Add(this.Btn_Row_Ins);
            this.PnlMainTitle.Controls.Add(this.Btn_Row_Del);
            this.PnlMainTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.PnlMainTitle.Location = new System.Drawing.Point(0, 0);
            this.PnlMainTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PnlMainTitle.Name = "PnlMainTitle";
            this.PnlMainTitle.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PnlMainTitle.Size = new System.Drawing.Size(1399, 52);
            this.PnlMainTitle.TabIndex = 268;
            // 
            // Btn_Add
            // 
            this.Btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Add.ImageOptions.Image = global::SmartMES.Properties.Resources.productadd_16X16;
            this.Btn_Add.Location = new System.Drawing.Point(951, 9);
            this.Btn_Add.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(100, 29);
            this.Btn_Add.TabIndex = 281;
            this.Btn_Add.Text = "일괄추가";
            this.Btn_Add.Visible = false;
            // 
            // LblMainTitle
            // 
            this.LblMainTitle.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.LblMainTitle.Appearance.Options.UseFont = true;
            this.LblMainTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblMainTitle.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.LblMainTitle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("LblMainTitle.ImageOptions.Image")));
            this.LblMainTitle.Location = new System.Drawing.Point(11, 9);
            this.LblMainTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LblMainTitle.Name = "LblMainTitle";
            this.LblMainTitle.Size = new System.Drawing.Size(429, 29);
            this.LblMainTitle.TabIndex = 59;
            this.LblMainTitle.Text = "품목별 검사유형정보";
            // 
            // Btn_Down
            // 
            this.Btn_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Down.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.Btn_Down.Location = new System.Drawing.Point(847, 9);
            this.Btn_Down.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Btn_Down.Name = "Btn_Down";
            this.Btn_Down.Size = new System.Drawing.Size(100, 29);
            this.Btn_Down.TabIndex = 283;
            this.Btn_Down.Visible = false;
            // 
            // Btn_Up
            // 
            this.Btn_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Up.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.Btn_Up.Location = new System.Drawing.Point(739, 9);
            this.Btn_Up.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Btn_Up.Name = "Btn_Up";
            this.Btn_Up.Size = new System.Drawing.Size(100, 29);
            this.Btn_Up.TabIndex = 282;
            this.Btn_Up.Visible = false;
            // 
            // Btn_Row_Add
            // 
            this.Btn_Row_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Row_Add.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Row_Add.ImageOptions.Image")));
            this.Btn_Row_Add.Location = new System.Drawing.Point(1177, 9);
            this.Btn_Row_Add.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Btn_Row_Add.Name = "Btn_Row_Add";
            this.Btn_Row_Add.Size = new System.Drawing.Size(100, 29);
            this.Btn_Row_Add.TabIndex = 279;
            this.Btn_Row_Add.Text = "행추가";
            this.Btn_Row_Add.Visible = false;
            // 
            // Btn_Row_Ins
            // 
            this.Btn_Row_Ins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Row_Ins.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Row_Ins.ImageOptions.Image")));
            this.Btn_Row_Ins.Location = new System.Drawing.Point(1069, 9);
            this.Btn_Row_Ins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Btn_Row_Ins.Name = "Btn_Row_Ins";
            this.Btn_Row_Ins.Size = new System.Drawing.Size(100, 29);
            this.Btn_Row_Ins.TabIndex = 278;
            this.Btn_Row_Ins.Text = "행삽입";
            this.Btn_Row_Ins.Visible = false;
            // 
            // Btn_Row_Del
            // 
            this.Btn_Row_Del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Row_Del.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Row_Del.ImageOptions.Image")));
            this.Btn_Row_Del.Location = new System.Drawing.Point(1284, 8);
            this.Btn_Row_Del.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Btn_Row_Del.Name = "Btn_Row_Del";
            this.Btn_Row_Del.Size = new System.Drawing.Size(100, 29);
            this.Btn_Row_Del.TabIndex = 280;
            this.Btn_Row_Del.Text = "행삭제";
            this.Btn_Row_Del.Visible = false;
            // 
            // BtnSave
            // 
            this.BtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.ImageOptions.Image")));
            this.BtnSave.Location = new System.Drawing.Point(559, 47);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(100, 31);
            this.BtnSave.TabIndex = 266;
            this.BtnSave.Text = "저장";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl1.CaptionImageOptions.Image")));
            this.groupControl1.Controls.Add(this.Gc_ItemExam);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1395, 740);
            this.groupControl1.TabIndex = 271;
            this.groupControl1.Text = "항목 정보";
            // 
            // Gc_ItemExam
            // 
            this.Gc_ItemExam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_ItemExam.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Gc_ItemExam.Location = new System.Drawing.Point(2, 124);
            this.Gc_ItemExam.MainView = this.Gv_ItemExam;
            this.Gc_ItemExam.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Gc_ItemExam.Name = "Gc_ItemExam";
            this.Gc_ItemExam.Size = new System.Drawing.Size(1391, 614);
            this.Gc_ItemExam.TabIndex = 1;
            this.Gc_ItemExam.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_ItemExam,
            this.gridView1});
            // 
            // Gv_ItemExam
            // 
            this.Gv_ItemExam.DetailHeight = 500;
            this.Gv_ItemExam.GridControl = this.Gc_ItemExam;
            this.Gv_ItemExam.Name = "Gv_ItemExam";
            this.Gv_ItemExam.OptionsEditForm.PopupEditFormWidth = 1143;
            this.Gv_ItemExam.OptionsView.ShowGroupPanel = false;
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 500;
            this.gridView1.GridControl = this.Gc_ItemExam;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 1143;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.BtnSearch);
            this.panelControl1.Controls.Add(this.LupExam);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.BtnSave);
            this.panelControl1.Controls.Add(this.Rdo_Exam_Div);
            this.panelControl1.Controls.Add(this.simpleButton13);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 36);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelControl1.Size = new System.Drawing.Size(1391, 88);
            this.panelControl1.TabIndex = 280;
            // 
            // BtnSearch
            // 
            this.BtnSearch.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSearch.ImageOptions.Image")));
            this.BtnSearch.Location = new System.Drawing.Point(450, 47);
            this.BtnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(100, 31);
            this.BtnSearch.TabIndex = 290;
            this.BtnSearch.Text = "조회";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // LupExam
            // 
            this.LupExam.EditValue = "";
            this.LupExam.Location = new System.Drawing.Point(156, 47);
            this.LupExam.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LupExam.Name = "LupExam";
            this.LupExam.Properties.AutoHeight = false;
            this.LupExam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupExam.Properties.NullText = "";
            this.LupExam.Properties.PopupView = this.gridView2;
            this.LupExam.Size = new System.Drawing.Size(286, 31);
            this.LupExam.TabIndex = 289;
            this.LupExam.EditValueChanged += new System.EventHandler(this.LupExam_EditValueChanged);
            // 
            // gridView2
            // 
            this.gridView2.DetailHeight = 400;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsEditForm.PopupEditFormWidth = 1143;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(4, 47);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(143, 31);
            this.labelControl2.TabIndex = 281;
            this.labelControl2.Text = "검사명";
            // 
            // Rdo_Exam_Div
            // 
            this.Rdo_Exam_Div.Location = new System.Drawing.Point(156, 8);
            this.Rdo_Exam_Div.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Rdo_Exam_Div.Name = "Rdo_Exam_Div";
            this.Rdo_Exam_Div.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Rdo_Exam_Div.Properties.Appearance.Options.UseBackColor = true;
            this.Rdo_Exam_Div.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Rdo_Exam_Div.Size = new System.Drawing.Size(677, 31);
            this.Rdo_Exam_Div.TabIndex = 280;
            // 
            // simpleButton13
            // 
            this.simpleButton13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton13.Appearance.Options.UseTextOptions = true;
            this.simpleButton13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleButton13.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton13.ImageOptions.Image")));
            this.simpleButton13.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton13.Location = new System.Drawing.Point(2847, 8);
            this.simpleButton13.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.simpleButton13.Name = "simpleButton13";
            this.simpleButton13.Size = new System.Drawing.Size(143, 37);
            this.simpleButton13.TabIndex = 59;
            this.simpleButton13.Text = "바코드 출력";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(4, 8);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(143, 31);
            this.labelControl1.TabIndex = 60;
            this.labelControl1.Text = "검사구분";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("groupControl2.CaptionImageOptions.Image")));
            this.groupControl2.Controls.Add(this.groupControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 52);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(1399, 744);
            this.groupControl2.TabIndex = 271;
            this.groupControl2.Text = "품목별 검사유형정보";
            // 
            // Frm_QM_ItemExamMgnt_Quantity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1399, 796);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.PnlMainTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_QM_ItemExamMgnt_Quantity";
            this.Text = "품목별 검사유형정보";
            this.Load += new System.EventHandler(this.Frm_QM_ItemExamMgnt_Quantity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PnlMainTitle)).EndInit();
            this.PnlMainTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_ItemExam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ItemExam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LupExam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rdo_Exam_Div.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl PnlMainTitle;
        private DevExpress.XtraEditors.LabelControl LblMainTitle;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
		private DevExpress.XtraEditors.GroupControl groupControl1;
		private DevExpress.XtraGrid.GridControl Gc_ItemExam;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_ItemExam;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.SimpleButton Btn_Row_Del;
		private DevExpress.XtraEditors.SimpleButton Btn_Row_Add;
		private DevExpress.XtraEditors.SimpleButton Btn_Row_Ins;
		private DevExpress.XtraEditors.RadioGroup Rdo_Exam_Div;
		private DevExpress.XtraEditors.SimpleButton simpleButton13;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
		private DevExpress.XtraEditors.GroupControl groupControl2;
		private DevExpress.XtraEditors.SimpleButton Btn_Add;
		private DevExpress.XtraEditors.SimpleButton Btn_Down;
		private DevExpress.XtraEditors.SimpleButton Btn_Up;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SearchLookUpEdit LupExam;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
    }
}