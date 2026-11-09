using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebViewer.View.Systems
{
    public partial class AuthorGroup : System.Web.UI.Page
    {
        protected string pstrMenuId = "";
        protected clsDatabase db = new clsDatabase();
        protected string query = "";
        protected string pstrErrMsg = "";

        protected string pstrCmd = "";

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
        }

        protected void GroupGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            query = string.Format("SELECT CONVERT(NVARCHAR(4), ISNULL(MAX(GroupCode), 1000) + 1) GroupCode FROM CodeAuthorGroup Where CorpId = '{0}'", Session["CorpId"]);
            DataRowView drv = db.GetDataRecord(query);

            if (drv != null)
            {
                e.NewValues["GroupCode"] = drv["GroupCode"].ToString();
            }
            else
            {
                GroupGrid.JSProperties["cpMsg"] = "그룹 등록중 오류가 발생했습니다.";
                e.Cancel = true;
                return;
            }

        }

        protected void DetailGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string param = e.Parameters;

            if (param != null)
            {
                string[] arr = param.Split('|');
                if (arr[0] == "FocusdRowChanged")
                {
                    query = string.Format("exec Code_GetCodeAuthorByGroupCode '{0}', '{1}'",
                                        Session["CorpId"], arr[1]);
                    DetailGrid.DataSource = db.GetDataView("CodeAuthorGroupDetail", query);
                    DetailGrid.DataBind();
                }
            }


        }

        protected void DetailGrid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            foreach (DevExpress.Web.Data.ASPxDataUpdateValues args in e.UpdateValues)
            {
                if (args.NewValues["AuthorYn"].ToString() == "Y")
                {
                    query = string.Format("Insert Into CodeAuthorGroupDetail(CorpId, GroupCode, MenuID, AuthorCode, UseYn, CreId, CreDt) Values("
                                + "'{0}', '{1}', '{2}', '{3}', 'Y', '{4}', GETDATE())",
                                Session["CorpId"], args.Keys[0].ToString(), args.Keys[1].ToString(), args.Keys[2].ToString(), Session["EmpNo"]);
                }
                else
                {
                    query = string.Format("Delete From CodeAuthorGroupDetail Where CorpId = '{0}' and GroupCode = '{1}' and MenuID = '{2}' and AuthorCode = '{3}'",
                                Session["CorpId"], args.Keys[0].ToString(), args.Keys[1].ToString(), args.Keys[2].ToString());
                }

                db.ExecuteQuery(query);
            }

            e.Handled = true;
            e.DeleteValues.Clear();
            e.InsertValues.Clear();
            e.UpdateValues.Clear();

            DetailGrid.JSProperties["cpMsg"] = "저장되었습니다.";
        }
    }
}