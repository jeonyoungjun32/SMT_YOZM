using System;
using System.Collections.Generic;
using System.Web.UI;

namespace MobileViewer.View.CommonCtrl
{
    public partial class SideMenu : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtEmpName.InnerText = (Session["EmpName"] ?? "").ToString();
                txtCorpName.InnerText = (Session["CorpName"] ?? "").ToString();

                List<clsAuthor> readAuthor = Session["AuthorRead"] as List<clsAuthor>;

                rptSideMenu.DataSource = readAuthor;
                rptSideMenu.DataBind();
            }
        }
    }
}