using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.DataProcessing.InMemoryDataProcessor;

using SmartCommon.Common;

namespace SmartMES.Models
{
    class ProjectDetail
    {
        #region Properties
        public string CorpID { get; set; }
        public string ProjectID { get; set; }
        public string PJiD { get; set; }
        public string TItle { get; set; }
        public string Type { get; set; }
        public string DateType { get; set; }
        public string WorkDays { get; set; }
        public string GroupLevel { get; set; }
        public string GroupYn { get; set; }
        public string Charge { get; set; }
        public string PMName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PlanStartDate { get; set; }
        public DateTime? PlanEndDate { get; set; }
        public string Status { get; set; }
  
        // 조회 조건용 Properties
        public DateTime? SearchStartDate { get; set; }
        public DateTime? SearchEndDate { get; set; }
        public string SearchProjectType { get; set; }
        public string SearchLoginID { get; set; }

        public string PlanYYYYMM { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public bool InsertOrUpdateProjectDetail()
        {
            try
            {
                // 존재 여부 확인
                if (CheckProjectIDExists())
                {
                    // Update
                    query = $@"
                                UPDATE PJ_ProjectD SET 
                                    Type = '{Type}',
                                    DateType = '{DateType}',
                                    Charge = '{Charge}',
                                    PMName = '{PMName}',
                                    StartDate = {(StartDate.HasValue ? $"'{StartDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    EndDate = {(EndDate.HasValue ? $"'{EndDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    PlanStartDate = {(PlanStartDate.HasValue ? $"'{PlanStartDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    PlanEndDate = {(PlanEndDate.HasValue ? $"'{PlanEndDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    Status = '{Status}'
                                WHERE CorpID = '{CorpID}' AND ProjectID = '{ProjectID}'";
                }
                else
                {
                    // Insert
                    query = $@"
                                INSERT INTO PJ_ProjectD (
                                    CorpId, ProjectID, PjID, Title, Type, DateType, WorkDays, GroupLevel, GroupYn, Charge, PMName, 
                                    StartDate, EndDate, PlanStartDate, PlanEndDate, Status)
                                    VALUES (
                                    '{CorpID}', '{ProjectID}', '{PJiD}', '{TItle}', '{Type}', 
                                    '{DateType}', '{WorkDays}', '{GroupLevel}', '{GroupYn}', '{Charge}', '{PMName}',
                                    {(StartDate.HasValue ? $"'{StartDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    {(EndDate.HasValue ? $"'{EndDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    {(PlanStartDate.HasValue ? $"'{PlanStartDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    {(PlanEndDate.HasValue ? $"'{PlanEndDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    '{Status}'
                                )
                            ";
                }

                return db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"프로젝트 저장 중 오류: {ex.Message}");
            }
        }

        public bool CheckProjectIDExists()
        {
            query = $@"
                        SELECT COUNT(*) AS CNT
                        FROM PJ_ProjectD
                        WHERE CorpID = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}'
                    ";
            DataRow dr = db.GetDataRow(query);
            return dr != null && Convert.ToInt32(dr["CNT"]) > 0;
        }

    }
}