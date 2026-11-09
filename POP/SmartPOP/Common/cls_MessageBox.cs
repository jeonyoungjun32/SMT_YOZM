using DevExpress.Utils.Drawing.Helpers;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMT_MES.Controls
{
    public static class cls_MessageBox
    {
        #region Message
        public static string DelMsg = "삭제내역이 있습니다. 삭제하시겠습니까?";
        public static string DelMsg_Select = "선택한 데이터를 삭제하시겠습니까?";
        public static string DelCaption = "삭제";
        public static string Del_Comp = "삭제가 완료되었습니다.";
        public static string Del_Fail = "삭제 중 오류가 발생했습니다";

        public static string SaveCaption = "삭제";
        public static string Save_Comp = "저장이 완료되었습니다.";
        public static string Save_Fail = "저장 중 오류가 발생했습니다.";
        #endregion

        public static DialogResult Show(string message = "", string caption = "", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();

            switch (buttons)
            {
                //AbortRetryIgnore    2
                //CancelTryContinue   6
                //RetryCancel 5
                case MessageBoxButtons.OKCancel: args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.Cancel }; break;
                case MessageBoxButtons.YesNo: args.Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No }; break;
                case MessageBoxButtons.YesNoCancel: args.Buttons = new DialogResult[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel }; break;
            }

            if (icon != MessageBoxIcon.None)
            {
                if (icon == MessageBoxIcon.Question) args.Icon = StockIconHelper.GetWindows8AssociatedIcon(SystemIcons.Question);
                else if (icon == MessageBoxIcon.Error) args.Icon = StockIconHelper.GetWindows8AssociatedIcon(SystemIcons.Error);
                else if (icon == MessageBoxIcon.Information) args.Icon = StockIconHelper.GetWindows8AssociatedIcon(SystemIcons.Information);
                else if (icon == MessageBoxIcon.Warning) args.Icon = StockIconHelper.GetWindows8AssociatedIcon(SystemIcons.Warning);
            }

            args.Caption = caption;
            args.Text = message;
            args.AllowHtmlText = DefaultBoolean.True;

            #region 자동 닫기 옵션
            //args.AutoCloseOptions.Delay = 0;
            //args.AutoCloseOptions.ShowTimerOnDefaultButton = true;
            #endregion

            args.Showing += Args;

            return XtraMessageBox.Show(args);
        }

        private static void Args(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Appearance.FontSizeDelta = 10;
            e.Form.Appearance.FontStyleDelta = System.Drawing.FontStyle.Bold;

            MessageButtonCollection buttons = e.Buttons as MessageButtonCollection;

            SimpleButton BtnYes = buttons[DialogResult.Yes] as SimpleButton;
            SimpleButton BtnNo = buttons[DialogResult.No] as SimpleButton;
            SimpleButton BtnOk = buttons[DialogResult.OK] as SimpleButton;
            SimpleButton BtnCancel = buttons[DialogResult.Cancel] as SimpleButton;

            if (BtnYes != null)
            {
                SetMessageBoxButton(BtnYes);
            }

            if (BtnNo != null)
            {
                SetMessageBoxButton(BtnNo);
            }

            if (BtnOk != null)
            {
                SetMessageBoxButton(BtnOk);
            }

            if (BtnCancel != null)
            {
                SetMessageBoxButton(BtnCancel);
            }
        }

        private static void SetMessageBoxButton(SimpleButton button)
        {
            button.Appearance.Font = new Font("D2 Coding", 18, FontStyle.Regular);
            button.Height += 15;
            button.Width += 40;
        }
    }
}
