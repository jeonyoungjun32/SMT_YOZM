using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using SmartCommon.Common;

namespace SmartMES.Models.BM
{
    /// <summary>EDI 원본 저장·품목 연결·포케스팅 반영</summary>
    internal static class BM_EdiImport
    {
        // 업로드 직후와 반영 시 같은 품목 연결 기준 사용
        private const string ItemConnectionSql = @"
                    UPDATE r
                    SET ItemCode = m.ItemCode,
                           Factor = m.Factor
                    FROM BM_EdiImportRow AS r
                           INNER JOIN BM_EdiItemMap AS m
                           ON m.CorpId = r.CorpId
                              AND m.CustCode = r.CustCode
                              AND m.SourceKey = r.SourceKey
                              AND NULLIF(r.ItemCode, '') IS NULL
                    WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.FileId = @File
                           AND r.CorpId = @Corp
                           AND r.Applied = 0;

                    UPDATE r
                    SET ItemCode = x.ItemCode
                    FROM BM_EdiImportRow AS r CROSS APPLY (SELECT MIN(m.ItemCode) AS ItemCode
                                                             FROM BM_EdiItemMap AS m
                                                             WHERE m.CorpId = r.CorpId
                                                                    AND m.SourceCode = r.SourceCode
                                                                    AND m.Factor = 1
                                                             HAVING COUNT(DISTINCT m.ItemCode) = 1) AS x
                    WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.FileId = @File
                           AND r.CorpId = @Corp
                           AND r.Applied = 0
                           AND NULLIF (r.ItemCode, '') IS NULL
                           AND LEN(r.SourceCode) = 13
                           AND r.SourceCode NOT LIKE '%[^0-9]%'
                           AND r.SourceOption = '';

                    UPDATE r
                    SET ItemCode = x.ItemCode
                    FROM BM_EdiImportRow AS r CROSS APPLY (SELECT MIN(p.ItemCode) AS ItemCode
                                                             FROM CM_ItemPrice AS p
                                                             WHERE p.CorpId = r.CorpId
                                                                    AND p.CustCode = r.CustCode
                                                                    AND p.PriceType = '20'
                                                                    AND (p.CustomerItemCode = NULLIF (r.SourceCode, '')
                                                                         OR p.CustomerItemName = r.SourceName)
                                                             HAVING COUNT(DISTINCT p.ItemCode) = 1) AS x
                    WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.FileId = @File
                           AND r.CorpId = @Corp
                           AND r.Applied = 0
                           AND NULLIF (r.ItemCode, '') IS NULL
                           AND r.SourceOption = '';

                    UPDATE r
                    SET ItemCode = x.ItemCode
                    FROM BM_EdiImportRow AS r CROSS APPLY (SELECT MIN(i.ItemCode) AS ItemCode
                                                             FROM CM_Item AS i
                                                             WHERE i.CorpId = r.CorpId
                                                                    AND i.UseYn = 'Y'
                                                                    AND (i.ItemCode = NULLIF (r.SourceCode, '')
                                                                         OR i.BarCode = NULLIF (r.SourceCode, '')
                                                                         OR i.ItemName = r.SourceName)
                                                             HAVING COUNT(*) = 1) AS x
                    WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.FileId = @File
                           AND r.CorpId = @Corp
                           AND r.Applied = 0
                           AND NULLIF (r.ItemCode, '') IS NULL
                           AND (r.SourceOption = '' OR r.SourceOption LIKE N'세트 분리:%');
        ";

        internal static bool IsDeliveryFee(object itemCode) => new[] { "9DL00000001", "9DL00000002", "9DL00000003" }.Contains(Convert.ToString(itemCode));

        private static SqlConnection Open()
        {
            var connection = new SqlConnection(Database.m_ConnectionString);
            connection.Open();
            return connection;
        }

        private static SqlCommand Command(SqlConnection connection, SqlTransaction transaction, string sql, params object[] parameters)
        {
            var command = new SqlCommand(sql, connection, transaction)
            {
                CommandTimeout = 60
            };
            command.Parameters.AddWithValue("@Corp", WrGlobal.CorpID);
            command.Parameters.AddWithValue("@User", WrGlobal.LoginID);
            for (int i = 0; i < parameters.Length; i += 2)
                command.Parameters.AddWithValue((string)parameters[i], parameters[i + 1] ?? DBNull.Value);

            return command;
        }

        /// <summary>사업장 조건과 매개변수를 사용하는 공통 조회</summary>
        internal static DataTable Query(string sql, params object[] parameters)
        {
            using (var connection = Open())
            using (var command = Command(connection, null, sql, parameters))
            using (var adapter = new SqlDataAdapter(command))
            {
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        /// <summary>B2B 등록 화면과 같은 사업장의 사용 거래처 조회</summary>
        internal static DataTable Customers()
        {
            return Query(@"
                SELECT CustCode, CustName, EdiMatchNames
                FROM dbo.CM_Cust
                WHERE CorpId=@Corp AND UseYn='Y' AND CustType IN ('B2B','B2C')
                ORDER BY CustName, CustCode;");
        }

        /// <summary>EDI 등록일·거래처별 업로드 목록</summary>
        internal static DataTable Files(DateTime date, string customer = "", string customerType = "전체")
        {
            return Query(@"
                SELECT   f.FileId,
                         f.FileName,
                         @Date AS BasisDate,
                         f.Status,
                         f.ErrorMessage,
                         f.CreId,
                         f.CreDt,
                         CASE WHEN COUNT(DISTINCT c.CustType) > 1 THEN 'B2B / B2C'
                              ELSE ISNULL(MAX(c.CustType), N'미연결') END AS CustType,
                         ISNULL(STUFF((
                             SELECT N', ' + names.CustName
                             FROM (
                                 SELECT DISTINCT customer.CustName
                                 FROM BM_EdiImportRow detail
                                 INNER JOIN CM_Cust customer
                                     ON customer.CorpId = detail.CorpId AND customer.CustCode = detail.CustCode
                                 WHERE detail.CorpId = @Corp AND detail.FileId = f.FileId
                             ) names
                             ORDER BY names.CustName
                             FOR XML PATH(''), TYPE
                         ).value('.', 'nvarchar(max)'), 1, 2, ''), N'미연결') AS CustName,
                         COUNT(r.RowId) AS [RowCount],
                         SUM(CASE WHEN r.Applied = 1 THEN 1 ELSE 0 END) AS AppliedCount
                FROM BM_EdiImportFile AS f
                         LEFT OUTER JOIN BM_EdiImportRow AS r
                         ON r.FileId = f.FileId
                         LEFT JOIN CM_Cust c ON c.CorpId=r.CorpId AND c.CustCode=r.CustCode AND c.CustType IN ('B2B','B2C')
                WHERE f.CorpId = @Corp
                         AND EXISTS (SELECT 1
                                     FROM BM_EdiImportRow AS d
                                     WHERE d.FileId = f.FileId
                                            AND d.OrderDate = @Date)
                         AND (@Type = N'전체' OR EXISTS (SELECT 1 FROM BM_EdiImportRow t JOIN CM_Cust tc ON tc.CorpId=t.CorpId AND tc.CustCode=t.CustCode WHERE t.FileId=f.FileId AND tc.CustType=@Type))
                         AND (@Customer = ''
                              OR EXISTS (SELECT 1
                                         FROM BM_EdiImportRow AS x
                                         WHERE x.FileId = f.FileId
                                                AND x.CustCode = @Customer))
                GROUP BY f.FileId, f.FileName, f.BasisDate, f.Status, f.ErrorMessage, f.CreId, f.CreDt
                ORDER BY f.FileId DESC;
            ",
                "@Date", date.Date,
                "@Customer", customer,
                "@Type", customerType);
        }

        /// <summary>업로드 상세와 미연결 항목 안내</summary>
        internal static DataTable Details(long fileId)
        {
            var rows = Query(@"
                SELECT   r.*,
                         i.ItemName,
                         c.CustName,
                         r.Amount AS StoredAmount,
                         COALESCE(p.Price, NULLIF(i.Amount, 0)) AS PreviewPrice
                FROM BM_EdiImportRow AS r
                         LEFT OUTER JOIN CM_Item AS i
                         ON i.CorpId = r.CorpId
                            AND i.ItemCode = r.ItemCode
                         LEFT OUTER JOIN CM_Cust AS c
                         ON c.CorpId = r.CorpId
                            AND c.CustCode = r.CustCode
                OUTER APPLY
                (
                    SELECT MIN(v.Price) AS Price
                    FROM
                    (
                        SELECT TOP (1) WITH TIES Price
                        FROM CM_ItemPrice
                        WHERE CorpId = r.CorpId AND CustCode = r.CustCode
                            AND ItemCode = r.ItemCode AND PriceType = '20'
                            AND ISNULL(UseYn, 'Y') = 'Y'
                            AND (ApplyDate IS NULL OR ApplyDate <= r.OrderDate)
                            AND (EndDate IS NULL OR EndDate >= r.OrderDate)
                        ORDER BY ApplyDate DESC
                    ) v
                    HAVING COUNT(DISTINCT v.Price) = 1 AND MIN(v.Price) > 0
                ) p
                WHERE r.CorpId = @Corp
                         AND r.FileId = @File
                ORDER BY r.SourceRow;
            ",
                "@File", fileId);

            foreach (DataRow row in rows.Rows)
            {
                PreviewAmount(row, row["PreviewPrice"]);

                if (Convert.ToBoolean(row["Applied"]))
                    continue;
                if (string.IsNullOrEmpty(Convert.ToString(row["ErrorMessage"])))
                {
                    string message = row.IsNull("CustName") ? "거래처 선택 필요"
                        : row.IsNull("ItemName") ? "품목 선택 필요" : "";

                    // 원본 금액이 없으면 거래처 단가 또는 직접 입력 필요
                    if (row.IsNull("Amount"))
                        message += (message.Length == 0 ? "" : " / ") + "금액 미입력: 거래처별 품목관리의 등록 금액 확인 또는 직접 입력";

                    row["ErrorMessage"] = message;
                }
            }

            rows.AcceptChanges();
            return rows;
        }

        /// <summary>거래처·품목 변경 시 유효 단가로 금액 미리 계산</summary>
        internal static void PreviewAmount(DataRow row)
        {
            using (var prices = Query(@"
                SELECT COALESCE(CASE WHEN COUNT(DISTINCT v.Price)=1 AND MIN(v.Price)>0 THEN MIN(v.Price) END, (SELECT NULLIF(Amount,0) FROM CM_Item WHERE CorpId=@Corp AND ItemCode=@Item)) AS Price
                FROM
                (
                    SELECT TOP (1) WITH TIES Price
                    FROM CM_ItemPrice
                    WHERE CorpId=@Corp AND CustCode=@Customer AND ItemCode=@Item
                        AND PriceType='20' AND ISNULL(UseYn,'Y')='Y'
                        AND (ApplyDate IS NULL OR ApplyDate<=@Date)
                        AND (EndDate IS NULL OR EndDate>=@Date)
                    ORDER BY ApplyDate DESC
                ) v;",
                "@Customer", row["CustCode"], "@Item", row["ItemCode"], "@Date", row["OrderDate"]))
            {
                PreviewAmount(row, prices.Rows.Count == 0 ? DBNull.Value : prices.Rows[0]["Price"]);
            }
        }

        internal static void PreviewAmount(DataRow row, object price)
        {
            if (IsDeliveryFee(row["ItemCode"]))
                return;

            // 원본·직접 입력 금액은 유지, 자동 계산 금액만 재계산
            if (!row.IsNull("Amount") && (row.IsNull("AmountFromPrice") || !Convert.ToBoolean(row["AmountFromPrice"])))
                return;

            row["Amount"] = price == DBNull.Value || row.IsNull("Qty") || row.IsNull("Factor")
                ? (object)DBNull.Value
                : Math.Round(Convert.ToDecimal(row["Qty"]) * Convert.ToDecimal(row["Factor"])
                    * Convert.ToDecimal(price), 0, MidpointRounding.AwayFromZero);
            row["AmountFromPrice"] = true;
        }

        /// <summary>엑셀 없이 입력할 등록 초안과 상세 행 저장</summary>
        internal static long CreateManual(DateTime date, string customer)
        {
            // 직접 등록은 각각 독립된 주문으로 구분, 저장·반영 전까지 수량 집계 제외
            string key = "수동:" + Guid.NewGuid().ToString("N");
            using (var connection = Open())
            using (var transaction = connection.BeginTransaction())
            {
                using (var command = Command(connection, transaction, @"
                    INSERT BM_EdiImportFile(CorpId,FileKey,FileName,BasisDate,Status,CreId)
                    VALUES(@Corp,@Key,N'직접 등록',@Date,N'작성중',@User);

                    DECLARE @File bigint = CAST(SCOPE_IDENTITY() AS bigint);
                    INSERT BM_EdiImportRow
                    (
                        FileId,CorpId,SourceSheet,SourceRow,CustCode,SourceCode,SourceName,
                        SourceOption,SourceKey,RowKey,OrderNo,CenterName,ShipToName,
                        OrderDate,Qty,Factor,Amount,Applied
                    )
                    VALUES
                    (
                        @File,@Corp,N'직접 등록',1,NULLIF(@Customer,''),'','',
                        '',@Key,@Key,'','','',@Date,0,1,NULL,0
                    );
                    SELECT @File;",
                    "@Key", key,
                    "@Date", date.Date,
                    "@Customer", customer))
                {
                    long id = Convert.ToInt64(command.ExecuteScalar());
                    transaction.Commit();
                    return id;
                }
            }
        }

        internal static string SourceKey(BM_EdiExcel.Row row)
        {
            // 옵션별 구성품 혼동 방지, 주문코드가 없는 양식은 원본 품명으로 연결
            string key = string.IsNullOrWhiteSpace(row.SourceCode)
                ? "N:" + BM_EdiExcel.Normalize(row.SourceName).Replace("|", "||")
                : "C:" + BM_EdiExcel.Normalize(row.SourceCode).Replace("|", "||");

            if (!string.IsNullOrWhiteSpace(row.Option))
                key += "|O:" + BM_EdiExcel.Normalize(row.Option).Replace("|", "||");

            return key;
        }

        /// <summary>등록 건 또는 선택 행 삭제와 해당 포케스팅 수량 복원</summary>
        internal static void Delete(long fileId, long[] rowIds, bool deleteFile = false)
        {
            if (!deleteFile && (rowIds == null || rowIds.Length == 0))
                return;

            using (var connection = Open())
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                using (var command = Command(connection, transaction, @"
                    DECLARE @LockResult int;
                    EXEC @LockResult = sys.sp_getapplock @Resource=@Corp,
                        @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout=30000;
                    IF @LockResult < 0 THROW 50001, N'다른 작업 처리 중입니다.', 1;
                    IF NOT EXISTS (SELECT 1 FROM BM_EdiImportFile WHERE CorpId=@Corp AND FileId=@File)
                        THROW 50001, N'삭제할 등록 건이 없습니다.', 1;

                    SELECT DISTINCT CustCode, ItemCode, OrderDate AS BasisDate
                    INTO #DeleteKeys
                    FROM BM_EdiImportRow
                    WHERE CorpId=@Corp AND FileId=@File AND Applied=1 AND ItemCode NOT IN ('9DL00000001','9DL00000002','9DL00000003')
                        AND (@DeleteFile = 1 OR RowId IN (SELECT CONVERT(bigint, value) FROM STRING_SPLIT(@Rows, ',')));

                    -- 포케스팅에서 별도 수정한 수량은 삭제로 덮어쓰지 않도록 확인
                    IF EXISTS (
                        SELECT 1 FROM #DeleteKeys k
                        LEFT JOIN BM_EdiForecastBackup b ON b.CorpId=@Corp
                            AND b.CustCode=k.CustCode AND b.ItemCode=k.ItemCode AND b.BasisDate=k.BasisDate
                        LEFT JOIN PM_ForecastOrderActual a ON a.CorpId=@Corp
                            AND a.CustCode=k.CustCode AND a.ItemCode=k.ItemCode AND a.OrderDate=k.BasisDate
                        WHERE b.LastQty IS NULL OR a.OrderQty IS NULL OR b.LastQty<>a.OrderQty)
                        THROW 50001, N'포케스팅 수량이 별도로 변경되어 삭제할 수 없습니다.', 1;

                    DELETE FROM BM_EdiImportRow WHERE CorpId=@Corp AND FileId=@File
                        AND (@DeleteFile = 1 OR RowId IN (SELECT CONVERT(bigint, value) FROM STRING_SPLIT(@Rows, ',')));

                    SELECT k.CustCode, k.ItemCode, k.BasisDate,
                        COUNT(r.RowId) AS RemainingCount, SUM(r.Qty*r.Factor) AS Qty
                    INTO #Remaining
                    FROM #DeleteKeys k
                    LEFT JOIN BM_EdiImportRow r ON r.CorpId=@Corp AND r.Applied=1
                        AND r.CustCode=k.CustCode AND r.ItemCode=k.ItemCode AND r.OrderDate=k.BasisDate
                    GROUP BY k.CustCode,k.ItemCode,k.BasisDate;

                    UPDATE a SET OrderQty=CASE WHEN r.RemainingCount>0 THEN r.Qty ELSE b.PreviousQty END
                    FROM PM_ForecastOrderActual a
                    INNER JOIN #Remaining r ON a.CorpId=@Corp AND a.CustCode=r.CustCode
                        AND a.ItemCode=r.ItemCode AND a.OrderDate=r.BasisDate
                    INNER JOIN BM_EdiForecastBackup b ON b.CorpId=a.CorpId AND b.CustCode=a.CustCode
                        AND b.ItemCode=a.ItemCode AND b.BasisDate=a.OrderDate
                    WHERE r.RemainingCount>0 OR b.PreviousQty IS NOT NULL;

                    DELETE a FROM PM_ForecastOrderActual a
                    INNER JOIN #Remaining r ON a.CorpId=@Corp AND a.CustCode=r.CustCode
                        AND a.ItemCode=r.ItemCode AND a.OrderDate=r.BasisDate
                    INNER JOIN BM_EdiForecastBackup b ON b.CorpId=a.CorpId AND b.CustCode=a.CustCode
                        AND b.ItemCode=a.ItemCode AND b.BasisDate=a.OrderDate
                    WHERE r.RemainingCount=0 AND b.PreviousQty IS NULL;

                    UPDATE b SET LastQty=r.Qty, ModDt=SYSDATETIME()
                    FROM BM_EdiForecastBackup b INNER JOIN #Remaining r ON b.CorpId=@Corp
                        AND b.CustCode=r.CustCode AND b.ItemCode=r.ItemCode AND b.BasisDate=r.BasisDate
                    WHERE r.RemainingCount>0;
                    DELETE b FROM BM_EdiForecastBackup b INNER JOIN #Remaining r ON b.CorpId=@Corp
                        AND b.CustCode=r.CustCode AND b.ItemCode=r.ItemCode AND b.BasisDate=r.BasisDate
                    WHERE r.RemainingCount=0;
                        UPDATE BM_EdiImportFile SET Status=CASE
                            WHEN EXISTS(SELECT 1 FROM BM_EdiImportRow WHERE FileId=@File AND Applied=0)
                                THEN N'확인필요'
                            WHEN EXISTS(SELECT 1 FROM BM_EdiImportRow WHERE FileId=@File)
                                THEN N'반영완료' ELSE N'작성중' END, ErrorMessage=NULL
                        WHERE CorpId=@Corp AND FileId=@File;
                    -- 등록 삭제는 상세와 포케스팅 재집계 후 파일 정보까지 함께 제거
                    IF @DeleteFile = 1
                        DELETE FROM BM_EdiImportFile WHERE CorpId=@Corp AND FileId=@File;
                ", "@File", fileId, "@Rows", string.Join(",", (rowIds ?? new long[0]).Distinct()), "@DeleteFile", deleteFile))
                    command.ExecuteNonQuery();

                transaction.Commit();
            }
        }

        /// <summary>선물세트 구성품의 거래처별 금액 적용, 품목 합계의 원 단위 반올림 차이 보정</summary>
        internal static void SetComponentAmounts(List<BM_EdiExcel.Row> rows)
        {
            foreach (var group in rows.Where(row => row.UseComponentPrice)
                .GroupBy(row => new { row.Customer, row.SourceName, row.OrderDate }))
            {
                using (var prices = Query(@"
                    SELECT p.Price
                    FROM CM_Item i
                    CROSS APPLY
                    (
                        SELECT TOP (1) WITH TIES Price
                        FROM CM_ItemPrice
                        WHERE CorpId=i.CorpId AND ItemCode=i.ItemCode AND CustCode=@Customer
                            AND PriceType='20' AND ISNULL(UseYn,'Y')='Y'
                            AND (ApplyDate IS NULL OR ApplyDate<=@Date)
                            AND (EndDate IS NULL OR EndDate>=@Date)
                        ORDER BY ApplyDate DESC
                    ) p
                    WHERE i.CorpId=@Corp AND i.ItemName=@Name AND i.UseYn='Y';",
                    "@Customer", group.Key.Customer, "@Name", group.Key.SourceName,
                    "@Date", group.Key.OrderDate))
                {
                    var values = prices.AsEnumerable().Where(row => !row.IsNull("Price"))
                        .Select(row => Convert.ToDecimal(row["Price"])).Distinct().ToArray();

                    if (values.Length != 1 || values[0] <= 0)
                        throw new InvalidOperationException(group.Key.SourceName
                            + ": 등록일에 유효한 거래처별 금액을 확인해 주세요.");

                    decimal total = 0m;
                    decimal allocated = 0m;
                    foreach (var row in group)
                    {
                        total += row.Qty.Value * values[0];
                        decimal rounded = Math.Round(total, 0, MidpointRounding.AwayFromZero);
                        row.Amount = rounded - allocated;
                        allocated = rounded;
                    }
                }
            }
        }

        /// <summary>원본 파일 중복 검사와 상세 저장</summary>
        internal static long Import(byte[] bytes, string fileName, string selectedCustomer = "")
        {
            // 파일명 출고일을 EDI 등록일과 포케스팅 반영일로 공통 사용
            bool useFileDate;
            var rows = BM_EdiExcel.Read(bytes, fileName, out useFileDate, selectedCustomer);

            if (rows.Any(row => !row.OrderDate.HasValue))
                throw new InvalidOperationException("파일명 출고일이 지정되지 않은 행: " + string.Join(", ", rows.Where(row => !row.OrderDate.HasValue).Select(row => row.Number)));

            SetComponentAmounts(rows);

            DateTime basisDate = rows.Min(row => row.OrderDate.Value.Date);
            // 같은 이름의 파일은 기존 등록을 수정하거나 삭제 후 재등록
            string fileKey = Path.GetFileName(fileName);
            using (var existing = Query(@"
                SELECT FileId,
                       BasisDate
                FROM BM_EdiImportFile
                WHERE CorpId = @Corp
                       AND FileKey = @FileKey;
            ",
                "@FileKey", fileKey))
            {
                if (existing.Rows.Count > 0)
                {
                    if (Convert.ToDateTime(existing.Rows[0]["BasisDate"]).Date != basisDate.Date)
                        throw new InvalidOperationException("이미 " + Convert.ToDateTime(existing.Rows[0]["BasisDate"]).ToString("yyyy-MM-dd") + " EDI 등록일로 등록된 파일입니다.");
                    throw new InvalidOperationException("이미 등록된 파일명입니다. 기존 등록을 확인하거나 삭제 후 다시 업로드해 주세요.");
                }
            }

            using (var connection = Open())
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                long fileId;
                using (var command = Command(connection, transaction, @"
                    IF EXISTS (SELECT 1
                               FROM BM_EdiImportFile WITH (UPDLOCK, HOLDLOCK)
                               WHERE CorpId = @Corp
                                      AND FileKey = @FileKey)
                        SELECT FileId
                        FROM BM_EdiImportFile
                        WHERE CorpId = @Corp
                               AND FileKey = @FileKey;
                    ELSE
                        BEGIN
                            INSERT BM_EdiImportFile (CorpId, FileKey, FileName, BasisDate, Status, ErrorMessage, CreId)
                            VALUES (@Corp, @FileKey, @Name, @Date, N'확인필요', @Error, @User);
                            SELECT CAST (SCOPE_IDENTITY() AS BIGINT);
                        END
                ",
                    "@FileKey", fileKey,
                    "@Name", Path.GetFileName(fileName),
                    "@Date", basisDate.Date,
                    "@Error", ""))
                    fileId = Convert.ToInt64(command.ExecuteScalar());
                using (var command = Command(connection, transaction, @"
                    SELECT COUNT(*)
                    FROM BM_EdiImportRow
                    WHERE FileId = @File;
                ",
                    "@File", fileId))
                    if (Convert.ToInt32(command.ExecuteScalar()) > 0)
                    {
                        transaction.Commit();
                        return fileId;
                    }

                var occurrences = new Dictionary<string, int>();
                foreach (var row in rows)
                {
                    string sourceKey = SourceKey(row);
                    // 각 값의 길이를 함께 저장해 구분자가 들어간 주문번호도 충돌 방지
                    string identity = string.Join("|", new[] { row.Customer, row.OrderNo, row.Center, row.ShipTo, sourceKey }
                        .Select(value => (value ?? "").Length + ":" + (value ?? "")));
                    // 주문번호가 없으면 파일명으로 구분, 출고일 변경은 주문 식별에 영향 없음
                    if (string.IsNullOrWhiteSpace(row.OrderNo))
                        identity += "|" + fileKey.Length + ":" + fileKey;
                    int occurrence;
                    occurrences.TryGetValue(identity, out occurrence);
                    occurrences[identity] = ++occurrence;
                    string rowKey = identity + "|" + occurrence;

                    // 인덱스 길이를 넘긴 식별값은 잘라 저장하지 않고 원본 확인
                    if (sourceKey.Length > 700 || rowKey.Length > 700)
                        throw new InvalidOperationException("상품 옵션 또는 주문 식별값이 너무 깁니다. 원본 행: " + row.Number);
                    using (var command = Command(connection, transaction, @"
                        INSERT BM_EdiImportRow (FileId, CorpId, SourceSheet, SourceRow, CustCode, SourceCode, SourceName, SourceOption, SourceKey, RowKey, OrderNo, CenterName, ShipToName, OrderDate, DeliveryDate, Qty, Amount, ErrorMessage)
                        VALUES (@File, @Corp, @Sheet, @Row, @Customer, @Code, @Name, @Option, @Key, @RowKey, @Order, @Center, @Ship, @OrderDate, @Delivery, @Qty, @Amount, @Error);
                    ",
                        "@File", fileId,
                        "@Sheet", row.Sheet,
                        "@Row", row.Number,
                        "@Customer", row.Customer,
                        "@Code", row.SourceCode,
                        "@Name", row.SourceName,
                        "@Option", row.Option,
                        "@Key", sourceKey,
                        "@RowKey", rowKey,
                        "@Order", row.OrderNo,
                        "@Center", row.Center,
                        "@Ship", row.ShipTo,
                        "@OrderDate", row.OrderDate,
                        "@Delivery", row.DeliveryDate,
                        "@Qty", row.Qty,
                        "@Amount", row.Amount,
                        "@Error", row.Error))
                        command.ExecuteNonQuery();
                }

                // 저장된 연결·거래처 품목·품목정보 순으로 업로드 직후 연결
                using (var command = Command(connection, transaction, ItemConnectionSql,
                    "@SelectedRows", null, "@File", fileId))
                    command.ExecuteNonQuery();

                transaction.Commit();
                return fileId;
            }
        }

        /// <summary>상세 추가·수정 저장과 기존 반영 수량 갱신</summary>
        internal static void SaveMappings(DataTable rows)
        {
            using (var connection = Open())
            using (var transaction = connection.BeginTransaction())
            {
                var savedIds = new Dictionary<DataRow, long>();

                foreach (DataRow row in rows.Rows)
                {
                    if (row.RowState != DataRowState.Modified && row.RowState != DataRowState.Added)
                        continue;

                    bool added = row.RowState == DataRowState.Added;
                    DataRowVersion original = added ? DataRowVersion.Current : DataRowVersion.Original;

                    using (var command = Command(connection, transaction, @"
                        IF NOT EXISTS (SELECT 1
                                       FROM CM_Cust
                                       WHERE CorpId = @Corp
                                              AND CustCode = @Customer
                                              AND UseYn = 'Y'
                                              AND CustType IN ('B2B', 'B2C'))
                            THROW 50001, N'사용 중인 B2B·B2C 거래처를 선택해 주세요.', 1;

                        IF NOT EXISTS (SELECT 1
                                       FROM CM_Item
                                       WHERE CorpId = @Corp
                                              AND ItemCode = @Item
                                              AND UseYn = 'Y')
                            THROW 50001, N'사용 중인 품목을 선택해 주세요.', 1;

                        IF @Item IN ('9DL00000001','9DL00000002','9DL00000003')
                        BEGIN
                            SET @Qty=0; SET @Factor=1; SET @AmountFromPrice=0;
                            IF @Amount IS NULL THROW 50001,N'배송료 금액을 입력해 주세요.',1;
                        END;

                        IF @Factor IS NULL
                           OR @Factor <= 0
                            THROW 50001, N'환산수량은 0보다 커야 합니다.', 1;

                        IF @Qty IS NULL
                           OR @Qty < 0
                           OR @Qty * @Factor <> FLOOR(@Qty * @Factor)
                           OR @Qty * @Factor > 2147483647
                            THROW 50001, N'발주수량과 환산수량을 확인해 주세요.', 1;

                        DECLARE @LockResult AS INT;

                        EXECUTE @LockResult = sys.sp_getapplock @Resource = @Corp, @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = 30000;

                        IF @LockResult < 0
                            THROW 50001, N'다른 업로드 처리 중입니다.', 1;

                        IF @Added=1
                        BEGIN
                            IF NOT EXISTS(SELECT 1 FROM BM_EdiImportFile WHERE CorpId=@Corp AND FileId=@File)
                                THROW 50001, N'등록 건이 삭제되었습니다. 다시 조회해 주세요.', 1;

                            INSERT BM_EdiImportRow
                                (FileId,CorpId,SourceSheet,SourceRow,CustCode,ItemCode,SourceCode,SourceName,
                                 SourceOption,SourceKey,RowKey,OrderNo,CenterName,ShipToName,
                                 OrderDate,Qty,Factor,Amount,Applied,ModId,ModDt)
                            SELECT @File,@Corp,N'직접 등록',
                                ISNULL((SELECT MAX(SourceRow) FROM BM_EdiImportRow WHERE FileId=@File),0)+1,
                                @Customer,@Item,'','','',@Key,@Key,'','','',
                                @NewDate,@Qty,@Factor,@Amount,0,@User,SYSDATETIME();
                            SELECT CAST(SCOPE_IDENTITY() AS bigint);
                            RETURN;
                        END

                        IF NOT EXISTS (SELECT 1
                                       FROM BM_EdiImportRow WITH (UPDLOCK, HOLDLOCK)
                                       WHERE CorpId = @Corp
                                              AND RowId = @Row
                                              AND Applied = @Applied
                                              AND (CustCode = @OldCustomer
                                                   OR (CustCode IS NULL
                                                       AND @OldCustomer IS NULL))
                                              AND (ItemCode = @OldItem
                                                   OR (ItemCode IS NULL
                                                       AND @OldItem IS NULL))
                                              AND (Qty = @OldQty
                                                   OR (Qty IS NULL
                                                       AND @OldQty IS NULL))
                                              AND Factor = @OldFactor
                                              AND (Amount = @OldAmount
                                                   OR (Amount IS NULL
                                                       AND @OldAmount IS NULL)))
                            THROW 50001, N'다른 사용자가 변경한 행입니다. 다시 조회해 주세요.', 1;

                        -- 기존 연결과 변경 연결을 같은 트랜잭션에서 재집계
                        IF @Applied = 1
                        BEGIN
                            DECLARE @Date date;
                            SELECT @Date=OrderDate FROM BM_EdiImportRow
                            WHERE CorpId=@Corp AND RowId=@Row;

                            DECLARE @Changed TABLE (
                                CustCode nvarchar(20), ItemCode nvarchar(200), Qty decimal(38,4),
                                PRIMARY KEY(CustCode,ItemCode));
                            INSERT @Changed(CustCode,ItemCode)
                            SELECT @OldCustomer,@OldItem WHERE @OldItem NOT IN ('9DL00000001','9DL00000002','9DL00000003')
                            UNION SELECT @Customer,@Item WHERE @Item NOT IN ('9DL00000001','9DL00000002','9DL00000003');

                            -- 포케스팅에서 별도로 수정한 집계는 덮어쓰지 않음
                            IF EXISTS (
                                SELECT 1 FROM @Changed k
                                JOIN BM_EdiForecastBackup b ON b.CorpId=@Corp AND b.CustCode=k.CustCode
                                    AND b.ItemCode=k.ItemCode AND b.BasisDate=@Date
                                LEFT JOIN PM_ForecastOrderActual a ON a.CorpId=b.CorpId AND a.CustCode=b.CustCode
                                    AND a.ItemCode=b.ItemCode AND a.OrderDate=b.BasisDate
                                WHERE a.OrderQty IS NULL OR a.OrderQty<>b.LastQty)
                                THROW 50001,N'포케스팅 수량이 변경되어 자동 수정할 수 없습니다. 기존 집계를 확인해 주세요.',1;

                            IF @OldItem NOT IN ('9DL00000001','9DL00000002','9DL00000003') AND NOT EXISTS (SELECT 1 FROM BM_EdiForecastBackup
                                WHERE CorpId=@Corp AND CustCode=@OldCustomer AND ItemCode=@OldItem AND BasisDate=@Date)
                                THROW 50001,N'기존 반영 이력이 없습니다. 다시 조회해 주세요.',1;

                            UPDATE k SET Qty=ISNULL((
                                SELECT SUM(CAST(r.Qty*r.Factor AS decimal(38,4)))
                                FROM BM_EdiImportRow r
                                WHERE r.CorpId=@Corp AND r.CustCode=k.CustCode AND r.ItemCode=k.ItemCode
                                  AND r.OrderDate=@Date AND r.Applied=1 AND r.RowId<>@Row),0)
                                + CASE WHEN k.CustCode=@Customer AND k.ItemCode=@Item THEN @Qty*@Factor ELSE 0 END
                            FROM @Changed k;

                            IF EXISTS(SELECT 1 FROM @Changed WHERE Qty<0 OR Qty>2147483647 OR Qty<>FLOOR(Qty))
                                THROW 50001,N'합계 수량이 저장 범위를 벗어났습니다.',1;

                            DECLARE @Totals TABLE (
                                CustCode nvarchar(20), ItemCode nvarchar(200), BasisDate date, Qty int);
                            INSERT @Totals
                            SELECT CustCode, ItemCode, @Date, CAST(Qty AS int) FROM @Changed;
                        " + ForecastQuantitySql + @"
                            UPDATE a SET ChannelName=c.CustName, ItemName=i.ItemName
                            FROM PM_ForecastOrderActual a
                            INNER JOIN @Changed k ON k.CustCode=a.CustCode AND k.ItemCode=a.ItemCode
                            INNER JOIN CM_Cust c ON c.CorpId=a.CorpId AND c.CustCode=a.CustCode
                            INNER JOIN CM_Item i ON i.CorpId=a.CorpId AND i.ItemCode=a.ItemCode
                            WHERE a.CorpId=@Corp AND a.OrderDate=@Date;

                            INSERT CM_ItemPrice(CorpId,CustCode,ItemCode,PriceType,UseYn,CreId,CreDt)
                            SELECT @Corp,@Customer,@Item,'20','Y',@User,GETDATE()
                            WHERE NOT EXISTS(SELECT 1 FROM CM_ItemPrice WHERE CorpId=@Corp AND CustCode=@Customer
                                AND ItemCode=@Item AND PriceType='20');
                        END

                        UPDATE BM_EdiImportRow
                        SET CustCode = @Customer,
                               ItemCode = @Item,
                               Factor = @Factor,
                               Qty = @Qty,
                               Amount = @Amount,
                               AmountFromPrice = @AmountFromPrice,
                               ModId = @User,
                               ModDt = SYSDATETIME()
                        WHERE CorpId = @Corp
                               AND RowId = @Row;

                        IF @@ROWCOUNT <> 1
                            THROW 50001, N'다른 사용자가 반영한 행입니다. 다시 조회해 주세요.', 1;

                        DELETE BM_EdiItemMap
                        WHERE CorpId = @Corp
                               AND CustCode = @Customer
                               AND SourceKey = @Key;

                        INSERT BM_EdiItemMap (CorpId, CustCode, SourceKey, SourceCode, SourceName, ItemCode, Factor, ModId)
                        VALUES (@Corp, @Customer, @Key, @Code, @Name, @Item, @Factor, @User);
                        SELECT @Row;
                    ",
                        "@Added", added,
                        "@File", row["FileId"],
                        "@NewDate", row["OrderDate"],
                        "@Customer", row["CustCode"],
                        "@Item", row["ItemCode"],
                        "@Factor", row["Factor"],
                        "@Qty", row["Qty"],
                        "@Amount", row["Amount"],
                        "@AmountFromPrice", row["AmountFromPrice"] == DBNull.Value ? false : row["AmountFromPrice"],
                        "@Applied", row["Applied", original],
                        "@OldCustomer", row["CustCode", original],
                        "@OldItem", row["ItemCode", original],
                        "@OldQty", row["Qty", original],
                        "@OldFactor", row["Factor", original],
                        "@OldAmount", row.Table.Columns.Contains("StoredAmount") ? row["StoredAmount", original] : row["Amount", original],
                        "@Row", row["RowId"],
                        "@Key", row["SourceKey"],
                        "@Code", row["SourceCode"],
                        "@Name", row["SourceName"]))
                        savedIds[row] = Convert.ToInt64(command.ExecuteScalar());
                }

                transaction.Commit();
                foreach (var saved in savedIds)
                {
                    saved.Key["RowId"] = saved.Value;

                    // 조회 시 계산한 금액과 DB 원본을 분리해 동시 수정 검증 유지
                    if (rows.Columns.Contains("StoredAmount"))
                        saved.Key["StoredAmount"] = saved.Key["Amount"];
                }
                rows.AcceptChanges();
            }
        }

        /// <summary>포케스팅 반영과 오류 상태 기록</summary>
        internal static string Apply(long fileId, long[] rowIds = null)
        {
            try
            {
                return ApplyCore(fileId, rowIds);
            }
            catch (Exception ex)
            {
                using (var connection = Open())
                    SetFileError(connection, null, fileId, ex.Message);

                return ex.Message;
            }
        }

        /// <summary>품목 연결·수량 검증·포케스팅 반영 트랜잭션</summary>
        private static string ApplyCore(long fileId, long[] rowIds)
        {
            using (var connection = Open())
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                // 선택한 품목만 검증·반영, 기존 반영분의 전체 집계는 유지
                using (var command = Command(connection, transaction, @"
                    SELECT COUNT(*)
                    FROM BM_EdiImportRow
                    WHERE CorpId = @Corp
                           AND FileId = @File;
                ",
                    "@SelectedRows", rowIds == null ? null : string.Join(",", rowIds), "@File", fileId))
                    if (Convert.ToInt32(command.ExecuteScalar()) == 0)
                        throw new InvalidOperationException("읽을 수 있는 상세 행이 없습니다. 파일 양식을 확인해 주세요.");
                // 다른 업로드와 같은 날짜 집계가 겹쳐도 한 트랜잭션 안에서 중복 검사와 수량 갱신
                using (var command = Command(connection, transaction, @"
                    DECLARE @LockResult AS INT;

                    EXECUTE @LockResult = sys.sp_getapplock @Resource = @Corp, @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = 30000;

                    IF @LockResult < 0
                        THROW 50001, N'업로드 처리 중입니다. 잠시 후 다시 시도해 주세요.', 1;

                " + ItemConnectionSql + @"
                ",
                    "@SelectedRows", rowIds == null ? null : string.Join(",", rowIds), "@File", fileId))
                    command.ExecuteNonQuery();
                // 원본 금액은 유지, 미입력 금액만 유효한 거래처 품목단가로 계산
                using (var command = Command(connection, transaction, @"
                    UPDATE r
                    SET Amount = ROUND(r.Qty * r.Factor * COALESCE(p.Price, NULLIF(i.Amount, 0)), 0),
                        AmountFromPrice = 1
                    FROM BM_EdiImportRow r
                    LEFT JOIN CM_Item i ON i.CorpId=r.CorpId AND i.ItemCode=r.ItemCode
                    OUTER APPLY
                    (
                        SELECT MIN(v.Price) AS Price
                        FROM
                        (
                            SELECT TOP (1) WITH TIES Price
                            FROM CM_ItemPrice
                            WHERE CorpId = r.CorpId AND CustCode = r.CustCode
                                AND ItemCode = r.ItemCode AND PriceType = '20'
                                AND ISNULL(UseYn, 'Y') = 'Y'
                                AND (ApplyDate IS NULL OR ApplyDate <= r.OrderDate)
                                AND (EndDate IS NULL OR EndDate >= r.OrderDate)
                            ORDER BY ApplyDate DESC
                        ) v
                        HAVING COUNT(DISTINCT v.Price) = 1 AND MIN(v.Price) > 0
                    ) p
                    WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.CorpId = @Corp AND r.FileId = @File
                        AND r.ItemCode NOT IN ('9DL00000001','9DL00000002','9DL00000003')
                        AND (r.Amount IS NULL OR r.AmountFromPrice = 1);
                ", "@SelectedRows", rowIds == null ? null : string.Join(",", rowIds), "@File", fileId))
                    command.ExecuteNonQuery();

                using (var command = Command(connection, transaction, @"
                    -- 배송료는 금액만 저장하고 수량 집계에서 제외
                    UPDATE BM_EdiImportRow SET Qty=0,Factor=1,AmountFromPrice=0
                    WHERE CorpId=@Corp AND FileId=@File AND Applied=0
                        AND ItemCode IN ('9DL00000001','9DL00000002','9DL00000003')
                        AND (@SelectedRows IS NULL OR RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ',')));
                    IF EXISTS(SELECT 1 FROM BM_EdiImportRow WHERE CorpId=@Corp AND FileId=@File AND Applied=0
                        AND ItemCode IN ('9DL00000001','9DL00000002','9DL00000003') AND Amount IS NULL
                        AND (@SelectedRows IS NULL OR RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))))
                        THROW 50001,N'배송료 금액을 입력해 주세요.',1;
                    SELECT COUNT(*)
                    FROM BM_EdiImportRow AS r
                    WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.CorpId = @Corp
                           AND r.FileId = @File
                           AND r.Applied = 0
                           AND (NULLIF (r.ErrorMessage, '') IS NOT NULL
                                OR r.Qty IS NULL
                                OR r.Qty < 0
                                OR r.Factor <= 0
                                OR r.Qty * r.Factor <> FLOOR(r.Qty * r.Factor)
                                OR r.Qty * r.Factor > 2147483647
                                OR NOT EXISTS (SELECT 1
                                               FROM CM_Cust AS c
                                               WHERE c.CorpId = r.CorpId
                                                      AND c.CustCode = r.CustCode
                                                      AND c.UseYn = 'Y'
                                                      AND c.CustType IN ('B2B', 'B2C'))
                                OR NOT EXISTS (SELECT 1
                                               FROM CM_Item AS i
                                               WHERE i.CorpId = r.CorpId
                                                      AND i.ItemCode = r.ItemCode
                                                      AND i.UseYn = 'Y'));
                ",
                    "@SelectedRows", rowIds == null ? null : string.Join(",", rowIds), "@File", fileId))
                {
                    int errors = Convert.ToInt32(command.ExecuteScalar());
                    if (errors > 0)
                    {
                        string message = errors + "행의 거래처·품목·수량 확인 필요 (파일 전체 반영 대기)";
                        SetFileError(connection, transaction, fileId, message);
                        transaction.Commit();
                        return message;
                    }
                }

                using (var command = Command(connection, transaction, @"
                    IF EXISTS (SELECT 1
                               FROM BM_EdiImportRow AS r
                                      INNER JOIN BM_EdiImportRow AS old
                                      ON old.CorpId = r.CorpId
                                         AND old.RowKey = r.RowKey
                                         AND old.Applied = 1
                                         AND old.RowId <> r.RowId
                               WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.FileId = @File
                                      AND r.CorpId = @Corp
                                      AND (old.Qty <> r.Qty
                                           OR old.Factor <> r.Factor
                                           OR old.ItemCode <> r.ItemCode
                                           OR old.CustCode <> r.CustCode
                                           OR old.OrderDate <> r.OrderDate))
                        THROW 50001, N'기존 주문과 수량·품목 또는 출고일이 다릅니다. 기존 업로드를 확인해 주세요.', 1;

                    DECLARE @Keys TABLE (
                        CustCode  NVARCHAR (20) ,
                        ItemCode  NVARCHAR (200),
                        BasisDate DATE          ,
                        PRIMARY KEY (CustCode, ItemCode, BasisDate));

                    INSERT @Keys
                    SELECT DISTINCT r.CustCode,
                                    r.ItemCode,
                                    r.OrderDate
                    FROM BM_EdiImportRow AS r
                           INNER JOIN BM_EdiImportFile AS f
                           ON f.FileId = r.FileId
                    WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.FileId = @File
                           AND r.CorpId = @Corp
                           AND r.Applied = 0
                           AND r.ItemCode NOT IN ('9DL00000001','9DL00000002','9DL00000003')
                           AND NOT EXISTS (SELECT 1
                                           FROM BM_EdiImportRow AS old
                                           WHERE old.CorpId = r.CorpId
                                                  AND old.RowKey = r.RowKey
                                                  AND old.Applied = 1);

                    IF EXISTS (SELECT 1
                               FROM @Keys AS k
                                      INNER JOIN BM_EdiForecastBackup AS b
                                      ON b.CorpId = @Corp
                                         AND b.CustCode = k.CustCode
                                         AND b.ItemCode = k.ItemCode
                                         AND b.BasisDate = k.BasisDate
                                      LEFT OUTER JOIN PM_ForecastOrderActual AS a
                                      ON a.CorpId = @Corp
                                         AND a.CustCode = k.CustCode
                                         AND a.ItemCode = k.ItemCode
                                         AND a.OrderDate = k.BasisDate
                               WHERE a.OrderQty IS NULL
                                      OR a.OrderQty <> b.LastQty)
                        THROW 50001, N'포케스팅에서 수동 수정된 수량이 있습니다. 업로드 반영을 중단했습니다.', 1;

                    UPDATE r
                    SET Applied = 1
                    FROM BM_EdiImportRow AS r
                    WHERE (@SelectedRows IS NULL OR r.RowId IN (SELECT CONVERT(bigint,value) FROM STRING_SPLIT(@SelectedRows, ','))) AND r.FileId = @File
                           AND r.CorpId = @Corp
                           AND r.Applied = 0
                           AND NOT EXISTS (SELECT 1
                                           FROM BM_EdiImportRow AS old
                                           WHERE old.CorpId = r.CorpId
                                                  AND old.RowKey = r.RowKey
                                                  AND old.Applied = 1);

                    INSERT CM_ItemPrice (CorpId, CustCode, ItemCode, PriceType, UseYn, CreId, CreDt)
                    SELECT DISTINCT @Corp,
                                    k.CustCode,
                                    k.ItemCode,
                                    '20',
                                    'Y',
                                    @User,
                                    GETDATE()
                    FROM @Keys AS k
                    WHERE NOT EXISTS (SELECT 1
                                       FROM CM_ItemPrice AS p
                                       WHERE p.CorpId = @Corp
                                              AND p.CustCode = k.CustCode
                                              AND p.ItemCode = k.ItemCode
                                              AND p.PriceType = '20');

                    DECLARE @Totals TABLE (
                        CustCode  NVARCHAR (20) ,
                        ItemCode  NVARCHAR (200),
                        BasisDate DATE          ,
                        Qty       INT           );

                    INSERT @Totals
                    SELECT   k.CustCode,
                             k.ItemCode,
                             k.BasisDate,
                             CAST (SUM(r.Qty * r.Factor) AS INT)
                    FROM @Keys AS k
                             INNER JOIN BM_EdiImportFile AS f
                             ON f.CorpId = @Corp
                             INNER JOIN BM_EdiImportRow AS r
                             ON r.FileId = f.FileId
                                AND r.OrderDate = k.BasisDate
                                AND r.CustCode = k.CustCode
                                AND r.ItemCode = k.ItemCode
                                AND r.Applied = 1
                    GROUP BY k.CustCode, k.ItemCode, k.BasisDate;

                " + ForecastQuantitySql + @"
                    -- 반영한 거래처를 공통 포케스팅 목록에 표시
                    UPDATE c SET ForecastUseYn='Y',ModId=@User,ModDt=GETDATE()
                    FROM CM_Cust c
                    WHERE c.CorpId=@Corp
                        AND EXISTS(SELECT 1 FROM @Keys k WHERE k.CustCode=c.CustCode);
                    UPDATE BM_EdiImportFile
                    SET Status = CASE WHEN EXISTS (SELECT 1 FROM BM_EdiImportRow WHERE CorpId=@Corp AND FileId=@File AND Applied=0)
                        THEN N'일부 반영' ELSE N'반영완료' END,
                           ErrorMessage = NULL
                    WHERE FileId = @File
                           AND CorpId = @Corp;
                ",
                    "@SelectedRows", rowIds == null ? null : string.Join(",", rowIds), "@File", fileId))
                    command.ExecuteNonQuery();
                transaction.Commit();
                return "반영완료";
            }
        }
        /// <summary>등록 건의 확인 필요 상태와 오류 내용 기록</summary>
        private static void SetFileError(SqlConnection connection, SqlTransaction transaction, long fileId, string message)
        {
            using (var command = Command(connection, transaction, @"
                UPDATE BM_EdiImportFile
                SET Status=N'확인필요', ErrorMessage=@Error
                WHERE CorpId=@Corp AND FileId=@File;
            ", "@File", fileId, "@Error", message.Length > 1000 ? message.Substring(0, 1000) : message))
                command.ExecuteNonQuery();
        }

        // 호출 쿼리의 @Totals(거래처·품목·날짜·수량)를 같은 트랜잭션에서 반영
        // 최초 수량은 PreviousQty로 보관, 이후 수정은 LastQty만 갱신
        private const string ForecastQuantitySql = @"
            INSERT BM_EdiForecastBackup(CorpId, CustCode, ItemCode, BasisDate, PreviousQty, LastQty)
            SELECT @Corp, t.CustCode, t.ItemCode, t.BasisDate, a.OrderQty, t.Qty
            FROM @Totals t
            LEFT JOIN PM_ForecastOrderActual a ON a.CorpId=@Corp AND a.CustCode=t.CustCode
                AND a.ItemCode=t.ItemCode AND a.OrderDate=t.BasisDate
            WHERE NOT EXISTS (
                SELECT 1 FROM BM_EdiForecastBackup b WHERE b.CorpId=@Corp
                    AND b.CustCode=t.CustCode AND b.ItemCode=t.ItemCode AND b.BasisDate=t.BasisDate);

            UPDATE a SET OrderQty=t.Qty
            FROM PM_ForecastOrderActual a
            INNER JOIN @Totals t ON t.CustCode=a.CustCode AND t.ItemCode=a.ItemCode AND t.BasisDate=a.OrderDate
            WHERE a.CorpId=@Corp;

            INSERT PM_ForecastOrderActual(CorpId, CustCode, ItemCode, ChannelName, ItemName, OrderDate, OrderQty)
            SELECT @Corp, t.CustCode, t.ItemCode, c.CustName, i.ItemName, t.BasisDate, t.Qty
            FROM @Totals t
            INNER JOIN CM_Cust c ON c.CorpId=@Corp AND c.CustCode=t.CustCode
            INNER JOIN CM_Item i ON i.CorpId=@Corp AND i.ItemCode=t.ItemCode
            WHERE NOT EXISTS (
                SELECT 1 FROM PM_ForecastOrderActual a WHERE a.CorpId=@Corp
                    AND a.CustCode=t.CustCode AND a.ItemCode=t.ItemCode AND a.OrderDate=t.BasisDate);

            UPDATE b SET LastQty=t.Qty, ModDt=SYSDATETIME()
            FROM BM_EdiForecastBackup b
            INNER JOIN @Totals t ON b.CustCode=t.CustCode AND b.ItemCode=t.ItemCode AND b.BasisDate=t.BasisDate
            WHERE b.CorpId=@Corp;
        ";
    }
}
