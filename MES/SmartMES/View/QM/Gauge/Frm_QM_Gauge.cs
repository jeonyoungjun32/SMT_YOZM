using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using SmartMES.View.Common;
using SMT_MES.View.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.QM.Gauge
{
	public partial class Frm_QM_Gauge : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        public Frm_QM_Gauge()
        {
            InitializeComponent();
        }

        private void Frm_QM_Gauge_Load(object sender, EventArgs e)
        {
            SetDesign();
            Search();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 데이터 입력
            Set_Gv(Gv_Gauge);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Gauge, "Status"))
                {
                    e.Cancel = true;
                }
            });
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

            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;

            RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
            Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Img.Buttons.RemoveAt(0);
            Btn_Img.Buttons.Add(Btn_Img_Move);
            Btn_Img.ButtonClick += Btn_Grid_Click;

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
			grid.Create_GvColumn(gv, "이미지", "GaugeImage", Btn_Img);
			grid.Create_GvColumn(gv, "사용여부", "UseYn", rpo.Create_rpoChk());

			grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
			grid.Create_GvColumn(gv, "등록IP", "CreIP");
			grid.Create_GvColumn(gv, "등록일시", "CreDt");

			grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
			grid.Create_GvColumn(gv, "수정IP", "ModIP");
			grid.Create_GvColumn(gv, "수정일시", "ModDt");

			////Column 단일 설정
			////VISIBLE
			//gv.Columns["Mearsuring_Image"].Visible = false; //추후 이미지 요청시 오픈

			grid.Column_AllowEdit(gv.Columns["GaugeName"]);
			grid.Column_AllowEdit(gv.Columns["SerialNo"]);
			grid.Column_AllowEdit(gv.Columns["Maker"]);
			grid.Column_AllowEdit(gv.Columns["PurchaseDate"]);
			grid.Column_AllowEdit(gv.Columns["CalCycleDay"]);
			//grid.Column_AllowEdit(gv.Columns["LastCalDate"]);
			//grid.Column_AllowEdit(gv.Columns["NextCalDate"]);
			grid.Column_AllowEdit(gv.Columns["Remark"]);
			grid.Column_AllowEdit(gv.Columns["GaugeImage"]);

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
			gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
			gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
			gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
																	  //gv.CustomRowCellEditForEditing += Gv_CustomRowCellEditForEditing;
		}

		private void Btn_Grid_Click(object sender, ButtonPressedEventArgs e)
		{
			//제품이미지, 제품사양서 등록

			ButtonEdit btn = sender as ButtonEdit;
			if (btn == null) return;

			GridView gv = Gc_Gauge.MainView as GridView;
			if (gv == null) return;

			DataRow drChanged = (Gc_Gauge.MainView as GridView).GetFocusedDataRow();
			string fieldName = gv.FocusedColumn.FieldName;

			string status = drChanged["Status"].ConvertString_Null_To_Empty();

			if (status == "신규")
			{
				XtraMessageBox.Show("계측기정보 선저장 필수");
				return;
			}

			string item_Cd = Convert.ToString(drChanged["GaugeCode"]);

			Frm_PictureEdit frm = new Frm_PictureEdit(item_Cd, fieldName);
            frm.FormClosed += FrmPictureEdit_FormClosed;
			frm.ShowDialog();
		}

        private void FrmPictureEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            //1. 선택한 row 저장
            int SaveFocusedRowInx = (Gc_Gauge.MainView as GridView).FocusedRowHandle;

            //2재조회
            Search();

            //3. 저장 후 선택한 row를 불러옴
            (Gc_Gauge.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_RowEdit_Machine(cmdInsert, null); break;   //행삽입
                //case (Keys.Alt | Keys.A): Btn_MachineRow_Add.PerformClick(); break;   //행추가
                case (Keys.Alt | Keys.D): Btn_RowEdit_Machine(cmdDelete, null); break; //행삭제
            }
        }

        private void Btn_RowEdit_Machine(object sender, EventArgs e)
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
                dic.Add("UseYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_Gauge.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_Gauge, dic, txt_Act);
        }

        private void Gv_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            //GridView gv = sender as GridView;


            ////Grid데이터가 없으면 스킵
            //if (e.RowHandle < 0) return;
            ////현재행 != 수정하려는 행일 시 스킵
            //if (e.RowHandle != gv.FocusedRowHandle) return;

            //cls_Repository rpo = new cls_Repository();
            //BS_ProcLine procLine = new BS_ProcLine();

            //string fieldName = e.Column.FieldName;

            //DataRow dr = gv.GetDataRow(e.RowHandle);

            //string line_Cd = dr["Line_Cd"].ConvertString_Null_To_Empty();

            //switch (fieldName)
            //{
            //    case "Process_Cd":
            //        if (line_Cd != "")
            //        {
            //            procLine.Line_Cd = line_Cd;
            //            DataTable dt_process = procLine.SearchLookUp_Select_ProcLine();

            //            e.RepositoryItem = rpo.Create_RepositoryItemSearchLookUp(dt_process, "코드", "명칭");
            //        }

            //        break;
            //}
        }

         private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
         {
            GridView gv = sender as GridView;
            DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
            string fielName = e.Column.FieldName;
            string LupValue = gv.GetRowCellValue(e.RowHandle, e.Column.FieldName).ConvertString_Null_To_Empty();
            string status = drChanged["Status"].ConvertString_Null_To_Empty();


            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
            
            //switch (fielName)
            //{
            //}

            gv.BestFitColumns();
         }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event


        #endregion Event End

        #region 조회 기능

        private void Search()
        {
			//1. 조회 검사
			if (!Check_Search()) return;

			//2. 조회
			QM_Gauge measuring = new QM_Gauge();
			Gc_Gauge.DataSource = measuring.Select_QM_Gauge();

			Gv_Gauge.BestFitColumns();   //Column 너비 조정
		}

        private bool Check_Search()
        {
            if ((Gv_Gauge.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
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

				case "cmdSave":
					Save();
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

		private void Save()
        {
            //작성내역 저장
            DataTable dt = Gc_Gauge.DataSource as DataTable;
            
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

            //이미지저장
            //PictureSave();

            //선택한 행 번호 저장
            int SaveFocusedRowInx = Gv_Gauge.FocusedRowHandle;

            //3. 정상절차 진행
            Search();

            //저장 후 해당 행으로 focus이동
            Gv_Gauge.FocusedRowHandle = SaveFocusedRowInx;

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }


        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            bool isRemoved = false;

            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                if(status == "신규" || status == "수정")
				{
                    if(Convert.ToString(dr["GaugeName"]) == "")
					{
                        XtraMessageBox.Show("계측기명이 비었습니다.");
                        return false;
					}

                    if (dr["CalCycleDay"].ConvertString_Null_To_Empty() == "")
                    {
                        XtraMessageBox.Show("검교정주기(일)이 비었습니다.");
                        return false;
                    }
                }
                else if (status == "삭제")
                {
                    isRemoved = true;
                }
            }

            //삭제내역 있을 경우

            if (isRemoved)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
			//데이터베이스에 반영
			DateTime date = new DateTime();

			if (div == 'I')
			{
				//신규 코드 따기
				Create_No(dr);
			}

			//1. 기본 셋팅
			QM_Gauge ms = new QM_Gauge();
			ms.GaugeCode = Convert.ToString(dr["GaugeCode"]);
			ms.GaugeName = Convert.ToString(dr["GaugeName"]);
			ms.GaugeType = Convert.ToString(dr["GaugeType"]);
			ms.SerialNo = Convert.ToString(dr["SerialNo"]);
			ms.Maker = Convert.ToString(dr["Maker"]);
			ms.CalCycleDay = dr["CalCycleDay"].ConvertInt_Null_To_Zero();
			ms.PurchaseDate = DateTime.TryParse(Convert.ToString(dr["PurchaseDate"]), out date) ? dr["PurchaseDate"].ConvertDate_To_Char8() : "";
			ms.LastCalDate = DateTime.TryParse(Convert.ToString(dr["LastCalDate"]), out date) ? dr["LastCalDate"].ConvertDate_To_Char8() : "";
			ms.NextCalDate = DateTime.TryParse(Convert.ToString(dr["NextCalDate"]), out date) ? dr["NextCalDate"].ConvertDate_To_Char8() : "";
			ms.Remark = Convert.ToString(dr["Remark"]);
			ms.UseYn = Convert.ToString(dr["UseYn"]);

			//2. DB 적용
			if (div == 'I')
			{
				//Insert
				if (!ms.Ins_QM_Gauge())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}
				else
				{
					//상태값 초기화
					dr["Status"] = "";
				}

				//추가 변경사항 private 따로 만들기
			}
			else if (div == 'U')
			{
				//Update
				if (!ms.Upd_QM_Gauge())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}
				else
				{
					//상태값 초기화
					dr["Status"] = "";
				}

				//추가 변경사항 private 따로 만들기
			}
			else if (div == 'D')
			{
				//Delete

				//검교정이력 삭제
				QM_GaugeCalibration mc = new QM_GaugeCalibration();
				mc.GaugeCode = ms.GaugeCode;
				mc.Del_QM_GaugeCalibration();

				if (!ms.Del_QM_Gauge())
				{
					//저장 중 에러 발생시
					MessageBox.Show("저장 중 에러가 발생하였습니다.");
					return false;
				}
				else
				{
					//상태값 초기화
					dr["Status"] = "";
				}

				//추가 변경사항 private 따로 만들기
			}

			//상태값 초기화
			dr[0] = "";

			return true;
		}

        private void Create_No(DataRow dr)
        {
			cls_Database cDB = new cls_Database();
			string initial = "GG";
            int No_Seq = cDB.Get_Last_Cd(WrGlobal.CorpID, initial, "QM_Gauge", "GaugeCode");

			dr["GaugeCode"] = initial + No_Seq.ToString("D4");
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