using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace SmartCommon.Common
{
    // Shared by MES disposal and POP consumption so both use the same LOT balances and expiry rule.
    public static class SemiFinishedStock
    {
        public static int ShelfLifeDays
        {
            get
            {
                int days;
                if (!int.TryParse(ConfigurationManager.AppSettings["YOZM.SemiFinishedShelfLifeDays"], out days)) days = 3;
                if (days < 2 || days > 3) throw new InvalidOperationException("반제품 보관기간은 2일 또는 3일로 설정해주세요.");
                return days;
            }
        }

        private const string LotsSql = @"
            ;WITH Movements AS (
                SELECT D.CorpId,D.ItemCode,ISNULL(D.LotNo,N'') LotNo,D.Qty,D.Type,D.InOutDate,M.ToHouseCode,M.FromHouseCode
                FROM ST_StockD D WITH (UPDLOCK,HOLDLOCK)
                INNER JOIN ST_StockM M ON M.CorpId=D.CorpId AND M.InOutNo=D.InOutNo
                WHERE D.CorpId=@CorpId AND (@ItemCode=N'' OR D.ItemCode=@ItemCode)
            ), Balances AS (
                SELECT V.HouseCode,D.ItemCode,D.LotNo,SUM(D.Qty*V.Sign) StockQty
                FROM Movements D CROSS APPLY (
                    SELECT D.ToHouseCode HouseCode,1 Sign WHERE ISNULL(D.ToHouseCode,N'')<>N''
                    UNION ALL SELECT D.FromHouseCode,-1 WHERE ISNULL(D.FromHouseCode,N'')<>N''
                ) V GROUP BY V.HouseCode,D.ItemCode,D.LotNo
            ), Production AS (
                SELECT ItemCode,LotNo,MIN(InOutDate) ProducedAt FROM Movements
                WHERE Type=N'생산' AND ISNULL(ToHouseCode,N'')<>N'' GROUP BY ItemCode,LotNo
            )
            SELECT B.HouseCode,W.Title HouseName,B.ItemCode,I.ItemName,B.LotNo,B.StockQty,
                COALESCE(R.EndDate,P.ProducedAt) ProducedAt,
                DATEADD(day,@Days,COALESCE(R.EndDate,P.ProducedAt)) ExpiresAt
            FROM Balances B INNER JOIN CM_Item I ON I.CorpId=@CorpId AND I.ItemCode=B.ItemCode AND (@SemiOnly=0 OR I.ItemType='30')
            LEFT JOIN CM_Warehouse W ON W.CorpId=@CorpId AND W.HouseID=B.HouseCode
            LEFT JOIN Production P ON P.ItemCode=B.ItemCode AND P.LotNo=B.LotNo
            OUTER APPLY (SELECT MIN(EndDate) EndDate FROM PM_OrderResult R
                WHERE R.CorpId=@CorpId AND R.ItemCode=B.ItemCode AND R.SerialNo=B.LotNo) R
            WHERE B.StockQty>0 AND (@HouseCode=N'' OR B.HouseCode=@HouseCode)
            ORDER BY COALESCE(R.EndDate,P.ProducedAt),B.ItemCode,B.LotNo;";

        public static DataTable LoadLots(string connectionString, string corpId, string itemCode = "", string houseCode = "", bool semiFinishedOnly = true)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(LotsSql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                SetParameters(command, corpId, itemCode, houseCode);
                command.Parameters.AddWithValue("@SemiOnly", semiFinishedOnly);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static DataTable Allocate(string connectionString, string corpId, string itemCode, string houseCode, decimal quantity, bool enforceShelfLife = true)
        {
            if (Transaction.Current == null) throw new InvalidOperationException("반제품 배분은 실적 저장 트랜잭션 안에서 처리해야 합니다.");
            var allocated = new DataTable();
            allocated.Columns.Add("LotNo", typeof(string));
            allocated.Columns.Add("Qty", typeof(decimal));
            if (quantity <= 0) throw new InvalidOperationException("반제품 사용량은 0보다 커야 합니다.");
            DataTable lots = LoadLots(connectionString, corpId, itemCode, houseCode, enforceShelfLife);
            DateTime now = DateTime.Now;
            decimal remaining = quantity;
            foreach (DataRow lot in lots.Rows)
            {
                if (enforceShelfLife && (lot.IsNull("ExpiresAt") || Convert.ToDateTime(lot["ExpiresAt"]) <= now ||
                    string.IsNullOrWhiteSpace(Convert.ToString(lot["LotNo"])))) continue;
                decimal take = Math.Min(remaining, Convert.ToDecimal(lot["StockQty"]));
                allocated.Rows.Add(lot["LotNo"], take);
                remaining -= take;
                if (remaining == 0) return allocated;
            }
            throw new InvalidOperationException("품목 " + itemCode + "의 사용 가능한 LOT 재고가 부족합니다. 반제품은 만료 또는 생산일 미확인 LOT를 사용할 수 없습니다.");
        }

        public static string GetHouseCode(string connectionString, string corpId, string itemCode, string defaultHouse)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT HouseID FROM CM_Item WHERE CorpId=@CorpId AND ItemCode=@ItemCode", connection))
            {
                command.Parameters.AddWithValue("@CorpId", corpId);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                connection.Open();
                string house = Convert.ToString(command.ExecuteScalar());
                if (string.IsNullOrWhiteSpace(house)) house = defaultHouse;
                if (string.IsNullOrWhiteSpace(house)) throw new InvalidOperationException("품목 " + itemCode + "의 출고창고를 설정해주세요.");
                if (house.Length > 10) throw new InvalidOperationException("출고창고 " + house + "의 코드가 출고 상세의 10자 제한을 초과합니다. 창고 설정을 확인해주세요.");
                return house;
            }
        }

        public static bool IsSemiFinished(string connectionString, string corpId, string itemCode)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("SELECT COUNT(*) FROM CM_Item WHERE CorpId=@CorpId AND ItemCode=@ItemCode AND ItemType='30'", connection))
            {
                command.Parameters.AddWithValue("@CorpId", corpId);
                command.Parameters.AddWithValue("@ItemCode", itemCode);
                connection.Open();
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public static DataTable LoadDisposals(string connectionString, string corpId)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(@"SELECT TOP (1000) D.InOutDate,D.ItemCode,I.ItemName,D.LotNo,
                    M.FromHouseCode HouseCode,D.Qty,D.Remark,D.WriteID,D.InOutNo
                FROM ST_StockD D INNER JOIN ST_StockM M ON M.CorpId=D.CorpId AND M.InOutNo=D.InOutNo
                LEFT JOIN CM_Item I ON I.CorpId=D.CorpId AND I.ItemCode=D.ItemCode
                WHERE D.CorpId=@CorpId AND D.JobNo LIKE 'DS%' AND LEFT(D.Remark,4)=N'[폐기]'
                ORDER BY D.InOutDate DESC,D.InOutNo DESC", connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@CorpId", corpId);
                var table = new DataTable(); adapter.Fill(table); return table;
            }
        }

        public static void DisposeLot(string connectionString, string corpId, string itemCode, string houseCode,
            string lotNo, decimal quantity, string reason, string workerId, string requestId)
        {
            if (quantity <= 0 || string.IsNullOrWhiteSpace(reason) || reason.Length > 200 ||
                string.IsNullOrWhiteSpace(lotNo) || string.IsNullOrWhiteSpace(houseCode))
                throw new InvalidOperationException("LOT, 창고, 양수의 폐기수량과 200자 이내 폐기사유를 입력해주세요.");
            if (string.IsNullOrWhiteSpace(requestId) || requestId.Length > 20 || !requestId.StartsWith("DS", StringComparison.Ordinal))
                throw new InvalidOperationException("폐기 요청번호가 올바르지 않습니다.");
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable, Timeout = TimeSpan.FromMinutes(2) }))
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(@"DECLARE @Result int;
                    EXEC @Result=sys.sp_getapplock @Resource=@Key,@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=15000;
                    IF @Result<0 RAISERROR(N'폐기 처리 중입니다. 다시 시도해주세요.',16,1);
                    SELECT COUNT(*) FROM ST_StockD WHERE CorpId=@CorpId AND JobNo=@RequestId;", connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Key", "YOZM:Disposal:" + corpId + ":" + requestId);
                    command.Parameters.AddWithValue("@CorpId", corpId);
                    command.Parameters.AddWithValue("@RequestId", requestId);
                    if (Convert.ToInt32(command.ExecuteScalar()) > 0) { scope.Complete(); return; }
                }
                DataTable lots = LoadLots(connectionString, corpId, itemCode, houseCode);
                decimal available = 0;
                foreach (DataRow lot in lots.Rows)
                    if (Convert.ToString(lot["LotNo"]) == lotNo) available += Convert.ToDecimal(lot["StockQty"]);
                if (quantity > available) throw new InvalidOperationException("폐기수량이 현재 LOT 잔량을 초과합니다. 다시 조회해주세요.");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string number;
                    using (var numberCommand = new SqlCommand("Code_AutoNumber", connection))
                    {
                        numberCommand.CommandType = CommandType.StoredProcedure;
                        numberCommand.Parameters.AddWithValue("@pCorpID", corpId);
                        numberCommand.Parameters.AddWithValue("@pDocType", "재고");
                        numberCommand.Parameters.AddWithValue("@pCurrentDate", DateTime.Today.ToString("yyyyMMdd"));
                        var output = numberCommand.Parameters.Add("@pNumber", SqlDbType.NVarChar, 20);
                        output.Direction = ParameterDirection.Output;
                        numberCommand.ExecuteNonQuery();
                        if (output.Value == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(output.Value)))
                            throw new InvalidOperationException("재고번호 발급에 실패했습니다.");
                        number = "ST" + Convert.ToString(output.Value);
                    }
                    using (var command = new SqlCommand(@"
                        INSERT ST_StockM(CorpId,InOutNo,OutDate,FromHouseCode,WriteID,WriteDate,Remark,CreId,CreIP,CreDt,Status)
                        VALUES(@CorpId,@Number,GETDATE(),@House,@Worker,GETDATE(),@Reason,@Worker,HOST_NAME(),GETDATE(),N'완료');
                        INSERT ST_StockD(CorpId,InOutNo,Seq,InOutDate,Type,ItemCode,LotNo,FromHouseCode,JobNo,JobSeq,JobSeq2,
                            Qty,WriteID,CloseYn,Remark,CreId,CreIP,CreDt,Status)
                        VALUES(@CorpId,@Number,1,GETDATE(),'O',@ItemCode,@LotNo,@House,@RequestId,0,0,
                            @Qty,@Worker,'N',@Reason,@Worker,HOST_NAME(),GETDATE(),N'완료');", connection))
                    {
                        command.Parameters.AddWithValue("@CorpId", corpId);
                        command.Parameters.AddWithValue("@Number", number);
                        command.Parameters.AddWithValue("@House", houseCode);
                        command.Parameters.AddWithValue("@ItemCode", itemCode);
                        command.Parameters.AddWithValue("@LotNo", lotNo);
                        command.Parameters.AddWithValue("@Qty", quantity);
                        command.Parameters.AddWithValue("@Worker", workerId);
                        command.Parameters.AddWithValue("@Reason", "[폐기] " + reason.Trim());
                        command.Parameters.AddWithValue("@RequestId", requestId);
                        command.ExecuteNonQuery();
                    }
                }
                scope.Complete();
            }
        }

        private static void SetParameters(SqlCommand command, string corpId, string itemCode, string houseCode)
        {
            command.Parameters.AddWithValue("@CorpId", corpId);
            command.Parameters.AddWithValue("@ItemCode", itemCode);
            command.Parameters.AddWithValue("@HouseCode", houseCode);
            command.Parameters.AddWithValue("@Days", ShelfLifeDays);
        }
    }
}
