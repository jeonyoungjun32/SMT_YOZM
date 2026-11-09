
namespace SmartMES.View.Management
{
    partial class Frm_MM_ProductAnalysis_View
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
            DevExpress.XtraCharts.TextAnnotation textAnnotation1 = new DevExpress.XtraCharts.TextAnnotation();
            DevExpress.XtraCharts.ChartAnchorPoint chartAnchorPoint1 = new DevExpress.XtraCharts.ChartAnchorPoint();
            DevExpress.XtraCharts.FreePosition freePosition1 = new DevExpress.XtraCharts.FreePosition();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_ProductAnalysis_View));
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_Product = new DevExpress.XtraGrid.GridControl();
            this.Gv_Product = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_ProductCmd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barChart = new DevExpress.XtraCharts.ChartControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdDisplay = new baseSMT.smtSearchButton();
            this.Lup_Process = new baseSMT.smtSearchLookUp();
            this.Lup_Item = new baseSMT.smtSearchLookUp();
            this.LupItemClass1 = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.cmdClose = new baseSMT.smtSearchButton();
            this.LupItemClass2 = new baseSMT.smtSearchLookUp();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Product)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Product)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProductCmd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(textAnnotation1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
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
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.Gc_Product);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1486, 297);
            this.groupControl2.TabIndex = 275;
            this.groupControl2.Text = "생산분석현황";
            // 
            // Gc_Product
            // 
            this.Gc_Product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_Product.Location = new System.Drawing.Point(2, 29);
            this.Gc_Product.MainView = this.Gv_Product;
            this.Gc_Product.Name = "Gc_Product";
            this.Gc_Product.Size = new System.Drawing.Size(1482, 266);
            this.Gc_Product.TabIndex = 269;
            this.Gc_Product.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Product,
            this.Gv_ProductCmd});
            // 
            // Gv_Product
            // 
            this.Gv_Product.DetailHeight = 397;
            this.Gv_Product.GridControl = this.Gc_Product;
            this.Gv_Product.Name = "Gv_Product";
            this.Gv_Product.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_ProductCmd
            // 
            this.Gv_ProductCmd.DetailHeight = 397;
            this.Gv_ProductCmd.GridControl = this.Gc_Product;
            this.Gv_ProductCmd.Name = "Gv_ProductCmd";
            this.Gv_ProductCmd.OptionsView.ShowGroupPanel = false;
            // 
            // barChart
            // 
            textAnnotation1.AnchorPoint = chartAnchorPoint1;
            textAnnotation1.AnnotationID = 0;
            textAnnotation1.AutoHeight = true;
            textAnnotation1.AutoWidth = true;
            textAnnotation1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            textAnnotation1.ConnectorStyle = DevExpress.XtraCharts.AnnotationConnectorStyle.None;
            textAnnotation1.DXFont = new DevExpress.Drawing.DXFont("맑은 고딕", 8F);
            textAnnotation1.Name = "Annotation1";
            textAnnotation1.ShapeKind = DevExpress.XtraCharts.ShapeKind.Rectangle;
            freePosition1.DockCorner = DevExpress.XtraCharts.DockCorner.RightTop;
            freePosition1.InnerIndents.Bottom = 782;
            freePosition1.InnerIndents.Left = 853;
            freePosition1.InnerIndents.Right = 4;
            freePosition1.InnerIndents.Top = 5;
            textAnnotation1.ShapePosition = freePosition1;
            textAnnotation1.Text = "해당 차트는 \'기준년월\'만 반영된 월 양품수량의 총합계입니다.";
            textAnnotation1.TextAlignment = System.Drawing.StringAlignment.Near;
            textAnnotation1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(55)))), ((int)(((byte)(52)))));
            this.barChart.AnnotationRepository.AddRange(new DevExpress.XtraCharts.Annotation[] {
            textAnnotation1});
            this.barChart.AutoLayout = false;
            xyDiagram1.AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
            xyDiagram1.AxisX.Tickmarks.MinorVisible = false;
            xyDiagram1.AxisX.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "10.2560590094837";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "0.0560590094836662";
            xyDiagram1.AxisX.WholeRange.Auto = false;
            xyDiagram1.AxisX.WholeRange.MaxValueSerializable = "31";
            xyDiagram1.AxisX.WholeRange.MinValueSerializable = "0";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.EnableAxisXScrolling = true;
            this.barChart.Diagram = xyDiagram1;
            this.barChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barChart.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left;
            this.barChart.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.barChart.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.barChart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.barChart.Location = new System.Drawing.Point(0, 0);
            this.barChart.Name = "barChart";
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            this.barChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.barChart.Size = new System.Drawing.Size(1486, 217);
            this.barChart.TabIndex = 277;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(14, 52);
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
            this.splitContainerControl1.Size = new System.Drawing.Size(1486, 530);
            this.splitContainerControl1.SplitterPosition = 297;
            this.splitContainerControl1.TabIndex = 279;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.cmdDisplay);
            this.smtCommonBar1.Controls.Add(this.Lup_Process);
            this.smtCommonBar1.Controls.Add(this.Lup_Item);
            this.smtCommonBar1.Controls.Add(this.LupItemClass2);
            this.smtCommonBar1.Controls.Add(this.LupItemClass1);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.cmdExcel);
            this.smtCommonBar1.Controls.Add(this.cmdClose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1513, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "생산분석현황";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 281;
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
            this.cmdDisplay.Location = new System.Drawing.Point(1307, 0);
            this.cmdDisplay.Name = "cmdDisplay";
            this.cmdDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdDisplay.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdDisplay.Size = new System.Drawing.Size(77, 40);
            this.cmdDisplay.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.cmdDisplay.TabIndex = 5;
            this.cmdDisplay.Text = "조회";
            this.cmdDisplay.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // Lup_Process
            // 
            this.Lup_Process.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Process.Location = new System.Drawing.Point(1087, 0);
            this.Lup_Process.LookAndFeel.SkinName = "WXI";
            this.Lup_Process.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Process.Name = "Lup_Process";
            this.Lup_Process.Size = new System.Drawing.Size(220, 40);
            this.Lup_Process.smtLabelTitleValue = "공정";
            this.Lup_Process.smtLabelTitleVisible = true;
            this.Lup_Process.smtLabelTitleWidth = 80;
            this.Lup_Process.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Process.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Process.smtLookUpDataSource = null;
            this.Lup_Process.smtLookUpDataValueMember = "코드";
            this.Lup_Process.smtLookUpSelectdIndex = -1;
            this.Lup_Process.smtLookUpText = "";
            this.Lup_Process.smtLookUpValue = "";
            this.Lup_Process.TabIndex = 293;
            // 
            // Lup_Item
            // 
            this.Lup_Item.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Item.Location = new System.Drawing.Point(836, 0);
            this.Lup_Item.LookAndFeel.SkinName = "WXI";
            this.Lup_Item.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Item.Name = "Lup_Item";
            this.Lup_Item.Size = new System.Drawing.Size(251, 40);
            this.Lup_Item.smtLabelTitleValue = "품목";
            this.Lup_Item.smtLabelTitleVisible = true;
            this.Lup_Item.smtLabelTitleWidth = 80;
            this.Lup_Item.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Item.smtLookUpDataDisplayMember = "코드명";
            this.Lup_Item.smtLookUpDataSource = null;
            this.Lup_Item.smtLookUpDataValueMember = "코드";
            this.Lup_Item.smtLookUpSelectdIndex = -1;
            this.Lup_Item.smtLookUpText = "";
            this.Lup_Item.smtLookUpValue = "";
            this.Lup_Item.TabIndex = 62;
            // 
            // LupItemClass1
            // 
            this.LupItemClass1.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupItemClass1.Location = new System.Drawing.Point(336, 0);
            this.LupItemClass1.LookAndFeel.SkinName = "WXI";
            this.LupItemClass1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupItemClass1.Name = "LupItemClass1";
            this.LupItemClass1.Size = new System.Drawing.Size(247, 40);
            this.LupItemClass1.smtLabelTitleValue = "품목분류 1";
            this.LupItemClass1.smtLabelTitleVisible = true;
            this.LupItemClass1.smtLabelTitleWidth = 80;
            this.LupItemClass1.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupItemClass1.smtLookUpDataDisplayMember = "";
            this.LupItemClass1.smtLookUpDataSource = null;
            this.LupItemClass1.smtLookUpDataValueMember = "";
            this.LupItemClass1.smtLookUpSelectdIndex = -1;
            this.LupItemClass1.smtLookUpText = "";
            this.LupItemClass1.smtLookUpValue = "";
            this.LupItemClass1.TabIndex = 294;
            this.LupItemClass1.smtLookUpEditValueChanged += new System.EventHandler(this.LupItemClass1_smtLookUpEditValueChanged);
            // 
            // Dt_Date
            // 
            this.Dt_Date.Dock = System.Windows.Forms.DockStyle.Left;
            this.Dt_Date.Location = new System.Drawing.Point(116, 0);
            this.Dt_Date.LookAndFeel.SkinName = "WXI";
            this.Dt_Date.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Dt_Date.Name = "Dt_Date";
            this.Dt_Date.Size = new System.Drawing.Size(220, 40);
            this.Dt_Date.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.Dt_Date.smtComboTitleItems = new string[0];
            this.Dt_Date.smtComboTitleValue = "";
            this.Dt_Date.smtComboTitleVisible = false;
            this.Dt_Date.smtComboTitleWidth = 100;
            this.Dt_Date.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.Dt_Date.smtEndDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 729);
            this.Dt_Date.smtEndDateTimeVisible = false;
            this.Dt_Date.smtLabelTitleValue = "조회기간";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 61;
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
            this.cmdExcel.Location = new System.Drawing.Point(1335, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(101, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 6;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
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
            this.cmdClose.Location = new System.Drawing.Point(1436, 0);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdClose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdClose.Size = new System.Drawing.Size(77, 40);
            this.cmdClose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "닫기";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // LupItemClass2
            // 
            this.LupItemClass2.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupItemClass2.Location = new System.Drawing.Point(583, 0);
            this.LupItemClass2.LookAndFeel.SkinName = "WXI";
            this.LupItemClass2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupItemClass2.Name = "LupItemClass2";
            this.LupItemClass2.Size = new System.Drawing.Size(253, 40);
            this.LupItemClass2.smtLabelTitleValue = "품목분류 2";
            this.LupItemClass2.smtLabelTitleVisible = true;
            this.LupItemClass2.smtLabelTitleWidth = 80;
            this.LupItemClass2.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupItemClass2.smtLookUpDataDisplayMember = "";
            this.LupItemClass2.smtLookUpDataSource = null;
            this.LupItemClass2.smtLookUpDataValueMember = "";
            this.LupItemClass2.smtLookUpSelectdIndex = -1;
            this.LupItemClass2.smtLookUpText = "";
            this.LupItemClass2.smtLookUpValue = "";
            this.LupItemClass2.TabIndex = 295;
            // 
            // Frm_MM_ProductAnalysis_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1513, 595);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_MM_ProductAnalysis_View";
            this.Text = "생산분석현황";
            this.Load += new System.EventHandler(this.Frm_MM_ProductAnalysis_View_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Product)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Product)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProductCmd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(textAnnotation1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).EndInit();
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

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl Gc_Product;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Product;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_ProductCmd;
        private DevExpress.XtraCharts.ChartControl barChart;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton cmdDisplay;
		private baseSMT.smtSearchButton cmdExcel;
		private baseSMT.smtSearchButton cmdClose;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchLookUp Lup_Item;
        private baseSMT.smtSearchLookUp Lup_Process;
        private baseSMT.smtSearchLookUp LupItemClass1;
        private baseSMT.smtSearchLookUp LupItemClass2;
    }
}