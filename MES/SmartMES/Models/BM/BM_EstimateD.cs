using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.BM
{
	class BM_EstimateD
	{
        #region Property
        public string CorpId { get; set; }

        public string EstimateNo { get; set; }

        public int Seq { get; set; }

        public string ItemCode { get; set; }

        public decimal Qty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Price { get; set; }

        public string Remark { get; set; }

        public string SalesRemark { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_BM_EstimateD()
        {
            query = $@"SELECT '' Status
                            , ed.CorpId
                            , ed.EstimateNo
                            , ed.Seq
                            , ed.ItemCode
                            , ed.Qty
                            , ed.UnitPrice
                            , ed.Price
                            , NULLIF(REPLACE(REPLACE(cm.Size, char(9), ''), ' ', ''), '') as Size
                            , ed.Remark
                            , ed.CreId
                            , ed.CreIP
                            , ed.CreDt
                            , ed.ModId
                            , ed.ModIP
                            , ed.ModDt
                            , ed.ModDt
                            , ed.SalesRemark
                        FROM BM_EstimateD ed
                  INNER JOIN CM_Item cm ON ed.CorpId = cm.CorpId and ed.ItemCode = cm.ItemCode 
                        WHERE ed.CorpId = '{WrGlobal.CorpID}'
                          AND ed.EstimateNo = '{EstimateNo}'";

            return db.GetDataTable(query);
        }

        public bool Ins_BM_EstimateD()
        {
            db.Edit_Property(this);

            query = $@"
                      INSERT INTO dbo.BM_EstimateD
                                (
                                     CorpId
                                    , EstimateNo
                                    , Seq
                                    , ItemCode
                                    , Qty
                                    , UnitPrice
                                    , Price
                                    , Remark
                                    , CreId
                                    , CreIP
                                    , CreDt
                                    , SalesRemark
                                )
                                VALUES (
                                 '{WrGlobal.CorpID}'
                                , '{EstimateNo}'
                                , '{Seq}'
                                , {ItemCode.ConvertStr_Empty_To_Null()}
                                , {Qty.ConvertInt_Null_To_Zero()}
                                , {UnitPrice.ConvertNumeric_Empty_To_Null()}
                                , {Price.ConvertNumeric_Empty_To_Null()}
                                , {Remark.ConvertStr_Empty_To_Null()}
                                , '{WrGlobal.LoginID}'
                                , '{WrGlobal.ClientHostName}'
                                , getdate()
                                , {SalesRemark.ConvertStr_Empty_To_Null()}
                                )
                                ";

            return db.ExecuteQuery(query);
        }


        public bool Upd_BM_EstimateD()
        {
            db.Edit_Property(this);

            query = $@"
                        UPDATE BM_EstimateD
                        SET
                          ItemCode  = {ItemCode.ConvertStr_Empty_To_Null()}
                        , Qty           = {Qty.ConvertInt_Null_To_Zero()}
                        , UnitPrice     = {UnitPrice.ConvertDecimal_Null_To_Zero()}
                        , Price         = {Price.ConvertDecimal_Null_To_Zero()}
                        , Remark        = {Remark.ConvertStr_Empty_To_Null()}
                        , SalesRemark   = {SalesRemark.ConvertStr_Empty_To_Null()}
                        , ModId         = '{WrGlobal.LoginID}'
                        , ModIP         = '{WrGlobal.ClientHostName}'
                        , ModDt         = GETDATE()
                            WHERE CorpId = '{CorpId}'
                            AND EstimateNo = '{EstimateNo}'
                            AND Seq = {Seq}

                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_BM_EstimateD()
        {
            query = $@"
                        DELETE BM_EstimateD 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND EstimateNo = '{EstimateNo}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_BM_EstimateDBySeq()
        {
            query = $@"
                        DELETE BM_EstimateD 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND EstimateNo = '{EstimateNo}'
                        AND Seq = '{Seq}'
                    ";

            return db.ExecuteQuery(query);
        }

    }
}
