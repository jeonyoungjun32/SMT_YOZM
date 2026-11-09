using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

using SmartCommon.Common;

namespace SmartMES.View.Chatbot
{
    public partial class FrmChatAI_Web : XtraForm
    {
        private readonly WebView2 _web = new WebView2 { Dock = DockStyle.Fill };

        public FrmChatAI_Web()
        {
            InitializeComponent();

            Controls.Add(_web);
            Load += async (s, e) => await InitWebAsync();
        }

        private async Task InitWebAsync()
        {
            try
            {
                await _web.EnsureCoreWebView2Async(null);
                // MES가 밝은 테마라 챗봇만 어두우면 붕 뜸. 윈도우 다크모드와 무관하게 밝게 고정
                _web.CoreWebView2.Profile.PreferredColorScheme = CoreWebView2PreferredColorScheme.Light;

                _web.NavigationCompleted += async (s, e) =>
                {
                    await _web.ExecuteScriptAsync(@"
                        document.documentElement.style.backgroundColor = '#ffffff';
                        document.body.style.backgroundColor = '#ffffff';
                        document.body.style.color = '#222222';
                    ");
                };

                // 서버 주소와 키는 App.config( 코드에 박으면 서버 옮길 때 재빌드해야 함 )
                string baseUrl = ConfigurationManager.AppSettings["ChatbotApiUrl"]
                                 ?? "http://127.0.0.1:8000";
                string apiKey = ConfigurationManager.AppSettings["ChatbotApiKey"] ?? "";

                // 채팅 화면에는 로그인 개념이 없어서, MES 접속 정보를 넘겨줌
                //   user - 대화로그의 EmployeeId/CreId. 대화목록·되묻기 맥락을 가르는 기준
                //   corp -
                //   db   - 이 MES가 붙어 있는 DB. 현업은 이거 말고 고를 수 없음
                //   key  - 서버 .env의 API_KEY. 화면이 이 값을 받아 이후 API 호출에 헤더로 붙임.
                //          없으면 화면은 뜨지만 조회가 전부 401이 됨
                string query = "?user=" + Uri.EscapeDataString(WrGlobal.LoginID ?? "")
                             + "&corp=" + Uri.EscapeDataString(WrGlobal.CorpID ?? "")
                             + "&db=" + Uri.EscapeDataString(WrGlobal.DBName ?? "")
                             + "&key=" + Uri.EscapeDataString(apiKey);

                _web.Source = new Uri(baseUrl.TrimEnd('/') + "/ui/" + query);
            }
            catch (Exception ex)
            {

                XtraMessageBox.Show(
                    "채팅 화면을 띄우지 못했습니다.\r\n"
                    + "챗봇 서버 주소와 WebView2 런타임 설치를 확인해 주세요.\r\n\r\n"
                    + "주소: " + (ConfigurationManager.AppSettings["ChatbotApiUrl"] ?? "(미설정)")
                    + "\r\n" + ex.Message);
            }
        }
    }
}
