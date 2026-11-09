namespace SmartMES.View.Dashboard
{
    partial class frmComprehensiveDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private void InitializeComponent()
        {
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose   = new DevExpress.XtraEditors.SimpleButton();
           // ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();

            this.cmdRefresh.Location = new System.Drawing.Point(0, 0);
            this.cmdRefresh.Name     = "cmdRefresh";
            this.cmdRefresh.Size     = new System.Drawing.Size(90, 28);
            this.cmdRefresh.TabIndex = 0;
            this.cmdRefresh.Text     = "새로고침";
            this.cmdRefresh.Visible  = false;
            this.cmdRefresh.Click   += new System.EventHandler(this.cmdRefresh_ElementClick);

            this.cmdClose.Location   = new System.Drawing.Point(100, 0);
            this.cmdClose.Name       = "cmdClose";
            this.cmdClose.Size       = new System.Drawing.Size(90, 28);
            this.cmdClose.TabIndex   = 1;
            this.cmdClose.Text       = "닫기";
            this.cmdClose.Visible    = false;
            this.cmdClose.Click     += new System.EventHandler(this.cmdClose_ElementClick);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdClose);
            this.Name  = "frmComprehensiveDashboard";
            this.Text  = "실시간 종합현황";
            this.Load += new System.EventHandler(this.frmComprehensiveDashboard_Load);

            //((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.SimpleButton cmdRefresh;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
    }
}
