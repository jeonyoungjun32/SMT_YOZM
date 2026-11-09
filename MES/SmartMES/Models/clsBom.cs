using SmartMES.Common;
using SmartMES.Common;
using System;
using System.Data;
using SmartCommon.Common;

namespace SmartMES.Models
{
    //BOM
    class clsBom
    {
        public string ProcessCd { get; set; }
        public string MtCode { get; set; }
        public string itemDiv { get; set; }

        Database db = new Database();
        string query;

        public string Select_ProcessTitle()
        {
            DataTable dt = null;
            string title = "";

            query = $@"
                        SELECT (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','401050', cp.ProcessType)) AS Title
                        FROM CM_Process cp 
                        WHERE cp.CorpId = '{WrGlobal.CorpID}'
                        AND cp.ProcessCode = '{ProcessCd}'
                ";

            dt = db.GetDataTable(query);
            
            if(dt != null)
			{
                if (dt.Rows.Count != 0) { title = dt.Rows[0][0].ToString(); }
			}

            return title;
        }

        public DataTable Select_Material()
        {
            query = $@"
            SELECT * FROM CM_Item WHERE ItemCode = '{MtCode}'
            ";

            return db.GetDataTable(query);
        }

        //BOM관리
        public DataView Select_Materialt_List()
        {
            query = $@"
                       SELECT cm.ItemCode, cm.ItemName, cm.UnitCode, cm.ItemType,cc.Title ItemTypeName , cm.Size
                       FROM CM_Item cm
                       Inner Join CM_CateItem cc on cm.ItemType = cc.ClassItemCode and cc.CLASS = 'A010'  --Z000 
                       Where cm.CorpId = '{WrGlobal.CorpID}' 
                       and cm.ItemType not in ('10', '20')
                       ";

            if (itemDiv.ConvertString_Null_To_Empty().ToUpper() != "ALL" && itemDiv.ConvertString_Null_To_Empty() != "")
            {
                query += $" AND cm.ItemType = '{itemDiv}' and cm.UseYn ='Y' ";
            }

            query += $" order by cm.ItemType,cm.ItemCode ";

            return db.GetDataView("BOM자재", query);
        }
    }
}
