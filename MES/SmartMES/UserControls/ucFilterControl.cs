using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;

namespace WindowsFormsApp7 {
    public partial class ucFilterControl : XtraUserControl, IFilterContent {
        private TextEdit textEdit;

        public ucFilterControl()
        {
            InitializeComponent();

            this.textEdit = new TextEdit();
            this.Controls.Add(this.textEdit);
            this.textEdit.Properties.AutoHeight = false;
            this.textEdit.Dock = DockStyle.Fill; 
            this.textEdit.EditValueChanged += TextEdit_EditValueChanged;
            this.textEdit.Visible = true;

            // 비활성화 상태일 때 표시할 텍스트 설정
            this.textEdit.Properties.NullValuePrompt = "";
            this.textEdit.Properties.NullValuePromptShowForEmptyValue = true;
            
            // 왼쪽에 아이콘 추가
            this.textEdit.Properties.ContextImageOptions.Image = global::SmartMES.Properties.Resources.search;
        }

        object changed = new object();
        /// <summary>검색창을 비워 메뉴에 적용된 검색 조건을 해제합니다.</summary>
        public void ClearFilter()
        {
            textEdit.EditValue = null;
        }

        private void TextEdit_EditValueChanged(object sender, EventArgs e)
        {
            EventHandler handler = Events[changed] as EventHandler;
            handler?.Invoke(this, EventArgs.Empty);
        }
        event EventHandler IFilterContent.FilterValueChanged
        {
            add { Events.AddHandler(changed, value); }
            remove { Events.RemoveHandler(changed, value); }
        }
        // Update any colors when the skin is changed.

        #region ComboBox
        void IFilterContent.UpdateAppearance()
        {
            // Update layout of controls when the size is changed.
        }

        void IFilterContent.UpdateLayout()
        {
            // For example, update the Textbox bounds.
            this.textEdit.Bounds = new Rectangle(20, (Height - this.textEdit.Height) / 2, Width - 40, this.textEdit.Height);
        }
        object IFilterContent.FilterValue { get { return this.textEdit.EditValue; } }
        Control IFilterContent.Control { get { return this; } }
        UserLookAndFeel IFilterContent.LookAndFeel { get { return LookAndFeel; } }

        #endregion
    }
}
