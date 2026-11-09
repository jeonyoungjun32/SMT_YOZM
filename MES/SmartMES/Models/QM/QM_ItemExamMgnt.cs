using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;

namespace SmartMES.Models.QM
{
	class QM_ItemExamMgnt
    {
        #region Property

        public string ItemCode { get; set; }             //품목
        public string ExamDivCd { get; set; }         //검사구분
        public string ExamCd { get; set; }             //검사코드
        public int DispSeq { get; set; }               //표시순번
        public string ExamSpec { get; set; }           //검사규격
        public string ExamMethod { get; set; }         //검사방법
        public string ExamUnit { get; set; }           //검사단위
        public string ExamCycle { get; set; }          //검사주기
        public string ValueTypeDivCd { get; set; }    //값 형태
        public int DecimalCnt { get; set; }            //소수점 자리수
        public string DefaultValue { get; set; }       //기본값
        public float MgntMin { get; set; }             //관리범위(하한값)
        public float MgntMax { get; set; }             //관리범위(상한값)
        public float DangerMin { get; set; }           //위험범위(하한값)
        public float DangerMax { get; set; }           //위험범위(상한값)
        public string UseYn { get; set; }              //사용유무
        public string MachineCode { get; set; }              //설비코드

        //
        public string ProcessCode { get; set; }

        #endregion Property End

        Database db = new Database();
        string query;

        public DataTable Select_ItemExam_ByItemCd(bool UseYn = true)
        {
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();
            ExamDivCd = ExamDivCd.ConvertStr_Empty_To_Null();
            ExamCd = ExamCd.ConvertStr_Empty_To_Null();

            query = $@"SELECT ItemCode,
                              ExamDivCd,
                              ExamCd,
                              ExamSpec,
                              ExamMethod,

                              ExamUnit,
                              ExamCycle

                              DecimalCnt,
                              DefaultValue,
                              MgntMax,
                              MgntMin,
                              DangerMin,
                              DangerMax

                         FROM QM_ItemExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND ItemCode = {ItemCode}
                          AND UseYn = '{(UseYn ? 'Y' : 'N')}'";

            if (ExamDivCd != "NULL") { query += $"\nAND ExamDivCd = {ExamDivCd}"; }
            if (ExamCd     != "NULL") { query += $"\nAND ExamCd = {ExamCd}"; }

            query += "\nORDER BY DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_ItemExam(string ItemCode, string ExamDivCd, string UseYn = "Y")
        {
            //품목별 검사항목(Frm_QM_ItemExamMgnt)에서 사용하는 Query
            query = $@"SELECT 
                              '' AS Status,
                              iem.DispSeq,
                              iem.ItemCode,
                              iem.ExamDivCd,
                              iem.ExamCd,
                              iem.ExamCd AS ExamNm,

                              --em.ExamNm,
                              iem.ExamSpec,
                              iem.ExamMethod,
                              iem.ExamUnit,
                              iem.ExamCycle,
                              iem.ValueTypeDivCd,

                              iem.DecimalCnt,

                              iem.DefaultValue,
                              iem.MgntMax,
                              iem.MgntMin,
                              iem.DangerMin,
                              iem.DangerMax,

                              iem.UseYn,
                              em.ProcessCode

                         FROM QM_ItemExamMgnt iem
                    LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd
                        WHERE iem.CorpID = '{WrGlobal.CorpID}'
                          AND iem.ItemCode = '{ItemCode}'
                          AND iem.ExamDivCd =  CASE WHEN '{ExamDivCd}' ='ALL' THEN iem.ExamDivCd
                                                  ELSE '{ExamDivCd}' END";

            if (UseYn != "")
            {
                query += $"\nAND iem.UseYn = '{UseYn}'";
            }

            query += $"\nORDER BY iem.DispSeq";

            return db.GetDataTable(query);
        }


        public DataTable Select_ItemExam_Machine(string machineCode, string ExamDivCd)
        {
            //설비별 검사항목(Frm_QM_ItemExamMgnt)에서 사용하는 Query
            query = $@"SELECT 
                              '' AS Status,
                              iem.DispSeq,
                              iem.ItemCode,
                              iem.ExamDivCd,
                              iem.ExamCd,
                              iem.ExamCd AS ExamNm,

                              --em.ExamNm,
                              iem.ExamSpec,
                              iem.ExamMethod,
                              iem.ExamUnit,
                              iem.ExamCycle,
                              iem.ValueTypeDivCd,

                              iem.DecimalCnt,

                              iem.DefaultValue,
                              iem.MgntMax,
                              iem.MgntMin,
                              iem.DangerMin,
                              iem.DangerMax,

                              iem.UseYn,
                              em.ProcessCode

                         FROM QM_ItemExamMgnt iem
                    LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd
                        WHERE iem.CorpID = '{WrGlobal.CorpID}'
                          AND iem.MachineCode = '{machineCode}'
                          AND iem.ExamDivCd =  CASE WHEN '{ExamDivCd}' ='ALL' THEN iem.ExamDivCd
                                                  ELSE '{ExamDivCd}' END
                          AND iem.UseYn = 'Y' 
                     ORDER BY iem.DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_ItemExam_List()
        {
            query = $@"SELECT '' ExamNo
                            , '' ExamSeq
                            , iem.ItemCode
                            , iem.ExamCd
                            , iem.DispSeq
                        	, em.ExamNm
                        	, iem.ExamSpec
                        	, iem.ExamMethod
                        	, iem.ExamUnit
                        	, iem.ValueTypeDivCd
                        	, iem.DefaultValue

                        	, '' AS X1
                        	, '' AS X2
                        	, '' AS X3
                        	, '' AS X4
                        	, '' AS X5
                        	, '' AS X6

                        	, '10' AS PassDivCd
                        	, '' AS ActionMemo
                        	, '' AS Remark

                        	--개발용
                        	, '' AS MeasureDivCd
                        	, '' AS MeasureTime
                        	, '' AS InFilePath
                        	, GETDATE() AS ExamDate
                        	, GETDATE() AS PassDate
                        	, '' AS ExamEmpId
                        	, '' AS LotNo

                        	, iem.MgntMin
                        	, iem.MgntMax
                        	, iem.DangerMin
                        	, iem.DangerMax
                        	, em.ExamCnt
                        	, em.DecimalCnt
                            , cci.ClassItemCode AS ItemCode

                         FROM QM_ItemExamMgnt iem
                    LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd AND em.ExamDivCd = iem.ExamDivCd
                    LEFT JOIN CM_CateItem cci ON cci.class  = 'Q0401020' AND iem.ExamCd = cci.Title

                        WHERE iem.CorpID = '{WrGlobal.CorpID}'
                          AND iem.UseYn = 'Y'
                          AND iem.ItemCode = '{ItemCode}'
                          AND iem.ExamDivCd = '{ExamDivCd}'";

            if (ValueTypeDivCd.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND iem.ValueTypeDivCd = '{ValueTypeDivCd}' ";
            }

            //단조, 전조, 프레스, 도장, 도금, 포장 등등 사내공정 진행 후 검사 일 경우 검사유형에 등록한 공정코드 보고 확인
            if (ProcessCode.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND ( em.ProcessCode = '{ProcessCode}' OR em.ProcessCode is null ) ";
            }

            query += "ORDER BY iem.DispSeq";

            return db.GetDataTable(query);

            //품질검사(Frm_QM_ExamResult)에서 사용하는 Query
            //ValueType(CmCd : 017) - 10 : 합/불, 20 : 값
            //ValueTypeDivCd = ValueTypeDivCd.ConvertStr_Empty_To_Null();

            /* query = $@"SELECT '' ExamNo
                             , '' ExamSeq
                             , iem.ItemCode
                             , iem.ExamCd
                             , iem.DispSeq
                             , em.ExamNm
                             , iem.ExamSpec
                             , iem.ExamMethod
                             , iem.ExamUnit
                             , iem.DefaultValue

                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X1
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X2
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X3
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X4
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X5
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X6

                             , '10' AS PassDivCd
                             , '' AS ActionMemo
                             , '' AS Remark

                             --개발용
                             , '' AS MeasureDivCd
                             , '' AS MeasureTime
                             , '' AS InFilePath
                             , GETDATE() AS ExamDate

                             , iem.MgntMin
                             , iem.MgntMax
                             , iem.DangerMin
                             , iem.DangerMax
                             , em.DecimalCnt

                          FROM QM_ItemExamMgnt iem
                     LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd

                         WHERE iem.CorpID = '{WrGlobal.CorpID}'
                           AND iem.UseYn = 'Y'
                           AND iem.ItemCode = '{ItemCode}'
                           AND iem.ExamDivCd = '{ExamDivCd}'";

             if (ValueTypeDivCd.ConvertString_Null_To_Empty() != "")
             {
                 query += $"\nAND iem.ValueTypeDivCd = '{ValueTypeDivCd}' ";
             }

             query += "ORDER BY iem.DispSeq";

             return db.GetDataTable(query);*/
        }


        public DataTable Select_ItemExam_List_Machine()
        {
            query = $@"SELECT '' ExamNo
                            , '' ExamSeq
                            , iem.ItemCode
                            , iem.ExamCd
                            , iem.DispSeq
                        	, em.ExamNm
                        	, iem.ExamSpec
                        	, iem.ExamMethod
                        	, iem.ExamUnit
                        	, iem.ValueTypeDivCd
                        	, iem.DefaultValue

                        	, '' AS X1
                        	, '' AS X2
                        	, '' AS X3
                        	, '' AS X4
                        	, '' AS X5
                        	, '' AS X6

                        	, '10' AS PassDivCd
                        	, '' AS ActionMemo
                        	, '' AS Remark

                        	--개발용
                        	, '' AS MeasureDivCd
                        	, '' AS MeasureTime
                        	, '' AS InFilePath
                        	, GETDATE() AS ExamDate
                        	, '' AS ExamEmpId

                        	, iem.MgntMin
                        	, iem.MgntMax
                        	, iem.DangerMin
                        	, iem.DangerMax
                        	, em.ExamCnt
                        	, em.DecimalCnt
                            , cci.ClassItemCode AS ItemCode

                         FROM QM_ItemExamMgnt iem
                    LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd AND em.ExamDivCd = iem.ExamDivCd
                    LEFT JOIN CM_CateItem cci ON cci.class  = 'Q0401020' AND iem.ExamCd = cci.Title

                        WHERE iem.CorpID = '{WrGlobal.CorpID}'
                          AND iem.UseYn = 'Y'
                          AND (iem.MachineCode = '{MachineCode}' OR iem.ItemCode = '{ItemCode}')
                          AND iem.ExamDivCd = '{ExamDivCd}'";

            if (ValueTypeDivCd.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND iem.ValueTypeDivCd = '{ValueTypeDivCd}' ";
            }

            query += "ORDER BY iem.DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_ItemExam_List_RMC(string classCode)
        {
            query = $@"SELECT '' ExamNo
                            , '' ExamSeq
                            , iem.ItemCode
                            , iem.ExamCd
                            , iem.DispSeq
                        	, em.ExamNm
                        	, iem.ExamSpec
                        	, iem.ExamMethod
                        	, iem.ExamUnit
                        	, iem.ValueTypeDivCd
                        	, iem.DefaultValue

                        	, '' AS X1
                        	, '' AS X2
                        	, '' AS X3
                        	, '' AS X4
                        	, '' AS X5
                        	, '' AS X6

                        	, '10' AS PassDivCd
                        	, '' AS ActionMemo
                        	, '' AS Remark

                        	--개발용
                        	, '' AS MeasureDivCd
                        	, '' AS MeasureTime
                        	, '' AS InFilePath
                        	, GETDATE() AS ExamDate

                        	, iem.MgntMin
                        	, iem.MgntMax
                        	, iem.DangerMin
                        	, iem.DangerMax
                        	, em.ExamCnt
                        	, em.DecimalCnt
                            , cci.ClassItemCode AS ItemCode

                         FROM QM_ItemExamMgnt iem
                    LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd
                    LEFT JOIN CM_CateItem cci ON cci.class  = '{classCode}' AND iem.ExamCd = cci.Title

                        WHERE iem.CorpID = '{WrGlobal.CorpID}'
                          AND iem.UseYn = 'Y'
                          AND iem.ItemCode = '{ItemCode}'
                          AND iem.ExamDivCd = '{ExamDivCd}'";

            if (ValueTypeDivCd.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND iem.ValueTypeDivCd = '{ValueTypeDivCd}' ";
            }

            if (ExamMethod.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND iem.ExamMethod = '{ExamMethod}' ";
            }

            //단조, 전조, 프레스, 도장, 도금, 포장 등등 사내공정 진행 후 검사 일 경우 검사유형에 등록한 공정코드 보고 확인
            if (ProcessCode.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND ( em.ProcessCode = '{ProcessCode}' OR em.ProcessCode is null ) ";
            }

            query += "ORDER BY iem.DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_ItemExam_List_RND()
        {
            query = $@"SELECT '' ExamNo
                            , '' ExamSeq
                            , iem.ItemCode
                            , em.ExamCd
                            , iem.DispSeq
                        	, em.ExamNm
                        	, em.ExamSpec
                        	, em.ExamMethod
                        	, em.ExamUnit
                        	, em.ValueTypeDivCd
                        	, iem.DefaultValue

                        	, '' AS X1
                        	, '' AS X2
                        	, '' AS X3
                        	, '' AS X4
                        	, '' AS X5
                        	, '' AS X6

                        	, '10' AS PassDivCd
                        	, '' AS ActionMemo
                        	, '' AS Remark

                        	--개발용
                        	, '' AS MeasureDivCd
                        	, '' AS MeasureTime
                        	, '' AS InFilePath
                        	, GETDATE() AS ExamDate

                        	, iem.MgntMin
                        	, iem.MgntMax
                        	, iem.DangerMin
                        	, iem.DangerMax
                        	, em.DecimalCnt

                         FROM QM_ExamMgnt em
                    LEFT JOIN QM_ItemExamMgnt iem ON em.ExamCd = iem.ExamCd

                        WHERE em.CorpID = '{WrGlobal.CorpID}'
                          AND em.UseYn = 'Y'
                          AND em.ExamDivCd = '{ExamDivCd}'";

            if (ValueTypeDivCd.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND em.ValueTypeDivCd = '{ValueTypeDivCd}' ";
            }

            //단조, 전조, 프레스, 도장, 도금, 포장 등등 사내공정 진행 후 검사 일 경우 검사유형에 등록한 공정코드 보고 확인
            if (ProcessCode.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND ( em.ProcessCode = '{ProcessCode}' OR em.ProcessCode is null ) ";
            }

            query += "ORDER BY iem.DispSeq";

            return db.GetDataTable(query);

            //품질검사(Frm_QM_ExamResult)에서 사용하는 Query
            //ValueType(CmCd : 017) - 10 : 합/불, 20 : 값
            //ValueTypeDivCd = ValueTypeDivCd.ConvertStr_Empty_To_Null();

            /* query = $@"SELECT '' ExamNo
                             , '' ExamSeq
                             , iem.ItemCode
                             , iem.ExamCd
                             , iem.DispSeq
                             , em.ExamNm
                             , iem.ExamSpec
                             , iem.ExamMethod
                             , iem.ExamUnit
                             , iem.DefaultValue

                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X1
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X2
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X3
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X4
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X5
                             , IIF('{default_Set_Yn}' = 'Y', iem.DefaultValue, '') AS X6

                             , '10' AS PassDivCd
                             , '' AS ActionMemo
                             , '' AS Remark

                             --개발용
                             , '' AS MeasureDivCd
                             , '' AS MeasureTime
                             , '' AS InFilePath
                             , GETDATE() AS ExamDate

                             , iem.MgntMin
                             , iem.MgntMax
                             , iem.DangerMin
                             , iem.DangerMax
                             , em.DecimalCnt

                          FROM QM_ItemExamMgnt iem
                     LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd

                         WHERE iem.CorpID = '{WrGlobal.CorpID}'
                           AND iem.UseYn = 'Y'
                           AND iem.ItemCode = '{ItemCode}'
                           AND iem.ExamDivCd = '{ExamDivCd}'";

             if (ValueTypeDivCd.ConvertString_Null_To_Empty() != "")
             {
                 query += $"\nAND iem.ValueTypeDivCd = '{ValueTypeDivCd}' ";
             }

             query += "ORDER BY iem.DispSeq";

             return db.GetDataTable(query);*/
        }

        public DataTable Select_ItemExam_ValueType()
        {
            query = $@"SELECT 
                              '' AS Status
                            , '' ExamNo
                            , '' ExamSeq

                            , iem.ItemCode
                        	, it.ItemCode
                            , it.ItemName
                            , it.Alias
                            , it.Size
                            , it.Standard

                            , iem.ExamDivCd
                            , iem.ExamCd
                            , iem.ExamCd AS ExamNm
                            , iem.DispSeq
                        	, em.ExamNm
                        	, iem.ExamSpec
                        	, iem.ExamMethod
                        	, iem.ExamUnit
                        	, iem.ExamCycle
                        	, iem.ValueTypeDivCd
                        	, iem.DecimalCnt
                        	, iem.DefaultValue

                        	, '' AS X1
                        	, '' AS X2
                        	, '' AS X3
                        	, '' AS X4
                        	, '' AS X5
                        	, '' AS X6

                        	, '10' AS PassDivCd
                        	, '' AS ActionMemo
                        	, '' AS Remark
                        	, 0.0 AS Tolerance
                            , 0.0 AS Tolerance_Min
                            , 0.0 AS Tolerance_Max

                        	--개발용
                        	, '' AS MeasureDivCd
                        	, '' AS MeasureTime
                        	, '' AS InFilePath
                        	, GETDATE() AS ExamDate

                        	, iem.MgntMin
                        	, iem.MgntMax
                        	, iem.DangerMin
                        	, iem.DangerMax
                        	, iem.UseYn

                         FROM QM_ItemExamMgnt iem
                    INNER JOIN CM_Item it ON iem.CorpID = it.CorpID AND iem.ItemCode = it.ItemCode
                    LEFT JOIN QM_ExamMgnt em ON iem.CorpID = em.CorpID AND em.ExamCd = iem.ExamCd

                        WHERE iem.CorpID = '{WrGlobal.CorpID}'
                          AND iem.UseYn = 'Y'
                          AND iem.ExamDivCd = '{ExamDivCd}'
            ";

            if (!Convert.ToString(ItemCode).IsNullOrEmpty())
            {
                query += $"\n AND iem.ItemCode = '{ItemCode}'";
            }

            if (!Convert.ToString(ValueTypeDivCd).IsNullOrEmpty())
            {
                query += $"\nAND iem.ValueTypeDivCd = '{ValueTypeDivCd}' ";
            }

            if (!Convert.ToString(ExamCd).IsNullOrEmpty())
            {
                query += $"\n AND iem.ExamCd = '{ExamCd}'";
            }

            query += "ORDER BY iem.ItemCode, iem.DispSeq";

            return db.GetDataTable(query);
        }

        public bool Ins_ItemExam()
         {
            db.Edit_Property(this);

            query = $@"INSERT INTO QM_ItemExamMgnt(CorpID, ItemCode, ExamDivCd, ExamCd, DispSeq
                                                  ,	ExamSpec, ExamMethod, ExamUnit, ExamCycle, ValueTypeDivCd
                                                  , DecimalCnt, MachineCode
                                                  , DefaultValue, MgntMin, MgntMax, DangerMin, DangerMax
                                                  , UseYn, CreId, CreIP, CreDt)
                                        VALUES ('{WrGlobal.CorpID}'
                                               ,'{ItemCode}'
                                               ,'{ExamDivCd}'
                                               ,'{ExamCd}'
                                               , {DispSeq}

                                               ,'{ExamSpec}'
                                               ,'{ExamMethod}'
                                               ,'{ExamUnit}'
                                               ,'{ExamCycle}'
                                               ,'{ValueTypeDivCd}'

                                               , {DecimalCnt}
                                               , {MachineCode.ConvertStr_Empty_To_Null()}

                                               ,'{DefaultValue}'
                                               , CASE WHEN ('{MgntMin}'   = '' OR {MgntMin}   = 0) THEN NULL ELSE {MgntMin}   END
                                               , CASE WHEN ('{MgntMax}'   = '' OR {MgntMax}   = 0) THEN NULL ELSE {MgntMax}   END
                                               , CASE WHEN ('{DangerMin}' = '' OR {DangerMin} = 0) THEN NULL ELSE {DangerMin} END
                                               , CASE WHEN ('{DangerMax}' = '' OR {DangerMax} = 0) THEN NULL ELSE {DangerMax} END
                                               ,'{UseYn}'

                                               ,'{WrGlobal.LoginID}'
                                               ,'{WrGlobal.ClientHostName}'
                                               ,GETDATE() )";

            return db.ExecuteQuery(query);
        }

        public bool Ins_ItemExam_ByMachineCode(string sourceMachineCode)
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO QM_ItemExamMgnt(CorpID, ItemCode, ExamDivCd, ExamCd, DispSeq
                                                  ,	ExamSpec, ExamMethod, ExamUnit, ExamCycle, ValueTypeDivCd
                                                  , DecimalCnt, MachineCode
                                                  , DefaultValue, MgntMin, MgntMax, DangerMin, DangerMax
                                                  , UseYn, CreId, CreDt)

            SELECT

                CorpID, '{MachineCode}', ExamDivCd, ExamCd, DispSeq
                , ExamSpec, ExamMethod, ExamUnit, ExamCycle, ValueTypeDivCd
                , DecimalCnt, '{MachineCode}'
                , DefaultValue, MgntMin, MgntMax, DangerMin, DangerMax
                , UseYn, '{WrGlobal.LoginID}', GETDATE()

            FROM QM_ItemExamMgnt
            WHERE CorpID = '{WrGlobal.CorpID}'
            AND ExamDivCd = '{ExamDivCd}'
            AND MachineCode = '{sourceMachineCode}'
            ";


            return db.ExecuteQuery(query);
        }

        public bool Insert_ItemExam_Batch(string Item_Div_Cd, string ExamDivCd)
        {
            query = $@"EXEC ItemExam_Batch '{WrGlobal.CorpID}', '{Item_Div_Cd}', '{ExamDivCd}', '{WrGlobal.LoginID}'";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ItemExam()
        {
            // ExamDivCd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ItemExamMgnt
                          SET 
                               ItemCode	        = '{ItemCode}'
                             , ExamDivCd	    = '{ExamDivCd}'
                             , ExamCd	        = '{ExamCd}'
                             , DispSeq	        =  {DispSeq} 
                             , ExamSpec	    = '{ExamSpec}'
                             , ExamMethod	    = '{ExamMethod}'
                             , ExamUnit	    = '{ExamUnit}'
                             , ExamCycle	    = '{ExamCycle}'
                             , ValueTypeDivCd	= '{ValueTypeDivCd}'
                             , DecimalCnt	    =  {DecimalCnt} 
                             , DefaultValue	= '{DefaultValue}'
                             , MgntMin	        =  CASE WHEN ('{MgntMin}' = '' OR {MgntMin} = 0) THEN NULL ELSE {MgntMin} END
                             , MgntMax	        =  CASE WHEN ('{MgntMax}' = '' OR {MgntMax} = 0) THEN NULL ELSE {MgntMax} END
                             , DangerMin	    =  CASE WHEN ('{DangerMin}' = '' OR {DangerMin} = 0) THEN NULL ELSE {DangerMin} END
                             , DangerMax	    =  CASE WHEN ('{DangerMax}' = '' OR {DangerMax} = 0) THEN NULL ELSE {DangerMax} END
                             , UseYn	        = '{UseYn}'
                             , MachineCode	        = {MachineCode.ConvertStr_Empty_To_Null()}

                             , ModId = '{WrGlobal.LoginID}'
                             , ModIP = '{WrGlobal.ClientHostName}'
                             , ModDt = GETDATE()

                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND ItemCode = '{ItemCode}'
                          AND ExamDivCd = '{ExamDivCd}'
                          AND ExamCd = '{ExamCd}' ";

            return db.ExecuteQuery(query);
        }

        public bool Del_ItemExam()
        {
            query = $@"DELETE QM_ItemExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND ItemCode = '{ItemCode}'
                          AND ExamDivCd = '{ExamDivCd}'
                          AND ExamCd = '{ExamCd}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_ItemExam_ExamCd()
        {
            query = $@"DELETE QM_ItemExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND ExamDivCd = '{ExamDivCd}'
                          AND ExamCd = '{ExamCd}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_ItemExam_MachineCode()
        {
            query = $@"DELETE QM_ItemExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND ExamDivCd = '{ExamDivCd}'
                          AND MachineCode = '{MachineCode}'";

            return db.ExecuteQuery(query);
        }

    }
}
