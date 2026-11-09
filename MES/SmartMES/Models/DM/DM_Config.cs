using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.DM
{
    public class DM_Config
    {
        #region Property
        public string AlertCode { get; set; }
        public string AlertName { get; set; }
        /// <summary>
        /// 공통코드 D11030
        /// </summary>
        public string AlertType { get; set; }
        /// <summary>
        /// 공통코드 D11040
        /// </summary>
        public string CompareBase { get; set; }
        public decimal Rate { get; set; }
        public string UseYn { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_Config(string alertType, string useYn)
        {
            query = $@"
                            SELECT '' AS Status,
                                   AlertCode,
                                   AlertName,
                                   AlertType,
                                   CompareBase,
                                   Rate,
                                   UseYn
                            FROM DM_Config
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND ('' = '{alertType}' OR AlertType = '{alertType}')
                            AND ('' = '{useYn}' OR UseYn = '{useYn}')
                      ";

            return db.GetDataTable(query);
        }

        public bool Insert_Config()
        {
            query = $@"    
                            DECLARE @pAlertCode VARCHAR(20) = '',
                                    @pLastSeq INT = 0

                            SELECT TOP 1 @pLastSeq = CONVERT(INT, REPLACE(AlertCode, 'AC', ''))
                            FROM DM_Config
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            ORDER BY AlertCode DESC

                            SET @pLastSeq = @pLastSeq + 1
                            SET @pAlertCode = 'AC' + FORMAT(@pLastSeq, '0000')

                            INSERT INTO DM_Config (
                                                        CorpId
                                                        , AlertCode
                                                        , AlertName
                                                        , AlertType
                                                        , CompareBase
                                                        , Rate
                                                        , UseYn
                                                        , CreId
                                                        , CreIP
                                                        , CreDt
                                                  )
                                           VALUES (
                                                        '{WrGlobal.CorpID}'
                                                        , @pAlertCode
                                                        , '{AlertName}'
                                                        , '{AlertType}'
                                                        , '{CompareBase}'
                                                        , {Rate}
                                                        , '{UseYn}'
                                                        , '{WrGlobal.LoginID}'
                                                        , HOST_NAME()
                                                        , GETDATE()
                                                  )
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Update_Config()
        {
            query = $@"
                            UPDATE DM_Config
                            SET AlertType = '{AlertType}',
                                AlertName = '{AlertName}',
                                CompareBase = '{CompareBase}',
                                Rate = {Rate},
                                UseYn = '{UseYn}',
                                ModId = '{WrGlobal.LoginID}',
                                ModIP = HOST_NAME(),
                                ModDt = GETDATE()
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND AlertCode = '{AlertCode}'
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Delete_Config()
        {
            query = $@"
                            DELETE FROM DM_Config
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND AlertCode = '{AlertCode}'
                      ";

            return db.ExecuteQuery(query);
        }
    }
}
