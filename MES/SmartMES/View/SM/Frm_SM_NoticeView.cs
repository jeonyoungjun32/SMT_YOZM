using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.SM;
using System;
using System.Windows.Forms;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_NoticeView : DevExpress.XtraEditors.XtraForm
    {
        string query = "";
        Database db = new Database();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_SM_NoticeView()
        {
            InitializeComponent();

            // 디자인
            Set_Design();

            // 조회
            DisplayData();
        }

        #region 디자인
        private void Set_Design()
        {
            // 날짜
            Set_Dt();

            // 그리드
            Set_Grid(gvSM_Notice);
        }

        private void Set_Dt()
        {
            smtDatetime.smtStartDateTimeValue = DateTime.Now.AddYears(-1); ;
            smtDatetime.smtEndDateTimeValue = DateTime.Now;
        }

        private void Set_Grid(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            clsCode code = new clsCode();
            RepositoryItemSearchLookUpEdit lup_User = rpo.Create_RepositoryItemSearchLookUp(code.Get_UserList(), "코드", "코드명");

            grid.Create_GvColumn(gv, "공지번호", "BoardNo");
            grid.Create_GvColumn(gv, "작성일자", "WriteDate");
            grid.Create_GvColumn(gv, "제목", "Subject");
            grid.Create_GvColumn(gv, "시작일자", "StartDate");
            grid.Create_GvColumn(gv, "종료일자", "EndDate");
            grid.Create_GvColumn(gv, "작성자", "CreId", lup_User);
            grid.Create_GvColumn(gv, "최종수정자", "ModId", lup_User);

            // 포맷

            // 날짜
            grid.SetCol_Type(gv.Columns["WriteDate"], clsGrid.TypeList.TDateTime);
            grid.SetCol_Type(gv.Columns["StartDate"], clsGrid.TypeList.TDateTime);
            grid.SetCol_Type(gv.Columns["EndDate"], clsGrid.TypeList.TDateTime);

            // 이벤트
            gv.DoubleClick += Gv_DoubleClick;
        }

        private void Gv_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo info = view.CalcHitInfo(view.GridControl.PointToClient(Control.MousePosition));
            if (!info.InRow) return;

            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridRowInfo rowInfo = info.RowInfo;
            if (rowInfo == null) return;

            if (gvSM_Notice != null && gvSM_Notice.IsValidRowHandle(rowInfo.RowHandle))
            {
                string BoardNo = gvSM_Notice.GetFocusedRowCellDisplayText("BoardNo") == null ? "" : gvSM_Notice.GetFocusedRowCellDisplayText("BoardNo");
                Frm_SM_Notice form = new Frm_SM_Notice(BoardNo, false);
                form.ShowDialog();
            }
        }
        #endregion

        #region 버튼이벤트
        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;

            string name = btn.smtButtonStyle.ConvertString_Null_To_Empty();

            switch (name)
            {
                case "조회":
                    DisplayData();
                    break;

                case "등록":
                    Ins_SM_Notice();
                    break;

                case "수정":
                    Upd_SM_Notice();
                    break;

                case "삭제":
                    Del_SM_Notice();
                    break;

                case "닫기":
                    this.Close();
                    break;

                default:
                    break;
            }
        }

        // 조회
        private void DisplayData()
        {
            SM_Notice nt = new SM_Notice();
            nt.SearchStart = smtDatetime.smtStartDateTimeValue.ToString("yyyyMMdd");
            nt.SearchEnd = smtDatetime.smtEndDateTimeValue.ToString("yyyyMMdd");

            gcSM_Notice.DataSource = nt.Select_SM_NoticeList();
            gvSM_Notice.BestFitColumns();
        }

        private void Ins_SM_Notice()
        {
            Frm_SM_Notice form = new Frm_SM_Notice("");
            form.ShowDialog();

            DisplayData();
        }

        private void Upd_SM_Notice()
        {
            string BoardNo = gvSM_Notice.GetFocusedRowCellValue("BoardNo").ConvertString_Null_To_Empty();
            Frm_SM_Notice form = new Frm_SM_Notice(BoardNo);
            form.ShowDialog();

            DisplayData();
            gvSM_Notice.FocusedRowHandle = gvSM_Notice.LocateByDisplayText(0, gvSM_Notice.Columns["BoardNo"], BoardNo);
        }

        private void Del_SM_Notice()
        {
            string BoardNo = gvSM_Notice.GetFocusedRowCellValue("BoardNo").ConvertString_Null_To_Empty();

            if (MessageBox.Show($"선택한 공지사항을 삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            SM_Notice nt = new SM_Notice();
            nt.BoardNo = BoardNo;

            if (!nt.Del_SM_Notice())
            {
                MessageBox.Show("저장 중 오류가 발생했습니다.");
            }

            MessageBox.Show("삭제가 완료되었습니다.");

            DisplayData();
        }
        #endregion
    }
}
