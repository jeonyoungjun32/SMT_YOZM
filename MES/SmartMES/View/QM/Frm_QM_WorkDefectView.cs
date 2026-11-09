using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Common;
using SmartMES.Common;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
	public partial class Frm_QM_WorkDefectView : DevExpress.XtraEditors.XtraForm
    {
        WrComFunc func = new WrComFunc();
        Database db = new Database();
        clsCode code = new clsCode();
        string query = "";
        clsGrid grid = new clsGrid();
        cls_Lookup clsLup = new cls_Lookup();

        private string[] ItemDiv = { "20", "30" }; //품목구분 리스트(10 : 상품, 20 : 제품, 30 : 반제품, 40 : 구매자재, 50 : 부자재, 60 : 샘플/개발)

        public Frm_QM_WorkDefectView()
        {
            InitializeComponent();
        }

        private void frmQM_WorkDefectView_Load(object sender, EventArgs e)   //FrmManufacturePlan_Load
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            SetLup();

            Set_GvOrderProgress(GvPM_OrderProcess);
            //Set_GvPM_OrderResult(GvPM_OrderResult);
            Set_GvRework(GvQM_WorkDefect);

            DisplayData();
        }

        private void SetLup()
		{
            //공정
            DataTable dt_process = code.GetProcess_List_ProcessType(true, null); //

            clsLup.Create_RepositoryItemSearchLookUp(Lup_Process, dt_process, "공정코드", "공정명");
            Lup_Process.smtLookUpValue = "ALL";

            //품목
            DataTable dt_item = code.GetMaterialList(ItemDiv, true).Table;
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Item, dt_item, "품목코드", "품명");
            //grid.SetLupView_Item(LupItem, "제품코드 초기화");
            Lup_Item.smtLookUpValue = "ALL";
            //LupItem.Properties.PopupFormSize = new System.Drawing.Size(1000, 0);
        }

        private void Set_GvOrderProgress(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            //헤더 멀티라인 설정
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
            grid.Create_GvColumn(gv, "공정", "ProcessName");
            grid.Create_GvColumn(gv, "라인코드", "LineCode");
            grid.Create_GvColumn(gv, "라인", "LineName");
            grid.Create_GvColumn(gv, "작업지시<Br>번호", "OrderNo");
            grid.Create_GvColumn(gv, "작업지시<Br>순번", "OrderSeq");
            grid.Create_GvColumn(gv, "제품코드", "ItemCode");
            grid.Create_GvColumn(gv, "제품명", "ProductName");
            grid.Create_GvColumn(gv, "품번", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName");
            grid.Create_GvColumn(gv, "작업지시<Br>수량", "OrderQty");
            grid.Create_GvColumn(gv, "생산<Br>수량", "WorkQty");
            grid.Create_GvColumn(gv, "양품<Br>수량", "GoodQty");
            grid.Create_GvColumn(gv, "부적합<Br>수량", "NgQty");
            grid.Create_GvColumn(gv, "시작시간", "StartDate");
            grid.Create_GvColumn(gv, "종료시간", "EndDate");


            //VISIBLE
            gv.Columns["ProcessCode"].Visible = false;
            gv.Columns["LineCode"].Visible = false;
            gv.Columns["LineName"].Visible = false;
            gv.Columns["StartDate"].Visible = false;
            gv.Columns["EndDate"].Visible = false;

            //WIDTH
            gv.Columns["OrderSeq"].MinWidth = 70;

            //TEXT OPTIONS
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ProcessName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["LineName"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //TEXT FORMAT
            gv.Columns["StartDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gv.Columns["StartDate"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            gv.Columns["EndDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gv.Columns["EndDate"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            grid.SetCol_Type(gv.Columns["OrderQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["WorkQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);

            gv.FocusedRowObjectChanged += GvOrder_FocusedRowObjectChanged;
        }


        private void GvOrder_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null) return;

            DisplayDefective();
        }

		private void Set_GvRework(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            //grid.Create_GvColumn(gv, "지시번호", "OrderNo");
            //grid.Create_GvColumn(gv, "지시순번", "Seq");
            //grid.Create_GvColumn(gv, "작업순번", "SubSeq");
            //grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");

            grid.Create_GvColumn(gv, "불량코드", "DefectCode");
            grid.Create_GvColumn(gv, "불량명", "DefectName");
            grid.Create_GvColumn(gv, "불량수량", "Qty");

            //VISIBLE
            //gv.Columns["OrderNo"].Visible = false;
            //gv.Columns["SerialNo"].Visible = false;
            //gv.Columns["Seq"].Visible = false;
            //gv.Columns["SubSeq"].Visible = false;

            //

            //컬럼 타입 설정
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);

            //SUMMARY
            grid.SetCol_Summary(gv, "Qty");
        }

        private void DisplayData()
        {
            int idx = GvPM_OrderProcess.FocusedRowHandle;
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");
            string processCode = Convert.ToString(Lup_Process.smtLookUpValue).ToUpper() == "ALL" ? "" : Convert.ToString(Lup_Process.smtLookUpValue);
            string ItemCode = Convert.ToString(Lup_Item.smtLookUpValue).ToUpper() == "ALL" ? "" : Convert.ToString(Lup_Item.smtLookUpValue);

            query = $@"SELECT  OP.ProcessCode
                                , OP.LineCode
                                , CP.ProcessName
		                        , CL.LineName
		                        , OP.OrderNo
		                        , OP.OrderSeq
		                        , WO.ItemCode ItemCode
		                        , CM2.ItemName ProductName
		                        , OP.ItemCode ItemCode
		                        , CM.ItemName ItemName
		                        , OP.OrderQty
		                        , OP.WorkQty
		                        , ORS.GoodQty
		                        , ORS.NgQty
		                        , OP.[Status]
		                        , ORS.StartDate
		                        , ORS.EndDate
                        FROM PM_OrderResult ORS
                        INNER JOIN PM_ProductPlan WO ON WO.OrderNo = ORS.OrderNo
						INNER JOIN PM_OrderProcess OP ON OP.OrderNo = ORS.OrderNo AND OP.ItemCode = ORS.ItemCode
                        INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode AND CP.WorkType = '10' -- 10:사내, 20:외주
                        LEFT JOIN CM_Line CL ON OP.LineCode = CL.LineCode
                        INNER JOIN CM_Item CM ON OP.ItemCode = cm.ItemCode 
                        INNER JOIN CM_Item CM2 ON WO.ItemCode = CM2.ItemCode
                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
                        AND WO.WorkDate BETWEEN '{startDate}' AND '{endDate}'
                        --AND CP.ProcessType = '10' -- 10:가공, 20:조립, 30:도금, 40: 포장`
                        AND OP.Status <> '대기'
                        ";

            if(ItemCode != "")
			{
                query += $"\nAND OP.ItemCode = '{ItemCode}' ";
			}

            if (processCode != "")
            {
                query += $"\nAND OP.ProcessCode = '{processCode}' ";
            }

            query += $"\nORDER BY OP.OrderNO , OP.OrderSeq";

            DataTable dt = db.GetDataTable(query);

            GcPM_OrderProcess.DataSource = dt;

            //데이터 없으면 클리어
            if(dt.Rows.Count < 1)
			{
                //GcPM_OrderResult.DataSource = null;
                GcQM_WorkDefect.DataSource = null;
			}
			else
			{
                GvPM_OrderProcess.FocusedRowHandle = idx;
                GvPM_OrderProcess.TopRowIndex = idx;

                GvPM_OrderProcess.BestFitColumns();
            }
        }

   //     private void DisplayResult()
   //     {
   //         DataRow dr = GvPM_OrderProcess.GetFocusedDataRow();

   //         if (dr == null)
   //         {
   //             //GcPM_OrderResult.DataSource = null;
   //             GcQM_WorkDefect.DataSource = null;
   //             return;
   //         }

   //         string orderNo = Convert.ToString(dr["OrderNo"]);
   //         int orderSeq = Convert.ToInt32(dr["OrderSeq"]);

   //         query = $@"SELECT odr.SubSeq
   //                             , odr.OrderNo
   //                             , odr.OrderSeq
   //                             , odr.SerialNo
   //                             , odr.WorkDate
   //                             , odr.GoodQty
   //                             , odr.NgQty
   //                             , odr.StartDate
   //                             , odr.EndDate
   //                             , odr.EndDate
   //                             , op.ItemCode
   //                             , IIF( (SELECT COUNT(OrderNo) 
   //                                    FROM WorkInspection 
   //                                    WHERE CorpId = odr.CorpId 
   //                                    AND OrderNo = odr.OrderNo 
   //                                    AND OrderSeq = odr.OrderSeq 
   //                                    AND SubSeq = odr.SubSeq
   //                                    ) > 0 , 'Y', 'N') AS ChkProc --공정검사 유무
   //                     FROM PM_OrderResult odr
   //                     LEFT JOIN PM_OrderProcess op ON odr.CorpId = op.CorpId AND odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq
   //                     WHERE odr.CorpID = '{WrGlobal.CorpID}'
   //                     AND odr.OrderNo = '{orderNo}'
   //                     AND odr.OrderSeq = {orderSeq}
   //                     ORDER BY odr.SubSeq DESC
   //                     ";

   //         DataTable dt = db.GetDataTable(query);

   //         GcPM_OrderResult.DataSource = dt;

   //         GvPM_OrderResult.BestFitColumns();

   //         if(dt.Rows.Count < 1)
			//{
   //             GcQM_WorkDefect.DataSource = null;
			//}
   //     }

        private void DisplayDefective()
        {
            if(GcPM_OrderProcess.DataSource == null)
			{
                GcQM_WorkDefect.DataSource = null;
				return;
			}

            if (GvPM_OrderProcess.RowCount < 1)
			{
                GcQM_WorkDefect.DataSource = null;
                return;
            }

            DataRow dr = GvPM_OrderProcess.GetFocusedDataRow();
            if (dr == null) return;

            string orderNo =    dr["OrderNo"].ConvertString_Null_To_Empty();//Convert.ToString(GvPM_OrderResult.GetFocusedRowCellValue("SerialNo"));
            string orderSeq = dr["OrderSeq"].ConvertString_Null_To_Empty();// Convert.ToString(GvPM_OrderResult.GetFocusedRowCellValue("ProcessCode"));

            #region
/*
            query = $@"
SELECT WD.ReworkID
                                , WD.SerialNo
		                        , CD.Title DefectName
		                        , WD.Qty
		                        , WD.Remark
                        FROM QM_WorkDefect WD
                        INNER JOIN PM_OrderResult ORL ON ORL.SerialNo = WD.SerialNo
                        INNER JOIN CM_DefectCode CD ON WD.DefectCode = CD.FaultCode
                        WHERE WD.CorpId = '{WrGlobal.CorpID}'
                        AND WD.SerialNo = '{serialNo}'
                        ORDER BY ORL.ReworkID DESC
";*/
			#endregion

			query = $@"
						SELECT
						wd.*, cd.Title AS DefectName
						FROM 
						(
							SELECT CorpId, DefectCode, SUM(Qty) AS Qty
							FROM QM_WorkDefect 
							WHERE CorpId =  '{WrGlobal.CorpID}'
							AND SerialNo IN (
							    SELECT SerialNo
							    FROM PM_OrderResult
							    WHERE CorpId = '{WrGlobal.CorpID}'
                                AND OrderNo = '{orderNo}'
                                AND OrderSeq = '{orderSeq}'
							) 
							GROUP BY CorpId, DefectCode
						) AS wd
						LEFT JOIN CM_DefectCode cd ON wd.CorpId = cd.CorpId AND wd.DefectCode = cd.FaultCode

                        ";

            DataTable dt = db.GetDataTable(query);

            GcQM_WorkDefect.DataSource = dt;
            GvQM_WorkDefect.BestFitColumns();
        }

        private void btnSearch_Click(object sender, EventArgs e)
		{
            DisplayData();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            //SaveDefect();
        }

        private void btnClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}

	}
}