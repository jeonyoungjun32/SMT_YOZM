using DevExpress.Web.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MobileViewer.View
{
    public partial class Main : System.Web.UI.Page
    {
        protected string pstrAuthor = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["EmpNo"] ?? "").ToString() == "")
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                //txtCorpName.InnerText = (Session["CorpName"] ?? "").ToString();
                //txtEmpName.InnerText = (Session["EmpName"] ?? "").ToString();

                //메뉴 VISIBLE 설정
                List<clsAuthor> readAuthor = (List<clsAuthor>)Session["AuthorRead"];

                List<string> allowedMenus = new List<String>();

                if (readAuthor != null)
                {
                    readAuthor.Select(x => x.ParamText).Distinct().ToList();
                }

                pstrAuthor = string.Join("|", allowedMenus);

                // 메인 메뉴 버튼, 좌측/하단 메뉴 바인딩
                rptMainMenu.DataSource = readAuthor;
                rptMainMenu.DataBind();

                //rptSideMenu.DataSource = readAuthor;
                //rptSideMenu.DataBind();
            }
        }

        protected void rptMainMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // 메뉴 아이콘 설정하는 부분
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BootstrapButton btn = (BootstrapButton)e.Item.FindControl("btnMenu");
                clsAuthor author = (clsAuthor)e.Item.DataItem;

                btn.CssClasses.Icon = $"bi bi {author.WebImageKey} btn_icon_mainmenu";
            }
        }
    }
}