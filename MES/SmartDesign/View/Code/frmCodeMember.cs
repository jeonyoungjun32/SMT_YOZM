using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using DevExpress.XtraGrid.Views.Grid;

using SmartCommon.Common;

using SmartDesign.Model;

namespace SmartDesign.View.Codes
{
	public partial class frmCodeMember : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        CodeMember codeMember = new CodeMember();
        clsRepository repo = new clsRepository();
        string query;

        public frmCodeMember()
        {
            InitializeComponent();
        }

        private void frmMaterialItem_Load(object sender, EventArgs e)
        {
            DisplayCategory();

            // 프로젝트 분류
            ProjectRepository projectRepo = new ProjectRepository();

            searchEmployee = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_PM_Charge(), "직원코드", "직원명");
            searchRole = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_Role(), "코드", "내용");
            searchPermission = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_Permission(), "코드", "내용");

            gcCodeMemberDetail.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                searchEmployee,
                searchRole,
                searchPermission
            });

            colEmpNo.ColumnEdit = searchEmployee;      // 사원 컬럼
            colWorkJob.ColumnEdit = searchRole;        // 역할 컬럼
            colWorkType.ColumnEdit = searchPermission; // 권한 컬럼

            // 값 선택 즉시 커밋
            searchEmployee.EditValueChanged += Repo_EditValueChanged;
            searchRole.EditValueChanged += Repo_EditValueChanged;
            searchPermission.EditValueChanged += Repo_EditValueChanged;

            // 클릭 즉시 편집 진입
            gvCodeMemberDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            gvCodeMemberDetail.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown; 
            gvCodeMemberDetail.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
        }


        private void Repo_EditValueChanged(object sender, EventArgs e)
        {
            gvCodeMemberDetail.PostEditor();
            gvCodeMemberDetail.UpdateCurrentRow();
        }

        private void cmdDisplay_ElementClick(object sender, EventArgs e)
        {
            // 트리조회
            DisplayCategory();
        }

        private void DisplayCategory()
        {
            //this.codeMemberMasterTableAdapter.Fill(this.codeDataSet.CodeMemberMaster);
           
            gcCodeMemberMaster.DataSource = codeMember.SelectCodeMemberMaster();
        }

        private void DisplayProject()
        {
            string mastID = gvCodeMemberMaster.GetFocusedRowCellValue("MastID")?.ToString() ?? "";
            if (!string.IsNullOrEmpty(mastID))
            {
                gcCodeMemberDetail.DataSource = codeMember.SelectCodeMemberDetail(mastID);
            }
            //this.codeMemberDetailTableAdapter.FillByList(this.codeDataSet.CodeMemberDetail, WrGlobal.CorpID, mastID);
        }

        private void cmdAdd_ElementClick(object sender, EventArgs e)
        {
            string newMastID = codeMember.GetNextMastID();
            DataTable dt = (DataTable)gcCodeMemberMaster.DataSource;
            if (dt != null)
            {
                DataRow newRow = dt.NewRow();
                newRow["CorpId"] = WrGlobal.CorpID;
                newRow["MastID"] = newMastID;
                newRow["Title"] = "";
                newRow["Remark"] = "";
                dt.Rows.Add(newRow);
            }

            //codeMemberMasterBindingSource.AddNew();
            //DataRowView drv = (DataRowView)codeMemberMasterBindingSource.Current;
            //drv["CorpId"] = WrGlobal.CorpID;
            //drv["MastID"] = "";
            //codeMemberMasterBindingSource.EndEdit();
        }

        private void cmdSave_ElementClick(object sender, EventArgs e)
        {
            gvCodeMemberMaster.PostEditor();
            gvCodeMemberMaster.UpdateCurrentRow();
            gvCodeMemberDetail.PostEditor();
            gvCodeMemberDetail.UpdateCurrentRow();

            try
            {
                if (MessageBox.Show("자료를 저장 하시겠습니까?", "저장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    bool masterNeedsSave = false;
                    bool masterSaveSuccess = true;

                    // 1. 마스터 저장 체크 및 처리
                    DataTable masterDt = (DataTable)gcCodeMemberMaster.DataSource;
                    foreach (DataRow row in masterDt.Rows)
                    {
                        if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                        {
                            string mastID = row["MastID"]?.ToString()?.Trim() ?? "";
                            string title = row["Title"]?.ToString()?.Trim() ?? "";

                            if (string.IsNullOrEmpty(mastID) || string.IsNullOrEmpty(title))
                            {
                                MessageBox.Show("그룹번호와 내용은 필수입니다.", "검증 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            masterNeedsSave = true;

                            // MastID 중복 체크 (신규 추가인 경우만)
                            if (row.RowState == DataRowState.Added)
                            {
                                if (codeMember.CheckMastIDExists(mastID))
                                {
                                    MessageBox.Show($"그룹번호 '{mastID}'가 이미 존재합니다.", "중복 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            codeMember.CorpId = row["CorpId"].ToString();
                            codeMember.MastID = mastID;
                            codeMember.Title = row["Title"]?.ToString() ?? "";
                            codeMember.Remark = row["Remark"]?.ToString() ?? "";

                            if (!codeMember.InsUpd_CodeMemberMaster())
                            {
                                MessageBox.Show("마스터 저장 중 오류가 발생했습니다.", "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                masterSaveSuccess = false;
                                return;
                            }
                        }
                    }

                    // 2. 마스터 저장이 성공했거나 저장할 필요가 없을 때 디테일 저장
                    if (masterSaveSuccess) 
                    {
                        DataTable detailDt = (DataTable)gcCodeMemberDetail.DataSource;
                        if (detailDt != null)
                        {
                            string currentMastID = gvCodeMemberMaster.GetFocusedRowCellValue("MastID")?.ToString() ?? "";

                            codeMember.CorpId = WrGlobal.CorpID;
                            codeMember.MastID = currentMastID;

                            var empNoGroups = detailDt.AsEnumerable()
                                .Where(row => row.RowState != DataRowState.Deleted &&
                                                !string.IsNullOrEmpty(row["EmpNo"]?.ToString()?.Trim()))
                                .GroupBy(row => row["EmpNo"]?.ToString()?.Trim())
                                .Where(g => g.Count() > 1)
                                .ToList();

                            if (empNoGroups.Any())
                            {
                                string duplicateEmpNos = string.Join(", ", empNoGroups.Select(g => g.Key));
                                MessageBox.Show($"중복된 사원번호가 있습니다: {duplicateEmpNos}", "중복 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }


                            //codeMember.DelAll_CodeMemberDetail();

                            foreach (DataRow row in detailDt.Rows)
                            {
                                if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                                {
                                    string empNo = row["EmpNo"]?.ToString()?.Trim() ?? "";

                                    if (string.IsNullOrEmpty(empNo))
                                    {
                                        MessageBox.Show("사원번호는 필수입니다.", "검증오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    codeMember.CorpId = row["CorpId"].ToString();
                                    codeMember.MastID = currentMastID;
                                    codeMember.EmpNo = row["EmpNo"]?.ToString() ?? "";
                                    codeMember.WorkJob = row["WorkJob"]?.ToString() ?? "";
                                    codeMember.WorkType = row["WorkType"]?.ToString() ?? "";
                                    codeMember.DetailRemark = row["Remark"]?.ToString() ?? "";

                                    if (!codeMember.InsUpd_CodeMemberDetail())
                                    {
                                        MessageBox.Show("디테일 저장 중 오류가 발생했습니다.", "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DisplayCategory();
                    DisplayProject();
                }
            }
            catch (Exception ex)
            {
                StackTrace stackTrace = new StackTrace();
                MessageBox.Show(ex.ToString(), "저장 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmdDelete_ElementClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 자료를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {
                    // IsFocusedView 사용
                    if (gvCodeMemberDetail.IsFocusedView)
                    {
                        // 디테일 삭제
                        int rowHandle = gvCodeMemberDetail.FocusedRowHandle;
                        if (rowHandle < 0)
                        {
                            MessageBox.Show("선택된 행이 없습니다.");
                            return;
                        }

                        string mastID = gvCodeMemberDetail.GetRowCellValue(rowHandle, "MastID")?.ToString() ?? "";
                        string empNo = gvCodeMemberDetail.GetRowCellValue(rowHandle, "EmpNo")?.ToString() ?? "";

                        if (!string.IsNullOrEmpty(empNo))
                        {
                            codeMember.CorpId = WrGlobal.CorpID;
                            codeMember.MastID = mastID;
                            codeMember.EmpNo = empNo;
                            codeMember.Del_CodeMemberDetail();
                            DisplayProject();
                        }
                    }
                    else if (gvCodeMemberMaster.IsFocusedView)
                    {
                        // 마스터 삭제
                        int rowHandle = gvCodeMemberMaster.FocusedRowHandle;
                        if (rowHandle < 0)
                        {
                            MessageBox.Show("선택된 행이 없습니다.");
                            return;
                        }

                        string mastID = gvCodeMemberMaster.GetRowCellValue(rowHandle, "MastID")?.ToString() ?? "";

                        if (!string.IsNullOrEmpty(mastID))
                        {
                            if (MessageBox.Show("해당 그룹에 속한 모든 인원 정보가 함께 삭제됩니다. 계속하시겠습니까?", "확인", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                codeMember.CorpId = WrGlobal.CorpID;
                                codeMember.MastID = mastID;
                                codeMember.Del_CodeMemberMaster();
                                DisplayCategory();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("삭제할 데이터를 먼저 선택하세요.");
                        return;
                    }

                    MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "삭제 에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            //if (MessageBox.Show("선택한 자료를 삭제 하시겠습니까?", "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
            //{
            //    try
            //    {
            //        codeMemberDetailBindingSource.RemoveCurrent();
            //        codeMemberDetailTableAdapter.Update(codeDataSet.CodeMemberDetail);
            //        MessageBox.Show("삭제되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }//end try
            //}//end if
        }

        private void cmdClose_ElementClick(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView gv = sender as GridView;
            gv.SetRowCellValue(e.RowHandle, "CorpId", WrGlobal.CorpID);
            gv.SetRowCellValue(e.RowHandle, "MastID", codeMember.GetNextMastID());
        }



        private void gridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView gv = sender as GridView;
            string mastID = gvCodeMemberMaster.GetFocusedRowCellValue("MastID")?.ToString() ?? "";
            gv.SetRowCellValue(e.RowHandle, "CorpId", WrGlobal.CorpID);
            gv.SetRowCellValue(e.RowHandle, "MastID", mastID);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayProject();
        }

    }
}