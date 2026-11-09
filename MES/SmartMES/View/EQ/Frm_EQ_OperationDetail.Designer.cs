
namespace SmartMES.View.EQ
{
    partial class Frm_EQ_OperationDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EQ_OperationDetail));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Save = new baseSMT.smtSearchButton();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.Lup_ItemCode = new baseSMT.smtBodyLookUp();
            this.Lup_MachineName = new baseSMT.smtBodyLookUp();
            this.Txt_MachineCode = new baseSMT.smtBodyTextBox();
            this.Dt_StartDate = new baseSMT.smtBodyDatetime();
            this.Dt_EndDate = new baseSMT.smtBodyDatetime();
            this.Dt_WorkDay = new baseSMT.smtBodyDatetime();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
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
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(711, 45);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "설비비가동등록";
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
            this.btn_Save.Location = new System.Drawing.Point(553, 0);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Padding = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.btn_Save.Size = new System.Drawing.Size(79, 45);
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
            this.btn_Close.Location = new System.Drawing.Point(632, 0);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(11, 0, 11, 0);
            this.btn_Close.Size = new System.Drawing.Size(79, 45);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.Lup_ItemCode);
            this.panelControl1.Controls.Add(this.Lup_MachineName);
            this.panelControl1.Controls.Add(this.Txt_MachineCode);
            this.panelControl1.Controls.Add(this.Dt_StartDate);
            this.panelControl1.Controls.Add(this.Dt_EndDate);
            this.panelControl1.Controls.Add(this.Dt_WorkDay);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 45);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(711, 170);
            this.panelControl1.TabIndex = 1;
            // 
            // Lup_ItemCode
            // 
            this.Lup_ItemCode.Location = new System.Drawing.Point(350, 80);
            this.Lup_ItemCode.LookAndFeel.SkinName = "WXI";
            this.Lup_ItemCode.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_ItemCode.Name = "Lup_ItemCode";
            this.Lup_ItemCode.Size = new System.Drawing.Size(330, 32);
            this.Lup_ItemCode.smtLabelTitleValue = "비가동사유";
            this.Lup_ItemCode.smtLabelTitleVisible = true;
            this.Lup_ItemCode.smtLabelTitleWidth = 100;
            this.Lup_ItemCode.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_ItemCode.smtLookUpDataDisplayMember = "코드명";
            this.Lup_ItemCode.smtLookUpDataSource = null;
            this.Lup_ItemCode.smtLookUpDataValueMember = "코드";
            this.Lup_ItemCode.smtLookUpMust = false;
            this.Lup_ItemCode.smtLookUpReadOnly = false;
            this.Lup_ItemCode.smtLookUpSelectdIndex = -1;
            this.Lup_ItemCode.smtLookUpText = "";
            this.Lup_ItemCode.smtLookUpValue = "";
            this.Lup_ItemCode.TabIndex = 129;
            // 
            // Lup_MachineName
            // 
            this.Lup_MachineName.Location = new System.Drawing.Point(352, 17);
            this.Lup_MachineName.LookAndFeel.SkinName = "WXI";
            this.Lup_MachineName.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_MachineName.Name = "Lup_MachineName";
            this.Lup_MachineName.Size = new System.Drawing.Size(328, 32);
            this.Lup_MachineName.smtLabelTitleValue = "설비명";
            this.Lup_MachineName.smtLabelTitleVisible = true;
            this.Lup_MachineName.smtLabelTitleWidth = 100;
            this.Lup_MachineName.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_MachineName.smtLookUpDataDisplayMember = "코드명";
            this.Lup_MachineName.smtLookUpDataSource = null;
            this.Lup_MachineName.smtLookUpDataValueMember = "코드";
            this.Lup_MachineName.smtLookUpMust = true;
            this.Lup_MachineName.smtLookUpReadOnly = false;
            this.Lup_MachineName.smtLookUpSelectdIndex = -1;
            this.Lup_MachineName.smtLookUpText = "";
            this.Lup_MachineName.smtLookUpValue = "";
            this.Lup_MachineName.TabIndex = 128;
            this.Lup_MachineName.smtLookUpEditValueChanged += new System.EventHandler(this.Lup_MachineName_smtLookUpEditValueChanged);
            // 
            // Txt_MachineCode
            // 
            this.Txt_MachineCode.Location = new System.Drawing.Point(14, 17);
            this.Txt_MachineCode.LookAndFeel.SkinName = "WXI";
            this.Txt_MachineCode.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Txt_MachineCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Txt_MachineCode.Name = "Txt_MachineCode";
            this.Txt_MachineCode.Size = new System.Drawing.Size(330, 32);
            this.Txt_MachineCode.smtLabelTitleValue = "설비코드";
            this.Txt_MachineCode.smtLabelTitleVisible = true;
            this.Txt_MachineCode.smtLabelTitleWidth = 100;
            this.Txt_MachineCode.smtTextBoxDecimal = 0;
            this.Txt_MachineCode.smtTextBoxMust = false;
            this.Txt_MachineCode.smtTextBoxNumber = false;
            this.Txt_MachineCode.smtTextBoxReadOnly = true;
            this.Txt_MachineCode.smtTextBoxValue = "";
            this.Txt_MachineCode.TabIndex = 127;
            // 
            // Dt_StartDate
            // 
            this.Dt_StartDate.Location = new System.Drawing.Point(14, 120);
            this.Dt_StartDate.LookAndFeel.SkinName = "WXI";
            this.Dt_StartDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_StartDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Dt_StartDate.Name = "Dt_StartDate";
            this.Dt_StartDate.Size = new System.Drawing.Size(330, 32);
            this.Dt_StartDate.smtDateTimeEditingTime = baseSMT.DatetimeType.Time;
            this.Dt_StartDate.smtDateTimeMust = true;
            this.Dt_StartDate.smtDateTimeReadOnly = false;
            this.Dt_StartDate.smtDateTimeValue = new System.DateTime(((long)(0)));
            this.Dt_StartDate.smtLabelTitleValue = "시작시간";
            this.Dt_StartDate.smtLabelTitleVisible = true;
            this.Dt_StartDate.smtLabelTitleWidth = 100;
            this.Dt_StartDate.TabIndex = 118;
            // 
            // Dt_EndDate
            // 
            this.Dt_EndDate.Location = new System.Drawing.Point(350, 119);
            this.Dt_EndDate.LookAndFeel.SkinName = "WXI";
            this.Dt_EndDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_EndDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Dt_EndDate.Name = "Dt_EndDate";
            this.Dt_EndDate.Size = new System.Drawing.Size(330, 32);
            this.Dt_EndDate.smtDateTimeEditingTime = baseSMT.DatetimeType.Time;
            this.Dt_EndDate.smtDateTimeMust = true;
            this.Dt_EndDate.smtDateTimeReadOnly = false;
            this.Dt_EndDate.smtDateTimeValue = new System.DateTime(((long)(0)));
            this.Dt_EndDate.smtLabelTitleValue = "종료시간";
            this.Dt_EndDate.smtLabelTitleVisible = true;
            this.Dt_EndDate.smtLabelTitleWidth = 100;
            this.Dt_EndDate.TabIndex = 117;
            // 
            // Dt_WorkDay
            // 
            this.Dt_WorkDay.Location = new System.Drawing.Point(14, 80);
            this.Dt_WorkDay.LookAndFeel.SkinName = "WXI";
            this.Dt_WorkDay.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_WorkDay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Dt_WorkDay.Name = "Dt_WorkDay";
            this.Dt_WorkDay.Size = new System.Drawing.Size(330, 32);
            this.Dt_WorkDay.smtDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_WorkDay.smtDateTimeMust = true;
            this.Dt_WorkDay.smtDateTimeReadOnly = false;
            this.Dt_WorkDay.smtDateTimeValue = new System.DateTime(((long)(0)));
            this.Dt_WorkDay.smtLabelTitleValue = "비가동일자";
            this.Dt_WorkDay.smtLabelTitleVisible = true;
            this.Dt_WorkDay.smtLabelTitleWidth = 100;
            this.Dt_WorkDay.TabIndex = 113;
            // 
            // frmEQ_OperationDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 215);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_EQ_OperationDetail";
            this.Text = "설비비가동등록";
            this.Load += new System.EventHandler(this.Frm_EQ_OperationDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Save;
        private baseSMT.smtSearchButton btn_Close;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private baseSMT.smtBodyDatetime Dt_WorkDay;
        private baseSMT.smtBodyDatetime Dt_StartDate;
        private baseSMT.smtBodyDatetime Dt_EndDate;
        private baseSMT.smtBodyLookUp Lup_ItemCode;
        private baseSMT.smtBodyLookUp Lup_MachineName;
        private baseSMT.smtBodyTextBox Txt_MachineCode;
    }
}