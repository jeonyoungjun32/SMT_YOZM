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
	public partial class RptPO_UAG : DevExpress.XtraReports.UI.XtraReport
	{

		Database db = new Database();
		string query = "";

		public RptPO_UAG()
		{
			InitializeComponent();
		}

		public RptPO_UAG(DataRow dr)
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
							, cm.Remark
							, pd.ItemCode
							, pd.MmOrderNo as BarCode

							, cm.ItemName
							, cm.UnitCode
							, ci.Title as UnitTitle
							, cm.Size

							, pm.PlacingDate
							, pm.CustCode
							, cc.CustName
							, cc.Tel
							, cc.Fax

							, cp.CorpName
							, cp.PresiName
							, cp.Address

							, cp.Tel AS Tel_UAG
							, cp.Fax AS Fax_UAG
							, cp.Tel + ' / ' + cp.Fax AS TelFax
							, pm.Remark pmRemark
							, GETDATE() AS TODAY
						FROM MM_PlacingD pd
						LEFT JOIN MM_PlacingM pm ON pd.CorpId = pm.CorpId AND pd.MmOrderNo = pm.MmOrderNo
						LEFT JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.ItemCode = cm.ItemCode
						LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode
						INNER JOIN SM_Corp cp ON pm.CorpId = cp.CorpId
						INNER JOIN CM_CateItem ci ON pd.CorpId = ci.CorpId AND CLASS = 'A011' AND cm.UnitCode = ci.ClassItemCode

						WHERE pd.CorpId = '{WrGlobal.CorpID}' AND pd.MmOrderNo = '{mmOrderNo}'
						ORDER BY MmOrderSeq
						";

			DataTable dt = db.GetDataTable(query);
			this.DataSource = dt;
		}

		public RptPO_UAG(DataTable dt)
		{
			InitializeComponent();

			this.DataSource = dt;
		}

	}
}
