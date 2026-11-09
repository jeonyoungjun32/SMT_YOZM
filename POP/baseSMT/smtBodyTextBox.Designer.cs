
namespace baseSMT
{
    partial class smtBodyTextBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.txt_Value = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Value.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(447, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // lbl_Title
            // 
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Title.Font = new System.Drawing.Font("Pretendard SemiBold", 10.2F);
            this.lbl_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(70)))), ((int)(((byte)(68)))));
            this.lbl_Title.Location = new System.Drawing.Point(0, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl_Title.Size = new System.Drawing.Size(100, 32);
            this.lbl_Title.TabIndex = 7;
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_Value
            // 
            this.txt_Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Value.EditValue = "";
            this.txt_Value.Location = new System.Drawing.Point(100, 0);
            this.txt_Value.Name = "txt_Value";
            this.txt_Value.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txt_Value.Properties.Appearance.Options.UseBackColor = true;
            this.txt_Value.Size = new System.Drawing.Size(188, 32);
            this.txt_Value.TabIndex = 5;
            // 
            // smtBodyTextBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.txt_Value);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.label1);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "smtBodyTextBox";
            this.Size = new System.Drawing.Size(288, 32);
            ((System.ComponentModel.ISupportInitialize)(this.txt_Value.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Title;
        private DevExpress.XtraEditors.TextEdit txt_Value;
    }
}
