using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MD;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
    public partial class Frm_MD_Mold_ChkView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //점검항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //점검항목 Repository 숫자입력

        //10 : 일상점검
        //20 : 정기점검
        private string InspectDivCd = "10"; //검사구분
        string WeekDivCd = "";

        public Frm_MD_Mold_ChkView()
        {
            InitializeComponent();

            SetDesign();
        }

        public Frm_MD_Mold_ChkView(string Div_Cd)
        {
            InitializeComponent();

            switch (Div_Cd)
            {
                case "Daily": InspectDivCd = "10"; break;   //금일상점검
                case "Routine": InspectDivCd = "20"; break; //금정기점검
            }

            SetDesign();
        }

        private void Frm_MD_Mold_ChkView_View_Load(object sender, EventArgs e)
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
                this.Text = "금형일상점검현황";
            }
            else if (InspectDivCd == "20")
            {
                this.Text = "금형정기점검현황";
                Lup_InspTime.Visible = false;
            }
        }

        private void Set_SearchLookUp()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            cls_Lookup clsLup = new cls_Lookup();
            MD_Mold mold = new MD_Mold();

            DataTable dt_Mold = mold.Select_Mold_Mgmt_No(true);
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Mold, dt_Mold, "코드", "금형명");
            Lup_Mold.smtLookUpValue = "ALL";

            CM_CateItem item = new CM_CateItem();
            DataTable dt_time = item.SelectClassItemLookUp(WrGlobal.CorpID, "MD4030");
            clsLup.Create_RepositoryItemSearchLookUp(Lup_InspTime, dt_time, "분류코드", "분류명");
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

            MD_Mold MD_Mold = new MD_Mold();
            RepositoryItemSearchLookUpEdit lup_Mold = rpo.Create_RepositoryItemSearchLookUp(MD_Mold.Select_Mold_Mgmt_No(), "코드", "금형명");

            CM_CateItem cm = new CM_CateItem();
            RepositoryItem lup_InspectDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD40"), "분류코드", "분류명");
            RepositoryItem lup_CycleDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4050"), "분류코드", "분류명");
            RepositoryItem lup_ChkTimeDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4030"), "분류코드", "분류명");
            RepositoryItem lup_WeekDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4060"), "분류코드", "분류명");
            RepositoryItem lup_ValueTypeDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4070"), "분류코드", "분류명");
            RepositoryItem lup_ActionResultDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4010"), "분류코드", "분류명");
            RepositoryItem lup_ActionDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD4020"), "분류코드", "분류명");

            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            // 체크박스 설정
            rpoChk.ValueChecked = "Y";
            rpoChk.ValueUnchecked = "N";
            rpoChk.NullStyle = StyleIndeterminate.Unchecked;
            rpoChk.AllowGrayed = false;

            grid.Create_GvColumn(gv, "점검일자", "InspectDate");
            grid.Create_GvColumn(gv, "점검구분", "InspectDivCd", lup_InspectDiv);
            grid.Create_GvColumn(gv, "점검시간", "ChkTimeDivCd", lup_ChkTimeDiv);
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "금형관리번호", "MoldMgmtNo");
            grid.Create_GvColumn(gv, "금형명", "MoldName");
            //grid.Create_GvColumn(gv, "순번", "Seq");

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

            grid.SetCol_Type(gv.Columns["InspectDate"], clsGrid.TypeList.TDate);

            gv.Columns["ChkImage"].Visible = false;

            if (InspectDivCd == "20")
            {
                gv.Columns["ChkTimeDivCd"].Visible = false;
            }

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;
            RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
            Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Img.Buttons.RemoveAt(0);
            Btn_Img.Buttons.Add(Btn_Img_Move);

            gv.RowClick += Event_IMG_Click;    //버튼 클릭 이벤트로 하면 grid focused를 할수없어서 gv클릭이벤트로 변경
            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //Row별 Repository 다르게하기
            gv.CellValueChanged += Gv_CellValueChanged;
        }

        private void Gv_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            grid.Change_Status(gv, e.RowHandle);

            gv.BestFitColumns();
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
                }
                else if (type == "20")
                {
                    //측정치 입력일경우
                    e.RepositoryItem = rpoTxt;
                }
            }
        }

        private void Event_IMG_Click(object sender, RowClickEventArgs e)
        {
            if (Gv_Insp.RowCount == 0) return;

            // 더블 클릭된 행 가져오기
            GridView gv = sender as GridView;
            if (gv == null) return;
            if (gv.FocusedColumn == null) return;

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

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = sender as SimpleButton;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "Btn_Search": Search(); break;
            }
        }

        #region 데이터 조회

        private void Search()
        {
            //검사목록 불러오기
            string MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();
            string FromDate = Dt_Date.smtStartDateTimeValue.ToString("yyyyMMdd");
            string ToDate = Dt_Date.smtEndDateTimeValue.ToString("yyyyMMdd");
            //string ChkTimeDivCd = Lup_InspTime.smtLookUpValue.ConvertString_Null_To_Empty();

            MD_MoldChk mc = new MD_MoldChk();
            mc.InspectDivCd = InspectDivCd; //10 : 일상점검, 20 : 정기점검
            mc.MoldCode = MoldCode;
            mc.WeekDivCd = WeekDivCd;
            //mc.ChkTimeDivCd = ChkTimeDivCd;

            DataTable dtInsp = mc.Sel_Mold_ChkView(FromDate, ToDate);
            if (dtInsp == null) return;

            Gc_Insp.DataSource = dtInsp;

            Gv_Insp.BestFitColumns();
        }

        private void Lup_EditValueChanged(object sender, EventArgs e)
        {
            Search();
        }

        #endregion 데이터 조회 End

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