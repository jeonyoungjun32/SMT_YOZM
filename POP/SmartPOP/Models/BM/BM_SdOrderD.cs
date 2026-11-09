using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.BM
{
    class BM_SdOrderD
    {

        #region Property
        public string CorpId { get; set; }

        public string SdOrderNo { get; set; }

        public int SdOrderSeq { get; set; }

        public string MaterialCode { get; set; }

        public string Size { get; set; }

        public string UnitCode { get; set; }

        public decimal Qty { get; set; }

        public decimal StockQty { get; set; }

        public decimal ReqQty { get; set; }

        public decimal ProQty { get; set; }

        public DateTime OutDate { get; set; }

        public decimal OutQty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Price { get; set; }

        public decimal VatPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string OrderNo { get; set; }

        public string OrderStatus { get; set; }

        public string PlacingStatus { get; set; }

        public string Remark { get; set; }

        public string VatType { get; set; }

        public int VatRate { get; set; }

        public string State { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime ModDt { get; set; }

        public string CustCode { get; set; }
        public string ShipToCode { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string SalesUser { get; set; }

        public string _SearchStart { get; set; }
        public string _SearchEnd { get; set; }

        public string SearchOption { get; set; }
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
                            , sd.MaterialCode
                            , sd.ShipToCode
                            , sd.Size
                            , sd.UnitCode
                            , sd.Qty
                            , sd.StockQty
                            , sd.ReqQty
                            , sd.ProQty
                            , sd.OutDate
                            , sd.OutQty
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
                            , c2.Title VatTypeTitle
                            , cm.MaterialName
                            , c1.Title UnitName

                        From BM_SdOrderD sd
                        Left Join CmCateItem c2 on c2.Class = 'C010' and c2.CorpId = sd.CorpId and c2.ItemCode = sd.VatType		--부가세코드
                        Left Join CM_Item cm on sd.CorpId = cm.CorpId and sd.MaterialCode = cm.MaterialCode
                        Left Join CmCateItem c1 on c1.CLASS = 'A011' and c1.ItemCode = sd.UnitCode	--단위코드
                        where sd.CorpId = '{corpId}' and sd.SdOrderNo = '{sdOrderNo}'
                ";

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
                              , sod.Qty - ISNULL(sd.TotalOutQty, 0) AS OutQty
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
                query += $" AND cm.CustCode = '{CustCode}'";
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
                                        , MaterialCode = '{MaterialCode}'
                                        , Size           = '{Size}'
                                        , UnitCode       = '{UnitCode}'
                                        , Qty            = '{Qty}'
                                        , StockQty       = '{StockQty}'
                                        , ReqQty         = '{ReqQty}'
                                        , ProQty         = '{ProQty}'
                                        , OutQty         = '{OutQty}'
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
                                , MaterialCode
                                , Size
                                , UnitCode
                                , Qty
                                , StockQty
                                , ReqQty
                                , ProQty
                                , OutQty
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
                                , '{MaterialCode}'
                                , '{Size}'
                                , '{UnitCode}'
                                , '{Qty}'
                                , '{StockQty}'
                                , '{ReqQty}'
                                , '{ProQty}'
                                , '{OutQty}'
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
                            , MaterialCode
                            , ShipToCode
                            , Size
                            , UnitCode
                            , Qty
                            , StockQty
                            , ReqQty
                            , ProQty
                            , OutDate
                            , OutQty
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
                            , '{MaterialCode}'
                            , '{ShipToCode}'
                            , '{Size}'
                            , '{UnitCode}'
                            , '{Qty}'
                            , '{StockQty}'
                            , '{ReqQty}'
                            , '{ProQty}'
                            , Null
                            , '{OutQty}'
                            , '{UnitPrice}'
                            , '{Price}'
                            , '{VatPrice}'
                            , '{TotalPrice}'
                            , Null
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
SET MaterialCode = '{MaterialCode}'
, Size           = '{Size}'
, UnitCode       = '{UnitCode}'
, Qty            = '{Qty}'
, StockQty       = '{StockQty}'
, ReqQty         = '{ReqQty}'
, ProQty         = '{ProQty}'
, OutDate        = '{OutDate}'
, OutQty         = '{OutQty}'
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
SdOrderNo    = '{SdOrderNo}'
SdOrderSeq   = '{SdOrderSeq}'
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

        public DataTable SelectBM_SdOrderD_ByNo_HS()
        {
            query = $@"SELECT TOP 0
                            SD.CorpId
                            , SD.SdOrderNo
                            , SD.SdOrderSeq
                            , SD.MaterialCode
                            , SD.ShipToCode
                            , SD.Size
                            , SD.UnitCode
                            , SD.Qty OrderQty
                            , SD.StockQty
                            , SD.ReqQty
                            , SD.ProQty
                            , SD.OutDate
                            , SD.OutQty
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
                            , ISNULL(CP.Price, 0) SalesPrice
                            
                            , CM.MaterialName
                        FROM BM_SdOrderD SD
                        INNER JOIN SdOrderMaster    SM ON SD.CorpId = SM.CorpId AND SD.SdOrderNo = SM.SdOrderNo
                        LEFT JOIN CM_ItemPrice   CP ON SD.CorpId = CP.CorpId AND SM.SoldToCode = CP.CustCode AND PriceType = '20' -- 20:매출
                        INNER JOIN CM_Item       CM ON SD.CorpId = CM.CorpId AND SD.MaterialCode = CM.MaterialCode
                        WHERE SD.CorpId = '{WrGlobal.CorpID}'
                        AND SM.SdOrderNo = '{SdOrderNo}'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Select_BM_SdOrderD_NotIssued()
        {
            if (CustCode.ToUpper() == "ALL") CustCode = "";

            query = $@"SELECT DISTINCT
                                -- BM_SdOrderD
                            SOD.CorpId
                            , SOD.SdOrderNo
                            , SOD.SdOrderSeq
                            , SOD.MaterialCode
                            , SOD.ShipToCode
                            , SOD.Size
                            , SOD.UnitCode
                            , SOD.Qty
                            , SOD.StockQty
                            , SOD.ReqQty
                            , SOD.ProQty
                            , SOD.OutDate
                            --, SOD.OutQty
                            , SOD.UnitPrice
                            , SOD.Price
                            , SOD.VatPrice
                            , SOD.TotalPrice
                            , SOD.DeliveryDate
                            , SOD.OrderNo
                            , SOD.OrderStatus
                            , SOD.PlacingStatus
                            , SOD.Remark
                            , SOD.VatType
                            , SOD.VatRate
                            , SOD.State
                            , SOD.CreId
                            , SOD.CreIP
                            , SOD.CreDt
                            , SOD.ModId
                            , SOD.ModIP
                            , SOD.ModDt
                
                            -- SdOrderMaster
                            , SM.SoldToCode
                            , SM.SalesMan

                            , A.OutQty
                        FROM BM_SdOrderD SOD
                        INNER JOIN SdOrderMaster SM ON SOD.SdOrderNo = SM.SdOrderNo
                        LEFT JOIN (
						        SELECT SdOrderNo
								        , SdOrderSeq
								        , SUM(Qty) OutQty
						        FROM BM_SalesD
						        GROUP BY SdOrderNo, SdOrderSeq
					        ) A ON A.SdOrderNo = SOD.SdOrderNo AND A.SdOrderSeq = SOD.SdOrderSeq
                        LEFT JOIN BM_SalesD SD ON SD.SdOrderNo = SOD.SdOrderNo AND SD.SdOrderSeq = SOD.SdOrderSeq
                        LEFT JOIN TaxInvoiceDetail TI  ON TI.JobNo = SD.SalesNo AND TI.JobSeq = SD.SalesSeq
                        WHERE SOD.CorpID = '{WrGlobal.CorpID}'";

            if (StartDate != null && EndDate != null) query += $"\n AND SM.ObtainDate BETWEEN '{StartDate}' AND '{EndDate}'";

            if (CustCode != "") query += $"\n AND SM.SoldToCode = '{CustCode}'";

            query += $@"
                        -- 출고되어있고 계산서 안떼어진 항목만 나오도록
                        AND TI.InvoiceNo IS NULL	
                        AND SD.SalesNo IS NOT NULL
                    ";
            return db.GetDataTable(query);
        }

        public DataTable SelectBM_SdOrderD_All()
        {
            if (SalesUser == "All") SalesUser = "";
            if (CustCode == "All") CustCode = "";
            if (MaterialCode == "All") MaterialCode = "";

            query = $@"SELECT SD.CorpId
                            , SD.SdOrderNo
                            , SD.SdOrderSeq
                            , SD.MaterialCode
                            , SD.ShipToCode
                            , SD.Size
                            , SD.UnitCode
                            , SD.Qty
                            , ISNULL(RE.ReturnQty, 0) AS ReturnQty
							-- 반품금액 =  단가 × 반품량 × 1.1 (부가세 10% 추가)
							, ROUND(ISNULL(RE.ReturnQty, 0) * SD.UnitPrice * 1.1, 2) AS ReturnAmount --소수점2자리까지만
                            -- 조정된 총액 = 총액 - 반품금액       
                            , SD.TotalPrice - (ISNULL(RE.ReturnQty, 0) * SD.UnitPrice * 1.1) AS AdjustedTotalPrice
                            , SD.StockQty
                            , SD.ReqQty
                            , SD.ProQty
                            , SD.OutDate
                            , SD.OutQty
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

                            , cm.MaterialName

                            , SM.ObtainDate
                            , SM.DeliveryDate
                            , SM.SalesMan
                            , SM.SoldToCode
                        FROM BM_SdOrderD SD
                        INNER JOIN SdOrderMaster SM ON SD.SdOrderNo = SM.SdOrderNo
                        LEFT JOIN CM_Item CM on SD.CorpId = CM.CorpId and SD.MaterialCode = CM.MaterialCode
                        LEFT JOIN (
									SELECT SdOrderNo, SdOrderSeq, SUM(Qty) AS ReturnQty
									FROM ReturnExchange
									WHERE CorpId = '{WrGlobal.CorpID}'
									GROUP BY SdOrderNo, SdOrderSeq
								  ) RE ON SD.SdOrderNo = RE.SdOrderNo
                        WHERE SD.CorpId = '{WrGlobal.CorpID}'
                        AND SM.ObtainDate BETWEEN '{StartDate}' AND '{EndDate}'
                        ";

            if (SalesUser != "") query += $"\n AND SM.SalesMan = '{SalesUser}'";
            if (CustCode != "") query += $"\n AND SM.SoldToCode = '{CustCode}'";
            if (MaterialCode != "") query += $"\n AND SD.MaterialCode = '{MaterialCode}'";

            query += $"\n ORDER BY SM.ObtainDate DESC";

            return db.GetDataTable(query);
        }


        public DataTable Select_NonPaymentList()
        {
            if (SalesUser == "All") SalesUser = "";
            if (CustCode == "All") CustCode = "";
            if (MaterialCode == "All") MaterialCode = "";

            query = $@"SELECT SOD.SdOrderNo
		                        , SOD.SdOrderSeq
		                        , SOD.MaterialCode
		                        , SOD.Qty										-- 주문량
		                        , SOD.Qty - ISNULL(A.ReqQty, 0) + ISNULL(A.BalanceQty, 0)  NonPayQty		-- 미납량 (주문량 - 요청수량 + 미출고수량)
		                        , ISNULL(C.StockQty, 0)	StockQty				-- 재고량
		                        , ISNULL(A.ReqQty, 0) ReqQty					-- 출고지시량
		                        , SOD.Qty - ISNULL(B.OutQty, 0) BalanceQty		-- 출고 후 미납 (주문량 - 출고량)
		                        , A.ReqDate										-- 출고요청일
		                        , SOM.SoldToCode								-- 주문자
		                        , SOD.ShipToCode								-- 출고처
		                        , ISNULL(SOD.UnitPrice, 0) UnitPrice			-- 단가
		                        , ISNULL(SOD.Price, 0) Price					-- 공급가
		                        , 1	AssignmentRate								-- 과제율 (추후에 한성에서 정보 받아야함)
		                        , ISNULL(SOD.TotalPrice, 0) TotalPrice			-- 합계 (공급가 X 과제율)
		                        , SOM.DeliveryDate								-- 납기일
                                
                                , SOM.SalesMan
                        FROM BM_SdOrderD SOD
                        INNER JOIN SdOrderMaster SOM ON SOD.SdOrderNo = SOM.SdOrderNo
                        LEFT JOIN (
									SELECT SdOrderNo
										, SdOrderSeq
										, SUM(BalanceQty) BalanceQty	-- 요청수량
										, ReqDate
										, SUM(BalanceQty) AS ReqQty     -- ReqQty 
									FROM RequestSalesOrder
									GROUP BY SdOrderNo, SdOrderSeq, ReqDate
									) AS A ON SOD.SdOrderNo = A.SdOrderNo AND SOD.SdOrderSeq = A.SdOrderSeq

                        LEFT JOIN (
			                        SELECT SdOrderNo
					                        , SdOrderSeq
					                        , SUM(Qty) OutQty	-- 출고수량
			                        FROM BM_SalesD
			                        GROUP BY SdOrderNo, SdOrderSeq
			                        ) AS B ON SOD.SdOrderNo = B.SdOrderNo AND SOd.SdOrderSeq = B.SdOrderSeq
                        LEFT JOIN (
			                        SELECT ProductCode
					                        , SUM(StockQty) StockQty	-- 재고수량
			                        FROM StockListView
			                        GROUP BY ProductCode
			                        ) AS C ON SOD.MaterialCode = C.ProductCode
                        WHERE 1=1
                ";

            if (SalesUser != "") query += $"\n AND SOM.SalesMan = '{SalesUser}'";
            if (CustCode != "") query += $"\n AND SOM.SoldToCode = '{CustCode}'";
            if (MaterialCode != "") query += $"\n AND SOD.MaterialCode = '{MaterialCode}'";
            if (SdOrderNo != "" && SdOrderNo != null) query += $"\n AND SOD.SdOrderNo = '{SdOrderNo}'";
            if (SdOrderSeq != 0) query += $"\n AND SOD.SdOrderSeq = '{SdOrderSeq}'";

            return db.GetDataTable(query);
        }

        public bool Upd_PM_ProductPlanState()
        {
            query = $@"
                        UPDATE BM_SdOrderD
                        SET OrderStatus = '계획확정'
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND SdOrderNo = '{SdOrderNo}'
                        AND SdOrderSeq = '{SdOrderSeq}'
                        ";

            return db.ExecuteQuery(query);
        }
    }
}
