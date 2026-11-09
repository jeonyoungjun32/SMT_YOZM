using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Common;
using SmartPOP.Models;
using SmartPOP.Models.PM;
using SmartPOP.Views.Common;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.PM
{
	public partial class Frm_PM_OrderProcess_View : DevExpress.XtraEditors.XtraForm
	{
		public delegate void FormSendDataHandler(DataRow dr);
		public event FormSendDataHandler FormSendEvent;

		cls_Grid grid = new cls_Grid();
		cls_Repository rpo = new cls_Repository();

		clsCode code = new clsCode();
		Database db = new Database();
		string query = "";
		string Target = ""; //터치패드 입력 대상

		public Frm_PM_OrderProcess_View()
		{
			InitializeComponent();

			Set_GridView(gv_ProcessCmd);
		}

		private void FrmPOPPM_OrderProcess_Load(object sender, EventArgs e)
		{
			DtpFrom.EditValue = DateTime.Now.AddDays(-3);
			DtpTo.EditValue = DateTime.Now;

			Set_Div_Rdo();

			//달력(현장화면용)크기세팅
			rpo.Set_POP_Dtp(DtpFrom);
			rpo.Set_POP_Dtp(DtpTo);

			//날짜 선택 시 조회 되도록
			this.DtpFrom.EditValueChanged += new System.EventHandler(this.Dtp_EditValueChanged);
			this.DtpTo.EditValueChanged += new System.EventHandler(this.Dtp_EditValueChanged);

			Search();
		}


		#region  초기 디자인세팅
		private void Set_Div_Rdo()
		{
			cls_RadioGroup rdo = new cls_RadioGroup();
			//cls_RadioGroup clsRadio = new cls_RadioGroup();
			//SC_CmCd cmCd = new SC_CmCd();

			////라인
			//DataTable dtLine = new BS_Line().Select_Line_CdNm(true);
			//clsRadio.Create_RadioGroupItem(Rdo_Line_Div, dtLine, "명칭", "코드");
			//Set_Rdo_Width(Rdo_Line_Div); //길이조절
			//Rdo_Line_Div.EditValue = "All";

			////사용자정보 라인코드, 공정코드 디폴트로 조회
			//SC_Emp emp = new SC_Emp();
			//emp.Emp_No = WrGlobal.LoginID;

			//DataTable dt = emp.Select_Emp();

			//if (dt != null)
			//{
			//	if (dt.Rows.Count > 0)
			//	{
			//		DataRow dr = dt.Rows[0];
			//		Rdo_Line_Div.EditValue = dr["Line_Cd"];
			//		//LupProcess.EditValue = dr["Process_Cd"];	드림 공정코드 없으므로 주석 처리
			//	}
			//}

			////주야 구분
			//cmCd.Cm_Class = "050";
			//clsRadio.Create_RadioGroupItem(Rdo_Work_Band_Div, cmCd.Select_CmCd(), "Cm_Nm", "Cm_Cd");
			Set_Rdo_Width(Rdo_Work_Band_Div); //길이조절
		}

		private void Set_Rdo_Width(RadioGroup rdoDiv)
		{
			//길이 조정
			rdoDiv.Width = 0;
			foreach (RadioGroupItem item in rdoDiv.Properties.Items)
			{
				rdoDiv.Width += (item.Description.Length * 38) + 30;
			}
		}

		private void Set_Calendar(DateEdit Cal)
		{
			//달력 디자인 변경
			//클릭이벤트 추가(화살표가 아닌 다른영역을 클릭해도 팝업활성화)
			Cal.MouseDown += (sender, e) => DateEdit_MouseDown(sender, e, Cal);
			Cal.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			//달력 팝업 크기 키우기
			Cal.Properties.CellSize = new Size(70, 60);
			//달력 날짜 크기 키우기
			int C_FontSize = 8;
			Cal.Properties.AppearanceCalendar.DayCellHoliday.FontSizeDelta = C_FontSize;
			Cal.Properties.AppearanceCalendar.DayCell.FontSizeDelta = C_FontSize;
			Cal.Properties.AppearanceCalendar.DayCellHighlighted.FontSizeDelta = C_FontSize;
			Cal.Properties.AppearanceCalendar.DayCellSelected.FontSizeDelta = C_FontSize;
			Cal.Properties.AppearanceCalendar.DayCellPressed.FontSizeDelta = C_FontSize;
			Cal.Properties.AppearanceCalendar.DayCellToday.FontSizeDelta = C_FontSize;
			Cal.Properties.AppearanceCalendar.DayCellToday.BackColor = Color.LightGray;
			Cal.Properties.AppearanceCalendar.Header.FontSizeDelta = C_FontSize;
			Cal.Properties.AppearanceCalendar.Header.ForeColor = Color.Black;
			Cal.Properties.AppearanceCalendar.HeaderPressed.FontSizeDelta = C_FontSize;
			Cal.Properties.AppearanceCalendar.HeaderHighlighted.FontSizeDelta = C_FontSize;
		}
		private void DateEdit_MouseDown(object sender, MouseEventArgs e, DateEdit Cal)
		{
			// MouseDown 이벤트가 발생한 위치가 화살표 버튼이 아닌 다른 UI인 경우
			if (e.Button == MouseButtons.Left && e.X > Cal.Width - Cal.Properties.Buttons[0].Width)
			{
				// 팝업 창 띄우기
				Cal.ShowPopup();
			}
		}

		#region grid setting
		private void Set_GridView(GridView gv)
		{
			gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
																												   //gv_ProcessCmd.OptionsView.ColumnAutoWidth = false;
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


			//그리드 시작 버튼 추가
			RepositoryItemHypertextLabel Gird_btn_Start = new RepositoryItemHypertextLabel();
			Gird_btn_Start.AutoHeight = false;
			Gird_btn_Start.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			Gird_btn_Start.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[]
				{new DevExpress.XtraEditors.Controls.EditorButton()});
			Gird_btn_Start.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			Gird_btn_Start.ContextImageOptions.SvgImage = global::SmartPOP.Properties.Resources.gettingstarted;
			Gird_btn_Start.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
			Gird_btn_Start.Click += new System.EventHandler(btn_Start_Click);

			//그리드 종료 버튼 추가
			RepositoryItemHypertextLabel Gird_btn_End = new RepositoryItemHypertextLabel();
			Gird_btn_End.AutoHeight = false;
			Gird_btn_End.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			Gird_btn_End.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[]
				{new DevExpress.XtraEditors.Controls.EditorButton()});
			Gird_btn_End.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
			Gird_btn_End.ContextImageOptions.SvgImage = global::SmartPOP.Properties.Resources.actions_deletecircled;
			Gird_btn_End.Padding = new System.Windows.Forms.Padding(16, 0, 0, 0);
			Gird_btn_End.Click += new System.EventHandler(btn_End_Click);

			grid.Create_GvColumn(gv, "지시번호", "OrderNo");
			grid.Create_GvColumn(gv, "지시번호순번", "OrderSeq");
			grid.Create_GvColumn(gv, "지시일자", "OrderDate");
			grid.Create_GvColumn(gv, "상태", "Status");
			grid.Create_GvColumn(gv, "시작", "Btn_Start", Gird_btn_Start);
			grid.Create_GvColumn(gv, "종료", "Btn_End", Gird_btn_End);

			//grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

			grid.Create_GvColumn(gv, "라인", "LineName");
			grid.Create_GvColumn(gv, "공정", "ProcessName");
			grid.Create_GvColumn(gv, "설비", "MachineName");
			grid.Create_GvColumn(gv, "품명", "ItemName");

			grid.Create_GvColumn(gv, "지시수량", "OrderQty");
			grid.Create_GvColumn(gv, "총생산량", "TotalWorkQty");
			grid.Create_GvColumn(gv, "비고", "Remark");

			//VISIBLE
			gv.Columns["OrderNo"].Visible = false;   //공정지시번호
			gv.Columns["OrderSeq"].Visible = false;   //공정지시번호
			gv.Columns["Btn_End"].Visible = false;      //종료버튼
			gv.Columns["OrderDate"].Visible = false;   //공정지시번호

			//Repository
			if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
			if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
			if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
			if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;


			// 컬럼 너비 조절

			gv.Columns["Btn_Start"].MaxWidth = 70;
			gv.Columns["OrderQty"].MaxWidth = 200;

			//gv.Columns["Cust_Cd"].MinWidth = 130;

			//수정가능 되어 있지 않으면 그리드 버튼 클릭 불가
			gv.Columns["Btn_Start"].OptionsColumn.AllowEdit = true;
			gv.Columns["Btn_End"].OptionsColumn.AllowEdit = true;

			//값 형태
			grid.SetCol_Type(gv.Columns["OrderQty"], cls_Grid.TypeList.TFloat);
			grid.SetCol_Type(gv.Columns["TotalWorkQty"], cls_Grid.TypeList.TFloat);

			//정렬
			gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			//gv.Columns["Start_Time"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			//gv.Columns["End_Time"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

			//셀클릭 시 표기 색깔

			//이벤트
			grid.SetPOPGrid(gv);
			gv.FocusedRowObjectChanged += Event_FocusChange;
			gv.DoubleClick += gv_ProcessCmd_DoubleClick;
			gv.CustomDrawCell += RowCellDisplayValue; //진행 상황표기
		}
		#endregion setting

		#endregion 초기 디자인세팅 End

		private void Event_FocusChange(object sender, FocusedRowObjectChangedEventArgs e)
		{
			//GridView gv = sender as GridView;
			//DataRow dr = gv.GetDataRow(e.FocusedRowHandle);

			//string rmk1 = dr["Remark"].ConvertString_Null_To_Empty();
			//TxtRmk1.Text = rmk1;
		}
		private void RowCellDisplayValue(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
			// 셀 스타일 변경 로직
			GridView gv = sender as GridView;
			string fieldName = e.Column.FieldName;

			//if (fieldName.Contains("Status") == false) return;

			string cellValue = Convert.ToString(gv.GetRowCellValue(e.RowHandle, e.Column));

			if (fieldName.Contains("Status"))
			{
				if (cellValue == "작업중")
				{
					//진행
					e.Appearance.ForeColor = Color.DarkGreen;
					e.DisplayText = "● " + cellValue;
				}
				else if (cellValue == "대기")
				{
					//대기
					e.Appearance.ForeColor = Color.Black;
					e.DisplayText = "● " + cellValue;
				}
				else if (cellValue == "작업완료")
				{
					//종료
					e.Appearance.ForeColor = Color.Red;
					e.DisplayText = "● " + cellValue;
				}
			}
		}

		private void Btn_Click(object sender, EventArgs e)
		{
			SimpleButton Btn = (SimpleButton)sender;

			string btnname = Btn.Name.ToString();

			DataRow dr = gv_ProcessCmd.GetFocusedDataRow();


			switch (btnname)
			{
				case "Btn_Search":
					Search();
					break;

				case "Btn_Send":
					Send_Data(dr);
					break;

				case "Btn_Back":
					End();
					break;

				case "Btn_Work_Standard_Sheet":
					string materialCode = "";

					if (dr != null)
					{
						materialCode = Convert.ToString(dr["MaterialCode"]);
					}
					Opensheet(materialCode);
					break;

				case "BtnWorkSheet_History":
					//Frm_Item_Specification_POP frm = new Frm_Item_Specification_POP(item_Cd);
					//frm.ShowDialog();
					break;
			}
		}

		private void gv_ProcessCmd_DoubleClick(object sender, EventArgs e)
		{
			DataRow dr = gv_ProcessCmd.GetFocusedDataRow();
			Send_Data(dr);
		}

		private void Send_Data(DataRow dr)
		{
			try
			{
				if (dr == null) return;
				//지시 DataRow 전달

				FormSendEvent(dr);
				this.Close();
			}
			catch
			{
			}
		}

		#region 조회
		private void Search()
		{
			string startDt = DtpFrom.DateTime.ToString("d");
			string endDt = DtpTo.DateTime.ToString("d");

			PM_OrderProcess op = new PM_OrderProcess();
			op.Status = Rdo_Cmd_Div.EditValue.ConvertString_Null_To_Empty();

			////op.ProcessCode = rdop.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Process.smtLookUpValue;
			//op.MachineCode = Lup_Machine.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Machine.smtLookUpValue;
			//rst.MaterialCode = Lup_Item.smtLookUpValue.ToUpper() == "ALL" ? "" : Lup_Item.smtLookUpValue;

			gc_ProcessCmd.DataSource = op.Select_OrderProcess_Result(startDt, endDt);

			gv_ProcessCmd.BestFitColumns();
		}

		private void Rdo_Cmd_Div_SelectedIndexChanged(object sender, EventArgs e)
		{
			Search();
		}

		private void Rdo_Line_Div_SelectedIndexChanged(object sender, EventArgs e)
		{
			Search();
		}

		private void Rdo_Work_Band_Div_SelectedIndexChanged(object sender, EventArgs e)
		{
			Search();
		}

		private void Dtp_EditValueChanged(object sender, EventArgs e)
		{
			Search();
		}

		#endregion 조회

		#region 작업상태
		private void btn_Start_Click(object sender, EventArgs e)
		{
			DataTable dt = gc_ProcessCmd.DataSource as DataTable;
			DataRow dr = gv_ProcessCmd.GetFocusedDataRow();

			//1-1. 진행, 완료 지시는 리턴
			string status = Convert.ToString(dr["Status"]);
			if (status == "종료")
			{
				SetMessage("START_FAIL", "이미 종료된 지시");
				return;
			}

			//2.시작 시 확인메세지
			if (DialogResult.OK != cls_MessageBox.Show("작업을 시작하시겠습니까?", "작업시작", MessageBoxButtons.OKCancel))
			{
				return;
			}

			//작업지시 상태 업데이트
			PM_OrderProcess op = new PM_OrderProcess();
			op.CorpId = WrGlobal.CorpID;
			op.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
			op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
			op.Status = "작업중";

			if(!op.Upd_Status())
			{
				cls_MessageBox.Show("작업지시 상태 업데이트 중 오류", "오류");
				return;
			}

			//4. 실적등록 화면으로 전환
			Send_Data(dr);
		}

		private void btn_End_Click(object sender, EventArgs e)
		{
			DataRow dr = gv_ProcessCmd.GetFocusedDataRow();

			string status = Convert.ToString(dr["Status"]);

			//대기, 완료 지시는 리턴
			if (status != "작업중") //"20"
			{
				SetMessage("END_FAIL", "시작되지 않거나 이미 종료된 지시");
				return;
			}

			//지시상태 작업완료로 업데잍
			PM_OrderProcess op = new PM_OrderProcess();
			op.OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
			op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
			op.Status = "작업완료";

			if (!op.Upd_Status())
			{
				SetMessage("END", "작업 종료 완료");
				return;
			}

			Search();
		}
		#endregion 작업상태end

		#region message
		private void SetMessage(string ID, string message)
		{
			string Caption = "";

			switch (ID)
			{
				case "START":
					Caption = "시작 완료";
					break;

				case "START_FAIL":
					Caption = "시작 실패";
					break;

				case "END":
					Caption = "종료 완료";
					break;

				case "END_FAIL":
					Caption = "종료 실패";
					break;

				case "":
					Caption = "";
					break;
			}

			if (DialogResult.OK == cls_MessageBox.Show(message, Caption))
			{
				//this.Close();
			}

		}

		#endregion message

		private void Opensheet(string item_Cd)
		{
			if (item_Cd == null || item_Cd == "") return;
			/*
			 * 프로세스 옵션
			 * 제품 사양서 조회(I: 이미지, P: PDF)
			 */

			if (WrGlobal.ProcOption["BS"]["Work_Standard_Sheed_IP"] == "P")
			{
				//FrmPDFViewer frm = new FrmPDFViewer(item_Cd, false);
				//frm.ShowDialog();
			}
			else
			{
				//제품사양서 조회
				Frm_PictureEdit frmPictureEdit = new Frm_PictureEdit(item_Cd, "Work_Standard_Sheet", false);
				frmPictureEdit.ShowDialog();
			}
		}

		private void End()
		{
			this.Close();
		}

		private void pageButton_Click(object sender, EventArgs e)
		{
			SimpleButton btn = sender as SimpleButton;

			GridView gv = gc_ProcessCmd.MainView as GridView;

			switch (btn.Name)
			{
				case "uppage": gv.MovePrevPage(); break;
				case "up": gv.MovePrev(); break;
				case "downpage": gv.MoveNextPage(); break;
				case "down": gv.MoveNext(); break;
			}
		}
	}
}