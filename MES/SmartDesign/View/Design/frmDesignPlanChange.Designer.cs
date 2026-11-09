
namespace SmartDesign.View.Design
{
    partial class frmDesignPlanChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDesignPlanChange));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.smtBodyDatetime1 = new baseSMT.smtBodyDatetime();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Save = new baseSMT.smtSearchButton();
            this.btn_Close = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.smtBodyDatetime1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 40);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(486, 131);
            this.panelControl1.TabIndex = 1;
            // 
            // smtBodyDatetime1
            // 
            this.smtBodyDatetime1.Location = new System.Drawing.Point(36, 46);
            this.smtBodyDatetime1.LookAndFeel.SkinName = "WXI";
            this.smtBodyDatetime1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtBodyDatetime1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.smtBodyDatetime1.Name = "smtBodyDatetime1";
            this.smtBodyDatetime1.Size = new System.Drawing.Size(411, 32);
            this.smtBodyDatetime1.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.smtBodyDatetime1.smtDateTimeMust = false;
            this.smtBodyDatetime1.smtDateTimeReadOnly = false;
            this.smtBodyDatetime1.smtDateTimeValue = new System.DateTime(((long)(0)));
            this.smtBodyDatetime1.smtLabelTitleValue = "목표일자";
            this.smtBodyDatetime1.smtLabelTitleVisible = true;
            this.smtBodyDatetime1.smtLabelTitleWidth = 100;
            this.smtBodyDatetime1.TabIndex = 0;
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
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(486, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "목표일자 변경";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 0;
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
            this.btn_Save.Location = new System.Drawing.Point(282, 0);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.btn_Save.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Save.Size = new System.Drawing.Size(102, 40);
            this.btn_Save.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "저장";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
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
            this.btn_Close.Location = new System.Drawing.Point(384, 0);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(102, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmDesignPlanChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 171);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmDesignPlanChange";
            this.Text = "목표일자 변경";
            this.Load += new System.EventHandler(this.frmDesignPlanChange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Save;
        private baseSMT.smtSearchButton btn_Close;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private baseSMT.smtBodyDatetime smtBodyDatetime1;
    }
}