
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;

namespace SmartMES.View.Management
{
	partial class Frm_MM_Dashboard_QM
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
			DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition6 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
			DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition7 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
			DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition8 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
			DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition9 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
			DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition10 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
			DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
			DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement6 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
			DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement7 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
			DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement8 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
			DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement9 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
			DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement10 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
			DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.LineSeriesView lineSeriesView2 = new DevExpress.XtraCharts.LineSeriesView();
			DevExpress.XtraCharts.XYDiagram xyDiagram5 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.XYDiagram xyDiagram6 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series6 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SplineSeriesView splineSeriesView2 = new DevExpress.XtraCharts.SplineSeriesView();
			this.tileViewColumn5 = new DevExpress.XtraGrid.Columns.TileViewColumn();
			this.tileViewColumn1 = new DevExpress.XtraGrid.Columns.TileViewColumn();
			this.tileViewColumn2 = new DevExpress.XtraGrid.Columns.TileViewColumn();
			this.tileViewColumn3 = new DevExpress.XtraGrid.Columns.TileViewColumn();
			this.tileViewColumn4 = new DevExpress.XtraGrid.Columns.TileViewColumn();
			this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
			this.GcNgType = new DevExpress.XtraGrid.GridControl();
			this.tileView1 = new DevExpress.XtraGrid.Views.Tile.TileView();
			this.panelControl11 = new DevExpress.XtraEditors.PanelControl();
			this.LblCurTime = new DevExpress.XtraEditors.LabelControl();
			this.TPMain = new DevExpress.Utils.Layout.TablePanel();
			this.panelControl12 = new DevExpress.XtraEditors.PanelControl();
			this.panelControl13 = new DevExpress.XtraEditors.PanelControl();
			this.LblDefectPerDiff = new DevExpress.XtraEditors.LabelControl();
			this.LblTodayDefectPer = new DevExpress.XtraEditors.LabelControl();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.panelControl9 = new DevExpress.XtraEditors.PanelControl();
			this.panelControl10 = new DevExpress.XtraEditors.PanelControl();
			this.LblPendingIncomingQty = new DevExpress.XtraEditors.LabelControl();
			this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
			this.LblPassRateDiff = new DevExpress.XtraEditors.LabelControl();
			this.LblTodayPassRate = new DevExpress.XtraEditors.LabelControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
			this.LblTopDefectQty = new DevExpress.XtraEditors.LabelControl();
			this.panelControl8 = new DevExpress.XtraEditors.PanelControl();
			this.LblTopDefectName = new DevExpress.XtraEditors.LabelControl();
			this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
			this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
			this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
			this.LblDefectQtyDiff = new DevExpress.XtraEditors.LabelControl();
			this.LblTodayDefectQty = new DevExpress.XtraEditors.LabelControl();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.GcExRst = new DevExpress.XtraGrid.GridControl();
			this.GvExRst = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.ChartNgCnt = new DevExpress.XtraCharts.ChartControl();
			this.ChartNgType = new DevExpress.XtraCharts.ChartControl();
			this.ChartNgPer = new DevExpress.XtraCharts.ChartControl();
			this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
			this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
			this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
			this.Tm_Time = new System.Windows.Forms.Timer(this.components);
			this.Tm_Search = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
			this.layoutControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GcNgType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tileView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl11)).BeginInit();
			this.panelControl11.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TPMain)).BeginInit();
			this.TPMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl12)).BeginInit();
			this.panelControl12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl13)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl9)).BeginInit();
			this.panelControl9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
			this.panelControl7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl8)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
			this.panelControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GcExRst)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GvExRst)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ChartNgCnt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ChartNgType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ChartNgPer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(series6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
			this.SuspendLayout();
			// 
			// tileViewColumn5
			// 
			this.tileViewColumn5.Caption = "순위";
			this.tileViewColumn5.FieldName = "Seq";
			this.tileViewColumn5.Name = "tileViewColumn5";
			this.tileViewColumn5.Visible = true;
			this.tileViewColumn5.VisibleIndex = 0;
			// 
			// tileViewColumn1
			// 
			this.tileViewColumn1.Caption = "불량 유형";
			this.tileViewColumn1.FieldName = "DefectName";
			this.tileViewColumn1.Name = "tileViewColumn1";
			this.tileViewColumn1.Visible = true;
			this.tileViewColumn1.VisibleIndex = 1;
			// 
			// tileViewColumn2
			// 
			this.tileViewColumn2.AppearanceCell.Options.UseTextOptions = true;
			this.tileViewColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.tileViewColumn2.Caption = "불량 수량";
			this.tileViewColumn2.FieldName = "Qty";
			this.tileViewColumn2.Name = "tileViewColumn2";
			this.tileViewColumn2.Visible = true;
			this.tileViewColumn2.VisibleIndex = 2;
			// 
			// tileViewColumn3
			// 
			this.tileViewColumn3.AppearanceCell.Options.UseTextOptions = true;
			this.tileViewColumn3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.tileViewColumn3.Caption = "비율";
			this.tileViewColumn3.FieldName = "Per";
			this.tileViewColumn3.Name = "tileViewColumn3";
			this.tileViewColumn3.Visible = true;
			this.tileViewColumn3.VisibleIndex = 3;
			// 
			// tileViewColumn4
			// 
			this.tileViewColumn4.Caption = "전월 대비";
			this.tileViewColumn4.FieldName = "LastMonthPer";
			this.tileViewColumn4.Name = "tileViewColumn4";
			this.tileViewColumn4.Visible = true;
			this.tileViewColumn4.VisibleIndex = 4;
			// 
			// layoutControl1
			// 
			this.layoutControl1.Controls.Add(this.GcNgType);
			this.layoutControl1.Controls.Add(this.panelControl11);
			this.layoutControl1.Controls.Add(this.TPMain);
			this.layoutControl1.Controls.Add(this.GcExRst);
			this.layoutControl1.Controls.Add(this.ChartNgCnt);
			this.layoutControl1.Controls.Add(this.ChartNgType);
			this.layoutControl1.Controls.Add(this.ChartNgPer);
			this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutControl1.Location = new System.Drawing.Point(0, 0);
			this.layoutControl1.Name = "layoutControl1";
			this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(810, 428, 650, 400);
			this.layoutControl1.Root = this.Root;
			this.layoutControl1.Size = new System.Drawing.Size(1412, 619);
			this.layoutControl1.TabIndex = 0;
			this.layoutControl1.Text = "layoutControl1";
			// 
			// GcNgType
			// 
			this.GcNgType.Location = new System.Drawing.Point(7, 498);
			this.GcNgType.MainView = this.tileView1;
			this.GcNgType.Name = "GcNgType";
			this.GcNgType.Size = new System.Drawing.Size(662, 114);
			this.GcNgType.TabIndex = 12;
			this.GcNgType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileView1});
			// 
			// tileView1
			// 
			this.tileView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			this.tileView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.tileViewColumn5,
            this.tileViewColumn1,
            this.tileViewColumn2,
            this.tileViewColumn3,
            this.tileViewColumn4});
			this.tileView1.GridControl = this.GcNgType;
			this.tileView1.Name = "tileView1";
			this.tileView1.OptionsTiles.ItemSize = new System.Drawing.Size(646, 40);
			this.tileView1.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.Kanban;
			tableColumnDefinition6.Length.Value = 59D;
			tableColumnDefinition7.Length.Value = 189D;
			tableColumnDefinition8.Length.Value = 124D;
			tableColumnDefinition9.Length.Value = 125D;
			tableColumnDefinition10.Length.Value = 125D;
			this.tileView1.TileColumns.Add(tableColumnDefinition6);
			this.tileView1.TileColumns.Add(tableColumnDefinition7);
			this.tileView1.TileColumns.Add(tableColumnDefinition8);
			this.tileView1.TileColumns.Add(tableColumnDefinition9);
			this.tileView1.TileColumns.Add(tableColumnDefinition10);
			this.tileView1.TileRows.Add(tableRowDefinition2);
			tileViewItemElement6.Column = this.tileViewColumn5;
			tileViewItemElement6.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement6.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
			tileViewItemElement6.Text = "tileViewColumn5";
			tileViewItemElement6.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement7.Column = this.tileViewColumn1;
			tileViewItemElement7.ColumnIndex = 1;
			tileViewItemElement7.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement7.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
			tileViewItemElement7.Text = "tileViewColumn1";
			tileViewItemElement7.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement8.Column = this.tileViewColumn2;
			tileViewItemElement8.ColumnIndex = 2;
			tileViewItemElement8.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement8.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
			tileViewItemElement8.Text = "tileViewColumn2";
			tileViewItemElement8.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement9.Column = this.tileViewColumn3;
			tileViewItemElement9.ColumnIndex = 3;
			tileViewItemElement9.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement9.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
			tileViewItemElement9.Text = "tileViewColumn3";
			tileViewItemElement9.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement10.Column = this.tileViewColumn4;
			tileViewItemElement10.ColumnIndex = 4;
			tileViewItemElement10.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			tileViewItemElement10.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Squeeze;
			tileViewItemElement10.Text = "tileViewColumn4";
			tileViewItemElement10.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
			this.tileView1.TileTemplate.Add(tileViewItemElement6);
			this.tileView1.TileTemplate.Add(tileViewItemElement7);
			this.tileView1.TileTemplate.Add(tileViewItemElement8);
			this.tileView1.TileTemplate.Add(tileViewItemElement9);
			this.tileView1.TileTemplate.Add(tileViewItemElement10);
			// 
			// panelControl11
			// 
			this.panelControl11.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl11.Controls.Add(this.LblCurTime);
			this.panelControl11.Location = new System.Drawing.Point(3, 3);
			this.panelControl11.Name = "panelControl11";
			this.panelControl11.Size = new System.Drawing.Size(1406, 33);
			this.panelControl11.TabIndex = 11;
			// 
			// LblCurTime
			// 
			this.LblCurTime.Appearance.Options.UseTextOptions = true;
			this.LblCurTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.LblCurTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblCurTime.Dock = System.Windows.Forms.DockStyle.Right;
			this.LblCurTime.Location = new System.Drawing.Point(1210, 0);
			this.LblCurTime.Name = "LblCurTime";
			this.LblCurTime.Size = new System.Drawing.Size(196, 33);
			this.LblCurTime.TabIndex = 4;
			this.LblCurTime.Text = "-";
			// 
			// TPMain
			// 
			this.TPMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TPMain.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.TPMain.Appearance.Options.UseBackColor = true;
			this.TPMain.AutoScroll = true;
			this.TPMain.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
			this.TPMain.Controls.Add(this.panelControl12);
			this.TPMain.Controls.Add(this.panelControl9);
			this.TPMain.Controls.Add(this.panelControl1);
			this.TPMain.Controls.Add(this.panelControl7);
			this.TPMain.Controls.Add(this.panelControl3);
			this.TPMain.Location = new System.Drawing.Point(3, 42);
			this.TPMain.Name = "TPMain";
			this.TPMain.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
			this.TPMain.Size = new System.Drawing.Size(1406, 108);
			this.TPMain.TabIndex = 1;
			// 
			// panelControl12
			// 
			this.TPMain.SetColumn(this.panelControl12, 2);
			this.panelControl12.Controls.Add(this.panelControl13);
			this.panelControl12.Controls.Add(this.LblDefectPerDiff);
			this.panelControl12.Controls.Add(this.LblTodayDefectPer);
			this.panelControl12.Controls.Add(this.labelControl4);
			this.panelControl12.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl12.Location = new System.Drawing.Point(565, 3);
			this.panelControl12.Name = "panelControl12";
			this.TPMain.SetRow(this.panelControl12, 0);
			this.panelControl12.Size = new System.Drawing.Size(275, 102);
			this.panelControl12.TabIndex = 7;
			// 
			// panelControl13
			// 
			this.panelControl13.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(135)))));
			this.panelControl13.Appearance.Options.UseBackColor = true;
			this.panelControl13.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl13.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl13.Location = new System.Drawing.Point(2, 2);
			this.panelControl13.Margin = new System.Windows.Forms.Padding(0);
			this.panelControl13.Name = "panelControl13";
			this.panelControl13.Size = new System.Drawing.Size(271, 4);
			this.panelControl13.TabIndex = 3;
			// 
			// LblDefectPerDiff
			// 
			this.LblDefectPerDiff.Appearance.ForeColor = System.Drawing.Color.IndianRed;
			this.LblDefectPerDiff.Appearance.Options.UseForeColor = true;
			this.LblDefectPerDiff.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblDefectPerDiff.Location = new System.Drawing.Point(12, 71);
			this.LblDefectPerDiff.Name = "LblDefectPerDiff";
			this.LblDefectPerDiff.Size = new System.Drawing.Size(198, 26);
			this.LblDefectPerDiff.TabIndex = 2;
			this.LblDefectPerDiff.Text = "-";
			// 
			// LblTodayDefectPer
			// 
			this.LblTodayDefectPer.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.LblTodayDefectPer.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(135)))));
			this.LblTodayDefectPer.Appearance.Options.UseFont = true;
			this.LblTodayDefectPer.Appearance.Options.UseForeColor = true;
			this.LblTodayDefectPer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblTodayDefectPer.Location = new System.Drawing.Point(12, 26);
			this.LblTodayDefectPer.Name = "LblTodayDefectPer";
			this.LblTodayDefectPer.Size = new System.Drawing.Size(100, 46);
			this.LblTodayDefectPer.TabIndex = 1;
			this.LblTodayDefectPer.Text = "-";
			// 
			// labelControl4
			// 
			this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl4.Location = new System.Drawing.Point(12, 9);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(83, 17);
			this.labelControl4.TabIndex = 0;
			this.labelControl4.Text = "불량률(월)";
			// 
			// panelControl9
			// 
			this.TPMain.SetColumn(this.panelControl9, 4);
			this.panelControl9.Controls.Add(this.panelControl10);
			this.panelControl9.Controls.Add(this.LblPendingIncomingQty);
			this.panelControl9.Controls.Add(this.labelControl15);
			this.panelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl9.Location = new System.Drawing.Point(1128, 3);
			this.panelControl9.Name = "panelControl9";
			this.TPMain.SetRow(this.panelControl9, 0);
			this.panelControl9.Size = new System.Drawing.Size(275, 102);
			this.panelControl9.TabIndex = 6;
			// 
			// panelControl10
			// 
			this.panelControl10.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(255)))));
			this.panelControl10.Appearance.Options.UseBackColor = true;
			this.panelControl10.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl10.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl10.Location = new System.Drawing.Point(2, 2);
			this.panelControl10.Margin = new System.Windows.Forms.Padding(0);
			this.panelControl10.Name = "panelControl10";
			this.panelControl10.Size = new System.Drawing.Size(271, 4);
			this.panelControl10.TabIndex = 3;
			// 
			// LblPendingIncomingQty
			// 
			this.LblPendingIncomingQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.LblPendingIncomingQty.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(124)))), ((int)(((byte)(255)))));
			this.LblPendingIncomingQty.Appearance.Options.UseFont = true;
			this.LblPendingIncomingQty.Appearance.Options.UseForeColor = true;
			this.LblPendingIncomingQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblPendingIncomingQty.Location = new System.Drawing.Point(12, 26);
			this.LblPendingIncomingQty.Name = "LblPendingIncomingQty";
			this.LblPendingIncomingQty.Size = new System.Drawing.Size(100, 46);
			this.LblPendingIncomingQty.TabIndex = 1;
			this.LblPendingIncomingQty.Text = "-";
			// 
			// labelControl15
			// 
			this.labelControl15.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl15.Location = new System.Drawing.Point(12, 9);
			this.labelControl15.Name = "labelControl15";
			this.labelControl15.Size = new System.Drawing.Size(100, 17);
			this.labelControl15.TabIndex = 0;
			this.labelControl15.Text = "수입검사 대기 건수";
			// 
			// panelControl1
			// 
			this.TPMain.SetColumn(this.panelControl1, 0);
			this.panelControl1.Controls.Add(this.panelControl2);
			this.panelControl1.Controls.Add(this.LblPassRateDiff);
			this.panelControl1.Controls.Add(this.LblTodayPassRate);
			this.panelControl1.Controls.Add(this.labelControl1);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl1.Location = new System.Drawing.Point(3, 3);
			this.panelControl1.Name = "panelControl1";
			this.TPMain.SetRow(this.panelControl1, 0);
			this.panelControl1.Size = new System.Drawing.Size(275, 102);
			this.panelControl1.TabIndex = 4;
			// 
			// panelControl2
			// 
			this.panelControl2.Appearance.BackColor = System.Drawing.Color.Orange;
			this.panelControl2.Appearance.Options.UseBackColor = true;
			this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl2.Location = new System.Drawing.Point(2, 2);
			this.panelControl2.Margin = new System.Windows.Forms.Padding(0);
			this.panelControl2.Name = "panelControl2";
			this.panelControl2.Size = new System.Drawing.Size(271, 4);
			this.panelControl2.TabIndex = 3;
			// 
			// LblPassRateDiff
			// 
			this.LblPassRateDiff.Appearance.ForeColor = System.Drawing.Color.IndianRed;
			this.LblPassRateDiff.Appearance.Options.UseForeColor = true;
			this.LblPassRateDiff.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblPassRateDiff.Location = new System.Drawing.Point(12, 71);
			this.LblPassRateDiff.Name = "LblPassRateDiff";
			this.LblPassRateDiff.Size = new System.Drawing.Size(244, 22);
			this.LblPassRateDiff.TabIndex = 2;
			this.LblPassRateDiff.Text = "-";
			// 
			// LblTodayPassRate
			// 
			this.LblTodayPassRate.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.LblTodayPassRate.Appearance.ForeColor = System.Drawing.Color.Orange;
			this.LblTodayPassRate.Appearance.Options.UseFont = true;
			this.LblTodayPassRate.Appearance.Options.UseForeColor = true;
			this.LblTodayPassRate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblTodayPassRate.Location = new System.Drawing.Point(12, 26);
			this.LblTodayPassRate.Name = "LblTodayPassRate";
			this.LblTodayPassRate.Size = new System.Drawing.Size(100, 46);
			this.LblTodayPassRate.TabIndex = 1;
			this.LblTodayPassRate.Text = "-";
			// 
			// labelControl1
			// 
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl1.Location = new System.Drawing.Point(12, 9);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(83, 17);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "합격률";
			// 
			// panelControl7
			// 
			this.TPMain.SetColumn(this.panelControl7, 3);
			this.panelControl7.Controls.Add(this.LblTopDefectQty);
			this.panelControl7.Controls.Add(this.panelControl8);
			this.panelControl7.Controls.Add(this.LblTopDefectName);
			this.panelControl7.Controls.Add(this.labelControl12);
			this.panelControl7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl7.Location = new System.Drawing.Point(847, 3);
			this.panelControl7.Name = "panelControl7";
			this.TPMain.SetRow(this.panelControl7, 0);
			this.panelControl7.Size = new System.Drawing.Size(275, 102);
			this.panelControl7.TabIndex = 6;
			// 
			// LblTopDefectQty
			// 
			this.LblTopDefectQty.Appearance.ForeColor = System.Drawing.Color.IndianRed;
			this.LblTopDefectQty.Appearance.Options.UseForeColor = true;
			this.LblTopDefectQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblTopDefectQty.Location = new System.Drawing.Point(12, 71);
			this.LblTopDefectQty.Name = "LblTopDefectQty";
			this.LblTopDefectQty.Size = new System.Drawing.Size(118, 22);
			this.LblTopDefectQty.TabIndex = 4;
			this.LblTopDefectQty.Text = "-";
			// 
			// panelControl8
			// 
			this.panelControl8.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(162)))), ((int)(((byte)(255)))));
			this.panelControl8.Appearance.Options.UseBackColor = true;
			this.panelControl8.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl8.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl8.Location = new System.Drawing.Point(2, 2);
			this.panelControl8.Margin = new System.Windows.Forms.Padding(0);
			this.panelControl8.Name = "panelControl8";
			this.panelControl8.Size = new System.Drawing.Size(271, 4);
			this.panelControl8.TabIndex = 3;
			// 
			// LblTopDefectName
			// 
			this.LblTopDefectName.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.LblTopDefectName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(162)))), ((int)(((byte)(255)))));
			this.LblTopDefectName.Appearance.Options.UseFont = true;
			this.LblTopDefectName.Appearance.Options.UseForeColor = true;
			this.LblTopDefectName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblTopDefectName.Location = new System.Drawing.Point(12, 26);
			this.LblTopDefectName.Name = "LblTopDefectName";
			this.LblTopDefectName.Size = new System.Drawing.Size(258, 46);
			this.LblTopDefectName.TabIndex = 1;
			this.LblTopDefectName.Text = "-";
			// 
			// labelControl12
			// 
			this.labelControl12.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl12.Location = new System.Drawing.Point(12, 9);
			this.labelControl12.Name = "labelControl12";
			this.labelControl12.Size = new System.Drawing.Size(118, 17);
			this.labelControl12.TabIndex = 0;
			this.labelControl12.Text = "최다 발생 불량";
			// 
			// panelControl3
			// 
			this.TPMain.SetColumn(this.panelControl3, 1);
			this.panelControl3.Controls.Add(this.panelControl4);
			this.panelControl3.Controls.Add(this.LblDefectQtyDiff);
			this.panelControl3.Controls.Add(this.LblTodayDefectQty);
			this.panelControl3.Controls.Add(this.labelControl6);
			this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl3.Location = new System.Drawing.Point(284, 3);
			this.panelControl3.Name = "panelControl3";
			this.TPMain.SetRow(this.panelControl3, 0);
			this.panelControl3.Size = new System.Drawing.Size(275, 102);
			this.panelControl3.TabIndex = 5;
			// 
			// panelControl4
			// 
			this.panelControl4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
			this.panelControl4.Appearance.Options.UseBackColor = true;
			this.panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.panelControl4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl4.Location = new System.Drawing.Point(2, 2);
			this.panelControl4.Margin = new System.Windows.Forms.Padding(0);
			this.panelControl4.Name = "panelControl4";
			this.panelControl4.Size = new System.Drawing.Size(271, 4);
			this.panelControl4.TabIndex = 3;
			// 
			// LblDefectQtyDiff
			// 
			this.LblDefectQtyDiff.Appearance.ForeColor = System.Drawing.Color.IndianRed;
			this.LblDefectQtyDiff.Appearance.Options.UseForeColor = true;
			this.LblDefectQtyDiff.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblDefectQtyDiff.Location = new System.Drawing.Point(12, 71);
			this.LblDefectQtyDiff.Name = "LblDefectQtyDiff";
			this.LblDefectQtyDiff.Size = new System.Drawing.Size(237, 22);
			this.LblDefectQtyDiff.TabIndex = 2;
			this.LblDefectQtyDiff.Text = "-";
			// 
			// LblTodayDefectQty
			// 
			this.LblTodayDefectQty.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 20F);
			this.LblTodayDefectQty.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
			this.LblTodayDefectQty.Appearance.Options.UseFont = true;
			this.LblTodayDefectQty.Appearance.Options.UseForeColor = true;
			this.LblTodayDefectQty.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.LblTodayDefectQty.Location = new System.Drawing.Point(12, 26);
			this.LblTodayDefectQty.Name = "LblTodayDefectQty";
			this.LblTodayDefectQty.Size = new System.Drawing.Size(100, 46);
			this.LblTodayDefectQty.TabIndex = 1;
			this.LblTodayDefectQty.Text = "-";
			// 
			// labelControl6
			// 
			this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl6.Location = new System.Drawing.Point(12, 9);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(83, 17);
			this.labelControl6.TabIndex = 0;
			this.labelControl6.Text = "불량 수량";
			// 
			// GcExRst
			// 
			this.GcExRst.Location = new System.Drawing.Point(679, 529);
			this.GcExRst.MainView = this.GvExRst;
			this.GcExRst.Name = "GcExRst";
			this.GcExRst.Size = new System.Drawing.Size(722, 79);
			this.GcExRst.TabIndex = 10;
			this.GcExRst.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GvExRst});
			// 
			// GvExRst
			// 
			this.GvExRst.GridControl = this.GcExRst;
			this.GvExRst.Name = "GvExRst";
			// 
			// ChartNgCnt
			// 
			xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
			xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
			this.ChartNgCnt.Diagram = xyDiagram4;
			this.ChartNgCnt.Location = new System.Drawing.Point(11, 356);
			this.ChartNgCnt.Name = "ChartNgCnt";
			series4.Name = "Series 1";
			series4.SeriesID = 0;
			series4.View = lineSeriesView2;
			this.ChartNgCnt.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series4};
			this.ChartNgCnt.Size = new System.Drawing.Size(684, 97);
			this.ChartNgCnt.TabIndex = 9;
			// 
			// ChartNgType
			// 
			xyDiagram5.AxisX.VisibleInPanesSerializable = "-1";
			xyDiagram5.AxisY.VisibleInPanesSerializable = "-1";
			xyDiagram5.Rotated = true;
			this.ChartNgType.Diagram = xyDiagram5;
			this.ChartNgType.Location = new System.Drawing.Point(713, 187);
			this.ChartNgType.Name = "ChartNgType";
			series5.Name = "Series 1";
			series5.SeriesID = 0;
			this.ChartNgType.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5};
			this.ChartNgType.Size = new System.Drawing.Size(692, 270);
			this.ChartNgType.TabIndex = 8;
			// 
			// ChartNgPer
			// 
			xyDiagram6.AxisX.VisibleInPanesSerializable = "-1";
			xyDiagram6.AxisY.VisibleInPanesSerializable = "-1";
			this.ChartNgPer.Diagram = xyDiagram6;
			this.ChartNgPer.Location = new System.Drawing.Point(7, 187);
			this.ChartNgPer.LookAndFeel.SkinName = "DevExpress Style";
			this.ChartNgPer.Name = "ChartNgPer";
			series6.Name = "Series 1";
			series6.SeriesID = 0;
			series6.View = splineSeriesView2;
			this.ChartNgPer.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series6};
			this.ChartNgPer.Size = new System.Drawing.Size(692, 132);
			this.ChartNgPer.TabIndex = 7;
			// 
			// Root
			// 
			this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			this.Root.GroupBordersVisible = false;
			this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.layoutControlGroup3});
			this.Root.Name = "Root";
			this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.Root.Size = new System.Drawing.Size(1412, 619);
			this.Root.TextVisible = false;
			// 
			// layoutControlItem3
			// 
			this.layoutControlItem3.Control = this.panelControl11;
			this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem3.MaxSize = new System.Drawing.Size(0, 39);
			this.layoutControlItem3.MinSize = new System.Drawing.Size(7, 39);
			this.layoutControlItem3.Name = "layoutControlItem3";
			this.layoutControlItem3.Size = new System.Drawing.Size(1412, 39);
			this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem3.TextVisible = false;
			// 
			// layoutControlItem6
			// 
			this.layoutControlItem6.Control = this.TPMain;
			this.layoutControlItem6.Location = new System.Drawing.Point(0, 39);
			this.layoutControlItem6.MaxSize = new System.Drawing.Size(0, 114);
			this.layoutControlItem6.MinSize = new System.Drawing.Size(7, 114);
			this.layoutControlItem6.Name = "layoutControlItem6";
			this.layoutControlItem6.Size = new System.Drawing.Size(1412, 114);
			this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem6.TextVisible = false;
			// 
			// layoutControlGroup3
			// 
			this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
			this.layoutControlGroup3.Location = new System.Drawing.Point(706, 153);
			this.layoutControlGroup3.Name = "layoutControlGroup3";
			this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup3.Size = new System.Drawing.Size(706, 311);
			this.layoutControlGroup3.Text = "불량 유형 파레토";
			// 
			// layoutControlItem2
			// 
			this.layoutControlItem2.Control = this.ChartNgType;
			this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem2.Name = "layoutControlItem2";
			this.layoutControlItem2.Size = new System.Drawing.Size(698, 276);
			this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem2.TextVisible = false;
			// 
			// layoutControlGroup4
			// 
			this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
			this.layoutControlGroup4.Location = new System.Drawing.Point(668, 0);
			this.layoutControlGroup4.Name = "layoutControlGroup4";
			this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup4.Size = new System.Drawing.Size(736, 120);
			this.layoutControlGroup4.Text = "최근 검사 이력";
			// 
			// layoutControlItem5
			// 
			this.layoutControlItem5.Control = this.GcExRst;
			this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem5.Name = "layoutControlItem5";
			this.layoutControlItem5.Size = new System.Drawing.Size(728, 85);
			this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem5.TextVisible = false;
			// 
			// layoutControlGroup5
			// 
			this.layoutControlGroup5.CustomizationFormText = "일별 불량수량 추이(최근 10일)";
			this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4});
			this.layoutControlGroup5.Location = new System.Drawing.Point(0, 138);
			this.layoutControlGroup5.Name = "layoutControlGroup5";
			this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup5.Size = new System.Drawing.Size(698, 138);
			this.layoutControlGroup5.Text = "일별 불량수량 추이(최근 10일)";
			// 
			// layoutControlItem4
			// 
			this.layoutControlItem4.Control = this.ChartNgCnt;
			this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem4.Name = "layoutControlItem4";
			this.layoutControlItem4.Size = new System.Drawing.Size(690, 103);
			this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem4.TextVisible = false;
			// 
			// layoutControlGroup1
			// 
			this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlGroup5});
			this.layoutControlGroup1.Location = new System.Drawing.Point(0, 153);
			this.layoutControlGroup1.Name = "layoutControlGroup1";
			this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup1.Size = new System.Drawing.Size(706, 311);
			this.layoutControlGroup1.Text = "일별 불량률 추이(최근 10일)";
			// 
			// layoutControlItem1
			// 
			this.layoutControlItem1.Control = this.ChartNgPer;
			this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem1.Name = "layoutControlItem1";
			this.layoutControlItem1.Size = new System.Drawing.Size(698, 138);
			this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem1.TextVisible = false;
			// 
			// layoutControlGroup2
			// 
			this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlGroup4});
			this.layoutControlGroup2.Location = new System.Drawing.Point(0, 464);
			this.layoutControlGroup2.Name = "layoutControlGroup2";
			this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
			this.layoutControlGroup2.Size = new System.Drawing.Size(1412, 155);
			this.layoutControlGroup2.Text = "불량유형별 상세 현황";
			// 
			// layoutControlItem7
			// 
			this.layoutControlItem7.Control = this.GcNgType;
			this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
			this.layoutControlItem7.MaxSize = new System.Drawing.Size(668, 0);
			this.layoutControlItem7.MinSize = new System.Drawing.Size(668, 26);
			this.layoutControlItem7.Name = "layoutControlItem7";
			this.layoutControlItem7.Size = new System.Drawing.Size(668, 120);
			this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
			this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
			this.layoutControlItem7.TextVisible = false;
			// 
			// Tm_Time
			// 
			this.Tm_Time.Interval = 1000;
			this.Tm_Time.Tick += new System.EventHandler(this.Tm_Time_Tick);
			// 
			// Tm_Search
			// 
			this.Tm_Search.Interval = 180000;
			this.Tm_Search.Tick += new System.EventHandler(this.Tm_Search_Tick);
			// 
			// Frm_MM_Dashboard_QM
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1412, 619);
			this.Controls.Add(this.layoutControl1);
			this.Name = "Frm_MM_Dashboard_QM";
			this.Text = "Frm_MM_Dashboard_QM";
			this.Load += new System.EventHandler(this.Frm_MM_Dashboard_QM_Load);
			((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
			this.layoutControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GcNgType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tileView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl11)).EndInit();
			this.panelControl11.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TPMain)).EndInit();
			this.TPMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl12)).EndInit();
			this.panelControl12.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl13)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl9)).EndInit();
			this.panelControl9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl10)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
			this.panelControl7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl8)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
			this.panelControl3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GcExRst)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GvExRst)).EndInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(lineSeriesView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ChartNgCnt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ChartNgType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(series6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ChartNgPer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraLayout.LayoutControl layoutControl1;
		private DevExpress.XtraLayout.LayoutControlGroup Root;
		private DevExpress.XtraCharts.ChartControl ChartNgCnt;
		private DevExpress.XtraCharts.ChartControl ChartNgType;
		private DevExpress.XtraCharts.ChartControl ChartNgPer;
		private PanelControl panelControl9;
		private PanelControl panelControl10;
		private LabelControl LblPendingIncomingQty;
		private LabelControl labelControl15;
		private PanelControl panelControl7;
		private PanelControl panelControl8;
		private LabelControl LblTopDefectName;
		private LabelControl labelControl12;
		private PanelControl panelControl1;
		private PanelControl panelControl2;
		private LabelControl LblPassRateDiff;
		private LabelControl LblTodayPassRate;
		private LabelControl labelControl1;
		private PanelControl panelControl3;
		private PanelControl panelControl4;
		private LabelControl LblDefectQtyDiff;
		private LabelControl LblTodayDefectQty;
		private LabelControl labelControl6;
		private LayoutControlItem layoutControlItem2;
		private DevExpress.XtraGrid.GridControl GcExRst;
		private DevExpress.XtraGrid.Views.Grid.GridView GvExRst;
		private LayoutControlItem layoutControlItem5;
		private LayoutControlGroup layoutControlGroup3;
		private LayoutControlGroup layoutControlGroup4;
		private TablePanel TPMain;
		private LayoutControlItem layoutControlItem6;
		private PanelControl panelControl11;
		private LabelControl LblCurTime;
		private LayoutControlItem layoutControlItem3;
		private LabelControl LblTopDefectQty;
		private System.Windows.Forms.Timer Tm_Time;
		private System.Windows.Forms.Timer Tm_Search;
		private PanelControl panelControl12;
		private PanelControl panelControl13;
		private LabelControl LblDefectPerDiff;
		private LabelControl LblTodayDefectPer;
		private LabelControl labelControl4;
		private DevExpress.XtraGrid.GridControl GcNgType;
		private LayoutControlGroup layoutControlGroup5;
		private LayoutControlItem layoutControlItem4;
		private LayoutControlGroup layoutControlGroup1;
		private LayoutControlItem layoutControlItem1;
		private LayoutControlGroup layoutControlGroup2;
		private LayoutControlItem layoutControlItem7;
		private DevExpress.XtraGrid.Views.Tile.TileView tileView1;
		private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn5;
		private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn1;
		private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn2;
		private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn3;
		private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn4;
	}
}