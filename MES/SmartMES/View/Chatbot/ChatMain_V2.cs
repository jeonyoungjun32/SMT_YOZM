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
using SmartCommon.Common;

namespace SmartMES.View.Chatbot
{
    public partial class ChatMain_V2 : XtraForm
    {
        #region 윈폼 움직이도록 따로 설정
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        #endregion

        #region 명령어
        // 자주 묻는 질문(FAQ) 데이터 (키워드-답변 쌍)
        private readonly Dictionary<string, string> _faq =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "야", "기본 예절을 지켜주세요🙏" },
                { "얌마",  "기본 예절을 지켜주세요🙏" },
                { "임마",  "기본 예절을 지켜주세요🙏" },
                { "고마워",  "천만에요👍" },
                { "감사",  "천만에요👍" },
                { "안녕",  "안녕하세요😆" },
                { "도움말",  "기능 준비중입니다." },
                { "종료",  "자주 사용하는 질문을 마칩니다." },
            };
        #endregion

        ChatBot cb = new ChatBot();

        private bool _awaitingStockQuery = false; // 재고 질의 대기 상태 플래그
        private DataTable _lastdt;

        class ChatItem
        {
            public bool IsUser { get; set; }
            public string Text { get; set; }
            public bool IsLink { get; set; }
            public override string ToString() => Text; // 디자이너/디버깅 편의
        }

        public ChatMain_V2()
        {
            InitializeComponent();

            // 폼 전체 드래그 가능하도록 이벤트 추가
            // 원래 가능한데 Border없애서 따로 설정(그냥 디자인때문에..)
            this.MouseDown += ChatMain_V2_MouseDown;
            EnableDragOnControls(this);

            // 채팅 리스트 박스의 항목 높이를 자동 조절하도록 설정
            listBoxControlChat.ItemAutoHeight = true;
            listBoxControlChat.Appearance.TextOptions.WordWrap = WordWrap.Wrap;

            // 채팅 아이템의 크기와 그리기를 제어하는 이벤트 핸들러 등록
            listBoxControlChat.MeasureItem += ListBoxControlChat_MeasureItem;
            listBoxControlChat.DrawItem += ListBoxControlChat_DrawItem;

            listBoxControlChat.MouseClick += listBoxControlChat_MouseClick;

            // 첫 인사
            var sb = new StringBuilder();
            sb.AppendLine("안녕하세요. FAQ봇입니다. 😊\n");
            sb.AppendLine("자주하는 질문은 위 버튼을 눌러 확인해 주세요.");
            sb.AppendLine("원하는 내용이 없으면 그것참 안됐네요.\n");
            sb.AppendLine("예시 질문:");
            sb.AppendLine(" - 아이디 생성 또는 변경 방법");
            sb.AppendLine(" - 프로그램 사용법\n");

            AddBot(sb.ToString());

            // 입력창 이벤트
            memoEditInput.KeyDown += MemoEditInput_KeyDown; // 메세지 보내기
            btnSend.Click += (s, e) => Send(); // 버튼 클릭 시 전송
        }

        #region 자주묻는질문
        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            _awaitingStockQuery = false;
            _lastdt = null;

            switch (btn.Name)
            {
                case "BtnProc":
                    Search_Process();  //생산현황조회
                    break;

                case "BtnCmd":
                    Search_Cmd(); //지시현황조회
                    break;

                case "BtnStock":
                    Search_Stock(); //재고현황조회
                    break;
            }
        }
        #region 생산현황조회 & 상세내역 링크연결
        //생산현황조회
        private void Search_Process()
        {
            DataTable dtProcess = cb.Select_PM_OrderResult(DateTime.Today.ToString("yyMMdd"));

            if (dtProcess == null || dtProcess.Rows.Count == 0)
            {
                AddBot("오늘 등록된 실적이 없습니다.");
                return;
            }

            _lastdt = dtProcess;

            string date = DateTime.Today.ToString("yyyy-MM-dd");
            int Count = dtProcess.Rows.Count;

            var statusGroups = dtProcess.AsEnumerable()
                            .GroupBy(r => r.Field<string>("Status"))
                            .ToDictionary(g => g.Key, g => g.Count());

            int workingCnt = statusGroups.ContainsKey("작업중") ? statusGroups["작업중"] : 0;
            int waitingCnt = statusGroups.ContainsKey("대기") ? statusGroups["대기"] : 0;
            int endCnt = statusGroups.ContainsKey("종료") ? statusGroups["종료"] : 0;

            var sb = new StringBuilder();
            sb.AppendLine($"[ {date} 생산 현황 ]\n");
            sb.AppendLine($"  작업중 : {workingCnt} 건");
            sb.AppendLine($"  대기 : {waitingCnt} 건");
            sb.AppendLine($"  종료 : {endCnt} 건");

            AddBot(sb.ToString());
            AddBot("생산현황 상세내역보기", isLink: true);
        }

        //상세내역 링크 설정
        private void AddBot(string message, bool isLink = false)
        {
            listBoxControlChat.Items.Add(new ChatItem { IsUser = false, Text = message, IsLink = isLink });
            listBoxControlChat.TopIndex = listBoxControlChat.ItemCount - 1;
        }

        //상세내역 조회
        private void ShowDetails()
        {
            if (_lastdt == null) return;

            var sb = new StringBuilder();
            sb.AppendLine("[상세 내역]");
            foreach (DataRow row in _lastdt.Rows)
            {
                string material = row["ItemName"]?.ToString();
                string status = row["Status"]?.ToString();
                int gqty = row["GoodQty"].ConvertInt_Null_To_Zero();
                int nqty = row["NgQty"].ConvertInt_Null_To_Zero();
                sb.AppendLine($" - {material} / {status} / 양품 {gqty:N0} / 불량 {nqty:N0}");
            }
            AddBot(sb.ToString());
        }

        private void listBoxControlChat_MouseClick(object sender, MouseEventArgs e)
        {
            int idx = listBoxControlChat.IndexFromPoint(e.Location);
            if (idx < 0) return;

            if (listBoxControlChat.Items[idx] is ChatItem item && item.IsLink)
            {
                ShowDetails();
            }
        }
        #endregion

        #region 지시현황조회
        //지시현황조회
        private void Search_Cmd()
        {
            DataTable dtCmd = cb.Select_CmdStatus(DateTime.Today.ToString("yyMMdd"));

            if (dtCmd == null || dtCmd.Rows.Count == 0)
            {
                AddBot("오늘 등록된 지시가 없습니다.");
                return;
            }

            string date = DateTime.Today.ToString("yyyy-MM-dd");
            int cmdCount = dtCmd.Rows.Count;

            var sb = new StringBuilder();
            sb.AppendLine($"[ {date} 지시 현황 ]\n");
            sb.AppendLine($"  총 지시 건수 : {cmdCount} 건");

            foreach (DataRow row in dtCmd.Rows)
            {
                string material = row["ItemName"]?.ConvertString_Null_To_Empty();
                int qty = row["Qty"].ConvertInt_Null_To_Zero();

                sb.AppendLine($"   - {material} / {qty:N0} 개");
            }

            AddBot(sb.ToString());
        }
        #endregion

        #region 재고현황 조회 & 답변기반
        //재고현황 조회
        private void Search_Stock()
        {
            AddBot("재고조회할 품목 또는 품목코드를 정확하게 입력해주세요");
            _awaitingStockQuery = true;   // 다음 사용자 입력을 재고 질의로 처리
        }

        private void HandleStockQuery(string material)
        {
            // 취소/종료 키워드
            if (string.Equals(material, "취소", StringComparison.OrdinalIgnoreCase))
            {
                AddBot("재고 조회를 취소했습니다.");
                _awaitingStockQuery = false;
                return;
            }
            if (string.Equals(material, "종료", StringComparison.OrdinalIgnoreCase))
            {
                AddBot("재고 조회를 종료했습니다.");
                _awaitingStockQuery = false;
                return;
            }

            cb.Material = material;
            DataTable dtStock = cb.Select_StockListView();

            if (dtStock == null || dtStock.Rows.Count == 0)
            {
                AddBot("일치하는 품목이 없습니다. \n품목코드(예: ABC123) 또는 정확한 품명을 입력해 주세요.");
                // 계속 대기 상태 유지: 사용자가 다시 입력하면 재조회
                _awaitingStockQuery = true;
                return;
            }

            // 가장 간단하게: 첫 행만 답변 (다건 처리 필요시 추후 확장)
            DataRow row = dtStock.Rows[0];

            string ItemName = row["ItemName"]?.ToString();
            int qty = row["StockQty"].ConvertInt_Null_To_Zero(); // 확장메서드 사용
            string unit = row["UnitCode"]?.ToString();

            AddBot($"[{ItemName}] 재고는 {qty:N0} {unit} 입니다.");
        }
        #endregion

        #endregion

        #region 답변관련
        // 사용자 입력 전송
        private void Send()
        {
            //_awaitingStockQuery = false;
            _lastdt = null;

            var text = memoEditInput.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            AddUser(text);
            memoEditInput.Text = string.Empty;

            // 재고 질의 대기 상태면 여기서 처리하고 종료
            if (_awaitingStockQuery)
            {
                HandleStockQuery(text);
                return;
            }

            // (기존) FAQ/기타 응답
            var answer = FindAnswer(text);
            AddBot(string.IsNullOrWhiteSpace(answer)
                ? "해당 정보를 찾을 수 없습니다." + Environment.NewLine +
                  "도움말을 입력하여 사용 가능한 명령어를 확인해보세요."
                : answer);
        }


        // 입력 텍스트에서 키워드 매칭 후 답변 찾기
        private string FindAnswer(string userText)
        {
            foreach (var kv in _faq)
            {
                if (userText.IndexOf(kv.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                    return kv.Value;
            }
            return null;
        }

        private void AddUser(string text)
        {
            // 사용자 말풍선 추가
            listBoxControlChat.Items.Add(new ChatItem { IsUser = true, Text = text });
            listBoxControlChat.TopIndex = listBoxControlChat.ItemCount - 1; // 스크롤 최신화
        }

        private void AddBot(string text)
        {
            // 챗봇 말풍선 추가
            listBoxControlChat.Items.Add(new ChatItem { IsUser = false, Text = text });
            listBoxControlChat.TopIndex = listBoxControlChat.ItemCount - 1; // 스크롤 최신화
        }
        #endregion

        #region 기능 외 기타 디자인 관련 이벤트

        private void MemoEditInput_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter만 누르면 전송
            if (e.KeyCode == Keys.Enter && !e.Shift && !e.Control && !e.Alt)
            {
                e.SuppressKeyPress = true; // Enter 입력이 줄바꿈되지 않도록 막음
                Send();
            }
            else if (e.KeyCode == Keys.Enter && e.Control)
            {
                // Ctrl+Enter → 줄바꿈 허용
            }
        }

        private void EnableDragOnControls(Control parent)
        {
            //폼 이동 이벤트 제외 설정
            foreach (Control c in parent.Controls)
            {
                if (c.Name == "btnClose") goto Recurse;

                // 입력/버튼류 제외
                if (c is DevExpress.XtraEditors.BaseEdit) goto Recurse;
                if (c is ButtonBase || c is SimpleButton) goto Recurse;

                // 링크류 제외
                if (c is LinkLabel || c is DevExpress.XtraEditors.HyperlinkLabelControl) goto Recurse;
                if (c is Label lbl && lbl.Cursor == Cursors.Hand) goto Recurse;

                // DevExpress ListBoxControl(=BaseListBoxControl)도 드래그 제외
                if (c is DevExpress.XtraEditors.BaseListBoxControl) goto Recurse;

                // 드래그 허용
                c.MouseDown += ChatMain_V2_MouseDown;

                Recurse:
                if (c.HasChildren) EnableDragOnControls(c);
            }
        }

        private void ChatMain_V2_MouseDown(object sender, MouseEventArgs e)
        {
            //폼의 Border를 없애서 폼 이동 이벤트 따로 설정, 버튼 이벤트는 EnableDragOnControls에서 따로 지정해줘야지 이벤트가 작동함
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 디자인

        // 둥근 사각형
        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            // 너무 작은 경우 보호
            if (d > bounds.Width) d = bounds.Width;
            if (d > bounds.Height) d = bounds.Height;

            var arc = new Rectangle(bounds.Location, new Size(d, d));

            // 좌상
            path.AddArc(arc, 180, 90);

            // 우상
            arc.X = bounds.Right - d;
            arc.Y = bounds.Top;
            path.AddArc(arc, 270, 90);

            // 우하
            arc.Y = bounds.Bottom - d;
            path.AddArc(arc, 0, 90);

            // 좌하
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        // 채팅 아이템 그리기 이벤트
        private void ListBoxControlChat_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            bool isUser = false;
            string text = "";
            bool isLink = false;

            var obj = listBoxControlChat.Items[e.Index];
            if (obj is ChatItem chat)
            {
                isUser = chat.IsUser;
                text = chat.Text ?? "";
                isLink = chat.IsLink;
            }
            else
            {
                text = obj?.ToString() ?? "";
                isUser = text.StartsWith("[You");
            }

            // 배경 그리기
            e.Appearance.DrawBackground(e.Cache, e.Bounds);

            int padding = 8;        // 텍스트 패딩
            int sideMargin = 80;    // 좌우 여백
            int maxTextWidth = e.Bounds.Width - sideMargin - (padding * 2);

            // 텍스트 크기 측정
            var textSize = e.Appearance.CalcTextSize(e.Cache, text, maxTextWidth).ToSize();

            int bubbleWidth = textSize.Width + padding * 2;
            int bubbleHeight = textSize.Height + padding * 2;

            // 사용자 메시지는 오른쪽, 챗봇은 왼쪽
            int x = isUser ? e.Bounds.Right - bubbleWidth - 10 : e.Bounds.Left + 10;

            // 말풍선 영역
            var bubbleRect = new Rectangle(x, e.Bounds.Top + 2, bubbleWidth, bubbleHeight);
            var textRect = new Rectangle(bubbleRect.Left + padding, bubbleRect.Top + padding,
                                           bubbleRect.Width - padding * 2, bubbleRect.Height - padding * 2);

            // 안티앨리어싱 적용 (둥근 모서리 가장자리 깔끔하게)
            var oldSmoothing = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // 둥근 사각형 배경 채우기 (테두리는 그리지 않음)
            using (var path = GetRoundedRect(bubbleRect, 12))
            using (var bgBrush = new SolidBrush(isUser ? Color.WhiteSmoke : Color.LightSteelBlue))
            {
                e.Graphics.FillPath(bgBrush, path);
            }

            // 텍스트 출력
            var flags = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.NoClipping;
            // 링크면 파란색 + 밑줄
            if (isLink)
            {
                using (var linkFont = new Font(e.Appearance.Font, FontStyle.Underline))
                {
                    TextRenderer.DrawText(e.Graphics, text, linkFont, textRect, Color.Blue, flags);
                }
            }
            else
            {
                TextRenderer.DrawText(e.Graphics, text, e.Appearance.Font, textRect, Color.Black, flags);
            }

            e.Graphics.SmoothingMode = oldSmoothing;
            e.Handled = true;
        }

        // 아이템 높이 측정 이벤트
        private void ListBoxControlChat_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0) return;

            var item = listBoxControlChat.Items[e.Index];
            string text = (item as ChatItem)?.Text ?? item?.ToString() ?? "";

            int padding = 8;
            int sideMargin = 80;
            int maxTextWidth = Math.Max(50, e.ItemWidth - sideMargin - (padding * 2));

            // 줄바꿈 고려하여 텍스트 크기 측정
            var proposed = new Size(maxTextWidth, int.MaxValue);
            var textSize = TextRenderer.MeasureText(
                text,
                listBoxControlChat.Appearance.Font,
                proposed,
                TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.NoClipping
            );

            // 항목 높이 = 텍스트 높이 + 패딩 + 여유공간
            e.ItemHeight = textSize.Height + padding * 2 + 4;
        }

        #endregion
    }
}
