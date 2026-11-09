using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmartCommon.Common;

namespace SmartDesign.Model
{
    class DesignPlan
    {
        #region Properties
        public string CorpId { get; set; }

        public string SearchStartDate { get; set; }

        public string SearchEndDate { get; set; }

        public string ProjectID { get; set; }

        public string PjID { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string DateType { get; set; }

        public string GroupYn { get; set; }

        public int GroupLevel { get; set; }

        public string WorkDays { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? PlanStartDate { get; set; }

        public DateTime? PlanEndDate { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        // 프로젝트 품목 조회
        public DataTable SelectProjectMasterList()
        {
            query = $@"
                        SELECT 
                                 A.CorpId
                               , A.CLASS
                               , A.ProjectID
                               , A.ProjectType
                               , A.Type
                               , A.Charge
                               , A.ProjectName
                               , B.EmployeeName
                          FROM ProjectMaster A
                         INNER JOIN CodeUser B on A.CorpID = B.CorpID AND A.Charge = B.EmployeeId
                         WHERE A.CorpId = '{WrGlobal.CorpID}' 
                           AND A.Startdate between  '{SearchStartDate}' and '{SearchEndDate}'
                    ";
            return db.GetDataTable(query);
        }

        //세부리스트 목표일자 변경
        public bool ChangePlanEndDate()
        {
            string planStartDateSql = PlanStartDate.HasValue ? $"'{PlanStartDate:yyyy-MM-dd}'" : "NULL";
            string planEndDateSql = PlanEndDate.HasValue ? $"'{PlanEndDate:yyyy-MM-dd}'" : "NULL";

            query = $@"
                        IF EXISTS (
                            SELECT PjID 
                              FROM ProjectDetail 
                             WHERE CorpId = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}' AND PjID = '{PjID}'
                        )
                        BEGIN
                            UPDATE ProjectDetail
                               SET PlanStartDate = {planStartDateSql},
                                   PlanEndDate = {planEndDateSql}
                             WHERE CorpId = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}' AND PjID = '{PjID}'
                        END
                    ";
            return db.ExecuteQuery(query);
        }

        //세부리스트 수정
        public bool ProjectDetailUpdate()
        {
            string startDateSql = StartDate.HasValue? $"'{StartDate:yyyy-MM-dd}'" : "NULL";
            string endDateSql = EndDate.HasValue? $"'{EndDate:yyyy-MM-dd}'" : "NULL"; 

            query = $@"
                        IF EXISTS (
                            SELECT PjID 
                              FROM ProjectDetail 
                             WHERE CorpId = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}' AND PjID = '{PjID}'
                        )
                        BEGIN
                            UPDATE ProjectDetail
                               SET StartDate = {startDateSql},
                                   EndDate = {endDateSql},
                                   Status = case when '{Status}' = '대기' then '10'
                                                 when '{Status}' = '진행중' then '20'
                                                 when '{Status}' = '완료' then '30'
                                             else '10' end
                             WHERE CorpId = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}' AND PjID = '{PjID}'
                        END
                    ";
            return db.ExecuteQuery(query);
        }

        //프로젝트 마스터 삭제
        public bool Del_ProjectMaster()
        {

            query = $@"
                        IF EXISTS (
                            SELECT *
                              FROM ProjectMaster 
                             WHERE CorpId = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}' 
                        )
                        BEGIN
                             DELETE FROM ProjectMaster
                             WHERE CorpId = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}' 
                        END
                    ";
            return db.ExecuteQuery(query);
        }

        //프로젝트 디테일 삭제
        public bool Del_ProjectDetail()
        {

            query = $@"
                        IF EXISTS (
                            SELECT *
                              FROM ProjectDetail 
                             WHERE CorpId = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}' 
                        )
                        BEGIN
                             DELETE FROM ProjectDetail
                             WHERE CorpId = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}' 
                        END
                    ";
            return db.ExecuteQuery(query);
        }
    }
}