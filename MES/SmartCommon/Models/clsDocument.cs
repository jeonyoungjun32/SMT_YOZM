using DevExpress.Xpo.DB.Helpers;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCommon.Models
{
    public class clsDocument
    {
        Database db = new Database();
        string query;

        public DataView GetDocumentCommonClass()
        {
            //query = string.Format("Select CLASS, CLASSNAME, HIGHCLASS FROM CM_CLASS Where Class like 'Z0%'");
            query = $"Select CLASS, CLASSNAME, HIGHCLASS FROM CM_CLASS Where CorpID = '{WrGlobal.CorpID}' and Class like 'Z0%'";
            return db.GetDataView("공통문서분류", query);
        }

        public DataView GetFileItemCode(string strClass)
        {
            query = $"Select ClassItemCode, Title FROM CM_CateItem Where CorpId = '{WrGlobal.CorpID}' and Class = '{strClass}' and UseYn = 'Y'";
            //query = $"Select ItemCode, Title FROM CM_CateItem Where CorpID = '{WrGlobal.CorpID}' and Class = '{strClass}' and UseYn = 'Y'";
            return db.GetDataView("디자인문서분류", query);
        }

        public int GetNextFileSeq(string tableName, string filter)
        {
            //query = string.Format("Select isnull(max(FileSeq), 0) + 1 seq From {0} Where {1}", tableName, filter);
            query = $"Select isnull(max(FileSeq), 0) + 1 seq From {tableName} Where CorpID = '{WrGlobal.CorpID}' and  {filter}";
            DataRowView drv = db.GetDataRecord(query);

            return (int)drv[0];
        }
    }
}
