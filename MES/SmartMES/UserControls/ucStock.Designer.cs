namespace SmartMES.UserControls
{
    partial class ucStock
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState1 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState2 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState3 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState4 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState5 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState6 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState7 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState8 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState9 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState10 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			DevExpress.XtraGauges.Core.Model.IndicatorState indicatorState11 = new DevExpress.XtraGauges.Core.Model.IndicatorState();
			this.panel3 = new System.Windows.Forms.Panel();
			this.LblSaftyQty = new DevExpress.XtraEditors.LabelControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
			this.stateIndicatorGauge1 = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge();
			this.stateIndicatorComponent1 = new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent();
			this.panel2 = new System.Windows.Forms.Panel();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.LblStockQty = new DevExpress.XtraEditors.LabelControl();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.stateIndicatorComponent1)).BeginInit();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.LblSaftyQty);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(170, 3);
			this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(88, 56);
			this.panel3.TabIndex = 1;
			// 
			// LblSaftyQty
			// 
			this.LblSaftyQty.AllowHtmlString = true;
			this.LblSaftyQty.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LblSaftyQty.Appearance.Options.UseFont = true;
			this.LblSaftyQty.Appearance.Options.UseTextOptions = true;
			this.LblSaftyQty.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.LblSaftyQty.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.LblSaftyQty.Dock = System.Windows.Forms.DockStyle.Right;
			this.LblSaftyQty.Location = new System.Drawing.Point(64, 0);
			this.LblSaftyQty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LblSaftyQty.Name = "LblSaftyQty";
			this.LblSaftyQty.Size = new System.Drawing.Size(24, 17);
			this.LblSaftyQty.TabIndex = 20;
			this.LblSaftyQty.Text = "15.5";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.gaugeControl1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(4, 65);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(158, 40);
			this.panel1.TabIndex = 0;
			// 
			// gaugeControl1
			// 
			this.gaugeControl1.AutoLayout = false;
			this.gaugeControl1.BackColor = System.Drawing.Color.Transparent;
			this.gaugeControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.gaugeControl1.Dock = System.Windows.Forms.DockStyle.Left;
			this.gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.stateIndicatorGauge1});
			this.gaugeControl1.LayoutInterval = 0;
			this.gaugeControl1.LayoutPadding = new DevExpress.XtraGauges.Core.Base.Thickness(0);
			this.gaugeControl1.Location = new System.Drawing.Point(0, 0);
			this.gaugeControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.gaugeControl1.Name = "gaugeControl1";
			this.gaugeControl1.Size = new System.Drawing.Size(41, 40);
			this.gaugeControl1.TabIndex = 17;
			// 
			// stateIndicatorGauge1
			// 
			this.stateIndicatorGauge1.Bounds = new System.Drawing.Rectangle(0, 0, 37, 41);
			this.stateIndicatorGauge1.Indicators.AddRange(new DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent[] {
            this.stateIndicatorComponent1});
			this.stateIndicatorGauge1.Name = "stateIndicatorGauge1";
			// 
			// stateIndicatorComponent1
			// 
			this.stateIndicatorComponent1.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(124F, 124F);
			this.stateIndicatorComponent1.Name = "stateIndicatorComponent2";
			this.stateIndicatorComponent1.Size = new System.Drawing.SizeF(200F, 200F);
			this.stateIndicatorComponent1.StateIndex = 0;
			indicatorState1.Name = "State1";
			indicatorState1.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight2;
			indicatorState2.Name = "State2";
			indicatorState2.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4;
			indicatorState3.Name = "State3";
			indicatorState3.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow3;
			indicatorState4.Name = "State4";
			indicatorState4.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow4;
			indicatorState5.Name = "State5";
			indicatorState5.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow5;
			indicatorState6.Name = "State6";
			indicatorState6.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow6;
			indicatorState7.Name = "State7";
			indicatorState7.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow7;
			indicatorState8.Name = "State8";
			indicatorState8.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow8;
			indicatorState9.Name = "State9";
			indicatorState9.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow9;
			indicatorState10.Name = "State10";
			indicatorState10.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.Arrow10;
			indicatorState11.Name = "Default";
			indicatorState11.ShapeType = DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4;
			this.stateIndicatorComponent1.States.AddRange(new DevExpress.XtraGauges.Core.Model.IIndicatorState[] {
            indicatorState1,
            indicatorState2,
            indicatorState3,
            indicatorState4,
            indicatorState5,
            indicatorState6,
            indicatorState7,
            indicatorState8,
            indicatorState9,
            indicatorState10,
            indicatorState11});
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.labelControl1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(4, 3);
			this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(6, 0, 6, 5);
			this.panel2.Size = new System.Drawing.Size(158, 56);
			this.panel2.TabIndex = 1;
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelControl1.Appearance.Options.UseFont = true;
			this.labelControl1.Appearance.Options.UseTextOptions = true;
			this.labelControl1.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.Word;
			this.labelControl1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
			this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControl1.Location = new System.Drawing.Point(6, 0);
			this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(146, 17);
			this.labelControl1.TabIndex = 13;
			this.labelControl1.Text = "Name";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.63636F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
			this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(262, 108);
			this.tableLayoutPanel1.TabIndex = 22;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.LblStockQty);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(170, 65);
			this.panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.panel4.Name = "panel4";
			this.panel4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.panel4.Size = new System.Drawing.Size(88, 40);
			this.panel4.TabIndex = 1;
			// 
			// LblStockQty
			// 
			this.LblStockQty.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LblStockQty.Appearance.Options.UseFont = true;
			this.LblStockQty.Dock = System.Windows.Forms.DockStyle.Right;
			this.LblStockQty.Location = new System.Drawing.Point(75, 5);
			this.LblStockQty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LblStockQty.Name = "LblStockQty";
			this.LblStockQty.Size = new System.Drawing.Size(13, 32);
			this.LblStockQty.TabIndex = 17;
			this.LblStockQty.Text = "5";
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// ucStock
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ucStock";
			this.Size = new System.Drawing.Size(262, 108);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.stateIndicatorGauge1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.stateIndicatorComponent1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        public DevExpress.XtraEditors.LabelControl LblSaftyQty;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGauges.Win.GaugeControl gaugeControl1;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorGauge stateIndicatorGauge1;
        private DevExpress.XtraGauges.Win.Gauges.State.StateIndicatorComponent stateIndicatorComponent1;
        private System.Windows.Forms.Panel panel2;
        public DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        public DevExpress.XtraEditors.LabelControl LblStockQty;
        private System.Windows.Forms.Timer timer1;
    }
}
