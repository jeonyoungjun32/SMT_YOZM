
namespace SmartMES.View.Monitoring
{
    partial class FrmCNCProduct
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCNCProduct));
			this.gcMachine = new DevExpress.XtraGrid.GridControl();
			this.gvMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
			this.LblTime = new DevExpress.XtraEditors.LabelControl();
			this.PnlStatus = new DevExpress.Utils.Layout.TablePanel();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.LblMainTitle = new DevExpress.XtraEditors.LabelControl();
			this.TmTime = new System.Windows.Forms.Timer(this.components);
			this.Tm_Search = new System.Windows.Forms.Timer(this.components);
			this.btn_Close = new baseSMT.smtSearchButton();
			this.smtCommonBar1 = new baseSMT.smtCommonBar();
			((System.ComponentModel.ISupportInitialize)(this.gcMachine)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gvMachine)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
			this.panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
			this.tablePanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PnlStatus)).BeginInit();
			this.PnlStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).BeginInit();
			this.smtCommonBar1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gcMachine
			// 
			this.gcMachine.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gcMachine.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gcMachine.Location = new System.Drawing.Point(0, 100);
			this.gcMachine.MainView = this.gvMachine;
			this.gcMachine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.gcMachine.Name = "gcMachine";
			this.gcMachine.Size = new System.Drawing.Size(1451, 586);
			this.gcMachine.TabIndex = 91;
			this.gcMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMachine});
			// 
			// gvMachine
			// 
			this.gvMachine.Appearance.Row.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
			this.gvMachine.Appearance.Row.Options.UseFont = true;
			this.gvMachine.ColumnPanelRowHeight = 45;
			this.gvMachine.GridControl = this.gcMachine;
			this.gvMachine.Name = "gvMachine";
			this.gvMachine.OptionsView.ShowGroupPanel = false;
			this.gvMachine.RowHeight = 35;
			this.gvMachine.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvMachine_CustomColumnDisplayText);
			// 
			// panelControl1
			// 
			this.panelControl1.Controls.Add(this.tablePanel1);
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelControl1.Location = new System.Drawing.Point(0, 40);
			this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size(1451, 60);
			this.panelControl1.TabIndex = 92;
			// 
			// tablePanel1
			// 
			this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
			this.tablePanel1.Controls.Add(this.LblTime);
			this.tablePanel1.Controls.Add(this.PnlStatus);
			this.tablePanel1.Controls.Add(this.LblMainTitle);
			this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tablePanel1.Location = new System.Drawing.Point(2, 2);
			this.tablePanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tablePanel1.Name = "tablePanel1";
			this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
			this.tablePanel1.Size = new System.Drawing.Size(1447, 56);
			this.tablePanel1.TabIndex = 0;
			// 
			// LblTime
			// 
			this.LblTime.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
			this.LblTime.Appearance.Options.UseFont = true;
			this.LblTime.Appearance.Options.UseTextOptions = true;
			this.LblTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
			this.tablePanel1.SetColumn(this.LblTime, 2);
			this.LblTime.Dock = System.Windows.Forms.DockStyle.Right;
			this.LblTime.Location = new System.Drawing.Point(1133, 4);
			this.LblTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.LblTime.Name = "LblTime";
			this.LblTime.Padding = new System.Windows.Forms.Padding(0, 0, 11, 0);
			this.tablePanel1.SetRow(this.LblTime, 0);
			this.LblTime.Size = new System.Drawing.Size(311, 48);
			this.LblTime.TabIndex = 276;
			this.LblTime.Text = "0000-00-00 오전 00:00:00";
			// 
			// PnlStatus
			// 
			this.tablePanel1.SetColumn(this.PnlStatus, 0);
			this.PnlStatus.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 12F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F)});
			this.PnlStatus.Controls.Add(this.label8);
			this.PnlStatus.Controls.Add(this.label7);
			this.PnlStatus.Controls.Add(this.label6);
			this.PnlStatus.Controls.Add(this.label5);
			this.PnlStatus.Controls.Add(this.label4);
			this.PnlStatus.Controls.Add(this.label3);
			this.PnlStatus.Controls.Add(this.label2);
			this.PnlStatus.Controls.Add(this.label1);
			this.PnlStatus.Dock = System.Windows.Forms.DockStyle.Left;
			this.PnlStatus.Location = new System.Drawing.Point(3, 4);
			this.PnlStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.PnlStatus.MinimumSize = new System.Drawing.Size(483, 47);
			this.PnlStatus.Name = "PnlStatus";
			this.tablePanel1.SetRow(this.PnlStatus, 0);
			this.PnlStatus.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
			this.PnlStatus.Size = new System.Drawing.Size(483, 48);
			this.PnlStatus.TabIndex = 275;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.PnlStatus.SetColumn(this.label8, 7);
			this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label8.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
			this.label8.Location = new System.Drawing.Point(408, 0);
			this.label8.Name = "label8";
			this.PnlStatus.SetRow(this.label8, 0);
			this.label8.Size = new System.Drawing.Size(72, 48);
			this.label8.TabIndex = 7;
			this.label8.Text = "종료";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.PnlStatus.SetColumn(this.label7, 6);
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Font = new System.Drawing.Font("Pretendard SemiBold", 20F, System.Drawing.FontStyle.Bold);
			this.label7.ForeColor = System.Drawing.Color.Red;
			this.label7.Location = new System.Drawing.Point(369, 0);
			this.label7.Name = "label7";
			this.PnlStatus.SetRow(this.label7, 0);
			this.label7.Size = new System.Drawing.Size(33, 48);
			this.label7.TabIndex = 6;
			this.label7.Text = "●";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.PnlStatus.SetColumn(this.label6, 5);
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(276, 0);
			this.label6.Name = "label6";
			this.PnlStatus.SetRow(this.label6, 0);
			this.label6.Size = new System.Drawing.Size(87, 48);
			this.label6.TabIndex = 5;
			this.label6.Text = "작업중";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.PnlStatus.SetColumn(this.label5, 4);
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Font = new System.Drawing.Font("Pretendard SemiBold", 20F, System.Drawing.FontStyle.Bold);
			this.label5.ForeColor = System.Drawing.Color.Green;
			this.label5.Location = new System.Drawing.Point(237, 0);
			this.label5.Name = "label5";
			this.PnlStatus.SetRow(this.label5, 0);
			this.label5.Size = new System.Drawing.Size(33, 48);
			this.label5.TabIndex = 4;
			this.label5.Text = "●";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.PnlStatus.SetColumn(this.label4, 3);
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(159, 0);
			this.label4.Name = "label4";
			this.PnlStatus.SetRow(this.label4, 0);
			this.label4.Size = new System.Drawing.Size(72, 48);
			this.label4.TabIndex = 3;
			this.label4.Text = "준비";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.PnlStatus.SetColumn(this.label3, 1);
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Font = new System.Drawing.Font("Pretendard SemiBold", 18F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(42, 0);
			this.label3.Name = "label3";
			this.PnlStatus.SetRow(this.label3, 0);
			this.label3.Size = new System.Drawing.Size(72, 48);
			this.label3.TabIndex = 2;
			this.label3.Text = "대기";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.PnlStatus.SetColumn(this.label2, 2);
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Pretendard SemiBold", 20F, System.Drawing.FontStyle.Bold);
			this.label2.ForeColor = System.Drawing.Color.Gold;
			this.label2.Location = new System.Drawing.Point(120, 0);
			this.label2.Name = "label2";
			this.PnlStatus.SetRow(this.label2, 0);
			this.label2.Size = new System.Drawing.Size(33, 48);
			this.label2.TabIndex = 1;
			this.label2.Text = "●";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.PnlStatus.SetColumn(this.label1, 0);
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Pretendard SemiBold", 20F, System.Drawing.FontStyle.Bold);
			this.label1.ForeColor = System.Drawing.Color.Gray;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.PnlStatus.SetRow(this.label1, 0);
			this.label1.Size = new System.Drawing.Size(33, 48);
			this.label1.TabIndex = 0;
			this.label1.Text = "●";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LblMainTitle
			// 
			this.LblMainTitle.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 23F, System.Drawing.FontStyle.Bold);
			this.LblMainTitle.Appearance.Options.UseFont = true;
			this.LblMainTitle.Appearance.Options.UseTextOptions = true;
			this.LblMainTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.LblMainTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.LblMainTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.tablePanel1.SetColumn(this.LblMainTitle, 1);
			this.LblMainTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LblMainTitle.Location = new System.Drawing.Point(560, 4);
			this.LblMainTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.LblMainTitle.Name = "LblMainTitle";
			this.tablePanel1.SetRow(this.LblMainTitle, 0);
			this.LblMainTitle.Size = new System.Drawing.Size(328, 48);
			this.LblMainTitle.TabIndex = 61;
			this.LblMainTitle.Text = "생산 진행 모니터링";
			// 
			// TmTime
			// 
			this.TmTime.Interval = 1000;
			this.TmTime.Tick += new System.EventHandler(this.TmTime_Tick);
			// 
			// Tm_Search
			// 
			this.Tm_Search.Interval = 5000;
			this.Tm_Search.Tick += new System.EventHandler(this.Tm_Search_Tick);
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
			this.btn_Close.Location = new System.Drawing.Point(1374, 0);
			this.btn_Close.Name = "btn_Close";
			this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.btn_Close.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
			this.btn_Close.Size = new System.Drawing.Size(77, 40);
			this.btn_Close.smtButtonStyle = baseSMT.EnumButtonStyles.닫기;
			this.btn_Close.TabIndex = 1;
			this.btn_Close.Text = "닫기";
			this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
			// 
			// smtCommonBar1
			// 
			this.smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
			this.smtCommonBar1.Appearance.Options.UseBackColor = true;
			this.smtCommonBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.smtCommonBar1.Controls.Add(this.btn_Close);
			this.smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.smtCommonBar1.Location = new System.Drawing.Point(0, 0);
			this.smtCommonBar1.Name = "smtCommonBar1";
			this.smtCommonBar1.Size = new System.Drawing.Size(1451, 40);
			this.smtCommonBar1.smtImageTitleSvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("smtCommonBar1.smtImageTitleSvgImage")));
			this.smtCommonBar1.smtImageTitleText = "가공생산 모니터링";
			this.smtCommonBar1.smtImageTitleVisible = true;
			this.smtCommonBar1.TabIndex = 93;
			// 
			// FrmCNCProduct
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1451, 686);
			this.Controls.Add(this.gcMachine);
			this.Controls.Add(this.panelControl1);
			this.Controls.Add(this.smtCommonBar1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "FrmCNCProduct";
			this.Text = "가공생산 모니터링";
			this.Load += new System.EventHandler(this.FrmCNCProduct_Load);
			((System.ComponentModel.ISupportInitialize)(this.gcMachine)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gvMachine)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
			this.panelControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
			this.tablePanel1.ResumeLayout(false);
			this.tablePanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PnlStatus)).EndInit();
			this.PnlStatus.ResumeLayout(false);
			this.PnlStatus.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.smtCommonBar1)).EndInit();
			this.smtCommonBar1.ResumeLayout(false);
			this.smtCommonBar1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gcMachine;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMachine;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.Utils.Layout.TablePanel PnlStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl LblMainTitle;
        private DevExpress.XtraEditors.LabelControl LblTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer TmTime;
        private System.Windows.Forms.Timer Tm_Search;
		private baseSMT.smtSearchButton btn_Close;
		private baseSMT.smtCommonBar smtCommonBar1;
	}
}