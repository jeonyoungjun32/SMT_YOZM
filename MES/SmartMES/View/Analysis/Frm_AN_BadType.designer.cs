
namespace SmartMES.View.Analysis
{
    partial class Frm_AN_BadType
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
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.Doughnut3DSeriesLabel doughnut3DSeriesLabel1 = new DevExpress.XtraCharts.Doughnut3DSeriesLabel();
            DevExpress.XtraCharts.Doughnut3DSeriesView doughnut3DSeriesView1 = new DevExpress.XtraCharts.Doughnut3DSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AN_BadType));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartWeekNG = new DevExpress.XtraCharts.ChartControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.LupLine = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LupProcess = new baseSMT.smtSearchLookUp();
            this.barDate = new baseSMT.smtSearchDatetime();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Colose = new baseSMT.smtSearchButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartWeekNG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LupLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartWeekNG);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1289, 726);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartWeekNG
            // 
            this.chartWeekNG.AppearanceNameSerializable = "In A Fog";
            this.chartWeekNG.BackColor = System.Drawing.Color.Transparent;
            this.chartWeekNG.BorderOptions.Color = System.Drawing.Color.Transparent;
            this.chartWeekNG.BorderOptions.Thickness = 2;
            this.chartWeekNG.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.MinorCount = 4;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.DefaultPane.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            xyDiagram1.DefaultPane.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            xyDiagram1.DefaultPane.BorderVisible = false;
            xyDiagram1.DefaultPane.EnableAxisXScrolling = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.DefaultPane.EnableAxisXZooming = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.DefaultPane.EnableAxisYScrolling = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.DefaultPane.EnableAxisYZooming = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.DefaultPane.SelectionRectangle.BorderColor = System.Drawing.Color.Transparent;
            xyDiagram1.DefaultPane.SelectionRectangle.Color = System.Drawing.Color.White;
            xyDiagram1.DefaultPane.Shadow.Color = System.Drawing.Color.Empty;
            this.chartWeekNG.Diagram = xyDiagram1;
            this.chartWeekNG.IndicatorsPaletteName = "Black and White";
            this.chartWeekNG.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartWeekNG.Legend.DXFont = new DevExpress.Drawing.DXFont("맑은 고딕", 8F);
            this.chartWeekNG.Legend.Name = "Default Legend";
            this.chartWeekNG.Legend.Title.DXFont = new DevExpress.Drawing.DXFont("맑은 고딕", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.chartWeekNG.Legend.Title.Text = "불량유형";
            this.chartWeekNG.Legend.Title.Visible = true;
            this.chartWeekNG.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartWeekNG.Location = new System.Drawing.Point(3, 3);
            this.chartWeekNG.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartWeekNG.Name = "chartWeekNG";
            this.chartWeekNG.PaletteName = "In A Fog";
            sideBySideBarSeriesLabel1.DXFont = new DevExpress.Drawing.DXFont("맑은 고딕", 13F);
            sideBySideBarSeriesLabel1.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            sideBySideBarSeriesLabel1.ShowForZeroValues = true;
            series1.Label = sideBySideBarSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.LegendName = "Default Legend";
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            series1.SeriesPointsSortingKey = DevExpress.XtraCharts.SeriesPointKey.Value_1;
            this.chartWeekNG.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            doughnut3DSeriesLabel1.LineStyle.DashStyle = DevExpress.XtraCharts.DashStyle.Dash;
            this.chartWeekNG.SeriesTemplate.Label = doughnut3DSeriesLabel1;
            this.chartWeekNG.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartWeekNG.SeriesTemplate.View = doughnut3DSeriesView1;
            this.chartWeekNG.Size = new System.Drawing.Size(1283, 720);
            this.chartWeekNG.TabIndex = 283;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1289, 726);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chartWeekNG;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1289, 726);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.LupLine);
            this.smtCommonBar1.Controls.Add(this.LupProcess);
            this.smtCommonBar1.Controls.Add(this.barDate);
            this.smtCommonBar1.Controls.Add(this.labelControl1);
            this.smtCommonBar1.Controls.Add(this.btn_Colose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1289, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "불량유형집계 ";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 2;
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
            this.smtSearchButton1.Location = new System.Drawing.Point(627, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.smtSearchButton1.TabIndex = 8;
            this.smtSearchButton1.Text = "조회";
            this.smtSearchButton1.Click += new System.EventHandler(this.BtnSearch_Rst_Click);
            // 
            // LupLine
            // 
            this.LupLine.EditValue = "";
            this.LupLine.Location = new System.Drawing.Point(743, 6);
            this.LupLine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LupLine.Name = "LupLine";
            this.LupLine.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
            this.LupLine.Properties.Appearance.Options.UseFont = true;
            this.LupLine.Properties.AutoHeight = false;
            this.LupLine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LupLine.Properties.NullText = "";
            this.LupLine.Properties.PopupView = this.gridView1;
            this.LupLine.Size = new System.Drawing.Size(122, 24);
            this.LupLine.TabIndex = 2;
            this.LupLine.Visible = false;
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 340;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // LupProcess
            // 
            this.LupProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupProcess.Location = new System.Drawing.Point(339, 0);
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
            this.LupProcess.TabIndex = 16;
            // 
            // barDate
            // 
            this.barDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDate.Location = new System.Drawing.Point(119, 0);
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
            this.barDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.barDate.smtStartDateTimeValue = new System.DateTime(2025, 1, 13, 10, 39, 30, 695);
            this.barDate.smtStartDateTimeVisible = true;
            this.barDate.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(704, 6);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.labelControl1.Size = new System.Drawing.Size(70, 26);
            this.labelControl1.TabIndex = 273;
            this.labelControl1.Text = "라인";
            this.labelControl1.Visible = false;
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
            this.btn_Colose.Location = new System.Drawing.Point(1212, 0);
            this.btn_Colose.Name = "btn_Colose";
            this.btn_Colose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Colose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Colose.Size = new System.Drawing.Size(77, 40);
            this.btn_Colose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Colose.TabIndex = 1;
            this.btn_Colose.Text = "닫기";
            this.btn_Colose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // Frm_AN_BadType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 766);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_AN_BadType";
            this.Text = "불량유형집계 ";
            this.Load += new System.EventHandler(this.Frm_AN_Progress_Day_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnut3DSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartWeekNG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LupLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SearchLookUpEdit LupLine;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraCharts.ChartControl chartWeekNG;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton smtSearchButton1;
		private baseSMT.smtSearchLookUp LupProcess;
		private baseSMT.smtSearchDatetime barDate;
		private baseSMT.smtSearchButton btn_Colose;
	}
}