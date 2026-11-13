using DevExpress.Utils.About;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
        public decimal? UnitPrice { get; set; }
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
        public string ReceiveHouseId { get; set; }
        public string MrpNo { get; set; }
        public bool BomChecked { get; set; }
        internal static bool UseYozmOrderColumns => string.Equals(Database.DbName, "YOZM", StringComparison.OrdinalIgnoreCase);
        internal static string NewOrderState => UseYozmOrderColumns ? "발주" : "진행";
        internal static string ReceivedState => UseYozmOrderColumns ? "입고" : "완료";

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_MM_PlacingD_MmOrderNo()
		{
            string yozmColumns = UseYozmOrderColumns ? @"
                              , CM.ItemType
                              , CM.DeliveryOverTolerancePct
                              , CM.PurchaseConversionQty
                              , PD.ReceiveHouseId
                              , PD.MrpNo
                              , PD.BomChecked
                              , COALESCE((SELECT SUM(R.Qty) FROM dbo.MM_PurchaseD R
                                  WHERE R.CorpId = PD.CorpId AND R.MmOrderNo = PD.MmOrderNo
                                    AND R.MmOrderSeq = PD.MmOrderSeq), 0) AS DeliveredQty" : "";
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
                              {yozmColumns}

                        FROM MM_PlacingD PD
						LEFT JOIN CM_Item CM ON PD.CorpId = CM.CorpId AND PD.ItemCode = cm.ItemCode
                        WHERE PD.CorpId = '{WrGlobal.CorpID}'
                        AND MmOrderNo = '{MmOrderNo}'
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
                            AND PM.State = N'{NewOrderState}'
                            AND (DB_NAME() <> N'YOZM' OR PM.CloseDate IS NULL OR PM.CloseDate <= '19000101')
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
            string house = UseYozmOrderColumns ? ", PD.ReceiveHouseId" : "";
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
                              {house}

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
                        AND (DB_NAME() <> N'YOZM' OR PM.CloseDate IS NULL OR PM.CloseDate <= '19000101')
            ";

            return db.GetDataTable(query);
		}

        public bool Insert_MM_PlacingD()
        {
            return ExecuteOrderWrite(BuildInsertQuery());
        }

        internal static DataTable SelectReceiptLimits(string orderNo, string receiptNo)
        {
            const string sql = @"
                SELECT D.MmOrderSeq, D.ItemCode, M.CustCode, M.CloseDate,
                       D.Qty * (1 + COALESCE(I.DeliveryOverTolerancePct, 0) / 100.0) - ISNULL(R.Qty, 0) AS AvailableQty
                FROM dbo.MM_PlacingD D
                JOIN dbo.MM_PlacingM M ON M.CorpId = D.CorpId AND M.MmOrderNo = D.MmOrderNo
                LEFT JOIN dbo.CM_Item I ON I.CorpId = D.CorpId AND I.ItemCode = D.ItemCode
                OUTER APPLY (
                    SELECT SUM(P.Qty) AS Qty FROM dbo.MM_PurchaseD P
                    WHERE P.CorpId = D.CorpId AND P.MmOrderNo = D.MmOrderNo AND P.MmOrderSeq = D.MmOrderSeq
                      AND P.StoNo <> @ReceiptNo
                ) R
                WHERE D.CorpId = @CorpId AND D.MmOrderNo = @OrderNo";
            using (var connection = new SqlConnection(Database.m_ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                command.Parameters.Add("@CorpId", SqlDbType.NVarChar, 10).Value = WrGlobal.CorpID;
                command.Parameters.Add("@OrderNo", SqlDbType.NVarChar, 20).Value = orderNo;
                command.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar, 20).Value = receiptNo ?? "";
                var rows = new DataTable();
                adapter.Fill(rows);
                return rows;
            }
        }

        internal string BuildInsertQuery()
        {
            string columns = UseYozmOrderColumns ? ", ReceiveHouseId, MrpNo, BomChecked" : "";
            string values = UseYozmOrderColumns ? ", @ReceiveHouseId, @MrpNo, @BomChecked" : "";
            return $@"
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
                           {columns}
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
                           , {Qty.ConvertFloat_Null_To_Zero()}
                           , {UnitPriceSql}
                           , {Price.ConvertFloat_Null_To_Zero()}
                           , {VatPrice.ConvertFloat_Null_To_Zero()}
                           , {TotalPrice.ConvertFloat_Null_To_Zero()}
                           , {VatType.ConvertStr_Empty_To_Null()}
                           , {VatRate.ConvertInt_Null_To_Zero()}
                           , {InQty.ConvertFloat_Null_To_Zero()}
                           , {BalanceQty.ConvertFloat_Null_To_Zero()}
                           , {LossRate.ConvertFloat_Null_To_Zero()}
                           , '{DeliveryDate.ConvertDate_To_Char10()}'
                           , {State.ConvertStr_Empty_To_Null()}
                           , {CloseDate.ConvertDate_To_Char10().ConvertStr_Empty_To_Null()}
                           , {CloseUser.ConvertStr_Empty_To_Null()}
                           , {Remark.ConvertStr_Empty_To_Null()}
                           , '{WrGlobal.LoginID}'
                           , '{WrGlobal.ClientHostName}'
                           , GETDATE()
                           {values}
                       )";
        }

        public bool Update_MM_PlacingD()
        {
            string extra = UseYozmOrderColumns ? @"
                              ReceiveHouseId = @ReceiveHouseId,
                              MrpNo = @MrpNo,
                              BomChecked = @BomChecked," : "";
            query = $@"
                        UPDATE MM_PlacingD
                        SET
                              MmOrderSeq = {MmOrderSeq.ConvertInt_Null_To_Zero()},
                              SdOrderNo = {SdOrderNo.ConvertStr_Empty_To_Null()},
                              SdOrderSeq = {SdOrderSeq.ConvertInt_Null_To_Zero()},
                              OrderNo = {OrderNo.ConvertStr_Empty_To_Null()},
                              ItemCode = {ItemCode.ConvertStr_Empty_To_Null()},
                              UnitCode = {UnitCode.ConvertStr_Empty_To_Null()},
                              Qty = '{Qty.ConvertFloat_Null_To_Zero()}',
                              UnitPrice = {UnitPriceSql},
                              {extra}
                              Price = '{Price.ConvertFloat_Null_To_Zero()}',
                              VatPrice = '{VatPrice.ConvertFloat_Null_To_Zero()}',
                              TotalPrice = '{TotalPrice.ConvertFloat_Null_To_Zero()}',
                              VatType = {VatType.ConvertStr_Empty_To_Null()},
                              VatRate = '{VatRate.ConvertInt_Null_To_Zero()}',
                              InQty = '{InQty.ConvertFloat_Null_To_Zero()}',
                              BalanceQty = '{BalanceQty.ConvertFloat_Null_To_Zero()}',
                              LossRate = '{LossRate.ConvertFloat_Null_To_Zero()}',
                              DeliveryDate = '{DeliveryDate.ConvertDate_To_Char10()}',
                              State = {State.ConvertStr_Empty_To_Null()},
                              CloseDate = '{CloseDate.ConvertDate_To_Char10()}',
                              CloseUser = {CloseUser.ConvertStr_Empty_To_Null()},
                              Remark = {Remark.ConvertStr_Empty_To_Null()},
                              ModId = '{WrGlobal.LoginID}',
                              ModIP = '{WrGlobal.ClientHostName}',
                              ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND MmOrderNo = {MmOrderNo.ConvertStr_Empty_To_Null()}
                          AND MmOrderSeq = {MmOrderSeq.ConvertInt_Null_To_One()}
                    ";

            return ExecuteOrderWrite(query);
        }

        private string UnitPriceSql => UseYozmOrderColumns ? "@UnitPrice" : (UnitPrice ?? 0m).ToString(CultureInfo.InvariantCulture);

        private bool ExecuteOrderWrite(string sql)
        {
            if (!UseYozmOrderColumns)
                return db.ExecuteQuery(sql);

            using (var command = new SqlCommand(sql))
            {
                command.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Precision = 22;
                command.Parameters["@UnitPrice"].Scale = 4;
                command.Parameters["@UnitPrice"].Value = UnitPrice ?? 0m;
                command.Parameters.Add("@ReceiveHouseId", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(ReceiveHouseId) ? (object)DBNull.Value : ReceiveHouseId;
                command.Parameters.Add("@MrpNo", SqlDbType.NVarChar, 20).Value = string.IsNullOrWhiteSpace(MrpNo) ? (object)DBNull.Value : MrpNo;
                command.Parameters.Add("@BomChecked", SqlDbType.Bit).Value = BomChecked;
                return db.ExecuteCommand(command);
            }
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
