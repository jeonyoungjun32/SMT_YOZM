using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.DM
{
    public class DM_Impact
    {
        #region Property
        public string ImpactCode { get; set; }
        public string ImpactType { get; set; }
        public string ImpactName { get; set; }
        public string ItemCode { get; set; }
        public decimal Rate { get; set; }
        public string UseYn { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_Impact_Lup(bool useYn = false, bool isAll = false)
        {
            query = "";
            if (isAll)
            {
                query += $@"
                                SELECT 'ALL' AS [조건코드],
                                       '전체' AS [조건명]

                                UNION ALL
                           ";
            }
            query += $@"
                                SELECT ImpactCode AS [조건코드],
                                       ImpactName AS [조건명]
                                FROM DM_Impact
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                {(useYn ? "AND UseYn = 'Y'" : "")}
                       ";

            return db.GetDataTable(query);
        }

        public DataTable Select_Impact(string impactType, string useYn)
        {
            query = $@"
                            SELECT '' AS Status,
                                   ImpactCode,
                                   ImpactType,
                                   ImpactName,
                                   ItemCode,
                                   Rate,
                                   UseYn
                            FROM DM_Impact
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND ('' = '{impactType}' OR ImpactType = '{impactType}')
                            AND ('' = '{useYn}' OR UseYn = '{useYn}')
                      ";

            return db.GetDataTable(query);
        }

        public bool Insert_Impact()
        {
            query = $@"
                            DECLARE @pImpactCode VARCHAR(20) = '',
                                    @pLastSeq INT = 0

                            SELECT TOP 1 @pLastSeq = CONVERT(INT, REPLACE(ImpactCode, 'IP', ''))
                            FROM DM_Impact
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            ORDER BY ImpactCode DESC

                            SET @pLastSeq = @pLastSeq + 1
                            SET @pImpactCode = 'AC' + FORMAT(@pLastSeq, '0000')

                            INSERT INTO DM_Impact (
                                                        CorpId
                                                        , ImpactCode
                                                        , ImpactType
                                                        , ImpactName
                                                        , ItemCode
                                                        , Rate
                                                        , UseYn
                                                        , CreId
                                                        , CreIP
                                                        , CreDt
                                                   )
                                            VALUES (
                                                        '{WrGlobal.CorpID}'
                                                        , @pImpactCode
                                                        , '{ImpactType}'
                                                        , '{ImpactName}'
                                                        , '{ItemCode}'
                                                        , {Rate}
                                                        , '{UseYn}'
                                                        , '{WrGlobal.LoginID}'
                                                        , HOST_NAME()
                                                        , GETDATE()
                                                   )
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Update_Impact()
        {
            query = $@"
                            UPDATE DM_Impact
                            SET ImpactType = '{ImpactType}',
                                ImpactName = '{ImpactName}',
                                ItemCode = '{ItemCode}',
                                Rate = {Rate},
                                UseYn = '{UseYn}',
                                ModId = '{WrGlobal.LoginID}',
                                ModIP = HOST_NAME(),
                                ModDt = GETDATE()
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND ImpactCode = '{ImpactCode}'
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Delete_Impact()
        {
            query = $@"
                            DELETE FROM DM_Impact
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND ImpactCode = '{ImpactCode}'
                      ";

            return db.ExecuteQuery(query);
        }
    }
}
