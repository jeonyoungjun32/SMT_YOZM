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
    public partial class ProcessingInspection : System.Web.UI.Page
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
                //txtTable.Text = Request.QueryString["table"];
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

                txtCorpId.Text = Session["CorpId"].ToString();

                pstrMenuId = Request.QueryString["id"] ?? "";

                startDate_Stored.Date = DateTime.Now.Date.AddDays(-7);
                endDate_Stored.Date = DateTime.Now.Date;
            }
        }

        protected void SetGrid()
        {
            //DocGrid.Columns.Clear();

            int colIdx = 0;

            BootstrapGridViewTextColumn col = new BootstrapGridViewTextColumn();
            col.CssClasses.HeaderCell = "gridCol";
            col.FieldName = "RowNum";
            col.Caption = "No";
            col.Width = 5;
            col.VisibleIndex = colIdx++;
            DocGrid.Columns.Add(col);


            query = string.Format("select * from MB_ProjectField where ProjectId = {0} order by DisplayOrder", Request.QueryString["pid"]);
            DataView dv = db.GetDataView("ProjectField", query);

            foreach (DataRowView drv in dv)
            {
                col = new BootstrapGridViewTextColumn();
                //col.CssClasses.HeaderCell = "gridCol";
                col.FieldName = drv["FieldName"].ToString();
                col.Caption = drv["ShortCaption"].ToString();
                col.Width = new Unit(Convert.ToInt32(drv["ListWidth"]) * 6);
                col.VisibleIndex = colIdx++;
                col.Visible = Convert.ToString(drv["VisibleYN"]) == "Y" ;

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
                    col.PropertiesEdit.DisplayFormatString = "d";
                }

                DocGrid.Columns.Add(col);
            }

            //검사버튼 추가
            BootstrapGridViewCommandColumn col_btn = new BootstrapGridViewCommandColumn();

            BootstrapGridViewCommandColumnCustomButton btn = new BootstrapGridViewCommandColumnCustomButton();
            btn.IconCssClass = "btn-search";
            //btn.CssClass = ".bg-facebook";
            btn.ID = "btnAdd";
            btn.Text = "검사내역";

            col_btn.CustomButtons.Add(btn);
            col_btn.Name = "col_btn";
            col_btn.Caption = " ";
            col_btn.Width = new Unit(3 * 6);
            col_btn.HorizontalAlign = HorizontalAlign.Center;

            DocGrid.Columns.Add(col_btn);
            DocGrid.CustomButtonCallback += DocGrid_CustomButtonCallback;

            //BootstrapGridViewButtonEditColumn btn = new BootstrapGridViewButtonEditColumn();
            //btn.Name = "btn_Info";
            //btn.FieldName = "btn_Info";
            //btn.PropertiesButtonEdit.ClearButton.DisplayMode = DevExpress.Web.ClearButtonDisplayMode.Never;
            //btn.PropertiesButtonEdit.Buttons.Add(new BootstrapEditButton() { IconCssClass = "fa fa-sync" });
            //btn.Width = 10;
            //btn.MaxWidth = 10;
            //DocGrid.Columns.Add(btn);
        }

        private void DocGrid_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            // 클릭된 행의 키 값 가져오기
            int id = (int)((BootstrapGridView)sender).GetRowValues(e.VisibleIndex, "ID");

            // 필요한 로직 수행 (예: 다른 페이지로 리다이렉트)
            Response.Redirect($"DetailsPage.aspx?ID={id}");
        }

        protected void SetComBobox()
        {
            cboLine.Items.Clear();
            //cboDocument_Type.SelectedIndex = 0;

            query = $"SELECT LineCode, LineName FROM CM_Line WHERE CorpId= '{Session["CorpId"]}' ";
            DataView dv = db.GetDataView("Cust", query);

            foreach (DataRowView item in dv)
            {
                //if (Convert.ToInt32(item["FieldOptionID"]) == 30)
                {
                    cboLine.Items.Add(item["LineName"].ToString(), item["LineCode"]);
                }
            }

            cboProcess.Items.Clear();
            //cboDocument_Type.SelectedIndex = 0;

            query = $"SELECT ProcessCode, ProcessName FROM CM_Process WHERE CorpId= '{Session["CorpId"]}' ";
            dv = db.GetDataView("Cust", query);

            foreach (DataRowView item in dv)
            {
                cboProcess.Items.Add(item["ProcessName"].ToString(), item["ProcessCode"]);
            }
            cboProcess.SelectedIndex = 0;

        }
    }
}