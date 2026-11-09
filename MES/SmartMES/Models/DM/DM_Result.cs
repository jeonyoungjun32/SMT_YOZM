using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.DM
{
    public class DM_Result
    {
        #region Property
        public int Seq { get; set; }
        public string ItemCode { get; set; }
        public string RegDate { get; set; }
		/// <summary>
		/// 공통코드 D11010
		/// </summary>
        public string RegType { get; set; }
        public int RegDiv { get; set; }
        public decimal Qty { get; set; }
        public decimal ProdQty { get; set; }
        public string ImpactCode1 { get; set; }
        public string ImpactCode2 { get; set; }
        public string ImpactCode3 { get; set; }
        public decimal ConfidenceRate { get; set; }
        public string ModelVersion { get; set; }
        public decimal? AdjustedQty { get; set; }
        public string Reason { get; set; }
		public string AdjustedId { get; set; }
        public string AdjustedDate { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

		public DataTable Select_Result(string searchDate, string itemName)
        {
			query = $@"
							SELECT DR.ItemCode,
								   CI.ItemName,
								   CASE RegType WHEN '10' THEN REPLACE(RegDate, '-', '년 ') + '월 ' + CONVERT(VARCHAR(1), RegDiv) + '주차'
												WHEN '20' THEN REPLACE(RegDate, '-', '년 ') + '월 '
												WHEN '30' THEN LEFT(RegDate, 4) + '년  ' + CONVERT(VARCHAR(1), RegDiv) + '분기'
												END AS RegDate,
								   DR.Qty,
								   DR.ProdQty,
								   ImpactCode1,
								   ImpactCode2,
								   ImpactCode3
							FROM DM_Result DR
							LEFT JOIN CM_Item CI ON DR.CorpId = CI.CorpId AND DR.ItemCode = CI.ItemCode
							WHERE DR.CorpId = '{WrGlobal.CorpID}'
							AND DR.RegDate = '{searchDate}'
							AND CI.ItemName LIKE '{itemName}%'
					  ";

			return db.GetDataTable(query);
        }

        public DataTable Select_Demand_Accuracy(string searchDate, string itemName)
        {
            query = $@"
                                    DECLARE @TargetMonth VARCHAR(7) = '{searchDate}';

									-- 1. 조회 대상 월의 1일과 마지막일
									DECLARE @MonthStart DATE = CAST(@TargetMonth + '-01' AS DATE);
									DECLARE @MonthEnd DATE = EOMONTH(@MonthStart);

									DECLARE @pWeekInfo TABLE (
										WeekNum INT,
										WeekStartDate VARCHAR(10),
										WeekEndDate VARCHAR(10)
									)

									-- 2. 앞/뒤 달 날짜를 포함하기 위해 첫째 주 월요일과 마지막 주 일요일로 범위 확장
									-- 첫째 주 월요일 (세션 설정 무관 계산)
									DECLARE @StartDate DATE = DATEADD(day, -((DATEPART(dw, @MonthStart) + @@DATEFIRST - 2) % 7), @MonthStart);
									-- 마지막 주 일요일 (세션 설정 무관 계산)
									DECLARE @EndDate DATE = DATEADD(day, 6 - ((DATEPART(dw, @MonthEnd) + @@DATEFIRST - 2) % 7), @MonthEnd);

									-- 3. 재귀 CTE로 확장된 전체 날짜 생성
									WITH DateList AS (
										SELECT @StartDate AS [Date]
										UNION ALL
										SELECT DATEADD(day, 1, [Date])
										FROM DateList
										WHERE [Date] < @EndDate
									),
									-- 4. 각 날짜별 기준 목요일 계산
									WeekBase AS (
										SELECT 
											[Date],
											DATENAME(dw, [Date]) AS [DayOfWeekName],
											-- 해당 날짜가 속한 주의 목요일 날짜
											DATEADD(day, 3 - (DATEPART(dw, [Date]) + @@DATEFIRST - 2) % 7, [Date]) AS [ThuDate]
										FROM DateList
									)
									-- 5. ISO 목요일 기준으로 월 및 주차 계산 출력
									INSERT INTO @pWeekInfo
									SELECT 
										DATEPART(iso_week, [ThuDate]) - DATEPART(iso_week, DATEFROMPARTS(YEAR([ThuDate]), MONTH([ThuDate]), 1)) + 1 AS [WeekNum],
										MIN([Date]) AS [WeekStartDate], -- 주차 시작일 (월)
										MAX([Date]) AS [WeekEndDate]    -- 주차 종료일 (일)
									FROM WeekBase
									GROUP BY 
										YEAR([ThuDate]),
										MONTH([ThuDate]),
										DATEPART(iso_week, [ThuDate]) - DATEPART(iso_week, DATEFROMPARTS(YEAR([ThuDate]), MONTH([ThuDate]), 1)) + 1
									ORDER BY 
										[WeekStartDate];


									------실 데이터

									WITH SdOrderData AS (
										SELECT BSOD.CorpId,
											   BSOD.ItemCode,
											   CONVERT(VARCHAR(10), BSOM.ObtainDate, 120) AS Date,
											   SUM(BSOD.Qty) AS Qty
										FROM BM_SdOrderD BSOD
										INNER JOIN BM_SdOrderM BSOM ON BSOD.CorpId = BSOM.CorpId AND BSOD.SdOrderNo = BSOM.SdOrderNo
										GROUP BY BSOD.CorpId, BSOM.ObtainDate, BSOD.ItemCode
									),

									SalesData AS (
										SELECT BSD.CorpId,
											   BSD.ItemCode,
											   CONVERT(VARCHAR(10), BSM.SalesDate, 120) AS Date,
											   SUM(BSD.Qty) AS Qty
										FROM BM_SalesD BSD
										INNER JOIN BM_SalesM BSM ON BSD.CorpId = BSM.CorpId AND BSD.SalesNo = BSM.SalesNo
										GROUP BY BSD.CorpId, BSM.SalesDate, BSD.ItemCode
									),

									ProdData AS (
										SELECT PP.CorpId,
											   PP.ItemCode,
											   CONVERT(VARCHAR(10), PP.WorkDate, 120) AS Date,
											   SUM(PP.GoodQty) AS Qty
										FROM PM_ProductPlan PP
										GROUP BY PP.CorpId, PP.WorkDate, PP.ItemCode
									),

									StockData AS (
										SELECT SD.CorpId,
										   SD.ItemCode,
										   CONVERT(VARCHAR(10), SD.InOutDate, 120) AS Date,
										   SUM(CASE WHEN ISNULL(SM.FromHouseCode, '') <> '' THEN SD.Qty * -1
													WHEN ISNULL(SM.ToHouseCode, '') <> '' THEN SD.Qty
													ELSE 0 END) AS Qty
										FROM ST_StockD SD
										INNER JOIN ST_StockM SM ON SD.CorpId = SM.CorpId AND SD.InOutNo = SM.InOutNo
										LEFT JOIN CM_Warehouse CW ON SM.CorpId = CW.CorpId AND (SM.FromHouseCode = CW.HouseID OR SM.ToHouseCode = CW.HouseID)
										AND CW.WareType = '20'
										GROUP BY SD.CorpId, SD.InOutDate, SD.ItemCode
									)

									SELECT DR.ItemCode,
										   CI.ItemName,
										   CASE RegType WHEN '10' THEN REPLACE(RegDate, '-', '년 ') + '월 ' + CONVERT(VARCHAR(1), RegDiv) + '주차'
														WHEN '20' THEN REPLACE(RegDate, '-', '년 ') + '월 '
														WHEN '30' THEN LEFT(RegDate, 4) + '년  ' + CONVERT(VARCHAR(1), RegDiv) + '분기'
														END AS RegDate,
										   DR.Qty,
										   ISNULL(SOD.Qty, 0) + ISNULL(SD.Qty, 0) + ISNULL(PD.Qty, 0) + ISNULL(SL.Qty, 0) AS BaseQty,
										   ROUND(DR.Qty - ISNULL(SOD.Qty, 0) + ISNULL(SD.Qty, 0) + ISNULL(PD.Qty, 0) + ISNULL(SL.Qty, 0) / IIF(DR.Qty = 0, 1, DR.Qty) * 100, 1) AS MapeRate,
										   CASE WHEN ABS(ROUND(DR.Qty - ISNULL(SOD.Qty, 0) + ISNULL(SD.Qty, 0) + ISNULL(PD.Qty, 0) + ISNULL(SL.Qty, 0) / IIF(DR.Qty = 0, 1, DR.Qty) * 100, 1)) < 10 THEN '우수'
												WHEN ABS(ROUND(DR.Qty - ISNULL(SOD.Qty, 0) + ISNULL(SD.Qty, 0) + ISNULL(PD.Qty, 0) + ISNULL(SL.Qty, 0) / IIF(DR.Qty = 0, 1, DR.Qty) * 100, 1)) < 15 THEN '양호'
												ELSE '주의' END AS EvalGrade
									FROM DM_Result DR
									INNER JOIN DM_Code DC ON DR.CorpId = DC.CorpId AND DR.ItemCode = DC.ItemCode
									LEFT JOIN CM_Item CI ON DR.CorpId = CI.CorpId AND DR.ItemCode = CI.ItemCode
									LEFT JOIN @pWeekInfo WI ON DR.RegType = '10' AND DR.RegDiv = WI.WeekNum
									OUTER APPLY (
													SELECT SOD.*
													FROM SdOrderData SOD
													WHERE SOD.CorpId = DR.CorpId
													AND SOD.ItemCode = DR.ItemCode
													AND (
														 (DR.RegDiv = '10' AND SOD.Date BETWEEN WI.WeekStartDate AND WI.WeekEndDate) OR
														 (DR.RegDiv = '20' AND LEFT(SOD.Date, 7) = DR.RegDate) OR
														 (DR.RegDiv = '30' AND DATEPART(qq, SOD.Date) = DATEPART(qq, DR.RegDate + '-01'))
														)
													AND DC.BaseType = '10'
												) SOD

									OUTER APPLY (
													SELECT SD.*
													FROM SalesData SD
													WHERE SD.CorpId = DR.CorpId
													AND SD.ItemCode = DR.ItemCode
													AND (
														 (DR.RegDiv = '10' AND SD.Date BETWEEN WI.WeekStartDate AND WI.WeekEndDate) OR
														 (DR.RegDiv = '20' AND LEFT(SD.Date, 7) = DR.RegDate) OR
														 (DR.RegDiv = '30' AND DATEPART(qq, SD.Date) = DATEPART(qq, DR.RegDate + '-01'))
														)
													AND DC.BaseType = '20'
												) SD

									OUTER APPLY (
													SELECT PD.*
													FROM ProdData PD
													WHERE PD.CorpId = DR.CorpId
													AND PD.ItemCode = DR.ItemCode
													AND (
														 (DR.RegDiv = '10' AND PD.Date BETWEEN WI.WeekStartDate AND WI.WeekEndDate) OR
														 (DR.RegDiv = '20' AND LEFT(PD.Date, 7) = DR.RegDate) OR
														 (DR.RegDiv = '30' AND DATEPART(qq, PD.Date) = DATEPART(qq, DR.RegDate + '-01'))
														)
													AND DC.BaseType = '30'
												) PD

									OUTER APPLY (
													SELECT SL.*
													FROM StockData SL
													WHERE SL.CorpId = DR.CorpId
													AND SL.ItemCode = DR.ItemCode
													AND (
														 (DR.RegDiv = '10' AND SL.Date BETWEEN WI.WeekStartDate AND WI.WeekEndDate) OR
														 (DR.RegDiv = '20' AND LEFT(SL.Date, 7) = DR.RegDate) OR
														 (DR.RegDiv = '30' AND DATEPART(qq, SL.Date) = DATEPART(qq, DR.RegDate + '-01'))
														)
													AND DC.BaseType = '40'
												) SL
									WHERE DR.CorpId = '1000'
									AND DR.RegDate = @TargetMonth
									AND CI.ItemName LIKE '{itemName}%'
                        ";

            return db.GetDataTable(query);
        } 

		public DataTable Select_AdjustedList(string searchDate, string itemName)
        {
			query = $@"
							SELECT DR.Seq,
								   DR.ItemCode,
								   CI.ItemName,
								   CASE RegType WHEN '10' THEN REPLACE(RegDate, '-', '년 ') + '월 ' + CONVERT(VARCHAR(1), RegDiv) + '주차'
												WHEN '20' THEN REPLACE(RegDate, '-', '년 ') + '월 '
												WHEN '30' THEN LEFT(RegDate, 4) + '년  ' + CONVERT(VARCHAR(1), RegDiv) + '분기'
												END AS RegDate,
								   Qty,
								   AdjustedQty,
								   Reason,
								   AdjustedId,
								   AdjustedDate
							FROM DM_Result DR
							LEFT JOIN CM_Item CI ON DR.CorpId = CI.CorpId AND DR.ItemCode = CI.ItemCode
							WHERE DR.CorpId = '{WrGlobal.CorpID}'
							AND DR.RegDate = '{searchDate}'
							AND CI.ItemName LIKE '{itemName}%'
					  ";

			return db.GetDataTable(query);
        }

		public bool Update_Adjust()
        {
			query = $@"
							UPDATE DM_Result
							SET AdjustedQty = {(AdjustedQty.HasValue ? AdjustedQty.Value.ToString() : "null")},
								Reason = {(!string.IsNullOrEmpty(Reason) ? $"'{Reason}'" : "null")},
								AdjustedId = {(!string.IsNullOrEmpty(AdjustedId) ? $"'{AdjustedId}'" : "null")},
								AdjustedDate = {(!string.IsNullOrEmpty(AdjustedDate) ? $"'{AdjustedDate}'" : "null")},
								ModId = '{WrGlobal.LoginID}',
								ModIP = HOST_NAME(),
								ModDt = GETDATE()
							WHERE CorpId = '{WrGlobal.CorpID}'
							AND Seq = {Seq}
					  ";

			return db.ExecuteQuery(query);
        }
    }
}
