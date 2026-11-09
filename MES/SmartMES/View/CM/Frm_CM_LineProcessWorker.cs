using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
	public partial class Frm_CM_LineProcessWorker : DevExpress.XtraEditors.XtraForm
	{
		clsGrid grid = new clsGrid();
		private string[] ProcDiv = { "10" }; //공정구분 리스트(10 : 자사공정, 20 : 외주공정)
		private string Selected_Process = "";
		private string Selected_Line = "";

		string LineWorker_YN = WrGlobal.ProcOption["BS"]["LineWorker_YN"];

		clsCode code = new clsCode();
		Database db = new Database();
		string query = "";


		public Frm_CM_LineProcessWorker()
		{
			InitializeComponent();
		}

		private void Frm_CM_LineProcessWorker_Load(object sender, EventArgs e)
		{
			SetDesign();

			//작업자 리스트


			Search_ProcLine(); //초기 공정 조회
			Search_Worker();  //초기 작업자 조회
		}


		private void SetDesign()
		{
			//GridView 데이터 입력
			Set_GridView();

			if (LineWorker_YN == "Y")
			{
				Txt_Process.Visible = false;
				labelControl1.Visible = false;

				Txt_Line.Visible = true;
				labelControl2.Visible = true;
			}
			else
			{
				Txt_Process.Visible = true;
				labelControl1.Visible = true;

				Txt_Line.Visible = false;
				labelControl2.Visible = false;
			}

			//Set_Process_RadioGroup(Rdo_Process_Div);

			//폼 닫을때 검사 작성된 내역 있는지 검사
			this.FormClosing += new FormClosingEventHandler((sender, e) =>
			{
				e.Cancel = false;
				if (grid.Check_Gv_FormClosing(gvEmp, "Status"))
				{
					e.Cancel = true;
				}
			});
		}

		#region GridView Setting

		private void Set_GridView()
		{
			//GridView 셋팅
			Set_GvWorker(Gv_Worker);      //작업자
			Set_GvProcLine(Gv_Process);
			Set_GvProdtWorker(gvEmp);      //작업 Route
		}

		private void Set_GvWorker(GridView gv)
		{
			//품목 Column 생성
			//Grid 공통 설정
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = true;     //Column 많으면 스크롤 생기도록 바꾸기
													   //검색창
			gv.OptionsFind.AlwaysVisible = false;
			gv.OptionsView.ShowGroupPanel = false;

			//Column 생성
			grid.Create_GvColumn(gv, "사용자코드", "ID");
			grid.Create_GvColumn(gv, "사용자명", "사용자명");

			grid.RowNumber_Indicator(gv);       //행순번 표기
			gv.DoubleClick += Event_Worker_DoubleClick;

			////코드숨김
			//if (WrGlobal.LoginID != "Dev")
			//{
			//    gv.Columns["EmployeeId"].Visible = false;
			//}
		}

		private void Event_Worker_DoubleClick(object sender, EventArgs e)
		{
			//1. 작업자 더블클릭 시 공정별 작업자로 이동
			if (LineWorker_YN == "Y" && Selected_Line == "")
			{
				MessageBox.Show("라인 미선택.");
				return;
			}
			if (LineWorker_YN == "N" && Selected_Process == "")
			{
				MessageBox.Show("공정 미선택.");
				return;
			}

			GridView gv = sender as GridView;
			DataTable dtProdtWorker = gcEmp.DataSource as DataTable;
			DataRow drProdtWorker = dtProdtWorker.NewRow();

			drProdtWorker["Status"] = "신규";
			drProdtWorker["EmpNo"] = gv.GetFocusedDataRow()["ID"];
			drProdtWorker["ProcessCode"] = Selected_Process;
			drProdtWorker["LineCode"] = Selected_Line;


			//2. 기존 라인/공정별 작업자와 중복 확인
			DataTable dtItem = gcEmp.DataSource as DataTable;

			//새로 입력된 정보
			string newEmp = Convert.ToString(drProdtWorker["EmpNo"]);
			string newProcessCode = Convert.ToString(drProdtWorker["ProcessCode"]);
			string newLineCode = Convert.ToString(drProdtWorker["LineCode"]);

			for (int curHandle = 0; curHandle < dtItem.Rows.Count; curHandle++)
			{
				//기존 정보
				string existingEmp = Convert.ToString(dtItem.Rows[curHandle]["EmpNo"]);
				string existingProcessCode = Convert.ToString(dtItem.Rows[curHandle]["ProcessCode"]);
				string existingLineCode = Convert.ToString(dtItem.Rows[curHandle]["LineCode"]);

				if (newEmp == existingEmp && newProcessCode == existingProcessCode && newLineCode == existingLineCode)
				{
					MessageBox.Show("이미 등록된 내역입니다.");
					return;
				}
			}

			dtProdtWorker.Rows.Add(drProdtWorker);

			gvEmp.BestFitColumns();
		}

		private void Set_GvProcLine(GridView gv)
		{
			//공정 Column 생성
			//Grid 공통 설정
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = true;     //Column 많으면 스크롤 생기도록 바꾸기

			//Column 생성
			grid.Create_GvColumn(gv, "공정코드", "ProcessCode");
			grid.Create_GvColumn(gv, "공정명", "ProcessName");
			grid.Create_GvColumn(gv, "라인코드", "LineCode");
			grid.Create_GvColumn(gv, "라인명", "LineName");

			grid.Column_AllowEdit(gv.Columns["ProcessCode"], false);
			grid.Column_AllowEdit(gv.Columns["ProcessName"], false);
			grid.Column_AllowEdit(gv.Columns["LineCode"], false);
			grid.Column_AllowEdit(gv.Columns["LineName"], false);

			grid.RowNumber_Indicator(gv);       //행순번 표기
			gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;

			if (WrGlobal.ProcOption["BS"]["LineWorker_YN"] == "Y")
			{
				gv.Columns["ProcessCode"].Visible = false;
				gv.Columns["ProcessName"].Visible = false;
			}
			else
			{
				gv.Columns["LineCode"].Visible = false;
				gv.Columns["LineName"].Visible = false;
			}

			//코드숨김
			if (WrGlobal.LoginID != "Dev")
			{
				gv.Columns["ProcessCode"].Visible = false;
				gv.Columns["LineCode"].Visible = false;
			}
		}

		private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
		{
			if (!Check_Search()) return;

			GridView gv = sender as GridView;
			DataRow dr = gv.GetDataRow(e.RowHandle);

			if (dr == null) return;

			Selected_Process = Convert.ToString(dr["ProcessCode"]);
			Txt_Process.Text = Convert.ToString(dr["ProcessName"]);
			Selected_Line = Convert.ToString(dr["LineCode"]);
			Txt_Line.Text = Convert.ToString(dr["LineName"]);

			DisplayData();
		}

		private void Set_GvProdtWorker(GridView gv)
		{
			//GvRoute Column 생성 및 셋팅
			//Grid 공통 설정
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = true;     //Column 많으면 스크롤 생기도록 바꾸기
			gv.IndicatorWidth = 50;                     //순번 너비

			//Repository 생성 or 이벤트 입력
			//공정
			clsRepository rpo = new clsRepository();
			RepositoryItemSearchLookUpEdit lup_Line = rpo.Create_RepositoryItemSearchLookUp(code.GetLine_List("", true), "라인코드", "라인명");
			RepositoryItemSearchLookUpEdit lup_Process = rpo.Create_RepositoryItemSearchLookUp(code.GetProcess_List(true), "공정코드", "공정명");

			//작업자
			RepositoryItemLookUpEdit lup_Worker = rpo.Create_RepositoryItem(code.GetWorker(), "ID", "사용자명");

			//주야구분
			RepositoryItemLookUpEdit lup_Work_Band = rpo.Create_RepositoryItem(code.GetCategory("401060", false).ToTable(), "분류코드", "분류명");

			RepositoryItemSearchLookUpEdit lup_EmployeeId = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(true), "ID", "사용자명");

			//Column 생성
			grid.Create_GvColumn(gv, "상태", "Status");
			grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_Process);
			grid.Create_GvColumn(gv, "라인", "LineCode", lup_Line);
			grid.Create_GvColumn(gv, "작업자", "EmpNo", lup_Worker);
			grid.Create_GvColumn(gv, "주야구분", "WorkBandDivCd", lup_Work_Band);

			grid.Create_GvColumn(gv, "등록자", "CreId", lup_EmployeeId);
			grid.Create_GvColumn(gv, "등록IP", "CreIP");
			grid.Create_GvColumn(gv, "등록일시", "CreDt");
			grid.Create_GvColumn(gv, "수정자", "ModId", lup_EmployeeId);
			grid.Create_GvColumn(gv, "수정IP", "ModIP");
			grid.Create_GvColumn(gv, "수정일시", "ModDt");

			//셀값 정렬
			gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center; //상태

			if (WrGlobal.ProcOption["BS"]["LineWorker_YN"] == "Y")
			{
				gv.Columns["ProcessCode"].Visible = false;
			}
			else
			{
				gv.Columns["LineCode"].Visible = false;
			}

			grid.Column_AllowEdit(gv.Columns["WorkBandDivCd"]);

			//칼럼 너비 설정
			gv.Columns["Status"].MaxWidth = 60;

			//이벤트 설정
			grid.RowNumber_Indicator(gv);       //행순번 표기
			gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
			gv.CellValueChanged += Gv_ProdtWorker_CellValueChanged;
		}

		private void Gv_ProdtWorker_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			GridView gv = sender as GridView;
			grid.Change_Status(gv, e.RowHandle);
		}

		#endregion GridView Setting End

		private void Search_ProcLine()
		{

			DataTable dt = new DataTable();

			if (LineWorker_YN == "Y")
			{
				CM_Line li = new CM_Line();
				dt = li.Select_CM_Line();
			}
			else
			{
				//공정 조회
				CM_Process cp = new CM_Process();
				cp.WorkType = "10";

				dt = cp.Select_Cm_ProcessByWorkType();
			}

			Gc_Process.DataSource = dt;

			//Gv_Process.BestFitColumns();   //Column 너비 조정
		}

		private void Search_Worker()
		{
			Gc_Worker.DataSource = code.GetWorker();
		}

		//작업자 리스트
		private void DisplayData()
		{
			CM_LineProcessWorker li = new CM_LineProcessWorker();
			li.ProcessCode = Selected_Process;
			li.LineCode = Selected_Line;

			gcEmp.DataSource = li.Select_ProdtWorkder();

			gvEmp.BestFitColumns();
		}

		private bool Check_Search()
		{
			if ((gvEmp.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
                                                                            OR Status = '삭제'").Length > 0)
			{
				if (MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					//다른항목 진행할 경우
					return true;
				}
				else
				{
					//다른항목 진행하지 않을경우
					return false;
				}
			}

			return true;
		}
		private void Btn_Delete_Click(object sender, EventArgs e)
		{
			Delete_Click();
		}

		private void Delete_Click()
		{
			//삭제 클릭시 삭제항목 표기
			Dictionary<string, object> dic = new Dictionary<string, object>();
			dic.Add("Status", "삭제");

			grid.Set_Row(gcEmp, dic, "Del");
		}

		//저장
		private void cmdSave_Click(object sender, EventArgs e)
		{
			try
			{
				gvEmp.PostEditor();
				gvEmp.UpdateCurrentRow();

				//작성내역 저장
				DataTable dt = gcEmp.DataSource as DataTable;

				//2. 데이터 저장
				for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
				{
					if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
					{
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

				//3. 조회
				Search_Worker();

				MessageBox.Show("저장이 정상적으로 완료되었습니다.");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private bool Check_SaveClick(DataTable dt)
		{
			//Item을 불러온 상태인지 검사
			if (dt == null) return false;

			int checkCnt = 0;

			foreach (DataRow dr in dt.Rows)
			{
				string status = dr["Status"].ConvertString_Null_To_Empty();

				switch (status)
				{
					case "신규":
						checkCnt++;
						break;

					case "수정":
						checkCnt++;
						break;

					//삭제내역 있으면 물어보기
					case "삭제":
						DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
						if (dialogResult == DialogResult.OK)
						{
							return true;
						}
						checkCnt++;
						break;
				}
			}

			if (checkCnt == 0) return false;

			return true;
		}

		private bool Save_ToDatabase(DataRow dr, char div)
		{
			//데이터베이스에 반영
			//1. 기본 셋팅
			CM_LineProcessWorker line = new CM_LineProcessWorker();
			line.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
			line.LineCode = dr["LineCode"].ConvertString_Null_To_Empty();
			line.EmpNo = dr["EmpNo"].ConvertString_Null_To_Empty();
			line.WorkBandDivCd = dr["WorkBandDivCd"].ConvertString_Null_To_Empty();
			//line.Seq = dr["Seq"];

			//2. DB 적용
			if (div == 'I')
			{
				//Insert
				if (!line.Ins_AuthLine())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}
			}
			else if (div == 'U')
			{
				if (!line.Upd_AuthLine())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}
			}
			else if (div == 'D')
			{
				if (!line.Del_AuthLine())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}
			}

			//상태값 초기화
			dr[0] = "";

			return true;
		}

		//상태값 변경
		private void gvRepair_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (e.Column.Name == "Status") return;  // 상태값이 바뀐거면 return;

			// 상태값 수정으로 변경
			Change_Status(gvEmp, e.RowHandle);

			gvEmp.BestFitColumns();
		}

		private void Change_Status(GridView gv, int rowHandle)
		{
			//셀값 수정 시 상태값 수정
			DataTable dt = gv.GridControl.DataSource as DataTable;
			string str_Status = Convert.ToString(gv.GetFocusedRowCellValue(dt.Columns[0].Caption));    //상태 값

			if (str_Status != "신규" &&
				str_Status != "신규(일괄)" &&
				str_Status != "수정" &&
				str_Status != "삭제")
			{
				gv.SetRowCellValue(rowHandle, gv.Columns[0].FieldName, "수정");
			}
		}

		//등록

		private void gvCM_Line_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			DisplayData();
		}


		//public string GetNextMachineRepairSeq()
		//{
			//string lineCode = Txt_Line.EditValue.ToString();
			//if (lineCode == "") return "";
			//DataTable dt = Txt_Line.Properties.DataSource as DataTable;

			//DataRow[] rows = dt.Select($"코드 = '{Txt_Line.EditValue.ToString()}'");
			//DataRow findRow = rows[0];

			//string processCode = findRow["ProcessCode"].ToString();

			//string query = $"SELECT ISNULL(MAX(Seq), 0) + 1 AS NextSeq FROM CM_LineProcessWorker WHERE ProcessCode= '{processCode}' AND LineCode = '{lineCode}'";
			//DataRowView drv = db.GetDataRecord(query);

			//if (drv != null)
			//{
			//	int nextSeq = Convert.ToInt32(drv[0]);
			//	return nextSeq.ToString();
			//}
			//else
			//{
			//	return "";
			//}
		//}

		private void cmdSearch_Click(object sender, EventArgs e)
		{
			DisplayData();
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}