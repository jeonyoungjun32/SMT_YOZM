using System;
using System.Data;
using System.Collections.Generic;
using SmartCommon.Common;

namespace SmartMES.View.Dashboard
{
    /// <summary>
    /// 실시간 종합현황 DB 조회 전담 클래스.
    /// frmCodeMember 의 codeMember / db 패턴과 동일.
    /// </summary>
    public class ComprehensiveRepository
    {
        private Database db = new Database();

        // ── 생산현황 (품목그룹별) ─────────────────────────────
        public DataTable SelectProductionByGroup(string corpId)
        {
            string query = $"exec Comprehensive_GetProductionByGroup '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 보전지수 ──────────────────────────────────────────
        public DataTable SelectMaintenanceIndex(string corpId)
        {
            string query = $"exec Comprehensive_GetMaintenanceIndex '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── OEE ───────────────────────────────────────────────
        public DataTable SelectOee(string corpId)
        {
            string query = $"exec Comprehensive_GetOee '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 품질현황 ──────────────────────────────────────────
        public DataTable SelectQuality(string corpId)
        {
            string query = $"exec Comprehensive_GetQuality '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 납품현황 ──────────────────────────────────────────
        public DataTable SelectDelivery(string corpId)
        {
            string query = $"exec Comprehensive_GetDelivery '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 재고현황 ──────────────────────────────────────────
        public DataTable SelectInventory(string corpId)
        {
            string query = $"exec Comprehensive_GetInventory '{corpId}'";
            return db.GetDataTable(query);
        }

        // ── 전체 통합 로드 ────────────────────────────────────
        /// <summary>
        /// SP 준비 완료 후 이 메서드 한 줄로 전환.
        /// 연결 실패 시 샘플 데이터 반환.
        /// </summary>
        public ComprehensiveData LoadAll(string corpId)
        {
            try
            {
                var data = new ComprehensiveData();

                // ── 생산현황 ──────────────────────────────────
                DataTable dtProd = SelectProductionByGroup(corpId);
                if (dtProd != null && dtProd.Rows.Count > 0)
                {
                    data.ProductionGroups = new List<ProductionGroupItem>();
                    int total = 0;
                    foreach (DataRow r in dtProd.Rows)
                    {
                        var item = new ProductionGroupItem
                        {
                            Name  = r["Name"]?.ToString()  ?? "",
                            Count = ToInt(r["Count"]),
                            Rate  = ToInt(r["Rate"])
                        };
                        data.ProductionGroups.Add(item);
                        total += item.Count;
                    }
                    data.ProductionTotal   = total;
                    data.WeeklyAchieveRate = ToDouble(dtProd.Rows[0]["WeeklyRate"]);
                }

                // ── 보전지수 ──────────────────────────────────
                DataTable dtMi = SelectMaintenanceIndex(corpId);
                if (dtMi != null && dtMi.Rows.Count > 0)
                {
                    data.MaintenanceIndex = ToDouble(dtMi.Rows[0]["IndexValue"]);
                    data.AccumHours       = ToInt(dtMi.Rows[0]["AccumHours"]);
                }

                // ── OEE ───────────────────────────────────────
                DataTable dtOee = SelectOee(corpId);
                if (dtOee != null && dtOee.Rows.Count > 0)
                {
                    data.OeeTotal   = ToDouble(dtOee.Rows[0]["OeeTotal"]);
                    data.OeeWarning = ToInt(dtOee.Rows[0]["Warning"]) == 1;
                    foreach (DataRow r in dtOee.Rows)
                    {
                        string line = r["Line"]?.ToString()?.Trim() ?? "";
                        if (line == "사출")
                        {
                            data.OeeSachulTime  = ToDouble(r["TimeRate"]);
                            data.OeeSachulPerf  = ToDouble(r["PerfRate"]);
                            data.OeeSachulYield = ToDouble(r["YieldRate"]);
                        }
                        else if (line == "조립")
                        {
                            data.OeeJoripTime   = ToDouble(r["TimeRate"]);
                            data.OeeJoripPerf   = ToDouble(r["PerfRate"]);
                            data.OeeJoripYield  = ToDouble(r["YieldRate"]);
                        }
                    }
                }

                // ── 품질현황 ──────────────────────────────────
                DataTable dtQual = SelectQuality(corpId);
                if (dtQual != null && dtQual.Rows.Count > 0)
                {
                    data.QualityRate  = ToDouble(dtQual.Rows[0]["QualityRate"]);
                    data.EorRate      = ToDouble(dtQual.Rows[0]["EorRate"]);
                    data.WorstEquipList = new List<WorstEquipItem>();
                    foreach (DataRow r in dtQual.Rows)
                    {
                        data.WorstEquipList.Add(new WorstEquipItem
                        {
                            EquipName = r["EquipName"]?.ToString() ?? "",
                            EquipType = r["EquipType"]?.ToString() ?? "",
                            BadRate   = ToDouble(r["BadRate"]),
                            BadCount  = ToInt(r["BadCount"])
                        });
                    }
                }

                // ── 납품현황 ──────────────────────────────────
                DataTable dtDel = SelectDelivery(corpId);
                if (dtDel != null && dtDel.Rows.Count > 0)
                {
                    DataRow r = dtDel.Rows[0];
                    data.GrRate           = ToDouble(r["GrRate"]);
                    data.TgRate           = ToDouble(r["TgRate"]);
                    data.LastRetrieveTime = r["LastRetrieveTime"]?.ToString() ?? "";
                    data.DoCnt = ToInt(r["DoCnt"]);
                    data.GrCnt = ToInt(r["GrCnt"]);
                    data.TgCnt = ToInt(r["TgCnt"]);
                    data.TgOk  = ToInt(r["TgOk"]);
                }

                // ── 재고현황 ──────────────────────────────────
                DataTable dtInv = SelectInventory(corpId);
                if (dtInv != null && dtInv.Rows.Count > 0)
                {
                    data.InventoryList = new List<InventoryItem>();
                    foreach (DataRow r in dtInv.Rows)
                    {
                        data.InventoryList.Add(new InventoryItem
                        {
                            Name      = r["Name"]?.ToString()     ?? "",
                            Day5      = ToInt(r["Day5"]),
                            Day15     = ToInt(r["Day15"]),
                            Day16Over = ToInt(r["Day16Over"])
                        });
                    }
                    data.StockRate = ToDouble(dtInv.Rows[0]["StockRate"]);
                }

                return data;
            }
            catch (Exception)
            {
                return ComprehensiveData.CreateSample();
            }
        }

        // ── 내부 헬퍼 ─────────────────────────────────────────
        private int ToInt(object val)
        {
            if (val == null || val == System.DBNull.Value) return 0;
            int result;
            return int.TryParse(val.ToString(), out result) ? result : 0;
        }

        private double ToDouble(object val)
        {
            if (val == null || val == System.DBNull.Value) return 0.0;
            double result;
            return double.TryParse(val.ToString(), out result) ? result : 0.0;
        }
    }
}
