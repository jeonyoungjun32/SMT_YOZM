-- YOZM 자재발주 상세에 구매 계획 입력 항목을 추가합니다.
IF DB_NAME() <> N'YOZM'
    THROW 50001, N'YOZM 데이터베이스에서 실행해 주세요.', 1;

SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    IF COL_LENGTH(N'dbo.MM_PlacingD', N'MonthlyUsageQty') IS NULL
    BEGIN
        ALTER TABLE dbo.MM_PlacingD ADD MonthlyUsageQty decimal(19,4) NULL
            CONSTRAINT CK_MM_PlacingD_MonthlyUsageQty CHECK (MonthlyUsageQty >= 0);
    END;

    IF NOT EXISTS (
        SELECT 1 FROM sys.extended_properties
        WHERE class = 1 AND major_id = OBJECT_ID(N'dbo.MM_PlacingD')
          AND minor_id = COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingD'), N'MonthlyUsageQty', 'ColumnId')
          AND name = N'MS_Description'
    )
    BEGIN
        EXEC sys.sp_addextendedproperty N'MS_Description', N'월사용량',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingD', N'COLUMN', N'MonthlyUsageQty';
    END;

    IF COL_LENGTH(N'dbo.MM_PlacingD', N'MinimumOrderQty') IS NULL
    BEGIN
        ALTER TABLE dbo.MM_PlacingD ADD MinimumOrderQty decimal(19,4) NULL
            CONSTRAINT CK_MM_PlacingD_MinimumOrderQty CHECK (MinimumOrderQty >= 0);
    END;

    IF NOT EXISTS (
        SELECT 1 FROM sys.extended_properties
        WHERE class = 1 AND major_id = OBJECT_ID(N'dbo.MM_PlacingD')
          AND minor_id = COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingD'), N'MinimumOrderQty', 'ColumnId')
          AND name = N'MS_Description'
    )
    BEGIN
        EXEC sys.sp_addextendedproperty N'MS_Description', N'최소주문수량(MOQ)',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingD', N'COLUMN', N'MinimumOrderQty';
    END;

    IF COL_LENGTH(N'dbo.MM_PlacingD', N'UsagePurpose') IS NULL
    BEGIN
        ALTER TABLE dbo.MM_PlacingD ADD UsagePurpose nvarchar(200) NULL;
    END;

    IF NOT EXISTS (
        SELECT 1 FROM sys.extended_properties
        WHERE class = 1 AND major_id = OBJECT_ID(N'dbo.MM_PlacingD')
          AND minor_id = COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingD'), N'UsagePurpose', 'ColumnId')
          AND name = N'MS_Description'
    )
    BEGIN
        EXEC sys.sp_addextendedproperty N'MS_Description', N'자재 사용 용도',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingD', N'COLUMN', N'UsagePurpose';
    END;

    IF COL_LENGTH(N'dbo.MM_PlacingD', N'UsageDepartment') IS NULL
    BEGIN
        ALTER TABLE dbo.MM_PlacingD ADD UsageDepartment nvarchar(100) NULL;
    END;

    IF NOT EXISTS (
        SELECT 1 FROM sys.extended_properties
        WHERE class = 1 AND major_id = OBJECT_ID(N'dbo.MM_PlacingD')
          AND minor_id = COLUMNPROPERTY(OBJECT_ID(N'dbo.MM_PlacingD'), N'UsageDepartment', 'ColumnId')
          AND name = N'MS_Description'
    )
    BEGIN
        EXEC sys.sp_addextendedproperty N'MS_Description', N'사용부서',
            N'SCHEMA', N'dbo', N'TABLE', N'MM_PlacingD', N'COLUMN', N'UsageDepartment';
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
