using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Diagnostics;
using SmartCommon.Common;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using baseSMT;
using System.Collections.Generic;
using DevExpress.XtraGrid;
using System.Drawing;
using SmartMES.Models.PM;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_ProductPlan_Indiv : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        //string strItemDiv = WrGlobal.ProcOption["SC"]["Product_ItemType"];
        string strItemDiv = "30";   //요즘 : 반제품만 나오도록

        public Frm_PM_ProductPlan_Indiv()
        {
            InitializeComponent();
        }

        private void frmBomProductList_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();

            //버튼 권한
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);

            //프로세스 옵션 : 생산계획 우선순위 조정여부 사용, 미사용
            if(WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] == "Y")
			{
                cmdUp.Visible = true;
                cmdDown.Visible = true;
			}

            DisplayData();
        }

        private void SetLup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Material.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", strItemDiv }, "전체");

            Lup_Material.smtLookUpDataDisplayMember = "품명";
            Lup_Material.smtLookUpDataValueMember = "품목코드";
            Lup_Material.smtLookUpValue = "ALL";
        }

        private void SetGridView()
        {
            SetPM_ProductPlan(GvPM_ProductPlan);
        }

		private void Gv_CellValueChanged1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            gv.SelectRow(e.RowHandle);
        }

		private void Gv_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            int idx = e.ControllerRow;

            if(e.Action == System.ComponentModel.CollectionChangeAction.Add)
			{
                gv.SetRowCellValue(idx, "Status", "신규");
			}
			else
			{
                gv.SetRowCellValue(idx, "Status", "");
			}
		}

		private void SetPM_ProductPlan(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = false;

			clsRepository rpo = new clsRepository();

			// 품목
            //RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] { "20" }), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] { strItemDiv }), "품목코드", "품명");

            RepositoryItemSearchLookUpEdit lup_Class1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", false), "코드", "명칭");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "구분", "UrgentYn");
            grid.Create_GvColumn(gv, "계획순번", "WorkSeq");
			grid.Create_GvColumn(gv, "계획번호", "OrderNo");
			grid.Create_GvColumn(gv, "계획일자", "WorkDate", rpo.Create_rpoDate());

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "계획수량", "OrderQty");
			grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "등록자", "CreId");
			grid.Create_GvColumn(gv, "등록IP", "CreIP");
			grid.Create_GvColumn(gv, "등록일시", "CreDt");
			grid.Create_GvColumn(gv, "수정자", "ModId");
			grid.Create_GvColumn(gv, "수정IP", "ModIP");
			grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //
            gv.Columns["ItemName"].ColumnEdit = lup_Item;
            gv.Columns["ItemClass1"].ColumnEdit = lup_Class1;
            gv.Columns["ItemClass2"].ColumnEdit = lup_Class2;
            gv.Columns["ItemClass3"].ColumnEdit = lup_Class3;

            //VISIBLE
            //프로세스 옵션 : 생산계획 우선순위 조정여부 사용, 미사용
            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] != "Y")
            {
                gv.Columns["WorkSeq"].Visible = false;
            }
            gv.Columns["UrgentYn"].Visible = false;

            //
			grid.Column_AllowEdit(gv.Columns["WorkDate"]);
			grid.Column_AllowEdit(gv.Columns["ItemName"]);
			grid.Column_AllowEdit(gv.Columns["OrderQty"]);
			grid.Column_AllowEdit(gv.Columns["Remark"]);

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["UrgentYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //EVENT
            gv.CellValueChanged += Gv_CellValueChanged;
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.CustomDrawCell += gv_CustomDrawCell;

            grid.Set_Navigator(GvPM_ProductPlan);
		}

		private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;

            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            string value = e.Value.ConvertString_Null_To_Empty();
            string fieldName = e.Column.FieldName;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            if(fieldName == "ItemName")
			{
                grid.SetInfo_Item(dr, value);
			}

            gv.BestFitColumns();
        }

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): cmdInsert.PerformClick(); break;  //행삽입
                case (Keys.Alt | Keys.D): cmdDelete.PerformClick(); ; break; //행삭제
            }
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

        private void CmdSearch_Click_1(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void DisplayData()
        {
            this.Validate();
            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();

            PM_ProductPlan wo = new PM_ProductPlan();
            wo.ItemCode = Lup_Material.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Material.smtLookUpValue;
            string itemDiv = strItemDiv;

            GcPM_ProductPlan.DataSource = wo.Select_PM_ProductPlan_Indiv(startDate, endDate, strItemDiv);
            GvPM_ProductPlan.BestFitColumns();
            GvPM_ProductPlan.ClearSorting();
        }

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
                dic.Add("WorkDate", DateTime.Now.ToString("d"));
            }
            else
            {
                if (Convert.ToString(GvPM_ProductPlan.GetFocusedRowCellValue("Status")) != "삭제")
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
                dic.Add("Status", Convert.ToString(GvPM_ProductPlan.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(GcPM_ProductPlan, dic, txt_Act);
        }

        // 출고요청 버튼 클릭
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
            int maxWorkSeq = 1;

            //프로세스 옵션 : 생산계획 우선순위 조정여부 사용, 미사용
            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] == "Y")
            {
				//계획 순번 재채번 : 현재는 grid에 조회하는 내역 다 재채번함
				for (int i = 0; i < GvPM_ProductPlan.RowCount; i++)
				{
                    string status = GvPM_ProductPlan.GetRowCellValue(i, "Status").ConvertString_Null_To_Empty();
                    string orderNo = GvPM_ProductPlan.GetRowCellValue(i, "OrderNo").ConvertString_Null_To_Empty();

                    //if (status == "신규")
                    //{
                    //GvPM_ProductPlan.SetRowCellValue(i, "WorkSeq", DBNull.Value);
                    //}
                    //else 
                    if (status == "" || status == "수정")
                    {
                        if (orderNo != "")
                        {
                            GvPM_ProductPlan.SetRowCellValue(i, "WorkSeq", maxWorkSeq++);
                            //GvPM_ProductPlan.SetRowCellValue(i, "Status", "수정");
                        }

                    }
                }
            }

            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (dt.Rows[curHandle]["OrderQty"].ConvertInt_Null_To_Zero() == 0) continue;

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


            DisplayData();

            XtraMessageBox.Show(cls_MessageBox.Save_Comp, cls_MessageBox.SaveCaption);
        }

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
                        MessageBox.Show("계획일자 항목이 비어있습니다.");
                        return false;
                    }

                    if (Convert.ToString(dr["ItemCode"]) == "")
                    {
                        MessageBox.Show("품목 항목이 비어있습니다.");
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
            //데이터베이스에 반영
            //1. 기본 셋팅
            PM_ProductPlan wo = new PM_ProductPlan();

            try
            {
                wo.CorpId = WrGlobal.CorpID;
                wo.OrderNo = Convert.ToString(dr["OrderNo"]);      //품목코드

                if (div != 'D')
                {
                    //Delete제외 공통
                    wo.WorkDate = dr["WorkDate"].ConvertDate_To_Char10();
                    wo.ItemCode = Convert.ToString(dr["ItemCode"]);
                    wo.OrderQty = dr["OrderQty"].ConvertInt_Null_To_Zero();
                    wo.SdOrderNo = Convert.ToString(dr["SdOrderNo"]);
                    wo.SdOrderSeq = dr["SdOrderSeq"].ConvertInt_Null_To_Zero();
                    wo.Remark = Convert.ToString(dr["Remark"]);
                    wo.WorkSeq = dr["WorkSeq"].ConvertInt_Null_To_Zero();
                    if (!dr["UrgentYn"].IsNullOrEmpty())
                        wo.UrgentYn = "Y";
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
                wo.OrderNo = "WO" + code.GetAutoNumber("작업지시", Convert.ToDateTime(wo.WorkDate));
                //Insert
                if (!wo.Insert_PM_ProductPlan())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                if (!wo.Insert_PM_SdRoute())
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
            dr["Status"] = "";

            return true;
        }


        //엑셀변환 하기
        private void cmdExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GvPM_ProductPlan.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

		private void cmdDown_Click(object sender, EventArgs e)
		{
            SimpleButton btn = sender as SimpleButton;
            if (btn == null) return;

            GridControl gc = GcPM_ProductPlan;
            if (gc == null) return;

            GridView gv = gc.MainView as GridView;
            if (gv == null) return;

            DataTable dt = gc.DataSource as DataTable;
            if (dt == null) return;
            if (dt.Rows.Count <= 1) return;

            int idx = gv.FocusedRowHandle;

            DataRow curRow = null;
            DataRow newRow = null;

            switch (btn.Name)
            {
                case "cmdUp":

                    if (idx == 0) return;   // Top일 경우 종료

                    curRow = dt.Rows[idx];
                    newRow = dt.NewRow();

                    newRow.ItemArray = curRow.ItemArray; // 내용 복사.

                    if (newRow["Status"].ConvertString_Null_To_Empty() != "신규")
                    {
                        newRow["Status"] = "수정";
                    }

                    dt.Rows.RemoveAt(idx);

                    gv.SetRowCellValue(idx - 1, "Status", "수정");
                    dt.Rows.InsertAt(newRow, idx - 1);

                    if (idx == dt.Rows.Count - 1)
                    {
                        gv.FocusedRowHandle = gv.FocusedRowHandle - 1;
                    }
                    else
                    {
                        gv.FocusedRowHandle = gv.FocusedRowHandle - 2;
                    }

                    break;

                case "cmdDown":

                    if (idx == dt.Rows.Count - 1) return; // Bottom일 경우 종료

                    curRow = dt.Rows[idx];
                    newRow = dt.NewRow();

                    newRow.ItemArray = curRow.ItemArray; // 내용 복사.

                    if (newRow["Status"].ConvertString_Null_To_Empty() != "신규")
                    {
                        newRow["Status"] = "수정";
                    }

                    dt.Rows.RemoveAt(idx);

                    gv.SetRowCellValue(idx, "Status", "수정");
                    dt.Rows.InsertAt(newRow, idx + 1);

                    gv.FocusedRowHandle++;

                    break;
            }

            gv.SelectRow(gv.FocusedRowHandle);

        }

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}