
namespace SmartMES.View.EQ
{
    partial class Frm_EQ_Operation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EQ_Operation));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.Dt_RepairDate = new baseSMT.smtSearchDatetime();
            this.btn_Ins = new baseSMT.smtSearchButton();
            this.btn_Upd = new baseSMT.smtSearchButton();
            this.btn_Del = new baseSMT.smtSearchButton();
            this.Btn_Close = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_Machine = new DevExpress.XtraGrid.GridControl();
            this.Gv_Machine = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Machine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Machine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.Dt_RepairDate);
            this.smtCommonBar1.Controls.Add(this.btn_Ins);
            this.smtCommonBar1.Controls.Add(this.btn_Upd);
            this.smtCommonBar1.Controls.Add(this.btn_Del);
            this.smtCommonBar1.Controls.Add(this.Btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1219, 45);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "설비비가동등록";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 0;
            // 
            // smtSearchButton1
            // 
            this.smtSearchButton1.AllowFocus = false;
            this.smtSearchButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton1.Appearance.Options.UseBackColor = true;
            this.smtSearchButton1.Appearance.Options.UseForeColor = true;
            this.smtSearchButton1.AutoSize = true;
            this.smtSearchButton1.Dock = System.Windows.Forms.DockStyle.Left;
            this.smtSearchButton1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton1.ImageOptions.ImageIndex = 0;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(362, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 45);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.smtSearchButton1.TabIndex = 7;
            this.smtSearchButton1.Text = "조회";
            this.smtSearchButton1.Click += new System.EventHandler(this.smtSearchButton1_Click);
            // 
            // Dt_RepairDate
            // 
            this.Dt_RepairDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_RepairDate.Location = new System.Drawing.Point(142, 0);
            this.Dt_RepairDate.LookAndFeel.SkinName = "WXI";
            this.Dt_RepairDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_RepairDate.Name = "Dt_RepairDate";
            this.Dt_RepairDate.Size = new System.Drawing.Size(220, 45);
            this.Dt_RepairDate.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Dt_RepairDate.smtComboTitleItems = new string[0];
            this.Dt_RepairDate.smtComboTitleValue = "";
            this.Dt_RepairDate.smtComboTitleVisible = false;
            this.Dt_RepairDate.smtComboTitleWidth = 100;
            this.Dt_RepairDate.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_RepairDate.smtEndDateTimeValue = new System.DateTime(2025, 2, 24, 18, 33, 14, 371);
            this.Dt_RepairDate.smtEndDateTimeVisible = false;
            this.Dt_RepairDate.smtLabelTitleValue = "비가동일자";
            this.Dt_RepairDate.smtLabelTitleVisible = true;
            this.Dt_RepairDate.smtLabelTitleWidth = 100;
            this.Dt_RepairDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_RepairDate.smtStartDateTimeValue = new System.DateTime(2025, 2, 24, 18, 33, 14, 370);
            this.Dt_RepairDate.smtStartDateTimeVisible = true;
            this.Dt_RepairDate.TabIndex = 6;
            // 
            // btn_Ins
            // 
            this.btn_Ins.AllowFocus = false;
            this.btn_Ins.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Ins.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Ins.Appearance.Options.UseBackColor = true;
            this.btn_Ins.Appearance.Options.UseForeColor = true;
            this.btn_Ins.AutoSize = true;
            this.btn_Ins.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Ins.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Ins.ImageOptions.ImageIndex = 1;
            this.btn_Ins.ImageOptions.ImageToTextIndent = 15;
            this.btn_Ins.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Ins.Location = new System.Drawing.Point(911, 0);
            this.btn_Ins.Name = "btn_Ins";
            this.btn_Ins.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Ins.Size = new System.Drawing.Size(77, 45);
            this.btn_Ins.smtButtonStyle = baseSMT.EnumButtonStyles.등록;
            this.btn_Ins.TabIndex = 2;
            this.btn_Ins.Text = "등록";
            this.btn_Ins.Click += new System.EventHandler(this.btn_Ins_Click);
            // 
            // btn_Upd
            // 
            this.btn_Upd.AllowFocus = false;
            this.btn_Upd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Upd.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Upd.Appearance.Options.UseBackColor = true;
            this.btn_Upd.Appearance.Options.UseForeColor = true;
            this.btn_Upd.AutoSize = true;
            this.btn_Upd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Upd.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Upd.ImageOptions.ImageIndex = 2;
            this.btn_Upd.ImageOptions.ImageToTextIndent = 15;
            this.btn_Upd.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Upd.Location = new System.Drawing.Point(988, 0);
            this.btn_Upd.Name = "btn_Upd";
            this.btn_Upd.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Upd.Size = new System.Drawing.Size(77, 45);
            this.btn_Upd.smtButtonStyle = baseSMT.EnumButtonStyles.수정;
            this.btn_Upd.TabIndex = 5;
            this.btn_Upd.Text = "수정";
            this.btn_Upd.Click += new System.EventHandler(this.btn_Upd_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.AllowFocus = false;
            this.btn_Del.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Del.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Del.Appearance.Options.UseBackColor = true;
            this.btn_Del.Appearance.Options.UseForeColor = true;
            this.btn_Del.AutoSize = true;
            this.btn_Del.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Del.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Del.ImageOptions.ImageIndex = 3;
            this.btn_Del.ImageOptions.ImageToTextIndent = 15;
            this.btn_Del.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Del.Location = new System.Drawing.Point(1065, 0);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Del.Size = new System.Drawing.Size(77, 45);
            this.btn_Del.smtButtonStyle = baseSMT.EnumButtonStyles.삭제;
            this.btn_Del.TabIndex = 4;
            this.btn_Del.Text = "삭제";
            this.btn_Del.Click += new System.EventHandler(this.btn_Del_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.AllowFocus = false;
            this.Btn_Close.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.Btn_Close.Appearance.ForeColor = System.Drawing.Color.White;
            this.Btn_Close.Appearance.Options.UseBackColor = true;
            this.Btn_Close.Appearance.Options.UseForeColor = true;
            this.Btn_Close.AutoSize = true;
            this.Btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Close.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.Btn_Close.ImageOptions.ImageIndex = 5;
            this.Btn_Close.ImageOptions.ImageToTextIndent = 15;
            this.Btn_Close.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.Btn_Close.Location = new System.Drawing.Point(1142, 0);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.Btn_Close.Size = new System.Drawing.Size(77, 45);
            this.Btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.Btn_Close.TabIndex = 1;
            this.Btn_Close.Text = "닫기";
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Gc_Machine);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 45);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1162, 350, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1219, 538);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Gc_Machine
            // 
            this.Gc_Machine.Location = new System.Drawing.Point(19, 46);
            this.Gc_Machine.MainView = this.Gv_Machine;
            this.Gc_Machine.Name = "Gc_Machine";
            this.Gc_Machine.Size = new System.Drawing.Size(1181, 473);
            this.Gc_Machine.TabIndex = 4;
            this.Gc_Machine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Machine});
            // 
            // Gv_Machine
            // 
            this.Gv_Machine.GridControl = this.Gc_Machine;
            this.Gv_Machine.Name = "Gv_Machine";
            this.Gv_Machine.OptionsView.ColumnAutoWidth = false;
            this.Gv_Machine.OptionsView.ShowAutoFilterRow = true;
            this.Gv_Machine.OptionsView.ShowGroupPanel = false;
            this.Gv_Machine.DoubleClick += new System.EventHandler(this.Gv_Machine_DoubleClick);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1219, 538);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1219, 538);
            this.layoutControlGroup1.Text = "비가동 리스트";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Gc_Machine;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1187, 479);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmEQ_Operation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1219, 583);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "Frm_EQ_Operation";
            this.Text = "설비비가동등록";
            this.Load += new System.EventHandler(this.Frm_EQ_Operation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Machine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Machine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton Btn_Close;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl Gc_Machine;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Machine;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private baseSMT.smtSearchButton btn_Ins;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private baseSMT.smtSearchButton btn_Upd;
        private baseSMT.smtSearchButton btn_Del;
        private baseSMT.smtSearchDatetime Dt_RepairDate;
        private baseSMT.smtSearchButton smtSearchButton1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
    }
}