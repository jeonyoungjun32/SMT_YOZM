using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.QM
{
    public partial class Frm_QM_4MChangeView : DevExpress.XtraEditors.XtraForm
	{
		clsGrid grid = new clsGrid();
		clsRepository rpo = new clsRepository();
		clsCode code = new clsCode();

		public Frm_QM_4MChangeView()
		{
			InitializeComponent();
		}

		private void Frm_PD_4M_Change_Load(object sender, EventArgs e)
		{
			Set_Design();

			Set_GridView(Gv_List);

			Func_Search();
		}

		private void Set_Design()
        {
			DataTable dt = code.smtLookUp("품목", new string[] { "", "10,20" }, "전체");

            Lup_Item.smtLookUpDataSource = dt;
			Lup_Item.smtLookUpDataDisplayMember = "품명";
			Lup_Item.smtLookUpDataValueMember = "품목코드";
			Lup_Item.smtLookUpValue = "ALL";
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
			grid.Create_GvColumn(gv, "등록IP", "CreIP");
			grid.Create_GvColumn(gv, "등록일시", "CreDt");
			grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
			grid.Create_GvColumn(gv, "수정IP", "ModIP");
			grid.Create_GvColumn(gv, "수정일시", "ModDt");

			//셀값 정렬
			gv.Columns["ConfirmYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //승인여부

			//값 정렬
			grid.SetCol_Type(gv.Columns["ChangeDate"], clsGrid.TypeList.TDate);
		}

		private void Func_Search()
        {
			//1. 조회 검사
			if (!Check_Search()) return;

			//2. 창고 조회
			QM_4MChange pd4m = new QM_4MChange();
			//pd4m.ConfirmYn = Convert.ToString(Rdo_ConfirmYn.EditValue) == "ALL" ? "" : Convert.ToString(Rdo_ConfirmYn.EditValue) == "10" ? "Y" : "N";
			pd4m.ItemCode = Lup_Item.smtLookUpValue.ConvertString_Null_To_Empty();

			Gc_List.DataSource = pd4m.Select_QM_4MChange();
			Gv_List.BestFitColumns();
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

		private void btn_Search_Click(object sender, EventArgs e)
		{
			Func_Search();
		}

		private void btn_Excel_Click(object sender, EventArgs e)
		{
			clsFunction func = new clsFunction();
			func.ExportExcel(Gv_List);
		}

		private void btn_Close_Click(object sender, EventArgs e)
        {
			this.Close();
        }
    }
}