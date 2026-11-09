using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.BM
{
	class BM_SampleOutM
    {
        #region Property

        public string CorpId { get; set; }

        public string OutNo { get; set; }

        public int OutSeq { get; set; }

        public string OutDate { get; set; }
        public string ItemCode { get; set; }
        public string UnitCode { get; set; }   
        public string SdOrderNo { get; set; }
        public int? SdOrderSeq {  get; set; }
        public string BarCodeNo { get; set; }
        public string ExpirationDate { get; set; }  

  
        public string ShipToCode { get; set; }

        public string Status { get; set; }
 
        public string ShipDate { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreDt { get; set; }

        public string ModId { get; set; }

        public string ModDt { get; set; }

        public int Qty { get; set; }

        #endregion Property End

        Database db = new Database();

        string query;

        public DataTable Select_BM_SampleOutM(string startDate, string endDate)
        {
            ShipToCode = ShipToCode.ConvertString_Null_To_Empty();

            query = $@"
                SELECT 
                       sm.CorpId
                     , sm.OutNo
                     , sm.OutSeq
                     , sm.ShipToCode
                     , sm.ItemCode
                     , sm.ItemCode AS ItemName
                     , sm.UnitCode
                     , sm.Qty
                     , sm.SdOrderNo
                     , sm.SdOrderSeq
                     , sm.BarCodeNo
                     , sm.OutDate
                     , sm.ExpirationDate
                     , sm.Status
                     , sm.Remark
                     , sm.CreId
                     , sm.CreDt
                     , sm.ModId
                     , sm.ModDt


                FROM BM_SampleOutM sm
                Left Join CM_Item cm on sm.CorpId = cm.CorpId and sm.ItemCode = cm.ItemCode
                WHERE sm.CorpId = '{WrGlobal.CorpID}'
                  AND sm.OutDate BETWEEN '{startDate}' AND '{endDate}'
              ";
       
            if (ShipToCode != "" && ShipToCode != "ALL")
            {
                query += $"\nAND ShipToCode = '{ShipToCode}'";
            }

            query += $"\nORDER BY OutNo DESC";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }


       public DataTable Select_BM_SampleOutD(string outNo)
       {
           ShipToCode = ShipToCode.ConvertString_Null_To_Empty();

           query = $@"
                       SELECT
                            '' AS [Status]
                            ,  sd.CorpId
                            , sd.OutNo
                            , sd.BarCodeNo
                            , sd.ShipToCode
                            , sd.OutDate
                            , sd.Remark
                            , sd.CreId
                            , sd.CreDt
                            , sd.ModId
                            , sd.ModDt
                            , ISNULL(sm.OutSeq, 0)       AS OutSeq
                            , ISNULL(sm.ItemCode, '')     AS ItemCode
                            , ISNULL(cm.ItemName, '')     AS ItemName
                            , ISNULL(sm.UnitCode, '')     AS UnitCode
                            , ISNULL(sm.Qty, 0)           AS Qty
                            , ISNULL(sm.SdOrderNo, '')    AS SdOrderNo
                            , ISNULL(sm.SdOrderSeq, 0)    AS SdOrderSeq
                            , sm.ExpirationDate
                            , ISNULL(sm.Status, '')       AS Status
                       FROM BM_SampleOutD sd
                       LEFT JOIN BM_SampleOutM sm 
                           ON sd.CorpId = sm.CorpId 
                          AND sd.OutNo  = sm.OutNo
                       LEFT JOIN CM_Item cm 
                           ON sm.CorpId  = cm.CorpId 
                          AND sm.ItemCode = cm.ItemCode
                       WHERE sd.CorpId = '{WrGlobal.CorpID}'
                         AND sd.OutNo  = '{outNo}'
                   ";

           if (ShipToCode != "" && ShipToCode != "ALL")
           {
               query += $"\nAND sd.ShipToCode = '{ShipToCode}'";
           }

           query += $"\nORDER BY sd.BarCodeNo ASC";

           DataTable dt = db.GetDataTable(query);
           return dt;
       }


        // 마스터 INSERT 
        public bool Insert_BM_SampleOutM()
        {
            SdOrderNo = SdOrderNo.ConvertString_Null_To_Empty();
            SdOrderSeq = SdOrderSeq.ConvertInt_Null_To_Zero();  
            query = $@"
                        INSERT INTO BM_SampleOutM
                        (
                              CorpId
                            , OutNo
                            , OutSeq
                            , ItemCode
                            , UnitCode
                            , Qty
                            , SdOrderNo
                            , SdOrderSeq
                            , BarCodeNo
                            , OutDate
                            , ExpirationDate
                            , ShipToCode
                            , Status
                            , Remark
                            , CreId
                            , CreDt
                        )
                        VALUES
                        (
                              '{CorpId}'
                            , '{OutNo}'
                            , {OutSeq}
                            , '{ItemCode}'
                            , '{UnitCode}'
                            , {Qty}
                            , '{SdOrderNo}'
                            , {SdOrderSeq}
                            , '{BarCodeNo}'
                            , '{OutDate}'
                            , '{ExpirationDate}'
                            , '{ShipToCode}'
                            , '{Status}'
                            , '{Remark}'
                            , '{WrGlobal.LoginID}'
                            , GETDATE()
                        )
                    ";
            return db.ExecuteQuery(query);
        }

        // 디테일 INSERT (바코드 1개씩)
        public bool Insert_BM_SampleOutD(string barCodeNo)
        {
            query = $@"
                        INSERT INTO BM_SampleOutD
                        (
                              CorpId
                            , OutNo
                            , BarCodeNo
                            , ShipToCode
                            , OutDate
                            , Remark
                            , CreId
                            , CreDt
                        )
                        VALUES
                        (
                              '{CorpId}'
                            , '{OutNo}'
                            , '{barCodeNo}'
                            , '{ShipToCode}'
                            , '{OutDate}'
                            , '{Remark}'
                            , '{WrGlobal.LoginID}'
                            , GETDATE()
                        )
                    ";
            return db.ExecuteQuery(query);
        }

        // 디테일 DELETE (OutNo 기준 전체 삭제)
        public bool Delete_BM_SampleOutD()
        {
            query = $@"
                        DELETE FROM BM_SampleOutD
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OutNo = '{OutNo}'
                    ";
            return db.ExecuteQuery(query);
        }

        // 마스터 DELETE (OutNo 기준)
        public bool Delete_BM_SampleOutM()
        {
            query = $@"
                        DELETE FROM BM_SampleOutM
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OutNo = '{OutNo}'
                    ";
            return db.ExecuteQuery(query);
        }
    }
}
