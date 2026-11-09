using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.QM.Gauge
{
	public partial class Frm_QM_GaugeView : DevExpress.XtraEditors.XtraForm
	{
		clsGrid grid = new clsGrid();
		clsRepository rpo = new clsRepository();
		clsCode code = new clsCode();

		public Frm_QM_GaugeView()
		{
			InitializeComponent();
		}

		private void Frm_QM_GaugeView_Load(object sender, EventArgs e)
		{
			SetDesign();
			Search();
		}

		#region 디자인 아이템 생성

		private void SetDesign()
		{
			//GridView 데이터 입력
			Set_Gv(Gv_Gauge);
		}

		#region GridView Setting

		//설비리스트
		private void Set_Gv(GridView gv)
		{
			//GvMachine Column 생성 및 셋팅
			//Grid 공통 설정
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
			gv.OptionsView.ColumnAutoWidth = true;     //Column 많으면 스크롤 생기도록 바꾸기
			gv.IndicatorWidth = 50;                     //순번 너비
														//검색창
			gv.OptionsFind.AlwaysVisible = false;
			gv.OptionsView.ShowGroupPanel = false;

			//BS_Line line = new BS_Line();
			//BS_Process process = new BS_Process();
			//DataTable dtEmp = new SC_Emp().Select_Emp("All");
			//SC_Emp ScEmp = new SC_Emp(); //등록,수정자    

			//RepositoryItemSearchLookUpEdit lup_Emp_No_ = rpo.Create_RepositoryItemSearchLookUp(ScEmp.Select_Emp_CdNm(), "Emp_No", "Emp_Nm");
			//RepositoryItemSearchLookUpEdit lup_Line_Cd = rpo.Create_RepositoryItemSearchLookUp(line.Select_Line("All"), "코드", "명칭");
			//RepositoryItemSearchLookUpEdit lup_Process_Cd = rpo.Create_RepositoryItemSearchLookUp(process.Select_Process("All"), "코드", "명칭");
			RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

			////Column 생성
			grid.Create_GvColumn(gv, "상태", "Status");
			grid.Create_GvColumn(gv, "계측기코드", "GaugeCode");
			grid.Create_GvColumn(gv, "계측기명", "GaugeName");
			grid.Create_GvColumn(gv, "관리번호", "SerialNo");
			grid.Create_GvColumn(gv, "메이커", "Maker");
			grid.Create_GvColumn(gv, "구입일자", "PurchaseDate", rpo.Create_rpoDate());

			grid.Create_GvColumn(gv, "교정주기(일)", "CalCycleDay", rpo.Create_rpoTxt_Numeric());
			grid.Create_GvColumn(gv, "최근교정일", "LastCalDate", rpo.Create_rpoDate());
			grid.Create_GvColumn(gv, "차기교정일", "NextCalDate", rpo.Create_rpoDate());
			grid.Create_GvColumn(gv, "비고", "Remark");

			grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
			grid.Create_GvColumn(gv, "등록IP", "CreIP");
			grid.Create_GvColumn(gv, "등록일시", "CreDt");

			grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
			grid.Create_GvColumn(gv, "수정IP", "ModIP");
			grid.Create_GvColumn(gv, "수정일시", "ModDt");

			////Column 단일 설정
			////VISIBLE
			//gv.Columns["Mearsuring_Image"].Visible = false; //추후 이미지 요청시 오픈

			////Repository 설정

			////셀값 정렬
			grid.SetCol_Type(gv.Columns["PurchaseDate"], clsGrid.TypeList.TDate);
			grid.SetCol_Type(gv.Columns["LastCalDate"], clsGrid.TypeList.TDate);
			grid.SetCol_Type(gv.Columns["NextCalDate"], clsGrid.TypeList.TDate);
			grid.SetCol_Type(gv.Columns["CalCycleDay"], clsGrid.TypeList.TInt);

			gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

			////칼럼 너비 설정
			//gv.Columns["Status"].MaxWidth = 60;

			//이벤트 설정
			gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
		}

		#endregion GridView Setting End

		#endregion 디자인 아이템 생성 종료

		#region Event


		#endregion Event End

		#region 조회 기능

		private void Search()
		{
			//2. 조회
			QM_Gauge measuring = new QM_Gauge();
			Gc_Gauge.DataSource = measuring.Select_QM_Gauge();

			Gv_Gauge.BestFitColumns();   //Column 너비 조정
		}

		private void Fun_Search_Image(string Machine_Cd)
		{
			////이미지 조회
			//BS_Machine machine = new BS_Machine();
			//DataRow drmachine = machine.Select_MachineInfo(Machine_Cd);

			//Machine_Image.EditValue = drmachine?["Machine_Image"];
		}

		#endregion 조회 기능 Endz

		#region 저장 기능

		private void cmd_Click(object sender, EventArgs e)
		{
			smtSearchButton Btn = (smtSearchButton)sender;

			string btnname = Btn.Name.ToString();

			switch (btnname)
			{
				case "cmdSearch":
					Search();
					break;

				case "cmdExcel":
					clsFunction func = new clsFunction();
					func.ExportExcel(Gv_Gauge);
					break;

				case "cmdClose":
					this.Close();
					break;
			}
		}

		private void PictureSave()
		{
			//    DataRow dr = Gv_Measuring.GetFocusedDataRow();
			//    byte[] image = Machine_Image.EditValue as byte[];

			//    BS_Machine machine = new BS_Machine();
			//    machine.Machine_Cd = Convert.ToString(dr["Machine_Cd"]);
			//    machine.Machine_Image = image;


			//    if (!machine.Upd_MachineImage()) { MessageBox.Show("이미지저장 중 에러가 발생하였습니다."); }
		}

		#endregion 저장 기능 End

		private void BtnPrint_Click(object sender, EventArgs e)
		{
			//DataRow dr = Gv_Measuring.GetFocusedDataRow();

			//if (dr == null)
			//{
			//    XtraMessageBox.Show("선택된 설비가 없습니다.");
			//    return;
			//}

			//BS_Machine machine = new BS_Machine();

			//string FocusMachinCd = Convert.ToString(dr["Machine_Cd"]);
			//machine.FocusMachinCd = FocusMachinCd;

			//DataTable MachineHistory = machine.Select_EquipmentManagement();

			//Rpt_Machine_Management report = new Rpt_Machine_Management(MachineHistory);
			//ReportPrintTool reportTool = new ReportPrintTool(report);

			//reportTool.PrinterSettings.Collate = false;
			//reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
			//reportTool.PreviewForm.PrintControl.ResumeLayout();

			//reportTool.ShowPreviewDialog();
		}
	}
}