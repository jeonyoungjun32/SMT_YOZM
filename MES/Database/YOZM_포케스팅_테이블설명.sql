IF DB_NAME() <> N'YOZM' THROW 50001, N'YOZM DB에서 실행해 주세요.', 1;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DECLARE @TableName sysname, @ColumnName sysname, @Description nvarchar(200);

DECLARE descriptions CURSOR LOCAL FAST_FORWARD FOR
SELECT TableName, ColumnName, Description
FROM (VALUES
    (N'PM_ForecastCustomerItem', NULL, N'포케스팅 거래처별 품목'),
    (N'PM_ForecastCustomerItem', N'CorpId', N'사업장코드'),
    (N'PM_ForecastCustomerItem', N'CustCode', N'거래처코드'),
    (N'PM_ForecastCustomerItem', N'ChannelName', N'판매채널명'),
    (N'PM_ForecastCustomerItem', N'ItemName', N'원본 품명'),
    (N'PM_ForecastCustomerItem', N'SourceOrder', N'원본 행 순번'),
    (N'PM_ForecastOrderActual', NULL, N'포케스팅 일별 실 발주량'),
    (N'PM_ForecastOrderActual', N'CorpId', N'사업장코드'),
    (N'PM_ForecastOrderActual', N'ChannelName', N'판매채널명'),
    (N'PM_ForecastOrderActual', N'ItemName', N'원본 품명'),
    (N'PM_ForecastOrderActual', N'OrderDate', N'발주일자'),
    (N'PM_ForecastOrderActual', N'OrderQty', N'실 발주량(미납 제외)')
) src(TableName, ColumnName, Description);

OPEN descriptions;
FETCH NEXT FROM descriptions INTO @TableName, @ColumnName, @Description;
WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @ObjectId int = OBJECT_ID(N'dbo.' + @TableName, N'U');
    DECLARE @ColumnId int = CASE WHEN @ColumnName IS NULL THEN 0
        ELSE COLUMNPROPERTY(@ObjectId, @ColumnName, 'ColumnId') END;
    DECLARE @Level2Type sysname = CASE WHEN @ColumnName IS NOT NULL THEN N'COLUMN' END;

    IF EXISTS
    (
        SELECT 1 FROM sys.extended_properties
        WHERE class = 1 AND major_id = @ObjectId AND minor_id = @ColumnId
          AND name = N'MS_Description'
    )
        EXEC sys.sp_updateextendedproperty N'MS_Description', @Description,
            N'SCHEMA', N'dbo', N'TABLE', @TableName, @Level2Type, @ColumnName;
    ELSE
        EXEC sys.sp_addextendedproperty N'MS_Description', @Description,
            N'SCHEMA', N'dbo', N'TABLE', @TableName, @Level2Type, @ColumnName;

    FETCH NEXT FROM descriptions INTO @TableName, @ColumnName, @Description;
END;
CLOSE descriptions;
DEALLOCATE descriptions;

COMMIT TRANSACTION;
