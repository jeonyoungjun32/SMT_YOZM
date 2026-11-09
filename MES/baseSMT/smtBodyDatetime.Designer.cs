
namespace baseSMT
{
    partial class smtBodyDatetime
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
            this.barEditDate = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditDate.Properties.CalendarTimeProperties)).BeginInit();
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
            this.lbl_Title.Size = new System.Drawing.Size(100, 32);
            this.lbl_Title.TabIndex = 11;
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // barEditDate
            // 
            this.barEditDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.barEditDate.EditValue = null;
            this.barEditDate.Location = new System.Drawing.Point(100, 0);
            this.barEditDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.barEditDate.Name = "barEditDate";
            this.barEditDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.barEditDate.Properties.Appearance.Options.UseBackColor = true;
            this.barEditDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditDate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.barEditDate.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.barEditDate.Properties.MaskSettings.Set("mask", "d");
            this.barEditDate.Properties.UseMaskAsDisplayFormat = true;
            this.barEditDate.Size = new System.Drawing.Size(188, 32);
            this.barEditDate.TabIndex = 20;
            // 
            // smtBodyDatetime
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.barEditDate);
            this.Controls.Add(this.lbl_Title);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "smtBodyDatetime";
            this.Size = new System.Drawing.Size(288, 32);
            ((System.ComponentModel.ISupportInitialize)(this.barEditDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Title;
        private DevExpress.XtraEditors.DateEdit barEditDate;
    }
}
