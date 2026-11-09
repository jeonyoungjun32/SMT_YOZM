using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Common
{
    class CommonClass
    {
        public string CorpId { get; set; }
        public string CLASS { get; set; }
        public int CLASSLEVEL { get; set; }
        public string CLASSNAME { get; set; }
        public string HIGHCLASS { get; set; }
        public string CLASSFULLNAME { get; set; }
        public string STARTDATE { get; set; }
        public string ENTDATE { get; set; }
        public string ROOTCLASS { get; set; }
        public string DisplayYn { get; set; }

        Database db = new Database();
        string query = "";

        public DataTable Get_InspectPeriod()
        {
            query = $@"SELECT ItemCode 코드, Title 명칭 FROM CM_CateItem WHERE CLASS = '204050'";

            return db.GetDataTable(query);
        }

        public DataTable Get_ValueTypeDivCd()
        {
            query = $@"SELECT Class 코드, ClassName 명칭 FROM CM_CLASS WHERE CLASSLEVEL = '2' AND HIGHCLASS = '10' ";

            return db.GetDataTable(query);
        }

        public DataTable Get_User()
        {
            query = $@"SELECT EmployeeId 코드, EmployeeName 명칭 FROM SM_User WHERE EmployeeId <> 'admin'";

            return db.GetDataTable(query);
        }
    }
}
