using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.Web.Internal.XmlProcessor;
using DevExpress.Web.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileViewer.View.Document
{
    public partial class BusinessOrder2 : System.Web.UI.Page
    {
        protected string pstrCmd = "";
        protected string pstrMenuId = "";
        protected string pstrPrjId = "";

        protected clsDatabase db = new clsDatabase();
        protected string query = "";
        protected string pstrErrMsg = "";

        protected string pstrAuthor = "";

        protected string SerialNo = "";
        protected string OrderNo = "";
        protected string OrderSeq = "";
        protected string SubSeq = "";


        protected void Page_Init(object sender, EventArgs e)
        {
			//로그인 세션 확인
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
				DocGrid.KeyFieldName = "SdOrderNo;SdOrderSeq";

                //txtTable.Text = Request.QueryString["table"];
                pstrPrjId = Request.QueryString["pid"] ?? "";
				OrderNo = Request.QueryString["SdOrderNo"] ?? "";
				OrderSeq = Request.QueryString["SdOrderSeq"] ?? "";

				//Hidden TextBox에 설정
				txtSdOrderNo.Text = OrderNo;
				txtSdOrderSeq.Text = OrderSeq;
				txtCorpId.Text = Session["CorpId"].ToString();
				txtCorpName.InnerText = (Session["CorpName"] ?? "").ToString();
				txtEmpName.InnerText = (Session["EmpName"] ?? "").ToString();

				#region 하단 메뉴 설정 수정 필요
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

				if (AuthorList.Exists(s => s.MenuID == "10711" && s.AuthorCode == "01"))
				{
					if (pstrAuthor != "") pstrAuthor += "|";
					pstrAuthor += "10711";
				}

				if (AuthorList.Exists(s => s.MenuID == "11712" && s.AuthorCode == "01"))
				{
					if (pstrAuthor != "") pstrAuthor += "|";
					pstrAuthor += "11712";
				}

				if (AuthorList.Exists(s => s.MenuID == "11713" && s.AuthorCode == "01"))
				{
					if (pstrAuthor != "") pstrAuthor += "|";
					pstrAuthor += "11713";
				}

				if (AuthorList.Exists(s => s.MenuID == "11714" && s.AuthorCode == "01"))
				{
					if (pstrAuthor != "") pstrAuthor += "|";
					pstrAuthor += "11714";
				}

				if (AuthorList.Exists(s => s.MenuID == "11715" && s.AuthorCode == "01"))
				{
					if (pstrAuthor != "") pstrAuthor += "|";
					pstrAuthor += "11715";
				}
				#endregion

				SetGrid();

				pstrMenuId = Request.QueryString["id"] ?? "";

				DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
				DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));

				startDate_Stored.Date = startDate;
				endDate_Stored.Date = endDate;
				SetComBobox();


                DocGrid.StartEdit(0);
			}
		}

        protected void Page_Load(object sender, EventArgs e)
        {

		}

		protected void SetComBobox()
		{
			cboCategory.Items.Clear();
			cboCust.Items.Clear();

			cboCategory.Items.Add("수주일자");

			cboCategory.DataBind();
			cboCategory.SelectedIndex = 0;

			query = $@"exec SMT_LookUpEditGet '거래처','','','','전체','Y'";

			DataView dv = db.GetDataView("거래처", query);

			foreach (DataRowView item in dv)
			{
				cboCust.Items.Add(item["거래처명"].ToString(), item["거래처코드"].ToString());
			}

			cboCust.DataBind();
			cboCust.SelectedIndex = 0;
		}


		protected void SetGrid()
        {
			int colIdx = 0;

			BootstrapGridViewTextColumn col = new BootstrapGridViewTextColumn();

			query = string.Format("select * from ProjectField where ProjectId = {0} order by DisplayOrder", Request.QueryString["pid"]);
			DataView dv = db.GetDataView("ProjectField", query);

			foreach (DataRowView drv in dv)
			{
				string fieldName = drv["FieldName"].ToString();
				if (fieldName == "SoldToCode" || fieldName == "MaterialCode" || fieldName == "HouseID")
				{
					BootstrapGridViewDropDownEditColumn downcol = new BootstrapGridViewDropDownEditColumn();

					BootstrapGridViewComboBoxColumn comboCol = new BootstrapGridViewComboBoxColumn();
					comboCol.FieldName = fieldName;
					comboCol.Caption = drv["ShortCaption"].ToString();
					comboCol.Width = new Unit(Convert.ToInt32(drv["ListWidth"]) * 6);
					comboCol.VisibleIndex = colIdx++;
					comboCol.Visible = Convert.ToString(drv["VisibleYN"]) == "Y";
					comboCol.PropertiesComboBox.ClientInstanceName = fieldName + "Combo";
					comboCol.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDownList;
					comboCol.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
					comboCol.ReadOnly = false;
					string editable = Convert.ToString(drv["Editable"]);

					//if (editable == "0")
					//{
					//	comboCol.ReadOnly = true;
					//	comboCol.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
					//}

					DocGrid.Columns.Add(comboCol);
				}

				else if(fieldName == "DeliveryDate" || fieldName == "ObtainDate")
                {
					BootstrapGridViewDateColumn dateCol = new BootstrapGridViewDateColumn();

					dateCol.FieldName = fieldName;
					dateCol.Caption = drv["ShortCaption"].ToString();
					dateCol.Width = new Unit(Convert.ToInt32(drv["ListWidth"]) * 6);
					dateCol.VisibleIndex = colIdx++;
					dateCol.Visible = Convert.ToString(drv["VisibleYN"]) == "Y";
					dateCol.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
					dateCol.ReadOnly = false;

					//EDITABLE
					string editable = Convert.ToString(drv["Editable"]);

					//if (editable == "0")
					//{
					//	dateCol.ReadOnly = true;
					//	dateCol.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
					//}

					DocGrid.Columns.Add(dateCol);
				}

				else
				{
					col = new BootstrapGridViewTextColumn();
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

					col.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.True;
					col.ReadOnly = false;
					//EDITABLE
					string editable = Convert.ToString(drv["Editable"]);

					//if (editable == "0")
					//{
					//	col.ReadOnly = true;
					//	col.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
					//}

					DocGrid.Columns.Add(col);
				}
			}

			DocGrid.SettingsDataSecurity.AllowEdit = true; 
			ASPxWebControl.BackwardCompatibility.UseEditorPropertiesInDataControlValidation = false;
		}

		//protected void DocGrid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
		//{
		//	// 유효성 검사 전에 값 확인
		//	foreach (var fieldName in e.NewValues.Keys)
		//	{
		//		var value = e.NewValues[fieldName];
		//		System.Diagnostics.Debug.WriteLine($"Field: {fieldName}, Value: {value}");
		//	}
		//}

		protected void DocGrid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs e)
		{
			if (e.Column.FieldName == "SoldToCode")
			{
				DataTable dt = Session["SoldToLookup"] as DataTable;
				if (dt == null)
				{
					string query = @"exec SMT_LookUpEditGet '거래처','','','','','Y'";
					dt = db.GetDataTable(query);
					Session["SoldToLookup"] = dt;
				}
				DataRow[] drs = dt.Select("거래처코드 = '" + e.Value.ToString() + "'");
				if (drs.Length > 0)
					e.DisplayText = drs[0]["거래처명"].ToString();
			}
			else if (e.Column.FieldName == "MaterialCode")
			{
				DataTable dt = Session["MaterialLookup"] as DataTable;
				if (dt == null)
				{
					string query = @"exec SMT_LookUpEditGet '품목','','20,30','','','Y'";
					dt = db.GetDataTable(query);
					Session["MaterialLookup"] = dt;
				}
				DataRow[] drs = dt.Select("품번코드 = '" + e.Value.ToString() + "'");
				if (drs.Length > 0)
					e.DisplayText = drs[0]["품명"].ToString();
			}
			else if (e.Column.FieldName == "HouseID")
			{
				DataTable dt = Session["HouseLookup"] as DataTable;
				if (dt == null)
				{
					string query = @"exec SMT_LookUpEditGet '창고정보','','','','','Y'";
					dt = db.GetDataTable(query);
					Session["HouseLookup"] = dt;
				}
				DataRow[] drs = dt.Select("코드 = '" + e.Value.ToString() + "'");
				if (drs.Length > 0)
					e.DisplayText = drs[0]["코드명"].ToString();
			}
		}

		protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int index = -1;
            if (int.TryParse(e.Parameters, out index))
                DocGrid.SettingsEditing.Mode = (GridViewEditingMode)index;
        }

        protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            try
			{
				if (DocGrid.FocusedRowIndex != -1)
				{
					string unitPrice = Convert.ToString(e.OldValues["UnitPrice"]);
					int price = 0;
					price = int.Parse(e.NewValues["Qty"].ToString()) * int.Parse(e.NewValues["UnitPrice"].ToString());
					SqlDataSource1.UpdateParameters["CorpId"].DefaultValue = txtCorpId.Text;
					//SqlDataSource1.UpdateParameters["SoldToCode"].DefaultValue = Convert.ToString(e.NewValues["SoldToCode"]);
					SqlDataSource1.UpdateParameters["MaterialCode"].DefaultValue = Convert.ToString(e.NewValues["MaterialCode"]);
					SqlDataSource1.UpdateParameters["Qty"].DefaultValue = Convert.ToString(e.NewValues["Qty"]);
					SqlDataSource1.UpdateParameters["ObtainDate"].DefaultValue = DateTime.Parse(e.NewValues["ObtainDate"].ToString()).ToString("yyyy-MM-dd");
					SqlDataSource1.UpdateParameters["DeliveryDate"].DefaultValue = DateTime.Parse(e.NewValues["DeliveryDate"].ToString()).ToString("yyyy-MM-dd");
					//SqlDataSource1.UpdateParameters["HouseID"].DefaultValue = Convert.ToString(e.NewValues["HouseID"]);
					SqlDataSource1.UpdateParameters["Remark"].DefaultValue = Convert.ToString(e.NewValues["Remark"]);
					SqlDataSource1.UpdateParameters["Price"].DefaultValue = price.ToString();
					SqlDataSource1.UpdateParameters["VatPrice"].DefaultValue = (price / 10).ToString();
					SqlDataSource1.UpdateParameters["TotalPrice"].DefaultValue = (price + (price / 10)).ToString();
					SqlDataSource1.UpdateParameters["SdOrderNo"].DefaultValue = Convert.ToString(e.NewValues["SdOrderNo"]);
					SqlDataSource1.UpdateParameters["SdOrderSeq"].DefaultValue = Convert.ToString(e.NewValues["SdOrderSeq"]);
					SqlDataSource1.UpdateParameters["NewYn"].DefaultValue = "N";
				}

				else
                {
					txtSdOrderNo.Text = "SO" + db.ExecAutoNumber(Session["CorpId"].ToString(), "수주번호", DateTime.Now.Date);
					txtSdOrderSeq.Text = "1";
					SqlDataSource1.UpdateParameters["CorpId"].DefaultValue = txtCorpId.Text;
                    SqlDataSource1.UpdateParameters["SoldToCode"].DefaultValue = Convert.ToString(e.NewValues["SoldToCode"]);
                    SqlDataSource1.UpdateParameters["MaterialCode"].DefaultValue = Convert.ToString(e.NewValues["MaterialCode"]);
					SqlDataSource1.UpdateParameters["Qty"].DefaultValue = Convert.ToString(e.NewValues["Qty"]);
					SqlDataSource1.UpdateParameters["ObtainDate"].DefaultValue = DateTime.Parse(e.NewValues["ObtainDate"].ToString()).ToString("yyyy-MM-dd");
					SqlDataSource1.UpdateParameters["DeliveryDate"].DefaultValue = DateTime.Parse(e.NewValues["DeliveryDate"].ToString()).ToString("yyyy-MM-dd");
                    SqlDataSource1.UpdateParameters["HouseID"].DefaultValue = Convert.ToString(e.NewValues["HouseID"]);
                    SqlDataSource1.UpdateParameters["Remark"].DefaultValue = Convert.ToString(e.NewValues["Remark"]);
					SqlDataSource1.UpdateParameters["SdOrderNo"].DefaultValue = Convert.ToString(e.NewValues["SdOrderNo"]);
					SqlDataSource1.UpdateParameters["SdOrderSeq"].DefaultValue = Convert.ToString(e.NewValues["SdOrderSeq"]);
					SqlDataSource1.UpdateParameters["NewYn"].DefaultValue = "Y";
				}

				SqlDataSource1.Update();
				e.Cancel = true; 
				DocGrid.CancelEdit();

				string originalSelectCommand = SqlDataSource1.SelectCommand;
				SqlDataSource1.SelectCommand = originalSelectCommand;

				// 파라미터 명시적 설정
				SqlDataSource1.SelectParameters.Clear();
				SqlDataSource1.SelectParameters.Add("pCorpID", txtCorpId.Text);
				SqlDataSource1.SelectParameters.Add("pCust", txtCust.Text);
				SqlDataSource1.SelectParameters.Add("pStartDate", txtStartDate.Text);
				SqlDataSource1.SelectParameters.Add("pEndDate", txtEndDate.Text);
				SqlDataSource1.Select(DataSourceSelectArguments.Empty);
				DocGrid.DataSourceID = "SqlDataSource1"; 
				DocGrid.DataBind(); 
			}
            catch(Exception ex) 
            {
            
            }
        }

		private bool ChkSample(double ucl, double lcl, double value)
		{
			bool result = true;

			if (!(value >= lcl && value <= ucl))
			{
				result = false;
			}

			return result;
		}

        #region 미사용
        protected void DocGrid_RowInserted(object sender, DevExpress.Web.Data.ASPxDataInsertedEventArgs e)
        {
            //// 삽입할 데이터 설정
            //SqlDataSource1.InsertParameters[""].DefaultValue = e.NewValues["Name"].ToString();
            //SqlDataSource1.InsertParameters["Age"].DefaultValue = e.NewValues["Age"].ToString();

            //// 데이터베이스에 삽입
            //SqlDataSource1.Insert();

            //// 편집 모드를 종료하고 그리드를 다시 바인딩
            //e.Cancel = true; // 삽입 작업을 완료하고 그리드의 기본 작업을 방지
            //BootstrapGridView1.CancelEdit(); // 편집 모드를 종료
            //BootstrapGridView1.DataBind(); // 새 데이터로 그리드를 다시 바인딩
        }

		#endregion

		protected void DocGrid_StartRowEditing(object sender, DevExpress.Web.Data.ASPxStartRowEditingEventArgs e)
		{
          //DocGrid.ClientSideEvents.BatchEditRowValidating
        }


		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (txtDeleteYN.Text != "Y") return;

			query = $@"
						DELETE SdOrderDetail
						WHERE CorpId = '{txtCorpId.Text}'
						AND SdOrderNo = '{txtSdOrderNo.Text}'
						AND SdOrderSeq	 = '{txtSdOrderSeq.Text}'
						
						";
			db.ExecuteQuery(query);

			Response.Write("<script>alert('삭제가 완료되었습니다.'))</script>");

			//초기화
			txtDeleteYN.Text = "";
			DocGrid.DataBind();
		}

		protected void grid_CellEditorInitialize(object sender, BootstrapGridViewEditorEventArgs e)
		{
			if (e.Column.FieldName == "SoldToCode")
			{
				BootstrapComboBox combo = e.Editor as BootstrapComboBox;
				if (combo != null)
				{
                    // 거래처 데이터 설정
                    query = $@"exec SMT_LookUpEditGet '거래처','','','','','Y'";
					DataTable dt = db.GetDataTable(query);
					combo.DataSource = dt;
					combo.ValueField = "거래처코드";
					combo.TextField = "거래처명";

					combo.DataBind();					
				}
			}

			if (e.Column.FieldName == "MaterialCode")
			{
				var combo = e.Editor as BootstrapComboBox;
				if (combo != null)
				{
					// 거래처 데이터 설정
					query = "exec SMT_LookUpEditGet '품목','','20,30','','','Y'";
					combo.DataSource = db.GetDataTable(query);
					combo.ValueField = "품번코드";
					combo.TextField = "품명";
					combo.DataBind();
				}
			}

			if (e.Column.FieldName == "HouseID")
			{
				var combo = e.Editor as BootstrapComboBox;
				if (combo != null)
				{
					// 거래처 데이터 설정					
					query = $@"exec SMT_LookUpEditGet '창고정보','','','','','Y'";
					combo.DataSource = db.GetDataTable(query);
					combo.ValueField = "코드"; 
					combo.TextField = "코드명";
					combo.DataBind();
				}
			}

			bool isNewRow = DocGrid.FocusedRowIndex == -1 ? true : false;

			if (e.Column.FieldName == "SoldToCode" || e.Column.FieldName == "HouseID") 
			{	
				//if (isNewRow)
				//{
				//	e.Editor.ReadOnly = isNewRow; 
				//}
				//else
				//{
				//	e.Editor.ReadOnly = !isNewRow;
				//}
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
        {
			DocGrid.CancelEdit();

			// 조회 조건 설정
			txtCust.Text = cboCust.Value != null ? cboCust.Value.ToString() : "";
			txtStartDate.Text = startDate_Stored.Value != null ?
				Convert.ToDateTime(startDate_Stored.Value).ToString("yyyy-MM-dd") : "";
			txtEndDate.Text = endDate_Stored.Value != null ?
				Convert.ToDateTime(endDate_Stored.Value).ToString("yyyy-MM-dd") : "";

			// SqlDataSource1의 SelectParameters 설정
			SqlDataSource1.SelectParameters.Clear(); // 기존 파라미터 초기화
			SqlDataSource1.SelectParameters.Add("pCorpId", txtCorpId.Text);
			SqlDataSource1.SelectParameters.Add("pCust", txtCust.Text);
			SqlDataSource1.SelectParameters.Add("pStartDate", txtStartDate.Text);
			SqlDataSource1.SelectParameters.Add("pEndDate", txtEndDate.Text);

			// 그리드의 DataSourceID 확인 및 설정
			if (DocGrid.DataSourceID != "SqlDataSource1")
			{
				DocGrid.DataSourceID = "SqlDataSource1";
			}

			// 데이터 조회 및 바인딩
			SqlDataSource1.Select(DataSourceSelectArguments.Empty);
			DocGrid.DataBind();

			DataView dv = SqlDataSource1.Select(DataSourceSelectArguments.Empty) as DataView;
			if (dv != null && dv.Count > 0)
			{
				// 데이터가 있는 경우
				System.Diagnostics.Debug.WriteLine($"조회된 행 수: {dv.Count}");
			}
			else
			{
				// 데이터가 없는 경우
				System.Diagnostics.Debug.WriteLine("조회된 데이터 없음");
			}
		}

		protected void btnInsert_Click(object sender, EventArgs e)
        {
			string dataSourceId = DocGrid.DataSourceID;
			DocGrid.DataSourceID = null;

			DataView dv = SqlDataSource1.Select(DataSourceSelectArguments.Empty) as DataView;
			DataTable dt;

			if (dv == null || dv.Count == 0)
			{
				// Create a new DataTable with the required columns
				dt = new DataTable();
				dt.Columns.Add("SdOrderNo", typeof(string));
				dt.Columns.Add("SdOrderSeq", typeof(string));
				dt.Columns.Add("SoldToCode", typeof(string));
				dt.Columns.Add("MaterialCode", typeof(string));
				dt.Columns.Add("Qty", typeof(int));
				dt.Columns.Add("ObtainDate", typeof(string));
				dt.Columns.Add("DeliveryDate", typeof(string));
				dt.Columns.Add("HouseID", typeof(string));
				dt.Columns.Add("UnitPrice", typeof(int));
				dt.Columns.Add("Price", typeof(int));
				dt.Columns.Add("VatPrice", typeof(int));
				dt.Columns.Add("TotalPrice", typeof(int));
				dt.Columns.Add("State", typeof(string));
				dt.Columns.Add("Remark", typeof(string));
			}
			else
			{
				// Convert DataView to DataTable if data exists
				dt = dv.ToTable();
			}

			DataRow newRow = dt.NewRow();
			newRow["SdOrderNo"] = "";
			newRow["SdOrderSeq"] = "1";

            dt.Rows.Add(newRow);

			DocGrid.DataSource = dt;
			DocGrid.DataBind();

			int lastRowIndex = dt.Rows.Count - 1;
			DocGrid.StartEdit(lastRowIndex);
			txtSdOrderNo.Text = "";
		}

		protected void DocGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
		{
			e.Cancel = true;

			string sdOrderNo = e.Keys["SdOrderNo"].ToString();
			string sdOrderSeq = e.Keys["SdOrderSeq"].ToString();

			if (DocGrid.DataSource is DataTable dt)
			{
				DataRow rowToDelete = dt.AsEnumerable()
					.FirstOrDefault(row => row["SdOrderNo"].ToString() == sdOrderNo && row["SdOrderSeq"].ToString() == sdOrderSeq);

				if (rowToDelete != null)
				{
					dt.Rows.Remove(rowToDelete);
				}

				DocGrid.DataSource = dt;
				DocGrid.DataBind();
			}

			else
			{
				DocGrid.DataSourceID = "SqlDataSource1";
				SqlDataSource1.DeleteParameters.Clear();
				SqlDataSource1.DeleteParameters.Add("CorpId", txtCorpId.Text);
				SqlDataSource1.DeleteParameters.Add("SdOrderNo", sdOrderNo);
				SqlDataSource1.DeleteParameters.Add("SdOrderSeq", sdOrderSeq);
				SqlDataSource1.Delete();
				DocGrid.DataBind();
			}
		}
	}
}