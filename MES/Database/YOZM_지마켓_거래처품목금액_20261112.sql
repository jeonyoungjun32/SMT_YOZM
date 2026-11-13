USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRAN;
-- 수주 SO-2611050026의 지마켓 상품별 금액, 주문 할인 제외
DECLARE @Price TABLE(ItemCode nvarchar(200), Price decimal(18,4));
INSERT @Price VALUES
(N'1PLPA100G0101',3544),
(N'1PLSA100G0101',3180),
(N'1TPPA130G0101',4453),
(N'1FBAX100G0101',3817),
(N'1TPSA120G0101',4458);
IF EXISTS(SELECT 1 FROM @Price v LEFT JOIN dbo.CM_ItemPrice p ON p.CorpId='1000' AND p.CustCode='B2C_GMARKET' AND p.ItemCode=v.ItemCode AND p.PriceType='20' GROUP BY v.ItemCode HAVING COUNT(p.Seq)<>1)
    THROW 50001,N'지마켓 품목별 금액 이력 확인 필요',1;
UPDATE p
SET Price=v.Price,
    ApplyDate=CASE WHEN p.ApplyDate>'20261105' THEN '20261105' ELSE p.ApplyDate END,
    ModId=N'개발자', ModDt=GETDATE()
OUTPUT inserted.ItemCode,deleted.Price AS BeforePrice,inserted.Price AS AfterPrice
FROM dbo.CM_ItemPrice p
INNER JOIN @Price v ON v.ItemCode=p.ItemCode
WHERE p.CorpId='1000' AND p.CustCode='B2C_GMARKET' AND p.PriceType='20';
-- 기존 업로드의 구성품 금액 정정, 수량과 포케스팅 반영 상태 유지
UPDATE r
SET Amount=ROUND(r.Qty*r.Factor*v.Price,0),
    AmountFromPrice=0, ModId=N'개발자', ModDt=GETDATE()
FROM dbo.BM_EdiImportRow r
INNER JOIN dbo.BM_EdiImportFile f ON f.FileId=r.FileId AND f.CorpId=r.CorpId
INNER JOIN @Price v ON v.ItemCode=r.ItemCode
WHERE r.CorpId='1000' AND r.CustCode='B2C_GMARKET'
    AND f.FileName=N'20261105_지마켓.xlsx' AND r.OrderDate='20261105'
    AND r.SourceOption LIKE N'세트 분리:%';
COMMIT;