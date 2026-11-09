using SmartCommon.Common;
using System;
using System.Data;
using SmartMES.Common;

namespace SmartMES.Models.QM
{
    public class QM_NonConformanceM
    {
        #region Property

        public string CorpId { get; set; }

        public string NCRNo { get; set; }

        public int Seq { get; set; }

        public string NCRDate { get; set; }

        public string NCRType { get; set; }

        public string InspectionNo { get; set; }

        public string ItemCode { get; set; }

        public string LotNo { get; set; }

        public string ProcessCode { get; set; }

        public string MachineCode { get; set; }

        public string DefectCode { get; set; }

        public decimal? DefectQty { get; set; }

        public string Severity { get; set; }

        public string Status { get; set; }

        public string UseYn { get; set; }

        public string DefectPosition { get; set; }

        public string DefectDescription { get; set; }

        public string ImageFileID { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        //조회용
        public string StartDt { get; set; }
        public string EndDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_QM_NonConformanceM()
        {
            query = $@"
                SELECT '' Status
                    , CorpId
                    , NCRNo
                    , Seq
                    , NCRDate
                    , NCRType
                    , InspectionNo
                    , ItemCode
                    , LotNo
                    , ProcessCode
                    , MachineCode
                    , DefectCode
                    , DefectQty
                    , Severity
                    , Status
                    , UseYn
                    , DefectPosition
                    , DefectDescription
                    , ImageFileID
                    , CreId
                    , CreIP
                    , CreDt
                    , ModId
                    , ModIP
                    , ModDt
                FROM QM_NonConformanceM
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND NCRDate BETWEEN '{StartDt} 00:00:00' AND '{EndDt} 23:59:59'
            ";

            if (NCRType != "" && NCRType.ToUpper() != "ALL")
            {
                query += $"\nAND NCRType = '{NCRType}'";
            }

            query += $"\nORDER BY NCRDate, Seq";

            return db.GetDataTable(query);
        }

        public bool Ins_QM_NonConformanceM()
        {
            query = $@"

                INSERT INTO QM_NonConformanceM
                ( CorpId
                  , NCRNo
                  , Seq
                  , NCRDate
                  , NCRType
                  , InspectionNo
                  , ItemCode
                  , LotNo
                  , ProcessCode
                  , MachineCode
                  , DefectCode
                  , DefectQty
                  , Severity
                  , Status
                  , UseYn
                  , DefectPosition
                  , DefectDescription
                  , ImageFileID
                  , CreId
                  , CreIP
                  , CreDt
                )
                VALUES
                (
                 '{WrGlobal.CorpID}'
                ,'{NCRNo}'
                ,'{Seq}'
                , '{NCRDate.ConvertDate_To_Char10()}'
                , {NCRType.ConvertStr_Empty_To_Null()}
                , {InspectionNo.ConvertStr_Empty_To_Null()}
                , {ItemCode.ConvertStr_Empty_To_Null()}
                , {LotNo.ConvertStr_Empty_To_Null()}
                , {ProcessCode.ConvertStr_Empty_To_Null()}
                , {MachineCode.ConvertStr_Empty_To_Null()}
                , {DefectCode.ConvertStr_Empty_To_Null()}
                , {(DefectQty.ConvertDecimal_Null_To_Zero())}
                , {Severity.ConvertStr_Empty_To_Null()}
                , {Status.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , {DefectPosition.ConvertStr_Empty_To_Null()}
                , {DefectDescription.ConvertStr_Empty_To_Null()}
                , {ImageFileID.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
                )
                ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_NonConformanceM()
        {
            query = $@"

                UPDATE QM_NonConformanceM
                SET
                  NCRDate = '{NCRDate.ConvertDate_To_Char10()}'
                , NCRType = {NCRType.ConvertStr_Empty_To_Null()}
                , InspectionNo = {InspectionNo.ConvertStr_Empty_To_Null()}
                , ItemCode = {ItemCode.ConvertStr_Empty_To_Null()}
                , LotNo = {LotNo.ConvertStr_Empty_To_Null()}
                , ProcessCode = {ProcessCode.ConvertStr_Empty_To_Null()}
                , MachineCode = {MachineCode.ConvertStr_Empty_To_Null()}
                , DefectCode = {DefectCode.ConvertStr_Empty_To_Null()}
                , DefectQty = {(DefectQty.ConvertDecimal_Null_To_Zero())}
                , Severity = {Severity.ConvertStr_Empty_To_Null()}
                , Status = {Status.ConvertStr_Empty_To_Null()}
                , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                , DefectPosition = {DefectPosition.ConvertStr_Empty_To_Null()}
                , DefectDescription = {DefectDescription.ConvertStr_Empty_To_Null()}
                , ImageFileID = {ImageFileID.ConvertStr_Empty_To_Null()}
                , ModId = '{WrGlobal.LoginID}'
                , ModIP = '{WrGlobal.ClientHostName}'
                , ModDt = GETDATE()
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND NCRNo = '{NCRNo}'
                AND Seq = '{Seq}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Del_QM_NonConformanceM()
        {
            query = $@"
                DELETE QM_NonConformanceM
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND NCRNo = '{NCRNo}'
                AND Seq = '{Seq}'
                ";

            return db.ExecuteQuery(query);
        }
    }
}