
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.Management
{
	partial class Frm_MM_Purchase_View
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
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView1 = new DevExpress.XtraCharts.DoughnutSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MM_Purchase_View));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.CustChart = new DevExpress.XtraCharts.ChartControl();
            this.ChartDoughnut = new DevExpress.XtraCharts.ChartControl();
            this.MMChart = new DevExpress.XtraCharts.ChartControl();
            this.Gc_MM = new DevExpress.XtraGrid.GridControl();
            this.Gv_MM = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar12 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.TPMain = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl9 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox3 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblNgQty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox6 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblTotalPlanQty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl111 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox2 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblGoodQty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox4 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblTotalWorkQty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl5 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox1 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblAchieveRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.Gc_Cust = new DevExpress.XtraGrid.GridControl();
            this.Gv_Cust = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar121 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.LupItem = new baseSMT.smtSearchLookUp();
            this.LupProcess = new baseSMT.smtSearchLookUp();
            this.barDate = new baseSMT.smtSearchDatetime();
            this.btn_Colose = new baseSMT.smtSearchButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Tm_Time = new System.Windows.Forms.Timer(this.components);
            this.Tm_Search = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartDoughnut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MMChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_MM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_MM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Cust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Cust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar121)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
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
            this.layoutControl1.Size = new System.Drawing.Size(1412, 809);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl2);
            this.panelControl1.Controls.Add(this.smtCommonBar1);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1412, 809);
            this.panelControl1.TabIndex = 1;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.CustChart);
            this.layoutControl2.Controls.Add(this.ChartDoughnut);
            this.layoutControl2.Controls.Add(this.MMChart);
            this.layoutControl2.Controls.Add(this.Gc_MM);
            this.layoutControl2.Controls.Add(this.TPMain);
            this.layoutControl2.Controls.Add(this.Gc_Cust);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 42);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(808, 566, 650, 400);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1408, 765);
            this.layoutControl2.TabIndex = 295;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // CustChart
            // 
            this.CustChart.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram1.Rotated = true;
            this.CustChart.Diagram = xyDiagram1;
            this.CustChart.Location = new System.Drawing.Point(473, 124);
            this.CustChart.LookAndFeel.SkinName = "DevExpress Style";
            this.CustChart.Name = "CustChart";
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            this.CustChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.CustChart.Size = new System.Drawing.Size(461, 276);
            this.CustChart.TabIndex = 16;
            // 
            // ChartDoughnut
            // 
            this.ChartDoughnut.AppearanceNameSerializable = "In A Fog";
            this.ChartDoughnut.Location = new System.Drawing.Point(948, 124);
            this.ChartDoughnut.Name = "ChartDoughnut";
            this.ChartDoughnut.PaletteName = "In A Fog";
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series2.Name = "Series 1";
            series2.SeriesID = 1;
            series2.View = doughnutSeriesView1;
            this.ChartDoughnut.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.ChartDoughnut.Size = new System.Drawing.Size(453, 276);
            this.ChartDoughnut.TabIndex = 15;
            // 
            // MMChart
            // 
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            this.MMChart.Diagram = xyDiagram2;
            this.MMChart.Location = new System.Drawing.Point(7, 124);
            this.MMChart.LookAndFeel.SkinName = "DevExpress Style";
            this.MMChart.Name = "MMChart";
            series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series3.Name = "Series 1";
            series3.SeriesID = 0;
            this.MMChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.MMChart.Size = new System.Drawing.Size(452, 276);
            this.MMChart.TabIndex = 14;
            // 
            // Gc_MM
            // 
            this.Gc_MM.Location = new System.Drawing.Point(7, 441);
            this.Gc_MM.MainView = this.Gv_MM;
            this.Gc_MM.Name = "Gc_MM";
            this.Gc_MM.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar12});
            this.Gc_MM.Size = new System.Drawing.Size(659, 317);
            this.Gc_MM.TabIndex = 10;
            this.Gc_MM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_MM});
            // 
            // Gv_MM
            // 
            this.Gv_MM.GridControl = this.Gc_MM;
            this.Gv_MM.Name = "Gv_MM";
            this.Gv_MM.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar12
            // 
            this.repositoryItemProgressBar12.Name = "repositoryItemProgressBar12";
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
            this.LblNgQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblNgQty.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.LblNgQty.Appearance.Options.UseFont = true;
            this.LblNgQty.Appearance.Options.UseForeColor = true;
            this.LblNgQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblNgQty.Location = new System.Drawing.Point(69, 27);
            this.LblNgQty.Name = "LblNgQty";
            this.LblNgQty.Size = new System.Drawing.Size(154, 46);
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
            this.labelControl15.Size = new System.Drawing.Size(100, 17);
            this.labelControl15.TabIndex = 0;
            this.labelControl15.Text = "공급업체수";
            // 
            // panelControl12
            // 
            this.TPMain.SetColumn(this.panelControl12, 0);
            this.panelControl12.Controls.Add(this.svgImageBox6);
            this.panelControl12.Controls.Add(this.LblTotalPlanQty);
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
            // LblTotalPlanQty
            // 
            this.LblTotalPlanQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblTotalPlanQty.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblTotalPlanQty.Appearance.Options.UseFont = true;
            this.LblTotalPlanQty.Appearance.Options.UseForeColor = true;
            this.LblTotalPlanQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblTotalPlanQty.Location = new System.Drawing.Point(62, 26);
            this.LblTotalPlanQty.Name = "LblTotalPlanQty";
            this.LblTotalPlanQty.Size = new System.Drawing.Size(135, 46);
            this.LblTotalPlanQty.TabIndex = 1;
            this.LblTotalPlanQty.Text = "-";
            // 
            // labelControl111
            // 
            this.labelControl111.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl111.Appearance.Options.UseForeColor = true;
            this.labelControl111.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl111.Location = new System.Drawing.Point(64, 9);
            this.labelControl111.Name = "labelControl111";
            this.labelControl111.Size = new System.Drawing.Size(83, 17);
            this.labelControl111.TabIndex = 0;
            this.labelControl111.Text = "총 발주건수";
            // 
            // panelControl7
            // 
            this.TPMain.SetColumn(this.panelControl7, 3);
            this.panelControl7.Controls.Add(this.svgImageBox2);
            this.panelControl7.Controls.Add(this.LblGoodQty);
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
            // LblGoodQty
            // 
            this.LblGoodQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblGoodQty.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.LblGoodQty.Appearance.Options.UseFont = true;
            this.LblGoodQty.Appearance.Options.UseForeColor = true;
            this.LblGoodQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblGoodQty.Location = new System.Drawing.Point(74, 27);
            this.LblGoodQty.Name = "LblGoodQty";
            this.LblGoodQty.Size = new System.Drawing.Size(195, 46);
            this.LblGoodQty.TabIndex = 1;
            this.LblGoodQty.Text = "-";
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
            this.labelControl12.Text = "총 입고금액";
            // 
            // panelControl3
            // 
            this.TPMain.SetColumn(this.panelControl3, 1);
            this.panelControl3.Controls.Add(this.svgImageBox4);
            this.panelControl3.Controls.Add(this.LblTotalWorkQty);
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
            // LblTotalWorkQty
            // 
            this.LblTotalWorkQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblTotalWorkQty.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LblTotalWorkQty.Appearance.Options.UseFont = true;
            this.LblTotalWorkQty.Appearance.Options.UseForeColor = true;
            this.LblTotalWorkQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblTotalWorkQty.Location = new System.Drawing.Point(70, 26);
            this.LblTotalWorkQty.Name = "LblTotalWorkQty";
            this.LblTotalWorkQty.Size = new System.Drawing.Size(135, 46);
            this.LblTotalWorkQty.TabIndex = 1;
            this.LblTotalWorkQty.Text = "-";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(70, 9);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(98, 17);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "총 매입건수";
            // 
            // panelControl5
            // 
            this.TPMain.SetColumn(this.panelControl5, 2);
            this.panelControl5.Controls.Add(this.svgImageBox1);
            this.panelControl5.Controls.Add(this.LblAchieveRate);
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
            // LblAchieveRate
            // 
            this.LblAchieveRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblAchieveRate.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.LblAchieveRate.Appearance.Options.UseFont = true;
            this.LblAchieveRate.Appearance.Options.UseForeColor = true;
            this.LblAchieveRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblAchieveRate.Location = new System.Drawing.Point(70, 26);
            this.LblAchieveRate.Name = "LblAchieveRate";
            this.LblAchieveRate.Size = new System.Drawing.Size(199, 46);
            this.LblAchieveRate.TabIndex = 1;
            this.LblAchieveRate.Text = "-";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Appearance.Options.UseForeColor = true;
            this.labelControl9.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl9.Location = new System.Drawing.Point(70, 9);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(97, 17);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "총 발주금액";
            // 
            // Gc_Cust
            // 
            this.Gc_Cust.Location = new System.Drawing.Point(680, 441);
            this.Gc_Cust.MainView = this.Gv_Cust;
            this.Gc_Cust.Name = "Gc_Cust";
            this.Gc_Cust.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar121});
            this.Gc_Cust.Size = new System.Drawing.Size(721, 317);
            this.Gc_Cust.TabIndex = 10;
            this.Gc_Cust.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Cust});
            // 
            // Gv_Cust
            // 
            this.Gv_Cust.GridControl = this.Gc_Cust;
            this.Gv_Cust.Name = "Gv_Cust";
            this.Gv_Cust.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemProgressBar121
            // 
            this.repositoryItemProgressBar121.Name = "repositoryItemProgressBar121";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.layoutControlGroup6});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1408, 765);
            this.layoutControlGroup1.TextVisible = false;
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
            this.layoutControlGroup2.Location = new System.Drawing.Point(673, 407);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(735, 358);
            this.layoutControlGroup2.Text = "공급업체별 상세 현황";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.Gc_Cust;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(727, 323);
            this.layoutControlItem3.Text = "layoutControlItem5";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 407);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(673, 358);
            this.layoutControlGroup3.Text = "품목별 매입금액 현황(TOP 10)";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.Gc_MM;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(665, 323);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 90);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup4.Size = new System.Drawing.Size(466, 317);
            this.layoutControlGroup4.Text = "월별 매입금액 추이";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.MMChart;
            this.layoutControlItem8.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(458, 282);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup5.Location = new System.Drawing.Point(466, 90);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(475, 317);
            this.layoutControlGroup5.Text = "공급업체별 매입금액 TOP 5";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.CustChart;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(467, 282);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup6.Location = new System.Drawing.Point(941, 90);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup6.Size = new System.Drawing.Size(467, 317);
            this.layoutControlGroup6.Text = "품목분류별 매입금액 비율";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ChartDoughnut;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(459, 282);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Controls.Add(this.LupItem);
            this.smtCommonBar1.Controls.Add(this.LupProcess);
            this.smtCommonBar1.Controls.Add(this.barDate);
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
            this.smtSearchButton1.Location = new System.Drawing.Point(911, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.smtSearchButton1.TabIndex = 8;
            this.smtSearchButton1.Text = "조회";
            this.smtSearchButton1.Click += new System.EventHandler(this.smtSearchButton1_Click);
            // 
            // LupItem
            // 
            this.LupItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupItem.Location = new System.Drawing.Point(623, 0);
            this.LupItem.LookAndFeel.SkinName = "WXI";
            this.LupItem.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupItem.Name = "LupItem";
            this.LupItem.Size = new System.Drawing.Size(288, 40);
            this.LupItem.smtLabelForeColor = System.Drawing.Color.White;
            this.LupItem.smtLabelTitleValue = "품목";
            this.LupItem.smtLabelTitleVisible = true;
            this.LupItem.smtLabelTitleWidth = 80;
            this.LupItem.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupItem.smtLookUpDataDisplayMember = "";
            this.LupItem.smtLookUpDataSource = null;
            this.LupItem.smtLookUpDataValueMember = "";
            this.LupItem.smtLookUpSelectdIndex = -1;
            this.LupItem.smtLookUpText = "";
            this.LupItem.smtLookUpValue = "";
            this.LupItem.TabIndex = 16;
            this.LupItem.Visible = false;
            // 
            // LupProcess
            // 
            this.LupProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupProcess.Location = new System.Drawing.Point(335, 0);
            this.LupProcess.LookAndFeel.SkinName = "WXI";
            this.LupProcess.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LupProcess.Name = "LupProcess";
            this.LupProcess.Size = new System.Drawing.Size(288, 40);
            this.LupProcess.smtLabelForeColor = System.Drawing.Color.White;
            this.LupProcess.smtLabelTitleValue = "공정";
            this.LupProcess.smtLabelTitleVisible = true;
            this.LupProcess.smtLabelTitleWidth = 80;
            this.LupProcess.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.LupProcess.smtLookUpDataDisplayMember = "";
            this.LupProcess.smtLookUpDataSource = null;
            this.LupProcess.smtLookUpDataValueMember = "";
            this.LupProcess.smtLookUpSelectdIndex = -1;
            this.LupProcess.smtLookUpText = "";
            this.LupProcess.smtLookUpValue = "";
            this.LupProcess.TabIndex = 17;
            this.LupProcess.Visible = false;
            // 
            // barDate
            // 
            this.barDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDate.Location = new System.Drawing.Point(10, 0);
            this.barDate.LookAndFeel.SkinName = "WXI";
            this.barDate.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barDate.Name = "barDate";
            this.barDate.Size = new System.Drawing.Size(325, 40);
            this.barDate.smtComboTitleAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.barDate.smtComboTitleItems = new string[0];
            this.barDate.smtComboTitleValue = "";
            this.barDate.smtComboTitleVisible = false;
            this.barDate.smtComboTitleWidth = 80;
            this.barDate.smtEndDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.barDate.smtEndDateTimeValue = new System.DateTime(2026, 7, 15, 0, 0, 0, 0);
            this.barDate.smtEndDateTimeVisible = true;
            this.barDate.smtLabelForeColor = System.Drawing.Color.White;
            this.barDate.smtLabelTitleValue = "조회일자";
            this.barDate.smtLabelTitleVisible = true;
            this.barDate.smtLabelTitleWidth = 80;
            this.barDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Date;
            this.barDate.smtStartDateTimeValue = new System.DateTime(2026, 7, 15, 0, 0, 0, 0);
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
            this.btn_Colose.Location = new System.Drawing.Point(1331, 0);
            this.btn_Colose.Name = "btn_Colose";
            this.btn_Colose.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Colose.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Colose.Size = new System.Drawing.Size(77, 40);
            this.btn_Colose.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Colose.TabIndex = 1;
            this.btn_Colose.Text = "닫기";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1412, 809);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1412, 809);
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
            // Frm_MM_Purchase_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 809);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_MM_Purchase_View";
            this.Text = "매입분석현황";
            this.Load += new System.EventHandler(this.Frm_AN_DefectType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartDoughnut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MMChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_MM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_MM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Cust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Cust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar121)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
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
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtSearchLookUp LupProcess;
        private baseSMT.smtSearchLookUp LupItem;
        private baseSMT.smtSearchDatetime barDate;
        private baseSMT.smtSearchButton btn_Colose;
        private LayoutControl layoutControl2;
        private LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraCharts.ChartControl MMChart;
        private DevExpress.XtraGrid.GridControl Gc_MM;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_MM;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar12;
        private TablePanel TPMain;
        private PanelControl panelControl9;
        private LabelControl LblNgQty;
        private LabelControl labelControl15;
        private PanelControl panelControl12;
        private LabelControl LblTotalPlanQty;
        private LabelControl labelControl111;
        private PanelControl panelControl7;
        private LabelControl LblGoodQty;
        private LabelControl labelControl12;
        private PanelControl panelControl3;
        private LabelControl LblTotalWorkQty;
        private LabelControl labelControl6;
        private PanelControl panelControl5;
        private LabelControl LblAchieveRate;
        private LabelControl labelControl9;
        private LayoutControlItem layoutControlItem6;
        private SvgImageBox svgImageBox1;
        private SvgImageBox svgImageBox2;
        private SvgImageBox svgImageBox3;
        private SvgImageBox svgImageBox4;
        private SvgImageBox svgImageBox6;
        private DevExpress.XtraCharts.ChartControl ChartDoughnut;
        private DevExpress.XtraGrid.GridControl Gc_Cust;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Cust;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar121;
        private LayoutControlItem layoutControlItem5;
        private LayoutControlItem layoutControlItem3;
        private DevExpress.XtraCharts.ChartControl CustChart;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem8;
        private LayoutControlItem layoutControlItem4;
        private LayoutControlGroup layoutControlGroup2;
        private LayoutControlGroup layoutControlGroup3;
        private LayoutControlGroup layoutControlGroup4;
        private LayoutControlGroup layoutControlGroup5;
        private LayoutControlGroup layoutControlGroup6;
    }
}