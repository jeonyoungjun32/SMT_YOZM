using DevExpress.Utils.About;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SmartCommon.Common;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using DevExpress.XtraRichEdit.Model;

namespace SmartMES.Models.MM
{
	public class MM_PlacingD
    {
        #region Property
        public string CorpId { get; set; }
        public string MmOrderNo { get; set; }
        public int? MmOrderSeq { get; set; }
        public string SdOrderNo { get; set; }
        public int? SdOrderSeq { get; set; }
        public string OrderNo { get; set; }
        public string ItemCode { get; set; }
        public string UnitCode { get; set; }
        public float? Qty { get; set; }
        public int? UnitPrice { get; set; }
        public int? Price { get; set; }
        public int? VatPrice { get; set; }
        public int? TotalPrice { get; set; }
        public string VatType { get; set; }
        public int? VatRate { get; set; }
        public float? InQty { get; set; }
        public float? BalanceQty { get; set; }
        public float? LossRate { get; set; }
        public string DeliveryDate { get; set; }
        public string State { get; set; }
        public string CloseDate { get; set; }
        public string CloseUser { get; set; }
        public string Remark { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public DateTime? CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public DateTime? ModDt { get; set; }
        public string CustCode { get; set; }

        #endregion

        Database db = new Database();
        string query = "";
        internal static bool UsePlanningFields => string.Equals(Database.DbName, "YOZM", StringComparison.OrdinalIgnoreCase);
        private List<SqlParameter> planningParameters;

        /// <summary>재고 기준일로 원본 발주계획을 조회하고 현재 구매처 코드의 등록 여부를 표시합니다.</summary>
        internal DataTable SelectImportedPurchasePlan(DateTime startDate, DateTime endDate, string customerCode)
        {
            const string sql = @"
                SELECT P.BasisDate, P.ItemCode, P.SourceItemName, P.SupplierSourceCode, P.SupplierSourceName,
                       P.SourceUnit, P.UnitPrice, P.CurrentStockQty, P.StockCoverageMonths, P.PurchaseQty,
                       P.PurchaseAmount, P.MonthlyUsageQty, P.DailyUsageQty, P.DeliveryDate,
                       P.DeliveryRemainingQty, P.LeadTimeDays, P.DeliveryCoverageMonths, P.UsagePurpose,
                       P.UsageDepartment, P.Remark, P.MinimumOrderQty, P.PalletBasis, P.SourceExtraQty,
                       P.SourceRow, P.SourceIssues,
                       CASE WHEN C.CustCode IS NULL THEN N'미등록'
                            WHEN C.UseYn <> 'Y' OR C.UseYn IS NULL THEN N'사용 중지'
                            WHEN C.CustName <> P.SupplierSourceName THEN N'거래처명 확인'
                            ELSE N'연결' END AS SupplierStatus
                FROM dbo.MM_PlacingPlan P
                LEFT JOIN dbo.CM_Cust C ON C.CorpId = P.CorpId AND C.CustCode = P.SupplierSourceCode
                WHERE P.CorpId = @CorpId AND P.BasisDate BETWEEN @StartDate AND @EndDate
                  AND (@CustomerCode IS NULL OR P.SupplierSourceCode = @CustomerCode)
                ORDER BY P.BasisDate DESC, P.SourceRow;";
            using (var connection = new SqlConnection(Database.m_ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.Parameters.Add("@CorpId", SqlDbType.NVarChar, 10).Value = WrGlobal.CorpID;
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate.Date;
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate.Date;
                command.Parameters.Add("@CustomerCode", SqlDbType.NVarChar, 30).Value =
                    string.IsNullOrWhiteSpace(customerCode) || customerCode == "ALL" ? (object)DBNull.Value : customerCode;
                var rows = new DataTable();
                adapter.Fill(rows);
                return rows;
            }
        }

        private bool ExecuteDetailWrite()
        {
            if (planningParameters == null)
                return db.ExecuteQuery(query);

            using (var command = new SqlCommand(query))
            {
                command.Parameters.AddRange(planningParameters.ToArray());
                return db.ExecuteCommand(command);
            }
        }

        /// <summary>발주 편집 화면에서 받은 계획 입력값을 기존 상세 저장 명령에 포함합니다.</summary>
        internal void SetPlanningFields(DataRow row)
        {
            planningParameters = null;
            if (!UsePlanningFields)
                return;

            planningParameters = new List<SqlParameter>
            {
                new SqlParameter("@MonthlyUsageQty", SqlDbType.Decimal) { Precision = 19, Scale = 4, Value = row["MonthlyUsageQty"] },
                new SqlParameter("@MinimumOrderQty", SqlDbType.Decimal) { Precision = 19, Scale = 4, Value = row["MinimumOrderQty"] },
                new SqlParameter("@UsagePurpose", SqlDbType.NVarChar, 200) { Value = row["UsagePurpose"] },
                new SqlParameter("@UsageDepartment", SqlDbType.NVarChar, 100) { Value = row["UsageDepartment"] }
            };
        }

        // 기준 창고를 임의로 고르지 않고 사업장 내 해당 품목의 입출고를 합산합니다.
        private const string CurrentStockSql = @"
            SELECT SUM(CASE s.StockType
                WHEN N'창고입고재고' THEN s.Qty
                WHEN N'창고출고재고' THEN -s.Qty END)
            FROM dbo.StockInOutView s
            WHERE s.CorpId = CM.CorpId AND s.ItemCode = CM.ItemCode
                AND s.InOutDate < DATEADD(day, 1, CONVERT(date, GETDATE()))";

        internal void FillPlanningItem(DataRow row, DataRow item)
        {
            if (!UsePlanningFields)
                return;

            string corp = WrGlobal.CorpID.Replace("'", "''");
            string code = Convert.ToString(item["ItemCode"]).Replace("'", "''");
            DataTable result = db.GetDataTable($@"
                SELECT CM.PTLoadQty, COALESCE(({CurrentStockSql}), 0) AS CurrentStockQty
                FROM dbo.CM_Item CM WHERE CM.CorpId = N'{corp}' AND CM.ItemCode = N'{code}'");
            if (result != null && result.Rows.Count == 1)
            {
                row["PTLoadQty"] = result.Rows[0]["PTLoadQty"];
                row["CurrentStockQty"] = result.Rows[0]["CurrentStockQty"];
            }
        }

        public DataTable Select_MM_PlacingD_MmOrderNo()
		{
            string planningColumns = UsePlanningFields ? $@"
                              , PM.CustCode
                              , Cust.CustName
                              , PM.PlacingDate
                              , PD.MonthlyUsageQty
                              , PD.MinimumOrderQty
                              , PD.UsagePurpose
                              , PD.UsageDepartment
                              , CM.PTLoadQty
                              , COALESCE(({CurrentStockSql}), 0) AS CurrentStockQty" : "";
            string planningJoins = UsePlanningFields ? @"
                        LEFT JOIN dbo.MM_PlacingM PM ON PM.CorpId = PD.CorpId AND PM.MmOrderNo = PD.MmOrderNo
                        LEFT JOIN dbo.CM_Cust Cust ON Cust.CorpId = PM.CorpId AND Cust.CustCode = PM.CustCode" : "";
            query = $@"
                        SELECT  '' AS Status
                              , PD.CorpId
                              , pd.MmOrderNo
                              , pd.MmOrderSeq
                              , pd.SdOrderNo
                              , pd.SdOrderSeq
                              , pd.OrderNo

                              , PD.ItemCode
                              , CM.ItemName
                              , CM.ItemClass1
                              , CM.ItemClass2
                              , CM.ItemClass3
                              , CM.Size
                              , CM.Alias
                              , CM.Standard
                              , CM.UnitWeight
                              , CM.Type
                              , CM.Texture

                              , PD.UnitCode
                              , pd.Qty
                              , PD.UnitPrice
                              , pd.Price
                              , pd.VatPrice
                              , pd.TotalPrice
                              , pd.VatType
                              , pd.VatRate
                              , pd.InQty
                              , pd.BalanceQty
                              , pd.LossRate
                              , pd.DeliveryDate
                              , pd.State
                              , pd.CloseDate
                              , pd.CloseUser
                              , PD.Remark
                              , PD.CreId
                              , PD.CreIP
                              , PD.CreDt
                              , PD.ModId
                              , PD.ModIP
                              , PD.ModDt
                              , CM.Size
                              , CM.UnitCode
                              {planningColumns}

                        FROM MM_PlacingD PD
						LEFT JOIN CM_Item CM ON PD.CorpId = CM.CorpId AND PD.ItemCode = cm.ItemCode
                        {planningJoins}
                        WHERE PD.CorpId = '{WrGlobal.CorpID}'
                        AND PD.MmOrderNo = '{MmOrderNo}'
                        ";

            if(ItemCode != null && ItemCode != "" && ItemCode.ToUpper() != "ALL")
			{
                query += $"\nAND PD.ItemCode = '{ItemCode}'";
			}

            DataTable dt = db.GetDataTable(query);
            return dt;
		}

        public DataTable Select_Placing_Cust()
        {
            query = $@"
                        SELECT 
                              PD.CorpId
                              , PD.MmOrderNo
                              , MmOrderSeq
                              , SdOrderNo
                              , SdOrderSeq
                              , OrderNo
                              , PD.ItemCode
                              , PD.UnitCode
                              , Qty
                              , PD.UnitPrice
                              , PD.Price
                              , PD.VatPrice
                              , PD.TotalPrice
                              , PD.VatType
                              , PD.VatRate
                              , 0 InQty
                              , Qty - ISNULL(InQty,0) BalanceQty
                              , LossRate
                              , PD.DeliveryDate
                              , PD.State
                              , PD.CloseDate
                              , CloseUser
                              , PD.Remark
                              , PD.CreId
                              , PD.CreIP
                              , PD.CreDt
                              , PD.ModId
                              , PD.ModIP
                              , PD.ModDt

                              , CM.ItemName
                              , CM.Size
							  , CM.UnitCode
                              , CM.ItemClass1
                              , CM.ItemClass2
                              , CM.ItemClass3
							  , PM.CustCode

                        FROM MM_PlacingD PD
                        LEFT JOIN MM_PlacingM PM ON PD.CorpId = PM.CorpId AND PD.MmOrderNo = PM.MmOrderNo
						LEFT JOIN CM_Item CM ON PD.CorpId = CM.CorpId AND PD.ItemCode = cm.ItemCode
                        WHERE PD.CorpId = '{WrGlobal.CorpID}'
                            AND PM.State = '진행'
                        ";

            if (CustCode != null && CustCode != "" && CustCode.ToUpper() != "ALL")
            {
                query += $"\nAND PM.CustCode = '{CustCode}'";
            }

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public DataTable Select_PlacingList(string startDate, string endDate, string placingType)
        {

            query = $@"
                        SELECT pd.MmOrderNo
                        , pd.MmOrderSeq
                        , pd.SdOrderNo
                        , pd.SdOrderSeq
                        , pd.ItemCode
                        , pd.Qty
                        , pd.UnitPrice
                        , pd.Price
                        , pd.VatPrice
                        , pd.TotalPrice
                        , pd.VatType
                        , pd.VatRate
                        , pd.InQty
                        , pd.BalanceQty
                        , pd.LossRate
                        , pd.DeliveryDate
                        , pd.State

                        , pm.CustCode
                        , pm.PlacingDate
                        --, pm.PlacingType

                        , cm.ItemName
                        , cm.Alias
                        , cm.ItemClass1
                        , cm.ItemClass2
                        , cm.ItemClass3
                        , cm.Size
                        , cm.UnitWeight
                        , cm.Texture
                        , cm.Type
                        , cm.UnitCode
                        , cm.ColorCode

                        , cci.Title AS PlacingType      -- 발주구분
                        , cci2.Title AS VatTitle         -- 부가세구분

                        FROM MM_PlacingD pd
                        LEFT JOIN MM_PlacingM pm ON pd.MmOrderNo = pm.MmOrderNo
                        LEFT JOIN CM_Item cm ON pd.ItemCode = cm.ItemCode
                        LEFT JOIN CM_CateItem cci ON pm.PlacingType = cci.ClassItemCode AND cci.CLASS = 'I010'        -- 발주구분
                        LEFT JOIN CM_CateItem cci2 ON pm.VatType = cci2.ClassItemCode AND cci2.CLASS = 'C010'            -- 부가세구분
                        WHERE pd.CorpID = '{WrGlobal.CorpID}'
                        AND CONVERT(nvarchar(10), pm.PlacingDate, 23) BETWEEN '{startDate}' AND '{endDate}'
                ";

            if (State != "") query += $"\n AND pd.State = '{State}'";
            if (CustCode != "") query += $"\n AND pm.CustCode = '{CustCode}'";
            if (placingType != "") query += $"\n AND pm.PlacingType = '{placingType}'";

            return db.GetDataTable(query);
        }

        public DataTable SelectRemainList()
		{
            query = $@"
                        SELECT 
                              PD.CorpId
                              , PD.MmOrderNo
                              , pd.MmOrderSeq
                              , SdOrderNo
                              , SdOrderSeq
                              , OrderNo
                              , PD.ItemCode
                              , PD.UnitCode
                              , pd.Qty
                              , PD.UnitPrice
                              , PD.Price
                              , PD.VatPrice
                              , PD.TotalPrice
                              , PD.VatType
                              , PD.VatRate
                              , 0 InQty
                              , Qty - ISNULL(pc.InQty,0) RemainQty
                              , LossRate
                              , PD.DeliveryDate
                              , PD.State
                              , PD.CloseDate
                              , CloseUser
                              , PD.Remark
                              , PD.CreId
                              , PD.CreIP
                              , PD.CreDt
                              , PD.ModId
                              , PD.ModIP
                              , PD.ModDt

                              , CM.ItemName
                              , CM.Size
							  , CM.UnitCode
                              , CM.ItemClass1
                              , CM.ItemClass2
                              , CM.ItemClass3
							  , PM.CustCode

                        FROM MM_PlacingD PD
                        LEFT JOIN MM_PlacingM PM ON PD.CorpId = PM.CorpId AND PD.MmOrderNo = PM.MmOrderNo
						LEFT JOIN 
						(
						    SELECT pc.CorpId, pc.MmOrderNo, pc.MmOrderSeq, SUM(Qty) AS InQty
						    FROM MM_PurchaseD pc
						    GROUP BY pc.CorpId, pc.MmOrderNo, pc.MmOrderSeq
						) AS pc ON pd.CorpId = pc.CorpId AND pd.MmOrderNo = pc.MmOrderNo AND pc.MmOrderSeq = pd.MmOrderSeq
						LEFT JOIN CM_Item CM ON PD.CorpId = CM.CorpId AND PD.ItemCode = cm.ItemCode
						WHERE pd.CorpId = '{WrGlobal.CorpID}'
						AND pd.MmOrderNo = '{MmOrderNo}'
						AND pd.Qty - ISNULL(pc.InQty,0) > 0
            ";

            return db.GetDataTable(query);
		}

        public bool Insert_MM_PlacingD()
        {
            string planningColumns = planningParameters == null ? "" : ", MonthlyUsageQty, MinimumOrderQty, UsagePurpose, UsageDepartment";
            string planningValues = planningParameters == null ? "" : ", @MonthlyUsageQty, @MinimumOrderQty, @UsagePurpose, @UsageDepartment";
            query = $@"
                        INSERT INTO MM_PlacingD
                        (
                              CorpId
                            , MmOrderNo
                            , MmOrderSeq
                            , SdOrderNo
                            , SdOrderSeq
                            , OrderNo
                            , ItemCode
                            , UnitCode
                            , Qty
                            , UnitPrice
                            , Price
                            , VatPrice
                            , TotalPrice
                            , VatType
                            , VatRate
                            , InQty
                            , BalanceQty
                            , LossRate
                            , DeliveryDate
                            , State
                            , CloseDate
                            , CloseUser
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                            {planningColumns}
                        )
                        VALUES
                        (
                              '{WrGlobal.CorpID}'
                            , {MmOrderNo.ConvertStr_Empty_To_Null()}
                            , {MmOrderSeq.ConvertInt_Null_To_Zero()}
                            , {SdOrderNo.ConvertStr_Empty_To_Null()}
                            , {SdOrderSeq.ConvertInt_Null_To_Zero()}
                            , {OrderNo.ConvertStr_Empty_To_Null()}
                            , {ItemCode.ConvertStr_Empty_To_Null()}
                            , {UnitCode.ConvertStr_Empty_To_Null()}
                            , '{Qty.ConvertFloat_Null_To_Zero()}'
                            , '{UnitPrice.ConvertFloat_Null_To_Zero()}'
                            , '{Price.ConvertFloat_Null_To_Zero()}'
                            , '{VatPrice.ConvertFloat_Null_To_Zero()}'
                            , '{TotalPrice.ConvertFloat_Null_To_Zero()}'
                            , {VatType.ConvertStr_Empty_To_Null()}
                            , '{VatRate.ConvertInt_Null_To_Zero()}'
                            , '{InQty.ConvertFloat_Null_To_Zero()}'
                            , '{BalanceQty.ConvertFloat_Null_To_Zero()}'
                            , '{LossRate.ConvertFloat_Null_To_Zero()}'
                            , '{DeliveryDate.ConvertDate_To_Char10()}'
                            , {State.ConvertStr_Empty_To_Null()}
                            , '{CloseDate.ConvertDate_To_Char10()}'
                            , {CloseUser.ConvertStr_Empty_To_Null()}
                            , {Remark.ConvertStr_Empty_To_Null()}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                            {planningValues}
                        )
                        ";

            return ExecuteDetailWrite();
        }

        public bool Update_MM_PlacingD()
        {
            // 발주 편집 화면에서 바꾸지 않는 입고·마감·연계 값은 기존 값을 유지합니다.
            string extraFields = planningParameters == null ? $@",
                              SdOrderNo = {SdOrderNo.ConvertStr_Empty_To_Null()},
                              SdOrderSeq = {SdOrderSeq.ConvertInt_Null_To_Zero()},
                              OrderNo = {OrderNo.ConvertStr_Empty_To_Null()},
                              InQty = '{InQty.ConvertFloat_Null_To_Zero()}',
                              BalanceQty = '{BalanceQty.ConvertFloat_Null_To_Zero()}',
                              LossRate = '{LossRate.ConvertFloat_Null_To_Zero()}',
                              State = {State.ConvertStr_Empty_To_Null()},
                              CloseDate = '{CloseDate.ConvertDate_To_Char10()}',
                              CloseUser = {CloseUser.ConvertStr_Empty_To_Null()}" : @",
                              MonthlyUsageQty = @MonthlyUsageQty,
                              MinimumOrderQty = @MinimumOrderQty,
                              UsagePurpose = @UsagePurpose,
                              UsageDepartment = @UsageDepartment";
            query = $@"
                        UPDATE MM_PlacingD
                        SET
                              MmOrderSeq = {MmOrderSeq.ConvertInt_Null_To_Zero()},
                              ItemCode = {ItemCode.ConvertStr_Empty_To_Null()},
                              UnitCode = {UnitCode.ConvertStr_Empty_To_Null()},
                              Qty = '{Qty.ConvertFloat_Null_To_Zero()}',
                              UnitPrice = '{UnitPrice.ConvertFloat_Null_To_Zero()}',
                              Price = '{Price.ConvertFloat_Null_To_Zero()}',
                              VatPrice = '{VatPrice.ConvertFloat_Null_To_Zero()}',
                              TotalPrice = '{TotalPrice.ConvertFloat_Null_To_Zero()}',
                              VatType = {VatType.ConvertStr_Empty_To_Null()},
                              VatRate = '{VatRate.ConvertInt_Null_To_Zero()}',
                              DeliveryDate = '{DeliveryDate.ConvertDate_To_Char10()}',
                              Remark = {Remark.ConvertStr_Empty_To_Null()},
                              ModId = '{WrGlobal.LoginID}',
                              ModIP = '{WrGlobal.ClientHostName}',
                              ModDt = GETDATE()
                              {extraFields}
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND MmOrderNo = {MmOrderNo.ConvertStr_Empty_To_Null()}
                          AND MmOrderSeq = {MmOrderSeq.ConvertInt_Null_To_One()}
                    ";

            return ExecuteDetailWrite();
        }

        public bool Delete_MM_PlacingD()
        {
            query = $@"
                        DELETE MM_PlacingD
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND MmOrderNo = {MmOrderNo.ConvertStr_Empty_To_Null()}
                          AND MmOrderSeq = {MmOrderSeq.ConvertInt_Null_To_One()}
                    ";

            return db.ExecuteQuery(query);
        }

    }
}
