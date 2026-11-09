using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.MM
{
	class MM_PurchaseD
	{
        #region Property
        public string CorpId { get; set; }

        public string StoNo { get; set; }

        public int StoSeq { get; set; }

        public string ItemCode { get; set; }
        public string MaterialCode { get; set; }
        public int? DetailSeq { get; set; }

        public string UnitCode { get; set; }

        public float? Qty { get; set; }

        public float? BadQty { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? Price { get; set; }

        public decimal? VatPrice { get; set; }

        public decimal? TotalPrice { get; set; }

        public string VatType { get; set; }

        public int? VatRate { get; set; }

        public string State { get; set; }

        public string WriteID { get; set; }

        public string MmOrderNo { get; set; }

        public int? MmOrderSeq { get; set; }

        public string ReqNo { get; set; }
        public int? ReqSeq { get; set; }
        public string QCNo { get; set; }

        public string LotNo { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public string Option5 { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        //
        public string CustCode { get; set; }

        public decimal? UpdUnitPrice { get; set; }

        public decimal? UpdPrice { get; set; }

        public decimal? UpdVatPrice { get; set; }

        public decimal? UpdTotalPrice { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable SelectMM_PurchaseD(string startDate, string endDate)
        {
            query = $@"
                        SELECT 
                        pd.StoNo
                        , pd.StoSeq
                        
                        , pd.MaterialCode
                        , cm.MaterialName
                        , cm.Alias
                        , cm.MaterialClass1
                        , cm.MaterialClass2
                        , cm.MaterialClass3
                        , cm.Size
                        , cm.UnitWeight
                        , cm.Texture
                        , cm.Type
                        , cm.Standard
                        , C1.Title MaterialTypeName
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
                        INNER JOIN PurchaseMaster pm ON pd.CorpId = pm.CorpId AND pd.StoNo = pm.StoNo
                        INNER JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.MaterialCode = cm.MaterialCode
                        LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode
                        LEFT JOIN CMCateItem C1 on pd.CorpId = C1.CorpId AND C1.class = 'A010' and C1.ItemCode = CM.MaterialType	--제품분류
		                LEFT JOIN CMCateItem C2 on pd.CorpId = C2.CorpId AND C2.class = '401040' and C2.ItemCode = CM.UnitCode		--단위코드
                        LEFT JOIN PlacingDetail PD2 on PD2.CorpId = PM.CorpId and PD2.MmOrderNo = PD.MmOrderNo and PD2.MmOrderSeq = PD.MmOrderSeq
		                LEFT JOIN PlacingMaster PM2 on PM2.CorpId = PM.CorpId and PD2.MmOrderNo = PM2.MmOrderNo

                        WHERE pd.CorpId = '{CorpId}'
                        AND pm.StoDate BETWEEN '{startDate}' AND '{endDate}'
                        ";

            if (MaterialCode != null && MaterialCode != "" && MaterialCode.ToUpper() != "ALL")
            {
                query += $"\nAND pd.MaterialCode = '{MaterialCode}'";
            }

            if (CustCode != null && CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $"\nAND pm.CustCode = '{CustCode}'";
            }

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public DataTable SelectMM_PurchaseD_Exam(string startDate, string endDate, string materialType, string examDivCd = "10")
        {
            query = $@"
                        SELECT 
                        pd.StoNo AS Ref_No
                        , pd.StoSeq AS Ref_No_Seq
                        
                        , pd.MaterialCode
                        , cm.MaterialName
                        , cm.Size
                        , cm.Standard
                        , cm.Alias
                        , cm.MaterialClass1
                        
                        , pd.Qty AS Exam_Qty
                        , pd.MmOrderNo
                        , pd.MmOrderSeq
                        
                        , pm.StoDate
                        , pm.CustCode
                        , cc.CustName

                        , IIF( (SELECT COUNT(Exam_No) 
						        FROM QM_ExamResult 
						        WHERE Corp_Cd = pd.CorpId
                                AND Ref_No = pd.StoNo 
						        AND Ref_No_Seq = pd.StoSeq
                                AND Exam_Div_Cd = '{examDivCd}'
						        ) > 0, 'Y', 'N') AS ExamYn
                        
                        FROM MM_PurchaseD pd
                        INNER JOIN PurchaseMaster pm ON pd.CorpId = pm.CorpId AND pd.StoNo = pm.StoNo
                        INNER JOIN CM_Item cm ON pd.CorpId = cm.CorpId AND pd.MaterialCode = cm.MaterialCode
                        LEFT JOIN CM_Cust cc ON pd.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode
                        WHERE pd.CorpId = '{CorpId}'
                        AND cm.ExamYn = 'Y'
                        AND pm.StoDate BETWEEN '{startDate}' AND '{endDate}'
                        ";

            if (MaterialCode != null && MaterialCode != "")
            {
                query += $"\nAND pd.MaterialCode = '{MaterialCode}'";
            }

            if (materialType != "" && materialType.ToUpper() != "ALL")
            {
                query += $"\nAND cm.MaterialType = '{materialType}'";
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
                               , LotNo        = {LotNo.ConvertStr_Empty_To_Null()}
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

        public void UpdatePlacingInQtyByPurchase(string stoNo)
        {
            query = string.Format("exec Placing_UpdateInQtyByPurchase '{0}', '{1}'", WrGlobal.CorpID, stoNo);
            db.ExecuteQuery(query);
        }

        public void MakeStockByPurchase(string stoNo)
        {
            query = string.Format("exec Stock_MakeStockByPurchase '{0}', '{1}', '{2}'", WrGlobal.CorpID, stoNo, WrGlobal.LoginID);
            db.ExecuteQuery(query);
        }

        public void DeletePurchaseMaster(string coId, string stoNo)
        {
            query = String.Format("Delete MM_PurchaseD Where CorpId ='{0}' And StoNo = '{1}'", coId, stoNo);
            db.ExecuteQuery(query);

            query = String.Format("Delete MM_PurchaseM Where CorpId ='{0}' And StoNo = '{1}'", coId, stoNo);
            db.ExecuteQuery(query);
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
