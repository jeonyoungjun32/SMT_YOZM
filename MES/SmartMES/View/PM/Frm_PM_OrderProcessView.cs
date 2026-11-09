using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Models;
using System;
using SmartCommon.Common;
using System.Data;
using DevExpress.XtraEditors.Repository;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using SmartMES.Models.PM;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_OrderProcessView : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        string ItemType = WrGlobal.ProcOption["SC"]["Product_ItemType"];

        public Frm_PM_OrderProcessView()
        {
            InitializeComponent();
        }

        private void frmPM_OrderProcessView_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();


            if (WrGlobal.ProcOption["PD"]["PM_OrderProcess_Priority_YN"] == "Y")
            {
                cmdUp.Visible = true;
                cmdDown.Visible = true;
                cmdSave.Visible = true;
            }

            DisplayData();
        }

        #region setDesign
        private void SetLup()
        {
            DtOrder.smtStartDateTimeValue = DateTime.Now.AddDays(-7);
            DtOrder.smtEndDateTimeValue = DateTime.Now;

            Lup_Material.smtLookUpDataSource = code.smtLookUp("품목", new string[] { "", ItemType }, "전체");

            Lup_Material.smtLookUpDataDisplayMember = "품명";
            Lup_Material.smtLookUpDataValueMember = "품목코드";
            Lup_Material.smtLookUpValue = "ALL";
        }

        private void SetGridView()
        {
            SetGridPM_ProductPlan(GvPM_OrderProcess);
        }

        private void SetGridPM_ProductPlan(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            clsRepository rpo = new clsRepository();

            // Repository: displayMember를 코드로 고정
            RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List(""), "라인코드", "라인명");
            RepositoryItemSearchLookUpEdit lup_material = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(null), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_ProcessCode = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(), "공정코드", "공정명");
            RepositoryItemSearchLookUpEdit lupMachine = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList().Table, "설비코드", "설비명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            // 컬럼 생성 (화면 표시명, 바인딩 필드명, 에디터)
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "구분", "UrgentYn");
            grid.Create_GvColumn(gv, "우선순위", "DisplayNumber");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "지시일자", "OrderDate", rpo.Create_rpoDate());

            grid.Create_GvColumn(gv, "라인명", "LineName");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");
            grid.Create_GvColumn(gv, "설비명", "MachineName");

            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

            // 편집 가능 컬럼 설정
            //grid.Column_AllowEdit(gv.Columns["ItemName"],false);
            //grid.Column_AllowEdit(gv.Columns["OrderDate"]);
            //grid.Column_AllowEdit(gv.Columns["OrderQty"]);
            //grid.Column_AllowEdit(gv.Columns["WorkQty"]);
            //grid.Column_AllowEdit(gv.Columns["GoodQty"]);
            //grid.Column_AllowEdit(gv.Columns["NgQty"]);
            //grid.Column_AllowEdit(gv.Columns["ItemCode"]);
            //grid.Column_AllowEdit(gv.Columns["ProcessCode"]);
            //grid.Column_AllowEdit(gv.Columns["MachineCode"]);

            // 포맷 설정
            grid.SetCol_Type(gv.Columns["OrderDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TInt);

            // 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["UrgentYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["LineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["MachineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // 필요 시 숨김 컬럼 지정 (없으면 생략 가능)
            // gv.Columns["SomeColumn"].Visible = false;

            if (WrGlobal.ProcOption["PD"]["PM_OrderProcess_Priority_YN"] != "Y")
            {
                gv.Columns["DisplayNumber"].Visible = false;
            }

            // 네비게이터 설정
            grid.Set_Navigator(GvPM_OrderProcess);

            gv.CellValueChanged += gv_CellValueChanged;
            gv.CustomDrawCell += gv_CustomDrawCell;
        }

        private void gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
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

        #endregion setDesign

        private void ButtonClick(object sender, EventArgs e)
        {
            baseSMT.smtSearchButton Btn = (baseSMT.smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                //조회
                case "cmdDisplay":
                    DisplayData();
                    break;

                //엑셀다운
                case "cmdExcel":
                    cmdExcel_Click();
                    break;
            }
        }

        #region Event

        private void cmdDown_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            if (btn == null) return;

            GridControl gc = GcPM_OrderProcess;
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

        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            GvPM_OrderProcess.PostEditor();
            GvPM_OrderProcess.UpdateCurrentRow();
            dt = GcPM_OrderProcess.DataSource as DataTable;

            //저장 시작
            //1. 데이터 불러온 상태인지 검사
            //if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            int displayNum = 1;

            //프로세스 옵션 : 작업지시 우선순위 조정여부 사용, 미사용
            if (WrGlobal.ProcOption["PD"]["PM_OrderProcess_Priority_YN"] == "Y")
            {
                //순번 재채번 : 현재는 grid에 조회하는 내역 다 재채번함
                for (int i = 0; i < GvPM_OrderProcess.RowCount; i++)
                {
                    string status = GvPM_OrderProcess.GetRowCellValue(i, "Status").ConvertString_Null_To_Empty();

                    if (status != "삭제") //삭제 아니면 재채번
                    {
                        GvPM_OrderProcess.SetRowCellValue(i, "DisplayNumber", displayNum++);
                    }

                    if (status == "")    //신규, 삭제는 제외
                    {
                        GvPM_OrderProcess.SetRowCellValue(i, "Status", "수정");
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

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            PM_OrderProcess op = new PM_OrderProcess();

            op.CorpId = WrGlobal.CorpID;
            op.OrderNo = dr["OrderNo"].ToString();
            op.ProcessCode = dr["ProcessCode"].ToString();
            op.ItemCode = dr["ItemCode"].ToString();
            op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
            op.LineCode = dr["LineCode"].ConvertString_Null_To_Empty();
            op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
            op.OrderDate = dr["OrderDate"].ConvertDate_To_Char10();
            op.MachineCode = dr["MachineCode"].ToString();
            op.OrderQty = dr["OrderQty"].ConvertInt_Null_To_Zero();
            op.Remark = dr["Remark"].ToString();
            op.Status = "대기";
            op.RoutingSeq = dr["RoutingSeq"].ConvertInt_Null_To_Zero();
            op.DisplayNumber = Convert.ToInt32(dr["DisplayNumber"]);
            if (!dr["UrgentYn"].IsNullOrEmpty())
                op.UrgentYn = "Y";

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

            //상태값 초기화
            dr["Status"] = "";

            return true;
        }

        //엑셀변환 하기
        private void cmdExcel_Click()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Worksheets|*.Xls";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                GvPM_OrderProcess.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Event

        #region 조회
        private void DisplayData()
        {
			string startDate = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char8();
			string endDate = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char8();

            PM_OrderProcess op = new PM_OrderProcess();
            op.ItemCode = Lup_Material.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Material.smtLookUpValue;

			GcPM_OrderProcess.DataSource = op.Select_OrderPropcess(startDate, endDate);

			GvPM_OrderProcess.BestFitColumns();
		}
        #endregion 조회
    }
}