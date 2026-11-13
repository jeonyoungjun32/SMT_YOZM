using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace SmartMES.Common
{
    /// <summary>컬럼 헤더 드래그로 왼쪽 고정·해제</summary>
    internal sealed class GridColumnPinning : IMessageFilter
    {
        // 화면별 이벤트 중복 연결 방지, 닫힌 화면의 참조는 자동 정리
        private readonly ConditionalWeakTable<GridView, object> attachedViews = new ConditionalWeakTable<GridView, object>();

        /// <summary>일반 그리드의 헤더를 처음 클릭할 때 드래그 이벤트 연결</summary>
        public bool PreFilterMessage(ref Message message)
        {
            const int leftButtonDown = 0x0201;

            if (message.Msg != leftButtonDown)
                return false;

            for (Control control = Control.FromChildHandle(message.HWnd); control != null; control = control.Parent)
            {
                if (!(control is GridControl grid))
                    continue;

                var point = grid.PointToClient(Control.MousePosition);

                // 밴드형 그리드는 별도 헤더 구조이므로 제외
                if (grid.GetViewAt(point) is GridView view
                    && !(view is BandedGridView)
                    && view.CalcHitInfo(point).InColumn)
                    attachedViews.GetValue(view, Attach);

                break;
            }

            // 원래 클릭과 드래그 동작은 그대로 전달
            return false;
        }

        /// <summary>드래그 위치에 따라 고정 상태를 결정하고 드롭 시 적용</summary>
        private static object Attach(GridView view)
        {
            FixedStyle? dropStyle = null;

            view.DragObjectStart += (sender, args) => dropStyle = null;

            view.DragObjectOver += (sender, args) =>
            {
                dropStyle = null;

                if (!(args.DragObject is GridColumn column) || args.DropInfo == null)
                    return;

                GridControl grid = view.GridControl;
                Point point = grid.PointToClient(Control.MousePosition);
                var hit = view.CalcHitInfo(point);

                if (!hit.InColumn || hit.Column == null || hit.Column == column)
                    return;

                // 고정 영역 위에서는 왼쪽 고정, 일반 영역 위에서는 고정 해제
                if (hit.Column.Fixed == FixedStyle.Left)
                    dropStyle = FixedStyle.Left;
                else if (hit.Column.Fixed == FixedStyle.None)
                {
                    // 고정 컬럼이 없으면 첫 헤더의 왼쪽 가장자리에서 고정 시작
                    int edgeWidth = 16 * grid.DeviceDpi / 96;
                    var left = view.CalcHitInfo(new Point(point.X - edgeWidth, point.Y));
                    bool atLeftEdge = view.VisibleColumns.Count > 0
                        && hit.Column == view.VisibleColumns[0]
                        && left.Column != hit.Column;

                    dropStyle = atLeftEdge ? FixedStyle.Left : FixedStyle.None;
                }

                // 왼쪽 고정 영역과 일반 영역 사이의 이동만 허용
                if (dropStyle.HasValue && dropStyle.Value != column.Fixed
                    && (column.Fixed == FixedStyle.Left || column.Fixed == FixedStyle.None))
                    args.DropInfo.Valid = true;
            };

            view.DragObjectDrop += (sender, args) =>
            {
                FixedStyle? style = dropStyle;
                dropStyle = null;

                if (args.Canceled || !style.HasValue || !(args.DragObject is GridColumn column)
                    || !column.Visible || column.Fixed == style.Value
                    || (column.Fixed != FixedStyle.Left && column.Fixed != FixedStyle.None))
                    return;

                // 고정 후 남은 컬럼이 자동 축소되지 않도록 가로 스크롤 유지
                if (style.Value == FixedStyle.Left)
                    view.OptionsView.ColumnAutoWidth = false;

                column.Fixed = style.Value;
            };

            return new object();
        }
    }
}
