using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.DM
{
    public class DM_Code
    {
        #region Property
        public string ItemCode { get; set; }
        /// <summary>
        /// 공통코드 : D11010
        /// </summary>
        public string ForecastCycle { get; set; }
        public int BaseMonth { get; set; }
        [Obsolete]
        public string UseSalesYn { get; set; }
        [Obsolete]
        public string UseShipYn { get; set; }
        [Obsolete]
        public string UseStockYn { get; set; }
        public string UseYn { get; set; }
        public string Remark { get; set; }
        /// <summary>
        /// 공통코드 : D11020
        /// </summary>
        public string BaseType { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_Code(string itemName, string useYn)
        {
            query = $@"
                            SELECT '' AS Status,
                                   DC.ItemCode,
                                   DC.ItemCode AS ItemName,
                                   DC.ForecastCycle,
                                   DC.BaseMonth,
                                   DC.UseYn,
                                   DC.Remark,
                                   DC.BaseType
                            FROM DM_Code DC
                            LEFT JOIN CM_Item CI ON DC.CorpId = CI.CorpId AND DC.ItemCode = CI.ItemCode
                            WHERE DC.CorpId = '{WrGlobal.CorpID}'
                            AND ('' = '{useYn}' OR DC.UseYn = '{useYn}')
                            AND CI.ItemName LIKE '{itemName}%'
                      ";

            return db.GetDataTable(query);
        }

        public DataTable Select_BaseData(DateTime startDate, DateTime endDate)
        {
            string stDate = startDate.ToString("yyyy-MM-dd");
            string edDate = endDate.ToString("yyyy-MM-dd");

            query = $@"
                        DECLARE @pStartDate VARCHAR(10) = '{stDate}',
		                        @pEndDate VARCHAR(10) = '{edDate}';

                        WITH SdOrderData AS (
	                        SELECT BSOD.CorpId,
		                           BSOD.ItemCode,
		                           SUM(BSOD.Qty) AS Qty
	                        FROM BM_SdOrderD BSOD
	                        INNER JOIN BM_SdOrderM BSOM ON BSOD.CorpId = BSOM.CorpId AND BSOD.SdOrderNo = BSOM.SdOrderNo
	                        WHERE CONVERT(VARCHAR(10), BSOM.ObtainDate, 120) BETWEEN @pStartDate AND @pEndDate
	                        GROUP BY BSOD.CorpId, BSOD.ItemCode
                        ),

                        SalesData AS (
	                        SELECT BSD.CorpId,
		                           BSD.ItemCode,
		                           SUM(BSD.Qty) AS Qty
	                        FROM BM_SalesD BSD
	                        INNER JOIN BM_SalesM BSM ON BSD.CorpId = BSM.CorpId AND BSD.SalesNo = BSM.SalesNo
	                        WHERE CONVERT(VARCHAR(10), BSM.SalesDate, 120) BETWEEN @pStartDate AND @pEndDate
	                        GROUP BY BSD.CorpId, BSD.ItemCode
                        ),

                        ProdData AS (
	                        SELECT PP.CorpId,
		                           PP.ItemCode,
		                           SUM(PP.GoodQty) AS Qty
	                        FROM PM_ProductPlan PP
	                        WHERE CONVERT(VARCHAR(10), PP.WorkDate, 120) BETWEEN @pStartDate AND @pEndDate
	                        GROUP BY PP.CorpId, PP.ItemCode
                        ),

                        StockData AS (
	                        SELECT SD.CorpId,
                                   SD.ItemCode,
                                   SUM(CASE WHEN ISNULL(SM.FromHouseCode, '') <> '' THEN SD.Qty * -1
			                                WHEN ISNULL(SM.ToHouseCode, '') <> '' THEN SD.Qty
			                                ELSE 0 END) AS Qty
                            FROM ST_StockD SD
                            INNER JOIN ST_StockM SM ON SD.CorpId = SM.CorpId AND SD.InOutNo = SM.InOutNo
                            LEFT JOIN CM_Warehouse CW ON SM.CorpId = CW.CorpId AND (SM.FromHouseCode = CW.HouseID OR SM.ToHouseCode = CW.HouseID)
                            WHERE CONVERT(VARCHAR(10), SD.InOutDate, 120) <= @pEndDate
                            AND CW.WareType = '20'
                            GROUP BY SD.CorpId, SD.ItemCode
                        )

                        SELECT DC.ItemCode,
	                           CI.ItemName,
	                           DC.BaseType,
	                           ISNULL(SOD.Qty, 0) + ISNULL(SD.Qty, 0) + ISNULL(PD.Qty, 0) + ISNULL(SL.Qty, 0) AS Qty
                        FROM DM_Code DC
                        LEFT JOIN CM_Item CI ON DC.CorpId = CI.CorpId AND DC.ItemCode = CI.ItemCode
                        LEFT JOIN SdOrderData SOD ON DC.CorpId = SOD.CorpId AND DC.ItemCode = SOD.ItemCode AND DC.BaseType = '10'
                        LEFT JOIN SalesData SD ON DC.CorpId = SD.CorpId AND DC.ItemCode = SD.ItemCode AND DC.BaseType = '20'
                        LEFT JOIN ProdData PD ON DC.CorpId = PD.CorpId AND DC.ItemCode = PD.ItemCode AND DC.BaseType = '30'
                        LEFT JOIN StockData SL ON DC.CorpId = SL.CorpId AND DC.ItemCode = SL.ItemCode AND DC.BaseType = '40'
                        WHERE DC.CorpId = '{WrGlobal.CorpID}'
                      ";

            return db.GetDataTable(query);
        }

        public bool Insert_Code()
        {
            query = $@"
                            INSERT INTO DM_Code (
                                                    CorpId
                                                    , ItemCode
                                                    , ForecastCycle
                                                    , BaseMonth
                                                    , UseYn
                                                    , BaseType
                                                    , Remark
                                                    , CreId
                                                    , CreIP
                                                    , CreDt
                                                )
                                         VALUES (
                                                    '{WrGlobal.CorpID}'
                                                    , '{ItemCode}'
                                                    , '{ForecastCycle}'
                                                    , {BaseMonth}
                                                    , '{UseYn}'
                                                    , '{BaseType}'
                                                    , '{Remark}'
                                                    , '{WrGlobal.LoginID}'
                                                    , HOST_NAME()
                                                    , GETDATE()
                                                 )
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Update_Code()
        {
            query = $@"
                            UPDATE DM_Code
                            SET ForecastCycle = '{ForecastCycle}',
                                BaseMonth = {BaseMonth},
                                UseYn = '{UseYn}',
                                BaseType = '{BaseType}',
                                Remark = '{Remark}',
                                ModId = '{WrGlobal.LoginID}',
                                ModIP = HOST_NAME(),
                                ModDt = GETDATE()
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND ItemCode = '{ItemCode}'
                      ";

            return db.ExecuteQuery(query);
        }

        public bool Delete_Code()
        {
            query = $@"
                            DELETE FROM DM_Code
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND ItemCode = '{ItemCode}'
                      ";

            return db.ExecuteQuery(query);
        }

        public DataTable Select_Item_Lup(bool useYn = false, bool isAll = false)
        {
            query = "";
            if (isAll)
            {
                query += $@"
                            SELECT 'ALL' AS [품목코드],
                                   '전체' AS [품목명]

                            UNION ALL
                           ";
            }

            query += $@"
                            SELECT DR.ItemCode AS [ItemName],
	                               CI.ItemName AS [품목명]
                            FROM DM_Result DR
                            LEFT JOIN CM_Item CI ON DR.CorpId = CI.CorpId AND DR.ItemCode = CI.ItemCode
                            WHERE DR.CorpId = '{WrGlobal.CorpID}'
                            {(useYn ? "AND UseYn = 'Y'" : "")}
                       ";

            return db.GetDataTable(query);
        }
    }
}
