USE YOZM;
SET XACT_ABORT ON;
BEGIN TRAN;
IF COL_LENGTH('dbo.CM_Cust','EdiMatchNames') IS NULL
    ALTER TABLE dbo.CM_Cust ADD EdiMatchNames nvarchar(500) NULL;
EXEC(N'UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''곰곰'' ELSE EdiMatchNames+N''|곰곰'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2B015'' AND CHARINDEX(N''|곰곰|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''비마트'' ELSE EdiMatchNames+N''|비마트'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2B017'' AND CHARINDEX(N''|비마트|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''올리브영'' ELSE EdiMatchNames+N''|올리브영'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2C_OLIVE'' AND CHARINDEX(N''|올리브영|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''지마켓'' ELSE EdiMatchNames+N''|지마켓'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2C_GMARKET'' AND CHARINDEX(N''|지마켓|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''이랜드'' ELSE EdiMatchNames+N''|이랜드'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2B013'' AND CHARINDEX(N''|이랜드|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''킴스'' ELSE EdiMatchNames+N''|킴스'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2B013'' AND CHARINDEX(N''|킴스|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''현대그린'' ELSE EdiMatchNames+N''|현대그린'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''323001'' AND CHARINDEX(N''|현대그린|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''세븐일레븐'' ELSE EdiMatchNames+N''|세븐일레븐'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2B007'' AND CHARINDEX(N''|세븐일레븐|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''씨에스유통'' ELSE EdiMatchNames+N''|씨에스유통'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2B006'' AND CHARINDEX(N''|씨에스유통|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''이마트에브리'' ELSE EdiMatchNames+N''|이마트에브리'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2B002'' AND CHARINDEX(N''|이마트에브리|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''에브리데이'' ELSE EdiMatchNames+N''|에브리데이'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2B002'' AND CHARINDEX(N''|에브리데이|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''알비글로벌'' ELSE EdiMatchNames+N''|알비글로벌'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2C_ALBI'' AND CHARINDEX(N''|알비글로벌|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''주식회사 피자앤컴퍼니'' ELSE EdiMatchNames+N''|주식회사 피자앤컴퍼니'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2C_PIZZA'' AND CHARINDEX(N''|주식회사 피자앤컴퍼니|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
UPDATE dbo.CM_Cust SET EdiMatchNames=CASE WHEN NULLIF(EdiMatchNames,'''') IS NULL THEN N''제이앤에이치'' ELSE EdiMatchNames+N''|제이앤에이치'' END,ModId=N''개발자'',ModDt=GETDATE() WHERE CorpId=''1000'' AND CustCode=''B2C_JNH'' AND CHARINDEX(N''|제이앤에이치|'',N''|''+ISNULL(EdiMatchNames,'''')+N''|'')=0;
');
IF NOT EXISTS(SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.CM_Cust') AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.CM_Cust'),'EdiMatchNames','ColumnId') AND name='MS_Description')
 EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=N'EDI 거래처명(복수 구분: |)',@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=N'CM_Cust',@level2type=N'COLUMN',@level2name=N'EdiMatchNames';
COMMIT;
