-- 3PL 통계 현황_실 발주량.xlsx의 2026년 데이터 시트에서 거래처별 품명만 가져옵니다.
USE [YOZM];
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF OBJECT_ID(N'dbo.PM_ForecastCustomerItem', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PM_ForecastCustomerItem
    (
        CorpId nvarchar(10) NOT NULL,
        CustCode nvarchar(20) NOT NULL,
        ChannelName nvarchar(100) NOT NULL,
        ItemName nvarchar(200) NOT NULL,
        SourceOrder int NOT NULL,
        CONSTRAINT PK_PM_ForecastCustomerItem PRIMARY KEY (CorpId, CustCode, ChannelName, ItemName)
    );
END;

INSERT dbo.PM_ForecastCustomerItem (CorpId, CustCode, ChannelName, ItemName, SourceOrder)
SELECT c.CorpId, src.CustCode, src.ChannelName, src.ItemName, src.SourceOrder
FROM (VALUES
    (N'B2B003', N'홈플러스 EX', N'플레인 그릭요거트', 47),
    (N'B2B003', N'홈플러스 EX', N'알룰로스', 48),
    (N'B2B003', N'홈플러스 EX', N'홈플PB 플레인', 49),
    (N'B2B003', N'홈플러스 EX', N'홈플PB 블루베리', 50),
    (N'B2B003', N'홈플러스', N'플레인 그릭요거트', 51),
    (N'B2B003', N'홈플러스', N'블루베리콩포트', 52),
    (N'B2B003', N'홈플러스', N'벨베티크리스피', 53),
    (N'B2B003', N'홈플러스', N'황치즈', 54),
    (N'B2B003', N'홈플러스', N'알룰로스', 55),
    (N'B2B003', N'홈플러스', N'홈플PB 플레인', 56),
    (N'B2B003', N'홈플러스', N'홈플PB 블루베리', 57),
    (N'B2B002', N'이마트 ED', N'플레인 그릭요거트', 58),
    (N'B2B002', N'이마트 ED', N'블루베리콩포트', 59),
    (N'B2B002', N'이마트 ED', N'카카오그래놀라', 60),
    (N'B2B002', N'이마트 ED', N'애플시나몬', 61),
    (N'B2B002', N'이마트 ED', N'플레인 소프트', 62),
    (N'B2B002', N'이마트 ED', N'말차', 63),
    (N'B2B004', N'롯데슈퍼', N'플레인 그릭요거트', 64),
    (N'B2B004', N'롯데슈퍼', N'블루베리콩포트', 65),
    (N'B2B004', N'롯데슈퍼', N'복숭아콩포트', 66),
    (N'B2B004', N'롯데슈퍼', N'알룰로스', 67),
    (N'B2B004', N'롯데슈퍼', N'플레인 소프트', 68),
    (N'B2B004', N'롯데슈퍼', N'말차', 69),
    (N'B2B004', N'롯데슈퍼', N'애플시나몬', 70),
    (N'B2B004', N'롯데슈퍼', N'블루베리 100g', 71),
    (N'B2B004', N'롯데슈퍼', N'마시는 그릭요거트 750ml', 72),
    (N'B2B005', N'롯데마트', N'플레인 그릭요거트', 73),
    (N'B2B005', N'롯데마트', N'블루베리콩포트', 74),
    (N'B2B005', N'롯데마트', N'복숭아콩포트', 75),
    (N'B2B005', N'롯데마트', N'카카오그래놀라', 76),
    (N'B2B005', N'롯데마트', N'알룰로스', 77),
    (N'B2B005', N'롯데마트', N'애플시나몬', 78),
    (N'B2B005', N'롯데마트', N'말차', 79),
    (N'B2B005', N'롯데마트', N'카페라떼', 80),
    (N'B2B005', N'롯데마트', N'푸룬', 81),
    (N'B2B005', N'롯데마트', N'블루베리 100g', 82),
    (N'B2B005', N'롯데마트', N'플레인 소프트', 83),
    (N'B2B005', N'롯데마트', N'플레인 450g', 84),
    (N'B2B005', N'롯데마트', N'플레인 800g', 85),
    (N'B2B005', N'롯데마트', N'마시는 그릭요거트 750ml', 86),
    (N'B2B005', N'롯데마트', N'오 그래놀라', 87),
    (N'B2B005', N'롯데마트', N'벨베티초코소보로', 88),
    (N'B2B013', N'킴스클럽', N'플레인 그릭요거트', 89),
    (N'B2B013', N'킴스클럽', N'블루베리콩포트', 90),
    (N'B2B013', N'킴스클럽', N'알룰로스', 91),
    (N'B2B013', N'킴스클럽', N'플레인 소프트', 92),
    (N'B2B013', N'킴스클럽', N'말차', 93),
    (N'B2B019', N'아워홈', N'플레인 그릭요거트', 112),
    (N'B2B019', N'아워홈', N'블루베리콩포트', 113),
    (N'B2B019', N'아워홈', N'벨베티크리스피', 114),
    (N'B2B019', N'아워홈', N'벨베티초코소보로', 115),
    (N'B2B019', N'아워홈', N'플레인 소프트', 116),
    (N'B2B018', N'삼성웰스토리', N'플레인 그릭요거트', 127),
    (N'B2B018', N'삼성웰스토리', N'블루베리콩포트', 128),
    (N'B2B018', N'삼성웰스토리', N'카카오그래놀라', 129),
    (N'B2B018', N'삼성웰스토리', N'벨베티크리스피', 130),
    (N'B2B018', N'삼성웰스토리', N'플레인 소프트', 131),
    (N'B2B018', N'삼성웰스토리', N'알룰로스', 132),
    (N'B2B018', N'삼성웰스토리', N'플레인 450g', 133),
    (N'B2B018', N'삼성웰스토리', N'말차', 134),
    (N'B2B001', N'이마트24', N'플레인 그릭요거트', 135),
    (N'B2B001', N'이마트24', N'블루베리콩포트', 136),
    (N'B2B001', N'이마트24', N'카카오그래놀라', 137),
    (N'B2B001', N'이마트24', N'벨베티초코소보로', 138),
    (N'B2B001', N'이마트24', N'오 그래놀라', 139),
    (N'B2B001', N'이마트24', N'플레인 소프트', 140),
    (N'B2B001', N'이마트24', N'푸룬', 141),
    (N'B2B001', N'이마트24', N'마시는 그릭요거트 750ml', 142),
    (N'B2B007', N'세븐일레븐', N'헬로키티 플레인', 143),
    (N'B2B007', N'세븐일레븐', N'스트로베리콩포트', 144),
    (N'B2B007', N'세븐일레븐', N'블루베리콩포트', 145),
    (N'B2B007', N'세븐일레븐', N'복숭아콩포트', 146),
    (N'B2B007', N'세븐일레븐', N'우베코코넛요거트 100g', 147),
    (N'B2B007', N'세븐일레븐', N'아사이베리 파르페 220ml', 148),
    (N'B2B010', N'CU', N'애플시나몬', 149),
    (N'B2B010', N'CU', N'벨베티크리스피', 150),
    (N'B2B010', N'CU', N'카페라떼', 151),
    (N'B2B010', N'CU', N'푸룬', 152),
    (N'B2B010', N'CU', N'요요하마 플레인', 153),
    (N'B2B010', N'CU', N'패트와매트 플레인', 154),
    (N'B2B010', N'CU', N'핑루 블루베리(CU)', 155),
    (N'B2B010', N'CU', N'마시는 그릭요거트 750ml', 156),
    (N'B2B010', N'CU', N'아사이베리 파르페 220ml', 157),
    (N'B2B011', N'GS25', N'춘식이 플레인', 158),
    (N'B2B011', N'GS25', N'블루베리콩포트', 159),
    (N'B2B011', N'GS25', N'카카오그래놀라', 160),
    (N'B2B011', N'GS25', N'푸룬', 161),
    (N'B2B011', N'GS25', N'말차', 162),
    (N'B2B011', N'GS25', N'알룰로스', 163),
    (N'B2B011', N'GS25', N'아사이볼 100g', 164),
    (N'B2B011', N'GS25', N'마시는 그릭요거트 750ml', 165),
    (N'B2B011', N'GS25', N'아사이베리 파르페 220ml', 166),
    (N'B2B012', N'GS슈퍼', N'플레인 그릭요거트', 167),
    (N'B2B012', N'GS슈퍼', N'블루베리콩포트', 168),
    (N'B2B012', N'GS슈퍼', N'말차', 169)
) src(CustCode, ChannelName, ItemName, SourceOrder)
INNER JOIN dbo.CM_Cust c ON c.CorpId = N'1000' AND c.CustCode = src.CustCode
WHERE NOT EXISTS
(
    SELECT 1
    FROM dbo.PM_ForecastCustomerItem f WITH (UPDLOCK, HOLDLOCK)
    WHERE f.CorpId = c.CorpId AND f.CustCode = src.CustCode
      AND f.ChannelName = src.ChannelName AND f.ItemName = src.ItemName
);
COMMIT TRANSACTION;
