using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SmartMES.View.Chatbot
{
    public partial class ChatMain : XtraForm
    {
        private readonly Dictionary<string, string> _faq =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "운영", "운영 시간은 평일 09:00 ~ 18:00 입니다." },
                { "환불", "구매일 포함 14일 이내 미개봉 상품에 한해 환불 가능합니다." },
                { "배송", "평균 2~3일 소요됩니다. 지역/재고에 따라 달라질 수 있어요." }
            };

        public ChatMain()
        {
            InitializeComponent();

            AddBot("안녕하세요! 간단 챗봇입니다. 궁금한 내용을 입력해 보세요 😊");

            memoEditInput.KeyDown += MemoEditInput_KeyDown;
            btnSend.Click += (s, e) => Send();
        }

        private void MemoEditInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift && !e.Control && !e.Alt)
            {
                e.SuppressKeyPress = true;
                Send();
            }
            else if (e.KeyCode == Keys.Enter && e.Control)
            {
                // 줄바꿈 허용
            }
        }

        private void Send()
        {
            var text = memoEditInput.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            AddUser(text);
            memoEditInput.Text = string.Empty;

            var answer = FindAnswer(text);
            AddBot(string.IsNullOrWhiteSpace(answer)
                ? "관련 답변을 찾지 못했어요. 다른 표현으로 다시 물어봐 주세요!"
                : answer);
        }

        private string FindAnswer(string userText)
        {
            foreach (var kv in _faq)
            {
                if (userText.IndexOf(kv.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return kv.Value;
            }
            return null;
        }


        private void AddUser(string text) => listBoxControlChat.Items.Add($"[You {DateTime.Now:HH:mm}] {text}");
        private void AddBot(string text) => listBoxControlChat.Items.Add($"[Bot {DateTime.Now:HH:mm}] {text}");
    }
}
