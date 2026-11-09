using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.CM
{
    public class CM_Routing
    {
        #region Property
        public string CorpId { get; set; }

        public string RoutingCode { get; set; }

        public int ChildID { get; set; }

        public int ParentID { get; set; }

        public int RoutingSeq { get; set; }

        public int TreeLevel { get; set; }

        public int RowNum { get; set; }

        public string ProcessCode { get; set; }

        public string WorkType { get; set; }

        public string InYn { get; set; }

        public string OutYn { get; set; }

        public string FinalYn { get; set; }

        public string RequireYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public string CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public string ModDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Code_GetRoutingDetail()
        {
            query = $@"dbo.Code_GetRoutingDetail '{WrGlobal.CorpID}', '{RoutingCode}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_CM_Routing()
        {
            query = $@"
            SELECT
                  CorpId
                , RoutingCode
                , RoutingName
                , RoutingType
                , Remark
                , UseYn
                , CreId
                , CreIP
                , CreDt
                , ModId
                , ModIP
                , ModDt
            FROM CM_Routing
            WHERE CorpId = '{WrGlobal.CorpID}'
            ";

            return db.GetDataTable(query);
        }

        public bool Ins_CM_Routing()
		{
            query = $@"
            INSERT INTO CM_Routing
            (
                CorpId
                , RoutingCode
                , ChildID
                , ParentID
                , RoutingSeq
                , TreeLevel
                , RowNum
                , ProcessCode
                , WorkType
                , InYn
                , OutYn
                , FinalYn
                , RequireYn
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                '{WrGlobal.CorpID}'
                , {RoutingCode.ConvertStr_Empty_To_Null()}
                , {ChildID}
                , {ParentID}
                , {RoutingSeq}
                , {TreeLevel}
                , {RowNum}
                , {ProcessCode.ConvertStr_Empty_To_Null()}
                , {WorkType.ConvertStr_Empty_To_Null()}
                , {InYn.ConvertStr_Empty_To_Null()}
                , {OutYn.ConvertStr_Empty_To_Null()}
                , {FinalYn.ConvertStr_Empty_To_Null()}
                , {RequireYn.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            ) ";

            return db.ExecuteQuery(query);
		}

        public bool Upd_CM_Routing()
        {
            query = $@"
                UPDATE CM_Routing
                SET
                      ParentID    = {ParentID}
                    , RoutingSeq  = {RoutingSeq}
                    , TreeLevel   = {TreeLevel}
                    , RowNum      = {RowNum}
                    , ProcessCode = {ProcessCode.ConvertStr_Empty_To_Null()}
                    , WorkType    = {WorkType.ConvertStr_Empty_To_Null()}
                    , InYn        = {InYn.ConvertStr_Empty_To_Null()}
                    , OutYn       = {OutYn.ConvertStr_Empty_To_Null()}
                    , FinalYn     = {FinalYn.ConvertStr_Empty_To_Null()}
                    , RequireYn   = {RequireYn.ConvertStr_Empty_To_Null()}
                    , ModId       = '{WrGlobal.LoginID}'
                    , ModIP       = '{WrGlobal.ClientHostName}'
                    , ModDt       = GETDATE()
                WHERE CorpId      = '{WrGlobal.CorpID}'
                  AND RoutingCode = {RoutingCode.ConvertStr_Empty_To_Null()}
                  AND ChildID     = {ChildID}
                ";

            return db.ExecuteQuery(query);
        }

        public bool Del_CM_Routing()
        {
            query = $@"
               DELETE FROM CM_Routing
               WHERE CorpId      = '{WrGlobal.CorpID}'
                 AND RoutingCode = '{RoutingCode}'
                ";

            return db.ExecuteQuery(query);
        }
    }
}
