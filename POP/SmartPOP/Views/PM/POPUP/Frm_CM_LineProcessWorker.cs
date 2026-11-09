using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Native.Lines;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.CM;
using SmartPOP.Models.PM;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.PM.POPUP
{
	public partial class Frm_CM_LineProcessWorker : DevExpress.XtraEditors.XtraForm
	{
		cls_Grid grid = new cls_Grid();
		cls_Repository rpo = new cls_Repository();
		clsCode code = new clsCode();

		DataRow curRow;
		string OrderNo = "";
		int OrderSeq = 0;
		int SubSeq = 0;

		public Dictionary<string, PM_OrderResultWorker> DicWorkers { get; set; } = new Dictionary<string, PM_OrderResultWorker>();

		public delegate void FormSendDataHandler(Dictionary<string, PM_OrderResultWorker> dicWorkder);
		public event FormSendDataHandler FormSendEvent;


		string LineWorker_YN = WrGlobal.ProcOption["BS"]["LineWorker_YN"];

		public Frm_CM_LineProcessWorker()
		{
			InitializeComponent();
		}

		public Frm_CM_LineProcessWorker(DataRow dr)
		{
			InitializeComponent();
			curRow = dr;

			this.OrderNo = curRow["OrderNo"].ConvertString_Null_To_Empty();
			this.OrderSeq = curRow["OrderSeq"].ConvertInt_Null_To_Zero();
			this.SubSeq = curRow["SubSeq"].ConvertInt_Null_To_Zero();
		}

		private void FrmPM_OrderResultWorker_Load(object sender, EventArgs e)
		{
			SetGridList();
			SetGridWorker();

			//GridView FindPanel Font 설정
			SetFindPanel(GcList);

			//RadioGroup 설정
			Set_Rdo();

			SearchList();
			SearchWorker();
		}

		private void Set_Rdo()
		{
			//RadioGroup 셋팅
			cls_RadioGroup clsRadio = new cls_RadioGroup();
			Rdo.Properties.Items.Clear();

			DataTable dt = null;
			//라인
			if (LineWorker_YN == "Y")
			{

				dt = code.GetLineLookUpList();
			}
			else
			{
				//공정
				dt = code.GetProcessListDt();
			}

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
			grid.SetPOPGrid(gv);

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
			gv.IndicatorWidth = 70;

			gv.RowClick += Gv_RowClick;
		}

		private void SetGridWorker()
		{
			GridView gv = GvWorker;
			grid.SetPOPGrid(gv);

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
			gv.IndicatorWidth = 70;
		}

		private void SetFindPanel(GridControl gc)
		{
			//FindPanel Font 
			FindControl findPanel = gc.Controls.Find("FindControlCore", false).FirstOrDefault() as FindControl;
			if (findPanel == null) return;

			findPanel.FindEdit.Font = new Font("맑은 고딕", 22F);
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

				case "BtnSave":
					Save();
					break;

				case "BtnClose":
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

			foreach (DataRow dr in dt.Rows)
			{
				worker.OrderNo = OrderNo;
				worker.OrderSeq = OrderSeq;
				worker.WorkerID = dr["WorkerID"].ConvertString_Null_To_Empty();

				worker.InsWorker();
			}
		}

		private void BtnUpDown_Click(object sender, EventArgs e)
		{
			GridView gv = GvList;
			SimpleButton btn = sender as SimpleButton;

			if(btn.Name == "BtnUppage")
			{
				gv.MovePrevPage();
			}
			else if (btn.Name == "BtnUp")
			{
				gv.MovePrev();
			}
			else if (btn.Name == "BtnDown")
			{
				gv.MoveNext();
			}
			else if (btn.Name == "BtnDownPage")
			{
				gv.MoveNextPage();
			}
		}
	}
}