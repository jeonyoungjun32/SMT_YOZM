using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.EQ;
using System;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_MachineMaster : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //점검항목 Repository CheckBox
        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //점검항목 Repository 숫자입력
        CM_CateItem item = new CM_CateItem();

        public Frm_EQ_MachineMaster()
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
            SetGrid_Insp(GvMaster);
            SetGrid_Repair(gvRepair);
            SetGrid_Work(GvWork);

            Set_Lup();
        }

        #region GridView 셋팅

        private void SetGrid_Insp(GridView gv)
        {
            //합/불검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "점검구분", "InspectDivCd");
            grid.Create_GvColumn(gv, "설비명", "Machine_Nm");
            grid.Create_GvColumn(gv, "점검일자", "InspectDate");
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

            //Repository 셋팅
            gv.Columns["WeekDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204060"), "분류코드", "분류명"); // 관리요일
            gv.Columns["CycleDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204050"), "분류코드", "분류명"); // 관리주기
            gv.Columns["ValueTypeDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204070"), "분류코드", "분류명"); // 설비검사 값 형태
            gv.Columns["InspectDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "2040"), "분류코드", "분류명"); //설비점검기준
            gv.Columns["ActionResultDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204010"), "분류코드", "분류명"); //조치상황
            gv.Columns["ActionDivCd"].ColumnEdit = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204020"), "분류코드", "분류명"); // 조치구분
            gv.Columns["ChargeEmpNo"].ColumnEdit = rpo.Create_RepositoryItem(code.GetDataTableUser(false), "코드", "명칭");  // 담당자
            gv.Columns["ActionEmpNo"].ColumnEdit = rpo.Create_RepositoryItem(code.GetDataTableUser(false), "코드", "명칭");  // 조치자

            //점검결과 Inspect_Check   CheckEdit 설정
            rpoChk = rpo.Create_rpoChk();
            rpoChk.ValueChecked = "10";
            rpoChk.ValueUnchecked = "20";

            rpoTxt = rpo.Create_rpoTxt_Numeric();

            RepositoryItemCheckEdit chk_Ins = rpo.Create_rpoChk();
            chk_Ins.ValueChecked = "10";
            chk_Ins.ValueUnchecked = "20";

            gv.Columns["InspectCheck"].ColumnEdit = chk_Ins;
        }

        private void SetGrid_Repair(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            // 날짜
            RepositoryItemDateEdit dateRepair = new RepositoryItemDateEdit();
            dateRepair.Mask.EditMask = "yyyy-MM-dd";
            dateRepair.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateRepair.UseMaskAsDisplayFormat = true;

            RepositoryItemDateEdit dateStart = new RepositoryItemDateEdit();
            dateStart.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            dateStart.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateStart.UseMaskAsDisplayFormat = true;

            RepositoryItemDateEdit dateEnd = new RepositoryItemDateEdit();
            dateEnd.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            dateEnd.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            dateEnd.UseMaskAsDisplayFormat = true;

            RepositoryItemSearchLookUpEdit lup_Machine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList(false), "설비코드", "설비명");

            //Column 생성
            //grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "수리일자", "RepairDate", dateRepair);
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName", lup_Machine);
            grid.Create_GvColumn(gv, "시작시간", "RepairStartTime", dateStart);
            grid.Create_GvColumn(gv, "종료시간", "RepairEndTime", dateEnd);
            grid.Create_GvColumn(gv, "고장내용", "RePairCause");
            grid.Create_GvColumn(gv, "수리내용", "RepairState");
            grid.Create_GvColumn(gv, "담당자", "RepairEmpNo");

            RepositoryItemSearchLookUpEdit lup_Manager = new RepositoryItemSearchLookUpEdit();
            lup_Manager.DataSource = code.GetUser();
            lup_Manager.DisplayMember = "사용자명";
            lup_Manager.ValueMember = "ID";
            lup_Manager.NullText = "";
            gv.Columns["RepairEmpNo"].ColumnEdit = lup_Manager;

            grid.SetCol_Type(gv.Columns["RepairStartTime"], clsGrid.TypeList.TDateTime);
            grid.SetCol_Type(gv.Columns["RepairEndTime"], clsGrid.TypeList.TDateTime);

            gv.Columns["MachineCode"].Visible = false;
        }

        private void SetGrid_Work(GridView gv)
        {
            //합/불검사 Grid
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

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
        }

        private void Set_Lup()
        {
            LupMachine.smtLookUpDataSource = code.GetMachineList(true);
            LupMachine.smtLookUpDataDisplayMember = "설비명";
            LupMachine.smtLookUpDataValueMember = "설비코드";
            LupMachine.smtLookUpValue = "ALL";
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

            // 일상점검현황 + 정기점검현황
            GcMaster.DataSource = mc.Select_MachineMaster();
            GvMaster.BestFitColumns();

            // 수리현황
            gcRepair.DataSource = mc.Select_EQ_Repair();
            gvRepair.BestFitColumns();

            // 이력현황
            GcWork.DataSource = mc.Select_MachineWork();
            GvWork.BestFitColumns();
        }

        #endregion 데이터 조회 End

    }
}
