using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.CM
{
    class CM_Warehouse
    {
        public string CorpId { get; set; }

        public string HouseID { get; set; }

        public string Title { get; set; }

        public string WareType { get; set; }

        public string WareKind { get; set; }

        public string WareState { get; set; }

        public string ProcessCode { get; set; }

        public string DeptCode { get; set; }

        public string Remark { get; set; }

        public DateTime? CloseDate { get; set; }

        public string Tel { get; set; }

        public string Fax { get; set; }

        public string Address { get; set; }

        public string UseYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        Database db = new Database();
        string query = "";

        public DataTable Select_WareHouse_Lookup(string type)
        {
            query = $@"SELECT HouseID AS 분류코드, Title as 분류명 FROM CM_Warehouse WHERE CorpId = '{WrGlobal.CorpID}' ";

            if (type == "제품") query += $@"AND HouseId != 'S0001'";
            else if (type == "자재") query += $@"AND HouseId != 'S0002'";
            else if (type == "공정") query += $@"AND WareType = '30'";

            return db.GetDataTable(query);
        }

        public DataTable Select_LupWareHouse(bool AllYn = false, string[] type = null)
        {
            query = "";

            if (AllYn)
            {
                query += $@"SELECT 'ALL' 창고코드, '전체' 창고명 
                            UNION ALL
                            ";
            }

            query += $@"SELECT HouseID 창고코드, Title 창고명
                        FROM CM_Warehouse
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND UseYn = 'Y'
                    ";

            if (type != null) query += $"\n AND WareType IN ('{string.Join("','", type)}')";

            query += $"\n ORDER BY HouseID ";

            return db.GetDataTable(query);
        }
    }
}
