using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SmartMES.Common;

namespace SmartMES.Models.AI
{
    class OC_AbsData
    {
        #region Property
        public string CorpCd { get; set; }
        public string Merge_Id { get; set; }
        public string Abs_Id { get; set; }
        public string Abs_Nm { get; set; }
        public string ProductDate { get; set; }
        public string ProductHour { get; set; }
        public string Lot_No { get; set; }
        public string InspectType { get; set; }
        public string Y_Method { get; set; }
        public string ItemCode { get; set; }
        public string Alias { get; set; }
        public string ItemName { get; set; }
        public string Result { get; set; }
        public byte[] Image { get; set; }
        public string CreId { get; set; }
        public string CreDt { get; set; }
        public string Outlier_Yn { get; set; }
        public string Miss_Yn { get; set; }
        public float Y1_Value { get; set; }
        public float Y2_Value { get; set; }
        public float Y3_Value { get; set; }
        public float Y_Min { get; set; }
        public float Y_Max { get; set; }
        public float X1_Value { get; set; }
        public float X2_Value { get; set; }
        public float X3_Value { get; set; }
        public float X4_Value { get; set; }
        public float X5_Value { get; set; }
        public float X6_Value { get; set; }
        public float X7_Value { get; set; }
        public float X8_Value { get; set; }
        public float X9_Value { get; set; }
        public float X10_Value { get; set; }
        public float X11_Value { get; set; }
        public float X12_Value { get; set; }
        public float X13_Value { get; set; }
        public float X14_Value { get; set; }
        public float X15_Value { get; set; }
        public float E1_1 { get; set; }
        public float E1_2 { get; set; }
        public float E2_1 { get; set; }
        public float E2_2 { get; set; }
        public float E3_1 { get; set; }
        public float E3_2 { get; set; }
        public float E4_1 { get; set; }
        public float E4_2 { get; set; }
        public float E5_1 { get; set; }
        public float E5_2 { get; set; }
        public float E6_1 { get; set; }
        public float E6_2 { get; set; }
        public float E7_1 { get; set; }
        public float E7_2 { get; set; }
        public float E8_1 { get; set; }
        public float E8_2 { get; set; }
        public float E9_1 { get; set; }
        public float E9_2 { get; set; }
        public float E10_1 { get; set; }
        public float E10_2 { get; set; }
        public float E11_1 { get; set; }
        public float E11_2 { get; set; }
        public float E12_1 { get; set; }
        public float E12_2 { get; set; }
        public string Remark { get; set; }

		#endregion

		Database db = new Database();
        string query;

        public DataTable Select_AbsId_List()
        {
            query = $@"SELECT ad.Abs_Id AS 추출ID, MIN(ad.ProductDate) AS From일자, MAX(ad.ProductDate) AS To일자, r.Record_Yn AS 처리여부
                         FROM OC_AbsData ad
						 INNER JOIN OC_AbsData_Record r ON ad.Abs_Id = r.Abs_Id
                        GROUP BY ad.Abs_Id, r.Record_Yn
                        ORDER BY ad.Abs_Id ";

            return db.GetDataSet(query);
        }

        public DataTable Select_ItemCode_List()
        {
            query = $@"SELECT DISTINCT ItemCode 품목코드, ItemName 품명, Alias 품번
                         FROM OC_AbsData 
                        WHERE 1=1 ";
            
            if (Abs_Id != "" && Abs_Id != null)
            {
                query += $" AND Abs_Id = '{Abs_Id}'";
            }

            query += " ORDER BY ItemCode ";

            return db.GetDataSet(query);
        }
        public DataTable Select_InspectionItem_List()
        {
            query = $@"
                         SELECT 
                           REPLACE(ColValue, 'Nm', 'Value') as ColValue
                         , ColName
                         FROM
                         (
	                         SELECT 
	                               Y_Nm + '1' AS Y1_Nm
	                            ,  Y_Nm + '2' AS Y2_Nm
	                            ,  Y_Nm + '3' AS Y3_Nm
	                         FROM ST_OptCond_Init
                         ) AS Result
                         UNPIVOT
                         (ColName FOR ColValue in ([Y1_Nm], [Y2_Nm], [Y3_Nm])) AS unpv
                         
                         UNION
                         
                         SELECT 
                           REPLACE(ColValue, 'Nm', 'Value') as ColValue
                         , ColName
                         FROM
                         (
	                         SELECT 
	                            X1_Nm , X2_Nm , X3_Nm , X4_Nm , X5_Nm , X6_Nm , X7_Nm , X8_Nm , X9_Nm , X10_Nm, X11_Nm, X12_Nm, X13_Nm, X14_Nm, X15_Nm
	                         FROM ST_OptCond_Init
                         ) AS Result
                         UNPIVOT
                         (ColName FOR ColValue in ([X1_Nm], [X2_Nm], [X3_Nm], [X4_Nm], [X5_Nm], [X6_Nm], [X7_Nm], [X8_Nm], [X9_Nm], [X10_Nm], [X11_Nm], [X12_Nm], [X13_Nm], [X14_Nm], [X15_Nm])) AS unpv
                       ";

            return db.GetDataSet(query);
        }

        public DataTable Select_InspectionItem_MinMax(string Value)
		{
            query = $@"
                        declare @value varchar(20) = '{Value}'
                        , @valueMin varchar(20)
                        , @valueMax varchar(20)
                        , @query varchar(500)

                        set @value = replace(@value, '_Value', '')
                        set @valueMin = @value + '_Min'
                        set @valueMax = @value + '_Max'

                        SET @query = 'SELECT  [' + @valueMin + '] AS Min, [' +  @valueMax + '] AS Max FROM ST_OptCond_Init'

                        exec (@query)

                        ";
            return db.GetDataSet(query);
        }

        public DataTable Select_InspectMgnt()
		{
            query = $@"
                        SELECT ItemCode, Y_Nominal, Y_Max, Y_Min, Y_DangerMax, Y_DangerMin
                        FROM ST_Item_Inspect_Mgnt
                        WHERE ItemCode = '{ItemCode}'
";


            return db.GetDataSet(query);
        }

        public DataTable Select_AbsId_List(string MergeId)
        {
            query = $@"SELECT DISTINCT Abs_Id 추출ID,/* CONVERT(Date, lr.CreDt) AS 추출일자,*/ sd.Remark
                         FROM OC_AbsData_LearningResult lr
						 LEFT JOIN  ST_Std_Dataset  sd on lr.Merge_Id = sd.Merge_Id";

            if (MergeId != "") {
                query += $"\n WHERE lr.Merge_Id = '{MergeId}'";
            }

            query += "\n ORDER BY lr.Abs_Id ASC";

            return db.GetDataSet(query);
        }

        public DataTable Select_MergeId_List(string absId = null)
        {
            query = $@"SELECT DISTINCT
		                      Merge_Id 병합ID
                         FROM OC_AbsData_LearningResult";
            if (absId != null)
            {
                query += "\n";
                query += $@"WHERE Abs_Id = '{absId}'";
            }

            return db.GetDataSet(query);
        }

        public DataTable Select_Item_List(string abs_Id, string merge_Id)
        {
            //2023-03-15 러닝 결과가 아닌 산출 결과 검색
            query = $@"SELECT DISTINCT
                        	  ItemCode 코드, ItemName 품명
                         FROM OC_AbsData
                        WHERE Abs_Id = '{abs_Id}'
                          --AND Merge_Id = '{merge_Id}'";

            return db.GetDataSet(query);
        }

        public DataTable SelectOC_AbsData_LearningResult_Response(string AbsId, string ItemCode)
        {
            query = $@"SELECT DISTINCT SUBSTRING(Result, CHARINDEX('response:',Result) + 10, CHARINDEX('|',Result) - CHARINDEX('response:',Result) - 10) Response
                            --, CONVERT(FLOAT, SUBSTRING(Result, CHARINDEX('response:',Result) + 10, CHARINDEX('|',Result) - CHARINDEX('response:',Result) - 10)) Response
                         FROM OC_AbsData_LearningResult
                        WHERE Abs_Id = '{AbsId}'
                          AND ItemCode = '{ItemCode}'";

            //query = $@"SELECT DISTINCT CONVERT(FLOAT, SUBSTRING(Result, CHARINDEX('도막 두께',Result) + 6, CHARINDEX('을/를',Result) - CHARINDEX('response:',Result) - 7)) Response
            //                --, CONVERT(FLOAT, SUBSTRING(Result, CHARINDEX('response:',Result) + 10, CHARINDEX('|',Result) - CHARINDEX('response:',Result) - 10)) Response
            //             FROM OC_AbsData_LearningResult
            //            WHERE Abs_Id = '{AbsId}'
            //              AND ItemCode = '{ItemCode}'";

            return db.GetDataSet(query);
        }

        public DataTable Select_AbsData_LearningResult_Result(string AbsId, string ItemCode, string response)
        {
            query = $@"SELECT CONVERT(FLOAT, SUBSTRING(Result, CHARINDEX('response:',Result) + 10, CHARINDEX('|',Result) - CHARINDEX('response:',Result) - 10)) AS Response
                         FROM OC_AbsData_LearningResult
                        WHERE Abs_Id = '{AbsId}'
                          AND ItemCode = '{ItemCode}'
                     ORDER BY ABS(CONVERT(FLOAT, SUBSTRING(Result, CHARINDEX('response:',Result) + 10, CHARINDEX('|',Result) - CHARINDEX('response:',Result) - 10)) - CONVERT(FLOAT, {response})) asc";

            //query = $@"SELECT CONVERT(FLOAT, SUBSTRING(Result, CHARINDEX('도막 두께',Result) + 6, CHARINDEX('을/를',Result) - CHARINDEX('response:',Result) - 7)) AS Response
            //             FROM OC_AbsData_LearningResult
            //            WHERE Abs_Id = '{AbsId}'
            //              AND ItemCode = '{ItemCode}'
            //         ORDER BY ABS(CONVERT(FLOAT, SUBSTRING(Result, CHARINDEX('도막 두께',Result) + 6, CHARINDEX('을/를',Result) - CHARINDEX('response:',Result) - 7)) - CONVERT(FLOAT, {response})) asc";
            
            DataTable dt = db.GetDataSet(query);
            string closest = "";

            if (dt.Rows.Count > 0) closest = db.GetDataSet(query).Rows[0][0].ToString();

            query = $@"SELECT Result
                         FROM OC_AbsData_LearningResult
                        WHERE Abs_Id = '{AbsId}'
                          AND ItemCode = '{ItemCode}'
                          AND Result LIKE '%{closest}%'";

            return db.GetDataSet(query);
        }

        public DataTable Get_Conditions()
        {
            query = $@"SELECT ItemCode, Result
                         FROM OC_AbsData_LearningResult
                        WHERE Merge_Id = '{Merge_Id}'
                          AND Abs_Id = '{Abs_Id}'";

            return db.GetDataSet(query);
        }

        public DataTable Create_F1_Score()
        {
            query = $@"EXEC AI_Ceate_F1_Score '{Abs_Id}'";

            return db.GetDataSet(query);
        }

        public DataTable Select_OC_AbsData(string AbsId)
        {
            query = $@"SELECT 
                       Abs_Id ,Abs_Nm ,ProductDate ,ProductHour ,Lot_No ,InspectType
                      ,Y_Method ,ItemCode ,Alias ,ItemName ,Outlier_Yn
                      ,Miss_Yn ,Y1_Value ,Y2_Value ,Y3_Value ,Y_Min ,Y_Max ,X1_Value
                      ,X2_Value ,X3_Value ,X4_Value ,X5_Value ,X6_Value ,X7_Value
                      ,X8_Value ,X9_Value ,X10_Value ,X11_Value ,X12_Value ,X13_Value ,X14_Value
                      ,X15_Value ,E1_1 ,E1_2 ,E2_1 ,E2_2 ,E3_1 ,E3_2 ,E4_1 ,E4_2
                      ,E5_1 ,E5_2 ,E6_1 ,E6_2 ,E7_1 ,E7_2 ,E8_1 ,E8_2 ,E9_1 ,E9_2 ,E10_1
                      ,E10_2 ,E11_1 ,E11_2 ,E12_1 ,E12_2
                     FROM OC_AbsData
                     WHERE Abs_Id = '{AbsId}'"; 

            return db.GetDataSet(query);
        }

        public void Insert_OC_AbsData(string MergeId, string AbsId, string ItemCode, string Outlier, string MissingValue, string Conditions)
        {
            //query = $@"SELECT CorpId, Merge_Id, ProductDate, ProductHour,
            //                  Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
            //                  Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
            //                  Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
            //                  X6_Value, X7_Value, X8_Value, X9_Value, E1_1, E1_2, E2_1,
            //                  E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
            //                  E7_1, E7_2, E8_1, E8_2
            //             FROM OC_MergeData
            //            WHERE Merge_Id = '{MergeId}'
            //              AND ItemCode = '{ItemCode}'
            //              AND {Conditions}";
            //
            //DataTable temp_table = db.GetDataSet(query);

            if (Outlier == "Y" && MissingValue == "Y")
            {
                string Outlier_YN = "N";
                string Miss_Yn = "N";
                query = $@"INSERT INTO OC_AbsData (CorpId, Abs_Id, ProductDate, ProductHour,
                                               Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
                                               Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
                                               Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
                                               X6_Value, X7_Value, X8_Value, X9_Value, X10_Value, X11_Value, 
                                               X12_Value, X13_Value, X14_Value, X15_Value, X16_Value, X17_Value, 
                                               E1_1, E1_2, E2_1, E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
                                               E7_1, E7_2, E8_1, E8_2)
		                    SELECT CorpId, '{AbsId}',ProductDate, ProductHour,
                                   Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
                                   Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
                                   Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
                                   X6_Value, X7_Value, X8_Value, X9_Value, X10_Value, X11_Value, 
                                   X12_Value, X13_Value, X14_Value, X15_Value, X16_Value, X17_Value, 
                                   E1_1, E1_2, E2_1,
                                   E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
                                   E7_1, E7_2, E8_1, E8_2
		                 FROM OC_MergeData
		                WHERE Merge_Id = '{MergeId}'
                          AND Outlier_Yn = '{Outlier_YN}'
                          AND Miss_Yn = '{Miss_Yn}'
                          AND ItemCode = '{ItemCode}'
                          AND {Conditions}";

                db.ExecuteQuery(query);
            }
            else if(Outlier == "Y" && MissingValue == "N")
            {
                string Outlier_YN = "N";
                query = $@"INSERT INTO OC_AbsData (CorpId, Abs_Id, ProductDate, ProductHour,
                                               Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
                                               Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
                                               Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
                                               X6_Value, X7_Value, X8_Value, X9_Value, X10_Value, X11_Value, 
                                               X12_Value, X13_Value, X14_Value, X15_Value, X16_Value, X17_Value, 
                                               E1_1, E1_2, E2_1, E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
                                               E7_1, E7_2, E8_1, E8_2)
		                    SELECT CorpId, '{AbsId}',ProductDate, ProductHour,
                                   Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
                                   Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
                                   Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
                                   X6_Value, X7_Value, X8_Value, X9_Value, X10_Value, X11_Value, 
                                   X12_Value, X13_Value, X14_Value, X15_Value, X16_Value, X17_Value, 
                                   E1_1, E1_2, E2_1,
                                   E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
                                   E7_1, E7_2, E8_1, E8_2

		                 FROM OC_MergeData
		                WHERE Merge_Id = '{MergeId}'
                          AND Outlier_Yn = '{Outlier_YN}'
                          AND ItemCode = '{ItemCode}'
                          AND {Conditions}";

                db.ExecuteQuery(query);
            }
            else if(Outlier == "N" && MissingValue == "Y")
            {
                string Miss_Yn = "N";
                query = $@"INSERT INTO OC_AbsData (CorpId, Abs_Id, ProductDate, ProductHour,
                                               Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
                                               Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
                                               Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
                                               X6_Value, X7_Value, X8_Value, X9_Value, X10_Value, X11_Value, 
                                               X12_Value, X13_Value, X14_Value, X15_Value, X16_Value, X17_Value, 
                                               E1_1, E1_2, E2_1, E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
                                               E7_1, E7_2, E8_1, E8_2)
		                    SELECT CorpId, '{AbsId}',ProductDate, ProductHour,
                                   Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
                                   Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
                                   Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
                                   X6_Value, X7_Value, X8_Value, X9_Value, X10_Value, X11_Value, 
                                   X12_Value, X13_Value, X14_Value, X15_Value, X16_Value, X17_Value, 
                                   E1_1, E1_2, E2_1,
                                   E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
                                   E7_1, E7_2, E8_1, E8_2

		                 FROM OC_MergeData
		                WHERE Merge_Id = '{MergeId}'
                          AND Miss_Yn = '{Miss_Yn}'
                          AND ItemCode = '{ItemCode}'
                          AND {Conditions}";

                db.ExecuteQuery(query);
            }
            else if(Outlier == "N" && MissingValue == "N")
            {
                query = $@"INSERT INTO OC_AbsData (CorpId, Abs_Id, ProductDate, ProductHour,
                                               Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
                                               Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
                                               Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
                                               X6_Value, X7_Value, X8_Value, X9_Value, X10_Value, X11_Value, 
                                               X12_Value, X13_Value, X14_Value, X15_Value, X16_Value, X17_Value, 
                                               E1_1, E1_2, E2_1, E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
                                               E7_1, E7_2, E8_1, E8_2)
		                    SELECT CorpId, '{AbsId}',ProductDate, ProductHour,
                                   Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
                                   Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
                                   Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
                                   X6_Value, X7_Value, X8_Value, X9_Value, X10_Value, X11_Value, 
                                   X12_Value, X13_Value, X14_Value, X15_Value, X16_Value, X17_Value, 
                                   E1_1, E1_2, E2_1,
                                   E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
                                   E7_1, E7_2, E8_1, E8_2
		                 FROM OC_MergeData
		                WHERE Merge_Id = '{MergeId}'
                          AND ItemCode = '{ItemCode}'
                          AND {Conditions}";

                db.ExecuteQuery(query);
            }
            

            //query = $@"INSERT INTO OC_AbsData (CorpId, Abs_Id, ProductDate, ProductHour,
            //                                   Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
            //                                   Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
            //                                   Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
            //                                   X6_Value, X7_Value, X8_Value, X9_Value, E1_1, E1_2, E2_1,
            //                                   E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
            //                                   E7_1, E7_2, E8_1, E8_2)
		    //                SELECT CorpId, '{AbsId}',ProductDate, ProductHour,
            //                       Lot_No, InspectType, Y_Method, ItemCode, Alias, ItemName, 
            //                       Outlier_Yn, Miss_Yn, Y1_Value, Y2_Value, Y3_Value, Y_Min, 
            //                       Y_Max, X1_Value, X2_Value, X3_Value, X4_Value, X5_Value,
            //                       X6_Value, X7_Value, X8_Value, X9_Value, E1_1, E1_2, E2_1,
            //                       E2_2, E3_1, E3_2, E4_1, E4_2, E5_1, E5_2, E6_1, E6_2, 
            //                       E7_1, E7_2, E8_1, E8_2
		    //             FROM OC_MergeData
		    //            WHERE Merge_Id = '{MergeId}'
            //              AND Outlier_Yn = '{Outlier}'
            //              AND Miss_Yn = '{MissingValue}'
            //              AND ItemCode = '{ItemCode}'
            //              AND {Conditions}";
        }

        public DataTable Get_Abs_data()
        {
            query = $@"SELECT *
                         FROM OC_AbsData
                        WHERE Abs_Id = '{Abs_Id}'
                          AND ItemCode = '{ItemCode}'";

            return db.GetDataSet(query);
        }

        public DataTable Y_Nominal(string ItemCode)
        {
            
            query = $@"SELECT Y_Nominal
                         FROM ST_Item_Inspect_Mgnt
                        WHERE ItemCode = '{ItemCode}'";

            return db.GetDataSet(query);
        }

        public DataTable ItemList()
        {
            query = $@"SELECT ItemCode, Y_Nominal
                         FROM ST_Item_Inspect_Mgnt";

            return db.GetDataSet(query);
        }

        public string Get_Abs_Id()
        {
            query = $@"SELECT [dbo].[Create_AbsID]()";

            return db.GetDataSet(query).Rows[0][0].ToString();
        }

        public void Insert_Abs_Result()
        {
            query = $@"INSERT INTO OC_AbsData_LearningResult
                                   (	   CorpId
                                          ,Abs_Id
	                                      ,Merge_Id 
                                          ,ItemCode
                                          ,Alias
                                          ,ItemName
                                          ,Result
                                          ,Image
                                          ,CreId
                                          ,CreDt
                                   )
                           VALUES('{CorpCd}', '{Abs_Id}', '{Merge_Id}', '{ItemCode}', '{Alias}', '{ItemName}', '{Result}',CONVERT(VARBINARY(MAX), '{Image}'), '{CreId}', GETDATE())";

            db.ExecuteQuery(query);
        }

        public DataTable SelectOC_AbsData_InspectTypeList(string abs_Id = null, string merge_Id = null, string itemCode = null)
        {
            query = @"SELECT DISTINCT
                             ad.InspectType
                        FROM OC_AbsData ad
                   LEFT JOIN OC_AbsData_LearningResult lr ON lr.abs_Id = ad.Abs_Id
                   		 AND lr.ItemCode = ad.ItemCode";

            if (abs_Id != null)
            {
                query += $"\n WHERE ad.Abs_Id = '{abs_Id}'";
            }
            if (merge_Id != null)
            {
                if (abs_Id == null)
                {
                    query += "\n WHERE ";
                }
                else
                {
                    query += "\n AND ";
                }

                query += $" lr.Merge_Id = '{merge_Id}'";
            }

            if (itemCode != null)
            {
                if (abs_Id == null && merge_Id == null)
                {
                    query += "\n WHERE ";
                }
                else
                {
                    query += "\n AND ";
                }

                query += $" ad.ItemCode = '{itemCode}'";
            }

            query += $"\n ORDER BY ad.InspectType DESC";

            return db.GetDataSet(query);
        }

        public DataRowView SelectOC_AbsData_XBarChartInfo(string abs_Id, string itemCode, string inspectType, float USL, float LSL)
        {
            query = $@"DECLARE @USL		FLOAT	= {USL},
                        	   @LSL		FLOAT	= {LSL}
                         
                         SELECT MIN(X_Bar) AS [X-Bar_Min],
                         	    MAX(X_Bar) AS [X-Bar_Max],
                         	    AVG(X_Bar) AS [X-Bar_CL],
                         	    --표준편차 :√(합((평균 - X_Bar)^2) / (데이터 수 - 1))
                         	    ISNULL(STDEV(X_Bar), 0) AS [X-Bar_표준편차],

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
                         			    MIN(Y_Value) AS [Min],
                         			    Max(Y_Value) AS [Max],
                         			    AVG(Y_Value) AS X_Bar,
                         			    MAX(Y_Value) - MIN(Y_Value) AS [Range]

                         	       FROM OC_AbsData
                         	    UNPIVOT(Y_Value
                         	    	    FOR Y_Cnt_Nm
                         	    	    IN ([Y1_Value], [Y2_Value], [Y3_Value])
                         	            ) AS unPvt

                         	      WHERE Abs_Id = '{abs_Id}'
                         	        AND ItemCode = '{itemCode}'
                                    AND InspectType = '{inspectType}'
                         	   GROUP BY Lot_No, InspectType, ItemCode
                               ) T
                      GROUP BY ItemCode";

            return db.GetDataRecord(query);
        }


        public DataRowView SelectOC_AbsData_RChartInfo(string abs_Id, string itemCode, string inspectType)
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
                           			  MIN(Y_Value) AS [Min],
                           			  Max(Y_Value) AS [Max],
                           			  AVG(Y_Value) AS X_Bar,
                           			  MAX(Y_Value) - MIN(Y_Value) AS [Range]

                           	     FROM OC_AbsData
                           	  UNPIVOT(Y_Value
                           		      FOR Y_Cnt_Nm
                           		      IN ([Y1_Value], [Y2_Value], [Y3_Value])
                           	          ) AS unPvt

                                WHERE Abs_Id = '{abs_Id}'
                           	      AND ItemCode = '{itemCode}'
                                  AND InspectType = '{inspectType}'
                           	 GROUP BY Lot_No, InspectType, ItemCode
                             ) T
                    GROUP BY ItemCode";

            return db.GetDataRecord(query);
        }

        public DataTable SelectOC_AbsData_ChartPointData(string abs_Id, string itemCode, string inspectType)
        {
            query = $@"SELECT Lot_No, InspectType, ItemCode,
                    		  AVG(Y_Value) AS X_Bar,
                    		  MAX(Y_Value) - MIN(Y_Value) AS [Range]
                         FROM OC_AbsData
                      UNPIVOT(Y_Value
                    	      FOR Y_Cnt_Nm
                    	      IN ([Y1_Value], [Y2_Value], [Y3_Value])
                              ) AS unPvt
                        WHERE Abs_Id = '{abs_Id}'
                          AND ItemCode = '{itemCode}'
                          AND InspectType = '{inspectType}'
                     GROUP BY Lot_No, InspectType, ItemCode";

            return db.GetDataSet(query);
        }

        public DataTable SelectAbsId()
        {
            query = $@"SELECT DISTINCT Abs_Id 추출ID, CONVERT(Date, lr.CreDt) AS 추출일자, sd.Remark
                                 FROM OC_AbsData_LearningResult lr
                                 LEFT JOIN  ST_Std_Dataset  sd on lr.Merge_Id = sd.Merge_Id
								 WHERE lr.Merge_Id = '{Merge_Id}'
                                 ORDER BY Abs_Id ASC";

            return db.GetDataSet(query);
        }

        public DataTable SELECT_MinMaxDate()
        {
            query = $@"SELECT MIN(ProductDate) as Min, Max(ProductDate) as Max FROM OC_AbsData WHERE Abs_Id = '{Abs_Id}'";

            return db.GetDataSet(query);
        }

        public DataTable SampleData(string min, string max)
        {
            query = $@"SELECT TOP 30 CAST(Y1_Value as decimal(10,2)) as Y1_Value, CAST(Y2_Value as decimal(10,2)) as Y2_Value, CAST(Y3_Value as decimal(10,2)) as Y3_Value
                                 FROM OC_AbsData 
                                 WHERE Outlier_Yn = 'N' AND Miss_Yn = 'N' AND ProductDate BETWEEN '{min}' AND '{max}' AND ItemCode = '{ItemCode}'";

            return db.GetDataSet(query);
        }

        public DataTable Image_Data(string AbsId, string ItemCode)
        {
            query = $@"SELECT Image
                         FROM OC_AbsData_LearningResult
                        WHERE Abs_Id = '{AbsId}' 
                          AND ItemCode = '{ItemCode}'";

            return db.GetDataSet(query);
        }

        public Bitmap GetBitmap(string AbsId, string ItemCode)
        {
            Bitmap bmp = null;
            try
            {
                DataTable dt = Image_Data(AbsId, ItemCode);
                if(dt.Rows.Count > 0)
                {
                    byte[] image = dt.Rows[0][0] as byte[];
                    if(image == null)
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

        public void Update_ModelScore(string f1, string accuracy, string precision, string recall)
        {
            query = $@"UPDATE Model_Score SET F1_Score = '{f1}', Accuracy = '{accuracy}', Precision = '{precision}', Recall = '{recall}' 
                                 WHERE Abs_Id = '{Abs_Id}' AND ItemCode = '{ItemCode}'";
            db.ExecuteQuery(query);
        }

        public void Update_Remark()
        {
            query = $@"UPDATE Model_Score SET Remark = '{Remark}'
                                 WHERE Abs_Id = '{Abs_Id}'";
            db.ExecuteQuery(query);
        }

        public void Insert_ModelScore(string f1, string accuracy, string precision, string recall)
        {
            query = $@"INSERT INTO Model_Score Values('{Abs_Id}', '{ItemCode}', '{f1}', '{precision}', '{recall}', '{accuracy}', '', '')";
            db.ExecuteQuery(query);
        }

        public DataTable Select_ModelScore()
        {
            query = $@"SELECT * FROM Model_Score WHERE Abs_Id = '{Abs_Id}' AND ItemCode = '{ItemCode}'";

            return db.GetDataSet(query);
        }

        public DataTable Select_ModelScore_All ()
        {
            query = $@"SELECT A.*, B.ItemName, B.Alias FROM Model_Score A
                                LEFT JOIN [ST_Item_Inspect_Mgnt] B ON A.ItemCode = B.ItemCode
                                ORDER BY Abs_Id ASC";

            return db.GetDataSet(query);
        }
    }
}
