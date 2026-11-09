using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.PM
{
    class PM_OrderProcess
    {
        #region Property
        public string CorpId { get; set; }

        public string OrderNo { get; set; }

        public int OrderSeq { get; set; }

        public string HalfMaterialCode { get; set; }

        public int? RoutingSeq { get; set; }

        public string ProcessCode { get; set; }

        public string NextProcessCode { get; set; }

        public string MachineCode { get; set; }

        public string LineCode { get; set; }

        public string InYn { get; set; }

        public string OutYn { get; set; }

        public int? OrderQty { get; set; }

        public int? WorkQty { get; set; }

        public int? GoodQty { get; set; }

        public int? LossQty { get; set; }
        public int? NgQty { get; set; }

        public string FinalYn { get; set; }

        public string RequireYn { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Status { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        public int DisplayNumber { get; set; }

        public string Remark { get; set; }
        public string machineCode { get; set; }
        #endregion Property

        Database db = new Database();
        string query = "";

        public DataTable Select_OrderProcess_Result(string startDt, string endDt)
        {
            //실적 - 지시조회 화면에서 사용(지시:실적 1:N)
            ProcessCode = ProcessCode.ConvertString_Null_To_Empty();
            MachineCode = MachineCode.ConvertString_Null_To_Empty();
            HalfMaterialCode = HalfMaterialCode.ConvertString_Null_To_Empty();
            Status = Status.ConvertString_Null_To_Empty();

            query = $@"
                     SELECT   OP.OrderNo                            -- 지시번호
			                , OP.OrderSeq                           -- 지시순번
			                , OP.OrderDate                          -- 지시일자
			
			                , OP.ProcessCode						-- 공정코드
			                , OP.MachineCode						-- 설비코드
			                , co.MachineName						-- 설비명
			                , OP.RoutingSeq							-- 라우팅순번
			                , OP.LineCode							-- 라인코드
			                , CP.ProcessName						-- 공정명
                            , CL.LineName                           -- 라인명
			
			                , WO.SdOrderNo							-- 수주번호
			                , WO.SdOrderSeq							-- 수주순번
                            , WO.CustCode							-- 거래처

			                , OP.ItemCode
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
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A011', cm.UnitCode)) AS UnitName
                            , cm.ColorCode
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A01210', cm.ItemClass1)) AS ClassName1	--품목분류1
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A01220', cm.ItemClass2)) AS ClassName2	--품목분류2
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A01230', cm.ItemClass3)) AS ClassName3	--품목분류3
			                , OP.OrderQty
			                , 0 AS WorkQty
			                , 0 AS GoodQty
			                , 0 AS NgQty
			                , OP.Remark OPRemark

                            , '' AS WorkDate
                            , '' AS WorkerID
                            , '' AS LotNo
			                , '' AS StartDate
                            , '' AS EndDate
                            , NULL AS SubSeq
                            , '' AS SerialNo
			                , OP.[Status]

                            --실적
                            , ISNULL(ODR.WorkQty, 0) AS TotalWorkQty

	                FROM PM_OrderProcess OP
	                LEFT JOIN PM_ProductPlan     WO ON OP.CorpId = WO.CorpId AND WO.OrderNo = OP.OrderNo
	                LEFT JOIN 
                    (
                        SELECT CorpId, OrderNo, OrderSeq, SUM(GoodQty) GoodQty, SUM(NgQty) NgQty, SUM(WorkQty) WorkQty
                        FROM PM_OrderResult   
                        GROUP BY CorpId, OrderNo, OrderSeq
                    ) ODR ON OP.CorpId = ODR.CorpId AND OP.OrderNo = ODR.OrderNo AND OP.OrderSeq = ODR.OrderSeq
	                LEFT JOIN CM_Process     CP ON OP.CorpId = CP.CorpId AND OP.ProcessCode = CP.ProcessCode
	                LEFT JOIN CM_Item    CM ON OP.CorpId = CM.CorpId AND OP.ItemCode = CM.ItemCode
	                LEFT JOIN EQ_Machine    Co ON OP.CorpId = Co.CorpId AND OP.MachineCode = co.MachineCode
                    LEFT JOIN CM_Line CL ON OP.CorpId = CL.CorpId AND OP.LineCode = CL.LineCode

			        WHERE OP.CorpId = '{WrGlobal.CorpID}'
			        AND OP.OrderDate  BETWEEN '{startDt}' AND '{endDt} 23:59'
                    ";

            if (ProcessCode != "" && ProcessCode.ToUpper() != "ALL")
            {
                query += $" AND OP.ProcessCode = '{ProcessCode}'";
            }
            if (MachineCode != "" && MachineCode.ToUpper() != "ALL")
            {
                query += $" AND OP.MachineCode = '{MachineCode}'";
            }
            if (HalfMaterialCode != "" && HalfMaterialCode.ToUpper() != "ALL")
            {
                query += $" AND OP.ItemCode = '{HalfMaterialCode}'";
            }

            if (Status != "" && Status.ToUpper() != "ALL")
            {
                query += $" AND OP.Status = '{Status}'";
            }

            query += " ORDER BY ISNULL(OP.DisplayNumber, 9999), OP.OrderNo, OP.OrderSeq ";

            return db.GetDataTable(query);
        }

        public DataTable Select_ProcessingList()
        {
            //if (HalfMaterialCode.ToUpper() == "ALL") HalfMaterialCode = "";

            query = $@"
                        SELECT B.OrderNo
                            , B.OrderSeq
	                        , B.MaterialCode
                            , B.ProcessCode
	                        , B.OrderQty
	                        , B.WorkQty
                            , COM.MachineCode
	                        , COM.MachineName
	                        , CM.MaterialName
                        FROM EQ_Machine COM
                        LEFT JOIN (
	                        SELECT
		                        A.OrderNo
		                        , A.OrderSeq
		                        , A.MaterialCode
		                        , A.ProcessCode
		                        , A.MachineCode
		                        , A.OrderQty
		                        , A.WorkQty
	                        FROM
	                        (
		                        SELECT 
			                        OP.OrderNo
			                        , OP.OrderSeq
			                        , OP.HalfMaterialCode AS MaterialCode
			                        , OP.ProcessCode
			                        , OP.MachineCode
			                        , OP.OrderQty
			                        , OP.WorkQty
			                        , ROW_NUMBER() OVER (PARTITION BY OP.MachineCode ORDER BY OP.OrderNo DESC, OP.OrderSeq DESC) AS rowNum
		                        FROM PM_OrderProcess OP
		                        INNER JOIN PM_SdRoute SR ON OP.OrderNo = SR.OrderNo AND OP.ProcessCode = SR.ProcessCode AND SR.ProcessKind = '10'  -- 가공만
		                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
		                        AND OP.Status IN ('시작', '작업중')
	                        ) AS A
	                        WHERE A.rowNum = 1
                        ) AS B ON COM.MachineCode = B.MachineCode
                        LEFT JOIN CM_Item CM ON B.MaterialCode = CM.MaterialCode
                    ";

            //if (HalfMaterialCode != "") query += $"\n AND A.MaterialCode = '{HalfMaterialCode}'";

            return db.GetDataTable(query);
        }

        public DataTable Select_WaitList()
        {
            //if (HalfMaterialCode.ToUpper() == "ALL") HalfMaterialCode = "";

            query = $@"SELECT *
                        FROM (
                        SELECT   '진행' State 
                                , OP.OrderNo
		                        , OP.OrderSeq
		                        , OP.HalfMaterialCode MaterialCode
                                , CM.MaterialName
		                        , OP.OrderQty
		                        , OP.WorkQty
		                        , iif(OP.StartDate = '1900-01-01', null, OP.StartDate) StartDate
		                        , iif(OP.EndDate = '1900-01-01', null, OP.EndDate) EndDate
                                , OP.DisplayNumber
                                , OP.OrderStatus
                        FROM PM_OrderProcess OP
                        INNER JOIN PM_SdRoute SR ON OP.OrderNo = SR.OrderNo AND OP.ProcessCode = SR.ProcessCode AND SR.ProcessKind = '10'  -- 가공만
                        INNER JOIN CM_Item CM ON OP.HalfMaterialCode = CM.MaterialCode
                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
                        AND OP.Status IN ('시작', '작업중')
                        AND OP.MachineCode = '{MachineCode}'
            

                        UNION ALL

                        SELECT '' State
                                , OP.OrderNo
		                        , OP.OrderSeq
		                        , OP.HalfMaterialCode MaterialCode
                                , CM.MaterialName
		                        , OP.OrderQty
		                        , OP.WorkQty
		                        , iif(OP.StartDate = '1900-01-01', null, OP.StartDate) StartDate
		                        , iif(OP.EndDate = '1900-01-01', null, OP.EndDate) EndDate
                                , OP.DisplayNumber
                                , OP.OrderStatus
                        FROM PM_OrderProcess OP
                        INNER JOIN CM_Item CM ON OP.HalfMaterialCode = CM.MaterialCode
                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
                        AND OP.Status = '대기'
                        AND OP.MachineCode = '{MachineCode}'
                        ) AS A
                        WHERE 1=1
                        ";

            //if (HalfMaterialCode != "") query += $"\n AND A.MaterialCode = '{HalfMaterialCode}'";

            query += $"\n ORDER BY CASE WHEN State = '진행' THEN 0 ELSE 1 END, DisplayNumber";

            return db.GetDataTable(query);
        }

        public void Insert_PM_OrderProcess()
        {
            query = $@"INSERT INTO PM_OrderProcess(CorpId,OrderNo,OrderSeq,HalfMaterialCode,
                                                                            RoutingSeq,ProcessCode,NextProcessCode,MachineCode,
                                                                            LineCode,InYn,OutYn,OrderQty,WorkQty,GoodQty,NgQty,
                                                                            FinalYn,RequireYn,StartDate,EndDate,Status,DisplayNumber,
                                                                            CreId,CreIP,CreDt)
                                VALUES(
                                                '{WrGlobal.CorpID}'
                                                ,'{OrderNo}'
                                                ,'{OrderSeq.ConvertInt_Null_To_Zero()}'
                                                ,'{HalfMaterialCode}'
                                                ,'{RoutingSeq.ConvertInt_Null_To_Zero()}'
                                                ,'{ProcessCode}'
                                                ,'{NextProcessCode}'
                                                ,'{MachineCode}'
                                                ,'{LineCode}'
                                                ,'{InYn}'
                                                ,'{OutYn}'
                                                ,'{OrderQty.ConvertInt_Null_To_Zero()}'
                                                ,'{WorkQty.ConvertInt_Null_To_Zero()}'
                                                ,'{GoodQty.ConvertInt_Null_To_Zero()}'
                                                ,'{NgQty.ConvertInt_Null_To_Zero()}'
                                                ,'{FinalYn}'
                                                ,'{RequireYn}'
                                                ,'{StartDate}'
                                                ,'{EndDate}'
                                                ,'{Status}'
                                                ,'{DisplayNumber.ConvertInt_Null_To_Zero()}'
                                                ,'{WrGlobal.LoginID}'
                                                ,'{WrGlobal.ClientHostName}'
                                                ,GETDATE())";
            db.ExecuteQuery(query);
        }

        public bool Order_Start()
        {
            query = $@"UPDATE PM_OrderProcess
                        SET StartDate = GETDATE()
                            , Status = '시작'
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Order_End()
        {
            query = $@"UPDATE PM_OrderProcess
                        SET EndDate = GETDATE()
                            , Status = '종료'
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Status()
        {
            query = $@"UPDATE PM_OrderProcess
                        SET Status = '{Status}'
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_OrderDisNum()
        {
            query = $@"UPDATE PM_OrderProcess
                        SET DisplayNumber = {DisplayNumber}
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public DataTable Select_PM_OrderProcess_ByNoSeq()
        {
            query = $@"SELECT CL.LineName
		                        , OP.OrderNo
                                , OP.OrderSeq
		                        , OP.HalfMaterialCode MaterialCode
		                        , CM.MaterialName
                                , CMM.MachineCode
		                        , OP.OrderQty
		                        , iif(OP.StartDate = '1900-01-01', null, OP.StartDate) StartDate
		                        , iif(OP.EndDate = '1900-01-01', null, OP.EndDate) EndDate
                                , OP.Status
                                , MC.DgpCode
                                , MC2.DgpCode OperationDgpCode
                                , MC.DgpName MachineName
                        FROM PM_OrderProcess OP
                        LEFT JOIN CM_Item CM ON OP.HalfMaterialCode = CM.MaterialCode
                        LEFT JOIN EQ_Machine CMM ON OP.MachineCode = CMM.MachineCode
                        LEFT JOIN CM_Line CL ON OP.LineCode= CL.LineCode
                        LEFT JOIN IF_DgpCode MC ON OP.MachineCode = MC.Equip_Name AND MC.Gauge_Type = '카운터'
                        LEFT JOIN IF_DgpCode MC2 ON OP.MachineCode = MC2.Equip_Name AND MC2.Gauge_Type = '가동여부'
                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
                        AND OP.OrderNo = '{OrderNo}'
                        AND OP.OrderSeq  = {OrderSeq}
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_InsMaterialInfo()
        {
            query = $@"
                        SELECT OrderNo
		                        , OrderSeq
                                , InMaterialCode
		                        , InQty
		                        , WorkQty
		                        , LossQty
		                        , NgQty
		                        , UseQty	-- 소요수량
		                        --, InQty - UseQty AS BalanceQty	-- 투입자재 잔량
                                , InQty - UseQty BalanceQty	-- 투입자재 잔량
                                , Remark
                        FROM(
	                        SELECT OP.OrderNo
			                        , OP.OrderSeq
                                    , SR2.BomMaterialCode AS InMaterialCode			-- 필요자재
			                        , ISNULL(A.Qty, 0)	InQty						-- 투입수량
			                        , ISNULL(OP.WorkQty, 0) WorkQty		-- 생산량
			                        , ISNULL(OP.LossQty, 0) LossQty							-- 헛가공수량
			                        , ISNULL(OP.NgQty, 0) NgQty
			                        , ISNULL(SR.OrderQty, 0) OrderQty1 
			                        , ISNULL(SR2.OrderQty, 0) OrderQty2			-- 지시량
			                        , (ISNULL(SR2.OrderQty, 0) * ISNULL(OP.WorkQty, 0) / SR.OrderQty) UseQty
                                    , OP.Remark
	                        FROM PM_OrderProcess OP
	                        LEFT JOIN PM_SdRoute SR ON OP.OrderNo = SR.OrderNo AND OP.ProcessCode = SR.ProcessCode
	                        LEFT JOIN PM_SdRoute SR2 ON SR.OrderNo = SR2.OrderNo AND SR.ChildID = SR2.ParentID 
	                        LEFT JOIN (
		                        SELECT JobNo OrderNo
				                        , JobSeq OrderSeq	
				                        , MaterialCode
				                        , SUM(Qty) Qty
		                        FROM ST_StockD 
		                        WHERE [Type] = '투입'
		                        GROUP BY JobNo, JobSeq, MaterialCode
	                        ) AS A ON OP.OrderNo = A.OrderNo AND OP.OrderSeq = A.OrderSeq AND A.MaterialCode = SR2.BomMaterialCode
                            LEFT JOIN CM_Item CM ON SR2.BomMaterialCode = CM.MaterialCode
	                        WHERE OP.CorpId = '{WrGlobal.CorpID}'
	                        AND OP.OrderNo = '{OrderNo}'
	                        AND OP.OrderSeq = {OrderSeq}
                            AND CM.MaterialType != '70'     --배출자재 제외
                        ) AS C
                        ";

            return db.GetDataTable(query);
        }

        public bool Upd_Remark()
        {
            query = $@"
                        UPDATE PM_OrderProcess
                        SET Remark = '{Remark}'
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public void Upd_Qty()
        {
            query = $@"
                        UPDATE PM_OrderProcess SET
                            WorkQty = {WorkQty} + ISNULL(LossQty, 0)
                            , GoodQty = '{GoodQty}'
                            , NgQty = '{NgQty}'
                        WHERE OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            db.ExecuteQuery(query);
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

        public bool Upd_LossQty()
        {
            query = $@"
                        UPDATE PM_OrderProcess
                        SET LossQty = {LossQty}
                            , WorkQty = {LossQty} + WorkQty
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_OrderProcess_Result()
        {
            query = $@"
                        UPDATE PM_OrderProcess
                        SET  WorkQty = isnull(B.GoodQty,0) + isnull(B.NgQty,0)
		                        , GoodQty = isnull(B.GoodQty, 0)
		                        , NgQty = isnull(B.NgQty,0)
		                        , Status = CASE	
						                        WHEN A.OrderQty <= B.GoodQty THEN '작업완료'
						                        ELSE '작업중'
					                        END
		                        , StartDate = B.StartDate
		                        , EndDate = B.EndDate
                        FROM PM_OrderProcess A, 
                        (
	                        SELECT SUM(GoodQty) AS GoodQty, SUM(NgQty) AS NgQty, MIN(StartDate) StartDate, MAX(EndDate) EndDate
	                        FROM PM_OrderResult
	                        WHERE CorpId    = '{CorpId}'
	                        AND OrderNo   = '{OrderNo}'
	                        AND OrderSeq  = {OrderSeq}
                        ) B
                        WHERE A.CorpId = '{CorpId}'
                        AND A.OrderNo = '{OrderNo}'
                        AND A.OrderSeq = '{OrderSeq}'
                        ";

            return db.ExecuteQuery(query);
        }

        public DataTable Chk_Status()
        {
            query = $@"
                        SELECT
	                        Status
                        FROM PM_OrderProcess
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_OrderList()
        {
            if (ProcessCode.ToUpper() == "ALL") ProcessCode = "";
            if (Status == "전체") Status = "";

            query = $@"SELECT OP.OrderNo
                                , OP.OrderSeq
		                        , WO.WorkDate
		                        , iif(WO.EndDate = '1900-01-01', null, WO.EndDate) EndDate
		                        , OP.ProcessCode
		                        , SR.PItemCode
		                        , SR.MItemCode
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
                        LEFT JOIN CM_Item CM ON SR.PItemCode = CM.ItemCode
                        WHERE OP.CorpID = '{WrGlobal.CorpID}'
                        AND WO.WorkDate BETWEEN '{StartDate:d}' AND '{EndDate:d}'
                    ";

            if (ProcessCode != "") query += $"\n AND OP.ProcessCode = '{ProcessCode}'";
            if (Status != "") query += $"\n AND OP.Status = '{Status}'";
            if (MachineCode != "")
            {
                query += $" AND OP.MachineCode IN ('{MachineCode}')";
            }

            query += $"\n ORDER BY ISNULL(OP.DisplayNumber, 9999), OP.OrderNo, OP.OrderSeq ";

            return db.GetDataTable(query);
        }

        public DataTable Select_OrderList_Main()
        {
            query = $@"SELECT OP.OrderNo
                                , OP.OrderSeq
		                        , WO.WorkDate
		                        , WO.EndDate
		                        , OP.ProcessCode
                                , OP.MachineCode
		                        , SR.MItemCode
		                        , SR.PItemCode
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
                        LEFT JOIN CM_Item CM ON SR.PItemCode = CM.ItemCode
                        WHERE OP.CorpID = '{WrGlobal.CorpID}'
                        AND WO.WorkDate BETWEEN '{StartDate:d}' AND '{EndDate:d}'
                    ";

            if (machineCode != "")
            {
                query += $"\n AND op.MachineCode IN ('{machineCode}')";
            }

            query += $"\n ORDER BY OP.OrderNo, OP.OrderSeq ";

            return db.GetDataTable(query);
        }
    }
}
