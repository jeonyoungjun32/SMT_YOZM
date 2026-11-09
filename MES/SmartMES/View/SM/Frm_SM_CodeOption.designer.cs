
namespace SmartMES.View.SM
{
	partial class Frm_SM_CodeOption
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_SM_CodeOption));
			this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
			this.Gc_Switch = new DevExpress.XtraGrid.GridControl();
			this.Gv_Switch = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
			this.Lup_TaskDiv = new DevExpress.XtraEditors.LookUpEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.smtSearchButton1 = new baseSMT.smtSearchButton();
			this.Btn_Down = new baseSMT.smtSearchButton();
			this.Btn_Up = new baseSMT.smtSearchButton();
			this.cmdDisplay = new baseSMT.smtSearchButton();
			this.cmdSave = new baseSMT.smtSearchButton();
			this.cmdClose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
			this.groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Gc_Switch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Switch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
			this.panelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Lup_TaskDiv.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupControl2
			// 
			this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
			this.groupControl2.AppearanceCaption.Options.UseFont = true;
			this.groupControl2.Controls.Add(this.Gc_Switch);
			this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupControl2.Location = new System.Drawing.Point(0, 40);
			this.groupControl2.Name = "groupControl2";
			this.groupControl2.Size = new System.Drawing.Size(1043, 469);
			this.groupControl2.TabIndex = 272;
			this.groupControl2.Text = "품목정보";
			// 
			// Gc_Switch
			// 
			this.Gc_Switch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Gc_Switch.Location = new System.Drawing.Point(2, 29);
			this.Gc_Switch.MainView = this.Gv_Switch;
			this.Gc_Switch.Name = "Gc_Switch";
			this.Gc_Switch.Size = new System.Drawing.Size(1039, 438);
			this.Gc_Switch.TabIndex = 1;
			this.Gc_Switch.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
			this.Gv_Switch});
			// 
			// Gv_Switch
			// 
			this.Gv_Switch.DetailHeight = 397;
			this.Gv_Switch.GridControl = this.Gc_Switch;
			this.Gv_Switch.Name = "Gv_Switch";
			this.Gv_Switch.OptionsView.ShowGroupPanel = false;
			// 
			// panelControl3
			// 
			this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl3.Controls.Add(this.Lup_TaskDiv);
			this.panelControl3.Controls.Add(this.labelControl1);
			this.panelControl3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelControl3.Location = new System.Drawing.Point(119, 0);
			this.panelControl3.Name = "panelControl3";
			this.panelControl3.Padding = new System.Windows.Forms.Padding(3);
			this.panelControl3.Size = new System.Drawing.Size(238, 40);
			this.panelControl3.TabIndex = 279;
			// 
			// Lup_TaskDiv
			// 
			this.Lup_TaskDiv.Location = new System.Drawing.Point(83, 7);
			this.Lup_TaskDiv.Name = "Lup_TaskDiv";
			this.Lup_TaskDiv.Properties.AutoHeight = false;
			this.Lup_TaskDiv.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
			new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.Lup_TaskDiv.Size = new System.Drawing.Size(149, 26);
			this.Lup_TaskDiv.TabIndex = 282;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
			this.labelControl1.Appearance.Options.UseForeColor = true;
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl1.Location = new System.Drawing.Point(10, 7);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(114, 26);
			this.labelControl1.TabIndex = 60;
			this.labelControl1.Text = "코드구분";
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
			this.smtCommonBar1.Controls.Add(this.Btn_Down);
			this.smtCommonBar1.Controls.Add(this.Btn_Up);
			this.smtCommonBar1.Controls.Add(this.cmdDisplay);
			this.smtCommonBar1.Controls.Add(this.panelControl3);
			this.smtCommonBar1.Controls.Add(this.cmdSave);
			this.smtCommonBar1.Controls.Add(this.cmdClose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1043, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "Frm_Switch";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 273;
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
			this.smtSearchButton1.ImageOptions.ImageIndex = 4;
			this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
			this.smtSearchButton1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtSearchButton1.ImageOptions.SvgImage")));
			this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.smtSearchButton1.Location = new System.Drawing.Point(644, 0);
			this.smtSearchButton1.Name = "smtSearchButton1";
			this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.smtSearchButton1.Size = new System.Drawing.Size(140, 40);
			this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.smtSearchButton1.TabIndex = 290;
			this.smtSearchButton1.Text = "표시순서 재채번";
			this.smtSearchButton1.Click += new System.EventHandler(this.Btn_Set_DispSeq_Click);
			// 
			// Btn_Down
			// 
			this.Btn_Down.AllowFocus = false;
			this.Btn_Down.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.Btn_Down.Appearance.ForeColor = System.Drawing.Color.White;
			this.Btn_Down.Appearance.Options.UseBackColor = true;
			this.Btn_Down.Appearance.Options.UseForeColor = true;
			this.Btn_Down.AutoSize = true;
			this.Btn_Down.Dock = System.Windows.Forms.DockStyle.Right;
			this.Btn_Down.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.Btn_Down.ImageOptions.ImageIndex = 2;
			this.Btn_Down.ImageOptions.ImageToTextIndent = 15;
			this.Btn_Down.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Btn_Down.ImageOptions.SvgImage")));
			this.Btn_Down.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.Btn_Down.Location = new System.Drawing.Point(784, 0);
			this.Btn_Down.Name = "Btn_Down";
			this.Btn_Down.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.Btn_Down.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.Btn_Down.Size = new System.Drawing.Size(49, 40);
			this.Btn_Down.smtButtonStyle = baseSMT.EnumButtonStyles.수정;
			this.Btn_Down.TabIndex = 289;
			this.Btn_Down.Click += new System.EventHandler(this.Btn_Click);
			// 
			// Btn_Up
			// 
			this.Btn_Up.AllowFocus = false;
			this.Btn_Up.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.Btn_Up.Appearance.ForeColor = System.Drawing.Color.White;
			this.Btn_Up.Appearance.Options.UseBackColor = true;
			this.Btn_Up.Appearance.Options.UseForeColor = true;
			this.Btn_Up.AutoSize = true;
			this.Btn_Up.Dock = System.Windows.Forms.DockStyle.Right;
			this.Btn_Up.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.Btn_Up.ImageOptions.ImageIndex = 4;
			this.Btn_Up.ImageOptions.ImageToTextIndent = 15;
			this.Btn_Up.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("Btn_Up.ImageOptions.SvgImage")));
			this.Btn_Up.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.Btn_Up.Location = new System.Drawing.Point(833, 0);
			this.Btn_Up.Name = "Btn_Up";
			this.Btn_Up.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.Btn_Up.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.Btn_Up.Size = new System.Drawing.Size(56, 40);
			this.Btn_Up.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.Btn_Up.TabIndex = 288;
			this.Btn_Up.Text = " ";
			this.Btn_Up.Click += new System.EventHandler(this.Btn_Click);
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
			this.cmdDisplay.Location = new System.Drawing.Point(357, 0);
			this.cmdDisplay.Name = "cmdDisplay";
			this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
			this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.cmdDisplay.TabIndex = 5;
			this.cmdDisplay.Text = "조회";
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
			this.cmdSave.Location = new System.Drawing.Point(889, 0);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdSave.Size = new System.Drawing.Size(77, 40);
			this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
			this.cmdSave.TabIndex = 3;
			this.cmdSave.Text = "저장";
			this.cmdSave.Click += new System.EventHandler(this.Btn_Save_Click);
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
			this.cmdClose.Location = new System.Drawing.Point(966, 0);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdClose.Size = new System.Drawing.Size(77, 40);
			this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.cmdClose.TabIndex = 1;
			this.cmdClose.Text = "닫기";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// Frm_SM_CodeOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1043, 509);
			this.Controls.Add(this.groupControl2);
			this.Controls.Add(this.smtCommonBar1);
			this.Name = "Frm_SM_CodeOption";
			this.Text = "Frm_Switch";
			((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
			this.groupControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Gc_Switch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_Switch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
			this.panelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Lup_TaskDiv.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraEditors.GroupControl groupControl2;
		private DevExpress.XtraGrid.GridControl Gc_Switch;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Switch;
		private DevExpress.XtraEditors.PanelControl panelControl3;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.LookUpEdit Lup_TaskDiv;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton Btn_Down;
		private baseSMT.smtSearchButton Btn_Up;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdSave;
		private baseSMT.smtSearchButton cmdClose;
		private baseSMT.smtSearchButton smtSearchButton1;
	}
}