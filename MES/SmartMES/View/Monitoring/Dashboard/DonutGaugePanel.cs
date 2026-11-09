using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartDesign.View.Dashboard
{
    /// <summary>도넛 게이지 패널 (달성율 / 품질율 겸용)</summary>
    public sealed class DonutGaugePanel : Panel
    {
        private string _label;
        private double _actual;
        private double _target;
        private Color  _ringColor;
        private Color  _pillBg;
        private Color  _pillFg;
        private string _subLabel;
        private bool   _compact;

        public DonutGaugePanel(
            string label, double actual, double target,
            Color ringColor, Color pillBg, Color pillFg,
            string subLabel = "", bool compact = false)
        {
            _label     = label;   _actual    = actual;    _target    = target;
            _ringColor = ringColor; _pillBg  = pillBg;    _pillFg    = pillFg;
            _subLabel  = subLabel;  _compact = compact;

            BackColor      = DT.SurfaceBg;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateValue(double actual, double target)
        {
            _actual = actual;
            _target = target;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = Width, h = Height;
            DrawHelper.FillCard(g, new Rectangle(0, 0, w - 1, h - 1), DT.R - 2);

            // Pill
            const int pillH = 16, pillY = 7;
            DrawHelper.DrawPill(g, _label, w / 2, pillY, _pillBg, _pillFg);

            // 도넛
            double ratio = _target > 0 ? Math.Min((double)_actual / _target, 1.0) : 0;
            double pct   = ratio * 100.0;
            float  sweep = (float)(ratio * 360.0);

            int topOff = pillY + pillH + 20;
            int btmOff = _compact ? 18 : 40;
            int drawH  = h - topOff - btmOff;
            int side   = Math.Min(w - 20, drawH);
            if (side < 20) return;

            int cx = w / 2, cy = topOff + drawH / 2;
            int thick = Math.Max(9, (side / 2) / 5);
            int r2 = (side - thick - 10) / 2;
            var outerR = new Rectangle(cx - r2, cy - r2, r2 * 2, r2 * 2);

            using (var pen = new Pen(DT.DonutTrack, thick))
                g.DrawArc(pen, outerR, -90, 360);

            if (sweep > 0.5f)
                using (var pen = new Pen(_ringColor, thick)
                { StartCap = LineCap.Round, EndCap = LineCap.Round })
                    g.DrawArc(pen, outerR, -90, sweep);

            // 퍼센트 텍스트
            float pFontSz = _compact ? 13f : 17f;
            using (var fP = new Font(DT.FN, pFontSz, FontStyle.Bold))
            using (var br = new SolidBrush(_ringColor))
            {
                string ps = pct.ToString("0.#") + "%";
                SizeF  sz = g.MeasureString(ps, fP);
                g.DrawString(ps, fP, br,
                    cx - sz.Width / 2,
                    cy - sz.Height / 2 - (pFontSz > 13 ? 5 : 3));
            }

            // 서브텍스트
            using (var fS = new Font(DT.FN, 7.5f))
            using (var br = new SolidBrush(DT.TextMuted))
            {
                string sub = _compact
                    ? (_subLabel.Length > 0 ? _subLabel : "")
                    : "(" + _actual.ToString("N0") + " / " + _target.ToString("N0") + ")";
                SizeF sz = g.MeasureString(sub, fS);
                g.DrawString(sub, fS, br,
                    cx - sz.Width / 2,
                    cy + (pFontSz > 13 ? 9 : 4));
            }

            // 하단 목표/실적 테이블 (compact 아닐 때)
            if (!_compact)
            {
                int tY   = h - btmOff;
                int colW = w / 2;
                using (var pen = new Pen(DT.Border, 0.8f))
                {
                    g.DrawLine(pen, 6, tY, w - 7, tY);
                    g.DrawLine(pen, colW, tY + 4, colW, h - 4);
                }
                string[] hdrs = { "목표", "실적" };
                string[] vals = { _target.ToString("N0"), _actual.ToString("N0") };
                Color[]  clrs = { DT.TextPrimary, _ringColor };
                using (var fH = new Font(DT.FN, 8f))
                using (var fV = new Font(DT.FN, 10f, FontStyle.Bold))
                using (var sbH = new SolidBrush(DT.TextMuted))
                using (var sf  = new StringFormat
                {
                    Alignment     = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    for (int i = 0; i < 2; i++)
                    {
                        var rH = new Rectangle(i * colW, tY + 7, colW, 13);
                        var rV = new Rectangle(i * colW, tY + 21, colW, btmOff - 23);
                        g.DrawString(hdrs[i], fH, sbH, rH, sf);
                        using (var sbV = new SolidBrush(clrs[i]))
                            g.DrawString(vals[i], fV, sbV, rV, sf);
                    }
                }
            }
        }
    }
}
