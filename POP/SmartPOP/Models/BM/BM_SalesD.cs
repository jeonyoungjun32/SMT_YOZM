using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.BM
{
    class BM_SalesD
    {

        #region Property
        public string CorpId { get; set; }
        public string SalesNo { get; set; }
        public int SalesSeq { get; set; }
        public string ProductCode { get; set; }
        public float UnitWeight { get; set; }
        public string UnitCode { get; set; }
        public float Qty { get; set; }
        public float UnitPrice { get; set; }
        public float Price { get; set; }
        public float VatPrice { get; set; }
        public float TotalPrice { get; set; }
        public float WonAmount { get; set; }
        public string VatType { get; set; }
        public int VatRate { get; set; }
        public string ReqType { get; set; }
        public string ReqNo { get; set; }
        public int ReqSeq { get; set; }
        public string SdOrderNo { get; set; }
        public int SdOrderSeq { get; set; }
        public string HouseCode { get; set; }
        public string HouseName { get; set; }
        public string ContainerNo { get; set; }
        public string BarCodeNo { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string DeliveryNo { get; set; }
        public string CarType { get; set; }
        public string CarNo { get; set; }
        public string Driver { get; set; }
        public string DeliveryDate { get; set; }
        public float DeliveryUnitPrice { get; set; }
        public float DeliveryGravity { get; set; }
        public float DeliveryTotalPrice { get; set; }
        public string DeliveryCompany { get; set; }
        public string DeliveryArea { get; set; }
        public string DeliveryType { get; set; }
        public string Distance { get; set; }
        public string Via { get; set; }
        public string Telphone { get; set; }
        public string DeliveryRemark { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public string CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public string ModDt { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ShipToCode { get; set; }
        public string CustCode { get; set; }
        public string SalesMan { get; set; }
        public string MaetrialCode { get; set; }
        public string stDate { get; set; }
        public string endDate { get; set; }

        public string InHouseCode { get; set; }

        public string PublicationYn { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_BM_SalesD(string OutFrom, string OutTo, string outType = "")
        {
            //출고등록(Frm_BN_ProductOut에서 출고조회시 사용하는 Query
            query = $@" SELECT 
                            '' AS Status
                          , sd.SalesNo
                          , sd.SalesSeq
                          , sm.SalesDate	
                          , sm.ShipDate	
                    
                          , sm.BillToCode
                          , c.CustName AS BillToName 
                          , c.ExchangeType
                          
                          , sm.ShipToCode
                          , cd.CustName AS ShipToName

                          , sm.CurrencyType
                          
                          , sd.ItemCode 
                          , Item.ItemName ProductName

                          , sd.ItemCode MaterialCode
                          , Item.ItemName 
                          , Item.Alias
                          , Item.ItemClass1
                          , Item.ItemClass2
                          , Item.ItemClass3
                          , Item.Size
                          , Item.UnitWeight
                          , Item.Texture
                          , Item.Type
                          , Item.UnitCode
                          , Item.ColorCode
                    
                          --, ItemPrice.Price
                          --, ItemPrice.Cust_Alias
                    
                          , sd.Qty
                          , sd.UnitCode
                          , sd.UnitPrice
                          , sd.Price
                          , sd.VatPrice
                          , sd.TotalPrice
                          , sm.SalesMan
                          , sm.WriteDate
                          , sm.WriteID
                          , sm.WriteName
                          , sd.Remark
                          , sd.HouseCode
                    
                          , sd.SdOrderNo 
                          , sd.SdOrderSeq 
                    
                          , sd.CreId
                          , sd.CreIP
                          , sd.CreDt   
                    	  , sd.ModId
                          , sd.ModIP
                          , sd.ModDt
                    
                          --수주
                          , som.PONo
                    
                    FROM BM_SalesD sd WITH(NOLOCK) 
                    LEFT JOIN BM_SalesM sm WITH(NOLOCK) ON sd.CorpId = sm.CorpId AND sd.SalesNo = sm.SalesNo
                    LEFT JOIN CM_Item Item WITH(NOLOCK) ON sd.CorpId = Item.CorpId AND sd.ItemCode = Item.ItemCode
                    LEFT JOIN CM_Cust             c ON sd.CorpId = c.CorpId AND c.CustCode = sm.BillToCode
                    LEFT JOIN CM_Cust            cd ON sd.CorpId = cd.CorpId AND cd.CustCode = sm.ShipToCode
                    LEFT JOIN BM_SdOrderM som WITH(NOLOCK) ON sd.CorpId = som.CorpId AND sd.SdOrderNo = som.SdOrderNo 
                        WHERE sd.CorpId = '{WrGlobal.CorpID}'
						 AND sm.SalesDate BETWEEN '{OutFrom}' AND '{OutTo}' 
                         AND sd.ReqType = '출하출고'
                          ";


            if (WrGlobal.ProcOption["BN"]["Direct_Cust_YN"] == "Y")
            {
                if (ShipToCode != "" && ShipToCode != null && ShipToCode.ToUpper() != "ALL")
                {
                    query += $"\nAND sm.ShipToCode = {ShipToCode}";
                }
            }

            if (CustCode != "" && CustCode != null && CustCode.ToUpper() != "ALL")
            {
                query += $"\nAND sm.BillToCode = '{CustCode}'";
            }

            if (ProductCode != "" && ProductCode != null && ProductCode.ToUpper() != "ALL")
            {
                query += $"\nAND sd.ItemCode = '{ProductCode}'";
            }

            if(outType != "")
			{
                query += $"\nAND sm.OutType = '{outType}'";
			}

            query += $"\nORDER BY sd.SalesNo, sd.SalesSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_Sales_ExamRst(string startDate, string EndDate, string[] Item_Div = null, string exam_Div = "")
        {
            query = $@"SELECT DISTINCT
                              sd.SalesNo AS RefNo
                            , sd.SalesSeq AS RefSeq
                            , CONVERT(DATE, sm.SalesDate) AS SalesDate

                            , sm.BillToCode
                            , bc.CustName BillToName
                            , sm.ShipToCode
                            , bcd.CustName AS ShipToName

                            , sd.ItemCode
                            , bi.Alias
                            , bi.ItemName 
                            , bi.Size
                            , bi.UnitCode
                            , bi.ItemClass1
                            , bi.ItemClass2
                            , bi.ItemClass3
                            
                            , sd.Qty AS ExamQty
                            , sd.Remark
                            , sd.CreId
                            , sd.CreIP
                            , sd.CreDt
                            , sd.ModId
                            , sd.ModIP
                            , sd.ModDt

                            , sd.SdOrderNo
                            , sd.SdOrderSeq
                            
                            --개발용
                            , IIF(qer.RefNo IS NULL, 'N', 'Y') AS ExamYn --검사 여부Yn

                         FROM BM_SalesD sd
                    LEFT JOIN BM_SalesM   sm  ON sd.CorpId = sm.CorpId AND sm.SalesNo       = sd.SalesNo
                    LEFT JOIN CM_Cust   bc  ON sd.CorpId = bc.CorpId AND bc.CustCode       = sm.BillToCode
                    LEFT JOIN CM_Cust   bcd ON sd.CorpId = bcd.CorpId AND bcd.CustCode      = sm.ShipToCode
                    LEFT JOIN CM_Item    bi  ON sd.CorpId = bi.CorpId AND bi.ItemCode       = sd.ItemCode
                    LEFT JOIN QM_ExamResult qer ON sd.CorpId = qer.CorpId AND qer.RefNo       = sd.SalesNo
                            AND qer.RefSeq   = sd.SalesSeq AND qer.ExamDivCd = IIF('{exam_Div}' = '', qer.ExamDivCd, '{exam_Div}')
                    WHERE sd.CorpId = '{WrGlobal.CorpID}'
                        AND sd.ReqType = '출하출고'
            ";


            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND bi.ItemType IN(";
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

            if (SalesNo != "" && SalesSeq != 0)
            {
                query += $"\nAND sd.SalesNo = {SalesNo.ConvertStr_Empty_To_Null()}";
                query += $"\nAND sd.SalesSeq = {SalesSeq.ConvertInt_Empty_To_Null()}";
            }
            else
            {
                query += $"AND sm.SalesDate BETWEEN '{startDate}' AND '{EndDate}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_TaxSalesList()
        {
            if (CustCode == "All") CustCode = "";
            if (ProductCode == "All") ProductCode = "";
            if (SalesMan == "All") SalesMan = "";

            query = $@"SELECT SOM.SoldToCode 
		                        , TIM.InvoiceNo	
		                        , SM.ShipToCode
		                        , SD.ProductCode MaterialCode
                                , CM.MaterialName
		                        , SOM.SalesMan
		                        , ISNULL(SUM(TID.Qty), 0) Qty
		                        , TID.UnitPrice
		                        , TID.Price
                                , TID.FinalPrice
		                        , TIM.refDt
                        FROM BM_SalesD SD
                        INNER JOIN BM_SalesM SM ON SD.SalesNo = SM.SalesNo
                        INNER JOIN TaxInvoiceDetail TID ON TID.JobNo = SD.SalesNo AND TID.JobSeq = SD.SalesSeq
                        INNER JOIN TaxInvoiceMaster TIM ON TID.InvoiceNo = TIM.InvoiceNo
                        INNER JOIN SdOrderMaster SOM ON SD.SdOrderNo = SOM.SdOrderNo
                        LEFT JOIN CM_Item CM ON SD.Productcode = CM.MaterialCode
                        WHERE SD.CorpId = '{WrGlobal.CorpID}'
                        ";

            if (CustCode != "") query += $"\n AND SOM.SoldToCode = '{CustCode}'";
            if (ProductCode != "") query += $"\n AND SD.ProductCode = '{ProductCode}'";
            if (SalesMan != "") query += $"\n AND SOM.SalesMan = '{SalesMan}'";

            query += $"\n GROUP BY SOM.SoldToCode, TIM.InvoiceNo, SM.ShipToCode, SD.ProductCode, CM.MaterialName, SOM.SalesMan, TID.UnitPrice, TID.Price, TID.FinalPrice, TIM.refDt";
            return db.GetDataTable(query);
        }

        public DataTable Select_BM_SalesD_BySalesNo()
        {
            query = $@"
                        SELECT 
                                  sd.SalesNo
		                        , sd.SalesSeq
		                        
		                        , sd.ProductCode
                                , cm.MaterialName ProductName
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

		                        , sd.Qty
		                        , sd.UnitPrice
		                        , sd.Price
		                        , sd.VatPrice
		                        , sd.TotalPrice
		                        , sd.HouseCode
		                        , sd.UnitCode
		                        , sd.Remark
		                        , sd.CreID
		                        , sd.CreIP
		                        , sd.CreDt
		                        , sd.ModID
		                        , sd.ModIP
		                        , sd.ModDt

		                        --Master
		                        , sm.SalesDate
		                        , sm.ShipToCode
		                        , sm.BillToCode

		                        , '' AS Del

                        FROM BM_SalesD sd
                        LEFT JOIN BM_SalesM sm On sm.CorpId = sd.CorpId AND sd.SalesNo = sm.SalesNo
                        LEFT JOIN CM_Item cm On sd.CorpId = cm.CorpId AND sd.ProductCode = cm.MaterialCode
                        WHERE sd.CorpID = '{WrGlobal.CorpID}'
                        AND sd.SalesNo = '{SalesNo}'
                        ORDER BY sd.SalesSeq DESC
            ";

            return db.GetDataTable(query);
        }

        public DataTable Select_BM_SalesD_BySalesNo_Lup()
        {
            query = $@"
                         SELECT 
                                  sd.SalesNo
		                        , sd.SalesSeq

		                        , sd.ItemCode
                                , sd.ItemCode ItemName
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

		                        , sd.Qty
		                        , sd.UnitPrice
		                        , sd.Price
		                        , sd.VatPrice
		                        , sd.TotalPrice
		                        , sd.WonAmount
		                        , sd.VatType
		                        , sd.VatRate

		                        , sd.HouseCode
		                        , sd.UnitCode
		                        , sd.Remark
		                        , sd.SdOrderNo
		                        , sd.SdOrderSeq

		                        , sd.CreID
		                        , sd.CreIP
		                        , sd.CreDt
		                        , sd.ModID
		                        , sd.ModIP
		                        , sd.ModDt

		                        --Master
		                        , sm.SalesDate
		                        , sm.ShipToCode
		                        , sm.BillToCode

		                        , '' AS Del

                        FROM BM_SalesD sd
                        LEFT JOIN BM_SalesM sm On sm.CorpId = sd.CorpId AND sd.SalesNo = sm.SalesNo
                        LEFT JOIN CM_Item cm ON sd.CorpId = cm.CorpId AND sd.ItemCode = cm.ItemCode
                        WHERE sd.CorpID = '{WrGlobal.CorpID}'
                        AND sd.SalesNo = '{SalesNo}'
                        ORDER BY sd.SalesSeq DESC
            ";

            return db.GetDataTable(query);
        }

        public DataTable Select_Order_FromSales(string startDt, string endDt)
        {
            query = $@"SELECT '' AS Status, 
                              som.ObtainDate,
                              som.SoldToCode,
                              som.SoldToCode SoldToName,
                              som.ShipToCode,
                              som.ShipToCode ShipToName,
                              som.PONo,
                              som.ExchangeRate,
                              som.SalesMan,
                              sod.MaterialCode,
                              cm.MaterialName,
                              cm.Alias,
                              sod.Qty OrderQty,
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

                              --출하
                              , '' SalesNo
                              , 0 Qty
                              , NULL SalesSeq
							  , sod.Qty - ISNULL(sd.OutQty, 0) Un_Qty
                              , '' HouseCode
                              , '' HouseName
                              , cc.ExchangeType CurrencyType

                         FROM BM_SdOrderD sod
                    LEFT JOIN SdOrderMaster som ON sod.CorpId = som.CorpId AND som.SdOrderNo = sod.SdOrderNo
                    LEFT JOIN CM_Item cm ON sod.CorpId = cm.CorpId AND cm.MaterialCode = sod.MaterialCode
                    LEFT JOIN CM_Cust cc ON cc.CorpId = cm.CorpId AND cc.CustCode = cm.CustCode
					LEFT JOIN 
					(
						SELECT SdOrderNo, SdOrderSeq, SUM(Qty) AS OutQty
						FROM BM_SalesD
						WHERE CorpId = '{WrGlobal.CorpID}'
						GROUP BY SdOrderNo, SdOrderSeq
					) sd ON sod.SdOrderNo = sd.SdOrderNo AND sod.SdOrderSeq = sd.SdOrderSeq
                    WHERE som.CorpId = '{WrGlobal.CorpID}' 
                    AND som.ObtainDate BETWEEN '{startDt}' AND '{endDt}'
                        ";

            if (CustCode != "") query += $"\n AND SOM.BillToCode = '{CustCode}'";
            if (ShipToCode != "") query += $"\n AND SOM.ShipToCode = '{ShipToCode}'";
            if (ProductCode != "") query += $"\n AND SD.ProductCode = '{ProductCode}'";
            if (SalesMan != "") query += $"\n AND SOM.SalesMan = '{SalesMan}'";

            return db.GetDataTable(query);
        }
        public DataTable Select_MaterialOutView()
        {
            MaetrialCode = MaetrialCode.ConvertStr_Empty_To_Null();

            query = $@"SELECT SD.SalesSeq
                            , CONVERT(DATE, SUBSTRING(SD.SalesNo, 3,6),112) AS OutDate
		                    , SD.ProductCode
		                    , SD.Qty
		                    , SD.UnitCode
                            , SD.UnitPrice
		                    , SD.Price
		                    , SD.VatPrice
		                    , SD.TotalPrice
		                    , SD.VatType
		                    , SD.VatRate
		                    , SD.HouseCode
                    FROM BM_SalesD SD
                    WHERE SD.CorpId = '{WrGlobal.CorpID}' AND SD.ReqType = '자재출고'
                    AND CONVERT(DATE, SUBSTRING(SD.SalesNo, 3,6),112) BETWEEN '{stDate}' AND '{endDate}'";
            if (MaetrialCode != "NULL")
            {
                query += $@"AND SD.ProductCode = {MaetrialCode}";
            }
            query += "ORDER BY SalesSeq";

            return db.GetDataTable(query);
        }

        public int GetDetailCnt()
        {
            query = $@"
            SELECT COUNT(*) CNT
            FROM BM_SalesD
            WHERE CorpId  = '{CorpId}'
            AND SalesNo   = '{SalesNo}'
            ";

            int cnt = 0;

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                cnt = dr["CNT"].ConvertInt_Null_To_Zero();
            }

            return cnt;
        }

        public bool InsertBM_SalesD()
        {
            query = $@"

                          INSERT INTO BM_SalesD
                            (
                            CorpId
                            , SalesNo
                            , SalesSeq
                            , ItemCode
                            , UnitWeight
                            , UnitCode
                            , Qty
                            , UnitPrice
                            , Price
                            , VatPrice
                            , TotalPrice
                            , WonAmount
                            , VatType
                            , VatRate
                            , ReqType
                            , SdOrderNo
                            , SdOrderSeq
                            , Status
                            , Remark
                            , HouseCode
                            , CreId
                            , CreIP
                            , CreDt
                            )
                            VALUES
                            (
                            '{CorpId}'
                            , '{SalesNo}'
                            , {SalesSeq}
                            , {ProductCode.ConvertStr_Empty_To_Null()}
                            , {UnitWeight.ConvertNumeric_Empty_To_Null()}
                            , {UnitCode.ConvertStr_Empty_To_Null()}
                            , {Qty.ConvertNumeric_Empty_To_Null()}
                            , {UnitPrice.ConvertNumeric_Empty_To_Null()}
                            , {Price.ConvertNumeric_Empty_To_Null()}
                            , {VatPrice.ConvertNumeric_Empty_To_Null()}
                            , {TotalPrice.ConvertNumeric_Empty_To_Null()}
                            , {WonAmount.ConvertNumeric_Empty_To_Null()}
                            , {VatType.ConvertStr_Empty_To_Null()}
                            , {VatRate.ConvertNumeric_Empty_To_Null()}
                            , {ReqType.ConvertStr_Empty_To_Null()}
                            , {SdOrderNo.ConvertStr_Empty_To_Null()}
                            , {SdOrderSeq.ConvertInt_Empty_To_Null()}
                            , {Status.ConvertStr_Empty_To_Null()}
                            , {Remark.ConvertStr_Empty_To_Null()}
                            , {HouseCode.ConvertStr_Empty_To_Null()}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.IpAddress}'
                            , getdate()
                            )

            ";

            return db.ExecuteQuery(query);
        }

        public bool UpdateBM_SalesD()
        {
            query = $@"

            UPDATE BM_SalesD
            SET  
            UnitWeight   = {UnitWeight.ConvertNumeric_Empty_To_Null()}
            , UnitCode   = {UnitCode.ConvertStr_Empty_To_Null()}
            , Qty        = {Qty.ConvertNumeric_Empty_To_Null()}
            , UnitPrice  = {UnitPrice.ConvertNumeric_Empty_To_Null()}
            , Price      = {Price.ConvertNumeric_Empty_To_Null()}
            , VatPrice   = {VatPrice.ConvertNumeric_Empty_To_Null()}
            , TotalPrice = {TotalPrice.ConvertNumeric_Empty_To_Null()}
            , WonAmount  = {WonAmount.ConvertFloat_Null_To_Zero()}
            , VatType    = {VatType.ConvertStr_Empty_To_Null()}
            , VatRate    = {VatRate.ConvertNumeric_Empty_To_Null()}
            , ReqType    = {ReqType.ConvertStr_Empty_To_Null()}
            , SdOrderNo  = {SdOrderNo.ConvertStr_Empty_To_Null()}
            , SdOrderSeq = {SdOrderSeq.ConvertNumeric_Empty_To_Null()}
            , HouseCode  = {HouseCode.ConvertStr_Empty_To_Null()}
            , Status     = {Status.ConvertStr_Empty_To_Null()}
            , Remark     = {Remark.ConvertStr_Empty_To_Null()}
            , ModId      = '{WrGlobal.LoginID}'
            , ModIP      = '{WrGlobal.ClientHostName}'
            , ModDt      = GETdATe()
            WHERE CorpId  = '{CorpId}'
            AND SalesNo   = '{SalesNo}'
            AND SalesSeq  = '{SalesSeq}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool DelteBM_SalesD()
        {
            query = $@"

            DELETE BM_SalesD
            WHERE CorpId  = '{CorpId}'
            AND SalesNo   = '{SalesNo}'
            AND SalesSeq  = '{SalesSeq}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool DeletebySalesNo()
        {
            query = $@"

            DELETE BM_SalesD
            WHERE CorpId  = '{CorpId}'
            AND SalesNo   = '{SalesNo}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool DeleteSalesMD_Work(string barcodeNo)
        {
            query = $@"

                    DECLARE @TB TABLE
                    (
                    	SalesNo nvarchar(20)
                    )
                    
                    INSERT @TB
                    SELECT SalesNo
                    FROM BM_SalesD 
                    WHERE CorpID = '{WrGlobal.CorpID}'
                    AND ReqType  = '자재출고'
                    AND BarCodeNo  = '{barcodeNo}'
                    
                    DELETE BM_SalesD WHERE CorpId = '{WrGlobal.CorpID}' AND SalesNo IN (SELECT SalesNo FROM @TB)
                    DELETE BM_SalesM WHERE CorpId = '{WrGlobal.CorpID}' AND SalesNo IN (SELECT SalesNo FROM @TB)
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
