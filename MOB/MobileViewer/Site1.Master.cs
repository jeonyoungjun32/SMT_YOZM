using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileViewer.Controls
{
	public partial class Site1 : System.Web.UI.MasterPage
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

                List<clsAuthor> AuthorList = (List<clsAuthor>)Session["Author"];

                if (AuthorList.Exists(s => s.MenuID == "10706" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "10706";
                }

                if (AuthorList.Exists(s => s.MenuID == "10707" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "10707";
                }

                if (AuthorList.Exists(s => s.MenuID == "10708" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "10708";
                }

                if (AuthorList.Exists(s => s.MenuID == "10709" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "10709";
                }

                if (AuthorList.Exists(s => s.MenuID == "10710" && s.AuthorCode == "01"))
                {
                    if (pstrAuthor != "") pstrAuthor += "|";
                    pstrAuthor += "10710";
                }

                //if (AuthorList.Exists(s => s.MenuID == "11001" && s.AuthorCode == "01"))
                //{
                //    if (pstrAuthor != "") pstrAuthor += "|";
                //    pstrAuthor += "11001";
                //}
                //if (AuthorList.Exists(s => s.MenuID == "11002" && s.AuthorCode == "01"))
                //{
                //    if (pstrAuthor != "") pstrAuthor += "|";
                //    pstrAuthor += "11002";
                //}
                //if (AuthorList.Exists(s => s.MenuID == "11003" && s.AuthorCode == "01"))
                //{
                //    if (pstrAuthor != "") pstrAuthor += "|";
                //    pstrAuthor += "11003";
                //}
                //if (AuthorList.Exists(s => s.MenuID == "11004" && s.AuthorCode == "01"))
                //{
                //    if (pstrAuthor != "") pstrAuthor += "|";
                //    pstrAuthor += "11004";
                //}
                //if (AuthorList.Exists(s => s.MenuID == "11005" && s.AuthorCode == "01"))
                //{
                //    if (pstrAuthor != "") pstrAuthor += "|";
                //    pstrAuthor += "11005";
                //}
                //if (AuthorList.Exists(s => s.MenuID == "11006" && s.AuthorCode == "01"))
                //{
                //    if (pstrAuthor != "") pstrAuthor += "|";
                //    pstrAuthor += "11006";
                //}
                //if (AuthorList.Exists(s => s.MenuID == "11007" && s.AuthorCode == "01"))
                //{
                //    if (pstrAuthor != "") pstrAuthor += "|";
                //    pstrAuthor += "11007";
                //}
            }
        }
	}
}