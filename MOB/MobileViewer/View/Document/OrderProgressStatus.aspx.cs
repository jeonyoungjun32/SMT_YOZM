using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileViewer.View
{
    public partial class OrderProgressStatus : System.Web.UI.Page
    {
        protected string pstrCmd = "";
        protected string pstrMenuId = "";
        protected string pstrPrjId = "";

        protected clsDatabase db = new clsDatabase();
        protected string query = "";
        protected string pstrErrMsg = "";

        protected string pstrAuthor = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["EmpNo"] == null || Session["EmpNo"].ToString() == "")
            {
                try
                {
                    HttpContext.Current.Response.Redirect("~/View/SessionError.aspx");
                }
                catch (ApplicationException)
                {
                    HttpContext.Current.Response.RedirectLocation =
                                         System.Web.VirtualPathUtility.ToAbsolute("~/View/SessionError.aspx");
                }
            }


            if (!IsPostBack)
            {
                pstrPrjId = Request.QueryString["pid"] ?? "";

                //txtCorpName.InnerText = (Session["CorpName"] ?? "").ToString();
                //txtEmpName.InnerText = (Session["EmpName"] ?? "").ToString();

                List<clsAuthor> AuthorList = (List<clsAuthor>)Session["Author"];

                if (AuthorList.Exists(s => s.MenuID == "11706" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "11706";
                }

                if (AuthorList.Exists(s => s.MenuID == "11707" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "11707";
                }

                if (AuthorList.Exists(s => s.MenuID == "11708" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "11708";
                }

                if (AuthorList.Exists(s => s.MenuID == "11709" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "11709";
                }

                if (AuthorList.Exists(s => s.MenuID == "11710" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "11710";
                }

                if (AuthorList.Exists(s => s.MenuID == "11711" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "11711";
                }

                if (AuthorList.Exists(s => s.MenuID == Request.QueryString["id"] && s.AuthorCode == "02"))
                {   // 이미지 보기
                }

                SetGrid();
                SetComBobox();
                pstrMenuId = Request.QueryString["id"] ?? "";

                txtCorpId.Text = Session["CorpId"].ToString();
                DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));

                startDate_Stored.Date = startDate;
                endDate_Stored.Date = endDate;
            }
        }

        protected void SetGrid()
        {
            DocGrid.Columns.Clear();

            int colIdx = 0;

            query = string.Format("select * from MB_ProjectField where ProjectId = {0} order by DisplayOrder", Request.QueryString["pid"]);
            DataView dv = db.GetDataView("ProjectField", query);

            foreach (DataRowView drv in dv)
            {
                BootstrapGridViewTextColumn col = new BootstrapGridViewTextColumn();
                col.FieldName = drv["FieldName"].ToString();
                col.Caption = drv["ShortCaption"].ToString();
                col.Width = new Unit(Convert.ToInt32(drv["ListWidth"]) * 6);
                col.VisibleIndex = colIdx++;
                col.Visible = Convert.ToString(drv["VisibleYN"]) == "Y";

                string fieldType = Convert.ToString(drv["FieldType"]).ToUpper();

                if (fieldType == "INT")
                {
                    col.PropertiesEdit.DisplayFormatString = "{0:#,0}";
                }
                else if (fieldType == "FLOAT1")
                {
                    col.PropertiesEdit.DisplayFormatString = "{0:#,0.0}";
                }
                else if (fieldType == "FLOAT2")
                {
                    col.PropertiesEdit.DisplayFormatString = "{0:#,0.00}";
                }

                DocGrid.Columns.Add(col);
            }
        }

        protected void SetComBobox()
        {
            cboCategory.Items.Clear();
            cboStatus.Items.Clear();

            cboCategory.Items.Add("지시일자");

            cboCategory.DataBind();
            cboCategory.SelectedIndex = 0;

            query = $@"SELECT 'All' 코드, '전체' 내용
                                  Union All
                                  SELECT 'W' 코드, '대기' 내용
                                  Union All
                                  SELECT 'R' 코드, '진행' 내용
                                  Union All
                                  SELECT 'C' 코드, '완료' 내용";

            DataView dv = db.GetDataView("거래처", query);

            foreach (DataRowView item in dv)
            {
                cboStatus.Items.Add(item["내용"].ToString(), item["코드"].ToString());
            }

            cboStatus.DataBind();
            cboStatus.SelectedIndex = 0;
        }
    }
}