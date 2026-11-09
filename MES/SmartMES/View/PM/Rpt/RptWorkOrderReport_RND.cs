using SmartCommon.Common;
using System.Data;

namespace SmartMES.View.PM.Rpt
{
	public partial class RptPM_ProductPlanReport_RND : DevExpress.XtraReports.UI.XtraReport
    {
        Database db = new Database();

        public RptPM_ProductPlanReport_RND(DataTable dt_OrderList)
        {
            InitializeComponent();

            this.DataSource = dt_OrderList;

            lbl_Machine.DataBindings.Add("Text", dt_OrderList, "MachineName");
            lbl_PM_ProductPlanDate.DataBindings.Add("Text", dt_OrderList, "OrderDate");
            lbl_CustName.DataBindings.Add("Text", dt_OrderList, "CustName");
            lbl_ItemName.DataBindings.Add("Text", dt_OrderList, "ItemName");
            lbl_MaterialAlias.DataBindings.Add("Text", dt_OrderList, "Alias");
            lbl_ColorName.DataBindings.Add("Text", dt_OrderList, "ColorCode");
            lbl_QtyM.DataBindings.Add("Text", dt_OrderList, "OrderQty");
            lbl_UnitCode.DataBindings.Add("Text", dt_OrderList, "UnitCode");
            lbl_SalesRemark.DataBindings.Add("Text", dt_OrderList, "SalesRemark");
            lbl_Remark.DataBindings.Add("Text", dt_OrderList, "SdOrder_Remark");
        }

    }
}
