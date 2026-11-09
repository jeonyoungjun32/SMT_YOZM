using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;

namespace SmartMES.Models.PM
{
    class PM_SdRoute
    {
        #region Property
        public string OrderNo { get; set; }
        public string ChildID { get; set; }
        public string ParentID { get; set; }
        public string SdOrderNo { get; set; }
        public int SdOrderSeq { get; set; }
        public string PItemCode { get; set; }
        public int RoutingSeq { get; set; }
        public string WorkType { get; set; }
        public string MItemCode { get; set; }
        public string MItemName { get; set; }
        public float StdPrice { get; set; }
        public float OrderQty { get; set; }
        public float WorkQty { get; set; }
        public float PlanQty { get; set; }
        public string Status { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string ProcessType { get; set; }
        public string PrevProcessCode { get; set; }
        public string NextProcessCode { get; set; }
        public string CustCode { get; set; }
        public string FinalYn { get; set; }
        public string SupplyYN { get; set; }
        public string Remarks { get; set; }

        public DateTime? CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public DateTime? ModDt { get; set; }
        public string ItemCode { get; set; }
        public int DetailSeq { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        #endregion
        Database db = new Database();
        string query;

        public DataTable Get_CmRoute()
        {
            query = $@" SELECT CR.ProcessCode
                                , CP.ProcessName
                                , ISNULL(A.LineCode, '') AS MachineCode
                                , CR.RoutingSeq
                                , CR.ChildID
                                , CR.ParentID
                        FROM CmRouting CR
                        LEFT JOIN CM_Process CP ON CR.ProcessCode = CP.ProcessCode
                        LEFT JOIN (
                                SELECT ProcessCode
                                        , RoutingSeq
                                        , LineCode
                                FROM PM_OrderProcess
                                WHERE CorpId = '{WrGlobal.CorpID}'
                                AND OrderNo = '{OrderNo}'
                            ) AS A ON CR.ProcessCode = A.ProcessCode AND CR.RoutingSeq = A.RoutingSeq
                        WHERE CR.CorpID = '{WrGlobal.CorpID}'
                        ORDER BY RoutingSeq
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_SdRouteByOrderNo()
        {
            query = $@"SELECT  
                            '' AS Status
                            , SR.OrderNo
		                    , SR.RoutingSeq
		                    , WO.WorkDate
		                    , WO.EndDate
		                    , SR.ItemCode
		                    , CMP.ItemName ProductName
		                    , IIF(op.OrderSeq IS NULL, SR.PlanQty, op.OrderQty) AS OrderQty

                            , SR.MItemCode ItemCode
                            , CM.ItemName ItemName
                            , CM.Size 
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

                            , SR.ProcessCode
                            , SR.ChildID
                            , SR.ParentID
                            , SR.Status SRStatus

                            , ISNULL(op.OrderDate, GETDATE()) OrderDate
                            , op.OrderSeq
                            , iif(op.CorpId IS NULL, cl.LineCode, op.LineCode) AS LineCode --라인코드 디폴트 값 : 공정 테이블 라인 컬럼
                            , op.MachineCode
                            , op.WorkQty
                            , IIF(op.OrderNo IS NULL, '', 'Y') AS OrderYn

                            --
                            , '' AS btnDelete

                            , op.Remark

                        FROM PM_SdRoute SR
                        LEFT JOIN PM_ProductPlan WO ON SR.CorpId = WO.CorpId AND SR.OrderNo = WO.OrderNo
                        LEFT JOIN PM_OrderProcess op ON SR.CorpId = op.CorpId AND SR.OrderNo = op.OrderNo And SR.RoutingSeq = op.RoutingSeq

                        LEFT JOIN CM_Item CMP ON SR.CorpId = CMP.CorpId AND SR.ItemCode = CMP.ItemCode
                        LEFT JOIN CM_Item CM  ON SR.CorpId = CM.CorpId AND SR.MItemCode = cm.ItemCode
                        LEFT JOIN CM_Line cl  ON SR.CorpId = cl.CorpId AND sr.ProcessCode = cl.ProcessCode
                        WHERE SR.CorpID = '{WrGlobal.CorpID}'
                        AND SR.WorkType = '10'

                        ";

            if (ProcessType.ConvertString_Null_To_Empty() != "" && ProcessType.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND SR.ProcessType = '{ProcessType}'";
            }

            if (ProcessCode.ConvertString_Null_To_Empty() != "" && ProcessCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND SR.ProcessCode = '{ProcessCode}'";
            }

            if (OrderNo.ConvertString_Null_To_Empty() != "")
			{
                query += $"\nAND SR.OrderNo = '{OrderNo}'";
            }

            if (Status.ConvertString_Null_To_Empty() != "" && Status.ConvertString_Null_To_Empty().ToUpper() != "ALL")
			{
                query += $@" AND SR.Status = '{Status}' ";
            }

            query += $"\nORDER BY sr.RoutingSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_SdRouteByOrderNo_KNP()
        {
            query = $@"SELECT  
                            '' AS Status
                            , SR.OrderNo
		                    , SR.RoutingSeq
		                    , WO.WorkDate
		                    , WO.EndDate
		                    , SR.ItemCode
		                    , CMP.ItemName ProductName
		                    , IIF(op.OrderSeq IS NULL, SR.PlanQty, op.OrderQty) AS OrderQty
                            , SR.PlanQty

                            , SR.MItemCode ItemCode
                            , CM.ItemName ItemName
                            , CM.Size 
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

                            , SR.ProcessCode
                            , SR.ChildID
                            , SR.ParentID
                            , SR.Status SRStatus

                            , ISNULL(SR.ModDt, op.OrderDate) OrderDate --계획일자가 없으면 지시일자가 나오도록
                            , op.OrderSeq
                            , op.LineCode
                            , op.MachineCode
                            , op.WorkQty
                            , IIF(op.OrderNo IS NULL, '', 'Y') AS OrderYn
                            , IIF(SR.Status IS NULL OR SR.Status = '', '', 'Y') AS RouteYn

                            --
                            , '' AS btnDelete

                            , op.Remark
                            , SR.Remarks

                        FROM PM_SdRoute SR
                        LEFT JOIN PM_ProductPlan WO ON SR.CorpId = WO.CorpId AND SR.OrderNo = WO.OrderNo
                        LEFT JOIN PM_OrderProcess op ON SR.CorpId = op.CorpId AND SR.OrderNo = op.OrderNo And SR.RoutingSeq = op.RoutingSeq

                        LEFT JOIN CM_Item CMP ON SR.CorpId = CMP.CorpId AND SR.ItemCode = CMP.ItemCode
                        LEFT JOIN CM_Item CM  ON SR.CorpId = CM.CorpId AND SR.MItemCode = cm.ItemCode
                        WHERE SR.CorpID = '{WrGlobal.CorpID}'
                        AND SR.WorkType = '10'

                        ";

            if (ProcessType.ConvertString_Null_To_Empty() != "" && ProcessType.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND SR.ProcessType = '{ProcessType}'";
            }

            if (ProcessCode.ConvertString_Null_To_Empty() != "" && ProcessCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND SR.ProcessCode = '{ProcessCode}'";
            }

            if (OrderNo.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND SR.OrderNo = '{OrderNo}'";
            }

            if (Status.ConvertString_Null_To_Empty() != "" && Status.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $@" AND SR.Status = '{Status}' ";
            }

            query += $"\nORDER BY sr.RoutingSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_PM_SdRouteByOrderNo_MC()
        {
            query = $@"SELECT  
                            '' AS Status
                            , SR.OrderNo
		                    , SR.RoutingSeq
		                    , WO.WorkDate
		                    , WO.EndDate
		                    , SR.PItemCode ProductCode
		                    , CMP.ItemName ProductName

                            , SR.MItemCode ItemCode
                            , CM.ItemName ItemName
                            , CM.Size 
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

                            , SR.ProcessCode
                            , SR.ChildID
                            , SR.ParentID
                            , SR.Status SRStatus

                            , (SELECT TOP 1 LineCode FROM CM_Line cl WHERE cl.CorpId = sr.CorpId AND cl.ProcessCode = sr.ProcessCode) AS LineCode
                            , '' AS MachineCode
                            , 0 AS OrderQty
                            --
                            , '' AS btnDelete

                            , '' AS Remark

                        FROM PM_SdRoute SR
                        LEFT JOIN PM_ProductPlan WO ON SR.CorpId = WO.CorpId AND SR.OrderNo = WO.OrderNo

                        LEFT JOIN CM_Item CMP ON SR.CorpId = CMP.CorpId AND SR.PItemCode = CMP.ItemCode
                        LEFT JOIN CM_Item CM  ON SR.CorpId = CM.CorpId AND SR.MItemCode = cm.ItemCode
                        WHERE SR.CorpID = '{WrGlobal.CorpID}'
                        AND SR.WorkType = '10'

                        ";

            if (ProcessType.ConvertString_Null_To_Empty() != "" && ProcessType.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND SR.ProcessType = '{ProcessType}'";
            }

            if (ProcessCode.ConvertString_Null_To_Empty() != "" && ProcessCode.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $"\nAND SR.ProcessCode = '{ProcessCode}'";
            }

            if (OrderNo.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND SR.OrderNo = '{OrderNo}'";
            }

            if (Status.ConvertString_Null_To_Empty() != "" && Status.ConvertString_Null_To_Empty().ToUpper() != "ALL")
            {
                query += $@" AND SR.Status = '{Status}' ";
            }

            query += $"\nORDER BY sr.RoutingSeq";

            return db.GetDataTable(query);
        }

        public bool Ins_PM_SdRoute()
        {
			query = $@"INSERT INTO PM_SdRoute 
						(
							CorpId
							, OrderNo
							, ChildID
							, ParentID
							, SdOrderNo
							, SdOrderSeq
							, ItemCode
							, RoutingSeq
							, WorkType
							, MItemCode
							, MItemName
							, [Status]
							, OrderQty
							, WorkQty
							, ProcessCode
							, ProcessType
							, CustCode
							, FinalYn
							, SupplyYn
							, ProcessKind
							, CreDt
							, CreID
							, CreIP
						)
						SELECT '{WrGlobal.CorpID}'
								, '{OrderNo}'
								, CB.ChildID
								, CB.ParentID
								, '{SdOrderNo}'
								, '{SdOrderSeq}'
								, CB.ItemCode
								, CB.RoutingSeq
								, CB.WorkType
								, CB.ItemCode
								, CB.ItemName
								, '대기'
								, CB.Qty
								, CB.Qty
								, CB.ProcessCode
								, CB.ProcessType
								, ''
								, CASE 
									WHEN CB.TreeLevel = 0 THEN 'Y'
									ELSE 'N'
								END
								, 'Y'
								, CP.ProcessType
								, GETDATE()
								, '{WrGlobal.LoginID}'
								, HOST_NAME()
						 FROM BM_SdOrderD SOD
						INNER JOIN CM_BOMRev CBM ON SOD.ItemCode = CBM.ItemCode AND CBM.UseYn = 'Y'
						INNER JOIN CM_BOM CB ON CBM.ItemCode = CB.ItemCode AND CBM.Revision = CB.Revision
						LEFT JOIN CM_Process CP ON CB.ProcessCode = CP.ProcessCode
						WHERE SOD.CorpId = '{WrGlobal.CorpID}'
						AND SOD.SdOrderNo = '{SdOrderNo}'
						AND SOD.SdOrderSeq = '{SdOrderSeq}'
						";

			return db.ExecuteQuery(query);
        }

        public bool Upd_PlanQty_ByRoutingSeq()
        {
            query = $@"UPDATE PM_SdRoute
                        SET PlanQty = '{PlanQty}'
                            , Status = '진행'
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND RoutingSeq = '{RoutingSeq}'
                        ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_PlanQty_ByRoutingSeq_KNP()
        {
            query = $@"UPDATE PM_SdRoute
                        SET PlanQty = '{PlanQty}'
                            , Status = '대기'
                            , Remarks = '{Remarks}'
                            , ModDt = {(ModDt.HasValue ? $"'{ModDt.Value:yyyy-MM-dd HH:mm:ss}'" : "NULL")}
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND RoutingSeq = '{RoutingSeq}'
                        ";

            return db.ExecuteQuery(query);
        }

        public bool Del_PlanQty_ByRoutingSeq_KNP()
        {
            query = $@"UPDATE PM_SdRoute
                        SET PlanQty = '{PlanQty}'
                            , Status = ''
                            , Remarks = '{Remarks}'
                            , ModDt = NULL
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND OrderNo = '{OrderNo}'
                        AND RoutingSeq = '{RoutingSeq}'
                        ";

            return db.ExecuteQuery(query);
        }
	}
}
