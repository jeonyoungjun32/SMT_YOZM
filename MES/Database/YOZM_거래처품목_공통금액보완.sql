USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRAN;
-- 사용자 승인: 거래처 금액 미등록 항목에 품목 공통 금액 복사
-- 기존 금액(0 포함)과 적용기간은 유지
UPDATE p
SET Price=i.Amount, ModId=N'개발자', ModDt=GETDATE()
OUTPUT inserted.Seq, inserted.CustCode, inserted.ItemCode, deleted.Price AS BeforePrice, inserted.Price AS AfterPrice
FROM dbo.CM_ItemPrice p
JOIN dbo.CM_Item i ON i.CorpId=p.CorpId AND i.ItemCode=p.ItemCode
WHERE p.CorpId='1000' AND p.PriceType='20' AND p.Price IS NULL AND i.Amount>0;
COMMIT;