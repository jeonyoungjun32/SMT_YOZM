using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.AI
{
    class OC_AbsData_Record
    {
        public string Abs_Id { get; set; }

        Database db = new Database();
        string query;

        public void UpdateRecord()
        {
            query = $@"UPDATE OC_AbsData_Record SET Record_Yn = 'Y' WHERE Abs_Id = '{Abs_Id}'";
            db.ExecuteQuery(query);
        }
    }
}
