
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.Analysis
{
	partial class Frm_AN_DefectType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AN_DefectType));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.DoughnutSeriesView doughnutSeriesView1 = new DevExpress.XtraCharts.DoughnutSeriesView();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.StackedBarSeriesView stackedBarSeriesView1 = new DevExpress.XtraCharts.StackedBarSeriesView();
            this.Tm_Time = new System.Windows.Forms.Timer(this.components);
            this.Tm_Search = new System.Windows.Forms.Timer(this.components);
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.cmdExcel = new baseSMT.smtSearchButton();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.LupProcess = new baseSMT.smtSearchLookUp();
            this.LupItem = new baseSMT.smtSearchLookUp();
            this.barDate = new baseSMT.smtSearchDatetime();
            this.btn_Close = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox6 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblTotalQty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl111 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox3 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblCnt = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.svgImageBox4 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblRate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl6 = new DevExpress.XtraEditors.PanelControl();
            this.LblTopQty = new DevExpress.XtraEditors.LabelControl();
            this.svgImageBox1 = new DevExpress.XtraEditors.SvgImageBox();
            this.LblTopType = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lineChart = new DevExpress.XtraCharts.ChartControl();
            this.ChartRatio = new DevExpress.XtraCharts.ChartControl();
            this.ChartQty = new DevExpress.XtraCharts.ChartControl();
            this.GcDefect = new DevExpress.XtraGrid.GridControl();
            this.GvDefect = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemProgressBar12 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).BeginInit();
            this.panelControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).BeginInit();
            this.panelControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcDefect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvDefect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
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
            this.smtCommonBar1.Controls.Add(this.btn_Close);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1412, 40);
            this.smtCommonBar1.smtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "불량유형별 차트";
            this.smtCommonBar1.smtImageTitleVisible = false;
            this.smtCommonBar1.TabIndex = 294;
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
            this.cmdExcel.Location = new System.Drawing.Point(1234, 0);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.cmdExcel.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.cmdExcel.Size = new System.Drawing.Size(101, 40);
            this.cmdExcel.smtButtonStyle = baseSMT.EnumButtonStyles.엑셀변환;
            this.cmdExcel.TabIndex = 18;
            this.cmdExcel.Text = "엑셀변환";
            this.cmdExcel.Visible = false;
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
            this.smtSearchButton1.Location = new System.Drawing.Point(796, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.smtSearchButton1.TabIndex = 8;
            this.smtSearchButton1.Text = "조회";
            this.smtSearchButton1.Click += new System.EventHandler(this.Tm_Search_Tick);
            // 
            // LupProcess
            // 
            this.LupProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupProcess.Location = new System.Drawing.Point(508, 0);
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
            // 
            // LupItem
            // 
            this.LupItem.Dock = System.Windows.Forms.DockStyle.Left;
            this.LupItem.Location = new System.Drawing.Point(220, 0);
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
            // 
            // barDate
            // 
            this.barDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDate.Location = new System.Drawing.Point(0, 0);
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
            this.barDate.smtEndDateTimeValue = new System.DateTime(2026, 7, 1, 0, 0, 0, 0);
            this.barDate.smtEndDateTimeVisible = false;
            this.barDate.smtLabelForeColor = System.Drawing.Color.White;
            this.barDate.smtLabelTitleValue = "조회일자";
            this.barDate.smtLabelTitleVisible = true;
            this.barDate.smtLabelTitleWidth = 100;
            this.barDate.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.barDate.smtStartDateTimeValue = new System.DateTime(2026, 7, 1, 0, 0, 0, 0);
            this.barDate.smtStartDateTimeVisible = true;
            this.barDate.TabIndex = 9;
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
            this.btn_Close.Location = new System.Drawing.Point(1335, 0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Close.Size = new System.Drawing.Size(77, 40);
            this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "닫기";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.BackColor = System.Drawing.Color.Transparent;
            this.layoutControl1.Controls.Add(this.tablePanel1);
            this.layoutControl1.Controls.Add(this.lineChart);
            this.layoutControl1.Controls.Add(this.ChartRatio);
            this.layoutControl1.Controls.Add(this.ChartQty);
            this.layoutControl1.Controls.Add(this.GcDefect);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(-1195, 363, 938, 655);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1412, 633);
            this.layoutControl1.TabIndex = 295;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.tablePanel1.Appearance.Options.UseBackColor = true;
            this.tablePanel1.AutoScroll = true;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F)});
            this.tablePanel1.Controls.Add(this.panelControl12);
            this.tablePanel1.Controls.Add(this.panelControl2);
            this.tablePanel1.Controls.Add(this.panelControl4);
            this.tablePanel1.Controls.Add(this.panelControl6);
            this.tablePanel1.Location = new System.Drawing.Point(3, 3);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Size = new System.Drawing.Size(1406, 84);
            this.tablePanel1.TabIndex = 19;
            // 
            // panelControl12
            // 
            this.tablePanel1.SetColumn(this.panelControl12, 0);
            this.panelControl12.Controls.Add(this.svgImageBox6);
            this.panelControl12.Controls.Add(this.LblTotalQty);
            this.panelControl12.Controls.Add(this.labelControl111);
            this.panelControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl12.Location = new System.Drawing.Point(3, 3);
            this.panelControl12.Name = "panelControl12";
            this.tablePanel1.SetRow(this.panelControl12, 0);
            this.panelControl12.Size = new System.Drawing.Size(346, 78);
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
            // LblTotalQty
            // 
            this.LblTotalQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblTotalQty.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
            this.LblTotalQty.Appearance.Options.UseFont = true;
            this.LblTotalQty.Appearance.Options.UseForeColor = true;
            this.LblTotalQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblTotalQty.Location = new System.Drawing.Point(62, 26);
            this.LblTotalQty.Name = "LblTotalQty";
            this.LblTotalQty.Size = new System.Drawing.Size(135, 46);
            this.LblTotalQty.TabIndex = 1;
            this.LblTotalQty.Text = "-";
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
            this.labelControl111.Text = "총 불량수량";
            // 
            // panelControl2
            // 
            this.tablePanel1.SetColumn(this.panelControl2, 3);
            this.panelControl2.Controls.Add(this.svgImageBox3);
            this.panelControl2.Controls.Add(this.LblCnt);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(1058, 3);
            this.panelControl2.Name = "panelControl2";
            this.tablePanel1.SetRow(this.panelControl2, 0);
            this.panelControl2.Size = new System.Drawing.Size(346, 78);
            this.panelControl2.TabIndex = 3;
            // 
            // svgImageBox3
            // 
            this.svgImageBox3.Location = new System.Drawing.Point(14, 16);
            this.svgImageBox3.Name = "svgImageBox3";
            this.svgImageBox3.Size = new System.Drawing.Size(52, 48);
            this.svgImageBox3.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgImageBox3.SvgImage")));
            this.svgImageBox3.TabIndex = 7;
            this.svgImageBox3.Text = "svgImageBox3";
            // 
            // LblCnt
            // 
            this.LblCnt.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblCnt.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.LblCnt.Appearance.Options.UseFont = true;
            this.LblCnt.Appearance.Options.UseForeColor = true;
            this.LblCnt.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblCnt.Location = new System.Drawing.Point(72, 27);
            this.LblCnt.Name = "LblCnt";
            this.LblCnt.Size = new System.Drawing.Size(154, 46);
            this.LblCnt.TabIndex = 6;
            this.LblCnt.Text = "-";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(72, 9);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(100, 17);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "불량유형 수";
            // 
            // panelControl4
            // 
            this.tablePanel1.SetColumn(this.panelControl4, 1);
            this.panelControl4.Controls.Add(this.svgImageBox4);
            this.panelControl4.Controls.Add(this.LblRate);
            this.panelControl4.Controls.Add(this.labelControl3);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(355, 3);
            this.panelControl4.Name = "panelControl4";
            this.tablePanel1.SetRow(this.panelControl4, 0);
            this.panelControl4.Size = new System.Drawing.Size(346, 78);
            this.panelControl4.TabIndex = 1;
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
            // LblRate
            // 
            this.LblRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblRate.Appearance.ForeColor = System.Drawing.Color.Green;
            this.LblRate.Appearance.Options.UseFont = true;
            this.LblRate.Appearance.Options.UseForeColor = true;
            this.LblRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblRate.Location = new System.Drawing.Point(70, 26);
            this.LblRate.Name = "LblRate";
            this.LblRate.Size = new System.Drawing.Size(135, 46);
            this.LblRate.TabIndex = 1;
            this.LblRate.Text = "-";
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
            this.labelControl3.Text = "전체 대비 불량률";
            // 
            // panelControl6
            // 
            this.tablePanel1.SetColumn(this.panelControl6, 2);
            this.panelControl6.Controls.Add(this.LblTopQty);
            this.panelControl6.Controls.Add(this.svgImageBox1);
            this.panelControl6.Controls.Add(this.LblTopType);
            this.panelControl6.Controls.Add(this.labelControl4);
            this.panelControl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl6.Location = new System.Drawing.Point(706, 3);
            this.panelControl6.Name = "panelControl6";
            this.tablePanel1.SetRow(this.panelControl6, 0);
            this.panelControl6.Size = new System.Drawing.Size(346, 78);
            this.panelControl6.TabIndex = 2;
            // 
            // LblTopQty
            // 
            this.LblTopQty.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.LblTopQty.Appearance.Options.UseForeColor = true;
            this.LblTopQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblTopQty.Location = new System.Drawing.Point(176, 9);
            this.LblTopQty.Name = "LblTopQty";
            this.LblTopQty.Size = new System.Drawing.Size(96, 22);
            this.LblTopQty.TabIndex = 4;
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
            // LblTopType
            // 
            this.LblTopType.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
            this.LblTopType.Appearance.ForeColor = System.Drawing.Color.DarkOrange;
            this.LblTopType.Appearance.Options.UseFont = true;
            this.LblTopType.Appearance.Options.UseForeColor = true;
            this.LblTopType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblTopType.Location = new System.Drawing.Point(70, 26);
            this.LblTopType.Name = "LblTopType";
            this.LblTopType.Size = new System.Drawing.Size(175, 46);
            this.LblTopType.TabIndex = 1;
            this.LblTopType.Text = "-";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.Location = new System.Drawing.Point(70, 9);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(134, 17);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "최다 불량 유형";
            // 
            // lineChart
            // 
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.lineChart.Diagram = xyDiagram1;
            this.lineChart.Location = new System.Drawing.Point(715, 395);
            this.lineChart.LookAndFeel.SkinName = "DevExpress Style";
            this.lineChart.Name = "lineChart";
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            series1.View = lineSeriesView1;
            this.lineChart.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.lineChart.SeriesTemplate.View = lineSeriesView2;
            this.lineChart.Size = new System.Drawing.Size(690, 231);
            this.lineChart.TabIndex = 14;
            // 
            // ChartRatio
            // 
            this.ChartRatio.AppearanceNameSerializable = "Light";
            this.ChartRatio.AutoLayout = false;
            this.ChartRatio.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.ChartRatio.Legend.Margins.Tag = "";
            this.ChartRatio.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.ChartRatio.Location = new System.Drawing.Point(715, 124);
            this.ChartRatio.Name = "ChartRatio";
            this.ChartRatio.PaletteName = "Pastel Kit";
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series2.Name = "Series 1";
            series2.SeriesID = 1;
            doughnutSeriesView1.Rotation = 270;
            doughnutSeriesView1.TotalLabel.DXFont = new DevExpress.Drawing.DXFont("맑은 고딕", 12F);
            doughnutSeriesView1.TotalLabel.TextColor = System.Drawing.Color.Black;
            series2.View = doughnutSeriesView1;
            this.ChartRatio.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.ChartRatio.Size = new System.Drawing.Size(690, 230);
            this.ChartRatio.TabIndex = 9;
            // 
            // ChartQty
            // 
            this.ChartQty.AppearanceNameSerializable = "Northern Lights";
            this.ChartQty.BorderOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ChartQty.BorderOptions.Thickness = 2;
            this.ChartQty.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.ChartQty.CrosshairEnabled = DevExpress.Utils.DefaultBoolean.False;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            xyDiagram2.DefaultPane.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            xyDiagram2.DefaultPane.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
            this.ChartQty.Diagram = xyDiagram2;
            this.ChartQty.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
            this.ChartQty.Location = new System.Drawing.Point(7, 124);
            this.ChartQty.LookAndFeel.SkinName = "DevExpress Style";
            this.ChartQty.Name = "ChartQty";
            this.ChartQty.PaletteBaseColorNumber = 1;
            this.ChartQty.PaletteName = "Northern Lights";
            series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.Name = "Series 1";
            series3.SeriesID = 0;
            series3.ShowInLegend = false;
            stackedBarSeriesView1.BarWidth = 0.7D;
            stackedBarSeriesView1.Border.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            stackedBarSeriesView1.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            stackedBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            series3.View = stackedBarSeriesView1;
            this.ChartQty.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.ChartQty.Size = new System.Drawing.Size(694, 230);
            this.ChartQty.TabIndex = 7;
            // 
            // GcDefect
            // 
            this.GcDefect.Location = new System.Drawing.Point(7, 395);
            this.GcDefect.MainView = this.GvDefect;
            this.GcDefect.Name = "GcDefect";
            this.GcDefect.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar12});
            this.GcDefect.Size = new System.Drawing.Size(694, 231);
            this.GcDefect.TabIndex = 10;
            this.GcDefect.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvDefect});
            // 
            // GvDefect
            // 
            this.GvDefect.GridControl = this.GcDefect;
            this.GvDefect.Name = "GvDefect";
            // 
            // repositoryItemProgressBar12
            // 
            this.repositoryItemProgressBar12.Name = "repositoryItemProgressBar12";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup1,
            this.layoutControlGroup5,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1412, 633);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "불량유형별 수량";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 90);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(708, 271);
            this.layoutControlGroup2.Text = "불량유형별 수량";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ChartQty;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(700, 236);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8});
            this.layoutControlGroup3.Location = new System.Drawing.Point(708, 361);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(704, 272);
            this.layoutControlGroup3.Text = "불량유형별 추이 (최근 6개월)";
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.lineChart;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(696, 237);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 361);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(708, 272);
            this.layoutControlGroup1.Text = "불량유형별 상세 현황";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.GcDefect;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(700, 237);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "불량유형별 비율";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
            this.layoutControlGroup5.Location = new System.Drawing.Point(708, 90);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(704, 271);
            this.layoutControlGroup5.Text = "불량유형별 비율";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ChartRatio;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(696, 236);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tablePanel1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 90);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(7, 90);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1412, 90);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Frm_AN_DefectType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 673);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Name = "Frm_AN_DefectType";
            this.Text = "불량유형별 차트";
            this.Load += new System.EventHandler(this.Frm_AN_DefectType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl12)).EndInit();
            this.panelControl12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl6)).EndInit();
            this.panelControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.svgImageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(doughnutSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(stackedBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GcDefect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvDefect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Timer Tm_Time;
		private System.Windows.Forms.Timer Tm_Search;
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton cmdExcel;
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtSearchLookUp LupProcess;
        private baseSMT.smtSearchLookUp LupItem;
        private baseSMT.smtSearchDatetime barDate;
        private baseSMT.smtSearchButton btn_Close;
        private LayoutControl layoutControl1;
        private LabelControl LblTopQty;
        private DevExpress.XtraCharts.ChartControl lineChart;
        private DevExpress.XtraCharts.ChartControl ChartRatio;
        private DevExpress.XtraCharts.ChartControl ChartQty;
        private DevExpress.XtraGrid.GridControl GcDefect;
        private DevExpress.XtraGrid.Views.Grid.GridView GvDefect;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar12;
        private LayoutControlGroup Root;
        private LayoutControlGroup layoutControlGroup2;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlGroup layoutControlGroup3;
        private LayoutControlItem layoutControlItem8;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem5;
        private LayoutControlGroup layoutControlGroup5;
        private LayoutControlItem layoutControlItem4;
        private TablePanel tablePanel1;
        private PanelControl panelControl12;
        private SvgImageBox svgImageBox6;
        private LabelControl LblTotalQty;
        private LabelControl labelControl111;
        private PanelControl panelControl4;
        private SvgImageBox svgImageBox4;
        private LabelControl LblRate;
        private LabelControl labelControl3;
        private PanelControl panelControl6;
        private SvgImageBox svgImageBox1;
        private LabelControl LblTopType;
        private LabelControl labelControl4;
        private LayoutControlItem layoutControlItem3;
        private PanelControl panelControl2;
        private SvgImageBox svgImageBox3;
        private LabelControl LblCnt;
        private LabelControl labelControl1;
    }
}