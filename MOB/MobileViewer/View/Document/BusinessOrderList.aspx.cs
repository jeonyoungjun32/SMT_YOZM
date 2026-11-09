using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileViewer.View.Document
{
    public partial class BusinessOrderList : System.Web.UI.Page
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
                pstrMenuId = Request.QueryString["id"] ?? "";

                //txtCorpName.InnerText = (Session["CorpName"] ?? "").ToString();
                //txtEmpName.InnerText = (Session["EmpName"] ?? "").ToString();

                //메뉴 설정
                SetMenu();

                SetGrid();
                SetComBobox();

                txtCorpId.Text = Session["CorpId"].ToString();
                DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));

                startDate_Stored.Date = startDate;
                endDate_Stored.Date = endDate;

                SqlDataSource1.SelectParameters["pCorID"].DefaultValue = Session["CorpId"].ToString();
                SqlDataSource1.SelectParameters["pStartDate"].DefaultValue = startDate.ToString("yyyyMMdd");
                SqlDataSource1.SelectParameters["pEndDate"].DefaultValue = endDate.ToString("yyyyMMdd");
                //SqlDataSource1.SelectParameters["pMaterialCode"].DefaultValue = "ALL";
                SqlDataSource1.SelectParameters["pCust"].DefaultValue = "ALL";

                DocGrid.DataBind();
            }
        }

        protected void SetMenu()
        {
            //좌측/하단 메뉴 설정

            List<clsAuthor> readAuthor = (List<clsAuthor>)Session["AuthorRead"];

            List<string> allowedMenus = new List<String>();

            if (readAuthor != null)
            {
                readAuthor.Select(x => x.ParamText).Distinct().ToList();
            }

            pstrAuthor = string.Join("|", allowedMenus);

            //rptSideMenu.DataSource = readAuthor;
            //rptSideMenu.DataBind();
        }

        protected void SetGrid()
        {
            clsGrid.CreateColumn(DocGrid, pstrPrjId);
        }

        protected void SetComBobox()
        {
            cboCategory.Items.Clear();
            cboCust.Items.Clear();

            cboCategory.Items.Add("수주일자");

            cboCategory.DataBind();
            cboCategory.SelectedIndex = 0;

            query = $@"exec SMT_LookUpEditGet '거래처','','','','전체','Y'";

            DataView dv = db.GetDataView("거래처", query);

            foreach (DataRowView item in dv)
            {
                cboCust.Items.Add(item["거래처명"].ToString(), item["거래처코드"].ToString());
            }

            cboCust.DataBind();
            cboCust.SelectedIndex = 0;
        }

        //protected void DocGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
        //{
        //    if (e.Column.FieldName == "CustCode")
        //    {
        //        DataTable dt = Session["CustLookup"] as DataTable;
        //        if (dt == null)
        //        {
        //            string query = @"exec SMT_LookUpEditGet '거래처','','','','','Y'";
        //            dt = db.GetDataTable(query);
        //            Session["SoldToLookup"] = dt;
        //        }
        //        DataRow[] drs = dt.Select("거래처코드 = '" + e.Value.ToString() + "'");
        //        if (drs.Length > 0)
        //            e.DisplayText = drs[0]["거래처명"].ToString();
        //    }
        //}
    }
}