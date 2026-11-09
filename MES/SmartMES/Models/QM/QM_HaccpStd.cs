using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.Models.QM
{
	class QM_HaccpStd
    {
        #region Property
        public string CorpId { get; set; }

        public string CcpDivCd { get; set; }

        public string ExamCd { get; set; }

        public string ExamNm { get; set; }

        public string TargetDivCd { get; set; }

        public decimal DispSeq { get; set; }

        public string Spec { get; set; }

        public string Method { get; set; }

        public string Cycle { get; set; }

        public string ValueTypeDivCd { get; set; }

        public int DecimalCnt { get; set; }

        public string DefaultValue { get; set; }

        public string Unit { get; set; }

        public decimal MgntMin { get; set; }

        public decimal MgntMax { get; set; }

        public decimal DangerMin { get; set; }

        public decimal DangerMax { get; set; }

        public string UseYn { get; set; }

        public string ItemClassCd { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime ModDt { get; set; }
        #endregion Property End

        Database db = new Database();
        string query;


        public DataTable Select_QM_HaccpStd()
        {
            CcpDivCd = CcpDivCd.ConvertString_Null_To_Empty();
            TargetDivCd = TargetDivCd.ConvertString_Null_To_Empty();
            ItemClassCd = ItemClassCd.ConvertString_Null_To_Empty();

            query = $@"

                SELECT '' AS Status
                     , st.CorpId
                     , st.CcpDivCd
                     , st.ExamCd
                     , st.ExamNm
                     , st.TargetDivCd
                     , st.DispSeq
                     , st.Spec
                     , st.Method
                     , st.Cycle
                     , st.ValueTypeDivCd
                     , st.DecimalCnt
                     , st.DefaultValue
                     , st.Unit
                     , st.MgntMin
                     , st.MgntMax
                     , st.DangerMin
                     , st.DangerMax
                     , st.UseYn
                     , st.ItemClassCd
                     , st.Option1
                     , st.Option2
                     , st.Option3
                     , st.CreId
                     , st.CreIP
                     , st.CreDt
                     , st.ModId
                     , st.ModIP
                     , st.ModDt

                    --검사용
                    , '' AS ExamNo
                    , NULL ExamSeq
                    , GETDATE() ExamDate
                    , '' AS RefNo
                    , NULL AS RefSeq
                    , NULL AS ExamQty
                    , '' AS ResultValue
                    , '10' AS PassDivCd
                    , '{WrGlobal.LoginID}' AS ExamEmpId
                    , '' AS MachineCode
                    , '' AS LotNo
                    , '' AS ActionMemo
                    , '' AS Remark

                  FROM QM_HaccpStd st
                 WHERE st.CorpId = '{WrGlobal.CorpID}'
                 AND UseYn = 'Y'
                "
                ;

            if(CcpDivCd  != "" && CcpDivCd != "ALL")
			{
                query += $"\nAND st.CcpDivCd = '{CcpDivCd}'";
			}

            if (TargetDivCd != "" && TargetDivCd != "ALL")
            {
                query += $"\nAND st.TargetDivCd = '{TargetDivCd}'";
            }

            if (ItemClassCd != "" && ItemClassCd != "ALL")
            {
                query += $"\nAND st.ItemClassCd = '{ItemClassCd}'";
            }

            query += $"\nORDER BY st.CcpDivCd, st.ItemClassCd, st.DispSeq";

            return db.GetDataTable(query);
        }

        public bool Ins_QM_HaccpStd()
        {
            query = $@"
            INSERT INTO QM_HaccpStd
            (
                  CorpId
                , CcpDivCd
                , ExamCd
                , ExamNm
                , TargetDivCd
                , DispSeq
                , Spec
                , Method
                , Cycle
                , ValueTypeDivCd
                , DecimalCnt
                , DefaultValue
                , Unit
                , MgntMin
                , MgntMax
                , DangerMin
                , DangerMax
                , UseYn
                , ItemClassCd
                , Option1
                , Option2
                , Option3
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                  '{WrGlobal.CorpID}'
                , {CcpDivCd.ConvertStr_Empty_To_Null()}
                , IIF('{ExamCd}' = '',  (dbo.[FN_Create_Cd_Exam_CCP]('{WrGlobal.CorpID}')), '{ExamCd}')
                , {ExamNm.ConvertStr_Empty_To_Null()}
                , {TargetDivCd.ConvertStr_Empty_To_Null()}
                , {DispSeq.ConvertNumeric_Empty_To_Null()}
                , {Spec.ConvertStr_Empty_To_Null()}
                , {Method.ConvertStr_Empty_To_Null()}
                , {Cycle.ConvertStr_Empty_To_Null()}
                , {ValueTypeDivCd.ConvertStr_Empty_To_Null()}
                , {DecimalCnt.ConvertNumeric_Empty_To_Null()}
                , {DefaultValue.ConvertStr_Empty_To_Null()}
                , {Unit.ConvertStr_Empty_To_Null()}
                , {MgntMin.ConvertNumeric_Empty_To_Null()}
                , {MgntMax.ConvertNumeric_Empty_To_Null()}
                , {DangerMin.ConvertNumeric_Empty_To_Null()}
                , {DangerMax.ConvertNumeric_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , {ItemClassCd.ConvertStr_Empty_To_Null()}
                , {Option1.ConvertStr_Empty_To_Null()}
                , {Option2.ConvertStr_Empty_To_Null()}
                , {Option3.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            )";

            return db.ExecuteQuery(query);
        }
        public bool Upd_QM_HaccpStd()
        {
            query = $@"
UPDATE QM_HaccpStd
   SET ExamNm = {ExamNm.ConvertStr_Empty_To_Null()}
     , TargetDivCd = {TargetDivCd.ConvertStr_Empty_To_Null()}
     , DispSeq = {DispSeq.ConvertNumeric_Empty_To_Null()}
     , Spec = {Spec.ConvertStr_Empty_To_Null()}
     , Method = {Method.ConvertStr_Empty_To_Null()}
     , Cycle = {Cycle.ConvertStr_Empty_To_Null()}
     , ValueTypeDivCd = {ValueTypeDivCd.ConvertStr_Empty_To_Null()}
     , DecimalCnt = {DecimalCnt.ConvertNumeric_Empty_To_Null()}
     , DefaultValue = {DefaultValue.ConvertStr_Empty_To_Null()}
     , Unit = {Unit.ConvertStr_Empty_To_Null()}
     , MgntMin = {MgntMin.ConvertNumeric_Empty_To_Null()}
     , MgntMax = {MgntMax.ConvertNumeric_Empty_To_Null()}
     , DangerMin = {DangerMin.ConvertNumeric_Empty_To_Null()}
     , DangerMax = {DangerMax.ConvertNumeric_Empty_To_Null()}
     , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
     , ItemClassCd = {ItemClassCd.ConvertStr_Empty_To_Null()}
     , Option1 = {Option1.ConvertStr_Empty_To_Null()}
     , Option2 = {Option2.ConvertStr_Empty_To_Null()}
     , Option3 = {Option3.ConvertStr_Empty_To_Null()}
     , ModId = '{WrGlobal.LoginID}'
     , ModIP = '{WrGlobal.ClientHostName}'
     , ModDt = GETDATE()
 WHERE CorpId = '{WrGlobal.CorpID}'
   AND CcpDivCd = '{CcpDivCd}'
   AND ExamCd = '{ExamCd}'
";

            return db.ExecuteQuery(query);
        }
        public bool Del_QM_HaccpStd()
        {
            query = $@"
DELETE QM_HaccpStd
 WHERE CorpId = '{WrGlobal.CorpID}'
   AND CcpDivCd = '{CcpDivCd}'
   AND ExamCd = '{ExamCd}'
";

            return db.ExecuteQuery(query);
        }

    }
}
