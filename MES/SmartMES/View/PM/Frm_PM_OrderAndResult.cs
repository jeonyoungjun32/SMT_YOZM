using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.PM;
using SmartMES.Models.ST;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_OrderAndResult : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        string ItemType = "10,20,30,40,50,60";

        public Frm_PM_OrderAndResult()
        {
            InitializeComponent();
        }

        private void Frm_PM_OrderAndResult_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();

            //버튼 권한
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), panel4);

            // 버튼 Visible 처리
            if (Database.DbName.Contains("RNDZIPPER"))
            {
                cmdPrint.Visible = true;
            }
            DisplayPM_ProductPlan();
        }

        #region 디자인

        private void SetLup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Material.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");
            Lup_Material.smtLookUpDataDisplayMember = "품명";
            Lup_Material.smtLookUpDataValueMember = "품목코드";
            Lup_Material.smtLookUpValue = "ALL";

            Lup_Process.smtLookUpDataSource = code.GetProcess_List_ProcessType(true, new string[] { "10", "20" });
            Lup_Process.smtLookUpDataDisplayMember = "공정명";
            Lup_Process.smtLookUpDataValueMember = "공정코드";
            Lup_Process.smtLookUpValue = "ALL";
        }

        private void SetGridView()
        {
            SetWorOrder(GvPM_ProductPlan);
            SetProcessResult(GvProcessResult);
        }

        private void SetWorOrder(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();

            //라인
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");
            //공정
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //품목
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", ItemType, "", ""), "품목코드", "품명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "지시상태", "SRStatus");
            grid.Create_GvColumn(gv, "지시일자", "OrderDate");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "비고", "Remark");

            // 입력부
            grid.Column_AllowEdit(gv.Columns["ItemCode"]);
            grid.Column_AllowEdit(gv.Columns["LineCode"]);
            grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            grid.Column_AllowEdit(gv.Columns["MachineCode"]);
            grid.Column_AllowEdit(gv.Columns["OrderQty"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["OrderDate"]);
            grid.Column_AllowEdit(gv.Columns["btnDelete"]);

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            if (Database.DbName.Contains("KNP"))
            {
                gv.Columns["MachineCode"].Visible = false;
            }

            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

		private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
		{
            DisplayProcessResult();
        }

		private void SetProcessResult(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			gv.OptionsView.ColumnAutoWidth = false;

            // 그리드 헤더 줄 바꿈 <Br>
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            clsRepository rpo = new clsRepository();

            //설비
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //거래처
            RepositoryItemSearchLookUpEdit lupCust = rpo.Create_RepositoryItemSearchLookUp(code.GetCustomer(), "거래처코드", "거래처명");
            //작업자
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false, null), "코드", "명칭");

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
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);

            grid.Create_GvColumn(gv, "작업일자", "WorkDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");
            grid.Create_GvColumn(gv, "유형별<Br>불량수량", "Bad_Qty_Detail", btn);
            grid.Create_GvColumn(gv, "작업자", "WorkerID", lup_Emp);
            grid.Create_GvColumn(gv, "시작시간", "StartDate");
            grid.Create_GvColumn(gv, "종료시간", "EndDate");
            grid.Create_GvColumn(gv, "비고", "Remark");

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

            //컬럼 숨김
            gv.Columns["OrderNo"].Visible = false;
            gv.Columns["OrderSeq"].Visible = false;
            gv.Columns["MachineCode"].Visible = false;

            //입력부
            grid.Column_AllowEdit(gv.Columns["WorkQty"]);
            grid.Column_AllowEdit(gv.Columns["GoodQty"]);
            grid.Column_AllowEdit(gv.Columns["NgQty"]);
            grid.Column_AllowEdit(gv.Columns["Bad_Qty_Detail"]);
            grid.Column_AllowEdit(gv.Columns["WorkDate"]);
            grid.Column_AllowEdit(gv.Columns["WorkerID"]);
            grid.Column_AllowEdit(gv.Columns["StartDate"]);
            grid.Column_AllowEdit(gv.Columns["EndDate"]);

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TInt);

            grid.SetCol_Summary(gv, "WorkQty");
            grid.SetCol_Summary(gv, "GoodQty");
            grid.SetCol_Summary(gv, "NgQty");

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineCode"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            GvProcessResult.CellValueChanged += GvProcessResult_CellValueChanged;
        }

		#endregion

		#region 조회


		private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayPM_ProductPlan();

            if (((DataView)GvPM_ProductPlan.DataSource).Count == 0)
            {
                GvProcessResult.GridControl.DataSource = null;
            }
        }

        private void DisplayPM_ProductPlan()
		{
            this.Validate();
            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();

            PM_OrderProcess op = new PM_OrderProcess();
            op.ItemCode = Lup_Material.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Material.smtLookUpValue;
            op.ProcessCode = Lup_Process.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;

            GcPM_ProductPlan.DataSource = op.Select_OrderPropcess_Indiv(startDate, endDate);

            if (Database.DbName.ToUpper().Contains("DOOIL"))
            {
                //포장 제외
                GcPM_ProductPlan.DataSource = op.Select_OrderPropcess_DOOIL(startDate, endDate);
            }

            GvPM_ProductPlan.BestFitColumns();
        }

        private void DisplayProcessResult()
        {
            GridView gv = GcPM_ProductPlan.MainView as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            PM_OrderResult rst = new PM_OrderResult();
            rst.OrderNo = Convert.ToString(dr["OrderNo"]);
            rst.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();

            GcProcessResult.DataSource = rst.Select_PM_OrderResult_Indiv();
            GvProcessResult.BestFitColumns();
        }

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
                if (Convert.ToString(dr["Status"]) == "신규")
                {
                    if (Convert.ToString(dr["WorkDate"]) == "")
                    {
                        MessageBox.Show("작업일자 항목이 비어있습니다.");
                        return false;
                    }

                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정") { checkCnt++; }
                else if (Convert.ToString(dr["Status"]) == "삭제")
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
            GridView gv = GcPM_ProductPlan.MainView as GridView;

            DataRow orderRow = gv.GetFocusedDataRow();
            if (orderRow == null) return false;

            PM_OrderResult rst = new PM_OrderResult();

            //데이터베이스에 반영
            //1. 기본 셋팅
            try
            {
                rst.CorpId = WrGlobal.CorpID;
                rst.OrderNo = Convert.ToString(orderRow["OrderNo"]);
                rst.OrderSeq = orderRow["OrderSeq"].ConvertInt_Null_To_Zero();
                rst.SubSeq = dr["SubSeq"].ConvertString_Null_To_Empty() == "" ? rst.GetNextSeq() : dr["SubSeq"].ConvertInt_Null_To_Zero(); // 작업 순번
                rst.SerialNo = Convert.ToString(dr["SerialNo"]);

                rst.WorkQty = dr["WorkQty"].ConvertInt_Null_To_Zero();           // 생산수량
                rst.GoodQty = dr["GoodQty"].ConvertInt_Null_To_Zero();           // 양품수량
                rst.NgQty = dr["NgQty"].ConvertInt_Null_To_Zero();               // 불량수량
                if (div != 'D')
                {
                    //Delete제외 공통
                    rst.ItemCode = Convert.ToString(orderRow["ItemCode"]);
                    rst.LineCode = Convert.ToString(orderRow["LineCode"]);
                    rst.ProcessCode = Convert.ToString(orderRow["ProcessCode"]);
                    rst.MachineCode = Convert.ToString(orderRow["MachineCode"]);

                    rst.WorkerID = dr["WorkerID"].ConvertString_Null_To_Empty();     // 작업자
                    rst.WorkDate = dr["WorkDate"].ConvertDate_To_Char8();            // 작업일자
                    //시작시간
                    if (dr["StartDate"].ToString() == null || dr["StartDate"].ToString() == "") { rst.StartDate = Convert.ToString(dr["StartDate"]); }
                    else { rst.StartDate = (Convert.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd HH:mm")); }
                    //종료시간
                    if (dr["EndDate"].ToString() == null || dr["EndDate"].ToString() == "") { rst.EndDate = Convert.ToString(dr["EndDate"]); }
                    else { rst.EndDate = (Convert.ToDateTime(dr["EndDate"]).ToString("yyyy-MM-dd HH:mm")); }
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
                // 시리얼번호 채번
                string serialNo = "SN" + code.GetAutoNumber("바코드", DateTime.Today);
                rst.SerialNo = serialNo;

                //Insert
                // 생산실적 등록
                if (!rst.Ins_PM_OrderResult())
                {
                    return false;
                }
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

                //수불삭제
                ST_StockD std = new ST_StockD();
                std.JobNo = rst.OrderNo;
                std.JobSeq = rst.OrderSeq;
                std.JobSeq2 = rst.SubSeq;

                std.DeleteStockMD_Work();

                // 수불등록
                if (!rst.Stock_MakeStockByWork())
                {
                    return false;
                }

                //프로세스 옵션 : 생산실적 등록 시 하위 품목 자동출고
                if (WrGlobal.ProcOption["PD"]["ProcResult_MatOut_AutoYN"] == "Y")
                {
                    if (!rst.Sales_MakeSalesByWork())
                    {
                        return false;
                    }
                }
            }
            else if (div == 'U')
            {
                // 생산실적 수정
                if (!rst.Upd_PM_OrderResult1())
                {
                    return false;
                }
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

                //수불삭제
                ST_StockD std = new ST_StockD();
                std.JobNo = rst.OrderNo;
                std.JobSeq = rst.OrderSeq;
                std.JobSeq2 = rst.SubSeq;

                std.DeleteStockMD_Work();

                // 수불등록
                if (!rst.Stock_MakeStockByWork())
                {
                    return false;
                }
            }
            else if (div == 'D')
            {
                //Delete
                // 생산실적 삭제
                if (!rst.Del_PM_OrderResult())
                {
                    return false;
                }

                // 지시별 실적 데이터가 없을 때 리셋
                DataTable chkTable = rst.chkPM_OrderResult();
                if (chkTable.Rows.Count == 0)
                {
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
                    return true;
                }
                else
                {
                    // 공정지시 수량 조정
                    if (!rst.Update_PM_OrderProcess_Qty())
                    {
                        return false;
                    }
                }
                //추가 변경사항 private 따로 만들기
            }

            //상태값 초기화
            dr["Status"] = "";

            return true;
        }

        #endregion


        private void GvProcessResult_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (fieldName == gv.Columns[0].FieldName) return;

            //상태값 수정
            grid.Change_Status(gv, e.RowHandle);

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

            switch (fieldName)
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

        private void cmdRowEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow drSdOrder = GvPM_ProductPlan.GetFocusedDataRow();
                if (drSdOrder == null) return;

                //행삽입, 추가, 삭제 버튼 클릭 이벤트
                //1. 이름의 마지막 3글자 가져오기
                smtSearchButton btn = sender as smtSearchButton;
                string txt_Act = btn.Tag.ToString();

                //2. 입력 데이터 설정
                Dictionary<string, object> dic = new Dictionary<string, object>();

                if (txt_Act != "Del")
                {
                    dic.Add("Status", "신규");
                    dic.Add("WorkDate", DateTime.Today);
                }
                else
                {
                    //삭제 진행인경우
                    dic.Add("Status", Convert.ToString(GvProcessResult.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
                }

                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(GcProcessResult, dic, txt_Act);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//end try
        }

        private void Btn_Bad_Detail_CliCk(object sender, ButtonPressedEventArgs e)
        {
            GridView gv = GcProcessResult.MainView as GridView;
            if (gv == null) return;
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            if (dr["WorkQty"] == null || Convert.ToString(dr["WorkQty"]) == "")
            {
                MessageBox.Show("생산수량이 없습니다."); return;
            }

            string colName = gv.FocusedColumn.FieldName;

            //유형별 불량수량
            if (colName == "Bad_Qty_Detail")
            {
                PM.Popup.Frm_BadQty_Popup frm = new PM.Popup.Frm_BadQty_Popup(dr);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();

                DisplayProcessResult();
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
		{
            DataTable dt = null;

            GvProcessResult.PostEditor();
            GvProcessResult.UpdateCurrentRow();
            dt = GcProcessResult.DataSource as DataTable;

            //저장 시작
            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {

                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    //if (dt.Rows[curHandle]["OrderQty"].ConvertInt_Null_To_Zero() == 0) continue;

                    if (!Save_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'D')) return;
                }
            }

            XtraMessageBox.Show(cls_MessageBox.Save_Comp);
            DisplayProcessResult();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            string dbName = Database.DbName;

            if (dbName.Contains("RNDZIPPER"))
            {
                Print_RND();
            }
        }

        private void Print_RND()
        {
            //string _ItemCode = GvPM_ProductPlan.GetFocusedRowCellValue("ItemCode").ConvertString_Null_To_Empty();
            //CM_ItemCombination cc = new CM_ItemCombination();
            //cc.ItemCode = _ItemCode;

            //string _Seq = (cc.Get_MaxSeq() - 1).ConvertString_Null_To_Empty();

            //rptRecipe_RND rpt = new rptRecipe_RND(_ItemCode, _Seq);
            //ReportPrintTool reportTool = new ReportPrintTool(rpt);
            //reportTool.PreviewForm.PrintControl.Zoom = 0.9f;

            //reportTool.ShowPreviewDialog();
        }
    }
}