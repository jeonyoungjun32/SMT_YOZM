using SmartCommon.Common;
using System;
using System.Data;
using SmartMES.Common;

namespace SmartMES.Models.QM
{
    public class QM_NCDisposition
    {
        #region Property

        public string CorpId { get; set; }

        public string DispositionNo { get; set; }

        public string NCRNo { get; set; }

        public string DispositionType { get; set; }

        public decimal DispositionQty { get; set; }

        public string ApprovalNo { get; set; }

        public string ApprovalStatus { get; set; }

        public string UseYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        // 조회용
        public string StartDt { get; set; }

        public string EndDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_QM_NCDisposition()
        {
            query = $@"
                SELECT '' Status
                    , CorpId
                    , DispositionNo
                    , NCRNo
                    , DispositionType
                    , DispositionQty
                    , ApprovalNo
                    , ApprovalStatus
                    , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                    , ModId
                    , ModIP
                    , ModDt
                FROM QM_NCDisposition
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND CreDt BETWEEN '{StartDt} 00:00:00' AND '{EndDt} 23:59:59'
            ";

            if (NCRNo != "")
            {
                query += $"\nAND NCRNo = '{NCRNo}'";
            }

            //if (DispositionNo != "")
            //{
            //    query += $"\nAND DispositionNo = '{DispositionNo}'";
            //}

            query += $"\nORDER BY CreDt, DispositionNo";

            return db.GetDataTable(query);
        }

        public bool Ins_QM_NCDisposition()
        {
            query = $@"
                INSERT INTO QM_NCDisposition
                (
                      CorpId
                    , DispositionNo
                    , NCRNo
                    , DispositionType
                    , DispositionQty
                    , ApprovalNo
                    , ApprovalStatus
                    , UseYn
                    , CreId
                    , CreIP
                    , CreDt
                )
                VALUES
                (
                      '{WrGlobal.CorpID}'
                    , '{DispositionNo}'
                    , {NCRNo.ConvertStr_Empty_To_Null()}
                    , {DispositionType.ConvertStr_Empty_To_Null()}
                    , {DispositionQty.ConvertInt_Null_To_Zero()}
                    , {ApprovalNo.ConvertStr_Empty_To_Null()}
                    , {ApprovalStatus.ConvertStr_Empty_To_Null()}
                    , {UseYn.ConvertStr_Empty_To_Null()}
                    , '{WrGlobal.LoginID}'
                    , '{WrGlobal.ClientHostName}'
                    , GETDATE()
                )
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_NCDisposition()
        {
            query = $@"
                UPDATE QM_NCDisposition
                SET
                      DispositionType = {DispositionType.ConvertStr_Empty_To_Null()}
                    , DispositionQty = {DispositionQty.ConvertInt_Null_To_Zero()}
                    , ApprovalNo = {ApprovalNo.ConvertStr_Empty_To_Null()}
                    , ApprovalStatus = {ApprovalStatus.ConvertStr_Empty_To_Null()}
                    , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                    , ModId = '{WrGlobal.LoginID}'
                    , ModIP = '{WrGlobal.ClientHostName}'
                    , ModDt = GETDATE()
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND DispositionNo = '{DispositionNo}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_QM_NCDisposition()
        {
            query = $@"
                DELETE QM_NCDisposition
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND DispositionNo = '{DispositionNo}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}