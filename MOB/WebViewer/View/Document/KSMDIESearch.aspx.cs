using DevExpress.Export;
using DevExpress.Web.Bootstrap;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebViewer.View.Document
{
    public partial class KSMDIESearch : System.Web.UI.Page
    {
        protected string pstrMenuId = "";
        protected string pstrPrjId = "";

        protected clsDatabase db = new clsDatabase();
        protected string query = "";
        protected string pstrErrMsg = "";

        protected string pstrCmd = "";

        protected void Page_Init(object sender, EventArgs e)
        {

        }

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
                List<clsAuthor> AuthorList = (List<clsAuthor>)Session["Author"];

                if (AuthorList.Exists(s => s.MenuID == "11006" && s.AuthorCode == "02"))
                {   // 이미지 보기
                    DocGrid.Toolbars[0].Items[2].Visible = true;
                    txtViewAuthor.Text = "Y";
                }
                if (AuthorList.Exists(s => s.MenuID == "11006" && s.AuthorCode == "03"))
                {   // 파일다운로드
                    DocGrid.Toolbars[0].Items[3].Visible = true;
                }

                SetComBobox();
                SetGrid();

                txtTable.Text = Request.QueryString["table"];
                pstrMenuId = Request.QueryString["id"] ?? "";
                pstrPrjId = Request.QueryString["pid"] ?? "";
            }
        }

        protected void SetGrid()
        {
            DocGrid.Columns.Clear();

            int colIdx = 0;

            BootstrapGridViewCommandColumn chk = new BootstrapGridViewCommandColumn();
            chk.ShowSelectCheckbox = true;
            chk.Width = new Unit(Convert.ToInt32(40));
            chk.VisibleIndex = colIdx++;
            DocGrid.Columns.Add(chk);

            BootstrapGridViewTextColumn col = new BootstrapGridViewTextColumn();
            col.FieldName = "DocumentKey";
            col.Visible = false;
            col.VisibleIndex = colIdx++;
            DocGrid.Columns.Add(col);


            query = string.Format("select * from MB_ProjectField where ProjectId = {0} order by DisplayOrder", Request.QueryString["pid"]);
            DataView dv = db.GetDataView("ProjectField", query);

            foreach (DataRowView drv in dv)
            {
                col = new BootstrapGridViewTextColumn();
                col.FieldName = drv["FieldName"].ToString();
                col.Caption = drv["ShortCaption"].ToString();
                col.Width = new Unit(Convert.ToInt32(drv["ListWidth"]) * 10);
                col.VisibleIndex = colIdx++;
                DocGrid.Columns.Add(col);
            }

            col = new BootstrapGridViewTextColumn();
            col.FieldName = "FileCol";
            col.Visible = false;
            col.VisibleIndex = colIdx++;
            DocGrid.Columns.Add(col);

        }
        protected void SetComBobox()
        {
            cboActualSize.Items.Clear();
            cboDocument_Type.Items.Clear();
            cboUOM.Items.Clear();

            cboActualSize.Items.Add("");
            cboDocument_Type.Items.Add("");
            cboUOM.Items.Add("");

            cboActualSize.SelectedIndex = 0;
            cboDocument_Type.SelectedIndex = 0;
            cboUOM.SelectedIndex = 0;

            chkCurrentRevision.Checked = true;


            query = string.Format("Select * from MB_ProjectFieldOptionData order by FieldOptionID, FieldDataID");
            DataView dv = db.GetDataView("ProjectFieldOptionData", query);

            foreach (DataRowView item in dv)
            {
                switch (Convert.ToInt32(item["FieldOptionID"]))
                {
                    case 2:
                        cboActualSize.Items.Add(item["FieldDataText"].ToString());
                        break;
                    case 60:
                        cboDocument_Type.Items.Add(item["FieldDataText"].ToString());
                        break;
                    case 4:
                        cboUOM.Items.Add(item["FieldDataText"].ToString());
                        break;
                }
            }
        }

        protected void DocGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "download")
            {
                if (DocGrid.IsGroupRow(DocGrid.FocusedRowIndex)) { return; }
                ASPxWebControl.RedirectOnCallback(string.Format(@"DownFile.aspx?id={0}&pid={1}&table={2}&key={3}&name={4}"
                            , Request.QueryString["id"]
                            , Request.QueryString["pid"]
                            , txtTable.Text
                            , DocGrid.GetRowValues(DocGrid.FocusedRowIndex, "DocumentKey")
                            , DocGrid.GetRowValues(DocGrid.FocusedRowIndex, "FileCol")));
            }
        }

        protected void DocGrid_ToolbarItemClick(object sender, BootstrapGridViewToolbarItemClickEventArgs e)
        {
            if (e.Item.Name == "CustomExportToXLSX")
            {
                DocGrid.ExportXlsToResponse("재고리스트", new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
            }
        }
    }
}