using DevExpress.XtraSplashScreen;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCode.Common
{
    public class WrWaitShow
    {
        public void WaitShow(System.Windows.Forms.Form obj, string caption, string description)
        {
            // Open a Splash Screen
            SplashScreenManager.ShowForm(obj, typeof(WaitDialog), true, true, false);
            SplashScreenManager.Default.SetWaitFormCaption(caption);
            SplashScreenManager.Default.SetWaitFormDescription(description);
        }
        //데이터 베이스 작업 시작할때
        public void ProgressShow(System.Windows.Forms.Form obj, string caption, string description, int pos)
        {
            SplashScreenManager.ShowForm(obj, typeof(SplashScreen1), true, true, false);
        }
        //데이터 베이스 작업 종료
        public void Close()
        {

            SplashScreenManager.CloseForm(false);
        }
    }//end class
}
