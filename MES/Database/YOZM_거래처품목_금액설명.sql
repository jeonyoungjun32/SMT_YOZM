USE [YOZM];
GO
-- 기존 Price 컬럼 사용: 품목 1개 기준 금액, 엑셀 원본 금액 우선
IF EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID(N'dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.CM_ItemPrice'),N'Price','ColumnId') AND name=N'MS_Description')
    EXEC sys.sp_updateextendedproperty @name=N'MS_Description', @value=N'금액(품목 1개 기준)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CM_ItemPrice', @level2type=N'COLUMN', @level2name=N'Price';
ELSE
    EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'금액(품목 1개 기준)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CM_ItemPrice', @level2type=N'COLUMN', @level2name=N'Price';