using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Common
{
    public class clsDocument
    {
        Database db = new Database();
        string query;

        public DataView GetDocumentCommonClass()
        {
            query = string.Format("Select CLASS, CLASSNAME, HIGHCLASS FROM CM_CLASS Where Class like 'Z0%'");
            return db.GetDataView("공통문서분류", query);
        }

        public DataView GetFileItemCode(string strClass)
        {
            query = string.Format("Select ItemCode, Title FROM CM_CateItem Where Class = '{0}' and UseYn = 'Y'", strClass);
            return db.GetDataView("디자인문서분류", query);
        }

        public DataView GetFileImageCode()
        {
            query = string.Format("Select ItemCode, Title FROM CM_CateItem Where Class = '102020' and UseYn = 'Y' and ItemCOde='10'");
            return db.GetDataView("이미지 코드분류", query);
        }
        public int GetNextFileSeq(string tableName, string filter)
        {
            query = string.Format("Select isnull(max(FileSeq), 0) + 1 seq From {0} Where {1}", tableName, filter);
            DataRowView drv = db.GetDataRecord(query);

            return (int)drv[0];
        }

        //작업표준서
        public DataView GetWorkStandards()
        {
            query = string.Format("Select CLASS, CLASSNAME, HIGHCLASS FROM CM_CLASS Where Class like 'Z040%' and Classlevel = 2");
            return db.GetDataView("작업표준서", query);
        }

    }
}
