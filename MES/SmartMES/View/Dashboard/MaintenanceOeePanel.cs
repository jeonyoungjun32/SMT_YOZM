using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartMES.View.Dashboard
{
    // ═════════════════════════════════════════════════════════
    // 보전지수 패널 (도넛 + 누적시간)
    // ═════════════════════════════════════════════════════════
    public sealed class MaintenancePanel : Panel
    {
        private double _index;
        private int    _hours;

        public MaintenancePanel(double index, int hours)
        {
            _index = index; _hours = hours;
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateData(double index, int hours)
        {
            _index = index; _hours = hours; Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = Width, h = Height;

            // 도넛 (보전지수 0 = 트랙만)
            int side  = Math.Min(w - 20, (h / 2) - 10);
            if (side < 20) side = 80;
            int cx    = w / 2;
            int cy    = h / 2 - 10;
            int r2    = side / 2;
            int thick = Math.Max(10, r2 / 4);
            var outerR = new Rectangle(cx - r2, cy - r2, r2 * 2, r2 * 2);

            // 트랙
            using (var pen = new Pen(Color.FromArgb(237, 240, 245), thick))
                g.DrawArc(pen, outerR, -90, 360);

            // 진행 (최대 100 기준 시각화)
            float sweep = (float)Math.Min(_index / 10.0 * 360, 360);
            if (sweep > 0.5f)
                using (var pen = new Pen(CT.BlueStrong, thick)
                { StartCap = LineCap.Round, EndCap = LineCap.Round })
                    g.DrawArc(pen, outerR, -90, sweep);

            // 중앙 지수값
            using (var f  = new Font(CT.FN, 18f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.TextPrimary))
            {
                string idx = _index.ToString("00.0");
                SizeF  sz  = g.MeasureString(idx, f);
                g.DrawString(idx, f, br, cx - sz.Width / 2, cy - sz.Height / 2);
            }

            // 누적 시간
            using (var f  = new Font(CT.FN, 20f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.TextPrimary))
            {
                string hrs = _hours.ToString("N0") + " H";
                SizeF  sz  = g.MeasureString(hrs, f);
                g.DrawString(hrs, f, br, cx - sz.Width / 2, cy + r2 + 10);
            }

            using (var f  = new Font(CT.FN, 8.5f))
            using (var br = new SolidBrush(CT.TextMuted))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("누적 가동시간", f, br,
                    new Rectangle(0, cy + r2 + 34, w, 14), sf);
        }
    }

    // ═════════════════════════════════════════════════════════
    // OEE 패널 (전체 % + 사출/조립 T·P·G)
    // ═════════════════════════════════════════════════════════
    public sealed class OeePanel : Panel
    {
        private double _total;
        private bool   _warning;
        private double _sTime, _sPerf, _sYield;
        private double _jTime, _jPerf, _jYield;

        public OeePanel(double total, bool warning,
                         double sTime, double sPerf, double sYield,
                         double jTime, double jPerf, double jYield)
        {
            _total = total; _warning = warning;
            _sTime = sTime; _sPerf  = sPerf;  _sYield = sYield;
            _jTime = jTime; _jPerf  = jPerf;  _jYield = jYield;
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public void UpdateData(double total, bool warning,
                                double sTime, double sPerf, double sYield,
                                double jTime, double jPerf, double jYield)
        {
            _total = total; _warning = warning;
            _sTime = sTime; _sPerf  = sPerf;  _sYield = sYield;
            _jTime = jTime; _jPerf  = jPerf;  _jYield = jYield;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = Width, h = Height;

            // 서브 타이틀
            using (var f  = new Font(CT.FN, 7.5f))
            using (var br = new SolidBrush(CT.TextMuted))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString("전체 설비종합효율", f, br,
                    new Rectangle(0, 6, w, 14), sf);

            // 경고 뱃지
            if (_warning)
            {
                string warnTxt = "⚠ 점검필요";
                using (var f   = new Font(CT.FN, 8f, FontStyle.Bold))
                using (var br  = new SolidBrush(CT.AmberText))
                {
                    SizeF sz = g.MeasureString(warnTxt, f);
                    int bx = w - (int)sz.Width - 16;
                    using (var bbr = new SolidBrush(CT.AmberBg))
                        g.FillRectangle(bbr, bx - 4, 4, (int)sz.Width + 8, 16);
                    g.DrawString(warnTxt, f, br, bx - 2, 5);
                }
            }

            // 전체 OEE 큰 숫자
            string oeeStr = _total.ToString("0.00") + "%";
            using (var f  = new Font(CT.FN, 26f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.BlueStrong))
            {
                //SizeF sz = g.MeasureString(oeeStr, f);
                //g.DrawString(oeeStr, f, br, cx: (w - sz.Width) / 2, cy: 22);
            }

            // 사출 / 조립 상세 행
            DrawOeeRow(g, "사출", _sTime, _sPerf, _sYield,
                CT.BluePillBg, CT.BluePillFg, 60);
            DrawOeeRow(g, "조립", _jTime, _jPerf, _jYield,
                CT.GreenPillBg, CT.GreenPillFg, 115);
        }

        private void DrawOeeRow(Graphics g, string label,
                                  double tVal, double pVal, double gVal,
                                  Color pillBg, Color pillFg, int y)
        {
            int w = Width;

            // Pill
            CDraw.DrawPill(g, label, 28, y, pillBg, pillFg, 40, 16);

            // T P G 값
            string[] keys = { "T", "P", "G" };
            double[] vals = { tVal, pVal, gVal };
            Color[]  clrs =
            {
                CT.BlueStrong,
                CT.GreenStrong,
                Color.FromArgb(133, 79, 11)
            };

            int kx = 75;
            for (int i = 0; i < 3; i++)
            {
                using (var fk = new Font(CT.FN, 8f))
                using (var bk = new SolidBrush(CT.TextMuted))
                    g.DrawString(keys[i], fk, bk, kx, y + 1);

                using (var fv = new Font(CT.FN, 8.5f, FontStyle.Bold))
                using (var bv = new SolidBrush(clrs[i]))
                    g.DrawString(vals[i].ToString("0.00") + "%", fv, bv, kx + 10, y + 1);

                kx += (w - 75) / 3;
            }
        }

        // float 오버로드 (OnPaint 내 인라인 사용용)
        private void DrawString(Graphics g, string text, Font f, SolidBrush br,
                                  float cx, float cy)
        {
            g.DrawString(text, f, br, cx, cy);
        }
    }
}
