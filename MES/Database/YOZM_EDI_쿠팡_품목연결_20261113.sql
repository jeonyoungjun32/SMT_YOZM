USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRANSACTION;
DECLARE @Map TABLE(CustCode nvarchar(20),SourceKey nvarchar(700) COLLATE Latin1_General_100_BIN2,SourceCode nvarchar(200),SourceName nvarchar(500),ItemCode nvarchar(200));
INSERT @Map VALUES(N'B2B014',N'C:8809520490184',N'8809520490184',N'[로켓프레시][SR]요즘플레인그릭요거트 800g',N'1PLPA800G0101'),
(N'B2B014',N'C:8809520490795',N'8809520490795',N'[로켓프레시] 플레인그릭요거트 소프트 500g',N'1PLSA500G0101'),
(N'B2B014',N'C:8809520490634',N'8809520490634',N'[로켓프레시] 플레인그릭요거트 with 블루베리콩포트 130g',N'1TPPA130G0101'),
(N'B2B014',N'C:8809520491006',N'8809520491006',N'[로켓프레시] 요즘 플레인 그릭요거트 소프트 100g 3개입',N'1PLSA100G0301'),
(N'B2B014',N'C:8809520490108',N'8809520490108',N'[로켓프레시] 요즘 그릭요거트 플레인 100g',N'1PLPA100G0101'),
(N'B2B014',N'C:8809520491518',N'8809520491518',N'[로켓프레시] 요즘 저당 말차 그릭요거트 100g 1개',N'1FBGA100G0101'),
(N'B2B014',N'C:8809520490924',N'8809520490924',N'[로켓프레시] 요즘 그릭요거트 플레인 450g, 1개',N'1PLPA450G0101'),
(N'B2B014',N'C:8809520490641',N'8809520490641',N'[로켓프레시] 플레인그릭요거트with블루베리콩포트 130g x 4개입',N'1TPPA130G0401'),
(N'B2B014',N'C:8809520491259',N'8809520491259',N'[로켓프레시] 요즘 벨베티 그릭요거트 with 초코소보로 그래놀라 120g',N'1TPSA120G0102');
IF EXISTS(SELECT 1 FROM @Map m WHERE NOT EXISTS(SELECT 1 FROM CM_Item i WHERE i.CorpId='1000' AND i.ItemCode=m.ItemCode AND i.UseYn='Y'))
 THROW 50001,N'사용 중인 품목 확인 필요',1;
UPDATE t SET ItemCode=m.ItemCode,Factor=1,SourceCode=m.SourceCode,SourceName=m.SourceName,ModId=N'개발자'
FROM BM_EdiItemMap t JOIN @Map m ON m.CustCode=t.CustCode AND m.SourceKey=t.SourceKey WHERE t.CorpId='1000';
INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
SELECT '1000',m.CustCode,m.SourceKey,m.SourceCode,m.SourceName,m.ItemCode,1,N'개발자' FROM @Map m
WHERE NOT EXISTS(SELECT 1 FROM BM_EdiItemMap t WHERE t.CorpId='1000' AND t.CustCode=m.CustCode AND t.SourceKey=m.SourceKey);
COMMIT;