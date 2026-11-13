USE YOZM;
SET XACT_ABORT ON;
BEGIN TRAN;
DECLARE @P TABLE(ItemCode nvarchar(200),Price decimal(18,4),ApplyDate date);
INSERT @P VALUES
('1FBAX100G0101',2400,'2026-10-06'),
('1FBGA100G0101',2500,'2026-10-12'),
('1FBLA100G0101',2400,'2026-10-06'),
('1PLPA100G0101',2000,'2026-10-06'),
('1PLPA450G0101',8800,'2026-10-06'),
('1PLSA100G0101',1850,'2026-10-06'),
('1PLSA500G0101',8300,'2026-10-06'),
('1TPPA120G0101',2800,'2026-10-06'),
('1TPPA130G0101',2800,'2026-10-06'),
('1TPPA130G0102',2800,'2026-10-12'),
('1TPPX130G0102',2800,'2026-10-06'),
('1TPPX130G0103',2800,'2026-10-14'),
('1TPSA120G0101',2800,'2026-10-06'),
('1TPSA120G0102',2800,'2026-10-06'),
('1TPTA020G0301',2300,'2026-10-23'),
('1TPTA020G0302',2300,'2026-10-06'),
('1TPTA020G0303',2300,'2026-10-19'),
('1TPTA030G0301',2300,'2026-10-12'),
('1TPTA030G0302',2300,'2026-10-12');
IF EXISTS(SELECT 1 FROM @P v LEFT JOIN CM_Item i ON i.CorpId='1000' AND i.ItemCode=v.ItemCode WHERE i.ItemCode IS NULL) THROW 50001,N'미등록 품목 확인 필요',1;
IF EXISTS(SELECT 1 FROM CM_ItemPrice p JOIN @P v ON v.ItemCode=p.ItemCode WHERE p.CorpId='1000' AND p.CustCode='B2C_JNH' AND p.PriceType='20' GROUP BY p.ItemCode HAVING COUNT(*)>1) THROW 50001,N'금액 이력 중복 확인 필요',1;
UPDATE p SET Price=v.Price,ApplyDate=CASE WHEN p.ApplyDate>v.ApplyDate THEN v.ApplyDate ELSE p.ApplyDate END,ModId=N'개발자',ModDt=GETDATE()
FROM CM_ItemPrice p JOIN @P v ON v.ItemCode=p.ItemCode WHERE p.CorpId='1000' AND p.CustCode='B2C_JNH' AND p.PriceType='20';
INSERT CM_ItemPrice(CorpId,ItemCode,CustCode,PriceType,Price,ApplyDate,UseYn,CreId,CreDt,ModId,ModDt)
SELECT '1000',v.ItemCode,'B2C_JNH','20',v.Price,v.ApplyDate,'Y',N'개발자',GETDATE(),N'개발자',GETDATE()
FROM @P v WHERE NOT EXISTS(SELECT 1 FROM CM_ItemPrice p WHERE p.CorpId='1000' AND p.CustCode='B2C_JNH' AND p.ItemCode=v.ItemCode AND p.PriceType='20');
-- 원본·직접 입력 금액은 유지하고 기존 자동 계산 행만 보완
UPDATE r SET Amount=ROUND(r.Qty*r.Factor*p.Price,0),AmountFromPrice=1,ModId=N'개발자',ModDt=GETDATE()
FROM BM_EdiImportRow r JOIN @P p ON p.ItemCode=r.ItemCode
WHERE r.CorpId='1000' AND r.CustCode='B2C_JNH' AND r.OrderDate>=p.ApplyDate AND (r.Amount IS NULL OR r.AmountFromPrice=1);
SELECT COUNT(*) PriceItems FROM CM_ItemPrice WHERE CorpId='1000' AND CustCode='B2C_JNH' AND PriceType='20';
SELECT COUNT(*) Rows,SUM(r.Qty*r.Factor) Qty,SUM(r.Amount) Amount FROM BM_EdiImportRow r JOIN BM_EdiImportFile f ON f.FileId=r.FileId WHERE r.CorpId='1000' AND f.FileName=N'20261105_제이앤에이치그룹(B2C 공통양식).xlsx';
COMMIT;