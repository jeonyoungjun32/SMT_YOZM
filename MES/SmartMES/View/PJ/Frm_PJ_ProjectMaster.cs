using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

using SmartCommon.Common;
using SmartCommon.Models;
using SmartCommon.View.Common;
using SmartMES.Models;
using SmartMES.View.PJ.Popup;

namespace SmartMES.View.PJ
{
	public partial class Frm_PJ_ProjectMaster : DevExpress.XtraEditors.XtraForm
    {
        string query = "";
        Database db = new Database();
        clsDocument doc = new clsDocument();
        clsGrid grid = new clsGrid();
        clsRepository repo = new clsRepository();
        ProjectMaster pm = new ProjectMaster();

        string projectType = "";
        private bool _isUpdating = false;

        RepositoryItemSearchLookUpEdit lup_Class;  // 프로젝트 분류 클래스 
        RepositoryItemSearchLookUpEdit lup_Material;  // 품명
        RepositoryItemDateEdit lup_Year;   // 년도
        RepositoryItemDateEdit lup_StartDate; // 시작일
        RepositoryItemDateEdit lup_EndDate;   // 종료일
        RepositoryItemDateEdit lup_PlanStartDate; //예상시작일
        RepositoryItemDateEdit lup_PlanEndDate;   //예상종료일
        RepositoryItemSearchLookUpEdit lup_Charge; // 담당자
        RepositoryItemSearchLookUpEdit lup_PM;     // PM
        RepositoryItemSearchLookUpEdit lup_Season; // 시즌
        private bool hasUnsavedNewRow;


        public Frm_PJ_ProjectMaster()
        {
            InitializeComponent();
        }

        public Frm_PJ_ProjectMaster(string _projectType)
        {
            InitializeComponent();

            projectType = _projectType;
        }

        private void frmDesignProject_Load(object sender, EventArgs e)
        {
            // 1. 날짜 설정 
            Dt_Date.smtStartDateTimeValue = DateTime.Now;

            // 2. RepositoryItemSearchLookUpEdit 세팅
            Set_SearchLup();

            // 3. Grid 세팅
            Set_ProjectMaster(gvProjectMaster);

            // 조회
            DisplayData();
        }


        #region 디자인 설정
        private void Set_SearchLup()
        {
            // 프로젝트 분류
            ProjectRepository projectRepo = new ProjectRepository();    
            materialSearchLookUpEidit.Properties.DataSource = projectRepo.Select_Material(new[] {"10","20"}); //제품
            lup_Material = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_Material(new[] { "10", "20" }), "품목코드", "품명");

            materialSearchLookUpEidit.Properties.ValueMember = "품목코드";
            materialSearchLookUpEidit.Properties.DisplayMember = "품명";

            lup_StartDate = new RepositoryItemDateEdit();
            lup_StartDate.Mask.EditMask = "yyyy-MM-dd";
            lup_StartDate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            lup_StartDate.UseMaskAsDisplayFormat = true;

            lup_EndDate = new RepositoryItemDateEdit();
            lup_EndDate.Mask.EditMask = "yyyy-MM-dd";
            lup_EndDate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            lup_EndDate.UseMaskAsDisplayFormat = true;

            lup_PlanStartDate = new RepositoryItemDateEdit();
            lup_PlanStartDate.Mask.EditMask = "yyyy-MM-dd";
            lup_PlanStartDate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            lup_PlanStartDate.UseMaskAsDisplayFormat = true;

            lup_PlanEndDate = new RepositoryItemDateEdit();
            lup_PlanEndDate.Mask.EditMask = "yyyy-MM-dd";
            lup_PlanEndDate.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            lup_PlanEndDate.UseMaskAsDisplayFormat = true;

            // PM, 담당자Charge 추가
            lup_PM = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_PM_Charge(), "직원코드", "직원명");
            pMNameSearchLookUpEidit.Properties.DataSource = projectRepo.Select_PM_Charge();
            pMNameSearchLookUpEidit.Properties.ValueMember = "직원코드";
            pMNameSearchLookUpEidit.Properties.DisplayMember = "직원명";

            lup_Charge = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_PM_Charge(), "직원코드", "직원명");
            chargeSearchLookUpEdit.Properties.DataSource = projectRepo.Select_PM_Charge();
            chargeSearchLookUpEdit.Properties.ValueMember = "직원코드";
            chargeSearchLookUpEdit.Properties.DisplayMember = "직원명";

            itemCodeColLookUpEdit.DataSource = doc.GetFileItemCode("102020");
            itemCodeColLookUpEdit.ValueMember = "ItemCode";
            itemCodeColLookUpEdit.DisplayMember = "Title";
        }

        private void Set_ProjectMaster(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.OptionsView.ShowAutoFilterRow = true; // 자동 필터 표시
            gv.OptionsView.ShowGroupPanel = false; // 그룹 패널 숨김

            // 컬럼 생성
            grid.Create_GvColumn(gv, "사업장번호", "CorpId");
            grid.Create_GvColumn(gv, "프로젝트ID", "ProjectID");
            grid.Create_GvColumn(gv, "분류", "CLASS", lup_Class);
            grid.Create_GvColumn(gv, "항목", "Type");
            grid.Create_GvColumn(gv, "프로젝트명", "ProjectName");
            grid.Create_GvColumn(gv, "품명", "ItemCode", lup_Material); // 품목 테이블과 연계 안되는 아예 별개의 프로젝트 품명
            grid.Create_GvColumn(gv, "시작계획일", "PlanStartDate", lup_PlanStartDate);
            grid.Create_GvColumn(gv, "목표종료일", "PlanEndDate", lup_PlanEndDate);
            grid.Create_GvColumn(gv, "시작일", "StartDate", lup_StartDate);
            grid.Create_GvColumn(gv, "종료일", "EndDate", lup_EndDate);
            grid.Create_GvColumn(gv, "소요일", "DurationDate");  // 소요일은 분류 항목에 따라 자동계산(수정불가)
            grid.Create_GvColumn(gv, "담당자", "Charge", lup_Charge);
            grid.Create_GvColumn(gv, "PM", "PMName", lup_PM);
            grid.Create_GvColumn(gv, "비고", "Remark");

            //수정가능
            grid.Column_AllowEdit(gv.Columns["CLASS"]);
            grid.Column_AllowEdit(gv.Columns["Type"]);
            grid.Column_AllowEdit(gv.Columns["ProjectName"]);
            grid.Column_AllowEdit(gv.Columns["ItemCode"]);
            grid.Column_AllowEdit(gv.Columns["StartDate"]);
            grid.Column_AllowEdit(gv.Columns["EndDate"]);
            grid.Column_AllowEdit(gv.Columns["PlanStartDate"]);
            grid.Column_AllowEdit(gv.Columns["PlanEndDate"]);
            grid.Column_AllowEdit(gv.Columns["Charge"]);
            grid.Column_AllowEdit(gv.Columns["PMName"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);


            // 숨김 컬럼들
            gv.Columns["CorpId"].Visible = false;
            gv.Columns["ProjectID"].Visible = false;
            gv.Columns["Type"].Visible = false;

            gv.BestFitColumns();

            // 이벤트 
            gv.FocusedRowChanged += gvProjectMaster_FocusedRowChanged;
            gv.CellValueChanged += gvProjectMaster_CellValueChanged;
        }
    
        #endregion 디자인 설정 END


        #region GV-Edit컨트롤 양방향 동기화 이벤트
        // Layout Edit 컨트롤들의 값 변경 이벤트

        // Edit 컨트롤 값 변경 시 Grid에 반영
        private void EditControl_ValueChanged(object sender, EventArgs e)
        {
            if (_isUpdating || gvProjectMaster.FocusedRowHandle < 0) return;

            _isUpdating = true;

            try
            {
                var editControl = sender as BaseEdit;
                if (editControl == null) return;

                // 현재 포커스된 행의 데이터 업데이트
                DataRow currentRow = gvProjectMaster.GetDataRow(gvProjectMaster.FocusedRowHandle);
                if (currentRow == null) return;

                // 컨트롤별로 해당 컬럼 업데이트
                if (editControl == classTextEdit)
                    currentRow["CLASS"] = classTextEdit.EditValue ?? DBNull.Value;
                else if (editControl == projectNameTextEdit)
                    currentRow["ProjectName"] = projectNameTextEdit.EditValue ?? DBNull.Value;
                else if (editControl == materialSearchLookUpEidit)
                    currentRow["ItemCode"] = materialSearchLookUpEidit.EditValue ?? DBNull.Value;
                else if (editControl == chargeSearchLookUpEdit)
                    currentRow["Charge"] = chargeSearchLookUpEdit.EditValue ?? DBNull.Value;
                else if (editControl == pMNameSearchLookUpEidit)
                    currentRow["PMName"] = pMNameSearchLookUpEidit.EditValue ?? DBNull.Value;
                else if (editControl == startDateDateEdit)
                    currentRow["StartDate"] = startDateDateEdit.EditValue ?? DBNull.Value;
                else if (editControl == endDateDateEdit)
                    currentRow["EndDate"] = endDateDateEdit.EditValue ?? DBNull.Value;
                else if (editControl == remarkTextEdit)
                    currentRow["Remark"] = remarkTextEdit.EditValue ?? DBNull.Value;
                else if (editControl == productImagePictureEdit)
                    currentRow["ProductImage"] = productImagePictureEdit.EditValue ?? DBNull.Value;

                // Grid 새로고침 
                gvProjectMaster.RefreshRow(gvProjectMaster.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 동기화 중 오류가 발생했습니다: {ex.Message}");
                
            }
            finally
            {
                _isUpdating = false;
            }
        }

        // Grid의 셀 값 변경 이벤트
        private void gvProjectMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_isUpdating) return;

            _isUpdating = true;

            try
            {
                // Grid에서 변경된 값을 Layout Edit 컨트롤에 반영
                DataRow currentRow = gvProjectMaster.GetDataRow(e.RowHandle);
                if (currentRow == null) return;


                // 변경된 컬럼에 따라 해당 Edit 컨트롤 업데이트
                switch (e.Column.FieldName)
                {
                    case "CLASS":
                        classTextEdit.EditValue = currentRow["CLASS"];
                        // CLASS 변경 시 소요일 자동 계산
                        CalculateDurationDate(currentRow);
                        break;
                    case "ProjectName":
                        projectNameTextEdit.EditValue = currentRow["ProjectName"];
                        break;
                    case "ItemCode":
                        materialSearchLookUpEidit.EditValue = currentRow["ItemCode"];
                        break;
                    case "Charge":
                        chargeSearchLookUpEdit.EditValue = currentRow["Charge"];
                        break;
                    case "PMName":
                        pMNameSearchLookUpEidit.EditValue = currentRow["PMName"];
                        break;
                    case "StartDate":
                        startDateDateEdit.EditValue = currentRow["StartDate"];
                        // 시작일 변경 시 종료일 자동 계산
                        CalculateEndDate(currentRow);
                        break;
                    case "EndDate":
                        endDateDateEdit.EditValue = currentRow["EndDate"];
                        break;
                    case "Remark":
                        remarkTextEdit.EditValue = currentRow["Remark"];
                        break;
                    case "ProductImage":
                        productImagePictureEdit.EditValue = currentRow["ProductImage"];
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Grid 값 동기화 중 오류가 발생했습니다: {ex.Message}");                
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void CalculateEndDate(DataRow currentRow)
        {
            // 시작일 + 소요일로 종료일 계산
            try
            {
                object startDateObj = currentRow["StartDate"];
                object durationObj = currentRow["DurationDate"];

                if (startDateObj == DBNull.Value || durationObj == DBNull.Value) return;

                DateTime startDate = Convert.ToDateTime(startDateObj);
                int duration = Convert.ToInt32(durationObj);

                DateTime endDate = startDate.AddDays(duration);
                currentRow["EndDate"] = endDate;

                // Layout Edit 컨트롤도 업데이트
                if (!_isUpdating)
                {
                    _isUpdating = true;
                    try
                    {
                        endDateDateEdit.EditValue = endDate;
                    }
                    finally
                    {
                        _isUpdating = false;
                    }
                }

                gvProjectMaster.RefreshRow(gvProjectMaster.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"종료일 계산 중 오류: {ex.Message}");
            }
        }


        private void CalculateDurationDate(DataRow currentRow)
        {
            try
            {
                // CLASS : 프로젝트 분류 코드 기반으로 소요일 계산
                string pjClass = currentRow["CLASS"]?.ToString();
                if (string.IsNullOrEmpty(pjClass))
                {
                    currentRow["DurationDate"] = 0;
                    return;
                }

                // CodeProject 에서 C해당 Class 의 WorkDays 합계 조회
                query =  $@"
                            SELECT SUM(ISNULL(WorkDays, 0)) AS TotalWorkDays
                            FROM CodeProject 
                            WHERE CorpId = '{WrGlobal.CorpID}' AND CLASS = '{pjClass}'
                        ";
                DataRow result = db.GetDataRow(query);

                int totalWorkDays = 0;
                if (result != null && result["TotalWorkDays"] != DBNull.Value)
                {
                    totalWorkDays = Convert.ToInt32(result["TotalWorkDays"]);
                }
                currentRow["DurationDate"] = totalWorkDays;


                // 소요일이 변경되었으므로 종료일도 재계산
                CalculateEndDate(currentRow);

                gvProjectMaster.RefreshRow(gvProjectMaster.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"소요일 계산 중 오류: {ex.Message}");
            }
        }
        #endregion

        #region 조회
        private void DisplayData()
        {
            try
            {
                pm.SearchStartDate = Dt_Date.smtStartDateTimeValue;
                pm.SearchEndDate = Dt_Date.smtEndDateTimeValue;
                gcProjectMaster.DataSource = pm.SelectProjectMasterList();
                gvProjectMaster.BestFitColumns();
                //this.projectMasterTableAdapter.FillByList(this.designDataSet.ProjectMaster, WrGlobal.CorpID, projectType, Dt_Date.smtStartDateTimeValue, Dt_Date.smtEndDateTimeValue, WrGlobal.LoginID);

                if(gcProjectMaster.DataSource != null)
                {
                    //삭제 후 프로젝트 상세내역 재조회 안먹혀서 추가
                    //추후에 여유로울 때 이전 포커스행 저장해서 다시 포커스되도록 수정 필요
                    gvProjectMaster.FocusedRowHandle = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gvProjectMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0) return;

            DataRow selectedRow = gvProjectMaster.GetDataRow(e.FocusedRowHandle);
            if (selectedRow != null)
            {
                _isUpdating = true;
                try
                {
                    // 각 Edit 컨트롤에 데이터 sett
                    corpIDTextEdit.EditValue = selectedRow["CorpId"];
                    projectIDTextEdit.EditValue = selectedRow["ProjectID"];
                    classTextEdit.EditValue = selectedRow["CLASS"];
                    projectNameTextEdit.EditValue = selectedRow["ProjectName"];
                    materialSearchLookUpEidit.EditValue = selectedRow["ItemCode"];
                    chargeSearchLookUpEdit.EditValue = selectedRow["Charge"];
                    pMNameSearchLookUpEidit.EditValue = selectedRow["PMName"];
                    startDateDateEdit.EditValue = selectedRow["StartDate"];
                    endDateDateEdit.EditValue = selectedRow["EndDate"];
                    PlanStartDateDateEdit.EditValue = selectedRow["PlanStartDate"];
                    PlanEnddateEdit.EditValue = selectedRow["PlanEndDate"];
                    remarkTextEdit.EditValue = selectedRow["Remark"];

                    if (selectedRow["ProductImage"] != DBNull.Value)
                    {
                        productImagePictureEdit.EditValue = selectedRow["ProductImage"];
                    }
                    else
                    {
                        productImagePictureEdit.EditValue = null;
                    }
                }
                finally
                {
                    _isUpdating = false;
                }
            }
            // 멤버 및 파일 정보 재조회
            DisplayFile();
        }
        #endregion 조회 END

        #region 상단패널 클릭이벤트 (조회 저장 삭제) 
        // 조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }          
        }

        // 새 행 추가
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (hasUnsavedNewRow && gvProjectMaster.FocusedRowHandle >= 0)
                {
                    MessageBox.Show("저장되지 않은 새 프로젝트가 있습니다. 먼저 저장해주세요.", "알림",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable dt = (DataTable)gcProjectMaster.DataSource;

                string docNo = "PJ" + db.ExecAutoNumber("프로젝트", DateTime.Now.Date);
                DataRow newRow = dt.NewRow();

                // 기본값 세팅
                newRow["CorpID"] = WrGlobal.CorpID;
                newRow["ProjectID"] = docNo;
                newRow["ProjectName"] = "";
                newRow["ProjectType"] = projectType;
                newRow["Charge"] = WrGlobal.LoginID;
                newRow["PMName"] = "";
                newRow["StartDate"] = DBNull.Value;
                newRow["EndDate"] = DBNull.Value;
                newRow["PlanStartDate"] = DateTime.Now.Date;
                newRow["PlanEndDate"] = DateTime.Now.Date.AddDays(21); // 기본 종료일은 3주 후
                newRow["DurationDate"] = 0;
                newRow["Remark"] = "";

                newRow["CreDt"] = DateTime.Now;
                newRow["CreId"] = WrGlobal.LoginID;
                newRow["CreIP"] = WrGlobal.ClientHostName;
                newRow["ModDt"] = DBNull.Value;
                newRow["ModId"] = DBNull.Value;
                newRow["ModIP"] = DBNull.Value;
                newRow["ProductImage"] = DBNull.Value;

                dt.Rows.Add(newRow);

                hasUnsavedNewRow = true;

                gvProjectMaster.RefreshData();
                gvProjectMaster.FocusedRowHandle = gvProjectMaster.RowCount - 1;
                gvProjectMaster.SelectRow(gvProjectMaster.FocusedRowHandle);

                // 프로젝트명 컬럼으로 포커스 이동하여 바로 편집 가능
                gvProjectMaster.FocusedColumn = gvProjectMaster.Columns["ProjectName"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 저장
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvProjectMaster.FocusedRowHandle < 0)
                {
                    MessageBox.Show("선택된 프로젝트가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 유효성 검사
                if (string.IsNullOrEmpty(projectNameTextEdit.Text))
                {
                    MessageBox.Show("프로젝트명을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    projectNameTextEdit.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(classTextEdit.Text))
                {
                    MessageBox.Show("프로젝트 분류를 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    classTextEdit.Focus();
                    return;
                }
                if (PlanStartDateDateEdit.EditValue == null)
                {
                    MessageBox.Show("시작계획일을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    startDateDateEdit.Focus();
                    return;
                }
                if (PlanEnddateEdit.EditValue == null)
                {
                    MessageBox.Show("목표종료일을 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    endDateDateEdit.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(chargeSearchLookUpEdit.Text))
                {
                    MessageBox.Show("담당자를 입력해주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chargeSearchLookUpEdit.Focus();
                    return;
                }

                if (MessageBox.Show("자료를 저장 하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    gvProjectMaster.PostEditor();
                    gvProjectMaster.UpdateCurrentRow();

                    pm.CorpID = corpIDTextEdit.Text;
                    pm.ProjectID = projectIDTextEdit.Text;
                    pm.ProjectType = projectType;
                    pm.CLASS = classTextEdit.EditValue?.ToString();
                    pm.Charge = chargeSearchLookUpEdit.EditValue?.ToString();
                    pm.PMName = pMNameSearchLookUpEidit.EditValue?.ToString();
                    pm.ProjectName = projectNameTextEdit.Text;
                    pm.StartDate = startDateDateEdit.DateTime == DateTime.MinValue ? (DateTime?)null : startDateDateEdit.DateTime;
                    pm.EndDate = endDateDateEdit.DateTime == DateTime.MinValue ? (DateTime?)null : endDateDateEdit.DateTime;
                    pm.Remark = remarkTextEdit.Text;
                    pm.ItemCode = materialSearchLookUpEidit.EditValue?.ToString();
                    pm.ProductImage = productImagePictureEdit.EditValue as byte[];

                    if (pm.InsertOrUpdateProjectMaster())
                    {
                        ProjectDetail pd = new ProjectDetail();
                        pd.CorpID = corpIDTextEdit.Text;
                        pd.ProjectID = projectIDTextEdit.Text;
                        pd.TItle = projectNameTextEdit.Text;
                        pd.Charge = chargeSearchLookUpEdit.EditValue?.ToString();
                        pd.PMName = pMNameSearchLookUpEidit.EditValue?.ToString();
                        pd.StartDate = startDateDateEdit.DateTime == DateTime.MinValue ? (DateTime?)null : startDateDateEdit.DateTime;
                        pd.EndDate = endDateDateEdit.DateTime == DateTime.MinValue ? (DateTime?)null : endDateDateEdit.DateTime;
                        pd.PlanStartDate = PlanStartDateDateEdit.DateTime == DateTime.MinValue ? (DateTime?)null : PlanStartDateDateEdit.DateTime;
                        pd.PlanEndDate = PlanEnddateEdit.DateTime == DateTime.MinValue ? (DateTime?)null : PlanEnddateEdit.DateTime;

                        if (pd.InsertOrUpdateProjectDetail())
                        {
                            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            hasUnsavedNewRow = false;
                            DisplayData();
                        }
                        else
                        {
                            pm.Del_ProjectMaster();
                        }
                    }
                    else
                    {
                        MessageBox.Show("저장에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"저장 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //삭제
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (gvProjectMaster.FocusedRowHandle < 0)
            {
                MessageBox.Show("선택된 프로젝트가 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string projectName = (gvProjectMaster.GetFocusedRowCellValue("ProjectName") ?? "").ToString();
            string projectID = (gvProjectMaster.GetFocusedRowCellValue("ProjectID") ?? "").ToString();

            if (MessageBox.Show($"선택한 프로젝트 '{projectName}'를\n삭제하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                pm.CorpID = WrGlobal.CorpID;
                pm.ProjectID = projectID;

                if (!pm.HasProjectDetail())
                {
                    if (pm.Del_ProjectMaster())
                    {
                        hasUnsavedNewRow = false;
                        DisplayData();
                    }
                }
                else
                {
                    MessageBox.Show("삭제할 수 없습니다.", "이미 프로젝트가 진행중입니다.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"삭제 중 오류가 발생했습니다: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DisplayData();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 파일 조회 저장 삭제
        private void DisplayFile()
        {
            try
            {
                // 프로젝트 마스터 연결 파일 조회
                if (gvProjectMaster.FocusedRowHandle < 0) return;

                string projectID = (gvProjectMaster.GetFocusedRowCellValue("ProjectID") ?? "").ToString();
                pm.ProjectID = projectID;
                gcCodeDocumentFile.DataSource = pm.SelectProjectFiles();
                gvCodeDocumentFile.BestFitColumns();

                //this.codeDocumentFileTableAdapter.FillByKey(this.codeDataSet.CodeDocumentFile, WrGlobal.CorpID, "Project",
                //                                    gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString(), "", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if ((gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString() ?? "") == "") return;

            try
            {
                string projectID = (gvProjectMaster.GetFocusedRowCellValue("ProjectID") ?? "").ToString();

                foDialog.FileName = "";

                if (foDialog.ShowDialog() == DialogResult.OK)
                {
                    if (foDialog.FileNames.Length > 0)
                    {
                        int fileSeq = doc.GetNextFileSeq("PJ_ProjectDocumentFile", "Gubun = 'Project' and KeyCode1 = '" + projectID + "'");

                        FtpFilesUploader uploader = new FtpFilesUploader("PJ_ProjectDocumentFile", fileSeq, "Project|" + projectID, foDialog.FileNames);
                        uploader.ShowDialog();

                        DisplayFile();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (gvCodeDocumentFile.SelectedRowsCount < 0) return;

            if (fbDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> selPaths = new List<string>();
                List<string> selOriFiles = new List<string>();
                List<string> selFiles = new List<string>();

                int[] idxs = gvCodeDocumentFile.GetSelectedRows();
                long totalSize = 0;

                foreach (int idx in idxs)
                {
                    selPaths.Add(gvCodeDocumentFile.GetRowCellValue(idx, "Path").ToString());
                    selOriFiles.Add(gvCodeDocumentFile.GetRowCellValue(idx, "OriFileName").ToString());
                    selFiles.Add(gvCodeDocumentFile.GetRowCellValue(idx, "FileName").ToString());
                    totalSize += Convert.ToInt64(gvCodeDocumentFile.GetRowCellValue(idx, "Capacity").ToString());
                }

                FtpFileDownloader downloader = new FtpFileDownloader(fbDialog.SelectedPath, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
                downloader.ShowDialog();
            }
        }

        private void btnFileDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("선택한 파일을 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            try
            {
                DataRowView drv = (DataRowView)gvCodeDocumentFile.GetFocusedRow();
                if (drv == null) return;
                clsFtp ftp = new clsFtp();

                string projectID = (gvProjectMaster.GetFocusedRowCellValue("ProjectID") ?? "").ToString();
                string filePath = drv["Path"]?.ToString() ?? "";
                string fileName = drv["FileName"]?.ToString() ?? "";
                int fileSeq = Convert.ToInt32(drv["FileSeq"]);


                // FTP에서 파일 삭제
                if (ftp.DeleteFile(filePath + fileName))
                {
                    // DB에서 파일 정보 삭제
                    pm.CorpID = WrGlobal.CorpID;
                    pm.ProjectID = projectID;
                    pm.FileSeq = fileSeq;

                    if (pm.Del_ProjectMasterDocument())
                    {
                        MessageBox.Show("파일이 삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisplayFile(); // 파일 목록 새로고침
                    }
                    else
                    {
                        MessageBox.Show("파일은 삭제되었지만 DB 정보 삭제에 실패했습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("파일 삭제에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 삭제 중 오류: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

    }
}