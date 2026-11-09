using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartCommon.Common;

namespace SmartDesign.Model
{
    class ProjectRepository
    {
        Database db = new Database();   
        string query = "";
        #region Properties

        #endregion  Properties

        // 프로젝트 분류 조회 
        public DataTable Select_ProjectClass()
        {
            query = $@"
                        Select 
                              CLASS 분류코드, CLASSNAME 분류명, 
                              HIGHCLASS 
                        From CLASS 
                        Where class like 'MD%' AND CLASSLEVEL != 0 AND DisplayYn ='Y' 
                          AND CorpId = '{WrGlobal.CorpID}'
                      ";
            return db.GetDataTable(query);
        }   

        public DataTable Select_ProjectSeason()
        {
            query = $@"SELECT 
                              ItemCode 시즌코드, Title 시즌명
                         FROM CmCateItem 
                        WHERE CorpId = '{WrGlobal.CorpID}' AND class = 'PJ33'";
            return db.GetDataTable(query);
        }

        public DataTable Select_PM_Charge()
        {
                       query = $@"SELECT 
                                          EmployeeId 직원코드, EmployeeName 직원명
                                     FROM CodeUser 
                                    WHERE CorpId = '{WrGlobal.CorpID}' 
                                      AND UseYn = 'Y' 
                                      AND EmployeeId <> 'admin'
                                      AND EmployeeId <> 'Dev'
                    ";
            return db.GetDataTable(query);
        }

        public DataTable Select_Role()
        {
            // 10 PM, 20 PL, 30 팀원
            query = $@"SELECT ItemCode 코드, Title 내용 
                         FROM CmCateItem 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                          AND Class = 'PJ70'
                        ";
            return db.GetDataTable(query);
        }

        public DataTable Select_Permission()
        {
            // 10 편집, 20 조회
            query = $@"SELECT ItemCode 코드, Title 내용 
                         FROM CmCateItem 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                          AND Class = 'PJ80'
                        ";
            return db.GetDataTable(query);
        }

        public DataTable Select_Status()
        {
            // 10 편집, 20 조회
            query = $@"SELECT ItemCode 코드, Title 내용 
                         FROM CmCateItem 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                          AND Class = 'PJ10'
                        ";
            return db.GetDataTable(query);
        }

        public DataTable Select_Material(string[] MaterialType = null)
        {
            query = $@"SELECT MaterialCode AS 품목코드, MaterialName AS 품명
                       FROM CMmaterial 
                       WHERE CorpId = '{WrGlobal.CorpID}' 
                       AND UseYn = 'Y'
                        ";
            //특정 구분만 보고 싶은 경우
            if (MaterialType != null &&
                MaterialType.Length > 0)
            {
                query += "\nAND MaterialType IN (";

                for (int idx = 0; idx < MaterialType.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{MaterialType[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_MaterialGJY()
        {
            query = $@"SELECT MaterialCode AS 품목코드, MaterialName AS 품명
                       FROM CMmaterial 
                       WHERE CorpId = '{WrGlobal.CorpID}' 
                       AND UseYn = 'Y'
                       AND MaterialType = '80'
                        ";
            return db.GetDataTable(query);
        }

    }
}
