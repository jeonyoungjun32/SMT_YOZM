using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartMES.View.Dashboard
{
    /// <summary>품목그룹별 생산현황 패널 (수평 누적 바 + 합계)</summary>
    public sealed class ProductionPanel : Panel
    {
        private List<ProductionGroupItem> _groups;
        private int    _total;
        private double _weeklyRate;

        // 청도 스펙트럼 (진한→연한)
        private static readonly Color[] BarColors =
        {
            Color.FromArgb(24,  95, 165),
            Color.FromArgb(55, 138, 221),
            Color.FromArgb(133, 183, 235),
            Color.FromArgb(181, 212, 244),
            Color.FromArgb(221, 227, 237)
        };

        public ProductionPanel(List<ProductionGroupItem> groups, int total, double weeklyRate)
        {
            _groups     = groups;
            _total      = total;
            _weeklyRate = weeklyRate;
            BackColor   = Color.Transparent;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateData(List<ProductionGroupItem> groups, int total, double weeklyRate)
        {
            _groups = groups; _total = total; _weeklyRate = weeklyRate;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (_groups == null || _groups.Count == 0) return;

            int w = Width, h = Height;
            int padL = 46, padR = 60, padTop = 6, rowH = 22;
            int barW = w - padL - padR;

            for (int i = 0; i < _groups.Count && i < 5; i++)
            {
                var item = _groups[i];
                int y = padTop + i * (rowH + 4);

                // 레이블
                using (var f  = new Font(CT.FN, 9f))
                using (var br = new SolidBrush(CT.TextSecond))
                using (var sf = new StringFormat
                {
                    Alignment     = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center
                })
                    g.DrawString(item.Name, f, br,
                        new Rectangle(0, y, padL - 4, rowH), sf);

                // 트랙 + 바
                var track = new Rectangle(padL, y, barW, rowH);
                Color barClr = i < BarColors.Length ? BarColors[i] : CT.Border;
                string lbl = item.Rate + "% (" + item.Count.ToString("N0") + ")";
                CDraw.DrawHBar(g, track, item.Rate, barClr, lbl);

                // 우측 카운트
                using (var f  = new Font(CT.FN, 8.5f))
                using (var br = new SolidBrush(CT.TextMuted))
                using (var sf = new StringFormat { LineAlignment = StringAlignment.Center })
                    g.DrawString(item.Count.ToString("N0"), f, br,
                        new Rectangle(padL + barW + 4, y, padR - 4, rowH), sf);
            }

            // 하단 구분선 + 합계
            int lineY = padTop + 5 * (rowH + 4) + 4;
            using (var pen = new Pen(CT.BorderLight))
                g.DrawLine(pen, 0, lineY, w, lineY);

            int footerY = lineY + 4;
            // 합계
            using (var f  = new Font(CT.FN, 8.5f))
            using (var br = new SolidBrush(CT.TextMuted))
                g.DrawString("합계", f, br, 4, footerY + 2);

            using (var f  = new Font(CT.FN, 13f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.BlueStrong))
                g.DrawString(_total.ToString("N0"), f, br, 40, footerY);

            // 이번주 달성율
            string rateStr = _weeklyRate.ToString("0.0") + "%";
            using (var f  = new Font(CT.FN, 13f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.GreenStrong))
            using (var sf = new StringFormat { Alignment = StringAlignment.Far })
                g.DrawString(rateStr, f, br,
                    new Rectangle(0, footerY, w - 4, 22), sf);

            using (var f  = new Font(CT.FN, 7.5f))
            using (var br = new SolidBrush(CT.TextMuted))
            using (var sf = new StringFormat { Alignment = StringAlignment.Far })
                g.DrawString("이번주 달성율", f, br,
                    new Rectangle(0, footerY + 18, w - 4, 14), sf);
        }
    }
}
