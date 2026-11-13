USE YOZM;
SET XACT_ABORT ON;
BEGIN TRAN;

-- 개발자 체크값을 공통 설정으로 이관, 미설정 항목은 기존 기본값 유지
IF COL_LENGTH('dbo.CM_ItemPrice','ForecastUseYn') IS NULL
    ALTER TABLE dbo.CM_ItemPrice ADD ForecastUseYn char(1) NOT NULL
        CONSTRAINT DF_CM_ItemPrice_ForecastUseYn DEFAULT('Y') WITH VALUES;

IF OBJECT_ID('dbo.PM_ForecastUserSetting','U') IS NOT NULL
BEGIN
    EXEC(N'
        SELECT c.CorpId,c.CustCode,COALESCE(u.UseYn,c.ForecastUseYn,''N'') UseYn
        INTO #Customers
        FROM dbo.CM_Cust c
        LEFT JOIN dbo.PM_ForecastUserSetting u ON u.CorpId=c.CorpId AND u.CustCode=c.CustCode
            AND u.LoginId=''dev'' AND u.ItemCode='''' AND u.PriceType='''';

        SELECT p.CorpId,p.Seq,ISNULL(u.UseYn,''Y'') UseYn
        INTO #Items
        FROM dbo.CM_ItemPrice p
        LEFT JOIN dbo.PM_ForecastUserSetting u ON u.CorpId=p.CorpId AND u.CustCode=p.CustCode
            AND u.ItemCode=p.ItemCode AND u.PriceType=p.PriceType AND u.LoginId=''dev'';

        UPDATE c SET ForecastUseYn=s.UseYn
        FROM dbo.CM_Cust c JOIN #Customers s ON s.CorpId=c.CorpId AND s.CustCode=c.CustCode;
        UPDATE p SET ForecastUseYn=s.UseYn
        FROM dbo.CM_ItemPrice p JOIN #Items s ON s.CorpId=p.CorpId AND s.Seq=p.Seq;

        IF EXISTS(SELECT CorpId,CustCode,UseYn FROM #Customers EXCEPT SELECT CorpId,CustCode,ForecastUseYn FROM dbo.CM_Cust)
            THROW 50001,N''거래처 체크값 이관 불일치'',1;
        IF EXISTS(SELECT CorpId,Seq,UseYn FROM #Items EXCEPT SELECT CorpId,Seq,ForecastUseYn FROM dbo.CM_ItemPrice)
            THROW 50001,N''품목 체크값 이관 불일치'',1;
    ');
    DROP TABLE dbo.PM_ForecastUserSetting;
END;

IF NOT EXISTS(SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_ItemPrice')
    AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_ItemPrice'),'ForecastUseYn','ColumnId') AND name='MS_Description')
    EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'포케스팅 공통 표시여부',
        @level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_ItemPrice',
        @level2type=N'COLUMN',@level2name=N'ForecastUseYn';
COMMIT;
