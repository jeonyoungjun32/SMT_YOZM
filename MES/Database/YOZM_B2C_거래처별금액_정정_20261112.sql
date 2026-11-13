USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRAN;
-- 업체별 원본 금액: 공통 금액과 다른 거래처 금액 유지
DECLARE @Prices TABLE(CustCode nvarchar(20), ItemCode nvarchar(200), Price decimal(18,4), ApplyDate date);
INSERT @Prices VALUES
(N'B2C_NAVER',N'1PLPA100G0301',11700.0000,N'2026-11-05'),
(N'B2C_NONGHYUP',N'1PLSA100G0101',4900.0000,N'2026-11-05'),
(N'B2C_NONGHYUP',N'1TPPA130G0101',4900.0000,N'2026-11-05'),
(N'B2C_NONGHYUP',N'1PLPA100G0101',3900.0000,N'2026-11-05'),
(N'B2C_OLIVE',N'1PLSA100G0101',3500.0000,N'2026-11-05'),
(N'B2C_CAFE24',N'1PLPA450G0101',15000.0000,N'2026-11-05'),
(N'B2C_CAFE24',N'1DRPA1.8L0101',7700.0000,N'2026-11-05'),
(N'B2C_CAFE24',N'1PLPA100G0301',11700.0000,N'2026-11-05'),
(N'B2C_COSTCO',N'1PLPA450G0101',8894.0000,N'2026-11-05'),
(N'B2C_COSTCO',N'1TPPA130G0101',2908.0000,N'2026-11-05'),
(N'B2C_COSTCO',N'1TPSA120G0102',2908.0000,N'2026-11-05'),
(N'B2C_COSTCO',N'1FBAX100G0101',2582.3333,N'2026-11-05'),
(N'B2C_COSTCO',N'1PLSA500G0101',8300.0000,N'2026-11-05'),
(N'B2C_PUDDING',N'1PLPA3.0K0101',66000.0000,N'2026-11-05'),
(N'B2C_KAKAO',N'1PLPA100G0101',2519.5000,N'2026-11-05'),
(N'B2C_KAKAO',N'1PLSA100G0101',2363.3333,N'2026-11-05'),
(N'B2C_KAKAO',N'1TPTA030G0301',5078.0000,N'2026-11-05'),
(N'B2C_KAKAO',N'1TPTA020G0301',3280.0000,N'2026-11-05'),
(N'B2C_KAKAO',N'1TPTA020G0302',5079.0000,N'2026-11-05'),
(N'B2C_KAKAO',N'1TPTA020G0303',3283.0000,N'2026-11-05');
IF EXISTS (SELECT 1 FROM @Prices v LEFT JOIN CM_ItemPrice p ON p.CorpId='1000' AND p.CustCode=v.CustCode AND p.ItemCode=v.ItemCode AND p.PriceType='20' GROUP BY v.CustCode,v.ItemCode HAVING COUNT(p.Seq)<>1) THROW 50001,N'거래처별 적용기간 중복 확인 필요',1;
UPDATE p SET Price=v.Price, ApplyDate=CASE WHEN p.ApplyDate>v.ApplyDate THEN v.ApplyDate ELSE p.ApplyDate END, ModId=N'개발자',ModDt=GETDATE()
OUTPUT inserted.CustCode,inserted.ItemCode,deleted.Price BeforePrice,inserted.Price AfterPrice
FROM CM_ItemPrice p JOIN @Prices v ON v.CustCode=p.CustCode AND v.ItemCode=p.ItemCode WHERE p.CorpId='1000' AND p.PriceType='20' AND (p.Price IS NULL OR p.Price<>v.Price OR p.ApplyDate>v.ApplyDate);
-- 저장된 자동 계산 금액만 최신 거래처 금액으로 보완, 원본 금액 유지
UPDATE r SET Amount=ROUND(r.Qty*r.Factor*v.Price,0),ModId=N'개발자',ModDt=GETDATE() FROM BM_EdiImportRow r JOIN @Prices v ON v.CustCode=r.CustCode AND v.ItemCode=r.ItemCode WHERE r.CorpId='1000' AND r.OrderDate>=v.ApplyDate AND r.AmountFromPrice=1;
-- 카카오 11월 5일: 수주 상세의 품목별 합계로 정정, 배송료 제외
DECLARE @Kakao TABLE(ItemCode nvarchar(200), Qty decimal(19,4), Amount decimal(19,4));
INSERT @Kakao VALUES (N'1PLPA100G0101',12,30234),(N'1PLSA100G0101',12,28360),(N'1TPTA030G0301',3,15234),(N'1TPTA020G0301',1,3280),(N'1TPTA020G0302',3,15237),(N'1TPTA020G0303',1,3283);
IF EXISTS(SELECT 1 FROM @Kakao k OUTER APPLY(SELECT SUM(r.Qty*r.Factor) Qty FROM BM_EdiImportRow r JOIN BM_EdiImportFile f ON f.FileId=r.FileId WHERE r.CorpId='1000' AND r.CustCode='B2C_KAKAO' AND f.FileName=N'20261105_카카오.xlsx' AND r.OrderDate='20261105' AND r.ItemCode=k.ItemCode) x WHERE x.Qty IS NULL OR x.Qty<>k.Qty) THROW 50001,N'카카오 구성품 수량 불일치',1;
;WITH Allocation AS (
SELECT r.RowId,k.Amount,k.Qty TotalQty,r.Qty*r.Factor RowQty,SUM(r.Qty*r.Factor) OVER(PARTITION BY r.FileId,r.ItemCode ORDER BY r.SourceRow,r.RowId ROWS UNBOUNDED PRECEDING) CumulativeQty FROM BM_EdiImportRow r JOIN BM_EdiImportFile f ON f.FileId=r.FileId JOIN @Kakao k ON k.ItemCode=r.ItemCode WHERE r.CorpId='1000' AND r.CustCode='B2C_KAKAO' AND f.FileName=N'20261105_카카오.xlsx' AND r.OrderDate='20261105'
)
UPDATE r SET Amount=ROUND(a.Amount*a.CumulativeQty/a.TotalQty,0)-ROUND(a.Amount*(a.CumulativeQty-a.RowQty)/a.TotalQty,0),AmountFromPrice=0,ModId=N'개발자',ModDt=GETDATE() FROM BM_EdiImportRow r JOIN Allocation a ON a.RowId=r.RowId;
COMMIT;
