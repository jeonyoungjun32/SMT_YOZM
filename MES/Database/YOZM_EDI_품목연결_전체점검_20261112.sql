USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRANSACTION;
DECLARE @Links TABLE(CustCode nvarchar(20),SourceKey nvarchar(700) COLLATE Latin1_General_100_BIN2,SourceCode nvarchar(200),SourceName nvarchar(500),ItemCode nvarchar(200),Factor decimal(19,4));
INSERT @Links VALUES(N'B2B_MN',N'N:요즘_그릭요거트_알룰로스',N'',N'요즘_그릭요거트_알룰로스',N'1FBAX100G0101',1);
INSERT @Links VALUES(N'B2B_MN',N'N:요즘_그릭요거트_애플시나몬',N'',N'요즘_그릭요거트_애플시나몬',N'1TPPX130G0102',1);
INSERT @Links VALUES(N'B2C_NAVER',N'C:000F_55_8559_',N'000F_55_8559_',N'요즘 그릭요거트 플레인 100g 3개입',N'1PLPA100G0301',1);
INSERT @Links VALUES(N'B2C_COSTCO',N'C:674761',N'674761',N'요즘 그릭요거트 플레인 450g x 2',N'1PLPA450G0101',2);
INSERT @Links VALUES(N'B2C_COSTCO',N'C:674763',N'674763',N'요즘 그릭요거트 블루베리 콩포트 130g x 5',N'1TPPA130G0101',5);
INSERT @Links VALUES(N'B2C_COSTCO',N'C:674764',N'674764',N'요즘 그릭요거트 초코소보로 120g x 5',N'1TPSA120G0102',5);
INSERT @Links VALUES(N'B2C_COSTCO',N'C:685530',N'685530',N'요즘 그릭요거트 알룰로스 블렌드 100g x 6',N'1FBAX100G0101',6);
INSERT @Links VALUES(N'B2C_COSTCO',N'C:674762',N'674762',N'요즘 그릭요거트 소프트 500g x 2',N'1PLSA500G0101',2);
INSERT @Links VALUES(N'B2C_CAFE24',N'N:[정기배송]플레인그릭요거트|O:매주받을구성선택=플레인450G2개',N'',N'[정기배송] 플레인 그릭요거트',N'1PLPA450G0101',2);
INSERT @Links VALUES(N'B2C_CAFE24',N'N:[정기배송]요즘그릭요거트|O:매주받을구성선택=플레인100G7개',N'',N'[정기배송] 요즘 그릭요거트',N'1PLPA100G0101',7);
INSERT @Links VALUES(N'B2C_CAFE24',N'N:[정기배송]요즘마시는그릭요거트스타터|O:옵션선택=요즘마시는그릭요거트스타터1.8L2병',N'',N'[정기배송] 요즘 마시는 그릭요거트 스타터',N'1DRPA1.8L0101',2);
INSERT @Links VALUES(N'B2C_JNH',N'N:플레인그릭요거트(100G)',N'',N'플레인그릭요거트 (100g)',N'1PLPA100G0101',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:플레인그릭요거트소프트(100G)',N'',N'플레인그릭요거트소프트 (100g)',N'1PLSA100G0101',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:플레인WITH블루베리콩포트(단포장)',N'',N'플레인with블루베리콩포트 (단포장)',N'1TPPA130G0101',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:플레인WITH카카오그래놀라(단포장)',N'',N'플레인with카카오그래놀라 (단포장)',N'1TPPA120G0101',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:벨베티WITH크리스피그래놀라(단포장)',N'',N'벨베티with크리스피그래놀라 (단포장)',N'1TPSA120G0101',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:N벨베티WITH초코소보로그래놀라(단포장)',N'',N'N벨베티with초코소보로그래놀라 (단포장)',N'1TPSA120G0102',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:마이노멀X요즘애플시나몬130G',N'',N'마이노멀x요즘 애플시나몬 130g',N'1TPPX130G0102',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:마이노멀X요즘알룰로스블렌드100G',N'',N'마이노멀x요즘 알룰로스 블렌드 100g',N'1FBAX100G0101',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:플레인450G',N'',N'플레인 450g',N'1PLPA450G0101',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:크리스피그래놀라3입(20GX3개)',N'',N'크리스피그래놀라 3입 (20g x 3개)',N'1TPTA020G0302',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:요즘그릭요거트저당말차그릭요거트100G',N'',N'요즘 그릭요거트 저당 말차 그릭요거트 100g',N'1FBGA100G0101',1);
INSERT @Links VALUES(N'B2C_JNH',N'N:블루베리콩포트3입(30GX3개)',N'',N'블루베리콩포트 3입 (30g x 3개)',N'1TPTA030G0301',1);
INSERT @Links VALUES(N'B2C_OLIVE',N'C:2345702177994|O:100G*5입',N'2345702177994',N'[B000000257632] 요즘 그릭요거트 소프트 100g 5입',N'1PLSA100G0101',5);
INSERT @Links VALUES(N'B2C_PUDDING',N'C:1000002649',N'1000002649',N'[요즘] 플레인 그릭요거트 3kg',N'1PLPA3.0K0101',1);
INSERT @Links VALUES(N'B2C_CAFE24',N'N:요즘그릭요거트플레인100G3개입',N'',N'요즘 그릭요거트 플레인 100g 3개입',N'1PLPA100G0301',1);
INSERT @Links VALUES(N'B2C_CAFE24',N'N:[동아쏘시오]요즘그릭요거트|O:선택=플레인100G',N'',N'[동아쏘시오] 요즘 그릭요거트',N'1PLPA100G0101',1);
INSERT @Links VALUES(N'B2C_ALBI',N'N:카카오120G',N'',N'카카오 120g',N'1TPPA120G0101',1);
INSERT @Links VALUES(N'B2C_ALBI',N'N:소프트500G',N'',N'소프트 500g',N'1PLSA500G0101',1);
INSERT @Links VALUES(N'B2C_PIZZA',N'N:플레인800G',N'',N'플레인 800g',N'1PLPA800G0101',1);
-- 원본 연결은 사업장·거래처·상품 키 단위, 이미 반영된 주문은 유지
INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
SELECT '1000',x.CustCode,x.SourceKey,x.SourceCode,x.SourceName,x.ItemCode,x.Factor,N'개발자' FROM @Links x
WHERE NOT EXISTS(SELECT 1 FROM BM_EdiItemMap m WHERE m.CorpId='1000' AND m.CustCode=x.CustCode AND m.SourceKey=x.SourceKey);
UPDATE r SET ItemCode=x.ItemCode,Factor=x.Factor,ModId=N'개발자',ModDt=GETDATE()
FROM BM_EdiImportRow r JOIN @Links x ON x.CustCode=r.CustCode AND x.SourceKey=r.SourceKey
WHERE r.CorpId='1000' AND r.Applied=0 AND NULLIF(r.ItemCode,'') IS NULL;
INSERT CM_ItemPrice(CorpId,CustCode,ItemCode,PriceType,UseYn,CreId,CreDt)
SELECT DISTINCT '1000',x.CustCode,x.ItemCode,'20','Y',N'개발자',GETDATE() FROM @Links x
WHERE NOT EXISTS(SELECT 1 FROM CM_ItemPrice p WHERE p.CorpId='1000' AND p.CustCode=x.CustCode AND p.ItemCode=x.ItemCode AND p.PriceType='20');
UPDATE CM_ItemPrice SET Price=11700.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_NAVER' AND ItemCode=N'1PLPA100G0301' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=8894.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_COSTCO' AND ItemCode=N'1PLPA450G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=2908.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_COSTCO' AND ItemCode=N'1TPPA130G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=2908.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_COSTCO' AND ItemCode=N'1TPSA120G0102' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=2582.3333,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_COSTCO' AND ItemCode=N'1FBAX100G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=8300.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_COSTCO' AND ItemCode=N'1PLSA500G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=15000.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_CAFE24' AND ItemCode=N'1PLPA450G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=7700.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_CAFE24' AND ItemCode=N'1DRPA1.8L0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=3500.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_OLIVE' AND ItemCode=N'1PLSA100G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=4900.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_NONGHYUP' AND ItemCode=N'1PLSA100G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=4900.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_NONGHYUP' AND ItemCode=N'1TPPA130G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=3900.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_NONGHYUP' AND ItemCode=N'1PLPA100G0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=66000.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_PUDDING' AND ItemCode=N'1PLPA3.0K0101' AND PriceType='20' AND Price IS NULL;
UPDATE CM_ItemPrice SET Price=11700.0000,ApplyDate=CASE WHEN ApplyDate IS NULL OR ApplyDate>N'2026-11-05' THEN N'2026-11-05' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND CustCode=N'B2C_CAFE24' AND ItemCode=N'1PLPA100G0301' AND PriceType='20' AND Price IS NULL;
UPDATE CM_Item SET Size=N'130g',ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND ItemCode=N'1TPPD130G0101' AND ISNULL(Size,'')=N'';
UPDATE CM_Item SET Size=N'20g',ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND ItemCode=N'1TPTA020G0302' AND ISNULL(Size,'')=N'';
UPDATE CM_Item SET Size=N'30g',ModId=N'개발자',ModDt=GETDATE() WHERE CorpId='1000' AND ItemCode=N'1TPTA030G0301' AND ISNULL(Size,'')=N'';
COMMIT TRANSACTION;