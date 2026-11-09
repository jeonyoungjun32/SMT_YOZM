
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.Monitoring
{
	partial class Frm_EQ_MachineDashboard
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraCharts.Series series19 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView10 = new DevExpress.XtraCharts.DoughnutSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram10 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series20 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView7 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram11 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series21 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView7 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView8 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series22 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView11 = new DevExpress.XtraCharts.DoughnutSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_EQ_MachineDashboard));
            DevExpress.XtraCharts.XYDiagram xyDiagram12 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series23 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView8 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.Series series24 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView12 = new DevExpress.XtraCharts.DoughnutSeriesView();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange28 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange29 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange30 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange31 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange32 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange33 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange34 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange35 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange36 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRunTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblStopTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblOperatingRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.ChartStopType = new DevExpress.XtraCharts.ChartControl();
            this.Gc_Repair = new DevExpress.XtraGrid.GridControl();
            this.Gv_Repair = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar121 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ChartEquipmentRate_Top5 = new DevExpress.XtraCharts.ChartControl();
            this.ChartEquipmentRate = new DevExpress.XtraCharts.ChartControl();
            this.Gc_Work = new DevExpress.XtraGrid.GridControl();
            this.Gv_Work = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar12 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.ChartEquipmentStatus = new DevExpress.XtraCharts.ChartControl();
            this.TPMain = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl9 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox3 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblTotalRepairTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox6 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblTotalMachineCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl111 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox2 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblOperatRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox4 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblRunningMachineCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox1 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblStopMachineCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.ChartMachineRepair = new DevExpress.XtraCharts.ChartControl();
            this.ChartRepairCause = new DevExpress.XtraCharts.ChartControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartStopType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Repair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Repair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar121)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartEquipmentRate_Top5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartEquipmentRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Work)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Work)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartEquipmentStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView11)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.ChartMachineRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartRepairCause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
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
            this.layoutControl1.Size = new System.Drawing.Size(1412, 727);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl2);
            this.panelControl1.Controls.Add(this.smtCommonBar1);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1412, 727);
            this.panelControl1.TabIndex = 1;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.tableLayoutPanel1);
            this.layoutControl2.Controls.Add(this.ChartStopType);
            this.layoutControl2.Controls.Add(this.Gc_Repair);
            this.layoutControl2.Controls.Add(this.ChartEquipmentRate_Top5);
            this.layoutControl2.Controls.Add(this.ChartEquipmentRate);
            this.layoutControl2.Controls.Add(this.Gc_Work);
            this.layoutControl2.Controls.Add(this.ChartEquipmentStatus);
            this.layoutControl2.Controls.Add(this.TPMain);
            this.layoutControl2.Controls.Add(this.ChartMachineRepair);
            this.layoutControl2.Controls.Add(this.ChartRepairCause);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 42);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(11, 440, 650, 400);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1408, 683);
            this.layoutControl2.TabIndex = 295;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(500, 535);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(433, 141);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblRunTime);
            this.panel1.Controls.Add(this.labelControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 135);
            this.panel1.TabIndex = 0;
            // 
            // lblRunTime
            // 
            this.lblRunTime.Appearance.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblRunTime.Appearance.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblRunTime.Appearance.Options.UseFont = true;
            this.lblRunTime.Appearance.Options.UseForeColor = true;
            this.lblRunTime.Location = new System.Drawing.Point(39, 79);
            this.lblRunTime.Name = "lblRunTime";
            this.lblRunTime.Size = new System.Drawing.Size(61, 30);
            this.lblRunTime.TabIndex = 1;
            this.lblRunTime.Text = "- 시간";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(32, 41);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(74, 23);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "가동 시간";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblStopTime);
            this.panel2.Controls.Add(this.labelControl3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(147, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(138, 135);
            this.panel2.TabIndex = 1;
            // 
            // lblStopTime
            // 
            this.lblStopTime.Appearance.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblStopTime.Appearance.ForeColor = System.Drawing.Color.Salmon;
            this.lblStopTime.Appearance.Options.UseFont = true;
            this.lblStopTime.Appearance.Options.UseForeColor = true;
            this.lblStopTime.Location = new System.Drawing.Point(38, 79);
            this.lblStopTime.Name = "lblStopTime";
            this.lblStopTime.Size = new System.Drawing.Size(61, 30);
            this.lblStopTime.TabIndex = 2;
            this.lblStopTime.Text = "- 시간";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(26, 41);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(91, 23);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "비가동 시간";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lblOperatingRate);
            this.panel3.Controls.Add(this.labelControl4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(291, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(139, 135);
            this.panel3.TabIndex = 2;
            // 
            // lblOperatingRate
            // 
            this.lblOperatingRate.Appearance.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblOperatingRate.Appearance.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblOperatingRate.Appearance.Options.UseFont = true;
            this.lblOperatingRate.Appearance.Options.UseForeColor = true;
            this.lblOperatingRate.Location = new System.Drawing.Point(51, 79);
            this.lblOperatingRate.Name = "lblOperatingRate";
            this.lblOperatingRate.Size = new System.Drawing.Size(34, 30);
            this.lblOperatingRate.TabIndex = 3;
            this.lblOperatingRate.Text = "- %";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(43, 41);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(51, 23);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "가동률";
            // 
            // ChartStopType
            // 
            this.ChartStopType.Location = new System.Drawing.Point(7, 535);
            this.ChartStopType.Name = "ChartStopType";
            this.ChartStopType.PaletteName = "In A Fog";
            series19.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series19.Name = "Series 1";
            series19.SeriesID = 1;
            series19.View = doughnutSeriesView10;
            this.ChartStopType.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series19};
            this.ChartStopType.Size = new System.Drawing.Size(479, 141);
            this.ChartStopType.TabIndex = 16;
            // 
            // Gc_Repair
            // 
            this.Gc_Repair.Location = new System.Drawing.Point(947, 535);
            this.Gc_Repair.MainView = this.Gv_Repair;
            this.Gc_Repair.Name = "Gc_Repair";
            this.Gc_Repair.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar121});
            this.Gc_Repair.Size = new System.Drawing.Size(454, 141);
            this.Gc_Repair.TabIndex = 15;
            this.Gc_Repair.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Repair});
            // 
            // Gv_Repair
            // 
            this.Gv_Repair.GridControl = this.Gc_Repair;
            this.Gv_Repair.Name = "Gv_Repair";
            this.Gv_Repair.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar121
            // 
            this.repositoryItemProgressBar121.Name = "repositoryItemProgressBar121";
            // 
            // ChartEquipmentRate_Top5
            // 
            this.ChartEquipmentRate_Top5.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram10.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram10.AxisY.VisibleInPanesSerializable = "-1";
            this.ChartEquipmentRate_Top5.Diagram = xyDiagram10;
            this.ChartEquipmentRate_Top5.Location = new System.Drawing.Point(947, 124);
            this.ChartEquipmentRate_Top5.LookAndFeel.SkinName = "DevExpress Style";
            this.ChartEquipmentRate_Top5.Name = "ChartEquipmentRate_Top5";
            this.ChartEquipmentRate_Top5.PaletteName = "Flow";
            series20.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series20.Name = "Series 1";
            series20.SeriesID = 0;
            sideBySideBarSeriesView7.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series20.View = sideBySideBarSeriesView7;
            this.ChartEquipmentRate_Top5.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series20};
            this.ChartEquipmentRate_Top5.Size = new System.Drawing.Size(454, 160);
            this.ChartEquipmentRate_Top5.TabIndex = 11;
            // 
            // ChartEquipmentRate
            // 
            this.ChartEquipmentRate.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram11.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram11.AxisY.VisibleInPanesSerializable = "-1";
            this.ChartEquipmentRate.Diagram = xyDiagram11;
            this.ChartEquipmentRate.Location = new System.Drawing.Point(7, 124);
            this.ChartEquipmentRate.LookAndFeel.SkinName = "DevExpress Style";
            this.ChartEquipmentRate.Name = "ChartEquipmentRate";
            this.ChartEquipmentRate.PaletteName = "Foundry";
            series21.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series21.Name = "Series 1";
            series21.SeriesID = 0;
            series21.View = lineSeriesView7;
            this.ChartEquipmentRate.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series21};
            this.ChartEquipmentRate.SeriesTemplate.View = lineSeriesView8;
            this.ChartEquipmentRate.Size = new System.Drawing.Size(479, 160);
            this.ChartEquipmentRate.TabIndex = 7;
            // 
            // Gc_Work
            // 
            this.Gc_Work.Location = new System.Drawing.Point(4, 322);
            this.Gc_Work.MainView = this.Gv_Work;
            this.Gc_Work.Name = "Gc_Work";
            this.Gc_Work.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar12});
            this.Gc_Work.Size = new System.Drawing.Size(485, 175);
            this.Gc_Work.TabIndex = 10;
            this.Gc_Work.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Work});
            // 
            // Gv_Work
            // 
            this.Gv_Work.GridControl = this.Gc_Work;
            this.Gv_Work.Name = "Gv_Work";
            this.Gv_Work.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar12
            // 
            this.repositoryItemProgressBar12.Name = "repositoryItemProgressBar12";
            // 
            // ChartEquipmentStatus
            // 
            this.ChartEquipmentStatus.Location = new System.Drawing.Point(500, 124);
            this.ChartEquipmentStatus.Name = "ChartEquipmentStatus";
            this.ChartEquipmentStatus.PaletteName = "In A Fog";
            series22.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series22.Name = "Series 1";
            series22.SeriesID = 1;
            series22.View = doughnutSeriesView11;
            this.ChartEquipmentStatus.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series22};
            this.ChartEquipmentStatus.Size = new System.Drawing.Size(433, 160);
            this.ChartEquipmentStatus.TabIndex = 9;
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
            this.panelControl9.Controls.Add(this.lblTotalRepairTime);
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
            // lblTotalRepairTime
            // 
            this.lblTotalRepairTime.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 18F);
            this.lblTotalRepairTime.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lblTotalRepairTime.Appearance.Options.UseFont = true;
            this.lblTotalRepairTime.Appearance.Options.UseForeColor = true;
            this.lblTotalRepairTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTotalRepairTime.Location = new System.Drawing.Point(69, 27);
            this.lblTotalRepairTime.Name = "lblTotalRepairTime";
            this.lblTotalRepairTime.Size = new System.Drawing.Size(200, 46);
            this.lblTotalRepairTime.TabIndex = 1;
            this.lblTotalRepairTime.Text = "-";
            // 
            // labelControl15
            // 
            this.labelControl15.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl15.Appearance.Options.UseForeColor = true;
            this.labelControl15.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl15.Location = new System.Drawing.Point(69, 9);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(100, 17);
            this.labelControl15.TabIndex = 0;
            this.labelControl15.Text = "총 고장 시간";
            // 
            // panelControl12
            // 
            this.TPMain.SetColumn(this.panelControl12, 0);
            this.panelControl12.Controls.Add(this.svgImageBox6);
            this.panelControl12.Controls.Add(this.lblTotalMachineCount);
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
            // lblTotalMachineCount
            // 
            this.lblTotalMachineCount.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.lblTotalMachineCount.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTotalMachineCount.Appearance.Options.UseFont = true;
            this.lblTotalMachineCount.Appearance.Options.UseForeColor = true;
            this.lblTotalMachineCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTotalMachineCount.Location = new System.Drawing.Point(62, 26);
            this.lblTotalMachineCount.Name = "lblTotalMachineCount";
            this.lblTotalMachineCount.Size = new System.Drawing.Size(135, 46);
            this.lblTotalMachineCount.TabIndex = 1;
            this.lblTotalMachineCount.Text = "-";
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
            this.labelControl111.Text = "전체 설비 수";
            // 
            // panelControl7
            // 
            this.TPMain.SetColumn(this.panelControl7, 3);
            this.panelControl7.Controls.Add(this.svgImageBox2);
            this.panelControl7.Controls.Add(this.lblOperatRate);
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
            // lblOperatRate
            // 
            this.lblOperatRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.lblOperatRate.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblOperatRate.Appearance.Options.UseFont = true;
            this.lblOperatRate.Appearance.Options.UseForeColor = true;
            this.lblOperatRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblOperatRate.Location = new System.Drawing.Point(74, 27);
            this.lblOperatRate.Name = "lblOperatRate";
            this.lblOperatRate.Size = new System.Drawing.Size(137, 46);
            this.lblOperatRate.TabIndex = 1;
            this.lblOperatRate.Text = "-";
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
            this.labelControl12.Text = "전체 가동률";
            // 
            // panelControl3
            // 
            this.TPMain.SetColumn(this.panelControl3, 1);
            this.panelControl3.Controls.Add(this.svgImageBox4);
            this.panelControl3.Controls.Add(this.lblRunningMachineCount);
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
            // lblRunningMachineCount
            // 
            this.lblRunningMachineCount.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.lblRunningMachineCount.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblRunningMachineCount.Appearance.Options.UseFont = true;
            this.lblRunningMachineCount.Appearance.Options.UseForeColor = true;
            this.lblRunningMachineCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblRunningMachineCount.Location = new System.Drawing.Point(70, 26);
            this.lblRunningMachineCount.Name = "lblRunningMachineCount";
            this.lblRunningMachineCount.Size = new System.Drawing.Size(135, 46);
            this.lblRunningMachineCount.TabIndex = 1;
            this.lblRunningMachineCount.Text = "-";
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
            this.labelControl6.Text = "가동 설비";
            // 
            // panelControl5
            // 
            this.TPMain.SetColumn(this.panelControl5, 2);
            this.panelControl5.Controls.Add(this.svgImageBox1);
            this.panelControl5.Controls.Add(this.lblStopMachineCount);
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
            // lblStopMachineCount
            // 
            this.lblStopMachineCount.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.lblStopMachineCount.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.lblStopMachineCount.Appearance.Options.UseFont = true;
            this.lblStopMachineCount.Appearance.Options.UseForeColor = true;
            this.lblStopMachineCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStopMachineCount.Location = new System.Drawing.Point(70, 26);
            this.lblStopMachineCount.Name = "lblStopMachineCount";
            this.lblStopMachineCount.Size = new System.Drawing.Size(144, 46);
            this.lblStopMachineCount.TabIndex = 1;
            this.lblStopMachineCount.Text = "-";
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
            this.labelControl9.Text = "비가동 설비";
            // 
            // ChartMachineRepair
            // 
            this.ChartMachineRepair.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram12.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram12.AxisY.VisibleInPanesSerializable = "-1";
            this.ChartMachineRepair.Diagram = xyDiagram12;
            this.ChartMachineRepair.Location = new System.Drawing.Point(500, 325);
            this.ChartMachineRepair.LookAndFeel.SkinName = "DevExpress Style";
            this.ChartMachineRepair.Name = "ChartMachineRepair";
            this.ChartMachineRepair.PaletteName = "Flow";
            series23.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series23.Name = "Series 1";
            series23.SeriesID = 0;
            sideBySideBarSeriesView8.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            series23.View = sideBySideBarSeriesView8;
            this.ChartMachineRepair.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series23};
            this.ChartMachineRepair.Size = new System.Drawing.Size(433, 169);
            this.ChartMachineRepair.TabIndex = 11;
            // 
            // ChartRepairCause
            // 
            this.ChartRepairCause.Location = new System.Drawing.Point(947, 325);
            this.ChartRepairCause.Name = "ChartRepairCause";
            this.ChartRepairCause.PaletteName = "In A Fog";
            series24.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series24.Name = "Series 1";
            series24.SeriesID = 1;
            series24.View = doughnutSeriesView12;
            this.ChartRepairCause.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series24};
            this.ChartRepairCause.Size = new System.Drawing.Size(454, 169);
            this.ChartRepairCause.TabIndex = 16;
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
            this.layoutControlGroup6,
            this.layoutControlGroup8,
            this.layoutControlGroup13,
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.layoutControlGroup7});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1408, 683);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "시간대별 생산량 추이";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 90);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(493, 201);
            this.layoutControlGroup3.Text = "설비 가동률 추이";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ChartEquipmentRate;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(485, 166);
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
            this.layoutControlGroup2.Location = new System.Drawing.Point(940, 90);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(468, 201);
            this.layoutControlGroup2.Text = "설비별 가동률 TOP 5";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ChartEquipmentRate_Top5;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(460, 166);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.CustomizationFormText = "설비별 상태 현황";
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 291);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Size = new System.Drawing.Size(493, 210);
            this.layoutControlGroup9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Text = "설비별 생산 현황";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.Gc_Work;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(491, 181);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "제품별 생산 비중";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup6.Location = new System.Drawing.Point(493, 90);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(447, 201);
            this.layoutControlGroup6.Text = "설비 상태 현황";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ChartEquipmentStatus;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(439, 166);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.CustomizationFormText = "최근 생산 지시 현황";
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.layoutControlGroup8.Location = new System.Drawing.Point(940, 501);
            this.layoutControlGroup8.Name = "layoutControlGroup8";
            this.layoutControlGroup8.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup8.Size = new System.Drawing.Size(468, 182);
            this.layoutControlGroup8.Text = "금일 고장 알람 (실시간)";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.Gc_Repair;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(460, 147);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup13
            // 
            this.layoutControlGroup13.CustomizationFormText = "고장 발생 현황";
            this.layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12});
            this.layoutControlGroup13.Location = new System.Drawing.Point(493, 291);
            this.layoutControlGroup13.Name = "layoutControlGroup13";
            this.layoutControlGroup13.OptionsItemText.TextToControlDistance = 6;
            this.layoutControlGroup13.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup13.Size = new System.Drawing.Size(447, 210);
            this.layoutControlGroup13.Text = "고장 발생 현황 (당월)";
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.ChartMachineRepair;
            this.layoutControlItem12.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(439, 175);
            this.layoutControlItem12.Text = "layoutControlItem3";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7});
            this.layoutControlGroup4.Location = new System.Drawing.Point(493, 501);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(447, 182);
            this.layoutControlGroup4.Text = "설비 가동 시간 현황";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.tableLayoutPanel1;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(439, 147);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 501);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(493, 182);
            this.layoutControlGroup5.Text = "비가동 유형 현황";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.ChartStopType;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(485, 147);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup7.Location = new System.Drawing.Point(940, 291);
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup7.Size = new System.Drawing.Size(468, 210);
            this.layoutControlGroup7.Text = "고장 유형 현황 (당월)";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ChartRepairCause;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(460, 175);
            this.layoutControlItem5.Text = "layoutControlItem11";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
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
            this.btn_Colose.Location = new System.Drawing.Point(1331, 0);
            this.btn_Colose.Name = "btn_Colose";
            this.btn_Colose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Colose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Colose.Size = new System.Drawing.Size(77, 40);
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
            this.Root.Size = new System.Drawing.Size(1412, 727);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1412, 727);
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
            arcScaleRange28.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange28.EndThickness = 22F;
            arcScaleRange28.EndValue = 33F;
            arcScaleRange28.Name = "Range0";
            arcScaleRange28.ShapeOffset = -4F;
            arcScaleRange28.StartThickness = 22F;
            arcScaleRange28.StartValue = -0.3F;
            arcScaleRange29.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange29.EndThickness = 22F;
            arcScaleRange29.EndValue = 66F;
            arcScaleRange29.Name = "Range1";
            arcScaleRange29.ShapeOffset = -4F;
            arcScaleRange29.StartThickness = 22F;
            arcScaleRange29.StartValue = 33F;
            arcScaleRange30.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange30.EndThickness = 22F;
            arcScaleRange30.EndValue = 100.3F;
            arcScaleRange30.Name = "Range2";
            arcScaleRange30.ShapeOffset = -4F;
            arcScaleRange30.StartThickness = 22F;
            arcScaleRange30.StartValue = 66F;
            this.MachneComponent.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange28,
            arcScaleRange29,
            arcScaleRange30});
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
            arcScaleRange31.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange31.EndThickness = 22F;
            arcScaleRange31.EndValue = 33F;
            arcScaleRange31.Name = "Range0";
            arcScaleRange31.ShapeOffset = -4F;
            arcScaleRange31.StartThickness = 22F;
            arcScaleRange31.StartValue = -0.3F;
            arcScaleRange32.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange32.EndThickness = 22F;
            arcScaleRange32.EndValue = 66F;
            arcScaleRange32.Name = "Range1";
            arcScaleRange32.ShapeOffset = -4F;
            arcScaleRange32.StartThickness = 22F;
            arcScaleRange32.StartValue = 33F;
            arcScaleRange33.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange33.EndThickness = 22F;
            arcScaleRange33.EndValue = 100.3F;
            arcScaleRange33.Name = "Range2";
            arcScaleRange33.ShapeOffset = -4F;
            arcScaleRange33.StartThickness = 22F;
            arcScaleRange33.StartValue = 66F;
            this.arcScaleComponent1.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange31,
            arcScaleRange32,
            arcScaleRange33});
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
            arcScaleRange34.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#9BBB59");
            arcScaleRange34.EndThickness = 22F;
            arcScaleRange34.EndValue = 33F;
            arcScaleRange34.Name = "Range0";
            arcScaleRange34.ShapeOffset = -4F;
            arcScaleRange34.StartThickness = 22F;
            arcScaleRange34.StartValue = -0.3F;
            arcScaleRange35.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#F4F56B");
            arcScaleRange35.EndThickness = 22F;
            arcScaleRange35.EndValue = 66F;
            arcScaleRange35.Name = "Range1";
            arcScaleRange35.ShapeOffset = -4F;
            arcScaleRange35.StartThickness = 22F;
            arcScaleRange35.StartValue = 33F;
            arcScaleRange36.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:#E73141");
            arcScaleRange36.EndThickness = 22F;
            arcScaleRange36.EndValue = 100.3F;
            arcScaleRange36.Name = "Range2";
            arcScaleRange36.ShapeOffset = -4F;
            arcScaleRange36.StartThickness = 22F;
            arcScaleRange36.StartValue = 66F;
            this.arcScaleComponent2.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange34,
            arcScaleRange35,
            arcScaleRange36});
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
            // Frm_EQ_MachineDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 727);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_EQ_MachineDashboard";
            this.Text = "설비 대시보드";
            this.Load += new System.EventHandler(this.Frm_PM_ProductionDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartStopType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Repair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Repair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar121)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartEquipmentRate_Top5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartEquipmentRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Work)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Work)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartEquipmentStatus)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(xyDiagram12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartMachineRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartRepairCause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
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
		private DevExpress.XtraCharts.ChartControl ChartEquipmentRate;
		private DevExpress.XtraGrid.GridControl Gc_Work;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Work;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar12;
		private DevExpress.XtraCharts.ChartControl ChartEquipmentStatus;
		private TablePanel TPMain;
		private PanelControl panelControl9;
		private LabelControl lblTotalRepairTime;
		private LabelControl labelControl15;
		private PanelControl panelControl12;
		private LabelControl lblTotalMachineCount;
		private LabelControl labelControl111;
		private PanelControl panelControl7;
		private LabelControl lblOperatRate;
		private LabelControl labelControl12;
		private PanelControl panelControl3;
		private LabelControl lblRunningMachineCount;
		private LabelControl labelControl6;
		private PanelControl panelControl5;
		private LabelControl lblStopMachineCount;
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
		private DevExpress.XtraCharts.ChartControl ChartEquipmentRate_Top5;
		private LayoutControlGroup layoutControlGroup2;
		private LayoutControlItem layoutControlItem3;
		private LabelControl LblCurTime;
		private LayoutControlGroup layoutControlGroup9;
		private LayoutControlItem layoutControlItem9;
        private LabelControl labelControl1;
        private ToggleSwitch toggleSwitch1;
        private LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraCharts.ChartControl ChartStopType;
        private DevExpress.XtraGrid.GridControl Gc_Repair;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Repair;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar121;
        private LayoutControlGroup layoutControlGroup8;
        private LayoutControlItem layoutControlItem8;
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
        private DevExpress.XtraCharts.ChartControl ChartMachineRepair;
        private LayoutControlGroup layoutControlGroup13;
        private LayoutControlItem layoutControlItem12;
        private DevExpress.XtraCharts.ChartControl ChartRepairCause;
        private LayoutControlItem layoutControlItem5;
        private LayoutControlGroup layoutControlGroup4;
        private LayoutControlGroup layoutControlGroup5;
        private LayoutControlItem layoutControlItem11;
        private LayoutControlGroup layoutControlGroup7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LayoutControlItem layoutControlItem7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private LabelControl lblRunTime;
        private LabelControl lblStopTime;
        private LabelControl lblOperatingRate;
    }
}