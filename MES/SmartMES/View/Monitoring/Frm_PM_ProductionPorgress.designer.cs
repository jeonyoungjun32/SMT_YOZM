
namespace SmartMES.View.Monitoring
{
    partial class Frm_PM_ProductionPorgress
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Gc_List = new DevExpress.XtraGrid.GridControl();
            this.Gv_List = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.tablePanel3 = new DevExpress.Utils.Layout.TablePanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblMainTitle = new DevExpress.XtraEditors.LabelControl();
            this.LblTime = new DevExpress.XtraEditors.LabelControl();
            this.TmTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).BeginInit();
            this.tablePanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Gc_List);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 82);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1326, 583);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Gc_List
            // 
            this.Gc_List.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Gc_List.Location = new System.Drawing.Point(2, 3);
            this.Gc_List.MainView = this.Gv_List;
            this.Gc_List.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Gc_List.Name = "Gc_List";
            this.Gc_List.Size = new System.Drawing.Size(1322, 577);
            this.Gc_List.TabIndex = 270;
            this.Gc_List.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_List});
            // 
            // Gv_List
            // 
            this.Gv_List.DetailHeight = 397;
            this.Gv_List.GridControl = this.Gc_List;
            this.Gv_List.Name = "Gv_List";
            this.Gv_List.OptionsClipboard.AllowExcelFormat = DevExpress.Utils.DefaultBoolean.True;
            this.Gv_List.OptionsClipboard.ClipboardMode = DevExpress.Export.ClipboardMode.Formatted;
            this.Gv_List.OptionsClipboard.PasteMode = DevExpress.Export.PasteMode.Append;
            this.Gv_List.OptionsView.ShowGroupPanel = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(1326, 583);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Gc_List;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1326, 583);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // tablePanel2
            // 
            this.tablePanel2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.tablePanel2.Appearance.Options.UseBackColor = true;
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 487.1995F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 440.1994F)});
            this.tablePanel2.Controls.Add(this.tablePanel3);
            this.tablePanel2.Controls.Add(this.LblMainTitle);
            this.tablePanel2.Controls.Add(this.LblTime);
            this.tablePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tablePanel2.Location = new System.Drawing.Point(0, 0);
            this.tablePanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F)});
            this.tablePanel2.Size = new System.Drawing.Size(1326, 82);
            this.tablePanel2.TabIndex = 96;
            // 
            // tablePanel3
            // 
            this.tablePanel2.SetColumn(this.tablePanel3, 2);
            this.tablePanel3.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F)});
            this.tablePanel3.Controls.Add(this.label6);
            this.tablePanel3.Controls.Add(this.label5);
            this.tablePanel3.Controls.Add(this.label4);
            this.tablePanel3.Controls.Add(this.label3);
            this.tablePanel3.Controls.Add(this.label2);
            this.tablePanel3.Controls.Add(this.label1);
            this.tablePanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tablePanel3.Location = new System.Drawing.Point(977, 3);
            this.tablePanel3.Name = "tablePanel3";
            this.tablePanel2.SetRow(this.tablePanel3, 0);
            this.tablePanel3.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel3.Size = new System.Drawing.Size(346, 76);
            this.tablePanel3.TabIndex = 274;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.tablePanel3.SetColumn(this.label6, 5);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(272, 0);
            this.label6.Name = "label6";
            this.tablePanel3.SetRow(this.label6, 0);
            this.label6.Size = new System.Drawing.Size(71, 76);
            this.label6.TabIndex = 5;
            this.label6.Text = "종료";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.tablePanel3.SetColumn(this.label5, 4);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(234, 0);
            this.label5.Name = "label5";
            this.tablePanel3.SetRow(this.label5, 0);
            this.label5.Size = new System.Drawing.Size(32, 76);
            this.label5.TabIndex = 4;
            this.label5.Text = "●";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.tablePanel3.SetColumn(this.label4, 3);
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(157, 0);
            this.label4.Name = "label4";
            this.tablePanel3.SetRow(this.label4, 0);
            this.label4.Size = new System.Drawing.Size(71, 76);
            this.label4.TabIndex = 3;
            this.label4.Text = "진행";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.tablePanel3.SetColumn(this.label3, 1);
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(41, 0);
            this.label3.Name = "label3";
            this.tablePanel3.SetRow(this.label3, 0);
            this.label3.Size = new System.Drawing.Size(71, 76);
            this.label3.TabIndex = 2;
            this.label3.Text = "대기";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tablePanel3.SetColumn(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(118, 0);
            this.label2.Name = "label2";
            this.tablePanel3.SetRow(this.label2, 0);
            this.label2.Size = new System.Drawing.Size(32, 76);
            this.label2.TabIndex = 1;
            this.label2.Text = "●";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tablePanel3.SetColumn(this.label1, 0);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.tablePanel3.SetRow(this.label1, 0);
            this.label1.Size = new System.Drawing.Size(32, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "●";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblMainTitle
            // 
            this.LblMainTitle.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.LblMainTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.LblMainTitle.Appearance.Options.UseFont = true;
            this.LblMainTitle.Appearance.Options.UseForeColor = true;
            this.LblMainTitle.Appearance.Options.UseTextOptions = true;
            this.LblMainTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblMainTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LblMainTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tablePanel2.SetColumn(this.LblMainTitle, 1);
            this.LblMainTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblMainTitle.Location = new System.Drawing.Point(393, 3);
            this.LblMainTitle.Name = "LblMainTitle";
            this.tablePanel2.SetRow(this.LblMainTitle, 0);
            this.LblMainTitle.Size = new System.Drawing.Size(578, 76);
            this.LblMainTitle.TabIndex = 60;
            this.LblMainTitle.Text = "생산진행 모니터링";
            // 
            // LblTime
            // 
            this.LblTime.Appearance.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            this.LblTime.Appearance.ForeColor = System.Drawing.Color.White;
            this.LblTime.Appearance.Options.UseFont = true;
            this.LblTime.Appearance.Options.UseForeColor = true;
            this.LblTime.Appearance.Options.UseTextOptions = true;
            this.LblTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.tablePanel2.SetColumn(this.LblTime, 0);
            this.LblTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblTime.Location = new System.Drawing.Point(3, 3);
            this.LblTime.Name = "LblTime";
            this.LblTime.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.tablePanel2.SetRow(this.LblTime, 0);
            this.LblTime.Size = new System.Drawing.Size(384, 76);
            this.LblTime.TabIndex = 63;
            this.LblTime.Text = "0000-00-00 오전 00:00:00";
            // 
            // TmTime
            // 
            this.TmTime.Interval = 1000;
            this.TmTime.Tick += new System.EventHandler(this.TmTime_Tick);
            // 
            // Frm_PM_ProductionPorgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 665);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.tablePanel2);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_PM_ProductionPorgress";
            this.Text = "생산진행 모니터링";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTmpMachine_FormClosing);
            this.Load += new System.EventHandler(this.Frm_PM_ProductionPorgress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            this.tablePanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).EndInit();
            this.tablePanel3.ResumeLayout(false);
            this.tablePanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl Gc_List;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_List;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.Utils.Layout.TablePanel tablePanel2;
		private DevExpress.Utils.Layout.TablePanel tablePanel3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private DevExpress.XtraEditors.LabelControl LblMainTitle;
		private DevExpress.XtraEditors.LabelControl LblTime;
		private System.Windows.Forms.Timer TmTime;
	}
}