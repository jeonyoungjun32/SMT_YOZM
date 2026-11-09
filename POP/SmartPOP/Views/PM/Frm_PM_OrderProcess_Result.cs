using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Models.PM;
using SmartPOP.Common;
using SmartPOP.Model.EQ;
using SmartPOP.Models;
using SmartPOP.Models.CM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.PM
{
    public partial class Frm_PM_OrderProcess_Result : DevExpress.XtraEditors.XtraForm
    {
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        clsCode code = new clsCode();

        string[] MachineDiv = null;
        string mc = string.Empty;

        public Frm_PM_OrderProcess_Result()
        {
            InitializeComponent();

            // 디자인
            Set_Design();

            // 조회
            MachineByID();
            Display_PM_OrderProcess();
        }

        #region 디자인
        private void Set_Design()
        {
            // DateEdit
            Set_DateEdit();

            // 공정 Lup
            Set_Lup();

            // 그리드
            Set_GvPM_OrderProcess(GvPM_OrderProcess);
            Set_GvResult(GvResult);
        }

        private void Set_DateEdit()
        {
            DateTime start = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime end = Convert.ToDateTime(start.AddDays(40).ToString("yyyy-MM-01"));

            DtOrder.smtStartDateTimeValue = start;
            DtOrder.smtEndDateTimeValue = end;

            //캘린더 크기 설정
            rpo.Set_POP_Dtp(DtOrder.smtStartDateEdit);
            rpo.Set_POP_Dtp(DtOrder.smtStartEndEdit);
        }

        private void Set_Lup()
        {
            CM_Process cp = new CM_Process();
            LupProc.smtLookUpDataSource = cp.SelectCM_ProcessLookup();
            LupProc.smtLookUpDataValueMember = "코드";
            LupProc.smtLookUpDataDisplayMember = "공정명";

            //SearchLookUp font size
            LupProc.smtLookUpView.Appearance.Row.Font = new Font("맑은 고딕", 18, FontStyle.Regular);         //본문row
            LupProc.smtLookUpView.Appearance.HeaderPanel.Font = new Font("맑은 고딕", 18, FontStyle.Regular); //헤더
        }

        #region 그리드
        private void Set_GvPM_OrderProcess(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            CM_Process cp = new CM_Process();
            RepositoryItemSearchLookUpEdit lup_Proc = rpo.Create_RepositoryItemSearchLookUp(cp.SelectCM_ProcessLookup(), "코드", "공정명");
            RepositoryItemSearchLookUpEdit Lup_WorkType = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("작업방법", "", ""), "코드", "코드명");

            grid.Create_GvColumn(gv, "작업지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "작업지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "지시일자", "WorkDate");
            grid.Create_GvColumn(gv, "완료요청일", "EndDate");
            grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_Proc);
            grid.Create_GvColumn(gv, "작업방법", "ItemCode", Lup_WorkType);
            grid.Create_GvColumn(gv, "품번코드", "PItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "부적합수량", "NgQty");
            grid.Create_GvColumn(gv, "상태", "Status");

            // 포멧
            grid.SetCol_Type(gv.Columns["OrderQty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkQty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["GoodQty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], cls_Grid.TypeList.TFloat);

            // Visible = false
            gv.Columns["OrderSeq"].Visible = false;
            gv.Columns["EndDate"].Visible = false;
            gv.Columns["ItemCode"].Visible = false;

            // 이벤트
            grid.SetPOPGrid(gv);
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
            gv.CustomDrawCell += gv_CustomDrawCell;
        }

        private void Set_GvResult(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lup_Worker = rpo.Create_RepositoryItemSearchLookUp(code.GetSM_UserList(), "ID", "사용자명");

            grid.Create_GvColumn(gv, "작업순번", "SubSeq");
            grid.Create_GvColumn(gv, "작업일자", "WorkDate");
            grid.Create_GvColumn(gv, "작업시작시간", "StartDate");
            grid.Create_GvColumn(gv, "작업종료시간", "EndDate");
            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");
            grid.Create_GvColumn(gv, "작업자", "WorkerID", lup_Worker);

            // 포멧
            grid.SetCol_Type(gv.Columns["WorkQty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["GoodQty"], cls_Grid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], cls_Grid.TypeList.TFloat);

            //
            grid.SetPOPGrid(gv);
        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            Display_Result();
        }

        private void gv_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            // 셀 스타일 변경 로직
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;

            if (fieldName.Contains("URGENT_YN") == false) return;

            string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

            if (cellValue == "긴급")
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.Red;
            }
        }

        #endregion
        #endregion

        #region 조회
        private void Display_PM_OrderProcess()
        {
            PM_OrderProcess op = new PM_OrderProcess();
            op.StartDate = DtOrder.smtStartDateTimeValue;
            op.EndDate = DtOrder.smtEndDateTimeValue;
            op.ProcessCode = LupProc.smtLookUpValue.ConvertString_Null_To_Empty();
            op.Status = CboState.smtComboBoxValue.ConvertString_Null_To_Empty();
            op.MachineCode = mc;

            GcPM_OrderProcess.DataSource = op.Select_OrderList();
            GvPM_OrderProcess.BestFitColumns();
        }

        private void Display_Result()
        {
            PM_OrderResult orl = new PM_OrderResult();
            orl.OrderNo = GvPM_OrderProcess.GetFocusedRowCellValue("OrderNo").ConvertString_Null_To_Empty();
            orl.OrderSeq = GvPM_OrderProcess.GetFocusedRowCellValue("OrderSeq").ConvertInt_Null_To_Zero();

            GcResult.DataSource = orl.Select_PM_OrderResult_ByNoSeq();
            GvResult.BestFitColumns();
        }

        private void MachineByID()
        {
            EQ_Machine machine = new EQ_Machine();
            machine.ModelNo = WrGlobal.LoginID;

            DataTable dtMachine = machine.Select_Machine_ByID();
            MachineDiv = dtMachine.AsEnumerable().Select(row => row["MachineCode"].ToString()).ToArray();

            if (MachineDiv.Length > 0)
            {
                mc = string.Join("','", MachineDiv);
            }
        }
        #endregion

        #region 이벤트
        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;
            string name = btn.smtButtonStyle.ConvertString_Null_To_Empty();

            switch (name)
            {
                case "조회":
                    Display_PM_OrderProcess();
                    break;

                case "엑셀변환":
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel Worksheets|*.Xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GvPM_OrderProcess.ExportToXls(sfd.FileName);
                        Process.Start(sfd.FileName);
                    }
                    break;

                case "닫기":
                    this.Close();
                    break;

                default: break;
            }
        }
        #endregion
    }
}