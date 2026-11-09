
namespace SmartMES.View.Analysis
{
    partial class Frm_AN_Bad_ChkView
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
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AN_Bad_ChkView));
            this.barChart = new DevExpress.XtraCharts.ChartControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_Bad_Chk = new DevExpress.XtraGrid.GridControl();
            this.Gv_Bad_Chk = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_ProductCmd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.LupProcess = new baseSMT.smtSearchLookUp();
            this.LupItem = new baseSMT.smtSearchLookUp();
            this.barDate = new baseSMT.smtSearchDatetime();
            this.btn_Colose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Bad_Chk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Bad_Chk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProductCmd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barChart
            // 
            xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "16.9632592592592";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "-0.933333333333333";
            xyDiagram1.AxisX.WholeRange.Auto = false;
            xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "30";
            xyDiagram1.AxisX.WholeRange.MinValueSerializable = "1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.EnableAxisXScrolling = true;
            this.barChart.Diagram = xyDiagram1;
            this.barChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barChart.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.barChart.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.barChart.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.barChart.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            this.barChart.Location = new System.Drawing.Point(0, 0);
            this.barChart.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.barChart.Name = "barChart";
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            this.barChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.barChart.Size = new System.Drawing.Size(1426, 245);
            this.barChart.TabIndex = 276;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.Gc_Bad_Chk);
            this.groupControl2.Controls.Add(this.label1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1426, 356);
            this.groupControl2.TabIndex = 275;
            this.groupControl2.Text = "불량현황";
            // 
            // Gc_Bad_Chk
            // 
            this.Gc_Bad_Chk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_Bad_Chk.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Gc_Bad_Chk.Location = new System.Drawing.Point(2, 36);
            this.Gc_Bad_Chk.MainView = this.Gv_Bad_Chk;
            this.Gc_Bad_Chk.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Gc_Bad_Chk.Name = "Gc_Bad_Chk";
            this.Gc_Bad_Chk.Size = new System.Drawing.Size(1422, 318);
            this.Gc_Bad_Chk.TabIndex = 269;
            this.Gc_Bad_Chk.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Bad_Chk,
            this.Gv_ProductCmd,
            this.gridView5});
            // 
            // Gv_Bad_Chk
            // 
            this.Gv_Bad_Chk.DetailHeight = 467;
            this.Gv_Bad_Chk.GridControl = this.Gc_Bad_Chk;
            this.Gv_Bad_Chk.Name = "Gv_Bad_Chk";
            this.Gv_Bad_Chk.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_Bad_Chk.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_ProductCmd
            // 
            this.Gv_ProductCmd.DetailHeight = 467;
            this.Gv_ProductCmd.GridControl = this.Gc_Bad_Chk;
            this.Gv_ProductCmd.Name = "Gv_ProductCmd";
            this.Gv_ProductCmd.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_ProductCmd.OptionsView.ShowGroupPanel = false;
            // 
            // gridView5
            // 
            this.gridView5.DetailHeight = 467;
            this.gridView5.GridControl = this.Gc_Bad_Chk;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsEditForm.PopupEditFormWidth = 1000;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(462, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 20);
            this.label1.TabIndex = 287;
            this.label1.Text = " ";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(18, 62);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.barChart);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1426, 621);
            this.splitContainerControl1.SplitterPosition = 356;
            this.splitContainerControl1.TabIndex = 278;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.LupProcess);
            this.smtCommonBar1.Controls.Add(this.LupItem);
            this.smtCommonBar1.Controls.Add(this.barDate);
            this.smtCommonBar1.Controls.Add(this.btn_Colose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1460, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "유형별불량현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 289;
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
            this.cmdExcel.Location = new System.Drawing.Point(1246, 0);
            this.cmdExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(122, 40);
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
            this.smtSearchButton1.Location = new System.Drawing.Point(1097, 0);
            this.smtSearchButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(92, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.smtSearchButton1.TabIndex = 8;
            this.smtSearchButton1.Text = "조회";
            this.smtSearchButton1.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // LupProcess
            // 
            this.LupProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupProcess.Location = new System.Drawing.Point(737, 0);
            this.LupProcess.LookAndFeel.SkinName = "WXI";
            this.LupProcess.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupProcess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LupProcess.Name = "LupProcess";
            this.LupProcess.Size = new System.Drawing.Size(360, 40);
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
            // LupItem
            // 
            this.LupItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupItem.Location = new System.Drawing.Point(377, 0);
            this.LupItem.LookAndFeel.SkinName = "WXI";
            this.LupItem.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LupItem.Name = "LupItem";
            this.LupItem.Size = new System.Drawing.Size(360, 40);
            this.LupItem.smtLabelTitleValue = "품목";
            this.LupItem.smtLabelTitleVisible = true;
            this.LupItem.smtLabelTitleWidth = 100;
            this.LupItem.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupItem.smtLookUpDataDisplayMember = "";
            this.LupItem.smtLookUpDataSource = null;
            this.LupItem.smtLookUpDataValueMember = "";
            this.LupItem.smtLookUpSelectdIndex = -1;
            this.LupItem.smtLookUpText = "";
            this.LupItem.smtLookUpValue = "";
            this.LupItem.TabIndex = 16;
            this.LupItem.TabStop = false;
            this.LupItem.Visible = false;
            // 
            // barDate
            // 
            this.barDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDate.Location = new System.Drawing.Point(157, 0);
            this.barDate.LookAndFeel.SkinName = "WXI";
            this.barDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.barDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
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
            this.btn_Colose.Location = new System.Drawing.Point(1368, 0);
            this.btn_Colose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Colose.Name = "btn_Colose";
            this.btn_Colose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btn_Colose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Colose.Size = new System.Drawing.Size(92, 40);
            this.btn_Colose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Colose.TabIndex = 1;
            this.btn_Colose.Text = "닫기";
            this.btn_Colose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Frm_AN_Bad_ChkView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1460, 700);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "Frm_AN_Bad_ChkView";
            this.Text = "유형별불량현황";
            this.Load += new System.EventHandler(this.Frm_QT_Bad_ChkView_Load);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Bad_Chk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Bad_Chk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProductCmd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraCharts.ChartControl barChart;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl Gc_Bad_Chk;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Bad_Chk;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_ProductCmd;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.Label label1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton smtSearchButton1;
		private baseSMT.smtSearchLookUp LupItem;
		private baseSMT.smtSearchDatetime barDate;
		private baseSMT.smtSearchButton btn_Colose;
		private baseSMT.smtSearchLookUp LupProcess;
		private baseSMT.smtSearchButton cmdExcel;
	}
}