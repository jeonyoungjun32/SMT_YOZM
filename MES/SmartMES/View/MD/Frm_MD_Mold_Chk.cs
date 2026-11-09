using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MD;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
    public partial class Frm_MD_Mold_Chk : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //점검항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //점검항목 Repository 숫자입력

        //10 : 일상점검
        //20 : 정기점검
        private string InspectDivCd = "10"; //검사구분
        string SaveDiv = "";
        string WeekDivCd = "";

        public Frm_MD_Mold_Chk()
        {
            InitializeComponent();
            SetDesign();
        }

        public Frm_MD_Mold_Chk(string Div_Cd)
        {
            InitializeComponent();

            switch (Div_Cd)
            {
                case "Daily": InspectDivCd = "10"; break;   //금일상점검
                case "Routine": InspectDivCd = "20"; break; //금정기점검
            }

            SetDesign();
        }

        private void Frm_MD_Mold_Chk_View_Load(object sender, EventArgs e)
        {
            Search();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //검사구분에따른 설정
            Set_TypeOption();

            Set_GridView();
            Set_SearchLookUp();
        }

        private void Set_TypeOption()
        {
            if (InspectDivCd == "10")
            {
                this.Text = "금형일상점검";
                lblLegend.Text = "범  례 : ○ 점검(양호) , △ 교체 , □ 수리(즉시 조치) ";
            }
            else if (InspectDivCd == "20")
            {
                this.Text = "금형정기점검";
                Lup_InspTime.Visible = false;
                layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void Set_SearchLookUp()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now);
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;
            Dt_Date.smtEndDateTimeVisible = false;

            cls_Lookup clsLup = new cls_Lookup();
            MD_Mold mold = new MD_Mold();

            DataTable dt_Mold = mold.Select_Mold_Mgmt_No(false);
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Mold, dt_Mold, "코드", "금형명");
            Lup_Mold.smtLookUpValue = dt_Mold.Rows[0]["코드"].ConvertString_Null_To_Empty();
            //Lup_Mold.smtLookUpValue = "ALL";

            CM_CateItem item = new CM_CateItem();
            DataTable dt_time = item.SelectClassItemLookUp(WrGlobal.CorpID, "MD4030");
            clsLup.Create_RepositoryItemSearchLookUp(Lup_InspTime, dt_time, "분류코드", "분류명");
            Lup_InspTime.smtLookUpValue = dt_time.Rows[0]["분류코드"].ConvertString_Null_To_Empty();
        }

        #region GridView Setting

        private void Set_GridView()
        {
            SetGridView(Gv_Insp);
        }

        private void SetGridView(GridView gv)
        {
            //합/불검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //Repository 셋팅
            CM_CateItem cm = new CM_CateItem();
            RepositoryItem lup_InspectDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD40"), "분류코드", "분류명");
            RepositoryItem lup_CycleDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4050"), "분류코드", "분류명");
            RepositoryItem lup_WeekDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4060"), "분류코드", "분류명");
            RepositoryItem lup_ValueTypeDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4070"), "분류코드", "분류명");
            RepositoryItem lup_ActionResultDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4010"), "분류코드", "분류명");
            RepositoryItem lup_ActionDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4020"), "분류코드", "분류명");

            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            MD_Mold MD_Mold = new MD_Mold();
            RepositoryItemSearchLookUpEdit lup_Mold = rpo.Create_RepositoryItemSearchLookUp(MD_Mold.Select_Mold_Mgmt_No(), "코드", "금형명");

            // 체크박스 설정
            rpoChk.ValueChecked = "Y";
            rpoChk.ValueUnchecked = "N";
            rpoChk.NullStyle = StyleIndeterminate.Unchecked;
            rpoChk.AllowGrayed = false;

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "점검구분", "InspectDivCd", lup_InspectDiv);
            grid.Create_GvColumn(gv, "금형", "MoldCode", lup_Mold);
            grid.Create_GvColumn(gv, "순번", "DispSeq");

            grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
            grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
            grid.Create_GvColumn(gv, "점검기준", "StandardSpec");
            grid.Create_GvColumn(gv, "점검주기", "CycleDivCd", lup_CycleDiv);
            grid.Create_GvColumn(gv, "관리요일", "WeekDivCd", lup_WeekDiv);
            grid.Create_GvColumn(gv, "등록형태", "ValueTypeDivCd", lup_ValueTypeDiv);
            grid.Create_GvColumn(gv, "점검결과", "InspectCheck");

            grid.Create_GvColumn(gv, "담당자", "ChargeEmpNo", lup_Emp);
            grid.Create_GvColumn(gv, "조치상황", "ActionResultDivCd", lup_ActionResultDiv);
            grid.Create_GvColumn(gv, "조치구분", "ActionDivCd", lup_ActionDiv);
            grid.Create_GvColumn(gv, "조치결과", "ActionMemo");
            grid.Create_GvColumn(gv, "조치자", "ActionEmpNo", lup_Emp);
            grid.Create_GvColumn(gv, "점검이미지", "ChkImage");
            grid.Create_GvColumn(gv, "최근점검Shot", "InspectShot");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //등록자, 수정자 column 숨김
            gv.Columns["MoldCode"].Visible = false;
            gv.Columns["ChkImage"].Visible = false;

            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;
            RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
            Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Img.Buttons.RemoveAt(0);
            Btn_Img.Buttons.Add(Btn_Img_Move);
            //groupControl3.Visible = false;
            gv.RowClick += Event_IMG_Click;    //버튼 클릭 이벤트로 하면 grid focused를 할수없어서 gv클릭이벤트로 변경
            gv.RowCellClick += Event_CellClick;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ChargeEmpNo"]);
            grid.Column_AllowEdit(gv.Columns["ActionResultDivCd"]);
            grid.Column_AllowEdit(gv.Columns["ActionDivCd"]);
            grid.Column_AllowEdit(gv.Columns["ActionMemo"]);
            grid.Column_AllowEdit(gv.Columns["ActionEmpNo"]);
            grid.Column_AllowEdit(gv.Columns["InspectCheck"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //Row별 Repository 다르게하기
            gv.CellValueChanged += Gv_CellValueChanged;
            gv.ShowingEditor += Gv_ShowingEditor;
        }

        private void Gv_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
        }

        private void Event_CellValueChagned_Num(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            //검사값이 관리범위를 벗어났는지 검사
            if (fieldNm == "InspectCheck")
            {
                float MgntMin = dr["MgntMin"].ConvertInt_Null_To_Zero();
                float MgntMax = dr["MgntMax"].ConvertInt_Null_To_Zero();
                float curValue = dr[fieldNm].ConvertFloat_Null_To_Zero();

                //관리범위 오버 검사
                //상한만 있을때
                if (MgntMin == 0 && MgntMax > 0)
                {
                    if (curValue > MgntMax)
                    {
                        //값이 상한보다 높으면 불합 처리
                        dr["PassDivCd"] = "20";
                    }
                }
                //하한만 있을때
                else if (MgntMin > 0 && MgntMax == 0)
                {
                    if (curValue < MgntMin)
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
                    CM_CateItem cm = new CM_CateItem();
                    RepositoryItem rpoCheck = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4080"), "분류코드", "분류명");

                    e.RepositoryItem = rpoCheck;
                    //e.Column.OptionsColumn.AllowEdit = false;
                }
                else if (type == "20")
                {
                    //측정치 입력일경우
                    e.RepositoryItem = rpoTxt;
                    //e.Column.OptionsColumn.AllowEdit = true;
                }
            }
        }

        private void Gv_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView gv = sender as GridView;

            if (gv.FocusedColumn.FieldName == "InspectCheck")
            {
                string type = Convert.ToString(gv.GetFocusedRowCellValue("ValueTypeDivCd"));

                if (type == "10")
                {
                    e.Cancel = true;
                }
            }
        }

        private void Event_IMG_Click(object sender, RowClickEventArgs e)
        {
            if (Gv_Insp.RowCount == 0) return;

            // 더블 클릭된 행 가져오기
            GridView gv = sender as GridView;
            int rowHandle = e.RowHandle;

            string fieldName = gv.FocusedColumn.FieldName;

            if (fieldName != "ChkImage") return;

            MD_MoldChk mc = new MD_MoldChk();
            mc.InspectDivCd = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "InspectDivCd"));
            mc.InspectDate = Convert.ToDateTime(gv.GetRowCellValue(e.RowHandle, "InspectDate")).ToString("yyyy-MM-dd");
            mc.MoldCode = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "MoldCode"));
            mc.Seq = Convert.ToInt16(gv.GetRowCellValue(e.RowHandle, "Seq"));
            mc.ChkTimeDivCd = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ChkTimeDivCd"));

            DataRow dataRow = mc.Select_Mold_ChkImage();
            if (dataRow == null || dataRow["ChkImage"].ConvertString_Null_To_Empty() == "")
            {
                MessageBox.Show("등록된 이미지가 없습니다.");
                return;
            }
        }

        private void Event_CellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            if (!e.Column.FieldName.ToString().Contains("InspectCheck")) return;

            string type = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd"));
            if (type == "20") return;

            string _Inspect_Check = e.CellValue.ConvertString_Null_To_Empty();
            _Inspect_Check = Change_Inspect_Chk(_Inspect_Check);

            gv.SetRowCellValue(e.RowHandle, e.Column, _Inspect_Check);
        }

        private string Change_Inspect_Chk(string Inspect_Chk)
        {
            string _Inspect_Chk = "";

            //점검("Y")→교체("C")→수리("R")→취소("") 순으로 값 변경
            switch (Inspect_Chk)
            {
                case "":
                    _Inspect_Chk = "10";
                    break;

                case "10":
                    _Inspect_Chk = "20";
                    break;

                case "20":
                    _Inspect_Chk = "30";
                    break;

                case "30":
                    _Inspect_Chk = "";
                    break;

                default:
                    break;
            }

            return _Inspect_Chk;
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = sender as SimpleButton;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "Btn_Search": Search(); break;
                case "Btn_Save": Save(); break;
            }
        }

        #region 데이터 조회

        private void Search()
        {
            //검사목록 불러오기
            string MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();
            var InspectDate = Dt_Date.smtStartDateTimeValue.ToString("yyyyMMdd");
            string ChkTimeDivCd = Lup_InspTime.smtLookUpValue.ConvertString_Null_To_Empty();

            if (MoldCode == "")
            {
                MessageBox.Show("금형 미입력");
                return;
            }
            //else if ( ChkTimeDivCd == "" && InspectDivCd == "10")
            //{
            //    MessageBox.Show("점검시간 미입력");
            //    return;
            //}

            GetDay(); //정기점검시 관리요일 필요

            MD_MoldChk mc = new MD_MoldChk();
            mc.InspectDivCd = InspectDivCd; //10 : 일상점검, 20 : 정기점검
            mc.InspectDate = InspectDate;
            mc.MoldCode = MoldCode;
            mc.WeekDivCd = WeekDivCd;
            mc.ChkTimeDivCd = ChkTimeDivCd;

            DataTable dtInsp = mc.Sel_Mold_Chk();
            if (dtInsp == null) return;

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

                MD_MoldChk_Std mcs = new MD_MoldChk_Std();
                mcs.MoldCode = MoldCode;
                mcs.InspectDivCd = InspectDivCd;
                mcs.WeekDivCd = WeekDivCd;

                Gc_Insp.DataSource = mcs.Select_Mold_Chk_List();
            }

            Gv_Insp.BestFitColumns();
        }

        public void GetDay()
        {
            //DateTime nowDt = DateTime.Now;
            DateTime nowDt = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue.ToString());

            switch (nowDt.DayOfWeek)
            {
                case DayOfWeek.Monday: WeekDivCd = "10"; break;
                case DayOfWeek.Tuesday: WeekDivCd = "20"; break;
                case DayOfWeek.Wednesday: WeekDivCd = "30"; break;
                case DayOfWeek.Thursday: WeekDivCd = "40"; break;
                case DayOfWeek.Friday: WeekDivCd = "50"; break;
                case DayOfWeek.Saturday: WeekDivCd = "60"; break;
                case DayOfWeek.Sunday: WeekDivCd = "70"; break;
            }
        }

        private void Lup_EditValueChanged(object sender, EventArgs e)
        {
            Search();
        }

        #endregion 데이터 조회 End

        #region 데이터 저장

        private void Save()
        {
            GridView gv = Gv_Insp;
            gv.PostEditor();
            gv.UpdateCurrentRow();

            try
            {
                DataTable dtInsp = Gc_Insp.DataSource as DataTable;

                string MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();
                var Insp_Date = Dt_Date.smtStartDateTimeValue.ToString("yyyyMMdd");
                string ChkTimeDivCd = Lup_InspTime.smtLookUpValue.ConvertString_Null_To_Empty();

                if (MoldCode == "" || dtInsp == null) return;

                int checkCnt = 0;

                MD_MoldChk mc = new MD_MoldChk();
                mc.MoldCode = MoldCode;        //금코드
                mc.InspectDate = Insp_Date;         //점검일자
                mc.ChkTimeDivCd = ChkTimeDivCd;   //금점검시간
                mc.InspectDivCd = InspectDivCd;    //점검구분 10:일상점검, 20:정기점검

                if (Insp_Date == "00010101")
                {
                    MessageBox.Show("조회일자를 입력해주세요.");
                    return;
                }
                if (ChkTimeDivCd == "" && InspectDivCd == "10")
                {
                    MessageBox.Show("점검시간 미입력");
                    return;
                }

                foreach (DataRow dr in dtInsp.Rows)
                {
                    string status = Convert.ToString(dr["Status"]);

                    if (status != "")
                    {
                        checkCnt++;
                    }

                    mc.MoldCode = Convert.ToString(dr["MoldCode"]);
                    mc.Seq = Convert.ToInt16(dr["Seq"]);                   //검사등록순번
                    mc.InspectCheck = Convert.ToString(dr["InspectCheck"]);        //점검결과
                    mc.ChargeEmpNo = Convert.ToString(dr["ChargeEmpNo"]);         //담당자
                    mc.ActionResultDivCd = Convert.ToString(dr["ActionResultDivCd"]); //조치진행 코드
                    mc.ActionDivCd = Convert.ToString(dr["ActionDivCd"]);        //조치내역 코드
                    mc.ActionMemo = Convert.ToString(dr["ActionMemo"]);          //조치내역
                    mc.ActionEmpNo = Convert.ToString(dr["ActionEmpNo"]);        //조치자
                    //mc.ChkImage = Convert.ToString(dr["ChkImage"]);
                    mc.Remark = Convert.ToString(dr["Remark"]);                                   //비고

                    if (SaveDiv == "Ins")
                    {
                        if (!mc.Ins_Mold_Chk())
                        {
                            //저장 중 에러 발생시
                            mc.Del_Mold_Chk();
                            MessageBox.Show("저장 중 에러가 발생하였습니다.");
                            break;
                        }
                    }

                    else if (SaveDiv == "Upd")
                    {
                        if (!mc.Upd_Mold_Chk())
                        {
                            //저장 중 에러 발생시
                            MessageBox.Show("저장 중 에러가 발생하였습니다.");
                            break;
                        }
                    }

                    dr["Status"] = "";
                }

                if (checkCnt == 0) return;

                MessageBox.Show("저장이 정상적으로 완료되었습니다.");

                Search();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion 데이터저장 End

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_Insp);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}