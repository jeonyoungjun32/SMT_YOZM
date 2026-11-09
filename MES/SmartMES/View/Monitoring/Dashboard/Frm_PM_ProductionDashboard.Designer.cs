namespace SmartMES.View.Monitoring.Dashboard
{
    partial class Frm_PM_ProductionDashboard
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// InitializeComponent: DevExpress XtraForm 최소 설정.
        /// cmdRefresh / cmdClose 는 DevExpress SimpleButton 으로 선언.
        /// 실제 배치는 BuildLayout() 에서 코드로 처리하므로
        /// 여기서는 커맨드 버튼만 등록합니다.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.cmdClose   = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();

            // ── cmdRefresh ─────────────────────────────────
            this.cmdRefresh.Location = new System.Drawing.Point(0, 0); // BuildLayout 에서 재배치
            this.cmdRefresh.Name     = "cmdRefresh";
            this.cmdRefresh.Size     = new System.Drawing.Size(90, 28);
            this.cmdRefresh.TabIndex = 0;
            this.cmdRefresh.Text     = "새로고침";
            this.cmdRefresh.Visible  = false; // 헤더에 직접 추가 시 true
            this.cmdRefresh.Click   += new System.EventHandler(this.cmdRefresh_ElementClick);

            // ── cmdClose ───────────────────────────────────
            this.cmdClose.Location = new System.Drawing.Point(100, 0);
            this.cmdClose.Name     = "cmdClose";
            this.cmdClose.Size     = new System.Drawing.Size(90, 28);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text     = "닫기";
            this.cmdClose.Visible  = false;
            this.cmdClose.Click   += new System.EventHandler(this.cmdClose_ElementClick);

            // ── Form ───────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(1440, 900);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmdClose);
            this.Name  = "frmProductionDashboard";
            this.Text  = "생산종합현황판";
            this.Load += new System.EventHandler(this.frmProductionDashboard_Load);

            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.SimpleButton cmdRefresh;
        private DevExpress.XtraEditors.SimpleButton cmdClose;
    }
}
