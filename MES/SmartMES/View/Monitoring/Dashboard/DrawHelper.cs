using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace SmartDesign.View.Dashboard
{
    /// <summary>커스텀 패널들이 공통으로 쓰는 GDI+ 드로잉 유틸리티</summary>
    internal static class DrawHelper
    {
        // ── 둥근 사각형 경로 (C# 7.3 호환) ──────────────────
        public static GraphicsPath RoundRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var p = new GraphicsPath();
            p.AddArc(r.X,         r.Y,          d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y,          d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d,   0, 90);
            p.AddArc(r.X,         r.Bottom - d, d, d,  90, 90);
            p.CloseFigure();
            return p;
        }

        // ── 카드 배경 ─────────────────────────────────────────
        public static void FillCard(Graphics g, Rectangle r, int radius)
        {
            using (var path = RoundRect(r, radius))
            using (var br   = new SolidBrush(DT.CardBg))
            using (var pen  = new Pen(DT.Border, 0.8f))
            {
                g.FillPath(br, path);
                g.DrawPath(pen, path);
            }
        }

        // ── 중앙 정렬 텍스트 ──────────────────────────────────
        public static void DrawCenter(Graphics g, string text, Font font, Color color, Rectangle r)
        {
            using (var br = new SolidBrush(color))
            using (var sf = new StringFormat
            {
                Alignment     = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
                g.DrawString(text, font, br, r, sf);
        }

        // ── Pill 라벨 ─────────────────────────────────────────
        public static void DrawPill(Graphics g, string text, int cx, int y, Color bg, Color fg)
        {
            using (var f = new Font(DT.FN, 8f, FontStyle.Bold))
            {
                int textWidth = (int)g.MeasureString(text, f).Width;

                int pw = Math.Max(46, textWidth + 16);
                int ph = 16;

                int px = cx - pw / 2;

                using (var path = RoundRect(new Rectangle(px, y, pw, ph), 8))
                using (var br = new SolidBrush(bg))
                    g.FillPath(br, path);

                using (var br = new SolidBrush(fg))
                using (var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                    g.DrawString(text, f, br, new Rectangle(px, y, pw, ph), sf);
            }
        }

        // ── Y축 눈금 + 그리드 ─────────────────────────────────
        public static void DrawYAxis(Graphics g, int x, int y, int cH, int cW, int yMax, int steps)
        {
            using (var fY  = new Font(DT.FN, 7f))
            using (var sbM = new SolidBrush(DT.TextMuted))
            using (var pen = new Pen(DT.GridLine))
            {
                for (int i = 0; i <= steps; i++)
                {
                    int   val = yMax * i / steps;
                    float yy  = y + cH - (float)val / yMax * cH;
                    if (i > 0) g.DrawLine(pen, x, yy, x + cW, yy);
                    string lbl = val >= 1000
                        ? (val / 1000.0).ToString("0.#") + "K"
                        : val.ToString();
                    SizeF sz = g.MeasureString(lbl, fY);
                    g.DrawString(lbl, fY, sbM, x - sz.Width - 2, yy - sz.Height / 2);
                }
            }
        }

        // ── X축 레이블 ────────────────────────────────────────
        public static void DrawXLabels(Graphics g, string[] labels,
                                        int pl, int pt, int cH, int cW, int pb)
        {
            float step = (float)cW / Math.Max(labels.Length - 1, 1);
            using (var fL = new Font(DT.FN, 7.5f))
            using (var sb = new SolidBrush(DT.TextMuted))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
            {
                for (int i = 0; i < labels.Length; i++)
                {
                    float lx = pl + i * step;
                    g.DrawString(labels[i], fL, sb,
                        new RectangleF(lx - 14, pt + cH + 2, 28, pb - 2), sf);
                }
            }
        }

        // ── 배열 최대값 ───────────────────────────────────────
        public static int ArrayMax(int[] arr)
        {
            int m = 0;
            foreach (int v in arr) if (v > m) m = v;
            return m;
        }

        // ── Y축 최대값 계산 ───────────────────────────────────
        public static int CalcYMax(int[] a, int[] b, int unit)
        {
            //int max = Math.Max(ArrayMax(a), ArrayMax(b));
            //if (max == 0) return unit;
            //return (int)(Math.Ceiling((double)max / unit) * unit);

            // 1. ArrayMax 대신 C# 기본 기능인 .Max()를 사용합니다.
            // 혹시라도 배열이 비어있을(null 또는 길이 0) 경우를 대비해 안전하게 0으로 처리해 줍니다.
            int maxA = (a != null && a.Length > 0) ? a.Max() : 0;
            int maxB = (b != null && b.Length > 0) ? b.Max() : 0;

            // 2. 두 배열의 최대값 중 진짜 최대값을 구합니다.
            int max = Math.Max(maxA, maxB);

            if (max == 0) return unit;

            return (int)(Math.Ceiling((double)max / unit) * unit);
        }
    }

    // 색상 정의
    public static class DT
    {
        // ── 배경 ──────────────────────────────────────────────
        public static readonly Color PageBg = Color.FromArgb(244, 246, 250);
        public static readonly Color CardBg = Color.White;
        public static readonly Color SurfaceBg = Color.FromArgb(248, 250, 252);
        public static readonly Color GridLine = Color.FromArgb(240, 244, 248);

        // ── 테두리 ────────────────────────────────────────────
        public static readonly Color Border = Color.FromArgb(221, 227, 237);
        public static readonly Color BorderLight = Color.FromArgb(237, 240, 245);

        // ── 텍스트 ────────────────────────────────────────────
        public static readonly Color TextPrimary = Color.FromArgb(30, 45, 74);
        public static readonly Color TextSecond = Color.FromArgb(74, 85, 104);
        public static readonly Color TextMuted = Color.FromArgb(107, 120, 144);

        // ── Blue ─────────────────────────────────────────────
        public static readonly Color BlueStrong = Color.FromArgb(24, 95, 165);
        public static readonly Color BlueLight = Color.FromArgb(181, 212, 244);
        public static readonly Color BluePillBg = Color.FromArgb(230, 241, 251);
        public static readonly Color BluePillFg = Color.FromArgb(12, 68, 124);

        // ── Green ────────────────────────────────────────────
        public static readonly Color GreenStrong = Color.FromArgb(59, 109, 17);
        public static readonly Color GreenLight = Color.FromArgb(192, 221, 151);
        public static readonly Color GreenPillBg = Color.FromArgb(234, 243, 222);
        public static readonly Color GreenPillFg = Color.FromArgb(39, 80, 10);

        // ── Orange ───────────────────────────────────────────
        public static readonly Color OrangeStrong = Color.FromArgb(255, 140, 0);
        public static readonly Color OrangeLight = Color.FromArgb(255, 205, 145);
        public static readonly Color OrangePillBg = Color.FromArgb(255, 243, 230);
        public static readonly Color OrangePillFg = Color.FromArgb(190, 85, 0);

        // ── 위험 ────────────────────────────────────────────
        public static readonly Color RedText = Color.FromArgb(197, 48, 48);

        // ── 도넛 트랙 ─────────────────────────────────────────
        public static readonly Color DonutTrack = Color.FromArgb(237, 240, 245);

        // ── OEE 라벨바 ────────────────────────────────────────
        public static readonly Color OeeBlueBarBg = Color.FromArgb(230, 241, 251);
        public static readonly Color OeeBlueBarFg = Color.FromArgb(12, 68, 124);
        public static readonly Color OeeGreenBarBg = Color.FromArgb(234, 243, 222);
        public static readonly Color OeeGreenBarFg = Color.FromArgb(39, 80, 10);
        public static readonly Color OeeOrangeBarBg = Color.FromArgb(255, 243, 230);
        public static readonly Color OeeOrangeBarFg = Color.FromArgb(190, 85, 0);

        // ── 폰트 ────────────────────────────────────────────
        public const string FN = "맑은 고딕";

        public static readonly Font TitleFont = new Font(FN, 17f, FontStyle.Bold);
        public static readonly Font CardTitle = new Font(FN, 8.5f, FontStyle.Bold);
        public static readonly Font ValLarge = new Font(FN, 17f, FontStyle.Bold);
        public static readonly Font ValMid = new Font(FN, 14f, FontStyle.Bold);
        public static readonly Font LabelFont = new Font(FN, 8f);
        public static readonly Font MicroFont = new Font(FN, 7.5f);
        public static readonly Font OeeVal = new Font(FN, 15f, FontStyle.Bold);

        // ── 카드 반지름 ────────────────────────────────────────
        public const int R = 10;
    }
}
