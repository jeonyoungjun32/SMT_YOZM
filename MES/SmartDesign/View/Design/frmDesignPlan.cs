using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using SmartCommon.Common;
using SmartCommon.Models;
using SmartCommon.View.Common;
using SmartCommon.View.Documents;
using SmartDesign.View.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SmartDesign.Model;
using SmartMES.Common;
using System.Linq;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;

namespace SmartDesign.View.Design
{
	public partial class frmDesignPlan : DevExpress.XtraEditors.XtraForm
    {
        string query = "";
        string corp = WrGlobal.CorpID;
        Database db = new Database();
        DesignPlan designplan = new DesignPlan();
        clsDocument doc = new clsDocument();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        List<ProjectTask> projectTasks = new List<ProjectTask>();
        ProjectRepository projectRepo = new ProjectRepository();

        string projectType = "";

        public frmDesignPlan()
        {
            InitializeComponent();

            itemCodeColLookUpEdit.DataSource = doc.GetFileItemCode("102020");
            itemCodeColLookUpEdit.ValueMember = "ItemCode";
            itemCodeColLookUpEdit.DisplayMember = "Title";
        }

        public frmDesignPlan(string _projectType)
        {
            InitializeComponent();

            projectType = _projectType;

            itemCodeColLookUpEdit.DataSource = doc.GetFileItemCode("102020");
            itemCodeColLookUpEdit.ValueMember = "ItemCode";
            itemCodeColLookUpEdit.DisplayMember = "Title";
        }

        private void frmDesignProject_Load(object sender, EventArgs e)
        {
            btn_Complete.Visible = false;

            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-01-01"));
            DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-12-31"));
            startDate = startDate.AddYears(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            DisplayData();
        }

        #region 조회

        //ProjectMater 테이블 가져오기
        private void DisplayData()
        {
            try
            {
                designplan.SearchStartDate = Dt_Date.smtStartDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
                designplan.SearchEndDate = Dt_Date.smtEndDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
                gcProjectMaster.DataSource =  designplan.SelectProjectMasterList();

                //이벤트
                gvProjectDetail.CellValueChanged += gvProjectDetail_CellValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // 파일 목록 가져오기
        private void DisplayFile()
        {
            try
            {
                if (gcProjectMaster.DataSource == null || gvProjectMaster.DataRowCount < 1) return;
                this.codeDocumentFileTableAdapter.FillByKey(this.codeDataSet.CodeDocumentFile, WrGlobal.CorpID, "Project",
                                                    gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString(), "", "");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ProjectDetail 테이블 가져오기 
        private void DisPlayDataDetail()
        {
            try
            {
                //ProjectDetail 테이블 생성
                string projectid = gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString();
                string ProjectName = gvProjectMaster.GetFocusedRowCellValue("ProjectName")?.ToString();

                query = $"exec Project_GetProjectSchedule '{corp}', '{projectid}'";
                gvProjectDetail.Columns.Clear();
                gcProjectDetail.DataSource = null;
                gcProjectDetail.DataSource = db.GetDataView("프로젝트", query);

                gvProjectDetail.Columns["내용"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

                RepositoryItemLookUpEdit statusEditor = new RepositoryItemLookUpEdit();

                string selectStatus = "SELECT Title, ItemCode FROM CmCateItem WHERE CorpId = '" + WrGlobal.CorpID + "' AND class LIKE 'PJ10%'";
                statusEditor.DataSource = db.GetDataView("프로젝트", selectStatus);
                statusEditor.ValueMember = "Title";       
                statusEditor.DisplayMember = "Title";   
                statusEditor.NullText = "선택";

                gcProjectDetail.RepositoryItems.Add(statusEditor);

                GridColumn colStatus = gvProjectDetail.Columns["상태"];
                if (colStatus != null)
                {
                    colStatus.ColumnEdit = statusEditor;
                    colStatus.OptionsColumn.AllowEdit = true;
                    colStatus.OptionsColumn.ReadOnly = false;
                }

                RepositoryItemSearchLookUpEdit lupCharge = rpo.Create_RepositoryItemSearchLookUp(projectRepo.Select_PM_Charge(), "직원코드", "직원명");
                gvProjectDetail.Columns["담당자"].ColumnEdit = lupCharge;

                if (gvProjectDetail.Columns.Count > 0)
                {
                    gvProjectDetail.Columns[0].Visible = false; // 사업장번호
                    gvProjectDetail.Columns[1].Visible = false; // GroupYn
                    gvProjectDetail.Columns[2].Visible = false; // GroupLevel
                    gvProjectDetail.Columns[3].Visible = false; // 프로젝트 ID
                    gvProjectDetail.Columns[5].Visible = true; // 제품유형
                    gvProjectDetail.Columns[6].Visible = true; // 항목
                    gvProjectDetail.Columns[7].Visible = false; // 내작/외주

                    gvProjectDetail.Columns["내용"].OptionsColumn.AllowEdit = false;
                }
                gvProjectDetail.BestFitColumns();
                for (int i = 18; i < gvProjectDetail.Columns.Count; i++)
                {
                    gvProjectDetail.Columns[i].Width = 50;
                    gvProjectDetail.Columns[i].OptionsColumn.AllowEdit = false;
                }

                var editableColumns = new[] { "시작일자", "완료일자", "상태" };

                foreach (GridColumn column in gvProjectDetail.Columns)
                {
                    bool isEditable = editableColumns.Contains(column.FieldName);

                    column.OptionsColumn.AllowEdit = isEditable;
                }

                gvProjectDetail.CellValueChanged -= gvProjectDetail_CellValueChanged;
                gvProjectDetail.CellValueChanged += gvProjectDetail_CellValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        private void gvProjectDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

            string column = e.Column.FieldName;
            int rowHandle = e.RowHandle;

            object value = e.Value;
            string currentStatus = view.GetRowCellValue(rowHandle, "상태")?.ToString();

            if (column == "시작일자")
            {
                bool hasValue = DateTime.TryParse(value?.ToString(), out _);

                if (hasValue)
                {
                    if (currentStatus != "완료")
                        view.SetRowCellValue(rowHandle, "상태", "진행중");
                }
                else
                {
                    view.SetRowCellValue(rowHandle, "상태", "대기");
                }
            }

            if (column == "완료일자")
            {
                bool hasValue = DateTime.TryParse(value?.ToString(), out _);

                if (hasValue)
                {
                    view.SetRowCellValue(rowHandle, "상태", "완료");
                }
                else
                {
                    object startDate = view.GetRowCellValue(rowHandle, "시작일자");
                    bool hasStartDate = DateTime.TryParse(startDate?.ToString(), out _);

                    if (hasStartDate)
                    {
                        view.SetRowCellValue(rowHandle, "상태", "진행중");
                    }
                    else
                    {
                        view.SetRowCellValue(rowHandle, "상태", "대기");
                    }
                }
            }
        }

        private void gvProjectMaster_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DisplayFile();
                DisPlayDataDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void gvProjectDetail_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.CellValue == null) return;


            string groupYn = gvProjectDetail.GetRowCellValue(e.RowHandle, "GroupYn").ToString();


            if (e.RowHandle >= 0)
            {
                if (groupYn == "Y" && (e.CellValue.ToString() == "계획" || e.CellValue.ToString() == "P"))
                {
                    e.Appearance.BackColor = Color.DarkGray;
                }
                if (groupYn == "Y" && (e.CellValue.ToString() == "계획" || e.CellValue.ToString() == "PS"))
                {
                    e.Appearance.BackColor = Color.FromArgb(150, 194, 145);
                }
                else if (e.CellValue.ToString() == "계획" || e.CellValue.ToString() == "P" || e.CellValue.ToString() == "대기")
                {
                    e.Appearance.BackColor = Color.LightGray;
                }
                else if (e.CellValue.ToString() == "계획" || e.CellValue.ToString() == "PS" || e.CellValue.ToString() == "대기")
                {
                    e.Appearance.BackColor = Color.FromArgb(244, 238, 238);
                }
                else if (e.CellValue.ToString() == "시작" || e.CellValue.ToString() == "OS" || e.CellValue.ToString() == "진행중")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 219, 170);
                }
                else if (e.CellValue.ToString() == "종료" || e.CellValue.ToString() == "CS" || e.CellValue.ToString() == "완료")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 183, 183);
                }
                else
                {
                    return;
                }
            }

        }

        #region  상단버튼


        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void btn_Change_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gvProjectDetail.GetSelectedRows();

            if (selectedRows.Length == 1)
            {
                int selectedHandle = selectedRows[0];

                frmDesignPlanChange planChange = new frmDesignPlanChange();
                planChange.PlandEndDate = (gvProjectDetail.GetRowCellValue(selectedHandle, "완료일자(계획)") as DateTime?)?.ToString("yyyy-MM-dd") ?? "";
                //gvProjectDetail.GetRowCellValue(selectedHandle, "완료일자(계획)")?.ToString() ?? "";

                GetProjectDetailList();

                if (planChange.ShowDialog() == DialogResult.OK)
                {
                    string selectedDateStr = planChange.PlandEndDate;
                    string originalDateStr = (gvProjectDetail.GetRowCellValue(selectedHandle, "완료일자(계획)") as DateTime?)?.ToString("yyyy-MM-dd") ?? "";
                    //gvProjectDetail.GetRowCellValue(selectedHandle, "완료일자(계획)")?.ToString() ?? "";

                    int targetPjID = Convert.ToInt32(gvProjectDetail.GetRowCellValue(selectedHandle, "제품유형"));

                    DateTime newEndDate = Convert.ToDateTime(selectedDateStr);

                    ChangeTaskDates(projectTasks, targetPjID, newEndDate);

                    DisPlayDataDetail();
                }
            }
            else if(selectedRows.Length == 0)
            {
                MessageBox.Show("변경할 스케줄이 선택되지 않았습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("변경할 스케줄을 하나만 선택해 주세요", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            clsFunction fns = new clsFunction();
            fns.ExportExcel(gvProjectDetail);
        }
        private void cmdMakePlan_Click(object sender, EventArgs e)
        {
            try
            {
                gvProjectDetail.PostEditor();
                gvProjectDetail.UpdateCurrentRow();

                string projectID = (gvProjectMaster.GetFocusedRowCellValue("ProjectID") ?? "").ToString();

                if (string.IsNullOrEmpty(projectID))
                    return;  

                bool shouldGenerate = false;
                if(gvProjectDetail.DataSource is DataView view)
                {
                    if (view.Count == 0)
                    {
                        shouldGenerate = true;
                    }
                    else
                    {
                        var result = MessageBox.Show(
                            "스케줄이 생성되어 있습니다. 다시 생성하시겠습니까?",
                            "알림",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Information
                        );

                        if (result == DialogResult.OK)
                            shouldGenerate = true;
                    }
                }
                
                if (shouldGenerate)
                {
                    string query = $"exec Project_MakeProjectSchedule '{corp}', '{projectID}'";
                    db.ExecuteQuery(query);

                    MessageBox.Show("스케줄이 생성되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DisPlayDataDetail();
            }
        }

        #endregion

        #region ftp파일관리

        // 파일 업로드
        private void btnUpload_Click(object sender, EventArgs e)
        {
            clsFunction func = new clsFunction();
            func.ExportExcel(gvProjectDetail);
        }
        // 파일다운로드 
        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (gvCodeDocumentFile.SelectedRowsCount < 1) return;

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
        // 파일 삭제
        private void btnFileDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("선택한 파일을 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            DataRowView drv = (DataRowView)gvCodeDocumentFile.GetFocusedRow();
            clsFtp ftp = new clsFtp();

            if (ftp.DeleteFile(drv["Path"].ToString() + drv["FileName"].ToString()))
            {
                codeDocumentFileBindingSource.RemoveCurrent();
                this.codeDocumentFileTableAdapter.Update(this.codeDataSet.CodeDocumentFile);
            }
        }


        private void layoutCtlGrpFile_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            try
            {
                this.Validate();

                codeDocumentFileBindingSource.EndEdit();
                this.codeDocumentFileTableAdapter.Update(this.codeDataSet.CodeDocumentFile);

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 프로젝트

        private void layoutCtlGrpProject_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            LayoutControlGroup layoutGroup = sender as LayoutControlGroup;
            //if (e.Button.Properties.Caption == "Hide")
            //    layoutGroup.HideToCustomization();

            string projectid = gvProjectMaster.GetFocusedRowCellValue("ProjectID")?.ToString() ?? "";
            frmProjectModify frm;
            switch (e.Button.Properties.Caption)
            {
                case "프로젝트추가":
                    frm = new frmProjectModify(corp, projectid, "");
                    frm.ShowDialog();
                    DisPlayDataDetail();
                    break;
                case "프로젝트수정":

                    if (projectid == "")
                    {
                        MessageBox.Show("선택한 디자인프로젝트가 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    frm = new frmProjectModify(corp, projectid, projectid);
                    frm.ShowDialog();
                    DisplayFile();
                    DisPlayDataDetail();
                    break;
            }

        }

        #endregion

        #region 프로젝트계획

        #endregion

        private void btnUpload_Click_1(object sender, EventArgs e)
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
                        int fileSeq = doc.GetNextFileSeq("CodeDocumentFile", "Gubun = 'Project' and KeyCode1 = '" + projectID + "'");

                        FtpFilesUploader uploader = new FtpFilesUploader("CodeDocumentFile", fileSeq, "Project|" + projectID, foDialog.FileNames);
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

        private void productImagePictureEdit_DoubleClick(object sender, EventArgs e)
        {
            frmImageView form = new frmImageView(productImagePictureEdit.Image);
            form.ShowDialog();
        }

        private void productImagePictureEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gvProjectDetail_DoubleClick(object sender, EventArgs e)
        {
            //string projectID = gvProjectDetail.GetFocusedRowCellValue("프로젝트ID")?.ToString();
            //string PjID = gvProjectDetail.GetFocusedRowCellValue("제품유형")?.ToString();

            //frmDesignPlanDetail form = new frmDesignPlanDetail("Update",projectID, PjID);
            //form.ShowDialog();

            //DisPlayDataDetail();
        }


        private void gvProjectDetail_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column != null)
            {
                if (e.Column.FieldName != null)
                {
                    if (e.Column.FieldName.IndexOf("W") >= 0 || e.Column.FieldName.IndexOf("/") >= 0)
                    {
                        if (e.Value != null)
                        {
                            switch (e.Value.ToString())
                            {
                                case "P":
                                case "PS":
                                case "OS":
                                case "CS":
                                    e.DisplayText = "";
                                    break;
                            }
                        }
                    }
                }

            }
        }

        private void GetProjectDetailList()
        {
            DataView dv = gvProjectDetail.DataSource as DataView;
            DataTable pjDetail = dv.ToTable();

            projectTasks = pjDetail.AsEnumerable().Select(row => new ProjectTask
            {
                ProjectID = row["프로젝트ID"].ToString(),
                PjID = Convert.ToInt32(row["제품유형"]),
                WorkDays = Convert.ToInt32(row["작업일"]),
                DateType = row["구분"].ToString(),
                GroupLevel = Convert.ToInt32(row["GroupLevel"]),
                Title = row["내용"].ToString(),
                StartDate = row.Field<DateTime?>("시작일자"),
                EndDate = row.Field<DateTime?>("완료일자"),
                PlanStartDate = row.Field<DateTime?>("시작일자(계획)"),
                PlanEndDate = row.Field<DateTime?>("완료일자(계획)"),
                Status = row["상태"].ToString()
            }).ToList();
        }

        private void ChangeTaskDates(List<ProjectTask> taskList, int basePjID, DateTime newPlanEndDate)
        {
            var baseTask = taskList.FirstOrDefault(t => t.PjID == basePjID);
            if (baseTask == null) return;

            DateTime originalPlanEndDate = baseTask.PlanEndDate.Value;
            int diffDays = (newPlanEndDate - originalPlanEndDate).Days;
            if (diffDays == 0)
            {
                Console.WriteLine("날짜 차이가 없습니다.");
                return;
            }

            //1. 선택된 중분류 날짜 변경
            baseTask.PlanEndDate = newPlanEndDate;
            SetDesignPlanInfo(baseTask); 
            designplan.ChangePlanEndDate();
            int baseGroupID = basePjID / 100;

            //2. 같은 그룹의 중분류들 날짜 밀기
            foreach (var t in taskList.Where(t => t.GroupLevel == 1 && t.PjID / 100 >= baseGroupID && t.PjID > basePjID && t.DateType =="순차"))
            {
                    if (t.PlanStartDate.HasValue) 
                        t.PlanStartDate = t.PlanStartDate.Value.AddDays(diffDays);
                    if (t.PlanEndDate.HasValue)
                        t.PlanEndDate = t.PlanEndDate.Value.AddDays(diffDays);

                    SetDesignPlanInfo(t); 
                    designplan.ChangePlanEndDate();
            }

            //3. 같은 그룹의 대분류 날짜 밀기
            int parentPjID = basePjID / 100;
            var parentTask = taskList.FirstOrDefault(t => t.PjID == parentPjID && t.GroupLevel == 0);
            if (parentTask != null)
            {
                var maxSubEnd = taskList
                    .Where(t => t.GroupLevel == 1 && (t.PjID / 100) == parentTask.PjID && t.PlanEndDate.HasValue)
                    .Max(t => t.PlanEndDate.Value);

                parentTask.PlanEndDate = maxSubEnd;

                SetDesignPlanInfo(parentTask);
                designplan.ChangePlanEndDate();
            }

            //4. 변경 완료 된 이후의 분류들 날짜 밀기
            DateTime? lastGroupEnd = parentTask?.PlanEndDate;  
            foreach (var groupTask in taskList.Where(t => t.GroupLevel == 0 && t.PjID > baseGroupID).OrderBy(t => t.PjID))
            {
                if (lastGroupEnd == null) break;

                // 대분류 시작일 = 이전 대분류의 PlanEndDate 다음날
                groupTask.PlanStartDate = lastGroupEnd.Value.AddDays(1);
                groupTask.PlanEndDate = groupTask.PlanStartDate.Value.AddDays(groupTask.WorkDays - 1);

                SetDesignPlanInfo(groupTask);
                designplan.ChangePlanEndDate();

                lastGroupEnd = groupTask.PlanEndDate;

                // 하위 중분류들 날짜 재계산
                var children = taskList
                    .Where(t => t.GroupLevel == 1 && (t.PjID / 100) == (groupTask.PjID))
                    .OrderBy(t => t.PjID);

                DateTime? subStart = groupTask.PlanStartDate;

                foreach (var child in children)
                {
                    if (child.DateType == "병행")
                    {
                        // 병행: 날짜 변경 없이 유지 (아무 것도 안함)
                    }
                    else
                    {
                        // 순차는 이전 중분류의 PlanEndDate 다음날
                        child.PlanStartDate = subStart;
                    }

                    child.PlanEndDate = child.PlanStartDate?.AddDays(child.WorkDays - 1);

                    SetDesignPlanInfo(child);
                    designplan.ChangePlanEndDate();

                    if (child.DateType == "순차")
                        subStart = child.PlanEndDate?.AddDays(1);  // 다음 순차 작업 기준점 갱신
                }

                // 대분류 PlanEndDate를 하위 중분류 중 가장 늦은 PlanEndDate로 조정
                var maxSubEnd = children
                    .Where(c => c.PlanEndDate.HasValue)
                    .Max(c => c.PlanEndDate.Value);

                groupTask.PlanEndDate = maxSubEnd;
                SetDesignPlanInfo(groupTask);
                designplan.ChangePlanEndDate();

                lastGroupEnd = groupTask.PlanEndDate;
            }
        }

        private void SetDesignPlanInfo(ProjectTask task)
        {
            if (task == null) return;

            designplan.ProjectID = task.ProjectID?.ToString();
            designplan.PjID = task.PjID.ToString();
            designplan.PlanStartDate = task.PlanStartDate;
            designplan.PlanEndDate = task.PlanEndDate;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            gvProjectDetail.PostEditor();
            gvProjectDetail.UpdateCurrentRow();

            var dataSource = gvProjectDetail.DataSource;
            List<DataRow> rows = new List<DataRow>();
            if (dataSource is DataView view)
            {
                rows = view.ToTable().AsEnumerable().ToList();
            }
            foreach (var item in rows)
            {
                DateTime? endDate = item.Field<DateTime?>("완료일자");
                if (endDate.HasValue)
                {
                    designplan.EndDate = endDate.Value;
                }
                else
                {
                    designplan.EndDate = null;
                }

                DateTime? startDate = item.Field<DateTime?>("시작일자");
                if (startDate.HasValue)
                {
                    designplan.StartDate = startDate.Value;
                }
                else
                {
                    designplan.StartDate = null;
                }

                designplan.Status = item["상태"]?.ToString();
                designplan.PjID = item["제품유형"]?.ToString();
                designplan.ProjectID = item["프로젝트ID"]?.ToString();

                designplan.ProjectDetailUpdate();
                DisPlayDataDetail();
            }
        }

        private void cmdDelPlan_Click(object sender, EventArgs e)
        {
            gvProjectMaster.PostEditor();
            gvProjectMaster.UpdateCurrentRow();

            gvProjectDetail.PostEditor();
            gvProjectDetail.UpdateCurrentRow();

            designplan.ProjectID = (gvProjectMaster.GetFocusedRowCellValue("ProjectID") ?? "").ToString();
            try
            {
                //프로젝트 세부 항목 삭제
                if (designplan.Del_ProjectDetail())
                {
                    //프로젝트 마스터 삭제
                    if (designplan.Del_ProjectMaster())
                    {
                        MessageBox.Show("프로젝트 삭제가 완료되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                DisplayData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    public class ProjectTask
    {
        public string ProjectID { get; set; }
        public int PjID { get; set; }
        public string Title { get; set; }
        public string DateType { get; set; }
        public int GroupLevel { get; set; }  // 0: 대분류, 1: 소분류
        public bool GroupYn { get; set; }    // Y: 대분류, N: 소분류
        public int WorkDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanEndDate { get; set; }

        public string Status { get; set; }
    }
}