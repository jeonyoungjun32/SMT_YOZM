using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartCommon.Common;

namespace SmartMES.Models
{
    class ProjectMember
    {

        #region Properties
        public string CorpId { get; set; }
        public string ProjectID { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public DateTime CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public DateTime ModDt { get; set; }

        // Detail Properties  
        public string EmployeeId { get; set; }
        public string WorkJob { get; set; }
        public string WorkType { get; set; }
        public string DetailRemark { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        #region 프로젝트 멤버 테이블 (프로젝트 담당 멤버) 
        // 프로젝트 멤버 조회
        public DataTable SelectProjectMember(string projectID)
        {
            query = $@"
                        SELECT 
                             '' as Status
                              pm.CorpId,
                              pm.ProjectID,
                              pm.EmployeeId,
                              cu.EmployeeName,
                              pm.WorkJob,
                              ci1.Title AS RoleName,
                              pm.WorkType,
                              ci2.Title AS PermissionName,
                              pm.Remark
                        FROM PJ_ProjectMember pm
                        LEFT JOIN SM_User cu ON pm.CorpId = cu.CorpId AND pm.EmployeeId = cu.EmployeeId
                        LEFT JOIN CM_CateItem ci1 ON ci1.CorpId = pm.CorpId AND ci1.Class = 'PJ70' AND ci1.ClassItemCode = pm.WorkJob
                        LEFT JOIN CM_CateItem ci2 ON ci2.CorpId = pm.CorpId AND ci2.Class = 'PJ80' AND ci2.ClassItemCode = pm.WorkType
                        WHERE pm.CorpId = '{WrGlobal.CorpID}' AND pm.ProjectID = '{projectID}'
                        ORDER BY pm.WorkType
                    ";
            return db.GetDataTable(query);
        }

        public bool InsProjectMember()
        {
            query = $@"
                        INSERT INTO PJ_ProjectMember (CorpId, ProjectID, EmployeeId, WorkJob, WorkType, Remark)
                                              VALUES ('{CorpId}' , '{ProjectID}', '{EmployeeId}', '{WorkJob}', '{WorkType}', '{Remark}') ";
            return db.ExecuteQuery(query);
        }

        // 기존 프로젝트 멤버 삭제 (필요시)
        public bool DelProjectMember(string projectID)
        {
            query = $@"
                        DELETE FROM PJ_ProjectMember 
                         WHERE CorpId = '{CorpId}' 
                           AND ProjectID = '{projectID}'
            ";
            return db.ExecuteQuery(query);
        }
        #endregion 
    }
}