using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SmartMES.Common
{
    /// <summary>
    /// MES에서 Ctrl+Q, Ctrl+S, Ctrl+D로 현재 화면의 조회·저장·삭제 버튼을 실행합니다.
    /// </summary>
    internal sealed class clsShortcut : IMessageFilter
    {
        private bool executing;

        public bool PreFilterMessage(ref Message message)
        {
            const int keyDown = 0x0100;
            if (message.Msg != keyDown || Control.ModifierKeys != Keys.Control)
                return false;

            // 팝업이 열려 있으면 키 입력을 받은 팝업을 기준으로 버튼을 찾습니다.
            var form = Control.FromChildHandle(message.HWnd)?.FindForm() ?? Form.ActiveForm;
            bool repeat = (message.LParam.ToInt64() & (1L << 30)) != 0;
            return TryExecute(form, (Keys)message.WParam.ToInt32(), repeat);
        }

        /// <summary>
        /// 현재 MDI 탭 또는 팝업의 버튼을 실행하고, 키를 누르고 있을 때 반복 실행을 막습니다.
        /// </summary>
        internal bool TryExecute(Form form, Keys key, bool repeat = false)
        {
            string caption;
            switch (key)
            {
                case Keys.Q: caption = "조회"; break;
                case Keys.S: caption = "저장"; break;
                case Keys.D: caption = "삭제"; break;
                default: return false;
            }

            if (executing)
                return true;
            if (form == null || form.IsDisposed)
                return false;

            form = form.ActiveMdiChild ?? form;
            var controls = VisibleControls(form).ToList();
            var buttons = controls.Where(control => control is IButtonControl && MatchesCaption(control.Text, caption)).ToList();
            var button = FindButton(form, buttons);
            if (buttons.Count > 0)
            {
                if (button == null || !button.Enabled)
                    return false;
                return Execute(repeat, () => ClickButton(button));
            }

            // 리본 화면은 현재 페이지에 표시된 명령만 후보로 사용합니다.
            var ribbonCommands = controls.OfType<RibbonControl>()
                .Where(ribbon => ribbon.Enabled && ribbon.SelectedPage != null)
                .SelectMany(ribbon => ribbon.SelectedPage.Groups.Cast<RibbonPageGroup>())
                .Where(group => group.Visible)
                .SelectMany(group => group.ItemLinks.Cast<BarItemLink>())
                .Where(link => link.Visible && link.Item is BarButtonItem
                    && link.Item.Visibility != BarItemVisibility.Never && MatchesCaption(link.Caption, caption))
                .GroupBy(link => link.Item)
                .Select(links => links.First())
                .ToList();
            if (ribbonCommands.Count != 1 || !ribbonCommands[0].Item.Enabled)
                return false;
            var command = ribbonCommands[0];
            return Execute(repeat, () => command.Item.PerformClick(command));
        }

        private bool Execute(bool repeat, System.Action click)
        {
            if (repeat)
                return true;
            try
            {
                executing = true;
                click();
                return true;
            }
            finally
            {
                executing = false;
            }
        }

        private static void ClickButton(Control control)
        {
            var button = control as SimpleButton;
            if (button == null)
            {
                ((IButtonControl)control).PerformClick();
                return;
            }

            bool allowFocus = button.AllowFocus;
            try
            {
                // 포커스를 막은 DevExpress 버튼도 클릭을 실행할 수 있도록 잠시 허용합니다.
                button.AllowFocus = true;
                button.PerformClick();
            }
            finally
            {
                if (!button.IsDisposed)
                    button.AllowFocus = allowFocus;
            }
        }

        private static Control FindButton(Control root, List<Control> buttons)
        {
            if (buttons.Count <= 1)
                return buttons.FirstOrDefault();

            // 분할 화면에서 같은 명령이 여러 개면 포커스가 있는 영역의 버튼을 우선합니다.
            var focus = buttons.Where(button => button.ContainsFocus).ToList();
            if (focus.Count == 1)
                return focus[0];
            for (Control scope = root; scope != null;)
            {
                var child = scope.Controls.Cast<Control>().FirstOrDefault(control => control.ContainsFocus);
                if (child == null)
                    break;
                var local = buttons.Where(button => child == button || child.Contains(button)).ToList();
                if (local.Count == 1)
                    return local[0];
                if (local.Count == 0)
                    break;
                scope = child;
            }

            var toolbarButtons = buttons.Where(IsInCommonBar).ToList();
            return toolbarButtons.Count == 1 ? toolbarButtons[0] : null;
        }

        private static bool MatchesCaption(string text, string caption)
        {
            string label = (text ?? "").Replace("&", "").Replace(" ", "").Trim();
            return label == caption
                || (caption == "조회" && label == "검색")
                || (caption == "삭제" && label == "행삭제");
        }

        private static IEnumerable<Control> VisibleControls(Control root)
        {
            foreach (Control child in root.Controls)
            {
                if (child.IsDisposed || !child.Visible)
                    continue;

                yield return child;
                foreach (var nested in VisibleControls(child))
                    yield return nested;
            }
        }

        private static bool IsInCommonBar(Control control)
        {
            for (var parent = control.Parent; parent != null; parent = parent.Parent)
                if (parent is baseSMT.smtCommonBar)
                    return true;
            return false;
        }
    }
}
