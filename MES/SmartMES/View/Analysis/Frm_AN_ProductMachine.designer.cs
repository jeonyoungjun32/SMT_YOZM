
namespace SmartMES.View.Analysis
{
    partial class Frm_AN_ProductMachine
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AN_ProductMachine));
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.chartProduct = new DevExpress.XtraCharts.ChartControl();
			this.Gc_List = new DevExpress.XtraGrid.GridControl();
			this.Gv_List = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			this.cmdExcel = new baseSMT.smtSearchButton();
			this.smtSearchButton1 = new baseSMT.smtSearchButton();
			this.LupProcess = new baseSMT.smtSearchLookUp();
			this.LupLine = new baseSMT.smtSearchLookUp();
			this.barDate = new baseSMT.smtSearchDatetime();
			this.btn_Colose = new baseSMT.smtSearchButton();
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chartProduct)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gc_List)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_List)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.chartProduct);
			this.layoutControl1.Controls.Add(this.Gc_List);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 40);
			this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1240, 565);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// chartProduct
			// 
			this.chartProduct.Location = new System.Drawing.Point(3, 284);
			this.chartProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chartProduct.Name = "chartProduct";
			this.chartProduct.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
			this.chartProduct.Size = new System.Drawing.Size(1234, 277);
			this.chartProduct.TabIndex = 35;
			// 
			// Gc_List
			// 
			this.Gc_List.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Gc_List.Location = new System.Drawing.Point(3, 4);
			this.Gc_List.MainView = this.Gv_List;
			this.Gc_List.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Gc_List.Name = "Gc_List";
			this.Gc_List.Size = new System.Drawing.Size(1234, 272);
			this.Gc_List.TabIndex = 34;
			this.Gc_List.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_List});
			// 
			// Gv_List
			// 
			this.Gv_List.GridControl = this.Gc_List;
			this.Gv_List.Name = "Gv_List";
			this.Gv_List.OptionsEditForm.PopupEditFormWidth = 914;
			this.Gv_List.OptionsView.ShowGroupPanel = false;
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1240, 565);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.Gc_List;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(1240, 280);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.chartProduct;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 280);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(1240, 285);
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
			this.smtCommonBar1.Controls.Add(this.LupLine);
			this.smtCommonBar1.Controls.Add(this.cmdExcel);
			this.smtCommonBar1.Controls.Add(this.LupProcess);
			this.smtCommonBar1.Controls.Add(this.barDate);
			this.smtCommonBar1.Controls.Add(this.btn_Colose);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1240, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "월별 생산량 차트";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 290;
			// 
			// cmdExcel
			// 
			this.cmdExcel.AllowFocus = false;
			this.cmdExcel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.cmdExcel.Appearance.ForeColor = System.Drawing.Color.White;
			this.cmdExcel.Appearance.Options.UseBackColor = true;
			this.cmdExcel.Appearance.Options.UseForeColor = true;
			this.cmdExcel.AutoSize = true;
			this.cmdExcel.Dock = System.Windows.Forms.DockStyle.Right;
			this.cmdExcel.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.cmdExcel.ImageOptions.ImageIndex = 9;
			this.cmdExcel.ImageOptions.ImageToTextIndent = 15;
			this.cmdExcel.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.cmdExcel.Location = new System.Drawing.Point(1062, 0);
			this.cmdExcel.Name = "cmdExcel";
			this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.cmdExcel.Size = new System.Drawing.Size(101, 40);
			this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
			this.cmdExcel.TabIndex = 18;
			this.cmdExcel.Text = "엑셀변환";
			this.cmdExcel.Click += new System.EventHandler(this.BtnExcel_Click);
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
			this.smtSearchButton1.Location = new System.Drawing.Point(930, 0);
			this.smtSearchButton1.Name = "smtSearchButton1";
			this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
			this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
			this.smtSearchButton1.TabIndex = 8;
			this.smtSearchButton1.Text = "조회";
			this.smtSearchButton1.Click += new System.EventHandler(this.BtnSearch_Rst_Click);
			// 
			// LupProcess
			// 
			this.LupProcess.Dock = System.Windows.Forms.DockStyle.Left;
			this.LupProcess.Location = new System.Drawing.Point(354, 0);
			this.LupProcess.LookAndFeel.SkinName = "WXI";
			this.LupProcess.LookAndFeel.UseDefaultLookAndFeel = false;
			this.LupProcess.Name = "LupProcess";
			this.LupProcess.Size = new System.Drawing.Size(288, 40);
			this.LupProcess.smtLabelTitleValue = "공정";
			this.LupProcess.smtLabelTitleVisible = true;
			this.LupProcess.smtLabelTitleWidth = 100;
			this.LupProcess.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.LupProcess.smtLookUpDataDisplayMember = "";
			this.LupProcess.smtLookUpDataSource = null;
			this.LupProcess.smtLookUpDataValueMember = "";
			this.LupProcess.smtLookUpSelectdIndex = -1;
			this.LupProcess.smtLookUpText = "";
			this.LupProcess.smtLookUpValue = "";
			this.LupProcess.TabIndex = 17;
			// 
			// LupLine
			// 
			this.LupLine.Dock = System.Windows.Forms.DockStyle.Left;
			this.LupLine.Location = new System.Drawing.Point(642, 0);
			this.LupLine.LookAndFeel.SkinName = "WXI";
			this.LupLine.LookAndFeel.UseDefaultLookAndFeel = false;
			this.LupLine.Name = "LupLine";
			this.LupLine.Size = new System.Drawing.Size(288, 40);
			this.LupLine.smtLabelTitleValue = "라인";
			this.LupLine.smtLabelTitleVisible = true;
			this.LupLine.smtLabelTitleWidth = 100;
			this.LupLine.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.LupLine.smtLookUpDataDisplayMember = "";
			this.LupLine.smtLookUpDataSource = null;
			this.LupLine.smtLookUpDataValueMember = "";
			this.LupLine.smtLookUpSelectdIndex = -1;
			this.LupLine.smtLookUpText = "";
			this.LupLine.smtLookUpValue = "";
			this.LupLine.TabIndex = 16;
			// 
			// barDate
			// 
			this.barDate.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDate.Location = new System.Drawing.Point(134, 0);
			this.barDate.LookAndFeel.SkinName = "WXI";
			this.barDate.LookAndFeel.UseDefaultLookAndFeel = false;
			this.barDate.Name = "barDate";
			this.barDate.Size = new System.Drawing.Size(220, 40);
			this.barDate.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.barDate.smtComboTitleItems = new string[0];
			this.barDate.smtComboTitleValue = "";
			this.barDate.smtComboTitleVisible = false;
			this.barDate.smtComboTitleWidth = 100;
			this.barDate.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Month;
			this.barDate.smtEndDateTimeValue = new System.DateTime(2025, 1, 13, 10, 39, 30, 699);
			this.barDate.smtEndDateTimeVisible = false;
			this.barDate.smtLabelTitleValue = "기준일자";
			this.barDate.smtLabelTitleVisible = true;
			this.barDate.smtLabelTitleWidth = 100;
			this.barDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Year;
			this.barDate.smtStartDateTimeValue = new System.DateTime(2025, 1, 13, 10, 39, 30, 695);
			this.barDate.smtStartDateTimeVisible = true;
			this.barDate.TabIndex = 9;
			// 
			// btn_Colose
			// 
			this.btn_Colose.AllowFocus = false;
			this.btn_Colose.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.btn_Colose.Appearance.ForeColor = System.Drawing.Color.White;
			this.btn_Colose.Appearance.Options.UseBackColor = true;
			this.btn_Colose.Appearance.Options.UseForeColor = true;
			this.btn_Colose.AutoSize = true;
			this.btn_Colose.Dock = System.Windows.Forms.DockStyle.Right;
			this.btn_Colose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
			this.btn_Colose.ImageOptions.ImageIndex = 5;
			this.btn_Colose.ImageOptions.ImageToTextIndent = 15;
			this.btn_Colose.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
			this.btn_Colose.Location = new System.Drawing.Point(1163, 0);
			this.btn_Colose.Name = "btn_Colose";
			this.btn_Colose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btn_Colose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btn_Colose.Size = new System.Drawing.Size(77, 40);
			this.btn_Colose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.btn_Colose.TabIndex = 1;
			this.btn_Colose.Text = "닫기";
			this.btn_Colose.Click += new System.EventHandler(this.CmdClose_Click);
			// 
			// Frm_AN_ProductMachine
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1240, 605);
			this.Controls.Add(this.layoutControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "Frm_AN_ProductMachine";
			this.Text = "월별 생산량 차트";
			this.Load += new System.EventHandler(this.Frm_AN_ProductMachine_Day_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chartProduct)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gc_List)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Gv_List)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl Gc_List;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_List;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraCharts.ChartControl chartProduct;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdExcel;
		private baseSMT.smtSearchButton smtSearchButton1;
		private baseSMT.smtSearchLookUp LupProcess;
		private baseSMT.smtSearchLookUp LupLine;
		private baseSMT.smtSearchDatetime barDate;
		private baseSMT.smtSearchButton btn_Colose;
	}
}