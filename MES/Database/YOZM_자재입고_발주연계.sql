IF DB_NAME() <> N'YOZM' THROW 50001, N'YOZM DB에서 실행해 주세요.', 1;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

-- 발주에서 불러온 소수 단가를 입고 상세에도 그대로 보관합니다.
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.MM_PurchaseD')
    AND name = N'UnitPrice' AND precision = 18 AND scale = 0 AND is_nullable = 1)
    ALTER TABLE dbo.MM_PurchaseD ALTER COLUMN UnitPrice decimal(22,4) NULL;

COMMIT;
