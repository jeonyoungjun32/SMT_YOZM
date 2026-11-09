using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.CM
{
    public class CM_RoutingRev
    {
        #region Property
        public string CorpId { get; set; }

        public string RoutingCode { get; set; }

        public string RoutingName { get; set; }

        public string RoutingType { get; set; }

        public string Remark { get; set; }

        public string UseYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public string CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public string ModDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_CM_RoutingRev()
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
            FROM CM_RoutingRev
            WHERE CorpId = '{WrGlobal.CorpID}'
            ";

            if (UseYn.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND UseYn = '{UseYn}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_CM_RoutingRev_Lup()
        {
            query = $@"
                SELECT RoutingCode 코드, RoutingName 명칭 
                FROM CM_RoutingRev 
                WHERE CorpId='{WrGlobal.CorpID}' 
                AND UseYn='Y'
            ";

            return db.GetDataTable(query);
        }

        public bool Ins_CM_RoutingRev()
		{
            query = $@"
            INSERT INTO CM_RoutingRev
            (
                CorpId
                , RoutingCode
                , RoutingName
                , RoutingType
                , Remark
                , UseYn
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                '{WrGlobal.CorpID}'
                , '{RoutingCode}'
                , {RoutingName.ConvertStr_Empty_To_Null()}
                , {RoutingType.ConvertStr_Empty_To_Null()}
                , {Remark.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            ) ";

            return db.ExecuteQuery(query);
		}

        public bool Upd_CM_RoutingRev()
        {
            query = $@"
                    UPDATE CM_RoutingRev
                    SET
                          RoutingName = {RoutingName.ConvertStr_Empty_To_Null()}
                        , RoutingType = {RoutingType.ConvertStr_Empty_To_Null()}
                        , Remark      = {Remark.ConvertStr_Empty_To_Null()}
                        , UseYn       = {UseYn.ConvertStr_Empty_To_Null()}
                        , ModId       = '{WrGlobal.LoginID}'
                        , ModIP       = '{WrGlobal.ClientHostName}'
                        , ModDt       = GETDATE()
                    WHERE CorpId = '{WrGlobal.CorpID}'
                      AND RoutingCode = '{RoutingCode}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_CM_RoutingRev()
        {
            query = $@"
               DELETE FROM CM_RoutingRev
               WHERE CorpId      = '{WrGlobal.CorpID}'
                 AND RoutingCode = '{RoutingCode}'
                ";

            return db.ExecuteQuery(query);
        }
    }
}
