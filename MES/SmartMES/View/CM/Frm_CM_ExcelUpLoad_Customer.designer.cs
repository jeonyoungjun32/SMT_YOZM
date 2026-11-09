
namespace SmartMES.View.CM
{
    partial class Frm_CM_ExcelUpLoad_Customer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_CM_ExcelUpLoad_Customer));
            this.Panel1 = new DevExpress.XtraEditors.PanelControl();
            this.Btn_ExcelDown = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Open = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Close = new DevExpress.XtraEditors.SimpleButton();
            this.Btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.LblMainTitle = new DevExpress.XtraEditors.LabelControl();
            this.Gc_Excel = new DevExpress.XtraGrid.GridControl();
            this.Gv_Excel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Gv_ProductCmd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.spreadsheetControl1 = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            ((System.ComponentModel.ISupportInitialize)(this.Panel1)).BeginInit();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Excel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Excel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProductCmd)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Btn_ExcelDown);
            this.Panel1.Controls.Add(this.Btn_Open);
            this.Panel1.Controls.Add(this.Btn_Close);
            this.Panel1.Controls.Add(this.Btn_Save);
            this.Panel1.Controls.Add(this.LblMainTitle);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.Panel1.Size = new System.Drawing.Size(1377, 76);
            this.Panel1.TabIndex = 268;
            // 
            // Btn_ExcelDown
            // 
            this.Btn_ExcelDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_ExcelDown.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_ExcelDown.ImageOptions.Image")));
            this.Btn_ExcelDown.Location = new System.Drawing.Point(1018, 41);
            this.Btn_ExcelDown.Name = "Btn_ExcelDown";
            this.Btn_ExcelDown.Size = new System.Drawing.Size(89, 26);
            this.Btn_ExcelDown.TabIndex = 295;
            this.Btn_ExcelDown.Text = "양식다운";
            this.Btn_ExcelDown.Click += new System.EventHandler(this.Btn_Click);
            // 
            // Btn_Open
            // 
            this.Btn_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Open.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Open.ImageOptions.Image")));
            this.Btn_Open.Location = new System.Drawing.Point(1114, 41);
            this.Btn_Open.Name = "Btn_Open";
            this.Btn_Open.Size = new System.Drawing.Size(80, 26);
            this.Btn_Open.TabIndex = 292;
            this.Btn_Open.Text = "열  기";
            this.Btn_Open.Click += new System.EventHandler(this.Btn_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Close.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Close.ImageOptions.Image")));
            this.Btn_Close.Location = new System.Drawing.Point(1288, 41);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(80, 26);
            this.Btn_Close.TabIndex = 291;
            this.Btn_Close.Text = "닫  기";
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Click);
            // 
            // Btn_Save
            // 
            this.Btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Save.ImageOptions.Image")));
            this.Btn_Save.Location = new System.Drawing.Point(1201, 41);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(80, 26);
            this.Btn_Save.TabIndex = 290;
            this.Btn_Save.Text = "저  장";
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Click);
            // 
            // LblMainTitle
            // 
            this.LblMainTitle.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold);
            this.LblMainTitle.Appearance.Options.UseFont = true;
            this.LblMainTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblMainTitle.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.LblMainTitle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("LblMainTitle.ImageOptions.Image")));
            this.LblMainTitle.Location = new System.Drawing.Point(9, 9);
            this.LblMainTitle.Name = "LblMainTitle";
            this.LblMainTitle.Size = new System.Drawing.Size(343, 26);
            this.LblMainTitle.TabIndex = 59;
            this.LblMainTitle.Text = "엑셀 업로드";
            // 
            // Gc_Excel
            // 
            this.Gc_Excel.Location = new System.Drawing.Point(429, 170);
            this.Gc_Excel.MainView = this.Gv_Excel;
            this.Gc_Excel.Name = "Gc_Excel";
            this.Gc_Excel.Size = new System.Drawing.Size(471, 330);
            this.Gc_Excel.TabIndex = 294;
            this.Gc_Excel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Gv_Excel,
            this.Gv_ProductCmd});
            // 
            // Gv_Excel
            // 
            this.Gv_Excel.DetailHeight = 397;
            this.Gv_Excel.GridControl = this.Gc_Excel;
            this.Gv_Excel.Name = "Gv_Excel";
            this.Gv_Excel.OptionsView.ShowGroupPanel = false;
            // 
            // Gv_ProductCmd
            // 
            this.Gv_ProductCmd.DetailHeight = 397;
            this.Gv_ProductCmd.GridControl = this.Gc_Excel;
            this.Gv_ProductCmd.Name = "Gv_ProductCmd";
            this.Gv_ProductCmd.OptionsView.ShowGroupPanel = false;
            // 
            // spreadsheetControl1
            // 
            this.spreadsheetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetControl1.Location = new System.Drawing.Point(0, 76);
            this.spreadsheetControl1.Name = "spreadsheetControl1";
            this.spreadsheetControl1.Options.Culture = new System.Globalization.CultureInfo("ko-KR");
            this.spreadsheetControl1.Size = new System.Drawing.Size(1377, 565);
            this.spreadsheetControl1.TabIndex = 269;
            this.spreadsheetControl1.Text = "spreadsheetControl1";
            // 
            // Frm_ExcelUpLoad_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 641);
            this.Controls.Add(this.spreadsheetControl1);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Gc_Excel);
            this.Name = "Frm_ExcelUpLoad_Item";
            this.Load += new System.EventHandler(this.Frm_ExcelUpLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Panel1)).EndInit();
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gc_Excel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_Excel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Gv_ProductCmd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl Panel1;
        private DevExpress.XtraEditors.LabelControl LblMainTitle;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControl1;
        private DevExpress.XtraEditors.SimpleButton Btn_Save;
        private DevExpress.XtraEditors.SimpleButton Btn_Open;
        private DevExpress.XtraEditors.SimpleButton Btn_Close;
        private DevExpress.XtraGrid.GridControl Gc_Excel;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_Excel;
        private DevExpress.XtraGrid.Views.Grid.GridView Gv_ProductCmd;
        private DevExpress.XtraEditors.SimpleButton Btn_ExcelDown;
    }
}