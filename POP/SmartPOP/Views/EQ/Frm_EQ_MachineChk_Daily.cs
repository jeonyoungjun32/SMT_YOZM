using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Common;
using SmartPOP.Model.EQ;
using SmartPOP.Models;
using SmartPOP.Models.CM;
using SmartPOP.Models.EQ;
using SMT_MES.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartPOP.Views.EQ
{
	public partial class Frm_EQ_MachineChk_Daily : DevExpress.XtraEditors.XtraForm
    {
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //점검항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //점검항목 Repository 숫자입력

        //10 : 설비일상점검
        //20 : 설비정기점검
        private string Inspect_Div_Cd = string.Empty; //검사구분
        string SaveDiv = "";
        bool Success = true;
        string Week_Div_Cd = "";

        string G_Rst_No = "";
        int G_Rst_Row_Seq = 0;

        public Frm_EQ_MachineChk_Daily()
        {
            InitializeComponent();
            Inspect_Div_Cd = "10";

            SetDesign();
        } 
        public Frm_EQ_MachineChk_Daily(string Div_Cd, string rRst_No, int rRst_Row_Seq)
        {
            InitializeComponent();

            switch (Div_Cd)
            {
                case "Daily": Inspect_Div_Cd = "10"; break;   //설비일상점검
                case "Routine": Inspect_Div_Cd = "20"; break; //설비정기점검
            }

            G_Rst_No = rRst_No;
            G_Rst_Row_Seq = rRst_Row_Seq;

            SetDesign();
        }

        public void GetDay()
        {
            DateTime nowDt = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue.ToString());

            switch (nowDt.DayOfWeek)
            {
                case DayOfWeek.Monday: Week_Div_Cd = "01"; break;
                case DayOfWeek.Tuesday: Week_Div_Cd = "02"; break;
                case DayOfWeek.Wednesday: Week_Div_Cd = "03"; break;
                case DayOfWeek.Thursday: Week_Div_Cd = "04"; break;
                case DayOfWeek.Friday: Week_Div_Cd = "05"; break;
                case DayOfWeek.Saturday: Week_Div_Cd = "06"; break;
                case DayOfWeek.Sunday: Week_Div_Cd = "07"; break;
            }
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now;
            //rpo.Set_POP_Dtp(DtpInspDate);

            //검사구분에따른 설정
            Set_TypeOption();

            //GridView 설정
            SetGrid_Insp(Gv_Insp);

            Set_Lup();
        }

        private void Set_TypeOption()
        {
            if (Inspect_Div_Cd == "10")
            {
                this.Name = "Frm_EQ_Machine_Chk_Daily";
                this.Text = "설비일상점검";
            }
            else if (Inspect_Div_Cd == "20")
            {
                this.Name = "Frm_EQ_Machine_Chk_Routine";
                this.Text = "설비정기점검";
            }
        }

        #region GridView 셋팅

        private void SetGrid_Insp(GridView gv)
        {
            //합/불검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "점검구분", "Inspect_Div_Cd");
            //grid.Create_GvColumn(gv, "표시순번", "Disp_Seq");

            grid.Create_GvColumn(gv, "관리항목", "Management_Item");
            grid.Create_GvColumn(gv, "확인방법", "Check_Method");
            grid.Create_GvColumn(gv, "점검기준", "Standard_Spec");
            grid.Create_GvColumn(gv, "점검주기", "Cycle_Div_Cd");
            grid.Create_GvColumn(gv, "관리요일", "Week_Div_Cd");
            grid.Create_GvColumn(gv, "등록형태", "ValueType_Div_Cd");
            grid.Create_GvColumn(gv, "점검결과", "Inspect_Check");

            grid.Create_GvColumn(gv, "담당자", "ChargeEmp_No");
            grid.Create_GvColumn(gv, "조치상황", "Action_Result_Div_Cd");
            grid.Create_GvColumn(gv, "조치구분", "Action_Div_Cd");
            grid.Create_GvColumn(gv, "조치결과", "Action_Memo");
            grid.Create_GvColumn(gv, "조치자", "Action_Emp_No");
            grid.Create_GvColumn(gv, "비고", "Rmk");

            grid.Create_GvColumn(gv, "등록자", "Ins_Emp_No");
            grid.Create_GvColumn(gv, "등록일시", "Ins_Date_Time");
            grid.Create_GvColumn(gv, "수정자", "Upd_Emp_No");
            grid.Create_GvColumn(gv, "수정일시", "Upd_Date_Time");

            //등록자, 수정자 column 숨김
            gv.Columns["Ins_Emp_No"].Visible = false;
            gv.Columns["Ins_Date_Time"].Visible = false;
            gv.Columns["Upd_Emp_No"].Visible = false;
            gv.Columns["Upd_Date_Time"].Visible = false;

            //Repository 셋팅
            /*
             * 024 : 설비점검기준              (10 : 설비일상점검, 20: 설비정기점검)
             * 026 : 관리주기(변동코드)        (10 : 1회/일, 20: 1회/주, 30 : 1회/월) 
             * 027 : 관리요일                 (10 : 설비일상점검, 20: 설비정기점검)
             * 028 : 설비검사 값 형태          (10 : 합/불, 20: 값)
             * 032 : 조치상태                 (10 : 조치완료, 20: 미조치)
             * 033 : 조치내역(변동코드)        (10 : 설비수리, 20: 부품교체)
             * 034 : 일별설비점검시간(변동코드) (10 : 10:00, 20 : 14:00)
             */

            //CmCateItem item = new CmCateItem();
            ////CodeUser user = new CodeUser();

            //gv.Columns["Week_Div_Cd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLoopUp("1000", "2060"), "분류코드", "분류명"); // 관리요일
            //gv.Columns["Cycle_Div_Cd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLoopUp("1000", "2050"), "분류코드", "분류명"); // 관리주기
            //gv.Columns["ValueType_Div_Cd"].ColumnEdit = rpo.Create_RepositoryItem(item.Select_Value_Div_Cd(), "코드", "명칭"); // 설비검사 값 형태
            //gv.Columns["Inspect_Div_Cd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLoopUp("1000", "2040"), "분류코드", "분류명"); //설비점검기준
            //gv.Columns["ChargeEmp_No"].ColumnEdit = rpo.Create_RepositoryItem(user.SelectCodeUser(), "작업자ID", "작업자명");  // 담당자
            //gv.Columns["Action_Emp_No"].ColumnEdit = rpo.Create_RepositoryItem(user.SelectCodeUser(), "작업자ID", "작업자명");  // 조치자


            //cmCd.Cm_Class = "032";//조치상태
            //gv.Columns["Action_Result_Div_Cd"].ColumnEdit = rpo.Create_RepositoryItem(cmCd.Select_CmCd(), "Cm_Cd", "Cm_Nm");
            //cmCd.Cm_Class = "033";//조치내역
            //gv.Columns["Action_Div_Cd"].ColumnEdit = rpo.Create_RepositoryItem(cmCd.Select_CmCd(), "Cm_Cd", "Cm_Nm");

            //SC_Emp emp = new SC_Emp();
            //var empList = rpo.Create_RepositoryItem(emp.Select_Emp("All"), "코드", "명칭");
            //gv.Columns["ChargeEmp_No"].ColumnEdit = empList;
            //gv.Columns["Action_Emp_No"].ColumnEdit = empList;

            //점검결과 Inspect_Check   CheckEdit 설정
            rpoChk = rpo.Create_rpoChk();
            rpoChk.ValueChecked = "10";
            rpoChk.ValueUnchecked = "20";

            rpoTxt = rpo.Create_rpoTxt_Numeric();

            RepositoryItemCheckEdit chk_Ins = rpo.Create_rpoChk();
            chk_Ins.ValueChecked = "10";
            chk_Ins.ValueUnchecked = "20";

            gv.Columns["Inspect_Check"].ColumnEdit = chk_Ins;

            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;
            RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
            Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Img.Buttons.RemoveAt(0);
            Btn_Img.Buttons.Add(Btn_Img_Move);
            //groupControl3.Visible = false;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["Inspect_Check"]);
            grid.Column_AllowEdit(gv.Columns["ChargeEmp_No"]);
            grid.Column_AllowEdit(gv.Columns["Action_Result_Div_Cd"]);
            grid.Column_AllowEdit(gv.Columns["Action_Div_Cd"]);
            grid.Column_AllowEdit(gv.Columns["Action_Memo"]);
            grid.Column_AllowEdit(gv.Columns["Action_Emp_No"]);
            grid.Column_AllowEdit(gv.Columns["Rmk"]);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //
            grid.SetPOPGrid(gv);
            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //Row별 Repository 다르게하기
        }

        private void Set_Lup()
        {
            cls_Lookup clsLup = new cls_Lookup();

            //1. 조회조건

            //설비
            EQ_MachineChk machine = new EQ_MachineChk();

            DataTable dt_machine = machine.Select_Machine_Lup(); 
            Lup_Machine.smtLookUpDataSource = dt_machine;
            Lup_Machine.smtLookUpDataDisplayMember = "명칭";
            Lup_Machine.smtLookUpDataValueMember = "코드";
            Lup_Machine.smtLookUpValue = dt_machine.Rows[0][0].ConvertString_Null_To_Empty();

            Lup_Machine.smtLookUpEditValueChanged += Lup_EditValueChanged;

            //LupMachine.Properties.ShowClearButton = false;
            //LupMachine.Properties.View.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            //LupMachine.Properties.View.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정
        }

        private void Event_GvCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            //점검항목 일정Row의 Repository다르게하기
            GridView gv = sender as GridView;
            if (e.RowHandle < 0) return;
            
            string fieldName = e.Column.FieldName;
            if (fieldName == "Inspect_Ok" || fieldName == "Inspect_Check")
            {
                string type = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ValueType_Div_Cd"));

                if (type == "102010")
                {
                    //양/불 선택일경우
                    //Gv_Record.Columns["Inspect_Ok"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //Gv_Record.Columns["Inspect_Check"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.RepositoryItem = rpoChk;
                }
                else if (type == "102020")
                {
                    //측정치 입력일경우
                    Gv_Insp.Columns["Inspect_Ok"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    e.RepositoryItem = rpoTxt;
                }
            }           
        }

        #endregion GridView 셋팅 End


        private void Lup_EditValueChanged(object sender, EventArgs e)
        {
            SearchLookUpEdit Lup = sender as SearchLookUpEdit;

            string LupName = Lup.Name.ToString();
            string LupValue = Lup.EditValue.ConvertString_Null_To_Empty();

            if (LupName == "LupInsp_Div") Inspect_Div_Cd = LupValue;

            Search_InspectItem();
        }

		#endregion

		#region 이벤트

		private void Frm_Load(object sender, EventArgs e)
        {
            Search_InspectItem();
        }

        #endregion

        #region 데이터 조회


        private void Search_InspectItem()
		{
            //검사목록 불러오기

            string Machine_Cd = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
            string Inspect_Date = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();

            if (Machine_Cd == "")
            {
                cls_MessageBox.Show("설비 미입력");
                return;
            }
            // 점검시간관련
            //else if (Chk_Time_Div_Cd == "" && Inspect_Div_Cd == "10")
            //{
            //    cls_MessageBox.Show("점검시간 미입력");
            //    return;
            //}

            GetDay(); //설비정기점검시 관리요일 필요

            EQ_Machine_Chk mc = new EQ_Machine_Chk();
            mc.Inspect_Div_Cd = Inspect_Div_Cd; //10 : 일상점검, 20 : 정기점검
            mc.Inspect_Date = Inspect_Date;
            mc.Machine_Cd = Machine_Cd;
            //mc.Chk_Time_Div_Cd = Chk_Time_Div_Cd;
            mc.Week_Div_Cd = Week_Div_Cd;


            DataTable dtInsp = mc.Select_InspectResult();

            //검사한 항목일경우
            if (dtInsp.Rows.Count > 0)
            {
                SaveDiv = "Upd"; //저장시 업데이트

                Gc_Insp.DataSource = dtInsp;
            }
            //미검사 항목일 경우
            else
            {
                SaveDiv = "Ins"; //저장시 인서트

                EQ_MachineChk mcs = new EQ_MachineChk();
                mcs.Machine_Cd = Machine_Cd;
                mcs.Inspect_Div_Cd = Inspect_Div_Cd;
                mcs.Week_Div_Cd = Week_Div_Cd;

                Gc_Insp.DataSource = mcs.Select_Machine_Chk_List();
            }

            Gv_Insp.BestFitColumns();
        }

        #endregion 데이터 조회 End

        #region 데이터 저장

        private void Save_Data()
        {
            try
			{
                DataTable dtInsp = Gc_Insp.DataSource as DataTable;

                string Machine_Cd = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
                string Insp_Date = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
                //string Chk_Time_Div_Cd = LupInspTime.EditValue.ConvertString_Null_To_Empty();

                if (Machine_Cd == "" || dtInsp == null) return;

                EQ_Machine_Chk mc = new EQ_Machine_Chk();
                mc.Machine_Cd = Machine_Cd;        //설비코드
                mc.Inspect_Date = Insp_Date;         //점검일자
                //mc.Chk_Time_Div_Cd = Chk_Time_Div_Cd;   //설비점검시간
                mc.Inspect_Div_Cd = Inspect_Div_Cd;    //점검구분 10:일상점검, 20:정기점검

                foreach (DataRow dr in dtInsp.Rows)
                {
                    mc.Seq = Convert.ToInt16(dr["Seq"]);                   //검사등록순번
                    mc.Inspect_Check = Convert.ToString(dr["Inspect_Check"]) == "10" ? "Y" : "N";        //점검결과
                    mc.ChargeEmp_No = Convert.ToString(dr["ChargeEmp_No"]);         //담당자
                    mc.Action_Result_Div_Cd = Convert.ToString(dr["Action_Result_Div_Cd"]); //조치진행 코드
                    mc.Action_Div_Cd = Convert.ToString(dr["Action_Div_Cd"]);        //조치내역 코드
                    mc.Action_Memo = Convert.ToString(dr["Action_Memo"]);          //조치내역
                    mc.Action_Emp_No = Convert.ToString(dr["Action_Emp_No"]);        //조치자
                    //mc.Chk_Image = Convert.ToString(dr["Chk_Image"]);
                    mc.Rmk = Convert.ToString(dr["Rmk"]);                                   //비고

                    if (SaveDiv == "Ins")
                    {
                        if (!mc.Ins_InspectResult())
                        {
                            //저장 중 에러 발생시
                            mc.Del_InspectResult();

                            Success = false;
                            break;
                        }
                    }

                    else if (SaveDiv == "Upd")
                    {
                        if (!mc.Upd_InspectResult())
                        {
                            //저장 중 에러 발생시

                            Success = false;
                            break;
                        }
                    }
                }

                if (Success) cls_MessageBox.Show("저장이 정상적으로 완료되었습니다.");
                else cls_MessageBox.Show("저장 중 에러가 발생하였습니다.");

                Search_InspectItem();

            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message);
            }
        }

        #endregion 데이터저장 End

        //조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search_InspectItem();
        }

        //저장
        private void button2_Click(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void smtSearchButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
