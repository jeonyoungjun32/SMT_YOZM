
namespace SmartMES.View.Common
{
    partial class Frm_PictureEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PictureEdit));
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.BtnWorkSheet_History = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.BtnClose = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 97);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(769, 342);
            this.pictureEdit1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.BtnWorkSheet_History);
            this.panelControl1.Controls.Add(this.BtnUpload);
            this.panelControl1.Controls.Add(this.BtnSave);
            this.panelControl1.Controls.Add(this.BtnDelete);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(769, 33);
            this.panelControl1.TabIndex = 3;
            // 
            // BtnWorkSheet_History
            // 
            this.BtnWorkSheet_History.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnWorkSheet_History.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnWorkSheet_History.ImageOptions.Image")));
            this.BtnWorkSheet_History.Location = new System.Drawing.Point(435, 2);
            this.BtnWorkSheet_History.Name = "BtnWorkSheet_History";
            this.BtnWorkSheet_History.Size = new System.Drawing.Size(92, 29);
            this.BtnWorkSheet_History.TabIndex = 290;
            this.BtnWorkSheet_History.Text = "제품이력";
            this.BtnWorkSheet_History.Visible = false;
            this.BtnWorkSheet_History.Click += new System.EventHandler(this.BtnWorkSheet_History_Click);
            // 
            // BtnUpload
            // 
            this.BtnUpload.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnUpload.ImageOptions.Image = SmartMES.Properties.Resources.add_16x16;
            this.BtnUpload.Location = new System.Drawing.Point(527, 2);
            this.BtnUpload.Name = "BtnUpload";
            this.BtnUpload.Size = new System.Drawing.Size(80, 29);
            this.BtnUpload.TabIndex = 289;
            this.BtnUpload.Text = "업로드";
            this.BtnUpload.Click += new System.EventHandler(this.Btn_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnSave.ImageOptions.Image = SmartMES.Properties.Resources.save_16x16;
            this.BtnSave.Location = new System.Drawing.Point(607, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(80, 29);
            this.BtnSave.TabIndex = 288;
            this.BtnSave.Text = "저장";
            this.BtnSave.Click += new System.EventHandler(this.Btn_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnDelete.ImageOptions.Image")));
            this.BtnDelete.Location = new System.Drawing.Point(687, 2);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(80, 29);
            this.BtnDelete.TabIndex = 287;
            this.BtnDelete.Text = "삭제";
            this.BtnDelete.Click += new System.EventHandler(this.Btn_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.BtnClose.Appearance.Options.UseFont = true;
            this.BtnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnClose.ImageOptions.Image = SmartMES.Properties.Resources.close_16x16;
            this.BtnClose.Location = new System.Drawing.Point(675, 2);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(92, 60);
            this.BtnClose.TabIndex = 287;
            this.BtnClose.Text = "닫기";
            this.BtnClose.Click += new System.EventHandler(this.Btn_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.BtnClose);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 33);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(769, 64);
            this.panelControl2.TabIndex = 291;
            // 
            // FrmPictureEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 439);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "FrmPictureEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnDelete;
        private DevExpress.XtraEditors.SimpleButton BtnUpload;
        private DevExpress.XtraEditors.SimpleButton BtnWorkSheet_History;
		private DevExpress.XtraEditors.SimpleButton BtnClose;
		private DevExpress.XtraEditors.PanelControl panelControl2;
	}
}