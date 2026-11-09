using DevExpress.Utils.DragDrop;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using static SmartPOP.Common.cls_Grid;
using DevExpress.XtraEditors.Controls;
using baseSMT;
//using SmartPOP.Models.Basic;
using SmartPOP.Report;
using DevExpress.XtraReports.UI;
using SmartPOP.Models;
using SmartPOP.Models.PM;

namespace SmartPOP.Views.PM
{
    public partial class Frm_PM_OrderResult_Process_Print : DevExpress.XtraEditors.XtraForm
    {
        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();
        clsCode code = new clsCode();

        public Frm_PM_OrderResult_Process_Print()
        {
            InitializeComponent();

            // 디자인
            Set_Design();

            // 조회
            Display_Plan();
        }

        #region 디자인

        private void Set_Design()
        {
            // DateEdit
            Set_DateEdit();

            // 그리드뷰
            Set_GvPlan(GvPlan);
            Set_GvSerial(GvSerialNo);
        }

        private void Set_DateEdit()
        {
            DateTime start = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime end = Convert.ToDateTime(start.AddDays(40).ToString("yyyy-MM-01"));

            DtOrder.smtStartDateTimeValue = start;
            DtOrder.smtEndDateTimeValue = end;

            //달력 크기 키우기
            rpo.Set_POP_Dtp(DtOrder.smtStartDateEdit);
            rpo.Set_POP_Dtp(DtOrder.smtStartEndEdit);
        }

        #region 그리드

        private void Set_GvPlan(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemCheckEdit chk_Use = rpo.Create_rpoChk();
            chk_Use.ValueChecked = "Y";
            chk_Use.ValueUnchecked = "N";
            chk_Use.NullStyle = StyleIndeterminate.Unchecked;
            chk_Use.AllowGrayed = false;

            RepositoryItemSearchLookUpEdit lup_Cust = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", ""), "거래처코드", "거래처명");

            grid.Create_GvColumn(gv, "작업지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시일자", "WorkDate");
            grid.Create_GvColumn(gv, "거래처", "SoldToCode", lup_Cust);
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "규격", "Size");
            grid.Create_GvColumn(gv, "지시수량", "OrderQty");
            grid.Create_GvColumn(gv, "납기일자", "DeliveryDate");
            grid.Create_GvColumn(gv, "상태", "Status");

            // 포맷
            grid.SetCol_Type(gv.Columns["OrderQty"], TypeList.TFloat);

            // 이벤트
            grid.SetPOPGrid(gv);
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
        }

        private void Set_GvSerial(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            grid.Create_GvColumn(gv, "작업일자", "WorkDate");
            grid.Create_GvColumn(gv, "바코드번호", "SerialNo");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");

            //
            grid.SetCol_Type(gv.Columns["GoodQty"], TypeList.TFloat);

            //
            grid.SetPOPGrid(gv);
        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            Display_Serial();
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
                    Display_Plan();
                    break;

                case "엑셀변환":
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel Worksheets|*.Xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GvPlan.ExportToXls(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                    break;

                case "프린터":
                    Open_Print();
                    break;

                default: break;
            }
        }
        #endregion

        #region 조회
        private void Display_Plan()
        {
            PM_ProductPlan wo = new PM_ProductPlan();
            wo.StartDt = DtOrder.smtStartDateTimeValue.ConvertDate_To_Char10();
            wo.EndDt = DtOrder.smtEndDateTimeValue.ConvertDate_To_Char10();
            wo.Status = CboState.smtComboBoxValue.ConvertString_Null_To_Empty();

            GcPlan.DataSource = wo.Select_PM_ProductPlan_CrossSdOrder();
            GvPlan.BestFitColumns();
        }

        private void Display_Serial()
        {
            PM_OrderResult sm = new PM_OrderResult();
            sm.OrderNo = GvPlan.GetFocusedRowCellValue("OrderNo").ConvertString_Null_To_Empty();

            GcSerialNo.DataSource = sm.Select_SerialNo_ByOrderNo();
            GvSerialNo.BestFitColumns();
        }

        private void Open_Print()
        {
            string serial = GvSerialNo.GetFocusedRowCellValue("SerialNo").ConvertString_Null_To_Empty();
            string OrderNo = GvPlan.GetFocusedRowCellValue("OrderNo").ConvertString_Null_To_Empty();
            rptReissueProcMove report = new rptReissueProcMove(serial, OrderNo);

            ReportPrintTool reportTool = new ReportPrintTool(report);
            reportTool.PreviewForm.PrintControl.Zoom = 0.9f;

            reportTool.ShowPreviewDialog();
        }
        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}