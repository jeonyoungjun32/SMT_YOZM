using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.DataProcessing.InMemoryDataProcessor;

using SmartCommon.Common;

namespace SmartDesign.Model
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
        public string Brand { get; set; }
        public DateTime? ProductYear { get; set; }
        public string Season { get; set; }
        public string GroupName { get; set; }
        public string MaterialCode { get; set; }
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
                            pm.Remark,
                            pm.Brand,
                            ci1.Title AS BrandName,
                            pm.ProductYear,
                            pm.Season,
                            ci3.Title AS SeasonName,
                            pm.GroupName,
                            cl2.CLASSNAME AS GroupClassName,
                            pm.MaterialCode,
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
                                    SELECT 1 FROM ProjectMember pm2 
                                    WHERE pm2.CorpID = pm.CorpID 
                                    AND pm2.ProjectID = pm.ProjectID 
                                    AND pm2.EmpNo = '{WrGlobal.LoginID}' 
                                    AND pm2.WorkType = '10'
                                ) THEN '10'
                                ELSE '20'
                            END AS WorkType
                        FROM ProjectMaster pm
                        LEFT JOIN CLASS cl ON pm.CorpID = cl.CorpId AND pm.CLASS = cl.CLASS
                        LEFT JOIN CodeUser cu1 ON pm.CorpID = cu1.CorpId AND pm.Charge = cu1.EmployeeId
                        LEFT JOIN CodeUser cu2 ON pm.CorpID = cu2.CorpId AND pm.PMName = cu2.EmployeeId
                        LEFT JOIN CmCateItem ci1 ON pm.CorpID = ci1.CorpId AND ci1.Class = 'A051' AND pm.Brand = ci1.ItemCode
                        LEFT JOIN CmCateItem ci3 ON pm.CorpID = ci3.CorpId AND ci3.Class = 'A053' AND pm.Season = ci3.ItemCode
                        LEFT JOIN CLASS cl2 ON pm.CorpID = cl2.CorpId AND pm.GroupName = cl2.CLASS
						LEFT JOIN (
									SELECT 
										  CorpId, 
										  CLASS, 
										  SUM(ISNULL(WorkDays, 0)) AS TotalWorkDays
									FROM CodeProject 
									GROUP BY CorpId, CLASS
								) cp ON pm.CorpId = cp.CorpId AND pm.CLASS = cp.CLASS
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
                query += $" AND CONVERT(CHAR(4), pm.StartDate, 112) = '{SearchStartDate.Value:yyyy}' OR CONVERT(CHAR(4), pm.EndDate, 112) = '{SearchStartDate.Value:yyyy}'";
            }
            //if (SearchEndDate.HasValue)
            //{
            //    query += $" AND CONVERT(CHAR(4), pm.EndDate, 112) = '{SearchEndDate.Value:yyyy}'";
            //}

            // 권한별 필터링
            if (!string.IsNullOrEmpty(SearchLoginID) && SearchLoginID != "All")
            {
                query += $@" AND (pm.Charge = '{SearchLoginID}' 
                                OR pm.PMName = '{SearchLoginID}'
                                OR EXISTS (
                                    SELECT 1 FROM ProjectMember pm2 
                                    WHERE pm2.CorpID = pm.CorpID 
                                    AND pm2.ProjectID = pm.ProjectID 
                                    AND pm2.EmpNo = '{SearchLoginID}'
                                ))";
            }

            query += " ORDER BY pm.CreDt DESC";

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
                                UPDATE ProjectMaster SET 
                                    CLASS = '{CLASS}',
                                    Type = '{Type}',
                                    Charge = '{Charge}',
                                    PMName = '{PMName}',
                                    ProjectName = '{ProjectName}',
                                    StartDate = {(StartDate.HasValue ? $"'{StartDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    EndDate = {(EndDate.HasValue ? $"'{EndDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    Remark = '{Remark}',
                                    Brand = '{Brand}',
                                    ProductYear = {(ProductYear.HasValue ? $"'{ProductYear.Value:yyyy-MM-dd}'" : "NULL")},
                                    Season = '{Season}',
                                    GroupName = '{GroupName}',
                                    MaterialCode = '{MaterialCode}',
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
                                INSERT INTO ProjectMaster (
                                    CorpID, ProjectID, ProjectType, CLASS, Type, Charge, PMName, 
                                    ProjectName, StartDate, EndDate, Remark, Brand, ProductYear, 
                                    Season, GroupName, MaterialCode, ProductImage, CreId, CreIP, CreDt
                                ) VALUES (
                                    '{CorpID}', '{ProjectID}', '{ProjectType}', '{CLASS}', '{Type}', 
                                    '{Charge}', '{PMName}', '{ProjectName}', 
                                    {(StartDate.HasValue ? $"'{StartDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    {(EndDate.HasValue ? $"'{EndDate.Value:yyyy-MM-dd}'" : "NULL")},
                                    '{Remark}', '{Brand}', 
                                    {(ProductYear.HasValue ? $"'{ProductYear.Value:yyyy-MM-dd}'" : "NULL")},
                                    '{Season}', '{GroupName}', '{MaterialCode}', @Pic,
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
                        FROM ProjectMaster 
                        WHERE CorpID = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}'
                    ";
            DataRow dr = db.GetDataRow(query);
            return dr != null && Convert.ToInt32(dr["CNT"]) > 0;
        }


        public bool HasProjectDetail()
        {
            query = $@"
                        SELECT 1
                        FROM ProjectDetail
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
                        DELETE FROM ProjectMember 
                        WHERE CorpID = '{WrGlobal.CorpID}' AND ProjectID = '{ProjectID}';
                        
                        DELETE FROM CodeDocumentFile 
                        WHERE CorpId = '{WrGlobal.CorpID}' AND Gubun = 'Project' AND KeyCode1 = '{ProjectID}';

                        -- 프로젝트 마스터 삭제
                        DELETE FROM ProjectMaster 
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
                        FROM CodeDocumentFile 
                        WHERE CorpId = '{WrGlobal.CorpID}' AND Gubun = 'Project' AND KeyCode1 = '{ProjectID}'
                        ORDER BY CreDt DESC, FileName
                      ";
            return db.GetDataTable(query);
        }

        //프로젝트 마스터 디자인 파일 삭제
        public bool Del_ProjectMasterDocument()
        {
            query = $@"
                        DELETE FROM CodeDocumentFile 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                          AND Gubun = 'Project' 
                          AND KeyCode1 = '{ProjectID}'
                          AND FileSeq = {FileSeq}
                      ";
            return db.ExecuteQuery(query);
        }


        // 광주요 프로젝트 마스터- 디테일 조회
        public DataTable Select_ProjectDetail_GJY()
        {
            query = $@"
                        DECLARE @YM char(6) = '{PlanYYYYMM}'  
                        DECLARE @StartOfMonth date = CAST(@YM + '01' AS date)
                        DECLARE @EndOfMonth date = EOMONTH(@StartOfMonth)

                        SELECT 
                            MIN(PM.ProjectID) as ProjectID,  -- 대표 ProjectID
                            PM.ProjectName,
                            '' as MaterialCode,				 -- 여러 MaterialCode가 섞여있어서 빈값으로 둠
                            COUNT(DISTINCT PM.MaterialCode) as Qty, -- 서로 다른 품목 수
                            MIN(PM.Charge) as Charge,				
                            CASE 
                                WHEN COUNT(PD.EndDate) = COUNT(*) THEN '30'	  -- 모든 절차가 완료
                                WHEN MIN(PD.StartDate) <= GETDATE() THEN '20' -- 진행중
                                ELSE '10' -- 계획
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

                        FROM ProjectMaster PM
                        INNER JOIN ProjectDetail PD ON PM.CorpID = PD.CorpID AND PM.ProjectID = PD.ProjectID
                        WHERE PM.CorpID = '{WrGlobal.CorpID}'
                            AND (
                                (PM.StartDate >= @StartOfMonth AND PM.StartDate <= @EndOfMonth)
                                OR
                                (PM.EndDate >= @StartOfMonth AND PM.EndDate <= @EndOfMonth)
                                OR
                                (PM.StartDate <= @StartOfMonth AND PM.EndDate >= @EndOfMonth)
                            )
    
                        GROUP BY PM.ProjectName 
                        ORDER BY PM.ProjectName
                    ";

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
                            PM.MaterialCode,			
                            COUNT(DISTINCT PM.MaterialCode) as Qty, -- 서로 다른 품목 수
                            MIN(PM.Charge) as Charge,				
                            CASE 
                                WHEN COUNT(PD.EndDate) = COUNT(*) THEN '30'	  -- 모든 절차가 완료
                                WHEN MIN(PD.StartDate) <= GETDATE() THEN '20' -- 진행중
                                ELSE '10' -- 계획
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

                        FROM ProjectMaster PM
                        INNER JOIN ProjectDetail PD ON PM.CorpID = PD.CorpID AND PM.ProjectID = PD.ProjectID
                        WHERE PM.CorpID = '{WrGlobal.CorpID}'
                            --AND (
                            --    (PM.StartDate >= @StartOfMonth AND PM.StartDate <= @EndOfMonth)
                            --    OR
                            --    (PM.EndDate >= @StartOfMonth AND PM.EndDate <= @EndOfMonth)
                            --    OR
                            --    (PM.StartDate <= @StartOfMonth AND PM.EndDate >= @EndOfMonth)
                            --)
                            AND CONVERT(CHAR(4), PM.StartDate, 112) = @YM OR CONVERT(CHAR(4), PM.EndDate, 112) = @YM
    
                        GROUP BY PM.ProjectName , PM.MaterialCode
                        ORDER BY PM.ProjectName
                    ";

            return db.GetDataTable(query);
        }

        // 프로젝트 마스터-디테일조회
        public DataTable Select_ProjectDetail()
        {
            query = $@"
                        DECLARE @YM char(6) = '{PlanYYYYMM}'  
                        DECLARE @StartOfMonth date = CAST(@YM + '01' AS date)
                        DECLARE @EndOfMonth date = EOMONTH(@StartOfMonth)

                        SELECT 
                            PM.ProjectID,
                            PM.ProjectName,
                            PM.MaterialCode,
                            1 as Qty, -- 개별 행이므로 1
                            PM.Charge,
                            CASE 
                                WHEN PD.EndDate IS NOT NULL THEN '30'        -- 완료
                                WHEN PD.StartDate <= GETDATE() THEN '20'     -- 진행중
                                ELSE '10'                                    -- 계획
                            END as Status,
    
                            -- 납기준수율 (완료된 경우에만 계산)
                            CASE 
                                WHEN PD.EndDate IS NOT NULL THEN 
                                    CASE 
                                        WHEN PD.EndDate <= PD.PlanEndDate THEN 100
                                        ELSE 0
                                    END
                                ELSE NULL
                            END as DeliveryComplianceRate,
    
                            -- 진척율 계산
                            CASE 
                                WHEN PD.PlanEndDate IS NULL OR PD.PlanStartDate IS NULL THEN 0
                                WHEN PD.EndDate IS NOT NULL THEN 100  -- 완료된 경우
                                WHEN PD.StartDate IS NOT NULL THEN 
                                    ROUND(
                                        CASE 
                                            WHEN DATEDIFF(DAY, PD.PlanStartDate, PD.PlanEndDate) = 0 THEN 0
                                            ELSE CAST(DATEDIFF(DAY, PD.PlanStartDate, GETDATE()) AS FLOAT) / 
                                                    CAST(DATEDIFF(DAY, PD.PlanStartDate, PD.PlanEndDate) AS FLOAT) * 100
                                        END, 2)
                                ELSE 0
                            END as ProgressRate,
    
                            -- 절차수량 (개별 행이므로 GroupYn이 'Y'가 아니면 1, 맞으면 0)
                            CASE WHEN ISNULL(PD.GroupYn, 'N') = 'N' THEN 1 ELSE 0 END as ProcessCount,
    
                            -- 지연여부 (1 또는 0)
                            CASE 
                                WHEN PD.EndDate IS NOT NULL AND PD.EndDate > PD.PlanEndDate THEN 1
                                WHEN PD.EndDate IS NULL AND PD.StartDate IS NOT NULL AND GETDATE() > PD.PlanEndDate THEN 1
                                ELSE 0
                            END as DelayCount,
    
                            -- 완료여부 (1 또는 0)
                            CASE  
                                WHEN PD.EndDate IS NOT NULL AND PD.EndDate <= PD.PlanEndDate THEN 1
                                WHEN PD.StartDate IS NOT NULL AND PD.StartDate <= PD.PlanStartDate THEN 1
                                ELSE 0
                            END as CompleteCount,
    
	                        -- PM.StartDate as MasterStartDate,
                            PD.PlanStartDate,
                            PD.PlanEndDate,
                            PD.StartDate, 
                            PD.EndDate, 
                            PD.Title		 -- 절차명 
                            --PD.GroupYn
                        FROM ProjectMaster PM
                        INNER JOIN ProjectDetail PD ON PM.CorpID = PD.CorpID AND PM.ProjectID = PD.ProjectID
                        WHERE PM.CorpID = '{WrGlobal.CorpID}'
                            AND (
                                (PM.StartDate >= @StartOfMonth AND PM.StartDate <= @EndOfMonth)
                                OR
                                (PM.EndDate >= @StartOfMonth AND PM.EndDate <= @EndOfMonth)
                                OR
                                (PM.StartDate <= @StartOfMonth AND PM.EndDate >= @EndOfMonth)
                            )

                        ORDER BY PM.ProjectID
                    ";

            return db.GetDataTable(query);
        }
    }
}