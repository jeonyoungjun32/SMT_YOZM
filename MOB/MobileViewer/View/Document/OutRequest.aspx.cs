using DevExpress.Web.Bootstrap;
using DevExpress.Web.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileViewer.View
{
    public partial class OutRequest : System.Web.UI.Page
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
                SetComBobox();
                pstrMenuId = Request.QueryString["id"] ?? "";

                txtCorpId.Text = Session["CorpId"].ToString();
                DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
                DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
                endDate = endDate.AddDays(-1);

                startDate_Stored.Date = startDate;
                endDate_Stored.Date = endDate;
            }
        }

        protected void SetGrid()
        {
            DocGridRequest.Columns.Clear();
            DocGridRelease.Columns.Clear();

            int colIdx = 0;

            query = string.Format("select * from MB_ProjectField where ProjectId = {0} order by DisplayOrder", Request.QueryString["pid"]);
            DataView dv = db.GetDataView("ProjectField", query);

            BootstrapGridViewCommandColumn colCommand = new BootstrapGridViewCommandColumn();
            colCommand.ShowEditButton = true;
            colCommand.VisibleIndex = 0;
            colCommand.Width = Unit.Pixel(40);
            DocGridRequest.Columns.Add(colCommand);

            foreach (DataRowView drv in dv)
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

                string editable = Convert.ToString(drv["Editable"]);

                if (editable == "0")
                {
                    col.ReadOnly = true;
                    col.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
                }

                DocGridRequest.Columns.Add(col);
            }


            query = string.Format("select * from MB_ProjectField where ProjectId = {0} order by DisplayOrder", 4);
            dv = db.GetDataView("ProjectField", query);

            foreach (DataRowView drv in dv)
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

                DocGridRelease.Columns.Add(col);
            }
        }

        protected void SetComBobox()
        {
            cboCategory.Items.Clear();
            cboCust.Items.Clear();

            cboCategory.Items.Add("요청등록일자");
            cboCategory.Items.Add("출고요청일자");
            cboCategory.DataBind();
            cboCategory.SelectedIndex = 0;

            query = string.Format("select a.코드, a.내용 from (select 1 Sorting, 'All' 코드, '전체' 내용 Union All "
                                                            + "select 2 Sorting, CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType in ('20','40')) a Order By a.Sorting, a.코드", Session["CorpId"]);

            DataView dv = db.GetDataView("거래처", query);

            foreach (DataRowView item in dv)
            {
                cboCust.Items.Add(item["내용"].ToString(), item["코드"].ToString());
            }

            cboCust.DataBind();
            cboCust.SelectedIndex = 0;
        }

        protected void btnRequest_onClick(object sender, EventArgs e)
        {
            int idx = DocGridRequest.FocusedRowIndex;

            if (idx < 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('선택된 행이 없습니다.');", true);
                return;
            }

            DataRow row = DocGridRequest.GetDataRow(idx);
            if (row == null) return;

            query = "Delete TempRequestSales Where HostName = Host_name()";
            db.ExecuteQuery(query);

            float outQty;
            string reqNo, reqSeq, shipToCode, custCode, materialCode;
            DateTime outDate;

            //납품처 및 출고수량 체크

            shipToCode = row["ShipToCode"].ToString();
            outQty = float.Parse(row["OutQty"].ToString());

            if (shipToCode == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('납품처가 없는 건이 있습니다.');", true);
                return;
            }

            if (outQty == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('출고수량을 입력해주세요.');", true);
                return;
            }

            custCode = row["CustCode"].ToString();
            outDate = (DateTime)row["OutDate"];
            materialCode = row["MaterialCode"].ToString();
            reqNo = row["ReqNo"].ToString();
            reqSeq = row["ReqSeq"].ToString();

            query = $"INSERT INTO TempRequestSales (HostName,CustCode,ShipToCode,OutDate,MaterialCode,OutQty,ReqNo,ReqSeq)";
            query = query + $"values (Host_name(),'{custCode}','{shipToCode}','{outDate.ToString("yyyy-MM-dd HH:mm:ss")}','{materialCode}',{outQty},'{reqNo}',{reqSeq})";
            db.ExecuteQuery(query);

            query = $"exec Sales_MakeSalesorder '{Session["CorpId"].ToString()}'";
            db.ExecuteQuery(query);

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('자료를 저장하였습니다.');", true);
        }

        protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
            {
                //UPDATE
                SqlDataSource1.UpdateParameters["CorpId"].DefaultValue = txtCorpId.Text;
                SqlDataSource1.UpdateParameters["ShipToCode"].DefaultValue = Convert.ToString(e.NewValues["ShipToCode"]);
                SqlDataSource1.UpdateParameters["CustCode"].DefaultValue = Convert.ToString(e.NewValues["CustCode"]);
                SqlDataSource1.UpdateParameters["OutDate"].DefaultValue = DateTime.Now.ToString("yyyy-MM-dd");
                SqlDataSource1.UpdateParameters["MaterialCode"].DefaultValue = Convert.ToString(e.NewValues["MaterialCode"]);
                SqlDataSource1.UpdateParameters["ReqNo"].DefaultValue = Convert.ToString(e.NewValues["ReqNo"]);
                SqlDataSource1.UpdateParameters["ReqSeq"].DefaultValue = Convert.ToString(e.NewValues["ReqSeq"]);
                SqlDataSource1.UpdateParameters["OutQty"].DefaultValue = Convert.ToString(e.NewValues["OutQty"]);

                SqlDataSource1.Update();

                e.Cancel = true;

                DocGridRequest.CancelEdit();
                DocGridRequest.DataBind();

                foreach (DictionaryEntry entry in e.NewValues)
                {
                    string fieldName = entry.Key.ToString();
                    object newValue = entry.Value;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void Grid_CellEditorInitialize(object sender, BootstrapGridViewEditorEventArgs e)
        {
            BootstrapGridView grid = (BootstrapGridView)sender;
            if (!grid.IsEditing || e.Column.FieldName != "City" || e.KeyValue == DBNull.Value || e.KeyValue == null)
                return;
        }

        protected void DocGridRequest_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            foreach (var update in e.UpdateValues)
            {
                var modifiedValue = update.NewValues["OutQty"];
                // 각 업데이트 항목 처리
            }
            e.Handled = true; // 직접 데이터 저장을 완료했음을 알림
        }
    }
}