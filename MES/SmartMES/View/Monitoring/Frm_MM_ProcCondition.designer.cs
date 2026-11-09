
namespace SmartMES.View.Monitoring
{
    partial class Frm_MM_ProcCondition
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
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.label258 = new System.Windows.Forms.Label();
            this.label259 = new System.Windows.Forms.Label();
            this.label263 = new System.Windows.Forms.Label();
            this.label262 = new System.Windows.Forms.Label();
            this.LblTime = new DevExpress.XtraEditors.LabelControl();
            this.LblMainTitle = new DevExpress.XtraEditors.LabelControl();
            this.TmSearch = new System.Windows.Forms.Timer(this.components);
            this.TmTime = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TPMain
            // 
            this.TPMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TPMain.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.TPMain.Appearance.Options.UseBackColor = true;
            this.TPMain.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.TPMain.Location = new System.Drawing.Point(14, 109);
            this.TPMain.Name = "TPMain";
            this.TPMain.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.TPMain.Size = new System.Drawing.Size(1535, 656);
            this.TPMain.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(40)))), ((int)(((byte)(58)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.tablePanel1);
            this.panelControl1.Controls.Add(this.LblTime);
            this.panelControl1.Controls.Add(this.LblMainTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1559, 85);
            this.panelControl1.TabIndex = 14;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tablePanel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(40)))), ((int)(((byte)(58)))));
            this.tablePanel1.Appearance.Options.UseBackColor = true;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F)});
            this.tablePanel1.Controls.Add(this.label258);
            this.tablePanel1.Controls.Add(this.label259);
            this.tablePanel1.Controls.Add(this.label263);
            this.tablePanel1.Controls.Add(this.label262);
            this.tablePanel1.Location = new System.Drawing.Point(1352, 56);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 17F)});
            this.tablePanel1.Size = new System.Drawing.Size(197, 42);
            this.tablePanel1.TabIndex = 65;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // label258
            // 
            this.label258.AutoSize = true;
            this.tablePanel1.SetColumn(this.label258, 0);
            this.label258.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label258.Font = new System.Drawing.Font("Pretendard SemiBold", 18F);
            this.label258.ForeColor = System.Drawing.Color.Aqua;
            this.label258.Location = new System.Drawing.Point(17, 13);
            this.label258.Name = "label258";
            this.tablePanel1.SetRow(this.label258, 0);
            this.label258.Size = new System.Drawing.Size(37, 15);
            this.label258.TabIndex = 66;
            this.label258.Text = "■";
            this.label258.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label259
            // 
            this.label259.AutoSize = true;
            this.tablePanel1.SetColumn(this.label259, 1);
            this.label259.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label259.Font = new System.Drawing.Font("Pretendard SemiBold", 10F);
            this.label259.ForeColor = System.Drawing.Color.White;
            this.label259.Location = new System.Drawing.Point(60, 13);
            this.label259.Name = "label259";
            this.tablePanel1.SetRow(this.label259, 0);
            this.label259.Size = new System.Drawing.Size(37, 15);
            this.label259.TabIndex = 65;
            this.label259.Text = "정상";
            this.label259.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label263
            // 
            this.label263.AutoSize = true;
            this.tablePanel1.SetColumn(this.label263, 2);
            this.label263.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label263.Font = new System.Drawing.Font("Pretendard SemiBold", 18F);
            this.label263.ForeColor = System.Drawing.Color.Red;
            this.label263.Location = new System.Drawing.Point(103, 13);
            this.label263.Name = "label263";
            this.tablePanel1.SetRow(this.label263, 0);
            this.label263.Size = new System.Drawing.Size(33, 15);
            this.label263.TabIndex = 68;
            this.label263.Text = "■";
            this.label263.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label262
            // 
            this.label262.AutoSize = true;
            this.tablePanel1.SetColumn(this.label262, 3);
            this.label262.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label262.Font = new System.Drawing.Font("Pretendard SemiBold", 10F);
            this.label262.ForeColor = System.Drawing.Color.White;
            this.label262.Location = new System.Drawing.Point(143, 13);
            this.label262.Name = "label262";
            this.tablePanel1.SetRow(this.label262, 0);
            this.label262.Size = new System.Drawing.Size(37, 15);
            this.label262.TabIndex = 67;
            this.label262.Text = "오류";
            this.label262.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblTime
            // 
            this.LblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblTime.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 15F);
            this.LblTime.Appearance.ForeColor = System.Drawing.Color.White;
            this.LblTime.Appearance.Options.UseFont = true;
            this.LblTime.Appearance.Options.UseForeColor = true;
            this.LblTime.Appearance.Options.UseTextOptions = true;
            this.LblTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.LblTime.Location = new System.Drawing.Point(14, 65);
            this.LblTime.Name = "LblTime";
            this.LblTime.Size = new System.Drawing.Size(202, 24);
            this.LblTime.TabIndex = 62;
            this.LblTime.Text = "2023-08-02 오전9:08";
            // 
            // LblMainTitle
            // 
            this.LblMainTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.LblMainTitle.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 35F, System.Drawing.FontStyle.Bold);
            this.LblMainTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.LblMainTitle.Appearance.Options.UseBackColor = true;
            this.LblMainTitle.Appearance.Options.UseFont = true;
            this.LblMainTitle.Appearance.Options.UseForeColor = true;
            this.LblMainTitle.Appearance.Options.UseTextOptions = true;
            this.LblMainTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.LblMainTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.LblMainTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblMainTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.LblMainTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblMainTitle.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.LblMainTitle.Location = new System.Drawing.Point(0, 0);
            this.LblMainTitle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.LblMainTitle.Name = "LblMainTitle";
            this.LblMainTitle.Padding = new System.Windows.Forms.Padding(6);
            this.LblMainTitle.Size = new System.Drawing.Size(1559, 85);
            this.LblMainTitle.TabIndex = 63;
            this.LblMainTitle.Text = "공정조건 모니터링";
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
            // Frm_MM_ProcCondition
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(40)))), ((int)(((byte)(58)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1559, 779);
            this.Controls.Add(this.TPMain);
            this.Controls.Add(this.panelControl1);
            this.Name = "Frm_MM_ProcCondition";
            this.Text = "사출 공정조건 모니터링";
            this.Load += new System.EventHandler(this.Frm_MM_ProcCondition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TPMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel TPMain;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl LblTime;
        private DevExpress.XtraEditors.LabelControl LblMainTitle;
        private System.Windows.Forms.Timer TmSearch;
        private System.Windows.Forms.Timer TmTime;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private System.Windows.Forms.Label label258;
        private System.Windows.Forms.Label label259;
        private System.Windows.Forms.Label label263;
        private System.Windows.Forms.Label label262;
    }
}