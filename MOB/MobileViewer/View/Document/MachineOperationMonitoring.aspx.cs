using DevExpress.Web;
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
    public partial class MachineOperationMonitoring : System.Web.UI.Page
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
                pstrMenuId = Request.QueryString["id"] ?? "";

                DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));
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
                if (drv["FieldName"].ToString() == "Status")
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
    }
}

//public class GridViewImageTemplate : ITemplate
//{
//    public void InstantiateIn(Control container)
//    {
//        // ASP.NET WebControl의 Image 컨트롤 사용
//        Image img = new Image();
//        img.Height = 15;

//        // GridView의 각 행의 데이터를 바인딩할 때 발생하는 이벤트
//        img.DataBinding += (sender, e) =>
//        {
//            Image image = (Image)sender;
//            GridViewDataItemTemplateContainer templateContainer = (GridViewDataItemTemplateContainer)image.NamingContainer;
//            int columnValue = int.Parse(
//                string.IsNullOrEmpty(DataBinder.Eval(templateContainer.DataItem, "Status")?.ToString())
//                    ? "0"
//                    : DataBinder.Eval(templateContainer.DataItem, "Status").ToString()
//            );

//            // 조건에 따라 이미지를 설정
//            if (columnValue > 0)
//            {
//                image.ImageUrl = "~/Images/BlueCircle.png"; // Y일 때 이미지 1
//            }
//            else
//            {
//                image.ImageUrl = "~/Images/RedCircle.png"; // N일 때 이미지 2
//            }
//        };

//        // 컨테이너에 이미지 컨트롤 추가
//        container.Controls.Add(img);
//    }
//}
