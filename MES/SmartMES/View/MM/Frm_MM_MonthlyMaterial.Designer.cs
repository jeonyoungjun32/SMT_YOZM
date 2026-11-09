
namespace SmartMES.View.MM
{
    partial class Frm_MM_MonthlyMaterial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_MonthlyMaterial));
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.GroupControlTitle = new DevExpress.XtraEditors.GroupControl();
            this.Gc_MonthlyMaterial = new DevExpress.XtraGrid.GridControl();
            this.Gv_MonthlyMaterial = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdSave = new baseSMT.smtSearchButton();
            this.btn_Excel = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.ssCM_Item = new baseSMT.smtSearchLookUp();
            this.smtHouseCode = new baseSMT.smtSearchLookUp();
            this.panel3 = new System.Windows.Forms.Panel();
            this.barEditDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmdClose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControlTitle)).BeginInit();
            this.GroupControlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_MonthlyMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_MonthlyMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barEditDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditDate.Properties.CalendarTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView2
            // 
            this.gridView2.Name = "gridView2";
            // 
            // GroupControlTitle
            // 
            this.GroupControlTitle.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.GroupControlTitle.AppearanceCaption.Options.UseFont = true;
            this.GroupControlTitle.CaptionImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("GroupControlTitle.CaptionImageOptions.Image")));
            this.GroupControlTitle.Controls.Add(this.Gc_MonthlyMaterial);
            this.GroupControlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupControlTitle.Location = new System.Drawing.Point(0, 40);
            this.GroupControlTitle.Name = "GroupControlTitle";
            this.GroupControlTitle.Size = new System.Drawing.Size(1377, 555);
            this.GroupControlTitle.TabIndex = 273;
            this.GroupControlTitle.Text = "자재정보";
            // 
            // Gc_MonthlyMaterial
            // 
            this.Gc_MonthlyMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_MonthlyMaterial.Location = new System.Drawing.Point(2, 29);
            this.Gc_MonthlyMaterial.MainView = this.Gv_MonthlyMaterial;
            this.Gc_MonthlyMaterial.Name = "Gc_MonthlyMaterial";
            this.Gc_MonthlyMaterial.Size = new System.Drawing.Size(1373, 524);
            this.Gc_MonthlyMaterial.TabIndex = 269;
            this.Gc_MonthlyMaterial.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_MonthlyMaterial});
            // 
            // Gv_MonthlyMaterial
            // 
            this.Gv_MonthlyMaterial.DetailHeight = 397;
            this.Gv_MonthlyMaterial.GridControl = this.Gc_MonthlyMaterial;
            this.Gv_MonthlyMaterial.Name = "Gv_MonthlyMaterial";
            this.Gv_MonthlyMaterial.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.True;
            this.Gv_MonthlyMaterial.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
            this.Gv_MonthlyMaterial.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;
            this.Gv_MonthlyMaterial.OptionsSelection.MultiSelect = true;
            this.Gv_MonthlyMaterial.OptionsView.ShowGroupPanel = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdSave);
            this.smtCommonBar1.Controls.Add(this.btn_Excel);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.ssCM_Item);
            this.smtCommonBar1.Controls.Add(this.smtHouseCode);
            this.smtCommonBar1.Controls.Add(this.panel3);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1377, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "자재일보";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 274;
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
            this.cmdSave.Location = new System.Drawing.Point(1122, 0);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdSave.Size = new System.Drawing.Size(77, 40);
            this.cmdSave.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "저장";
            this.cmdSave.Visible = false;
            // 
            // btn_Excel
            // 
            this.btn_Excel.AllowFocus = false;
            this.btn_Excel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Excel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Excel.Appearance.Options.UseBackColor = true;
            this.btn_Excel.Appearance.Options.UseForeColor = true;
            this.btn_Excel.AutoSize = true;
            this.btn_Excel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Excel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Excel.ImageOptions.ImageIndex = 9;
            this.btn_Excel.ImageOptions.ImageToTextIndent = 15;
            this.btn_Excel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Excel.Location = new System.Drawing.Point(1199, 0);
            this.btn_Excel.Name = "btn_Excel";
            this.btn_Excel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Excel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Excel.Size = new System.Drawing.Size(101, 40);
            this.btn_Excel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.btn_Excel.TabIndex = 333;
            this.btn_Excel.Text = "엑셀변환";
            // 
            // btn_Search
            // 
            this.btn_Search.AllowFocus = false;
            this.btn_Search.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Search.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Search.Appearance.Options.UseBackColor = true;
            this.btn_Search.Appearance.Options.UseForeColor = true;
            this.btn_Search.AutoSize = true;
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Search.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Search.ImageOptions.ImageIndex = 0;
            this.btn_Search.ImageOptions.ImageToTextIndent = 15;
            this.btn_Search.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Search.Location = new System.Drawing.Point(820, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 331;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // ssCM_Item
            // 
            this.ssCM_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.ssCM_Item.Location = new System.Drawing.Point(532, 0);
            this.ssCM_Item.LookAndFeel.SkinName = "WXI";
            this.ssCM_Item.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ssCM_Item.Name = "ssCM_Item";
            this.ssCM_Item.Size = new System.Drawing.Size(288, 40);
            this.ssCM_Item.smtLabelTitleValue = "품목";
            this.ssCM_Item.smtLabelTitleVisible = true;
            this.ssCM_Item.smtLabelTitleWidth = 100;
            this.ssCM_Item.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.ssCM_Item.smtLookUpDataDisplayMember = "";
            this.ssCM_Item.smtLookUpDataSource = null;
            this.ssCM_Item.smtLookUpDataValueMember = "";
            this.ssCM_Item.smtLookUpSelectdIndex = -1;
            this.ssCM_Item.smtLookUpText = "";
            this.ssCM_Item.smtLookUpValue = "";
            this.ssCM_Item.TabIndex = 328;
            // 
            // smtHouseCode
            // 
            this.smtHouseCode.Dock = System.Windows.Forms.DockStyle.Left;
            this.smtHouseCode.Location = new System.Drawing.Point(267, 0);
            this.smtHouseCode.LookAndFeel.SkinName = "WXI";
            this.smtHouseCode.LookAndFeel.UseDefaultLookAndFeel = false;
            this.smtHouseCode.Name = "smtHouseCode";
            this.smtHouseCode.Size = new System.Drawing.Size(265, 40);
            this.smtHouseCode.smtLabelTitleValue = "창고";
            this.smtHouseCode.smtLabelTitleVisible = true;
            this.smtHouseCode.smtLabelTitleWidth = 50;
            this.smtHouseCode.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.smtHouseCode.smtLookUpDataDisplayMember = "코드명";
            this.smtHouseCode.smtLookUpDataSource = null;
            this.smtHouseCode.smtLookUpDataValueMember = "코드";
            this.smtHouseCode.smtLookUpSelectdIndex = -1;
            this.smtHouseCode.smtLookUpText = "";
            this.smtHouseCode.smtLookUpValue = "";
            this.smtHouseCode.TabIndex = 329;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.barEditDate);
            this.panel3.Controls.Add(this.labelControl2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(92, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(175, 40);
            this.panel3.TabIndex = 332;
            // 
            // barEditDate
            // 
            this.barEditDate.EditValue = null;
            this.barEditDate.Location = new System.Drawing.Point(65, 5);
            this.barEditDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barEditDate.Name = "barEditDate";
            this.barEditDate.Properties.AutoHeight = false;
            this.barEditDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditDate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.barEditDate.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.barEditDate.Size = new System.Drawing.Size(103, 31);
            this.barEditDate.TabIndex = 39;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(6, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 24);
            this.labelControl2.TabIndex = 26;
            this.labelControl2.Text = "기준일자";
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
            this.cmdClose.Location = new System.Drawing.Point(1300, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Frm_MT_MonthlyMaterial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 595);
            this.Controls.Add(this.GroupControlTitle);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_MT_MonthlyMaterial";
            this.Text = "자재일보";
            this.Load += new System.EventHandler(this.Frm_MT_MatOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GroupControlTitle)).EndInit();
            this.GroupControlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_MonthlyMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_MonthlyMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barEditDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.GroupControl GroupControlTitle;
        private DevExpress.XtraGrid.GridControl Gc_MonthlyMaterial;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_MonthlyMaterial;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchLookUp ssCM_Item;
        private baseSMT.smtSearchLookUp smtHouseCode;
        private baseSMT.smtSearchButton cmdSave;
        private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchButton btn_Search;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.DateEdit barEditDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private baseSMT.smtSearchButton btn_Excel;
    }
}