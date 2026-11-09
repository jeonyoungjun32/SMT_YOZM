using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SmartMES.View.Dashboard
{
    internal static class CDraw
    {
        // ── 둥근 사각형 경로 ──────────────────────────────────
        public static GraphicsPath RoundRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            var p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        // ── 카드 배경 ─────────────────────────────────────────
        public static void FillCard(Graphics g, Rectangle r, int radius)
        {
            using (var path = RoundRect(r, radius))
            using (var br = new SolidBrush(CT.CardBg))
            using (var pen = new Pen(CT.Border, 0.8f))
            {
                g.FillPath(br, path);
                g.DrawPath(pen, path);
            }
        }

        // ── 중앙 정렬 텍스트 ──────────────────────────────────
        public static void DrawCenter(Graphics g, string text, Font font,
                                       Color color, Rectangle r)
        {
            using (var br = new SolidBrush(color))
            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
                g.DrawString(text, font, br, r, sf);
        }

        // ── Pill 라벨 ─────────────────────────────────────────
        public static void DrawPill(Graphics g, string text, int cx, int y,
                                     Color bg, Color fg, int w = 46, int h = 16)
        {
            int px = cx - w / 2;
            using (var path = RoundRect(new Rectangle(px, y, w, h), 8))
            using (var br = new SolidBrush(bg))
                g.FillPath(br, path);
            using (var f = new Font(CT.FN, 8f, FontStyle.Bold))
            using (var br = new SolidBrush(fg))
            using (var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
                g.DrawString(text, f, br, new Rectangle(px, y, w, h), sf);
        }

        // ── 수평 바 (생산/납품/WORST 등) ──────────────────────
        public static void DrawHBar(Graphics g, Rectangle trackRect,
                                     float pct, Color fillColor,
                                     string label = "", Color? labelColor = null)
        {
            // 트랙
            using (var br = new SolidBrush(CT.BorderLight))
                g.FillRectangle(br, trackRect);

            // 채움
            int fillW = (int)(trackRect.Width * Math.Min(pct / 100f, 1f));
            if (fillW > 0)
            {
                var fillRect = new Rectangle(trackRect.X, trackRect.Y,
                                              fillW, trackRect.Height);
                using (var br = new SolidBrush(fillColor))
                    g.FillRectangle(br, fillRect);
            }

            // 라벨 (바 안)
            if (!string.IsNullOrEmpty(label))
            {
                using (var f = new Font(CT.FN, 7.5f, FontStyle.Bold))
                using (var br = new SolidBrush(labelColor ?? Color.White))
                using (var sf = new StringFormat { LineAlignment = StringAlignment.Center })
                    g.DrawString(label, f, br,
                        new Rectangle(trackRect.X + 6, trackRect.Y,
                                       Math.Max(fillW - 6, 0), trackRect.Height), sf);
            }
        }

        // ── 섹션 미니 타이틀 ──────────────────────────────────
        public static void DrawMiniTitle(Graphics g, string text, Rectangle r)
        {
            using (var f = new Font(CT.FN, 7.5f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.TextMuted))
            using (var sf = new StringFormat { LineAlignment = StringAlignment.Center })
                g.DrawString(text, f, br, r, sf);
        }
    }
}
