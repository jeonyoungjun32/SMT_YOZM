using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Model.EQ;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.EQ;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_MachineChk : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //점검항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //점검항목 Repository 숫자입력
        CM_CateItem item = new CM_CateItem();

        //10 : 설비일상점검
        //20 : 설비정기점검
        private string Inspect_Div_Cd = string.Empty; //검사구분
        string SaveDiv = "";
        bool Success = true;
        string WeekDivCd = "";

        string G_Rst_No = "";
        int G_Rst_Row_Seq = 0;

        public Frm_EQ_MachineChk(string Div_Cd)
        {
            InitializeComponent();

            switch (Div_Cd)
            {
                case "Daily": Inspect_Div_Cd = "10"; break;   //설비일상점검
                case "Routine": Inspect_Div_Cd = "20"; break; //설비정기점검
            }

            SetDesign();
        }

        public void GetDay()
        {
            //DateTime nowDt = DateTime.Now;
            DateTime nowDt = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue.ToString());

            switch (nowDt.DayOfWeek)
            {
                case DayOfWeek.Monday:      WeekDivCd = "10"; break;
                case DayOfWeek.Tuesday:     WeekDivCd = "20"; break;
                case DayOfWeek.Wednesday:   WeekDivCd = "30"; break;
                case DayOfWeek.Thursday:    WeekDivCd = "40"; break;
                case DayOfWeek.Friday:      WeekDivCd = "50"; break;
                case DayOfWeek.Saturday:    WeekDivCd = "60"; break;
                case DayOfWeek.Sunday:      WeekDivCd = "70"; break;
            }
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now;

            //검사구분에따른 설정
            Set_TypeOption();

            //GridView 설정
            SetGrid_Insp(Gv_Insp);

            Set_Lup();
        }

        private void Set_TypeOption()
        {
            if(Inspect_Div_Cd == "10")
            {
                this.Name = "Frm_EQ_MachineChk_Daily";
                this.Text = "설비일상점검";
                //LblMainTitle.Text = "설비일상점검";

            }
            else if(Inspect_Div_Cd == "20")
            {
                this.Name = "Frm_EQ_MachineChk_Routine";
                this.Text = "설비정기점검";
                //LblMainTitle.Text = "설비정기점검";
                Lup_InspTime.Visible = false;

                //cmdDisplay.Location = new System.Drawing.Point(375, 6);
                //cmdSave.Location = new System.Drawing.Point(450, 6);
            }
        }

        #region GridView 셋팅

		private void SetGrid_Insp(GridView gv)
        {
            //합/불검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false


            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "점검구분", "InspectDivCd");
            //grid.Create_GvColumn(gv, "표시순번", "DispSeq");

            grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
            grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
            grid.Create_GvColumn(gv, "점검기준", "StandardSpec");
            grid.Create_GvColumn(gv, "점검주기", "CycleDivCd");
            grid.Create_GvColumn(gv, "관리요일", "WeekDivCd");
            grid.Create_GvColumn(gv, "등록형태", "ValueTypeDivCd");
            grid.Create_GvColumn(gv, "점검결과", "InspectCheck");

            grid.Create_GvColumn(gv, "담당자", "ChargeEmpNo");
            grid.Create_GvColumn(gv, "조치상황", "ActionResultDivCd");
            grid.Create_GvColumn(gv, "조치구분", "ActionDivCd");
            grid.Create_GvColumn(gv, "조치결과", "ActionMemo");
            grid.Create_GvColumn(gv, "조치자", "ActionEmpNo");
            grid.Create_GvColumn(gv, "점검이미지", "ChkImage");
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //등록자, 수정자 column 숨김
            gv.Columns["CreId"].Visible = false;
            gv.Columns["CreDt"].Visible = false;
            gv.Columns["ModId"].Visible = false;
            gv.Columns["ModDt"].Visible = false;
            gv.Columns["ChkImage"].Visible = false; //점검이미지 기능 작동 안해서 숨김처리함 (드림 게시) 240524

            if (Database.DbName.ToUpper().Contains("ALLTHEN"))
            {
                gv.Columns["Action_Result_Div_Cd"].Visible = false;
                gv.Columns["Action_Div_Cd"].Visible = false;
                gv.Columns["ActionMemo"].Visible = false;
                gv.Columns["Action_Emp_No"].Visible = false;
            }

            //Repository 셋팅

            gv.Columns["InspectDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "2040"), "분류코드", "분류명"); //설비점검기준
            gv.Columns["CycleDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204050"), "분류코드", "분류명"); // 관리주기
            gv.Columns["WeekDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204060"), "분류코드", "분류명"); // 관리요일
            gv.Columns["ValueTypeDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204070"), "분류코드", "분류명"); // 등록형태
            gv.Columns["ChargeEmpNo"].ColumnEdit = rpo.Create_RepositoryItem(code.GetDataTableUser(false), "코드", "명칭");  // 담당자
            gv.Columns["ActionEmpNo"].ColumnEdit = rpo.Create_RepositoryItem(code.GetDataTableUser(false), "코드", "명칭");  // 조치자
            gv.Columns["ActionResultDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204010"), "분류코드", "분류명"); //조치상황
            gv.Columns["ActionDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204020"), "분류코드", "분류명"); // 조치구분

            //점검결과 Inspect_Check   CheckEdit 설정
            rpoChk = rpo.Create_rpoChk();
            rpoChk.ValueChecked = "10";
            rpoChk.ValueUnchecked = "20";

            rpoTxt = rpo.Create_rpoTxt_Numeric();

            RepositoryItemCheckEdit chk_Ins = rpo.Create_rpoChk();
            chk_Ins.ValueChecked = "10";
            chk_Ins.ValueUnchecked = "20";

            gv.Columns["InspectCheck"].ColumnEdit = chk_Ins;

            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;
            RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
            Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Img.Buttons.RemoveAt(0);
            Btn_Img.Buttons.Add(Btn_Img_Move);
            //groupControl3.Visible = false;
            gv.RowClick += Event_IMG_Click;    //버튼 클릭 이벤트로 하면 grid focused를 할수없어서 gv클릭이벤트로 변경

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["InspectCheck"]);
            grid.Column_AllowEdit(gv.Columns["ChargeEmpNo"]);
            grid.Column_AllowEdit(gv.Columns["ActionResultDivCd"]);
            grid.Column_AllowEdit(gv.Columns["ActionDivCd"]);
            grid.Column_AllowEdit(gv.Columns["ActionMemo"]);
            grid.Column_AllowEdit(gv.Columns["ActionEmpNo"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //Row별 Repository 다르게하기
        }

        private void Set_Lup()
        {
            cls_Lookup clsLup = new cls_Lookup();

            //1. 조회조건

            //라인
            DataTable dt_line = code.GetLine_List("", true);
            clsLup.Create_RepositoryItemSearchLookUp(LupLine, dt_line, "라인코드", "라인명");

            //설비
            DataTable dt_machine = code.GetMachineList().ToTable();
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Machine, dt_machine, "설비코드", "설비명");

            if(dt_machine != null)
			{
                Lup_Machine.smtLookUpValue = dt_machine.Rows[0]["설비코드"].ToString();
            }

            //일별설비점검시간
            DataTable dt_time = item.SelectClassItemLookUp("1000", "204030");
            clsLup.Create_RepositoryItemSearchLookUp(Lup_InspTime, dt_time, "분류코드", "분류명");

            if (dt_time != null)
            {
                if (dt_time.Rows.Count > 0) Lup_InspTime.smtLookUpValue = dt_time.Rows[0]["분류코드"].ToString();
            }

            Lup_Machine.smtLookUpEditValueChanged += Lup_EditValueChanged;
        }

        private void Event_CellValueChagned_Num(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            //검사값이 관리범위를 벗어났는지 검사
            if(fieldNm == "Inspect_Check")
            {
                float MgntMin = dr["MgntMin"].ConvertInt_Null_To_Zero();
                float MgntMax = dr["MgntMax"].ConvertInt_Null_To_Zero();
                float curValue = dr[fieldNm].ConvertFloat_Null_To_Zero();

                //관리범위 오버 검사
                //상한만 있을때
                if (MgntMin == 0 && MgntMax > 0)
                {
                    if(curValue > MgntMax)
                    {
                        //값이 상한보다 높으면 불합 처리
                        dr["PassDivCd"] = "20";
                    }
                }
                //하한만 있을때
                else if (MgntMin > 0 && MgntMax == 0)
                {
                    if(curValue < MgntMin)
                    {
                        //값이 하한보다 낮으면 불합 처리
                        dr["PassDivCd"] = "20";
                    }
                }
                //상,하한 둘다 있을때
                else if (MgntMin > 0 && MgntMin < MgntMax)
                {
                    if (curValue < MgntMin || curValue > MgntMax)
                    {
                        //값이 하한보다 낮거나 상한보다 높으면 불합 처리
                        dr["PassDivCd"] = "20";
                    }
                }

            }
        }

        private void Event_GvCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            //점검항목 일정Row의 Repository다르게하기
            GridView gv = sender as GridView;
            if (e.RowHandle < 0) return;

            string fieldName = e.Column.FieldName;
            if (fieldName == "InspectCheck")
            {
                string type = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd"));

                if (type == "10")
                {
                    //양/불 선택일경우
                    e.RepositoryItem = rpoChk;
                }
                else if (type == "20")
                {
                    //측정치 입력일경우
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

            Search_Insp();
        }

		#endregion

		#region 이벤트

		private void Frm_Load(object sender, EventArgs e)
        {
            Search_Insp();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Gv_Insp.PostEditor();
            Gv_Insp.UpdateCurrentRow();

            smtSearchButton Btn = sender as smtSearchButton;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdDisplay": Search_Insp(); break;
                case "cmdSave": Save_Data(); break;
                case "cmdDelete": Delete(); break;
                case "cmdClose": this.Close(); break;
            }
        }

        private void Event_IMG_Click(object sender, RowClickEventArgs e)
        {
            if (Gv_Insp.RowCount == 0) return;

            // 더블 클릭된 행 가져오기
            GridView gv = sender as GridView;
            int rowHandle = e.RowHandle;

            string fieldName = gv.FocusedColumn.FieldName;

            if (fieldName != "Chk_Image") return;

            EQ_MachineChk mc = new EQ_MachineChk();
            mc.InspectDivCd = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "Inspect_Div_Cd"));
            mc.InspectDate = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "Inspect_Date"));
            mc.MachineCode = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "MachineCode"));
            mc.Seq = Convert.ToInt16(gv.GetRowCellValue(e.RowHandle, "Seq"));
            mc.ChkTimeDivCd = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "Chk_Time_Div_Cd"));

            DataRow dataRow = mc.Select_Machine_Chk_Image();

            if (dataRow == null || dataRow["Chk_Image"].ConvertString_Null_To_Empty() == "")
            {
                MessageBox.Show("등록된 이미지가 없습니다.");
                return;
            }

        }

        #endregion

        #region 데이터 조회


        private void Search_Insp()
		{

			//검사목록 불러오기

            string MachineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
            var Inspect_Date = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
            string Chk_Time_Div_Cd = Lup_InspTime.smtLookUpValue.ConvertString_Null_To_Empty();

            if (MachineCode == "")
			{
                MessageBox.Show("설비 미입력");
                return;
			}
            else if ( Chk_Time_Div_Cd == "" && Inspect_Div_Cd == "10")
            {
                MessageBox.Show("점검시간 미입력");
                return;
            }

            GetDay(); //설비정기점검시 관리요일 필요

            EQ_MachineChk mc = new EQ_MachineChk();
            mc.InspectDivCd = Inspect_Div_Cd; //10 : 일상점검, 20 : 정기점검
            mc.InspectDate = Inspect_Date;
            mc.MachineCode = MachineCode;
            mc.ChkTimeDivCd = Chk_Time_Div_Cd;

            if(WrGlobal.ProcOption["EQ"]["Machine_WeekDivUseYN"] == "Y")//정기점검 요일 사용여부
			{
				mc.WeekDivCd = WeekDivCd;
			}

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

                EQ_MachineChkStd mcs = new EQ_MachineChkStd();
                mcs.MachineCode = MachineCode;
                mcs.InspectDivCd = Inspect_Div_Cd;

                if (WrGlobal.ProcOption["EQ"]["Machine_WeekDivUseYN"] == "Y")//정기점검 요일 사용여부
                {
                    mcs.WeekDivCd = WeekDivCd;
                }

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

                string MachineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
                var Insp_Date = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char8();
                string Chk_Time_Div_Cd = Lup_InspTime.smtLookUpValue.ConvertString_Null_To_Empty();

                if (MachineCode == "" || dtInsp == null) return;

                EQ_MachineChk mc = new EQ_MachineChk();
                mc.MachineCode      = MachineCode;        //설비코드
                mc.InspectDate    = Insp_Date;         //점검일자
                mc.ChkTimeDivCd = Chk_Time_Div_Cd;   //설비점검시간
                mc.InspectDivCd  = Inspect_Div_Cd;    //점검구분 10:일상점검, 20:정기점검

                string valueType;
                foreach (DataRow dr in dtInsp.Rows)
                {
                    valueType = dr["ValueTypeDivCd"].ConvertString_Null_To_Empty();

                    mc.Seq                  = Convert.ToInt16(dr["Seq"]);                   //검사등록순번
                    if (valueType == "10") mc.InspectCheck = Convert.ToString(dr["InspectCheck"]) == "10" ? "Y" : "N";        //점검결과
                    else mc.InspectCheck = dr["InspectCheck"].ConvertString_Null_To_Empty();
                    mc.ChargeEmpNo         = Convert.ToString(dr["ChargeEmpNo"]);         //담당자
                    mc.ActionResultDivCd = Convert.ToString(dr["ActionResultDivCd"]); //조치진행 코드
                    mc.ActionDivCd        = Convert.ToString(dr["ActionDivCd"]);        //조치내역 코드
                    mc.ActionMemo          = Convert.ToString(dr["ActionMemo"]);          //조치내역
                    mc.ActionEmpNo        = Convert.ToString(dr["ActionEmpNo"]);        //조치자
                    //mc.Chk_Image = Convert.ToString(dr["Chk_Image"]);
                    mc.Remark = Convert.ToString(dr["Remark"]);                                   //비고

                    if(SaveDiv == "Ins")
                    {
                        if(!mc.Ins_InspectResult())
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

                if (Success) MessageBox.Show("저장이 정상적으로 완료되었습니다.");
                else         MessageBox.Show("저장 중 에러가 발생하였습니다.");

                Search_Insp();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion 데이터저장 End


        private void Delete()
        {
            DialogResult dialogResult = XtraMessageBox.Show("설비점검 등록 내역을 삭제하시겠습니까?", "설비점검 삭제", MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            EQ_MachineChk eq = new EQ_MachineChk();
            eq.InspectDivCd = Inspect_Div_Cd;
            eq.MachineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
            eq.ChkTimeDivCd = Lup_InspTime.smtLookUpValue.ConvertString_Null_To_Empty();

            if (Inspect_Div_Cd == "10")
			{
                eq.InspectDate = Dt_Date.smtStartDateTimeValue.ToString("yyyyMMdd");
                eq.Del_InspectResult();
            }
            else if(Inspect_Div_Cd == "20")
			{
                string yyyyMM = Dt_Date.smtStartDateTimeValue.ToString("yyyyMM");
                eq.Del_InspectResult_Routine(yyyyMM);
            }

            Search_Insp();

            XtraMessageBox.Show(cls_MessageBox.Del_Comp);
        }
    }
}
