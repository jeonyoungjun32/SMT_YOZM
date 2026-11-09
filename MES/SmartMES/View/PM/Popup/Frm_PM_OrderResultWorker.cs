using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.PM;
using System;
using System.Collections.Generic;
using System.Data;

namespace SmartMES.View.PM
{
    public partial class Frm_PM_OrderResultWorker : DevExpress.XtraEditors.XtraForm
	{
		/*
		 * 라인 및 공정별 옵션 미적용 상태로, TPG 기준 공정별 작업자 구조로 개발되어 있음. 추후 라인/공정 단위 옵션 처리를 위한 구조 개선 필요
		 */

		clsGrid grid = new clsGrid();
		clsRepository rpo = new clsRepository();
		clsCode code = new clsCode();

		DataRow curRow;
		string OrderNo = "";
		int OrderSeq = 0;
		int SubSeq = 0;

		Database db = new Database();
		string query = "";

		public Dictionary<string, PM_OrderResultWorker> DicWorkers { get; set; } = new Dictionary<string, PM_OrderResultWorker>();

		public delegate void FormSendDataHandler(Dictionary<string, PM_OrderResultWorker> dicWorkder);
		public event FormSendDataHandler FormSendEvent;


		string LineWorker_YN = WrGlobal.ProcOption["BS"]["LineWorker_YN"];

		public Frm_PM_OrderResultWorker()
		{
			InitializeComponent();
		}

		public Frm_PM_OrderResultWorker(DataRow dr)
		{
			InitializeComponent();
			curRow = dr;

			this.OrderNo = curRow["OrderNo"].ConvertString_Null_To_Empty();
			this.OrderSeq = curRow["OrderSeq"].ConvertInt_Null_To_Zero();
			this.SubSeq = curRow["SubSeq"].ConvertInt_Null_To_Zero();
		}

		private void Frm_PM_OrderResultWorker_Load(object sender, EventArgs e)
		{
			SetGridList();
			SetGridWorker();

			//RadioGroup 설정
			Set_Rdo();

			SearchList();
			SearchWorker();
		}

		private void Set_Rdo()
		{
			//RadioGroup 셋팅
			clsRadioGroup clsRadio = new clsRadioGroup();
			Rdo.Properties.Items.Clear();

			DataTable dt = null;

			if (LineWorker_YN == "Y")
			{
				//라인
				query = $@"Select LineCode 코드, LineName 명칭 From CM_Line Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";

			}
			else
			{
				//공정
				query = $@"Select ProcessCode 코드, ProcessName 명칭 From CM_Process Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";
			}

			dt = db.GetDataTable(query);

			foreach (DataRow dr in dt.Rows)
			{
				clsRadio.Create_RadioGroupItem(Rdo, dr["명칭"].ConvertString_Null_To_Empty(), dr["코드"].ConvertString_Null_To_Empty());
			}

			if (curRow["ProcessCode"].ConvertString_Null_To_Empty() == "")
				Rdo.EditValue = "ALL";
			else
				Rdo.EditValue = curRow["ProcessCode"];
		}

		#region Grid 디자인 설정
		private void SetGridList()
		{
			GridView gv = GvList;

			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

			gv.OptionsFind.ShowClearButton = true;
			gv.OptionsFind.FindNullPrompt = "검색어를 입력해 주세요.";

			//Repository

			RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false, null, "Y"), "코드", "명칭");

			RepositoryItemCheckEdit checkEdit = rpo.Create_rpoChk();

			grid.Create_GvColumn(gv, "지시번호", "OrderNo");
			grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
			grid.Create_GvColumn(gv, "작업순번", "SubSeq");

			grid.Create_GvColumn(gv, "작업자명", "WorkerID", lup_Emp_No);


			//기타
			gv.Columns["OrderNo"].Visible = false;
			gv.Columns["OrderSeq"].Visible = false;
			gv.Columns["SubSeq"].Visible = false;

			grid.RowNumber_Indicator(gv);
			gv.RowClick += Gv_RowClick;
		}

		private void SetGridWorker()
		{
			GridView gv = GvWorker;

			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

			//Repository
			RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false, null, "Y"), "코드", "명칭");
			RepositoryItemCheckEdit checkEdit = rpo.Create_rpoChk();

			grid.Create_GvColumn(gv, "지시번호", "OrderNo");
			grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
			grid.Create_GvColumn(gv, "실적순번", "SubSeq");

			//grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
			//grid.Create_GvColumn(gv, "공정명", "ProcessName");
			grid.Create_GvColumn(gv, "작업자명", "WorkerID", lup_Emp_No);


			//기타
			gv.Columns["OrderNo"].Visible = false;
			gv.Columns["OrderSeq"].Visible = false;
			gv.Columns["SubSeq"].Visible = false;


			gv.RowClick += Gv_RowClick;

			grid.RowNumber_Indicator(gv);
		}

		#endregion

		private void Rdo_SelectedIndexChanged(object sender, EventArgs e)
		{
			SearchList();
		}

		private void Btn_Click(object sender, EventArgs e)
		{
			smtSearchButton btn = sender as smtSearchButton;
			if (btn == null) return;

			switch (btn.Name)
			{
				case "BtnSearch":
					SearchList();
					break;

				case "cmdSave":
					Save();
					break;

				case "cmdClose":
					this.Close();
					break;
			}
		}

		private void SearchList()
		{
			//왼쪽 Grid
			CM_LineProcessWorker ca = new CM_LineProcessWorker();
			//ca.LineCode = Rdo.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Rdo.EditValue.ConvertString_Null_To_Empty();
			ca.ProcessCode = Rdo.EditValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Rdo.EditValue.ConvertString_Null_To_Empty();

			GcList.DataSource = ca.Init(OrderNo, OrderSeq, SubSeq);

			GvList.BestFitColumns();
		}

		private void SearchWorker()
		{
			PM_OrderResultWorker wm = new PM_OrderResultWorker();
			wm.OrderNo = this.OrderNo;
			wm.OrderSeq = OrderSeq;
			wm.SubSeq = SubSeq;
			DataTable dt = wm.Select_Worker();

			GcWorker.DataSource = dt;

			//실적 내역 없이 추가 했을경우 Dictionary에 담음
			if (OrderNo == "" && dt.Rows.Count > 0)
			{
				foreach (KeyValuePair<string, PM_OrderResultWorker> item in DicWorkers)
				{
					PM_OrderResultWorker pd = item.Value;

					DataRow newRow = dt.NewRow();

					newRow["WorkerID"] = pd.WorkerID;
					newRow["WorkerName"] = pd.WorkerName;
					//newRow["Line_Cd"] = pd.Line_Cd;

					dt.Rows.Add(newRow);
				}
			}

			GvWorker.BestFitColumns();
		}

		private void Gv_RowClick(object sender, RowClickEventArgs e)
		{
			GridView gv = sender as GridView;
			if (gv == null) return;

			DataRow dr = gv.GetDataRow(e.RowHandle);
			if (dr == null) return;

			if (gv.Name == "GvList")
			{
				//ROW ADD
				DataTable dt = GcWorker.DataSource as DataTable;
				if (dt == null) return;

				//중복 체크
				string workEmpNo = dr["WorkerID"].ConvertString_Null_To_Empty();

				DataRow[] drs = dt.Select($" WorkerID = '{workEmpNo}'");

				if (drs.Length > 0)
				{

					cls_MessageBox.Show("이미 추가된 작업자입니다.");
					return;
				}

				DataRow newRow = dt.NewRow();

				newRow["WorkerID"] = workEmpNo;
				newRow["WorkerName"] = dr["WorkerName"];

				dt.Rows.Add(newRow);
			}
			else if (gv.Name == "GvWorker")
			{
				GvWorker.GetFocusedDataRow().Delete();
			}

			GvWorker.RefreshData();
			GvWorker.BestFitColumns();
		}

		private void Save()
		{
			DataTable dt = GcWorker.DataSource as DataTable;
			if (dt == null) return;

			//datatable 변경 내용 적용
			dt.AcceptChanges();

			//POP에서 사용 (실적건 없이 작업자 전달)
			DicWorkers.Clear();

			foreach (DataRow row in dt.Rows)
			{
				string workEmpNo = Convert.ToString(row["WorkerID"]);

				PM_OrderResultWorker pD_Worker = new PM_OrderResultWorker();
				pD_Worker.WorkerID = workEmpNo;
				pD_Worker.WorkerName = Convert.ToString(row["WorkerName"]);
				//pD_Worker.Line_Cd = Convert.ToString(row["Line_Cd"]);
				//pD_Worker.Fix_Yn = "Y";

				DicWorkers.Add(workEmpNo, pD_Worker);
			}

			if (SubSeq != 0)
			{
				//DB에 저장
				Save_Worker(dt);
			}

			if (FormSendEvent != null)
			{
				FormSendEvent(DicWorkers);
			}

			this.Close();
		}

		private void Save_Worker(DataTable dt)
		{
			//기존 데이터 삭제
			PM_OrderResultWorker worker = new PM_OrderResultWorker();
			worker.OrderNo = OrderNo;
			worker.OrderSeq = OrderSeq;
			worker.SubSeq = SubSeq;

			worker.DelWorker();

			string workerName = "";

			foreach (DataRow dr in dt.Rows)
			{
				worker.OrderNo = OrderNo;
				worker.OrderSeq = OrderSeq;
				worker.WorkerID = dr["WorkerID"].ConvertString_Null_To_Empty();

				worker.InsWorker();

				if (workerName.Length > 0) workerName += ", ";
				workerName += dr["WorkerName"].ConvertString_Null_To_Empty();
			}

			PM_OrderResult or = new PM_OrderResult();
			or.OrderNo = OrderNo;
			or.OrderSeq = OrderSeq;
			or.SubSeq = SubSeq;
			or.WorkerID = workerName;

			or.Upd_PM_OrderResult_WorkerID();
		}
	}
}