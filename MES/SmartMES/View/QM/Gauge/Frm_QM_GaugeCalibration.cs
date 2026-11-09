using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.QM;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using static SmartCommon.Common.clsGrid;

namespace SmartMES.View.QM.Gauge
{
	public partial class Frm_QM_GaugeCalibration : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();
        string CommonFileClassItemCode = "Z08050"; // 품질관리 계측기 교정 성적서

        public Frm_QM_GaugeCalibration()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 데이터 입력
            Set_GvGauge(Gv_Gauge);
            Set_GvGauge_Cal(Gv_GaugeCal);

            Dt_Date.Visible = false;

            //이미지
            pictureEdit1.Properties.ExportMode = ExportMode.Value;
            pictureEdit1.Properties.PictureStoreMode = PictureStoreMode.ByteArray;
            pictureEdit1.Properties.SizeMode = PictureSizeMode.Zoom; //이미지크기 컨테이너에 맞춤(잘림방지)
            pictureEdit1.Properties.ContextMenuStrip = new ContextMenuStrip();
            //pictureEdit1.Properties.OpenFileDialogFilter = "Image Files(*.PNG; *.BMP; *.JPG; *.GIF)| *.PNG; *.BMP; *.JPG; *.GIF";

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

        private void Set_GvGauge(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "계측기코드", "GaugeCode");
            grid.Create_GvColumn(gv, "계측기명", "GaugeName");
            grid.Create_GvColumn(gv, "관리번호", "SerialNo");
            grid.Create_GvColumn(gv, "교정주기(일)", "CalCycleDay");
            grid.Create_GvColumn(gv, "최근교정일", "LastCalDate");
            grid.Create_GvColumn(gv, "차기교정일", "NextCalDate");

            grid.Create_GvColumn(gv, "제조사", "Maker");
            grid.Create_GvColumn(gv, "구입일자", "PurchaseDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "이미지", "GaugeImage");

            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");

            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //VISIBLE
            gv.Columns["PurchaseDate"].Visible = false; 
            gv.Columns["CreId"].Visible = false; 
            gv.Columns["CreIP"].Visible = false; 
            gv.Columns["CreDt"].Visible = false; 
            gv.Columns["ModId"].Visible = false; 
            gv.Columns["ModIP"].Visible = false; 
            gv.Columns["ModDt"].Visible = false; 

            //Repository 설정

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["LastCalDate"], TypeList.TDate);
            grid.SetCol_Type(gv.Columns["NextCalDate"], TypeList.TDate);
            grid.SetCol_Type(gv.Columns["PurchaseDate"], TypeList.TDate);
            grid.SetCol_Type(gv.Columns["CalCycleDay"], TypeList.TInt);

            //칼럼 너비 설정

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
            //gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            //gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            //gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            //gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            //gv.CustomRowCellEditForEditing += Gv_CustomRowCellEditForEditing;

        }

        private void Set_GvGauge_Cal(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            RepositoryItemSearchLookUpEdit lup_Result = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "Q020", false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "계측기코드", "GaugeCode");
            grid.Create_GvColumn(gv, "계측기명", "GaugeName");
            grid.Create_GvColumn(gv, "교정일자", "CalibrationDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "차기교정일", "NextDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "교정업체", "CalibrationCompany");
            grid.Create_GvColumn(gv, "교정금액", "Cost");
            grid.Create_GvColumn(gv, "교정결과", "Result", lup_Result);
            grid.Create_GvColumn(gv, "이미지등록", "CalImage", rpo.Create_rpoPic());
			grid.Create_GvColumn(gv, "비고", "Remark", rpo.Create_rpoMemo());
			grid.Create_GvColumn(gv, "사용여부", "UseYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "성적서여부", "CfYn");

            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");

            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //VISIBLE
            gv.Columns["GaugeCode"].Visible = false; 
			gv.Columns["GaugeName"].Visible = false;
            gv.Columns["CalImage"].Visible = false;

            //EDIT
            grid.Column_AllowEdit(gv.Columns["CalibrationDate"]);
            grid.Column_AllowEdit(gv.Columns["CalibrationCompany"]);
            grid.Column_AllowEdit(gv.Columns["Cost"]);
            grid.Column_AllowEdit(gv.Columns["CalImage"]);
            grid.Column_AllowEdit(gv.Columns["Result"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            //Repository 설정

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["CfYn"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			grid.SetCol_Type(gv.Columns["CalibrationDate"], TypeList.TDate);
			grid.SetCol_Type(gv.Columns["NextDate"], TypeList.TDate);
            grid.SetCol_Type(gv.Columns["Cost"], TypeList.TFloat);

            //칼럼 너비 설정

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
			gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
			gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
			gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
			gv.CustomRowCellEditForEditing += Gv_CustomRowCellEditForEditing;
			//gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged1;

		}

		private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
		{
            string GaugeCode = Gv_Gauge.GetFocusedRowCellValue("GaugeCode").ConvertString_Null_To_Empty();

            ////이미지 조회
            Fun_Search_Image(GaugeCode);

            Search_Cal();
        }

		private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_RowEdit(cmdInsert,null); break;   //행삽입
                //case (Keys.Alt | Keys.A): Btn_MachineRow_Add.PerformClick(); break;   //행추가
                case (Keys.Alt | Keys.D): Btn_RowEdit(cmdDelete, null); break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            smtSearchButton btn = sender as smtSearchButton;
            string txt_Act = btn.Tag.ConvertString_Null_To_Empty();

            //
            DataRow dr = Gv_Gauge.GetFocusedDataRow();
            if (dr == null) return;

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("GaugeCode", dr["GaugeCode"]);
                dic.Add("UseYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_Gauge.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_GaugeCal, dic, txt_Act);
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

            if (e.Column.FieldName == "CalibrationDate")
            {
                DateTime calibrationDate;

                if (DateTime.TryParse(
                    Convert.ToString(drChanged["CalibrationDate"]),
                    out calibrationDate))
                {
                    DataRow drGauge = Gv_Gauge.GetFocusedDataRow();

                    if (drGauge != null)
                    {
                        int calCycleDay =
                            drGauge["CalCycleDay"].ConvertInt_Null_To_Zero();

                        if (calCycleDay > 0)
                        {
                            drChanged["NextDate"] =
                                calibrationDate.AddDays(calCycleDay);
                        }
                        else
                        {
                            drChanged["NextDate"] = DBNull.Value;
                        }
                    }
                }
            }


            gv.BestFitColumns();
         }
    
        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            grid.Check_OverLap_CellValue(sender as GridView, e, "CalibrationDate");
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_QM_GaugeCalibration_Load(object sender, EventArgs e)
        {
            Search();
        }

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

                case "cmdUpload":
                    Upload();
                    break;

                case "cmdDownload":
                    Download();
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

        #endregion Event End

        #region 조회 기능

        private void Search()
        {
            QM_Gauge Gauge = new QM_Gauge();
            Gc_Gauge.DataSource = Gauge.Select_QM_Gauge();

			Gv_Gauge.BestFitColumns();   //Column 너비 조정
        }

        private void Search_Cal()
		{
            //1. 조회 검사
            if (!Check_Search()) return;

            DataRow dr = Gv_Gauge.GetFocusedDataRow();
            
            if(dr == null)
			{
                Gc_GaugeCal.DataSource = null;
                return;
			}

            QM_GaugeCalibration ms = new QM_GaugeCalibration();
            ms.GaugeCode = dr["GaugeCode"].ConvertString_Null_To_Empty();

            Gc_GaugeCal.DataSource = ms.Select_QM_GaugeCalibration("","");

            Gv_GaugeCal.BestFitColumns();   //Column 너비 조정
        }

        private void Fun_Search_Image(string GaugeCode)
        {
            QM_Gauge Gauge = new QM_Gauge();
            Gauge.GaugeCode = GaugeCode;

            DataRow drImg = Gauge.Select_Gauge_Image();

            pictureEdit1.EditValue = drImg?["GaugeImage"];
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

        #endregion 조회 기능 Endz

        #region 저장 기능

        private void Save()
        {
            Gv_GaugeCal.FocusInvalidRow();
            Gv_GaugeCal.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = Gc_GaugeCal.DataSource as DataTable;

            DataRow drGauge = Gv_Gauge.GetFocusedDataRow();
            if (drGauge == null) return;

            int SaveFocusedRowInx = Gv_Gauge.FocusedRowHandle;
            string gaugeCode = drGauge["GaugeCode"].ConvertString_Null_To_Empty();

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

            //계측기 테이블 교정일자 업데이트
            Update_GaugeCalibrationDate(gaugeCode);

            //이미지저장
            //PictureSave();

            //선택한 행 번호 저장

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
                    if(Convert.ToString(dr["GaugeCode"]) == "")
					{
                        XtraMessageBox.Show("계측기명이 비었습니다.");
                        return false;
					}
                    else if (Convert.ToString(dr["CalibrationDate"]) == "")
                    {
                        XtraMessageBox.Show("검교정일자가 비었습니다.");
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
            //1. 기본 셋팅
            QM_GaugeCalibration cal = new QM_GaugeCalibration();
            cal.CalibrationNo = Convert.ToString(dr["CalibrationNo"]);
            cal.GaugeCode = Convert.ToString(dr["GaugeCode"]);
            cal.CalibrationDate = DateTime.TryParse(Convert.ToString(dr["CalibrationDate"]), out DateTime date)? dr["CalibrationDate"].ConvertDate_To_Char10() : "";
            cal.CalibrationCompany = Convert.ToString(dr["CalibrationCompany"]);
            cal.Result = Convert.ToString(dr["Result"]);

            cal.NextDate = DateTime.TryParse(Convert.ToString(dr["NextDate"]), out date)? dr["NextDate"].ConvertDate_To_Char10() : "";

            cal.Cost = dr["Cost"].ConvertDecimal_Null_To_Zero();
            cal.UseYn = Convert.ToString(dr["UseYn"]);
            cal.Remark = Convert.ToString(dr["Remark"]);
			//ms.CalImage = dr["CalImage"] as byte[];

			//2. DB 적용
			if (div == 'I')
            {
                //No 따기
                cal.CalibrationNo = "CB" + code.GetAutoNumber("계측기교정", Convert.ToDateTime(cal.CalibrationDate));

                //Insert
                if (!cal.Ins_QM_GaugeCalibration())
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
                if (!cal.Upd_QM_GaugeCalibration())
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
                if (!cal.Del_QM_GaugeCalibration())
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

		private bool Update_GaugeCalibrationDate(string gaugeCode)
        {
            QM_GaugeCalibration cal = new QM_GaugeCalibration();
            cal.GaugeCode = gaugeCode;

            DataRow dr = cal.Select_LatestCalibration();

            QM_Gauge gauge = new QM_Gauge();
            gauge.GaugeCode = gaugeCode;

            if (dr == null)
            {
                // 검교정 이력이 하나도 없는 경우
                gauge.LastCalDate = "";
                gauge.NextCalDate = "";
            }
            else
            {
                gauge.LastCalDate =
                    dr["CalibrationDate"].ConvertDate_To_Char10();

                gauge.NextCalDate =
                    DateTime.TryParse(Convert.ToString(dr["NextDate"]), out DateTime nextDate)
                        ? nextDate.ToString("yyyy-MM-dd")
                        : "";
            }

            return gauge.Upd_CalibrationDate();
        }

        #endregion 저장 기능 End

        private void Upload()
        {

            const string tableName = "CM_CommonFile";

            // 1. 계측기 선택 확인
            DataRow drGauge = Gv_Gauge.GetFocusedDataRow();

            if (drGauge == null)
            {
                XtraMessageBox.Show("선택된 계측기가 없습니다.");
                return;
            }

            string gaugeCode = drGauge["GaugeCode"].ConvertString_Null_To_Empty();

            if (gaugeCode == "")
            {
                XtraMessageBox.Show("계측기코드가 없습니다.");
                return;
            }

            // 2. 파일 선택
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "교정 성적서 선택";
                ofd.Filter = "모든 파일 (*.*)|*.*";

                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                string localFilePath = Path.GetDirectoryName(ofd.FileName);
                string fileName = Path.GetFileName(ofd.FileName);
                string title = Path.GetFileNameWithoutExtension(ofd.FileName);

                // 3. 교정 기본정보 생성
                DateTime calibrationDate = DateTime.Now;

                string calibrationNo =
                    "CB" + code.GetAutoNumber("계측기교정", calibrationDate);

                int calCycleDay =
                    drGauge["CalCycleDay"].ConvertInt_Null_To_Zero();

                DateTime? nextDate = null;

                if (calCycleDay > 0)
                {
                    nextDate = calibrationDate.AddDays(calCycleDay);
                }

                // 4. FTP 업로드
                string ftpPath =
                    WrGlobal.m_ftpServerip + "/" + WrGlobal.m_ftpFolderPath;

                cls_FTP clsFtp = new cls_FTP();

                FileInfo file =
                    clsFtp.UploadFile(
                        ftpPath,
                        localFilePath,
                        fileName,
                        tableName);

                if (file == null)
                {
                    XtraMessageBox.Show("파일 업로드 중 오류가 발생하였습니다.");
                    return;
                }

                // 5. 공통파일 등록
                CM_CommonFile doc = new CM_CommonFile();

                doc.CorpId = WrGlobal.CorpID;
                doc.ClassItemCode = CommonFileClassItemCode;
                doc.FileSeq = doc.GetNextSeq();
                doc.Title = title;
                doc.OriFileName = fileName;
                doc.FileName = fileName;
                doc.Path = "/" + WrGlobal.m_ftpFolderPath + "/" + tableName + "/";
                doc.Capacity = file.Length;
                doc.FileExt = file.Extension;
                doc.RefNo = calibrationNo;

                if (!doc.Ins_CM_CommonFile())
                {
                    XtraMessageBox.Show("성적서 파일정보 저장 중 오류가 발생하였습니다.");
                    return;
                }

                // 6. 계측기 교정이력 등록
                QM_GaugeCalibration cal = new QM_GaugeCalibration();

                cal.CalibrationNo = calibrationNo;
                cal.GaugeCode = gaugeCode;
                cal.CalibrationDate = calibrationDate.ToString("yyyy-MM-dd");

                cal.CertificateFileID =
                    doc.FileSeq.ConvertInt_Empty_To_Null();

                cal.Result = "10"; // 합격

                cal.NextDate =
                    nextDate.HasValue
                        ? nextDate.Value.ToString("yyyy-MM-dd")
                        : "";

                cal.UseYn = "Y";

                if (!cal.Ins_QM_GaugeCalibration())
                {
                    XtraMessageBox.Show("계측기 교정이력 저장 중 오류가 발생하였습니다.");
                    return;
                }

                // 7. 계측기 최신 교정일 / 차기교정일 갱신
                if (!Update_GaugeCalibrationDate(gaugeCode))
                {
                    XtraMessageBox.Show("계측기 교정일자 갱신 중 오류가 발생하였습니다.");
                    return;
                }

                // 8. 재조회
                Search();

                XtraMessageBox.Show("교정 성적서가 정상적으로 등록되었습니다.");
            }
        }

        private void Download()
        {
            DataRow drCal = Gv_GaugeCal.GetFocusedDataRow();
            if (drCal == null) return;

			if (drCal["CertificateFileID"].ConvertString_Null_To_Empty() == "")
			{
                XtraMessageBox.Show("등록된 성적서 없음");
				return;
			}

			FolderBrowserDialog fb = new FolderBrowserDialog();

            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CM_CommonFile cf = new CM_CommonFile();
                cf.ClassItemCode = this.CommonFileClassItemCode;
                cf.RefNo = drCal["CalibrationNo"].ConvertString_Null_To_Empty();

                DataTable dt = cf.Select_CM_CommonFile();

                if(dt != null && dt.Rows.Count > 0)
				{
                    DataRow dr = dt.Rows[0];

                    List<string> selPaths = new List<string>();
                    List<string> selOriFiles = new List<string>();
                    List<string> selFiles = new List<string>();
                    long totalSize = 0;

                    selPaths.Add(dr["Path"].ToString());
                    selOriFiles.Add(dr["OriFileName"].ToString());
                    selFiles.Add(dr["FileName"].ToString());
                    totalSize += Convert.ToInt64(dr["Capacity"]);

                    FtpFileDownloader downloader = new FtpFileDownloader(fb.SelectedPath, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
                    downloader.ShowDialog();
                }
            }
        }
    }
}