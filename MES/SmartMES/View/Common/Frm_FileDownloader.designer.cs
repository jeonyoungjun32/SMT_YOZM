namespace SmartMES.Common
{
    partial class FileDownloader
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblFolderLen = new System.Windows.Forms.Label();
            this.lblFName = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.pBar2 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBar2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.panelControl1.Controls.Add(this.lblFolderLen);
            this.panelControl1.Controls.Add(this.lblFName);
            this.panelControl1.Controls.Add(this.lblMsg);
            this.panelControl1.Controls.Add(this.pBar2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(446, 110);
            this.panelControl1.TabIndex = 4;
            // 
            // lblFolderLen
            // 
            this.lblFolderLen.Location = new System.Drawing.Point(272, 16);
            this.lblFolderLen.Name = "lblFolderLen";
            this.lblFolderLen.Size = new System.Drawing.Size(150, 12);
            this.lblFolderLen.TabIndex = 22;
            this.lblFolderLen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Location = new System.Drawing.Point(25, 16);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(0, 12);
            this.lblFName.TabIndex = 20;
            this.lblFName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMsg.Location = new System.Drawing.Point(24, 72);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(139, 13);
            this.lblMsg.TabIndex = 19;
            this.lblMsg.Text = "파일 작업중입니다...";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pBar2
            // 
            this.pBar2.Location = new System.Drawing.Point(22, 31);
            this.pBar2.Name = "pBar2";
            this.pBar2.Size = new System.Drawing.Size(400, 25);
            this.pBar2.TabIndex = 17;
            // 
            // FileDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 110);
            this.Controls.Add(this.panelControl1);
            this.Name = "FileDownloader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "다운로더";
            this.Shown += new System.EventHandler(this.FileDownloader_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBar2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblFolderLen;
        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.Label lblMsg;
        private DevExpress.XtraEditors.ProgressBarControl pBar2;
    }
}