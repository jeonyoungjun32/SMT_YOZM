using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.Model;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.BM
{
	class BM_SdOrderD
	{

		#region Property
		public string CorpId { get; set; }

		public string SdOrderNo { get; set; }

		public int SdOrderSeq { get; set; }

		public string ShipToCode { get; set; }

		public string ItemCode { get; set; }

		public decimal Qty { get; set; }

		public decimal StockQty { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal Price { get; set; }

		public decimal VatPrice { get; set; }

		public decimal TotalPrice { get; set; }

		public DateTime DeliveryDate { get; set; }

		public string OrderNo { get; set; }

		public string OrderStatus { get; set; }

		public string PlacingStatus { get; set; }

		public string PlacingOutStatus { get; set; }

		public string Remark { get; set; }

		public string VatType { get; set; }

		public int VatRate { get; set; }

		public string State { get; set; }

		//조회용
		public string CustCode { get; set; }
		public string StartDate { get; set; }
		public string EndDate { get; set; }

		#endregion

		Database db = new Database();
		string query = "";

        public DataTable SelectBM_SdOrderD_ByNo(string corpId, string sdOrderNo)
        {
            query = $@"
                        Select '' AS Status
                            , sd.CorpId
                            , sd.SdOrderNo
                            , sd.SdOrderSeq

                            , sd.ItemCode
                            , sd.ItemCode AS ItemName
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A011', cm.UnitCode)) AS UnitCode --품목단위

                            , cm.ItemName AS colItemName
                            , cm.Alias
                            , cm.ItemClass1
                            , cm.ItemClass2
                            , cm.ItemClass3
                            , cm.Size
                            , cm.UnitWeight
                            , cm.Texture
                            , cm.Type
                            --, cm.UnitCode
                            , cm.ColorCode
                            , cm.PackQty

                            , sd.ShipToCode
                            , sd.ShipToCode AS ShipToName
                            , sd.Qty
                            , sd.StockQty
                            , sd.UnitPrice
                            , sd.Price
                            , sd.VatPrice
                            , sd.TotalPrice
                            , sd.DeliveryDate
                            , sd.OrderNo
                            , sd.OrderStatus
                            , sd.PlacingStatus
                            , sd.Remark
                            , sd.VatType
                            , sd.VatRate
                            , sd.State
                            , sd.CreId
                            , sd.CreIP
                            , sd.CreDt
                            , sd.ModId
                            , sd.ModIP
                            , sd.ModDt
                         
                            , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','C010',  sd.VatType)) AS VatTypeTitle --부가세구분
                            , NULL AS colDelete

                        From BM_SdOrderD sd
						Left Join BM_SdOrderM sm on sd.CorpId = sm.CorpId and sd.SdOrderNo = sm.SdOrderNo
                        Left Join CM_Item cm on sd.CorpId = cm.CorpId and sd.ItemCode = cm.ItemCode
                        where sd.CorpId = '{corpId}' and sd.SdOrderNo = '{sdOrderNo}'
                ";

            return db.GetDataTable(query);
        }

		public DataTable SelectBM_SdOrderD(string corpId, string startDt, string endDt, string custCode, string outProgress = "")
		{
            query = $@"SELECT  '' AS Status
						        , sod.SdOrderNo
						        , sod.SdOrderSeq
						        , som.ObtainDate
						        , som.SoldToCode
						        , som.SoldToCode AS SoldToName
						        , sod.ShipToCode
						        , sod.ShipToCode AS ShipToName

						        , som.OrderType
						        , som.VatType
						        , som.VatRate
						        , som.SalesMan
						        , som.OutState
						        , som.HouseID
						        , som.State

						        , sod.ItemCode
						        , sod.ItemCode AS ItemName
						        , cm.ItemName  AS colItemName
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

						        , sod.Qty
						        , sod.UnitPrice
						        , sod.Price
						        , sod.VatPrice
						        , sod.TotalPrice

						        , sod.DeliveryDate
						        , sod.OrderStatus
						        , sod.PlacingStatus
						        , sod.PlacingOutStatus
						        , sod.Remark

                                --출하용
                                , som.SoldToCode AS BillToCode
                                , CONVERT(DATE, GETDATE()) AS OutDate
                                , ISNULL(sd.TotalOutQty, 0) AS TotalOutQty
                                , sod.Qty - ISNULL(sd.TotalOutQty, 0) AS RemainQty
                                --, sod.Qty - ISNULL(sd.TotalOutQty, 0) AS OutQty
                                , CAST(0 AS DECIMAL(18,3)) AS OutQty
                                , cm.HouseID AS HouseCode --제품창고 기본으로 조회

                    FROM BM_SdOrderD sod
                    LEFT JOIN BM_SdOrderM som ON sod.CorpId = som.CorpId AND som.SdOrderNo = sod.SdOrderNo
                    LEFT JOIN CM_Item cm ON sod.CorpId = cm.CorpId AND cm.ItemCode = sod.ItemCode
                    LEFT JOIN CM_Cust cc ON cc.CorpId = cm.CorpId AND cc.CustCode = cm.CustCode
                LEFT JOIN 
                    (
                        SELECT SdOrderNo, SdOrderSeq, SUM(Qty) AS TotalOutQty
                        FROM BM_SalesD 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND SdOrderNo IS NOT NULL
                        GROUP BY SdOrderNo, SdOrderSeq
                    ) AS sd ON sod.SdOrderNo = sd.SdOrderNo AND sod.SdOrderSeq = sd.SdOrderSeq

                    WHERE som.CorpId = '{WrGlobal.CorpID}' 
                    AND som.ObtainDate BETWEEN '{startDt}' AND '{endDt}'";

            if (custCode != "" && custCode.ToUpper() != "ALL")
            {
                query += $" AND som.SoldToCode = '{custCode}'";
            }


            if (outProgress == "N")
            {
                query += $" AND sod.Qty > ISNULL(sd.TotalOutQty, 0)";
            }
            else if (outProgress == "Y")
            {
                query += $" AND sod.Qty <= ISNULL(sd.TotalOutQty, 0)";
            }

            query += " ORDER BY som.ObtainDate, sod.SdOrderNo, sod.SdOrderSeq";

            return db.GetDataTable(query);
        }

		public DataTable SelectBM_SdOrderD(string CustCode, string startDt, string endDt)
		{
			query = $@"SELECT '' AS Status, 
                              som.ObtainDate,
                              som.SoldToCode,
                              som.SoldToCode SoldToName,
                              sod.ShipToCode,
                              sod.ShipToCode ShipToName,
                              som.PONo,
                              som.SalesMan,

                              sod.ItemCode,
                              cm.ItemName,
                              cm.Alias,
                               cm.ItemClass1
                              , cm.ItemClass2
                              , cm.ItemClass3
                              , cm.Size
                              , cm.UnitWeight
                              , cm.Texture
                              , cm.Type
                              , cm.UnitCode
                              , cm.ColorCode

                              , sod.Qty,
                              sod.UnitPrice,
                              sod.Price,
                              sod.VatPrice,
                              sod.TotalPrice,
                              
                              sod.DeliveryDate,
                              sod.Remark,

                              --데이터용
                              sod.SdOrderNo,
                              sod.SdOrderSeq,
                              --(sod.SdOrderNo + '/' + CONVERT(VARCHAR, sod.SdOrderSeq)) AS LotNo,
                              sod.UnitPrice,
                              cm.ExamYn

                              --출하용
                              , som.SoldToCode AS BillToCode
                              , CONVERT(DATE, GETDATE()) AS OutDate
                              , ISNULL(sd.TotalOutQty, 0) AS TotalOutQty
                              , sod.Qty - ISNULL(sd.TotalOutQty, 0) AS RemainQty
                              --, sod.Qty - ISNULL(sd.TotalOutQty, 0) AS OutQty
                              , CAST(0 AS DECIMAL(18,3)) AS OutQty
                              , cm.HouseID AS HouseCode --제품창고 기본으로 조회

                         FROM BM_SdOrderD sod
                    LEFT JOIN BM_SdOrderM som ON sod.CorpId = som.CorpId AND som.SdOrderNo = sod.SdOrderNo
                    LEFT JOIN CM_Item cm ON sod.CorpId = cm.CorpId AND cm.ItemCode = sod.ItemCode
                    LEFT JOIN CM_Cust cc ON cc.CorpId = cm.CorpId AND cc.CustCode = cm.CustCode
                    LEFT JOIN 
                    (
                        SELECT SdOrderNo, SdOrderSeq, SUM(Qty) AS TotalOutQty
                        FROM BM_SalesD 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND SdOrderNo IS NOT NULL
                        GROUP BY SdOrderNo, SdOrderSeq
                    ) AS sd ON sod.SdOrderNo = sd.SdOrderNo AND sod.SdOrderSeq = sd.SdOrderSeq

                        WHERE som.CorpId = '{WrGlobal.CorpID}'
                        AND som.ObtainDate BETWEEN '{startDt}' AND '{endDt}'";

			if (CustCode != "" && CustCode.ToUpper() != "ALL")
			{
				query += $" AND som.SoldToCode = '{CustCode}'";
			}

			query += " ORDER BY som.ObtainDate, sod.SdOrderNo, sod.SdOrderSeq";

			return db.GetDataTable(query);
		}

		public DataTable Select_SdOrderByProduct()
		{
			query = $@"SELECT TOP 0
                            SD.CorpId
                            , SD.SdOrderNo
                            , SD.SdOrderSeq
                            , SD.ItemCode
                            , SD.ShipToCode
                            , SD.Qty OrderQty
                            , SD.StockQty
                            , SD.UnitPrice
                            , SD.Price
                            , SD.VatPrice
                            , SD.TotalPrice
                            , SD.DeliveryDate
                            , SD.OrderNo
                            , SD.OrderStatus
                            , SD.PlacingStatus
                            , SD.Remark
                            , SD.VatType
                            , SD.VatRate
                            , SD.State
                            , SD.CreId
                            , SD.CreIP
                            , SD.CreDt
                            , SD.ModId
                            , SD.ModIP
                            , SD.ModDt
                            
                            , CP.CustAlias CustItemCode
                            , ISNULL(CP.Price, 0) UnitPrice
                            
                            , CM.ItemName
                            , CM.Alias
                            , CM.UnitWeight
                            , CM.Size
                            , CM.ItemClass1
						    , CM.ItemClass2
						    , CM.ItemClass3
						    , CM.UnitCode
						    , CM.PackQty
                            , 0 SalesPrice

                        FROM BM_SdOrderD SD
                        INNER JOIN BM_SdOrderM    SM ON SD.CorpId = SM.CorpId AND SD.SdOrderNo = SM.SdOrderNo
                        LEFT JOIN CM_ItemPrice   CP ON SD.CorpId = CP.CorpId AND SM.SoldToCode = CP.CustCode AND PriceType = '20' -- 20:매출
                        INNER JOIN CM_Item       CM ON SD.CorpId = CM.CorpId AND SD.ItemCode = cm.ItemCode
                        WHERE SD.CorpId = '{WrGlobal.CorpID}'
                        AND SM.SdOrderNo = '{SdOrderNo}'
                        ";

			return db.GetDataTable(query);
		}

		public DataTable SelectBM_SdOrderD_All()
		{
			if (CustCode == "ALL") CustCode = "";
			if (ItemCode == "ALL") ItemCode = "";
			//if (State.ToUpper() == "ALL") State = "";

			query = $@"
                    SET ANSI_WARNINGS OFF
                    SET ARITHIGNORE ON
                    SET ARITHABORT OFF

                    SELECT SD.CorpId
                            , SD.SdOrderNo
                            , SD.SdOrderSeq
                            , SD.ItemCode
                            , SD.ShipToCode
                            
                            , SD.Qty
                            , CEILING(SD.Qty / cm.PackQty) AS OrderBoxQty

							, st.StockQty
                            , CEILING(st.StockQty / cm.PackQty) AS StockBoxQty

							, st.StockQty - SD.Qty AS OverQty
							, CEILING(st.StockQty / cm.PackQty) - CEILING(SD.Qty / cm.PackQty)  AS OverBoxQty

                            , ISNULL(RE.ReturnQty, 0) AS ReturnQty
							-- 반품금액 =  단가 × 반품량 × 1.1 (부가세 10% 추가)
							, ROUND(ISNULL(RE.ReturnQty, 0) * SD.UnitPrice * 1.1, 2) AS ReturnAmount --소수점2자리까지만
                            -- 조정된 총액 = 총액 - 반품금액       
                            , SD.TotalPrice - (ISNULL(RE.ReturnQty, 0) * SD.UnitPrice * 1.1) AS AdjustedTotalPrice
                            , SD.StockQty

                            , SD.UnitPrice
                            , SD.Price
                            , SD.VatPrice
                            , SD.TotalPrice
                            , SD.DeliveryDate
                            , SD.OrderNo
                            , SD.OrderStatus
                            , SD.PlacingStatus
                            , SD.Remark
                            , SD.VatType
                            , SD.VatRate
                            , CASE WHEN SD.State = '출고완료' THEN '판매완료' ELSE SD.State END AS State  -- GJY용 State 변경
                            , SD.CreId
                            , SD.CreIP
                            , SD.CreDt
                            , SD.ModId
                            , SD.ModIP
                            , SD.ModDt

                            , cm.UnitCode	--품목단위
                            , cm.ItemClass1	--품목분류1
                            , cm.ItemClass2	--품목분류2
                            , cm.ItemClass3	--품목분류3

                            , cm.ItemName
                            , cm.PackQty

                            , SM.ObtainDate
                            , SM.DeliveryDate
                            , SM.SalesMan
                            , SM.SoldToCode
                            , EM.Date AS EstimateDate
                        FROM BM_SdOrderD SD
                        INNER JOIN BM_SdOrderM SM ON SD.SdOrderNo = SM.SdOrderNo
                        LEFT JOIN CM_Item CM on SD.CorpId = CM.CorpId and SD.ItemCode = cm.ItemCode
                        LEFT JOIN (
									SELECT SdOrderNo, SdOrderSeq, SUM(Qty) AS ReturnQty
									FROM BM_Return
									WHERE CorpId = '{WrGlobal.CorpID}'
									GROUP BY SdOrderNo, SdOrderSeq
								  ) RE ON SD.SdOrderNo = RE.SdOrderNo
		                LEFT JOIN 
						(
							select ItemCode, SUM(StockQty) AS StockQty
							from StockListView
							GROUP BY ItemCode
						) AS st ON sd.ItemCode = st.ItemCode
                        LEFT JOIN BM_EstimateM EM ON SM.EstimateNo = EM.EstimateNo 
                        WHERE SD.CorpId = '{WrGlobal.CorpID}'
                        AND SM.ObtainDate BETWEEN '{StartDate}' AND '{EndDate}'
                        ";

			if (CustCode != "") query += $"\n AND SM.SoldToCode = '{CustCode}'";
			if (ItemCode != "") query += $"\n AND SD.ItemCode = '{ItemCode}'";
			//if (State != "")
			//{
			//    string stateText = "";
			//    switch (State)
			//    {
			//        case "10": stateText = "대기"; break;
			//        case "20": stateText = "진행"; break;
			//        case "30": stateText = "출고완료"; break;
			//        default: stateText = State; break;
			//    }
			//    query += $"\n AND SD.State = '{stateText}'";
			//}

			query += $"\n ORDER BY SM.ObtainDate DESC";

			return db.GetDataTable(query);
		}
		public DataTable SelectBM_SdOrderD_PM_ProductPlan(string CustCode, string startDt, string endDt)
		{
			query = $@"SELECT '' AS Status, 
                              som.ObtainDate,
                              som.SoldToCode AS CustCode,
                              cc.CustName,
                              som.ShipToCode,
                              cc2.CustName ShipToName,
                              som.PONo,
                              som.SalesMan,
                              sod.ItemCode,
                              sod.ItemCode AS ItemName,
                              cm.Alias,
                              cm.Size,
                              cm.UnitCode,
							  cm.ItemClass1,
							  cm.ItemClass2,
							  cm.ItemClass3,
                              sod.Qty,
                              sod.UnitPrice,
                              sod.Price,
                              sod.VatPrice,
                              sod.TotalPrice,
                              
                              som.DeliveryDate,
                              sod.Remark AS Remark,

                              --데이터용
                              sod.SdOrderNo,
                              sod.SdOrderSeq,
                              --(sod.SdOrderNo + '/' + CONVERT(VARCHAR, sod.SdOrderSeq)) AS LotNo,
                              sod.UnitPrice,
                              cm.ExamYn,
                              cm.UnitWeight

                              --계획용
                              , '' AS OrderNo

                              , CONVERT(DATE, GETDATE()) AS WorkDate
                              , ISNULL(sd.TotalPrepQty, 0) AS TotalOrderQty
                              , sod.Qty - ISNULL(sd.TotalPrepQty, 0) AS RemainQty
                              , sod.Qty - ISNULL(sd.TotalPrepQty, 0) AS OrderQty
                              , '' AS LineCode
                              , '' AS ProcessCode
                              , '' AS MachineCode

                    FROM BM_SdOrderD sod
                    LEFT JOIN BM_SdOrderM som ON sod.CorpId = som.CorpId AND som.SdOrderNo = sod.SdOrderNo
                    LEFT JOIN CM_Item cm ON sod.CorpId = cm.CorpId AND cm.ItemCode = sod.ItemCode
                    LEFT JOIN CM_Cust cc ON cc.CorpId = cm.CorpId AND cc.CustCode = som.SoldToCode
                    LEFT JOIN CM_Cust cc2 ON cc.CorpId = cm.CorpId AND cc2.CustCode = som.ShipToCode
                    LEFT JOIN 
                    (
                        SELECT SdOrderNo, SdOrderSeq, SUM(OrderQty) AS TotalPrepQty
                        FROM PM_ProductPlan 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND SdOrderNo IS NOT NULL
                        GROUP BY SdOrderNo, SdOrderSeq
                    ) AS sd ON sod.SdOrderNo = sd.SdOrderNo AND sod.SdOrderSeq = sd.SdOrderSeq

                        WHERE som.CorpId = '{WrGlobal.CorpID}'
                        AND som.ObtainDate BETWEEN '{startDt}' AND '{endDt}'";

			if (CustCode != "" && CustCode.ToUpper() != "ALL")
			{
				query += $" AND som.SoldToCode = '{CustCode}'";
			}

			query += " ORDER BY ObtainDate DESC";

			return db.GetDataTable(query);
		}

        public DataTable SelectSdOrder_Summary(string startDate, string endDate, string outState)
        {
            query = $@"
                        SELECT 
                        SM.OutState
                      , SD.CorpId
                      , SD.SdOrderNo
                      , SD.SdOrderSeq
                      , SM.SoldToCode
                      , SM.ObtainDate

                      , SD.ItemCode
                      , CM.ItemName
                      , CM.ItemClass1
                      , CM.ItemClass2
                      , CM.ItemClass3
                      , CM.Size
                      , CM.UnitCode
                      , CM.Alias

					 ,  SD.Qty AS Qty
                      , ISNULL(SL.Qty, 0) AS OutQty
                      , SD.Qty - ISNULL(SL.Qty, 0) AS NOutQty
                  FROM BM_SdOrderD SD
                  INNER JOIN BM_SdOrderM SM ON SD.SdOrderNo = SM.SdOrderNo
                  LEFT JOIN CM_Item CM ON SD.CorpId = CM.CorpId AND SD.ItemCode = cm.ItemCode
                   JOIN (
                      SELECT 
                          CorpId,
                          SdOrderNo,
                          SdOrderSeq,
                          SUM(Qty) AS Qty
                      FROM BM_SalesD
                      GROUP BY CorpId, SdOrderNo, SdOrderSeq
                  ) SL ON SD.CorpId = SL.CorpId AND SD.SdOrderNo = SL.SdOrderNo AND SD.SdOrderSeq = SL.SdOrderSeq
                  --LEFT JOIN BM_SalesM SM2 ON SM.SoldToCode = SM2.ShipToCode
                  WHERE SD.CorpId = '{WrGlobal.CorpID}'
                      AND SM.ObtainDate BETWEEN '{startDate}' AND '{endDate}'";

            if (ItemCode != "" && ItemCode != "ALL")
            {
                query += $"\n AND SD.ItemCode = '{ItemCode}'";
            }

            if (!string.IsNullOrEmpty(outState) && outState != "ALL")
                query += $"\n AND SM.OutState = '{outState}'";

            query += @" ORDER BY SM.ObtainDate DESC";

            return db.GetDataTable(query);
        }

        public DataTable SelectOrder_ShortageView(string orderCondition)
        {
            query = $@"

            DECLARE @CorpId nvarchar(30) = '{WrGlobal.CorpID}';
            
            -- 선택한 수주의 자재 소요량 계산
            WITH MaterialDemand AS
            (
                SELECT
                      B.MaterialCode AS MItemCode
                    , SUM
                      (
                            CAST(O.Qty AS DECIMAL(18,4))
                            * B.Qty
                            / CASE
                                WHEN ISNULL(B.CalcQty, 0) = 0 THEN 1
                                ELSE B.CalcQty
                              END
                      ) AS TotalRequiredQty
                FROM BM_SdOrderD AS O
                CROSS APPLY FN_Bom(@CorpId, O.ItemCode) AS B
                WHERE O.CorpId = @CorpId
                  AND EXISTS
                  (
                        SELECT 1
                        FROM
                        (
                            VALUES
                                {orderCondition}
                        ) AS V(SdOrderNo, SdOrderSeq)
                        WHERE V.SdOrderNo = O.SdOrderNo
                          AND V.SdOrderSeq = O.SdOrderSeq
                  )
                GROUP BY
                    B.MaterialCode
            ),
            
            -- 품목별 현재고
            CurrentStock AS
            (
                SELECT 
                     ItemCode
                    , SUM(StockQty) AS TotalStockQty
                FROM StockListView
            	WHERE CorpId = @CorpId
                GROUP BY
                    ItemCode
            )
            
            SELECT
                  dbo.FN_Get_CateItem_Title(@CorpId, 'A010', CM.ItemType) AS MaterialTypeName
                , D.MItemCode AS ItemCode
                , CM.ItemName
                , CM.Alias
                , CM.ItemClass1
                , CM.ItemClass2
                , CM.ItemClass3
                , CM.Size
                , CM.UnitWeight
                , CM.Texture
                , CM.Type
                , CM.UnitCode
            
                -- 현재고
                , ISNULL(S.TotalStockQty, 0) AS CurStockQty
            
                -- 총 소요량
                , D.TotalRequiredQty
            
                -- 예상재고
                , ISNULL(S.TotalStockQty, 0) - D.TotalRequiredQty AS ExpectStockQty
            
                -- 안전재고
                , ISNULL(CM.SaftyQty, 0) AS SaftyQty
            
                -- 과부족
                , ISNULL(S.TotalStockQty, 0)
                  - D.TotalRequiredQty
                  - ISNULL(CM.SaftyQty, 0) AS DiffQty
            
            FROM MaterialDemand D
            LEFT JOIN CurrentStock S ON D.MItemCode = S.ItemCode
            LEFT JOIN CM_Item CM ON CM.CorpId = @CorpId AND D.MItemCode = CM.ItemCode
            WHERE D.MItemCode IS NOT NULL
            ORDER BY
                  CM.ItemType
                , CM.ItemName;
                ";

            return db.GetDataTable(query);
        }

        public bool GetSdOrderD_OrderStatus()
		{
            bool result = false;

            query = $@"
                SELECT top 1 * 
                FROM BM_SdOrderD 
                WHERE CorpID = '{WrGlobal.CorpID}' 
                AND SdOrderNo = '{SdOrderNo}' 
                AND OrderStatus = '{OrderStatus}'
            ";

            DataRow dr = db.GetDataRow(query);

            if(dr!= null)
			{
                result = true;
			}

            return result;
        }

		public bool InsUpd_BM_SdOrderD()
		{
			query = $@"

                            IF EXISTS
                            (
	                            SELECT SdOrderSeq
	                            FROM BM_SdOrderD
	                            WHERE CorpId = '{CorpId}'
	                            AND SdOrderNo = '{SdOrderNo}'
	                            AND SdOrderSeq = '{SdOrderSeq}'
                            )
                            BEGIN


                                    UPDATE BM_SdOrderD
                                    SET ShipToCode = '{ShipToCode}'
                                        , ItemCode = '{ItemCode}'
                                        , Qty            = '{Qty}'
                                        , StockQty       = '{StockQty}'
                                        , UnitPrice      = '{UnitPrice}'
                                        , Price          = '{Price}'
                                        , VatPrice       = '{VatPrice}'
                                        , TotalPrice     = '{TotalPrice}'
                                        , DeliveryDate   = '{DeliveryDate.ToString("d")}'
                                        , OrderStatus    = '{OrderStatus}'
                                        , PlacingStatus  = '{PlacingStatus}'
                                        , Remark         = '{Remark}'
                                        , VatType        = '{VatType}'
                                        , VatRate        = '{VatRate}'
                                        , State          = '{State}'
                                        , ModId = '{WrGlobal.LoginID}'
                                        , ModIP = '{WrGlobal.ClientIpAddress}'
                                        , ModDt = GETDATE()

                                    WHERE CorpId = '{CorpId}'
                                    AND SdOrderNo    = '{SdOrderNo}'
                                    AND SdOrderSeq   = '{SdOrderSeq}'

                            END

                            ELSE
                            BEGIN

                            INSERT INTO BM_SdOrderD
                            (
                                  CorpId
                                , SdOrderNo
                                , SdOrderSeq
                                , ShipToCode
                                , ItemCode
                                , Qty
                                , StockQty
                                , UnitPrice
                                , Price
                                , VatPrice
                                , TotalPrice
                                , DeliveryDate
                                , OrderNo
                                , OrderStatus
                                , PlacingStatus
                                , Remark
                                , VatType
                                , VatRate
                                , State
                                , CreId
                                , CreIP
                                , CreDt

                            )
                            VALUES
                            (
                                  '{CorpId}'
                                , '{SdOrderNo}'
                                , '{SdOrderSeq}'
                                , '{ShipToCode}'
                                , '{ItemCode}'
                                , '{Qty}'
                                , '{StockQty}'
                                , '{UnitPrice}'
                                , '{Price}'
                                , '{VatPrice}'
                                , '{TotalPrice}'
                                , '{DeliveryDate.ToString("d")}'
                                , '{OrderNo}'
                                , '{OrderStatus}'
                                , '{PlacingStatus}'
                                , '{Remark}'
                                , '{VatType}'
                                , '{VatRate}'
                                , '{State}'
                                , '{WrGlobal.LoginID}'
                                , '{WrGlobal.ClientIpAddress}'
                                , GETDATE()

                            )
                            END

            ";

			return db.ExecuteQuery(query);
		}

		public bool Ins_BM_SdOrderD()
		{
			query = $@"

                        INSERT INTO BM_SdOrderD
                        (
                              CorpId
                            , SdOrderNo
                            , SdOrderSeq
                            , ItemCode
                            , ShipToCode
                            , Qty
                            , StockQty
                            , UnitPrice
                            , Price
                            , VatPrice
                            , TotalPrice
                            , DeliveryDate
                            , OrderNo
                            , OrderStatus
                            , PlacingStatus
                            , Remark
                            , VatType
                            , VatRate
                            , State
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                              '{CorpId}'
                            , '{SdOrderNo}'
                            , '{SdOrderSeq}'
                            , '{ItemCode}'
                            , '{ShipToCode}'
                            , '{Qty}'
                            , '{StockQty}'
                            , '{UnitPrice}'
                            , '{Price}'
                            , '{VatPrice}'
                            , '{TotalPrice}'
                            , '{DeliveryDate.ToString("d")}'
                            , '{OrderNo}'
                            , '{OrderStatus}'
                            , '{PlacingStatus}'
                            , '{Remark}'
                            , '{VatType}'
                            , '{VatRate}'
                            , '{State}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientIpAddress}'
                            , GETDATE()

                        )

            ";

			return db.ExecuteQuery(query);
		}

		public bool Upd_BM_SdOrderD()
		{
			query = $@"
                        UPDATE BM_SdOrderD
                                SET ItemCode = '{ItemCode}'
                                    , Qty            = '{Qty}'
                                    , StockQty       = '{StockQty}'
                                    , UnitPrice      = '{UnitPrice}'
                                    , Price          = '{Price}'
                                    , VatPrice       = '{VatPrice}'
                                    , TotalPrice     = '{TotalPrice}'
                                    , DeliveryDate   = '{DeliveryDate.ToString("d")}'
                                    , OrderStatus    = '{OrderStatus}'
                                    , PlacingStatus  = '{PlacingStatus}'
                                    , Remark         = '{Remark}'
                                    , VatType        = '{VatType}'
                                    , VatRate        = '{VatRate}'
                                    , State          = '{State}'
                                    , ModId = '{WrGlobal.LoginID}'
                                    , ModIP = '{WrGlobal.ClientIpAddress}'
                                    , ModDt = GETDATE()
                        WHERE CorpId = '{CorpId}'
                        AND SdOrderNo    = '{SdOrderNo}'
                        AND SdOrderSeq   = '{SdOrderSeq}'
            ";

			return db.ExecuteQuery(query);
		}

		public bool Upd_BM_SdOrderDQtyPrice()
		{
			query = $@"

UPDATE BM_SdOrderD
SET  Qty            = '{Qty}'
, UnitPrice      = '{UnitPrice}'
, Price          = '{Price}'
, VatPrice       = '{VatPrice}'
, TotalPrice     = '{TotalPrice}'
, Remark         = '{Remark}'
, ModId = '{WrGlobal.LoginID}'
, ModIP = '{WrGlobal.ClientIpAddress}'
, ModDt = GETDATE()
WHERE CorpId = '{CorpId}'
AND SdOrderNo    = '{SdOrderNo}'
AND SdOrderSeq   = '{SdOrderSeq}'
            ";


			return db.ExecuteQuery(query);
		}
        public bool Upd_OrderStatus_BySdOrderNo()
        {
            query = $@"
                UPDATE BM_SdOrderD
                SET  OrderStatus = '{OrderStatus}'
                WHERE CorpId = '{CorpId}'
                AND SdOrderNo    = '{SdOrderNo}'
            ";


            return db.ExecuteQuery(query);
        }


        public bool Upd_StateOrder()
		{
			query = $@"
                            --수주테이블 계획상태 업데이트
	                    EXEC Sales_BM_SdOrderDOrderStatus '{WrGlobal.CorpID}', '{SdOrderNo}', '{SdOrderSeq}'
                        ";
			return db.ExecuteQuery(query);
		}

		public bool Del_BM_SdOrderD()
		{
			query = $@"
                        DELETE BM_SdOrderD
                        WHERE CORPID = '{CorpId}'
                        AND SdOrderNo = '{SdOrderNo}'
                        AND SdOrderSeq = '{SdOrderSeq}'
                    ";

			return db.ExecuteQuery(query);
		}
	}
}
