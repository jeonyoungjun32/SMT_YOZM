using DevExpress.Web;
using DevExpress.Web.Bootstrap;
using DevExpress.Web.Internal.XmlProcessor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileViewer.View
{
    public partial class ProcessingInspectionDetail : System.Web.UI.Page
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
				//DocGrid.KeyFieldName = "InspNo;InspSeq";

				//txtTable.Text = Request.QueryString["table"];
				pstrPrjId = Request.QueryString["pid"] ?? "";
				OrderNo = Request.QueryString["orderno"] ?? "";
				OrderSeq = Request.QueryString["orderseq"] ?? "";
				SerialNo = Request.QueryString["serialno"] ?? "";
				SubSeq = Request.QueryString["subseq"] ?? "";

				//Hidden TextBox에 설정
				txtSerialNo.Text = SerialNo;
				txtOrderNo.Text = OrderNo;
				txtOrderSeq.Text = OrderSeq;
				txtSubSeq.Text = SubSeq;
				txtCorpId.Text = Session["CorpId"].ToString();
				//txtCorpName.InnerText = (Session["CorpName"] ?? "").ToString();
				//txtEmpName.InnerText = (Session["EmpName"] ?? "").ToString();

				//공정, 라인, 설비 코드 가져오기
				query = $@"
                        SELECT TOP 1 ORL.OrderNo
                            , ORL.OrderSeq
                            , ORL.SubSeq
                            , OP.ProcessCode
                            , CP.ProcessName
                            , OP.LineCode
                            , CL.LineName
                            , OP.HalfMaterialCode MaterialCode
                            , CL.MachineCode
                        FROM OrderResult ORL
                        INNER JOIN OrderProcess OP ON OP.OrderNo = ORL.OrderNo AND OP.OrderSeq = ORL.OrderSeq
                        INNER JOIN CmProcess CP ON OP.ProcessCode = CP.ProcessCode
                        INNER JOIN CmLine CL ON OP.LineCode = CL.LineCode
                        WHERE OP.CorpId = '{Convert.ToString(Session["CorpId"])}'
                        AND ORL.SerialNo = '{SerialNo}'

";

				DataRowView drv = db.GetDataRecord(query);

				if (drv != null)
				{
					txtProcess.Text = Convert.ToString(drv["ProcessCode"]);
					txtMaterial.Text = Convert.ToString(drv["MaterialCode"]);
					txtMachine.Text = Convert.ToString(drv["MachineCode"]);
				}

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

				//SetComBobox();
				SetGrid();



				pstrMenuId = Request.QueryString["id"] ?? "";

				//startDate_Stored.Date = DateTime.Now.Date.AddDays(-7);
				//endDate_Stored.Date = DateTime.Now.Date;


				//DocGrid.DataBind();
				//DocGrid.StartEdit(0);
			}
		}

        protected void Page_Load(object sender, EventArgs e)
        {

			
		}

        protected void SetGrid()
        {
			//DocGrid.Columns.Clear();

			int colIdx = 0;

			BootstrapGridViewTextColumn col = new BootstrapGridViewTextColumn();
			//col.FieldName = "RowNum";
			//col.Caption = "No";
			//col.Width = 5;
			//col.VisibleIndex = colIdx++;
			//DocGrid.Columns.Add(col);


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
				col.Visible = Convert.ToString(drv["VisibleYN"]) == "Y";

				//DISPLAY FOTMAT
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

				//EDITABLE
				string editable = Convert.ToString(drv["Editable"]);

				if (editable == "0")
				{
					col.ReadOnly = true;
					col.SettingsEditForm.Visible = DevExpress.Utils.DefaultBoolean.False;
				}

				DocGrid.Columns.Add(col);
			}

			//BootstrapGridViewCommandColumn colComm = new BootstrapGridViewCommandColumn();
			//colComm.ShowEditButton = true;
			//colComm.ShowDeleteButton = true;

			//DocGrid.Columns.Add(colComm);

			DocGrid.SettingsDataSecurity.AllowEdit = true;
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
                double ucl, lcl, sizeValue, sValue1, sValue2, sValue3, sValue4, sValue5 = 0;
                string disValue = Convert.ToString(e.NewValues["DisplayConfirm"]);
                double.TryParse(Convert.ToString(e.NewValues["UCL"]), out ucl);
                double.TryParse(Convert.ToString(e.NewValues["LCL"]), out lcl);
                double.TryParse(Convert.ToString(e.NewValues["SizeConfirm"]), out sizeValue);
                double.TryParse(Convert.ToString(e.NewValues["Sample1"]), out sValue1);
                double.TryParse(Convert.ToString(e.NewValues["Sample2"]), out sValue2);
                double.TryParse(Convert.ToString(e.NewValues["Sample3"]), out sValue3);
                double.TryParse(Convert.ToString(e.NewValues["Sample4"]), out sValue4);
                double.TryParse(Convert.ToString(e.NewValues["Sample5"]), out sValue5);

                string result = SetResult(ucl, lcl, disValue, sizeValue, sValue1, sValue2, sValue3, sValue4, sValue5);


				//UPDATE
                SqlDataSource1.UpdateParameters["CorpId"].DefaultValue = txtCorpId.Text;
                SqlDataSource1.UpdateParameters["SerialNo"].DefaultValue = txtSerialNo.Text;
                SqlDataSource1.UpdateParameters["DisplayConfirm"].DefaultValue = Convert.ToString(e.NewValues["DisplayConfirm"]);
                SqlDataSource1.UpdateParameters["SizeConfirm"].DefaultValue = Convert.ToString(e.NewValues["SizeConfirm"]);
                SqlDataSource1.UpdateParameters["Sample1"].DefaultValue = Convert.ToString(e.NewValues["Sample1"]);
                SqlDataSource1.UpdateParameters["Sample2"].DefaultValue = Convert.ToString(e.NewValues["Sample2"]);
                SqlDataSource1.UpdateParameters["Sample3"].DefaultValue = Convert.ToString(e.NewValues["Sample3"]);
                SqlDataSource1.UpdateParameters["Sample4"].DefaultValue = Convert.ToString(e.NewValues["Sample4"]);
                SqlDataSource1.UpdateParameters["Sample5"].DefaultValue = Convert.ToString(e.NewValues["Sample5"]);

                SqlDataSource1.UpdateParameters["InspNo"].DefaultValue = Convert.ToString(e.NewValues["InspNo"]);
                SqlDataSource1.UpdateParameters["InspSeq"].DefaultValue = Convert.ToString(e.NewValues["InspSeq"]);

                SqlDataSource1.UpdateParameters["Result"].DefaultValue = result;

                SqlDataSource1.Update();

                e.Cancel = true;

				DocGrid.CancelEdit();
				DocGrid.DataBind();
            }
            catch(Exception ex) 
            {
            
            }
        }

		private string SetResult(double ucl, double lcl, string disValue, double sizeValue, double sValue1, double sValue2, double sValue3, double sValue4, double sValue5)
		{
			string inspType = cboInspMaster.SelectedItem.GetFieldValue("InspType").ToString();

			string result = "";

			switch (inspType)
			{
				case "10":// 샘플검사

                    result = "OK";

                    if (!ChkSample(ucl, lcl, sValue1)) result = "NG"; 
					if (!ChkSample(ucl, lcl, sValue2)) result = "NG"; 
					if (!ChkSample(ucl, lcl, sValue3)) result = "NG"; 
					if (!ChkSample(ucl, lcl, sValue4)) result = "NG"; 
					if (!ChkSample(ucl, lcl, sValue5)) result = "NG"; 


					break;

				case "20":// 외관검사


					if (!(disValue == "OK" || disValue == "NG"))
					{
						return result;
					}
					else
					{
						result = disValue;
					}


					break;

				case "30":// 치수검사

					if (sizeValue >= lcl && sizeValue <= ucl)
					{
						result = "OK";
					}
					else
					{
						result = "NG";
					}

					break;

				default:
					break;
			}

			return result;
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
            int idx = DocGrid.FocusedRowIndex;

            DataRow dr = DocGrid.GetDataRow(idx);
            string orderNo = Convert.ToString(dr["OrderNo"]);
            
            if(orderNo == "")
			{
				query = $@"
						 INSERT WorkInspection
						 (
						 CorpId
						, OrderNo
						, OrderSeq
						, SubSeq
						, SerialNo
						, InspNo
						, InspSeq
						, InspTitle
						, UCL
						, LCL
						)
						 SELECT CD.CorpId
						, '{txtOrderNo.Text}'
						, '{txtOrderSeq.Text}'
						,'{txtSubSeq.Text}'
						, '{txtSerialNo.Text}'
						 ,CD.InspCode AS InspNo
						, CD.Seq AS InspSeq
						, CD.InspTitle
						, CD.UCL
						, CD.LCL
						FROM CodeInspectionDetail CD 
						LEFT JOIN WorkInspection WI  ON CD.CorpId = wi.CorpId AND CD.InspCode = WI.InspNo AND CD.Seq = WI.InspSeq AND WI.SerialNo = '{txtSerialNo.Text}'
						WHERE CD.CorpId = '{txtCorpId.Text}'
						AND InspCode = '{txtInspCode.Text}'
						AND WI.CorpId IS NULL
						
						";

				db.ExecuteQuery(query);

                DocGrid.DataBind();
				DocGrid.StartEdit(idx);
			}
        }

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			if (txtDeleteYN.Text != "Y") return;

			query = $@"
						DELETE FROM WorkInspection
						WHERE CorpId = '{txtCorpId.Text}'
						AND SerialNo = '{txtSerialNo.Text}'
						AND InspNo	 = '{txtInspCode.Text}'
						
						";
			db.ExecuteQuery(query);

			Response.Write("<script>alert('삭제가 완료되었습니다.'))</script>");

			//초기화
			txtDeleteYN.Text = "";

			DocGrid.DataBind();
		}

		protected void cboInspMaster_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtInspCode.Text = Convert.ToString(cboInspMaster.SelectedItem.GetFieldValue("InspCode"));
		}

		protected void DocGrid_EditFormLayoutCreated(object sender, BootstrapGridViewEditFormLayoutEventArgs e)
		{
		}
	}
}