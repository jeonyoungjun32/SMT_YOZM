using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.EQ;
using System;
using System.Data;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_Machine_DailyCheck : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        CM_CateItem item = new CM_CateItem();

        bool Success = true;
        string _Date = "";
        string _InspTime = "";
        string _Muchine_Cd = "";
        string _saveType = "";

        public Frm_EQ_Machine_DailyCheck()
        {
            InitializeComponent();
        }

        private void Frm_QT_Bad_ChkView_Load(object sender, EventArgs e)
        {
            //조회조건
            Set_SearchLup();
            //그리드
            Set_Grid(Gv_Daily_Chk);

            Search();

            //조회조건 변경 시 자동 재조회 되도록
            this.Lup_InspTime.smtLookUpEditValueChanged += new System.EventHandler(this.lupChange_Search);
            this.Lup_Machine.smtLookUpEditValueChanged += new System.EventHandler(this.lupChange_Search);
        }

        private void Set_SearchLup()
        {
            cls_Lookup clsLup = new cls_Lookup();

            //기간
            Dt_Date.smtStartDateTimeValue = DateTime.Now;

            //DtpDate.Properties.DisplayFormat.FormatString = "yyyy-MM";
            //DtpDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            //DtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //DtpDate.Properties.Mask.EditMask = "yyyy-MM";

            //설비
            DataTable dt_machine = code.GetMachineList().ToTable();
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Machine, dt_machine, "설비코드", "설비명");

            if (dt_machine != null && dt_machine.Rows.Count > 0)
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
        }

        private void Set_Grid(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //설비명
            RepositoryItemSearchLookUpEdit lup_MachineCode = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");

            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명칭", "Machine_Nm", lup_MachineCode);
            grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
            grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
            grid.Create_GvColumn(gv, "점검기준", "StandardSpec");

            string fieldNm = "";
            string caption = "";

            for (int i = 1; i <= 31; i++)
            {
                fieldNm = i.ToString("D2");
                caption = fieldNm + ("일");

                grid.Create_GvColumn(gv, caption, caption);

                gv.Columns[caption].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }

            if (WrGlobal.LoginID != "Dev")
            {
                gv.Columns["MachineCode"].Visible = false;
            }

            //셀값 변경 허용
            foreach (GridColumn col in gv.Columns)
            {
                grid.Column_AllowEdit(col, false);
            }

            //이벤트
            //grid.RowNumber_Indicator(gv);     //행번호 표기
            gv.RowCellClick += Event_CellClick;  //셀값 수정 Event

        }
        #region 조회
        private void Search()
        {
            string date = Dt_Date.smtStartDateTimeValue.ToString("yyyyMM");
            string MachineCode = Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL"?"": Lup_Machine.smtLookUpValue.ConvertString_Null_To_Empty();
            string chk_time_div_cd = Lup_InspTime.smtLookUpValue.ConvertString_Null_To_Empty();

            if(MachineCode == "")
            {
                XtraMessageBox.Show("설비 미선택", "조회조건 미선택");
                return;
            }

            EQ_MachineChk machine_Chk = new EQ_MachineChk();
            machine_Chk.InspectDate = date;
            machine_Chk.InspectDivCd ="10";
            machine_Chk.ChkTimeDivCd = chk_time_div_cd;
            machine_Chk.MachineCode = MachineCode;
            string Month = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue).ToString("MM");

            DataTable dt = machine_Chk.Select_Machine_ChkView_upd(Month);
            if (dt.Rows.Count < 1)
            {
                Gc_Daily_Chk.DataSource = machine_Chk.Select_Machine_ChkView_Ins(Month);
                _saveType = "Insert";
            }
            else
            {
                Gc_Daily_Chk.DataSource = dt;
                _saveType = "Update";
            }

            Gv_Daily_Chk.BestFitColumns();
        }
        #endregion

        #region Event
        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;

            switch(btn.Name)
            {
                case "cmdDisplay":
                    Search();
                    break;
                case "cmdClose":
                    this.Close();
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
            if (dr[e.Column.FieldName].ToString().Trim() !=  "") { _saveType = "Delete"; };

            //int targetRow = e.RowHandle;
            //string ManagementItem = dr["ManagementItem"].ToString();

            string Day = e.Column.FieldName.Substring(0,2);
            _Date = Convert.ToDateTime(Dt_Date.smtStartDateTimeValue).ToString("yyyyMM");
            _InspTime = Lup_InspTime.smtLookUpValue.ToString();
            _Muchine_Cd = dr["MachineCode"].ToString();

            EQ_MachineChk mc = new EQ_MachineChk();

            mc.InspectDivCd = "10";                     //점검구분 10:일상점검, 20:정기점검
            mc.Seq = Convert.ToInt16(dr["Seq"]);          //검사등록순번
            mc.InspectDate = _Date + Day;                      //검사일자
            mc.ChkTimeDivCd = _InspTime;               //일별점검시간 공통코드: 10-08:00, 20-20:00

            mc.InspectCheck = "Y";                       //점검결과
            mc.ChargeEmpNo = "";                         //담당자
            mc.ActionResultDivCd = "";                 //조치진행 코드
            mc.ActionDivCd = "";                        //조치내역 코드
            mc.ActionMemo ="";                           //조치내역
            mc.ActionEmpNo = "";                        //조치자;
            mc.Remark = "";                                  //비고

            //검사한 항목일경우
            if (_saveType == "Update")
            {
                mc.MachineCode = _Muchine_Cd;  //설비코드

                if (!mc.Upd_InspectResult())
                {
                    //저장 중 에러 발생시
                    Success = false;
                }
            }
            //미검사 항목일 경우
            else if(_saveType == "Insert")
            {
                mc.MachineCode = _Muchine_Cd;  //설비코드
                if (!mc.Ins_InspectResult())
                {
                    Success = false;
                }
            }
            //삭제
            else if (_saveType == "Delete")
            {
                mc.MachineCode = _Muchine_Cd;  //설비코드
                if (!mc.Del_InspectResult_Daliy())
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

        private void lupChange_Search(object sender, EventArgs e)
        {
            Search();
        }
        #endregion
    }
}