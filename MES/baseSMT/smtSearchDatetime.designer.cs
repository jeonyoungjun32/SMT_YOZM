
namespace baseSMT
{
    partial class smtSearchDatetime
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
            this.barEditEndDate = new DevExpress.XtraEditors.DateEdit();
            this.barEditStartDate = new DevExpress.XtraEditors.DateEdit();
            this.barEditType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbl_Title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.barEditEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barEditEndDate
            // 
            this.barEditEndDate.EditValue = null;
            this.barEditEndDate.Location = new System.Drawing.Point(325, 0);
            this.barEditEndDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.barEditEndDate.Name = "barEditEndDate";
            this.barEditEndDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.barEditEndDate.Properties.Appearance.Options.UseBackColor = true;
            this.barEditEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditEndDate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.barEditEndDate.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.barEditEndDate.Properties.MaskSettings.Set("mask", "d");
            this.barEditEndDate.Properties.UseMaskAsDisplayFormat = true;
            this.barEditEndDate.Size = new System.Drawing.Size(120, 32);
            this.barEditEndDate.TabIndex = 20;
            // 
            // barEditStartDate
            // 
            this.barEditStartDate.EditValue = null;
            this.barEditStartDate.Location = new System.Drawing.Point(200, 0);
            this.barEditStartDate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.barEditStartDate.Name = "barEditStartDate";
            this.barEditStartDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.barEditStartDate.Properties.Appearance.Options.UseBackColor = true;
            this.barEditStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditStartDate.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.DateTimeMaskManager));
            this.barEditStartDate.Properties.MaskSettings.Set("useAdvancingCaret", true);
            this.barEditStartDate.Properties.MaskSettings.Set("mask", "d");
            this.barEditStartDate.Properties.UseMaskAsDisplayFormat = true;
            this.barEditStartDate.Size = new System.Drawing.Size(120, 32);
            this.barEditStartDate.TabIndex = 19;
            // 
            // barEditType
            // 
            this.barEditType.EditValue = "";
            this.barEditType.Location = new System.Drawing.Point(0, 0);
            this.barEditType.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.barEditType.Name = "barEditType";
            this.barEditType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.barEditType.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.barEditType.Properties.Appearance.Options.UseBackColor = true;
            this.barEditType.Properties.Appearance.Options.UseForeColor = true;
            this.barEditType.Properties.Appearance.Options.UseTextOptions = true;
            this.barEditType.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.barEditType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barEditType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.barEditType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.barEditType.Size = new System.Drawing.Size(100, 32);
            this.barEditType.TabIndex = 21;
            // 
            // lbl_Title
            // 
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Pretendard SemiBold", 10.2F);
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(100, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(100, 32);
            this.lbl_Title.TabIndex = 23;
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // smtSearchDatetime
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.barEditEndDate);
            this.Controls.Add(this.barEditStartDate);
            this.Controls.Add(this.barEditType);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "smtSearchDatetime";
            this.Size = new System.Drawing.Size(666, 32);
            ((System.ComponentModel.ISupportInitialize)(this.barEditEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barEditType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit barEditEndDate;
        private DevExpress.XtraEditors.DateEdit barEditStartDate;
        private DevExpress.XtraEditors.ComboBoxEdit barEditType;
        private System.Windows.Forms.Label lbl_Title;
    }
}
