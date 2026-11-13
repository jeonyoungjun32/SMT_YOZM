USE YOZM;
SET XACT_ABORT ON;
BEGIN TRAN;
IF OBJECT_ID('dbo.BM_EdiImportFile') IS NULL
CREATE TABLE dbo.BM_EdiImportFile (
    FileId bigint IDENTITY PRIMARY KEY,
    CorpId nvarchar(10) NOT NULL,
    FileKey nvarchar(260) NOT NULL,
    FileName nvarchar(260) NOT NULL,
    BasisDate date NOT NULL,
    Status nvarchar(20) NOT NULL,
    ErrorMessage nvarchar(1000) NULL,
    CreId nvarchar(128) NOT NULL,
    CreDt datetime2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT UQ_BM_EdiImportFile UNIQUE(CorpId,FileKey));
IF OBJECT_ID('dbo.BM_EdiImportRow') IS NULL
CREATE TABLE dbo.BM_EdiImportRow (
    RowId bigint IDENTITY PRIMARY KEY,
    FileId bigint NOT NULL REFERENCES dbo.BM_EdiImportFile(FileId),
    CorpId nvarchar(10) NOT NULL,
    SourceSheet nvarchar(128) NOT NULL,
    SourceRow int NOT NULL,
    CustCode nvarchar(20) NULL,
    ItemCode nvarchar(200) NULL,
    SourceCode nvarchar(200) NOT NULL,
    SourceName nvarchar(500) NOT NULL,
    SourceOption nvarchar(1000) NOT NULL,
    SourceKey nvarchar(700) COLLATE Latin1_General_100_BIN2 NOT NULL,
    RowKey nvarchar(700) COLLATE Latin1_General_100_BIN2 NOT NULL,
    OrderNo nvarchar(200) NOT NULL,
    CenterName nvarchar(200) NOT NULL,
    ShipToName nvarchar(300) NOT NULL,
    OrderDate date NULL,
    DeliveryDate date NULL,
    Qty decimal(19,4) NULL,
    Factor decimal(19,4) NOT NULL DEFAULT 1,
    Amount decimal(19,4) NULL,
    ErrorMessage nvarchar(1000) NULL,
    Applied bit NOT NULL DEFAULT 0,
    ModId nvarchar(128) NULL,
    ModDt datetime2 NULL);
IF NOT EXISTS(SELECT 1 FROM sys.indexes WHERE name='UX_BM_EdiAppliedRow' AND object_id=OBJECT_ID('BM_EdiImportRow'))
CREATE UNIQUE INDEX UX_BM_EdiAppliedRow ON BM_EdiImportRow(CorpId,RowKey) WHERE Applied=1;
IF OBJECT_ID('dbo.BM_EdiItemMap') IS NULL
CREATE TABLE dbo.BM_EdiItemMap (
    CorpId nvarchar(10) NOT NULL,
    CustCode nvarchar(20) NOT NULL,
    SourceKey nvarchar(700) COLLATE Latin1_General_100_BIN2 NOT NULL,
    SourceCode nvarchar(200) NOT NULL,
    SourceName nvarchar(500) NOT NULL,
    ItemCode nvarchar(200) NOT NULL,
    Factor decimal(19,4) NOT NULL DEFAULT 1,
    ModId nvarchar(128) NOT NULL,
    ModDt datetime2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT PK_BM_EdiItemMap PRIMARY KEY NONCLUSTERED(CorpId,CustCode,SourceKey));
IF OBJECT_ID('dbo.BM_EdiForecastBackup') IS NULL
CREATE TABLE dbo.BM_EdiForecastBackup (
    CorpId nvarchar(10) NOT NULL,
    CustCode nvarchar(20) NOT NULL,
    ItemCode nvarchar(200) NOT NULL,
    BasisDate date NOT NULL,
    PreviousQty int NULL,
    LastQty int NOT NULL,
    ModDt datetime2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT PK_BM_EdiForecastBackup PRIMARY KEY(CorpId,CustCode,ItemCode,BasisDate));

IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B010' AND SourceKey=N'C:8809520490634')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B010',N'C:8809520490634',N'8809520490634',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B010' AND SourceKey=N'C:8809520490856')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B010',N'C:8809520490856',N'8809520490856',N'벨베티 크리스피 120g',N'1TPSA120G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B010' AND SourceKey=N'C:8809520491549')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B010',N'C:8809520491549',N'8809520491549',N'마시는그릭 750ml',N'1DRPA750M0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B010' AND SourceKey=N'C:8809520491778')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B010',N'C:8809520491778',N'8809520491778',N'패트와매트 플레인',N'1PLP000000101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B010' AND SourceKey=N'C:8809520491426')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B010',N'C:8809520491426',N'8809520491426',N'플레인 애플시나몬 130g',N'1TPPX130G0102',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B010' AND SourceKey=N'C:8809520491532')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B010',N'C:8809520491532',N'8809520491532',N'플레인 푸룬 130g',N'1TPPX130G0104',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B012' AND SourceKey=N'C:8809520490108')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B012',N'C:8809520490108',N'8809520490108',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B011' AND SourceKey=N'C:8809520490634')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B011',N'C:8809520490634',N'8809520490634',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B011' AND SourceKey=N'C:8809520491396')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B011',N'C:8809520491396',N'8809520491396',N'알룰로스 블랜드 100g',N'1FBAX100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B011' AND SourceKey=N'C:8809520491426')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B011',N'C:8809520491426',N'8809520491426',N'플레인 애플시나몬 130g',N'1TPPX130G0102',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B011' AND SourceKey=N'C:8809520491549')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B011',N'C:8809520491549',N'8809520491549',N'마시는그릭 750ml',N'1DRPA750M0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B011' AND SourceKey=N'C:8809520491761')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B011',N'C:8809520491761',N'8809520491761',N'춘식이 키캡 플레인',N'1PLPX100G0104',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B011' AND SourceKey=N'C:8809520491518')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B011',N'C:8809520491518',N'8809520491518',N'말차그릭요거트 100g',N'1FBGA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B006' AND SourceKey=N'C:8809520491549')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B006',N'C:8809520491549',N'8809520491549',N'마시는그릭 750ml',N'1DRPA750M0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B006' AND SourceKey=N'C:8809520490634')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B006',N'C:8809520490634',N'8809520490634',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B004' AND SourceKey=N'C:8809520490108')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B004',N'C:8809520490108',N'8809520490108',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B004' AND SourceKey=N'C:8809520490771')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B004',N'C:8809520490771',N'8809520490771',N'소프트 그릭요거트 100g',N'1PLSA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B004' AND SourceKey=N'C:8809520491396')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B004',N'C:8809520491396',N'8809520491396',N'알룰로스 블랜드 100g',N'1FBAX100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B004' AND SourceKey=N'C:8809520491549')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B004',N'C:8809520491549',N'8809520491549',N'마시는그릭 750ml',N'1DRPA750M0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B004' AND SourceKey=N'C:8809520490634')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B004',N'C:8809520490634',N'8809520490634',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B018' AND SourceKey=N'C:1000749378')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B018',N'C:1000749378',N'1000749378',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B018' AND SourceKey=N'C:1000786118')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B018',N'C:1000786118',N'1000786118',N'알룰로스 블랜드 100g',N'1FBAX100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B018' AND SourceKey=N'C:1000749387')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B018',N'C:1000749387',N'1000749387',N'소프트 그릭요거트 100g',N'1PLSA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B018' AND SourceKey=N'C:1000786114')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B018',N'C:1000786114',N'1000786114',N'플레인 그릭요거트 450g',N'1PLPA450G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B018' AND SourceKey=N'C:1000809554')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B018',N'C:1000809554',N'1000809554',N'말차그릭요거트 100g',N'1FBGA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B020' AND SourceKey=N'C:AA0022609')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B020',N'C:AA0022609',N'AA0022609',N'서울우유 플레인 100g*8',N'1PLPB100G0801',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B020' AND SourceKey=N'C:AA0022610')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B020',N'C:AA0022610',N'AA0022610',N'서울우유 소프트 100g*8',N'1PLSB100G0801',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B020' AND SourceKey=N'C:AA0022611')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B020',N'C:AA0022611',N'AA0022611',N'서울우유 더진한 450*4',N'1PLPB450G0401',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B020' AND SourceKey=N'C:AA0022612')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B020',N'C:AA0022612',N'AA0022612',N'서울우유 더진한소프트 450*4',N'1PLSB450G0401',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B020' AND SourceKey=N'C:AA0022614')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B020',N'C:AA0022614',N'AA0022614',N'서울우유 알룰로스 100g*8',N'1FBAB800G0801',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B007' AND SourceKey=N'C:8809520490634')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B007',N'C:8809520490634',N'8809520490634',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B007' AND SourceKey=N'C:8809520491525')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B007',N'C:8809520491525',N'8809520491525',N'플레인 복숭아 130g',N'1TPPX130G0103',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B007' AND SourceKey=N'C:8809520490481')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B007',N'C:8809520490481',N'8809520490481',N'플레인 딸기 130g',N'1TPPA130G0102',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B007' AND SourceKey=N'C:8809520491594')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B007',N'C:8809520491594',N'8809520491594',N'요즘 그릭요거트 아사이베리 파르페 220ml',N'3FDIC000003',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B019' AND SourceKey=N'C:27416518')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B019',N'C:27416518',N'27416518',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B019' AND SourceKey=N'C:27416556')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B019',N'C:27416556',N'27416556',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B019' AND SourceKey=N'C:27412695')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B019',N'C:27412695',N'27412695',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B019' AND SourceKey=N'C:27412640')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B019',N'C:27412640',N'27412640',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B019' AND SourceKey=N'C:27395004')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B019',N'C:27395004',N'27395004',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B019' AND SourceKey=N'C:27412701')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B019',N'C:27412701',N'27412701',N'소프트 그릭요거트 100g',N'1PLSA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B019' AND SourceKey=N'C:27394823')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B019',N'C:27394823',N'27394823',N'소프트 그릭요거트 100g',N'1PLSA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B002' AND SourceKey=N'C:8809520490634')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B002',N'C:8809520490634',N'8809520490634',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B002' AND SourceKey=N'C:8809520490108')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B002',N'C:8809520490108',N'8809520490108',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B001' AND SourceKey=N'C:8809520490634')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B001',N'C:8809520490634',N'8809520490634',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B001' AND SourceKey=N'C:8809520490269')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B001',N'C:8809520490269',N'8809520490269',N'플레인 카카오 120g',N'1TPPA120G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B001' AND SourceKey=N'C:8809520491549')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B001',N'C:8809520491549',N'8809520491549',N'마시는그릭 750ml',N'1DRPA750M0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B001' AND SourceKey=N'C:8809520491532')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B001',N'C:8809520491532',N'8809520491532',N'플레인 푸룬 130g',N'1TPPX130G0104',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B001' AND SourceKey=N'C:8809520490108')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B001',N'C:8809520490108',N'8809520490108',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B001' AND SourceKey=N'C:8809520490771')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B001',N'C:8809520490771',N'8809520490771',N'소프트 그릭요거트 100g',N'1PLSA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B009' AND SourceKey=N'C:8809520491648')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B009',N'C:8809520491648',N'8809520491648',N'플레인 그릭요거트 450g*2',N'1PLPA450G0201',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B008' AND SourceKey=N'C:8809520491372')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B008',N'C:8809520491372',N'8809520491372',N'소프트 그릭요거트 500g*2',N'1PLSA500G0201',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B008' AND SourceKey=N'C:8809520490641')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B008',N'C:8809520490641',N'8809520490641',N'플레인 블루베리 130g*4',N'1TPPA130G0401',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B008' AND SourceKey=N'C:8809520491556')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B008',N'C:8809520491556',N'8809520491556',N'마시는그릭 1.8L',N'1DRPA1.8L0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B015' AND SourceKey=N'C:PL02002572857')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B015',N'C:PL02002572857',N'PL02002572857',N'곰곰 소프트 100g',N'1PLSD100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520490184')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520490184',N'8809520490184',N'플레인 그릭요거트 800g',N'1PLPA800G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520490795')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520490795',N'8809520490795',N'소프트 그릭요거트 500g',N'1PLSA500G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520490955')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520490955',N'8809520490955',N'플레인 그릭요거트 100g*3',N'1PLPA100G0301',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520490269')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520490269',N'8809520490269',N'플레인 카카오 120g',N'1TPPA120G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520490634')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520490634',N'8809520490634',N'플레인 블루베리 130g',N'1TPPA130G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520491006')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520491006',N'8809520491006',N'소프트 그릭요거트 100g*3',N'1PLSA100G0301',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520490108')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520490108',N'8809520490108',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520491556')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520491556',N'8809520491556',N'마시는그릭 1.8L',N'1DRPA1.8L0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520491549')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520491549',N'8809520491549',N'마시는그릭 750ml',N'1DRPA750M0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520490924')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520490924',N'8809520490924',N'플레인 그릭요거트 450g',N'1PLPA450G0101',1,N'개발자');
IF NOT EXISTS(SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B003' AND SourceKey=N'C:8809520490108')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B003',N'C:8809520490108',N'8809520490108',N'플레인 그릭요거트 100g',N'1PLPA100G0101',1,N'개발자');
DECLARE @Descriptions TABLE(ColumnName sysname,Title nvarchar(100));
INSERT @Descriptions VALUES
    (N'FileId',N'업로드번호'),
    (N'CorpId',N'사업장코드'),
    (N'FileKey',N'등록 파일명 또는 직접 등록 식별값'),
    (N'FileName',N'원본파일명'),
    (N'BasisDate',N'포케스팅기준일'),
    (N'Status',N'처리상태'),
    (N'ErrorMessage',N'오류내용'),
    (N'CreId',N'등록자'),
    (N'CreDt',N'등록일시'),
    (N'RowId',N'업로드행번호'),
    (N'SourceSheet',N'원본시트명'),
    (N'SourceRow',N'원본행번호'),
    (N'CustCode',N'거래처코드'),
    (N'ItemCode',N'품목코드'),
    (N'SourceCode',N'원본주문코드'),
    (N'SourceName',N'원본품명'),
    (N'SourceOption',N'원본옵션'),
    (N'SourceKey',N'원본 상품 식별값(상품코드 또는 품명·옵션)'),
    (N'RowKey',N'주문 식별값(거래처·주문번호·상품·반복순번)'),
    (N'OrderNo',N'원본주문번호'),
    (N'CenterName',N'센터명'),
    (N'ShipToName',N'납품처명'),
    (N'OrderDate',N'원본발주일'),
    (N'DeliveryDate',N'원본납기일'),
    (N'Qty',N'원본수량'),
    (N'Factor',N'품목환산수량'),
    (N'Amount',N'원본금액'),
    (N'Applied',N'포케스팅반영여부'),
    (N'ModId',N'수정자'),
    (N'ModDt',N'수정일시'),
    (N'PreviousQty',N'반영전수량'),
    (N'LastQty',N'최종반영수량');
DECLARE @Table sysname,@Column sysname,@Title nvarchar(100);
DECLARE descriptions CURSOR LOCAL FAST_FORWARD FOR
SELECT t.name,c.name,d.Title FROM sys.tables t JOIN sys.columns c ON c.object_id=t.object_id
JOIN @Descriptions d ON d.ColumnName=c.name WHERE t.name IN('BM_EdiImportFile','BM_EdiImportRow','BM_EdiItemMap','BM_EdiForecastBackup');
OPEN descriptions; FETCH NEXT FROM descriptions INTO @Table,@Column,@Title;
WHILE @@FETCH_STATUS=0 BEGIN
IF NOT EXISTS(SELECT 1 FROM sys.extended_properties WHERE major_id=OBJECT_ID('dbo.'+@Table) AND minor_id=COLUMNPROPERTY(OBJECT_ID('dbo.'+@Table),@Column,'ColumnId') AND name='MS_Description')
EXEC sys.sp_addextendedproperty @name=N'MS_Description',@value=@Title,@level0type=N'SCHEMA',@level0name=N'dbo',@level1type=N'TABLE',@level1name=@Table,@level2type=N'COLUMN',@level2name=@Column;
FETCH NEXT FROM descriptions INTO @Table,@Column,@Title;
END
CLOSE descriptions; DEALLOCATE descriptions;
COMMIT;
-- 쿠팡 벨베티 초코소보로 120g의 원본 바코드 연결
IF NOT EXISTS (SELECT 1 FROM BM_EdiItemMap WHERE CorpId='1000' AND CustCode='B2B014' AND SourceKey=N'C:8809520491259')
    INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
    VALUES('1000','B2B014',N'C:8809520491259','8809520491259',N'[로켓프레시] 요즘 벨베티 그릭요거트 with 초코소보로 그래놀라 120g','1TPSA120G0102',1,N'개발자');

-- 거래처 단가 기반 금액 계산
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

