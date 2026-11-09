using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartMES.View.Dashboard
{
    /// <summary>품질현황 패널 (양품율 + EOR 수치 + 설비별 WORST TOP5 바)</summary>
    public sealed class QualityPanel : Panel
    {
        private double _qualityRate;
        private double _eorRate;
        private List<WorstEquipItem> _worstList;

        public QualityPanel(double qualityRate, double eorRate, List<WorstEquipItem> worstList)
        {
            _qualityRate = qualityRate;
            _eorRate     = eorRate;
            _worstList   = worstList ?? new List<WorstEquipItem>();
            BackColor    = Color.Transparent;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateData(double qualityRate, double eorRate, List<WorstEquipItem> worstList)
        {
            _qualityRate = qualityRate;
            _eorRate     = eorRate;
            _worstList   = worstList ?? new List<WorstEquipItem>();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = Width, h = Height;

            // ── 상단: 양품율 / EOR 두 박스 ──────────────────
            int boxW = (w - 12) / 2;
            int boxH = 52;

            DrawMetricBox(g, new Rectangle(0, 0, boxW, boxH),
                "양품율 (당월)", _qualityRate.ToString("0.00") + "%", CT.BlueStrong);

            DrawMetricBox(g, new Rectangle(boxW + 8, 0, boxW, boxH),
                "EOR (불량출하율)", _eorRate.ToString("0.00") + "%", CT.RedText);

            // ── 구분선 ───────────────────────────────────────
            int lineY = boxH + 6;
            using (var pen = new Pen(CT.BorderLight))
                g.DrawLine(pen, 0, lineY, w, lineY);

            // ── WORST TOP5 ───────────────────────────────────
            using (var f  = new Font(CT.FN, 7.5f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.TextMuted))
                g.DrawString("설비별 불량 WORST TOP 5 (당월)", f, br, 0, lineY + 5);

            int padL   = 78, padR = 36;
            int barAreaW = w - padL - padR;
            int rowH   = 22;
            int startY = lineY + 20;

            // 색상: 상위 두 개 진한 빨강 → 점점 연해짐
            Color[] barClrs =
            {
                CT.RedBar1, CT.RedBar1, CT.RedBar2, CT.RedBar3, CT.RedBar3
            };

            for (int i = 0; i < _worstList.Count && i < 5; i++)
            {
                var item = _worstList[i];
                int y    = startY + i * (rowH + 2);

                // 장비명
                using (var f  = new Font(CT.FN, 8.5f, FontStyle.Bold))
                using (var br = new SolidBrush(CT.TextSecond))
                    g.DrawString(item.EquipName, f, br, 0, y);

                using (var f  = new Font(CT.FN, 7.5f))
                using (var br = new SolidBrush(CT.TextMuted))
                    g.DrawString(item.EquipType, f, br, 0, y + 11);

                // 바 (100% 중 BadRate%)
                float maxRate = 10f; // 10% 기준 표시
                float pct     = (float)(item.BadRate / maxRate * 100);
                var track     = new Rectangle(padL, y + 3, barAreaW, 14);
                Color clr     = i < barClrs.Length ? barClrs[i] : CT.RedBar3;
                CDraw.DrawHBar(g, track, pct, clr, item.BadRate.ToString("0.0") + "%");

                // 우측 건수
                using (var f  = new Font(CT.FN, 9f, FontStyle.Bold))
                using (var br = new SolidBrush(CT.RedText))
                using (var sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                    g.DrawString(item.BadCount.ToString("N0"), f, br,
                        new Rectangle(padL + barAreaW + 4, y + 3, padR - 4, 14), sf);

                // 행 구분선
                if (i < _worstList.Count - 1)
                    using (var pen = new Pen(CT.BorderLight))
                        g.DrawLine(pen, 0, y + rowH + 1, w, y + rowH + 1);
            }
        }

        private void DrawMetricBox(Graphics g, Rectangle r, string label,
                                     string value, Color valueColor)
        {
            using (var br = new SolidBrush(CT.SurfaceBg))
            using (var path = CDraw.RoundRect(r, 8))
                g.FillPath(br, path);
            using (var pen = new Pen(CT.Border, 0.8f))
            using (var path = CDraw.RoundRect(r, 8))
                g.DrawPath(pen, path);

            using (var f  = new Font(CT.FN, 7.5f))
            using (var br = new SolidBrush(CT.TextMuted))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString(label, f, br,
                    new Rectangle(r.X, r.Y + 6, r.Width, 14), sf);

            using (var f  = new Font(CT.FN, 18f, FontStyle.Bold))
            using (var br = new SolidBrush(valueColor))
            using (var sf = new StringFormat
            {
                Alignment     = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
                g.DrawString(value, f, br,
                    new Rectangle(r.X, r.Y + 18, r.Width, r.Height - 18), sf);
        }
    }
}
