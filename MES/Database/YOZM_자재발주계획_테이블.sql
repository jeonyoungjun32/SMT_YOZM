-- YOZM 자재발주 계획 스냅샷을 보관합니다.
IF DB_NAME() <> N'YOZM' THROW 50001, N'YOZM DB에서 실행해 주세요.', 1;
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    IF OBJECT_ID(N'dbo.MM_PlacingPlan', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.MM_PlacingPlan (
            CorpId nvarchar(10) NOT NULL,
            BasisDate date NOT NULL,
            ItemCode nvarchar(200) NOT NULL,
            SourceItemName nvarchar(200) NOT NULL,
            SupplierSourceCode nvarchar(30) NOT NULL,
            SupplierSourceName nvarchar(200) NOT NULL,
            SourceUnit nvarchar(20) NULL,
            UnitPrice decimal(19,4) NULL,
            CurrentStockQty decimal(19,4) NULL,
            StockCoverageMonths decimal(19,4) NULL,
            PurchaseQty decimal(19,4) NULL,
            PurchaseAmount decimal(19,4) NULL,
            MonthlyUsageQty decimal(19,4) NULL,
            DailyUsageQty decimal(19,4) NULL,
            DeliveryDate date NULL,
            DeliveryRemainingQty decimal(19,4) NULL,
            LeadTimeDays decimal(19,4) NULL,
            DeliveryCoverageMonths decimal(19,4) NULL,
            UsagePurpose nvarchar(200) NULL,
            UsageDepartment nvarchar(100) NULL,
            Remark nvarchar(500) NULL,
            MinimumOrderQty decimal(19,4) NULL,
            PalletBasis nvarchar(100) NULL,
            SourceExtraQty decimal(19,4) NULL,
            SourceRow int NOT NULL,
            SourceIssues nvarchar(200) NULL,
            CreatedAt datetime2(0) NOT NULL CONSTRAINT DF_MM_PlacingPlan_CreatedAt DEFAULT SYSDATETIME(),
            CONSTRAINT PK_MM_PlacingPlan PRIMARY KEY (CorpId, BasisDate, ItemCode, SupplierSourceCode)
        );
    END;
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'CorpId', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'사업장코드',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'CorpId';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'BasisDate', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'재고기준일',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'BasisDate';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'ItemCode', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'품목코드',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'ItemCode';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'SourceItemName', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'원본 품명',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'SourceItemName';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'SupplierSourceCode', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'원본 거래처코드',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'SupplierSourceCode';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'SupplierSourceName', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'원본 구매처명',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'SupplierSourceName';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'SourceUnit', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'원본 발주단위',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'SourceUnit';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'UnitPrice', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'단가',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'UnitPrice';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'CurrentStockQty', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'기준일 현재고량',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'CurrentStockQty';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'StockCoverageMonths', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'현재고 재고회전(개월)',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'StockCoverageMonths';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'PurchaseQty', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'구매계획수량',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'PurchaseQty';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'PurchaseAmount', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'구매금액',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'PurchaseAmount';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'MonthlyUsageQty', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'월사용량',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'MonthlyUsageQty';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'DailyUsageQty', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'일평균 사용량(월 22일 기준)',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'DailyUsageQty';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'DeliveryDate', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'납품예정일',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'DeliveryDate';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'DeliveryRemainingQty', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'납품일 기준 잔여재고량',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'DeliveryRemainingQty';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'LeadTimeDays', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'리드타임(일)',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'LeadTimeDays';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'DeliveryCoverageMonths', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'납품일 기준 재고회전(개월)',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'DeliveryCoverageMonths';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'UsagePurpose', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'자재 사용 용도',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'UsagePurpose';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'UsageDepartment', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'사용부서',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'UsageDepartment';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'Remark', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'특이사항',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'Remark';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'MinimumOrderQty', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'최소주문수량(MOQ)',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'MinimumOrderQty';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'PalletBasis', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'1P 기준(적재수량·컷 수)',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'PalletBasis';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'SourceExtraQty', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'원본 미지정 수량',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'SourceExtraQty';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'SourceRow', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'원본 행번호',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'SourceRow';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'SourceIssues', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'원본 수식오류',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'SourceIssues';
    IF NOT EXISTS (SELECT 1 FROM sys.extended_properties
        WHERE class=1 AND major_id=OBJECT_ID(N'dbo.MM_PlacingPlan')
          AND minor_id=COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingPlan'), N'CreatedAt', 'ColumnId')
          AND name=N'MS_Description')
        EXEC sys.sp_addextendedproperty N'MS_Description', N'등록일시',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingPlan', N'COLUMN', N'CreatedAt';
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
