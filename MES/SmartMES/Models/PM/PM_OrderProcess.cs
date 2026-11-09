using DevExpress.CodeParser;
using DevExpress.Diagram.Core.Shapes;
using DevExpress.XtraRichEdit.Model;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Runtime.InteropServices.ComTypes;

namespace SmartMES.Models.PM
{
	class PM_OrderProcess
    {
        #region Property
        public string CorpId { get; set; }

        public string OrderNo { get; set; }

        public int OrderSeq { get; set; }

        public string OrderDate { get; set; }

        public string ItemCode { get; set; }

        public int RoutingSeq { get; set; }

        public string ProcessCode { get; set; }

        public string MachineCode { get; set; }

        public string LineCode { get; set; }

        public decimal OrderQty { get; set; }

        public decimal WorkQty { get; set; }

        public decimal GoodQty { get; set; }

        public decimal LossQty { get; set; }

        public decimal NgQty { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string DeliveryDate { get; set; }

        public string Status { get; set; }

        public string OrderStatus { get; set; }

        public int DisplayNumber { get; set; }
        public string UrgentYn { get; set; }

        public string Remark { get; set; }


        //조회용
        public string SearchStart { get; set; }
        public string SearchEnd { get; set; }


        #endregion Property

        Database db = new Database();
        string query = "";


        public DataTable Select_OrderPropcess(string startDate, string endDate)
        {
            query = $@"
                      SELECT '' AS Status
                            , op.OrderNo
                            , op.OrderSeq
                            , op.OrderDate
                            , op.ItemCode AS ItemCode
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
                            , op.RoutingSeq
                            , op.ProcessCode
                            , cp.ProcessName
                            , op.MachineCode
                            , mc.MachineName
                            , op.LineCode
                            , cl.LineName

                            , case when op.UrgentYn = 'Y' then '긴급'
	                            else '' end AS UrgentYn
                            , op.DisplayNumber

                            , op.OrderQty
                            , ISNULL(odr.WorkQty, 0) AS WorkQty
                            , ISNULL(odr.GoodQty, 0) AS GoodQty
                            , ISNULL(odr.NgQty, 0) AS NgQty
                            , op.StartDate
                            , op.EndDate
                            , op.Status AS OrderStatus
                            , op.Remark
                            , op.CreId
                            , op.CreIP
                            , op.CreDt
                            , op.ModId
                            , op.ModIP
                            , op.ModDt

                            , ISNULL(odr.WorkQty * 100.0 / NULLIF(op.OrderQty, 0), 0) AS PgRate
                            , ISNULL(odr.NgQty   * 100.0 / NULLIF(odr.WorkQty, 0), 0) AS NgRate
                        FROM PM_OrderProcess op
                        
                        LEFT JOIN (
                            SELECT 
                                  OrderNo
                                , OrderSeq
                                , SUM(WorkQty) AS WorkQty
                                , SUM(GoodQty) AS GoodQty
                                , SUM(NgQty)   AS NgQty
                            FROM PM_OrderResult
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            GROUP BY OrderNo, OrderSeq
                        ) odr ON odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq
                        
                        LEFT JOIN CM_Item cm ON cm.CorpId = op.CorpId AND cm.ItemCode = op.ItemCode
                        LEFT JOIN CM_Line     cl ON cl.CorpId = op.CorpId AND cl.LineCode     = op.LineCode
                        LEFT JOIN CM_Process  cp ON cp.CorpId = op.CorpId AND cp.ProcessCode  = op.ProcessCode
                        LEFT JOIN EQ_Machine mc ON mc.CorpId = op.CorpId AND mc.MachineCode = op.MachineCode

                        WHERE op.CorpId = '{WrGlobal.CorpID}'
                        AND op.OrderDate BETWEEN '{startDate}' AND '{endDate}'
            ";

            if (ItemCode.ConvertString_Null_To_Empty() != "" && ItemCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND op.ItemCode = '{ItemCode}'";
            }

            query += $"\nORDER BY CASE WHEN op.UrgentYn = 'Y' THEN 0 ELSE 1 END, ISNULL(op.DisplayNumber, 9999), op.OrderNo, op.OrderSeq ";

            return db.GetDataTable(query);
        }

        public DataTable Select_OrderPropcess_Indiv(string startDate, string endDate)
        {
            query = $@"
                        SELECT
                        '' AS Status
                        , op.OrderNo
                        , op.OrderSeq
                        , op.OrderDate

                        , op.ItemCode ItemCode
                        , cm.ItemName
                        , cm.Alias
                        , cm.Size
                        , cm.ItemClass1
                        , cm.ItemClass2
                        , cm.ItemClass3
                        , cm.UnitWeight
                        , cm.Texture
                        , cm.Type
                        , cm.UnitCode
                        , cm.ColorCode

                        , op.RoutingSeq
                        , op.ProcessCode
                        , cp.ProcessName
                        , op.MachineCode
                        , mc.MachineName
                        , op.LineCode
                        , cl.LineName
                        , op.OrderQty
                        , op.WorkQty
                        , op.GoodQty
                        , op.NgQty
                        , op.StartDate
                        , op.EndDate
                        , op.Status as SRStatus
                        , op.UrgentYn
                        , op.Remark
                        , op.CreId
                        , op.CreIP
                        , op.CreDt
                        , op.ModId
                        , op.ModIP
                        , op.ModDt

                        FROM PM_OrderProcess op
                        LEFT JOIN CM_Item cm ON op.CorpId = cm.CorpId AND op.ItemCode = cm.ItemCode
                        LEFT JOIN CM_Line cl ON op.CorpId = cl.CorpId AND op.LineCode = cl.LineCode
                        LEFT JOIN CM_Process cp ON op.CorpId = cp.CorpId AND op.ProcessCode = cp.ProcessCode
                        LEFT JOIN EQ_Machine mc ON op.CorpId = mc.CorpId AND op.MachineCode = mc.MachineCode
                        WHERE op.CorpId = '{WrGlobal.CorpID}'
                        AND op.OrderDate BETWEEN '{startDate}' AND '{endDate}'
            ";

            if (ItemCode.ConvertString_Null_To_Empty() != "" && ItemCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND op.ItemCode = '{ItemCode}'";
            }
            if (ProcessCode.ConvertString_Null_To_Empty() != "" && ProcessCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND op.ProcessCode = '{ProcessCode}'";
            }
            if (OrderNo.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND op.OrderNo = '{OrderNo}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_OrderPropcess_DOOIL(string startDate, string endDate)
        {
            query = $@"
                        SELECT
                        '' AS Status
                        , op.OrderNo
                        , op.OrderSeq
                        , op.OrderDate

                        , op.ItemCode ItemCode
                        , cm.ItemName
                        , cm.Alias
                        , cm.Size
                        , cm.ItemClass1
                        , cm.ItemClass2
                        , cm.ItemClass3
                        , cm.UnitWeight
                        , cm.Texture
                        , cm.Type
                        , cm.UnitCode
                        , cm.ColorCode

                        , op.RoutingSeq
                        , op.ProcessCode
                        , cp.ProcessName
                        , op.MachineCode
                        , mc.MachineName
                        , op.LineCode
                        , cl.LineName
                        , op.OrderQty
                        , op.WorkQty
                        , op.GoodQty
                        , op.NgQty
                        , op.FinalYn
                        , op.StartDate
                        , op.EndDate
                        , op.Status as SRStatus
                        , op.Remark
                        , op.CreId
                        , op.CreIP
                        , op.CreDt
                        , op.ModId
                        , op.ModIP
                        , op.ModDt

                        FROM PM_OrderProcess op
                        LEFT JOIN CM_Item cm ON op.CorpId = cm.CorpId AND op.ItemCode = cm.ItemCode
                        LEFT JOIN CM_Line cl ON op.CorpId = cl.CorpId AND op.LineCode = cl.LineCode
                        LEFT JOIN CM_Process cp ON op.CorpId = cp.CorpId AND op.ProcessCode = cp.ProcessCode
                        LEFT JOIN EQ_Machine mc ON op.CorpId = mc.CorpId AND op.MachineCode = mc.MachineCode
                        WHERE op.CorpId = '{WrGlobal.CorpID}'
                        AND op.OrderDate BETWEEN '{startDate}' AND '{endDate}'
                        AND op.ProcessCode NOT IN ('PR002') -- 포장 공정 제외
            ";

            if (ItemCode.ConvertString_Null_To_Empty() != "" && ItemCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND op.ItemCode = '{ItemCode}'";
            }
            if (ProcessCode.ConvertString_Null_To_Empty() != "" && ProcessCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND op.ProcessCode = '{ProcessCode}'";
            }
            if (OrderNo.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND op.OrderNo = '{OrderNo}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderProcess_TPG(string startDate, string endDate)
        {
            query = $@"
                        SELECT
                        '' AS Status
                        , op.OrderNo
                        , op.OrderSeq
                        , op.OrderDate

                        , op.ItemCode ItemCode
                        , cm.ItemName
                        , cm.Alias
                        , cm.Size
                        , cm.ItemClass1
                        , cm.ItemClass2
                        , cm.ItemClass3
                        , cm.UnitWeight
                        , cm.Texture
                        , cm.Type
                        , cm.UnitCode
                        , cm.ColorCode

                        , op.RoutingSeq
                        , op.ProcessCode
                        , cp.ProcessName
                        , op.MachineCode
                        , mc.MachineName
						, mc.ArrayValue
						, mc.RPM
						, mc.ProductionYield
                        , op.LineCode
                        , cl.LineName
                        , op.OrderQty
                        , op.WorkQty
                        , op.GoodQty
                        , op.NgQty
                        , op.FinalYn
                        , op.StartDate
                        , op.EndDate
                        , op.Status as SRStatus
                        , op.Remark
                        , op.UrgentYn
                        , op.CreId
                        , op.CreIP
                        , op.CreDt
                        , op.ModId
                        , op.ModIP
                        , op.ModDt
                        FROM PM_OrderProcess op
                        LEFT JOIN CM_Item cm ON op.CorpId = cm.CorpId AND op.ItemCode = cm.ItemCode
                        LEFT JOIN CM_Line cl ON op.CorpId = cl.CorpId AND op.LineCode = cl.LineCode
                        LEFT JOIN CM_Process cp ON op.CorpId = cp.CorpId AND op.ProcessCode = cp.ProcessCode
                        LEFT JOIN EQ_Machine mc ON op.CorpId = mc.CorpId AND op.MachineCode = mc.MachineCode
                        WHERE op.CorpId = '{WrGlobal.CorpID}'
                        AND op.OrderDate BETWEEN '{startDate}' AND '{endDate}'
            ";

            if (ItemCode.ConvertString_Null_To_Empty() != "" && ItemCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND op.ItemCode = '{ItemCode}'";
            }
            if (OrderNo.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND op.OrderNo = '{OrderNo}'";
            }

            return db.GetDataTable(query);
}

        public DataTable Select_PM_OrderProcess(string startDate, string endDate)
        {
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();
            MachineCode = MachineCode.ConvertStr_Empty_To_Null();
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();

            query = $@"
                     SELECT '' AS Status
					 		, OP.[Status] AS OrderStatus
			                , OP.OrderNo                            -- 지시번호
			                , OP.OrderSeq                           -- 지시순번
			                , OP.OrderDate                          -- 지시일자
			                , OP.ProcessCode						-- 공정코드
							, CP.ProcessName						-- 공정명
			                , OP.MachineCode						-- 설비코드
			                , OP.LineCode							-- 라인코드
							, OP.ItemCode AS ItemCode
                            , CM.ItemName ItemName
			                , OP.OrderQty
                            , rst.SerialNo 
                            , rst.WorkQty
			                , rst.GoodQty
			                , rst.NgQty
                            , ISNULL(rst.WorkDate, GETDATE()) AS WorkDate
                            , rst.WorkerID
			                , rst.StartDate
                            , rst.EndDate
                            , rst.SubSeq

	                FROM PM_OrderProcess OP
                    INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode
                    INNER JOIN CM_Item CM ON OP.ItemCode = cm.ItemCode
	                LEFT JOIN PM_OrderResult rst ON OP.CorpId = rst.CorpId AND OP.OrderNo = rst.OrderNo AND OP.OrderSeq = rst.OrderSeq
			        WHERE OP.CorpId = '{WrGlobal.CorpID}'
			        AND OP.OrderDate  BETWEEN '{startDate}' AND '{endDate}'
                    
";

            if (ProcessCode != "NULL")
            {
                query += $" AND OP.ProcessCode = {ProcessCode}";
            }
            if (MachineCode != "NULL")
            {
                query += $" AND OP.MachineCode = {MachineCode}";
            }
            if (ItemCode != "NULL")
            {
                query += $" AND OP.ItemCode = {ItemCode}";
            }

            query += "Order By OP.OrderNo, OP.OrderSeq, OP.OrderDate";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderProcess_N(string startDate, string endDate)
        {
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();
            MachineCode = MachineCode.ConvertStr_Empty_To_Null();
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();

            query = $@"
                    SELECT '' AS Status
			                , OP.OrderNo                            -- 지시번호
			                , OP.OrderSeq                           -- 지시순번
			                , OP.OrderDate                          -- 지시일자
			
			                , OP.ProcessCode						-- 공정코드
			                , OP.MachineCode						-- 설비코드
			                , OP.RoutingSeq							-- 라우팅순번
			                , OP.LineCode							-- 라인코드
			                , CP.ProcessName						-- 공정명
			
			                , WO.SdOrderNo							-- 수주번호
			                , WO.SdOrderSeq							-- 수주순번
                            , WO.CustCode							-- 거래처
                            , OP.Status AS WorkStatus               -- 작업상태

			                , OP.ItemCode ItemCode
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
                            , cm.HouseID AS OutHouseID
                            
                            , case when OP.UrgentYn = 'Y' then '긴급'
	                            else '' end AS UrgentYn
                            , OP.DisplayNumber

			                , OP.OrderQty
                            , ISNULL(ORS.TotalQty, 0) AS TotalWorkQty  
			                , 0 WorkQty
			                , 0 GoodQty
			                , 0 NgQty

                            , CONVERT(varchar(10), GETDATE(), 120) AS WorkDate

                            , '' AS WorkerID
					        , GETDATE() AS StartDate
					        , NULL AS EndDate   
							, 0 AS SubSeq
                            , '' AS SerialNo
                            , '' AS LotNo
			                , OP.[Status]
			                , isnull(SD.FinalYn,'N') as FinalYn		-- 최종공정

	                FROM PM_OrderProcess OP
	                INNER JOIN PM_ProductPlan WO ON WO.OrderNo = OP.OrderNo
	                INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode
	                INNER JOIN CM_Item CM ON OP.ItemCode = cm.ItemCode 
	                INNER JOIN PM_SdRoute SD ON OP.OrderNo = SD.OrderNo AND OP.RoutingSeq = SD.RoutingSeq
                     LEFT JOIN (
                        SELECT 
                            OrderNo,
                            OrderSeq,
                            SUM(ISNULL(GoodQty, 0) + ISNULL(NgQty, 0)) AS TotalQty
                        FROM PM_OrderResult
                        GROUP BY OrderNo, OrderSeq
                    ) ORS ON OP.OrderNo = ORS.OrderNo AND OP.OrderSeq = ORS.OrderSeq

			        WHERE OP.CorpId = '{WrGlobal.CorpID}'
			        AND OP.OrderDate  BETWEEN '{startDate}' AND '{endDate}'
                ";

            if (ProcessCode != "NULL")
            {
                query += $" AND OP.ProcessCode = {ProcessCode}";
            }
            if (MachineCode != "NULL")
            {
                query += $" AND OP.MachineCode = {MachineCode}";
            }
            if (ItemCode != "NULL")
            {
                query += $" AND OP.ItemCode = {ItemCode}";
            }

            query += $"\nORDER BY CASE WHEN OP.UrgentYn = 'Y' THEN 0 ELSE 1 END, ISNULL(OP.DisplayNumber, 9999), OP.OrderNo, OP.OrderSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_OrderList_ByOrderNo()
        {
            query = $@"SELECT OP.OrderNo
                            , OP.OrderSeq
		                    , OP.StartDate
		                    , OP.EndDate
		                    , OP.ProcessCode
		                    , SR.ItemCode
		                    , SR.ItemCode
		                    , CM.ItemName
		                    , ISNULL(OP.OrderQty, 0) AS OrderQty
		                    , ISNULL(OP.WorkQty, 0) AS WorkQty
		                    , ISNULL(OP.GoodQty, 0) AS GoodQty
		                    , ISNULL(OP.NgQty, 0) AS NgQty
		                    , OP.Status
                            , OP.StartDate
                            , OP.EndDate
                    FROM PM_OrderProcess OP
                    LEFT JOIN PM_ProductPlan WO ON OP.OrderNo = WO.OrderNo
                    LEFT JOIN PM_SdRoute SR ON SR.OrderNo = OP.OrderNo AND SR.RoutingSeq = OP.RoutingSeq
                    LEFT JOIN CM_Item CM ON SR.ItemCode = cm.ItemCode
                    WHERE OP.CorpID = '{WrGlobal.CorpID}'
                    AND OP.OrderNo = '{OrderNo}'
                ";

            query += $"\n ORDER BY OP.OrderNo DESC, OP.OrderSeq DESC";

            return db.GetDataTable(query);
        }

        public DataTable SelectOrderProcess_QM_WorkDefect(string startDate, string endDate)
		{
            query = $@"SELECT 
                                OP.ProcessCode
                                , OP.LineCode
                                , CP.ProcessName
		                        , CL.LineName
		                        , OP.OrderNo
		                        , OP.OrderSeq

		                        , OP.ItemCode 
		                        , CM.ItemName
		                        , CM.Alias
		                        , CM.Size
		                        , CM.ItemClass1
		                        , CM.ItemClass2
		                        , CM.ItemClass3

		                        , ISNULL(OP.OrderQty, 0) OrderQty
		                        , ISNULL(OP.WorkQty, 0) WorkQty
		                        , ISNULL(OP.GoodQty, 0) GoodQty
		                        , ISNULL(OP.NgQty, 0) NgQty
		                        , OP.[Status]
		                        , OP.StartDate
		                        , OP.EndDate

                        FROM PM_OrderProcess OP
                        INNER JOIN PM_ProductPlan WO ON WO.OrderNo = OP.OrderNo
                        INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode AND CP.WorkType = '10' -- 10:사내, 20:외주
                        LEFT JOIN CM_Line CL ON OP.LineCode = CL.LineCode
                        INNER JOIN CM_Item CM ON OP.ItemCode = cm.ItemCode 
                        INNER JOIN CM_Item CM2 ON WO.ItemCode = CM2.ItemCode
                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
                        AND WO.WorkDate BETWEEN '{startDate}' AND '{endDate}'
                        AND OP.Status <> '대기'
						AND OP.RoutingSeq = (
							SELECT MAX(RoutingSeq) 
							FROM PM_OrderProcess OP2 
							WHERE OP2.OrderNo = OP.OrderNo
							AND OP2.CorpId = OP.CorpId
						)
                        ";

            if (ItemCode != "")
            {
                query += $"\nAND OP.ItemCode = '{ItemCode}' ";
            }

            if (ProcessCode != "")
            {
                query += $"\nAND OP.ProcessCode = '{ProcessCode}' ";
            }

            query += $"\nORDER BY OP.OrderNO , OP.OrderSeq";

            return db.GetDataTable(query);
        }

        public bool ExistsOP_ByOrderNo(string orderNo)
        {
            query = $@"
                       SELECT 1
                        FROM PM_OrderProcess
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{orderNo}'

                        ";

            DataRow dr = db.GetDataRow(query);

            bool result = false;

            if(dr != null)
			{
                result = dr[0].ConvertString_Null_To_Empty() == "1";
			}

            return result;
        }

        public bool Check_PreviousRoutingWorkQty()
        {
            query = $@"
                        SELECT TOP 1
                               CASE WHEN SUM(ISNULL(OP.WorkQty, 0)) > 0 THEN 1 ELSE 0 END AS Result
                        FROM PM_SdRoute SR
                        LEFT JOIN PM_OrderProcess OP ON SR.CorpId = OP.CorpId
                                                    AND SR.OrderNo = OP.OrderNo
                                                    AND SR.RoutingSeq = OP.RoutingSeq
                        WHERE SR.CorpId = '{WrGlobal.CorpID}'
                        AND SR.OrderNo = '{OrderNo}'
                        AND SR.WorkType = '10'
                        AND SR.RoutingSeq < {RoutingSeq}
                        GROUP BY SR.RoutingSeq
                        ORDER BY SR.RoutingSeq DESC
                        ";

            DataRow dr = db.GetDataRow(query);

            if (dr == null)
            {
                return true;
            }

            return dr["Result"].ConvertInt_Null_To_Zero() == 1;
        }

        public bool Ins_PM_OrderProcess_GS()
        {
            query = $@"
                        INSERT INTO PM_OrderProcess
		                (
			                CorpId,
			                OrderNo,
			                OrderSeq,
			                ItemCode,
			                RoutingSeq,
			                PrevRoutingSeq,
			                NextRoutingSeq,
			                ProcessCode,
			                PrevProcessCode,
			                NextProcessCode,
			                LineCode,
			                OutYn,
			                OrderQty,
			                WorkQty,
			                GoodQty,
			                NgQty,
			                StartYn,
			                FinalYn,
			                RequireYn,
			                StartDate,
			                EndDate,
			                Status,
			                CreId,
			                CreIP,
			                CreDt
		                )
		                SELECT
			                '{WrGlobal.CorpID}',--소속사업장
			                '{OrderNo}',--작업지시번호
			                ROW_NUMBER() OVER(ORDER BY R.RoutingSeq) ,--순번
			                R.ItemCode as ItemCode,--반제품
			                R.RoutingSeq,--라우팅순번
			                P.RoutingSeq as PrevRoutingSeq,--전공정 라우팅순번
			                N.RoutingSeq as NextRoutingSeq,--후공정 라우팅순번
			                R.ProcessCode,--공정코드
			                P.ProcessCode as PrevProcessCode,--전공정코드
			                N.ProcessCode as NextProcessCode,--후공정코드
			                null as LineCode,--라인코드
			                null as OutYn,--배출여부
			                R.PlanQty * R.WorkQty as OrderQty,--작업지시수량
			                0 as WorkQty,--생산량
			                0 as GoodQty,--양품수량
			                0 as NgQty,--부적합수량
			                case when P.RoutingSeq is null then 'Y' else 'N' end as StartYn,--첫공정여부
			                case when N.RoutingSeq is null then 'Y' else 'N' end as FinalYn,--마지막공정여부
			                null as RequireYn,--필수공정여부
			                null as StartDate,--시작일시
			                null as EndDate,--종료일시
			                '작업대기' as Status,--상태
			                '{WrGlobal.LoginID}',
			                HOST_NAME(),
			                GETDATE()
			                FROM PM_SdRoute R
			                LEFT JOIN PM_SdRoute P ON R.RoutingSeq = P.RoutingSeq + 1 AND R.OrderNo = P.OrderNo
			                LEFT JOIN PM_SdRoute N ON R.RoutingSeq = N.RoutingSeq - 1 AND R.OrderNo = N.OrderNo
			                WHERE R.CorpId = '{WrGlobal.CorpID}'
                            AND R.WorkType = '10' --사내만
			                AND R.OrderNo = '{OrderNo}';
                
                        -- PM_SdRoute 상태 변경
                        UPDATE PM_SdRoute SET Status = '진행' WHERE CorpID = '{WrGlobal.CorpID}' AND OrderNo = '{OrderNo}' AND ProcessType = 'P';
                        
                        -- PM_ProductPlan 상태 변경
		                UPDATE PM_ProductPlan SET Status = 'R' WHERE CorpID = '{WrGlobal.CorpID}' AND OrderNo = '{OrderNo}';
                        ";
            return db.ExecuteQuery(query);
        }

        public int Get_OrderSeq()
        {
            query = $@" SELECT TOP 1 OrderSeq
                        FROM PM_OrderProcess 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        ORDER BY OrderSeq DESC";

            DataTable dt = db.GetDataTable(query);

            int maxSeq = 0;

            if (dt.Rows.Count > 0)
            {
                maxSeq = dt.Rows[0]["OrderSeq"].ConvertInt_Null_To_Zero();
            }

            return maxSeq;
        }
        public bool Insert_PM_OrderProcess()
        {
            query = $@"INSERT INTO PM_OrderProcess(CorpId,OrderNo,OrderSeq,ItemCode,
                                                                            RoutingSeq,ProcessCode,MachineCode,
                                                                            LineCode,OrderQty,WorkQty,GoodQty,NgQty,
                                                                            StartDate,EndDate,DeliveryDate,Status,Remark,OrderDate
                                                                            ,CreId,CreIP,CreDt
                                                                            , UrgentYn
                                                                                )
                                VALUES(
                                                '{WrGlobal.CorpID}'
                                                ,'{OrderNo}'
                                                ,'{OrderSeq.ConvertInt_Null_To_Zero()}'
                                                ,'{ItemCode}'
                                                ,'{RoutingSeq.ConvertInt_Null_To_Zero()}'
                                                ,'{ProcessCode}'
                                                ,'{MachineCode}'
                                                ,'{LineCode}'
                                                ,'{OrderQty.ConvertDecimal_Null_To_Zero()}'
                                                ,'{WorkQty.ConvertDecimal_Null_To_Zero()}'
                                                ,'{GoodQty.ConvertDecimal_Null_To_Zero()}'
                                                ,'{NgQty.ConvertDecimal_Null_To_Zero()}'
                                                ,'{StartDate}'
                                                ,'{EndDate}'
                                                ,'{DeliveryDate}'
                                                ,'{Status}'
                                                ,'{Remark}'
                                                ,{OrderDate.ConvertStr_Empty_To_Null()}
                                                ,'{WrGlobal.LoginID}'
                                                ,'{WrGlobal.ClientHostName}'
                                                ,GETDATE()

                                                ,'{UrgentYn}'
                                                )
                                                ";
            return db.ExecuteQuery(query);
        }

        public bool Ins_PM_OrderProcessAuto_PM_SdRoute()
		{
            query = $@"
        DECLARE @pCorpId nvarchar(20) = '{WrGlobal.CorpID}'
        , @pOrderNo nvarchar(20) = '{OrderNo}'
        , @pQty float = '{OrderQty}'
        
        	;with T_PM_SdRoute as  
		(
			select 
			distinct
			ROW_NUMBER() over(order by A.RoutingSeq asc) as  OrderSeq,
			A.*,
			case when M.OrderNo is null then 'N' else 'Y' end as InYn
			from PM_SdRoute A
			left join (select OrderNo,ParentID from PM_SdRoute group by OrderNo,ParentID) M on A.OrderNo = M.OrderNo and A.ChildID = M.ParentID
			where A.CorpId = @pCorpId
			and A.OrderNo = @pOrderNo
			and A.ProcessType = 'P'
		)
		insert into PM_OrderProcess
		(
			CorpId,
			OrderNo,
			OrderSeq,
			OrderDate,
			ItemCode,
			RoutingSeq,
			PrevRoutingSeq,
			NextRoutingSeq,
			ProcessCode,
			PrevProcessCode,
			NextProcessCode,
			LineCode,
			MachineCode,
			InYn,
			OutYn,
			OrderQty,
			WorkQty,
			GoodQty,
			NgQty,
			StartYn,
			FinalYn,
			RequireYn,
			StartDate,
			EndDate,
			Status,
			CreId,
			CreIP,
			CreDt
		)
		select 
		R.CorpId,--소속사업장
		R.OrderNo,--작업지시번호
		R.OrderSeq,--순번
		'{OrderDate}',--지시일자
		R.MItemCode as ItemCode,--반제품
		R.RoutingSeq,--라우팅순번
		P.RoutingSeq as PrevRoutingSeq,--전공정 라우팅순번
		N.RoutingSeq as NextRoutingSeq,--후공정 라우팅순번
		R.ProcessCode,--공정코드
		P.ProcessCode as PrevProcessCode,--전공정코드
		N.ProcessCode as NextProcessCode,--후공정코드
		'{LineCode}' as LineCode,--라인코드
		'{MachineCode}' as LineCode,--라인코드
		R.InYn,--투입여부
		null as OutYn,--배출여부
		@pQty as OrderQty,--작업지시수량
		--R.PlanQty * R.WorkQty as OrderQty,--작업지시수량
		0 as WorkQty,--생산량
		0 as GoodQty,--양품수량
		0 as NgQty,--부적합수량
		case when P.RoutingSeq is null then 'Y' else 'N' end as StartYn,--첫공정여부
		case when N.RoutingSeq is null then 'Y' else 'N' end as FinalYn,--마지막공정여부
		null as RequireYn,--필수공정여부
		null as StartDate,--시작일시
		null as EndDate,--종료일시
		'작업대기' as Status,--상태
		'{WrGlobal.LoginID}',
		host_name(),
		getdate()
		from T_PM_SdRoute R
		left join T_PM_SdRoute P on R.OrderSeq = P.OrderSeq + 1
		left join T_PM_SdRoute N on R.OrderSeq = N.OrderSeq - 1
		where R.WorkType = '10' 

		update PM_SdRoute 
		set 
		Status = '진행',
		PlanQty = @pQty
		where OrderNo = @pOrderNo and  ProcessType = 'P'

		update PM_ProductPlan set Status = 'R' where OrderNo = @pOrderNo;

        ";
            return db.ExecuteQuery(query);
		}

        public bool Upd_PM_OrderProcess()
        {
            query = $@" 
                        UPDATE PM_OrderProcess
                        SET 
                          ProcessCode       = {ProcessCode.ConvertStr_Empty_To_Null()}
                        , MachineCode       = {MachineCode.ConvertStr_Empty_To_Null()}
                        , LineCode          = {LineCode.ConvertStr_Empty_To_Null()}
                        , OrderDate         = {OrderDate.ConvertStr_Empty_To_Null()}
                        , OrderQty          = {OrderQty.ConvertDecimal_Null_To_Zero()}
                        , Remark            = {Remark.ConvertStr_Empty_To_Null()}
                        , ModId             = '{WrGlobal.LoginID}'
                        , ModIP             = '{WrGlobal.ClientIpAddress}'
                        , ModDt             = GETDATE()

                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Update_PM_OrderProcess_Indiv()
        {
            query = $@"
                        UPDATE PM_OrderProcess
                        SET 
                          ProcessCode       = {ProcessCode.ConvertStr_Empty_To_Null()}
                        , ItemCode  = {ItemCode.ConvertStr_Empty_To_Null()}
                        , MachineCode       = {MachineCode.ConvertStr_Empty_To_Null()}
                        , LineCode          = {LineCode.ConvertStr_Empty_To_Null()}
                        , OrderDate         = {OrderDate.ConvertStr_Empty_To_Null()}
                        , OrderQty          = {OrderQty.ConvertDecimal_Null_To_Zero()}
                        , Remark            = {Remark.ConvertStr_Empty_To_Null()}
                        , ModId             = '{WrGlobal.LoginID}'
                        , ModIP             = '{WrGlobal.ClientIpAddress}'
                        , ModDt             = GETDATE()

                        , UrgentYn         = '{UrgentYn}'
                        , DisplayNumber     = {DisplayNumber.ConvertInt_Empty_To_Null()}
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public void Upd_Qty_OrderNoSeq()
        {
            query = $@"
                     
                        UPDATE PM_OrderProcess
                        SET WorkQty = B.WorkQty
                            , GoodQty = B.GoodQty
                            , NgQty = B.NgQty
                        FROM PM_OrderProcess A, 
                        (
                            SELECT SUM(WorkQty) WorkQty
                            , SUM(GoodQty) GoodQty
                            , SUM(NgQty) NgQty
                            FROM PM_OrderResult
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND OrderNo = '{OrderNo}'
                            AND OrderSeq = '{OrderSeq}'
                            ) B
                        WHERE A.CorpId = '{WrGlobal.CorpID}'
                        AND A.OrderNo  = '{OrderNo}'
                        AND A.OrderSeq = '{OrderSeq}'
                    ";

            db.ExecuteQuery(query);
        }

        public bool Del_PM_OrderProcess()
        {
            query = $@" DELETE PM_OrderProcess
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_ByOrderNo()
        {
            query = $@" DELETE PM_OrderProcess
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
