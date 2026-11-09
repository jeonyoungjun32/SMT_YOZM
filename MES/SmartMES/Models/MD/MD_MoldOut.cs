using System.Data;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.MD
{
    public class MD_MoldOut
    {
        #region Property

        public string OutNo { get; set; }

        public int OutRowSeq { get; set; }

        public int DispSeq { get; set; }

        public string OutDate { get; set; }

        public string CustCode { get; set; }

        public string MoldCode { get; set; }

        public decimal OutQty { get; set; }

        public string Remark { get; set; }

        public string InNo { get; set; }

        public int InRowSeq { get; set; }

        public int OutType { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_MD_MoldOut(string outFrom, string outTo)
        {
            MoldCode = MoldCode.ConvertString_Null_To_Empty();
            CustCode = CustCode.ConvertString_Null_To_Empty();

            query = $@"
                        SELECT
                              '' AS Status
                            , MO.OutNo
                            , MO.OutRowSeq
                            , MO.DispSeq
                            , CONVERT(DATE, MO.OutDate) AS OutDate
                            
                            --, MO.CustCode
                            --, MO.CustCode AS Cust_Nm
                            , MO.MoldCode
                            , MO.MoldCode AS MoldName
                            , MD.MoldMgmtNo
                            , MD.MoldName
                            , MD.MoldSN
                            , MD.Chasu
                            
                            , MO.OutQty
                            , MO.Remark
                            , MO.InNo
                            , MO.InRowSeq
                            , MO.CreId
                            , MO.CreDt
                            , MO.ModId
                            , MO.ModDt
                            , MO.OutType

                            , MD.ProdtCust
                            , MD.SupplyCust AS CustCode
                            , MD.MoldPjtNo
                        FROM MD_MoldOut MO
                        INNER JOIN MD_Mold MD ON MO.CorpId = MD.CorpId AND MO.MoldCode = MD.MoldCode 
                        WHERE MO.CorpId = '{WrGlobal.CorpID}'
                        AND MO.OutDate BETWEEN '{outFrom}' AND '{outTo}'
                    ";

            if(MoldCode != "ALL" && MoldCode != "")
            {
                query += $"\n AND MO.MoldCode = '{MoldCode}' ";
            }
            if (CustCode.ToUpper() != "ALL" && CustCode != null && CustCode != "")
            {
                query += $"\n AND MO.CustCode = '{CustCode}' ";
            }

            return db.GetDataTable(query);
        }

        public bool Ins_MoldOut()
        {
            query = $@"
                    INSERT INTO MD_MoldOut
                    (
                        CorpId
                        , OutNo
                        , OutRowSeq
                        , OutType
                        , DispSeq
                        , OutDate
                        , CustCode
                        , MoldCode
                        , OutQty
                        , Remark
                        , InNo
                        , InRowSeq
                        , CreId
                        , CreIP
                        , CreDt
                    )
                    VALUES
                    (
                        '{WrGlobal.CorpID}'
                        , '{OutNo}'
                        , '{OutRowSeq}'
                        , '{OutType}'
                        , {DispSeq.ConvertInt_Null_To_Zero()}
                        , {OutDate.ConvertStr_Empty_To_Null()}
                        , {CustCode.ConvertStr_Empty_To_Null()}
                        , {MoldCode.ConvertStr_Empty_To_Null()}
                        , {OutQty.ConvertNumeric_Empty_To_Null()}
                        , {Remark.ConvertStr_Empty_To_Null()}
                        , {InNo.ConvertStr_Empty_To_Null()}
                        , {InRowSeq.ConvertNumeric_Empty_To_Null()}
                        , '{WrGlobal.LoginID}'
                        , '{WrGlobal.ClientHostName}'
                        , GETDATE()
                    )

                     UPDATE MD_Mold
                        SET UseYn = 'N'
                      WHERE MoldCode = {MoldCode.ConvertStr_Empty_To_Null()}
                    
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_MoldOut()
        {
            query = $@"
                        UPDATE MD_MoldOut
                          SET  OutDate = {OutDate.ConvertStr_Empty_To_Null()}
                             , CustCode = {CustCode.ConvertStr_Empty_To_Null()}
                             , MoldCode = {MoldCode.ConvertStr_Empty_To_Null()}
                             , OutQty = {OutQty.ConvertNumeric_Empty_To_Null()}
                             , Remark = {Remark.ConvertStr_Empty_To_Null()}
                             , OutType = '{OutType}'
                            , ModId = '{WrGlobal.LoginID}' 
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OutNo = '{OutNo}'
                        AND OutRowSeq = '{OutRowSeq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_MoldOut()
        {
            query = $@"
                        DELETE MD_MoldOut
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND OutNo = '{OutNo}'
                        AND OutRowSeq = '{OutRowSeq}'

                        UPDATE MD_Mold
                           SET UseYn = 'Y'
                         WHERE MoldCode = {MoldCode.ConvertStr_Empty_To_Null()}
                    ";

            return db.ExecuteQuery(query);
        }

        public DataTable Select_MoldOut_ByMoldCode()
        {
            query = $@"
                    SELECT *
                    FROM MD_MoldOut
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND MoldCode = '{MoldCode}'
                    ";

            return db.GetDataTable(query);
        }
    }
}
