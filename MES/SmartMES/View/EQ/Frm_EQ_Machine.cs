using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartCommon.View.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.EQ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
    /// <summary>
    /// 설비정보
    /// </summary>
    public partial class Frm_EQ_Machine : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsMachine machine = new clsMachine();

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        string query = "";
        Database db = new Database();
        public Frm_EQ_Machine()
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            SetDesign();

            DisplayMachine();

            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정
        }

        private void SetDesign()
        {
			//GridView 데이터 입력
			Set_GvMachine(gvEQ_Machine);
			Set_GvMachineImage(gvEQ_MachineImage);
			Set_Lup();
		}

		private void Set_Lup()
		{
			ssMGroup.smtLookUpDataSource = machine.GetClassView("2010", "2010", "2", true).ToTable();
			ssMGroup.smtLookUpDataDisplayMember = "명칭";
			ssMGroup.smtLookUpDataValueMember = "코드";
		}

        /// <summary>설비 상세 항목은 기존 그리드에 연결하고, 원본 이관 정보는 조회만 허용합니다.</summary>
        private void SetDetailColumns()
        {
            AddDetailColumn("ProductionPlantName", "생산공장명", null, true);
            AddDetailColumn("ProductionLineName", "생산라인명", null, true);
            AddDetailColumn("InstallDate", "설치일자", rpo.Create_rpoDate(), true);
            AddDetailColumn("ManufactureDate", "제작일자", rpo.Create_rpoDate(), true);
            AddDetailColumn("UsePlantName", "사용공장명", null, true);
            AddDetailColumn("MaintenanceContactName", "관리업체담당자", null, true);
            AddDetailColumn("MaintenanceContactTel", "관리업체연락처", null, true);
            AddDetailColumn("InspectionDepartmentName", "점검관리부서명", null, true);
            AddDetailColumn("InspectionStartDate", "점검시작일", rpo.Create_rpoDate(), true);
            AddDetailColumn("InspectionEndDate", "점검종료일", rpo.Create_rpoDate(), true);
            AddDetailColumn("IsMeasuringEquipment", "계측설비 여부", new RepositoryItemCheckEdit { AllowGrayed = true, ValueChecked = true, ValueUnchecked = false, ValueGrayed = DBNull.Value }, true);
            AddDetailColumn("MeasuringInstrumentNo", "계측기번호", null, true);
            AddDetailColumn("PowerSupplyType", "전원구분", null, true);
            AddDetailColumn("PowerConsumptionKw", "사용전력(kW)", new RepositoryItemCalcEdit { Precision = 4 }, true);
            AddDetailColumn("AgingGrade", "노후화등급", null, true);
            AddDetailColumn("LegacyMachineCode", "구 설비번호", null, true);
            AddDetailColumn("SourceRegisteredAt", "원본 자료 등록일시", rpo.Create_rpoDate(), false);
            AddDetailColumn("EquipmentNumber", "원본 설비번호", null, true);
            AddDetailColumn("RegisteredMachineName", "원본 상세 설비명", null, false);
            AddDetailColumn("OriginalMachineName", "대표 설비명 변경 전 이름", null, false);
            gvEQ_Machine.OptionsView.ShowAutoFilterRow = true;
            gvEQ_Machine.Columns["MachineCode"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvEQ_Machine.Columns["MachineName"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
        }

        private void AddDetailColumn(string field, string caption, RepositoryItem editor = null, bool editable = true)
        {
            grid.Create_GvColumn(gvEQ_Machine, caption, field);
            var column = gvEQ_Machine.Columns[field];
            column.Width = field.EndsWith("Name") ? 180 : 130;
            grid.Column_AllowEdit(column, editable);
            column.OptionsColumn.ReadOnly = !editable;
            if (editor != null)
            {
                gcEQ_Machine.RepositoryItems.Add(editor);
                column.ColumnEdit = editor;
            }
            if (editor is RepositoryItemDateEdit)
            {
                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                column.DisplayFormat.FormatString = field == "SourceRegisteredAt" ? "yyyy-MM-dd HH:mm:ss" : "yyyy-MM-dd";
            }
            else if (editor is RepositoryItemCalcEdit)
            {
                column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                column.DisplayFormat.FormatString = "#,##0.####";
            }
        }

        private void Set_GvMachine(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            RepositoryItemLookUpEdit lupMGroup = rpo.Create_RepositoryItem(machine.GetClassView("2010", "2010", "2", false).ToTable(), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            if (!Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase))
                grid.Create_GvColumn(gv, "설비분류", "MGroup", lupMGroup);
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            if (!Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase))
                grid.Create_GvColumn(gv, "관리번호", "ModelNo");

            //올덴, TPG 컬럼 추가
            if (Database.DbName.ToUpper().Contains("ALLTHEN"))
            {
                grid.Create_GvColumn(gv, "열", "ArrayValue");
                grid.Create_GvColumn(gv, "RPM", "RPM");
                grid.Create_GvColumn(gv, "수율", "ProductionYield");

                grid.Column_AllowEdit(gv.Columns["ArrayValue"]);
                grid.Column_AllowEdit(gv.Columns["RPM"]);
                grid.Column_AllowEdit(gv.Columns["ProductionYield"]);
            }
            if (Database.DbName.ToUpper().Contains("TPG"))
            {
                grid.Create_GvColumn(gv, "배열", "ArrayValue");
                grid.Create_GvColumn(gv, "RPM", "RPM");
                grid.Create_GvColumn(gv, "수율", "ProductionYield");

                grid.Column_AllowEdit(gv.Columns["ArrayValue"]);
                grid.Column_AllowEdit(gv.Columns["RPM"]);
                grid.Column_AllowEdit(gv.Columns["ProductionYield"]);

                grid.SetCol_Type(gv.Columns["ProductionYield"], clsGrid.TypeList.TFloat);
            }

            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "사용유무", "UseYn", rpo.Create_rpoChk());
            if (!Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase))
                grid.Create_GvColumn(gv, "규격", "Size");
            grid.Create_GvColumn(gv, "약식명", "AliasName");
            if (!Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase))
                grid.Create_GvColumn(gv, "시리얼번호", "SerialNo");
            if (!Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase))
                grid.Create_GvColumn(gv, "메이커", "Maker");

            grid.Create_GvColumn(gv, "제조회사", "Prodution");
            if (!Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase))
                grid.Create_GvColumn(gv, "제조일자", "CreateYYMM", rpo.Create_rpoDate());

            if (Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase))
                SetDetailColumns();

            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //자동채번이 아닐 경우 코드값 편집 허용
            if (WrGlobal.ProcOption["BS"]["Machine_AutoNo"] != "Y")
            {
                grid.Column_AllowEdit(gv.Columns["MachineCode"]);
            }

            grid.Column_AllowEdit(gv.Columns["MGroup"]);
            grid.Column_AllowEdit(gv.Columns["MachineName"]);
            grid.Column_AllowEdit(gv.Columns["Size"]);

            grid.Column_AllowEdit(gv.Columns["AliasName"]);
            grid.Column_AllowEdit(gv.Columns["SerialNo"]);
            grid.Column_AllowEdit(gv.Columns["Maker"]);
            grid.Column_AllowEdit(gv.Columns["ModelNo"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["Prodution"]);
            grid.Column_AllowEdit(gv.Columns["CreateYYMM"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //칼럼 너비 설정
            gv.Columns["Status"].MaxWidth = 60;

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey_machine;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            gv.FocusedRowObjectChanged += FocusedRowObjectChanged;   //행 변경시 해당거래처의 납품처리스트조회
        }

		private void FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            DataRow dr = gv.GetFocusedDataRow();

            if(dr == null)
			{
                gcEQ_MachineImage.DataSource = null;
                pictureEdit1.EditValue = null;
                return;
			}

            DisplayImg();
        }

        private void Event_ProcessGridKey_machine(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): cmdIns.PerformClick(); break;   //행삽입
                //case (Keys.Alt | Keys.A): Btn_MachineRow_Add.PerformClick(); break;   //행추가
                case (Keys.Alt | Keys.D): cmdDel.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Tag.ToString();

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
                dic.Add("Status", Convert.ToString(gvEQ_Machine.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(gcEQ_Machine, dic, txt_Act);

            gvEQ_Machine.BestFitColumns();   //Column 너비 조정
        }

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            clsGrid grid = new clsGrid();
            grid.Check_OverLap_CellValue(sender as GridView, e, "MachineCode");
        }

        private void Event_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			GridView gv = sender as GridView;
			DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
			string fielName = e.Column.FieldName;
			string status = drChanged["Status"].ConvertString_Null_To_Empty();

			//셀값 수정 시 상태값 수정
			if (e.Column.FieldName == gv.Columns[0].FieldName) return;
			grid.Change_Status(gv, e.RowHandle);
		}

        private void Set_GvMachineImage(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			gv.InitNewRow += Gv_InitNewRow;
			gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;
			gv.CellValueChanged += GvImage_CellValueChanged;
        }

		private void GvImage_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
            //GridView gv = sender as GridView;
            //DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
            //string fielName = e.Column.FieldName;
            //string status = drChanged["Status"].ConvertString_Null_To_Empty();

            ////셀값 수정 시 상태값 수정
            //if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            //grid.Change_Status(gv, e.RowHandle);
        }

		private void Gv_FocusedRowObjectChanged(object sender, FocusedRowObjectChangedEventArgs e)
		{
            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);

            if (dr == null)
            {
                pictureEdit1.EditValue = null;
                return;
            }

            pictureEdit1.EditValue = dr["Image"];
		}

		private void Gv_InitNewRow(object sender, InitNewRowEventArgs e)
		{
            GridView gv = sender as GridView;
            DataTable dt = gcEQ_MachineImage.DataSource as DataTable;

            DataRow drMC = gvEQ_Machine.GetFocusedDataRow();
            if (drMC == null) return;
            if (drMC["Status"].ConvertString_Null_To_Empty() == "신규")
            {
                XtraMessageBox.Show("설비 등록 후 추가할 수 있습니다");
                gv.DeleteRow(e.RowHandle);
                return;
            }


            //최대순번
            int seq = 0;
            foreach (DataRow tmpItem in dt.Rows)
            {
                if (tmpItem["Seq"] != DBNull.Value)
                {
                    if (seq < (int)tmpItem["Seq"]) seq = (int)tmpItem["Seq"];
                }
            }
            gv.SetRowCellValue(e.RowHandle, "Status", "신규");
            gv.SetRowCellValue(e.RowHandle, "Seq", seq+1);
            gv.SetRowCellValue(e.RowHandle, "MachineCode", drMC["MachineCode"]);
		}

		private void ssProcess_smtLookUpEditValueChanged(object sender, EventArgs e)
        {
            DisplayMachine();
        }

        private void ssUseYn_smtComboBoxTextChanged(object sender, EventArgs e)
        {
            DisplayMachine();
        }

        private void DisplayMachine()
        {
            this.Validate();

            EQ_Machine cm = new EQ_Machine();
            cm.CorpId = WrGlobal.CorpID;
            cm.MGroup = ssMGroup.smtLookUpValue == "ALL" ? "" : ssMGroup.smtLookUpValue;
            cm.UseYn = ssUseYn.smtComboBoxValue == "전체" ? "" : ssUseYn.smtComboBoxValue;

            if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
            {
                gcEQ_Machine.DataSource = cm.Select_Machine();
            }
            else
            {
                gcEQ_Machine.DataSource = cm.SelectMachine();
            }
            gvEQ_Machine.BestFitColumns();
        }

        private void DisplayImg()
		{
            DataRow dr = gvEQ_Machine.GetFocusedDataRow();

            if (dr == null)
            {
                gcEQ_MachineImage.DataSource = null;
                pictureEdit1.EditValue = null;
                return;
            }

            EQ_MachineImage ci = new EQ_MachineImage();
            ci.CorpId = WrGlobal.CorpID;
            ci.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();

            DataTable dtImage = ci.SelectImageByMachineCode();

            gcEQ_MachineImage.DataSource = dtImage;

            if (dtImage == null)
            {
                pictureEdit1.EditValue = null;
                return;
            }

            if (dtImage.Rows.Count < 1) pictureEdit1.EditValue = null;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;

			switch (btn.Name)
			{
                case "cmdIns": Btn_RowEdit(btn, e); break;
                case "cmdDel": Btn_RowEdit(btn, e); break;
                case "cmdSave": Save(); break;
                case "cmdClose": this.Close(); break;
			}
        }

        private void Save()
		{
            this.Validate();
            gvEQ_Machine.CloseEditor();
            if (!gvEQ_Machine.UpdateCurrentRow()) return;

            //작성내역 저장
            DataTable dt = gcEQ_Machine.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase_Machine(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase_Machine(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase_Machine(dt.Rows[curHandle], 'D')) return;
                }
            }

			//선택한 행 번호 저장
			int SaveFocusedRowInx = gvEQ_Machine.FocusedRowHandle;

            //3. 정상절차 진행
            DisplayMachine();

            //저장 후 해당 행으로 focus이동
            gvEQ_Machine.FocusedRowHandle = SaveFocusedRowInx;

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

                if (status == "삭제")
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

        private bool Save_ToDatabase_Machine(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            EQ_Machine machine = new EQ_Machine();
            machine.CorpId = WrGlobal.CorpID;
            machine.MachineCode = Convert.ToString(dr["MachineCode"]);    //설비명

            if (div != 'D' && Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    if (div == 'I' && string.IsNullOrWhiteSpace(machine.MachineCode))
                        machine.MachineCode = machine.GetNewMachineCode();
                    machine.SaveDetails(dr, div == 'I');
                    dr["Status"] = "";
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "설비 저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (div != 'D')
            {
                machine.MGroup = Convert.ToString(dr["MGroup"]);    //설비명
                machine.MachineName = Convert.ToString(dr["MachineName"]);    //설비명
                machine.Size = Convert.ToString(dr["Size"]);    //설비명
                machine.AliasName = Convert.ToString(dr["AliasName"]);    //설비명
                machine.SerialNo = Convert.ToString(dr["SerialNo"]);    //설비명
                machine.Maker = Convert.ToString(dr["Maker"]);    //설비명
                machine.ModelNo = Convert.ToString(dr["ModelNo"]);    //설비명
                machine.Remark = Convert.ToString(dr["Remark"]);    //설비명
                machine.UseYn = Convert.ToString(dr["UseYn"]);    //설비명
                machine.Prodution = Convert.ToString(dr["Prodution"]);    //설비명
                machine.CreateYYMM = dr["CreateYYMM"].ConvertDate_To_Char10();    //설비명
                if (Database.DbName.ToUpper().Contains("ALLTHEN"))
                {
                    machine.ArrayValue = dr["ArrayValue"].ConvertInt_Null_To_Zero();
                    machine.RPM = dr["RPM"].ConvertInt_Null_To_Zero();
                }
                if (Database.DbName.ToUpper().Contains("TPG"))
                {
                    machine.ArrayValue = dr["ArrayValue"].ConvertInt_Null_To_Zero();
                    machine.RPM = dr["RPM"].ConvertInt_Null_To_Zero();
                    machine.ProductionYield = dr["ProductionYield"].ConvertFloat_Null_To_Zero();
                }
            }

            //2. DB 적용
            if (div == 'I')
            {
                //신규 설비 코드 채번
                if(machine.MachineCode == "")
				{
                    machine.MachineCode = machine.GetNewMachineCode();
				}

                //Insert
                if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
                {
                    if (!machine.InsertMachine())
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
                }
                else
                {
                    if (!machine.Ins_Machine())
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
                }
                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'U')
            {
                //Update
                if (Database.DbName.ToUpper().Contains("ALLTHEN") || Database.DbName.ToUpper().Contains("TPG"))
                {
                    if (!machine.UpdateMachine())
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
                }
                else
                {
                    if (!machine.Upd_Machine())
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
                }
                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'D')
            {
                EQ_MachineImage img = new EQ_MachineImage();
                img.CorpId = WrGlobal.CorpID;
                img.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();
                img.Del_ImageBy_MachineCd();

                //Delete
                if (!machine.Del_Machine())
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

        #region 이미지

        /*        private void SaveImage()
        {
            DataTable dt = gcEQ_MachineImage.DataSource as DataTable;
            if (dt == null) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase_Image(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase_Image(dt.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase_Image(dt.Rows[curHandle], 'D')) return;
                }
            }
        }
        private bool Save_ToDatabase_Image(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            EQ_MachineImage image = new EQ_MachineImage();
            image.CorpId = WrGlobal.CorpID;
            image.MachineCode = Convert.ToString(dr["MachineCode"]);    //설비명

            if (div != 'D')
            {
                image.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
                image.Title = Convert.ToString(dr["Title"]);    //설비명
                image.Image = dr["Image"] as byte[];    //설비명
            }

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!image.Ins_Image())
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
                if (!image.Upd_Image())
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
                if (!image.Del_Image())
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
*/

        //이미지 등록
        private void colImageSearchButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvEQ_MachineImage.IsNewItemRow(gvEQ_MachineImage.FocusedRowHandle)) return;

            this.Validate();

            // 2. 이미지 업로드 = FTP에 저장
            if (gvEQ_Machine.SelectedRowsCount < 0) return;
            try
            {
                string MachineCode = (gvEQ_Machine.GetFocusedRowCellValue("MachineCode") ?? "").ToString();
                int Seq = int.Parse((gvEQ_MachineImage.GetFocusedRowCellValue("Seq") ?? "1").ToString());
                string Title = (gvEQ_MachineImage.GetFocusedRowCellValue("Title") ?? "").ToString().Trim();

                foDialog.FileName = "";
                if (foDialog.ShowDialog() == DialogResult.OK)
                {
                    if (foDialog.FileNames.Length > 0)
                    {
                        if (Title != "")
                        {
                            foDialog.FileNames[0] = Title;
                        }

                        FtpFilesUploader uploader = new FtpFilesUploader("EQ_MachineImage", Seq, Title + "|" + MachineCode, foDialog.FileNames);
                        uploader.ShowDialog();

                        if (uploader.UploadSuccesss)
                        {
                            // DB 업로드 성공
                            MessageBox.Show("파일이 성공적으로 업로드되었습니다.", "업로드 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            pictureEdit1.Image = new Bitmap(foDialog.FileName);
                            pictureEdit1.Tag = foDialog.FileName;
                        }
                        else
                        {
                            // DB 업로드 실패
                            MessageBox.Show("파일 업로드에 실패하였습니다.", "업로드 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //이미지 다운로드
        private void colImageDownButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvEQ_MachineImage.IsNewItemRow(gvEQ_MachineImage.FocusedRowHandle)) return;

            if (gvEQ_MachineImage.SelectedRowsCount < 0) return;
            try
            {
                if (fbDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int[] idxs = gvEQ_MachineImage.GetSelectedRows();
                    bool hasDownloadedAny = false;

                    //long totalSize = 0;
                    foreach (int idx in idxs)
                    {
                        string corpId = gvEQ_MachineImage.GetRowCellValue(idx, "CorpId").ToString();
                        string MachineCode = gvEQ_MachineImage.GetRowCellValue(idx, "MachineCode").ToString();
                        string seq = gvEQ_MachineImage.GetRowCellValue(idx, "Seq").ToString();
                        query = $"SELECT Image FROM EQ_MachineImage WHERE CorpId = {corpId} AND MachineCode = '{MachineCode}' AND Seq = {seq}";
                        byte[] imageData = db.GetImageFromQuery(query);

                        if (imageData != null)
                        {
                            string savePath = Path.Combine(fbDialog.SelectedPath, $"{MachineCode}_{seq}.jpg");
                            File.WriteAllBytes(savePath, imageData);
                            hasDownloadedAny = true;
                        }
                    }
                    // 다운로드에 성공한 경우에만 메시지 표시
                    if (hasDownloadedAny)
                    {
                        MessageBox.Show("파일이 성공적으로 다운로드 되었습니다.", "다운로드 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("다운로드할 이미지가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //이미지 삭제
        private void colImageDelButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvEQ_MachineImage.IsNewItemRow(gvEQ_MachineImage.FocusedRowHandle)) return;

            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)
            {
                if (MessageBox.Show(cls_MessageBox.DelMsg_Select, cls_MessageBox.DelCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                try
                {
                    DataRow dr = gvEQ_MachineImage.GetFocusedDataRow();
                    if (dr == null) return;

                    EQ_MachineImage ci = new EQ_MachineImage();
                    ci.CorpId = WrGlobal.CorpID;
                    ci.MachineCode = dr["MachineCode"].ConvertString_Null_To_Empty();
                    ci.Seq = dr["Seq"].ConvertInt_Null_To_Zero();

                    if (ci.Del_Image())
                    {
                        DisplayImg();
                        MessageBox.Show(cls_MessageBox.Del_Comp, cls_MessageBox.DelCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(cls_MessageBox.Del_Fail, cls_MessageBox.DelCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion
    }
}