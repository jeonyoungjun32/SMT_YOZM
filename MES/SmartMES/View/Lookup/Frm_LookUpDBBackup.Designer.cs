
namespace SmartMES.View.Lookup
{
    partial class Frm_LookUpDBBackup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_LookUpDBBackup));
			this.lup_Corp = new baseSMT.smtBodyLookUp();
			this.smtCommonBar2 = new baseSMT.smtCommonBar();
			this.btn_Save = new baseSMT.smtSearchButton();
			this.smtSearchButton1 = new baseSMT.smtSearchButton();
			this.dateTimeBack = new baseSMT.smtBodyDatetime();
			this.txt_SaveName = new baseSMT.smtBodyTextBox();
			this.txt_Path = new baseSMT.smtBodyTextBox();
			this.txt_DBname = new baseSMT.smtBodyTextBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.lblStatus = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).BeginInit();
			this.smtCommonBar2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lup_Corp
			// 
			this.lup_Corp.Location = new System.Drawing.Point(12, 66);
			this.lup_Corp.Name = "lup_Corp";
			this.lup_Corp.Size = new System.Drawing.Size(288, 32);
			this.lup_Corp.smtLabelTitleValue = "사업장";
			this.lup_Corp.smtLabelTitleVisible = true;
			this.lup_Corp.smtLabelTitleWidth = 120;
			this.lup_Corp.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.lup_Corp.smtLookUpDataDisplayMember = "";
			this.lup_Corp.smtLookUpDataSource = null;
			this.lup_Corp.smtLookUpDataValueMember = "";
			this.lup_Corp.smtLookUpMust = false;
			this.lup_Corp.smtLookUpReadOnly = true;
			this.lup_Corp.smtLookUpSelectdIndex = -1;
			this.lup_Corp.smtLookUpText = "";
			this.lup_Corp.smtLookUpValue = "";
			this.lup_Corp.TabIndex = 6;
			// 
			// smtCommonBar2
			// 
			this.smtCommonBar2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar2.Appearance.Options.UseBackColor = true;
			this.smtCommonBar2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar2.Controls.Add(this.btn_Save);
			this.smtCommonBar2.Controls.Add(this.smtSearchButton1);
			this.smtCommonBar2.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar2.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar2.Name = "smtCommonBar2";
			this.smtCommonBar2.Size = new System.Drawing.Size(964, 40);
			this.smtCommonBar2.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar2.smtImageTitleAutoText = false;
			this.smtCommonBar2.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar2.smtImageTitleSvgImage")));
			this.smtCommonBar2.smtImageTitleText = "DB백업정보";
			this.smtCommonBar2.smtImageTitleVisible = true;
			this.smtCommonBar2.TabIndex = 94;
			// 
			// btn_Save
			// 
			this.btn_Save.AllowFocus = false;
			this.btn_Save.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.btn_Save.Appearance.ForeColor = System.Drawing.Color.White;
			this.btn_Save.Appearance.Options.UseBackColor = true;
			this.btn_Save.Appearance.Options.UseForeColor = true;
			this.btn_Save.AutoSize = true;
			this.btn_Save.Dock = System.Windows.Forms.DockStyle.Right;
			this.btn_Save.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.btn_Save.ImageOptions.ImageIndex = 4;
			this.btn_Save.ImageOptions.ImageToTextIndent = 15;
			this.btn_Save.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.btn_Save.Location = new System.Drawing.Point(810, 0);
			this.btn_Save.Name = "btn_Save";
			this.btn_Save.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btn_Save.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btn_Save.Size = new System.Drawing.Size(77, 40);
			this.btn_Save.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.btn_Save.TabIndex = 2;
			this.btn_Save.Text = "저장";
			this.btn_Save.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// smtSearchButton1
			// 
			this.smtSearchButton1.AllowFocus = false;
			this.smtSearchButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtSearchButton1.Appearance.ForeColor = System.Drawing.Color.White;
			this.smtSearchButton1.Appearance.Options.UseBackColor = true;
			this.smtSearchButton1.Appearance.Options.UseForeColor = true;
			this.smtSearchButton1.AutoSize = true;
			this.smtSearchButton1.Dock = System.Windows.Forms.DockStyle.Right;
			this.smtSearchButton1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.smtSearchButton1.ImageOptions.ImageIndex = 5;
			this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
			this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.smtSearchButton1.Location = new System.Drawing.Point(887, 0);
			this.smtSearchButton1.Name = "smtSearchButton1";
			this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
			this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.smtSearchButton1.TabIndex = 1;
			this.smtSearchButton1.Text = "닫기";
			this.smtSearchButton1.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// dateTimeBack
			// 
			this.dateTimeBack.Location = new System.Drawing.Point(12, 104);
			this.dateTimeBack.LookAndFeel.SkinName = "WXI";
			this.dateTimeBack.LookAndFeel.UseDefaultLookAndFeel = false;
			this.dateTimeBack.Name = "dateTimeBack";
			this.dateTimeBack.Size = new System.Drawing.Size(288, 32);
			this.dateTimeBack.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.dateTimeBack.smtDateTimeMust = false;
			this.dateTimeBack.smtDateTimeReadOnly = true;
			this.dateTimeBack.smtDateTimeValue = new System.DateTime(((long)(0)));
			this.dateTimeBack.smtLabelTitleValue = "백업일자";
			this.dateTimeBack.smtLabelTitleVisible = true;
			this.dateTimeBack.smtLabelTitleWidth = 120;
			this.dateTimeBack.TabIndex = 95;
			// 
			// txt_SaveName
			// 
			this.txt_SaveName.Location = new System.Drawing.Point(12, 142);
			this.txt_SaveName.LookAndFeel.SkinName = "WXI";
			this.txt_SaveName.LookAndFeel.UseDefaultLookAndFeel = false;
			this.txt_SaveName.Name = "txt_SaveName";
			this.txt_SaveName.Size = new System.Drawing.Size(288, 32);
			this.txt_SaveName.smtLabelTitleValue = "백업파일명";
			this.txt_SaveName.smtLabelTitleVisible = true;
			this.txt_SaveName.smtLabelTitleWidth = 120;
			this.txt_SaveName.smtTextBoxDecimal = 0;
			this.txt_SaveName.smtTextBoxMust = true;
			this.txt_SaveName.smtTextBoxNumber = false;
			this.txt_SaveName.smtTextBoxReadOnly = false;
			this.txt_SaveName.smtTextBoxValue = "";
			this.txt_SaveName.TabIndex = 96;
			// 
			// txt_Path
			// 
			this.txt_Path.Location = new System.Drawing.Point(12, 180);
			this.txt_Path.LookAndFeel.SkinName = "WXI";
			this.txt_Path.LookAndFeel.UseDefaultLookAndFeel = false;
			this.txt_Path.Name = "txt_Path";
			this.txt_Path.Size = new System.Drawing.Size(929, 32);
			this.txt_Path.smtLabelTitleValue = "백업폴더명";
			this.txt_Path.smtLabelTitleVisible = true;
			this.txt_Path.smtLabelTitleWidth = 120;
			this.txt_Path.smtTextBoxDecimal = 0;
			this.txt_Path.smtTextBoxMust = false;
			this.txt_Path.smtTextBoxNumber = false;
			this.txt_Path.smtTextBoxReadOnly = true;
			this.txt_Path.smtTextBoxValue = "";
			this.txt_Path.TabIndex = 97;
			// 
			// txt_DBname
			// 
			this.txt_DBname.Location = new System.Drawing.Point(12, 218);
			this.txt_DBname.LookAndFeel.SkinName = "WXI";
			this.txt_DBname.LookAndFeel.UseDefaultLookAndFeel = false;
			this.txt_DBname.Name = "txt_DBname";
			this.txt_DBname.Size = new System.Drawing.Size(288, 32);
			this.txt_DBname.smtLabelTitleValue = "데이터베이스명";
			this.txt_DBname.smtLabelTitleVisible = true;
			this.txt_DBname.smtLabelTitleWidth = 120;
			this.txt_DBname.smtTextBoxDecimal = 0;
			this.txt_DBname.smtTextBoxMust = false;
			this.txt_DBname.smtTextBoxNumber = false;
			this.txt_DBname.smtTextBoxReadOnly = true;
			this.txt_DBname.smtTextBoxValue = "";
			this.txt_DBname.TabIndex = 98;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(776, 46);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(176, 23);
			this.progressBar1.TabIndex = 99;
			// 
			// lblStatus
			// 
			this.lblStatus.Appearance.Options.UseTextOptions = true;
			this.lblStatus.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.lblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblStatus.Location = new System.Drawing.Point(539, 46);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(231, 23);
			this.lblStatus.TabIndex = 100;
			// 
			// Frm_LookUpDBBackup
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(964, 267);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.txt_DBname);
			this.Controls.Add(this.txt_Path);
			this.Controls.Add(this.txt_SaveName);
			this.Controls.Add(this.dateTimeBack);
			this.Controls.Add(this.smtCommonBar2);
			this.Controls.Add(this.lup_Corp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.LookAndFeel.SkinName = "WXI";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Name = "Frm_LookUpDBBackup";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.Frm_LookUpDBBackup_Load);
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar2)).EndInit();
			this.smtCommonBar2.ResumeLayout(false);
			this.smtCommonBar2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtBodyLookUp lup_Corp;
        private baseSMT.smtCommonBar smtCommonBar2;
        private baseSMT.smtSearchButton btn_Save;
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtBodyDatetime dateTimeBack;
        private baseSMT.smtBodyTextBox txt_SaveName;
        private baseSMT.smtBodyTextBox txt_Path;
        private baseSMT.smtBodyTextBox txt_DBname;
		private System.Windows.Forms.ProgressBar progressBar1;
		private DevExpress.XtraEditors.LabelControl lblStatus;
	}
}