namespace SmartMES.View.Monitoring.Machine
{
    partial class Frm_Stock
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
            this.cOMM_IDLabel = new System.Windows.Forms.Label();
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.widgetView1 = new DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView(this.components);
            this.cOMM_IDTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widgetView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cOMM_IDTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cOMM_IDLabel
            // 
            this.cOMM_IDLabel.AutoSize = true;
            this.cOMM_IDLabel.Location = new System.Drawing.Point(278, 177);
            this.cOMM_IDLabel.Name = "cOMM_IDLabel";
            this.cOMM_IDLabel.Size = new System.Drawing.Size(73, 17);
            this.cOMM_IDLabel.TabIndex = 2;
            this.cOMM_IDLabel.Text = "COMM ID:";
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.View = this.widgetView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.widgetView1});
            // 
            // widgetView1
            // 
            this.widgetView1.RootContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.widgetView1.QueryControl += new DevExpress.XtraBars.Docking2010.Views.QueryControlEventHandler(this.WidgetView1_QueryControl);
            // 
            // cOMM_IDTextEdit
            // 
            this.cOMM_IDTextEdit.Location = new System.Drawing.Point(358, 173);
            this.cOMM_IDTextEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cOMM_IDTextEdit.Name = "cOMM_IDTextEdit";
            this.cOMM_IDTextEdit.Size = new System.Drawing.Size(114, 24);
            this.cOMM_IDTextEdit.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(291, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // frmMachine6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 701);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cOMM_IDLabel);
            this.Controls.Add(this.cOMM_IDTextEdit);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Frm_Stock";
            this.Text = "Frm_Stock";
            this.Load += new System.EventHandler(this.Frm_Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widgetView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cOMM_IDTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Widget.WidgetView widgetView1;
        private DevExpress.XtraEditors.TextEdit cOMM_IDTextEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label cOMM_IDLabel;
    }
}