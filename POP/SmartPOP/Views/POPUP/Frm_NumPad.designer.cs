namespace SmartPOP.Views.POPUP
{
    partial class Frm_NumPad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_NumPad));
            this.btn_OK = new DevExpress.XtraEditors.SimpleButton();
            this.TxtTotalQty = new DevExpress.XtraEditors.TextEdit();
            this.BtnBackSpaceTotalQty = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalUp = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalDown = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum0 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum9 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum8 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum7 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum6 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum5 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum4 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum3 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum2 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNum1 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnTotalNumClear = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDot = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.TxtTotalQty.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.btn_OK.Appearance.Options.UseFont = true;
            this.btn_OK.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_OK.ImageOptions.Image")));
            this.btn_OK.Location = new System.Drawing.Point(28, 386);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(324, 77);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "ENTER";
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // TxtTotalQty
            // 
            this.TxtTotalQty.EditValue = "0";
            this.TxtTotalQty.Location = new System.Drawing.Point(28, 1);
            this.TxtTotalQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtTotalQty.Name = "TxtTotalQty";
            this.TxtTotalQty.Properties.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.TxtTotalQty.Properties.Appearance.Options.UseFont = true;
            this.TxtTotalQty.Properties.Appearance.Options.UseTextOptions = true;
            this.TxtTotalQty.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TxtTotalQty.Size = new System.Drawing.Size(324, 62);
            this.TxtTotalQty.TabIndex = 2;
            this.TxtTotalQty.TabStop = false;
            this.TxtTotalQty.EditValueChanged += new System.EventHandler(this.TxtTotalQty_EditValueChanged);
            // 
            // BtnBackSpaceTotalQty
            // 
            this.BtnBackSpaceTotalQty.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackSpaceTotalQty.Appearance.Options.UseFont = true;
            this.BtnBackSpaceTotalQty.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnBackSpaceTotalQty.ImageOptions.Image")));
            this.BtnBackSpaceTotalQty.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.BtnBackSpaceTotalQty.Location = new System.Drawing.Point(275, 68);
            this.BtnBackSpaceTotalQty.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnBackSpaceTotalQty.Name = "BtnBackSpaceTotalQty";
            this.BtnBackSpaceTotalQty.Size = new System.Drawing.Size(77, 77);
            this.BtnBackSpaceTotalQty.TabIndex = 62;
            this.BtnBackSpaceTotalQty.Tag = "back";
            this.BtnBackSpaceTotalQty.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalUp
            // 
            this.BtnTotalUp.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnTotalUp.ImageOptions.Image")));
            this.BtnTotalUp.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.BtnTotalUp.Location = new System.Drawing.Point(275, 150);
            this.BtnTotalUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalUp.Name = "BtnTotalUp";
            this.BtnTotalUp.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalUp.TabIndex = 64;
            this.BtnTotalUp.Tag = "up";
            this.BtnTotalUp.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalDown
            // 
            this.BtnTotalDown.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnTotalDown.ImageOptions.Image")));
            this.BtnTotalDown.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.BtnTotalDown.Location = new System.Drawing.Point(275, 233);
            this.BtnTotalDown.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalDown.Name = "BtnTotalDown";
            this.BtnTotalDown.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalDown.TabIndex = 63;
            this.BtnTotalDown.Tag = "down";
            this.BtnTotalDown.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum0
            // 
            this.BtnTotalNum0.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum0.Appearance.Options.UseFont = true;
            this.BtnTotalNum0.Location = new System.Drawing.Point(28, 314);
            this.BtnTotalNum0.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum0.Name = "BtnTotalNum0";
            this.BtnTotalNum0.Size = new System.Drawing.Size(159, 67);
            this.BtnTotalNum0.TabIndex = 74;
            this.BtnTotalNum0.Tag = "num";
            this.BtnTotalNum0.Text = "0";
            this.BtnTotalNum0.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum9
            // 
            this.BtnTotalNum9.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum9.Appearance.Options.UseFont = true;
            this.BtnTotalNum9.Location = new System.Drawing.Point(193, 68);
            this.BtnTotalNum9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum9.Name = "BtnTotalNum9";
            this.BtnTotalNum9.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum9.TabIndex = 73;
            this.BtnTotalNum9.Tag = "num";
            this.BtnTotalNum9.Text = "9";
            this.BtnTotalNum9.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum8
            // 
            this.BtnTotalNum8.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum8.Appearance.Options.UseFont = true;
            this.BtnTotalNum8.Location = new System.Drawing.Point(111, 68);
            this.BtnTotalNum8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum8.Name = "BtnTotalNum8";
            this.BtnTotalNum8.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum8.TabIndex = 72;
            this.BtnTotalNum8.Tag = "num";
            this.BtnTotalNum8.Text = "8";
            this.BtnTotalNum8.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum7
            // 
            this.BtnTotalNum7.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum7.Appearance.Options.UseFont = true;
            this.BtnTotalNum7.Location = new System.Drawing.Point(28, 68);
            this.BtnTotalNum7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum7.Name = "BtnTotalNum7";
            this.BtnTotalNum7.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum7.TabIndex = 71;
            this.BtnTotalNum7.Tag = "num";
            this.BtnTotalNum7.Text = "7";
            this.BtnTotalNum7.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum6
            // 
            this.BtnTotalNum6.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum6.Appearance.Options.UseFont = true;
            this.BtnTotalNum6.Location = new System.Drawing.Point(193, 150);
            this.BtnTotalNum6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum6.Name = "BtnTotalNum6";
            this.BtnTotalNum6.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum6.TabIndex = 70;
            this.BtnTotalNum6.Tag = "num";
            this.BtnTotalNum6.Text = "6";
            this.BtnTotalNum6.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum5
            // 
            this.BtnTotalNum5.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum5.Appearance.Options.UseFont = true;
            this.BtnTotalNum5.Location = new System.Drawing.Point(111, 150);
            this.BtnTotalNum5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum5.Name = "BtnTotalNum5";
            this.BtnTotalNum5.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum5.TabIndex = 69;
            this.BtnTotalNum5.Tag = "num";
            this.BtnTotalNum5.Text = "5";
            this.BtnTotalNum5.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum4
            // 
            this.BtnTotalNum4.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum4.Appearance.Options.UseFont = true;
            this.BtnTotalNum4.Location = new System.Drawing.Point(28, 150);
            this.BtnTotalNum4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum4.Name = "BtnTotalNum4";
            this.BtnTotalNum4.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum4.TabIndex = 68;
            this.BtnTotalNum4.Tag = "num";
            this.BtnTotalNum4.Text = "4";
            this.BtnTotalNum4.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum3
            // 
            this.BtnTotalNum3.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum3.Appearance.Options.UseFont = true;
            this.BtnTotalNum3.Location = new System.Drawing.Point(193, 233);
            this.BtnTotalNum3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum3.Name = "BtnTotalNum3";
            this.BtnTotalNum3.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum3.TabIndex = 67;
            this.BtnTotalNum3.Tag = "num";
            this.BtnTotalNum3.Text = "3";
            this.BtnTotalNum3.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum2
            // 
            this.BtnTotalNum2.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum2.Appearance.Options.UseFont = true;
            this.BtnTotalNum2.Location = new System.Drawing.Point(111, 233);
            this.BtnTotalNum2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum2.Name = "BtnTotalNum2";
            this.BtnTotalNum2.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum2.TabIndex = 66;
            this.BtnTotalNum2.Tag = "num";
            this.BtnTotalNum2.Text = "2";
            this.BtnTotalNum2.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNum1
            // 
            this.BtnTotalNum1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNum1.Appearance.Options.UseFont = true;
            this.BtnTotalNum1.Location = new System.Drawing.Point(28, 233);
            this.BtnTotalNum1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNum1.Name = "BtnTotalNum1";
            this.BtnTotalNum1.Size = new System.Drawing.Size(77, 77);
            this.BtnTotalNum1.TabIndex = 65;
            this.BtnTotalNum1.Tag = "num";
            this.BtnTotalNum1.Text = "1";
            this.BtnTotalNum1.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // BtnTotalNumClear
            // 
            this.BtnTotalNumClear.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnTotalNumClear.Appearance.Options.UseFont = true;
            this.BtnTotalNumClear.Location = new System.Drawing.Point(275, 314);
            this.BtnTotalNumClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnTotalNumClear.Name = "BtnTotalNumClear";
            this.BtnTotalNumClear.Size = new System.Drawing.Size(77, 67);
            this.BtnTotalNumClear.TabIndex = 77;
            this.BtnTotalNumClear.Tag = "C";
            this.BtnTotalNumClear.Text = "C";
            this.BtnTotalNumClear.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(28, 468);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(324, 77);
            this.simpleButton1.TabIndex = 78;
            this.simpleButton1.Text = "CLOSE";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // BtnDot
            // 
            this.BtnDot.Appearance.Font = new System.Drawing.Font("Pretendard SemiBold", 30F, System.Drawing.FontStyle.Bold);
            this.BtnDot.Appearance.Options.UseFont = true;
            this.BtnDot.Location = new System.Drawing.Point(193, 314);
            this.BtnDot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnDot.Name = "BtnDot";
            this.BtnDot.Size = new System.Drawing.Size(77, 67);
            this.BtnDot.TabIndex = 79;
            this.BtnDot.Tag = ".";
            this.BtnDot.Text = ".";
            this.BtnDot.Click += new System.EventHandler(this.TotalQtyNumPad_Click);
            // 
            // FrmNumPad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 563);
            this.ControlBox = false;
            this.Controls.Add(this.BtnDot);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.BtnTotalNumClear);
            this.Controls.Add(this.BtnTotalNum0);
            this.Controls.Add(this.BtnTotalNum9);
            this.Controls.Add(this.BtnTotalNum8);
            this.Controls.Add(this.BtnTotalNum7);
            this.Controls.Add(this.BtnTotalNum6);
            this.Controls.Add(this.BtnTotalNum5);
            this.Controls.Add(this.BtnTotalNum4);
            this.Controls.Add(this.BtnTotalNum3);
            this.Controls.Add(this.BtnTotalNum2);
            this.Controls.Add(this.BtnTotalNum1);
            this.Controls.Add(this.BtnTotalUp);
            this.Controls.Add(this.BtnTotalDown);
            this.Controls.Add(this.BtnBackSpaceTotalQty);
            this.Controls.Add(this.TxtTotalQty);
            this.Controls.Add(this.btn_OK);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(302, 504);
            this.MinimumSize = new System.Drawing.Size(302, 504);
            this.Name = "FrmNumPad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " ";
            this.Load += new System.EventHandler(this.FrmNumPad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TxtTotalQty.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btn_OK;
        private DevExpress.XtraEditors.TextEdit TxtTotalQty;
        private DevExpress.XtraEditors.SimpleButton BtnBackSpaceTotalQty;
        private DevExpress.XtraEditors.SimpleButton BtnTotalUp;
        private DevExpress.XtraEditors.SimpleButton BtnTotalDown;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum0;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum9;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum8;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum7;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum6;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum5;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum4;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum3;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum2;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNum1;
        private DevExpress.XtraEditors.SimpleButton BtnTotalNumClear;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton BtnDot;
    }
}