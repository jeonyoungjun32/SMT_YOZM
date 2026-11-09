namespace SmartMES.View.SM
{
	partial class Frm_SM_Notice
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SM_Notice));
			this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
			this.cmdSave = new DevExpress.XtraBars.Navigation.NavButton();
			this.navButton1 = new DevExpress.XtraBars.Navigation.NavButton();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.btn_Save = new baseSMT.smtSearchButton();
			this.btn_Close = new baseSMT.smtSearchButton();
			this.WriteDt = new baseSMT.smtBodyDatetime();
			this.txt_BoardNo = new baseSMT.smtBodyTextBox();
			this.txt_Subject = new baseSMT.smtBodyTextBox();
			this.StartDt = new baseSMT.smtBodyDatetime();
			this.EndDt = new baseSMT.smtBodyDatetime();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.fontRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.FontRibbonPageGroup();
			this.fontRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.FontRibbonPageGroup();
			this.changeFontNameItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontNameItem();
			this.changeFontSizeItem2 = new DevExpress.XtraRichEdit.UI.ChangeFontSizeItem();
			this.illustrationsBar1 = new DevExpress.XtraRichEdit.UI.IllustrationsBar();
			this.smtBodyRichEdit1 = new baseSMT.smtBodyRichEdit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// repositoryItemFontEdit1
			// 
			this.repositoryItemFontEdit1.AutoHeight = false;
			this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
			// 
			// cmdSave
			// 
			this.cmdSave.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
			this.cmdSave.Caption = "저 장";
			this.cmdSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.ImageOptions.Image")));
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Visible = false;
			// 
			// navButton1
			// 
			this.navButton1.Alignment = DevExpress.XtraBars.Navigation.NavButtonAlignment.Right;
			this.navButton1.Caption = "저 장";
			this.navButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("navButton1.ImageOptions.Image")));
			this.navButton1.Name = "navButton1";
			this.navButton1.Visible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.btn_Save);
			this.smtCommonBar1.Controls.Add(this.btn_Close);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1198, 40);
			this.smtCommonBar1.smtImageTitleAutoText = false;
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "공지사항정보";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 187;
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
			this.btn_Save.Location = new System.Drawing.Point(1044, 0);
			this.btn_Save.Name = "btn_Save";
			this.btn_Save.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btn_Save.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btn_Save.Size = new System.Drawing.Size(77, 40);
			this.btn_Save.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.btn_Save.TabIndex = 2;
			this.btn_Save.Text = "저장";
			this.btn_Save.Click += new System.EventHandler(this.Btn_Click);
			// 
			// btn_Close
			// 
			this.btn_Close.AllowFocus = false;
			this.btn_Close.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.btn_Close.Appearance.ForeColor = System.Drawing.Color.White;
			this.btn_Close.Appearance.Options.UseBackColor = true;
			this.btn_Close.Appearance.Options.UseForeColor = true;
			this.btn_Close.AutoSize = true;
			this.btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
			this.btn_Close.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.btn_Close.ImageOptions.ImageIndex = 5;
			this.btn_Close.ImageOptions.ImageToTextIndent = 15;
			this.btn_Close.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.btn_Close.Location = new System.Drawing.Point(1121, 0);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btn_Close.Size = new System.Drawing.Size(77, 40);
			this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.btn_Close.TabIndex = 1;
			this.btn_Close.Text = "닫기";
			this.btn_Close.Click += new System.EventHandler(this.Btn_Click);
			// 
			// WriteDt
			// 
			this.WriteDt.Location = new System.Drawing.Point(379, 22);
			this.WriteDt.LookAndFeel.SkinName = "WXI";
			this.WriteDt.LookAndFeel.UseDefaultLookAndFeel = false;
			this.WriteDt.Name = "WriteDt";
			this.WriteDt.Size = new System.Drawing.Size(288, 32);
			this.WriteDt.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.WriteDt.smtDateTimeMust = false;
			this.WriteDt.smtDateTimeReadOnly = true;
			this.WriteDt.smtDateTimeValue = new System.DateTime(((long)(0)));
			this.WriteDt.smtLabelTitleValue = "작성일자";
			this.WriteDt.smtLabelTitleVisible = true;
			this.WriteDt.smtLabelTitleWidth = 100;
			this.WriteDt.TabIndex = 188;
			// 
			// txt_BoardNo
			// 
			this.txt_BoardNo.Location = new System.Drawing.Point(29, 22);
			this.txt_BoardNo.LookAndFeel.SkinName = "WXI";
			this.txt_BoardNo.LookAndFeel.UseDefaultLookAndFeel = false;
			this.txt_BoardNo.Name = "txt_BoardNo";
			this.txt_BoardNo.Size = new System.Drawing.Size(288, 32);
			this.txt_BoardNo.smtLabelTitleValue = "공지번호";
			this.txt_BoardNo.smtLabelTitleVisible = true;
			this.txt_BoardNo.smtLabelTitleWidth = 100;
			this.txt_BoardNo.smtTextBoxDecimal = 0;
			this.txt_BoardNo.smtTextBoxMust = false;
			this.txt_BoardNo.smtTextBoxNumber = false;
			this.txt_BoardNo.smtTextBoxReadOnly = true;
			this.txt_BoardNo.smtTextBoxValue = "";
			this.txt_BoardNo.TabIndex = 189;
			// 
			// txt_Subject
			// 
			this.txt_Subject.Location = new System.Drawing.Point(29, 98);
			this.txt_Subject.LookAndFeel.SkinName = "WXI";
			this.txt_Subject.LookAndFeel.UseDefaultLookAndFeel = false;
			this.txt_Subject.Name = "txt_Subject";
			this.txt_Subject.Size = new System.Drawing.Size(832, 32);
			this.txt_Subject.smtLabelTitleValue = "제목";
			this.txt_Subject.smtLabelTitleVisible = true;
			this.txt_Subject.smtLabelTitleWidth = 100;
			this.txt_Subject.smtTextBoxDecimal = 0;
			this.txt_Subject.smtTextBoxMust = true;
			this.txt_Subject.smtTextBoxNumber = false;
			this.txt_Subject.smtTextBoxReadOnly = false;
			this.txt_Subject.smtTextBoxValue = "";
			this.txt_Subject.TabIndex = 190;
			// 
			// StartDt
			// 
			this.StartDt.Location = new System.Drawing.Point(30, 60);
			this.StartDt.LookAndFeel.SkinName = "WXI";
			this.StartDt.LookAndFeel.UseDefaultLookAndFeel = false;
			this.StartDt.Name = "StartDt";
			this.StartDt.Size = new System.Drawing.Size(288, 32);
			this.StartDt.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.StartDt.smtDateTimeMust = true;
			this.StartDt.smtDateTimeReadOnly = false;
			this.StartDt.smtDateTimeValue = new System.DateTime(((long)(0)));
			this.StartDt.smtLabelTitleValue = "시작일자";
			this.StartDt.smtLabelTitleVisible = true;
			this.StartDt.smtLabelTitleWidth = 100;
			this.StartDt.TabIndex = 192;
			// 
			// EndDt
			// 
			this.EndDt.Location = new System.Drawing.Point(380, 60);
			this.EndDt.LookAndFeel.SkinName = "WXI";
			this.EndDt.LookAndFeel.UseDefaultLookAndFeel = false;
			this.EndDt.Name = "EndDt";
			this.EndDt.Size = new System.Drawing.Size(288, 32);
			this.EndDt.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
			this.EndDt.smtDateTimeMust = true;
			this.EndDt.smtDateTimeReadOnly = false;
			this.EndDt.smtDateTimeValue = new System.DateTime(((long)(0)));
			this.EndDt.smtLabelTitleValue = "종료일자";
			this.EndDt.smtLabelTitleVisible = true;
			this.EndDt.smtLabelTitleWidth = 100;
			this.EndDt.TabIndex = 193;
			// 
			// panelControl1
			// 
			this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
			this.panelControl1.Appearance.Options.UseBackColor = true;
			this.panelControl1.Controls.Add(this.WriteDt);
			this.panelControl1.Controls.Add(this.txt_BoardNo);
			this.panelControl1.Controls.Add(this.EndDt);
			this.panelControl1.Controls.Add(this.txt_Subject);
			this.panelControl1.Controls.Add(this.StartDt);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl1.Location = new System.Drawing.Point(0, 40);
			this.panelControl1.LookAndFeel.SkinMaskColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
			this.panelControl1.LookAndFeel.SkinName = "WXI";
			this.panelControl1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(1198, 141);
			this.panelControl1.TabIndex = 195;
			// 
			// fontRibbonPageGroup1
			// 
			this.fontRibbonPageGroup1.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.True;
			this.fontRibbonPageGroup1.Name = "fontRibbonPageGroup1";
			this.fontRibbonPageGroup1.Text = "";
			// 
			// fontRibbonPageGroup2
			// 
			this.fontRibbonPageGroup2.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.True;
			this.fontRibbonPageGroup2.Name = "fontRibbonPageGroup2";
			this.fontRibbonPageGroup2.Text = "";
			// 
			// changeFontNameItem1
			// 
			this.changeFontNameItem1.Edit = this.repositoryItemFontEdit1;
			this.changeFontNameItem1.Id = 23;
			this.changeFontNameItem1.Name = "changeFontNameItem1";
			// 
			// changeFontSizeItem2
			// 
			this.changeFontSizeItem2.Edit = null;
			this.changeFontSizeItem2.Id = 24;
			this.changeFontSizeItem2.Name = "changeFontSizeItem2";
			// 
			// illustrationsBar1
			// 
			this.illustrationsBar1.BarName = "";
			this.illustrationsBar1.Control = null;
			this.illustrationsBar1.DockCol = 0;
			this.illustrationsBar1.DockRow = 1;
			this.illustrationsBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.illustrationsBar1.Text = "";
			// 
			// smtBodyRichEdit1
			// 
			this.smtBodyRichEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.smtBodyRichEdit1.Location = new System.Drawing.Point(0, 181);
			this.smtBodyRichEdit1.LookAndFeel.SkinName = "WXI";
			this.smtBodyRichEdit1.LookAndFeel.UseDefaultLookAndFeel = false;
			this.smtBodyRichEdit1.Name = "smtBodyRichEdit1";
			this.smtBodyRichEdit1.Size = new System.Drawing.Size(1198, 671);
			this.smtBodyRichEdit1.smtRichEditFontVisible = true;
			this.smtBodyRichEdit1.smtRichEditReadOnly = false;
			this.smtBodyRichEdit1.smtRichEditRtfText = resources.GetString("smtBodyRichEdit1.smtRichEditRtfText");
			this.smtBodyRichEdit1.smtRichEditText = "";
			this.smtBodyRichEdit1.TabIndex = 196;
			// 
			// FrmNotice_RND
			// 
			this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
			this.Appearance.Options.UseBackColor = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1198, 852);
			this.Controls.Add(this.smtBodyRichEdit1);
			this.Controls.Add(this.panelControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("FrmNotice_RND.IconOptions.Image")));
			this.LookAndFeel.SkinName = "WXI";
			this.LookAndFeel.UseDefaultLookAndFeel = false;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "FrmNotice_RND";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraBars.Navigation.NavButton cmdSave;
		private DevExpress.XtraBars.Navigation.NavButton navButton1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton btn_Save;
		private baseSMT.smtSearchButton btn_Close;
		private baseSMT.smtBodyDatetime WriteDt;
		private baseSMT.smtBodyTextBox txt_BoardNo;
		private baseSMT.smtBodyTextBox txt_Subject;
		private baseSMT.smtBodyDatetime StartDt;
		private baseSMT.smtBodyDatetime EndDt;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraRichEdit.UI.FontRibbonPageGroup fontRibbonPageGroup1;
		private DevExpress.XtraRichEdit.UI.FontRibbonPageGroup fontRibbonPageGroup2;
		private DevExpress.XtraRichEdit.UI.ChangeFontNameItem changeFontNameItem1;
		private DevExpress.XtraRichEdit.UI.ChangeFontSizeItem changeFontSizeItem2;
		private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
		private DevExpress.XtraRichEdit.UI.IllustrationsBar illustrationsBar1;
		private baseSMT.smtBodyRichEdit smtBodyRichEdit1;
	}
}