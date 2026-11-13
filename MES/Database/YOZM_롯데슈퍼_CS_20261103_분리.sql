USE YOZM;
SET XACT_ABORT ON;
BEGIN TRAN;
DECLARE @S TABLE(ItemCode nvarchar(200),Qty int,Total int);
INSERT @S VALUES('1TPPA130G0101',185,185),('1TPPX130G0102',15,18),('1FBGA100G0101',13,16),('1TPPX130G0103',26,32),('1DRPA750M0101',4,248),('1PLSA100G0101',10,147);
IF EXISTS(SELECT 1 FROM @S s LEFT JOIN PM_ForecastOrderActual a ON a.CorpId='1000' AND a.CustCode='B2B004' AND a.ItemCode=s.ItemCode AND a.OrderDate='20261103' WHERE a.OrderQty IS NULL OR a.OrderQty<>s.Total)
 THROW 50000,N'현재 수량과 원본 불일치',1;
IF EXISTS(SELECT 1 FROM PM_ForecastOrderActual WHERE CorpId='1000' AND CustCode='B2B006' AND OrderDate='20261103') THROW 50001,N'이미 분리된 내역 존재',1;
INSERT CM_ItemPrice(CorpId,CustCode,ItemCode,PriceType,ChannelName,UseYn,SourceOrder,CreId,CreDt)
SELECT '1000','B2B006',s.ItemCode,'20',N'롯데슈퍼(씨에스유통)','Y',
(SELECT MIN(SourceOrder) FROM CM_ItemPrice p WHERE p.CorpId='1000' AND p.CustCode='B2B004' AND p.ItemCode=s.ItemCode),N'개발자',GETDATE()
FROM @S s WHERE NOT EXISTS(SELECT 1 FROM CM_ItemPrice p WHERE p.CorpId='1000' AND p.CustCode='B2B006' AND p.ItemCode=s.ItemCode AND p.PriceType='20');
INSERT PM_ForecastOrderActual(CorpId,CustCode,ItemCode,ChannelName,ItemName,OrderDate,OrderQty)
SELECT '1000','B2B006',s.ItemCode,N'롯데슈퍼(씨에스유통)',i.ItemName,'20261103',s.Qty FROM @S s JOIN CM_Item i ON i.CorpId='1000' AND i.ItemCode=s.ItemCode;
UPDATE a SET OrderQty=a.OrderQty-s.Qty FROM PM_ForecastOrderActual a JOIN @S s ON s.ItemCode=a.ItemCode WHERE a.CorpId='1000' AND a.CustCode='B2B004' AND a.OrderDate='20261103';
COMMIT;
SELECT CustCode,SUM(OrderQty) Qty FROM PM_ForecastOrderActual WHERE CorpId='1000' AND CustCode IN('B2B004','B2B006') AND OrderDate='20261103' GROUP BY CustCode;
