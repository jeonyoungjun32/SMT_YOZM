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
using SmartMES.Models.PM;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_OrderProcess_Indiv : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();

        string strItemDiv = WrGlobal.ProcOption["SC"]["Product_ItemType"]; //

        public Frm_PM_OrderProcess_Indiv()
        {
            InitializeComponent();
        }

        private void Frm_PM_OrderProcess_Indiv_Load(object sender, EventArgs e)
        {
            SetLup();
            SetGridView();

            //버튼 권한
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);

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
            SetPM_OrderProcess(GvList);
        }

		private void SetPM_OrderProcess(GridView gv)
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
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", "", strItemDiv, "", ""), "품목코드", "품명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "지시상태", "SRStatus");
            grid.Create_GvColumn(gv, "지시일자", "OrderDate");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode", lup_Item);
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line_Cd);
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_ProcessCode);
            grid.Create_GvColumn(gv, "설비", "MachineCode", lupMachine);
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //
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

            //EVENT
            gv.CellValueChanged += Gv_CellValueChanged;
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여

            grid.Set_Navigator(GvList);
		}

		private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): cmdInsert.PerformClick(); break;  //행삽입
                case (Keys.Alt | Keys.D): cmdDelete.PerformClick(); ; break; //행삭제
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

            PM_OrderProcess op = new PM_OrderProcess();
            op.ItemCode = Lup_Material.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Material.smtLookUpValue;

            GcList.DataSource = op.Select_OrderPropcess_Indiv(startDate, endDate);

            GvList.BestFitColumns();
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
                dic.Add("OrderDate", DateTime.Now.ToString("d"));
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(GvList.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(GcList, dic, txt_Act);
        }

        // 출고요청 버튼 클릭
        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            GvList.PostEditor();
            GvList.UpdateCurrentRow();
            dt = GcList.DataSource as DataTable;

            //저장 시작
            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
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
                    if (Convert.ToString(dr["OrderDate"]) == "")
                    {
                        MessageBox.Show("지시일자 항목이 비어있습니다.");
                        return false;
                    }

                    if (Convert.ToString(dr["ItemCode"]) == "")
                    {
                        MessageBox.Show("품목 항목이 비어있습니다.");
                        return false;
                    }

                    if (Convert.ToString(dr["ProcessCode"]) == "")
                    {
                        MessageBox.Show("공정 항목이 비어있습니다.");
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
            PM_OrderProcess op = new PM_OrderProcess();

            try
            {
                op.CorpId = WrGlobal.CorpID;
                op.OrderNo = Convert.ToString(dr["OrderNo"]);      //품목코드

                if (div != 'D')
                {
                    //Delete제외 공통
                    op.OrderSeq = 1;
                    op.OrderDate = dr["OrderDate"].ConvertDate_To_Char10();
                    op.ItemCode = dr["ItemCode"].ToString();
                    op.LineCode = dr["LineCode"].ToString();
                    op.ProcessCode = dr["ProcessCode"].ToString();
                    op.MachineCode = dr["MachineCode"].ToString();
                    op.OrderQty = dr["OrderQty"].ConvertInt_Null_To_Zero();
                    op.Remark = dr["Remark"].ToString();
                    op.Status = "대기";
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
                op.OrderNo = "WO" + code.GetAutoNumber("작업지시", Convert.ToDateTime(op.OrderDate));
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
                if (Convert.ToString(dr["ItemCode"]) == "")
                {
                    MessageBox.Show("품목 항목이 비어있습니다.");
                    return false;
                }

                if (Convert.ToString(dr["ProcessCode"]) == "")
                {
                    MessageBox.Show("공정 항목이 비어있습니다.");
                    return false;
                }

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
                if (!op.Del_ByOrderNo())
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
                GvList.ExportToXls(sfd.FileName);

                Process.Start(sfd.FileName);
            }
        }

        //닫기
        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}