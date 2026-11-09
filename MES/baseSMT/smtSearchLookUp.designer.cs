
namespace baseSMT
{
    partial class smtSearchLookUp
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
            this.lup_Value = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.pView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.lup_Value.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pView)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_Title.Font = new System.Drawing.Font("Pretendard SemiBold", 10.2F);
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(0, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(100, 32);
            this.lbl_Title.TabIndex = 9;
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lup_Value
            // 
            this.lup_Value.EditValue = "";
            this.lup_Value.Location = new System.Drawing.Point(100, 0);
            this.lup_Value.Name = "lup_Value";
            this.lup_Value.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lup_Value.Properties.Appearance.Options.UseBackColor = true;
            this.lup_Value.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.lup_Value.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lup_Value.Properties.DisplayMember = "코드명";
            this.lup_Value.Properties.NullText = "";
            this.lup_Value.Properties.PopupView = this.pView;
            this.lup_Value.Properties.PopupWidthMode = DevExpress.XtraEditors.PopupWidthMode.ContentWidth;
            this.lup_Value.Properties.ValueMember = "코드";
            this.lup_Value.Size = new System.Drawing.Size(188, 32);
            this.lup_Value.TabIndex = 20;
            // 
            // pView
            // 
            this.pView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.pView.Name = "pView";
            this.pView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.pView.OptionsView.ShowGroupPanel = false;
            // 
            // smtSearchLookUp
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lup_Value);
            this.Controls.Add(this.lbl_Title);
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "smtSearchLookUp";
            this.Size = new System.Drawing.Size(288, 32);
            ((System.ComponentModel.ISupportInitialize)(this.lup_Value.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Title;
        private DevExpress.XtraEditors.SearchLookUpEdit lup_Value;
        private DevExpress.XtraGrid.Views.Grid.GridView pView;
    }
}
