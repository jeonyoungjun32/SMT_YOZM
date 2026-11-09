using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
	public partial class Frm_QM_4MChange_ConfirmHistory : DevExpress.XtraEditors.XtraForm
	{
		clsGrid grid = new clsGrid();
		clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        public Frm_QM_4MChange_ConfirmHistory()
		{
			InitializeComponent();
		}

		private void Frm_PD_4M_Approve_Load(object sender, EventArgs e)
		{
			Set_Design();

			Set_GridView(Gv_List);
			Set_GridView_History(Gv_Detail);

			Func_Search();
		}

		private void Set_Design()
		{
			//품목
            DataTable dt = code.smtLookUp("품목", new string[] { "", "10,20" }, "전체");

            Lup_Item.smtLookUpDataSource = dt;
            Lup_Item.smtLookUpDataDisplayMember = "품명";
            Lup_Item.smtLookUpDataValueMember = "품목코드";
            Lup_Item.smtLookUpValue = "ALL";

            DataTable dtConfirm = code.smtLookUp("4M관리", "", "전체");
			Lup_Confirm.smtLookUpDataSource = dtConfirm;
			Lup_Confirm.smtLookUpDataDisplayMember = "코드명";
			Lup_Confirm.smtLookUpDataValueMember = "코드";
			Lup_Confirm.smtLookUpValue = "ALL";
        }

		private void Set_GridView(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			//gv.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //그리드 기분 중앙 정렬
			gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
			gv.IndicatorWidth = 50;                     //순번 너비
			gv.OptionsView.AllowHtmlDrawHeaders = true;

			//4M구분
			RepositoryItemSearchLookUpEdit lup_4M = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("4M구분", "", ""), "코드", "코드명");
			//품명
			RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목", new string[] { "", "10,20" }, ""), "품목코드", "품명");
            //공정
            RepositoryItemSearchLookUpEdit lup_Proc = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("공정", "", ""), "공정코드", "공정명");
            //설비
            RepositoryItemSearchLookUpEdit lup_machine = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("설비", "", ""), "설비코드", "설비명");
            //하위품목
            RepositoryItemSearchLookUpEdit lup_Mat_Item = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("자재품목", new string[] { "30,40" }, ""), "품목코드", "품명");
			//사원번호
			RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("사용자", "", ""), "사원ID", "사원명");

			// 승인여부
			RepositoryItemCheckEdit chk_ConfirmYn = rpo.Create_rpoChk();

			grid.Create_GvColumn(gv, "4M 번호", "ChangeNo");
			grid.Create_GvColumn(gv, "등록일자", "ChangeDate");
			grid.Create_GvColumn(gv, "4M관리", "ChangeType", lup_4M);
			grid.Create_GvColumn(gv, "품목", "ItemCode", lup_Item);
			grid.Create_GvColumn(gv, "공정", "ProcessCode", lup_Proc);
			grid.Create_GvColumn(gv, "설비", "MachineCode", lup_machine);
			grid.Create_GvColumn(gv, "하위품목", "MItemCode", lup_Mat_Item);
			grid.Create_GvColumn(gv, "작업자", "EmpId", lup_Emp);
			grid.Create_GvColumn(gv, "사유", "Remark");
			grid.Create_GvColumn(gv, "승인여부", "ConfirmYn", chk_ConfirmYn);
			grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp);
			grid.Create_GvColumn(gv, "등록일시", "CreDt");
			grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
			grid.Create_GvColumn(gv, "수정일시", "ModDt");

			//셀값 정렬
			gv.Columns["ConfirmYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //승인여부

			//값 정렬
			grid.SetCol_Type(gv.Columns["ChangeDate"], clsGrid.TypeList.TDate);

			gv.FocusedRowChanged += Event_FocusedRowChanged;
			//gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
		}

		private void Set_GridView_History(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			//gv.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //그리드 기분 중앙 정렬
			gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
			gv.IndicatorWidth = 50;                     //순번 너비
			gv.OptionsView.AllowHtmlDrawHeaders = true;

			//사원번호
			RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("사용자", "", ""), "사원ID", "사원명");

			// 승인여부
			RepositoryItemCheckEdit chk_Yn = rpo.Create_rpoChk();

			grid.Create_GvColumn(gv, "4M 번호", "ChangeNo");
			grid.Create_GvColumn(gv, "이력ID", "HistoryID");
			grid.Create_GvColumn(gv, "변경 Revision", "RevisionNo");
			grid.Create_GvColumn(gv, "변경전", "BeforeContent");
			grid.Create_GvColumn(gv, "변경후", "AfterContent");
			grid.Create_GvColumn(gv, "승인결재", "ApprovalNo");
			grid.Create_GvColumn(gv, "사용여부", "UseYn", chk_Yn);
			grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp);
			grid.Create_GvColumn(gv, "등록일시", "CreDt");
			grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
			grid.Create_GvColumn(gv, "수정일시", "ModDt");

			//셀값 정렬
			gv.Columns["UseYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //사용여부
		}

		private void Event_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			Func_Search_History();
		}

		private void Func_Search()
		{
			//1. 조회 검사
			if (!Check_Search()) return;

            //2. 창고 조회
            QM_4MChange pd4m = new QM_4MChange();
			pd4m.ConfirmYn = Lup_Confirm.smtLookUpValue;
			pd4m.ItemCode = Lup_Item.smtLookUpValue;

			Gc_List.DataSource = pd4m.Select_QM_4MChange();
			Gv_List.BestFitColumns();
		}

		private void Func_Search_History()
		{
			if (Gv_List.FocusedRowHandle < 0)
			{
				Gc_Detail.DataSource = null;
				return;
			}

			DataRow dr = Gv_List.GetDataRow(Gv_List.FocusedRowHandle);
			if (dr == null) return;

			QM_4MChangeHistory hist = new QM_4MChangeHistory();
			hist.ChangeNo = dr["ChangeNo"].ConvertString_Null_To_Empty();

			Gc_Detail.DataSource = hist.Select_QM_4MChangeHistory();

			Gv_Detail.BestFitColumns();
		}

		private bool Check_Search()
		{
			if ((Gv_List.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
                                                                      OR Status = '수정'
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

		private void Btn_RowEdit(object sender, EventArgs e)
		{
			//행삽입, 추가, 삭제 버튼 클릭 이벤트
			//1. 이름의 마지막 3글자 가져오기
			SimpleButton btn = sender as SimpleButton;
			string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

			//2. 입력 데이터 설정
			Dictionary<string, object> dic = new Dictionary<string, object>();
			if (txt_Act != "Del")
			{
				//삽입, 추가 진행인 경우
				dic.Add("Status", "신규");
				dic.Add("ChangeDate", DateTime.Now.ToString("yyyy-MM-dd"));
				dic.Add("ConfirmYn", "N");
			}
			else
			{
				//삭제 진행인경우
				dic.Add("Status", Convert.ToString(Gv_List.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
			}

			//3. 행삽입, 추가, 삭제 프로세스 진행
			grid.Set_Row(Gc_List, dic, txt_Act);
		}

		private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			GridView gv = sender as GridView;

			//셀값 수정 시 상태값 수정
			if (e.Column.FieldName == gv.Columns[0].FieldName) return;
			grid.Change_Status(gv, e.RowHandle);
		}

        private void BtnSave_Click(object sender, EventArgs e)
        {
			Func_Save();
		}

		private void Func_Save()
		{
			Gv_List.PostEditor();           //입력한 값을 확정
			Gv_List.UpdateCurrentRow();     //현재 행(Row)의 변경 내용을 데이터 소스에 적용

			//작성내역 저장
			DataTable dt = Gc_List.DataSource as DataTable;

			//1. 데이터 불러온 상태인지 검사
			if (!Check_SaveClick(dt)) return;

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

			//3. 정상절차 진행
			Func_Search();

			MessageBox.Show("저장이 정상적으로 완료되었습니다.");
		}

		private bool Check_SaveClick(DataTable dt)
		{
			//Data를 불러온 상태인지 검사
			if (dt == null) return false;

			int checkCnt = 0;
			bool isDelete = false;

			foreach (DataRow dr in dt.Rows)
			{
				string status = dr["Status"].ConvertString_Null_To_Empty();

				switch (status)
				{
					case "신규":

						checkCnt++;
						break;

					case "삭제":
						checkCnt++;
						isDelete = true;
						break;
				}
			}

			if (isDelete)
			{
				DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);

				if (dialogResult == DialogResult.OK)
				{
					return true;
				}
			}

			return true;
		}

		private bool Save_ToDatabase(DataRow dr, char div)
		{
			//데이터베이스에 반영
			//1. 기본 셋팅
			QM_4MChange pd4m = new QM_4MChange();

			pd4m.ChangeNo = dr["ChangeNo"].ConvertString_Null_To_Empty();
			if (div != 'D')
			{
				//Delete제외 공통
				pd4m.ChangeDate = dr["ChangeDate"].ConvertDate_To_Char8();
				pd4m.ChangeType = dr["ChangeType"].ConvertString_Null_To_Empty();
				pd4m.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
				pd4m.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
				pd4m.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();
				pd4m.MItemCode = dr["MItemCode"].ConvertString_Null_To_Empty();
				pd4m.EmpId = dr["EmpId"].ConvertString_Null_To_Empty();
				pd4m.Remark = dr["Remark"].ConvertString_Null_To_Empty();
				pd4m.ConfirmYn = dr["ConfirmYn"].ConvertString_Null_To_Empty();
			}

			//2. DB 적용
			if (div == 'I')
			{
				//Insert
				if (!pd4m.Ins_QM_4MChange())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}

				//추가 변경사항 private 따로 만들기
			}
			else if (div == 'U')
			{
				//Update
				if (!pd4m.Upd_QM_4MChange())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}

				//추가 변경사항 private 따로 만들기
			}
			else if (div == 'D')
			{
				//Delete
				if (!pd4m.Del_QM_4MChange())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}

				//추가 변경사항 private 따로 만들기
			}

			//상태값 초기화
			dr[0] = "";

			return true;
		}

        private void btn_Search_Click(object sender, EventArgs e)
        {
			Func_Search();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
			this.Close();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(Gv_List);
        }
    }
}