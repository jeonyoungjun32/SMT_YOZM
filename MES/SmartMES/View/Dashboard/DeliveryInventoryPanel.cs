using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SmartMES.View.Dashboard
{
    /// <summary>납품현황 + 재고현황 복합 패널</summary>
    public sealed class DeliveryInventoryPanel : Panel
    {
        private double _grRate, _tgRate;
        private string _lastTime;
        private int    _doCnt, _grCnt, _tgCnt, _tgOk;
        private List<InventoryItem> _invList;
        private double _stockRate;

        // 재고 DataGridView
        private DataGridView _dgv;

        public DeliveryInventoryPanel(
            double grRate, double tgRate, string lastTime,
            int doCnt, int grCnt, int tgCnt, int tgOk,
            List<InventoryItem> invList, double stockRate)
        {
            _grRate    = grRate;   _tgRate    = tgRate;
            _lastTime  = lastTime ?? "";
            _doCnt     = doCnt;   _grCnt     = grCnt;
            _tgCnt     = tgCnt;   _tgOk      = tgOk;
            _invList   = invList  ?? new List<InventoryItem>();
            _stockRate = stockRate;

            BackColor      = Color.Transparent;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);

            BuildGrid();
        }

        private void BuildGrid()
        {
            _dgv = new DataGridView
            {
                ReadOnly                  = true,
                AllowUserToAddRows        = false,
                AllowUserToDeleteRows     = false,
                AllowUserToResizeRows     = false,
                RowHeadersVisible         = false,
                BorderStyle               = BorderStyle.None,
                BackgroundColor           = CT.CardBg,
                GridColor                 = CT.BorderLight,
                Font                      = new Font(CT.FN, 8.5f),
                AutoSizeColumnsMode       = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode             = DataGridViewSelectionMode.FullRowSelect,
                CellBorderStyle           = DataGridViewCellBorderStyle.SingleHorizontal,
                EnableHeadersVisualStyles = false
            };

            _dgv.ColumnHeadersDefaultCellStyle.BackColor  = CT.SurfaceBg;
            _dgv.ColumnHeadersDefaultCellStyle.ForeColor  = CT.TextMuted;
            _dgv.ColumnHeadersDefaultCellStyle.Font       = new Font(CT.FN, 8f, FontStyle.Bold);
            _dgv.ColumnHeadersDefaultCellStyle.Alignment  =
                DataGridViewContentAlignment.MiddleCenter;
            _dgv.ColumnHeadersHeight = 22;
            _dgv.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            _dgv.DefaultCellStyle.Alignment          = DataGridViewContentAlignment.MiddleCenter;
            _dgv.DefaultCellStyle.ForeColor          = CT.TextSecond;
            _dgv.DefaultCellStyle.BackColor          = CT.CardBg;
            _dgv.DefaultCellStyle.SelectionBackColor = CT.SurfaceBg;
            _dgv.DefaultCellStyle.SelectionForeColor = CT.TextPrimary;
            _dgv.AlternatingRowsDefaultCellStyle.BackColor = CT.SurfaceBg;

            _dgv.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "품목", HeaderText = "품목", FillWeight = 70 });
            _dgv.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Day5", HeaderText = "5일↓", FillWeight = 85 });
            _dgv.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Day15", HeaderText = "15일↓", FillWeight = 85 });
            _dgv.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Day16", HeaderText = "16일+", FillWeight = 85 });

            _dgv.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == 1 && e.RowIndex >= 0)
                    e.CellStyle.ForeColor = CT.Inv5Text;
                else if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                    e.CellStyle.ForeColor = CT.Inv15Text;
                else if (e.ColumnIndex == 3 && e.RowIndex >= 0)
                    e.CellStyle.ForeColor = CT.Inv16Text;
            };

            Controls.Add(_dgv);
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            _dgv.Rows.Clear();
            foreach (var item in _invList)
            {
                string d16 = item.Day16Over > 0
                    ? item.Day16Over.ToString("N0") : "—";
                _dgv.Rows.Add(item.Name,
                    item.Day5.ToString("N0"),
                    item.Day15.ToString("N0"),
                    d16);
            }
        }

        public void UpdateData(
            double grRate, double tgRate, string lastTime,
            int doCnt, int grCnt, int tgCnt, int tgOk,
            List<InventoryItem> invList, double stockRate)
        {
            _grRate    = grRate;   _tgRate    = tgRate;
            _lastTime  = lastTime ?? "";
            _doCnt     = doCnt;   _grCnt     = grCnt;
            _tgCnt     = tgCnt;   _tgOk      = tgOk;
            _invList   = invList  ?? new List<InventoryItem>();
            _stockRate = stockRate;
            RefreshGrid();
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutControls();
        }

        private void LayoutControls()
        {
            if (_dgv == null) return;
            int w = Width, h = Height;

            // 납품 영역 높이: 상단 130px → 나머지 재고
            int splitY   = 130;
            int gridTop  = splitY + 28;  // 재고 타이틀 22 + 여백
            int gridH    = h - gridTop - 20; // 하단 재고율 바 20px

            _dgv.SetBounds(0, gridTop, w, Math.Max(gridH, 40));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode     = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = Width;

            // ── 납품 타이틀 ──────────────────────────────────
            CDraw.DrawMiniTitle(g, "납품 현황",
                new Rectangle(0, 0, 60, 16));

            // GR RATE 바
            DrawDeliveryBar(g, "입고율 (GR)",  _grRate, CT.DeliveryBar1, 18);
            DrawDeliveryBar(g, "목표율 (TG)",  _tgRate, CT.DeliveryBar2, 42);

            // 최종조회 시간
            using (var f  = new Font(CT.FN, 7f))
            using (var br = new SolidBrush(CT.TextMuted))
                g.DrawString("최종조회: " + _lastTime, f, br, 0, 66);

            // 건수 4개 박스
            DrawStatBox(g, "발주건수", _doCnt.ToString(),   0,          80, (w - 12) / 4);
            DrawStatBox(g, "입고건수", _grCnt.ToString(),   (w-12)/4+4, 80, (w - 12) / 4);
            DrawStatBox(g, "목표건수", _tgCnt.ToString(),   (w-12)/2+4, 80, (w - 12) / 4);
            DrawStatBox(g, "달성건수", _tgOk.ToString(),    3*(w-12)/4+8,80, (w-12)/4);

            // ── 재고 타이틀 ──────────────────────────────────
            int splitY = 130;
            using (var pen = new Pen(CT.BorderLight))
                g.DrawLine(pen, 0, splitY, w, splitY);

            CDraw.DrawMiniTitle(g, "재고 현황",
                new Rectangle(0, splitY + 4, 60, 16));

            // ── 재고율 바 (하단) ──────────────────────────────
            int h = Height;
            int barY = h - 16;
            using (var f  = new Font(CT.FN, 7.5f))
            using (var br = new SolidBrush(CT.TextMuted))
                g.DrawString("재고율", f, br, 0, barY);

            int barX  = 38, barW = w - 38 - 36;
            CDraw.DrawHBar(g,
                new Rectangle(barX, barY, barW, 12),
                (float)_stockRate, CT.BlueStrong);

            using (var f  = new Font(CT.FN, 8f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.BlueStrong))
            using (var sf = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                g.DrawString(_stockRate.ToString("0.0") + "%", f, br,
                    new Rectangle(barX + barW + 2, barY, 34, 12), sf);
        }

        private void DrawDeliveryBar(Graphics g, string label, double rate,
                                      Color fillColor, int y)
        {
            int w = Width;
            using (var f  = new Font(CT.FN, 8.5f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.TextPrimary))
                g.DrawString(label, f, br, 0, y);

            int bx = 74, bw = w - 74 - 4;
            CDraw.DrawHBar(g,
                new Rectangle(bx, y + 1, bw, 16),
                (float)rate, fillColor, rate.ToString("0") + "%");
        }

        private void DrawStatBox(Graphics g, string label, string value,
                                   int x, int y, int bw)
        {
            var r = new Rectangle(x, y, bw, 36);
            using (var br  = new SolidBrush(CT.SurfaceBg))
            using (var path = CDraw.RoundRect(r, 6))
                g.FillPath(br, path);
            using (var pen  = new Pen(CT.Border, 0.8f))
            using (var path = CDraw.RoundRect(r, 6))
                g.DrawPath(pen, path);

            using (var f  = new Font(CT.FN, 7.5f))
            using (var br = new SolidBrush(CT.TextMuted))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                g.DrawString(label, f, br, new Rectangle(x, y + 3, bw, 14), sf);

            using (var f  = new Font(CT.FN, 11f, FontStyle.Bold))
            using (var br = new SolidBrush(CT.TextPrimary))
            using (var sf = new StringFormat
            {
                Alignment     = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
                g.DrawString(value, f, br, new Rectangle(x, y + 14, bw, 20), sf);
        }
    }
}
