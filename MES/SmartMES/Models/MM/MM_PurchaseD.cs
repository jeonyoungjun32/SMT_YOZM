using DevExpress.Utils.About;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;

namespace SmartMES.Models.MM
{
	public class MM_PurchaseD
	{
        #region Property
        public string CorpId { get; set; }

        public string StoNo { get; set; }

        public int StoSeq { get; set; }

        public string ItemCode { get; set; }

        public string UnitCode { get; set; }

        public float Qty { get; set; }

        public float BadQty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Price { get; set; }

        public decimal VatPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string VatType { get; set; }

        public int VatRate { get; set; }

        public string State { get; set; }

        public string WriteID { get; set; }

        public string MmOrderNo { get; set; }

        public int MmOrderSeq { get; set; }

        public string QCNo { get; set; }

        public string LotNo { get; set; }

        public string Remark { get; set; }

        public string CustLotNo { get; set; }


        //조건용
        public string CustCode { get; set; }


        #endregion

        Database db = new Database();
        string query = "";

        public DataTable SelectMM_PurchaseD(string startDate, string endDate)
		{
            query = $@"
                        SELECT 
                        pd.StoNo
                        , pd.StoSeq
                        
                        , pd.ItemCode
                        , cm.ItemName
                        , cm.Alias
                        , cm.ItemClass1
                        , cm.ItemClass2
                        , cm.ItemClass3
                        , cm.Size
                        , cm.UnitWeight
                        , cm.Texture
                        , cm.Type
                        , cm.Standard
                        , C1.Title ItemTypeName
                        , cm.UnitCode
                        , C2.Title UnitName 
                        , cm.ColorCode

                        , pd.Qty
                        , pd.UnitPrice
                        , pd.Price
                        , pd.VatPrice
                        , pd.TotalPrice
                        , pd.MmOrderNo
                        , pd.MmOrderSeq
                        , pd.VatType
						, pd.VatRate
                        
                        , pm.StoDate
                        , pm.CustCode
                        , cc.CustName

                        , pd.CreId
                        , pd.CreIP
                        , pd.CreDt
                        , pd.ModId
                        , pd.ModIP
                        , pd.ModDt

                        --발주정보
                        , PM2.PlacingDate

                        , 0 UpdUnitPrice
                        , 0 UpdPrice
                        , 0 UpdVatPrice
                        , 0 UpdTotalPrice
                        , 0 DiffTotalPrice

                        FROM MM_PurchaseD pd
                        INNER JOIN MM_PurchaseM pm ON pd.CorpId = pm.CorpId AND pd.StoNo = pm.StoNo
                        INNER JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.ItemCode = cm.ItemCode
                        LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode
                        LEFT JOIN CM_CateItem C1 on pd.CorpId = C1.CorpId AND C1.class = 'A010' and C1.ClassItemCode = CM.ItemType	--제품분류
		                LEFT JOIN CM_CateItem C2 on pd.CorpId = C2.CorpId AND C2.class = '401040' and C2.ClassItemCode = CM.UnitCode		--단위코드
                        LEFT JOIN MM_PlacingD PD2 on PD2.CorpId = PM.CorpId and PD2.MmOrderNo = PD.MmOrderNo and PD2.MmOrderSeq = PD.MmOrderSeq
		                LEFT JOIN MM_PlacingM PM2 on PM2.CorpId = PM.CorpId and PD2.MmOrderNo = PM2.MmOrderNo

                        WHERE pd.CorpId = '{WrGlobal.CorpID}'
                        AND pm.StoDate BETWEEN '{startDate}' AND '{endDate}'
                        ";

            if(ItemCode != null && ItemCode != "" && ItemCode.ToUpper() != "ALL")
			{
                query += $"\nAND pd.ItemCode = '{ItemCode}'";
			}

            if (CustCode != null && CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $"\nAND pm.CustCode = '{CustCode}'";
            }

            DataTable dt = db.GetDataTable(query);

            return dt;
		}

        public DataTable SelectMM_PurchaseD_StoNo(string corpId, string stoNo)
		{
            query = $@"EXEC Purchase_GetDetailList '{corpId}', '{stoNo}'";

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public DataTable SelectMM_PurchaseD_Exam(string startDate, string endDate, string ItemType, string examDivCd = "10")
        {
            query = $@"
                        SELECT 
                        pd.StoNo AS RefNo
                        , pd.StoSeq AS RefSeq
                        
                        , pd.ItemCode
                        , cm.ItemName
                        , cm.Size
                        , cm.Standard
                        , cm.Alias
                        , cm.ItemClass1
                        , cm.Texture
                        , cm.UnitCode
                        
                        , pd.Qty AS ExamQty
                        , pd.MmOrderNo
                        , pd.MmOrderSeq
                        
                        , pm.StoDate
                        , pm.CustCode
                        , cc.CustName

						, pd.QCNo
						, pd.LotNo
						, pd.CustLotNo

                        --검사여부
                        , IIF(ap.ExamCnt > 0, 'Y', 'N') AS ExamYn

                        -- 전체 합격 여부 (모두 10이면 10, 아니면 20)
                           , CASE 
                                WHEN ap.ExamCnt = 0 THEN NULL          -- 검사내역 없으면(ExamYn='N') 전체합격여부는 NULL 처리
                                WHEN ap.NonPassCnt = 0 THEN '합격'
                                ELSE '불합격'
                              END AS AllPass

                        
                        FROM MM_PurchaseD pd
                        INNER JOIN MM_PurchaseM pm ON pd.CorpId = pm.CorpId AND pd.StoNo = pm.StoNo
                        INNER JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.ItemCode = cm.ItemCode
                        LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode
                        OUTER APPLY (
                            SELECT
                                  COUNT(*) AS ExamCnt
                                , SUM(CASE WHEN er.PassDivCd <> '10' THEN 1 ELSE 0 END) AS NonPassCnt
                            FROM QM_ExamResult er
                            WHERE er.CorpId     = pd.CorpId
                              AND er.RefNo      = pd.StoNo
                              AND er.RefSeq  = pd.StoSeq
                              AND er.ExamDivCd = '{examDivCd}'
                        ) ap

                        WHERE pd.CorpId = '{CorpId}'
                        AND cm.ExamYn = 'Y'
                        AND pm.StoDate BETWEEN '{startDate}' AND '{endDate}'
                        ";

            if (ItemCode != null && ItemCode != "")
            {
                query += $"\nAND pd.ItemCode = '{ItemCode}'";
            }

            if(ItemType != "" && ItemType.ToUpper() != "ALL")
            {
                query += $"\nAND cm.ItemType = '{ItemType}'";
			}

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public DataTable Select_MM_PurchaseD_StoNo()
        {
            query = $@"
                        SELECT '' AS Status
                             , PD.CorpId
                             , StoNo
                             , StoSeq
                             , PD.ItemCode
                             , PD.UnitCode
                             , Qty
                             , BadQty
                             , PD.UnitPrice
                             , Price
                             , VatPrice
                             , TotalPrice
                             , VatType
                             , VatRate
                             , State
                             , WriteID
                             , MmOrderNo
                             , MmOrderSeq
                             , QCNo
                             , LotNo
                 
                             , PD.Remark
                             , PD.CreId
                             , PD.CreIP
                             , PD.CreDt
                             , PD.ModId
                             , PD.ModIP
                             , PD.ModDt
                             , CustLotNo

							 , CM.ItemName
							 , CM.Alias
							 , CM.Size
							 , CM.UnitCode
							 , CM.Texture
							 , CM.ItemClass1
							 , CM.ItemClass2
							 , CM.ItemClass3
							 , CM.UnitWeight
							 , CM.Type

                        FROM MM_PurchaseD PD
						LEFT JOIN CM_Item CM ON PD.CorpId = CM.CorpId AND PD.ItemCode = cm.ItemCode
                        WHERE PD.CorpId = '{WrGlobal.CorpID}'
                          AND StoNo  = '{StoNo}'
                      ";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public DataTable SelectPurchaseDList(string startDate, string endDate)
        {
            query = $@"
                        SELECT 
                        pd.StoNo
                        , pd.StoSeq
                        , pd.LotNo
                        
                        , pd.ItemCode
                        , cm.ItemName
                        , cm.Alias
                        , cm.ItemClass1
                        , cm.ItemClass2
                        , cm.ItemClass3
                        , cm.Size
                        , cm.UnitWeight
                        , cm.Texture
                        , cm.Type
                        , cm.Standard
                        , C1.Title ItemTypeName
                        , cm.UnitCode
                        , C2.Title UnitName 
                        , cm.ColorCode

                        , pd.Qty
                        , pd.UnitPrice
                        , pd.Price
                        , pd.VatPrice
                        , pd.TotalPrice
                        , pd.MmOrderNo
                        , pd.MmOrderSeq
                        , pd.VatType
						, pd.VatRate
                        
                        , pm.StoDate
                        , pm.CustCode
                        , cc.CustName

                        , pd.LotNo
                        , pd.CreId
                        , pd.CreIP
                        , pd.CreDt
                        , pd.ModId
                        , pd.ModIP
                        , pd.ModDt

                        --출고정보
                        , 0 OutQty

                        FROM MM_PurchaseD pd
                        INNER JOIN MM_PurchaseM pm ON pd.CorpId = pm.CorpId AND pd.StoNo = pm.StoNo
                        INNER JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.ItemCode = cm.ItemCode
                        LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode
                        LEFT JOIN CM_CateItem C1 on pd.CorpId = C1.CorpId AND C1.class = 'A010' and C1.ClassItemCode = CM.ItemType	--제품분류
		                LEFT JOIN CM_CateItem C2 on pd.CorpId = C2.CorpId AND C2.class = '401040' and C2.ClassItemCode = CM.UnitCode		--단위코드
                        INNER JOIN StockListView sv ON pd.ItemCode = sv.ItemCode AND sv.ItemType IN (40,50,70,80,90) AND sv.StockQty > 0  -- 자재현재고
                        WHERE pd.CorpId = '{CorpId}'
                        AND pm.StoDate BETWEEN '{startDate}' AND '{endDate}'
                        ";

            if (CustCode != null && CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $"\nAND pm.CustCode = '{CustCode}'";
            }

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public bool Insert_MM_PurchaseD()
        {
            query = $@"
                     INSERT INTO MM_PurchaseD
                            (
                                CorpId
                              , StoNo
                              , StoSeq
                              , ItemCode
                              , UnitCode
                              , Qty
                              , BadQty
                              , UnitPrice
                              , Price
                              , VatPrice
                              , TotalPrice
                              , VatType
                              , VatRate
                              , State
                              , WriteID
                              , MmOrderNo
                              , MmOrderSeq
                              , QCNo
                              , LotNo
                              , Remark
                              , CreId
                              , CreIP
                              , CreDt
                            )
                        VALUES
                        (
                                '{WrGlobal.CorpID}'
                              , {StoNo.ConvertStr_Empty_To_Null()}
                              , {StoSeq.ConvertInt_Null_To_Zero()}
                              , {ItemCode.ConvertStr_Empty_To_Null()}
                              , {UnitCode.ConvertStr_Empty_To_Null()}
                              , '{Qty.ConvertFloat_Null_To_Zero()}'
                              , '{BadQty.ConvertFloat_Null_To_Zero()}'
                              , '{UnitPrice.ConvertFloat_Null_To_Zero()}'
                              , '{Price.ConvertFloat_Null_To_Zero()}'
                              , '{VatPrice.ConvertFloat_Null_To_Zero()}'
                              , '{TotalPrice.ConvertFloat_Null_To_Zero()}'
                              , {VatType.ConvertStr_Empty_To_Null()}
                              , '{VatRate.ConvertInt_Null_To_Zero()}'
                              , {State.ConvertStr_Empty_To_Null()}
                              , {WriteID.ConvertStr_Empty_To_Null()}
                              , {MmOrderNo.ConvertStr_Empty_To_Null()}
                              , {MmOrderSeq.ConvertInt_Null_To_Zero()}
                              , {QCNo.ConvertStr_Empty_To_Null()}
                              , {LotNo.ConvertStr_Empty_To_Null()}
                              , {Remark.ConvertStr_Empty_To_Null()}
                              , '{WrGlobal.LoginID}'
                              , '{WrGlobal.ClientHostName}'
                              , GETDATE()
                        )

                    ";

            return db.ExecuteQuery(query);
        }

        public bool Update_MM_PurchaseD()
        {
            query = $@"
                        UPDATE MM_PurchaseD
                           SET 
                                 ItemCode = {ItemCode.ConvertStr_Empty_To_Null()}
                               , UnitCode     = {UnitCode.ConvertStr_Empty_To_Null()}
                               , Qty          = '{Qty.ConvertFloat_Null_To_Zero()}'
                               , BadQty       = '{BadQty.ConvertFloat_Null_To_Zero()}'
                               , UnitPrice    = '{UnitPrice.ConvertFloat_Null_To_Zero()}'
                               , Price        = '{Price.ConvertFloat_Null_To_Zero()}'
                               , VatPrice     = '{VatPrice.ConvertFloat_Null_To_Zero()}'
                               , TotalPrice   = '{TotalPrice.ConvertFloat_Null_To_Zero()}'
                               , VatType      = {VatType.ConvertStr_Empty_To_Null()}
                               , VatRate      = '{VatRate.ConvertInt_Null_To_Zero()}'
                               , State        = {State.ConvertStr_Empty_To_Null()}
                               , WriteID      = {WriteID.ConvertStr_Empty_To_Null()}
                               , MmOrderNo    = {MmOrderNo.ConvertStr_Empty_To_Null()}
                               , MmOrderSeq   = {MmOrderSeq.ConvertInt_Null_To_Zero()}
                               , QCNo         = {QCNo.ConvertStr_Empty_To_Null()}
                               , Remark       = {Remark.ConvertStr_Empty_To_Null()}
                               , ModId        = '{WrGlobal.LoginID}'
                               , ModIP        = '{WrGlobal.ClientHostName}'
                               , ModDt        = GETDATE()
                         WHERE CorpId = '{WrGlobal.CorpID}'
                           AND StoNo  = {StoNo.ConvertStr_Empty_To_Null()}
                           AND StoSeq = {StoSeq.ConvertInt_Null_To_Zero()}
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Update_MM_PurchaseDLotNo()
        {
            query = $@"
                        UPDATE MM_PurchaseD
                           SET 
                                 ItemCode = {ItemCode.ConvertStr_Empty_To_Null()}
                               , QCNo         = {QCNo.ConvertStr_Empty_To_Null()}
                               , ModId        = '{WrGlobal.LoginID}'
                               , ModIP        = '{WrGlobal.ClientHostName}'
                               , ModDt        = GETDATE()
                         WHERE CorpId = '{WrGlobal.CorpID}'
                           AND StoNo  = {StoNo.ConvertStr_Empty_To_Null()}
                           AND StoSeq = {StoSeq.ConvertInt_Null_To_Zero()}
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Delete_MM_PurchaseD()
        {
            query = $@"
                        DELETE MM_PurchaseD
                         WHERE CorpId = '{WrGlobal.CorpID}'
                           AND StoNo  = '{StoNo}'
                           AND StoSeq = '{StoSeq}'
                      ";

            return db.ExecuteQuery(query);
        }
    }
}
