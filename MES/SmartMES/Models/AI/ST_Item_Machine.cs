using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.AI
{
    class ST_Item_Machine
    {
        public string ItemCode { get; set; }
        public string MachineName { get; set; }

        Database db = new Database();
        string query;

        public DataTable Select_Item_Machine()
        {
            query = $@"SELECT * FROM ST_Item_Machine ORDER BY MachineName ASC";

            return db.GetDataSet(query);
        }

        public void Update_Item_Machine()
        {
            query = $@"UPDATE ST_Item_Machine SET MachineName = '{MachineName}' WHERE ItemCode = '{ItemCode}'";

            db.ExecuteQuery(query);
        }

        public DataTable Select_MachineName()
        {
            query = $@"SELECT MachineName FROM  ST_Item_Machine WHERE ItemCode = '{ItemCode}'";

            return db.GetDataSet(query);
        }
    }
}
