using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartCommon.Common;

namespace SmartDesign.Model
{
    class CodeProject
    {

        #region Properties
        public string CorpId { get; set; }

        public string CLASS { get; set; }

        public string PjID { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string DateType { get; set; }

        public string GroupYn { get; set; }
        
        public string WorkDays { get; set; }

        public string Remark { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        // 프로젝트 품목 조회
        public DataTable SelectCodeProjectList(string CLASS)
        {
            query = $@"
                        SELECT 
                               CorpId
                               , CLASS
                               , PjID
                               , Title
                               , Type
                               , DateType
                               , WorkDays
                               , GroupYn
                               , GroupLevel
                               , Remark
                          FROM CodeProject 
                         WHERE CorpId = '{WrGlobal.CorpID}' 
                           AND CLASS = '{CLASS}'
                    ";
            return db.GetDataTable(query);
        }

        // 프로젝트 품목 저장 (INSERT/UPDATE)
        public bool InsUpd_CodeProjectList()
        {
            query = $@"
                        IF EXISTS (
                            SELECT PjID 
                              FROM CodeProject 
                             WHERE CorpId = '{WrGlobal.CorpID}' AND CLASS = '{CLASS}' AND PjID = '{PjID}'
                        )
                        BEGIN
                            UPDATE CodeProject
                               SET PjID = '{PjID}',
                                   Title = '{Title}',
                                   Type = '{Type}' ,
                                   DateType = '{DateType}',
                                   WorkDays = '{WorkDays}',
                                   GroupYn = '{GroupYn}' ,
                                  GroupLevel = 
                                               CASE 
                                                   WHEN '{GroupYn}' = 'Y' THEN 0
                                                   WHEN '{GroupYn}' = 'N' THEN 1
                                                   ELSE GroupLevel
                                               END,
                                   Remark = '{Remark}'
                            WHERE CorpId = '{WrGlobal.CorpID}' AND CLASS = '{CLASS}' AND PjID = '{PjID}'
                        END
                        ELSE
                        BEGIN
                            INSERT INTO CodeProject (
                                CorpId, CLASS, PjID, Title, Type, DateType, WorkDays, GroupYn, GroupLevel, Remark
                            )
                            VALUES (
                                        '{WrGlobal.CorpID}', '{CLASS}', '{PjID}', '{Title}', '{Type}', '{DateType}', '{WorkDays}', '{GroupYn}', 
                                              CASE 
                                                   WHEN '{GroupYn}' = 'Y' THEN 0
                                                   WHEN '{GroupYn}' = 'N' THEN 1
                                                   ELSE NULL  
                                               END, '{Remark}'
                                   )
                        END
                    ";
            return db.ExecuteQuery(query);
        }

        // 프로젝트 품목 삭제
        public bool Del_CodeProjectList()
        {
            query = $@"
                        DELETE 
                          FROM CodeProject 
                         WHERE CorpId = '{CorpId}' AND CLASS = '{CLASS}' AND PjID = '{PjID}'
                    ";
            return db.ExecuteQuery(query);
        }


    }
}