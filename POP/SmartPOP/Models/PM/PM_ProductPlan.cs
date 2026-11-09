using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.PM
{
    class PM_ProductPlan
    {
        #region Properties
        public string CorpId { get; set; }
        public string OrderNo { get; set; }
        public string SdOrderNo { get; set; }
        public int SdOrderSeq { get; set; }
        public string MaterialCode { get; set; }
        public string WorkDate { get; set; }
        public int WorkSeq { get; set; }
        public int OrderQty { get; set; }
        public int WorkQty { get; set; }
        public int GoodQty { get; set; }
        public int NgQty { get; set; }
        public string Status { get; set; }
        public string EndDate { get; set; }
        public string Revision { get; set; }
        public string AutoOpYN { get; set; }

        // 조회조건용
        public string StartDt { get; set; }
        public string EndDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public bool Upd_PM_ProductPlanStatus()
        {
            query = $@"UPDATE PM_ProductPlan
                        SET Status = '{Status}'
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        ";

            return db.ExecuteQuery(query);
        }

        public DataTable Select_PM_ProductPlan_ByDate()
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
                            , WO.MaterialCode
                            , CM.MaterialName
                            , WO.WorkDate
                            , WO.EndDate
                            , WO.OrderQty
                            , WO.AutoOpYN
                            , WO.SdOrderNo
                            , WO.SdOrderSeq
                            , CASE
                                WHEN WO.Status = 'W' THEN '대기'
                                WHEN WO.Status = 'R' THEN '진행중'
                                ELSE '완료'
                              END AS Status

                        FROM PM_ProductPlan WO
                        LEFT JOIN CM_Item CM ON WO.MaterialCode = CM.MaterialCode
                        WHERE WO.CorpID = '{WrGlobal.CorpID}'
                        AND WorkDate BETWEEN '{StartDt}' AND '{EndDt}'
                        ";

            if (Status != "") query += $"\n AND WO.Status = '{Status}'";

            query += $"\n ORDER BY WO.WorkDate DESC";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_ProductPlan_CrossSdOrder()
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
		                    , 
WO.ItemCode
		                    , CM.ItemName
		                    , CM.Size
		                    , WO.WorkDate
		                    , WO.OrderQty
		                    , SOM.SoldToCode
		                    , SOM.DeliveryDate
		                    , CASE
                                   WHEN WO.Status = 'W' THEN '대기'
                                   WHEN WO.Status = 'R' THEN '진행중'
                                   WHEN WO.Status = 'C' THEN '완료'
                              END AS Status
                        FROM PM_ProductPlan WO
                        LEFT JOIN BM_SdOrderD SOD ON WO.SdOrderNo = SOD.SdOrderNo AND WO.SdOrderSeq = SOD.SdOrderSeq
                        LEFT JOIN BM_SdOrderM SOM ON SOD.SdOrderNo = SOM.SdOrderNo
                        LEFT JOIN CM_Item CM ON WO.ItemCode = CM.ItemCode
                        WHERE WO.CorpID = '{WrGlobal.CorpID}'
                        AND WO.WorkDate BETWEEN '{StartDt}' AND '{EndDt}'
                        ";

            if (Status != "") query += $"\n AND WO.Status = '{Status}'";

            query += $"\n ORDER BY WO.OrderNo";

            return db.GetDataTable(query);
        }

        public DataTable Select_RptPM_ProductPlan_Top()
        {
            query = $@"SELECT DISTINCT
                            WO.OrderNo
                            , WO.WorkDate	-- 지시일자
		                    , SM.SoldToCode	-- 수주처코드
		                    , CC1.CustName	-- 수주처명 
		                    , WO.MaterialCode	-- 제품코드
		                    , CM1.MaterialName	-- 제품명
		                    , CM1.[Lenght] + '/' + CM1.Width + '/' + CM1.Height AS [Size]	-- 제품규격
		                    , SM.DeliveryDate	-- 납기일자
                            , CM1.FilmNo        -- 필름번호
		                    , CM1.PoNo			-- P/O NO
                            , CM1.LinkColor     -- 인쇄색상
		                    , SD.Qty			-- 수주수량
		                    , WO.OrderQty		-- 작업수량
                        FROM PM_ProductPlan WO
                        LEFT JOIN BM_SdOrderD SD ON WO.SdOrderNo = SD.SdOrderNo AND WO.SdOrderSeq = SD.SdOrderSeq
                        LEFT JOIN SdOrderMaster SM ON SD.SdOrderNo = SM.SdOrderNo
                        LEFT JOIN PM_OrderProcess OP ON WO.OrderNo = OP.OrderNo
                        LEFT JOIN CM_Item CM1 ON CM1.MaterialCode = WO.MaterialCode
                        LEFT JOIN CM_Cust CC1 ON SM.SoldToCode = CC1.CustCode
                        WHERE WO.CorpId = '{WrGlobal.CorpID}'
                        AND WO.OrderNo = '{OrderNo}'
                ";

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
		                        , WO.MaterialCode
		                        , CM.MaterialName
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
                        LEFT JOIN CM_Item CM ON WO.MaterialCode = CM.MaterialCode
                        WHERE WO.CorpId = '{WrGlobal.CorpID}'
                        AND WO.WorkDate BETWEEN '{StartDt}' AND '{EndDt}'
                    ";

            if (Status != "") query += $"\n WO.Status = '{Status}'";

            query += $"\n ORDER BY WO.OrderNo DESC";

            return db.GetDataTable(query);
        }

        public DataTable Select_ProcProgressView()
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

            string tblA = $@"SELECT WO.OrderNo
		                            , WO.WorkDate
		                            , WO.EndDate
		                            , WO.MaterialCode
		                            , CM.MaterialName
		                            , WO.OrderQty
		                            , CASE
                                        WHEN WO.Status = 'W' THEN '대기'
                                        WHEN WO.Status = 'R' THEN '진행중'
                                        WHEN WO.Status = 'C' THEN '완료'
                                        END AS Status 
		                            , SR.ProcessName + 
				                            CASE 
					                            WHEN OP.Status IS NULL THEN ' (''미지시'')'
					                            ELSE ' (' + OP.Status + ')'
				                            END  ProcList
		                            , SR.RoutingSeq
                            FROM PM_ProductPlan WO
                            LEFT JOIN SdRoute SR ON WO.OrderNo = SR.OrderNo AND SR.ProcessType = 'P'
                            LEFT JOIN PM_OrderProcess OP ON SR.OrderNo = OP.OrderNo AND SR.RoutingSeq = OP.RoutingSeq
                            LEFT JOIN CM_Item CM ON CM.MaterialCode = WO.MaterialCode
                            WHERE WO.CorpId = '{WrGlobal.CorpID}'
                            AND WO.WorkDate BETWEEN '{StartDt}' AND '{EndDt}'
                        ";

            if (Status != "") tblA += $"\n AND WO.Status = '{Status}'";

            string _tblA = $@"SELECT WO.OrderNo
		                            , WO.WorkDate
		                            , WO.EndDate
		                            , WO.MaterialCode
		                            , CM.MaterialName
		                            , WO.OrderQty
		                            , CASE
                                        WHEN WO.Status = ''W'' THEN ''대기''
                                        WHEN WO.Status = ''R'' THEN ''진행중''
                                        WHEN WO.Status = ''C'' THEN ''완료''
                                        END AS Status 
		                            , SR.ProcessName + 
				                            CASE 
					                            WHEN OP.Status IS NULL THEN '' (미지시)''
					                            ELSE '' ('' + OP.Status + '')''
				                            END  ProcList
		                            , SR.RoutingSeq
                            FROM PM_ProductPlan WO
                            LEFT JOIN SdRoute SR ON WO.OrderNo = SR.OrderNo AND SR.ProcessType = ''P''
                            LEFT JOIN PM_OrderProcess OP ON SR.OrderNo = OP.OrderNo AND SR.RoutingSeq = OP.RoutingSeq
                            LEFT JOIN CM_Item CM ON CM.MaterialCode = WO.MaterialCode
                            WHERE WO.CorpId = ''{WrGlobal.CorpID}''
                            AND WO.WorkDate BETWEEN ''{StartDt}'' AND ''{EndDt}''
                        ";

            if (Status != "") _tblA += $"\n AND WO.Status = ''{Status}''";

            query = $@"
                DECLARE @cols NVARCHAR(MAX);
                DECLARE @query NVARCHAR(MAX);

                -- 1. 동적으로 컬럼 목록 (Proc1, Proc2, ...) 생성
                SELECT @cols = STRING_AGG('MAX(CASE WHEN ProcNum = ' + CAST(ProcNum AS NVARCHAR) + 
                    ' THEN ProcList END) AS Proc' + CAST(ProcNum AS NVARCHAR), ', ')
                FROM (
                    SELECT DISTINCT ROW_NUMBER() OVER (PARTITION BY OrderNo ORDER BY RoutingSeq) AS ProcNum
                    FROM ( " + tblA + $@" ) AS A
                ) AS ProcNumbers;

                -- 2. 동적 SQL 생성
                SET @query = '
                WITH RankedData AS (
                    SELECT 
                        OrderNo, 
                        WorkDate, 
                        EndDate, 
                        MaterialCode, 
                        MaterialName, 
                        OrderQty, 
                        Status, 
                        ProcList, 
                        ROW_NUMBER() OVER (PARTITION BY OrderNo ORDER BY RoutingSeq) AS ProcNum
                    FROM ( " + _tblA + $@" ) AS A
                )
                SELECT 
                    OrderNo, 
                    WorkDate, 
                    EndDate, 
                    MaterialCode, 
                    MaterialName, 
                    OrderQty, 
                    Status, ' + @cols + '
                FROM RankedData
                GROUP BY 
                    OrderNo, 
                    WorkDate, 
                    EndDate, 
                    MaterialCode, 
                    MaterialName, 
                    OrderQty, 
                    Status
                ORDER BY OrderNo, WorkDate;';

                -- 3. SQL 실행
                EXEC sp_executesql @query;

            ";
            return db.GetDataTable(query);
        }

        public bool Upd_PM_ProductPlanQty_POP()
        {
            query = $@"
                    DECLARE @MAX_SEQ INT
                    SELECT @MAX_SEQ = ChildID
                    FROM PM_SdRoute
                    WHERE CorpId =  '{CorpId}'
                    AND OrderNo = '{OrderNo}'
                    AND RoutingSeq = (	SELECT MAX(RoutingSeq) 
                    					FROM PM_SdRoute 
                    					WHERE  CorpId = '{CorpId}' 
                    					AND OrderNo = '{OrderNo}' 
                    					AND WorkType = '10') --사내)
                    
                    UPDATE PM_ProductPlan
                    SET  WorkQty = isnull(B.GoodQty,0) + isnull(B.NgQty,0)
                    		, GoodQty = isnull(B.GoodQty, 0)
                    		, NgQty = isnull(B.NgQty,0)
                    FROM PM_ProductPlan A, 
                    (
                    	SELECT CorpId, OrderNo, OrderSeq, SUM(GoodQty) AS GoodQty, SUM(NgQty) AS NgQty
                    	FROM PM_OrderResult
                    	WHERE OrderSeq = @MAX_SEQ
                    	GROUP BY CorpId, OrderNo, OrderSeq
                    ) B
                    WHERE A.CorpId = '{CorpId}'
                    AND A.OrderNo = '{OrderNo}'
                    AND A.CorpId = B.CorpId
                    AND A.OrderNo = B.OrderNo
            ";

            return db.ExecuteQuery(query);
        }
    }
}
