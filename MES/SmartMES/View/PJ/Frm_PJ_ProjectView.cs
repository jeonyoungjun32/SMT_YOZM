using System;
using baseSMT;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;

namespace SmartMES.View.PJ
{
    public partial class Frm_PJ_ProjectView : DevExpress.XtraEditors.XtraForm
    {
        clsRepository repo = new clsRepository();
        clsGrid grid = new clsGrid();

        string YYYYMM = "";  // 년월

        RepositoryItemSearchLookUpEdit lup_Material;  // 품명

        public Frm_PJ_ProjectView()
        {
            InitializeComponent();

            // 디자인
            Set_Design();

            // 조회
            Func_Search();
        }

        public Frm_PJ_ProjectView(string yyyyMM)
        {
            InitializeComponent();

            this.YYYYMM = yyyyMM;

            // 디자인
            Set_Design();

            // 조회
            Func_Search();
        }

        #region 디자인
        private void Set_Design()
        {
            // 날짜
            Set_Dt();

            // 기본값 설정
            Set_BasicInfo();

            // 그리드
            Set_GvPlanDetail(Gv_Project);
        }

        private void Set_Dt()
        {
            Dt_YYYY.smtStartDateTimeValue = DateTime.Now.Date;
        }

        private void Set_BasicInfo()
        {
            this.YYYYMM = Dt_YYYY.smtStartDateTimeValue.ToString("yyyy");
        }

        #region 그리드
        private void Set_GvPlanDetail(GridView gv)
        {
            //Grid Setting
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.OptionsView.ShowAutoFilterRow = true; // 자동 필터 표시
            gv.OptionsView.ShowGroupPanel = false; // 그룹 패널 숨김
            gv.OptionsView.ShowIndicator = false;


            ProjectRepository projectRepo = new ProjectRepository();
            RepositoryItemSearchLookUpEdit lup_Charge = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_PM_Charge(), "직원코드", "직원명");
            RepositoryItemSearchLookUpEdit lup_Material = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_Material(), "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_Status = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_Status(), "코드", "내용");
            //lup_Material = repo.Create_RepositoryItemSearchLookUp(projectRepo.Select_MaterialGJY(), "품목코드", "품명");

            grid.Create_GvColumn(gv, "프로젝트ID", "ProjectID");
            grid.Create_GvColumn(gv, "프로젝트명", "ProjectName");
            grid.Create_GvColumn(gv, "품명", "ItemCode", lup_Material);
            grid.Create_GvColumn(gv, "품목수량", "Qty");
            grid.Create_GvColumn(gv, "담당자", "Charge", lup_Charge);
            grid.Create_GvColumn(gv, "상태", "Status", lup_Status);
            grid.Create_GvColumn(gv, "납기준수율", "DeliveryComplianceRate");
            grid.Create_GvColumn(gv, "진척율", "ProgressRate");
            grid.Create_GvColumn(gv, "절차수량", "ProcessCount");
            grid.Create_GvColumn(gv, "지연수량", "DelayCount");
            grid.Create_GvColumn(gv, "시작일자", "StartDate");
            grid.Create_GvColumn(gv, "목표종료일자", "PlanEndDate");
            grid.Create_GvColumn(gv, "완료일자", "EndDate");

            //VISIBLE
            gv.Columns["ProjectID"].Visible = false;
            gv.Columns["DeliveryComplianceRate"].Visible = false;
            gv.Columns["ProgressRate"].Visible = false;
            gv.Columns["ProcessCount"].Visible = false;
            gv.Columns["DelayCount"].Visible = false;
            gv.Columns["EndDate"].Visible = false;


            // 포맷
            grid.SetCol_Type(gv.Columns["Qty"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["ProcessCount"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["DelayCount"], clsGrid.TypeList.TInt);

            grid.SetCol_Type(gv.Columns["StartDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["PlanEndDate"], clsGrid.TypeList.TDate);
            grid.SetCol_Type(gv.Columns["EndDate"], clsGrid.TypeList.TDate);

        }

        #endregion 그리드 END
        #endregion

        #region CRUD
        #region 조회
        private void Func_Search()
        {
            Set_BasicInfo();

            ProjectMaster pm = new ProjectMaster();
            pm.PlanYYYYMM = YYYYMM;

            Gc_Project.DataSource = pm.Select_ProjectDetailView();

            Gv_Project.BestFitColumns();
        }
        #endregion 조회 END

        #endregion

        #region 버튼이벤트
        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;

            switch (btn.Text)
            {
                case "조회":
                    Func_Search();
                    break;

                case "엑셀변환":
                    ExcelExportHelper.CreateMultipleExport(smtCommonBar1.smtImageTitleText, "Excel Worksheets|*.xls",
                    ("계획정보", Gv_Project)
                    ).Export();
                    break;

                case "닫기":
                    this.Close();
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}