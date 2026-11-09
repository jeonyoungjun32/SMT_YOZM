using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models.PM;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_OrderResult_MatInput : DevExpress.XtraEditors.XtraForm
    {
        //공정실적
        clsGrid grid = new clsGrid();

        public Frm_PM_OrderResult_MatInput()
        {
            InitializeComponent();
            SetDesign();
        }

        private void SetDesign()
        {
            DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));
            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            //GridView 설정
            Set_GridView_Rst(Gv_ProcessResult);
        }

        private void Set_GridView_Rst(GridView gv)
        {
            gv.OptionsFind.AlwaysVisible = false;

            clsGrid grid = new clsGrid();
            clsRepository rpo = new clsRepository();

            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            //gv.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //그리드 기분 중앙 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.IndicatorWidth = 50;                     //순번 너비
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            gv.OptionsView.AllowCellMerge = true;   //셀병합

            grid.Create_GvColumn(gv, "생산일자", "WorkDate");
            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
            grid.Create_GvColumn(gv, "실적순번", "SubSeq");
            grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");

            //grid.Create_GvColumn(gv, "라인코드", "LineCode");
            //grid.Create_GvColumn(gv, "라인명", "LineName");

            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정명", "ProcessName");

			grid.Create_GvColumn(gv, "품목코드", "P_ItemCode");
			grid.Create_GvColumn(gv, "품명", "P_ItemName");
            grid.Create_GvColumn(gv, "품번", "P_Alias");
            grid.Create_GvColumn(gv, "규격", "P_Size");

            grid.Create_GvColumn(gv, "생산수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품수량", "GoodQty");
            grid.Create_GvColumn(gv, "불량수량", "NgQty");

            grid.Create_GvColumn(gv, "출고번호", "SalesNo");
            grid.Create_GvColumn(gv, "출고순번", "SalesSeq");
            grid.Create_GvColumn(gv, "투입품목코드", "M_ItemCode");
            grid.Create_GvColumn(gv, "투입품명", "M_ItemName");
            grid.Create_GvColumn(gv, "HSCODE", "HSCODE");
			//grid.Create_GvColumn(gv, "단위", "M_Unit");
			grid.Create_GvColumn(gv, "LOTNO", "LotNo");
			grid.Create_GvColumn(gv, "출고수량", "Qty");

			//gv.Columns["Line_Cd"].Visible = false;
			//gv.Columns["Line_Nm"].Visible = false;
			gv.Columns["ProcessCode"].Visible = false;
			gv.Columns["P_Alias"].Visible = false;
			gv.Columns["P_Size"].Visible = false;
			//gv.Columns["LotNo"].Visible = false;
			//gv.Columns["GoodQty"].Visible = false;
			//gv.Columns["NgQty"].Visible = false;
			//gv.Columns["OrderNo"].Visible = false;
			//gv.Columns["OrderSeq"].Visible = false;

			//값 형태
			grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);

            //gv.Columns["WorkDate"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            //gv.Columns["InOutNo"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            //gv.Columns["Seq"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

			//gv.Columns["M_ItemCode"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
			//gv.Columns["M_ItemName"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

			//gv.Columns["LotNo"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
			gv.Columns["Qty"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            grid.SetCol_Summary(gv, "WorkQty");
            grid.SetCol_Summary(gv, "Qty");

            //이벤트
            //gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellMerge += Gv_CellMerge;
            gv.CustomSummaryCalculate += Gv_CustomSummaryCalculate;
            gv.Columns["WorkQty"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
        }

        private void Gv_CellMerge(object sender, CellMergeEventArgs e)
        {
            //셀 병합시 예외처리
            GridView gv = sender as GridView;
            if (gv == null) return;

            string serialNo1 = gv.GetRowCellValue(e.RowHandle1, "SerialNo") + "";
            string serialNo2 = gv.GetRowCellValue(e.RowHandle2, "SerialNo") + "";

            bool sameKey = serialNo1 == serialNo2;

            if (!sameKey)
            {
                e.Handled = true;
                e.Merge = false;
                return;
            }
        }

        private void Gv_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if ((e.Item as DevExpress.XtraGrid.GridSummaryItem)?.FieldName != "WorkQty")
                return;

            GridView gv = sender as GridView;

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            {
                e.TotalValue = 0;
            }

            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            {
                HashSet<string> keys = new HashSet<string>();

                decimal total = 0;

                for (int i = 0; i < gv.RowCount; i++)
                {
                    string key =
                        gv.GetRowCellValue(i, "OrderNo").ToString()
                      + gv.GetRowCellValue(i, "OrderSeq").ToString()
                      + gv.GetRowCellValue(i, "SubSeq").ToString();

                    if (!keys.Contains(key))
                    {
                        keys.Add(key);

                        total += Convert.ToDecimal(
                            gv.GetRowCellValue(i, "WorkQty")
                        );
                    }
                }

                e.TotalValue = total;
            }
        }

        private void Frm_PM_OrderResult_MatInput_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "BtnSearch":
                    //공정실적 조회
                    Search();
                    break;
            }
        }

        private void Search()
        {
            string rstFrom = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string rstTo = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();

            PM_OrderResult odr = new PM_OrderResult();
            DataTable dt = odr.SelectMatInput(rstFrom, rstTo);

            Gc_ProcessResult.DataSource = dt;
            Gv_ProcessResult.BestFitColumns();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}