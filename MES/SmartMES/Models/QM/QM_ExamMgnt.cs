using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.QM
{
	class QM_ExamMgnt
    {
        #region Property

        public string ExamDivCd { get; set; }         //검사구분
        public string ExamCd { get; set; }             //검사코드
        public string ExamNm { get; set; }             //검사명
        public string ExamSpec { get; set; }           //검사규격
        public string ExamMethod { get; set; }         //검사방법
        public string ExamUnit { get; set; }           //검사단위
        public string ExamCycle { get; set; }          //검사주기
        public string ValueTypeDivCd { get; set; }    //값 형태
        public int DecimalCnt { get; set; }         //소수점 자리수
        public string UseYn { get; set; }              //사용여부
        public string ProcessCode { get; set; }              //공정코드
        public string ItemType { get; set; }              //


        #endregion Property End

        Database db = new Database();
        string query;

        //s-mes
        public DataTable Select_Exam_Lup(string ExamDivCd, bool UseYn = true)
        {
            query = $@"SELECT 
                              em.ProcessCode
                            , pr.ProcessName
                            , em.ExamCd
                            , em.ExamNm
                                                     
                         FROM QM_ExamMgnt em
                         LEFT JOIN Cm_Process pr ON pr.CorpID = em.CorpID AND pr.ProcessCode = em.ProcessCode
                        WHERE em.CorpID = '{WrGlobal.CorpID}'
                          AND em.UseYn = {(UseYn ? "'Y'" : "'N'")}";

            if (ExamDivCd.ToUpper() != "ALL")
            {
                query += $"\nAND em.ExamDivCd = {ExamDivCd.ConvertStr_Empty_To_Null()}";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Exam_Mgnt_Lup(string ExamDivCd, bool UseYn = true)
        {
            query = $@"SELECT 
                              em.ExamCd 코드 
                            , em.ExamNm 명칭
                                                     
                         FROM QM_ExamMgnt em
                        WHERE em.CorpID = '{WrGlobal.CorpID}'
                          AND em.UseYn = {(UseYn ? "'Y'" : "'N'")}";

            if (ExamDivCd.ToUpper() != "ALL")
            {
                query += $"\nAND em.ExamDivCd = {ExamDivCd.ConvertStr_Empty_To_Null()}";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_ExamNumeric(string ExamDivCd)
        {
            query = $@"SELECT ExamCd
                            , ExamNm
                                                     
                         FROM QM_ExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND UseYn = 'Y'
                          AND ValueTypeDivCd = '20'";

            if (ExamDivCd.ToUpper() != "ALL")
            {
                query += $"\nAND ExamDivCd = {ExamDivCd.ConvertStr_Empty_To_Null()}";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Exam(string ExamDivCd, bool UseYn = true)
        {
            query = $@"SELECT '' AS Status
                            , ExamDivCd
                            , ExamCd
                            , ExamNm
                            , ExamSpec
                            , ExamMethod
                            , ExamUnit
                            , ExamCycle
                            , ValueTypeDivCd
                            , DecimalCnt
                            , UseYn
                            , ProcessCode
                            , ExamCnt
                            , ItemType

                         FROM QM_ExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'";

            if(ExamDivCd.ToUpper() != "ALL")
            {
                query += $"\nAND ExamDivCd = {ExamDivCd.ConvertStr_Empty_To_Null()}";
            }

            return db.GetDataTable(query);
        }

        public int Select_Max_ExamCnt(string ExamDivCd)
        {
            query = $@"SELECT MAX(ExamCnt) AS CNT
                         FROM QM_ExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'";

            if (ExamDivCd.ToUpper() != "ALL")
            {
                query += $"\nAND ExamDivCd = {ExamDivCd.ConvertStr_Empty_To_Null()}";
            }

            DataRow dr = db.GetDataRow(query);

            int cnt = 0;

            if(dr != null)
			{
                cnt = dr["CNT"].ConvertInt_Null_To_Zero();
			}

            return cnt;
        }

        public DataTable Select_ItemExam_List_Daily()
        {
            query = $@"SELECT '' ExamNo
                            , '' ExamSeq
                            , '' AS ItemCode
                            , em.ExamCd
                            , ROW_NUMBER() OVER(ORDER BY ExamCd)  DispSeq
                        	, em.ExamNm
                        	, em.ExamSpec
                        	, em.ExamMethod
                        	, em.ExamUnit
                        	, em.ValueTypeDivCd
                        	, '' AS DefaultValue

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
                        	, '' AS LotNo

                        	, em.ExamCnt
                        	, em.DecimalCnt

                         FROM QM_ExamMgnt em

                        WHERE em.CorpID = '{WrGlobal.CorpID}'
                          AND em.UseYn = 'Y'
                          AND em.ExamDivCd = '{ExamDivCd}'";

            if (ValueTypeDivCd.ConvertString_Null_To_Empty() != "")
            {
                query += $"\nAND em.ValueTypeDivCd = '{ValueTypeDivCd}' ";
            }

            query += "ORDER BY ExamCd";

            return db.GetDataTable(query);
        }

        public DataRow Select_Exam_Info()
        {
            if (ExamCd == "" && ExamNm == "") return null;

            query = $@"SELECT TOP 1
                              ExamCd
                            , ExamNm
                            , ExamSpec
                            , ExamMethod
                            , ExamUnit
                            , ExamCycle
                            , ValueTypeDivCd
                            , DecimalCnt
                            , ProcessCode
                         FROM QM_ExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'";

            if (ExamCd != "" && ExamCd != null) { query += $" AND ExamCd = '{ExamCd}'"; }
            if (ExamNm != "" && ExamNm != null) { query += $" AND ExamNm = '{ExamNm}'"; }

            return db.GetDataRow(query);
        }

        public DataTable Add_Exam()
        {
            query = $@"SELECT '신규' AS Status
                            , Null AS DispSeq
                            , '' AS ItemCode

                            , ExamDivCd
                            , ExamCd
                            , ExamCd AS ExamNm
                            , ExamSpec
                            , ExamMethod
                            , ExamUnit
                            , ExamCycle
                            , ValueTypeDivCd
                            , DecimalCnt
                            , '' AS DefaultValue

                            , Null AS MgntMin
                            , Null AS MgntMax
                            , Null AS DangerMin
                            , Null AS DangerMin
                            , UseYn
                            , ProcessCode

                         FROM QM_ExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND ExamDivCd =  '{ExamDivCd}'
                          AND UseYn = 'Y' ";

            return db.GetDataTable(query);
        }


        public DataTable Select_ExamNumeric(string CorpID, string ExamDivCd)
        {
            query = $@"SELECT ExamCd
                            , ExamNm
                                                     
                         FROM QM_ExamMgnt
                        WHERE CorpID = '{CorpID}'
                          AND UseYn = 'Y'
                          AND ValueTypeDivCd = '20'";

            if (ExamDivCd.ToUpper() != "ALL")
            {
                query += $"\nAND ExamDivCd = {ExamDivCd.ConvertStr_Empty_To_Null()}";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Exam(string CorpID, string ExamDivCd, bool UseYn = true)
        {
            query = $@"SELECT '' AS Status
                            , ProcessCode
                            , ExamDivCd
                            , ExamCd
                            , ExamNm
                            , ExamSpec
                            , ExamMethod
                            , ExamUnit
                            , ExamCycle
                            , ValueTypeDivCd
                            , DecimalCnt
                            , UseYn

                         FROM QM_ExamMgnt
                        WHERE CorpID = '{CorpID}'";

            if (ExamDivCd.ToUpper() != "ALL")
            {
                query += $"\nAND ExamDivCd = {ExamDivCd.ConvertStr_Empty_To_Null()}";
            }

            return db.GetDataTable(query);
        }

        public bool Ins_Exam()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO QM_ExamMgnt(CorpID, ExamDivCd, ExamCd, ExamNm, ExamSpec, ExamMethod, ExamUnit,
                                                ExamCycle, ValueTypeDivCd, DecimalCnt, UseYn, ProcessCode, ItemType, 
                                                CreId, CreIP, CreDt
)
                                        VALUES ('{WrGlobal.CorpID}'
                                                , '{ExamDivCd}'
                                                , CASE WHEN '{WrGlobal.ProcOption["BS"]["Quality_AutoNo"]}' = 'Y' THEN dbo.FN_Create_Cd_Exam('{WrGlobal.CorpID}','{ExamDivCd}')
                                                       ELSE '{ExamCd}'
                                                  END
                                                , '{ExamNm}'
                                                , '{ExamSpec}'
                                                , '{ExamMethod}'
                                                , '{ExamUnit}'
                                                , '{ExamCycle}'
                                                , '{ValueTypeDivCd}'
                                                ,  {DecimalCnt}
                                                , '{UseYn}'
                                                , {ProcessCode.ConvertStr_Empty_To_Null()}
                                                , {ItemType.ConvertStr_Empty_To_Null()}

                                                ,'{WrGlobal.LoginID}'
                                                , '{WrGlobal.ClientHostName}'
                                                ,GETDATE() )";

            return db.ExecuteQuery(query);
        }


        public bool Ins_Exam(string CorpID)
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO QM_ExamMgnt(CorpID, ExamDivCd, ExamCd, ExamNm, ExamSpec, ExamMethod, ExamUnit,
                                                ExamCycle, ValueTypeDivCd, DecimalCnt, UseYn, ProcessCode,
                                                CreId, CreDt
)
                                        VALUES ('{CorpID}'
                                                , '{ExamDivCd}'
                                                , CASE WHEN '{WrGlobal.ProcOption["BS"]["Quality_AutoNo"]}' = 'Y' THEN dbo.FN_Create_Cd_Exam('{CorpID}','{ExamDivCd}')
                                                       ELSE '{ExamCd}'
                                                  END
                                                , '{ExamNm}'
                                                , '{ExamSpec}'
                                                , '{ExamMethod}'
                                                , '{ExamUnit}'
                                                , '{ExamCycle}'
                                                , '{ValueTypeDivCd}'
                                                ,  {DecimalCnt}
                                                , '{UseYn}'
                                                ,  {ProcessCode.ConvertStr_Empty_To_Null()}

                                                ,'{WrGlobal.LoginID}'
                                                ,GETDATE() )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Exam()
        {
            // ExamDivCd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamMgnt
                          SET 
                               ExamNm =            '{ExamNm}'
                             , ExamSpec =          '{ExamSpec}'
                             , ExamMethod =        '{ExamMethod}'
                             , ExamUnit =          '{ExamUnit}'
                             , ExamCycle =         '{ExamCycle}'
                             , ValueTypeDivCd =   '{ValueTypeDivCd}'
                             , DecimalCnt =         {DecimalCnt}
                             , UseYn =             '{UseYn}'
                             , ProcessCode =          {ProcessCode.ConvertStr_Empty_To_Null()}
                             , ItemType =        {ItemType.ConvertStr_Empty_To_Null()}

                             , ModId = '{WrGlobal.LoginID}'
                             , ModIP = '{WrGlobal.ClientHostName}'
                             , ModDt = GETDATE()

                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND ExamCd = '{ExamCd}'";

            return db.ExecuteQuery(query);
        }


        public bool Upd_Exam(string CorpID)
        {
            // ExamDivCd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamMgnt
                          SET 
                               ExamNm =            '{ExamNm}'
                             , ExamSpec =          '{ExamSpec}'
                             , ExamMethod =        '{ExamMethod}'
                             , ExamUnit =          '{ExamUnit}'
                             , ExamCycle =         '{ExamCycle}'
                             , ValueTypeDivCd =   '{ValueTypeDivCd}'
                             , DecimalCnt =         {DecimalCnt}
                             , UseYn =             '{UseYn}'
                             , ProcessCode =          {ProcessCode.ConvertStr_Empty_To_Null()}

                             , ModId = '{WrGlobal.LoginID}'
                             , ModDt = GETDATE()

                        WHERE CorpID = '{CorpID}'
                          AND ExamCd = '{ExamCd}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_Exam()
        {
            query = $@"DELETE QM_ExamMgnt
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND ExamCd = '{ExamCd}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_Exam(string CorpID)
        {
            query = $@"DELETE QM_ExamMgnt
                        WHERE CorpID = '{CorpID}'
                          AND ExamCd = '{ExamCd}'";

            return db.ExecuteQuery(query);
        }
    }
}
