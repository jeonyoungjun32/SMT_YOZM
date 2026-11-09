using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Common;
using System;
using System.Data;

namespace SmartMES.Models.AI
{
	class OC_ProcCond
    {
        public string CorpId { get; set; }
        public string Cond_Id { get; set; }
        public string ProductDate { get; set; }
        public string ProductTime { get; set; }
        public string Lot_No { get; set; }
        public string InspectType { get; set; }
        public string Y_Method { get; set; }
        public string ItemCode { get; set; }
        public string Alias { get; set; }
        public string ItemName { get; set; }
        public float? Y1_Value { get; set; } = null;
        public float? Y2_Value { get; set; } = null;
        public float? Y3_Value { get; set; } = null;
        public float? Y_Min { get; set; } = null;
        public float? Y_Max { get; set; } = null;
        public float? X1_Value { get; set; } = null;
        public float? X2_Value { get; set; } = null;
        public float? X3_Value { get; set; } = null;
        public float? X4_Value { get; set; } = null;
        public float? X5_Value { get; set; } = null;
        public float? X6_Value { get; set; } = null;
        public float? X7_Value { get; set; } = null;
        public float? X8_Value { get; set; } = null;
        public float? X9_Value { get; set; } = null;
        public float? X10_Value { get; set; } = null;
        public float? X11_Value { get; set; } = null;
        public float? X12_Value { get; set; } = null;
        public float? X13_Value { get; set; } = null;
        public float? X14_Value { get; set; } = null;
        public float? X15_Value { get; set; } = null;

        public string StartDate { get; set; }
        public string EndDate { get; set; }


        Database db = new Database();

        string query;

        public DataTable Create_CondID()
        {
            query = $@"SELECT DBO.Create_CondID() AS CondID";

            return db.GetDataSet(query);
        }


        public DataTable SelectOC_ProcCond()
        {
            query = $@" SELECT   CorpId,Cond_Id,ProductDate,Lot_No
                                ,InspectType,Y_Method,ItemCode,Alias,ItemName
                                ,Y1_Value, Y2_Value, Y3_Value, Y_Min,Y_Max
                                ,X1_Value,X2_Value,X3_Value,X4_Value,X5_Value
                                ,X6_Value,X7_Value,X8_Value,X9_Value,X10_Value
                                ,X11_Value,X12_Value,X13_Value,X14_Value,X15_Value
                          FROM OC_ProcCond ";

            return db.GetDataSet(query);
        }

        public DataTable SelectOC_ProcCond(string SearchId, string StartDate, string EndDate)
        {
            query = $@" SELECT   CorpId,Cond_Id,ProductDate,ProductTime,Lot_No
                                ,InspectType,Y_Method,ItemCode,Alias,ItemName
                                ,Y1_Value, Y2_Value, Y3_Value, Y_Min,Y_Max
                                ,X1_Value,X2_Value,X3_Value,X4_Value,X5_Value
                                ,X6_Value,X7_Value,X8_Value,X9_Value,X10_Value
                                ,X11_Value,X12_Value,X13_Value,X14_Value,X15_Value
                          FROM OC_ProcCond 
                         WHERE ProductDate BETWEEN  '{StartDate}' AND '{EndDate}'
                         AND Cond_Id = '{SearchId}'
                         ORDER BY ProductDate ASC, ProductTime ASC";

            return db.GetDataSet(query);
        }


        public DataTable SelectProcess_RawData(string StartDate, string EndDate)
        {
            /*
            query = $@" SELECT  OPC.Cond_Id, PRD.*, '' AS ProductTime
                          FROM Process_RawData PRD
                          LEFT JOIN OC_ProcCond OPC 
	                            ON OPC.ProductDate = PRD.ProductDate
                               AND OPC.InspectType = PRD.InspectType
                               AND OPC.LOT_No = PRD.Lot_No
                               AND OPC.ItemCode = PRD.ItemCode
                         WHERE 1=1
                            AND PRD.ProductDate BETWEEN  '{StartDate}' AND '{EndDate}'
                           --AND OPC.Cond_Id IS NULL
                        ";
            */
            string today = DateTime.Now.Date.ToString().Replace("-", "").Substring(0, 8);
            string lastDate = "1";
            if (int.Parse(EndDate) >= int.Parse(today))
            {
                ProductTime = DateTime.Now.Hour.ToString();
                if (int.Parse(ProductTime) < 10) ProductTime = "0" + ProductTime;
                ProductTime = ProductTime + "00";
                lastDate = today;
                EndDate = DateTime.Now.AddDays(-1).ToString().Replace("-", "").Substring(0, 8);
            }


            query = $@" 
                        SELECT 
	                        ProductDate, ProductTIme, InspectType, LOT_No
	                        ,ItemCode, Alias ,ItemName
	                        ,Y_Method, MIN, MAX
	                        ,Y1_Value, Y2_Value, Y3_Value
	                        ,X1_Value, X2_Value, X3_Value
	                        ,X4_Value, X5_Value, X6_Value
	                        ,X7_Value, X8_Value, X9_Value
	                        ,X10_Value, X11_Value, X12_Value
	                        ,X13_Value, X14_Value, X15_Value
	                        ,X16_Value, X17_Value
                        FROM Process_RawData
                         WHERE 1=1
                            AND ProductDate BETWEEN  '{StartDate}' AND '{EndDate}' ";

            if (ProductTime.IsNullOrEmpty() == false) query += $@"OR (PRODUCTDATE = '{lastDate}' AND ProductTime < '{ProductTime}') "; 

            query += $@"ORDER BY ProductDate, ProductTime";

            // 예외추가..
            if (int.Parse(lastDate) == int.Parse(StartDate))
            {
                query = $@" 
                        SELECT 
	                        ProductDate, ProductTIme, InspectType, LOT_No
	                        ,ItemCode, Alias ,ItemName
	                        ,Y_Method, MIN, MAX
	                        ,Y1_Value, Y2_Value, Y3_Value
	                        ,X1_Value, X2_Value, X3_Value
	                        ,X4_Value, X5_Value, X6_Value
	                        ,X7_Value, X8_Value, X9_Value
	                        ,X10_Value, X11_Value, X12_Value
	                        ,X13_Value, X14_Value, X15_Value
                        FROM Process_RawData
                         WHERE 1=1
                            AND ProductDate BETWEEN  '{StartDate}' AND '{lastDate}' AND ProductTime < '{ProductTime}' 
                            ORDER BY ProductDate, ProductTime";
            }

            return db.GetDataSet(query);
        }

        public DataTable SelectOC_ProcCond_CondIDList()
        {
            query = @"SELECT pc.Cond_ID　공정조건ID
                              , MIN(PRODUCTDATE) 수집시작일자
                              , MAX(PRODUCTDATE) 수집종료일자
                              , sd.Remark
                        FROM OC_ProcCond pc
						JOIN ST_Std_Dataset  sd on pc.Cond_ID = sd.Cond_ID
						GROUP BY pc.Cond_Id, sd.Remark　ORDER BY pc.Cond_Id ASC";

            return db.GetDataSet(query);
        }

        public DataTable SelectOC_ProcCond_CondIDList_MergeYn()
        {
            query = @"SELECT pc.Cond_ID　공정조건ID
                           , MIN(PRODUCTDATE) 수집시작일자
                           , MAX(PRODUCTDATE) 수집종료일자
                           , CASE WHEN ds.Merge_Id IS NULL THEN 'N'
                           	      ELSE 'Y'
                           	 END AS 병합여부
                           , ds.Merge_Id AS 병합ID
                           , ds.Remark
                        FROM OC_ProcCond pc
                   LEFT JOIN ST_Std_DataSet ds ON ds.Cond_Id = pc.Cond_Id
                    GROUP BY pc.Cond_Id, ds.Merge_Id, ds.Remark
                    ORDER BY pc.Cond_Id ASC";

            return db.GetDataSet(query);
        }

        //public DataTable SelectOC_ProcCond_CondID(string cond_Id)
        //{
        //    query = $@"SELECT DISTINCT Cond_Id 공정조건ID
        //                      ,GetDate_From　수집시작일자
        //                      ,GetDate_To　수집종료일자
        //                FROM OC_ProcCond
        //                WHERE Cond_Id = '{cond_Id}'";

        //    return db.GetDataSet(query);
        //}

        public void Insert_OC_ProcCond()
        {
            db.Edit_Property(this);

            /*
             
            // ver1
            query = $@"
                    INSERT INTO  OC_ProcCond
                    (
                         CorpId, Cond_Id, ProductDate, ProductTime, Lot_No
                        ,InspectType, Y_Method
                        ,ItemCode, Alias, ItemName
                        ,Y1_Value,Y2_Value,Y3_Value,Y_Min,Y_Max
                        ,X1_Value, X2_Value, X3_Value, X4_Value, X5_Value
                        ,X6_Value, X7_Value, X8_Value, X9_Value
                        ,X10_Value ,X11_Value, X12_Value, X13_Value, X14_Value, X15_Value
                    )
                    VALUES
                    (
                         '{WrGlobal.CorpID}', '{CondId}', '{ProductDate}', '{ProductTime}', '{Lot_No}'
                        ,'{InspectType}', '{Y_Method}'
                        ,'{ItemCode}', '{Alias}', '{ItemName}'

                        , CASE WHEN '{Y1_Value}' = '' THEN NULL ELSE '{Y1_Value}' END
                        , CASE WHEN '{Y2_Value}' = '' THEN NULL ELSE '{Y2_Value}' END
                        , CASE WHEN '{Y3_Value}' = '' THEN NULL ELSE '{Y3_Value}' END

                        , CASE WHEN '{Y_Min}' = '' THEN NULL ELSE '{Y_Min}' END
                        , CASE WHEN '{Y_Max}' = '' THEN NULL ELSE '{Y_Max}' END

                        , CASE WHEN '{X1_Value}' = '' THEN NULL ELSE '{X1_Value}' END
                        , CASE WHEN '{X2_Value}' = '' THEN NULL ELSE '{X2_Value}' END
                        , CASE WHEN '{X3_Value}' = '' THEN NULL ELSE '{X3_Value}' END
                        , CASE WHEN '{X4_Value}' = '' THEN NULL ELSE '{X4_Value}' END
                        , CASE WHEN '{X5_Value}' = '' THEN NULL ELSE '{X5_Value}' END
                        , CASE WHEN '{X6_Value}' = '' THEN NULL ELSE '{X6_Value}' END
                        , CASE WHEN '{X7_Value}' = '' THEN NULL ELSE '{X7_Value}' END
                        , CASE WHEN '{X8_Value}' = '' THEN NULL ELSE '{X8_Value}' END
                        , CASE WHEN '{X9_Value}' = '' THEN NULL ELSE '{X9_Value}' END
                        , CASE WHEN '{X10_Value}' = '' THEN NULL ELSE '{X10_Value}' END
                        , CASE WHEN '{X11_Value}' = '' THEN NULL ELSE '{X11_Value}' END
                        , CASE WHEN '{X12_Value}' = '' THEN NULL ELSE '{X12_Value}' END
                        , CASE WHEN '{X13_Value}' = '' THEN NULL ELSE '{X13_Value}' END
                        , CASE WHEN '{X14_Value}' = '' THEN NULL ELSE '{X14_Value}' END
                        , CASE WHEN '{X15_Value}' = '' THEN NULL ELSE '{X15_Value}' END

                    ) ";

            db.ExecuteQuery(query);
            */


            // ver2
            query = $@" 
             INSERT INTO OC_ProcCond
             (
	            CorpId, Cond_Id, ProductDate, ProductTime, Lot_No
	            ,InspectType, Y_Method
	            ,ItemCode, Alias, ItemName
	            ,Y1_Value,Y2_Value,Y3_Value,Y_Min,Y_Max
	            ,X1_Value, X2_Value, X3_Value, X4_Value, X5_Value
	            ,X6_Value, X7_Value, X8_Value, X9_Value,X10_Value
	            ,X11_Value, X12_Value, X13_Value, X14_Value, X15_Value
	            ,X16_Value, X17_Value
             )
		  
            SELECT 
                    '{WrGlobal.CorpID}', '{Cond_Id}', CONVERT(varchar(10), CONVERT(Decimal, ProductDate)), ProductTIme, LOT_No
                    ,InspectType,Y_Method
                    ,ItemCode, Alias, ItemName            
	                ,Y1_Value, Y2_Value, Y3_Value, [MIN], [MAX]
	                ,X1_Value, X2_Value, X3_Value, X4_Value, X5_Value
	                ,X6_Value, X7_Value, X8_Value, X9_Value
	                ,X10_Value, X11_Value, X12_Value ,X13_Value, X14_Value, X15_Value
	                ,X16_Value, X17_Value
             FROM Process_RawData
            WHERE ProductDate BETWEEN '{StartDate}' AND '{EndDate}'
            ORDER BY ProductDate, ProductTIme
";

            db.ExecuteQuery(query);
        }
    }
}
