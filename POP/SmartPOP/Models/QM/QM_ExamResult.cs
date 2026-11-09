using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMES.Common;
using SmartPOP.Common;

namespace SmartMES.Models.QM
{
    public class QM_ExamResult
    {
        #region Property

        public string Exam_No { get; set; }//검사번호
        public int Exam_No_Seq { get; set; }//검사순번
        public int Disp_Seq { get; set; }//표시순번
        public string Exam_Date { get; set; }//검사일자
        public string Item_Cd { get; set; }//품목
        public string Exam_Div_Cd { get; set; }//검사구분
        public string Exam_Cd { get; set; }//검사코드
        public string Ref_No { get; set; }//참조번호
        public int Ref_No_Seq { get; set; }//참조순번
        public float Exam_Qty { get; set; }//검사수량
        public string Measure_Div_Cd { get; set; }//측정구분
        public string Measure_Time { get; set; }//측정시간
        public string X1 { get; set; }//X1
        public string X2 { get; set; }//X2
        public string X3 { get; set; }//X3
        public string X4 { get; set; }//X4
        public string X5 { get; set; }//X5
        public string X6 { get; set; }//X6
        public string Pass_Div_Cd { get; set; }//판정여부
        public string Action_Memo { get; set; }//조치내역
        public string Rmk { get; set; }//비고
        public string In_FilePath { get; set; }
        public string Cust_Cd { get; set; }//거래처
        public string Line_Cd { get; set; }//거래처
        public string Process_Cd { get; set; }//거래처
        public string Machine_Cd { get; set; }//거래처
        public string File_Ext { get; set; }//파일확장자


        #endregion Property End

        Database db = new Database();
        string query;

        #region Select문

        public DataTable Select_ExamResult(string ValueType = "")
        {
            //품질검사(Frm_QM_ExamResult)에서 사용하는 Query
            //ValueType(CmCd : 017) - 10 : 합/불, 20 : 값
            query = $@"SELECT qer.ExamNo 
                            , qer.ExamSeq
                            , qer.ItemCode
                            , qer.ExamCd
                            , em.ExamNm
                            , iem.ExamSpec
                            , iem.ExamMethod
                            , iem.ExamUnit
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
                            , CONVERT(DATE, qer.ExamDate) AS Exam_Date

                            , qer.CreId
                            , qer.CreIP
                            , qer.CreDt
                            , qer.ModId
                            , qer.ModIP
                            , qer.ModDt
                            
                            , iem.MgntMin
                            , iem.MgntMax
                            , iem.DangerMin
                            , iem.DangerMax

                         FROM QM_ExamResult qer
                        LEFT JOIN QM_ItemExamMgnt iem ON iem.ExamCd = qer.ExamCd AND iem.ItemCode = qer.ItemCode AND iem.CorpId = qer.CorpId
                        LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd AND em.CorpId = qer.CorpId

                        WHERE qer.CorpId = '{WrGlobal.CorpID}'
                          AND qer.RefNo = '{Ref_No}'
                          AND qer.RefSeq = '{Ref_No_Seq}'";

            if (ValueType != "")
            {
                //검사값 구분(합/불, 숫자형 구분)
                query += $"\nAND iem.ValueTypeDiv_Cd = '{ValueType}'";
            }

            //검사구분
            if (Exam_Div_Cd != null)
            {
                query += $"\nAND qer.ExamDivCd = '{Exam_Div_Cd}'";
            }

            //초중종같은 측정구분(초물, 중물,종물)이 있는 검사일경우 사용
            if (Measure_Div_Cd != null && Measure_Div_Cd != "")
            {
                query += $"\nAND qer.MeasureDivCd = '{Measure_Div_Cd}'";
            }

            query += "\nORDER BY iem.DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_ExamResult_SeialNo(string ValueType = "")
        {
            //품질검사(Frm_QM_ExamResult)에서 사용하는 Query
            //ValueType(CmCd : 017) - 10 : 합/불, 20 : 값
            query = $@"SELECT qer.Exam_No 
                            , qer.Exam_No_Seq
                            , qer.Item_Cd
                            , qer.Exam_Cd
                            , em.Exam_Nm
                            , iem.Exam_Spec
                            , iem.Exam_Method
                            , iem.Exam_Unit
                            , qer.X1
                            , qer.X2
                            , qer.X3
                            , qer.X4
                            , qer.X5
                            , qer.X6
                            , qer.Pass_Div_Cd
                            , qer.Action_Memo
                            , qer.Rmk

                            --개발용
                            , qer.Measure_Div_Cd
                            , qer.Measure_Time
                            , qer.In_FilePath
                            , CONVERT(DATE, qer.Exam_Date) AS Exam_Date

                            , qer.Ins_Emp_No
                            , qer.Ins_Date_Time
                            , qer.Upd_Emp_No
                            , qer.Upd_Date_Time
                            
                            , iem.Mgnt_Min
                            , iem.Mgnt_Max
                            , iem.Danger_Min
                            , iem.Danger_Max

                         FROM QM_ExamResult qer
                    LEFT JOIN BS_ItemExam_Mgnt iem ON iem.Exam_Cd = qer.Exam_Cd
                        	                      AND iem.Item_Cd = qer.Item_Cd AND iem.CorpId = qer.Corp_Cd
                    LEFT JOIN BS_Exam_Mgnt em ON em.Exam_Cd = iem.Exam_Cd       AND em.CorpId = qer.Corp_Cd

                        WHERE qer.Corp_Cd = '{WrGlobal.CorpID}'
                          AND qer.Ref_No = '{Ref_No}'";

            if (ValueType != "")
            {
                //검사값 구분(합/불, 숫자형 구분)
                query += $"\nAND iem.ValueType_Div_Cd = '{ValueType}'";
            }

            //검사구분
            if (Exam_Div_Cd != null)
            {
                query += $"\nAND qer.Exam_Div_Cd = '{Exam_Div_Cd}'";
            }

            //초중종같은 측정구분(초물, 중물,종물)이 있는 검사일경우 사용
            if (Measure_Div_Cd != null && Measure_Div_Cd != "")
            {
                query += $"\nAND qer.Measure_Div_Cd = '{Measure_Div_Cd}'";
            }

            query += "\nORDER BY iem.Disp_Seq";

            return db.GetDataTable(query);
        }

        public DataTable SELECT_RptOQC_Press_Result()
        {
            //품질검사(Frm_QM_ExamResult)에서 사용하는 Query
            //ValueType(CmCd : 017) - 10 : 합/불, 20 : 값
            query = $@"
                        SELECT 
                          qer.Exam_No 
                        , qer.Exam_No_Seq
                        , em.Exam_Nm
                        , iem.Exam_Spec
                        , iem.Exam_Cycle
                            , IIF(iem.ValueType_Div_Cd = '10' , (CASE qer.X1 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X1 ) AS X1
                            , IIF(iem.ValueType_Div_Cd = '10' , (CASE qer.X2 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X2 ) AS X2
                            , IIF(iem.ValueType_Div_Cd = '10' , (CASE qer.X3 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X3 ) AS X3
                            , IIF(iem.ValueType_Div_Cd = '10' , (CASE qer.X4 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X4 ) AS X4
                            , IIF(iem.ValueType_Div_Cd = '10' , (CASE qer.X5 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X5 ) AS X5
                            , IIF(iem.ValueType_Div_Cd = '10' , (CASE qer.X6 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X6 ) AS X6
                        , CASE qer.Pass_Div_Cd WHEN '20' THEN '불합격' WHEN '10' THEN '합격' ELSE '' END AS Pass_Div_Cd

                        , qer.Item_Cd
                        , qer.Exam_Cd
                        , iem.Exam_Method
                        , iem.Exam_Unit
                        , qer.Action_Memo
                        , qer.Rmk

                        --개발용
                        , qer.Measure_Div_Cd
                        , qer.Measure_Time
                        , qer.In_FilePath
          				, Convert(Date, qer.Exam_Date, 112) AS Exam_Date


                        FROM QM_ExamResult qer
                        LEFT JOIN BS_ItemExam_Mgnt iem ON qer.Corp_Cd = iem.Corp_Cd AND iem.Exam_Cd = qer.Exam_Cd AND iem.Item_Cd = qer.Item_Cd
                        LEFT JOIN BS_Exam_Mgnt		em ON qer.Corp_Cd = em.Corp_Cd AND em.Exam_Cd = iem.Exam_Cd

                        WHERE qer.Corp_Cd = '{WrGlobal.CorpID}'
                          AND qer.Ref_No = '{Ref_No}'
                          AND qer.Ref_No_Seq = '{Ref_No_Seq}'
";

            if (Exam_Div_Cd != "" && Exam_Div_Cd != null)
            {
                query += $"\nAND qer.Exam_Div_Cd = '{Exam_Div_Cd}'";
            }

            if (Measure_Div_Cd != "" && Measure_Div_Cd != null)
            {
                query += $"\nAND qer.Measure_Div_Cd = '{Measure_Div_Cd}'";
            }

            return db.GetDataTable(query);
        }
        public DataTable Select_ExamResultByExamCd(string[] Exam_Cd)
        {
            //ValueType(CmCd : 017) - 10 : 합/불, 20 : 값
            query = $@"SELECT 
                             ROW_NUMBER() OVER(ORDER BY qer.Exam_Cd) AS RowNum
                            , qer.Exam_No 
                            , qer.Exam_No_Seq
                            , qer.Item_Cd
                            , qer.Exam_Cd
                            , em.Exam_Nm
                            , iem.Exam_Spec
                            , iem.Exam_Method
                            , iem.Exam_Unit
                            , qer.X1
                            , qer.X2
                            , qer.X3
                            , qer.X4
                            , qer.X5
                            , qer.X6
                            , IIF(em.ValueType_Div_Cd = '10' , (CASE qer.X1 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X1 ) AS X1_OK
                            , IIF(em.ValueType_Div_Cd = '10' , (CASE qer.X2 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X2 ) AS X2_OK
                            , IIF(em.ValueType_Div_Cd = '10' , (CASE qer.X3 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X3 ) AS X3_OK
                            , IIF(em.ValueType_Div_Cd = '10' , (CASE qer.X4 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X4 ) AS X4_OK
                            , IIF(em.ValueType_Div_Cd = '10' , (CASE qer.X5 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X5 ) AS X5_OK
                            , IIF(em.ValueType_Div_Cd = '10' , (CASE qer.X6 WHEN 'Y' THEN 'OK' WHEN 'N' THEN 'NG' ELSE '' END ) , qer.X6 ) AS X6_OK
                            , qer.Pass_Div_Cd
                            , CASE qer.Pass_Div_Cd WHEN '10' THEN '합격' WHEN '20' THEN '불합격' ELSE '' END AS Pass_Div_Nm
                            , 'n=5/LOT' AS Supplier
                            , '-' AS Consumer
                            , qer.Action_Memo
                            , qer.Rmk

                            --개발용
                            , qer.Measure_Div_Cd
                            , CONVERT(INT, qer.Measure_Time) AS Measure_Time
                            , qer.In_FilePath
                            , CONVERT(DATE, qer.Exam_Date) AS Exam_Date

                            , qer.Ins_Emp_No
                            , emp.Emp_Nm AS Ins_Emp_Nm
                            , qer.Ins_Date_Time
                            , qer.Upd_Emp_No
                            , qer.Upd_Date_Time
                            
                            , iem.Mgnt_Min
                            , iem.Mgnt_Max
                            , iem.Danger_Min
                            , iem.Danger_Max

                         FROM QM_ExamResult qer
                    LEFT JOIN BS_ItemExam_Mgnt iem ON qer.Corp_Cd = iem.Corp_Cd AND iem.Exam_Cd = qer.Exam_Cd AND iem.Item_Cd = qer.Item_Cd
                    LEFT JOIN BS_Exam_Mgnt      em ON qer.Corp_Cd = em.Corp_Cd AND em.Exam_Cd = iem.Exam_Cd
                    LEFT JOIN SC_Emp           emp ON qer.Corp_Cd = emp.Corp_Cd AND qer.Ins_Emp_No = emp.Emp_No

                        WHERE qer.Corp_Cd = '{WrGlobal.CorpID}'
                          AND qer.Ref_No = '{Ref_No}'
                          AND qer.Ref_No_Seq = '{Ref_No_Seq}'
                        ";

            //품목 구분 여러개로 볼 경우
            if (Exam_Cd != null && Exam_Cd.Length > 0)
            {
                query += "\nAND qer.Exam_Cd IN(";
                for (int idx = 0; idx < Exam_Cd.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $"'{Exam_Cd[idx]}'";
                }
                query += ")";
            }


            //초중종같은 측정구분(초물, 중물,종물)이 있는 검사일경우 사용
            if (Measure_Div_Cd != null && Measure_Div_Cd != "")
            {
                query += $"\nAND qer.Measure_Div_Cd = '{Measure_Div_Cd}'";
            }

            query += "\nORDER BY iem.Disp_Seq";

            return db.GetDataTable(query);
        }

        public DataRow Select_ExamResult_Exsits()
        {
            /*
            //Frm_QM_ExamResult(품질검사)에서 사용하는 Query
            query = $@"SELECT TOP 1 Exam_No
                            , Exam_No_Seq
                            , In_FilePath
                         FROM QM_ExamResult
                        WHERE Ref_No = '{Ref_No}'
                          AND Ref_No_Seq = {Ref_No_Seq}
                          AND Exam_Div_Cd = '{Exam_Div_Cd}' ";

            if(Exam_Div_Cd == "40")
                query += $"\n AND Measure_Div_Cd = '{Measure_Div_Cd}' ";

            query += $"\nORDER BY Exam_No, Exam_No_Seq DESC";
            */


            query = $@"SELECT TOP 1 Exam_No
                            , Exam_No_Seq
                            , In_FilePath
                            , File_Ext
                         FROM QM_ExamResult
                        WHERE Ref_No = '{Ref_No}'
                          AND Ref_No_Seq = {Ref_No_Seq}
                          AND Exam_Div_Cd = '{Exam_Div_Cd}'";

            if (Measure_Div_Cd != "" && Measure_Div_Cd != null)
            {
                query += $"\nAND Measure_Div_Cd = '{Measure_Div_Cd}' ";
            }

            query += $"\nORDER BY Exam_No, Exam_No_Seq DESC";

            return db.GetDataRow(query);
        }

        public DataRow Select_ExamResult_Exsits_No()
        {
            /*
            //Frm_QM_ExamResult(품질검사)에서 사용하는 Query
            query = $@"SELECT TOP 1 Exam_No
                            , Exam_No_Seq
                            , In_FilePath
                         FROM QM_ExamResult
                        WHERE Ref_No = '{Ref_No}'
                          AND Ref_No_Seq = {Ref_No_Seq}
                          AND Exam_Div_Cd = '{Exam_Div_Cd}' ";

            if(Exam_Div_Cd == "40")
                query += $"\n AND Measure_Div_Cd = '{Measure_Div_Cd}' ";

            query += $"\nORDER BY Exam_No, Exam_No_Seq DESC";
            */


            query = $@"SELECT TOP 1 Exam_No
                            , Exam_No_Seq
                            , In_FilePath
                            , File_Ext
                         FROM QM_ExamResult
                        WHERE Ref_No = '{Ref_No}'
                          AND Exam_Div_Cd = '{Exam_Div_Cd}'
                    ";

            if (Measure_Div_Cd != "" && Measure_Div_Cd != null)
            {
                query += $"\nAND Measure_Div_Cd = '{Measure_Div_Cd}' ";
            }

            query += $"\nORDER BY Exam_No, Exam_No_Seq DESC";

            return db.GetDataRow(query);
        }

        public DataTable Select_ExamListView_PQC(string startDt, string endDt, string[] Item_Div, string processType)
        {
            Line_Cd = Line_Cd.ConvertStr_Empty_To_Null();
            Process_Cd = Process_Cd.ConvertStr_Empty_To_Null();
            Machine_Cd = Machine_Cd.ConvertStr_Empty_To_Null();
            Item_Cd = Item_Cd.ConvertStr_Empty_To_Null();

            query = $@"

                    SELECT	
                    
                     er.Exam_No 
                    , er.Exam_No_Seq
                    , er.Item_Cd 
                    , CONVERT(DATE, er.Exam_Date) AS ExamDate
                    
                    , eim.Exam_Spec
                    , em.Exam_Nm AS Title
                    , eim.Exam_Method
                    , eim.Exam_Unit
                    , er.Measure_Div_Cd ------
                    , er.X1
                    , er.X2
                    , er.X3
                    , er.X4
                    , er.X5
                    , er.X6
                    , er.Exam_Qty
                    , er.Action_Memo

                    , OP.ProcessCode
                    , CP.ProcessName

                    , OP.LineCode
                    , CL.LineName

                    , OP.MachineCode
                    , MC.MachineName

                    , OP.OrderNo
                    , OP.OrderSeq
                    , OP.OrderQty
                    , OP.WorkQty
                    , OP.GoodQty
                    , OP.NgQty

                    --, WO.MaterialCode ProductCode
                    --, CM2.MaterialName ProductName

                    , OP.HalfMaterialCode MaterialCode
                    , CM.MaterialName MaterialName
                    , CM.Alias MaterialAliass

                    , odr.SubSeq
                    , odr.SerialNo

                    --개발용
                    , IIF(er.Ref_No IS NULL, 'N', 'Y') AS ExamYn
                    
                    FROM QM_ExamResult er
                    INNER JOIN PM_OrderResult odr ON er.Corp_Cd = odr.CorpId AND odr.SerialNo = er.Ref_No
                    INNER JOIN PM_OrderProcess op ON er.Corp_Cd = op.CorpId AND odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq

                    INNER JOIN BS_Exam_Mgnt em ON er.Exam_Cd = em.Exam_Cd AND er.Corp_Cd = em.CorpId
                    INNER JOIN BS_ItemExam_Mgnt eim ON er.Item_Cd = eim.Item_Cd  AND er.Exam_Cd = eim.Exam_Cd AND eim.CorpId = er.Corp_Cd
                    INNER JOIN CM_Item cm ON er.Corp_Cd = cm.CorpId AND er.Item_Cd = cm.MaterialCode
                    INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode
                    LEFT JOIN CM_Line CL ON OP.LineCode = CL.LineCode
                    LEFT JOIN EQ_Machine MC ON OP.MachineCode = MC.MachineCode

                    WHERE er.Corp_Cd = '{WrGlobal.CorpID}' 
                    AND er.Exam_Date BETWEEN '{startDt}' AND '{endDt}'
                    AND er.Exam_Div_Cd = '{Exam_Div_Cd}'
                    ";


            //if (Line_Cd != "NULL")
            //{
            //    query += $"\nAND pr.Line_Cd = {Line_Cd}";
            //}

            if (processType != "")
            {
                query += $"\nAND CP.ProcessType = '{processType}' --10:가공, 20:조립";
            }

            if (Process_Cd != "NULL")
            {
                query += $"\nAND pr.ProcessCode = {Process_Cd}";
            }
            if (Machine_Cd != "NULL")
            {
                query += $"\nAND pr.MachineCode = {Machine_Cd}";
            }
            if (Item_Cd != "NULL")
            {
                query += $"\nAND pr.Item_Cd = {Item_Cd}";
            }

            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND cm.MaterialType IN(";
                for (int idx = 0; idx < Item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $"'{Item_Div[idx]}'";
                }
                query += ")";
            }

            query += "\nORDER BY er.Exam_No, er.Exam_No_Seq, er.Measure_Div_Cd ";

            return db.GetDataTable(query);
        }

        public DataTable Select_ExamListView_IQC(string startDt, string endDt, string lupItem, string[] Item_Div = null)
        {
            Cust_Cd = Cust_Cd.ConvertStr_Empty_To_Null();

            query = $@"

                    SELECT	
                    
                     er.Exam_No 
                    , er.Exam_No_Seq
                    , CONVERT(DATE, er.Exam_Date) AS ExamDate

                    , er.Item_Cd AS MaterialCode
                    , cm.MaterialName
                    , cm.Alias

                    , eim.Exam_Spec
                    , em.Exam_Nm AS Title
                    , eim.Exam_Method
                    , eim.Exam_Unit

                    , er.Measure_Div_Cd ------
                    , er.X1
                    , er.X2
                    , er.X3
                    , er.X4
                    , er.X5
                    , er.X6
                    , er.Exam_Qty
                    , er.Action_Memo

					, er.In_FilePath
					, er.File_Ext

					, pd.StoNo
					, pd.StoSeq
					, pd.Qty

					, pm.CustCode
					, cc.CustName

                    --개발용
                    
                    FROM QM_ExamResult er
                    INNER JOIN BS_Exam_Mgnt em ON er.Exam_Cd = em.Exam_Cd AND er.Corp_Cd = em.CorpId
                    INNER JOIN BS_ItemExam_Mgnt eim ON er.Item_Cd = eim.Item_Cd  AND er.Exam_Cd = eim.Exam_Cd AND eim.CorpId = er.Corp_Cd
                    INNER JOIN CM_Item cm ON er.Corp_Cd = cm.CorpId AND er.Item_Cd = cm.MaterialCode
                    LEFT JOIN MM_PurchaseD pd ON er.Corp_Cd = pd.CorpId AND er.Ref_No = pd.StoNo AND er.Ref_No_Seq = pd.StoSeq
                    LEFT JOIN PurchaseMaster pm ON er.Corp_Cd = pm.CorpId AND pd.StoNo = pm.StoNo
                    LEFT JOIN CM_Cust cc ON er.Corp_Cd = cc.CorpId AND pm.CustCode = cc.CustCode



                    WHERE er.Corp_Cd = '{WrGlobal.CorpID}' 
                    AND er.Exam_Date BETWEEN '{startDt}' AND '{endDt}'
                    AND er.Exam_Div_Cd = '10' --수입검사";


            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND cm.MaterialType IN(";
                for (int idx = 0; idx < Item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $"'{Item_Div[idx]}'";
                }
                query += ")";
            }

            //if (Cust_Cd != "NULL")
            //{
            //    query += $"\nAND pr.Cust_Cd = {Cust_Cd}";
            //}

            if (lupItem != null && lupItem != "")
            {
                query += $"\nAND er.Item_Cd = '{lupItem}'";
            }

            query += "\nORDER BY Exam_No, Exam_No_Seq ";


            return db.GetDataTable(query);
        }

        public DataTable Select_ExamListView_OutSourcingIQC(string startDt, string endDt, string lupItem, string[] Item_Div = null)
        {
            Cust_Cd = Cust_Cd.ConvertStr_Empty_To_Null();

            query = $@"

                    SELECT	
                    
                     er.Exam_No 
                    , er.Exam_No_Seq
                    , CONVERT(DATE, er.Exam_Date) AS ExamDate

                    , er.Item_Cd AS MaterialCode
                    , cm.MaterialName
                    , cm.Alias

                    , eim.Exam_Spec
                    , em.Exam_Nm AS Title
                    , eim.Exam_Method
                    , eim.Exam_Unit

                    , er.Measure_Div_Cd ------
                    , er.X1
                    , er.X2
                    , er.X3
                    , er.X4
                    , er.X5
                    , er.X6
                    , er.Exam_Qty
                    , er.Action_Memo

					, er.In_FilePath
					, er.File_Ext

					, pd.OSNo
					, pd.OSSeq
					, pd.Qty

					, pm.CustCode
					, cc.CustName

                    --개발용
                    
                    FROM QM_ExamResult er
                    INNER JOIN BS_Exam_Mgnt em ON er.Exam_Cd = em.Exam_Cd AND er.Corp_Cd = em.CorpId
                    INNER JOIN BS_ItemExam_Mgnt eim ON er.Item_Cd = eim.Item_Cd  AND er.Exam_Cd = eim.Exam_Cd AND eim.CorpId = er.Corp_Cd
                    INNER JOIN CM_Item cm ON er.Corp_Cd = cm.CorpId AND er.Item_Cd = cm.MaterialCode
                    LEFT JOIN OutSourcingDetail pd ON er.Corp_Cd = pd.CorpId AND er.Ref_No = pd.OSNo AND er.Ref_No_Seq = pd.OSSeq
                    LEFT JOIN OutSourcingMaster pm ON er.Corp_Cd = pm.CorpId AND pd.OSNo = pm.OSNo
                    LEFT JOIN CM_Cust cc ON er.Corp_Cd = cc.CorpId AND pm.CustCode = cc.CustCode

                    WHERE er.Corp_Cd = '{WrGlobal.CorpID}' 
                    AND er.Exam_Date BETWEEN '{startDt}' AND '{endDt}'
                    AND er.Exam_Div_Cd = '70' --70입고검사";


            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND cm.MaterialType IN(";
                for (int idx = 0; idx < Item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $"'{Item_Div[idx]}'";
                }
                query += ")";
            }

            //if (Cust_Cd != "NULL")
            //{
            //    query += $"\nAND pr.Cust_Cd = {Cust_Cd}";
            //}

            if (lupItem != null && lupItem != "")
            {
                query += $"\nAND er.Item_Cd = '{lupItem}'";
            }

            query += "\nORDER BY Exam_No, Exam_No_Seq ";


            return db.GetDataTable(query);
        }

        public DataTable Select_ExamList_OQCOut(string startDt, string endDt, string[] Item_Div = null)
        {
            query = $@"SELECT	
		                     er.Exam_No 
	                    	, er.Exam_No_Seq
		                    , er.Item_Cd 
		                    , CONVERT(DATE, er.Exam_Date) AS ExamDate
		                    , pr.Cust_Cd
		                    , cm.Cust_Nm

		                    , bic.ItemClass_Nm

							, bi.Alias  ----------
		                    , bi.Item_Nm
		                    , bi.Spec
		                    , bi.Unit
		                    , bi.ShortName
		                    , bi.Base_Weight
		                    , bi.Type


		                    , eim.Exam_Spec
		                    , em.Exam_Nm AS Title
                            , eim.Exam_Method
                            , eim.Exam_Unit
							, er.Measure_Div_Cd ------S
		                    , er.X1
		                    , er.X2
		                    , er.X3
		                    , er.X4
		                    , er.X5
		                    , er.X6
		                    , er.Exam_Qty
		                    , er.Action_Memo

                            --개발용
                            , IIF(er.Ref_No IS NULL, 'N', 'Y') AS ExamYn

                            FROM QM_ExamResult er
                            --INNER JOIN BS_Customer cm ON pr.Cust_Cd = cm.Cust_Cd
							--INNER JOIN BN_ProductOut pr ON pr.Out_No = er.Ref_No and pr.Out_Row_Seq = er.Ref_No_Seq -----
							LEFT JOIN BN_ProductOut pr ON pr.Out_No = er.Ref_No and pr.Out_Row_Seq = er.Ref_No_Seq AND pr.Corp_Cd = er.Corp_Cd
                            LEFT JOIN BS_Customer cm ON pr.Cust_Cd = cm.Cust_Cd AND cm.Corp_Cd = er.Corp_Cd
                            INNER JOIN BS_Exam_Mgnt em ON er.Exam_Cd = em.Exam_Cd AND em.Corp_Cd = er.Corp_Cd
                            INNER JOIN BS_ItemExam_Mgnt eim ON er.Item_Cd = eim.Item_Cd  AND er.Exam_Cd = eim.Exam_Cd AND eim.Corp_Cd = er.Corp_Cd
                            INNER JOIN BS_Item bi ON er.Item_Cd = bi.Item_Cd AND bi.Corp_Cd = er.Corp_Cd
                            INNER JOIN BS_ItemClass bic ON bic.ItemClass_Cd = bi.ItemClass_Cd AND bic.Corp_Cd = er.Corp_Cd

                            WHERE er.Corp_Cd = '{WrGlobal.CorpID}' 
                               AND er.Exam_Date BETWEEN '{startDt}' AND '{endDt}'
                               AND er.Exam_Div_Cd = '{Exam_Div_Cd}' ";

            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND bi.Item_Div_Cd IN(";
                for (int idx = 0; idx < Item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $"'{Item_Div[idx]}'";
                }
                query += ")";
            }

            query += "\nORDER BY Exam_No, Exam_No_Seq ";


            return db.GetDataTable(query);
        }

        public DataTable Select_ExamList_OQCPIn(string startDt, string endDt, string[] Item_Div = null)
        {
            query = $@"SELECT	
		                     er.Exam_No 
	                    	, er.Exam_No_Seq
		                    , er.Item_Cd 
		                    , CONVERT(DATE, er.Exam_Date) AS ExamDate
		                    --, pr.Cust_Cd
		                    --, cm.Cust_Nm

		                    , bic.ItemClass_Nm

							, bi.Alias  ----------
		                    , bi.Item_Nm
		                    , bi.Spec
		                    , bi.Unit
		                    , bi.ShortName
		                    , bi.Base_Weight
		                    , bi.Type

		                    , eim.Exam_Spec
		                    , em.Exam_Nm AS Title
                            , eim.Exam_Method
                            , eim.Exam_Unit
							, er.Measure_Div_Cd ------
		                    , er.X1
		                    , er.X2
		                    , er.X3
		                    , er.X4
		                    , er.X5
		                    , er.X6
		                    , er.Exam_Qty
		                    , er.Action_Memo

                            , pr.Line_Cd
                        	, pr.Process_Cd
                        	, pr.Machine_Cd

                            --개발용
                            , IIF(er.Ref_No IS NULL, 'N', 'Y') AS ExamYn

                            FROM QM_ExamResult er
							LEFT JOIN PD_ProductIn ppi ON ppi.ProdIn_No = er.Ref_No and ppi.ProdIn_Row_Seq = er.Ref_No_Seq AND ppi.Corp_Cd = er.Corp_Cd
                            LEFT JOIN PD_ProcessResult pr ON ppi.Corp_Cd = pr.Corp_Cd AND ppi.Rst_No = pr.Rst_No AND ppi.Rst_Row_Seq = pr.Rst_Row_Seq
                            --LEFT JOIN BS_Customer cm ON pr.Cust_Cd = cm.Cust_Cd AND cm.Corp_Cd = er.Corp_Cd
                            INNER JOIN BS_Exam_Mgnt em ON er.Exam_Cd = em.Exam_Cd AND em.Corp_Cd = er.Corp_Cd
                            INNER JOIN BS_ItemExam_Mgnt eim ON er.Item_Cd = eim.Item_Cd  AND er.Exam_Cd = eim.Exam_Cd AND eim.Corp_Cd = er.Corp_Cd
                            INNER JOIN BS_Item bi ON er.Item_Cd = bi.Item_Cd AND bi.Corp_Cd = er.Corp_Cd
                            INNER JOIN BS_ItemClass bic ON bic.ItemClass_Cd = bi.ItemClass_Cd AND bic.Corp_Cd = er.Corp_Cd

                            WHERE er.Corp_Cd = '{WrGlobal.CorpID}' 
                               AND er.Exam_Date BETWEEN '{startDt}' AND '{endDt}'
                               AND er.Exam_Div_Cd = '{Exam_Div_Cd}' ";

            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND bi.Item_Div_Cd IN(";
                for (int idx = 0; idx < Item_Div.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $"'{Item_Div[idx]}'";
                }
                query += ")";
            }

            query += "\nORDER BY Exam_No, Exam_No_Seq ";


            return db.GetDataTable(query);
        }

        public DataRow Select_ExamXR_ChartInfo(string startDt, string endDt, float LSL = 0, float USL = 0, double C = 1.023)
        {
            //X-BarR 관리도 Chart셋팅정보 호출 Query
            query = $@"DECLARE @XBar_K	FLOAT = {C},	--XBar 관리계수
                    		   @USL		FLOAT = {USL},
                               @LSL		FLOAT = {LSL}
                    
                    SELECT MIN(X_Bar) AS [X-Bar_Min]
                    	, MAX(X_Bar) AS [X-Bar_Max]
                    	, AVG(X_Bar) AS [X-Bar_CL]
                    	
                    	--표준편차 :√(합((평균 - X_Bar)^2) / (데이터 수 - 1))
                    	, ISNULL(STDEV(X_Bar), 0) AS[X-Bar_표준편차]
                    	
                    	--[X-Bar_LCL] = CL - (X_Bar 관리계수 * R-CL)
                    	, AVG(X_Bar) - (@XBar_K * AVG([RANGE])) AS [X-Bar_LCL]
                    	--[X-Bar_UCL] = CL + (X_Bar 관리계수 * R-CL)
                    	, AVG(X_Bar) + (@XBar_K * AVG([RANGE])) AS [X-Bar_UCL]
                    	
                    	--Cp = (규격상한 - 규격하한) / (6 * 표준편차), 
                    	, ABS(@USL - @LSL) / (6 * STDEV(X_Bar)) AS Cp
                    	--K(치우침계수) = | CL - ((규격상한 + 규격하한) / 2) | * ((규격상한 - 규격하한) / 2)
                    	, CASE WHEN @LSL <> 0 AND @USL <> 0
                    			THEN ABS(AVG(X_Bar) - ((@USL + @LSL) / 2)) / ((@USL + @LSL) / 2)
                    		ELSE NULL
                    	END AS K
                    	
                    	--Cpk = (1 - 치우침계수) * Cp, 규격상한, 하한이 둘다 있을 때 치우침계수가 반영된 Cp
                    	, CASE WHEN @LSL <> 0 AND @USL <> 0
                    			THEN (1 - ABS(AVG(X_Bar) - ((@USL + @LSL) / 2)) / ((@USL + @LSL) / 2))
                    			    * ((@USL - @LSL) / (6 * STDEV(X_Bar)))
                    		ELSE NULL
                    	END
                    	AS [Cpk]
                    	--Cpl = (CL - 규격하한) / (3 * 표준편차), 규격하한만 존재할 때 규격 하한에 가까운 정도
                    	, CASE WHEN @LSL <> 0
                    	          THEN (AVG(X_Bar) - @LSL) / (3 * STDEV(X_Bar))
                    	ELSE NULL
                    	END AS [Cpl]
                    	--Cpu = (규격상한 - CL) / (3 * 표준편차), 규격상한만 존재할 때 규격 상한에 가까운 정도
                    	, CASE WHEN @USL <> 0
                    	          THEN (@USL - AVG(X_Bar)) / (3 * STDEV(X_Bar))
                    	ELSE NULL
                    	END AS [Cpu]
                    
                    	, MAX([Range]) AS [R_Max]
                    	, MIN([Range]) AS [R_Min]
                        , AVG([Range]) AS [R_CL]
                        , ISNULL(STDEV([Range]), 0) AS [R_표준편차]
                         --R-LCL = R-CL - (R_Bar 관리계수 * R_CL)
                        , CASE WHEN AVG([Range]) - (@XBar_K * AVG([Range])) < 0 THEN 0
                    	    ELSE AVG([Range]) - (@XBar_K * AVG([Range]))
                    	 END AS [R_LCL]
                         --R_UCL = R-CL + (R_Bar 관리계수 * R-CL)
                        , AVG([Range]) + (@XBar_K * AVG([Range])) AS [R_UCL]
                    
                    	, MAX(Seq) AS [Data_Cnt]
                    FROM
                    (
                    	SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS Seq
	                         , Corp_Cd
	                         , CONCAT(Exam_No, '/', Exam_No_Seq) AS Lot_No
	                         , Item_Cd
	                         , AVG(CONVERT(FLOAT, ExamValue)) AS X_Bar	--XBar Chart Value값
	                         , MAX(CONVERT(FLOAT, ExamValue)) - MIN(CONVERT(FLOAT, ExamValue)) AS [Range]	--R Chart Value값
	                      FROM QM_ExamResult
	                   UNPIVOT
                             (
	                    	   ExamValue
	                    	   FOR ValueSeq
	                    	   IN([X1], [X2], [X3], [X4], [X5], [X6])
	                         ) AS Piv
	                     WHERE Corp_Cd = '{WrGlobal.CorpID}'
	                       AND Exam_Div_Cd = '{Exam_Div_Cd}'
	                       AND Exam_Cd = '{Exam_Cd}'
	                       AND Item_Cd = '{Item_Cd}'
	                       AND Exam_Date BETWEEN '{startDt}' AND '{endDt}'
	                       --AND Measure_Div_Cd = NULL
	                  GROUP BY Corp_Cd, CONCAT(Exam_No, '/', Exam_No_Seq), Item_Cd
                    ) T
                    GROUP BY T.Item_Cd";

            return db.GetDataRow(query);
        }

        public DataTable Select_ExamXR_DataList(string startDt, string endDt)
        {
            //X-BarR 관리도 데이터 호출 Query

            query = $@"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS Seq
	                        , Corp_Cd
	                        , CONCAT(Exam_No, '/', Exam_No_Seq) AS Lot_No
	                        , Item_Cd
	                        , AVG(CONVERT(FLOAT, ExamValue)) AS X_Bar	--XBar Chart Value값
	                        , MAX(CONVERT(FLOAT, ExamValue)) - MIN(CONVERT(FLOAT, ExamValue)) AS [Range]	--R Chart Value값
	                     FROM QM_ExamResult
	                  UNPIVOT
                            (
	                   	        ExamValue
	                   	        FOR ValueSeq
	                   	        IN([X1], [X2], [X3], [X4], [X5], [X6])
	                        ) AS Piv
	                    WHERE Corp_Cd = '{WrGlobal.CorpID}'
	                      AND Exam_Div_Cd = '{Exam_Div_Cd}'
	                      AND Exam_Cd = '{Exam_Cd}'
	                      AND Item_Cd = '{Item_Cd}'
	                      AND Exam_Date BETWEEN '{startDt}' AND '{endDt}'
	                      --AND Measure_Div_Cd = NULL
	                 GROUP BY Corp_Cd, CONCAT(Exam_No, '/', Exam_No_Seq), Item_Cd";

            return db.GetDataTable(query);
        }

        public DataRow Select_ExamXR_ByExam_No()
        {
            //검사 결과 1건에 대한 X1~X6 평균, 표준편차, 최소/최대 값 구하기
            query = $@"
	                    SELECT AVG(XVlaue) AS Value_Avg, STDEV(XVlaue) AS Value_StDev, MIN(XVlaue) AS Value_Min, MAX(XVlaue) AS Value_Max
	                    FROM (
	                    	    SELECT Exam_No, Exam_No_Seq, X, CONVERT(FLOAT, XVlaue) AS XVlaue
	                    	    FROM QM_ExamResult
	                    	    UNPIVOT ( XVlaue FOR X IN ([X1], [X2], [X3], [X4], [X5], [X6])) AS UNPVT
	                    	    WHERE Corp_Cd = '{WrGlobal.CorpID}'
                                AND Exam_No = '{Exam_No}'
	                    	    AND Exam_No_Seq = '{Exam_No_Seq}'
	                    ) AS A
                    ";

            return db.GetDataRow(query);
        }


        public DataTable Select_XValue_ByExam_No()
        {
            //검사 결과 1건에 대한 XVlaue
            query = $@"
	                    SELECT Exam_No, Exam_No_Seq, REPLACE(X, 'X', '') AS XType, CONVERT(FLOAT, XVlaue) AS XValue
	                    FROM QM_ExamResult
	                    UNPIVOT ( XVlaue FOR X IN ([X1], [X2], [X3], [X4], [X5], [X6])) AS UNPVT
	                    WHERE Corp_Cd = '{WrGlobal.CorpID}'
                           AND Exam_No = '{Exam_No}'
	                    AND Exam_No_Seq = '{Exam_No_Seq}'
                    ";

            return db.GetDataTable(query);
        }

        public DataRow Select_ExamXR_ByExam_No_KH(string MeasDivCd)
        {
            //검사 결과 1건에 대한 X1~X6 평균, 표준편차, 최소/최대 값 구하기
            query = $@"
	                    SELECT AVG(XVlaue) AS Value_Avg, STDEV(XVlaue) AS Value_StDev, MIN(XVlaue) AS Value_Min, MAX(XVlaue) AS Value_Max
	                    FROM (
	                    	    SELECT Meas_No, Meas_No_Seq, X, CONVERT(FLOAT, XVlaue) AS XVlaue
	                    	    FROM QT_CoatingThickness
	                    	    UNPIVOT ( XVlaue FOR X IN ([X1], [X2], [X3], [X4], [X5])) AS UNPVT
	                    	    WHERE Corp_Cd = '{WrGlobal.CorpID}'
                                AND Meas_No = '{Exam_No}'
	                    	    --AND Meas_No_Seq = '{Exam_No_Seq}' 
                                AND Meas_Div_Cd = '{MeasDivCd}'
	                    ) AS A
                    ";

            return db.GetDataRow(query);
        }


        public DataTable Select_XValue_ByExam_No_KH(string MeasDivCd)
        {
            //검사 결과 1건에 대한 XVlaue
            query = $@"
	                    SELECT Meas_No, Meas_No_Seq, REPLACE(X, 'X', '') AS XType, CONVERT(FLOAT, XVlaue) AS XValue
	                    FROM QT_CoatingThickness
	                    UNPIVOT ( XVlaue FOR X IN ([X1], [X2], [X3], [X4], [X5])) AS UNPVT
	                    WHERE Corp_Cd = '{WrGlobal.CorpID}'
                        AND Meas_No = '{Exam_No}'
	                    --AND Meas_No_Seq = '{Exam_No_Seq}' 
                        AND Meas_Div_Cd = '{MeasDivCd}'
                    ";

            return db.GetDataTable(query);
        }


        public DataRow SelectExamPass()
        {
            query = $@"

                            DECLARE @PASS VARCHAR(10) = '', @CNT INT = 0
						  
							IF EXISTS 
							(
								SELECT Ref_No, Ref_No_Seq, Pass_Div_Cd
								FROM QM_ExamResult
								WHERE Corp_Cd = '{WrGlobal.CorpID}'
								AND Exam_Div_Cd = '{Exam_Div_Cd}'  --
								AND Ref_No = '{Ref_No}'
								AND Ref_No_Seq = '{Ref_No_Seq}'
							)
							BEGIN

									SELECT @CNT = COUNT(Pass_Div_Cd)
									FROM QM_ExamResult
									WHERE Corp_Cd = '{WrGlobal.CorpID}'
									AND Exam_Div_Cd = '{Exam_Div_Cd}'  --출하검사
									AND Ref_No = '{Ref_No}'
									AND Ref_No_Seq = '{Ref_No_Seq}'
									AND Pass_Div_Cd = '20' --불합격
						  		
								
								IF @CNT > 0
								BEGIN
									SET @PASS = 'N'
								END 
								ELSE
								BEGIN
									SET @PASS = 'Y'
								END

							END
							

                        SELECT @PASS
                        
";
            return db.GetDataRow(query);
        }

        public DataRow SelectExamPassQty()
        {
            query = $@"

						SELECT Ref_No
						    , Ref_No_Seq
						    , CASE WHEN Pass_Div_Cd = 10 THEN '합격' ELSE '불합격' END
						    ,Exam_Qty
						FROM QM_ExamResult					
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
						AND Exam_Div_Cd = '{Exam_Div_Cd}'  --
						AND Ref_No = '{Ref_No}'
						AND Ref_No_Seq = '{Ref_No_Seq}'                        
                      ";
            return db.GetDataRow(query);
        }

        #endregion Select문 End

        public bool Ins_ExamResult()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO QM_ExamResult(Corp_Cd, Exam_No, Exam_No_Seq, Disp_Seq, Exam_Date
                                               , Item_Cd, Exam_Div_Cd, Exam_Cd, Ref_No, Ref_No_Seq
                                               , Exam_Qty, Measure_Div_Cd, Measure_Time
                                               , X1, X2, X3, X4, X5, X6
                                               , Pass_Div_Cd, Action_Memo, Rmk
                                               , Ins_Emp_No, Ins_Date_Time)
                            VALUES ('{WrGlobal.CorpID}'
                                  , {Exam_No.ConvertStr_Empty_To_Null()}
                                  , {Exam_No_Seq.ConvertInt_Empty_To_Null()}
                                  , {Disp_Seq.ConvertInt_Empty_To_Null()}
                                  , {Exam_Date.ConvertStr_Empty_To_Null()}

                                  , {Item_Cd.ConvertStr_Empty_To_Null()}
                                  , {Exam_Div_Cd.ConvertStr_Empty_To_Null()}
                                  , {Exam_Cd.ConvertStr_Empty_To_Null()}
                                  , {Ref_No.ConvertStr_Empty_To_Null()}
                                  , {Ref_No_Seq.ConvertInt_Empty_To_Null()}

                                  , {Exam_Qty.ConvertFloat_Empty_To_Null()}
                                  , {Measure_Div_Cd.ConvertStr_Empty_To_Null()}
                                  , {Measure_Time.ConvertStr_Empty_To_Null()}

                                  , {X1.ConvertStr_Empty_To_Null()}
                                  , {X2.ConvertStr_Empty_To_Null()}
                                  , {X3.ConvertStr_Empty_To_Null()}
                                  , {X4.ConvertStr_Empty_To_Null()}
                                  , {X5.ConvertStr_Empty_To_Null()}
                                  , {X6.ConvertStr_Empty_To_Null()}

                                  , {Pass_Div_Cd.ConvertStr_Empty_To_Null()}
                                  , {Action_Memo.ConvertStr_Empty_To_Null()}
                                  , {Rmk.ConvertStr_Empty_To_Null()}

                                  ,'{WrGlobal.LoginID}'
                                  ,GETDATE() )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ExamResult()
        {
            // Exam_Div_Cd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamResult
                          SET X1 = {X1.ConvertStr_Empty_To_Null()}
                            , X2 = {X2.ConvertStr_Empty_To_Null()}
                            , X3 = {X3.ConvertStr_Empty_To_Null()}
                            , X4 = {X4.ConvertStr_Empty_To_Null()}
                            , X5 = {X5.ConvertStr_Empty_To_Null()}
                            , X6 = {X6.ConvertStr_Empty_To_Null()}

                            
                            , Exam_Date = {Exam_Date}
                            , Measure_Time = {Measure_Time.ConvertStr_Empty_To_Null()}

                            , Pass_Div_Cd = {Pass_Div_Cd.ConvertStr_Empty_To_Null()}
                            , Action_Memo = {Action_Memo.ConvertStr_Empty_To_Null()}
                            , Rmk = {Rmk.ConvertStr_Empty_To_Null()}

                            , Upd_Emp_No = '{WrGlobal.LoginID}'
                            , Upd_Date_Time = GETDATE()

                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Exam_No = '{Exam_No}'
                          AND Exam_No_Seq = '{Exam_No_Seq}'";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ExamResult_FilePath()
        {
            // Exam_Div_Cd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamResult
                          SET In_FilePath = {In_FilePath.ConvertStr_Empty_To_Null()}
    
                            , Upd_Emp_No = '{WrGlobal.LoginID}'
                            , Upd_Date_Time = GETDATE()
                            , File_Ext = '{File_Ext}'

                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Ref_No = '{Ref_No}'
                          AND Ref_No_Seq = '{Ref_No_Seq}'
                          AND Exam_Div_Cd = '{Exam_Div_Cd}' ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ExamResult_FilePath_No()
        {
            // Exam_Div_Cd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamResult
                          SET In_FilePath = {In_FilePath.ConvertStr_Empty_To_Null()}
    
                            , Upd_Emp_No = '{WrGlobal.LoginID}'
                            , Upd_Date_Time = GETDATE()
                            , File_Ext = '{File_Ext}'

                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Ref_No = '{Ref_No}'
                          AND Exam_Div_Cd = '{Exam_Div_Cd}' ";

            return db.ExecuteQuery(query);
        }


        public bool Del_ExamResult()
        {
            query = $@"DELETE QM_ExamResult
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Exam_No = '{Exam_No}'";

            if (Exam_No_Seq != 0)
                query += $"\n AND Exam_No_Seq = {Exam_No_Seq}";

            return db.ExecuteQuery(query);
        }

        public bool Del_ExamResult_ByRefNo_Seq()
        {
            query = $@"DELETE QM_ExamResult
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Ref_No = '{Ref_No}'
                          AND Ref_No_Seq = '{Ref_No_Seq}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_ExamResult_ByRefNo()
        {
            query = $@"DELETE QM_ExamResult
                        WHERE Corp_Cd = '{WrGlobal.CorpID}'
                          AND Ref_No = '{Ref_No}'
                    ";

            if (Exam_Div_Cd != "" && Exam_Div_Cd != null)
            {
                query += $"\nAND Exam_Div_Cd = '{Exam_Div_Cd}'";
            }

            return db.ExecuteQuery(query);
        }

        public DataTable Select_ItemCd_List()
        {
            query = $@"SELECT DISTINCT er.Item_Cd 품목코드, it.Item_Nm 품명, it.Alias 품번
                                FROM QT_Proc_CPK er
                                LEFT JOIN BS_Item it ON er.Item_Cd = it.Item_Cd
                                WHERE 1=1 ";

            if (Exam_Div_Cd != "" && Exam_Div_Cd != null)
            {
                query += $" AND er.Exam_Div_Cd = '{Exam_Div_Cd}'";
            }

            query += " ORDER BY er.Item_Cd ";

            return db.GetDataTable(query);
        }

    }
}
