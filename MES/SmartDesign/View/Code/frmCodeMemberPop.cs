using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

using SmartCommon.Common;

using SmartDesign.Model;

namespace SmartDesign.View.Codes
{
    public partial class frmCodeMemberPop : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query;
        CodeMember codeMember = new CodeMember();
        clsRepository repo = new clsRepository();
        ProjectMaster pm = new ProjectMaster();

        public delegate void SetData(string selClass);  //델리게이트 선언
        public event SetData onSetData;                 //계약

        public frmCodeMemberPop()
        {
            InitializeComponent();
        }

        private void frmCodeMemberPop_Load(object sender, EventArgs e)
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
        }

        private void cmdDisplay_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
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

        // 선택
        private void cmdSelect_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            string mastID = gvCodeMemberMaster.GetFocusedRowCellValue("MastID")?.ToString() ?? "";

            if (mastID == "")
            {
                MessageBox.Show("등록할 항목을 선택해 주세요.");
                return;
            }

            onSetData(mastID);
            this.Close();
        }

        private void cmdClose_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            this.Close();
        }


        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayProject();
        }
    }
}