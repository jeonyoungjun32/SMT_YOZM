using DevExpress.CodeParser;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using SmartCommon.Common;
using System;
using System.Data;

namespace SmartDesign.View.Project
{
    public partial class frmDesignPlanDetail : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";
        string projectID = "";
        string pjID = "";
        string crud = "";

        public frmDesignPlanDetail()
        {
            InitializeComponent();
        }

        public frmDesignPlanDetail(string crud, string projectID, string pjID)
        {
            InitializeComponent();

            this.crud = crud;
            this.projectID = projectID;
            this.pjID = pjID;

            query = "select ItemCode 코드, Title 진행사항 from CmCateItem where CorpId = '{WrGlobal.CorpID}' and class like 'A0PJ10%'";
            lookupStatus.Properties.DataSource = db.GetDataView("진행사항",query);
            lookupStatus.Properties.ValueMember = "코드";
            lookupStatus.Properties.DisplayMember = "진행사항";

            query = $"select EmployeeId 사번, EmployeeName 이름 from CodeUser Where CorpID = '{WrGlobal.CorpID}'";
            searchCharge.Properties.DataSource = db.GetDataView("직원", query);
            searchCharge.Properties.DisplayMember = "이름";
            searchCharge.Properties.ValueMember = "사번";

            query = "select ItemCode 코드, Title 내용 from CmCateItem where CorpId = '{WrGlobal.CorpID}' and class = 'A0PJ20'";
            searchJobType.Properties.DataSource = db.GetDataView("작업구분", query);
            searchJobType.Properties.DisplayMember = "내용";
            searchJobType.Properties.ValueMember = "코드";


            projectMasterTableAdapter.FillByKey(this.designDataSet.ProjectMaster, WrGlobal.CorpID, projectID);
            if (crud == "Insert")
            {
                projectDetailBindingSource.AddNew();
                DataRowView item = (DataRowView)projectDetailBindingSource.Current;
                item["CorpID"] = WrGlobal.CorpID;
                item["ProjectID"] = projectID;
                item["PjID"] = pjID + "01";
                item["StartDate"] = DateTime.Now.Date;
                item["EndDate"] = DateTime.Now.Date;
                item["PlanStartDate"] = DateTime.Now.Date;
                item["PlanEndDate"] = DateTime.Now.Date;
                item["DateType"] = "D";
                item["Status"] = "P";
                item["Charge"] = WrGlobal.LoginID;

                projectDetailBindingSource.EndEdit();


            }
            else
            {
                projectDetailTableAdapter.FillByList(this.designDataSet.ProjectDetail, WrGlobal.CorpID, projectID, pjID);
            }
            projectMemoTableAdapter.FillByList(this.designDataSet.ProjectMemo, WrGlobal.CorpID, projectID, pjID);


        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {

            //projectMasterBindingSource.EndEdit();
            //projectMasterTableAdapter.Update(this.designDataSet.ProjectMaster);

            DataRowView item = (DataRowView)projectDetailBindingSource.Current;
            int days = ((DateTime)endDateDateEdit.EditValue  - (DateTime)startDateDateEdit.EditValue).Days + 1;
            item["WorkDays"] = days;
            projectDetailBindingSource.EndEdit();
            projectDetailTableAdapter.Update(this.designDataSet.ProjectDetail);

            this.Close();
        }

        private void frmDesignPlanDetail_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRowView drvDetail = (DataRowView)projectDetailBindingSource.Current;

            DataRowView drv = (DataRowView)projectMemoBindingSource.Current;
            drv["WriteDate"] = DateTime.Now;
            drv["WriteID"] = WrGlobal.LoginID;
            drv["Body"] = memoEdit1.Text;

            projectMemoBindingSource.EndEdit();
            projectMemoTableAdapter.Update(this.designDataSet.ProjectMemo);

            projectMemoTableAdapter.FillByList(this.designDataSet.ProjectMemo, WrGlobal.CorpID, projectID, pjID);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            projectMemoBindingSource.AddNew();
            DataRowView drv = (DataRowView)projectMemoBindingSource.Current;

            drv["CorpID"] = WrGlobal.CorpID;
            drv["ProjectID"] = projectID;
            drv["PjID"] = pjID;
            drv["WriteDate"] = DateTime.Now;
            drv["WriteID"] = WrGlobal.LoginID;
            drv["Body"] = "";
            projectMemoBindingSource.EndEdit();

        }
    }
}