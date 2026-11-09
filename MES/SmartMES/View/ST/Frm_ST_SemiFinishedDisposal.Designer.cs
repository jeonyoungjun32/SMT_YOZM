namespace SmartMES.View.ST
{
    partial class Frm_ST_SemiFinishedDisposal
    {
        private baseSMT.smtCommonBar smtCommonBar1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton cmdSave;
        private DevExpress.XtraEditors.PanelControl panelInput;
        private DevExpress.XtraEditors.SpinEdit Txt_Qty;
        private DevExpress.XtraEditors.TextEdit Txt_Reason;
        private DevExpress.XtraEditors.LabelControl lblGuide;
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraGrid.GridControl Gc_Stock;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Stock;
        private DevExpress.XtraGrid.GridControl Gc_History;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_History;

        private void InitializeComponent()
        {
            smtCommonBar1 = new baseSMT.smtCommonBar();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            cmdSave = new DevExpress.XtraEditors.SimpleButton();
            panelInput = new DevExpress.XtraEditors.PanelControl();
            Txt_Qty = new DevExpress.XtraEditors.SpinEdit();
            Txt_Reason = new DevExpress.XtraEditors.TextEdit();
            lblGuide = new DevExpress.XtraEditors.LabelControl();
            tabControl = new DevExpress.XtraTab.XtraTabControl();
            Gc_Stock = new DevExpress.XtraGrid.GridControl();
            Gv_Stock = new DevExpress.XtraGrid.Views.Grid.GridView(Gc_Stock);
            Gc_History = new DevExpress.XtraGrid.GridControl();
            Gv_History = new DevExpress.XtraGrid.Views.Grid.GridView(Gc_History);
            ((System.ComponentModel.ISupportInitialize)Gc_Stock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Gv_Stock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Gc_History).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Gv_History).BeginInit();
            SuspendLayout();
            smtCommonBar1.Name = "smtCommonBar1";
            smtCommonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            smtCommonBar1.Height = 40;
            smtCommonBar1.smtImageTitleText = "반제품 폐기관리";
            smtCommonBar1.smtImageTitleVisible = true;
            smtCommonBar1.Appearance.BackColor = System.Drawing.Color.FromArgb(49, 63, 92);
            smtCommonBar1.Appearance.Options.UseBackColor = true;
            smtCommonBar1.Controls.Add(cmdSave);
            smtCommonBar1.Controls.Add(btnSearch);
            btnSearch.Name = "btnSearch";
            btnSearch.Text = "조회";
            btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            btnSearch.Width = 100;
            btnSearch.Click += btnSearch_Click;
            cmdSave.Name = "cmdSave";
            cmdSave.Text = "선택 LOT 폐기";
            cmdSave.Dock = System.Windows.Forms.DockStyle.Right;
            cmdSave.Width = 150;
            cmdSave.Click += cmdSave_Click;
            panelInput.Dock = System.Windows.Forms.DockStyle.Top;
            panelInput.Height = 90;
            var lblQty = new DevExpress.XtraEditors.LabelControl { Text = "폐기수량", Location = new System.Drawing.Point(15, 18) };
            var lblReason = new DevExpress.XtraEditors.LabelControl { Text = "폐기사유", Location = new System.Drawing.Point(250, 18) };
            Txt_Qty.Location = new System.Drawing.Point(85, 15);
            Txt_Qty.Width = 140;
            Txt_Qty.Properties.IsFloatValue = true;
            Txt_Qty.Properties.MinValue = 0;
            Txt_Qty.Properties.MaxValue = 999999999;
            Txt_Qty.Properties.EditMask = "n4";
            Txt_Reason.Location = new System.Drawing.Point(325, 15);
            Txt_Reason.Width = 430;
            Txt_Reason.Properties.MaxLength = 200;
            lblGuide.Location = new System.Drawing.Point(15, 55);
            panelInput.Controls.Add(lblQty);
            panelInput.Controls.Add(Txt_Qty);
            panelInput.Controls.Add(lblReason);
            panelInput.Controls.Add(Txt_Reason);
            panelInput.Controls.Add(lblGuide);
            tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            var stockPage = new DevExpress.XtraTab.XtraTabPage { Text = "반제품 LOT 재고" };
            var historyPage = new DevExpress.XtraTab.XtraTabPage { Text = "폐기 이력 (최근 1,000건)" };
            tabControl.TabPages.Add(stockPage);
            tabControl.TabPages.Add(historyPage);
            Gc_Stock.Dock = System.Windows.Forms.DockStyle.Fill;
            Gc_Stock.MainView = Gv_Stock;
            Gc_Stock.Name = "Gc_Stock";
            Gc_History.Dock = System.Windows.Forms.DockStyle.Fill;
            Gc_History.MainView = Gv_History;
            Gc_History.Name = "Gc_History";
            Gv_Stock.Name = "Gv_Stock";
            Gv_History.Name = "Gv_History";
            Gv_Stock.FocusedRowChanged += Gv_Stock_FocusedRowChanged;
            Gv_Stock.RowStyle += Gv_Stock_RowStyle;
            stockPage.Controls.Add(Gc_Stock);
            historyPage.Controls.Add(Gc_History);
            Controls.Add(tabControl);
            Controls.Add(panelInput);
            Controls.Add(smtCommonBar1);
            ClientSize = new System.Drawing.Size(1180, 650);
            MinimumSize = new System.Drawing.Size(850, 500);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Name = "Frm_ST_SemiFinishedDisposal";
            Text = "반제품 폐기관리";
            ((System.ComponentModel.ISupportInitialize)Gc_Stock).EndInit();
            ((System.ComponentModel.ISupportInitialize)Gv_Stock).EndInit();
            ((System.ComponentModel.ISupportInitialize)Gc_History).EndInit();
            ((System.ComponentModel.ISupportInitialize)Gv_History).EndInit();
            ResumeLayout(false);
        }
    }
}
