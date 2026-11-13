USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF COL_LENGTH('dbo.CM_ItemPrice','SalesItemSourceId') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [SalesItemSourceId] bigint NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'SalesItemSourceId','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'판매품목 원본번호',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'SalesItemSourceId';
IF COL_LENGTH('dbo.CM_ItemPrice','ChannelName') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [ChannelName] nvarchar(100) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'ChannelName','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'판매채널명',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'ChannelName';
IF COL_LENGTH('dbo.CM_ItemPrice','ForecastItemName') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [ForecastItemName] nvarchar(200) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'ForecastItemName','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'포케스팅 품명',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'ForecastItemName';
IF COL_LENGTH('dbo.CM_ItemPrice','SourceShipToCode') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [SourceShipToCode] nvarchar(30) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'SourceShipToCode','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'원본 납품처코드',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'SourceShipToCode';
IF COL_LENGTH('dbo.CM_ItemPrice','SourceShipToName') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [SourceShipToName] nvarchar(200) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'SourceShipToName','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'원본 납품처명',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'SourceShipToName';
IF COL_LENGTH('dbo.CM_ItemPrice','SourceSoldToCode') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [SourceSoldToCode] nvarchar(30) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'SourceSoldToCode','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'원본 매출처코드',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'SourceSoldToCode';
IF COL_LENGTH('dbo.CM_ItemPrice','SourceSoldToName') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [SourceSoldToName] nvarchar(200) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'SourceSoldToName','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'원본 매출처명',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'SourceSoldToName';
IF COL_LENGTH('dbo.CM_ItemPrice','SalesUnit') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [SalesUnit] nvarchar(20) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'SalesUnit','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'판매단위',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'SalesUnit';
IF COL_LENGTH('dbo.CM_ItemPrice','DiscountRate') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [DiscountRate] decimal(9,4) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'DiscountRate','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'할인율',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'DiscountRate';
IF COL_LENGTH('dbo.CM_ItemPrice','DefaultYn') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [DefaultYn] bit NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'DefaultYn','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'기본사용여부',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'DefaultYn';
IF COL_LENGTH('dbo.CM_ItemPrice','CustomerItemCode') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [CustomerItemCode] nvarchar(100) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'CustomerItemCode','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'거래처 품목코드',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'CustomerItemCode';
IF COL_LENGTH('dbo.CM_ItemPrice','CustomerItemName') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [CustomerItemName] nvarchar(200) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'CustomerItemName','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'거래처 품목명',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'CustomerItemName';
IF COL_LENGTH('dbo.CM_ItemPrice','NotificationMethod') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [NotificationMethod] nvarchar(100) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'NotificationMethod','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'통보방법',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'NotificationMethod';
IF COL_LENGTH('dbo.CM_ItemPrice','SourceOrder') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [SourceOrder] int NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'SourceOrder','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'원본순서',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'SourceOrder';
IF COL_LENGTH('dbo.CM_ItemPrice','UseYn') IS NULL ALTER TABLE dbo.CM_ItemPrice ADD [UseYn] char(1) NULL;
IF NOT EXISTS (SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'UseYn','ColumnId') AND name='MS_Description') EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'사용여부',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',@level2type=N'COLUMN',@level2name=N'UseYn';
EXEC(N'
IF OBJECT_ID(''dbo.CM_CustSalesItem'',''U'') IS NOT NULL
BEGIN
 INSERT dbo.CM_ItemPrice(CorpId,ItemCode,CustCode,PriceType,Price,ApplyDate,EndDate,CustAlias,Remark,CreId,CreDt,ModId,ModDt,SalesItemSourceId,ChannelName,ForecastItemName,SourceShipToCode,SourceShipToName,SourceSoldToCode,SourceSoldToName,SalesUnit,DiscountRate,DefaultYn,CustomerItemCode,CustomerItemName,NotificationMethod,SourceOrder,UseYn)
 SELECT CorpId,ItemCode,CustCode,''20'',NULL,StartDate,EndDate,CASE WHEN LEN(CustomerItemCode)<=30 THEN CustomerItemCode END,Remark,CreId,CreDt,ModId,ModDt,Id,ChannelName,ForecastItemName,SourceShipToCode,SourceShipToName,SourceSoldToCode,SourceSoldToName,SalesUnit,DiscountRate,DefaultYn,CustomerItemCode,CustomerItemName,NotificationMethod,SourceOrder,UseYn
 FROM dbo.CM_CustSalesItem s WHERE NOT EXISTS(SELECT 1 FROM dbo.CM_ItemPrice p WHERE p.CorpId=s.CorpId AND p.SalesItemSourceId=s.Id);
 IF EXISTS(SELECT 1 FROM dbo.CM_CustSalesItem s WHERE NOT EXISTS(SELECT 1 FROM dbo.CM_ItemPrice p WHERE p.CorpId=s.CorpId AND p.SalesItemSourceId=s.Id AND p.ItemCode=s.ItemCode AND p.CustCode=s.CustCode))
 THROW 50000,N''판매품목 이전 검증 실패'',1;
 DROP TABLE dbo.CM_CustSalesItem;
END;
UPDATE dbo.CM_ItemPrice SET UseYn=''Y'' WHERE UseYn IS NULL;
');
DELETE a FROM dbo.SM_UserAuthor a JOIN dbo.SM_MenuGroup m ON m.CorpId=a.CorpId AND m.Id=a.MenuID WHERE m.FormName='Frm_CM_CustSalesItem' AND m.ProjectName='SmartMES';
DELETE dbo.SM_MenuGroup WHERE FormName='Frm_CM_CustSalesItem' AND ProjectName='SmartMES';
COMMIT;

UPDATE dbo.CM_ItemPrice SET ModId=N'개발자',
    ModDt=CONVERT(datetime,SYSUTCDATETIME() AT TIME ZONE 'UTC' AT TIME ZONE 'Korea Standard Time')
WHERE CorpId='1000' AND SalesItemSourceId IS NOT NULL
  AND NULLIF(LTRIM(RTRIM(ModId)),N'') IS NOT NULL;
UPDATE dbo.SM_MenuGroup SET Title_ko=N'거래처별 품목 관리' WHERE ProjectName='SmartMES' AND FormName='Frm_CM_ItemPrice_Cust';
