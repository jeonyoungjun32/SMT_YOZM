using SmartMES.Common;
using System.Data;
using SmartCommon.Common;

namespace SmartMES.Models.QM
{
	public class QM_ExamResult
    {
        #region Property

        public string ExamNo { get; set; }//검사번호
        public int ExamSeq { get; set; }//검사순번
        public int DispSeq { get; set; }//표시순번
        public string ExamDate { get; set; }//검사일자
        public string ItemCode { get; set; }//품목
        public string ExamDivCd { get; set; }//검사구분
        public string ExamCd { get; set; }//검사코드
        public string RefNo { get; set; }//참조번호
        public int RefSeq { get; set; }//참조순번
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
        public string FileExt { get; set; }//파일확장자
        public string ExamEmpId { get; set; }//검사자
        public string MachineCode { get; set; }//설비
        public string LotNo { get; set; }//LotNo
        public string PassDate { get; set; }//판정일자

        //조건용
        public string LineCode { get; set; }//라인
        public string ProcessCode { get; set; }//공정


        #endregion Property End

        Database db = new Database();
        string query;

        public DataTable Select_ExamResult(string ValueType = "")
        {
            //품질검사(Frm_QM_ExamResult)에서 사용하는 Query
            //ValueType(CmCd : 017) - 10 : 합/불, 20 : 값
            query = $@"SELECT
                              qer.ExamNo 
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
                            , qer.ExamQty

                            , qer.ExamEmpId

                            --개발용
                            , qer.ExamDivCd
                            , qer.MeasureDivCd
                            , qer.MeasureTime
                            , qer.InFilePath
                            , CONVERT(DATE, qer.ExamDate) AS ExamDate

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
                            , em.ExamCnt

                         FROM QM_ExamResult qer
                    LEFT JOIN QM_ItemExamMgnt iem ON iem.ExamCd = qer.ExamCd
                        	                      AND iem.ItemCode = qer.ItemCode AND iem.CorpId = qer.CorpId
                    LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd   AND qer.ExamDivCd = em.ExamDivCd     AND em.CorpId = qer.CorpId
                        WHERE qer.CorpId = '{WrGlobal.CorpID}'
                          AND qer.RefNo = '{RefNo}'";

            //공정검사인 경우 SerialNo 사용으로 Seq가 없어서 분리
            if(RefSeq != 0)
			{
                query += $"\nAND qer.RefSeq = '{RefSeq}'";
			}

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

            query += "\nORDER BY iem.DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_ExamResult_TPG(string ValueType = "")
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
                            , qer.ExamQty

                            , qer.ExamEmpId
                            , qer.LotNo

                            --개발용
                            , qer.MeasureDivCd
                            , qer.MeasureTime
                            , qer.InFilePath
                            , CONVERT(DATE, qer.ExamDate) AS ExamDate

                            , qer.CreId
                            , qer.CreDt
                            , qer.ModId
                            , qer.ModDt
                            , CONVERT(DATE, qer.PassDate) PassDate
                            
                            , iem.MgntMin
                            , iem.MgntMax
                            , iem.DangerMin
                            , iem.DangerMax
                            , em.ExamCnt

                         FROM QM_ExamResult qer
                    LEFT JOIN QM_ItemExamMgnt iem ON iem.ExamCd = qer.ExamCd
                        	                      AND iem.ItemCode = qer.ItemCode AND iem.CorpId = qer.CorpId
                    LEFT JOIN QM_ExamMgnt em ON em.ExamCd = iem.ExamCd   AND qer.ExamDivCd = em.ExamDivCd     AND em.CorpId = qer.CorpId
                        WHERE qer.CorpId = '{WrGlobal.CorpID}'
                          AND qer.RefNo = '{RefNo}'";

            //공정검사인 경우 SerialNo 사용으로 Seq가 없어서 분리
            if (RefSeq != 0)
            {
                query += $"\nAND qer.RefSeq = '{RefSeq}'";
            }

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

            query += "\nORDER BY iem.DispSeq";

            return db.GetDataTable(query);
        }

        public DataRow Select_ExamResult_Exsits()
        {
            query = $@"SELECT TOP 1 ExamNo
                            , ExamSeq
                            , InFilePath
                            , FileExt
                            , MeasureDivCd
                         FROM QM_ExamResult
                        WHERE RefNo = '{RefNo}'
                          AND RefSeq = {RefSeq}
                          AND ExamDivCd = '{ExamDivCd}'";

            if (MeasureDivCd != "" && MeasureDivCd != null)
            {
                query += $"\nAND MeasureDivCd = '{MeasureDivCd}' ";
            }

            query += $"\nORDER BY ExamNo, ExamSeq DESC";

            return db.GetDataRow(query);
        }

        public DataRow Select_ExamResult_Exsits_No()
        {
            /*
            //Frm_QM_ExamResult(품질검사)에서 사용하는 Query
            query = $@"SELECT TOP 1 ExamNo
                            , ExamSeq
                            , InFilePath
                         FROM QM_ExamResult
                        WHERE RefNo = '{RefNo}'
                          AND RefSeq = {RefSeq}
                          AND ExamDivCd = '{ExamDivCd}' ";

            if(ExamDivCd == "40")
                query += $"\n AND MeasureDivCd = '{MeasureDivCd}' ";

            query += $"\nORDER BY ExamNo, ExamSeq DESC";
            */


            query = $@"SELECT TOP 1 ExamNo
                            , ExamSeq
                            , InFilePath
                            , FileExt
                         FROM QM_ExamResult
                        WHERE RefNo = '{RefNo}'
                          AND ExamDivCd = '{ExamDivCd}'
                    ";

            if (MeasureDivCd != "" && MeasureDivCd != null)
            {
                query += $"\nAND MeasureDivCd = '{MeasureDivCd}' ";
            }

            query += $"\nORDER BY ExamNo, ExamSeq DESC";

            return db.GetDataRow(query);
        }

        public DataTable Select_ExamListView_PQC(string startDt, string endDt, string[] Item_Div, string processType)
        {
            LineCode = LineCode.ConvertStr_Empty_To_Null();
            ProcessCode = ProcessCode.ConvertStr_Empty_To_Null();
            MachineCode = MachineCode.ConvertStr_Empty_To_Null();
            ItemCode = ItemCode.ConvertStr_Empty_To_Null();

            query = $@"

                    SELECT	
                    
                     er.ExamNo 
                    , er.ExamSeq
                    , er.ItemCode 
                    , CONVERT(DATE, er.ExamDate) AS ExamDate
                    
                    , eim.ExamSpec
                    , em.ExamNm AS Title
                    , eim.ExamMethod
                    , eim.ExamUnit
                    , er.MeasureDivCd ------
                    , er.X1
                    , er.X2
                    , er.X3
                    , er.X4
                    , er.X5
                    , er.X6
                    , er.ExamQty
                    , er.ActionMemo

                    , er.RefNo
                    , er.RefSeq

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

                    , OP.ItemCode ItemCode
                    , CM.ItemName ItemName
                    , CM.Alias MaterialAliass

                    , odr.SubSeq
                    , odr.SerialNo

                    --개발용
                    , IIF(er.RefNo IS NULL, 'N', 'Y') AS ExamYn
                    
                    FROM QM_ExamResult er
                    INNER JOIN PM_OrderResult odr ON er.CorpId = odr.CorpId AND odr.SerialNo = er.RefNo
                    INNER JOIN PM_OrderProcess op ON er.CorpId = op.CorpId AND odr.OrderNo = op.OrderNo AND odr.OrderSeq = op.OrderSeq

                    INNER JOIN QM_ExamMgnt em ON er.ExamCd = em.ExamCd AND er.CorpId = em.CorpId
                    INNER JOIN QM_ItemExamMgnt eim ON er.ItemCode = eim.ItemCode  AND er.ExamCd = eim.ExamCd AND eim.CorpId = er.CorpId
                    INNER JOIN CM_Item cm ON er.CorpId = cm.CorpId AND er.ItemCode = cm.ItemCode
                    INNER JOIN CM_Process CP ON OP.ProcessCode = CP.ProcessCode
                    LEFT JOIN CM_Line CL ON OP.LineCode = CL.LineCode
                    LEFT JOIN EQ_Machine MC ON OP.MachineCode = MC.MachineCode

                    WHERE er.CorpId = '{WrGlobal.CorpID}' 
                    AND er.ExamDate BETWEEN '{startDt}' AND '{endDt}'
                    AND er.ExamDivCd = '{ExamDivCd}'
                    ";


            //if (Line_Cd != "NULL")
            //{
            //    query += $"\nAND pr.Line_Cd = {Line_Cd}";
            //}

            if(processType != "")
			{
                query += $"\nAND CP.ProcessType = '{processType}' --10:가공, 20:조립";
			}

            if (LineCode != "NULL")
            {
                query += $"\nAND op.LineCode = {LineCode}";
            }

            if (ProcessCode != "NULL")
            {
                query += $"\nAND odr.ProcessCode = {ProcessCode}";
            }

            if (MachineCode != "NULL")
            {
                query += $"\nAND op.MachineCode = {MachineCode}";
            }

            if (ItemCode != "NULL")
            {
                query += $"\nAND OP.ItemCode = {ItemCode}";
            }

            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND cm.ItemType IN(";
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

            query += "\nORDER BY er.ExamNo, er.ExamSeq, er.MeasureDivCd ";

            return db.GetDataTable(query);
        }

        public DataTable Select_ExamListView_IQC(string startDt, string endDt, string lupItem, string[] Item_Div = null)
        {
      
            query = $@"

                    SELECT	
                    
                     er.ExamNo 
                    , er.ExamSeq
                    , CONVERT(DATE, er.ExamDate) AS ExamDate

                    , er.ItemCode AS ItemCode
                    , cm.ItemName
                    , cm.Alias

                    , eim.ExamSpec
                    , em.ExamNm AS Title
                    , eim.ExamMethod
                    , eim.ExamUnit

                    , er.MeasureDivCd ------
                    , er.X1
                    , er.X2
                    , er.X3
                    , er.X4
                    , er.X5
                    , er.X6
                    , er.ExamQty
                    , er.ActionMemo

					, er.InFilePath
					, er.FileExt

					, pd.StoNo
					, pd.StoSeq
					, pd.Qty

					, pm.CustCode
					, cc.CustName
                    , (select dbo.FN_Get_CateItem_Title ('{WrGlobal.CorpID}','A010', cm.ItemType)) AS ItemTypeName --품목구분

                    --개발용
                    
                    FROM QM_ExamResult er
                    INNER JOIN QM_ExamMgnt em ON er.ExamCd = em.ExamCd AND er.CorpId = em.CorpId
                    INNER JOIN QM_ItemExamMgnt eim ON er.ItemCode = eim.ItemCode  AND er.ExamCd = eim.ExamCd AND eim.CorpId = er.CorpId
                    INNER JOIN CM_Item cm ON er.CorpId = cm.CorpId AND er.ItemCode = cm.ItemCode
                    INNER JOIN MM_PurchaseD pd ON er.CorpId = pd.CorpId AND er.RefNo = pd.StoNo AND er.RefSeq = pd.StoSeq
                    INNER JOIN MM_PurchaseM pm ON er.CorpId = pm.CorpId AND pd.StoNo = pm.StoNo
                    INNER JOIN CM_Cust cc ON er.CorpId = cc.CorpId AND pm.CustCode = cc.CustCode



                    WHERE er.CorpId = '{WrGlobal.CorpID}' 
                    AND er.ExamDate BETWEEN '{startDt}' AND '{endDt}'
                    AND er.ExamDivCd = '10' --수입검사";


            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND cm.ItemType IN(";
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
                query += $"\nAND er.ItemCode = '{lupItem}'";
            }

            query += "\nORDER BY ExamNo, ExamSeq ";


            return db.GetDataTable(query);
        }
   
        public DataTable Select_ExamList_OQCOut(string startDt, string endDt, string[] Item_Div = null)
        {
            query = $@"SELECT	
		                     er.ExamNo 
	                    	, er.ExamSeq
		                    , CONVERT(DATE, er.ExamDate) AS ExamDate
		                    , sm.BillToCode CustCode
		                    , cc.CustName

		                    , er.ItemCode ItemCode
		                    , cm.ItemName
							, cm.Alias  ----------
		                    , cm.Size
		                    , cm.Type


		                    , eim.ExamSpec
		                    , em.ExamNm AS Title
                            , eim.ExamMethod
                            , eim.ExamUnit
							, er.MeasureDivCd ------S
		                    , er.X1
		                    , er.X2
		                    , er.X3
		                    , er.X4
		                    , er.X5
		                    , er.X6
		                    , er.ExamQty
		                    , er.ActionMemo

		                    , er.RefNo
		                    , er.RefSeq
		                    , er.Remark

                            FROM QM_ExamResult er
							LEFT JOIN BM_SalesD sd ON sd.SalesNo = er.RefNo and sd.SalesSeq = er.RefSeq AND sd.CorpId = er.CorpId
							LEFT JOIN BM_SalesM sm ON sm.SalesNo = er.RefNo AND sm.CorpId = er.CorpId
                            LEFT JOIN CM_Cust cc ON sm.BillToCode = cc.CustCode AND cc.CorpId = er.CorpId
                            LEFT JOIN QM_ExamMgnt em ON er.ExamCd = em.ExamCd AND em.CorpId = er.CorpId
                            LEFT JOIN QM_ItemExamMgnt eim ON er.ItemCode = eim.ItemCode  AND er.ExamCd = eim.ExamCd AND eim.CorpId = er.CorpId
                            LEFT JOIN CM_Item cm ON er.ItemCode = cm.ItemCode AND cm.CorpId = er.CorpId

                            WHERE er.CorpId = '{WrGlobal.CorpID}' 
                               AND er.ExamDate BETWEEN '{startDt}' AND '{endDt}'
                               AND er.ExamDivCd = '{ExamDivCd}' ";

            //품목 구분 여러개로 볼 경우
            if (Item_Div != null && Item_Div.Length > 0)
            {
                query += "\nAND cm.ItemType  IN(";
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

            query += "\nORDER BY ExamNo, ExamSeq ";


            return db.GetDataTable(query);
        }

        public DataRow Select_ExamXR_ChartInfo(string startDt, string endDt, float LSL = 0, float USL = 0, double C = 1.023)
        {
            //X-BarR 관리도 Chart셋팅정보 호출 Query
            query = $@"
                        	SET ANSI_WARNINGS OFF
                        	SET ARITHIGNORE ON
                        	SET ARITHABORT OFF
                        
                        DECLARE @XBar_K	FLOAT = {C},	--XBar 관리계수
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
	                         , CorpId
	                         , CONCAT(ExamNo, '/', ExamSeq) AS Lot_No
	                         , ItemCode
	                         , AVG(CONVERT(FLOAT, ExamValue)) AS X_Bar	--XBar Chart Value값
	                         , MAX(CONVERT(FLOAT, ExamValue)) - MIN(CONVERT(FLOAT, ExamValue)) AS [Range]	--R Chart Value값
	                      FROM QM_ExamResult
	                   UNPIVOT
                             (
	                    	   ExamValue
	                    	   FOR ValueSeq
	                    	   IN([X1], [X2], [X3], [X4], [X5], [X6])
	                         ) AS Piv
	                     WHERE CorpId = '{WrGlobal.CorpID}'
	                       AND ExamDivCd = '{ExamDivCd}'
	                       AND ExamCd = '{ExamCd}'
	                       AND ItemCode = '{ItemCode}'
	                       AND ExamDate BETWEEN '{startDt}' AND '{endDt}'
	                       --AND MeasureDivCd = NULL
	                  GROUP BY CorpId, CONCAT(ExamNo, '/', ExamSeq), ItemCode
                    ) T
                    GROUP BY T.ItemCode";

            return db.GetDataRow(query);
        }

        public DataTable Select_ExamXR_DataList(string startDt, string endDt)
        {
            //X-BarR 관리도 데이터 호출 Query

            query = $@"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS Seq
	                        , CorpId
	                        , CONCAT(ExamNo, '/', ExamSeq) AS Lot_No
	                        , ItemCode
	                        , AVG(CONVERT(FLOAT, ExamValue)) AS X_Bar	--XBar Chart Value값
	                        , MAX(CONVERT(FLOAT, ExamValue)) - MIN(CONVERT(FLOAT, ExamValue)) AS [Range]	--R Chart Value값
	                     FROM QM_ExamResult
	                  UNPIVOT
                            (
	                   	        ExamValue
	                   	        FOR ValueSeq
	                   	        IN([X1], [X2], [X3], [X4], [X5], [X6])
	                        ) AS Piv
	                    WHERE CorpId = '{WrGlobal.CorpID}'
	                      AND ExamDivCd = '{ExamDivCd}'
	                      AND ExamCd = '{ExamCd}'
	                      AND ItemCode = '{ItemCode}'
	                      AND ExamDate BETWEEN '{startDt}' AND '{endDt}'
	                      --AND MeasureDivCd = NULL
	                 GROUP BY CorpId, CONCAT(ExamNo, '/', ExamSeq), ItemCode";

            return db.GetDataTable(query);
        }

        public bool Ins_ExamResult()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO QM_ExamResult(CorpId, ExamNo, ExamSeq, DispSeq, ExamDate
                                               , ItemCode, ExamDivCd, ExamCd, RefNo, RefSeq
                                               , ExamQty, MeasureDivCd, MeasureTime
                                               , X1, X2, X3, X4, X5, X6
                                               , PassDivCd, ActionMemo, Remark, ExamEmpId
                                               , MachineCode
                                               , CreId, CreIP, CreDt)
                            VALUES ('{WrGlobal.CorpID}'
                                  , {ExamNo.ConvertStr_Empty_To_Null()}
                                  , {ExamSeq.ConvertInt_Empty_To_Null()}
                                  , {DispSeq.ConvertInt_Empty_To_Null()}
                                  , {ExamDate.ConvertStr_Empty_To_Null()}

                                  , {ItemCode.ConvertStr_Empty_To_Null()}
                                  , {ExamDivCd.ConvertStr_Empty_To_Null()}
                                  , {ExamCd.ConvertStr_Empty_To_Null()}
                                  , {RefNo.ConvertStr_Empty_To_Null()}
                                  , {RefSeq.ConvertInt_Empty_To_Null()}

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
                                  , {ExamEmpId.ConvertStr_Empty_To_Null()}

                                  , {MachineCode.ConvertStr_Empty_To_Null()}

                                  ,'{WrGlobal.LoginID}'
                                  ,'{WrGlobal.ClientHostName}'
                                  ,GETDATE() )";

            return db.ExecuteQuery(query);
        }

        public bool Ins_ExamResult_TPG()
        {
            db.Edit_Property(this);

            query = $@"INSERT INTO QM_ExamResult(CorpId, ExamNo, ExamSeq, DispSeq, ExamDate
                                               , ItemCode, ExamDivCd, ExamCd, RefNo, RefSeq
                                               , ExamQty, MeasureDivCd, MeasureTime
                                               , X1, X2, X3, X4, X5, X6
                                               , PassDivCd, ActionMemo, Remark, ExamEmpId
                                               , MachineCode, LotNo, PassDate
                                               , CreId, CreIP, CreDt)
                            VALUES ('{WrGlobal.CorpID}'
                                  , {ExamNo.ConvertStr_Empty_To_Null()}
                                  , {ExamSeq.ConvertInt_Empty_To_Null()}
                                  , {DispSeq.ConvertInt_Empty_To_Null()}
                                  , {ExamDate.ConvertStr_Empty_To_Null()}

                                  , {ItemCode.ConvertStr_Empty_To_Null()}
                                  , {ExamDivCd.ConvertStr_Empty_To_Null()}
                                  , {ExamCd.ConvertStr_Empty_To_Null()}
                                  , {RefNo.ConvertStr_Empty_To_Null()}
                                  , {RefSeq.ConvertInt_Empty_To_Null()}

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
                                  , {ExamEmpId.ConvertStr_Empty_To_Null()}

                                  , {MachineCode.ConvertStr_Empty_To_Null()}
                                  , {LotNo.ConvertStr_Empty_To_Null()}
                                  , {PassDate.ConvertStr_Empty_To_Null()}

                                  ,'{WrGlobal.LoginID}'
                                  ,'{WrGlobal.ClientHostName}'
                                  ,GETDATE() )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ExamResult()
        {
            // ExamDivCd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamResult
                          SET X1 = {X1.ConvertStr_Empty_To_Null()}
                            , X2 = {X2.ConvertStr_Empty_To_Null()}
                            , X3 = {X3.ConvertStr_Empty_To_Null()}
                            , X4 = {X4.ConvertStr_Empty_To_Null()}
                            , X5 = {X5.ConvertStr_Empty_To_Null()}
                            , X6 = {X6.ConvertStr_Empty_To_Null()}

                            , ExamDate = {ExamDate}
                            , ExamEmpId = {ExamEmpId.ConvertStr_Empty_To_Null()}
                            , MeasureTime = {MeasureTime.ConvertStr_Empty_To_Null()}

                            , PassDivCd = {PassDivCd.ConvertStr_Empty_To_Null()}
                            , ActionMemo = {ActionMemo.ConvertStr_Empty_To_Null()}
                            , Remark = {Remark.ConvertStr_Empty_To_Null()}

                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()

                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND ExamNo = '{ExamNo}'
                          AND ExamSeq = '{ExamSeq}'";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ExamResult_TPG()
        {
            // ExamDivCd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamResult
                          SET X1 = {X1.ConvertStr_Empty_To_Null()}
                            , X2 = {X2.ConvertStr_Empty_To_Null()}
                            , X3 = {X3.ConvertStr_Empty_To_Null()}
                            , X4 = {X4.ConvertStr_Empty_To_Null()}
                            , X5 = {X5.ConvertStr_Empty_To_Null()}
                            , X6 = {X6.ConvertStr_Empty_To_Null()}
                            , ExamQty = {ExamQty.ConvertNumeric_Empty_To_Null()}

                            , ExamDate = {ExamDate}
                            , ExamEmpId = {ExamEmpId.ConvertStr_Empty_To_Null()}
                            , MeasureTime = {MeasureTime.ConvertStr_Empty_To_Null()}

                            , PassDivCd = {PassDivCd.ConvertStr_Empty_To_Null()}
                            , ActionMemo = {ActionMemo.ConvertStr_Empty_To_Null()}
                            , Remark = {Remark.ConvertStr_Empty_To_Null()}

                            , LotNo = {LotNo.ConvertStr_Empty_To_Null()}
                            , PassDate = {PassDate.ConvertStr_Empty_To_Null()}

                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()

                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND ExamNo = '{ExamNo}'
                          AND ExamSeq = '{ExamSeq}'";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ExamResult_FilePath()
        {
            // ExamDivCd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamResult
                          SET InFilePath = {InFilePath.ConvertStr_Empty_To_Null()}
    
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                            , FileExt = '{FileExt}'

                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND RefNo = '{RefNo}'
                          AND RefSeq = '{RefSeq}'
                          AND ExamDivCd = '{ExamDivCd}' ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ExamResult_FilePath_No()
        {
            // ExamDivCd
            db.Edit_Property(this);

            query = $@"UPDATE QM_ExamResult
                          SET InFilePath = {InFilePath.ConvertStr_Empty_To_Null()}
    
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                            , FileExt = '{FileExt}'

                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND RefNo = '{RefNo}'
                          AND ExamDivCd = '{ExamDivCd}' ";

            return db.ExecuteQuery(query);
        }

        public bool Del_ExamResult()
        {
            query = $@"DELETE QM_ExamResult
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND ExamNo = '{ExamNo}'";

            if (ExamSeq != 0)
                query += $"\n AND ExamSeq = {ExamSeq}";

            return db.ExecuteQuery(query);
        }

        public bool Del_ExamResult_ByRefSeq()
        {
            query = $@"DELETE QM_ExamResult
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND RefNo = '{RefNo}'
                          AND RefSeq = '{RefSeq}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_ExamResult_ByRefNo()
        {
            query = $@"DELETE QM_ExamResult
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND RefNo = '{RefNo}'
                    ";

            if(ExamDivCd != "" && ExamDivCd != null)
            {
                query += $"\nAND ExamDivCd = '{ExamDivCd}'";
			}

            return db.ExecuteQuery(query);
        }

        public bool Del_ExamResult_ByRefNo_ExamDiv()
        {
            query = $@"DELETE QM_ExamResult
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND RefNo = '{RefNo}'
                          AND RefSeq = '{RefSeq}'
                          AND ExamDivCd = '{ExamDivCd}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
