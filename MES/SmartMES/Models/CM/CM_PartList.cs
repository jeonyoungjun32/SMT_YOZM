using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;

namespace SmartMES.Models.CM
{
	public class CM_PartList
	{
		#region Property
		public string Parent_ItemCode { get; set; }
		public string Me_ItemCode { get; set; }
		public string CustCode { get; set; }
		public decimal Need_Qty { get; set; }
		public string UseYn { get; set; }
		#endregion

		Database db = new Database();
		string query = "";

		public DataTable Select_PartList()
		{
			query = $@"
						SELECT '' AS Status
						, pl.Parent_ItemCode
						, pl.Me_ItemCode AS ItemCode
						, pl.CustCode
						, itm.ItemCode AS Item_Nm
						, itm.ItemName
						, itm.Alias
						, itm.Size
						, itm.Standard
						
						, pl.Need_Qty
						, pl.UseYn
						, pl.CreId
						, pl.CreDt
						, pl.ModId
						, pl.ModDt
						
						FROM BS_PartList pl
						LEFT JOIN CM_Item itm ON pl.CorpId = itm.CorpId AND pl.Me_ItemCode = itm.ItemCode
						WHERE pl.CorpId = '{WrGlobal.CorpID}'
						AND pl.Parent_ItemCode = '{Parent_ItemCode}'
						AND pl.CustCode = '{CustCode}'
					";

			return db.GetDataTable(query);
		}

		public bool Ins_PartList()
		{
			query = $@"
						INSERT INTO BS_PartList
						(
							CorpId
							, Parent_ItemCode
							, Me_ItemCode
							, CustCode
							, Need_Qty
							, UseYn
							, CreId
							, CreDt
						)
						VALUES
						(
							'{WrGlobal.CorpID}'
							, '{Parent_ItemCode}'
							, '{Me_ItemCode}'
							, '{CustCode}'
							, {Need_Qty}
							, '{UseYn}'
							, '{WrGlobal.LoginID}'
							, GETDATE()
						)
					";

			return db.ExecuteQuery(query);
		}

		public bool Upd_PartList()
		{
			query = $@"
						UPDATE BS_PartList
						SET Need_Qty = {Need_Qty.ConvertDecimal_Empty_To_Null()}
						  , ModId = '{WrGlobal.LoginID}'
						  , ModDt = GETDATE()
						WHERE CorpId = '{WrGlobal.CorpID}'
						AND Parent_ItemCode = '{Parent_ItemCode}'
						AND Me_ItemCode = '{Me_ItemCode}'
						AND CustCode = '{CustCode}'

					";

			return db.ExecuteQuery(query);
		}

		public bool Del_PartList()
		{
			query = $@"
						DELETE BS_PartList
						WHERE CorpId ='{WrGlobal.CorpID}'
						AND Parent_ItemCode = '{Parent_ItemCode}'
						AND Me_ItemCode = '{Me_ItemCode}'
						AND CustCode = '{CustCode}'
						";

			return db.ExecuteQuery(query);
		}

		public bool Del_PartList_Parent()
		{
			query = $@"
						DELETE BS_PartList
						WHERE CorpId ='{WrGlobal.CorpID}'
						AND Parent_ItemCode = '{Parent_ItemCode}'
						";

			return db.ExecuteQuery(query);
		}
	}
}
