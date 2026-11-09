using DevExpress.XtraRichEdit.Model;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.BM
{
	class BM_EstimateM
	{
        #region Property
        public string CorpId { get; set; }

        public string EstimateNo { get; set; }

        public string Date { get; set; }

        public string State { get; set; }

        public string Name { get; set; }

        public string CustCode { get; set; }

        public decimal Price { get; set; }

        public decimal VatPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string VatType { get; set; }

        public int VatRate { get; set; }

        public string Manager { get; set; }

        public string Remark { get; set; }
        #endregion

        Database db = new Database();
        string query = "";


        public DataTable Select_BM_EstimateM(string startDate, string endDate)
        {
            query = $@"SELECT 
                              CorpId
                            , EstimateNo
                            , Date
                            , State
                            , Name
                            , CustCode
                            , Price
                            , VatPrice
                            , TotalPrice
                            , VatType
                            , VatRate
                            , Manager
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                         FROM BM_EstimateM
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                          AND Date BETWEEN '{startDate}' AND '{endDate}'
                   ";

            if (CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $" AND CustCode = '{CustCode}'";
            }

            if (State.ConvertString_Null_To_Empty() != "")
            {
                query += $" AND State = '{State}'";
            }

            query += " ORDER BY CreDt DESC";

            return db.GetDataTable(query);
        }

        public DataRow Select_BM_EstimateM_Row()
        {
            query = $@"SELECT 
                              CorpId
                            , EstimateNo
                            , Date
                            , State
                            , Name
                            , CustCode
                            , Price
                            , VatPrice
                            , TotalPrice
                            , VatType
                            , VatRate
                            , Manager
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                         FROM BM_EstimateM
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND EstimateNo = '{EstimateNo}'";

            return db.GetDataRow(query);
        }




        public bool Ins_BM_EstimateM()
        {
            db.Edit_Property(this);

            query = $@"
                       INSERT INTO BM_EstimateM(
                                  CorpId
                                , EstimateNo
                                , Date
                                , State
                                , Name
                                , CustCode
                                , Price
                                , VatPrice
                                , TotalPrice
                                , VatType
                                , VatRate
                                , Manager
                                , Remark
                                , CreId
                                , CreIP
                                , CreDt
                                )
                                VALUES (
                                 '{WrGlobal.CorpID}'
                                , '{EstimateNo}'
                                , '{Date}'
                                , '대기'
                                , {Name.ConvertStr_Empty_To_Null()}
                                , {CustCode.ConvertStr_Empty_To_Null()}
                                , {Price.ConvertNumeric_Empty_To_Null()}
                                , {VatPrice.ConvertNumeric_Empty_To_Null()}
                                , {TotalPrice.ConvertNumeric_Empty_To_Null()}
                                , {VatType.ConvertStr_Empty_To_Null()}
                                , {VatRate.ConvertInt_Null_To_Zero()}
                                , {Manager.ConvertStr_Empty_To_Null()}
                                , {Remark.ConvertStr_Empty_To_Null()}
                                , '{WrGlobal.LoginID}'
                                , '{WrGlobal.IpAddress}'
                                , getdate()
                                )
                                ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_BM_EstimateM()
        {
            db.Edit_Property(this);

            query = $@"
                        UPDATE BM_EstimateM
                        SET
                          Date     = '{Date}'
                        , Name          = {Name.ConvertStr_Empty_To_Null()}
                        , CustCode      = {CustCode.ConvertStr_Empty_To_Null()}
                        , Price         = {Price.ConvertDecimal_Null_To_Zero()}
                        , VatPrice      = {VatPrice.ConvertDecimal_Null_To_Zero()}
                        , TotalPrice    = {TotalPrice.ConvertDecimal_Null_To_Zero()}
                        , VatType       = {VatType.ConvertStr_Empty_To_Null()}
                        , VatRate       = {VatRate.ConvertDecimal_Null_To_Zero()}                        
                        , Manager       = {Manager.ConvertStr_Empty_To_Null()}
                        , Remark        = {Remark.ConvertStr_Empty_To_Null()}
                        , ModId         = '{WrGlobal.LoginID}'
                        , ModIP         = '{WrGlobal.IpAddress}'
                        , ModDt         = GETDATE()
                        WHERE CorpId = '{CorpId}'
                        AND EstimateNo = '{EstimateNo}'

                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_BM_EstimateM_State()
        {
            query = $@"
                        UPDATE BM_EstimateM
                        SET
                          State  = '수주'
                        , ModId         = '{WrGlobal.LoginID}'
                        , ModIP         = '{WrGlobal.IpAddress}'
                        , ModDt         = GETDATE()
                            WHERE CorpId = '{CorpId}'
                            AND EstimateNo = '{EstimateNo}'

                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_BM_EstimateM_State_Rollback()
        {
            query = $@"
                        UPDATE BM_EstimateM
                        SET
                          State  = '대기'
                        , ModId         = '{WrGlobal.LoginID}'
                        , ModIP         = '{WrGlobal.IpAddress}'
                        , ModDt         = GETDATE()
                            WHERE CorpId = '{CorpId}'
                            AND EstimateNo = '{EstimateNo}'

            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_BM_EstimateM()
        {
            query = $@"
                        DELETE BM_EstimateM 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND EstimateNo = '{EstimateNo}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
