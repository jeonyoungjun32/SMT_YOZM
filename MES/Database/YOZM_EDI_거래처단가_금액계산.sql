USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.BM_EdiImportRow', 'AmountFromPrice') IS NULL
    ALTER TABLE dbo.BM_EdiImportRow ADD AmountFromPrice bit NOT NULL
        CONSTRAINT DF_BM_EdiImportRow_AmountFromPrice DEFAULT (0);
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
    WHERE major_id=OBJECT_ID('dbo.BM_EdiImportRow')
      AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.BM_EdiImportRow'),'AmountFromPrice','ColumnId')
      AND name='MS_Description')
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'거래처 단가 자동계산 여부',
        @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BM_EdiImportRow',
        @level2type=N'COLUMN', @level2name=N'AmountFromPrice';
COMMIT;
