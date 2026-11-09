
namespace baseSMT
{
    partial class smtBodyMemoEdit
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
            this.lbl_Title = new System.Windows.Forms.Label();
            this.txt_Memo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Memo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Title.Font = new System.Drawing.Font("Pretendard SemiBold", 10.2F);
            this.lbl_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(70)))), ((int)(((byte)(68)))));
            this.lbl_Title.Location = new System.Drawing.Point(0, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl_Title.Size = new System.Drawing.Size(100, 150);
            this.lbl_Title.TabIndex = 7;
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_Memo
            // 
            this.txt_Memo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Memo.Location = new System.Drawing.Point(100, 0);
            this.txt_Memo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Memo.Name = "txt_Memo";
            this.txt_Memo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txt_Memo.Properties.Appearance.Options.UseBackColor = true;
            this.txt_Memo.Properties.Padding = new System.Windows.Forms.Padding(2);
            this.txt_Memo.Size = new System.Drawing.Size(406, 150);
            this.txt_Memo.TabIndex = 87;
            // 
            // smtBodyMemoEdit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.txt_Memo);
            this.Controls.Add(this.lbl_Title);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "smtBodyMemoEdit";
            this.Size = new System.Drawing.Size(506, 150);
            ((System.ComponentModel.ISupportInitialize)(this.txt_Memo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Title;
        private DevExpress.XtraEditors.MemoEdit txt_Memo;
    }
}
