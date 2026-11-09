using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MD;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
    public partial class Frm_MD_MoldChk_Daily : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        bool Success = true;
        string _Date = "";
        string _InspTime = "";
        string _Machine_Cd = "";
        string _saveType = "";
        string _SearchDate = "";
        string ViewType = "";

        public Frm_MD_MoldChk_Daily()
        {
            InitializeComponent();
        }

        public Frm_MD_MoldChk_Daily(string Div_Cd)
        {
            InitializeComponent();

            switch (Div_Cd)
            {
                case "View": ViewType = "View"; break;
            }
        }

        private void Frm_MD_MoldChk_Daily_View_Load(object sender, EventArgs e)
        {
            pictureEdit1.Properties.ExportMode = ExportMode.Value;
            pictureEdit1.Properties.PictureStoreMode = PictureStoreMode.ByteArray;

            Lup_Mold.Visible = false;
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (ViewType == "View")
            {
                Lup_Mold.Visible = true;
                Lup_MeasureTime.Visible = false;
                layoutControlGroup4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                Btn_Confirm.Visible = false;
            }

            //조회조건
            Set_SearchLookUp();
            //그리드
            Set_GridView();
            // 하단 고정 텍스트
            Set_TxtEdit();

            SearchMoldList();
        }

        #region 디자인 아이템 생성

        private void Set_SearchLookUp()
        {
            barEditDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            barEditDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            barEditDate.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.All;
            DateTime date = Convert.ToDateTime(DateTime.Today.Date.ToString("yyyy-MM-dd"));
            barEditDate.EditValue = date;

            if (ViewType == "View") {
                lblDate.Text = "기준년월";

                barEditDate.Properties.Mask.EditMask = "yyyy-MM";
                barEditDate.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
            }

            cls_Lookup clsLup = new cls_Lookup();
            MD_Mold mold = new MD_Mold();

            DataTable dt_Mold = mold.Select_Mold_Mgmt_No(true);
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Mold, dt_Mold, "코드", "금형명");
            Lup_Mold.smtLookUpValue = "ALL";

            CM_CateItem item = new CM_CateItem();
            DataTable dt_time = item.SelectClassItemLookUp(WrGlobal.CorpID, "MD4030");
            clsLup.Create_RepositoryItemSearchLookUp(Lup_MeasureTime, dt_time, "분류코드", "분류명");

        }

        private void Set_TxtEdit()
        {
            MemoEdit memo = Lbl_FixInfo;

            memo.Properties.ReadOnly = true;
            memo.Properties.WordWrap = true;
            memo.Properties.ScrollBars = ScrollBars.None;

            memo.Font = new Font("맑은 고딕", 13f, FontStyle.Bold);
            memo.Text =
                "※ 금형 교환 전, 후 일상점검 진행\r\n" +
                "※ 이상 발생시 관리자 및 금형팀 통보 및 조치\r\n" +
                "※ 점검후 양호한 금형은 지정된 장소로 이동 보관\r\n\r\n" +
                "범  례 : ○ 점검(양호) , △ 교체 , □ 수리(즉시 조치)";
        }

        #region GridView Setting

        private void Set_GridView()
        {
            Set_GridList(Gv_MoldList);
            Set_Grid(Gv_Daily_Chk);
        }

        private void Set_GridList(GridView gv)
        {
            gv.Columns.Clear();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            MD_Mold MD_Mold = new MD_Mold();
            RepositoryItemSearchLookUpEdit lup_Mold = rpo.Create_RepositoryItemSearchLookUp(MD_Mold.Select_Mold_Mgmt_No(), "코드", "금형명");

            //grid.Create_GvColumn(gv, "점검구분", "InspectDivCd");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldCode", lup_Mold);
            grid.Create_GvColumn(gv, "차수", "chasu");
            grid.Create_GvColumn(gv, "특수부품", "SpecialParts");
            grid.Create_GvColumn(gv, "누적타수", "AccumulateShot");

            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

        private void Set_Grid(GridView gv)
        {
            gv.Columns.Clear();

            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            MD_Mold MD_Mold = new MD_Mold();
            RepositoryItemSearchLookUpEdit lup_Mold = rpo.Create_RepositoryItemSearchLookUp(MD_Mold.Select_Mold_Mgmt_No(), "코드", "금형명");

            CM_Cust cust = new CM_Cust();
            RepositoryItemSearchLookUpEdit lup_Cust = rpo.Create_RepositoryItemSearchLookUp(cust.Select_Cust_List(new string[] { }), "코드", "명칭");

            //grid.Create_GvColumn(gv, "점검구분", "InspectDivCd");
            grid.Create_GvColumn(gv, "순번", "Seq");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldCode", lup_Mold);
            grid.Create_GvColumn(gv, "차수", "Chasu");
            grid.Create_GvColumn(gv, "고객사코드", "SupplyCust", lup_Cust);
            grid.Create_GvColumn(gv, "제작처코드", "ProdtCust");
            grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
            grid.Create_GvColumn(gv, "점검기준", "StandardSpec");
            grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
            string fieldNm = "";
            string caption = "";

            if (ViewType == "") // 등록화면
            {
                //string day = string.Format("{0:D2}", Convert.ToDateTime(Dt_Date.smtStartDateTimeValue).Day);
                string day = string.Format("{0:D2}", barEditDate.DateTime.Day);
                caption = day + ("일");
                grid.Create_GvColumn(gv, caption, caption);
                gv.Columns[caption].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            else // 현황화면
            {
                DateTime dt = Convert.ToDateTime(barEditDate.EditValue);
                int lastDay = DateTime.DaysInMonth(dt.Year, dt.Month);
                for (int i = 1; i <= lastDay; i++)
                {
                    fieldNm = i.ToString("D2");
                    caption = fieldNm + ("일");

                    grid.Create_GvColumn(gv, caption, caption);

                    gv.Columns[caption].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }
            }
            //Visible =false
            gv.Columns["Seq"].Visible = false;
            gv.Columns["SupplyCust"].Visible = false;
            gv.Columns["ProdtCust"].Visible = false;
            //gv.Columns["chasu"].Visible = false;

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            Gv_Daily_Chk.BestFitColumns();

            //이벤트
            //grid.RowNumber_Indicator(gv);     //행번호 표기
            if (ViewType == "") gv.RowCellClick -= Event_CellClick;  //셀값 수정 Event
            if (ViewType == "") gv.RowCellClick += Event_CellClick;  //셀값 수정 Event

            //Merge
            gv.OptionsView.AllowCellMerge = true;                           // Merge허용
            gv.CellMerge -= Gv_CellMerge;   //셀병합 예외처리
            gv.CellMerge += Gv_CellMerge;   //셀병합 예외처리
        }

        private void Gv_CellMerge(object sender, CellMergeEventArgs e)
        {
            //셀 병합시 예외처리
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;

            //머지 활성화
            if (fieldNm == "MoldSN"
                || fieldNm == "MoldCode"
                || fieldNm == "Chasu"
                || fieldNm == "SupplyCust"
                || fieldNm == "ProdtCust"
                )
            {
                string v1 = Convert.ToString(e.CellValue1);
                string v2 = Convert.ToString(e.CellValue2);

                e.Merge = (v1 == v2);
                e.Handled = true;
            }
            else
            {
                // 그 외 컬럼은 병합 금지
                e.Merge = false;
                e.Handled = true;
            }
        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            Search();
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region 조회

        private void SearchMoldList()
        {
            //SC_Corp corp = new SC_Corp();
            //DataTable dt = corp.Select_SC_Corp();
            //pictureEdit1.EditValue = dt.Rows[0]?["RoutineImg1"];

            MD_Mold mold = new MD_Mold();
            string date = Convert.ToDateTime(barEditDate.EditValue).ToString("yyyyMMdd");
            Gc_Daily_Chk.DataSource = null;
            Gc_MoldList.DataSource = mold.Select_MoldList(date);
            Gv_MoldList.BestFitColumns();
        }

        private void Search()
        {
            string MoldCode = string.Empty;

            if (ViewType == "View")
            {
                MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();
            }
            else
            {
                DataRow dr = Gv_MoldList.GetFocusedDataRow();
                MoldCode = Convert.ToString(dr["MoldCode"]);
            }

            string date = Convert.ToDateTime(barEditDate.EditValue).ToString("yyyyMM");
            this._SearchDate = barEditDate.DateTime.ToString("yyyyMMdd");
            if (_SearchDate == "00010101")
            {
                MessageBox.Show("조회기간을 입력해주세요.");
                Gc_Daily_Chk.DataSource = null;
                return;
            }
            //string MoldCode = Convert.ToString(dr["MoldCode"]);
            //string MoldCode = LupMold.EditValue.ConvertString_Null_To_Empty() == "ALL"?"": LupMold.EditValue.ConvertString_Null_To_Empty();
            string chk_time_div_cd = Lup_MeasureTime.smtLookUpValue.ConvertString_Null_To_Empty();
            if (chk_time_div_cd == "")
            {
                MessageBox.Show("점검시간 미입력");
                return;
            }

            MD_MoldChk mc = new MD_MoldChk();
            //mc.InspectDivCd = InspectDivCd; //10 : 일상점검, 20 : 정기점검
            mc.InspectDivCd = "10"; //10 : 일상점검, 20 : 정기점검
            mc.InspectDate = date;
            mc.MoldCode = MoldCode;
            mc.ChkTimeDivCd = chk_time_div_cd;
            string Month = Convert.ToDateTime(barEditDate.EditValue).ToString("MM");

            DataTable dt = mc.Select_Mold_ChkView_upd(barEditDate.DateTime);
            if (dt.Rows.Count < 1)
            {
                Gc_Daily_Chk.DataSource = mc.Select_Mold_ChkView_Ins(Month);
                _saveType = "Insert";
            }
            else
            {
                //Gc_Daily_Chk.DataSource = mc.Select_Mold_ChkView_Ins(Month);
                Gc_Daily_Chk.DataSource = dt;
                _saveType = "Update";
                mEdit_Rmk.Text = dt.Rows[0]["Remark"].ToString();
            }
            Gv_Daily_Chk.BestFitColumns();

            // 조치사항
            string fromdate = Convert.ToDateTime(barEditDate.EditValue).ToString("yyyyMMdd");
            string todate = Convert.ToDateTime(barEditDate.EditValue).ToString("yyyyMMdd");

            MD_MoldChk_Rmk rmk = new MD_MoldChk_Rmk();
            rmk.InspectDivCd = "10";
            rmk.MoldCode = MoldCode;
            rmk.ChkTimeDivCd = chk_time_div_cd;

            DataRow drRmk = rmk.Select_MoldChk_Rmk(fromdate, todate);
            if (drRmk != null)
            {
                mEdit_Rmk.Text = drRmk["Remark"].ConvertString_Null_To_Empty();
            }
            else
            {
                mEdit_Rmk.Text = string.Empty;
            }
        }

        private void Confirm_ALL()
        {
            DataTable dt_Bool = Gc_Daily_Chk.DataSource as DataTable;
            GridView gv_Num = Gv_Daily_Chk;

            string field_Nm = "";

            if (dt_Bool == null) return;
            // 정성 OK 셋팅
            if (dt_Bool.Rows.Count > 0)
            {
                foreach (DataColumn col in dt_Bool.Columns)
                {
                    field_Nm = col.ColumnName;

                    if (field_Nm.EndsWith("일"))
                    {
                        foreach (DataRow dr in dt_Bool.Rows)
                        {
                            dr[field_Nm] = "10";
                        }
                    }
                }
            }

            Save_Chk();
        }

        #endregion

        #region Event
        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;

            switch (btn.Name)
            {
                case "Btn_Search":
                    if (ViewType == "View")
                        Search();
                    else
                        SearchMoldList();

                    break;

                case "BtnSave":
                    SearchMoldList();
                    break;

                case "Btn_Confirm":
                    Confirm_ALL();
                    break;
            }
        }

        private void Event_CellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            if (!e.Column.FieldName.ToString().Contains("일")) return;
            if (dr[e.Column.FieldName].ToString().Trim() == "") { _saveType = "Insert"; };
            if (dr[e.Column.FieldName].ToString().Trim() != "") { _saveType = "Update"; };

            string Day = e.Column.FieldName.Substring(0, 2);
            _Date = Convert.ToDateTime(barEditDate.EditValue).ToString("yyyyMM");
            _InspTime = Lup_MeasureTime.smtLookUpValue.ConvertString_Null_To_Empty();
            _Machine_Cd = dr["MoldCode"].ToString();
            string _Inspect_Check = e.CellValue.ConvertString_Null_To_Empty();

            // 점검(○)→교체(△)→수리(□)→취소 순으로 값 변경
            _Inspect_Check = Change_Inspect_Chk(_Inspect_Check);
            if (_Inspect_Check == "") { _saveType = "Delete"; };

            MD_MoldChk mc = new MD_MoldChk();

            mc.InspectDivCd = "10";                     //점검구분 10:일상점검, 20:정기점검
            mc.Seq = Convert.ToInt16(dr["Seq"]);          //검사등록순번
            mc.InspectDate = _Date + Day;                      //검사일자
            mc.ChkTimeDivCd = _InspTime;               //일별점검시간 공통코드: 10-08:00, 20-20:00

            mc.InspectCheck = _Inspect_Check;                       //점검결과
            mc.ChargeEmpNo = "";                         //담당자
            mc.ActionResultDivCd = "";                 //조치진행 코드
            mc.ActionDivCd = "";                        //조치내역 코드
            mc.ActionMemo = "";                           //조치내역
            mc.ActionEmpNo = "";                        //조치자;
            mc.Remark = "";                                  //비고

            //검사한 항목일경우
            if (_saveType == "Update")
            {
                mc.MoldCode = _Machine_Cd;  //설비코드

                if (!mc.Upd_Mold_Chk())
                {
                    //저장 중 에러 발생시
                    Success = false;
                }
            }
            //미검사 항목일 경우
            else if (_saveType == "Insert")
            {
                mc.MoldCode = _Machine_Cd;  //설비코드
                if (!mc.Ins_Mold_Chk())
                {
                    Success = false;
                }
            }
            //삭제
            else if (_saveType == "Delete")
            {
                mc.MoldCode = _Machine_Cd;  //설비코드
                if (!mc.Del_Mold_Chk())
                {
                    Success = false;
                }
            }

            //선택한 행 번호 저장
            int SaveFocusedRowInx = Gv_Daily_Chk.FocusedRowHandle;

            Search();

            //저장 후 해당 행으로 focus이동
            Gv_Daily_Chk.FocusedRowHandle = SaveFocusedRowInx;
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

                case "○":
                    _Inspect_Chk = "20";
                    break;

                case "△":
                    _Inspect_Chk = "30";
                    break;

                case "□":
                    _Inspect_Chk = "";
                    break;

                default:
                    break;
            }

            return _Inspect_Chk;
        }

        private string Get_Inspect_Chk(string Inspect_Chk)
        {
            string _Inspect_Chk = "";

            switch (Inspect_Chk)
            {
                case "○":       // 점검
                    _Inspect_Chk = "Y";
                    break;

                case "△":       // 완료
                    _Inspect_Chk = "C";
                    break;

                case "□":       // 수리
                    _Inspect_Chk = "R";
                    break;

                default:
                    break;
            }

            return _Inspect_Chk;
        }

        private void lupChange_Search(object sender, EventArgs e)
        {
            SearchMoldList();
            Set_Grid(Gv_Daily_Chk);
        }

        #endregion

        #region 저장
        private void Save_Chk()
        {
            if (Gv_MoldList.RowCount == 0) return;

            GridView gv = Gv_Daily_Chk;
            if (gv == null) return;
            if (gv.RowCount == 0) return;

            MD_MoldChk mc = new MD_MoldChk();
            mc.MoldCode = Gv_MoldList.GetFocusedRowCellValue("MoldCode").ConvertString_Null_To_Empty();
            mc.InspectDivCd = "10";                     //점검구분 10:일상점검, 20:정기점검
            mc.InspectDate = _SearchDate;       //검사일자
            mc.ChkTimeDivCd = Lup_MeasureTime.smtLookUpValue.ConvertString_Null_To_Empty();               //일별점검시간 공통코드: 10-08:00, 20-20:00

            // 해당일자, 해당금형의 모든 검사내역 삭제
            if (!mc.Del_Mold_Chk_ByMoldCode())
            {
                MessageBox.Show("저장중 오류가 발생했습니다.");
                return;
            }

            for (int i = 0; i < gv.RowCount; i++)
            {
                mc.Seq = Convert.ToInt16(gv.GetRowCellValue(i, "Seq").ConvertInt_Null_To_Zero());          //검사등록순번
                mc.InspectCheck = "10";                       //점검결과
                mc.ChargeEmpNo = "";                         //담당자
                mc.ActionResultDivCd = "";                 //조치진행 코드
                mc.ActionDivCd = "";                        //조치내역 코드
                mc.ActionMemo = "";                           //조치내역
                mc.ActionEmpNo = "";                        //조치자;
                mc.Remark = "";                                  //비고

                if (!mc.Ins_Mold_Chk())
                {
                    MessageBox.Show("저장중 오류가 발생했습니다.");
                    return;
                }
            }

            //선택한 행 번호 저장
            int SaveFocusedRowInx = Gv_Daily_Chk.FocusedRowHandle;

            Search();

            //저장 후 해당 행으로 focus이동
            Gv_Daily_Chk.FocusedRowHandle = SaveFocusedRowInx;
        }
        #endregion

        private void BtnSave_Img_Click(object sender, EventArgs e)
        {
            //byte[] imageL = pictureEdit1.EditValue as byte[];
            //
            //SC_Corp corp = new SC_Corp();
            //corp.RoutineImg1 = imageL;
            //
            //if (!corp.Upd_RoutineImg1())
            //{
            //    {
            //        MessageBox.Show("이미지저장 중 에러가 발생하였습니다.");
            //    }
            //}
        }

        private void BtnSave_Rmk_Click(object sender, EventArgs e)
        {
            DataRow dr = Gv_MoldList.GetFocusedDataRow();
            if (dr == null)
            {
                MessageBox.Show("선택된 데이터가 없습니다.");
                return;
            }

            string moldCode = Convert.ToString(dr["MoldCode"]);
            string fromdate = Convert.ToDateTime(barEditDate.EditValue).ToString("yyyyMMdd");
            string todate = Convert.ToDateTime(barEditDate.EditValue).ToString("yyyyMMdd");

            MD_MoldChk_Rmk rmk = new MD_MoldChk_Rmk();
            rmk.MoldCode = moldCode;
            rmk.InspectDate = fromdate;
            rmk.InspectDivCd = "10";
            rmk.ChkTimeDivCd = Lup_MeasureTime.smtLookUpValue.ConvertString_Null_To_Empty();
            rmk.Remark = mEdit_Rmk.Text;

            DataTable rmkdt = rmk.Sel_MoldChk_Rmk(fromdate, todate);

            if (rmkdt != null && rmkdt.Rows.Count > 0)
            {
                if (!rmk.Upd_MoldChk_Rmk(fromdate, todate))
                {
                    MessageBox.Show("저장중 오류가 발생했습니다.");
                    return;
                }
            }
            else
            {
                if (!rmk.Ins_MoldChk_Rmk())
                {
                    MessageBox.Show("저장중 오류가 발생했습니다.");
                    return;
                }
            }

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_Daily_Chk);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}