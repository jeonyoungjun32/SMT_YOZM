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
	public partial class RptPO_ALLTHEN : DevExpress.XtraReports.UI.XtraReport
	{

		Database db = new Database();
		string query = "";

		public RptPO_ALLTHEN()
		{
			InitializeComponent();
		}

		public RptPO_ALLTHEN(DataRow dr)
		{
			InitializeComponent();

			string mmOrderNo = dr["MmOrderNo"].ConvertString_Null_To_Empty();

			query = $@"
						SELECT 
						  pd.MmOrderNo
						, pd.MmOrderSeq
						, pd.Qty
						, pd.UnitPrice
						, pd.TotalPrice
						, pd.Remark
						, pd.DeliveryDate						

						, pd.ItemCode
						, cm.ItemName
						, cm.UnitCode
						, cm.Size
						, cm.Standard

						, pm.PlacingDate
						, pm.CustCode
						, cc.CustName
						, cc.Tel CustTel
						, cc.Fax CustFax

						FROM MM_PlacingD pd
						LEFT JOIN MM_PlacingM pm ON pd.CorpId = pm.CorpId AND pd.MmOrderNo = pm.MmOrderNo
						LEFT JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.ItemCode = cm.ItemCode
						LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode

						WHERE pd.CorpId = '{WrGlobal.CorpID}' AND pd.MmOrderNo = '{mmOrderNo}'
						";

			DataTable dt = db.GetDataTable(query);
			this.DataSource = dt;
			
		}

		public RptPO_ALLTHEN(DataTable dt)
		{
			InitializeComponent();

			this.DataSource = dt;
		}

	}
}
