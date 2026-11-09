
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.QM
{
    class QM_ExamResultDaily
    {
        #region Property

        public string ExamNo { get; set; }//검사번호
        public int ExamNoSeq { get; set; }//검사순번
        public int DispSeq { get; set; }//표시순번
        public string ExamDate { get; set; }//검사일자
        public string ItemCode { get; set; }//품목
        public string ExamDivCd { get; set; }//검사구분
        public string ExamCd { get; set; }//검사코드
        public string RefNo { get; set; }//참조번호
        public int RefNoSeq { get; set; }//참조순번
        public float ExamQty { get; set; }//검사수량
        public string MeasureDivCd { get; set; }//측정구분
        public string MeasureTime { get; set; }//측정시간
        public string X1 { get; set; }//X1
        public string X2 { get; set; }//X2
        public string X3 { get; set; }//X3
        public string X4 { get; set; }//X4
        public string X5 { get; set; }//X5
        public string X6 { get; set; }//X6
        public string PassDivCd { get; set; }//판정여부
        public string ActionMemo { get; set; }//조치내역
        public string Remark { get; set; }//비고
        public string InFilePath { get; set; }
        public string ExamEmpId { get; set; }
        public string MachineCode { get; set; }//거래처
        public string FileExt { get; set; }//파일확장자
        public string PassDate { get; set; }//판정일자

        #endregion Property End

        Database db = new Database();
        string query;


        public DataTable SelectExamResultByExamDate(string ValueType = "")
        {
            //검사내역만 등록하는 화면에서 조회 하는 쿼리, 일자 기준, 품목코드 입력 X
            //ValueType(CmCd : 017) - 10 : 합/불, 20 : 값
            query = $@"SELECT qer.ExamNo 
                            , qer.ExamNoSeq
                            , qer.ItemCode
                            , qer.ExamCd
                            , em.ExamNm
                            , em.ExamSpec
                            , em.ExamMethod
                            , em.ExamUnit
                            , qer.X1
                            , qer.X2
                            , qer.X3
                            , qer.X4
                            , qer.X5
                            , qer.X6
                            , qer.PassDivCd
                            , qer.ActionMemo
                            , qer.Remark

                            --개발용
                            , qer.MeasureDivCd
                            , qer.MeasureTime
                            , qer.InFilePath
                            , qer.FileExt
                            , CONVERT(DATE, qer.ExamDate) AS ExamDate

                            , qer.CreId
                            , qer.CreIP
                            , qer.CreDt
                            , qer.ModId
                            , qer.ModIP
                            , qer.ModDt
                            , qer.ExamEmpId
                            , qer.MachineCode
                            
                            , em.ExamCnt
                            , em.ValueTypeDivCd

                         FROM QM_ExamResultDaily qer
                    LEFT JOIN QM_ExamMgnt em ON em.ExamCd = qer.ExamCd   AND qer.ExamDivCd = em.ExamDivCd     AND em.CorpId = qer.CorpId
                    WHERE qer.CorpId = '{WrGlobal.CorpID}'
                    AND qer.ExamDate = '{ExamDate}'
                    ";

            if (ValueType != "")
            {
                //검사값 구분(합/불, 숫자형 구분)
                query += $"\nAND iem.ValueTypeDivCd = '{ValueType}'";
            }

            //검사구분
            if (ExamDivCd != null)
            {
                query += $"\nAND qer.ExamDivCd = '{ExamDivCd}'";
            }

            //초중종같은 측정구분(초물, 중물,종물)이 있는 검사일경우 사용
            if (MeasureDivCd != null && MeasureDivCd != "")
            {
                query += $"\nAND qer.MeasureDivCd = '{MeasureDivCd}'";
            }

            //설비
            if (MachineCode != null && MachineCode != "")
            {
                query += $"\nAND qer.MachineCode = '{MachineCode}'";
            }


            query += "\nORDER BY qer.DispSeq";

            return db.GetDataTable(query);
        }

        public DataRow SelectExamResultExsitsDaily()
        {
            query = $@"SELECT TOP 1 ExamNo
                            , ExamNoSeq
                            , InFilePath
                            , FileExt
                            , MeasureDivCd
                         FROM QM_ExamResultDaily
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ExamDate = '{ExamDate}'
                          AND ExamDivCd = '{ExamDivCd}'";

            if (MeasureDivCd != "" && MeasureDivCd != null)
            {
                query += $"\nAND MeasureDivCd = '{MeasureDivCd}' ";
            }

            query += $"\nORDER BY ExamNo, ExamNoSeq DESC";

            return db.GetDataRow(query);
        }


        public bool InsExamResultDaily()
        {
            query = $@"INSERT INTO QM_ExamResultDaily(CorpId, ExamNo, ExamNoSeq, DispSeq, ExamDate
                                               , ItemCode, ExamDivCd, ExamCd, RefNo, RefNoSeq
                                               , ExamQty, MeasureDivCd, MeasureTime
                                               , X1, X2, X3, X4, X5, X6
                                               , PassDivCd, ActionMemo, Remark
                                                , MachineCode
                                               , CreId, CreIP, CreDt)
                            VALUES ('{WrGlobal.CorpID}'
                                  , {ExamNo.ConvertStr_Empty_To_Null()}
                                  , {ExamNoSeq.ConvertInt_Empty_To_Null()}
                                  , {DispSeq.ConvertInt_Empty_To_Null()}
                                  , {ExamDate.ConvertStr_Empty_To_Null()}

                                  , {ItemCode.ConvertStr_Empty_To_Null()}
                                  , {ExamDivCd.ConvertStr_Empty_To_Null()}
                                  , {ExamCd.ConvertStr_Empty_To_Null()}
                                  , {RefNo.ConvertStr_Empty_To_Null()}
                                  , {RefNoSeq.ConvertInt_Empty_To_Null()}

                                  , {ExamQty.ConvertFloat_Empty_To_Null()}
                                  , {MeasureDivCd.ConvertStr_Empty_To_Null()}
                                  , {MeasureTime.ConvertStr_Empty_To_Null()}

                                  , {X1.ConvertStr_Empty_To_Null()}
                                  , {X2.ConvertStr_Empty_To_Null()}
                                  , {X3.ConvertStr_Empty_To_Null()}
                                  , {X4.ConvertStr_Empty_To_Null()}
                                  , {X5.ConvertStr_Empty_To_Null()}
                                  , {X6.ConvertStr_Empty_To_Null()}

                                  , {PassDivCd.ConvertStr_Empty_To_Null()}
                                  , {ActionMemo.ConvertStr_Empty_To_Null()}
                                  , {Remark.ConvertStr_Empty_To_Null()}

                                  , {MachineCode.ConvertStr_Empty_To_Null()}

                                  ,'{WrGlobal.LoginID}'
                                  ,'{WrGlobal.ClientHostName}'
                                  ,GETDATE() )";

            return db.ExecuteQuery(query);
        }

        public bool UpdExamResultDaily()
        {
            // ExamDivCd
            query = $@"UPDATE QM_ExamResultDaily
                          SET X1 = {X1.ConvertStr_Empty_To_Null()}
                            , X2 = {X2.ConvertStr_Empty_To_Null()}
                            , X3 = {X3.ConvertStr_Empty_To_Null()}
                            , X4 = {X4.ConvertStr_Empty_To_Null()}
                            , X5 = {X5.ConvertStr_Empty_To_Null()}
                            , X6 = {X6.ConvertStr_Empty_To_Null()}

                            , ExamDate = {ExamDate}
                            , MeasureTime = {MeasureTime.ConvertStr_Empty_To_Null()}

                            , PassDivCd = {PassDivCd.ConvertStr_Empty_To_Null()}

                            , MachineCode = {MachineCode.ConvertStr_Empty_To_Null()}
                            , ExamEmpId = {ExamEmpId.ConvertStr_Empty_To_Null()}

                            , ActionMemo = {ActionMemo.ConvertStr_Empty_To_Null()}
                            , Remark = {Remark.ConvertStr_Empty_To_Null()}

                            , ModId= '{WrGlobal.LoginID}'
                            , ModIP= '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()

                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND ExamNo = '{ExamNo}'
                          AND ExamNoSeq = '{ExamNoSeq}'";

            return db.ExecuteQuery(query);
        }

        public bool DelExamResultDaily()
        {
            query = $@"DELETE QM_ExamResultDaily
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND ExamDivCd = '{ExamDivCd}'
                          AND ExamNo = '{ExamNo}'
                    ";

            if (ExamNoSeq != 0)
                query += $"\n AND ExamNoSeq = {ExamNoSeq}";

            return db.ExecuteQuery(query);
        }

        public bool DelExamResultByRefNoExamDiv()
        {
            query = $@"DELETE QM_ExamResultDaily
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND RefNo = '{RefNo}'
                          AND RefNoSeq = '{RefNoSeq}'
                          AND ExamDivCd = '{ExamDivCd}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
