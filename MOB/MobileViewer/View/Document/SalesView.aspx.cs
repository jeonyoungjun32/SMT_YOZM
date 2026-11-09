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
    public partial class SalesView : System.Web.UI.Page
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

                #region 좌측/하단 메뉴 설정

                //메뉴 VISIBLE 설정
                List<clsAuthor> readAuthor = (List<clsAuthor>)Session["AuthorRead"];

                List<string> allowedMenus = new List<String>();

                if (readAuthor != null)
                {
                    readAuthor.Select(x => x.ParamText).Distinct().ToList();
                }

                pstrAuthor = string.Join("|", allowedMenus);

                //좌측/하단 메뉴 바인딩
                //rptSideMenu.DataSource = readAuthor;
                //rptSideMenu.DataBind();

                #endregion

                SetGrid();
                SetComBobox();
                pstrMenuId = Request.QueryString["id"] ?? "";

                txtCorpId.Text = Session["CorpId"].ToString();
                DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
                DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
                endDate = endDate.AddDays(-1);

                startDate_Stored.Date = startDate;
                endDate_Stored.Date = endDate;

                SqlDataSource1.SelectParameters["pCorID"].DefaultValue = Session["CorpId"].ToString();
                SqlDataSource1.SelectParameters["pCategory"].DefaultValue = "출하일자";
                SqlDataSource1.SelectParameters["pStartDate"].DefaultValue = startDate.ToString("yyyyMMdd");
                SqlDataSource1.SelectParameters["pEndDate"].DefaultValue = endDate.ToString("yyyyMMdd");
                SqlDataSource1.SelectParameters["pCustCode"].DefaultValue = "ALL";

                DocGrid.DataBind();
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
                else if (fieldType == "DATE")
                {
                    col.PropertiesEdit.DisplayFormatString = "{0:yyyy-MM-dd}";
                }

                DocGrid.Columns.Add(col);
            }
        }

        protected void SetComBobox()
        {
            cboCategory.Items.Clear();
            cboCustomer.Items.Clear();

            cboCategory.Items.Add("출하일자");
            cboCategory.Items.Add("출하요청일자");

            cboCategory.DataBind();
            cboCategory.SelectedIndex = 0;

            query = string.Format("select a.코드, a.내용 from (select 1 Sorting, 'All' 코드, '전체' 내용 Union All "
                                                            + "select 2 Sorting, CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType in ('20','40')) a Order By a.Sorting, a.코드", Session["CorpId"]);

            DataView dv = db.GetDataView("거래처", query);

            foreach (DataRowView item in dv)
            {
                cboCustomer.Items.Add(item["내용"].ToString(), item["코드"].ToString());
            }

            cboCustomer.DataBind();
            cboCustomer.SelectedIndex = 0;
        }
    }
}