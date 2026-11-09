using DevExpress.XtraEditors;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Common;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.Models.AI
{

	class OC_MergeData
    {
        #region Property

        public string Merge_Id { get; set; }

        public string ProductDate { get; set; }

        public string ProductHour { get; set; }

        public string Lot_No { get; set; }

        public string InspectType { get; set; }

        public string Y_Method { get; set; }

        public string ItemCode { get; set; }

        public string Alias { get; set; }

        public string ItemName { get; set; }

        public string Outlier_Yn { get; set; }

        public string Miss_Yn { get; set; }

        public string InsEmpNo { get; set; }

        public decimal? Y1_Value { get; set; }

        public decimal? Y2_Value { get; set; }

        public decimal? Y3_Value { get; set; }

        public decimal? Y_Min { get; set; }

        public decimal? Y_Max { get; set; }

        public decimal? X1_Value { get; set; }

        public decimal? X2_Value { get; set; }

        public decimal? X3_Value { get; set; }

        public decimal? X4_Value { get; set; }

        public decimal? X5_Value { get; set; }

        public decimal? X6_Value { get; set; }

        public decimal? X7_Value { get; set; }

        public decimal? X8_Value { get; set; }

        public decimal? X9_Value { get; set; }

        public decimal? X10_Value { get; set; }

        public decimal? X11_Value { get; set; }

        public decimal? X12_Value { get; set; }

        public decimal? X13_Value { get; set; }

        public decimal? X14_Value { get; set; }

        public decimal? X15_Value { get; set; }

        public decimal? E1_1 { get; set; }

        public decimal? E1_2 { get; set; }

        public decimal? E2_1 { get; set; }

        public decimal? E2_2 { get; set; }

        public decimal? E3_1 { get; set; }

        public decimal? E3_2 { get; set; }

        public decimal? E4_1 { get; set; }

        public decimal? E4_2 { get; set; }

        public decimal? E5_1 { get; set; }

        public decimal? E5_2 { get; set; }

        public decimal? E6_1 { get; set; }

        public decimal? E6_2 { get; set; }

        public decimal? E7_1 { get; set; }

        public decimal? E7_2 { get; set; }

        public decimal? E8_1 { get; set; }

        public decimal? E8_2 { get; set; }

        public decimal? E9_1 { get; set; }

        public decimal? E9_2 { get; set; }

        public decimal? E10_1 { get; set; }

        public decimal? E10_2 { get; set; }

        public decimal? E11_1 { get; set; }

        public decimal? E11_2 { get; set; }

        public decimal? E12_1 { get; set; }

        public decimal? E12_2 { get; set; }
        #endregion


        Database db = new Database();
        string query;

        public DataTable SelectOC_MergeData_ByCondID(string cond_Id)
        {
            query = $@"SELECT distinct sd.Cond_Id
                            , md.Merge_Id
                            , ProductDate
                            , ProductHour
                            , Lot_No
                            , InspectType

                            , Y_Method
                            , ItemCode
                            , Alias
                            , ItemName
                            , Outlier_Yn
                            , Miss_Yn

                            --공정데이터
                            , Y1_Value, Y2_Value, Y3_Value, Y_Min, Y_Max
                            , X1_Value,  X2_Value,  X3_Value,  X4_Value,  X5_Value
                            , X6_Value,  X7_Value,  X8_Value,  X9_Value,  X10_Value
                            , X11_Value, X12_Value, X13_Value, X14_Value, X15_Value
                            , X16_Value, X17_Value
                            --외부데이터
                            , E1_1,  E1_2,  E2_1,  E2_2,  E3_1,  E3_2
                            , E4_1,  E4_2,  E5_1,  E5_2,  E6_1,  E6_2
                            , E7_1,  E7_2,  E8_1,  E8_2,  E9_1,  E9_2
                            , E10_1, E10_2, E11_1, E11_2, E12_1, E12_2
                            --비전데이터
                            , VS.Fault_Yn, VS.Fault_Div

                        FROM OC_MergeData md
                        JOIN ST_Std_Dataset sd ON sd.Merge_Id = md.Merge_Id
                      LEFT  JOIN (SELECT  Fault_Yn
                                   , Fault_Div
                                   , CONVERT(CHAR, Film_Date, 112) AS Film_Date
                                   , [Label_Id]
                                FROM VS_Inspect WHERE 1=1) VS ON VS.Film_Date = md.ProductDate
                                                                        AND VS.[Label_Id] = md.ItemCode
                         WHERE sd.Cond_Id = '{cond_Id}'
                        ORDER BY ProductDate ASC, ProductHour ASC";

            if (Database.DbName != "AI_JINSUNG" && Database.DbName != "AI_CHANGIL")
            {

                query = $@"SELECT sd.Cond_Id
                            , md.Merge_Id
                            , ProductDate
                            , ProductHour
                            , Lot_No
                            , InspectType

                            , Y_Method
                            , ItemCode
                            , Alias
                            , ItemName
                            , Outlier_Yn
                            , Miss_Yn

                            --공정데이터
                            , Y1_Value, Y2_Value, Y3_Value, Y_Min, Y_Max
                            , X1_Value,  X2_Value,  X3_Value,  X4_Value,  X5_Value
                            , X6_Value,  X7_Value,  X8_Value,  X9_Value,  X10_Value
                            , X11_Value, X12_Value, X13_Value, X14_Value, X15_Value
                            --외부데이터
                            , E1_1,  E1_2,  E2_1,  E2_2,  E3_1,  E3_2
                            , E4_1,  E4_2,  E5_1,  E5_2,  E6_1,  E6_2
                            , E7_1,  E7_2,  E8_1,  E8_2,  E9_1,  E9_2
                            , E10_1, E10_2, E11_1, E11_2, E12_1, E12_2

                        FROM OC_MergeData md
                        JOIN ST_Std_Dataset sd ON sd.Merge_Id = md.Merge_Id
                         AND sd.Cond_Id = '{cond_Id}'
                        ORDER BY ProductDate ASC, ProductHour ASC";
            }

            return db.GetDataSet(query);
        }

        public DataTable SelectOC_MergeData(string procMissYn = "N", string procOutlierYn = "N")
        {
            query = $@" SELECT Merge_Id
                             , ProductDate 
                             , ProductHour
                             , CONCAT(CONVERT(DATE, ProductDate), ' ', STUFF(RIGHT('0000' + ProductHour, 4), 3, 0, ':')) AS ProductTime
                             , Lot_No
                             , InspectType

                             , Y_Method
                             , ItemCode
                             , Alias
                             , ItemName
                             , Outlier_Yn
                             , Miss_Yn

                             --공정데이터
                             , Y1_Value, Y2_Value, Y3_Value, Y_Min, Y_Max
                             , X1_Value,  X2_Value,  X3_Value,  X4_Value,  X5_Value
                             , X6_Value,  X7_Value,  X8_Value,  X9_Value,  X10_Value
                             , X11_Value, X12_Value, X13_Value, X14_Value, X15_Value
                             --외부데이터
                             , E1_1,  E1_2,  E2_1,  E2_2,  E3_1,  E3_2
                             , E4_1,  E4_2,  E5_1,  E5_2,  E6_1,  E6_2
                             , E7_1,  E7_2,  E8_1,  E8_2,  E9_1,  E9_2
                             , E10_1, E10_2, E11_1, E11_2, E12_1, E12_2
						FROM OC_MergeData
                        WHERE CorpId = '{WrGlobal.CorpID}'";

            if (Merge_Id != "" && Merge_Id != null)
            {
                query += $" AND Merge_Id = '{Merge_Id}' ";
            }
            //AND ItemCode = '{ItemCode}' 진성테크로 할 때는 설정 적용

            if (ItemCode != "" && ItemCode != null)
            {
                query += $" AND ItemCode = '{ItemCode}'";
            }

            query += $@" AND Miss_Yn = (CASE WHEN '{procMissYn}' = 'Y' THEN 'N' ELSE Miss_Yn END) 
                         AND Outlier_Yn = (CASE WHEN '{procOutlierYn}' = 'Y' THEN 'N' ELSE Outlier_Yn END) 
                         ORDER BY Merge_Id, ProductDate, ProductHour ";

            return db.GetDataSet(query);
        }

        public DataRowView SelectOC_MergeData_LastID()
        {
            query = $@"SELECT DISTINCT TOP 1
                        	  Merge_Id
                         FROM OC_MergeData
                     ORDER BY Merge_Id DESC";

            return db.GetDataRecord(query);
        }

        public DataRowView SelectOC_MergeData_XBarChartInfo(string merge_Id, string itemCode, string inspectType, float USL, float LSL, string missYN = "", string outLier = "")
        {
            query = $@"DECLARE @USL		FLOAT	= {USL},
                        	   @LSL		FLOAT	= {LSL}
                         
                         SELECT MIN(X_Bar) AS [X-Bar_Min],
                         	    MAX(X_Bar) AS [X-Bar_Max],
                         	    AVG(X_Bar) AS [X-Bar_CL],
                         	    --표준편차 :√(합((평균 - X_Bar)^2) / (데이터 수 - 1))
                         	    ISNULL(STDEV(X_Bar), 0) AS[X-Bar_표준편차],

                         	    --[X-Bar_LCL] = CL - (X_Bar 관리계수 * R-CL)
                         	    AVG(X_Bar) - (1.023 * AVG([RANGE])) AS [X-Bar_LCL],
                         	    --[X-Bar_UCL] = CL + (X_Bar 관리계수 * R-CL)
                         	    AVG(X_Bar) + (1.023 * AVG([RANGE])) AS [X-Bar_UCL],

                         	    --Cp = (규격상한 - 규격하한) / (6 * 표준편차), 
                         	    ABS(@USL - @LSL) / (6 * STDEV(X_Bar)) AS Cp,
                         	    --K(치우침계수) = | CL - ((규격상한 + 규격하한) / 2) | * ((규격상한 - 규격하한) / 2)
								CASE WHEN @LSL <> 0 AND @USL <> 0
										THEN ABS(AVG(X_Bar) - ((@USL + @LSL) / 2)) / ((@USL + @LSL) / 2)
									ELSE NULL
								END AS K,

	                            --Cpk = (1 - 치우침계수) * Cp, 규격상한, 하한이 둘다 있을 때 치우침계수가 반영된 Cp
								CASE WHEN @LSL <> 0 AND @USL <> 0
										THEN (1 - ABS(AVG(X_Bar) - ((@USL + @LSL) / 2)) / ((@USL + @LSL) / 2))
										    * ((@USL - @LSL) / (6 * STDEV(X_Bar)))
									ELSE NULL
								END
                                AS [Cpk],
	                            --Cpl = (CL - 규격하한) / (3 * 표준편차), 규격하한만 존재할 때 규격 하한에 가까운 정도
	                            CASE WHEN @LSL <> 0 AND @USL <> 0
                                          THEN (AVG(X_Bar) - @LSL) / (3 * STDEV(X_Bar))
	                            ELSE NULL
	                            END AS [Cpl],
	                            --Cpu = (규격상한 - CL) / (3 * 표준편차), 규격상한만 존재할 때 규격 상한에 가까운 정도
	                            CASE WHEN @LSL <> 0 AND @USL <> 0
                                          THEN (@USL - AVG(X_Bar)) / (3 * STDEV(X_Bar))
	                            ELSE NULL
	                            END AS [Cpu]

                              , MAX(Seq) AS [Data_Cnt]

                           FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS Seq,
                                        Lot_No, InspectType, ItemCode,
                         			    AVG(Y_Value) AS X_Bar,
                         			    MAX(Y_Value) - MIN(Y_Value) AS [Range]

                         	       FROM OC_MergeData
                         	    UNPIVOT(Y_Value
                         	    	    FOR Y_Cnt_Nm
                         	    	    IN ([Y1_Value], [Y2_Value], [Y3_Value])
                         	            ) AS unPvt

                         	      WHERE Merge_Id = '{merge_Id}'
                         	        AND ItemCode = '{itemCode}'
                                    AND InspectType = '{inspectType}'";
            if (missYN == "Y")
            {
                query += $@" AND Miss_Yn = 'N'";
            }
            if (outLier == "Y")
            {
                query += $@"  AND Outlier_Yn = 'N'";
            }
            query += $@" GROUP BY Lot_No, InspectType, ItemCode
                               ) T
                      GROUP BY ItemCode";

            return db.GetDataRecord(query);
        }

        public DataRowView SelectOC_MergeData_RChartInfo(string merge_Id, string itemCode, string inspectType, string missYN = "", string outLier = "")
        {
            query = $@"SELECT MAX([Range]) AS [R_Max],
	                          MIN([Range]) AS [R_Min],
                              AVG([Range]) AS [R_CL],
                           	  ISNULL(STDEV([Range]), 0) AS [R_표준편차],
                           	  --R-LCL = R-CL - (R_Bar 관리계수 * R_CL)
                           	  CASE WHEN AVG([Range]) - (1.023 * AVG([Range])) < 0 THEN 0
								   ELSE AVG([Range]) - (1.023 * AVG([Range]))
							  END AS [R_LCL],	
                           	  --R_UCL = R-CL + (R_Bar 관리계수 * R-CL)
                           	  AVG([Range]) + (1.023 * AVG([Range])) AS [R_UCL]

                            , MAX(Seq) AS [Data_Cnt]

                         FROM (SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS Seq,
                                      Lot_No, InspectType, ItemCode,
                           			  AVG(Y_Value) AS X_Bar,
                           			  MAX(Y_Value) - MIN(Y_Value) AS [Range]

                           	     FROM OC_MergeData
                           	  UNPIVOT(Y_Value
                           		      FOR Y_Cnt_Nm
                           		      IN ([Y1_Value], [Y2_Value], [Y3_Value])
                           	          ) AS unPvt

                                WHERE Merge_Id = '{merge_Id}'
                           	      AND ItemCode = '{itemCode}'
                                  AND InspectType = '{inspectType}'";
            if (missYN == "Y")
            {
                query += $@"AND Miss_Yn = 'N'";
            }
            if (outLier == "Y")
            {
                query += $@"AND Outlier_Yn = 'N'";
            }
            query += $@"GROUP BY Lot_No, InspectType, ItemCode
                             ) T
                    GROUP BY ItemCode";

            return db.GetDataRecord(query);
        }

        public DataTable SelectOC_MergeData_ChartPointData(string merge_Id, string itemCode, string inspectType, string missYN = "", string outLier = "")
        {
            query = $@"SELECT Lot_No, InspectType, ItemCode,
                    		  AVG(Y_Value) AS X_Bar,
                    		  MAX(Y_Value) - MIN(Y_Value) AS [Range]
                         FROM OC_MergeData
                      UNPIVOT(Y_Value
                    	      FOR Y_Cnt_Nm
                    	      IN ([Y1_Value], [Y2_Value], [Y3_Value])
                              ) AS unPvt
                        WHERE Merge_Id = '{merge_Id}'
                          AND ItemCode = '{itemCode}'
                          AND InspectType = '{inspectType}'";
            if (missYN == "Y")
            {
                query += $@"AND Miss_Yn = 'N'";
            }
            if (outLier == "Y")
            {
                query += $@"AND Outlier_Yn = 'N'";
            }

            query += $@"GROUP BY Lot_No, InspectType, ItemCode";

            return db.GetDataSet(query);
        }

        public void Insert_MergeData(string cond_Id, string ex_Id)
        {
            query = $@"INSERT INTO OC_MergeData( CorpId
                                               , Merge_Id
                                               , ProductDate
                                               , ProductHour
                                               , Lot_No

                                               , InspectType
                                               , Y_Method
                                               , ItemCode
                                               , Alias
                                               , ItemName

                                               , Outlier_Yn
                                               , Miss_Yn

                                               , Y1_Value, Y2_Value, Y3_Value, Y_Min , Y_Max
                                               , X1_Value,  X2_Value,  X3_Value,  X4_Value,  X5_Value
                                               , X6_Value,  X7_Value,  X8_Value,  X9_Value,  X10_Value
                                               , X11_Value, X12_Value, X13_Value, X14_Value, X15_Value
                                               , X16_Value, X17_Value
)
                             SELECT CorpId
                                 , dbo.Create_MergeId() --자동채번
                                 , ProductDate
                                 , ProductTime
                                 , Lot_No

                                 , InspectType
                                 , Y_Method
                                 , ItemCode
                                 , Alias
                                 , ItemName

                                 , 'N'  --Outlier_Yn
                                 , 'N'  --Miss_Yn

                                 --공정데이터
                                 , Y1_Value, Y2_Value, Y3_Value, Y_Min , Y_Max
                                 , X1_Value,  X2_Value,  X3_Value,  X4_Value,  X5_Value
                                 , X6_Value,  X7_Value,  X8_Value,  X9_Value,  X10_Value
                                 , X11_Value, X12_Value, X13_Value, X14_Value, X15_Value
                                 , X16_Value, X17_Value
                                 
						   FROM OC_ProcCond 
             WHERE Cond_Id = '{cond_Id}'";

            db.ExecuteQuery(query);
        }

        public DataTable SelectOC_MergeData_MergeIDList()
        {
            query = $@"SELECT DISTINCT md.Merge_Id 병합ID, sd.Remark
                        FROM OC_MergeData md
						LEFT JOIN ST_Std_Dataset  sd on md.Merge_Id = sd.Merge_Id
						ORDER BY md.Merge_Id ASC";

            return db.GetDataSet(query);
        }

        public void GetMergeId(Object sender)
        {
            Database sqlDb = null;
            string query = string.Empty;

            try
            {
                sqlDb = new Database();

                query = $@" SELECT DISTINCT md.Merge_Id AS 병합ID
                                          , sd.Cond_Id AS 공정조건ID
                                          , sd.Ex_Id AS 외부조건ID
                                          , CONVERT(DATE, sd.GetDate_From) AS 수집시작일자
                                          , CONVERT(DATE, sd.GetDate_To) AS 수집종료일자
                                          , sd. Remark
                            FROM OC_MergeData md
                            LEFT JOIN ST_Std_Dataset sd ON md.Merge_Id = sd.Merge_Id
                            WHERE md.CorpId = '{WrGlobal.CorpID}' 
                            ORDER BY md.Merge_Id ";

                if (sender.GetType().Equals(typeof(SearchLookUpEdit)))
                {
                    SearchLookUpEdit searchLook = sender as SearchLookUpEdit;

                    searchLook.Properties.DataSource = sqlDb.GetDataView("M", query);
                    searchLook.Properties.ValueMember = "병합ID";
                    searchLook.Properties.DisplayMember = "병합ID";

                    searchLook.Properties.PopulateViewColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlDb.DBClose();
                sqlDb = null;
            }
        }

        public DataTable SelectOC_MergeData_ItemList(string merge_Id = null)
        {
            query = @"SELECT DISTINCT ItemCode 코드, Alias 품번, ItemName 품명
                        FROM OC_MergeData ";

            if (merge_Id != null)
            {
                query += $"\n WHERE Merge_Id = '{merge_Id}'";
            }
            query += $"\n ORDER BY ItemCode ASC";

            return db.GetDataSet(query);
        }

        public DataTable SelectOC_MergeData_InspectTypeList(string merge_Id = null)
        {
            query = @"SELECT DISTINCT InspectType
                        FROM OC_MergeData";

            if (merge_Id != null)
            {
                query += $"\n WHERE Merge_Id = '{merge_Id}'";
            }
            query += $"\n ORDER BY InspectType ASC";

            return db.GetDataSet(query);
        }

        public int SelectOC_MergeData_Count(string merge_Id)
        {
            query = $@" SELECT COUNT(*)
                        FROM OC_MergeData
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND Merge_Id = '{merge_Id}'";

            DataTable dt = db.GetDataSet(query);

            return dt.Rows[0][0].ConvertInt_Null_To_Zero();
        }

        public DataRowView SelectOC_MergeData_PreTreatmentYn(string merge_Id)
        {
            query = $@"SELECT COUNT(CASE WHEN Outlier_Yn = 'Y' THEN 1 END) AS Outlier_Yn
						    , COUNT(CASE WHEN Miss_Yn = 'Y' THEN 1 END) AS Miss_Yn
					     FROM OC_MergeData
					    WHERE Merge_Id = '{merge_Id}'";

            return db.GetDataRecord(query);
        }

        public DataTable SelectOC_MergeDate_MonthList()
        {
            query = $@" SELECT DISTINCT SUBSTRING(CONVERT(VARCHAR, CONVERT(DATETIME, ProductDate), 23), 1, 7) AS '년월'
                        FROM  OC_MergeData 
                        WHERE Merge_Id = '{Merge_Id}' 
                        ORDER BY '년월' ";

            return db.GetDataSet(query);
        }

        public DataTable SelectOC_MergeData_XValue(string month)
        {
            //월별 공정조건 데이터 조회
            query = $@" SELECT * 
                        FROM
                        (
                            SELECT ROW_NUMBER() OVER (PARTITION BY ProductDate, ProductHour ORDER BY ProductDate, ProductHour DESC) AS RowNum
                        	, CONCAT(CONVERT(DATE, ProductDate), ' ', STUFF(RIGHT('0000' + ProductHour, 4), 3, 0, ':')) AS ProductTime, ItemCode
                        	, X1_Value 
                        	, X2_Value
                        	, X3_Value
                        	, X4_Value
                        	, X5_Value
                        	, X6_Value
                        	, X7_Value
                        	, X8_Value
                        	, X9_Value
                        	, X10_Value
                        	, X11_Value
                        	, X12_Value
                        	, X13_Value
                        	, X14_Value
                        	, X15_Value
                        	FROM OC_MergeData 
                        	WHERE Merge_Id = '{Merge_Id}' ) AS OC_MergeData
                        WHERE RowNum = 1 
                        AND ProductTime LIKE '{month}%'";
            if (ItemCode != "" && ItemCode != null)
            {
                query += $" AND ItemCode = '{ItemCode}'";
            }
            query += $@" ORDER BY ProductTime ";

            return db.GetDataSet(query);
        }

        public DataTable Exec_GetMissingData(string merge_Id)
        {
            query = $@" EXEC GetMissingData '{WrGlobal.CorpID}', '{merge_Id}' ";

            return db.GetDataSet(query);
        }

        public DataTable Exec_MissingDataProcessing(string merge_Id)
        {
            query = $@" EXEC MissingDataProcessing '{WrGlobal.CorpID}', '{merge_Id}' ";

            return db.GetDataSet(query);
        }

        public DataTable Exec_GetOutlierData(string merge_Id)
        {
            query = $@" EXEC GetOutlierData '{WrGlobal.CorpID}', '{merge_Id}' ";

            return db.GetDataSet(query);
        }

        public DataTable Exec_OutlierDataProcessing(string merge_Id)
        {
            query = $@" EXEC OutlierDataProcessing '{WrGlobal.CorpID}', '{merge_Id}' ";

            return db.GetDataSet(query);
        }

        public void Make_Histogram(string Merge_Id, string Outlier_Yn, string Miss_Yn)
        {
            query = $@"EXEC AIBest_Histo_Graph_Python '{Merge_Id}', '{Outlier_Yn}', '{Miss_Yn}'";

            db.ExecuteQuery(query);
        }

        public void Make_Heatmap(string Merge_Id, string Outlier_Yn, string Miss_Yn)
        {
            query = $@"EXEC AIBest_HeatMap_Python '{Merge_Id}', '{Outlier_Yn}', '{Miss_Yn}'";

            db.ExecuteQuery(query);
        }

        public void DecisionTree_Learning(string Merge_Id, string InsEmpNo)
        {
            query = $@"EXEC Insert_AbsData_LearningResult '{Merge_Id}', '{InsEmpNo}'";

            db.ExecuteQuery(query);
        }

        public DataTable DecisionTree_Learning_View(string Merge_id, string Outlier, string MissingValue)
        {
            query = $@"EXEC AIBest_DecisionTree_OptCondition_Python '{Merge_id}', '{Outlier}', '{MissingValue}'";

            return db.GetDataSet(query);
        }

        public DataTable SELECT_MinMaxDate()
        {
            query = $@"SELECT MIN(ProductDate) as Min, Max(ProductDate) as Max FROM OC_MergeData WHERE Merge_Id = '{Merge_Id}'";

            return db.GetDataSet(query);
        }

        public DataTable SampleData(string min, string max)
        {
            query = $@"SELECT TOP 30 CAST(Y1_Value as decimal(10,2)) as Y1_Value, CAST(Y2_Value as decimal(10,2)) as Y2_Value, CAST(Y3_Value as decimal(10,2)) as Y3_Value
                                 FROM OC_MergeData 
                                 WHERE Outlier_Yn = 'N' AND Miss_Yn = 'N' AND ProductDate BETWEEN '{min}' AND '{max}' AND ItemCode = '{ItemCode}'";

            return db.GetDataSet(query);
        }

        public void DecisionTree_Result_Insert()
        {
            query = $@"EXEC AIBest_DecisionTree_Result_Insert '{WrGlobal.CorpID}', '{Merge_Id}', '{InsEmpNo}', '{Outlier_Yn}', '{Miss_Yn}'";

            db.ExecuteQuery(query);
        }

        public Bitmap GetBitmap_Heatmap_ver()
        {
            Bitmap bmp = null;
            query = $@"EXEC AIBest_HeatMap_Python '{Merge_Id}','{Outlier_Yn}','{Miss_Yn}'";

            try
            {
                DataTable heatmap = db.GetDataSet(query);
                if (heatmap.Rows.Count > 0)
                {
                    byte[] image = heatmap.Rows[0][0] as byte[];
                    if (image == null)
                    {
                        return bmp;
                    }
                    MemoryStream ms = new MemoryStream(image, 0, image.Length);
                    bmp = new Bitmap(ms);
                }
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }

        public DataView Heatmap()
        {
            query = $@"EXEC AI_Correl '{Merge_Id}'";
            if (Database.DbName == "AI_JINSUNGTECH" || Database.DbName == "AI_ORIENT" || Database.DbName == "AI_SAMGWANG") query = $@"EXEC AI_Correl_Opt '{Merge_Id}'";
            return db.GetDataView("54", query);
        }
    }
}
