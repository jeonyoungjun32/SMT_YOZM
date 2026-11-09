
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.Management
{
	partial class Frm_MM_Dashboard_Total
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
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange7 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange8 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange9 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange10 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange11 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange12 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DColumn chartControlCommandGalleryItemGroup2DColumn2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DColumn();
            DevExpress.XtraCharts.UI.CreateBarChartItem createBarChartItem2 = new DevExpress.XtraCharts.UI.CreateBarChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedBarChartItem createFullStackedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedBarChartItem();
            DevExpress.XtraCharts.UI.CreateSideBySideFullStackedBarChartItem createSideBySideFullStackedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateSideBySideFullStackedBarChartItem();
            DevExpress.XtraCharts.UI.CreateSideBySideStackedBarChartItem createSideBySideStackedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateSideBySideStackedBarChartItem();
            DevExpress.XtraCharts.UI.CreateStackedBarChartItem createStackedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedBarChartItem();
            DevExpress.XtraCharts.UI.CreateWaterfallChartItem createWaterfallChartItem2 = new DevExpress.XtraCharts.UI.CreateWaterfallChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup3DColumn chartControlCommandGalleryItemGroup3DColumn2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup3DColumn();
            DevExpress.XtraCharts.UI.CreateBar3DChartItem createBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedBar3DChartItem createFullStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateManhattanBarChartItem createManhattanBarChartItem2 = new DevExpress.XtraCharts.UI.CreateManhattanBarChartItem();
            DevExpress.XtraCharts.UI.CreateSideBySideFullStackedBar3DChartItem createSideBySideFullStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateSideBySideFullStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateSideBySideStackedBar3DChartItem createSideBySideStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateSideBySideStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateStackedBar3DChartItem createStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupCylinderColumn chartControlCommandGalleryItemGroupCylinderColumn2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupCylinderColumn();
            DevExpress.XtraCharts.UI.CreateCylinderBar3DChartItem createCylinderBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateCylinderBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateCylinderFullStackedBar3DChartItem createCylinderFullStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateCylinderFullStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateCylinderManhattanBarChartItem createCylinderManhattanBarChartItem2 = new DevExpress.XtraCharts.UI.CreateCylinderManhattanBarChartItem();
            DevExpress.XtraCharts.UI.CreateCylinderSideBySideFullStackedBar3DChartItem createCylinderSideBySideFullStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateCylinderSideBySideFullStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateCylinderSideBySideStackedBar3DChartItem createCylinderSideBySideStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateCylinderSideBySideStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateCylinderStackedBar3DChartItem createCylinderStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateCylinderStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupConeColumn chartControlCommandGalleryItemGroupConeColumn2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupConeColumn();
            DevExpress.XtraCharts.UI.CreateConeBar3DChartItem createConeBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateConeBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateConeFullStackedBar3DChartItem createConeFullStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateConeFullStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateConeManhattanBarChartItem createConeManhattanBarChartItem2 = new DevExpress.XtraCharts.UI.CreateConeManhattanBarChartItem();
            DevExpress.XtraCharts.UI.CreateConeSideBySideFullStackedBar3DChartItem createConeSideBySideFullStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateConeSideBySideFullStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateConeSideBySideStackedBar3DChartItem createConeSideBySideStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateConeSideBySideStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreateConeStackedBar3DChartItem createConeStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreateConeStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupPyramidColumn chartControlCommandGalleryItemGroupPyramidColumn2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupPyramidColumn();
            DevExpress.XtraCharts.UI.CreatePyramidBar3DChartItem createPyramidBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreatePyramidBar3DChartItem();
            DevExpress.XtraCharts.UI.CreatePyramidFullStackedBar3DChartItem createPyramidFullStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreatePyramidFullStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreatePyramidManhattanBarChartItem createPyramidManhattanBarChartItem2 = new DevExpress.XtraCharts.UI.CreatePyramidManhattanBarChartItem();
            DevExpress.XtraCharts.UI.CreatePyramidSideBySideFullStackedBar3DChartItem createPyramidSideBySideFullStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreatePyramidSideBySideFullStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreatePyramidSideBySideStackedBar3DChartItem createPyramidSideBySideStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreatePyramidSideBySideStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.CreatePyramidStackedBar3DChartItem createPyramidStackedBar3DChartItem2 = new DevExpress.XtraCharts.UI.CreatePyramidStackedBar3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DLine chartControlCommandGalleryItemGroup2DLine2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DLine();
            DevExpress.XtraCharts.UI.CreateLineChartItem createLineChartItem2 = new DevExpress.XtraCharts.UI.CreateLineChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedLineChartItem createFullStackedLineChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedLineChartItem();
            DevExpress.XtraCharts.UI.CreateScatterLineChartItem createScatterLineChartItem2 = new DevExpress.XtraCharts.UI.CreateScatterLineChartItem();
            DevExpress.XtraCharts.UI.CreateSplineChartItem createSplineChartItem2 = new DevExpress.XtraCharts.UI.CreateSplineChartItem();
            DevExpress.XtraCharts.UI.CreateStackedLineChartItem createStackedLineChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedLineChartItem();
            DevExpress.XtraCharts.UI.CreateStepLineChartItem createStepLineChartItem2 = new DevExpress.XtraCharts.UI.CreateStepLineChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup3DLine chartControlCommandGalleryItemGroup3DLine2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup3DLine();
            DevExpress.XtraCharts.UI.CreateLine3DChartItem createLine3DChartItem2 = new DevExpress.XtraCharts.UI.CreateLine3DChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedLine3DChartItem createFullStackedLine3DChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedLine3DChartItem();
            DevExpress.XtraCharts.UI.CreateSpline3DChartItem createSpline3DChartItem2 = new DevExpress.XtraCharts.UI.CreateSpline3DChartItem();
            DevExpress.XtraCharts.UI.CreateStackedLine3DChartItem createStackedLine3DChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedLine3DChartItem();
            DevExpress.XtraCharts.UI.CreateStepLine3DChartItem createStepLine3DChartItem2 = new DevExpress.XtraCharts.UI.CreateStepLine3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DPie chartControlCommandGalleryItemGroup2DPie2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DPie();
            DevExpress.XtraCharts.UI.CreatePieChartItem createPieChartItem2 = new DevExpress.XtraCharts.UI.CreatePieChartItem();
            DevExpress.XtraCharts.UI.CreateDoughnutChartItem createDoughnutChartItem2 = new DevExpress.XtraCharts.UI.CreateDoughnutChartItem();
            DevExpress.XtraCharts.UI.CreateNestedDoughnutChartItem createNestedDoughnutChartItem2 = new DevExpress.XtraCharts.UI.CreateNestedDoughnutChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup3DPie chartControlCommandGalleryItemGroup3DPie2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup3DPie();
            DevExpress.XtraCharts.UI.CreatePie3DChartItem createPie3DChartItem2 = new DevExpress.XtraCharts.UI.CreatePie3DChartItem();
            DevExpress.XtraCharts.UI.CreateDoughnut3DChartItem createDoughnut3DChartItem2 = new DevExpress.XtraCharts.UI.CreateDoughnut3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DBar chartControlCommandGalleryItemGroup2DBar2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DBar();
            DevExpress.XtraCharts.UI.CreateRotatedBarChartItem createRotatedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateRotatedBarChartItem();
            DevExpress.XtraCharts.UI.CreateRotatedFullStackedBarChartItem createRotatedFullStackedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateRotatedFullStackedBarChartItem();
            DevExpress.XtraCharts.UI.CreateRotatedSideBySideFullStackedBarChartItem createRotatedSideBySideFullStackedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateRotatedSideBySideFullStackedBarChartItem();
            DevExpress.XtraCharts.UI.CreateRotatedSideBySideStackedBarChartItem createRotatedSideBySideStackedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateRotatedSideBySideStackedBarChartItem();
            DevExpress.XtraCharts.UI.CreateRotatedStackedBarChartItem createRotatedStackedBarChartItem2 = new DevExpress.XtraCharts.UI.CreateRotatedStackedBarChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DArea chartControlCommandGalleryItemGroup2DArea2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup2DArea();
            DevExpress.XtraCharts.UI.CreateAreaChartItem createAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateAreaChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedAreaChartItem createFullStackedAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedAreaChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedSplineAreaChartItem createFullStackedSplineAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedSplineAreaChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedStepAreaChartItem createFullStackedStepAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedStepAreaChartItem();
            DevExpress.XtraCharts.UI.CreateSplineAreaChartItem createSplineAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateSplineAreaChartItem();
            DevExpress.XtraCharts.UI.CreateStackedAreaChartItem createStackedAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedAreaChartItem();
            DevExpress.XtraCharts.UI.CreateStackedStepAreaChartItem createStackedStepAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedStepAreaChartItem();
            DevExpress.XtraCharts.UI.CreateStackedSplineAreaChartItem createStackedSplineAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedSplineAreaChartItem();
            DevExpress.XtraCharts.UI.CreateStepAreaChartItem createStepAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateStepAreaChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup3DArea chartControlCommandGalleryItemGroup3DArea2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroup3DArea();
            DevExpress.XtraCharts.UI.CreateArea3DChartItem createArea3DChartItem2 = new DevExpress.XtraCharts.UI.CreateArea3DChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedArea3DChartItem createFullStackedArea3DChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedArea3DChartItem();
            DevExpress.XtraCharts.UI.CreateFullStackedSplineArea3DChartItem createFullStackedSplineArea3DChartItem2 = new DevExpress.XtraCharts.UI.CreateFullStackedSplineArea3DChartItem();
            DevExpress.XtraCharts.UI.CreateSplineArea3DChartItem createSplineArea3DChartItem2 = new DevExpress.XtraCharts.UI.CreateSplineArea3DChartItem();
            DevExpress.XtraCharts.UI.CreateStackedArea3DChartItem createStackedArea3DChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedArea3DChartItem();
            DevExpress.XtraCharts.UI.CreateStackedSplineArea3DChartItem createStackedSplineArea3DChartItem2 = new DevExpress.XtraCharts.UI.CreateStackedSplineArea3DChartItem();
            DevExpress.XtraCharts.UI.CreateStepArea3DChartItem createStepArea3DChartItem2 = new DevExpress.XtraCharts.UI.CreateStepArea3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupPoint chartControlCommandGalleryItemGroupPoint2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupPoint();
            DevExpress.XtraCharts.UI.CreatePointChartItem createPointChartItem2 = new DevExpress.XtraCharts.UI.CreatePointChartItem();
            DevExpress.XtraCharts.UI.CreateBubbleChartItem createBubbleChartItem2 = new DevExpress.XtraCharts.UI.CreateBubbleChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupFunnel chartControlCommandGalleryItemGroupFunnel2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupFunnel();
            DevExpress.XtraCharts.UI.CreateFunnelChartItem createFunnelChartItem2 = new DevExpress.XtraCharts.UI.CreateFunnelChartItem();
            DevExpress.XtraCharts.UI.CreateFunnel3DChartItem createFunnel3DChartItem2 = new DevExpress.XtraCharts.UI.CreateFunnel3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupFinancial chartControlCommandGalleryItemGroupFinancial2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupFinancial();
            DevExpress.XtraCharts.UI.CreateStockChartItem createStockChartItem2 = new DevExpress.XtraCharts.UI.CreateStockChartItem();
            DevExpress.XtraCharts.UI.CreateCandleStickChartItem createCandleStickChartItem2 = new DevExpress.XtraCharts.UI.CreateCandleStickChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupRadar chartControlCommandGalleryItemGroupRadar2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupRadar();
            DevExpress.XtraCharts.UI.CreateRadarPointChartItem createRadarPointChartItem2 = new DevExpress.XtraCharts.UI.CreateRadarPointChartItem();
            DevExpress.XtraCharts.UI.CreateRadarLineChartItem createRadarLineChartItem2 = new DevExpress.XtraCharts.UI.CreateRadarLineChartItem();
            DevExpress.XtraCharts.UI.CreateRadarAreaChartItem createRadarAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateRadarAreaChartItem();
            DevExpress.XtraCharts.UI.CreateRadarRangeAreaChartItem createRadarRangeAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateRadarRangeAreaChartItem();
            DevExpress.XtraCharts.UI.CreateScatterRadarLineChartItem createScatterRadarLineChartItem2 = new DevExpress.XtraCharts.UI.CreateScatterRadarLineChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupPolar chartControlCommandGalleryItemGroupPolar2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupPolar();
            DevExpress.XtraCharts.UI.CreatePolarPointChartItem createPolarPointChartItem2 = new DevExpress.XtraCharts.UI.CreatePolarPointChartItem();
            DevExpress.XtraCharts.UI.CreatePolarLineChartItem createPolarLineChartItem2 = new DevExpress.XtraCharts.UI.CreatePolarLineChartItem();
            DevExpress.XtraCharts.UI.CreatePolarAreaChartItem createPolarAreaChartItem2 = new DevExpress.XtraCharts.UI.CreatePolarAreaChartItem();
            DevExpress.XtraCharts.UI.CreatePolarRangeAreaChartItem createPolarRangeAreaChartItem2 = new DevExpress.XtraCharts.UI.CreatePolarRangeAreaChartItem();
            DevExpress.XtraCharts.UI.CreateScatterPolarLineChartItem createScatterPolarLineChartItem2 = new DevExpress.XtraCharts.UI.CreateScatterPolarLineChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupRange chartControlCommandGalleryItemGroupRange2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupRange();
            DevExpress.XtraCharts.UI.CreateRangeBarChartItem createRangeBarChartItem2 = new DevExpress.XtraCharts.UI.CreateRangeBarChartItem();
            DevExpress.XtraCharts.UI.CreateSideBySideRangeBarChartItem createSideBySideRangeBarChartItem2 = new DevExpress.XtraCharts.UI.CreateSideBySideRangeBarChartItem();
            DevExpress.XtraCharts.UI.CreateRangeAreaChartItem createRangeAreaChartItem2 = new DevExpress.XtraCharts.UI.CreateRangeAreaChartItem();
            DevExpress.XtraCharts.UI.CreateRangeArea3DChartItem createRangeArea3DChartItem2 = new DevExpress.XtraCharts.UI.CreateRangeArea3DChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupGantt chartControlCommandGalleryItemGroupGantt2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupGantt();
            DevExpress.XtraCharts.UI.CreateGanttChartItem createGanttChartItem2 = new DevExpress.XtraCharts.UI.CreateGanttChartItem();
            DevExpress.XtraCharts.UI.CreateSideBySideGanttChartItem createSideBySideGanttChartItem2 = new DevExpress.XtraCharts.UI.CreateSideBySideGanttChartItem();
            DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupBoxPlot chartControlCommandGalleryItemGroupBoxPlot2 = new DevExpress.XtraCharts.UI.ChartControlCommandGalleryItemGroupBoxPlot();
            DevExpress.XtraCharts.UI.CreateBoxPlotChartItem createBoxPlotChartItem2 = new DevExpress.XtraCharts.UI.CreateBoxPlotChartItem();
            DevExpress.XtraGauges.Core.Model.ImageIndicatorState imageIndicatorState2 = new DevExpress.XtraGauges.Core.Model.ImageIndicatorState();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chartSales = new DevExpress.XtraCharts.ChartControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.progressInRate = new DevExpress.XtraEditors.ProgressBarControl();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblOutCount = new DevExpress.XtraEditors.LabelControl();
            this.lblInCount = new DevExpress.XtraEditors.LabelControl();
            this.lblOrderCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblGoodRate = new DevExpress.XtraEditors.LabelControl();
            this.lblNgRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.Chart_QM = new DevExpress.XtraCharts.ChartControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_TotalQty = new DevExpress.XtraEditors.LabelControl();
            this.lbl_AchieveRate = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalQty = new DevExpress.XtraEditors.LabelControl();
            this.lblAchieveRate = new DevExpress.XtraEditors.LabelControl();
            this.ChartPM = new DevExpress.XtraCharts.ChartControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblEquipOEE = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl8 = new DevExpress.XtraEditors.PanelControl();
            this.gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
            this.circularGauge2 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.lblMachineRate = new DevExpress.XtraGauges.Win.Base.LabelComponent();
            this.arcScaleRangeBarComponent4 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent();
            this.arcScaleComponent4 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.lblRunHour = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl11 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.GcST = new DevExpress.XtraGrid.GridControl();
            this.GvST = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.circularGauge13 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.MachneComponent = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.circularGauge1 = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.arcScaleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.arcScaleRangeBarComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent();
            this.arcScaleMarkerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleMarkerComponent();
            this.arcScaleRangeBarComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent();
            this.commandBarGalleryDropDown1 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown2 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown3 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown4 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown5 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown6 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown7 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.commandBarGalleryDropDown8 = new DevExpress.XtraBars.Commands.CommandBarGalleryDropDown(this.components);
            this.arcScaleRangeBarComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent();
            this.arcScaleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.lblMaintainRate = new DevExpress.XtraGauges.Win.Base.LabelComponent();
            this.stateImageIndicatorComponent1 = new DevExpress.XtraGauges.Win.Base.StateImageIndicatorComponent();
            this.arcScaleEffectLayerComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleEffectLayerComponent();
            this.arcScaleEffectLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleEffectLayerComponent();
            this.arcScaleBackgroundLayerComponent4 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.arcScaleBackgroundLayerComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.arcScaleComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressInRate.Properties)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_QM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartPM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl8)).BeginInit();
            this.panelControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMachineRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl11)).BeginInit();
            this.panelControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GcST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MachneComponent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleMarkerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMaintainRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateImageIndicatorComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleEffectLayerComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleEffectLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chartSales);
            this.layoutControl1.Controls.Add(this.panelControl4);
            this.layoutControl1.Controls.Add(this.tableLayoutPanel3);
            this.layoutControl1.Controls.Add(this.panelControl3);
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.panelControl8);
            this.layoutControl1.Controls.Add(this.panelControl11);
            this.layoutControl1.Controls.Add(this.GcST);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(810, 428, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1412, 761);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chartSales
            // 
            this.chartSales.AppearanceNameSerializable = "Light";
            this.chartSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.chartSales.BorderOptions.Thickness = 4;
            this.chartSales.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartSales.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.chartSales.Location = new System.Drawing.Point(538, 541);
            this.chartSales.Margin = new System.Windows.Forms.Padding(0);
            this.chartSales.Name = "chartSales";
            this.chartSales.PaletteBaseColorNumber = 1;
            this.chartSales.PaletteName = "Blue II";
            this.chartSales.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartSales.SeriesTemplate.View = lineSeriesView2;
            this.chartSales.Size = new System.Drawing.Size(411, 212);
            this.chartSales.TabIndex = 20;
            // 
            // panelControl4
            // 
            this.panelControl4.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl4.Appearance.Options.UseBackColor = true;
            this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl4.Controls.Add(this.labelControl14);
            this.panelControl4.Controls.Add(this.progressInRate);
            this.panelControl4.Location = new System.Drawing.Point(538, 412);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(411, 32);
            this.panelControl4.TabIndex = 19;
            // 
            // labelControl14
            // 
            this.labelControl14.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl14.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 13F);
            this.labelControl14.Appearance.Options.UseBackColor = true;
            this.labelControl14.Appearance.Options.UseFont = true;
            this.labelControl14.Appearance.Options.UseTextOptions = true;
            this.labelControl14.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl14.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl14.Location = new System.Drawing.Point(0, 0);
            this.labelControl14.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(87, 32);
            this.labelControl14.TabIndex = 10;
            this.labelControl14.Text = "입고율";
            // 
            // progressInRate
            // 
            this.progressInRate.Dock = System.Windows.Forms.DockStyle.Right;
            this.progressInRate.EditValue = 75;
            this.progressInRate.Location = new System.Drawing.Point(87, 0);
            this.progressInRate.Margin = new System.Windows.Forms.Padding(0);
            this.progressInRate.Name = "progressInRate";
            this.progressInRate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.progressInRate.Properties.ProgressPadding = new System.Windows.Forms.Padding(7);
            this.progressInRate.Properties.ShowTitle = true;
            this.progressInRate.Size = new System.Drawing.Size(324, 32);
            this.progressInRate.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.lblOutCount, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblInCount, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblOrderCount, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelControl10, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelControl3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(538, 444);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(411, 97);
            this.tableLayoutPanel3.TabIndex = 18;
            // 
            // lblOutCount
            // 
            this.lblOutCount.Appearance.BackColor = System.Drawing.Color.White;
            this.lblOutCount.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 25F);
            this.lblOutCount.Appearance.Options.UseBackColor = true;
            this.lblOutCount.Appearance.Options.UseFont = true;
            this.lblOutCount.Appearance.Options.UseTextOptions = true;
            this.lblOutCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblOutCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblOutCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutCount.Location = new System.Drawing.Point(274, 38);
            this.lblOutCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblOutCount.Name = "lblOutCount";
            this.lblOutCount.Size = new System.Drawing.Size(137, 59);
            this.lblOutCount.TabIndex = 20;
            this.lblOutCount.Text = "913";
            // 
            // lblInCount
            // 
            this.lblInCount.Appearance.BackColor = System.Drawing.Color.White;
            this.lblInCount.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 25F);
            this.lblInCount.Appearance.Options.UseBackColor = true;
            this.lblInCount.Appearance.Options.UseFont = true;
            this.lblInCount.Appearance.Options.UseTextOptions = true;
            this.lblInCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblInCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInCount.Location = new System.Drawing.Point(137, 38);
            this.lblInCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblInCount.Name = "lblInCount";
            this.lblInCount.Size = new System.Drawing.Size(137, 59);
            this.lblInCount.TabIndex = 19;
            this.lblInCount.Text = "913";
            // 
            // lblOrderCount
            // 
            this.lblOrderCount.Appearance.BackColor = System.Drawing.Color.White;
            this.lblOrderCount.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 25F);
            this.lblOrderCount.Appearance.Options.UseBackColor = true;
            this.lblOrderCount.Appearance.Options.UseFont = true;
            this.lblOrderCount.Appearance.Options.UseTextOptions = true;
            this.lblOrderCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblOrderCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblOrderCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrderCount.Location = new System.Drawing.Point(0, 38);
            this.lblOrderCount.Margin = new System.Windows.Forms.Padding(0);
            this.lblOrderCount.Name = "lblOrderCount";
            this.lblOrderCount.Size = new System.Drawing.Size(137, 59);
            this.lblOrderCount.TabIndex = 9;
            this.lblOrderCount.Text = "913";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl10.Appearance.Options.UseBackColor = true;
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Appearance.Options.UseForeColor = true;
            this.labelControl10.Appearance.Options.UseTextOptions = true;
            this.labelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl10.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl10.Location = new System.Drawing.Point(274, 0);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(137, 38);
            this.labelControl10.TabIndex = 18;
            this.labelControl10.Text = "출고 건수";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl3.Location = new System.Drawing.Point(137, 0);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(137, 38);
            this.labelControl3.TabIndex = 17;
            this.labelControl3.Text = "입고 건수";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(137, 38);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "발주 건수";
            // 
            // panelControl3
            // 
            this.panelControl3.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl3.Appearance.Options.UseBackColor = true;
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.tableLayoutPanel1);
            this.panelControl3.Controls.Add(this.labelControl9);
            this.panelControl3.Controls.Add(this.Chart_QM);
            this.panelControl3.Location = new System.Drawing.Point(7, 384);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(516, 370);
            this.panelControl3.TabIndex = 17;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblGoodRate, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblNgRate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelControl7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelControl8, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(516, 91);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // lblGoodRate
            // 
            this.lblGoodRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 25F);
            this.lblGoodRate.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblGoodRate.Appearance.Options.UseFont = true;
            this.lblGoodRate.Appearance.Options.UseForeColor = true;
            this.lblGoodRate.Appearance.Options.UseTextOptions = true;
            this.lblGoodRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblGoodRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblGoodRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGoodRate.Location = new System.Drawing.Point(0, 25);
            this.lblGoodRate.Margin = new System.Windows.Forms.Padding(0);
            this.lblGoodRate.Name = "lblGoodRate";
            this.tableLayoutPanel1.SetRowSpan(this.lblGoodRate, 3);
            this.lblGoodRate.Size = new System.Drawing.Size(258, 66);
            this.lblGoodRate.TabIndex = 12;
            this.lblGoodRate.Text = "54.33%";
            // 
            // lblNgRate
            // 
            this.lblNgRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 25F);
            this.lblNgRate.Appearance.ForeColor = System.Drawing.Color.Firebrick;
            this.lblNgRate.Appearance.Options.UseFont = true;
            this.lblNgRate.Appearance.Options.UseForeColor = true;
            this.lblNgRate.Appearance.Options.UseTextOptions = true;
            this.lblNgRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblNgRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNgRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgRate.Location = new System.Drawing.Point(258, 25);
            this.lblNgRate.Margin = new System.Windows.Forms.Padding(0);
            this.lblNgRate.Name = "lblNgRate";
            this.tableLayoutPanel1.SetRowSpan(this.lblNgRate, 3);
            this.lblNgRate.Size = new System.Drawing.Size(258, 66);
            this.lblNgRate.TabIndex = 13;
            this.lblNgRate.Text = "82.2%";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Appearance.Options.UseTextOptions = true;
            this.labelControl7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl7.Location = new System.Drawing.Point(0, 0);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(258, 25);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "양품률 (당월)";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl8.Appearance.Options.UseFont = true;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Appearance.Options.UseTextOptions = true;
            this.labelControl8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl8.Location = new System.Drawing.Point(258, 0);
            this.labelControl8.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(258, 25);
            this.labelControl8.TabIndex = 15;
            this.labelControl8.Text = "불량률 (당월)";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 10F);
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl9.Appearance.Options.UseFont = true;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl9.Location = new System.Drawing.Point(0, 91);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.labelControl9.Size = new System.Drawing.Size(516, 27);
            this.labelControl9.TabIndex = 16;
            this.labelControl9.Text = "품목별 불량 WORST TOP 5 (당월) ";
            // 
            // Chart_QM
            // 
            this.Chart_QM.BackColor = System.Drawing.Color.White;
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram3.Rotated = true;
            this.Chart_QM.Diagram = xyDiagram3;
            this.Chart_QM.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Chart_QM.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.Chart_QM.Location = new System.Drawing.Point(0, 118);
            this.Chart_QM.Name = "Chart_QM";
            this.Chart_QM.PaletteBaseColorNumber = 1;
            this.Chart_QM.PaletteName = "Red";
            series3.Name = "Series 1";
            series3.SeriesID = 0;
            this.Chart_QM.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.Chart_QM.Size = new System.Drawing.Size(516, 252);
            this.Chart_QM.TabIndex = 11;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.tableLayoutPanel2);
            this.panelControl2.Controls.Add(this.ChartPM);
            this.panelControl2.Location = new System.Drawing.Point(7, 68);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(516, 275);
            this.panelControl2.TabIndex = 16;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lbl_TotalQty, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_AchieveRate, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalQty, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblAchieveRate, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 207);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.37037F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.62963F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(516, 68);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // lbl_TotalQty
            // 
            this.lbl_TotalQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.lbl_TotalQty.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_TotalQty.Appearance.Options.UseFont = true;
            this.lbl_TotalQty.Appearance.Options.UseForeColor = true;
            this.lbl_TotalQty.Appearance.Options.UseTextOptions = true;
            this.lbl_TotalQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_TotalQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_TotalQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TotalQty.Location = new System.Drawing.Point(3, 2);
            this.lbl_TotalQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbl_TotalQty.Name = "lbl_TotalQty";
            this.lbl_TotalQty.Size = new System.Drawing.Size(252, 15);
            this.lbl_TotalQty.TabIndex = 10;
            this.lbl_TotalQty.Text = "합계";
            // 
            // lbl_AchieveRate
            // 
            this.lbl_AchieveRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.lbl_AchieveRate.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lbl_AchieveRate.Appearance.Options.UseFont = true;
            this.lbl_AchieveRate.Appearance.Options.UseForeColor = true;
            this.lbl_AchieveRate.Appearance.Options.UseTextOptions = true;
            this.lbl_AchieveRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbl_AchieveRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl_AchieveRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_AchieveRate.Location = new System.Drawing.Point(261, 2);
            this.lbl_AchieveRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbl_AchieveRate.Name = "lbl_AchieveRate";
            this.lbl_AchieveRate.Size = new System.Drawing.Size(252, 15);
            this.lbl_AchieveRate.TabIndex = 7;
            this.lbl_AchieveRate.Text = "실시간 달성율";
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 25F);
            this.lblTotalQty.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblTotalQty.Appearance.Options.UseFont = true;
            this.lblTotalQty.Appearance.Options.UseForeColor = true;
            this.lblTotalQty.Appearance.Options.UseTextOptions = true;
            this.lblTotalQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTotalQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTotalQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalQty.Location = new System.Drawing.Point(3, 21);
            this.lblTotalQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTotalQty.Name = "lblTotalQty";
            this.tableLayoutPanel2.SetRowSpan(this.lblTotalQty, 3);
            this.lblTotalQty.Size = new System.Drawing.Size(252, 45);
            this.lblTotalQty.TabIndex = 8;
            this.lblTotalQty.Text = "54,223";
            // 
            // lblAchieveRate
            // 
            this.lblAchieveRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 25F);
            this.lblAchieveRate.Appearance.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblAchieveRate.Appearance.Options.UseFont = true;
            this.lblAchieveRate.Appearance.Options.UseForeColor = true;
            this.lblAchieveRate.Appearance.Options.UseTextOptions = true;
            this.lblAchieveRate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblAchieveRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAchieveRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAchieveRate.Location = new System.Drawing.Point(261, 21);
            this.lblAchieveRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblAchieveRate.Name = "lblAchieveRate";
            this.tableLayoutPanel2.SetRowSpan(this.lblAchieveRate, 3);
            this.lblAchieveRate.Size = new System.Drawing.Size(252, 45);
            this.lblAchieveRate.TabIndex = 9;
            this.lblAchieveRate.Text = "112.2%";
            // 
            // ChartPM
            // 
            this.ChartPM.AppearanceNameSerializable = "Light";
            this.ChartPM.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram4.Rotated = true;
            this.ChartPM.Diagram = xyDiagram4;
            this.ChartPM.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChartPM.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.ChartPM.Location = new System.Drawing.Point(0, 0);
            this.ChartPM.Name = "ChartPM";
            this.ChartPM.PaletteBaseColorNumber = 1;
            this.ChartPM.PaletteName = "Flow";
            series4.Name = "Series 1";
            series4.SeriesID = 0;
            this.ChartPM.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series4};
            this.ChartPM.Size = new System.Drawing.Size(516, 207);
            this.ChartPM.TabIndex = 11;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.tableLayoutPanel4);
            this.panelControl1.Controls.Add(this.lblEquipOEE);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Location = new System.Drawing.Point(961, 68);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(444, 275);
            this.panelControl1.TabIndex = 15;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 115);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(444, 160);
            this.tableLayoutPanel4.TabIndex = 9;
            // 
            // lblEquipOEE
            // 
            this.lblEquipOEE.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 35F);
            this.lblEquipOEE.Appearance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblEquipOEE.Appearance.Options.UseFont = true;
            this.lblEquipOEE.Appearance.Options.UseForeColor = true;
            this.lblEquipOEE.Appearance.Options.UseTextOptions = true;
            this.lblEquipOEE.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblEquipOEE.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblEquipOEE.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEquipOEE.Location = new System.Drawing.Point(0, 36);
            this.lblEquipOEE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblEquipOEE.Name = "lblEquipOEE";
            this.lblEquipOEE.Size = new System.Drawing.Size(444, 79);
            this.lblEquipOEE.TabIndex = 8;
            this.lblEquipOEE.Text = "100.55%";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl4.Location = new System.Drawing.Point(0, 0);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(444, 36);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "전체 설비종합효율";
            // 
            // panelControl8
            // 
            this.panelControl8.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl8.Appearance.Options.UseBackColor = true;
            this.panelControl8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl8.Controls.Add(this.gaugeControl1);
            this.panelControl8.Controls.Add(this.lblRunHour);
            this.panelControl8.Controls.Add(this.labelControl5);
            this.panelControl8.Location = new System.Drawing.Point(537, 68);
            this.panelControl8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl8.Name = "panelControl8";
            this.panelControl8.Size = new System.Drawing.Size(410, 275);
            this.panelControl8.TabIndex = 12;
            // 
            // gaugeControl1
            // 
            this.gaugeControl1.ColorScheme.TargetElements = ((DevExpress.XtraGauges.Core.Base.TargetElement)((DevExpress.XtraGauges.Core.Base.TargetElement.RangeBar | DevExpress.XtraGauges.Core.Base.TargetElement.Label)));
            this.gaugeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.circularGauge2});
            this.gaugeControl1.Location = new System.Drawing.Point(0, 0);
            this.gaugeControl1.Name = "gaugeControl1";
            this.gaugeControl1.Size = new System.Drawing.Size(410, 191);
            this.gaugeControl1.TabIndex = 8;
            // 
            // circularGauge2
            // 
            this.circularGauge2.AutoSize = DevExpress.Utils.DefaultBoolean.True;
            this.circularGauge2.Bounds = new System.Drawing.Rectangle(6, 6, 398, 179);
            this.circularGauge2.Labels.AddRange(new DevExpress.XtraGauges.Win.Base.LabelComponent[] {
            this.lblMachineRate});
            this.circularGauge2.Name = "circularGauge2";
            this.circularGauge2.RangeBars.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent[] {
            this.arcScaleRangeBarComponent4});
            this.circularGauge2.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.arcScaleComponent4});
            // 
            // lblMachineRate
            // 
            this.lblMachineRate.AppearanceText.DXFont = new DevExpress.Drawing.DXFont("Segoe UI", 25F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblMachineRate.Name = "circularGauge1_Label1";
            this.lblMachineRate.Size = new System.Drawing.SizeF(140F, 60F);
            this.lblMachineRate.Text = "100%";
            this.lblMachineRate.ZOrder = -1001;
            // 
            // arcScaleRangeBarComponent4
            // 
            this.arcScaleRangeBarComponent4.ArcScale = this.arcScaleComponent4;
            this.arcScaleRangeBarComponent4.Name = "circularGauge1_RangeBar2";
            this.arcScaleRangeBarComponent4.RoundedCaps = true;
            this.arcScaleRangeBarComponent4.ShowBackground = true;
            this.arcScaleRangeBarComponent4.StartOffset = 80F;
            this.arcScaleRangeBarComponent4.ZOrder = -10;
            // 
            // arcScaleComponent4
            // 
            this.arcScaleComponent4.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent4.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent4.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent4.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent4.AppearanceTickmarkText.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 8.5F);
            this.arcScaleComponent4.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A");
            this.arcScaleComponent4.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 125F);
            this.arcScaleComponent4.EndAngle = 90F;
            this.arcScaleComponent4.MajorTickCount = 0;
            this.arcScaleComponent4.MajorTickmark.FormatString = "{0:F0}";
            this.arcScaleComponent4.MajorTickmark.ShapeOffset = -14F;
            this.arcScaleComponent4.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1;
            this.arcScaleComponent4.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent4.MaxValue = 100F;
            this.arcScaleComponent4.MinorTickCount = 0;
            this.arcScaleComponent4.MinorTickmark.ShapeOffset = -7F;
            this.arcScaleComponent4.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2;
            this.arcScaleComponent4.Name = "scale1";
            this.arcScaleComponent4.StartAngle = -270F;
            this.arcScaleComponent4.Value = 20F;
            // 
            // lblRunHour
            // 
            this.lblRunHour.Appearance.BackColor = System.Drawing.Color.White;
            this.lblRunHour.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 25F);
            this.lblRunHour.Appearance.Options.UseBackColor = true;
            this.lblRunHour.Appearance.Options.UseFont = true;
            this.lblRunHour.Appearance.Options.UseTextOptions = true;
            this.lblRunHour.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblRunHour.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRunHour.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblRunHour.Location = new System.Drawing.Point(0, 191);
            this.lblRunHour.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblRunHour.Name = "lblRunHour";
            this.lblRunHour.Size = new System.Drawing.Size(410, 49);
            this.lblRunHour.TabIndex = 2;
            this.lblRunHour.Text = "913 H";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 12F);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.labelControl5.Appearance.Options.UseBackColor = true;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControl5.Location = new System.Drawing.Point(0, 240);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(410, 35);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "누적 가동시간";
            // 
            // panelControl11
            // 
            this.panelControl11.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl11.Controls.Add(this.labelControl2);
            this.panelControl11.Controls.Add(this.labelControl16);
            this.panelControl11.Location = new System.Drawing.Point(3, 3);
            this.panelControl11.Name = "panelControl11";
            this.panelControl11.Size = new System.Drawing.Size(1406, 28);
            this.panelControl11.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(0, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.labelControl2.Size = new System.Drawing.Size(254, 28);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "실 시 간  종 합 현 황";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 11F);
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Appearance.Options.UseTextOptions = true;
            this.labelControl16.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl16.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl16.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelControl16.Location = new System.Drawing.Point(1210, 0);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.labelControl16.Size = new System.Drawing.Size(196, 28);
            this.labelControl16.TabIndex = 4;
            this.labelControl16.Text = "2026년 5월 21일 9:50";
            // 
            // GcST
            // 
            this.GcST.Location = new System.Drawing.Point(960, 415);
            this.GcST.MainView = this.GvST;
            this.GcST.Name = "GcST";
            this.GcST.Size = new System.Drawing.Size(441, 335);
            this.GcST.TabIndex = 10;
            this.GcST.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvST});
            // 
            // GvST
            // 
            this.GvST.GridControl = this.GcST;
            this.GvST.Name = "GvST";
            this.GvST.OptionsView.EnableAppearanceOddRow = true;
            this.GvST.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup2,
            this.layoutControlItem3,
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.layoutControlGroup6});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1412, 761);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(954, 34);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(458, 316);
            this.layoutControlGroup3.Text = "설비 종합 효율";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(450, 281);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(530, 316);
            this.layoutControlGroup2.Text = "실시간 생산현황";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl2;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(522, 281);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl11;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1412, 34);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup8});
            this.layoutControlGroup4.Location = new System.Drawing.Point(530, 350);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(882, 411);
            this.layoutControlGroup4.Text = "납품 및 재고현황";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(419, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(455, 376);
            this.layoutControlGroup1.Text = "재고 현황";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.GcST;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(447, 341);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.CustomizationFormText = "설비 종합 효율";
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem6,
            this.layoutControlItem9});
            this.layoutControlGroup8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup8.Size = new System.Drawing.Size(419, 376);
            this.layoutControlGroup8.Text = "납품현황";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.tableLayoutPanel3;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem8.Size = new System.Drawing.Size(411, 97);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.panelControl4;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem6.Size = new System.Drawing.Size(411, 32);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.chartSales;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 129);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem9.Size = new System.Drawing.Size(411, 212);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "일별 불량수량 추이(최근 10일)";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 350);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(530, 411);
            this.layoutControlGroup5.Text = "실시간 품질현황";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.panelControl3;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(522, 376);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup6.Location = new System.Drawing.Point(530, 34);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(424, 316);
            this.layoutControlGroup6.Text = "보전지수";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.panelControl8;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(416, 281);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // circularGauge13
            // 
            this.circularGauge13.AutoSize = DevExpress.Utils.DefaultBoolean.False;
            this.circularGauge13.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.arcScaleBackgroundLayerComponent1});
            this.circularGauge13.Bounds = new System.Drawing.Rectangle(6, 6, 351, 211);
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
            arcScaleRange7.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange7.EndThickness = 22F;
            arcScaleRange7.EndValue = 33F;
            arcScaleRange7.Name = "Range0";
            arcScaleRange7.ShapeOffset = -4F;
            arcScaleRange7.StartThickness = 22F;
            arcScaleRange7.StartValue = -0.3F;
            arcScaleRange8.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange8.EndThickness = 22F;
            arcScaleRange8.EndValue = 66F;
            arcScaleRange8.Name = "Range1";
            arcScaleRange8.ShapeOffset = -4F;
            arcScaleRange8.StartThickness = 22F;
            arcScaleRange8.StartValue = 33F;
            arcScaleRange9.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange9.EndThickness = 22F;
            arcScaleRange9.EndValue = 100.3F;
            arcScaleRange9.Name = "Range2";
            arcScaleRange9.ShapeOffset = -4F;
            arcScaleRange9.StartThickness = 22F;
            arcScaleRange9.StartValue = 66F;
            this.MachneComponent.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange7,
            arcScaleRange8,
            arcScaleRange9});
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
            this.circularGauge1.Bounds = new System.Drawing.Rectangle(6, 6, 351, 211);
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
            this.arcScaleComponent1.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange10,
            arcScaleRange11,
            arcScaleRange12});
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
            // arcScaleRangeBarComponent1
            // 
            this.arcScaleRangeBarComponent1.Name = "circularGauge2_RangeBar1";
            this.arcScaleRangeBarComponent1.ZOrder = -10;
            // 
            // arcScaleMarkerComponent1
            // 
            this.arcScaleMarkerComponent1.Name = "circularGauge2_Marker1";
            this.arcScaleMarkerComponent1.ZOrder = -100;
            // 
            // arcScaleRangeBarComponent2
            // 
            this.arcScaleRangeBarComponent2.Name = "circularGauge2_RangeBar2";
            this.arcScaleRangeBarComponent2.ZOrder = -10;
            // 
            // commandBarGalleryDropDown1
            // 
            // 
            // 
            // 
            createBarChartItem2.Caption = "Clustered Column";
            createBarChartItem2.Description = "Compare values across categories by using vertical rectangles.\r\n\r\nUse it when the" +
    " order of categories is not important or for displaying item counts such as a hi" +
    "stogram.";
            createBarChartItem2.Hint = "Compare values across categories by using vertical rectangles.\r\n\r\nUse it when the" +
    " order of categories is not important or for displaying item counts such as a hi" +
    "stogram.";
            createFullStackedBarChartItem2.Caption = "100% Stacked Column";
            createFullStackedBarChartItem2.Description = "Compare the percentage that each value contributes to a total across categories b" +
    "y using vertical rectangles.\r\n\r\nUse it to emphasize the proportion of each data " +
    "series.";
            createFullStackedBarChartItem2.Hint = "Compare the percentage that each value contributes to a total across categories b" +
    "y using vertical rectangles.\r\n\r\nUse it to emphasize the proportion of each data " +
    "series.";
            createSideBySideFullStackedBarChartItem2.Caption = "Clustered 100% Stacked Column";
            createSideBySideFullStackedBarChartItem2.Description = "Combine the advantages of both the 100% Stacked Column and Clustered Column chart" +
    " types, so that you can stack different columns, and combine them into groups ac" +
    "ross the same axis value.";
            createSideBySideFullStackedBarChartItem2.Hint = "Combine the advantages of both the 100% Stacked Column and Clustered Column chart" +
    " types, so that you can stack different columns, and combine them into groups ac" +
    "ross the same axis value.";
            createSideBySideStackedBarChartItem2.Caption = "Clustered Stacked Column";
            createSideBySideStackedBarChartItem2.Description = "Combine the advantages of both the Stacked Column and Clustered Column chart type" +
    "s, so that you can stack different columns, and combine them into groups across " +
    "the same axis value.";
            createSideBySideStackedBarChartItem2.Hint = "Combine the advantages of both the Stacked Column and Clustered Column chart type" +
    "s, so that you can stack different columns, and combine them into groups across " +
    "the same axis value.";
            createStackedBarChartItem2.Caption = "Stacked Column";
            createStackedBarChartItem2.Description = "Compare the contribution of each value to a total across categories by using vert" +
    "ical rectangles.\r\n\r\nUse it to emphasize the total across series for one category" +
    ".";
            createStackedBarChartItem2.Hint = "Compare the contribution of each value to a total across categories by using vert" +
    "ical rectangles.\r\n\r\nUse it to emphasize the total across series for one category" +
    ".";
            createWaterfallChartItem2.Caption = "Waterfall";
            createWaterfallChartItem2.Description = "A Waterfall chart displays a sequence of bars that indicate positive or negative " +
    "changes.";
            createWaterfallChartItem2.Hint = "A Waterfall chart displays a sequence of bars that indicate positive or negative " +
    "changes.";
            chartControlCommandGalleryItemGroup2DColumn2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createBarChartItem2,
            createFullStackedBarChartItem2,
            createSideBySideFullStackedBarChartItem2,
            createSideBySideStackedBarChartItem2,
            createStackedBarChartItem2,
            createWaterfallChartItem2});
            createBar3DChartItem2.Caption = "3-D Clustered Column";
            createBar3DChartItem2.Description = "Compare values across categories and display clustered columns in 3-D format.";
            createBar3DChartItem2.Hint = "Compare values across categories and display clustered columns in 3-D format.";
            createFullStackedBar3DChartItem2.Caption = "100% Stacked Column in 3-D";
            createFullStackedBar3DChartItem2.Description = "Compare the percentage each value contributes to a total across categories and di" +
    "splay 100% stacked columns in 3-D format.";
            createFullStackedBar3DChartItem2.Hint = "Compare the percentage each value contributes to a total across categories and di" +
    "splay 100% stacked columns in 3-D format.";
            createManhattanBarChartItem2.Caption = "3-D Column";
            createManhattanBarChartItem2.Description = "Compare values across categories and across series on three axes.\r\n\r\nUse it when " +
    "the categories and series are equally important.";
            createManhattanBarChartItem2.Hint = "Compare values across categories and across series on three axes.\r\n\r\nUse it when " +
    "the categories and series are equally important.";
            createSideBySideFullStackedBar3DChartItem2.Caption = "Clustered 100% Stacked Column in 3-D";
            createSideBySideFullStackedBar3DChartItem2.Description = "Combine the advantages of both the 100% Stacked Column and Clustered Column chart" +
    " types in 3-D format, so that you can stack different columns, and combine them " +
    "into groups across the same axis value.";
            createSideBySideFullStackedBar3DChartItem2.Hint = "Combine the advantages of both the 100% Stacked Column and Clustered Column chart" +
    " types in 3-D format, so that you can stack different columns, and combine them " +
    "into groups across the same axis value.";
            createSideBySideStackedBar3DChartItem2.Caption = "Clustered Stacked Column in 3-D";
            createSideBySideStackedBar3DChartItem2.Description = "Combine the advantages of both the Stacked Column and Clustered Column chart type" +
    "s in 3-D format, so that you can stack different columns, and combine them into " +
    "groups across the same axis value.";
            createSideBySideStackedBar3DChartItem2.Hint = "Combine the advantages of both the Stacked Column and Clustered Column chart type" +
    "s in 3-D format, so that you can stack different columns, and combine them into " +
    "groups across the same axis value.";
            createStackedBar3DChartItem2.Caption = "Stacked Column in 3-D";
            createStackedBar3DChartItem2.Description = "Compare the contribution of each value to a total across categories and display s" +
    "tacked columns in 3-D format.";
            createStackedBar3DChartItem2.Hint = "Compare the contribution of each value to a total across categories and display s" +
    "tacked columns in 3-D format.";
            chartControlCommandGalleryItemGroup3DColumn2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createBar3DChartItem2,
            createFullStackedBar3DChartItem2,
            createManhattanBarChartItem2,
            createSideBySideFullStackedBar3DChartItem2,
            createSideBySideStackedBar3DChartItem2,
            createStackedBar3DChartItem2});
            createCylinderBar3DChartItem2.Caption = "Clustered Cylinder";
            createCylinderBar3DChartItem2.Description = "Compare values across categories.";
            createCylinderBar3DChartItem2.Hint = "Compare values across categories.";
            createCylinderFullStackedBar3DChartItem2.Caption = "100% Stacked Cylinder";
            createCylinderFullStackedBar3DChartItem2.Description = "Compare the percentage each value contributes to a total across categories.";
            createCylinderFullStackedBar3DChartItem2.Hint = "Compare the percentage each value contributes to a total across categories.";
            createCylinderManhattanBarChartItem2.Caption = "3-D Cylinder";
            createCylinderManhattanBarChartItem2.Description = "Compare values across categories and across series and display a cylinder chart o" +
    "n three axes.";
            createCylinderManhattanBarChartItem2.Hint = "Compare values across categories and across series and display a cylinder chart o" +
    "n three axes.";
            createCylinderSideBySideFullStackedBar3DChartItem2.Caption = "Clustered 100% Stacked Cylinder";
            createCylinderSideBySideFullStackedBar3DChartItem2.Description = "Combine the advantages of both the 100% Stacked Cylinder and Clustered Cylinder c" +
    "hart types, so that you can stack different cylinders, and combine them into gro" +
    "ups across the same axis value.";
            createCylinderSideBySideFullStackedBar3DChartItem2.Hint = "Combine the advantages of both the 100% Stacked Cylinder and Clustered Cylinder c" +
    "hart types, so that you can stack different cylinders, and combine them into gro" +
    "ups across the same axis value.";
            createCylinderSideBySideStackedBar3DChartItem2.Caption = "Clustered Stacked Cylinder";
            createCylinderSideBySideStackedBar3DChartItem2.Description = "Combine the advantages of both the Stacked Cylinder and Clustered Cylinder chart " +
    "types, so that you can stack different cylinders, and combine them into groups a" +
    "cross the same axis value.";
            createCylinderSideBySideStackedBar3DChartItem2.Hint = "Combine the advantages of both the Stacked Cylinder and Clustered Cylinder chart " +
    "types, so that you can stack different cylinders, and combine them into groups a" +
    "cross the same axis value.";
            createCylinderStackedBar3DChartItem2.Caption = "Stacked Cylinder";
            createCylinderStackedBar3DChartItem2.Description = "Compare the contribution of each value to a total across categories.";
            createCylinderStackedBar3DChartItem2.Hint = "Compare the contribution of each value to a total across categories.";
            chartControlCommandGalleryItemGroupCylinderColumn2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createCylinderBar3DChartItem2,
            createCylinderFullStackedBar3DChartItem2,
            createCylinderManhattanBarChartItem2,
            createCylinderSideBySideFullStackedBar3DChartItem2,
            createCylinderSideBySideStackedBar3DChartItem2,
            createCylinderStackedBar3DChartItem2});
            createConeBar3DChartItem2.Caption = "Clustered Cone";
            createConeBar3DChartItem2.Description = "Compare values across categories.";
            createConeBar3DChartItem2.Hint = "Compare values across categories.";
            createConeFullStackedBar3DChartItem2.Caption = "100% Stacked Cone";
            createConeFullStackedBar3DChartItem2.Description = "Compare the percentage each value contributes to a total across categories.";
            createConeFullStackedBar3DChartItem2.Hint = "Compare the percentage each value contributes to a total across categories.";
            createConeManhattanBarChartItem2.Caption = "3-D Cone";
            createConeManhattanBarChartItem2.Description = "Compare values across categories and across series and display a cone chart on th" +
    "ree axes.";
            createConeManhattanBarChartItem2.Hint = "Compare values across categories and across series and display a cone chart on th" +
    "ree axes.";
            createConeSideBySideFullStackedBar3DChartItem2.Caption = "Clustered 100% Stacked Cone";
            createConeSideBySideFullStackedBar3DChartItem2.Description = "Combine the advantages of both the 100% Stacked Cone and Clustered Cone chart typ" +
    "es, so that you can stack different cones, and combine them into groups across t" +
    "he same axis value.";
            createConeSideBySideFullStackedBar3DChartItem2.Hint = "Combine the advantages of both the 100% Stacked Cone and Clustered Cone chart typ" +
    "es, so that you can stack different cones, and combine them into groups across t" +
    "he same axis value.";
            createConeSideBySideStackedBar3DChartItem2.Caption = "Clustered Stacked Cone";
            createConeSideBySideStackedBar3DChartItem2.Description = "Combine the advantages of both the Stacked Cone and Clustered Cone chart types, s" +
    "o that you can stack different cones, and combine them into groups across the sa" +
    "me axis value.";
            createConeSideBySideStackedBar3DChartItem2.Hint = "Combine the advantages of both the Stacked Cone and Clustered Cone chart types, s" +
    "o that you can stack different cones, and combine them into groups across the sa" +
    "me axis value.";
            createConeStackedBar3DChartItem2.Caption = "Stacked Cone";
            createConeStackedBar3DChartItem2.Description = "Compare the contribution of each value to a total across categories.";
            createConeStackedBar3DChartItem2.Hint = "Compare the contribution of each value to a total across categories.";
            chartControlCommandGalleryItemGroupConeColumn2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createConeBar3DChartItem2,
            createConeFullStackedBar3DChartItem2,
            createConeManhattanBarChartItem2,
            createConeSideBySideFullStackedBar3DChartItem2,
            createConeSideBySideStackedBar3DChartItem2,
            createConeStackedBar3DChartItem2});
            createPyramidBar3DChartItem2.Caption = "Clustered Pyramid";
            createPyramidBar3DChartItem2.Description = "Compare values across categories.";
            createPyramidBar3DChartItem2.Hint = "Compare values across categories.";
            createPyramidFullStackedBar3DChartItem2.Caption = "100% Stacked Pyramid";
            createPyramidFullStackedBar3DChartItem2.Description = "Compare the percentage each value contributes to a total across categories.";
            createPyramidFullStackedBar3DChartItem2.Hint = "Compare the percentage each value contributes to a total across categories.";
            createPyramidManhattanBarChartItem2.Caption = "3-D Pyramid";
            createPyramidManhattanBarChartItem2.Description = "Compare values across categories and across series and display a pyramid chart on" +
    " three axes.";
            createPyramidManhattanBarChartItem2.Hint = "Compare values across categories and across series and display a pyramid chart on" +
    " three axes.";
            createPyramidSideBySideFullStackedBar3DChartItem2.Caption = "Clustered 100% Stacked Pyramid";
            createPyramidSideBySideFullStackedBar3DChartItem2.Description = "Combine the advantages of both the 100% Stacked Pyramid and Clustered Pyramid cha" +
    "rt types, so that you can stack different pyramids, and combine them into groups" +
    " across the same axis value.";
            createPyramidSideBySideFullStackedBar3DChartItem2.Hint = "Combine the advantages of both the 100% Stacked Pyramid and Clustered Pyramid cha" +
    "rt types, so that you can stack different pyramids, and combine them into groups" +
    " across the same axis value.";
            createPyramidSideBySideStackedBar3DChartItem2.Caption = "Clustered Stacked Pyramid";
            createPyramidSideBySideStackedBar3DChartItem2.Description = "Combine the advantages of both the Stacked Pyramid and Clustered Pyramid chart ty" +
    "pes, so that you can stack different pyramids, and combine them into groups acro" +
    "ss the same axis value.";
            createPyramidSideBySideStackedBar3DChartItem2.Hint = "Combine the advantages of both the Stacked Pyramid and Clustered Pyramid chart ty" +
    "pes, so that you can stack different pyramids, and combine them into groups acro" +
    "ss the same axis value.";
            createPyramidStackedBar3DChartItem2.Caption = "Stacked Pyramid";
            createPyramidStackedBar3DChartItem2.Description = "Compare the contribution of each value to a total across categories.";
            createPyramidStackedBar3DChartItem2.Hint = "Compare the contribution of each value to a total across categories.";
            chartControlCommandGalleryItemGroupPyramidColumn2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createPyramidBar3DChartItem2,
            createPyramidFullStackedBar3DChartItem2,
            createPyramidManhattanBarChartItem2,
            createPyramidSideBySideFullStackedBar3DChartItem2,
            createPyramidSideBySideStackedBar3DChartItem2,
            createPyramidStackedBar3DChartItem2});
            this.commandBarGalleryDropDown1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            chartControlCommandGalleryItemGroup2DColumn2,
            chartControlCommandGalleryItemGroup3DColumn2,
            chartControlCommandGalleryItemGroupCylinderColumn2,
            chartControlCommandGalleryItemGroupConeColumn2,
            chartControlCommandGalleryItemGroupPyramidColumn2});
            this.commandBarGalleryDropDown1.Manager = null;
            this.commandBarGalleryDropDown1.Name = "commandBarGalleryDropDown1";
            // 
            // commandBarGalleryDropDown2
            // 
            // 
            // 
            // 
            createLineChartItem2.Caption = "Line";
            createLineChartItem2.Description = "Displays trend overtime (dates, years) or ordered categories. Useful when there a" +
    "re many data points and the order is important.";
            createLineChartItem2.Hint = "Displays trend overtime (dates, years) or ordered categories. Useful when there a" +
    "re many data points and the order is important.";
            createFullStackedLineChartItem2.Caption = "100% Stacked Line";
            createFullStackedLineChartItem2.Description = "Displays the trend of the percentage each value contributes over time or ordered " +
    "categories.";
            createFullStackedLineChartItem2.Hint = "Displays the trend of the percentage each value contributes over time or ordered " +
    "categories.";
            createScatterLineChartItem2.Caption = "Scatter Line";
            createScatterLineChartItem2.Description = "Displays series points in the same order as they appear in the collection.";
            createScatterLineChartItem2.Hint = "Displays series points in the same order as they appear in the collection.";
            createSplineChartItem2.Caption = "Spline";
            createSplineChartItem2.Description = "Plot a fitted curve through each data point in a series.";
            createSplineChartItem2.Hint = "Plot a fitted curve through each data point in a series.";
            createStackedLineChartItem2.Caption = "Stacked Line";
            createStackedLineChartItem2.Description = "Displays the trend of the contribution of each value over time or ordered categor" +
    "ies.";
            createStackedLineChartItem2.Hint = "Displays the trend of the contribution of each value over time or ordered categor" +
    "ies.";
            createStepLineChartItem2.Caption = "Step Line";
            createStepLineChartItem2.Description = "Show to what extent values have changed for different points in the same series.";
            createStepLineChartItem2.Hint = "Show to what extent values have changed for different points in the same series.";
            chartControlCommandGalleryItemGroup2DLine2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createLineChartItem2,
            createFullStackedLineChartItem2,
            createScatterLineChartItem2,
            createSplineChartItem2,
            createStackedLineChartItem2,
            createStepLineChartItem2});
            createLine3DChartItem2.Caption = "3-D Line";
            createLine3DChartItem2.Description = "Displays each row or column of data as a 3-D ribbon on three axes.";
            createLine3DChartItem2.Hint = "Displays each row or column of data as a 3-D ribbon on three axes.";
            createFullStackedLine3DChartItem2.Caption = "100% Stacked Line in 3-D";
            createFullStackedLine3DChartItem2.Description = "Displays all series stacked and is useful when it is necessary to compare how muc" +
    "h each series adds to the total aggregate value for specific arguments (as perce" +
    "nts).";
            createFullStackedLine3DChartItem2.Hint = "Displays all series stacked and is useful when it is necessary to compare how muc" +
    "h each series adds to the total aggregate value for specific arguments (as perce" +
    "nts).";
            createSpline3DChartItem2.Caption = "3-D Spline";
            createSpline3DChartItem2.Description = "Plot a fitted curve through each data point in a series.";
            createSpline3DChartItem2.Hint = "Plot a fitted curve through each data point in a series.";
            createStackedLine3DChartItem2.Caption = "Stacked Line in 3-D";
            createStackedLine3DChartItem2.Description = "Displays all points from different series in a stacked manner and is useful when " +
    "it is necessary to compare how much each series adds to the total aggregate valu" +
    "e for specific arguments.";
            createStackedLine3DChartItem2.Hint = "Displays all points from different series in a stacked manner and is useful when " +
    "it is necessary to compare how much each series adds to the total aggregate valu" +
    "e for specific arguments.";
            createStepLine3DChartItem2.Caption = "Step Line in 3-D";
            createStepLine3DChartItem2.Description = "Show to what extent values have changed for different points in the same series.";
            createStepLine3DChartItem2.Hint = "Show to what extent values have changed for different points in the same series.";
            chartControlCommandGalleryItemGroup3DLine2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createLine3DChartItem2,
            createFullStackedLine3DChartItem2,
            createSpline3DChartItem2,
            createStackedLine3DChartItem2,
            createStepLine3DChartItem2});
            this.commandBarGalleryDropDown2.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            chartControlCommandGalleryItemGroup2DLine2,
            chartControlCommandGalleryItemGroup3DLine2});
            this.commandBarGalleryDropDown2.Manager = null;
            this.commandBarGalleryDropDown2.Name = "commandBarGalleryDropDown2";
            // 
            // commandBarGalleryDropDown3
            // 
            // 
            // 
            // 
            createPieChartItem2.Caption = "Pie";
            createPieChartItem2.Description = "Displays the contribution of each value to a total.\r\n\r\nUse it when the values can" +
    " be added together or when you have only one data series and all values are posi" +
    "tive.";
            createPieChartItem2.Hint = "Displays the contribution of each value to a total.\r\n\r\nUse it when the values can" +
    " be added together or when you have only one data series and all values are posi" +
    "tive.";
            createDoughnutChartItem2.Caption = "Doughnut";
            createDoughnutChartItem2.Description = "Displays the contribution of each value to a total like a pie chart, but it can c" +
    "ontain multiple series.";
            createDoughnutChartItem2.Hint = "Displays the contribution of each value to a total like a pie chart, but it can c" +
    "ontain multiple series.";
            createNestedDoughnutChartItem2.Caption = "Nested Doughnut";
            createNestedDoughnutChartItem2.Description = "Displays the contribution of each value to a total while comparing series with on" +
    "e doughnut nested in another one.";
            createNestedDoughnutChartItem2.Hint = "Displays the contribution of each value to a total while comparing series with on" +
    "e doughnut nested in another one.";
            chartControlCommandGalleryItemGroup2DPie2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createPieChartItem2,
            createDoughnutChartItem2,
            createNestedDoughnutChartItem2});
            createPie3DChartItem2.Caption = "Pie in 3-D";
            createPie3DChartItem2.Description = "Displays the contribution of each value to a total.";
            createPie3DChartItem2.Hint = "Displays the contribution of each value to a total.";
            createDoughnut3DChartItem2.Caption = "Doughnut in 3-D";
            createDoughnut3DChartItem2.Description = "Compare the percentage values of different point arguments in the same series, an" +
    "d illustrate these values as easy to understand pie slices, but with a hole in i" +
    "ts center.";
            createDoughnut3DChartItem2.Hint = "Compare the percentage values of different point arguments in the same series, an" +
    "d illustrate these values as easy to understand pie slices, but with a hole in i" +
    "ts center.";
            chartControlCommandGalleryItemGroup3DPie2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createPie3DChartItem2,
            createDoughnut3DChartItem2});
            this.commandBarGalleryDropDown3.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            chartControlCommandGalleryItemGroup2DPie2,
            chartControlCommandGalleryItemGroup3DPie2});
            this.commandBarGalleryDropDown3.Manager = null;
            this.commandBarGalleryDropDown3.Name = "commandBarGalleryDropDown3";
            // 
            // commandBarGalleryDropDown4
            // 
            // 
            // 
            // 
            createRotatedBarChartItem2.Caption = "Bar";
            createRotatedBarChartItem2.Description = "Insert a bar chart.\r\n\r\nBar charts are the best chart type for comparing multiple " +
    "values.";
            createRotatedBarChartItem2.Hint = "Insert a bar chart.\r\n\r\nBar charts are the best chart type for comparing multiple " +
    "values.";
            createRotatedFullStackedBarChartItem2.Caption = "100% Stacked Bar";
            createRotatedSideBySideFullStackedBarChartItem2.Caption = "Clustered 100% Stacked Bar";
            createRotatedSideBySideFullStackedBarChartItem2.Description = "Combine the advantages of both the 100% Stacked Bar and Clustered Bar chart types" +
    ", so you can stack different bars, and combine them into groups across the same " +
    "axis value.";
            createRotatedSideBySideFullStackedBarChartItem2.Hint = "Combine the advantages of both the 100% Stacked Bar and Clustered Bar chart types" +
    ", so you can stack different bars, and combine them into groups across the same " +
    "axis value.";
            createRotatedSideBySideStackedBarChartItem2.Caption = "Clustered Stacked Bar";
            createRotatedSideBySideStackedBarChartItem2.Description = "Combine the advantages of both the Stacked Bar and Clustered Bar chart types, so " +
    "that you can stack different bars, and combine them into groups across the same " +
    "axis value.";
            createRotatedSideBySideStackedBarChartItem2.Hint = "Combine the advantages of both the Stacked Bar and Clustered Bar chart types, so " +
    "that you can stack different bars, and combine them into groups across the same " +
    "axis value.";
            createRotatedStackedBarChartItem2.Caption = "Stacked Bar";
            createRotatedStackedBarChartItem2.Description = "Compare the contribution of each value to a total across categories by using hori" +
    "zontal rectangles.";
            createRotatedStackedBarChartItem2.Hint = "Compare the contribution of each value to a total across categories by using hori" +
    "zontal rectangles.";
            chartControlCommandGalleryItemGroup2DBar2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createRotatedBarChartItem2,
            createRotatedFullStackedBarChartItem2,
            createRotatedSideBySideFullStackedBarChartItem2,
            createRotatedSideBySideStackedBarChartItem2,
            createRotatedStackedBarChartItem2});
            this.commandBarGalleryDropDown4.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            chartControlCommandGalleryItemGroup2DBar2});
            this.commandBarGalleryDropDown4.Manager = null;
            this.commandBarGalleryDropDown4.Name = "commandBarGalleryDropDown4";
            // 
            // commandBarGalleryDropDown5
            // 
            // 
            // 
            // 
            createAreaChartItem2.Caption = "Area";
            createAreaChartItem2.Description = "Displays the trend of values over time or categories.";
            createAreaChartItem2.Hint = "Displays the trend of values over time or categories.";
            createFullStackedAreaChartItem2.Caption = "100% Stacked Area";
            createFullStackedAreaChartItem2.Description = "Displays the trend of the percentage each value contributes over time or categori" +
    "es.\r\n\r\nUse it to emphasize the trend in the proportion of each series.";
            createFullStackedAreaChartItem2.Hint = "Displays the trend of the percentage each value contributes over time or categori" +
    "es.\r\n\r\nUse it to emphasize the trend in the proportion of each series.";
            createFullStackedSplineAreaChartItem2.Caption = "100% Stacked Spline Area";
            createFullStackedSplineAreaChartItem2.Description = "Behave similar to 100% Stacked Area, but plot a fitted curve through each data po" +
    "int in a series.";
            createFullStackedSplineAreaChartItem2.Hint = "Behave similar to 100% Stacked Area, but plot a fitted curve through each data po" +
    "int in a series.";
            createFullStackedStepAreaChartItem2.Caption = "100% Stacked Step Area";
            createFullStackedStepAreaChartItem2.Description = "Behaves similarly to the 100% Stacked Area Chart but connects data points using h" +
    "orizontal and vertical lines.";
            createFullStackedStepAreaChartItem2.Hint = "Behaves similarly to the 100% Stacked Area Chart but connects data points using h" +
    "orizontal and vertical lines.";
            createSplineAreaChartItem2.Caption = "Spline Area";
            createSplineAreaChartItem2.Description = "Behave similar to Area Chart but plot a fitted curve through each data point in a" +
    " series.";
            createSplineAreaChartItem2.Hint = "Behave similar to Area Chart but plot a fitted curve through each data point in a" +
    " series.";
            createStackedAreaChartItem2.Caption = "Stacked Area";
            createStackedAreaChartItem2.Description = "Displays the trend of the contribution of each value over time or categories.\r\n\r\n" +
    "Use it to emphasize the trend in the total across series for one category.";
            createStackedAreaChartItem2.Hint = "Displays the trend of the contribution of each value over time or categories.\r\n\r\n" +
    "Use it to emphasize the trend in the total across series for one category.";
            createStackedStepAreaChartItem2.Caption = "Stacked Step Area";
            createStackedStepAreaChartItem2.Description = "Behaves similarly to the Stacked Area Chart but connects data points using horizo" +
    "ntal and vertical lines.";
            createStackedStepAreaChartItem2.Hint = "Behaves similarly to the Stacked Area Chart but connects data points using horizo" +
    "ntal and vertical lines.";
            createStackedSplineAreaChartItem2.Caption = "Stacked Spline Area";
            createStackedSplineAreaChartItem2.Description = "Behave similar to Stacked Area Chart but plot a fitted curve through each data po" +
    "int in a series.";
            createStackedSplineAreaChartItem2.Hint = "Behave similar to Stacked Area Chart but plot a fitted curve through each data po" +
    "int in a series.";
            createStepAreaChartItem2.Caption = "Step Area";
            createStepAreaChartItem2.Description = "Show how much values have changed for different points of the same series.";
            createStepAreaChartItem2.Hint = "Show how much values have changed for different points of the same series.";
            chartControlCommandGalleryItemGroup2DArea2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createAreaChartItem2,
            createFullStackedAreaChartItem2,
            createFullStackedSplineAreaChartItem2,
            createFullStackedStepAreaChartItem2,
            createSplineAreaChartItem2,
            createStackedAreaChartItem2,
            createStackedStepAreaChartItem2,
            createStackedSplineAreaChartItem2,
            createStepAreaChartItem2});
            createArea3DChartItem2.Caption = "3-D Area";
            createFullStackedArea3DChartItem2.Caption = "100% Stacked Area in 3-D";
            createFullStackedSplineArea3DChartItem2.Caption = "100% Stacked Spline Area in 3-D";
            createFullStackedSplineArea3DChartItem2.Description = "Behave similar to 100% Stacked Area Chart in 3D, but plot a fitted curve through " +
    "each data point in a series.";
            createFullStackedSplineArea3DChartItem2.Hint = "Behave similar to 100% Stacked Area Chart in 3D, but plot a fitted curve through " +
    "each data point in a series.";
            createSplineArea3DChartItem2.Caption = "Spline Area in 3-D";
            createSplineArea3DChartItem2.Description = "Behave similar to 3D Area Chart, but plot a fitted curve through each data point " +
    "in a series.";
            createSplineArea3DChartItem2.Hint = "Behave similar to 3D Area Chart, but plot a fitted curve through each data point " +
    "in a series.";
            createStackedArea3DChartItem2.Caption = "Stacked Area in 3-D";
            createStackedArea3DChartItem2.Description = "Displays series as areas on a diagram, so that the value of each data point is ag" +
    "gregated with the underlying data points\' values.";
            createStackedArea3DChartItem2.Hint = "Displays series as areas on a diagram, so that the value of each data point is ag" +
    "gregated with the underlying data points\' values.";
            createStackedSplineArea3DChartItem2.Caption = "Stacked Spline Area in 3-D";
            createStackedSplineArea3DChartItem2.Description = "Behave similar to Stacked Area in 3D chart, but plot a fitted curve through each " +
    "data point in a series.";
            createStackedSplineArea3DChartItem2.Hint = "Behave similar to Stacked Area in 3D chart, but plot a fitted curve through each " +
    "data point in a series.";
            createStepArea3DChartItem2.Caption = "Step Area in 3-D";
            createStepArea3DChartItem2.Description = "Show to what extent values have changed for different points in the same series.";
            createStepArea3DChartItem2.Hint = "Show to what extent values have changed for different points in the same series.";
            chartControlCommandGalleryItemGroup3DArea2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createArea3DChartItem2,
            createFullStackedArea3DChartItem2,
            createFullStackedSplineArea3DChartItem2,
            createSplineArea3DChartItem2,
            createStackedArea3DChartItem2,
            createStackedSplineArea3DChartItem2,
            createStepArea3DChartItem2});
            this.commandBarGalleryDropDown5.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            chartControlCommandGalleryItemGroup2DArea2,
            chartControlCommandGalleryItemGroup3DArea2});
            this.commandBarGalleryDropDown5.Manager = null;
            this.commandBarGalleryDropDown5.Name = "commandBarGalleryDropDown5";
            // 
            // commandBarGalleryDropDown6
            // 
            // 
            // 
            // 
            createPointChartItem2.Caption = "Point";
            createPointChartItem2.Description = "Use it when it\'s necessary to show stand-alone data points on the same chart plot" +
    ".";
            createPointChartItem2.Hint = "Use it when it\'s necessary to show stand-alone data points on the same chart plot" +
    ".";
            createBubbleChartItem2.Caption = "Bubble";
            createBubbleChartItem2.Description = "Resemble a Scatter chart, but compare sets of three values instead of two. The th" +
    "ird value determines the size of the bubble marker.";
            createBubbleChartItem2.Hint = "Resemble a Scatter chart, but compare sets of three values instead of two. The th" +
    "ird value determines the size of the bubble marker.";
            chartControlCommandGalleryItemGroupPoint2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createPointChartItem2,
            createBubbleChartItem2});
            createFunnelChartItem2.Caption = "Funnel";
            createFunnel3DChartItem2.Caption = "3-D Funnel";
            chartControlCommandGalleryItemGroupFunnel2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createFunnelChartItem2,
            createFunnel3DChartItem2});
            createStockChartItem2.Caption = "Stock";
            createCandleStickChartItem2.Caption = "Candle Stick";
            chartControlCommandGalleryItemGroupFinancial2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createStockChartItem2,
            createCandleStickChartItem2});
            createRadarPointChartItem2.Caption = "Radar Point";
            createRadarPointChartItem2.Description = "Show points from two or more different series on the same points arguments on a c" +
    "ircular grid that has multiple axes along which data can be plotted.";
            createRadarPointChartItem2.Hint = "Show points from two or more different series on the same points arguments on a c" +
    "ircular grid that has multiple axes along which data can be plotted.";
            createRadarLineChartItem2.Caption = "Radar Line";
            createRadarLineChartItem2.Description = "Show trends for several series and compare their values for the same points argum" +
    "ents on a circular grid that has multiple axes along which data can be plotted.";
            createRadarLineChartItem2.Hint = "Show trends for several series and compare their values for the same points argum" +
    "ents on a circular grid that has multiple axes along which data can be plotted.";
            createRadarAreaChartItem2.Caption = "Radar Area";
            createRadarAreaChartItem2.Description = "Displays series as filled area on a circular grid that has multiple axes along wh" +
    "ich data can be plotted.";
            createRadarAreaChartItem2.Hint = "Displays series as filled area on a circular grid that has multiple axes along wh" +
    "ich data can be plotted.";
            createRadarRangeAreaChartItem2.Caption = "Radar Range Area";
            createScatterRadarLineChartItem2.Caption = "Scatter Radar Line";
            chartControlCommandGalleryItemGroupRadar2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createRadarPointChartItem2,
            createRadarLineChartItem2,
            createRadarAreaChartItem2,
            createRadarRangeAreaChartItem2,
            createScatterRadarLineChartItem2});
            createPolarPointChartItem2.Caption = "Polar Point";
            createPolarPointChartItem2.Description = "Show points from two or more different series on the same circular diagram on the" +
    " basis of angles.";
            createPolarPointChartItem2.Hint = "Show points from two or more different series on the same circular diagram on the" +
    " basis of angles.";
            createPolarLineChartItem2.Caption = "Polar Line";
            createPolarLineChartItem2.Description = "Show trends for several series and compare their values for the same points argum" +
    "ents on a circular diagram on the basis of angles.";
            createPolarLineChartItem2.Hint = "Show trends for several series and compare their values for the same points argum" +
    "ents on a circular diagram on the basis of angles.";
            createPolarAreaChartItem2.Caption = "Polar Area";
            createPolarAreaChartItem2.Description = "Displays series as filled area on a circular diagram on the basis of angles.";
            createPolarAreaChartItem2.Hint = "Displays series as filled area on a circular diagram on the basis of angles.";
            createPolarRangeAreaChartItem2.Caption = "Polar Range Area";
            createScatterPolarLineChartItem2.Caption = "Scatter Polar Line";
            chartControlCommandGalleryItemGroupPolar2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createPolarPointChartItem2,
            createPolarLineChartItem2,
            createPolarAreaChartItem2,
            createPolarRangeAreaChartItem2,
            createScatterPolarLineChartItem2});
            createRangeBarChartItem2.Caption = "Range Column";
            createRangeBarChartItem2.Description = "Displays vertical columns along the Y-axis (the axis of values). Each column repr" +
    "esents a range of data for each argument value.";
            createRangeBarChartItem2.Hint = "Displays vertical columns along the Y-axis (the axis of values). Each column repr" +
    "esents a range of data for each argument value.";
            createSideBySideRangeBarChartItem2.Caption = "Clustered Range Column";
            createSideBySideRangeBarChartItem2.Description = "Show activity columns from different series grouped by their arguments. Each colu" +
    "mn represents a range of data with two values for each argument value.";
            createSideBySideRangeBarChartItem2.Hint = "Show activity columns from different series grouped by their arguments. Each colu" +
    "mn represents a range of data with two values for each argument value.";
            createRangeAreaChartItem2.Caption = "Range Area";
            createRangeArea3DChartItem2.Caption = "Range Area in 3-D";
            chartControlCommandGalleryItemGroupRange2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createRangeBarChartItem2,
            createSideBySideRangeBarChartItem2,
            createRangeAreaChartItem2,
            createRangeArea3DChartItem2});
            createGanttChartItem2.Caption = "Gantt";
            createGanttChartItem2.Description = "Track different activities during the time frame.";
            createGanttChartItem2.Hint = "Track different activities during the time frame.";
            createSideBySideGanttChartItem2.Caption = "Clustered Gantt";
            chartControlCommandGalleryItemGroupGantt2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createGanttChartItem2,
            createSideBySideGanttChartItem2});
            createBoxPlotChartItem2.Caption = "BoxPlot";
            chartControlCommandGalleryItemGroupBoxPlot2.Items.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItem[] {
            createBoxPlotChartItem2});
            this.commandBarGalleryDropDown6.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            chartControlCommandGalleryItemGroupPoint2,
            chartControlCommandGalleryItemGroupFunnel2,
            chartControlCommandGalleryItemGroupFinancial2,
            chartControlCommandGalleryItemGroupRadar2,
            chartControlCommandGalleryItemGroupPolar2,
            chartControlCommandGalleryItemGroupRange2,
            chartControlCommandGalleryItemGroupGantt2,
            chartControlCommandGalleryItemGroupBoxPlot2});
            this.commandBarGalleryDropDown6.Manager = null;
            this.commandBarGalleryDropDown6.Name = "commandBarGalleryDropDown6";
            // 
            // commandBarGalleryDropDown7
            // 
            this.commandBarGalleryDropDown7.Manager = null;
            this.commandBarGalleryDropDown7.Name = "commandBarGalleryDropDown7";
            // 
            // commandBarGalleryDropDown8
            // 
            this.commandBarGalleryDropDown8.Manager = null;
            this.commandBarGalleryDropDown8.Name = "commandBarGalleryDropDown8";
            // 
            // arcScaleRangeBarComponent3
            // 
            this.arcScaleRangeBarComponent3.ArcScale = this.arcScaleComponent2;
            this.arcScaleRangeBarComponent3.Name = "circularGauge1_RangeBar2";
            this.arcScaleRangeBarComponent3.RoundedCaps = true;
            this.arcScaleRangeBarComponent3.ShowBackground = true;
            this.arcScaleRangeBarComponent3.StartOffset = 80F;
            this.arcScaleRangeBarComponent3.ZOrder = -10;
            // 
            // arcScaleComponent2
            // 
            this.arcScaleComponent2.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceTickmarkText.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 8.5F);
            this.arcScaleComponent2.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A");
            this.arcScaleComponent2.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 125F);
            this.arcScaleComponent2.EndAngle = 60F;
            this.arcScaleComponent2.MajorTickCount = 0;
            this.arcScaleComponent2.MajorTickmark.FormatString = "{0:F0}";
            this.arcScaleComponent2.MajorTickmark.ShapeOffset = -14F;
            this.arcScaleComponent2.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1;
            this.arcScaleComponent2.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent2.MaxValue = 100F;
            this.arcScaleComponent2.MinorTickCount = 0;
            this.arcScaleComponent2.MinorTickmark.ShapeOffset = -7F;
            this.arcScaleComponent2.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2;
            this.arcScaleComponent2.Name = "scale1";
            this.arcScaleComponent2.StartAngle = -240F;
            this.arcScaleComponent2.Value = 20F;
            // 
            // lblMaintainRate
            // 
            this.lblMaintainRate.AppearanceText.DXFont = new DevExpress.Drawing.DXFont("Segoe UI", 35F, DevExpress.Drawing.DXFontStyle.Bold, DevExpress.Drawing.DXGraphicsUnit.World);
            this.lblMaintainRate.Name = "circularGauge1_Label1";
            this.lblMaintainRate.Size = new System.Drawing.SizeF(140F, 60F);
            this.lblMaintainRate.Text = "910";
            this.lblMaintainRate.ZOrder = -1001;
            // 
            // stateImageIndicatorComponent1
            // 
            imageIndicatorState2.Name = "State1";
            this.stateImageIndicatorComponent1.ImageStateCollection.AddRange(new DevExpress.XtraGauges.Core.Model.ImageIndicatorState[] {
            imageIndicatorState2});
            this.stateImageIndicatorComponent1.IndicatorScale = this.arcScaleComponent2;
            this.stateImageIndicatorComponent1.Name = "circularGauge2_StateImageIndicator1";
            // 
            // arcScaleEffectLayerComponent2
            // 
            this.arcScaleEffectLayerComponent2.ArcScale = this.arcScaleComponent2;
            this.arcScaleEffectLayerComponent2.Name = "circularGauge2_EffectLayer2";
            this.arcScaleEffectLayerComponent2.ZOrder = -1000;
            // 
            // arcScaleEffectLayerComponent1
            // 
            this.arcScaleEffectLayerComponent1.ArcScale = this.arcScaleComponent2;
            this.arcScaleEffectLayerComponent1.Name = "circularGauge2_EffectLayer1";
            this.arcScaleEffectLayerComponent1.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5F, 0.99F);
            this.arcScaleEffectLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.EffectLayerShapeType.Empty;
            this.arcScaleEffectLayerComponent1.Size = new System.Drawing.SizeF(220F, 110F);
            this.arcScaleEffectLayerComponent1.ZOrder = -1000;
            // 
            // arcScaleBackgroundLayerComponent4
            // 
            this.arcScaleBackgroundLayerComponent4.ArcScale = this.arcScaleComponent2;
            this.arcScaleBackgroundLayerComponent4.Name = "circularGauge2_BackgroundLayer2";
            this.arcScaleBackgroundLayerComponent4.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularFull_Style16;
            this.arcScaleBackgroundLayerComponent4.ZOrder = 1000;
            // 
            // arcScaleBackgroundLayerComponent3
            // 
            this.arcScaleBackgroundLayerComponent3.ArcScale = this.arcScaleComponent2;
            this.arcScaleBackgroundLayerComponent3.Name = "circularGauge2_BackgroundLayer1";
            this.arcScaleBackgroundLayerComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularFull_Style16;
            this.arcScaleBackgroundLayerComponent3.ZOrder = 1000;
            // 
            // arcScaleComponent3
            // 
            this.arcScaleComponent3.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.AppearanceTickmarkText.DXFont = new DevExpress.Drawing.DXFont("Tahoma", 8.5F);
            this.arcScaleComponent3.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#484E5A");
            this.arcScaleComponent3.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 125F);
            this.arcScaleComponent3.EndAngle = 60F;
            this.arcScaleComponent3.MajorTickmark.FormatString = "{0:F0}";
            this.arcScaleComponent3.MajorTickmark.ShapeOffset = -14F;
            this.arcScaleComponent3.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_1;
            this.arcScaleComponent3.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent3.MaxValue = 100F;
            this.arcScaleComponent3.MinorTickCount = 4;
            this.arcScaleComponent3.MinorTickmark.ShapeOffset = -7F;
            this.arcScaleComponent3.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style16_2;
            this.arcScaleComponent3.Name = "circularGauge2_Scale2";
            this.arcScaleComponent3.StartAngle = -240F;
            this.arcScaleComponent3.Value = 50F;
            // 
            // Frm_MM_Dashboard_Total
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 761);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_MM_Dashboard_Total";
            this.Text = "Frm_MM_Dashboard_Total";
            this.Load += new System.EventHandler(this.Frm_MM_Dashboard_Total_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressInRate.Properties)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_QM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartPM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl8)).EndInit();
            this.panelControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMachineRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl11)).EndInit();
            this.panelControl11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GcST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MachneComponent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.circularGauge1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleMarkerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarGalleryDropDown8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleRangeBarComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMaintainRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stateImageIndicatorComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleEffectLayerComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleEffectLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraGrid.GridControl GcST;
		private DevExpress.XtraGrid.Views.Grid.GridView GvST;
		private LayoutControlItem layoutControlItem5;
		private LayoutControlGroup layoutControlGroup2;
		private LayoutControlGroup layoutControlGroup3;
		private LayoutControlGroup layoutControlGroup4;
		private LayoutControlGroup layoutControlGroup5;
		private PanelControl panelControl11;
		private LabelControl labelControl16;
		private LayoutControlItem layoutControlItem3;
        private LabelControl labelControl2;
        private LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge13;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent MachneComponent;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent2;
        private PanelControl panelControl8;
        private LabelControl lblRunHour;
        private LabelControl labelControl5;
        private LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent arcScaleRangeBarComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleMarkerComponent arcScaleMarkerComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent arcScaleRangeBarComponent2;
        private PanelControl panelControl1;
        private LabelControl labelControl4;
        private LayoutControlItem layoutControlItem2;
        private LabelControl lblEquipOEE;
        private PanelControl panelControl2;
        private LabelControl lblTotalQty;
        private LabelControl lbl_AchieveRate;
        private LayoutControlItem layoutControlItem1;
        private LabelControl lblAchieveRate;
        private LabelControl lbl_TotalQty;
        private LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraCharts.ChartControl ChartPM;
        private PanelControl panelControl3;
        private DevExpress.XtraCharts.ChartControl Chart_QM;
        private LayoutControlItem layoutControlItem4;
        private LabelControl lblGoodRate;
        private LabelControl lblNgRate;
        private LabelControl labelControl7;
        private LabelControl labelControl8;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown1;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown2;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown3;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown4;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown5;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown6;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown7;
        private DevExpress.XtraBars.Commands.CommandBarGalleryDropDown commandBarGalleryDropDown8;
        private LabelControl labelControl9;
        private LayoutControlGroup layoutControlGroup8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private LayoutControlItem layoutControlItem8;
        private LabelControl labelControl1;
        private LabelControl labelControl3;
        private LabelControl labelControl10;
        private ProgressBarControl progressInRate;
        private PanelControl panelControl4;
        private LayoutControlItem layoutControlItem6;
        private DevExpress.XtraCharts.ChartControl chartSales;
        private LayoutControlItem layoutControlItem9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent arcScaleRangeBarComponent3;
        private DevExpress.XtraGauges.Win.GaugeControl gaugeControl1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge2;
        private DevExpress.XtraGauges.Win.Base.LabelComponent lblMachineRate;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleRangeBarComponent arcScaleRangeBarComponent4;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent4;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent2;
        private DevExpress.XtraGauges.Win.Base.LabelComponent lblMaintainRate;
        private DevExpress.XtraGauges.Win.Base.StateImageIndicatorComponent stateImageIndicatorComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleEffectLayerComponent arcScaleEffectLayerComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleEffectLayerComponent arcScaleEffectLayerComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent4;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent3;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent3;
        private LabelControl labelControl14;
        private LabelControl lblOutCount;
        private LabelControl lblInCount;
        private LabelControl lblOrderCount;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
    }
}