using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.DM
{
    public class DM_Weather
    {
        public string SectionCode { get; set; }
        public string UseYn { get; set; }

        Database db = new Database();
        string query = "";

        public DataTable Select_LupSectionCode(bool isAll = false)
        {
            query = "";
            if (isAll)
            {
                query += $@"
                            SELECT 'ALL' AS [행정코드],
                                   '전체' AS [지역]

                            UNION ALL
                           ";
            }

            query += $@"
                            SELECT SectionCode AS [행정코드],
	                               Depth1 + ' ' + Depth2 + IIF(Depth3 IS NULL, '', ' ' + Depth3) AS [지역]
                            FROM [API].[dbo].WX_Section
                       ";

            return db.GetDataTable(query);
        }

        public DataTable Select_Weather(DateTime startDate, DateTime endDate, string sectionCode)
        {
            string stDate = startDate.ToString("yyyy-MM-dd");
            string edDate = endDate.ToString("yyyy-MM-dd");

            query = $@"
                            SELECT Date,
                                   SectionCode,
                                   Prcp,
                                   AvgTemp,
                                   MinTemp,
                                   MaxTemp
                            FROM DM_Weather
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND ('' = '{sectionCode}' OR SectionCode = '{sectionCode}')
                            AND Date BETWEEN '{stDate}' AND '{edDate}'
                      ";

            return db.GetDataTable(query);
        }

        public DataTable Select_Weather_Request()
        {
            query = $@"
                            SELECT SectionCode,
                                   UseYn
                            FROM [API].[dbo].WX_Request
                            WHERE DbName = '{Database.DbName}'
                            AND CorpId = '{WrGlobal.CorpID}'
                      ";

            return db.GetDataTable(query);
        }

        public bool Insert_Updated_Request()
        {
            query = $@"
                            MERGE INTO [API].[dbo].WX_Request AS T
                            USING ( VALUES (
                                    '{Database.DbName}',
                                    '{WrGlobal.CorpID}',
                                    '{SectionCode}',
                                    '{UseYn}'
                            )) AS S (DbName, CorpId, SectionCode, UseYn)

                            ON T.DbName = S.DbName AND T.CorpId = S.CorpId AND T.SectionCode = S.SectionCode

                            WHEN MATCHED THEN
                                UPDATE SET T.UseYn = S.UseYn

                            WHEN NOT MATCHED THEN
                                INSERT (DbName, CorpId, SectionCode, UseYn, CollectSynoptic)
                                VALUES (S.DbName, S.CorpId, S.SectionCode, S.UseYn, 'Y');
                      ";

            return db.ExecuteQuery(query);
        }
    }
}
