using System;
using System.Collections.Generic;

namespace SmartMES.View.Dashboard
{
    /// <summary>
    /// 실시간 종합현황판 데이터 모델.
    /// DB 조회 후 ComprehensiveRepository.LoadAll() 로 채워서 frmComprehensiveDashboard.UpdateUI() 에 전달.
    /// </summary>
    public class ComprehensiveData
    {
        // ── 생산현황 (품목그룹별) ─────────────────────────────
        public List<ProductionGroupItem> ProductionGroups { get; set; }
        public int   ProductionTotal    { get; set; }
        public double WeeklyAchieveRate { get; set; }   // 이번주 달성율 (%)

        // ── 보전지수 ──────────────────────────────────────────
        public double MaintenanceIndex  { get; set; }   // 00.0
        public int    AccumHours        { get; set; }   // 누적 가동시간 (H)

        // ── OEE ───────────────────────────────────────────────
        public double OeeTotal          { get; set; }   // 전체 OEE (%)
        public bool   OeeWarning        { get; set; }   // 점검필요 여부

        public double OeeSachulTime     { get; set; }   // 사출 시간가동율 T
        public double OeeSachulPerf     { get; set; }   // 사출 성능가동율 P
        public double OeeSachulYield    { get; set; }   // 사출 양품율    G
        public double OeeJoripTime      { get; set; }   // 조립 시간가동율 T
        public double OeeJoripPerf      { get; set; }   // 조립 성능가동율 P
        public double OeeJoripYield     { get; set; }   // 조립 양품율    G

        // ── 품질현황 ──────────────────────────────────────────
        public double QualityRate       { get; set; }   // 양품율 (당월, %)
        public double EorRate           { get; set; }   // EOR 불량출하율 (%)
        public List<WorstEquipItem> WorstEquipList { get; set; }

        // ── 납품현황 ──────────────────────────────────────────
        public double GrRate            { get; set; }   // 입고율 (%)
        public double TgRate            { get; set; }   // 목표율 (%)
        public string LastRetrieveTime  { get; set; }
        public int    DoCnt             { get; set; }   // 발주건수
        public int    GrCnt             { get; set; }   // 입고건수
        public int    TgCnt             { get; set; }   // 목표건수
        public int    TgOk              { get; set; }   // 달성건수

        // ── 재고현황 ──────────────────────────────────────────
        public List<InventoryItem> InventoryList { get; set; }
        public double StockRate         { get; set; }   // 재고율 (%)

        // ════════════════════════════════════════════════════
        /// <summary>UI 테스트용 샘플 데이터</summary>
        public static ComprehensiveData CreateSample()
        {
            return new ComprehensiveData
            {
                ProductionGroups = new List<ProductionGroupItem>
                {
                    new ProductionGroupItem { Name = "냉장고", Count = 20134, Rate = 47 },
                    new ProductionGroupItem { Name = "에어컨", Count = 11029, Rate = 26 },
                    new ProductionGroupItem { Name = "세탁기", Count =  6783, Rate = 16 },
                    new ProductionGroupItem { Name = "청소기", Count =  4712, Rate = 11 },
                    new ProductionGroupItem { Name = "기타",   Count =   368, Rate =  1 }
                },
                ProductionTotal    = 75465,
                WeeklyAchieveRate  = 116.0,

                MaintenanceIndex   = 0.0,
                AccumHours         = 913,

                OeeTotal           = 109.49,
                OeeWarning         = true,
                OeeSachulTime      = 98.42, OeeSachulPerf  = 107.13, OeeSachulYield = 98.59,
                OeeJoripTime       = 83.17, OeeJoripPerf   = 124.64, OeeJoripYield  = 100.0,

                QualityRate        = 99.58,
                EorRate            = 64.86,
                WorstEquipList = new List<WorstEquipItem>
                {
                    new WorstEquipItem { EquipName = "사출기 13호기", EquipType = "사출기", BadRate = 6.3,  BadCount = 1079 },
                    new WorstEquipItem { EquipName = "사출기 13호기", EquipType = "사출기", BadRate = 5.9,  BadCount = 2047 },
                    new WorstEquipItem { EquipName = "사출조립 2호기",EquipType = "사출조립",BadRate = 5.4, BadCount =  723 },
                    new WorstEquipItem { EquipName = "사출기 12호기", EquipType = "사출기", BadRate = 4.1,  BadCount = 1982 },
                    new WorstEquipItem { EquipName = "사출기 18호기", EquipType = "사출기", BadRate = 3.8,  BadCount = 1561 }
                },

                GrRate             = 71.0,
                TgRate             = 68.0,
                LastRetrieveTime   = "2026-03-31 17:11:23",
                DoCnt = 275, GrCnt = 194, TgCnt = 250, TgOk = 170,

                InventoryList = new List<InventoryItem>
                {
                    new InventoryItem { Name = "에어컨", Day5 = 221210, Day15 = 496931, Day16Over = 0 },
                    new InventoryItem { Name = "냉장고", Day5 = 363884, Day15 = 1003392,Day16Over = 0 },
                    new InventoryItem { Name = "세탁기", Day5 = 206445, Day15 = 260277, Day16Over = 0 },
                    new InventoryItem { Name = "기타",   Day5 =   9911, Day15 =  21274, Day16Over = 0 }
                },
                StockRate = 10.0
            };
        }
    }

    public class ProductionGroupItem
    {
        public string Name  { get; set; }
        public int    Count { get; set; }
        public int    Rate  { get; set; }   // 구성비율 (%)
    }

    public class WorstEquipItem
    {
        public string EquipName { get; set; }
        public string EquipType { get; set; }
        public double BadRate   { get; set; }
        public int    BadCount  { get; set; }
    }

    public class InventoryItem
    {
        public string Name      { get; set; }
        public int    Day5      { get; set; }
        public int    Day15     { get; set; }
        public int    Day16Over { get; set; }
    }
}
