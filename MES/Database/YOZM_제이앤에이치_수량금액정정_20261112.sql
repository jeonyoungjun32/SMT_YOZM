USE YOZM;
SET XACT_ABORT ON;
BEGIN TRAN;
DECLARE @P TABLE(ItemCode nvarchar(200), Qty int, Price decimal(18,4));
INSERT @P VALUES
('1PLPA100G0101',34,2000),('1PLPA450G0101',10,8800),('1PLSA100G0101',14,1850),
('1TPPA130G0101',4,2800),('1TPTA030G0301',2,2300),('1TPPX130G0102',4,2800),
('1FBAX100G0101',4,2400),('1TPPA120G0101',4,2800),('1TPSA120G0101',4,2800),
('1TPSA120G0102',4,2800),('1TPTA020G0302',11,2300),('1FBGA100G0101',3,2500);
DECLARE @File bigint=(SELECT MAX(FileId) FROM BM_EdiImportFile WHERE CorpId='1000' AND FileName=N'20261105_제이앤에이치그룹(B2C 공통양식).xlsx');
IF @File IS NULL THROW 50001,N'등록 파일 없음',1;
IF EXISTS(SELECT 1 FROM BM_EdiImportRow WHERE FileId=@File AND Applied=1) THROW 50001,N'기반영 수량 확인 필요',1;
IF EXISTS(SELECT 1 FROM @P v LEFT JOIN CM_ItemPrice p ON p.CorpId='1000' AND p.CustCode='B2C_JNH' AND p.ItemCode=v.ItemCode AND p.PriceType='20' GROUP BY v.ItemCode HAVING COUNT(p.Seq)<>1) THROW 50001,N'거래처 품목 금액 이력 확인 필요',1;
UPDATE p SET Price=v.Price,ApplyDate=CASE WHEN ApplyDate>'20261105' THEN '20261105' ELSE ApplyDate END,ModId=N'개발자',ModDt=GETDATE()
FROM CM_ItemPrice p JOIN @P v ON v.ItemCode=p.ItemCode WHERE p.CorpId='1000' AND p.CustCode='B2C_JNH' AND p.PriceType='20';
-- 다른 발주서 시트의 빨간 취소 주문도 제외, 금액 정정 유지
DELETE FROM BM_EdiImportRow
WHERE FileId=@File AND ItemCode='1TPSA120G0101'
    AND OrderNo IN ('32104007464802','32104011164512') AND Applied=0;
UPDATE r SET Amount=ROUND(r.Qty*r.Factor*p.Price,0),AmountFromPrice=1,ModId=N'개발자',ModDt=GETDATE()
FROM BM_EdiImportRow r JOIN @P p ON p.ItemCode=r.ItemCode WHERE r.FileId=@File;
IF EXISTS(SELECT 1 FROM @P p OUTER APPLY(SELECT SUM(Qty*Factor) Qty FROM BM_EdiImportRow WHERE FileId=@File AND ItemCode=p.ItemCode)x WHERE ISNULL(x.Qty,-1)<>p.Qty) THROW 50001,N'수주 상세와 품목별 수량 불일치',1;
SELECT COUNT(*) Rows,SUM(Qty*Factor) Qty,SUM(Amount) Amount FROM BM_EdiImportRow WHERE FileId=@File;
COMMIT;