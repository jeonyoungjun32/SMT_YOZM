using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.EQ;
using SmartMES.View.Lookup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_MachineDowntime : DevExpress.XtraEditors.XtraForm
    {
        private string query = "";

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        Database db = new Database();

        clsCode code = new clsCode();

        public Frm_EQ_MachineDowntime()
        {
            InitializeComponent();
        }

        private void Frm_EQ_MachineDowntime_Load(object sender, EventArgs e)
        {
            SetDesign();

            DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));
            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            DisplayOper();
        }

        #region 디자인 아이템 생성
        private void SetDesign()
        {
            //GridView 설정
            SetGrid(GvOperation);
            Set_Lup();
        }

        #region GridView 세팅

        private void SetGrid(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lupOper = rpo.Create_RepositoryItemSearchLookUp(code.GetOperationCode(), "코드", "코드명");

            //라인
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List("", false), "라인코드", "라인명");

            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(false), "공정코드", "공정명");

            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "비가동일자", "WorkDay", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "비가동코드", "ClassItemCode", lupOper);
            grid.Create_GvColumn(gv, "시작시간", "StartTime", rpo.Create_TimeEdit());
            grid.Create_GvColumn(gv, "종료시간", "EndTime", rpo.Create_TimeEdit());
            grid.Create_GvColumn(gv, "정지시간", "StopTime");
            grid.Create_GvColumn(gv, "비가동사유", "Remark", rpo.Create_rpoMemo());
            grid.Create_GvColumn(gv, "조치내용", "OperationMeasure", rpo.Create_rpoMemo());
            grid.Create_GvColumn(gv, "작업지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "작업지시순번", "OrderSeq");

            gv.Columns["Remark"].MinWidth = 200;
            gv.Columns["OperationMeasure"].MinWidth = 200;

            // 수정여부 설정
            grid.Column_AllowEdit(gv.Columns["WorkDay"]);
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["LineCode"]);
            grid.Column_AllowEdit(gv.Columns["MachineCode"]);
            grid.Column_AllowEdit(gv.Columns["ClassItemCode"]);
            grid.Column_AllowEdit(gv.Columns["StartTime"]);
            grid.Column_AllowEdit(gv.Columns["EndTime"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["OperationMeasure"]);

            // Lup 검색설정
            lupOper.View.OptionsView.ShowAutoFilterRow = true;

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // 컬럼 숨김
            if (Database.DbName.Contains("RNDZIPPER"))
            {
                // 작업지시번호, 작업지시순번 숨김
                gv.Columns["OrderNo"].Visible = false;
                gv.Columns["OrderSeq"].Visible = false;
                gv.Columns["StopTime"].Visible = false;         // 정지시간
            }

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
        }

        #endregion GridView 세팅 END

        private void Set_Lup()
        {
            Lup_Machine.smtLookUpDataSource = code.Select_Machine_Lup(true, true);
            Lup_Machine.smtLookUpDataDisplayMember = "설비명";
            Lup_Machine.smtLookUpDataValueMember = "설비코드";

            Lup_Machine.smtLookUpValue = "ALL";
        }

        #endregion


        #region 조회

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayOper();
        }

        private void DisplayOper()
        {
            EQ_Operation op = new EQ_Operation();
            op.MachineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();

            GcOperation.DataSource = op.Select_EQ_Operation(Dt_Date.smtStartDateTimeValue, Dt_Date.smtEndDateTimeValue);
            GvOperation.BestFitColumns();
        }

        #endregion

        #region 행추가, 삭제, 저장

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            GridView gv = GvOperation;

            gv.PostEditor();
            gv.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = GcOperation.DataSource as DataTable;

            if (!Chk_Oper(dt)) return;

            //1. 데이터 저장
            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            DisplayOper();
        }

        private bool Chk_Oper(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            string status = "";

            DateTime startTime, endTime;

            foreach (DataRow dr in dt.Rows)
            {
                status = Convert.ToString(dr["Status"]);

                bool isStartTimeValid = DateTime.TryParse(Convert.ToString(dr["StartTime"]), out startTime);
                bool isEndTimeValid = DateTime.TryParse(Convert.ToString(dr["EndTime"]), out endTime);

                if (status == "신규" || status == "수정")
				{
                    if (Convert.ToString(dr["MachineCode"]) == "")
                    {
                        MessageBox.Show("설비 항목이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["WorkDay"]) == "")
					{
						MessageBox.Show("비가동일자 항목이 비어있습니다.");
						return false;
					}
                    else if (Convert.ToString(dr["ClassItemCode"]) == "")
                    {
                        MessageBox.Show("비가동코드 항목이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["StartTime"]) == "")
                    {
                        MessageBox.Show("시작시간 항목이 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["EndTime"]) == "")
					{
                        MessageBox.Show("종료시간 항목이 비어있습니다.");
                        return false;
					}
                    else if (!isStartTimeValid || !isEndTimeValid)
                    {
                        MessageBox.Show("시작시간 또는 종료시간의 형식이 올바르지 않습니다.");
                        return false;
                    }
                    else if (endTime < startTime)
                    {
                        MessageBox.Show("종료시간이 시작시간보다 빠를 수 없습니다.\n시간을 다시 확인해주세요.");
                        return false;
                    }

                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정") { checkCnt++; }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {

                    checkCnt++;
                    isDelete = true;
                }
            }
            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            DateTime startTime = new DateTime();
            DateTime.TryParse(dr["StartTime"].ConvertString_Null_To_Empty(), out startTime);

            DateTime endTime = new DateTime();
            DateTime.TryParse(dr["EndTime"].ConvertString_Null_To_Empty(), out endTime);

            EQ_Operation mo = new EQ_Operation();
            mo.LineCode = dr["LineCode"].ConvertString_Null_To_Empty();
            mo.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
            mo.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();
            mo.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
            mo.WorkDay = dr["WorkDay"].ConvertDate_To_Char8();
            mo.StartTime = startTime.ToString("yyyy-MM-dd HH:mm");
            mo.EndTime = endTime.ToString("yyyy-MM-dd HH:mm");
            mo.StopTime = dr["StopTime"].ConvertInt_Null_To_Zero();
            mo.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
            mo.Remark = dr["Remark"].ConvertString_Null_To_Empty();
            mo.OperationMeasure = dr["OperationMeasure"].ConvertString_Null_To_Empty();
            mo.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
            mo.OperationYn = "N";   // 비가동
            mo.CLASS = dr["CLASS"].ConvertString_Null_To_Empty();     // 설비 비가동사유
            mo.ItemCode = dr["ClassItemCode"].ConvertString_Null_To_Empty();


            if (div == 'I')
            {
                mo.Seq = mo.Get_LastEQ_OperationSeq()["Seq"].ConvertInt_Null_To_Zero();

                if (!mo.Ins_EQ_Operation())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'U')
            {
                if (!mo.Upd_EQ_Operation())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'D')
            {
                if(!mo.Del_EQ_Operation())
                {
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }

            dr[0] = "";

            return true;
        }

        #endregion

        #region 이벤트

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fielName = e.Column.FieldName;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            switch (fielName)
            {
                case "StartTime":
                case "EndTime":
                    string start = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "StartTime"));
                    string end = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "EndTime"));

                    DateTime startTime = new DateTime();
                    DateTime endTime = new DateTime();

                    if (DateTime.TryParse(start, out startTime) && DateTime.TryParse(end, out endTime))
                    {
                        gv.SetRowCellValue(e.RowHandle, "StopTime", endTime.Subtract(startTime).TotalMinutes);
                    }
                    break;
            }

            gv.BestFitColumns();
        }

        private void BtnClick(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");

                //신규일때, 공정명, 라인명, 설비명을 가져옴.
                dic.Add("WorkDay", DateTime.Now);
                //dic.Add("ProcessCode", processCode);
                //dic.Add("ProcessName", processName);
                //dic.Add("LineCode", lineCode);
                //dic.Add("LineName", lineName);
                //dic.Add("MachineCode", machineCode);
                //dic.Add("MachineName", machineName);
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(GvOperation.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(GcOperation, dic, txt_Act);
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            string empNo = WrGlobal.LoginID;

            Frm_SelectPM_ProductPlan frm = new Frm_SelectPM_ProductPlan(empNo);
            frm.ShowDialog();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}