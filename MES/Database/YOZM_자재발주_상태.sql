IF DB_NAME() <> N'YOZM' THROW 50001, N'YOZM DB에서 실행해 주세요.', 1;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

EXEC(N'
CREATE OR ALTER PROCEDURE dbo.Placing_UpdateInQtyByPurchase
    @pCorpId nvarchar(10), @pStoNo nvarchar(20), @pExcludeReceipt bit = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT MmOrderNo INTO #AffectedOrders
    FROM dbo.MM_PurchaseD
    WHERE CorpId = @pCorpId AND StoNo = @pStoNo AND MmOrderNo IS NOT NULL;

    UPDATE D
    SET InQty = R.Qty,
        BalanceQty = CASE WHEN D.Qty > R.Qty THEN D.Qty - R.Qty ELSE 0 END,
        State = CASE WHEN D.Qty > 0 AND R.Qty >= D.Qty THEN N''입고'' ELSE N''발주'' END
    FROM dbo.MM_PlacingD D
    JOIN #AffectedOrders A ON A.MmOrderNo = D.MmOrderNo
    CROSS APPLY (
        SELECT ISNULL(SUM(P.Qty), 0) AS Qty
        FROM dbo.MM_PurchaseD P
        WHERE P.CorpId = D.CorpId AND P.MmOrderNo = D.MmOrderNo AND P.MmOrderSeq = D.MmOrderSeq
          AND (@pExcludeReceipt = 0 OR P.StoNo <> @pStoNo)
    ) R
    WHERE D.CorpId = @pCorpId;

    UPDATE M
    SET State = CASE WHEN EXISTS (
        SELECT 1 FROM dbo.MM_PlacingD D
        WHERE D.CorpId = M.CorpId AND D.MmOrderNo = M.MmOrderNo
          AND D.State = N''발주''
    ) THEN N''발주'' ELSE N''입고'' END
    FROM dbo.MM_PlacingM M
    JOIN #AffectedOrders A ON A.MmOrderNo = M.MmOrderNo
    WHERE M.CorpId = @pCorpId;
END;
');

UPDATE dbo.MM_PlacingD
SET State = CASE WHEN Qty > 0 AND ISNULL(InQty, 0) >= Qty THEN N'입고' ELSE N'발주' END
WHERE State IS NULL OR State NOT IN (N'발주', N'입고');

UPDATE M SET State = CASE
    WHEN EXISTS (SELECT 1 FROM dbo.MM_PlacingD D WHERE D.CorpId=M.CorpId AND D.MmOrderNo=M.MmOrderNo)
      AND NOT EXISTS (SELECT 1 FROM dbo.MM_PlacingD D WHERE D.CorpId=M.CorpId AND D.MmOrderNo=M.MmOrderNo AND D.State=N'발주')
    THEN N'입고' ELSE N'발주' END
FROM dbo.MM_PlacingM M
WHERE M.State IS NULL OR M.State NOT IN (N'발주', N'입고');

COMMIT;
