using System.Drawing;

namespace SmartMES.View.Dashboard
{
    /// <summary>종합현황 전체 색상·폰트 상수. 디자인 변경 시 이 파일만 수정.</summary>
    internal static class CT
    {
        // ── 배경 ──────────────────────────────────────────────
        public static readonly Color PageBg    = Color.FromArgb(244, 246, 250);
        public static readonly Color CardBg    = Color.White;
        public static readonly Color SurfaceBg = Color.FromArgb(248, 250, 252);
        public static readonly Color GridLine  = Color.FromArgb(240, 244, 248);

        // ── 테두리 ────────────────────────────────────────────
        public static readonly Color Border      = Color.FromArgb(221, 227, 237);
        public static readonly Color BorderLight = Color.FromArgb(237, 240, 245);

        // ── 텍스트 ────────────────────────────────────────────
        public static readonly Color TextPrimary = Color.FromArgb(30,  45,  74);
        public static readonly Color TextSecond  = Color.FromArgb(74,  85, 104);
        public static readonly Color TextMuted   = Color.FromArgb(107, 120, 144);

        // ── Blue (사출 계열) ──────────────────────────────────
        public static readonly Color BlueStrong  = Color.FromArgb(24,  95, 165);
        public static readonly Color BluePillBg  = Color.FromArgb(230, 241, 251);
        public static readonly Color BluePillFg  = Color.FromArgb(12,   68, 124);

        // ── Green (조립 계열) ─────────────────────────────────
        public static readonly Color GreenStrong = Color.FromArgb(59, 109,  17);
        public static readonly Color GreenPillBg = Color.FromArgb(234, 243, 222);
        public static readonly Color GreenPillFg = Color.FromArgb(39,   80,  10);

        // ── Amber (경고) ──────────────────────────────────────
        public static readonly Color AmberBg    = Color.FromArgb(250, 238, 218);
        public static readonly Color AmberText  = Color.FromArgb(99,   56,   6);

        // ── Red (불량/위험) ───────────────────────────────────
        public static readonly Color RedText    = Color.FromArgb(197,  48,  48);
        public static readonly Color RedBar1    = Color.FromArgb(226,  75,  74);   // 최악
        public static readonly Color RedBar2    = Color.FromArgb(232, 127,  58);   // 중간
        public static readonly Color RedBar3    = Color.FromArgb(234, 180,  68);   // 양호

        // ── 납품 바 ───────────────────────────────────────────
        public static readonly Color DeliveryBar1 = Color.FromArgb(24,  95, 165);
        public static readonly Color DeliveryBar2 = Color.FromArgb(55, 138, 221);

        // ── 재고 컬럼 ─────────────────────────────────────────
        public static readonly Color Inv5Text   = Color.FromArgb(39,   80,  10);
        public static readonly Color Inv15Text  = Color.FromArgb(186, 117,  23);
        public static readonly Color Inv16Text  = Color.FromArgb(163,  45,  45);

        // ── 폰트 ──────────────────────────────────────────────
        public const string FN = "맑은 고딕";

        public static readonly Font TitleFont  = new Font(FN, 17f, FontStyle.Bold);
        public static readonly Font CardHdr    = new Font(FN,  8.5f, FontStyle.Bold);
        public static readonly Font ValLarge   = new Font(FN, 26f, FontStyle.Bold);
        public static readonly Font ValMid     = new Font(FN, 16f, FontStyle.Bold);
        public static readonly Font ValSmall   = new Font(FN, 11f, FontStyle.Bold);
        public static readonly Font LabelFont  = new Font(FN,  8f);
        public static readonly Font MicroFont  = new Font(FN,  7.5f);

        // ── 카드 반지름 ───────────────────────────────────────
        public const int R = 10;
    }
}
