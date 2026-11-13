using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.PM
{
    internal static class PM_Forecasting
    {
        /// <summary>사업장별 거래처·품목과 기준월 실 발주량 조회</summary>
        internal static DataTable SelectActualQuantities(DateTime selectedMonth)
        {
            var monthStart = new DateTime(selectedMonth.Year, selectedMonth.Month, 1);
            string fromDate = monthStart.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string toDate = monthStart.AddMonths(1).ToString("yyyyMMdd", CultureInfo.InvariantCulture);

            string query = $@"
                SELECT COALESCE(f.CustCode,a.CustCode) AS CustCode,
                       COALESCE(c.CustType,N'미분류') AS CustType,
                       COALESCE(c.CustName,a.ChannelName) AS ChannelName,
                       COALESCE(f.ItemCode,a.ItemCode) AS ItemCode,
                       COALESCE(i.ItemName,a.ItemName) AS ItemName,
                       DAY(a.OrderDate) AS OrderDay, a.OrderQty
                FROM
                (
                    SELECT p.CustCode,p.ItemCode,MIN(p.SourceOrder) AS SourceOrder
                    FROM dbo.CM_ItemPrice p
                    INNER JOIN dbo.CM_Cust c ON c.CorpId=p.CorpId AND c.CustCode=p.CustCode
                    WHERE p.CorpId='{WrGlobal.CorpID}' AND p.PriceType='20'
                      AND ISNULL(p.UseYn,'Y')='Y' AND c.UseYn='Y'
                      AND p.ItemCode NOT IN ('9DL00000001','9DL00000002','9DL00000003')
                      AND c.CustType IN ('B2B','B2C')
                      -- 거래처와 품목의 공통 표시 설정을 모두 적용
                      AND ISNULL(c.ForecastUseYn,'N')='Y'
                      AND ISNULL(p.ForecastUseYn,'Y')='Y'
                    GROUP BY p.CustCode,p.ItemCode
                ) f
                FULL OUTER JOIN
                (
                    SELECT CustCode,ItemCode,MIN(ChannelName) AS ChannelName,
                           MIN(ItemName) AS ItemName,OrderDate,SUM(OrderQty) AS OrderQty
                    FROM dbo.PM_ForecastOrderActual
                    WHERE CorpId='{WrGlobal.CorpID}'
                      AND OrderDate>='{fromDate}' AND OrderDate<'{toDate}'
                    GROUP BY CustCode,ItemCode,OrderDate
                ) a ON a.CustCode=f.CustCode AND a.ItemCode=f.ItemCode
                LEFT JOIN dbo.CM_Cust c ON c.CorpId='{WrGlobal.CorpID}'
                    AND c.CustCode=COALESCE(f.CustCode,a.CustCode)
                LEFT JOIN dbo.CM_Item i ON i.CorpId='{WrGlobal.CorpID}'
                    AND i.ItemCode=COALESCE(f.ItemCode,a.ItemCode)
                -- 사용 거래처·품목만 표시, 실 발주량으로 숨긴 항목이 다시 표시되는 현상 방지
                WHERE f.CustCode IS NOT NULL
                ORDER BY c.CustType,c.CustName,f.SourceOrder,ItemCode,a.OrderDate";

            return new Database().GetDataTable(query);
        }

        /// <summary>변경한 날짜의 실 발주량 저장 및 동시 수정 검사</summary>
        internal static void SaveActualQuantities(string custCode, string itemCode, string channel, string itemName, DataTable quantities)
        {
            if (string.IsNullOrWhiteSpace(custCode) || string.IsNullOrWhiteSpace(itemCode))
                throw new InvalidOperationException("거래처코드와 품목코드를 확인해 주세요.");

            using (DataTable changed = quantities.GetChanges(DataRowState.Modified))
            {
                if (changed == null)
                    return;

                using (var connection = new SqlConnection(Database.m_ConnectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandText = @"
                            SET NOCOUNT ON;
                            IF @OldQty IS NULL
                            BEGIN
                                INSERT dbo.PM_ForecastOrderActual (CorpId, CustCode, ItemCode, ChannelName, ItemName, OrderDate, OrderQty)
                                SELECT @CorpId, @CustCode, @ItemCode, @Channel, @ItemName, @Date, @Qty
                                WHERE NOT EXISTS
                                (
                                    SELECT 1 FROM dbo.PM_ForecastOrderActual WITH (UPDLOCK, HOLDLOCK)
                                    WHERE CorpId = @CorpId AND CustCode = @CustCode
                                      AND ItemCode = @ItemCode AND OrderDate = @Date
                                );
                            END
                            ELSE IF @Qty IS NULL
                            BEGIN
                                DELETE dbo.PM_ForecastOrderActual
                                WHERE CorpId = @CorpId AND CustCode = @CustCode AND ItemCode = @ItemCode
                                  AND OrderDate = @Date AND OrderQty = @OldQty;
                            END
                            ELSE
                            BEGIN
                                UPDATE dbo.PM_ForecastOrderActual
                                SET OrderQty = @Qty
                                WHERE CorpId = @CorpId AND CustCode = @CustCode AND ItemCode = @ItemCode
                                  AND OrderDate = @Date AND OrderQty = @OldQty;
                            END;
                            SELECT @@ROWCOUNT;";

                        command.Parameters.Add("@CorpId", SqlDbType.NVarChar, 10).Value = WrGlobal.CorpID;
                        command.Parameters.Add("@CustCode", SqlDbType.NVarChar, 20).Value = custCode;
                        command.Parameters.Add("@ItemCode", SqlDbType.NVarChar, 200).Value = itemCode;
                        command.Parameters.Add("@Channel", SqlDbType.NVarChar, 100).Value = channel;
                        command.Parameters.Add("@ItemName", SqlDbType.NVarChar, 200).Value = itemName;
                        command.Parameters.Add("@Date", SqlDbType.Date);
                        command.Parameters.Add("@Qty", SqlDbType.Int);
                        command.Parameters.Add("@OldQty", SqlDbType.Int);

                        foreach (DataRow row in changed.Rows)
                        {
                            object quantity = row["OrderQty"];
                            object original = row["OrderQty", DataRowVersion.Original];

                            if (Equals(quantity, original))
                                continue;

                            command.Parameters["@Date"].Value = row["OrderDate"];
                            command.Parameters["@Qty"].Value = quantity;
                            command.Parameters["@OldQty"].Value = original;

                            if ((int)command.ExecuteScalar() != 1)
                                throw new InvalidOperationException("다른 곳에서 발주량을 변경했습니다. 화면을 다시 조회해 주세요.");
                        }

                        transaction.Commit();
                    }
                }
            }
        }
    }
}
