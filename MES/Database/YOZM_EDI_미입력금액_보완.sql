USE YOZM;
SET XACT_ABORT ON;
BEGIN TRAN;
-- 원본 금액이 없는 행만 보완, 포케스팅 반영 상태는 유지
UPDATE r
SET Amount=ROUND(r.Qty*r.Factor*COALESCE(p.Price,NULLIF(i.Amount,0)),0), AmountFromPrice=1
OUTPUT inserted.RowId,inserted.Amount
FROM BM_EdiImportRow r
JOIN CM_Item i ON i.CorpId=r.CorpId AND i.ItemCode=r.ItemCode
OUTER APPLY
(
 SELECT MIN(v.Price) Price FROM
 (
  SELECT TOP(1) WITH TIES Price FROM CM_ItemPrice
  WHERE CorpId=r.CorpId AND CustCode=r.CustCode AND ItemCode=r.ItemCode
    AND PriceType='20' AND ISNULL(UseYn,'Y')='Y'
    AND (ApplyDate IS NULL OR ApplyDate<=r.OrderDate)
    AND (EndDate IS NULL OR EndDate>=r.OrderDate)
  ORDER BY ApplyDate DESC
 )v HAVING COUNT(DISTINCT v.Price)=1 AND MIN(v.Price)>0
)p
WHERE r.CorpId='1000' AND r.Amount IS NULL
  AND COALESCE(p.Price,NULLIF(i.Amount,0))>0 AND r.Qty IS NOT NULL AND r.Factor IS NOT NULL;
COMMIT;