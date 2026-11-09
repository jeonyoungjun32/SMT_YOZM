using DevExpress.Export;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.Bootstrap;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebViewer.View.Systems
{
    public partial class CreateEmployee : System.Web.UI.Page
    {
        protected string pstrMenuId = "";
        protected clsDatabase db = new clsDatabase();
        protected string query = "";
        protected string pstrErrMsg = "";

        protected string pstrCmd = "";

        protected void Page_Init(object sender, EventArgs e)
        {
            ((BootstrapGridViewTagBoxColumn)EmplGrid.Columns["AuthorGroup"]).PropertiesTagBox.DataSource = GetAuthorGroupSource();
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
        }

        private System.Data.DataSet GetAuthorGroupSource()
        {
            clsDatabase db = new clsDatabase();
            string _query = string.Format("SELECT GroupCode, GroupName FROM CodeAuthorGroup Where CorpId = '{0}' ORDER BY GroupCode", Session["CorpId"].ToString());

            return db.GetDataSet("CodeAuthorGroup", _query);
        }

        protected void DeptTreeList_CustomJSProperties(object sender, TreeListCustomJSPropertiesEventArgs e)
        {
            ASPxTreeList treeList = sender as ASPxTreeList;
            Hashtable nameTable = new Hashtable();
            foreach (TreeListNode node in treeList.GetVisibleNodes())
                nameTable.Add(node.Key, string.Format("{0}", node["DeptName"]));
            e.Properties["cpDeptNames"] = nameTable;
        }

        protected void dropDownEdit_Init(object sender, EventArgs e)
        {
            BootstrapDropDownEdit dropDownEdit = (BootstrapDropDownEdit)sender;
            GridViewEditItemTemplateContainer container = dropDownEdit.NamingContainer as GridViewEditItemTemplateContainer;

            if (!container.Grid.IsNewRowEditing)
                dropDownEdit.Text = container.Text == "&nbsp;" ? "" : container.Text;
        }

        protected void EmplGrid_CellEditorInitialize(object sender, BootstrapGridViewEditorEventArgs e)
        {
            if (EmplGrid.IsNewRowEditing)
            {
                if (e.Column.FieldName == "EmployeeId")
                {
                    e.Editor.ReadOnly = false;
                }
                if (e.Column.FieldName == "UseYn")
                {
                    e.Editor.Value = "Y";
                }
            }
            else
            {
                if (e.Column.FieldName == "EmployeeId")
                {
                    e.Editor.ReadOnly = true;
                }

                if (e.KeyValue.ToString() == "admin")
                {
                    if (e.Column.FieldName == "AuthorGroup")
                    {
                        e.Editor.ReadOnly = true;
                    }
                    if (e.Column.FieldName == "EmployeeName")
                    {
                        e.Editor.ReadOnly = true;
                    }
                    if (e.Column.FieldName == "UseYn")
                    {
                        e.Editor.ReadOnly = true;
                    }
                }
            }

        }

        protected void EmplGrid_CommandButtonInitialize(object sender, BootstrapGridViewCommandButtonEventArgs e)
        {
            if (e.VisibleIndex < 0) return;
            DataRowView item = (DataRowView)EmplGrid.GetRow(e.VisibleIndex);

            if ((item["EmployeeId"] ?? "").ToString() == "admin")
            {
                if (e.ButtonType == DevExpress.Web.ColumnCommandButtonType.Delete) e.Visible = false;
            }
        }

        protected void EmplGrid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            clsDatabase db = new clsDatabase();
            string _query = string.Format("UPDATE CodeUser Set Password = '{2}' WHERE CorpId = '{0}' and EmployeeId = '{1}'",
                    Session["CorpId"].ToString(), e.NewValues["EmployeeId"].ToString(), clsCommon.SHA256Hash(Session["Temp"].ToString()));

            db.ExecuteQuery(_query);
        }

        protected void EmplGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["Password2"] == null)
            {
                EmplGrid.JSProperties["cpMsg"] = "비밀번호를 입력하세요.";
                Session["Temp"] = "";
                e.Cancel = true;
                return;
            }

            string msg = checkPassword(e.NewValues["Password2"].ToString());

            if (msg != "")
            {
                EmplGrid.JSProperties["cpMsg"] = msg;
                Session["Temp"] = "";
                e.Cancel = true;
                return;
            }

            Session["Temp"] = e.NewValues["Password2"].ToString();

            BootstrapGridView grid = (BootstrapGridView)sender;
            GridViewDataColumn column = grid.Columns["DeptCode"] as GridViewDataColumn;
            BootstrapDropDownEdit dde = grid.FindEditRowCellTemplateControl(column, "dropDownEdit") as BootstrapDropDownEdit;

            if (dde.KeyValue.ToString() != "")
                e.NewValues["DeptCode"] = dde.KeyValue.ToString();
        }

        protected void EmplGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["Password2"] != null)
            {
                clsDatabase db = new clsDatabase();
                string _query = string.Format("UPDATE CodeUser Set Password = '{2}' WHERE CorpId = '{0}' and EmpNo = '{1}'",
                        Session["CorpId"].ToString(), e.Keys[0].ToString(), clsCommon.SHA256Hash(e.NewValues["Password2"].ToString()));

                db.ExecuteQuery(_query);
            }

            BootstrapGridView grid = (BootstrapGridView)sender;
            GridViewDataColumn column = grid.Columns["DeptCode"] as GridViewDataColumn;
            BootstrapDropDownEdit dde = grid.FindEditRowCellTemplateControl(column, "dropDownEdit") as BootstrapDropDownEdit;

            if (dde.KeyValue.ToString() != "")
                e.NewValues["DeptCode"] = dde.KeyValue.ToString();
        }

        protected void EmplGrid_ToolbarItemClick(object sender, DevExpress.Web.Bootstrap.BootstrapGridViewToolbarItemClickEventArgs e)
        {
            switch (e.Item.Name)
            {
                case "CustomExportToXLSX":
                    EmplGrid.ExportXlsToResponse("사용자리스트", new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    break;
                default:
                    break;
            }
        }

        private string checkPassword(string pw)
        {
            string msg = "";

            //Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[0-9])(?=.*[\W]).{8,20}$");

            //if (pw.Length < 8 || pw.Length > 20)
            //{
            //    msg = "비밀번호는 영문, 숫자, 특수문자를 조합하여 8자리 ~ 20자리 이내로 입력하세요.";
            //}
            //else if (pw.Any(x => Char.IsWhiteSpace(x)) == true)
            //{
            //    msg = "비밀번호는 공백 없이 입력하세요.";
            //}
            //else if (!regex.IsMatch(pw.ToLower()))
            //{
            //    msg = "비밀번호는 영문, 숫자, 특수문자를 조합하여 8자리 ~ 20자리 이내로 입력하세요.";
            //}

            return msg;
        }
    }
}