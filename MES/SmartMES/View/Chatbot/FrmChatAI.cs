using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using SmartMES.Models.ChatBot;
using System.Data;
using SmartMES.Common;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;
using SmartCommon.Common;

namespace SmartMES.View.Chatbot
{
    public partial class FrmChatAI : XtraForm
    {
        #region 상태값
        private string _currentIntent = null;   // 대화 흐름
        #endregion

        ChatBot cb = new ChatBot();

        class ChatItem
        {
            public bool IsUser { get; set; }
            public string Text { get; set; }
            public bool IsLink { get; set; }
            public string Time { get; set; } = DateTime.Now.ToString("HH:mm");

            public override string ToString() => Text;
        }

        public FrmChatAI()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.Sizable;
            listBoxControlChat.ItemAutoHeight = true;
            listBoxControlChat.Appearance.TextOptions.WordWrap = WordWrap.Wrap;

            listBoxControlChat.MeasureItem += ListBoxControlChat_MeasureItem;
            listBoxControlChat.DrawItem += ListBoxControlChat_DrawItem;

            listBoxControlChat.Resize += (s, e) =>
            {
                listBoxControlChat.Invalidate();
            };

            AddBot("안녕하세요! 👋\n저는 SmartMES AI 도우미입니다.\n편하게 물어봐 주세요!\n\n예시 질문:\n• A품목 재고 얼마나 남았어?\n• 오늘 생산량 알려줘\n• 설비 현황 어때?\n• 오늘 작업지시 뭐야?");

            memoEditInput.KeyDown += MemoEditInput_KeyDown;
            btnSend.Click += async (s, e) => await Send();

            // ✅ 우클릭 복사 메뉴 설정
            var contextMenu = new ContextMenuStrip();
            var copyItem = new ToolStripMenuItem("📋 복사");

            contextMenu.Items.Add(copyItem);
            listBoxControlChat.ContextMenuStrip = contextMenu;

            listBoxControlChat.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    var lb = listBoxControlChat;

                    Point pt = lb.PointToClient(Control.MousePosition);

                    int index = lb.IndexFromPoint(pt);

                    if (index >= 0)
                    {
                        lb.SelectedIndex = index;
                        lb.Focus();
                    }
                }
            };

            // ✅ 복사 클릭 이벤트
            copyItem.Click += (s, e) =>
            {
                Point pt = listBoxControlChat.PointToClient(Control.MousePosition);

                int index = listBoxControlChat.IndexFromPoint(pt);
                if (index < 0) return;

                var item = listBoxControlChat.Items[index];

                string text = item is ChatItem chat ? chat.Text : item?.ToString();

                if (string.IsNullOrWhiteSpace(text)) return;

                Clipboard.SetText(text);
            };

            contextMenu.Items.Add(copyItem);
            listBoxControlChat.ContextMenuStrip = contextMenu;
        }

        #region Gemini API

        private string ExtractText(string json)
        {
            try
            {
                var obj = JObject.Parse(json);

                var parts = obj["candidates"]?[0]?["content"]?["parts"] as JArray;
                if (parts != null)
                {
                    foreach (var part in parts)
                    {
                        string t = part["text"]?.ToString();
                        if (!string.IsNullOrEmpty(t))
                            return t.Replace("**", "").Replace("* ", "• ").Replace("# ", "").Trim();
                    }
                }

                string error = obj["error"]?["message"]?.ToString();
                if (!string.IsNullOrEmpty(error))
                {
                    // ✅ 할당량 초과 감지
                    if (error.Contains("quota") || error.Contains("Quota") || error.Contains("429"))
                        return "⏳ 요청이 너무 많습니다. 잠시 후 다시 시도해주세요.";

                    return $"API 오류: {error}";
                }

                return "응답 없음";
            }
            catch (Exception ex)
            {
                return "파싱 실패: " + ex.Message;
            }
        }
        #endregion


        #region 핵심 Send
        private async Task Send()
        {
            var text = memoEditInput.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            AddUser(text);
            memoEditInput.Text = "";

            int loadingIndex = listBoxControlChat.Items.Count;
            AddBot("답변 생성중...");

            string answer = await CallGeminiWithTools(text);

            if (loadingIndex < listBoxControlChat.Items.Count)
                listBoxControlChat.Items.RemoveAt(loadingIndex);

            AddBot(answer);
        }

        private async Task<string> CallGeminiWithTools(string userText)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiKey = "YOUR_SECRET";
                    //string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";
                    //string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-lite:generateContent?key={apiKey}";
                    string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.1-flash-lite:generateContent?key={apiKey}";

                    string prompt = $@"아래 질문을 분석하세요.

                                        MES 조회가 필요하면 JSON만 반환하세요.
                                        형식: {{""intent"":""의도값"",""keyword"":""검색어 또는 빈문자열""}}

                                        의도값 목록:
                                        - Stock : 재고 조회 (특정 품목이면 keyword에 품목)
                                        - OrderResult : 생산 실적 조회
                                        - WorkOrder : 작업지시 조회
                                        - Machine : 설비 현황 조회

                                        규칙:
                                        - 재고 조회는 품목명을 keyword 에 넣는다
                                        - [중요] 날짜나 기간이 포함된 질문(OrderResult, WorkOrder 등)은 해당 날짜를 반드시 'yyyy-MM-dd' 형식으로 변환하여 keyword 에 넣는다.
                                          (예: '2026년 4월 1일', '4월 1일', '2026.04.01' 모두 '2026-04-01'로 변환)
                                        - 만약 '오늘', '어제', '내일', '금일', '전일' 같은 상대적인 날짜 키워드인 경우, 그 텍스트 그대로 keyword 에 넣는다.
                                        - 날짜나 품목 등 특정 키워드가 없으면 keyword 는 빈문자열로 처리한다.

                                        MES 조회가 필요없는 질문인 경우는
                                        → 한국어로 질문에 대한 직접적인 답변만 간단히 출력.

                                        질문: {userText}";

                    var body = new JObject
                    {
                        ["contents"] = new JArray
                    {
                        new JObject
                        {
                            ["parts"] = new JArray { new JObject { ["text"] = prompt } }
                        }
                    }
                    };

                    var res = await client.PostAsync(url,
                        new StringContent(body.ToString(), Encoding.UTF8, "application/json"));

                    if ((int)res.StatusCode == 429) return "⏳ 요청이 너무 많습니다. 잠시 후 다시 시도해주세요.";

                    var result = ExtractText(await res.Content.ReadAsStringAsync());

                    // JSON인지 확인
                    var cleaned = result.Replace("```json", "").Replace("```", "").Trim();
                    JObject intentObj;
                    try
                    {
                        intentObj = JObject.Parse(cleaned);
                    }
                    catch
                    {
                        return result; // other → 바로 반환
                    }

                    string intent = intentObj["intent"]?.ToString() ?? "other";
                    string keyword = intentObj["keyword"]?.ToString() ?? "";

                    string dbResult = GetDbResult(intent, keyword);

                    // DB 결과 없으면 기존 답변
                    if (string.IsNullOrEmpty(dbResult))
                        return result;

                    // 답변 자연스럽게 변환 요청
                    string finalPrompt = $@"당신은 SmartMES AI 챗봇입니다.
                                            아래 데이터를 사용자에게 보기 좋게 정리하세요.

                                            규칙:
                                            - 핵심 정보만 간결하게 출력
                                            - 가능한 경우 제목 1줄 + 내용 정리
                                            - 여러 항목이면 줄바꿈 + '•' 리스트 사용
                                            - 데이터가 단일 값이면 한 줄로 자연스럽게 표현
                                            - 최대 5줄 이내

                                            데이터: {dbResult}
                                            ";

                    var finalBody = new JObject
                    {
                        ["contents"] = new JArray
                        {
                            new JObject
                            {
                                ["parts"] = new JArray
                                {
                                    new JObject { ["text"] = finalPrompt }
                                }
                            }
                        }
                    };

                    var finalRes = await client.PostAsync(url,
                        new StringContent(finalBody.ToString(), Encoding.UTF8, "application/json"));

                    return ExtractText(await finalRes.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                return "오류: " + ex.Message;
            }
        }

        private string GetDbResult(string intent, string keyword)
        {
            string workDate = ParseWorkDate(keyword);

            switch (intent)
            {
                case "Stock":   //재고
                    cb.Material = keyword;

                    var dt = cb.Select_StockListView_Material();

                    if (dt == null || dt.Rows.Count == 0)
                        return string.IsNullOrEmpty(keyword) ? "재고 데이터 없음" : $"'{keyword}' 재고 없음";

                    var sb = new StringBuilder();

                    sb.AppendLine($"📦 재고 현황 ({dt.Rows.Count:N0}건)");

                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(
                            $"• [{row["ItemCode"]}] {row["ItemName"]} : " +
                            $"{row["StockQty"].ConvertInt_Null_To_Zero():N0} {row["UnitCode"]}"
                        );
                    }

                    return sb.ToString();

                case "OrderResult": //실적
                    cb.WorkDate = workDate;

                    dt = cb.Select_PM_OrderResult();

                    if (dt == null || dt.Rows.Count == 0)
                        return string.IsNullOrEmpty(keyword) ? "생산실적 데이터 없음" : $"{keyword} 생산실적 데이터 없음";

                    sb = new StringBuilder();
                    sb.AppendLine($"📋 {(string.IsNullOrEmpty(keyword) ? "전체" : keyword)} 생산실적 ({dt.Rows.Count:N0}건)");

                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(
                            $"• {row["ItemName"]} | " +
                            $"생산:{row["WorkQty"].ConvertInt_Null_To_Zero():N0} / " +
                            $"양품:{row["GoodQty"].ConvertInt_Null_To_Zero():N0}" +
                            $"불량:{row["NgQty"].ConvertInt_Null_To_Zero():N0}"
                        );
                    }

                    return sb.ToString();

                case "WorkOrder": //지시
                    cb.WorkDate = workDate;

                    dt = cb.Select_PM_OrderProcess();

                    if (dt == null || dt.Rows.Count == 0)
                        return string.IsNullOrEmpty(keyword)
                            ? "작업지시 데이터 없음"
                            : $"{keyword} 작업지시 데이터 없음";

                    int total = dt.Rows.Count;

                    int working = dt.AsEnumerable()
                                    .Count(r => r["Status"]?.ToString() != "완료");

                    int complete = dt.AsEnumerable()
                                     .Count(r => r["Status"]?.ToString() == "완료");

                    sb = new StringBuilder();

                    sb.AppendLine($"📋 {(string.IsNullOrEmpty(keyword) ? "전체" : keyword)} 작업지시");
                    sb.AppendLine($"총 {total:N0}건 | 작업중 {working:N0}건 | 완료 {complete:N0}건");

                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(
                            $"• {row["ItemName"]} | " +
                            $"지시:{row["OrderQty"].ConvertInt_Null_To_Zero():N0} / " +
                            $"작업:{row["WorkQty"].ConvertInt_Null_To_Zero():N0} / " +
                            $"상태:{row["Status"]}"
                        );
                    }

                    return sb.ToString();

                case "Machine":
                    return "설비 현황 조회 (구현 필요)";

                default:
                    return "";
            }
        }

        private string ParseWorkDate(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return "";

            keyword = keyword.Trim();

            if (keyword.Contains("오늘") || keyword.Contains("금일") || keyword.Contains("당일") || keyword.Contains("현재") || keyword.Contains("today"))
            {
                return DateTime.Now.ToString("yyyyMMdd");
            }

            if (keyword.Contains("어제") || keyword.Contains("전일"))
            {
                return DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            }

            if (keyword.Contains("내일"))
            {
                return DateTime.Now.AddDays(1).ToString("yyyyMMdd");
            }

            return keyword.Replace("-", "").Replace(".", "").Replace("/", "");
        }

        #endregion

        #region UI
        private void AddUser(string text)
        {
            listBoxControlChat.Items.Add(new ChatItem { IsUser = true, Text = text });
            listBoxControlChat.TopIndex = listBoxControlChat.ItemCount - 1;
        }

        private void AddBot(string text)
        {
            listBoxControlChat.Items.Add(new ChatItem { IsUser = false, Text = text });
            listBoxControlChat.TopIndex = listBoxControlChat.ItemCount - 1;
        }

        private void MemoEditInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                _ = Send();
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            var arc = new Rectangle(bounds.Location, new Size(d, d));
            path.AddArc(arc, 180, 90);

            arc.X = bounds.Right - d;
            path.AddArc(arc, 270, 90);

            arc.Y = bounds.Bottom - d;
            path.AddArc(arc, 0, 90);

            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void ListBoxControlChat_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var item = listBoxControlChat.Items[e.Index] as ChatItem;
            if (item == null) return;

            bool isUser = item.IsUser;
            string text = item.Text ?? "";

            e.Appearance.DrawBackground(e.Cache, e.Bounds);

            int padding = 10;
            int avatarSize = 36;
            int avatarMargin = 8;
            int timeWidth = 35; // 시간 표시 공간

            int leftMargin = !isUser ? avatarSize + avatarMargin * 2 : timeWidth + 10;
            int rightMargin = isUser ? timeWidth + 10 : 10;
            int maxWidth = e.Bounds.Width - leftMargin - rightMargin;
            int textMaxWidth = maxWidth - (padding * 2);

            var textSize = e.Appearance.CalcTextSize(e.Cache, text, textMaxWidth).ToSize();
            int bubbleWidth = Math.Max(60, Math.Min(textSize.Width + padding * 2, maxWidth));
            int bubbleHeight = textSize.Height + padding * 2;

            int avatarY = e.Bounds.Top + 4;

            if (!isUser)
            {
                var avatarRect = new Rectangle(e.Bounds.Left + avatarMargin, avatarY, avatarSize, avatarSize);
                var old2 = e.Graphics.SmoothingMode;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var avatarBrush = new SolidBrush(Color.FromArgb(99, 99, 102)))
                    e.Graphics.FillEllipse(avatarBrush, avatarRect);
                using (var font = new Font(e.Appearance.Font.FontFamily, 8, FontStyle.Bold))
                {
                    var initSize = TextRenderer.MeasureText("SMT", font);
                    var initRect = new Rectangle(
                        avatarRect.Left + (avatarSize - initSize.Width) / 2,
                        avatarRect.Top + (avatarSize - initSize.Height) / 2,
                        initSize.Width, initSize.Height);
                    TextRenderer.DrawText(e.Graphics, "SMT", font, initRect, Color.White);
                }
                e.Graphics.SmoothingMode = old2;
            }

            int x = isUser
                ? e.Bounds.Right - bubbleWidth - timeWidth - 10
                : e.Bounds.Left + avatarSize + avatarMargin * 2;

            var bubbleRect = new Rectangle(x, e.Bounds.Top + 4, bubbleWidth, bubbleHeight);
            var textRect = new Rectangle(
                bubbleRect.Left + padding,
                bubbleRect.Top + padding,
                bubbleRect.Width - padding * 2,
                bubbleRect.Height - padding * 2);

            var old = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var path = GetRoundedRect(bubbleRect, 12))
            using (var brush = new SolidBrush(isUser ? Color.FromArgb(0, 122, 255) : Color.FromArgb(229, 229, 234)))
                e.Graphics.FillPath(brush, path);

            var flags = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding;
            TextRenderer.DrawText(e.Graphics, text, e.Appearance.Font, textRect,
                isUser ? Color.White : Color.Black, flags);

            // 시간
            string timeText = item.Time ?? DateTime.Now.ToString("HH:mm");
            using (var timeFont = new Font(e.Appearance.Font.FontFamily, 7, FontStyle.Regular))
            {
                var timeSize = TextRenderer.MeasureText(timeText, timeFont);
                int timeX = isUser
                    ? bubbleRect.Left - timeSize.Width - 4
                    : bubbleRect.Right + 4;
                int timeY = bubbleRect.Bottom - timeSize.Height - 2;
                TextRenderer.DrawText(e.Graphics, timeText, timeFont,
                    new Rectangle(timeX, timeY, timeSize.Width, timeSize.Height), Color.Gray);
            }

            e.Graphics.SmoothingMode = old;
            e.Handled = true;
        }

        private void ListBoxControlChat_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = listBoxControlChat.Items[e.Index] as ChatItem;
            if (item == null) return;

            string text = item.Text ?? "";

            int padding = 10;
            int avatarSize = 36;
            int avatarMargin = 8;
            bool isUser = item.IsUser;

            int leftMargin = !isUser ? avatarSize + avatarMargin * 2 : 10;
            int rightMargin = isUser ? 10 : 20;

            // ✅ 너비를 아주 보수적으로 잡기
            int maxWidth = Math.Max(100, e.ItemWidth - leftMargin - rightMargin - 40);
            int textWidth = maxWidth - (padding * 2) - 20;

            var textSize = TextRenderer.MeasureText(
                text,
                listBoxControlChat.Appearance.Font,
                new Size(textWidth, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPadding
            );

            // ✅ 높이 넉넉하게
            e.ItemHeight = textSize.Height + padding * 2 + 30;
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}