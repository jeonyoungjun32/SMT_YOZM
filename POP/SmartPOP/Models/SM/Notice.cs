using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.SystemCode
{
	public class Notice
	{
		#region Property
		public string CorpId { get; set; }

		public string BoardNo { get; set; }

		public string Class { get; set; }

		public DateTime WriteDate { get; set; }

		public string Subject { get; set; }

		public string WriteName { get; set; }

		public string Body { get; set; }

		public string DisplayType { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }
		#endregion

		Database db = new Database();
		string query = "";

		public DataRow SelectNotice_BoardNo()
		{
			query = $@"
				SELECT *
				FROM Notice
				WHERE CorpId = '{CorpId}'
				AND BoardNo = '{BoardNo}'
				
				";

			return db.GetDataRow(query);
		}

	}
}
