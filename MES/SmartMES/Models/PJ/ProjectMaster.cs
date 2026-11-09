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
    class ProjectMaster
    {
        #region Properties
        public string CorpID { get; set; }
        public string ProjectID { get; set; }
        public string ProjectType { get; set; }
        public string CLASS { get; set; }
        public string Type { get; set; }
        public string Charge { get; set; }
        public string PMName { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Remark { get; set; }

        public string ItemCode { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public DateTime? CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public DateTime? ModDt { get; set; }
        public byte[] ProductImage { get; set; }
        public int FileSeq { get; set; }    

        // 조회 조건용 Properties
        public DateTime? SearchStartDate { get; set; }
        public DateTime? SearchEndDate { get; set; }
        public string SearchProjectType { get; set; }
        public string SearchLoginID { get; set; }

        public string PlanYYYYMM { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        // 프로젝트 마스터 목록 조회
        public DataTable SelectProjectMasterList()
        {
            query = $@"
                        SELECT 
                            pm.CorpID,
                            pm.ProjectID,
                            pm.ProjectType,
                            pm.CLASS,
                            cl.CLASSNAME AS ClassName,
                            pm.Type,
                            pm.Charge,
                            cu1.EmployeeName AS ChargeName,
                            pm.PMName,
                            cu2.EmployeeName AS PMEmployeeName,
                            pm.ProjectName,
                            pm.StartDate,
                            pm.EndDate,      
							-- 소요일 계산 (CodeProject의 WorkDays 합계) ~> 시작, 종료일자 차이로 변경(케이엔피)
							DATEDIFF(DAY, pm.StartDate, pm.EndDate) AS DurationDate,  --ISNULL(cp.TotalWorkDays, 0) AS DurationDate,
                            pd.PlanStartDate,
                            pd.PlanEndDate,
                            pm.Remark,
                            pm.ItemCode,
                            pm.ProductImage,
                            pm.CreId,
                            pm.CreIP,
                            pm.CreDt,
                            pm.ModId,
                            pm.ModIP,
                            pm.ModDt,
                            -- 권한 체크를 위한 WorkType 조회
                            CASE 
                                WHEN pm.Charge = '{WrGlobal.LoginID}' OR pm.PMName = '{WrGlobal.LoginID}' THEN '00'
                                WHEN EXISTS (
                                    SELECT 1 FROM PJ_ProjectMember pm2 
                                    WHERE pm2.CorpID = pm.CorpID 
                                    AND pm2.ProjectID = pm.ProjectID 
                                    AND pm2.EmployeeId = '{WrGlobal.LoginID}' 
                                    AND pm2.WorkType = '10'
                                ) THEN '10'
                                ELSE '20'
                            END AS WorkType
                        FROM PJ_ProjectM pm
                        LEFT JOIN PJ_ProjectD pd ON pm.CorpId = pd.CorpId AND pm.ProjectID = pd.ProjectID
                        LEFT JOIN CM_CLASS cl ON pm.CorpID = cl.CorpId AND pm.CLASS = cl.CLASS
                        LEFT JOIN SM_User cu1 ON pm.CorpID = cu1.CorpId AND pm.Charge = cu1.EmployeeId
                        LEFT JOIN SM_User cu2 ON pm.CorpID = cu2.CorpId AND pm.PMName = cu2.EmployeeId
                        WHERE pm.CorpID = '{WrGlobal.CorpID}'
                      ";

            // 프로젝트 타입 조건
            if (!string.IsNullOrEmpty(SearchProjectType))
            {
                query += $" AND pm.ProjectType = '{SearchProjectType}'";
            }

            // 날짜 조건
            if (SearchStartDate.HasValue)
            {
                query += $" AND CONVERT(CHAR(4), pd.PlanStartDate, 112) = '{SearchStartDate.Value:yyyy}' OR CONVERT(CHAR(4), pd.PlanEndDate, 112) = '{SearchStartDate.Value:yyyy}'";
            }

            query += " ORDER BY pm.CreDt DESC";

            return db.GetDataTable(query);
        }

        public DataTable Select_ProjectDetailView()
        {
            query = $@"
                        DECLARE @YM varchar(10) = '{PlanYYYYMM}'  
                        --DECLARE @StartOfMonth date = CAST(@YM + '01' AS date)
                        --DECLARE @EndOfMonth date = EOMONTH(@StartOfMonth)

                        SELECT 
                            MIN(PM.ProjectID) as ProjectID,  -- 대표 ProjectID
                            PM.ProjectName,
                            PM.ItemCode,			
                            COUNT(DISTINCT PM.ItemCode) as Qty, -- 서로 다른 품목 수
                            MIN(PM.Charge) as Charge,				
                            CASE 
                                WHEN COUNT(PD.EndDate) = COUNT(*) THEN 'C'	  -- 모든 절차가 완료
                                WHEN MIN(PD.StartDate) <= GETDATE() THEN 'O' -- 진행중
                                ELSE 'P' -- 대기
                            END as Status,
    
                            -- 납기준수율 (완료된 항목 중 목표일자 내 완료한 비율)
                            CASE 
                                WHEN COUNT(CASE WHEN PD.EndDate IS NOT NULL THEN 1 END) = 0 THEN 0
                                ELSE ROUND(
                                    CAST(COUNT(CASE WHEN PD.EndDate IS NOT NULL AND PD.EndDate <= PD.PlanEndDate THEN 1 END) AS FLOAT) / 
                                    CAST(COUNT(CASE WHEN PD.EndDate IS NOT NULL THEN 1 END) AS FLOAT) * 100, 2)
                            END as DeliveryComplianceRate,
                            -- 진척율 계산 (완료일자-시작일자)/(목표일자-계획시작일자)
	                        CASE 
		                        WHEN MAX(PD.PlanEndDate) IS NULL OR MIN(PD.PlanStartDate) IS NULL THEN 0
		                        WHEN COUNT(PD.EndDate) = COUNT(*) THEN 100 
		                        WHEN MAX(PD.EndDate) IS NOT NULL THEN 
			                        ROUND(
				                        CASE 
					                        WHEN DATEDIFF(DAY, MIN(PD.PlanStartDate), MAX(PD.PlanEndDate)) = 0 THEN 0
					                        ELSE CAST(DATEDIFF(DAY, MIN(PD.PlanStartDate), MAX(PD.EndDate)) AS FLOAT) / 
						                            CAST(DATEDIFF(DAY, MIN(PD.PlanStartDate), MAX(PD.PlanEndDate)) AS FLOAT) * 100
				                        END, 2)
		                        ELSE 
			                        ROUND(
				                        CASE 
					                        WHEN DATEDIFF(DAY, MIN(PD.PlanStartDate), MAX(PD.PlanEndDate)) = 0 THEN 0
					                        ELSE CAST(DATEDIFF(DAY, MIN(PD.PlanStartDate), GETDATE()) AS FLOAT) / 
						                            CAST(DATEDIFF(DAY, MIN(PD.PlanStartDate), MAX(PD.PlanEndDate)) AS FLOAT) * 100
				                        END, 2)
	                        END as ProgressRate,
	                        ---- 진척수량
	                        --CASE 
	                        --	WHEN COUNT(*) = 0 THEN 0
	                        --	ELSE ROUND(
	                        --		CAST(COUNT(CASE WHEN PD.EndDate IS NOT NULL THEN 1 END) AS FLOAT) / 
	                        --		CAST(COUNT(*) AS FLOAT) * 100, 2)
	                        --END as ProgressCount,
    
                            -- 절차수량 (GroupYn이 'Y'가 아닌 항목들의 수)
                            COUNT(CASE WHEN ISNULL(PD.GroupYn, 'N') = 'N' THEN 1 END) as ProcessCount,
    
                            -- 지연수량 (기간내 완료 못한 수량)
                            COUNT(CASE 
			                            WHEN PD.EndDate IS NOT NULL AND PD.EndDate > PD.PlanEndDate THEN 1
			                            WHEN PD.EndDate IS NULL AND PD.StartDate IS NOT NULL AND GETDATE() > PD.PlanEndDate THEN 1
		                            END) as DelayCount,
	                        -- 기간내완료수량
	                        COUNT(CASE  
			                            WHEN PD.EndDate IS NOT NULL AND PD.EndDate <= PD.PlanEndDate THEN 1
			                            WHEN PD.StartDate IS NOT NULL AND PD.StartDate <= PD.PlanStartDate THen 1
		                            END) as CompleteCount,
                            MIN(PM.StartDate) as StartDate,
                            MAX(PD.PlanEndDate) as PlanEndDate,  -- 가장 늦은 목표일자
                            MAX(PD.EndDate) as EndDate			 -- 가장 늦은 완료일자 

                        FROM PJ_ProjectM PM
                        LEFT JOIN PJ_ProjectD PD ON PM.CorpID = PD.CorpID AND PM.ProjectID = PD.ProjectID
                        WHERE PM.CorpID = '{WrGlobal.CorpID}'
                            --AND (
                            --    (PM.StartDate >= @StartOfMonth AND PM.StartDate <= @EndOfMonth)
                            --    OR
                            --    (PM.EndDate >= @StartOfMonth AND PM.EndDate <= @EndOfMonth)
                            --    OR
                            --    (PM.StartDate <= @StartOfMonth AND PM.EndDate >= @EndOfMonth)
                            --)
                            AND CONVERT(CHAR(4), PD.PlanStartDate, 112) = @YM OR CONVERT(CHAR(4), PD.PlanEndDate, 112) = @YM
    
                        GROUP BY PM.ProjectName , PM.ItemCode
                        ORDER BY PM.ProjectName
                    ";

            return db.GetDataTable(query);
        }

        public bool InsertOrUpdateProjectMaster()
        {
            try
            {
                // 존재 여부 확인
                if (CheckProjectIDExists())
                {
                    // Update
                    query = $@"
                                UPDATE PJ_ProjectM SET 
                                    CLASS = '{CLASS}',
                                    Type = '{Type}',
                                    Charge = '{Charge}',
                                    PMName = '{PMName}',
                                    ProjectName = '{ProjectName}',
                                    StartDate = {(StartDate.HasValue ? $"'{StartDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    EndDate = {(EndDate.HasValue ? $"'{EndDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    Remark = '{Remark}',
                                    ItemCode = '{ItemCode}',
                                    ProductImage = @Pic,
                                    ModId = '{WrGlobal.LoginID}',
                                    ModIP = '{WrGlobal.ClientHostName}',
                                    ModDt = GETDATE()
                                WHERE CorpID = '{CorpID}' AND ProjectID = '{ProjectID}'
                            ";
                }
                else
                {
                    // Insert
                    query = $@"
                                INSERT INTO PJ_ProjectM (
                                    CorpID, ProjectID, ProjectType, CLASS, Type, Charge, PMName, 
                                    ProjectName, StartDate, EndDate, Remark, ItemCode, ProductImage, CreId, CreIP, CreDt
                                ) VALUES (
                                    '{CorpID}', '{ProjectID}', '{ProjectType}', '{CLASS}', '{Type}', 
                                    '{Charge}', '{PMName}', '{ProjectName}', 
                                    {(StartDate.HasValue ? $"'{StartDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    {(EndDate.HasValue ? $"'{EndDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    '{Remark}',  '{ItemCode}', @Pic,
                                    '{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}', GETDATE()
                                )
                            ";
                }

                return db.ExecuteQueryImage(query, ProductImage);
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
                        FROM PJ_ProjectM 
                        WHERE CorpID = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}'
                    ";
            DataRow dr = db.GetDataRow(query);
            return dr != null && Convert.ToInt32(dr["CNT"]) > 0;
        }


        public bool HasProjectDetail()
        {
            query = $@"
                        SELECT 1
                        FROM PJ_ProjectD
                        WHERE ProjectID = '{ProjectID}'
                    ";
            DataTable dt = db.GetDataTable(query);
            return dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        // 프로젝트 마스터 삭제
        public bool Del_ProjectMaster()
        {
            query = $@"
                        -- 관련 데이터 먼저 삭제
                        DELETE FROM PJ_ProjectMember 
                        WHERE CorpID = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}';
                        
                        DELETE FROM PJ_ProjectDocumentFile 
                        WHERE CorpId = '{WrGlobal.CorpID}' AND Gubun = 'Project' AND KeyCode1 = '{ProjectID}';

                        -- 프로젝트 삭제
                        DELETE FROM PJ_ProjectD
                        WHERE CorpID = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}';

                        DELETE FROM PJ_ProjectM 
                        WHERE CorpID = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}';
                      ";
            return db.ExecuteQuery(query);
        }


        // 프로젝트 마스터 연결 파일 조회
        public DataTable SelectProjectFiles()
        {
            query = $@"
                        SELECT 
                              CorpId,
                              Gubun,
                              KeyCode1,
                              KeyCode2,
                              KeyCode3,
                              FileSeq,
                              ItemCode,
                              Title,
                              FileName, 
                              OriFileName,
                              Capacity,
                              Path,
                              CreId,
                              CreIP,
                              CreDt
                        FROM PJ_ProjectDocumentFile
                        WHERE CorpId = '{WrGlobal.CorpID}' AND Gubun = 'Project' AND KeyCode1 = '{ProjectID}'
                        ORDER BY CreDt DESC, FileName
                      ";
            return db.GetDataTable(query);
        }

        //프로젝트 마스터 디자인 파일 삭제
        public bool Del_ProjectMasterDocument()
        {
            query = $@"
                        DELETE FROM PJ_ProjectDocumentFile 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                          AND Gubun = 'Project' 
                          AND KeyCode1 = '{ProjectID}'
                          AND FileSeq = {FileSeq}
                      ";
            return db.ExecuteQuery(query);
        }
    }
}