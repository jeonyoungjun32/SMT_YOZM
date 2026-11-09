using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartDesign.View.Dashboard
{
    // ═════════════════════════════════════════════════════════
    // 바 차트 (전월실적)
    // ═════════════════════════════════════════════════════════
    public sealed class BarChartPanel : Panel
    {
        private string[] _labels;
        private int[]    _target;
        private int[]    _actual;
        private Color    _targetColor;
        private Color    _actualColor;

        private const int PL = 36, PR = 6, PT = 14, PB = 18;

        public BarChartPanel(string[] labels, int[] target, int[] actual, Color targetColor, Color actualColor)
        {
            _labels = labels; _target = target; _actual = actual;
            _targetColor = targetColor; _actualColor = actualColor;
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateData(int[] target, int[] actual)
        {
            // 차트 데이터 null 처리
            int len = _labels != null ? _labels.Length : 5;

            _target = target ?? new int[len];
            _actual = actual ?? new int[len];

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_target == null || _actual == null || _labels == null || _labels.Length == 0)
            {
                return;
            }

            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = Width, h = Height;
            int cW = w - PL - PR, cH = h - PT - PB;
            if (cW < 10 || cH < 10) return;

            int yMax = DrawHelper.CalcYMax(_target, _actual, 300);
            DrawHelper.DrawYAxis(g, PL, PT, cH, cW, yMax, 4);

            int   n  = _labels.Length;
            float gW = (float)cW / n;
            float bW = gW * 0.28f;
            float gp = gW * 0.04f;

            for (int i = 0; i < n; i++)
            {
                float x0 = PL + i * gW + gW * 0.14f;

                float tH = _target[i] > 0 ? (float)_target[i] / yMax * cH : 0f;
                using (var br = new SolidBrush(_targetColor))
                    g.FillRectangle(br, x0, PT + cH - tH, bW, tH);

                float aH = _actual[i] > 0 ? (float)_actual[i] / yMax * cH : 0f;
                using (var br = new SolidBrush(_actualColor))
                    g.FillRectangle(br, x0 + bW + gp, PT + cH - aH, bW, aH);

                using (var fL = new Font(DT.FN, 7.5f))
                using (var sb = new SolidBrush(DT.TextMuted))
                using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                    g.DrawString(_labels[i], fL, sb,
                        new RectangleF(PL + i * gW, PT + cH + 2, gW, PB - 2), sf);
            }

            using (var pen = new Pen(DT.Border, 0.8f))
                g.DrawLine(pen, PL, PT + cH, PL + cW, PT + cH);
        }
    }

    // ═════════════════════════════════════════════════════════
    // 라인 차트 (주간)
    // ═════════════════════════════════════════════════════════
    public sealed class LineChartPanel : Panel
    {
        private string[] _labels;
        private int[]    _target;
        private int[]    _actual;
        private Color    _lineColor;
        private Color    _fillColor;

        private const int PL = 36, PR = 6, PT = 12, PB = 18;

        public LineChartPanel(string[] labels, int[] target, int[] actual, Color lineColor, Color fillColor)
        {
            _labels    = labels;  _target    = target;  _actual    = actual;
            _lineColor = lineColor; _fillColor = fillColor;
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateData(int[] target, int[] actual)
        {
            _target = target; _actual = actual; Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_target == null || _actual == null || _labels == null || _labels.Length == 0)
            {
                return;
            }

            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = Width, h = Height;
            int cW = w - PL - PR, cH = h - PT - PB;
            if (cW < 10 || cH < 10 || _labels.Length < 2) return;

            int n    = _labels.Length;
            int yMax = DrawHelper.CalcYMax(_target, _actual, 300);
            DrawHelper.DrawYAxis(g, PL, PT, cH, cW, yMax, 4);

            float step = (float)cW / (n - 1);

            // 목표 점선
            var tPts = new PointF[n];
            for (int i = 0; i < n; i++)
                tPts[i] = new PointF(PL + i * step,
                    PT + cH - (float)_target[i] / yMax * cH);
            using (var pen = new Pen(Color.FromArgb(180, 180, 180), 1.3f)
            { DashStyle = DashStyle.Dash })
                g.DrawLines(pen, tPts);

            // 실적 fill
            var aPts = new PointF[n];
            for (int i = 0; i < n; i++)
                aPts[i] = new PointF(PL + i * step,
                    PT + cH - (float)_actual[i] / yMax * cH);

            var fillPts = new PointF[n + 2];
            for (int i = 0; i < n; i++) fillPts[i] = aPts[i];
            fillPts[n]     = new PointF(aPts[n - 1].X, PT + cH);
            fillPts[n + 1] = new PointF(aPts[0].X,     PT + cH);
            using (var br = new SolidBrush(_fillColor))
                g.FillPolygon(br, fillPts);

            // 실적 라인
            using (var pen = new Pen(_lineColor, 2f))
                g.DrawLines(pen, aPts);

            // 포인트
            foreach (var pt in aPts)
            {
                using (var br = new SolidBrush(_lineColor))
                    g.FillEllipse(br, pt.X - 3.5f, pt.Y - 3.5f, 7f, 7f);
                using (var br = new SolidBrush(Color.White))
                    g.FillEllipse(br, pt.X - 2f, pt.Y - 2f, 4f, 4f);
            }

            using (var pen = new Pen(DT.Border, 0.8f))
                g.DrawLine(pen, PL, PT + cH, PL + cW, PT + cH);

            DrawHelper.DrawXLabels(g, _labels, PL, PT, cH, cW, PB);
        }
    }

    // ═════════════════════════════════════════════════════════
    // OEE 항목 카드
    // ═════════════════════════════════════════════════════════
    public sealed class OeeItemPanel : Panel
    {
        private string _title;
        private List<KeyValuePair<string, double>> _items = new List<KeyValuePair<string, double>>();

        public OeeItemPanel(string title)
        {
            _title = title;
            BackColor = DT.CardBg;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateValues(List<KeyValuePair<string, double>> items)
        {
            _items = items;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = Width, h = Height;
            DrawHelper.FillCard(g, new Rectangle(0, 0, w - 1, h - 1), DT.R);

            int tH = 28;
            using (var f  = new Font(DT.FN, 8.5f, FontStyle.Bold))
            using (var br = new SolidBrush(DT.TextMuted))
            using (var sf = new StringFormat
            {
                Alignment     = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
                g.DrawString(_title, f, br, new Rectangle(0, 0, w, tH), sf);

            using (var pen = new Pen(DT.Border, 0.8f))
                g.DrawLine(pen, 6, tH, w - 7, tH);

            int bodyH = h - tH;
            int n = _items.Count;

            if (n == 0) return;

            float partW = (float)w / n;

            var colors = new[]
            {
                new { Bg = DT.OeeBlueBarBg,  Fg = DT.OeeBlueBarFg,  ValClr = DT.BlueStrong },
                new { Bg = DT.OeeGreenBarBg, Fg = DT.OeeGreenBarFg, ValClr = DT.GreenStrong },
               new { Bg = DT.OeeOrangeBarBg, Fg = DT.OeeOrangeBarFg, ValClr = DT.OrangeStrong }
            };

            for (int i = 0; i < n; i++)
            {
                var item = _items[i];
                var c = colors[i % colors.Length];

                int startX = (int)(i * partW);
                int currentW = (int)partW;

                if (i == n - 1) currentW = w - startX;

                // N등분 된 구역에 데이터 그리기
                DrawHalf(g, startX, tH, currentW, bodyH,
                         item.Key, item.Value, c.Bg, c.Fg, c.ValClr);

                // 칸을 나누는 세로선 그리기
                if (i < n - 1)
                {
                    using (var pen = new Pen(DT.Border, 0.8f))
                        g.DrawLine(pen, startX + currentW, tH + 4, startX + currentW, h - 4);
                }
            }
        }

        private void DrawHalf(Graphics g, int x, int y, int w, int h,
                               string lbl, double val,
                               Color barBg, Color barFg, Color valClr)
        {
            int bH = 19;
            using (var br = new SolidBrush(barBg))
                g.FillRectangle(br, x + 2, y + 2, w - 4, bH);
            using (var f  = new Font(DT.FN, 8f, FontStyle.Bold))
            using (var br = new SolidBrush(barFg))
            using (var sf = new StringFormat
            {
                Alignment     = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
                g.DrawString(lbl, f, br, new Rectangle(x + 2, y + 2, w - 4, bH), sf);

            using (var f  = new Font(DT.FN, 15f, FontStyle.Bold))
            using (var br = new SolidBrush(valClr))
            using (var sf = new StringFormat
            {
                Alignment     = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
                g.DrawString(val.ToString("0.##") + "%", f, br,
                    new Rectangle(x, y + bH + 2, w, h - bH - 4), sf);
        }
    }

    // ═════════════════════════════════════════════════════════
    // 카드 헤더 패널
    // ═════════════════════════════════════════════════════════
    internal sealed class CardHeaderPanel : Panel
    {
        private readonly string _title;
        private readonly Color  _accent;

        public CardHeaderPanel(string title, Color accent)
        {
            _title = title; _accent = accent;
            BackColor = DT.SurfaceBg;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using (var pen = new Pen(DT.Border, 0.8f))
                g.DrawLine(pen, 0, Height - 1, Width, Height - 1);

            using (var br = new SolidBrush(_accent))
                g.FillRectangle(br, 0, 4, 3, Height - 8);

            using (var f  = new Font(DT.FN, 11f, FontStyle.Bold))
            using (var br = new SolidBrush(DT.TextMuted))
            using (var sf = new StringFormat { LineAlignment = StringAlignment.Center })
                g.DrawString(_title, f, br,
                    new Rectangle(10, 0, Width - 12, Height), sf);
        }
    }

    // ═════════════════════════════════════════════════════════
    // 범례 패널
    // ═════════════════════════════════════════════════════════
    internal sealed class LegendPanel : Panel
    {
        private readonly string[] _texts;
        private readonly Color[]  _colors;
        private readonly bool     _firstDashed;

        public LegendPanel(string[] texts, Color[] colors, bool firstDashed)
        {
            _texts = texts; _colors = colors; _firstDashed = firstDashed;
            BackColor = Color.Transparent;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            using (var fL = new Font(DT.FN, 8f))
            {
                int totalW = 0;
                for (int i = 0; i < _texts.Length; i++)
                {
                    SizeF sz = g.MeasureString(_texts[i], fL);
                    totalW += 12 + (int)sz.Width + (i < _texts.Length - 1 ? 10 : 0);
                }

                int cx = Width - totalW - 4;
                int cy = (Height - 10) / 2;

                using (var sb = new SolidBrush(DT.TextMuted))
                {
                    for (int i = 0; i < _texts.Length; i++)
                    {
                        if (_firstDashed && i == 0)
                        {
                            using (var pen = new Pen(
                                Color.FromArgb(180, 180, 180), 1.2f)
                            { DashStyle = DashStyle.Dash })
                                g.DrawLine(pen, cx, cy + 4, cx + 10, cy + 4);
                        }
                        else
                        {
                            using (var br = new SolidBrush(_colors[i]))
                                g.FillRectangle(br, cx, cy, 9, 7);
                        }
                        SizeF sz = g.MeasureString(_texts[i], fL);
                        g.DrawString(_texts[i], fL, sb, cx + 12, cy - 1);
                        cx += 12 + (int)sz.Width + 10;
                    }
                }
            }
        }
    }
}
