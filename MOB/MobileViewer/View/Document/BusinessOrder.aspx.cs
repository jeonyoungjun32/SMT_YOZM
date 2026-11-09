using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.Web.Internal.XmlProcessor;
using DevExpress.Web.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileViewer.View
{
    public partial class BusinessOrder : System.Web.UI.Page
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
				pstrMenuId = Request.QueryString["id"] ?? "";
				OrderNo = Request.QueryString["SdOrderNo"] ?? "";
				OrderSeq = Request.QueryString["SdOrderSeq"] ?? "";

				//Hidden TextBox에 설정
				txtSdOrderNo.Text = OrderNo;
				txtSdOrderSeq.Text = OrderSeq;
				txtCorpId.Text = Session["CorpId"].ToString();
				//txtCorpName.InnerText = (Session["CorpName"] ?? "").ToString();
				//txtEmpName.InnerText = (Session["EmpName"] ?? "").ToString();

				//조회 조건 설정
				DateTime endDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
				DateTime startDate = Convert.ToDateTime(endDate.AddDays(-7).ToString("yyyy-MM-dd"));

				startDate_Stored.Date = startDate;
				endDate_Stored.Date = endDate;

				// 페이지가 처음 로드될 때만 DB에서 조회하여 ViewState에 콤보박스 데이터 저장
				CacheComboBoxData();
				SetComBobox();

				//메뉴 설정
				SetMenu();

				//Grid Column설정
				SetGrid();

				SqlDataSource1.SelectParameters["pCorpId"].DefaultValue = Session["CorpId"].ToString();
				SqlDataSource1.SelectParameters["pStartDate"].DefaultValue = startDate.ToString("yyyyMMdd");
				SqlDataSource1.SelectParameters["pEndDate"].DefaultValue = endDate.ToString("yyyyMMdd");
				//SqlDataSource1.SelectParameters["pMaterialCode"].DefaultValue = "ALL";
				SqlDataSource1.SelectParameters["pCust"].DefaultValue = "ALL";

				DocGrid.DataBind();
			}
		}

		protected void CacheComboBoxData()
		{
			Session["Combo_Cust"] = db.GetDataTable("exec SMT_LookUpEditGet '거래처','','','','','Y'");
			Session["Combo_Item"] = db.GetDataTable("exec SMT_LookUpEditGet '품목','','20,30','','','Y'");
			Session["Combo_House"] = db.GetDataTable("exec SMT_LookUpEditGet '창고','','','','','Y'");
		}

		protected void SetMenu()
		{
			//좌측/하단 메뉴 설정

			List<clsAuthor> readAuthor = (List<clsAuthor>)Session["AuthorRead"];

			List<string> allowedMenus = new List<String>();

			if (readAuthor != null)
			{
				readAuthor.Select(x => x.ParamText).Distinct().ToList();
			}

			pstrAuthor = string.Join("|", allowedMenus);

			//rptSideMenu.DataSource = readAuthor;
			//rptSideMenu.DataBind();
		}

		protected void Page_Load(object sender, EventArgs e)
        {
			DocGrid.DataBind();

		}

		protected void SetComBobox()
		{
			cboCategory.Items.Clear();
			cboCust.Items.Clear();

			cboCategory.Items.Add("수주일자");

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

		protected void SetGrid()
        {
			DocGrid.SettingsDataSecurity.AllowEdit = true;
			ASPxWebControl.BackwardCompatibility.UseEditorPropertiesInDataControlValidation = false;

			//ProjectField 컬럼 추가
			clsGrid.CreateColumn(DocGrid, pstrPrjId);

			// 데이터 삽입/수정/삭제 권한을 코드로 부여
			DocGrid.SettingsDataSecurity.AllowInsert = true;
			DocGrid.SettingsDataSecurity.AllowEdit = true;
			DocGrid.SettingsDataSecurity.AllowDelete = true;

			//CommandColumn 추가
			BootstrapGridViewCommandColumn commandColumn = new BootstrapGridViewCommandColumn();
			commandColumn.ShowEditButton = true;
			commandColumn.ShowDeleteButton = true;
			commandColumn.Width = Unit.Pixel(80); // 아이콘 2개에 맞는 적절한 픽셀값 추천
			DocGrid.Columns.Add(commandColumn);
		}

		protected void grid_CellEditorInitialize(object sender, BootstrapGridViewEditorEventArgs e)
		{
			if (e.Column.FieldName == "CustName")
			{
				BootstrapComboBox combo = e.Editor as BootstrapComboBox;
				if (combo != null)
				{
					//거래처 데이터 설정
					combo.DataSource = Session["Combo_Cust"];
					combo.ValueField = "거래처코드";
					combo.TextField = "거래처명";
					combo.DataBind();
				}
			}

			if (e.Column.FieldName == "ItemName")
			{
				BootstrapComboBox combo = e.Editor as BootstrapComboBox;
				if (combo != null)
				{
					//품목 데이터 설정
					combo.DataSource = Session["Combo_Item"];
					combo.ValueField = "품목코드";
					combo.TextField = "품명";
					combo.DataBind();
				}
			}

			if (e.Column.FieldName == "HouseID")
			{
				BootstrapComboBox combo = e.Editor as BootstrapComboBox;
				if (combo != null)
				{
					// 창고 데이터 설정					
					combo.DataSource = Session["Combo_House"];
					combo.ValueField = "코드";
					combo.TextField = "코드명";
					combo.DataBind();
				}
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			DocGrid.CancelEdit();

			//// 조회 조건 설정
			txtCust.Text = cboCust.Value != null ? cboCust.Value.ToString() : "";
			txtStartDate.Text = startDate_Stored.Value != null ? Convert.ToDateTime(startDate_Stored.Value).ToString("yyyy-MM-dd") : "";
			txtEndDate.Text = endDate_Stored.Value != null ? Convert.ToDateTime(endDate_Stored.Value).ToString("yyyy-MM-dd") : "";

			//// 데이터 조회 및 바인딩
			DocGrid.DataBind();
		}

		protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
			try
			{
				DateTime deliveryDate = new DateTime();
				DateTime.TryParse(Convert.ToString(e.NewValues["DeliveryDate"]), out deliveryDate);

				int unitPrice = 0;
				int.TryParse(Convert.ToString(e.NewValues["UnitPrice"]), out unitPrice);

				int price = 0;
				price = int.Parse(e.NewValues["Qty"].ToString()) * unitPrice;

				SqlDataSource1.UpdateParameters["CorpId"].DefaultValue = txtCorpId.Text;
				SqlDataSource1.UpdateParameters["SoldToName"].DefaultValue = Convert.ToString(e.NewValues["CustName"]);
				SqlDataSource1.UpdateParameters["ItemName"].DefaultValue = Convert.ToString(e.NewValues["ItemName"]);
				SqlDataSource1.UpdateParameters["Qty"].DefaultValue = Convert.ToString(e.NewValues["Qty"]);
				SqlDataSource1.UpdateParameters["ObtainDate"].DefaultValue = DateTime.Parse(e.NewValues["ObtainDate"].ToString()).ToString("yyyy-MM-dd");
				SqlDataSource1.UpdateParameters["DeliveryDate"].DefaultValue = deliveryDate == DateTime.MinValue ? null : deliveryDate.ToString("d");
				SqlDataSource1.UpdateParameters["Remark"].DefaultValue = Convert.ToString(e.NewValues["Remark"]);
				SqlDataSource1.UpdateParameters["Price"].DefaultValue = price.ToString();
				SqlDataSource1.UpdateParameters["VatPrice"].DefaultValue = (price / 10).ToString();
				SqlDataSource1.UpdateParameters["TotalPrice"].DefaultValue = (price + (price / 10)).ToString();
				SqlDataSource1.UpdateParameters["SdOrderNo"].DefaultValue = Convert.ToString(e.Keys["SdOrderNo"]);
				SqlDataSource1.UpdateParameters["SdOrderSeq"].DefaultValue = Convert.ToString(e.Keys["SdOrderSeq"]);
				SqlDataSource1.UpdateParameters["NewYn"].DefaultValue = "N";

				SqlDataSource1.Update();

				e.Cancel = true;
				DocGrid.CancelEdit();
			}
			catch (Exception ex)
			{

			}
		}

		protected void btnInsert_Click(object sender, EventArgs e)
		{
			DocGrid.AddNewRow();
		}

		protected void DocGrid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
		{
			// 'New' 버튼을 눌렀을 때 기본값 설정
			e.NewValues["ObtainDate"] = DateTime.Now;
			e.NewValues["State"] = "대기";
			e.NewValues["DeliveryDate"] = DateTime.Now;
		}

		protected void DocGrid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
		{
			//납기일자
			DateTime deliveryDate = new DateTime();
			DateTime.TryParse(Convert.ToString(e.NewValues["DeliveryDate"]), out deliveryDate);

			string materialCode = Convert.ToString(e.NewValues["ItemName"]);
			string custCode = Convert.ToString(e.NewValues["CustName"]);
			string obtainDate = DateTime.Parse(e.NewValues["ObtainDate"].ToString()).ToString("d");
			float unitPrice = 0;

			//단가 가져오기
			query = $@" 
				SELECT [dbo].[FN_Get_ItemPrice] ('{txtCorpId.Text}', '20', '{materialCode}', '{custCode}', '{obtainDate}')
				";

			DataRowView drv = db.GetDataRecord(query);

			if(drv != null)
			{
				float.TryParse(Convert.ToString(drv[0]), out unitPrice);
			}

			// 새 수주번호 생성
			txtSdOrderNo.Text = "SO" + db.ExecAutoNumber(Session["CorpId"].ToString(), "수주번호", DateTime.Now.Date);

			// Insert 파라미터 설정
			SqlDataSource1.InsertParameters["CorpId"].DefaultValue = txtCorpId.Text;
			SqlDataSource1.InsertParameters["SdOrderNo"].DefaultValue = txtSdOrderNo.Text;
			SqlDataSource1.InsertParameters["SoldToCode"].DefaultValue = Convert.ToString(e.NewValues["CustName"]);
			SqlDataSource1.InsertParameters["ItemCode"].DefaultValue = materialCode;
			SqlDataSource1.InsertParameters["Qty"].DefaultValue = Convert.ToString(e.NewValues["Qty"]);
			SqlDataSource1.InsertParameters["ObtainDate"].DefaultValue = DateTime.Parse(e.NewValues["ObtainDate"].ToString()).ToString("yyyy-MM-dd");
			SqlDataSource1.InsertParameters["DeliveryDate"].DefaultValue = deliveryDate == DateTime.MinValue ? null : deliveryDate.ToString("d");
			SqlDataSource1.InsertParameters["HouseID"].DefaultValue = Convert.ToString(e.NewValues["HouseID"]);
			SqlDataSource1.InsertParameters["Remark"].DefaultValue = Convert.ToString(e.NewValues["Remark"]);
			SqlDataSource1.InsertParameters["SdOrderNo"].DefaultValue = Convert.ToString(e.NewValues["SdOrderNo"]);

			SqlDataSource1.Insert();

			e.Cancel = true;
			DocGrid.CancelEdit();
		}


		protected void DocGrid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
		{
			SqlDataSource1.DeleteParameters["SdOrderNo"].DefaultValue = Convert.ToString(e.Keys["SdOrderNo"]);
			SqlDataSource1.DeleteParameters["SdOrderSeq"].DefaultValue = Convert.ToString(e.Keys["SdOrderSeq"]);

			SqlDataSource1.Delete();

			e.Cancel = true;
			DocGrid.CancelEdit();
		}
	}
}