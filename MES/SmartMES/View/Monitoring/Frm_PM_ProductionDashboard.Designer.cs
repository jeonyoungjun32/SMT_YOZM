
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.Monitoring
{
	partial class Frm_PM_ProductionDashboard
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
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView1 = new DevExpress.XtraCharts.DoughnutSeriesView();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView2 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView2 = new DevExpress.XtraCharts.DoughnutSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_PM_ProductionDashboard));
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange1 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange2 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange3 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange4 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange5 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange6 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange7 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange8 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange9 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            this.WriteDate = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.Title = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.Gc_OrderProcess = new DevExpress.XtraGrid.GridControl();
            this.Gv_OrderProcess = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar121 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.Gc_Notice = new DevExpress.XtraGrid.GridControl();
            this.Gv_Notice = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.GcMachine = new DevExpress.XtraGrid.GridControl();
            this.GvMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ChartProduction_Day = new DevExpress.XtraCharts.ChartControl();
            this.ChartProduction_Time = new DevExpress.XtraCharts.ChartControl();
            this.Gc_Work = new DevExpress.XtraGrid.GridControl();
            this.Gv_Work = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar12 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ChartSales_Cust = new DevExpress.XtraCharts.ChartControl();
            this.TPMain = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl9 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox3 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblSalesAmt = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox6 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblWorkQty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl111 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox2 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblOpRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox4 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblAchieveRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox1 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblNgRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.lineChart = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
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
            this.btn_Colose = new baseSMT.smtSearchButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_OrderProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_OrderProcess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar121)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Notice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Notice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartProduction_Day)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartProduction_Time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Work)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Work)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSales_Cust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView2)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
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
            // WriteDate
            // 
            this.WriteDate.Caption = "등록일자";
            this.WriteDate.FieldName = "WriteDate";
            this.WriteDate.MinWidth = 24;
            this.WriteDate.Name = "WriteDate";
            this.WriteDate.OptionsColumn.AllowFocus = false;
            this.WriteDate.Visible = true;
            this.WriteDate.VisibleIndex = 0;
            this.WriteDate.Width = 129;
            // 
            // Title
            // 
            this.Title.Caption = "제목";
            this.Title.FieldName = "Subject";
            this.Title.MinWidth = 24;
            this.Title.Name = "Title";
            this.Title.OptionsColumn.AllowFocus = false;
            this.Title.Visible = true;
            this.Title.VisibleIndex = 1;
            this.Title.Width = 444;
            // 
            // layoutControl1
            // 
            this.layoutControl1.BackColor = System.Drawing.Color.Transparent;
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1195, 363, 938, 655);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1765, 792);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl2);
            this.panelControl1.Controls.Add(this.smtCommonBar1);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1765, 792);
            this.panelControl1.TabIndex = 1;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.chartControl1);
            this.layoutControl2.Controls.Add(this.Gc_OrderProcess);
            this.layoutControl2.Controls.Add(this.Gc_Notice);
            this.layoutControl2.Controls.Add(this.GcMachine);
            this.layoutControl2.Controls.Add(this.ChartProduction_Day);
            this.layoutControl2.Controls.Add(this.ChartProduction_Time);
            this.layoutControl2.Controls.Add(this.Gc_Work);
            this.layoutControl2.Controls.Add(this.ChartSales_Cust);
            this.layoutControl2.Controls.Add(this.TPMain);
            this.layoutControl2.Controls.Add(this.lineChart);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 42);
            this.layoutControl2.Margin = new System.Windows.Forms.Padding(4);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(11, 440, 650, 400);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1761, 748);
            this.layoutControl2.TabIndex = 295;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // chartControl1
            // 
            this.chartControl1.Location = new System.Drawing.Point(528, 574);
            this.chartControl1.Margin = new System.Windows.Forms.Padding(4);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.PaletteName = "In A Fog";
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.Name = "Series 1";
            series1.SeriesID = 1;
            series1.View = doughnutSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl1.Size = new System.Drawing.Size(536, 170);
            this.chartControl1.TabIndex = 16;
            // 
            // Gc_OrderProcess
            // 
            this.Gc_OrderProcess.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.Gc_OrderProcess.Location = new System.Drawing.Point(1075, 577);
            this.Gc_OrderProcess.MainView = this.Gv_OrderProcess;
            this.Gc_OrderProcess.Margin = new System.Windows.Forms.Padding(4);
            this.Gc_OrderProcess.Name = "Gc_OrderProcess";
            this.Gc_OrderProcess.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar121});
            this.Gc_OrderProcess.Size = new System.Drawing.Size(679, 164);
            this.Gc_OrderProcess.TabIndex = 15;
            this.Gc_OrderProcess.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_OrderProcess});
            // 
            // Gv_OrderProcess
            // 
            this.Gv_OrderProcess.DetailHeight = 412;
            this.Gv_OrderProcess.GridControl = this.Gc_OrderProcess;
            this.Gv_OrderProcess.Name = "Gv_OrderProcess";
            this.Gv_OrderProcess.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_OrderProcess.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar121
            // 
            this.repositoryItemProgressBar121.Name = "repositoryItemProgressBar121";
            // 
            // Gc_Notice
            // 
            this.Gc_Notice.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Gc_Notice.Location = new System.Drawing.Point(4, 574);
            this.Gc_Notice.MainView = this.Gv_Notice;
            this.Gc_Notice.Margin = new System.Windows.Forms.Padding(0);
            this.Gc_Notice.Name = "Gc_Notice";
            this.Gc_Notice.Size = new System.Drawing.Size(516, 170);
            this.Gc_Notice.TabIndex = 13;
            this.Gc_Notice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Notice});
            // 
            // Gv_Notice
            // 
            this.Gv_Notice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.WriteDate,
            this.Title});
            this.Gv_Notice.DetailHeight = 412;
            this.Gv_Notice.GridControl = this.Gc_Notice;
            this.Gv_Notice.Name = "Gv_Notice";
            this.Gv_Notice.OptionsTiles.GroupTextPadding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.Gv_Notice.OptionsTiles.IndentBetweenGroups = 0;
            this.Gv_Notice.OptionsTiles.IndentBetweenItems = 0;
            this.Gv_Notice.OptionsTiles.ItemSize = new System.Drawing.Size(632, 35);
            this.Gv_Notice.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.Gv_Notice.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.Gv_Notice.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.Gv_Notice.OptionsTiles.RowCount = 0;
            tableColumnDefinition1.Length.Value = 105D;
            tableColumnDefinition2.Length.Value = 499D;
            this.Gv_Notice.TileColumns.Add(tableColumnDefinition1);
            this.Gv_Notice.TileColumns.Add(tableColumnDefinition2);
            this.Gv_Notice.TileRows.Add(tableRowDefinition1);
            tileViewItemElement1.Column = this.WriteDate;
            tileViewItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement1.Text = "WriteDate";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.Column = this.Title;
            tileViewItemElement2.ColumnIndex = 1;
            tileViewItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileViewItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
            tileViewItemElement2.Text = "Title";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            this.Gv_Notice.TileTemplate.Add(tileViewItemElement1);
            this.Gv_Notice.TileTemplate.Add(tileViewItemElement2);
            // 
            // GcMachine
            // 
            this.GcMachine.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            gridLevelNode1.RelationName = "Level1";
            this.GcMachine.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GcMachine.Location = new System.Drawing.Point(528, 360);
            this.GcMachine.MainView = this.GvMachine;
            this.GcMachine.Margin = new System.Windows.Forms.Padding(4);
            this.GcMachine.Name = "GcMachine";
            this.GcMachine.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
            this.GcMachine.Size = new System.Drawing.Size(536, 172);
            this.GcMachine.TabIndex = 12;
            this.GcMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvMachine});
            // 
            // GvMachine
            // 
            this.GvMachine.DetailHeight = 412;
            this.GvMachine.GridControl = this.GcMachine;
            this.GvMachine.Name = "GvMachine";
            this.GvMachine.OptionsEditForm.PopupEditFormWidth = 1000;
            this.GvMachine.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // ChartProduction_Day
            // 
            this.ChartProduction_Day.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.ChartProduction_Day.Diagram = xyDiagram1;
            this.ChartProduction_Day.Location = new System.Drawing.Point(1075, 147);
            this.ChartProduction_Day.LookAndFeel.SkinName = "DevExpress Style";
            this.ChartProduction_Day.Margin = new System.Windows.Forms.Padding(4);
            this.ChartProduction_Day.Name = "ChartProduction_Day";
            this.ChartProduction_Day.PaletteName = "Flow";
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Name = "Series 1";
            series2.SeriesID = 0;
            sideBySideBarSeriesView1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series2.View = sideBySideBarSeriesView1;
            this.ChartProduction_Day.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.ChartProduction_Day.Size = new System.Drawing.Size(679, 168);
            this.ChartProduction_Day.TabIndex = 11;
            // 
            // ChartProduction_Time
            // 
            this.ChartProduction_Time.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            this.ChartProduction_Time.Diagram = xyDiagram2;
            this.ChartProduction_Time.Location = new System.Drawing.Point(7, 147);
            this.ChartProduction_Time.LookAndFeel.SkinName = "DevExpress Style";
            this.ChartProduction_Time.Margin = new System.Windows.Forms.Padding(4);
            this.ChartProduction_Time.Name = "ChartProduction_Time";
            this.ChartProduction_Time.PaletteName = "Flow";
            series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.Name = "Series 1";
            series3.SeriesID = 0;
            sideBySideBarSeriesView2.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series3.View = sideBySideBarSeriesView2;
            this.ChartProduction_Time.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.ChartProduction_Time.Size = new System.Drawing.Size(510, 168);
            this.ChartProduction_Time.TabIndex = 7;
            // 
            // Gc_Work
            // 
            this.Gc_Work.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.Gc_Work.Location = new System.Drawing.Point(4, 360);
            this.Gc_Work.MainView = this.Gv_Work;
            this.Gc_Work.Margin = new System.Windows.Forms.Padding(4);
            this.Gc_Work.Name = "Gc_Work";
            this.Gc_Work.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar12});
            this.Gc_Work.Size = new System.Drawing.Size(516, 172);
            this.Gc_Work.TabIndex = 10;
            this.Gc_Work.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Work});
            // 
            // Gv_Work
            // 
            this.Gv_Work.DetailHeight = 412;
            this.Gv_Work.GridControl = this.Gc_Work;
            this.Gv_Work.Name = "Gv_Work";
            this.Gv_Work.OptionsEditForm.PopupEditFormWidth = 1000;
            this.Gv_Work.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar12
            // 
            this.repositoryItemProgressBar12.Name = "repositoryItemProgressBar12";
            // 
            // ChartSales_Cust
            // 
            this.ChartSales_Cust.Location = new System.Drawing.Point(531, 147);
            this.ChartSales_Cust.Margin = new System.Windows.Forms.Padding(4);
            this.ChartSales_Cust.Name = "ChartSales_Cust";
            this.ChartSales_Cust.PaletteName = "In A Fog";
            series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series4.Name = "Series 1";
            series4.SeriesID = 1;
            series4.View = doughnutSeriesView2;
            this.ChartSales_Cust.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series4};
            this.ChartSales_Cust.Size = new System.Drawing.Size(530, 168);
            this.ChartSales_Cust.TabIndex = 9;
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
            this.TPMain.Margin = new System.Windows.Forms.Padding(4);
            this.TPMain.Name = "TPMain";
            this.TPMain.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.TPMain.Size = new System.Drawing.Size(1755, 100);
            this.TPMain.TabIndex = 1;
            // 
            // panelControl9
            // 
            this.TPMain.SetColumn(this.panelControl9, 4);
            this.panelControl9.Controls.Add(this.svgImageBox3);
            this.panelControl9.Controls.Add(this.LblSalesAmt);
            this.panelControl9.Controls.Add(this.labelControl15);
            this.panelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl9.Location = new System.Drawing.Point(1408, 4);
            this.panelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl9.Name = "panelControl9";
            this.TPMain.SetRow(this.panelControl9, 0);
            this.panelControl9.Size = new System.Drawing.Size(343, 92);
            this.panelControl9.TabIndex = 4;
            // 
            // svgImageBox3
            // 
            this.svgImageBox3.Location = new System.Drawing.Point(14, 19);
            this.svgImageBox3.Margin = new System.Windows.Forms.Padding(4);
            this.svgImageBox3.Name = "svgImageBox3";
            this.svgImageBox3.Size = new System.Drawing.Size(65, 56);
            this.svgImageBox3.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox3.SvgImage")));
            this.svgImageBox3.TabIndex = 4;
            this.svgImageBox3.Text = "svgImageBox3";
            // 
            // LblSalesAmt
            // 
            this.LblSalesAmt.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 18F);
            this.LblSalesAmt.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.LblSalesAmt.Appearance.Options.UseFont = true;
            this.LblSalesAmt.Appearance.Options.UseForeColor = true;
            this.LblSalesAmt.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblSalesAmt.Location = new System.Drawing.Point(86, 32);
            this.LblSalesAmt.Margin = new System.Windows.Forms.Padding(4);
            this.LblSalesAmt.Name = "LblSalesAmt";
            this.LblSalesAmt.Size = new System.Drawing.Size(250, 54);
            this.LblSalesAmt.TabIndex = 1;
            this.LblSalesAmt.Text = "-";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl15.Location = new System.Drawing.Point(86, 11);
            this.labelControl15.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(125, 20);
            this.labelControl15.TabIndex = 0;
            this.labelControl15.Text = "생산 지연 건수";
            // 
            // panelControl12
            // 
            this.TPMain.SetColumn(this.panelControl12, 0);
            this.panelControl12.Controls.Add(this.svgImageBox6);
            this.panelControl12.Controls.Add(this.LblWorkQty);
            this.panelControl12.Controls.Add(this.labelControl111);
            this.panelControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl12.Location = new System.Drawing.Point(4, 4);
            this.panelControl12.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl12.Name = "panelControl12";
            this.TPMain.SetRow(this.panelControl12, 0);
            this.panelControl12.Size = new System.Drawing.Size(343, 92);
            this.panelControl12.TabIndex = 0;
            // 
            // svgImageBox6
            // 
            this.svgImageBox6.Location = new System.Drawing.Point(5, 19);
            this.svgImageBox6.Margin = new System.Windows.Forms.Padding(4);
            this.svgImageBox6.Name = "svgImageBox6";
            this.svgImageBox6.Size = new System.Drawing.Size(65, 56);
            this.svgImageBox6.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox6.SvgImage")));
            this.svgImageBox6.TabIndex = 4;
            this.svgImageBox6.Text = "svgImageBox6";
            // 
            // LblWorkQty
            // 
            this.LblWorkQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblWorkQty.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblWorkQty.Appearance.Options.UseFont = true;
            this.LblWorkQty.Appearance.Options.UseForeColor = true;
            this.LblWorkQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblWorkQty.Location = new System.Drawing.Point(78, 31);
            this.LblWorkQty.Margin = new System.Windows.Forms.Padding(4);
            this.LblWorkQty.Name = "LblWorkQty";
            this.LblWorkQty.Size = new System.Drawing.Size(169, 54);
            this.LblWorkQty.TabIndex = 1;
            this.LblWorkQty.Text = "-";
            // 
            // labelControl111
            // 
            this.labelControl111.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl111.Appearance.Options.UseForeColor = true;
            this.labelControl111.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl111.Location = new System.Drawing.Point(80, 11);
            this.labelControl111.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl111.Name = "labelControl111";
            this.labelControl111.Size = new System.Drawing.Size(182, 20);
            this.labelControl111.TabIndex = 0;
            this.labelControl111.Text = "계획 대비 생산률";
            // 
            // panelControl7
            // 
            this.TPMain.SetColumn(this.panelControl7, 3);
            this.panelControl7.Controls.Add(this.svgImageBox2);
            this.panelControl7.Controls.Add(this.LblOpRate);
            this.panelControl7.Controls.Add(this.labelControl12);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl7.Location = new System.Drawing.Point(1057, 4);
            this.panelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl7.Name = "panelControl7";
            this.TPMain.SetRow(this.panelControl7, 0);
            this.panelControl7.Size = new System.Drawing.Size(343, 92);
            this.panelControl7.TabIndex = 3;
            // 
            // svgImageBox2
            // 
            this.svgImageBox2.Location = new System.Drawing.Point(18, 19);
            this.svgImageBox2.Margin = new System.Windows.Forms.Padding(4);
            this.svgImageBox2.Name = "svgImageBox2";
            this.svgImageBox2.Size = new System.Drawing.Size(65, 56);
            this.svgImageBox2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox2.SvgImage")));
            this.svgImageBox2.TabIndex = 3;
            this.svgImageBox2.Text = "svgImageBox2";
            // 
            // LblOpRate
            // 
            this.LblOpRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblOpRate.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LblOpRate.Appearance.Options.UseFont = true;
            this.LblOpRate.Appearance.Options.UseForeColor = true;
            this.LblOpRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblOpRate.Location = new System.Drawing.Point(92, 32);
            this.LblOpRate.Margin = new System.Windows.Forms.Padding(4);
            this.LblOpRate.Name = "LblOpRate";
            this.LblOpRate.Size = new System.Drawing.Size(171, 54);
            this.LblOpRate.TabIndex = 1;
            this.LblOpRate.Text = "-";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Appearance.Options.UseForeColor = true;
            this.labelControl12.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl12.Location = new System.Drawing.Point(92, 11);
            this.labelControl12.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(148, 20);
            this.labelControl12.TabIndex = 0;
            this.labelControl12.Text = "설비가동률";
            // 
            // panelControl3
            // 
            this.TPMain.SetColumn(this.panelControl3, 1);
            this.panelControl3.Controls.Add(this.svgImageBox4);
            this.panelControl3.Controls.Add(this.LblAchieveRate);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(355, 4);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl3.Name = "panelControl3";
            this.TPMain.SetRow(this.panelControl3, 0);
            this.panelControl3.Size = new System.Drawing.Size(343, 92);
            this.panelControl3.TabIndex = 1;
            // 
            // svgImageBox4
            // 
            this.svgImageBox4.Location = new System.Drawing.Point(15, 19);
            this.svgImageBox4.Margin = new System.Windows.Forms.Padding(4);
            this.svgImageBox4.Name = "svgImageBox4";
            this.svgImageBox4.Size = new System.Drawing.Size(65, 56);
            this.svgImageBox4.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox4.SvgImage")));
            this.svgImageBox4.TabIndex = 3;
            this.svgImageBox4.Text = "svgImageBox4";
            // 
            // LblAchieveRate
            // 
            this.LblAchieveRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblAchieveRate.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LblAchieveRate.Appearance.Options.UseFont = true;
            this.LblAchieveRate.Appearance.Options.UseForeColor = true;
            this.LblAchieveRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblAchieveRate.Location = new System.Drawing.Point(88, 31);
            this.LblAchieveRate.Margin = new System.Windows.Forms.Padding(4);
            this.LblAchieveRate.Name = "LblAchieveRate";
            this.LblAchieveRate.Size = new System.Drawing.Size(169, 54);
            this.LblAchieveRate.TabIndex = 1;
            this.LblAchieveRate.Text = "-";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(88, 11);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(169, 20);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "양품률";
            // 
            // panelControl5
            // 
            this.TPMain.SetColumn(this.panelControl5, 2);
            this.panelControl5.Controls.Add(this.svgImageBox1);
            this.panelControl5.Controls.Add(this.LblNgRate);
            this.panelControl5.Controls.Add(this.labelControl9);
            this.panelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl5.Location = new System.Drawing.Point(706, 4);
            this.panelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl5.Name = "panelControl5";
            this.TPMain.SetRow(this.panelControl5, 0);
            this.panelControl5.Size = new System.Drawing.Size(343, 92);
            this.panelControl5.TabIndex = 2;
            // 
            // svgImageBox1
            // 
            this.svgImageBox1.Location = new System.Drawing.Point(15, 19);
            this.svgImageBox1.Margin = new System.Windows.Forms.Padding(4);
            this.svgImageBox1.Name = "svgImageBox1";
            this.svgImageBox1.Size = new System.Drawing.Size(65, 56);
            this.svgImageBox1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox1.SvgImage")));
            this.svgImageBox1.TabIndex = 2;
            this.svgImageBox1.Text = "svgImageBox1";
            // 
            // LblNgRate
            // 
            this.LblNgRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblNgRate.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.LblNgRate.Appearance.Options.UseFont = true;
            this.LblNgRate.Appearance.Options.UseForeColor = true;
            this.LblNgRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblNgRate.Location = new System.Drawing.Point(88, 31);
            this.LblNgRate.Margin = new System.Windows.Forms.Padding(4);
            this.LblNgRate.Name = "LblNgRate";
            this.LblNgRate.Size = new System.Drawing.Size(180, 54);
            this.LblNgRate.TabIndex = 1;
            this.LblNgRate.Text = "-";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.Location = new System.Drawing.Point(88, 11);
            this.labelControl9.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(145, 20);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "평균 사이클 타임";
            // 
            // lineChart
            // 
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            this.lineChart.Diagram = xyDiagram3;
            this.lineChart.Location = new System.Drawing.Point(1075, 363);
            this.lineChart.LookAndFeel.SkinName = "DevExpress Style";
            this.lineChart.Margin = new System.Windows.Forms.Padding(4);
            this.lineChart.Name = "lineChart";
            series5.Name = "Series 1";
            series5.SeriesID = 0;
            series5.View = lineSeriesView1;
            this.lineChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5};
            this.lineChart.SeriesTemplate.View = lineSeriesView2;
            this.lineChart.Size = new System.Drawing.Size(679, 166);
            this.lineChart.TabIndex = 14;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(1761, 748);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "시간대별 생산량 추이";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 106);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(524, 216);
            this.layoutControlGroup3.Text = "시간대별 생산량 추이";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ChartProduction_Time;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(516, 174);
            this.layoutControlItem2.Text = "layoutControlItem1";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.TPMain;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(0, 106);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(9, 106);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1761, 106);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(1068, 106);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(693, 216);
            this.layoutControlGroup2.Text = "일자별 생산 실적 추이";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ChartProduction_Day;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(685, 174);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 322);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Size = new System.Drawing.Size(524, 214);
            this.layoutControlGroup9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Text = "공정별 생산 현황";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.Gc_Work;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(522, 178);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10});
            this.layoutControlGroup7.Location = new System.Drawing.Point(0, 536);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(524, 212);
            this.layoutControlGroup7.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Text = "지연 공정 / 알림";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.Gc_Notice;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(522, 176);
            this.layoutControlItem10.Text = "공지사항";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "제품별 생산 비중";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup6.Location = new System.Drawing.Point(524, 106);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(544, 216);
            this.layoutControlGroup6.Text = "제품별 생산 비중";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ChartSales_Cust;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(536, 174);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup11
            // 
            this.layoutControlGroup11.CustomizationFormText = "불량 현황";
            this.layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup11.Location = new System.Drawing.Point(1068, 322);
            this.layoutControlGroup11.Name = "layoutControlGroup11";
            this.layoutControlGroup11.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup11.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup11.Size = new System.Drawing.Size(693, 214);
            this.layoutControlGroup11.Text = "불량 현황";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lineChart;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(685, 172);
            this.layoutControlItem7.Text = "layoutControlItem8";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup5.Location = new System.Drawing.Point(524, 322);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(544, 214);
            this.layoutControlGroup5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Text = "설비 가동 현황";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.GcMachine;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(542, 178);
            this.layoutControlItem5.Text = "설비 가동 현황";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.CustomizationFormText = "최근 생산 지시 현황";
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.layoutControlGroup8.Location = new System.Drawing.Point(1068, 536);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup8.Size = new System.Drawing.Size(693, 212);
            this.layoutControlGroup8.Text = "최근 생산 지시 현황";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.Gc_OrderProcess;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(685, 170);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            this.layoutControlGroup12.CustomizationFormText = "생산 목표 달성 현황";
            this.layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.layoutControlGroup12.Location = new System.Drawing.Point(524, 536);
            this.layoutControlGroup12.Name = "layoutControlGroup12";
            this.layoutControlGroup12.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup12.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup12.Size = new System.Drawing.Size(544, 212);
            this.layoutControlGroup12.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup12.Text = "생산 목표 달성 현황";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.chartControl1;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(542, 176);
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
            this.smtCommonBar1.Controls.Add(this.btn_Colose);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(2, 2);
            this.smtCommonBar1.Margin = new System.Windows.Forms.Padding(4);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1761, 40);
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
            this.LblCurTime.Location = new System.Drawing.Point(1248, 0);
            this.LblCurTime.Margin = new System.Windows.Forms.Padding(4);
            this.LblCurTime.Name = "LblCurTime";
            this.LblCurTime.Size = new System.Drawing.Size(198, 40);
            this.LblCurTime.TabIndex = 21;
            this.LblCurTime.Text = "2026-07-21 17:05:23";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelControl1.Location = new System.Drawing.Point(1446, 0);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(104, 40);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "자동갱신";
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toggleSwitch1.EditValue = true;
            this.toggleSwitch1.Location = new System.Drawing.Point(1550, 0);
            this.toggleSwitch1.Margin = new System.Windows.Forms.Padding(4);
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.Properties.OffText = "Off";
            this.toggleSwitch1.Properties.OnText = "On";
            this.toggleSwitch1.Size = new System.Drawing.Size(119, 40);
            this.toggleSwitch1.TabIndex = 19;
            this.toggleSwitch1.Toggled += new System.EventHandler(this.toggleSwitch1_Toggled);
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
            this.btn_Colose.Location = new System.Drawing.Point(1669, 0);
            this.btn_Colose.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Colose.Name = "btn_Colose";
            this.btn_Colose.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btn_Colose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Colose.Size = new System.Drawing.Size(92, 40);
            this.btn_Colose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Colose.TabIndex = 1;
            this.btn_Colose.Text = "닫기";
            this.btn_Colose.Click += new System.EventHandler(this.btn_Colose_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1765, 792);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1765, 792);
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
            arcScaleRange1.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange1.EndThickness = 22F;
            arcScaleRange1.EndValue = 33F;
            arcScaleRange1.Name = "Range0";
            arcScaleRange1.ShapeOffset = -4F;
            arcScaleRange1.StartThickness = 22F;
            arcScaleRange1.StartValue = -0.3F;
            arcScaleRange2.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange2.EndThickness = 22F;
            arcScaleRange2.EndValue = 66F;
            arcScaleRange2.Name = "Range1";
            arcScaleRange2.ShapeOffset = -4F;
            arcScaleRange2.StartThickness = 22F;
            arcScaleRange2.StartValue = 33F;
            arcScaleRange3.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange3.EndThickness = 22F;
            arcScaleRange3.EndValue = 100.3F;
            arcScaleRange3.Name = "Range2";
            arcScaleRange3.ShapeOffset = -4F;
            arcScaleRange3.StartThickness = 22F;
            arcScaleRange3.StartValue = 66F;
            this.MachneComponent.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange1,
            arcScaleRange2,
            arcScaleRange3});
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
            arcScaleRange4.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange4.EndThickness = 22F;
            arcScaleRange4.EndValue = 33F;
            arcScaleRange4.Name = "Range0";
            arcScaleRange4.ShapeOffset = -4F;
            arcScaleRange4.StartThickness = 22F;
            arcScaleRange4.StartValue = -0.3F;
            arcScaleRange5.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange5.EndThickness = 22F;
            arcScaleRange5.EndValue = 66F;
            arcScaleRange5.Name = "Range1";
            arcScaleRange5.ShapeOffset = -4F;
            arcScaleRange5.StartThickness = 22F;
            arcScaleRange5.StartValue = 33F;
            arcScaleRange6.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange6.EndThickness = 22F;
            arcScaleRange6.EndValue = 100.3F;
            arcScaleRange6.Name = "Range2";
            arcScaleRange6.ShapeOffset = -4F;
            arcScaleRange6.StartThickness = 22F;
            arcScaleRange6.StartValue = 66F;
            this.arcScaleComponent1.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange4,
            arcScaleRange5,
            arcScaleRange6});
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
            this.arcScaleComponent2.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange7,
            arcScaleRange8,
            arcScaleRange9});
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
            // Frm_PM_ProductionDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1765, 792);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Frm_PM_ProductionDashboard";
            this.Text = "생산 대시보드";
            this.Load += new System.EventHandler(this.Frm_PM_ProductionDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_OrderProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_OrderProcess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar121)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Notice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Notice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartProduction_Day)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartProduction_Time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Work)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Work)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartSales_Cust)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
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
		private baseSMT.smtSearchButton btn_Colose;
		private LayoutControl layoutControl2;
		private LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraCharts.ChartControl ChartProduction_Time;
		private DevExpress.XtraGrid.GridControl Gc_Work;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Work;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar12;
		private DevExpress.XtraCharts.ChartControl ChartSales_Cust;
		private TablePanel TPMain;
		private PanelControl panelControl9;
		private LabelControl LblSalesAmt;
		private LabelControl labelControl15;
		private PanelControl panelControl12;
		private LabelControl LblWorkQty;
		private LabelControl labelControl111;
		private PanelControl panelControl7;
		private LabelControl LblOpRate;
		private LabelControl labelControl12;
		private PanelControl panelControl3;
		private LabelControl LblAchieveRate;
		private LabelControl labelControl6;
		private PanelControl panelControl5;
		private LabelControl LblNgRate;
		private LabelControl labelControl9;
		private LayoutControlGroup layoutControlGroup3;
		private LayoutControlItem layoutControlItem2;
		private LayoutControlGroup layoutControlGroup6;
		private LayoutControlItem layoutControlItem4;
		private LayoutControlItem layoutControlItem6;
		private SvgImageBox svgImageBox1;
		private SvgImageBox svgImageBox2;
		private SvgImageBox svgImageBox3;
		private SvgImageBox svgImageBox4;
		private SvgImageBox svgImageBox6;
		private DevExpress.XtraCharts.ChartControl ChartProduction_Day;
		private LayoutControlGroup layoutControlGroup2;
		private LayoutControlItem layoutControlItem3;
		private LabelControl LblCurTime;
		private DevExpress.XtraGrid.GridControl GcMachine;
		private DevExpress.XtraGrid.Views.Grid.GridView GvMachine;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
		private DevExpress.XtraGrid.GridControl Gc_Notice;
		private LayoutControlGroup layoutControlGroup5;
		private LayoutControlItem layoutControlItem5;
		private LayoutControlGroup layoutControlGroup9;
		private LayoutControlItem layoutControlItem9;
		private LayoutControlGroup layoutControlGroup7;
		private LayoutControlItem layoutControlItem10;
		private DevExpress.XtraGrid.Views.Tile.TileView Gv_Notice;
		private DevExpress.XtraGrid.Columns.TileViewColumn WriteDate;
		private DevExpress.XtraGrid.Columns.TileViewColumn Title;
        private LabelControl labelControl1;
        private ToggleSwitch toggleSwitch1;
        private LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraCharts.ChartControl lineChart;
        private LayoutControlGroup layoutControlGroup11;
        private LayoutControlItem layoutControlItem7;
        private LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraGrid.GridControl Gc_OrderProcess;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_OrderProcess;
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
    }
}