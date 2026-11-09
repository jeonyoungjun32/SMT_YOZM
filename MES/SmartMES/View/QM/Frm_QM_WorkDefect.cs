using DevExpress.CodeParser;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.PM;
using SmartMES.Models.QM;
using SmartMES.Models.ST;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SmartMES.View.QM
{
	public partial class Frm_QM_WorkDefect : DevExpress.XtraEditors.XtraForm
	{
		WrComFunc func = new WrComFunc();
		Database db = new Database();
		clsCode code = new clsCode();
		string query = "";
		clsGrid grid = new clsGrid();
		cls_Lookup clsLup = new cls_Lookup();
        clsRepository rpo = new clsRepository();

        private string[] ItemDiv = { "20", "30" }; //품목구분 리스트(10 : 상품, 20 : 제품, 30 : 반제품, 40 : 구매자재, 50 : 부자재, 60 : 샘플/개발)

		public Frm_QM_WorkDefect()
		{
			InitializeComponent();
		}

		private void frmQM_WorkDefect_Load(object sender, EventArgs e)   //FrmManufacturePlan_Load
		{
			DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
			DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
			endDate = endDate.AddDays(-1);

			Dt_Date.smtStartDateTimeValue = startDate;
			Dt_Date.smtEndDateTimeValue = endDate;

			SetLup();

			Set_GvOrderProgress(GvPM_OrderProcess);
			Set_GvPM_OrderResult(GvPM_OrderResult);
			Set_GvRework(GvQM_WorkDefect);

			DisplayOrderProcess();
		}

		private void SetLup()
		{
			//공정
			//DataTable dt_process = code.GetProcess_List_ProcessType(true, new string[] { "10" }); //가공공정만
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

			//품목분류
			RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");

			RepositoryItemSearchLookUpEdit lup_Class1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", false), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Class2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", false), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Class3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", false), "코드", "명칭");


			grid.Create_GvColumn(gv, "상태", "Status");
			grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
			grid.Create_GvColumn(gv, "공정", "ProcessName");
			grid.Create_GvColumn(gv, "라인코드", "LineCode");
			grid.Create_GvColumn(gv, "라인", "LineName");
			grid.Create_GvColumn(gv, "작업지시<Br>번호", "OrderNo");
			grid.Create_GvColumn(gv, "작업지시<Br>순번", "OrderSeq");

			grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

			grid.Create_GvColumn(gv, "작업지시<Br>수량", "OrderQty");
			grid.Create_GvColumn(gv, "생산<Br>수량", "WorkQty");
			grid.Create_GvColumn(gv, "양품<Br>수량", "GoodQty");
			grid.Create_GvColumn(gv, "부적합<Br>수량", "NgQty");
			grid.Create_GvColumn(gv, "시작시간", "StartDate");
			grid.Create_GvColumn(gv, "종료시간", "EndDate");

			//RPO
			if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
			if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class1;
			if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class2;
			if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class3;

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

			//TxtOrderNo.Text = Convert.ToString(dr["OrderNo"]);
			//TxtOrderSeq.Text = Convert.ToString(dr["OrderSeq"]);

			DisplayResult();
		}

		private void Set_GvPM_OrderResult(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			gv.OptionsView.ColumnAutoWidth = false;

			// 체크박스
			clsRepository rpo = new clsRepository();
			RepositoryItemCheckEdit chk_Use = rpo.Create_rpoChk();
			chk_Use.ValueChecked = "Y";
			chk_Use.ValueUnchecked = "N";
			chk_Use.NullStyle = StyleIndeterminate.Unchecked;
			chk_Use.AllowGrayed = false;

			grid.Create_GvColumn(gv, "작업순번", "SubSeq");
			grid.Create_GvColumn(gv, "시리얼넘버", "SerialNo");
			grid.Create_GvColumn(gv, "작업시작시간", "StartDate");
			grid.Create_GvColumn(gv, "작업종료시간", "EndDate");
			grid.Create_GvColumn(gv, "양품수량", "GoodQty");
			grid.Create_GvColumn(gv, "불량수량", "NgQty");
			grid.Create_GvColumn(gv, "공정검사", "ChkProc", chk_Use);
			//grid.Create_GvColumn(gv, "검사표준서", "ChkQC", chk_Use);

			//VISIBLE
			gv.Columns["ChkProc"].Visible = false;

			// 포맷 설졍
			gv.Columns["StartDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			gv.Columns["StartDate"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

			gv.Columns["EndDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			gv.Columns["EndDate"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

			grid.SetCol_Type(gv.Columns["GoodQty"], clsGrid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TFloat);

			//SUMMARY
			grid.SetCol_Summary(gv, "GoodQty");
			grid.SetCol_Summary(gv, "NgQty");

			//EVENT
			gv.FocusedRowObjectChanged += GvPM_OrderResult_FocusedRowObjectChanged;
		}

		private void GvPM_OrderResult_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
		{
			DisplayDefective();
		}

		private void Set_GvRework(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			gv.OptionsView.ColumnAutoWidth = false;

            RepositoryItemSearchLookUpEdit lup_HouseCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("창고정보", "40", ""), "코드", "코드명");

            grid.Create_GvColumn(gv, "지시번호", "OrderNo");
			grid.Create_GvColumn(gv, "지시순번", "Seq");
			grid.Create_GvColumn(gv, "작업순번", "SubSeq");
			grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");

			grid.Create_GvColumn(gv, "부적합번호", "ReworkID");
			grid.Create_GvColumn(gv, "불량코드", "FaultCode");
			grid.Create_GvColumn(gv, "불량명", "FaultName");
			grid.Create_GvColumn(gv, "불량수량", "Qty");
			grid.Create_GvColumn(gv, "불량창고", "HouseCode", lup_HouseCode);
			grid.Create_GvColumn(gv, "비고", "Remark");

			//VISIBLE
			gv.Columns["OrderNo"].Visible = false;
			gv.Columns["SerialNo"].Visible = false;
			gv.Columns["Seq"].Visible = false;
			gv.Columns["SubSeq"].Visible = false;

			//
			grid.Column_AllowEdit(gv.Columns["Qty"], true);
			grid.Column_AllowEdit(gv.Columns["HouseCode"], true);
			grid.Column_AllowEdit(gv.Columns["Remark"], true);

			//컬럼 타입 설정
			grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TFloat);

			//SUMMARY
			grid.SetCol_Summary(gv, "Qty");
		}

		private void DisplayOrderProcess()
		{
			int idx = GvPM_OrderProcess.FocusedRowHandle;

			string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
			string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");
			string processCode = Convert.ToString(Lup_Process.smtLookUpValue).ToUpper() == "ALL" ? "" : Convert.ToString(Lup_Process.smtLookUpValue);
			string ItemCode = Convert.ToString(Lup_Item.smtLookUpValue).ToUpper() == "ALL" ? "" : Convert.ToString(Lup_Item.smtLookUpValue);

			PM_OrderProcess op = new PM_OrderProcess();
			op.ItemCode = ItemCode;
			op.ProcessCode = processCode;

			DataTable dt = op.SelectOrderProcess_QM_WorkDefect(startDate, endDate);

			GcPM_OrderProcess.DataSource = dt;

			//데이터 없으면 클리어
			if (dt.Rows.Count < 1)
			{
				GcPM_OrderResult.DataSource = null;
				GcQM_WorkDefect.DataSource = null;
			}
			else
			{
				GvPM_OrderProcess.FocusedRowHandle = idx;
				GvPM_OrderProcess.TopRowIndex = idx;

				GvPM_OrderProcess.BestFitColumns();
			}
		}

		private void DisplayResult()
		{
			DataRow dr = GvPM_OrderProcess.GetFocusedDataRow();

			if (dr == null)
			{
				GcPM_OrderResult.DataSource = null;
				GcQM_WorkDefect.DataSource = null;
				return;
			}

			string orderNo = Convert.ToString(dr["OrderNo"]);
			int orderSeq = Convert.ToInt32(dr["OrderSeq"]);
			PM_OrderResult odr = new PM_OrderResult();
			odr.OrderNo = orderNo;
			odr.OrderSeq = orderSeq;

			DataTable dt = odr.Select_PM_OrderResult_Indiv();

			GcPM_OrderResult.DataSource = dt;
			GvPM_OrderResult.BestFitColumns();

			if (dt.Rows.Count < 1)
			{
				GcQM_WorkDefect.DataSource = null;
			}
        }

		private void DisplayDefective()
		{
			if (GvPM_OrderResult == null)
			{
				GcQM_WorkDefect.DataSource = null;
				return;
			}

			if (GvPM_OrderResult.RowCount < 1)
			{
				GcQM_WorkDefect.DataSource = null;
				return;
			}


			string serialNo = Convert.ToString(GvPM_OrderResult.GetFocusedRowCellValue("SerialNo"));
			//string processCode = Convert.ToString(GvPM_OrderProcess.GetFocusedRowCellValue("ProcessCode"));
			string processCode = "";
            int seq = GvPM_OrderResult.GetFocusedRowCellValue("OrderSeq").ConvertInt_Null_To_Zero();
            int subSeq = GvPM_OrderResult.GetFocusedRowCellValue("SubSeq").ConvertInt_Null_To_Zero();

			QM_WorkDefect wd = new QM_WorkDefect();
			wd.Seq = seq;
			wd.SubSeq = subSeq;
			wd.SerialNo = serialNo;
		
            DataTable dt = wd.SelectQM_WorkDefect(processCode);

			GcQM_WorkDefect.DataSource = dt;
			GvQM_WorkDefect.BestFitColumns();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			DisplayOrderProcess();
		}


		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveDefect();
		}

		private void SaveDefect()
		{
			this.Validate();

			GridView gv = GvPM_OrderResult;

			DataRow drOrder = gv.GetFocusedDataRow();
			if (drOrder == null) return;

			DataTable dt = GcQM_WorkDefect.DataSource as DataTable;
			if (dt == null) return;

			string serialNo = drOrder["SerialNo"].ConvertString_Null_To_Empty();
			int seq = drOrder["OrderSeq"].ConvertInt_Null_To_Zero();
			int subSeq = drOrder["SubSeq"].ConvertInt_Null_To_Zero();
			string orderNo = drOrder["OrderNo"].ConvertString_Null_To_Empty();
			string ItemCode = drOrder["ItemCode"].ConvertString_Null_To_Empty();
			string workDate = drOrder["WorkDate"].ConvertDate_To_Char10();

			DateTime DtworkDate = Convert.ToDateTime(workDate);

			QM_WorkDefect wd = new QM_WorkDefect();

			foreach (DataRow dr in dt.Rows)
			{
				string houseCode = dr["HouseCode"].ConvertString_Null_To_Empty();	//불량창고

				wd.SerialNo = serialNo;
				wd.Seq = seq;
				wd.SubSeq = subSeq;
				wd.ReworkID = dr["ReworkID"].ConvertString_Null_To_Empty();

				wd.ExamType = "10"; //10:생산, 20:검사
				wd.OrderNo = orderNo;
				wd.WorkDate = workDate;
				wd.ItemCode = ItemCode;
				wd.DefectCode = dr["FaultCode"].ConvertString_Null_To_Empty();

				wd.Qty = dr["Qty"].ConvertFloat_Null_To_Zero();
				wd.Remark = dr["Remark"].ConvertString_Null_To_Empty();

				//신규등록
				if (wd.ReworkID == "" && wd.Qty != 0)
				{
					//1. 공정불량 저장
					//리워크아이디 채번
					wd.ReworkID = wd.GetNextReworkID();

					wd.Ins_QM_WorkDefect(); //INSERT

					//2. 재고 - 부적합 처리
					string inOutNo = "ST" + code.GetAutoNumber("재고", DtworkDate);

					ST_StockM sm = new ST_StockM();
					sm.InOutNo = inOutNo;
					sm.OutDate = workDate;
					sm.ToHouseCode = houseCode;
					sm.WriteID = WrGlobal.LoginID;
					sm.Ins_ST_StockM();

					ST_StockD sd = new ST_StockD();
					sd.InOutNo = inOutNo;
					sd.Seq = 1;
					sd.InOutDate = workDate;
					sd.Type = "부적합";
					sd.ItemCode = ItemCode;
					sd.ToHouseCode = houseCode;
					sd.JobNo = wd.ReworkID;
					sd.Qty = wd.Qty.ConvertDecimal_Null_To_Zero();
					sd.CloseYn = "Y";
					sd.Ins_ST_StockD();
				}
				else
				{
					//공정불량 업데이트 
					wd.Upd_QM_WorkDefect();

					//재고 업데이트
					ST_StockM sm = new ST_StockM();
					sm.ToHouseCode = houseCode;
					sm.Upd_StockM_Defect(wd.ReworkID);

					ST_StockD sd = new ST_StockD();
					sd.Qty = wd.Qty.ConvertDecimal_Null_To_Zero();
					sd.ToHouseCode = houseCode;
					sd.JobNo = wd.ReworkID;
					sd.Upd_Qty_ByDefect_JobNo();
				}
				// 지시, 실적 불량수량 업데이트
				query = $@" EXEC Upd_NgQty_WorkDefect '{WrGlobal.CorpID}', '{serialNo}';";
				db.ExecuteQuery(query);
			}
            MessageBox.Show("저장이 완료되었습니다.");
			DisplayOrderProcess();
			DisplayResult();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}