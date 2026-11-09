using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.EQ;
using SmartMES.Models.PM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.PM
{
	public partial class Frm_PM_OrderProcess_MC : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        public Frm_PM_OrderProcess_MC()
        {
            InitializeComponent();
        }

        private void frmPM_OrderProcess_MC_Load(object sender, EventArgs e)
        {
            //올덴, TPG 공정리스트 숨기기 처리
            if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            {
                layoutControlGroup5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                splitterItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            // 디자인
            Set_Design();
            // 조회
            DisplayPM_ProductPlan();
        }

        #region 디자인
        private void Set_Design()
        {
            // Radio
            Set_Rdo();

            // DateEdit
            Set_DateEdit();

            // 그리드
            Set_GvPM_ProductPlan(GvPM_ProductPlan);
            Set_GvPM_OrderProcess(GvList);
            Set_GvMachine(GvMCList);

            //
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), panel2);
        }

        private void Set_DateEdit()
        {
            DateTime start = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime end = Convert.ToDateTime(start.AddDays(40).ToString("yyyy-MM-01"));

            DtOrder.smtStartDateTimeValue = start;
            DtOrder.smtEndDateTimeValue = end;
        }


        private void Set_Rdo()
        {
            //품목구분 RadioGroup 셋팅
            //Item 생성

            clsRadioGroup clsRadio = new clsRadioGroup();
            DataTable dtP = code.GetItemType(new string[] { "20", "30" });

            clsRadio.Create_RadioGroupItem(Rdo_ItemType, dtP, "코드명", "코드");

            Rdo_ItemType.SelectedIndex = 0;
            Rdo_ItemType.SelectedIndexChanged += Rdo_Item_Div_SelectedIndexChanged;
        }

        private void Rdo_Item_Div_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayPM_ProductPlan();
        }

        #region 그리드

        private void Set_GvPM_ProductPlan(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            gv.OptionsBehavior.ReadOnly = true;

            clsRepository rpo = new clsRepository();
            //거래처
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(), "거래처코드", "거래처명");

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "구분", "UrgentYn");
            grid.Create_GvColumn(gv, "계획순번", "WorkSeq");
            grid.Create_GvColumn(gv, "수주번호", "SdOrderNo");
            grid.Create_GvColumn(gv, "수주순번", "SdOrderSeq");
            grid.Create_GvColumn(gv, "계획번호", "PrepNo");
            grid.Create_GvColumn(gv, "계획번호", "OrderNo");
            grid.Create_GvColumn(gv, "계획일자", "WorkDate", rpo.Create_rpoDate());

            grid.Create_GvColumn(gv, "거래처", "CustCode", lupCust);

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "계획수량", "OrderQty");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //Repository
            gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;
            gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            gv.Columns["UrgentYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //visible
            gv.Columns["SdOrderNo"].Visible = false;
            gv.Columns["SdOrderSeq"].Visible = false;
            gv.Columns["PrepNo"].Visible = false;

            //프로세스 옵션 : 생산계획 우선순위 조정여부 사용, 미사용
            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] != "Y")
            {
                gv.Columns["WorkSeq"].Visible = false;
            }

            if (WrGlobal.ProcOption["BS"]["Item_OneCust"] != "Y")
            {
                gv.Columns["CustCode"].Visible = false;
            }

            //EVENT
            grid.Set_Navigator(GvPM_ProductPlan);
            gv.FocusedRowObjectChanged += GvPM_ProductPlan_FocusedRowObjectChanged;
            gv.CustomDrawCell += gv_CustomDrawCell;
        }

        private void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("UrgentYn") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "긴급")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Red;
            }

        }
        private void Set_GvPM_OrderProcess(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            //라인
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");

            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            grid.Create_GvColumn(gv, "라우팅순번", "RoutingSeq");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");

            //Repository
            gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;
            gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;

            //
            grid.Column_AllowEdit(gv.Columns["LineCode"]);
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["OrderQty"]);

            //FIX
            gv.Columns["LineCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["ProcessCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            gv.Columns["OrderQty"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //VISIBLE
            gv.Columns["OrderNo"].Visible = false;
            gv.Columns["OrderQty"].Visible = false;

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);

			//EVENT
			grid.RowNumber_Indicator(gv);
			grid.Set_Navigator(gv);
            gv.FocusedRowObjectChanged += GvPM_OrderProcess_FocusedRowObjectChanged;
        }

        private void Set_GvMachine(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "지시일자", "OrderDate");
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);

            //올덴, TPG 컬럼 추가
            if (Database.DbName.ToUpper().Contains("ALLTHEN"))
            {
                grid.Create_GvColumn(gv, "RPM", "RPM");
                grid.Create_GvColumn(gv, "열", "ArrayValue");
                grid.Create_GvColumn(gv, "생산율", "ProductionYield");

                GvMCList.Columns["ProductionYield"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                GvMCList.Columns["ProductionYield"].DisplayFormat.FormatString = "p0";

                gv.Columns["ProductionYield"].Visible = false; //251113 올덴 요청사항 : 생산율 컬럼 숨김
            }
            if (Database.DbName.ToUpper().Contains("TPG"))
            {
                grid.Create_GvColumn(gv, "배열", "ArrayValue");
                grid.Create_GvColumn(gv, "RPM", "RPM");
                grid.Create_GvColumn(gv, "Capa(10hr)", "Capa10");
                grid.Create_GvColumn(gv, "Capa(21hr)", "Capa21");
                grid.Create_GvColumn(gv, "수율", "ProductionYield");

                grid.SetCol_Type(gv.Columns["Capa10"], clsGrid.TypeList.TInt);
                grid.SetCol_Type(gv.Columns["Capa21"], clsGrid.TypeList.TInt);
                GvMCList.Columns["ProductionYield"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                GvMCList.Columns["ProductionYield"].DisplayFormat.FormatString = "{0:0.#}%";
            }

            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "긴급여부", "UrgentYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "비고", "Remark");

            //
            grid.Column_AllowEdit(gv.Columns["OrderDate"]);
            grid.Column_AllowEdit(gv.Columns["MachineCode"]);
            grid.Column_AllowEdit(gv.Columns["OrderQty"]);
            grid.Column_AllowEdit(gv.Columns["UrgentYn"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //
            grid.SetCol_Summary(gv, "OrderQty");

            //EVENT
            grid.RowNumber_Indicator(gv);
            grid.Set_Navigator(gv);
            gv.CellValueChanged += Gv_CellValueChanged1;
        }

		private void Gv_CellValueChanged1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

		private void GvPM_ProductPlan_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            Display_PM_OrderProcess();

            if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            {
                if (GvList.RowCount == 0)
                {
                    DisplayMachine();
                }
            }
        }

        private void GvPM_OrderProcess_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            DisplayMachine();
        }

        private void GvMCList_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            {
                //셀값 변경 시 다른 Column 셀값 수정
                GridView gv = sender as GridView;
                string fieldName = e.Column.FieldName;
                DataRow drChanged = gv.GetDataRow(e.RowHandle);

                // 설비 선택시 해당 설비의 열, RPM, 수율 값 보여주기
                if (fieldName == "MachineCode")
                {
                    EQ_Machine cm = new EQ_Machine();
                    cm.MachineCode = Convert.ToString(drChanged["MachineCode"]);
                    DataRow drMachine = cm.GetMachineData();

                    if (drMachine != null)
                    {
                        drChanged["ArrayValue"] = drMachine["ArrayValue"];
                        drChanged["RPM"] = drMachine["RPM"];
                        if (Database.DbName.ToUpper().Contains("TPG"))
                        { drChanged["ProductionYield"] = drMachine["ProductionYield"]; }
                    }
                    else
                    {
                        drChanged["ArrayValue"] = DBNull.Value;
                        drChanged["RPM"] = DBNull.Value;
                        if (Database.DbName.ToUpper().Contains("TPG"))
                        { drChanged["ProductionYield"] = DBNull.Value; }
                    }
                }

                // 설비, 지시수량 입력시 설비 정보 바탕으로 계산하여 값 변경
                if (fieldName == "MachineCode" || fieldName == "OrderQty")
                {
                    if (Database.DbName.ToUpper().Contains("ALLTHEN"))
                    {
                        CalProdRate_ALLTHEN(e.RowHandle);
                    }
                    if (Database.DbName.ToUpper().Contains("TPG"))
                    {
                        CalCapa_TPG(e.RowHandle);
                    }
                }

                gv.BestFitColumns();
            }
        }

        private void CalProdRate_ALLTHEN(int rowHandle)
        {
            try
            {
                int qty = GvMCList.GetRowCellValue(rowHandle, "OrderQty").ConvertInt_Null_To_Zero();
                int rpm = GvMCList.GetRowCellValue(rowHandle, "RPM").ConvertInt_Null_To_Zero();
                int arr = GvMCList.GetRowCellValue(rowHandle, "ArrayValue").ConvertInt_Null_To_Zero();

                if (rpm != 0 && arr != 0)
                {
                    // 생산율 계산 - 지시수량/RPM/M(60)/H(9)/열
                    float prodRate = ((float)qty / rpm / 60 / 9 / arr);
                    GvMCList.SetRowCellValue(rowHandle, "ProductionYield", Math.Round(prodRate, 2));
                }
                else
                {
                    GvMCList.SetRowCellValue(rowHandle, "ProductionYield", DBNull.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalCapa_TPG(int rowHandle)
        {
            try
            {
                int rate = GvMCList.GetRowCellValue(rowHandle, "ProductionYield").ConvertInt_Null_To_Zero();
                int rpm = GvMCList.GetRowCellValue(rowHandle, "RPM").ConvertInt_Null_To_Zero();
                int arr = GvMCList.GetRowCellValue(rowHandle, "ArrayValue").ConvertInt_Null_To_Zero();

                if (rate != 0 && rpm != 0 && arr != 0)
                {
                    // Capa(10hr), Capa(21hr) 계산 - 배열*RPM*60*hour*수율
                    int capa10 = (arr * rpm * 60 * 10 * rate) / 100;
                    int capa21 = (arr * rpm * 60 * 21 * rate) / 100;

                    GvMCList.SetRowCellValue(rowHandle, "Capa10", capa10);
                    GvMCList.SetRowCellValue(rowHandle, "Capa21", capa21);
                }
                else
                {
                    GvMCList.SetRowCellValue(rowHandle, "Capa10", DBNull.Value);
                    GvMCList.SetRowCellValue(rowHandle, "Capa21", DBNull.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
        #endregion

        #region 이벤트
        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;
            string name = btn.smtButtonStyle.ConvertString_Null_To_Empty();

            switch (name)
            {
                case "조회":
                    DisplayPM_ProductPlan();
                    break;

                case "저장":
                    Func_Save();
                    break;

                case "엑셀변환":
                    break;

                default: break;
            }
        }
        #endregion

        #region 조회

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            DisplayPM_ProductPlan();
        }

        private void DisplayPM_ProductPlan()
        {
            this.Validate();

            string startDate = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char10();

            PM_ProductPlan wo = new PM_ProductPlan();
            wo.ItemType = Rdo_ItemType.EditValue.ConvertString_Null_To_Empty();
            GcPM_ProductPlan.DataSource = wo.Select_PM_ProductPlan(WrGlobal.CorpID, startDate, endDate);

            GvPM_ProductPlan.BestFitColumns();
        }

        private void Display_PM_OrderProcess()
        {
            this.Validate();

            DataRow dr = GvPM_ProductPlan.GetFocusedDataRow();

            if(dr == null)
			{
                GcList.DataSource = null;
                return;
			}

            string startDate = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char10();

            PM_SdRoute sr = new PM_SdRoute();
            sr.StartDate = startDate;
            sr.EndDate = endDate;
            sr.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
            sr.ProcessType = "P";

            DataTable dt = sr.Select_PM_SdRouteByOrderNo_MC();

            GcList.DataSource = dt;
            GvList.BestFitColumns();
		}

        private void DisplayMachine()
        {
            this.Validate();

            DataRow dr = GvList.GetFocusedDataRow();

            if (dr == null)
            {
                GcMCList.DataSource = null;
                return;
            }

            string startDate = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char10();

            PM_OrderProcess op = new PM_OrderProcess();
            op.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
            op.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            op.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();

            DataTable dt = new DataTable();
            dt = op.Select_OrderPropcess_Indiv(startDate, endDate);

            GcMCList.DataSource = dt;

            GvMCList.BestFitColumns();
        }

        #endregion

        #region 저장
        private void Func_Save()
        {
            this.Validate();

            int idx_wo = GvPM_ProductPlan.FocusedRowHandle;
            int idx_op = GvList.FocusedRowHandle;

            DataTable dt = GcMCList.DataSource as DataTable;

            bool Success = true;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                switch (status)
                {
                    case "신규": if (!Save_ToDatabase(dr, 'I')) { Success = false; } break;
                    case "수정": if (!Save_ToDatabase(dr, 'U')) { Success = false; } break;
                    case "삭제": if (!Save_ToDatabase(dr, 'D')) { Success = false; } break;
                }
            }

            //3. 정상절차 진행
            if (Success)
            {
                DisplayPM_ProductPlan();
                Display_PM_OrderProcess();
                DisplayMachine();

                //grid에서 선택된 행 유지
                GvPM_ProductPlan.FocusedRowHandle = idx_wo;
                GvPM_ProductPlan.TopRowIndex = idx_wo;
                GvList.FocusedRowHandle = idx_op;
                GvList.TopRowIndex = idx_op;

                MessageBox.Show(cls_MessageBox.Save_Comp);
            }
            else
            {
                MessageBox.Show(cls_MessageBox.Save_Fail);
            }
        }

        private bool Check_SaveClick(DataTable dt)
        {
            if (dt == null) return false;

            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            string status = "";

            DataRow drPM_SdRoute = GvList.GetFocusedDataRow();
            if (drPM_SdRoute == null) return false;

            if(drPM_SdRoute["ProcessCode"].ConvertString_Null_To_Empty() == "")
			{
                MessageBox.Show("공정 항목이 비어있습니다.");
                return false;
            }
            //else if(drPM_SdRoute["LineCode"].ConvertString_Null_To_Empty() == "")
			//{
            //    MessageBox.Show("라인 항목이 비어있습니다.");
            //    return false;
            //}

            foreach (DataRow dr in dt.Rows)
            {
                status = Convert.ToString(dr["Status"]);

                if (status == "신규" || status == "수정")
                {
					if (Convert.ToString(dr["OrderDate"]) == "")
					{
						MessageBox.Show("지시일자 항목이 비어있습니다.");
						return false;
					}
                    //else if (dr["MachineCode"].ConvertString_Null_To_Empty() == "")
                    //{
                    //    XtraMessageBox.Show("설비를 입력하세요.");
                    //    return false;
                    //}
                    else if (dr["OrderQty"].ConvertString_Null_To_Empty() == "")
                    {
                        XtraMessageBox.Show("지시수량을 입력하세요.");
                        return false;
                    }
                    checkCnt++;
                }
                else if (status == "삭제")
                {
                    isDelete = true;
                    checkCnt++;
                }
            }
            if (checkCnt == 0) return false;

            //삭제내역 있으면 물어보기
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
			//데이터베이스에 반영
			//1. 기본 셋팅
			DataRow drPM_ProductPlan = GvList.GetFocusedDataRow();

			PM_OrderProcess op = new PM_OrderProcess();

			op.CorpId = WrGlobal.CorpID;
			op.OrderNo = drPM_ProductPlan["OrderNo"].ToString();
			op.ProcessCode = drPM_ProductPlan["ProcessCode"].ToString();
			op.ItemCode = drPM_ProductPlan["ItemCode"].ToString();
			op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
			op.LineCode = drPM_ProductPlan["LineCode"].ConvertString_Null_To_Empty();
			op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
			op.OrderDate = dr["OrderDate"].ConvertDate_To_Char10();
			op.MachineCode = dr["MachineCode"].ToString();
			op.OrderQty = dr["OrderQty"].ConvertInt_Null_To_Zero();
			op.Remark = dr["Remark"].ToString();
			op.Status = "대기";
            op.RoutingSeq = drPM_ProductPlan["RoutingSeq"].ConvertInt_Null_To_Zero();
            op.UrgentYn = dr["UrgentYn"].ConvertString_Null_To_Empty();

            //2. DB 적용
            if (div == 'I')
			{

                cls_Database cDB = new cls_Database();
                int No_Seq = cDB.Get_NoSeq("PM_OrderProcess", "OrderNo", op.OrderNo, "OrderSeq");
                op.OrderSeq = No_Seq;

                //Insert
                if (!op.Insert_PM_OrderProcess())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}
				//추가 변경사항 private 따로 만들기
			}
			else if (div == 'U')
			{
				//Update
				if (!op.Update_PM_OrderProcess_Indiv())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}

				//추가 변경사항 private 따로 만들기
			}
			else if (div == 'D')
			{
                //Delete
                //실적 + 재고 + 불량 삭제
                PM_OrderResult odr = new PM_OrderResult();
                odr.OrderNo = op.OrderNo;
                odr.OrderSeq = op.OrderSeq;
                odr.Del_PM_OrderResult_ALL_OrderNoSeq();

                //지시삭제
				if (!op.Del_PM_OrderProcess())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}

				//추가 변경사항 private 따로 만들기
			}

			//상태값 초기화
			dr["Status"] = "";

			return true;
		}

        #endregion

        private void cmdRowEdit_Click(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            smtSearchButton btn = sender as smtSearchButton;
            string txt_Act = btn.Tag.ToString();

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("OrderDate", DateTime.Now.ToString("d"));

            }
            else
            {
                string status = Convert.ToString(GvMCList.GetFocusedRowCellValue("Status"));

                if (status != "삭제")
                {
                    //지시로 실적등록 내역 확인 메시지
                    string orderNo = GvMCList.GetFocusedRowCellValue("OrderNo").ConvertString_Null_To_Empty();
                    int orderSeq = GvMCList.GetFocusedRowCellValue("OrderSeq").ConvertInt_Null_To_Zero();

                    PM_OrderResult odr = new PM_OrderResult();

                    if (odr.ExistsResult_NoSeq(orderNo, orderSeq))
                    {
                        if(XtraMessageBox.Show("해당 지시에 이미 실적이 등록되었습니다.\n삭제 시 관련 데이터가 함께 삭제됩니다.\n삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
						{
                            return;
						}
                    }
                }

                //삭제 진행인경우
                dic.Add("Status", status == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(GcMCList, dic, txt_Act);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}