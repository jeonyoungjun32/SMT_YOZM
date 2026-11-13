USE YOZM;
-- 과거 데이터 보정용: 식별값 전환 이후에는 재실행 금지
IF COL_LENGTH('dbo.BM_EdiImportFile','FileKey') IS NOT NULL
    THROW 50001,N'해시 제거 이전의 일회성 보정 스크립트입니다. 현재 DB에 재실행하지 마세요.',1;

SET XACT_ABORT ON;
BEGIN TRAN;
DECLARE @Parts TABLE(OldRowId bigint,ItemCode nvarchar(200),SourceName nvarchar(500),SourceOption nvarchar(1000),SourceKey varchar(64),RowKey varchar(64),Qty decimal(19,4),Amount decimal(19,4));
INSERT @Parts VALUES(330,N'1PLPA100G0101',N'플레인 그릭요거트 100g',N'세트 분리: (특가 구성) 요즘 그릭요거트 혼합 베스트 5종 구성 / 플레인+소프트+블루베리+알룰로스+크리스피그래놀라 | ','2872654A0E3E1E72ECFDBC4ACB41C97F6EC748036A49815B229A1C6D19C5E9E3','C2F47431827226401FF4EBFAEBD382C035F792FC33D8B11BBF7C217836D100D4',1,4280),
(330,N'1PLSA100G0101',N'소프트 그릭요거트 100g',N'세트 분리: (특가 구성) 요즘 그릭요거트 혼합 베스트 5종 구성 / 플레인+소프트+블루베리+알룰로스+크리스피그래놀라 | ','7C63BB48461ADC7315956FCD3E617F687DB5B29AB767098271D58D24A7533743','05540A24F77BE66DD29DD958DE1DB9FF807A09578BE101BE3A07C7607E28B5D9',1,4280),
(330,N'1TPPA130G0101',N'플레인 블루베리 130g',N'세트 분리: (특가 구성) 요즘 그릭요거트 혼합 베스트 5종 구성 / 플레인+소프트+블루베리+알룰로스+크리스피그래놀라 | ','5335BB7EC0FBF6E655C01EE2E54D981F7438BAB4C28B967033D16BBA952C2D9D','EFDC2DE9F1C16BF938421C7C5ACF11D8D05259AF2C2EB34A358CC4EF411BF161',1,4280),
(330,N'1FBAX100G0101',N'알룰로스 블랜드 100g',N'세트 분리: (특가 구성) 요즘 그릭요거트 혼합 베스트 5종 구성 / 플레인+소프트+블루베리+알룰로스+크리스피그래놀라 | ','C10BCD2BF1C48F6D0EB07D63111E6913298356C99AB0071B7CE52FEBE13440A9','D294DB26FF15A67F91BC474A29B006440895BEACA34771E858C40DF8FB79530D',1,4280),
(330,N'1TPSA120G0101',N'벨베티 크리스피 120g',N'세트 분리: (특가 구성) 요즘 그릭요거트 혼합 베스트 5종 구성 / 플레인+소프트+블루베리+알룰로스+크리스피그래놀라 | ','80644CD2948DC5DEC929AA9A17EF864329FFFC308F2950BB4CA8647E6D69D39E','5C519A2CF6611B71DB076C2886893FA9924AA03A010F550D70BF5B0F8C770A73',1,4280),
(331,N'1PLPA100G0101',N'플레인 그릭요거트 100g',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 카카오 그래놀라 토핑 (3개입), 토핑 선택 2: 초코소보로 그래놀라 토핑 (3개입)','36E2B9492D92FAC4E9CA92EE1544A397AF17C98ADA0223B9625B585E367D35AF','E131DE48C49ECB069DEBE56A7A0ADE7B7D2847BE32A314F256AC91AC5AC84ECC',3,11175),
(331,N'1PLSA100G0101',N'소프트 그릭요거트 100g',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 카카오 그래놀라 토핑 (3개입), 토핑 선택 2: 초코소보로 그래놀라 토핑 (3개입)','21C21F249095E2B65AD607DA66D99DE3B1B8A1F882E32194196D647E9FC1D121','22CC089C8E27CD4838DED5C7A60F31880FAD6EA4A026115E375C5A1669A0842A',3,11175),
(331,N'1TPTA020G0301',N'카카오그래놀라 토핑 (20g x 3개)',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 카카오 그래놀라 토핑 (3개입), 토핑 선택 2: 초코소보로 그래놀라 토핑 (3개입)','5E23F39E05A284E9355374731822FB1F1F9D3AE1876414C672961600AC774B5C','02A0C41AD880D5CA9C78B8D85274A33676BEB2C266013155F6886791A8D441A0',1,3725),
(331,N'1TPTA020G0303',N'초코소보로그래놀라 토핑 (20g x 3개)',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 카카오 그래놀라 토핑 (3개입), 토핑 선택 2: 초코소보로 그래놀라 토핑 (3개입)','AD212E83010399CADC7699A3C51B1B939CD391B40DE88CF6BA5EF54A0DEC5767','C801717C4FF6A9B0EF2769C8457D4703D98FC1512DA5B578B3F6AD571B822E27',1,3725),
(332,N'1PLPA100G0101',N'플레인 그릭요거트 100g',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 크리스피 그래놀라 토핑 (3개입), 토핑 선택 2: 블루베리 콩포트 토핑 (3개입)','37278F366B1FBE6C99311427E7AE75314BC37A4B386599F66721320D41DEEA22','5EC3CECA85EA750360C739AD1A73BFE31E525AB9137A36103889DF43BE8D5921',3,11175),
(332,N'1PLSA100G0101',N'소프트 그릭요거트 100g',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 크리스피 그래놀라 토핑 (3개입), 토핑 선택 2: 블루베리 콩포트 토핑 (3개입)','4327C5F296A3A1A0A84071CB341FC6FEE438D523E8926E944D2F28CAFFD2D9B7','886FC4622DC8A4FADBBEC0EE6F820B07246B68096D242F34768AA9DF9D104E46',3,11175),
(332,N'1TPTA020G0302',N'크리스피그래놀라 토핑 (20g x 3개)',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 크리스피 그래놀라 토핑 (3개입), 토핑 선택 2: 블루베리 콩포트 토핑 (3개입)','66A54D261370838771BDDDED4975B113A8327B9BCED1739BDC2CB46BB5C4D368','569C3316910E2FAB1CB65D42B489871C52C4AE0CE6874D9F0339BC73215CE469',1,3725),
(332,N'1TPTA030G0301',N'블루베리콩포트 토핑 (30g x 3개)',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 크리스피 그래놀라 토핑 (3개입), 토핑 선택 2: 블루베리 콩포트 토핑 (3개입)','2DE0875E94C78146404D7706F376D657BCE7A056B1566364F26CC62EBE412058','B969DDF651E7B97F2483293207119CBCD7B030A396EC18DA173250C72F20D98B',1,3725),
(333,N'1PLPA100G0101',N'플레인 그릭요거트 100g',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 블루베리 콩포트 토핑 (3개입), 토핑 선택 2: 크리스피 그래놀라 토핑 (3개입)','915806859814D35F0EC3AF6713E8D3EF21562CABF914C3C6AFC0EB7E70A6B1E7','01B886D1164121682FA9E8D5263EBB2FF300143515CC3E2283928C2ABAA75671',3,11175),
(333,N'1PLSA100G0101',N'소프트 그릭요거트 100g',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 블루베리 콩포트 토핑 (3개입), 토핑 선택 2: 크리스피 그래놀라 토핑 (3개입)','B11CFD1C5F085F0A55DE7B69B70F607083A38022FDACBED9DE57FE003A22846A','8D01338B7C7A08474DE280AF777A3E628FAB09FC11AED62811FAA15B780A9DF0',3,11175),
(333,N'1TPTA030G0301',N'블루베리콩포트 토핑 (30g x 3개)',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 블루베리 콩포트 토핑 (3개입), 토핑 선택 2: 크리스피 그래놀라 토핑 (3개입)','70EC7DE0DF376CCCA20BE137448ECFDEAC254EDB5526FB6013A8DE03C674DE77','6A90539E617A2461EF196AD1421DBD1911E6F97A480C2CABE069785259BEC6E7',1,3725),
(333,N'1TPTA020G0302',N'크리스피그래놀라 토핑 (20g x 3개)',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 블루베리 콩포트 토핑 (3개입), 토핑 선택 2: 크리스피 그래놀라 토핑 (3개입)','97A2F9598E6C698934A25BE847A1E7AE138F304BD0A346408497EE56925819C5','71EA6628023DA430EAD3DBE333BED13A2CA27D7C6E14743DABD76062BA487D51',1,3725),
(334,N'1PLPA100G0101',N'플레인 그릭요거트 100g',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 블루베리 콩포트 토핑 (3개입), 토핑 선택 2: 크리스피 그래놀라 토핑 (3개입)','915806859814D35F0EC3AF6713E8D3EF21562CABF914C3C6AFC0EB7E70A6B1E7','9B3E0A9777C88DF68FED6E63A4FB9422E3B574A1353A6B969931B12DB40FD910',3,11175),
(334,N'1PLSA100G0101',N'소프트 그릭요거트 100g',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 블루베리 콩포트 토핑 (3개입), 토핑 선택 2: 크리스피 그래놀라 토핑 (3개입)','B11CFD1C5F085F0A55DE7B69B70F607083A38022FDACBED9DE57FE003A22846A','EB3BF1789C3C91971EE1957E14BCBCB37CCAEE563A20F0073BB2F116BB189E4F',3,11175),
(334,N'1TPTA030G0301',N'블루베리콩포트 토핑 (30g x 3개)',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 블루베리 콩포트 토핑 (3개입), 토핑 선택 2: 크리스피 그래놀라 토핑 (3개입)','70EC7DE0DF376CCCA20BE137448ECFDEAC254EDB5526FB6013A8DE03C674DE77','FA54E65739F88FBC2E8E6D0E48769078A785617C83B5701AFF96B9F375933375',1,3725),
(334,N'1TPTA020G0302',N'크리스피그래놀라 토핑 (20g x 3개)',N'세트 분리: 저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2) | 그릭요거트 선택: 플레인 (3개) + 소프트 (3개), 토핑 선택 1: 블루베리 콩포트 토핑 (3개입), 토핑 선택 2: 크리스피 그래놀라 토핑 (3개입)','97A2F9598E6C698934A25BE847A1E7AE138F304BD0A346408497EE56925819C5','B570B147A287A71FF06FDB4CD0640A6B74F85167D3492F8AC131B6E8B4D345FA',1,3725);
-- 기존 미반영 세트만 분리, 반영 상태가 바뀌면 중단
IF (SELECT COUNT(*) FROM BM_EdiImportRow WHERE CorpId='1000' AND RowId IN(330,331,332,333,334) AND Applied=0 AND ItemCode IS NULL)<>5
 THROW 50001,N'세트 원본이 변경되었거나 이미 분리됨',1;
IF EXISTS(SELECT 1 FROM @Parts p JOIN BM_EdiImportRow r ON r.RowId=p.OldRowId JOIN BM_EdiItemMap m ON m.CorpId=r.CorpId AND m.CustCode=r.CustCode AND m.SourceKey=p.SourceKey WHERE m.ItemCode<>p.ItemCode OR m.Factor<>1)
 THROW 50002,N'기존 구성품 연결과 충돌',1;
INSERT BM_EdiItemMap(CorpId,CustCode,SourceKey,SourceCode,SourceName,ItemCode,Factor,ModId)
SELECT DISTINCT r.CorpId,r.CustCode,p.SourceKey,N'',p.SourceName,p.ItemCode,1,N'개발자'
FROM @Parts p JOIN BM_EdiImportRow r ON r.RowId=p.OldRowId
WHERE NOT EXISTS(SELECT 1 FROM BM_EdiItemMap m WHERE m.CorpId=r.CorpId AND m.CustCode=r.CustCode AND m.SourceKey=p.SourceKey);
INSERT CM_ItemPrice(CorpId,CustCode,ItemCode,PriceType,UseYn,CreId,CreDt,ModId,ModDt)
SELECT DISTINCT r.CorpId,r.CustCode,p.ItemCode,'20','Y',N'개발자',GETDATE(),N'개발자',GETDATE()
FROM @Parts p JOIN BM_EdiImportRow r ON r.RowId=p.OldRowId
WHERE NOT EXISTS(SELECT 1 FROM CM_ItemPrice m WHERE m.CorpId=r.CorpId AND m.CustCode=r.CustCode AND m.ItemCode=p.ItemCode AND m.PriceType='20');
INSERT BM_EdiImportRow(FileId,CorpId,SourceSheet,SourceRow,CustCode,ItemCode,SourceCode,SourceName,SourceOption,SourceKey,RowKey,OrderNo,CenterName,ShipToName,OrderDate,DeliveryDate,Qty,Factor,Amount,ErrorMessage,Applied,AmountFromPrice,ModId,ModDt)
SELECT r.FileId,r.CorpId,r.SourceSheet,r.SourceRow,r.CustCode,p.ItemCode,N'',p.SourceName,p.SourceOption,p.SourceKey,p.RowKey,r.OrderNo,r.CenterName,r.ShipToName,r.OrderDate,r.DeliveryDate,p.Qty,1,p.Amount,NULL,0,0,N'개발자',GETDATE()
FROM @Parts p JOIN BM_EdiImportRow r ON r.RowId=p.OldRowId;
DELETE FROM BM_EdiImportRow WHERE CorpId='1000' AND RowId IN(330,331,332,333,334);
COMMIT;