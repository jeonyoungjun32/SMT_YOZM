using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.EQ;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_Operation : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        public Frm_EQ_Operation()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_RepairDate.smtStartDateTimeValue = DateTime.Now;

            Set_Grid(Gv_Machine);
        }

        #region GridView Setting

        private void Set_Grid(GridView gv)
        {
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            RepositoryItemSearchLookUpEdit machine = rpo.Create_RepositoryItemSearchLookUp(code.Select_Machine_Lup(false), "설비코드", "설비명");
            RepositoryItemSearchLookUpEdit type = rpo.Create_RepositoryItemSearchLookUp(code.GetClassList("2030M"), "분류코드", "분류명");

            grid.Create_GvColumn(gv, "비가동일자", "WorkDay");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName", machine);
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "비가동사유", "ItemCode", type);
            grid.Create_GvColumn(gv, "시작시간", "StartTime");
            grid.Create_GvColumn(gv, "종료시간", "EndTime");

            gv.Columns["Seq"].Visible = false;
        }

        #endregion GridView Setting
        #endregion 디자인 아이템 생성

        #region 조회 기능

        private void Func_Search()
        {
            EQ_Operation mo = new EQ_Operation();
            DateTime workDay = Dt_RepairDate.smtStartDateTimeValue;

            DataTable dt = mo.Select_EQ_Operation(workDay, workDay);
            Gc_Machine.DataSource = dt;
            Gv_Machine.BestFitColumns();
        }

        #endregion 조회 기능

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Event

        private void Frm_EQ_Operation_Load(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void btn_Ins_Click(object sender, EventArgs e)
        {
            Frm_EQ_OperationDetail frm = new Frm_EQ_OperationDetail("");
            frm.ShowDialog();

            Func_Search();
        }

        private void btn_Upd_Click(object sender, EventArgs e)
        {
            if (Gv_Machine.DataRowCount < 1) { return; }

            string machineCode = Gv_Machine.GetFocusedRowCellValue("MachineCode").ConvertString_Null_To_Empty();
            int seq = Gv_Machine.GetFocusedRowCellValue("Seq").ConvertInt_Null_To_Zero();

            if (machineCode == "")
            {
                return;
            }

            Frm_EQ_OperationDetail frm = new Frm_EQ_OperationDetail(machineCode, seq);
            frm.ShowDialog();

            Func_Search();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (Gv_Machine.DataRowCount < 1) { return; }

            string machineCode = Gv_Machine.GetFocusedRowCellDisplayText("MachineCode").ConvertString_Null_To_Empty();
            int seq = Gv_Machine.GetFocusedRowCellValue("Seq").ConvertInt_Null_To_Zero();

            if (MessageBox.Show($"선택한 '{machineCode}' 설비비가동내역을 삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            try
            {
                EQ_Operation mo = new EQ_Operation();
                mo.MachineCode = machineCode;
                mo.Seq = seq;

                if (!mo.Del_EQ_Operation())
                {
                    MessageBox.Show("삭제 중 에러가 발생하였습니다.");
                    return;
                }

                MessageBox.Show("삭제가 완료되었습니다.");

                Func_Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Gv_Machine_DoubleClick(object sender, EventArgs e)
        {
            if (Gv_Machine.DataRowCount < 1) { return; }

            string machineCode = Gv_Machine.GetFocusedRowCellDisplayText("MachineCode").ConvertString_Null_To_Empty();
            int seq = Gv_Machine.GetFocusedRowCellValue("Seq").ConvertInt_Null_To_Zero();

            if (machineCode == "")
            {
                return;
            }

            Frm_EQ_OperationDetail frm = new Frm_EQ_OperationDetail(machineCode, seq, false);
            frm.ShowDialog();

            Func_Search();
        }

        private void smtSearchButton1_Click(object sender, EventArgs e)
        {
            Func_Search();
        }
        #endregion Event

    }
}