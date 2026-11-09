using DevExpress.XtraCharts.Native;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.Model;
using System.Runtime.InteropServices.ComTypes;
using SmartPOP.Common;

namespace SmartPOP.Models.PM
{
    public class PM_OrderResult
    {
        #region Property

        public string CorpId { get; set; }
        public string OrderNo { get; set; }
        public int OrderSeq { get; set; }
        public int SubSeq { get; set; }
        public string LineCode { get; set; }
        public string ProcessCode { get; set; }
        public string MachineCode { get; set; }
        public string MaterialCode { get; set; }
        public string WorkerID { get; set; }
        public string LotNo { get; set; }
        public string SerialNo { get; set; }
        public string WorkDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int OrderQty { get; set; }
        public int WorkQty { get; set; }
        public int GoodQty { get; set; }
        public int NgQty { get; set; }
        public string DefectCode { get; set; }
        public string Result { get; set; }
        public string Rmk { get; set; }
        public string ProductCode { get; set; }
        public string ChildID { get; set; }
        public string ParentID { get; set; }

        // 조회조건용
        public string StartDt { get; set; }
        public string EndDt { get; set; }

        #endregion

        Database db = new Database();
        string query;


        public DataTable Select_PM_OrderProcess(string mc)
        {
            //실적등록 화면에서 사용
            StartDt = StartDt.ConvertStr_Empty_To_Null();
            EndDt = EndDt.ConvertStr_Empty_To_Null();
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();
            MachineCode = MachineCode.ConvertStr_Empty_To_Null();
            MaterialCode = MaterialCode.ConvertStr_Empty_To_Null();

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

			                , OP.HalfMaterialCode MaterialCode
			                , cm.MaterialName
                            , cm.Alias
                            , cm.MaterialClass1
                            , cm.MaterialClass2
                            , cm.MaterialClass3
                            , cm.Size
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            , cm.UnitCode
                            , cm.ColorCode

			                , OP.OrderQty
                            , ISNULL(ORS.TotalQty, 0) AS TotalWorkQty  
			                , 0 WorkQty
			                , 0 GoodQty
			                , 0 NgQty

                            , CONVERT(varchar(10), GETDATE(), 120) AS WorkDate

                            , ISNULL(RS.WorkerID, '') AS WorkerID
					        , ISNULL(CONVERT(varchar(10), RS.StartDate, 120), '') AS StartDate
					        , ISNULL(CONVERT(varchar(10), RS.EndDate, 120), '') AS EndDate   
                            , RS.OrderNo
							, RS.OrderSeq
							, RS.SubSeq
                            , RS.SerialNo
                            , CASE 
									    WHEN RS.StartDate IS NULL THEN 'Ins'
								    ELSE 'Upd'
							    END AS RowType
                          
                            , NULL AS SubSeq
                            , '' AS SerialNo
			                , OP.[Status]
			                , isnull(SD.FinalYn,'N') as FinalYn		-- 최종공정
                            , MC.DgpCode -- 접점코드
                            , 0 Cavity
                            , 0 Counter

	                FROM PM_OrderProcess OP
	                INNER JOIN PM_ProductPlan WO ON WO.OrderNo = OP.OrderNo
	                INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode
	                INNER JOIN CM_Item CM ON OP.HalfMaterialCode = CM.MaterialCode 
	                INNER JOIN PM_SdRoute SD ON OP.OrderNo = SD.OrderNo AND OP.RoutingSeq = SD.RoutingSeq
     LEFT JOIN (
                        SELECT 
                            OrderNo,
                            OrderSeq,
                            SUM(ISNULL(GoodQty, 0) + ISNULL(NgQty, 0)) AS TotalQty
                        FROM PM_OrderResult
                        GROUP BY OrderNo, OrderSeq
                    ) ORS ON OP.OrderNo = ORS.OrderNo AND OP.OrderSeq = ORS.OrderSeq
                    LEFT JOIN IF_DgpCode MC ON OP.CorpId = MC.Corpid AND OP.MachineCode = MC.Equip_Name AND MC.Gauge_Type = '카운터' AND MC.Value_Or = 'Y'
                    LEFT JOIN (
					                    SELECT OR1.OrderNo,
						                       OR1.OrderSeq,
                                               OR1.SubSeq,
						                       OR1.WorkerID,
                                               OR1.SerialNo,
						                       OR1.StartDate,
						                       OR1.EndDate
					                    FROM PM_OrderResult OR1
					                    INNER JOIN (
						                    SELECT OrderNo, OrderSeq, MIN(StartDate) AS MinStartDate
						                    FROM PM_OrderResult
						                    WHERE EndDate IS NULL
						                    GROUP BY OrderNo, OrderSeq
					                    ) AS OR2
					                      ON OR1.OrderNo = OR2.OrderNo
					                     AND OR1.OrderSeq = OR2.OrderSeq
					                     AND OR1.StartDate = OR2.MinStartDate
					                      WHERE OR1.EndDate IS NULL
				                    ) RS
				                    ON OP.OrderNo = RS.OrderNo
				                    AND OP.OrderSeq = RS.OrderSeq

			        WHERE OP.CorpId = '{WrGlobal.CorpID}'
                    AND OP.Status <> '작업완료' 
			        AND OP.OrderDate  BETWEEN {StartDt} AND {EndDt}  
                    ";

            if (mc != "")
            {
                query += $"\n AND op.MachineCode IN ({mc})";
            }

            if (ProcessCode != "NULL")
            {
                query += $" AND OP.ProcessCode = {ProcessCode}";
            }

            //if (MachineCode != "NULL")
            //{
            //    query += $" AND OP.MachineCode = {MachineCode}";
            //}

            if (MaterialCode != "NULL")
            {
                query += $" AND OP.MaterialCode = {MaterialCode}";
            }

            query += $"\nOrder By OP.OrderNo, OP.OrderSeq, OP.OrderDate";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResult(string mc)
        {
            //실적등록 화면에서 사용
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();
            MachineCode = MachineCode.ConvertStr_Empty_To_Null();
            MaterialCode = MaterialCode.ConvertStr_Empty_To_Null();

            query = $@"SELECT  '' AS Status
                                , odr.SubSeq
                                , odr.SerialNo
								, odr.OrderNo
								, odr.OrderSeq
								, odr.WorkDate

								, wo.CustCode
								, op.LineCode
								, odr.MaterialCode
								, odr.MaterialCode AS MaterialName
                                , cm.Alias
                                , cm.MaterialClass1
                                , cm.MaterialClass2
                                , cm.MaterialClass3
                                , cm.Size
                                , cm.UnitWeight
                                , cm.Texture
                                , cm.Type
                                , cm.UnitCode
                                , cm.ColorCode

								, odr.ProcessCode
								, odr.ProcessCode AS ProcessName
                                , odr.MachineCode

								, odr.WorkerID
                                , odr.WorkDate
                                , odr.GoodQty
                                , odr.NgQty
                                , odr.StartDate
                                , odr.EndDate
                                , IIF( (SELECT COUNT(Exam_No) 
                                        FROM QM_ExamResult
                                        WHERE Corp_Cd = odr.CorpId 
                                        AND Exam_Div_Cd = '30'
                                        AND Ref_No = odr.SerialNo
                                       ) > 0 , 'Y', 'N') AS ChkProc --공정검사 유무
                                , op.OrderQty
                                , ISNULL(odr.GoodQty, 0) + ISNULL(odr.NgQty, 0) AS WorkQty

                        FROM PM_OrderResult odr
						LEFT JOIN PM_ProductPlan wo ON wo.OrderNo = odr.OrderNo
                        LEFT JOIN PM_OrderProcess op ON odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq
                        LEFT JOIN CM_Item cm ON odr.CorpId = cm.CorpId AND odr.MaterialCode = cm.MaterialCode

                        WHERE odr.CorpID = '{WrGlobal.CorpID}' 
                        AND odr.WorkDate BETWEEN '{StartDt} 00:00:00' AND '{EndDt} 23:59:59'";


            if (ProcessCode != "NULL")
            {
                query += $" AND odr.ProcessCode = {ProcessCode}";
            }

            if (mc != "")
            {
                query += $"\n AND odr.MachineCode IN ({mc})";
            }

            //if (MachineCode != "NULL")
            //{
            //    query += $" AND odr.MachineCode = {MachineCode}";
            //}

            if (MaterialCode != "NULL")
            {
                query += $@"AND odr.MaterialCode = {MaterialCode}";
            }

            query += " ORDER BY odr.WorkDate, odr.SerialNo";

            return db.GetDataTable(query);
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

        public DataTable Select_PM_OrderResult_ByOrderNo()
        {
            query = $@"SELECT odr.SubSeq
                                , odr.SerialNo
                                , odr.OrderNo
                                , odr.OrderSeq
                                , op.HalfMaterialCode AS ProductCode
                                , odr.ProcessCode
                                , odr.WorkDate
                                , odr.GoodQty
                                , odr.NgQty
                                , odr.StartDate
                                , odr.EndDate
                                , IIF( (SELECT COUNT(OrderNo) 
                                       FROM WorkInspection 
                                       WHERE CorpId = odr.CorpId 
                                       AND OrderNo = odr.OrderNo 
                                       AND OrderSeq = odr.OrderSeq 
                                       AND SubSeq = odr.SubSeq
                                       ) > 0 , 'Y', 'N') AS ChkProc --공정검사 유무
                        FROM PM_OrderResult odr
                        LEFT JOIN PM_OrderProcess op ON odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq
                        WHERE odr.CorpID = '{WrGlobal.CorpID}'
                        AND odr.OrderNo = '{OrderNo}'
                        AND odr.OrderSeq = {OrderSeq}
                        ORDER BY odr.SubSeq DESC
                        ";

            return db.GetDataTable(query);
        }

        public DataRow Select_PM_OrderResult_BySerialNo(string serialNo)
        {
            query = $@"
                        SELECT 
                            odr.OrderNo
                            , odr.OrderSeq
                            , odr.SubSeq
                            , odr.SerialNo
                            , odr.MachineCode
                            , ma.MachineName
                            , odr.WorkQty
                            , odr.ItemCode
                            , cm.ItemName
                            , cm.Size
                            , cm.UnitCode
                        FROM PM_OrderResult odr
                        LEFT JOIN CM_Item cm on odr.CorpId = cm.CorpId AND odr.ItemCode= cm.ItemCode
                        LEFT JOIN EQ_Machine ma on odr.CorpId = ma.CorpId AND odr.MachineCode = ma.MachineCode
                        WHERE odr.CorpId = '{WrGlobal.CorpID}'
                        AND odr.SerialNo = '{serialNo}'
                        ";

            return db.GetDataRow(query);
        }

        public DataTable Select_PM_OrderResult_ByNoSeq()
        {
            query = $@"SELECT OrderNo
		                        , OrderSeq
		                        , SubSeq
		                        , WorkDate
		                        , StartDate
		                        , EndDate
		                        , (ISNULL(GoodQty, 0) + ISNULL(NgQty, 0)) AS WorkQty
		                        , ISNULL(GoodQty, 0) AS GoodQty
		                        , ISNULL(NgQTy, 0) AS NgQty
		                        , WorkerID
                        FROM PM_OrderResult
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                        ORDER BY SubSeq DESC
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_SerialNo_ByOrderNo()
        {
            query = $@"
                SELECT 
                	convert(varchar(10),R.WorkDate,120) as WorkDate,
                	R.ProcessCode,
                	P.ProcessName,
                	R.SerialNo,
                	R.GoodQty
                	FROM PM_OrderResult R
                	left join CM_Process P on P.ProcessCode = r.ProcessCode
                	where R.CorpId = '{WrGlobal.CorpID}' AND R.OrderNo = '{OrderNo}'
                ";

            return db.GetDataTable(query);
        }

        public DataTable Select_RptSerial()
        {
            query = $@"SELECT 
                               R.SerialNo
                                , CM.ItemName MaterialName
                                , CC.CustName
                                , ISNULL(SOD.Qty, 0) Qty
                                , R.GoodQty
                                , SOM.ObtainDate
                                , SOM.DeliveryDate
                        FROM PM_OrderResult R
                        LEFT JOIN PM_ProductPlan WO ON R.OrderNo = WO.OrderNo
                        LEFT JOIN BM_SdOrderD SOD ON WO.SdOrderNo = SOD.SdOrderNo AND WO.SdOrderSeq = SOD.SdOrderSeq
                        LEFT JOIN BM_SdOrderM SOM ON SOD.SdOrderNo = SOM.SdOrderNo
                        LEFT JOIN CM_Item CM ON SOD.ItemCode = CM.ItemCode
                        LEFT JOIN CM_Cust CC ON CC.CustCode = SOM.SoldToCode
                        WHERE R.CorpId = '{WrGlobal.CorpID}'
                        AND R.OrderNo = '{OrderNo}' and R.SerialNo = '{SerialNo}'
                        ";

            return db.GetDataTable(query);
        }

        public int GetMaxSubSeq()
        {
            int maxSubSeq = 0;

            query = $@"
                	SELECT ISNULL(MAX(SubSeq), 0) AS MaxSubSeq
                	FROM PM_OrderResult
                	WHERE CorpId = '{CorpId}'
                	AND OrderNo = '{OrderNo}'
                	AND OrderSeq = '{OrderSeq}'
                
                ";
            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                maxSubSeq = dr["MaxSubSeq"].ConvertInt_Null_To_Zero();
            }

            return maxSubSeq;

        }

        public bool ExistsPM_OrderResult()
        {
            int cnt = 0;

            query = $@"SELECT COUNT(CorpId) AS CNT
                        FROM PM_OrderResult
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                        AND SubSeq = '{SubSeq}'
                    ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                cnt = dr["CNT"].ConvertInt_Null_To_Zero();
            }

            return cnt > 0;
        }

        public DataTable Select_PM_OrderResult_Status()
        {
            query = $@"
                       SELECT Odr.OrderNo
                                , Odr.OrderSeq
                                , Odr.SubSeq
                            
                                , Odr.StartDate
                                , Odr.EndDate

								, MO.StartTime
								, MO.EndTime
								, StopTime
                                , CASE
								WHEN NULLIF(Odr.EndDate, CONVERT(datetime, '1900-01-01')) IS NOT NULL THEN '작업종료'
								WHEN NULLIF(MO.StartTime, CONVERT(datetime, '1900-01-01')) IS NULL THEN '대기'
								WHEN NULLIF(MO.EndTime,   CONVERT(datetime, '1900-01-01')) IS NULL
									    AND NULLIF(MO.EndTime, CONVERT(datetime, '1900-01-01')) IS NULL THEN '비가동'
								WHEN NULLIF(MO.StartTime, CONVERT(datetime, '1900-01-01')) IS NOT NULL
									    AND NULLIF(MO.EndTime,   CONVERT(datetime, '1900-01-01')) IS NULL THEN '작업중'
								ELSE '대기'
							END AS WorkStatus
                        FROM PM_OrderResult Odr
                      LEFT JOIN EQ_Operation MO ON Odr.OrderNo = MO.OrderNo
                        AND MO.Seq = (SELECT MAX(Seq)
							FROM EQ_Operation
							WHERE OrderNo = Odr.OrderNo)
                        WHERE Odr.CorpId =  '{CorpId}'
                        AND Odr.OrderNo = '{OrderNo}'
                        AND Odr.OrderSeq = '{OrderSeq}'
                        AND Odr.SubSeq = '{SubSeq}'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResult_Exam(string exam_Div, string startDt = "", string endDt = "", string[] Item_Div = null, List<string> processCds = null, string serialNo = "")
        {
            //
            query = $@"SELECT DISTINCT 
                                         ODR.SerialNo AS Ref_No
                                       , NULL AS Ref_No_Seq
                                       , ODR.WorkDate

                                       , ODR.ProcessCode
                                       , CP.ProcessName

                                       , OP.LineCode
                                       , CL.LineName

                                       , OP.MachineCode
                                       , MC.MachineName

                                       , OP.OrderNo
                                       , OP.OrderSeq

                                       , WO.MaterialCode ProductCode
                                       , CM2.MaterialName ProductName
                                       , CM2.Alias
                                       , CM2.Size
                                       
                                        , CM2.MaterialClass1
                                        , CM2.MaterialClass2
                                        , CM2.MaterialClass3
                                        , CM2.UnitWeight
                                        , CM2.Texture
                                        , CM2.Type
                                        , CM2.UnitCode
                                        , CM2.ColorCode


                                       , OP.HalfMaterialCode MaterialCode
                                       , CM.MaterialName MaterialName
                                       , CM.Alias MaterialAlias

                                       , OP.OrderQty
                                       , OP.WorkQty 
                                       , ODR.GoodQty + ODR.NgQty Exam_Qty
                                       , ODR.GoodQty
                                       , ODR.NgQty
                                       , ODR.SubSeq

                                       , OP.[Status]

                                       --, IIF(WI.SerialNo IS NULL, 'N', 'Y') AS ChkProc
                               	   , CASE WHEN ( SELECT COUNT(*) 
									  FROM QM_ExamResult 
									  WHERE Corp_Cd = OP.CorpId
									  AND Exam_Div_Cd = '{exam_Div}'            --초중종
									  AND Ref_No = ODR.SerialNo
                                         ) > 0 THEN 'Y'
										ELSE 'N' END AS ExamYn --검사 여부Yn

                                   FROM PM_OrderResult ODR
                                   LEFT JOIN PM_OrderProcess OP  ON ODR.CorpId = OP.CorpId AND ODR.OrderNo = OP.OrderNo AND ODR.OrderSeq = OP.OrderSeq
                                   INNER JOIN PM_ProductPlan   WO  ON ODR.CorpId = OP.CorpId AND WO.OrderNo = OP.OrderNo
                                   LEFT JOIN CM_Process    CP  ON ODR.CorpId = OP.CorpId AND OP.ProcessCode = CP.ProcessCode
                                   LEFT JOIN CM_Line       CL  ON ODR.CorpId = OP.CorpId AND OP.LineCode = CL.LineCode
                                   LEFT JOIN EQ_Machine  MC  ON ODR.CorpId = OP.CorpId AND OP.MachineCode = MC.MachineCode
                                   INNER JOIN CM_Item  CM  ON ODR.CorpId = OP.CorpId AND OP.HalfMaterialCode = CM.MaterialCode 
                                   INNER JOIN CM_Item  CM2 ON ODR.CorpId = OP.CorpId AND WO.MaterialCode = CM2.MaterialCode

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

            if (MaterialCode != "" && MaterialCode.ToUpper() != "ALL")
            {
                query += $"\n AND OP.HalfMaterialCode = '{MaterialCode}' ";
            }

            if (serialNo != "")
            {
                query += $"\n AND ODR.serialNo = '{serialNo}' ";
            }

            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND CM.MaterialType IN(";
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

        public DataTable Select_PackagingProc()
        {
            query = $@"
                    SELECT
	                      BMC.Status
	                    , BMC.BomMaterialCode --소요자재
	                    , CM.MaterialName --소요자재
	                    , OP.OrderQty	  --지시량
	                    , SR.WorkQty         --사용량
                        , SUM(BMC.InQty) AS Qty			 --입고량
	                    , ISNULL(OP.LossQty,0) AS LossQty     --로스량(수기입력)
                        , OP.WorkQty  --생산량
                        , (ISNULL( SUM(BMC.InQty) ,0) - (ISNULL(SR.WorkQty,0) + 0)) AS RemainQty  
                        , (ISNULL(OP.OrderQty,0)) - (ISNULL(OP.WorkQty, 0)) AS NoneWorkQty  --미생산량
                        , OP.OrderNo , OP.OrderSeq 
                    FROM BomMaterialCheck BMC
                    LEFT JOIN PM_SdRoute SR ON SR.BomMaterialCode = BMC.BomMaterialCode AND Sr.OrderNo = BMC.OrderNo
                    LEFT JOIN PM_OrderProcess OP ON OP.OrderNo = BMC.OrderNo AND OP.OrderSeq = BMC.OrderSeq
                    LEFT JOIN CM_Item CM ON CM.MaterialCode = BMC.BomMaterialCode

                    WHERE OP.OrderNo = '{OrderNo}' AND OP.OrderSeq = '{OrderSeq}'  AND BMC.Status  = '{ProductCode}' 
                    GROUP BY BMC.Status,	  BMC.BomMaterialCode 
	                    , CM.MaterialName 
	                    , OP.OrderQty	  
	                    , SR.WorkQty      
	                    , OP.LossQty    
	                    , OP.WorkQty
                        , OP.OrderNo , OP.OrderSeq 

                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResult_StopTime(string startTime, string endTime)
        {
            query = $@"
                       SELECT
                               CONCAT(
									ISNULL(SUM(StopTime), 0) / 60, '시간 ',
									ISNULL(SUM(StopTime), 0) % 60, '분'
								) AS StopTime
                        , ISNULL(SUM(StopTime), 0) AS StopTimeMM
						FROM EQ_Operation
                        WHERE CorpId =  '{CorpId}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
		                AND StartTime >= '{startTime}'";

            if (endTime != "")
            {
                query += $"\nAND EndTime <= '{endTime}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResult()
        {
            query = $@"
                        SELECT 
                                  odr.OrderNo
                                , odr.OrderSeq
                                , odr.SubSeq
                                , odr.WorkDate
                        
                                , odr.ProcessCode
                                , cp.ProcessName

                                , odr.MachineCode
                                , cdm.MachineName

                                , odr.ItemCode
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
                        
                                , odr.WorkerID
                                , cu.EmployeeName WorkerName
                                , odr.LotNo
                                , odr.SerialNo
                                , odr.WorkDate
                                , odr.StartDate
                                , odr.EndDate
                        
                                , odr.WorkQty
                                , odr.GoodQty
                                , odr.NgQty
                        
                                , op.Remark AS OPRemark
                                , op.Status AS OPStatus
                                , op.OrderQty

                                , op.LineCode
                                , op.WorkQty AS TotalWorkQty
                                , cl.LineName
                        
                        FROM PM_OrderResult odr
                        LEFT JOIN PM_OrderProcess op ON odr.CorpId = op.CorpId AND odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq
                        LEFT JOIN CM_Item cm ON odr.CorpId = cm.CorpId AND odr.ItemCode = cm.ItemCode
                        LEFT JOIN CM_Line cl ON odr.CorpId = cl.CorpId AND op.LineCode = cl.LineCode
                        LEFT JOIN CM_Process cp ON odr.CorpId = cp.CorpId AND odr.ProcessCode = cp.ProcessCode
                        LEFT JOIN EQ_Machine cdm ON odr.CorpId = cdm.CorpId AND odr.MachineCode = cdm.MachineCode
                        LEFT JOIN SM_User cu ON odr.CorpId = cu.CorpId AND odr.WorkerID = cu.EmployeeId
                        WHERE odr.CorpID = '{WrGlobal.CorpID}'";

            if (OrderNo.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND odr.OrderNo = '{OrderNo}' AND odr.OrderSeq = '{OrderSeq}'";
            }

            if (StartDate.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND odr.WorkDate >= '{StartDate}'";
            }

            if (EndDate.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND odr.WorkDate <= '{EndDate}'";
            }

            query += $"\nORDER BY odr.SubSeq DESC";


            return db.GetDataTable(query);
        }

        public DataTable Select_BomList_OLD()
        {
            query = $@"
                    SELECT DISTINCT
	                      SR.BomMaterialCode --소요자재
	                    , SR.BomMaterialName --소요자재
	                    , SR.OrderQty		 --지시량
	                    , SR.WorkQty         --사용량
                        , A.Qty			 --입고량
	                    , SR.WorkQty         --사용량
	                    , SR.NextProcessCode AS pNextProc , CP.ProcessName --다음공정
                        , ISNULL(OP.LossQty,0) AS LossQty     --로스량(수기입력)
                        , (OP.GoodQty + OP.NgQty) AS WorkQty  --생산량
						, (ISNULL(A.OutQty,0) - (ISNULL(SR.WorkQty,0) + 0)) AS RemainQty                        --재공잔량(입고량-(사용량+로스량))                        
                        , (ISNULL(SR.OrderQty,0)) - (ISNULL(OP.GoodQty,0) + ISNULL(OP.NgQty,0)) AS NoneWorkQty  --미생산량
                        , A.MmOrderNo , A.MmOrderSeq 

                    FROM SdRoute SR
                    LEFT JOIN SdRoute Parent ON SR.ParentID = Parent.ChildID AND SR.OrderNo = Parent.OrderNo
                    LEFT JOIN CM_Process CP   ON SR.NextProcessCode = CP.ProcessCode
                    LEFT JOIN 
                        (SELECT SD.MaterialCode, SD.Qty, RD.MmOrderNo, Rd.MmOrderSeq FROM ST_StockD SD 
                         LEFT JOIN RequestDetail RD ON RD.ReqNo =SD.JobNo AND RD.ReqSeq = SD.JobSeq ) A 
                    ON A.MaterialCode = SR.BomMaterialCode AND A.MmOrderNo = SR.OrderNo 
                    LEFT JOIN PM_OrderProcess OP ON A.MmOrderNo = OP.OrderNo AND A.MmOrderSeq = OP.OrderSeq 
                    WHERE SR.OrderNo = '{OrderNo}' AND SR.ParentID = '{ChildID}' 
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_nextProc()
        {
            query = $@"
                    SELECT DISTINCT
	                     SR.NextProcessCode AS pNextProc , CP.ProcessName --다음공정
                    FROM PM_SdRoute SR
                    LEFT JOIN PM_SdRoute Parent ON SR.ParentID = Parent.ChildID AND SR.OrderNo = Parent.OrderNo
                    LEFT JOIN CM_Process CP   ON SR.NextProcessCode = CP.ProcessCode
                    LEFT JOIN 
                        (SELECT SD.MaterialCode, SD.OutQty, RD.MmOrderNo, Rd.MmOrderSeq FROM ST_StockD SD 
                         LEFT JOIN RequestDetail RD ON RD.ReqNo =SD.JobNo AND RD.ReqSeq = SD.JobSeq ) A 
                    ON A.MaterialCode = SR.BomMaterialCode AND A.MmOrderNo = SR.OrderNo 
                    LEFT JOIN PM_OrderProcess OP ON A.MmOrderNo = OP.OrderNo AND A.MmOrderSeq = OP.OrderSeq 
                    WHERE SR.OrderNo = '{OrderNo}' AND SR.ChildID = '{ChildID}' 
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_BomList()
        {
            query = $@"
				    SELECT 
						  SR.BomMaterialCode     --소요자재
					    , SR.BomMaterialName     --소요자재
						, Parent.BomMaterialCode --생산품
						, Parent.BomMaterialName --생산품
						, OP.OrderQty            --지시량
						, ISNULL(SR.WorkQty * Op.OrderQty, 0) as WorkQty --SR.WorkQty	사용량	-> 소요량 * 작업지시수량	
						--, ST.StockQty AS Qty     --입고량(재고량)
						-- 수정된 입고량(재고량)
						, ISNULL(dbo.FN_CURRENT_STOCK_ByMaterialType('1000', cm.MaterialType ,SR.BomMaterialCode), 0) as Qty -- 함수 수정후 적용
						, OP.LossQty             --로스량(수기입력)
						, OP.WorkQty		     --생산량		
                        , (ISNULL(OP.OrderQty,0)) - (ISNULL(OP.WorkQty, 0)) AS NoneWorkQty  --미생산량
                        --, (ISNULL(ST.StockQty ,0) - (ISNULL(SR.WorkQty,0) + 0)) AS RemainQty  --재공잔량
						, (ISNULL(dbo.FN_CURRENT_STOCK_ByMaterialType('1000', CM.MaterialType , SR.BomMaterialCode), 0) - (ISNULL(SR.WorkQty, 0) + 0)) AS RemainQty  -- 재공잔량
                        , OP.OrderNo 
                        , OP.OrderSeq 
					FROM PM_SdRoute SR
					LEFT JOIN PM_SdRoute Parent ON SR.ParentID = Parent.ChildID AND SR.OrderNo = Parent.OrderNo
					LEFT JOIN PM_OrderProcess OP ON OP.OrderNo = Parent.OrderNo AND OP.HalfMaterialCode = Parent.BomMaterialCode
                    LEFT JOIN CM_Item CM ON CM.MaterialCode = SR.BomMaterialCode
					--LEFT JOIN (SELECT MaterialCode, Sum(Qty) AS StockQty FROM ST_StockD GROUP BY MaterialCode) ST
					--          ON ST.MaterialCode = SR.BomMaterialCode
					WHERE SR.OrderNo = '{OrderNo}' AND SR.ParentID = '{ChildID}'

/*
                    SELECT
	                      BMC.Status
	                    , BMC.BomMaterialCode --소요자재
	                    , CM.MaterialName --소요자재
	                    , OP.OrderQty	  --지시량
	                    , SR.WorkQty         --사용량
                        , SUM(BMC.InQty) AS Qty			 --입고량
	                    , ISNULL(OP.LossQty,0) AS LossQty     --로스량(수기입력)
                        , OP.WorkQty  --생산량
                        , (ISNULL( SUM(BMC.InQty) ,0) - (ISNULL(SR.WorkQty,0) + 0)) AS RemainQty  
                        , (ISNULL(OP.OrderQty,0)) - (ISNULL(OP.WorkQty, 0)) AS NoneWorkQty  --미생산량
                        , OP.OrderNo , OP.OrderSeq 
                    FROM BomMaterialCheck BMC
                    LEFT JOIN SdRoute SR ON SR.BomMaterialCode = BMC.BomMaterialCode AND Sr.OrderNo = BMC.OrderNo
                    LEFT JOIN PM_OrderProcess OP ON OP.OrderNo = BMC.OrderNo AND OP.OrderSeq = BMC.OrderSeq
                    LEFT JOIN CM_Item CM ON CM.MaterialCode = BMC.BomMaterialCode

                    WHERE OP.OrderNo = '{OrderNo}' AND OP.OrderSeq = '{OrderSeq}'  AND BMC.Status  = '{ProductCode}' 
                    GROUP BY BMC.Status,	  BMC.BomMaterialCode 
	                    , CM.MaterialName 
	                    , OP.OrderQty	  
	                    , SR.WorkQty      
	                    , OP.LossQty    
	                    , OP.WorkQty
                        , OP.OrderNo , OP.OrderSeq 
*/
                    ";

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
                        , '{ProcessCode}'
                        , {MachineCode.ConvertStr_Empty_To_Null()}
                        , {MaterialCode.ConvertStr_Empty_To_Null()}
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

        public bool Ins_OrderProgress()
        {
            query = $@"INSERT INTO OrderProgress
	                    (
		                     CorpId
                            , OrderNo
                            , OrderSeq
                            , WorkDate
                            , WorkerID
                            , WorkType
                            , ProcessCode
		                    , StartDate
                            , EndDate
		                    , WorkQty
                            , GoodQty
                            , NgQty
		                    , CompletedYn
                            , CreId
                            , CreIP
                            , CreDt
	                    )
                      VALUES
                       (
                          '{CorpId}'
                        , '{OrderNo}'
                        , {OrderSeq}
                        , {StartDate.ConvertStr_Empty_To_Null()}
                        , '{WrGlobal.LoginID}'
                        , '10'
                        , {ProcessCode.ConvertStr_Empty_To_Null()}
                        , {StartDate.ConvertStr_Empty_To_Null()}
                        , {EndDate.ConvertStr_Empty_To_Null()}                        
                        , {WorkQty.ConvertNumeric_Empty_To_Null()}
                        , {GoodQty.ConvertNumeric_Empty_To_Null()}
                        , {NgQty.ConvertNumeric_Empty_To_Null()}
                         , CASE 
                              WHEN {OrderQty.ConvertNumeric_Empty_To_Null()} <= {GoodQty.ConvertNumeric_Empty_To_Null()} THEN 'Y'
                              ELSE 'N'
                          END
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
                      WorkQty     = {WorkQty.ConvertNumeric_Empty_To_Null()}
                    , GoodQty     = {GoodQty.ConvertNumeric_Empty_To_Null()}
                    , NgQty       = {NgQty.ConvertNumeric_Empty_To_Null()}
                    , Status    = '작업중'
                    , StartDate = {StartDate.ConvertStr_Empty_To_Null()}
                    , EndDate   = {EndDate.ConvertStr_Empty_To_Null()}
               WHERE CorpId    = '{CorpId}'
                    AND OrderNo   = '{OrderNo}'
                    AND OrderSeq  = {OrderSeq}
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_PM_ProductPlan()
        {
            //SdRoute 사내공정 마지막 라우팅 순번의 지시일 경우
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

        public bool Upd_PM_OrderResult_POP()
        {
            query = $@"
                UPDATE PM_OrderResult
                SET
                 ProcessCode = {ProcessCode.ConvertStr_Empty_To_Null()}
                , MachineCode = {MachineCode.ConvertStr_Empty_To_Null()}
                , WorkerID    = '{WorkerID}'
                , LotNo       = {LotNo.ConvertStr_Empty_To_Null()}
                , WorkDate    = {WorkDate.ConvertStr_Empty_To_Null()}
                , StartDate   = {StartDate.ConvertStr_Empty_To_Null()}
                , EndDate     = {EndDate.ConvertStr_Empty_To_Null()}
                , WorkQty     = {WorkQty.ConvertFloat_Null_To_Zero()}
                , GoodQty     = {GoodQty.ConvertFloat_Null_To_Zero()}
                , NgQty       = {NgQty.ConvertFloat_Null_To_Zero()}
                , Result      = {Result.ConvertStr_Empty_To_Null()}
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND OrderNo = '{OrderNo}'
                AND OrderSeq = '{OrderSeq}'
                AND SubSeq = '{SubSeq}'
                ";

            return db.ExecuteQuery(query);
        }

        public void Update_PM_OrderResult_NgQty()
        {
            query = $@"
                        UPDATE PM_OrderResult SET
                            NgQty = {NgQty}
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND OrderSeq = '{OrderSeq}'
                        AND SubSeq = '{SubSeq}'
                    ";

            db.ExecuteQuery(query);
        }

        public bool Stock_MakeStockByWork()
        {
            // 수불 등록
            query = $@" EXEC Stock_MakeStockByWork '{CorpId}', '{OrderNo}', {OrderSeq}, {SubSeq}, 'Y' ";

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

        public bool Upd_PM_OrderResult()
        {
            query = $@"

                UPDATE PM_OrderResult
                SET  
                      ProcessCode = {ProcessCode.ConvertStr_Empty_To_Null()}
                    , MachineCode = {MachineCode.ConvertStr_Empty_To_Null()}
                    , MaterialCode = {MaterialCode.ConvertStr_Empty_To_Null()}
                    , WorkerID    = {WorkerID.ConvertStr_Empty_To_Null()}
                    , LotNo       = {LotNo.ConvertStr_Empty_To_Null()}
                    , WorkDate    = {WorkDate.ConvertStr_Empty_To_Null()}
                    , StartDate   = {StartDate.ConvertStr_Empty_To_Null()}
                    , EndDate     = {EndDate.ConvertStr_Empty_To_Null()}
                    , WorkQty     = {WorkQty.ConvertNumeric_Empty_To_Null()}
                    , GoodQty     = {GoodQty.ConvertNumeric_Empty_To_Null()}
                    , NgQty       = {NgQty.ConvertNumeric_Empty_To_Null()}
                    , DefectCode  = {DefectCode.ConvertStr_Empty_To_Null()}
                    , Result      = {Result.ConvertStr_Empty_To_Null()}
                    , ModId       = '{WrGlobal.LoginID}'
                    , ModIP       = '{WrGlobal.IpAddress}'
                    , ModDt       =  getdate()
               WHERE CorpId    = '{CorpId}'
                    AND OrderNo   = '{OrderNo}'
                    AND OrderSeq  = {OrderSeq}
                    AND SubSeq    = {SubSeq}
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_OrderProgress()
        {
            query = $@"

                UPDATE OrderProgress
                SET  
                      WorkDate    = {WorkDate.ConvertStr_Empty_To_Null()}
                    , WorkerID    = {WorkerID.ConvertStr_Empty_To_Null()}
                    , ProcessCode = {ProcessCode.ConvertStr_Empty_To_Null()}                    
                    , StartDate   = {StartDate.ConvertStr_Empty_To_Null()}
                    , EndDate     = {EndDate.ConvertStr_Empty_To_Null()}
                    , WorkQty  = {WorkQty.ConvertNumeric_Empty_To_Null()}
                    , GoodQty     = {GoodQty.ConvertNumeric_Empty_To_Null()}
                    , NgQty       = {NgQty.ConvertNumeric_Empty_To_Null()}
                    , ModId       = '{WrGlobal.LoginID}'
                    , ModIP       = '{WrGlobal.IpAddress}'
                    , ModDt       =  getdate()
               WHERE CorpId    = '{CorpId}'
                    AND OrderNo   = '{OrderNo}'
                    AND OrderSeq  = {OrderSeq}
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_NgQTY()
        {
            query = $@"

                UPDATE PM_OrderResult
                SET
                     NgQty       = {NgQty.ConvertFloat_Null_To_Zero()}
					, GoodQty = WorkQty - {NgQty.ConvertFloat_Null_To_Zero()}
                    , ModId       = '{WrGlobal.LoginID}'
                    , ModIP       = '{WrGlobal.IpAddress}'
                    , ModDt       =  getdate()
               WHERE CorpId    = '{WrGlobal.CorpID}'
                    AND SerialNo  = '{SerialNo}'
					AND SubSeq    = {SubSeq}
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
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_OrderProgress()
        {
            query = $@"
                DELETE OrderProgress
                WHERE CorpId  = '{CorpId}'
                AND OrderNo   = '{OrderNo}'
                AND OrderSeq  = {OrderSeq}
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
            //SdRoute 사내공정 마지막 라우팅 순번의 지시일 경우
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
    }
}
