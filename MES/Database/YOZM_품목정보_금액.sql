USE [YOZM];
GO
IF COL_LENGTH(N'dbo.CM_Item', N'Amount') IS NULL
    ALTER TABLE dbo.CM_Item ADD Amount decimal(19,4) NULL;
GO
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID(N'dbo.CM_Item') AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.CM_Item'),N'Amount','ColumnId') AND name=N'MS_Description')
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'금액(품목 1개 기준)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CM_Item', @level2type=N'COLUMN', @level2name=N'Amount';