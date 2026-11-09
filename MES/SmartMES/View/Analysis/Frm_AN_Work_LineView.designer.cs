
namespace SmartMES.View.Analysis
{
    partial class Frm_AN_Work_LineView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_AN_Work_LineView));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            this.smtCommonBar1 = new baseSMT.smtCommonBar();
            this.btn_Search = new baseSMT.smtSearchButton();
            this.Lup_Process = new baseSMT.smtSearchLookUp();
            this.Lup_Line = new baseSMT.smtSearchLookUp();
            this.Dt_Date = new baseSMT.smtSearchDatetime();
            this.smtSearchButton1 = new baseSMT.smtSearchButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.tablePanel11 = new DevExpress.Utils.Layout.TablePanel();
            this.BarChart_Nonwork = new DevExpress.XtraCharts.ChartControl();
            this.groupControl11 = new DevExpress.XtraEditors.GroupControl();
            this.Gc_NonWork_Class = new DevExpress.XtraGrid.GridControl();
            this.Gv_NonWork_Class = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn111 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
            this.smtCommonBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel11)).BeginInit();
            this.tablePanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BarChart_Nonwork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl11)).BeginInit();
            this.groupControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_NonWork_Class)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_NonWork_Class)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // smtCommonBar1
            // 
            this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtCommonBar1.Appearance.Options.UseBackColor = true;
            this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.smtCommonBar1.Controls.Add(this.btn_Search);
            this.smtCommonBar1.Controls.Add(this.Lup_Process);
            this.smtCommonBar1.Controls.Add(this.Lup_Line);
            this.smtCommonBar1.Controls.Add(this.Dt_Date);
            this.smtCommonBar1.Controls.Add(this.smtSearchButton1);
            this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
            this.smtCommonBar1.Name = "smtCommonBar1";
            this.smtCommonBar1.Size = new System.Drawing.Size(1240, 40);
            this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
            this.smtCommonBar1.smtImageTitleText = "설비종합효율";
            this.smtCommonBar1.smtImageTitleVisible = true;
            this.smtCommonBar1.TabIndex = 8;
            // 
            // btn_Search
            // 
            this.btn_Search.AllowFocus = false;
            this.btn_Search.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.btn_Search.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Search.Appearance.Options.UseBackColor = true;
            this.btn_Search.Appearance.Options.UseForeColor = true;
            this.btn_Search.AutoSize = true;
            this.btn_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Search.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btn_Search.ImageOptions.ImageIndex = 0;
            this.btn_Search.ImageOptions.ImageToTextIndent = 15;
            this.btn_Search.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.btn_Search.Location = new System.Drawing.Point(776, 0);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btn_Search.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Search.Size = new System.Drawing.Size(77, 40);
            this.btn_Search.smtButtonStyle = baseSMT.EnumButtonStyles.조회;
            this.btn_Search.TabIndex = 32;
            this.btn_Search.Text = "조회";
            this.btn_Search.Click += new System.EventHandler(this.BtnSearch_Rst_Click);
            // 
            // Lup_Process
            // 
            this.Lup_Process.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Process.Location = new System.Drawing.Point(556, 0);
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
            this.Lup_Process.TabIndex = 33;
            // 
            // Lup_Line
            // 
            this.Lup_Line.Dock = System.Windows.Forms.DockStyle.Left;
            this.Lup_Line.Location = new System.Drawing.Point(336, 0);
            this.Lup_Line.LookAndFeel.SkinName = "WXI";
            this.Lup_Line.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Lup_Line.Name = "Lup_Line";
            this.Lup_Line.Size = new System.Drawing.Size(220, 40);
            this.Lup_Line.smtLabelTitleValue = "라인";
            this.Lup_Line.smtLabelTitleVisible = true;
            this.Lup_Line.smtLabelTitleWidth = 80;
            this.Lup_Line.smtLookUpAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.Lup_Line.smtLookUpDataDisplayMember = "품명";
            this.Lup_Line.smtLookUpDataSource = null;
            this.Lup_Line.smtLookUpDataValueMember = "품목코드";
            this.Lup_Line.smtLookUpSelectdIndex = -1;
            this.Lup_Line.smtLookUpText = "";
            this.Lup_Line.smtLookUpValue = "";
            this.Lup_Line.TabIndex = 31;
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
            this.Dt_Date.smtLabelTitleValue = "기준일자";
            this.Dt_Date.smtLabelTitleVisible = true;
            this.Dt_Date.smtLabelTitleWidth = 100;
            this.Dt_Date.smtStartDateTimeEditingTime = baseSMT.DatetimeType.Month;
            this.Dt_Date.smtStartDateTimeValue = new System.DateTime(2025, 3, 21, 14, 56, 30, 715);
            this.Dt_Date.smtStartDateTimeVisible = true;
            this.Dt_Date.TabIndex = 30;
            // 
            // smtSearchButton1
            // 
            this.smtSearchButton1.AllowFocus = false;
            this.smtSearchButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.smtSearchButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.smtSearchButton1.Appearance.Options.UseBackColor = true;
            this.smtSearchButton1.Appearance.Options.UseForeColor = true;
            this.smtSearchButton1.AutoSize = true;
            this.smtSearchButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.smtSearchButton1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.smtSearchButton1.ImageOptions.ImageIndex = 5;
            this.smtSearchButton1.ImageOptions.ImageToTextIndent = 15;
            this.smtSearchButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(21, 21);
            this.smtSearchButton1.Location = new System.Drawing.Point(1163, 0);
            this.smtSearchButton1.Name = "smtSearchButton1";
            this.smtSearchButton1.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.smtSearchButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.smtSearchButton1.Size = new System.Drawing.Size(77, 40);
            this.smtSearchButton1.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
            this.smtSearchButton1.TabIndex = 1;
            this.smtSearchButton1.Text = "닫기";
            this.smtSearchButton1.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tablePanel11);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 40);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1240, 565);
            this.layoutControl1.TabIndex = 9;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1240, 565);
            this.Root.TextVisible = false;
            // 
            // tablePanel11
            // 
            this.tablePanel11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePanel11.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 39.16F)});
            this.tablePanel11.Controls.Add(this.BarChart_Nonwork);
            this.tablePanel11.Controls.Add(this.groupControl11);
            this.tablePanel11.Location = new System.Drawing.Point(3, 3);
            this.tablePanel11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tablePanel11.Name = "tablePanel11";
            this.tablePanel11.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F)});
            this.tablePanel11.Size = new System.Drawing.Size(1234, 559);
            this.tablePanel11.TabIndex = 284;
            // 
            // BarChart_Nonwork
            // 
            this.tablePanel11.SetColumn(this.BarChart_Nonwork, 0);
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.BarChart_Nonwork.Diagram = xyDiagram1;
            this.BarChart_Nonwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BarChart_Nonwork.Legend.DXFont = new DevExpress.Drawing.DXFont("Pretendard SemiBold", 10F, DevExpress.Drawing.DXFontStyle.Bold);
            this.BarChart_Nonwork.Legend.Name = "Default Legend";
            this.BarChart_Nonwork.Location = new System.Drawing.Point(3, 282);
            this.BarChart_Nonwork.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BarChart_Nonwork.Name = "BarChart_Nonwork";
            this.tablePanel11.SetRow(this.BarChart_Nonwork, 1);
            series1.Name = "Series 1";
            series1.SeriesID = 0;
            this.BarChart_Nonwork.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.BarChart_Nonwork.Size = new System.Drawing.Size(1228, 275);
            this.BarChart_Nonwork.TabIndex = 1;
            // 
            // groupControl11
            // 
            this.groupControl11.AppearanceCaption.Font = new System.Drawing.Font("Pretendard SemiBold", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl11.AppearanceCaption.Options.UseFont = true;
            this.tablePanel11.SetColumn(this.groupControl11, 0);
            this.groupControl11.Controls.Add(this.Gc_NonWork_Class);
            this.groupControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl11.Location = new System.Drawing.Point(3, 2);
            this.groupControl11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupControl11.Name = "groupControl11";
            this.tablePanel11.SetRow(this.groupControl11, 0);
            this.groupControl11.Size = new System.Drawing.Size(1228, 276);
            this.groupControl11.TabIndex = 0;
            this.groupControl11.Text = "실적";
            // 
            // Gc_NonWork_Class
            // 
            this.Gc_NonWork_Class.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gc_NonWork_Class.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gc_NonWork_Class.Location = new System.Drawing.Point(2, 29);
            this.Gc_NonWork_Class.MainView = this.Gv_NonWork_Class;
            this.Gc_NonWork_Class.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gc_NonWork_Class.Name = "Gc_NonWork_Class";
            this.Gc_NonWork_Class.Size = new System.Drawing.Size(1224, 245);
            this.Gc_NonWork_Class.TabIndex = 269;
            this.Gc_NonWork_Class.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_NonWork_Class});
            // 
            // Gv_NonWork_Class
            // 
            this.Gv_NonWork_Class.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.Gv_NonWork_Class.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Gv_NonWork_Class.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn9,
            this.gridColumn8,
            this.gridColumn10,
            this.gridColumn111});
            this.Gv_NonWork_Class.DetailHeight = 397;
            this.Gv_NonWork_Class.GridControl = this.Gc_NonWork_Class;
            this.Gv_NonWork_Class.Name = "Gv_NonWork_Class";
            this.Gv_NonWork_Class.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "비가동일자";
            this.gridColumn11.MinWidth = 19;
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "라인";
            this.gridColumn2.MinWidth = 19;
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "공정";
            this.gridColumn3.MinWidth = 19;
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "설비";
            this.gridColumn4.MinWidth = 19;
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "비가동구분";
            this.gridColumn5.MinWidth = 19;
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "비가동명칭";
            this.gridColumn6.MinWidth = 19;
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "시작시간";
            this.gridColumn7.MinWidth = 19;
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "종료시간";
            this.gridColumn9.MinWidth = 19;
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "정지시간(분)";
            this.gridColumn8.MinWidth = 19;
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "비고";
            this.gridColumn10.MinWidth = 19;
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn111
            // 
            this.gridColumn111.Caption = "점유율(%)";
            this.gridColumn111.MinWidth = 19;
            this.gridColumn111.Name = "gridColumn111";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tablePanel11;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1240, 565);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // Frm_AN_Work_LineView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 605);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.smtCommonBar1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Frm_AN_Work_LineView";
            this.Text = "설비종합효율";
            ((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
            this.smtCommonBar1.ResumeLayout(false);
            this.smtCommonBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel11)).EndInit();
            this.tablePanel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarChart_Nonwork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl11)).EndInit();
            this.groupControl11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_NonWork_Class)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_NonWork_Class)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private baseSMT.smtCommonBar smtCommonBar1;
        private baseSMT.smtSearchButton btn_Search;
        private baseSMT.smtSearchLookUp Lup_Line;
        private baseSMT.smtSearchDatetime Dt_Date;
        private baseSMT.smtSearchButton smtSearchButton1;
        private baseSMT.smtSearchLookUp Lup_Process;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.Utils.Layout.TablePanel tablePanel11;
        private DevExpress.XtraCharts.ChartControl BarChart_Nonwork;
        private DevExpress.XtraEditors.GroupControl groupControl11;
        private DevExpress.XtraGrid.GridControl Gc_NonWork_Class;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_NonWork_Class;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn111;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}