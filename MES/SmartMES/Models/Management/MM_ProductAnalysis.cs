using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

using SmartMES.Common;
using DevExpress.XtraRichEdit.Model;
using System.Runtime.InteropServices.ComTypes;
using SmartCommon.Common;

namespace SmartMES.Model.Management
{
    class MM_ProductAnalysis
    {
        public string Line_Cd { get; set; } //라인코드
        public string ProcessCode { get; set; } //공정코드
        public string CustCode { get; set; } //거래처코드
        public string ItemCode { get; set; } //품목코드
        public string MachineCode { get; set; } //설비코드
        public string ItemClass1 { get; set; } //품목분류1
        public string ItemClass2 { get; set; } //품목분류2

        Database db = new Database();
        string query;

        //생산분석현황 조회
        public DataTable Product_View(string startDate, string endDate)
        {
            //쿼리 수정 필요 (품목 빠짐)
            query = $@"
                        DECLARE @StartDate DATE = '{startDate}';
                        DECLARE @EndDate DATE = '{endDate}';
                        
                        ;WITH RawData AS
                        (
                            SELECT 
                                  A.CorpId
                                , FORMAT(DAY(A.WorkDate), 'D2') AS WorkDay    -- '01' ~ '31' 문자열로 변환 (PIVOT용)
                                , op.LineCode
                                , A.ProcessCode
                                , A.ItemCode
                                , SUM(A.GoodQty) AS GoodQty
                            FROM PM_OrderResult A
							INNER JOIN PM_OrderProcess op ON A.CorpId = op.CorpId AND A.OrderNo = op.OrderNo AND A.ItemCode = op.ItemCode
                            WHERE A.CorpId = '{WrGlobal.CorpID}'
                              AND A.WorkDate BETWEEN @StartDate AND @EndDate
                            GROUP BY 
                                  A.CorpId
                                , FORMAT(DAY(A.WorkDate), 'D2')
                                , op.LineCode
                                , A.ProcessCode
                                , A.ItemCode
                        )
                        
                        -- 피벗된 테이블 생성
                        , PivotedData AS (
                            SELECT *
                            FROM RawData
                            PIVOT (
                                SUM(GoodQty)
                                FOR WorkDay IN ([01], [02], [03], [04], [05], [06], [07], [08], [09], [10],
                                                [11], [12], [13], [14], [15], [16], [17], [18], [19], [20],
                                                [21], [22], [23], [24], [25], [26], [27], [28], [29], [30], [31])
                            ) AS PVT
                        )
                        
                        -- 최종 결과 조인
                        SELECT 
                              PV.*
                            , CP.ProcessName
                            , CL.LineName
                            , CM.ItemName
                            , CM.ItemClass1
                            , CM.ItemClass2
                            , CM.ItemClass3
                            , ISNULL(
                                COALESCE([01], 0) + COALESCE([02], 0) + COALESCE([03], 0) + COALESCE([04], 0) +
                                COALESCE([05], 0) + COALESCE([06], 0) + COALESCE([07], 0) + COALESCE([08], 0) +
                                COALESCE([09], 0) + COALESCE([10], 0) + COALESCE([11], 0) + COALESCE([12], 0) +
                                COALESCE([13], 0) + COALESCE([14], 0) + COALESCE([15], 0) + COALESCE([16], 0) +
                                COALESCE([17], 0) + COALESCE([18], 0) + COALESCE([19], 0) + COALESCE([20], 0) +
                                COALESCE([21], 0) + COALESCE([22], 0) + COALESCE([23], 0) + COALESCE([24], 0) +
                                COALESCE([25], 0) + COALESCE([26], 0) + COALESCE([27], 0) + COALESCE([28], 0) +
                                COALESCE([29], 0) + COALESCE([30], 0) + COALESCE([31], 0), 0
                              ) AS Good_Qty_SUM
                        FROM PivotedData PV
                        LEFT JOIN CM_Process CP ON PV.ProcessCode = CP.ProcessCode AND PV.CorpId = CP.CorpId
                        LEFT JOIN CM_Line CL ON PV.LineCode = CL.LineCode AND PV.CorpId = CL.CorpId
                        LEFT JOIN CM_Item CM ON PV.ItemCode = cm.ItemCode AND PV.CorpId = CM.CorpId
                        WHERE 1=1
                                        ";

            if (ProcessCode.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND PV.ProcessCode = '{ProcessCode}'";
            }
            if (ItemClass1.ConvertString_Null_To_Empty() != "")
            {
                query += $"\n AND cm.ItemClass1 = '{ItemClass1}' ";
            }
            if (ItemClass2.ConvertString_Null_To_Empty() != "")
            {
                query += $"\n AND cm.ItemClass2 = '{ItemClass2}' ";
            }
            if (ItemCode != "" && ItemCode != "ALL")
            {
                query += $"\nAND PV.ItemCode = '{ItemCode}'";
            }

            query += "\n ORDER BY PV.ProcessCode ";

            return db.GetDataTable(query);
        }

        public DataTable Product_Machine_View(string FromDate, string ToDate)
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            Line_Cd = Line_Cd.ConvertStr_Empty_To_Null();
            MachineCode = MachineCode.ConvertStr_Empty_To_Null();
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();

            query = $@"

                        SET ANSI_WARNINGS OFF;
                        SET ARITHIGNORE ON;
                        SET ARITHABORT OFF;

                        SELECT  
                              RT.*

                            -- 달성률 = 생산 / 지시
                            , ROUND((CONVERT(float, RT.TotalWorkQty) / NULLIF(CONVERT(float, RT.TotalOrderQty), 0)) * 100, 2) AS 달성률

                            -- 불량률 = 불량 / 생산
                            , ROUND((CONVERT(float, RT.TotalNgQty) / NULLIF(CONVERT(float, RT.TotalWorkQty), 0)) * 100, 2) AS 불량률

                            -- 가동율 = (가동 - 정지) / 가동
                            , CASE WHEN ISNULL(RT.가동시간,0)=0 THEN 0 ELSE CAST(ROUND(((RT.가동시간-RT.정지시간)/CONVERT(float,RT.가동시간))*100,2) AS float) END AS 가동률

                        FROM
                        (
                            SELECT  
                                  OP.MachineCode
                                , CO.MachineName
                                , OP.ProcessCode

                                -- 지시수량
                                , SUM(OP.OrderQty) AS TotalOrderQty

                                -- 생산수량
                                , SUM(ISNULL(ODR.WorkQty, 0)) AS TotalWorkQty
                                , SUM(ISNULL(ODR.GoodQty, 0)) AS TotalGoodQty
                                , SUM(ISNULL(ODR.NgQty, 0))   AS TotalNgQty

                                 -- 가동시간
                                , SUM(ISNULL(ODR.RunTime,0)) AS 가동시간

                                -- 비가동
                                , ISNULL(ST.StopTime,0) AS 정지시간

                            FROM PM_OrderProcess OP

                            LEFT JOIN
                            (
                                SELECT CorpId, OrderNo, OrderSeq
                                     , SUM(GoodQty) AS GoodQty
                                     , SUM(NgQty)   AS NgQty
                                     , SUM(WorkQty) AS WorkQty
                                     , SUM(DATEDIFF(MINUTE, StartDate, EndDate)) AS RunTime
                                FROM PM_OrderResult
                                  WHERE CorpId = '1000'
                                    AND WorkDate BETWEEN '{FromDate}' AND '{ToDate}'
                                GROUP BY CorpId, OrderNo, OrderSeq
                            ) ODR 
                                ON OP.CorpId = ODR.CorpId 
                               AND OP.OrderNo = ODR.OrderNo AND OP.OrderSeq = ODR.OrderSeq

                            LEFT JOIN
                            (
                                SELECT CorpId, MachineCode
                                      , SUM(StopTime) AS StopTime
                                FROM EQ_Operation
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                  AND StartTime >= '{FromDate}'
                                  AND EndTime   <  '{ToDate}'
                                GROUP BY CorpId, MachineCode
                            ) ST 
                                ON OP.CorpId = ST.CorpId AND OP.MachineCode = ST.MachineCode
    
	                        LEFT JOIN EQ_Machine CO ON OP.CorpId = CO.CorpId AND OP.MachineCode = CO.MachineCode

                            WHERE OP.CorpId = '{WrGlobal.CorpID}'
                              AND OP.OrderDate BETWEEN '{FromDate}' AND '{ToDate}'
                       
                ";

            if (MachineCode != "NULL")
            {
                query += $"\nAND OP.MachineCode = {MachineCode}";
            }
            if (ProcessCode != "NULL")
            {
                query += $"\nAND OP.ProcessCode = {ProcessCode}";
            }

            query += $@"  GROUP BY OP.MachineCode, CO.MachineName, OP.ProcessCode, ST.StopTime
                        ) RT
                         ORDER BY RT.MachineCode ";

            return db.GetDataTable(query);
        }

        public DataTable Product_View_Chart(string yyyyMM)
        {
            string sum_Day = "";

            for (int i = 1; i <= 31; i++)
            {
                if (i == 31) sum_Day += "[" + i.ToString("D2") + "]";
                else sum_Day += "[" + i.ToString("D2") + "], ";
            }

            query = $@"

                SELECT
                
                	PV.*
                FROM 
                (
                
                	SELECT 
                		A.CorpID
                		, FORMAT(DAY(WorkDate), 'D2') AS DD
                		, SUM(A.GoodQty) AS GoodQty
                	FROM PM_OrderResult A
                	WHERE A.CorpID = '{WrGlobal.CorpID}'
                	AND FORMAT(A.WorkDate, 'yyyyMM') LIKE '{yyyyMM}%'
                	GROUP BY A.CorpID, DAY(WorkDate)
                
                ) AS RT
                PIVOT (SUM(GoodQty) FOR [DD] IN({sum_Day})) AS PV
                WHERE 1=1
                ";
            return db.GetDataTable(query);
        }

        public DataTable ProductEntireView_ByCmd(string startDate, string endDate)
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            CustCode = CustCode.ConvertStr_Empty_To_Null();

            query = $@"
                        SELECT wo.CustCode, ccm.CustName, wo.ItemCode, cm.ItemName, wo.OrderQty, wo.WorkQty, wo.GoodQty, wo.NgQty
                        , ISNULL((wo.GoodQty/wo.OrderQty)*100,0) AS ProgressRate
                        , ISNULL((wo.GoodQty/wo.OrderQty)*100,0) AS ProgressRatebar
                        FROM PM_ProductPlan wo
                        LEFT JOIN CM_Cust ccm ON wo.CorpId = ccm.CorpId AND wo.CustCode = ccm.CustCode
                        LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode

                        WHERE wo.CorpId = '{WrGlobal.CorpID}'
                        AND wo.WorkDate BETWEEN '{startDate}' AND '{endDate}'

                    ";
            if (ItemCode != "NULL")
            {
                query += $"  AND wo.ItemCode = {ItemCode}";
            }
            query += $" ORDER BY wo.WorkDate ";
            return db.GetDataTable(query);
        }

        public DataTable ProductEntireView_ByROrder(string startDate, string endDate)
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            CustCode = CustCode.ConvertStr_Empty_To_Null();

            query = $@"
                           SELECT
                            sm.ShipToCode CustCode
                          , bc.CustName
                          , sd.ItemCode
                          , bi.ItemName
                          , ObtainDate 
                          , ISNULL(sd.Qty, 0)     AS Order_Qty
                          , ISNULL(sr.OrderQty, 0)  AS ProcCmd_Qty
                          , ISNULL(sr.GoodQty + sr.NgQty, 0)     AS Result_Qty
                          , ISNULL(sr.GoodQty, 0)     AS Good_Qty
                          , ISNULL((sr.GoodQty + sr.NgQty/sd.Qty)*100,0) AS ProgressRate
                          , ISNULL((sr.GoodQty + sr.NgQty/sd.Qty)*100,0) AS ProgressRatebar

                        FROM BM_SdOrderD sd           
                        LEFT JOIN BM_SdOrderM sm on sd.CorpId = sm.CorpId and sd.SdOrderNo = sm.SdOrderNo
						left join 
						(
							select sr.CorpId, sr.SdOrderNo, sr.SdOrderSeq, MItemCode, sum(sr.OrderQty) OrderQty, SUM(odr.GoodQty) GoodQty, SUM(odr.NgQty) NgQty
							from PM_SdRoute sr
							LEFT JOIN PM_OrderProcess op on sr.CorpId = op.CorpId AND sr.OrderNo = op.OrderNo 
							LEFT JOIN PM_OrderResult odr on sr.CorpId = odr.CorpId AND op.OrderNo = odr.OrderNo and op.OrderSeq = odr.OrderSeq
							group by sr.CorpId, sr.SdOrderNo, sr.SdOrderSeq, MItemCode
						) sr on sd.CorpId = sr.CorpId AND sd.SdOrderNo = sr.SdOrderNo and sd.SdOrderSeq = sr.SdOrderSeq  and sd.ItemCode = sr.MItemCode

                        LEFT JOIN CM_Cust  bc ON  sd.CorpId = bc.CorpId AND sm.ShipToCode = bc.CustCode
                        LEFT JOIN CM_Item bi ON sd.CorpId = bi.CorpId AND sd.ItemCode = bi.ItemCode


                        WHERE sd.CorpId =  '{WrGlobal.CorpID}'
                        AND sm.ObtainDate between '{startDate}' and '{endDate}'
   
                    ";
            if (ItemCode != "NULL")
            {
                query += $"  AND sd.ItemCode = {ItemCode}";
            }
            if (CustCode != "NULL")
            {
                query += $"  AND sm.ShipToCode = {CustCode}";
            }
            query += $" ORDER BY ObtainDate, CustName";
            return db.GetDataTable(query);
        }

        public DataTable ProductEntireView_ItemWorkQty(string startDate, string endDate)
        {
            query = $@" 
                        SELECT wo.ItemCode, cm.ItemName
                                , ISNULL(SUM(wo.OrderQty), 0) OrderQty
                                , ISNULL(SUM(wo.WorkQty), 0) WorkQty
                                , ISNULL(SUM(wo.GoodQty), 0) GoodQty
                                , ISNULL(SUM(wo.NgQty), 0) NgQty
                                , ISNULL(SUM(sd.Qty), 0) AS ROrderQty
                        FROM PM_ProductPlan wo
                        LEFT JOIN BM_SdOrderD sd on wo.CorpId = sd.CorpId AND wo.SdOrderNo = sd.SdOrderNo and wo.SdOrderSeq = sd.SdOrderSeq
                        LEFT JOIN CM_Cust ccm ON wo.CorpId = ccm.CorpId AND wo.CustCode = ccm.CustCode
                        LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
                        WHERE wo.CorpId = '{WrGlobal.CorpID}'
                        AND wo.WorkDate BETWEEN '{startDate}' AND '{endDate}'
                    ";

            if (CustCode != "NULL")
            {
                query += $"  AND wo.CustCode = {CustCode}";
            }

            query += $" GROUP BY wo.ItemCode , cm.ItemName  ORDER BY  WorkQty DESC ";

            return db.GetDataTable(query);
        }

        public DataTable ProductEntireView_ItemTOP3_ByCust(string startDate, string endDate, string custCd)
        {
            query = $@"
                         SELECT TOP 3 
                                 wo.ItemCode
                                 , wo.CustCode
                                 , cm.ItemName
                                 , ISNULL(SUM(wo.OrderQty), 0) AS OrderQty
                                 , ISNULL(SUM(wo.WorkQty), 0) AS WorkQty
                                 , ISNULL(SUM(wo.GoodQty), 0) AS GoodQty
                                 , ISNULL(SUM(wo.NgQty), 0) AS NgQty
                                ,  ISNULL(SUM(sd.Qty), 0) AS ROrderQty
                        FROM PM_ProductPlan wo
                        LEFT JOIN BM_SdOrderD sd on wo.CorpId = sd.CorpId AND wo.SdOrderNo = sd.SdOrderNo and wo.SdOrderSeq = sd.SdOrderSeq
                        LEFT JOIN CM_Cust ccm ON wo.CorpId = ccm.CorpId AND wo.CustCode = ccm.CustCode
                        LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
                        WHERE wo.CorpId = '{WrGlobal.CorpID}'
                        AND wo.WorkDate BETWEEN '{startDate}' AND '{endDate}'
                        AND wo.CustCode = '{custCd}'
                        GROUP BY wo.ItemCode, wo.CustCode, cm.ItemName
                        ORDER BY WorkQty DESC
                     ";

            return db.GetDataTable(query);
        }

        public DataTable ProductEntireView_CustOrderQty(string startDate, string endDate)
        {

            query = $@" 
                        SELECT sm.SoldToCode AS CustCode
                        , ccm.CustName
                        , ISNULL(SUM(sd.Qty), 0) AS ROrderQty
						, ISNULL(SUM(pp.OrderQty), 0) AS OrderQty
                        , ISNULL(SUM(pp.WorkQty), 0) AS  WorkQty
                        , ISNULL(SUM(pp.GoodQty), 0) AS GoodQty
                        , ISNULL(SUM(pp.NgQty), 0) AS  NgQty
                        FROM BM_SdOrderD sd
                        LEFT JOIN BM_SdOrderM sm on sm.CorpId = sd.CorpId AND sm.SdOrderNo = sd.SdOrderNo
                        LEFT JOIN PM_ProductPlan pp on pp.CorpId = sd.CorpId AND pp.SdOrderNo = sd.SdOrderNo AND pp.SdOrderSeq = sd.SdOrderSeq
                        LEFT JOIN CM_Cust ccm ON sd.CorpId = ccm.CorpId AND sm.SoldToCode = ccm.CustCode
                        LEFT JOIN CM_Item cm ON sd.CorpId = cm.CorpId AND sd.ItemCode = cm.ItemCode
                        WHERE sd.CorpId ='{WrGlobal.CorpID}'
                        AND sm.ObtainDate BETWEEN'{startDate}' AND '{endDate}'
                   

                    ";
            if (CustCode != "" && CustCode != "NULL" && CustCode.ToUpper() != "ALL")
            {
                query += $"  AND sm.SoldToCode = {CustCode}";
            }
            query += $@"   GROUP BY sm.SoldToCode , ccm.CustName , sd.Qty
                     ORDER BY sd.Qty DESC  ";

            return db.GetDataTable(query);

        }

        public DataTable ProductEntireView_Total(string startDate, string endDate)
        {
            query = $@" 
                        SELECT FORMAT(WorkDate, 'yyyy-MM') as MONTH
                        , ISNULL(SUM(wo.OrderQty),0) OrderQty
                        , ISNULL(SUM(wo.WorkQty),0) WorkQty
                        , ISNULL(SUM(wo.GoodQty),0) GoodQty
                        , ISNULL(SUM(wo.NgQty),0) NgQty
                        , ISNULL(SUM(sd.Qty),0) AS ROrderQty
                        , SUM(ISNULL((wo.WorkQty/wo.OrderQty)*100,0)) AS ProgressRate
                        , SUM(ISNULL((wo.WorkQty/wo.OrderQty)*100,0)) AS ProgressRatebar
                        FROM PM_ProductPlan wo
                        LEFT JOIN BM_SdOrderD sd on wo.CorpId = sd.CorpId AND wo.SdOrderNo = sd.SdOrderNo and wo.SdOrderSeq = sd.SdOrderSeq
                        LEFT JOIN CM_Cust ccm ON wo.CorpId = ccm.CorpId AND wo.CustCode = ccm.CustCode
                        LEFT JOIN CM_Item cm ON wo.CorpId = cm.CorpId AND wo.ItemCode = cm.ItemCode
                        WHERE wo.CorpId = '{WrGlobal.CorpID}'
                        AND wo.WorkDate BETWEEN '{startDate}' AND '{endDate}'
                    ";

            if (CustCode != "" && CustCode != "NULL")
            {
                query += $"  AND wo.CustCode = {CustCode}";
            }
            query += "GROUP BY FORMAT(WorkDate, 'yyyy-MM')";
            return db.GetDataTable(query);
        }

        public DataTable ProductEntireView_ByROrder(string yyyyMM)
        {
            query = $@"
                        SELECT
                              som.SdOrderNo
                            , por.OrderNo
                            , CONVERT(date, por.WorkDate) AS WorkDate

                            , som.SoldToCode              AS CustCode
                            , bc.CustName

                            , sod.ItemCode
                            , bi.ItemName

                            , CONVERT(date, som.ObtainDate)   AS Order_Date
                            , CONVERT(date, sod.DeliveryDate) AS Delivery_Date

                            , ISNULL(sod.Qty, 0)         AS Order_Qty

                            , ISNULL(por.WorkQty, 0)     AS Result_Qty
                            , ISNULL(por.GoodQty, 0)     AS Good_Qty
                            , ISNULL(por.NgQty, 0)       AS Ng_Qty

                            , CASE
                                WHEN ISNULL(sod.Qty, 0) = 0 THEN 0
                                ELSE ROUND((ISNULL(por.WorkQty, 0) / sod.Qty) * 100, 2)
                              END AS ProgressRate

                            , CASE
                                WHEN ISNULL(sod.Qty, 0) = 0 THEN 0
                                ELSE ROUND((ISNULL(por.WorkQty, 0) / sod.Qty) * 100, 2)
                              END AS ProgressRateBar

                            , bp.ProcessName
                            , bl.LineName

                        FROM BM_SdOrderM som
                        LEFT JOIN BM_SdOrderD sod ON sod.CorpId   = som.CorpId AND sod.SdOrderNo = som.SdOrderNo
                        LEFT JOIN PM_ProductPlan pp ON pp.CorpId = sod.CorpId AND pp.SdOrderNo = sod.SdOrderNo AND pp.SdOrderSeq = sod.SdOrderSeq
                        LEFT JOIN PM_OrderResult por ON por.CorpId = pp.CorpId AND por.OrderNo = pp.OrderNo
                        LEFT JOIN CM_Cust bc ON bc.CorpId = som.CorpId AND bc.CustCode = som.SoldToCode
                        LEFT JOIN CM_Item bi ON bi.CorpId = sod.CorpId AND bi.ItemCode = sod.ItemCode
                        LEFT JOIN CM_Process bp ON bp.CorpId = por.CorpId AND bp.ProcessCode = por.ProcessCode
                        LEFT JOIN CM_Line bl ON bl.CorpId = por.CorpId AND bl.LineCode = por.LineCode

                        WHERE som.CorpId = '{WrGlobal.CorpID}'
                        AND (CONVERT(VARCHAR(6), som.ObtainDate, 112) = '{yyyyMM}' or  CONVERT(VARCHAR(6), som.DeliveryDate, 112)= '{yyyyMM}')
                    ";
            if (ItemCode != "" && ItemCode != "NULL")
            {
                query += $"  AND bi.ItemCode = '{ItemCode}'";
            }
            if (CustCode != "" && CustCode != "NULL")
            {
                query += $"  AND som.SoldToCode = '{CustCode}'";
            }
            query += $" ORDER BY Order_Date, CustName";
            return db.GetDataTable(query);

        }
    }
}
