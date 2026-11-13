USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRAN;
-- 판매 금액 우선, 판매 금액이 없는 품목은 구매 금액 사용
;WITH Candidates AS
(
    SELECT p.*, ROW_NUMBER() OVER
    (
        PARTITION BY p.CorpId, p.ItemCode
        ORDER BY CASE p.PriceType WHEN '20' THEN 0 ELSE 1 END,
                 p.ApplyDate DESC, COALESCE(p.ModDt,p.CreDt) DESC, p.Seq DESC, p.CustCode
    ) AS RowNo
    FROM dbo.CM_ItemPrice p
    WHERE p.CorpId='1000' AND p.PriceType IN ('10','20') AND p.Price>0
      AND ISNULL(p.UseYn,'Y')='Y'
      AND (p.ApplyDate IS NULL OR p.ApplyDate<DATEADD(day,1,CONVERT(date,GETDATE())))
      AND (p.EndDate IS NULL OR p.EndDate>=CONVERT(date,GETDATE()))
)
-- 일치하는 금액은 동일한 값, 차이가 있으면 최신 적용 금액 선택
UPDATE i
SET Amount=p.Price, ModId=N'개발자', ModDt=GETDATE()
OUTPUT inserted.ItemCode, deleted.Amount AS BeforeAmount, inserted.Amount AS AfterAmount
FROM dbo.CM_Item i
JOIN Candidates p ON p.CorpId=i.CorpId AND p.ItemCode=i.ItemCode AND p.RowNo=1
WHERE i.CorpId='1000' AND i.Amount IS NULL;
COMMIT;