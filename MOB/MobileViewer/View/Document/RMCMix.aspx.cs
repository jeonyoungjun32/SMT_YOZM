using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Web.Bootstrap;
using DevExpress.Web.Bootstrap.Internal;
using DevExpress.XtraSpellChecker.Native;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileViewer.View.Document
{
    public partial class RMCMix : System.Web.UI.Page
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
				pstrMenuId = Request.QueryString["id"] ?? "";

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

				// 헬퍼 클래스의 메서드를 한 줄 호출하여 그리드를 설정합니다.
				//SetGrid();
				clsGrid.CreateBandedColumn(DocGrid, pstrPrjId);

				//SetComBobox();

                txtCorpId.Text = Session["CorpId"].ToString();
				//DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
				//DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));

				//startDate_Stored.Date = startDate;
				//endDate_Stored.Date = endDate;
				txtMixCode.Text = "ALL";
				DocGrid.DataBind();
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
				string fieldName = drv["FieldName"].ToString();
				if (fieldName == "CustCode")
				{
					BootstrapGridViewComboBoxColumn comboCol = new BootstrapGridViewComboBoxColumn();
					comboCol.FieldName = fieldName;
					comboCol.Caption = drv["ShortCaption"].ToString();
					comboCol.Width = new Unit(Convert.ToInt32(drv["ListWidth"]) * 6);
					comboCol.VisibleIndex = colIdx++;
					comboCol.Visible = Convert.ToString(drv["VisibleYN"]) == "Y";
					comboCol.PropertiesComboBox.ClientInstanceName = fieldName + "Combo";

					comboCol.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;

					string editable = Convert.ToString(drv["Editable"]);

					if (editable == "0")
					{
						comboCol.ReadOnly = true;
						comboCol.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
					}

					DocGrid.Columns.Add(comboCol);
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

		//protected void SetComBobox()
		//{
		//    cboCategory.Items.Clear();
		//    cboCust.Items.Clear();

		//    cboCategory.Items.Add("수주일자");

		//    cboCategory.DataBind();
		//    cboCategory.SelectedIndex = 0;

		//    query = $@"exec SMT_LookUpEditGet '거래처','','','','전체','Y'";

		//    DataView dv = db.GetDataView("거래처", query);

		//    foreach (DataRowView item in dv)
		//    {
		//        cboCust.Items.Add(item["거래처명"].ToString(), item["거래처코드"].ToString());
		//    }

		//    cboCust.DataBind();
		//    cboCust.SelectedIndex = 0;
		//}

		protected void DocGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
		{
			//if (e.Column.FieldName == "CustCode")
			//{
			//	DataTable dt = Session["CustLookup"] as DataTable;
			//	if (dt == null)
			//	{
			//		string query = @"exec SMT_LookUpEditGet '거래처','','','','','Y'";
			//		dt = db.GetDataTable(query);
			//		Session["SoldToLookup"] = dt;
			//	}
			//	DataRow[] drs = dt.Select("거래처코드 = '" + e.Value.ToString() + "'");
			//	if (drs.Length > 0)
			//		e.DisplayText = drs[0]["거래처명"].ToString();
			//}
		}
	}
}