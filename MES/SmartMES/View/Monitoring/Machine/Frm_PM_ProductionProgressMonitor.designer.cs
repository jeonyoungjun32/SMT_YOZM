namespace SmartMES.View.Monitoring.Machine
{
    partial class Frm_PM_ProductionProgressMonitor
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
            this.LblMainTitle = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.PnlMainTitle = new DevExpress.XtraEditors.PanelControl();
            this.panelDashboard = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PnlMainTitle)).BeginInit();
            this.PnlMainTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // LblMainTitle
            // 
            this.LblMainTitle.Appearance.Font = new System.Drawing.Font("돋움체", 11F, System.Drawing.FontStyle.Bold);
            this.LblMainTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.LblMainTitle.Appearance.Options.UseFont = true;
            this.LblMainTitle.Appearance.Options.UseForeColor = true;
            this.LblMainTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblMainTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblMainTitle.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.LblMainTitle.Location = new System.Drawing.Point(4, 4);
            this.LblMainTitle.Margin = new System.Windows.Forms.Padding(4);
            this.LblMainTitle.Name = "LblMainTitle";
            this.LblMainTitle.Size = new System.Drawing.Size(237, -7);
            this.LblMainTitle.TabIndex = 59;
            this.LblMainTitle.Text = "     공정조건 모니터링";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(241, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(90, -7);
            this.panel1.TabIndex = 60;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.cmdSearch.FlatAppearance.BorderSize = 0;
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Pretendard SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmdSearch.ForeColor = System.Drawing.Color.White;
            this.cmdSearch.Image = global::SmartMES.Properties.Resources.Search_16px;
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(0, 0);
            this.cmdSearch.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(98, -7);
            this.cmdSearch.TabIndex = 35;
            this.cmdSearch.Text = "     검색";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.UseVisualStyleBackColor = true;
            // 
            // PnlMainTitle
            // 
            this.PnlMainTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PnlMainTitle.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(63)))), ((int)(((byte)(92)))));
            this.PnlMainTitle.Appearance.Options.UseBackColor = true;
            this.PnlMainTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.PnlMainTitle.Controls.Add(this.panel1);
            this.PnlMainTitle.Controls.Add(this.LblMainTitle);
            this.PnlMainTitle.Location = new System.Drawing.Point(2, 2);
            this.PnlMainTitle.Margin = new System.Windows.Forms.Padding(4);
            this.PnlMainTitle.Name = "PnlMainTitle";
            this.PnlMainTitle.Padding = new System.Windows.Forms.Padding(4);
            this.PnlMainTitle.Size = new System.Drawing.Size(1332, 1);
            this.PnlMainTitle.TabIndex = 267;
            // 
            // panelDashboard
            // 
            this.panelDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDashboard.Location = new System.Drawing.Point(0, 0);
            this.panelDashboard.Name = "panelDashboard";
            this.panelDashboard.Size = new System.Drawing.Size(298, 258);
            this.panelDashboard.TabIndex = 95;
            // 
            // Frm_PM_ProductionProgressMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 258);
            this.Controls.Add(this.panelDashboard);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Frm_PM_ProductionProgressMonitor";
            this.Load += new System.EventHandler(this.Frm_PM_ProductionProgressMonitor_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PnlMainTitle)).EndInit();
            this.PnlMainTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl LblMainTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdSearch;
        private DevExpress.XtraEditors.PanelControl PnlMainTitle;
        private System.Windows.Forms.Panel panelDashboard;
    }
}