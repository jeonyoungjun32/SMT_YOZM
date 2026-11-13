IF DB_NAME() <> N'YOZM' THROW 50001, N'YOZM DB에서 실행해 주세요.', 1;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF COL_LENGTH(N'dbo.MM_PlacingD', N'ReceiveHouseId') IS NULL
BEGIN
    ALTER TABLE dbo.MM_PlacingD ADD ReceiveHouseId nvarchar(20) NULL;
    EXEC sys.sp_addextendedproperty N'MS_Description', N'예정 입고창고 코드',
        N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingD', N'COLUMN', N'ReceiveHouseId';
END;

IF COL_LENGTH(N'dbo.MM_PlacingD', N'MrpNo') IS NULL
BEGIN
    ALTER TABLE dbo.MM_PlacingD ADD MrpNo nvarchar(20) NULL;
    EXEC sys.sp_addextendedproperty N'MS_Description', N'MRP 참조번호',
        N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingD', N'COLUMN', N'MrpNo';
END;

IF COL_LENGTH(N'dbo.MM_PlacingD', N'BomChecked') IS NULL
BEGIN
    ALTER TABLE dbo.MM_PlacingD ADD BomChecked bit NOT NULL CONSTRAINT DF_MM_PlacingD_BomChecked DEFAULT (0);
    EXEC sys.sp_addextendedproperty N'MS_Description', N'BOM 체크 여부',
        N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingD', N'COLUMN', N'BomChecked';
END;

IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'dbo.MM_PlacingD')
    AND name = N'UnitPrice' AND precision = 18 AND scale = 0 AND is_nullable = 1)
    ALTER TABLE dbo.MM_PlacingD ALTER COLUMN UnitPrice decimal(22,4) NULL;

COMMIT;
