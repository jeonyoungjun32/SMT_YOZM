using System.Data;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.MD
{
    public class MD_MoldInOut
    {
        #region Property

        public string InOutNo { get; set; }

        public int InOutRowSeq { get; set; }

        public int DispSeq { get; set; }

        public string InDate { get; set; }

        public string OutDate { get; set; }

        public string CustCode { get; set; }

        public string ShipCustCode { get; set; }

        public string MoldCode { get; set; }

        public int InQty { get; set; }

        public int InShot { get; set; }

        public decimal InPrice { get; set; }

        public decimal InAmt { get; set; }

        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable Select_MoldInOut(string inFrom, string inTo)
        {
            query = $@"

                    SELECT 
                        '' AS Status
                        , MI.InOutNo
                        , MI.InOutRowSeq
                        , MI.DispSeq
                        , CONVERT(DATE, MI.InDate) AS InDate
                        , CONVERT(DATE, MI.OutDate) AS OutDate
                        , ISNULL(MI.CustCode, MD.SupplyCust) AS CustCode
                        , CM.CustName
                        , MI.MoldCode
                        , MI.InQty
                        , MI.InShot AS InitShot
                        , MI.InPrice
                        , MI.InAmt
                        , MI.ShipCustCode
                        , MI.Remark
                        , MI.CreId
                        , MI.CreDt
                        , MI.ModId
                        , MI.ModDt
                        , MD.MoldMgmtNo
                        , MD.MoldSN
                        , MD.Chasu
                        , MD.MoldCode AS MoldName
                        , MD.ProdtCust
                        , MD.MoldPjtNo
                    
                    FROM MD_MoldInOut MI
                    
                    INNER JOIN MD_Mold MD ON MI.CorpId = MD.CorpId AND MI.MoldCode = MD.MoldCode
                    LEFT JOIN CM_Cust CM ON MI.CorpId = CM.CorpId AND MI.CustCode = CM.CustCode
                    
                    WHERE MI.CorpId = '{WrGlobal.CorpID}'
                      AND MI.OutDate BETWEEN '{inFrom}' AND '{inTo}'
                    
                    ";

            if (MoldCode != "")
            {
                query += $"\n AND MI.MoldCode = '{MoldCode}' ";
            }
            return db.GetDataTable(query);
        }

        public DataTable Select_MoldInOut()
        {
            query = $@"

                    SELECT 
                        '' AS Status
                        , MI.InOutNo
                        , MI.InOutRowSeq
                        , MI.DispSeq
                        , CONVERT(DATE, MI.InDate) AS InDate
                        , CONVERT(DATE, MI.OutDate) AS OutDate
                        , MI.CustCode
                        , CM.CustName
                        , MI.MoldCode
                        , MI.InQty
                        , MI.InShot AS InitShot
                        , MI.InPrice
                        , MI.InAmt
                        , MI.Remark
                        , MI.CreId
                        , MI.CreDt
                        , MI.ModId
                        , MI.ModDt
						, MD.MoldPjtNo
                        , MD.MoldMgmtNo
                        , MD.MoldSN
                        , MD.Chasu
                        , MD.MoldName
                        , MD.ProdtCust
                    
                    FROM MD_MoldInOut MI
                    INNER JOIN MD_Mold MD ON MI.CorpId = MD.CorpId AND MI.MoldCode = MD.MoldCode
                    LEFT JOIN CM_Cust CM ON MI.CorpId = CM.CorpId AND MI.CustCode = CM.CustCode
                    
                    WHERE MI.CorpId = '{WrGlobal.CorpID}'
                      AND MI.MoldCode  = '{MoldCode}' ";

            return db.GetDataTable(query);
        }


        public DataTable Select_MD_MoldIn_MoldOut(string remainYn, string inFrom, string inTo)
        {
            //금형출고 - 금형입고 조회 쿼리
            query = $@"

                        SELECT
                            '' AS Status
                            , MI.InNo
                            , MI.InRowSeq
                            , MI.DispSeq
                            , CONVERT(DATE, MI.InDate) AS InDate
                            
                            , MI.CustCode
                            , MI.CustCode AS Cust_Nm

                            , MI.MoldCode
                            , MI.MoldCode AS MoldName
                            , MD.MoldMgmtNo

                            , MI.InQty
                            , MI.InPrice
                            , MI.InAmt
                            , MI.Remark
                            , MI.CreId
                            , MI.CreDt
                            , MI.ModId
                            , MI.ModDt

                           
                            --
                            , '' AS OutNo
                            , NULL AS OutRowSeq
                            , CONVERT(DATE, GETDATE()) AS OutDate
                            , NULL AS OutQty
                            , CASE WHEN '{'N'}' = 'Y' THEN mi.InQty - ISNULL(mo.OutQty, 0)	--출고량 초기값 미입고량 자동입력
                                                      ELSE 0	                                --출고량 초기값 0
                                                      END AS OutQty
                            , MI.InQty - ISNULL(MO.OutQty, 0) AS RemaInQty	--미출고량
                        
                        FROM MD_MoldIn MI
                        LEFT JOIN (
                                    SELECT CorpId
                                    , InNo
                                    , InRowSeq
                                    , SUM(OutQty) AS OutQty
                                    FROM MD_MoldOut
                                    GROUP BY CorpId, InNo, InRowSeq
                                    ) AS MO ON MI.CorpId = MO.CorpId 
                                           AND MI.InNo = MO.InNo 
                                           AND MI.InRowSeq = MO.InRowSeq
                        INNER JOIN MD_Mold MD ON MI.CorpId = MD.CorpId AND MI.MoldCode = MD.MoldCode
                        INNER JOIN CM_Cust CM ON MI.CorpId = CM.CorpId AND MI.CustCode = CM.CustCode
                        WHERE  MI.CorpId = '{WrGlobal.CorpID}'
                          AND MI.InDate BETWEEN '{inFrom}' AND '{inTo}'

            ";

            if(MoldCode != "" && MoldCode != "ALL")
            {
                query += $"\n AND MI.MoldCode = '{MoldCode}' ";
            }

            if (CustCode != "" && CustCode != "ALL")
            {
                query += $"\n AND MI.CustCode = '{CustCode}' ";
            }

            if (remainYn == "10")
            {
                //입고 미완료 목록
                query += $"\nAND mi.InQty - ISNULL(mo.OutQty, 0) > 0 --미출고 목록";
            }
            else if (remainYn == "20")
            {
                //입고완료 목록
                query += $"\nAND mi.InQty - ISNULL(mo.OutQty, 0) <= 0 --출고완료 목록";
            }

            return db.GetDataTable(query);
        }

        public bool Insert_MoldInOut()
        {
            query = $@"
                        INSERT INTO MD_MoldInOut
                        (
                            CorpId
                            , InOutNo
                            , InOutRowSeq
                            , DispSeq
                            , InDate
                            , OutDate
                            , CustCode
                            , MoldCode
                            , InQty
                            , InShot
                            , InPrice
                            , InAmt
                            , Remark
                            , ShipCustCode
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{InOutNo}'
                            , '{InOutRowSeq}'
                            , {DispSeq.ConvertInt_Null_To_Zero()}
                            , {InDate.ConvertStr_Empty_To_Null()}
                            , {OutDate.ConvertStr_Empty_To_Null()}
                            , {CustCode.ConvertStr_Empty_To_Null()}
                            , {MoldCode.ConvertStr_Empty_To_Null()}
                            , {InQty.ConvertNumeric_Empty_To_Null()}
                            , {InShot.ConvertNumeric_Empty_To_Null()}
                            , {InPrice.ConvertNumeric_Empty_To_Null()}
                            , {InAmt.ConvertNumeric_Empty_To_Null()}
                            , {Remark.ConvertStr_Empty_To_Null()}
                            , {ShipCustCode.ConvertStr_Empty_To_Null()}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )

                        UPDATE MD_Mold
                           SET UseYn = 'Y'
                         WHERE MoldCode = {MoldCode.ConvertStr_Empty_To_Null()}                    
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Update_MoldInOut()
        {
            query = $@" 
                        UPDATE MD_MoldInOut
                        SET InDate = {InDate.ConvertStr_Empty_To_Null()}
                          , OutDate = {OutDate.ConvertStr_Empty_To_Null()}
                          , CustCode = {CustCode.ConvertStr_Empty_To_Null()}
                          , MoldCode = {MoldCode.ConvertStr_Empty_To_Null()}
                          , InQty = {InQty.ConvertNumeric_Empty_To_Null()}
                          , InShot = {InShot.ConvertNumeric_Empty_To_Null()}
                          , InPrice = {InPrice.ConvertNumeric_Empty_To_Null()}
                          , InAmt = {InAmt.ConvertNumeric_Empty_To_Null()}
                          , Remark = {Remark.ConvertStr_Empty_To_Null()}
                          , ShipCustCode = {ShipCustCode.ConvertStr_Empty_To_Null()}
                          , ModId = '{WrGlobal.LoginID}'
                          , ModIP = '{WrGlobal.ClientHostName}'
                          , ModDt = GETDATE()

                        WHERE CorpId ='{WrGlobal.CorpID}'
                         AND InOutNo = '{InOutNo}'
                         AND InOutRowSeq = '{InOutRowSeq}' 
                    ";

            return db.ExecuteQuery(query);
        }


        public bool Delete_MoldInOut()
        {
            query = $@" DELETE MD_MoldInOut
                        WHERE CorpId ='{WrGlobal.CorpID}'
                        AND InOutNo = '{InOutNo}'
                        AND InOutRowSeq = '{InOutRowSeq}' ";

            return db.ExecuteQuery(query);
        }
    }
}
