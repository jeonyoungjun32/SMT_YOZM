using DevExpress.XtraCharts.Native;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.Model;
using System.Runtime.InteropServices.ComTypes;

namespace SmartMES.Models.PM
{
    public class PM_ProductPlan
    {
        #region Property

        public string CorpId { get; set; }

        public string OrderNo { get; set; }

        public string SdOrderNo { get; set; }

        public int SdOrderSeq { get; set; }

        public string CustCode { get; set; }

        public string ItemCode { get; set; }

        public string WorkDate { get; set; }

        public int WorkSeq { get; set; }

        public decimal OrderQty { get; set; }

        public decimal WorkQty { get; set; }

        public decimal GoodQty { get; set; }

        public decimal NgQty { get; set; }

        public string Status { get; set; }

        public string EndDate { get; set; }

        public string WorkManager { get; set; }

        public string DeliveryDate { get; set; }

        public string AutoOpYn { get; set; }

        public string LineCode { get; set; }

        public string ProcessCode { get; set; }

        public string MachineCode { get; set; }

        public string UrgentYn { get; set; }

        public string Remark { get; set; }

        // 조회조건용
        public string StartDt { get; set; }
        public string EndDt { get; set; }

        public string AutoOpYN { get; set; }
        public string OrderStatus { get; set; }
        public string ItemType { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable Select_PM_ProductPlan(string corpId, string startDate, string endDate)
        {
            ItemType = ItemType.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT 
                            '' AS RowStatus
                            , wo.CorpId
                            , wo.OrderNo
                            , wo.SdOrderNo
                            , wo.SdOrderSeq
                            , wo.CustCode

                            , wo.ItemCode
                            , cm.ItemName
                            , cm.Alias
                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.Size
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            , cm.UnitCode
                            , cm.ColorCode

                            , wo.WorkDate
                            , wo.WorkSeq
                            , wo.OrderQty
                            , wo.WorkQty
                            , wo.GoodQty
                            , wo.NgQty
                            , wo.Status
                            , wo.EndDate
                            , wo.LineCode
                            , wo.ProcessCode
                            , wo.MachineCode
                            , wo.Remark

                            , wo.Status AS OrderStatus
                            , case when wo.UrgentYn = 'Y' then '긴급'
	                            else '' end AS UrgentYn

                        FROM PM_ProductPlan wo
                        LEFT JOIN BM_SdOrderD sod ON wo.CorpId = sod.CorpId AND wo.SdOrderNo = sod.SdOrderNo AND wo.SdOrderSeq = sod.SdOrderSeq
                        LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
                        WHERE wo.CorpId = '{corpId}'
                        AND wo.WorkDate BETWEEN '{startDate}' AND '{endDate}'

                ";

            if (ItemType != "")
            {
                query += $"\n AND cm.ItemType = '{ItemType}'";
            }

            //프로세스 옵션 : 생산계획 우선순위 조정여부 사용, 미사용
            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] == "Y")
            {
                query += $"\nORDER BY CASE WHEN wo.UrgentYn = 'Y' THEN 0 ELSE 1 END, CASE WHEN wo.WorkSeq IS NULL THEN 99 ELSE wo.WorkSeq END ASC, wo.WorkDate, wo.OrderNo";
            }
            else
            {
                query += $"\nORDER BY CASE WHEN wo.UrgentYn = 'Y' THEN 0 ELSE 1 END, wo.WorkDate, wo.OrderNo";
            }

            return db.GetDataTable(query);
        }

        public bool Upd_PM_ProductPlan_GS()
        {
            query = $@"
                        -- PM_ProductPlan 수정 
                        UPDATE PM_ProductPlan
                        SET WorkDate = '{WorkDate}'--지시일자
	                        , OrderQty = '{OrderQty}' --작업지시수량	
	                        , EndDate = '{EndDate}' --완료요청일
	                        , AutoOpYN = '{AutoOpYN}'
	                        , Remark = '{Remark}'
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}';

                        -- PM_SdRoute 삭제, PM_OrderProcess 삭제
                        DELETE FROM PM_SdRoute
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}';

                        DELETE FROM PM_OrderProcess
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}';
                ";

            return db.ExecuteQuery(query);
        }

        public DataTable Select_PM_ProductPlan_BySdOrderNo()
        {
            query = $@"
                        SELECT 
                            '' AS RowStatus
                            , wo.CorpId
                            , wo.OrderNo
                            , wo.SdOrderNo
                            , wo.SdOrderSeq

                            , wo.ItemCode
                            , cm.ItemName
                            , cm.Alias
                            , cm.Size

							, wo.UrgentYn
                            , wo.WorkDate
                            , wo.WorkSeq

                            , wo.OrderQty
                            , wo.WorkQty
                            , wo.GoodQty
                            , wo.NgQty

                            , case when wo.Status = 'W' then '대기'
	                            when wo.Status = 'R' then '진행중'
	                            when wo.Status = 'C' then '완료'
	                            else '' end as Status
                            , wo.EndDate
                            , wo.AutoOpYN
                            , wo.Remark
                            , wo.CreId
                            , wo.CreIP
                            , wo.CreDt
                            , wo.ModId
                            , wo.ModIP
                            , wo.ModDt

                        FROM PM_ProductPlan wo
                        LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
                        WHERE wo.CorpId = '{CorpId}'
                        AND wo.SdOrderNo = '{SdOrderNo}'
                        AND wo.SdOrderSeq = '{SdOrderSeq}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_ProductPlan_Indiv(string startDate, string endDate, string itemDiv = "")
        {
            ItemCode = ItemCode.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT 
                            '' AS Status
                            , wo.CorpId
                            , wo.OrderNo
                            , wo.WorkSeq
                            , wo.SdOrderNo
                            , wo.SdOrderSeq

                            , wo.ItemCode
                            , wo.ItemCode ItemName
                            , cm.ItemName AS col_ItemName
                            , cm.Alias
                            , cm.Size
                            , cm.UnitCode
                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            , cm.ColorCode
    
                            , case when wo.UrgentYn = 'Y' then '긴급'
	                            else '' end as UrgentYn
                            , wo.WorkDate
                            , wo.WorkSeq

                            , wo.OrderQty
                            , wo.WorkQty
                            , wo.GoodQty
                            , wo.NgQty

                            , case when wo.Status = 'W' then '대기'
	                            when wo.Status = 'R' then '진행중'
	                            when wo.Status = 'C' then '완료'
	                            else '' end as Status
                            , wo.EndDate
                            , wo.AutoOpYN
                            , wo.Remark
                            , wo.CreId
                            , wo.CreIP
                            , wo.CreDt
                            , wo.ModId
                            , wo.ModIP
                            , wo.ModDt

                        FROM PM_ProductPlan wo
                        LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
                        WHERE wo.WorkDate BETWEEN '{startDate}' AND '{endDate} 23:59'
                    ";

			if (ItemCode != "" && ItemCode.ToUpper() != "ALL")
			{
				query += $"\nAND wo.ItemCode = '{ItemCode}'";
			}
            if (itemDiv != "")
            {
                query += $"\nAND cm.ItemType = '{itemDiv}'";
            }

            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] == "Y")
			{
				query += $"\nORDER BY CASE WHEN wo.UrgentYn = 'Y' THEN 0 ELSE 1 END, CASE WHEN wo.WorkSeq IS NULL THEN 99 ELSE wo.WorkSeq END ASC, wo.WorkDate, wo.OrderNo";
			}
			else
			{
                query += $"\nORDER BY CASE WHEN wo.UrgentYn = 'Y' THEN 0 ELSE 1 END, wo.WorkDate, wo.OrderNo";
			}

			return db.GetDataTable(query);
        }

        public DataTable Select_PM_ProductPlan_Indiv_Available(string startDate, string endDate, bool allYn)
        {
            ItemCode = ItemCode.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT 
                            '' AS Status
                            , wo.CorpId
                            , wo.OrderNo
                            , wo.WorkSeq
                            , wo.SdOrderNo
                            , wo.SdOrderSeq

                            , wo.ItemCode
                            , wo.ItemCode ItemName
                            , cm.ItemName AS col_ItemName
                            , cm.Alias
                            , cm.Size
                            , cm.UnitCode
                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            , cm.ColorCode
    
                            , case when wo.UrgentYn = 'Y' then '긴급'
	                            else '' end as UrgentYn
                            , wo.WorkDate
                            , wo.WorkSeq

                            , wo.OrderQty
                            , wo.WorkQty
                            , wo.GoodQty
                            , wo.NgQty
                            , wo.OrderQty - ISNULL(wo.WorkQty, 0) AS RemainQty

                            , case when wo.Status = 'W' then '대기'
	                            when wo.Status = 'R' then '진행중'
	                            when wo.Status = 'C' then '완료'
	                            else '' end as Status
                            , wo.EndDate
                            , wo.Revision
                            , wo.AutoOpYN
                            , wo.Remark
                            , wo.CreId
                            , wo.CreIP
                            , wo.CreDt
                            , wo.ModId
                            , wo.ModIP
                            , wo.ModDt

                        FROM PM_ProductPlan wo
                        LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
                        WHERE wo.WorkDate BETWEEN '{startDate}' AND '{endDate} 23:59'
                    ";


            query = $@"

DECLARE @CorpId nvarchar(30) = '{WrGlobal.CorpID}';
DECLARE @StartDate varchar(20) = '{startDate}';
DECLARE @EndDate varchar(20) = '{endDate} 23:59';

-- [내부 계산용 CTE 시작]
WITH CurrentStock AS (
    SELECT ItemCode AS ItemCode, SUM(StockQty) AS TotalStockQty
    FROM StockListView
    GROUP BY ItemCode
),
PlanDemand AS (
    SELECT A.OrderNo, A.WorkDate, A.WorkSeq, B.MItemCode AS ItemCode,
           SUM(CASE WHEN ISNULL(A.OrderQty, 0) = 0 THEN 0 
                    WHEN ISNULL(A.WorkQty, 0) >= A.OrderQty THEN 0 
                    ELSE B.WorkQty * (A.OrderQty - ISNULL(A.WorkQty, 0)) END) AS RequiredQty
    FROM PM_ProductPlan A
    INNER JOIN PM_SdRoute B ON A.CorpId = B.CorpId AND A.OrderNo = B.OrderNo
    WHERE A.CorpId = @CorpId
	AND A.WorkDate BETWEEN @StartDate AND @EndDate
	AND A.OrderQty > ISNULL(A.WorkQty, 0) 

    GROUP BY A.OrderNo, A.WorkDate, A.WorkSeq, B.MItemCode
),
CumulativeDemand AS (
    SELECT OrderNo, ItemCode, RequiredQty,
           SUM(RequiredQty) OVER (PARTITION BY ItemCode ORDER BY WorkSeq  ASC, WorkDate, OrderNo) AS AccRequiredQty
    FROM PlanDemand
),
OrderShortage AS (
    SELECT D.OrderNo,
           MAX(CASE WHEN cm.ItemType in ('40', '50', '60', '70')
                    AND (ISNULL(S.TotalStockQty, 0) - D.AccRequiredQty) < 0 THEN 1 ELSE 0 END) AS IsMatShort,
           MAX(CASE WHEN cm.ItemType = '20' AND (ISNULL(S.TotalStockQty, 0) - D.AccRequiredQty) < 0 THEN 1 ELSE 0 END) AS IsProdShort
    FROM CumulativeDemand D
    LEFT JOIN CurrentStock S ON D.ItemCode = S.ItemCode
    LEFT JOIN CM_Item cm ON D.ItemCode = cm.ItemCode
    GROUP BY D.OrderNo
)
-- [내부 계산용 CTE 끝]


-- [실제 상단 그리드 출력부]
SELECT 
    '' AS Status
    , wo.CorpId
    , wo.OrderNo
    , wo.WorkSeq
    , wo.SdOrderNo
    , wo.SdOrderSeq

    , wo.ItemCode
    , wo.ItemCode AS ItemName_Key
    , cm.ItemName AS col_ItemName
    , cm.Alias
    , cm.Size
    , cm.UnitCode
    , cm.ItemClass1
    , cm.ItemClass2
    , cm.ItemClass3

    , CASE WHEN wo.UrgentYn = 'Y' THEN '긴급' ELSE '' END AS UrgentYn
    , wo.WorkDate
    , wo.WorkSeq AS WorkSeq_Col -- 중복 컬럼명 방지

    , wo.OrderQty
    , wo.WorkQty
    , wo.GoodQty
    , wo.NgQty
    , (wo.OrderQty - ISNULL(wo.WorkQty, 0)) AS RemainQty

    -- [추가하신 3개 컬럼]
    , CASE WHEN OS.IsMatShort = 1 THEN 'Y' ELSE '' END AS 자재부족
    , CASE WHEN OS.IsProdShort = 1 THEN 'Y' ELSE '' END AS 제품부족
    , CASE 
        WHEN wo.Status = 'C' OR ISNULL(wo.OrderQty, 0) <= ISNULL(wo.WorkQty, 0) THEN '완료'
        WHEN OS.IsMatShort = 1 OR OS.IsProdShort = 1 THEN '불가' 
        ELSE '가능' 
      END AS 생산가능

    , CASE WHEN wo.Status = 'W' THEN '대기'
           WHEN wo.Status = 'R' THEN '진행중'
           WHEN wo.Status = 'C' THEN '완료'
           ELSE '' END AS Status_Name
           
    , wo.EndDate
    , wo.Revision
    , wo.AutoOpYN
    , wo.Remark
    , wo.CreId
    , wo.CreIP
    , wo.CreDt
    , wo.ModId
    , wo.ModIP
    , wo.ModDt

FROM PM_ProductPlan wo
LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
LEFT JOIN OrderShortage OS ON wo.OrderNo = OS.OrderNo -- 가용재고 상태 조인
WHERE wo.WorkDate BETWEEN @StartDate AND @EndDate

";

            if (ItemCode != "" && ItemCode.ToUpper() != "ALL")
            {
                query += $"\nAND wo.ItemCode = {ItemCode}";
            }

			if (!allYn)
			{
                query += $"\nAND wo.OrderQty - ISNULL(wo.WorkQty, 0) > 0";
			}

            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] == "Y")
            {
                query += $"\nORDER BY CASE WHEN wo.UrgentYn = 'Y' THEN 0 ELSE 1 END, CASE WHEN wo.WorkSeq IS NULL THEN 99 ELSE wo.WorkSeq END ASC, wo.WorkDate, wo.OrderNo";
            }
            else
            {
                query += $"\nORDER BY CASE WHEN wo.UrgentYn = 'Y' THEN 0 ELSE 1 END, wo.WorkDate, wo.OrderNo";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_PrepPM_ProductPlan_SdOrder(string startDt, string endDt)
        {
            //수주기반 작업지시 조회 화면에서 사용
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            query = $@"
                      SELECT '' AS Status
			                , wo.WorkDate
			                , wo.OrderNo

			                , wo.SdOrderNo
			                , wo.SdOrderSeq

                            , wo.CustCode
			                , wo.ItemCode
			                , wo.ItemCode AS ItemName
			                , cm.ItemName AS col_ItemName
			                , cm.Revision
			                , cm.Alias
			                , cm.Size
			                , cm.UnitCode
							, cm.ItemClass1
							, cm.ItemClass2
							, cm.ItemClass3
                            , cm.UnitWeight
							, cm.Texture

                            , wo.LineCode
                            , wo.ProcessCode
                            , wo.MachineCode

                            , case when wo.UrgentYn = 'Y' then '긴급'
	                            else '' end as UrgentYn

			                , wo.OrderQty
			                --, wo.WorkQty
			                , wo.Remark
			                , wo.Status

			                , ISNULL(wo.WorkQty, 0)               AS WorkQty
			                , wo.OrderQty - ISNULL(wo.WorkQty, 0) AS RemainQty
                            , CASE 
                                        WHEN ISNULL(wo.OrderQty, 0) = 0 THEN 0 
                                        ELSE CAST((ISNULL(wo.WorkQty, 0) / CAST(wo.OrderQty AS FLOAT)) * 100 AS DECIMAL(18, 1)) 
                                   END AS WorkRate

                            , om.SoldToCode

			                FROM PM_ProductPlan wo
			                INNER JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
                            LEFT JOIN BM_SdOrderM om ON wo.CorpId = om.CorpId AND wo.SdOrderNo = om.SdOrderNo
			                WHERE wo.CorpId = '{WrGlobal.CorpID}'
			                AND WOrkDate  BETWEEN '{startDt}' AND '{endDt}'  
						  --AND pd.PrepQty > ISNULL(pd.OrderQty, 0) --미완료만 (예비생산 > 지시량)
                    ";

            if (ItemCode != "NULL")
            {
                query += $" AND wo.ItemCode = {ItemCode}";
            }

            //프로세스 옵션 : 생산계획 우선순위 조정여부 사용, 미사용
            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] == "Y")
            {
                query += $"\nORDER BY CASE WHEN wo.UrgentYn = 'Y' THEN 0 ELSE 1 END, CASE WHEN wo.WorkSeq IS NULL THEN 99 ELSE wo.WorkSeq END ASC, wo.WorkDate, wo.OrderNo";
            }
            else
            {
                query += $"\nORDER BY CASE WHEN wo.UrgentYn = 'Y' THEN 0 ELSE 1 END, wo.WorkDate, wo.OrderNo";
            }


            return db.GetDataTable(query);
        }

        public DataTable Select_PrepPM_ProductPlan_SdOrder_RND(string startDt, string endDt)
        {
            //수주기반 작업지시 조회 화면에서 사용
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            query = $@"
                      SELECT '' AS Status
			                , wo.WorkDate
			                , wo.OrderNo

			                , wo.SdOrderNo
			                , wo.SdOrderSeq

                            --, wo.CustCode
			                , wo.ItemCode
			                , wo.ItemCode AS ItemName
			                , cm.ItemName AS col_ItemName
			                , cm.Revision
			                , cm.Alias
			                , cm.Size
			                , cm.UnitCode
							, cm.ItemClass1
							, cm.ItemClass2
							, cm.ItemClass3

                            , wo.LineCode
                            , wo.ProcessCode
                            , wo.MachineCode

			                , wo.OrderQty
			                , wo.WorkQty
			                , wo.Remark
			                , wo.Status

			                , ISNULL(wo.WorkQty, 0)               AS WorkQty
			                , wo.OrderQty - ISNULL(wo.WorkQty, 0) AS RemainQty

                            , sod.SalesRemark
                            , sod.Remark
                            , sod.ColorCode

                            , som.SoldToCode AS CustCode
			                FROM PM_ProductPlan wo
                            LEFT JOIN BM_SdOrderD sod ON wo.SdOrderNo = sod.SdOrderNo AND wo.SdOrderSeq = sod.SdOrderSeq
                            LEFT JOIN BM_SdOrderM som ON sod.SdOrderNo = som.SdOrderNo
			                LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
			                WHERE wo.CorpId = '{WrGlobal.CorpID}'
			                AND WOrkDate  BETWEEN '{startDt}' AND '{endDt}'  
						  --AND pd.PrepQty > ISNULL(pd.OrderQty, 0) --미완료만 (예비생산 > 지시량)
                    ";

            if (ItemCode != "NULL")
            {
                query += $" AND wo.ItemCode = {ItemCode}";
            }

            //프로세스 옵션 : 생산계획 우선순위 조정여부 사용, 미사용
            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] == "Y")
            {
                query += $"\nORDER BY CASE WHEN wo.WorkSeq IS NULL THEN 99 ELSE wo.WorkSeq END ASC, wo.WorkDate, wo.OrderNo";
            }
            else
            {
                query += $"\nORDER BY wo.OrderNo DESC";
            }

            return db.GetDataTable(query);
        }

        public DataTable SelectPM_ProductPlanShortageView(string orderNos)
        {
            query = $@"
                
                DECLARE 
                @CorpId nvarchar(30) = '{WrGlobal.CorpID}';
                
                
                -- 1. 계획에 필요한 총 자재 소요량 계산
                WITH MaterialDemand AS (
                    SELECT
                        B.MItemCode ItemCode
                        , SUM(B.PlanQty) AS TotalRequiredQty
                    FROM PM_ProductPlan AS A
                    INNER JOIN PM_SdRoute AS B ON A.CorpId = B.CorpId AND A.OrderNo = B.OrderNo
                    WHERE A.CorpId = @CorpId
                AND A.OrderNo In ({orderNos})
                    GROUP BY B.MItemCode
                ),
                -- 품목별 재고 (창고/Lot 무관하게 품목별 총합)
                CurrentStock AS (
                    SELECT
                          ItemCode ItemCode
                        , SUM(StockQty) AS TotalStockQty
                    FROM StockListView
                    WHERE 1=1
                    GROUP BY ItemCode, ItemName
                )
                
                
                SELECT
                	(SELECT [dbo].[FN_Get_CateItem_Title]('{WrGlobal.CorpID}', 'A010', cm.ItemType)) AS ItemTypeName
                    , D.ItemCode 
                    , cm.ItemName 
                    , cm.Alias
                    , cm.ItemClass1
                    , cm.ItemClass2
                    , cm.ItemClass3
                    , cm.Size
                    , cm.UnitWeight
                    , cm.Texture
                    , cm.Type
                    , cm.UnitCode
                    , cm.ColorCode
                
                
                    -- 현재고 (재고 뷰에서 가져옴)
                    , ISNULL(S.TotalStockQty, 0) AS CurStockQty
                  
                    -- 생산소요량(수요) -> (생산계획에서 계산)
                    , D.TotalRequiredQty AS TotalRequiredQty
                    
                    -- 예상 재고 (현재고  - 생산소요량)
                    , (ISNULL(S.TotalStockQty, 0) - D.TotalRequiredQty) AS ExpectStockQty
                    
                	--안전재고
                    , ISNULL(cm.SaftyQty, 0) AS SaftyQty 
                
                	-- 과부족 수량 (예상재고 - 안전재고)
                    , (ISNULL(S.TotalStockQty, 0) - D.TotalRequiredQty - ISNULL(cm.SaftyQty, 0)) AS DiffQty 
                    
                FROM MaterialDemand AS D
                LEFT JOIN CurrentStock AS S ON D.ItemCode = S.ItemCode
                LEFT JOIN CM_Item cm ON D.ItemCode = cm.ItemCode
                WHERE D.ItemCode IS NOT NULL    -- NULL 항목 미출력
                ORDER BY cm.ItemType, cm.ItemName
                ";

            return db.GetDataTable(query);
        }

        public DataTable SelectPM_ProductPlanShortageView_Available(string orderNos)
        {
            query = $@"
                
                DECLARE 
                @CorpId nvarchar(30) = '{WrGlobal.CorpID}';
                
            
-- 1. 품목별 현재고 (기준 재고)
WITH CurrentStock AS (
    SELECT
        ItemCode AS ItemCode
        , SUM(StockQty) AS TotalStockQty
    FROM StockListView
    GROUP BY ItemCode
),
-- 2. 계획(작업지시)별 자재 소요량
PlanDemand AS (
    SELECT
        A.OrderNo
        , A.UrgentYn
        , A.WorkDate
        , ISNULL(A.WorkSeq, 99) WorkSeq
        , B.MItemCode AS ItemCode
        , SUM(
              CASE 
                  WHEN ISNULL(A.OrderQty, 0) = 0 THEN 0 
                  WHEN ISNULL(A.WorkQty, 0) >= A.OrderQty THEN 0 
                  ELSE B.WorkQty * (A.OrderQty - ISNULL(A.WorkQty, 0)) -- B.WorkQty = BOM소요량
              END
            ) AS RequiredQty
    FROM PM_ProductPlan AS A
    INNER JOIN PM_SdRoute AS B ON A.CorpId = B.CorpId AND A.OrderNo = B.OrderNo
    WHERE A.CorpId = @CorpId
      AND A.OrderNo IN ({orderNos}) -- C#에서 파라미터로 동적 할당
      AND A.OrderQty > isnull(A.WorkQty, 0)
    GROUP BY A.OrderNo, B.MItemCode, A.UrgentYn, A.WorkDate, ISNULL(A.WorkSeq, 99) 
),
CumulativeDemand AS (
    SELECT 
        OrderNo
        , ItemCode
        , UrgentYn
        , WorkDate
        , WorkSeq
        , RequiredQty
        , SUM(RequiredQty) OVER (PARTITION BY ItemCode ORDER BY OrderNo) AS AccRequiredQty
        , ROW_NUMBER() OVER (PARTITION BY ItemCode ORDER BY OrderNo) AS SeqNo
    FROM PlanDemand
)
, MaterialShortage AS (
    SELECT
        D.OrderNo
        , CASE 
            WHEN MIN(ISNULL(S.TotalStockQty, 0) - ISNULL(D.AccRequiredQty, 0) - ISNULL(cm.SaftyQty, 0)) < 0
            THEN 1 ELSE 0 
          END AS IsShortage
    FROM CumulativeDemand D
    LEFT JOIN CurrentStock S ON D.ItemCode = S.ItemCode
    LEFT JOIN CM_Item cm ON D.ItemCode = cm.ItemCode
    GROUP BY D.OrderNo
)
-- 3. 계획 순서에 따른 누적 소요량 계산 (핵심 로직)
-- 동일한 자재에 대해 작업지시 순서대로 소요량을 누적(Running Total)합니다.


-- 4. 최종 가용재고 산출
SELECT
    (SELECT [dbo].[FN_Get_CateItem_Title](@CorpId, 'A010', cm.ItemType)) AS ItemTypeName
    , D.OrderNo
    , D.ItemCode 
    , cm.ItemName 
    , cm.Alias
    , cm.Size
    , cm.UnitCode
    , D.SeqNo 

    -- 초기 현재고
    , ISNULL(S.TotalStockQty, 0) AS CurStockQty

    -- 해당 계획(OrderNo)에서 단독으로 필요한 소요량
    , ISNULL(D.RequiredQty, 0) AS PlanRequiredQty
    
    -- 현재 계획까지 누적된 총 소요량
    , ISNULL(D.AccRequiredQty, 0) AS TotalRequiredQty
    
    -- 계획 진행 후 가용재고 (초기 현재고 - 누적 소요량)
    , (ISNULL(S.TotalStockQty, 0) - ISNULL(D.AccRequiredQty, 0)) AS AvailableStockQty
    
    -- 안전재고
    , ISNULL(cm.SaftyQty, 0) AS SaftyQty 

    -- 과부족 수량 (가용재고 - 안전재고)
    , (ISNULL(S.TotalStockQty, 0) - ISNULL(D.AccRequiredQty, 0) - ISNULL(cm.SaftyQty, 0)) AS DiffQty 
    
FROM CumulativeDemand AS D
LEFT JOIN CurrentStock AS S ON D.ItemCode = S.ItemCode
LEFT JOIN CM_Item cm ON D.ItemCode = cm.ItemCode
ORDER BY cm.ItemType, D.ItemCode, CASE WHEN D.UrgentYn = 'Y' THEN 0 ELSE 1 END, WorkSeq, WorkDate, OrderNo

                ";

            return db.GetDataTable(query);
        }



        public DataTable Select_PM_ProductPlan_Plan(string PlanFrom, int plusDay)
        {
            query = $@"
                        SELECT
	                        '' AS Status
	                        , OrderNo
	                        , CONVERT(DATE, WO.WorkDate) AS Work_Date 
	                        , CONVERT(DATE, WO.EndDate) AS End_Date
                            , WO.Status OrderStatus 
	                        , WO.OrderQty Day0_Qty ";

            for (int i = 1; i <= plusDay; i++)
            {
                query += $@"
                            ,(SELECT OrderQty FROM PM_ProductPlan
                            WHERE CorpId = '{WrGlobal.CorpID}' 
                            AND CONVERT(CHAR(8), WorkDate, 112) = CONVERT(CHAR(8), DATEADD(DAY, {i}, WO.WorkDate), 112)
                            AND ProcessCode = WO.ProcessCode
                            AND MachineCode = WO.MachineCode
                            AND ItemCode = WO.ItemCode) AS Day{i}_Qty
                        ";
            }

            query += $@"
	                        , CC.CustName
	                        , CP.ProcessCode
	                        , CP.ProcessName
	                        , CMM.MachineCode
	                        , CMM.MachineName

	                        , WO.ItemCode
	                        , WO.ItemCode ItemName
	                        , CM.Alias
	                        , CM.Size
	                        , CM.UnitWeight
	                        , WO.Remark
                        FROM PM_ProductPlan WO
                        LEFT JOIN CM_Item CM ON WO.CorpId = CM.CorpId AND WO.ItemCode = cm.ItemCode
                        LEFT JOIN CM_Cust CC ON WO.CorpId = CC.CorpId AND CM.CustCode = CC.CustCode
                        LEFT JOIN CM_Process CP ON WO.CorpId = CP.CorpId AND WO.ProcessCode = CP.ProcessCode
                        LEFT JOIN EQ_Machine CMM ON WO.CorpId = CM.CorpId AND WO.MachineCode = CMM.MachineCode
                        WHERE WO.CorpId = '{WrGlobal.CorpID}'
                        AND CONVERT(CHAR(8), WO.WorkDate, 112) = '{PlanFrom}'";

            if (ProcessCode != "ALL")
            {
                query += $"\nAND WO.ProcessCode = '{ProcessCode}'";
            }

            if (MachineCode != "ALL")
            {
                query += $"\nAND WO.MachineCode = '{MachineCode}'";
            }

            if (ItemCode != "ALL")
            {
                query += $"\nAND WO.ItemCode = '{ItemCode}'";
            }

            query += $@"
                        AND (WO.ProcessCode is not null OR WO.MachineCode is not null) --SMES 생산계획 화면에서 등록한게 아닌데이터가 나올까봐 넣음
                        ORDER BY WO.CorpId, WO.ProcessCode, WO.MachineCode, WO.ItemCode
                    ";

            return db.GetDataTable(query);
        }

        public int Select_PM_ProductPlan_NoSeq(string PlanFrom)
        {
            int Return = 0;

            try
            {
                query = $@"SELECT TOP 1 CONVERT(INT, RIGHT(OrderNo,4)) NoSeq
                           FROM PM_ProductPlan WITH(NOLOCK)
                          WHERE CorpId    = '{WrGlobal.CorpID}'
                            AND CONVERT(CHAR(8), WorkDate, 112) = '{PlanFrom}'
                            And ProcessCode = '{ProcessCode}'
                            And MachineCode = '{MachineCode}'
                            And ItemCode    = '{ItemCode}'
                            ORDER BY OrderNo DESC 
                          ";

                db.ExecuteQuery(query);

                DataView dv = db.GetDataView("제품리스트", query);

                if (dv.Count > 0)
                {
                    foreach (DataRowView item in dv)
                    {
                        Return = int.Parse(item["NoSeq"].ToString());
                    }

                }
                else
                {
                    Return = 0;
                }

            }
            catch
            {
                Return = 0;
            }

            return Return;
        }

        public DataTable Select_PM_ProductPlan_Use()
        {
            query = $@"
                        SELECT
                            OrderNo
                        FROM PM_ProductPlan
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CONVERT(CHAR(8), WorkDate, 112) = '{WorkDate.ConvertDate_To_Char8()}'
                        AND ProcessCode = '{ProcessCode}'
                        AND MachineCode = '{MachineCode}'
                        AND ItemCode = '{ItemCode}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_ProductPlan()
        {
            query = $@"
                        SELECT OrderNo
                        FROM PM_ProductPlan
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}' AND Status != 'W'  --진행상태 C:완료, R:진행중, W:대기
                    ";

            return db.GetDataTable(query);
        }

        public DataTable SelectPlanShortageView()
		{
            ItemCode = ItemCode.ConvertString_Null_To_Empty();

            //생산계획 대비 재고과부족현황
            query = $@"
                    SELECT 
	                      WO.OrderNo
	                    , WO.WorkDate
	                    , WO.SdOrderNo
	                    , WO.SdOrderSeq

	                    , WO.ItemCode
	                    , WO.ItemCode AS ItemName
	
	                    , WO.OrderQty

	                    , ISNULL(SD.Qty,0) AS SdOrderQty
	                    , ISNULL(wo.OrderQty,0) AS OrderQty
	                    , ISNULL(SV.StockQty ,0) AS StockQty
	                    , ISNULL(SV.StockQty,0) -  ISNULL(WO.OrderQty,0) AS Over_Shorts_Qty
	
 
                     FROM PM_ProductPlan WO
                     LEFT JOIN BM_SdOrderD SD ON WO.CorpId = SD.CorpId AND SD.SdOrderNo = WO.SdOrderNo AND SD.SdOrderSeq = WO.SdOrderSeq
                     LEFT JOIN StockListView SV ON SV.ItemCode = WO.ItemCode
                                                -- AND WareHouseCode = 'P00002'     -- 창고 고정 삭제
                     LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND cm.ItemCode = WO.ItemCode
                     LEFT JOIN CM_Warehouse cw ON SV.WareHouseCode = cw.HouseID


                     WHERE WO.CorpId = '{CorpId}' 
                     AND WorkDate BETWEEN '{StartDt}' AND '{EndDt}' 
                     AND cw.WareType = '20'     -- 제품창고만
                    ";

            if(ItemCode != "" && ItemCode.ToUpper() != "ALL")
			{
                query += $"\nAND wo.ItemCode = '{ItemCode}'";
			}

            //프로세스 옵션 : 생산계획 우선순위 조정여부 사용, 미사용
            if (WrGlobal.ProcOption["PD"]["PM_ProductPlan_Priority_YN"] == "Y")
            {
                query += $"\nORDER BY CASE WHEN wo.WorkSeq IS NULL THEN 99 ELSE wo.WorkSeq END ASC, wo.WorkDate, wo.OrderNo";
            }
            else
            {
                query += $"\nORDER BY wo.WorkDate, wo.OrderNo";
            }

            return db.GetDataTable(query);
		}


        public DataTable Select_WorkResultView()
        {
            switch (Status)
            {
                case "전체":
                    Status = "";
                    break;

                case "대기":
                    Status = "W";
                    break;

                case "진행중":
                    Status = "R";
                    break;

                case "완료":
                    Status = "C";
                    break;

                default:
                    break;
            }

            query = $@"SELECT WO.OrderNo
		                        , WO.WorkDate
		                        , WO.EndDate
		                        , WO.ItemCode
		                        , CM.ItemName
		                        , WO.OrderQty
		                        , WO.WorkQty
		                        , WO.GoodQty
		                        , WO.NgQty
		                        , CASE
                                    WHEN WO.Status = 'W' THEN '대기'
                                    WHEN WO.Status = 'R' THEN '진행중'
                                    WHEN WO.Status = 'C' THEN '완료'
                                    END AS Status
                        FROM PM_ProductPlan WO
                        LEFT JOIN CM_Item CM ON WO.ItemCode = cm.ItemCode
                        WHERE WO.CorpId = '{WrGlobal.CorpID}'
                        AND CONVERT(NVARCHAR(8), WO.WorkDate, 112) BETWEEN '{StartDt}' AND '{EndDt}'
                    ";

            if (Status != "") query += $"\n WO.Status = '{Status}'";

            query += $"\n ORDER BY WO.OrderNo DESC";

            return db.GetDataTable(query);
        }

        public DataTable Select_Monitoring_TodayList()
        {
            query = $@"
                SELECT OrderNo
		                , ISNULL(OrderQty, 0) AS OrderQty
		                , ISNULL(WorkQty, 0) AS  WorkQty
		                , ISNULL(GoodQty, 0) AS GoodQty
		                , ISNULL(NgQty, 0) AS NgQty
		                , ISNULL(OrderQty, 0) - ISNULL(WorkQty, 0) AS BalanceQty
                FROM PM_ProductPlan
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND CONVERT(nvarchar(10), WorkDate, 23) = '{WorkDate}'
                ORDER BY OrderNo DESC
                    ";

            return db.GetDataTable(query);
        }
        public bool Ins_PM_ProductPlan()
        {
            query = $@"INSERT INTO PM_ProductPlan
	                    (
		                    CorpId
		                    , OrderNo
		                    , SdOrderNo
		                    , SdOrderSeq
		                    , ItemCode
		                    , WorkDate
		                    , WorkSeq
		                    , OrderQty
		                    , WorkQty
		                    , GoodQty
		                    , NgQty
		                    , [Status]
		                    , EndDate
		                    , Revision
                            , AutoOpYn
                            , Remark
                            , WorkManager
                            , CustCode
                            , DeliveryDate
	                    )
                    SELECT SM.CorpId
		                    , '{OrderNo}'
		                    , SD.SdOrderNo
		                    , SD.SdOrderSeq
		                    , SD.ItemCode
		                    , CONVERT(nvarchar(10), GETDATE(), 23)
		                    , CONVERT(INT,RIGHT('{OrderNo}',4))
		                    , SD.Qty
		                    , 0
		                    , 0
		                    , 0
		                    , 'W'
		                    , CONVERT(nvarchar(10), GETDATE() + 7, 23)
		                    , CM.Revision
                            , '{AutoOpYN}'
                            , '{Remark}'
                            , '{WorkManager}'
                            , '{CustCode}'
                            , '{DeliveryDate}'
                    FROM BM_SdOrderM SM
                    INNER JOIN BM_SdOrderD SD on SD.CorpId = SM.CorpId and SD.SdOrderNo = SM.SdOrderNo
                    INNER JOIN CM_Item CM on CM.CorpId = SD.CorpId and cm.ItemCode = SD.ItemCode
                    WHERE SM.CorpId = '{WrGlobal.CorpID}'
                    AND SM.SdOrderNo = '{SdOrderNo}'
                    AND SD.SdOrderSeq = '{SdOrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }
        public bool Insert_PM_ProductPlan()
        {
            query = $@"
                        INSERT INTO PM_ProductPlan
                        (
                            CorpId
                            , OrderNo
                            , ItemCode
                            , WorkDate
                            , OrderQty
                            , WorkQty
                            , GoodQty
                            , NgQty
                            , Status
                            , EndDate
                            , ProcessCode
                            , MachineCode
                            , SdOrderNo
                            , SdOrderSeq
                            , Remark
                            , UrgentYn
                            , WorkSeq
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{OrderNo}'
                            , '{ItemCode}'
                            , '{WorkDate}'
                            , {OrderQty}
                            , 0
                            , 0
                            , 0
                            , 'W'
                            , '{EndDate}'
                            , '{ProcessCode}'
                            , '{MachineCode}'
                            , {SdOrderNo.ConvertStr_Empty_To_Null()}
                            , {SdOrderSeq.ConvertInt_Empty_To_Null()}
                            , '{Remark}'
                            , '{UrgentYn}'
                            , {WorkSeq.ConvertInt_Empty_To_Null()}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Insert_PM_ProductPlan_BySdOrder()
        {
            query = $@"
                        INSERT INTO PM_ProductPlan
                        (
                              CorpId
                            , OrderNo
                            , SdOrderNo
                            , SdOrderSeq
                            , CustCode
                            , ItemCode
                            , WorkDate
                            , OrderQty
                            , Status
                            , LineCode
                            , ProcessCode
                            , MachineCode
                            , Remark
                        )
                        VALUES
                        (
                              '{WrGlobal.CorpID}'
                            , '{OrderNo}'
                            , '{SdOrderNo}'
                            , '{SdOrderSeq}'
                            , '{CustCode}'
                            , '{ItemCode}'
                            , '{WorkDate}'
                            , '{OrderQty}'
                            , 'W' --진행상태 C:완료, R:진행중, W:대기
                            , '{LineCode}'
                            , '{ProcessCode}'
                            , '{MachineCode}'
                            , '{Remark}'
                        )
                    ";

            return db.ExecuteQuery(query);

        }

        public bool Insert_PM_SdRoute()
        {
            query = $@"
                        INSERT INTO PM_SdRoute
                            (
	                              CorpId
	                            , OrderNo
	                            , ChildID
	                            , ParentID
	                            , SdOrderNo
	                            , SdOrderSeq
	                            , PItemCode
	                            , RoutingSeq
	                            , WorkType
	                            , MItemCode
	                            , MItemName
	                            , StdPrice
	                            , OrderQty
	                            , WorkQty
	                            , PlanQty
	                            , Status
	                            , ProcessCode
	                            , ProcessName
	                            , ProcessType
	                            , CustCode
	                            , MItemTypeName
	                            , FinalYn
                                , CalcQty
                            )
                            SELECT 
							        BOM.CorpId
	                            , '{OrderNo}'
	                            , BOM.ChildID
	                            , BOM.ParentID
	                            , '{SdOrderNo}'
	                            , '{SdOrderSeq}'
	                            , BOM.PItemCode
	                            , BOM.RoutingSeq
	                            , BOM.WorkType
	                            , BOM.MaterialCode
	                            , BOM.MaterialName
	                            , 0                         -- StdPrice
	                            , {OrderQty}                -- OrderQty
	                            , BOM.Qty                   -- WorkQty
	                            ,  {OrderQty} * BOM.Qty     -- WorkQty

	                            , '' AS Status
	                            , BOM.ProcessCode
	                            , BOM.ProcessName
	                            , BOM.ProcessType
	                            , '' AS CustCode
	                            , BOM.ItemTypeName
								, case when bom.ProcessType = 'M' then null 
								when bom.NextRoutingSeq is null then 'Y' 
								else 'N' end as FinalYn --최종공정
                                , BOM.CalcQty
							FROM [FN_Bom] ('{WrGlobal.CorpID}','{ItemCode}') AS BOM

                    ";

            return db.ExecuteQuery(query);
        }

        public bool Insert_PM_SdRoute_YOZM()
        {
            query = $@"
                        ;WITH BOM_SOURCE AS
                        (
                            SELECT
                                  CB.*
                                , MI.ItemType AS MaterialType
                            FROM CM_BOM CB
                            INNER JOIN CM_Item PI
                                ON CB.CorpId = PI.CorpId
                                AND CB.PItemCode = PI.ItemCode
                                AND CB.Revision = PI.Revision
                            LEFT JOIN CM_Item MI
                                ON CB.CorpId = MI.CorpId
                                AND CB.MItemCode = MI.ItemCode
                            WHERE CB.CorpId = '{WrGlobal.CorpID}'
                            AND CB.PItemCode = '{ItemCode}'
                        )
                        , BOM AS
                        (
                            SELECT *
                            FROM BOM_SOURCE
                            WHERE TreeLevel = 0

                            UNION ALL

                            SELECT CHILD.*
                            FROM BOM_SOURCE CHILD
                            INNER JOIN BOM PARENT
                                ON CHILD.ParentID = PARENT.ChildID
                                AND CHILD.TreeLevel = PARENT.TreeLevel + 1
                            WHERE NOT
                            (
                                PARENT.ProcessType = 'M'
                                AND PARENT.MaterialType = '30'
                            )
                        )
                        INSERT INTO PM_SdRoute
                            (
	                              CorpId
	                            , OrderNo
	                            , ChildID
	                            , ParentID
	                            , SdOrderNo
	                            , SdOrderSeq
	                            , PItemCode
	                            , RoutingSeq
	                            , WorkType
	                            , MItemCode
	                            , MItemName
	                            , StdPrice
	                            , OrderQty
	                            , WorkQty
	                            , PlanQty
	                            , Status
	                            , ProcessCode
	                            , ProcessName
	                            , ProcessType
	                            , CustCode
	                            , MItemTypeName
	                            , FinalYn
                                , CalcQty
                            )
                            SELECT 
							        BOM.CorpId
	                            , '{OrderNo}'
	                            , BOM.ChildID
	                            , BOM.ParentID
	                            , '{SdOrderNo}'
	                            , '{SdOrderSeq}'
	                            , BOM.PItemCode
	                            --, BOM.RoutingSeq
	                            , CASE WHEN BOM.ProcessType = 'P' THEN
	                                ROW_NUMBER() OVER
	                                (
	                                    PARTITION BY BOM.ProcessType
	                                    ORDER BY BOM.ChildID DESC
	                                )
	                              ELSE NULL END AS RoutingSeq
	                            , BOM.WorkType
	                            , BOM.MItemCode
	                            , BOM.MItemName
	                            , 0                         -- StdPrice
	                            , {OrderQty}                -- OrderQty
	                            , BOM.Qty                   -- WorkQty
	                            ,  {OrderQty} * BOM.Qty     -- WorkQty

	                            , '' AS Status
	                            , BOM.ProcessCode
	                            , BOM.ProcessName
	                            , BOM.ProcessType
	                            , '' AS CustCode
	                            , BOM.ItemTypeName
								, CASE WHEN BOM.ProcessType = 'M' THEN NULL
                                  WHEN EXISTS
                                  (
                                      SELECT 1
                                      FROM BOM NEXT_BOM
                                      WHERE NEXT_BOM.ProcessType = 'P'
                                      AND NEXT_BOM.RoutingSeq > BOM.RoutingSeq
                                  ) THEN 'N'
                                  ELSE 'Y' END AS FinalYn --최종공정
                                , BOM.CalcQty
							FROM BOM
                            OPTION (MAXRECURSION 32767)
                    ";

            return db.ExecuteQuery(query);
        }


        public bool Update_PM_SdRoute()
        {
            query = $@"
                        UPDATE PM_SdRoute
                            SET OrderQty = {OrderQty}
                              , PlanQty = {OrderQty} * WorkQty
                        WHERE OrderNo = '{OrderNo}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Update_PM_ProductPlan()
        {
            query = $@"
                        UPDATE PM_ProductPlan
                        SET
                            ItemCode = '{ItemCode}'
                            , WorkDate = '{WorkDate}'
                            , OrderQty = {OrderQty}
                            , EndDate = '{EndDate}'
                            , ProcessCode = '{ProcessCode}'
                            , MachineCode = '{MachineCode}'
                            , Remark = '{Remark}'
                            , UrgentYn = '{UrgentYn}'
                            , WorkSeq = {WorkSeq.ConvertInt_Empty_To_Null()}
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()

                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                    ";

            return db.ExecuteQuery(query);
        }
        public bool Update_PM_ProductPlan_Simple()
        {
            query = $@"
                        UPDATE PM_ProductPlan
                        SET
                            ItemCode = '{ItemCode}'
                            , OrderQty = {OrderQty}
                            , LineCode = '{LineCode}'
                            , ProcessCode = '{ProcessCode}'
                            , MachineCode = '{MachineCode}'
                            , Remark = '{Remark}'
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                    ";

            return db.ExecuteQuery(query);
        }
        public bool UpdatePM_ProductPlanStatus()
		{
            query = $@"
                        UPDATE PM_ProductPlan
                        SET Status = '{Status}'
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Delete_PM_ProductPlan()
        {
            query = $@"
                        DELETE PM_SdRoute
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'

                        DELETE PM_ProductPlan
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Delete_WeekOrder(string fromDate, string toDate)
        {
            query = $@"
                        DELETE PM_ProductPlan
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CONVERT(CHAR(8), WorkDate, 112) BETWEEN '{fromDate}' AND '{toDate}'
                        AND ISNULL(ProcessCode, '') = '{ProcessCode}'
                        AND ISNULL(MachineCode, '') = '{MachineCode}'
                        AND ISNULL(ItemCode, '') = '{ItemCode}'
                    ";

            return db.ExecuteQuery(query);
        }



    }
}
