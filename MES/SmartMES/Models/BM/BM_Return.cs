using SmartMES.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCommon.Common;
using System.Data;

namespace SmartMES.Models.BM
{
    class BM_Return
    {
        #region Property
        public string CorpId { get; set; }

        public string ReturnNo { get; set; }

        public string Type { get; set; }

        public string Reason { get; set; }

        public string ReturnDate { get; set; }

        public string InDate { get; set; }

        public string ItemCode { get; set; }

        public int Qty { get; set; }

        public string SdOrderNo { get; set; }

        public int SdOrderSeq { get; set; }

        public string SalesNo { get; set; }

        public int SalesSeq { get; set; }

        public int Price { get; set; }
        public int VatPrice { get; set; }
        public int TotalPrice { get; set; }
        public string StockYn { get; set; }

        #endregion Property

        Database db = new Database();
        string query = "";

        public DataTable SelectReturn_BySalesNoSeq()
		{
            query = $@"SELECT re.CorpId
                              , ReturnNo
                              , re.Type
                              , ci.Title
                              , Reason
                              , ReturnDate
	                          , cm.ItemName
                              , InDate
                              , re.ItemCode
                              , re.Qty
                              , Price
                              , SdOrderNo
                              , SdOrderSeq
                              , SalesNo
                              , SalesSeq
							  , CASE 
								WHEN EXISTS (
									SELECT 1 
									FROM ST_StockD sd 
									WHERE sd.JobNo = re.ReturnNo 
									AND sd.Type = '반품'
								) THEN '입고' 
								ELSE '미입고'  
							  END AS IsInStock
                            , StockYn
                        FROM BM_Return re
                        LEFT JOIN CM_CateItem ci on re.Type = ci.ClassItemCode AND ci.CLASS = '103010'
                        LEFT JOIN CM_Item cm on cm.CorpId = re.CorpId AND cm.ItemCode = re.ItemCode
                        WHERE re.CorpId = '{WrGlobal.CorpID}' 
                        AND SalesNo = '{SalesNo}' 
                        AND SalesSeq = {SalesSeq}
                       ";

            return db.GetDataTable(query);
        }

        public void Insert_BM_Return()
        {
            query = $@" INSERT INTO BM_Return (
                            CorpId, ReturnNo, [Type], Reason, ReturnDate, ItemCode, Qty
                            , SdOrderNo, SdOrderSeq, SalesNo, SalesSeq, Price
                            , CreId, CreIP, CreDt, StockYn
                        ) 
                        VALUES (  '{WrGlobal.CorpID}', '{ReturnNo}', '{Type}', '{Reason}', '{ReturnDate}', '{ItemCode}', {Qty}
                                    , '{SdOrderNo}', '{SdOrderSeq}', '{SalesNo}', '{SalesSeq}', '{Price}'
                                     , '{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}', GETDATE(), '{StockYn}') ";
            db.ExecuteQuery(query);
        }

        public void Update_BM_Return()
        {
            query = $@" UPDATE BM_Return
                        SET InDate = '{InDate}'
                        WHERE ReturnNo = '{ReturnNo}' 
                        ";
            db.ExecuteQuery(query);
        }

        public void Delete_BM_Return()
        {
            query = $@"
                DELETE BM_Return 
                WHERE CorpId = '{WrGlobal.CorpID}' AND ReturnNo = '{ReturnNo}'
                ";

            db.ExecuteQuery(query);
        }
    }
}
