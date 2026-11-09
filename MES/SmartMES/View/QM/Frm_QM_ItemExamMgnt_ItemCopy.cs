using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
	public partial class Frm_QM_ItemExamMgnt_ItemCopy : DevExpress.XtraEditors.XtraForm
	{
		clsGrid grid = new clsGrid();
		clsRepository rpo = new clsRepository();
		clsCode code = new clsCode();

		RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox
		RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력

		string Item_Div_Cd = "";
		string ExamDivCd = "";
		string ItemCode = "";

		bool selectState = false;

		public Frm_QM_ItemExamMgnt_ItemCopy()
		{
			InitializeComponent();
		}

		public Frm_QM_ItemExamMgnt_ItemCopy(string item_Div_Cd, string ExamDivCd, string ItemCode)
		{
			InitializeComponent();

			this.Item_Div_Cd = item_Div_Cd;
			this.ExamDivCd = ExamDivCd;
			this.ItemCode = ItemCode;

		}

		private void Frm_QM_ItemExamMgnt_ItemCopy_Load(object sender, EventArgs e)
		{

			Lup_Item_Div.Properties.DataSource = code.GetCM_CateItem(WrGlobal.CorpID, "A010", false);
			Lup_Item_Div.Properties.ValueMember = "코드";
			Lup_Item_Div.Properties.DisplayMember = "명칭";



			LupItemClass.Properties.DataSource = code.GetCM_CateItem(WrGlobal.CorpID, "A01210", new string[] { Item_Div_Cd });
			LupItemClass.Properties.ValueMember = "코드";
			LupItemClass.Properties.DisplayMember = "명칭";

			Set_ItemGridView(Gv_Item);
			Set_ItemExamGridView(Gv_ItemExam);

			////검사방법 데이터 입력
			//Set_ExamMethod_Lup(Lup_ExamMethod);

			////폼 닫을때 검사 작성된 내역 있는지 검사
			//this.FormClosing += new FormClosingEventHandler((sender, e) => {
			//	e.Cancel = false;
			//	if (grid.Check_Gv_FormClosing(Gv_ItemExam, "Status"))
			//	{
			//		e.Cancel = true;
			//	}
			//});

			Func_ExamSearch();
			Func_ItemSearch();

			Lup_Item_Div.EditValue = this.Item_Div_Cd;
			LupItemClass.EditValue = "ALL";
		}

		private void Set_ItemGridView(GridView gv)
		{
			//Column 생성 및 셋팅
			//Grid 공통 설정
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
			gv.OptionsFind.AlwaysVisible = true;


			//
			//gv.OptionsSelection.MultiSelect = true;
			//gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;


			RepositoryItem lup_ItemType = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목구분", "", ""), "코드", "코드명");
			//품목분류
			RepositoryItemSearchLookUpEdit lup_Class1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", false), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Class2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", false), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Class3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", false), "코드", "명칭");

			//Column 생성
			grid.Create_GvColumn(gv, "Selection", "ChkYN", rpo.Create_rpoChk());
			grid.Create_GvColumn(gv, "품목구분", "ItemType", lup_ItemType);
			grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

			//Repository 설정
			grid.Column_AllowEdit(gv.Columns["ChkYN"], true);
			grid.Column_AllowEdit(gv.Columns["ItemName"], false);

			if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class1;
			if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class2;
			if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class3;



			gv.Columns["ChkYN"].OptionsFilter.AllowFilter = false;
			gv.Columns["ChkYN"].OptionsFilter.AllowInHeaderSearch = DevExpress.Utils.DefaultBoolean.False;

			grid.RowNumber_Indicator(gv);       //행순번 표기
			//gv.ColumnFilterChanged += Gv_ColumnFilterChanged;
			//gv.BeforeLeaveRow += Gv_BeforeLeaveRow;
			//gv.RowStyle += Event_ChangeBG_ByHasExamMgnt;    //검사데이터 여부에따른 색상 지정
		}

		private void Set_ItemExamGridView(GridView gv)
		{
			//Column 생성 및 셋팅
			//Grid 공통 설정
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
			gv.OptionsBehavior.AutoPopulateColumns = false;


			QM_ExamMgnt Exam = new QM_ExamMgnt();
			RepositoryItem lup_Exam_Div = rpo.Create_RepositoryItem(code.GetCM_CateItem(WrGlobal.CorpID, "Q005", false), "코드", "명칭");
			RepositoryItem lup_ValueType = rpo.Create_RepositoryItem(code.GetCM_CateItem(WrGlobal.CorpID, "Q006", false), "코드", "명칭");
			RepositoryItem lup_ExamCd = rpo.Create_RepositoryItemSearchLookUp(Exam.Select_Exam_Lup(ExamDivCd), "ExamCd", "ExamNm");

			CM_Process proc = new CM_Process();
			RepositoryItemLookUpEdit lup_Proc_Cd = rpo.Create_RepositoryItem(proc.SelectCM_Process(false, new string[] { "10", "20" }), "코드", "명칭");

			//Column 생성
			grid.Create_GvColumn(gv, "상태", "Status");
			grid.Create_GvColumn(gv, "표시순번", "DispSeq");
			grid.Create_GvColumn(gv, "사용여부", "UseYn");
			grid.Create_GvColumn(gv, "검사구분", "ExamDivCd");
			grid.Create_GvColumn(gv, "공정구분", "ProcessCode", lup_Proc_Cd);
			grid.Create_GvColumn(gv, "검사코드", "ExamCd");
			grid.Create_GvColumn(gv, "검사명", "ExamNm");
			grid.Create_GvColumn(gv, "검사규격", "ExamSpec");
			grid.Create_GvColumn(gv, "검사방법", "ExamMethod");
			grid.Create_GvColumn(gv, "검사단위", "ExamUnit");
			grid.Create_GvColumn(gv, "검사주기", "ExamCycle");
			grid.Create_GvColumn(gv, "값 형태", "ValueTypeDivCd");
			grid.Create_GvColumn(gv, "소수점 자리수", "DecimalCnt");
			grid.Create_GvColumn(gv, "기본값", "DefaultValue");
			grid.Create_GvColumn(gv, "관리범위 하한", "MgntMin");
			grid.Create_GvColumn(gv, "관리범위 상한", "MgntMax");
			grid.Create_GvColumn(gv, "위험범위 하한", "DangerMin");
			grid.Create_GvColumn(gv, "위험범위 상한", "DangerMax");

			gv.Columns["Status"].Visible = false;
			gv.Columns["UseYn"].Visible = false;

			//Column 단일 설정
			//Repository 설정
			gv.Columns["ExamNm"].ColumnEdit = lup_ExamCd;
			//검사항목 - X1 ~ X5 CheckEdit 설정
			rpoChk = rpo.Create_rpoChk();
			rpoTxt = rpo.Create_rpoTxt_Numeric();
			//사용여부
			RepositoryItemCheckEdit chk_Use = rpo.Create_rpoChk();
			gv.Columns["ExamDivCd"].ColumnEdit = lup_Exam_Div;
			gv.Columns["ValueTypeDivCd"].ColumnEdit = lup_ValueType;
			gv.Columns["UseYn"].ColumnEdit = chk_Use;

			//셀값 변경 허용
			grid.Column_AllowEdit(gv.Columns["ExamNm"]);          //검사명
			grid.Column_AllowEdit(gv.Columns["ExamDivCd"]);      //검사구분
			grid.Column_AllowEdit(gv.Columns["ExamSpec"]);        //검사규격
			grid.Column_AllowEdit(gv.Columns["ExamMethod"]);      //검사방법
			grid.Column_AllowEdit(gv.Columns["ExamUnit"]);        //검사단위
			grid.Column_AllowEdit(gv.Columns["ExamCycle"]);       //검사주기
			grid.Column_AllowEdit(gv.Columns["ValueTypeDivCd"]); //값 형태
			grid.Column_AllowEdit(gv.Columns["DecimalCnt"]);      //소수점 자리수
			grid.Column_AllowEdit(gv.Columns["DefaultValue"]);    //기본값
			grid.Column_AllowEdit(gv.Columns["MgntMin"]);         //관리범위 하한
			grid.Column_AllowEdit(gv.Columns["MgntMax"]);         //관리범위 상한
			grid.Column_AllowEdit(gv.Columns["DangerMin"]);       //위험범위 하한
			grid.Column_AllowEdit(gv.Columns["DangerMax"]);       //위험범위 상한
			grid.Column_AllowEdit(gv.Columns["UseYn"]);           //사용여부

			//셀값 정렬
			gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

			grid.SetCol_Type(gv.Columns["DecimalCnt"], clsGrid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["MgntMin"], clsGrid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["MgntMax"], clsGrid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["DangerMin"], clsGrid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["DangerMax"], clsGrid.TypeList.TFloat);

			gv.Columns["UseYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //사용여부

			//이벤트 설정
			//gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;        //행번호 표기
			//gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;                 //삭제시 배경색 지정
			gv.CellValueChanged += Event_CellValueChanged;                  //셀값 수정시 상태값 수정
																			//gv.GridControl.ProcessGridKey += Event_ProcessGridKey;          //행추가, 삽입, 삭제 단축키기능 부여
			gv.ValidatingEditor += Event_ValidatingEditor;                  //코드 입력시 중복 확인
			gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //Row별 Repository 다르게하기

			if (WrGlobal.LoginID != "Dev")
			{
				gv.Columns["ExamCd"].Visible = false;

				gv.Columns["DangerMin"].Visible = false;
				gv.Columns["DangerMax"].Visible = false;
			}
		}

		private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			GridView gv = sender as GridView;
			string fieldName = e.Column.FieldName;
			DataRow drChanged = gv.GetDataRow(e.RowHandle);

			string value = e.Value.ConvertString_Null_To_Empty();

			//셀값 수정 시 상태값 수정
			if (e.Column.FieldName == gv.Columns[0].FieldName) return;
			grid.Change_Status(gv, e.RowHandle);

			if (fieldName == "ExamNm")
			{
				QM_ExamMgnt Exam = new QM_ExamMgnt();
				Exam.ExamCd = value;

				DataRow drExam = Exam.Select_Exam_Info();
				if (drExam == null) return;

				drChanged["ExamCd"] = drChanged["ExamNm"];

				if (drChanged.Table.Columns.Contains("ExamSpec")) { drChanged["ExamSpec"] = drExam["ExamSpec"]; }
				if (drChanged.Table.Columns.Contains("ExamMethod")) { drChanged["ExamMethod"] = drExam["ExamMethod"]; }
				if (drChanged.Table.Columns.Contains("ExamUnit")) { drChanged["ExamUnit"] = drExam["ExamUnit"]; }
				if (drChanged.Table.Columns.Contains("ExamCycle")) { drChanged["ExamCycle"] = drExam["ExamCycle"]; }
				if (drChanged.Table.Columns.Contains("ValueTypeDivCd")) { drChanged["ValueTypeDivCd"] = drExam["ValueTypeDivCd"]; }
				if (drChanged.Table.Columns.Contains("DecimalCnt")) { drChanged["DecimalCnt"] = drExam["DecimalCnt"]; }
				if (drChanged.Table.Columns.Contains("ProcessCode")) { drChanged["ProcessCode"] = drExam["ProcessCode"]; }
			}

			gv.BestFitColumns();
		}

		private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
		{
			//셀값 입력시 중복 검사
			GridView gv = sender as GridView;
			grid.Check_OverLap_CellValue(gv, e, "ExamCd");
			Check_SameExamNm(gv, e);
		}


		private void Check_SameExamNm(GridView gv, BaseContainerValidateEditorEventArgs e)
		{
			string ColName = gv.FocusedColumn.FieldName;

			if (ColName == "ExamCd")
			{
				for (int idx = 0; idx < gv.RowCount; idx++)
				{
					DataRow dr = gv.GetDataRow(idx);

					if (Convert.ToString(dr["ExamCd"]) == Convert.ToString(e.Value))
					{
						e.Valid = false;    //입력 불가 처리
						e.ErrorText = $"중복된 {gv.FocusedColumn.Caption}이(가) 있습니다.";  //경고 알림
						break;
					}
				}
			}
		}

		private void Event_GvCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
		{
			//검사항목 일정Row의 Repository다르게하기
			GridView gv = sender as GridView;

			if (e.RowHandle >= 0)
			{
				string fieldName = e.Column.FieldName;
				if (fieldName == "DefaultValue")
				{
					string type = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd"));

					if (type == "10")
					{
						//양/불 선택일경우
						e.RepositoryItem = rpoChk;
					}
					else if (type == "20")
					{
						//측정치 입력일경우
						e.RepositoryItem = rpoTxt;
					}
				}
				else if (fieldName == "ExamNm")
				{
					string ExamDivCd = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ExamDivCd"));
					ExamDivCd = ExamDivCd == "" ? "ALL" : ExamDivCd;

					QM_ExamMgnt Exam = new QM_ExamMgnt();
					RepositoryItem lup_ExamCd = rpo.Create_RepositoryItemSearchLookUp(Exam.Select_Exam_Lup(ExamDivCd), "ExamCd", "ExamNm");
					e.RepositoryItem = lup_ExamCd;
				}
			}
		}

		private void Func_ItemSearch()
		{
			//1. 조회 검사

			//2.품목 조회
			CM_Item cm = new CM_Item();
			cm.ItemType = Lup_Item_Div.EditValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : Lup_Item_Div.EditValue.ConvertString_Null_To_Empty();
			cm.ItemClass1 = LupItemClass.EditValue.ConvertString_Null_To_Empty().ToUpper() == "ALL" ? "" : LupItemClass.EditValue.ConvertString_Null_To_Empty();

			Gc_Item.DataSource = cm.Select_Item_HasExamMgnt(this.ExamDivCd);

			Gv_Item.BestFitColumns();   //Column 너비 조정

			if (Gv_Item.DataRowCount > 0)
				Func_ExamSearch();
		}

		private void Func_ExamSearch()
		{
			//1.조회 검사
			if (Gv_Item.RowCount < 1) return;

			//string ItemCode = Gv_Item.GetFocusedRowCellValue("ItemCode").ConvertString_Null_To_Empty();
			string ItemCode = this.ItemCode;

			//2.품목별검사항목 조회
			QM_ItemExamMgnt ItemExam = new QM_ItemExamMgnt();

			Gc_ItemExam.DataSource = ItemExam.Select_ItemExam(ItemCode, ExamDivCd);
			Gv_ItemExam.BestFitColumns();   //Column 너비 조정
		}

		private void Btn_Row_Del_Click(object sender, EventArgs e)
		{
			//삭제 메세지
			if(XtraMessageBox.Show("검사항목을 삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
			{
				return;
			}

			//삭제 처리
			Gv_ItemExam.DeleteSelectedRows();
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			this.Validate();

			Database db = new Database();
			string query = "";

			DataTable dtItem = Gc_Item.DataSource as DataTable;
			if (dtItem == null) return;

			DataTable dtExam = Gc_ItemExam.DataSource as DataTable;
			if (dtExam == null) return;
			if (dtExam.Rows.Count < 1) return;

			//정렬
			dtExam.DefaultView.Sort = "ExamDivCd ASC";

			DataTable dtExamDivCd = dtExam.DefaultView.ToTable(true, new string[] { "ExamDivCd" });


			for (int i = 0; i < Gv_Item.RowCount; i++)
			{
				DataRow dr= Gv_Item.GetDataRow(i);

				//체크한 품목
				if(Convert.ToString(dr["ChkYN"]) == "Y")
				{
					//해당하는 검사구분 항목 삭제

					foreach (DataRow item in dtExamDivCd.Rows)
					{
						query = $@"
							DELETE FROM QM_ItemExamMgnt
							WHERE CorpId = '{WrGlobal.CorpID}'
							AND ItemCode = '{dr["ItemCode"]}'
							AND ExamDivCd = '{item["ExamDivCd"]}'
							";

						db.ExecuteQuery(query);
					}

					int DispSeq = 1;
					string oldExamdiv = "";

					for (int j = 0; j < Gv_ItemExam.RowCount; j++)
					{
						DataRow drExam = Gv_ItemExam.GetDataRow(j);
						string curExamDiv = Convert.ToString(drExam["ExamDivCd"]);

						if (oldExamdiv == "")
						{
							oldExamdiv = curExamDiv;
						}
						else if (oldExamdiv != curExamDiv)
						{
							//순번 초기화
							DispSeq = 1;
							oldExamdiv = curExamDiv;

						}

						if (!Save_ToDatabase(Convert.ToString(dr["ItemCode"]), drExam, DispSeq++))
						{
							//저장 중 에러 발생시
							MessageBox.Show("저장 중 에러가 발생하였습니다.");
							return;
						}
					}
				}
			}

			XtraMessageBox.Show("저장이 완료되었습니다.");
		}

		private bool Save_ToDatabase(string ItemCode, DataRow dr, int DispSeq)
		{

			//데이터베이스에 반영
			//1. 기본 셋팅
			QM_ItemExamMgnt ItemExam = new QM_ItemExamMgnt();

			ItemExam.ItemCode = ItemCode;
			ItemExam.ExamDivCd = dr["ExamDivCd"].ConvertString_Null_To_Empty();
			ItemExam.ExamCd = dr["ExamCd"].ConvertString_Null_To_Empty();
			ItemExam.DispSeq = DispSeq;
			ItemExam.ExamSpec = dr["ExamSpec"].ConvertString_Null_To_Empty();
			ItemExam.ExamMethod = dr["ExamMethod"].ConvertString_Null_To_Empty();
			ItemExam.ExamUnit = dr["ExamUnit"].ConvertString_Null_To_Empty();
			ItemExam.ExamCycle = dr["ExamCycle"].ConvertString_Null_To_Empty();
			ItemExam.ValueTypeDivCd = dr["ValueTypeDivCd"].ConvertString_Null_To_Empty();
			ItemExam.DecimalCnt = dr["DecimalCnt"].ConvertInt_Null_To_Zero();
			ItemExam.DefaultValue = dr["DefaultValue"].ConvertString_Null_To_Empty();
			ItemExam.MgntMin = dr["MgntMin"].ConvertFloat_Null_To_Zero();
			ItemExam.MgntMax = dr["MgntMax"].ConvertFloat_Null_To_Zero();
			ItemExam.DangerMin = dr["DangerMin"].ConvertFloat_Null_To_Zero();
			ItemExam.DangerMax = dr["DangerMax"].ConvertFloat_Null_To_Zero();
			ItemExam.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

			if (!ItemExam.Ins_ItemExam())
			{
				//저장 중 에러 발생시
				MessageBox.Show("저장 중 에러가 발생하였습니다.");
				return false;
			}

			return true;
		}

		private void Btn_Row_Edit_Click(object sender, EventArgs e)
		{
			//행삽입, 추가, 삭제 버튼 클릭 이벤트
			//1. 이름의 마지막 3글자 가져오기
			smtSearchButton btn = sender as smtSearchButton;
			string txt_Act = btn.Tag.ConvertString_Null_To_Empty();

			//2. 입력 데이터 설정
			Dictionary<string, object> dic = new Dictionary<string, object>();
			if (txt_Act != "Del")
			{
				//삽입, 추가 진행인 경우
				dic.Add("Status", "신규");
				//dic.Add("Item_Div_Cd", Convert.ToString(Rdo_Exam_Div.EditValue) == "ALL" ? "" : Convert.ToString(Rdo_Exam_Div.EditValue));
				//dic.Add("ItemClass_Nm", Convert.ToString(Lup_ItemClass.EditValue) == "ALL" ? "" : Convert.ToString(Lup_ItemClass.EditValue));
				//dic.Add("BOM_Div_Cd", "40");
				dic.Add("ExamDivCd", this.ExamDivCd);
				dic.Add("UseYn", "Y");

				//3. 행삽입, 추가, 삭제 프로세스 진행
				grid.Set_Row(Gc_ItemExam, dic, txt_Act);
			}
			else
			{
				//삭제 진행인경우
				//dic.Add("Status", Convert.ToString(Gv_ItemExam.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
				Gv_ItemExam.DeleteRow(Gv_ItemExam.FocusedRowHandle);
			}


		}

		//선택한 로우 위, 아래로 이동
		private void Btn_RowMove(object sender, EventArgs e)
		{
			SimpleButton Btn = sender as SimpleButton;

			DataTable dt = Gv_ItemExam.GridControl.DataSource as DataTable;

			int idx = Gv_ItemExam.FocusedRowHandle;

			string BtnName = Btn.Name;

			if (BtnName.Contains("Up"))
			{
				if (idx == 0) return; // Top일 경우 종료
			}
			else
			{
				if (idx == dt.Rows.Count - 1) return; // Bottom일 경우 종료
			}

			DataRow curRow = dt.Rows[idx];
			curRow["Status"] = "수정";

			DataRow newRow = dt.NewRow();
			newRow.ItemArray = curRow.ItemArray; // 내용 복사

			dt.Rows.RemoveAt(idx);

			if (BtnName.Contains("Up"))
			{
				dt.Rows.InsertAt(newRow, idx - 1);

				if (idx == dt.Rows.Count - 1)
					Gv_ItemExam.FocusedRowHandle = Gv_ItemExam.FocusedRowHandle - 1;
				else
					Gv_ItemExam.FocusedRowHandle = Gv_ItemExam.FocusedRowHandle - 2;
			}
			else
			{
				dt.Rows.InsertAt(newRow, idx + 1);

				Gv_ItemExam.FocusedRowHandle++;
			}

			Gv_ItemExam.SelectRow(Gv_ItemExam.FocusedRowHandle);
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Rdo_Item_Div_EditValueChanged(object sender, EventArgs e)
		{
			string itemDiv = Lup_Item_Div.EditValue.ConvertString_Null_To_Empty();
			this.Item_Div_Cd = itemDiv;
			Func_ItemSearch();


			LupItemClass.Properties.DataSource = code.GetCM_CateItem(WrGlobal.CorpID, "A01210", new string[] { Item_Div_Cd });
			LupItemClass.Properties.ValueMember = "코드";
			LupItemClass.Properties.DisplayMember = "명칭";
		}

		private void LupItemClass_EditValueChanged(object sender, EventArgs e)
		{
			Func_ItemSearch();
		}

		private void BtnAllCheck_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < Gv_Item.RowCount; i++)
			{
				Gv_Item.SetRowCellValue(i, "ChkYN", "Y");
			}
		}
	}
}