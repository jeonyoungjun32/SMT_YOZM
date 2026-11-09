
namespace SmartMES.UserControls { 
    partial class Btn_ExcelDown_ALL
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Btn_ExcelDown_ALL));
            this.Btn_Save_Xls = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // Btn_Save_Xls
            // 
            this.Btn_Save_Xls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Save_Xls.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Save_Xls.ImageOptions.Image")));
            this.Btn_Save_Xls.Location = new System.Drawing.Point(0, 0);
            this.Btn_Save_Xls.Name = "Btn_Save_Xls";
            this.Btn_Save_Xls.Size = new System.Drawing.Size(80, 23);
            this.Btn_Save_Xls.TabIndex = 308;
            this.Btn_Save_Xls.Text = "엑셀다운";
            // 
            // Btn_ExcelDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_Save_Xls);
            this.Name = "Btn_ExcelDown";
            this.Size = new System.Drawing.Size(80, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Btn_Save_Xls;
    }
}
