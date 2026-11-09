
namespace SmartMES.View
{
    partial class Frm_SettingConntionDB
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SettingConntionDB));
			this.TxtDBName = new DevExpress.XtraEditors.TextEdit();
			this.TxtUrl = new DevExpress.XtraEditors.TextEdit();
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			this.cmdTest = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.TxtDBName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtUrl.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TxtDBName
			// 
			this.TxtDBName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtDBName.Location = new System.Drawing.Point(130, 66);
			this.TxtDBName.Name = "TxtDBName";
			this.TxtDBName.Properties.AutoHeight = false;
			this.TxtDBName.Size = new System.Drawing.Size(386, 26);
			this.TxtDBName.TabIndex = 34;
			// 
			// TxtUrl
			// 
			this.TxtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtUrl.Location = new System.Drawing.Point(130, 33);
			this.TxtUrl.Name = "TxtUrl";
			this.TxtUrl.Properties.AutoHeight = false;
			this.TxtUrl.Size = new System.Drawing.Size(386, 26);
			this.TxtUrl.TabIndex = 33;
			// 
			// groupControl2
			// 
			this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
			this.groupControl2.AppearanceCaption.Options.UseFont = true;
			this.groupControl2.Controls.Add(this.labelControl1);
			this.groupControl2.Controls.Add(this.labelControl2);
			this.groupControl2.Controls.Add(this.TxtUrl);
			this.groupControl2.Controls.Add(this.TxtDBName);
			this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl2.Location = new System.Drawing.Point(0, 40);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Padding = new System.Windows.Forms.Padding(3);
			this.groupControl2.Size = new System.Drawing.Size(524, 147);
			this.groupControl2.TabIndex = 272;
			this.groupControl2.Text = "연결 정보";
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl1.Location = new System.Drawing.Point(9, 66);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(114, 26);
			this.labelControl1.TabIndex = 283;
			this.labelControl1.Text = "Database 명";
			// 
			// labelControl2
			// 
			this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl2.Location = new System.Drawing.Point(9, 33);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(114, 26);
			this.labelControl2.TabIndex = 282;
			this.labelControl2.Text = "연결 주소";
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.cmdTest);
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(524, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "연결 설정";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 273;
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
			this.cmdSave.Location = new System.Drawing.Point(370, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 4;
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
			this.cmdClose.Location = new System.Drawing.Point(447, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 5;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// cmdTest
			// 
			this.cmdTest.AllowFocus = false;
			this.cmdTest.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdTest.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdTest.Appearance.Options.UseBackColor = true;
			this.cmdTest.Appearance.Options.UseForeColor = true;
			this.cmdTest.AutoSize = true;
			this.cmdTest.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdTest.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdTest.ImageOptions.ImageIndex = 6;
			this.cmdTest.ImageOptions.ImageToTextIndent = 15;
			this.cmdTest.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdTest.Location = new System.Drawing.Point(281, 0);
			this.cmdTest.Name = "cmdTest";
			this.cmdTest.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdTest.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdTest.Size = new System.Drawing.Size(89, 40);
			this.cmdTest.smtButtonStyle = baseSMT.EnumButtonStyles.확정;
			this.cmdTest.TabIndex = 6;
			this.cmdTest.Text = "테스트";
			this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
			// 
			// Frm_SettingConntionDB
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(524, 187);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.smtCommonBar1);
			this.MinimumSize = new System.Drawing.Size(338, 221);
			this.Name = "Frm_SettingConntionDB";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "연결 설정";
			this.Load += new System.EventHandler(this.Frm_SetConnectionDB_Load);
			((System.ComponentModel.ISupportInitialize)(this.TxtDBName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TxtUrl.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit TxtDBName;
        private DevExpress.XtraEditors.TextEdit TxtUrl;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdTest;
		private baseSMT.smtSearchButton cmdClose;
	}
}