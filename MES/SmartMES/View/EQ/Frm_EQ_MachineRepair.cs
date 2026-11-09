using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.EQ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_MachineRepair : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_EQ_MachineRepair()
        {
            InitializeComponent();

            Set_Design();
            Set_GridView(gvRepair);
        }

        private void Set_Design()
        {
            DataTable dt = code.GetMachineList(true);

            Lup_Machine.smtLookUpDataSource = dt;
            Lup_Machine.smtLookUpDataValueMember = "설비코드";
            Lup_Machine.smtLookUpDataDisplayMember = "설비명";
            Lup_Machine.smtLookUpValue = "ALL";
        }

        private void Set_GridView(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            // 날짜
            RepositoryItemDateEdit dateRepair = new RepositoryItemDateEdit();
            dateRepair.Mask.EditMask = "yyyy-MM-dd";
            dateRepair.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateRepair.UseMaskAsDisplayFormat = true;

            RepositoryItemDateTimeOffsetEdit dateStart = new RepositoryItemDateTimeOffsetEdit();
            dateStart.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            dateStart.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateStart.UseMaskAsDisplayFormat = true;

            RepositoryItemDateTimeOffsetEdit dateEnd = new RepositoryItemDateTimeOffsetEdit();
            dateEnd.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            dateEnd.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateEnd.UseMaskAsDisplayFormat = true;

            RepositoryItemSearchLookUpEdit lup_Machine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList(false), "설비코드", "설비명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "수리일자", "RepairDate", dateRepair);
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName", lup_Machine);
            grid.Create_GvColumn(gv, "시작시간", "RepairStartTime", dateStart);
            grid.Create_GvColumn(gv, "종료시간", "RepairEndTime", dateEnd);
            grid.Create_GvColumn(gv, "고장내용", "RePairCause");
            grid.Create_GvColumn(gv, "수리내용", "RepairState");
            grid.Create_GvColumn(gv, "담당자", "RepairEmpNo");
            //grid.Create_GvColumn(gv, "등록자", "CreId");
            //grid.Create_GvColumn(gv, "등록일시", "CreDt");
            //grid.Create_GvColumn(gv, "수정자", "ModId");
            //grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["RepairDate"]);            //수리일자
            grid.Column_AllowEdit(gv.Columns["MachineName"]);           //설비명
            grid.Column_AllowEdit(gv.Columns["RepairStartTime"]);       //시작시간
            grid.Column_AllowEdit(gv.Columns["RepairEndTime"]);         //종료시간
            grid.Column_AllowEdit(gv.Columns["RePairCause"]);           //고장내용
            grid.Column_AllowEdit(gv.Columns["RepairState"]);           //수리내용
            grid.Column_AllowEdit(gv.Columns["RepairEmpNo"]);           //담당자

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            RepositoryItemSearchLookUpEdit lup_Manager = new RepositoryItemSearchLookUpEdit();
            lup_Manager.DataSource = code.GetUser();
            lup_Manager.DisplayMember = "사용자명";
            lup_Manager.ValueMember = "ID";
            lup_Manager.NullText = "";
            gv.Columns["RepairEmpNo"].ColumnEdit = lup_Manager;

            //grid.SetCol_Type(gv.Columns["RepairStartTime"], clsGrid.TypeList.TDateTime);
            //grid.SetCol_Type(gv.Columns["RepairEndTime"], clsGrid.TypeList.TDateTime);

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            //gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정

            gv.Columns["MachineCode"].Visible = false;
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
        }

        private void Frm_EQ_MachineRepair_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            string machineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();

            EQ_Repair repair = new EQ_Repair();
            repair.MachineCode = machineCode;

            DataTable dt = repair.Select_EQ_Repair();

            gcRepair.DataSource = dt;
            gvRepair.BestFitColumns();
        }

        private void cmd_Click(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            smtSearchButton btn = sender as smtSearchButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(gvRepair.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(gcRepair, dic, txt_Act);
        }

        #region 저장 기능

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            //미입력된 칸 있는지 확인
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Status"]) == "신규")
                {
                    if (Convert.ToString(dr["MachineName"]) == "")
                    {
                        MessageBox.Show("설비명 항목이 비어있습니다.");
                        return false;
                    }

                    if (Convert.ToString(dr["RepairDate"]) == "")
                    {
                        MessageBox.Show("수리일자 항목이 비어있습니다.");
                        return false;
                    }

                    if (Convert.ToString(dr["RepairStartTime"]) == "" || Convert.ToString(dr["RepairEndTime"]) == "")
                    {
                        MessageBox.Show("시작시간, 종료시간을 입력해주세요.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정") { checkCnt++; }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {
                    isDelete = true;
                    checkCnt++;
                }
            }
            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    return true;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            EQ_Repair repair = new EQ_Repair();
            repair.Repair_No = dr["RepairNo"].ConvertString_Null_To_Empty();

            if (div != 'D')
            {
                repair.Repair_Date = dr["RepairDate"].ConvertDate_To_Char8();
                repair.MachineCode = dr["MachineName"].ConvertString_Null_To_Empty();
                repair.Repair_Cause = dr["RePairCause"].ConvertString_Null_To_Empty();
                repair.Repair_State = dr["RepairState"].ConvertString_Null_To_Empty();
                repair.Repair_Emp_No = dr["RepairEmpNo"].ConvertString_Null_To_Empty();
                repair.Repair_Start_Time = Convert.ToDateTime(dr["RepairStartTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                repair.Repair_End_Time = Convert.ToDateTime(dr["RepairEndTime"]).ToString("yyyy-MM-dd HH:mm:ss");
            }

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!repair.Ins_EQ_Repair())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'U')
            {
                //Update
                if (!repair.Upd_EQ_Repair())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
            }
            else if (div == 'D')
            {
                //Update
                if (!repair.Del_EQ_Repair())
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
        #endregion 저장 기능 end

        //엑셀 버튼
        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(gvRepair);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                gvRepair.PostEditor();
                gvRepair.UpdateCurrentRow();

                //작성내역 저장
                DataTable dt = gcRepair.DataSource as DataTable;

                //1. 데이터 불러온 상태인지 검사
                if (!Check_SaveClick(dt)) return;

                //2. 데이터 저장
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
                //3. 조회
                DisplayData();

                MessageBox.Show("저장이 정상적으로 완료되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(gvRepair);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}