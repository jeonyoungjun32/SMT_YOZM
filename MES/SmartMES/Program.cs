using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraReports.Extensions;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMES.Common;
using Microsoft.Win32;
using SmartCommon.Common;
using System.Threading;
using DevExpress.XtraEditors;

namespace SmartMES
{
    internal static class Program
    {

        public static ReportStorageExtension reportStorage;
        public static ReportStorageExtension ReportStorage
        {
            get
            {
                return reportStorage;
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WrGlobal.GAM_SW = false; //감리용

            DevExpress.DashboardCommon.Localization.DashboardLocalizer.Active = new clsDashboardCommonLocalizer();
            DevExpress.DashboardWin.Localization.DashboardWinLocalizer.Active = new clsDashboardWinLocalizer();

            //WrGlobal.reportStorage = new DataSetReportStorage();
            ReportStorageExtension.RegisterExtensionGlobal(WrGlobal.reportStorage);

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            bool isFontInstall = false;
            foreach (FontFamily fontFamily in installedFontCollection.Families)
            {
                if (fontFamily.Name.Equals("Pretendard SemiBold"))
                {
                    isFontInstall = true;
                }
            }

            if (!isFontInstall)
            {
                Shell32.Shell shell = new Shell32.Shell();
                Shell32.Folder fontFolder = shell.NameSpace(0x14);
                fontFolder.CopyHere(Application.StartupPath + @"\Fonts\Pretendard-SemiBold.ttf");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // 아이콘
            DevExpress.XtraEditors.FormIconOptions.Default.Icon = new System.Drawing.Icon(new System.IO.MemoryStream(Properties.Resources.YOZM_favicon));

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // 현재 MES 화면의 조회·저장·삭제 버튼을 Ctrl+Q, Ctrl+S, Ctrl+D로 실행합니다.
            Application.AddMessageFilter(new clsShortcut());

            string strShowFrom = "";
            RegistryKey reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\SNEXISMES");
            if (reg != null)
            {
                object showFrom = reg.GetValue("StartForm");
                if (showFrom != null)
                {
                    strShowFrom = showFrom.ToString();
                }
            }

            if (strShowFrom != "")
            {
                var frm = Activator.CreateInstance(Type.GetType(strShowFrom), null) as Form;
                frm.WindowState = FormWindowState.Maximized;
                Application.Run(frm);
            }
            else if (new Frm_Login().ShowDialog() == DialogResult.OK)
            {
                Application.Run(new Frm_Main());
            }
        }


        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (IsKnownAccessibilityBug(e.Exception)) return; // 조용히 무시
            MessageBox.Show(e.Exception.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex && IsKnownAccessibilityBug(ex)) return;
        }

        private static bool IsKnownAccessibilityBug(Exception ex)
        {
            return ex is NullReferenceException
                && ex.StackTrace != null
                && (ex.StackTrace.Contains("ActualFillStyle")
                    || ex.StackTrace.Contains("ChartControlAccessible")
                    || ex.StackTrace.Contains("CreateAccessibleChartViewData"));
        }
    }
}
