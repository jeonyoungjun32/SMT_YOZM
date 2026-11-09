using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCommon.Common;

namespace SmartMES.Models.CM
{
    class CM_BOM
    {
        #region Property
        public string CorpId { get; set; }

        public string PItemCode { get; set; }

        public string Revision { get; set; }

        public int ChildID { get; set; }

        public int ParentID { get; set; }

        public float Qty { get; set; }

        public string MItemCode { get; set; }

        public string MItemName { get; set; }

        public string ProcessCode { get; set; }

        public string ProcessName { get; set; }

        public string WorkType { get; set; }

        public int RoutingSeq { get; set; }

        public string UnitCode { get; set; }

        public int TreeLevel { get; set; }

        public int RowNum { get; set; }

        public string ProcessType { get; set; }

        public string ItemTypeName { get; set; }

        public float CalcQty { get; set; }

        public string Remark { get; set; }


        //
        #endregion Property End

        Database db = new Database();
        string query = "";

        public DataTable Code_GetBomRevision()
        {
            query = $@"

	            SELECT 
                    cb.CorpId
                    , cb.PItemCode
                    , cb.Revision
                    , cb.ChildID
                    , cb.ParentID
                    , cb.Qty
                    , cb.MItemCode
                    , cb.MItemName
                    , cb.ProcessCode
                    , cb.ProcessName
                    , cb.WorkType
                    , cb.RoutingSeq
                    , cb.UnitCode
                    , cb.TreeLevel
                    , cb.RowNum
                    , cb.ProcessType
                    --, cb.ItemTypeName
                    , cb.CalcQty
                    , cb.Remark
                    , cb.CreId
                    , cb.CreIP
                    , cb.CreDt
                    , cb.ModId
                    , cb.ModIP
                    , cb.ModDt
                    
                    , iif(cb.ProcessName is null, '', '(' + cb.ProcessName + ') ') + isnull(cb.MItemName, '') DisplayName
                    , (select dbo.[FN_Get_CateItem_Title] ('{WrGlobal.CorpID}', 'A010', cm.ItemType)) ItemTypeName
                    , cb.RequiredQty
                    , cb.ActualQty
                    , cb.SupplyType
                    , cb.IssueType
                    , cb.InputProcessName
                    , cb.BomStartDate
                    , cb.BomEndDate

	            FROM CM_BOM cb	
	            LEFT JOIN CM_Item cm on cm.CorpId = cb.CorpId AND cm.ItemCode = cb.MItemCode

	            Where cb.CorpId = '{WrGlobal.CorpID}' 
                and cb.PItemCode = '{PItemCode}' 
                and cb.Revision = '{Revision}'
	            order by ProcessType DESC, ParentID, ChildID

                ";
            return db.GetDataTable(query);
        }

        public bool InsertBOM()
		{
            query = $@"

            INSERT INTO CM_BOM
            (
                CorpId
                , PItemCode
                , Revision
                , ChildID
                , ParentID
                , Qty
                , MItemCode
                , MItemName
                , ProcessCode
                , ProcessName
                , WorkType
                , RoutingSeq
                , UnitCode
                , TreeLevel
                , RowNum
                , ProcessType

                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                 '{WrGlobal.CorpID}'
                 , '{PItemCode}'
                 , '{Revision}'
                 , '{ChildID}'
                 , '{ParentID}'
                 , '{Qty}'
                 , '{MItemCode}'
                 , '{MItemName}'
                 , {ProcessCode.ConvertStr_Empty_To_Null()}
                 , {ProcessName.ConvertStr_Empty_To_Null()}
                 , {WorkType.ConvertStr_Empty_To_Null()}
                 , '{RoutingSeq}'
                 , {UnitCode.ConvertStr_Empty_To_Null()}
                 , '{TreeLevel}'
                 , '{RowNum}'
                 , {ProcessType.ConvertStr_Empty_To_Null()}

                 , '{WrGlobal.LoginID}'
                 , '{WrGlobal.ClientIpAddress}'
                 , GETDATE()
            )
            ";

            return db.ExecuteQuery(query);
		}

        public bool InsertBOMByRoutingCode(string routingCode)
        {
            query = $@"

            INSERT INTO CM_BOM
            (
                CorpId
                , PItemCode
                , Revision
                , ChildID
                , ParentID
                , Qty
                , MItemCode
                , MItemName
                , ProcessCode
                , ProcessName
                , WorkType
                , RoutingSeq
                , UnitCode
                , TreeLevel
                , RowNum
                , ProcessType

                , CreId
                , CreIP
                , CreDt
            )
            SELECT
                  cm.CorpId
                , cm.ItemCode
                , '{Revision}'
                , cr.ChildID
                , cr.ParentID
                , 1
                , CASE WHEN cr.TreeLevel = 0 THEN cm.ItemCode ELSE NULL END
                , CASE WHEN cr.TreeLevel = 0 THEN cm.ItemName ELSE NULL END
                , cr.ProcessCode
                , cp.ProcessName
                , cr.WorkType
                , cr.RoutingSeq
                , CASE WHEN cr.TreeLevel = 0 THEN cm.UnitCode ELSE NULL END
                , cr.TreeLevel
                , cr.RowNum
                , 'P'

                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientIpAddress}'
                , GETDATE()
            FROM CM_Item cm
            INNER JOIN CM_Routing cr
                ON cm.CorpId = cr.CorpId
            INNER JOIN CM_Process cp
                ON cr.CorpId = cp.CorpId
                AND cr.ProcessCode = cp.ProcessCode
            WHERE cm.CorpId = '{WrGlobal.CorpID}'
            AND cm.ItemCode = '{PItemCode}'
            AND cr.RoutingCode = '{routingCode}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool DeleteBOMByMItemCode()
        {
            query = $@"
            DELETE CM_BOM
            WHERE CorpId = '{WrGlobal.CorpID}'
            AND PItemCode = '{PItemCode}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
