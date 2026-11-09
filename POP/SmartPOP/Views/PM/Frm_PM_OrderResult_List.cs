using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Models.PM;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.BM;
using SmartPOP.Models.ST;
using SMT_MES.Controls;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartPOP.Views.PM
{
	public partial class Frm_PM_OrderResult_List : DevExpress.XtraEditors.XtraForm
	{
		public delegate void FormSendDataHandler(DataRow dr);
		public event FormSendDataHandler FormSendEvent;

		cls_Grid grid = new cls_Grid();
		cls_Repository rpo = new cls_Repository();

		clsCode code = new clsCode();

		public Frm_PM_OrderResult_List()
		{
			InitializeComponent();
		}

		public Frm_PM_OrderResult_List(string orderNo, int orderSeq)
		{
			InitializeComponent();

			TxtOrderNo.EditValue = orderNo;
			TxtOrderSeq.EditValue = orderSeq;
		}

		private void Frm_POP_Product_Load(object sender, EventArgs e)
		{
			//Txt_FromDate.EditValue = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
			//Txt_ToDate.EditValue = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

			rpo.Set_POP_Dtp(Txt_FromDate);
			rpo.Set_POP_Dtp(Txt_ToDate);

			Txt_FromDate.EditValue = DateTime.Now;
			Txt_ToDate.EditValue = DateTime.Now;

			InitGrid(gvProcessResult);
			InitLupSetting();

			Search();
		}
		private void InitGrid(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = false;

			//Row MultiLine
			gv.OptionsView.RowAutoHeight = true;

			//헤더 멀티라인 설정
			gv.OptionsView.AllowHtmlDrawHeaders = true;
			gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

			//단위
			RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
			//품목분류1,2,3
			RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

			grid.Create_GvColumn(gv, "지시상태", "OPStatus");

			grid.Create_GvColumn(gv, "지시번호", "OrderNo");
			grid.Create_GvColumn(gv, "지시순번", "OrderSeq");
			grid.Create_GvColumn(gv, "작업순번", "SubSeq");
			grid.Create_GvColumn(gv, "실적일자", "WorkDate");
			grid.Create_GvColumn(gv, "라인명", "LineName");
			grid.Create_GvColumn(gv, "공정명", "ProcessName");
			grid.Create_GvColumn(gv, "설비명", "MachineName");

			grid.Create_GvColumn(gv, "작업자", "WorkerID");

			grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

			grid.Create_GvColumn(gv, "생산량", "WorkQty");
			grid.Create_GvColumn(gv, "양품량", "GoodQty");
			grid.Create_GvColumn(gv, "불량량", "NgQty");
			grid.Create_GvColumn(gv, "시작일시", "StartDate");
			grid.Create_GvColumn(gv, "종료일시", "EndDate");

			//Repository
			if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
			if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
			if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
			if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;

			//
			gv.Columns["OPStatus"].Visible = false;
			gv.Columns["LineName"].Visible = false;
			gv.Columns["OrderNo"].Visible = false;
			gv.Columns["OrderSeq"].Visible = false;
			gv.Columns["OrderSeq"].Visible = false;
			gv.Columns["ItemClass1"].Visible = false;
			gv.Columns["ItemClass2"].Visible = false;

			//format
			grid.SetCol_Type(gv.Columns["WorkQty"], cls_Grid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["GoodQty"], cls_Grid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["NgQty"], cls_Grid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["StartDate"], cls_Grid.TypeList.TDateTime);
			grid.SetCol_Type(gv.Columns["EndDate"], cls_Grid.TypeList.TDateTime);

			//summary
			grid.SetCol_Summary(gv, "WorkQty");
			grid.SetCol_Summary(gv, "GoodQty");
			grid.SetCol_Summary(gv, "NgQty");

			grid.SetPOPGrid(gv);
		}

		private void InitLupSetting()
		{
			//품목
			//BS_Item Item = new BS_Item();
			//DataTable dt_item = Item.Select_Item_List(new string[] { "20", "30" }, false);
			//cls.Create_RepositoryItemSearchLookUpDefault(LupItem, dt_item, "Item_Cd", "Item_Nm");

			////공정
			//BS_Process Base = new BS_Process();
			//DataTable dt = Base.Select_Process_ResultYN("All");

			//LupProcess.Properties.DataSource = dt;
			//LupProcess.Properties.ValueMember = "코드";
			//LupProcess.Properties.DisplayMember = "명칭";

			//LupProcess.EditValue = dt.Rows[0][0].ConvertString_Null_To_Empty();
			//LupProcess.Properties.ShowClearButton = false;
		}


		public void SearchLookUpMachine(SearchLookUpEdit lookUpEdit, string Corp_Cd)
		{
			string process = Convert.ToString(LupProcess.EditValue);

			//BS_Machine Base = new BS_Machine();
			//DataTable dt = Base.Select_Machine(process);

			//lookUpEdit.Properties.DataSource = dt;
			//lookUpEdit.Properties.ValueMember = "코드";
			//lookUpEdit.Properties.DisplayMember = "명칭";

			//lookUpEdit.Properties.ShowClearButton = false;
		}

		public void SearchLookUpEmp(SearchLookUpEdit lookUpEdit, string Corp_Cd, bool WorkerYN)
		{
			string process = LupProcess.EditValue.ConvertString_Null_To_Empty();

			//SC_Emp Base = new SC_Emp();
			//DataTable dt = Base.Select_Emp_ByProcess(WorkerYN, process);

			//lookUpEdit.Properties.DataSource = dt;
			//lookUpEdit.Properties.ValueMember = "코드";
			//lookUpEdit.Properties.DisplayMember = "명칭";

			//lookUpEdit.EditValue = dt.Rows[0][0].ConvertString_Null_To_Empty();
			//lookUpEdit.Properties.ShowClearButton = false;
		}

		public void SearchLookUpItem(SearchLookUpEdit lookUpEdit, string Corp_Cd)
		{
			string process = Convert.ToString(LupProcess.EditValue);

			//BS_Item Base = new BS_Item();
			////DataTable dt = Base.Select_Item();
			//DataTable dt = Base.Select_Item_ByProcess_Use_POP(process); // 작업자는 품번으로 구분하기 때문에 코드, 품명, 품목 세가지 다 보여줌

			//lookUpEdit.Properties.DataSource = dt;
			//lookUpEdit.Properties.ValueMember = "Item_Cd";
			//lookUpEdit.Properties.DisplayMember = "Item_Nm";

		}

		//private void Lup_EditValueChanged(object sender, EventArgs e)
		//{
		//	DevExpress.XtraEditors.SearchLookUpEdit Btn = (DevExpress.XtraEditors.SearchLookUpEdit)sender;

		//	string btnname = Btn.Name.ToString();

		//	switch (btnname)
		//	{
		//		case "LupProcess":
		//			SearchLookUpMachine(LupMachine, WrGlobal.CorpID);
		//			break;

		//		case "LupMachine":
		//			SearchLookUpEmp(LupEmp, WrGlobal.CorpID, true);
		//			break;

		//		case "LupEmp":
		//			SearchLookUpItem(LupItem, WrGlobal.CorpID);
		//			break;
		//	}
		//}


		private void Btn_Click(object sender, EventArgs e)
		{
			SimpleButton Btn = (SimpleButton)sender;

			string btnname = Btn.Name.ToString();

			switch (btnname)
			{
				case "Btn_Search":
					Search();
					break;

				case "Btn_Send":
					Send_RstNo();
					break;

				case "Btn_Delete":
					Delete();
					Search();
					break;

				case "Btn_Back":
					End();
					break;
			}
		}

		
		private void Search()
		{
			gcProcessResult.DataSource = null;

			string process = Convert.ToString(LupProcess.EditValue);
			string machine = Convert.ToString(LupMachine.EditValue);
			string woker = Convert.ToString(LupEmp.EditValue);
			string item = Convert.ToString(LupItem.EditValue);

			string fromDate = Txt_FromDate.DateTime.ToString("d");
			string toDate = Txt_ToDate.DateTime.ToString("d");

			PM_OrderResult odr = new PM_OrderResult();

			if(TxtOrderNo.Text.Trim() == "")
			{
				//기간으로 조회
				odr.StartDate = fromDate;
				odr.EndDate = toDate;
			}
			else
			{
				odr.OrderNo = TxtOrderNo.Text;
				odr.OrderSeq = TxtOrderSeq.EditValue.ConvertInt_Null_To_Zero();
			}

			DataTable dt = odr.Select_PM_OrderResult();

			gcProcessResult.DataSource = dt;

			gvProcessResult.BestFitColumns();
		}

		private void Delete()
		{
			DataRow dr = gvProcessResult.GetFocusedDataRow();

			if (dr == null) return;
			if (dr["OrderNo"].ConvertString_Null_To_Empty() == "") return;

			DialogResult result = cls_MessageBox.Show("<size=40>작업실적을 삭제하시겠습니까?</size>", "<size=40><color=red>주의!</color></size>", MessageBoxButtons.OKCancel);

			if (result != DialogResult.OK)
			{
				return;
			}

			PM_OrderResult rst = new PM_OrderResult();
			rst.CorpId = WrGlobal.CorpID;
			rst.OrderNo = Convert.ToString(dr["OrderNo"]);
			rst.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
			rst.SubSeq = dr["SubSeq"].ConvertInt_Null_To_Zero(); // 작업 순번
			rst.SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty(); // 

			/// 생산실적 삭제
			rst.Del_PM_OrderResult();

			//작업자 삭제
			PM_OrderResultWorker ow = new PM_OrderResultWorker();
			ow.CorpId = WrGlobal.CorpID;
			ow.OrderNo = Convert.ToString(dr["OrderNo"]);
			ow.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
			ow.SubSeq = dr["SubSeq"].ConvertInt_Null_To_Zero(); // 작업 순번
			ow.DelWorker();

			//재고 투입, 입고내역 삭제
			ST_StockD std = new ST_StockD();
			std.CorpId = WrGlobal.CorpID;
			std.JobNo = Convert.ToString(dr["OrderNo"]);
			std.JobSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
			std.JobSeq2 = dr["SubSeq"].ConvertInt_Null_To_Zero(); // 작업 순번
			std.DeleteStockMD_Work();

			//자재출고내역 삭제
			BM_SalesD sd = new BM_SalesD();
			sd.DeleteSalesMD_Work(rst.SerialNo);

			//PM_OrderProcess 업데이트
			PM_OrderProcess op = new PM_OrderProcess();
			op.CorpId = WrGlobal.CorpID;
			op.OrderNo = rst.OrderNo;
			op.OrderSeq = rst.OrderSeq;

			op.Upd_OrderProcess_Result();

			//PM_ProductPlan 업데이트
			PM_ProductPlan wo = new PM_ProductPlan();
			wo.CorpId = WrGlobal.CorpID;
			wo.OrderNo = wo.OrderNo;
			wo.Upd_PM_ProductPlanQty_POP();

			cls_MessageBox.Show("삭제 완료");
		}

		private void gvProcessResult_DoubleClick(object sender, EventArgs e)
		{
			Send_RstNo();
		}

		private void Send_RstNo()
		{
			DataRow dr = gvProcessResult.GetFocusedDataRow();
			if (dr == null) return;

			FormSendEvent(dr);
			this.Close();
		}

		private void End()
		{
			this.Close();
		}
	}
}