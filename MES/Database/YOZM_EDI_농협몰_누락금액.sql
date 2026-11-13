USE YOZM;
SET XACT_ABORT ON;
BEGIN TRAN;
-- 농협몰 원본 금액 / 묶음 수량: 24,500/5, 23,400/6
INSERT CM_ItemPrice(CorpId,CustCode,ItemCode,PriceType,Price,ApplyDate,UseYn,CreId,CreDt,ModId,ModDt)
SELECT '1000','B2C_NONGHYUP',v.ItemCode,'20',v.Price,'20261105','Y',N'개발자',GETDATE(),N'개발자',GETDATE()
FROM (VALUES ('1TPPA130G0101',4900),('1PLPA100G0101',3900)) v(ItemCode,Price)
WHERE NOT EXISTS(SELECT 1 FROM CM_ItemPrice p WITH(UPDLOCK,HOLDLOCK) WHERE p.CorpId='1000' AND p.CustCode='B2C_NONGHYUP' AND p.ItemCode=v.ItemCode AND p.PriceType='20');
SELECT @@ROWCOUNT AS AddedPrices;
COMMIT;