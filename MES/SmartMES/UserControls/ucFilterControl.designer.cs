namespace WindowsFormsApp7 {
    partial class ucFilterControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.cbEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cbEdit
            // 
            this.cbEdit.Location = new System.Drawing.Point(23, 4);
            this.cbEdit.Margin = new System.Windows.Forms.Padding(4);
            this.cbEdit.Name = "cbEdit";
            this.cbEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbEdit.Properties.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue"});
            this.cbEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbEdit.Size = new System.Drawing.Size(116, 20);
            this.cbEdit.TabIndex = 0;
            this.cbEdit.Visible = false;
            // 
            // ucFilterControl
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(50)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbEdit);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucFilterControl";
            this.Size = new System.Drawing.Size(260, 36);
            ((System.ComponentModel.ISupportInitialize)(this.cbEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbEdit;
    }
}
