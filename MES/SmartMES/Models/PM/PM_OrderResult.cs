using DevExpress.XtraRichEdit.Model;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using static SmartMES.View.Analysis.Frm_AN_Work_LineView;

namespace SmartMES.Models.PM
{
	public class PM_OrderResult
    {
        #region Property

        public string CorpId { get; set; }

        public string OrderNo { get; set; }

        public int OrderSeq { get; set; }

        public int SubSeq { get; set; }

        public string ItemCode { get; set; }

        public string LineCode { get; set; }
        public string ProcessCode { get; set; }

        public string MachineCode { get; set; }

        public string WorkerID { get; set; }

        public string LotNo { get; set; }

        public string SerialNo { get; set; }

        public string WorkDate { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public decimal WorkQty { get; set; }

        public decimal GoodQty { get; set; }

        public decimal NgQty { get; set; }

        public string Result { get; set; }

        public string Remark { get; set; }

        public string MoldCode { get; set; }

        public int Shot { get; set; }


        // 조회조건용
        public string StartDt { get; set; }
        public string EndDt { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable Select_PM_OrderResult()
        {
            OrderNo = OrderNo.ConvertStr_Empty_To_Null();
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();
            MachineCode = MachineCode.ConvertStr_Empty_To_Null();
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();

            query = $@"SELECT  '' AS Status
                                , odr.SubSeq
                                , odr.SerialNo
								, odr.OrderNo
								, odr.OrderSeq
								, odr.WorkDate
								, odr.ItemCode
                                , cm.ItemName
                                , cm.HouseID AS OutHouseID
                                , op.LineCode
								, odr.ProcessCode
								, odr.ProcessCode AS ProcessName
								, odr.WorkerID
                                , odr.WorkDate
                                , odr.GoodQty
                                , odr.NgQty
                                , odr.StartDate
                                , odr.EndDate
                                , IIF( (SELECT COUNT(ExamNo)
                                        FROM QM_ExamResult
                                        WHERE CorpId = odr.CorpId
                                        AND ExamDivCd = '30'
                                        AND RefNo = odr.SerialNo
                                       ) > 0 , 'Y', 'N') AS ChkProc --공정검사 유무
                                , odr.MachineCode
                                , op.OrderQty
                                , odr.WorkQty
                                , odr.LotNo
                        FROM PM_OrderResult odr
                        LEFT JOIN PM_OrderProcess op ON odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq
                        LEFT JOIN CM_Item cm ON odr.ItemCode = cm.ItemCode
                        WHERE odr.CorpID = '{WrGlobal.CorpID}'
                        AND odr.WorkDate BETWEEN '{StartDt} 00:00:00' AND '{EndDt} 23:59:59'";

            if (OrderNo != "NULL")
            {
                query += $" AND odr.OrderNo = {OrderNo}";
            }
            if (OrderSeq != 0)
            {
                query += $" AND OP.OrderSeq = {OrderSeq}";
            }
            if (ProcessCode != "NULL")
            {
                query += $" AND odr.ProcessCode = {ProcessCode}";
            }
            if (MachineCode != "NULL")
            {
                query += $" AND OP.MachineCode = {MachineCode}";
            }
            if (ItemCode != "NULL")
            {
                query += $@"AND odr.ItemCode = {ItemCode}";
            }

            query += " ORDER BY odr.SerialNo ";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResultEditView()
        {
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();

            query = $@"WITH ResultAgg AS
                       (
                           SELECT
                                 CONVERT(date, odr.WorkDate) AS WorkDate
                               , odr.ProcessCode
                               , SUM(ISNULL(odr.WorkQty, 0)) AS WorkQty
                               , SUM(ISNULL(odr.GoodQty, 0)) AS GoodQty
                               , SUM(ISNULL(odr.NgQty, 0)) AS NgQty
                               , CASE
                                 WHEN COUNT(odr.ModDt) > 0 THEN 'Y'
                                 ELSE 'N'
                               END AS ModifyYn
                           FROM PM_OrderResult odr
                           WHERE odr.CorpId = '{WrGlobal.CorpID}'
                             AND odr.WorkDate BETWEEN '{StartDt} 00:00:00' AND '{EndDt} 23:59:59'";
            if (!string.IsNullOrEmpty(ProcessCode) && ProcessCode != "NULL")
            {
                query += $@" AND odr.ProcessCode = {ProcessCode}";
            }
            query += $@"GROUP BY CONVERT(date, odr.WorkDate), odr.ProcessCode),
                       OrderAgg AS
                       (
                           SELECT
                                 CONVERT(date, odr.WorkDate) AS WorkDate
                               , odr.ProcessCode
                               , SUM(op.OrderQty) AS OrderQty
                           FROM
                           (
                               SELECT DISTINCT
                                     CONVERT(date, WorkDate) AS WorkDate
                                   , ProcessCode
                                   , OrderNo
                                   , OrderSeq
                               FROM PM_OrderResult
                               WHERE CorpId = '1000'
                                 AND WorkDate BETWEEN '{StartDt} 00:00:00' AND '{EndDt} 23:59:59'";
            if (!string.IsNullOrEmpty(ProcessCode) && ProcessCode != "NULL")
            {
                query += $@" AND ProcessCode = {ProcessCode}";
            }
            query += $@"
                           ) odr
                           INNER JOIN PM_OrderProcess op  ON op.CorpId = '{WrGlobal.CorpID}' AND op.OrderNo = odr.OrderNo AND op.OrderSeq = odr.OrderSeq
                           GROUP BY odr.WorkDate, odr.ProcessCode
                       )
                    SELECT
                            ra.WorkDate
                          , ra.ProcessCode
                          , ISNULL(oa.OrderQty, 0) AS OrderQty
                          , ra.WorkQty
                          , ra.GoodQty
                          , ra.NgQty
                          , CASE WHEN ra.ModifyYn = 'Y' THEN '수정됨'
                                 ELSE '-'
                            END AS Modify
                          -- 달성률 = 작업수량 / 지시수량
                          , CASE
                              WHEN ISNULL(oa.OrderQty, 0) = 0 THEN 0
                              ELSE CAST(ra.WorkQty * 100.0 / oa.OrderQty AS DECIMAL(10, 2))
                            END AS AchievementRate
                      
                          -- 양품률 = 양품수량 / 작업수량
                          , CASE
                              WHEN ISNULL(ra.WorkQty, 0) = 0 THEN 0
                              ELSE CAST(ra.GoodQty * 100.0 / ra.WorkQty AS DECIMAL(10, 2))
                            END AS GoodRate
                      
                      FROM ResultAgg ra
                      LEFT JOIN OrderAgg oa ON oa.WorkDate = ra.WorkDate AND oa.ProcessCode = ra.ProcessCode 
                      ORDER BY ra.WorkDate, ra.ProcessCode";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResult_Indiv()
        {
            OrderNo = OrderNo.ConvertStr_Empty_To_Null();

            query = $@"SELECT  '' AS Status
                                , odr.SubSeq
                                , odr.SerialNo
								, odr.OrderNo
								, odr.OrderSeq

								, odr.WorkDate
                                , odr.StartDate
                                , odr.EndDate

								, odr.ItemCode
                                , cm.ItemName
                                , op.LineCode
								, odr.ProcessCode
								, odr.ProcessCode AS ProcessName
								, odr.WorkerID
                                , odr.WorkDate

                                , odr.WorkQty
                                , odr.GoodQty
                                ,  ISNULL(odr.NgQty, 0) NgQty

                                , IIF( (SELECT COUNT(ExamNo)
                                        FROM QM_ExamResult
                                        WHERE CorpId = odr.CorpId
                                        AND ExamDivCd = '30'
                                        AND RefNo = odr.SerialNo
                                       ) > 0 , 'Y', 'N') AS ChkProc --공정검사 유무
                                , odr.MachineCode
                                , op.OrderQty
                                , CASE
										WHEN odr.WorkQty IS NULL OR odr.WorkQty = 0
											THEN ISNULL(odr.GoodQty, 0) + ISNULL(odr.NgQty, 0)
										ELSE odr.WorkQty
									END AS WorkQty

                        FROM PM_OrderResult odr
                        LEFT JOIN PM_OrderProcess op ON odr.CorpId = op.CorpId AND odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq
                        LEFT JOIN CM_Item cm ON odr.CorpId = cm.CorpId AND odr.ItemCode = cm.ItemCode
                        WHERE odr.CorpID = '{WrGlobal.CorpID}'";

            if (OrderNo != "NULL")
            {
                query += $" AND odr.OrderNo = {OrderNo}";
            }
            if (OrderSeq != 0)
            {
                query += $" AND OP.OrderSeq = {OrderSeq}";
            }

            query += " ORDER BY odr.OrderNo, odr.SerialNo DESC";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResult_Indiv(string startDate, string endDate)
        {
            OrderNo = OrderNo.ConvertStr_Empty_To_Null();
            SerialNo = SerialNo.ConvertString_Null_To_Empty();
            ProcessCode = ProcessCode.ConvertString_Null_To_Empty();
            ItemCode = ItemCode.ConvertString_Null_To_Empty();

            query = $@"SELECT  '' AS Status
                                , odr.SubSeq
                                , odr.SerialNo
								, odr.OrderNo
								, odr.OrderSeq

								, odr.WorkDate
                                , odr.StartDate
                                , odr.EndDate

								, odr.ItemCode
                                , cm.ItemName
                                , cm.Alias
                                , cm.Size
                                , cm.UnitCode
                                , cm.ItemClass1
                                , cm.ItemClass2
                                , cm.ItemClass3

                                , op.LineCode
								, odr.ProcessCode
								, cp.ProcessName
								, odr.WorkerID
                                , odr.WorkDate

                                , odr.WorkQty
                                , odr.GoodQty
                                ,  ISNULL(odr.NgQty, 0) NgQty
                                ,  CASE WHEN ISNULL(odr.NgQty, 0) = 0 OR ISNULL(odr.NgQty, 0) = 0
                                    THEN NULL
                                    ELSE (ISNULL(odr.NgQty, 0) / ISNULL(odr.WorkQty, 0) )END AS NgPer

                                , IIF( (SELECT COUNT(ExamNo)
                                        FROM QM_ExamResult
                                        WHERE CorpId = odr.CorpId
                                        AND ExamDivCd = '30'
                                        AND RefNo = odr.SerialNo
                                       ) > 0 , 'Y', 'N') AS ChkProc --공정검사 유무
                                , odr.MachineCode
                                , op.OrderQty
                                , CASE
										WHEN odr.WorkQty IS NULL OR odr.WorkQty = 0
											THEN ISNULL(odr.GoodQty, 0) + ISNULL(odr.NgQty, 0)
										ELSE odr.WorkQty
									END AS WorkQty

                        FROM PM_OrderResult odr
                        LEFT JOIN PM_OrderProcess op ON odr.CorpId = op.CorpId AND odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq
                        LEFT JOIN CM_Item cm ON odr.CorpId = cm.CorpId AND odr.ItemCode = cm.ItemCode
                        LEFT JOIN CM_process cp ON odr.CorpId = cp.CorpId AND odr.ProcessCode = cp.ProcessCode
                        WHERE odr.CorpID = '{WrGlobal.CorpID}'";

            if(startDate != "")
			{
                query += $"\nAND odr.WorkDate >= '{startDate}'";
			}

            if (endDate != "")
            {
                query += $"\nAND odr.WorkDate <= '{endDate}'";
            }

            if (OrderNo != "NULL")
            {
                query += $" AND odr.OrderNo = {OrderNo}";
            }

            if (OrderSeq != 0)
            {
                query += $" AND OP.OrderSeq = {OrderSeq}";
            }

            if (SerialNo != "")
            {
                query += $" AND odr.SerialNo LIKE '%{SerialNo}%'";
            }

            if (ProcessCode != "" && ProcessCode.ToUpper() != "ALL")
            {
                query += $" AND odr.ProcessCode = '{ProcessCode}'";
            }

            if (ItemCode != "" && ItemCode.ToUpper() != "ALL")
            {
                query += $" AND odr.ItemCode = '{ItemCode}'";
            }

            query += " ORDER BY odr.OrderNo, odr.SerialNo DESC";

            return db.GetDataTable(query);
        }

        public DataTable SelectPM_OrderResult_OrderNo()
        {
            query = $@"
	                    SELECT OrderNo, OrderSeq, SubSeq
		                FROM PM_OrderResult
		                WHERE CorpId= '{WrGlobal.CorpID}'
		                AND OrderNo = '{OrderNo}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResultView()
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            query = $@"SELECT 
                                  odr.SubSeq
                                , odr.SerialNo
								, odr.OrderNo
								, odr.OrderSeq

								, odr.WorkDate

								, wo.CustCode
								, odr.ItemCode
								, cm.ItemName

								, odr.ProcessCode
								, odr.ProcessCode AS ProcessName

                                , odr.WorkDate
                                , odr.GoodQty
                                , ISNULL(odr.NgQty, 0) NgQty
                                , odr.StartDate
                                , odr.EndDate
                                , IIF( (SELECT COUNT(ExamNo) 
                                        FROM QM_ExamResult
                                        WHERE CorpId = odr.CorpId 
                                        AND ExamDivCd = '30'
                                        AND RefNo = odr.SerialNo
                                       ) > 0 , 'Y', 'N') AS ChkProc --공정검사 유무
                        FROM PM_OrderResult odr
						LEFT JOIN PM_ProductPlan wo ON wo.OrderNo = odr.OrderNo
                        INNER JOIN CM_Item cm ON odr.CorpId = cm.CorpId AND odr.ItemCode = cm.ItemCode 
                        WHERE odr.CorpID = '{WrGlobal.CorpID}' 
                        AND odr.WorkDate BETWEEN '{StartDt} 00:00:00' AND '{EndDt} 23:59:59'";

            if (ProcessCode != "")
            {
                query += $@"AND odr.ProcessCode = '{ProcessCode}' ";
            }
            if (ItemCode != "NULL")
            {
                query += $@"AND wo.ItemCode = {ItemCode} ";
            }

            query += "ORDER BY odr.OrderNo, odr.SerialNo DESC";
            return db.GetDataTable(query);
        }

        public bool ExistsResult_No(string orderNo)
        {
            query = $@"
                       SELECT 1
                        FROM PM_OrderResult
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{orderNo}'
                        ";

            DataRow dr = db.GetDataRow(query);

            bool result = false;

            if (dr != null)
            {
                result = dr[0].ConvertString_Null_To_Empty() == "1";
            }

            return result;
        }

        public bool ExistsResult_NoSeq(string orderNo, int orderSeq)
        {
            query = $@"
                       SELECT 1
                        FROM PM_OrderResult
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{orderNo}'
                        AND OrderSeq = '{orderSeq}'
                        ";

            DataRow dr = db.GetDataRow(query);

            bool result = false;

            if (dr != null)
            {
                result = dr[0].ConvertString_Null_To_Empty() == "1";
            }

            return result;
        }

        public DataTable SelectMatInput(string fromDate, string toDate)
        {
            //공정실적 자재투입현황
            ItemCode = ItemCode.ConvertString_Null_To_Empty();

            query = $@"
                         SELECT	-- 실적
		                          WorkDate				-- 생산일자
		                        , A.OrderNo			-- 지시번호
		                        , A.OrderSeq		-- 지시번호 순번
		                        , A.SubSeq			-- 실적번호 순번
		                        , A.SerialNo			--  시리얼번호
		                        , A.ProcessCode		-- 공정코드
		                        , cp.ProcessName		-- 공정명
		                        , A.ItemCode    P_ItemCode	-- 품목코드
		                        , cm.ItemName	P_ItemName	-- 품명
		                        , cm.Alias			P_Alias	-- 품번
		                        , cm.Size		P_Size	-- 규격
		                        , A.GoodQty		
								, A.NgQty
								, A.WorkQty

		                        -- 출고
		                        , M.SalesNo
		                        , M.SalesSeq
		                        , M.SalesDate
		                        , M.ItemCode M_ItemCode
								, cml.ItemName M_ItemName
		                        , M.Qty
								, M.LotNo
                                , cci.ExtraValue1 AS HSCODE

                        FROM PM_OrderResult A
                        LEFT JOIN (

	                        SELECT sd.CorpId, sd.SalesNo, sd.SalesSeq, sm.SalesDate, sd.ItemCode ItemCode, Qty, BarCodeNo, LotNo
	                        FROM BM_SalesD sd
							LEFT JOIN BM_SalesM sm ON sd.CorpId = sm.CorpId AND sd.SalesNo = sm.SalesNo
	                        WHERE sd.CorpId = '{WrGlobal.CorpID}'
                            AND sd.ReqType = '자재출고'

                        ) M ON A.SerialNo = M.BarCodeNo
                        LEFT JOIN PM_OrderProcess op On A.CorpId = op.CorpId AND A.OrderNo = op.OrderNo AND A.OrderSeq = op.OrderSeq
                        LEFT JOIN CM_Process cp On A.CorpId = cp.CorpId AND A.ProcessCode = cp.ProcessCode
                        LEFT JOIN CM_Item cm On A.CorpId = cm.CorpId AND A.ItemCode = cm.ItemCode
						LEFT JOIN CM_Item cml On M.CorpId = cml.CorpId AND M.ItemCode = cml.ItemCode
						LEFT JOIN CM_CateItem cci ON A.CorpId = cci.CorpId AND cci.class = 'A01210' AND cml.ItemClass1 = cci.ClassItemCode
    
                        WHERE A.CorpID = '{WrGlobal.CorpID}'
                        AND M.SalesNo IS NOT NULL
                        AND cml.ItemType <> '20' --완제품 제외
                        AND A.WorkDate BETWEEN '{fromDate}' AND '{toDate}'";

            //if(ItemCode != "")
            //{
            //    query += $"\nAND A.ItemCode LIKE @ItemCode";
            //}

            //if (Line_Cd != "" && Line_Cd != null && Line_Cd.ToUpper() != "ALL")
            //{
            //    query += $"\nAND A.Line_Cd = '{Line_Cd}'";
            //}

            query += $"\nORDER BY OrderNo, Orderseq, SerialNo";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResult_Exam(string exam_Div, string startDt = "", string endDt = "", string[] Item_Div = null, List<string> processCds = null, string serialNo = "")
        {
            //실적 기준 공정검사
            query = $@"SELECT DISTINCT 
                                         ODR.SerialNo AS RefNo
                                       , NULL AS RefSeq
                                       , ODR.WorkDate

                                       , ODR.ProcessCode
                                       , CP.ProcessName

                                       , OP.LineCode
                                       , CL.LineName

                                       , OP.MachineCode
                                       , MC.MachineName

                                       , OP.OrderNo
                                       , OP.OrderSeq

                                       , WO.ItemCode ItemCode
                                       , CM2.ItemName
                                       , CM2.Alias
                                       , CM2.Size
                                       
                                        , CM2.ItemClass1
                                        , CM2.ItemClass2
                                        , CM2.ItemClass3
                                        , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A01210', cm2.ItemClass1)) AS ClassName1	--품목분류1
                                        , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A01220', cm2.ItemClass2)) AS ClassName2	--품목분류2
                                        , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A01230', cm2.ItemClass3)) AS ClassName3	--품목분류3

                                        , CM2.UnitWeight
                                        , CM2.Texture
                                        , CM2.Type
                                        , CM2.UnitCode
                                        , CM2.ColorCode


                                       , OP.ItemCode ItemCode
                                       , CM.ItemName ItemName
                                       , CM.Alias MaterialAlias

                                       , OP.OrderQty
                                       , OP.WorkQty 
                                       , ODR.GoodQty + ODR.NgQty ExamQty
                                       , ODR.GoodQty
                                       , ODR.NgQty
                                       , ODR.SubSeq

                                       , OP.[Status]

                                       --, IIF(WI.SerialNo IS NULL, 'N', 'Y') AS ChkProc
                               	   , CASE WHEN ( SELECT COUNT(*) 
									  FROM QM_ExamResult 
									  WHERE CorpId = OP.CorpId
									  AND ExamDivCd = '{exam_Div}'            --초중종
									  AND RefNo = ODR.SerialNo
                                         ) > 0 THEN 'Y'
										ELSE 'N' END AS ExamYn --검사 여부Yn

                                   FROM PM_OrderResult ODR
                                   LEFT JOIN PM_OrderProcess OP  ON ODR.CorpId = OP.CorpId AND ODR.OrderNo = OP.OrderNo AND ODR.OrderSeq = OP.OrderSeq
                                   INNER JOIN PM_ProductPlan   WO  ON ODR.CorpId = OP.CorpId AND WO.OrderNo = OP.OrderNo
                                   LEFT JOIN CM_Process    CP  ON ODR.CorpId = OP.CorpId AND OP.ProcessCode = CP.ProcessCode
                                   LEFT JOIN CM_Line       CL  ON ODR.CorpId = OP.CorpId AND OP.LineCode = CL.LineCode
                                   LEFT JOIN EQ_Machine  MC  ON ODR.CorpId = OP.CorpId AND OP.MachineCode = MC.MachineCode
                                   INNER JOIN CM_Item  CM  ON ODR.CorpId = OP.CorpId AND OP.ItemCode = cm.ItemCode 
                                   INNER JOIN CM_Item  CM2 ON ODR.CorpId = OP.CorpId AND WO.ItemCode = CM2.ItemCode

                                   WHERE OP.CorpId = '{WrGlobal.CorpID}'
                                       ";

            if (startDt != "")
            {
                query += $"\n AND ODR.WorkDate >= '{startDt}'";
            }

            if (endDt != "")
            {
                query += $"\n AND ODR.WorkDate <= '{endDt} 23:59'";
            }

            if (ProcessCode != "")
            {
                query += $"\n AND ODR.ProcessCode = '{ProcessCode}' ";
            }

            if (LineCode != "")
            {
                query += $"\n AND OP.LineCode = '{LineCode}' ";
            }

            if (MachineCode != "")
            {
                query += $"\n AND OP.MachineCode = '{MachineCode}' ";
            }

            if (ItemCode != "" && ItemCode.ToUpper() != "ALL")
            {
                query += $"\n AND OP.ItemCode = '{ItemCode}' ";
            }

            if (serialNo != "")
            {
                query += $"\n AND ODR.serialNo = '{serialNo}' ";
            }

            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND CM.ItemType IN(";
                for (int idx = 0; idx < Item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $"'{Item_Div[idx]}'";
                }
                query += ")";
            }

            query += "\n ORDER BY ODR.SerialNo";

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public DataTable Select_PM_OrderResult_ByQM_WorkDefect(string fromDate, string toDate, string custCode)
        {
            query = $@" -- 불량 유형별 집계 쿼리
                        DECLARE @Start_Date DATETIME = '{fromDate}'
                        DECLARE @End_Date DATETIME = '{toDate}'
                        DECLARE @CustCode VARCHAR(20) = '{custCode}'          -- 빈 값이면 전체 거래처
                        DECLARE @ItemCode VARCHAR(20) = '{ItemCode}'; -- 빈 값이면 전체 품번

                        -- 기본 작업 실적 정보 조회
                        WITH WorkResults AS (
                            SELECT 
                                OR1.OrderNo,
		                        OR1.SerialNo,
		                        OR1.OrderSeq,
		                        OR1.SubSeq, 
                                C.CustCode,
                                C.CustName,
                                SR.ItemCode as ItemCode,
                                M.ItemName,
                                OR1.WorkDate,
                                OR1.GoodQty + OR1.NgQty AS WorkQty -- 총작업수량
                            FROM 
                                PM_OrderResult OR1
                                INNER JOIN PM_SdRoute SR ON OR1.OrderNo = SR.OrderNo AND OR1.OrderSeq = SR.ChildID
                                INNER JOIN CM_Item M ON SR.ItemCode = M.ItemCode
                                INNER JOIN CM_Cust C ON M.CustCode = C.CustCode
                            WHERE 
                                OR1.WorkDate BETWEEN @Start_Date AND @End_Date
                                AND (@CustCode = '' OR C.CustCode = @CustCode)
                                AND (@ItemCode = '' OR SR.ItemCode = @ItemCode)
                        )

                        -- 불량 정보 집계
                        SELECT
                            WR.OrderNo,
	                        WR.OrderSeq,
	                        WR.SubSeq,
                            WR.CustName,
                            WR.ItemCode, 
                            WR.ItemName,
                            CONVERT(VARCHAR(10), WR.WorkDate, 120) AS WorkDate,
	                        WD.ReworkID,
                            CD.Title AS FaultCodeName,
                            WR.WorkQty,
                            WD.Qty as BadQty --ISNULL(SUM(WD.Qty), 0) AS BadQty
                        FROM
                            WorkResults WR
                            INNER JOIN QM_WorkDefect WD ON WR.OrderNo = WD.OrderNo
                            LEFT JOIN CM_DefectCode CD ON WD.DefectCode = CD.FaultCode
                        GROUP BY
                            WR.OrderNo,
                            WR.CustName,
                            WR.ItemCode,
                            WR.ItemName,
                            WR.WorkDate,
                            CD.Title,
	                        WR.WorkQty,
	                        WR.OrderSeq, WR.SubSeq, WD.ReworkID,
	                        WD.Qty
                        ORDER BY
                            WR.WorkDate DESC,
                            WR.OrderNo
                            --CD.Title
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_Bad_Total(string yyyyMM)
        {
            query = $@"DECLARE 
                                @YYYYMM CHAR(6) = '{yyyyMM}'
                                , @ProcessCode VARCHAR(10) = '{ProcessCode}'
                                , @ItemCode VARCHAR(20) = '{ItemCode}'

                                CREATE TABLE #TEMP
                                (
                                DD VARCHAR(20)		--일
                                , PART VARCHAR(30)	--구분
                                , Qty FLOAT			--수량
                                );

                                WITH CTE_1st
                                AS
                                (
                                SELECT [DD], 
                                SUM(Work_Qty) AS Work_Qty, 
                                SUM(Work_Qty) - SUM(Bad_Qty) AS GOOD_QTY, 
                                SUM(Bad_Qty) AS Bad_Qty, 
                                CASE WHEN SUM(Bad_Qty) = 0 OR SUM(Work_Qty) = 0 THEN 0 ELSE ROUND((SUM(Bad_Qty) / SUM(Work_Qty)) * 1000000, 2)
                                END AS PPM
                                , CASE WHEN SUM(Bad_Qty) = 0 OR SUM(Work_Qty) = 0 THEN 0 ELSE ROUND((SUM(Bad_Qty) / SUM(Work_Qty)) * 100, 2)
                                END AS PPH                
                                FROM
	                                ( 
	                                SELECT CONVERT(VARCHAR, WorkDate, 112) AS EndDate, 
		                                    RIGHT(CONVERT(VARCHAR, WorkDate, 112), 2) AS DD,
		                                    ISNULL(SUM(GoodQty), 0) + ISNULL(SUM(NgQty), 0) AS Work_Qty,
		                                    --ISNULL(SUM(NgQty), 0) AS Bad_Qty
                                            0 AS Bad_Qty
	                                FROM PM_OrderResult 
	                                WHERE CONVERT(VARCHAR, WorkDate, 112) LIKE @YYYYMM + '%'
                                    AND ISNULL(ProcessCode, '') LIKE '%' + @ProcessCode + '%'
	                                GROUP BY CONVERT(VARCHAR, WorkDate, 112) 

                                    UNION ALL
                	    
                	                --불량 
                	                SELECT 
                	    		            CONVERT(nvarchar(8),WorkDate, 112)
                	    	              , RIGHT(CONVERT(nvarchar(8),WorkDate, 112), 2) AS DD
                	    	              , 0 AS Work_Qty
                	    	              , ISNULL(SUM(B.Qty), 0)  AS Bad_Qty
                                          --, 0  AS Bad_Qty
                
                	                FROM QM_WorkDefect B
                	                WHERE CONVERT(nvarchar(8),WorkDate, 112) LIKE @YYYYMM + '%'
                	                --AND ISNULL(Line_Cd, '') LIKE '%' + @Line_Cd + '%'
                	                AND ISNULL(FromProcessCode, '') LIKE '%' + @ProcessCode + '%'
                                    GROUP BY WorkDate
	                                ) AS A
	                                GROUP BY [DD]
                                )

                                INSERT INTO #TEMP(DD, PART, Qty)
	                                SELECT DD, PART, Qty
	                                FROM
	                                (
		                                SELECT DD, Work_Qty, Good_Qty, Bad_Qty, PPM, PPH
		                                FROM CTE_1st
	                                )_A
	                                CROSS APPLY(
		                                    SELECT '총생산', ISNULL(Work_Qty, 0) UNION ALL
                                            SELECT '총양품', ISNULL(Good_Qty, 0) UNION ALL
                                            SELECT '총불량', ISNULL(Bad_Qty, 0) UNION ALL 
                                            SELECT '불량률[PPM]' , ISNULL(PPM, 0) UNION ALL 
                                            SELECT '불량률[PPH]',  ISNULL(PPH, 0)
				                                ) AS B(PART, Qty)
                            
                                ";

            query += $@"

                  --불량유형 데이터
                
                	INSERT INTO #TEMP(DD, PART, Qty)
                	SELECT 
                            RIGHT(CONVERT(nvarchar(8),WorkDate, 112), 2) AS DD
                            , C.Title AS PART
                            , ISNULL(SUM(B.Qty), 0)  AS Bad_Qty
                
                    FROM QM_WorkDefect B
                    LEFT JOIN CM_DefectCode C ON B.DefectCode = C.FaultCode --AND ExamDivCd = '30' --공정검사만
                	WHERE CONVERT(nvarchar(8),WorkDate, 112) LIKE @YYYYMM + '%'
                	--AND ISNULL(B.Line_Cd, '') LIKE '%' + @Line_Cd + '%'
                	AND ISNULL(B.FromProcessCode, '') LIKE '%' + @ProcessCode + '%'
					AND B.CorpID = '{WrGlobal.CorpID}'";
            /*
            if (MachineCode != "NULL")
            {
                query += $@" AND ISNULL(B.MachineCode, '') LIKE '%' +  @MachineCode + '%'      ";
            }
            if (ItemCode != "NULL")
            {
                query += $@" AND ISNULL(B.ItemCode, '') LIKE '%' + @ItemCode + '%'     ";
            }
            */
            query += $@" GROUP BY WorkDate, B.DefectCode, C.Title
                ";


            query += $@"
                                /* 이전 합계 데이터
                                --합계데이터
                                INSERT INTO #TEMP(DD, PART, Qty)
                                SELECT 'TOTAL'
                                , PART
                                , ROUND(IIF(PART LIKE '불량률%' , AVG(QTY), SUM(Qty)) , 2)
                                FROM #TEMP
                                GROUP BY PART
                                */



                        -- ========= 신규합계데이터(기존 총양품/총불량과 PPM,PPH 안맞던 문제 해결) ===========
                        ;WITH TOT AS (
							SELECT
								SUM(CASE WHEN PART = '총생산' THEN Qty ELSE 0 END) AS TotalWork,
								SUM(CASE WHEN PART = '총양품' THEN Qty ELSE 0 END) AS TotalGood,
								SUM(CASE WHEN PART = '총불량' THEN Qty ELSE 0 END) AS TotalBad
							FROM #TEMP
							WHERE DD <> 'TOTAL'
						)


						INSERT INTO #TEMP(DD, PART, Qty)
						SELECT 'TOTAL', '총생산', TotalWork FROM TOT
						UNION ALL
						SELECT 'TOTAL', '총양품', TotalGood FROM TOT
						UNION ALL
						SELECT 'TOTAL', '총불량', TotalBad FROM TOT
						UNION ALL
						SELECT 'TOTAL', '불량률[PPM]',
							   CASE WHEN TotalWork = 0 THEN 0
									ELSE ROUND((TotalBad / TotalWork) * 1000000.0, 2)
							   END
						FROM TOT
						UNION ALL
						SELECT 'TOTAL', '불량률[PPH]',
							   CASE WHEN TotalWork = 0 THEN 0
									ELSE ROUND((TotalBad / TotalWork) * 100.0, 2)
							   END
						FROM TOT

						UNION ALL
						
						SELECT 'TOTAL', PART, SUM(Qty)
						FROM #TEMP
						WHERE DD <> 'TOTAL'
						  AND PART NOT IN ('총생산','총양품','총불량','불량률[PPM]','불량률[PPH]')
						GROUP BY PART;


                                SELECT *
                                FROM 
                                (
	                                SELECT * FROM #TEMP
                                ) AS C 
                                PIVOT 
                                (
	                                MAX(Qty) FOR DD IN ([TOTAL],[01],[02],[03],[04],[05],[06],[07],[08],[09],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])
                                ) AS PVT
                                ORDER BY CASE PART WHEN '총생산' THEN 1 
                                WHEN '총양품' THEN 3 
                                WHEN '총불량' THEN 5
                                WHEN '불량률[PPH]' THEN 7
                                WHEN '불량률[PPM]' THEN 9                
                                ELSE 10 END, PART               
                
                                DROP TABLE #TEMP";

            return db.GetDataTable(query);
        }

        public DataTable Select_BadTotalByMonth(string yyyy)
        {
            query = $@"DECLARE 
                                @YYYY CHAR(6) = '{yyyy}'
                                , @ProcessCode VARCHAR(10) = '{ProcessCode}'
                                , @ItemCode VARCHAR(20) = '{ItemCode}'

                                CREATE TABLE #TEMP
                                (
                                MM VARCHAR(20)      --월
                                , PART VARCHAR(30)	--구분
                                , Qty FLOAT			--수량
                                );

                                WITH CTE_1st
                                AS
                                (
                                SELECT
                                        [MM],
                                        SUM(Work_Qty) AS Work_Qty,
                                        SUM(Work_Qty) - SUM(Bad_Qty) AS Good_Qty,
                                        SUM(Bad_Qty) AS Bad_Qty,

                                        CASE WHEN SUM(Bad_Qty) = 0 OR SUM(Work_Qty) = 0
                                            THEN 0
                                            ELSE ROUND((SUM(Bad_Qty) / SUM(Work_Qty)) * 1000000, 2) END AS PPM,

                                        CASE WHEN SUM(Bad_Qty) = 0 OR SUM(Work_Qty) = 0
                                            THEN 0
                                            ELSE ROUND((SUM(Bad_Qty) / SUM(Work_Qty)) * 100, 2) END AS PPH
                                    FROM
                                    (
                                        SELECT
                                            LEFT(CONVERT(VARCHAR, WorkDate, 112), 6) AS EndYM,
                                            RIGHT(LEFT(CONVERT(VARCHAR, WorkDate, 112), 6), 2) AS MM,
                                            ISNULL(SUM(GoodQty),0) + ISNULL(SUM(NgQty),0) AS Work_Qty,
                                            0 AS Bad_Qty
                                        FROM PM_OrderResult
                                        WHERE LEFT(CONVERT(VARCHAR, WorkDate, 112), 4) = @YYYY
                                          AND ISNULL(ProcessCode, '') LIKE '%' + @ProcessCode + '%'
                                        GROUP BY LEFT(CONVERT(VARCHAR, WorkDate, 112), 6)

                                        UNION ALL

                                        --불량
                                        SELECT
                                            LEFT(CONVERT(VARCHAR, WorkDate, 112), 6),
                                            RIGHT(LEFT(CONVERT(VARCHAR, WorkDate, 112), 6), 2) AS MM,
                                            0 AS Work_Qty,
                                            ISNULL(SUM(B.Qty), 0) AS Bad_Qty
                                        FROM QM_WorkDefect B
                                        WHERE LEFT(CONVERT(VARCHAR, WorkDate, 112), 4) = @YYYY
                                          AND ISNULL(FromProcessCode, '') LIKE '%' + @ProcessCode + '%'
                                        GROUP BY LEFT(CONVERT(VARCHAR, WorkDate, 112), 6)
                                    ) A
                                    GROUP BY [MM]
                                )

                                INSERT INTO #TEMP(MM, PART, Qty)
                                SELECT MM, PART, Qty
                                FROM
                                (
                                    SELECT MM, Work_Qty, Good_Qty, Bad_Qty, PPM, PPH
                                    FROM CTE_1st
                                ) A
                                CROSS APPLY (
                                    SELECT '총생산', ISNULL(Work_Qty,0) UNION ALL
                                    SELECT '총양품', ISNULL(Good_Qty,0) UNION ALL
                                    SELECT '총불량', ISNULL(Bad_Qty,0) UNION ALL
                                    SELECT '불량률[PPM]', ISNULL(PPM,0) UNION ALL
                                    SELECT '불량률[PPH]', ISNULL(PPH,0)
                                ) B(PART, Qty)
                                ";

            query += $@"

                  --불량유형 데이터  
                	INSERT INTO #TEMP(MM, PART, Qty)
                    SELECT
                        RIGHT(LEFT(CONVERT(VARCHAR, WorkDate,112), 6), 2) AS MM,
                        C.Title AS PART,
                        ISNULL(SUM(B.Qty), 0)
                    FROM QM_WorkDefect B
                    LEFT JOIN CM_DefectCode C ON B.DefectCode = C.FaultCode
                    WHERE LEFT(CONVERT(VARCHAR, WorkDate,112), 4) = @YYYY
                      AND ISNULL(B.FromProcessCode, '') LIKE '%' + @ProcessCode + '%'
					  AND B.CorpID = '{WrGlobal.CorpID}'";
            /*
            if (MachineCode != "NULL")
            {
                query += $@" AND ISNULL(B.MachineCode, '') LIKE '%' +  @MachineCode + '%'      ";
            }
            if (ItemCode != "NULL")
            {
                query += $@" AND ISNULL(B.ItemCode, '') LIKE '%' + @ItemCode + '%'     ";
            }
            */
            query += $@" GROUP BY LEFT(CONVERT(VARCHAR, WorkDate,112), 6), B.DefectCode, C.Title
                ";


            query += $@"
                                /* 이전 합계 데이터
                                --합계데이터
                                INSERT INTO #TEMP(DD, PART, Qty)
                                SELECT 'TOTAL'
                                , PART
                                , ROUND(IIF(PART LIKE '불량률%' , AVG(QTY), SUM(Qty)) , 2)
                                FROM #TEMP
                                GROUP BY PART
                                */



                        -- ========= 신규합계데이터(기존 총양품/총불량과 PPM,PPH 안맞던 문제 해결) ===========
                        ;WITH TOT AS (
							 SELECT
                                    SUM(CASE WHEN PART = '총생산' THEN Qty ELSE 0 END) AS TotalWork,
                                    SUM(CASE WHEN PART = '총양품' THEN Qty ELSE 0 END) AS TotalGood,
                                    SUM(CASE WHEN PART = '총불량' THEN Qty ELSE 0 END) AS TotalBad
                                FROM #TEMP
                                WHERE MM <> 'TOTAL'
                            )
                            
                            INSERT INTO #TEMP(MM, PART, Qty)
                            SELECT 'TOTAL', '총생산', TotalWork FROM TOT
                            UNION ALL
                            SELECT 'TOTAL', '총양품', TotalGood FROM TOT
                            UNION ALL
                            SELECT 'TOTAL', '총불량', TotalBad FROM TOT
                            UNION ALL
                            SELECT 'TOTAL', '불량률[PPM]',
                                   CASE WHEN TotalWork = 0 THEN 0
                                        ELSE ROUND((TotalBad / TotalWork) * 1000000.0, 2)
                                   END
                            FROM TOT
                            UNION ALL
                            SELECT 'TOTAL', '불량률[PPH]',
                                   CASE WHEN TotalWork = 0 THEN 0
                                        ELSE ROUND((TotalBad / TotalWork) * 100.0, 2)
                                   END
                            FROM TOT
                            UNION ALL
                            SELECT 'TOTAL', PART, SUM(Qty)
                            FROM #TEMP
                            WHERE MM <> 'TOTAL'
                              AND PART NOT IN ('총생산','총양품','총불량','불량률[PPM]','불량률[PPH]')
                            GROUP BY PART;


                                SELECT *
                                FROM
                                (
	                                SELECT * FROM #TEMP
                                ) AS C
                                PIVOT
                                (
	                                MAX(Qty) FOR MM IN ([TOTAL], [01],[02],[03],[04],[05],[06],[07],[08],[09],[10],[11],[12])
                                ) AS PVT
                                ORDER BY CASE PART WHEN '총생산' THEN 1
                                WHEN '총양품' THEN 3
                                WHEN '총불량' THEN 5
                                WHEN '불량률[PPH]' THEN 7
                                WHEN '불량률[PPM]' THEN 9
                                ELSE 10 END, PART
                
                                DROP TABLE #TEMP";

            return db.GetDataTable(query);
        }

        public DataTable Select_BadAnalysis(string fromDate, string toDate, string itemClass1 = "", string itemClass2 = "")
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();

            query = $@"WITH FirstQuery AS (
                                SELECT sub.ProcessCode, sub.ItemCode, cm.ItemName, sub.Work_Qty AS 생산량,sub.Good_Qty AS 양품수량,sub.Bad_Qty  AS 불량수량, 
                                FORMAT(IIF(ISNULL(sub.Work_Qty, 0) = 0, 0, ROUND( ((ISNULL(sub.Bad_Qty, 0) * 1.0 / ISNULL(sub.Work_Qty, 0)) * 100), 2)), 'N2') AS 불량률,
                                cm.ItemClass1, cm.ItemClass2
                                FROM
                                (
                                    SELECT A.ProcessCode, C.ItemCode, 
                                    SUM(A.GoodQty) + SUM(A.NGqty) AS Work_Qty, SUM(A.GoodQty) AS Good_Qty, SUM(A.NGqty) AS Bad_Qty
                                    FROM PM_OrderResult A
                                    LEFT JOIN PM_OrderProcess B ON A.OrderNo = B.OrderNo AND A.OrderSeq = B.OrderSeq
                                    LEFT JOIN PM_ProductPlan C ON A.OrderNo = C.OrderNo
                                    WHERE A.WorkDate BETWEEN CONVERT(DATETIME, '{fromDate}') AND DATEADD(SECOND, -1, DATEADD(DAY, 1, CONVERT(DATETIME, '{toDate}'))) ";

            if (ProcessCode != "NULL" && ProcessCode != "ALL")
            {
                query += $" AND A.ProcessCode  = {ProcessCode} ";
            }

            if (ItemCode != "NULL" && ItemCode != "ALL")
            {
                query += $"AND C.ItemCode = {ItemCode}\n";
            }

            query += @" GROUP BY A.ProcessCode, C.ItemCode
                        ) sub
                        LEFT JOIN CM_Item cm ON cm.ItemCode = sub.ItemCode
                        LEFT JOIN CM_Process cp ON cp.ProcessCode = sub.ProcessCode
                        WHERE 1=1 ";

            if (itemClass1 != "NULL" && itemClass1 != "ALL")
            {
                query += $" AND cm.ItemClass1 = '{itemClass1}' ";
            }

            if (itemClass2 != "NULL" && itemClass2 != "ALL")
            {
                query += $" AND cm.ItemClass2 = '{itemClass2}' ";
            }

            query += @" ),
                        RankedFirstQuery AS (
                            SELECT *,
                                   ROW_NUMBER() OVER (ORDER BY 불량률 DESC) AS 'Index'
                            FROM FirstQuery
                        )
                        SELECT *
                        FROM RankedFirstQuery
                        ORDER BY ItemName";

            return db.GetDataTable(query);
        }

        public DataTable Select_NonWork(string startDate, string endDate, MachineInfo info)
        {
            /*
			 * 조업시간 : 임의설정 = 24
			 * 계획정지시간 : 비가동유형NonWork_Div_Cd 비가동부분이 계획정지인 시간의 합 (공통코드 10:계획정지, 20:비계획정지)
			 * 부하시간 = 조업시간 - 계획정지시간
			 * 가동시간 = 부하시간 - 정지시간
			 * 시간가동률 = (부하시간 - 정지시간) / 부하시간 * 100
			 */

            string line_Cd = info.Line_Cd;
            string ProcessCode = info.ProcessCode;

            query = $@"

					DECLARE @TMP TABLE
                    (
                    WorkDay VARCHAR(8)
                    , LineCode VARCHAR(10)
                    , ProcessCode VARCHAR(10)
                    , MachineCode VARCHAR(10)
                    , Min_Time DATETIME
                    , Max_Time DATETIME
                    , 조업시간		INT
                    , 부하시간		INT
                    , 가동시간		INT
                    , 계획정지시간	INT
                    , 정지시간	INT
                    , 시간가동률 FLOAT
                    )

                    DECLARE @OPHOURS INT = 24 * 60	--조업시간
                    DECLARE @CorpId VARCHAR(4) = '{WrGlobal.CorpID}'
                    , @STDT VARCHAR(8) = '{startDate}'
                    , @ENDT VARCHAR(8) = '{endDate}'

                    INSERT @TMP
                    (
                    WorkDay 
                    , ProcessCode 
                    , LineCode  
                    , MachineCode 
                    , Min_Time 
                    , Max_Time 
                    , 조업시간
                    )

                    SELECT CONVERT(VARCHAR, A.WorkDate,112) AS WorkDay, ISNULL(A.ProcessCode, '') AS ProcessCode, ISNULL(B.LineCode, '') AS LineCode, ISNULL(A.MachineCode, '') AS MachineCode, 
                                MIN(A.StartDate) AS MIN_TIME, MAX(A.EndDate) AS MAX_TIME, @OPHOURS
                    FROM PM_OrderResult A
                    LEFT JOIN PM_OrderProcess B ON A.OrderNo = B.OrderNo AND A.OrderSeq = B.OrderSeq
                    LEFT JOIN CM_Line C ON B.LineCode = C.LineCode
                    WHERE A.CorpId = @CorpId
                    AND CONVERT(VARCHAR, A.WorkDate,112) BETWEEN @STDT AND @ENDT
                    AND ISNULL(A.ProcessCode, '') =  IIF('' = '', ISNULL(A.ProcessCode, ''), '')
                    AND ISNULL(B.LineCode, '') = IIF('' = '', ISNULL(B.LineCode, ''), '')
                    GROUP BY A.WorkDate, A.ProcessCode, B.LineCode, ISNULL(A.MachineCode, '')

                    --정지 구분코드 존재x
                    UPDATE @TMP
                    SET 계획정지시간 = 0
                    , 정지시간 = ISNULL(B.STOP_TIME, 0)
                    FROM @TMP A
                    LEFT JOIN (
                    SELECT WorkDay, ISNULL(LineCode, '') AS LineCode, ISNULL(ProcessCode, '') AS ProcessCode, ISNULL(MachineCode, '') AS MachineCode
                    , SUM(DATEDIFF(MINUTE, StartTime, EndTime)) AS Stop_Time
                    FROM EQ_Operation
                    WHERE CorpId = @CorpId
                    AND WorkDay BETWEEN @STDT AND @ENDT
                    GROUP BY WorkDay, LineCode, ProcessCode, MachineCode
                    ) B
                    ON A.WorkDay = B.WorkDay
                    AND A.LineCode	 = B.LineCode
                    AND A.ProcessCode = B.ProcessCode
                    AND A.MachineCode = B.MachineCode

                    UPDATE @TMP
                    SET 부하시간 = 조업시간 - 계획정지시간


                    UPDATE @TMP
                    SET 가동시간 = 부하시간 - 정지시간
                    , 시간가동률 =  ROUND((CAST(부하시간 AS FLOAT) - 정지시간) / CAST(부하시간 AS FLOAT) * 100, 2)

                   SELECT tp.*, cl.LineName, cp.ProcessName, cm.MachineName
                    FROM @TMP tp
                    LEFT JOIN CM_Line cl	ON cl.CorpId = @CorpId AND tp.LineCode = cl.LineCode
                    LEFT JOIN CM_Process cp ON cp.CorpId = @CorpId AND tp.ProcessCode = cp.ProcessCode
                    LEFT JOIN EQ_Machine cm ON cm.CorpId = @CorpId AND tp.MachineCode = cm.MachineCode
                    WHERE 1=1 ";

            if (line_Cd != "")
            {
                query += $@"AND tp.LineCode = '{line_Cd}' ";
            }

            if (ProcessCode != "")
            {
                query += $@"AND tp.ProcessCode = '{ProcessCode}' ";
            }


            return db.GetDataTable(query);
        }

        public DataTable chkPM_OrderResult()
        {
            query = $@"SELECT 1
                       FROM PM_OrderResult
                       WHERE OrderNo = '{OrderNo}'";

            return db.GetDataTable(query);
        }

        public bool Ins_PM_OrderResult()
        {
            query = $@"INSERT INTO PM_OrderResult
	                    (
		                     CorpId
                            , OrderNo
                            , OrderSeq
                            , SubSeq
                            , ProcessCode
                            , MachineCode
                            , ItemCode
                            , WorkerID
                            , LotNo
                            , SerialNo
                            , WorkDate
                            , StartDate
                            , EndDate
                            , WorkQty
                            , GoodQty
                            , NgQty
                            , Result
                            , CreId
                            , CreIP
                            , CreDt
	                    )
                      VALUES
                       (
                          '{CorpId}'
                        , '{OrderNo}'
                        , {OrderSeq}
                        , {SubSeq}
                        , {ProcessCode.ConvertStr_Empty_To_Null()}
                        , {MachineCode.ConvertStr_Empty_To_Null()}
                        , {ItemCode.ConvertStr_Empty_To_Null()}
                        , {WorkerID.ConvertStr_Empty_To_Null()}
                        , {LotNo.ConvertStr_Empty_To_Null()}
                        , {SerialNo.ConvertStr_Empty_To_Null()}
                        , {WorkDate.ConvertStr_Empty_To_Null()}
                        , {StartDate.ConvertStr_Empty_To_Null()}
                        , {EndDate.ConvertStr_Empty_To_Null()}
                        , {WorkQty.ConvertNumeric_Empty_To_Null()}
                        , {GoodQty.ConvertNumeric_Empty_To_Null()}
                        , {NgQty.ConvertNumeric_Empty_To_Null()}
                        , {Result.ConvertStr_Empty_To_Null()}
                        , '{WrGlobal.LoginID}'
                        , '{WrGlobal.IpAddress}'
                        , getdate()

                        )

                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_PM_OrderProcess()
        {
            query = $@"


         UPDATE PM_OrderProcess
                SET  
                      WorkQty     = B.WorkQty
                    , GoodQty     = B.GoodQty
                    , NgQty       = B.NgQty
                    , Status    =  CASE WHEN A.OrderQty <= B.GoodQty THEN '작업완료' ELSE '작업중' END
                    , StartDate = B.StartDate
                    , EndDate   = B.EndDate
                    
					FROM PM_OrderProcess A, 
					(
						SELECT SUM(WorkQty) WorkQty, SUM(GoodQty) GoodQty, SUM(NgQty) NgQty, MIN(StartDate) StartDate, MAX(EndDate)  EndDate
						FROM  PM_OrderResult B
						WHERE CorpId    = '{CorpId}'
						AND OrderNo   = '{OrderNo}'
						AND OrderSeq  = {OrderSeq}
					) AS B
               WHERE A.CorpId = '{CorpId}'
			   AND A.OrderNo = '{OrderNo}'
			   AND A.OrderSeq = '{OrderSeq}'
";

            return db.ExecuteQuery(query);
        }

        public bool Upd_PM_ProductPlan()
        {
            //PM_SdRoute 사내공정 마지막 라우팅 순번의 지시일 경우
            query = $@"

                  UPDATE PM_ProductPlan
                    SET 
                            WorkQty  = {WorkQty.ConvertNumeric_Empty_To_Null()}
                          , GoodQty  = {GoodQty.ConvertNumeric_Empty_To_Null()}
                          , NgQty    = {NgQty.ConvertNumeric_Empty_To_Null()}

                    WHERE CorpId = '{CorpId}'
                      AND OrderNo = '{OrderNo}'
                      AND EXISTS (
                          SELECT 1
                          FROM PM_SdRoute
                          WHERE CorpId = '{CorpId}'
                            AND OrderNo = '{OrderNo}'
                            AND WorkType = '10'
                            AND RoutingSeq = (
                                SELECT MAX(RoutingSeq)
                                FROM PM_SdRoute
                                WHERE CorpId = '{CorpId}'
                                  AND OrderNo = '{OrderNo}'
                                  AND WorkType = '10'
                            )
                            AND ChildID = {OrderSeq}  -- 현재 공정이 마지막이면
                      )
            ";

            return db.ExecuteQuery(query);
        }

        public bool Stock_MakeStockByWork()
        {
            // 수불 등록
            query = $@" EXEC Stock_MakeStockByWork '{CorpId}', '{OrderNo}', {OrderSeq}, {SubSeq}, 'Y' ";

            return db.ExecuteQuery(query);
        }

        public bool Stock_MakeStockByWork2()
        {
            // 수불 등록
            // 기존 Stock_MakeStockByWork 는 최종공정만 입고처리함
            // 2는 하위자재출고 + 입고처리
            // 하위자재출고 창고 = CM_Line에 있는 InHouseID, 입고창고는 CM_Line에 OutHouseID (기존과 같음)
            query = $@" EXEC Stock_MakeStockByWork2 '{CorpId}', '{OrderNo}', {OrderSeq}, {SubSeq}, 'Y' ";

            return db.ExecuteQuery(query);
        }

        public bool Sales_MakeSalesByWork()
        {
            // 자재 자동출고
            query = $@" EXEC Sales_MakeSalesByWork '{CorpId}', '{OrderNo}', {OrderSeq}, {SubSeq}";

            return db.ExecuteQuery(query);
        }

        public int GetNextSeq()
        {
            int seq = 0;

            query = $@" SELECT MAX(SubSeq) AS MaxSeq
                        FROM PM_OrderResult 
                        WHERE CorpId    = '{CorpId}'
                            AND OrderNo   = '{OrderNo}'
                            AND OrderSeq  = {OrderSeq}
                    ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                seq = dr["MaxSeq"].ConvertInt_Null_To_Zero();
            }

            return seq + 1;
        }

        public bool Upd_PM_OrderResult1()
        {
            query = $@"

                UPDATE PM_OrderResult
                SET  
                      ProcessCode = {ProcessCode.ConvertStr_Empty_To_Null()}
                    , MachineCode = {MachineCode.ConvertStr_Empty_To_Null()}
                    , ItemCode = {ItemCode.ConvertStr_Empty_To_Null()}
                    , WorkerID    = {WorkerID.ConvertStr_Empty_To_Null()}
                    , LotNo       = {LotNo.ConvertStr_Empty_To_Null()}
                    , WorkDate    = {WorkDate.ConvertStr_Empty_To_Null()}
                    , StartDate   = {StartDate.ConvertStr_Empty_To_Null()}
                    , EndDate     = {EndDate.ConvertStr_Empty_To_Null()}
                    , WorkQty     = {WorkQty.ConvertNumeric_Empty_To_Null()}
                    , GoodQty     = {GoodQty.ConvertNumeric_Empty_To_Null()}
                    , NgQty       = {NgQty.ConvertNumeric_Empty_To_Null()}
                    , Result      = {Result.ConvertStr_Empty_To_Null()}
                    , ModId       = '{WrGlobal.LoginID}'
                    , ModIP       = '{WrGlobal.IpAddress}'
                    , ModDt       =  getdate()

               WHERE CorpId    = '{CorpId}'
                    AND OrderNo   = '{OrderNo}'
                    AND OrderSeq  = {OrderSeq}
                    AND SubSeq    = {SubSeq}
                    AND SerialNo  = '{SerialNo}'
            ";

             return db.ExecuteQuery(query);
        }

        public bool Upd_NgQTY()
        {
            query = $@"

                UPDATE PM_OrderResult
                SET
                     NgQty       = {NgQty.ConvertNumeric_Empty_To_Null()}
					, GoodQty = WorkQty - {NgQty.ConvertNumeric_Empty_To_Null()}
                    , ModId       = '{WrGlobal.LoginID}'
                    , ModIP       = '{WrGlobal.IpAddress}'
                    , ModDt       =  getdate()
               WHERE CorpId    = '{WrGlobal.CorpID}'
                    AND SerialNo  = '{SerialNo}'
					AND SubSeq    = {SubSeq}
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_PM_OrderResult_Qty()
        {
            query = $@"
                    UPDATE PM_OrderResult
                    SET WorkQty = '{GoodQty}' + '{NgQty}'
                         , GoodQty = '{GoodQty}'
                        , NgQty = '{NgQty}'
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND SerialNo = '{SerialNo}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_PM_OrderResult_WorkerID()
        {
            query = $@"
                    UPDATE PM_OrderResult
                    SET  WorkerID = '{WorkerID}'
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND OrderNo = '{OrderNo}'
                    AND OrderSeq = '{OrderSeq}'
                    AND SubSeq = '{SubSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_PM_OrderResult()
        {
            query = $@"
                DELETE PM_OrderResult
                WHERE CorpId  = '{CorpId}'
                AND OrderNo   = '{OrderNo}'
                AND OrderSeq  = {OrderSeq}
                AND SubSeq    = {SubSeq}
                AND SerialNo  = '{SerialNo}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Reset_PM_OrderProcess()
        {
            query = $@"

                UPDATE PM_OrderProcess
                SET  
                      WorkQty     = 0
                    , GoodQty     = 0
                    , NgQty       = 0
                    , Status    = '대기'
                    , StartDate = NULL
                    , EndDate   = NULL

               WHERE CorpId    = '{CorpId}'
                    AND OrderNo   = '{OrderNo}'
                    AND OrderSeq  = {OrderSeq}
            ";

            return db.ExecuteQuery(query);
        }

        public bool Reset_PM_ProductPlan()
        {
            //PM_SdRoute 사내공정 마지막 라우팅 순번의 지시일 경우
            query = $@"

                  UPDATE PM_ProductPlan
                    SET 
                            WorkQty  = 0 
                          , GoodQty  = 0 
                          , NgQty    = 0

                    WHERE CorpId = '{CorpId}'
                      AND OrderNo = '{OrderNo}'
                      AND EXISTS (
                          SELECT 1
                          FROM PM_SdRoute
                          WHERE CorpId = '{CorpId}'
                            AND OrderNo = '{OrderNo}'
                            AND WorkType = '10'
                            AND RoutingSeq = (
                                SELECT MAX(RoutingSeq)
                                FROM PM_SdRoute
                                WHERE CorpId = '{CorpId}'
                                  AND OrderNo = '{OrderNo}'
                                  AND WorkType = '10'
                            )
                            AND ChildID = {OrderSeq}  -- 현재 공정이 마지막이면
                      )
            ";

            return db.ExecuteQuery(query);
        }

        public bool Update_PM_OrderProcess_Qty()
        {
            query = $@"

                UPDATE PM_OrderProcess
                SET  
                      WorkQty     = WorkQty - {WorkQty}
                    , GoodQty     = GoodQty - {GoodQty}
                    , NgQty       = NgQty - {NgQty}

               WHERE CorpId    = '{CorpId}'
                    AND OrderNo   = '{OrderNo}'
                    AND OrderSeq  = {OrderSeq}
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Shot()
        {
            query = $@"
                    UPDATE PM_OrderResult
                    SET Shot = '{Shot}'
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND OrderNo = '{OrderNo}'
                    AND OrderSeq = '{OrderSeq}'

                ";

            return db.ExecuteQuery(query);
        }
        public bool Del_PM_OrderResult_ALL_OrderNoSeq()
        {
            query = $@"
                    --재고삭제
                    DECLARE @TB TABLE
                    (
                    	InOutNo nvarchar(20)
                    )
                    
                    INSERT @TB
                    SELECT InOutNo
                    FROM ST_StockD 
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND JobNo = '{OrderNo}'
                    AND JobSeq = '{OrderSeq}'
                    
                    DELETE ST_StockD WHERE InOutNo IN (SELECT InOutNo FROM @TB)
                    DELETE ST_StockM WHERE InOutNo IN (SELECT InOutNo FROM @TB);
                    
                    --불량삭제
                    DELETE FROM QM_WorkDefect
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND OrderNo = '{OrderNo}'
                    AND Seq = '{OrderSeq}'

                    --실적 삭제
                    DELETE FROM PM_OrderResult
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND OrderNo = '{OrderNo}'
                    AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

    }
}
