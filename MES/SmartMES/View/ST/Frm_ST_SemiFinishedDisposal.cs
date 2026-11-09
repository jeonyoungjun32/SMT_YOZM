using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.ST
{
    public partial class Frm_ST_SemiFinishedDisposal : DevExpress.XtraEditors.XtraForm
    {
        private readonly clsGrid grid = new clsGrid();
        private readonly string menuId;
        private string requestId;

        public Frm_ST_SemiFinishedDisposal(string sourceMenuId)
        {
            InitializeComponent();
            menuId = sourceMenuId;
            SetGridView();
            Auth.ApplyButtonAuth(menuId, smtCommonBar1);
            lblGuide.Text = "보관기간 " + SemiFinishedStock.ShelfLifeDays + "일 / 만료 LOT는 붉게, 생산일 미확인 LOT는 노랗게 표시됩니다.";
            Shown += Frm_ST_SemiFinishedDisposal_Shown;
        }

        private void SetGridView()
        {
            grid.Create_GvColumn(Gv_Stock, "창고코드", "HouseCode");
            grid.Create_GvColumn(Gv_Stock, "창고", "HouseName");
            grid.Create_GvColumn(Gv_Stock, "품목코드", "ItemCode");
            grid.Create_GvColumn(Gv_Stock, "품명", "ItemName");
            grid.Create_GvColumn(Gv_Stock, "LOT", "LotNo");
            grid.Create_GvColumn(Gv_Stock, "현재고", "StockQty");
            grid.Create_GvColumn(Gv_Stock, "생산일시", "ProducedAt");
            grid.Create_GvColumn(Gv_Stock, "만료일시", "ExpiresAt");
            grid.Create_GvColumn(Gv_History, "폐기일시", "InOutDate");
            grid.Create_GvColumn(Gv_History, "품목코드", "ItemCode");
            grid.Create_GvColumn(Gv_History, "품명", "ItemName");
            grid.Create_GvColumn(Gv_History, "LOT", "LotNo");
            grid.Create_GvColumn(Gv_History, "창고", "HouseCode");
            grid.Create_GvColumn(Gv_History, "폐기수량", "Qty");
            grid.Create_GvColumn(Gv_History, "폐기사유", "Remark");
            grid.Create_GvColumn(Gv_History, "처리자", "WriteID");
            grid.Create_GvColumn(Gv_History, "수불번호", "InOutNo");
            foreach (GridView view in new[] { Gv_Stock, Gv_History })
            {
                view.OptionsBehavior.Editable = false;
                view.OptionsView.ColumnAutoWidth = false;
                view.OptionsView.ShowGroupPanel = false;
                view.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            foreach (string field in new[] { "ProducedAt", "ExpiresAt" })
            {
                Gv_Stock.Columns[field].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                Gv_Stock.Columns[field].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            }
        }

        private void Frm_ST_SemiFinishedDisposal_Shown(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            try
            {
                Gc_Stock.DataSource = SemiFinishedStock.LoadLots(Database.m_ConnectionString, WrGlobal.CorpID);
                Gc_History.DataSource = SemiFinishedStock.LoadDisposals(Database.m_ConnectionString, WrGlobal.CorpID);
                Gv_Stock.BestFitColumns();
                Gv_History.BestFitColumns();
                ResetInput();
            }
            catch (Exception ex) { cls_MessageBox.Show(ex.Message); }
        }

        private void ResetInput()
        {
            Txt_Qty.EditValue = 0m;
            Txt_Reason.EditValue = "";
            requestId = "DS" + Guid.NewGuid().ToString("N").Substring(0, 18);
        }

        private void Gv_Stock_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ResetInput();
        }

        private void Gv_Stock_RowStyle(object sender, RowStyleEventArgs e)
        {
            DataRow row = Gv_Stock.GetDataRow(e.RowHandle);
            if (row == null) return;
            if (row.IsNull("ExpiresAt")) e.Appearance.BackColor = Color.LightYellow;
            else if (Convert.ToDateTime(row["ExpiresAt"]) <= DateTime.Now) e.Appearance.BackColor = Color.MistyRose;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!Auth.HasAuth(menuId, "02")) { cls_MessageBox.Show("등록/수정 권한이 필요합니다."); return; }
            DataRow row = Gv_Stock.GetFocusedDataRow();
            if (row == null) return;
            decimal quantity = Txt_Qty.Value;
            if (quantity <= 0 || string.IsNullOrWhiteSpace(Txt_Reason.Text))
            { cls_MessageBox.Show("폐기수량과 폐기사유를 입력해주세요."); return; }
            if (cls_MessageBox.Show("선택 LOT에서 " + quantity + "만큼 폐기 처리하시겠습니까?", Text,
                MessageBoxButtons.OKCancel) != DialogResult.OK) return;
            cmdSave.Enabled = false;
            try
            {
                SemiFinishedStock.DisposeLot(Database.m_ConnectionString, WrGlobal.CorpID,
                    Convert.ToString(row["ItemCode"]), Convert.ToString(row["HouseCode"]),
                    Convert.ToString(row["LotNo"]), quantity, Txt_Reason.Text, WrGlobal.LoginID, requestId);
                DisplayData();
                cls_MessageBox.Show("폐기와 재고수불을 저장했습니다.");
            }
            catch (Exception ex) { cls_MessageBox.Show("폐기 처리를 취소했습니다.\r\n" + ex.Message); }
            finally { cmdSave.Enabled = Auth.HasAuth(menuId, "02"); }
        }
    }
}
