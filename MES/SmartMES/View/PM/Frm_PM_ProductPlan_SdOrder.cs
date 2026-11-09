using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SmartMES.Models.PM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.PM
{
	public partial class Frm_PM_ProductPlan_SdOrder : DevExpress.XtraEditors.XtraForm
    {
        WrComFunc func = new WrComFunc();
        clsCode code = new clsCode();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        public Frm_PM_ProductPlan_SdOrder()
        {
            InitializeComponent();
        }

        private void frmPrepPlanDetail_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();

            //버튼 권한
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), panel4);

            DisplayOrder();
        }


        #region 디자인

        private void SetLup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            ////그리드 납품처(매출 거래처)
            //DataTable dt = code.GetCustomerList(new string[] { "20" }, false);

            //repositoryItemSearchLookUpEdit1.DataSource = dt;
            //repositoryItemSearchLookUpEdit1.DisplayMember = "거래처명";
            //repositoryItemSearchLookUpEdit1.ValueMember = "거래처코드";

            //repositoryItemSearchLookUpEdit2.DataSource = dt;
            //repositoryItemSearchLookUpEdit2.DisplayMember = "거래처명";
            //repositoryItemSearchLookUpEdit2.ValueMember = "거래처코드";

            Lup_Cust.smtLookUpDataSource = code.Get_CustomerDt(true);
            Lup_Cust.smtLookUpDataDisplayMember = "내용";
            Lup_Cust.smtLookUpDataValueMember = "코드";
            Lup_Cust.smtLookUpValue = "ALL";
        }

        private void SetGridView()
        {
            SetGridSdOrder(GvSdOrder);
            SetGridPM_ProductPlan(GvPM_ProductPlan);
        }


        private void SetGridSdOrder(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            //Repository 생성
            //품목
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] { "10", "20" }), "품목코드", "품명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            //컬럼 생성
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");

            grid.Create_GvColumn(gv, "수주일자", "ObtainDate");
            grid.Create_GvColumn(gv, "납기일자", "DeliveryDate", rpo.Create_rpoDate());

            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "수주수량", "Qty");
            grid.Create_GvColumn(gv, "미지시량", "RemainQty");

            if(Database.DbName.ToUpper() == "TPG")
			{
                //수주번호, 순번 Visible 순서 조정으로 인하여 삭제했다가 추가
                gv.Columns.Remove(gv.Columns["SdOrderNo"]);
                gv.Columns.Remove(gv.Columns["SdOrderSeq"]);

                grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
                grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");
            }

            //VISIBLE
            gv.Columns["CustCode"].Visible = false;

            //
            gv.Columns["ItemName"].ColumnEdit = lup_material;
            if (WrGlobal.CdOption["제품코드 초기화"]["ItemClass1"].DispYn == true)
            {
                gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            }
            if (WrGlobal.CdOption["제품코드 초기화"]["ItemClass2"].DispYn == true)
            {
                gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            }
            if (WrGlobal.CdOption["제품코드 초기화"]["ItemClass3"].DispYn == true)
            {
                gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;
            }
            if (WrGlobal.CdOption["제품코드 초기화"]["UnitCode"].DispYn == true)
            {
                gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            }

            //
            grid.SetCol_Type(gv.Columns["ObtainDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["RemainQty"], clsGrid.TypeList.TFloat);

            //EVENT
            grid.RowNumber_Indicator(gv);
            grid.Set_Navigator(gv);

            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

		private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
		{
            DisplayPM_ProductPlan();
        }

		private void SetGridPM_ProductPlan(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();

            //라인
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");
            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");

            grid.Create_GvColumn(gv, "상태", "RowStatus");
            grid.Create_GvColumn(gv, "계획번호", "OrderNo");
            grid.Create_GvColumn(gv, "계획일자", "WorkDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "계획수량", "OrderQty");
            grid.Create_GvColumn(gv, "긴급여부", "UrgentYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "비고", "Remark");

            if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            {
                grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
                grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");
            }

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            grid.SetCol_Type(gv.Columns["WorkDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);

            grid.SetCol_Summary(gv, "OrderQty");

            grid.Column_AllowEdit(gv.Columns["WorkDate"]);
            grid.Column_AllowEdit(gv.Columns["OrderQty"]);
            grid.Column_AllowEdit(gv.Columns["UrgentYn"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            gv.Columns["LineCode"].Visible = false;
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["MachineCode"].Visible = false;

            GvPM_ProductPlan.CellValueChanged += GvPrepPlan_CellValueChanged;
        }

		#endregion

		#region 조회


		private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayOrder();
        }

        private void DisplayOrder()
		{
            this.Validate();
            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            string custCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();

            //수주조회
            BM_SdOrderD sd = new BM_SdOrderD();
            DataTable dt = sd.SelectBM_SdOrderD_PM_ProductPlan(custCode, startDate, endDate);
            GcSdOrder.DataSource = dt;

            GvSdOrder.BestFitColumns();

            if(dt == null) GcPM_ProductPlan.DataSource = null;
            if(dt.Rows.Count < 1) GcPM_ProductPlan.DataSource = null;
        }

        private void DisplayPM_ProductPlan()
        {
            GridView gv = GcSdOrder.MainView as GridView;

            DataRow dr = gv.GetFocusedDataRow();

            if (dr == null)
            {
                GcPM_ProductPlan.DataSource = null;
                return;
            }

            PM_ProductPlan wo = new PM_ProductPlan();
            wo.CorpId = WrGlobal.CorpID;
            wo.SdOrderNo = Convert.ToString(dr["SdOrderNo"]);
            wo.SdOrderSeq = dr["SdOrderSeq"].ConvertInt_Null_To_One();

            GcPM_ProductPlan.DataSource = wo.Select_PM_ProductPlan_BySdOrderNo();
            GvPM_ProductPlan.BestFitColumns();
        }

        #endregion

        #region 이벤트

        #endregion

        #region CRUD


        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            //미입력된 칸 있는지 확인
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["RowStatus"]) == "신규")
                {
                    if (Convert.ToString(dr["WorkDate"]) == "")
                    {
                        MessageBox.Show("계획일자 항목이 비어있습니다.");
                        return false;
                    }

                    if (Convert.ToString(dr["OrderQty"]) == "")
                    {
                        MessageBox.Show("계획수량 항목이 비어있습니다.");
                        return false;
                    }

                    checkCnt++;
                }
                else if (Convert.ToString(dr["RowStatus"]) == "수정") { checkCnt++; }
                else if (Convert.ToString(dr["RowStatus"]) == "삭제")
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
            PM_ProductPlan wo = new PM_ProductPlan();
            string workDate = dr["WorkDate"].ConvertDate_To_Char10();

            try
            {
                wo.CorpId = WrGlobal.CorpID;
                wo.OrderNo = Convert.ToString(dr["OrderNo"]);      //품목코드

                if (div != 'D')
                {
                    //Delete제외 공통
                    wo.WorkDate = workDate;
                    wo.ItemCode = Convert.ToString(dr["ItemCode"]);
                    wo.OrderQty = dr["OrderQty"].ConvertInt_Null_To_Zero();
                    //wo.LineCode = dr["LineCode"].ConvertString_Null_To_Empty();
                    //wo.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
                    //wo.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();
                    wo.SdOrderNo = Convert.ToString(dr["SdOrderNo"]);
                    wo.SdOrderSeq = dr["SdOrderSeq"].ConvertInt_Null_To_Zero();
                    wo.Remark = Convert.ToString(dr["Remark"]);
                    wo.AutoOpYN = WrGlobal.ProcOption["PD"]["AutoOrderProcessYN"];
                    wo.UrgentYn = dr["UrgentYn"].ConvertString_Null_To_Empty();
                }
            }
            catch
            {
                MessageBox.Show("값 형태변환 에러");
                return false;
            }

            //2. DB 적용
            if (div == 'I')
            {
                // 1. 생산계획 추가
                // 지시번호 채번
                string orderNo = "WO" + code.GetAutoNumber("작업지시", Convert.ToDateTime(workDate));


                wo.OrderNo = orderNo;
                //Insert
                if (!wo.Insert_PM_ProductPlan())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                //if (!wo.Insert_PM_SdRoute())
                if (!wo.Insert_PM_SdRoute_YOZM())
                {
                    cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                    return false;
                }
            }
            else if (div == 'U')
            {
                //Update
                if (!wo.Update_PM_ProductPlan())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                if (!wo.Update_PM_SdRoute())
                {
                    cls_MessageBox.Show(cls_MessageBox.Save_Fail, cls_MessageBox.SaveCaption);
                    return false;
                }
            }
            else if (div == 'D')
            {
                //Delete
                if (!wo.Delete_PM_ProductPlan())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }

            //상태값 초기화
            dr["RowStatus"] = "";


            return true;

        }

        #endregion


        private void GvPrepPlan_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;

            //상태값 수정
            grid.Change_Status(gv, e.RowHandle);
        }

        private void cmdRowEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow drSdOrder = GvSdOrder.GetFocusedDataRow();
                if (drSdOrder == null) return;

                if (GcPM_ProductPlan.DataSource == null) return;

                //행삽입, 추가, 삭제 버튼 클릭 이벤트
                //1. 이름의 마지막 3글자 가져오기
                smtSearchButton btn = sender as smtSearchButton;
                string txt_Act = btn.Tag.ToString();

                //2. 입력 데이터 설정
                Dictionary<string, object> dic = new Dictionary<string, object>();

                if (txt_Act != "Del")
                {
                    //신규상태
                    //BOM, 라우팅 등록 확인
                    CM_BOMRev cbm = new CM_BOMRev();
                    cbm.CorpId = WrGlobal.CorpID;
                    cbm.ItemCode = drSdOrder["ItemCode"].ConvertString_Null_To_Empty();

					if (!cbm.ExistsBOMMaster())
					{
                        XtraMessageBox.Show("BOM/라우팅 정보가 등록되어 있지 않습니다.\n기준정보 등록 완료 후 계획을 등록해 주세요.", "BOM/라우팅 미등록", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
					}

                    dic.Add("RowStatus", "신규");
                    dic.Add("SdOrderNo", drSdOrder["SdOrderNo"]);
                    dic.Add("SdOrderSeq", drSdOrder["SdOrderSeq"]);
                    dic.Add("ItemCode", drSdOrder["ItemCode"]);
                    dic.Add("WorkDate", DateTime.Now);
                }
                else
                {
                    if (Convert.ToString(GvPM_ProductPlan.GetFocusedRowCellValue("RowStatus")) != "삭제")
					{
                        //지시 내역 확인 있으면 Return
                        string orderNo = GvPM_ProductPlan.GetFocusedRowCellValue("OrderNo").ConvertString_Null_To_Empty();

                        PM_OrderProcess op = new PM_OrderProcess();

                        if (op.ExistsOP_ByOrderNo(orderNo))
                        {
                            XtraMessageBox.Show("해당 생산계획으로 이미 작업지시가 생성되어 삭제할 수 없습니다.\n작업지시를 먼저 삭제한 후 다시 시도하세요.");
                            return;
                        }
                    }

                    //삭제 진행인경우
                    dic.Add("RowStatus", Convert.ToString(GvPM_ProductPlan.GetFocusedRowCellValue("RowStatus")) == "삭제" ? "" : "삭제");
                }

                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(GcPM_ProductPlan, dic, txt_Act);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//end try
        }

		private void cmdSave_Click(object sender, EventArgs e)
		{
            DataTable dt = null;

            GvPM_ProductPlan.PostEditor();
            GvPM_ProductPlan.UpdateCurrentRow();
            dt = GcPM_ProductPlan.DataSource as DataTable;

            //저장 시작
            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {

                if (Convert.ToString(dt.Rows[curHandle]["RowStatus"]) == "신규")
                {
                    if (dt.Rows[curHandle]["OrderQty"].ConvertInt_Null_To_Zero() == 0) continue;

                    if (!Save_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["RowStatus"]) == "수정")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["RowStatus"]) == "삭제")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }

            XtraMessageBox.Show(cls_MessageBox.Save_Comp);

            int idx = GvSdOrder.FocusedRowHandle;
            DisplayOrder();
            GvSdOrder.FocusedRowHandle = idx;
            //DisplayPM_ProductPlan();
        }

		private void CallPrepPlanAdd(object sender, EventArgs e)
		{
            //frmPrepPlan frm = new frmPrepPlan();

            Frm_PM_ProductPlan_Indiv frm = new Frm_PM_ProductPlan_Indiv();
            frm.Tag = this.Tag;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}