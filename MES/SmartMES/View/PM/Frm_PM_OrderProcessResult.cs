using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.BM;
using SmartMES.Models.CM;
using SmartMES.Models.PM;
using SmartMES.Models.QM;
using SmartMES.Models.ST;
using SmartMES.View.PM.Popup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_OrderProcessResult : DevExpress.XtraEditors.XtraForm
    {
        private string SelectType = "Wo"; //조회상태구분 - Wo : 작업지시, Rst : 생산실적

        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        CM_Process process = new CM_Process();

        string ItemType = WrGlobal.ProcOption["SC"]["Product_ItemType"];
        string DefaultProcessCode = "";

        Database db = new Database();
        string query = "";

        public Frm_PM_OrderProcessResult()
        {
            InitializeComponent();

            Set_GridView_Wo(Gv_PM_ProductPlan);         // 작업지시
            Set_GridView_Rst(Gv_ProcessResult);    // 생산실적
        }

        private void frmPM_OrderProcessResult_Load(object sender, EventArgs e)
        {
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);

            SetLup();

            tabbedControlGroup1.SelectedTabPageIndex = 1;

            //셀 수정 가능 여부
            //foreach (DevExpress.XtraGrid.Columns.GridColumn col in Gv_ProcessResult.Columns)
            //{
            //    grid.Column_AllowEdit(col, false);
            //}

            Search();
        }

        #region setDesign
        private void SetLup()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            Lup_Item.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";

            DataTable dt = code.GetProcess_List_ProcessType(true, null);//
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    DefaultProcessCode = dt.Rows[0]["공정코드"].ConvertString_Null_To_Empty();
                }
            }

            Lup_Process.smtLookUpDataSource = dt;
            Lup_Process.smtLookUpDataDisplayMember = "공정명";
            Lup_Process.smtLookUpDataValueMember = "공정코드";
            Lup_Process.smtLookUpValue = "ALL";

            Lup_Machine.smtLookUpDataSource = code.GetMachineList(true);
            Lup_Machine.smtLookUpDataDisplayMember = "설비명";
            Lup_Machine.smtLookUpDataValueMember = "설비코드";
            Lup_Machine.smtLookUpValue = "ALL";
        }

        private void Set_GridView_Wo(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();
            //라인
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");
            //품목
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] {"10","20","30","40","50","60"}), "품목코드", "품목코드");
            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //작업자
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false, null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_House = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "구분", "UrgentYn");
            grid.Create_GvColumn(gv, "작업상태", "WorkStatus");
            grid.Create_GvColumn(gv, "지시상태", "OrderStatus");
            grid.Create_GvColumn(gv, "지시일자", "OrderDate");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            //grid.Create_GvColumn(gv, "품목코드", "ItemCode", lup_material);
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");

            grid.Create_GvColumn(gv, "총생산", "TotalWorkQty");

            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");

            grid.Create_GvColumn(gv, "작업일자", "WorkDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "로트번호", "LotNo", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "작업자", "WorkerID", lup_Emp);
            grid.Create_GvColumn(gv, "시작시간", "StartDate");
            grid.Create_GvColumn(gv, "종료시간", "EndDate");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "작업순번", "SubSeq"); //실적등록 확인용
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            grid.Create_GvColumn(gv, "등록구분", "RowType");

            grid.Create_GvColumn(gv, "제품입고창고", "OutHouseID", lup_House);

            //시작시간, 종료시간 칼럼
            RepositoryItemDateEdit dateTimeEdit = new RepositoryItemDateEdit();
            dateTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateTimeEdit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            dateTimeEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateTimeEdit.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            dateTimeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm";
            dateTimeEdit.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;

            gv.Columns["StartDate"].ColumnEdit = dateTimeEdit;
            gv.Columns["EndDate"].ColumnEdit = dateTimeEdit;

            //입력
            grid.Column_AllowEdit(gv.Columns["WorkQty"]);
            grid.Column_AllowEdit(gv.Columns["GoodQty"]);
            grid.Column_AllowEdit(gv.Columns["NgQty"]);
            grid.Column_AllowEdit(gv.Columns["WorkDate"]);
            grid.Column_AllowEdit(gv.Columns["LotNo"]);
            grid.Column_AllowEdit(gv.Columns["WorkerID"]);
            grid.Column_AllowEdit(gv.Columns["StartDate"]);
            grid.Column_AllowEdit(gv.Columns["EndDate"]);

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["TotalWorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TInt);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["UrgentYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["WorkStatus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //visible
            gv.Columns["SubSeq"].Visible = false;
            gv.Columns["RowType"].Visible = false;
            gv.Columns["SerialNo"].Visible = false;
            gv.Columns["OrderStatus"].Visible = false;
            gv.Columns["OutHouseID"].Visible = false;
            gv.Columns["LotNo"].Visible = false;

            //
            grid.SetCol_Summary(gv, "OrderQty");
            grid.SetCol_Summary(gv, "TotalWorkQty");

            //프로세스 옵션 실적에서 다중 작업자 사용여부
            if (WrGlobal.ProcOption["PD"]["MultiWorkerYn"] == "Y")
            {
                //실적순번이 없으면 저장하기 애매하여 일단 주석
                gv.Columns["WorkerID"].Visible = false;
                //gv.Columns["WorkerID"].ColumnEdit = null; //다중작업자 사용시 룩업 사용X
                //gv.Columns["WorkerID"].OptionsColumn.AllowEdit = false;
                //gv.RowCellClick += Gv_RowCellClick;
            }

            //이벤트
            gv.CellValueChanged += Gv_CellValueChanged;
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

        private void Lup_material_EditValueChanged(object sender, EventArgs e)
		{
            SearchLookUpEdit lup = sender as SearchLookUpEdit;

            DataRow dr = Gv_ProcessResult.GetFocusedDataRow();
            if (dr == null) return;

            DataRow drLup = lup.Properties.View.GetFocusedDataRow();

            if(drLup != null)
			{
                dr["ItemName"] = lup.Properties.View.GetFocusedDataRow()["품명"];
			}
		}

		private void Set_GridView_Rst(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            // 그리드 헤더 줄 바꿈 <Br>
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            clsRepository rpo = new clsRepository();

            //품목
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] { "10", "20", "30" }, "Y"), "품목코드", "품목코드");
            lup_material.EditValueChanged += Lup_material_EditValueChanged;

            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //작업자
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false, null), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_House = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            //화살표 버튼 추가
            EditorButton btn_Move = new EditorButton();
            btn_Move.Kind = ButtonPredefines.Right;
            RepositoryItemButtonEdit btn = new RepositoryItemButtonEdit();
            btn.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            btn.Buttons.RemoveAt(0);
            btn.Buttons.Add(btn_Move);
            btn.ButtonClick += Btn_Bad_Detail_CliCk; //버튼 이벤트  //유형별불량, 투입재료 팝업 호출

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "실적순번", "SubSeq");
            grid.Create_GvColumn(gv, "실적번호", "SerialNo");
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "품목코드", "ItemCode", lup_material);
            grid.Create_GvColumn(gv, "품명", "ItemName");

            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");
            grid.Create_GvColumn(gv, "유형별<Br>불량수량", "Bad_Qty_Detail", btn);

            grid.Create_GvColumn(gv, "작업자", "WorkerID", lup_Emp);
            grid.Create_GvColumn(gv, "로트번호", "LotNo", rpo.Create_rpoDate());

            grid.Create_GvColumn(gv, "작업일자", "WorkDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "시작시간", "StartDate");
            grid.Create_GvColumn(gv, "종료시간", "EndDate");

            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "제품입고창고", "OutHouseID", lup_House);

            //시작시간, 종료시간 칼럼
            RepositoryItemDateEdit dateTimeEdit = new RepositoryItemDateEdit();
            dateTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateTimeEdit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            dateTimeEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateTimeEdit.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            dateTimeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm";
            dateTimeEdit.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;

            gv.Columns["StartDate"].ColumnEdit = dateTimeEdit;
            gv.Columns["EndDate"].ColumnEdit = dateTimeEdit;

            //입력부
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["MachineCode"]);
            grid.Column_AllowEdit(gv.Columns["ItemCode"]);
            grid.Column_AllowEdit(gv.Columns["WorkQty"]);
            grid.Column_AllowEdit(gv.Columns["GoodQty"]);
            grid.Column_AllowEdit(gv.Columns["NgQty"]);
            grid.Column_AllowEdit(gv.Columns["Bad_Qty_Detail"]);
            grid.Column_AllowEdit(gv.Columns["LotNo"]);
            grid.Column_AllowEdit(gv.Columns["WorkDate"]);
            grid.Column_AllowEdit(gv.Columns["WorkerID"]);
            grid.Column_AllowEdit(gv.Columns["StartDate"]);
            grid.Column_AllowEdit(gv.Columns["EndDate"]);

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TInt);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //visible
            gv.Columns["OrderQty"].Visible = false;
            gv.Columns["OutHouseID"].Visible = false;

            //
            grid.SetCol_Summary(gv, "OrderQty");
            grid.SetCol_Summary(gv, "WorkQty");
            grid.SetCol_Summary(gv, "GoodQty");
            grid.SetCol_Summary(gv, "NgQty");

            //프로세스 옵션 실적에서 다중 작업자 사용여부
            if (WrGlobal.ProcOption["PD"]["MultiWorkerYn"] == "Y")
            {
                gv.Columns["WorkerID"].ColumnEdit = null; //다중작업자 사용시 룩업 사용X
                gv.Columns["WorkerID"].OptionsColumn.AllowEdit = false;
                gv.RowCellClick += Gv_RowCellClick;
            }

            //이벤트
            gv.CellValueChanged += Gv_CellValueChanged;
            gv.ValidatingEditor += Gv_ValidatingEditor;
        }

		private void Gv_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            DataRow dr = gv.GetFocusedDataRow();
            string ColName = gv.FocusedColumn.FieldName;

            switch (ColName)
            {
                case "ItemName":

                    if (dr["SerialNo"].ConvertString_Null_To_Empty() != "")
                    {
                        e.Valid = false;    //입력 불가 처리
                        e.ErrorText = "등록된 실적 건의 품목을 변경할 수 없습니다.";
                    }
                    break;
            }
        }


        private void Gv_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GridView gv = sender as GridView;
            if (e.Column.FieldName != "WorkerID") return;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            Frm_PM_OrderResultWorker f = new Frm_PM_OrderResultWorker(dr);
			f.FormClosed += F_FormClosed;
            f.ShowDialog();
        }

		private void F_FormClosed(object sender, FormClosedEventArgs e)
		{
            Search();
        }

		#endregion setDesign

		private void ButtonClick(object sender, EventArgs e)
        {
            smtSearchButton Btn = sender as smtSearchButton;
            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "BtnSearch":
                    //조회
                    Search();
                    break;

                case "cmdSave":
                    //저장
                    Save();
                    break;

                case "cmdExcel":
                    //엑셀다운
                    cmdExcel_Click();
                    break;
            }
        }

        #region 조회

        private void Search()
        {
            try
            {
                if (tabbedControlGroup1.SelectedTabPageIndex == 0)
                {
                    Search_PM_ProductPlan();     // 지시리스트
                    SelectType = "Wo";
                    cmdIns.Visible = false;
                }
                else
                {
                    Search_Rst();           // 실적리스트
                    SelectType = "Rst";
                    cmdIns.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Search_PM_ProductPlan()
        {
            PM_OrderProcess op = new PM_OrderProcess();

            string startDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            op.ProcessCode = Lup_Process.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;
            op.MachineCode = Lup_Machine.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Machine.smtLookUpValue;
            op.ItemCode = Lup_Item.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Item.smtLookUpValue;

            Gc_PM_ProductPlan.DataSource = op.Select_PM_OrderProcess_N(startDt, endDt);
            Gv_PM_ProductPlan.BestFitColumns();
        }

        private void Search_Rst()
        {
            PM_OrderResult rst = new PM_OrderResult();

            rst.StartDt = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            rst.EndDt = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();
            rst.ProcessCode = Lup_Process.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;
            rst.MachineCode = Lup_Machine.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Machine.smtLookUpValue;
            rst.ItemCode = Lup_Item.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Item.smtLookUpValue;

            Gc_ProcessResult.DataSource = rst.Select_PM_OrderResult();
            Gv_ProcessResult.BestFitColumns();
        }

        #endregion 조회

        #region 저장

        private void Save()
        {
            //작성내역 저장
            DataTable dt;
            GridView gv = null;

            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
			{
                dt = Gc_PM_ProductPlan.DataSource as DataTable;
                gv = Gv_PM_ProductPlan;
			}
            else
			{
                dt = Gc_ProcessResult.DataSource as DataTable;
                gv = Gv_ProcessResult;
			}

            gv.PostEditor();
            gv.UpdateCurrentRow();

            bool Success = false;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            foreach (DataRow dr in dt.Rows)
            {
                string status = Convert.ToString(dr["Status"]);
                switch (status)
                {
                    case "신규": Success = Save_ToDatabase(dr, 'I'); break;

                    case "수정":
                        //if (SelectType == "Wo" && Convert.ToString(dr["SubSeq"]) == "")
                        //    //실적 등록
                        //    Success = Save_ToDatabase(dr, 'I');
                        //else
                            //실적 수정
                            Success = Save_ToDatabase(dr, 'U');
                        break;

                    case "삭제":
                        Success = Save_ToDatabase(dr, 'D'); break;

                    default: Success = true; break;
                }
                if (!Success) break;
            }

            //상태값 초기화
            foreach (DataRow dr in dt.Rows)
            {
                if (Success)
                {
                    //상태값 삭제
                    dr["Status"] = "";
                }
                else
                {
                    //실적번호 삭제
                    if (Convert.ToString(dr["Status"]) == "신규")
                    {
                        //dr["OrderSeq"] = "";
                    }
                }
            }

            if (Success)
            {
                MessageBox.Show("저장이 정상적으로 완료되었습니다.");

                Search();
            }
            else
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //데이터 불러온 상태인지 검사
            if (dt == null) return false;

            string status = "";
            bool isDelete = false;

            foreach (DataRow dr in dt.Rows)
            {
                status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규" || status == "수정")
                {
                    // === 공통 필수값 체크 (Wo, Rst 모두) ===
                    if (Convert.ToString(dr["WorkQty"]) == "") { MessageBox.Show("생산수량을 입력해주세요"); return false; }
                    if (Convert.ToString(dr["GoodQty"]) == "") { MessageBox.Show("양품수량을 입력해주세요"); return false; }
                    if (Convert.ToString(dr["WorkDate"]) == "") { MessageBox.Show("작업일자를 입력해주세요"); return false; }

                    // === 실적리스트만 추가 체크 ===
                    if (SelectType == "Rst")
                    {
                        if (Convert.ToString(dr["ProcessCode"]) == "") { MessageBox.Show("공정을 입력해주세요"); return false; }
                        if (Convert.ToString(dr["MachineCode"]) == "") { MessageBox.Show("설비를 입력해주세요"); return false; }
                        if (Convert.ToString(dr["ItemCode"]) == "") { MessageBox.Show("품목코드를 입력해주세요"); return false; }
                    }

                    // 이전공정 수량 입력되어야 다음 공정 저장 가능
                    if (SelectType == "Wo" && status == "신규")
                    {
                        PM_OrderProcess op = new PM_OrderProcess();
                        op.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
                        op.RoutingSeq = dr["RoutingSeq"].ConvertInt_Null_To_Zero();

                        if (!op.Check_PreviousRoutingWorkQty())
                        {
                            MessageBox.Show("이전 공정의 생산수량이 입력되지 않아 저장할 수 없습니다.");
                            return false;
                        }
                    }
                }
                else if (status == "삭제")
                {
                    isDelete = true;
                }
            }

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
            //데이터베이스 반영
            try
            {
                //1. 기본 셋팅
                PM_OrderResult rst = new PM_OrderResult();
                rst.CorpId = WrGlobal.CorpID;
                rst.OrderNo = Convert.ToString(dr["OrderNo"]);
                rst.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
                rst.SubSeq = dr["SubSeq"].ConvertInt_Null_To_Zero() == 0 ? rst.GetNextSeq() : dr["SubSeq"].ConvertInt_Null_To_Zero(); // 작업 순번
                rst.ProcessCode = Convert.ToString(dr["ProcessCode"]);           // 공정
                rst.MachineCode = Convert.ToString(dr["MachineCode"]);           // 설비
                rst.ItemCode = Convert.ToString(dr["ItemCode"]);         // 품목
                rst.WorkerID = dr["WorkerID"].ConvertString_Null_To_Empty();     // 작업자
                rst.SerialNo = Convert.ToString(dr["SerialNo"]);                                         // 시리얼번호
                rst.WorkDate = dr["WorkDate"].ConvertDate_To_Char8();            // 작업일자
                //시작시간
                if (dr["StartDate"].ToString() == null || dr["StartDate"].ToString() == "") { rst.StartDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm"); }
                else { rst.StartDate = (Convert.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd HH:mm")); }
                //종료시간
                if (dr["EndDate"].ToString() == null || dr["EndDate"].ToString() == "") { rst.EndDate = Convert.ToString(dr["EndDate"]); }
                else { rst.EndDate = (Convert.ToDateTime(dr["EndDate"]).ToString("yyyy-MM-dd HH:mm")); }
                rst.WorkQty = dr["WorkQty"].ConvertInt_Null_To_Zero();           // 생산수량
                rst.GoodQty = dr["GoodQty"].ConvertInt_Null_To_Zero();           // 양품수량
                rst.NgQty = dr["NgQty"].ConvertInt_Null_To_Zero();               // 불량수량
                if (dr["LotNo"].ToString() == null || dr["LotNo"].ToString() == "") { rst.LotNo = ""; }
                else { rst.LotNo = (Convert.ToDateTime(dr["LotNo"]).ToString("yyyy-MM-dd")); }


                //하위 자재 재고 조회 후 실적 진행 여부 선택
                //if (div == 'I' || div == 'U')
                //{
                //    DataTable dtStock = CheckMaterialStock(rst.OrderNo, rst.ItemCode, rst.WorkQty);
                //
                //    if (dtStock != null && dtStock.Rows.Count > 0)
                //    {
                //        string msg = $"재고가 부족한 자재가 {dtStock.Rows.Count}종 있습니다.\n\n";
                //
                //        foreach (DataRow drStock in dtStock.Rows)
                //        {
                //            msg += $"{drStock["MItemCode"]}  부족수량 : {drStock["LackQty"]}\n";
                //        }
                //
                //        msg += "\n생산을 진행하시겠습니까?";
                //
                //        DialogResult result = MessageBox.Show(msg, "재고부족", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        if (result == DialogResult.No)
                //        {
                //            return false;
                //        }
                //    }
                //}

                //2. DB 적용
                if (div == 'I')
                {
                    // 시리얼번호 채번
                    string serialNo = "SN" + code.GetAutoNumber("바코드", Convert.ToDateTime(dr["WorkDate"]));
                    rst.SerialNo = serialNo;

                    // [POP_UpdateWorkingStatus_NB] 프로시저
                    // 생산실적 등록
                    if (!rst.Ins_PM_OrderResult())
                    {
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    // 생산실적 수정
                    if (!rst.Upd_PM_OrderResult1())
                    {
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //수불삭제
                    ST_StockD std = new ST_StockD();

                    if (rst.OrderNo == "")
                        std.JobNo = rst.SerialNo;
                    else
                        std.JobNo = rst.OrderNo;
                    std.JobSeq = rst.OrderSeq;
                    std.JobSeq2 = rst.SubSeq;

                    std.DeleteStockMD_Work();

                    //자재출고내역 삭제
                    BM_SalesD sd = new BM_SalesD();
                    sd.DeleteSalesMD_Work(rst.SerialNo);

                    //불량 삭제
                    QM_WorkDefect defect = new QM_WorkDefect();
                    defect.SerialNo = rst.SerialNo;
                    defect.DelQM_WorkDefect_BySerialNo();

                    // 생산실적 삭제
                    if (!rst.Del_PM_OrderResult())
                    {
                        return false;
                    }
                    // 공정지시 리셋
                    if (!rst.Reset_PM_OrderProcess())
                    {
                        return false;
                    }
                    // 작업지시 리셋
                    if (!rst.Reset_PM_ProductPlan())
                    {
                        return false;
                    }
                }

                //신규 or 수정
                if(div =='I' || div == 'U')
				{
                    // 공정지시 업데이트
                    if (!rst.Upd_PM_OrderProcess())
                    {
                        return false;
                    }
                    // 작업지시 업데이트(사내공정 마지막 라우팅 순번)
                    if (!rst.Upd_PM_ProductPlan())
                    {
                        return false;
                    }

                    // 수불등록
                    //기존 재고 등록 내역 삭제
                    ST_StockM stm = new ST_StockM();
                    ST_StockD std = new ST_StockD();
                    std.JobNo = rst.OrderNo;
                    std.JobSeq = rst.OrderSeq;
                    std.JobSeq2 = rst.SubSeq;

                    std.DeleteStockMD_Work();

                    //생산재고 입고창고 구하기
                    query = $@"
                    SELECT TOP 1
                        CI.ItemType
                        , CASE WHEN EXISTS
                        (
                            SELECT 1
                            FROM PM_OrderProcess OP
                            WHERE OP.CorpId = CI.CorpId
                              AND OP.OrderNo = '{rst.OrderNo}'
                              AND OP.OrderSeq = {rst.OrderSeq}
                              AND OP.OrderSeq =
                              (
                                  SELECT MAX(OP2.OrderSeq)
                                  FROM PM_OrderProcess OP2
                                  WHERE OP2.CorpId = OP.CorpId
                                    AND OP2.OrderNo = OP.OrderNo
                              )
                        ) THEN 'Y' ELSE 'N' END AS FinalYn
                    FROM CM_Item CI
                    WHERE CI.CorpId = '{WrGlobal.CorpID}'
                      AND CI.ItemCode = '{rst.ItemCode}'
                    ";

                    DataRow drItem = db.GetDataRow(query);
                    string toHouse = "S0010";

                    if (drItem != null
                        && drItem["ItemType"].ConvertString_Null_To_Empty() == "20"
                        && drItem["FinalYn"].ConvertString_Null_To_Empty() == "Y")
                    {
                        toHouse = "S0002";
                    }

                    //자재수불 + 자재출고 처리
                    if (WrGlobal.ProcOption["PD"]["ProcResult_MatOut_AutoYN"] == "Y")
                    {
                        //기존 자재출고 데이터 삭제
                        BM_SalesD sd = new BM_SalesD();
                        sd.DeleteSalesMD_Work(rst.SerialNo);

                        MaterialOutByLotNo(rst.ItemCode, rst.WorkQty, rst.WorkDate, rst.OrderNo, rst.OrderSeq, rst.SubSeq, rst.SerialNo);
					}

					//생산재고 입고처리
					stm = new ST_StockM();
					stm.InOutNo = "ST" + code.GetAutoNumber("재고", Convert.ToDateTime(dr["WorkDate"]));
					stm.OutDate = rst.WorkDate;
					stm.ToHouseCode = toHouse;
					stm.WriteID = WrGlobal.LoginID;
					stm.Ins_ST_StockM();

					std = new ST_StockD();
					std.InOutNo = stm.InOutNo;
					std.Seq = 1;
					std.InOutDate = rst.WorkDate;
					std.ToHouseCode = toHouse;
					std.Type = "생산";
					std.ItemCode = rst.ItemCode;

                    if (rst.OrderNo == "")  //지시없이 등록할 경우 JobNo에 SerialNo
                        std.JobNo = rst.SerialNo;
                    else
                        std.JobNo = rst.OrderNo;

                    std.JobSeq = rst.OrderSeq;
					std.JobSeq2 = rst.SubSeq;
					std.Qty = rst.GoodQty;
					std.LotNo = rst.SerialNo;
					std.Status = "완료";
					std.Ins_ST_StockD();
				}

				return true;
			}
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool MaterialOutByLotNo(string InOutNo, string SalesNo, string ItemCode, decimal NeedQty, string WorkDate, string fromHouse, string OrderNo, int OrderSeq, int SubSeq, string SerialNo)
        {
            // LotNo별 자재 자동출고 프로시저
            query = $@" EXEC MaterialOutByLotNo
                                                '{WrGlobal.CorpID}'
                                              , '{InOutNo}'
                                              , '{SalesNo}'
                                              , '{ItemCode}'
                                              , '{NeedQty}'
                                              , '{WorkDate}'
                                              , '{fromHouse}'
                                              , '{OrderNo}'
                                              , '{OrderSeq}'
                                              , '{SubSeq}'
                                              , '{WrGlobal.LoginID}'
                                              , '{WrGlobal.ClientHostName}'
                                              , '{SerialNo}'
                                    ";

            return db.ExecuteQuery(query);
        }

        public bool MaterialOutByLotNo(string ItemCode, decimal WorkQty, string WorkDate, string OrderNo, int OrderSeq, int SubSeq, string SerialNo)
        {
            query = $@"EXEC PM_MaterialOutByLotNo
                                  '{WrGlobal.CorpID}'
                                , '{OrderNo}'
                                , '{OrderSeq}'
                                , '{SubSeq}'
                                , '{SerialNo}'
                                , '{ItemCode}'
                                , '{WorkQty}'
                                , '{WrGlobal.LoginID}'
                                , '{WorkDate}'
                            ";

            return db.ExecuteQuery(query);
        }

        public DataTable CheckMaterialStock(string orderNo, string ItemCode, decimal workQty)
        {
            query = $@"    SELECT
                                B.MItemCode,
                                M.ItemName,
                                B.NeedQty,
                                ISNULL(S.StockQty,0) StockQty,
                                B.NeedQty - ISNULL(S.StockQty,0) AS LackQty
                            FROM
                            (
                                SELECT
                                    bom.MItemCode,
                                    (bom.WorkQty * 1.0 / bom.CalcQty) * {workQty} AS NeedQty
                                FROM PM_SdRoute sd
                                LEFT JOIN PM_SdRoute bom
                                    ON sd.CorpId = bom.CorpId
                                    AND sd.OrderNo = bom.OrderNo
                                    AND sd.ChildID = bom.ParentID
                                WHERE sd.CorpId = '{WrGlobal.CorpID}'
                                AND sd.OrderNo = '{orderNo}'
                                AND sd.MItemCode = '{ItemCode}'
                                AND bom.MItemCode IS NOT NULL
                            ) B
                            LEFT JOIN (
                                SELECT ItemCode, SUM(CurrentQty) StockQty
                                FROM StockByLotNoView
                                GROUP BY ItemCode
                            ) S
                            ON B.MItemCode = S.ItemCode
                            LEFT JOIN CM_Item M
                            ON B.MItemCode = M.ItemCode
                            WHERE ISNULL(S.StockQty,0) < B.NeedQty
                            ";

            return db.GetDataTable(query);
        }

        #endregion

        #region Event
        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
            string colName = e.Column.FieldName;

            int workQty = gv.GetFocusedRowCellValue("WorkQty").ConvertInt_Null_To_Zero();
            int goodQty = gv.GetFocusedRowCellValue("GoodQty").ConvertInt_Null_To_Zero();
            int ngQty = gv.GetFocusedRowCellValue("NgQty").ConvertInt_Null_To_Zero();

            workQty = Math.Max(workQty, 0);
            goodQty = Math.Max(goodQty, 0);
            ngQty = Math.Max(ngQty, 0);

            if (Convert.ToString(drChanged["WorkQty"]).Contains("-") || Convert.ToString(drChanged["GoodQty"]).Contains("-") || Convert.ToString(drChanged["NgQty"]).Contains("-"))
            {
                MessageBox.Show("마이너스 수량은 입력할 수 없습니다.");
                return;
            }

            //0. 수정사항 있을시 상태값 수정 표기
            if (colName == gv.Columns["Status"].FieldName) return;

            if(gv == Gv_PM_ProductPlan)
			{
                gv.SetRowCellValue(e.RowHandle, "Status", "신규");
            }
			else
			{
                grid.Change_Status(gv, e.RowHandle);
			}

            switch (colName)
            {
                case "WorkQty":

                    drChanged["GoodQty"] = Math.Max(workQty - ngQty, 0);
                    break;

                case "GoodQty":

                    if (workQty != 0)
                    {
                        drChanged["NgQty"] = Math.Max(workQty - goodQty, 0);
                    }
                    else
                    {
                        drChanged["WorkQty"] = goodQty + ngQty;
                    }
                    break;

                case "NgQty":

                    if (Convert.ToString(drChanged["WorkQty"]) == "")
                    {
                        MessageBox.Show("생산수량을 입력해주세요."); return;
                    }
                    drChanged["GoodQty"] = Math.Max(workQty - ngQty, 0);
                    break;
            }
        }

        private void cmdExcel_Click()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Gv_ProcessResult.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Event

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1.이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            GridView gv = Gv_ProcessResult;

            //비활성화
            if (tabbedControlGroup1.SelectedTabPageIndex == 0)
            {
                MessageBox.Show("작업지시 조회상태에서 행조작 작업을 할 수 없습니다.");
                return;
            }

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("WorkDate", DateTime.Now.Date);
                dic.Add("ProcessCode", DefaultProcessCode); //공정 디폴트값 올덴은 PR002, 공정구분 20
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString((gv as GridView).GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_ProcessResult, dic, txt_Act);
        }

        private void tabbedControlGroup1_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            Search();
        }

        private void Btn_Bad_Detail_CliCk(object sender, ButtonPressedEventArgs e)
        {
            GridView gv = Gc_ProcessResult.MainView as GridView;
            if (gv == null) return;
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            if (SelectType.ToUpper() == "WO")
            {
                MessageBox.Show("지시조회 상태에서는 불량을 등록할 수 없습니다."); return;
            }
            else
            {
                if (dr["WorkQty"] == null || Convert.ToString(dr["WorkQty"]) == "")
                {
                    MessageBox.Show("생산수량이 없습니다."); return;
                }

                string colName = gv.FocusedColumn.FieldName;

                //유형별 불량수량
                if (colName == "Bad_Qty_Detail")
                {
                    Frm_PM_BadQty frm = new Frm_PM_BadQty(dr);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();

                    Search();
                }
            }
        }
    }
}
