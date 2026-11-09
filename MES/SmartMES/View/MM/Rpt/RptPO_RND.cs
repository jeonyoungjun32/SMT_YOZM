using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using SmartMES.Common;
using System.Drawing;
using SmartCommon.Common;

namespace SmartMES.View.Placing.Rpt
{
	public partial class RptPO_RND : DevExpress.XtraReports.UI.XtraReport
	{

		Database db = new Database();
		string query = "";

		public RptPO_RND()
		{
			InitializeComponent();
		}

		public RptPO_RND(DataRow dr)
		{
			InitializeComponent();

			string mmOrderNo = dr["MmOrderNo"].ConvertString_Null_To_Empty();

			query = $@"
						 SELECT 
							pd.MmOrderNo
							, pd.MmOrderSeq
							, pd.Qty
							, pd.UnitPrice
							, pd.VatPrice
							, pd.TotalPrice
							, pd.DeliveryDate
							, pd.Remark
							, pd.ItemCode
							, pd.MmOrderNo as BarCode

							, cm.ItemName
							, cm.UnitCode
							, cm.Size

							, pm.PlacingDate
							, pm.CustCode
							, cc.CustName
							, cc.Tel
							, cc.Fax

							, cp.Tel AS CorpTel
							, cp.Fax
							, cp.Tel + ' / ' + cp.Fax AS TelFax
						FROM MM_PlacingD pd
						LEFT JOIN MM_PlacingM pm ON pd.CorpId = pm.CorpId AND pd.MmOrderNo = pm.MmOrderNo
						LEFT JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.ItemCode = cm.ItemCode
						LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode
						INNER JOIN SM_Corp cp ON pm.CorpId = cp.CorpId 

						WHERE pd.CorpId = '{WrGlobal.CorpID}' AND pd.MmOrderNo = '{mmOrderNo}'
						";

			DataTable dt = db.GetDataTable(query);
			this.DataSource = dt;
		}

		public RptPO_RND(DataTable dt)
		{
			InitializeComponent();

			this.DataSource = dt;
		}

	}
}
