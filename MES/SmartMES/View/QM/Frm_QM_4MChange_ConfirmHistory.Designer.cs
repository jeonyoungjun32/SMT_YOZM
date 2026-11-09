
namespace SmartMES.View.QM
{
	partial class Frm_QM_4MChange_ConfirmHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_QM_4MChange_ConfirmHistory));
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Excel = new baseSMT.smtSearchButton();
            this.btn_Save = new baseSMT.smtSearchButton();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Confirm = new baseSMT.smtSearchLookUp();
            this.Lup_Item = new baseSMT.smtSearchLookUp();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_List = new DevExpress.XtraGrid.GridControl();
            this.Gv_List = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.Pnl_Proc = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Gc_Detail = new DevExpress.XtraGrid.GridControl();
            this.Gv_Detail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Pnl_Proc1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_Proc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_Proc1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Excel);
            this.smtCommonBar1.Controls.Add(this.btn_Save);
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Confirm);
            this.smtCommonBar1.Controls.Add(this.Lup_Item);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1389, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "4M 변경이력";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 285;
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
            this.btn_Excel.Location = new System.Drawing.Point(1134, 0);
            this.btn_Excel.Name = "btn_Excel";
            this.btn_Excel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Excel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Excel.Size = new System.Drawing.Size(101, 40);
            this.btn_Excel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.btn_Excel.TabIndex = 16;
            this.btn_Excel.Text = "엑셀변환";
            this.btn_Excel.Click += new System.EventHandler(this.btn_Excel_Click);
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
            this.btn_Save.Location = new System.Drawing.Point(1235, 0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Save.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Save.Size = new System.Drawing.Size(77, 40);
            this.btn_Save.smtButtonStyle = baseSMT.EnumButtonStyles.저장;
            this.btn_Save.TabIndex = 15;
            this.btn_Save.Text = "저장";
            this.btn_Save.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.btn_Search.Location = new System.Drawing.Point(692, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 6;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // Lup_Confirm
            // 
            this.Lup_Confirm.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Confirm.Location = new System.Drawing.Point(404, 0);
            this.Lup_Confirm.LookAndFeel.SkinName = "WXI";
            this.Lup_Confirm.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Confirm.Name = "Lup_Confirm";
            this.Lup_Confirm.Size = new System.Drawing.Size(288, 40);
            this.Lup_Confirm.smtLabelForeColor = System.Drawing.Color.White;
            this.Lup_Confirm.smtLabelTitleValue = "승인여부";
            this.Lup_Confirm.smtLabelTitleVisible = true;
            this.Lup_Confirm.smtLabelTitleWidth = 100;
            this.Lup_Confirm.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Confirm.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Confirm.smtLookUpDataSource = null;
            this.Lup_Confirm.smtLookUpDataValueMember = "코드";
            this.Lup_Confirm.smtLookUpSelectdIndex = -1;
            this.Lup_Confirm.smtLookUpText = "";
            this.Lup_Confirm.smtLookUpValue = "";
            this.Lup_Confirm.TabIndex = 14;
            // 
            // Lup_Item
            // 
            this.Lup_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Item.Location = new System.Drawing.Point(116, 0);
            this.Lup_Item.LookAndFeel.SkinName = "WXI";
            this.Lup_Item.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Item.Name = "Lup_Item";
            this.Lup_Item.Size = new System.Drawing.Size(288, 40);
            this.Lup_Item.smtLabelForeColor = System.Drawing.Color.White;
            this.Lup_Item.smtLabelTitleValue = "품목";
            this.Lup_Item.smtLabelTitleVisible = true;
            this.Lup_Item.smtLabelTitleWidth = 100;
            this.Lup_Item.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Item.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Item.smtLookUpDataSource = null;
            this.Lup_Item.smtLookUpDataValueMember = "코드";
            this.Lup_Item.smtLookUpSelectdIndex = -1;
            this.Lup_Item.smtLookUpText = "";
            this.Lup_Item.smtLookUpValue = "";
            this.Lup_Item.TabIndex = 9;
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
            this.btn_Close.Location = new System.Drawing.Point(1312, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(77, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Gc_List);
            this.layoutControl1.Controls.Add(this.Gc_Detail);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(601, 439, 437, 485);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1389, 649);
            this.layoutControl1.TabIndex = 286;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Gc_List
            // 
            this.Gc_List.Location = new System.Drawing.Point(7, 34);
            this.Gc_List.MainView = this.Gv_List;
            this.Gc_List.Name = "Gc_List";
            this.Gc_List.Size = new System.Drawing.Size(1375, 295);
            this.Gc_List.TabIndex = 1;
            this.Gc_List.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_List});
            // 
            // Gv_List
            // 
            this.Gv_List.DetailHeight = 397;
            this.Gv_List.GridControl = this.Gc_List;
            this.Gv_List.Name = "Gv_List";
            this.Gv_List.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_List.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.splitterItem2,
            this.Pnl_Proc,
            this.Pnl_Proc1});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1389, 649);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new System.Drawing.Point(0, 336);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(1389, 16);
            // 
            // Pnl_Proc
            // 
            this.Pnl_Proc.CustomizationFormText = "4M 내역";
            this.Pnl_Proc.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.Pnl_Proc.Location = new System.Drawing.Point(0, 0);
            this.Pnl_Proc.Name = "Pnl_Proc";
            this.Pnl_Proc.OptionsItemText.TextToControlDistance = 6;
            this.Pnl_Proc.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Pnl_Proc.Size = new System.Drawing.Size(1389, 336);
            this.Pnl_Proc.Text = "4M내역";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.Gc_List;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1381, 301);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // Gc_Detail
            // 
            this.Gc_Detail.Location = new System.Drawing.Point(7, 386);
            this.Gc_Detail.MainView = this.Gv_Detail;
            this.Gc_Detail.Name = "Gc_Detail";
            this.Gc_Detail.Size = new System.Drawing.Size(1375, 256);
            this.Gc_Detail.TabIndex = 1;
            this.Gc_Detail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Detail});
            // 
            // Gv_Detail
            // 
            this.Gv_Detail.DetailHeight = 397;
            this.Gv_Detail.GridControl = this.Gc_Detail;
            this.Gv_Detail.Name = "Gv_Detail";
            this.Gv_Detail.OptionsEditForm.PopupEditFormWidth = 914;
            this.Gv_Detail.OptionsView.ShowGroupPanel = false;
            // 
            // Pnl_Proc1
            // 
            this.Pnl_Proc1.CustomizationFormText = "검사내역";
            this.Pnl_Proc1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Pnl_Proc1.Location = new System.Drawing.Point(0, 352);
            this.Pnl_Proc1.Name = "Pnl_Proc1";
            this.Pnl_Proc1.OptionsItemText.TextToControlDistance = 6;
            this.Pnl_Proc1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Pnl_Proc1.Size = new System.Drawing.Size(1389, 297);
            this.Pnl_Proc1.Text = "변경이력";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Gc_Detail;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1381, 262);
            this.layoutControlItem1.Text = "layoutControlItem2";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Frm_QM_4MChange_ConfirmHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 689);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_QM_4MChange_ConfirmHistory";
            this.Text = "4M 변경이력";
            this.Load += new System.EventHandler(this.Frm_PD_4M_Approve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_Proc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pnl_Proc1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

		}

        #endregion

        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchLookUp Lup_Item;
        private baseSMT.smtSearchButton btn_Close;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private baseSMT.smtSearchLookUp Lup_Confirm;
        private baseSMT.smtSearchButton btn_Excel;
        private baseSMT.smtSearchButton btn_Save;
        private DevExpress.XtraGrid.GridControl Gc_List;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_List;
        private DevExpress.XtraLayout.LayoutControlGroup Pnl_Proc;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl Gc_Detail;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Detail;
        private DevExpress.XtraLayout.LayoutControlGroup Pnl_Proc1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}