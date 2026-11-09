
namespace SmartCommon.View.Mail
{
    partial class ucAddressReg
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
			this.panel1 = new System.Windows.Forms.Panel();
			this.tboxAddress = new DevExpress.XtraEditors.TextEdit();
			this.lboxAddress = new System.Windows.Forms.ListBox();
			this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
			this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tboxAddress.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnAdd);
			this.panel1.Controls.Add(this.tboxAddress);
			this.panel1.Controls.Add(this.btnDelete);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(426, 26);
			this.panel1.TabIndex = 3;
			// 
			// tboxAddress
			// 
			this.tboxAddress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tboxAddress.Location = new System.Drawing.Point(0, 0);
			this.tboxAddress.Name = "tboxAddress";
			this.tboxAddress.Properties.AutoHeight = false;
			this.tboxAddress.Size = new System.Drawing.Size(351, 26);
			this.tboxAddress.TabIndex = 3;
			// 
			// lboxAddress
			// 
			this.lboxAddress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lboxAddress.FormattingEnabled = true;
			this.lboxAddress.ItemHeight = 12;
			this.lboxAddress.Location = new System.Drawing.Point(0, 26);
			this.lboxAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lboxAddress.Name = "lboxAddress";
			this.lboxAddress.Size = new System.Drawing.Size(426, 170);
			this.lboxAddress.TabIndex = 4;
			// 
			// btnAdd
			// 
			this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnAdd.Location = new System.Drawing.Point(276, 0);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 26);
			this.btnAdd.TabIndex = 4;
			this.btnAdd.Text = "추가";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnDelete.Location = new System.Drawing.Point(351, 0);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 26);
			this.btnDelete.TabIndex = 5;
			this.btnDelete.Text = "삭제";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// ucAddressReg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lboxAddress);
			this.Controls.Add(this.panel1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "ucAddressReg";
			this.Size = new System.Drawing.Size(426, 196);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.tboxAddress.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.TextEdit tboxAddress;
        private System.Windows.Forms.ListBox lboxAddress;
		private DevExpress.XtraEditors.SimpleButton btnAdd;
		private DevExpress.XtraEditors.SimpleButton btnDelete;
	}
}
