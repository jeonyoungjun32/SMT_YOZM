
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.Monitoring
{
	partial class Frm_MM_QualityDashboard
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
            DevExpress.XtraCharts.XYDiagram xyDiagram6 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series8 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView4 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram7 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series9 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView5 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series10 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView3 = new DevExpress.XtraCharts.DoughnutSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_QualityDashboard));
            DevExpress.XtraCharts.XYDiagram xyDiagram8 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series11 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView5 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView6 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series12 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView4 = new DevExpress.XtraCharts.DoughnutSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram9 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series13 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView6 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram10 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series14 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView7 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView8 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange10 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange11 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange12 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange13 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange14 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange15 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange16 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange17 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange18 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_DefectTop5 = new DevExpress.XtraGrid.GridControl();
            this.Gv_DefectTop5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.chartNgRate = new DevExpress.XtraCharts.ChartControl();
            this.chartNgRateByProcess = new DevExpress.XtraCharts.ChartControl();
            this.chartDefectStatus = new DevExpress.XtraCharts.ChartControl();
            this.Gc_WorkDefect = new DevExpress.XtraGrid.GridControl();
            this.Gv_WorkDefect = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar121 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.TPMain = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl9 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox3 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblNgQty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox6 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblGoodRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl111 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox2 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblRework = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox4 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblNgRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox1 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblInspectionQty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.chartExamResult = new DevExpress.XtraCharts.ChartControl();
            this.chartNgType = new DevExpress.XtraCharts.ChartControl();
            this.chartNgByExam = new DevExpress.XtraCharts.ChartControl();
            this.chartGoodRate = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.LblCurTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.toggleSwitch1 = new DevExpress.XtraEditors.ToggleSwitch();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Tm_Time = new System.Windows.Forms.Timer(this.components);
            this.Tm_Search = new System.Windows.Forms.Timer(this.components);
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.circularGauge13 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.MachneComponent = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.circularGauge1 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.arcScaleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.circularGauge2 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.arcScaleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_DefectTop5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_DefectTop5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNgRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNgRateByProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDefectStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_WorkDefect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_WorkDefect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar121)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).BeginInit();
            this.TPMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl9)).BeginInit();
            this.panelControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).BeginInit();
            this.panelControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).BeginInit();
            this.panelControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartExamResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNgType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNgByExam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGoodRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MachneComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.BackColor = System.Drawing.Color.Transparent;
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1195, 363, 938, 655);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1412, 673);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl2);
            this.panelControl1.Controls.Add(this.smtCommonBar1);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1412, 673);
            this.panelControl1.TabIndex = 1;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.Gc_DefectTop5);
            this.layoutControl2.Controls.Add(this.chartNgRate);
            this.layoutControl2.Controls.Add(this.chartNgRateByProcess);
            this.layoutControl2.Controls.Add(this.chartDefectStatus);
            this.layoutControl2.Controls.Add(this.Gc_WorkDefect);
            this.layoutControl2.Controls.Add(this.TPMain);
            this.layoutControl2.Controls.Add(this.chartExamResult);
            this.layoutControl2.Controls.Add(this.chartNgType);
            this.layoutControl2.Controls.Add(this.chartNgByExam);
            this.layoutControl2.Controls.Add(this.chartGoodRate);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 42);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(11, 440, 650, 400);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1408, 629);
            this.layoutControl2.TabIndex = 295;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // Gc_DefectTop5
            // 
            this.Gc_DefectTop5.Location = new System.Drawing.Point(4, 477);
            this.Gc_DefectTop5.MainView = this.Gv_DefectTop5;
            this.Gc_DefectTop5.Name = "Gc_DefectTop5";
            this.Gc_DefectTop5.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
            this.Gc_DefectTop5.Size = new System.Drawing.Size(461, 148);
            this.Gc_DefectTop5.TabIndex = 19;
            this.Gc_DefectTop5.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_DefectTop5});
            // 
            // Gv_DefectTop5
            // 
            this.Gv_DefectTop5.GridControl = this.Gc_DefectTop5;
            this.Gv_DefectTop5.Name = "Gv_DefectTop5";
            this.Gv_DefectTop5.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // chartNgRate
            // 
            this.chartNgRate.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram6.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram6.AxisY.VisibleInPanesSerializable = "-1";
            this.chartNgRate.Diagram = xyDiagram6;
            this.chartNgRate.Location = new System.Drawing.Point(476, 124);
            this.chartNgRate.LookAndFeel.SkinName = "DevExpress Style";
            this.chartNgRate.Name = "chartNgRate";
            this.chartNgRate.PaletteName = "Flow";
            series8.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series8.Name = "Series 1";
            series8.SeriesID = 0;
            sideBySideBarSeriesView4.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series8.View = sideBySideBarSeriesView4;
            this.chartNgRate.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series8};
            this.chartNgRate.Size = new System.Drawing.Size(455, 136);
            this.chartNgRate.TabIndex = 18;
            // 
            // chartNgRateByProcess
            // 
            this.chartNgRateByProcess.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram7.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram7.AxisY.Label.TextPattern = "{V:#,##0}";
            xyDiagram7.AxisY.VisibleInPanesSerializable = "-1";
            this.chartNgRateByProcess.Diagram = xyDiagram7;
            this.chartNgRateByProcess.Location = new System.Drawing.Point(473, 298);
            this.chartNgRateByProcess.LookAndFeel.SkinName = "DevExpress Style";
            this.chartNgRateByProcess.Name = "chartNgRateByProcess";
            this.chartNgRateByProcess.PaletteName = "Flow";
            series9.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series9.Name = "Series 1";
            series9.SeriesID = 0;
            sideBySideBarSeriesView5.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series9.View = sideBySideBarSeriesView5;
            this.chartNgRateByProcess.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series9};
            this.chartNgRateByProcess.Size = new System.Drawing.Size(461, 144);
            this.chartNgRateByProcess.TabIndex = 17;
            // 
            // chartDefectStatus
            // 
            this.chartDefectStatus.Location = new System.Drawing.Point(473, 477);
            this.chartDefectStatus.Name = "chartDefectStatus";
            this.chartDefectStatus.PaletteName = "In A Fog";
            series10.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series10.Name = "Series 1";
            series10.SeriesID = 1;
            series10.View = doughnutSeriesView3;
            this.chartDefectStatus.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series10};
            this.chartDefectStatus.Size = new System.Drawing.Size(461, 148);
            this.chartDefectStatus.TabIndex = 16;
            // 
            // Gc_WorkDefect
            // 
            this.Gc_WorkDefect.Location = new System.Drawing.Point(945, 480);
            this.Gc_WorkDefect.MainView = this.Gv_WorkDefect;
            this.Gc_WorkDefect.Name = "Gc_WorkDefect";
            this.Gc_WorkDefect.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar121});
            this.Gc_WorkDefect.Size = new System.Drawing.Size(456, 142);
            this.Gc_WorkDefect.TabIndex = 15;
            this.Gc_WorkDefect.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_WorkDefect});
            // 
            // Gv_WorkDefect
            // 
            this.Gv_WorkDefect.GridControl = this.Gc_WorkDefect;
            this.Gv_WorkDefect.Name = "Gv_WorkDefect";
            this.Gv_WorkDefect.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar121
            // 
            this.repositoryItemProgressBar121.Name = "repositoryItemProgressBar121";
            // 
            // TPMain
            // 
            this.TPMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.TPMain.Appearance.Options.UseBackColor = true;
            this.TPMain.AutoScroll = true;
            this.TPMain.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F)});
            this.TPMain.Controls.Add(this.panelControl9);
            this.TPMain.Controls.Add(this.panelControl12);
            this.TPMain.Controls.Add(this.panelControl7);
            this.TPMain.Controls.Add(this.panelControl3);
            this.TPMain.Controls.Add(this.panelControl5);
            this.TPMain.Location = new System.Drawing.Point(3, 3);
            this.TPMain.Name = "TPMain";
            this.TPMain.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.TPMain.Size = new System.Drawing.Size(1402, 84);
            this.TPMain.TabIndex = 1;
            // 
            // panelControl9
            // 
            this.TPMain.SetColumn(this.panelControl9, 4);
            this.panelControl9.Controls.Add(this.svgImageBox3);
            this.panelControl9.Controls.Add(this.LblNgQty);
            this.panelControl9.Controls.Add(this.labelControl15);
            this.panelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl9.Location = new System.Drawing.Point(1125, 3);
            this.panelControl9.Name = "panelControl9";
            this.TPMain.SetRow(this.panelControl9, 0);
            this.panelControl9.Size = new System.Drawing.Size(274, 78);
            this.panelControl9.TabIndex = 4;
            // 
            // svgImageBox3
            // 
            this.svgImageBox3.Location = new System.Drawing.Point(11, 16);
            this.svgImageBox3.Name = "svgImageBox3";
            this.svgImageBox3.Size = new System.Drawing.Size(52, 48);
            this.svgImageBox3.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox3.SvgImage")));
            this.svgImageBox3.TabIndex = 4;
            this.svgImageBox3.Text = "svgImageBox3";
            // 
            // LblNgQty
            // 
            this.LblNgQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 18F);
            this.LblNgQty.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.LblNgQty.Appearance.Options.UseFont = true;
            this.LblNgQty.Appearance.Options.UseForeColor = true;
            this.LblNgQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblNgQty.Location = new System.Drawing.Point(69, 27);
            this.LblNgQty.Name = "LblNgQty";
            this.LblNgQty.Size = new System.Drawing.Size(200, 46);
            this.LblNgQty.TabIndex = 1;
            this.LblNgQty.Text = "-";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl15.Location = new System.Drawing.Point(69, 9);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(114, 17);
            this.labelControl15.TabIndex = 0;
            this.labelControl15.Text = "판정 부적합 건수";
            // 
            // panelControl12
            // 
            this.TPMain.SetColumn(this.panelControl12, 0);
            this.panelControl12.Controls.Add(this.svgImageBox6);
            this.panelControl12.Controls.Add(this.LblGoodRate);
            this.panelControl12.Controls.Add(this.labelControl111);
            this.panelControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl12.Location = new System.Drawing.Point(3, 3);
            this.panelControl12.Name = "panelControl12";
            this.TPMain.SetRow(this.panelControl12, 0);
            this.panelControl12.Size = new System.Drawing.Size(274, 78);
            this.panelControl12.TabIndex = 0;
            // 
            // svgImageBox6
            // 
            this.svgImageBox6.Location = new System.Drawing.Point(4, 16);
            this.svgImageBox6.Name = "svgImageBox6";
            this.svgImageBox6.Size = new System.Drawing.Size(52, 48);
            this.svgImageBox6.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox6.SvgImage")));
            this.svgImageBox6.TabIndex = 4;
            this.svgImageBox6.Text = "svgImageBox6";
            // 
            // LblGoodRate
            // 
            this.LblGoodRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblGoodRate.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblGoodRate.Appearance.Options.UseFont = true;
            this.LblGoodRate.Appearance.Options.UseForeColor = true;
            this.LblGoodRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblGoodRate.Location = new System.Drawing.Point(62, 26);
            this.LblGoodRate.Name = "LblGoodRate";
            this.LblGoodRate.Size = new System.Drawing.Size(135, 46);
            this.LblGoodRate.TabIndex = 1;
            this.LblGoodRate.Text = "-";
            // 
            // labelControl111
            // 
            this.labelControl111.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl111.Appearance.Options.UseForeColor = true;
            this.labelControl111.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl111.Location = new System.Drawing.Point(64, 9);
            this.labelControl111.Name = "labelControl111";
            this.labelControl111.Size = new System.Drawing.Size(146, 17);
            this.labelControl111.TabIndex = 0;
            this.labelControl111.Text = "전체 양품률";
            // 
            // panelControl7
            // 
            this.TPMain.SetColumn(this.panelControl7, 3);
            this.panelControl7.Controls.Add(this.svgImageBox2);
            this.panelControl7.Controls.Add(this.LblRework);
            this.panelControl7.Controls.Add(this.labelControl12);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl7.Location = new System.Drawing.Point(844, 3);
            this.panelControl7.Name = "panelControl7";
            this.TPMain.SetRow(this.panelControl7, 0);
            this.panelControl7.Size = new System.Drawing.Size(274, 78);
            this.panelControl7.TabIndex = 3;
            // 
            // svgImageBox2
            // 
            this.svgImageBox2.Location = new System.Drawing.Point(14, 16);
            this.svgImageBox2.Name = "svgImageBox2";
            this.svgImageBox2.Size = new System.Drawing.Size(52, 48);
            this.svgImageBox2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox2.SvgImage")));
            this.svgImageBox2.TabIndex = 3;
            this.svgImageBox2.Text = "svgImageBox2";
            // 
            // LblRework
            // 
            this.LblRework.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblRework.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LblRework.Appearance.Options.UseFont = true;
            this.LblRework.Appearance.Options.UseForeColor = true;
            this.LblRework.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblRework.Location = new System.Drawing.Point(74, 27);
            this.LblRework.Name = "LblRework";
            this.LblRework.Size = new System.Drawing.Size(137, 46);
            this.LblRework.TabIndex = 1;
            this.LblRework.Text = "-";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Appearance.Options.UseForeColor = true;
            this.labelControl12.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl12.Location = new System.Drawing.Point(74, 9);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(118, 17);
            this.labelControl12.TabIndex = 0;
            this.labelControl12.Text = "재작업 건수";
            // 
            // panelControl3
            // 
            this.TPMain.SetColumn(this.panelControl3, 1);
            this.panelControl3.Controls.Add(this.svgImageBox4);
            this.panelControl3.Controls.Add(this.LblNgRate);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(283, 3);
            this.panelControl3.Name = "panelControl3";
            this.TPMain.SetRow(this.panelControl3, 0);
            this.panelControl3.Size = new System.Drawing.Size(274, 78);
            this.panelControl3.TabIndex = 1;
            // 
            // svgImageBox4
            // 
            this.svgImageBox4.Location = new System.Drawing.Point(12, 16);
            this.svgImageBox4.Name = "svgImageBox4";
            this.svgImageBox4.Size = new System.Drawing.Size(52, 48);
            this.svgImageBox4.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox4.SvgImage")));
            this.svgImageBox4.TabIndex = 3;
            this.svgImageBox4.Text = "svgImageBox4";
            // 
            // LblNgRate
            // 
            this.LblNgRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblNgRate.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LblNgRate.Appearance.Options.UseFont = true;
            this.LblNgRate.Appearance.Options.UseForeColor = true;
            this.LblNgRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblNgRate.Location = new System.Drawing.Point(70, 26);
            this.LblNgRate.Name = "LblNgRate";
            this.LblNgRate.Size = new System.Drawing.Size(199, 46);
            this.LblNgRate.TabIndex = 1;
            this.LblNgRate.Text = "-";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(70, 9);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(135, 17);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "전체 불량률 (PPM)";
            // 
            // panelControl5
            // 
            this.TPMain.SetColumn(this.panelControl5, 2);
            this.panelControl5.Controls.Add(this.svgImageBox1);
            this.panelControl5.Controls.Add(this.LblInspectionQty);
            this.panelControl5.Controls.Add(this.labelControl9);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(564, 3);
            this.panelControl5.Name = "panelControl5";
            this.TPMain.SetRow(this.panelControl5, 0);
            this.panelControl5.Size = new System.Drawing.Size(274, 78);
            this.panelControl5.TabIndex = 2;
            // 
            // svgImageBox1
            // 
            this.svgImageBox1.Location = new System.Drawing.Point(12, 16);
            this.svgImageBox1.Name = "svgImageBox1";
            this.svgImageBox1.Size = new System.Drawing.Size(52, 48);
            this.svgImageBox1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox1.SvgImage")));
            this.svgImageBox1.TabIndex = 2;
            this.svgImageBox1.Text = "svgImageBox1";
            // 
            // LblInspectionQty
            // 
            this.LblInspectionQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblInspectionQty.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.LblInspectionQty.Appearance.Options.UseFont = true;
            this.LblInspectionQty.Appearance.Options.UseForeColor = true;
            this.LblInspectionQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblInspectionQty.Location = new System.Drawing.Point(70, 26);
            this.LblInspectionQty.Name = "LblInspectionQty";
            this.LblInspectionQty.Size = new System.Drawing.Size(144, 46);
            this.LblInspectionQty.TabIndex = 1;
            this.LblInspectionQty.Text = "-";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.Location = new System.Drawing.Point(70, 9);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(116, 17);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "검사 건수";
            // 
            // chartExamResult
            // 
            xyDiagram8.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram8.AxisY.VisibleInPanesSerializable = "-1";
            this.chartExamResult.Diagram = xyDiagram8;
            this.chartExamResult.Location = new System.Drawing.Point(945, 301);
            this.chartExamResult.LookAndFeel.SkinName = "DevExpress Style";
            this.chartExamResult.Name = "chartExamResult";
            series11.Name = "Series 1";
            series11.SeriesID = 0;
            series11.View = lineSeriesView5;
            this.chartExamResult.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series11};
            this.chartExamResult.SeriesTemplate.View = lineSeriesView6;
            this.chartExamResult.Size = new System.Drawing.Size(456, 138);
            this.chartExamResult.TabIndex = 14;
            // 
            // chartNgType
            // 
            this.chartNgType.AppearanceNameSerializable = "Light";
            this.chartNgType.AutoLayout = false;
            this.chartNgType.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartNgType.Legend.Margins.Tag = "";
            this.chartNgType.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartNgType.Location = new System.Drawing.Point(945, 124);
            this.chartNgType.Name = "chartNgType";
            this.chartNgType.PaletteName = "In A Fog";
            series12.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series12.Name = "Series 1";
            series12.SeriesID = 1;
            doughnutSeriesView4.Rotation = 270;
            doughnutSeriesView4.TotalLabel.DXFont = new DevExpress.Drawing.DXFont("맑은 고딕", 12F);
            doughnutSeriesView4.TotalLabel.TextColor = System.Drawing.Color.Black;
            series12.View = doughnutSeriesView4;
            this.chartNgType.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series12};
            this.chartNgType.Size = new System.Drawing.Size(456, 136);
            this.chartNgType.TabIndex = 9;
            // 
            // chartNgByExam
            // 
            this.chartNgByExam.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram9.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram9.AxisY.VisibleInPanesSerializable = "-1";
            this.chartNgByExam.Diagram = xyDiagram9;
            this.chartNgByExam.Location = new System.Drawing.Point(4, 298);
            this.chartNgByExam.LookAndFeel.SkinName = "DevExpress Style";
            this.chartNgByExam.Name = "chartNgByExam";
            this.chartNgByExam.PaletteName = "Flow";
            series13.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series13.Name = "Series 1";
            series13.SeriesID = 0;
            sideBySideBarSeriesView6.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series13.View = sideBySideBarSeriesView6;
            this.chartNgByExam.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series13};
            this.chartNgByExam.Size = new System.Drawing.Size(461, 144);
            this.chartNgByExam.TabIndex = 17;
            // 
            // chartGoodRate
            // 
            xyDiagram10.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram10.AxisY.VisibleInPanesSerializable = "-1";
            this.chartGoodRate.Diagram = xyDiagram10;
            this.chartGoodRate.Location = new System.Drawing.Point(7, 124);
            this.chartGoodRate.LookAndFeel.SkinName = "DevExpress Style";
            this.chartGoodRate.Name = "chartGoodRate";
            series14.Name = "Series 1";
            series14.SeriesID = 0;
            series14.View = lineSeriesView7;
            this.chartGoodRate.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series14};
            this.chartGoodRate.SeriesTemplate.View = lineSeriesView8;
            this.chartGoodRate.Size = new System.Drawing.Size(455, 136);
            this.chartGoodRate.TabIndex = 14;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlItem6,
            this.layoutControlGroup2,
            this.layoutControlGroup9,
            this.layoutControlGroup7,
            this.layoutControlGroup6,
            this.layoutControlGroup11,
            this.layoutControlGroup5,
            this.layoutControlGroup8,
            this.layoutControlGroup12});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1408, 629);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "양품률 추이";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 90);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(469, 177);
            this.layoutControlGroup3.Text = "양품률 추이";
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.chartGoodRate;
            this.layoutControlItem12.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(461, 142);
            this.layoutControlItem12.Text = "layoutControlItem8";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.TPMain;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(0, 90);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(7, 90);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1408, 90);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(938, 90);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(470, 177);
            this.layoutControlGroup2.Text = "불량 유형별 현황";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chartNgType;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(462, 142);
            this.layoutControlItem3.Text = "layoutControlItem4";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 267);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Size = new System.Drawing.Size(469, 179);
            this.layoutControlGroup9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Text = "검사 단계별 판정 현황";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.chartNgByExam;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(467, 150);
            this.layoutControlItem9.Text = "layoutControlItem5";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 446);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(469, 183);
            this.layoutControlGroup7.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Text = "주요 부적합 TOP 5";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.Gc_DefectTop5;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(467, 154);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "불량률 (PPM) 추이";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup6.Location = new System.Drawing.Point(469, 90);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(469, 177);
            this.layoutControlGroup6.Text = "불량률 (PPM) 추이";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chartNgRate;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(461, 142);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup11
            // 
            this.layoutControlGroup11.CustomizationFormText = "불량 현황";
            this.layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup11.Location = new System.Drawing.Point(938, 267);
            this.layoutControlGroup11.Name = "layoutControlGroup11";
            this.layoutControlGroup11.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup11.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup11.Size = new System.Drawing.Size(470, 179);
            this.layoutControlGroup11.Text = "검사 현황";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.chartExamResult;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(462, 144);
            this.layoutControlItem7.Text = "layoutControlItem8";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup5.Location = new System.Drawing.Point(469, 267);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(469, 179);
            this.layoutControlGroup5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Text = "공정별 불량률 (PPM)";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chartNgRateByProcess;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(467, 150);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.CustomizationFormText = "최근 생산 지시 현황";
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.layoutControlGroup8.Location = new System.Drawing.Point(938, 446);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup8.Size = new System.Drawing.Size(470, 183);
            this.layoutControlGroup8.Text = "최근 부적합 발생 현황";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.Gc_WorkDefect;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(462, 148);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            this.layoutControlGroup12.CustomizationFormText = "생산 목표 달성 현황";
            this.layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.layoutControlGroup12.Location = new System.Drawing.Point(469, 446);
            this.layoutControlGroup12.Name = "layoutControlGroup12";
            this.layoutControlGroup12.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup12.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup12.Size = new System.Drawing.Size(469, 183);
            this.layoutControlGroup12.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup12.Text = "부적합 현황";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.chartDefectStatus;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(467, 154);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.LblCurTime);
            this.smtCommonBar1.Controls.Add(this.labelControl1);
            this.smtCommonBar1.Controls.Add(this.toggleSwitch1);
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(2, 2);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1408, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = null;
            this.smtCommonBar1.smtImageTitleText = "";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 294;
            // 
            // LblCurTime
            // 
            this.LblCurTime.Appearance.Options.UseTextOptions = true;
            this.LblCurTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblCurTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblCurTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblCurTime.Location = new System.Drawing.Point(995, 0);
            this.LblCurTime.Name = "LblCurTime";
            this.LblCurTime.Size = new System.Drawing.Size(158, 40);
            this.LblCurTime.TabIndex = 21;
            this.LblCurTime.Text = "2026-07-21 17:05:23";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelControl1.Location = new System.Drawing.Point(1153, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 40);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "자동갱신";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toggleSwitch1.EditValue = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(1236, 0);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.OffText = "Off";
            this.toggleSwitch1.Properties.OnText = "On";
            this.toggleSwitch1.Size = new System.Drawing.Size(95, 40);
            this.toggleSwitch1.TabIndex = 19;
            this.toggleSwitch1.Toggled += new System.EventHandler(this.toggleSwitch1_Toggled);
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
            this.btn_Close.Location = new System.Drawing.Point(1331, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(77, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1412, 673);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1412, 673);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Tm_Time
            // 
            this.Tm_Time.Interval = 1000;
            this.Tm_Time.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Tm_Search
            // 
            this.Tm_Search.Interval = 180000;
            this.Tm_Search.Tick += new System.EventHandler(this.Tm_Search_Tick);
            // 
            // layoutControlGroup10
            // 
            this.layoutControlGroup10.CustomizationFormText = "불량유형별 수량 (상위 10)";
            this.layoutControlGroup10.Location = new System.Drawing.Point(0, 680);
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup10.Size = new System.Drawing.Size(1761, 68);
            this.layoutControlGroup10.Text = "월별 매출 추이(최근 6개월)";
            // 
            // circularGauge13
            // 
            this.circularGauge13.AutoSize = DevExpress.Utils.DefaultBoolean.False;
            this.circularGauge13.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.arcScaleBackgroundLayerComponent1});
            this.circularGauge13.Bounds = new System.Drawing.Rectangle(7, 7, 439, 248);
            this.circularGauge13.Name = "circularGauge13";
            this.circularGauge13.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[] {
            this.arcScaleNeedleComponent1});
            this.circularGauge13.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.MachneComponent});
            // 
            // arcScaleBackgroundLayerComponent1
            // 
            this.arcScaleBackgroundLayerComponent1.ArcScale = this.MachneComponent;
            this.arcScaleBackgroundLayerComponent1.Name = "bg";
            this.arcScaleBackgroundLayerComponent1.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5F, 0.99F);
            this.arcScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style27;
            this.arcScaleBackgroundLayerComponent1.Size = new System.Drawing.SizeF(200F, 102F);
            this.arcScaleBackgroundLayerComponent1.ZOrder = 1000;
            // 
            // MachneComponent
            // 
            this.MachneComponent.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.MachneComponent.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.MachneComponent.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.MachneComponent.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.MachneComponent.AppearanceTickmarkText.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 10F);
            this.MachneComponent.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#999999");
            this.MachneComponent.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 159F);
            this.MachneComponent.EndAngle = 0F;
            this.MachneComponent.MajorTickCount = 6;
            this.MachneComponent.MajorTickmark.AllowTickOverlap = true;
            this.MachneComponent.MajorTickmark.FormatString = "{0:F0}";
            this.MachneComponent.MajorTickmark.ShapeOffset = -2F;
            this.MachneComponent.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
            this.MachneComponent.MajorTickmark.TextOffset = 18F;
            this.MachneComponent.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.MachneComponent.MaxValue = 100F;
            this.MachneComponent.MinorTickCount = 4;
            this.MachneComponent.MinorTickmark.ShapeOffset = 4F;
            this.MachneComponent.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
            this.MachneComponent.MinorTickmark.ShowTick = false;
            this.MachneComponent.Name = "scale1";
            this.MachneComponent.RadiusX = 101F;
            this.MachneComponent.RadiusY = 101F;
            arcScaleRange10.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange10.EndThickness = 22F;
            arcScaleRange10.EndValue = 33F;
            arcScaleRange10.Name = "Range0";
            arcScaleRange10.ShapeOffset = -4F;
            arcScaleRange10.StartThickness = 22F;
            arcScaleRange10.StartValue = -0.3F;
            arcScaleRange11.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange11.EndThickness = 22F;
            arcScaleRange11.EndValue = 66F;
            arcScaleRange11.Name = "Range1";
            arcScaleRange11.ShapeOffset = -4F;
            arcScaleRange11.StartThickness = 22F;
            arcScaleRange11.StartValue = 33F;
            arcScaleRange12.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange12.EndThickness = 22F;
            arcScaleRange12.EndValue = 100.3F;
            arcScaleRange12.Name = "Range2";
            arcScaleRange12.ShapeOffset = -4F;
            arcScaleRange12.StartThickness = 22F;
            arcScaleRange12.StartValue = 66F;
            this.MachneComponent.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange10,
            arcScaleRange11,
            arcScaleRange12});
            this.MachneComponent.StartAngle = -180F;
            this.MachneComponent.Value = 80F;
            // 
            // arcScaleNeedleComponent1
            // 
            this.arcScaleNeedleComponent1.ArcScale = this.MachneComponent;
            this.arcScaleNeedleComponent1.EndOffset = 8F;
            this.arcScaleNeedleComponent1.Name = "needle";
            this.arcScaleNeedleComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style27;
            this.arcScaleNeedleComponent1.StartOffset = -6F;
            this.arcScaleNeedleComponent1.ZOrder = -50;
            // 
            // circularGauge1
            // 
            this.circularGauge1.AutoSize = DevExpress.Utils.DefaultBoolean.False;
            this.circularGauge1.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.arcScaleBackgroundLayerComponent2});
            this.circularGauge1.Bounds = new System.Drawing.Rectangle(7, 7, 439, 248);
            this.circularGauge1.Name = "circularGauge1";
            this.circularGauge1.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[] {
            this.arcScaleNeedleComponent2});
            this.circularGauge1.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.arcScaleComponent1});
            // 
            // arcScaleBackgroundLayerComponent2
            // 
            this.arcScaleBackgroundLayerComponent2.ArcScale = this.arcScaleComponent1;
            this.arcScaleBackgroundLayerComponent2.Name = "bg";
            this.arcScaleBackgroundLayerComponent2.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5F, 0.99F);
            this.arcScaleBackgroundLayerComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style27;
            this.arcScaleBackgroundLayerComponent2.Size = new System.Drawing.SizeF(200F, 102F);
            this.arcScaleBackgroundLayerComponent2.ZOrder = 1000;
            // 
            // arcScaleComponent1
            // 
            this.arcScaleComponent1.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent1.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent1.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent1.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent1.AppearanceTickmarkText.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 10F);
            this.arcScaleComponent1.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#999999");
            this.arcScaleComponent1.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 159F);
            this.arcScaleComponent1.EndAngle = 0F;
            this.arcScaleComponent1.MajorTickCount = 6;
            this.arcScaleComponent1.MajorTickmark.AllowTickOverlap = true;
            this.arcScaleComponent1.MajorTickmark.FormatString = "{0:F0}";
            this.arcScaleComponent1.MajorTickmark.ShapeOffset = -2F;
            this.arcScaleComponent1.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
            this.arcScaleComponent1.MajorTickmark.TextOffset = 18F;
            this.arcScaleComponent1.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent1.MaxValue = 100F;
            this.arcScaleComponent1.MinorTickCount = 4;
            this.arcScaleComponent1.MinorTickmark.ShapeOffset = 4F;
            this.arcScaleComponent1.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
            this.arcScaleComponent1.MinorTickmark.ShowTick = false;
            this.arcScaleComponent1.Name = "scale1";
            this.arcScaleComponent1.RadiusX = 101F;
            this.arcScaleComponent1.RadiusY = 101F;
            arcScaleRange13.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange13.EndThickness = 22F;
            arcScaleRange13.EndValue = 33F;
            arcScaleRange13.Name = "Range0";
            arcScaleRange13.ShapeOffset = -4F;
            arcScaleRange13.StartThickness = 22F;
            arcScaleRange13.StartValue = -0.3F;
            arcScaleRange14.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange14.EndThickness = 22F;
            arcScaleRange14.EndValue = 66F;
            arcScaleRange14.Name = "Range1";
            arcScaleRange14.ShapeOffset = -4F;
            arcScaleRange14.StartThickness = 22F;
            arcScaleRange14.StartValue = 33F;
            arcScaleRange15.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange15.EndThickness = 22F;
            arcScaleRange15.EndValue = 100.3F;
            arcScaleRange15.Name = "Range2";
            arcScaleRange15.ShapeOffset = -4F;
            arcScaleRange15.StartThickness = 22F;
            arcScaleRange15.StartValue = 66F;
            this.arcScaleComponent1.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange13,
            arcScaleRange14,
            arcScaleRange15});
            this.arcScaleComponent1.StartAngle = -180F;
            this.arcScaleComponent1.Value = 80F;
            // 
            // arcScaleNeedleComponent2
            // 
            this.arcScaleNeedleComponent2.ArcScale = this.arcScaleComponent1;
            this.arcScaleNeedleComponent2.EndOffset = 8F;
            this.arcScaleNeedleComponent2.Name = "needle";
            this.arcScaleNeedleComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style27;
            this.arcScaleNeedleComponent2.StartOffset = -6F;
            this.arcScaleNeedleComponent2.ZOrder = -50;
            // 
            // circularGauge2
            // 
            this.circularGauge2.AutoSize = DevExpress.Utils.DefaultBoolean.False;
            this.circularGauge2.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.arcScaleBackgroundLayerComponent3});
            this.circularGauge2.Bounds = new System.Drawing.Rectangle(7, 7, 439, 248);
            this.circularGauge2.Name = "circularGauge2";
            this.circularGauge2.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[] {
            this.arcScaleNeedleComponent3});
            this.circularGauge2.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.arcScaleComponent2});
            // 
            // arcScaleBackgroundLayerComponent3
            // 
            this.arcScaleBackgroundLayerComponent3.ArcScale = this.arcScaleComponent2;
            this.arcScaleBackgroundLayerComponent3.Name = "bg";
            this.arcScaleBackgroundLayerComponent3.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5F, 0.99F);
            this.arcScaleBackgroundLayerComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style27;
            this.arcScaleBackgroundLayerComponent3.Size = new System.Drawing.SizeF(200F, 102F);
            this.arcScaleBackgroundLayerComponent3.ZOrder = 1000;
            // 
            // arcScaleComponent2
            // 
            this.arcScaleComponent2.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceTickmarkText.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 10F);
            this.arcScaleComponent2.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#999999");
            this.arcScaleComponent2.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 159F);
            this.arcScaleComponent2.EndAngle = 0F;
            this.arcScaleComponent2.MajorTickCount = 6;
            this.arcScaleComponent2.MajorTickmark.AllowTickOverlap = true;
            this.arcScaleComponent2.MajorTickmark.FormatString = "{0:F0}";
            this.arcScaleComponent2.MajorTickmark.ShapeOffset = -2F;
            this.arcScaleComponent2.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
            this.arcScaleComponent2.MajorTickmark.TextOffset = 18F;
            this.arcScaleComponent2.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent2.MaxValue = 100F;
            this.arcScaleComponent2.MinorTickCount = 4;
            this.arcScaleComponent2.MinorTickmark.ShapeOffset = 4F;
            this.arcScaleComponent2.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style27_1;
            this.arcScaleComponent2.MinorTickmark.ShowTick = false;
            this.arcScaleComponent2.Name = "scale1";
            this.arcScaleComponent2.RadiusX = 101F;
            this.arcScaleComponent2.RadiusY = 101F;
            arcScaleRange16.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange16.EndThickness = 22F;
            arcScaleRange16.EndValue = 33F;
            arcScaleRange16.Name = "Range0";
            arcScaleRange16.ShapeOffset = -4F;
            arcScaleRange16.StartThickness = 22F;
            arcScaleRange16.StartValue = -0.3F;
            arcScaleRange17.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange17.EndThickness = 22F;
            arcScaleRange17.EndValue = 66F;
            arcScaleRange17.Name = "Range1";
            arcScaleRange17.ShapeOffset = -4F;
            arcScaleRange17.StartThickness = 22F;
            arcScaleRange17.StartValue = 33F;
            arcScaleRange18.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange18.EndThickness = 22F;
            arcScaleRange18.EndValue = 100.3F;
            arcScaleRange18.Name = "Range2";
            arcScaleRange18.ShapeOffset = -4F;
            arcScaleRange18.StartThickness = 22F;
            arcScaleRange18.StartValue = 66F;
            this.arcScaleComponent2.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange16,
            arcScaleRange17,
            arcScaleRange18});
            this.arcScaleComponent2.StartAngle = -180F;
            this.arcScaleComponent2.Value = 80F;
            // 
            // arcScaleNeedleComponent3
            // 
            this.arcScaleNeedleComponent3.ArcScale = this.arcScaleComponent2;
            this.arcScaleNeedleComponent3.EndOffset = 8F;
            this.arcScaleNeedleComponent3.Name = "needle";
            this.arcScaleNeedleComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style27;
            this.arcScaleNeedleComponent3.StartOffset = -6F;
            this.arcScaleNeedleComponent3.ZOrder = -50;
            // 
            // Frm_MM_QualityDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 673);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_MM_QualityDashboard";
            this.Text = "품질 대시보드";
            this.Load += new System.EventHandler(this.Frm_MM_QualityDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_DefectTop5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_DefectTop5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNgRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNgRateByProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDefectStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_WorkDefect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_WorkDefect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar121)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).EndInit();
            this.TPMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl9)).EndInit();
            this.panelControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).EndInit();
            this.panelControl12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl5)).EndInit();
            this.panelControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartExamResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNgType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartNgByExam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartGoodRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MachneComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent3)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private System.Windows.Forms.Timer Tm_Time;
		private System.Windows.Forms.Timer Tm_Search;
		private PanelControl panelControl1;
		private LayoutControlItem layoutControlItem1;
		private baseSMT.smtCommonBar smtCommonBar1;
		private baseSMT.smtSearchButton btn_Close;
		private LayoutControl layoutControl2;
		private LayoutControlGroup layoutControlGroup1;
		private TablePanel TPMain;
		private PanelControl panelControl9;
		private LabelControl LblNgQty;
		private LabelControl labelControl15;
		private PanelControl panelControl12;
		private LabelControl LblGoodRate;
		private LabelControl labelControl111;
		private PanelControl panelControl7;
		private LabelControl LblRework;
		private LabelControl labelControl12;
		private PanelControl panelControl3;
		private LabelControl LblNgRate;
		private LabelControl labelControl6;
		private PanelControl panelControl5;
		private LabelControl LblInspectionQty;
		private LabelControl labelControl9;
		private LayoutControlGroup layoutControlGroup3;
		private LayoutControlGroup layoutControlGroup6;
		private LayoutControlItem layoutControlItem6;
		private SvgImageBox svgImageBox1;
		private SvgImageBox svgImageBox2;
		private SvgImageBox svgImageBox3;
		private SvgImageBox svgImageBox4;
		private SvgImageBox svgImageBox6;
		private LayoutControlGroup layoutControlGroup2;
		private LabelControl LblCurTime;
		private LayoutControlGroup layoutControlGroup5;
		private LayoutControlGroup layoutControlGroup9;
		private LayoutControlGroup layoutControlGroup7;
        private LabelControl labelControl1;
        private ToggleSwitch toggleSwitch1;
        private LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraCharts.ChartControl chartExamResult;
        private LayoutControlGroup layoutControlGroup11;
        private LayoutControlItem layoutControlItem7;
        private LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraCharts.ChartControl chartDefectStatus;
        private DevExpress.XtraGrid.GridControl Gc_WorkDefect;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_WorkDefect;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar121;
        private LayoutControlGroup layoutControlGroup8;
        private LayoutControlItem layoutControlItem8;
        private LayoutControlItem layoutControlItem11;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge13;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent MachneComponent;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent3;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent3;
        private DevExpress.XtraCharts.ChartControl chartNgRateByProcess;
        private LayoutControlItem layoutControlItem5;
        private DevExpress.XtraCharts.ChartControl chartNgType;
        private LayoutControlItem layoutControlItem3;
        private DevExpress.XtraCharts.ChartControl chartNgByExam;
        private LayoutControlItem layoutControlItem9;
        private DevExpress.XtraCharts.ChartControl chartNgRate;
        private DevExpress.XtraCharts.ChartControl chartGoodRate;
        private LayoutControlItem layoutControlItem12;
        private LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.GridControl Gc_DefectTop5;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_DefectTop5;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private LayoutControlItem layoutControlItem4;
    }
}