
namespace SmartMES.View.Monitoring.Machine
{
    partial class Frm_PM_MachineProgress
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
            this.TPMain = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.LblTime = new DevExpress.XtraEditors.LabelControl();
            this.LblMainTitle = new DevExpress.XtraEditors.LabelControl();
            this.TmSearch = new System.Windows.Forms.Timer(this.components);
            this.TmTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
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
            this.TPMain.Location = new System.Drawing.Point(0, 78);
            this.TPMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TPMain.Name = "TPMain";
            this.TPMain.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.TPMain.Size = new System.Drawing.Size(1949, 835);
            this.TPMain.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.flowLayoutPanel1);
            this.panelControl1.Controls.Add(this.LblTime);
            this.panelControl1.Controls.Add(this.LblMainTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1949, 66);
            this.panelControl1.TabIndex = 14;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1409, 14);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(32, 48);
            this.flowLayoutPanel1.TabIndex = 15;
            this.flowLayoutPanel1.Visible = false;
            // 
            // LblTime
            // 
            this.LblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblTime.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.LblTime.Appearance.Options.UseFont = true;
            this.LblTime.Appearance.Options.UseTextOptions = true;
            this.LblTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.LblTime.Location = new System.Drawing.Point(18, 37);
            this.LblTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LblTime.Name = "LblTime";
            this.LblTime.Size = new System.Drawing.Size(252, 30);
            this.LblTime.TabIndex = 62;
            this.LblTime.Text = "2023-08-02 오전9:08";
            // 
            // LblMainTitle
            // 
            this.LblMainTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.LblMainTitle.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 28F);
            this.LblMainTitle.Appearance.Options.UseBackColor = true;
            this.LblMainTitle.Appearance.Options.UseFont = true;
            this.LblMainTitle.Appearance.Options.UseTextOptions = true;
            this.LblMainTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblMainTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LblMainTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblMainTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.LblMainTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblMainTitle.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.LblMainTitle.Location = new System.Drawing.Point(0, 0);
            this.LblMainTitle.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.LblMainTitle.Name = "LblMainTitle";
            this.LblMainTitle.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.LblMainTitle.Size = new System.Drawing.Size(1949, 66);
            this.LblMainTitle.TabIndex = 63;
            this.LblMainTitle.Text = "설비별 생산 현황";
            // 
            // TmSearch
            // 
            this.TmSearch.Interval = 5000;
            this.TmSearch.Tick += new System.EventHandler(this.TmSearch_Tick);
            // 
            // TmTime
            // 
            this.TmTime.Interval = 1000;
            this.TmTime.Tick += new System.EventHandler(this.TmTime_Tick);
            // 
            // Frm_PM_MachineProgress
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1949, 916);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.TPMain);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frm_PM_MachineProgress";
            this.Text = "설비별 생산진행 모니터링";
            this.Load += new System.EventHandler(this.Frm_PM_MachineProgress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel TPMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl LblTime;
        private DevExpress.XtraEditors.LabelControl LblMainTitle;
        private System.Windows.Forms.Timer TmSearch;
        private System.Windows.Forms.Timer TmTime;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	}
}