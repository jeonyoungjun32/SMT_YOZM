USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRAN;
IF COL_LENGTH('dbo.CM_Cust','ForecastUseYn') IS NULL
    ALTER TABLE dbo.CM_Cust ADD ForecastUseYn char(1) NOT NULL CONSTRAINT DF_CM_Cust_ForecastUseYn DEFAULT ('Y') WITH VALUES;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_Cust') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_Cust'),'ForecastUseYn','ColumnId') AND name='MS_Description')
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'포케스팅 사용여부', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CM_Cust', @level2type=N'COLUMN', @level2name=N'ForecastUseYn';
UPDATE dbo.CM_ItemPrice SET UseYn='Y' WHERE UseYn IS NULL;
IF NOT EXISTS (SELECT 1 FROM sys.default_constraints WHERE parent_object_id=OBJECT_ID('dbo.CM_ItemPrice') AND parent_column_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'UseYn','ColumnId'))
    ALTER TABLE dbo.CM_ItemPrice ADD CONSTRAINT DF_CM_ItemPrice_UseYn DEFAULT ('Y') FOR UseYn;
COMMIT;
