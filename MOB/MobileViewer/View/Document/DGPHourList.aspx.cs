using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

namespace MobileViewer.View
{
    public partial class DGPHourList : System.Web.UI.Page
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

                SetComBobox();
                SetGrid();

                pstrMenuId = Request.QueryString["id"] ?? "";

                txtCorpId.Text = Session["CorpId"].ToString();

				DocGrid.DataBind();
            }
        }

        protected void SetGrid()
        {
            DocGrid.Columns.Clear();


            int colIdx = 0;

         
            //col.FieldName = "RowNum";
            //col.Caption = "No";
            //col.Width = 5;
            //col.VisibleIndex = colIdx++;
            //DocGrid.Columns.Add(col);


            query = string.Format("select * from MB_ProjectField where ProjectId = {0} order by DisplayOrder", Request.QueryString["pid"]);
            DataView dv = db.GetDataView("ProjectField", query);

            foreach (DataRowView drv in dv)
            {
                if(drv["FieldName"].ToString() == "Counter")
                {
                    BootstrapGridViewDataColumn col = new BootstrapGridViewDataColumn();
                    //col.CssClasses.HeaderCell = "gridCol";
                    col.DataItemTemplate = new GridViewImageTemplate();
                    col.FieldName = drv["FieldName"].ToString();
                    col.Caption = drv["ShortCaption"].ToString();
                    col.Width = new Unit(Convert.ToInt32(drv["ListWidth"]) * 6);
                    col.VisibleIndex = colIdx++;
                    col.Visible = Convert.ToString(drv["VisibleYN"]) == "Y";

                    DocGrid.Columns.Add(col);
                }
                else
                {
                    BootstrapGridViewTextColumn col = new BootstrapGridViewTextColumn();
                    col.CssClasses.HeaderCell = "gridCol";
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
        }

        protected void SetComBobox()
        {
            //cboDocument_Type.Items.Clear();
            ////cboDocument_Type.SelectedIndex = 0;

            //query = string.Format("Select * from MB_ProjectFieldOptionData order by FieldOptionID, FieldDataID");
            //DataView dv = db.GetDataView("ProjectFieldOptionData", query);

            //foreach (DataRowView item in dv)
            //{
            //    if (Convert.ToInt32(item["FieldOptionID"]) == 30)
            //    {
            //        cboDocument_Type.Items.Add(item["FieldDataText"].ToString());
            //    }
            //}
        }
	}
}
