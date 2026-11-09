
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.DTL
{
	partial class Frm_DT_MachineView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DT_MachineView));
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange1 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange2 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange3 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange4 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange5 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange6 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange7 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange8 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange9 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_Work = new DevExpress.XtraGrid.GridControl();
            this.Gv_Work = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar12 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.TPMain = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox5 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblWaitingMachineCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl9 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox3 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblFailureMachineCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox2 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblOperatRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox4 = new DevExpress.XtraEditors.SvgImageBox();
            this.lblRunningMachineCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
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
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Work)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Work)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).BeginInit();
            this.TPMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl9)).BeginInit();
            this.panelControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
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
            this.layoutControl2.Controls.Add(this.Gc_Work);
            this.layoutControl2.Controls.Add(this.TPMain);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 42);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(11, 440, 650, 400);
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(1408, 683);
            this.layoutControl2.TabIndex = 295;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // Gc_Work
            // 
            this.Gc_Work.Location = new System.Drawing.Point(4, 121);
            this.Gc_Work.MainView = this.Gv_Work;
            this.Gc_Work.Name = "Gc_Work";
            this.Gc_Work.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar12});
            this.Gc_Work.Size = new System.Drawing.Size(1400, 558);
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
            // TPMain
            // 
            this.TPMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.TPMain.Appearance.Options.UseBackColor = true;
            this.TPMain.AutoScroll = true;
            this.TPMain.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F)});
            this.TPMain.Controls.Add(this.panelControl2);
            this.TPMain.Controls.Add(this.panelControl9);
            this.TPMain.Controls.Add(this.panelControl7);
            this.TPMain.Controls.Add(this.panelControl3);
            this.TPMain.Location = new System.Drawing.Point(3, 3);
            this.TPMain.Name = "TPMain";
            this.TPMain.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.TPMain.Size = new System.Drawing.Size(1402, 84);
            this.TPMain.TabIndex = 1;
            // 
            // panelControl2
            // 
            this.TPMain.SetColumn(this.panelControl2, 1);
            this.panelControl2.Controls.Add(this.svgImageBox5);
            this.panelControl2.Controls.Add(this.lblWaitingMachineCount);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(354, 3);
            this.panelControl2.Name = "panelControl2";
            this.TPMain.SetRow(this.panelControl2, 0);
            this.panelControl2.Size = new System.Drawing.Size(345, 78);
            this.panelControl2.TabIndex = 4;
            // 
            // svgImageBox5
            // 
            this.svgImageBox5.Location = new System.Drawing.Point(12, 16);
            this.svgImageBox5.Name = "svgImageBox5";
            this.svgImageBox5.Size = new System.Drawing.Size(52, 48);
            this.svgImageBox5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox5.SvgImage")));
            this.svgImageBox5.TabIndex = 3;
            this.svgImageBox5.Text = "svgImageBox5";
            // 
            // lblWaitingMachineCount
            // 
            this.lblWaitingMachineCount.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.lblWaitingMachineCount.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblWaitingMachineCount.Appearance.Options.UseFont = true;
            this.lblWaitingMachineCount.Appearance.Options.UseForeColor = true;
            this.lblWaitingMachineCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblWaitingMachineCount.Location = new System.Drawing.Point(88, 26);
            this.lblWaitingMachineCount.Name = "lblWaitingMachineCount";
            this.lblWaitingMachineCount.Size = new System.Drawing.Size(135, 46);
            this.lblWaitingMachineCount.TabIndex = 1;
            this.lblWaitingMachineCount.Text = "-";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(70, 9);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(135, 17);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "대기";
            // 
            // panelControl9
            // 
            this.panelControl9.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.panelControl9.Appearance.Options.UseBackColor = true;
            this.TPMain.SetColumn(this.panelControl9, 2);
            this.panelControl9.Controls.Add(this.svgImageBox3);
            this.panelControl9.Controls.Add(this.lblFailureMachineCount);
            this.panelControl9.Controls.Add(this.labelControl15);
            this.panelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl9.Location = new System.Drawing.Point(704, 3);
            this.panelControl9.Name = "panelControl9";
            this.TPMain.SetRow(this.panelControl9, 0);
            this.panelControl9.Size = new System.Drawing.Size(345, 78);
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
            // lblFailureMachineCount
            // 
            this.lblFailureMachineCount.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 18F);
            this.lblFailureMachineCount.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.lblFailureMachineCount.Appearance.Options.UseFont = true;
            this.lblFailureMachineCount.Appearance.Options.UseForeColor = true;
            this.lblFailureMachineCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblFailureMachineCount.Location = new System.Drawing.Point(69, 27);
            this.lblFailureMachineCount.Name = "lblFailureMachineCount";
            this.lblFailureMachineCount.Size = new System.Drawing.Size(200, 46);
            this.lblFailureMachineCount.TabIndex = 1;
            this.lblFailureMachineCount.Text = "-";
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
            this.labelControl15.Text = "고장";
            // 
            // panelControl7
            // 
            this.TPMain.SetColumn(this.panelControl7, 3);
            this.panelControl7.Controls.Add(this.svgImageBox2);
            this.panelControl7.Controls.Add(this.lblOperatRate);
            this.panelControl7.Controls.Add(this.labelControl12);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl7.Location = new System.Drawing.Point(1055, 3);
            this.panelControl7.Name = "panelControl7";
            this.TPMain.SetRow(this.panelControl7, 0);
            this.panelControl7.Size = new System.Drawing.Size(345, 78);
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
            this.labelControl12.Text = "평균가동률";
            // 
            // panelControl3
            // 
            this.TPMain.SetColumn(this.panelControl3, 0);
            this.panelControl3.Controls.Add(this.svgImageBox4);
            this.panelControl3.Controls.Add(this.lblRunningMachineCount);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(3, 3);
            this.panelControl3.Name = "panelControl3";
            this.TPMain.SetRow(this.panelControl3, 0);
            this.panelControl3.Size = new System.Drawing.Size(345, 78);
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
            this.lblRunningMachineCount.Location = new System.Drawing.Point(88, 26);
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
            this.labelControl6.Text = "가동중";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlGroup9});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1408, 683);
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
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.CustomizationFormText = "설비별 상태 현황";
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9});
            this.layoutControlGroup9.Location = new System.Drawing.Point(0, 90);
            this.layoutControlGroup9.Name = "layoutControlGroup9";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup9.Size = new System.Drawing.Size(1408, 593);
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
            this.layoutControlItem9.Size = new System.Drawing.Size(1406, 564);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
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
            // Frm_DT_MachineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 727);
            this.Controls.Add(this.layoutControl1);
            this.Name = "Frm_DT_MachineView";
            this.Text = "설비 데이터 조회";
            this.Load += new System.EventHandler(this.Frm_DT_MachineView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Work)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Work)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).EndInit();
            this.TPMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl9)).EndInit();
            this.panelControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
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
		private DevExpress.XtraGrid.GridControl Gc_Work;
		private DevExpress.XtraGrid.Views.Grid.GridView Gv_Work;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar12;
		private TablePanel TPMain;
		private PanelControl panelControl9;
		private LabelControl lblFailureMachineCount;
		private LabelControl labelControl15;
		private PanelControl panelControl7;
		private LabelControl lblOperatRate;
		private LabelControl labelControl12;
		private PanelControl panelControl3;
		private LabelControl lblRunningMachineCount;
		private LabelControl labelControl6;
		private LayoutControlItem layoutControlItem6;
		private SvgImageBox svgImageBox2;
		private SvgImageBox svgImageBox3;
		private SvgImageBox svgImageBox4;
		private LabelControl LblCurTime;
		private LayoutControlGroup layoutControlGroup9;
		private LayoutControlItem layoutControlItem9;
        private LabelControl labelControl1;
        private ToggleSwitch toggleSwitch1;
        private LayoutControlGroup layoutControlGroup10;
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
        private PanelControl panelControl2;
        private SvgImageBox svgImageBox5;
        private LabelControl lblWaitingMachineCount;
        private LabelControl labelControl3;
    }
}