using baseSMT;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.EQ;
using SmartMES.Models.PM;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_MachineWorkHistoryView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        public Frm_EQ_MachineWorkHistoryView()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            //GridView 설정
            SetGrid_Insp(GvWork);

            Set_Lup();
        }

        private void Set_Lup()
        {
            LupMachine.smtLookUpDataSource = code.GetMachineList(true);
            LupMachine.smtLookUpDataDisplayMember = "설비명";
            LupMachine.smtLookUpDataValueMember = "설비코드";
            LupMachine.smtLookUpValue = "ALL";
        }

        #region GridView 셋팅

        private void SetGrid_Insp(GridView gv)
        {
            //합/불검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            //grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "작업지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "순번", "OrderSeq");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");
            grid.Create_GvColumn(gv, "담당자", "EmployeeName");
            grid.Create_GvColumn(gv, "작업일자", "WorkDate");
            grid.Create_GvColumn(gv, "시작시간", "StartDate");
            grid.Create_GvColumn(gv, "종료시간", "EndDate");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");

            //column 숨김
            gv.Columns["MachineCode"].Visible = false;
            gv.Columns["StartDate"].Visible = false;
            gv.Columns["EndDate"].Visible = false;
            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreDt"].Visible = false;

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["GoodQty"]);
            //grid.Column_AllowEdit(gv.Columns["NgQty"]);

            //gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
        }

        #endregion GridView 셋팅 End

        #endregion

        #region 이벤트

        private void Frm_Load(object sender, EventArgs e)
        {
            Search_Master();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton Btn = sender as smtSearchButton;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdDisplay": Search_Master(); break;
                case "cmdClose": this.Close(); break;
            }
        }

        #endregion

        #region 데이터 조회


        private void Search_Master()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            string MachineCode = LupMachine.smtLookUpValue.ConvertString_Null_To_Empty();

            EQ_MachineChk mc = new EQ_MachineChk();
            mc.startDate = startDate;
            mc.endDate = endDate;
            mc.MachineCode = MachineCode;

            GcWork.DataSource = mc.Select_MachineWork();
            GvWork.BestFitColumns();
        }

        #endregion 데이터 조회 End

        #region 데이터 저장

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                //작성내역 저장
                DataTable dt = GcWork.DataSource as DataTable;

                //1. 데이터 불러온 상태인지 검사
                if (!Check_SaveClick(dt)) return;

                //2. 데이터 저장
                for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
                {
                    if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                    {
                        if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
                    }
                }
                //3. 조회
                Search_Master();

                MessageBox.Show("저장이 정상적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;

            //미입력된 칸 있는지 확인
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Status"]) == "신규") { checkCnt++; }
                else if (Convert.ToString(dr["Status"]) == "수정") { checkCnt++; }
            }
            if (checkCnt == 0) return false;

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            PM_OrderResult odr = new PM_OrderResult();

            odr.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
            odr.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
            odr.SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty();
            odr.GoodQty = dr["GoodQty"].ConvertInt_Null_To_Zero();
            odr.NgQty = dr["NgQty"].ConvertInt_Null_To_Zero();

            //2. DB 적용
            if (div == 'U')
            {
                //Update
                if (!odr.Upd_PM_OrderResult_Qty())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }


            //상태값 초기화
            dr[0] = "";

            return true;
        }
        #endregion 데이터저장 End

    }
}
