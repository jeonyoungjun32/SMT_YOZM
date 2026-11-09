using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.QM
{
	class QM_HaccpExamResult
	{
        #region Property
        public string CorpId { get; set; }

        public string ExamNo { get; set; }

        public int ExamSeq { get; set; }

        public int DispSeq { get; set; }

        public string ExamDate { get; set; }

        public string CcpDivCd { get; set; }

        public string TargetDivCd { get; set; }

        public string ExamCd { get; set; }

        public string RefNo { get; set; }

        public int RefSeq { get; set; }

        public decimal ExamQty { get; set; }

        public string ResultValue { get; set; }

        public string PassDivCd { get; set; }

        public string ExamEmpId { get; set; }

        public string MachineCode { get; set; }

        public string LotNo { get; set; }

        public string ActionMemo { get; set; }

        public string Remark { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime ModDt { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_QM_HaccpExamResult()
        {
            query = $@"
                SELECT '' AS Status
                     , hr.CorpId
                     , hr.ExamNo
                     , hr.ExamSeq
                     , hr.DispSeq
                     , hr.ExamDate
                     , hr.CcpDivCd
                     , hr.TargetDivCd
                     , hr.ExamCd
                     , hr.RefNo
                     , hr.RefSeq
                     , hr.ExamQty
                     , hr.ResultValue
                     , hr.PassDivCd

                     , hr.ExamEmpId
                     , us.EmployeeName AS ExamEmpName

                     , hr.MachineCode
                     , hr.LotNo
                     , hr.ActionMemo
                     , hr.Remark
                     , hr.CreId
                     , hr.CreIP
                     , hr.CreDt
                     , hr.ModId
                     , hr.ModIP
                     , hr.ModDt

                     , hs.ExamNm
                     , hs.DispSeq
                     , hs.Spec
                     , hs.Method
                     , hs.Cycle
                     , hs.ValueTypeDivCd
                     , hs.DecimalCnt
                     , hs.DefaultValue
                     , hs.Unit
                     , hs.MgntMin
                     , hs.MgntMax

                  FROM QM_HaccpExamResult hr
                  LEFT JOIN QM_HaccpStd hs ON hr.CorpId = hs.CorpId AND hr.ExamCd = hs.ExamCd
                  LEFT JOIN SM_User us ON hr.CorpId = us.CorpId AND hr.ExamEmpId = us.EmployeeId
                 WHERE hr.CorpId = '{WrGlobal.CorpID}'
                    ";

            if (CcpDivCd.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND hr.CcpDivCd = '{CcpDivCd}'";
            }

            if (TargetDivCd.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND hr.TargetDivCd = '{TargetDivCd}'";
            }

            if (RefNo.ConvertString_Null_To_Empty() != "")
			{
                query += $"\nAND hr.RefNo = '{RefNo}'";
			}

            if (RefSeq.ConvertInt_Null_To_Zero() != 0)
            {
                query += $"\nAND hr.RefSeq = '{RefSeq}'";
            }

            return db.GetDataTable(query);
        }

        public DataRow Select_ExamResult_Exsits()
        {
            //참조 번호, 순번 기준이 아닐 시 일자, ccp, 관리대상 구분으로 변경해야함
            query = $@"SELECT TOP 1 ExamNo
                         FROM QM_HaccpExamResult
                        WHERE RefNo = '{RefNo}'
                          AND RefSeq = {RefSeq}";

            if (CcpDivCd != "" && CcpDivCd != null)
            {
                query += $"\nAND CcpDivCd = '{CcpDivCd}' ";
            }

            if (TargetDivCd != "" && TargetDivCd != null)
            {
                query += $"\nAND TargetDivCd = '{TargetDivCd}' ";
            }

            query += $"\nORDER BY ExamNo, ExamSeq DESC";

            return db.GetDataRow(query);
        }

        public bool Ins_QM_HaccpExamResult()
        {
            query = $@"
                INSERT INTO QM_HaccpExamResult
                (
                      CorpId
                    , ExamNo
                    , ExamSeq
                    , DispSeq
                    , ExamDate
                    , CcpDivCd
                    , TargetDivCd
                    , ExamCd
                    , RefNo
                    , RefSeq
                    , ExamQty
                    , ResultValue
                    , PassDivCd
                    , ExamEmpId
                    , MachineCode
                    , LotNo
                    , ActionMemo
                    , Remark
                    , CreId
                    , CreIP
                    , CreDt
                )
                VALUES
                (
                      '{WrGlobal.CorpID}'
                    , {ExamNo.ConvertStr_Empty_To_Null()}
                    , {ExamSeq.ConvertNumeric_Empty_To_Null()}
                    , {DispSeq.ConvertNumeric_Empty_To_Null()}
                    , {ExamDate.ConvertStr_Empty_To_Null()}
                    , {CcpDivCd.ConvertStr_Empty_To_Null()}
                    , {TargetDivCd.ConvertStr_Empty_To_Null()}
                    , {ExamCd.ConvertStr_Empty_To_Null()}
                    , {RefNo.ConvertStr_Empty_To_Null()}
                    , {RefSeq.ConvertNumeric_Empty_To_Null()}
                    , {ExamQty.ConvertNumeric_Empty_To_Null()}
                    , {ResultValue.ConvertStr_Empty_To_Null()}
                    , {PassDivCd.ConvertStr_Empty_To_Null()}
                    , {ExamEmpId.ConvertStr_Empty_To_Null()}
                    , {MachineCode.ConvertStr_Empty_To_Null()}
                    , {LotNo.ConvertStr_Empty_To_Null()}
                    , {ActionMemo.ConvertStr_Empty_To_Null()}
                    , {Remark.ConvertStr_Empty_To_Null()}
                    , '{WrGlobal.LoginID}'
                    , '{WrGlobal.ClientHostName}'
                    , GETDATE()
                )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_HaccpExamResult()
        {
            query = $@"
                UPDATE QM_HaccpExamResult
                   SET DispSeq = {DispSeq.ConvertNumeric_Empty_To_Null()}
                     , ExamDate = {ExamDate.ConvertStr_Empty_To_Null()}
                     , CcpDivCd = {CcpDivCd.ConvertStr_Empty_To_Null()}
                     , TargetDivCd = {TargetDivCd.ConvertStr_Empty_To_Null()}
                     , ExamCd = {ExamCd.ConvertStr_Empty_To_Null()}
                     , RefNo = {RefNo.ConvertStr_Empty_To_Null()}
                     , RefSeq = {RefSeq.ConvertNumeric_Empty_To_Null()}
                     , ExamQty = {ExamQty.ConvertNumeric_Empty_To_Null()}
                     , ResultValue = {ResultValue.ConvertStr_Empty_To_Null()}
                     , PassDivCd = {PassDivCd.ConvertStr_Empty_To_Null()}
                     , ExamEmpId = {ExamEmpId.ConvertStr_Empty_To_Null()}
                     , MachineCode = {MachineCode.ConvertStr_Empty_To_Null()}
                     , LotNo = {LotNo.ConvertStr_Empty_To_Null()}
                     , ActionMemo = {ActionMemo.ConvertStr_Empty_To_Null()}
                     , Remark = {Remark.ConvertStr_Empty_To_Null()}
                     , ModId = '{WrGlobal.LoginID}'
                     , ModIP = '{WrGlobal.ClientHostName}'
                     , ModDt = GETDATE()
                 WHERE CorpId = '{WrGlobal.CorpID}'
                   AND ExamNo = '{ExamNo}'
                   AND ExamSeq = '{ExamSeq}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Del_QM_HaccpExamResult()
        {
            query = $@"
            DELETE QM_HaccpExamResult
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND ExamNo = '{ExamNo}'
               AND ExamSeq = '{ExamSeq}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
